using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Apps_FrmInPo_IAEdit_Print : System.Web.UI.Page
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

        string Sql = @"select distinct ia.id,ia.cticketcode,ia.pono,ia.batchno,ia.cerpcode,ia.tradecode,ia.currency,
ia.ddefine1,ia.cvendercode,ia.cvender,sp.flag_name status,ia.ccreateownercode,CONVERT(varchar(100),ia.dcreatetime, 23) as dcreatetime,
ia.lastupdateowner,CONVERT(varchar(100),ia.lastupdatetime, 23) as lastupdatetime
                                from inasn_ia ia 
                                left join inasn_ia_d iad on iad.id=ia.id
                                left join sys_parameter sp on sp.flag_id=ia.cstatus and sp.flag_type='IN_PO_IA'                                
                        WHERE ia.id='" + printId + "'";
        DataTable tb = SqlDBHelp.ExecuteToDataTable(Sql);
         
        tb.TableName = "INASN_IA";

        string Sql_d = @"select ROW_NUMBER() over(order by A.createtime) AS RNUM, A.ID,A.IDS, A.Erpcodeline,A.PONO,A.POLINE,A.CINVCODE,A.CINVNAME,A.Datecode,A.Qtytotal,A.Qtypassed,A.QTYUNPASSED,
A.QTYPENDING,sp.flag_name from INASN_IA_D A
                              left join sys_parameter sp on sp.flag_type='IN_PO_IA_D' and sp.flag_id=A.Status
                                where A.ID='" + printId + "'";
        DataTable tb_d = SqlDBHelp.ExecuteToDataTable(Sql_d);
        tb_d.TableName = "INASN_IA_D";

        DataSet ds = new DataSet();

        ds.Tables.Add(tb_d);
        ds.Tables.Add(tb);

        InAsn_IAReport rep = new InAsn_IAReport();
        rep.DataSource = ds;
        InAsnReport1.Report = rep;
        InAsnReport1.Report.FillDataSource();
        InAsnReport1.DataBind();
    }
}