using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Comm_Fun 的摘要说明
/// </summary>
public class Comm_Fun
{
	public Comm_Fun()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
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
            string Sql = string.Format(@"select  [dbo].[Fun_IsDecimal]('{0}',{1},{2},{3})", strInput, Is_Negative, Is_Zero, Is_Dec);
            result = DBHelp.ExecuteScalar(Sql).ToString();
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
    /// 生成单据号
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public string CreateNo(string tableName)
    {
        string sql = "select Fun_CreateNo('" + tableName + "') newNo";

        return DBHelp.ExecuteScalar(sql).ToString();
    }
    /// 检查批次号是否正确
    /// <summary>
    /// 检查批次号是否正确
    /// </summary>
    /// <param name="P_Part">批次号</param>
    /// <param name="P_Reserve1">预留1</param>
    /// <param name="errmsg">返回信息</param>
    /// <returns></returns>
    public static bool CheckFun_GetBatchNo(string P_Part, string P_Reserve1, out string errmsg)
    {
        bool boolTF = false;
        errmsg = "";
        try
        {
            string Sql = string.Format(@"select [dbo].[Fun_GetBatchNo]('CHECK','{0}','{1}')", P_Part, P_Reserve1);
            errmsg = DBHelp.ExecuteScalar(Sql).ToString();
            if (errmsg == "0")
            {
                boolTF = true;
            }
        }
        catch (Exception err)
        {
            errmsg = err.Message;
        }
        return boolTF;
    }
    //判断工单下料号状态是否满足异动条件
    //20130731134649
    public static string CanModDebit(string ErpCode, string CinvCode, string Type, string AsnId, string ReserveField1, string ReserveField2)
    {
        var sql = "select [dbo].[fun_check_mo_cinvcode_flag]('" + ErpCode + "','" + CinvCode + "','" +
                  Type + "','" + AsnId + "','" + ReserveField1 +
                  "','" + ReserveField2 + "')";
        return (string)DBHelp.ExecuteScalar(sql);
    }
    //判断通知单是否满足异动条件
    //20131105102731
    public static string CanAsnDebit(string AsnCode, string CinvCode, string Type, string ReserveField1, string ReserveField2)
    {
        var sql = "select [dbo].[fun_check_cinvcodebyAsn]('" + AsnCode + "','" + CinvCode + "','" +
                  Type + "','" + ReserveField1 +
                  "','" + ReserveField2 + "')";
        return (string)DBHelp.ExecuteScalar(sql);
    }

