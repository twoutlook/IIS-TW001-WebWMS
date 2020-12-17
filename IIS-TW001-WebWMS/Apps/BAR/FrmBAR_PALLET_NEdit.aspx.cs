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
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;

/// <summary>
/// 描述: 11-->FrmBAR_PALLETEdit 页面后台类文件
/// 作者: --wjw
/// 创建于: 2013-1-03 16:13:00
/// </summary>
public partial class FrmBAR_PALLET_NEdit : WMSBasePage //PageBase, IPageEdit
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        ucShowBarType.SetCompName = txtPTYPENAME.ClientID;
        ucShowBarType.SetORGCode = txtTypeId.ClientID;
        if (this.IsPostBack == false)
        {
            this.InitPage();
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
            if (this.Operation() != SYSOperation.New)
            {
                ShowData();

            }
            else
            {
                this.txtcreateuser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                this.txtcreatetime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
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
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BAR_PALLET');return false;";
        txtPTYPENAME.Attributes["onclick"] = "Show('" + ucShowBarType.GetDivName + "');";
        //本页面打开新增窗口
        //this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmBAR_PALLET_DEdit.aspx?IDM=" + this.KeyID, SYSOperation.New, "") + "','關聯棧板和箱','BAR_PALLET_D');return false;";
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //GridBind();
        
    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<BAR_PALLET_M> con = new GenericRepository<BAR_PALLET_M>(context);
        IGenericRepository<BAR_TYPE> con1 = new GenericRepository<BAR_TYPE>(context);
        var caseList = from p in con.Get()
                       where p.id == this.KeyID
                       select p;
        BAR_PALLET_M entity = caseList.ToList().FirstOrDefault();

         var caseList1 = from p in con1.Get()
                       where p.id==entity.type_id
                       select p;
     
        //this.drCSTATUS.SelectedValue = entity.CSTATUS;
        this.txtCCODE.Text = entity.palletno;
        this.txtCNAME.Text = entity.palletname;
        this.txtcreatetime.Text = Convert.ToDateTime(entity.createtime).ToString("yyyy-MM-dd");
        this.txtcreateuser.Text = entity.createowner;
        if (!string.IsNullOrEmpty(entity.lastmodifyownre))
        {
            this.txtupdatetime.Text = Convert.ToDateTime(entity.lastmodifytime).ToString("yyyy-MM-dd");
            this.txtupdateuser.Text = entity.lastmodifyownre;
        }
        txtTypeId.Text = entity.type_id;
        //TabMain0.Visible = true;
        if (caseList1!=null)
            this.txtPTYPENAME.Text = caseList1.ToList().FirstOrDefault().typename;


    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (this.txtCNAME.Text.Trim() == "")
        {
            this.Alert("名称项不允许空！");
            this.SetFocus(txtCNAME);
            return false;
        }
        ////
        if (this.txtCNAME.Text.Trim().Length > 0)
        {
            if (this.txtCNAME.Text.GetLengthByByte() > 100)
            {
                this.Alert("名称项超过指定的长度100！");
                this.SetFocus(txtCNAME);
                return false;
            }
        }
        //////
        //
        if (this.txtPTYPENAME.Text.Trim() == "")
        {
            this.Alert("类型项不能为空！");
            this.SetFocus(txtPTYPENAME);
            return false;
        }
        //////
        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BAR_PALLET_M SendData()
    {
        BAR_PALLET_M entity = new BAR_PALLET_M();
        if (this.Operation() == SYSOperation.Modify)
        {
            entity.id = this.KeyID;
            entity.palletno = this.txtCCODE.Text.Trim();
        }
        else if (this.Operation() == SYSOperation.New)
        {
            entity.id = Guid.NewGuid().ToString();
        }
        //
        this.txtCNAME.Text = this.txtCNAME.Text.Trim();
        if (this.txtCNAME.Text.Length > 0)
        {
            entity.palletname = txtCNAME.Text;
        }
        else
        {
            //entity.SetDBNull("PALLETNAME", true);
        }
        if (this.txtTypeId.Text.Length > 0)
        {
            entity.type_id = txtTypeId.Text;
        }
        else
        {
            //entity.SetDBNull("TYPE_ID", true);
        }

        #region 界面上不可见的字段项
        /*
        entity.ID = ;
        entity.CCREATEOWNERCODE = ;
        entity.DCREATETIME = ;
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
        IGenericRepository<BAR_PALLET_M> con = new GenericRepository<BAR_PALLET_M>(context);
        if (this.CheckData())
        {
            BAR_PALLET_M entity = (BAR_PALLET_M)this.SendData();
            string strKeyID = "";
            strKeyID += entity.id;
            try
            {
                if (this.Operation() == SYSOperation.Modify)
                {
                    entity.lastmodifytime = DateTime.Now;
                    entity.lastmodifyownre = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Update(entity);
                    con.Save();
                    this.AlertAndBack("FrmBAR_PALLET_NEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), "保存成功");
                }

                else if (this.Operation() == SYSOperation.New)
                {
                    entity.id = Guid.NewGuid().ToString();
                    entity.palletno = GetPalletNo("BAR_PALLET_M");
                    entity.createtime = DateTime.Now;
                    entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Insert(entity);
                    con.Save();
                    strKeyID = "";
                    strKeyID += entity.id;
                    this.AlertAndBack("FrmBAR_PALLET_NEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), "保存成功");
                }
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + "失败！" + E.Message);
#if Debug 
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
            }
        }

    }

    #region IPageGrid 成员

    //public void GridBind()
    //{

    //    IGenericRepository<BAR_PALLET_D> entity = new GenericRepository<BAR_PALLET_D>(context);
    //    var caseList = from p in entity.Get()
    //                   orderby p.createtime descending
    //                   where 1 == 1
    //                   select p;
    //    if (txtCCODE.Text != string.Empty)
    //        caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cartoncode) && x.cartoncode.Contains(txtCCODE.Text));


    //    if (this.KeyID != string.Empty)
    //    {
    //        caseList = caseList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.Contains(KeyID));
    //    }

      
    //    if (caseList != null && caseList.Count() > 0)
    //    {
    //        AspNetPager1.RecordCount = caseList.Count();
    //        AspNetPager1.PageSize = this.PageSize;
    //    }

    //    AspNetPager1.CustomInfoHTML = " 总页数:<b>" + "</b>";
    //    grdPallet_D.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
    //    grdPallet_D.DataBind();

    //    //Bar_FrmBAR_PALLET_DListQuery listQuery = new Bar_FrmBAR_PALLET_DListQuery();
    //    //DataTable dtSource = listQuery.GetList(txtCinvcode.Text, this.KeyID, false, this.grdNavigatorPallet_D.CurrentPageIndex, this.grdPallet_D.PageSize);
    //    //this.grdPallet_D.DataSource = dtSource;
    //    //this.grdPallet_D.DataBind();
    //    //;
    //}

    #endregion

    #region IPage 成员

    //public void InitPage()
    //{
    //    this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
    //   // this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
    //    this.grdPallet_D.DataKeyNames = new string[]{"ID"};
    //    #region Disable and ReadOnly
    //    #endregion Disable and ReadOnly
    //    //本页面打开新增窗口
    //    this.btnNew.Attributes["onclick"]= "PopupFloatWinMax('" +  PageBase.BuildRequestPageURL("FrmBAR_PALLET_NEdit.aspx", SYSOperation.New,"") + "','新建栈板','BAR_PALLET');return false;";
    //    //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmBAR_PALLETEdit.aspx", SYSOperation.New,""),800,600);

    //}

    #endregion

   

    //protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    //{
    //    this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
    //    GridBind();
    //}
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    CurrendIndex = 1;
    //    this.GridBind();
    //}

    private string GetPalletNo(string type)
    {
        string strSQL = string.Format(@"select dbo.Fun_CreateNo('{0}')", type);
        return DBHelp.ExecuteScalar(strSQL);
    }
   
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorPallet_D
    }
    /// <summary>
    /// 记录栈板号数据集
    /// </summary>
    public Dictionary<string, string> SelectID
    {
        set { ViewState["SelectID"] = value; }
        get { return ViewState["SelectID"] as Dictionary<string, string>; }
    }

    ///// 删除栈板明细
    ///// <summary>
    ///// 删除栈板明细
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (SelectID == null)
    //        {
    //            SelectID = new Dictionary<string, string>();
    //        }
    //        for (int i = 0; i < this.grdPallet_D.Rows.Count; i++)
    //        {
    //            if (this.grdPallet_D.Rows[i].Cells[0].Controls[0] is CheckBox)
    //            {
    //                CheckBox chkSelect = (CheckBox)this.grdPallet_D.Rows[i].FindControl("chkSelect");
    //                if (chkSelect.Checked)
    //                {
    //                    string ids = this.grdPallet_D.DataKeys[i].Values[0].ToString();
    //                    if (!(SelectID.ContainsKey(ids)))
    //                    {
    //                        SelectID.Add(ids, ids);
    //                    }
    //                }
    //            }
    //        }
    //        if (SelectID.Count == 0)
    //        {
    //            this.Alert("请选择需要删除的栈板明细！");
    //        }
    //        else if (SelectID.Count > 0)
    //        {
    //            Del_Pallet_D();
    //        }
    //        SelectID.Clear();
    //        //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    //    }
    //    catch (Exception E)
    //    {
    //        this.Alert("删除失败！" + E.Message.ToJsString());

    //    }
    //    #region 注销
    //    //DBUtil.BeginTrans();
    //    //try
    //    //{
    //    //    Bar_FrmBAR_PALLET_MListQuery list = new Bar_FrmBAR_PALLET_MListQuery();
    //    //    if (list.CheckTempSN(txtCCODE.Text.Trim()))
    //    //    {
    //    //        this.Alert("棧板號:" + txtCCODE.Text + "已存在上架明細中不能刪除！");
    //    //    }
    //    //    else
    //    //    {
    //    //        for (int i = 0; i < this.grdPallet_D.Rows.Count; i++)
    //    //        {
    //    //            if (this.grdPallet_D.Rows[i].Cells[0].Controls[0] is CheckBox)
    //    //            {
    //    //                CheckBox chkSelect = (CheckBox)this.grdPallet_D.Rows[i].FindControl("chkSelect");
    //    //                if (chkSelect.Checked)
    //    //                {
    //    //                    BAR_PALLET_DEntity entity = new BAR_PALLET_DEntity();
    //    //                    //主键赋值
    //    //                    entity.IDS = this.grdPallet_D.DataKeys[i].Values[0].ToString();
    //    //                    BAR_PALLET_DRule.Delete(entity);	//执行动作 
    //    //                }
    //    //            }
    //    //        }

    //    //        this.Alert("删除成功！");
    //    //        DBUtil.Commit();
    //    //    }
    //    //    this.GridBind();
    //    //}
    //    //catch (Exception E)
    //    //{
    //    //    this.Alert("删除失败！" + E.Message.ToJsString());
    //    //    DBUtil.Rollback();
    //    //} 
    //    #endregion
    //}

    /// 删除栈板号
    /// <summary>
    /// 删除栈板号
    /// </summary>
    //public void Del_Pallet_D()
    //{
    //    try
    //    {
    //        string ErrMsg = string.Empty;
    //        string returnValue = string.Empty;
    //        Bar_PalletCarton_Query.ExecProc_Del_PalletCarton_D(SelectID, "0", WmsWebUserInfo.GetCurrentUser().UserNo, ref ErrMsg, ref returnValue);
    //        if (returnValue == "0")
    //        {
    //            Alert("删除成功！");
    //        }
    //        else
    //        {
    //            Alert(ErrMsg);
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        Alert(err.Message);
    //    }
    //}

    //private string GetKeyIDS(int rowIndex)
    //{
    //    string strKeyId = "";
    //    for (int i = 0; i < this.grdPallet_D.DataKeyNames.Length; i++)
    //    {
    //        strKeyId += this.grdPallet_D.DataKeys[rowIndex].Values[i].ToString() + ",";
    //    }
    //    strKeyId = strKeyId.TrimEnd(',');
    //    return strKeyId;
    //}

    //protected void grdPallet_D_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        string strKeyID = this.GetKeyIDS(e.Row.RowIndex);


    //        HyperLink linkModify = (HyperLink)e.Row.Cells[3].Controls[0];
    //        linkModify.NavigateUrl = "#";
    //        this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBAR_PALLET_DEdit.aspx?IDM=" + this.KeyID, SYSOperation.Modify, strKeyID), "關聯棧板和箱", "BAR_PALLET_D");

    //        //    TableCell cell = e.Row.Cells[e.Row.Cells.Count - 2];

    //        //    switch (cell.Text)
    //        //    {
    //        //        case "0":
    //        //            cell.Text = "无效";
    //        //            break;
    //        //        case "1":
    //        //            cell.Text = "有效";
    //        //            break;
    //        //        default:
    //        //            break;
    //        //    }
    //        //}
    //    }
    //}

    protected void dsgrdPallet_D_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //if (this.IsPostBack == false)
        //{
        //    e.Cancel = true;
        //}        
    }

    protected void dsgrdPallet_D_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
      
    }
    #endregion

}

