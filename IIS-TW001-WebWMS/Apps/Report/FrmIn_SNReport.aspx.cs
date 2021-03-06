﻿using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.SqlServer;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Collections;

public partial class FrmIn_SNReport : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.InitPage();
            //txtDINDATEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            //txtDINDATETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        //DataTable dtSource = ReportDataSource.OutBill_SNRepoort(txtErpcode.Text.Trim(), txtDINDATEFrom.Text, txtDINDATETo.Text,txtWare.Text.Trim().ToUpper(),txtCpositionCode.Text.Trim().ToUpper(), txtCinvcode.Text.Trim().ToUpper(), false, this.grdNavigatorINBILL.CurrentPageIndex, this.grdINBILL.PageSize);
        //this.grdINBILL.DataSource = dtSource;
        //this.grdINBILL.DataBind();

    }
    public DataTable ToExcel()
    {
        List<view_inbill_snreport> list = new List<view_inbill_snreport>();
        list = GetQueryList().ToList();
        btnExcel.ExcelName = "SN入库明细报表";

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("worktype", "WareHouseType"));//作业方式

        var srcdata = GetGridSourceDataByList(list, flagList);

        return srcdata;
    }

    public IQueryable<view_inbill_snreport> GetQueryList()
    {
        IGenericRepository<view_inbill_snreport> conn = new GenericRepository<view_inbill_snreport>(db);
        var caseList = from p in conn.Get()
                       orderby p.DINDATE descending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtErpcode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CERPCODE) && x.CERPCODE.Contains(txtErpcode.Text.Trim()));
            }
            if (txtDINDATEFrom.Text != string.Empty)
                caseList = caseList.Where(x => x.DINDATE != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.DINDATE) >= 0);
            if (txtDINDATETo.Text != string.Empty)
                caseList = caseList.Where(x => x.DINDATE != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.DINDATE) <= 0);
            if (txtWare.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CWAREID) && x.CWAREID.Contains(txtWare.Text.Trim()));
            }
            if (txtCpositionCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CPOSITIONCODE) && x.CPOSITIONCODE.Contains(txtCpositionCode.Text.Trim()));
            }
            if (txtCinvcode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CINVCODE) && x.CINVCODE.Contains(txtCinvcode.Text.Trim()));
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
        flagList.Add(new Tuple<string, string>("worktype", "WareHouseType"));//作业方式

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
        //this.grdINBILL.DataKeyNames = new string[] { "ID" };  
        Help.DropDownListDataBind(GetParametersByFlagType("WareHouseType"), this.ddlworktype, "全部", "FLAG_NAME", "FLAG_ID", "");
        Help.RadioButtonDataBind(GetParametersByFlagType("MonthOrWeek"), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");
    }

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        Bind("");
    }
    protected void grdINBILL_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdINBILL_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }
    protected void grdINBILL_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}