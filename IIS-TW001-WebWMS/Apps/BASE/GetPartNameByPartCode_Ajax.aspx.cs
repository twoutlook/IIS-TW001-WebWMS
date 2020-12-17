using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_GetPartNameByPartCode_Ajax : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string PartCode = Request.QueryString["PartCode"];
            if (PartCode != null && PartCode.Length > 0)
            {
                try
                {
                    Response.Write(this.GetCINVNAMEByCINVCODE(PartCode));
                }
                catch (Exception)
                {
                }                
            }
            Response.End();
        }
    }

    public string GetCINVNAMEByCINVCODE(string CINVCODE)
    {
        var modPart = db.BASE_PART.Where(x => x.cpartnumber == CINVCODE).FirstOrDefault();
        if (modPart != null)
        {
            return modPart.cpartname;
        }
        else {
            return "";
        }
    }
}