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

public partial class FrmPositionStockLine_DReport : WMSBasePage
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();

            ShowData();
        }
        btnExcel.GetExportToExcelSource3 += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler3(ToExcel);
    }

    #region IPage 成员

    /// <summary>
    /// 初始化页面。主要做一下动作
    ///1、DropDownList,ListBox数据装载,
    ///2、新增按钮、删除的按钮的Java脚本注册等
    ///一般在PageLoad 事件调用，
    ///并且IsPostBack = false时
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('NOSTOCKLIST');return false;";
        // this.grdINASN.DataKeyNames = new string[] { "IDS" };
    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        Line = Request.QueryString["ID"];
        Search();
    }
    public IList ToExcel()
    {
        List<view_position_nostocklist_line> list = new List<view_position_nostocklist_line>();

        IGenericRepository<view_position_nostocklist_line> con = new GenericRepository<view_position_nostocklist_line>(context);
        var caseList = from p in con.Get()
                       where p.line == Line
                       orderby p.cpositioncode descending
                       select p;


        list = caseList.ToList();
        return list;
    }
    /// <summary>
    /// 區域ID
    /// </summary>
    public string Line
    {
        get { return ViewState["Line"].ToString(); }
        set { ViewState["Line"] = value; }
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<view_position_nostocklist_line> con = new GenericRepository<view_position_nostocklist_line>(context);
        var caseList = from p in con.Get()
                       where p.line == Line
                       orderby p.cpositioncode descending
                       select p;

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
        this.grdNoStock.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        this.grdNoStock.DataBind();
    }

    #endregion
    /// 查詢方法
    /// <summary>
    /// 查詢方法
    /// </summary>
    public void Search()
    {
        this.GridBind();
    }
    protected void grdNoStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }  
}