using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

/// <summary>
/// SeralService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class SeralService : System.Web.Services.WebService {

    public SeralService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public WebServiceResult SaveSeral(SeralSaveParameter parameter)
    {
        WebServiceResult result = new WebServiceResult("0", "保存成功！");
        if (parameter == null)
        {
            result.code = "1";
            result.msg = "参数错误，保存失败！";
            return result;
        }

        DBContext context = new DBContext();
        try
        {
            using (var modContext = context)
            {
                if (parameter.OrderType.Equals("IN"))
                {

                    #region 判断序列号是否已存在(入了又出的，可再次入)
                    //该序列号入库次数
                    int inCount = modContext.BASE_SERIAL_NUMBER.Where(x => x.serialno == parameter.SeralNo && x.dtype == "1").Count();
                    //该序列号出库次数
                    int outCount = modContext.BASE_SERIAL_NUMBER.Where(x => x.serialno == parameter.SeralNo && x.dtype == "2").Count();
                    if (inCount != outCount) {
                        result.code = "1";
                        result.msg = "序列号存在！";
                        return result;
                    }
                    #endregion

                    var modInBill = modContext.INBILL.Where(x => x.cticketcode == parameter.CticketCode).FirstOrDefault();
                    if (modInBill != null)
                    {
                        //该物料需要扫的序列号数
                        decimal NeedCount = modContext.INBILL_D.Where(x => x.cinvcode == parameter.CinvCode && x.id == modInBill.id).Sum(x => x.iquantity.Value);
                        int SavedCount = modContext.BASE_SERIAL_NUMBER.Where(x => x.billcticketcode == modInBill.cticketcode && x.cinvcode == parameter.CinvCode).Count();
                        if (NeedCount > Convert.ToDecimal(SavedCount))
                        {
                            var modInAsn = modContext.INASN.Where(x => x.id == modInBill.casnid).FirstOrDefault();
                            
                            BASE_SERIAL_NUMBER modSeral = new BASE_SERIAL_NUMBER();
                            modSeral.id = Guid.NewGuid().ToString();
                            modSeral.cticketcode = modInAsn != null ? modInAsn.cticketcode : "";
                            modSeral.billcticketcode = modInBill.cticketcode;
                            modSeral.cerpcode = modInBill.cerpcode;
                            modSeral.cinvcode = parameter.CinvCode;
                            modSeral.serialno = parameter.SeralNo;
                            modSeral.quantity = 1;
                            modSeral.dtype = "1";
                            modSeral.cstatus = "0";
                            modSeral.dcreatetime = DateTime.Now;
                            modSeral.lastupdatetime = DateTime.Now;
                            modSeral.ccreateuser = parameter.User;
                            modSeral.lastupdateuser = parameter.User;

                            modContext.BASE_SERIAL_NUMBER.Add(modSeral);
                            modContext.SaveChanges();
                        }
                        else {
                            result.code = "1";
                            result.msg = "序列号已维护结束，请勿再添加！";
                        }
                    }
                    else {
                        result.code = "1";
                        result.msg = "未找到单据,保存失败！";
                    }
                }
                else if (parameter.OrderType.Equals("OUT"))
                {
                    #region 出库时判断序列号是否存在
                    var config = modContext.SYS_CONFIG.Where(x => x.code == "140114").FirstOrDefault();
                    if (config != null && config.config_value == "1") {
                        //取该序列号最近的一笔入库记录
                        var inSeral = modContext.BASE_SERIAL_NUMBER.Where(x => x.serialno == parameter.SeralNo && x.dtype == "1").OrderByDescending(x=>x.dcreatetime).FirstOrDefault();
                        if (inSeral != null)
                        {
                            var outSeral = modContext.BASE_SERIAL_NUMBER.Where(x => x.serialno == parameter.SeralNo && x.dtype == "2" && x.dcreatetime > inSeral.dcreatetime).FirstOrDefault();
                            if (outSeral != null)
                            {
                                result.code = "1";
                                result.msg = "该序列号已被使用！";
                                return result;
                            }

                            if (inSeral.cstatus != "2") {
                                result.code = "1";
                                result.msg = "该序列号还未完成入库，暂不能使用！";
                                return result;
                            }
                            if (inSeral.cinvcode != parameter.CinvCode) {
                                result.code = "1";
                                result.msg = "该序列号不属于该物料，不能使用！";
                                return result;
                            }
                        }
                        else {
                            result.code = "1";
                            result.msg = "该序列号不存在！";
                            return result;
                        }
                    }
                    #endregion

                    var modOutBill = modContext.OUTBILL.Where(x => x.cticketcode == parameter.CticketCode).FirstOrDefault();
                    if (modOutBill != null)
                    {
                        //该物料需要扫的序列号数
                        decimal NeedCount = modContext.OUTBILL_D.Where(x => x.cinvcode == parameter.CinvCode && x.id == modOutBill.id).Sum(x => x.iquantity);
                        int SavedCount = modContext.BASE_SERIAL_NUMBER.Where(x => x.cticketcode == modOutBill.cticketcode && x.cinvcode == parameter.CinvCode).Count();
                        if (NeedCount > Convert.ToDecimal(SavedCount))
                        {
                            BASE_SERIAL_NUMBER modSeral = new BASE_SERIAL_NUMBER();
                            modSeral.id = Guid.NewGuid().ToString();
                            modSeral.cticketcode = modOutBill.cticketcode;
                            modSeral.billcticketcode = "";
                            modSeral.cerpcode = modOutBill.cerpcode;
                            modSeral.cinvcode = parameter.CinvCode;
                            modSeral.serialno = parameter.SeralNo;
                            modSeral.quantity = 1;
                            modSeral.dtype = "2";
                            modSeral.cstatus = "0";
                            modSeral.dcreatetime = DateTime.Now;
                            modSeral.lastupdatetime = DateTime.Now;
                            modSeral.ccreateuser = parameter.User;
                            modSeral.lastupdateuser = parameter.User;

                            modContext.BASE_SERIAL_NUMBER.Add(modSeral);
                            modContext.SaveChanges();
                        }
                        else
                        {
                            result.code = "1";
                            result.msg = "序列号已维护结束，请勿再添加！";
                        }
                    }
                    else
                    {
                        result.code = "1";
                        result.msg = "未找到单据,保存失败！";
                    }
                }
                else {
                    result.code = "1";
                    result.msg = "参数错误！";
                }
            }
        }
        catch (Exception ex) {
            result.code = "1";
            result.msg = "保存失败！" + ex.ToString();
        }
        return result;
    }

    [WebMethod]
    public WebServiceResult GetSerals(string OrderType, string OrderCode, string CinvCode)
    {
        WebServiceResult result = new WebServiceResult("0", "");
        DBContext context = new DBContext();
        //拼接数据列表
        StringBuilder nodeHtml = new StringBuilder();
        using (var modContext = context)
        {
            try
            {
                //已有的规则明细需删除
                List<BASE_SERIAL_NUMBER> seralList = null;
                if (OrderType == "IN")
                {
                    seralList = modContext.BASE_SERIAL_NUMBER.Where(x => x.cinvcode == CinvCode && x.billcticketcode == OrderCode && x.dtype == "1").OrderByDescending(x=>x.dcreatetime).ToList();
                }
                else if (OrderType == "OUT")
                {
                    seralList = modContext.BASE_SERIAL_NUMBER.Where(x => x.cinvcode == CinvCode && x.cticketcode == OrderCode && x.dtype == "2").OrderByDescending(x => x.dcreatetime).ToList();
                }

                if (seralList!= null && seralList.Any())
                {
                    result.code = seralList.Count.ToString();
                    int seq = 1;
                    foreach (var item in seralList)
                    {
                        nodeHtml.Append("<tr data-id=\"" + item.id + "\" >");
                        nodeHtml.Append("<td class=\"txtItemTitle\">" + seq.ToString() + "</td>");
                        nodeHtml.Append("<td>" + item.serialno + "</td>");
                        nodeHtml.Append("<td>" + item.dcreatetime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "</td>");
                        nodeHtml.Append("<td>" + item.ccreateuser + "</td>");

                        nodeHtml.Append("<td>");
                        if (item.cstatus == "0")
                        {
                            nodeHtml.Append("<a href=\"javascript:;\" data-id=\"" + item.id + "\" class=\"cancelButton\" onclick=\"WMSUI.DeleteSeralNo(this);\">" + Resources.Lang.FrmBar_CodePrintEdit_Th_ShangChu + "</a>");
                        }
                        nodeHtml.Append("</td>");
                        nodeHtml.Append("</tr>");
                        seq++;
                    }
                }
                else
                {
                    nodeHtml.Append("<tr><td colspan=\"5\">暂无序列号</td></tr>");
                }

                result.msg = nodeHtml.ToString();
            }
            catch (Exception ex)
            {
                result.code = "-1";
                result.msg = "保存失败，错误信息：" + ex.Message;
            }
        }
        return result;
    }

    [WebMethod]
    public WebServiceResult DeleteSeralNo(string id) {
        WebServiceResult result = new WebServiceResult("0", "");
        DBContext context = new DBContext();
        using (var modContext = context) {
            try
            {
                var modSeral = modContext.BASE_SERIAL_NUMBER.Where(x => x.id == id).FirstOrDefault();
                if (modSeral != null)
                {
                    if (modSeral.cstatus == "0")
                    {
                        modContext.BASE_SERIAL_NUMBER.Remove(modSeral);
                        modContext.SaveChanges();
                    }
                    else {
                        result.code = "1";
                        result.msg = "当前序列号状态不能删除！";
                    }
                }
                else {
                    result.code = "1";
                    result.msg = "未找到要删除的序列号,删除失败！";
                }
            }
            catch (Exception ex) {
                result.code = "1";
                result.msg = "删除异常，错误信息：" + ex.Message;
            }
        }
        return result;
    }
}


public class SeralSaveParameter
{
    public string CinvCode { get; set; }
    public string CticketCode { get; set; }
    public string OrderType { get; set; }
    public string SeralNo { get; set; }
    public string User { get; set; }
}
