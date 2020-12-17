using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraReports.Web;
using Resources;

public partial class Apps_FrmINASNEdit_Print : System.Web.UI.Page
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

        string Sql = @"SELECT OA.CTICKETCODE,
                               TY.TYPENAME,
                               OA.CSO,
                               OA.CERPCODE,
                               OA.CCLIENT,
                               OA.CCLIENTCODE,
                               OA.CCREATEOWNERCODE,
                               CONVERT(varchar(100), OA.DCREATETIME, 23) as DCREATETIME,
                               CASE OA.worktype WHEN '1' THEN '立库' WHEN '0' THEN '平库'END AS WORKTYPE
                          FROM OUTASN OA WITH(NOLOCK)
                          LEFT JOIN OUTTYPE TY WITH(NOLOCK) ON TY.CERPCODE = OA.ITYPE
                         WHERE OA.ID ='" + printId + "'";
        DataTable tb =DBHelp.ExecuteToDataTable(Sql);
        tb.TableName = "OUTASN";

        string Sql_d = @"SELECT row_number() over (order by newid()) as RNUM,A.CSO,
                                   A.CINVNAME,
                                   A.CINVCODE,
                                   cast(A.IQUANTITY as decimal(18,2)) as IQUANTITY,
                                   A.CERPCODELINE,
                                   A.IDS,
                                   cast(dbo.[Fun_GetInOrOutBill_D_Qty](A.CINVCODE, A.Cerpcodeline, A.Id, 0) as decimal(18,2)) as OutBill_Qty,
                                   cast(dbo.[Fun_GetInOrOutBill_D_Qty](A.CINVCODE, A.Cerpcodeline, A.Id, 1) as decimal(18,2)) as OutBilled_Qty,
                                   part.calias as CALIAS
                                   from OUTASN_D A WITH(NOLOCK)
                                   INNER JOIN BASE_PART part WITH(NOLOCK) ON A.cinvcode = part.cpartnumber
                                   where A.ID='" + printId + "'";
        DataTable tb_d = DBHelp.ExecuteToDataTable(Sql_d);
        tb_d.TableName = "OUTASN_D";

        DataSet ds=new DataSet();

        ds.Tables.Add(tb_d);
        ds.Tables.Add(tb);
       
        OutAsnReport rep = new OutAsnReport();
        rep.DataSource = ds;
        OutAsnReport1.Report = rep;
        OutAsnReport1.Report.FillDataSource();
        OutAsnReport1.DataBind();
    }
}