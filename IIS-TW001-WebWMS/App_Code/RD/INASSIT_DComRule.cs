using System;
using System.IO;
using System.Data;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using DreamTek.ASRS.Business.SP;

    /// <summary>
    /// 描述: 对表INASSIT_D进行操作的业务规则类
    /// 作者: --陈建华
    /// 创建于: 2012-09-28 13:31:41
    /// </summary>
public class INASSIT_DComRule
{
    /// <summary>
    /// Constructor
    /// </summary>
    public INASSIT_DComRule()
    {

    }


    #region sql server

    public static bool Delete(Dictionary<string, string> ids, ref int P_ReturnValue, ref string P_INFOTEXT)
    {
        bool returnValue = true;
        try
        {
            string InAssit_d_id = string.Empty;
            StringBuilder idstr = new StringBuilder();
            foreach (string item in ids.Values)
            {
                if (InAssit_d_id.Length == 0)
                {
                    InAssit_d_id = item;
                }
                idstr.Append("'" + item + "',");
            }
            //删除指引


            string[] Result;
            var id = idstr != null && idstr.Length > 0 ? idstr.ToString().Substring(0, idstr.Length - 1) : InAssit_d_id;
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_InAssitd_id:" + id);
            SparaList.Add("@P_ReturnValue:" + 1);
            SparaList.Add("@P_INFOTEXT:" + "");

            Result = DBHelp.ExecuteProc("PROC_DEL_INASSIT_D", SparaList);
            P_INFOTEXT = Result[1];
            P_ReturnValue = Convert.ToInt32(Result[0]);

            if (P_ReturnValue == 1)
            {
                returnValue = false;
            }

        }
        catch (Exception)
        {
            returnValue = false;
        }
        return returnValue;
    }


    /// <param name="id">主表编号</param>
    /// <param name="isGetRowCount">是否是获取总记录数</param>
    /// <param name="pageIndex">页码,从0开始，任何小于0的参数值，返回所有记录数</param>
    /// <param name="pageSize">每页记录数</param>
    public static DataTable GetList(string id, string CINVCODE, bool isGetRowCount, int pageIndex, int pageSize, out int count)
    {
        count = 0;
        var sqlCount = string.Empty;

        string strSQL = string.Format(@"SELECT A.IDS,A.ID,A.CSTATUS,A.INUM,A.CPOSITIONCODE,A.CPOSITION,A.CINVBARCODE,A.CINVCODE,A.CINVNAME,A.CBATCH,A.CMEMO,
                                A.COPERATORCODE,A.COPERATOR,A.CASNID,ia.cticketcode Inasn_Code,A.IASNLINE  
                                From INASSIT_D A LEFT JOIN Inasn ia on a.casnid=ia.id
                                where 1=1  and  A.ID='{0}'", id);

        if (isGetRowCount)
        {
            sqlCount = string.Format(" SELECT count(1)  From INASSIT_D A LEFT JOIN Inasn ia on a.casnid=ia.id   where  A.ID='{0}' AND 1 = 1  ", id);
        }

        string whereSql = string.Empty;

        // whereSql += !string.IsNullOrEmpty(id) ? string.Format(" and A.ID='{0}' ", id) : string.Empty;

        whereSql += !string.IsNullOrEmpty(CINVCODE) ?
            string.Format(" and A.CINVCODE like '%{0}%' ) ", CINVCODE) : string.Empty;

        strSQL = strSQL + whereSql;
        sqlCount = sqlCount + whereSql;

        PageSpliter ps = new PageSpliter(strSQL);
        ps.OrderBySql = " order by CINVCODE";
        ps.PageSize = pageSize;
        ps.PageIndex = pageIndex;
        var psSql = ps.GetPageSQL();

        DataTable tb = SqlDBHelp.ExecuteToDataTable(psSql);

        if (isGetRowCount)
        {
            Object obj = SqlDBHelp.ExcuteScalarSQL(sqlCount);
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out count);
            }
        }

