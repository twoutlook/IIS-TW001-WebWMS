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


public partial class ShowPllentCarton_Div : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // btnSearch_Click(null, null);
        }
    }

    void Search(int iPage)
    {
        CurrendIndex = 1;
        Bind("");
    }

    public IQueryable<v_bar_pallet_m_sn> GetQueryList()
    {
        IGenericRepository<v_bar_pallet_m_sn> pigeonBill = new GenericRepository<v_bar_pallet_m_sn>(db);
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search(0);
    }
    protected void grdCartonSN_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}