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
using System.Data;
using DreamTek.ASRS.Business.Import;

public partial class FrmASRS_Report : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
        }
        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
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
        if (CheckData())
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
            Bind("");
        }

    }
    #endregion
    #region 方法
    public bool CheckData()
    {
        bool isOK = true;
        if (string.IsNullOrEmpty(txtDate.Text.Trim()))
        {
            Alert(Resources.Lang.FrmASRS_Report_MSG1 + "！");//日期项不能为空
            txtDate.Focus();
            isOK =  false;
        }
        return isOK;
    }
    public DataTable ToExcel()
    {
        int pageCount = 0;        
        DataTable dt = new DataTable();
        ReportQuery query = new ReportQuery();      
        dt = query.GetLineWorkReportList(txtDate.Text.Trim(), txtLine.Text.Trim(), -1, PageSize, out pageCount);
        return dt;
    }   

    public void Bind(string sortStr)
    {
        DataTable dt = new DataTable();
        ReportQuery query = new ReportQuery();
        int pageCount = 0;
        this.PageSize = grdASRS.PageSize; 
        dt = query.GetLineWorkReportList(txtDate.Text.Trim(), txtLine.Text.Trim(),CurrendIndex, PageSize, out pageCount);

       
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;
        grdASRS.DataSource = dt;
        grdASRS.DataBind();
       
    }
    protected void grdASRS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         if (e.Row.RowType == DataControlRowType.Footer)
        {
            ReportQuery query = new ReportQuery();
            DataTable dt = new DataTable();
            int pageCount = 0;
            dt = query.GetLineWorkReportList(txtDate.Text.Trim(), txtLine.Text.Trim(), -1, PageSize, out pageCount);

            e.Row.Cells[3].Text = Resources.Lang.FrmSTOCK_CurrentQueryList_HeJi + "：";//合计
             e.Row.Cells[3].Style.Add("text-align", "right");
             e.Row.Cells[4].Text = dt.AsEnumerable().Select(x => x.Field<int>("RKQty")).Sum().ToString();
             e.Row.Cells[5].Text = dt.AsEnumerable().Select(x => x.Field<int>("CKQty")).Sum().ToString();
             e.Row.Cells[6].Text = dt.AsEnumerable().Select(x => x.Field<int>("FKQty")).Sum().ToString();
             e.Row.Cells[7].Text = dt.AsEnumerable().Select(x => x.Field<int>("SUMQty")).Sum().ToString();   
        }
    }
    #endregion
}