using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Text;

/// <summary>
/// OutAsnQuery 的摘要说明
/// </summary>
public class OutAsnQuery
{

    public OutAsnQuery()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 返回id
    /// </summary>
    /// <param name="cerpcode"></param>
    /// <returns></returns>
    public static DataTable GetOutAsnID(string cerpcode)
    {
        string strSQL = string.Format(@"
                                        select oa.id from Outasn oa 
                                        where oa.cerpcode='{0}'
                                        union
                                        select oa.id from Outasn oa 
                                          where 1 = 1
                                            and oa.is_merge='1'
                                            and oa.merge_id in (select oa1.merge_id 
                                                             from Outasn oa1
                                                            where 1 = 1
                                                              and oa1.merge_id is not null
                                                              and oa1.is_merge='0'
                                                              and oa1.cerpcode='{0}')
                                    ", cerpcode.Trim());
        return DBHelp.ExecuteToDataTable(strSQL);
    }

    public static bool validateIsExistErrorMsg(string id)
    {
        string sql = "select count(*) from log_syserror l with(nolock) where l.sovrce_no='" + id + "'";

        if (int.Parse(DBHelp.ExecuteScalar(sql))> 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 验证出库通知单的明细是否都生成了出库单
    /// </summary>
    /// <param name="outAsnId"></param>
    /// <returns></returns>
    public static bool ValidateOutAsn_DIsAllCreateOutBill(string outAsnId)
    {
        string strSQL = string.Format(@"
                                        SELECT  COUNT(1)
                                        FROM    ( SELECT    oad.cinvcode ,
                                                            ISNULL(SUM(oad.iquantity), 0) OutAsn_Qty ,
                                                            ( SELECT    ISNULL(SUM(obd.iquantity), 0) iquantity
                                                              FROM      dbo.OUTBILL_D obd
                                                                        INNER JOIN dbo.OUTBILL ob ON obd.id = ob.id
                                                              WHERE     obd.cinvbarcode = oad.cinvcode
                                                                        AND ob.coutasnid = '{0}'
                                                            ) Outbill_Qty
                                                  FROM      dbo.OUTASN_D oad
                                                  WHERE     oad.id = '{0}'
                                                  GROUP BY  oad.cinvcode
                                                ) newTable
                                        WHERE   ( newTable.OutAsn_Qty - newTable.Outbill_Qty ) > 0
                                    ", outAsnId.Trim());
        return DBHelp.ExecuteToDataTable(strSQL).Rows.Count == 0 ? false : true;
    }

    //判断工单下料号状态是否满足异动条件 
    //20130731134649
    public static string CanModDebit(string ErpCode, string CinvCode, string Type, string AsnId, string ReserveField1, string ReserveField2)
    {
        string sql = "select dbo.Fun_check_mo_cinvcode_flag('" + ErpCode + "','" + CinvCode + "','" +
                  Type + "','" + AsnId + "','" + ReserveField1 +
                  "','" + ReserveField2 + "') msg ";
        return DBHelp.ExecuteToDataTable(sql).Rows[0]["msg"].ToString();
    }

    //判断通知单是否满足异动条件
    //20131105102731
    public static string CanAsnDebit(string AsnCode, string CinvCode, string Type, string ReserveField1, string ReserveField2)
    {
        var sql = "select dbo.Fun_check_cinvcodebyAsn('" + AsnCode + "','" + CinvCode + "','" +
                  Type + "','" + ReserveField1 +
                  "','" + ReserveField2 + "') msg";
        return DBHelp.ExecuteToDataTable(sql).Rows[0]["msg"].ToString();
    }


    /// <summary>
    /// 检查通知单是否有明细信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool CheckOutAsn_DCount(string id)
    {
        bool asn_d = false;
        try
        {
            string Sql = @"select count(*) from outasn_d ad where ad.id='" + id + "'";
            DataTable tb = DBHelp.ExecuteToDataTable(Sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                if (Convert.ToInt32(tb.Rows[0][0].ToString()) > 0)
                {
                    asn_d = true;
                }
            }
        }
        catch (Exception err)
        {
            asn_d = false;
        }
        return asn_d;

    }

    /// <summary>
    /// 生成单据号
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static string CreateNo(string tableName)
    {
        string sql = "select dbo.Fun_CreateNo('" + tableName + "') newNo ";

        return DBHelp.ExcuteScalarSQL(sql).ToString();
    }


    /// 获取配置表中的值
    /// <summary>
    /// 获取配置表中的值
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string GetConFig(string code)
    {
        string Sql = string.Format(@"select fi.config_value from sys_config fi where fi.code='{0}' ", code);
        return DBHelp.ExcuteScalarSQL(Sql).ToString();
    }


    //判断是否存在补单中的明细
    public static bool CheckAsn_DStatus(string asn_id)
    {
        string Sql = string.Format(@"select count(1) from outasn_d oad where oad.id='{0}' and oad.cstatus in ('0','4')", asn_id);
        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(Sql)) > 0 ? true : false;
    }

    //判断是否为合并后通知单
    public static bool IsMerge(string outAsnId)
    {
        string sql = @"select count(*) from OUTASN o where o.IS_MERGE= '1' and MERGE_ID is not null and o.id = '" + outAsnId + "'";
        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(sql)) == 0 ? true : false;
    }

    /// <summary>
    /// 检查工单状态是否可以可用
    /// </summary>
    /// <param name="erpCode"></param>
    /// <returns></returns>
    public static string Check_WIP_STATUS(string erpCode)
    {
        string sql = "select dbo.Fun_Check_WIP_STATUS_IsCF('" + erpCode.Trim() + @"')";

        return DBHelp.ExcuteScalarSQL(sql).ToString();
    }

    /// <summary>
    /// 验证ReturnToVendor类型的出库通知单中填的ErpCode的是否可用
    /// </summary>
    /// <param name="erpCode">ErpCode</param>
    /// <returns>验证消息</returns>
    public static string CheckReturnToVendorErpCodeIsUsable(string erpCode)
    {
        string sql = @"select dbo.Fun_CheckReturnToVendorHead('" + erpCode.Trim() + "')";
        return DBHelp.ExcuteScalarSQL(sql).ToString();
    }

    /// <summary>
    /// 判断料号是否是半成品或成品 是true 否false
    /// </summary>
    /// <param name="cinvcode"></param>
    /// <returns></returns>
    public static bool Check_FG_CinvCode(string cinvcode)
    {
        string Sql =
            string.Format(
                @"select count(1) from base_part bp where bp.cpartnumber='{0}' and bp.ctype in ('1','2') ", cinvcode);
        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(Sql)) > 0 ? true : false;
    }

    /// 检查数量是否符合条件
    /// <summary>
    /// 检查数量是否符合条件
    /// </summary>
    /// <param name="strInput">输入数量字符串</param>
    /// <param name="Is_Negative">是否允許為負數  0 不允許 1 允許</param>
    /// <param name="Is_Zero">是否允許為0, 0 不允許 1 允許</param>
    /// <param name="Is_Dec">是否允許含有小數點， 0 不允許 1允許</param>
    /// <param name="result">返回错误信息</param>
    /// <returns>符合为true，不符合false</returns>
    public static bool Fun_IsDecimal(string strInput, int Is_Negative, int Is_Zero, int Is_Dec, out string result)
    {
        result = "0";
        try
        {
            string Sql = string.Format(@"select dbo.Fun_IsDecimal('{0}',{1},{2},{3}) ", strInput, Is_Negative, Is_Zero, Is_Dec);
            result = DBHelp.ExcuteScalarSQL(Sql).ToString();
            if (result == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception err)
        {
            result = err.Message;
            return false;
        }
    }

    /// <summary>
    /// 验证 Wip Issue 工单量是否已发齐！
    /// </summary>
    /// <param name="erpCode"></param>
    /// <returns></returns>
    public static string CheckWipIssueHead(string erpCode, decimal IsSpecialWIP_Issue)
    {
        string sql = "select dbo.Fun_CheckWipIssueHead('" + erpCode.Trim() + @"'," + IsSpecialWIP_Issue + ")";

        return DBHelp.ExcuteScalarSQL(sql).ToString();
    }

    /// <summary>
    /// WIP Completion Return 工单完工退 17-204
    /// </summary>
    /// <param name="erpCode"></param>
    /// <returns></returns>
    public static string CheckWip_C_R_Head(string erpCode)
    {
        string sql = "select dbo.Fun_CheckWip_C_R_Head('" + erpCode.Trim() + "')";

        return DBHelp.ExcuteScalarSQL(sql).ToString();
    }

    /// <summary>
    /// 检查工单是否可以超领
    /// </summary>
    /// <param name="erpCode"></param>
    /// <returns></returns>
    public static string CheckWIP_IsOkCF(string erpCode)
    {
        string sql = "select dbo.Fun_CheckWIP_IsOkCF('" + erpCode.Trim() + @"') ";

        return DBHelp.ExcuteScalarSQL(sql).ToString();
    }

    /// <summary>
    /// 超领卡控
    /// </summary>
    /// <param name="Erpcode"></param>
    /// <param name="strcinv"></param>
    /// <param name="LineQty"></param>
    /// <param name="puserno"></param>
    /// <param name="bz"></param>
    /// <returns></returns>
    public static string CheckOverCollar(string Erpcode, string strcinv, decimal LineQty, string puserno, string bz)
    {
        string msg = "";
        try
        {
            string sql = string.Format(@"select dbo.Fun_CheckOverCollar('{0}','{1}',{2},'{3}','{4}')", Erpcode,
                                 strcinv, LineQty, puserno, bz);
            msg = DBHelp.ExcuteScalarSQL(sql).ToString();
            return msg;
        }
        catch (Exception err)
        {
            return err.Message;
        }
    }
    /// <summary>
    /// 检查工单是否可以做负退料 WIP Negative Return
    /// </summary>
    /// <param name="erpCode"></param>
    /// <returns></returns>
    public static string CheckWIP_N_R_Head(string erpCode, decimal IsSpecial)
    {
        string sql = "select dbo.Fun_CheckWIP_N_R_Head('" + erpCode.Trim() + "'," + IsSpecial + ")";

        return DBHelp.ExcuteScalarSQL(sql).ToString();
    }

    public static bool IsSpecialOut(string ID)
    {
        string sql = "select count(*) from outasn oa where oa.id = '" + ID + "' and SPECIAL_OUT = '1'";
        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(sql)) > 0 ? true : false;
    }

    /// <summary>
    /// 检查出库单是否存在揀貨指引单
    /// </summary>
    /// <param name="outAsnId"></param>
    /// <returns></returns>
    public static bool CheckIsExistOutAssitByOutAsnId(string outAsnId)
    {
        string sql = "select count(*) from Outassit oa where oa.coutasnid='" + outAsnId + "'";
        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(sql)) > 0 ? true : false;
    }

