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
using DreamTek.WMS.DAL.Model;
using DreamTek.WMS.Repository.Stock;

public partial class Apps_Report_FrmTemporaryAreaCinv_Report : WMSBasePage
{
    StockRepository storkquery = new StockRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();   
        }
        btnExcel.GetExportToExcelSource3 += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler3(ToExcel);
    }

    public IList ToExcel()
    {
        int total=0;
        List<V_TemporaryAreaCinv_Report> list = new List<V_TemporaryAreaCinv_Report>();
        list = GetQueryList(-1,out total).ToList();
        //btnExcel.ExcelName = Resources.Lang.FrmStockWarning_Report_MSG2;//"库存有效期预警报表";
        return list;
    }
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";       
    }

    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {        
        Search();    
    }

    /// <summary>
    /// 查询方法
    /// </summary>
    public void Search()
    {
        this.CurrendIndex = 1;
        this.GridBind();
    }
    /// <summary>
    /// 绑定列表信息
    /// </summary>
    public void GridBind()
    {
        int total = 0;
        var caselist = GetQueryList(CurrendIndex,out total);
        AspNetPager1.RecordCount = total;
        AspNetPager1.PageSize = this.PageSize;
        grdStockWarn.DataSource = caselist;
        grdStockWarn.DataBind();
    }
    public IEnumerable<V_TemporaryAreaCinv_Report> GetQueryList(int pageindex,out int total)
    {       
        V_TemporaryAreaCinv_Report whereObject = new V_TemporaryAreaCinv_Report();
        whereObject.cinvcode = txtCinvcode.Text.Trim();
        whereObject.cspecifications = txtcspec.Text.Trim();
        whereObject.WarningDate = txtWarningDate.Text.Trim();
        IEnumerable<V_TemporaryAreaCinv_Report> curStockList = storkquery.GetTempAreaStockWarningReport(whereObject, PageSize, pageindex, out total).ToList();
        return curStockList;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }


    protected void grdStockWarn_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }    
    protected void grdStockWarn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
}