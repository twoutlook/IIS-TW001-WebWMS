using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// BarQuery 的摘要说明
/// </summary>
public class BarQuery
{
	public BarQuery()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    //获取待打印的SN
    public static DataTable GetPrintSn(string pid)
    {
        var sql =
           @"select 
                  sn_code SN, 
                  cinvcode CINVCODE,
                  cinvcode_name,
				  CONVERT(VARCHAR(50),CONVERT(DECIMAL(18,2),quantity)) quantity, 
                  CONVERT(VARCHAR(50),CONVERT(DECIMAL(18,2),quantity)) +'(' + isnull(CONVERT( VARCHAR(50),CONVERT(DECIMAL(18,2),bss.boxNum) ),'')+'/箱)' QTY,
                  bar_type, 
                  CPOSITIONCODE CPOSITIONCODE, 
                  po_number PO,                  
                  VENDOR_NAME Supplier,
                    CASE when  vendor IS NULL OR vendor='' THEN ''
				      WHEN ven.cvendorid IS NOT NULL THEN ven.calias
					  WHEN cus.cclientid IS NOT NULL THEN cus.calias END AS vendor,
                  datecode,
                  cspecifications,
                  part.calias as OCIVCODE,
               case when sn_code!=null or sn_code!='' then RIGHT(sn_code,1) ELSE 1  END AS sersnum
               ,bss.boxNum
             from base_bar_sn bss 
             LEFT JOIN 	BASE_PART part on bss.cinvcode=part.cpartnumber
              LEFT JOIN dbo.base_vendor ven ON bss.vendor=ven.cvendorid
			 LEFT JOIN dbo.base_client cus ON cus.cclientid=bss.vendor
             where bss.id in(" +
            pid + ")";
        return DBHelp.ExecuteToDataTable(sql);
    }   
    //获取SNcode
    public static string GetSerialNum(string cinvcode,string vendor,string po_number,string datecode,string id,out string errMSG)
    {
        var msg = string.Empty;
        var retVal=string.Empty;  
        var serialNum = 1;
        var sql = string.Format(@" SELECT TOP 1 serialNo FROM 
                                    (
                                       select sn_code,SUBSTRING(sn_code,72,4) as serialNo from BASE_BAR_SN where cinvcode='{0}' and ISNULL(vendor,'')='{1}' 
                                        and ISNULL(po_number,'')='{2}'  and datecode='{3}' and id!='{4}'  
                                     )T 
                                    ORDER BY serialNo DESC
                                    ", cinvcode, vendor, po_number, datecode,id);
        //var sql = "select dbo.[FUN_Get_SNCode]('" + pc + "','" + selectedValue + "','" + seriesnum + "') ";      
                                    
         object obj=DBHelp.ExecuteScalar(sql);
         if (obj == null)
         {
             retVal = string.Format("{0:D4}", serialNum);

         }
         else
         {
             var serialval = obj.ToString();
            // var serialval=!string.IsNullOrEmpty(sncode)?sncode.Substring(sncode.Length-4,4):string.Empty;
             if (!string.IsNullOrEmpty(serialval))
             {
                 try
                 {
                     serialNum = int.Parse(serialval)+1;
                     // num = decimal.Parse(qty);
                 }
                 catch (Exception ex)
                 {
                     msg = Resources.Lang.BarQuery_Msg1+ ":" + ex.Message;
                     //"流水号生成失败：" + ex.Message;
                 }
             }
             else
             {
                 serialNum = 1;
             }

           
             retVal = string.Format("{0:D4}", serialNum);

         }
         errMSG = msg;
         return retVal;
    }  
     /// <summary>
        /// 根据类型获取对应条码类型数量
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
    public DataTable GetBAR_TYPE(string type)
    {
        string strSQL = string.Format("SELECT ID,TYPENAME from BAR_TYPE A  where 1 = 1 and A.TYPE='{0}'", type);


        DataTable dtBAR_PALLET = DBHelp.ExecuteToDataTable(strSQL);
        return dtBAR_PALLET;
    }
    /// 检查栈板箱类型是否已经存在栈板/箱，存在true，不存在false
    /// <summary>
    /// 检查栈板箱类型是否已经存在栈板/箱，存在true，不存在false
    /// </summary>
    /// <param name="id">类型ID</param>
    /// <returns></returns>
    public static bool CheckTypeIDExists(string id)
    {
        string Sql = string.Format(@"select count(*) from bar_type tp
                                         where tp.id = '{0}'
                                           and exists
                                         (select 1
                                          from (select type_id   from bar_carton_m 
                                                union
                                                select type_id  from bar_pallet_m ) t
                                         where t.type_id = tp.id)", id);
        object ret = DBHelp.ExecuteScalar(Sql);
        if (ret == null) return false;
        else
        return Convert.ToInt32(DBHelp.ExecuteScalar(Sql)) > 0 ? true : false;
    }
    /// 判断是否混放是否可用 
    /// <summary>
    /// 判断是否混放是否可用
    /// </summary>
    /// <returns></returns>
    public static bool GetBarType_Enabled()
    {
        string Sql = @"select count(*)
                              from sys_parameter sp
                             where sp.flag_type = 'BAR_TYPE'
                               and sp.flag_id = 1";
        object ret = DBHelp.ExecuteScalar(Sql);
        if (ret == null) return false;
        else
            return Convert.ToInt32(DBHelp.ExecuteScalar(Sql)) > 0 ? true : false;
    }

    public static string IsExistsSN(string sncode)
    {
        var sql = string.Format(@"select dbo.FUN_SNISEXISTS('{0}')", sncode, string.Empty);

        return DBHelp.ExecuteScalar(sql);
        
    }
    /// <summary>
    /// 生成SN条码
    /// </summary>
    /// <param name="datecode"></param>
    /// <param name="civCode"></param>
    /// <param name="vendor"></param>
    /// <param name="po"></param>
    /// <param name="p"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static string CreateBaseBarSN(string datecode, string civCode, string po, string vendor, string id)
    {
        var sql = string.Format(@"select dbo.Fun_CreateBaseBarSN('{0}','{1}','{2}','{3}','{4}')",datecode,civCode,po,vendor,id);
        return DBHelp.ExecuteScalar(sql);
    }
}