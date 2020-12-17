using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;

public partial class FrmPart_NoReCord_Report : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.InitPage();
        }
    }
    #region IPageGrid 成员

    public void GridBind()
    {
        //labStock.Text = ReportDataSource.GetNoReCode_Stock(txtCinvcode.Text.Trim().ToUpper());
        //DataTable dtSource = ReportDataSource.Get_PartNoRecordRepoort(txtCinvcode.Text.Trim().ToUpper(), false, this.grdNavigatorStock.CurrentPageIndex, this.grdStock.PageSize);
        //this.grdStock.DataSource = dtSource;
        //this.grdStock.DataBind();

        CurrendIndex = 1;
        Bind("");

    }


    public IQueryable<view_get_part_norecode> GetQueryList()
    {
        IGenericRepository<view_get_part_norecode> conn = new GenericRepository<view_get_part_norecode>(db);
        var caseList = from p in conn.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCinvcode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));
            }
            
        }
        return caseList;
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

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        grdStock.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdStock.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    public bool CheckData()
    {
        if (txtCinvcode.Text.Trim() == "")
        {
            Alert(Resources.Lang.FrmPart_NoReCord_Report_MSG1);//+"查询料号不能为空"
            this.SetFocus(txtCinvcode);
            return false;
        }
        return true;
    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";
        //this.grdStock.DataKeyNames = new string[] { "ID" };
      
    }

    #endregion

    protected DataTable grdNavigatorStock_GetExportToExcelSource()
    {
        //DataTable dtSource = ReportDataSource.Get_PartNoRecordRepoort(txtCinvcode.Text.Trim().ToUpper(), false, -1, -1);
        //return dtSource;
        return new DataTable();
        ;
    }

    protected void grdStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorStock.IsDbPager)
        //{
        //    grdNavigatorStock.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdStock.PageIndex = e.NewPageIndex;
        //}
    }
    protected void grdStock_PageIndexChanged(object sender, EventArgs e)
    {
        //if(grdNavigatorStock.IsDbPager)
        {
            this.GridBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (CheckData())
        {
            ////重新设置GridNavigator的RowCount
            //grdNavigatorStock.CurrentPageIndex = 0;

            //DataTable dtRowCount = ReportDataSource.Get_PartNoRecordRepoort(txtCinvcode.Text.Trim().ToUpper(), true, 0, 0);
            //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
            //{
            //    this.grdNavigatorStock.RowCount = dtRowCount.Rows[0][0].ToInt32();
            //}
            //else
            //{
            //    this.grdNavigatorStock.RowCount = 0;
            //}
            // false, this.grdNavigatorStock.CurrentPageIndex, this.grdStock.PageSize
            this.GridBind();
        }
       
    }
  
    protected void grdStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
        }

    }

    protected void dsGrdStock_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdStock_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //if (e.ReturnValue is DataTable)
        //{
        //    if (grdNavigatorStock.IsDbPager == false)
        //        this.grdNavigatorStock.RowCount = ((DataTable)e.ReturnValue).Rows.Count;
        //}

    }

}

