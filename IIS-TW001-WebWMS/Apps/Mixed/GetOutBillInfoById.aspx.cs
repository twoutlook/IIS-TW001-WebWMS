using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Data.Entity.SqlServer;

public partial class Apps_Mixed_GetOutBillInfoById : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string cerpcode = Request.QueryString["cerpcode"];
            if (cerpcode != null && cerpcode.Length > 0)
            {
                IGenericRepository<OUTASN> con = new GenericRepository<OUTASN>(context);
                var caseList = from p in con.Get()
                               where p.cerpcode == cerpcode
                               select p;
                OUTASN entity = caseList.ToList().FirstOrDefault();
                StringBuilder sb = new StringBuilder();
                if (entity != null)
                {
                    //erpcode
                    //sb.Append(entity.cerpcode + "|");
                    //栈板/箱号：
                    //sb.Append(entity.palletcode + "|");
                    //出库类型
                    sb.Append(entity.itype + "|");
                    ////SO号
                    //sb.Append(outASNEntity.CSO + "|");
                    ////出库类型
                    //sb.Append(outASNEntity.ITYPE + "|");
                    ////备注
                    //sb.Append(outASNEntity.CMEMO + "|");
                    ////投料點
                    //sb.Append(outASNEntity.CDEFINE1 + "|");
                }
                Response.Write(sb.ToString());
                Response.End();
            }
        }
    }
}