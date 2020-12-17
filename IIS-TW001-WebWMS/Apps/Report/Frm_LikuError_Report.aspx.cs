using DreamTek.ASRS.DAL;
using DreamTek.WMS.DAL.Model.Report;
using DreamTek.WMS.Repository.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_Report_Frm_LikuError_Report : WMSBasePage
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
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";

        List<BASE_CRANECONFIG> lkList = db.BASE_CRANECONFIG.Where(x => x.PLCType == "LK" & x.FLAG == "0").OrderBy(x=>x.CRANEID).ToList();
        Help.DropDownListDataBind(lkList, this.drpLiKuSite, Resources.Lang.Common_ALL, "CRANENAME", "CRANEID", "");
    }

    #endregion

    #region IPageGrid 成员

    public DataTable ToExcel()
    {
        int total = 0;
        btnExcel.ExcelName = "地上盘异常报表";// "进出明细报表";
        IEnumerable<CMD_MST_Error> curCmdErrorList = new ReportRepository().GetCMDMSTErrorList(drpLiKuSite.SelectedValue, txtCreateDateFrom.Text.Trim(), txtCreateDateTo.Text.Trim(), txtErrorCode.Text.Trim(), txtErrorMsg.Text.Trim(), PageSize, CurrendIndex, out total);
        return WMSBasePage.ListToDataTable(curCmdErrorList.ToList());
    }

    public void GridBind()
    {
        int total = 0;
        IEnumerable<CMD_MST_Error> curCmdErrorList = new ReportRepository().GetCMDMSTErrorList(drpLiKuSite.SelectedValue, txtCreateDateFrom.Text.Trim(), txtCreateDateTo.Text.Trim(), txtErrorCode.Text.Trim(), txtErrorMsg.Text.Trim(), PageSize, CurrendIndex, out total);
        AspNetPager1.RecordCount = total;
        AspNetPager1.PageSize = this.PageSize;
        this.grdLiKuError.DataSource = curCmdErrorList;
        this.grdLiKuError.DataBind();
    }
    #endregion

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdLiKuError.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdLiKuError.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

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

    protected void grdLiKuError_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
}