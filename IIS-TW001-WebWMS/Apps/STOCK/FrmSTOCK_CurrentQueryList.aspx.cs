using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Business.Allocate;
/// <summary>
/// 描述: 期间库存-->FrmSTOCK_DURATIONList 页面后台类文件
/// 作者: 
/// 创建于: 2012-10-17 15:48:44
/// </summary>
public partial class FrmSTOCK_CurrentQueryList : WMSBasePage
{

    public Dictionary<string, string> SelectedCinvcodes
    {
        get { return Session["SelectCinvcodes"] as Dictionary<string, string>; }
        set { Session["SelectCinvcodes"] = value; }
    }

    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack == false)
        {
            this.InitPage();
            //btnSearch_Click(null, null);
            //栈板维护关联查询
            if (this.Operation() == SYSOperation.View)
            {
                this.grdSTOCK_DURATION.Columns[1].Visible = false;
                this.txtCPOSITIONCODE.Text = Request.QueryString["ids"] != null ? Request.QueryString["ids"] : "";
                this.txtCINVCODE.Text = Request.QueryString["ID"] != null ? Request.QueryString["ID"] : "";
                if (Request.QueryString["ids"] != null)
                {
                    this.btnBack.Visible = true;
                    this.btnBack.Attributes["onclick"] = "CloseMySelf('STOCK_Current');return false;";
                }

                this.txtPalletCode.Text = Request["PACKAGENO"] != null ? Request["PACKAGENO"].ToString() : "";
                this.rbtList.SelectedValue = "2";
                btnSearch_Click(null, null);
            }
        }

        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
    }

    public DataTable ToExcel()
    {
        int pageCount = 0;
        AllocateQuery query = new AllocateQuery();
        DataTable dt = query.GetStockCurrentList(txtCWAREHOUSEName.Text.Trim(), txtCPOSITIONCODE.Text.Trim(), txtCINVCODE.Text.Trim(), rbtList.SelectedValue, txtPalletCode.Text.Trim(), txtPackgeNo.Text.Trim(),txtcspec.Text.Trim(), -1, PageSize, out pageCount);
        return dt;
    }

    #region IPageGrid 成员

    private string GetSelectedCinvcodes()
    {
        var cinvcodes = string.Empty;
        if (SelectedCinvcodes != null)
        {
            foreach (var item in SelectedCinvcodes)
            {
                cinvcodes = cinvcodes + "'" + item.Value + "',";
            }
            cinvcodes = cinvcodes.Substring(0, cinvcodes.Length - 1);
        }
        return cinvcodes;
    }

    public void GridBind()
    {
        int pageCount = 0;
        AllocateQuery query = new AllocateQuery();
        DataTable dt = query.GetStockCurrentList(txtCWAREHOUSEName.Text.Trim(), txtCPOSITIONCODE.Text.Trim(), this.txtCINVCODE.Text,this.txtRank_Final.Text, rbtList.SelectedValue,
txtPalletCode.Text.Trim(), txtPackgeNo.Text.Trim(),txtcspec.Text.Trim(),CurrendIndex, PageSize, out pageCount);
        AspNetPager1.CustomInfoHTML = " " + Resources.Lang.WMS_Common_Pager_PageCount + ":<b>" + "</b>";//总页数
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;

        // Note by Qamar 2020-11-15
        // 庫存查詢時不顯示台惟備料儲位
        int tw001 = -1;
        //foreach (DataRow row in dt.Rows)
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            if (row["cpositioncode"].ToString() == "TW001")
                if (row["cposition"].ToString() == "台惟備料儲位")
                    tw001 = i;
        }
        if (tw001 >= 0)
            dt.Rows.RemoveAt(tw001);

        // NOTE by Mark 09/20
        dt = GetGridSourceData_PART_RANK(dt);

        grdSTOCK_DURATION.DataSource = dt;
        grdSTOCK_DURATION.DataBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Session["StockCurrentIndex"] != null && !string.IsNullOrEmpty(Session["StockCurrentIndex"].ToString()))
        {
            CurrendIndex = Convert.ToInt32(Session["StockCurrentIndex"]);
            Session.Remove("StockCurrentIndex");
        }
        else
            CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = CurrendIndex;
        this.GridBind();
    }
    public bool CheckData()
    {
        if (this.txtCPOSITIONCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtCPOSITIONCODE.Text.Trim().Length > 0)
        {
        }

        return true;
    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //this.grdSTOCK_DURATION.DataKeyNames = new string[] { "ID", "IOCCUPYQTY", "LOCKNUM" };
    }

    #endregion

    protected void BtnNew_Click(object sender, EventArgs e)
    {

    }

    private string GetKeyIDS(int rowIndex)
    {
        if (grdSTOCK_DURATION.DataKeys[rowIndex].Values[0] == null)
        {
            return string.Empty;
        }
        return this.grdSTOCK_DURATION.DataKeys[rowIndex].Values[0].ToString();
    }

    protected void grdSTOCK_DURATION_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink link = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            link.NavigateUrl = "#";
            decimal qty = 0;
            decimal.TryParse(e.Row.Cells[e.Row.Cells.Count - 4].Text.Trim(), out qty);
            if (qty > 0 && !string.IsNullOrEmpty(strKeyID))
            {
                this.OpenFloatWinMax(link, BuildRequestPageURL("FrmSTOCK_CURRENT_DETAIL_Edit.aspx", SYSOperation.Modify, strKeyID) + "&StockCurrentIndex=" + CurrendIndex, Resources.Lang.FrmSTOCK_CURRENT_DETAIL_Edit_PageName, "STOCK_CURRENT_DETAIL");//库存明细
            }
            //占用量
            HyperLink hlToIOCCUPYQTY_Info = (HyperLink)e.Row.FindControl("hlToIOCCUPYQTY_Info");
            hlToIOCCUPYQTY_Info.NavigateUrl = "#";
            decimal IoccupyQty = 0;
            decimal.TryParse(this.grdSTOCK_DURATION.DataKeys[e.Row.RowIndex].Values[1].ToString(), out IoccupyQty);
            if (IoccupyQty > 0 && !string.IsNullOrEmpty(strKeyID))
            {
                this.OpenFloatWin(hlToIOCCUPYQTY_Info, BuildRequestPageURL("FrmOutASSITListByStock.aspx", SYSOperation.Modify, strKeyID) + "&StockCurrentIndex=" + CurrendIndex, Resources.Lang.FrmSTOCK_CurrentQueryList_AssitZhanyong, "OutASSITListByStock", 640, 480);//拣货指引占用详情
            }

            //物料名称
            var partName = e.Row.Cells[6].Text;
            if (!string.IsNullOrEmpty(partName) && partName.Length > 20)
            {
                e.Row.Cells[6].Text = partName.Substring(0, 20) + "...";
            }

            //DateCode 锁定详情
            HyperLink HL_DateCode_Info = (HyperLink)e.Row.FindControl("HL_DateCode_Info");
            HL_DateCode_Info.NavigateUrl = "#";
            decimal LockNum = 0;
            decimal.TryParse(this.grdSTOCK_DURATION.DataKeys[e.Row.RowIndex].Values[2].ToString(), out LockNum);
            if (LockNum > 0 && !string.IsNullOrEmpty(strKeyID))
            {
                this.OpenFloatWin(HL_DateCode_Info, BuildRequestPageURL("FrmDateCode_Lock_Detail.aspx", SYSOperation.Modify, strKeyID) + "&StockCurrentIndex=" + CurrendIndex, Resources.Lang.FrmSTOCK_CurrentQueryList_DateCodeDetail, "Stock_Lock", 640, 480);//DateCode锁定详情
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // NOTE by Mark, 10/22,
            // 以多語TW 合計 找到 FrmSTOCK_CurrentQueryList_HeJi
            AllocateQuery query = new AllocateQuery();
            var total = query.GetStockCurrentTotalQty(txtCWAREHOUSEName.Text.Trim(), txtCPOSITIONCODE.Text.Trim(), txtCINVCODE.Text.Trim(), rbtList.SelectedValue, txtPalletCode.Text.Trim(), txtPackgeNo.Text.Trim(),txtcspec.Text.Trim());
            //e.Row.Cells[9].Text = Resources.Lang.FrmSTOCK_CurrentQueryList_HeJi + "：";//合计
            //e.Row.Cells[9].Style.Add("text-align", "right");
            //e.Row.Cells[11].Text = total.ToString("N2");
            e.Row.Cells[10].Text = Resources.Lang.FrmSTOCK_CurrentQueryList_HeJi + "：";//合计
            e.Row.Cells[10].Style.Add("text-align", "right");
            e.Row.Cells[13].Text = total.ToString("N2"); // 12 is not working
        }
    }

    protected void dsGrdSTOCK_DURATION_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

    }  
}