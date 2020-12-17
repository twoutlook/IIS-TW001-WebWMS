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



public partial class UserControls_ShowPO_Div : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }


    void Search(int iPage)
    {
        CurrendIndex = 1;
        Bind("");
    }

    public void Bind(string sortStr)
    {
        using (var context = this.db)
        {
            var caseList = from p in context.V_GetPOLine
                           select p;

            #region 查询条件
            if (txtPOLine.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.poline) && x.poline.Contains(txtPOLine.Text.Trim()));
            }
            if (txtCinvcode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));
            }
            if (SetSearchPO != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.pono) && x.pono.Contains(SetSearchPO));
            }
            if (!string.IsNullOrEmpty(SetSearchIA_ID))
            {
                //IGenericRepository<INASN_IA_D> InIA_D = new GenericRepository<INASN_IA_D>(db);
                //var InIA_D_List = (from p in InIA_D.Get()
                //                   where p.id == SetSearchIA_ID && !string.IsNullOrEmpty(p.inpo_d_ids)
                //                   select p).ToList();
                caseList = caseList.Where(x => !context.INASN_IA_D.Any(y => y.inpo_d_ids == x.ids && y.id == SetSearchIA_ID));
            }
            #endregion

            //按照列名排序
            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
            gvReport.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
            gvReport.DataBind();
        }
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

    [Browsable(true), Description("PO")]
    public string SetPO { get; set; }

    [Browsable(true), Description("查询PO")]
    public string SetSearchPO { get; set; }

    [Browsable(true), Description("POLine")]
    public string SetPOLine { get; set; }

    [Browsable(true), Description("总数量")]
    public string SetIqty { get; set; }

    [Browsable(true), Description("IDS")]
    public string SetIDS { get; set; }

    [Browsable(true), Description("查询预入库通知单ID")]
    public string SetSearchIA_ID { get; set; }

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
        //  dtRowCount = listQuery.GetPOLine(txtCinvcode.Text.Trim(), txtPOLine.Text.Trim(), SetSearchPO,SetSearchIA_ID, true, -1, -1);  
      
           
        
        
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
                       @"SetControlPOValue('" + SetPOLine + "','" + Server.HtmlDecode(viewrow.Cells[1].Text) + "','" 
                       + SetCINVCODE + "','" + Server.HtmlDecode(viewrow.Cells[2].Text) + "','"
                       + SetCINVNAME + "','" + Server.HtmlDecode(viewrow.Cells[3].Text) + "','"
                       + SetIqty + "','" + Server.HtmlDecode(viewrow.Cells[4].Text) + "','"
                       + SetIDS + "','" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
                      
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                        "SetControlPOValue('" + SetPOLine + "','" + Server.HtmlDecode(viewrow.Cells[1].Text) + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);

         }
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {

    }
}
