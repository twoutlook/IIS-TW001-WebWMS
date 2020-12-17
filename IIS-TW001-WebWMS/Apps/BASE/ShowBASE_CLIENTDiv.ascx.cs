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

public partial class UserControls_ShowBASE_CLIENTDiv : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    void Search(int iPage)
    {
        CurrendIndex = 1;
        Bind("");
    }

    public IQueryable<BASE_CLIENT> GetQueryList()
    {
        IGenericRepository<BASE_CLIENT> pigeonBill = new GenericRepository<BASE_CLIENT>(db);
        var caseList = from p in pigeonBill.Get()
                       where p.cstatus == "0"
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cclientid) && x.cclientid.Contains(txtCode.Text.Trim()));
            }
            if (txtName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cclientname) && x.cclientname.Contains(txtName.Text.Trim()));
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
     //   BASE_FrmBASE_CLIENTListQuery listQuery = new BASE_FrmBASE_CLIENTListQuery();
     //   DataTable dtRowCount = listQuery.GetList("","",this.TextBox1.Text, this.txtName.Text, "", "", "", "", "", "","",true, -1, -1);
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
        Search(0);

       // System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
       // System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
       // this.grdNavigator.RenderControl(oHtmlTextWriter);
       //this.DataGridNavigator.InnerHtml =   oStringWriter.ToString();
        //this.grdNavigator.Controls[6].Visible = false;
        //this.grdNavigator.Controls[5].Visible = false;
        
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
                       "SetControlValue('" + SetCompName + "','" + viewrow.Cells[1].Text + "(" + viewrow.Cells[2].Text + ")" + "');"
                       + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        else
        {
            
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                            "SetControlValue('" + SetCompName + "','" + viewrow.Cells[1].Text + "','" + SetORGCode + "','" + viewrow.Cells[2].Text + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
           
        }
    }
}
