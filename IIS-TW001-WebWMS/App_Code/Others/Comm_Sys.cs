using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

/// <summary>
/// Comm_Sys 的摘要说明
/// </summary>
public class Comm_Sys
{
	public Comm_Sys()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 获取Crane所有的线别
    /// </summary>
    /// <param name="flag_type">类型</param>
    /// <returns></returns>
    public static DataTable GetLineList()
    {
        string Sql = @" select CRANEID from craneconfig  ";
        return DBHelp.ExecuteToDataTable(Sql);
    }

    /// <summary>
    /// 获取站点列表
    /// </summary>
    /// <param name="lineId">线</param>
    /// <returns></returns>
    public static DataTable GetSiteList(string lineId)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("SiteId");
        string Sql = string.Format(@" select sitecount from craneconfig  where craneid='{0}' ", lineId);
        var countDt = DBHelp.ExecuteToDataTable(Sql);
        if (countDt != null && countDt.Rows.Count > 0)
        {
            for (int i = 0; i < Convert.ToInt32(countDt.Rows[0][0]); i++)
            {
                DataRow row = dt.NewRow();
                row["SiteId"] = (i + 1).ToString();
                dt.Rows.Add(row);
            }
        }
        return dt;
    }

    /// <summary>
    /// 获取下拉菜单数据
    /// </summary>
    /// <param name="flag_type">类型</param>
    /// <returns></returns>
    public static DataTable GetSysType(string flag_type)
    {
        string Sql = string.Format(
            @"select flag_id,flag_name from sys_parameter where flag_type='{0}'", flag_type);
        return DBHelp.ExecuteToDataTable(Sql);
    }

    /// <summary>
    /// 获取配置代码名称
    /// </summary>
    /// <param name="flag_type">类型</param>
    /// <param name="flag_id"></param>
    /// <returns></returns>
    public static string GetSysTypeName(string flag_type, string flag_id)
    {
        string Sql = string.Format(
            @"select flag_name from sys_parameter where flag_type='{0}' and flag_id = '{1}'", flag_type, flag_id);
        return DBHelp.ExecuteScalar(Sql);
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
            string Sql = string.Format(@" select count(*) from USERFUNCTION  
                                              where funcno = '{0}' and userno ='{1}'", strpoint, struseno);
            return Convert.ToInt32(DBHelp.ExecuteScalar(Sql).ToString()) > 0 ? true : false;
        }
        catch (Exception)
        {
            point = false;
        }
        return point;
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
}