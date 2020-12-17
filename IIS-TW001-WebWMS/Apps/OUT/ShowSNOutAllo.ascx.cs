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

public partial class Apps_OUT_ShowSNOutAllo : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (SetConFig == "1")
            {
                grdSN.Columns[1].HeaderText = "批号";

            }

            this.txtCinvCode.Text = SetSearchCinvCode;
            this.txtCpositionCode.Text = SetSearchCposition;
            this.hiddErpCode.Value = SetErpCode;
            this.hiddIsNeedErp.Value = SetIsNeedErp;
            this.hiddIsNeedVendor.Value = SetIsNeedVendor;
            this.hiddVendorCode.Value = SetVendor;
        }
    }

    void Search(int iPage)
    {
        CurrendIndex = 1;
        Bind("");
    }

    public IQueryable<V_ShowSN_Stock> GetQueryList()
    {
        IGenericRepository<V_ShowSN_Stock> pigeonBill = new GenericRepository<V_ShowSN_Stock>(db);
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
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvCode.Text.Trim()));
            }
            if (txtCpositionCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCpositionCode.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.hiddIsNeedErp.Value))
            {
                if (this.hiddIsNeedErp.Value == "1")
                {
                    if (!string.IsNullOrEmpty(this.hiddErpCode.Value))
                    {
                        caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ERPCODE) && x.ERPCODE.Equals(this.hiddErpCode.Value));
                    }
                    else
                    {
                        caseList = caseList.Where(x => string.IsNullOrEmpty(x.ERPCODE));
                    }
                }
            }
            if (!string.IsNullOrEmpty(this.hiddIsNeedVendor.Value))
            {
                if (this.hiddIsNeedVendor.Value == "1")
                {
                    if (!string.IsNullOrEmpty(this.hiddVendorCode.Value))
                    {
                        caseList = caseList.Where(x => !string.IsNullOrEmpty(x.VENDORCODE) && x.VENDORCODE.Equals(this.hiddVendorCode.Value));
                    }
                    else
                    {
                        caseList = caseList.Where(x => string.IsNullOrEmpty(x.VENDORCODE));
                    }
                }
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

        grdSN.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdSN.DataBind();
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

    [Browsable(true), Description("查询储位")]
    public string SetSearchCposition { get; set; }

    [Browsable(true), Description("ERPCODE")]
    public string SetErpCode { get; set; }
    [Browsable(true), Description("ERPCODE")]
    public string SetIsNeedErp { get; set; }

    [Browsable(true), Description("设置显示")]
    public string SetConFig { get; set; }

    [Browsable(true), Description("入库单编号")]
    public string SetInbillCode { get; set; }

    [Browsable(true), Description("数量")]
    public string SetQty { get; set; }

    [Browsable(true), Description("数量")]
    public string SetAllQty { get; set; }

    [Browsable(true), Description("数量")]
    public string SetTypeId { get; set; }


    [Browsable(true), Description("Div名称")]
    public string GetDivName { get { return ajaxWebSearChComp.ClientID; } }

    [Browsable(true), Description("类型，返回值类型")]
    public string itype { get; set; }

    [Browsable(true), Description("类型，返回值类型")]
    public string LineQtyId { get; set; }

    //SN拆分只要求查询出0的数据，其他数据不允许查询，拆分界面设置为0，其他默认0,1,2
    [Browsable(true), Description("SNType")]
    public string SNType { get; set; }

    [Browsable(true), Description("DATECODE")]
    public string SetDateCode { get; set; }

    [Browsable(true), Description("CSO")]
    public string SetCSO { get; set; }

    [Browsable(true), Description("VENDORCODE")]
    public string SetVENDORCODE { get; set; }

    [Browsable(true), Description("Vendor")]
    public string SetVendor { get; set; }
    [Browsable(true), Description("Vendor")]
    public string SetIsNeedVendor { get; set; }

    [Browsable(true), Description("NeedQty")]
    public string SetNeedQty { get; set; }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search(0);
    }
    protected void grdSN_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

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
            if (Session["LINEQTY"] != null)
            {
                LineQtyId = Session["LINEQTY"].ToString();
            }

            if (Session["DATECODE"] != null)
            {
                SetDateCode = Session["DATECODE"].ToString();
            }

            if (Session["CSO"] != null)
            {
                SetCSO = Session["CSO"].ToString();
            }
            if (Session["Vendor"] != null)
            {
                SetVENDORCODE = Session["Vendor"].ToString();
            }

            if (Session["OutBillNeedQty"] != null)
            {
                SetNeedQty = Session["OutBillNeedQty"].ToString();
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
                            "SetSNSplitOutAllo2('" + SetSN + "','" + viewrow.Cells[2].Text +
                            "','" + SetQty + "','" + SetAllQty + "','" + viewrow.Cells[4].Text +
                            "','" + SetTypeId + "','" + viewrow.Cells[1].Text + "','" + SetDateCode + "','" + viewrow.Cells[5].Text +
                            "','" + SetCSO + "','" + viewrow.Cells[7].Text + "','" + SetVENDORCODE + "','" + viewrow.Cells[8].Text + "','" + SetNeedQty + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        }
        else if (itype == "3")//设置两个值
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                            "SetSNSplitOutAllo2_New('" + SetSN + "','" + viewrow.Cells[2].Text +
                            "','" + SetQty + "','" + viewrow.Cells[4].Text +
                            "','" + SetTypeId + "','" + viewrow.Cells[1].Text +
                            "','" + SetDateCode + "','" + viewrow.Cells[5].Text + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
        }
    }

    protected void grdSN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //转换
            if (e.Row.Cells[1].Text.Trim() == "0")
            {
                e.Row.Cells[1].Text = "箱";
            }
            else if (e.Row.Cells[1].Text.Trim() == "1")
            {
                e.Row.Cells[1].Text = "栈板";
            }
            else if (e.Row.Cells[1].Text.Trim() == "2")
            {
                e.Row.Cells[1].Text = "箱";
            }
            else if (e.Row.Cells[1].Text.Trim() == "3")
            {
                e.Row.Cells[1].Text = "批号";
            }
        }
    }
}



