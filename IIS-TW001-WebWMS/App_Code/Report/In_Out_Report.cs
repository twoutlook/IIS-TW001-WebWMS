using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DreamTek.WMS.DAL.Common;
public class In_Out_Report
{
    public class Menuss
    {
        public string Cwareid { get; set; }
        public string Cwarename { get; set; }
        public string CPOSITIONCODE { get; set; }
        public string cposition { get; set; }
        public string CINVCODE { get; set; }
        public decimal initCount { get; set; }
        public decimal inCount { get; set; }
        public decimal outCount { get; set; }
        public decimal finalCount { get; set; }
        public string cspecifications { get; set; }
    }
    public static IQueryable<Menuss> Menus(DataTable dt)
    {
        List<Menuss> list = new List<Menuss>();
        if (dt == null) return list.AsQueryable();
        list = (from DataRow dr in dt.Rows
                select new Menuss
                {
                    Cwareid = dr["Cwareid"].ToString(),
                    Cwarename = dr["Cwarename"].ToString(),
                    CPOSITIONCODE = dr["CPOSITIONCODE"].ToString(),
                    cposition = dr["cposition"].ToString(),
                    CINVCODE = dr["CINVCODE"].ToString(),
                    initCount = Convert.ToDecimal(dr["initCount"].ToString()),
                    inCount = Convert.ToDecimal(dr["inCount"].ToString()),
                    outCount = Convert.ToDecimal(dr["outCount"].ToString()),
                    finalCount = Convert.ToDecimal(dr["finalCount"].ToString()),
                    cspecifications = dr["cspecifications"].ToString()
                }).ToList();
        return list.AsQueryable();
    }
    public static DataTable FrmInOut_Report(string dateFrom, string dateTo, string WAREHOUSE, string CARGOSPACE, string PART, string cspec)
    {
        string WhSql = "", WhSql1 = "", WhSql2 = "";
        string WhdSql = "", WhdSql1 = "", WhdSql2 = "";

        if (!string.IsNullOrEmpty(dateFrom))
        {
            WhSql += " and (('" + dateFrom + "' is null) or DINDATE >= '" + dateFrom + "')";
            WhdSql += " and (('" + dateFrom + "' is null) or doutdate >= '" + dateFrom + "')";
        }
        if (!string.IsNullOrEmpty(dateTo))
        {
            dateTo = dateTo + " 23:59:59";
            WhSql1 += "and (('" + dateTo + "' is null) or DINDATE <= '" + dateTo + "')";
            WhdSql1 += "and (('" + dateTo + "' is null) or doutdate <= '" + dateTo + "')";
        }
        if (!string.IsNullOrEmpty(dateFrom))
        {
            WhSql2 += "and  ('" + dateFrom + "' is null) or DINDATE < '" + dateFrom + "'";
            WhdSql2 += "and  ('" + dateFrom + "' is null) or doutdate < '" + dateFrom + "'";
        }
        string strSQL = string.Format(@"select wareHouse.Cwareid,
                                     wareHouse.Cwarename,
                                     in_D.CPOSITIONCODE,
                                     cargospace.cposition,
                                     in_D.CINVCODE,
                                     (isnull(inInit.in_Num,0) - isnull(outInit.out_Num,0)) initCount,
                                     isnull(in_D.inNum, 0) inCount,
                                     isnull(out_D.outNum, 0) outCount,
                                     ((isnull(inInit.in_Num,0) - isnull(outInit.out_Num,0)) + isnull(in_D.inNum, 0) - isnull(out_D.outNum, 0)) finalCount
                                    ,(SELECT TOP 1 ISNULL(p.cspecifications,'') AS cspecifications FROM dbo.BASE_PART p WITH(NOLOCK) WHERE p.cpartnumber =in_d.cinvcode) AS cspecifications ---物料规格	
                                from ((select CPOSITIONCODE, CINVCODE, sum(IQUANTITY) inNum
                                          from inBill_D where 1=1 {0}  {1}
                                         group by CPOSITIONCODE, CINVCODE) in_D left
                                      join(select CPOSITIONCODE, CINVCODE, sum(IQUANTITY) outNum
                                              from OUTBILL_D where 1=1 {3} {4}
                                             group by CPOSITIONCODE, CINVCODE) out_D on
                                      in_D.CPOSITIONCODE = out_D.CPOSITIONCODE and
                                      in_D.CINVCODE = out_D.CINVCODE)" + @"inner join BASE_CARGOSPACE cargospace
                              on cargospace.CPOSITIONCODE = in_D.CPOSITIONCODE
                           inner join BASE_WAREHOUSE wareHouse
                              on wareHouse.Id = cargospace.warehouseid
                            left join (select CPOSITIONCODE, CINVCODE,sum(IQUANTITY) in_Num
                                    from inBill_D 
                                   where 1=1 {2}
                                   group by CPOSITIONCODE, CINVCODE) inInit
                              on inInit.CPOSITIONCODE = in_D.CPOSITIONCODE and inInit.CINVCODE = in_D.CINVCODE
                            left join (select CPOSITIONCODE, CINVCODE,sum(IQUANTITY) out_Num
                                    from OUTBILL_D 
                                   where 1=1 {5}
                                   group by CPOSITIONCODE, CINVCODE) outInit
                              on outInit.CPOSITIONCODE = in_D.CPOSITIONCODE and outInit.CINVCODE = in_D.CINVCODE", WhSql, WhSql1, WhSql2, WhdSql, WhdSql1, WhdSql2);
        strSQL += " where 1=1";
        if (!string.IsNullOrEmpty(WAREHOUSE))
        {
            strSQL += " and wareHouse.Cwareid like '%" + WAREHOUSE + "%'";
        }
        if (!string.IsNullOrEmpty(CARGOSPACE))
        {
            strSQL += " and in_D.CPOSITIONCODE like '%" + CARGOSPACE + "%'";
        }
        if (!string.IsNullOrEmpty(PART))
        {
            strSQL += " and in_D.CINVCODE like '%" + PART + "%'";
        }
        if (!string.IsNullOrEmpty(cspec))
        {
            strSQL += " AND EXISTS (SELECT 1 FROM dbo.BASE_PART p WITH(NOLOCK) WHERE p.cpartnumber =in_d.cinvcode AND p.cspecifications LIKE '%" + cspec + "%')";
        }
        return DBHelp.ExecuteToDataTable(strSQL);
    }
    /// <summary>
    /// 物料进出明细报表加上月份汇总
    /// </summary>
    /// <param name="cinvcode"></param>
    /// <param name="cspec"></param>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <returns></returns>
    public static DataSet FrmInOut_ReprotNew(string cinvcode, string cspec, string dateFrom, string dateTo)
    {
        string str_guid = Guid.NewGuid().ToString();
        string str_language = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        string strSQL = string.Format(@"EXEC dbo.Proc_QueryInOutdetailsReport '{0}','{1}','{2}','{3}','{4}','{5}'", str_guid, cinvcode, cspec, dateFrom, dateTo, str_language);
        return DBHelp.ExecuteToDataSet(strSQL);
    }
    /// <summary>
    /// 物料进出汇总报表
    /// </summary>
    /// <param name="cinvcode"></param>
    /// <param name="cspec"></param>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <returns></returns>
    public static DataTable FrmInOut_ReprotSummay(string cinvcode, string cspec, string dateFrom, string dateTo,string rank_final,int index, int pagesize, out int count)
    {
        count = 0;
        //准备查询的数据
        string str_guid = Guid.NewGuid().ToString();
        string strSQL = string.Format(@"EXEC dbo.Proc_QueryInOutSummayReport '{0}','{1}','{2}','{3}','{4}'", str_guid, cinvcode, cspec, dateFrom, dateTo);
        DBHelp.ExecuteNonQuery(strSQL);

        string sql = " select * from dbo.Temp_QueryInOutSummaryReport v where v.id = '" + str_guid + "' ";
        if (!string.IsNullOrEmpty(rank_final))
        {
            sql += " AND substring(v.cinvcode,len(v.cinvcode),1)='" + rank_final + "'";
        }
        sql += " AND substring(v.cinvcode,len(v.cinvcode)-1,1)='-'";

        string sqlCount = " select count(1) from dbo.Temp_QueryInOutSummaryReport v where v.id = '" + str_guid + "'";
        if (!string.IsNullOrEmpty(rank_final))
        {
            sqlCount += " AND substring(v.cinvcode,len(v.cinvcode),1)='" + rank_final + "'";
        }
        sqlCount += " AND substring(v.cinvcode,len(v.cinvcode)-1,1)='-'";

        string whereSql = string.Empty;

        string psSql = sql;
        if (index != -1)
        {
            PageSpliter ps = new PageSpliter(sql);
            ps.OrderBySql = " order by cinvcode";
            ps.PageSize = pagesize;
            ps.PageIndex = index;
            psSql = ps.GetPageSQL();
        }
        DataTable dt = SqlDBHelp.ExecuteToDataTable(psSql);
        Object obj = SqlDBHelp.ExcuteScalarSQL(sqlCount);
        if (obj != null)
        {
            int.TryParse(obj.ToString(), out count);
        }
        //int allpagescount =decimal.ToInt32(Math.Round(decimal.Parse(count.ToString())/decimal.Parse(pagesize.ToString())));
        //if (allpagescount == index || index == -1 || count < pagesize)  //尾页显示统计信息
        //{
        //得出统计的数据值
        sqlCount = string.Format(@"SELECT SUM(beginstocksummary) AS Allbegin,SUM(insummary) AS AllIn,SUM(outsummary) AS AllOut,SUM(endstocksunmary) AS Allend FROM dbo.Temp_QueryInOutSummaryReport WITH(NOLOCK) WHERE id='{0}'", str_guid);
        if (!string.IsNullOrEmpty(rank_final))
        {
            sqlCount += " AND substring(cinvcode,len(cinvcode),1)='" + rank_final + "'";
        }
        sqlCount += " AND substring(cinvcode,len(cinvcode)-1,1)='-'";

        DataTable dtCount = SqlDBHelp.ExecuteToDataTable(sqlCount);

        if (dtCount != null && dtCount.Rows.Count > 0 && !string.IsNullOrEmpty(dtCount.Rows[0][0].ToString()))
        {
            DataRow dr = dt.NewRow();
            //注意这里因为页面显示与导出的dt的列的数量不一致，所以这里倒序写入数据，省的判断分开写逻辑---多一个行数的字段
            dr[dr.ItemArray.Count() - 1] = decimal.Parse(dtCount.Rows[0]["Allend"].ToString()).ToString("N2");
            dr[dr.ItemArray.Count() - 2] = decimal.Parse(dtCount.Rows[0]["AllOut"].ToString()).ToString("N2");
            dr[dr.ItemArray.Count() - 3] = decimal.Parse(dtCount.Rows[0]["AllIn"].ToString()).ToString("N2");
            dr[dr.ItemArray.Count() - 4] = decimal.Parse(dtCount.Rows[0]["Allbegin"].ToString()).ToString("N2");
            dr[dr.ItemArray.Count() - 5] = "合计";
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }

        //}
        //得到结果集后删除结果集
        strSQL = string.Format(@"DELETE FROM dbo.Temp_QueryInOutSummaryReport WHERE id='{0}' ", str_guid);
        DBHelp.ExecuteNonQuery(strSQL);

        return dt;
    }
}