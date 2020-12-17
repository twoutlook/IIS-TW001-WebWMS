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

public partial class Apps_Report_FrmCommandQuery_Report : WMSBasePage
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

    /// <summary>
    /// 当前页数 -- AGV
    /// </summary>
    public int CurrendIndex3
    {
        get
        {
            if (ViewState["CurrendIndex3"] == null)
            {
                ViewState["CurrendIndex3"] = 1;
            }
            return (int)ViewState["CurrendIndex3"];
        }
        set
        {
            ViewState["CurrendIndex3"] = value;
        }
    }

    /// <summary>
    /// 当前页数 -- RGV
    /// </summary>
    public int CurrendIndex4
    {
        get
        {
            if (ViewState["CurrendIndex4"] == null)
            {
                ViewState["CurrendIndex4"] = 1;
            }
            return (int)ViewState["CurrendIndex4"];
        }
        set
        {
            ViewState["CurrendIndex4"] = value;
        }
    }

    /// <summary>
    /// 当前页数 -- RGV
    /// </summary>
    public int CurrendIndex5
    {
        get
        {
            if (ViewState["CurrendIndex5"] == null)
            {
                ViewState["CurrendIndex5"] = 1;
            }
            return (int)ViewState["CurrendIndex5"];
        }
        set
        {
            ViewState["CurrendIndex5"] = value;
        }
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            GridBind_MST();
            //是否配置了AGV
            var isNeedAGV = this.GetConFig("140116");
            if (isNeedAGV == "1")
            {
                GridBind_AGV();
            }
            else {
                this.tabAGV.Visible = false;
            }
            //是否配置了RGV
            var isNeedRGV = this.GetConFig("140111");
            if (isNeedRGV == "1")
            {
                GridBind_RGV();
            }
            else {
                this.tabRGV.Visible = false;
            }
            //是否配置有台车
            var isNeedCar = this.GetConFig("140112");
            if (isNeedCar == "1")
            {
                GridBind_CAR();
            }
            else
            {
                this.tabCar.Visible = false;
            }         
        }
    }

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        var commandStatusList = GetParametersByFlagType("CommandStatus");
        Help.DropDownListDataBind(commandStatusList, ddlCmdSts, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(commandStatusList, ddlstatusAGV, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(commandStatusList, ddlRGVCstatus, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(commandStatusList, ddlCARCstatus, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");

        var commandTypeList = GetParametersByFlagType("CommandType");
        Help.DropDownListDataBind(commandTypeList, ddlRemark, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(commandTypeList, ddlRGVtype, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(commandTypeList, ddlCARtype, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        ddlCARtype.Items.RemoveAt(3);
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
        IGenericRepository<V_CMD_MST> con = new GenericRepository<V_CMD_MST>(context);
        var caseList = from p in con.Get()
                       orderby p.ActTime descending,p.TrnDate descending
                       where 1 == 1
                       select p;
        if (txtPackageNo.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.PACKAGENO) && x.PACKAGENO.Contains(txtPackageNo.Text.Trim()));
        if (txtCticketCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CTICKETCODE) && x.CTICKETCODE.Contains(txtCticketCode.Text.Trim()));
        if (txtLineID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.LineId) && x.LineId.ToString().Equals(txtLineID.Text.Trim()));
        if (txtstnNO.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.StnNo) && x.StnNo.ToString().Equals(txtstnNO.Text.Trim()));
        if (ddlRemark.SelectedValue != "")
            caseList = caseList.Where(x => x.CmdMode.ToString().Equals(ddlRemark.SelectedValue));
        if (ddlCmdSts.SelectedValue != "")
            caseList = caseList.Where(x => x.CmdSts.ToString().Equals(ddlCmdSts.SelectedValue));
        if (!string.IsNullOrEmpty(txtTrnDateFrom.Text.Trim()))
            caseList = caseList.Where(x => x.TrnDate != null && SqlFunctions.DateDiff("dd", txtTrnDateFrom.Text.Trim(), x.TrnDate) >= 0);
        if (txtTrnDateTo.Text != string.Empty)
            caseList = caseList.Where(x => x.TrnDate != null && SqlFunctions.DateDiff("dd", txtTrnDateTo.Text.Trim(), x.TrnDate) <= 0);
        if (!string.IsNullOrEmpty(txtEndTimeFrom.Text.Trim()))
            caseList = caseList.Where(x => x.EndTime != null && SqlFunctions.DateDiff("dd", txtEndTimeFrom.Text.Trim(), x.EndTime) >= 0);
        if (txtEndTimeTo.Text != string.Empty)
            caseList = caseList.Where(x => x.EndTime != null && SqlFunctions.DateDiff("dd", txtEndTimeTo.Text.Trim(), x.EndTime) <= 0);
        //20200601添加cmdsno的查询条件
        if (txtCmdSNo.Text != string.Empty)
        {
            caseList = caseList.Where(x => x.CmdSno.ToString().Equals(txtCmdSNo.Text.Trim()));
        }
        //20200601添加返回状态的查询条件
        if (txtResult.Text != string.Empty)
        {
            caseList = caseList.Where(x => x.Result.ToString().Contains(txtResult.Text.Trim()));
        }        
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
        var listResult = GetPageSize(caseList, PageSize, CurrendIndex2).ToList();
        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("Cmdsts", "CmdStsName", "CommandStatus"));//狀態
        flagList.Add(new Tuple<string, string, string>("CmdMode", "ModeName", "CommandType"));//命令类型       
        var source = GetGridDataByAddColumns(listResult, flagList);
        this.grdCMD.DataSource = source;
        //this.grdCMD.DataSource = GetPageSize(caseList, PageSize, CurrendIndex2).ToList();
        this.grdCMD.DataBind();
    }
    #endregion

    #region AGV相关
    protected void btnSearchAGV_Click(object sender, EventArgs e)
    {
        CurrendIndex3 = 1;
        AspNetPager2.CurrentPageIndex = 1;
        this.GridBind_AGV();
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {

        this.CurrendIndex3 = AspNetPager2.CurrentPageIndex;//索引同步
        GridBind_AGV();
    }

    public void GridBind_AGV()
    {
        IGenericRepository<V_CMD_AgvTask> con = new GenericRepository<V_CMD_AgvTask>(context);
        var caseList = from p in con.Get()
                       orderby p.Createtime descending
                       where 1 == 1
                       select p;
        if (txtAGVPk.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.data) && x.data.Contains(txtAGVPk.Text.Trim()));
        if (txtReqCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.reqcode) && x.reqcode.Contains(txtReqCode.Text.Trim()));
        if (TxtTaskCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.taskcode) && x.taskcode.ToString().Equals(TxtTaskCode.Text.Trim()));
        if (TxtTaskType.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.tasktyp) && x.tasktyp.ToString().Equals(TxtTaskType.Text.Trim()));
        if (txtValue.Text != "")
            caseList = caseList.Where(x => x.value.ToString().Contains(txtValue.Text));

        if (ddlstatusAGV.SelectedValue != "")
            caseList = caseList.Where(x => x.status.ToString().Equals(ddlstatusAGV.SelectedValue));
        //if (!string.IsNullOrEmpty(txtreqDatefROM.Text.Trim()))
        //    caseList = caseList.Where(x => x.reqtime != null && SqlFunctions.DateDiff("dd", txtreqDatefROM.Text.Trim(), x.reqtime) >= 0);
        //if (txtreqDateTo.Text != string.Empty)
        //    caseList = caseList.Where(x => x.reqtime != null && SqlFunctions.DateDiff("dd", txtreqDateTo.Text.Trim(), x.reqtime) <= 0);
        if (!string.IsNullOrEmpty(txtAGVCdateFrom.Text.Trim()))
            caseList = caseList.Where(x => x.Createtime != null && SqlFunctions.DateDiff("dd", txtAGVCdateFrom.Text.Trim(), x.Createtime) >= 0);
        if (txtAGVCdateTo.Text != string.Empty)
            caseList = caseList.Where(x => x.Createtime != null && SqlFunctions.DateDiff("dd", txtAGVCdateTo.Text.Trim(), x.Createtime) <= 0);

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager2.RecordCount = caseList.Count();
            AspNetPager2.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager2.RecordCount = 0;
            AspNetPager2.PageSize = this.PageSize;
        }
        AspNetPager2.PageSize = this.PageSize;
        this.grdAGV.DataSource = GetPageSize(caseList, PageSize, CurrendIndex3).ToList();
        this.grdAGV.DataBind();
    }
    #endregion

    #region RGV相关
    protected void btnSearchRGV_Click(object sender, EventArgs e)
    {
        CurrendIndex4 = 1;
        AspNetPager3.CurrentPageIndex = 1;
        this.GridBind_RGV();
    }

    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {

        this.CurrendIndex4 = AspNetPager3.CurrentPageIndex;//索引同步
        GridBind_RGV();
    }

    public void GridBind_RGV()
    {
        IGenericRepository<V_CMD_MST_RGV> con = new GenericRepository<V_CMD_MST_RGV>(context);
        var caseList = from p in con.Get()
                       orderby p.TrnDate descending
                       where 1 == 1
                       select p;
        if (txtRGVPK.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.PACKAGENO) && x.PACKAGENO.Contains(txtRGVPK.Text.Trim()));
        if (this.txtRGVCticketCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CTICKETCODE) && x.CTICKETCODE.Contains(txtRGVCticketCode.Text.Trim()));
        if (txtRGVlINEID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.LineId) && x.LineId.ToString().Equals(txtRGVlINEID.Text.Trim()));
        if (txtRGVSiteID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.StnNo) && x.StnNo.ToString().Equals(txtRGVSiteID.Text.Trim()));
        if (ddlRGVtype.SelectedValue != "")
            caseList = caseList.Where(x => x.CmdMode.ToString().Equals(ddlRGVtype.SelectedValue));

        if (ddlRGVCstatus.SelectedValue != "")
            caseList = caseList.Where(x => x.Cmdsts.ToString().Equals(ddlRGVCstatus.SelectedValue));
        if (!string.IsNullOrEmpty(ddlRGV_trnDateFrom.Text.Trim()))
            caseList = caseList.Where(x => x.TrnDate != null && SqlFunctions.DateDiff("dd", ddlRGV_trnDateFrom.Text.Trim(), x.TrnDate) >= 0);
        if (ddlRGV_trnDateTo.Text != string.Empty)
            caseList = caseList.Where(x => x.TrnDate != null && SqlFunctions.DateDiff("dd", ddlRGV_trnDateTo.Text.Trim(), x.TrnDate) <= 0);
        if (!string.IsNullOrEmpty(txtRGV_EndTimeFrom.Text.Trim()))
            caseList = caseList.Where(x => x.EndTime != null && SqlFunctions.DateDiff("dd", txtRGV_EndTimeFrom.Text.Trim(), x.EndTime) >= 0);
        if (txtRGV_EndTimeTo.Text != string.Empty)
            caseList = caseList.Where(x => x.EndTime != null && SqlFunctions.DateDiff("dd", txtRGV_EndTimeTo.Text.Trim(), x.EndTime) <= 0);
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager3.RecordCount = caseList.Count();
            AspNetPager3.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager3.RecordCount = 0;
            AspNetPager3.PageSize = this.PageSize;
        }
        AspNetPager3.PageSize = this.PageSize;


        var listResult = GetPageSize(caseList, PageSize, CurrendIndex4).ToList();
        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("Cmdsts", "CmdStsName", "CommandStatus"));//狀態
        flagList.Add(new Tuple<string, string, string>("CmdMode", "ModeName", "CommandType"));//命令类型       
        var source = GetGridDataByAddColumns(listResult, flagList);

        this.grdRGV.DataSource = source;
        //this.grdRGV.DataSource = GetPageSize(caseList, PageSize, CurrendIndex4).ToList();
        this.grdRGV.DataBind();
    }

    #endregion

    #region 台车相关

    protected void btnSearchCAR_Click(object sender, EventArgs e)
    {
        CurrendIndex5 = 1;
        AspNetPager4.CurrentPageIndex = 1;
        this.GridBind_CAR();
    }

    protected void AspNetPager4_PageChanged(object sender, EventArgs e)
    {

        this.CurrendIndex5 = AspNetPager4.CurrentPageIndex;//索引同步
        GridBind_CAR();
    }

    public void GridBind_CAR()
    {
        IGenericRepository<V_CMD_MST_RGV> con = new GenericRepository<V_CMD_MST_RGV>(context);
        var caseList = from p in con.Get()
                       orderby p.TrnDate descending
                       where 1 == 1
                       select p;
        if (txtCARPK.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.PACKAGENO) && x.PACKAGENO.Contains(txtCARPK.Text.Trim()));
        if (this.txtCARCticketCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CTICKETCODE) && x.CTICKETCODE.Contains(txtCARCticketCode.Text.Trim()));
        if (txtCARlINEID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.LineId) && x.LineId.ToString().Equals(txtCARlINEID.Text.Trim()));
        if (txtCARSiteID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.StnNo) && x.StnNo.ToString().Equals(txtCARSiteID.Text.Trim()));
        if (ddlCARtype.SelectedValue != "")
            caseList = caseList.Where(x => x.CmdMode.ToString().Equals(ddlCARtype.SelectedValue));

        if (ddlCARCstatus.SelectedValue != "")
            caseList = caseList.Where(x => x.Cmdsts.ToString().Equals(ddlCARCstatus.SelectedValue));
        if (!string.IsNullOrEmpty(ddlCAR_trnDateFrom.Text.Trim()))
            caseList = caseList.Where(x => x.TrnDate != null && SqlFunctions.DateDiff("dd", ddlCAR_trnDateFrom.Text.Trim(), x.TrnDate) >= 0);
        if (ddlCAR_trnDateTo.Text != string.Empty)
            caseList = caseList.Where(x => x.TrnDate != null && SqlFunctions.DateDiff("dd", ddlCAR_trnDateTo.Text.Trim(), x.TrnDate) <= 0);
        if (!string.IsNullOrEmpty(txtCAR_EndTimeFrom.Text.Trim()))
            caseList = caseList.Where(x => x.EndTime != null && SqlFunctions.DateDiff("dd", txtCAR_EndTimeFrom.Text.Trim(), x.EndTime) >= 0);
        if (txtCAR_EndTimeTo.Text != string.Empty)
            caseList = caseList.Where(x => x.EndTime != null && SqlFunctions.DateDiff("dd", txtCAR_EndTimeTo.Text.Trim(), x.EndTime) <= 0);

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager4.RecordCount = caseList.Count();
            AspNetPager4.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager4.RecordCount = 0;
            AspNetPager4.PageSize = this.PageSize;
        }
        AspNetPager4.PageSize = this.PageSize;

        var listResult = GetPageSize(caseList, PageSize, CurrendIndex5).ToList();
        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("Cmdsts", "CmdStsName", "CommandStatus"));//狀態
        flagList.Add(new Tuple<string, string, string>("CmdMode", "ModeName", "CommandType"));//命令类型       
        var source = GetGridDataByAddColumns(listResult, flagList);

        this.grdCAR.DataSource = source;
        this.grdCAR.DataBind();
    }
    #endregion











}