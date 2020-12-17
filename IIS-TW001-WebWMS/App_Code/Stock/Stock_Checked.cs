using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Stock_Checked 的摘要说明
/// </summary>
public class Stock_Checked
{
	public Stock_Checked()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 获取限制功能点
    /// </summary>
    /// <param name="struseno"></param>
    /// <returns></returns>
    public static bool CheckPoint(string struseno, string strpoint)
    {
        bool point = false;
        try
        {
            string Sql = string.Format(@" select count(*) from USERFUNCTION t 
                                              where t.funcno = '{0}' and t.userno ='{1}'", strpoint, struseno);
            return Convert.ToInt32(DBHelp.ExecuteScalar(Sql).ToString()) > 0 ? true : false;
        }
        catch (Exception)
        {
            point = false;
        }
        return point;
    }
    /// 检查冻结的SN是否已经关联
    /// <summary>
    /// 检查冻结的SN是否已经关联
    /// </summary>
    /// <param name="sncode"></param>
    /// <returns></returns>
    public static bool CheckSNIs_ExistErp(string sncode)
    {
        string Sql = string.Format(@"select count(1) from stock_sn_erpunlock se where se.cstatus=0 and se.sncode='{0}'", sncode);
        return Convert.ToInt32(DBHelp.ExecuteScalar(Sql)) > 0 ? true : false;
    }
    //判断是否存在有效计划
    public static bool GetFlag(string Flag, string ID)
    {
        string sql = "";
        if (Flag == "0")//查询是否为无效状态
        {
            sql = "select count(*) from STOCK_CHECK_PLAN t where cstatus = '0' and ID = '" + ID + "'";
        }
        else if (Flag == "1")//查询是否为有效状态
        {
            sql = "select count(*) from STOCK_CHECK_PLAN t where cstatus = '1' and ID = '" + ID + "'";
        }
        else if (Flag == "2")//查询
        {
            sql = "select count(*) from STOCK_CHECK_PLAN t where cstatus = '1' ";
        }
        return Convert.ToInt32(DBHelp.ExecuteScalar(sql)) == 0 ? false : true;
    }

    //更改状态
    public static void UpdateStatus(string Flag, string userno, string ID)
    {
        var sql = "";
        if (Flag.Equals("0"))//更改其他可用为无效
        {
            sql = "update STOCK_CHECK_PLAN set LAST_UPD_OWNER = '" + userno +
                  "', LAST_UPD_TIME=Getdate(),CSTATUS='0' where CSTATUS='1'";
        }
        else if (Flag.Equals("1"))//更改无效-有效
        {
            sql = "update STOCK_CHECK_PLAN  set LAST_UPD_OWNER = '" + userno +
                  "', LAST_UPD_TIME=Getdate(), CSTATUS='1' where CSTATUS='0' and ID = '" + ID + "'";
        }
        else if (Flag.Equals("2"))//更改有效-无效
        {
            sql = "update STOCK_CHECK_PLAN  set LAST_UPD_OWNER = '" + userno +
                  "', LAST_UPD_TIME=Getdate(), CSTATUS='0' where CSTATUS='1' and ID = '" + ID + "'";
        }

        DBHelp.ExecuteNonQuery(sql);
    }
}