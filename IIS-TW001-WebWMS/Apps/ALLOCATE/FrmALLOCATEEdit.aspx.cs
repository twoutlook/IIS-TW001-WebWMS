using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Text;
using System.ComponentModel;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using DreamTek.ASRS.Business.SP.ProcedureModel;
using DreamTek.ASRS.Business.Base;
using DreamTek.ASRS.Business.Allocate;
using DreamTek.ASRS.Business.Others;
using System.Data.SqlClient;
using Resources;

/// <summary>
/// 描述: 1111-->FrmALLOCATEEdit 页面后台类文件
/// 作者: --chenlei
/// 创建于: 2012-11-01 09:29:23
/// </summary>
/*
Roger
2013/7/2 8:44:29
20130702084429
解决狂点按钮重复提交问题
*/
public partial class ALLOCATE_FrmALLOCATEEdit : WMSBasePage
{

    private string MainStatus
    {
        get
        {
            if (ViewState["MainStatus"] == null)
            {
                return ViewState["MainStatus"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            ViewState["MainStatus"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();

            //储存调拨类型
            if (!string.IsNullOrEmpty(Request["AllocateType"]))
            {
                Session["AllocateType"] = Request["AllocateType"].ToString();
            }            
            if (this.Operation() == SYSOperation.Modify)
            {
                ShowData();
                if (!string.IsNullOrEmpty(Request["AllocateType"]))
                {                    
                    if (!Request["AllocateType"].ToString().Equals("0"))
                    {
                        btnSave.Enabled = false;
                        btnSave.Attributes["style"] = "color:#b5b5b5";
                        btnNew.Enabled = false;
                        btnNew.Attributes["style"] = "color:#b5b5b5";
                        btnDelete.Enabled = false;
                        btnDelete.Attributes["style"] = "color:#b5b5b5";
                        btnOutput.Enabled = false;
                        btnOutput.Attributes["style"] = "color:#b5b5b5";
                        btnCancle.Enabled = false;
                        btnCancle.Attributes["style"] = "color:#b5b5b5";
                    }
                }
            }
            else if (this.Operation() == SYSOperation.Preserved1)
            {
                ShowData();
                this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmALLOCATE_DEdit.aspx", SYSOperation.New, "") + "&parentId=" + this.KeyID + "','','ALLOCATE_D');");//Roger 2013/12/25 13:59:12
            }
            else
            {                
                txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// HH:mm:ss
                txtDINDATE.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// HH:mm:ss
                txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo); //WmsWebUserInfo.GetCurrentUser().UserNo
            }
            //调拨单限制
            //if (allo.CheckPoint(WmsWebUserInfo.GetCurrentUser().UserNo))
            //{
            //    ddlallow.Enabled = true;
            //}
            //if (ddlCSTATUS.SelectedValue != "0")
            //{
            //    ddlallow.Enabled = false;
            //}

            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        if (ddlCSTATUS.SelectedValue == "0")
        {
            btnImportExcel.Enabled = true;
        }
        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
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
        btnCreateInOutBill.Enabled = false;
        //this.ckbIstockType.DataSource = GetAllocateType();
        //this.ckbIstockType.DataTextField = "flag_name";
        //this.ckbIstockType.DataValueField = "flag_id";
        //this.ckbIstockType.DataBind();
        //if (this.Operation() == SYSOperation.New)
        //    this.ckbIstockType.SelectedIndex = 0;
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('ALLOCATE');return false;";
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.Operation() == SYSOperation.Approve)
        {
            this.btnSave.Text = Lang.FrmALLOCATEEdit_Msg08;//Resources.Lang.FrmALLOCATEEdit_Msg08;//审批
        }
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdALLOCATE_D.DataKeyNames = new string[] { "IDS" };

        //本页面打开新增窗口
        //this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + PageBase.BuildRequestPageURL("FrmALLOCATE_DList.aspx", SysOperation.New, "") + "','新建调拨单','ALLOCATE_D');return false;";
        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmALLOCATE_DEdit.aspx", SysOperation.New,""),800,600);
        //是否显示ASRS 1显示 0 不显示
        ASRSFig = CommFunction.GetConFig("000006");
        if (ASRSFig == "1")
        {
            //btnOutput.Visible = true;
            grdALLOCATE_D.Columns[10].Visible = true;
            grdALLOCATE_D.Columns[11].Visible = true;
            grdALLOCATE_D.Columns[12].Visible = true;
        }
        else
        {
            //btnOutput.Visible = false;
            grdALLOCATE_D.Columns[10].Visible = false;
            grdALLOCATE_D.Columns[11].Visible = false;
            grdALLOCATE_D.Columns[12].Visible = false;
        }

        var allotypeList = SysParameterList.GetList("", "", "AllocateType", false, -1, -1);
        //業務類型
        Help.CheckBoxListDataBind(allotypeList, this.ckbIstockType, "", "FLAG_NAME", "FLAG_ID", "", RepeatDirection.Vertical);
        this.ckbIstockType.RepeatColumns = allotypeList.Count;
        //调拨单状态
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "ALLOCATE", false, -1, -1), this.ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");

        //yes or no
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "YesOrNo", false, -1, -1), this.ddlallow, "", "FLAG_NAME", "FLAG_ID", "");
        if (this.Operation() == SYSOperation.New)
            this.ckbIstockType.SelectedIndex = 0;
    }

    #endregion
  
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

    public bool AllocateFromOutBill
    {
        get
        {
            if (ViewState["AllocateFromOutBill"] == null)
            {
                ViewState["AllocateFromOutBill"] = false;
            }
            return Convert.ToBoolean(ViewState["AllocateFromOutBill"]);
        }
        set { ViewState["AllocateFromOutBill"] = value; }
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
        var caseList = from p in conn.Get()
                       where p.id == this.KeyID
                       select p;
        ALLOCATE entity = caseList.ToList().FirstOrDefault();
        if (entity != null)  //BUCKINGHA-894
        {
            entity.id = this.KeyID.Trim();
            this.txtID.Text = entity.id.ToString();
            this.txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(entity.ccreateownercode);
            this.txtDCREATETIME.Text = entity.dcreatetime.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtCAUDITPERSON.Text = OPERATOR.GetUserNameByAccountID(entity.cauditperson);
            this.txtDAUDITTIME.Text = entity.daudittime.HasValue ? entity.daudittime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtCTICKETCODE.Text = entity.cticketcode;
            this.ddlCSTATUS.SelectedValue = entity.cstatus;
            this.txtCERPCODE.Text = entity.cerpcode;
            this.txtCMEMO.Text = entity.cmemo;
            this.txtDINDATE.Text = entity.dindate.ToString("yyyy-MM-dd HH:mm:ss");
            ddlallow.SelectedValue = entity.is_allow.HasValue ? entity.is_allow.Value.ToString() : "";
            txtpaozhuantime.Text = entity.ddefine3.HasValue ? entity.ddefine3.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            txtdebittime.Text = entity.debittime.HasValue ? entity.debittime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            txtdebituser.Text = OPERATOR.GetUserNameByAccountID(entity.debitowner);
            this.ckbIstockType.SelectedValue = entity.ALLOTYPE;


            Status = entity.cstatus;
            if (entity.cstatus != "0")
            {
                this.btnDelete.Enabled = false;
                this.btnNew.Enabled = false;
                this.btnSave.Enabled = false;

                imgDINDATE.Visible = false;
                this.txtID.Enabled = false;
                this.txtCCREATEOWNERCODE.Enabled = false;
                this.txtDCREATETIME.Enabled = false;
                this.txtCAUDITPERSON.Enabled = false;
                this.txtDAUDITTIME.Enabled = false;
                this.txtCTICKETCODE.Enabled = false;
                this.ddlCSTATUS.Enabled = false;
                this.txtCERPCODE.Enabled = false;
                this.txtCMEMO.Enabled = false;
                this.txtDINDATE.Enabled = false;
                this.btnImportExcel.Enabled = false;
            }
            if (entity.cstatus == "0")
            {
                this.btnImportExcel.Enabled = true;
                this.btnImportExcel.Attributes["onclick"] = "PopupFloatWin('" + BuildRequestPageURL("/Apps/Import/FrmImportAllocateDetail.aspx", SYSOperation.New, "") + "&AllocateId=" + this.KeyID + "&CTICKETCODE=" + txtCTICKETCODE.Text.Trim() + "&ImportType=allocate','上传調撥單明细','Allocate_D',600,320); return false;";
            }

            ckbIstockType.Enabled = false;
            if (Status == "1")
            {
                btnOutput.Enabled = true;
            }
            else
            {
                btnCancle.Enabled = false;
                btnOutput.Enabled = false;
                //btnRefres.Enabled = false;
            }
            AllocateQuery allQry = new AllocateQuery();
            if (allQry.AllocateFromOutBill(txtID.Text))
            {
                btnNew.Enabled = false;
                btnDelete.Enabled = false;
                AllocateFromOutBill = true;
            }

            if (entity.cdefine1 == "3")
            {
                btnNew.Enabled = false;
                btnDelete.Enabled = false;
                btnCancle.Enabled = false;
            }

            GridBind();

            //暂存=>立库 ,都不能操作
            if (entity.ALLOTYPE == "4") {
                btnSave.Enabled = false;
                btnNew.Enabled = false;
                btnDelete.Enabled = false;
                btnCancle.Enabled = false;
                btnOutput.Enabled = false;
            }



        }
    }


    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {       
        if (Operation() == SYSOperation.Modify)
        {
            IGenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
            ALLOCATE bo = (from p in conn.Get()
                           where p.id == this.KeyID
                           select p
                          ).FirstOrDefault();
            if (bo != null && !string.IsNullOrEmpty(bo.cstatus) && !bo.cstatus.Equals("0"))
            {
                this.Alert(Lang.FrmALLOCATEEdit_Msg10);//此調撥單不是未處理狀態，不能添加明細！
                return false;
            }
        }
        //
        if (this.txtCCREATEOWNERCODE.Text.Trim() == "")
        {
            this.Alert(Lang.FrmALLOCATEEdit_Msg11);//制单人项不允许空！
            this.SetFocus(txtCCREATEOWNERCODE);
            return false;
        }
        ////
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (OPERATOR.GetUserIDByUserName(this.txtCCREATEOWNERCODE.Text).GetLengthByByte() > 100)
            {
                this.Alert(Lang.FrmALLOCATEEdit_Msg12);//制单人项超过指定的长度100！
                this.SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }
        ////
        if (this.txtCAUDITPERSON.Text.Trim().Length > 0)
        {
            if (OPERATOR.GetUserIDByUserName(this.txtCAUDITPERSON.Text).GetLengthByByte() > 100)
            {
                this.Alert(Lang.FrmALLOCATEEdit_Msg13);//审核人项超过指定的长度100！
                this.SetFocus(txtCAUDITPERSON);
                return false;
            }
        }
        ////
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
            if (this.txtCERPCODE.Text.GetLengthByByte() > 30)
            {
                this.Alert(Lang.FrmALLOCATEEdit_Msg14);//ERP单号项超过指定的长度30！
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        ////
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 200)
            {
                this.Alert(Lang.FrmALLOCATEEdit_Msg15);//备注项超过指定的长度200！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        //
        if (this.txtDINDATE.Text.Trim() == "")
        {
            this.Alert(Lang.FrmALLOCATEEdit_Msg16);//调拨日期项不允许空！
            this.SetFocus(txtDINDATE);
            return false;
        }

        if (this.ckbIstockType.SelectedValue == "4") {
            this.Alert("该类型的调拨单不允许新增,请在PDA上操作！");
            this.SetFocus(ckbIstockType);
            return false;
        }

        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public ALLOCATE SendData()
    {
        ALLOCATE entity = new ALLOCATE();
        this.txtID.Text = this.txtID.Text.Trim();
        if (this.txtID.Text.Length > 0)
        {

            entity.id = txtID.Text.Trim();
            IGenericRepository<ALLOCATE> Mall = new GenericRepository<ALLOCATE>(db);
            var bo = (from p in Mall.Get()
                      where p.id == txtID.Text.Trim()
                      select p).FirstOrDefault();
            if (bo != null && !string.IsNullOrEmpty(bo.id))
            {
                entity = bo;
            }
        }

        entity.ccreateownercode = WmsWebUserInfo.GetCurrentUser().UserNo;
        entity.dcreatetime = txtDCREATETIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        this.txtCAUDITPERSON.Text = this.txtCAUDITPERSON.Text.Trim();
        if (this.txtCAUDITPERSON.Text.Length > 0)
        {
            entity.cauditperson = OPERATOR.GetUserIDByUserName(txtCAUDITPERSON.Text);
        }

        //
        this.txtDAUDITTIME.Text = this.txtDAUDITTIME.Text.Trim();
        if (this.txtDAUDITTIME.Text.Length > 0)
        {
            entity.daudittime = txtDAUDITTIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");//Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        //
        this.txtCTICKETCODE.Text = this.txtCTICKETCODE.Text.Trim();
        if (this.txtCTICKETCODE.Text.Length > 0)
        {
            entity.cticketcode = txtCTICKETCODE.Text;
        }

        entity.cstatus = ddlCSTATUS.SelectedValue.Trim();
        this.txtCERPCODE.Text = this.txtCERPCODE.Text.Trim();
        if (this.txtCERPCODE.Text.Length > 0)
        {
            entity.cerpcode = txtCERPCODE.Text;
        }

        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }

        //
        this.txtDINDATE.Text = this.txtDINDATE.Text.Trim();
        if (this.txtDINDATE.Text.Length > 0)
        {
            if (this.txtDINDATE.Text.Length == 19)
            {
                entity.dindate = Convert.ToDateTime(txtDINDATE.Text.Trim());
            }

        }
        decimal da = 0;
        if (decimal.TryParse(ddlallow.SelectedValue, out da))
        {
            entity.is_allow = da;
        }
        for (int i = 0; i < ckbIstockType.Items.Count; i++)
        {
            if (ckbIstockType.Items[i].Selected)
                entity.ALLOTYPE = ckbIstockType.Items[i].Value;
        }
        entity.cdefine1 = "0";     

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
        SaveDataToDB(sender);
        this.btnSave.Enabled = true;
        //20130702084429
        btnSave.Style.Remove("disabled");
    }

    private void SaveDataToDB(object sender)
    {
        if (this.CheckData())
        {
            ALLOCATE entity = (ALLOCATE)this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";
            try
            {
                GenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
                if (this.Operation() == SYSOperation.Modify || SYSOperation.Preserved1 == this.Operation())
                {
                    strKeyID = txtID.Text.Trim();
                    entity.id = txtID.Text.Trim();

                    conn.Update(entity);
                    conn.Save();
                    this.AlertAndBack("FrmALLOCATEEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Lang.Common_SuccessSave);//保存成功
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    entity.cticketcode = new Fun_CreateNo_Wms().CreateNo("ALLOCATE");
                    strKeyID = Guid.NewGuid().ToString();
                    entity.id = strKeyID;
                    entity.special = 0;
                    entity.special_type = 0;
                    entity.is_merge = 0;
                    entity.critical_part = 0;
                    entity.cmemo = entity.cmemo + entity.cticketcode;
                    conn.Insert(entity);
                    conn.Save();
                    this.AlertAndBack("FrmALLOCATEEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Lang.Common_SuccessSave);//保存成功
                }
                if ((sender as Button).ID == "btnNew")
                {
                    Response.Redirect("FrmALLOCATEEdit.aspx?" + BuildQueryString(SYSOperation.Preserved1, strKeyID));
                }
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + Lang.FrmALLOCATEEdit_Msg17 + E.Message); //失败！
            }
        }
    }


    public void GridBind()
    {
        Bind("");
    }

    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {      
        //BUCKINGHA-894
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
        }        
        this.GridBind();
        IsFirstPage = true;//恢复默认值
       
        //BUCKINGHA-894
        //this.GridBind();
    }
   
    public ALLOCATE GetAllocate()
    {
        ALLOCATE bo = new ALLOCATE();
        if (!string.IsNullOrEmpty(this.KeyID))
        {
            IGenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
            bo = (from p in conn.Get()
                  where p.id == this.KeyID
                  select p).FirstOrDefault();
        }
        return bo;
    }

    public ALLOCATE GetAllocate(string Id)
    {
        ALLOCATE bo = new ALLOCATE();
        if (!string.IsNullOrEmpty(this.KeyID))
        {
            IGenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
            bo = (from p in conn.Get()
                  where p.id == Id
                  select p).FirstOrDefault();
        }
        return bo;
    }

    /// <summary>
    /// 鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        //DBUtil.BeginTrans();
        try
        {
            IGenericRepository<ALLOCATE_D> conn = new GenericRepository<ALLOCATE_D>(db);
            for (int i = 0; i < this.grdALLOCATE_D.Rows.Count; i++)
            {
                if (this.grdALLOCATE_D.Rows[i].FindControl("chkSelect") is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdALLOCATE_D.Rows[i].FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        //ALLOCATE_FrmALLOCATE_DListQuery listallo = new ALLOCATE_FrmALLOCATE_DListQuery();
                        var bo = GetAllocate(this.txtID.Text.Trim());
                        //if (listallo.CheckAlloStatus(this.txtID.Text.Trim()))
                        if (!string.IsNullOrEmpty(bo.cstatus) && !bo.cstatus.Equals("0"))
                        {
                            msg = Lang.FrmALLOCATEEdit_Msg18;//此調撥單不是未處理狀態，不能删除明細！
                        }
                        else
                        {
                            //ALLOCATE_DEntity entity = new ALLOCATE_DEntity();
                            ////主键赋值
                            //entity.IDS = this.grdALLOCATE_D.DataKeys[i].Values[0].ToString().Trim();
                            //ALLOCATE_DRule.Delete(entity);	//执行动作 
                            string ids = this.grdALLOCATE_D.DataKeys[i].Values[0].ToString().Trim();
                            ALLOCATE_D dBO = (from p in conn.Get()//  conn.GetByID(this.grdALLOCATE_D.DataKeys[i].Values[0].ToString().Trim());
                                              where p.ids == ids
                                              select p).FirstOrDefault();
                            if (bo.ALLOTYPE == "0")
                            {
                                if (dBO != null && dBO.asrs_status.HasValue)
                                {
                                    if (dBO.asrs_status.Value != 0)
                                    {
                                        msg += Lang.FrmALLOCATEList_ExCommondNoDel;//已经产生AS/RS命令，不能删除
                                    }
                                    else
                                    {
                                        conn.Delete(ids);
                                        conn.Save();
                                    }
                                }
                            }
                            else if (bo.ALLOTYPE == "1")
                            {
                                conn.Delete(ids);
                                conn.Save();
                            }
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Lang.Common_SuccessDel; //删除成功！
            }
            this.Alert(msg);
            //DBUtil.Commit();
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Lang.Common_FailDel + E.Message.ToJsString());//"删除失败！
            //DBUtil.Rollback();
        }
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdALLOCATE_D.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdALLOCATE_D.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    public void Bind(string sortStr)
    {
        var caseList = GetQueryList();

        if (caseList != null && caseList.Count() > 0)
        {
            //按照列名排序
            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }
            var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
            var source = from p in data
                         join oper in db.BASE_PART on p.cinvcode equals oper.cpartnumber into temp
                         from tt in temp.DefaultIfEmpty()
                         select new
                         {
                             p.ids
                            ,
                             p.id
                            ,
                             p.cstatus
                            ,
                             p.iquantity
                            ,
                             p.cpositioncode
                            ,
                             p.cposition
                            ,
                             p.cinvbarcode
                            ,
                             p.cinvcode
                            ,
                             p.cinvname
                            ,
                             p.dindate
                            ,
                             p.cinpersoncode
                            ,
                             p.cmemo
                            ,
                             p.cdefine1
                            ,
                             p.cdefine2
                            ,
                             p.ddefine3
                            ,
                             p.ddefine4
                            ,
                             p.idefine5
                            ,
                             p.ctopositioncode
                            ,
                             p.ctoposition
                            ,
                             p.cmidpositioncode
                            ,
                             p.ioriquantity
                            ,
                             p.toallocount
                            ,
                             p.outbill_d_ids
                            ,
                             p.allo_type
                            ,
                             p.sptype
                            ,
                             p.cmidpositionname
                            ,
                             p.is_upline
                            ,
                             p.is_scan
                            ,
                             p.part_bond
                            ,
                             p.critical_part
                            ,
                             p.asrs_status
                            ,
                             p.wmstskid
                            ,
                             p.asrs_num
                            ,
                             p.lineid
                            ,
                             cspecifications = tt == null ? "" : tt.cspecifications
                             //28-10-2020 by Qamar
                             ,
                             part = p.cinvcode.Substring(0, p.cinvcode.Length - 2)
                             ,
                             rank_final = p.cinvcode.Substring(p.cinvcode.Length - 1, 1)
                         };


            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
            grdALLOCATE_D.DataSource = source.ToList();
            grdALLOCATE_D.DataBind();
        }
        else
        {
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
            grdALLOCATE_D.DataSource = null;
            grdALLOCATE_D.DataBind();
        }
        
    }

    public IQueryable<ALLOCATE_D> GetQueryList()
    {
        if (!string.IsNullOrEmpty(txtID.Text.Trim()))
        {
            IGenericRepository<ALLOCATE_D> conn = new GenericRepository<ALLOCATE_D>(db);
           
            var caseList = from p in conn.Get()                          
                           orderby p.dindate descending
                           where p.id == txtID.Text.Trim()
                           select p;
            //29-10-2020 by Qamar
            if (txtCinvcode.Text.Trim() != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Substring(0, x.cinvcode.Length - 2).Contains(txtCinvcode.Text.Trim()));
            }
            if (txtRANK_FINAL.Text.Trim() != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Substring(x.cinvcode.Length - 1, 1) == txtRANK_FINAL.Text.Trim().ToUpper());
            }
            return caseList;
        }
        else
        {
            return null;
        }

    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    protected void grdALLOCATE_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];

            Button btnRefresh = (Button)e.Row.FindControl("btnRefresh");

            //if (Status == "0")
            //{
            linkModify.NavigateUrl = "#";
            //this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmALLOCATE_DEdit.aspx", SysOperation.Modify, strKeyID), "调拨单", "ALLOCATE_D", 800, 600);
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmALLOCATE_DEdit.aspx", SYSOperation.Modify, strKeyID), "" + Lang.FrmALLOCATEList_Title1+ "", "ALLOCATE_D");//Roger 2013/12/25 13:59:12  //调拨单
            //}
            //else
            //{
            //    linkModify.Enabled = false;
            //}


            //((LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0]).Enabled = e.Row.Cells[e.Row.Cells.Count - 3].Text == "0" ? false : true;
            if (ASRSFig == "1" && (ckbIstockType.SelectedValue == "0" || ckbIstockType.SelectedValue == "4"))
            {
                #region AS/RS
                //修改状态
                Button LinkASRS_STATUS = (Button)e.Row.FindControl("LinkASRS_STATUS");
                LinkButton lnkStatus = (LinkButton)e.Row.FindControl("lnkStatus");

                LinkASRS_STATUS.Style.Add("color", "Blue");


                //if (e.Row.Cells[11].Text == "1")
                //{
                //    LinkASRS_STATUS.Enabled = false;
                //}

                //e.Row.Cells[e.Row.Cells.Count - 4].Text = "重试";
                if (e.Row.Cells[12].Text == "7")
                {
                    LinkASRS_STATUS.Enabled = false;
                    LinkASRS_STATUS.Visible = false;
                }
                switch (LinkASRS_STATUS.Text.Trim())//10
                {
                    case "0":
                        LinkASRS_STATUS.Text = Lang.FrmALLOCATEEdit_Msg19;//[调拨]
                        break;
                    case "1":
                        LinkASRS_STATUS.Text = Lang.FrmALLOCATEEdit_Msg20;//[运作中]
                        LinkASRS_STATUS.Enabled = false;
                        break;
                    case "7":
                        // btnRefres.Enabled = false;
                        if (Status == "1")
                        {
                            btnOutput.Enabled = true;
                        }
                        else
                        {
                            btnOutput.Enabled = false;
                        }
                        btnCancle.Enabled = false;
                        LinkASRS_STATUS.Visible = false;
                        break;
                    case "8":
                        LinkASRS_STATUS.Text = Lang.FrmALLOCATEEdit_Msg21;//[重试]
                        break;
                }

                switch (lnkStatus.Text)//11
                {
                    case "0":
                        lnkStatus.Text = Lang.Common_ASRSStatus01;//未处理
                        break;
                    case "1":
                        lnkStatus.Text = Lang.Common_ASRSStatus02;//"运作中";
                        break;
                    case "7":
                        lnkStatus.Text =Lang.Common_ASRSStatus03; //"处理完成";
                        lnkStatus.Enabled = false;
                        break;
                    case "8":
                        lnkStatus.Text = Lang.Common_ASRSStatus04; //"处理异常";
                        break;
                }
                #endregion

                if (AllocateFromOutBill)
                {
                    LinkASRS_STATUS.Enabled = false;
                }
                //if (ddlCSTATUS.SelectedValue != "0")
                //{
                //    LinkASRS_STATUS.Enabled = false;

                //}
            }
            else
            {
                Button LinkASRS_STATUS = (Button)e.Row.FindControl("LinkASRS_STATUS");
                LinkButton lnkStatus = (LinkButton)e.Row.FindControl("lnkStatus");
                Button LinkRefresh = (Button)e.Row.FindControl("btnRefresh");
                LinkRefresh.Visible = false;
                LinkASRS_STATUS.Visible = false;
                lnkStatus.Visible = false;
            }
            e.Row.Cells[e.Row.Cells.Count - 2].Text = e.Row.Cells[e.Row.Cells.Count - 2].Text == "0" ? Lang.Common_Status01 : Lang.Common_Status02;//未处理:已完成

            //28-10-2020 by Qamar
            if (e.Row.Cells[7].Text == "_")
                e.Row.Cells[7].Text = "";

            //物料名称
            var partName = e.Row.Cells[8].Text;
            if (!string.IsNullOrEmpty(partName) && partName.Length > 20)
            {
                e.Row.Cells[8].Text = partName.Substring(0, 20) + "...";
            }

            if (ddlCSTATUS.SelectedValue != "0")
            {
                e.Row.Cells[e.Row.Cells.Count - 2].Text = Lang.Common_Status02;//已完成
                // btnRefresh.Enabled = false;
            }
            //调拨人
            e.Row.Cells[17].Text = OPERATOR.GetUserNameByAccountID(e.Row.Cells[17].Text);
        }
    }
    //修改状态：
    //pan gao 执行AS/RS调拨命令
    protected void LinkASRS_STATUS_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = (sender as Button).CommandArgument;
            string msg = string.Empty;
            string lineId = "1";//SP通过储位来判断线别
            PROC_ASRS_AlloChangeStatus proc = new PROC_ASRS_AlloChangeStatus();
            proc.Ids = ids;
            proc.Space = "1";
            proc.Lineid = lineId;
            proc.UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
            proc.Execute();
            if (proc.ReturnValue == 1)
            {
                Alert(proc.ErrorMessage);
            }
            //BUCKINGHA-769 这个逻辑放在了脚本里了。
