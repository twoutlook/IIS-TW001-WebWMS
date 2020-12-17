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
using System.Reflection;


public partial class UserControls_ShowVendorCustomerDiv : BaseUserControl
{



    #region gridview索引
    /// <summary>
    /// 当前页数 -- 扫描器
    /// </summary>
    public int CurrendIndex2
    {
        get
        {
            if (ViewState["CurrendIndex2"] == null)
            {
                ViewState["CurrendIndex2"] = 1;
            }
            return (int)ViewState["CurrendIndex2"];
        }
        set
        {
            ViewState["CurrendIndex2"] = value;
        }
    }

    /// <summary>
    /// 当前页数 -- RGV
    /// </summary>
    public int CurrendIndex3
    {
        get
        {
            if (ViewState["CurrendIndex3"] == null)
            {
                ViewState["CurrendIndex3"] = 1;
            }
            return (int)ViewState["CurrendIndex3"];
        }
        set
        {
            ViewState["CurrendIndex3"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ////获取更新控件儿 
            //UpdatePanel mapanel = UpdatePanel1; 
            ////设置触发模式 
            //mapanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
            //AsyncPostBackTrigger tri = new AsyncPostBackTrigger();
            //if (this.hnTabIndex2.Value == "1")
            //{
            //    //trigger.ControlID = this.btnSearch.ID;       
            //    CurrendIndex2 = 1;
            //    Bind("");

            //    //tri.ControlID = "btnSearch";
            //    //tri.EventName = "Click";
            //    //mapanel.Triggers.Add(tri); 
            //}
            //else
            //{

            //    CurrendIndex3 = 1;
            //    //BindCustomer("");
            //    this.btnSearchCustomer_Click(sender, e);
            //    //tri.ControlID = "btnSearchCustomer";
            //    //tri.EventName = "Click";
            //    //mapanel.Triggers.Add(tri);   
            //}

            CurrendIndex2 = 1;
            Bind("");
            CurrendIndex3 = 1;
            this.btnSearchCustomer_Click(sender, e);
        }
        

         //this.UpdatePanel1.Triggers.Add(trigger);
         ////if (this.UpdatePanel1.IsInAsyncPostBack)
         ////{
         //  UpdatePanel1.GetType().GetMethod("Initialize", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(UpdatePanel1, null);
         //}
        
    }

    //void Search(int iPage)
    //{
    //    System.Data.DataTable pdat = null;

    //    BASE_FrmBASE_VENDORListQuery listQuery = new BASE_FrmBASE_VENDORListQuery();
    //    pdat = listQuery.GetList("", "", this.TextBox1.Text, this.txtName.Text, "", "", "", "","0", "", false, iPage, this.gvReport.PageSize);
       
    //    gvReport.PageIndex = iPage;
    //    gvReport.DataSource = pdat;
    //    gvReport.DataBind();
       

    //}

    
    private void BindCustomer(string sortStr)
    {
        IGenericRepository<BASE_CLIENT> pigeonBill = new GenericRepository<BASE_CLIENT>(db);
        var caseList = from p in pigeonBill.Get()
                       where p.cstatus == "0"
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (TxtCusNO.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cclientid) && x.cclientid.Equals(this.TxtCusNO.Text.Trim()));
            }
            if (TxtCusName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cclientname) && x.cclientname.Contains(this.TxtCusName.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }

            AspNetPager2.RecordCount = caseList.Count();
            AspNetPager2.PageSize = this.PageSize;
            GrdCustomer.DataSource = GetPageSize(caseList, PageSize, CurrendIndex3).ToList();
            GrdCustomer.DataBind();
        }
        else
        {
            GrdCustomer.DataSource = null;
            GrdCustomer.DataBind();
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }

    }

    public IQueryable<V_BASE_VENDOR> GetQueryList()
    {
        IGenericRepository<V_BASE_VENDOR> pigeonBill = new GenericRepository<V_BASE_VENDOR>(db);
        var caseList = from p in pigeonBill.Get()
                       where p.cstatus == "0"
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cvendorid) && x.cvendorid.Equals(txtCode.Text.Trim()));
            }
            if (txtName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cvendor) && x.cvendor.Contains(txtName.Text.Trim()));
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
            gvReport.DataSource = GetPageSize(caseList, PageSize, CurrendIndex2).ToList();
            gvReport.DataBind();
        }
        else
        {

            gvReport.DataSource = null;
            gvReport.DataBind();
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }

       
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex2 = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex3 = AspNetPager2.CurrentPageIndex;//索引同步        
        BindCustomer("");
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa", "refresh();", true);
       
    }
    [Browsable(true), Description("刷新的控件名")]
    public string SetCompName { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetORGCode { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetORGWorkType { get; set; }

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
        CurrendIndex2 = 1;
        AspNetPager1.CurrentPageIndex = 1;
        Bind("");
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


   
    protected void GrdCustomer_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow viewrow = GrdCustomer.Rows[e.NewSelectedIndex];
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
    protected void btnSearchCustomer_Click(object sender, EventArgs e)
    {
        CurrendIndex3 = 1;
        AspNetPager2.CurrentPageIndex = 1;
        //this.UpdatePanel1.Triggers.AsyncPostBackTrigger
        BindCustomer("");
        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa", "refresh();", true);
       
    }
}

