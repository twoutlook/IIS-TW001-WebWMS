using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


    public partial class ShowNotcieDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.cssUrl.Attributes["href"] = "";
        }

        public void SetNoteciDetail(string strContent, string strTitle)
        {
            lbContent.Text = strContent.Replace("\n", "<br>").Replace(" ", "&nbsp;");
            lbTitle.Text = strTitle.Replace("\n", "<br>").Replace(" ", "&nbsp;");
            
        }
    }


