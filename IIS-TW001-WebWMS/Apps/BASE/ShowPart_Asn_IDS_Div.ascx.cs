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


public partial class UserControls_ShowPart_Asn_IDS_Div : BaseUserControl
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

    //    pdat = listQuery.GetAsn_D(txtCinvCode.Text.Trim().ToUpper(), txtCinvName.Text.Trim(), SearchAsnID, SearchBillID, SearchErpCode, SearchType, false, iPage, this.gvReport.PageSize);
       
    //    gvReport.PageIndex = iPage;
    //    gvReport.DataSource = pdat;
    //    gvReport.DataBind();
    //}


    void Search(int iPage)
    {
        CurrendIndex = 1;
        Bind("");
    }

    public IEnumerable<V_Inasn_D> GetQueryList_IN()
    {
        IGenericRepository<V_Inasn_D> pigeonBill = new GenericRepository<V_Inasn_D>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCinvCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvCode.Text.Trim()));
            }
            if (txtCinvName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvname) && x.cinvname.Contains(txtCinvName.Text.Trim()));
            }
            if (SearchAsnID != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.Equals(SearchAsnID));
            }
            if (SearchBillID != string.Empty)
            {
                IGenericRepository<INASN_D> detailsBO = new GenericRepository<INASN_D>(db);
                var dList = (from p in detailsBO.Get()
                             where p.id == SearchBillID
                             select new { IDS = p.ids }).ToList();
                caseList = caseList.Where(x => !dList.Exists(y => y.IDS.Equals(x.ids)));
            }
        }
        return caseList;
    }

    public IEnumerable<V_Outasn_D> GetQueryList_OUT()
    {
        IGenericRepository<V_Outasn_D> pigeonBill = new GenericRepository<V_Outasn_D>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCinvCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvCode.Text.Trim()));
            }
            if (txtCinvName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvname) && x.cinvname.Contains(txtCinvName.Text.Trim()));
            }
            if (SearchAsnID != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.Equals(SearchAsnID));
            }
            if (SearchBillID != string.Empty)
            {
                IGenericRepository<OUTASN_D> detailsBO = new GenericRepository<OUTASN_D>(db);
                var dList = (from p in detailsBO.Get()
                             where p.id == SearchBillID
                             select new { IDS = p.ids }).ToList();
                caseList = caseList.Where(x => !dList.Exists(y => y.IDS.Equals(x.ids)));
            }
        }
        return caseList;
    }

    public void Bind(string sortStr)
    {
        if (SearchType.Equals("IN"))
        {
            var caseList = GetQueryList_IN();
            //string s =  SearchType.Equals("IN") ? "a":"b";
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
        else
        {
            var caseList = GetQueryList_OUT();
            //string s =  SearchType.Equals("IN") ? "a":"b";
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


    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    [Browsable(true), Description("品名")]
    public string SetCINVNAME { get; set; }

    [Browsable(true), Description("料号")]
    public string SetCINVCODE { get; set; }

    [Browsable(true), Description("通知单明细IDS")]
    public string SetAsn_D_IDS { get; set; }

    [Browsable(true), Description("数量")]
    public string SetIQTY { get; set; }

    [Browsable(true), Description("子项ERP单号")]
    public string SetERPLine { get; set; }

    [Browsable(true), Description("通知单ID")]
    public string SearchAsnID { get; set; }

    [Browsable(true), Description("入库单ID")]
    public string SearchBillID { get; set; }

    [Browsable(true), Description("ErpCOde")]
    public string SearchErpCode { get; set; }

    [Browsable(true), Description("类型")]
    public string SearchType { get; set; }

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


        //DataTable dtRowCount = listQuery.GetAsn_D(txtCinvCode.Text.Trim().ToUpper(), txtCinvName.Text.Trim(),SearchAsnID,SearchBillID,SearchErpCode,SearchType, true, -1, -1);
      
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
       
       //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
       //                     "SetPartValue('" + SetCINVCODE + "','" + Server.HtmlDecode(viewrow.Cells[1].Text) + "','" + SetCINVNAME + "','" + (viewrow.Cells[2].Text) + "','" + SetAsn_D_IDS + "','" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
       
       ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                       @"SetAsnPartValue('" + SetCINVCODE + "','" + Server.HtmlDecode(viewrow.Cells[1].Text) + "','"
                       + SetCINVNAME + "','" + Server.HtmlDecode(viewrow.Cells[2].Text) + "','"
                       + SetERPLine + "','" + Server.HtmlDecode(viewrow.Cells[3].Text) + "','"
                       + SetIQTY + "','" + Server.HtmlDecode(viewrow.Cells[4].Text) + "','"
                       + SetAsn_D_IDS + "','" + gvReport.DataKeys[e.NewSelectedIndex][0].ToString() + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
                      
    }
}
