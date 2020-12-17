using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq;
/// <summary>
/// 描述: 操作人-->FrmBASE_OPERATOREdit 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-09-29 15:23:50
/// </summary>
public partial class BASE_FrmBASE_OPERATOREdit:WMSBasePage
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        ucBaseShowOperatorDiv.SetCompName = txtCOPERATORNAME.ClientID;
        ucBaseShowOperatorDiv.SetORGCode = txtUserNo.ClientID;
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData(this.KeyID);
            }
            else
            {
                txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtCCREATEOWNER.Text =  OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
            }
            //打开时查询CQ2013-7-8 14:09:44

            if (this.Operation() == SYSOperation.Preserved1)
            {
                hfTabIndex.Value = "2";
            }
            this.btnSearch_D_Click(this.btnSearch_D, EventArgs.Empty);
        }
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        btnSave_D.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave_D) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        //暂时截取界面
    }

    #region IPage 成员

    /// <summary>
    /// 初始化页面。主要做一下动作
    ///1、DropDownList,ListBox数据装载,
    ///2、新增按钮、删除的按钮的Java脚本注册等
    ///一般在PageLoad 事件调用，
    ///并且IsPostBack = false时
    /// </summary>
    public void InitPage()
    {
        UserId = Request.QueryString["ID"];
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_OPERATOR');return false;";
        this.btnBack_D.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_OPERATOR');return false;";
        txtUserNo.Attributes["onclick"] = "Show('" + ucBaseShowOperatorDiv.GetDivName + "');";
        this.grdBASE_DEPARTMENT.DataKeyNames = new string[] { "DEPARTMENTNO" };
        //删除确认提示
        if (this.Operation() == SYSOperation.Modify)
        {
            this.btnDelete.Attributes["onclick"] = "var userNo = '" + this.KeyID + "'; return window.confirm('" + Resources.Lang.FrmBASE_CLIENTEdit_Msg01 + "' + userNo + '?');";//要删除
        }
        else
        {
            this.btnDelete.Visible = false;
        }
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.Operation() == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.FrmALLOCATEEdit_Msg08;//审批;
        }
        Help.DropDownListDataBind(GetParametersByFlagType("BASE_OPERATOR.CSTATUS"), dplCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(GetParametersByFlagType("SelectOrNot"), ddltype, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//选择类型
        
    }
    /// 绑定列表
    ///  CQ 2013-7-5 14:50:44
    /// <summary>
    /// 绑定列表
    /// </summary>
    public void GridBind()
    {
        //BASE_FrmBASE_OPERATORListQuery listQuery = new BASE_FrmBASE_OPERATORListQuery();
        //DataTable dtSource = listQuery.GetList_Depart(txtDepartNo.Text.Trim().ToUpper(), txtDepartName.Text.Trim(), ddltype.SelectedValue.Trim(), HidFUser.Value,
        //                                              false, this.grdNavigatorBASE_DEPARTMENT.CurrentPageIndex,
        //                                              this.grdBASE_DEPARTMENT.PageSize);
        //this.grdBASE_DEPARTMENT.DataSource = dtSource;
        //this.grdBASE_DEPARTMENT.DataBind();


        IGenericRepository<BASE_DEPARTMENT> entity = new GenericRepository<BASE_DEPARTMENT>(context);
        var caseList = from p in entity.Get()
                       orderby p.createdate descending
                       where 1 == 1
                       select p;
        if (!string.IsNullOrEmpty(txtDepartNo.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.departmentno) && x.departmentno.Contains(txtDepartNo.Text.Trim().ToUpper()));
        if (txtDepartName.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.departmentname) && x.departmentname.Contains(txtDepartName.Text.Trim()));

        //if (ddltype.SelectedValue != "")
        //    caseList = caseList.Where(x => x.t.ToString().Equals(ddltype.SelectedValue));
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        grdBASE_DEPARTMENT.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdBASE_DEPARTMENT.DataBind();
    }

    #endregion
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    /// 导出Excel
    /// CQ 2013-7-5 15:22:59 
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <returns></returns>
    //protected DataTable grdNavigatorBASE_DEPARTMENT_GetExportToExcelSource()
    //{
    //    BASE_FrmBASE_OPERATORListQuery listQuery = new BASE_FrmBASE_OPERATORListQuery();
    //    DataTable dtSource = listQuery.GetList_Depart(txtDepartNo.Text.Trim().ToUpper(), txtDepartName.Text.Trim(), ddltype.SelectedValue.Trim(), HidFUser.Value, false, -1, -1);
    //    return dtSource;
    //}


    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData(string ID)
    {
        txtUserNo.Enabled = false;

        IGenericRepository<BASE_OPERATOR> con = new GenericRepository<BASE_OPERATOR>(context);
        var caseList = from p in con.Get()
                       where p.id == ID
                       select p;
        BASE_OPERATOR entity = caseList.ToList().FirstOrDefault<BASE_OPERATOR>();
        entity.id = this.KeyID;
        this.txtUserNo.Text = entity.caccountid;
        //获取操作人编码
        this.HidFUser.Value = entity.caccountid;
        this.txtCOPERATORNAME.Text = entity.coperatorname;
        this.txtCALIAS.Text = entity.calias;
        this.txtCDEPARTMENT.Text = entity.cdepartment;
        this.txtCDUTY.Text = entity.cduty;
        this.txtCPHONE.Text = entity.cphone;
        this.txtCSHIFT.Text = entity.cshift;
        this.txtCERPCODE.Text = entity.cerpcode;
        //this.txtCSTATUS.Text = entity.CSTATUS;
        try
        {
            this.dplCSTATUS.SelectedIndex = int.Parse(entity.cstatus);
        }
        catch
        { }
        this.txtCMEMO.Text = entity.cmemo;
        //entity.DCREATETIME.HasValue ? entity.DCREATETIME.ToString("yyyy-MM-dd HH:mm:ss") : "";
        this.txtDCREATETIME.Text = entity.dcreatetime.HasValue ? Convert.ToDateTime(entity.dcreatetime).ToString("yyyy-MM-dd HH:mm:ss") : "";
        this.txtCCREATEOWNER.Text = OPERATOR.GetUserNameByAccountID(entity.ccreateowner);
        txtupdatetime.Text = entity.lastupdatetime != null ? Convert.ToDateTime(entity.lastupdatetime).ToString("yyyy-MM-dd HH:mm:ss") : "";
        txtupdateuser.Text = OPERATOR.GetUserNameByAccountID(entity.lastupdateowner);
        this.txtID.Text = entity.id;

    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //
        //if (this.drCACCOUNTID.SelectedValue.Trim() == "")
        //{
        //    this.Alert("请选择操作人！");
        //    this.SetFocus(drCACCOUNTID);
        //    return false;
        //}
        ////
        //if(this.txtCACCOUNTID.Text.Trim().Length > 0)
        //{
        //    if(this.txtCACCOUNTID.Text.GetLengthByByte() > 20)
        //    {
        //        this.Alert("操作人编码项超过指定的长度20！");
        //        this.SetFocus(txtCACCOUNTID);
        //        return false;
        //    }
        //}
        //
        if (this.txtUserNo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBASE_OPERATOREdit_Msg01); //操作人编号项不允许空！
            this.SetFocus(txtUserNo);
            return false;
        }

        if (this.txtUserNo.Text.Trim() != "" && this.Operation() == SYSOperation.New)
        {
            IGenericRepository<BASE_OPERATOR> con = new GenericRepository<BASE_OPERATOR>(context);
            var exisBO = (from p in con.Get()
                          where p.caccountid == this.txtUserNo.Text.Trim()
                          select p).FirstOrDefault();
            if (exisBO != null && exisBO.id != null && exisBO.id.Length > 0)
            {
                this.Alert(Resources.Lang.FrmBASE_OPERATOREdit_Msg02);//操作人编号项已存在！
                this.SetFocus(txtUserNo);
                return false;
            }
        }


        if (this.txtCOPERATORNAME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBASE_OPERATOREdit_Msg03);//操作人姓名项不允许空！
            this.SetFocus(txtCOPERATORNAME);
            return false;
        }
        //
        if (this.txtCOPERATORNAME.Text.Trim().Length > 0)
        {
            if (this.txtCOPERATORNAME.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_OPERATOREdit_Msg04);//操作人姓名项超过指定的长度20！
                this.SetFocus(txtCOPERATORNAME);
                return false;
            }
        }
        //
        if (this.txtCALIAS.Text.Trim().Length > 0)
        {
            if (this.txtCALIAS.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg06);//助记码项超过指定的长度20！
                this.SetFocus(txtCALIAS);
                return false;
            }
        }
        //
        if (this.txtCDEPARTMENT.Text.Trim().Length > 0)
        {
            if (this.txtCDEPARTMENT.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg07);//部门项超过指定的长度20！
                this.SetFocus(txtCDEPARTMENT);
                return false;
            }
        }
        //
        if (this.txtCDUTY.Text.Trim().Length > 0)
        {
            if (this.txtCDUTY.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg08);//职务项超过指定的长度20！
                this.SetFocus(txtCDUTY);
                return false;
            }
        }
        //
        if (this.txtCPHONE.Text.Trim().Length > 0)
        {
            if (this.txtCPHONE.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg09);//电话项超过指定的长度20！
                this.SetFocus(txtCPHONE);
                return false;
            }
        }
        //
        if (this.txtCSHIFT.Text.Trim().Length > 0)
        {
            if (this.txtCSHIFT.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_OPERATOREdit_Msg05); //班别项超过指定的长度20！
                this.SetFocus(txtCSHIFT);
                return false;
            }
        }
        //
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
            if (this.txtCERPCODE.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg10);//ERP编码项超过指定的长度50！
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        //
        if (this.dplCSTATUS.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmMixed_D_Msg13);//状态项不允许空！
            this.SetFocus(this.dplCSTATUS);
            return false;
        }
        //
        if (this.dplCSTATUS.Text.Trim().Length > 0)
        {
            if (this.dplCSTATUS.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg15);//状态项超过指定的长度20！
                this.SetFocus(this.dplCSTATUS);
                return false;
            }
        }
        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_OPERATOREdit_Msg06);//备注项超过指定的长度20！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        //
        if (this.txtDCREATETIME.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate(this.txtDCREATETIME.Text)== false)
            {
                this.Alert(Resources.Lang.FrmBASE_OPERATORList_Msg01);//创建日期项不是有效的日期！
                this.SetFocus(txtDCREATETIME);
                return false;
            }
        }
        //
        if (this.txtCCREATEOWNER.Text.Trim().Length > 0)
        {
            if (this.txtCCREATEOWNER.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_OPERATOREdit_Msg07);//创建人项超过指定的长度20！
                this.SetFocus(txtCCREATEOWNER);
                return false;
            }
        }
        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_OPERATOR SendData()
    {
        BASE_OPERATOR entity = new BASE_OPERATOR();

        if (this.txtUserNo.Text.Length > 0)
        {
            entity.caccountid = txtUserNo.Text;
        }
        else
        {
            entity.caccountid = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CACCOUNTID = null;
        }
        //
        this.txtCOPERATORNAME.Text = this.txtCOPERATORNAME.Text.Trim();
        if (this.txtCOPERATORNAME.Text.Length > 0)
        {
            entity.coperatorname = txtCOPERATORNAME.Text;
        }
        else
        {
            entity.coperatorname = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.COPERATORNAME = null;
        }
        //
        this.txtCALIAS.Text = this.txtCALIAS.Text.Trim();
        if (this.txtCALIAS.Text.Length > 0)
        {
            entity.calias = txtCALIAS.Text;
        }
        else
        {
            entity.calias = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CALIAS = null;
        }
        //
        this.txtCDEPARTMENT.Text = this.txtCDEPARTMENT.Text.Trim();
        if (this.txtCDEPARTMENT.Text.Length > 0)
        {
            entity.cdepartment = txtCDEPARTMENT.Text;
        }
        else
        {
            entity.cdepartment = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CDEPARTMENT = null;
        }
        //
        this.txtCDUTY.Text = this.txtCDUTY.Text.Trim();
        if (this.txtCDUTY.Text.Length > 0)
        {
            entity.cduty = this.txtCDUTY.Text;
        }
        else
        {
            entity.cduty = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CDUTY = null;
        }
        //
        this.txtCPHONE.Text = this.txtCPHONE.Text.Trim();
        if (this.txtCPHONE.Text.Length > 0)
        {
            entity.cphone = txtCPHONE.Text;
        }
        else
        {
            entity.cphone = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CPHONE = null;
        }
        //
        this.txtCSHIFT.Text = this.txtCSHIFT.Text.Trim();
        if (this.txtCSHIFT.Text.Length > 0)
        {
            entity.cshift = txtCSHIFT.Text;
        }
        else
        {
            entity.cshift = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSHIFT = null;
        }
        //
        this.txtCERPCODE.Text = this.txtCERPCODE.Text.Trim();
        if (this.txtCERPCODE.Text.Length > 0)
        {
            entity.cerpcode = txtCERPCODE.Text;
        }
        else
        {
            entity.cerpcode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CERPCODE = null;
        }
        //
        this.dplCSTATUS.Text = this.dplCSTATUS.Text.Trim();
        if (this.dplCSTATUS.Text.Length > 0)
        {
            entity.cstatus = this.dplCSTATUS.SelectedValue;
        }
        else
        {
            entity.cstatus=string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSTATUS = null;
        }
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        else
        {
            entity.cmemo = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CMEMO = null;
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
            //entity.DCREATETIME = null;
        }
        //

        this.txtCCREATEOWNER.Text = this.txtCCREATEOWNER.Text.Trim();
        if (this.txtCCREATEOWNER.Text.Length > 0)
        {
            entity.ccreateowner = OPERATOR.GetUserIDByUserName(txtCCREATEOWNER.Text);
        }
        else
        {
            entity.ccreateowner = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCREATEOWNER = null;
        }
        #region 界面上不可见的字段项
        /*
            entity.CDEFINE1 = ;
            entity.CDEFINE2 = ;
            entity.DDEFINE3 = ;
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
        IGenericRepository<BASE_OPERATOR> con = new GenericRepository<BASE_OPERATOR>(context);
        if (this.CheckData())
        {
            var entity = this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
                entity.id = this.KeyIDS[0];
            }
            string strKeyID = "";
            try
            {
                if (this.Operation() == SYSOperation.Modify)
                {
                    strKeyID = txtID.Text.Trim();
                    entity.id = strKeyID;
                    entity.lastupdatetime = DateTime.Now;
                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Update(entity);
                    con.Save();
                    this.AlertAndBack("FrmBASE_OPERATOREdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
                }
                /*
                else if(this.Operation == SYSOperation.Apply)
                {
                    BASE_OPERATORRule.Apply(entity);
                    this.AlertAndBack("FrmBASE_OPERATOREdit.aspx?" + BuildQueryString(SYSOperation.View, strKeyID),"申报成功"); 
                }
                else if(this.Operation == SYSOperation.Audit)
                {
                    BASE_OPERATORRule.Audit(entity);
                    this.AlertAndBack("FrmBASE_OPERATOREdit.aspx?" + BuildQueryString(SYSOperation.View, strKeyID),"审批成功"); 
                }
                */
                else if (this.Operation() == SYSOperation.New)
                {
                    strKeyID = Guid.NewGuid().ToString();
                    entity.id = strKeyID;
                    entity.lastupdatetime = DateTime.Now;
                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Insert(entity);
                    con.Save();
                    this.AlertAndBack("FrmBASE_OPERATOREdit.aspx?" + BuildQueryString(SYSOperation.New, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                }
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATEEdit_Msg17 + E.Message); //"失败！" 
#if Debug 
                    this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_OPERATOR> con = new GenericRepository<BASE_OPERATOR>(context);
        try
        {
           con.Delete(this.KeyID);
           con.Save();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_DelFail + E.Message); //删除失败！this.Alert("删除失败！" + E.Message);
#if Debug 
                this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
        }

    }

    /// <summary>
    /// 用户编码
    /// </summary>
    public string UserId
    {
        get { return ViewState["UserId"].ToString(); }
        set { ViewState["UserId"] = value; }
    }

    /// <summary>
    /// 加载已设置的责任部门
    /// </summary>
    private void LoadBASE_DEPARTMENT()
    {

        SelectIds =OPERATOR.GetOperatorDepartment(HidFUser.Value.Trim());

        SelectedData = OPERATOR.GetAllDepartment(HidFUser.Value.Trim());

    }
   
    /// <summary>
    /// 已选择的部门信息
    /// </summary>
    public Dictionary<string, string> SelectedData
    {
        get { return ViewState["SelectedData"] as Dictionary<string, string>; }
        set { ViewState["SelectedData"] = value; }
    }

    /// <summary>
    /// 选择信息
    /// </summary>
    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
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

        foreach (GridViewRow item in this.grdBASE_DEPARTMENT.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //获取ID
                string code = this.grdBASE_DEPARTMENT.DataKeys[item.RowIndex][0].ToString();

                //控件选中且集合中不存在添加
                if (cbo.Checked && !SelectIds.ContainsKey(code))
                {
                    SelectIds.Add(code, code);
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(code))
                {
                    SelectIds.Remove(code);
                }
            }
        }
    }

    /// <summary>
    /// 数据邦定前 获取上一页已选中的行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdBASE_DEPARTMENT_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }

    /// 数据行绑定
    /// <summary>
    /// 数据行绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdBASE_DEPARTMENT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //获取ID
            string id = this.grdBASE_DEPARTMENT.DataKeys[e.Row.RowIndex][0].ToString();

            CheckBox cbo = e.Row.FindControl("chkSelect") as CheckBox;
            cbo.Attributes.Add("onclick", "SelIDCancelAll()");

            if (SelectedData.Values.Count > 0)
            {
                //SelectedCARGOSPACE_Dt = BASE_PARTRule.GetPlaceWLRelPlace(PartId, id);
                if (SelectedData.ContainsKey(id))
                {
                    cbo.Checked = true;
                }
                if (SelectIds.ContainsKey(id))
                {
                    //如果是控件处于选中状态
                    cbo.Checked = true;
                }
            }
        }

    }

  
    /// <summary>
    /// 分页数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdBASE_DEPARTMENT_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    /// 交付部门保存按钮
    /// <summary>
    /// 交付部门保存按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_D_Click(object sender, EventArgs e)
    {
        GetSelectedIds();
        string msg = "";
        if (OPERATOR.SetBaseOperatorDepart(SelectIds, HidFUser.Value, WmsWebUserInfo.GetCurrentUser().UserNo))
        {
            msg = Resources.Lang.FrmBASE_WL_AREA_Msg03; //设置成功!
            this.AlertAndBack("FrmBASE_OPERATOREdit.aspx?" + BuildQueryString(SYSOperation.Preserved1, UserId), msg);
        }
        else
        {
            msg = Resources.Lang.FrmBASE_WL_AREA_Msg04;//设置失败
            Alert(msg);
        }


    }

    /// 部门查询按钮
    /// CQ 2013-7-5 15:26:28
    /// <summary>
    /// 部门查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_D_Click(object sender, EventArgs e)
    {
        LoadBASE_DEPARTMENT();
        CurrendIndex = 1;
        this.GridBind();
    }
    #endregion   
}

