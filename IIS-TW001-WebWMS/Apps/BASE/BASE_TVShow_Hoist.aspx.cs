using DreamTek.ASRS.Business.SP.ProcedureModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Apps_BASE_BASE_TVShow_Hoist : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string CRANE = string.Empty;
        CRANE = this.Request.QueryString["CRANE"].ToString();
       
        this.lblTitle.Text = "提升机" + CRANE;
        this.storey_id.Text = CRANE;
        this.lblRefreshtime.Text = DreamTek.ASRS.Business.Base.CommFunction.GetConFig("100000");
        BindData();
    }

    private void BindData()
    {
        string strSQL = string.Format("Exec dbo.Proc_TVShow_SJJ '{0}'", this.storey_id.Text);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            lblLeftStatus.Text = dt.Rows[0]["Status_out"].ToString();
            lblStartPoint.Text = dt.Rows[0]["SiteCode"].ToString();
            lblCticketCode.Text = dt.Rows[0]["OutBillCode"].ToString();
            lblPalletCode.Text = dt.Rows[0]["PallatCode"].ToString();
            lblTerminalPoint.Text = dt.Rows[0]["PositionCode_out"].ToString();
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
        else
        {
            lblLeftStatus.Text = "无数据";
            lblStartPoint.Text = "无数据";
            lblCticketCode.Text = "无数据";
            lblPalletCode.Text = "无数据";
            lblTerminalPoint.Text = "无数据";
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
            lblQty6.Text = "111";         
        }
    }

    [WebMethod]
    public static string GetData(string strCRANE)
    {
        string Ret = "";

        string strSQL = string.Format("Exec dbo.Proc_TVShow_SJJ '{0}'", strCRANE);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);

        if (dt.Rows.Count > 0)
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
        return Ret;
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindData();
    }
}