    /// <summary>
    /// 验证出库通知单是否生成出库单
    /// </summary>
    /// <param name="InAsnId"></param>
    /// <returns></returns>
    public static bool ValidateIsCreateOutBill(string outAsnId)
    {
        string sql = "select count(*) from OutBill ob where ob.coutasnid='" + outAsnId.Trim() + "'";
        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(sql)) == 0 ? false : true;
    }

    /// <summary>
    /// 验证入库单明细是否生成过入库单
    /// Fun_GetInBill_D_Qty(A.CINVCODE,A.Cerpcodeline,A.Id)
    /// </summary>
    /// <returns>返回 true 没有生成，false 生成</returns>
    public static bool ValidateIsCreateOutBill(string CINVCODE, string Cerpcodeline, string OutAsn_Id)
    {
        string sql = "select dbo.Fun_GetInOrOutBill_D_Qty('" + CINVCODE.Trim() + "','" + Cerpcodeline.Trim() + "','" + OutAsn_Id.Trim() + "',0) ";
        if (Cerpcodeline == "&nbsp;")
        {
            sql = "select dbo.Fun_GetInOrOutBill_D_Qty('" + CINVCODE.Trim() + "',null,'" + OutAsn_Id.Trim() + "',0)";
        }
        int count = Convert.ToInt32(DBHelp.ExcuteScalarSQL(sql));
        return count == 0 ? true : false;
    }

    /// <summary>
    /// 检查是否已生成指引
    /// </summary>
    /// <param name="InAsn_Id"></param>
    /// <returns></returns>
    public static bool CheckIsExistOutAssitByOutAsn_Id(string OutAsn_Id)
    {
        string sql = "select count(*) from OutAssit oa where oa.coutasnid = '" + OutAsn_Id.Trim() + "'";
        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(sql)) > 0 ? true : false;
    }

    public static void UpdateStatus(string ID)
    {
        var UpdSql =
             @"update outasn set cstatus = '3'
                    where not exists (select 1
                                        from (select a.cinvcode, a.inum, b.iquantity
                                                from (select sum(od.iquantity) inum, od.cinvcode
                                                        from outasn_d od
                                                        where od.id = '" + ID + @"' group by od.cinvcode) a
                                                    left join
                                                    (select sum(obd.iquantity) iquantity, obd.cinvcode
                                                        from outbill_d obd, outbill ob
                                                        where obd.id = ob.id
                                                        and ob.cstatus >= 2
                                                        and ob.coutasnid = '" + ID + @"' group by obd.cinvcode) b
                                                on a.cinvcode = b.cinvcode) c
                                        where c.inum <> c.iquantity
                                            or c.iquantity is null)
                        and exists (select 1 from outasn_d t where t.id = id) 
                        and id = '" + ID + "'";
        DBHelp.ExcuteScalarSQL(UpdSql);
    }

    /// <summary>
    /// 根据ERP单号获取入库通知单ID
    /// </summary>
    /// <param name="erpNo"></param>
    /// <returns></returns>
    public static string GetInAsnIdByErpNo(string erpNo)
    {
        string sql = "select ia.id from InAsn ia where ia.CERPCODE='" + erpNo + "' and ia.itype='101'";

        object obj = DBHelp.ExcuteScalarSQL(sql);

        return obj != null ? obj.ToString() : "";
    }
    /// <summary>
    /// 判断传入的料号是否可以超领
    /// </summary>
    /// <param name="PartNumber">料号</param>
    /// <param name="Type">类型 ：CL 超领,CJ 拆解</param>
    /// <returns>是否可以超领或拆解</returns>
    public static bool CheckWIP_CL_PARTINFO(string PartNumber, string Type)
    {
        string sql = "select dbo.Fun_CheckWIP_CL_PARTINFO('" + PartNumber.Trim() + "','" + Type + "') ";

        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(sql)) == 0 ? true : false;
    }

    /// 获取自增序列号码
    /// <summary>
    /// 获取自增序列号码
    /// </summary>
    /// <param name="pID">单据ID</param>
    /// <param name="pType">类型 0 预入库通知单明细</param>
    /// <param name="pReserved1"></param>
    /// <param name="pReserved2"></param>
    /// <returns></returns>
    public static string Fun_GetNo(string pID, string pType, string pReserved1, string pReserved2)
    {
        string Sql = string.Format(@"select dbo.Fun_GetNo('{0}','{1}','{2}','{3}')", pID, pType, pReserved1,
                                   pReserved2);
        return DBHelp.ExcuteScalarSQL(Sql).ToString();
    }

    /// <summary>
    /// 根据料号获取品名
    /// </summary>
    /// <param name="CPARTNUMBER"></param>
    /// <returns></returns>
    public static string GetCPARTNAMEByCPARTNUMBER(string CPARTNUMBER)
    {
        string sql = "select CPARTNAME from base_part part where part.CPARTNUMBER='" + CPARTNUMBER + "'";

        try
        {
            return DBHelp.ExcuteScalarSQL(sql).ToString();
        }
        catch (Exception)
        {
            return "";
        }
    }

    /// <summary>
    /// 检查料号是否为空
    /// </summary>
    /// <param name="cinvcode">料号</param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static bool Fun_CheckTax_SameBond(string cinvcode, out string msg)
    {
        msg = "";
        bool booTF = false;
        string returnvalue = string.Empty;
        try
        {
            string Sql = string.Format(@"select dbo.Fun_CheckTax_SameBond('{0}','',4,'','') ", cinvcode);
            returnvalue = DBHelp.ExcuteScalarSQL(Sql).ToString();
            if (returnvalue == "OK")
            {
                booTF = true;
            }
            else
            {
                msg = returnvalue;
            }
        }
        catch (Exception err)
        {
            msg = err.Message;
        }
        return booTF;
    }

    /// 检查工单发料料号是否重复
    /// <summary>
    /// 检查工单发料料号是否重复
    /// </summary>
    /// <param name="outasnid">通知单ID</param>
    /// <param name="cinvcode">料号</param>
    /// <returns></returns>
    public static bool CheckWipIssue_CinvCode(string outasnid, string cinvcode)
    {
        string Sql = string.Format(@"select count(1) from outasn_d od where od.id='{0}' and od.cinvcode='{1}'", outasnid, cinvcode);
        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(Sql)) > 0 ? true : false;
    }

    /// <summary>
    /// （出、入）通知单，明细中项次相同料号、相同子ERPcode（或空）只能添加一条
    /// </summary>
    /// <param name="InAsnId"></param>
    /// <param name="CINVCODE"></param>
    /// <param name="CERPCODELINE"></param>
    /// <returns></returns>
    public static bool ValidateAsn_D_IsExist(string OutAsnId, string CINVCODE, string CERPCODELINE, string OutAsnIds)
    {
        bool returnValue = false;
        string sql = "select top 1 ids from outAsn_d oad where oad.id='" + OutAsnId.Trim() + "' and oad.cinvcode='" + CINVCODE.Trim() + "'";

        if (CERPCODELINE.Length > 0)
        {
            sql += " and oad.cerpcodeline='" + CERPCODELINE.Trim() + "'";
        }
        if (DBHelp.ExcuteScalarSQL(sql) == null)
        {
            //没有查到 可以入
            returnValue = true;
        }
        else if (DBHelp.ExcuteScalarSQL(sql) != null && DBHelp.ExcuteScalarSQL(sql).ToString() == OutAsnIds)
        {
            //查到了但，是原记录的修改，所以也可以入
            returnValue = true;
        }

        return returnValue;
    }

    /// <summary>
    /// 检查当前料号是否在工单内
    /// </summary>
    /// <param name="AsnId">通知单ID</param>
    /// <param name="PartNumber">料号</param>
    /// <returns>返回检查消息 OK 或 其他</returns>
    public static string CheckWIP_Part(string AsnId, string PartNumber)
    {
        string sql = "select dbo.Fun_CheckWIP_Part('" + AsnId.Trim() + "','" + PartNumber.Trim() + "')";

        return DBHelp.ExcuteScalarSQL(sql).ToString().ToUpper();
    }

    /// <summary>
    /// 验证 Wip Issue 的数量是否有效
    /// </summary>
    /// <param name="outAsn_id">出库通知单ID</param>
    /// <param name="CINVCODE">料号</param>
    /// <param name="OutQty">本次要出库的数量</param>
    /// <param name="workType">工作类型(0：新增,1:修改)</param>
    /// <param name="OriginalQty">修改时带出来的原始数量</param>
    /// <returns></returns>
    public static string CheckWipIssueQty(string OutAsnId, string CINVCODE, string OutQty, string workType, string OriginalQty)
    {
        string sql = "select dbo.Fun_CheckWipIssueQty('" + OutAsnId.Trim() + "','" + CINVCODE.Trim() + "','" + OutQty.Trim() + "','" + workType.Trim() + "','" + OriginalQty.Trim() + "') ";
        return DBHelp.ExcuteScalarSQL(sql).ToString();
    }

    /// <summary>
    /// 验证 WIP Negative Return 特殊元件退料 的数量是否有效
    /// </summary>
    /// <param name="outAsn_id">出库通知单ID</param>
    /// <param name="CINVCODE">料号</param>
    /// <param name="OutQty">本次要出库的数量</param>
    /// <param name="workType">工作类型(0：新增,1:修改)</param>
    /// <param name="OriginalQty">修改时带出来的原始数量</param>
    /// <returns></returns>
    public static string CheckWip_N_R_SpecialQty(string OutAsnId, string CINVCODE, string OutQty, string workType, string OriginalQty)
    {
        string sql = "select dbo.Fun_CheckWip_N_R_SpecialQty('" + OutAsnId.Trim() + "','" + CINVCODE.Trim() + "','" + OutQty.Trim() + "','" + workType.Trim() + "','" + OriginalQty.Trim() + "')";
        return DBHelp.ExcuteScalarSQL(sql).ToString();
    }

    /// <summary>
    /// 验证 WIP Completion Return 的数量是否有效 (工单完工退料数量验证)
    /// </summary>
    /// <param name="outAsn_id">出库通知单ID</param>
    /// <param name="CINVCODE">料号</param>
    /// <param name="OutQty">本次要出库的数量</param>
    /// <param name="workType">工作类型(0：新增,1:修改)</param>
    /// <param name="OriginalQty">修改时带出来的原始数量</param>
    /// <returns></returns>
    public static string CheckCheckWip_C_R_Qty(string OutAsnId, string CINVCODE, string OutQty, string workType, string OriginalQty)
    {
        string sql = "select dbo.Fun_CheckWip_C_R_Qty('" + OutAsnId.Trim() + "','" + CINVCODE.Trim() + "','" + OutQty.Trim() + "','" + workType.Trim() + "','" + OriginalQty.Trim() + "')";
        return DBHelp.ExcuteScalarSQL(sql).ToString();
    }

    /// <summary>
    /// 验证出库数量是否有效 Return to Vendor : 36 数量验证
    /// </summary>
    /// <param name="outAsn_id">出库通知单ID</param>
    /// <param name="CINVCODE">料号</param>
    /// <param name="OutQty">本次要出库的数量</param>
    /// <param name="workType">工作类型(0：新增,1:修改)</param>
    /// <param name="OriginalQty">修改时带出来的原始数量</param>
    /// <returns></returns>
    public static string CheckReturnToVendorQty(string OutAsnId, string CINVCODE, string OutQty, string workType, string OriginalQty, string CERPCODELINE)
    {
        string sql = "select dbo.Fun_CheckReturnToVendorQty('" + OutAsnId.Trim() + "','" + CINVCODE.Trim() + "','" + OutQty.Trim() + "','" + workType.Trim() + "','" + OriginalQty.Trim() + "','" + CERPCODELINE.Trim() + "')";
        return DBHelp.ExcuteScalarSQL(sql).ToString();
    }

    /// <summary>
    /// 判断工单中的料是否可以超领
    /// </summary>
    /// <param name="erpCode"></param>
    /// <returns></returns>
    public static string CheckWIP_CF_ByPartNumber(string outAsnId, string partNumber)
    {
        string sql = "select dbo.Fun_CheckWIP_CF_ByPartNumber('" + outAsnId.Trim() + @"','" + partNumber.Trim() + @"')";
        return DBHelp.ExcuteScalarSQL(sql).ToString();
    }

    /// <summary>
    /// 获取料号获取品名
    /// </summary>
    /// <param name="CINVCODE"></param>
    /// <returns></returns>
    public static string GetCINVNAMEByCINVCODE(string CINVCODE)
    {
        string sql = "select part.cpartname from base_part part where part.cpartnumber='" + CINVCODE + "'";
        object obj = DBHelp.ExcuteScalarSQL(sql);
        return obj.ToString();
    }
}