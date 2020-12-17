using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business.Base;
using System.Data.Entity.SqlServer;

public partial class Apps_Report_FrmCmd_Mst_Log : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }

    #region SQL
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        this.GridBind();
    }
    public void GridBind()
    {
        IGenericRepository<V_CMD_MST_LOG> entity = new GenericRepository<V_CMD_MST_LOG>(context);
        var caseList = from p in entity.Get()
                       orderby p.ID ascending
                       where 1 == 1
                       select p;
        if (!string.IsNullOrEmpty(txtCreateTimeFrom.Text.Trim()))
            caseList = caseList.Where(x => x.CreateTime != null && SqlFunctions.DateDiff("dd", txtCreateTimeFrom.Text.Trim(), x.CreateTime) >= 0);
        if (txtCreateTimeTo.Text != string.Empty)
            caseList = caseList.Where(x => x.CreateTime != null && SqlFunctions.DateDiff("dd", txtCreateTimeTo.Text.Trim(), x.CreateTime) <= 0);

        if (txtWmsTaskId.Text != string.Empty)
            caseList = caseList.Where(x =>  x.WmsTaskId.ToString().Equals(txtWmsTaskId.Text.Trim()));
        if (txtPACKAGENO.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.PACKAGENO) && x.PACKAGENO.Contains(txtPACKAGENO.Text.Trim()));
        if (txtCTICKETCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CTICKETCODE) && x.CTICKETCODE.Contains(txtCTICKETCODE.Text.Trim()));
        if (txtLineId.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.LineId) && x.LineId.Contains(txtLineId.Text.Trim()));

        AspNetPager1.RecordCount = caseList.Count();
        AspNetPager1.PageSize = this.PageSize;
        grdBASE_CLIENT.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdBASE_CLIENT.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void grdBASE_CLIENT_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortName = e.SortExpression;
        if (SortedField.Equals(sortName))
        {
            if (SortedAD.Equals(Ascending))
            {
                SortedAD = Descending;//取反
            }
            else
            {
                SortedAD = Ascending;
            }
        }
        else
        {
            SortedField = sortName;
            SortedAD = Ascending;
        }

        GridBind();
    }
    #endregion

}