using DreamTek.ASRS.Business.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_Get_Special_MergeErp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string mergeerp = Request.QueryString["vMer_Erp"];
            Response.Write(Comm_Function.Fun_Get_Special_MergeErp(mergeerp,0));
            Response.End();
        }
    }
}