using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// BaseUserControl 的摘要说明
/// </summary>
public class BaseUserControl : System.Web.UI.UserControl
{
	public BaseUserControl()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public int PageSize = 15;
    public DBContext db = new DBContext();

    /// <summary>
    /// 当前页数
    /// </summary>
    public int CurrendIndex
    {
        get
        {
            if (ViewState["CurrendIndex"] == null)
            {
                ViewState["CurrendIndex"] = 1;
            }
            return (int)ViewState["CurrendIndex"];
        }
        set
        {
            ViewState["CurrendIndex"] = value;
        }
    }

    public static IEnumerable<T> GetPageSize<T>(IEnumerable<T> source, int pageSize, int currendIndex)
    {
        return source.Take(pageSize * currendIndex).Skip(pageSize * (currendIndex - 1));
    }

    //public static bool IsNullOrEmpty(this string str)
    //{
    //    bool bl = false;
    //    if (string.IsNullOrEmpty(str))
    //    {
    //        bl = true;
    //    }
    //    return bl;
    //}


}

