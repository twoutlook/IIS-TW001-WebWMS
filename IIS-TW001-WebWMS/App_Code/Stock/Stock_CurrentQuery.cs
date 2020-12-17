using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Stock_Current 的摘要说明
/// </summary>
public class Stock_CurrentQuery
{
    public Stock_CurrentQuery()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 检查库位上是否有货物
    /// pan gao
    /// </summary>
    /// <returns></returns>
    public bool CheckStockIncludedInfo(string cpositioncode)
    {
        bool bl = false;
        string sql = string.Format(" select count(1) from view_stock_current WHERE cpositioncode='{0}'  ", cpositioncode);
        string strNum = DBHelp.ExecuteScalar(sql).ToString();
        int i = 0;
        if (int.TryParse(strNum, out i))
        {
            if (i > 0)
            {
                bl = true;//该储位上有货物
            }
        }
        return bl;
    }
    /// 获取指定IDS的可锁定数量
    /// <summary>
    /// 获取指定IDS的可锁定数量
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public static decimal GetDateCodeLockNum(string ids)
    {
        string Sql = string.Format(@"select [dbo].[Fun_DateCode_Lock_Num]('{0}','0')", ids);
        object obj = DBHelp.ExecuteScalar(Sql);
        if (obj == null) return 0;
        else
        return Convert.ToDecimal(DBHelp.ExecuteScalar(Sql));
    }
    public DataTable GetDateCodeList_M(string cinvCode, string isAll)
    {
        string strSQL = string.Format(@"select bw.cwarename,
       ba.area_name,
       vc.ID,
       bw.CWARENAME CWAREHOUSE,
bw.CWAREID CWAREHOUSECODE,
       bc.CPOSITIONCODE,
       bc.CPOSITION,
       VC.CINVCODE,
       vc.CINVNAME,
       vc.CDATECODE,
       isnull(vc.IQTY, 0) IQTY,
       vc.IOCCUPYQTY,
       vc.LOCKNUM
  from base_cargospace bc
  left join base_area ba
    on bc.cdefine1 = ba.id
  left join view_stock_current vc
    on bc.cpositioncode = vc.cpositioncode
  left join base_warehouse bw
    on bc.warehouseid = bw.id
 where 1 = 1 and vc.CINVCODE like '%{0}%' and  bc.pallet_code = {1} ", cinvCode, isAll);

        DataTable dtSTOCK_DURATION = DBHelp.ExecuteToDataTable(strSQL);
        return dtSTOCK_DURATION;
    }
}