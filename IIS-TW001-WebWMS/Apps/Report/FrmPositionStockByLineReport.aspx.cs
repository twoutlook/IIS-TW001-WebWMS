using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Collections;

public partial class Apps_Report_FrmPositionStockByLineReport : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();           
        }
        btnExcel.GetExportToExcelSource3 += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler3(ToExcel);
    }
    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";
    }
   

    #endregion
    #region 事件
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }
    /// 查詢按鈕
    /// <summary>
    /// 查詢按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {       
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        Bind("");

    }
    #endregion
    #region 方法
    public IList ToExcel()
    {
        List<view_position_stock_line> list = new List<view_position_stock_line>();
        list = GetQueryList().ToList();
        return list;
    }
    public IQueryable<view_position_stock_line> GetQueryList()
    {
        IGenericRepository<view_position_stock_line> conn = new GenericRepository<view_position_stock_line>(db);
        var caseList = from p in conn.Get()
                       orderby p.line ascending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtLine.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.line) && x.line.Contains(txtLine.Text.Trim()));
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
        grdPositionRatio.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdPositionRatio.DataBind();
    }
    protected void grdPositionRatio_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.grdPositionRatio.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //未使用储位
            HyperLink hlToNOSTOCKCOUNT_Info = (HyperLink)e.Row.FindControl("hlToNOSTOCKCOUNT_Info");
            hlToNOSTOCKCOUNT_Info.NavigateUrl = "#";
            decimal IoccupyQty = Convert.ToDecimal(this.grdPositionRatio.DataKeys[e.Row.RowIndex].Values[1].ToString());
            if (IoccupyQty > 0)
            {
                this.OpenFloatWin(hlToNOSTOCKCOUNT_Info, BuildRequestPageURL("FrmPositionStockLine_DReport.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.CommonB_LineNotUsedCpoctionLIST, "NOSTOCKLIST", 800, 480);//"线别未使用儲位列表信息"
            }
        }
    }
    #endregion
}