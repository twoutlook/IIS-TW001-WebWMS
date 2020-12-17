using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;

/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/

using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Collections;


public partial class Apps_Report_FrmBASE_SNList_Report : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        btnExcel.GetExportToExcelSource3 += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler3(ToExcel);
    }
    public IList ToExcel()
    {
        List<view_get_snlist_report> list = new List<view_get_snlist_report>();
      
        list = GetQueryList().ToList();
        btnExcel.ExcelName = Resources.Lang.FrmBASE_SNList_Report_Content1;//"条码查询报表";
        return list;
    }
	
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //CQ 2014-6-17 13:19:34
        //2014-8-21 09:42:09
        //if (txtCinvCode.Text.Trim() == "" && txtSN.Text.Trim() == "" && txtCpositioncode.Text.Trim()=="")
        //{
        //    this.Alert("查询条件不能全部为空！");
        //    return;
        //}
        //ReportDataSource da = new ReportDataSource();
        //grdNavigatorSNList.CurrentPageIndex = 0;
        //DataTable dtRowCount = da.GetSNList_Report(txtSN.Text.Trim().ToUpper(), txtCinvCode.Text.Trim().ToUpper(),txtCpositioncode.Text.Trim().ToUpper(), true, 0, 0);
        
        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdNavigatorSNList.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdNavigatorSNList.RowCount = 0;
        //}

        //this.GridBind();
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        Bind("");
    }

    public IQueryable<view_get_snlist_report> GetQueryList()
    {
        IGenericRepository<view_get_snlist_report> conn = new GenericRepository<view_get_snlist_report>(db);
        var caseList = from p in conn.Get()
                       orderby p.cinvcode descending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtSN.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.sn_code) && x.sn_code.Contains(txtSN.Text.Trim()));
            }
            if (txtCinvCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvCode.Text.Trim()));
            }
            if (txtCpositioncode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCpositioncode.Text.Trim()));
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
        else
        {
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }
        grdSNList.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdSNList.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }



    //20130605103658
    protected DataTable grdNavigatorSNList_GetExportToExcelSource()
    {
        //ReportDataSource da = new ReportDataSource();
        //DataTable dtSource = da.GetSNList_Report(txtSN.Text.Trim().ToUpper(), txtCinvCode.Text.Trim().ToUpper(), txtCpositioncode.Text.Trim().ToUpper(), false, -1, -1);
        //return dtSource;
        return null;
    }


    public void GridBind()
    {
        //ReportDataSource da = new ReportDataSource();
        //DataTable dtSource = da.GetSNList_Report(txtSN.Text.Trim().ToUpper(), txtCinvCode.Text.Trim().ToUpper(), txtCpositioncode.Text.Trim().ToUpper(), false, this.grdNavigatorSNList.CurrentPageIndex, this.grdSNList.PageSize);
        //this.grdSNList.DataSource = dtSource;
        //this.grdSNList.DataBind();
    }

    protected void grdSNList_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    protected void grdSNList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorSNList.IsDbPager)
        //{
        //    grdNavigatorSNList.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdSNList.PageIndex = e.NewPageIndex;
        //}
    }

    protected void grdSNList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }

    }
}