using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DreamTek.ASRS.Business;
using System.Data;

/// <summary>
/// OutBill 的摘要说明
/// </summary>
public class OutBillQuery
{
    public OutBillQuery()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 批量扣账
    /// </summary>
    /// <param name="InBill_Ids"></param>
    /// <returns></returns>
    public static string BatchOutBillTOStock_Currnt(Dictionary<string, string> OutBill_Ids, string UserNo)
    {
        string guid = Guid.NewGuid().ToString();
        StringBuilder sb = new StringBuilder();
        foreach (string item in OutBill_Ids.Keys)
        {
            string msg = string.Empty;
            if (Check_Proc_OutBillTOStock_Currnt(item, UserNo, guid, ref msg))
            {
                #region 调用存储过程
                List<string> SparaList = new List<string>();
                SparaList.Add("@P_Guid:" + guid);
                SparaList.Add("@P_OutBill_Id:" + UserNo);
                SparaList.Add("@P_UserNo:" + item.Trim());
                SparaList.Add("@P_ReturnValue:" + "");
                SparaList.Add("@INFOTEXT:" + "");
                string[] Result = DBHelp.ExecuteProc("Proc_DeliverAndOUTStockCurrent", SparaList);
                if (Result.Length == 1)
                {
                    msg = Result.ToString();
                    sb.Append(Result.ToString() + "扣账失败!\r\n");//" 操作失败！";
                }
                else if (Result[0] == "1")
                {
                    sb.Append(new ErrorMsg().GetErrorMsg(guid) + "扣账失败!\r\n");//" 操作失败！";
                }
                #endregion
                #region  注销代码
                //Proc_DeliverAndOUTStockCurrent proc = new Proc_DeliverAndOUTStockCurrent();
                //proc.P_Guid = guid;
                //proc.P_OutBill_Id = item.Trim();
                //proc.P_UserNo = UserNo.Trim();
                //proc.Execute();
                //if (proc.P_ReturnValue == 1)
                //{
                //    sb.Append(new ErrorMsgQuery().GetErrorMsg(guid) + "扣账失败!\r\n");//" 操作失败！";
                //}
                #endregion
            }
            else
            {
                sb.Append(msg + "\r\n扣账失败!\r\n");
            }
        }

        if (sb.ToString().Length == 0)
        {
            sb.Append("扣账操作成功!");
        }

        return sb.ToString();
    }
    private static bool Check_Proc_OutBillTOStock_Currnt(string OutBill_Id, string UserNo, string P_Guid, ref string msg)
    {
        bool returnValue = true;
        try
        {
            #region 调用存储过程
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_OutBillId:" + OutBill_Id);
            SparaList.Add("@P_UserNo:" + UserNo);
            SparaList.Add("@P_Guid:" + P_Guid);
            SparaList.Add("@P_ReturnValue:" + "");
            SparaList.Add("@INFOTEXT:" + "");
            string[] Result = DBHelp.ExecuteProc("Proc_CheckOutBillTOCURRENT", SparaList);
            if (Result.Length == 1)
            {
                msg = Result.ToString();
                returnValue = false;
            }
            else if (Result[0] == "1")
            {
                msg = new ErrorMsg().GetErrorMsg(P_Guid);
                returnValue = false;
            }
            #endregion
           
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            returnValue = false;
        }

        return returnValue;
    }
    /// <summary>
    /// 批量扣账测试
    /// </summary>
    /// <param name="InBill_Ids"></param>
    /// <returns></returns>
    /// 
    public static string BatchOutBillTOStock_Currnt_Test(Dictionary<string, string> OutBill_Ids, string UserNo)
    {
        string guid = Guid.NewGuid().ToString();
        StringBuilder sb = new StringBuilder();
        foreach (string item in OutBill_Ids.Keys)
        {
            string msg = string.Empty;
            if (Check_Proc_OutBillTOStock_Currnt_Test(item, UserNo, guid, ref msg))
            {
                #region 调用存储过程
                List<string> SparaList = new List<string>();
                SparaList.Add("@P_OutBillId:" + item.Trim());
                SparaList.Add("@P_UserNo:" + UserNo);
                SparaList.Add("@P_Guid:" + guid);
                SparaList.Add("@P_ReturnValue:" + "");
                SparaList.Add("@INFOTEXT:" + "");
                string[] Result = DBHelp.ExecuteProc("Proc_DeliverAndOUTStockCurrent", SparaList);
                if (Result.Length == 1)
                {

                    sb.Append(Result.ToString() + "扣账失败!\r\n");//" 操作失败！";
                }
                else if (Result[0] == "1")
                {
                    sb.Append(new ErrorMsg().GetErrorMsg(guid) + "扣账失败!\r\n");//" 操作失败！";
                }
                #endregion
                #region 注销
                //Proc_DeliverAndOUTStockCurrent proc = new Proc_DeliverAndOUTStockCurrent();
                //proc.P_Guid = guid;
                //proc.P_OutBill_Id = item.Trim();
                //proc.P_UserNo = UserNo.Trim();
                //proc.Execute();
                //if (proc.P_ReturnValue == 1)
                //{
                //    sb.Append(new ErrorMsgQuery().GetErrorMsg(guid) + "扣账失败!\r\n");//" 操作失败！";
                //}
                #endregion
            }
            else
            {
                sb.Append(msg + "\r\n扣账失败!\r\n");
            }
        }

        if (sb.ToString().Length == 0)
        {
            sb.Append("扣账操作成功!");
        }

        return sb.ToString();
    }
    private static bool Check_Proc_OutBillTOStock_Currnt_Test(string OutBill_Id, string UserNo, string P_Guid, ref string msg)
    {
        bool returnValue = true;
        try
        {
            #region 调用存储过程
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_OutBillId:" + OutBill_Id);
            SparaList.Add("@P_UserNo:" + UserNo);
            SparaList.Add("@P_Guid:" + P_Guid);
            SparaList.Add("@P_ReturnValue:" + "");
            SparaList.Add("@INFOTEXT:" + "");
            string[] Result = DBHelp.ExecuteProc("Proc_CheckOBTOCURRENT_Test", SparaList);
            if (Result.Length == 1)
            {

                msg = Result.ToString();
                returnValue = false;
            }
            else if (Result[0] == "1")
            {
                msg = new ErrorMsg().GetErrorMsg(P_Guid);
                returnValue = false;
            }
            #endregion
            #region 注销
            //proc = new Proc_CheckOBTOCURRENT_Test();
            //proc.P_OutBillId = OutBill_Id;
            //proc.P_UserNo = UserNo;
            //proc.P_Guid = P_Guid;
            //proc.Execute();
            //if (proc.P_ReturnValue > 0)
            //{
            //    msg = new ErrorMsg().GetErrorMsg(P_Guid);
            //    returnValue = false;
            //}
            #endregion
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            returnValue = false;
        }

        return returnValue;
    }
    // 判断相邻储位是否有阻挡[出库] 
    public string GetStopCurrent_Out(string id)
    {
        string strCposition = "";
        try
        {

            string Sql = string.Format(@" select  distinct CPOSITIONCODE  from base_cargospace  where  Pallet_code = 1 and CPOSITIONCODE in  
                                             (select CPOSITIONCODE   from  BASE_CARGOSPACE
                                                      where CX+2 = (select CX  from  BASE_CARGOSPACE
                                                      where CPOSITIONCODE in ( select CPOSITIONCODE  from OUTBILL_D bid where
                                                 bid.ids='{0}' ))
 
                                                        and CY =(select CY  from  BASE_CARGOSPACE
                                                      where CPOSITIONCODE in ( select CPOSITIONCODE  from OUTBILL_D bid where
                                                 bid.ids='{0}' ))
                                                        and CZ = (select CZ  from BASE_CARGOSPACE
                                                      where CPOSITIONCODE in ( select CPOSITIONCODE  from OUTBILL_D bid where
                                                 bid.ids='{0}' ))) ", id);

            DataTable tb = DBHelp.ExecuteToDataTable(Sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                strCposition = tb.Rows[0][0].ToString();
            }
        }
        catch (Exception)
        {
            strCposition = "";
        }
        return strCposition;
    }
    public string strGetCY(string ids)
    {
        string strCY = "";
        try
        {
            string Sql = string.Format(@" select B.CX   from  BASE_CARGOSPACE B where CPOSITIONCODE ='{0}' ", ids);
            DataTable dtCy = DBHelp.ExecuteToDataTable(Sql);
            if (dtCy != null && dtCy.Rows.Count > 0)
            {
                strCY = dtCy.Rows[0][0].ToString();
            }
        }
        catch (Exception)
        {
            strCY = "";
        }
        return strCY;

    }
    // 活动所有空的储位 
    public DataTable GetAllCurrentByNull(string id)
    {

        string Sql = string.Format(@" select CPOSITIONCODE , CX,CY,CZ  from BASE_CARGOSPACE  
                                          where CX  in ('01','02')  and  PALLET_CODE = '0' and  CPOSITIONCODE not in (select distinct CPOSITIONCODE from VIEW_STOCK_CURRENT) ", id);

        DataTable dtINBILL_D = DBHelp.ExecuteToDataTable(Sql);
        return dtINBILL_D;
    }

    // 活动所有空的储位 
    /// <summary>
    /// 修改逻辑：阻挡储位是高储位时，获取目的储位时也必须是高储位，低储位时就获取低储位
    /// </summary>
    /// <param name="id">CX</param>
    /// <param name="cz">阻挡的CZ</param>
    /// <returns></returns>
    public DataTable GetAllCurrentByNull(string id, string cz)
    {
        //pan gao 20160526 以后用（01,02），在1,2两个位置中去找

        string Sql = string.Format(@" select CPOSITIONCODE , CX,CY,CZ  from BASE_CARGOSPACE  
                                          where CX  in ('01','02')  and  PALLET_CODE = '0' and  CPOSITIONCODE not in (select distinct CPOSITIONCODE from VIEW_STOCK_CURRENT) ", id);

        if (!string.IsNullOrEmpty(cz))
        {
            //根据储位最后两位层数判断是高低储位  01 02 03 09 高储位  04 05 06 07 08 低储位
            if (cz == "04" || cz == "05" || cz == "06" || cz == "07" || cz == "08")
            {
                Sql = string.Format(@" select CPOSITIONCODE , CX,CY,CZ  from BASE_CARGOSPACE  
                                          where CX  in ('01','02')  and  PALLET_CODE = '0' 
                                                and CZ in ('04','05','06','07','08') 
                                                and  CPOSITIONCODE not in (select distinct CPOSITIONCODE from VIEW_STOCK_CURRENT) ", id);
            }
            else
            {
                Sql = string.Format(@" select CPOSITIONCODE , CX,CY,CZ  from BASE_CARGOSPACE  
                                          where CX  in ('01','02')  and  PALLET_CODE = '0' 
                                            and CZ in ('01','02','03','09') 
                                            and  CPOSITIONCODE not in (select distinct CPOSITIONCODE from VIEW_STOCK_CURRENT) ", id);
            }
        }
        if (id.Trim().Equals("01"))
        {
            //根据储位最后两位层数判断是高低储位  01 02 03 09 高储位  04 05 06 07 08 低储位
            if (cz == "04" || cz == "05" || cz == "06" || cz == "07" || cz == "08")
            {
                Sql = string.Format(@" select CPOSITIONCODE , CX,CY,CZ  from BASE_CARGOSPACE  
                                          where CX  in ('01','02')  and  PALLET_CODE = '0' 
                                            and CZ in ('04','05','06','07','08') 
                                            and  CPOSITIONCODE not in (select distinct CPOSITIONCODE from VIEW_STOCK_CURRENT) ", id);
            }
            else
            {
                Sql = string.Format(@" select CPOSITIONCODE , CX,CY,CZ  from BASE_CARGOSPACE  
                                          where CX  in ('01','02')  and  PALLET_CODE = '0' 
                                          and CZ in ('01','02','03','09') 
                                          and  CPOSITIONCODE not in (select distinct CPOSITIONCODE from VIEW_STOCK_CURRENT) ", id);
            }
        }
        else if (id.Trim().Equals("02"))
        {
            //根据储位最后两位层数判断是高低储位  01 02 03 09 高储位  04 05 06 07 08 低储位
            if (cz == "04" || cz == "05" || cz == "06" || cz == "07" || cz == "08")
            {
                Sql = string.Format(@" select CPOSITIONCODE , CX,CY,CZ  from BASE_CARGOSPACE  
                                          where CX  in ('01','02')  and  PALLET_CODE = '0' 
                                            and CZ in ('04','05','06','07','08') 
                                           and  CPOSITIONCODE not in (select distinct CPOSITIONCODE from VIEW_STOCK_CURRENT) ", id);
            }
            else
            {
                Sql = string.Format(@" select CPOSITIONCODE , CX,CY,CZ  from BASE_CARGOSPACE  
                                          where CX  in ('01','02')  and  PALLET_CODE = '0'
                                          and CZ in ('01','02','03','09') 
                                          and  CPOSITIONCODE not in (select distinct CPOSITIONCODE from VIEW_STOCK_CURRENT) ", id);
            }
        }
        else
        {

        }

        //获取阻挡储位时，如果其中包含正在执行的储位，要过滤去掉
        Sql = Sql +
        @"
               and  (cx+ cy+ cz) not in 
                (
                select a.loc from cmd_mst a where a.cmdno in ( select cmdno from cmd_mst where cmdsts in ('0','1')  ) 
                union
                select a.newloc from cmd_mst a where a.cmdno in ( select cmdno from cmd_mst where cmdsts in ('0','1')  ) and a.newloc is not null
                )
             ";

        DataTable dtINBILL_D = DBHelp.ExecuteToDataTable(Sql);
        return dtINBILL_D;
    }

    //pangao 20160525 判断是储位上是否为有货物，根据储位号码来查询
    public bool CheckStockByStopLoc(string stopLoc)
    {
        bool pda = false;
        try
        {
            string Sql = string.Format(@" select count(1) from VIEW_STOCK_CURRENT  VC
                                              inner join base_cargospace  bc on vc.cpositioncode = bc.cpositioncode
                                              where bc.cx+bc.cy+bc.cz = '{0}' ", stopLoc);
            DataTable tb = DBHelp.ExecuteToDataTable(Sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                if (Convert.ToInt32(tb.Rows[0][0].ToString()) == 0)
                {
                    pda = true;
                }
            }
        }
        catch (Exception)
        {
            pda = false;
        }
        return pda;
    }
    //pan gao 20160531
    public DataTable GetListByIDS(string ids)
    {
        string strSQL = string.Format(@" select obd.ids,obd.id,obd.PALLET_CODE,obd.cinvcode,obd.cinvname,obd.iquantity,obd.cpositioncode,obd.cposition,obd.wmstskid,obl.cticketcode 
                                              from outbill_d obd inner join outbill obl on obd.id = obl.id where obd.ids='{0}' ", ids);

        DataTable dtINBILL_D = DBHelp.ExecuteToDataTable(strSQL);
        return dtINBILL_D;
    }
    public DataRow GetOUTBILLLByID(string id)
    {
        string StrSql = string.Format("SELECT A.*,B.CTICKETCODE bCCODE, B.SPECIAL_OUT from OUTBILL A left join OUTASN B on A.COUTASNID=B.ID where A.ID='{0}'",id);
        DataTable dtOUTBILL = DBHelp.ExecuteToDataTable(StrSql);

        DataRow dr = null;

        if (dtOUTBILL.Rows.Count > 0)
        {
            dr = dtOUTBILL.Rows[0];
        }

        return dr;
    }
    //获取出库单Code
    public static string GetOutbillCode(string ids)
    {
        var sql = @"select CTICKETCODE from outbill_d od, outbill o where o.id = od.id and od.ids='" + ids.Trim() + "'";
        var obj = DBHelp.ExecuteScalar(sql);
        if (obj == null)
            return "";
        else
        return DBHelp.ExecuteScalar(sql).ToString();
    }

    //获取出库单Code
    public static string GetOutCode(string id)
    {
        var sql = @"select CTICKETCODE from outbill o where o.id = '" + id.Trim() + "'";
        var obj = DBHelp.ExecuteScalar(sql);
        if (obj == null) 
            return "";
        else
        return DBHelp.ExecuteScalar(sql).ToString();
    }
   
}