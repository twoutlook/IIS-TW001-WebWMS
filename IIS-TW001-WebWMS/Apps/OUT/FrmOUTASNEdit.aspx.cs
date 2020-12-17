using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Text;
using DreamTek.ASRS.Business.Others;
using DreamTek.ExternalService.NCS;

/// <summary>
/// 出库通知单编辑页面
/// </summary>
public partial class OUT_FrmOUTASNEdit : WMSBasePage
{

    #region 页面属性

    string OutAsn_Ids = string.Empty;

    public string Status
    {
        get
        {
            if (ViewState["Status"] != null)
            {
                return ViewState["Status"].ToString();
            }
            return "";
        }
        set { ViewState["Status"] = value; }
    }

    /// <summary>
    /// 数据来源 ( 0 : WMS，1 :oracle ERP )
    /// </summary>
    public string CDEFINE2
    {
        get
        {
            if (ViewState["CDEFINE2"] != null)
            {
                return ViewState["CDEFINE2"].ToString();
            }
            return "";
        }
        set { ViewState["CDEFINE2"] = value; }
    }

    public string OutType
    {
        get
        {
            if (ViewState["OutType"] != null)
            {
                return ViewState["OutType"].ToString();
            }
            return "";
        }
        set { ViewState["OutType"] = value; }
    }

