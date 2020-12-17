using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class DefaultMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            //this.cssUrl.Href ="~/Layout/CSS/" + WebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
            if (WmsWebUserInfo.IsLogin())
            {
                this.cssUrl.Href = "~/Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
                this.cssUrlBackground.Href = "~/Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/MasterPage.css";
            }       
           
            
        }
    }
}
