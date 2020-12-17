using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Business;

/// <summary>
/// InBill 的摘要说明
/// </summary>
public class InBill
{
    //private static DBContext context = new DBContext();
    public InBill()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    //判断是否存在补单中的明细
    public static bool CheckAsn_DStatus(string asn_id)
    {
        string Sql = string.Format(@"select count(1) from inasn_d iad where iad.id='{0}' and iad.cstatus in ('0','4') AND EXISTS (SELECT 1 FROM dbo.INASN a WITH(NOLOCK) WHERE a.id = iad.id AND a.cstatus='0')", asn_id);
        return Convert.ToInt32(DBHelp.ExecuteScalar(Sql)) > 0 ? true : false;
    }
    /// <summary>
    /// 批量扣账
    /// </summary>
    /// <param name="InBill_Ids"></param>
    /// <returns></returns>
    public static string BatchInBillTOStock_Currnt_Proc(Dictionary<string, string> InBill_Ids, string userNo)
    {
        StringBuilder sb = new StringBuilder();

        string guid = Guid.NewGuid().ToString();

        bool ck = true;
        foreach (string item in InBill_Ids.Keys)
        {
            string cguid = Guid.NewGuid().ToString();
            string cmsg = string.Empty;
            if (!InBill.Check_Proc_OutBillTOStock_Currnt(item, userNo, cguid, ref cmsg))
            {
                sb.Append(InBill_Ids[item] + "扣账失败!" + cmsg);
                ck = false;
            }
        }
        if (!ck)
        {
            return sb.ToString();
        }

        sb = new StringBuilder();
        foreach (string item in InBill_Ids.Keys)
        {
            string msg = string.Empty;
            #region 调用存储过程
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_InBillId:" + item);
            SparaList.Add("@P_UserNo:" + userNo);
            SparaList.Add("@P_ReturnValue:" + "");
            SparaList.Add("@INFOTEXT:" + "");
            string[] Result = DBHelp.ExecuteProc("Proc_InBillTOSTOCK_CURRENT", SparaList);
            if (Result.Length == 1)
            {

                sb.Append(InBill_Ids[item]+Result[1] + "扣账失败!");
            }
            else if (Result[0] == "1")
            {
                sb.Append(InBill_Ids[item] + "扣账失败!");
            }
            #endregion
            #region 注销
            //    if (Check_Proc_OutBillTOStock_Currnt(item, userNo, guid, ref msg))
            //    {
            //        Proc_InBillTOSTOCK_CURRENT proc = new Proc_InBillTOSTOCK_CURRENT();
            //        proc.P_InBillId = item;
            //        proc.P_UserNo = userNo;
            //        proc.Execute();
            //        if (proc.P_ReturnValue == 1)
            //        {
            //            sb.Append(InBill_Ids[item] + "扣账失败!\r\n");
            //        }
            //    }
            #endregion
            sb.Append(msg);
        }
        if (sb.ToString().Length == 0)
        {
            sb.Append("扣账操作成功!");
        }

        return sb.ToString();
    }
    /// <summary>
    /// 入库扣帐验证
    /// </summary>
    /// <param name="InBill_Id"></param>
    /// <param name="UserNo"></param>
    /// <param name="P_Guid"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static bool Check_Proc_OutBillTOStock_Currnt(string InBill_Id, string UserNo, string P_Guid, ref string msg)
    {
        bool returnValue = true;
        try
        {
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_InBillId:" + InBill_Id);
            SparaList.Add("@P_Guid:" + P_Guid);
            SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo.Trim());
            string Result = DBHelp.ExecuteProcReturnValue("Proc_CheckInBillTOCURRENT", SparaList, "@P_ReturnValue");
            if (Result != "0")
            {
                msg = new ErrorMsg().GetErrorMsg(P_Guid);
                returnValue = false;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            returnValue = false;
        }
        #region 注销
        //try
        //{
        //    Proc_CheckInBillTOCURRENT procCheck = new Proc_CheckInBillTOCURRENT();
        //    procCheck.P_InBillId = InBill_Id;
        //    procCheck.P_Guid = P_Guid;
        //    procCheck.P_UserNo = UserNo;
        //    procCheck.Execute();
        //    if (procCheck.P_ReturnValue > 0)
        //    {
        //        msg = new ErrorMsgQuery().GetErrorMsg(P_Guid);
        //        returnValue = false;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message;
        //    returnValue = false;
        //}
        #endregion
        return returnValue;
    }

