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
using DreamTek.ASRS.Business.Base;

/// <summary>
/// 描述: 11-->FrmBAR_CARTON_MEdit 页面后台类文件
/// 作者: --wjw
/// 创建于: 2013-1-03 16:13:00
/// </summary>
public partial class FrmBAR_CARTON_NEdit : WMSBasePage
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        //ucShowBarType.SetCompName = txtPTYPENAME.ClientID;
        //ucShowBarType.SetORGCode = txtTypeId.ClientID;
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData();
                this.btnNew.Enabled = true;
                this.btnDelete0.Enabled = true;
            }
            else
            {
                //txtCCREATEOWNERCODE.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                //txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            }
        }
        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BAR_CARTON_N');return false;";
        this.grdINASN_D.DataKeyNames = new string[] { "IDS" };
        //txtPTYPENAME.Attributes["onclick"] = "Show('" + ucShowBarType.GetDivName + "');";
        //btnNew
        this.btnNew.Attributes["onclick"] = "return PopupFloatWin('" + BuildRequestPageURL("FrmBAR_CARTON_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID + "','','BAR_CARTON_D',600,400);";

        Help.DropDownListDataBind(new BarQuery().GetBAR_TYPE("1"), ddlTYPE_ID, "", "TYPENAME", "ID", "");

        this.btnNew.Enabled = false;
        this.btnDelete0.Enabled = false;
    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<BAR_CARTON_M> con = new GenericRepository<BAR_CARTON_M>(context);
        var caseList = from p in con.Get()
                       where p.id == this.KeyID
                       select p;
        BAR_CARTON_M entity = caseList.ToList().FirstOrDefault();
        hf_Id.Value = entity.id;
        this.txtCCODE.Text = entity.carton_no;
        this.txtCNAME.Text = entity.carton_name;
        ddlTYPE_ID.SelectedValue = entity.type_id;
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<BAR_CARTON_D> entity = new GenericRepository<BAR_CARTON_D>(context);
        var caseList = from p in entity.Get()
                       orderby p.createtime descending
                       where 1 == 1
                       select p;

        if (hf_Id.Value != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.Contains(hf_Id.Value));


       
       
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        AspNetPager1.CustomInfoHTML = " 总页数:<b>" + "</b>";
        grdINASN_D.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdINASN_D.DataBind();


        //Bar_FrmBAR_CARTON_DListQuery listQuery = new Bar_FrmBAR_CARTON_DListQuery();
        //DataTable dtSource = null;
        
        //if (hf_Id.Value.Length > 0)
        //{
        //    dtSource = listQuery.GetList("", hf_Id.Value, false, this.grdNavigatorINASN_D.CurrentPageIndex, this.grdINASN_D.PageSize);
        //}
        //this.grdINASN_D.DataSource = dtSource;
        //this.grdINASN_D.DataBind();
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
 
  
  

    

    protected void btnDeleteBAR_CARTON_M_Click(object sender, EventArgs e)
    {
        IGenericRepository<BAR_PALLET> con = new GenericRepository<BAR_PALLET>(context);
        try
        {
            con.Delete(this.KeyID.ToString());

            con.Save();
        }
        catch (Exception E)
        {
            this.Alert("删除失败！" + E.Message);
#if Debug 
            this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
        }

    }

    /// <summary>
    /// 记录料号
    /// </summary>
    public Dictionary<string, string> SelectID
    {
        set { ViewState["SelectID"] = value; }
        get { return ViewState["SelectID"] as Dictionary<string, string>; }
    }

    /// 删除料号按钮
    /// <summary>
    /// 删除料号按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        try
        {

            if (SelectID == null)
            {
                SelectID = new Dictionary<string, string>();
            }
            //if (Bar_FrmBAR_CARTON_DListQuery.CheckCartonIs_ExistPallet(txtCCODE.Text.Trim()))
            //{
            //    //已经关联栈板
            //    Alert("该箱号已经关联栈板，请解除关系后再删除料号！");
            //}
            //else
            //{
            //    for (int i = 0; i < this.grdINASN_D.Rows.Count; i++)
            //    {
            //        if (this.grdINASN_D.Rows[i].Cells[0].Controls[0] is CheckBox)
            //        {
            //            CheckBox chkSelect = (CheckBox)this.grdINASN_D.Rows[i].Cells[0].Controls[0];
            //            if (chkSelect.Checked)
            //            {
            //                string ids = this.grdINASN_D.DataKeys[i].Values[0].ToString();
            //                if (!(SelectID.ContainsKey(ids)))
            //                {
            //                    SelectID.Add(ids, ids);
            //                }

            //            }
            //        }
            //    }
            //    if (SelectID.Count == 0)
            //    {
            //        Alert("请选择需要删除的箱号");
            //    }
            //    else if (SelectID.Count > 0)
            //    {
            //        Del_Carton_D();
            //    }

            //}

            //SelectID.Clear();
            //this.btnSearch_Click(sender, e);

        }
        catch (Exception E)
        {
            this.Alert("删除失败！" + E.Message.ToJsString());
        }

        #region 注销
        //DBUtil.BeginTrans();
        //try
        //{
        //    Bar_FrmBAR_CARTON_MListQuery list = new Bar_FrmBAR_CARTON_MListQuery();
        //    if (list.GetSN(txtCCODE.Text.Trim()))
        //    {
        //        Alert("箱號：" + txtCCODE.Text.Trim() + "已存在于上架中，不能刪除料號");
        //    }
        //    else
        //    {
        //        for (int i = 0; i < this.grdINASN_D.Rows.Count; i++)
        //        {
        //            if (this.grdINASN_D.Rows[i].Cells[0].Controls[0] is CheckBox)
        //            {
        //                CheckBox chkSelect = (CheckBox)this.grdINASN_D.Rows[i].Cells[0].Controls[0];
        //                if (chkSelect.Checked)
        //                {
        //                    BAR_CARTON_DEntity entity = new BAR_CARTON_DEntity();
        //                    ////主键赋值
        //                    entity.IDS = this.grdINASN_D.DataKeys[i].Values[0].ToString();
        //                    BAR_CARTON_DRule.Delete(entity); //执行动作 

        //                }
        //            }
        //        }
        //        this.Alert("删除成功！");
        //        DBUtil.Commit();
        //        this.btnSearch_Click(sender, e);
        //    }
        //}
        //catch (Exception E)
        //{
        //    this.Alert("删除失败！" + E.Message.ToJsString());
        //    DBUtil.Rollback();
        //} 
        #endregion
    }

    /// 删除箱明细
    /// <summary>
    /// 删除箱明细
    /// </summary>
    public void Del_Carton_D()
    {
        try
        {
            string ErrMsg = string.Empty;
            string returnValue = string.Empty;
            //Bar_PalletCarton_Query.ExecProc_Del_PalletCarton_D(SelectID, "2", WmsWebUserInfo.GetCurrentUser().UserNo, ref ErrMsg, ref returnValue);
            //if (returnValue == "0")
            //{
            //    Alert("删除成功！");
            //}
            //else
            //{
            //    Alert(ErrMsg);
            //}
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }
    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdINASN_D.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdINASN_D.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdINASN_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBAR_CARTON_DEdit.aspx", SYSOperation.Modify, strKeyID), "箱明细编辑", "BAR_CARTON_D", 600, 350);

        }
    }

    protected void dsGrdINASN_D_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdINASN_D_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
     
    }
    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        ////
        //if (this.txtCCODE.Text.Trim() == "")
        //{
        //    this.Alert("编码项不允许空！");
        //    this.SetFocus(txtCCODE);
        //    return false;
        //}
        //////
        //if (this.txtCCODE.Text.Trim().Length > 0)
        //{
        //    if (this.txtCCODE.Text.GetLengthByByte() > 20)
        //    {
        //        this.Alert("编码项超过指定的长度20！");
        //        this.SetFocus(txtCCODE);
        //        return false;
        //    }
        //}
        ////
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
        return true;
    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BAR_CARTON_M SendData()
    {
        BAR_CARTON_M entity = new BAR_CARTON_M();
        this.txtCNAME.Text = this.txtCNAME.Text.Trim();
        if (this.txtCNAME.Text.Length > 0)
        {
            entity.carton_name = txtCNAME.Text;
        }
        else
        {
            //entity.SetDBNull("CARTON_NAME", true);
        }
        //类型
        entity.type_id = ddlTYPE_ID.SelectedValue.Trim();

        return entity;

    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveToDB(sender);
    }

    private void SaveToDB(object sender)
    {
        IGenericRepository<BAR_CARTON_M> con = new GenericRepository<BAR_CARTON_M>(context);
        if (this.CheckData())
        {
            BAR_CARTON_M entity = (BAR_CARTON_M)this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
                entity.id = this.KeyIDS[0];
            }
            string strKeyID = "";

            try
            {
                if (this.Operation() == SYSOperation.Modify)
                {
                    strKeyID = hf_Id.Value.Trim();
                    entity.id = strKeyID;
                    entity.lastmodifytime = DateTime.Now;
                    entity.lastmodifyownere = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Update(entity);
                    con.Save();
                    this.AlertAndBack("FrmBAR_CARTON_NEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), "保存成功");
                }

                else if (this.Operation() == SYSOperation.New)
                {
                    entity.carton_no = new Fun_CreateNo().CreateNo("BAR_CARTON_M");
                    strKeyID = Guid.NewGuid().ToString();
                    entity.id = strKeyID;
                    entity.createtime = DateTime.Now;
                    entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Insert(entity);
                    con.Save();
                    this.AlertAndBack("FrmBAR_CARTON_NEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), "保存成功");
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

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    string ids = (sender as LinkButton).CommandArgument;
    //    this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASN_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + ids + "&InType=" + txtITYPE.SelectedValue.Trim() + "','','BAR_CARTON_D',600,350);");
    //}

    protected void txtITYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (txtITYPE.SelectedValue == "44")
        //{
        //    this.btnNew.Enabled = false;
        //    this.btnDelete0.Enabled = false;
        //}
        //else
        //{
        //    this.btnNew.Enabled = true;
        //    this.btnDelete0.Enabled = true;
        //}
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

        foreach (GridViewRow item in this.grdINASN_D.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string ids = this.grdINASN_D.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

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

    /// <summary>
    /// 生成入库单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateInBill_Click(object sender, EventArgs e)
    {
    }

    protected void grdINASN_D_DataBinding(object sender, EventArgs e)
    {
        //GetSelectedIds();
    }
    #endregion
}

