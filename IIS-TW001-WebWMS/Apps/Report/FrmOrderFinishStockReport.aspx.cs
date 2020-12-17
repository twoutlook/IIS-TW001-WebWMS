using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_Report_FrmOrderFinishStockReport : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitPage();
        }
        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
    }

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grd_OrderStock.DataKeyNames = new string[] { "ID" };

    }

    public DataTable ToExcel()
    {
        string sql = " select v.*,(v.AllQty - v.LockQty) as LessQty from dbo.V_OrderFinish_Stock v where 1=1 ";
        if (!string.IsNullOrEmpty(txtOrderNo.Text.Trim()))
        {
            sql+= string.Format(" and v.OrderNo like '%{0}%' ", txtOrderNo.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
        {
            sql+=string.Format(" and v.CinvCode like '%{0}%' ", txtCinvcode.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtLineId.Text.Trim()))
        {
            sql += string.Format(" and  cast(v.OrderLine as varchar(50)) = '{0}' ", txtLineId.Text.Trim());
        }
        sql += " order by  v.LastUpdateTime desc ";
        DataTable tb = SqlDBHelp.ExecuteToDataTable(sql);
        return tb;
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridDataBind();
    }

    /// 查询按钮
    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        GridDataBind();
    }

    public void GridDataBind()
    {
        using (var modContext = this.context)
        {
            var queryList = from p in modContext.V_OrderFinish_Stock
                            select p;
            if (!string.IsNullOrEmpty(txtOrderNo.Text.Trim()))
            {
                queryList = queryList.Where(x => x.OrderNo.Contains(txtOrderNo.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
            {
                queryList = queryList.Where(x => x.CinvCode.Contains(txtCinvcode.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtLineId.Text.Trim())) {
                queryList = queryList.Where(x => x.OrderLine.ToString() == txtLineId.Text.Trim());
            }
            queryList = queryList.OrderByDescending(x => x.LastUpdateTime);

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;

            List<V_OrderFinish_Stock> data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            var source = from p in data
                         select new
                         {
                             p.Id,
                             p.OrderNo,
                             p.CustomOrderNo,
                             p.CustomId,
                             p.CustomName,
                             p.CinvCode,
                             p.CinvName,
                             p.OrderLine,
                             p.cwarehousecode,
                             p.cwarehouse,
                             p.cpositioncode,
                             p.cposition,
                             p.AllQty,
                             p.LockQty,
                             LessQty = (p.AllQty-p.LockQty)
                         };

            grd_OrderStock.DataSource = source.ToList();
            grd_OrderStock.DataBind();
        }
    }


    private string GetKeyIDS(int rowIndex)
    {
        return this.grd_OrderStock.DataKeys[rowIndex].Values[0].ToString();
    }

    protected void grd_OrderStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            //HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            //linkModify.NavigateUrl = "#";
            //this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmOutOrderEdit.aspx", SYSOperation.Modify, strKeyID), "订单", "OUTORDER");
        }
    }

}