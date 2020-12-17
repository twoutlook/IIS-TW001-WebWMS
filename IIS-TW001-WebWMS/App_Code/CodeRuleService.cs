using DreamTek.ASRS.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// CodeRuleService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class CodeRuleService : System.Web.Services.WebService
{
    public string basicData = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    [WebMethod]
    public WebServiceResult SaveCodeRules(CodeRuleParameter parameter)
    {
        WebServiceResult result = new WebServiceResult("0", "保存成功！");
        if (parameter == null)
        {
            result.code = "1";
            result.msg = "参数错误，保存失败！";
            return result;
        }

        #region 部分规则再验证
        if (parameter.Rules == null || !parameter.Rules.Any())
        {
            result.code = "1";
            result.msg = "无规则明细,保存失败!";
            return result;
        }

        if (parameter.Rules.Any(x => x.IType == "4") && !parameter.Rules.Any(x => x.IType == "3"))
        {
            result.code = "1";
            result.msg = "设置采购单项次需要同时设置采购单单号！";
            return result;
        }

        if (parameter.Rules.Any(x => x.IType == "7") && !parameter.Rules.Any(x => x.IType == "6"))
        {
            result.code = "1";
            result.msg = "设置销售单项次需要同时设置销售单单号！";
            return result;
        }
        if (parameter.Rules.Where(x => x.IType == "10").Count() > 10) {
            result.code = "1";
            result.msg = "最多只能设置10个任意值类型的项！";
            return result;
        }

        #endregion

        DBContext context = new DBContext();
        using (var modContext = context)
        {
            using (var dbContextTransaction = modContext.Database.BeginTransaction())
            {
                try
                {
                    var modRule = modContext.BASE_BARCODE_RULE.Where(x => x.ID == parameter.id).FirstOrDefault();
                    if (modRule != null)
                    {
                        if (modRule.STATUS == "0")
                        {
                            //已有的规则明细需删除
                            var ruleItemList = modContext.BASE_BARCODE_RULE_D.Where(x => x.RULEID == modRule.ID).ToList();
                            if (ruleItemList.Any())
                            {
                                foreach (var item in ruleItemList)
                                {
                                    modContext.BASE_BARCODE_RULE_D.Remove(item);
                                }
                                modContext.SaveChanges();
                            }

                            foreach (var item in parameter.Rules)
                            {
                                BASE_BARCODE_RULE_D modRule_d = new BASE_BARCODE_RULE_D();
                                modRule_d.ID = Guid.NewGuid().ToString();
                                modRule_d.RULEID = modRule.ID;
                                modRule_d.SNO = item.Sno;
                                modRule_d.NAME = item.Name;
                                modRule_d.ILENGTH = item.Len;
                                modRule_d.IFROM = item.IFrom;
                                modRule_d.ITO = item.ITo;
                                modRule_d.BARCODETYPEID = item.IType;
                                modRule_d.FORMATID = item.IFormat;
                                modRule_d.DEFAULTVALUE = item.IDefault;
                                modRule_d.MANDATORY = item.IMandatory ? "1" : "0";
                                modRule_d.CREATEUSER = item.CreateUser;
                                modRule_d.CREATEDATE = DateTime.Now;
                                modRule_d.REMARK = item.Remark;
                                modContext.BASE_BARCODE_RULE_D.Add(modRule_d);
                            }
                            modContext.SaveChanges();
                            dbContextTransaction.Commit();

                        }
                        else
                        {
                            result.code = "1";
                            result.msg = "当前规则的状态不允许保存!";
                        }
                    }
                    else
                    {
                        result.code = "1";
                        result.msg = "信息异常,保存失败!";
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    result.code = "1";
                    result.msg = "保存失败，错误信息：" + ex.Message;
                }
            }
        }

        return result;
    }

    [WebMethod]
    public WebServiceResult LoadCodeRules(string ruleid,string printid)
    {
        WebServiceResult result = new WebServiceResult("0", "");
        DBContext context = new DBContext();
        //拼接数据列表
        StringBuilder nodeHtml = new StringBuilder();
        using (var modContext = context)
        {
            try
            {
                nodeHtml.Append("<tr>");
                nodeHtml.Append("<td colspan=\"2\" style=\"padding:15px 0px;\">");
                nodeHtml.Append("<table class=\"InputTable ruleItemTable\" style=\"width: 100%\" cellspacing=\"1\" cellpadding=\"1\" border=\"0\">");
                nodeHtml.Append("<thead><tr>");
                nodeHtml.Append("<th style=\"width:150px;\">名称</th>");
                nodeHtml.Append("<th style=\"width:200px;\">值</th>");
                nodeHtml.Append("<th style=\"width:50px;\">长度</th>");
                nodeHtml.Append("<th style=\"width:70px;\">位数</th>");
                nodeHtml.Append("<th style=\"width:60px;\">格式</th>");
                nodeHtml.Append("<th style=\"width:150px;\">设定值</th>");
                nodeHtml.Append("<th style=\"width:auto;\">说明</th>");
                nodeHtml.Append("</tr></thead>");
                nodeHtml.Append("<tbody class=\"tbRuleItems\">");
                //已有的规则明细需删除
                var ruleItemList = modContext.BASE_BARCODE_RULE_D.Where(x => x.RULEID == ruleid).OrderBy(x => x.SNO).ToList();
                if (ruleItemList.Any())
                {
                    foreach (var item in ruleItemList)
                    {
                        string isNeedStr = item.MANDATORY == "1" ? "<span class=\"requiredSign\">*</span>" : "";
                        nodeHtml.Append("<tr data-isneed=\"" + item.MANDATORY + "\" data-len=\"" + item.ILENGTH + "\" data-fmat=\"" + item.FORMATID + "\" data-defval=\"" + item.DEFAULTVALUE + "\" data-ruletype=\"" + item.BARCODETYPEID + "\" data-seq=\"" + item.SNO.ToString() + "\" data-name =\"" + item.NAME + "\" data-source=\"0\">");
                        nodeHtml.Append("<td class=\"txtItemTitle\">" + isNeedStr + item.NAME + "</td>");
                        string initialVal = string.Empty;
                        string disableStr = string.Empty;
                        if (item.BARCODETYPEID == "2")
                        {
                            if (item.FORMATID == "YYMMDD")
                            {
                                initialVal = DateTime.Now.ToString("yyMMdd");
                            }
                            else if (item.FORMATID == "MMDDYY")
                            {
                                initialVal = DateTime.Now.ToString("MMddyy");
                            }
                            else if (item.FORMATID == "YYWW")
                            {
                                initialVal = DateTime.Now.ToString("yy") + GetWeekOfYear(DateTime.Now).ToString().PadLeft(2, '0');
                            }
                        }
                        else if (item.BARCODETYPEID == "11")
                        {
                            disableStr = "disabled=\"disabled\"";
                            if (item.FORMATID == "0-9")
                            {
                                int fistNo = 1;
                                initialVal = fistNo.ToString().PadLeft(item.ILENGTH.Value, '0');
                            }
                            else if (item.FORMATID == "0-Z")
                            {
                                int fistNo = 1;
                                initialVal = fistNo.ToString().PadLeft(item.ILENGTH.Value, '0');
                            }
                        }
                        nodeHtml.Append("<td class=\"td-left\"><input id=\"txtValue\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width: 150px;\" " + disableStr + " autocomplete=\"off\" value=\"" + initialVal + "\" />");
                        nodeHtml.Append("<ul class=\"weul\"></ul>");
                        nodeHtml.Append("</td>");
                        nodeHtml.Append("<td class=\"td-left\">" + item.ILENGTH.ToString() + "</td>");
                        nodeHtml.Append("<td class=\"td-left\">");
                        nodeHtml.Append(item.IFROM.ToString());
                        nodeHtml.Append(" - ");
                        nodeHtml.Append(item.ITO.ToString());
                        nodeHtml.Append("</td>");
                        //格式
                        nodeHtml.Append("<td class=\"tdFormat\">");
                        nodeHtml.Append(item.FORMATID);
                        nodeHtml.Append("</td>");
                        nodeHtml.Append("<td>" + item.DEFAULTVALUE + "</td>");
                        nodeHtml.Append("<td>" + item.REMARK + "</td>");
                        nodeHtml.Append("</tr>");
                    }

                    if (!string.IsNullOrEmpty(printid))
                    {
                        List<BASE_BARCODE_PRINT_D> printItems = context.BASE_BARCODE_PRINT_D.Where(x => x.PrintId == printid).OrderBy(x => x.PrintSeq).ToList();
                        if (printItems.Any())
                        {
                            foreach (var iPrint in printItems)
                            {
                                string isNeedStr = iPrint.IsNeedVal == "1" ? "<span class=\"requiredSign\">*</span>" : "";
                                nodeHtml.Append("<tr data-isneed=\"" + iPrint.IsNeedVal + "\" data-len=\"0\" data-fmat=\"\" data-defval=\"\" data-ruletype=\"" + iPrint.PrintItemType + "\" data-seq=\"" + iPrint.PrintSeq.ToString() + "\" data-name =\"" + iPrint.PrintItemName + "\" data-source=\"1\">");
                                nodeHtml.Append("<td class=\"txtItemTitle\">" + isNeedStr + iPrint.PrintItemName + "</td>");
                                string initialVal = string.Empty;
                                string disableStr = string.Empty;
                                nodeHtml.Append("<td class=\"td-left\"><input id=\"txtValue\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width: 150px;\" " + disableStr + " autocomplete=\"off\" value=\"" + initialVal + "\" />");
                                nodeHtml.Append("<ul class=\"weul\"></ul>");
                                nodeHtml.Append("</td>");
                                nodeHtml.Append("<td class=\"td-left\"></td>");
                                nodeHtml.Append("<td class=\"td-left\">");
                                nodeHtml.Append("</td>");
                                //格式
                                nodeHtml.Append("<td class=\"tdFormat\">");
                                if (iPrint.PrintItemType == "0")
                                {
                                    nodeHtml.Append("字符");
                                }
                                else
                                {
                                    nodeHtml.Append("数值");
                                }
                                nodeHtml.Append("</td>");
                                nodeHtml.Append("<td></td>");
                                nodeHtml.Append("<td>" + iPrint.PrintRemark + "</td>");
                                nodeHtml.Append("</tr>");
                            }
                        }
                    }
                }
                else
                {
                    nodeHtml.Append("<tr><td colspan=\"7\">暂无规则明细</td></tr>");
                }
                nodeHtml.Append("</tbody>");
                nodeHtml.Append("</table>");
                nodeHtml.Append("</td></tr>");

                result.msg = nodeHtml.ToString();
            }
            catch (Exception ex)
            {
                result.code = "1";
                result.msg = "保存失败，错误信息：" + ex.Message;
            }
        }
        return result;
    }

    [WebMethod]
    public WebServiceResult CheckRuleCount(int RCount,string BarCodeType)
    {
        WebServiceResult result = new WebServiceResult("0", "检查通过！");

        DBContext context = new DBContext();
        int enabledCount = context.BASE_BARCODE_RULE.Where(x => x.STATUS == "1" && x.RULETYPE.Equals(BarCodeType)).Count();
        if ((enabledCount + RCount) > 5) {
            result.code = "1";
            result.msg = "启用数量太多，会影响系统运行效率!";
            return result;
        }
        return result;
    }

    [WebMethod]
    public WebServiceResult SaveCodeNo(CodeNoParameter parameter)
    {
        WebServiceResult result = new WebServiceResult("0", "保存成功！");
        if (parameter == null)
        {
            result.code = "1";
            result.msg = "参数错误，保存失败！";
            return result;
        }

        if (string.IsNullOrEmpty(parameter.RuleId) || parameter.RuleVales == null || !parameter.RuleVales.Any())
        {
            result.code = "1";
            result.msg = "参数错误，保存失败！";
            return result;
        }


        DBContext context = new DBContext();
        using (var modContext = context)
        {
            #region 检查相关信息
            var modRule = modContext.BASE_BARCODE_RULE.Where(x => x.ID == parameter.RuleId).FirstOrDefault();
            if (modRule == null)
            {
                result.code = "1";
                result.msg = "未找到该条码规则，保存失败！";
                return result;
            }

            if (modRule.STATUS != "1")
            {
                result.code = "1";
                result.msg = "该规则已作废，保存失败！";
                return result;
            }

            //排序
            var RuleValues = parameter.RuleVales.Where(x => x.Source == 0).OrderBy(x => x.Sno).ToList();
            if (RuleValues == null || !RuleValues.Any())
            {
                result.code = "1";
                result.msg = "参数错误，保存失败！";
                return result;
            }

            var modRuleDList = modContext.BASE_BARCODE_RULE_D.Where(x => x.RULEID == modRule.ID).OrderBy(x => x.SNO).ToList();
            if (modRuleDList == null || !modRuleDList.Any() || modRuleDList.Count != RuleValues.Count)
            {
                result.code = "1";
                result.msg = "参数项与规则明细项不匹配，保存失败！";
                return result;
            }

            var printItems = new List<CodeNoItem>();
            var printMod = new BASE_BARCODE_PRINT();
            var printList = new List<BASE_BARCODE_PRINT_D>();
            if (!string.IsNullOrEmpty(parameter.PrintId))
            {
                printItems = parameter.RuleVales.Where(x => x.Source == 1).OrderBy(x => x.Sno).ToList();
                printMod = modContext.BASE_BARCODE_PRINT.Where(x => x.Id == parameter.PrintId).FirstOrDefault();
                printList = modContext.BASE_BARCODE_PRINT_D.Where(x => x.PrintId == parameter.PrintId).OrderBy(x => x.PrintSeq).ToList();

                if (printMod == null || printItems == null || printList == null || !printItems.Any() || !printList.Any() || printItems.Count != printList.Count) {
                    result.code = "1";
                    result.msg = "参数项与打印明细项不匹配，保存失败！";
                    return result;
                }

                if (printMod.Cstatus != "1")
                {
                    result.code = "1";
                    result.msg = "该打印规则已作废，保存失败！";
                    return result;
                }
            }

            string SNCode = string.Empty;//条码
            string datacode = string.Empty;//日期
            string vendor = string.Empty;//供应商编码
            string vendorName = string.Empty;//供应商名称
            string partNo = string.Empty;//物料编码
            string partName = string.Empty;//物料名称
            string po_number = string.Empty;
            string po_lineid = string.Empty;//采购单项次
            string so_number = string.Empty;//销售单号
            string so_lineid = string.Empty;//销售单项次
            string clientid = string.Empty;//客户编码
            string clientname = string.Empty;//客户名称
            decimal quantity = 0;

            int ArbitrarySeq = 1;
            string ArbitraryVal1 = string.Empty;//任意值1
            string ArbitraryVal2 = string.Empty;//任意值2
            string ArbitraryVal3 = string.Empty;//任意值3
            string ArbitraryVal4 = string.Empty;//任意值4
            string ArbitraryVal5 = string.Empty;//任意值5
            string ArbitraryVal6 = string.Empty;//任意值6
            string ArbitraryVal7 = string.Empty;//任意值7
            string ArbitraryVal8 = string.Empty;//任意值8
            string ArbitraryVal9 = string.Empty;//任意值9
            string ArbitraryVal10 = string.Empty;//任意值10

            #region 参数校验
            

            foreach (var item in RuleValues)
            {
                var currentRuleItem = modRuleDList.Where(x => x.SNO == item.Sno).FirstOrDefault();
                if (currentRuleItem == null)
                {
                    result.code = "1";
                    result.msg = "未匹配到规则明细项，保存失败！";
                    return result;
                }

                switch (currentRuleItem.BARCODETYPEID)
                {
                    case "1"://料号
                        //填了值，不管是不是必要字段都校验
                        if (!string.IsNullOrEmpty(item.RuleVal))
                        {
                            var modPart = modContext.BASE_PART.Where(x => x.cpartnumber == item.RuleVal).FirstOrDefault();
                            if (modPart == null)
                            {
                                result.code = "1";
                                result.msg = "料号不存在！";
                                return result;
                            }
                            if (modPart.cstatus != "0")
                            {
                                result.code = "1";
                                result.msg = "物料状态不正确！";
                                return result;
                            }
                            partNo = item.RuleVal;
                            partName = modPart.cpartname;
                        }
                        SNCode += string.Format("{0," + currentRuleItem.ILENGTH + "}", item.RuleVal);
                        break;
                    case "2"://生产日期
                        //TODO:是否再校验
                        datacode = item.RuleVal;
                        SNCode += string.Format("{0," + currentRuleItem.ILENGTH + "}", item.RuleVal);
                        break;
                    case "3"://采购单单号
                        //填了值，不管是不是必要字段都校验
                        if (!string.IsNullOrEmpty(item.RuleVal))
                        {
                            if (!modContext.INPO.Any(x => x.pono == item.RuleVal && x.status != 3))
                            {
                                result.code = "1";
                                result.msg = "采购单不存在！";
                                return result;
                            }
                            po_number = item.RuleVal;
                        }
                        SNCode += string.Format("{0," + currentRuleItem.ILENGTH + "}", item.RuleVal);
                        break;
                    case "4"://采购单项次
                        //填了值，不管是不是必要字段都校验
                        if (!string.IsNullOrEmpty(item.RuleVal))
                        {
                            int lineId = 0;
                            if (!int.TryParse(item.RuleVal, out lineId))
                            {
                                result.code = "1";
                                result.msg = currentRuleItem.NAME + "格式不正确！";
                                return result;
                            }
                            po_lineid = item.RuleVal;
                            //TODO:是否校验项次存不存在

                        }
                        SNCode += string.Format("{0," + currentRuleItem.ILENGTH + "}", item.RuleVal);
                        break;
                    case "5"://供应商代码
                        //填了值，不管是不是必要字段都校验
                        if (!string.IsNullOrEmpty(item.RuleVal))
                        {
                            var modVendor = modContext.BASE_VENDOR.Where(x => x.cvendorid == item.RuleVal).FirstOrDefault();
                            if (modVendor == null)
                            {
                                result.code = "1";
                                result.msg = "供应商不存在！";
                                return result;
                            }
                            if (modVendor.cstatus != "0")
                            {
                                result.code = "1";
                                result.msg = "供应商状态不正确！";
                                return result;
                            }
                            vendor = item.RuleVal;
                            vendorName = modVendor.cvendor;
                        }
                        SNCode += string.Format("{0," + currentRuleItem.ILENGTH + "}", item.RuleVal);
                        break;
                    case "6"://销售单单号
                        //填了值，不管是不是必要字段都校验
                        if (!string.IsNullOrEmpty(item.RuleVal))
                        {
                            if (!modContext.OUTORDER.Any(x => x.OrderNo == item.RuleVal && x.Status != 3))
                            {
                                result.code = "1";
                                result.msg = "销售单不存在！";
                                return result;
                            }
                            so_number = item.RuleVal;
                        }
                        SNCode += string.Format("{0," + currentRuleItem.ILENGTH + "}", item.RuleVal);
                        break;
                    case "7"://销售单项次
                        //填了值，不管是不是必要字段都校验
                        if (!string.IsNullOrEmpty(item.RuleVal))
                        {
                            int lineId = 0;
                            if (!int.TryParse(item.RuleVal, out lineId))
                            {
                                result.code = "1";
                                result.msg = currentRuleItem.NAME + "格式不正确！";
                                return result;
                            }
                            so_lineid = item.RuleVal;
                            //TODO:是否校验项次存不存在

                        }
                        SNCode += string.Format("{0," + currentRuleItem.ILENGTH + "}", item.RuleVal);
                        break;
                    case "8"://客户代码
                        //填了值，不管是不是必要字段都校验
                        if (!string.IsNullOrEmpty(item.RuleVal))
                        {
                            var modClient = modContext.BASE_CLIENT.Where(x => x.cclientid == item.RuleVal).FirstOrDefault();
                            if (modClient == null)
                            {
                                result.code = "1";
                                result.msg = "客户不存在！";
                                return result;
                            }
                            if (modClient.cstatus != "0")
                            {
                                result.code = "1";
                                result.msg = "客户状态不正确！";
                                return result;
                            }
                            clientid = item.RuleVal;
                            clientname = modClient.cclientname;
                        }
                        SNCode += string.Format("{0," + currentRuleItem.ILENGTH + "}", item.RuleVal);
                        break;
                    case "9"://数量
                        //填了值，不管是不是必要字段都校验
                        if (!string.IsNullOrEmpty(item.RuleVal))
                        {
                            decimal checkDec = 0;
                            if (!decimal.TryParse(item.RuleVal, out checkDec))
                            {
                                result.code = "1";
                                result.msg = currentRuleItem.NAME + "格式不正确！";
                                return result;
                            }
                            else {
                                string txtInterger = string.Empty;
                                string txtDecimal = string.Empty;
                                if (!string.IsNullOrEmpty(currentRuleItem.FORMATID))
                                {
                                    txtInterger = currentRuleItem.FORMATID.Split(',')[0];
                                    txtDecimal = currentRuleItem.FORMATID.Split(',')[1];
                                }
                                string decVal = checkDec.ToString();
                                if (decVal.Contains("."))
                                { //包含小数
                                    int decLength = decVal.Length - decVal.IndexOf('.') - 1;
                                    if (decLength > Convert.ToInt32(txtDecimal))
                                    {
                                        result.code = "1";
                                        result.msg = currentRuleItem.NAME + "格式不正确！";
                                        return result;
                                    }
                                    int intlength = decVal.Length - decLength - 1;
                                    if (intlength > Convert.ToInt32(txtInterger))
                                    {
                                        result.code = "1";
                                        result.msg = currentRuleItem.NAME + "格式不正确！";
                                        return result;
                                    }
                                }
                                else
                                {
                                    if (decVal.Length > Convert.ToInt32(txtInterger))
                                    {
                                        result.code = "1";
                                        result.msg = currentRuleItem.NAME + "格式不正确！";
                                        return result;
                                    }
                                }

                                quantity = checkDec;
                            }
                        }
                        SNCode += string.Format("{0," + currentRuleItem.ILENGTH + "}", item.RuleVal);
                        break;
                    case "10"://任意值
                        //填了值，不管是不是必要字段都校验
                        if (!string.IsNullOrEmpty(item.RuleVal))
                        {
                            //预留值非空
                            if (!string.IsNullOrEmpty(currentRuleItem.DEFAULTVALUE))
                            {
                                ArrayList b = new ArrayList(currentRuleItem.DEFAULTVALUE.Split(','));
                                if (b.IndexOf(item.RuleVal) < 0)
                                {
                                    result.code = "1";
                                    result.msg = "任意值不在设定值范围内！";
                                    return result;
                                }
                            }
                        }

                        switch (ArbitrarySeq) { 
                            case 1:
                                ArbitraryVal1 = item.RuleVal;
                                break;
                            case 2:
                                ArbitraryVal2 = item.RuleVal;
                                break;
                            case 3:
                                ArbitraryVal3 = item.RuleVal;
                                break;
                            case 4:
                                ArbitraryVal4 = item.RuleVal;
                                break;
                            case 5:
                                ArbitraryVal5 = item.RuleVal;
                                break;
                            case 6:
                                ArbitraryVal6 = item.RuleVal;
                                break;
                            case 7:
                                ArbitraryVal7 = item.RuleVal;
                                break;
                            case 8:
                                ArbitraryVal8 = item.RuleVal;
                                break;
                            case 9:
                                ArbitraryVal9 = item.RuleVal;
                                break;
                            case 10:
                                ArbitraryVal10 = item.RuleVal;
                                break;
                        
                        }
                        ArbitrarySeq++;
                        SNCode += string.Format("{0," + currentRuleItem.ILENGTH + "}", item.RuleVal);
                        break;
                    case "11"://流水号

                        SNCode += string.Format("{0," + currentRuleItem.ILENGTH + "}", item.RuleVal);
                        break;
                    default:
                        break;
                }
            }

            //判断供应商是否在对应采购单中
            if (!string.IsNullOrEmpty(po_number) && !string.IsNullOrEmpty(vendor))
            {
                if (!modContext.INPO.Any(x => x.pono == po_number && x.vendorid == vendor && x.status != 3))
                {
                    result.code = "1";
                    result.msg = "供应商与采购单不匹配！";
                    return result;
                }
            }
            //判断客户是否在销售单中
            if (!string.IsNullOrEmpty(so_number) && !string.IsNullOrEmpty(clientid))
            {
                if (!modContext.OUTORDER.Any(x => x.OrderNo == so_number && x.CustomId == clientid && x.Status != 3))
                {
                    result.code = "1";
                    result.msg = "客户与销售单不匹配！";
                    return result;
                }
            }
            #endregion

            #endregion

            #region 校验条码长度
            if (SNCode.Length != modRule.RULELEN)
            {
                result.code = "1";
                result.msg = "生成条码异常，保存失败！";
                return result;
            }

            #endregion

            #region 保存数据
            using (var dbContextTransaction = modContext.Database.BeginTransaction())
            {
                try
                {
                    if (string.IsNullOrEmpty(parameter.SNId) || parameter.SNId == Guid.Empty.ToString())
                    {
                        var liuShuiItem = modRuleDList.Where(x => x.BARCODETYPEID == "11").FirstOrDefault();
                        if (liuShuiItem != null)//如果规则中有流水号字段,需要匹配库中已存在的相同信息的条码,取出最大的
                        {
                            int iStart = liuShuiItem.IFROM.Value - 1;
                            int iEnd = liuShuiItem.ITO.Value - 1;
                            int rightLength = modRule.RULELEN.Value - liuShuiItem.ITO.Value;
                            string leftStr = SNCode.Substring(0, iStart);//流水号左边字符串
                            string liushuiStr = SNCode.Substring(iStart, liuShuiItem.ILENGTH.Value);//流水号字符串
                            string rightStr = SNCode.Substring(iEnd, rightLength);//流水号右边字符串

                            var maxSNMod = modContext.BASE_BAR_SN.Where(x => modContext.BASE_BARCODE_RULE.Any(y => y.ID == x.CodeRuleId && y.RULELEN == modRule.RULELEN) && x.sn_code.StartsWith(leftStr) && x.sn_code.EndsWith(rightStr)).OrderByDescending(x => x.sn_code).FirstOrDefault();
                            if (maxSNMod != null)
                            {
                                var msxSN = maxSNMod.sn_code.Substring(iStart, liuShuiItem.ILENGTH.Value);
                                if (liuShuiItem.FORMATID == "0-9")
                                {
                                    int newLiushui = Convert.ToInt32(msxSN.TrimStart('0')) + 1;
                                    SNCode = leftStr + newLiushui.ToString().PadLeft(liuShuiItem.ILENGTH.Value, '0') + rightStr;
                                }
                                else if (liuShuiItem.FORMATID == "0-Z")
                                {
                                    SNCode = leftStr + getSNFlow(msxSN, liuShuiItem.ILENGTH.Value) + rightStr;
                                }
                            }
                            else
                            {
                                if (liuShuiItem.FORMATID == "0-9")
                                {
                                    int fistNo = 1;
                                    SNCode = leftStr + fistNo.ToString().PadLeft(liuShuiItem.ILENGTH.Value, '0') + rightStr;
                                }
                                else if (liuShuiItem.FORMATID == "0-Z")
                                {
                                    int fistNo = 1;
                                    SNCode = leftStr + fistNo.ToString().PadLeft(liuShuiItem.ILENGTH.Value, '0') + rightStr;
                                }
                            }
                        }
                        else
                        {
                            var modSnaa = modContext.BASE_BAR_SN.Where(x => x.sn_code.Equals(SNCode)).FirstOrDefault();
                            if (modSnaa != null)
                            {
                                result.code = "1";
                                result.msg = "已存在相同的条码:" + SNCode.ToString();
                                return result;
                            }
                        }

                        BASE_BAR_SN modSn = new BASE_BAR_SN();
                        modSn.id = Guid.NewGuid().ToString();
                        modSn.sn_code = SNCode;
                        modSn.datecode = datacode;
                        modSn.vendor = vendor;
                        modSn.vendor_name = vendorName;
                        modSn.cinvcode = partNo;
                        modSn.cinvcode_name = partName;
                        modSn.sn_type = "1";
                        modSn.create_owner = parameter.CreateUser;
                        modSn.create_time = DateTime.Now;
                        modSn.po_number = po_number;
                        modSn.batch_number = "0";
                        modSn.batch_qty = 0;
                        modSn.bar_type = "1";
                        modSn.boxNum = 0;
                        modSn.quantity = quantity;
                        modSn.BarCodeType = modRule.RULETYPE;

                        #region 新增字段
                        modSn.Po_LineId = po_lineid;
                        modSn.So_Number = so_number;
                        modSn.So_LineId = so_lineid;
                        modSn.ClientCode = clientid;
                        modSn.ClinetName = clientname;
                        modSn.ArbitraryVal1 = ArbitraryVal1;
                        modSn.ArbitraryVal2 = ArbitraryVal2;
                        modSn.ArbitraryVal3 = ArbitraryVal3;
                        modSn.ArbitraryVal4 = ArbitraryVal4;
                        modSn.ArbitraryVal5 = ArbitraryVal5;
                        modSn.ArbitraryVal6 = ArbitraryVal6;
                        modSn.ArbitraryVal7 = ArbitraryVal7;
                        modSn.ArbitraryVal8 = ArbitraryVal8;
                        modSn.ArbitraryVal9 = ArbitraryVal9;
                        modSn.ArbitraryVal10 = ArbitraryVal10;
                        #endregion

                        modSn.CodeRuleId = modRule.ID;
                        if (!string.IsNullOrEmpty(parameter.PrintId))
                        {
                            modSn.PrintRuleId = printMod.Id;
                            foreach (var subPrint in printItems)
                            {
                                switch (subPrint.Sno)
                                {
                                    case 1:
                                        modSn.PrintVal1 = subPrint.RuleVal;
                                        break;
                                    case 2:
                                        modSn.PrintVal2 = subPrint.RuleVal;
                                        break;
                                    case 3:
                                        modSn.PrintVal3 = subPrint.RuleVal;
                                        break;
                                    case 4:
                                        modSn.PrintVal4 = subPrint.RuleVal;
                                        break;
                                    case 5:
                                        modSn.PrintVal5 = subPrint.RuleVal;
                                        break;
                                    case 6:
                                        modSn.PrintVal6 = subPrint.RuleVal;
                                        break;
                                    case 7:
                                        modSn.PrintVal7 = subPrint.RuleVal;
                                        break;
                                    case 8:
                                        modSn.PrintVal8 = subPrint.RuleVal;
                                        break;
                                    case 9:
                                        modSn.PrintVal9 = subPrint.RuleVal;
                                        break;
                                    case 10:
                                        modSn.PrintVal10 = subPrint.RuleVal;
                                        break;
                                }

                            }
                        }

                        modContext.BASE_BAR_SN.Add(modSn);
                        modContext.SaveChanges();
                    }
                    else
                    {
                        BASE_BAR_SN modSn = modContext.BASE_BAR_SN.Where(x => x.id == parameter.SNId).FirstOrDefault();
                        modSn.sn_code = SNCode;
                        modSn.datecode = datacode;
                        modSn.vendor = vendor;
                        modSn.vendor_name = vendorName;
                        modSn.cinvcode = partNo;
                        modSn.cinvcode_name = modRule.ID;
                        modSn.sn_type = "1";
                        modSn.last_upd_owner = parameter.CreateUser;
                        modSn.last_upd_time = DateTime.Now;
                        modSn.po_number = po_number;
                        modSn.batch_number = "0";
                        modSn.batch_qty = 0;
                        modSn.bar_type = "1";
                        modSn.boxNum = 0;
                        modSn.quantity = quantity;
                        modContext.Entry(modSn).State = System.Data.Entity.EntityState.Modified;
                        modContext.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    result.code = "1";
                    result.msg = "保存失败，错误信息：" + ex.Message;
                }
            }
            #endregion
        }

        return result;
    }

    [WebMethod]
    public WebServiceResult GetSnDate(string code, string codetype)
    {
        WebServiceResult result = new WebServiceResult("0", "获取成功！");
        if (string.IsNullOrEmpty(codetype))
        {
            result.code = "1";
            result.msg = "参数错误，保存失败！";
            return result;
        }

        try
        {
            //拼接数据列表
            StringBuilder nodeHtml = new StringBuilder();
            DBContext context = new DBContext();
            using (var modContext = context)
            {
                if (codetype == "1")
                {
                    var partList = new List<BASE_PART>();
                    if (string.IsNullOrEmpty(code))
                    {
                        partList = modContext.BASE_PART.Where(x=>x.cstatus == "0").OrderBy(x => x.cpartnumber).Take(15).ToList();
                    }
                    else
                    {
                        partList = modContext.BASE_PART.Where(x => x.cstatus == "0" && ( x.cpartnumber.Contains(code) || x.cpartname.Contains(code))).OrderBy(x => x.cpartnumber).Take(15).ToList();
                    }

                    if (partList.Any())
                    {
                        foreach (var item in partList)
                        {
                            nodeHtml.Append("<li data-number=\"" + item.cpartnumber + "\">【" + item.cpartnumber + "】" + item.cpartname + "</li>");
                        }
                    }
                    result.msg = nodeHtml.ToString();
                    return result;
                }
                else if (codetype == "5")
                { //供应商
                    var vendorList = new List<BASE_VENDOR>();
                    if (string.IsNullOrEmpty(code))
                    {
                        vendorList = modContext.BASE_VENDOR.Where(x => x.cstatus == "0").OrderBy(x => x.cvendorid).Take(15).ToList();
                    }
                    else
                    {
                        vendorList = modContext.BASE_VENDOR.Where(x => x.cstatus == "0" && ( x.cvendorid.Contains(code) || x.cvendor.Contains(code))).OrderBy(x => x.cvendorid).Take(15).ToList();
                    }

                    if (vendorList.Any())
                    {
                        foreach (var item in vendorList)
                        {
                            nodeHtml.Append("<li data-number=\"" + item.cvendorid + "\">【" + item.cvendorid + "】" + item.cvendor + "</li>");
                        }
                    }
                    result.msg = nodeHtml.ToString();
                    return result;
                }
                else if (codetype == "8")
                {
                    var clientList = new List<BASE_CLIENT>();
                    if (string.IsNullOrEmpty(code))
                    {
                        clientList = modContext.BASE_CLIENT.Where(x => x.cstatus == "0").OrderBy(x => x.cclientid).Take(15).ToList();
                    }
                    else
                    {
                        clientList = modContext.BASE_CLIENT.Where(x => x.cstatus == "0" && (x.cclientid.Contains(code) || x.cclientname.Contains(code))).OrderBy(x => x.cclientid).Take(15).ToList();
                    }

                    if (clientList.Any())
                    {
                        foreach (var item in clientList)
                        {
                            nodeHtml.Append("<li data-number=\"" + item.cclientid + "\">【" + item.cclientid + "】" + item.cclientname + "</li>");
                        }
                    }
                    result.msg = nodeHtml.ToString();
                    return result;
                }
                else
                {
                    result.code = "1";
                    result.msg = "不支持的类型";
                    return result;
                }
            }
        }
        catch (Exception ex)
        {
            result.code = "1";
            result.msg = "发生错误：" + ex.Message;
            return result;
        }
    }


    [WebMethod]
    public WebServiceResult CheckSnDate(string code, string codetype)
    {
        WebServiceResult result = new WebServiceResult("0", "检查通过！");
        if (string.IsNullOrEmpty(codetype))
        {
            result.code = "1";
            result.msg = "参数错误，检查失败！";
            return result;
        }

        try
        {
            //拼接数据列表
            DBContext context = new DBContext();
            using (var modContext = context)
            {
                if (codetype == "1")
                {
                    if (modContext.BASE_PART.Any(x => x.cpartnumber == code && x.cstatus == "0"))
                    {
                        result.code = "0";
                        result.msg = "校验通过！";
                        return result;
                    }
                    else {
                        result.code = "1";
                        result.msg = "校验失败！";
                        return result;
                    }
                }
                else if (codetype == "3")
                {
                    if (modContext.INPO.Any(x => x.pono == code && x.status != 3))//排除已撤消状态
                    {
                        result.code = "0";
                        result.msg = "校验通过！";
                        return result;
                    }
                    else
                    {
                        result.code = "1";
                        result.msg = "校验失败！";
                        return result;
                    }
                }
                else if (codetype == "5")
                { //供应商
                    if (modContext.BASE_VENDOR.Any(x => x.cvendorid == code && x.cstatus == "0"))
                    {
                        result.code = "0";
                        result.msg = "校验通过！";
                        return result;
                    }
                    else
                    {
                        result.code = "1";
                        result.msg = "校验失败！";
                        return result;
                    }
                }
                else if (codetype == "6")
                { //销售单号
                    if (modContext.OUTORDER.Any(x => x.OrderNo == code && x.Status != 3))//排除已撤消的
                    {
                        result.code = "0";
                        result.msg = "校验通过！";
                        return result;
                    }
                    else
                    {
                        result.code = "1";
                        result.msg = "校验失败！";
                        return result;
                    }
                }
                else if (codetype == "8")
                {
                    if (modContext.BASE_CLIENT.Any(x => x.cclientid == code && x.cstatus == "0"))
                    {
                        result.code = "0";
                        result.msg = "校验通过！";
                        return result;
                    }
                    else
                    {
                        result.code = "1";
                        result.msg = "校验失败！";
                        return result;
                    }
                }
                else
                {
                    result.code = "1";
                    result.msg = "不支持的类型";
                    return result;
                }
            }
        }
        catch (Exception ex)
        {
            result.code = "1";
            result.msg = "发生错误：" + ex.Message;
            return result;
        }
    }

    [WebMethod]
    public WebServiceResult ExportToExcel(ExportToExcelParameter parameter)
    {
        DataTable dtExport = new DataTable();

        #region 创建列模版
        dtExport.Columns.Add("SNCODE", typeof(string)); //SN
        dtExport.Columns.Add("DATECODE", typeof(string)); //DATECODE
        dtExport.Columns.Add("CINVCODE", typeof(string)); //物料编码
        dtExport.Columns.Add("CINVNAME", typeof(string)); //物料名称
        dtExport.Columns.Add("PONUMBER", typeof(string)); //采购单单号
        dtExport.Columns.Add("POLINEID", typeof(string)); //采购单项次
        dtExport.Columns.Add("VENDOR", typeof(string)); //供应商编号
        dtExport.Columns.Add("VENDORNAME", typeof(string)); //供应商名称
        dtExport.Columns.Add("SONUMBER", typeof(string)); //销售单单号
        dtExport.Columns.Add("SOLINEID", typeof(string)); //销售单项次
        dtExport.Columns.Add("CLIENT", typeof(string)); //客户编号
        dtExport.Columns.Add("CLIENTNAME", typeof(string)); //客户名称
        dtExport.Columns.Add("QUANTITY", typeof(string)); //数量
        dtExport.Columns.Add("ArbitraryVal1", typeof(string)); //任意值1
        dtExport.Columns.Add("ArbitraryVal2", typeof(string)); //任意值2
        dtExport.Columns.Add("ArbitraryVal3", typeof(string)); //任意值3
        dtExport.Columns.Add("ArbitraryVal4", typeof(string)); //任意值4
        dtExport.Columns.Add("ArbitraryVal5", typeof(string)); //任意值5
        dtExport.Columns.Add("ArbitraryVal6", typeof(string)); //任意值6
        dtExport.Columns.Add("ArbitraryVal7", typeof(string)); //任意值7
        dtExport.Columns.Add("ArbitraryVal8", typeof(string)); //任意值8
        dtExport.Columns.Add("ArbitraryVal9", typeof(string)); //任意值9
        dtExport.Columns.Add("ArbitraryVal10", typeof(string)); //任意值10
        dtExport.Columns.Add("PrintVal1", typeof(string)); //附加打印信息1
        dtExport.Columns.Add("PrintVal2", typeof(string)); //附加打印信息2
        dtExport.Columns.Add("PrintVal3", typeof(string)); //附加打印信息3
        dtExport.Columns.Add("PrintVal4", typeof(string)); //附加打印信息4
        dtExport.Columns.Add("PrintVal5", typeof(string)); //附加打印信息5
        dtExport.Columns.Add("PrintVal6", typeof(string)); //附加打印信息6
        dtExport.Columns.Add("PrintVal7", typeof(string)); //附加打印信息7
        dtExport.Columns.Add("PrintVal8", typeof(string)); //附加打印信息8
        dtExport.Columns.Add("PrintVal9", typeof(string)); //附加打印信息9
        dtExport.Columns.Add("PrintVal10", typeof(string)); //附加打印信息10
        #endregion

        string sql = " select sn.* from BASE_BAR_SN sn with(nolock) where 1=1 ";
        if (!string.IsNullOrEmpty(parameter.BarCodeType)) {
            sql += string.Format(" and sn.BarCodeType = '{0}' ", parameter.BarCodeType);
        }
        if (!string.IsNullOrEmpty(parameter.sn))
        {
            sql += string.Format(" and sn.sn_code like '%{0}%' ", parameter.sn);
        }
        if (!string.IsNullOrEmpty(parameter.CreateFrom))
        {
            DateTime createFrom = Convert.ToDateTime(parameter.CreateFrom);
            sql += string.Format(" and sn.create_time >= '{0}' ", createFrom.ToString("yyyy-MM-dd"));
        }
        if (!string.IsNullOrEmpty(parameter.CreateTo))
        {
            DateTime createTo = Convert.ToDateTime(parameter.CreateTo).AddDays(1).AddSeconds(-1);
            sql += string.Format(" and sn.create_time <= '{0}' ", createTo.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        if (!string.IsNullOrEmpty(parameter.CreateUser))
        {
            DateTime createFrom = Convert.ToDateTime(parameter.CreateFrom);
            sql += string.Format(" and sn.create_owner like '%{0}%' ", parameter.CreateUser);
        }

        sql += " order by  sn.create_time desc ";

        DataTable tb = SqlDBHelp.ExecuteToDataTable(sql);
        if (tb != null && tb.Rows.Count > 0) {
            DataRow drExport = null;
            foreach (DataRow row in tb.Rows)
            {
                drExport = dtExport.NewRow();
                drExport["SNCODE"] = row["sn_code"];
                drExport["DATECODE"] = row["datecode"];
                drExport["CINVCODE"] = row["cinvcode"];
                drExport["CINVNAME"] = row["cinvcode_name"];
                drExport["PONUMBER"] = row["po_number"];
                drExport["POLINEID"] = row["Po_LineId"];
                drExport["VENDOR"] = row["vendor"];//供应商编号
                drExport["VENDORNAME"] = row["vendor_name"];//供应商名称
                drExport["SONUMBER"] = row["So_Number"]; //销售单单号
                drExport["SOLINEID"] = row["So_LineId"]; //销售单项次
                drExport["CLIENT"] = row["ClientCode"]; //销售单单号
                drExport["CLIENTNAME"] = row["ClinetName"]; //销售单项次
                drExport["QUANTITY"] = Convert.ToDecimal(row["quantity"].ToString()).ToString("f2");
                drExport["ArbitraryVal1"] = row["ArbitraryVal1"]; //任意值1
                drExport["ArbitraryVal2"] = row["ArbitraryVal2"]; //任意值2
                drExport["ArbitraryVal3"] = row["ArbitraryVal3"]; //任意值3
                drExport["ArbitraryVal4"] = row["ArbitraryVal4"]; //任意值4
                drExport["ArbitraryVal5"] = row["ArbitraryVal5"]; //任意值5
                drExport["ArbitraryVal6"] = row["ArbitraryVal6"]; //任意值6
                drExport["ArbitraryVal7"] = row["ArbitraryVal7"]; //任意值7
                drExport["ArbitraryVal8"] = row["ArbitraryVal8"]; //任意值8
                drExport["ArbitraryVal9"] = row["ArbitraryVal9"]; //任意值9
                drExport["ArbitraryVal10"] = row["ArbitraryVal10"]; //任意值10
                drExport["PrintVal1"] = row["PrintVal1"]; //附加打印信息1
                drExport["PrintVal2"] = row["PrintVal2"]; //附加打印信息2
                drExport["PrintVal3"] = row["PrintVal3"]; //附加打印信息3
                drExport["PrintVal4"] = row["PrintVal4"]; //附加打印信息4
                drExport["PrintVal5"] = row["PrintVal5"]; //附加打印信息5
                drExport["PrintVal6"] = row["PrintVal6"]; //附加打印信息6
                drExport["PrintVal7"] = row["PrintVal7"]; //附加打印信息7
                drExport["PrintVal8"] = row["PrintVal8"]; //附加打印信息8
                drExport["PrintVal9"] = row["PrintVal9"]; //附加打印信息9
                drExport["PrintVal10"] = row["PrintVal10"]; //附加打印信息10
                dtExport.Rows.Add(drExport);
            }
        }

        string templetPath = Server.MapPath("/ExcelTemplate/SNExportTemplete.xls");//获取模板文件
        Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(templetPath); //定义Excel工作簿，加载模板
        Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];
        Aspose.Cells.Cells cells = worksheet.Cells;
        cells.ImportDataTable(dtExport, false, 1, 0);
        cells.Rows.RemoveAt(dtExport.Rows.Count + 1);
        string exportFileName = "条码信息_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        string newfileName = Server.MapPath("~/TempFile/" + exportFileName + ".xls");
        workbook.Save(newfileName); //保存文件
        return new WebServiceResult("0", "/TempFile/" + exportFileName + ".xls");

    }


    [WebMethod]
    public WebServiceResult SavePrintRules(PrintRuleParameter parameter)
    {
        WebServiceResult result = new WebServiceResult("0", "保存成功！");
        if (parameter == null)
        {
            result.code = "1";
            result.msg = "参数错误，保存失败！";
            return result;
        }

        #region 部分规则再验证
        if (parameter.Rules == null || !parameter.Rules.Any())
        {
            result.code = "1";
            result.msg = "无规则明细,保存失败!";
            return result;
        }

        #endregion

        DBContext context = new DBContext();
        using (var modContext = context)
        {
            using (var dbContextTransaction = modContext.Database.BeginTransaction())
            {
                try
                {
                    var modPrint = modContext.BASE_BARCODE_PRINT.Where(x => x.Id == parameter.id).FirstOrDefault();
                    if (modPrint != null)
                    {
                        if (modPrint.Cstatus == "0")
                        {
                            //已有的规则明细需删除
                            var ruleItemList = modContext.BASE_BARCODE_PRINT_D.Where(x => x.PrintId == modPrint.Id).ToList();
                            if (ruleItemList.Any())
                            {
                                foreach (var item in ruleItemList)
                                {
                                    modContext.BASE_BARCODE_PRINT_D.Remove(item);
                                }
                                modContext.SaveChanges();
                            }

                            foreach (var item in parameter.Rules)
                            {
                                BASE_BARCODE_PRINT_D modPrint_d = new BASE_BARCODE_PRINT_D();
                                modPrint_d.Id = Guid.NewGuid().ToString();
                                modPrint_d.PrintId = modPrint.Id;
                                modPrint_d.PrintSeq = item.Seq;
                                modPrint_d.PrintItemName = item.Name;
                                modPrint_d.PrintItemType = item.IType;
                                modPrint_d.IsNeedVal = item.IsNeed ? "1" : "0";
                                modPrint_d.PrintRemark = item.Remark;
                                modContext.BASE_BARCODE_PRINT_D.Add(modPrint_d);
                            }
                            modContext.SaveChanges();
                            dbContextTransaction.Commit();

                        }
                        else
                        {
                            result.code = "1";
                            result.msg = "当前规则的状态不允许保存!";
                        }
                    }
                    else
                    {
                        result.code = "1";
                        result.msg = "信息异常,保存失败!";
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    result.code = "1";
                    result.msg = "保存失败，错误信息：" + ex.Message;
                }
            }
        }

        return result;
    
    }



    private string getSNFlow(string oldSNFlow ,int plen) {
        int oldDec = GetDecFlow(oldSNFlow);
        string newFlow = SetDecFlow(oldDec + 1);
        return newFlow.PadLeft(plen, '0');
    }

    private int GetDecFlow(string flowVal) {
        int n = 0;
        Func<char, int> getnum = (x) => { for (int i = 0; i < basicData.Length; i++) if (x == basicData[i]) return i; return 0; };
        for (int i = 0; i < flowVal.Length; i++)
        {
            n += Convert.ToInt32(Math.Pow((double)basicData.Length, (double)(flowVal.Length - i - 1)) * getnum(flowVal[i]));
        }
        return n;   
    }

    private string SetDecFlow(int decFlow) {
        string value = "";
        int decvalue = decFlow;
        int n = 0;
        if (decvalue == 0) return new string(new char[] { basicData[0] });
        while (decvalue > 0)
        {
            n = decvalue % basicData.Length;
            value = basicData[n] + value;
            decvalue = decvalue / basicData.Length;
        }
        return value;  
    }


    /// <summary>
    /// 获取指定日期，在为一年中为第几周
    /// </summary>
    /// <param name="dt">指定时间</param>
    /// <reutrn>返回第几周</reutrn>
    private int GetWeekOfYear(DateTime dt)
    {
        GregorianCalendar gc = new GregorianCalendar();
        int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        return weekOfYear;
    }

}

public class CodeRuleParameter
{
    //主键
    public string id { get; set; }

    public List<CodeRuleItem> Rules { get; set; }

}

public class CodeRuleItem
{
    public int Sno { get; set; }
    public string Name { get; set; }
    public int Len { get; set; }
    public int IFrom { get; set; }
    public int ITo { get; set; }
    public string IType { get; set; }
    public string IFormat { get; set; }
    public string IDefault { get; set; }
    public bool IMandatory { get; set; }
    public string Remark { get; set; }
    public string CreateUser { get; set; }
}

public class WebServiceResult
{

    public WebServiceResult()
    {

    }
    public WebServiceResult(string _code, string _msg)
    {
        code = _code;
        msg = _msg;
    }
    public string code { get; set; }
    public string msg { get; set; }
}


public class CodeNoParameter
{
    public string RuleId { get; set; }
    public string CreateUser { get; set; }
    public string SNId { get; set; }
    public string PrintId { get; set; }
    public List<CodeNoItem> RuleVales { get; set; }
}

public class CodeNoItem
{
    public int Sno { get; set; }
    public int Source { get; set; }
    public string RuleVal { get; set; }
}


public class PrintRuleParameter
{
    //主键
    public string id { get; set; }
    public string CreateUser { get; set; }
    public List<PrintRuleItem> Rules { get; set; }

}

public class PrintRuleItem
{
    public int Seq { get; set; }
    public string Name { get; set; }
    public string IType { get; set; }
    public bool IsNeed { get; set; }
    public string Remark { get; set; }  
}


public class ExportToExcelParameter
{
    public string sn { get; set; }

    public string CreateFrom { get; set; }

    public string CreateTo { get; set; }

    public string CreateUser { get; set; }

    public string BarCodeType { get; set; }


}
