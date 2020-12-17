using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

/// <summary>
/// ErrorMsg_Query 的摘要说明
/// </summary>
public class ErrorMsg_Query
{
	public ErrorMsg_Query()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 获取ERROR消息
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public string GetErrorMsg(string guid)
    {
        string sql = "select * from CHECKERRORMSG  where guid='" + guid + "' order by CREATEDATE";
        StringBuilder sb = new System.Text.StringBuilder();
        //DataTable errorDt = DBUtil.Fill(sql);            
        DataTable errorDt = DBHelp.ExecuteToDataTable(sql); 
        if (errorDt != null && errorDt.Rows.Count > 0)
        {
            foreach (DataRow row in errorDt.Rows)
            {
                sb.Append(row["errormsg"] + "\r\n");
            }
        }
        string deleteSql = "delete from CHECKERRORMSG  where guid='" + guid + "'";
        DBHelp.ExecuteNonQuery(deleteSql);
        return sb.ToString();
    }
}