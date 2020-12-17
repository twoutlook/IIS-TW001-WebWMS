using System;
using System.Data;
using System.Linq;
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
public partial class Apps_Bar_FrmBAR_Type : WMSBasePage //PageBase, IPageGrid
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
        IGenericRepository<BAR_TYPE> entity = new GenericRepository<BAR_TYPE>(context);
        IGenericRepository<BASE_OPERATOR> entity1 = new GenericRepository<BASE_OPERATOR>(context);
        var caseList = from p in entity.Get()
                       join q in entity1.Get() on p.craeteowner equals q.caccountid
                       orderby p.createtime descending
                       where 1 == 1
                       select new
                       {
                           p.id,
                           p.type,
                           p.typename,
                           p.mix,
                           p.max_qty,
                           p.lastmodifytime,
                           p.lastmodifyowner,
                           p.barcode_type,
                           p.craeteowner,
                           p.createtime,
                           q.coperatorname
                       };

        if (txtCNAME.Text.Trim() != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.typename) && x.typename.Contains(txtCNAME.Text.Trim()));



        if (dplYN.SelectedValue != "")
            caseList = caseList.Where(x => x.mix.ToString().Equals(dplYN.SelectedValue));

        if (dplType.SelectedValue != "")
            caseList = caseList.Where(x => x.barcode_type.ToString().Equals(dplType.SelectedValue));
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        AspNetPager1.CustomInfoHTML = " 总页数:<b>" + "</b>";
        grdBAR_Type.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdBAR_Type.DataBind();

        //Bar_FrmBAR_TYPEListQuery listQuery = new Bar_FrmBAR_TYPEListQuery();
        //DataTable dtSource = listQuery.GetList_T(txtCNAME.Text.Trim(), dplType.SelectedValue, dplYN.SelectedValue, dplCode.SelectedValue, false, this.grdNavigatorBAR_Type.CurrentPageIndex, this.grdBAR_Type.PageSize);
        //this.grdBAR_Type.DataSource = dtSource;
        //this.grdBAR_Type.DataBind();
        //;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
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
 
    public bool CheckData()
    {


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
        this.grdBAR_Type.DataKeyNames = new string[] { "ID" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmBAR_Type_Edit.aspx", SYSOperation.New, "") + "','新建類型','BAR_TYPE');return false;";
        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmBAR_PALLETEdit.aspx", SYSOperation.New,""),800,600);

    }

    #endregion

   

  
   

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorBAR_PALLET
    }

    /// <summary>
    /// 删除按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<BAR_TYPE> con = new GenericRepository<BAR_TYPE>(context);
        try
        {
            
            string sn = "0";
            for (int i = 0; i < this.grdBAR_Type.Rows.Count; i++)
            {
                if (this.grdBAR_Type.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBAR_Type.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string typeid = this.grdBAR_Type.DataKeys[i].Values[0].ToString();
                        if (BarQuery.CheckTypeIDExists(typeid))
                        {
                            Alert("該類型已經關聯棧板/箱，不能刪除！");
                            return;
                        }
                        else
                        {
                            con.Delete(typeid);
                            con.Save();
                         
                        }


                    }
                }
            }

            this.Alert("删除成功！");
            this.GridBind();

        }
        catch (Exception E)
        {
            this.Alert("删除失败！" + E.Message.ToJsString());
        }
    }



    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdBAR_Type.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBAR_Type.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdBAR_Type_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[8].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBAR_Type_Edit.aspx", SYSOperation.Modify, strKeyID), "類型維護", "BAR_CARTON_N");

            //箱或棧板
            switch (e.Row.Cells[2].Text.Trim().ToUpper())
            {
                case "0":
                    e.Row.Cells[2].Text = "棧板";
                    break;
                case "1":
                    e.Row.Cells[2].Text = "箱";
                    break;
                default:
                    break;
            }
            //是否混放状态
            switch (e.Row.Cells[3].Text.Trim().ToUpper())
            {
                case "Y":
                    e.Row.Cells[3].Text = "是";
                    break;
                case "N":
                    e.Row.Cells[3].Text = "否";
                    break;
                default:
                    break;
            }
            //条码类型
            switch (e.Row.Cells[4].Text.Trim().ToUpper())
            {
                case "39":
                    e.Row.Cells[4].Text = "Code39";
                    break;
                case "128":
                    e.Row.Cells[4].Text = "Code128";
                    break;
                default:
                    break;
            }

        }
    }

    protected void dsgrdBAR_Type_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //if (this.IsPostBack == false)
        //{
        //    e.Cancel = true;
        //}        
    }

    protected void dsgrdBAR_Type_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
      
    }
    #endregion
}

