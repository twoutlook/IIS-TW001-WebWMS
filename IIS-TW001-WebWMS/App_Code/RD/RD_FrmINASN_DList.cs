using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DreamTek.ASRS.Business.SP;

/// <summary>
/// RD_FrmINASN_DListQuery 的摘要说明
/// </summary>
public class RD_FrmINASN_DList
{
    public RD_FrmINASN_DList()
    {
    }


    /// <summary>
    /// 
    /// <param name="id">主表編號</param>
    /// <param name="isGetRowCount">是否是獲取總記錄數</param>
    /// <param name="pageIndex">頁碼,從0開始，任何小於0的參數值，返回所有記錄數</param>
    /// <param name="pageSize">每頁記錄數</param>
    /// <returns></returns>
    public static DataTable GetList_Split(string id, string cinvcode, bool isGetRowCount, int pageIndex, int pageSize)
    {

        string strSQL = @"SELECT A.IDS,A.ID,A.CSTATUS,A.CINVCODE,A.CINVNAME,A.IQUANTITY,A.CINVBARCODE,
                                A.CBATCH,A.CERPCODELINE,A.PO_NUMBERNAME,A.PO_LINENUMBERNAME,sp.flag_name MSTATUS,
                                case when  dbo.Fun_GetInOrOutBill_D_Qty(A.CINVCODE, A.Cerpcodeline, A.Id, 0) > A.IQUANTITY
                                  then A.IQUANTITY 
                                 else  dbo.Fun_GetInOrOutBill_D_Qty(A.CINVCODE, A.Cerpcodeline, A.Id, 0) end
                                 InBill_Qty,
                                case when dbo.Fun_GetInOrOutBill_D_Qty(A.CINVCODE, A.Cerpcodeline, A.Id, 1) > A.IQUANTITY
                                  then A.IQUANTITY
                                  else dbo.Fun_GetInOrOutBill_D_Qty(A.CINVCODE, A.Cerpcodeline, A.Id, 1) end
                                   InBilled_Qty,
                                   
                                   case 
                                   when A.IQUANTITY - dbo.Fun_GetInOrOutBill_D_Qty(A.CINVCODE, A.Cerpcodeline, A.Id, 0) >0
                                   then A.IQUANTITY - dbo.Fun_GetInOrOutBill_D_Qty(A.CINVCODE, A.Cerpcodeline, A.Id, 0)
                                   else 0 end LessQty
                                   
                                from INASN_D A
                                left join sys_parameter sp on sp.flag_type='ASN_D_STATUS' and sp.flag_id=A.Cstatus
                                where 1 = 1  ";
        if (isGetRowCount)
        {
            strSQL = " SELECT count(1) from  dbo.INASN_D A  where 1 = 1 ";
        }
       
        //主表編號
        if (id.IsNullOrEmpty() == false)
        {
            strSQL += string.Format(" and A.ID='{0}'", id);
        }
        //料號
        if (cinvcode.IsNullOrEmpty() == false)
        {
            strSQL += string.Format(" and A.CINVCODE = '{0}'", cinvcode);
        }

        if (isGetRowCount == false)
        {
            strSQL += " order by A.CINVCODE ";
            PageSpliter pageSpliter = new PageSpliter(strSQL);
            pageSpliter.PageSize = pageSize;
            strSQL = pageSpliter.GetPageSQL();
        }
        DataTable dtINASN_D = DBHelp.ExecuteToDataTable(strSQL);
        return dtINASN_D;

    }

}

