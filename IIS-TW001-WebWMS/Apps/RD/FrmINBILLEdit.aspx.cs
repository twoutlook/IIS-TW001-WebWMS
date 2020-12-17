using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using DreamTek.ASRS.Business.SP.ProcedureModel;


/// <summary>
/// 描述: 入库管理-->FrmINBILLEdit 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:53:49
/// 
/*
 * Roger
 * 2013/5/14 14:12:18
 * 20130514141218
 * 扣帐增加权限限制
 */
/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/
/*
Roger
2013/7/2 8:44:29
20130702084429
解决狂点按钮重复提交问题
*/
/// </summary>
public partial class RD_FrmINBILLEdit : WMSBasePage// PageBase, IPageEdit
{
    #region SQL
    //DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {

        ucINASN_Div.SetCompName = txtCASNID.ClientID;
        ucINASN_Div.SetORGCode = txtINASN_id.ClientID;
        ucINASN_Div.workType = null;

        //if (string.IsNullOrEmpty(KeyID) && !string.IsNullOrEmpty(txtID.Text.Trim()))
        //{
        //    KeyID = txtID.Text.Trim();
        //}

        if (this.IsPostBack == false)
        {
            Page_Load_Function();
            if (this.Operation() == SYSOperation.Modify)
            {
                SetInbillEnabled();

            }
            else if (this.Operation() == SYSOperation.New)
            {
                  //雪龙没有平库，所以锁定立库，并且不能编辑
                ddlWorkType.SelectedValue = "1";
                this.ddlWorkType.Enabled = false;
            }
        }
        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
        //20130702084429
        //this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
        this.btnInSTOCK_CURRENT.Attributes["onclick"] = this.GetPostBackEventReference(this.btnInSTOCK_CURRENT) + ";this.disabled=true;";
        this.btnInbill.Attributes["onclick"] = this.GetPostBackEventReference(this.btnInbill) + ";this.disabled=true;";
        this.btnCancel.Attributes["onclick"] = this.GetPostBackEventReference(this.btnCancel) + ";this.disabled=true;";
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        btnInSTOCK_ASRS.Attributes.Add("onclick", this.GetPostBackEventReference(btnInSTOCK_ASRS) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        btnInSTOCK_CURRENT.Attributes.Add("onclick", this.GetPostBackEventReference(btnInSTOCK_CURRENT) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
      
    }

    /// 设置inbill表头状态
    /// <summary>
    /// 设置inbill表头状态
    /// </summary>
    public void SetInbillEnabled()
    {
        txtCTICKETCODE.Enabled = false;
        txtCASNID.Enabled = false;
        ddlIType.Enabled = false;
        txtCERPCODE.Enabled = false;
        txtCDEFINE1.Enabled = false;
        txtCDEFINE2.Enabled = false;
        txtDINDATE.Enabled = false;
        txtDCREATETIME.Enabled = false;
        txtCCREATEOWNERCODE.Enabled = false;
        txtCAUDITPERSON.Enabled = false;
        txtDAUDITTIME.Enabled = false;
        ddlWorkType.Enabled = false;
        //Roger 2014/1/10 9:44:53 特殊WR，删除新增保存均不可以
        IGenericRepository<INBILL> entity = new GenericRepository<INBILL>(context);
        var caseList = from p in entity.Get()
                       where p.id == txtID.Text.Trim()
                       select p;
        if (caseList.Count() > 0)
        {
            var isSpecialReturn = InAsn.isSpecialWipReturn(caseList.ToList().FirstOrDefault<INBILL>().casnid);
            if (isSpecialReturn)
            {
                btnNew.Enabled = false;
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
            }
        }
    }

    /// <summary>
    /// 页面加载方法
    /// </summary>
    private void Page_Load_Function()
    {
        btnNew.Enabled = false;
        btnInSTOCK_CURRENT.Enabled = false;
        btnInSTOCK_ASRS.Enabled = false;//ASRS CQ 2015-5-5 17:26:08
        this.txtCTICKETCODE.Enabled = false;
        this.btnCreateInBill.Enabled = false;
        this.InitPage();
        if (this.Operation() == SYSOperation.Modify)
        {
            this.btnCreateInBill.Enabled = true;
            btnInSTOCK_CURRENT.Enabled = true;
            btnInSTOCK_ASRS.Enabled = true;//ASRS CQ 2015-5-5 17:26:08
            btnNew.Enabled = true;
            ShowData();
        }
        else if (this.Operation() == SYSOperation.Preserved1)
        {
            //20130702084429
            btnInSTOCK_CURRENT.Style.Remove("disabled");
            this.btnCreateInBill.Enabled = true;
            btnInSTOCK_CURRENT.Enabled = true;
            btnInSTOCK_ASRS.Style.Remove("disabled");//ASRS CQ 2015-5-5 17:26:08
            btnInSTOCK_ASRS.Enabled = true;//ASRS CQ 2015-5-5 17:26:08
            btnNew.Enabled = true;
            ShowData();
            //this.Operation = SYSOperation.Modify;
            //this.OpenFloatWin(BuildRequestPageURL("FrmINASN_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID, "", "INASN_D", 600, 350);
            //this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmINBILL_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID + "&Inasn_id=" + txtINASN_id.Text.Trim() + "&InType=" + ddlIType.SelectedValue + "&TradeCode=" + txtCDEFINE1.Text.Trim() + "&Currency=" + txtCDEFINE2.Text.Trim() + "','入庫單明細','INBILL_D');");//Roger SN整合 2013/12/12 11:39:54
            this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmINBILL_DEdit.aspx", SYSOperation.New, this.KeyID) + "&Inasn_id=" + txtINASN_id.Text.Trim() + "&InType=" + ddlIType.SelectedValue + "&TradeCode=" + txtCDEFINE1.Text.Trim() + "&Currency=" + txtCDEFINE2.Text.Trim() + "','入庫單明細','INBILL_D');");
        }
        else
        {
            this.btnSetCARGOSPACE.Enabled = false;
            this.txtCCREATEOWNERCODE.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
            this.txtID.Text = Guid.NewGuid().ToString();
            this.txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtDINDATE.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //this.txtCTICKETCODE.Text = new Fun_CreateNo().CreateNo("INBILL");
            this.ddlCSTATUS.SelectedValue = "0";
        }
        LoadInType();
    }

    /// <summary>
    /// 加载入库类型
    /// </summary>
    private void LoadInType()
    {
        //SYS_PARAMETERQuery query = new SYS_PARAMETERQuery();
        if (this.Operation() == SYSOperation.New)
        {
            Help.DropDownListDataBind(GetInType(false), this.ddlIType, "", "FUNCNAME", "EXTEND1", "");
        }
        else
        {
            Help.DropDownListDataBind(GetInType(true), this.ddlIType, "", "FUNCNAME", "EXTEND1", "");
        }
    }

    #region IPage 成员
    public string ASRSFig
    {
        get
        {
            if (ViewState["ASRSFig"] != null)
            {
                return ViewState["ASRSFig"].ToString();
            }
            return "";
        }
        set { ViewState["ASRSFig"] = value; }
    }
    /// <summary>
    /// 初始化页面。主要做一下动作
    ///1、DropDownList,ListBox数据装载,
    ///2、新增按钮、删除的按钮的Java脚本注册等
    ///一般在PageLoad 事件调用，
    ///并且IsPostBack = false时
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INBILL');return false;";
        txtCASNID.Attributes["onclick"] = "Show('" + ucINASN_Div.GetDivName + "');";
        Help.DropDownListDataBind(GetParametersByFlagType("WorkType"), this.ddlWorkType, Resources.Lang.Common_ALL, "flag_name", "flag_id", "");
        //ddlCSTATUS 状态
        Help.DropDownListDataBind(GetParametersByFlagType("INBILL"), this.ddlCSTATUS, Resources.Lang.Common_ALL, "flag_name", "flag_id", "");
        this.grdINBILL_D.DataKeyNames = new string[] { "IDS" };
        
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.Operation() == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.FrmINBILLEdit_MSG42;//"审批";
        }
        //是否显示ASRS 1显示 0 不显示
        ASRSFig =this.GetConFig("000006");
        var workType = string.Empty;
        IGenericRepository<INBILL> con = new GenericRepository<INBILL>(context);
        var caseList = from p in con.Get()
                       where p.id == this.KeyID.Trim()
                       select p;

