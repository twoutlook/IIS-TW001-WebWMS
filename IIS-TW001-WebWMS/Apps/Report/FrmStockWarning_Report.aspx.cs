using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using DreamTek.ASRS.Business.Report;


using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Collections;

// 库存预警报表
// 创建时间 2013-7-24 10:57:48

public partial class Apps_FrmStockWarning_Report : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Search();
            this.GridBind();
        }
        btnExcel.GetExportToExcelSource3 += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler3(ToExcel);
    }

    public IList ToExcel()
    {
        List<WARN_TEMP_STOCK_INFO> list = new List<WARN_TEMP_STOCK_INFO>();
        list = GetQueryList().ToList();
        btnExcel.ExcelName = Resources.Lang.FrmStockWarning_Report_MSG2;//"库存有效期预警报表";
        return list;
    }
   /// <summary>
   /// 导出Excel
   /// </summary>
   /// <returns></returns>
    protected DataTable grdNavigatorStockWarn_GetExportToExcelSource()
    {
        //ReportDataSource listQuery = new ReportDataSource();
        //DataTable dtSource = listQuery.FrmStockWare_Report(txtWareCode.Text.Trim().ToUpper(), txtCpositioncode.Text.Trim().ToUpper(), txtCinvcode.Text.Trim().ToUpper(), txtShelflife.Text.Trim(), txtDaysPast.Text.Trim(), false, -1, -1);
        //return null;

        //ReportDataSource listQuery = new ReportDataSource();
        DataTable listQuery = new DataTable();
        DataTable dtSource = FrmStockWare_Report(txtWareCode.Text.Trim().ToUpper(), txtCpositioncode.Text.Trim().ToUpper(), txtCinvcode.Text.Trim().ToUpper(), txtShelflife.Text.Trim(), txtDaysPast.Text.Trim(), false, -1, -1);
        return dtSource;
    }

    /// 查询库存有效期报表
    /// <summary>
    /// 查询库存有效期报表
    /// </summary>
    /// <param name="cware">仓库</param>
    /// <param name="cposition">储位</param>
    /// <param name="cinvCode">料号</param>
    /// <param name="shelflife">保质期</param>
    /// <param name="dayspast">过期天数</param>
    /// <param name="isGetRowCount">是否是获取总记录数</param>
    /// <param name="pageIndex">页码,从0开始，任何小于0的参数值，返回所有记录数</param>
    /// <param name="pageSize">每页记录数</param>
    /// <returns></returns>
    public DataTable FrmStockWare_Report(string cware, string cposition, string cinvCode, string shelflife, string dayspast, bool isGetRowCount, int pageIndex, int pageSize)
    {
        string strSQL = @"select * from warn_temp_stock_info fo where fo.createuser='WMS'";
        if (isGetRowCount)
        {
            strSQL = @"select count(1) from warn_temp_stock_info fo where fo.createuser='WMS'";
        }
        List<IDbDataParameter> paras = new List<IDbDataParameter>();
        IDbDataParameter para;
        string strOperator;

        //仓库
        if (cware.IsNullOrEmpty() == false)
        {
            strSQL += " and fo.cwarecode like  '" + cware + "%'";
        }
        //储位
        if (cposition.IsNullOrEmpty() == false)
        {
            strSQL += " and fo.cpositioncode like '" + cposition + "%'";
        }
        //料号
        if (cinvCode.IsNullOrEmpty() == false)
        {
            strSQL += " and fo.cinvcode like  '%" + cinvCode + "%'";
        }
        //保质期
        if (shelflife.IsNullOrEmpty() == false)
        {
            strSQL += "  and fo.cshelflife='" + shelflife + "'";
        }
        //过期天数
        if (dayspast.IsNullOrEmpty() == false)
        {
            strSQL += "  and fo.dayspast ='" + dayspast + "'";
        }


        DataTable tbStock = DBHelp.ExecuteToDataTable(strSQL);
        return tbStock;
    }






    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ReportDataSource query = new ReportDataSource();
        if (query.StockWareCount())
        {
            Search();
            this.GridBind();
        }
        else
        {

            List<string> paraList = new List<string>();
            paraList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
            string Result = DBHelp.ExecuteProcReturnValue("Proc_Stock_WarningMail", paraList, "@P_return_Value");

            if (Result == "0")
            {
                Search();
                this.GridBind();
            }

            //string return_value = "";
            //Proc_Stock_WarningMail proc = new Proc_Stock_WarningMail();
            //proc.P_UserNo = "WMS";
            //proc.Execute();
            //return_value = proc.P_return_Value;
            //if (return_value.Equals("0"))
            //{
            //    Search();
            //    this.GridBind();
            //}

        }
       
    }

    /// <summary>
    /// 查询方法
    /// </summary>
    public void Search()
    {
       
        this.CurrendIndex = 1;
    }

    /// <summary>
    /// 数据绑定
    /// </summary>
    public void GridBind()
    {

        Bind("");
    }


    public IQueryable<WARN_TEMP_STOCK_INFO> GetQueryList()
    {
        IGenericRepository<WARN_TEMP_STOCK_INFO> conn = new GenericRepository<WARN_TEMP_STOCK_INFO>(db);
        var caseList = from p in conn.Get()
                       orderby p.datecode ascending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtWareCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwarecode) && x.cwarecode.Contains(txtWareCode.Text.Trim()));
            }
            if (txtCpositioncode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCpositioncode.Text.Trim()));
            }
            if (txtCinvcode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCpositioncode.Text.Trim()));
            }
            decimal d = 0;
            if (txtShelflife.Text != string.Empty && txtShelflife.Text.IsDecimal())
            {
                caseList = caseList.Where(x => x.cshelflife != null && x.cshelflife.Value == Convert.ToDecimal(txtShelflife.Text.Trim()));
            }
            if (txtDaysPast.Text != string.Empty)
            {
                caseList = caseList.Where(x => x.dayspast != null && x.dayspast.Value == Convert.ToDecimal(txtDaysPast.Text.Trim()));
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

        grdStockWarn.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdStockWarn.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }


    protected void grdStockWarn_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    protected void grdStockWarn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorStockWarn.IsDbPager)
        //{
        //    grdNavigatorStockWarn.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdStockWarn.PageIndex = e.NewPageIndex;
        //}
    }
    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdStockWarn.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdStockWarn.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return this.grdStockWarn.DataKeys[rowIndex].Values[0].ToString();
    }
    protected void grdStockWarn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);

            //SN明细 锁定详情
            HyperLink HL_SN_Info = (HyperLink)e.Row.FindControl("HL_SN_Info");
            HL_SN_Info.NavigateUrl = "#";
            decimal SNNum = Convert.ToDecimal(this.grdStockWarn.DataKeys[e.Row.RowIndex].Values[1].ToString());
            if (SNNum > 0)
            {
                this.OpenFloatWin(HL_SN_Info, BuildRequestPageURL("FrmStockWarning_SNList.aspx", SYSOperation.Modify, strKeyID), "<%$ Resources:Lang, FrmStockWarning_Report_MSG9 %>", "Warning_SN", 640, 480);
            }
        }

    }
}