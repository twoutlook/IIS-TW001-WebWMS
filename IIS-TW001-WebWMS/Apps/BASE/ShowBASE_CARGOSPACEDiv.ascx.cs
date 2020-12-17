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


public partial class UserControls_ShowBASE_CARGOSPACEDiv : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //btnSearch_Click(null, null);
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

    public string OutCinvCode
    {
        get
        {
            return this.hiddNewCinvcode.Value;
        }
        set { this.hiddNewCinvcode.Value = value; }
    }

    public string workType
    {
        get;
        set;
    }

    /// <summary>
    /// 是否暂存单据
    /// </summary>
    public string IsTemporary
    {
        get;
        set;
    }


    public string DrpIsAll
    {
        get
        {
            return this.ddlIsAll.SelectedValue;
        }
        set
        {

            this.ddlIsAll.SelectedValue = value;
            if (value == "2")
            {
                this.ddlIsAll.Enabled = false;
            }
        }
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
        // CurrendIndex = 1;
        Bind("");
    }

    public IQueryable<V_BASE_CARGOSPACE_SC> GetQueryList()
    {
        if (!string.IsNullOrEmpty(this.IsTemporary) && this.IsTemporary == "1")
        {
            IGenericRepository<V_BASE_CARGOSPACE_SC> con = new GenericRepository<V_BASE_CARGOSPACE_SC>(db);
            IGenericRepository<BASE_CARGOSPACE> conbc = new GenericRepository<BASE_CARGOSPACE>(db);
            IGenericRepository<BASE_AREA> conba = new GenericRepository<BASE_AREA>(db);

            var caseList = from p in con.Get()
                           where p.IsTemporary == "1"
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
                if (!string.IsNullOrEmpty(ddlsiteType.SelectedValue))
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ctype) && x.ctype.Equals(ddlsiteType.SelectedValue));
                }
                //料号
                if (!string.IsNullOrEmpty(hiddNewCinvcode.Value))
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Equals(hiddNewCinvcode.Value));
                }
                if (ddlIsAll.SelectedValue.Equals("1"))//没有
                {
                    caseList = caseList.Where(x => x.IDS == null || string.IsNullOrEmpty(x.IDS));
                }
                if (ddlIsAll.SelectedValue.Equals("2"))//有
                {
                    caseList = caseList.Where(x => x.IDS != null && !string.IsNullOrEmpty(x.IDS));
                }
            }
            return caseList;

        }
        else
        {
            IGenericRepository<V_BASE_CARGOSPACE_SC> con = new GenericRepository<V_BASE_CARGOSPACE_SC>(db);
            var caseList = from p in con.Get()
                           where p.IsTemporary == "0" && p.worktype == this.workType
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

                if (!string.IsNullOrEmpty(ddlsiteType.SelectedValue))
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ctype) && x.ctype.Equals(ddlsiteType.SelectedValue));
                }
                //料号
                if (!string.IsNullOrEmpty(hiddNewCinvcode.Value))
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Equals(hiddNewCinvcode.Value));
                }
                if (ddlIsAll.SelectedValue.Equals("1"))//没有
                {
                    caseList = caseList.Where(x => x.IDS == null || string.IsNullOrEmpty(x.IDS));
                }
                if (ddlIsAll.SelectedValue.Equals("2"))//有
                {
                    caseList = caseList.Where(x => x.IDS != null && !string.IsNullOrEmpty(x.IDS));
                }
            }
            return caseList;
        }
    }

    public void Bind(string sortStr)
    {

        var caseList = GetQueryList();

        if (caseList != null && caseList.Count() >= 0)
        {
            //按照列名排序
            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CurrentPageIndex = CurrendIndex;
        gvReport.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList(); //caseList.ToList();
        gvReport.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
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
        //   IsALL = false;
        //   BASE_FrmBASE_CARGOSPACEListQuery listQuery = new BASE_FrmBASE_CARGOSPACEListQuery();
        //   DataTable dtRowCount = listQuery.GetListByDiv(CinvCode, IsALL, this.TextBox1.Text, this.txtName.Text, true, -1, -1);
        //   //DataTable dtRowCount = list.GetMASTCOMPANYLIST(this.txtName.Text, true, 0, 0);
        //   this.grdNavigator.CurrentPageIndex = 0;
        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdNavigator.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdNavigator.RowCount = 0;
        //}
        CurrendIndex = 1;
        Search(0);

        // System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        // System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        // this.grdNavigator.RenderControl(oHtmlTextWriter);
        //this.DataGridNavigator.InnerHtml =   oStringWriter.ToString();
        //if (this.grdNavigator.Controls.Count >= 6)
        //{
        //    this.grdNavigator.Controls[6].Visible = false;
        //    this.grdNavigator.Controls[5].Visible = false;
        //}
    }
    protected void btnSearchALL_Click(object sender, EventArgs e)
    {
        IsALL = true;
        // BASE_FrmBASE_CARGOSPACEListQuery listQuery = new BASE_FrmBASE_CARGOSPACEListQuery();
        //DataTable dtRowCount = listQuery.GetListByDiv(CinvCode, IsALL, this.TextBox1.Text, "", true, -1, -1, ddlIsAll.SelectedValue);
        //DataTable dtRowCount = list.GetMASTCOMPANYLIST(this.txtName.Text, true, 0, 0);
        //this.grdNavigator.CurrentPageIndex = 0;
        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdNavigator.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdNavigator.RowCount = 0;
        //}
        CurrendIndex = 1;
        Search(0);

        // System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        // System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        // this.grdNavigator.RenderControl(oHtmlTextWriter);
        //this.DataGridNavigator.InnerHtml =   oStringWriter.ToString();
        //if (this.grdNavigator.Controls.Count >= 6)
        //{
        //    this.grdNavigator.Controls[6].Visible = false;
        //    this.grdNavigator.Controls[5].Visible = false;
        //}
    }
    protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //try
        //{
        //    this.grdNavigator.CurrentPageIndex = e.NewPageIndex;
        //    Search(e.NewPageIndex);
        //}
        //catch
        //{
        //    this.grdNavigator.CurrentPageIndex = 0;
        //    Search(0);
        //}

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
