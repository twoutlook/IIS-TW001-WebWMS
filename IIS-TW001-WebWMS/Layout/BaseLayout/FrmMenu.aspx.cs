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
using System.Text.RegularExpressions;

public partial class FrmMenu : System.Web.UI.Page
{
    private string GetRelativeURL(string url)
    {
        if (url.IsNullOrEmpty())
            return url;
        string pattern = "(http(s){0,1})|(javascript):";
        Regex r = new Regex(pattern, RegexOptions.Multiline);
        Match m = r.Match(url);
        if (m.Success == false || m.Index != 0)
        {
            url = "../../" + url;
            return url;
        }
        else
        {
            return url;
        }
    }
    

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Response.Clear();
        //MenuEntity menuModelEntity = new MenuEntity();
        Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        Response.Write("<tree id=\"0\">");
        int level = ConfigurationManager.AppSettings["ModuleLevel"].ToInt();
        RightsWS.RightsServiceForMultiApp rightsWS = new RightsWS.RightsServiceForMultiApp();
        ////rightsWS.GetNavigatorMenu(
        //List<SYSTEM_TMODULEEntity> moduleList = SYSTEM_TMODULEQuery.GetModuleList(WmsWebUserInfo.GetCurrentUser().RootMenuID, WmsWebUserInfo.GetCurrentUser().PowerList, level);

        //foreach (SYSTEM_TMODULEEntity moduleEntity in moduleList)
        //{
        //    Response.Write("<item text=\"" + moduleEntity.FMAINALIAS + "\" id=\"mLevel_" + moduleEntity.ID + "\" open=\"true\">");
        //    if (moduleEntity.M_LINK.IsNullOrEmpty() == false)
        //    {
        //        Response.Write("<userdata name=\"url\">" + GetRelativeURL(moduleEntity.M_LINK).ToXMLString() + "</userdata>");
        //    }

        //    foreach (SYSTEM_TMODULEEntity subMenu in moduleEntity.Children)
        //    {
        //        ///处理权限管理平台这个功能点
        //        if (subMenu.FAUTHORITYID == 903.0M)
        //        {
        //            try
        //            {
        //                subMenu.M_LINK = string.Format(subMenu.M_LINK,WmsWebUserInfo.GetCurrentUser().UserNo,WmsWebUserInfo.GetCurrentUser().GUID);
        //            }
        //            catch
        //            {

        //            }
        //        }
        //        Response.Write("<item text=\"" + subMenu.FMAINALIAS + "\" id=\"mLevel_" + subMenu.ID + "\" open=\"true\">");
        //        if (subMenu.M_LINK.IsNullOrEmpty() == false)
        //        {
        //            Response.Write("<userdata name=\"url\">" + GetRelativeURL(subMenu.M_LINK).ToXMLString() + "</userdata>");
        //        }
        //        if (level == 3)
        //        {
        //            foreach (SYSTEM_TMODULEEntity grandsonMenu in subMenu.Children)
        //            {
        //                ///处理权限管理平台这个功能点
        //                if (grandsonMenu.FAUTHORITYID == 903.0M)
        //                {
        //                    try
        //                    {
        //                        grandsonMenu.M_LINK = string.Format(grandsonMenu.M_LINK,WmsWebUserInfo.GetCurrentUser().UserNo,WmsWebUserInfo.GetCurrentUser().GUID);
        //                    }
        //                    catch
        //                    {

        //                    }
        //                }
        //                Response.Write("<item text=\"" + grandsonMenu.FMAINALIAS + "\" id=\"mLevel_" + grandsonMenu.ID + "\" open=\"true\">");
        //                if (grandsonMenu.M_LINK.IsNullOrEmpty() == false)
        //                {
        //                    Response.Write("<userdata name=\"url\">" + GetRelativeURL(grandsonMenu.M_LINK).ToXMLString() + "</userdata>");
        //                }
        //                Response.Write("</item>");
        //            }
        //        }
        //        Response.Write("</item>");
        //    }
        //    Response.Write("</item>");
        //}
        //Response.Write("</tree>");
    }
}

