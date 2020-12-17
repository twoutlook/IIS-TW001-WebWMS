using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BAR_FrmSeral_DEdit : WMSBasePage
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

    public string User {
        get { return this.hiddUser.Value; }
        set { this.hiddUser.Value = value; }
    }

    public string CinvCode {
        get { return this.hiddCinvCode.Value; }
        set { this.hiddCinvCode.Value = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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

        if (string.IsNullOrEmpty(this.Request.QueryString["CINVCODE"]))
        {
            this.CinvCode = "";
        }
        else
        {
            this.CinvCode = this.Request.QueryString["CINVCODE"];
        }

        this.User = WmsWebUserInfo.GetCurrentUser().UserNo;
    }

    private void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('SeralDEdit');return false;";
        this.txtBillCode.Text = this.OrderCode;
        this.txtBillCode.Enabled = false;
        this.txtCinvcode.Text = this.CinvCode;
        this.txtCinvcode.Enabled = false;
        this.txtTotalCount.Enabled = false;
        this.txtSavedCount.Enabled = false;
        if (this.OrderType == "IN")
        {
            var modInBill = context.INBILL.Where(x => x.cticketcode == this.OrderCode).FirstOrDefault();
            if (modInBill != null)
            {
                //该物料需要扫的序列号数
                decimal NeedCount = context.INBILL_D.Where(x => x.cinvcode == this.CinvCode && x.id == modInBill.id).Sum(x => x.iquantity.Value);
                this.txtTotalCount.Text = NeedCount.ToString("F2");
            }
        }
        else if (this.OrderType == "OUT")
        {
            var modOutBill = context.OUTBILL.Where(x => x.cticketcode == this.OrderCode).FirstOrDefault();
            if (modOutBill != null)
            {
                //该物料需要扫的序列号数
                decimal NeedCount = context.OUTBILL_D.Where(x => x.cinvcode == this.CinvCode && x.id == modOutBill.id).Sum(x => x.iquantity);
                this.txtTotalCount.Text = NeedCount.ToString("F2");
            }
        }
    }

}