    //是否是特殊元件领料
    public decimal IsSpecialWIP_Issue
    {
        get
        {
            if (ViewState["IsSpecialWIP_Issue"] != null)
            {
                return Convert.ToDecimal(ViewState["IsSpecialWIP_Issue"]);
            }
            return 0;
        }
        set { ViewState["IsSpecialWIP_Issue"] = value; }
    }

    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }

    public bool IsQuery
    {
        get
        {
            if (ViewState["IsQuery"] != null)
            {
                return bool.Parse(ViewState["IsQuery"].ToString());
            }
            return false;
        }
        set { ViewState["IsQuery"] = value; }
    }
    #endregion

    #region 页面加载

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtOutAsnCTICKETCODE.Enabled = false;
        this.ShowBASE_CLIENTDiv1.SetCompName = this.txtCCLIENT.ClientID;
        this.ShowBASE_CLIENTDiv1.SetORGCode = this.txtCCLIENTCODE.ClientID;
        this.ShowFG_CinvCode_Div1.SetCINVCODE = txt_FG_CinvCode.ClientID;

        if (this.IsPostBack == false)
        {   
            this.InitPage();

            if (this.Operation() == SYSOperation.Modify)
            {
                ShowData();
                txtITYPE.Enabled = false;
                drpWorkType.Enabled = false;
                this.ddl_Is_Whole.Enabled = false;
            }
            else if (this.Operation() == SYSOperation.Preserved1)
            {
                ShowData();
                this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmOUTASN_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID + "&IsSpecialWIP_Issue=" + IsSpecialWIP_Issue + "','','OUTASN_D',1000,500);");
            }
            else
            {
                this.txtCCREATEOWNERCODE.Text =  OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
                this.txtID.Text = Guid.NewGuid().ToString();
                this.txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                ddlReason.Enabled = false;
                btnISplit.Visible = false;
                //雪龙没有平库，所以锁定立库，并且不能编辑
                drpWorkType.SelectedValue = "1";
                this.drpWorkType.Enabled = false;

                //Note by Qamar 2020-11-11
                //台惟鎖定"其他出庫", 並且不能編輯
                txtITYPE.SelectedValue = "206";
                txtITYPE_TextChanged(txtITYPE, null);
                txtITYPE.Enabled = false;

                //Note by Qamar 2020-12-07
                //台惟預設"工單發料"
                ddlReason.SelectedIndex = 3;
            }
            //检查时存在出库通知单明细，存在则不允许修改出库通知单类型和ErpCode
            if (context.OUTASN_D.Where(x => x.id == txtID.Text.Trim()).Any())
            {
                txtITYPE.Enabled = false;
                txtCERPCODE.Enabled = false;
                drpWorkType.Enabled = false;
            }
          
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
            this.btnImportExcel.Enabled = false;
        }

        btBomInput();
      
        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
        this.btnCreateOutBill.Attributes["onclick"] = this.GetPostBackEventReference(this.btnCreateOutBill) + ";this.disabled=true;";
        this.btnCreateTemporary.Attributes["onclick"] = this.GetPostBackEventReference(this.btnCreateTemporary) + ";this.disabled=true;";
    }

    /// <summary>
    /// 页面初始化
    /// </summary>
    public void InitPage()
    {
        try
        {
            if (IsSpecialWIP_Issue == 0)
            {
                IsSpecialWIP_Issue = Convert.ToDecimal(Request.QueryString["IsSpecialWIP_Issue"]);
            }
        }
        catch (Exception)
        {

        }

        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTASN');return false;";

        //var paraList = context.SYS_PARAMETER.Where(x => x.flag_type == "OS").OrderBy(x => x.flag_id).ToList();
       // Help.DropDownListDataBind(paraList, this.drCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");

        Help.DropDownListDataBind(SysParameterList.GetList("", "", "OS", false, -1, -1), this.drCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "WorkType", false, -1, -1), this.drpWorkType, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        //分批整批
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "Is_Whole", false, -1, -1), this.ddl_Is_Whole, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");

        if (IsSpecialWIP_Issue == 1)
        {
            ltPageTable.Text = Resources.Lang.FrmOUTASNEdit_TeShuYuanJian;
            Help.DropDownListDataBind(this.GetOutTypeBySpecialIssue2(false), this.txtITYPE, Resources.Lang.FrmOUTASNEdit_TypeSelect, "typename", "cerpcode", "");
        }
        else
        {
            if (this.Operation() == SYSOperation.New)
            {
                Help.DropDownListDataBind(GetOutType(false), this.txtITYPE, Resources.Lang.FrmOUTASNEdit_TypeSelect, "FUNCNAME", "EXTEND1", "");
                //理由码
                Help.DropDownListDataBind(GetReasonCode("2", false), this.ddlReason, Resources.Lang.FrmINASNEdit_MSG3, "REASONCONTENT", "REASONCODE", "");
            }
            else
            {
                Help.DropDownListDataBind(GetOutType(true), this.txtITYPE, Resources.Lang.FrmOUTASNEdit_TypeSelect, "FUNCNAME", "EXTEND1", "");
                //加上来源于ERP的理由码
                Help.DropDownListDataBind(GetReasonCode("2", true), this.ddlReason, Resources.Lang.FrmINASNEdit_MSG3, "REASONCONTENT", "REASONCODE", "");
            }
        }

        this.txtCCLIENT.Attributes["onclick"] = "Show('" + ShowBASE_CLIENTDiv1.GetDivName + "');";
        this.txtCCLIENTCODE.Attributes["onclick"] = "Show('" + ShowBASE_CLIENTDiv1.GetDivName + "');";

        //删除确认提示
        if (this.Operation() == SYSOperation.Modify || this.Operation() == SYSOperation.Preserved1)
        {
            this.btnDelete.Enabled = true;
        }
        else
        {
            this.btnDelete.Enabled = false;
        }
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.Operation() == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.FrmOUTASNEdit_ShengPi;
        }

        //Help.DropDownListDataBind(GetReason(), this.ddlReason, Resources.Lang.FrmOUTASNEdit_LiYouMaSelect, "FUNCNAME", "EXTEND1", "");     
       
       
    }

    #endregion

    #region 页面事件

    /// 出库类型变更事件
    /// <summary>
    /// 出库类型变更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtITYPE_TextChanged(object sender, EventArgs e)
    {
        string Itype = txtITYPE.SelectedValue;
        //一般销货
        if (Itype == "202")
        {
            ddl_Is_Whole.Enabled = true;
        }
        else
        {
            ddl_Is_Whole.SelectedValue = "0";
            ddl_Is_Whole.Enabled = false;
        }
        //工单发料
        if (Itype == "203")
        {
            ltSearch.Text = @"<img alt='' onclick=""disponse_div(event,document.all('ctl00_ContentPlaceHolderMain_ShowFG_CinvCode_Div1_ajaxWebSearChComp'));""
                                    src=""../../Images/Search.gif"" class=""select"" />";
            txt_FG_CinvCode.Enabled = true;
            txt_FG_Qty.Enabled = true;
        }
        else
        {
            ltSearch.Text = "";
            txt_FG_CinvCode.Text = "";
            txt_FG_Qty.Text = "";
            txt_FG_CinvCode.Enabled = false;
            txt_FG_Qty.Enabled = false;
        }
        if (Itype == "206")
        { //其他出库
            ddlReason.Enabled = true;
        }
        else {
            ddlReason.Enabled = false;
            ddlReason.SelectedValue = "";
        }

        var paraList = context.BASE_TypeMapping.Where(x => x.WMS_TypeCode == Itype && x.type == "OutBillNo").OrderBy(x => x.ERP_TypeCode).ToList();
        Help.DropDownListDataBind(paraList, this.drpBillNo, Resources.Lang.FrmOUTASNEdit_QingXuanZe, "ERP_TypeName", "ERP_TypeCode", "");
    }

    /// 新增明细
    /// <summary>   
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmOUTASNEdit.aspx?" + BuildQueryString(SYSOperation.Preserved1, txtID.Text.Trim() + "&IsSpecialWIP_Issue=" + IsSpecialWIP_Issue));//
    }

    /// 查询按钮事件
    /// <summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;//默认第一页
            AspNetPager1.CurrentPageIndex = 1;
        }
        IsFirstPage = true;
        this.GridBind();
    }

    /// 保存输入内容到数据库
    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Note by Qamar 2020-11-23
        if (txtCERPCODE.Text.Trim() == "")
            txtCERPCODE.Text = DateTime.Now.ToString("yyyyMMddHHmmss");

        try
        {
            btnSave.Enabled = false;
            SaveTODB(sender);
            btBomInput();
            btnSave.Enabled = true;
            this.btnSave.Style.Remove("disabled");
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }

    /// 删除明细
    /// <summary>
    /// 删除明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            for (int i = 0; i < this.grdOUTASN_D.Rows.Count; i++)
            {
                if (this.grdOUTASN_D.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdOUTASN_D.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        IGenericRepository<OUTASN_D> con = new GenericRepository<OUTASN_D>(context);
                        var caseList = from p in con.Get().AsEnumerable()
                                       where p.ids == this.grdOUTASN_D.DataKeys[i].Values[0].ToString()
                                       select p;
                        OUTASN_D entity = caseList.ToList().FirstOrDefault();

                        //料号
                        string cinvcode = entity.cinvcode;
                        string erpCodeLine = entity.cerpcodeline;

                        //未生成过出库单及未生成指引
                        if (OutAsnQuery.ValidateIsCreateOutBill(cinvcode, erpCodeLine, txtID.Text.Trim())
                            && !OutAsnQuery.CheckIsExistOutAssitByOutAsn_Id(txtID.Text.Trim()))
                        {                          
                            if (entity.cstatus.Equals("0"))
                            {
                                con.Delete(entity.ids);
                                con.Save();
                            }
                            else
                            {
                                string ErpCode = string.Empty;
                                if (erpCodeLine != "&nbsp;")
                                {
                                    ErpCode = erpCodeLine;
                                }
                                msg += Resources.Lang.FrmOUTASNEdit_Tips_LiaoHao + "[" + cinvcode + "]ERP" + Resources.Lang.WMS_Common_Element_XiangCi + "[" + ErpCode + "]" + Resources.Lang.FrmOUTASNEdit_Tips_StatusCuoWu + "！\r\n";
                            }
                        }
                        else
                        {
                            string ErpCode = string.Empty;
                            if (erpCodeLine != "&nbsp;")
                            {
                                ErpCode = erpCodeLine;
                            }
                            msg += Resources.Lang.FrmOUTASNEdit_Tips_LiaoHao + "[" + cinvcode + "]ERP" + Resources.Lang.WMS_Common_Element_XiangCi + "[" + ErpCode + "]" + Resources.Lang.FrmOUTASNEdit_Tips_YiShengCheng + "！\r\n";
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;
            }
            else
            {
                msg = Resources.Lang.WMS_Common_Msg_DeleteFailed + "\r\n" + msg;
            }
            this.Alert(msg);
            this.GridBind();
        }
        catch (Exception ex)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + ex.Message.ToString());
        }
    }

    /// <summary>
    /// 生成出库单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateOutBill_Click(object sender, EventArgs e)
    {
        GetSelectedIds();
        string outBill_id = Guid.NewGuid().ToString();
        string msg = string.Empty;
        if (SelectIds.Count > 0)
        {
            IGenericRepository<OUTASN_D> conn = new GenericRepository<OUTASN_D>(context);
            foreach (var item in SelectIds.Values)
            {
                var caseList = from p in conn.Get()
                               where p.ids == item.Trim()
                               select p;
                OUTASN_D OutAsn_d = caseList.ToList().FirstOrDefault();
                //20130731134649
                var result = OutAsnQuery.CanModDebit(txtCERPCODE.Text.Trim(), OutAsn_d.cinvcode, "0", "", this.txtITYPE.SelectedValue.Trim(), "");
                if (!result.Equals("1"))
                {
                    Alert(result);
                    return;
                }

                //20131105102731 通知单存在修改中的料时，不允许生成出库单
                result = OutAsnQuery.CanAsnDebit(txtOutAsnCTICKETCODE.Text.Trim(), OutAsn_d.cinvcode, "0", "", "");
                if (!result.Equals("OK"))
                {
                    Alert(result);
                    return;
                }
            }
            try
            {
                IGenericRepository<TEMP_SELECTASND> con = new GenericRepository<TEMP_SELECTASND>(context);
                foreach (var item in SelectIds.Values)
                {
                    TEMP_SELECTASND entity = new TEMP_SELECTASND();
                    entity.asn_ids = item.Trim();
                    entity.asn_id = this.KeyID;
                    entity.userno = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.guid = outBill_id;
                    entity.createdate = DateTime.Now;
                    entity.ID = Guid.NewGuid().ToString();
                    con.Insert(entity);

                }
                con.Save();
            }
            catch
            {
            }
            var result1 = OutAsnQuery.CanModDebit(txtCERPCODE.Text, "", "2", this.KeyID, "", "");
            if (!result1.Equals("1"))
            {
                Alert(result1);
                return;
            }

            #region 调用存储过程
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_OutAsn_id:" + this.KeyID);
            SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
            SparaList.Add("@P_OutBill_Id:" + outBill_id);
            SparaList.Add("@P_IsTemporary:" + "0");
            SparaList.Add("@P_ReturnValue:" + "");
            SparaList.Add("@INFOTEXT:" + "");
            string[] results = DBHelp.ExecuteProc("Proc_CreateOutBill", SparaList);
            if (results.Length == 1)//调用存储过程有错误
            {
                msg = results[0].ToString();
            }
            else if (results[0] == "0")
            {
                //成功
            }
            else
                msg = results[1].ToString();
            #endregion

            if (msg.Length == 0)
            {
                btnCreateTemporary.Style.Remove("disabled");
                btnCreateOutBill.Style.Remove("disabled");
                //生成成功 跳转到出库单页面
                this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmOUTBILLEdit.aspx", SYSOperation.Modify, outBill_id) + "','" + Resources.Lang.FrmOUTASNList_Tips_ChuKuDan + "','OUTBILL');");
            }
        }
        else
        {
            msg = Resources.Lang.FrmOUTASNEdit_Tips_QingXuanZe;
            btnCreateOutBill.Style.Remove("disabled");
            btnCreateTemporary.Style.Remove("disabled");
        }
        if (msg.Length > 0)
        {
            Alert(msg);
        }
    }


    /// <summary>
    /// 生成暂存出库单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateTemporary_Click(object sender, EventArgs e)
    {
        GetSelectedIds();
        string outBill_id = Guid.NewGuid().ToString();
        string msg = string.Empty;
        if (SelectIds.Count > 0)
        {
            IGenericRepository<OUTASN_D> conn = new GenericRepository<OUTASN_D>(context);
            foreach (var item in SelectIds.Values)
            {
                var caseList = from p in conn.Get()
                               where p.ids == item.Trim()
                               select p;
                OUTASN_D OutAsn_d = caseList.ToList().FirstOrDefault();
                //20130731134649
                var result = OutAsnQuery.CanModDebit(txtCERPCODE.Text.Trim(), OutAsn_d.cinvcode, "0", "", this.txtITYPE.SelectedValue.Trim(), "");
                if (!result.Equals("1"))
                {
                    Alert(result);
                    return;
                }

                //20131105102731 通知单存在修改中的料时，不允许生成出库单
                result = OutAsnQuery.CanAsnDebit(txtOutAsnCTICKETCODE.Text.Trim(), OutAsn_d.cinvcode, "0", "", "");
                if (!result.Equals("OK"))
                {
                    Alert(result);
                    return;
                }
            }
            try
            {
                IGenericRepository<TEMP_SELECTASND> con = new GenericRepository<TEMP_SELECTASND>(context);
                foreach (var item in SelectIds.Values)
                {
                    TEMP_SELECTASND entity = new TEMP_SELECTASND();
                    entity.asn_ids = item.Trim();
                    entity.asn_id = this.KeyID;
                    entity.userno = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.guid = outBill_id;
                    entity.createdate = DateTime.Now;
                    entity.ID = Guid.NewGuid().ToString();
                    con.Insert(entity);

                }
                con.Save();
            }
            catch
            {
            }
            var result1 = OutAsnQuery.CanModDebit(txtCERPCODE.Text, "", "2", this.KeyID, "", "");
            if (!result1.Equals("1"))
            {
                Alert(result1);
                return;
            }

            #region 调用存储过程
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_OutAsn_id:" + this.KeyID);
            SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
            SparaList.Add("@P_OutBill_Id:" + outBill_id);
            SparaList.Add("@P_IsTemporary:" + "1");
            SparaList.Add("@P_ReturnValue:" + "");
            SparaList.Add("@INFOTEXT:" + "");
            string[] results = DBHelp.ExecuteProc("Proc_CreateOutBill", SparaList);
            if (results.Length == 1)//调用存储过程有错误
            {
                msg = results[0].ToString();
            }
            else if (results[0] == "0")
            {
                //成功
            }
            else
                msg = results[1].ToString();
            #endregion

            if (msg.Length == 0)
            {
                btnCreateTemporary.Style.Remove("disabled");
                btnCreateOutBill.Style.Remove("disabled");
                //生成成功 跳转到出库单页面
                this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmOUTBILLEdit.aspx", SYSOperation.Modify, outBill_id) + "','" + Resources.Lang.FrmOUTASNList_Tips_ChuKuDan + "','OUTBILL');");
            }
        }
        else
        {
            msg = Resources.Lang.FrmOUTASNEdit_Tips_QingXuanZe;
            btnCreateOutBill.Style.Remove("disabled");
            btnCreateTemporary.Style.Remove("disabled");
        }
        if (msg.Length > 0)
        {
            Alert(msg);
        }
    }


    /// 通知单拆解
    /// <summary>
    /// 通知单拆解
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDisassembly_Click(object sender, EventArgs e)
    {
        bool Check_IsOk = true;
        //判断选中几条记录
        var CheckCount = (from GridViewRow item in this.grdOUTASN_D.Rows
                          let itemFindControl = item.FindControl("chkSelect")
                          where itemFindControl != null && itemFindControl is CheckBox
                          let cbo = itemFindControl as CheckBox
                          where cbo.Checked
                          select item).Count();
        switch (CheckCount)
        {
            case 0:
                Alert(Resources.Lang.FrmOUTASNEdit_Tips_LiaohaoXuanZe);
                return;
            default:
                if (CheckCount > 1)
                {
                    Alert(Resources.Lang.FrmOUTASNEdit_Tips_DanChai);
                    return;
                }
                break;
        }

        OutAsn_Ids = "";
        foreach (GridViewRow item in from GridViewRow item in this.grdOUTASN_D.Rows
                                     let itemFindControl = item.FindControl("chkSelect")
                                     where itemFindControl != null && itemFindControl is CheckBox
                                     let cbo = itemFindControl as CheckBox
                                     where cbo.Checked
                                     select item)
        {
            //获取ID
            OutAsn_Ids = this.grdOUTASN_D.DataKeys[item.RowIndex][0].ToString();
            if (!OutAsnQuery.CheckWIP_CL_PARTINFO(this.grdOUTASN_D.DataKeys[item.RowIndex][1].ToString().Trim(), "CJ"))
            {
                this.Alert("[" + this.grdOUTASN_D.DataKeys[item.RowIndex][1].ToString().Trim() + "]" + Resources.Lang.FrmOUTASNEdit_Tips_LiaoHaoBuNengChai);
                Check_IsOk = false;
            }
            //20130731134649 
            var result = OutAsnQuery.CanModDebit(item.Cells[8].Text.Trim(), item.Cells[1].Text.Trim(), "0", "", this.txtITYPE.SelectedValue.Trim(), "");
            if (!result.Equals("1"))
            {
                Alert(result);
                return;
            }

            //20131105102731 通知单存在修改中的料时，不允许生成出库单
            result = OutAsnQuery.CanAsnDebit(txtOutAsnCTICKETCODE.Text.Trim(), item.Cells[1].Text.Trim(), "0", "", "");
            if (!result.Equals("OK"))
            {
                Alert(result);
                return;
            }
            break;
        }

        if (Check_IsOk)
        {
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmOUTASNEdit_Disassembly.aspx", SYSOperation.New, txtID.Text.Trim()) + "&IDS=" + OutAsn_Ids + "','','FrmOUTASNEdit_Disassembly',600,320);");
        }
    }

    /// <summary>
    /// 通知单拆解
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnISplit_Click(object sender, EventArgs e)
    {
        this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmOUTASNSplit.aspx", SYSOperation.New, txtID.Text.Trim()) + "','','FrmOUTASNSplit',1000,650);");
    }

    /// 打印按钮
    /// <summary>
    /// 打印按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string printid = this.txtID.Text.Trim();
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmOUTASNEdit_Print.aspx", SYSOperation.New, printid) + "','" + Resources.Lang.FrmOUTASNEdit_Tips_DaYingChuKu + "','BAR_REPACK',800,600);");
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInBom_Click(object sender, EventArgs e)
    {
        BomSave();
    }

    /// <summary>
    /// 分页控件变更页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    /// <summary>
    /// 列表行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdOUTASN_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            LinkButton linkModify = (LinkButton)e.Row.FindControl("LinkButton1");
            //WIP Issue : 工单发料 203 -35 、Sales order issue ：一般销货202-33
            if ((Status.Length > 0 && !Status.Equals("0")) || OutType.Equals("203"))
            {
                linkModify.Enabled = false;
            }
            //WMS创建的未处理数据可以编辑
            if ((Status.Length > 0 && Status.Equals("0")) && CDEFINE2.Equals("0"))
            {
                linkModify.Enabled = true;
            }
            else
            {
                linkModify.Enabled = false;
            }
            //特殊元件领料
            try
            {
                if (OutType.Equals("203") && Request.QueryString["IsSpecialPage"].Equals("0") && IsSpecialWIP_Issue == 1)//IsSpecialPage
                {
                    linkModify.Enabled = false;
                }

                //Roger 2013-5-2 17:48:49 合并后通知单不允许补单
                if (!OutAsnQuery.IsMerge(KeyID))
                {
                    linkModify.Enabled = false;
                }
                if (IsQuery) //1303 拼板出库 1302 库存调整出库 //this.txtITYPE.SelectedValue.Trim().Equals("1303") || this.txtITYPE.SelectedValue.Trim().Equals("1302")
                {
                    linkModify.Enabled = false;
                }
            }
            catch (Exception)
            {

            }
            //获取ID
            CheckBox cbo = e.Row.FindControl("chkSelect") as CheckBox;

            //判断是否已在SelectIds集合中
            if (SelectIds.ContainsKey(strKeyID))
            {
                //如果是控件处于选中状态
                cbo.Checked = true;
            }
        }
    }

    /// <summary>
    /// 列表行编辑事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string ids = (sender as LinkButton).CommandArgument;

        switch (txtITYPE.SelectedValue)
        {
            //收货退回 201-36
            case "201":
                hfInAsn_Id.Value = OutAsnQuery.GetInAsnIdByErpNo(txtCERPCODE.Text.Trim());
                if (hfInAsn_Id.Value.Length > 0)
                {
                    Session["inAsn_id"] = hfInAsn_Id.Value;
                }
                break;
            default:
                break;
        }
        this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmOUTASN_DEdit.aspx", SYSOperation.Modify, ids) + "','" + Resources.Lang.FrmOUTASNList_Menu_PageName + "','OUTASN_D',1000,500);");//+ "&IsSpecialWIP_Issue=" + IsSpecialWIP_Issue
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdOUTASN_D_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }

    protected void btnSync_Click(object sender, EventArgs e)
    {
        string id = this.KeyID;
        var outasn = context.OUTASN.Where(x => x.id == id).FirstOrDefault();
        if (outasn == null)
        {
            this.Alert("通知单数据异常！");
            return;
        }

        if (outasn.cstatus != "1" && outasn.cstatus != "2" && outasn.cstatus != "3")
        {
            this.Alert("当前通知单不能进行该操作！");
            return;
        }
        string msg = string.Empty;
        bool isSuccess = new OutAsnSync().OutAsnSyncById(id, out msg);
        if (isSuccess)
        {
            this.AlertAndBack("FrmOUTASNEdit.aspx?" + BuildQueryString(SYSOperation.Modify, id + "&IsSpecialPage=" + Request.QueryString["IsSpecialPage"] + "&IsSpecialWIP_Issue=" + this.IsSpecialWIP_Issue), "抛转成功！");
        }
        else
        {
            this.Alert(msg);
        }
    }


    #endregion

    #region 页面方法

    /// <summary>
    /// 获取列表对应列id
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <returns></returns>
    private string GetKeyIDS(int rowIndex)
    {
        return this.grdOUTASN_D.DataKeys[rowIndex].Values[0].ToString();
    }

    /// <summary>
    /// 列表数据绑定
    /// </summary>
    public void GridBind()
    {
        using (var modContext = this.context)
        {
            var queryList = from p in modContext.V_OUTASN_DL
                            orderby p.lineid
                            where 1 == 1
                            select p;
            if (txtID.Text != string.Empty)
            {
                queryList = queryList.Where(x => x.id.ToString().Equals(txtID.Text));
            }
            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
            {
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));
            }

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;

            var data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();

            List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
            flagList.Add(new Tuple<string, string>("cstatus", "OASN_D_STATUS"));

            var srcdata = GetGridSourceDataByList(data, flagList);
           
            //NOTE by Mark, 09/19
            srcdata=GetGridSourceData_PART_RANK(srcdata);

            this.grdOUTASN_D.DataSource = srcdata;//GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            this.grdOUTASN_D.DataBind();
            //检查时存在出库通知单明细，存在则不允许修改出库通知单类型和ErpCode
            if (queryList != null && queryList.Count() > 0)
            {
                txtITYPE.Enabled = false;
                txtCERPCODE.Enabled = false;
            }
        }
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        OUTASN entity = this.context.OUTASN.Where(x => x.id == this.KeyID).FirstOrDefault();
        if (entity == null)
        {
            return;
        }
        
        this.txtID.Text = entity.id;
        //根据当前单据号ID而确认当前单据是否可以进行编辑操作      
        OUTTYPE OUTASN_type = GetOUTTYPEByID(this.txtID.Text, "OUTASN");
        if (OUTASN_type != null && !string.IsNullOrEmpty(OUTASN_type.Is_Query.ToString()))
        {
            IsQuery = OUTASN_type.Is_Query.ToString() == "1" ? true : false;
        }
        else
        {
            IsQuery = false;
        }
        //根据当前单据号ID而确认当前单据是否可以进行编辑操作      
        this.txtOutAsnCTICKETCODE.Text = entity.cticketcode;
        this.drCSTATUS.SelectedValue = entity.cstatus;
        this.txtITYPE.SelectedValue = entity.itype.ToString();
        this.txtCCLIENTCODE.Text = entity.cclientcode;
        this.txtCCLIENT.Text = entity.cclient;
        this.txtCSO.Text = entity.cso;
        this.txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(entity.ccreateownercode);
        this.txt_FG_CinvCode.Text = entity.fg_cinvcode;
        this.txt_FG_Qty.Text = Convert.ToDecimal(entity.fg_qty).ToString("f2");
        this.ddlReason.SelectedValue = entity.reasoncode;
        if (entity.itype == 206)
        {
            ddlReason.Enabled = true;
        }
        else {
            ddlReason.Enabled = false;
        }

        if (entity.dcreatetime != null)
        {
            this.txtDCREATETIME.Text = entity.dcreatetime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        this.txtCERPCODE.Text = entity.cerpcode;
        this.txtCAUDITPERSONCODE.Text = entity.cauditpersoncode;
        this.txtDAUDITDATE.Text = entity.dauditdate.HasValue ? entity.dauditdate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
        this.txtCMEMO.Text = entity.cmemo;
        this.txtCDEFINE1.Text = entity.cdefine1;
        TabMain0.Visible = true;
        Status = entity.cstatus;
        CDEFINE2 = entity.cdefine2;
        IsSpecialWIP_Issue = entity.idefine5.HasValue ? entity.idefine5.Value : 0;
        OutType = entity.itype.ToString();
        this.btnImportExcel.Enabled = true;
        this.drpWorkType.SelectedValue = entity.worktype;
        ddl_Is_Whole.SelectedValue = entity.is_whole.HasValue ? entity.is_whole.ToString() : "0";

        var paraList = context.BASE_TypeMapping.Where(x => x.WMS_TypeCode == entity.itype.ToString() && x.type == "OutBillNo").OrderBy(x => x.ERP_TypeCode).ToList();
        Help.DropDownListDataBind(paraList, this.drpBillNo, Resources.Lang.FrmOUTASNEdit_QingXuanZe, "ERP_TypeName", "ERP_TypeCode", "");
        this.drpBillNo.SelectedValue = entity.billno;
        this.drpBillNo.Enabled = false;


        if (entity.cstatus == "3") {
            btnSync.Visible = true;
        }


        //33 Sales Order Issue 202
        if (!entity.cstatus.Equals("0") || !entity.cdefine2.Equals("0"))//|| OutType.Equals("202")取消一般销货的卡控 CQ 2015-3-25 10:41:16
        {
            SetTblFormControlEnabled(false);
        }
        //btnCreateTemporary.Enabled = false;
        //btnCreateOutBill.Enabled = false;
        //百亨平移 BUCKINGHA-997
        if (entity.cstatus.Equals("0"))
        {
            btnCreateTemporary.Enabled = true;
            btnCreateOutBill.Enabled = true;
        }
        else
        {
            btnCreateTemporary.Enabled = false;
            btnCreateOutBill.Enabled = false;
        }

        //状态为 未处理 并且 数据来原为 1 [数据来源 ( 0 : WMS，1 :oracle ERP )]的生成出库单按钮可用
        if (entity.cstatus.Equals("0") && entity.cdefine2.Equals("1"))
        {
            btnCreateOutBill.Enabled = true;
            btnCreateTemporary.Enabled = true;
        }
        else
        {
            //检查其是否存在0未处理 4补单中的明细，存在则显示
            if (OutAsnQuery.CheckAsn_DStatus(txtID.Text.Trim()))
            {
                btnCreateOutBill.Enabled = true;
                btnCreateTemporary.Enabled = true;
            }
        }
       
        //WIP Issue : 35 、Sales order issue ：33-202
        if (string.IsNullOrEmpty(entity.cdefine2)/*Roegr 20130509 增加判空处理*/ || !entity.cdefine2.Equals("0"))//|| OutType.Equals("202")取消一般销货的卡控 CQ 2015-3-25 10:41:16
        {
            this.btnDelete.Enabled = false;
            this.btnNew.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnImportExcel.Enabled = false;
        }
        //Return to Vendor 36--201收货退回不允许导入
        if (OutType.Equals("201"))
        {
            this.btnImportExcel.Enabled = false;
        }
        //特殊元件领料
        try
        {
            //工单发料35  --203
            if (OutType.Equals("203") && Request.QueryString["IsSpecialPage"].Equals("0") && IsSpecialWIP_Issue == 1)//IsSpecialPage
            {
                SetTblFormControlEnabled(false);
            }
        }
        catch (Exception)
        {

        }
        this.btnImportExcel.Attributes["onclick"] = "PopupFloatWin('" + BuildRequestPageURL("/Apps/Import/FrmImportAsnDetail.aspx", SYSOperation.New, "") + "&AsnId=" + this.KeyID + "&CTICKETCODE=" + txtOutAsnCTICKETCODE.Text.Trim() + "&ImportType=Out','" + Resources.Lang.FrmOUTASNEdit_Tips_ShangChuanMingXi + "','OUTASN_D',600,320); return false;";
        //WIP Completion Return 工单完工退 17--204
        if (OutType.Equals("204"))
        {
            SetTblFormControlEnabled(false);
        }

        //Roger 2013-5-2 17:48:49 合并后通知单不允许补单
        if (context.OUTASN.Any(x=>x.is_merge == 1 && x.id == KeyID))
        {
            btnDelete.Enabled = false;
            btnNew.Enabled = false;
            btnSave.Enabled = false;
            btnCreateOutBill.Enabled = false;
            btnCreateTemporary.Enabled = false;
            btnImportExcel.Enabled = false;
        }

        //20130617112810
        //特殊超领的，啥都不允许操作
        if (entity.special_out.Equals("1"))
        {
            SetTblFormControlEnabled(false);
            btnSave.Enabled = true;
        }

        if (entity.cstatus == "0")
        {
            btnISplit.Enabled = true;
        }
        else {
            btnISplit.Enabled = false;
        }
        //拼板出库的只能查看信息，不可做任何操作
        if (IsQuery) //1303 拼板出库 1302 库存调整出库 //this.txtITYPE.SelectedValue.Trim().Equals("1303") || this.txtITYPE.SelectedValue.Trim().Equals("1302")
        {
            btnDelete.Enabled = false;
            btnNew.Enabled = false;
            btnSave.Enabled = false;
            btnCreateOutBill.Enabled = false;
            btnCreateTemporary.Enabled = false;
            btnImportExcel.Enabled = false;
            btnISplit.Enabled = false;
        }

        //接口过来的单据不能修改，不能删除，不能增加子项
        if (entity.cdefine2.Equals("1") || entity.is_merge == 1) {
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnNew.Enabled = false;
        }

    }

    /// <summary>
    /// 设置控件可用性
    /// </summary>
    /// <param name="value"></param>
    private void SetTblFormControlEnabled(bool value)
    {
        this.txtCAUDITPERSONCODE.Enabled = value;
        this.txtCCLIENT.Enabled = value;
        this.txtCCLIENTCODE.Enabled = value;
        this.txtCCREATEOWNERCODE.Enabled = value;
        this.txtCERPCODE.Enabled = value;
        this.txtCSO.Enabled = value;
        this.txtOutAsnCTICKETCODE.Enabled = value;
        this.txtDAUDITDATE.Enabled = value;
        this.txtDCREATETIME.Enabled = value;
        this.txtITYPE.Enabled = value;
        this.drCSTATUS.Enabled = value;
        txtCCLIENTCODE.Attributes.Remove("onclick");
        txtCCLIENT.Attributes.Remove("onclick");
        btnDelete.Enabled = value;
        btnNew.Enabled = value;
        btnSave.Enabled = value;
        btnCreateOutBill.Enabled = value;
        btnCreateTemporary.Enabled = value;
        btnImportExcel.Enabled = value;
        txtCDEFINE1.Enabled = value;
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        #region 字段非空，长度检查
        ASCIIEncoding strData = new ASCIIEncoding();
        string msg = string.Empty;
        //
        if (this.txtID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_IdBuNengKong);
            this.SetFocus(txtID);
            return false;
        }
        //
        if (this.txtID.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtID.Text).Length > 50)
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_IdChangDu);
                this.SetFocus(txtID);
                return false;
            }
        }

        if (this.txtITYPE.SelectedValue.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_LeiXingXuanze);
            this.SetFocus(txtITYPE);
            return false;
        }

        if (this.txtCCLIENTCODE.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCCLIENTCODE.Text).Length > 50)
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_KeHuChangDu);
                this.SetFocus(txtCCLIENTCODE);
                return false;
            }
        }
        //
        if (this.txtCCLIENT.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCCLIENT.Text).Length > 150)
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_KeHuMingChangDu);
                this.SetFocus(txtCCLIENT);
                return false;
            }
        }
        //
        if (this.txtCSO.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCSO.Text).Length > 30)
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_SoChangDu);
                this.SetFocus(txtCSO);
                return false;
            }
        }
        //
        if (this.txtCCREATEOWNERCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_ZhiDanRen);
            this.SetFocus(txtCCREATEOWNERCODE);
            return false;
        }
        //
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCCREATEOWNERCODE.Text).Length > 50)
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_ZhiDanRenChangDu);
                this.SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }
        //
        if (this.txtDCREATETIME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_ZhiDanRiQi);
            this.SetFocus(txtDCREATETIME);
            return false;
        }
        //
        if (this.txtCAUDITPERSONCODE.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCAUDITPERSONCODE.Text).Length > 50)
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_ShenHeRenChangDu);
                this.SetFocus(txtCAUDITPERSONCODE);
                return false;
            }
        }
        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCMEMO.Text).Length > 200)
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_BeiZhuChangDu);
                this.SetFocus(txtCMEMO);
                return false;
            }
        }

        if (txtITYPE.SelectedValue != "202" && ddl_Is_Whole.SelectedValue == "1")
        {
            this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_FenPiChuHuo);
            this.SetFocus(ddl_Is_Whole);
            return false;
        }

        if (this.txtITYPE.SelectedValue == "206")
        {
            if (this.ddlReason.SelectedValue == "")
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_XuanZeLiyouMa);
                return false;
            }
        }

        if (drpWorkType.SelectedValue == "")
        {
            this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_XuanZeZuoYe);
            return false;
        }

        #endregion

        #region 检查工单为空和状态
        //不等于其他出库，都必须输入ERPCODE
        if (txtITYPE.SelectedValue.Trim() != "206")
        {
            if (this.txtCERPCODE.Text.Trim().Length == 0)
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_NeedERPCode);
                this.SetFocus(txtCERPCODE);
                return false;
            }

        }

        if (txtITYPE.SelectedValue.Trim() == "203" || txtITYPE.SelectedValue.Trim() == "204" ||
            txtITYPE.SelectedValue.Trim() == "205")
        {
            //检查所有类型的工单状态是否可用
            msg = OutAsnQuery.Check_WIP_STATUS(this.txtCERPCODE.Text);
            if (!msg.Equals("OK"))
            {
                this.Alert(msg);
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }

        #endregion

        #region 201-36 收货退回   
        if (this.txtITYPE.Text.Trim().Equals("201"))
        {
            if (this.txtCERPCODE.Text.Trim().Length == 0)
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_NeedERPCode);
                this.SetFocus(txtCERPCODE);
                return false;
            }
            
            msg = OutAsnQuery.CheckReturnToVendorErpCodeIsUsable(txtCERPCODE.Text.Trim());
            if (!msg.Equals("OK"))
            {
                this.Alert(msg);
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        #endregion

        #region 203 - 35工单发料
        //WIP Issue : 35        
        if (this.txtITYPE.SelectedValue.Equals("203"))
        {
            //完工料号不能为空
            if (this.txt_FG_CinvCode.Text.Trim() == "")
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_NeedWanGongLiao);
                this.SetFocus(txt_FG_CinvCode);
                return false;
            }
            //完工数量不能为空
            if (this.txt_FG_Qty.Text.Trim() == "")
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_NeedWanGongLiang);
                this.SetFocus(txt_FG_Qty);
                return false;
            }
            //完工数量必须位数字
            //检查数量，不允许小数，负数，0 CQ 2014-2-13 13:39:48
            string msg1 = string.Empty;
            if (!(Comm_Function.Fun_IsDecimal(txt_FG_Qty.Text.Trim(), 0, 0, 1, out msg1)))
            {
                this.Alert(msg1);
                this.SetFocus(txt_FG_Qty);
                return false;
            }

            msg = OutAsnQuery.CheckWipIssueHead(this.txtCERPCODE.Text, IsSpecialWIP_Issue);
            if (!msg.Equals("OK"))
            {
                this.Alert(msg);
                this.SetFocus(txtCERPCODE);
                return false;
            }

            if (drpBillNo.SelectedValue == "") {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_ShuRuERPCode);
                this.SetFocus(drpBillNo);
                return false;
            }
        }

        //WIP Completion Return 工单完工退 17
        if (this.txtITYPE.SelectedValue.Equals("204"))
        {
            msg = OutAsnQuery.CheckWip_C_R_Head(this.txtCERPCODE.Text);
            if (!msg.Equals("OK"))
            {
                this.Alert(msg);
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        //工单超领(超发) 类型
        if (this.txtITYPE.SelectedValue.Trim().Equals("205"))
        {
            //未發料不可選擇超領發料
            msg = OutAsnQuery.CheckWIP_IsOkCF(txtCERPCODE.Text.Trim());
            if (!msg.Equals("OK"))
            {
                this.Alert(msg);
                this.SetFocus(txtCERPCODE);
                return false;
            }
            if (this.txtCMEMO.Text.Trim().Length == 0)
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_NeedFlowCode);
                this.SetFocus(txtCMEMO);
                return false;
            }
            //完工入库OK不允许超领（有权限允许）CQ 2013-9-17 10:13:36
            msg = OutAsnQuery.CheckOverCollar(txtCERPCODE.Text.Trim(), "", 0, WmsWebUserInfo.GetCurrentUser().UserNo, "1");
            if (!msg.Equals("0"))
            {
                this.Alert(msg);
                this.SetFocus(txtCERPCODE);
                return false;
            }

        }
        #endregion

        #region 工单修改
        if (this.Operation() == SYSOperation.Modify && !OutAsnQuery.IsSpecialOut(txtID.Text.Trim()))
        {
            //只有未生成指引或未生出库单的才能修改!
            if (OutAsnQuery.CheckIsExistOutAssitByOutAsnId(txtID.Text.Trim())
                || OutAsnQuery.ValidateIsCreateOutBill(txtID.Text.Trim()))
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_BuNengXiuGai);
                return false;
            }
        }
        #endregion

        #region 特殊元件
        //特定元件只能使用與非標準工單Roger 20130509
        if (IsSpecialWIP_Issue == 1)
        {
            //Roger 20130522
            if (this.txtCMEMO.Text.Trim().Length == 0 && this.txtITYPE.SelectedValue.Equals("460"))
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_NeedFlowCode);
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        #endregion

        return true;
    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public OUTASN SendData()
    {
        OUTASN entity = new OUTASN();
        //
        this.txtID.Text = this.txtID.Text.Trim();
        if (this.txtID.Text.Length > 0)
        {
            entity.id = txtID.Text;
        }
        //
        this.txtOutAsnCTICKETCODE.Text = this.txtOutAsnCTICKETCODE.Text.Trim();
        if (this.txtOutAsnCTICKETCODE.Text.Length > 0)
        {
            entity.cticketcode = txtOutAsnCTICKETCODE.Text;
        }
        else
        {
            entity.cticketcode = OutAsnQuery.CreateNo("OUTASN");
        }
        entity.cstatus = "0";
        if (this.Operation() != SYSOperation.New)
        {
            if (!string.IsNullOrEmpty(this.drCSTATUS.SelectedValue))
                entity.cstatus = this.drCSTATUS.SelectedValue;
        }
        else
        {
            entity.is_merge = 0;
        }
        //
        entity.itype = decimal.Parse(txtITYPE.SelectedValue);
        //
        this.txtCCLIENTCODE.Text = this.txtCCLIENTCODE.Text.Trim();
        if (this.txtCCLIENTCODE.Text.Length > 0)
        {
            entity.cclientcode = txtCCLIENTCODE.Text;
        }
        //
        this.txtCCLIENT.Text = this.txtCCLIENT.Text.Trim();
        if (this.txtCCLIENT.Text.Length > 0)
        {
            entity.cclient = txtCCLIENT.Text;
        }
        //
        this.txtCSO.Text = this.txtCSO.Text.Trim();
        if (this.txtCSO.Text.Length > 0)
        {
            entity.cso = txtCSO.Text;
        }
        //
        this.txtCCREATEOWNERCODE.Text = this.txtCCREATEOWNERCODE.Text.Trim();
        if (this.txtCCREATEOWNERCODE.Text.Length > 0)
        {
            entity.ccreateownercode = OPERATOR.GetUserIDByUserName(txtCCREATEOWNERCODE.Text);
        }
        //
        this.txtDCREATETIME.Text = this.txtDCREATETIME.Text.Trim();
        if (this.txtDCREATETIME.Text.Length > 0)
        {
            entity.dcreatetime = txtDCREATETIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        //
        this.txtCERPCODE.Text = this.txtCERPCODE.Text.Trim();
        if (this.txtCERPCODE.Text.Length > 0)
        {
            entity.cerpcode = txtCERPCODE.Text;
        }
        else
        {
            entity.cerpcode = "";//20200424针对erpcode不填的情况做的处理，防止后续页面出现问题
        }
        //
        this.txtCAUDITPERSONCODE.Text = this.txtCAUDITPERSONCODE.Text.Trim();
        if (this.txtCAUDITPERSONCODE.Text.Length > 0)
        {
            entity.cauditpersoncode = txtCAUDITPERSONCODE.Text;
        }
        //
        this.txtDAUDITDATE.Text = this.txtDAUDITDATE.Text.Trim();
        if (this.txtDAUDITDATE.Text.Length > 0)
        {
            entity.dauditdate = txtDAUDITDATE.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        this.txtCDEFINE1.Text = this.txtCDEFINE1.Text.Trim();
        if (this.txtCDEFINE1.Text.Length > 0)
        {
            entity.cdefine1 = txtCDEFINE1.Text;
        }
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        entity.cdefine2 = "0";//数量来源 ( 0 : WMS，1 :oracle ERP )
        entity.idefine5 = IsSpecialWIP_Issue;
        //20130516143828 获取完工料号
        if (this.txtITYPE.Text.Trim().Equals("203"))
        {
            //vConfig 判断是否连接ERP 1连接 0 不连接
            string vConfig = OutAsnQuery.GetConFig("000004");
            if (vConfig == "1")
            {

            }
            else
            {
                //不连接
                entity.fg_cinvcode = txt_FG_CinvCode.Text.Trim();
                entity.fg_qty = txt_FG_Qty.Text.Trim().ToDecimal();
            }

        }
        entity.reasoncode = this.ddlReason.SelectedValue;
        entity.worktype = this.drpWorkType.SelectedValue;
        entity.is_merge = 0; //默认是未合并的
        entity.special_out = 0;//默认为非特殊
        entity.billno = this.drpBillNo.SelectedValue;

        return entity;
    }

    public void btBomInput()
    {
        string sql = @"SELECT Count(1) FROM V_INPUBOMCOUNT  WHERE ID=(SELECT ID FROM BASE_BOM WHERE CINVCODE IN ('" + txt_FG_CinvCode.Text.Trim() + "')) and CINVCODE not in (SELECT CINVCODE FROM  outasn_d where id in (SELECT id FROM  outasn where fg_cinvcode='" + txt_FG_CinvCode.Text.Trim() + "'AND CTICKETCODE ='" + txtOutAsnCTICKETCODE.Text.Trim() + "'))";
        object obj = DBHelp.ExcuteScalarSQL(sql);
        int Bom_Count = Convert.ToInt32(obj == null ? 0 : obj);
        if (Bom_Count > 0)
        {
            btnInBom.Visible = true;
            btnInBom.Enabled = true;
        }
        else
        {
            btnInBom.Visible = true;
            btnInBom.Enabled = false;
        }
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="sender"></param>
    private void SaveTODB(object sender)
    {
        IGenericRepository<OUTASN> con = new GenericRepository<OUTASN>(context);
        string msg = string.Empty;
        if (this.CheckData())
        {
            OUTASN entity = this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";
            try
            {
                if (this.Operation() == SYSOperation.Modify || this.Operation() == SYSOperation.Preserved1)
                {
                    strKeyID = txtID.Text.Trim();
                    entity.id = strKeyID;
                    con.Update(entity);
                    con.Save();
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    strKeyID = Guid.NewGuid().ToString();
                    entity.id = strKeyID;
                    con.Insert(entity);
                    con.Save();
                    if (this.txtITYPE.SelectedValue.Equals("203"))
                        btnSave.Text = Resources.Lang.FrmOUTASNEdit_Button_BOMJianCe;
                    //完工退204-17
                    if (this.txtITYPE.SelectedValue.Equals("204"))
                    {
                        #region 调用存储过程
                        List<string> SparaList = new List<string>();
                        SparaList.Add("@P_OutAsnId:" + strKeyID);
                        SparaList.Add("@P_ReturnValue:" + "");
                        SparaList.Add("@INFOTEXT:" + "");
                        string[] results = DBHelp.ExecuteProc("PRC_CreateOutAsn_d", SparaList);
                        if (results.Length == 1)//调用存储过程有错误
                        {
                            this.Alert(results[0].ToString());
                        }
                        else if (results[0] == "0")
                        {
                            //成功
                            msg = Resources.Lang.WMS_Common_Msg_SaveSuccess;
                        }
                        else
                        {
                            //删除通知单
                            con.Delete(strKeyID);
                            con.Save();
                            msg = results[1].ToString() + Resources.Lang.WMS_Common_Msg_SaveFailed;
                            throw new Exception(msg);
                        }
                        #endregion
                    }
                }
                if ((sender as Button).ID == "btnNew")
                {
                    Response.Redirect("FrmOUTASNEdit.aspx?" + BuildQueryString(SYSOperation.Preserved1, strKeyID + "&IsSpecialPage=" + Request.QueryString["IsSpecialPage"] + "&IsSpecialWIP_Issue=" + this.IsSpecialWIP_Issue));
                }
                else
                {
                    this.AlertAndBack("FrmOUTASNEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID + "&IsSpecialPage=" + Request.QueryString["IsSpecialPage"] + "&IsSpecialWIP_Issue=" + this.IsSpecialWIP_Issue), Resources.Lang.WMS_Common_Msg_SaveSuccess);
                }
            }
            catch (Exception E)
            {
                #region 调用存储过程
                List<string> SparaList = new List<string>();
                SparaList.Add("@P_OutAsn_id:" + strKeyID);
                SparaList.Add("@P_ReturnValue:" + "");
                SparaList.Add("@INFOTEXT:" + "");
                string[] result = DBHelp.ExecuteProc("Proc_DeleteOutAsn", SparaList);
                if (result.Length == 1)//调用存储过程有错误
                {
                    this.Alert(result[0].ToString());
                }
                else if (result[0] == "0")
                {
                    //成功
                }
                else
                    msg = result[1].ToString();
                #endregion
                this.Alert(Resources.Lang.WMS_Common_Msg_SaveFailed + E.Message);//E.Message
            }
        }
    }

    /// <summary>
    /// 获取选择的ID
    /// </summary>
    public void GetSelectedIds()
    {
        if (SelectIds == null)
        {
            SelectIds = new Dictionary<string, string>();
        }

        foreach (GridViewRow item in this.grdOUTASN_D.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string ids = this.grdOUTASN_D.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                //Note by Qamar 2020-11-28
                //台惟案 強制勾選
                cbo.Checked = true;

                //控件选中且集合中不存在添加
                if (cbo.Checked && !SelectIds.ContainsKey(ids))
                {
                    SelectIds.Add(ids, ids);
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(ids))
                {
                    SelectIds.Remove(ids);
                }
            }
        }
    }

    /// 保存Bom
    /// <summary>
    /// 保存Bom
    /// </summary>
    public void BomSave()
    {
        #region //BOM工单发料
        if (this.txtITYPE.SelectedValue.Equals("203"))
        {
            try
            {
                #region 调用存储过程
                List<string> SparaList = new List<string>();
                SparaList.Add("@P_CTICKETCODE:" + txtOutAsnCTICKETCODE.Text.Trim());
                SparaList.Add("@P_FG_CINVCODE:" + txt_FG_CinvCode.Text.Trim());
                SparaList.Add("@ReturnValue:" + "");
                SparaList.Add("@INFOTEXT:" + "");
                string[] results = DBHelp.ExecuteProc("PRC_BOM_WIP_Issue_Insert", SparaList);
                if (results.Length == 1)//调用存储过程有错误
                {
                    this.Alert(results[0].ToString());
                }
                else if (results[0] == "0")
                {
                    //成功
                    Alert(Resources.Lang.FrmOUTASNEdit_Tips_BOMChengGong);
                }
                else
                    Alert(Resources.Lang.FrmOUTASNEdit_Tips_BOMShiBai);
                #endregion
                this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
                this.GridBind();
            }
            catch (Exception aq)
            {
                Alert(Resources.Lang.FrmOUTASNEdit_Tips_BOMYiChang + aq.Message);
            }
            btBomInput();
        }
        #endregion
    }

    #endregion

}






























