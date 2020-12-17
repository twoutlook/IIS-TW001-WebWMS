using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Text;
using System.Data;
/// <summary>
/// OUTBILL_DRule 的摘要说明
/// </summary>
public class OUTBILL_XDRule
{
    public OUTBILL_XDRule()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public DataRow GetOUTBILLLByID(string id)
    {
        string sql = string.Format(@"
                                    SELECT A.*,B.CTICKETCODE bCCODE, B.SPECIAL_OUT from OUTBILL A
                                    left join OUTASN B on A.COUTASNID=B.ID
                                    where A.ID='{0}'
                                ", id);
        DataTable dtOUTBILL = DBHelp.ExecuteToDataTable(sql);
        DataRow dr = null;
        if (dtOUTBILL.Rows.Count > 0)
        {
            dr = dtOUTBILL.Rows[0];
        }
        return dr;
    }
    //获取出库单明细储位
    public static string GetOutBill_Position(string ids)
    {
        string Sql = string.Format(@"select bid.cpositioncode from outbill_d bid where bid.ids='{0}'", ids);
        DataTable tb = DBHelp.ExecuteToDataTable(Sql);
        if (tb != null && tb.Rows.Count > 0)
        {
            return tb.Rows[0][0].ToString();
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 验证是否有揀貨明细
    /// </summary>
    /// <param name="cticketcode">出库单单据号</param>
    /// <returns></returns>
    public static bool ValidateIsExistTemp_OutBill_D(string cticketcode)
    {
        string sql = "select count(*) from temp_outbill_d t where t.outbillcticketcode='" + cticketcode.Trim() + "'";

        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(sql)) > 0 ? true : false;
    }

    /// <summary>
    /// 检查WIP Issue工单状态 是否 可以生成指引
    /// </summary>
    /// <param name="ErpCode"></param>
    /// <returns></returns>
    public static string CheckWipIssueStatusByErpCode(string ErpCode)
    {
        string Erp = "";
        try
        {
            string Sql = @"select dbo.FUN_CHEK_STATE_MO('" + ErpCode + "')";
            DataTable tb = DBHelp.ExecuteToDataTable(Sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                Erp = tb.Rows[0][0].ToString();
            }
        }
        catch (Exception)
        {

            Erp = "";
        }

        return Erp;
    }



    public static string CheckISBack(string OutBill_ID)
    {

        string isTrue = "";
        try
        {
            string Sql = @"select dbo.FUN_CheckISBack('" + OutBill_ID + "')";
            DataTable tb = DBHelp.ExecuteToDataTable(Sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                isTrue = tb.Rows[0][0].ToString();
            }
        }
        catch (Exception)
        {

            isTrue = "";
        }

        return isTrue;
    }



    //判断是否已经扣帐
    public DataTable IsDebit(string cticketcode)
    {
        var sql = "select 1 from outbill o where o.CSTATUS not in ('0','1') and o.CTICKETCODE = '" + cticketcode +
                  "'";
        return DBHelp.ExecuteToDataTable(sql);
    }

    //判断是否为合并后通知单
    public static bool IsMerge(string outAsnId)
    {
        string sql = @"select count(*) from OUTASN o where o.IS_MERGE= '1' and MERGE_ID is not null and o.id = '" + outAsnId + "'";
        return Convert.ToInt32(DBHelp.ExecuteScalar(sql)) == 0 ? true : false;
    }
   
    //判断是否为特殊超领单
    public static bool IsSpecialBill(string billid)
    {
        var sql =
            @"select count(1) from outbill o, outasn ta where o.coutasnid = ta.id and ta.special_out = '1' and o.id = '" +
            billid + "'";
        return int.Parse(DBHelp.ExcuteScalarSQL(sql).ToString()) > 0 ? true : false;
    }
    //判断SN
    public static string GetSNType(string carton)
    {
        var sql = @"select SNType from view_get_palorcar_type t where t.SNNo = '" + carton + "'";
        return DBHelp.ExecuteScalar(sql) == null ? "0" : DBHelp.ExecuteScalar(sql).ToString();
    }
    //删除SN
    public void DeleteSn(string ids)
    {
        var sql = @"delete from outbill_d_sn  where outbill_d_ids = '" + ids.Trim() + "'";
        DBHelp.ExecuteNonQuery(sql);
    }
    /// <summary>
    /// 检查SN是否已经保存过
    /// </summary>
    /// <param name="outbill_d_ids"></param>
    /// <returns></returns>
    public static bool CheckIsExistOutBill_D_SN(string outbill_d_ids)
    {
        string Sql = string.Format(@"select count(1) from outbill_d_sn sn where sn.outbill_d_ids='{0}'", outbill_d_ids);
        return Convert.ToInt32(DBHelp.ExecuteScalar(Sql)) > 0 ? true : false;
    }

    /// <summary>
    /// 判断入库单状态 是true否false
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="strstatus">判断状态</param>
    /// <returns></returns>
    public bool CheckStatus(string id, string strstatus)
    {
        bool cstutas = false;
        try
        {
            string Sql = string.Format(@"select count(*) from outbill ob where ob.id='{0}' and ob.cstatus='{1}'", id, strstatus);
            DataTable tb = DBHelp.ExecuteToDataTable(Sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                if (Convert.ToInt32(tb.Rows[0][0].ToString()) > 0)
                {
                    cstutas = true;
                }
            }
        }
        catch (Exception)
        {
            cstutas = false;
        }
        return cstutas;
    }

    //判断是否为补单
    public bool isBD(string id)
    {
        var sql = @"select count(1) from outbill o where o.id='" + id.Trim() + "' and not exists(select 1 from outassit ot where ot.coutasnid = o.coutasnid)";
        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(sql)) > 0 ? true : false;
    }
    //判断是否存在SN
    public static bool ExistSN(string ids)
    {
        var sql = @"select count(1) from outbill_d_sn t where t.OUTBILL_D_IDS = '" + ids + "'";
        return Convert.ToInt32(DBHelp.ExcuteScalarSQL(sql)) > 0 ? true : false;
    }

    //获取已经保存的本次数量
    public static decimal GetSN_DEditByID(string id)
    {
        string Sql = string.Format(@"select isnull(sum(sn.quantity + isnull(sn.line_qty,0)),0) from outbill_d_sn sn where sn.id='{0}'", id);
        return Convert.ToDecimal(DBHelp.ExcuteScalarSQL(Sql));
    }
}