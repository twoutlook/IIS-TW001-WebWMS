using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Apps_OUT_GetOutAsnInfoById : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string OutAsnId = Request.QueryString["OutAsnId"];
            if (OutAsnId != null && OutAsnId.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                using (var modContext = context) {
                    var modOutAsn = modContext.OUTASN.Where(x => x.id == OutAsnId).FirstOrDefault();
                    if (modOutAsn != null) {
                        //客戶名稱
                        sb.Append(modOutAsn.cclient + "|");
                        //客戶編碼
                        sb.Append(modOutAsn.cclientcode + "|");
                        //ERP單号
                        sb.Append(modOutAsn.cerpcode + "|");
                        //SO号
                        sb.Append(modOutAsn.cso + "|");
                        //出库类型
                        sb.Append(modOutAsn.itype + "|");
                        //备注
                        sb.Append(modOutAsn.cmemo + "|");
                        //投料點
                        sb.Append(modOutAsn.cdefine1 + "|");                   
                    }
                }

                Response.Write(sb.ToString());
                Response.End();
            }
        }
    }
}