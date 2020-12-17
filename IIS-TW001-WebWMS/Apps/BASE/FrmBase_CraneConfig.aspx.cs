using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;

public partial class BASE_FrmBASE_CraneConfig : WMSBasePage
{
    DBContext context = new DBContext();

    #region gridview索引
    /// <summary>
    /// 当前页数 -- 扫描器
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
    /// 当前页数 -- RGV
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
    /// 当前页数 -- AGV
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
    /// 当前页数 -- AGV站点
    /// </summary>
    public int CurrendIndex6
    {
        get
        {
            if (ViewState["CurrendIndex6"] == null)
            {
                ViewState["CurrendIndex6"] = 1;
            }
            return (int)ViewState["CurrendIndex6"];
        }
        set
        {
            ViewState["CurrendIndex6"] = value;
        }
    }

    /// <summary>
    /// 当前页数 -- 升降机
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

    /// <summary>
    /// 当前页数 -- 台车
    /// </summary>
    public int CurrendIndex7
    {
        get
        {
            if (ViewState["CurrendIndex7"] == null)
            {
                ViewState["CurrendIndex7"] = 1;
            }
            return (int)ViewState["CurrendIndex7"];
        }
        set
        {
            ViewState["CurrendIndex7"] = value;
        }
    }

    /// <summary>
    /// 当前页数 -- 台车
    /// </summary>
    public int CurrendIndex8
    {
        get
        {
            if (ViewState["CurrendIndex8"] == null)
            {
                ViewState["CurrendIndex8"] = 1;
            }
            return (int)ViewState["CurrendIndex8"];
        }
        set
        {
            ViewState["CurrendIndex8"] = value;
        }
    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            GridBind();

            GridBind_Scan();

            //是否配置了RGV
            var isNeedRGV = this.GetConFig("140111");
            if (isNeedRGV == "1")
            {
                GridBind_RGV();
            }
            else {
                tabRGV.Visible = false;
            }
            //是否配置了AGV
            var isNeedAGV = this.GetConFig("140116");
            if (isNeedAGV == "1")
            {
                GridBind_AGV();
                GridBind_AGVSITE();
            }
            else {
                tabAGV.Visible = false;
            }
            //是否配置了SJJ
            var isNeedSJJ = this.GetConFig("140117");
            if (isNeedSJJ == "1")
            {
                GridBind_SJJ();
            }
            else {
                tabSJJ.Visible = false;
            }          
            //是否配置有台车
            var isNeedCar = this.GetConFig("140112");
            if (isNeedCar == "1")
            {  
                GridBind_CAR();
            }
            else {
                this.tabCar.Visible = false;
            }
            //是否配置了输送线
            var isNeedSSX = this.GetConFig("140119");
            if (isNeedSSX == "1")
            {
                GridBind_SSX();
            }
            else {
                this.tabSSX.Visible = false;
            }
        }
    }

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdBASE_Crane.DataKeyNames = new string[] { "ID","FLAG" };
        this.grdBASE_CraneDetailScan.DataKeyNames = new string[] { "ID", "FLAG" };
        this.grdRGV.DataKeyNames = new string[] { "ID", "FLAG" };
        this.grdAGV.DataKeyNames = new string[] { "ID", "FLAG" };
        this.grdSJJ.DataKeyNames = new string[] { "ID", "FLAG" };
        this.grdAGVSite.DataKeyNames = new string[] { "ID", "FLAG" };
        this.grdCAR.DataKeyNames = new string[] { "ID", "FLAG" };
        this.grdSSX.DataKeyNames = new string[] { "ID", "FLAG" };

        //本页面打开新增窗口        
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBase_CraneConfigEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmBase_CraneConfig_Msg03+ "','BASE_Crane',800,600);return false;";//新建立库线别
        this.btnSacnAdd.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBase_CraneConfigEdit_D_C.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmBase_CraneConfig_Msg04 + "','BASE_CraneDetialScan',800,600);return false;";//新建固定扫描器
        this.btnSJJAdd.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBaseSJJ.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmBase_CraneConfig_Msg05 + "','BASE_SJJ',800,600);return false;";//新建提升机
        this.btnAGVAdd.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBase_AGV.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmBase_CraneConfig_Msg06 + "','BASE_AGV',800,600);return false;";//新建AGV
        this.btnRGVAdd.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBase_RGV.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmBase_CraneConfig_Msg07 + "','BASE_RGV',800,600);return false;";//新建RGV
        this.btnNewAGVS.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBase_AGV_D.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmBase_CraneConfig_Msg08 + "','BASE_AGV_D',800,600);return false;";//新建RGV
        this.btnCARAdd.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBase_CAR.aspx", SYSOperation.New, "") + "','" + Resources.Lang.WMS_Common_Element_NewTaiChe + "','BASE_CAR',800,600);return false;";//新建台车
        this.btnSSXAdd.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBase_SSX.aspx", SYSOperation.New, "") + "','" + Resources.Lang.WMS_Common_Element_NewShuSongXian + "','BASE_SSX',800,600);return false;";//新建输送线

        Help.DropDownListDataBind(GetParametersByFlagType("SITETYPE"), this.ddlAGVSITETYPE, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(GetParametersByFlagType("AGVSITE"), this.ddlISOCCUPY, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");

        var useStatusList = GetParametersByFlagType("BASE_CLIENT");
        Help.DropDownListDataBind(useStatusList, ddlCraneSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(useStatusList, ddlSTATUS_RGV, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(useStatusList, ddlSTATUS_AGV, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(useStatusList, ddlCSTATUS_AS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(useStatusList, ddlSTATUS_SJJ, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(useStatusList, ddlSTATUS_CAR, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(useStatusList, ddlSTATUS_SSX, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
    }

    #endregion

    #region 线别管理
    public void GridBind()
    {
        IGenericRepository<BASE_CRANECONFIG> vcon = new GenericRepository<BASE_CRANECONFIG>(context);
        var caseList = from p in vcon.Get()
                       orderby p.CRANEID ascending
                       where 1 == 1
                       select p;
        if (!string.IsNullOrEmpty(txtCraneID.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANEID) && x.CRANEID.Contains(txtCraneID.Text.Trim()));

        if (!string.IsNullOrEmpty(txtCraneNAME.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANENAME) && x.CRANENAME.Contains(txtCraneNAME.Text.Trim()));

        if (!string.IsNullOrEmpty(txtCraneIP.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANEIP) && x.CRANEIP.Contains(txtCraneIP.Text.Trim()));

        if (!string.IsNullOrEmpty(txtGroupID.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.GROUPID) && x.GROUPID.Contains(txtGroupID.Text.Trim()));

        if (!string.IsNullOrEmpty(txtSiteCount.Text.Trim()))
            caseList = caseList.Where(x => x.SITECOUNT!=null && x.SITECOUNT.ToString() == txtSiteCount.Text.Trim());
        if (ddlCraneSTATUS.SelectedValue != "")
        {
            caseList = caseList.Where(x => x.FLAG.ToString().Equals(ddlCraneSTATUS.SelectedValue));
        }
        caseList = caseList.Where(x => x.PLCType == "LK");
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("FLAG", "FLAGName", "BASE_CLIENT"));//是否保税

        var srcdata = GetGridDataByAddColumns(data, flagList);

        grdBASE_Crane.DataSource = srcdata;
        grdBASE_Crane.DataBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
        }
        this.GridBind();
        IsFirstPage = true;//恢复默认值
    }

    protected void grdBASE_Crane_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.grdBASE_Crane.DataKeys[e.Row.RowIndex].Values[0].ToString();//this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";

            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("frmBase_CraneConfigEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBase_CraneConfig_Msg09, "BASE_Crane");//立库线别管理
        }
    }

    protected void dsGrdBASE_Crane_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    /// <summary>
    /// 立库线别停用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDiscontinue_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_CRANECONFIG> con = new GenericRepository<BASE_CRANECONFIG>(context);
        try
        {
            int number = 0;
            for (int i = 0; i < grdBASE_Crane.Rows.Count; i++)
            {
                if (this.grdBASE_Crane.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_Crane.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                        number++;
                }
            }
            if (number == 0)
            {
                throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg01);//请选择需要停用的项！
            }
            for (int i = 0; i < this.grdBASE_Crane.Rows.Count; i++)
            {
                if (this.grdBASE_Crane.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_Crane.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string status = this.grdBASE_Crane.DataKeys[i].Values[1].ToString();
                        if (status == "0")//if (this.grdBASE_Crane.Rows[i].Cells[7].Text.Equals("使用中"))
                        {
                            string id = this.grdBASE_Crane.DataKeys[i].Values[0].ToString();
                            var caseList = from p in con.Get()
                                           where p.ID == id
                                           select p;
                            BASE_CRANECONFIG entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG>();
                            entity.DISCONTINUEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                            entity.DISCONTINUETIME = DateTime.Now;
                            entity.FLAG = "1";
                            con.Update(entity);
                            con.Save();
                        }
                        else
                        {
                            throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg10);//只有状态为[使用中]才能停用.
                        }
                    }
                }
            }
            this.GridBind();
            msg = Resources.Lang.FrmBase_CraneConfig_Msg11; //停用成功！
        }
        catch (Exception Ex)
        {

            msg += Resources.Lang.FrmBase_CraneConfig_Msg12 + "[" + Ex.Message + "]"; //停用失败!
        }
        this.Alert(msg);
    }
    #endregion

    //private string GetKeyIDS(int rowIndex)
    //{
    //    string strKeyId = "";
    //    for (int i = 0; i < this.grdBASE_Crane.DataKeyNames.Length; i++)
    //    {
    //        strKeyId += this.grdBASE_Crane.DataKeys[rowIndex].Values[i].ToString() + ",";
    //    }
    //    strKeyId = strKeyId.TrimEnd(',');
    //    return strKeyId;
    //}

    #region Scan 扫描器

    public void GridBind_Scan()
    {
        IGenericRepository<BASE_CRANECONFIG_DETIAL_SCAN> vcon = new GenericRepository<BASE_CRANECONFIG_DETIAL_SCAN>(context);
        var caseList = from p in vcon.Get()
                       orderby p.scanid ascending
                       where 1 == 1 
                       select p;

        //caseList = caseList.Where(x => x.ids == SiteIDS && x.siteid == SiteIDstr);       

        if (!string.IsNullOrEmpty(txtSacnID.Text.Trim()))
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.scanid) && x.scanid.Equals(txtSacnID.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(txtSacnName.Text.Trim()))
        {
           caseList= caseList.Where(x => !string.IsNullOrEmpty(x.scanname) && x.scanname.Contains(txtSacnName.Text.Trim()));
        }
        //for (int i = 0; i < caseList.ToList().Count; i++)
        //{
        //    switch (caseList.ToList()[i].flag.ToString())
        //    {
        //        case "0": caseList.ToList()[i].flag = Resources.Lang.FrmBase_CraneConfig_Msg13; break;//使用中
        //        case "1": caseList.ToList()[i].flag = Resources.Lang.FrmBase_CraneConfig_Msg14; break;//停用
        //        default: caseList.ToList()[i].flag = Resources.Lang.Common_ExceptionStatus; break; //异常状态
        //    }
        //}
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager2.RecordCount = caseList.Count();
            AspNetPager2.PageSize = this.PageSize;
        }
        AspNetPager2.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

        var data = GetPageSize(caseList, PageSize, CurrendIndex2).ToList();

        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("FLAG", "FLAGName", "BASE_CLIENT"));//是否保税

        var srcdata = GetGridDataByAddColumns(data, flagList);

        grdBASE_CraneDetailScan.DataSource = srcdata;
        grdBASE_CraneDetailScan.DataBind();

    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex2 = AspNetPager2.CurrentPageIndex;//索引同步
        GridBind_Scan();
    }

    protected void dsgrdBASE_CraneDetailScan_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void grdBASE_CraneDetailScan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.grdBASE_CraneDetailScan.DataKeys[e.Row.RowIndex].Values[0].ToString();
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("frmBase_CraneConfigEdit_D_C.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBase_CraneConfig_Msg15, "BASE_CraneDetailScan");//固定扫描器管理
        }

    }


    protected void grdBASE_CraneDetailScan_PageIndexChanged(object sender, EventArgs e)
    {
        //if(grdNavigatorALLOCATE_D.IsDbPager)
        {
            this.GridBind_Scan();
        }
    }

    protected void BtnScanSearch_Click(object sender, EventArgs e)
    {
        this.CurrendIndex2 = 1;
        AspNetPager2.CurrentPageIndex = 1;
        GridBind_Scan();
    }

    /// <summary>
    /// 扫描器停用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDiscontinue_Scane_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_CRANECONFIG_DETIAL_SCAN> con = new GenericRepository<BASE_CRANECONFIG_DETIAL_SCAN>(context);
        try
        {
            int number = 0;
            for (int i = 0; i < grdBASE_CraneDetailScan.Rows.Count; i++)
            {
                if (this.grdBASE_CraneDetailScan.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CraneDetailScan.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                        number++;
                }
            }
            if (number == 0)
            {
                throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg01);//请选择需要停用的项！
            }
            for (int i = 0; i < this.grdBASE_CraneDetailScan.Rows.Count; i++)
            {
                if (this.grdBASE_CraneDetailScan.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CraneDetailScan.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string status = this.grdBASE_CraneDetailScan.DataKeys[i].Values[1].ToString();
                        if (status == "0")//使用中
                        {
                            string id = this.grdBASE_CraneDetailScan.DataKeys[i].Values[0].ToString();
                            var caseList = from p in con.Get()
                                           where p.id == id
                                           select p;
                            BASE_CRANECONFIG_DETIAL_SCAN entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG_DETIAL_SCAN>();
                            entity.DISCONTINUEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                            entity.DISCONTINUETIME = DateTime.Now;
                            entity.flag = "1";
                            con.Update(entity);
                            con.Save();
                        }
                        else
                        {
                            throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg10);//只有状态为[使用中]才能停用.
                        }
                    }
                }
            }
            this.GridBind_Scan();
            msg = Resources.Lang.FrmBase_CraneConfig_Msg11; //停用成功！
        }
        catch (Exception Ex)
        {

            msg += Resources.Lang.FrmBase_CraneConfig_Msg12 + "[" + Ex.Message + "]"; //停用失败!
        }
        this.Alert(msg);
    }

    #endregion

    #region RGV
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchRGV_Click(object sender, EventArgs e)
    {
        CurrendIndex3 = 1;
        AspNetPager3.CurrentPageIndex = 1;
        this.GridBind_RGV();
    }

    public void GridBind_RGV()
    {
        IGenericRepository<BASE_CRANECONFIG> vcon = new GenericRepository<BASE_CRANECONFIG>(context);
        var caseList = from p in vcon.Get()
                       orderby p.CRANEID ascending
                       where 1 == 1
                       select p;

        if (!string.IsNullOrEmpty(txtCraneID_RGV.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANEID) && x.CRANEID.Contains(txtCraneID_RGV.Text.Trim()));

        if (!string.IsNullOrEmpty(txtCraneName_RGV.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANENAME) && x.CRANENAME.Contains(txtCraneName_RGV.Text.Trim()));

        if (!string.IsNullOrEmpty(txtCraneIP_RGV.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANEIP) && x.CRANEIP.Contains(txtCraneIP_RGV.Text.Trim()));

        if (!string.IsNullOrEmpty(txtEQUIPMENTNAME_RGV.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.EQUIPMENTNAME) && x.EQUIPMENTNAME.Contains(txtEQUIPMENTNAME_RGV.Text.Trim()));

        //if (!string.IsNullOrEmpty(txtTTYPE_RGV.Text.Trim()))
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.TTYPE) && x.TTYPE.Contains(txtTTYPE_RGV.Text.Trim()));
        if (ddlSTATUS_RGV.SelectedValue != "")
        {
            caseList = caseList.Where(x => x.FLAG.ToString().Equals(ddlSTATUS_RGV.SelectedValue));
        }
        caseList = caseList.Where(x => x.PLCType =="RGV");
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager3.RecordCount = caseList.Count();
            AspNetPager3.PageSize = this.PageSize;
        }
        AspNetPager3.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

        var data = GetPageSize(caseList, PageSize, CurrendIndex3).ToList();

        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("FLAG", "FLAGName", "BASE_CLIENT"));//是否保税

        var srcdata = GetGridDataByAddColumns(data, flagList);

        grdRGV.DataSource = srcdata;
        grdRGV.DataBind();
    }
    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex3 = AspNetPager3.CurrentPageIndex;//索引同步
        GridBind_RGV();
    }

    protected void grdRGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.grdRGV.DataKeys[e.Row.RowIndex].Values[0].ToString();//this.GetKeyIDS_RGV(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";

            //this.OpenFloatWin(linkModify, BuildRequestPageURL("frmBase_RGV.aspx", SYSOperation.Modify, strKeyID), "RGV管理", "BASE_RGV", 800, 600);
            //PopupFloatWinMax
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("frmBase_RGV.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBase_CraneConfig_Msg16, "BASE_RGV"); //RGV管理
        }
    }

    //private string GetKeyIDS_RGV(int rowIndex)
    //{
    //    string strKeyId = "";
    //    for (int i = 0; i < this.grdRGV.DataKeyNames.Length; i++)
    //    {
    //        strKeyId += this.grdRGV.DataKeys[rowIndex].Values[i].ToString() + ",";
    //    }
    //    strKeyId = strKeyId.TrimEnd(',');
    //    return strKeyId;
    //}

    /// <summary>
    /// RGV停用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDiscontinue_RGV_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_CRANECONFIG> con = new GenericRepository<BASE_CRANECONFIG>(context);
        try
        {
            int number = 0;
            for (int i = 0; i < grdRGV.Rows.Count; i++)
            {
                if (this.grdRGV.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdRGV.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                        number++;
                }
            }
            if (number == 0)
            {
                throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg01);//请选择需要停用的项！
            }
            for (int i = 0; i < this.grdRGV.Rows.Count; i++)
            {
                if (this.grdRGV.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdRGV.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string status = this.grdRGV.DataKeys[i].Values[1].ToString();
                        if (status == "0")//if (this.grdRGV.Rows[i].Cells[6].Text.Equals("使用中"))
                        {
                            string id = this.grdRGV.DataKeys[i].Values[0].ToString();
                            var caseList = from p in con.Get()
                                           where p.ID == id
                                           select p;
                            BASE_CRANECONFIG entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG>();
                            entity.DISCONTINUEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                            entity.DISCONTINUETIME = DateTime.Now;
                            entity.FLAG = "1";
                            con.Update(entity);
                            con.Save();
                        }
                        else
                        {
                            throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg10);//只有状态为[使用中]才能停用.
                        }
                    }
                }
            }
            this.GridBind_RGV();
            msg = Resources.Lang.FrmBase_CraneConfig_Msg11; //停用成功！
        }
        catch (Exception Ex)
        {

            msg += Resources.Lang.FrmBase_CraneConfig_Msg12 + "[" + Ex.Message + "]"; //停用失败!
        }
        this.Alert(msg);
    }
    #endregion

    #region AGV
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchAGV_Click(object sender, EventArgs e)
    {
        CurrendIndex4 = 1;
        AspNetPager4.CurrentPageIndex = 1;
        this.GridBind_AGV();
    }

    public void GridBind_AGV()
    {
        IGenericRepository<BASE_CRANECONFIG> vcon = new GenericRepository<BASE_CRANECONFIG>(context);
        var caseList = from p in vcon.Get()
                       orderby p.CRANEID ascending
                       where 1 == 1
                       select p;

        if (!string.IsNullOrEmpty(txtCraneID_AGV.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANEID) && x.CRANEID.Contains(txtCraneID_AGV.Text.Trim()));
        if (!string.IsNullOrEmpty(txtCraneName_AGV.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANENAME) && x.CRANENAME.Contains(txtCraneName_AGV.Text.Trim()));
        //if (!string.IsNullOrEmpty(txtTTYPE_AGV.Text.Trim()))
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.TTYPE) && x.TTYPE.Contains(txtTTYPE_AGV.Text.Trim()));
        if (ddlSUPPLIER_AGV.SelectedValue != "")
        {
            caseList = caseList.Where(x => x.SUPPLIER.ToString().Equals(ddlSUPPLIER_AGV.SelectedValue));
        }
        if (ddlSTATUS_AGV.SelectedValue != "")
        {
            caseList = caseList.Where(x => x.FLAG.ToString().Equals(ddlSTATUS_AGV.SelectedValue));
        }
        caseList = caseList.Where(x => x.PLCType =="AGV");
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager4.RecordCount = caseList.Count();
            AspNetPager4.PageSize = this.PageSize;
        }
        AspNetPager4.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

        var data = GetPageSize(caseList, PageSize, CurrendIndex4).ToList();

        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("FLAG", "FLAGName", "BASE_CLIENT"));//是否保税

        var srcdata = GetGridDataByAddColumns(data, flagList);

        grdAGV.DataSource = srcdata;
        grdAGV.DataBind();
    }

    protected void AspNetPager4_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex4 = AspNetPager4.CurrentPageIndex;//索引同步
        GridBind_AGV();
    }

    protected void grdAGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //switch (e.Row.Cells[4].Text)
            //{
            //    case "0": e.Row.Cells[4].Text = "海康威视";
            //        break;
            //    case "1": e.Row.Cells[4].Text = "其它";
            //        break;
            //    default: e.Row.Cells[4].Text = Resources.Lang.Common_Exception;//"异常"
            //        break;
            //}
            //ddlSUPPLIER_AGV
            try
            {
                e.Row.Cells[4].Text = ddlSUPPLIER_AGV.Items.FindByValue(e.Row.Cells[4].Text.Trim()).Text;
            }
            catch
            {
                e.Row.Cells[4].Text = Resources.Lang.Common_ExceptionStatus; //异常状态
            }
            string strKeyID = this.grdAGV.DataKeys[e.Row.RowIndex].Values[0].ToString();//this.GetKeyIDS_AGV(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";

            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("frmBase_AGV.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBase_CraneConfig_Msg17, "BASE_AGV"); //AGV管理
        }
    }

    //private string GetKeyIDS_AGV(int rowIndex)
    //{
    //    string strKeyId = "";
    //    for (int i = 0; i < this.grdAGV.DataKeyNames.Length; i++)
    //    {
    //        strKeyId += this.grdAGV.DataKeys[rowIndex].Values[i].ToString() + ",";
    //    }
    //    strKeyId = strKeyId.TrimEnd(',');
    //    return strKeyId;
    //}

    /// <summary>
    /// AGV停用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDiscontinue_AGV_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_CRANECONFIG> con = new GenericRepository<BASE_CRANECONFIG>(context);
        try
        {
            int number = 0;
            for (int i = 0; i < grdAGV.Rows.Count; i++)
            {
                if (this.grdAGV.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdAGV.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                        number++;
                }
            }
            if (number == 0)
            {
                throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg01);//请选择需要停用的项！
            }
            for (int i = 0; i < this.grdAGV.Rows.Count; i++)
            {
                if (this.grdAGV.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdAGV.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string status = this.grdAGV.DataKeys[i].Values[1].ToString();
                        if (status == "0")//if (this.grdAGV.Rows[i].Cells[5].Text.Equals("使用中"))
                        {
                            string id = this.grdAGV.DataKeys[i].Values[0].ToString();
                            var caseList = from p in con.Get()
                                           where p.ID == id
                                           select p;
                            BASE_CRANECONFIG entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG>();
                            entity.DISCONTINUEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                            entity.DISCONTINUETIME = DateTime.Now;
                            entity.FLAG = "1";
                            con.Update(entity);
                            con.Save();
                        }
                        else
                        {
                            throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg10);//只有状态为[使用中]才能停用.
                        }
                    }
                }
            }
            this.GridBind_AGV();
            msg = Resources.Lang.FrmBase_CraneConfig_Msg11; //停用成功！
        }
        catch (Exception Ex)
        {

            msg += Resources.Lang.FrmBase_CraneConfig_Msg12 + "[" + Ex.Message + "]"; //停用失败!
        }
        this.Alert(msg);
    }
    #endregion

    #region 升降机
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchSJJ_Click(object sender, EventArgs e)
    {
        CurrendIndex5 = 1;
        AspNetPager5.CurrentPageIndex = 1;
        this.GridBind_SJJ();
    }

    public void GridBind_SJJ()
    {
        IGenericRepository<BASE_CRANECONFIG> vcon = new GenericRepository<BASE_CRANECONFIG>(context);
        var caseList = from p in vcon.Get()
                       orderby p.CRANEID ascending
                       where 1 == 1
                       select p;

        if (!string.IsNullOrEmpty(txtCraneID_SJJ.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANEID) && x.CRANEID.Contains(txtCraneID_SJJ.Text.Trim()));

        if (!string.IsNullOrEmpty(txtCraneName_SJJ.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANENAME) && x.CRANENAME.Contains(txtCraneName_SJJ.Text.Trim()));

        if (!string.IsNullOrEmpty(txtCraneIP_SJJ.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANEIP) && x.CRANEIP.Contains(txtCraneIP_SJJ.Text.Trim()));

        //if (!string.IsNullOrEmpty(txtTTYPE_SJJ.Text.Trim()))
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.TTYPE) && x.TTYPE.Contains(txtTTYPE_SJJ.Text.Trim()));
        if (!string.IsNullOrEmpty(txtEQUIPMENTNAME_SJJ.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.EQUIPMENTNAME) && x.EQUIPMENTNAME.Contains(txtEQUIPMENTNAME_SJJ.Text.Trim()));
        if (ddlSTATUS_SJJ.SelectedValue != "")
        {
            caseList = caseList.Where(x => x.FLAG.ToString().Equals(ddlSTATUS_SJJ.SelectedValue));
        }
        caseList = caseList.Where(x => x.PLCType =="SJJ");
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager5.RecordCount = caseList.Count();
            AspNetPager5.PageSize = this.PageSize;
        }
        AspNetPager5.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

        var data = GetPageSize(caseList, PageSize, CurrendIndex5).ToList();

        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("FLAG", "FLAGName", "BASE_CLIENT"));//是否保税

        var srcdata = GetGridDataByAddColumns(data, flagList);

        grdSJJ.DataSource = srcdata;
        grdSJJ.DataBind();
    }

    protected void AspNetPager5_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex5 = AspNetPager5.CurrentPageIndex;//索引同步
        GridBind_SJJ();
    }

    protected void grdSJJ_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.grdSJJ.DataKeys[e.Row.RowIndex].Values[0].ToString();//this.GetKeyIDS_SJJ(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";

            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("frmBaseSJJ.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBase_CraneConfig_Msg18, "BASE_AGV"); //提升机管理
        }
    }

    /// <summary>
    /// 升降机停用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDiscontinue_SJJ_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_CRANECONFIG> con = new GenericRepository<BASE_CRANECONFIG>(context);
        try
        {
            int number = 0;
            for (int i = 0; i < grdSJJ.Rows.Count; i++)
            {
                if (this.grdSJJ.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSJJ.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                        number++;
                }
            }
            if (number == 0)
            {
                throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg01);//请选择需要停用的项！
            }
            for (int i = 0; i < this.grdSJJ.Rows.Count; i++)
            {
                if (this.grdSJJ.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSJJ.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string status = this.grdSJJ.DataKeys[i].Values[1].ToString();
                        if (status=="0")//if (this.grdSJJ.Rows[i].Cells[6].Text.Equals("使用中"))
                        {
                            string id = this.grdSJJ.DataKeys[i].Values[0].ToString();
                            var caseList = from p in con.Get()
                                           where p.ID == id
                                           select p;
                            BASE_CRANECONFIG entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG>();
                            entity.DISCONTINUEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                            entity.DISCONTINUETIME = DateTime.Now;
                            entity.FLAG = "1";
                            con.Update(entity);
                            con.Save();
                        }
                        else
                        {
                            throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg10);//只有状态为[使用中]才能停用.
                        }
                    }
                }
            }
            this.GridBind_SJJ();
            msg = Resources.Lang.FrmBase_CraneConfig_Msg11; //停用成功！
        }
        catch (Exception Ex)
        {

            msg += Resources.Lang.FrmBase_CraneConfig_Msg12 + "[" + Ex.Message + "]"; //停用失败!
        }
        this.Alert(msg);
    }
    #endregion

    #region AGV站点
    public void GridBind_AGVSITE()
    {
        using (var modContext = context)
        {
            var caseList = from p in modContext.BASE_CRANECONFIG_DETIAL
                           select p;

            if (!string.IsNullOrEmpty(txtSiteID.Text.Trim()))
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.SITEID) && x.SITEID.Contains(txtSiteID.Text.Trim()));
            if (!string.IsNullOrEmpty(txtSiteName.Text.Trim()))
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.SITENAME) && x.SITENAME.Contains(txtSiteName.Text.Trim()));
            if (ddlAGVSITETYPE.SelectedValue != "")
                caseList = caseList.Where(x => x.AGVSITETYPE.ToString().Equals(ddlAGVSITETYPE.SelectedValue));
            if (!string.IsNullOrEmpty(txtSTOREY.Text.Trim()))
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.STOREY) && x.STOREY.Equals(txtSTOREY.Text.Trim()));
            if (ddlCSTATUS_AS.SelectedValue != "")
            {
                caseList = caseList.Where(x => x.FLAG.ToString().Equals(ddlCSTATUS_AS.SelectedValue));
            }
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.AGVSITETYPE));
            if (ddlISOCCUPY.SelectedValue != "")
            {
                caseList = caseList.Where(x=>!string.IsNullOrEmpty(x.STOREY) && x.IS_OCCUPY.ToString().Equals(ddlISOCCUPY.SelectedValue));
            }
            if (caseList != null && caseList.Count() > 0)
            {
                AspNetPager6.RecordCount = caseList.Count();
                AspNetPager6.PageSize = this.PageSize;
            }
            caseList = caseList.OrderBy(x => x.SITEID);

            AspNetPager6.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

            var data = GetPageSize(caseList, PageSize, CurrendIndex6).ToList();

            List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
            flagList.Add(new Tuple<string, string, string>("FLAG", "FLAGName", "BASE_CLIENT"));//状态
            flagList.Add(new Tuple<string, string, string>("AGVSITETYPE","AGVSITETYPEName", "SITETYPE"));//类型
            flagList.Add(new Tuple<string, string, string>("IS_OCCUPY", "OCCUPYName", "AGVSITE"));//类别

            var srcdata = GetGridDataByAddColumns(data, flagList);

            grdAGVSite.DataSource = srcdata;
            grdAGVSite.DataBind();
        }
    }
    protected void grdAGVSite_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[6].Text)
            {
                case "Y": e.Row.Cells[6].Text = Resources.Lang.FrmALLOCATE_DEdit_SN_Msg03;//"是";
                    break;
                case "N": e.Row.Cells[6].Text = Resources.Lang.FrmALLOCATE_DEdit_SN_Msg04;//"否";
                    break;
                default: e.Row.Cells[6].Text = Resources.Lang.Common_Exception;//"异常"
                    break;
            }
            
            string strKeyID = this.grdAGVSite.DataKeys[e.Row.RowIndex].Values[0].ToString();//this.GetKeyIDS_AS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBase_AGV_D.aspx?Index=" + CurrendIndex6, SYSOperation.Modify, strKeyID), Resources.Lang.FrmBase_CraneConfig_Msg19, "BASE_AGV_D"); //AGV站点管理
        }
    }
    protected void btnDiscontinueAGVS_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        try
        {
            int number = 0;
            for (int i = 0; i < grdAGVSite.Rows.Count; i++)
            {
                if (this.grdAGVSite.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdAGVSite.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                        number++;
                }
            }
            if (number == 0)
            {
                throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg01);//请选择需要停用的项！
            }
            for (int i = 0; i < this.grdAGVSite.Rows.Count; i++)
            {
                if (this.grdAGVSite.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdAGVSite.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string status = this.grdAGVSite.DataKeys[i].Values[1].ToString();
                        if (status=="0")//if (this.grdAGVSite.Rows[i].Cells[7].Text.Equals("使用中"))
                        {
                            string id = this.grdAGVSite.DataKeys[i].Values[0].ToString();
                            var caseList = from p in con.Get()
                                           where p.ID == id
                                           select p;
                            BASE_CRANECONFIG_DETIAL entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG_DETIAL>();
                            entity.DISCONTINUEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                            entity.DISCONTINUETIME = DateTime.Now;
                            entity.FLAG = "1";
                            con.Update(entity);
                            con.Save();
                        }
                        else
                        {
                            throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg10);//只有状态为[使用中]才能停用.
                        }
                    }
                }
            }
            this.GridBind_AGVSITE();
            msg = Resources.Lang.FrmBase_CraneConfig_Msg11; //停用成功！
        }
        catch (Exception Ex)
        {

            msg += Resources.Lang.FrmBase_CraneConfig_Msg12 + "[" + Ex.Message + "]"; //停用失败!
        }
        this.Alert(msg);
    }
    protected void btnSearchAGVS_Click(object sender, EventArgs e)
    {
        if (Session["Index6"] != null)
        {
            CurrendIndex6 = Convert.ToInt32(Session["Index6"]);
            Session.Remove("Index6");
        }
        else
            CurrendIndex6 = 1;
        AspNetPager6.CurrentPageIndex = 1;
        this.GridBind_AGVSITE();
    }

    protected void AspNetPager6_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex6 = AspNetPager6.CurrentPageIndex;//索引同步
        GridBind_AGVSITE();
    }

    //private string GetKeyIDS_AS(int rowIndex)
    //{
    //    string strKeyId = "";
    //    for (int i = 0; i < this.grdAGVSite.DataKeyNames.Length; i++)
    //    {
    //        strKeyId += this.grdAGVSite.DataKeys[rowIndex].Values[i].ToString() + ",";
    //    }
    //    strKeyId = strKeyId.TrimEnd(',');
    //    return strKeyId;
    //}
    #endregion


    #region 台车
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchCAR_Click(object sender, EventArgs e)
    {
        //BUCKINGHA-894
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex7 = 1;
            AspNetPager7.CurrentPageIndex = 1;
        }
        this.GridBind_CAR();
        IsFirstPage = true;//恢复默认值       
    }

    public void GridBind_CAR()
    {
        using (var modContext = new DBContext())
        {
            var caseList = from p in modContext.BASE_CRANECONFIG
                           orderby p.CRANEID ascending
                           where 1 == 1
                           select p;

            if (!string.IsNullOrEmpty(txtCraneID_CAR.Text.Trim()))
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANEID) && x.CRANEID.Contains(txtCraneID_CAR.Text.Trim()));

            if (!string.IsNullOrEmpty(txtCraneName_CAR.Text.Trim()))
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANENAME) && x.CRANENAME.Contains(txtCraneName_CAR.Text.Trim()));

            if (!string.IsNullOrEmpty(txtCraneIP_CAR.Text.Trim()))
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANEIP) && x.CRANEIP.Contains(txtCraneIP_CAR.Text.Trim()));

            if (!string.IsNullOrEmpty(txtEQUIPMENTNAME_CAR.Text.Trim()))
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.EQUIPMENTNAME) && x.EQUIPMENTNAME.Contains(txtEQUIPMENTNAME_CAR.Text.Trim()));

            if (ddlSTATUS_CAR.SelectedValue != "")
            {
                caseList = caseList.Where(x => x.FLAG.ToString().Equals(ddlSTATUS_CAR.SelectedValue));
            }
            caseList = caseList.Where(x => x.PLCType == "CAR");
            if (caseList != null && caseList.Count() > 0)
            {
                AspNetPager7.RecordCount = caseList.Count();
                AspNetPager7.PageSize = this.PageSize;
            }
            AspNetPager7.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

            var data = GetPageSize(caseList, PageSize, CurrendIndex7).ToList();

            List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
            flagList.Add(new Tuple<string, string, string>("FLAG", "FLAGName", "BASE_CLIENT"));//是否保税

            var srcdata = GetGridDataByAddColumns(data, flagList);

            grdCAR.DataSource = srcdata;
            grdCAR.DataBind();
        }
    }
    protected void AspNetPager7_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex7 = AspNetPager7.CurrentPageIndex;//索引同步
        GridBind_CAR();
    }

    protected void grdCAR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.grdCAR.DataKeys[e.Row.RowIndex].Values[0].ToString();
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";

            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBase_CAR.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Element_TaiCheGuanLi , "BASE_CAR"); //台车管理
        }
    }

    /// <summary>
    /// 台车停用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDiscontinue_CAR_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_CRANECONFIG> con = new GenericRepository<BASE_CRANECONFIG>(context);
        try
        {
            int number = 0;
            for (int i = 0; i < grdCAR.Rows.Count; i++)
            {
                if (this.grdCAR.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdCAR.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                        number++;
                }
            }
            if (number == 0)
            {
                throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg01);//请选择需要停用的项！
            }
            for (int i = 0; i < this.grdCAR.Rows.Count; i++)
            {
                if (this.grdCAR.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdCAR.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string status = this.grdCAR.DataKeys[i].Values[1].ToString();
                        if (status == "0")//if (this.grdCAR.Rows[i].Cells[6].Text.Equals("使用中"))
                        {
                            string id = this.grdCAR.DataKeys[i].Values[0].ToString();
                            var caseList = from p in con.Get()
                                           where p.ID == id
                                           select p;
                            BASE_CRANECONFIG entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG>();
                            entity.DISCONTINUEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                            entity.DISCONTINUETIME = DateTime.Now;
                            entity.FLAG = "1";
                            con.Update(entity);
                            con.Save();
                        }
                        else
                        {
                            throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg10);//只有状态为[使用中]才能停用.
                        }
                    }
                }
            }
            this.GridBind_CAR();
            msg = Resources.Lang.FrmBase_CraneConfig_Msg11; //停用成功！
        }
        catch (Exception Ex)
        {

            msg += Resources.Lang.FrmBase_CraneConfig_Msg12 + "[" + Ex.Message + "]"; //停用失败!
        }
        this.Alert(msg);
    }

    #endregion

    #region 输送线
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchSSX_Click(object sender, EventArgs e)
    {
        //BUCKINGHA-894
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex8 = 1;
            AspNetPager8.CurrentPageIndex = 1;
        }
        this.GridBind_SSX();
        IsFirstPage = true;//恢复默认值       
    }

    public void GridBind_SSX()
    {
        using (var modContext = new DBContext())
        {
            var caseList = from p in modContext.BASE_CRANECONFIG
                           orderby p.CRANEID ascending
                           where 1 == 1
                           select p;

            if (!string.IsNullOrEmpty(txtCraneID_SSX.Text.Trim()))
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANEID) && x.CRANEID.Contains(txtCraneID_SSX.Text.Trim()));

            if (!string.IsNullOrEmpty(txtCraneName_SSX.Text.Trim()))
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANENAME) && x.CRANENAME.Contains(txtCraneName_SSX.Text.Trim()));

            if (!string.IsNullOrEmpty(txtCraneIP_CAR.Text.Trim()))
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANEIP) && x.CRANEIP.Contains(txtCraneIP_CAR.Text.Trim()));

            if (!string.IsNullOrEmpty(txtEQUIPMENTNAME_CAR.Text.Trim()))
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.EQUIPMENTNAME) && x.EQUIPMENTNAME.Contains(txtEQUIPMENTNAME_CAR.Text.Trim()));

            if (ddlSTATUS_CAR.SelectedValue != "")
            {
                caseList = caseList.Where(x => x.FLAG.ToString().Equals(ddlSTATUS_CAR.SelectedValue));
            }
            caseList = caseList.Where(x => x.PLCType == "SSX");
            if (caseList != null && caseList.Count() > 0)
            {
                AspNetPager8.RecordCount = caseList.Count();
                AspNetPager8.PageSize = this.PageSize;
            }
            AspNetPager8.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

            var data = GetPageSize(caseList, PageSize, CurrendIndex8).ToList();

            List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
            flagList.Add(new Tuple<string, string, string>("FLAG", "FLAGName", "BASE_CLIENT"));//是否保税

            var srcdata = GetGridDataByAddColumns(data, flagList);

            grdSSX.DataSource = srcdata;
            grdSSX.DataBind();
        }
    }
    protected void AspNetPager8_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex8 = AspNetPager8.CurrentPageIndex;//索引同步
        GridBind_SSX();
    }

    protected void grdSSX_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.grdSSX.DataKeys[e.Row.RowIndex].Values[0].ToString();
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";

            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBase_SSX.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Element_ShuSongXianGuanLi , "BASE_SSX"); //输送线管理
        }
    }

    /// <summary>
    /// 台车停用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDiscontinue_SSX_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_CRANECONFIG> con = new GenericRepository<BASE_CRANECONFIG>(context);
        try
        {
            int number = 0;
            for (int i = 0; i < grdSSX.Rows.Count; i++)
            {
                if (this.grdSSX.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSSX.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                        number++;
                }
            }
            if (number == 0)
            {
                throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg01);//请选择需要停用的项！
            }
            for (int i = 0; i < this.grdSSX.Rows.Count; i++)
            {
                if (this.grdSSX.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSSX.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string status = this.grdSSX.DataKeys[i].Values[1].ToString();
                        if (status == "0")
                        {
                            string id = this.grdSSX.DataKeys[i].Values[0].ToString();
                            var caseList = from p in con.Get()
                                           where p.ID == id
                                           select p;
                            BASE_CRANECONFIG entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG>();
                            entity.DISCONTINUEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                            entity.DISCONTINUETIME = DateTime.Now;
                            entity.FLAG = "1";
                            con.Update(entity);
                            con.Save();
                        }
                        else
                        {
                            throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg10);//只有状态为[使用中]才能停用.
                        }
                    }
                }
            }
            this.GridBind_SSX();
            msg = Resources.Lang.FrmBase_CraneConfig_Msg11; //停用成功！
        }
        catch (Exception Ex)
        {

            msg += Resources.Lang.FrmBase_CraneConfig_Msg12 + "[" + Ex.Message + "]"; //停用失败!
        }
        this.Alert(msg);
    }

    #endregion

}