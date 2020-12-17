using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Text;

//SN管理
public partial class Apps_BAR_FrmBar_SNManagement : WMSBasePage
{
    //条码管理类型
    public string BarCodeType
    {
        get { return this.hiddBarCodeType.Value; }
        set { this.hiddBarCodeType.Value = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetParameters();
            InitPage();
            this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");

            btnSearch_Click(btnSearch, EventArgs.Empty);
            btnSearchRule_Click(btnSearchRule, EventArgs.Empty);
            btnSearchPrint_Click(btnSearchPrint, EventArgs.Empty);
        }
    }

    /// <summary>
    /// 获取传入的页面参数
    /// </summary>
    private void GetParameters()
    {
        if (string.IsNullOrEmpty(this.Request.QueryString["BarCodeType"]))
        {
            this.BarCodeType = "PCS";//没传参数时默认是pcs
        }
        else
        {
            this.BarCodeType = this.Request.QueryString["BarCodeType"];
        }
    }

    public void InitPage()
    {
        cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //设定网格主键
        grdSNBar.DataKeyNames = new string[] { "ID" };
        grdSNRule.DataKeyNames = new string[] { "ID" };
        grdSNPrint.DataKeyNames = new string[] { "ID" };
        //本页面打开新增窗口

        #region 根据类型不同显示不同信息
        string newRuleTitle = string.Empty;
        string newPrintTitle = string.Empty;
        string newBarCodeTitle = string.Empty;

        switch (this.BarCodeType)
        {
            case "PCS":
                lblTitle.Text = Resources.Lang.FrmBar_SNManagement_PCS_lblTitle;
                spRule1.InnerText = Resources.Lang.FrmBar_SNManagement_PCS_spRule1;
                spRule2.InnerText = Resources.Lang.FrmBar_SNManagement_PCS_spRule2;
                spRule3.InnerText = Resources.Lang.FrmBar_SNManagement_PCS_spRule3;
                newRuleTitle = Resources.Lang.FrmBar_SNManagement_PCS_newRuleTitle;
                newPrintTitle = Resources.Lang.FrmBar_SNManagement_PCS_newPrintTitle;
                newBarCodeTitle = Resources.Lang.FrmBar_SNManagement_PCS_newBarCodeTitle;
                break;
            case "CARTON":
                lblTitle.Text = Resources.Lang.FrmBar_SNManagement_CARTON_lblTitle;
                spRule1.InnerText = Resources.Lang.FrmBar_SNManagement_CARTON_spRule1;
                spRule2.InnerText = Resources.Lang.FrmBar_SNManagement_CARTON_spRule2;
                spRule3.InnerText = Resources.Lang.FrmBar_SNManagement_CARTON_spRule3;
                newRuleTitle = Resources.Lang.FrmBar_SNManagement_CARTON_newRuleTitle;
                newPrintTitle = Resources.Lang.FrmBar_SNManagement_CARTON_newPrintTitle;
                newBarCodeTitle = Resources.Lang.FrmBar_SNManagement_CARTON_newBarCodeTitle;
                break;
            case "PALLET":
                lblTitle.Text = Resources.Lang.FrmBar_SNManagement_PALLET_lblTitle;
                spRule1.InnerText = Resources.Lang.FrmBar_SNManagement_PALLET_spRule1;
                spRule2.InnerText = Resources.Lang.FrmBar_SNManagement_PALLET_spRule2;
                spRule3.InnerText = Resources.Lang.FrmBar_SNManagement_PALLET_spRule3;
                newRuleTitle = Resources.Lang.FrmBar_SNManagement_PALLET_newRuleTitle;
                newPrintTitle = Resources.Lang.FrmBar_SNManagement_PALLET_newPrintTitle;
                newBarCodeTitle = Resources.Lang.FrmBar_SNManagement_PALLET_newBarCodeTitle;
                break;
        }

        #endregion
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "RuleStatus", false, -1, -1), this.drpRuleStatus, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", ""); //全部
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "RuleStatus", false, -1, -1), this.drpPrintStatus, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", ""); //全部        
        btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmBar_CodeMakeEdit.aspx", SYSOperation.New, "&RuleType=" + this.BarCodeType) + "','" + newBarCodeTitle + "','SnCode');return false;";
        btnNewRule.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmBar_CodeRuleEdit.aspx", SYSOperation.New, "&RuleType=" + this.BarCodeType) + "','" + newRuleTitle + "','SnRule');return false;";
        btnNewPrint.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmBar_CodePrintEdit.aspx", SYSOperation.New, "&RuleType=" + this.BarCodeType) + "','" + newPrintTitle + "','SnPrint');return false;";
    }

    #region 条码规则
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        RuleDataBind();
    }

    protected void btnSearchRule_Click(object sender, EventArgs e)
    {
        AspNetPager2.CurrentPageIndex = 1;
        this.RuleDataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    private void RuleDataBind()
    {
        using (var modContext = this.context)
        {
            var queryList = from p in modContext.BASE_BARCODE_RULE
                            orderby p.CREATEDATE descending
                            where p.RULETYPE == this.BarCodeType
                            select p;

            if (!string.IsNullOrEmpty(txtRuleCode.Text.Trim()))
            {
                queryList = queryList.Where(x => x.RULECODE.Contains(txtRuleCode.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtRuleName.Text.Trim()))
            {
                queryList = queryList.Where(x => x.RULENAME.Contains(txtRuleName.Text.Trim()));
            }
            if (drpRuleStatus.SelectedValue != "")
            {
                queryList = queryList.Where(x => x.STATUS.ToString().Equals(drpRuleStatus.SelectedValue));
            }
            if (!string.IsNullOrEmpty(txtCreateDateFrom.Text.Trim()))
            {
                DateTime createTimeFrom = Convert.ToDateTime(txtCreateDateFrom.Text.Trim());
                queryList = queryList.Where(x => x.CREATEDATE != null && x.CREATEDATE >= createTimeFrom);
            }
            if (!string.IsNullOrEmpty(txtCreateDateTo.Text.Trim()))
            {
                DateTime createTimeTo = Convert.ToDateTime(txtCreateDateTo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.CREATEDATE != null && x.CREATEDATE < createTimeTo);
            }
            if (!string.IsNullOrEmpty(txtCreateUser.Text.Trim()))
            {
                queryList = queryList.Where(x => x.CREATEUSER.Contains(txtCreateUser.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtEnableDateFrom.Text.Trim()))
            {
                DateTime dindateFrom = Convert.ToDateTime(txtEnableDateFrom.Text.Trim());
                queryList = queryList.Where(x => x.ENABLEDATE != null && x.ENABLEDATE >= dindateFrom);
            }
            if (!string.IsNullOrEmpty(txtEnableDateTo.Text.Trim()))
            {
                DateTime dindateTo = Convert.ToDateTime(txtEnableDateTo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.ENABLEDATE != null && x.ENABLEDATE < dindateTo);
            }
            if (!string.IsNullOrEmpty(txtEnableUser.Text.Trim()))
            {
                queryList = queryList.Where(x => x.ENABLEUSER.Contains(txtEnableUser.Text.Trim()));
            }

            if (queryList != null)
            {
                AspNetPager2.RecordCount = queryList.Count();
                AspNetPager2.PageSize = this.PageSize;
            }
            //grdSNRule.DataSource = GetPageSize(queryList, PageSize, AspNetPager2.CurrentPageIndex).ToList();
            var listResult = GetPageSize(queryList, PageSize, AspNetPager2.CurrentPageIndex).ToList();
            var source = GetGridSourceDataByList(listResult, "STATUS","RuleStatus");
            grdSNRule.DataSource = source;
            grdSNRule.DataBind();          
        }
    }

    protected void grdSNRule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //ID
            string strKeyID = this.GetKeyIDS_Rule(e.Row.RowIndex);
            //编辑
            var linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBar_CodeRuleEdit.aspx", SYSOperation.Modify, strKeyID + "&RuleType=" + this.BarCodeType), this.GetSubPageTitle("BarCodeRule"), "SnRule");

            ////状态
            //if (e.Row.Cells[5].Text.Trim() == "2")
            //{
            //    e.Row.Cells[5].Text = Resources.Lang.WMS_Common_Button_Disable;
            //}
            //else if (e.Row.Cells[5].Text.Trim() == "1")
            //{
            //    e.Row.Cells[5].Text = Resources.Lang.FrmBar_SNManagement_Status_qiyong;
            //}
            //else
            //{
            //    e.Row.Cells[5].Text = Resources.Lang.FrmBar_SNManagement_Status_weiqiyong;
            //}
        }
    }

    private string GetKeyIDS_Rule(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdSNRule.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdSNRule.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEnableRule_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_BARCODE_RULE> con = new GenericRepository<BASE_BARCODE_RULE>(context);
        try
        {
            for (int i = 0; i < grdSNRule.Rows.Count; i++)
            {
                var chkSelect = (CheckBox)grdSNRule.Rows[i].Cells[0].FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    string id = this.grdSNRule.DataKeys[i].Values[0].ToString();
                    var modRule = con.Get().Where(x => x.ID == id).FirstOrDefault();
                    if (modRule != null)
                    {
                        if (modRule.STATUS == "0")
                        {
                            List<BASE_BARCODE_RULE_D> ruleDetails = context.BASE_BARCODE_RULE_D.Where(x => x.RULEID == modRule.ID).ToList();
                            if (ruleDetails.Any())
                            {
                                modRule.STATUS = "1";
                                modRule.ENABLEDATE = DateTime.Now;
                                modRule.ENABLEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                                con.Update(modRule);
                                con.Save();
                            }
                            else
                            {
                                msg = Resources.Lang.FrmBar_SNManagement_rule_msg + modRule.RULECODE + Resources.Lang.FrmBar_SNManagement_rule_msg1;
                                break;
                            }
                        }
                        else
                        {
                            msg = Resources.Lang.FrmBar_SNManagement_rule_msg + modRule.RULECODE + Resources.Lang.FrmBar_SNManagement_rule_msg2;
                            break;
                        }
                    }
                    else
                    {
                        msg = Resources.Lang.FrmBar_SNManagement_qiyong_shibai;
                        break;
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.FrmBar_SNManagement_qiyong_chenggong;
            }
            btnSearchRule_Click(null, null);
        }
        catch (Exception ex)
        {
            msg += Resources.Lang.FrmBar_SNManagement_qiyong_shibai2 + ex.Message;
        }
        Alert(msg);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDisableRule_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_BARCODE_RULE> con = new GenericRepository<BASE_BARCODE_RULE>(context);
        try
        {
            for (int i = 0; i < grdSNRule.Rows.Count; i++)
            {
                var chkSelect = (CheckBox)grdSNRule.Rows[i].Cells[0].FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    string id = this.grdSNRule.DataKeys[i].Values[0].ToString();
                    var modRule = con.Get().Where(x => x.ID == id).FirstOrDefault();
                    if (modRule != null)
                    {
                        if (modRule.STATUS == "1")
                        {
                            bool isInSn = context.BASE_BAR_SN.Any(x => x.CodeRuleId == modRule.ID);
                            bool isInStock = context.STOCK_CURRENT_SN.Any(x => x.RULECODE == modRule.RULECODE);
                            bool isInInbilltemp = context.TEMP_INBILL_D.Any(x => x.RULECODE == modRule.RULECODE);
                            bool isInBill = context.INBILL_D_SN.Any(x => x.RULECODE == modRule.RULECODE);

                            if (!isInSn && !isInStock && !isInInbilltemp && !isInBill)
                            {
                                modRule.STATUS = "2";
                                modRule.ENABLEDATE = DateTime.Now;
                                modRule.ENABLEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                                modRule.MODIFYUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                                modRule.MODIFYDATE = DateTime.Now;
                                con.Update(modRule);
                                con.Save();
                            }
                            else
                            {
                                msg = Resources.Lang.FrmBar_SNManagement_rule_msg + modRule.RULECODE + Resources.Lang.FrmBar_SNManagement_rule_msg3;
                                break;
                            }
                        }
                        else
                        {
                            msg = Resources.Lang.FrmBar_SNManagement_rule_msg + modRule.RULECODE + Resources.Lang.FrmBar_SNManagement_rule_msg2;
                            break;
                        }
                    }
                    else
                    {
                        msg = Resources.Lang.FrmBar_SNManagement_zuofei_shibai;
                        break;
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.FrmBar_SNManagement_zuofei_chenggong;
            }
            btnSearchRule_Click(null, null);
        }
        catch (Exception ex)
        {
            msg += Resources.Lang.FrmBar_SNManagement_zuofei_shibai2 + ex.Message;
        }
        Alert(msg);
    }


    #endregion

    #region 条码维护
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    //网格绑定
    public void GridBind()
    {
        var PgSize = 100;
        IGenericRepository<BASE_BAR_SN> entity = new GenericRepository<BASE_BAR_SN>(context);
        var caseList = from p in entity.Get()
                       orderby p.create_time descending
                       where p.BarCodeType == this.BarCodeType
                       select p;

        if (txtSN.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.sn_code) && x.sn_code.Contains(txtSN.Text));
        if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text))
        {
            DateTime dtFrom = Convert.ToDateTime(txtDCREATETIMEFrom.Text);
            caseList = caseList.Where(x => x.create_time >= dtFrom);
        }
        if (!string.IsNullOrEmpty(txtDCREATETIMETo.Text))
        {
            DateTime dtTo = Convert.ToDateTime(txtDCREATETIMETo.Text).AddHours(23).AddMinutes(59).AddSeconds(59);
            caseList = caseList.Where(x => x.create_time <= dtTo);
        }
        if (!string.IsNullOrEmpty(txtCreateOwner.Text))
        {
            caseList = caseList.Where(x => x.create_owner.Contains(txtCreateOwner.Text));
        }

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = PgSize;          
            grdSNBar.DataSource = GetPageSize(caseList, PgSize, CurrendIndex).ToList();
            grdSNBar.DataBind();
        }
        else
        {
            grdSNBar.DataSource = null;
            grdSNBar.DataBind();
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
        }
        this.GridBind();
        IsFirstPage = true;//恢复默认值
    }



    //网格绑定
    protected void grdSNBar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //ID
            string strKeyID = grdSNBar.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //编辑
            var linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];

            var rule = e.Row.Cells[2].FindControl("txtCodeRuleId") as TextBox;
            //新增部分
            bool isNew = false;
            Guid ruleId;
            if (Guid.TryParse(rule.Text.Trim(), out ruleId))
            {
                isNew = true;
            }


            linkModify.NavigateUrl = "#";
            if (isNew)
            {
                OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBar_CodeMakeEdit.aspx", SYSOperation.Modify, strKeyID + "&RuleType=" + this.BarCodeType), this.GetSubPageTitle("MakeBarCode"), "SnCode");
            }
            else
            {
                OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBar_SNManagementEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBar_SNManagement_PCS_Barcode, "SnCode");
            }

        }
    }

    //删除
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        int count = 0;
        IGenericRepository<BASE_BAR_SN> con = new GenericRepository<BASE_BAR_SN>(context);
        try
        {
            for (int i = 0; i < grdSNBar.Rows.Count; i++)
            {
                var chkSelect = (CheckBox)grdSNBar.Rows[i].Cells[0].FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    con.Delete(this.grdSNBar.DataKeys[i].Values[0].ToString());
                    con.Save();
                    count++;

                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;
            }
            btnSearch_Click(null, null);
        }
        catch (Exception ex)
        {
            msg += Resources.Lang.WMS_Common_Msg_DeleteFailed + ex.Message;
        }
        Alert(msg);
    }

    #endregion

    #region 打印规则
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        PrintDataBind();
    }

    protected void btnSearchPrint_Click(object sender, EventArgs e)
    {
        AspNetPager3.CurrentPageIndex = 1;
        this.PrintDataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    private void PrintDataBind()
    {
        using (var modContext = this.context)
        {
            var queryList = from p in modContext.BASE_BARCODE_PRINT
                            orderby p.CreateTime descending
                            where p.BarCodeType == this.BarCodeType
                            select p;

            if (!string.IsNullOrEmpty(txtPrintCode.Text.Trim()))
            {
                queryList = queryList.Where(x => x.PrintCode.Contains(txtPrintCode.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtPrintName.Text.Trim()))
            {
                queryList = queryList.Where(x => x.PrintName.Contains(txtPrintName.Text.Trim()));
            }
            if (drpPrintStatus.SelectedValue != "")
            {
                queryList = queryList.Where(x => x.Cstatus.Equals(drpPrintStatus.SelectedValue));
            }
            if (!string.IsNullOrEmpty(txtPrintCreateFrom.Text.Trim()))
            {
                DateTime createTimeFrom = Convert.ToDateTime(txtPrintCreateFrom.Text.Trim());
                queryList = queryList.Where(x => x.CreateTime != null && x.CreateTime >= createTimeFrom);
            }
            if (!string.IsNullOrEmpty(txtPrintCreateTo.Text.Trim()))
            {
                DateTime createTimeTo = Convert.ToDateTime(txtPrintCreateTo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.CreateTime != null && x.CreateTime < createTimeTo);
            }
            if (!string.IsNullOrEmpty(txtPrintCreate.Text.Trim()))
            {
                queryList = queryList.Where(x => x.CreateUser.Contains(txtPrintCreate.Text.Trim()));
            }

            if (queryList != null)
            {
                AspNetPager3.RecordCount = queryList.Count();
                AspNetPager3.PageSize = this.PageSize;
            }
        
            //grdSNPrint.DataSource = GetPageSize(queryList, PageSize, AspNetPager3.CurrentPageIndex).ToList();     
            var listResult = GetPageSize(queryList, PageSize, AspNetPager3.CurrentPageIndex).ToList();
            var source = GetGridSourceDataByList(listResult, "Cstatus","RuleStatus");
            grdSNPrint.DataSource = source;
            grdSNPrint.DataBind();
        }
    }

    protected void grdSNPrint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //ID
            string strKeyID = this.GetKeyIDS_Print(e.Row.RowIndex);
            //编辑
            var linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBar_CodePrintEdit.aspx", SYSOperation.Modify, strKeyID + "&RuleType=" + this.BarCodeType), this.GetSubPageTitle("PrintRule"), "SnPrint");

            ////状态
            //if (e.Row.Cells[4].Text.Trim() == "2")
            //{
            //    e.Row.Cells[4].Text = Resources.Lang.WMS_Common_Button_Disable;
            //}
            //else if (e.Row.Cells[4].Text.Trim() == "1")
            //{
            //    e.Row.Cells[4].Text = Resources.Lang.FrmBar_SNManagement_Status_qiyong;
            //}
            //else
            //{
            //    e.Row.Cells[4].Text = Resources.Lang.FrmBar_SNManagement_Status_weiqiyong;
            //}
        }
    }

    private string GetKeyIDS_Print(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdSNPrint.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdSNPrint.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEnablePrint_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_BARCODE_PRINT> con = new GenericRepository<BASE_BARCODE_PRINT>(context);
        try
        {
            for (int i = 0; i < grdSNPrint.Rows.Count; i++)
            {
                var chkSelect = (CheckBox)grdSNPrint.Rows[i].Cells[0].FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    string id = this.grdSNPrint.DataKeys[i].Values[0].ToString();
                    var modPrint = con.Get().Where(x => x.Id == id).FirstOrDefault();
                    if (modPrint != null)
                    {
                        if (modPrint.Cstatus == "0")
                        {
                            List<BASE_BARCODE_PRINT_D> ruleDetails = context.BASE_BARCODE_PRINT_D.Where(x => x.PrintId == modPrint.Id).ToList();
                            if (ruleDetails.Any())
                            {
                                modPrint.Cstatus = "1";
                                modPrint.EnableTime = DateTime.Now;
                                modPrint.EnableUser = WmsWebUserInfo.GetCurrentUser().UserNo;
                                con.Update(modPrint);
                                con.Save();
                            }
                            else
                            {
                                msg = Resources.Lang.FrmBar_SNManagement_rule_msg + modPrint.PrintCode + Resources.Lang.FrmBar_SNManagement_rule_msg1;
                                break;
                            }
                        }
                        else
                        {
                            msg = Resources.Lang.FrmBar_SNManagement_rule_msg + modPrint.PrintCode + Resources.Lang.FrmBar_SNManagement_rule_msg2;
                            break;
                        }
                    }
                    else
                    {
                        msg = Resources.Lang.FrmBar_SNManagement_qiyong_shibai;
                        break;
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.FrmBar_SNManagement_qiyong_chenggong;
            }
            btnSearchPrint_Click(null, null);
        }
        catch (Exception ex)
        {
            msg += Resources.Lang.FrmBar_SNManagement_qiyong_shibai2 + ex.Message;
        }
        Alert(msg);
    }

    protected void btnDisablePrint_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_BARCODE_PRINT> con = new GenericRepository<BASE_BARCODE_PRINT>(context);
        try
        {
            for (int i = 0; i < grdSNPrint.Rows.Count; i++)
            {
                var chkSelect = (CheckBox)grdSNPrint.Rows[i].Cells[0].FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    string id = this.grdSNPrint.DataKeys[i].Values[0].ToString();
                    var modPrint = con.Get().Where(x => x.Id == id).FirstOrDefault();
                    if (modPrint != null)
                    {
                        if (modPrint.Cstatus == "1")
                        {
                            bool isInSn = context.BASE_BAR_SN.Any(x => x.PrintRuleId == modPrint.Id);
                            bool isInCodeRule = context.BASE_BARCODE_RULE.Any(x => x.DefaultPrintId == modPrint.Id);
                            if (!isInSn && !isInCodeRule)
                            {
                                modPrint.Cstatus = "2";
                                modPrint.EnableTime = DateTime.Now;
                                modPrint.EnableUser = WmsWebUserInfo.GetCurrentUser().UserNo;
                                con.Update(modPrint);
                                con.Save();
                            }
                            else
                            {
                                msg = Resources.Lang.FrmBar_SNManagement_rule_msg + modPrint.PrintCode + Resources.Lang.FrmBar_SNManagement_rule_msg3;
                                break;
                            }
                        }
                        else
                        {
                            msg = Resources.Lang.FrmBar_SNManagement_rule_msg + modPrint.PrintCode + Resources.Lang.FrmBar_SNManagement_rule_msg4;
                            break;
                        }
                    }
                    else
                    {
                        msg = Resources.Lang.FrmBar_SNManagement_zuofei_shibai;
                        break;
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.FrmBar_SNManagement_zuofei_chenggong;
            }
            btnSearchPrint_Click(null, null);
        }
        catch (Exception ex)
        {
            msg += Resources.Lang.FrmBar_SNManagement_zuofei_shibai2 + ex.Message;
        }
        Alert(msg);
    }

    #endregion

    public Dictionary<string, string> SelectIdCode
    {
        set { ViewState["SelectIdCode"] = value; }
        get { return ViewState["SelectIdCode"] as Dictionary<string, string>; }
    }

    //打印
    protected void btnprint_Click(object sender, EventArgs e)
    {
        #region

        GetSelectedId();
        if (SelectIdCode.Count == 0)
        {
            Alert(Resources.Lang.FrmBar_SNManagement_Print_title1);
            return;
        }
        string pid = "";

        foreach (var item in SelectIdCode)
        {
            pid = pid + "'" + item.Key + "',";
        }
        if (pid != "")
        {
            pid = pid.Substring(0, pid.Length - 1);
        }

        //GetPrintSN
        DataTable dt = BarQuery.GetPrintSn(pid);
        dt.TableName = "SNList";

        Session["DT"] = dt;

        Session["SNLength"] = "75";

        this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("/Apps/BAR/Form_SNBarCode_Print.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmBar_SNManagement_Print_SN + "','BAR_SN',800,600);");

        #endregion
    }


    public void GetSelectedId()
    {
        try
        {
            if (SelectIdCode == null)
            {
                SelectIdCode = new Dictionary<string, string>();
            }

            foreach (GridViewRow item in this.grdSNBar.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    CheckBox cbo = itemFindControl as CheckBox;

                    //获取ID
                    string id = this.grdSNBar.DataKeys[item.RowIndex][0].ToString();

                    //控件选中且集合中不存在添加
                    if (cbo.Enabled && cbo.Checked && !SelectIdCode.ContainsKey(id))
                    {
                        SelectIdCode.Add(id, this.grdSNBar.DataKeys[item.RowIndex][0].ToString());
                    }
                    //未选中且集合中存在的移除                    
                    else if (!cbo.Checked && SelectIdCode.ContainsKey(id))
                    {
                        SelectIdCode.Remove(id);
                    }
                }
            }
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }

    private string GetSubPageTitle(string pageName)
    {
        switch (this.BarCodeType)
        {
            case "PCS":
                if (pageName == "BarCodeRule")
                {
                    return "PCS条码规则";
                }
                else if (pageName == "PrintRule")
                {
                    return "PCS打印信息";
                }
                else if (pageName == "MakeBarCode")
                {
                    return "PCS条码维护";
                }
                break;
            case "CARTON":
                if (pageName == "BarCodeRule")
                {
                    return "箱条码规则";
                }
                else if (pageName == "PrintRule")
                {
                    return "箱打印信息";
                }
                else if (pageName == "MakeBarCode")
                {
                    return "箱条码维护";
                }
                break;
            case "PALLET":
                if (pageName == "BarCodeRule")
                {
                    return "栈板条码规则";
                }
                else if (pageName == "PrintRule")
                {
                    return "栈板打印信息";
                }
                else if (pageName == "MakeBarCode")
                {
                    return "栈板条码维护";
                }
                break;
        }
        return "";

    }
}