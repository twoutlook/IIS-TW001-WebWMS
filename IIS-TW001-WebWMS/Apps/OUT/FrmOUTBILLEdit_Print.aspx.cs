using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Apps_FrmOUTBILLEdit_Print : System.Web.UI.Page
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

        string Sql = @"SELECT A.ID,
                              A.CCREATEOWNERCODE,
                              CONVERT(varchar(100), A.DCREATETIME, 23) as DCREATETIME,
                              A.CAUDITPERSON,A.DAUDITTIME,A.CTICKETCODE,A.CERPCODE,A.COUTASNID,A.CMEMO,
                              CONVERT(varchar(100), A.DINDATE, 23) as DINDATE,
                              A.CSO,B.CTICKETCODE BCCODE,A.OType,outType.Typename,A.CSTATUS,
                              A.Ddefine3,
                              A.CDEFINE1,
                              CONVERT(varchar(100), A.Debittime, 23) as Debittime,
                              A.Debitowner,
                              A.Cclientcode,
                              A.Cclient,
                              (select FLAG_NAME from sys_parameter sp 
                               where sp.flag_type='OUTBILL' 
                               and sp.flag_id=A.CSTATUS) as CSTATUS_Name,
                              CASE A.worktype WHEN '1' THEN '立库' WHEN '0' THEN '平库'END AS WORKTYPE
                             From OUTBILL A with(nolock)
                             left join OUTASN B with(nolock) on A.COUTASNID = B.ID
                             left join outtype outType with(nolock) on outType.CERPCODE = A.OType 
                         WHERE A.ID ='" + printId + "'";
        DataTable tb = DBHelp.ExecuteToDataTable(Sql);
        tb.TableName = "OUTBILL";

        string Sql_d = @"SELECT ROW_NUMBER() over(order by A.DOUTDATE) AS RNUM,A.*,A.IQUANTITY-A.DeliveriesQTY AS HandOverQTY
                            ,sn.FurnaceNo,
                            sn.DateCode,
                            sn.SN_CODE,
                            bp.cunits,
                            bp.calias as CALIAS               
                            FROM 
                                (SELECT A.IDS,
                                       A.ID,
                                       A.CSTATUS,
                                       CAST(A.IQUANTITY AS DECIMAL(18,2)) AS IQUANTITY,
                                       A.CPOSITIONCODE,
                                       A.CPOSITION,
                                       A.CINVBARCODE,
                                       A.CINVCODE,
                                       A.CINVNAME,
                                       A.CERPCODELINE,
                                       A.DOUTDATE,
                                       A.COUTPERSONCODE,
                                       A.CMEMO,
                                       A.IOUTASNLINE,
                                       A.LINE_QTY,
                                       (select isnull(sum(oh.qty), 0)
                                          from OUT_HANDOVER oh WITH(NOLOCK)
                                         where oh.outbill_d_id = a.ids) DeliveriesQTY,
                                       A.CDEFINE1,
                                       A.ASRS_STATUS
                                  from OUTBILL_D A WITH(NOLOCK)) A
                             left join  outbill_d_sn sn WITH(NOLOCK) on a.ids = sn.outbill_d_ids                            
                             left join BASE_PART bp WITH(NOLOCK) on a.cinvcode = bp.CPARTNUMBER
                                   where A.ID='" + printId + "'";
        DataTable tb_d = DBHelp.ExecuteToDataTable(Sql_d);
        tb_d.TableName = "OUTBILL_D";

        DataSet ds = new DataSet();

        ds.Tables.Add(tb_d);
        ds.Tables.Add(tb);

        OutBillReport rep = new OutBillReport();
        rep.DataSource = ds;
        OutAsnReport1.Report = rep;
        OutAsnReport1.Report.FillDataSource();
        OutAsnReport1.DataBind();
    }
}