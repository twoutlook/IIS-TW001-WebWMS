using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Collections;

/// <summary>
/// 描述: 入库管理-->FrmINBILLList 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:57:39
/// </summary>
public partial class FrmWARNINGSTOCK_Report : WMSBasePage
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }

        btnExcel.GetExportToExcelSource3 += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler3(ToExcel);
    }


    #region IPageGrid 成员
    public IList ToExcel()
    {
        List<V_WARNINGSTOCK_REPORT> list = new List<V_WARNINGSTOCK_REPORT>();

        IGenericRepository<V_WARNINGSTOCK_REPORT> con = new GenericRepository<V_WARNINGSTOCK_REPORT>(context);
        var caseList = from p in con.Get()
                       orderby p.iqty ascending
                       where 1 == 1
                       select p;
        if (TextBox3.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpartnumber) && x.cpartnumber.Contains(TextBox3.Text.Trim()));
        if (TextBox4.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpartname) && x.cpartname.Contains(TextBox4.Text.Trim()));
        }
        if (ddlAlarm.SelectedValue != "")
            caseList = caseList.Where(x => x.isalarm.ToString().Equals(ddlAlarm.SelectedValue));

        list = caseList.ToList();
        btnExcel.ExcelName = Resources.Lang.FrmWARNINGSTOCK_Report_Msg1; //"库存预警报表 ";
        return list;
    }

    public void GridBind()
    {
        //STOCK_FrmWARNINGSTOCKLisQuery listQuery = new STOCK_FrmWARNINGSTOCKLisQuery();
        //DataTable dtSource = listQuery.GetList(TextBox3.Text, TextBox4.Text, false, this.grdNavigatorINBILL.CurrentPageIndex, this.grdINBILL.PageSize, ddlAlarm.SelectedValue);
        //this.grdINBILL.DataSource = dtSource;
        //this.grdINBILL.DataBind();

        //if (dtSource != null && dtSource.Rows.Count >= 1)
        //{
        //    this.grdNavigatorINBILL.RowCount = dtSource.Rows.Count;
        //}
        //else
        //{
        //    this.grdNavigatorINBILL.RowCount = 0;
        //}

        IGenericRepository<V_WARNINGSTOCK_REPORT> con = new GenericRepository<V_WARNINGSTOCK_REPORT>(context);
        var caseList = from p in con.Get()
                       orderby p.iqty ascending
                       where 1 == 1
                       select p;
        if (TextBox3.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpartnumber) && x.cpartnumber.Contains(TextBox3.Text.Trim()));
        if (TextBox4.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpartname) && x.cpartname.Contains(TextBox4.Text.Trim()));
        }
        if (ddlAlarm.SelectedValue != "")
            caseList = caseList.Where(x => x.isalarm.ToString().Equals(ddlAlarm.SelectedValue));
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
        AspNetPager1.PageSize = this.PageSize;
        this.grdINBILL.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        this.grdINBILL.DataBind();
    }


    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //this.grdINBILL.DataKeyNames = new string[]{"ID"};
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly


    }

    #endregion

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

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdINBILL.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdINBILL.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdINBILL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            //HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count-1].Controls[0];
            //linkModify.NavigateUrl = "#";
            //this.OpenFloatWinMax(linkModify,BuildRequestPageURL("FrmINBILLEdit.aspx",SysOperation.Modify, strKeyID),"入库单","INBILL");
        }
    }

    protected void dsGrdINBILL_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

}