        return tb;

    }
    #endregion

    /// <summary>
    /// 生成单据号
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static string CreateNo(string tableName)
    {
        string sql = "select dbo.Fun_CreateNo('" + tableName + "') newNo";

        return SqlDBHelp.ExecuteScalar(sql);
        ;
    }


    #region oracle
    //        /// <summary>
    //        /// 新增
    //        /// </summary>
    //        /// <param name="entity">实体类(INASSIT_DEntity)</param>
    //        public static void Insert(INASSIT_DEntity entity)
    //        {
    //            PersistenceFactory factory = new PersistenceFactory();
    //            IPersistence persistence = factory.CreateInstance();
    //            persistence.Insert(entity);
    //        }

    //        /// <summary>
    //        /// 修改
    //        /// </summary>
    //        /// <param name="entity">实体类(INASSIT_DEntity)</param>
    //        public static void Update(INASSIT_DEntity entity)
    //        {
    //            PersistenceFactory factory = new PersistenceFactory();
    //            IPersistence persistence = factory.CreateInstance();
    //            persistence.Update(entity);
    //        }

    //        /// <summary>
    //        /// 删除(根据实体中定义的主键删除)
    //        /// </summary>
    //        /// <param name="entity">实体类(INASSIT_DEntity)</param>
    //        public static void Delete(INASSIT_DEntity entity)
    //        {
    //            PersistenceFactory factory = new PersistenceFactory();
    //            IPersistence persistence = factory.CreateInstance();
    //            persistence.Delete(entity);
    //        }





    ////        public static bool Delete(Dictionary<string, string> ids)
    ////        {
    ////            bool returnValue = true;
    ////            DBUtil.BeginTrans();
    ////            try
    ////            {
    ////                string InAssit_d_id = string.Empty;
    ////                StringBuilder idstr = new StringBuilder();
    ////                foreach (string item in ids.Values)
    ////                {
    ////                    if (InAssit_d_id.Length == 0)
    ////                    {
    ////                        InAssit_d_id = item;
    ////                    }
    ////                    idstr.Append("'" + item + "',");
    ////                }
    ////                //删除指引
    ////                string sql = "delete from inassit_d iad where iad.ids in (" + idstr.ToString().Substring(0, idstr.ToString().Length - 1) + ")";

    ////                DBUtil.ExecuteNonQuery(sql);
    ////                //释放货位
    ////                string sqlUpdate = @"update base_cargospace cs 
    ////                                    set cs.cstatus=0 
    ////                                    where cs.cpositioncode in (
    ////                                                                select bpc.cpositioncode 
    ////                                                                from base_part_cargospace bpc 
    ////                                                                where bpc.CPARTNUMBER in (
    ////                                                                                        select CINVCODE from InAsn_d 
    ////                                                                                        where id=(select CASNID from InAssit where id=(select iad.id from InAssit_d iad where iad.ids='BE362F295FD641A88606651796D9324D')
    ////                                                                                        )))";
    ////                DBUtil.ExecuteNonQuery(sqlUpdate);
    ////                //更新入库通知单状态为 未处理
    ////                string sqlUpdateStatus = "update inasn ia set ia.cstatus=0 where ia.id=(select CASNID from InAssit where id=(select iad.id from InAssit_d iad where iad.ids='" + InAssit_d_id + "'))";

    ////                DBUtil.ExecuteNonQuery(sqlUpdateStatus);

    ////                DBUtil.Commit();
    ////            }
    ////            catch (Exception)
    ////            {
    ////                returnValue = false;
    ////                //throw;
    ////                DBUtil.Rollback();
    ////            }
    ////            return returnValue;
    ////        }

    //        /// <summary>
    //        /// 将赋值的属性作为条件,进行删除
    //        /// </summary>
    //        /// <param name="entity">实体类(INASSIT_DEntity)</param>
    //        public static int DeleteByProperty(INASSIT_DEntity entity)
    //        {
    //            PersistenceFactory factory = new PersistenceFactory();
    //            IPersistence persistence = factory.CreateInstance();
    //            return persistence.DeleteByProperty(entity);
    //        }

    //        /// <summary>
    //        /// 批量更新
    //        /// </summary>
    //        /// <param name="conditionEntity">需要更新的记录（根据那些赋过值的属性对应的字段检索出来的记录)</param>
    //        /// <param name="dataEntity">更新后的数据（只更新那些赋过值的属性对应的字段）</param>
    //        public static int BatchUpdate(INASSIT_DEntity conditionEntity, INASSIT_DEntity dataEntity)
    //        {
    //            PersistenceFactory factory = new PersistenceFactory();
    //            IPersistence persistence = factory.CreateInstance();
    //            return persistence.BatchUpdate<INASSIT_DEntity>(conditionEntity, dataEntity);
    //        }

    #endregion

    /// <param name="casnid">入库通知单号</param>
    /// <param name="cticketcode">单据号</param>
    /// <param name="cstatus">状态[初始值]（0 未处理,1 已完成,）</param>
    /// <param name="ccreateownercode">制单人</param>
    /// <param name="dcreatetimeFrom">制单日期</param>
    /// <param name="dcreatetimeTo">制单日期</param>
    /// <param name="isGetRowCount">是否是获取总记录数</param>
    /// <param name="pageIndex">页码,从0开始，任何小于0的参数值，返回所有记录数</param>
    /// <param name="pageSize">每页记录数</param>
    public static DataTable GetList(string casnNo, string cticketcode, string cstatus, string ITYPE, string ccreateownercode,
                            string dcreatetimeFrom, string dcreatetimeTo, string CINVCODE, bool isGetRowCount, int pageIndex,
                             int pageSize, out int count, string userNo)
    {
        DataTable tb = new DataTable();
        count = 0;
        string sqlcount = string.Empty;
        string whereSql = string.Empty;
        string strSQL = @"SELECT A.ID,A.CCREATEOWNERCODE,dbo.Fun_GetOperatorInfo(A.CCREATEOWNERCODE,'1') AS CCREATEOWNERName,A.DCREATETIME,A.CTICKETCODE,A.CSTATUS,ia.cerpcode,
                                    (select s.flag_name from dbo.V_SYS_PARAMETER s where s.flag_type='ASSIT' and s.flag_id=A.CSTATUS AND s.LANGUAGEID = '" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "')CSTATUS_Name,A.CASNID,ia.cticketcode ASN_Code,vt.FLAG_NAME AS typename,ia.itype,ia.CDEFINE1,ia.CDEFINE2 from INASSIT A LEFT join inasn ia on A.CASNID=ia.ID LEFT join inType it on ia.itype=it.cerpcode LEFT JOIN dbo.V_SYS_PARAMETER vt WITH(NOLOCK) ON it.cerpcode = vt.FLAG_ID AND vt.LANGUAGEID = '" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' WHERE 1 = 1 ";
        if (isGetRowCount)
        {
            sqlcount = @"
                            SELECT count(1)  from INASSIT A left join inasn ia on A.CASNID=ia.ID left join inType it on ia.itype=it.cerpcode
                            where 1 = 1";
        }

        //入库通知单号

        if (!string.IsNullOrEmpty(casnNo))
        {

            whereSql += " and A.cticketcode like '%" + casnNo + "%'";

        }
        //单据号
        if (!string.IsNullOrEmpty(cticketcode))
        {

            whereSql += " and ia.CTICKETCODE like '%" + cticketcode + "%'";

        }
        //状态[初始值]（0 未处理,1 已完成,）
        if (!string.IsNullOrEmpty(cstatus))
        {

            whereSql += " and A.CSTATUS = '" + cstatus + "'";
        }

        //入库类型
        if (!string.IsNullOrEmpty(ITYPE))
        {

            whereSql += " and ia.ITYPE ='" + ITYPE + "'";

        }
        if (!string.IsNullOrEmpty(CINVCODE))
        {
            whereSql += " and A.id in (select iad.id from Inassit_d iad where iad.cinvcode like '%" + CINVCODE.Trim().ToUpper() + "%')";
        }
        //制单人
        if (!string.IsNullOrEmpty(ccreateownercode))
        {

            whereSql += " and dbo.Fun_GetOperatorInfo(A.CCREATEOWNERCODE,'1') like '%" + ccreateownercode + "%'";

        }

        //制单日期
        if (!string.IsNullOrEmpty(dcreatetimeFrom))
        {
            whereSql += " and CONVERT(varchar(10),A.DCREATETIME,120) >= '" + dcreatetimeFrom.Trim() + "'";

        }
        //制单日期
        if (!string.IsNullOrEmpty(dcreatetimeTo))
        {
            whereSql += " and CONVERT(varchar(10),A.DCREATETIME,120) <= '" + dcreatetimeTo.Trim() + "'";
        }

        if (!string.IsNullOrEmpty(userNo))
        {
            whereSql += " and ia.ITYPE in (select EXTEND1 from UserFunction where userno= '" + userNo + "') ";

        }


        strSQL = strSQL + whereSql;
        sqlcount = sqlcount + whereSql;

        PageSpliter ps = new PageSpliter(strSQL);
        ps.OrderBySql = " order by DCREATETIME DESC";
        ps.PageSize = pageSize;
        ps.PageIndex = pageIndex;
        var psSql = ps.GetPageSQL();

        tb = SqlDBHelp.ExecuteToDataTable(psSql);

        if (isGetRowCount)
        {
            Object obj = SqlDBHelp.ExcuteScalarSQL(sqlcount);
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out count);
            }
        }


        return tb;

    }
}