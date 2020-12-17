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


public partial class UserControls_ShowPOIA_Div : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //btnSearch_Click(null, null);
        }
    }

    //void Search(int iPage)
    //{
    //    INPOQuery listQuery = new INPOQuery();

    //    System.Data.DataTable pdat = null;

    //    if (pdat == null)
    //    {
    //       pdat = listQuery.GetPO(txtPONO.Text.Trim(),false, iPage, this.gvReport.PageSize);
           
    //    }
           
    //    gvReport.PageIndex = iPage;
    //    gvReport.DataSource = pdat;
    //    gvReport.DataBind();


    //}


    void Search(int iPage)
    {
        CurrendIndex = 1;
        Bind("");
    }

    public IQueryable<V_GetPOList> GetQueryList()
    {
        IGenericRepository<V_GetPOList> pigeonBill = new GenericRepository<V_GetPOList>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtPONO.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.pono) && x.pono.Contains(txtPONO.Text.Trim()));
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


    [Browsable(true), Description("PO")]
    public string SetPO { get; set; }

    [Browsable(true), Description("供应商")]
    public string SetVendorCode { get; set; }

    [Browsable(true), Description("供应商名称")]
    public string SetVendorName { get; set; }

    [Browsable(true), Description("ID")]
    public string SetPO_ID { get; set; }

    [Browsable(true), Description("查询PO")]
    public string SetSearchPO { get; set; }


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
        //INPOQuery listQuery = new INPOQuery();
        //DataTable dtRowCount = null;
      
          
        //if (dtRowCount == null)
        //{
        //    dtRowCount = listQuery.GetPO(txtPONO.Text.Trim(), true, -1, -1);  
        //}
           
        
        
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
        GridViewRow viewrow = gvReport.Rows[e.NewSelectedIndex];
        e.Cancel = true;
        if (GetComp)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                      @"SetControlValue('" + SetPO + "','" + Server.HtmlDecode(viewrow.Cells[1].Text) + "','"
                      + SetVendorCode + "','" + Server.HtmlDecode(viewrow.Cells[2].Text) + "','"
                      + SetVendorName + "','" + Server.HtmlDecode(viewrow.Cells[3].Text) + "','"
                      + SetPO_ID + "','" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
             
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                        "SetControlValue('" + SetPO + "','" + Server.HtmlDecode(viewrow.Cells[1].Text) + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);

         }
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {

    }
}
