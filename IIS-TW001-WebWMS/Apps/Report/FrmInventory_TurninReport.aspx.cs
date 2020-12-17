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
using System.Data.Entity.SqlServer;
using System.Collections;
public partial class FrmInventory_TurninReport : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.InitPage();
            txtDINDATEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            txtDINDATETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        //DataTable dtSource = ReportDataSource.SN_ResetRepoort(txtpanuser.Text, ddlsType.SelectedValue, txtDINDATEFrom.Text, txtDINDATETo.Text, txtCpositionCode.Text.Trim().ToUpper(), txtCinvcode.Text.Trim().ToUpper(), false, this.grdNavigatorINBILL.CurrentPageIndex, this.grdINBILL.PageSize);
        //this.grdINBILL.DataSource = dtSource;
        //this.grdINBILL.DataBind();
    }

    public DataTable ToExcel()
    {
        List<view_inventory_turnin> list = new List<view_inventory_turnin>();

        list = GetQueryList().ToList();
        btnExcel.ExcelName = Resources.Lang.FrmInventory_TurninReport_title1;//"SN盘入明细报表";

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("stype", "DiskType"));//是否保税
        flagList.Add(new Tuple<string, string>("worktype", "WareHouseType"));//类型

        var srcdata = GetGridSourceDataByList(list, flagList);

        return srcdata;
    }


    public IQueryable<view_inventory_turnin> GetQueryList()
    {
        IGenericRepository<view_inventory_turnin> conn = new GenericRepository<view_inventory_turnin>(db);
        var caseList = from p in conn.Get()
                       orderby p.cinvcode ascending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtpanuser.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinpersoncode) && x.cinpersoncode.Contains(txtpanuser.Text.Trim()));
            }
            if (ddlsType.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.stype) && x.stype.Equals(ddlsType.SelectedValue));
            }
            if (txtDINDATEFrom.Text != string.Empty)
                caseList = caseList.Where(x => x.dindate != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.dindate) >= 0);
            if (txtDINDATETo.Text != string.Empty)
                caseList = caseList.Where(x => x.dindate != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.dindate) <= 0);
            if (txtCpositionCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCpositionCode.Text.Trim()));
            }
            if (txtCinvcode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));
            }
            if (ddlworktype.SelectedValue != "")
                caseList = caseList.Where(x => x.worktype.ToString().Equals(ddlworktype.SelectedValue));

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
        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("stype", "DiskType"));//是否保税
        flagList.Add(new Tuple<string, string>("worktype", "WareHouseType"));//类型

        var srcdata = GetGridSourceDataByList(data, flagList);

        grdINBILL.DataSource = srcdata;
        grdINBILL.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    public bool CheckData()
    {
        return true;
    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";
        Help.DropDownListDataBind(GetParametersByFlagType("WareHouseType"), this.ddlworktype, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(GetParametersByFlagType("DiskType"), this.ddlsType, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
    }

    #endregion



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        Bind("");
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorINBILL
    }

    protected void grdINBILL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //}
    }

    protected void dsGrdINBILL_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdINBILL_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //if (e.ReturnValue is DataTable)
        //{
        //    if (grdNavigatorINBILL.IsDbPager == false)
        //        this.grdNavigatorINBILL.RowCount = ((DataTable)e.ReturnValue).Rows.Count;
        //}
    }

}