    /// <summary>
    /// 根据入库通知单ID获取入库通知单信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public DataTable GetInAsnById(string id)
    {
        string sql = "select * from Inasn ia where ia.id='" + id + "'";

        return DBHelp.ExecuteToDataTable(sql);
    }
    /// <summary>
    /// 验证是否有上架明细
    /// </summary>
    /// <param name="cticketcode">入库单单据号</param>
    /// <returns></returns>
    public static bool ValidateIsExistTemp_InBill_D(string cticketcode)
    {
        string sql = "select count(*) from temp_inbill_d t where t.inbillcticketcode = '" + cticketcode.Trim() + "'";
        return Convert.ToInt32(DBHelp.ExecuteScalar(sql)) > 0 ? true : false;
    }
    /// <summary>
    /// 入库专用
    /// </summary>
    /// <param name="CINVCODE">料号</param>
    /// <param name="CPOSITIONCODE">储位</param>
    /// <param name="isRight">是否右边模糊：false全模糊</param>
    /// <param name="size">显示数量</param>
    /// <returns></returns>
    /// //2015-11-30 添加数量参数Sum[ASRS专用]
    public DataTable GetCargoSpaceListByBaseData(string CINVCODE, string CPOSITIONCODE, bool isRight, int size, string sum)
    {
        //2015-11-30 添加数量Sum，查询符合入库数量的库，并由小到大排序
        string strSQL = string.Format(@"select top {0} bc.cpositioncode , bc.cposition, isnull((select (bc.imaxcapacity- isnull((sum(sc.iqty)),0)) as qit from stock_current sc where sc.cpositioncode = bc.cpositioncode
                                            ),0)IQTY
  from BASE_PART_CARGOSPACE P inner JOIN base_cargospace bc ON P.cpositioncode=bc.cpositioncode
  AND P.CPARTNUMBER = '" + CINVCODE.Trim() + @"' and isnull((select (bc.imaxcapacity- isnull((sum(sc.iqty)),0)) as qit from stock_current sc where sc.cpositioncode = bc.cpositioncode
                                            ),0)>'" + sum + "'", size);
        strSQL += "  order by IQTY";
        return DBHelp.ExecuteToDataTable(strSQL);
    }
    /// <summary>
    /// 判断储位是否有栈板
    /// 占用,返回false
    /// 未占用，返回true
    /// </summary>
    /// <param name="cpositioncode">储位编码</param>
    /// <returns></returns>
    public bool CheckBASE_CARGOSPACE(string cpositioncode)
    {
        bool pda = false;
        try
        {
            string Sql = string.Format(@"select a.pallet_code from BASE_CARGOSPACE a where a.cpositioncode  = '{0}' ", cpositioncode);
            DataTable tb = DBHelp.ExecuteToDataTable(Sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                if (Convert.ToString(tb.Rows[0][0].ToString()) == "0")
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
    // 判断相邻储位是否有阻挡[入库] 
    public string GetStopCurrent_RD(string id)
    {
        string strCposition = "";
        try
        {

            string Sql = string.Format(@" select  distinct CPOSITIONCODE  from base_cargospace  where  Pallet_code = 1 and CPOSITIONCODE in  
                                             (select CPOSITIONCODE   from  BASE_CARGOSPACE
                                                      where CX+2 = (select CX  from  BASE_CARGOSPACE
                                                      where CPOSITIONCODE in ( select CPOSITIONCODE  from inbill_d bid where
                                                 bid.ids='{0}' ))
 
                                                        and CY =(select CY  from  BASE_CARGOSPACE
                                                      where CPOSITIONCODE in ( select CPOSITIONCODE  from inbill_d bid where
                                                 bid.ids='{0}' ))
                                                        and CZ = (select CZ  from BASE_CARGOSPACE
                                                      where CPOSITIONCODE in ( select CPOSITIONCODE  from inbill_d bid where
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
        //获取阻挡储位时，如果其中包含正在执行的储位，要过滤去掉
        Sql = Sql +
        @"
               and (cx+ cy+ cz) not in 
                (
                select loc from cmd_mst  where cmdno in ( select cmdno from cmd_mst where cmdsts in ('0','1')  ) 
                union
                select newloc from cmd_mst  where cmdno in ( select cmdno from cmd_mst where cmdsts in ('0','1')  ) and newloc is not null
                )
             ";
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

    public bool CheckBASE_CARGOSPACE_OK(string ids)
    {
        bool pda = false;
        try
        {
            string Sql = string.Format(@"select a.pallet_code from BASE_CARGOSPACE a 
                                            where a.cpositioncode  
                                            in (SELECT A.cpositioncode from INBILL_D A where A.ids = '{0}') ", ids);
            DataTable tb = DBHelp.ExecuteToDataTable(Sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                if (Convert.ToString(tb.Rows[0][0].ToString()) == "0")
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


    //pan gao 20160531
    public DataTable GetListByIDS(string ids)
    {
        string strSQL = string.Format(@" select ind.ids,ind.id,ind.PALLET_CODE,ind.cinvcode,ind.cinvname,ind.iquantity,ind.cpositioncode,ind.cposition,ind.wmstskid,ib.cticketcode
                                             from inbill_d ind  inner join inbill ib on ind.id= ib.id where ind.ids='{0}' ", ids);

        DataTable dtINBILL_D = DBHelp.ExecuteToDataTable(strSQL);
        return dtINBILL_D;
    }
    /// <summary>
    /// 判断储位是否存在
    /// 存在,返回true
    /// 不存在，返回false
    /// </summary>
    /// <param name="cpositioncode">储位编码</param>
    /// <returns></returns>
    public bool CheckBASEExistingCARGOSPACE(string cpositioncode)
    {
        bool pda = false;
        try
        {
            string Sql = string.Format(@"select count(1) from BASE_CARGOSPACE a where a.cpositioncode  = '{0}' ", cpositioncode);
            DataTable tb = DBHelp.ExecuteToDataTable(Sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                if (Convert.ToString(tb.Rows[0][0].ToString()) == "1")
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
    /// <summary>
    /// 根据入库通知单ID和料号获取入库通知明细信息
    /// </summary>
    /// <param name="InBillId"></param>
    /// <param name="cinvcode"></param>
    /// <param name="inbill_qty"></param>
    /// <returns></returns>
    public DataTable GetInAsn_DByInBillIdAndCinv(string InBillId, string cinvcode, decimal inbill_qty)
    {
        //string sql = "select CERPCODELINE,IPOLINE,CINVBARCODE,CMEMO from InAsn_d iad where iad.id=(select ib.casnid from InBill ib where ib.id='" + InBillId + "') and iad.cinvcode='" + cinvcode + "'";
        string sql = string.Format(@"select iad.CERPCODELINE, iad.IPOLINE, iad.CINVBARCODE, iad.CMEMO
                                          from InAsn_d iad
                                          left join (select bi.casnid,bid.cinvcode,bid.cerpcodeline,sum(bid.iquantity)iqty from inbill_d bid,inbill bi 
                                                    where bid.id=bi.id group by bi.casnid,bid.cinvcode,bid.cerpcodeline)t1 
                                                  on t1.casnid=iad.id and t1.cinvcode=iad.cinvcode
                                                     and ((iad.cerpcodeline = t1.cerpcodeline) or (t1.cerpcodeline is null and iad.cerpcodeline is null))
                                         where iad.id =(select ib.casnid  from InBill ib where ib.id = '{0}')
                                           and iad.cinvcode = '{1}' and iad.manual = 1 and iad.iquantity - isnull(t1.iqty,0) + {2} > 0 
                                           order by iad.cerpcodeline asc ", InBillId, cinvcode, inbill_qty);
        return DBHelp.ExecuteToDataTable(sql);
    }
    //修改入库通知单明细状态
    public static void UpdateAsnStatus(string billId, string cinvcode)
    {
        string Sql = string.Format(@"  update inasn_d set cstatus='4'
                                where exists (select 1 from inbill bi where bi.id='{0}' and bi.casnid=inasn_d.id)
                                and cinvcode='{1}'", billId, cinvcode);
        DBHelp.ExecuteNonQuery(Sql);
    }
    //校验SN格式
    public static string CheckSNFormate(string snCode)
    {
        var sql = @"select [dbo].[Fun_GetDateCode_FromSN]('" + snCode.Trim() + "')";
        return DBHelp.ExecuteScalar(sql).ToString();
    }
    /// <summary>
    /// 检查SN是否已经保存过
    /// </summary>
    /// <param name="inbill_d_ids"></param>
    /// <returns></returns>
    public static bool CheckIsExistInBill_D_SN(string inbill_d_ids)
    {
        string Sql = string.Format(@"select count(1) from inbill_d_sn sn where sn.inbill_d_ids='{0}'", inbill_d_ids);
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
            string Sql = string.Format(@"select count(*) from inbill ib where ib.id='{0}' and ib.cstatus='{1}'", id, strstatus);
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
    /// 检查是否PDA产生的出库单
    /// <summary>
    /// 检查是否PDA产生的出库单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool CheckPDA(string id)
    {
        bool pda = false;
        try
        {
            string Sql = string.Format(@"select count(*)
                                              from temp_inbill_d temp
                                             inner join inbill bi
                                                on bi.cticketcode = temp.inbillcticketcode
                                             where bi.id = '{0}'", id);
            DataTable tb = DBHelp.ExecuteToDataTable(Sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                if (Convert.ToInt32(tb.Rows[0][0].ToString()) > 0)
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
    //判断除本明细外是否存在ids
    public static bool ExistSNBySnCode(string palletCode, string sncode, string ids)
    {
        //var sql = @"select count(1) from (select count(1) qty from inbill_d_sn t where t.SN_CODE = '" + sncode + "' and t.INBILL_D_IDS <> '" + ids + 
        //          @"' union select count(1) qty from temp_inbill_d tid where tid.cinvbarcode  = '" + sncode + "') where qty > 0";
        string sql = string.Format(@"select count(1)
                                              from (select count(1) qty
                                                      from inbill_d_sn t inner join inbill bi on bi.id=t.inbill_id
                                                     where t.SN_CODE = '{0}' and t.INBILL_D_IDS <> '{1}' and t.PalletCode<>'{2}' and bi.cstatus = '0'
                                                    union
                                                    select count(1) qty
                                                      from temp_inbill_d tid
                                                     where tid.cinvbarcode = '{0}' and PalletCode<>'{2}' and tid.cstatus ='0'
                                                    union
                                                    select count(1) qty
                                                      from allocate_scan_qty sc inner join allocate te  on te.id = sc.id
                                                     where te.special = 1 and sc.sncode = '{0}'	 and te.palletcode<> '{2}'
                                                       and te.cstatus not in ('2', '3', '9')
                                                       union
                                                       select count(1) qty from stock_current_sn sc
                                                       where sc.sncode = '{0}' and sc.palletcode <> '{2}'
													   
													   ) a
                                             where qty > 0  ", sncode, ids, palletCode);

        return int.Parse(DBHelp.ExecuteScalar(sql).ToString()) > 0 ? true : false;
    }

    //判断除本明细外是否存在ids
    public static bool ExistPCSNBySnCode(string sncode)
    {
        string sql = string.Format(@"select count(1)
                                    from (select count(1) qty
                                            from inbill_d_sn t inner join inbill bi on bi.id=t.inbill_id
                                            where t.SN_CODE = '{0}' and  bi.cstatus = '0' 
                                        union
                                        select count(1) qty
                                            from temp_inbill_d tid
                                            where tid.cinvbarcode = '{0}'and tid.cstatus ='0'
                                        union
                                        select count(1) qty
                                            from allocate_scan_qty sc inner join allocate te  on te.id = sc.id
                                            where te.special = 1 and sc.sncode = '{0}'	 
                                            and te.cstatus not in ('2', '3', '9')
                                            union
                                            select count(1) qty from stock_current_sn sc
                                            where sc.sncode = '{0}' 
													   
				                            ) a
                                    where qty > 0  ", sncode);

        return int.Parse(DBHelp.ExecuteScalar(sql).ToString()) > 0 ? true : false;
    }
    public static bool ExistPCSNBySnCode1(string sncode,string ids)
    {
        string sql = string.Format(@"select count(1)
                                    from (select count(1) qty
                                            from inbill_d_sn t inner join inbill bi on bi.id=t.inbill_id
                                            where t.SN_CODE = '{0}' and  bi.cstatus = '0' and t.inbill_id <> '{1}'
                                        union
                                        select count(1) qty
                                            from temp_inbill_d tid
                                            where tid.cinvbarcode = '{0}'and tid.cstatus ='0'
                                        union
                                        select count(1) qty
                                            from allocate_scan_qty sc inner join allocate te  on te.id = sc.id
                                            where te.special = 1 and sc.sncode = '{0}'	 
                                            and te.cstatus not in ('2', '3', '9')
                                            union
                                            select count(1) qty from stock_current_sn sc
                                            where sc.sncode = '{0}' 
													   
				                            ) a
                                    where qty > 0  ", sncode, ids);

        return int.Parse(DBHelp.ExecuteScalar(sql).ToString()) > 0 ? true : false;
    }


    public static bool IsWipReturn(string ids)
    {
        var sql =
            @"select count(*) from inbill bi 
                                inner join inbill_d bid on bi.id=bid.id
                                where bi.ITYPE = '43'
                                and bid.ids='" +
            ids.Trim() + "'";
        return int.Parse(DBHelp.ExecuteScalar(sql).ToString()) > 0 ? true : false;
    }

    public static string CheckSNCode(string snCode)
    {
        var sql = string.Format(@"select dbo.FUN_CHECKSNCODE('{0}','{1}')", snCode, string.Empty);            

        return DBHelp.ExecuteScalar(sql);
    }

    //获取入库通知单单号
    public static string GetAsnCodeByInbillCode(string CticketCode)
    {
        var sql = @"select t.cticketcode from inasn t where t.id = (select tl.casnid from inbill tl where tl.cticketcode = '" + CticketCode + "')";
        return DBHelp.ExecuteScalar(sql).ToString();
    }

    //校验WR
    public static string WRCheck(string CticketCode, string snCode, string qty, string type, string snids)
    {
        var sql = @"select [dbo].[Fun_CheckSN_WipReturn]('" + CticketCode + "','" + snCode + "','" + qty + "','" + type + "','" + snids + "')";
        return DBHelp.ExecuteScalar(sql).ToString();
    }
    //判断SN是否存在箱中
    public static bool ExistSNBySnCode(string snCode)
    {
        var sql = @"select count(1) from bar_carton_d bcd where bcd.sncode = '" + snCode + "'";
        return int.Parse(DBHelp.ExecuteScalar(sql).ToString()) > 0 ? true : false;
    }
    //获取栈板箱的数量
    public static decimal GetPalletOrCarton(string pcCode)
    {
        string Sql = string.Format(@"select isnull(sum(t.qty),0) from view_get_palorcar_qty t where t.SNNo = '{0}'", pcCode);
        return decimal.Parse(DBHelp.ExecuteScalar(Sql).ToString());
    }
    //判断wip return 是否已存在指引，true 存在 false不存在
    public static bool CheckWipReturn_Assit(string inbill_Id)
    {
        string Sql = string.Format(@"select count(1) from inassit ia inner join inasn sn  on sn.id = ia.casnid where sn.itype = '43'
                                        and exists (select 1 from inasn ias inner join inbill bi  on bi.casnid = ias.id 
                                        where ias.cerpcode = sn.cerpcode and bi.id = '{0}')", inbill_Id);
        return Convert.ToInt32(DBHelp.ExecuteScalar(Sql)) > 0 ? true : false;

    }
    //判断入库单是否是指定类型，是true，否false
    public static bool CheckInType(string id, string type)
    {
        string Sql = string.Format(@"select count(1) from inbill bi where bi.id='{0}' and bi.itype='{1}'", id, type);
        return Convert.ToInt32(DBHelp.ExecuteScalar(Sql)) > 0 ? true : false;
    }
    /// <summary>
    /// 验证工单退料(WIP Return=43)退的数量是否有效
    /// </summary>
    /// <param name="InBill_id">入库单ID</param>
    /// <param name="CINVCODE">料号</param>
    /// <param name="Qty">当前输入数量</param>
    /// <param name="Type">操作类型(0:新增,1:修改)</param>
    /// <param name="originalQty">原始数量</param>
    /// <param name="cpositioncode">储位编码</param>
    /// <returns>0：表示成功</returns>
    public static string CheckWipReturnQty_InBill(string InBill_id, string CINVCODE, string cpositioncode, string Qty, string Type, string originalQty)
    {
        /*Roger 解决储位问题 2013/6/21 18:02:54*/
        string sql = "select [dbo].[Fun_CheckWipReturnQty_IB]('" + InBill_id.Trim() + "','" + CINVCODE.Trim() + "','" + cpositioncode.Trim() + "','" + Qty.Trim() + "','" + Type.Trim() + "','" + originalQty.Trim() + "','')";
        return DBHelp.ExecuteScalar(sql).ToString();
    }


    //判断所保存的明细是否已经存在入库单中
    public static bool CheckInBillIs_ExistAsnIDS(string asnids, string inbillid)
    {
        string Sql = string.Format(@"select count(1) from inbill_d bid where bid.id='{0}' and bid.asn_d_ids='{1}'", inbillid, asnids);
        return Convert.ToInt32(DBHelp.ExecuteScalar(Sql)) > 0 ? true : false;

    }

    //获取入库通知单明细的dateCode
    public static string GetAsn_DateCode(string ids)
    {
        if (ids.Trim().Length < 1)
        {
            return "";
        }
        else
        {
            string Sql = string.Format(@"select isnull(iad.datecode,0) from inasn_d iad where iad.ids='{0}'", ids);
            object Return = DBHelp.ExecuteScalar(Sql);
            if (Return == null)
                return "";
            else
                return Return.ToString();
        }

    }
    //判断是否为补单
    public static bool IsBD(string ids)
    {
        var sql = @"select count(1)  from inbill_d td, inbill t where t.id = td.id and not exists(select 1 from inassit st,inassit_d ist where st.id=ist.id and ist.cinvcode=td.cinvcode and st.casnid = t.casnid) and td.ids='" + ids.Trim() + "'";
        return int.Parse(DBHelp.ExecuteScalar(sql).ToString()) > 0 ? true : false;
    }
    //Roger 2013/11/28 14:02:35 SN整合 如果存在SN，则必须先删除SN才可以修改数量
    public static bool ExistSN(string ids)
    {
        var sql = @"select count(1) from inbill_d_sn t where t.INBILL_D_IDS = '" + ids + "'";
        return int.Parse(DBHelp.ExecuteScalar(sql).ToString()) > 0 ? true : false;
    }
    //删除SN
    public void DeleteSn(string ids)
    {
        var sql = @"delete from inbill_d_sn where inbill_d_ids = '" + ids.Trim() + "'";
        DBHelp.ExecuteNonQuery(sql);
    }

    public void DeleteBn(string ids)
    {
        var sql = @"delete from inbill_d_bn where SNIDS in (select id from inbill_d_sn where inbill_d_ids = '" + ids.Trim() + "')";
        DBHelp.ExecuteNonQuery(sql);
    }

    /// <summary>
    /// 更新阻挡和目的储位
    /// </summary>
    public int UpdatePostionForStop(string type, string ids, string stopCode, string newCode)
    {
        string sql = string.Empty;
        if (type.ToUpper().Equals("IN"))
        {
            sql = string.Format("update inbill_d set cstoppositioncode='{0}',cnewpositioncode='{1}' where ids='{2}' ", stopCode, newCode, ids);
        }
        else
        {
            sql = string.Format("update outbill_d set cstoppositioncode='{0}',cnewpositioncode='{1}' where ids='{2}' ", stopCode, newCode, ids);
        }
        return DBHelp.ExecuteToInt(sql);
    }

    #region 补单
    /// <summary>
    /// 判断储位是否有库存
    /// </summary>
    /// <param name="cpositioncode">储位编码</param>
    /// <returns>true：存在库存 false：不存在库存</returns>
    public static bool IsHasStockCurrent(string cpositioncode)
    {
        string strSQL = string.Format(@"SELECT  COUNT(1)
                                        FROM    dbo.STOCK_CURRENT a
                                                LEFT JOIN dbo.STOCK_CURRENT_SN b ON a.id = b.stock_id
                                        WHERE   a.cpositioncode = '{0}'
                                                AND b.qty > 0", cpositioncode);
        if (int.Parse(DBHelp.ExecuteScalar(strSQL)) > 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 判断储位是否在做循环盘点
    /// </summary>
    /// <param name="cpositioncode">储位编码</param>
    /// <returns>true：存在循环盘点 false：不存在循环盘点中</returns>
    public static bool IsHasCheckBill(string cpositioncode)
    {
        string strSQL = string.Format(@"SELECT   COUNT(1) 
			                                    FROM    STOCK_CHECKBILL sc with(nolock)
			                                    INNER JOIN STOCK_CHECKBILL_D scd with(nolock) ON scd.id = sc.id
			                                    WHERE   sc.cstatus in ('0','1','6')    ---0:未处理 1：已审核 6：盘点中 5：盘点完成
			                                    AND scd.cpositioncode = '{0}'
			                                    ", cpositioncode);
        if (int.Parse(DBHelp.ExecuteScalar(strSQL)) > 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 判断储位库存是否满
    /// </summary>
    /// <param name="cpositioncode">储位编码</param>
    /// <returns>true：存在库存 false：不存在库存</returns>
    public static decimal IsMaxCapacity(string cpositioncode)
    {
        string strSQL = string.Format(@"SELECT  sum(isnull(a.iqty,0))
                                        FROM    dbo.STOCK_CURRENT a
                                                LEFT JOIN dbo.STOCK_CURRENT_SN b ON a.id = b.stock_id
                                        WHERE   a.cpositioncode = '{0}'", cpositioncode);
        return decimal.Parse(DBHelp.ExecuteScalar(strSQL));
    }



    /// <summary>
    /// 判断明细储位是否相同
    /// </summary>
    /// <param name="id">入库单头ID</param>
    /// <param name="cpositioncode">储位编码</param>
    /// <param name="ids">入库单表明细IDs</param>
    /// <returns>true：明细存在不同储位 false：不存在</returns>
    public static bool CpositionCodeIdentical(string id, string cpositioncode,string ids)
    {
        string strSQL = string.Format(@"SELECT  COUNT(1)
                                        FROM    dbo.INBILL a
                                                LEFT JOIN dbo.INBILL_D b ON a.id = b.id
                                        WHERE   a.id = '{0}'
                                                AND b.cpositioncode IS NOT NULL
                                                AND b.cpositioncode != '{1}'
                                                AND b.ids!='{2}'", id, cpositioncode,ids);
        if (int.Parse(DBHelp.ExecuteScalar(strSQL)) > 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 判断储位是否被占用
    /// </summary>
    /// <param name="cpositionCode">储位编码</param>
    /// <returns>true：储位被占用 false：未被占用</returns>
    public static bool CpositionCodeIsOccupation(string cpositionCode, string id)
    {
        string strSQL = string.Format(@"SELECT  COUNT(1)
                                        FROM    BASE_CARGOSPACE
                                        WHERE   cpositioncode = '{0}'
                                                AND cpositioncode NOT IN ( SELECT   cpositioncode
                                                                           FROM     dbo.TEMP_INBILL_D
                                                                           WHERE    cstatus IN ( '0', '3' ) ------ ( '0','1','3' ) 20200710更新，去掉已经完成的 
                                                                                    AND cpositioncode IS NOT NULL )
                                                AND cpositioncode NOT IN (
                                                SELECT  ald.ctopositioncode
                                                FROM    ALLOCATE_D ald
                                                        LEFT JOIN ALLOCATE al ON ald.id = al.id
                                                WHERE   al.cstatus NOT IN ( '2', '3' )
                                                        AND ald.cpositioncode IS NOT NULL )
                                                AND cx + cy + cz NOT IN ( SELECT    c.NewLoc
                                                                          FROM      CMD_MST c
                                                                          WHERE     c.CmdSts IN ( '0', '1' )
                                                                                    AND c.NewLoc IS NOT NULL )
                                                AND cpositioncode NOT IN (
                                                SELECT  b.cpositioncode
                                                FROM    dbo.INBILL a
                                                        LEFT JOIN dbo.INBILL_D b ON a.id = b.id
                                                WHERE   a.cstatus = '0'
                                                        AND a.id != '{1}'
                                                        AND b.cpositioncode IS NOT NULL )", cpositionCode, id);
        if (int.Parse(DBHelp.ExecuteScalar(strSQL)) == 0)
            return true;
        else
            return false;
    }
    /// <summary>
    /// 判断库存是否已存在箱号
    /// </summary>
    /// <param name="palletCode">箱号</param>
    /// <returns>true:存在 false：不存在</returns>
    public static bool StockCurrentHasPalletCode(string palletCode)
    {
        string strSQL = string.Format(@"SELECT  COUNT(1)
                                        FROM    dbo.STOCK_CURRENT_SN
                                        WHERE   sncode = '{0}'
                                                AND qty > 0
                                    ", palletCode);
        if (int.Parse(DBHelp.ExecuteScalar(strSQL)) > 0)
            return true;
        else
            return false;
    }
    /// <summary>
    /// 获取同一入库单下的sn
    /// </summary>
    /// <param name="ids">入库单明细ids</param>
    /// <param name="id">入库单id</param>
    /// <returns></returns>
    public static DataTable GetPalletCode(string ids, string id)
    {
        string strSQL = string.Format(@"SELECT DISTINCT
                                                c.sn_code
                                         FROM   dbo.INBILL a
                                                LEFT JOIN dbo.INBILL_D b ON a.id = b.id
                                                LEFT JOIN dbo.INBILL_D_SN c ON a.id = c.inbill_id
                                         WHERE  a.id = '{0}'
                                                AND b.ids != '{1}'
                                                AND c.sn_code IS NOT NULL", id, ids);
        return DBHelp.ExecuteToDataTable(strSQL);
    }

    /// <summary>
    /// 获取储位线别
    /// </summary>
    /// <param name="cpositionCode"></param>
    /// <returns></returns>
    public static string GetCpositionLineid(string cpositionCode)
    {
        string strSQL = string.Format(@"SELECT  lineid
                                        FROM    dbo.BASE_CARGOSPACE
                                        WHERE   cpositioncode = '{0}'", cpositionCode);
        return DBHelp.ExecuteScalar(strSQL);
    }

    /// <summary>
    /// 获取储位类型
    /// </summary>
    /// <param name="cpositionCode"></param>
    /// <returns></returns>
    public static string GetCpositionCodeCtype(string cpositionCode)
    {
        string strSQL = string.Format(@"SELECT  ctype
                                        FROM    dbo.BASE_CARGOSPACE
                                        WHERE   cpositioncode = '{0}'", cpositionCode);
        return DBHelp.ExecuteScalar(strSQL);
    }

    /// <summary>
    /// 判断线别站点是否与其他明细相同
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ids"></param>
    /// <param name="lineid"></param>
    /// <param name="siteid"></param>
    /// <returns></returns>
    public static bool GetInbillDCraneSite(string id, string ids, string lineid, string siteid)
    {
        string strSQL = string.Format(@"SELECT  COUNT(1)
                                        FROM    dbo.INBILL a
                                                LEFT JOIN dbo.INBILL_D b ON a.id = b.id
                                        WHERE   a.id = '{0}'
                                                AND b.ids != '{1}'
                                                AND(b.pallet_code != '{2}'
                                                or b.wire != '{3}')
                                                AND b.pallet_code IS NOT NULL
                                                AND b.wire IS NOT NULL", id, ids, siteid, lineid);
        if (int.Parse(DBHelp.ExecuteScalar(strSQL)) > 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 获取线别Cranid
    /// </summary>
    /// <param name="craneid"></param>
    /// <returns></returns>
    public static string GetCraneId(string craneid)
    {
        string strSQL = string.Format(@"SELECT  CRANEID
                                        FROM    dbo.BASE_CRANECONFIG
                                        WHERE   ID = '{0}'", craneid);
        return DBHelp.ExecuteScalar(strSQL);
    }
    #endregion
    /// <summary>
    /// 检查栈板号是否已存在
    /// </summary>
    /// <param name="palletCode"></param>
    /// <param name="snCode"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool ExistPalletCodeBySnCode(string palletCode, string snCode, string id)
    {
        string sql = string.Format(@"select COUNT(1)
                                              from (select count(1) qty
                                                      from inbill_d_sn t inner join inbill bi on bi.id=t.inbill_id
                                                     where t.PalletCode = '{0}' and  t.inbill_id <> '{1}' and bi.cstatus = '0'
                                                    union
                                                    select count(1) qty
                                                      from temp_inbill_d tid
                                                     where tid.PalletCode = '{0}' and tid.cstatus ='0'
                                                    union
                                                    select count(1) qty
                                                      from allocate_scan_qty sc inner join allocate te  on te.id = sc.id
                                                     where te.special = 1 and te.palletcode = '{0}'
                                                       and te.cstatus not in ('2', '3', '9')
                                                       union
                                                       select count(1) qty from stock_current_sn sc
                                                       where sc.palletcode = '{0}') a
                                             where qty > 0  ", palletCode, id);


        return int.Parse(DBHelp.ExecuteScalar(sql).ToString()) > 0 ? true : false;
    }


    public static string CheckPalletCodeByBill(string billId)
    {
        string sql = string.Format(@"select isnull(PalletCode,'') from dbo.INBILL_D_SN  WHERE inbill_id='{0}' ", billId);

        return DBHelp.ExecuteScalar(sql);

    }

    /// <summary>
    /// 判断当前sn是否存在于未完成的暂存调立库的调拨单中
    /// </summary>
    /// <param name="sncode"></param>
    /// <returns></returns>
    public static bool CheckExistsInTempAllo(string sncode)
    {
        string sql = string.Format(@"select COUNT(1)
                                             FROM dbo.ALLOCATE ob with(nolock)
			                                INNER JOIN dbo.ALLOCATE_D_SN dsn with(nolock) on ob.id = dsn.allocate_id
			                                WHERE (dsn.palletcode = '{0}' OR dsn.sn_code='{0}')
			                                  AND ob.cstatus in('0','1')
			                                  AND ob.allotype ='4'", sncode);

        return int.Parse(DBHelp.ExecuteScalar(sql).ToString()) > 0 ? true : false;
    }

}