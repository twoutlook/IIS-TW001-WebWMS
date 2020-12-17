using DreamTek.ASRS.Business.SP.ProcedureModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Apps_BASE_BASE_TVShow_Mixed : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
       string storey = string.Empty;
       storey = this.Request.QueryString["storey"].ToString();
       // storey = "6291"; //配料位编号
        this.lblTitle.Text = "配料位" + storey;
        this.storey_id.Text = storey;
        this.lblRefreshtime.Text = DreamTek.ASRS.Business.Base.CommFunction.GetConFig("100000");
        BindData();

    }

    private void BindData()
    {
        string strSQL = string.Format("Exec dbo.Proc_TVShow_Mixed '{0}'", this.storey_id.Text);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
           
        }
        else
        {
            lblErpCode1.Text = "无数据";
            lblSchedule1.Text = "无数据";
            lblErpCode2.Text = "无数据";
            lblSchedule2.Text = "无数据";
            lblErpCode3.Text = "无数据";
            lblSchedule3.Text = "无数据";
            lblErpCode4.Text = "无数据";
            lblSchedule4.Text = "无数据";
            lblErpCode5.Text = "无数据";
            lblSchedule5.Text = "无数据";
            lblErpCode6.Text = "无数据";
            lblSchedule6.Text = "无数据";

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
    public static string GetData(string pageindex, string storey_id,string currPageIndex)
    {
        string Ret = "";

        string strSQL = string.Format("Exec dbo.Proc_TVShow_Mixed '{0}'", storey_id);
        string strSQLCurrent = string.Format("Exec Proc_TVShow_Mixed_Current '{0}'", storey_id);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        DataTable dtCurrent = DBHelp.ExecuteToDataTable(strSQLCurrent);
        int count = 0;
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToInt32(pageindex) > Math.Ceiling((double)dt.Rows.Count / 6))
                pageindex = "1";
            Ret += (pageindex + "/" + Math.Ceiling((double)dt.Rows.Count / 6).ToString()) + "||";
            Ret += (Convert.ToInt32(pageindex) + 1).ToString() + "||";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                    if (Convert.ToInt32(dt.Rows[i]["RowNum"].ToString()) >= (Convert.ToInt32(pageindex) * 6 - 5) && Convert.ToInt32(dt.Rows[i]["RowNum"].ToString()) <= (Convert.ToInt32(pageindex) * 6))
                    {
                        count++;
                        Ret += dt.Rows[i]["ErpCode"].ToString() + "||";
                        Ret += dt.Rows[i]["PAllQty"].ToString() + "||";
                        //Ret += dt.Rows[i]["PkPM"].ToString() + "||";
                    }                    
            }
            var diffcount = 6 - count;
            if (diffcount > 0)
            {
                for (int j = 0; j < diffcount; j++)
                {
                    Ret += "" + "||";
                    Ret += "" + "||";
                }
            }
        }       
        if (dtCurrent.Rows.Count > 0)
        {
            if (Convert.ToInt32(currPageIndex) > Math.Ceiling((double)dtCurrent.Rows.Count / 6))
                currPageIndex = "1";
            //Ret += (currPageIndex + "/" + Math.Ceiling((double)dtCurrent.Rows.Count / 6).ToString()) + "||";
            Ret += (Convert.ToInt32(currPageIndex) + 1).ToString() + "||";
            Ret += dtCurrent.Rows[0]["MIXEDCODE"].ToString() + "||";
            for (int i = 0; i < dtCurrent.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtCurrent.Rows[i]["RowNum"].ToString()) >= (Convert.ToInt32(currPageIndex) * 6 - 5) && Convert.ToInt32(dtCurrent.Rows[i]["RowNum"].ToString()) <= (Convert.ToInt32(currPageIndex) * 6))
                {
                    Ret += dtCurrent.Rows[i]["cinvcode"].ToString() + "||";
                    Ret += dtCurrent.Rows[i]["iquantity"].ToString() + "||";
                    //Ret += dt.Rows[i]["PkPM"].ToString() + "||";
                }
            }           
        }
        return Ret.Substring(0, Ret.Length - 2);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindData();
    }
}