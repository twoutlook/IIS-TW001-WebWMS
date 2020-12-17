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
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Business.SP.ProcedureModel;

/// <summary>
/// 描述: 入库管理-->FrmINASSITEdit 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:49:46
/// </summary>
/*
Roger
2013/7/2 8:44:29
20130702084429
解决狂点按钮重复提交问题
*/
public partial class RD_FrmINASSITEdit : WMSBasePage// PageBase, IPageEdit
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucINASN_Div.SetCompName = txtCASNID.ClientID;
       // ucINASN_Div.SetORGCode = hfINASN_id.ClientID;
        ucINASN_Div.SetORGCode = txtINASN_id.ClientID;
        ucINASN_Div.workType = "0";
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

               // this.Operation() = SysOperation.Modify;

                this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASSIT_DEdit.aspx", SYSOperation.New, "") + "&parentId=" + this.KeyID + "&cASNID=" + txtINASN_id.Text.Trim() + "&cTicketCode=" + txtCTICKETCODE.Text.Trim() + "','上架指引单','INASSIT_D',600,350);");
                //this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASSIT_DEdit.aspx", SysOperation.New, "") + "&parentId=" + this.KeyID + "&cASNID=" + txtINASN_id.Text.Trim() + "&cTicketCode=" + txtCTICKETCODE.Text.Trim() + "','','INASSIT_D',600,350);");
            }
            else
            {
                txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
                txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// HH:mm:ss
               // txtCTICKETCODE.Text = new Fun_CreateNo().CreateNo("INASSIT");
                //btnDeleteItem.Enabled = false;
                //btnAutoCreate.Enabled = false;
            }
           // SearchItems();
            this.GridBind();
        }
        this.btnAutoCreate.Attributes["onclick"] = this.GetPostBackEventReference(this.btnAutoCreate) + ";this.disabled=true;";
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
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INASSIT');return false;";
        txtCASNID.Attributes["onclick"] = "Show('" + ucINASN_Div.GetDivName + "');";
        //删除确认提示
        if (this.Operation() == SYSOperation.Modify)
        {
            //要删除
            this.btnDelete.Attributes["onclick"] = "var userNo = '" + this.KeyID + "'; return window.confirm('<%$ Resources:Lang, FrmINASSIT_DEdit_MSG14 %>' + userNo + '?');";
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
            this.btnSave.Text = Resources.Lang.FrmINBILLEdit_MSG42;//"审批";
        }
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //入库类型
        Help.DropDownListDataBind(this.GetInType(true), this.ddlInType, Resources.Lang.FrmINASSITEdit_MSG10, "FUNCNAME", "EXTEND1", "");
        //Help.DropDownListDataBind(new SYS_PARAMETERQuery().GetSys_ParameterByFLAG_TYPE("ASSIT"), this.ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
        //全部
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "ASSIT", false, -1, -1), this.ddlCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        hfId.Value = Request.QueryString["ID"];
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        //this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdINASSIT_D.DataKeyNames = new string[] { "IDS" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        //this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + PageBase.BuildRequestPageURL("FrmINASSIT_DEdit.aspx", SysOperation.New, "&parentId=" + hfId.Value) + "','新建上架指引','INASSIT_D');return false;";
        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmINASSIT_DEdit.aspx", SysOperation.New,""),800,600); 
    }

    #endregion



    /// <summary>
    /// 获取系统类型
    /// </summary>
    /// <param name="FLAG_TYPE"></param>
    /// <returns></returns>
    public DataTable GetSys_ParameterByFLAG_TYPE(string FLAG_TYPE)
    {
        string strSQL = "select FLAG_ID,FLAG_NAME,FLAG_TYPE,SORTID from sys_parameter where flag_type='" + FLAG_TYPE + "' order by sortid";

        return DBHelp.ExecuteToDataTable(strSQL);

        //DataTable dtBASE_CARGOSPACE = DBUtil.Fill(strSQL);
        //return dtBASE_CARGOSPACE;
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
	{
        btnDeleteItem.Enabled = true;
        //INASSITEntity entity = new INASSITEntity();
        //entity.ID = this.KeyID;
        //entity.SelectByPKeys();
        //RD_FrmINASSITListQuery query = new RD_FrmINASSITListQuery();
        //INASSITEntity entity = query.GetINASSITEntityById(this.KeyID);

        IGenericRepository<INASSIT> con = new GenericRepository<INASSIT>(context);
        var caseList = from p in con.Get()   
                       where p.id == this.KeyID
                       select p;
        INASSIT entity = caseList.ToList().FirstOrDefault<INASSIT>();


        hfId.Value = entity.id.ToString();
        this.ddlCSTATUS.Text = entity.cstatus;
       
        this.txtCTICKETCODE.Text = entity.cticketcode;
        this.txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(entity.ccreateownercode);
       // this.txtDCREATETIME.Text = entity.dcreatetime != null ? entity.dcreatetime.ToString("yyyy-MM-dd HH:mm:ss") : "";
        this.txtDCREATETIME.Text = entity.dcreatetime != null ?entity.dcreatetime.ToString(): "";
 
        this.txtINASN_id.Text = entity.casnid;



        IGenericRepository<INASN> coniasn = new GenericRepository<INASN>(context);
        var caseListIasn = from p in coniasn.Get()
                       where p.id == entity.casnid
                       select p;
        INASN entityiasn = caseListIasn.ToList().FirstOrDefault<INASN>();

        this.ddlInType.SelectedValue = entityiasn.itype;
        this.txtCASNID.Text = entityiasn.cticketcode;
        Status = entity.cstatus;

        if (entity.cstatus != "0")
        {
            SetTblFormControlEnabled(false);
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

    private void SetTblFormControlEnabled(bool value)
    {
        txtCASNID.Enabled = value;
        txtCCREATEOWNERCODE.Enabled = value;
        txtCTICKETCODE.Enabled = value;
        txtDCREATETIME.Enabled = value;
        txtINASN_id.Enabled = value;
        ddlCSTATUS.Enabled = value;

        txtCASNID.Attributes.Remove("onclick");

        btnDelete.Enabled = value;
        btnDeleteItem.Enabled = value;
        btnAutoCreate.Enabled = value;
        btnNew.Enabled = value;
        btnSave.Enabled = value;
        //btnSearch.Enabled = value;

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
        #region 为空检查
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (this.txtCCREATEOWNERCODE.Text.GetLengthByByte() > 100)
            {
                //制单人项超过指定的长度100
                this.Alert(Resources.Lang.FrmINASSITEdit_MSG1+ "！");
                this.SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }

        if (this.txtCASNID.Text.Trim().Length == 0)
        {
            //请选择入库通知单号！(一定要在选择框点击选择按钮)
            this.Alert(Resources.Lang.FrmINASSITEdit_MSG2 );
            this.SetFocus(txtCASNID);
            return false;
        }

        if (this.txtCASNID.Text.Trim().Length > 0)
        {
            if (this.txtCASNID.Text.GetLengthByByte() > 30)
            {
                //入库通知单号项超过指定的长度30
                this.Alert(Resources.Lang.FrmINASSITEdit_MSG3+"！");
                this.SetFocus(txtCASNID);
                return false;
            }
        }
        //取消检查CQ 2014-10-15 16:53:23
        //if (INASNRule.ValidateIsCreateInBill(txtINASN_id.Text.Trim()))
        //{
        //    this.Alert("该入库通知单号已生成过入库单,所以不能生成指引！");
        //    this.SetFocus(txtCASNID);
        //    return false;
        //}

        var asnID=!string.IsNullOrEmpty(this.txtINASN_id.Text)?this.txtINASN_id.Text.Trim():"";
        INTYPE INASN_type = GetINTYPEByID(asnID, "INASN");
        if (INASN_type.Is_Query == 1)
        {
            //此入库通知单的类型为仅查询，不能生成指引单
            this.Alert(Resources.Lang.FrmINASSITEdit_MSG4+"！");
            this.SetFocus(txtCASNID);
            return false;
        }

        #endregion

        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public INASSIT SendData()
    {
        //INASSITEntity entity = new INASSITEntity();

        INASSIT entity = new INASSIT();
        //

        //this.txtCTICKETCODE.Text = this.txtCASNID.Text.Trim();
        if(this.txtCTICKETCODE.Text.Length > 0)
        {
        	entity.cticketcode = txtCTICKETCODE.Text;
        }
        else
        {
            //entity.cticketcode = new Fun_CreateNo().CreateNo("INASSIT");
            entity.cticketcode = INASSIT_DComRule.CreateNo("INASSIT");
        	//entity.SetDBNull("CTICKETCODE",true);
        	//对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        	//entity.CTICKETCODE = null;
        }
        //
        this.txtCCREATEOWNERCODE.Text = this.txtCCREATEOWNERCODE.Text.Trim();
        if(this.txtCCREATEOWNERCODE.Text.Length > 0)
        {
            entity.ccreateownercode = OPERATOR.GetUserIDByUserName(txtCCREATEOWNERCODE.Text);
        }
        else
        {
            entity.ccreateownercode = null;
        	//对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        	//entity.CCREATEOWNERCODE = null;
        }
        //
        this.txtDCREATETIME.Text = this.txtDCREATETIME.Text.Trim();
        if(this.txtDCREATETIME.Text.Length > 0)
        {
            entity.dcreatetime = txtDCREATETIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");// HH:mm:ss
        }
        else
        {
            entity.dcreatetime = null;
        	//对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        	//entity.DCREATETIME = null;
        }
        //
        this.txtINASN_id.Text = txtINASN_id.Text.Trim();
        if (this.txtINASN_id.Text.Length > 0)
        {
            entity.casnid = txtINASN_id.Text;//txtCASNID.Text;
        }
        else
        {
            entity.casnid = null;
        	//对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        	//entity.CASNID = null;
        }
        entity.cmemo = txtCMEMO.Text;
        entity.cstatus = "0";
        #region 界面上不可见的字段项
        /*
        entity.CMEMO = ;
        entity.CDEFINE1 = ;
        entity.CDEFINE2 = ;
        entity.CDEFINE3 = ;
        entity.CDEFINE4 = ;
        entity.CDEFINE5 = ;
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
        SaveDate(sender);
        this.btnSave.Enabled = true;
    }

    private void SaveDate(object sender)
    {
        IGenericRepository<INASSIT> con = new GenericRepository<INASSIT>(context);
        this.btnSave.Enabled = false; //CQ 2013-5-13 13:47:51
        if (this.CheckData())
        {
            INASSIT entity = (INASSIT)this.SendData();
            if (this.Operation() != SYSOperation.New)
            {

            }
            string strKeyID = "";
            
            try
            {
                if (this.Operation() == SYSOperation.Modify)
                {
                    strKeyID = hfId.Value;
                    entity.id = hfId.Value;
                    con.Update(entity);
                    con.Save();
                    
                    //this.AlertAndBack("FrmINASSITEdit.aspx?" + BuildQueryString(SysOperation.Modify, strKeyID),"保存成功"); 
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    //判断是否存在指引，如果存在不重复创建
                    //var entity2 = (from p in con.Get()
                    //              where p.casnid == entity.casnid && p.cstatus == "0"
                    //              select p).FirstOrDefault<INASSIT>();
                    //if (entity2 == null)
                    //{
                        strKeyID = Guid.NewGuid().ToString();
                        entity.id = strKeyID;
                        hfId.Value = strKeyID;
                        con.Insert(entity);
                        con.Save();                          
                    //}
                    this.btnAutoCreate.Enabled = true;
                    //this.AlertAndBack("FrmINASSITEdit.aspx?" + BuildQueryString(SysOperation.Modify, strKeyID),"保存成功");                    
                }
                if ((sender as Button).ID == "btnNew")
                {
                    //strKeyID = hfId.Value;
                    //Response.Redirect(BuildRequestPageURL("FrmINASSITEdit.aspx", SysOperation.Preserved1, strKeyID));
                    this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASSITEdit.aspx", SYSOperation.New, this.KeyID) + "&parentId=" + this.KeyID + "&cASNID=" + txtINASN_id.Text.Trim() + "&cTicketCode=" + txtCTICKETCODE.Text.Trim() + "','上架指引单','INASSIT_D',840,600);");

                }
                else if ((sender as Button).ID != "btnAutoCreate" && (sender as Button).ID != "btnAutoCreateTest")
                {
                    //Response.Redirect(BuildRequestPageURL("FrmINASSITEdit.aspx", SysOperation.Modify, strKeyID));
                    this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASSITEdit.aspx", SYSOperation.New, this.KeyID) + "&parentId=" + this.KeyID + "&cASNID=" + txtINASN_id.Text.Trim() + "&cTicketCode=" + txtCTICKETCODE.Text.Trim() + "','上架指引单','INASSIT_D',840,600);");

                }
            }
            catch (Exception E)
            {
                //失败
                this.Alert(this.GetOperationName() + Resources.Lang.FrmINBILLEdit_MSG54+ "！" + E.Message);
#if Debug 
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        IGenericRepository<INASSIT> con = new GenericRepository<INASSIT>(context);
        try
        {
          con.Delete(this.KeyID.ToString());
          con.Save();
        }
        //INASSITEntity entity = new INASSITEntity(); 
        //try 
        //{ 
        //    entity.ID = this.KeyID;
        //    INASSITRule.Delete(entity);
        //    //
        //    this.ClientScript.RegisterStartupScript(GetType(), "deleteMsg", "fun_AlertAndRedirectNewPage('删除成功!','FrmINASSITList.aspx');", true);
        //} 
        catch (Exception E) 
        {
            //删除失败
            this.Alert(Resources.Lang.FrmINBILLEdit_MSG56+ "！" + E.Message); 
            #if Debug 
            this.Response.Write(entity.DBAccess().GetLastSQL());                
            #endif 
        }
               
    }

    #region IPageGrid 成员

    public void GridBind()
    {        
        //RD_FrmINASSIT_DListQuery listQuery = new RD_FrmINASSIT_DListQuery();
        int count = 0;
        this.CurrendIndex = 1;
        DataTable dtSource = INASSIT_DComRule.GetList(hfId.Value, txtCinvCode.Text.Trim(), true, this.CurrendIndex, this.PageSize, out count);
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
        AspNetPager1.RecordCount = count;
        AspNetPager1.PageSize = this.PageSize;
        this.grdINASSIT_D.DataSource = dtSource;
        this.grdINASSIT_D.DataBind();
        
    }

    //public bool CheckData()
    //{
    //    if (this.txtID.Text.Trim().Length > 0)
    //    {
    //        if (this.txtID.Text.IsDecimal() == false)
    //        {
    //            this.Alert("主表编号项不是有效的十进制数字！");
    //            this.SetFocus(txtID);
    //            return false;
    //        }
    //    }
    //    return true;

    //}

    #endregion

    #region IPage 成员

    public void InitPage_DList()
    {
                       
    }

    #endregion

    protected DataTable grdNavigatorINASSIT_D_GetExportToExcelSource()
    {
       // RD_FrmINASSIT_DListQuery listQuery = new RD_FrmINASSIT_DListQuery();
        int count=0;
        DataTable dtSource = INASSIT_DComRule.GetList(hfId.Value, txtCinvCode.Text.Trim(), false, -1, -1, out count);
        return dtSource;
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
   
    protected void grdINASSIT_D_PageIndexChanged(object sender, EventArgs e)
    {
        //if(grdNavigatorINASSIT_D.IsDbPager)
        //{
            this.GridBind();
        //}
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //重新设置GridNavigator的RowCount
        //SearchItems();  
        this.CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;  
        this.GridBind();
    }

    

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

        foreach (GridViewRow item in this.grdINASSIT_D.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string id = this.grdINASSIT_D.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                //控件选中且集合中不存在添加
                if (cbo.Checked && !SelectIds.ContainsKey(id))
                {
                    SelectIds.Add(id, id);
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(id))
                {
                    SelectIds.Remove(id);
                }
            }
        }
    }

    protected void btnDeleteItem_Click(object sender, EventArgs e)
    {
       
        GetSelectedIds();
        string msg = string.Empty;
        var returnValue=0;
        var errorMSG=string.Empty;
        if (SelectIds.Count > 0)
        {
            if (INASSIT_DComRule.Delete(SelectIds,ref returnValue,ref errorMSG))
            {
                msg = Resources.Lang.FrmINBILLEdit_MSG55;// "删除成功!";
            }
            else
            {
                //删除失败
                msg = Resources.Lang.FrmINBILLEdit_MSG56+"!" + errorMSG;
            }
        }
        else
        {
            msg = Resources.Lang.FrmINASSITEdit_MSG5 + "!";//"请选择要删除的数据！";
        }
        //SearchItems();
        GridBind();
        Alert(msg);
    }



    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdINASSIT_D.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdINASSIT_D.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdINASSIT_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            //HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            //linkModify.NavigateUrl = "#";
            //this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmINASSIT_DEdit.aspx", SysOperation.Modify, strKeyID), "", "INASSIT_D", 600, 350);
            //this.OpenFloatWinMax(linkDelete, BuildRequestPageURL("FrmINASSIT_DEdit.aspx", SysOperation.Modify, strKeyID), "上架指引", "INASSIT_D");

            //e.Row.Cells[e.Row.Cells.Count - 2].Text = e.Row.Cells[e.Row.Cells.Count - 2].Text == "0" ? "未处理" : "已完成";
        }
    }

    protected void dsGrdINASSIT_D_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }


    protected void btnNew_Click(object sender, EventArgs e)
    {
        SaveDate(sender);
        this.btnAutoCreate.Enabled = true;
    }

    /// <summary>
    /// 生成指引
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAutoCreate_Click(object sender, EventArgs e)
    {
        this.btnAutoCreate.Enabled = false;
        try
        {
            #region 生成指引
            string msg = string.Empty;
            if (this.CheckData())
            {
                if (txtINASN_id.Text.Length > 0)
                {
                    //保存单头
                    SaveDate(sender);
                    string guid = Guid.NewGuid().ToString();


                    #region sql server 调用存储过程
                    List<string> SparaList = new List<string>();
                    SparaList.Add("@P_InAssit_id:" + hfId.Value.Trim());
                    SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                    SparaList.Add("@P_Guid:" + guid);
                    SparaList.Add("@P_ReturnValue:" + 0);
                    SparaList.Add("@INFOTEXT:" + "");
                    string[] Result = DBHelp.ExecuteProc("Proc_AutoCreateInAssit_D", SparaList);
                    if (Result.Length==2 && Result[0] == "1" && Result[1].Length>0)//调用存储过程有错误
                    {
                        msg += Result[1];

                    }
                    
                    if (msg.Length > 0)
                    {
                        //生成失败
                        msg = Resources.Lang.FrmINASSITEdit_MSG7+ "!" + msg;
                        this.btnAutoCreate.Enabled = true;
                        //20130702084429
                        btnAutoCreate.Style.Remove("disabled");
                        msg = new ErrorMsg_Query().GetErrorMsg(guid);//+ msg;
                    }
                    else
                    {
                           //获取已生成的上架指引
                            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
                            msg = Resources.Lang.FrmINASSITEdit_MSG6 + "!"; //"生成成功!";
                            this.btnAutoCreate.Enabled = false;
                    }

                    #endregion
                   
                }
                else
                {
                    msg = Resources.Lang.FrmINASSITEdit_MSG8 + "!"; //"请选择入库通知单!";
                    this.btnAutoCreate.Enabled = true;
                    //20130702084429
                    btnAutoCreate.Style.Remove("disabled");
                }
                Alert(msg); 
            
            }
            else
            {
                //20130702084429
                btnAutoCreate.Style.Remove("disabled");
                this.btnAutoCreate.Enabled = true;
            }
            #endregion
        }
        catch (Exception err)
        {
            Alert(err.Message);
            //20130702084429
            btnAutoCreate.Style.Remove("disabled");
            this.btnAutoCreate.Enabled = true;
        }
       
    }
    protected void grdINASSIT_D_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }

    protected void btnAutoCreateTest_Click(object sender, EventArgs e)
    {
        this.btnAutoCreateTest.Enabled = false;
        string msg = string.Empty;
        if (this.CheckData())
        {
            if (txtINASN_id.Text.Length > 0)
            {
                this.btnAutoCreate.Enabled = false;

                //保存单头
                SaveDate(sender);
                string guid = Guid.NewGuid().ToString();
                Proc_AutoCreateInAssit_D_Test proc = new Proc_AutoCreateInAssit_D_Test();
                proc.P_InAssit_id = hfId.Value.Trim();
                proc.P_Guid = guid;
                proc.P_UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                proc.Execute();
                if (proc.P_ReturnValue == 0)
                {
                    //获取已生成的上架指引
                    this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
                    msg = Resources.Lang.FrmINASSITEdit_MSG6;//"生成成功!";
                    this.btnAutoCreateTest.Enabled = false;
                }
                else
                {
                    //msg = proc.INFOTEXT + "生成失败!";
                    msg = Resources.Lang.FrmINASSITEdit_MSG7; //"生成失败!";
                    this.btnAutoCreateTest.Enabled = true;
                }
                msg = new ErrorMsg_Query().GetErrorMsg(guid) + msg;
            }
            else
            {
                //请选择入库通知单
                msg = Resources.Lang.FrmINASSITEdit_MSG8+"!";
                this.btnAutoCreateTest.Enabled = true;
            }
            Alert(msg);
        }
    }

    //打印功能
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string printid = this.KeyID;
            //打印上架指引单
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASSITEdit_Print.aspx", SYSOperation.New, printid) + "','" + Resources.Lang.FrmINASSITEdit_MSG9 + "','BAR_REPACK',840,600);");

        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }
}

