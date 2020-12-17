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

        string Sql = @"SELECT IA.ID,IA.CTICKETCODE,
                               TY.TYPENAME,
                               IA.CERPCODE,
                               IA.CPO,
                               IA.CDEFINE1,
                               IA.CDEFINE2,
                               IA.CVENDER,
                               IA.CVENDERCODE,
							   CASE WHEN LTRIM(IA.DDEFINE3)='Y' then '是' else '否' end as DDEFINE3,
                               IA.CCREATEOWNERCODE,
							   CONVERT(NVARCHAR(10),IA.DCREATETIME,120)as DCREATETIME,
                               IA.WORKTYPE,case when IA.WORKTYPE=0 THEN '平仓' when IA.WORKTYPE=1 THEN '立库' end as worktypeName          
                          FROM INASN IA
                          LEFT JOIN INTYPE TY
                            ON TY.CERPCODE = IA.ITYPE
                        WHERE IA.ID='" + printId + "'";
        DataTable tb = DBHelp.ExecuteToDataTable(Sql);
        tb.TableName = "INASN";

        string Sql_d = @"SELECT ROW_NUMBER() over(order by A.CINVCODE) RNUM,A.ID,A.IDS,A.CINVCODE,
									 p.calias AS CALIAS,
                                     A.CINVNAME,
                                     A.IQUANTITY,
                                     A.CERPCODELINE,
                                     A.PO_NUMBERNAME,
                                     A.PO_LINENUMBERNAME,
                                     dbo.Fun_GetInOrOutBill_D_Qty(A.CINVCODE,A.Cerpcodeline,A.Id,0) InBill_Qty,
                                     dbo.Fun_GetInOrOutBill_D_Qty(A.CINVCODE,A.Cerpcodeline,A.Id,1) InBilled_Qty                                    
                                from INASN_D A 
								JOIN dbo.BASE_PART p WITH(NOLOCK) ON A.cinvcode = p.cpartnumber
                                where A.ID='" + printId + "'";
        DataTable tb_d = DBHelp.ExecuteToDataTable(Sql_d);
        tb_d.TableName = "INASN_D";

        DataSet ds=new DataSet();

        ds.Tables.Add(tb_d);
        ds.Tables.Add(tb);
        
        InAsnReport rep = new InAsnReport();
        rep.DataSource = ds;
        InAsnReport1.Report = rep;
        InAsnReport1.Report.FillDataSource();
        InAsnReport1.DataBind();
    }
}