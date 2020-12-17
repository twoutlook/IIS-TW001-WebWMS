using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Apps_OUT_FrmChangeByOutAsnEdit_Print : System.Web.UI.Page
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

        string Sql = @"select oc.id,oc.cticketcode, oc.outasn_cticketcode, oc.create_owner,oc.create_time,
(case  oc.cstatus when '0' then '未处理' when '1' then '已审核' when '2' then '处理中' when '3' then '已完成' end) cstatus from outasnchange oc
                         WHERE oc.id ='" + printId + "'";
        DataTable tb = DBHelp.ExecuteToDataTable(Sql);
        tb.TableName = "OUTASNCHANGE";

        string Sql_d = @"SELECT ROW_NUMBER() OVER (ORDER BY od.id ASC)  AS RNUM,od.id,od.ids,od.outasn_cticketcode, od.cinvcode, od.cinvname, od.oldnum, od.nownum, od.create_owner, od.create_time,
(case od.CSTATUS when '0' then '未处理' when '1' then '已完成' end) CSTATUS, 
                            od.CERPCODELINE,od.asn_d_ids from outasnchange_d od
                                   where od.id='" + printId + "'";
        DataTable tb_d = DBHelp.ExecuteToDataTable(Sql_d);
        tb_d.TableName = "OUTASNCHANGE_D";

        DataSet ds = new DataSet();

        ds.Tables.Add(tb_d);
        ds.Tables.Add(tb);

        OutAsnChangeReport rep = new OutAsnChangeReport();
        rep.DataSource = ds;
        OutAsnReport1.Report = rep;
        OutAsnReport1.Report.FillDataSource();
        OutAsnReport1.DataBind();
    }
}