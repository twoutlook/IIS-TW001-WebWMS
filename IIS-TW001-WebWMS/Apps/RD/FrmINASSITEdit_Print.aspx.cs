using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraReports.Web;
using Resources;

public partial class Apps_FrmINASSITEdit_Print : System.Web.UI.Page
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

        string Sql = @"SELECT A.ID,A.CCREATEOWNERCODE, CONVERT(VARCHAR(100),a.dcreatetime,23) as DCREATETIME,A.CTICKETCODE,A.CSTATUS,
                                    (select s.flag_name from Sys_Parameter s where s.flag_type='ASSIT' and s.flag_id=A.CSTATUS)CSTATUS_Name,
                                    A.CASNID,ia.cticketcode ASN_Code,it.typename,ia.itype,ia.CDEFINE1,ia.CDEFINE2
                              from INASSIT A left join inasn ia on A.CASNID=ia.ID left join inType it on ia.itype=it.cerpcode
                        WHERE A.ID='" + printId + "'";
        DataTable tb = DBHelp.ExecuteToDataTable(Sql);
        tb.TableName = "INASSIT";

        string Sql_d = @"SELECT ROW_NUMBER() OVER(ORDER BY a.cinvcode)  AS RNUM,A.IDS,A.ID,A.CSTATUS,CONVERT(DECIMAL(18,2),A.INUM) AS INUM,A.CPOSITIONCODE,
                        A.CPOSITION,A.CINVBARCODE,A.CINVCODE,A.CINVNAME,A.CBATCH,A.CMEMO,A.COPERATORCODE,A.COPERATOR,
                        A.CASNID,ia.cticketcode Inasn_Code,A.IASNLINE ,bp.calias
                                From INASSIT_D A 
								LEFT JOIN Inasn ia on a.casnid=ia.id
								LEFT JOIN dbo.BASE_PART bp ON a.cinvcode=bp.cpartnumber
                                where A.ID='" + printId + "'";
        DataTable tb_d = DBHelp.ExecuteToDataTable(Sql_d);
        tb_d.TableName = "INASSIT_D";

        DataSet ds=new DataSet();

        ds.Tables.Add(tb_d);
        ds.Tables.Add(tb);

        InAssitReport rep = new InAssitReport();
        rep.DataSource = ds;
        InAsnReport1.Report = rep;
        InAsnReport1.Report.FillDataSource();
        InAsnReport1.DataBind();
    }
}