using DreamTek.WMS.DAL.Common;
using DreamTek.WMS.DAL.Model;
using DreamTek.WMS.DAL.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TW_BASE_PART_Repo
/// </summary>
public class TW_BASE_PART_Repo
{

    public static Decimal IMPOSIBLE_DROP_DOWN_VAL = 98765.12m;// m 是指DECIMAL, 不然是 double
    public Base_Resources GetById(string id)
    {
        string sql = string.Format(" select * from dbo.Base_Resources where id='{0}' ", id);
        Base_Resources bo = DapperHelper.Query<Base_Resources>(sql).FirstOrDefault();
        return bo;
    }

    public IEnumerable<TW_BASE_PART> Query(TW_BASE_PART whereObject, int pageSize, int pageIndex, out int total)
    {
        total = 0;
        string sql = @" SELECT *
                            FROM dbo.TW_BASE_PART
                            WHERE 1=1 
                          ";
        //string sqlCount = @" SELECT count(1) FROM TW_BASE_PART  WHERE 1=1";

        if (!string.IsNullOrEmpty(whereObject.PART))
        {
            sql = sql + " and PART LIKE '%" + whereObject.PART + "%'";

        }
        if (!string.IsNullOrEmpty(whereObject.RANK_FINAL))
        {
            sql = sql + " and RANK_FINAL ='" + whereObject.RANK_FINAL + "'";

        }
        if (!string.IsNullOrEmpty(whereObject.cpartname))
        {
            sql = sql + " and cpartname LIKE '%" + whereObject.cpartname + "%'";

        }
        if (!string.IsNullOrEmpty(whereObject.calias))
        {
            sql = sql + " and calias LIKE '%" + whereObject.calias + "%'";

        }
        if (!string.IsNullOrEmpty(whereObject.cerpcode))
        {
            sql = sql + " and cerpcode LIKE '%" + whereObject.cerpcode + "%'";

        }
        if (!(whereObject.bonded == IMPOSIBLE_DROP_DOWN_VAL))// NOTE: by Mark, 09/15 應該設一個  constant 
        {
            sql = sql + " and bonded ='" + whereObject.bonded + "'";

        }

        if (!string.IsNullOrEmpty(whereObject.ctype))
        {
            sql = sql + " and ctype ='" + whereObject.ctype + "'";

        }

        if (!(whereObject.mtype == IMPOSIBLE_DROP_DOWN_VAL))// NOTE: by Mark, 09/15 應該設一個  constant 
        {
            sql = sql + " and mtype ='" + whereObject.mtype + "'";

        }
        if (!(whereObject.ineedwarn == IMPOSIBLE_DROP_DOWN_VAL))// NOTE: by Mark, 09/15 應該設一個  constant 
        {
            sql = sql + " and ineedwarn ='" + whereObject.ineedwarn + "'";

        }

        //NOTE by Mark, 09/15, TODO
        //是否設置儲位：
        //if (!string.IsNullOrEmpty(whereObject.cdefaultcargo))
        //{
        //    sql = sql + " and cdefaultcargo ='" + whereObject.cdefaultcargo + "'";


        //狀態

        if (!string.IsNullOrEmpty(whereObject.cstatus))
        {
            sql = sql + " and cstatus ='" + whereObject.cstatus + "'";

        }


        //是否免檢：TODO
        if (!(whereObject.ineedcheck == IMPOSIBLE_DROP_DOWN_VAL))
        {
            //  sql = sql + " and ineedcheck ='" + whereObject.ineedcheck + "'";

        }

        //創建人：
        if (!string.IsNullOrEmpty(whereObject.createowner))
        {
            sql = sql + " and createowner LIKE '%" + whereObject.createowner + "%'";

        }

        //創建時間：
        //   caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtCreateTimeFrom.Text.Trim(), x.createtime) >= 0);
        //var v1 = whereObject.createtimeFROM;
        //if (whereObject.createtimeFROM != null)
        //{
        //    sql = sql + " and DATEDIFF(dd, '" + whereObject.createtimeFROM + "', createtime ) > 0";

        //}
        if (!string.IsNullOrEmpty(whereObject.TXTcreatetime))
        {
            sql = sql + " and DATEDIFF(dd, '" + whereObject.TXTcreatetime + "', createtime ) > 0";

        }

        //}
        ////if (!string.IsNullOrEmpty(whereObject.LanguageId))
        ////{
        ////    sql = sql + " and LanguageId=@LanguageId ";
        ////    sqlCount = sqlCount + " and LanguageId=@LanguageId ";
        ////}

        //if (!string.IsNullOrEmpty(whereObject.SourceKey))
        //{
        //    whereObject.SourceKey = "%" + whereObject.SourceKey + "%";
        //    sql = sql + " and SourceKey like @SourceKey ";
        //    sqlCount = sqlCount + " and SourceKey like @SourceKey ";
        //}
        //if (!string.IsNullOrEmpty(whereObject.SourceValue))
        //{
        //    whereObject.SourceValue = "%" + whereObject.SourceValue + "%";
        //    sql = sql + " and SourceValue like @SourceValue ";
        //    sqlCount = sqlCount + " and SourceValue like @SourceValue ";
        //}
        //if (!string.IsNullOrEmpty(whereObject.CStatus))
        //{
        //    sql = sql + " and CStatus=@CStatus ";
        //    sqlCount = sqlCount + " and CStatus=@CStatus ";
        //}

        //sql = sql + " ORDER BY br.SourceKey,br.LanguageId ";
        string sqlCount = @" SELECT count(1) FROM (" + sql + " ) T1";
        sql = sql + " ORDER BY PART";

        IEnumerable<TW_BASE_PART> list = DapperHelper.QuerySpliter<TW_BASE_PART>(sql, pageSize, pageIndex, whereObject);
        total = DapperHelper.GetTotal(sqlCount, whereObject);

        return list;
    }

