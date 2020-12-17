using DreamTek.ASRS.Business.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_Get_Special_MergeLineID : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string mergeline = Request.QueryString["vMer_LineID"];
            Response.Write(Comm_Function.Fun_Get_Merge_LineID(mergeline, 0));
            Response.End();
        }
    }
}