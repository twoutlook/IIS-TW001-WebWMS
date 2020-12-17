using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Apps_FrmINBILLEdit_Print : System.Web.UI.Page
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

        string Sql =string.Format(@"SELECT  ib.id ,
                                    ib.ccreateownercode ,
                                    CONVERT(NVARCHAR(100), ib.dcreatetime, 23) AS DCREATETIME ,
                                    ib.cauditperson ,
                                    CONVERT(NVARCHAR(100), ib.daudittime, 23) AS DAUDITTIME ,
                                    ib.cticketcode ,
                                    ib.cstatus ,
                                    sp.flag_name Status_Name ,
                                    ib.casnid ,
                                    ia.cticketcode ASNcode ,
                                    CONVERT(NVARCHAR(100), ib.dindate, 23) AS DINDATE ,
                                    ib.cmemo ,
                                    ib.cerpcode ,
                                    it.typename ,
                                    CONVERT(NVARCHAR(100), ib.ddefine3, 20) AS ddefine3 ,
                                    CONVERT(NVARCHAR(100), ib.debittime, 23) AS debittime ,
                                    ib.debitowner ,
                                    ib.cdefine1 ,
                                    ib.cdefine2,
                                    IA.WORKTYPE,
                                    case when ib.WORKTYPE=0 THEN '平仓' when ib.WORKTYPE=1 THEN '立库' end as worktypeName
                            FROM    INBILL ib
                                    LEFT JOIN INASN ia ON ib.casnid = ia.id
                                    LEFT JOIN INTYPE it ON ib.itype = it.cerpcode
                                    LEFT JOIN SYS_PARAMETER sp ON ib.cstatus = sp.flag_id
                                                              AND sp.flag_type = 'INBILL'
                            WHERE   ib.id = '{0}'", printId);
        DataTable tb = DBHelp.ExecuteToDataTable(Sql);
        tb.TableName = "INBILL";

        string Sql_d =string.Format(@"SELECT  ROW_NUMBER() OVER ( ORDER BY A.dindate DESC ) AS RNUM ,
                                                A.ids ,
                                                A.id ,
                                                A.cstatus ,
                                                A.cinvcode ,
                                                A.cinvname ,
                                                sn.quantity as iquantity,
                                                A.cinvbarcode ,
                                                A.cerpcodeline ,
                                                A.cmemo ,
                                                A.cpositioncode ,
                                                A.cposition ,
                                                CONVERT(NVARCHAR(100), A.dindate, 20) AS DINDATE ,
                                                A.cinpersoncode ,
                                                A.iasnline ,
                                                ( CASE A.asrs_status
                                                    WHEN 0 THEN '[入庫]'
                                                    WHEN 1 THEN '[取消]'
                                                    WHEN 8 THEN '[重試]'
                                                    ELSE ''
                                                  END ) AS LinkASRS_STATUS ,
                                                ( CASE A.asrs_status
                                                    WHEN 0 THEN '未處理'
                                                    WHEN 1 THEN '運作中'
                                                    WHEN 7 THEN '處理完成'
                                                    WHEN 8 THEN '處理異常'
                                                    ELSE ''
                                                  END ) AS ASRS_STATUS ,
                                                sn.furnaceno ,
                                                sn.datecode ,
                                                sn.sn_code ,
                                                bp.cunits
                                        FROM    INBILL_D A
                                                LEFT JOIN INBILL_D_SN sn ON sn.inbill_d_ids = A.ids
                                                LEFT JOIN BASE_PART bp ON A.cinvcode = bp.cpartnumber
                                        WHERE   A.id = '{0}'", printId);
        DataTable tb_d = DBHelp.ExecuteToDataTable(Sql_d);
        tb_d.TableName = "INBILL_D";

        DataSet ds = new DataSet();

        ds.Tables.Add(tb_d);
        ds.Tables.Add(tb);

        InBillReport rep = new InBillReport();
        rep.DataSource = ds;
        InAsnReport1.Report = rep;
        InAsnReport1.Report.FillDataSource();
        InAsnReport1.DataBind();
    }
}