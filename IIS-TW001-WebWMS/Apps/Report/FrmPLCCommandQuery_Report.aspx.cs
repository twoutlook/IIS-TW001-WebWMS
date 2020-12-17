using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.Entity.SqlServer;
using DreamTek.WMS.DAL.Model.Report;
using DreamTek.WMS.Repository.Report;

public partial class Apps_Report_FrmPLCCommandQuery_Report : WMSBasePage
{
    DBContext context = new DBContext();
    #region gridview索引
    /// <summary>
    /// 当前页数 -- 立库
    /// </summary>
    public int CurrendIndex2
    {
        get
        {
            if (ViewState["CurrendIndex2"] == null)
            {
                ViewState["CurrendIndex2"] = 1;
            }
            return (int)ViewState["CurrendIndex2"];
        }
        set
        {
            ViewState["CurrendIndex2"] = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            GridBind_MST();           
        }
    }

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        var commandTypeList = GetParametersByFlagType("CommandType_PLC");
        Help.DropDownListDataBind(commandTypeList, ddlCmdType, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
       
    }

    #region 立库相关
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex2 = 1;
        AspNetPager1.CurrentPageIndex = 1;
        this.GridBind_MST();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex2 = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind_MST();
    }

    public void GridBind_MST()
    {
        int total = 0;
        V_PLC_CMD_MST whereObject = new V_PLC_CMD_MST();
        whereObject.topositionCode = txtPosition.Text.ToString();
        whereObject.positionCode = txtPosition.Text.ToString();
        whereObject.lineID = txtLineID.Text.Trim();
        whereObject.siteNo = txtstnNO.Text.Trim();
        whereObject.cmdMode = ddlCmdType.SelectedValue.Trim();
        whereObject.starttime = txtTrnDateFrom.Text.Trim();
        whereObject.endtime = txtTrnDateTo.Text.Trim();
        whereObject.cmdSno = txtCmdSNo.Text.Trim();
        whereObject.result = txtResult.Text.Trim();
        IEnumerable<V_PLC_CMD_MST> curPLCCmdList = new ReportRepository().GetPLCCmdList(whereObject, PageSize, CurrendIndex2, out total);
        AspNetPager1.RecordCount = total;
        AspNetPager1.PageSize = this.PageSize;
        grdCMD.DataSource = curPLCCmdList;
        grdCMD.DataBind();    
    }
    #endregion
}