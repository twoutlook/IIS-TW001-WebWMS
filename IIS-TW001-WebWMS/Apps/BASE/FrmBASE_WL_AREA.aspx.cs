using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;

using DreamTek.ASRS.Business;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Data.Entity.SqlServer;

/// <summary>
/// 描述: 储位管理-->FrmBASE_CARGOSPACEList 页面后台类文件
/// 作者: --wjw
/// 创建于: 2012-10-09 18:29:23
/// </summary>
public partial class FrmBASE_WL_AREA : WMSBasePage //WebBasePage, IPageGrid
{
    BasePartListQuery bptQry = new BasePartListQuery();

    protected void Page_Load(object sender, EventArgs e)
    {
        ucShowArea.SetCompName = txtCDEFINE1.ClientID;
        ucShowArea.SetORGCode = txtAreaID.ClientID;
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.InitPage1();
            //SelectedCARGOSPACE_Dt = BASE_PARTRule.GetAllLablePlace(PartId);
            // 加载已设置的储位编码

            //LoadBASE_PART_CARGOSPACE();
            LoadOPERATOR_AREA();

            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
            this.btnSearch1_Click(this.btnSearch1, EventArgs.Empty);

            //LoadBASE_PART_CARGOSPACE();
            //  LoadOPERATOR_AREA();
        }

