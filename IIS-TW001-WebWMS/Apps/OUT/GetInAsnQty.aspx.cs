using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_OUT_GetInAsnQty : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string CINVCODE = Request.QueryString["CINVCODE"];
            string InAsn_Id = Request.QueryString["InAsn_Id"];

            InAsn query = new InAsn();
            Response.Write(query.GetInQtyByCinvcodeAndInAsnId(CINVCODE, InAsn_Id));
            Response.End();
        }
    }
}