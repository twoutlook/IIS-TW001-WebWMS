using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Apps_FrmChangeByInAsnEdit_Print : System.Web.UI.Page
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

        string Sql = @"select ic.id,ic.cticketcode, ic.inasn_cticketcode, ic.create_owner, convert(nvarchar(20),ic.create_time,120)  create_time,
(case ic.cstatus when '0' then '未处理' when '1' then '已审核'when '2' then '处理中'when '3' then '已完成' end) cstatus
 from inasnchange ic                 
                        WHERE ic.id='" + printId + "'";
        DataTable tb = DBHelp.ExecuteToDataTable(Sql);
        tb.TableName = "INASNCHANGE";

        string Sql_d = @" select ROW_NUMBER() over(order by icd.cinvcode) AS RNUM,icd.id, icd.ids, icd.cinvcode, icd.cinvname, icd.cerpcodeline,icd.oldnum,
  icd.nownum, icd.create_owner, convert(nvarchar(20),icd.create_time,120)  create_time,
  (case icd.CSTATUS when '0' then '未处理' when '1' then '已完成' end ) CSTATUS from inasnchange_d icd
                                where icd.id='" + printId + "'";
        DataTable tb_d = DBHelp.ExecuteToDataTable(Sql_d);
        tb_d.TableName = "INASNCHANGE_D";

        DataSet ds = new DataSet();

        ds.Tables.Add(tb_d);
        ds.Tables.Add(tb);

        ChangeByInAsnReport rep = new ChangeByInAsnReport();
        rep.DataSource = ds;
        InAsnReport1.Report = rep;
        InAsnReport1.Report.FillDataSource();
        InAsnReport1.DataBind();
    }
}