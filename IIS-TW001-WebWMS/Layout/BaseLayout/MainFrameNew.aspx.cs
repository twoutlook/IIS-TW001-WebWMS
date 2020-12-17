using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;
using DreamTek.ASRS.Business.Base;
using DreamTek.ASRS.Business.Others;

public partial class MainFrameNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string cssFile = this.GetRelativePath() + WmsWebUserInfo.GetCurrentUser().CSS_DIR + "/left.css";
            HtmlLink link = new HtmlLink();
            link.Href = cssFile;
            link.Attributes["type"] = "text/css";
            link.Attributes["rel"] = "stylesheet";

            Page.Header.Controls.Add(link);

            string expMsg = BaseCommQuery.ShowExpireMSG("WEB");
            if (!string.IsNullOrEmpty(expMsg))
            {
                this.Alert(expMsg);//提示即将过期信息
            }
        }
    }

    private void GetAllSites()
    {
        DataTable dt = new DataTable();
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["RightsSQL"].ToString();
        WmsWebUserInfo userInfo = WmsWebUserInfo.GetCurrentUser();

        if (dt == null || dt.Rows.Count == 0) return;

        StringBuilder sb = new StringBuilder("");
        sb.Append("<ul class='ulCompanystyle'>");
        string url = string.Empty;
        foreach (DataRow row in dt.Rows)
        {
            sb.Append("<li class='liCompanystyle'>");
            url = row["REMARK"].ToString();
            //去掉url中最后一个'/'
            if (!string.IsNullOrEmpty(url) && url.Length > 0 && url.Substring(url.Length - 1).Equals("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            sb.Append(string.Format("<a href=\"{0}/login.aspx?userName={1}&pwd={2}\" class='aCompanystyle'><img src=\"../Css/LG/Images/store.gif\" alt=\"\" class=\"imgCompanystyle\"/><p class=\"pCompanystyle\"> {3}</p></a>",
                             url, userInfo.UserNo,
                             HttpUtility.HtmlEncode(Comm_Function.DESEncrypt(userInfo.Password)),
                             row["APPNAME"].ToString()));
            sb.Append("</li>");
        }
        sb.Append("</ul>");

        pnlCompanyList.InnerHtml = sb.ToString();
    }

    /// <summary>
    /// 显示提示信息
    /// </summary>
    /// <param name="p_Message">提示信息字符串</param>
    public void Alert(string p_Message)
    {
        if (p_Message == null)
        {
            return;
        }
        if (p_Message.Contains("\r\n")) p_Message = p_Message.Replace("\r\n", "\\r\\n");
        if (p_Message.Contains("'")) p_Message = p_Message.Replace("'", "\'");
        if (p_Message.Contains(";")) p_Message = p_Message.Replace(";", "\\;");
        if (p_Message.Contains("\"")) p_Message = p_Message.Replace("\"", "");
        string p_Script = "alert('" + p_Message.ToJsString() + "')";
        this.Page.Page.Response.Write("<script type='text/javascript'>alert('" + p_Message + "');</script>");
    }

    public string GetRelativePath() {
        return "../../";
    }
}

