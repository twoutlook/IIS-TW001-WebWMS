using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Base_Part 的摘要说明
/// </summary>
public class Base_Part
{
	public Base_Part()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 检查当前料号是否在通知单内
    /// </summary>
    /// <param name="Bill_Id">出入库单ID</param>
    /// <param name="PartNumber">料号</param>
    /// <returns>返回检查消息 OK 或 其他</returns>
    public static string CheckWIP_PartByBillId(string Bill_Id, string PartNumber)
    {
        string sql = "select [dbo].[Fun_CheckWIP_PartByBillId]('" + Bill_Id.Trim() + "','" + PartNumber.Trim() + "')";

        return DBHelp.ExecuteScalar(sql).ToString().ToUpper();
    }
    /// 通过料号获取料号是保税非保税
    /// <summary>
    /// 通过料号获取料号是保税非保税
    /// </summary>
    /// <param name="cinvcode"></param>
    /// <returns></returns>
    public static string GetBond_FromPart(string cinvcode)
    {
        string Return = "";
        string Sql = string.Format(@"select case bp.bonded when 0 then 'Y' else 'N' end from base_part bp where bp.cpartnumber='{0}'", cinvcode);
        object obj = DBHelp.ExecuteScalar(Sql);
        if (obj!=null)
          Return=DBHelp.ExecuteScalar(Sql).ToString();
        return Return;
    }
    //pan gao 20160603
    /// <summary>
    /// 根据产品编码
    /// </summary>
    /// <param name="CPARTNUMBER"></param>
    /// <returns></returns>
    public static string GetPRODUCTCODEByCpartNumber(string cpartNumber)
    {
        string sql = "select top 1 PRODUCTCODE from base_part part where part.CPARTNUMBER='" + cpartNumber.Trim() + "'";

        try
        {
            return DBHelp.ExecuteScalar(sql).ToString();
        }
        catch (Exception)
        {
            return "";
        }
    }
    /// 判断料号SN是否必须一次出库
    /// <summary>
    /// 判断料号SN是否必须一次出库
    /// </summary>
    /// <param name="cinvcode">料号</param>
    /// <returns>是true 否false</returns>
    public static bool CheckGroupPart(string cinvcode)
    {

        string Sql = string.Format(@"select count(*)
                                              from (select t.cinCount,
                                                           (select count(*)
                                                              from base_strongpick_part bs
                                                             where bs.parthead = SUBSTRING('{0}', 1, len(bs.parthead))
                                                               and bs.parttype = 0) yx
                                                      from (select count(*) cinCount
                                                              from sn_item_head_config hc
                                                             where hc.item_head = SUBSTRING('{0}', 1, len(hc.item_head))) t) t1
                                             where t1.cinCount > 0
                                               and t1.yx = 0", cinvcode);
        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(Sql)) > 0 ? true : false;
    }
    /// <summary>
    /// 根据出库单ID和储位编码获取超发线边仓编码和名称
    /// </summary>
    /// <param name="outBill_id">出库单ID</param>
    /// <param name="cpositioncode">储位编码</param>
    /// <returns>超发线边仓编码和名称</returns>
    public string GetLikeCargoSpaceByOutBill_id(string outBill_id, string cpositioncode)
    {
        string sql = "select [dbo].[Fun_GetLikeCargoSpace]('" + outBill_id.Trim() + "','" + cpositioncode.Trim() + "')";
        var obj = DBHelp.ExecuteScalar(sql);
        if (obj == null) return "";
        else 
        return DBHelp.ExecuteScalar(sql).ToString();
    }
}