using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Drawing;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using DreamTek.ASRS.Business.Others;


namespace DreamTek.WebWMS.Web
{
	/// <summary>
	/// 
	/// </summary>
    public partial class FrmTopCompany : WMSBasePage
	{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.cssUrl.Href = "../../" + WmsWebUserInfo.GetCurrentUser().CSS_DIR.ToString() + "/top.css";

            lblSessionUserName.InnerText = WmsWebUserInfo.GetCurrentUser().UserNo;
            lblDate.InnerText = "(" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ")";

            //WinBuilder.EnumWinFuture winFuture = WinBuilder.EnumWinFuture.HaveAddressBar | WinBuilder.EnumWinFuture.HaveMenuBar | WinBuilder.EnumWinFuture.Resizable | WinBuilder.EnumWinFuture.HaveToolBar
            if (!IsPostBack)
            {
                //CheckCompanyCount();
                this.imgLogo.ImageUrl = "~/Layout/Css/LG/Images/Top/top_log.gif" + "?v=" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
        }

        //private void CheckCompanyCount()
        //{
        //    DataTable dt = new DataTable();
        //    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["RightsSQL"].ToString();
        //    dt = Comm_Function.GetAppsList(conn, "1001");
        //    //string url = Request.Url.GetLeftPart(UriPartial.Authority);
        //    WmsWebUserInfo userInfo = WmsWebUserInfo.GetCurrentUser();

        //    if (dt == null) return;
        //    if (dt.Rows.Count == 1)
        //    {
        //        aChange.Visible = false;
        //    }
        //}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
