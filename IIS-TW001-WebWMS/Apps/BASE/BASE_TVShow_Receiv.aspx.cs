using DreamTek.ASRS.Business.SP.ProcedureModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_BASE_TVShow_Receiv : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {        
        this.lblTitle.Text = "设备运行状况";
        this.lblRefreshtime.Text = DreamTek.ASRS.Business.Base.CommFunction.GetConFig("100000");
        BindData();
    }

    private void BindData()
    {
        string strSQL = string.Format("Exec dbo.Proc_TVShow_ShouLiao");
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            Label2.Text = dt.Rows[0]["LineIDL1"].ToString();
            Label3.Text = dt.Rows[0]["LineIDL2"].ToString();
            Label13.Text = dt.Rows[0]["LineIDR1"].ToString();
            Label14.Text = dt.Rows[0]["LineIDR2"].ToString();
            Label16.Text = dt.Rows[0]["LineIDS1"].ToString();

            if (dt.Rows[0]["StatusL1"].ToString().Trim() == "1")
            {
                Label2.ForeColor = System.Drawing.Color.Red;
                Label6.ForeColor = System.Drawing.Color.Red;
                Label17.ForeColor = System.Drawing.Color.Red;
                Label7.ForeColor = System.Drawing.Color.Red;
                Label19.ForeColor = System.Drawing.Color.Red;
                   
            }
            if (dt.Rows[0]["StatusL2"].ToString().Trim() == "1")
            {
                Label3.ForeColor = System.Drawing.Color.Red;
                Label8.ForeColor = System.Drawing.Color.Red;
                Label9.ForeColor = System.Drawing.Color.Red;
                Label10.ForeColor = System.Drawing.Color.Red;
                Label11.ForeColor = System.Drawing.Color.Red;
                   
            }
            if (dt.Rows[0]["StatusR1"].ToString().Trim() == "1")
                Label13.ForeColor = System.Drawing.Color.Red;
            if (dt.Rows[0]["StatusR2"].ToString().Trim() == "1")
                Label14.ForeColor = System.Drawing.Color.Red;
            if (dt.Rows[0]["StatusS1"].ToString().Trim() == "1")
                Label16.ForeColor = System.Drawing.Color.Red;
          
        }       
    }

    [WebMethod]
    public static string GetData()
    {
        string Ret = "";

        string strSQL = string.Format("Exec dbo.Proc_TVShow_ShouLiao");
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);

        if (dt.Rows.Count > 0)
        {
            Ret += dt.Rows[0]["LineIDL1"].ToString() + "||";
            Ret += dt.Rows[0]["LineIDL2"].ToString() + "||";
            Ret += dt.Rows[0]["LineIDR1"].ToString() + "||";
            Ret += dt.Rows[0]["LineIDR2"].ToString() + "||";
            Ret += dt.Rows[0]["LineIDS1"].ToString() + "||";
            Ret += dt.Rows[0]["StatusL1"].ToString() + "||";
            Ret += dt.Rows[0]["StatusL2"].ToString() + "||";
            Ret += dt.Rows[0]["StatusR1"].ToString() + "||";
            Ret += dt.Rows[0]["StatusR2"].ToString() + "||";
            Ret += dt.Rows[0]["StatusS1"].ToString() + "||";
        
        }
        return Ret.Substring(0, Ret.Length - 2);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindData();
    }
}