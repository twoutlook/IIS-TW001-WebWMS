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
using Resources;

public partial class ShowBOM_D_CinvCode_Div : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //btnSearch_Click(null, null);
        }
    }

    void Search(int iPage)
    {
        CurrendIndex = 1;
        Bind("");
    }

    public IQueryable<V_PARTUNIONBOM> GetQueryList()
    {
        IGenericRepository<V_PARTUNIONBOM> pigeonBill = new GenericRepository<V_PARTUNIONBOM>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCinvcode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CPARTNUMBER) && x.CPARTNUMBER.Contains(txtCinvcode.Text.Trim()));
            }
            if (txtCinvName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CPARTNAME) && x.CPARTNAME.Contains(txtCinvName.Text.Trim()));
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
    public string SetCINVBARCODE { get; set; }

    [Browsable(true), Description("品名")]
    public string SetCINVNAME { get; set; }

    [Browsable(true), Description("料号")]
    public string SetCINVCODE { get; set; }

    [Browsable(true), Description("类型")]
    public string SetType { get; set; }

    [Browsable(true), Description("类型")]
    public string SearchType { get; set; }

    Dictionary<string, string> _SetTypeCode = new Dictionary<string, string>();
    /// <summary>
    /// 获取
    /// </summary>
    public Dictionary<string, string> SetTypeCode
    {
        get
        {
            return _SetTypeCode;
        }
        set
        {
            _SetTypeCode = value;
        }
    }
    
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
        //DataTable dtRowCount=null;
        //if (dtRowCount == null)
        //    dtRowCount = Tool.GetBOM_D_cinvCode(txtCinvcode.Text.Trim(), txtCinvName.Text.Trim(), SearchType, true, -1, -1);
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
        try
        {
            GridViewRow viewrow = gvReport.Rows[e.NewSelectedIndex];
            e.Cancel = true;
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                                "SetControlValue('"
                                + SetCINVCODE + "','" + Server.HtmlDecode(viewrow.Cells[1].Text) + "','"
                                + SetCINVNAME + "','" + Server.HtmlDecode(viewrow.Cells[2].Text) + "','"
                                + SetType + "','" + Server.HtmlDecode(viewrow.Cells[3].Text) + "','"
                                + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);

        }
        catch (Exception se)
        {
            Alert(Resources.Lang.ShowBOM_D_CinvCode_Div_Msg01+ "：" + se.Message);//ShowBOM_D_CinvCode_Div.ascx页面gvReport_SelectedIndexChanging方法发生异常！ 错误讯息
        }
    }

}
