using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.Entity.SqlServer;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 描述: 
/// 作者: --
/// 创建于: 2013-6-30 9:54:10
/// </summary>
public partial class FrmStock_CheckDiffSearch : WMSBasePage// PageBase
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            IGenericRepository<STOCK_CHECK_PLAN> con = new GenericRepository<STOCK_CHECK_PLAN>(context);
            var caseList = from p in con.Get()
                           where p.cstatus =="1"
                           select p;
            if (caseList.Count()>0)
            this.txtCheckName.Text = caseList.ToList().FirstOrDefault<STOCK_CHECK_PLAN>().plan_name;
           // this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        btnExcel.GetExportToExcelSource3 += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler3(ToExcel);
    }


    #region IPageGrid 成员
    public IList ToExcel()
    {
        List<V_Stock_CheckDiffSearch> list = new List<V_Stock_CheckDiffSearch>();

        IGenericRepository<V_Stock_CheckDiffSearch> con = new GenericRepository<V_Stock_CheckDiffSearch>(context);
        var caseList = from p in con.Get()
                       orderby p.dcheckdate descending
                       where 1 == 1
                       select p;
        if (txtCheckCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtCheckCode.Text.Trim().ToUpper()));
        if (txtCheckName.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccheckname) && x.ccheckname.Contains(txtCheckName.Text.Trim()));
        if (txtWareNo.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwareid) && x.cwareid.Contains(txtWareNo.Text.Trim().ToUpper()));
        if (dplCHECKTYPE.SelectedValue != "")
            caseList = caseList.Where(x => x.ptype.ToString().Equals(dplCHECKTYPE.SelectedValue));
        if (txtDINDATEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.dcheckdate != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.dcheckdate) >= 0);
        if (txtDINDATETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dcheckdate != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.dcheckdate) <= 0);
        if (txtPositionCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtPositionCode.Text.Trim().ToUpper()));
        if (txtCinvCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvCode.Text.Trim().ToUpper()));
        if (txtOracle.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtOracle.Text.Trim()));

        list = caseList.ToList();
        btnExcel.ExcelName = Resources.Lang.FrmStock_CheckDiffSearch_Title1;//"物理盘点明细报表";
        return list;
    }
    public void GridBind()
    {
        //ReportDataSource listQuery = new ReportDataSource();
        //DataTable dtSource = listQuery.GetCheckDiffSearch(txtCheckCode.Text.Trim().ToUpper(), txtCheckName.Text.Trim(), dplCHECKTYPE.SelectedValue.Trim(), txtWareNo.Text.Trim().ToUpper(), txtPositionCode.Text.Trim().ToUpper(), txtCinvCode.Text.Trim().ToUpper(), txtOracle.Text.Trim(), txtDINDATEFrom.Text.Trim(), txtDINDATETo.Text.Trim(), false, this.grdNavigatorINBILL.CurrentPageIndex, this.grdINBILL.PageSize);
        //this.grdINBILL.DataSource = dtSource;
        //this.grdINBILL.DataBind();

        IGenericRepository<V_Stock_CheckDiffSearch> con = new GenericRepository<V_Stock_CheckDiffSearch>(context);
        var caseList = from p in con.Get()
                       orderby p.dcheckdate descending
                       where 1 == 1
                       select p;
        if (txtCheckCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtCheckCode.Text.Trim().ToUpper()));
        if (txtCheckName.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccheckname) && x.ccheckname.Contains(txtCheckName.Text.Trim()));
        if (txtWareNo.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwareid) && x.cwareid.Contains(txtWareNo.Text.Trim().ToUpper()));
        if (dplCHECKTYPE.SelectedValue != "")
            caseList = caseList.Where(x => x.ptype.ToString().Equals(dplCHECKTYPE.SelectedValue));
        if (txtDINDATEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.dcheckdate != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.dcheckdate) >= 0);
        if (txtDINDATETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dcheckdate != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.dcheckdate) <= 0);
        if (txtPositionCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtPositionCode.Text.Trim().ToUpper()));
        if (txtCinvCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvCode.Text.Trim().ToUpper()));
        if (txtOracle.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtOracle.Text.Trim()));
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

    //public bool CheckData()
    //{

    //    if (this.txtDINDATEFrom.Text.Trim().Length > 0)
    //    {
    //        if (this.txtDINDATEFrom.Text.IsDate() == false)
    //        {
    //            this.Alert("入库日期项不是有效的日期！");
    //            this.SetFocus(txtDINDATEFrom);
    //            return false;
    //        }
    //    }
    //    if (this.txtDINDATETo.Text.Trim() == "")
    //    {
    //        this.Alert("到项不允许空！");
    //        this.SetFocus(txtDINDATETo);
    //        return false;
    //    }
    //    if (this.txtDINDATETo.Text.Trim().Length > 0)
    //    {
    //        if (this.txtDINDATETo.Text.IsDate() == false)
    //        {
    //            this.Alert("到项不是有效的日期！");
    //            this.SetFocus(txtDINDATETo);
    //            return false;
    //        }
    //    }
      
      
    //    return true;

    //}

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        Help.DropDownListDataBind(GetParametersByFlagType("IsDifference"), this.dplCHECKTYPE, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
    }

    #endregion
  
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorINBILL
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
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
            string strKeyID = this.grdINBILL.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //初盘详情
            HyperLink HL_Chupan = (HyperLink)e.Row.FindControl("HL_Chupan");
            HL_Chupan.NavigateUrl = "#";
            decimal ChupanNum = 0;
            string chupan = this.grdINBILL.DataKeys[e.Row.RowIndex].Values[1] != null?this.grdINBILL.DataKeys[e.Row.RowIndex].Values[1].ToString():string.Empty;
            if (string.IsNullOrEmpty(chupan) == false)
            {
                ChupanNum = Convert.ToDecimal(chupan);
            }
            if (ChupanNum > 0)
            {
                this.OpenFloatWin(HL_Chupan, BuildRequestPageURL("FrmStock_CheckDiffSearch_SN.aspx", SYSOperation.Modify, strKeyID) + "&CheckType=2", Resources.Lang.FrmStock_CheckDiffSearch_MSG1, "Stock_CheckSN", 800, 600);//"初盤SN详情"
            }
            //复盘
            HyperLink HL_Fupan = (HyperLink)e.Row.FindControl("HL_Fupan");
            HL_Fupan.NavigateUrl = "#";
            decimal FupanNum = 0;
            string fupan = this.grdINBILL.DataKeys[e.Row.RowIndex].Values[2] != null?this.grdINBILL.DataKeys[e.Row.RowIndex].Values[2].ToString():string.Empty;
            if (string.IsNullOrEmpty(fupan) == false)
            {
                FupanNum = Convert.ToDecimal(fupan);
            }
           
            if (FupanNum > 0)
            {
                this.OpenFloatWin(HL_Fupan, BuildRequestPageURL("FrmStock_CheckDiffSearch_SN.aspx", SYSOperation.Modify, strKeyID) + "&CheckType=3", Resources.Lang.FrmStock_CheckDiffSearch_MSG2, "Stock_CheckSN", 800, 600);//"复盘SN详情"
            }
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

