using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;

public partial class ShowSN_Split_Div : BaseUserControl
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

    public IQueryable<v_ShowSNSplit> GetQueryList()
    {
        IGenericRepository<v_ShowSNSplit> pigeonBill = new GenericRepository<v_ShowSNSplit>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtSN.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.SN_CODE) && x.SN_CODE.Contains(txtSN.Text.Trim()));
            }
            if (txtCinvCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CINVCODE) && x.CINVCODE.Contains(txtCinvCode.Text.Trim()));
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
            grdSN.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
            grdSN.DataBind();
        }
        else
        {
            grdSN.DataSource = null;
            grdSN.DataBind();
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    [Browsable(true), Description("SN编号")]
    public string SetSN { get; set; }

    [Browsable(true), Description("料号")]
    public string SetCinvCode { get; set; }

    [Browsable(true), Description("查询料号")]
    public string SetSearchCinvCode { get; set; }

    [Browsable(true), Description("入库单编号")]
    public string SetInbillCode { get; set; }

    [Browsable(true), Description("数量")]
    public string SetQty { get; set; }

    [Browsable(true), Description("数量")]
    public string SetAllQty { get; set; }

    [Browsable(true), Description("数量")]
    public string SetTypeId { get; set; }

    [Browsable(true), Description("11")]
    public string addid { get; set; }

    [Browsable(true), Description("储位")]
    public string SetPosition { get; set; }

    [Browsable(true), Description("Div名称")]
    public string GetDivName { get { return ajaxWebSearChComp.ClientID; } }

    [Browsable(true), Description("类型，返回值类型")]
    public string itype { get; set; }

    //SN拆分只要求查询出0的数据，其他数据不允许查询，拆分界面设置为0，其他默认0,1,2
    [Browsable(true), Description("SNType")]
    public string SNType { get; set; }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search(0);
    }

    protected void grdSN_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //this.grdNavigator.CurrentPageIndex = e.NewPageIndex;
            // Search(e.NewPageIndex);
        }
        catch
        {
            //this.grdNavigator.CurrentPageIndex = 0;
            // Search(0);
        }

    }

    protected void grdSN_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow viewrow = grdSN.Rows[e.NewSelectedIndex];
        e.Cancel = true;

        if (string.IsNullOrEmpty(itype) && string.IsNullOrEmpty(SetSN) && string.IsNullOrEmpty(SetQty))
        {
            if (Session["iType"] != null)
            {
                itype = Session["iType"].ToString();
            }
            if (Session["SN"] != null)
            {
                SetSN = Session["SN"].ToString();
            }
            if (Session["QTY"] != null)
            {
                SetQty = Session["QTY"].ToString();
            }
            if (Session["ALLQTY"] != null)
            {
                SetAllQty = Session["ALLQTY"].ToString();
            }
            if (Session["TypeID"] != null)
            {
                SetTypeId = Session["TypeID"].ToString();
            }
        }

        if (itype == "0")//设置一个值
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                                                    "SelectPart('" + SetSN + "','" + viewrow.Cells[2].Text + "');"
                                                    + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        }
        else if (itype == "1")//设置两个值
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                            "SetSNSplit2('" + SetSN + "','" + viewrow.Cells[2].Text +
                            "','" + SetQty + "','" + SetAllQty + "','" + viewrow.Cells[4].Text +
                            "','" + SetTypeId + "','" + viewrow.Cells[1].Text +
                            "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        }
        else if (itype == "2")//设置四个值
        {

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                            "SetSNSplit4('" + SetSN + "','" + viewrow.Cells[2].Text + "','" + SetCinvCode + "','" + viewrow.Cells[3].Text +
                            "','" + SetQty + "','" + viewrow.Cells[4].Text +
                            "','" + SetPosition + "','" + viewrow.Cells[5].Text + "','" + addid +
                            "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);

        }
        else if (itype == "3")//设置两个值
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                            "SetSNSplit2_New('" + SetSN + "','" + viewrow.Cells[2].Text +
                            "','" + SetQty + "','" + viewrow.Cells[4].Text +
                            "','" + SetTypeId + "','" + viewrow.Cells[1].Text +
                            "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        }
    }

    protected void grdSN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //转换
            if (e.Row.Cells[1].Text.Trim() == "0")
            {
                e.Row.Cells[1].Text = "SN";
            }
            else if (e.Row.Cells[1].Text.Trim() == "1")
            {
                e.Row.Cells[1].Text = "栈板";
            }
            else if (e.Row.Cells[1].Text.Trim() == "2")
            {
                e.Row.Cells[1].Text = "箱";
            }
        }
    }
}