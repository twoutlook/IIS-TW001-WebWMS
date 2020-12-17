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

public partial class UserControls_ShowPARTByInAsnIdDiv : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // btnSearch_Click(null, null);
        }
    }

    //void Search(int iPage)
    //{
    //    System.Data.DataTable pdat = null;

    //    BASE_FrmBASE_PARTListQuery listQuery = new BASE_FrmBASE_PARTListQuery();
    //    //string Asn_id = string.Empty;
    //    //string tableName = string.Empty;
    //    //if (Session["inAsn_id"] != null)
    //    //{
    //    //    hfAsnId.Value = Session["inAsn_id"].ToString();
    //    //    hfTableName.Value = "INASN_D";
    //    //}
    //    //if (Session["OutAsn_id"] != null)
    //    //{
    //    //    hfAsnId.Value = Session["OutAsn_id"].ToString();
    //    //    hfTableName.Value = "OUTASN_D";
    //    //}

    //    pdat = listQuery.GetList(this.txtCINVCODE.Text, this.txtName.Text, hfAsnId.Value.Trim(), hfErpCode.Value.Trim(), hfTableName.Value, false, iPage, this.gvReport.PageSize);
       
    //    gvReport.PageIndex = iPage;
    //    gvReport.DataSource = pdat;
    //    gvReport.DataBind();
    //}


    void Search(int iPage)
    {
        CurrendIndex = 1;
        Bind("");
    }

    public IEnumerable<V_PartInfo_BasePart> GetQueryList_BPT()
    {
        IGenericRepository<V_PartInfo_BasePart> pigeonBill = new GenericRepository<V_PartInfo_BasePart>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCINVCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCINVCODE.Text.Trim()));
            }
            if (txtName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvname) && x.cinvname.Contains(txtName.Text.Trim()));
            }
        }
        return caseList;
    }

    public IEnumerable<V_PartInfo_InAsn_D> GetQueryList_IN()
    {
        IGenericRepository<V_PartInfo_InAsn_D> pigeonBill = new GenericRepository<V_PartInfo_InAsn_D>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCINVCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCINVCODE.Text.Trim()));
            }
            if (txtName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvname) && x.cinvname.Contains(txtName.Text.Trim()));
            }
        }
        return caseList;
    }

    public IEnumerable<V_PartInfo_OutAsn_D> GetQueryList_OUT()
    {
        IGenericRepository<V_PartInfo_OutAsn_D> pigeonBill = new GenericRepository<V_PartInfo_OutAsn_D>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCINVCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCINVCODE.Text.Trim()));
            }
            if (txtName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvname) && x.cinvname.Contains(txtName.Text.Trim()));
            }
        }
        return caseList;
    }

    public IEnumerable<V_PartInfo_WIP> GetQueryList_WIP()
    {
        IGenericRepository<V_PartInfo_WIP> pigeonBill = new GenericRepository<V_PartInfo_WIP>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCINVCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCINVCODE.Text.Trim()));
            }
            if (txtName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvname) && x.cinvname.Contains(txtName.Text.Trim()));
            }
        }
        return caseList;
    }

    public void Bind(string sortStr)
    {
        if (hfTableName.Value.ToUpper().Equals("INASN_D"))
        {
            var caseList = GetQueryList_IN();

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
        else if (hfTableName.Value.ToUpper().Equals("OUTASN_D"))
        {
            var caseList = GetQueryList_OUT();

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
        }
        else {
            var caseList = GetQueryList_BPT();

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
        }

        
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }
	

    [Browsable(true), Description("刷新的控件名")]
    public string SetAsnIds { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetCINVNAME { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetCINVCODE { get; set; }

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
        //BASE_FrmBASE_PARTListQuery listQuery = new BASE_FrmBASE_PARTListQuery();
        //string Asn_id = string.Empty;
        //string tableName = string.Empty;
        if (Session["inAsn_id"] != null)
        {
            hfAsnId.Value = Session["inAsn_id"].ToString();
            hfTableName.Value = "INASN_D";
            Session.Remove("inAsn_id");
        }
        if (Session["OutAsn_id"] != null)
        {
            hfAsnId.Value = Session["OutAsn_id"].ToString();
            hfTableName.Value = "OUTASN_D";
            Session.Remove("OutAsn_id");
        }
        if (Session["Type"] != null)
        {
            hfType.Value = Session["Type"].ToString();
            if (hfType.Value=="38")
            {
                //WIP Negative Issue : 38
                hfErpCode.Value = Session["ErpCode"].ToString();
                hfTableName.Value = "wipnegativeissue_temp";
            }
            Session.Remove("ErpCode");
            Session.Remove("Type");
        }
        //DataTable dtRowCount = listQuery.GetList(this.txtCINVCODE.Text, this.txtName.Text, hfAsnId.Value.Trim(), hfErpCode.Value.Trim(), hfTableName.Value, true, -1, -1);
        ////DataTable dtRowCount = list.GetMASTCOMPANYLIST(this.txtName.Text, true, 0, 0);
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
        //if (GetComp)
        //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
        //               "SetControlValue('" + SetCINVNAME + "','" + viewrow.Cells[1].Text + "(" + viewrow.Cells[2].Text + ")" + "');"
        //               + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        //else
        //{
            
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                            "SetPartValue('" + SetCINVCODE + "','" + Server.HtmlDecode(viewrow.Cells[1].Text) + "','" + SetCINVNAME + "','" + (viewrow.Cells[2].Text) + "','" + SetAsnIds + "','" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
           
       // }
    }
}
