using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// InAsn 的摘要说明
/// </summary>
public class InAsn
{
	public InAsn()
	{

        //
		// TODO: 在此处添加构造函数逻辑
		//
	}
    //判断是否为特殊退料
    public static bool isSpecialWipReturn(string id)
    {
        DBContext context = new DBContext();
        IGenericRepository<INASN> entity = new GenericRepository<INASN>(context);
        var caseList = from p in entity.Get()
                       where p.specil_return == '1' && p.id == id
                       select p;
        if (caseList.Count() > 0)
            return true;
        else
            return false;
    }
    /// <summary>
    /// 验证出库通知单的明细是否都生成了出库单
    /// </summary>
    /// <param name="outAsnId"></param>
    /// <returns></returns>
    public static bool ValidateInAsn_DIsAllCreateInBill(string InAsnId)
    {
        string sql = @"select count(*)
                            from
                            (
                                select iad.cinvcode,nvl(sum(iad.iquantity),0)InAsn_Qty,
                                        (select nvl(sum(ibd.iquantity),0)iquantity
                                        from InBill_d ibd inner join InBill ib
                                                on ibd.id=ib.id
                                        where ibd.cinvcode=iad.cinvcode
                                            and ib.CASNID='" + InAsnId.Trim() + @"')InBill_Qty
                                from InAsn_d iad
                                where iad.id='" + InAsnId.Trim() + @"'
                                group by iad.cinvcode
                            )newTable
                            where (InAsn_Qty-InBill_Qty) > 0";
        return Convert.ToInt32(DBHelp.ExecuteScalar(sql)) == 0 ? false : true;
    }
    /// <summary>
    /// 验证入库通知单是否生成入库单
    /// </summary>
    /// <param name="InAsnId"></param>
    /// <returns></returns>
    public static bool ValidateIsCreateInBill(string InAsnId)
    {
        DBContext context = new DBContext();
        IGenericRepository<INBILL> entity = new GenericRepository<INBILL>(context);
        var caseList = from p in entity.Get()
                       where p.casnid == InAsnId.Trim()
                       select p;
        if (caseList.Count() > 0)
            return true;
        else
            return false;
    }
    /// <summary>
    /// 获取入库通知状态
    /// </summary>
    /// <param name="InAsn_Id"></param>
    /// <returns></returns>
    public static string GetInAsnStatusByInAsnId(string InAsn_Id)
    {
        string sql = "select ia.cstatus from InAsn ia where ia.id='" + InAsn_Id.Trim() + "'";
        object Return = DBHelp.ExecuteScalar(sql);
        if (Return == null)
            return "";
        else
            return Return.ToString();
    }
    /// <summary>
    /// 根据料号和入库通知单ID获取相应的入库数量
    /// </summary>
    /// <param name="cinvcode">料号</param>
    /// <param name="InAsn_Id">入库通知单ID</param>
    /// <returns>入库数量 0表示没有找到相应的入库数量</returns>
    public decimal GetInQtyByCinvcodeAndInAsnId(string cinvcode, string InAsn_Id)
    {
        string sql = "select iad.iquantity from Inasn_d iad where iad.id='" + InAsn_Id + "' and iad.cinvcode='" + cinvcode + "'";

        object obj = DBHelp.ExecuteScalar(sql);

        if (obj != null)
        {
            return Convert.ToDecimal(obj);
        }

        return 0;
    }

