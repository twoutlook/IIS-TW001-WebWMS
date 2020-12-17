using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Stock_CheckBill 的摘要说明
/// </summary>
public class Stock_CheckBill
{
	public Stock_CheckBill()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 获取明细表储位
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static DataTable GetCheckBill_List(string id)
    {
        string strSQL = string.Format("select distinct cpositioncode from STOCK_CHECKBILL_D where id = '{0}'", id);
        return DBHelp.ExecuteToDataTable(strSQL);
    }

    /// <summary>
    /// 锁定储位
    /// </summary>
    /// <param name="cpocode"></param>
    /// <param name="dcode"></param>
    public static void UpdateCpoStatus(string cpocode,string dcode)
    {
        string strSQL = string.Format("update BASE_CARGOSPACE set cstatus ='1' ,lastcstatus =(select b.cstatus from BASE_CARGOSPACE b where b.cpositioncode='{1}')  where cpositioncode = '{1}'", dcode, cpocode);
        DBHelp.ExecuteToDataTable(strSQL);
    }

    /// <summary>
    /// 还原储位状态
    /// </summary>
    /// <param name="cpocode"></param>
    /// <param name="dcode"></param>
    public static void UpdateCpoStatus(string cpocode)
    {
        string strSQL = string.Format("update BASE_CARGOSPACE set cstatus ='0' ,lastcstatus =(select b.cstatus from BASE_CARGOSPACE b where b.cpositioncode='{0}') where cpositioncode = '{0}'", cpocode);
        DBHelp.ExecuteToDataTable(strSQL);
    }
}