    public int Update(Base_Resources bo)
    {
        string sql = @" update dbo.Base_Resources  
                            set SourceValue=@SourceValue,CStatus=@CStatus,LanguageId=@LanguageId,
                                ModuleId=@ModuleId,Remark=@Remark,
                                ModifyUser=@ModifyUser,Modifytime=@Modifytime
                            where id=@id ";
        int count = DapperHelper.Execute(sql, bo);
        return count;
    }

    public IEnumerable<Base_ResourcesQuery> QueryDetail(Base_ResourcesQuery whereObject, int pageSize, int pageIndex, out int total)
    {
        total = 0;
        string sql = @" SELECT br.*,p.flag_name AS 'LanguageName' FROM dbo.Base_Resources br
                            left JOIN SYS_PARAMETER p ON br.LanguageId = p.flag_id
                            WHERE p.flag_type='Language'   
                          ";
        string sqlCount = @" SELECT count(1) FROM dbo.Base_Resources br
                            left JOIN SYS_PARAMETER p ON br.LanguageId = p.flag_id
                            WHERE p.flag_type='Language'   
                          ";
        if (!string.IsNullOrEmpty(whereObject.LanguageId))
        {
            sql = sql + " and LanguageId!=@LanguageId ";
            sqlCount = sqlCount + " and LanguageId!=@LanguageId ";
        }

        if (!string.IsNullOrEmpty(whereObject.SourceKey))
        {
            sql = sql + " and SourceKey = @SourceKey ";
            sqlCount = sqlCount + " and SourceKey = @SourceKey ";
        }

        sql = sql + " ORDER BY br.SourceKey,br.LanguageId ";

        IEnumerable<Base_ResourcesQuery> list = DapperHelper.QuerySpliter<Base_ResourcesQuery>(sql, pageSize, pageIndex, whereObject);
        total = DapperHelper.GetTotal(sqlCount, whereObject);

        return list;
    }

    /// <summary>
    /// 根据wmscode及类型获取erpcode
    /// </summary>
    /// <param name="type"></param>
    /// <param name="wmscode"></param>
    /// <returns></returns>
    public TypeMapping GetMappingByWMSCode(string type, string wmscode)
    {
        string sql = string.Format(" select * from dbo.BASE_TypeMapping where type = '{0}' and WMS_TypeCode = '{1}' ", type, wmscode);
        TypeMapping bo = DapperHelper.Query<TypeMapping>(sql).FirstOrDefault();
        return bo;
    }

    /// <summary>
    /// 新增日志
    /// </summary>
    /// <param name="bo"></param>
    /// <returns></returns>
    public int InsertInterFaceLog(Base_InterFaceLog bo)
    {
        string sql = string.Format(@" insert into dbo.BASE_INTERFACELOG (
                                                      ID,
                                                      TICKETCODE,
                                                      ERPCODE,
                                                      TYPEID,
                                                      METHODNAME,
                                                      ERRORMSG,
                                                      CREATEDATE,
                                                      CREATEOWNER,
                                                      REMARK,
                                                      BO,
                                                      TYPENAME
                                                  ) 
                                                  values (
                                                      @ID,
                                                      @TICKETCODE,
                                                      @ERPCODE,
                                                      @TYPEID,
                                                      @METHODNAME,
                                                      @ERRORMSG,
                                                      @CREATEDATE,
                                                      @CREATEOWNER,
                                                      @REMARK,
                                                      @BO,
                                                      @TYPENAME
                                                  )  ");
        return DapperHelper.Execute(sql, bo);
    }



}


