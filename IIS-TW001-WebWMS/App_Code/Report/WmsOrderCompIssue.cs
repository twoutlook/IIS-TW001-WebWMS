using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// OrderCompIssue 的摘要说明
/// </summary>
public class WmsOrderCompIssue
{
	public WmsOrderCompIssue()
	{

     
    }
    public DataTable OrderCompIssue(string serpcode, string user_no)
    {
        string Sql = @"select distinct(tp.cerpcode) cerpcode,tp.CompNum,tp.NoCompNum,tp.compcinvcode from temp_ordercompissue tp
                         WHERE 1=1 ";
        if (serpcode != "")
        {
            Sql += " and tp.cerpcode ='" + serpcode + "'";
        }
        if (user_no != "")
        {
            Sql += " and tp.tuserno='" + user_no + "'";
        }
        DataTable tb = DBHelp.ExecuteToDataTable(Sql);
        return tb;
    }


    public DataTable OrderCompIssue_D(string serpcode, string scinvcode, string user_no)
    {
        string Sql_d = @"select cinvcode,CASE type_smt WHEN 1 THEN '非SMT' WHEN 2 THEN 'SMT' WHEN 3 THEN '混合' WHEN 4 THEN '非SMT' end  type_smt,wmsyfl,
                         tlnum,clnum,ctnum,required_quantity,wfanum,cfnum,sfnum,cfhdkznum,cfhdwkznum,clcfnum,wlpsnum
                         from temp_ordercompissue tp
                         where  1=1 ";
        if (serpcode != "")
        {
            Sql_d += " and tp.cerpcode ='" + serpcode + "'";
        }
        //料号为0是标志
        if (scinvcode != "")
        {
            Sql_d += " and tp.cinvcode='" + scinvcode + "'";
        }

        if (user_no != "")
        {
            Sql_d += " and tp.tuserno='" + user_no + "'";
        }

        DataTable tb_d = DBHelp.ExecuteToDataTable(Sql_d);
        return tb_d;
    }
}