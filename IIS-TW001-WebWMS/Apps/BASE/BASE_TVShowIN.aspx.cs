using DreamTek.ASRS.Business.SP.ProcedureModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_BASE_TVShowIN : System.Web.UI.Page
{
    string storey = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断电子看板，1=简约版 2=科技版
        string strSQL = @"SELECT t.config_value  FROM dbo.SYS_CONFIG t WHERE t.code='120004' ORDER BY t.code";
        var configVal = DBHelp.ExecuteScalar(strSQL);

        if (configVal != null && configVal == "2")
        {
            Server.Transfer("BASE_TVShowINNew.aspx");
        }
        else
        {
            this.storey = this.Request.QueryString["Storey"].ToString();
            this.lblTitle.Text = "入库口" + storey + "F01";
            this.storey_id.Text = storey;
            this.lblRefreshtime.Text = DreamTek.ASRS.Business.Base.CommFunction.GetConFig("100000");
            BindData();
        }
    }
    
    private void BindData()
    {
        string strSQL = string.Format("Exec dbo.Proc_TVShow_IN '{0}'", storey);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            if (!dt.Rows[0]["Status_out"].ToString().Trim().Contains("出库中"))
            {
                lblLeftStatus.Text = dt.Rows[0]["Status_out"].ToString();
                lblSiteCode.Text = dt.Rows[0]["SiteCode"].ToString();
                lblCticketCode.Text = dt.Rows[0]["OutBillCode"].ToString();
                lblPalletCode.Text = dt.Rows[0]["PallatCode"].ToString();
                lblCpositionCode.Text = dt.Rows[0]["PositionCode_out"].ToString();
                lblCinvCode1.Text = dt.Rows[0]["WL1_out"].ToString();
                lblQty1.Text = dt.Rows[0]["OutBillQty1"].ToString();
                lblCinvCode2.Text = dt.Rows[0]["WL2_out"].ToString();
                lblQty2.Text = dt.Rows[0]["OutBillQty2"].ToString();
                lblCinvCode3.Text = dt.Rows[0]["WL3_out"].ToString();
                lblQty3.Text = dt.Rows[0]["OutBillQty3"].ToString();
                lblCinvCode4.Text = dt.Rows[0]["WL4_out"].ToString();
                lblQty4.Text = dt.Rows[0]["OutBillQty4"].ToString();
                lblCinvCode5.Text = dt.Rows[0]["WL5_out"].ToString();
                lblQty5.Text = dt.Rows[0]["OutBillQty5"].ToString();
                lblCinvCode6.Text = dt.Rows[0]["WL6_out"].ToString();
                lblQty6.Text = dt.Rows[0]["OutBillQty6"].ToString();
            }
            else if (dt.Rows[0]["Status_out"].ToString().Trim().Contains("出库中"))
            {
                lblLeftStatus_P.Text = dt.Rows[0]["Status_out"].ToString();
                lblPallteCode_P.Text = dt.Rows[0]["PACKAGENO"].ToString();
                lblCticketcode_P.Text = dt.Rows[0]["CTICKETCODE"].ToString();
                lblPostitionCode_P.Text = dt.Rows[0]["CPOSITIONCODE"].ToString();
                lblErpCode1_P.Text = dt.Rows[0]["ERPCODE1"].ToString();
                lblErpCode2_P.Text = dt.Rows[0]["ERPCODE2"].ToString();
                lblErpCode3_P.Text = dt.Rows[0]["ERPCODE3"].ToString();
                lblErpCode4_P.Text = dt.Rows[0]["ERPCODE4"].ToString();
                lblCinvCode1_P.Text = dt.Rows[0]["CINVCODE1"].ToString();
                lblCinvCode2_P.Text = dt.Rows[0]["CINVCODE2"].ToString();
                lblCinvCode3_P.Text = dt.Rows[0]["CINVCODE3"].ToString();
                lblCinvCode4_P.Text = dt.Rows[0]["CINVCODE4"].ToString();
                lblQty1_P.Text = dt.Rows[0]["QTY1"].ToString();
                lblQty2_P.Text = dt.Rows[0]["QTY2"].ToString();
                lblQty3_P.Text = dt.Rows[0]["QTY3"].ToString();
                lblQty4_P.Text = dt.Rows[0]["QTY4"].ToString();
            }
        }
        else
        {
            lblLeftStatus.Text = "无数据";
            lblSiteCode.Text = "无数据";
            lblCticketCode.Text = "无数据";
            lblPalletCode.Text = "无数据";
            lblCpositionCode.Text = "无数据";
            lblCinvCode1.Text = "无数据";
            lblQty1.Text = "无数据";
            lblCinvCode2.Text = "无数据";
            lblQty2.Text = "无数据";
            lblCinvCode3.Text = "无数据";
            lblQty3.Text = "无数据";
            lblCinvCode4.Text = "无数据";
            lblQty4.Text = "无数据";
            lblCinvCode5.Text = "无数据";
            lblQty5.Text = "无数据";
            lblCinvCode6.Text = "";
            lblQty6.Text = "";
        }
    }

    [WebMethod]
    public static string GetData(string Storey)
    {
        string Ret = "";

        string strSQL = string.Format("Exec dbo.Proc_TVShow_IN '{0}'", Storey);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);

        if (dt.Rows.Count > 0)
        {
            if (!dt.Rows[0]["Status_out"].ToString().Trim().Contains("出库中"))
            {
                Ret += dt.Rows[0]["Status_out"].ToString() + "||";
                Ret += dt.Rows[0]["SiteCode"].ToString() + "||";
                Ret += dt.Rows[0]["OutBillCode"].ToString() + "||";
                Ret += dt.Rows[0]["PallatCode"].ToString() + "||";
                Ret += dt.Rows[0]["PositionCode_out"].ToString() + "||";
                Ret += dt.Rows[0]["WL1_out"].ToString() + "||";
                Ret += dt.Rows[0]["OutBillQty1"].ToString() + "||";
                Ret += dt.Rows[0]["WL2_out"].ToString() + "||";
                Ret += dt.Rows[0]["OutBillQty2"].ToString() + "||";
                Ret += dt.Rows[0]["WL3_out"].ToString() + "||";
                Ret += dt.Rows[0]["OutBillQty3"].ToString() + "||";
                Ret += dt.Rows[0]["WL4_out"].ToString() + "||";
                Ret += dt.Rows[0]["OutBillQty4"].ToString() + "||";
                Ret += dt.Rows[0]["WL5_out"].ToString() + "||";
                Ret += dt.Rows[0]["OutBillQty5"].ToString() + "||";
                Ret += dt.Rows[0]["WL6_out"].ToString() + "||";
                Ret += dt.Rows[0]["OutBillQty6"].ToString() + "||";
            }
            else if (dt.Rows[0]["Status_out"].ToString().Trim().Contains("出库中"))
            {
                Ret += dt.Rows[0]["Status_out"].ToString() + "||";
                Ret += dt.Rows[0]["PACKAGENO"].ToString() + "||";
                Ret += dt.Rows[0]["CTICKETCODE"].ToString() + "||";
                Ret += dt.Rows[0]["CPOSITIONCODE"].ToString() + "||";
                Ret += dt.Rows[0]["ERPCODE1"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE1"].ToString() + "||";
                Ret += dt.Rows[0]["QTY1"].ToString() + "||";
                Ret += dt.Rows[0]["ERPCODE2"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE2"].ToString() + "||";
                Ret += dt.Rows[0]["QTY2"].ToString() + "||";
                Ret += dt.Rows[0]["ERPCODE3"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE3"].ToString() + "||";
                Ret += dt.Rows[0]["QTY3"].ToString() + "||";
                Ret += dt.Rows[0]["ERPCODE4"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE4"].ToString() + "||";
                Ret += dt.Rows[0]["QTY4"].ToString() + "||";  
            }

        }
        return Ret.Substring(0, Ret.Length - 2);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindData();
    }
}