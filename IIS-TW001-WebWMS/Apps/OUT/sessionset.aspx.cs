using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_OUT_sessionset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["SN"] != null)
        {
            Session["SN"] = Request.QueryString["SN"];
        }
        if (Request.QueryString["QTY"] != null)
        {
            Session["QTY"] = Request.QueryString["QTY"];
        }
        if (Request.QueryString["iType"] != null)
        {
            Session["iType"] = Request.QueryString["iType"];
        }
        if (Request.QueryString["ALLQTY"] != null)
        {
            Session["ALLQTY"] = Request.QueryString["ALLQTY"];
        }
        if (Request.QueryString["LINEQTY"] != null)
        {
            Session["LINEQTY"] = Request.QueryString["LINEQTY"];
        }
        if (Request.QueryString["TypeID"] != null)
        {
            Session["TypeID"] = Request.QueryString["TypeID"];
        }
        if (Request.QueryString["Carton"] != null)
        {
            Session["Carton"] = Request.QueryString["Carton"];
        }
        if (Request.QueryString["DateCode"] != null)
        {
            Session["DATECODE"] = Request.QueryString["DateCode"];
        }
        if (Request.QueryString["CSO"] != null)
        {
            Session["CSO"] = Request.QueryString["CSO"];
        }
        if (Request.QueryString["Vendor"] != null)
        {
            Session["Vendor"] = Request.QueryString["Vendor"];
        }
        if (Request.QueryString["NeedQty"] != null)
        {
            Session["OutBillNeedQty"] = Request.QueryString["NeedQty"];
        }
    }
}