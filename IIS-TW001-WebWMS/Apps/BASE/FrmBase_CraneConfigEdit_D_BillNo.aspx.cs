using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_FrmBase_CraneConfigEdit_D_BillNo : WMSBasePage
{
    /// <summary>
    /// snID
    /// </summary>
    public string Id
    {
        get { return this.hiddId.Value; }
        set { this.hiddId.Value = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            GetParameters();
            this.InitPage();
        }
    }

    /// <summary>
    /// 获取传入的页面参数
    /// </summary>
    private void GetParameters()
    {
        if (string.IsNullOrEmpty(this.Request.QueryString["ID"]))
        {
            this.Id = Guid.Empty.ToString();
        }
        else
        {
            this.Id = this.Request.QueryString["ID"];
        }
    }

    private void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "CloseMySelf('BILLNO');return false;";
        this.hiddUser.Value = WmsWebUserInfo.GetCurrentUser().UserName;
        LoadDate();
    }

    private void LoadDate() {
        //拼接数据列表
        StringBuilder nodeHtml = new StringBuilder();
        var mod = context.BASE_CRANECONFIG_TRADETYPE.Where(x => x.IDS == this.Id).FirstOrDefault();
        if (mod != null)
        {
            if (mod.INOROUTCODE == "O")
            {
                List<BASE_TypeMapping> outbillNoList = context.BASE_TypeMapping.Where(x => x.WMS_TypeCode == mod.CERPCODE && x.CStatus == "0" && x.type == "OutBillNo").OrderBy(x => x.ERP_TypeCode).ToList();
                if (outbillNoList != null && outbillNoList.Any())
                {
                    List<BASE_CRANECONFIG_TRADETYPE_D> dList = context.BASE_CRANECONFIG_TRADETYPE_D.Where(x => x.tradetypeids == this.Id).ToList();

                    foreach (var item in outbillNoList) {
                        nodeHtml.Append("<tr>");
                        nodeHtml.Append("<td>");

                        string checkStr = "";
                        if (dList != null && dList.Any(x => x.billno == item.ERP_TypeCode)) {
                            checkStr = " checked=\"checked\" ";
                        }
                        nodeHtml.Append("<input id=\"chkmandatory\" type=\"checkbox\"   " + checkStr + "/>");
                        nodeHtml.Append("</td>");
                        nodeHtml.Append("<td class=\"erpcode\">" + item.ERP_TypeCode + "</td>");
                        nodeHtml.Append("<td>" + item.ERP_TypeName + "</td>");
                        nodeHtml.Append("<td>" + item.WMS_TypeCode + "</td>");
                        nodeHtml.Append("<td>" + item.WMS_TypeName + "</td>");
                        nodeHtml.Append("</tr>");
                    }
                }
                else {
                    nodeHtml.Append("<tr><td colspan=\"5\"><%= Resources.Lang.FrmBase_CraneConfigEdit_D_BillNo_Msg01%></td></tr>");//暂无配置数据
                }
            }
            else
            {
                List<BASE_TypeMapping> inbillNoList = context.BASE_TypeMapping.Where(x => x.WMS_TypeCode == mod.CERPCODE && x.CStatus == "0" && x.type == "InBillNo").OrderBy(x => x.ERP_TypeCode).ToList();
                if (inbillNoList != null && inbillNoList.Any())
                {
                    List<BASE_CRANECONFIG_TRADETYPE_D> dList = context.BASE_CRANECONFIG_TRADETYPE_D.Where(x => x.tradetypeids == this.Id).ToList();

                    foreach (var item in inbillNoList)
                    {
                        nodeHtml.Append("<tr>");
                        nodeHtml.Append("<td>");

                        string checkStr = "";
                        if (dList != null && dList.Any(x => x.billno == item.ERP_TypeCode))
                        {
                            checkStr = " checked=\"checked\" ";
                        }
                        nodeHtml.Append("<input id=\"chkmandatory\" type=\"checkbox\"   " + checkStr + "/>");
                        nodeHtml.Append("</td>");
                        nodeHtml.Append("<td class=\"erpcode\">" + item.ERP_TypeCode + "</td>");
                        nodeHtml.Append("<td>" + item.ERP_TypeName + "</td>");
                        nodeHtml.Append("<td>" + item.WMS_TypeCode + "</td>");
                        nodeHtml.Append("<td>" + item.WMS_TypeName + "</td>");
                        nodeHtml.Append("</tr>");
                    }
                }
                else {
                    nodeHtml.Append("<tr><td colspan=\"5\"><%= Resources.Lang.FrmBase_CraneConfigEdit_D_BillNo_Msg01%></td></tr>");//暂无配置数据
                }
            }
        }
        else {
            nodeHtml.Append("<tr><td colspan=\"5\"><%= Resources.Lang.FrmBase_CraneConfigEdit_D_BillNo_Msg01%></td></tr>");//暂无配置数据
        }
        tbBillNo.InnerHtml = nodeHtml.ToString();
    }


    [WebMethod]
    public static string SaveBills(string ids, string billnos, string user)
    {
        DBContext context = new DBContext();
        using (var modContext = context)
        {
            try
            {
                var mod = modContext.BASE_CRANECONFIG_TRADETYPE.Where(x => x.IDS == ids).FirstOrDefault();
                if (mod == null) {
                    return Resources.Lang.Common_SuccessFail;//"保存失败！";
                }

                //已有的关系需删除
                List<BASE_CRANECONFIG_TRADETYPE_D> dList = modContext.BASE_CRANECONFIG_TRADETYPE_D.Where(x => x.tradetypeids == ids).ToList();
                if (dList.Any())
                {
                    foreach (var item in dList)
                    {
                        modContext.BASE_CRANECONFIG_TRADETYPE_D.Remove(item);
                    }
                    modContext.SaveChanges();
                }

                if (!string.IsNullOrEmpty(billnos)) {
                    string[] bills = billnos.Split(',');
                    List<BASE_TypeMapping> billNoList = null;
                    if (mod.INOROUTCODE == "O")
                    {
                        billNoList = modContext.BASE_TypeMapping.Where(x => x.WMS_TypeCode == mod.CERPCODE && x.CStatus == "0" && x.type == "OutBillNo").OrderBy(x => x.ERP_TypeCode).ToList();
                    }
                    else
                    {
                        billNoList = modContext.BASE_TypeMapping.Where(x => x.WMS_TypeCode == mod.CERPCODE && x.CStatus == "0" && x.type == "InBillNo").OrderBy(x => x.ERP_TypeCode).ToList();
                    }
                    if (billNoList != null && billNoList.Any())
                    {
                        foreach (var item in bills)
                        {
                            var billItem = billNoList.Where(x => x.ERP_TypeCode == item).FirstOrDefault();
                            if (billItem != null) {
                                BASE_CRANECONFIG_TRADETYPE_D modTD = new BASE_CRANECONFIG_TRADETYPE_D();
                                modTD.id = Guid.NewGuid().ToString();
                                modTD.tradetypeids = ids;
                                modTD.siteid = mod.ID;
                                modTD.billno = billItem.ERP_TypeCode;
                                modTD.billname = billItem.ERP_TypeName;
                                modTD.createuser = user;
                                modTD.createtime = DateTime.Now;
                                modContext.BASE_CRANECONFIG_TRADETYPE_D.Add(modTD);
                            }
                        }
                        modContext.SaveChanges();
                    }
                }
                
                return "0";
            }
            catch (Exception ex) {
                return Resources.Lang.Common_SuccessFail + "：" + ex.ToString();//保存失败！
            }
        }
        
    }

}