    /// 验证日期格式是否是YYMMDD
    /// <summary>
    /// 验证日期格式是否是YYMMDD
    /// </summary>
    /// <param name="strdate">验证字符串</param>
    /// <returns></returns>
    public static bool Check_Is_Date(string strdate)
    {
        string Sql = string.Format(@"select ISDATE('{0}')", strdate);
        return Convert.ToInt32(DBHelp.ExecuteScalar(Sql)) == 1 ? true : false;
    }
    /// 获取数据库时间
    /// <summary>
    /// 获取数据库时间
    /// </summary>
    /// <param name="sFormat"></param>
    /// <returns></returns>
    public static string GetDBDateTime()
    {
        string Sql = @"SELECT CONVERT(VARCHAR(8),GETDATE(),12)";
        return DBHelp.ExecuteScalar(Sql);
    }
    //获取当前时间
    public static DateTime? GetDBNowTime()
    {
        return DateTime.Parse(DateTime.Now.ToString());
    }
    /// 上传工单检查SWR的状态
    /// <summary>
    /// 上传工单检查SWR的状态
    /// </summary>
    /// <param name="erpcode"></param>
    /// <param name="pBz"></param>
    /// <returns></returns>
    public static string Fun_Check_SWR_Status(string erpcode, int pBz)
    {
        string Sql = string.Format(@"select dbo.[Fun_Check_SWR_Status]('{0}',{1})", erpcode, pBz);
        return DBHelp.ExecuteScalar(Sql).ToString();
    }
    //判断料号是否必须输入
    //必须输入返回true
    public static bool IsCinSNPossible(string CinvCode, string UserNo)
    {
        var sql = @"select isnull(pfri.info_result, 'NO'), isnull(pfri.info_msg, rt.msg)
                          from PROC_FUN_RETURN_INFO pfri,
                               (select [dbo].[FUN_SN_ITEM_CONTROL]('" + UserNo + @"', '" + CinvCode + @"', '', '0', '', '') msg ) rt
                         where pfri.fun_proc_name = 'FUN_SN_ITEM_CONTROL'
                           and rt.msg = pfri.info_code";
        DataTable dt = DBHelp.ExecuteToDataTable(sql);
        if (dt != null)
        {
            if (dt.Rows[0][0].ToString().Trim().Equals("NO"))
            {
                return true;
            }
            return false;
        }
        return false;
    }
    /// 检查料号区域卡控 true 不卡控 false 卡控
    /// <summary>
    /// 检查料号区域卡控 true 不卡控 false 卡控
    /// </summary>
    /// <param name="strcinv">料号</param>
    /// <param name="strcpos">储位</param>
    /// <param name="puser">操作用户</param>
    /// <param name="pbz">标识</param>
    /// <param name="pReserveField1">预留字段1</param>
    /// <param name="pReserveField2">预留字段2</param>
    /// <param name="result">返回错误信息</param>
    /// <returns></returns>
    public static bool Fun_IsControl_Area(string strcinv, string strcpos, string puser, int pbz,
                                          string pReserveField1, string pReserveField2, out string result)
    {
        result = "0";
        try
        {
            string Sql = string.Format(@"select [dbo].[Fun_IsControl_Area]('{0}', '{1}', '{2}',{3}, '{4}', '{5}')", strcinv, strcpos, puser, pbz, pReserveField1, pReserveField2);
            result = DBHelp.ExecuteScalar(Sql).ToString();
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
    }/// RMA 整新入库卡控
    /// <summary>
    /// RMA 整新入库卡控
    /// </summary>
    /// <param name="pInBill_ID">入库单ID</param>
    /// <param name="pCpositionCode">储位</param>
    /// <param name="pUserNo">操作人</param>
    /// <param name="pReserved1">预留字段1</param>
    /// <param name="pReserved2">预留字段2</param>
    /// <param name="result">返回错误信息</param>
    /// <returns></returns>
    public static bool Fun_RMA_To_WareHouse(string pInBill_ID, string pCpositionCode, string pUserNo,
                                              string pReserved1, string pReserved2, out string result)
    {
        result = "0";
        try
        {
            string Sql = string.Format(@"select [dbo].[Fun_RMA_To_WareHouse]('{0}','{1}','{2}','{3}','{4}')", pInBill_ID, pCpositionCode, pUserNo, pReserved1, pReserved2);
                result = DBHelp.ExecuteScalar(Sql).ToString();
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
    /// 检查出入库单数量是否正确
    /// </summary>
    /// <param name="P_Asn_IDS">通知单IDS</param>
    /// <param name="P_Bill_IDS">出入库单明细IDS</param>
    /// <param name="P_CinvCode">料号</param>
    /// <param name="P_Qty">输入数量</param>
    /// <param name="P_Type">类型 0 新增 1 编辑</param>
    /// <param name="P_BZ">0 入库</param>
    /// <param name="errmsg">返回错误信息</param>
    /// <returns></returns>
    public static bool Fun_Check_Asn_DQty(string P_Asn_IDS, string P_Bill_IDS, string P_CinvCode, decimal P_Qty, int P_Type, int P_BZ, out string errmsg)
    {
        bool boolTF = false;
        errmsg = "";
        string returnmsg = string.Empty;
        try
        {
            string Sql = string.Format(@"select [dbo].[Fun_Check_Asn_DQty]('{0}','{1}','{2}',{3},{4},{5})",
                P_Asn_IDS, P_Bill_IDS, P_CinvCode, P_Qty, P_Type, P_BZ);
            returnmsg = DBHelp.ExecuteScalar(Sql).ToString();
            if (returnmsg == "OK")
            {
                boolTF = true;
            }
            else
            {
                errmsg = returnmsg;
            }
        }
        catch (Exception err)
        {
            errmsg = err.Message;
        }
        return boolTF;
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
            string Sql = string.Format(@"select  [dbo].[Fun_CheckTax_SameBond]('{0}','',4,'','')", cinvcode);
            var obj = DBHelp.ExecuteScalar(Sql);
            if (obj == null) return false;
            else
            {
                returnvalue = DBHelp.ExecuteScalar(Sql).ToString();
                if (returnvalue == "OK")
                {
                    booTF = true;
                }
                else
                {
                    msg = returnvalue;
                }
            }
        }
        catch (Exception err)
        {
            msg = err.Message;
        }
        return booTF;
    }
    /// 判断是否允许超发
    /// <summary>
    /// 判断是否允许超发
    /// </summary>
    /// <param name="IDSCode">IDS或单据号</param>
    /// <param name="cinvcode">料号</param>
    /// <param name="userno">操作人</param>
    /// <param name="pBZ">标识 0 出库单明细IDS</param>
    /// <param name="pReservedField1">预留字段1</param>
    /// <param name="pReservedField2">预留字段2</param>
    /// <returns></returns>
    public static bool Fun_CheckCinvCode_ChaoFa(string IDSCode, string cinvcode, string userno, string pBZ,
                                       string pReservedField1, string pReservedField2)
    {
        string Sql = string.Format(@"select [dbo].[Fun_CheckCinvCode_ChaoFa]('{0}','{1}','{2}','{3}','{4}','{5}')",
                                     IDSCode, cinvcode, userno, pBZ, pReservedField1, pReservedField2);
        return DBHelp.ExcuteScalarSQL(Sql) == "1" ? true : false;
    }
    /// 通过SN获取SN的数量
    /// <summary>
    /// 通过SN获取SN的数量
    /// </summary>
    /// <param name="strSN">SN</param>
    /// <returns>SN的数量</returns>
    public static decimal Fun_GetNum_FromSN(string strSN)
    {
        string Sql = string.Format(@"select [dbo].[Fun_GetNum_FromSN]('{0}', 0) from dual", strSN);
        return Convert.ToDecimal(DBHelp.ExcuteScalarSQL(Sql));
    }
    /// <summary>
    /// 验证DateCode与配置的datecode格式
    /// </summary>
    /// <param name="str_datecode"></param>
    /// <returns></returns>
    public static string CheckDateCodeFormat(string str_datecode)
    {
        string returnmsg = string.Empty;
        DateTime dt_datecode;
        //获得配置的DateCode的时间格式  ----100100
        string datecodeformat =DreamTek.ASRS.Business.Base.CommFunction.GetConFig("100100");
        if (!string.IsNullOrEmpty(datecodeformat))
        {
            try
            {
                dt_datecode = DateTime.ParseExact(str_datecode, datecodeformat, null);
                if (dt_datecode.CompareTo(DateTime.Now) > 0)
                {
                    returnmsg = Resources.Lang.FrmINASN_DEdit_CanNotExceedCurrent; //DateCode 不能大于当前日期                   
                }
            }
            catch
            {
                returnmsg = Resources.Lang.Common_DateCodeFormatCheck + " [" + datecodeformat + "]"; //DateCode 必须是有效的时间且满足配置的格式                
            }
        }
        else
        {
           returnmsg = Resources.Lang.Common_DateCodeFormatEmpty; //请先配置DateCode的格式！         
        }
        return returnmsg;
    }

}