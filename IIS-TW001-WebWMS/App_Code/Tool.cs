using System;
using System.Data;
using System.Collections.Generic;
/// <summary>
///Tool 的摘要说明
/// </summary>
public class Tool
{
    public Tool()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //

    }
    /// <summary>
    /// 提示对话框
    /// </summary>
    /// <param name="Value"></param>
    public static void Alert(string Value)
    {
        System.Web.HttpContext.Current.Response.Write("<script>alert('" + Value + "')</script>");

    }
    /// <summary>
    /// 获取Guid
    /// </summary>
    /// <returns></returns>
    public static string getGUID()
    {
        System.Guid guid = new Guid();
        guid = Guid.NewGuid();
        string str = guid.ToString();
        return str;
    }
    /// <summary>  
    /// 根据GUID获取16位的唯一字符串  
    /// </summary>  
    /// <param name=\"guid\"></param>  
    /// <returns></returns>  
    //public static string GuidTo16String(long Ticks)
    //{
    //    long i = 1;
    //    foreach (byte b in Guid.NewGuid().ToByteArray())
    //        i *= ((int)b + 1);
    //    long a = DateTime.Now.Ticks;
    //    return string.Format("{0:x}", i - Ticks);
    //}

    /// <summary>
    /// 非空验证
    /// </summary>
    /// <param name="Dictionary">第一个参数:出现为空的提示文字，第二个参数是：要验证的文本</param>
    /// <returns></returns>
    public static bool IsNullCheck(Dictionary<string, string> Dictionary)
    {
        bool falge = true;
        foreach (var item in Dictionary)
        {
            if (item.Value.IsNullOrEmpty())
            {
                Alert(item.Key);
                return false;
            }
        }
        return falge;
    }
    /// <summary>
    /// 返回当前登录用户ID
    /// </summary>
    /// <returns></returns>
    public static string User() { return WmsWebUserInfo.GetCurrentUser().UserNo; }  
  
   
}