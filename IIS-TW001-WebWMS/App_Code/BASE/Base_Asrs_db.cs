using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// BASE_ASRS_DB 的摘要说明
/// </summary>
public class Base_Asrs_db
{
	public Base_Asrs_db()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static string GetDBID()
    {
        string returnvalue = ""; int vCount = 0;
        string Sql = string.Format(@"select count(*) from BASE_ASRS_DB ba");    
        object obj = DBHelp.ExecuteScalar(Sql);
        if (obj != null) vCount = Convert.ToInt32(DBHelp.ExecuteScalar(Sql));
        if (vCount > 0)
        {
            string Sql2 = string.Format(@"select ba.id from BASE_ASRS_DB ba ");
            returnvalue = DBHelp.ExecuteScalar(Sql2).ToString();
        }
        return returnvalue;
    }
}