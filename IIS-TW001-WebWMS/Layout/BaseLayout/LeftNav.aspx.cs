using DreamTek.ASRS.Business.Tools;
using DreamTek.WMS.DAL.Model.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Layout_BaseLayout_LeftNav : System.Web.UI.Page
{

    public string RightUrl
    {
        get
        {
            if (ViewState["RightUrl"] != null)
            {
                return ViewState["RightUrl"].ToString();
            }
            return "";
        }
        set
        {
            ViewState["RightUrl"] = value;
        }
    }

    public string CompayNO
    {
        get 
        {
            if (ViewState["CompayNO"] != null)
            {
                return ViewState["CompayNO"].ToString();
            }
            return string.Empty;
        }
        set
        {
            ViewState["CompayNO"] = value;
        }
    }

    public string ProjectNO
    {
        get
        {
            if (ViewState["ProjectNO"] != null)
            {
                return ViewState["ProjectNO"].ToString();
            }
            return string.Empty;
        }
        set
        {
            ViewState["ProjectNO"] = value;
        }
    }

    public string Language
    {
        get
        {
            if (ViewState["Language"] != null)
            {
                return ViewState["Language"].ToString();
            }
            return "zh-CN";
        }
        set
        {
            ViewState["Language"] = value;
        }
    }

    public string UserNO
    {
        get
        {
            var userNO = WmsWebUserInfo.GetCurrentUser().UserNo;
            return userNO;
        }
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string buttonUrl = ConfigurationManager.AppSettings["RightUrl"] + "RightsAPI/GetBtnListByUserJson";
            RightUrl = ConfigurationManager.AppSettings["RightUrl"] + "RightsAPI/GetMenuListByUserJson";
            CompayNO = ConfigurationManager.AppSettings["CompayNO"];
            ProjectNO = ConfigurationManager.AppSettings["ProjectNO"];
            if (Request.Cookies["language"] != null)
            {
                Language = Request.Cookies["language"].Value;
            }

            //1.读取菜单列表
            string url = string.Format(@"{0}?userno={1}&companyno={2}&projectno={3}&language={4}", RightUrl, UserNO, CompayNO, ProjectNO, Language);
            string jsonStr = WebPageExtension.ExecuteGetUrl(url);
            string script = string.Format(@" var RightMenuList = {0}; ", jsonStr);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "addScript", script, true);
            Session["RightMenuList"] = jsonStr;

            //2.读取按钮权限列表
            url = string.Format(@"{0}?userno={1}&companyno={2}&projectno={3}&language={4}", buttonUrl, UserNO, CompayNO, ProjectNO, Language);
            jsonStr = WebPageExtension.ExecuteGetUrl(url);
            List<V_GetBtnFunctionListEntity> btnList = null;
            try
            {
                btnList = WebPageExtension.DeserializeObject<List<V_GetBtnFunctionListEntity>>(jsonStr);
            }
            catch(Exception ex)
            {
            
            }
            Session["ButtonList"] = btnList;
        }
    }

}