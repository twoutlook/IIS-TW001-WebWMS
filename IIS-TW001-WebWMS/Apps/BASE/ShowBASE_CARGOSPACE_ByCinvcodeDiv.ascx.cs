using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using Resources;

public partial class UserControls_ShowBASE_CARGOSPACE_ByCinvcodeDiv : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //btnSearch_Click(null, null);
        }
    }

    [Browsable(true), Description("刷新的控件名")]
    public string SetCompName { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetORGCode { get; set; }

    [Browsable(true), Description("Div名称")]
    public string GetDivName { get { return ajaxWebSearChComp.ClientID; } }

    [Browsable(true), Description("是否显示企业名称(企业代码)")]
    public bool GetComp { get; set; }

    /// <summary>
    /// 料号
    /// </summary>
    [Browsable(true), Description("料号")]
    public string PartNo { get { return hfPartNo.Value; } set { this.hfPartNo.Value = value; } }

    /// <summary>
    /// 各单据的 GUID
    /// </summary>
    public string Id { get { return hfId.Value; } set { this.hfId.Value = value; } }

    /// <summary>
    /// 入库类型
    /// </summary>
    public string InType { get { return hfInType.Value; } set { this.hfInType.Value = value; } }

    /// <summary>
    /// 贸易代码
    /// </summary>
    public string TradeCode { get { return hfTradeCode.Value; } set { this.hfTradeCode.Value = value; } }

    /// <summary>
    /// 币别
    /// </summary>
    public string Currency { get { return hfCurrency.Value; } set { this.hfCurrency.Value = value; } }

    void Search(int iPage)
    {
        System.Data.DataTable pdat = null;

        //BASE_FrmBASE_CARGOSPACEListQuery listQuery = new BASE_FrmBASE_CARGOSPACEListQuery();
        //if (Session["CINVCODE"] != null)
        //{
        //    hfCINVCODE.Value = Session["CINVCODE"].ToString();
        //}
        
        //pdat = listQuery.GetList(PartNo, InType, TradeCode, Currency, this.TextBox1.Text, this.txtName.Text, false, iPage, this.gvReport.PageSize);
       
        gvReport.PageIndex = iPage;
        gvReport.DataSource = pdat;
        gvReport.DataBind();
    }
       

    void Alert(string Message)
    {
        Page.ClientScript.RegisterClientScriptBlock(GetType(), "f1", "<script>alert('" + Message + "！');</script>");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //BASE_FrmBASE_CARGOSPACEListQuery listQuery = new BASE_FrmBASE_CARGOSPACEListQuery();
        //if (Session["CINVCODE"] != null)
        //{
        //    hfCINVCODE.Value = Session["CINVCODE"].ToString();
        //}
        //用Cookie传参
        //获取Cookie对像
        HttpCookie cookie = Request.Cookies["CARGOSPACE_Query"];
        if (cookie != null)
        {
            //Id = cookie.Values["Id"];
            InType = cookie.Values["InType"];
            TradeCode = cookie.Values["TradeCode"];
            Currency = cookie.Values["Currency"];
            //清除Cookie参数
            Response.Cookies.Remove("CARGOSPACE_Query");
            cookie.Expires = DateTime.Now.AddDays(-1);
        }//CARGOSPACE_Query_PratNo
        HttpCookie cookiePratNo = Request.Cookies["CARGOSPACE_Query_PratNo"];
        if (cookiePratNo != null)
        {
            PartNo = cookiePratNo.Values["PartNo"];
            Response.Cookies.Remove("CARGOSPACE_Query_PratNo");
            cookiePratNo.Expires = DateTime.Now.AddDays(-1);
        }
        DataTable dtRowCount = null;// listQuery.GetList(PartNo, InType, TradeCode, Currency, this.TextBox1.Text, this.txtName.Text, true, -1, -1);
        //DataTable dtRowCount = list.GetMASTCOMPANYLIST(this.txtName.Text, true, 0, 0);
        //this.grdNavigator.CurrentPageIndex = 0;
        // if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        // {
        //     this.grdNavigator.RowCount = dtRowCount.Rows[0][0].ToInt32();
        // }
        // else
        // {
        //     this.grdNavigator.RowCount = 0;
        // }
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
        try
        {
            //this.grdNavigator.CurrentPageIndex = e.NewPageIndex;
            Search(e.NewPageIndex);
        }
        catch
        {
            //this.grdNavigator.CurrentPageIndex = 0;
            Search(0);
        }
        
    }

    protected void gvReport_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow viewrow = gvReport.Rows[e.NewSelectedIndex];
        e.Cancel = true;
        if (GetComp)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                       "SetControlValueByCinvcode('" + SetCompName + "','" + viewrow.Cells[1].Text + "(" + viewrow.Cells[2].Text + ")" + "');"
                       + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        else
        {
            
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                            "SetControlValueByCinvcode('" + SetCompName + "','" + viewrow.Cells[2].Text + "','" + SetORGCode + "','" + viewrow.Cells[1].Text + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
           
        }
    }
}
