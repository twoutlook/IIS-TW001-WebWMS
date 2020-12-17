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


public partial class Apps_RD_ShowINASN_Div : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // btnSearch_Click(null, null);
            //SearchTopTen();
            Search(0);
        }
    }

    //public void SearchTopTen()
    //{
    //    RD_FrmINASNListQuery listQuery = new RD_FrmINASNListQuery();
    //    DataTable dtRowCount = listQuery.GetTopTen(true, -1, -1);
    //    this.grdNavigator.CurrentPageIndex = 0;
    //    if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
    //    {
    //        this.grdNavigator.RowCount = dtRowCount.Rows[0][0].ToInt32();
    //    }
    //    else
    //    {
    //        this.grdNavigator.RowCount = 0;
    //    }
    //    System.Data.DataTable pdat = null;

    //    pdat = listQuery.GetTopTen(false, 0, this.gvReport.PageSize);

    //    gvReport.PageIndex = 0;
    //    gvReport.DataSource = pdat;
    //    gvReport.DataBind();

    //    if (this.grdNavigator.Controls.Count >= 6)
    //    {
    //        this.grdNavigator.Controls[6].Visible = false;
    //        this.grdNavigator.Controls[5].Visible = false;
    //    }
    //}


    //void Search(int iPage)
    //{
    //    System.Data.DataTable pdat = null;

    //    RD_FrmINASNListQuery listQuery = new RD_FrmINASNListQuery();
    //    pdat = listQuery.GetShowAsn(txtCTICKETCODE.Text, "", false, iPage, this.gvReport.PageSize);

    //    gvReport.PageIndex = iPage;
    //    gvReport.DataSource = pdat;
    //    gvReport.DataBind();
    //}

    public string workType { get; set; }

    void Search(int iPage)
    {
        CurrendIndex = 1;
        Bind("");
    }

    public IQueryable<V_GetInAsn_Ten> GetQueryList()
    {
        IGenericRepository<V_GetInAsn_Ten> pigeonBill = new GenericRepository<V_GetInAsn_Ten>(db);
        var caseList = from p in pigeonBill.Get()
                       where p.CSTATUS  == "0"
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (!string.IsNullOrEmpty(this.workType)) {
                caseList = caseList.Where(x => x.WORKTYPE == this.workType);
            }
            if (txtCTICKETCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CTICKETCODE) && x.CTICKETCODE.Contains(txtCTICKETCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.workType))
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.WORKTYPE) && x.WORKTYPE.Contains(workType));
            }
            
        }
        return caseList;
    }

    public void Bind(string sortStr)
    {
        var caseList = GetQueryList();

        if (caseList != null && caseList.Count() > 0)
        {
            //按照列名排序
            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        gvReport.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        gvReport.DataBind();
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
        //RD_FrmINASNListQuery listQuery = new RD_FrmINASNListQuery();
        //DataTable dtRowCount = listQuery.GetShowAsn(txtCTICKETCODE.Text, "", true, -1, -1);
        ////DataTable dtRowCount = list.GetMASTCOMPANYLIST(this.txtName.Text, true, 0, 0);
        //this.grdNavigator.CurrentPageIndex = 0;
        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdNavigator.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdNavigator.RowCount = 0;
        //}
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
        e.Cancel = true;
        if (GetComp)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                       "SetInAsnValue('" + SetCompName + "','" + viewrow.Cells[1].Text + "(" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + ")" + "');"
                       + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        else
        {

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                            "SetInAsnValue('" + SetCompName + "','" + viewrow.Cells[1].Text + "','" + SetORGCode + "','" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);

        }
    }
}