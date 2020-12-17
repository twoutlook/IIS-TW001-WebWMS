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


/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/

public partial class Report_FrmHandOver_Check_Report : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       
  
        //grdNavigatorSNList.CurrentPageIndex = 0;
        //DataTable dtRowCount = ReportDataSource.GetHandout_Check(txtErpCode.Text.Trim(), txtCinvCode.Text.Trim().ToUpper(), txtCheckUser.Text,txtBeginDate.Text.Trim(),txtEndDate.Text.Trim(), true, 0, 0);
        
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
        Bind("");
    }

    //20130605103658
    protected DataTable grdNavigatorSNList_GetExportToExcelSource()
    {

        DataTable dtSource = null;// ReportDataSource.GetHandout_Check(txtErpCode.Text.Trim(), txtCinvCode.Text.Trim().ToUpper(), txtCheckUser.Text, txtBeginDate.Text.Trim(), txtEndDate.Text.Trim(), false, -1, -1);
        return dtSource;
    }


    public void GridBind()
    {
        //DataTable dtSource = ReportDataSource.GetHandout_Check(txtErpCode.Text.Trim(), txtCinvCode.Text.Trim().ToUpper(), txtCheckUser.Text, txtBeginDate.Text.Trim(), txtEndDate.Text.Trim(), false, this.grdNavigatorSNList.CurrentPageIndex, this.grdSNList.PageSize);
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


    public IQueryable<view_get_handover_check> GetQueryList()
    {
        IGenericRepository<view_get_handover_check> conn = new GenericRepository<view_get_handover_check>(db);
        var caseList = from p in conn.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtErpCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtErpCode.Text.Trim()));
            }
            if (txtCinvCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvCode.Text.Trim()));
            }
            if (txtCheckUser.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.createowner) && x.createowner.Contains(txtCheckUser.Text.Trim()));
            }

            DateTime d;
            if (txtBeginDate.Text != string.Empty)
            {
                if (DateTime.TryParse(txtBeginDate.Text, out d))
                {
                    caseList = caseList.Where(x => x.createtime.HasValue && x.createtime.Value >=d);
                }
            }
            if (txtEndDate.Text != string.Empty)
            {
                if (DateTime.TryParse(txtEndDate.Text, out d))
                {
                    caseList = caseList.Where(x => x.createtime.HasValue && x.createtime.Value < d.AddDays(1));
                }
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

        grdSNList.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdSNList.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }


}