using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;

/// <summary>
/// Base_Cargospace 的摘要说明
/// </summary>
public class Base_Cargospace
{
    //private static DBContext context = new DBContext();
	public Base_Cargospace()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// 判断储位状态
    /// <summary>
    /// 判断储位状态
    /// </summary>
    /// <param name="cpcode">储位编码</param>
    /// <param name="cstatus">状态</param>
    /// <returns></returns>
    public bool CheckCpositionStatus(string cpcode, string cstatus)
    {
        bool status = false;
        try
        {
            string Sql = string.Format(@"select count(*) from base_cargospace bc where bc.cpositioncode ='{0}' and bc.cstatus='{1}'", cpcode, cstatus);
            return Convert.ToInt32(DBHelp.ExecuteScalar(Sql).ToString()) > 0 ? true : false;
        }
        catch (Exception)
        {
            status = false;
        }
        return status;
    }
    /// <summary>
    /// 验证选择的储位是否是保税还是非保税
    /// </summary>
    /// <param name="P_BONDED">保税还是非保税(Y/N)</param>
    /// <param name="CPOSITIONCODE">储位编码</param>
    /// <returns>返回0验证失败,返回值 大于 1验证成功</returns>
    public static bool CheckInBill_CPOSITIONCODE(string P_BONDED, string CPOSITIONCODE)
    {
        string sql = @"SELECT count(*) 
                            from BASE_CARGOSPACE A
                            left join base_warehouse bwh on a.warehouseid = bwh.id
                            where A.CPOSITIONCODE = '" + CPOSITIONCODE.Trim() + @"'
                            and bwh.BONDED = '" + P_BONDED.Trim() + "'";

        return Convert.ToInt32(DBHelp.ExecuteScalar(sql)) == 0 ? false : true;
    }
    /// <summary>
    /// 根据储位编码获取储位名称
    /// </summary>
    /// <param name="cpositionCode"></param>
    /// <returns></returns>
    public static string GetCpositionByCpositionCode(string cpositionCode)
    {
        try
        {
            string sql = "select bcs.cposition from base_cargospace bcs where bcs.cpositioncode='" + cpositionCode.Trim().ToUpper() + "'";

            return DBHelp.ExecuteScalar(sql).ToString();
        }
        catch (Exception)
        {
            return "";
        }
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
        //            string strSQL = @"  select cpositioncode , cposition, IQTY
        //                                from 
        //                                (
        //                                  select bc.cpositioncode , bc.cposition ,
        //                                         nvl((select sc.iqty from stock_current sc 
        //                                              where sc.cpositioncode = bc.cpositioncode
        //                                              and sc.cinvcode = '" + CINVCODE.Trim() + @"'),0)IQTY
        //                                  from base_cargospace bc
        //                                  order by bc.cpositioncode
        //                                )newbase_cargospace
        //                                where 1 = 1 and IQTY > 0 ";
        //            string strSQL = @"  select bc.cpositioncode , bc.cposition ,
        //                                        nvl((select sc.iqty from stock_current sc 
        //                                            where sc.cpositioncode = bc.cpositioncode
        //                                            and sc.cinvcode = '" + CINVCODE.Trim() + @"'),0)IQTY
        //                                from base_cargospace bc
        //                                where 1=1 ";//order by bc.cpositioncode
        //            //料号
        //            if (CPOSITIONCODE.IsNullOrEmpty() == false)
        //            {
        //                if (isRight)
        //                {
        //                    strSQL += " and cpositioncode like '" + CPOSITIONCODE.ToUpper() + "%'";
        //                }
        //                else
        //                {
        //                    strSQL += " and cpositioncode like '%" + CPOSITIONCODE.ToUpper() + "%'";
        //                }
        //            }
        //2015-11-30  正式备份注释
        //            string strSQL = @"  select bc.cpositioncode , bc.cposition, nvl((select sc.iqty from stock_current sc where sc.cpositioncode = bc.cpositioncode
        //                                            and sc.cinvcode = '" + CINVCODE.Trim() + @"'),0)IQTY
        //  from BASE_PART_CARGOSPACE P inner JOIN base_cargospace bc ON P.cpositioncode=bc.cpositioncode
        //  AND CPARTNUMBER in '" + CINVCODE.Trim() + @"'";
        //2015-11-30 添加数量Sum，查询符合入库数量的库，并由小到大排序
        string strSQL =string.Format( @"  select  top {0} bc.cpositioncode , bc.cposition, isnull((select (bc.imaxcapacity- isnull((sum(sc.iqty)),0)) as qit from stock_current sc where sc.cpositioncode = bc.cpositioncode
                                            ),0)IQTY
  from BASE_PART_CARGOSPACE P inner JOIN base_cargospace bc ON P.cpositioncode=bc.cpositioncode
  AND CPARTNUMBER = '" + CINVCODE.Trim() + @"' and isnull((select (bc.imaxcapacity- isnull((sum(sc.iqty)),0)) as qit from stock_current sc where sc.cpositioncode = bc.cpositioncode
                                            ),0)>'" + sum + "'",size);
        strSQL += "  order by IQTY";
        return DBHelp.ExecuteToDataTable(strSQL);
    }
    //获取待修改的数据
    public static List<V_Stock_Base_Cargospace> GetChangeList(string ccargoid, string ccargoname, string CWARENAME, string CWAREID)
    {
        DBContext context = new DBContext();
        IGenericRepository<V_Stock_Base_Cargospace> entity = new GenericRepository<V_Stock_Base_Cargospace>(context);
        var caseList = from p in entity.Get()
                       where 1 == 1
                       select p;
        if (ccargoid != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(ccargoid.Trim()));
        if (ccargoname != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cposition) && x.cposition.Contains(ccargoname.Trim()));
        if (CWARENAME != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwarename) && x.cwarename.Contains(CWARENAME.Trim()));
        if (CWAREID != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwareid) && x.cwareid.Contains(CWAREID.Trim()));
        return caseList.ToList();
    }
    /// <summary>
    /// 从库存表中获取指定料号所在的储位和库存数量
    /// </summary>
    /// <param name="CINVCODE">料号</param>
    /// <param name="CPOSITIONCODE">储位</param>
    /// <param name="isRight">是否右边模糊：false全模糊</param>
    /// <param name="size">显示数量</param>
    /// <returns></returns>
    /// 2015-12-02  出库根据数量选择合适的储位  [ASRS专用]
    /// //2015-12-02  线边仓不选
    public DataTable GetCargoSpaceListByStock(string CINVCODE, string CPOSITIONCODE, bool isRight, int size, string sum)
    {
       
        string sql =string.Format( @"select top {0} *
                            from
                            (
                                select t1.cpositioncode ,t1.cposition , sum (t1.qty) as IQTY , min(t1.datecode) as datecode ,t1.sncode 
                                from 
                                (select sc.cpositioncode ,sc.cposition , scd.qty ,scd.sncode,scd.datecode 
                                from stock_current sc left join Stock_Current_SN scd on sc.id =scd.stock_ID 
                                where scd.stocktype ='1' and sc.cinvcode =  '" + CINVCODE.Trim() + @"'
                                and  sc.cpositioncode not in(select t.cpositioncode from BASE_LINE_INFO t) ) t1 
                                group by  t1.cpositioncode ,t1.cposition , t1.qty ,t1.sncode , t1.datecode                               
                          ) newS   where 1=1 ",size);

        if (isRight)
        {
            sql += " and newS.cpositioncode like '" + CPOSITIONCODE.Trim().ToUpper() + "%'";
        }
        else
        {
            sql += " and newS.cpositioncode like '%" + CPOSITIONCODE.Trim().ToUpper() + "%'";
        }
        sql += @" order by newS.datecode ";
        return DBHelp.ExecuteToDataTable(sql);
    }
    /// <summary>
    /// 更新库存冻结/释放状态
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="positoncode"></param>
    /// <param name="positionname"></param>
    /// <param name="cwarename"></param>
    /// <param name="cwareid"></param>
    public void UpdateBASE_CARGOSPACE_Status(string flag, string positoncode, string positionname, string cwarename, string cwareid)
    {
        string strSQL = string.Format(@"EXEC Proc_BASE_CARGOSPACE_StatusUpdate '{0}','{1}','{2}','{3}','{4}'", flag, positoncode, positionname, cwarename, cwareid);
        SqlDBHelp.ExecuteNonQuery(strSQL);
    }

    /// <summary>
    /// 验证选择的储位是否与料号关联
    /// </summary>
    /// <param name="cpositioncode">储位编码</param>
    /// <param name="cinvcode">料号</param>
    /// <returns>返回0验证失败,返回值 大于 1验证成功</returns>
    public static bool CheckCinvcodeWithCpositioncode(string cpositioncode, string cinvcode)
    {
        string sql = string.Format(@"SELECT count(1)
                                      FROM BASE_CARGOSPACE A WITH(NOLOCK)
                                      WHERE A.CPOSITIONCODE = '{0}'
                                        AND ( EXISTS (SELECT 1 FROM BASE_PART_AREA bpa WITH(NOLOCK) WHERE bpa.area_id = A.cdefine1 AND bpa.partnumber = '{1}')
                                           OR EXISTS (SELECT 1 FROM BASE_PART_CARGOSPACE bpc WITH(NOLOCK) WHERE bpc.cpartnumber = '{1}' and (bpc.cpositioncode = A.CPOSITIONCODE or bpc.cpositioncode = '*'))
	                                        )", cpositioncode, cinvcode);
        return Convert.ToInt32(DBHelp.ExecuteScalar(sql)) == 0 ? false : true;
    }


}