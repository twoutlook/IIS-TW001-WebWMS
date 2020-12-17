using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using DreamTek.ASRS.Business.Allocate;
using System.Data;

public partial class Apps_ALLOCATE_ShowPK_CARGOSPACEDiv : BaseUserControl
{
    /// <summary>
    /// 仓库类型 0:平库   1:立库
    /// </summary>
    public string WType
    {
        get
        {
            if (ViewState["WType"] == null)
            {
                ViewState["WType"] = string.Empty;
            }
            return ViewState["WType"].ToString();
        }
        set
        {
            ViewState["WType"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //btnSearch_Click(null, null);
              //栈板上是否有货
            Help.DropDownListDataBind(SysParameterList.GetList("", "", "ISHasCinv", false, -1, -1), this.ddlIsAll, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
            
                //储位种类
            Help.DropDownListDataBind(SysParameterList.GetList("", "", "CARGOSPACETYPE", false, -1, -1), this.ddlCtype, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        }
    }

    public override void DataBind()
    {

        base.DataBind();
    }

    public string CinvCode
    {
        get
        {
            //Control con = this.Parent.FindControl("txtCINVCODE");
            //if (con is TextBox)
            //{
            //    return ((TextBox)this.Parent.FindControl("txtCINVCODE")).Text;
            //}
            //else
            //{
            //    return "";
            //}
            return this.txtCinvCode.Value;
        }
        set { this.txtCinvCode.Value = value; }
    }
    public bool IsALL
    {
        get
        {
            if (ViewState["IsALL"] == null)
            {
                ViewState["IsALL"] = false;
            }
            return (bool)ViewState["IsALL"];
        }
        set { ViewState["IsALL"] = value; }
    }

    void Search(int iPage)
    {
        CurrendIndex = 1;
        Bind("");
    }

    public IQueryable<V_BASE_CARGOSPACE_PK> GetQueryList()
    {
        IGenericRepository<V_BASE_CARGOSPACE_PK> pigeonBill = new GenericRepository<V_BASE_CARGOSPACE_PK>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCode.Text.Trim()));
            }
            if (txtName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cposition) && x.cposition.Contains(txtName.Text.Trim()));
            }

            if (ddlIsAll.SelectedValue.Equals("1"))//没有
            {
                caseList = caseList.Where(x => x.IDS == null || string.IsNullOrEmpty(x.IDS));
            }
            if (ddlIsAll.SelectedValue.Equals("2"))//有
            {
                caseList = caseList.Where(x => x.IDS != null && !string.IsNullOrEmpty(x.IDS));
            }
            if (!string.IsNullOrEmpty(WType))
            {
                caseList = caseList.Where(x =>x.wtype!=null  && x.wtype.Equals(WType));
            }
            if (!string.IsNullOrEmpty(ddlCtype.SelectedValue))
            {
                caseList = caseList.Where(x => x.ctype.Equals(ddlCtype));
            }
        }
        return caseList;
    }

    public void Bind(string sortStr)
    {
        int pageCount = 0;
        AllocateQuery query = new AllocateQuery();
        DataTable dt =new DataTable();
        if (SetCompName.Contains("txtCPOSITION"))
        {
           ddlIsAll.SelectedValue = "2";       
           dt= query.GetAllocateList(txtCode.Text.Trim(), txtName.Text.Trim(), ddlIsAll.SelectedValue, WType,ddlCtype.SelectedValue,CinvCode,"0", CurrendIndex, PageSize, out pageCount);           
        }
        else{
            //目的储位直接选择储位上没有值的选项。
            ddlIsAll.SelectedValue = "1";
            //ddlIsAll.Enabled = false;
            dt = query.GetAllocateList(txtCode.Text.Trim(), txtName.Text.Trim(), ddlIsAll.SelectedValue, WType, ddlCtype.SelectedValue, "","1", CurrendIndex, PageSize, out pageCount);
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;
        AspNetPager1.CurrentPageIndex = CurrendIndex;
        gvReport.DataSource = dt;
        gvReport.DataBind();
        //var caseList = GetQueryList();

        //if (caseList != null && caseList.Count() > 0)
        //{
        //    //按照列名排序
        //    if (!string.IsNullOrEmpty(sortStr))
        //    {
        //        caseList = caseList.OrderBy(sortStr);
        //    }

        //    AspNetPager1.RecordCount = caseList.Count();
        //    AspNetPager1.PageSize = this.PageSize;
        //}
        //var list = caseList.ToList();
        //gvReport.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        //gvReport.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    [Browsable(true), Description("刷新的控件名")]
    public string SetCompName { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetORGCode { get; set; }

    [Browsable(true), Description("Div名称")]
    public string GetDivName { get { return ajaxWebSearChComp.ClientID; } }

    [Browsable(true), Description("是否显示企业名称(企业代码)")]
    public bool GetComp { get; set; }

    void Alert(string Message)
    {
        Page.ClientScript.RegisterClientScriptBlock(GetType(), "f1", "<script>alert('" + Message + "！');</script>");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search(0);
    }
    protected void btnSearchALL_Click(object sender, EventArgs e)
    {
        IsALL = true;
        Search(0);
    }
    protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvReport_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow viewrow = gvReport.Rows[e.NewSelectedIndex];
        e.Cancel = false;
        if (GetComp)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                       "SetCARGOSPACEValue('" + SetCompName + "','" + viewrow.Cells[1].Text + "(" + viewrow.Cells[2].Text + ")" + "');"
                       + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        else
        {

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                            "SetCARGOSPACEValue2('" + SetCompName + "','" + viewrow.Cells[2].Text + "','" + SetORGCode + "','" + viewrow.Cells[1].Text + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);

        }
    }
}