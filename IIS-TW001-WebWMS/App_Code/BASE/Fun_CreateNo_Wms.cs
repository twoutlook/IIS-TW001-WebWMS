using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Fun_CreateNo 的摘要说明
/// </summary>
public class Fun_CreateNo_Wms
{
    public Fun_CreateNo_Wms()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 生成单据号
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public string CreateNo(string tableName)
    {
        string sql = "select [dbo].[Fun_CreateNo]('" + tableName + "') newNo";

        return DBHelp.ExecuteScalar(sql).ToString();
    }
}