using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;
using System.Collections;
using System.Web.Security;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;

public partial class PCMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            //this.cssUrl.Href ="~/Layout/CSS/" + WebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
            if (WmsWebUserInfo.IsLogin())
            {
                this.cssUrl.Href = "~/Layout/CSS/LG/Page.css";
                this.cssUrlBackground.Href = "~/Layout/CSS/LG/MasterPage.css";
            }
        }
    }
}