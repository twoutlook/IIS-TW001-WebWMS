using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BAR_FrmSeralEdit : WMSBasePage
{

    /// <summary>
    /// 规则ID
    /// </summary>
    public string OrderCode
    {
        get { return this.hiddcticketcode.Value; }
        set { this.hiddcticketcode.Value = value; }
    }

    public string OrderType
    {
        get { return this.hiddInOrOut.Value; }
        set { this.hiddInOrOut.Value = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            GetParameters();
            this.InitPage();
        }
    }


    /// <summary>
    /// 获取传入的页面参数
    /// </summary>
    private void GetParameters()
    {
        if (string.IsNullOrEmpty(this.Request.QueryString["CODE"]))
        {
            this.OrderCode = "";
        }
        else
        {
            this.OrderCode = this.Request.QueryString["CODE"];
        }

        if (string.IsNullOrEmpty(this.Request.QueryString["TYPE"]))
        {
            this.OrderType = "";
        }
        else
        {
            this.OrderType = this.Request.QueryString["TYPE"];
        }
    }

    private void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        grdBill.DataKeyNames = new string[] { "ID","cinvcode" };
        ShowData();      
    }


    public void ShowData() {
        if (this.OrderType == "IN")
        {
            var modInbill = context.INBILL.Where(x => x.cticketcode == this.OrderCode).FirstOrDefault();
            if (modInbill != null)
            {
                this.txtBillCode.Text = modInbill.cticketcode;
                var modInAsn = context.INASN.Where(x => x.id == modInbill.casnid).FirstOrDefault();
                if (modInAsn != null) {
                    this.txtAsnCode.Text = modInAsn.cticketcode;
                }
            }
        }
        else if (this.OrderType == "OUT") {
            var modOutbill = context.OUTBILL.Where(x => x.cticketcode == this.OrderCode).FirstOrDefault();
            if (modOutbill != null)
            {
                this.txtBillCode.Text = modOutbill.cticketcode;
                var modOutAsn = context.OUTASN.Where(x => x.id == modOutbill.coutasnid).FirstOrDefault();
                if (modOutAsn != null)
                {
                    this.txtAsnCode.Text = modOutAsn.cticketcode;
                }
            }   
        }
        this.txtAsnCode.Enabled = false;
        this.txtBillCode.Enabled = false;
        DataBind();
    }

    public void DataBind() {
        using (var modContext = this.context)
        {
            var queryList = from p in modContext.V_Seral_Details
                            orderby p.cinvcode descending
                            where p.BillType == this.OrderType
                            select p;

            if (!string.IsNullOrEmpty(this.OrderCode))
            {
                queryList = queryList.Where(x => x.cticketcode.Equals(this.OrderCode));
            }
            if (queryList != null)
            {
                AspNetPager1.RecordCount = queryList.Count();
                AspNetPager1.PageSize = this.PageSize;
            }
            //grdSNRule.DataSource = GetPageSize(queryList, PageSize, AspNetPager2.CurrentPageIndex).ToList();
            var listResult = GetPageSize(queryList, PageSize, AspNetPager1.CurrentPageIndex).ToList();
            grdBill.DataSource = listResult;
            grdBill.DataBind();
        }
    }


    protected void grdBill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string cinvcode = e.Row.Cells[1].Text;

            //编辑
            var linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmSeral_DEdit.aspx", SYSOperation.Modify, "" + "&TYPE=" + this.OrderType + "&CINVCODE=" + cinvcode + "&CODE=" + this.OrderCode), "", "SeralDEdit");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        DataBind();
    }
}