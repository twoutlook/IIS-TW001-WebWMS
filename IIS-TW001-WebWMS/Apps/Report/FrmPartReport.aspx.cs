using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Data;
using DreamTek.ASRS.Business.Stock;

public partial class Apps_Report_FrmPartReport : WMSBasePage
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        ucShowWAREHOUSEDiv.SetORGCode = txtwarehousename .ClientID;
        ucShowWAREHOUSEDiv.SetCompName = txtwarehouse.ClientID;
        ucShowArea.SetORGCode = txtareaname.ClientID;
        ucShowArea.SetCompName = txtarea.ClientID;
        //txtwarehousename  txtareaname
        if (this.IsPostBack == false)
        {         
            if (Request.QueryString["Flag"] != null && !string.IsNullOrEmpty(Request.QueryString["Flag"].ToString()))
            {
                Flag = Request.QueryString["Flag"].ToString();
            }
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
    }

    #region IPage 成员
    public string Flag
    {
        get
        {
            if (ViewState["Flag"] != null)
            {
                return ViewState["Flag"].ToString();
            }
            return "";
        }
        set { ViewState["Flag"] = value; }
    }
    #endregion

    public DataTable ToExcel()
    {
        int pageCount = 0;
        DataTable dt = new DataTable();
        StockQuery query = new StockQuery();
        if (Flag == "0")
        {
            btnExcel.ExcelName = Resources.Lang.FrmPartReport_Content2; //"仓库物料统计报表";
            dt = query.GetPartListByWareHouse(txtwarehouse.Text.Trim(),txtcspec.Text.Trim(), -1, PageSize, out pageCount);
        }
        else if (Flag == "1")
        {
            btnExcel.ExcelName = Resources.Lang.FrmPartReport_Content3; //"区域物料统计报表";
            dt = query.GetPartListByArea(txtarea.Text.Trim(), txtcspec1.Text.Trim(), -1, PageSize, out pageCount);
        } 
        return dt;
    }
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        if (Flag == "0")  //仓库物料统计报表
        {
            lblTitle.Text = Resources.Lang.Common_WareHouse; //"仓库";
            trwarehouse.Visible = true;
            trarea.Visible = false;
            grdPartSpace.Columns[2].Visible = false;
            grdPartSpace.Columns[3].Visible = false;           
        }
        else  //区域物料统计报表
        {
            lblTitle.Text = Resources.Lang.CommonB_Area; //"区域";
            trwarehouse.Visible = false;
            trarea.Visible = true;
            grdPartSpace.Columns[0].Visible = false;
            grdPartSpace.Columns[1].Visible = false;           
        }

        #region Disable and ReadOnly
        #endregion Disable and ReadOnly

    }
    protected void grdPartSpace_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }
    protected void grdPartSpace_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
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
        GridBind();
    }
  
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    public void GridBind()
    {
        int pageCount = 0;
        DataTable dt = new DataTable();
        StockQuery query = new StockQuery();
        if(Flag =="0")
        {
            dt = query.GetPartListByWareHouse(txtwarehouse.Text.Trim(),txtcspec.Text.Trim(),CurrendIndex, PageSize, out pageCount);
        }
        else if(Flag =="1")
        {
            dt = query.GetPartListByArea(txtarea.Text.Trim(), txtcspec1.Text.Trim(), CurrendIndex, PageSize, out pageCount);
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;
        grdPartSpace.DataSource = dt;
        grdPartSpace.DataBind();     
    }
   
}