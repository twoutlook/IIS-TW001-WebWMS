using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DreamTek.ASRS.DAL;

public partial class Apps_ALLOCATE_FrmALLOCATEEdit_Print : System.Web.UI.Page
{
    /// <summary>
    /// 记录ID号
    /// </summary>
    public string printId
    {
        get { return ViewState["printId"].ToString(); }
        set { ViewState["printId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DBContext db = new DBContext();

        printId = Request.QueryString["ID"];


        string Sql = @"SELECT A.ID,A.CCREATEOWNERCODE,convert(varchar(25),A.DCREATETIME,121) DCREATETIME,
A.CAUDITPERSON,convert(varchar(25),A.DAUDITTIME,121) DAUDITTIME ,A.CTICKETCODE,A.CERPCODE,A.CSTATUS,
(case A.CSTATUS when '0' then 	'未处理' when '1' then '已审核' when '2' then '已完成' when '3' then '已抛转' when '4' then '已确认'
   when '5' then '調撥中' when '6' then '調撥完成' end) CSTATUS_Name,
convert(varchar(25),A.DINDATE,121) DINDATE,(case A.IS_ALLOW when 0 then '是' when 1 then '否' end) IS_ALLOW,
A.ID,convert(varchar(25),A.Ddefine3,121) Ddefine3,convert(varchar(25),A.Debittime,121) Debittime,A.Debitowner 
from ALLOCATE A  where A.Special = 0  and A.ID ='" + printId + "'";

        DataTable tb = DBHelp.ExecuteToDataTable(Sql);
        tb.TableName = "ALLOCATE";

        string Sql_d = @"SELECT row_number() over(order by a.id) AS RNUM,A.IDS,A.ID,A.CINVCODE,A.CINVNAME,cast(A.IQUANTITY as decimal(18,2)) as IQUANTITY,A.CPOSITIONCODE,A.CPOSITION,
A.CTOPOSITIONCODE,A.CTOPOSITION,convert(varchar(25),A.DINDATE,121) DINDATE,A.CINPERSONCODE,A.CMIDPOSITIONCODE,A.CDEFINE1
 from ALLOCATE_D A
                                   where A.ID='" + printId + "'";

        DataTable tb_d = DBHelp.ExecuteToDataTable(Sql_d); //DBUtil.Fill(Sql_d);
        tb_d.TableName = "ALLOCATE_D";
        
        DataSet ds = new DataSet();

        ds.Tables.Add(tb_d);
        ds.Tables.Add(tb);


        AllocateReport rep = new AllocateReport();
        rep.DataSource = ds;
        OutAsnReport1.Report = rep;
        OutAsnReport1.Report.FillDataSource();
        OutAsnReport1.DataBind();
    }
}