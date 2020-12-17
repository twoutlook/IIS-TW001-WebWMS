using DreamTek.ASRS.Business.SP.ProcedureModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_BASE_TVShow_ProductionMent : System.Web.UI.Page
{
    //string PageIndex = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.storey = this.Request.QueryString["Storey"].ToString();
        this.lblTitle.Text = "出库进度";
        //this.storey_id.Text = pageIndex;
        this.lblRefreshtime.Text = DreamTek.ASRS.Business.Base.CommFunction.GetConFig("100000");
        BindData();
    }

    private void BindData()
    {
        string strSQL = string.Format("Exec dbo.Proc_TVShow_ProductionMent {0}", 0);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {           
        }
        else
        {
            this.indexPage.Text = "0.00/0.00";
            this.erpcode1.Text = "无数据";
            this.lkpm1.Text = "无数据";
            this.pkpm1.Text = "无数据";

            this.erpcode2.Text = "无数据";
            this.lkpm2.Text = "无数据";
            this.pkpm2.Text = "无数据";

            this.erpcode3.Text = "无数据";
            this.lkpm3.Text = "无数据";
            this.pkpm3.Text = "无数据";

            this.erpcode4.Text = "无数据";
            this.lkpm4.Text = "无数据";
            this.pkpm4.Text = "无数据";

            this.erpcode5.Text = "无数据";
            this.lkpm5.Text = "无数据";
            this.pkpm5.Text = "无数据";

            this.erpcode6.Text = "无数据";
            this.lkpm6.Text = "无数据";
            this.pkpm6.Text = "无数据";

            this.erpcode7.Text = "无数据";
            this.lkpm7.Text = "无数据";
            this.pkpm7.Text = "无数据";

            this.erpcode8.Text = "无数据";
            this.lkpm8.Text = "无数据";
            this.pkpm8.Text = "无数据";
        }
    }

    [WebMethod]
    public static string GetData(string Storey)
    {
        string Ret = "";

        string strSQL = string.Format("Exec dbo.Proc_TVShow_ProductionMent '{0}'", 0);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);

        if (dt.Rows.Count > 0)
        {          
            if (Convert.ToInt32(Storey) > Math.Ceiling((double)dt.Rows.Count / 8))
                Storey = "1";
            Ret += (Storey + "/" + Math.Ceiling((double)dt.Rows.Count / 8).ToString()) + "||";
            Ret += (Convert.ToInt32(Storey) + 1).ToString() + "||";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if( Convert.ToInt32(dt.Rows[i]["RowNum"].ToString()) >= (Convert.ToInt32(Storey) *8 -7) && Convert.ToInt32(dt.Rows[i]["RowNum"].ToString()) <= (Convert.ToInt32(Storey) *8 ))
                {
                    Ret += dt.Rows[i]["ErpCode"].ToString() + "||";
                    Ret += dt.Rows[i]["LkPM"].ToString() + "||";
                    Ret += dt.Rows[i]["PkPM"].ToString() + "||";
                }
            }          
        }
        return Ret.Substring(0,Ret.Length - 2);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindData();
    }
}