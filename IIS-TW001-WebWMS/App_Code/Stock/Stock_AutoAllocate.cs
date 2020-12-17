using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Stock_AutoAllocate 的摘要说明
/// </summary>
public class Stock_AutoAllocate
{
	public Stock_AutoAllocate()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// 判断是否存在未扣帐的物料配送单
    /// <summary>
    /// 判断是否存在未扣帐的物料配送单
    /// </summary>
    /// <param name="erpcode"></param>
    /// <returns></returns>
    public static bool CheckAutoAlloCount(string erpcode)
    {
        string Sql = string.Format(@"select count(*)
                                          from allocate te
                                         where te.cstatus not in ('2', '3', '9')
                                           and te.special = 2
                                           and te.cerpcode = '{0}'", erpcode);
        return Convert.ToInt32(DBHelp.ExecuteScalar(Sql)) > 0 ? true : false;
    }
}