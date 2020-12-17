using DreamTek.ASRS.Business.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// RPDataSource 的摘要说明
/// </summary>
public class RPDataSource
{
	public RPDataSource()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 获取物理盘点差异数
    /// </summary>
    /// <param name="pname"></param>
    /// <returns></returns>
    public DataTable GetCheckDiffHeader(string pname)
    {
        DataTable tb = null;
        try
        {
            string Sql = @" select A.znum,A.chuyzs,A.fuyzs,A.chufuyzs,

                        case when sign(A.chuyzs/A.znum*100-1) = -1 then '0'+convert(nvarchar(50),round(A.chuyzs/A.znum*100,2))+'%' else convert(nvarchar(50),round(A.chuyzs/A.znum*100,2))+'%' end as  chuyzl,


                        case when sign(A.fuyzs/A.znum*100-1) = -1 then '0'+convert(nvarchar(50),round(A.fuyzs/A.znum*100,2))+'%' else convert(nvarchar(50),round(A.fuyzs/A.znum*100,2))+'%' end as  fuyzl,


                        case when sign(A.chufuyzs/A.znum*100-1)=-1 then '0'+convert(nvarchar(50),round(A.chufuyzs/A.znum*100,2))+'%' else convert(nvarchar(50),round(A.chufuyzs/A.znum*100,2))+'%' end as chufuyzl

                        from (

                        select  
                        count(1) znum,--快照数

                        sum( case when isnull(di.onediff,0) = 0 then 1 else 0 end  ) as chuyzs,
                        --sum(decode(di.onediff, 0, 1, 0)) chuyzs,

                        sum(case when isnull(di.twodiff,0) = 0 then 1 else 0 end ) as fuyzs,
                        --sum(decode(di.twodiff, 0, 1, 0)) fuyzs,

                        sum( case when isnull(di.onenum,0)=0 then 0 when isnull(di.onenum,0) = isnull(di.twonum,0) then 1 else 0 end) as chufuyzs
                        --sum(decode(di.onenum,null,0,di.twonum,1,0)) chufuyzs

                        from view_stock_checkdiff di



                            ";
            if (pname != "")
            {
                Sql += " where di.ccheckname like '%" + pname + "%'";
            }
            Sql += " ) A";
            tb = DBHelp.ExecuteToDataTable(Sql);
        }
        catch (Exception)
        {

            return tb;
        }
        return tb;
    }

    /// 获取物理盘点单差异
    /// <summary>
    /// 获取物理盘点单差异
    /// </summary>
    /// <param name="pcode">盘点单号</param>
    /// <param name="pname">盘点单名称</param>
    /// <param name="ptype">差异类型</param>
    /// <param name="wareno">仓库编码</param>
    /// <param name="poscode">储位</param>
    /// <param name="pcinv">料号</param>
    /// <param name="datefrom">开始时间</param>
    /// <param name="dateto">结束时间</param>
    /// <returns></returns>
    public DataTable GetCheckDiff(string pcode, string pname, string ptype, string wareno, string poscode,
                                  string pcinv, string pOracle, string datefrom, string dateto)
    {
        DataTable tb = null;
        try
        {
            string Sql = @"
                                 select di.cticketcode,di.ccheckname,di.cerpcode,di.cwareid,di.cwarename,di.cpositioncode,
                                  di.cinvcode, di.iquantity, di.onenum,convert(nvarchar(30), di.onedate,120) onedate, 
                                  di.onename, di.twonum, convert(nvarchar(30), di.twodate,120) twodate, '物理' CHECKTYPE,
                                  di.twoname,di.onediff,di.twodiff,convert(nvarchar(30), di.dcheckdate,120) dcheckdate
                                  from view_stock_checkdiff di
                                  where 1=1 
                             
                             ";
            //盘点单号
            if (pcode != "")
            {
                Sql += " and upper(di.cticketcode) like '%" + pcode + "%'";
            }
            //盘点单名称
            if (pname != "")
            {
                Sql += " and di.ccheckname like '%" + pname + "%'";
            }
            //差异类型
            if (ptype != "")
            {
                Sql += @" and ((1=" + ptype + "  and ((di.onediff=0 and di.twodiff=0) or (di.onediff=0 and di.twodiff is null))) " +
                       " or (0=" + ptype + "  and  ((di.onediff !=0 or di.twodiff !=0) and (di.onediff is not null))))";
            }
            //仓库
            if (wareno != "")
            {
                Sql += " and di.cwareid like '" + wareno + "%'";
            }
            //储位
            if (poscode != "")
            {
                Sql += " and di.cpositioncode like '" + poscode + "%'";
            }
            //料号
            if (pcinv != "")
            {
                Sql += " and di.cinvcode like '" + pcinv + "%'";
            }

            //Oralce盘点单号
            if (pOracle != "")
            {
                Sql += " and di.cerpcode like '%" + pOracle + "%'";
            }
            //起始日期
            if (datefrom != "")
            {
                Sql += " and di.dcheckdate >= " + datefrom;// to_date('" + datefrom + "','yyyy-MM-dd hh24:mi:ss')";
            }
            //结束日期
            if (dateto != "")
            {
                Sql += " and di.dcheckdate <=" + dateto;// to_date('" + dateto + "','yyyy-MM-dd hh24:mi:ss')";
            }
            tb = DBHelp.ExecuteToDataTable(Sql);
        }
        catch (Exception)
        {
            return tb;
        }
        return tb;
    }


    public DataTable GetMergeErpCode(string ErpCode, bool isGetRowCount, int pageIndex, int pageSize)
    {
        string strSQL = @"  SELECT top  " + pageSize + " * FROM V_Merge_ErpCode_Report where 1=1 ";

         
        List<IDbDataParameter> paras = new List<IDbDataParameter>();
        IDbDataParameter para;
        string strOperator;

        //ErpCode
        if (ErpCode.IsNullOrEmpty() == false)
        {
          
            strSQL += " and cerpcode = '"+ErpCode+"' ";
            
        }

        strSQL += "  and  RowNum >   " + (pageIndex - 1) * pageSize ;

        DataTable tb = DBHelp.ExecuteToDataTable(strSQL);
        return tb;
    }

    public DataTable GetList(string erpCode, string dinDateFrom, string dinDinDateTo, string Department, string status, bool isGetRowCount, int pageIndex, int pageSize)
    {

        string sql = "  select top " + pageSize + " vd.* from v_digitalsignage vd, sys_parameter sp where sp.flag_name=vd.STATENAME and sp.flag_type='Electronic_Board' ";

        //erpcode
        if (erpCode != null && erpCode.Length > 0)
        {
            sql += " and erpcode = '" + erpCode + @"'";
        }

        //Roger
        if (!string.IsNullOrEmpty(dinDateFrom) && !string.IsNullOrEmpty(dinDinDateTo))
        {
            sql += " and erpcode in (select wo from in_mo_info where   convert(nvarchar(10),start_date,120)  >=  '" + dinDateFrom + "' and   convert(nvarchar(10),start_date,120) <=  '" + dinDinDateTo + "' ";
        }

        //部门
        if (!string.IsNullOrEmpty(Department))
        {
            if (Department == "生管")
            {
                sql += " and RESP_DEPT is null";
            }
            else
            {
                sql += " and RESP_DEPT = '" + Department + @"'";
            }
        }

        //状态
        if (!string.IsNullOrEmpty(status))
        {
            sql += " and flag_id = '" + status + @"'";
        }

        sql += "  and  RowNum >   " + (pageIndex - 1) * pageSize;
        
        DataTable tb = DBHelp.ExecuteToDataTable(sql);

        return tb;

    }

    public DataTable GetList2(string erpCode, string dinDateFrom, string dinDinDateTo, string Department, string status, bool isGetRowCount, int pageIndex, int pageSize)
    {

        /* ******************
         *  请自行调用存储过程pro_Digital()
         * *******************/
        // string sql = "  select vd.* from v_digitalsignage vd, sys_parameter sp where sp.flag_name=vd.STATENAME and sp.flag_type='Electronic_Board' ";


        string sql = @"
                     select vd.*,cinvcode,qall from v_digitalsignage vd
                     left join sys_parameter sp on sp.flag_name=vd.STATENAME
                     left join 
                     ( 
                        select * from 
                       (
                           select  tt.*,s.iqty from 
                           (
                               select t.qallcinvcode,t.ERPCODE ,
                                 substring(t.qallcinvcode,1,charindex('|',t.qallcinvcode)-1) cinvcode,
	                             substring(t.qallcinvcode,1,charindex('|',t.qallcinvcode)+1) qall
                                 from v_digitalsignage t 
              
                           )tt 
                           LEFT join 
                           (
                                 select CINVCODE,sum(iqty) iqty from STOCK_CURRENT group by CINVCODE
             
                           ) s on s.CINVCODE=tt.cinvcode
       
                       ) tp
   
                     ) tttt on tttt.erpcode =vd.ERPCODE
 
                      where   sp.flag_type='Electronic_Board' and iqty is not null

                      ";

        //erpcode
        if (erpCode != null && erpCode.Length > 0)
        {
            sql += " and vd.erpcode like '%" + erpCode + @"%'";
        }

        //Roger
        if (!string.IsNullOrEmpty(dinDateFrom) && !string.IsNullOrEmpty(dinDinDateTo))
        {
            sql += " and vd.erpcode in (select wo from in_mo_info where start_date >= '" + dinDateFrom + "' and start_date <= '" + dinDinDateTo + "') ";
        }

        //部门
        if (!string.IsNullOrEmpty(Department))
        {
            sql += " and vd.RESP_DEPT like '%" + Department + @"%'";
        }

        //状态
        if (!string.IsNullOrEmpty(status))
        {
            sql += " and sp.flag_id = '%" + status + @"%'";
        }

        PageSpliter pageSpliter = new PageSpliter(sql);
        pageSpliter.PageIndex = pageIndex;
        pageSpliter.PageSize = pageSize;
        string psSql = pageSpliter.GetPageSQL();

        DataTable tb = DBHelp.ExecuteToDataTable(psSql);

        return tb;

    }

    public DataTable ExportExcelList(string erpCode, string dinDateFrom, string dinDinDateTo, string Department, string status)
    {
        string sql = "  select RESP_DEPT, ERPCODE, START_QUANTITY, START_DATE, STATENAME, PARTCOUNT, OASSITALLYES, OASSITPARTNO, TEMPBILLALLYES, TEMPBILLPARTNO, OBILLALLYES, OBILLPARTNO, QALLYES from v_digitalsignage vd, sys_parameter sp where sp.flag_name=vd.STATENAME and sp.flag_type='Electronic_Board' ";

        if (erpCode != null && erpCode.Length > 0)
        {
            sql += " and erpcode = '" + erpCode + @"'";
        }

        //Roger
        if (!string.IsNullOrEmpty(dinDateFrom) && !string.IsNullOrEmpty(dinDinDateTo))
        {
            sql += " and erpcode in (select wo from in_mo_info where start_date >= '" + dinDateFrom + "' and start_date <='" + dinDinDateTo + "'  )";
        }

        //部门
        if (!string.IsNullOrEmpty(Department))
        {
            if (Department == "生管")
            {
                sql += " and RESP_DEPT is null";
            }
            else
            {
                sql += " and RESP_DEPT = '" + Department + @"'";
            }
        }

        //状态
        if (!string.IsNullOrEmpty(status))
        {
            sql += " and flag_id = '" + status + @"'";
        }


        DataTable tb = DBHelp.ExecuteToDataTable(sql);

        return tb;
    }

    //部门
    public DataTable GetDepartment()
    {
        var sql = "select t.department_name from mo_department t where t.status='1' ";
        DataTable tb = DBHelp.ExecuteToDataTable(sql);

        return tb;
    }

    //部门
    public DataTable GetDepartmentMO()
    {
        var sql = " select t.departmentno, t.departmentname from base_department t ";
        DataTable tb = DBHelp.ExecuteToDataTable(sql);

        return tb;
    }

    //状态
    public DataTable GetStatus()
    {
        var sql = "select t.FLAG_ID,t.flag_name from sys_parameter t where t.flag_type='Electronic_Board' ";
        DataTable tb = DBHelp.ExecuteToDataTable(sql);

        return tb;
    }

    /// <summary>
    /// 获取物料名称
    /// </summary>
    /// <param name="CPARTNUMBER"></param>
    /// <returns></returns>
    public string GetPartName(string CPARTNUMBER)
    {
        string sql = string.Format(@" select t.cpartname from base_part t where t.CPARTNUMBER='{0}' ", CPARTNUMBER);
        var obj = DBHelp.ExecuteScalar(sql);
        return obj == null ? string.Empty : obj.ToString();
    }

    //CSPECIFICATIONS
    /// <summary>
    /// 获取物料规格
    /// </summary>
    /// <param name="CPARTNUMBER"></param>
    /// <returns></returns>
    public string GetSpecifications(string CPARTNUMBER)
    {
        string sql = string.Format(@" select t.CSPECIFICATIONS from base_part t where t.CPARTNUMBER='{0}' ", CPARTNUMBER);
        var obj = DBHelp.ExecuteScalar(sql);
        return obj == null ? string.Empty : obj.ToString();
    }

    /// <summary>
    /// 获取物料总库存数量
    /// </summary>
    /// <param name="CPARTNUMBER"></param>
    /// <returns></returns>
    public string GetPartSumNum(string CPARTNUMBER)
    {
        string sql = string.Format(@" select nvl(sum(sc.iqty),0) from stock_current sc where sc.cinvcode='{0}' ", CPARTNUMBER);
        var obj = DBHelp.ExecuteScalar(sql);
        return obj == null ? string.Empty : obj.ToString();
    }


}