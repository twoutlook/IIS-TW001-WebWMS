using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
public class OutTypeQuery
    {
        public DataTable GetList()
        {
            string strSQL = "SELECT typename,cerpcode from OUTTYPE A where (DISABLE_DATE is null or DISABLE_DATE >= GetDate());";
            DataTable dtCARGOSPACE = DBHelp.ExecuteToDataTable(strSQL);
            return dtCARGOSPACE;
        }

        /// 获取超领类型
        /// <summary>
        /// 获取超领类型
        /// </summary>
        /// <param name="departno">部门编码</param>
        /// <param name="bz">标识，0有效 1 全部 </param>
        /// <returns></returns>
        public static DataTable GetChaoLType(string departno, string bz)
        {
            try
            {
                string Sql = @"select distinct bd.typecode,bd.typename 
                                 from base_department_otype bd where 1=1";
                if (string.IsNullOrEmpty(departno) == false)
                {
                    Sql += " and bd.departmentno='" + departno + "'";
                }
                if (bz == "0")
                {
                    Sql += "   and bd.flag = 0 ";
                }
                Sql += " order by bd.typecode asc";
                DataTable tb = DBHelp.ExecuteToDataTable(Sql);
                return tb;
            }
            catch (Exception)
            {
                return null;
            }
           
        }
    }
