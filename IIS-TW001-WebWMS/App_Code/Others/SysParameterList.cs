using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
using DreamTek.WMS.DAL.Model.Base;
using DreamTek.WMS.DAL.Common;

/// <summary>
/// SysParameterList 的摘要说明
/// </summary>
public class SysParameterList
{
  //public static   DBContext context = new DBContext();
	public SysParameterList()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static  IList GetListExp(string FLAG_ID, string FLAG_NAME, string FLAG_TYPE, bool isGetRowCount, int pageIndex, int pageSize)
    {
        DBContext context = new DBContext();
        IGenericRepository<SYS_PARAMETER> con = new GenericRepository<SYS_PARAMETER>(context);
        var caseList = from p in con.Get()
                       where 1 == 1
                       select p;

        if (!string.IsNullOrEmpty(FLAG_ID))
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.flag_id) && x.flag_id.Equals(FLAG_ID.Trim()));
        }
        //编号(可用来打印条码)
        if (!string.IsNullOrEmpty(FLAG_NAME))
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.flag_name) && x.flag_name.Equals(FLAG_NAME.Trim()));
        }
        //类型
        if (!string.IsNullOrEmpty(FLAG_TYPE))
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.flag_type) && x.flag_type.Equals(FLAG_TYPE.Trim()));
        }       
        return caseList.OrderBy(" sortid asc ").ToList();
      


    }
    /// <summary>
    /// 获取sysPARAMETER的数据集
    /// </summary>
    /// <param name="FLAG_ID"></param>
    /// <param name="FLAG_NAME"></param>
    /// <param name="FLAG_TYPE"></param>
    /// <param name="isGetRowCount"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static IList GetList(string FLAG_ID, string FLAG_NAME, string FLAG_TYPE, bool isGetRowCount, int pageIndex, int pageSize)
    {
        //string sql = string.Format(@"select * from V_SYS_PARAMETER v where 1=1");
        //if (!string.IsNullOrEmpty(FLAG_ID))
        //{
        //    sql += "AND v.FLAG_ID = '" + FLAG_ID + "'";
        //}
        ////编号(可用来打印条码)
        //if (!string.IsNullOrEmpty(FLAG_NAME))
        //{
        //    sql += "AND v.FLAG_NAME = '" + FLAG_NAME + "'";
        //}
        ////类型
        //if (!string.IsNullOrEmpty(FLAG_TYPE))
        //{
        //    sql += "AND v.FLAG_TYPE = '" + FLAG_TYPE + "'";
        //}
        ////语言
        //string systemLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        //if (!string.IsNullOrEmpty(systemLanguage))
        //{
        //    sql += "AND v.LANGUAGEID = '" + systemLanguage + "'";
        //}
        //sql += "ORDER BY v.FLAG_TYPE,v.SORTID";

        //IList list = DapperHelper.Query<V_SYS_PARAMETER>(sql).ToList();    

        return new SysParameterList().GetParametersByFlagType(FLAG_TYPE);
        //return list;
    }
    /// <summary>
    /// 根据语言获得所有的V_SYS_PARAMETER的数据
    /// </summary>
    /// <param name="systemLanguage"></param>
    /// <returns></returns>
    public IEnumerable<V_SYS_PARAMETER> GetPARAMETERList(string systemLanguage)
    {
        string sql = string.Format(@"select * from V_SYS_PARAMETER v where 1=1");      
        //语言       
        if (!string.IsNullOrEmpty(systemLanguage))
        {
            sql += "AND UPPER(v.LANGUAGEID) = '" + systemLanguage.ToUpper() + "'";
        }
        sql += "ORDER BY v.FLAG_TYPE,v.SORTID";

        IEnumerable<V_SYS_PARAMETER> list = DapperHelper.Query<V_SYS_PARAMETER>(sql).ToList();
        return list;
    }
    /// <summary>
    /// 根据表名获取对应该状态值
    /// </summary>
    /// <param name="flag_type"></param>
    /// <returns></returns>
    public DataTable LoadStatusByFlag_type(string flag_type)
    {
        string sql = "select s.flag_id,s.flag_name from sys_parameter s where s.flag_type='" + flag_type.Trim() + "' ORDER BY s.flag_id asc";

        return DBHelp.ExecuteToDataTable(sql);
    }
    /// <summary>
    /// 获取入库通知单类型-查询
    /// </summary>
    /// <param name="IsSearch"></param>
    /// <returns></returns>
    public DataTable GetInType(bool IsSearch)
    {
        string sql = string.Format(@"select distinct f.typename as FUNCNAME,f.cerpcode as EXTEND1 
                       from intype f inner join ( select FUNCNAME,EXTEND1 from dbo.UserFunction where modno ='I' and userno='{0}') t  on t.EXTEND1 = f.cerpcode 
                       where 1=1 ", WmsWebUserInfo.GetCurrentUser().UserNo);
        if (!IsSearch)
        {
            //增删改
            sql += " and f.Is_Query !='1' and f.cerpcode != '101' and (DISABLE_DATE is null or DISABLE_DATE >= sysdate)";
        }
        return DBHelp.ExecuteToDataTable(sql);
    }
    /// <summary>
    /// 获取系统类型
    /// </summary>
    /// <param name="FLAG_TYPE"></param>
    /// <returns></returns>
    public DataTable GetSys_ParameterByFLAG_TYPE(string FLAG_TYPE)
    {
        string strSQL = "select FLAG_ID,FLAG_NAME,FLAG_TYPE,SORTID from sys_parameter where flag_type='" + FLAG_TYPE + "' order by sortid";
        DataTable dtBASE_CARGOSPACE = DBHelp.ExecuteToDataTable(strSQL);
        return dtBASE_CARGOSPACE;
    }

    /// <summary>
    /// 获取线别信息
    /// </summary>
    /// <returns></returns>
    public DataTable GetCrane()
    {
        string strSQL = string.Format(@"  SELECT ID,cranename,CRANEID FROM BASE_CRANECONFIG a 
                                        where a.CRANEID in (select CRANEID from dbo.BASE_CRANECONFIG where PLCType='LK')
									   order by CRANEID asc
									    ");
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        return dt;
    }

//    /// <summary>
//    /// 根据线别获取站点
//    /// </summary>
//    /// <param name="wireid"></param>
//    /// <returns></returns>
//    public DataTable GetSite(string wireid,string itype)
//    {
//            string strSQL = string.Format(@"SELECT detail.SITENAME,detail.craneid +'-'+ detail.siteid as siteid FROM BASE_CRANECONFIG_DETIAL detail with(nolock) 
//                                        WHERE detail.sitetype in('1','3') 
//                                          and detail.CRANEID = '{0}'
//                                          and exists (select 1 from BASE_CRANECONFIG_TRADETYPE bct with(nolock) where bct.id = detail.id  and bct.CSTATUS ='0'  and bct.INOROUTCODE = 'I' and bct.cerpcode = '{1}')
//                                        ORDER BY SITEID ASC", wireid, itype);
       
//        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
//        return dt;
//    }

    public bool CheckTradeType(string wireid,string siteID ,string itype)
    {
        string strSQL = string.Format(@"SELECT COUNT(1) FROM BASE_CRANECONFIG_DETIAL detail with(nolock) 
                                        WHERE detail.sitetype in('1','3') 
                                          and detail.CRANEID = '{0}'
                                          AND detail.siteid='{1}'
                                          and exists (select 1 from BASE_CRANECONFIG_TRADETYPE bct with(nolock) 
                                            where bct.id = detail.id  and bct.CSTATUS ='0'  and bct.INOROUTCODE = 'I' and bct.cerpcode = '{2}')
                                             ", wireid,siteID ,itype);

        var val = DBHelp.ExecuteScalar(strSQL);        
        return val=="1"?true:false;

    }




    /// <summary>
    /// 根据线别获取站点
    /// </summary>
    /// <param name="wireid"></param>
    /// <returns></returns>
    public DataTable GetSite(string wireid, string itype)
    {
        string strSQL = string.Format(@"SELECT SITENAME,craneid+'-'+siteid as  siteid FROM BASE_CRANECONFIG_DETIAL WHERE sitetype in('1','3') 
                                        and CRANEID = '{0}' ORDER BY SITEID ASC", wireid);
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        return dt;
    }

    /// <summary>
    /// 根据类型，id获取名称
    /// </summary>
    /// <param name="flag_type"></param>
    /// <param name="flag_id"></param>
    /// <returns></returns>
    public static string GetTypeName(string flag_type, string flag_id)
    {
        string strSQL = string.Format(@"SELECT  flag_name
                                        FROM    dbo.SYS_PARAMETER
                                        WHERE   flag_type = '{0}'
                                                AND flag_id = '{1}'", flag_type, flag_id);
        return DBHelp.ExecuteScalar(strSQL);
    }

    /// <summary>
    /// 获取所有代码组
    /// </summary>
    /// <returns></returns>
    private List<V_SYS_PARAMETER> GetAllParameterList() {
        //语言
        string sysLang = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        string key = SystemCache.SYSParamter + "_" + sysLang;
        var cache = CacheHelper.GetCache(key);
        if (cache == null)
        {
            var parameterList = this.GetPARAMETERList(sysLang);
            if (parameterList != null)
            {
                var enumerable = parameterList.ToList();
                CacheHelper.SetCache(key, enumerable, 36000);//添加缓存10个小时
                return enumerable;
            }
            else {
                return new List<V_SYS_PARAMETER>();
            }
        }
        else {
            return (List<V_SYS_PARAMETER>)cache;
        }
    }

    /// <summary>
    /// 根据flagType 获取所有子项
    /// </summary>
    /// <param name="flagType"></param>
    /// <returns></returns>
    public List<V_SYS_PARAMETER> GetParametersByFlagType(string flagType)
    {
        //var parameterList = this.GetAllParameterList();
        //if (parameterList != null && parameterList.Any())
        //{
        //    return parameterList.Where(x => x.FLAG_TYPE == flagType).OrderBy(x => x.SORTID).ToList();
        //}
        //else {//以防出现问题没有数据时，再去取一遍
        string sysLang = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        var paraList = this.GetPARAMETERList(sysLang);
        if (paraList != null && paraList.Any())
        {
            return paraList.Where(x => x.FLAG_TYPE == flagType).OrderBy(x => x.SORTID).ToList();
        }
        else
        {
            return null;
        }
        //}
    }

    /// <summary>
    /// 根据flagType 获取所有子项
    /// </summary>
    /// <param name="flagType"></param>
    /// <returns></returns>
    public V_SYS_PARAMETER GetParametersByFlagType(string flagType,string flagId)
    {
        var parameterList = this.GetAllParameterList();
        if (parameterList != null && parameterList.Any())
        {
            return parameterList.Where(x => x.FLAG_TYPE == flagType && x.FLAG_ID == flagId).FirstOrDefault();
        }
        else
        {//以防出现问题没有数据时，再去取一遍
            string sysLang = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            var paraList = this.GetPARAMETERList(sysLang);
            if (paraList != null && paraList.Any())
            {
                return paraList.Where(x => x.FLAG_TYPE == flagType && x.FLAG_ID == flagId).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 根据flagType 以及flagid 获得当前系统语言的名称
    /// </summary>
    /// <param name="flagType"></param>
    /// <returns></returns>
    public string GetParametersNameByFlagType(string flagType, string flagId)
    {
        var parameterList = this.GetAllParameterList();
        if (parameterList != null && parameterList.Any())
        {
            return parameterList.Where(x => x.FLAG_TYPE == flagType && x.FLAG_ID == flagId).FirstOrDefault().FLAG_NAME;
        }
        else
        {//以防出现问题没有数据时，再去取一遍
            string sysLang = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            var paraList = this.GetPARAMETERList(sysLang);
            if (paraList != null && paraList.Any())
            {
                return paraList.Where(x => x.FLAG_TYPE == flagType && x.FLAG_ID == flagId).FirstOrDefault().FLAG_NAME;
            }
            else
            {
                return "";
            }
        }
    }

}