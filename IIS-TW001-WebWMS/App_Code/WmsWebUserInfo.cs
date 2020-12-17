using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

/// <summary>
/// WebUserInfo 的摘要说明
/// </summary>
public class WmsWebUserInfo
{
    public WmsWebUserInfo()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    // 摘要:
    //     存放在会话中的关键字
    public const string SESSION_KEY = "CurrentUser";

    // 摘要:
    //     实现同一个账户在某一时刻只能有一个登录
    public static bool OnlyOneOnline;

    // 摘要:
    //     实现换肤的需要 样式的URL 2009-10-21
    public string CSS_DIR { get; set; }
    //
    // 摘要:
    //     实现换肤的需要 样式的名称（不是具体的文件名称）
    public string CSS_Name { get; set; }
    //
    // 摘要:
    //     全局ID, 为实现同一个账户在某一时刻只能有一个登录,后登陆者会踢掉前面的登录
    public string GlobalID { get; set; }
    public decimal RootMenuID { get; set; }
    //
    // 摘要:
    //     登陆页面，相对于本Web程序的根目录的相对路径，或者绝对路径 如果是相对于Web程序的根目录的相对路径，请在字符串开始位置不要加"/"; 如果是绝对路径，请加上http://
    //     或者https://
    public static string LoginPage { get { return "Layout/Baselayout/Login.aspx"; } set { } }
   
    //
    // 摘要:
    //     判断是否有权限
   
    //

    public List<string> BeProxyPowerList { get; set; }
    //
    // 摘要:
    //     被代理的角色列表
    public List<string> BeProxyRoleList { get; set; }
    //
    // 摘要:
    //     被代理的用户列表
    public List<string> BeProxyUserList { get; set; }
    //
    // 摘要:
    //     用户所在单位
    public string CompanyName { get; set; }
    //
    // 摘要:
    //     用户所在公司编号
    public string CompID { get; set; }
    //
    // 摘要:
    //     单位类型
    public Enum CompType { get; set; }
    //
    // 摘要:
    //     扩展属性
    public string Extends { get; set; }
    //
    // 摘要:
    //     登陆密码
    public string Password { get; set; }
    //
    // 摘要:
    //     有或者无权限
    public List<string> PowerList { get; set; }
    //
    // 摘要:
    //     只读权限
    public List<string> ReadList { get; set; }
    //
    // 摘要:
    //     角色列表
    public List<string> RoleList { get; set; }
    //
    // 摘要:
    //     用户序号
    public int UserID { get; set; }
    //
    // 摘要:
    //     用户名称
    public string UserName { get; set; }
    //
    // 摘要:
    //     用户登录帐号,非整型的ID号
    public string UserNo { get; set; }
    public string GUID { get; set; }
    public string AppNo { get; set; }
    //


    public static WmsWebUserInfo GetCurrentUser()
    {
        return GetCurrentUser_Inner();
    }
    private static WmsWebUserInfo GetCurrentUser_Inner()
    {
        WmsWebUserInfo info;
        HttpSessionState session = HttpContext.Current.Session;

        var user = session["CurrentUser"];

        if (user != null)
        {
            info = (WmsWebUserInfo)user;
        }
        else
        {
            Relogin("尚未登录或者会话已超时");
            return null;
        }
        return info;
    }

    private static void Relogin(string message)
    {
        HttpResponse response = HttpContext.Current.Response;
        response.Clear();
        HttpContext.Current.Session.Abandon();
        string applicationPath = HttpContext.Current.Request.ApplicationPath;

        if (applicationPath[applicationPath.Length - 1] == '/')
        {
            applicationPath = applicationPath.Substring(0, applicationPath.Length - 1);
        }
        response.Write("<script type='text/javascript'>alert('" + message + "');window.top.location.href = '../../Layout/BaseLayout/Login.aspx';</script>");
         
    }
    // 摘要:
    //     写权限
    public List<string> WriteList { get; set; }

    public static bool IsLogin()
    {
        try
        {
            if (HttpContext.Current.Session["CurrentUser"] == null)
            {
                return false;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }



    public string Language { get; set; }
}