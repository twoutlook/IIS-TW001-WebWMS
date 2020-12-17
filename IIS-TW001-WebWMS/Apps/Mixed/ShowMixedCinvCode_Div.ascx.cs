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

public partial class Apps_Mixed_ShowMixedCinvCode_Div : BaseUserControl
{
    /// <summary>
    /// 出库单单号
    /// </summary>
    public string CerpCode
    {
        get
        {
            if (ViewState["CerpCode"] == null)
            {
                ViewState["CerpCode"] = string.Empty;
            }
            return ViewState["CerpCode"].ToString();
        }
        set
        {
            ViewState["CerpCode"] = value;
        }
    }
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

    public IQueryable<V_OutMixedList> GetQueryList()
    {
        IGenericRepository<V_OutMixedList> outmixed = new GenericRepository<V_OutMixedList>(db);
        var caseList = from p in outmixed.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (CerpCode != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Equals(CerpCode));
            }
            if (txtCinvCode.Text.Trim()!= string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvCode.Text.Trim()));
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
    public string SetCinvCode { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetCinvName { get; set; }

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


    }
    protected void gvReport_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow viewrow = gvReport.Rows[e.NewSelectedIndex];
        e.Cancel = true;
        if (GetComp)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                       "SetOutAsnValue('" + SetCinvCode + "','" + viewrow.Cells[1].Text + "(" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + ")" + "');"
                       + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        else
        {
            string cname = viewrow.Cells[2].Text.Replace("&#39;","#");
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                            "SetOutMixedValue('" + SetCinvCode + "','" + viewrow.Cells[1].Text + "','" + SetCinvName + "','" + cname + "','" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + /*"','" + SetErpCode + "','" + viewrow.Cells[2].Text + "','" + SetPalletCode + "','" + viewrow.Cells[3].Text +*/ "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);

        }
    }
}