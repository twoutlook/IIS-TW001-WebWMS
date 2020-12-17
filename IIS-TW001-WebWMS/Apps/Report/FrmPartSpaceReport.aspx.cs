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
/// 描述: 物料庫存多儲位分佈報表
/// 作者: --CQ
/// 创建于:2013-10-28 10:57:47
/// </summary>
/*
Roger
2013/7/2 8:44:29
20130702084429
解决狂点按钮重复提交问题
*/
public partial class FrmPartSpaceReport : WMSBasePage
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
        List<REPORT_PART_MORE_SPACE> list = new List<REPORT_PART_MORE_SPACE>();
        list = GetQueryList().ToList();
        btnExcel.ExcelName = Resources.Lang.FrmPartSpaceReport_Title1;// "物料庫存多儲位分佈報表";
        return list;
    }
	
    public void GridBind()
    {
        //ReportDataSource ds = new ReportDataSource();
        //DataTable dtSource = ds.GetPartSpace(txtwarehouse.Text.Trim().ToUpper(), txtCpositioncode.Text.Trim().ToUpper(), txtCinvcode.Text.Trim().ToUpper(), false, this.grdNavigatorPartSpace.CurrentPageIndex, this.grdPartSpace.PageSize);
        //this.grdPartSpace.DataSource = dtSource;
        //this.grdPartSpace.DataBind();
    }


    public IQueryable<REPORT_PART_MORE_SPACE> GetQueryList()
    {
        IGenericRepository<REPORT_PART_MORE_SPACE> conn = new GenericRepository<REPORT_PART_MORE_SPACE>(db);
        var caseList = from p in conn.Get()
                       orderby p.createtime descending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtwarehouse.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.warehouse) && x.warehouse.Contains(txtwarehouse.Text.Trim()));
            }
            if (txtCpositioncode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCpositioncode.Text.Trim()));
            }
            if (txtCinvcode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));
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

        grdPartSpace.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdPartSpace.DataBind();
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
        //this.grdPartSpace.DataKeyNames = new string[] { "ID" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
      //  this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + PageBase.BuildRequestPageURL("FrmAllocateSpecial_Edit.aspx", SysOperation.New, "") + "','新建退料调拨单','ALLOCATE');return false;";
        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmALLOCATEEdit.aspx", SysOperation.New,""),800,600);

    }

    #endregion

    protected DataTable grdNavigatorPartSpace_GetExportToExcelSource()
    {
        //ReportDataSource ds = new ReportDataSource();
        //DataTable dtSource = ds.GetPartSpace(txtwarehouse.Text.Trim().ToUpper(),txtCpositioncode.Text.Trim().ToUpper(), txtCinvcode.Text.Trim().ToUpper(), false, -1, -1);
       
        
        return new DataTable();
    }

    protected void grdPartSpace_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorPartSpace.IsDbPager)
        //{
        //    grdNavigatorPartSpace.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdPartSpace.PageIndex = e.NewPageIndex;
        //}
    }
    protected void grdPartSpace_PageIndexChanged(object sender, EventArgs e)
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
        //grdNavigatorPartSpace.CurrentPageIndex = 0;
        //ReportDataSource ds = new ReportDataSource();
        //DataTable dtRowCount = ds.GetPartSpace(txtwarehouse.Text.Trim().ToUpper(),
        //                                       txtCpositioncode.Text.Trim().ToUpper(), txtCinvcode.Text.Trim().ToUpper(),
        //                                       true, 0, 0);
        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdNavigatorPartSpace.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdNavigatorPartSpace.RowCount = 0;
        //}

        //this.GridBind();
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        Bind("");
    }
  

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdPartSpace.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdPartSpace.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdPartSpace_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
         
        }
    }

    protected void dsGrdPartSpace_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdPartSpace_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //if (e.ReturnValue is DataTable)
        //{
        //    if (grdNavigatorPartSpace.IsDbPager == false)
        //        this.grdNavigatorPartSpace.RowCount = ((DataTable)e.ReturnValue).Rows.Count;
        //}

    }
  
}

