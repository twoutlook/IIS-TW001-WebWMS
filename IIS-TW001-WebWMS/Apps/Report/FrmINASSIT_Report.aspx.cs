using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.Entity.SqlServer;
using DreamTek.ASRS.Business.Import;

public partial class Apps_Report_FrmINASSIT_Report : WMSBasePage
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInType();
            // this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
    }

    private void LoadInType()
    {
        Help.DropDownListDataBind(GetInType(true), this.ddlInType, Resources.Lang.Common_ALL, "FUNCNAME", "EXTEND1", "");
        Help.DropDownListDataBind(GetParametersByFlagType("WareHouseType"), this.ddlworktype, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
    }

    public DataTable ToExcel()
    {
        int pageCount = 0;
        DataTable dt = new DataTable();
        ReportQuery query = new ReportQuery();
        btnExcel.ExcelName = Resources.Lang.FrmINASSIT_Report_MSG1;//"上架明细报表";
        dt = query.GetInassitReportList(txtInAsnCticketcode.Text.Trim(), txtInAssiCticketcode.Text.Trim(), txtErpCode.Text.Trim(), ddlInType.SelectedValue, txtDCREATETIMEFrom.Text.Trim(), txtDCREATETIMETo.Text.Trim(), TextBox2.Text.Trim(), TextBox3.Text.Trim(), TextBox1.Text.Trim(), ddlworktype.SelectedValue, -1, PageSize, out pageCount);

        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("worktype", "newworktype", "WareHouseType"));//作业方式
        flagList.Add(new Tuple<string, string, string>("intype", "newintype", "INTYPE"));//入库方式
        var srcdata = GetGridDataByDataTable(dt, flagList);

        return srcdata;
    }
	
    public void GridBind()
    {
        int pageCount = 0;
        ReportQuery query = new ReportQuery();
        DataTable dt = query.GetInassitReportList(txtInAsnCticketcode.Text.Trim(), txtInAssiCticketcode.Text.Trim(), txtErpCode.Text.Trim(), ddlInType.SelectedValue, txtDCREATETIMEFrom.Text.Trim(), txtDCREATETIMETo.Text.Trim(), TextBox2.Text.Trim(), TextBox3.Text.Trim(), TextBox1.Text.Trim(),ddlworktype.SelectedValue, CurrendIndex, PageSize, out pageCount);
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;

        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("worktype", "newworktype", "WareHouseType"));//作业方式
        flagList.Add(new Tuple<string, string, string>("intype", "newintype", "INTYPE"));//入库方式
        var srcdata = GetGridDataByDataTable(dt, flagList);
        grdINASN.DataSource = srcdata;
        grdINASN.DataBind();


        //ReportDataSource listQuery = new ReportDataSource();
        //DataTable dtSource = listQuery.FrmINASSIT_Report(txtInAsnCticketcode.Text, txtInAssiCticketcode.Text, txtErpCode.Text, txtDCREATETIMEFrom.Text, txtDCREATETIMETo.Text, TextBox2.Text, TextBox3.Text, TextBox1.Text, ddlInType.SelectedValue, false, this.grdNavigatorINASN.CurrentPageIndex, this.grdINASN.PageSize);
        //this.grdINASN.DataSource = dtSource;
        //this.grdINASN.DataBind();
        //IGenericRepository<V_INASSIT_Report> con = new GenericRepository<V_INASSIT_Report>(context);
        //var caseList = from p in con.Get()
        //               orderby p.dindate descending
        //               where 1 == 1
        //               select p;
        //if (txtInAsnCticketcode.Text != string.Empty)
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.inasncticketcode) && x.inasncticketcode.Contains(txtInAsnCticketcode.Text.Trim()));
        //if (txtInAssiCticketcode.Text != string.Empty)
        //{
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.inassicticketcode) && x.inassicticketcode.Contains(txtInAssiCticketcode.Text.Trim()));
        //}
        //if (txtErpCode.Text != string.Empty)
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtErpCode.Text.Trim()));

        //if (ddlInType.SelectedValue != "")
        //    caseList = caseList.Where(x => x.intype.ToString().Equals(ddlInType.SelectedValue));
        //if (txtDCREATETIMEFrom.Text != string.Empty)
        //    caseList = caseList.Where(x => x.dindate != null && SqlFunctions.DateDiff("dd", txtDCREATETIMEFrom.Text.Trim(), x.dindate) >= 0);
        //if (txtDCREATETIMETo.Text != string.Empty)
        //    caseList = caseList.Where(x => x.dindate != null && SqlFunctions.DateDiff("dd", txtDCREATETIMETo.Text.Trim(), x.dindate) <= 0);

        //if (TextBox2.Text != string.Empty)
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwareid) && x.cwareid.Contains(TextBox2.Text.Trim()));
        //if (TextBox3.Text != string.Empty)
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(TextBox3.Text.Trim()));

        //if (TextBox1.Text != string.Empty)
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(TextBox1.Text.Trim()));
        //if (caseList != null && caseList.Count() > 0)
        //{
        //    AspNetPager1.RecordCount = caseList.Count();
        //    AspNetPager1.PageSize = this.PageSize;
        //}
        //else
        //{
        //    AspNetPager1.RecordCount = 0;
        //    AspNetPager1.PageSize = this.PageSize;
        //}
        //AspNetPager1.PageSize = this.PageSize;
        //this.grdINASN.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        //this.grdINASN.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        this.GridBind();
    }

    protected void grdINASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
}