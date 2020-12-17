using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraReports.Web;
using Resources;
using DreamTek.ASRS.DAL;

public partial class Apps_FrmInPoEdit_Print : System.Web.UI.Page
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

        string Sql = @"select p.id,p.pono,pa.flag_name as potype,CONVERT(varchar(100),p.podate, 23) as podate,p.vendorid,p.vendorname,sp.flag_name status,p.currency,p.paymentterm,p.shipfrom,p.shipto,
(case when p.source='0' then 'WMS' when p.source='1' then 'ERP' else '其他' end) source,CONVERT(varchar(100),p.createtime, 23) as createtime,p.createowner
 from inpo p 
                                left join sys_parameter sp on sp.flag_id=p.status and sp.flag_type='IN_PO'
                                left join sys_parameter pa on pa.flag_id=p.potype and pa.flag_type='IN_PO_TYPE'
                        WHERE p.id='" + printId + "'";
        DataTable tb = SqlDBHelp.ExecuteToDataTable(Sql);
        tb.TableName = "INPO";

        string Sql_d = @"select ROW_NUMBER() over(order by A.createtime) AS RNUM,A.Ids,A.Id,A.Poline,A.Cinvcode,A.Cinvname,A.IQUANTITY,A.Unit,A.PRICE,(A.Price*A.Iquantity) total,A.STATUS from inpo_d A
                                where A.ID='" + printId + "'";

        SqlDBHelp.ExecuteToDataTable(Sql_d);
        DataTable tb_d = SqlDBHelp.ExecuteToDataTable(Sql_d);
        tb_d.TableName = "INPO_D";

        DataSet ds = new DataSet();

        ds.Tables.Add(tb_d);
        ds.Tables.Add(tb);

        InPoReport rep = new InPoReport();
        rep.DataSource = ds;
        InAsnReport1.Report = rep;
        InAsnReport1.Report.FillDataSource();
        InAsnReport1.DataBind();
    }
}