using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BAR_FrmBar_CodeRuleEdit : WMSBasePage
{
    /// <summary>
    /// 规则ID
    /// </summary>
    public string RuleId
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
            this.RuleId = Guid.Empty.ToString();
        }
        else
        {
            this.RuleId = this.Request.QueryString["ID"];
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
                this.ltPageTable.Text = Resources.Lang.FrmBar_CodeRuleEdit_PageTitle_PCS;// "PCS条码管理->条码设置";
                break;
            case "CARTON":
                this.ltPageTable.Text = Resources.Lang.FrmBar_CodeRuleEdit_PageTitle_CARTON;// "箱条码管理->条码设置";
                break;
            case "PALLET":
                this.ltPageTable.Text = Resources.Lang.FrmBar_CodeRuleEdit_PageTitle_PALLET;// "栈板条码管理->条码设置";
                break;
        }
    }

    private void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearchRule'].click(); CloseMySelf('SnRule');return false;";
        //var paraList = context.SYS_PARAMETER.Where(x => x.flag_type == "RULETYPE").OrderBy(x => x.flag_id).ToList();
        //Help.DropDownListDataBind(paraList, this.drpCodeType, "", "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "RULETYPE", false, -1, -1), this.drpCodeType, "", "FLAG_NAME", "FLAG_ID", "");         

        var printList = context.BASE_BARCODE_PRINT.Where(x => x.Cstatus == "1" && x.BarCodeType == this.RuleType).OrderBy(x => x.PrintCode).ToList();
        this.drpPrintRule.Items.Clear();
        this.drpPrintRule.Items.Add(new ListItem(Resources.Lang.WMS_Common_Element_QingXuanZe, ""));
        if (printList != null && printList.Any())
        {
            foreach (var item in printList)
            {
                this.drpPrintRule.Items.Add(new ListItem("【" + item.PrintCode + "】" + item.PrintName, item.Id));
            }
        }

        if (this.Operation() == SYSOperation.New)
        {
            this.drpCodeType.SelectedValue = this.RuleType;
            this.drpCodeType.Enabled = false;
            trRuleDetail.Visible = false;
        }
        else
        {
            ShowData();
        }

        txtRuleCode.Enabled = false;
        hiddUser.Value = WmsWebUserInfo.GetCurrentUser().UserNo;
    }

    public void ShowData()
    {
        var modRule = context.BASE_BARCODE_RULE.Where(x => x.ID == this.RuleId).FirstOrDefault();
        if (modRule != null)
        {
            this.txtRuleCode.Text = modRule.RULECODE;
            this.txtRuleName.Text = modRule.RULENAME;
            this.txtRuleLen.Text = modRule.RULELEN.ToString();
            this.txtCreateTime.Text = modRule.CREATEDATE.HasValue ? modRule.CREATEDATE.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtCreateUser.Text = modRule.CREATEUSER;
            this.txtCMEMO.Text = modRule.REMARK;
            this.drpCodeType.SelectedValue = modRule.RULETYPE;
            this.drpPrintRule.SelectedValue = modRule.DefaultPrintId;
            this.txtUpdateTime.Text = modRule.MODIFYDATE.HasValue ? modRule.MODIFYDATE.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtUpdateUser.Text = modRule.MODIFYUSER;

            //设置不可用
            this.txtRuleCode.Enabled = false;
            this.txtRuleName.Enabled = false;
            this.txtRuleLen.Enabled = false;
            this.txtCreateTime.Enabled = false;
            this.txtCreateUser.Enabled = false;
            this.txtCMEMO.Enabled = false;
            this.drpCodeType.Enabled = false;
            this.txtUpdateTime.Enabled = false;
            this.txtUpdateUser.Enabled = false;

            //作废之后不允许修改
            if (modRule.STATUS == "2")
            {
                this.btnSave.Enabled = false;
                this.drpPrintRule.Enabled = false;
            }

            this.trRuleDetail.Visible = true;
            List<BASE_BARCODE_RULE_D> ruleDetails = context.BASE_BARCODE_RULE_D.Where(x => x.RULEID == this.RuleId).ToList();
            if (modRule.STATUS == "0")
            { //未启用状态
                hiddDropDownListOfRuleType.Value = GetDropDownListOfRuleType("0", false);
                hiddDropDownListOfDateFormat.Value = GetDropDownListOfDateFormat("0", false);
                hiddDropDownListOfSnoFormat.Value = GetDropDownListOfSnoFormat("0", false);
                if (ruleDetails.Any())
                {
                    LoadRuleDetails(ruleDetails, false);
                }
            }
            else
            {
                btnNew.Disabled = true;//不能新增
                btnSaveRule.Disabled = true;//不能保存
                LoadRuleDetails(ruleDetails, true);
                thOption.Visible = false;//不能删除项
            }
        }
    }

    private void LoadRuleDetails(List<BASE_BARCODE_RULE_D> ruleDetails, bool isDisable)
    {

        //拼接数据列表
        StringBuilder nodeHtml = new StringBuilder();
        if (ruleDetails.Any())
        {
            string disableStr = "";
            if (isDisable)
            {
                disableStr = "disabled=\"disabled\"";
            }
            ruleDetails = ruleDetails.OrderBy(x => x.SNO).ToList();
            foreach (var item in ruleDetails)
            {
                nodeHtml.Append("<tr>");
                nodeHtml.Append("<td>" + item.SNO + "</td>");
                nodeHtml.Append("<td class=\"td-left\"><input id=\"txtName\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width: 150px;\" " + disableStr + " value=\"" + item.NAME + "\" /></td>");
                nodeHtml.Append("<td class=\"td-left\"><input id=\"txtlen\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width:60px;text-align:center;\" " + disableStr + " value=\"" + item.ILENGTH.ToString() + "\" /></td>");
                nodeHtml.Append("<td class=\"td-left\" style=\"position: relative;\">");
                nodeHtml.Append("<input id=\"txtIFrom\" type=\"text\" class=\"textBox\" style=\"width: 45px;text-align: center;\" disabled=\"disabled\" maxlength=\"5\" value=\"" + item.IFROM.ToString() + "\" />");
                nodeHtml.Append(" - ");
                nodeHtml.Append("<input id=\"txtITo\" type=\"text\" class=\"textBox\" style=\"width: 45px;text-align: center;\" disabled=\"disabled\" maxlength=\"5\" value=\"" + item.ITO.ToString() + "\" />");
                nodeHtml.Append("</td>");
                nodeHtml.Append("<td class=\"td-left\">");
                nodeHtml.Append(GetDropDownListOfRuleType(item.BARCODETYPEID, isDisable));
                nodeHtml.Append("</td>");
                nodeHtml.Append("<td class=\"tdFormat\">");
                if (item.BARCODETYPEID == "2")
                {
                    nodeHtml.Append(GetDropDownListOfDateFormat(item.FORMATID, isDisable));
                }
                else if (item.BARCODETYPEID == "9")
                { //数量
                    string txtInterger = string.Empty;
                    string txtDecimal = string.Empty;
                    if (!string.IsNullOrEmpty(item.FORMATID))
                    {
                        txtInterger = item.FORMATID.Split(',')[0];
                        txtDecimal = item.FORMATID.Split(',')[1];
                    }
                    nodeHtml.Append(Resources.Lang.FrmBar_CodeRuleEdit_Format_zhengshu + "<input id=\"txtInteger\" type=\"text\" class=\"numbercss\" autocomplete=\"off\" value=\"" + txtInterger + "\" />" + Resources.Lang.FrmBar_CodeRuleEdit_Format_xiaoshu + "<input id=\"txtDecimal\" type=\"text\" class=\"numbercss\" autocomplete=\"off\" value=\"" + txtDecimal + "\" />");
                }
                else if (item.BARCODETYPEID == "11")
                {
                    nodeHtml.Append(GetDropDownListOfSnoFormat(item.FORMATID, isDisable));
                }
                else
                {
                    nodeHtml.Append("");
                }
                nodeHtml.Append("</td>");
                if (item.BARCODETYPEID == "10" && !isDisable)
                {
                    nodeHtml.Append("<td><input id=\"txtDefaultValue\" type=\"text\" class=\"textBox\" style=\"width: 98%;min-width:80px\" maxlength=\"100\"  value=\"" + item.DEFAULTVALUE + "\" /></td>");
                }
                else
                {
                    nodeHtml.Append("<td><input id=\"txtDefaultValue\" type=\"text\" class=\"textBox\" style=\"width: 98%;min-width:80px\" maxlength=\"5\"  disabled=\"disabled\" value=\"" + item.DEFAULTVALUE + "\" /></td>");
                }
                string checkStr = item.MANDATORY == "1" ? " checked=\"checked\" " : "";
                if (item.BARCODETYPEID == "11" || isDisable)
                {
                    nodeHtml.Append("<td><span id=\"spanmandatory\"></span><input id=\"chkmandatory\" type=\"checkbox\" " + checkStr + " disabled=\"disabled\" /></td>");
                }
                else
                {
                    nodeHtml.Append("<td><span id=\"spanmandatory\"></span><input id=\"chkmandatory\" type=\"checkbox\"   " + checkStr + "/></td>");
                }
                nodeHtml.Append("<td><input type=\"text\" id=\"txtRemark\" style=\"width: 98%;min-width: 200px;\" " + disableStr + " value=\"" + item.REMARK + "\" /></td>");
                if (!isDisable)
                {
                    nodeHtml.Append("<td>");
                    nodeHtml.Append("<a href=\"javascript:;\" class=\"cancelButton\" onclick=\"WMSUI.DeleteRuleItem(this);\">" + Resources.Lang.FrmBar_CodePrintEdit_Th_ShangChu + "</a>");
                    nodeHtml.Append("</td>");
                }
                nodeHtml.Append("</tr>");
            }
        }
        tbRuleItems.InnerHtml = nodeHtml.ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            btnSave.Enabled = false;
            SaveTODB();
            btnSave.Enabled = true;
            this.btnSave.Style.Remove("disabled");
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }

    private void SaveTODB()
    {
        string msg = string.Empty;
        if (this.CheckData())
        {
            try
            {
                if (this.Operation() == SYSOperation.New)
                {
                    using (var modContext = context)
                    {
                        string strKeyID = Guid.NewGuid().ToString();
                        BASE_BARCODE_RULE modRule = new BASE_BARCODE_RULE();
                        modRule.ID = strKeyID;
                        var seq = 0;
                        var maxRule = modContext.BASE_BARCODE_RULE.Where(x => x.RULECODE.Contains("BCR")).Max(x => x.RULECODE);
                        if (!string.IsNullOrEmpty(maxRule))
                        {
                            seq = Convert.ToInt32(maxRule.Substring(maxRule.Length - 4, 4));
                        }
                        seq += 1;
                        modRule.RULECODE = "BCR" + seq.ToString().PadLeft(4, '0');
                        modRule.RULENAME = this.txtRuleName.Text.Trim();
                        modRule.RULETYPE = this.drpCodeType.SelectedValue;
                        modRule.RULELEN = Convert.ToInt32(this.txtRuleLen.Text.Trim());
                        modRule.CREATEDATE = DateTime.Now;
                        modRule.CREATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                        modRule.REMARK = this.txtCMEMO.Text.Trim();
                        modRule.STATUS = "0";//未启用
                        if (!string.IsNullOrEmpty(drpPrintRule.SelectedValue)) {
                            modRule.DefaultPrintId = drpPrintRule.SelectedValue;
                        }
                        modContext.BASE_BARCODE_RULE.Add(modRule);
                        modContext.SaveChanges();
                        this.AlertAndBack("FrmBar_CodeRuleEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID) + "&RuleType=" + this.RuleType, Resources.Lang.WMS_Common_Msg_SaveSuccess);
                    }
                }
                else
                {
                    using (var modContext = context)
                    {
                        var modRule = modContext.BASE_BARCODE_RULE.Where(x => x.ID == this.RuleId).FirstOrDefault();
                        if (modRule != null)
                        {
                            modRule.DefaultPrintId = drpPrintRule.SelectedValue;
                            modRule.MODIFYDATE = DateTime.Now;
                            modRule.MODIFYUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                            modContext.Entry(modRule).State = System.Data.Entity.EntityState.Modified;
                            modContext.SaveChanges();

                            this.AlertAndBack("FrmBar_CodeRuleEdit.aspx?" + BuildQueryString(SYSOperation.Modify, this.RuleId) + "&RuleType=" + this.RuleType, Resources.Lang.WMS_Common_Msg_SaveSuccess);
                        }
                        else
                        {
                            this.AlertAndBack("FrmBar_CodeRuleEdit.aspx?" + BuildQueryString(SYSOperation.Modify, this.RuleId) + "&RuleType=" + this.RuleType, Resources.Lang.WMS_Common_Msg_SaveFailed);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                if (this.Operation() == SYSOperation.Modify)
                {
                    this.AlertAndBack("FrmBar_CodeRuleEdit.aspx?" + BuildQueryString(SYSOperation.Modify, this.RuleId) + "&RuleType=" + this.RuleType, Resources.Lang.WMS_Common_Msg_SaveFailed);
                }
                else
                {
                    this.Alert(Resources.Lang.WMS_Common_Msg_SaveFailed + ":" + ex.Message);
                }
            }
        }
    }

    public bool CheckData()
    {
        if (string.IsNullOrEmpty(txtRuleName.Text.Trim()))
        {
            this.Alert(Resources.Lang.FrmBar_CodeRuleEdit_Tips_NeedRuleName);
            this.SetFocus(txtRuleName);
            return false;
        }

        if (string.IsNullOrEmpty(txtRuleLen.Text.Trim()))
        {
            this.Alert(Resources.Lang.FrmBar_CodeRuleEdit_Tips_NeedLength);
            this.SetFocus(txtRuleLen);
            return false;
        }
        else
        {
            //校验是数字
            int checkVal = 0;
            if (!int.TryParse(txtRuleLen.Text.Trim(), out checkVal))
            {
                this.Alert(Resources.Lang.FrmBar_CodeRuleEdit_Tips_LengthError);
                this.SetFocus(txtRuleLen);
                return false;
            }
            if (checkVal <= 0)
            {
                this.Alert(Resources.Lang.FrmBar_CodeRuleEdit_Tips_LengthDaYu);
                this.SetFocus(txtRuleLen);
                return false;
            }
            else if (checkVal > 200)
            {
                this.Alert(Resources.Lang.FrmBar_CodeRuleEdit_Tips_LengthXiaoYu);
                this.SetFocus(txtRuleLen);
                return false;
            }
        }

        if (this.txtCMEMO.Text.Trim().Length > 250)
        {
            this.Alert(Resources.Lang.FrmBar_CodeRuleEdit_Tips_BeiZhuChangDu);
            this.SetFocus(txtCMEMO);
            return false;
        }
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="curSex"></param>
    /// <returns></returns>
    private string GetDropDownListOfRuleType(string currentId, bool disabledFalg)
    {
        //自定义下拉控件
        System.Web.UI.HtmlControls.HtmlSelect drp = new System.Web.UI.HtmlControls.HtmlSelect();
        drp.Attributes["id"] = "drpRuleType";
        drp.Attributes["class"] = "dropDownList";
        drp.Attributes["style"] = "width:98%;min-width:100px;";
        if (disabledFalg)
        {
            drp.Attributes["disabled"] = "disabled";
        }
        var paraList = context.SYS_PARAMETER.Where(x => x.flag_type == "BARCODETYPE").OrderBy(x => x.sortid).ToList();
        drp.Items.Clear();
        foreach (var item in paraList)
        {
            ListItem list = new ListItem();
            list.Text = item.flag_name.ToString();
            list.Value = item.flag_id.ToString();
            drp.Items.Add(list);
        }
        //设置选中值
        if (currentId != "0")
        {
            DropDownListItemSelected(drp, currentId);
        }
        drp.Attributes["onchange"] = "WMSUI.RuleTypeChange(this);";
        //返回HTML字符串
        return ReturnControl(drp);
    }

    private string GetDropDownListOfDateFormat(string currentFomrmat, bool disabledFalg)
    {
        //自定义下拉控件
        System.Web.UI.HtmlControls.HtmlSelect drp = new System.Web.UI.HtmlControls.HtmlSelect();
        drp.Attributes["id"] = "drpDateFormat";
        drp.Attributes["class"] = "dropDownList";
        drp.Attributes["style"] = "width:98%;min-width:100px;";
        if (disabledFalg)
        {
            drp.Attributes["disabled"] = "disabled";
        }
        var paraList = context.SYS_PARAMETER.Where(x => x.flag_type == "DATEFORMAT").OrderBy(x => x.sortid).ToList();
        drp.Items.Clear();
        foreach (var item in paraList)
        {
            ListItem list = new ListItem();
            list.Text = item.flag_name.ToString();
            list.Value = item.flag_id.ToString();
            drp.Items.Add(list);
        }
        //设置选中值
        if (currentFomrmat != "0")
        {
            DropDownListItemSelected(drp, currentFomrmat);
        }
        //返回HTML字符串
        return ReturnControl(drp);
    }

    private string GetDropDownListOfSnoFormat(string currentFomrmat, bool disabledFalg)
    {
        //自定义下拉控件
        System.Web.UI.HtmlControls.HtmlSelect drp = new System.Web.UI.HtmlControls.HtmlSelect();
        drp.Attributes["id"] = "drpSnoFormat";
        drp.Attributes["class"] = "dropDownList";
        drp.Attributes["style"] = "width:98%;min-width:100px;";
        if (disabledFalg)
        {
            drp.Attributes["disabled"] = "disabled";
        }
        var paraList = context.SYS_PARAMETER.Where(x => x.flag_type == "SNOFORMAT").OrderBy(x => x.sortid).ToList();
        drp.Items.Clear();
        foreach (var item in paraList)
        {
            ListItem list = new ListItem();
            list.Text = item.flag_name.ToString();
            list.Value = item.flag_id.ToString();
            drp.Items.Add(list);
        }
        //设置选中值
        if (currentFomrmat != "0")
        {
            DropDownListItemSelected(drp, currentFomrmat);
        }
        //返回HTML字符串
        return ReturnControl(drp);
    }

    /// <summary>
    /// 将HTML控件转换成字符串
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public string ReturnControl(Control control)
    {
        StringWriter sw = new StringWriter(CultureInfo.InvariantCulture);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        control.RenderControl(htw);
        htw.Flush();
        htw.Close();
        return sw.ToString();
    }

    public void DropDownListItemSelected(System.Web.UI.HtmlControls.HtmlSelect dropDownList, string selectedValue)
    {
        for (int i = 0; i < dropDownList.Items.Count; i++)
        {
            if (dropDownList.Items[i].Value == selectedValue)
            {
                dropDownList.SelectedIndex = i;
                break;
            }
        }
    }
}