    /// <summary>
    /// 允许做特殊元件退料的入库类型
    /// </summary>
    /// <returns></returns>
    /// 2015-12-08 去除作废的状态
    public static DataTable GetInTypeBySpecialReturn2(bool IsSearch)
    {
        string sql = @"select typename,cerpcode from intype it 
                         INNER JOIN dbo.V_SYS_PARAMETER s WITH(NOLOCK) ON s.FLAG_ID= f.cerpcode AND s.FLAG_TYPE='INTYPE' AND s.LANGUAGEID='"+System.Threading.Thread.CurrentThread.CurrentCulture.Name+"' where it.transaction_source_type_id=5  and it.attribute1='N' and transaction_action_id in ('1', '27', '33', '34') and it.enable='0'";
        if (!IsSearch)
        {
            sql += " and (DISABLE_DATE is null or DISABLE_DATE >= sysdate)";
        }
        return DBHelp.ExecuteToDataTable(sql);
    }
    //判断工单下料号状态是否满足异动条件
    //20130731134649
    public static string CanModDebit(string ErpCode, string CinvCode, string Type, string AsnId, string ReserveField1, string ReserveField2)
    {
        var sql = "select [dbo].[Fun_check_mo_cinvcode_flag]('" + ErpCode + "','" + CinvCode + "','" +
                  Type + "','" + AsnId + "','" + ReserveField1 +
                  "','" + ReserveField2 + "')";
        return (string)DBHelp.ExecuteScalar(sql);
    }
    //判断通知单是否满足异动条件
    //20131105102731
    public static string CanAsnDebit(string AsnCode, string CinvCode, string Type, string ReserveField1, string ReserveField2)
    {
        var sql = "select [dbo].[Fun_check_cinvcodebyAsn]('" + AsnCode + "','" + CinvCode + "','" +
                  Type + "','" + ReserveField1 +
                  "','" + ReserveField2 + "')";
        return (string)DBHelp.ExecuteScalar(sql);
    }
    /// <summary>
    /// 获取入库通知单类型-编辑新增
    /// </summary>
    /// <param name="IsSearch"></param>
    /// <returns></returns>
    /// 2015-12-08 去除作废的状态
    public static DataTable GetInTypeNew2(bool IsSearch)
    {
        string sql =string.Format( @"select distinct f.typename as FUNCNAME,f.cerpcode as EXTEND1 
                       from intype f inner join ( select FUNCNAME,EXTEND1 from UserFunction where userno='{0}') t  on t.EXTEND1 = f.cerpcode 
                       where 1=1 and f.enable='0'", WmsWebUserInfo.GetCurrentUser().UserNo);
        if (!IsSearch)
        {
            //增删改
            sql += " and f.Is_Query !='1'  and f.cerpcode not in('18','2','99999')  and (DISABLE_DATE is null or DISABLE_DATE >= getdate())";
        }
        return DBHelp.ExecuteToDataTable(sql);
    }
    /// 检查入库通知单是否有通知单明细
    /// <summary>
    /// 检查入库通知单是否有通知单明细
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool CheckInAsn_DCount(string id)
    {
        DBContext context = new DBContext();
        bool asn_d = false;
        try
        {
            IGenericRepository<INASN_D> entity = new GenericRepository<INASN_D>(context);
            var caseList = from p in entity.Get()
                           where p.id == id.Trim()
                           select p;
            if(caseList.Count()>0) asn_d = true;
        }
        catch (Exception err)
        {
            asn_d = false;
        }
        return asn_d;

    }
    /// <summary>
    /// 验证入库单明细是否生成过入库单
    /// Fun_GetInBill_D_Qty(A.CINVCODE,A.Cerpcodeline,A.Id)
    /// </summary>
    /// <param name="CINVCODE">料号</param>
    /// <param name="Cerpcodeline">ErpCode</param>
    /// <param name="InAsn_Id">入库通知单ID</param>
    /// <returns></returns>
    public static bool ValidateIsCreateInBill(string CINVCODE, string Cerpcodeline, string InAsn_Id)
    {
        string sql = "select [dbo].[Fun_GetInOrOutBill_D_Qty]('" + CINVCODE.Trim() + "','" + Cerpcodeline.Trim() + "','" + InAsn_Id.Trim() + "',0)";
        if (Cerpcodeline == "&nbsp;")
        {
            sql = "select [dbo].[Fun_GetInOrOutBill_D_Qty]('" + CINVCODE.Trim() + "',null,'" + InAsn_Id.Trim() + "',0)";
        }

        return Convert.ToDecimal(DBHelp.ExecuteScalar(sql)) == 0 ? true : false;
    }
    /// <summary>
    /// 检查是否已生成指引
    /// </summary>
    /// <param name="InAsn_Id"></param>
    /// <returns></returns>
    public static bool CheckIsExistInAssitByInAsn_Id(string InAsn_Id)
    {
        string sql = "select count(*) from InAssit ia where ia.CASNID = '" + InAsn_Id.Trim() + "'";
        return Convert.ToInt32(DBHelp.ExecuteScalar(sql)) > 0 ? true : false;
    }
    public static void UpdateStatus(string ID)
    {
        var UpdSql =
            @"update inasn  set cstatus = '3'
                    where not exists (select 1
                                        from (select a.cinvcode, a.inum, b.iquantity
                                                from (select sum(iad.iquantity) inum, iad.cinvcode
                                                        from inasn_d iad
                                                        where iad.id = '" + ID + @"' group by iad.cinvcode) a,
                                                    (select sum(ibd.iquantity) iquantity, ibd.cinvcode
                                                        from inbill_d ibd, inbill ib
                                                        where ibd.id = ib.id
                                                        and ib.cstatus >= 1
                                                        and ib.casnid = '" + ID + @"' group by ibd.cinvcode) b
                                                where a.cinvcode = b.cinvcode) c
                                        where c.inum <> c.iquantity
                                            or c.iquantity is null)
                        and exists(select 1 from inasn_d d where d.id = id)
                        and id = '" + ID + "'";
        DBHelp.ExecuteNonQuery(UpdSql.ToString());
    }
    /// <summary>
    /// 验证工单完工後是否可以退料 单头验证
    /// </summary>
    /// <param name="erpCode"></param>
    /// <returns>OK</returns>
    public string CheckWipNegativeIssueHead(string erpCode)
    {
        try
        {
            string sql = "select  [dbo].[Fun_CheckWipNegativeIssueHead]('" + erpCode + "') ";

            return DBHelp.ExecuteScalar(sql).ToString().ToUpper();
        }
        catch (Exception)
        {
            return "Error Message";
        }
    }
    /// <summary>
    /// 验证Return Receipt类型的入库通知单中填的ErpCode的是否可用
    /// </summary>
    /// <param name="erpCode">ErpCode</param>
    /// <returns>验证消息</returns>
    public static string Fun_CheckRMA_ReceiptHead(string erpCode)
    {
        string sql = @"select  [dbo].[Fun_CheckRMA_ReceiptHead]('" + erpCode.Trim() + "')";
        return DBHelp.ExecuteScalar(sql).ToString();
    }
    /// <summary>
    /// 验证工单完工後是否可以退料 单头验证
    /// </summary>
    /// <param name="erpCode"></param>
    /// <returns>OK</returns>
    public  string CheckWIP_Return_Head(string erpCode)
    {
        try
        {
            string sql = "select [dbo].[FUN_CHECKWIPRETURNBYENTYNAME]('" + erpCode + "')";

            return DBHelp.ExecuteScalar(sql).ToString().ToUpper();
        }
        catch (Exception)
        {
            return "Error Message";
        }
    }
    /// <summary>
    /// 验证传入的ERPCode是否可以入库 (工单完工入库)
    /// </summary>
    /// <param name="erpCode"></param>
    /// <returns>OK</returns>
    public string ValidateOutBillIsExist(string erpCode, string InQty)
    {
        string sql = "select [dbo].[FUN_CHECKRESULTBYENTYNAME]('" + erpCode.Trim() + "'," + InQty.Trim() + ") ";

        return DBHelp.ExecuteScalar(sql).ToString().ToUpper();
    }
    /// <summary>
    /// 检查工单是否可以做超领退
    /// </summary>
    /// <param name="erpCode"></param>
    /// <returns></returns>
    public string CheckWip_CLT_Head(string erpCode)
    {
        string sql = "select [dbo].[Fun_CheckWip_CLT_Head]('" + erpCode.Trim() + "')";

        return DBHelp.ExecuteScalar(sql).ToString();
    }


    /// <summary>
    /// 检查工单状态是否可以可用
    /// </summary>
    /// <param name="erpCode"></param>
    /// <returns></returns>
    public static string Check_WIP_STATUS(string erpCode)
    {
        string sql = "select [dbo].[Fun_Check_WIP_STATUS_IsCF]('" + erpCode.Trim() + @"')";

        return DBHelp.ExecuteScalar(sql).ToString();
    }
   
    /// <summary>
    /// 传入工单号码 把负数为0的全部写入表WIPNEGATIVEISSUE_TEMP中
    /// </summary>
    /// <param name="erpCode"></param>
    public static void Get_FUN_CHECKWIPNEGATIVEISSUE(string erpCode)
    {
        //string sql = "select FUN_CHECKWIPNEGATIVEISSUE(" + erpCode + ") from dual";

        //DBUtil.ExecuteNonQuery(sql);
        //Proc_CHECKWIPNEGATIVEISSUE proc = new Proc_CHECKWIPNEGATIVEISSUE();
        //proc.WIP_ENTITY_NAME = erpCode.Trim();
        //proc.Execute();
    }
    /// <summary>
    /// 生成单据号
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public string CreateNo(string tableName)
    {
        string sql = "select [dbo].[Fun_CreateNo]('" + tableName + "')";

        return DBHelp.ExecuteScalar(sql).ToString();
    }
    /// <summary>
    /// 根据选择的出库通知单明细生成出库单
    /// </summary>
    /// <param name="SelectIds">选择的出库通知单明细</param>
    /// <param name="outAsnId">出库通知单ID</param>
    /// <param name="outBillId">生成好的出库单ID</param>
    /// <param name="userNo">操作人</param>
    /// <param name="IsTemporary">是否暂存单据  0:非暂存 1：暂存单据</param>
    /// <param name="msg">返回的消息</param>
    /// <returns>返回是否成功</returns>
    public static bool CreateInBillByInAsn_D(Dictionary<string, string> SelectIds, string InAsnId, string InBillId, string userNo, string IsTemporary, ref string msg)
    {
        bool returnValue = false;
        //string guid = Guid.NewGuid().ToString(););
        try
        {
            string delSql = string.Format(@"delete from  Temp_Selectasnd where asn_id='{0}' and userno='{1}' ", InAsnId,userNo);
            DBHelp.ExecuteNonQuery(delSql);

            foreach (var item in SelectIds.Values)
            {
                string insertSql = "Insert into Temp_Selectasnd values('" + item.Trim() + "','" + InAsnId.Trim() + "','" + userNo.Trim() + "','" + InBillId.Trim() + "',getdate(),NEWID())";
                DBHelp.ExecuteNonQuery(insertSql);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        #region 调用存储过程
        List<string> SparaList = new List<string>();
        SparaList.Add("@P_InAsn_id:" + InAsnId.Trim());
        SparaList.Add("@P_InBill_Id:" + InBillId.Trim());
        SparaList.Add("@P_UserNo:" + userNo.Trim());
        SparaList.Add("@P_IsTemporary:" + IsTemporary);
        SparaList.Add("@P_ReturnValue:" + "");
        SparaList.Add("@INFOTEXT:" + "");
        string[] Result = DBHelp.ExecuteProc("Proc_CreateInBill", SparaList);
        if (Result.Length == 1)//调用存储过程有错误
        {
            returnValue = false;
        }
        else if (Result[0] == "0")
        {
            //生成成功 跳转到入库单页面
            returnValue = true;
        }
        else
        {
            msg = Result[1];
        }
        #endregion 
        return returnValue;
    }



    /// <summary>
    /// 生成入库通知单表体
    /// </summary>
    /// <param name="erpCode">ERP</param>
    /// <param name="InAsnId">入库通知单ID</param>
    /// <param name="errorMsg">ERROR消息</param>
    /// <returns>是否生成 成功</returns>
    public bool GetProcCreateInAsn_D(string erpCode, string InAsnId, string qty, ref string errorMsg)
    {
        DBContext context = new DBContext();
        bool returnValue = false;
        //调用生成表体的存储过程
        string funMsg = this.ValidateOutBillIsExist(erpCode, qty).ToUpper();
        if (funMsg == "OK")
        {
            //调用查询方法获取生成的表体信息
            #region 调用存储过程
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_ErpCode:" + erpCode);
            SparaList.Add("@P_InAsnId:" + InAsnId);
            SparaList.Add("@P_InQty:" + qty);
            SparaList.Add("@P_ReturnValue:" + "");
            SparaList.Add("@INFOTEXT:" + "");
            string[] Result = DBHelp.ExecuteProc("PRC_CreateInAsn_d", SparaList);
            if (Result.Length == 1)//调用存储过程有错误
            {
                returnValue = false;
            }
            else if (Result[0] == "0")
            {
                //生成成功 跳转到入库单页面
                returnValue = true;
            }
            else
            {
                //删除通知单
                IGenericRepository<INASN> con = new GenericRepository<INASN>(context);
                con.Delete(InAsnId);
                con.Save();
                errorMsg = Result[1];
            }
            #endregion  
        }
        else
        {
            errorMsg = funMsg;
        }
        return returnValue;
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
        string Sql = string.Format(@"select [dbo].[Fun_GetNo]('{0}','{1}','{2}','{3}')", pID, pType, pReserved1,
                                   pReserved2);
        return DBHelp.ExecuteScalar(Sql).ToString();
    }
    /// <summary>
    /// 检查当前料号是否在工单内
    /// </summary>
    /// <param name="AsnId">通知单ID</param>
    /// <param name="PartNumber">料号</param>
    /// <returns>返回检查消息 OK 或 其他</returns>
    public static string CheckWIP_Part(string AsnId, string PartNumber)
    {
        string sql = "select [dbo].[Fun_CheckWIP_Part]('" + AsnId.Trim() + "','" + PartNumber.Trim() + "')";

        return DBHelp.ExecuteScalar(sql).ToString().ToUpper();
    }

    /// <summary>
    /// 验证工单负发料的数量是否有效
    /// Fun_CheckWipReturnQty
    /// </summary>
    /// <param name="InAsn_Id">入库通知单ID</param>
    /// <param name="CINVCODE">料号</param>
    /// <param name="CurrentQty">本次验证数量</param>
    /// <param name="WorkType">操作类型(0:新增 , 1:修改)</param>
    /// <param name="OriginalQty">修改时的初始数量</param>
    /// <returns></returns>
    public static string CheckWipNegativeIssueQty(string InAsn_Id, string CINVCODE, string CurrentQty, string WorkType, string OriginalQty)
    {
        string sql = "select [dbo].[Fun_CheckWipNegativeIssueQty]('" + InAsn_Id.Trim() + "','" + CINVCODE.Trim() + "','" + CurrentQty.Trim() + "','" + WorkType.Trim() + "','" + OriginalQty.Trim() + "')";

        return DBHelp.ExecuteScalar(sql).ToString();
    }
    /// <summary>
    /// （出、入）通知单，明细中项次相同料号、相同子ERPcode（或空）是否存在
    /// </summary>
    /// <param name="InAsnId"></param>
    /// <param name="CINVCODE"></param>
    /// <param name="CERPCODELINE"></param>
    /// <returns></returns>
    public static bool ValidateAsn_D_IsExist(string InAsnId, string CINVCODE, string CERPCODELINE, string InAsnIds)
    {
        bool returnValue = false;
        string sql = "select top 1 ids from inAsn_d iad where iad.id='" + InAsnId.Trim() + "' and iad.cinvcode='" + CINVCODE.Trim() + "'";

        if (CERPCODELINE.Length > 0)
        {
            sql += " and iad.cerpcodeline='" + CERPCODELINE.Trim() + "'";
        }
        if (DBHelp.ExecuteScalar(sql) == null)
        {
            //没有查到 可以入
            returnValue = true;
        }
        else if (DBHelp.ExecuteScalar(sql) != null && DBHelp.ExecuteScalar(sql).ToString() == InAsnIds)
        {
            //查到了但，是原记录的修改，所以也可以入
            returnValue = true;
        }

        return returnValue;
    }

    //获取检查销货退回数量是否正确
    public static string Fun_CheckRMA_ReceiptQty(string InAsnId, string CINVCODE, string OutQty, string workType, string OriginalQty, string CERPCODELINE)
    {
        string sql = "select [dbo].[Fun_CheckRMA_ReceiptQty]('" + InAsnId.Trim() + "','" + CINVCODE.Trim() + "','" + OutQty.Trim() + "','" + workType.Trim() + "','" + OriginalQty.Trim() + "','" + CERPCODELINE.Trim() + "')";
        return DBHelp.ExecuteScalar(sql).ToString();
    }
    /// <summary>
    /// 验证工单退料的数量是否有效
    /// Fun_CheckWipReturnQty
    /// </summary>
    /// <param name="InAsn_Id">入库通知单ID</param>
    /// <param name="CINVCODE">料号</param>
    /// <param name="CurrentQty">本次验证数量</param>
    /// <param name="WorkType">操作类型(0:新增 , 1:修改)</param>
    /// <param name="OriginalQty">修改时的初始数量</param>
    /// <returns></returns>
    public static string ValidateCheckWipReturnQty(string InAsn_Id, string CINVCODE, string CurrentQty, string WorkType, string OriginalQty)
    {
        string sql = "select [dbo].[Fun_CheckWipReturnQty]('" + InAsn_Id.Trim() + "','" + CINVCODE.Trim() + "','" + CurrentQty.Trim() + "','" + WorkType.Trim() + "','" + OriginalQty.Trim() + "') ";

        return DBHelp.ExecuteScalar(sql).ToString();
    }
    /// <summary>
    /// 检查WIPCompletion工单完工入库的数量是否正确
    /// </summary>
    /// <param name="InAsn_Ids"></param>
    /// <param name="qty"></param>
    /// <returns></returns>
    public static string CheckWIPCompletionQty(string InAsn_Ids, decimal qty)
    {
        try
        {
            string sql = "select [dbo].[Fun_CheckWIPCompletionQty]('" + InAsn_Ids.Trim() + "'," + qty + ")";
            return DBHelp.ExecuteScalar(sql).ToString();
        }
        catch (Exception)
        {
            return "验证失败！";
        }
    }
    /// <summary>
    /// 【工单完工  檢查工單完工發料數 】
    /// </summary>
    /// <param name="erpCode"></param>
    /// <param name="qty"></param>
    /// <returns></returns>
    public static bool FUN_CHECKCOMPLETESTATUS(string erpCode, decimal qty, ref string msg)
    {
        bool returnValue = false;

        string sql = "select [dbo].[FUN_CHECKCOMPLETESTATUS]('" + erpCode + "'," + qty + ")";

        msg = DBHelp.ExecuteScalar(sql).ToString();
        if (msg.ToUpper() == "OK")
        {
            returnValue = true;
        }

        return returnValue;
    }

    /// <summary>
    /// 根据料号和入库通知单ID获取相应的入库数量
    /// </summary>
    /// <param name="cinvcode">料号</param>
    /// <param name="InAsn_Id">入库通知单ID</param>
    /// <returns>入库数量 0表示没有找到相应的入库数量</returns>
    public decimal GetInQtyByCinvcode(string cinvcode, string erpCode)
    {
        string sql = "select isnull( sum(iad.iquantity),0) from Inasn_d iad inner join Inasn ia  on ia.id = iad.id where  1=1 and ia.itype = 38   and iad.cinvcode='" + cinvcode + "' and ia.cerpcode='" + erpCode + "'";

        object obj = DBHelp.ExecuteScalar(sql);

        if (obj != null)
        {
            return Convert.ToDecimal(obj);
        }

        return 0;
    }
    /// <summary>
    /// 根据料号和入库通知单ID获取相应的入库数量
    /// </summary>
    /// <param name="cinvcode">料号</param>
    /// <param name="InAsn_Id">入库通知单ID</param>
    /// <returns>入库数量 0表示没有找到相应的入库数量</returns>
    public decimal GetWipnegativeissue_tempByCinvcode(string cinvcode, string erpCode)
    {
        string sql = "select abs(t.quantity_issued) from wipnegativeissue_temp t inner join base_part p  on p.cerpcode= t.inventory_item_id  where 1=1 and p.cpartnumber='" + cinvcode + "' and t.wip_entity_name ='" + erpCode + "'";

        object obj = DBHelp.ExecuteScalar(sql);

        if (obj != null)
        {
            return Convert.ToDecimal(obj);
        }

        return 0;
    }
    /// <summary>
    /// 根据料号和入库通知单ID获取相应的入库数量
    /// </summary>
    /// <param name="cinvcode">料号</param>
    /// <param name="InAsn_Id">入库通知单ID</param>
    /// <returns>入库数量 0表示没有找到相应的入库数量</returns>
    public decimal GetInQtyByCinvcodeOut(string cinvcode, string erpCode)
    {
            string sql = @"select isnull(sum(iad.iquantity),0) 
                           from Inasn_d iad inner join Inasn ia  
                                on ia.id = iad.id 
                           where  1=1 and ia.itype = 103 
                            and iad.cinvcode='" + cinvcode + @"' 
                            and ia.cerpcode='" + erpCode + "'";

        object obj = DBHelp.ExecuteScalar(sql);

        if (obj != null)
        {
            return Convert.ToDecimal(obj);
        }

        return 0;
    }
    /// <summary>
    /// 根据料号和入库通知单ID获取相应的入库数量
    /// </summary>
    /// <param name="cinvcode">料号</param>
    /// <param name="InAsn_Id">入库通知单ID</param>
    /// <returns>入库数量 0表示没有找到相应的入库数量</returns>
    public decimal GetOutQtyByCinvcode(string cinvcode, string erpCode)
    {
        try
        {
            string sql = @"select isnull(sum(obd.iquantity),0)iquantity
                            from Outbill_d obd
                                 inner join Outbill ob
                                 on obd.id = ob.id
                            where ob.otype = '203'
                              and ob.cstatus not in ('0','1')
                              and obd.cinvcode='" + cinvcode + @"' 
                              and ob.cerpcode='" + erpCode + "'";

            //object obj = DBUtil.ExecuteScalar(sql);

            return Convert.ToDecimal(sql);
        }
        catch (Exception)
        {
            return 0;
        }
    }
    /// <summary>
    /// 验证工单超领退的数量是否有效
    /// Fun_CheckWipReturnQty
    /// </summary>
    /// <param name="InAsn_Id">入库通知单ID</param>
    /// <param name="CINVCODE">料号</param>
    /// <param name="CurrentQty">本次验证数量</param>
    /// <param name="WorkType">操作类型(0:新增 , 1:修改)</param>
    /// <param name="OriginalQty">修改时的初始数量</param>
    /// <returns></returns>
    public static string CheckWip_CLT_Qty(string InAsn_Id, string CINVCODE, string CurrentQty, string WorkType, string OriginalQty)
    {
        string sql = "select [dbo].[Fun_CheckWip_CLT_Qty]('" + InAsn_Id.Trim() + "','" + CINVCODE.Trim() + "','" + CurrentQty.Trim() + "','" + WorkType.Trim() + "','" + OriginalQty.Trim() + "') from dual";

        return DBHelp.ExecuteScalar(sql).ToString();
    }
    //判断料是否可以整盘退
    public static bool IsCanSpecialReturn(string cinvcode, string AsnId)
    {
        /*string sql = @"select count(1) from outbill_d od, outbill o where o.id = od.id and o.otype = '35' and od.cinvcode = '" + cinvcode + @"' and o.cerpcode = '" + erpcode + @"'
                          and exists(select 1 from outbill_d_sn ods where ods.outbill_d_ids = od.ids and ods.line_qty > 0)
                          and not exists(select 1 from cx_wms_discrete_jobs_v4@ebs t where t.quantity_completed > 0 and t.wip_entity_name = '" + erpcode + @"' and organization_id = 90 )
                          and exists(select 1 from dual where 1 = 1
                                        and (select sum(dd.iquantity) from outbill_d dd, outbill d where d.id = dd.id and d.otype = '35' and dd.cinvcode = '" + cinvcode + @"' and d.cerpcode = '" + erpcode + @"')  = 
                                            nvl((select sum(dds.quantity) from outbill_d_sn dds where dds.outbill_d_ids in (select sd.ids from outbill_d sd, outbill s where s.id = sd.id and s.otype = '35' and sd.cinvcode = '" + cinvcode + @"' and s.cerpcode = '" + erpcode + @"')), 0))
                          and not exists(select 1 from allocate_d ad, allocate a where a.id = ad.id and ad.cinvcode = od.cinvcode and a.cerpcode = o.cerpcode and a.special = '1' and a.special_type = '0')
                          and not exists(select 1 from inbill t, inbill_d td where t.id = td.id and td.cinvcode = od.cinvcode and t.cerpcode = o.cerpcode and t.itype='43' 
                                            and (   not exists(select 1 from inbill_d_sn tds where tds.inbill_d_ids = td.ids ) 
                                                 or exists(select 1 from inbill_d_sn tds where tds.inbill_d_ids = td.ids 
                                                              and not exists (select 1 from outbill ol, outbill_d otd, outbill_d_sn ots where ol.id = otd.id and otd.ids = ots.outbill_d_ids and otd.cinvcode = '" + cinvcode + @"' and ol.cerpcode = '" + erpcode + @"' and ots.sn_code = tds.sn_code and ots.quantity = tds.quantity and ol.otype = '35' and ol.cstatus >=2))))";*/
        string sql = @"select [dbo].[fun_check_wr_bymo]('" + AsnId + "', '" + cinvcode + "', '')";
        return DBHelp.ExecuteScalar(sql).ToString() == "OK" ? true : false;
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
    
}