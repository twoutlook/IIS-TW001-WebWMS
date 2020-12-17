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

public partial class Apps_BAR_FrmBar_CodePrintEdit : WMSBasePage
{
    /// <summary>
    /// 规则ID
    /// </summary>
    public string PrintId
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
            this.PrintId = Guid.Empty.ToString();
        }
        else
        {
            this.PrintId = this.Request.QueryString["ID"];
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
                this.ltPageTable.Text = Resources.Lang.FrmBar_CodePrintEdit_PageTitle_PCS;// "PCS条码管理->打印设置";
                break;
            case "CARTON":
                this.ltPageTable.Text = Resources.Lang.FrmBar_CodePrintEdit_PageTitle_CARTON;// "箱条码管理->打印设置";
                break;
            case "PALLET":
                this.ltPageTable.Text = Resources.Lang.FrmBar_CodePrintEdit_PageTitle_PALLET;// "栈板条码管理->打印设置";
                break;
        }
    }

    private void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearchPrint'].click(); CloseMySelf('SnPrint');return false;";
        //var paraList = context.SYS_PARAMETER.Where(x => x.flag_type == "RULETYPE").OrderBy(x => x.flag_id).ToList();
        //Help.DropDownListDataBind(paraList, this.drpCodeType, "", "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "RULETYPE", false, -1, -1), this.drpCodeType, "", "FLAG_NAME", "FLAG_ID", "");         

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

        txtPrintCode.Enabled = false;
        hiddUser.Value = WmsWebUserInfo.GetCurrentUser().UserNo;
    }

    public void ShowData()
    {
        var modPrint = context.BASE_BARCODE_PRINT.Where(x => x.Id == this.PrintId).FirstOrDefault();
        if (modPrint != null)
        {
            this.txtPrintCode.Text = modPrint.PrintCode;
            this.txtPrintName.Text = modPrint.PrintName;
            this.txtCreateTime.Text = modPrint.CreateTime.HasValue ? modPrint.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtCreateUser.Text = modPrint.CreateUser;
            this.txtRemark.Text = modPrint.Remark;
            this.drpCodeType.SelectedValue = modPrint.BarCodeType;
            //设置不可用
            this.txtPrintCode.Enabled = false;
            this.txtPrintName.Enabled = false;
            this.txtCreateTime.Enabled = false;
            this.txtCreateUser.Enabled = false;
            this.txtRemark.Enabled = false;
            this.btnSave.Enabled = false;

            this.trRuleDetail.Visible = true;
            List<BASE_BARCODE_PRINT_D> printDetails = context.BASE_BARCODE_PRINT_D.Where(x => x.PrintId == this.PrintId).ToList();
            if (modPrint.Cstatus == "0")
            { //未启用状态
                hiddDropDownListOfRuleType.Value = GetDropDownListOfRuleType("0", false);
                if (printDetails.Any())
                {
                    LoadRuleDetails(printDetails, false);
                }
            }
            else
            {
                btnNew.Disabled = true;//不能新增
                btnSaveRule.Disabled = true;//不能保存
                LoadRuleDetails(printDetails, true);
                thOption.Visible = false;//不能删除项
            }
        }
    }

    private void LoadRuleDetails(List<BASE_BARCODE_PRINT_D> printDetails, bool isDisable)
    {

        //拼接数据列表
        StringBuilder nodeHtml = new StringBuilder();
        if (printDetails.Any())
        {
            string disableStr = "";
            if (isDisable)
            {
                disableStr = "disabled=\"disabled\"";
            }
            printDetails = printDetails.OrderBy(x => x.PrintSeq).ToList();
            foreach (var item in printDetails)
            {
                nodeHtml.Append("<tr>");
                nodeHtml.Append("<td>" + item.PrintSeq + "</td>");
                nodeHtml.Append("<td class=\"td-left\"><input id=\"txtName\" type=\"text\" class=\"textBox\" autocomplete=\"off\" style=\"width:98%;min-width: 150px;\" " + disableStr + " value=\"" + item.PrintItemName + "\" /></td>");
                nodeHtml.Append("<td class=\"td-left\">");
                nodeHtml.Append(GetDropDownListOfRuleType(item.PrintItemType, isDisable));
                nodeHtml.Append("</td>");

                string checkStr = item.IsNeedVal == "1" ? " checked=\"checked\" " : "";
                if (isDisable)
                {
                    nodeHtml.Append("<td><span id=\"spanmandatory\"></span><input id=\"chkmandatory\" type=\"checkbox\" " + checkStr + " disabled=\"disabled\" /></td>");
                }
                else
                {
                    nodeHtml.Append("<td><span id=\"spanmandatory\"></span><input id=\"chkmandatory\" type=\"checkbox\"   " + checkStr + "/></td>");
                }
                nodeHtml.Append("<td><input type=\"text\" id=\"txtRemark\" style=\"width: 98%;min-width: 200px;\" " + disableStr + " autocomplete=\"off\" value=\"" + item.PrintRemark + "\" /></td>");
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
                using (var modContext = context)
                {
                    string strKeyID = Guid.NewGuid().ToString();
                    BASE_BARCODE_PRINT modPrint = new BASE_BARCODE_PRINT();
                    modPrint.Id = strKeyID;
                    var seq = 0;
                    var maxRule = modContext.BASE_BARCODE_PRINT.Where(x => x.PrintCode.Contains("BCRP")).Max(x => x.PrintCode);
                    if (!string.IsNullOrEmpty(maxRule))
                    {
                        seq = Convert.ToInt32(maxRule.Substring(maxRule.Length - 3, 3));
                    }
                    seq += 1;
                    modPrint.PrintCode = "BCRP" + seq.ToString().PadLeft(3, '0');
                    modPrint.PrintName = this.txtPrintName.Text.Trim();
                    modPrint.BarCodeType = this.drpCodeType.SelectedValue;
                    modPrint.CreateTime = DateTime.Now;
                    modPrint.CreateUser = WmsWebUserInfo.GetCurrentUser().UserNo;
                    modPrint.Remark = this.txtRemark.Text.Trim();
                    modPrint.Cstatus = "0";//未启用
                    modContext.BASE_BARCODE_PRINT.Add(modPrint);
                    modContext.SaveChanges();
                    this.AlertAndBack("FrmBar_CodePrintEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID) + "&RuleType=" + this.RuleType, Resources.Lang.WMS_Common_Msg_SaveSuccess);
                }
            }
            catch (Exception ex)
            {
                this.Alert(Resources.Lang.WMS_Common_Msg_SaveFailed + ex.Message);
            }
        }
    }

    public bool CheckData()
    {
        if (string.IsNullOrEmpty(txtPrintName.Text.Trim()))
        {
            this.Alert(Resources.Lang.FrmBar_CodePrintEdit_Tips_NeedPrintName);
            this.SetFocus(txtPrintName);
            return false;
        }

        if (this.txtRemark.Text.Trim().Length > 250)
        {
            this.Alert(Resources.Lang.FrmBar_CodePrintEdit_Tips_BeizhuChangDu);
            this.SetFocus(txtRemark);
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

        ListItem list = new ListItem();
        list.Text = Resources.Lang.FrmBar_CodeMakeEdit_Format_ZiFu;
        list.Value = "0";
        drp.Items.Add(list);

        ListItem list1 = new ListItem();
        list1.Text = Resources.Lang.FrmBar_CodeMakeEdit_Format_ShuZhi;
        list1.Value = "1";
        drp.Items.Add(list1);


        //设置选中值
        if (currentId != "0")
        {
            DropDownListItemSelected(drp, currentId);
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