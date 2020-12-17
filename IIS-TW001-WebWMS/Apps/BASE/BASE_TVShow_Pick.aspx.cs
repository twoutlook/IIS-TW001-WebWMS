using DreamTek.ASRS.Business.SP.ProcedureModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_BASE_TVShow_Pick : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string pclDress = string.Empty;
        pclDress = this.Request.QueryString["PLCDress"].ToString();
        string strF=GetFmsg(pclDress);

        string configStyle = DreamTek.ASRS.Business.Base.CommFunction.GetConFig("120004");
        if (configStyle == "2") //1=简约版 2=科技版
        {
            Response.Redirect("BASE_TVShow_PickNew.aspx?PLCDress=" + pclDress, false);
        }

        this.lblTitle.Text = "拣料位" + strF;
        this.storey_id.Text = pclDress;
        this.lblRefreshtime.Text = DreamTek.ASRS.Business.Base.CommFunction.GetConFig("100000");
        BindData();
    }
    public string GetFmsg(string pclDress)
    {
        string str="";      
        switch (pclDress)
        {
            case "A11": str = "1F01"; break;
            case "B10": str = "2F01"; break;
            case "B11": str = "2F02"; break;
            case "B14": str = "2F03"; break;
            case "B15": str = "2F04"; break;
            default: str = "";
                break;
        }     
        return str;
    }

    private void BindData()
    {
        string strSQL = string.Format("Exec dbo.Proc_TVShow_Pick '{0}'", this.storey_id.Text);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            if (!dt.Rows[0]["CSTATUS"].ToString().Trim().Contains("拣料中"))
            {
                lblLeftStatus.Text = dt.Rows[0]["CSTATUS"].ToString();
                lblErpCode.Text = dt.Rows[0]["ERPCODE"].ToString();
                lblCticketCode.Text = dt.Rows[0]["CTICKETCODE"].ToString();
                lblPalletCode.Text = dt.Rows[0]["PACKAGENO"].ToString();
                lblCpositionCode.Text = dt.Rows[0]["CPOSITIONCODE"].ToString();
                lblCinvCode1.Text = dt.Rows[0]["CINVCODE1"].ToString();
                lblQty1.Text = dt.Rows[0]["QTY1"].ToString();
                lblCinvCode2.Text = dt.Rows[0]["CINVCODE2"].ToString();
                lblQty2.Text = dt.Rows[0]["QTY2"].ToString();
                lblCinvCode3.Text = dt.Rows[0]["CINVCODE3"].ToString();
                lblQty3.Text = dt.Rows[0]["QTY3"].ToString();
                lblCinvCode4.Text = dt.Rows[0]["CINVCODE4"].ToString();
                lblQty4.Text = dt.Rows[0]["QTY4"].ToString();
                lblCinvCode5.Text = dt.Rows[0]["CINVCODE5"].ToString();
                lblQty5.Text = dt.Rows[0]["QTY5"].ToString();
                lblCinvCode6.Text = dt.Rows[0]["CINVCODE6"].ToString();
                lblQty6.Text = dt.Rows[0]["QTY6"].ToString();
            }
            else if (dt.Rows[0]["CSTATUS"].ToString().Trim().Contains("拣料中"))
            {
                lblLeftStatus_P.Text = dt.Rows[0]["CSTATUS"].ToString();
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
            lblErpCode.Text = "无数据";
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
            lblCinvCode6.Text = "无数据";
            lblQty6.Text = "无数据";
        }
    }

    [WebMethod]
    public static string GetData(string plcstr)
    {
        string Ret ="";

        string strSQL = string.Format("Exec dbo.Proc_TVShow_Pick '{0}'", plcstr);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);

        if (dt.Rows.Count > 0)
        {
            if (!dt.Rows[0]["CSTATUS"].ToString().Trim().Contains("拣料中"))
            {
                Ret += dt.Rows[0]["CSTATUS"].ToString() + "||";
                Ret += dt.Rows[0]["ERPCODE"].ToString() + "||";
                Ret += dt.Rows[0]["CTICKETCODE"].ToString() + "||";
                Ret += dt.Rows[0]["PACKAGENO"].ToString() + "||";
                Ret += dt.Rows[0]["CPOSITIONCODE"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE1"].ToString() + "||";
                Ret += dt.Rows[0]["QTY1"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE2"].ToString() + "||";
                Ret += dt.Rows[0]["QTY2"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE3"].ToString() + "||";
                Ret += dt.Rows[0]["QTY3"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE4"].ToString() + "||";
                Ret += dt.Rows[0]["QTY4"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE5"].ToString() + "||";
                Ret += dt.Rows[0]["QTY5"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE6"].ToString() + "||";
                Ret += dt.Rows[0]["QTY6"].ToString() + "||";
            }
            else if (dt.Rows[0]["CSTATUS"].ToString().Trim().Contains("拣料中"))
            {
                Ret += dt.Rows[0]["CSTATUS"].ToString() + "||";
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