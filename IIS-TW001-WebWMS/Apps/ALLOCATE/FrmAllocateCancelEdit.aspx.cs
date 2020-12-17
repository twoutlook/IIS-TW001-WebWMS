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


/// <summary>
/// 描述: 1111-->FrmALLOCATEEdit 页面后台类文件
/// 作者: --chenlei
/// 创建于: 2012-11-01 09:29:23
/// </summary>
public partial class ALLOCATE_FrmAllocateCancelEdit : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() == SYSOperation.Modify)
            {
                ShowData();
            }
            else if (this.Operation() == SYSOperation.Preserved1)
            {
                ShowData();
                //this.OpenFloatWin(BuildRequestPageURL("FrmINASN_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID, "", "INASN_D", 600, 350);
                this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmALLOCATE_DEdit.aspx", SYSOperation.New, "") + "&parentId=" + this.KeyID + "','','ALLOCATE_D',600,350);");
            }
            else
            {
                txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// HH:mm:ss
                txtDINDATE.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// HH:mm:ss
                txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
                //this.txtCTICKETCODE.Text = new Fun_CreateNo().CreateNo("ALLOCATE");
            }
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
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
        btnCreateInOutBill.Enabled = false;     
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('ALLOCATE');return false;";
        ////设置保存按钮的文字及其状态
        //if(this.Operation() == SYSOperation.View)
        //{
        //    this.btnSave.Visible = false;
        //}
        //else if(this.Operation() == SYSOperation.Approve)
        //{
        //     this.btnSave.Text = "审批";+ WebUserInfo.GetCurrentUser().CSS_Name 
        //}"
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";      
        this.grdALLOCATE_D.DataKeyNames = new string[] { "IDS" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        //this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + PageBase.BuildRequestPageURL("FrmALLOCATE_DList.aspx", SYSOperation.New, "") + "','新建调拨单','ALLOCATE_D');return false;";
        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmALLOCATE_DEdit.aspx", SYSOperation.New,""),800,600);

        //调拨单状态
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "AllocateCancelStatus", false, -1, -1), this.ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
        if (ddlCSTATUS.Items.Count == 3)
            ddlCSTATUS.Items.RemoveAt(1);
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly

    }

    #endregion

    public string Status
    {
        get {
            if (ViewState["Status"] != null)
            {
                return ViewState["Status"].ToString();
            }
            return "";
        }
        set { ViewState["Status"] = value; }
    }
   
    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
	{
        //ALLOCATEEntity entity = new ALLOCATEEntity();
        //entity.ID = this.KeyID.Trim();
        //entity.SelectByPKeys()
        //entity.SelectByPKeys();
        //this.txtID.Text = entity.ID.ToString();
        //this.txtCCREATEOWNERCODE.Text = entity.CCREATEOWNERCODE;
        //this.txtDCREATETIME.Text = entity.DCREATETIME.ToString("yyyy-MM-dd HH:mm:ss");
        //this.txtCAUDITPERSON.Text = entity.CAUDITPERSON;
        //this.txtDAUDITTIME.Text = entity.DAUDITTIME.HasValue ? entity.DAUDITTIME.ToString("yyyy-MM-dd HH:mm:ss") : "";
        //this.txtCTICKETCODE.Text = entity.CTICKETCODE;
        //this.ddlCSTATUS.SelectedValue = entity.CSTATUS;
        //this.txtCERPCODE.Text = entity.CERPCODE;
        //this.txtCMEMO.Text = entity.CMEMO;
        //this.txtDINDATE.Text = entity.DINDATE.ToString("yyyy-MM-dd HH:mm:ss");
        ////this.txtCDEFINE1.Text = entity.CDEFINE1;
        ////this.txtCDEFINE2.Text = entity.CDEFINE2;
        ////this.txtDDEFINE3.Text = entity.DDEFINE3.ToString("yyyy-MM-dd HH:mm:ss");
        ////this.txtDDEFINE4.Text = entity.DDEFINE4.ToString("yyyy-MM-dd HH:mm:ss");
        ////this.txtIDEFINE5.Text = entity.IDEFINE5.ToString();
        //Status = entity.CSTATUS;
        //if (entity.CSTATUS != "0" && entity.CSTATUS != "1" && entity.CSTATUS != "4")
        //{            
        //    this.btnDelete.Enabled= false;
        //    this.btnNew.Enabled = false;
        //    this.btnSave.Enabled = false;

        //    imgDINDATE.Visible = false;
        //    this.txtID.Enabled = false;
        //    this.txtCCREATEOWNERCODE.Enabled = false;
        //    this.txtDCREATETIME.Enabled = false;
        //    this.txtCAUDITPERSON.Enabled = false;
        //    this.txtDAUDITTIME.Enabled = false;
        //    this.txtCTICKETCODE.Enabled = false;
        //    this.ddlCSTATUS.Enabled = false;
        //    this.txtCERPCODE.Enabled = false;
        //    this.txtCMEMO.Enabled = false;
        //    this.txtDINDATE.Enabled = false;
        //    this.btnImportExcel.Enabled = false;
        //}
        //if (entity.CSTATUS == "0" || entity.CSTATUS == "1" || entity.CSTATUS == "4")
        //{
        //    this.btnImportExcel.Enabled = true;
        //    this.btnImportExcel.Attributes["onclick"] = "PopupFloatWin('" + BuildRequestPageURL("/Apps/Import/FrmImportAllocateDetail.aspx", SYSOperation.New, "") + "&AllocateId=" + this.KeyID + "&CTICKETCODE=" + txtCTICKETCODE.Text.Trim() + "&ImportType=allocate','上传調撥單明细','Allocate_D',600,320); return false;";
        //}
        IGenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
        var caseList = from p in conn.Get()
                       where p.id == this.KeyID
                       select p;
        ALLOCATE entity = caseList.ToList().FirstOrDefault();
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

        Status = entity.cstatus;
        if (entity.cstatus != "0" && entity.cstatus != "1" && entity.cstatus != "4")
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
            this.btnImportExcel.Attributes["onclick"] = "PopupFloatWin('" + BuildRequestPageURL("/Apps/Import/FrmImportAllocateDetail.aspx", SYSOperation.New, "") + "&AllocateId=" + this.KeyID + "&CTICKETCODE=" + txtCTICKETCODE.Text.Trim() + "&ImportType=allocate','" + Resources.Lang.FrmALLOCATEEdit_Msg09 + "','Allocate_D',600,320); return false;";//上传調撥單明细
        }     
        AllocateQuery allQry = new AllocateQuery();
        if (allQry.AllocateFromOutBill(txtID.Text))
        {
            btnNew.Enabled = false;
            btnDelete.Enabled = false;
           
        }

        GridBind();
      
	}




    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //if (Operation() == SYSOperation.Modify)
        //{
        //    IGenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
        //    ALLOCATE bo = (from p in conn.Get()
        //                   where p.id == this.KeyID
        //                   select p
        //                  ).FirstOrDefault();
        //    if (bo != null && !string.IsNullOrEmpty(bo.cstatus) && !bo.cstatus.Equals("0"))
        //    {
        //        this.Alert("此調撥單不是未處理狀態，不能添加明細！");
        //        return false;
        //    }
        //}
        //
        if (this.txtCCREATEOWNERCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg11);//制单人项不允许空！
            this.SetFocus(txtCCREATEOWNERCODE);
            return false;
        }
        ////
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (OPERATOR.GetUserIDByUserName(this.txtCCREATEOWNERCODE.Text).GetLengthByByte() > 100)
            {
               this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg12);//制单人项超过指定的长度100！
                this.SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }
        ////
        if (this.txtCAUDITPERSON.Text.Trim().Length > 0)
        {
            if (OPERATOR.GetUserIDByUserName(this.txtCAUDITPERSON.Text).GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg13); //审核人项超过指定的长度100！
                this.SetFocus(txtCAUDITPERSON);
                return false;
            }
        }
        ////
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
            if (this.txtCERPCODE.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg14); //ERP单号项超过指定的长度30！
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        ////
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 200)
            {
                this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg15); //备注项超过指定的长度200！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        //
        if (this.txtDINDATE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg16); //调拨日期项不允许空！
            this.SetFocus(txtDINDATE);
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
           // ALLOCATE entity = (ALLOCATE)this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";
            try
            {
               // GenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
                if (this.Operation() == SYSOperation.Modify || SYSOperation.Preserved1 == this.Operation())
                {
                    strKeyID = txtID.Text.Trim();
                   // entity.id = txtID.Text.Trim();
                    //BUCKINGHA-775 调拨单调账将已审核状态变成未处理后，相应的占用量需释放掉    状态改变直接放在sp里执行
                       
                    Proc_ReleaseStockIoccupyqty proc = new Proc_ReleaseStockIoccupyqty();
                    proc.P_Allocate_id = strKeyID;
                    proc.P_Cstatus =ddlCSTATUS.SelectedValue.Trim();                          
                    proc.Execute();
                    if (proc.P_return_Value != "0" && proc.P_ReturnMsg.Length > 0)
                    {
                        Alert(proc.P_ReturnMsg);
                    }
                    else
                    {
                        this.AlertAndBack("FrmAllocateCancelEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                    }                           
                                        
                    //BUCKINGHA-775 调拨单调账将已审核状态变成未处理后，相应的占用量需释放掉
                    //conn.Update(entity);
                    //conn.Save();
                }
               
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATEEdit_Msg17 + E.Message); //"失败！" 
            }
        }
    }

    public void GridBind()
    {
        Bind("");
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
            List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
            flagList.Add(new Tuple<string, string>("cstatus", "AllocateCancelStatus"));

            var srcdata = GetGridSourceDataByList(data, flagList);

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
            grdALLOCATE_D.DataSource = srcdata;//GetPageSize(caseList, PageSize, CurrendIndex).ToList();
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
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
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
            if (txtCinvcode.Text.Trim() != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));
            }
            return caseList;
        }
        else
        {
            return null;
        }

    } 

    
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {       
        this.GridBind();
    } 
   
    /// <summary>
    /// 删除按钮
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
                            msg = Resources.Lang.FrmALLOCATEEdit_Msg18; //此調撥單不是未處理狀態，不能删除明細！
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
                                        msg += Resources.Lang.FrmALLOCATEList_ExCommondNoDel; //已经产生AS/RS命令，不能删除
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
                msg = Resources.Lang.FrmALLOCATEList_DelSuccess; //删除成功！
            }
            this.Alert(msg);
            //DBUtil.Commit();
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.Common_FailDel + E.Message.ToJsString()); //删除失败！
            //DBUtil.Rollback();
        }
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
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
    //private string GetKeyIDS(int rowIndex)
    //{
    //    string strKeyId = "";
    //    for (int i = 0; i < this.grdALLOCATE_D.DataKeyNames.Length; i++)
    //    {
    //        strKeyId += this.grdALLOCATE_D.DataKeys[rowIndex].Values[i].ToString() + ",";
    //    }
    //    strKeyId = strKeyId.TrimEnd(',');
    //    return strKeyId;
    //}

    protected void grdALLOCATE_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            //HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            //if (Status == "0" || Status == "1" || Status == "4")
            //{
            //    linkModify.Enabled = false;
            //   // linkModify.NavigateUrl = "#";
            //   // this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmALLOCATE_DEdit.aspx", SYSOperation.Modify, strKeyID), "调拨单", "ALLOCATE_D", 600, 350);
            //}
            //else
            //{
            //    linkModify.Enabled = false;
            //}           

            //((LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0]).Enabled = e.Row.Cells[e.Row.Cells.Count - 3].Text == "0" ? false : true;

            e.Row.Cells[e.Row.Cells.Count - 1].Text = e.Row.Cells[e.Row.Cells.Count - 1].Text == "0" ? Resources.Lang.Common_Status01 : Resources.Lang.Common_Status02; //"未处理" : "已完成";
            //调拨人
            e.Row.Cells[12].Text = OPERATOR.GetUserNameByAccountID(e.Row.Cells[12].Text);
        }
    }
  

    protected void btnNew_Click(object sender, EventArgs e)
    {
        //SaveDataToDB(sender);
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

}

