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
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Collections;

/// <summary>
/// 描述: 区域储位使用率报表
/// 作者: --CQ
/// 创建于:2013-10-28 10:57:47
/// </summary>

public partial class FrmPositionStockRatioReport : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        btnExcel.GetExportToExcelSource3 += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler3(ToExcel);
    }


    #region IPageGrid 成员
    public IList ToExcel()
    {
        List<view_position_stock_ratio> list = new List<view_position_stock_ratio>();
      
        list = GetQueryList().ToList();
        btnExcel.ExcelName = Resources.Lang.FrmPositionStockRatioReport_title1;// "區域儲位使用率報表";
        return list;
    }
    public void GridBind()
    {
        //ReportDataSource ds = new ReportDataSource();
        //DataTable dtSource = ds.GetRatio(txtArea.Text.Trim(),  false, this.grdNavigatorPositionRatio.CurrentPageIndex, this.grdPositionRatio.PageSize);
        //this.grdPositionRatio.DataSource = dtSource;
        //this.grdPositionRatio.DataBind();
        
    }


    public IQueryable<view_position_stock_ratio> GetQueryList()
    {
        IGenericRepository<view_position_stock_ratio> conn = new GenericRepository<view_position_stock_ratio>(db);
        var caseList = from p in conn.Get()
                       orderby p.area_name descending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtArea.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.area_name) && x.area_name.Contains(txtArea.Text.Trim()));
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
        //this.grdPositionRatio.DataKeyNames = new string[] { "ID,NOSTOCKCOUNT" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
      //  this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + PageBase.BuildRequestPageURL("FrmAllocateSpecial_Edit.aspx", SysOperation.New, "") + "','新建退料调拨单','ALLOCATE');return false;";
        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmALLOCATEEdit.aspx", SysOperation.New,""),800,600);

    }

    #endregion

    //protected DataTable grdNavigatorPositionRatio_GetExportToExcelSource()
    //{
    //    //ReportDataSource ds = new ReportDataSource();
    //    //DataTable dtSource = ds.GetRatio(txtArea.Text.Trim(), false, -1, -1);
       
        
    //    return null;
    //}

    protected void grdPositionRatio_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorPositionRatio.IsDbPager)
        //{
        //    grdNavigatorPositionRatio.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdPositionRatio.PageIndex = e.NewPageIndex;
        //}
    }
    protected void grdPositionRatio_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    /// 查詢按鈕
    /// <summary>
    /// 查詢按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //重新设置GridNavigator的RowCount
        //grdNavigatorPositionRatio.CurrentPageIndex = 0;
        //ReportDataSource ds = new ReportDataSource();
        //DataTable dtRowCount = ds.GetRatio(txtArea.Text.Trim(), true, 0, 0);
        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdNavigatorPositionRatio.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdNavigatorPositionRatio.RowCount = 0;
        //}

        //this.GridBind();

        CurrendIndex = 1;
        Bind("");

    }
  

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdPositionRatio.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdPositionRatio.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdPositionRatio_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string strKeyID = this.grdPositionRatio.DataKeys[e.Row.RowIndex].Values[0].ToString();
        //    //未使用储位
        //    HyperLink hlToNOSTOCKCOUNT_Info = (HyperLink)e.Row.FindControl("hlToNOSTOCKCOUNT_Info");
        //    hlToNOSTOCKCOUNT_Info.NavigateUrl = "#";
        //    decimal IoccupyQty = Convert.ToDecimal(this.grdPositionRatio.DataKeys[e.Row.RowIndex].Values[1].ToString());
        //    if (IoccupyQty > 0)
        //    {
        //        this.OpenFloatWin(hlToNOSTOCKCOUNT_Info, BuildRequestPageURL("FrmPositionStockRatio_DReport.aspx", SYSOperation.Modify, strKeyID), "區域未使用儲位列表信息", "NOSTOCKLIST", 800, 480);
        //    }
        //}
    }

    protected void dsGrdPositionRatio_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdPositionRatio_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //if (e.ReturnValue is DataTable)
        //{
        //    if (grdNavigatorPositionRatio.IsDbPager == false)
        //        this.grdNavigatorPositionRatio.RowCount = ((DataTable)e.ReturnValue).Rows.Count;
        //}

    }
  
}

