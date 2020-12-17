using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BAR_FrmBar_CodeMakeEdit_aspx : WMSBasePage
{
    /// <summary>
    /// snID
    /// </summary>
    public string SnGuid
    {
        get { return this.hiddId.Value; }
        set { this.hiddId.Value = value; }
    }

    public string RuleType
    {
        get { return this.hiddRuleType.Value; }
        set { this.hiddRuleType.Value = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetParameters();
            this.InitTitle();
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
            this.SnGuid = Guid.Empty.ToString();
        }
        else
        {
            this.SnGuid = this.Request.QueryString["ID"];
        }

        if (string.IsNullOrEmpty(this.Request.QueryString["RuleType"]))
        {
            this.RuleType = "";
        }
        else
        {
            this.RuleType = this.Request.QueryString["RuleType"];
        }
    }

    private void InitTitle()
    {
        switch (this.RuleType)
        {
            case "PCS":
                if (this.Operation() == SYSOperation.New)
                {
                    this.ltPageTable.Text = Resources.Lang.FrmBar_CodeMakeEdit_PageTitle_PCS_New;// "PCS条码管理->新建条码";
                }
                else
                {
                    this.ltPageTable.Text = Resources.Lang.FrmBar_CodeMakeEdit_PageTitle_PCS_View;// "PCS条码管理->查看条码";
                }
                break;
            case "CARTON":
                if (this.Operation() == SYSOperation.New)
                {
                    this.ltPageTable.Text = Resources.Lang.FrmBar_CodeMakeEdit_PageTitle_CARTON_New;// "箱条码管理->新建条码";
                }
                else
                {
                    this.ltPageTable.Text = Resources.Lang.FrmBar_CodeMakeEdit_PageTitle_CARTON_View;// "箱条码管理->查看条码";
                }
                break;
            case "PALLET":
                if (this.Operation() == SYSOperation.New)
                {
                    this.ltPageTable.Text = Resources.Lang.FrmBar_CodeMakeEdit_PageTitle_PALLET_New;// "栈板条码管理->新建条码";
                }
                else
                {
                    this.ltPageTable.Text = Resources.Lang.FrmBar_CodeMakeEdit_PageTitle_PALLET_View;// "栈板条码管理->查看条码";
                }
                break;
        }
    }

    private void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('SnCode');return false;";

        string currentRule = "";//当前规则
        string currentPrint = "";//当前打印规则
        string SN = "";
        if (this.Operation() != SYSOperation.New && this.SnGuid != Guid.Empty.ToString())
        {
            BASE_BAR_SN modSn = context.BASE_BAR_SN.Where(x => x.id == this.SnGuid && x.BarCodeType == this.RuleType).FirstOrDefault();
            if (modSn != null)
            {
                SN = modSn.sn_code;
                currentRule = modSn.CodeRuleId;
                currentPrint = modSn.PrintRuleId;
            }
            btnSaveRule.Disabled = true;
        }

        var paraList = context.BASE_BARCODE_RULE.Where(x => x.RULETYPE == this.RuleType && x.STATUS == "1").OrderBy(x => x.CREATEDATE).ToList();
        drpRuleSelect.Items.Clear();

        if (paraList.Any())
        {
            if (string.IsNullOrEmpty(currentRule))
            {
                currentRule = paraList.FirstOrDefault().ID;
            }
            foreach (var item in paraList)
            {
                ListItem optionItem = new ListItem(item.RULENAME, item.ID);
                optionItem.Attributes["defprint"] = !string.IsNullOrEmpty(item.DefaultPrintId) ? item.DefaultPrintId : "";
                drpRuleSelect.Items.Add(optionItem);
            }
            drpRuleSelect.SelectedValue = currentRule;
        }

        var printList = context.BASE_BARCODE_PRINT.Where(x => x.Cstatus == "1" && x.BarCodeType == this.RuleType).OrderBy(x => x.CreateTime).ToList();
        drpPrintSelect.Items.Clear();
        drpPrintSelect.Items.Add(new ListItem(Resources.Lang.FrmBar_CodeMakeEdit_Tips_XuanzeDayin, ""));
        if (printList.Any())
        {
            foreach (var item in printList)
            {
                ListItem optionItem = new ListItem(item.PrintName, item.Id);
                drpPrintSelect.Items.Add(optionItem);
            }
        }
        drpPrintSelect.SelectedValue = currentPrint;

        if (this.Operation() == SYSOperation.New)
        {
            LoadRuleItems(currentRule, currentPrint);
        }
        else
        {
            drpRuleSelect.Enabled = false;
            drpPrintSelect.Enabled = false;
            BindRuleItems(currentRule, SN);
            btnSaveRule.Attributes["disabled"] = "disabled";
        }

        hiddUser.Value = WmsWebUserInfo.GetCurrentUser().UserNo;
    }

    private void LoadRuleItems(string currentRule, string currentPrint)
    {
        //拼接数据列表
        StringBuilder nodeHtml = new StringBuilder();
        if (!string.IsNullOrEmpty(currentRule))
        {
            var modRule = context.BASE_BARCODE_RULE.Where(x => x.ID == currentRule).FirstOrDefault();
            if (modRule != null && !string.IsNullOrEmpty(modRule.DefaultPrintId))
            {
                currentPrint = modRule.DefaultPrintId;
                drpPrintSelect.SelectedValue = currentPrint;
            }

            List<BASE_BARCODE_RULE_D> ruleItems = context.BASE_BARCODE_RULE_D.Where(x => x.RULEID == currentRule).OrderBy(x => x.SNO).ToList();
            if (ruleItems.Any())
            {
                nodeHtml.Append("<tr>");
                nodeHtml.Append("<td colspan=\"2\" style=\"padding:15px 0px;\">");
                nodeHtml.Append("<table class=\"InputTable ruleItemTable\" style=\"width: 100%\" cellspacing=\"1\" cellpadding=\"1\" border=\"0\">");
                nodeHtml.Append("<thead><tr>");
                nodeHtml.Append("<th style=\"width:150px;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_Name + "</th>");
                nodeHtml.Append("<th style=\"width:200px;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_Value + "</th>");
                nodeHtml.Append("<th style=\"width:50px;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_Length + "</th>");
                nodeHtml.Append("<th style=\"width:70px;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_Weishu + "</th>");
                nodeHtml.Append("<th style=\"width:60px;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_Geshi + "</th>");
                nodeHtml.Append("<th style=\"width:150px;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_SheDing + "</th>");
                nodeHtml.Append("<th style=\"width:auto;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_Shuoming + "</th>");
                nodeHtml.Append("</tr></thead>");
                nodeHtml.Append("<tbody class=\"tbRuleItems\">");
                foreach (var item in ruleItems)
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
                    nodeHtml.Append("<td class=\"td-left\"><input id=\"txtValue\" type=\"text\" class=\"textBox\" maxlength=\"50\" style=\"width:98%;min-width: 150px;\" " + disableStr + " autocomplete=\"off\" value=\"" + initialVal + "\" />");
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
                    if (item.BARCODETYPEID == "9")
                    {
                        if (!string.IsNullOrEmpty(item.FORMATID))
                        {
                            int interger = Convert.ToInt32(item.FORMATID.Split(',')[0]);
                            int dec = Convert.ToInt32(item.FORMATID.Split(',')[1]);
                            if (dec == 0)
                            {
                                nodeHtml.Append(interger + Resources.Lang.FrmBar_CodeMakeEdit_Format_Interger);
                            }
                            else
                            {
                                nodeHtml.Append(interger + Resources.Lang.FrmBar_CodeMakeEdit_Format_Interger + dec + Resources.Lang.FrmBar_CodeMakeEdit_Format_Decimal);
                            }
                        }
                    }
                    else
                    {
                        nodeHtml.Append(item.FORMATID);
                    }
                    nodeHtml.Append("</td>");
                    nodeHtml.Append("<td>" + item.DEFAULTVALUE + "</td>");
                    nodeHtml.Append("<td>" + item.REMARK + "</td>");
                    nodeHtml.Append("</tr>");
                }

                if (!string.IsNullOrEmpty(currentPrint))
                {
                    List<BASE_BARCODE_PRINT_D> printItems = context.BASE_BARCODE_PRINT_D.Where(x => x.PrintId == currentPrint).OrderBy(x => x.PrintSeq).ToList();
                    if (printItems.Any())
                    {
                        foreach (var iPrint in printItems)
                        {
                            string isNeedStr = iPrint.IsNeedVal == "1" ? "<span class=\"requiredSign\">*</span>" : "";
                            nodeHtml.Append("<tr data-isneed=\"" + iPrint.IsNeedVal + "\" data-len=\"0\" data-fmat=\"\" data-defval=\"\" data-ruletype=\"" + iPrint.PrintItemType + "\" data-seq=\"" + iPrint.PrintSeq.ToString() + "\" data-name =\"" + iPrint.PrintItemName + "\" data-source=\"1\">");
                            nodeHtml.Append("<td class=\"txtItemTitle\">" + isNeedStr + iPrint.PrintItemName + "</td>");
                            string initialVal = string.Empty;
                            string disableStr = string.Empty;
                            nodeHtml.Append("<td class=\"td-left\"><input id=\"txtValue\" type=\"text\" class=\"textBox\" maxlength=\"50\" style=\"width:98%;min-width: 150px;\" " + disableStr + " autocomplete=\"off\" value=\"" + initialVal + "\" />");
                            nodeHtml.Append("<ul class=\"weul\"></ul>");
                            nodeHtml.Append("</td>");
                            nodeHtml.Append("<td class=\"td-left\"></td>");
                            nodeHtml.Append("<td class=\"td-left\">");
                            nodeHtml.Append("</td>");
                            //格式
                            nodeHtml.Append("<td class=\"tdFormat\">");
                            if (iPrint.PrintItemType == "0")
                            {
                                nodeHtml.Append(Resources.Lang.FrmBar_CodeMakeEdit_Format_ZiFu);
                            }
                            else
                            {
                                nodeHtml.Append(Resources.Lang.FrmBar_CodeMakeEdit_Format_ShuZhi);
                            }
                            nodeHtml.Append("</td>");
                            nodeHtml.Append("<td></td>");
                            nodeHtml.Append("<td>" + iPrint.PrintRemark + "</td>");
                            nodeHtml.Append("</tr>");
                        }
                    }
                }

                nodeHtml.Append("</tbody>");
                nodeHtml.Append("</table>");
                nodeHtml.Append("</td></tr>");
            }
            else
            {
                nodeHtml.Append("<tr>");
                nodeHtml.Append("<td colspan=\"2\">");
                nodeHtml.Append(Resources.Lang.FrmBar_CodeMakeEdit_Tips_XuanzeGuiZe);
                nodeHtml.Append("</td>");
                nodeHtml.Append("</tr>");
            }
        }
        else
        {
            nodeHtml.Append("<tr>");
            nodeHtml.Append("<td colspan=\"2\">");
            nodeHtml.Append(Resources.Lang.FrmBar_CodeMakeEdit_Tips_XuanzeGuiZe);
            nodeHtml.Append("</td>");
            nodeHtml.Append("</tr>");
        }
        tbRuleItems.InnerHtml = nodeHtml.ToString();
    }

    private void BindRuleItems(string currentRule, string sn)
    {
        this.txtSN.Text = sn;
        //拼接数据列表
        StringBuilder nodeHtml = new StringBuilder();
        if (!string.IsNullOrEmpty(currentRule))
        {
            List<BASE_BARCODE_RULE_D> ruleItems = context.BASE_BARCODE_RULE_D.Where(x => x.RULEID == currentRule).OrderBy(x => x.SNO).ToList();
            if (ruleItems.Any())
            {
                nodeHtml.Append("<tr>");
                nodeHtml.Append("<td colspan=\"2\" style=\"padding:15px 0px;\">");
                nodeHtml.Append("<table class=\"InputTable ruleItemTable\" style=\"width: 100%\" cellspacing=\"1\" cellpadding=\"1\" border=\"0\">");
                nodeHtml.Append("<thead><tr>");
                nodeHtml.Append("<th style=\"width:150px;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_Name + "</th>");
                nodeHtml.Append("<th style=\"width:200px;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_Value + "</th>");
                nodeHtml.Append("<th style=\"width:50px;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_Length + "</th>");
                nodeHtml.Append("<th style=\"width:70px;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_Weishu + "</th>");
                nodeHtml.Append("<th style=\"width:60px;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_Geshi + "</th>");
                nodeHtml.Append("<th style=\"width:150px;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_SheDing + "</th>");
                nodeHtml.Append("<th style=\"width:auto;\">" + Resources.Lang.FrmBar_CodeMakeEdit_Th_Shuoming + "</th>");
                nodeHtml.Append("</tr></thead>");
                nodeHtml.Append("<tbody class=\"tbRuleItems\">");
                foreach (var item in ruleItems)
                {
                    string isNeedStr = item.MANDATORY == "1" ? "<span class=\"requiredSign\">*</span>" : "";
                    nodeHtml.Append("<tr data-isneed=\"" + item.MANDATORY + "\" data-len=\"" + item.ILENGTH + "\" data-fmat=\"" + item.FORMATID + "\" data-defval=\"" + item.DEFAULTVALUE + "\" data-ruletype=\"" + item.BARCODETYPEID + "\" data-seq=\"" + item.SNO.ToString() + "\" data-name =\"" + item.NAME + "\" data-source=\"0\">");
                    nodeHtml.Append("<td class=\"txtItemTitle\">" + isNeedStr + item.NAME + "</td>");
                    string initialVal = string.Empty;

                    initialVal = sn.Substring(item.IFROM.Value - 1, item.ILENGTH.Value).Trim();
                    string disableStr = string.Empty;
                    //if (item.BARCODETYPEID == "11")
                    //{
                    //    disableStr = "disabled=\"disabled\"";
                    //}
                    disableStr = "disabled=\"disabled\"";
                    nodeHtml.Append("<td class=\"td-left\"><input id=\"txtValue\" type=\"text\" class=\"textBox\" maxlength=\"50\" style=\"width:98%;min-width: 150px;\" " + disableStr + " autocomplete=\"off\" value=\"" + initialVal + "\" />");
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

                BASE_BAR_SN modSn = context.BASE_BAR_SN.Where(x => x.id == this.SnGuid).FirstOrDefault();

                if (modSn != null && !string.IsNullOrEmpty(modSn.PrintRuleId))
                {
                    List<BASE_BARCODE_PRINT_D> printItems = context.BASE_BARCODE_PRINT_D.Where(x => x.PrintId == modSn.PrintRuleId).OrderBy(x => x.PrintSeq).ToList();
                    if (printItems.Any())
                    {
                        foreach (var iPrint in printItems)
                        {
                            string isNeedStr = iPrint.IsNeedVal == "1" ? "<span class=\"requiredSign\">*</span>" : "";
                            nodeHtml.Append("<tr data-isneed=\"" + iPrint.IsNeedVal + "\" data-len=\"0\" data-fmat=\"\" data-defval=\"\" data-ruletype=\"" + iPrint.PrintItemType + "\" data-seq=\"" + iPrint.PrintSeq.ToString() + "\" data-name =\"" + iPrint.PrintItemName + "\" data-source=\"1\">");
                            nodeHtml.Append("<td class=\"txtItemTitle\">" + isNeedStr + iPrint.PrintItemName + "</td>");
                            string initialVal = string.Empty;
                            switch (iPrint.PrintSeq)
                            {
                                case 1:
                                    initialVal = modSn.PrintVal1;
                                    break;
                                case 2:
                                    initialVal = modSn.PrintVal2;
                                    break;
                                case 3:
                                    initialVal = modSn.PrintVal3;
                                    break;
                                case 4:
                                    initialVal = modSn.PrintVal4;
                                    break;
                                case 5:
                                    initialVal = modSn.PrintVal5;
                                    break;
                                case 6:
                                    initialVal = modSn.PrintVal6;
                                    break;
                                case 7:
                                    initialVal = modSn.PrintVal7;
                                    break;
                                case 8:
                                    initialVal = modSn.PrintVal8;
                                    break;
                                case 9:
                                    initialVal = modSn.PrintVal9;
                                    break;
                                case 10:
                                    initialVal = modSn.PrintVal10;
                                    break;
                            }

                            string disableStr = string.Empty;
                            disableStr = "disabled=\"disabled\"";
                            nodeHtml.Append("<td class=\"td-left\"><input id=\"txtValue\" type=\"text\" class=\"textBox\" maxlength=\"50\" style=\"width:98%;min-width: 150px;\" " + disableStr + " autocomplete=\"off\" value=\"" + initialVal + "\" />");
                            nodeHtml.Append("<ul class=\"weul\"></ul>");
                            nodeHtml.Append("</td>");
                            nodeHtml.Append("<td class=\"td-left\"></td>");
                            nodeHtml.Append("<td class=\"td-left\">");
                            nodeHtml.Append("</td>");
                            //格式
                            nodeHtml.Append("<td class=\"tdFormat\">");
                            if (iPrint.PrintItemType == "0")
                            {
                                nodeHtml.Append(Resources.Lang.FrmBar_CodeMakeEdit_Format_ZiFu);
                            }
                            else
                            {
                                nodeHtml.Append(Resources.Lang.FrmBar_CodeMakeEdit_Format_ShuZhi);
                            }
                            nodeHtml.Append("</td>");
                            nodeHtml.Append("<td></td>");
                            nodeHtml.Append("<td>" + iPrint.PrintRemark + "</td>");
                            nodeHtml.Append("</tr>");
                        }
                    }
                }

                nodeHtml.Append("</tbody>");
                nodeHtml.Append("</table>");
                nodeHtml.Append("</td></tr>");
            }
            else
            {
                nodeHtml.Append("<tr>");
                nodeHtml.Append("<td colspan=\"2\">");
                nodeHtml.Append(Resources.Lang.FrmBar_CodeMakeEdit_Tips_XuanzeGuiZe);
                nodeHtml.Append("</td>");
                nodeHtml.Append("</tr>");
            }
        }
        else
        {
            nodeHtml.Append("<tr>");
            nodeHtml.Append("<td colspan=\"2\">");
            nodeHtml.Append(Resources.Lang.FrmBar_CodeMakeEdit_Tips_XuanzeGuiZe);
            nodeHtml.Append("</td>");
            nodeHtml.Append("</tr>");
        }
        tbRuleItems.InnerHtml = nodeHtml.ToString();
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