//            else
//            {
//                //将栈板号更新至asrs中
//                string strSQL = string.Format(@"UPDATE  dbo.CMD_MST
//                                            SET     PACKAGENO = ( SELECT    ISNULL(palletcode, '')
//                                                                  FROM      dbo.STOCK_CURRENT
//                                                                  WHERE     cpositioncode = ( SELECT TOP 1
//                                                                                                      cpositioncode
//                                                                                              FROM    dbo.ALLOCATE_D
//                                                                                              WHERE   ids = '{0}'
//                                                                                            )
//                                                                )
//                                            WHERE   CTICKETCODE = '{1}'", ids, txtCTICKETCODE.Text.Trim());
//                SqlDBHelp.ExecuteNonQuery(strSQL);
//            }
            btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        catch (Exception E)
        {
            this.Alert(Lang.FrmALLOCATEEdit_Msg22 + E.Message.ToJsString());//调拨失败！
        }        
    }

    //刷新
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        string ids = (sender as Button).CommandArgument;

        //抓取更新SN的状态
        string msg = string.Empty;
        if (!SqlDBCommon_ASRS.ASRS_AlloRefresh(ids, out msg))
        {
            Alert(msg);
        }
        btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    protected void dsGrdALLOCATE_D_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdALLOCATE_D_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //if (e.ReturnValue is DataTable)
        //{
        //    if (grdNavigatorALLOCATE_D.IsDbPager == false)
        //        this.grdNavigatorALLOCATE_D.RowCount = ((DataTable)e.ReturnValue).Rows.Count;
        //}
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (this.txtCTICKETCODE.Text.Trim() == "")
        {
            this.Alert(Lang.FrmALLOCATEEdit_Msg23);//请先保存调拨单！
            this.SetFocus(txtCTICKETCODE);
        }
        else
        {
            IGenericRepository<ALLOCATE_D> conn = new GenericRepository<ALLOCATE_D>(db);
            var caseList = from p in conn.Get()
                           where p.id == txtID.Text
                           select p;
            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
            {
                caseList = caseList.Where(x => x.cinvcode.Equals(txtCinvcode.Text.Trim()));
            }
            //ALLOCATE_FrmALLOCATE_DListQuery listQuery = new ALLOCATE_FrmALLOCATE_DListQuery();
            //DataTable dtSource = new DataTable();
            //dtSource = listQuery.GetList_V(txtID.Text, txtCinvcode.Text.Trim(), false, this.grdNavigatorALLOCATE_D.CurrentPageIndex, this.grdALLOCATE_D.PageSize);
            //if (dtSource.Rows.Count >= 1)
            if (caseList != null && caseList.Count() > 0 && Session["AllocateType"]!=null && Session["AllocateType"].ToString()!="0") // Request["AllocateType"].ToString() != "0"  //处理参数跳转到第三页面后数据参数丢失
            {
                btnNew.Enabled = false;
                return;
            }
            SaveDataToDB(sender);
        }
    }

    /// <summary>
    /// 调拨
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateInOutBill_Click(object sender, EventArgs e)
    {
        //Proc_CreateAllocate proc = new Proc_CreateAllocate();
        //proc.P_Allocate_id = txtID.Text.Trim();
        //proc.Execute();
        //if (proc.P_ReturnValue == 0)
        //{
        //    Alert("调拨成功!");
        //}
        //else
        //{
        //    Alert("调拨失败!");
        //}
    }
    /// <summary>
    /// 调拨
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOutput_Click(object sender, EventArgs e)
    {

        //string msg = string.Empty;      
        //try
        //{
        //    string errmsg;
        //    int selectcount = 0;
        //    bool dbflag = false;
        //    //if (OracleDBCommon_ASRS.DataBaseConn(out errmsg))
        //    //{
        //    dbflag = true;
        //    //}

        //    //测试连接成功
        //    if (dbflag)
        //    {
        //        for (int i = 0; i < this.grdALLOCATE_D.Rows.Count; i++)
        //        {
        //            if (this.grdALLOCATE_D.Rows[i].FindControl("chkSelect") is CheckBox)
        //            {
        //                CheckBox chkSelect = (CheckBox)this.grdALLOCATE_D.Rows[i].FindControl("chkSelect");
        //                if (chkSelect.Checked)
        //                {
        //                    selectcount++;
        //                    string ids = this.grdALLOCATE_D.DataKeys[i].Values[0].ToString().Trim();
        //                    string allostatus = SqlDBCommon_ASRS.GetAlloStatus(ids);
        //                    if (allostatus.Equals("7"))
        //                    {
        //                        //pan gao
        //                        string positonFrom = grdALLOCATE_D.Rows[i].Cells[6].Text.Trim();
        //                        string positonTo = grdALLOCATE_D.Rows[i].Cells[8].Text.Trim();
        //                        //if (STOCK_CURRENTListQuery.ChangesPositionCodeForStock(positonFrom, positonTo, ids))
        //                        //{
        //                        //    this.Alert("调拨成功！");
        //                        //    this.btnOutput.Enabled = false;

        //                        //    string ledWEB = ConfigurationManager.AppSettings["LEDWEB"];
        //                        //    if (ledWEB == "1")
        //                        //    {
        //                        //        LEDSendText led = new LEDSendText(Server.MapPath("~/EQ2008_Dll_Set.ini"));
        //                        //        led.LED_ShowCompleted();
        //                        //    }

        //                        //}
        //                        //else
        //                        //{
        //                        //    this.Alert("调拨失败！");
        //                        //}
        //                    }
        //                    else
        //                    {
        //                        this.Alert("作业尚未完成，请稍等！");
        //                    }
        //                }                      
        //            }
        //            if (selectcount == 0)
        //            {
        //                this.Alert("至少选择一行");
        //                return;
        //            }
        //        }

        //        this.GridBind();
        //    }
        //    // OracleDBCommon_ASRS.DBConnClose();

        //}
        //catch (Exception E)
        //{
        //    this.Alert("调拨失败！" + E.Message.ToJsString());          
        //}
        //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);

    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        int selectcount = 0;     
        try
        {
            string errmsg;
            bool dbflag = false;        
            dbflag = true;          

            //测试连接成功
            if (dbflag)
            {
                for (int i = 0; i < this.grdALLOCATE_D.Rows.Count; i++)
                {
                    if (this.grdALLOCATE_D.Rows[i].FindControl("chkSelect") is CheckBox)
                    {
                        CheckBox chkSelect = (CheckBox)this.grdALLOCATE_D.Rows[i].FindControl("chkSelect");
                        if (chkSelect.Checked)
                        {
                            selectcount++;
                            string ids = this.grdALLOCATE_D.DataKeys[i].Values[0].ToString().Trim();

                            if (!SqlDBCommon_ASRS.ASRS_AlloCancel(ids, out msg))
                            {
                                Alert(msg);
                            }
                            else
                            {
                                this.Alert(Lang.FrmALLOCATEEdit_Msg24); //取消成功
                            }
                        }                      
                    }
                }
                if (selectcount == 0)
                {
                    this.Alert(Lang.FrmALLOCATEEdit_Msg25);//至少选择一行
                    return;
                }
                this.GridBind();
            }
           
        }
        catch (Exception E)
        {
            this.Alert(Lang.FrmALLOCATEEdit_Msg22 + E.Message.ToJsString());  //  调拨失败！
        }
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }


    //打印功能
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string printid = this.txtID.Text.Trim();
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmALLOCATEEdit_Print.aspx", SYSOperation.New, printid) + "','" + Lang.FrmALLOCATEEdit_Msg26+ "','BAR_REPACK',1000,900);");//打印調撥更單
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {  
        string positonCode = (sender as LinkButton).CommandArgument;
        this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("../Base/FrmBaseASRSList.aspx", SYSOperation.Modify, txtID.Text.Trim()) + "&code=" + GetErpCode(txtCTICKETCODE.Text.Trim()) + "&caseType=ALLOCATE&positonCode=" + positonCode + "','"+Lang.FrmALLOCATEEdit_Msg27+"','ASRS_LIST');");//ASRS命令列表
    }
    /// <summary>
    /// 获得当前调拨单的erpcode (针对出库调拨的调拨单)
    /// </summary>
    /// <param name="cticketcode"></param>
    /// <returns></returns>
    public string GetErpCode(string cticketcode)
    {
        string returnvalue = cticketcode;
        IGenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
        var bo = (from p in conn.Get()
                  where p.cticketcode == cticketcode && p.cdefine1 == "1" //出库调拨
                  select p).FirstOrDefault();
        if (bo != null && !string.IsNullOrEmpty(bo.id))  
        {          
            returnvalue = bo.cerpcode;
        }        
        return returnvalue;
    }
}