        //CQ 2014-9-10 10:30:08
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        btnSave1.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave1) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
      
        //this.FunctionNo = ""; //TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
    }

    /// <summary>
    /// 已选择的储位信息
    /// </summary>
    public Dictionary<string,string> SelectedCARGOSPACE_Dt
    {
        get { return ViewState["SelectedCARGOSPACE_Dt"] as Dictionary<string, string>; }
        set { ViewState["SelectedCARGOSPACE_Dt"] = value; }
    }

    /// <summary>
    /// 物料ID
    /// </summary>
    public string PartId
    {
        get { return ViewState["PartId"].ToString(); }
        set { ViewState["PartId"] = value; }
    }

    /// <summary>
    /// 加载已设置的储位编码
    /// </summary>
    private void LoadBASE_PART_CARGOSPACE()
    {
        //BASE_FrmBASE_PARTListQuery query = new BASE_FrmBASE_PARTListQuery();
        BasePartListQuery query = new BasePartListQuery();

        SelectIds = query.GetBasePartCargoSpaceByPartId(PartId);
        SelectedCARGOSPACE_Dt = query.GetAllLablePlace(PartId);

         
    }




    #region IPageGrid 成员

    //public void GridBind()
    //{
    //    BASE_FrmBASE_CARGOSPACEListQuery listQuery = new BASE_FrmBASE_CARGOSPACEListQuery();
    //    DataTable dtSource = listQuery.GetList("", "", "", txtCCARGOID.Text, txtCCARGONAME.Text, ddlCSTATUS.SelectedValue, txtCALIAS.Text, txtCERPCODE.Text, txtCTYPE.Text, 
    //        txtDEXPIREDATEFrom.Text, txtDEXPIREDATETo.Text, ddlIPERMITMIX.SelectedValue, "", GetAreaId(), ddlBSC.SelectedValue, ddlISSetArea.SelectedValue, PartId, false, this.grdNavigatorBASE_CARGOSPACE.CurrentPageIndex, this.grdBASE_CARGOSPACE.PageSize);
    //    this.grdBASE_CARGOSPACE.DataSource = dtSource;
    //    this.grdBASE_CARGOSPACE.DataBind();
    //}

    public IQueryable<v_BASE_CARGOSPACE> GetQueryList()
    {
        IGenericRepository<v_BASE_CARGOSPACE> pigeonBill = new GenericRepository<v_BASE_CARGOSPACE>(db);
        var caseList = from p in pigeonBill.Get()
                       orderby p.CPOSITIONCODE descending
                       where 1==1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCCARGOID.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CPOSITIONCODE) && x.CPOSITIONCODE.Contains(txtCCARGOID.Text.Trim()));
            }
            if (txtCCARGONAME.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CPOSITION) && x.CPOSITION.Contains(txtCCARGONAME.Text.Trim()));
            }
            if (ddlCSTATUS.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CSTATUS) && x.CSTATUS.Contains(ddlCSTATUS.SelectedValue));
            }
            if (txtCALIAS.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CALIAS) && x.CALIAS.Contains(txtCALIAS.Text.Trim()));
            }
            if (txtCERPCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CERPCODE) && x.CERPCODE.Contains(txtCERPCODE.Text.Trim()));
            }
            if (txtCTYPE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CTYPE) && x.CTYPE.Contains(txtCTYPE.Text.Trim()));
            }
            if (txtDEXPIREDATEFrom.Text != string.Empty)
            {
                caseList = caseList.Where(x => x.DEXPIREDATE != null && x.DEXPIREDATE.HasValue == true 
                    && Convert.ToDateTime( x.DEXPIREDATE.Value.ToString("yyyy-MM-dd") ) >= Convert.ToDateTime(txtDEXPIREDATEFrom.Text.Trim()));
            }
            if (txtDEXPIREDATETo.Text != string.Empty)
            {
                caseList = caseList.Where(x => x.DEXPIREDATE != null && x.DEXPIREDATE.HasValue == true
                    && Convert.ToDateTime(x.DEXPIREDATE.Value.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(txtDEXPIREDATETo.Text.Trim()));
            }
            if (ddlIPERMITMIX.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => x.IPERMITMIX != null && x.IPERMITMIX.HasValue == true && x.IPERMITMIX.Value.ToString() == (ddlIPERMITMIX.SelectedValue));
            }
            //是否已设置储位
            if (ddlISSetArea.SelectedValue == "0" || ddlISSetArea.SelectedValue == "1")
            {
                BASE_PART bpt = bptQry.GetBasePartByID(this.PartId);

                IGenericRepository<DreamTek.ASRS.DAL.BASE_PART_CARGOSPACE> BPCConn = new GenericRepository<DreamTek.ASRS.DAL.BASE_PART_CARGOSPACE>(db);
                var bpv = (from p in BPCConn.Get()
                           where p.cpartnumber == bpt.cpartnumber
                           select p.cpositioncode ).ToList();
                if (ddlISSetArea.SelectedValue == "1")
                {
                    caseList = caseList.Where(x => bpv.Contains(x.CPOSITIONCODE));
                }
                else {
                    caseList = caseList.Where(x => !bpv.Contains(x.CPOSITIONCODE));
                }
                //caseList = caseList.Where(x => x.IPERMITMIX != null && x.IPERMITMIX.HasValue == true && x.IPERMITMIX.Value.ToString() == (ddlIPERMITMIX.SelectedValue));
            }
            var quye =  GetAreaId();
            if (quye != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CDEFINE1) && x.CDEFINE1.Contains(quye));
            }

            if (ddlBSC.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.BONDED) && x.BONDED.Contains(ddlBSC.SelectedValue));
            }

        }
        return caseList;
    }

    public void GridBind(string sortStr)
    {
        var caseList = GetQueryList();


        if (caseList != null && caseList.Count() > 0)
        {
            //按照列名排序
            //if (!string.IsNullOrEmpty(sortStr))
            //{
            //    caseList = caseList.OrderBy(sortStr);
            //}

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }
        grdBASE_CARGOSPACE.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdBASE_CARGOSPACE.DataBind();

    }


    public bool CheckData()
    {
        if (this.txtCCARGOID.Text.Trim().Length > 0)
        {
        }
        if (this.txtCCARGONAME.Text.Trim().Length > 0)
        {
        }
        if (this.txtCALIAS.Text.Trim().Length > 0)
        {
        }
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtCTYPE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDEXPIREDATEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDEXPIREDATEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmBASE_PARTList_Msg01);//终止日期项不是有效的日期！
                this.SetFocus(txtDEXPIREDATEFrom);
                return false;
            }
        }
        if (this.txtDEXPIREDATETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_ToItemEmpty);//到项不允许空！
            this.SetFocus(txtDEXPIREDATETo);
            return false;
        }
        if (this.txtDEXPIREDATETo.Text.Trim().Length > 0)
        {
            if (this.txtDEXPIREDATETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmALLOCATEList_NoEffToItem);//到项不是有效的日期！
                this.SetFocus(txtDEXPIREDATETo);
                return false;
            }
        }
        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        PartId = Request.QueryString["ID"];
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";
        this.grdBASE_CARGOSPACE.DataKeyNames = new string[] { "CPOSITIONCODE" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口

        this.btnNew.Attributes["onclick"] = "PopupFloatWin('" + BuildRequestPageURL("FrmBASE_CARGOSPACEEdit.aspx", SYSOperation.New, "") + "','"+Resources.Lang.FrmBASE_CARGOSPACEList_Mag03 +"','BASE_CARGOSPACE',600,370);return false;";//新建储位管理

        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BAR');return false;";


        txtCDEFINE1.Attributes["onclick"] = "Show('" + ucShowArea.GetDivName + "');";
        //在新窗口打开的代码： this.OpenWin(btnNew,WebBasePage.BuildRequestPageURL("FrmBASE_CARGOSPACEEdit.aspx", SysOperation.New,""),800,600);
        Dictionary<string, string> dt = bptQry.GetAllLablePlace(PartId);
        if (dt.Values.Count > 0)
        {
            if (dt.Values.Count == 1)
            {
                if (dt.ContainsKey("*"))
                {
                    ddlAllType.SelectedIndex = 1;
                }
            }
        }

    }

    #endregion

    //protected DataTable grdNavigatorBASE_CARGOSPACE_GetExportToExcelSource()
    //{
    //    BASE_FrmBASE_CARGOSPACEListQuery listQuery = new BASE_FrmBASE_CARGOSPACEListQuery();
    //    DataTable dtSource = listQuery.GetList("", "", "", this.txtCCARGOID.Text, this.txtCCARGONAME.Text, ddlCSTATUS.SelectedValue, this.txtCALIAS.Text, this.txtCERPCODE.Text, this.txtCTYPE.Text, this.txtDEXPIREDATEFrom.Text, this.txtDEXPIREDATETo.Text, ddlIPERMITMIX.SelectedValue, "", GetAreaId(), ddlBSC.SelectedValue, ddlISSetArea.SelectedValue, PartId, false, -1, -1);
    //    return dtSource;
    //}
    private string GetAreaId()
    {
        BasePartListQuery qry = new BasePartListQuery();

        string areaId = string.Empty;
        if (string.IsNullOrEmpty(txtCDEFINE1.Text))
            txtAreaID.Text = string.Empty;

        var areaId2 = qry.GetAREANAMEBYName(txtCDEFINE1.Text);
        //DataTable dtArea = new BAS_AREAListQuery().GetAREANAMEBYName(txtCDEFINE1.Text);
        if (!string.IsNullOrEmpty(areaId2))
            areaId = areaId2;
        else areaId = txtAreaID.Text;
        return areaId;
    }
    protected void grdBASE_CARGOSPACE_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorBASE_CARGOSPACE.IsDbPager)
        //{
        //    grdNavigatorBASE_CARGOSPACE.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdBASE_CARGOSPACE.PageIndex = e.NewPageIndex;
        //}
    }
    protected void grdBASE_CARGOSPACE_PageIndexChanged(object sender, EventArgs e)
    {

        //if(grdNavigatorBASE_CARGOSPACE.IsDbPager)
        {
            this.GridBind("");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadBASE_PART_CARGOSPACE();
        //grdNavigatorBASE_CARGOSPACE.CurrentPageIndex = 0;
        ////重新设置GridNavigator的RowCount
        //BASE_FrmBASE_CARGOSPACEListQuery listQuery = new BASE_FrmBASE_CARGOSPACEListQuery();
        //DataTable dtRowCount = listQuery.GetList("", "", "", this.txtCCARGOID.Text, this.txtCCARGONAME.Text, ddlCSTATUS.SelectedValue, this.txtCALIAS.Text, this.txtCERPCODE.Text, this.txtCTYPE.Text, this.txtDEXPIREDATEFrom.Text, this.txtDEXPIREDATETo.Text, ddlIPERMITMIX.SelectedValue, "", GetAreaId(), ddlBSC.SelectedValue, ddlISSetArea.SelectedValue, PartId, true, 0, 0);

        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdNavigatorBASE_CARGOSPACE.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdNavigatorBASE_CARGOSPACE.RowCount = 0;
        //}

        //this.GridBind();
        AspNetPager1.CurrentPageIndex = 1;
        CurrendIndex = 1;
        GridBind("");
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorBASE_CARGOSPACE
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //DBUtil.BeginTrans();
        try
        {
            IGenericRepository<DreamTek.ASRS.DAL.BASE_CARGOSPACE> con = new GenericRepository<DreamTek.ASRS.DAL.BASE_CARGOSPACE>(db);
            for (int i = 0; i < this.grdBASE_CARGOSPACE.Rows.Count; i++)
            {
                if (this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        DreamTek.ASRS.DAL.BASE_CARGOSPACE bo = new DreamTek.ASRS.DAL.BASE_CARGOSPACE();
                        var caseList = from p in con.Get()
                                       where p.id == this.grdBASE_CARGOSPACE.DataKeys[i].Values[0].ToString()
                                       select p;
                        bo = caseList.ToList().FirstOrDefault<DreamTek.ASRS.DAL.BASE_CARGOSPACE>();
                        con.Delete(bo);
                    }
                }
            }
            con.Save();
              this.Alert(Resources.Lang.Common_SuccessDel); //删除成功!         
            //DBUtil.Commit();

            this.GridBind("");
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.Common_FailDel + E.Message.ToJsString()); //删除失败！
            //DBUtil.Rollback();
        }
    }



    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdBASE_CARGOSPACE.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBASE_CARGOSPACE.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdBASE_CARGOSPACE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var ba = new BasePartListQuery().GetBASE_AREA_ById(e.Row.Cells[e.Row.Cells.Count - 14].Text);
            if (ba != null )
                e.Row.Cells[e.Row.Cells.Count - 14].Text = ba.area_name;


            //e.Row.Cells[e.Row.Cells.Count - 13].Text = e.Row.Cells[e.Row.Cells.Count - 13].Text == "Y" ? "是" : "否";
            //ddlBSC 是否保稅
            //e.Row.Cells[e.Row.Cells.Count - 13].Text = ddlBSC.Items.FindByValue(e.Row.Cells[e.Row.Cells.Count - 13].Text.Trim()).Text;
            try
            {
                e.Row.Cells[e.Row.Cells.Count - 13].Text = ddlBSC.Items.FindByValue(e.Row.Cells[e.Row.Cells.Count - 13].Text.Trim()).Text;
            }
            catch
            {
                e.Row.Cells[e.Row.Cells.Count - 13].Text = Resources.Lang.Common_ExceptionStatus; //异常状态
            }
            //获取ID
            string id = this.grdBASE_CARGOSPACE.DataKeys[e.Row.RowIndex][0].ToString();
            CheckBox cbo = e.Row.FindControl("chkSelect") as CheckBox;
            cbo.Attributes.Add("onclick", "SelIDCancelAll()");

            if (SelectedCARGOSPACE_Dt.Values.Count > 0)
            {
                if (SelectedCARGOSPACE_Dt.Values.Count == 1 && SelectedCARGOSPACE_Dt.ContainsKey("*"))
                {
                    cbo.Checked = true;
                }
                else
                {
                    //SelectedCARGOSPACE_Dt = BASE_PARTRule.GetPlaceWLRelPlace(PartId, id);
                    if (SelectedCARGOSPACE_Dt.ContainsKey(id))
                    {
                        cbo.Checked = true;
                    }
                    if (SelectIds.ContainsKey(id))
                    {
                        //如果是控件处于选中状态
                        cbo.Checked = true;
                    }
                }
            }
        }

    }

    protected void dsGrdBASE_CARGOSPACE_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdBASE_CARGOSPACE_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.ReturnValue is DataTable)
        {
            //if (grdNavigatorBASE_CARGOSPACE.IsDbPager == false)
            //    this.grdNavigatorBASE_CARGOSPACE.RowCount = ((DataTable)e.ReturnValue).Rows.Count;
        }

    }

    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }
    public Dictionary<string, string> NoSelectIds
    {
        set { ViewState["NoSelectIds"] = value; }
        get { return ViewState["NoSelectIds"] as Dictionary<string, string>; }
    }
    /// <summary>
    /// 获取选择的ID
    /// </summary>
    public void GetSelectedIds()
    {
        if (SelectIds == null)
        {
            SelectIds = new Dictionary<string, string>();
        }

        foreach (GridViewRow item in this.grdBASE_CARGOSPACE.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string code = this.grdBASE_CARGOSPACE.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                //控件选中且集合中不存在添加
                if (cbo.Checked && !SelectIds.ContainsKey(code))
                {
                    SelectIds.Add(code, code);
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(code))
                {
                    SelectIds.Remove(code);
                }
            }
        }
    }
    /// <summary>
    /// 获取没有选择的编号
    /// </summary>
    public void GetNoSelectedIds()
    {
        if (NoSelectIds == null)
            NoSelectIds = new Dictionary<string, string>();
        foreach (GridViewRow item in this.grdBASE_CARGOSPACE.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;
                //获取ID
                string code = this.grdBASE_CARGOSPACE.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;
                //控件选中且集合中不存在添加
                if (!cbo.Checked && !NoSelectIds.ContainsKey(code))
                {
                    NoSelectIds.Add(code, code);
                }
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        this.GridBind("");
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex2 = AspNetPager2.CurrentPageIndex;//索引同步
        this.GridBind1("");
    }

    /// <summary>
    /// 设置的储位
    /// </summary>
    //public string Select_BASE_CARGOSPACE
    //{
    //    get { return ViewState["Select_BASE_CARGOSPACE"].ToString(); }
    //    set { ViewState["Select_BASE_CARGOSPACE"] = value; }
    //}

    //物料管理页面进行储位关联时，若为保税物料，保存时若选择了非保税储位则进行提示
    private bool CheckBondedWithCargospace(Dictionary<string, string> SelectIds, bool isAll)
    {
        bool bl = false;

        BasePartListQuery qry = new BasePartListQuery();
        BASE_PART bpt = qry.GetBasePartByID(this.PartId);

        //BASE_PARTEntity entity = new BASE_PARTEntity();
        //entity.ID = this.PartId;
        //entity.SelectByPKeys();
        //if (entity.BONDED != null)
        if(bpt != null && bpt.bonded != null && bpt.bonded.HasValue)
        {
            //bl = BASE_PARTRule.CheckBondedWithCargospace(entity.BONDED == 0 ? "Y" : "N", SelectIds, isAll);
            bl = qry.CheckBondedWithCargospace(bpt.bonded.Value == 0 ? "Y" : "N", SelectIds, isAll);
        }
        return bl;
    }

    /// <summary>
    /// 保存按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {

        GetSelectedIds();

        string msg = "";

        //选择的编号，选择指定的用户编号，登录用户账号，登录用户名称

        ////当选择关联储位复选框时，将当前物料保存对应所有储位，在关联表中储位关系用*号表示
        ////当取消所有关联时，删除对应的物料管理储位关系
        ////当状态为对应的所有储位，取消已默认选择的关联储位时，记录取消的储位编号，保存时除取消的编号外其他所有的储位编号

        //1设置所选储位 2设置所有储位3取消所有储位关联
        string selType = ddlAllType.SelectedValue;

        if (selType == "1")//设置所选储位
        {
            //* 去除星号
            if (SelectIds.ContainsKey("*"))
            {
                SelectIds.Remove("*");
            }

            if (CheckBondedWithCargospace(SelectIds, false))
            {
                Alert(Resources.Lang.FrmBASE_WL_AREA_Msg02);//储位的税别与物料的不一致！
                return;
            }
            if (bptQry.SetBasePartCargospace(SelectIds, PartId, WmsWebUserInfo.GetCurrentUser().UserNo + "(" + WmsWebUserInfo.GetCurrentUser().UserNo + ")"))
            //if (BASE_PARTRule.SetBasePartCargospace(SelectIds, PartId, WebUserInfo.GetCurrentUser().UserNo + "(" + WebUserInfo.GetCurrentUser().UserNo + ")"))
            {
                //Select_BASE_CARGOSPACE = SelectIds.Count.ToString();
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg03;//设置成功!
                //WLBM=" + Request.QueryString["WLBM"].ToString()
                AlertAndBack("FrmBASE_WL_AREA.aspx?" + BuildQueryString(SYSOperation.New, PartId), msg);
            }
            else
            {
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg04;//设置失败
                Alert(msg);
            }
        }
        else if (selType == "2")//设置所有储位
        {
            if (CheckBondedWithCargospace(null, true))
            {
                Alert(Resources.Lang.FrmBASE_WL_AREA_Msg02);//储位的税别与物料的不一致！
                return;
            }
            if (bptQry.SetWLAreaRel(PartId, WmsWebUserInfo.GetCurrentUser().UserNo + "(" + WmsWebUserInfo.GetCurrentUser().UserNo + ")", true))
            //if (BASE_PARTRule.SetWLAreaRel(PartId, WebUserInfo.GetCurrentUser().UserNo + "(" + WebUserInfo.GetCurrentUser().UserNo + ")", true))
            {
                //Select_BASE_CARGOSPACE = SelectIds.Count.ToString();
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg03;//设置成功!
                //WLBM="+Request.QueryString["WLBM"].ToString()
                this.AlertAndBack("FrmBASE_WL_AREA.aspx?" + BuildQueryString(SYSOperation.New, PartId), msg);
            }
            else
            {
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg04;//设置失败
                Alert(msg);
            }
        }
        else//取消所有储位关联
        {
            if (bptQry.SetCancelAllPRel(PartId))
            //if (BASE_PARTRule.SetCancelAllPRel(PartId))
            {
                //Select_BASE_CARGOSPACE = "";
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg03;//设置成功!
                //WLBM="+Request.QueryString["WLBM"].ToString()
                this.AlertAndBack("FrmBASE_WL_AREA.aspx?" + BuildQueryString(SYSOperation.New, PartId), msg);
            }
            else
            {
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg04;//设置失败
                Alert(msg);
            }
        }
        //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        //CQ 2014-9-10 10:30:08
        btnSave.Style.Remove("disabled");

    }

    /// <summary>
    /// 数据邦定前 获取上一页已选中的行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdBASE_CARGOSPACE_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }

    /****************************设置物料关联的区域*************************/
    /// <summary>
    /// 物料关联的区域
    /// </summary>
    public Dictionary<string, string> SelectIds1
    {
        set { ViewState["SelectIds1"] = value; }
        get { return ViewState["SelectIds1"] as Dictionary<string, string>; }
    }
    /// <summary>
    /// 读取的优先级
    /// </summary>
    public Dictionary<string, string> GetIPRIORITY
    {
        set { ViewState["GetIPRIORITY"] = value; }
        get { return ViewState["GetIPRIORITY"] as Dictionary<string, string>; }
    }
    /// <summary>
    /// 设置的优先级
    /// </summary>
    public Dictionary<string, string> SelectIdsSaveIPRIORITY
    {
        set { ViewState["SelectIdsIPRIORITY"] = value; }
        get { return ViewState["SelectIdsIPRIORITY"] as Dictionary<string, string>; }
    }
    public void InitPage1()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdBAS_AREA.DataKeyNames = new string[] { "ID" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWin('" + BuildRequestPageURL("FrmBASE_CARGOSPACEEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmBASE_CARGOSPACEList_Mag03 + "','BASE_CARGOSPACE',600,370);return false;";//新建储位管理

        this.btnBack1.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BAR');return false;";


        //txtCDEFINE1.Attributes["onclick"] = "Show('" + ucShowArea.GetDivName + "');";
        //在新窗口打开的代码： this.OpenWin(btnNew,WebBasePage.BuildRequestPageURL("FrmBASE_CARGOSPACEEdit.aspx", SysOperation.New,""),800,600);
        //DataTable dt = BASE_OPERATORRule.GetAllOperArea(PartId);
        //if (dt.Rows.Count > 0)
        //{
        //    if (dt.Rows.Count == 1)
        //    {
        //        if (dt.Rows[0]["AREA_ID"].ToString() == "*")
        //        {
        //            ddlAllType.SelectedIndex = 1;
        //        }
        //    }
        //}

    }
    //public void GridBind1()
    //{
    //    BAS_AREAListQuery listQuery = new BAS_AREAListQuery();

    //    DataTable dtSource = listQuery.GetList(txtAreaName.Text.Trim(), txtblCw.Text.Trim(), txtCreateUser.Text.Trim(), txtFromDate.Text.Trim(), txtEndDate.Text, cbCF.SelectedValue, false, this.grdBAS_AREA1.CurrentPageIndex, this.grdBAS_AREA.PageSize);
    //    this.grdBAS_AREA.DataSource = dtSource;
    //    this.grdBAS_AREA.DataBind();
    //}


    public IQueryable<DreamTek.ASRS.DAL.BASE_AREA> GetQueryList2()
    {
        IGenericRepository<DreamTek.ASRS.DAL.BASE_AREA> pigeonBill = new GenericRepository<DreamTek.ASRS.DAL.BASE_AREA>(db);
        var caseList = from p in pigeonBill.Get()
                       orderby p.createtime ascending
                       where 1==1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtAreaName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.area_name) && x.area_name.Contains(txtAreaName.Text.Trim()));
            }
            if (txtblCw.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.handover_cargo) && x.handover_cargo.Contains(txtblCw.Text.Trim()));
            }
            if (txtCreateUser.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.createowner) && x.createowner.Contains(txtCreateUser.Text.Trim()));
            }
            if (txtFromDate.Text != string.Empty)
            {
                caseList = caseList.Where(x => x.createtime != null && x.createtime.HasValue == true
                    && SqlFunctions.DateDiff("dd", txtFromDate.Text.Trim(),x.createtime) >= 0 );
            }
            if (txtEndDate.Text != string.Empty)
            {
                caseList = caseList.Where(x => x.createtime != null && x.createtime.HasValue == true
                    && SqlFunctions.DateDiff("dd", txtEndDate.Text.Trim(),x.createtime) <= 0 );
            }
            if (cbCF.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.flag) && x.flag.Contains(cbCF.SelectedValue));
            }


        }
        return caseList;
    }

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

    public void GridBind1(string sortStr)
    {
        var caseList = GetQueryList2();

        if (caseList != null && caseList.Count() > 0)
        {
            //按照列名排序
            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }

            AspNetPager2.RecordCount = caseList.Count();
            AspNetPager2.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager2.RecordCount = 0;
            AspNetPager2.PageSize = this.PageSize;
        }
        grdBAS_AREA.DataSource = GetPageSize(caseList, PageSize, CurrendIndex2).ToList();
        grdBAS_AREA.DataBind();
    }


    private string GetKeyIDS1(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdBAS_AREA.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBAS_AREA.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected DataTable grdBAS_AREA1_GetExportToExcelSource()
    {
        //BAS_AREAListQuery listQuery = new BAS_AREAListQuery();
        //DataTable dtSource = listQuery.GetList(txtAreaName.Text.Trim(), txtblCw.Text.Trim(), txtCreateUser.Text.Trim(), txtFromDate.Text.Trim(), txtEndDate.Text, cbCF.SelectedValue, false, -1, -1);
        //return dtSource;
        return new DataTable();
    }
    protected void grdBAS_AREA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdBAS_AREA1.IsDbPager)
        //{
        //    grdBAS_AREA1.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdBAS_AREA.PageIndex = e.NewPageIndex;
        //}
    }
    protected void grdBAS_AREA_PageIndexChanged(object sender, EventArgs e)
    {
        //if(grdBAS_AREA1.IsDbPager)
        {
            this.GridBind1("");
        }
    }
    protected void grdBAS_AREA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS1(e.Row.RowIndex);
            //获取ID
            string id = this.grdBAS_AREA.DataKeys[e.Row.RowIndex][0].ToString();
            CheckBox cbo = e.Row.FindControl("chkSelect") as CheckBox;
            TextBox txtYXJ = e.Row.FindControl("txtYXJ") as TextBox;
            cbo.Attributes.Add("onclick", "SelIDCancelAll()");


            //DataTable dt = BASE_OPERATORRule.GetAllOperArea(Request.QueryString["WLBM"].ToString());
            //if (dt.Rows.Count > 0)
            //{
            //if (dt.Rows.Count == 1 && dt.Rows[0]["AREA_ID"].ToString() == "*")
            //{
            //    cbo.Checked = true;
            //}
            //else
            //{
            if (SelectIds1.ContainsKey(id))
            {
                //如果是控件处于选中状态
                cbo.Checked = true;
                if (GetIPRIORITY.ContainsKey(id))
                    txtYXJ.Text = GetIPRIORITY[id];
            }
            else cbo.Checked = false;
            //}
            //}
            //是否超发  cbCF
            //switch (e.Row.Cells[5].Text)
            //{
            //    case "0":
            //        e.Row.Cells[5].Text = "是";
            //        break;
            //    case "1":
            //        e.Row.Cells[5].Text = "否";
            //        break;
            //    default:
            //        break;
            //}         
            try
            {
                e.Row.Cells[5].Text = cbCF.Items.FindByValue(e.Row.Cells[5].Text.Trim()).Text;
            }
            catch
            {
                e.Row.Cells[5].Text = Resources.Lang.Common_ExceptionStatus; //异常状态
            }
        }

    }

    protected void dsgrdBAS_AREA_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsgrdBAS_AREA_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //if (e.ReturnValue is DataTable)
        //{
        //    if (grdBAS_AREA1.IsDbPager == false)
        //        this.grdBAS_AREA1.RowCount = ((DataTable)e.ReturnValue).Rows.Count;
        //}

    }
    /// <summary>
    /// 加载操作人的已设置区域编号
    /// </summary>
    private void LoadOPERATOR_AREA()
    {
        // ddlAllType.Attributes.Add("style", "display:none");
        //tdSZCWNAME.Attributes.Add("style", "display:none");
        //tdSetType.Attributes.Add("style", "display:none");
        //BASE_PARTRule query = new BASE_PARTRule();
        //Request.QueryString["WLBM"].ToString()
        SelectIds1 = bptQry.GetWLAreaByPartId(PartId);
        GetIPRIORITY = bptQry.GetWLAreaYXJ(PartId);
    }


    protected void btnSearch1_Click(object sender, EventArgs e)
    {
        LoadOPERATOR_AREA();
        //重新设置GridNavigator的RowCount
        //BAS_AREAListQuery listQuery = new BAS_AREAListQuery();
        //DataTable dtRowCount = listQuery.GetList(txtAreaName.Text.Trim(), txtblCw.Text.Trim(), txtCreateUser.Text.Trim(), txtFromDate.Text.Trim(), txtEndDate.Text, cbCF.SelectedValue, true, 0, 0);
        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdBAS_AREA1.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdBAS_AREA1.RowCount = 0;
        //}



        CurrendIndex2 = 1;
        AspNetPager2.CurrentPageIndex = 1;
        this.GridBind1("");
    }
    /// <summary>
    /// 保存按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave1_Click(object sender, EventArgs e)
    {

        GetSelectedIds1();
        string msg = "";

        //GetSelectedIds1();

        //1设置所选区域 2设置所有区域3取消所有区域关联
        string selType = ddlAllType2.SelectedValue;

        if (selType == "1")
        {
            //Request.QueryString["WLBM"].ToString()
            if (bptQry.SetWLArea(SelectIds1, SelectIdsSaveIPRIORITY, PartId, WmsWebUserInfo.GetCurrentUser().UserNo + "(" + WmsWebUserInfo.GetCurrentUser().UserName + ")"))
            //if (BASE_PARTRule.SetWLArea(SelectIds1, SelectIdsSaveIPRIORITY, PartId, WebUserInfo.GetCurrentUser().UserNo + "(" + WebUserInfo.GetCurrentUser().UserName + ")"))
            {
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg03;//设置成功!
                //this.AlertAndBack("FrmBASE_WL_AREA.aspx?WLBM=" + Request.QueryString["WLBM"].ToString() + BuildQueryString(SysOperation.New, PartId), msg);
                this.AlertAndBack("FrmBASE_WL_AREA.aspx?" + BuildQueryString(SYSOperation.New, PartId), msg);
            }
            else
            {
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg04;//设置失败
                Alert(msg);
            }
        }
        else if (selType == "2")
        {
            //Request.QueryString["WLBM"].ToString()
            if (bptQry.SetWLAreaQY(PartId, WmsWebUserInfo.GetCurrentUser().UserNo + "(" + WmsWebUserInfo.GetCurrentUser().UserName + ")", SelectIdsSaveIPRIORITY, true))
            {
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg03;//设置成功!
                //WLBM=" + Request.QueryString["WLBM"].ToString()
                this.AlertAndBack("FrmBASE_WL_AREA.aspx?" + BuildQueryString(SYSOperation.New, PartId), msg);
            }
            else
            {
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg04;//设置失败
                Alert(msg);
            }
        }
        else
        {
            //Request.QueryString["WLBM"].ToString()
            if (bptQry.SetCancelAllOperArea(PartId))
            {
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg03;//设置成功!
                //this.AlertAndBack("FrmBASE_WL_AREA.aspx?WLBM="+Request.QueryString["WLBM"].ToString() + BuildQueryString(SysOperation.New, PartId), msg);
                this.AlertAndBack("FrmBASE_WL_AREA.aspx?" + BuildQueryString(SYSOperation.New, PartId), msg);
            }
            else
            {
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg04;//设置失败
                Alert(msg);
            }
        }
        #region OLD

        #endregion
        //this.btnSearch1_Click(this.btnSearch1, EventArgs.Empty);
        //CQ 2014-9-10 10:30:08
        btnSave1.Style.Remove("disabled");
    }

    /// <summary>
    /// 数据邦定前 获取上一页已选中的行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdBAS_AREA_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds1();
    }
    /// <summary>
    /// 获取选择的ID
    /// </summary>
    public void GetSelectedIds1()
    {
        if (SelectIds1 == null)
        {
            SelectIds1 = new Dictionary<string, string>();
        }
        if (SelectIdsSaveIPRIORITY == null)
        {
            SelectIdsSaveIPRIORITY = new Dictionary<string, string>();
        }
        //foreach (var item in SelectIds.Keys)
        //{
        //    SelectIdsIPRIORITY.Add(item, "");
        //}
        SelectIdsSaveIPRIORITY.Clear();
        foreach (GridViewRow item in this.grdBAS_AREA.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string code = this.grdBAS_AREA.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;
                //优先级
                string yxj = (item.FindControl("txtYXJ") as TextBox).Text;
                //控件选中且集合中不存在添加
                if (cbo.Checked)
                {
                    SelectIdsSaveIPRIORITY.Add(code, yxj);
                }
                if (cbo.Checked && !SelectIds1.ContainsKey(code))
                {
                    //GetIPRIORITY.Add(code, yxj);
                    SelectIds1.Add(code, code);
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds1.ContainsKey(code))
                {
                    // GetIPRIORITY.Remove(code);

                    SelectIds1.Remove(code);
                }
            }
        }

        if (ddlAllType2.SelectedValue == "2")
        {
            SelectIdsSaveIPRIORITY.Clear();
            foreach (GridViewRow item in this.grdBAS_AREA.Rows)
            {
                string code = this.grdBAS_AREA.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;
                //优先级
                string yxj = (item.FindControl("txtYXJ") as TextBox).Text;
                SelectIdsSaveIPRIORITY.Add(code, yxj);

            }
        }
    }
}

