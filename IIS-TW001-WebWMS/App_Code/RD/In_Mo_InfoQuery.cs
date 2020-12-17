using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class In_Mo_InfoQuery
    {
          /// <summary>
        /// 根据工单号取工单信息
        /// </summary>
        /// <param name="gdh"></param>
        /// <returns></returns>
        public DataTable GetIn_InfoBYIdName(string gdh)
        {
            string sql = @"select ID from In_Mo_Info A where 1=1";
            if (!string.IsNullOrEmpty(gdh))
            {

                sql += " and A.WO ='" + gdh+"'";
             
            }
            DataTable dtGD = DBHelp.ExecuteToDataTable(sql);
            return dtGD;
        }

        //20130510105528
        public static bool CanChangeDate(string erpCode)
        {
            var sql = @"select count(*)
                          from outasn o
                         where 1 = 1
                           and o.ITYPE = '35'
                           and ((o.cstatus not in ('0')) or
                                (exists (select 1 from outbill ob where ob.coutasnid = o.id)))
                           and o.cerpcode = '" + erpCode + "'";
            var obj = DBHelp.ExecuteScalar(sql);
            if (obj == null)
                return false;
            else
                return int.Parse(obj.ToString()) > 0 ? false : true;
        }

        //Roger 20120615 线体不在对应数据中报错
        public static bool GetLine(string line)
        {
            var sql = "select count(*) from base_line_list t where t.lineid = '" + line + "'";
            var obj = DBHelp.ExecuteScalar(sql);
            if (obj == null)
                return false;
            else
                return int.Parse(obj.ToString()) == 0 ? false : true;
        }
        
        //判断部门编码是否在可维护的编码中
        public static bool HasMaintenance(string Departmentno)
        {
            var sql = "select count(*) from BASE_DEPARTMENT t where t.DEPARTMENTNO = '" + Departmentno + "'";
            var obj = DBHelp.ExecuteScalar(sql);
            if (obj == null)
                return false;
            else
                return int.Parse(obj.ToString()) == 0 ? false : true;
        }
        /// <summary>
        /// 导出所有错误数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetTempList()
        {
            string strSQL = @" select * from V_Temp_IN_MO_Info";

            DataTable dtAreaList = DBHelp.ExecuteToDataTable(strSQL);
            return dtAreaList;

        }
    }


