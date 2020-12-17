using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// OPERATOR 的摘要说明
/// </summary>
public class OPERATOR
{
	public OPERATOR()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 保存关联操作人和交付部门
    /// </summary>
    /// <param name="SelectIds">部门集合</param>
    /// <param name="Operator"></param>
    /// <param name="userNo"></param>
    /// <returns></returns>
    public static bool SetBaseOperatorDepart(Dictionary<string, string> SelectIds, string Operator, string userNo)
    {
        bool returnValue = true;
        string strSql = string.Empty;
        List<String> StrBuilder = new List<string>();
        try
        {
            string delsql = string.Format(@"delete from base_deliver_department where operatorno='{0}'", Operator);
            DBHelp.ExecuteNonQuery(delsql);
            foreach (string item in SelectIds.Values)
            {
                string itemname = "";
                string Sql = string.Format(@"select departmentname from base_department dp where dp.departmentno='{0}'", item);
                itemname = DBHelp.ExecuteScalar(Sql).ToString().Trim();
                strSql = string.Format(@"insert into base_deliver_department
                                          (id,
                                           operatorno,
                                           departmentno,
                                           departmentname,
                                           createdate,
                                           createuser,
                                           lastupdatetime,
                                           lastupdateuser)
                                        values
                                          (newid(), '{0}', '{1}', '{2}', getdate(), '{3}', getdate(), '{3}')",
                                       Operator, item, itemname, userNo);
                DBHelp.ExecuteNonQuery(strSql);
            }
        }
        catch (Exception)
        {
            return false;
        }
        return returnValue;
    }
    /// 获取已经选择的部门
    /// <summary>
    /// 获取已经选择的部门
    /// </summary>
    /// <param name="userNo">用户编码</param>
    /// <returns></returns>
    public static Dictionary<string, string> GetOperatorDepartment(string userNo)
    {
        Dictionary<string, string> operatorDepart = new Dictionary<string, string>();
        string sql =
            string.Format(@"select distinct dp.departmentno,dp.departmentname 
                                from base_deliver_department dp where dp.operatorno='{0}'", userNo);
        DataTable dt = DBHelp.ExecuteToDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            if (!operatorDepart.ContainsKey(dr["departmentno"].ToString()))
            {
                operatorDepart.Add(dr["departmentno"].ToString().Trim().ToUpper(), dr["departmentno"].ToString().Trim().ToUpper());
            }
        }

        return operatorDepart;
    }
    public static Dictionary<string, string> GetAllDepartment(string stroperator)
    {
        Dictionary<string, string> departdic = new Dictionary<string, string>();

        string sql = "select dp.departmentno from base_deliver_department dp where dp.operatorno='" + stroperator + "'";
        DataTable dt = DBHelp.ExecuteToDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            departdic.Add(dr["departmentno"].ToString(), dr["departmentno"].ToString());
        }
        return departdic;
    }
    /// <summary>
    /// 根据物料编码获取物料和货位的关联关系
    /// </summary>
    /// <param name="partNo"></param>
    /// <returns></returns>
    public static Dictionary<string, string> GetBasePartCargoSpaceByPartId(string partId)
    {
        Dictionary<string, string> cargoSpaceIds = new Dictionary<string, string>();
        string sql = @"select bpc.cpositioncode from BASE_PART_CARGOSPACE bpc 
                                        where bpc.cpartnumber=(select part.CPARTNUMBER 
                                                               from base_part part 
                                                               where part.id='" + partId + @"' 
                                                                     and rownum=1)";


        //            string sql = @"select bpc.cpositioncode from BASE_PART_CARGOSPACE bpc 
        //                            where bpc.cpartnumber='" + partId + "' and rownum=1)";  
        DataTable dt = DBHelp.ExecuteToDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            if (!cargoSpaceIds.ContainsKey(dr["cpositioncode"].ToString()))
            {
                cargoSpaceIds.Add(dr["cpositioncode"].ToString(), dr["cpositioncode"].ToString());
            }
        }

        return cargoSpaceIds;
    }
    /// <summary>
    /// 根据操作人编号获取对应的区域编号
    /// </summary>
    /// <param name="partNo"></param>
    /// <returns></returns>
    public static Dictionary<string, string> GetOperatorAreaByPartId(string partId)
    {
        Dictionary<string, string> cargoSpaceIds = new Dictionary<string, string>();
        string sql = @"select bot.AREA_ID from BASE_OPERATOR_AREA_NEW bot 
                            where bot.CACCOUNTID='" + partId + "'";
        DataTable dt = DBHelp.ExecuteToDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            if (!cargoSpaceIds.ContainsKey(dr["AREA_ID"].ToString()))
            {
                cargoSpaceIds.Add(dr["AREA_ID"].ToString(), dr["AREA_ID"].ToString());
            }
        }

        return cargoSpaceIds;
    }
    /// <summary>
    /// 根据操作人编号获取操作人对应的区域
    /// </summary>
    /// <param name="wlbm"></param>
    /// <returns></returns>
    public static DataTable GetAllOperArea(string operId)
    {
        string sql = "select A.CACCOUNTID,A.AREA_ID from BASE_OPERATOR_AREA_NEW A where A.CACCOUNTID='" + operId + "'";
        return DBHelp.ExecuteToDataTable(sql);
    }

    /// <summary>
    /// 设置操作人管理的区域
    /// </summary>
    /// <param name="SelectIds"></param>
    /// <param name="PartNo"></param>
    /// <param name="userNo"></param>
    /// <returns></returns>
    public static bool SetOperorArea(Dictionary<string, string> SelectIds, string PartId, string userNo)
    {
        try
        {

            string strSql = string.Empty;
            string guid = Guid.NewGuid().ToString();
            strSql = string.Format("delete from BASE_OPERATOR_AREA_NEW  where CACCOUNTID='{0}'", PartId);
            DBHelp.ExecuteNonQuery(strSql);
            foreach (string item in SelectIds.Values)
            {

                //选择的编号，选择指定的用户编号，登录用户账号，登录用户名称
                //CACCOUNTID 操作人 AREA_ID区域编码 DCREATETIME创建日期CCREATEOWNER创建人ID
                strSql = string.Format("insert into BASE_OPERATOR_AREA_NEW values('{0}','{1}','{2}','{3}')", PartId, item, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userNo);

                DBHelp.ExecuteNonQuery(strSql);
            }
        }
        catch (Exception)
        {
            return false;

        }

        return true;
    }


    /// <summary>
    /// 保存操作人对应所有的区域，用*表示
    /// </summary>
    /// <param name="wlId"></param>
    /// <param name="userNo"></param>
    /// <param name="isAll"></param>
    /// <returns></returns>
    public static bool SetOperAreaRel(string userid, string userNo, bool isAll)
    {
        bool returnValue = true;
        try
        {

            string strSql = string.Empty;
            //删除原有关系
            strSql = string.Format("delete from BASE_OPERATOR_AREA_NEW  where CACCOUNTID='{0}'", userid);
            DBHelp.ExecuteNonQuery(strSql);
            //查询所有区域
            Dictionary<string, string> cargoSpaceIds =GetBaseArea();
            //设置所有区域关联操作人
            foreach (string item in cargoSpaceIds.Values)
            {

                strSql = string.Format("insert into BASE_OPERATOR_AREA_NEW values('{0}','{1}','{2}','{3}')", userid, item, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userNo);
                DBHelp.ExecuteNonQuery(strSql);
            }


            return returnValue;

        }
        catch (Exception)
        {
            return false;
        }
    }
    /// <summary>
    /// 获取所有区域
    /// </summary>
    /// <param name="partNo"></param>
    /// <returns></returns>
    public static Dictionary<string, string> GetBaseArea()
    {
        Dictionary<string, string> cargoSpaceIds = new Dictionary<string, string>();
        string sql = @"select bot.ID from BASE_AREA bot ";

        DataTable dt = DBHelp.ExecuteToDataTable(sql);

      foreach(DataRow dr in dt.Rows)
      {
            if (!cargoSpaceIds.ContainsKey(dr["ID"].ToString()))
            {
                cargoSpaceIds.Add(dr["ID"].ToString(), dr["ID"].ToString());
            }
      }

        return cargoSpaceIds;
    }

    /// <summary>
    /// 取消操作人和区域的关系
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public static bool SetCancelAllOperArea(string userid)
    {
        string sql = "delete from BASE_OPERATOR_AREA_NEW  where CACCOUNTID='" + userid + "'"; ;
        return  DBHelp.ExecuteToInt(sql) > 0;
    }
    /// <summary>
    /// 根据账户ID获得名称
    /// </summary>
    /// <param name="userID">用户ID</param>
    /// <returns></returns>
    public static string GetUserNameByAccountID(string userID)
    {
        string userid = string.Empty;
        string sql = string.Format(@"SELECT dbo.Fun_GetOperatorInfo('{0}','1')",userID);
        object obj = DBHelp.ExcuteScalarSQL(sql);
        if (obj != null)
            userid = obj.ToString();
        else
            userid = "";
        return userid;
    }
    /// <summary>
    /// 根据账户名称获得用户ID
    /// </summary>
    /// <param name="userName">用户姓名</param>
    /// <returns></returns>
    public static string GetUserIDByUserName(string userName)
    {
        string username = string.Empty;
        string sql = string.Format(@"SELECT dbo.Fun_GetOperatorInfo('{0}','0')", userName);
        object obj = DBHelp.ExcuteScalarSQL(sql);
        if (obj != null)
            username = obj.ToString();
        else
            username = "";
        return username;
    }
}