using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_RD_GetInAsnInfoById : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string InAsnId = Request.QueryString["InAsnId"];
            if (InAsnId != null && InAsnId.Length > 0)
            {
                //RD_FrmINASNListQuery query = new RD_FrmINASNListQuery();
                DataTable dt = GetInAsnById(InAsnId);

                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    //主表ERP单号
                    sb.Append(dt.Rows[0]["CERPCODE"].ToString() + "|");
                    //贸易代码
                    sb.Append(dt.Rows[0]["CDEFINE1"].ToString() + "|");
                    //币别
                    sb.Append(dt.Rows[0]["CDEFINE2"].ToString() + "|");
                    //入库类型
                    sb.Append(dt.Rows[0]["ITYPE"].ToString() + "|");
                    //备注
                    sb.Append(dt.Rows[0]["CMEMO"].ToString() + "|");
                }
                Response.Write(sb.ToString());
                Response.End();
            }
        }
    }

    /// <summary>
    /// 根据入库通知单ID获取入库通知单信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public DataTable GetInAsnById(string id)
    {
        string sql = "select * from Inasn ia where ia.id='" + id + "'";

        return DBHelp.ExecuteToDataTable(sql);
    }

}