using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// InType 的摘要说明
/// </summary>
public class InTypeLogic
{
    public InTypeLogic()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// 获取入库类型和名称
    /// <summary>
    /// 获取入库类型和名称
    /// </summary>
    /// <param name="typecode"></param>
    /// <returns></returns>
    public static DataTable GetInTypeName(string typecode)
    {
        string Sql = String.Format(@"select tp.typename,tp.cerpcode from intype tp where tp.cerpcode='{0}'", typecode);
        return DBHelp.ExecuteToDataTable(Sql);
    }
}