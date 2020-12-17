using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Apps_FrmOUTASSITEdit_Print : System.Web.UI.Page
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
        printId = Request.QueryString["ID"];

        string Sql = @"SELECT A.ID,dbo.Fun_GetOperatorInfo(A.CCREATEOWNERCODE,'1') as CCREATEOWNERCODE,CONVERT(varchar(100),A.DCREATETIME, 23) DCREATETIME,A.CTICKETCODE,A.CSTATUS,case A.IS_MERGE when '0' then 'N' when '1' then 'Y' end as IS_MERGE,
                                (select s.flag_name from Sys_Parameter s where s.flag_type = 'ASSIT' and s.flag_id = A.CSTATUS) as CSTATUS_Name,
                                A.COUTASNID,B.cticketcode CTCode,ot.TYPENAME,B.Cerpcode 
                              from OUTASSIT A left join OUTASN B on A.coutasnid = b.id left join OutType ot on B.ITYPE = ot.CERPCODE
                         WHERE A.ID ='" + printId + "'";
        DataTable tb = DBHelp.ExecuteToDataTable(Sql);
        tb.TableName = "OUTASSIT";

        string Sql_d = @"SELECT row_number() over (order by A.CINVCODE desc) as RNUM,A.ID,A.IDS,A.CSTATUS,cast(A.INUM as decimal(18,2)) as INUM,A.CPOSITIONCODE,A.CPOSITION,A.CINVBARCODE,A.CINVCODE,A.CINVNAME,A.CBATCH,A.CMEMO,A.COPERATORCODE, dbo.Fun_GetOperatorInfo(A.COPERATOR,'1') as COPERATOR ,bp.calias as CALIAS
                         from OUTASSIT_D A with(nolock)
                         left join BASE_PART bp WITH(NOLOCK) on A.cinvcode = bp.CPARTNUMBER
                         where A.ID='" + printId + "'";
        DataTable tb_d = DBHelp.ExecuteToDataTable(Sql_d);
        tb_d.TableName = "OUTASSIT_D";

        DataSet ds = new DataSet();

        ds.Tables.Add(tb_d);
        ds.Tables.Add(tb);

        OutAssitReport rep = new OutAssitReport();
        rep.DataSource = ds;
        OutAsnReport1.Report = rep;
        OutAsnReport1.Report.FillDataSource();
        OutAsnReport1.DataBind();
    }
}