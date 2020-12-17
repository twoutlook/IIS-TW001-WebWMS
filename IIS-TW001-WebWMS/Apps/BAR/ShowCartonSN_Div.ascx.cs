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

public partial class ShowCartonSN_Div : BaseUserControl
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

    public IQueryable<v_bar_pallet_m> GetQueryList()
    {
        IGenericRepository<v_bar_pallet_m> pigeonBill = new GenericRepository<v_bar_pallet_m>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtPllent.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.palletno) && x.palletno.Contains(txtPllent.Text.Trim()));
            }
            if (txtCarton.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.carton_no) && x.carton_no.Contains(txtCarton.Text.Trim()));
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

        grdCartonSN.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdCartonSN.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    [Browsable(true), Description("Div名称")]
    public string GetDivName { get { return ajaxWebSearChComp.ClientID; } }

    [Browsable(true), Description("cartonid")]
    public string cartonid { get; set; }

    [Browsable(true), Description("typeid")]
    public string typeid { get; set; }

    [Browsable(true), Description("qtyid")]
    public string qtyid { get; set; }

    [Browsable(true), Description("料号")]
    public string cinvcode { get; set; }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search(0);
    }

    protected void grdCartonSN_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow viewrow = grdCartonSN.Rows[e.NewSelectedIndex];
        e.Cancel = true;

        if (string.IsNullOrEmpty(cartonid) && string.IsNullOrEmpty(typeid) && string.IsNullOrEmpty(qtyid))
        {
            if (Session["TypeID"] != null)
            {
                typeid = Session["TypeID"].ToString();
            }
            if (Session["SN"] != null)
            {
                cartonid = Session["SN"].ToString();
            }
            if (Session["QTY"] != null)
            {
                qtyid = Session["QTY"].ToString();
            }
        }

        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                   "SetSnBack('" + cartonid + "','" + typeid + "','" + qtyid + "','" + viewrow.Cells[1].Text + "','" + viewrow.Cells[2].Text + "','" + viewrow.Cells[4].Text + "');"
                   + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
    }
}