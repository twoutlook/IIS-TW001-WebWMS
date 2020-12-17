using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;

/// <summary>
/// 描述: 111-->FrmBAR_CARTON_NList 页面后台类文件
/// 作者: --wjw
/// 创建于: 2013-1-05 19:13:38
/// </summary>
/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/
public partial class FrmBAR_PALLET_NList : WMSBasePage //PageBase, IPageGrid
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }


    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<BAR_CARTON_M> entity = new GenericRepository<BAR_CARTON_M>(context);
        IGenericRepository<BAR_TYPE> entityType = new GenericRepository<BAR_TYPE>(context);
        var caseList = from p in entity.Get()
                       join sr in entityType.Get() on p.type_id equals sr.id
                       orderby p.createtime descending
                       where 1 == 1
                       select new
                       {
                           carton_no = p.carton_no,
                           carton_name = p.carton_name,
                           mix = sr.mix,
                           barcode_type = sr.barcode_type

                       };
        if (txtCCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.carton_no) && x.carton_no.Contains(txtCCODE.Text));


        if (txtCNAME.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.carton_name) && x.carton_name.Contains(txtCNAME.Text));
        }

        if (dplYN.SelectedValue != "")
            caseList = caseList.Where(x => x.mix.ToString().Equals(dplYN.SelectedValue));
        if (dplCode.SelectedValue != "")
            caseList = caseList.Where(x => x.barcode_type.ToString().Equals(dplCode.SelectedValue));
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        AspNetPager1.CustomInfoHTML = " 总页数:<b>" + "</b>";
        grdBAR_CARTON.DataSource = GetPageSize(caseList.AsQueryable(), PageSize, CurrendIndex).ToList();
        grdBAR_CARTON.DataBind();



        //Bar_FrmBAR_CARTON_MListQuery listQuery = new Bar_FrmBAR_CARTON_MListQuery();
        //DataTable dtSource = listQuery.GetList(txtCCODE.Text, txtCNAME.Text, dplYN.SelectedValue, dplCode.SelectedValue, false, this.grdNavigatorBAR_CARTON.CurrentPageIndex, this.grdBAR_CARTON.PageSize);
        //this.grdBAR_CARTON.DataSource = dtSource;
        //this.grdBAR_CARTON.DataBind();
        //;
    }

    public bool CheckData()
    {

        if (this.txtCCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtCNAME.Text.Trim().Length > 0)
        {
        }
        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdBAR_CARTON.DataKeyNames = new string[] { "ID", "CARTON_NO" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmBAR_CARTON_NEdit.aspx", SYSOperation.New, "") + "','新建箱','BAR_CARTON_N');return false;";
        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmBAR_PALLETEdit.aspx", SYSOperation.New,""),800,600);
        //批量新增btnNew0
        this.btnNew0.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmBAR_CARTON_NewEdit.aspx", SYSOperation.New, "") + "','批量新建箱','BAR_CARTON_N');return false;";
        //this.OpenWin(this.btnNew0,PageBase.BuildRequestPageURL("FrmBAR_CARTON_NewEdit.aspx",SYSOperation.New, ""),600,400); 
        Help.DropDownListDataBind(GetParametersByFlagType("YorN"), this.dplYN, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", ""); //全部             //是否混放
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

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorBAR_PALLET
    }

    /// <summary>
    /// 记录栈板号数据集
    /// </summary>
    public Dictionary<string, string> SelectID
    {
        set { ViewState["SelectID"] = value; }
        get { return ViewState["SelectID"] as Dictionary<string, string>; }
    }
    /// 箱号删除
    /// <summary>
    /// 箱号删除
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
            for (int i = 0; i < this.grdBAR_CARTON.Rows.Count; i++)
            {
                if (this.grdBAR_CARTON.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBAR_CARTON.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string id = this.grdBAR_CARTON.DataKeys[i].Values[0].ToString();
                        if (!(SelectID.ContainsKey(id)))
                        {
                            SelectID.Add(id, id);
                        }
                    }
                }
            }
            if (SelectID.Count == 0)
            {
                this.Alert("请选择需要删除的箱号！");
            }
            else if (SelectID.Count > 0)
            {
                Del_CartonNo();
            }
            SelectID.Clear();
            // this.GridBind();      
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        catch (Exception E)
        {
            this.Alert("删除失败！" + E.Message.ToJsString());
        }

        #region 注销
        //DBUtil.BeginTrans();
        //try
        //{
        //    Bar_FrmBAR_CARTON_MListQuery list=new Bar_FrmBAR_CARTON_MListQuery();
        //    string sn = "0";
        //        for (int i = 0; i < this.grdBAR_CARTON.Rows.Count; i++)
        //        {
        //            if (this.grdBAR_CARTON.Rows[i].Cells[0].Controls[1] is CheckBox)
        //            {
        //                CheckBox chkSelect = (CheckBox) this.grdBAR_CARTON.Rows[i].Cells[0].Controls[1];
        //                if (chkSelect.Checked)
        //                {
        //                    BAR_CARTON_MEntity entity = new BAR_CARTON_MEntity();
        //                    //主键赋值
        //                    entity.ID = this.grdBAR_CARTON.DataKeys[i].Values[0].ToString();
        //                    string code = list.GetCode(this.grdBAR_CARTON.DataKeys[i].Values[0].ToString());
        //                    if (list.GetSN(code))
        //                    {
        //                        Alert("箱號：" + code + "已存在于上架中，不能刪除");
        //                        sn = "1";
        //                    }
        //                    else
        //                    {
        //                        BAR_CARTON_MRule.Delete(entity); //执行动作
        //                    }
        //                }
        //            }
        //        }
        //       if (sn == "0")
        //       {
        //           this.Alert("删除成功！");
        //       }
        //        DBUtil.Commit();
        //       // this.GridBind();      
        //    this.btnSearch_Click(this.btnSearch,EventArgs.Empty);
        //}
        //catch (Exception E)
        //{
        //    this.Alert("删除失败！" + E.Message.ToJsString());
        //    DBUtil.Rollback();
        //} 
        #endregion
    }

    /// 删除箱号
    /// <summary>
    /// 删除箱号
    /// </summary>
    public void Del_CartonNo()
    {
        try
        {
            string ErrMsg = string.Empty;
            string returnValue = string.Empty;
            //Bar_PalletCarton_Query.ExecProc_Del_PalletCarton_D(SelectID, "3", WmsWebUserInfo.GetCurrentUser().UserNo, ref ErrMsg, ref returnValue);
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
        for (int i = 0; i < this.grdBAR_CARTON.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBAR_CARTON.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = this.grdBAR_CARTON.DataKeys[rowIndex].Values[0].ToString();//strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdBAR_CARTON_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[9].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBAR_CARTON_NEdit.aspx", SYSOperation.Modify, strKeyID), "箱", "BAR_CARTON_N");

            //HyperLink linkModify_P = (HyperLink)e.Row.Cells[10].Controls[0];
            //linkModify_P.NavigateUrl = "#";
            //if (e.Row.Cells[6].Text.Trim() == "128")
            //{
            //    this.OpenFloatWin(linkModify_P, BuildRequestPageURL("/Apps/BAR/Carton_Print_Y.aspx", SYSOperation.Modify, strKeyID), "打印页面", "PrintCarton", 800, 600);
            //}
            //else
            //{    
            //  this.OpenFloatWin(linkModify_P, BuildRequestPageURL("/Apps/BAR/Carton_Print.aspx", SYSOperation.Modify, strKeyID), "打印页面", "PrintCarton", 800, 600);
            //}

            //是否混放状态
            switch (e.Row.Cells[5].Text.Trim().ToUpper())
            {
                case "Y":
                    e.Row.Cells[5].Text = "是";
                    break;
                case "N":
                    e.Row.Cells[5].Text = "否";
                    break;
                default:
                    break;
            }
            //条码类型
            switch (e.Row.Cells[6].Text.Trim())
            {
                case "39":
                    e.Row.Cells[6].Text = "Code39";
                    break;
                case "128":
                    e.Row.Cells[6].Text = "Code128";
                    break;
                default:
                    break;
            }

        }
    }

    protected void dsgrdBAR_CARTON_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //if (this.IsPostBack == false)
        //{
        //    e.Cancel = true;
        //}        
    }

    protected void dsgrdBAR_CATION_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
       

    }
    /// <summary>
    /// 记录栈板号数据集
    /// </summary>
    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }

    public string item = "";
    public string temp = "";
    public string ids = "";
    /// <summary>
    /// 打印功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GetSelectedIds();
            if (SelectIds.Count > 0)
            {
                GetID();
                Session["ids"] = ids;
                if (dplCode.SelectedValue == "128")
                {
                    this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("/Apps/BAR/Carton_Print_Y.aspx", SYSOperation.New, "") + "','打印條碼','BAR_PALLET_M',800,600);");
                }
                else
                {
                    this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("/Apps/BAR/Carton_Print.aspx", SYSOperation.New, "") + "','打印條碼','BAR_PALLET_M',800,600);");
                }
                SelectIds.Clear();
                // this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("/Apps/BAR/Carton_Print.aspx", SYSOperation.New, "") + "','打印條碼','BAR_PALLET_M',800,600);");
            }
            else
            {
                Alert("请选择需要打印的项");
            }

        }
        catch (Exception)
        {

            throw;
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

        foreach (GridViewRow item in this.grdBAR_CARTON.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string id = this.grdBAR_CARTON.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                //控件选中且集合中不存在添加
                if (cbo.Enabled && cbo.Checked && !SelectIds.ContainsKey(id))
                {
                    SelectIds.Add(id, this.grdBAR_CARTON.DataKeys[item.RowIndex][1].ToString());
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(id))
                {
                    SelectIds.Remove(id);
                }
            }
        }
    }

    StringBuilder strid = new StringBuilder();
    /// <summary>
    /// 获取传递的ID字符串
    /// </summary>
    public void GetID()
    {
        try
        {
            foreach (var items in SelectIds)
            {
                strid.Append("'" + items.Value + "',");
            }
            ids = strid.ToString().Substring(1, strid.ToString().Length - 3);
        }
        catch (Exception)
        {

            throw;
        }
    }

    /// <summary>
    /// 批量打印按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBatchPrint_Click(object sender, EventArgs e)
    {
        try
        {
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("/Apps/BAR/Carton_BatchPrint.aspx", SYSOperation.New, "") + "','批量打印','BAR_Carton_M',850,650);");
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
}

