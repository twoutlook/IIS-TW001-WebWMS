using DreamTek.ASRS.Business.SP.ProcedureModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_BASE_TVShow : System.Web.UI.Page
{                  
    string LineID = string.Empty;
    string SiteID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string LineAndSite = this.Request.QueryString["LineAndSite"].ToString();
        this.LineID = LineAndSite.Split(',')[0].ToString();
        this.SiteID = LineAndSite.Split(',')[1].ToString();
        this.line_ID.Text = LineID;
        this.site_ID.Text = SiteID;
    
      BindGD();
    }
    public void BindGD()
    {                                             
         
        string strSQL1 = string.Format("Exec dbo.Proc_TVShow '{0}','{1}'", SiteID, LineID);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL1);

        if (dt.Rows.Count > 0)
        {   
            labLineID.Text = dt.Rows[0]["LineName"].ToString();
            labSiteID.Text = dt.Rows[0]["SiteName"].ToString();
            labPallatCodeCount.Text = dt.Rows[0]["PallatCodeCount"].ToString();
            lblLeftStatus.Text = dt.Rows[0]["Status_out"].ToString();
            labERPCode.Text = dt.Rows[0]["ERPCode"].ToString();
            labBillCode.Text = dt.Rows[0]["OutBillCode"].ToString();
            labPositionCode.Text = dt.Rows[0]["PositionCode_out"].ToString();
            labPallatCode.Text = dt.Rows[0]["PallatCode"].ToString();
            labCinvCode1.Text = dt.Rows[0]["WL1_out"].ToString();
            labOutBillQty1.Text = dt.Rows[0]["OutBillQty1"].ToString();
            labOutJHQty1.Text = dt.Rows[0]["OutJHQty1"].ToString();
            labOutFKQty1.Text = dt.Rows[0]["OutFKQty1"].ToString();
            labCinvCode2.Text = dt.Rows[0]["WL2_out"].ToString();
            labOutBillQty2.Text = dt.Rows[0]["OutBillQty2"].ToString();
            labOutJHQty2.Text = dt.Rows[0]["OutJHQty2"].ToString();
            labOutFKQty2.Text = dt.Rows[0]["OutFKQty2"].ToString();
            labCinvCode3.Text = dt.Rows[0]["WL3_out"].ToString();
            labOutBillQty3.Text = dt.Rows[0]["OutBillQty3"].ToString();
            labOutJHQty3.Text = dt.Rows[0]["OutJHQty3"].ToString();
            labOutFKQty3.Text = dt.Rows[0]["OutFKQty3"].ToString();
            labCinvCode4.Text = dt.Rows[0]["WL4_out"].ToString();
            labOutBillQty4.Text = dt.Rows[0]["OutBillQty4"].ToString();
            labOutJHQty4.Text = dt.Rows[0]["OutJHQty4"].ToString();
            labOutFKQty4.Text = dt.Rows[0]["OutFKQty4"].ToString();
            labOutFB1.Text = dt.Rows[0]["P_LineId1"].ToString() + dt.Rows[0]["P_StnNo1"].ToString() + dt.Rows[0]["P_PlCount1"].ToString();
            labOutFB2.Text = dt.Rows[0]["P_LineId2"].ToString() + dt.Rows[0]["P_StnNo2"].ToString() + dt.Rows[0]["P_PlCount2"].ToString();
            labOutFB3.Text = dt.Rows[0]["P_LineId3"].ToString() + dt.Rows[0]["P_StnNo3"].ToString() + dt.Rows[0]["P_PlCount3"].ToString();
            labOutFB4.Text = dt.Rows[0]["P_LineId4"].ToString() + dt.Rows[0]["P_StnNo4"].ToString() + dt.Rows[0]["P_PlCount4"].ToString();
            labMessage.Text = dt.Rows[0]["Message"].ToString();

            if (dt.Rows[0]["Message"].ToString() != null && dt.Rows[0]["Message"].ToString().Length > 0)
            {
                isMessageTD.Style["background-color"] = "red";
                isMessageTD.Style["color"] = "white";
            }
        }

        else
        {
            labLineID.Text = "无数据";
            labSiteID.Text = "无数据";
            labPallatCodeCount.Text = "无数据";
            lblLeftStatus.Text = "无数据";
            labERPCode.Text = "无数据";
            labBillCode.Text = "无数据";
            labPositionCode.Text = "无数据";
            labPallatCode.Text = "无数据";
            labCinvCode1.Text = "无数据";
            labOutBillQty1.Text = "无数据";
            labOutJHQty1.Text = "无数据";
            labOutFKQty1.Text = "无数据";
            labCinvCode2.Text = "无数据";
            labOutBillQty2.Text = "无数据";
            labOutJHQty2.Text = "无数据";
            labOutFKQty2.Text = "无数据";
            labCinvCode3.Text = "无数据";
            labOutBillQty3.Text = "无数据";
            labOutJHQty3.Text = "无数据";
            labOutFKQty3.Text = "无数据";
            labCinvCode4.Text = "无数据";
            labOutBillQty4.Text = "无数据";
            labOutJHQty4.Text = "无数据";
            labOutFKQty4.Text = "无数据";
            labOutFB1.Text = "无数据";
            labOutFB2.Text = "无数据";
            labOutFB3.Text = "无数据";
            labOutFB4.Text = "无数据";
            labMessage.Text = "无数据";
        }
      
    }
    [WebMethod]
    public static string GetData(string LineID, string SiteID)
    {
        string Ret = "";
                                        
        string strSQL1 = string.Format("Exec dbo.Proc_TVShow '{0}','{1}'", SiteID, LineID);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL1);

        if (dt.Rows.Count > 0)
        {
            Ret += dt.Rows[0]["PallatCodeCount"].ToString() + "||";
            Ret += dt.Rows[0]["Status_out"].ToString() + "||";
            Ret += dt.Rows[0]["ERPCode"].ToString() + "||";
            Ret += dt.Rows[0]["OutBillCode"].ToString() + "||";
            Ret += dt.Rows[0]["PositionCode_out"].ToString() + "||";
            Ret += dt.Rows[0]["PallatCode"].ToString() + "||";
            Ret += dt.Rows[0]["WL1_out"].ToString() + "||";
            Ret += dt.Rows[0]["OutBillQty1"].ToString() + "||";
            Ret += dt.Rows[0]["OutJHQty1"].ToString() + "||";
            Ret += dt.Rows[0]["OutFKQty1"].ToString() + "||";
            Ret += dt.Rows[0]["WL2_out"].ToString() + "||";
            Ret += dt.Rows[0]["OutBillQty2"].ToString() + "||";
            Ret += dt.Rows[0]["OutJHQty2"].ToString() + "||";
            Ret += dt.Rows[0]["OutFKQty2"].ToString() + "||";
            Ret += dt.Rows[0]["WL3_out"].ToString() + "||";
            Ret += dt.Rows[0]["OutBillQty3"].ToString() + "||";
            Ret += dt.Rows[0]["OutJHQty3"].ToString() + "||";
            Ret += dt.Rows[0]["OutFKQty3"].ToString() + "||";
            Ret += dt.Rows[0]["WL4_out"].ToString() + "||";
            Ret += dt.Rows[0]["OutBillQty4"].ToString() + "||";
            Ret += dt.Rows[0]["OutJHQty4"].ToString() + "||";
            Ret += dt.Rows[0]["OutFKQty4"].ToString() + "||";            
            Ret += dt.Rows[0]["P_LineId1"].ToString() + dt.Rows[0]["P_StnNo1"].ToString() + dt.Rows[0]["P_PlCount1"].ToString() + "||";
            Ret += dt.Rows[0]["P_LineId2"].ToString() + dt.Rows[0]["P_StnNo2"].ToString() + dt.Rows[0]["P_PlCount2"].ToString() + "||";
            Ret += dt.Rows[0]["P_LineId3"].ToString() + dt.Rows[0]["P_StnNo3"].ToString() + dt.Rows[0]["P_PlCount3"].ToString() + "||";
            Ret += dt.Rows[0]["P_LineId4"].ToString() + dt.Rows[0]["P_StnNo4"].ToString() + dt.Rows[0]["P_PlCount4"].ToString() + "||";

            Ret += dt.Rows[0]["Message"].ToString() + "||";
        }
        return Ret;
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindGD();
    }
}