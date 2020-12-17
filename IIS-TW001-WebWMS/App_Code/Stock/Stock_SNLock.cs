using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Stock_SNLock 的摘要说明
/// </summary>
public class Stock_SNLock
{
	public Stock_SNLock()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// 判斷輸入字符串是否是數字，且是整數 是true 否false
    /// <summary>
    /// 判斷輸入字符串是否是數字，且是整數 是true 否false
    /// </summary>
    /// <param name="message">輸入字符</param>
    /// <param name="result">返回值</param>
    /// <returns>是true 否false</returns>
    public static bool IsInteger(string message, out int result)
    {
        result = -1;   //result 定义为out 用来输出值 
        try
        {
            result = Convert.ToInt32(message);
            return true;
        }
        catch
        {
            return false;
        }
    }
}