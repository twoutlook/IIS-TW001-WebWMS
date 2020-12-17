using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Collections;


public partial class Apps_Report_FrmBASE_CARGOSPACE_Report : WMSBasePage
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Help.DropDownListDataBind(GetParametersByFlagType("C"), this.DropDownList1, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
            Help.DropDownListDataBind(GetParametersByFlagType("TrueOrFalse"), this.DropDownList2, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        }
        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
    }
    public DataTable ToExcel()
    {
        List<V_CARGOSPACE_Report> list = new List<V_CARGOSPACE_Report>();

        IGenericRepository<V_CARGOSPACE_Report> con = new GenericRepository<V_CARGOSPACE_Report>(context);
        var caseList = from p in con.Get()
                       orderby p.cposition descending
                       where 1 == 1
                       select p;
        if (TextBox2.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwareid) && x.cwareid.Contains(TextBox2.Text.Trim()));
        if (TextBox3.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(TextBox3.Text.Trim()));
        }
        //if (TextBox1.Text != string.Empty)
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(TextBox1.Text.Trim()));

        if (DropDownList1.SelectedValue != "")
            caseList = caseList.Where(x => x.cstatus.ToString().Equals(DropDownList1.SelectedValue));
        if (DropDownList2.SelectedValue != "")
            caseList = caseList.Where(x => x.ipermitmix.ToString().Equals(DropDownList2.SelectedValue));

        list = caseList.ToList();
        btnExcel.ExcelName = Resources.Lang.FrmBASE_CARGOSPACE_Report_MSG1;//"储位明细报表";

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("cstatus", "C"));//类型
        flagList.Add(new Tuple<string, string>("ipermitmix", "TrueOrFalse"));//是否补单

        var srcdata = GetGridSourceDataByList(list, flagList);
        return srcdata;
    }
    public void GridBind()
    {
        IGenericRepository<V_CARGOSPACE_Report> con = new GenericRepository<V_CARGOSPACE_Report>(context);
        var caseList = from p in con.Get()
                       orderby p.cposition descending
                       where 1 == 1
                       select p;
        if (TextBox2.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwareid) && x.cwareid.Contains(TextBox2.Text.Trim()));
        if (TextBox3.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(TextBox3.Text.Trim()));
        }
        //if (TextBox1.Text != string.Empty)
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(TextBox1.Text.Trim()));

        if (DropDownList1.SelectedValue != "")
            caseList = caseList.Where(x => x.cstatus.ToString().Equals(DropDownList1.SelectedValue));
        if (DropDownList2.SelectedValue != "")
            caseList = caseList.Where(x => x.ipermitmix.ToString().Equals(DropDownList2.SelectedValue));


        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.PageSize = this.PageSize;

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("cstatus", "C"));//类型
        flagList.Add(new Tuple<string, string>("ipermitmix", "TrueOrFalse"));//是否补单

        var srcdata = GetGridSourceDataByList(data, flagList);

        this.grdINASN.DataSource = srcdata;
        this.grdINASN.DataBind();
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
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //}

    }
}