        INBILL entity = caseList.ToList().FirstOrDefault<INBILL>();
        if (entity!=null && entity.worktype != null)
        {
            workType= entity.worktype;
            IsLKorPC(workType);

        }

        //if (ASRSFig == "1" )
        //{
        //    btnInSTOCK_CURRENT.Visible = false;
        //    btnInSTOCK_ASRS.Visible = true;
        //    btnRefreshAll.Visible = true;
        //    btnInbill.Visible = true;
        //    btnCancel.Visible = true;
        //    grdINBILL_D.Columns[12].Visible = true;
        //    grdINBILL_D.Columns[13].Visible = true;
        //    grdINBILL_D.Columns[14].Visible = true;
        //}
        else
        {
            btnInSTOCK_CURRENT.Visible = true;
            btnInSTOCK_ASRS.Visible = false;
            btnRefreshAll.Visible = false;
            btnInbill.Visible = false;
            btnCancel.Visible = false;
            grdINBILL_D.Columns[13].Visible = false;
            grdINBILL_D.Columns[14].Visible = false;
            grdINBILL_D.Columns[15].Visible = false;
            grdINBILL_D.Columns[16].Visible = true;
        }

        #region Disable and ReadOnly
        #endregion Disable and ReadOnly

        
    }

    private void IsLKorPC(string workType)
    {
        if (workType == "1")
        {
            btnInSTOCK_CURRENT.Visible = false;
            btnInSTOCK_ASRS.Visible = true;
            btnRefreshAll.Visible = true;
            btnInbill.Visible = true;
            btnCancel.Visible = true;
            grdINBILL_D.Columns[13].Visible = true;
            grdINBILL_D.Columns[14].Visible = true;
            grdINBILL_D.Columns[15].Visible = true;
            grdINBILL_D.Columns[16].Visible = true;
        }
        else
        {
            btnInSTOCK_CURRENT.Visible = true;
            btnInSTOCK_ASRS.Visible = false;
            btnRefreshAll.Visible = false;
            btnInbill.Visible = false;
            btnCancel.Visible = false;
            grdINBILL_D.Columns[13].Visible = false;
            grdINBILL_D.Columns[14].Visible = false;
            grdINBILL_D.Columns[15].Visible = false;
            grdINBILL_D.Columns[16].Visible = false;

        }
    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<INBILL> con = new GenericRepository<INBILL>(context);
        var caseList = from p in con.Get()
                       where p.id == this.KeyID.Trim()
                       select p;
        INBILL entity = caseList.ToList().FirstOrDefault<INBILL>();
        this.txtID.Text = entity.id.ToString();
        this.txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(entity.ccreateownercode);
        this.txtDCREATETIME.Text = entity.dcreatetime.HasValue ? entity.dcreatetime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
        this.txtCAUDITPERSON.Text = OPERATOR.GetUserNameByAccountID(entity.cauditperson);
        this.txtDAUDITTIME.Text = entity.daudittime.HasValue ? entity.daudittime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
        this.txtCTICKETCODE.Text = entity.cticketcode;
        this.ddlCSTATUS.SelectedValue = entity.cstatus;
        this.ddlWorkType.SelectedValue = entity.worktype;
        IGenericRepository<INASN> Entiy = new GenericRepository<INASN>(context);
        var caseEntiy= from p in Entiy.Get()
                       where p.id == entity.casnid
                       select p;
        if (caseEntiy.Count() > 0) this.txtCASNID.Text = caseEntiy.ToList().FirstOrDefault<INASN>().cticketcode;
        this.txtDINDATE.Text =entity.dindate!=null ? Convert.ToDateTime(entity.dindate).ToString("yyyy-MM-dd HH:mm:ss") : "";
        this.txtCMEMO.Text = entity.cmemo;
        this.txtCERPCODE.Text = entity.cerpcode;
        this.txtCDEFINE1.Text = entity.cdefine1;
        this.txtCDEFINE2.Text = entity.cdefine2;
        this.ddlIType.SelectedValue = entity.itype.ToString();
        this.txtINASN_id.Text = entity.casnid;
        this.txtPalletCode.Text = entity.palletcode;
        //ddlWorkType.SelectedValue = entity.worktype;
        //扣帐时间 扣帐人，抛转时间 CQ 2014-7-31 13:51:14
        txtdebittime.Text = entity.debittime.HasValue ? entity.debittime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
        txtdebituser.Text = OPERATOR.GetUserNameByAccountID(entity.debitowner);
        txtpaozhuantime.Text = entity.ddefine3.HasValue ? entity.ddefine3.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";


        this.TabMain0.Visible = true;
        Status = entity.cstatus;
        IsExistTempInBill_D = InBill.ValidateIsExistTemp_InBill_D(entity.cticketcode);
        //this.btnDelete.Enabled = !IsExistTempInBill_D;
        bool value = false;
         

        if (entity.cstatus.Equals("0") && !IsExistTempInBill_D)
        {
            value = true;
        }

        INTYPE INBILL_type = GetINTYPEByID(this.KeyID, "INBILL");
        if (INBILL_type.Is_Query == 1)
        {
            value = false;
        }

        SetTblFormControlEnabled(value);
        hdnCreateType.Value = entity.creatertype;
        //if (entity.ITYPE == 43)//WIP Return : 43
        //{
        //    this.btnSetCARGOSPACE.Enabled = false;
        //}
        //if (entity.CSTATUS.Equals("0") && !IsExistTempInBill_D)
        //{
        //    btnDelete.Enabled = true;
        //}
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);

        string isNeedSeral = this.GetConFig("140114");
        if (isNeedSeral == "1")
        {
            using (var modContext = new DBContext())
            {
                //存在需要管控序列号的物料
                if (modContext.INBILL_D.Any(x => x.id == entity.id && modContext.BASE_PART.Any(y => y.cpartnumber == x.cinvcode && y.NeedSerial == 1)))
                {
                    btnSeral.Visible = true;
                }
            }
        }

    }
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
    /// 是否存在上架明细
    /// </summary>
    public bool IsExistTempInBill_D
    {
        get
        {
            if (ViewState["IsExistTempInBill_D"] != null)
            {
                return Convert.ToBoolean(ViewState["IsExistTempInBill_D"].ToString());
            }
            return false;
        }
        set { ViewState["IsExistTempInBill_D"] = value; }
    }

    private void SetTblFormControlEnabled(bool value)
    {
        this.txtCASNID.Enabled = value;
        this.txtCAUDITPERSON.Enabled = value;
        this.txtCCREATEOWNERCODE.Enabled = value;
        this.txtCDEFINE1.Enabled = value;
        this.txtCDEFINE2.Enabled = value;
        this.txtCERPCODE.Enabled = value;
        this.txtCTICKETCODE.Enabled = value;
        this.txtDAUDITTIME.Enabled = value;
        this.txtDCREATETIME.Enabled = value;
        this.txtDINDATE.Enabled = value;
        //this.ddlCSTATUS.Enabled = value;
        this.ddlIType.Enabled = value;
        this.ddlWorkType.Enabled = value;

        txtCASNID.Attributes.Remove("onclick");
        //imgDAUDITTIME.Attributes.Remove("onclick");
        imgDINDATE.Attributes.Remove("onclick");

        btnCreateInBill.Enabled = value;
        btnDelete.Enabled = value;
        btnNew.Enabled = value;
        btnSave.Enabled = value;
        btnSearch.Enabled = value;
        btnInSTOCK_CURRENT.Enabled = value;
        btnInSTOCK_ASRS.Enabled = value;//ASRS CQ 2015-5-5 17:26:08
        btnRefreshAll.Enabled = value;
        btnInbill.Enabled = value;
        btnCancel.Enabled = value;

        //btnSetCARGOSPACE.Enabled = value;
        //Status = entity.CSTATUS;
        //if (entity.CSTATUS != "0")
        //{
        //    SetTblFormControlEnabled(false);
        //}
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        #region 非空检查
        //
        if (this.txtID.Text.Trim() == "")
        {
            //ID项不允许空
            this.Alert(Resources.Lang.FrmINBILLEdit_MSG43+"！");
            this.SetFocus(txtID);
            return false;
        }

        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (this.txtCCREATEOWNERCODE.Text.GetLengthByByte() > 50)
            {
                //制单人项超过指定的长度50
                this.Alert(Resources.Lang.FrmINBILLEdit_MSG44+"！");
                this.SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }
        //
        if (this.txtDCREATETIME.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate( this.txtDCREATETIME.Text) == false)
            {
                //制单日期项不是有效的日期
                this.Alert(Resources.Lang.FrmINBILLEdit_MSG45+"！");
                this.SetFocus(txtDCREATETIME);
                return false;
            }
        }
        //
        if (this.txtCAUDITPERSON.Text.Trim().Length > 0)
        {
            if (this.txtCAUDITPERSON.Text.GetLengthByByte() > 50)
            {
                //审核人项超过指定的长度50
                this.Alert(Resources.Lang.FrmINBILLEdit_MSG46+"！");
                this.SetFocus(txtCAUDITPERSON);
                return false;
            }
        }

        if (this.txtCASNID.Text.Trim() == "" || this.txtINASN_id.Text.Trim() == "")
        {
            //入库通知单单号项不允许空
            this.Alert(Resources.Lang.FrmINBILLEdit_MSG47+"！");
            this.SetFocus(txtCASNID);
            return false;
        }
        //
        if (this.txtCASNID.Text.Trim().Length > 0)
        {
            if (this.txtCASNID.Text.GetLengthByByte() > 30)
            {
                //入库通知单单号项超过指定的长度30
                this.Alert(Resources.Lang.FrmINBILLEdit_MSG48+"！");
                this.SetFocus(txtCASNID);
                return false;
            }
        }
       
        if (this.txtDINDATE.Text.Trim().Length == 0)
        {
            //入库日期项不允许空
            this.Alert(Resources.Lang.FrmINBILLEdit_MSG49 + "！");
            this.SetFocus(txtDINDATE);
            return false;
        }

        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 200)
            {
                //备注项超过指定的长度200
                this.Alert(Resources.Lang.FrmINBILLEdit_MSG50 + "！");
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        //
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
            if (this.txtCERPCODE.Text.GetLengthByByte() > 30)
            {
                //ERP单号项超过指定的长度30
                this.Alert(Resources.Lang.FrmINBILLEdit_MSG51 + "！");
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        #endregion

        ////判断单据状态        
        //if (!INASNRule.GetInAsnStatusByInAsnId(this.txtINASN_id.Text.Trim()).Equals("0"))
        //{
        //    this.Alert("只有未处理的单据才能生成入库单!");
        //    this.SetFocus(txtCASNID);
        //    return false;
        //}
        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public INBILL SendData(IGenericRepository<INBILL> con)
    {

        INBILL entity = (from p in con.Get()
                         where p.id == this.KeyID
                         select p).FirstOrDefault<INBILL>();

        if (entity == null)
        {
            entity = new INBILL();
        }
        //INBILL entity = new INBILL();
        //
        //this.txtID.Text = this.txtID.Text.Trim();
        //if (this.txtID.Text.Length > 0)
        //{
        //    entity.id = txtID.Text;
        //}
        //else
        //{
        //    entity.id = string.Empty;
        //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        //    //entity.ID = null;
        //}
        //
        this.txtCCREATEOWNERCODE.Text = this.txtCCREATEOWNERCODE.Text.Trim();
        if (this.txtCCREATEOWNERCODE.Text.Length > 0)
        {
            entity.ccreateownercode = txtCCREATEOWNERCODE.Text;
        }
        else
        {
            entity.ccreateownercode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCREATEOWNERCODE = null;
        }
        //
        this.txtDCREATETIME.Text = this.txtDCREATETIME.Text.Trim();
        if (this.txtDCREATETIME.Text.Length > 0)
        {
            entity.dcreatetime = txtDCREATETIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            entity.dcreatetime = null;
        }
        //
        this.txtCAUDITPERSON.Text = this.txtCAUDITPERSON.Text.Trim();
        if (this.txtCAUDITPERSON.Text.Length > 0)
        {
            entity.cauditperson = txtCAUDITPERSON.Text;
        }
        else
        {
            entity.cauditperson = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CAUDITPERSON = null;
        }
        //
        this.txtDAUDITTIME.Text = this.txtDAUDITTIME.Text.Trim();
        if (this.txtDAUDITTIME.Text.Length > 0)
        {
            entity.daudittime = txtDAUDITTIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        else
        {
          
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            entity.daudittime = null;
        }
        //
        this.txtCTICKETCODE.Text = this.txtCTICKETCODE.Text.Trim();
        if (this.txtCTICKETCODE.Text.Length > 0)
        {
            entity.cticketcode = txtCTICKETCODE.Text;
        }
        else
        {
            entity.cticketcode = new Fun_CreateNo_Wms().CreateNo("INBILL");
            //entity.SetDBNull("CTICKETCODE",true);
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CTICKETCODE = null;
        }
        //
        //this.txtCSTATUS.Text = this.txtCSTATUS.Text.Trim();
        //if(this.txtCSTATUS.Text.Length > 0)
        //{

        if (this.Operation() != SYSOperation.Modify)
        {
            entity.cstatus = ddlCSTATUS.SelectedValue.Trim();
        }
        //}
        //else
        //{
        //    entity.SetDBNull("CSTATUS",true);
        //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        //    //entity.CSTATUS = null;
        //}
        //
        DataTable InAsnDt = null;
        this.txtCASNID.Text = this.txtCASNID.Text.Trim();
        if (this.txtCASNID.Text.Length > 0)
        {
            entity.casnid = txtINASN_id.Text.Trim();
            InAsnDt = new InBill().GetInAsnById(txtINASN_id.Text.Trim());
            if (InAsnDt.Rows.Count == 0) InAsnDt = null;
        }
        else
        {
            entity.casnid = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CASNID = null;
        }
        //
        this.txtDINDATE.Text = this.txtDINDATE.Text.Trim();
        if (this.txtDINDATE.Text.Length > 0)
        {
            if (this.txtDINDATE.Text.Length == 19)
            {
                entity.dindate = Convert.ToDateTime(txtDINDATE.Text.Trim());
            }
            else
            {
                entity.dindate = Convert.ToDateTime(txtDINDATE.Text.Trim() + " " + Help.DateTime_ToChar(DateTime.Now.Hour) + ":" + Help.DateTime_ToChar(DateTime.Now.Minute) + ":" + Help.DateTime_ToChar(DateTime.Now.Second));
            }
        }
        else
        {
          
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            entity.dindate = DateTime.Now;
        }
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        else
        {
            if (InAsnDt != null && !(InAsnDt.Rows[0]["CMEMO"] is DBNull))
            {
                entity.cmemo = InAsnDt.Rows[0]["CMEMO"].ToString();
            }
            else
            {
                entity.cmemo = string.Empty;
            }
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CMEMO = null;
        }
        //
        this.txtCERPCODE.Text = this.txtCERPCODE.Text.Trim();
        if (this.txtCERPCODE.Text.Length > 0)
        {
            entity.cerpcode = txtCERPCODE.Text;
        }
        else
        {
            if (InAsnDt != null && !(InAsnDt.Rows[0]["CERPCODE"] is DBNull))
            {
                entity.cerpcode = InAsnDt.Rows[0]["CERPCODE"].ToString();
            }
            else
            {
                entity.cerpcode = string.Empty;
            }

            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CERPCODE = null;
        }
        this.txtCDEFINE1.Text = this.txtCDEFINE1.Text.Trim();
        if (this.txtCDEFINE1.Text.Length > 0)
        {
            entity.cdefine1 = txtCDEFINE1.Text;
        }
        else
        {
            if (InAsnDt != null && !(InAsnDt.Rows[0]["CDEFINE1"] is DBNull))
            {
                entity.cdefine1 = InAsnDt.Rows[0]["CDEFINE1"].ToString();
            }
            else
            {
                entity.cdefine1 = string.Empty;
            }
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CERPCODE = null;
        }
        this.txtCDEFINE2.Text = this.txtCDEFINE2.Text.Trim();
        if (this.txtCDEFINE2.Text.Length > 0)
        {
            entity.cdefine2 = txtCDEFINE2.Text;
        }
        else
        {
           if (InAsnDt.Rows.Count>0)
            {
                 if (!string.IsNullOrEmpty(InAsnDt.Rows[0]["CDEFINE2"].ToString()))
                entity.cdefine2 = InAsnDt.Rows[0]["CDEFINE2"].ToString();
            }
            else
            {
                entity.cdefine2 = string.Empty;
            }
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CERPCODE = null;
        }
        if (InAsnDt.Rows.Count>0)
        {
            if (!string.IsNullOrEmpty(InAsnDt.Rows[0]["ITYPE"].ToString()))
                entity.itype = Convert.ToDecimal(InAsnDt.Rows[0]["ITYPE"]);
        }
        else
        {
            entity.itype = Convert.ToDecimal(ddlIType.SelectedValue.Trim());
        }

        var workType = this.ddlWorkType.SelectedValue;
        if (InAsnDt != null) {
            workType = InAsnDt.Rows[0]["WORKTYPE"].ToString();
        }
        entity.worktype = !string.IsNullOrEmpty(workType) ? workType.Trim() : "1";
        entity.operationtype = 1;


        #region 界面上不可见的字段项
        /*
        entity.DDEFINE3 = ;
        entity.CDEFINE1 = ;
        entity.CDEFINE2 = ;
        entity.DDEFINE4 = ;
        entity.IDEFINE5 = ;
        */
        #endregion
        return entity;

    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.btnSave.Enabled = false; //CQ 2013-5-13 13:47:51
        SaveToDB(sender);
        this.btnCreateInBill.Enabled = true;
        this.btnSave.Enabled = true;
        //20130702084429
        btnSave.Style.Remove("disabled");
    }

    private void SaveToDB(object sender)
    {
        IGenericRepository<INBILL> con = new GenericRepository<INBILL>(context);
        if (this.CheckData())
        {
            INBILL entity = (INBILL)this.SendData(con);
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";
            string msg = string.Empty;
            try
            {
                
                if (this.Operation() == SYSOperation.Modify)
                {

                    strKeyID = txtID.Text.Trim();
                    entity.id = txtID.Text.Trim();
                    con.Update(entity);
                    con.Save();
                    //this.AlertAndBack("FrmINBILLEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID),"保存成功"); 
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    strKeyID = Guid.NewGuid().ToString();
                    entity.id = strKeyID;
                    con.Insert(entity);
                    con.Save();

                    #region 调用存储过程
                    List<string> SparaList = new List<string>();
                    SparaList.Add("@P_InAsn_id:" + txtINASN_id.Text.Trim());
                    SparaList.Add("@P_InBill_Id:" + strKeyID);
                    SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                    SparaList.Add("@P_IsTemporary:" + "0");
                    SparaList.Add("@P_ReturnValue:" + "");
                    SparaList.Add("@INFOTEXT:" + "");  
                    string[] Result = DBHelp.ExecuteProc("Proc_CreateInBill", SparaList);
                    if (Result.Length == 1)//调用存储过程有错误
                    {
                        //入库单生成失败
                        msg = Resources.Lang.FrmINBILLEdit_MSG52 + "！";
                    }
                    else if (Result[0] == "0")
                    {
                        this.AlertAndBack("FrmINBILLEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.FrmINBILLEdit_MSG53);//"保存成功"); 
                    }
                    else if (Result[0] == "1")
                    {
                        //入库单生成失败
                        msg = Resources.Lang.FrmINBILLEdit_MSG52 + "！";
                    }
                    #endregion 
                }
                //提交列表数量
                //string 

                foreach (var row in grdINBILL_D.Rows)
                {
                    //检查提交数据 [入库数量、入库储位]，验证成功更新数据

                }

                IsLKorPC(entity.worktype);
                //Response.Redirect("FrmINBILLEdit.aspx?" + BuildQueryString(SYSOperation.Preserved1, strKeyID));
                if ((sender as Button).ID == "btnNew")
                {
                    //strKeyID = hfId.Value;
                    Response.Redirect(BuildRequestPageURL("FrmINBILLEdit.aspx", SYSOperation.Preserved1, strKeyID));
                }
                else
                {
                    //保存成功
                    this.AlertAndBack("FrmINBILLEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.FrmINBILLEdit_MSG53 + "!" + msg);
                }
               
            }
            catch (Exception E)
            {
                //失败
                this.Alert(this.GetOperationName() + Resources.Lang.FrmINBILLEdit_MSG52 +"！");// + E.Message
#if Debug 
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
            }
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        //Response.Redirect(BuildRequestPageURL("FrmINBILLEdit.aspx", SYSOperation.Preserved1, txtID.Text.Trim()));
        //SaveToDB(sender);
        this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmINBILL_DEdit.aspx", SYSOperation.New, this.KeyID) + "&Inasn_id=" + txtINASN_id.Text.Trim() + "&InType=" + ddlIType.SelectedValue + "&TradeCode=" + txtCDEFINE1.Text.Trim() + "&Currency=" + txtCDEFINE2.Text.Trim() + "','" + Resources.Lang.FrmINBILL_DEdit_MSG1 + "','INBILL_D');");//入庫單明細

    }

    #region IPageGrid 成员

    public void GridBind()
    {
        //RD_FrmINBILL_DListQuery listQuery = new RD_FrmINBILL_DListQuery();
        //DataTable dtSource = listQuery.GetList_V(txtID.Text, txtCinvcode.Text.Trim(), false, this.grdNavigatorINBILL_D.CurrentPageIndex, this.grdINBILL_D.PageSize);
        //this.grdINBILL_D.DataSource = dtSource;
        //this.grdINBILL_D.DataBind();

        IGenericRepository<V_INBILL_D> entity = new GenericRepository<V_INBILL_D>(context);
        var caseList = from p in entity.Get()                      
                       orderby p.LineID ascending
                       where 1 == 1 && p.id == txtID.Text.Trim()
                       select p;

        //if (txtID.Text != string.Empty)
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.Contains(txtID.Text.Trim()));
        //if (txtCinvcode.Text != string.Empty)
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cin) && x.cvendor.Contains(txtCinvcode.Text.Trim()));

        if (txtCinvcode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));
       
        if (caseList != null && caseList.Count() > 0)
        {            
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
                                                                                                       //grdINBILL_D.DataSource = GetPageSize(caseList.AsQueryable(), PageSize, CurrendIndex).ToList();

        //var listResult = GetPageSize(caseList.AsQueryable(), PageSize, CurrendIndex).ToList();
        //var source = GetGridSourceDataByList(listResult, "ASRS_STATUS", "ASRS_STATUS");

        //var source = GetGridSourceDataByList(listResult, "cstatus", "ASN_D_STATUS");
        //var src2 = GetGridSourceDataSplitPart(source);
        //grdINASN_D.DataSource = src2;
        //      var listResult = GetPageSize(caseList.OrderBy(x => x.lineId), PageSize, CurrendIndex).ToList();
        //      var source = getgridsourcedatabylist(listresult, "cstatus", "asn_d_status");
        //      var src2 = getgridsourcedatasplitpart(source);
        //      grdinasn_d.datasource = src2;

        //grdINBILL_D.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        var listResult = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        var source = GetGridSourceDataByList(listResult, "ASRS_STATUS", "ASRS_STATUS");
        var src2 = GetGridSourceData_PART_RANK(source);
        grdINBILL_D.DataSource = src2;
        grdINBILL_D.DataBind();
      
    }


    #endregion
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }

    //protected void BtnNew_Click(object sender, EventArgs e)
    //{
    //    //grdNavigatorINBILL_D
    //}

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string ReturnValue = string.Empty;
        string errText = string.Empty;
        string msg = string.Empty;
        try
        {
            #region 注销
            

            for (int i = 0; i < this.grdINBILL_D.Rows.Count; i++)
            {
                if (this.grdINBILL_D.Rows[i].Cells[0].FindControl("chkSelect") is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdINBILL_D.Rows[i].Cells[0].FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        #region 调用存储过程
                        List<string> SparaList = new List<string>();
                        SparaList.Add("@P_Bill_ID:" + txtID.Text.Trim());
                        SparaList.Add("@P_Bill_IDS:" + this.grdINBILL_D.DataKeys[i].Values[0].ToString());
                        SparaList.Add("@P_BZ:" + "1");
                        SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                        SparaList.Add("@P_return_Value:" + "");
                        SparaList.Add("@errText:" + "");
                        string[] Result = DBHelp.ExecuteProc("Proc_Delete_WebBill_D", SparaList);
                        if (Result.Length == 1)//调用存储过程有错误
                        {
                            msg += Result[0];
                        }
                        else if (Result[0] == "1")
                        {
                            msg += Result[1];
                        }
                        #endregion 
                    }
                }
            }
            if (msg.Length == 0)
            {
                //删除成功
                this.Alert(Resources.Lang.FrmINBILLEdit_MSG55+ "！");
            }
            else
            {
                Alert(msg);
            }

            this.GridBind();
        }
        catch (Exception E)
        {
            //删除失败
            this.Alert(Resources.Lang.FrmINBILLEdit_MSG56+"！" + E.Message.ToJsString());
        }
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdINBILL_D.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdINBILL_D.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    // WL 20160516 页面显示
    protected void grdINBILL_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            LinkButton linkModify = (LinkButton)e.Row.FindControl("LinkButton1");
            TextBox txtGv_CPOSITIONCODE = e.Row.FindControl("txtGv_CPOSITIONCODE") as TextBox;
            //TextBox txtSpace = e.Row.FindControl("txtSpace") as TextBox;
            TextBox txtGv_IQUANTITY = e.Row.FindControl("txtGv_IQUANTITY") as TextBox;
            Button btnRefresh = (Button)e.Row.FindControl("btnRefresh");
            //修改状态
            Button LinkASRS_STATUS = (Button)e.Row.FindControl("LinkASRS_STATUS");
            LinkButton lnkStatus = (LinkButton)e.Row.FindControl("lnkStatus");
            // WL 20160511 修改状态 
            Button LinkSPACE_STATUS_I = (Button)e.Row.FindControl("LinkSPACE_STATUS_I");
            Button LinkSPACE_STATUS_O = (Button)e.Row.FindControl("LinkSPACE_STATUS_O");

            LinkASRS_STATUS.Style.Add("color", "Blue");
            LinkSPACE_STATUS_I.Style.Add("color", "Blue");
            LinkSPACE_STATUS_O.Style.Add("color", "Blue");
            LinkSPACE_STATUS_I.Enabled = false;
            LinkSPACE_STATUS_O.Enabled = false;

            //物料名称
            var partName = e.Row.Cells[5].Text;
            if (!string.IsNullOrEmpty(partName) && partName.Length > 20)
            {
                e.Row.Cells[5].Text = partName.Substring(0, 20) + "...";
            }

            if ((Status.Length > 0 && Status.Equals("0")) && !IsExistTempInBill_D)
            {
                //linkModify.NavigateUrl = "#";
                //this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmINBILL_DEdit.aspx?Currency=" + txtCDEFINE2.Text.Trim() + "&TradeCode=" + txtCDEFINE1.Text.Trim() + "&Inasn_id=" + txtINASN_id.Text.Trim(), SYSOperation.Modify, strKeyID), "入库单", "INBILL_D", 600, 350);

                //onBlur
                //txtGv_CPOSITIONCODE.Attributes["onBlur"] = "submitData('In','PositionCode','" + strKeyID.Trim() + "','','',this.value,this);";
                //
                string script = "";
                if (ASRSFig == "1")
                {
                    //2015-11-30 传递的参数要有数量Sum   [ASRS专用]
                    script = string.Format(@"<script type=""text/javascript"">
                                                     $(function () {{
                                                         $(""#{0}"").autocomplete({{
                                                             source: function (request, response) {{
                                                                 //alert(request.term);
                                                                 //alert($(this).attr(""CINVCODE""));
                                                                 $.ajax({{
                                                                     url: ""../Server/Cargospan.ashx?PositionCode="" + request.term + ""&CinvCode={1}&Type=In&Sum={3}&ASRSFig=1"",
                                                                     dataType: ""xml"",
                                                                     error: function (XMLHttpRequest, textStatus, errorThrown) {{
                                                                         //alert(XMLHttpRequest.status);
                                                                         //alert(XMLHttpRequest.readyState);
                                                                         //alert(errorThrown);
                                                                         //alert(textStatus);
                                                                     }},
                                                                     success: function (data) {{
                                                                         //alert(data);
                                                                         response($(""reuslt"", data).map(function () {{

                                                                             return {{
                                                                                 value: $(""CPOSITIONCODE"", this).text(),
                                                                                 label: $(""CPOSITIONCODE"", this).text() + "" ["" + $(""IQTY"", this).text() + ""]"",
                                                                                 id: $(""CPOSITION"", this).text()
                                                                             }}
                                                                         }}));
                                                                         //alert(data.xml);
                                                                     }}
                                                                 }});
                                                             }},
                                                             autoFocus: true,
                                                             minLength: 0,
                                                             minChars:0,
                                                             delay: 0,
                                                             select: function (event, ui) {{  
                                                                  $.get(""../BASE/SubmitDate.aspx"",
                                                                        {{ Type: 'In', DataType: 'PositionCode', Ids: '" + strKeyID.Trim() + @"', Qty: $(""#{2}"").val(), Line_Qty: 0, PositionCode: ui.item.value }},
                                                                        function (data) {{
                                                                            $(""#ctl00_ContentPlaceHolderMain_showMsgTd"").html(data);
                                                                        }});
                                                             }},
                                                             open: function () {{
                                                                 $(this).removeClass(""ui-corner-all"").addClass(""ui-corner-top"");
                                                             }},
                                                             close: function () {{
                                                                 $(this).removeClass(""ui-corner-top"").addClass(""ui-corner-all"");
                                                             }}
                                                         }});
                                                     }});
                                                    </script> ",
                                   txtGv_CPOSITIONCODE.ClientID,
                                   e.Row.Cells[3].Text.Trim(), txtGv_IQUANTITY.ClientID, txtGv_IQUANTITY.Text.Trim());
                }
                else
                {
                    script = string.Format(@"<script type=""text/javascript"">
                                                     $(function () {{
                                                         $(""#{0}"").autocomplete({{
                                                             source: function (request, response) {{
                                                                 //alert(request.term);
                                                                 //alert($(this).attr(""CINVCODE""));
                                                                 $.ajax({{
                                                                     url: ""../Server/Cargospan.ashx?PositionCode="" + request.term + ""&CinvCode={1}&Type=In&ASRSFig=0"",
                                                                     dataType: ""xml"",
                                                                     error: function (XMLHttpRequest, textStatus, errorThrown) {{
                                                                         //alert(XMLHttpRequest.status);
                                                                         //alert(XMLHttpRequest.readyState);
                                                                         //alert(errorThrown);
                                                                         //alert(textStatus);
                                                                     }},
                                                                     success: function (data) {{
                                                                         //alert(data);
                                                                         response($(""reuslt"", data).map(function () {{

                                                                             return {{
                                                                                 value: $(""CPOSITIONCODE"", this).text(),
                                                                                 label: $(""CPOSITIONCODE"", this).text() + "" ["" + $(""IQTY"", this).text() + ""]"",
                                                                                 id: $(""CPOSITION"", this).text()
                                                                             }}
                                                                         }}));
                                                                         //alert(data.xml);
                                                                     }}
                                                                 }});
                                                             }},
                                                             autoFocus: true,
                                                             minLength: 0,
                                                             minChars:0,
                                                             delay: 0,
                                                             select: function (event, ui) {{  
                                                                  $.get(""../BASE/SubmitDate.aspx"",
                                                                        {{ Type: 'In', DataType: 'PositionCode', Ids: '" + strKeyID.Trim() + @"', Qty: $(""#{2}"").val(), Line_Qty: 0, PositionCode: ui.item.value }},
                                                                        function (data) {{
                                                                            $(""#ctl00_ContentPlaceHolderMain_showMsgTd"").html(data);
                                                                        }});
                                                             }},
                                                             open: function () {{
                                                                 $(this).removeClass(""ui-corner-all"").addClass(""ui-corner-top"");
                                                             }},
                                                             close: function () {{
                                                                 $(this).removeClass(""ui-corner-top"").addClass(""ui-corner-all"");
                                                             }}
                                                         }});
                                                     }});
                                                    </script> ",
                                    txtGv_CPOSITIONCODE.ClientID,
                                    e.Row.Cells[3].Text.Trim(), txtGv_IQUANTITY.ClientID);
                }
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), txtGv_CPOSITIONCODE.ClientID, script);

                //20130624152404
                txtGv_IQUANTITY.Attributes["onBlur"] = "submitData('In','Qty','" + strKeyID.Trim() + "',this.value,'','" + txtGv_CPOSITIONCODE.ClientID.Trim() + "',this);";

                if (Status.Equals("0") && e.Row.Cells[11].Text.Trim().Length > 0 && e.Row.Cells[11].Text.Trim() != "&nbsp;")
                {
                    //btnSetCARGOSPACE.Enabled = true;
                }
            }
            else
            {
                //linkModify.Enabled = false;--Roger 2013/11/28 14:02:35 SN整合
                txtGv_CPOSITIONCODE.Enabled = false;
                txtGv_IQUANTITY.Enabled = false;
                btnSetCARGOSPACE.Enabled = false;
                LinkASRS_STATUS.Enabled = false;
                //LinkSPACE_STATUS_I.Enabled = false;
                //LinkSPACE_STATUS_O.Enabled = false;
            }

            if (ASRSFig == "1")
            {
                #region AS/RS
                if (e.Row.Cells[15].Text != "0")
                {
                    txtGv_CPOSITIONCODE.Enabled = false;
                    txtGv_IQUANTITY.Enabled = false;
                }
                if (e.Row.Cells[15].Text == "0")
                {
                    btnRefresh.Enabled = false;
                    //当储位默认为空时，绑定符合条件的储位
                    if (txtGv_CPOSITIONCODE.Text == "")
                    {
                        //2015-11-30 储位编码默认为库余量符合入库单最小数量的储位   [ASRS专用]
                        DataTable pdat = null;
                        //2015-12-02 获得符合条件的第一个储位编号
                        pdat = new InBill().GetCargoSpaceListByBaseData(e.Row.Cells[3].Text.Trim(), "", true, 15, txtGv_IQUANTITY.Text.Trim());
                        if (pdat.Rows.Count > 0)
                        {
                            txtGv_CPOSITIONCODE.Text = pdat.Rows[0]["cpositioncode"].ToString();
                        }
                    }
                    //e.Row.Cells[10].Text = @"<script type=""text/javascript""> $(""CPOSITIONCODE"", this).text()</script>";
                }



                //e.Row.Cells[e.Row.Cells.Count - 4].Text = "重试";
                if (e.Row.Cells[15].Text == "7")
                {
                    LinkASRS_STATUS.Enabled = false;
                }
                switch (LinkASRS_STATUS.Text.Trim())//12
                {
                    case "0":
                        LinkASRS_STATUS.Text = "["+Resources.Lang.FrmINBILLEdit_MSG57+"]";//"[入库]";
                        break;
                    case "1":
                        LinkASRS_STATUS.Text = "["+Resources.Lang.FrmINBILLEdit_MSG58+"]";//"[取消]";
                        LinkASRS_STATUS.Enabled = false;
                        break;
                    case "7":
                        //LinkASRS_STATUS.Text = "";
                        LinkASRS_STATUS.Visible = false;
                        break;
                    case "8":
                        LinkASRS_STATUS.Text = "["+Resources.Lang.FrmINBILLEdit_MSG59+"]";//"[重试]";
                        break;

                }
                switch (lnkStatus.Text)//13
                {
                    case "0":
                        lnkStatus.Text = Resources.Lang.FrmDispatchUnitEdit_MsgTitle32;  //"未处理";
                        break;
                    case "1":
                        lnkStatus.Text = Resources.Lang.FrmINBILLEdit_MSG60;  //"运作中";
                        break;
                    case "7":
                        lnkStatus.Text = Resources.Lang.FrmINBILLEdit_MSG61;  //"处理完成";
                        lnkStatus.Enabled = false;
                        break;
                    case "8":
                        lnkStatus.Text = Resources.Lang.FrmINBILLEdit_MSG62;  //"处理异常";
                        break;
                }

                //switch (e.Row.Cells[14].Text)//14
                //{
                //    case "0":
                //        e.Row.Cells[14].Text = Resources.Lang.FrmDispatchUnitEdit_MsgTitle32;  //"未处理";
                //        break;
                //    case "1":
                //        e.Row.Cells[14].Text = Resources.Lang.FrmINBILLEdit_MSG60;  //"运作中";
                //        break;
                //    case "7":
                //        e.Row.Cells[14].Text = Resources.Lang.FrmINBILLEdit_MSG61;  //"处理完成";
                //        break;
                //    case "8":
                //        e.Row.Cells[14].Text = Resources.Lang.FrmINBILLEdit_MSG62;  //"处理异常";
                //        break;
                //}

                // WL 20160511 
                //string str_PALLET_CODE = "";
                string str_CPOSITIONCODE = txtGv_CPOSITIONCODE.Text.ToString();
                if (new InBill().CheckBASE_CARGOSPACE(str_CPOSITIONCODE))
                {
                    LinkSPACE_STATUS_I.Enabled = true;
                    LinkSPACE_STATUS_O.Style.Add("color", "");
                }
                else
                {
                    LinkSPACE_STATUS_O.Enabled = true;
                    LinkSPACE_STATUS_I.Style.Add("color", "");
                }
                LinkSPACE_STATUS_I.Text = "[" + Resources.Lang.FrmINBILLEdit_MSG57 + "]";//"[入库]";
                LinkSPACE_STATUS_O.Text =  "[" + Resources.Lang.FrmINBILLEdit_MSG63 + "]";//"[出库]";

                if (LinkASRS_STATUS.Text !=  "[" + Resources.Lang.FrmINBILLEdit_MSG57 + "]");//"[入库]")
                {
                    LinkSPACE_STATUS_I.Enabled = false;
                    LinkSPACE_STATUS_O.Enabled = false;
                    LinkSPACE_STATUS_I.Style.Add("color", "");
                    LinkSPACE_STATUS_O.Style.Add("color", "");
                }

                /*
                // 空栈板入库
                switch (LinkSPACE_STATUS_I.Text.Trim())
                {
                    case "0":
                        LinkSPACE_STATUS_I.Text = "[入库]";
                        break;
                    case "1":
                        LinkSPACE_STATUS_I.Text = "[在库]";
                        break;
                }

                //　栈板出库
                switch (LinkSPACE_STATUS_I.Text.Trim())
                {
                    case "0":
                        LinkSPACE_STATUS_I.Text = "[入库]";
                        break;
                    case "1":
                        LinkSPACE_STATUS_I.Text = "[在库]";
                        break;
                }*/

                #endregion
            }



        }
    }

    //修改状态：
    protected void LinkASRS_STATUS_Click(object sender, EventArgs e)
    {
        try
        {
            
            // 需要追加一个选择站点的选择
            string ids = (sender as Button).CommandArgument;
            string msg = string.Empty;
            string Pallet = "";
            (sender as Button).Enabled = false;

            IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
            var caseList = from p in con.Get()
                           where p.ids == ids
                           select p;
            INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();

            if (entity!=null &&(string.IsNullOrEmpty(entity.cpositioncode)||string.IsNullOrEmpty(entity.cposition)))
            {
                Alert("储位编码/储位,不能为空!");
            }
            else
            {

                BASE_CRANECONFIG_DETIAL entity2 = null;
                if (entity != null && !string.IsNullOrEmpty(entity.pallet_code))
                {
                    IGenericRepository<BASE_CRANECONFIG_DETIAL> condetail = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
                    var caseList2 = from p in condetail.Get()
                                    where p.ID == entity.pallet_code
                                    select p;
                    entity2 = caseList2.ToList().FirstOrDefault<BASE_CRANECONFIG_DETIAL>();
                }


                PROC_ASRS_InChangeStatus proc = new PROC_ASRS_InChangeStatus();
                proc.Ids = ids;
                proc.Space = entity2 != null ? entity2.SITEID : entity.pallet_code;
                proc.Lineid = entity.wire.ToString();
                proc.UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                proc.Execute();
                if (proc.ReturnValue == 1)
                {
                    Alert(proc.ErrorMessage);
                }
                //if (!WmsDBCommon_ASRS.ASRS_InChangeStatus(ids, entity.pallet_code, out msg, entity.LineID.ToString(), Server.MapPath("~/EQ2008_Dll_Set.ini")))
                //{
                //    Alert(msg);
                //}
                else
                {
                    WmsDBCommon_ASRS.LED_IN(ids, "", out msg, Server.MapPath("~/EQ2008_Dll_Set.ini"));
                }
                (sender as Button).Enabled = true;
                btnSearch_Click(this.btnSearch, EventArgs.Empty);
            }
        }
        catch (Exception E)
        {
            Alert(E.Message);
        }
        
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string positonCode = (sender as LinkButton).CommandArgument;
        this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("../Base/FrmBaseASRSList.aspx", SYSOperation.Modify, txtID.Text.Trim()) + "&code=" + txtCTICKETCODE.Text + "&caseType=IN&positonCode=" + positonCode + "','ASRS命令列表','ASRS_LIST');");
    }

    //刷新
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        string ids = (sender as Button).CommandArgument;

        //抓取更新SN的状态
        string msg = string.Empty;
        if (!WmsDBCommon_ASRS.ASRS_InRefresh(ids, out msg))
        {
            Alert(msg);
        }
        btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    protected void dsGrdINBILL_D_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

   
    /// <summary>
    /// 生成入库单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateInBill_Click(object sender, EventArgs e)
    {
        string msg = Resources.Lang.FrmINBILLEdit_MSG64;// "入库单生成失败！";
        #region 调用存储过程
        List<string> SparaList = new List<string>();
        SparaList.Add("@P_Inasn_id:" + txtINASN_id.Text.Trim());
        SparaList.Add("@P_InBill_id:" + txtID.Text.Trim());
        SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
        string Result = DBHelp.ExecuteProcReturnValue("Proc_CreateInBillByInAsn", SparaList, "@P_ReturnValue");
        if (Result == "0")
        {
            btnSearch_Click(sender, e);
        }
        else
        {
            msg += Result[1];
        }
        #endregion 
        if (msg.Length > 0)
        {
            Alert(msg);
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string ids = (sender as LinkButton).CommandArgument;
        this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmINBILL_DEdit.aspx", SYSOperation.Modify, txtID.Text.Trim()) + "&IDS=" + ids + "&Inasn_id=" + txtINASN_id.Text.Trim() + "&InType=" + ddlIType.SelectedValue + "&TradeCode=" + txtCDEFINE1.Text.Trim() + "&Currency=" + txtCDEFINE2.Text.Trim() + "&CreateType=" + hdnCreateType.Value.Trim() + "','入庫單明細','INBILL_D');");
    }

    /// <summary>
    /// 设置为同一储位
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSetCARGOSPACE_Click(object sender, EventArgs e)
    {

        #region 调用存储过程
        List<string> SparaList = new List<string>();
        SparaList.Add("@P_Bill_Id:" + txtID.Text.Trim());
        string Result = DBHelp.ExecuteProcReturnValue("Proc_SetBillCARGOSPACE", SparaList, "@P_ReturnValue");
        if (Result == "0")
        {
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        else
        {
            //设置失败
            Alert(Resources.Lang.FrmINBILLEdit_MSG65+"!");
        }
        #endregion 
    }

    /// <summary>
    /// 扣帐
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInSTOCK_CURRENT_Click(object sender, EventArgs e)
    {
        //var EmptyCount = this.grdINBILL_D.Rows.Cast<GridViewRow>().Where(
        //       dr => (dr.FindControl("txtGv_CPOSITIONCODE") as TextBox).Text.Trim().IsNullOrEmpty()).Select(dr => dr).
        //       Count();
        //if (EmptyCount > 0)
        //{
        //    Alert("存在未选择储位明细");
        //    btnInSTOCK_CURRENT.Style.Remove("disabled");
        //    btnInSTOCK_CURRENT.Enabled = true;
        //    return;
        //}

        btnInSTOCK_CURRENT.Enabled = false;//Roger 2013-4-24 18:33:44
        string msg = string.Empty;
        string guid = Guid.NewGuid().ToString();
        if (InBill.Check_Proc_OutBillTOStock_Currnt(txtID.Text.Trim(), WmsWebUserInfo.GetCurrentUser().UserNo, guid, ref msg))
        {
            #region 调用存储过程
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_InBillId:" + txtID.Text.Trim());
            SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo.Trim());
            SparaList.Add("@P_ReturnValue:" + "");
            SparaList.Add("@INFOTEXT:" + "");
            string[] Result = DBHelp.ExecuteProc("Proc_InBillTOSTOCK_CURRENT", SparaList);
            if (Result.Length == 1)//调用存储过程有错误
            {
                //扣账失败
                msg += "[" + txtCTICKETCODE.Text.Trim() + "]" + Resources.Lang.FrmINBILLEdit_MSG67 + "!";
                btnInSTOCK_CURRENT.Enabled = true;//Roger 2013-4-24 18:33:44
                //20130702084429
                btnInSTOCK_CURRENT.Style.Remove("disabled");
            }
            else if (Result[0] == "0")
            {
                //扣账成功
                msg = "[" + txtCTICKETCODE.Text.Trim() + "]" + Resources.Lang.FrmINBILLEdit_MSG68 + "!";
                Page_Load_Function();
            }
            else
            {
                //扣账失败
                msg += "[" + txtCTICKETCODE.Text.Trim() + "]" + Result[1] + Resources.Lang.FrmINBILLEdit_MSG67 +  "!";
                btnInSTOCK_CURRENT.Enabled = true;//Roger 2013-4-24 18:33:44
                //20130702084429
                btnInSTOCK_CURRENT.Style.Remove("disabled");
            }
            #endregion 
          
        }
        else
        {
            btnInSTOCK_CURRENT.Enabled = true;//Roger 2013-4-24 18:33:44
            //20130702084429
            btnInSTOCK_CURRENT.Style.Remove("disabled");
        }
        Alert(msg);
    }
    /// <summary>
    /// ASRS过账
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInSTOCK_ASRS_Click(object sender, EventArgs e)
    {
        //var EmptyCount = this.grdINBILL_D.Rows.Cast<GridViewRow>().Where(
        //        dr => (dr.FindControl("txtGv_CPOSITIONCODE") as TextBox).Text.Trim().IsNullOrEmpty()).Select(dr => dr).
        //        Count();
        //if (EmptyCount > 0)
        //{
        //    Alert("存在未选择储位明细");
        //    btnInSTOCK_CURRENT.Style.Remove("disabled");
        //    btnInSTOCK_CURRENT.Enabled = true;
        //    return;
        //}

        btnInSTOCK_ASRS.Enabled = false;//Roger 2013-4-24 18:33:44
        string msg = string.Empty;
        string guid = Guid.NewGuid().ToString();
        if (InBill.Check_Proc_OutBillTOStock_Currnt(txtID.Text.Trim(), WmsWebUserInfo.GetCurrentUser().UserNo, guid, ref msg))
        {
            #region 调用存储过程
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_InBillId:" + txtID.Text.Trim());
            SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo.Trim());
            SparaList.Add("@P_ReturnValue:" + "");
            SparaList.Add("@INFOTEXT:" + "");
            string[] Result = DBHelp.ExecuteProc("Proc_InBillTOSTOCK_CURRENT", SparaList);
            if (Result.Length == 1)//调用存储过程有错误
            {
                //扣账失败
                msg += "[" + txtCTICKETCODE.Text.Trim() + "]" + Resources.Lang.FrmINBILLEdit_MSG67 + "!\r\n";
                btnInSTOCK_ASRS.Enabled = true;//Roger 2013-4-24 18:33:44
                //20130702084429
                btnInSTOCK_ASRS.Style.Remove("disabled");
            }
            else if (Result[0] == "0")
            {
                //扣账成功
                msg = "[" + txtCTICKETCODE.Text.Trim() + "]" + Resources.Lang.FrmINBILLEdit_MSG68 + "!";
                //pan gao led web test
                //string ledWEB = ConfigurationManager.AppSettings["LEDWEB"];
                //if (ledWEB == "1")
                //{
                //    LEDSendText led = new LEDSendText(Server.MapPath("~/EQ2008_Dll_Set.ini"));
                //    led.LED_ShowCompleted();
                //}
                Page_Load_Function();
            }
            else
            {
                //扣账失败
                msg += "[" + txtCTICKETCODE.Text.Trim() + "]" + Resources.Lang.FrmINBILLEdit_MSG67 + "!\r\n";
                btnInSTOCK_ASRS.Enabled = true;//Roger 2013-4-24 18:33:44
                //20130702084429
                btnInSTOCK_ASRS.Style.Remove("disabled");
            }
            #endregion 
            #region 注销
       
         #endregion
        }
        else
        {
            btnInSTOCK_ASRS.Enabled = true;//Roger 2013-4-24 18:33:44
            //20130702084429
            btnInSTOCK_ASRS.Style.Remove("disabled");
        }
        Alert(msg);
    }
    protected void ddlIType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //103 工单退料
        if (ddlIType.SelectedValue.Equals("103"))
        {
            this.btnSetCARGOSPACE.Enabled = false;
        }
    }

    /// 刷新全部按钮
    /// <summary>
    /// 刷新全部按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRefreshAll_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = string.Empty;
            string id = txtID.Text.Trim();
            if (!WmsDBCommon_ASRS.ASRS_InRefresh_All(id, out msg))
            {
                Alert(msg);
            }
            btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }

    /// 入库按钮判断
    /// <summary>
    /// 入库按钮判断
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInbill_Click(object sender, EventArgs e)
    {
        this.btnInbill.Enabled = false;
        string ReturnMsg = string.Empty;
        try
        {

            if (WmsDBCommon_ASRS.DataBaseConn(out ReturnMsg))
            {
                //处理数据
                for (int i = 0; i < this.grdINBILL_D.Rows.Count; i++)
                {
                    if (this.grdINBILL_D.Rows[i].Cells[0].FindControl("chkSelect") is CheckBox)
                    {
                        CheckBox chkSelect = (CheckBox)this.grdINBILL_D.Rows[i].Cells[0].FindControl("chkSelect");
                        if (chkSelect.Checked)
                        {
                            string ids = this.grdINBILL_D.DataKeys[i].Values[0].ToString();
                            string idsmsg = string.Empty;
                            IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
                            var caseList = from p in con.Get()
                                           where p.ids == ids
                                           select p;
                            INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
                            PROC_ASRS_InBill_All proc = new PROC_ASRS_InBill_All();
                            proc.Ids = ids;
                            proc.Lineid = entity.LineID.ToString();
                            proc.Space = entity.pallet_code;
                            proc.UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                            proc.Execute();
                            if (proc.ReturnValue == 1)
                            {
                                Alert(proc.ErrorMessage);
                            }
                            //if (!(WmsDBCommon_ASRS.ASRS_InBill_All(ids, entity.pallet_code,entity.LineID.ToString(), out idsmsg)))
                            //{
                            //    Alert(idsmsg);
                            //    break;
                            //}
                        }
                    }
                }
                WmsDBCommon_ASRS.DBConnClose();
            }
            else
            {
                Alert(ReturnMsg);

            }
            this.GridBind();
        }
        catch (Exception E)
        {
            //入库失败
            this.Alert( Resources.Lang.FrmINBILLEdit_MSG69+"！" + E.Message.ToJsString());
        }
        this.btnInbill.Enabled = true;
        btnInbill.Style.Remove("disabled");
    }

    /// 取消按钮
    /// <summary>
    /// 取消按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.btnCancel.Enabled = false;
        string ReturnMsg = string.Empty;
        try
        {

            if (WmsDBCommon_ASRS.DataBaseConn(out ReturnMsg))
            {
                //处理数据
                for (int i = 0; i < this.grdINBILL_D.Rows.Count; i++)
                {
                    if (this.grdINBILL_D.Rows[i].Cells[0].FindControl("chkSelect") is CheckBox)
                    {
                        CheckBox chkSelect = (CheckBox)this.grdINBILL_D.Rows[i].Cells[0].FindControl("chkSelect");
                        if (chkSelect.Checked)
                        {
                            string ids = this.grdINBILL_D.DataKeys[i].Values[0].ToString();
                            string idsmsg = string.Empty;
                            PROC_ASRS_InCancel_All proc = new PROC_ASRS_InCancel_All();
                            proc.Ids = ids;
                            proc.UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                            proc.Execute();
                            if (proc.ReturnValue == 1)
                            {
                                Alert(proc.ErrorMessage);
                            }
                            //if (!(WmsDBCommon_ASRS.ASRS_InCancel_All(ids, out idsmsg)))
                            //{
                            //    Alert(idsmsg);
                            //    break;
                            //}
                        }
                    }
                }
                WmsDBCommon_ASRS.DBConnClose();
            }
            else
            {
                Alert(ReturnMsg);

            }
            this.GridBind();
        }
        catch (Exception E)
        {
            //入库失败
            this.Alert( Resources.Lang.FrmINBILLEdit_MSG69+"！" + E.Message.ToJsString());
        }
        this.btnCancel.Enabled = true;
        btnCancel.Style.Remove("disabled");
    }

    //打印功能
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string printid = this.txtID.Text.Trim();
            //打印入库单
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINBILLEdit_Print.aspx", SYSOperation.New, printid) + "','" + Resources.Lang.FrmINBILLEdit_MSG70 + "','BAR_REPACK',840,600);");

        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }


    // WL20160511 追加空栈板入库操作
    protected void LinkSPACE_STATUS_I_Click(object sender, EventArgs e)
    {
        string ids = (sender as Button).CommandArgument;
        string msg = string.Empty;
        string Pallet = "";
        IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
        var caseList = from p in con.Get()
                       where p.ids == ids
                       select p;
        INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
        // 获得储位状态
        if (new InBill().CheckBASE_CARGOSPACE_OK(ids))
        {
            Pallet = "0";
        }
        else
        {
            Pallet = "1";
        }
        if (!WmsDBCommon_ASRS.ASRS_InChangeStatus_S_I(ids, Pallet,entity.LineID.ToString(), out msg))
        {
            Alert(msg);
        }
        btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    protected void LinkSPACE_STATUS_O_Click(object sender, EventArgs e)
    {
        //pan gao 注释
        //空栈板出库，AS/RS入库整合到一个事件中
        //string ids = (sender as Button).CommandArgument;
        //string msg = string.Empty;
        //string Pallet = "";
        //// 获得储位状态
        //RD_FrmINBILL_DListQuery RD_INBLL = new RD_FrmINBILL_DListQuery();
        //if (RD_INBLL.CheckBASE_CARGOSPACE_OK(ids))
        //{
        //    Pallet = "0";
        //}
        //else
        //{
        //    Pallet = "1";
        //}
        //if (!WmsDBCommon_ASRS.ASRS_InChangeStatus_S_O(ids, Pallet, out msg))
        //{
        //    Alert(msg);
        //}

        //btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    /// <summary>
    /// 空栈板出库，AS/RS入库整合到一个事件中
    /// 把空栈板出库的方法复制到这里，与入库一起调用
    /// </summary>
    private bool SpaceOut(string ids)
    {
        bool bl = false;
        string msg = string.Empty;
        string Pallet = "";
        IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
        var caseList = from p in con.Get()
                       where p.ids == ids
                       select p;
        INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
        // 获得储位状态
        if (new InBill().CheckBASE_CARGOSPACE_OK(ids))
        {
            Pallet = "0";
            return true;//如果库位没有栈板，不用做空栈板出库
        }
        else
        {
            Pallet = "1";
        }
        if (!WmsDBCommon_ASRS.ASRS_InChangeStatus_S_O(ids, Pallet,entity.LineID.ToString(), out msg))
        {
            Alert(msg);
        }
        else
        {
            bl = true;//空栈板出库成功
        }
        return bl;
    }
  
            
   #endregion
    #endregion


    /// 刷新全部按钮
    /// <summary>
    /// 刷新全部按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSeral_Click(object sender, EventArgs e)
    {
        string code = txtCTICKETCODE.Text;
        this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("../BAR/FrmSeralEdit.aspx", SYSOperation.Modify, "" + "&TYPE=IN&CODE=" + code) + "','序列号管理','SERALCONTROL',800,700);");
    }

}

          

