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
using System.Data;
using DreamTek.ASRS.Business.Mixed;
public partial class Apps_Mixed_ShowOutASNMixed_Div : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // btnSearch_Click(null, null);
            //SearchTopTen();
        }
    }

    void Search(int iPage)
    {
        CurrendIndex = 1;
        Bind("");
    }

    //public IQueryable<V_OutBill_Adj> GetQueryList()
    //{
    //    IGenericRepository<V_OutBill_Adj> pigeonBill = new GenericRepository<V_OutBill_Adj>(db);
    //    var caseList = from p in pigeonBill.Get()
    //                   select p;

    //    if (caseList != null && caseList.Count() > 0)
    //    {
    //        if (txtCerpCode.Text != string.Empty)
    //        {
    //            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtCerpCode.Text.Trim()));
    //        }

    //    }
    //    return caseList;
    //}
   
    public void Bind(string sortStr)
    {
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

        //gvReport.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        //gvReport.DataBind();
        MixedQuery query = new MixedQuery();
        int pageCount = 0;
        var dt = new DataTable();
        dt = query.MixedQueryList(txtCerpCode.Text.Trim(), CurrendIndex, PageSize, out pageCount);
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATE_Audit_Mail_Msg01 + ":<b>" + "</b>";//总页数
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;

        this.gvReport.DataSource = dt;
        this.gvReport.DataBind();
       
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

    [Browsable(true), Description("刷新的控件名")]
    public string SetErpCode { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetPalletCode { get; set; }

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
                       "SetOutAsnValue('" + SetCompName + "','" + viewrow.Cells[1].Text + "(" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + ")" + "');"
                       + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        else
        {

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                            "SetOutMixedValue('" + SetCompName + "','" + viewrow.Cells[1].Text + "','" + SetORGCode + "','" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + /*"','" + SetErpCode + "','" + viewrow.Cells[2].Text + "','" + SetPalletCode + "','" + viewrow.Cells[3].Text +*/ "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);

        }
    }
}