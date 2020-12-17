using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.DAL;

public partial class Apps_BASE_FrmBase_ShowTVBySiteNew1 : System.Web.UI.Page
{
    public string LineId
    {
        get { return this.hiddlineid.Value; }
        set { this.hiddlineid.Value = value; }
    }

    public string SiteId
    {
        get { return this.hiddsiteid.Value; }
        set { this.hiddsiteid.Value = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断电子看板，1=简约版 2=科技版
        var configVal = DreamTek.ASRS.Business.Base.CommFunction.GetConFig("120004");
        if (configVal == "1")
        {
            //Server.Transfer("FrmBase_ShowTVBySiteNew1.aspx");
            //Note by Qamar 2020-10-20
            Server.Transfer("FrmBase_ShowTVBySite.aspx");
        }
        else
        {
            GetParameters();
            DBContext db = new DBContext();
            string typename = "入";
            var sitetype = db.BASE_CRANECONFIG_DETIAL.Where(x => x.CRANEID == LineId && x.SITEID == SiteId).FirstOrDefault();
            if (sitetype != null)
            {
                if (sitetype.SITETYPE == "2")
                    typename = "出";
                else if (sitetype.SITETYPE == "3")
                {
                    typename = "出";
                    //sitetype.SITETYPE = "2";
                }
            }
            this.lblTitle.Text = typename + "庫口" + LineId + "線" + SiteId + "站";
            this.lblRefreshtime.Text = DreamTek.ASRS.Business.Base.CommFunction.GetConFig("100000");
            this.lblTitleMsg.Text = DreamTek.ASRS.Business.Base.CommFunction.GetConFigDesc("120005");
            BindData();
        }
    }

    /// <summary>
    /// 获取传入的页面参数
    /// </summary>
    private void GetParameters()
    {
        if (string.IsNullOrEmpty(this.Request.QueryString["LineId"]))
        {
            this.LineId = "";
        }
        else
        {
            this.LineId = this.Request.QueryString["LineId"];
        }
        if (string.IsNullOrEmpty(this.Request.QueryString["SiteId"]))
        {
            this.SiteId = "";
        }
        else
        {
            this.SiteId = this.Request.QueryString["SiteId"];
        }
    }

    //簡體轉繁體. Note by Qamar 2020-10-20
    public static string ConvertToTW(string cn)
    {
        string Ret = String.Copy(cn);
        Ret = Ret.Replace("库", "庫");
        Ret = Ret.Replace("调", "調");
        Ret = Ret.Replace("拨", "撥");
        Ret = Ret.Replace("单", "單");
        //Proc_GetTVShowInfoBySite用的是 "已完成"
        //Proc_GetTVShowInfoBySiteNew用的是 "出完成"
        //by Qamar
        if (Ret == "已完成")
            Ret = "出完成";
        return Ret;
    }

    //儲位編碼轉成儲位名稱 Note by Qamar 2020-11-17
    private static string ConvertToCpositionName(string CpositionCode)
    {
        string CpositionName = "";
        try
        {
            if (CpositionCode.Substring(4, 1) == "1")
                CpositionName += "M";
            else if (CpositionCode.Substring(4, 1) == "2")
                CpositionName += "N";
            CpositionName += CpositionCode.Substring(CpositionCode.Length - 1, 1);
            CpositionName += CpositionCode.Substring(CpositionCode.Length - 3, 2);
        }
        catch
        {
            CpositionName = "";
        }
        return CpositionName;
    }

    //去掉小數點之後不重要的零. Note by Qamar 2020-11-17
    private static string ConvertToStringLikeInt(string str)
    {
        try
        {
            str = Convert.ToString(Convert.ToDouble(str)*100/100);
        }
        catch
        {
            str = "";
        }
        return str;
    }

    //加入分隔字串變成 part@@rankfinal. Note by Qamar 2020-11-17
    private static string ConvertToPart_and_Rankfinal(string str)
    {
        string result = "";
        try
        {
            result = str.Substring(0, str.Length - 2);
            string rankfinal = str.Substring(str.Length - 1, 1);
            if (rankfinal == "_")
                rankfinal = "";
            result += "@@" + rankfinal; 
        }
        catch
        {
            result = "@@";
        }
        return result;
    }

    //設定lblCinvCode lblRankFinal lblQty的Text. Note by Qamar 2020-11-17
    private void Set_CinvCode_and_RankFinal_and_Qty(int stringtype, DataTable dt, int index)
    {
        string str_index = Convert.ToString(index);
        Label[] arr_cinvcode = new Label[] { null, lblCinvCode1, lblCinvCode2, lblCinvCode3,
            lblCinvCode4, lblCinvCode5, lblCinvCode6, lblCinvCode7, lblCinvCode8};
        Label[] arr_rankfinal = new Label[] { null, lblRankFinal1, lblRankFinal2, lblRankFinal3,
            lblRankFinal4, lblRankFinal5, lblRankFinal6, lblRankFinal7, lblRankFinal8};
        Label[] arr_qty = new Label[] { null, lblQty1, lblQty2, lblQty3, lblQty4, lblQty5, lblQty6, lblQty7, lblQty8 };
        string cinvcode = "";
        switch (stringtype)
        {
            case 1:
                arr_qty[index].Text = ConvertToStringLikeInt(dt.Rows[0]["OutBillQty" + str_index].ToString());
                cinvcode = dt.Rows[0]["WL" + str_index + "_out"].ToString();
                break;
            case 3:
                arr_qty[index].Text = ConvertToStringLikeInt(dt.Rows[0]["QTY" + str_index].ToString());
                cinvcode = dt.Rows[0]["CINVCODE" + str_index].ToString();
                break;
        }
        try
        {
            arr_cinvcode[index].Text = cinvcode.Substring(0, cinvcode.Length - 2);
            arr_rankfinal[index].Text = cinvcode.Substring(cinvcode.Length - 1, 1);
            if (arr_rankfinal[index].Text == "_")
                arr_rankfinal[index].Text = "";
        }
        catch
        {
            arr_cinvcode[index].Text = cinvcode;
            arr_rankfinal[index].Text = "";
        }
    }

    //屏蔽錯誤訊息. Note by Qamar 2020-11-21
    private static string HideErrorMessange(string status, string msg)
    {
        if (status == "返庫中")
        {
            try
            {
                if (ConvertToTW(msg).Substring(0, 3) == "調撥單")
                {
                    //屏蔽錯誤訊息
                    msg = "";
                }
            }
            catch { }
        }
        return msg;
    }

    private void BindData()
    {
        string strSQL = string.Format("Exec dbo.Proc_GetTVShowInfoBySite '{0}','{1}' ", this.LineId, this.SiteId);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            if (dt.Columns.Contains("OutBillCode"))
            {
                lblLeftStatus.Text = ConvertToTW(dt.Rows[0]["Status_out"].ToString());
                lblSiteCode.Text = dt.Rows[0]["SiteCode"].ToString();
                lblCticketCode.Text = dt.Rows[0]["OutBillCode"].ToString();
                lblPalletCode.Text = dt.Rows[0]["PallatCode"].ToString();
                lblCpositionCode.Text = ConvertToCpositionName(dt.Rows[0]["PositionCode_out"].ToString());

                //Note by Qamar 2020-11-17
                for (int i = 1; i <= 8; i++)
                    Set_CinvCode_and_RankFinal_and_Qty(1, dt, i);
                lblCinvCode9.Text = dt.Rows[0]["WL9_out"].ToString();
                lblQty9.Text = ConvertToTW(dt.Rows[0]["OutBillQty9"].ToString());

                //Note by Qamar 2020-11-21
                lblQty9.Text = HideErrorMessange(lblLeftStatus.Text, lblQty9.Text);
            }
            else if (dt.Columns.Contains("TODOCOUNT"))
            {
                lblLeftStatus_P.Text = ConvertToTW(dt.Rows[0]["Status_out"].ToString());
                lblPallteCode_P.Text = dt.Rows[0]["PACKAGENO"].ToString();
                lblCticketcode_C.Text = dt.Rows[0]["CTICKETCODE"].ToString();
                lblPostitionCode_C.Text = ConvertToCpositionName(dt.Rows[0]["CPOSITIONCODE"].ToString());
                lblErpCode1_C.Text = dt.Rows[0]["ERPCODE1"].ToString();
                lblErpCode2_C.Text = dt.Rows[0]["ERPCODE2"].ToString();
                lblErpCode3_C.Text = dt.Rows[0]["ERPCODE3"].ToString();
                lblErpCode4_C.Text = dt.Rows[0]["DAIJIAN"].ToString(); //是否整出
                lblCinvCode1_C.Text = dt.Rows[0]["CINVCODE1"].ToString();
                lblCinvCode2_C.Text = dt.Rows[0]["CINVCODE2"].ToString();
                lblCinvCode3_C.Text = dt.Rows[0]["CINVCODE3"].ToString();
                labOutFB1.Text = dt.Rows[0]["P_LineId1"].ToString() + dt.Rows[0]["P_StnNo1"].ToString() + dt.Rows[0]["P_PlCount1"].ToString();
                labOutFB2.Text = dt.Rows[0]["P_LineId2"].ToString() + dt.Rows[0]["P_StnNo2"].ToString() + dt.Rows[0]["P_PlCount2"].ToString();
                labOutFB3.Text = dt.Rows[0]["P_LineId3"].ToString() + dt.Rows[0]["P_StnNo3"].ToString() + dt.Rows[0]["P_PlCount3"].ToString();
                labOutFB4.Text = dt.Rows[0]["P_LineId4"].ToString() + dt.Rows[0]["P_StnNo4"].ToString() + dt.Rows[0]["P_PlCount4"].ToString();
                labOutFB5.Text = dt.Rows[0]["P_LineId5"].ToString() + dt.Rows[0]["P_StnNo5"].ToString() + dt.Rows[0]["P_PlCount5"].ToString();

                lblQty1_C.Text = ConvertToStringLikeInt(dt.Rows[0]["QTY1"].ToString());
                lblQty2_C.Text = ConvertToStringLikeInt(dt.Rows[0]["QTY2"].ToString());
                lblQty3_C.Text = ConvertToStringLikeInt(dt.Rows[0]["QTY3"].ToString());
                lblTODOCOUNT_C.Text = dt.Rows[0]["TODOCOUNT"].ToString();//待出库指令数
            }
            else
            {
                /*
                lblLeftStatus_P.Text = ConvertToTW(dt.Rows[0]["Status_out"].ToString());
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
                */
                //Note by Qamar 2020-11-21
                lblLeftStatus.Text = ConvertToTW(dt.Rows[0]["Status_out"].ToString());
                lblCticketCode.Text = dt.Rows[0]["CTICKETCODE"].ToString();
                lblPalletCode.Text = dt.Rows[0]["PACKAGENO"].ToString();
                lblCpositionCode.Text = ConvertToCpositionName(dt.Rows[0]["CPOSITIONCODE"].ToString());
                for (int i = 1; i <= 8; i++)
                    Set_CinvCode_and_RankFinal_and_Qty(3, dt, i);
                lblQty9.Text = HideErrorMessange(lblLeftStatus.Text, lblQty9.Text);
            }
        }
        else
        {
            lblLeftStatus.Text = "無數據";
            lblSiteCode.Text = "無數據";
            lblCticketCode.Text = "無數據";
            lblPalletCode.Text = "無數據";
            lblCpositionCode.Text = "無數據";
            lblCinvCode1.Text = "無數據";
            lblQty1.Text = "無數據";
            lblCinvCode2.Text = "無數據";
            lblQty2.Text = "無數據";
            lblCinvCode3.Text = "無數據";
            lblQty3.Text = "無數據";
            lblCinvCode4.Text = "無數據";
            lblQty4.Text = "無數據";
            lblCinvCode5.Text = "無數據";
            lblQty5.Text = "無數據";
            lblCinvCode6.Text = "無數據";
            lblQty6.Text = "無數據";
            lblCinvCode7.Text = "無數據";
            lblQty7.Text = "無數據";
            lblCinvCode8.Text = "無數據";
            lblQty8.Text = "無數據";

            lblCinvCode9.Text = "";
            lblQty9.Text = "";
        }
    }

