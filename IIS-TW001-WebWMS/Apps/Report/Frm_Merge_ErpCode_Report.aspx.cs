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



public partial class Frm_Merge_ErpCode_Report : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack == false)
        {
            this.InitPage();
           // this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }


    #region IPageGrid 成员

    public void GridBind()
    {
        //ReportDataSource ds = new ReportDataSource();
        RPDataSource ds = new RPDataSource();
        DataTable dtSource = ds.GetMergeErpCode(txtErpCode.Text.Trim(), false, this.CurrendIndex, this.grdErpCode.PageSize);
        //this.grdErpCode.DataSource = dtSource;
        //this.grdErpCode.DataBind();
    }

    public bool CheckData()
    {
        if (txtErpCode.Text.Trim() == "")
        {
            //查询工单号不能为空
            Alert(Resources.Lang.Frm_Merge_ErpCode_Report_MSG1+ "！");
            this.SetFocus(txtErpCode);
            return false;
        }
        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";
        
    }

    #endregion

    /// 导出
    /// <summary>
    /// 导出
    /// </summary>
    /// <returns></returns>
    protected DataTable grdNavigatorErpCode_GetExportToExcelSource()
    {
        //ReportDataSource ds = new ReportDataSource();
        RPDataSource ds = new RPDataSource();
        DataTable dtSource = ds.GetMergeErpCode(txtErpCode.Text.Trim(), false, -1, -1);
        return dtSource;
    }

    protected void grdErpCode_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorErpCode.IsDbPager)
        //{
        //    grdNavigatorErpCode.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdErpCode.PageIndex = e.NewPageIndex;
        //}
    }

    protected void grdErpCode_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //if (!CheckData())
        //{
        //    return;
        //}
        //grdNavigatorErpCode.CurrentPageIndex = 0;
        //ReportDataSource ds = new ReportDataSource();
        //DataTable dtRowCount = ds.GetMergeErpCode(txtErpCode.Text.Trim(), true, 0, 0);

        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdNavigatorErpCode.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdNavigatorErpCode.RowCount = 0;
        //}
        this.CurrendIndex = 1;
        this.GridBind();
    }


    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdErpCode.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdErpCode.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdErpCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }

    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        //Bind("");
    }

}

