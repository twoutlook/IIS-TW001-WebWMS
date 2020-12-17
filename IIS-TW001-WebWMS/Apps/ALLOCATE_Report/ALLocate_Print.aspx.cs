using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraReports.Web;
using Resources;

public partial class Apps_ALLOCATE_Report_ALLocate_Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string printId = Request.QueryString["ID"];
        //string printId = "";
        //printId = Session["ID"].ToString();
       
       // string Sql = @"SELECT A.ID,A.CTICKETCODE,A.DCREATETIME,A.CAUDITPERSON,A.DAUDITTIME FROM ALLOCATE A WHERE A.ID='"+printId+"'";
        string Sql = @"SELECT A.ID,A.CTICKETCODE,CONVERT(NVARCHAR(10),A.DCREATETIME,120) as DCREATETIME,US.USERNAME as CAUDITPERSON,CONVERT(NVARCHAR(10),A.DAUDITTIME,120) as DAUDITTIME FROM ALLOCATE A LEFT JOIN  zsict1_user US ON US.USERNO=A.CAUDITPERSON WHERE A.ID='" + printId + "'";  // ---BUCKINGHA-838 条码管理修改CH
        //DataTable tb = DBUtil.Fill(Sql);
        DataTable tb = DBHelp.ExecuteToDataTable(Sql);
        tb.TableName = "ALLOCATE";
        string Sql2 = @"SELECT AD.IDS,AD.ID,AD.CINVCODE,AD.CINVNAME,cast(AD.IQUANTITY as decimal(18,2)) as  IQUANTITY,AD.CPOSITIONCODE,AD.CPOSITION,AD.CTOPOSITIONCODE,AD.CTOPOSITION FROM ALLOCATE_D AD WHERE AD.ID='" + printId + "'";
        DataTable tb2 = DBHelp.ExecuteToDataTable(Sql2); //DBUtil.Fill(Sql2);
        tb2.TableName = "ALLOCATE_D";
        DataSet ds=new DataSet();
       
        ds.Tables.Add(tb2);
        ds.Tables.Add(tb);
        XReport re=new XReport();
        re.DataSource = ds;
        XaReport.Report = re;
        XaReport.Report.FillDataSource();
        XaReport.DataBind();
    }
}