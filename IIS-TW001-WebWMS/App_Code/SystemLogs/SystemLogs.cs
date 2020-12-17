using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// SystemLogs 的摘要说明
/// </summary>
public class SystemLogs
{
    //public static DBContext context = new DBContext();
	public SystemLogs()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 判断是否有错误消息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool validateIsExistErrorMsg(string id)
    {
        DBContext context = new DBContext();
        //IGenericRepository<LOG_SYSERROR> entity = new GenericRepository<LOG_SYSERROR>(context);
        //var caseList = from p in entity.Get()
        //               where p.sovrce_no == id.Trim()
        //               select p;
        int i = context.Database.SqlQuery<int>(" select count(1) from LOG_SYSERROR  l with(nolock) where l.sovrce_no='" + id.Trim() + "' ").FirstOrDefault();
        if (i > 0)
        {
            return true;
        }
        return false;
    }
}