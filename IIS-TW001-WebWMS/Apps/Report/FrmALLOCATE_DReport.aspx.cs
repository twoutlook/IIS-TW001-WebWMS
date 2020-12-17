using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Text;
using System.ComponentModel;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.Entity.SqlServer;
using System.Collections;


/// <summary>
/// 描述: 1111-->FrmALLOCATEList 页面后台类文件
/// 作者: --chenlei
/// 创建于: 2012-11-01 09:31:05
/// </summary>
public partial class FrmALLOCATE_DReport :WMSBasePage
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);

    }


    #region IPageGrid 成员
    public DataTable ToExcel()
    {
        List<V_ALLOCATE_DReport> list = new List<V_ALLOCATE_DReport>();

        IGenericRepository<V_ALLOCATE_DReport> con = new GenericRepository<V_ALLOCATE_DReport>(context);
        var caseList = from p in con.Get()
                       orderby p.cticketcode descending
                       where 1 == 1
                       select p;
        if (txtCCREATEOWNERCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccreateownercode) && x.ccreateownercode.Contains(txtCCREATEOWNERCODE.Text.Trim()));
        if (txtCAUDITPERSON.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cauditperson) && x.cauditperson.Contains(txtCAUDITPERSON.Text.Trim()));
        }
        if (ddlCSTATUS.SelectedValue != "")
            caseList = caseList.Where(x => x.cstatus.ToString().Equals(ddlCSTATUS.SelectedValue));
        if (txtCTICKETCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtCTICKETCODE.Text.Trim()));

        if (txtDCREATETIMEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMEFrom.Text.Trim(), x.dcreatetime) >= 0);
        if (txtDCREATETIMETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMETo.Text.Trim(), x.dcreatetime) <= 0);
        if (txtDAUDITTIMEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.daudittime != null && SqlFunctions.DateDiff("dd", txtDAUDITTIMEFrom.Text.Trim(), x.daudittime) >= 0);
        if (txtDAUDITTIMETo.Text != string.Empty)
            caseList = caseList.Where(x => x.daudittime != null && SqlFunctions.DateDiff("dd", txtDAUDITTIMETo.Text.Trim(), x.daudittime) <= 0);
        if (txtDINDATEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.dindate != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.dindate) >= 0);
        if (txtDINDATETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dindate != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.dindate) <= 0);

        if (txtERP.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtERP.Text.Trim()));
        if (txtLH.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtLH.Text.Trim()));
        if (ddlworktype.SelectedValue != "")
            caseList = caseList.Where(x => x.ALLOTYPE.ToString().Equals(ddlworktype.SelectedValue));
        if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
        {
            caseList = caseList.Where(x => x.cspecifications.ToString().Contains(txtcspec.Text.Trim()));
        }
        list = caseList.ToList();
        btnExcel.ExcelName = Resources.Lang.FrmALLOCATE_DReport_MSG2; //"调拨单明细报表";

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("worktype", "WareHouseType"));//类型
        var srcdata = GetGridSourceDataByList(list, flagList);

        return srcdata;
    }
	
    public void GridBind()
    {
        IGenericRepository<V_ALLOCATE_DReport> con = new GenericRepository<V_ALLOCATE_DReport>(context);
        var caseList = from p in con.Get()
                       orderby p.cticketcode descending
                       where 1 == 1
                       select p;
        if (txtCCREATEOWNERCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccreateownercode) && x.ccreateownercode.Contains(txtCCREATEOWNERCODE.Text.Trim()));
        if (txtCAUDITPERSON.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cauditperson) && x.cauditperson.Contains(txtCAUDITPERSON.Text.Trim()));
        }
        if (ddlCSTATUS.SelectedValue != "")
            caseList = caseList.Where(x => x.cstatus.ToString().Equals(ddlCSTATUS.SelectedValue));
        if (txtCTICKETCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtCTICKETCODE.Text.Trim()));

        if (txtDCREATETIMEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMEFrom.Text.Trim(), x.dcreatetime) >= 0);
        if (txtDCREATETIMETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMETo.Text.Trim(), x.dcreatetime) <= 0);
        if (txtDAUDITTIMEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.daudittime != null && SqlFunctions.DateDiff("dd", txtDAUDITTIMEFrom.Text.Trim(), x.daudittime) >= 0);
        if (txtDAUDITTIMETo.Text != string.Empty)
            caseList = caseList.Where(x => x.daudittime != null && SqlFunctions.DateDiff("dd", txtDAUDITTIMETo.Text.Trim(), x.daudittime) <= 0);
        if (txtDINDATEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.dindate != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.dindate) >= 0);
        if (txtDINDATETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dindate != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.dindate) <= 0);
        
        if (txtERP.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtERP.Text.Trim()));
        if (txtLH.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtLH.Text.Trim()));
        if (ddlworktype.SelectedValue != "")
            caseList = caseList.Where(x => x.ALLOTYPE.ToString().Equals(ddlworktype.SelectedValue));
        if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
        {
            caseList = caseList.Where(x => x.cspecifications.ToString().Contains(txtcspec.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(txtRANK_FINAL.Text.Trim()))
        {
            caseList = caseList.Where(x =>x.cinvcode.Contains("-") && x.cinvcode.EndsWith("-" + txtRANK_FINAL.Text.Trim()));
        }
        else
        {
            caseList = caseList.Where(x => x.cinvcode.Contains("-") && x.cinvcode.Substring(x.cinvcode.Length - 2, 1).Equals("-"));
        }
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
        int Count = caseList.Count();
        AspNetPager1.PageSize = this.PageSize;

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("worktype", "WareHouseType"));//作业方式
        flagList.Add(new Tuple<string, string>("TypeCode", "INTYPE"));//类型

        var srcdata = GetGridSourceDataByList(data, flagList);

        #region  显示批/序號(RANK) 代码 2020-09-16 李舟蕾
        DataTable dt = srcdata;
        dt.Columns.Add("RANK_FINAL", Type.GetType("System.String")); //批/序號(RANK)

        DataTable newdt = dt.Clone();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string Cinvcode = Convert.ToString(dt.Rows[i]["cinvcode"]);
            string[] array = Cinvcode.Split(new char[] { '-' });

            if (array.Count() > 1 && array[array.Count() - 1].ToString().Length == 1)
            {
                int s1 = Cinvcode.LastIndexOf("-");
                for (int k = 0; k < Cinvcode.Length - s1; k++)
                {
                    dt.Rows[i]["cinvcode"] = Cinvcode.Remove(Cinvcode.Length - k - 1);
                }
                if (array[array.Count() - 1].ToString() == "_")
                {
                    dt.Rows[i]["RANK_FINAL"] = "";
                }
                else
                {
                    dt.Rows[i]["RANK_FINAL"] = array[array.Count() - 1].ToString().ToUpper();
                }

                newdt.ImportRow(dt.Rows[i]);
            }
        }
        #endregion

        this.grdALLOCATE.DataSource = newdt;
        this.grdALLOCATE.DataBind();
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

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        Help.DropDownListDataBind(GetParametersByFlagType("AllocateType"), this.ddlworktype, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        var allocates = GetParametersByFlagType("ALLOCATE");
        var ss = allocates.Where(x => x.FLAG_ID == "7").FirstOrDefault();//已交付
        if (ss != null) {
            allocates.Remove(ss);
        }
        Help.DropDownListDataBind(allocates, this.ddlCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");  
    }

    #endregion


    protected void grdALLOCATE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

}

