using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Apps_RD_GetAsnInfo_IDS_AjAx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string asn_d_ids = Request.QueryString["asn_d_ids"];
            string type = Request.QueryString["type"];
            if (asn_d_ids != null && asn_d_ids.Length > 0)
            {
                DataTable dt = this.GetAsnInFo(asn_d_ids, type);

                StringBuilder sb = new StringBuilder();
                if (dt != null && dt.Rows.Count > 0)
                {
                    //PO项次
                    sb.Append(dt.Rows[0]["IPOLINE"].ToString() + "|");
                    //物料条码
                    sb.Append(dt.Rows[0]["CINVBARCODE"].ToString() + "|");
                    //备注
                    sb.Append(dt.Rows[0]["CMEMO"].ToString() + "|");
                }
                Response.Write(sb.ToString());
                Response.End();
            }
        }  
    }

    /// <summary>
    /// 通过通知单明细IDS获取通知单信息
    /// </summary>
    /// <param name="asn_d_ids">通知单明细IDS</param>
    /// <param name="type">类型 IN</param>
    /// <returns></returns>
    public DataTable GetAsnInFo(string asn_d_ids, string type)
        {
            string sql = string.Empty;
            if (type == "IN")
            {
                sql = string.Format(@"select  iad.IPOLINE, iad.CINVBARCODE, iad.CMEMO
                    from InAsn_d iad where iad.ids = '{0}' 
                    and (iad.manual = 1 or (iad.manual = 0 and iad.cstatus = 0))", asn_d_ids);
            }
            else if (type == "OUT")
            {
                sql = string.Format(@"select  iad.ISOLINE IPOLINE, iad.CINVBARCODE, iad.CMEMO
                    from outasn_D iad where iad.ids = '{0}' 
                    and (iad.manual = 1 or (iad.manual = 0 and iad.cstatus = 0))", asn_d_ids);
            }
            return SqlDBHelp.ExecuteToDataTable(sql);
        }
}