    [WebMethod]
    public static string GetData(string lineid, string siteid)
    {
        string Ret = "";

        string strSQL = string.Format(" Exec dbo.Proc_GetTVShowInfoBySite '{0}','{1}' ", lineid, siteid);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);

        if (dt.Rows.Count > 0)
        {
            if (dt.Columns.Contains("OutBillCode")) //if (!dt.Rows[0]["Status_out"].ToString().Trim().Contains("出库中"))
            {
                Ret += ConvertToTW(dt.Rows[0]["Status_out"].ToString()) + "||";
                Ret += dt.Rows[0]["SiteCode"].ToString() + "||";
                Ret += dt.Rows[0]["OutBillCode"].ToString() + "||";
                Ret += dt.Rows[0]["PallatCode"].ToString() + "||";
                Ret += ConvertToCpositionName(dt.Rows[0]["PositionCode_out"].ToString()) + "||";
                //Note by Qamar 2020-11-17
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["WL1_out"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["OutBillQty1"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["WL2_out"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["OutBillQty2"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["WL3_out"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["OutBillQty3"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["WL4_out"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["OutBillQty4"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["WL5_out"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["OutBillQty5"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["WL6_out"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["OutBillQty6"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["WL7_out"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["OutBillQty7"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["WL8_out"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["OutBillQty8"].ToString()) + "||";
                Ret += dt.Rows[0]["WL9_out"].ToString() + "||";
                Ret += HideErrorMessange(ConvertToTW(dt.Rows[0]["Status_out"].ToString()), dt.Rows[0]["OutBillQty9"].ToString()) + "||";
            }
            else if (dt.Columns.Contains("TODOCOUNT"))
            {
                Ret += ConvertToTW(dt.Rows[0]["Status_out"].ToString()) + "||";
                Ret += dt.Rows[0]["PACKAGENO"].ToString() + "||";
                Ret += dt.Rows[0]["CTICKETCODE"].ToString() + "||";
                Ret += ConvertToCpositionName(dt.Rows[0]["CPOSITIONCODE"].ToString()) + "||";
                Ret += dt.Rows[0]["ERPCODE1"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE1"].ToString() + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY1"].ToString()) + "||";
                Ret += dt.Rows[0]["ERPCODE2"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE2"].ToString() + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY2"].ToString()) + "||";
                Ret += dt.Rows[0]["ERPCODE3"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE3"].ToString() + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY3"].ToString()) + "||";
                Ret += dt.Rows[0]["DAIJIAN"].ToString() + "||";
                //Ret += dt.Rows[0]["CINVCODE4"].ToString() + "||";
                Ret += dt.Rows[0]["P_LineId1"].ToString() + dt.Rows[0]["P_StnNo1"].ToString() + dt.Rows[0]["P_PlCount1"].ToString() + "||";
                Ret += dt.Rows[0]["P_LineId2"].ToString() + dt.Rows[0]["P_StnNo2"].ToString() + dt.Rows[0]["P_PlCount2"].ToString() + "||";
                Ret += dt.Rows[0]["P_LineId3"].ToString() + dt.Rows[0]["P_StnNo3"].ToString() + dt.Rows[0]["P_PlCount3"].ToString() + "||";
                Ret += dt.Rows[0]["P_LineId4"].ToString() + dt.Rows[0]["P_StnNo4"].ToString() + dt.Rows[0]["P_PlCount4"].ToString() + "||";
                Ret += dt.Rows[0]["P_LineId5"].ToString() + dt.Rows[0]["P_StnNo5"].ToString() + dt.Rows[0]["P_PlCount5"].ToString() + "||";
                Ret += dt.Rows[0]["TODOCOUNT"].ToString() + "||";

            }
            else
            {
                /*
                Ret += ConvertToTW(dt.Rows[0]["Status_out"].ToString()) + "||";
                Ret += dt.Rows[0]["PACKAGENO"].ToString() + "||";
                Ret += dt.Rows[0]["CTICKETCODE"].ToString() + "||";
                Ret += ConvertToCpositionName(dt.Rows[0]["CPOSITIONCODE"].ToString()) + "||";
                Ret += dt.Rows[0]["ERPCODE1"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE1"].ToString() + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY1"].ToString()) + "||";
                Ret += dt.Rows[0]["ERPCODE2"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE2"].ToString() + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY2"].ToString()) + "||";
                Ret += dt.Rows[0]["ERPCODE3"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE3"].ToString() + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY3"].ToString()) + "||";
                Ret += dt.Rows[0]["ERPCODE4"].ToString() + "||";
                Ret += dt.Rows[0]["CINVCODE4"].ToString() + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY4"].ToString()) + "||";
                */
                //Note by Qamar 2020-11-21
                Ret += ConvertToTW(dt.Rows[0]["Status_out"].ToString()) + "||";
                Ret += dt.Rows[0]["ERPCODE1"].ToString() + "||";
                Ret += dt.Rows[0]["CTICKETCODE"].ToString() + "||";
                Ret += dt.Rows[0]["PACKAGENO"].ToString() + "||";
                Ret += ConvertToCpositionName(dt.Rows[0]["CPOSITIONCODE"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["CINVCODE1"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY1"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["CINVCODE2"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY2"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["CINVCODE3"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY3"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["CINVCODE4"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY4"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["CINVCODE5"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY5"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["CINVCODE6"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY6"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["CINVCODE7"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY7"].ToString()) + "||";
                Ret += ConvertToPart_and_Rankfinal(dt.Rows[0]["CINVCODE8"].ToString()) + "||";
                Ret += ConvertToStringLikeInt(dt.Rows[0]["QTY8"].ToString()) + "||";
                Ret += "||";
                Ret += "||";
            }

        }
        return Ret.Substring(0, Ret.Length - 2);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindData();
    }
}