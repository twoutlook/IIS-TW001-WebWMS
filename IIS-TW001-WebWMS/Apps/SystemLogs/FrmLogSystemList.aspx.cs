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

/// <summary>
/// 描述: -->FrmINASN_DList 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:47:53
/// </summary>
public partial class RD_FrmLogSystemList : WMSBasePage
{

    #region 页面属性
    /// <summary>
    /// 错误表名称
    /// </summary>
    public string TableName
    {
        get { return this.hiddTableName.Value; }
        set { this.hiddTableName.Value = value.ToString(); }
    }

    /// <summary>
    /// 错误数据的id
    /// </summary>
    public string DataId
    {
        get { return this.hiddId.Value; }
        set { this.hiddId.Value = value.ToString(); }
    }

    #endregion


    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch,EventArgs.Empty);
        }
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        switch (this.TableName)
        {
            case "OUTBILL":
                using (var modContext = context) {
                    var queryList = from p in modContext.LOG_SYSERROR
                                    where p.sovrce_no == this.DataId
                                    select p;

                    if (txtErrorMsg.Text.Trim() != string.Empty)
                    {
                        queryList = queryList.Where(x => x.errormsg.Contains(txtErrorMsg.Text.Trim()));
                    }

                    if (txtDATEFrom.Text.Trim() != string.Empty)
                    {
                        DateTime dtFrom = Convert.ToDateTime(txtDATEFrom.Text.Trim());
                        queryList = queryList.Where(x => x.createdate >= dtFrom);
                    }
                    if (txtDATETo.Text.Trim() != string.Empty)
                    {
                        DateTime dtTo = Convert.ToDateTime(txtDATETo.Text.Trim()).AddDays(1);
                        queryList = queryList.Where(x => x.createdate < dtTo);
                    }

                    AspNetPager1.RecordCount = queryList.Count();
                    AspNetPager1.PageSize = this.PageSize;
                    queryList = queryList.OrderBy(x => x.createdate);

                    List<LOG_SYSERROR> data =  GetPageSize(queryList, PageSize, CurrendIndex).ToList();

                    var source = from p in data
                                 join sp in modContext.SYS_PARAMETER.DefaultIfEmpty() on p.type.ToString() equals sp.flag_id
                                 where sp.flag_type == "LOG_SYSERROR"
                                 select new
                                 {
                                     p.id,
                                     ErrorNumber = p.errorcode,
                                     ErrorMsg = p.errormsg,
                                     CaseNO = txtCaseNO.Text,
                                     ERPCode = txtErpCode.Text,
                                     ModuleTypeName = Resources.Lang.FrmLogSystemList_Msg01, //出库管理
                                     SubModuleTypeName = Resources.Lang.FrmLogSystemList_Msg02, //出库单
                                     SourceTypeName = sp.flag_name,
                                     CREATEDATE = p.createdate,
                                     CreateUser = "WMS"
                                 };

                    this.grdINASN_D.DataSource = source.ToList();
                    this.grdINASN_D.DataBind();
                }
                break;

            case "INBILL":
                using (var modContext = context)
                {
                    var queryList = from p in modContext.LOG_SYSERROR
                                    where p.sovrce_no == this.DataId
                                    select p;

                    if (txtErrorMsg.Text.Trim() != string.Empty)
                    {
                        queryList = queryList.Where(x => x.errormsg.Contains(txtErrorMsg.Text.Trim()));
                    }

                    if (txtDATEFrom.Text.Trim() != string.Empty)
                    {
                        DateTime dtFrom = Convert.ToDateTime(txtDATEFrom.Text.Trim());
                        queryList = queryList.Where(x => x.createdate >= dtFrom);
                    }
                    if (txtDATETo.Text.Trim() != string.Empty)
                    {
                        DateTime dtTo = Convert.ToDateTime(txtDATETo.Text.Trim()).AddDays(1);
                        queryList = queryList.Where(x => x.createdate < dtTo);
                    }

                    AspNetPager1.RecordCount = queryList.Count();
                    AspNetPager1.PageSize = this.PageSize;
                    queryList = queryList.OrderBy(x => x.createdate);

                    List<LOG_SYSERROR> data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();

                    var source = from p in data
                                 join sp in modContext.SYS_PARAMETER.DefaultIfEmpty() on p.type.ToString() equals sp.flag_id
                                 where sp.flag_type == "LOG_SYSERROR"
                                 select new
                                 {
                                     p.id,
                                     ErrorNumber = p.errorcode,
                                     ErrorMsg = p.errormsg,
                                     CaseNO = txtCaseNO.Text,
                                     ERPCode = txtErpCode.Text,
                                     ModuleTypeName = Resources.Lang.FrmLogSystemList_Msg03, //入库管理
                                     SubModuleTypeName = Resources.Lang.FrmLogSystemList_Msg04, //入库单
                                     SourceTypeName = sp.flag_name,
                                     CREATEDATE = p.createdate,
                                     CreateUser = "WMS"
                                 };

                    this.grdINASN_D.DataSource = source.ToList();
                    this.grdINASN_D.DataBind();
                }
                break;
            case "OutAsn":
                using (var modContext = context)
                {
                    var queryList = from p in modContext.LOG_SYSERROR
                                    where p.sovrce_no == this.DataId
                                    select p;

                    if (txtErrorMsg.Text.Trim() != string.Empty)
                    {
                        queryList = queryList.Where(x => x.errormsg.Contains(txtErrorMsg.Text.Trim()));
                    }

                    if (txtDATEFrom.Text.Trim() != string.Empty)
                    {
                        DateTime dtFrom = Convert.ToDateTime(txtDATEFrom.Text.Trim());
                        queryList = queryList.Where(x => x.createdate >= dtFrom);
                    }
                    if (txtDATETo.Text.Trim() != string.Empty)
                    {
                        DateTime dtTo = Convert.ToDateTime(txtDATETo.Text.Trim()).AddDays(1);
                        queryList = queryList.Where(x => x.createdate < dtTo);
                    }

                    AspNetPager1.RecordCount = queryList.Count();
                    AspNetPager1.PageSize = this.PageSize;
                    queryList = queryList.OrderBy(x => x.createdate);

                    List<LOG_SYSERROR> data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();

                    var source = from p in data
                                 join sp in modContext.SYS_PARAMETER.DefaultIfEmpty() on p.type.ToString() equals sp.flag_id
                                 where sp.flag_type == "LOG_SYSERROR"
                                 select new
                                 {
                                     p.id,
                                     ErrorNumber = p.errorcode,
                                     ErrorMsg = p.errormsg,
                                     CaseNO = txtCaseNO.Text,
                                     ERPCode = txtErpCode.Text,
                                     ModuleTypeName = Resources.Lang.FrmLogSystemList_Msg01, //出库管理
                                     SubModuleTypeName = Resources.Lang.FrmLogSystemList_Msg05, //出库通知单
                                     SourceTypeName = sp.flag_name,
                                     CREATEDATE = p.createdate,
                                     CreateUser = "WMS"
                                 };

                    this.grdINASN_D.DataSource = source.ToList();
                    this.grdINASN_D.DataBind();
                }
                break;
            default:
                IGenericRepository<V_SYS_LOG> con = new GenericRepository<V_SYS_LOG>(context);
                var caseList = from p in con.Get()
                               orderby p.CreateDate descending
                               where 1 == 1
                               select p;

                if (txtErrorNumber.Text.Trim() != string.Empty)
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ErrorNumber) && x.ErrorNumber.Contains(txtErrorNumber.Text.Trim()));
                }

                if (txtCaseNO.Text.Trim() != string.Empty)
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CaseNO) && x.CaseNO.Contains(txtCaseNO.Text.Trim()));
                }

                if (txtErpCode.Text.Trim() != string.Empty)
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ERPCode) && x.ERPCode.Contains(txtErpCode.Text.Trim()));
                }

                if (txtErrorMsg.Text.Trim() != string.Empty)
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ErrorMsg) && x.ErrorMsg.Contains(txtErrorMsg.Text.Trim()));
                }

                if (ddlModule.SelectedValue != string.Empty)
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ModuleType) && x.ModuleType.Equals(ddlModule.SelectedValue));
                }
                if (ddlSubModule.SelectedValue != string.Empty)
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.SubModuleType) && x.SubModuleType.Equals(ddlSubModule.SelectedValue));
                }
                if (ddlSource.SelectedValue != string.Empty)
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.SourceType) && x.SourceType.Equals(ddlSource.SelectedValue));
                }
                DateTime d;
                if (txtDATEFrom.Text.Trim() != string.Empty && DateTime.TryParse(txtDATEFrom.Text, out d))
                {
                    caseList = caseList.Where(x => x.CreateDate.HasValue && SqlFunctions.DateDiff("dd", d, x.CreateDate) >= 0);
                }
                if (txtDATETo.Text.Trim() != string.Empty && DateTime.TryParse(txtDATETo.Text, out d))
                {
                    caseList = caseList.Where(x => x.CreateDate.HasValue && SqlFunctions.DateDiff("dd", d, x.CreateDate) <= 0);
                }
                if (txtUser.Text.Trim() != string.Empty)
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CreateUser) && x.CreateUser.Contains(txtUser.Text.Trim()));
                }

                if (caseList != null)
                {
                    AspNetPager1.RecordCount = caseList.Count();
                    AspNetPager1.PageSize = this.PageSize;
                }
                this.grdINASN_D.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
                this.grdINASN_D.DataBind();
                break;
        }
    }

    #endregion

    #region IPage 成员
   
    public void InitPage()
    {
        this.TableName = !string.IsNullOrEmpty(Request.QueryString["TableName"]) ? Request.QueryString["TableName"].ToString() : "";
        this.DataId = !string.IsNullOrEmpty(Request.QueryString["ID"]) ? Request.QueryString["ID"].ToString() : "";

        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //txtID.Text = Request.QueryString["ID"];
        //txtTableName.Text = Request.QueryString["TableName"];
        //txtDATEFrom.Visible = false;
        //txtDATETo.Visible = false;
        //lblDAUDITDATEToTo.Visible = false;
        //btnSearch.Visible = false;
        //imgDATEFrom.Visible = false;
        //imgDATETo.Visible = false;
        //lblLogsType.Visible = false;
        //ddlLogsType.Visible = false;
        //if (txtTableName.Text.Trim().Length == 0)
        //{
        //    //获取类型绑定ddl
        //    IGenericRepository<SYS_PARAMETER> con = new GenericRepository<SYS_PARAMETER>(context);
        //    var caseList = from p in con.Get()
        //                   orderby p.sortid descending
        //                   where p.flag_type == "LOG_SYSERROR"
        //                   select p;
        //    Help.DropDownListDataBind(caseList.ToList(), this.ddlLogsType, "全部", "FLAG_NAME", "FLAG_ID", "");
        //    this.lblTitle.Text = "创建日期";
        //    txtDATEFrom.Visible = true;
        //    txtDATETo.Visible = true;
        //    lblDAUDITDATEToTo.Visible = true;
        //    btnSearch.Visible = true;
        //    imgDATEFrom.Visible = true;
        //    imgDATETo.Visible = true;
        //    lblLogsType.Visible = true;
        //    ddlLogsType.Visible = true;
        //    this.txtCT_Code.Visible = false;
        //}

        IGenericRepository<SYS_LOG_Type> conn = new GenericRepository<SYS_LOG_Type>(context);
        var logTypeList = from p in conn.Get()
                          orderby p.TypeId
                          where p.GroupName.ToUpper() == "MODULETYPE"
                          select p;
        Help.DropDownListDataBind(logTypeList.ToList(), this.ddlModule, Resources.Lang.Common_ALL, "TypeName", "TypeId", ""); //全部

        var sorTypeList = from p in conn.Get()
                          orderby p.TypeId
                          where p.GroupName.ToUpper() == "SOURCETYPE"
                          select p;
        Help.DropDownListDataBind(sorTypeList.ToList(), this.ddlSource, Resources.Lang.Common_ALL, "TypeName", "TypeId", ""); //全部

        if (this.TableName == "OUTBILL" || this.TableName == "INBILL" || this.TableName == "OutAsn")
        {
            LoadOutBillInfo(this.TableName);
        }
        
    }

    #endregion

    private void LoadOutBillInfo(string tableName)
    {

        switch (tableName)
        {
            case "OUTBILL":
                if (!string.IsNullOrEmpty(this.DataId))
                {
                    var modOutBill = context.OUTBILL.Where(x => x.id == this.DataId).FirstOrDefault();
                    if (modOutBill != null)
                    {
                        txtCaseNO.Text = modOutBill.cticketcode;
                        txtCaseNO.Enabled = false;
                        txtErpCode.Text = modOutBill.cerpcode;
                        txtErpCode.Enabled = false;
                    }
                }
                this.ddlModule.SelectedValue = "5";
                break;

            case "INBILL":
                if (!string.IsNullOrEmpty(this.DataId))
                {
                    var modInBill = context.INBILL.Where(x => x.id == this.DataId).FirstOrDefault();
                    if (modInBill != null)
                    {
                        txtCaseNO.Text = modInBill.cticketcode;
                        txtCaseNO.Enabled = false;
                        txtErpCode.Text = modInBill.cerpcode;
                        txtErpCode.Enabled = false;
                    }
                }
                this.ddlModule.SelectedValue = "4";
                break;
            case "OutAsn":
                if (!string.IsNullOrEmpty(this.DataId))
                {
                    var modOutAsn = context.OUTASN.Where(x => x.id == this.DataId).FirstOrDefault();
                    if (modOutAsn != null) {
                        txtCaseNO.Text = modOutAsn.cticketcode;
                        txtCaseNO.Enabled = false;
                        txtErpCode.Text = modOutAsn.cerpcode;
                        txtErpCode.Enabled = false;
                    }
                }
                this.ddlModule.SelectedValue = "5";
                break;
        }

    }







    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorINASN_D
    }

    protected void dsGrdINASN_D_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }        
    }   

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void grdINASN_D_Sorting(object sender, GridViewSortEventArgs e)
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
  

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        string moduleId = ddlModule.SelectedValue;
        if (moduleId.IsNullOrEmpty())
        {
            ddlSubModule.Items.Clear();
            
        }
        else
        {
            ddlSubModule.Visible = true;

            IGenericRepository<SYS_LOG_Type> conn = new GenericRepository<SYS_LOG_Type>(context);
            var logTypeList = from p in conn.Get()
                              orderby p.TypeId
                              where p.GroupName.ToUpper() == "SUBMODULETYPE" && p.TypeId.StartsWith(moduleId+"_")
                              select p;
            if (logTypeList != null && logTypeList.Count() > 0)
            {
                Help.DropDownListDataBind(logTypeList.ToList(), this.ddlSubModule, Resources.Lang.Common_ALL, "TypeName", "TypeId", ""); //全部
                ddlSubModule.Enabled = true;
            }
            else {
                ddlSubModule.Enabled = false;
            }

        }
        
    }
}

