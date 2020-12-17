using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Configuration;
using DreamTek.ASRS.DAL;
using System.Linq.Dynamic;
using System.Data;
using DreamTek.ASRS.Business.Base;
using Resources;

public partial class ShowOperatorDiv : BaseUserControl
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
        System.Data.DataTable pdat = null;
        ZSICT1_USERQuery listQuery = new ZSICT1_USERQuery();

        pdat = listQuery.GetList(txtUserNo.Text, txtName.Text, false);

        gvReport.PageIndex = iPage;
        gvReport.DataSource = pdat;
        gvReport.DataBind();
        
    }

    //protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    //{
    //    this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
    //    Search(this.CurrendIndex);
    //}

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
        ////ZSICT1_USERQuery listQuery = new ZSICT1_USERQuery();
        ////DataTable dtRowCount = listQuery.GetList(txtUserNo.Text, txtName.Text, true, -1, -1);
        ////DataTable dtRowCount = list.GetMASTCOMPANYLIST(this.txtName.Text, true, 0, 0);
        ////this.grdNavigator.CurrentPageIndex = 0;
        ////if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        ////{
        ////    this.grdNavigator.RowCount = dtRowCount.Rows[0][0].ToInt32();
        ////}
        ////else
        ////{
        ////    this.grdNavigator.RowCount = 0;
        ////}
        //this.CurrendIndex = 1;
        //Search(1);

        //// System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        //// System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        //// this.grdNavigator.RenderControl(oHtmlTextWriter);
        ////this.DataGridNavigator.InnerHtml =   oStringWriter.ToString();
        ////if (this.grdNavigator.Controls.Count >= 6)
        ////{
        ////    this.grdNavigator.Controls[6].Visible = false;
        ////    this.grdNavigator.Controls[5].Visible = false;
        ////}

        ZSICT1_USERQuery listQuery = new ZSICT1_USERQuery();
        DataTable dtRowCount = listQuery.GetList(txtUserNo.Text, txtName.Text, true);
        //DataTable dtRowCount = list.GetMASTCOMPANYLIST(this.txtName.Text, true, 0, 0);
        AspNetPager1.CurrentPageIndex = 1;
        if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        {           
            AspNetPager1.CustomInfoHTML = Lang.FrmALLOCATEList_TotalPages + ":<b>" + "</b>";
            AspNetPager1.RecordCount = Convert.ToInt32(dtRowCount.Rows[0][0]);
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            this.AspNetPager1.RecordCount = 0;
        }
        Search(1);

        // System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        // System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        // this.grdNavigator.RenderControl(oHtmlTextWriter);
        //this.DataGridNavigator.InnerHtml =   oStringWriter.ToString();
        //if (this.AspNetPager1.RecordCount >= 6)
        //{
        //    this.grdNavigator.Controls[6].Visible = false;
        //    this.grdNavigator.Controls[5].Visible = false;
        //}



    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
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
        try
        {
            this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步 this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            Search(CurrendIndex);
        }
        catch
        {
            this.AspNetPager1.CurrentPageIndex = 1;
            Search(1);
        }
    }
    protected void gvReport_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //GridViewRow viewrow = gvReport.Rows[e.NewSelectedIndex];
        //e.Cancel = true;
        //if (GetComp)
        //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
        //               "SetControlValue('" + SetCompName + "','" + viewrow.Cells[2].Text + "(" + viewrow.Cells[2].Text + ")" + "');"
        //               + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        //else
        //{

        //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
        //                    "SetControlValue('" + SetCompName + "','" + viewrow.Cells[2].Text + "','" + SetORGCode + "','" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);

        //}
        GridViewRow viewrow = gvReport.Rows[e.NewSelectedIndex];
        e.Cancel = true;
        if (GetComp)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                       "SetControlValue('" + SetCompName + "','" + viewrow.Cells[2].Text + "(" + viewrow.Cells[2].Text + ")" + "');"
                       + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        else
        {

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                            "SetControlValue('" + SetCompName + "','" + viewrow.Cells[2].Text + "','" + SetORGCode + "','" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);

        }
    }
}
