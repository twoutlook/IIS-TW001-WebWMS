using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DreamTek.ASRS.Business;
using System.Data;

/// <summary>
/// OUT_TRANSPORTATIONPLANQuery 的摘要说明
/// </summary>
public class OUT_TRANSPORTATIONPLANQuery
{
    public OUT_TRANSPORTATIONPLANQuery()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static string BatchOUTTRANSPORTATIONPLAN(Dictionary<string, string> OUTTRANSPORTATIONPLAN_DOCNO_ID, string UserNo)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string item in OUTTRANSPORTATIONPLAN_DOCNO_ID.Keys)
        {
            string msg = string.Empty;
            #region 调用存储过程
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_DOCNO_ID:" + item.Trim());
            SparaList.Add("@P_UserNo:" + UserNo);
            SparaList.Add("@P_return_Value:" + "");
            SparaList.Add("@P_ErrText:" + "");
            string[] Result = DBHelp.ExecuteProc("OUT_TRANSPORTATIONPLAN_MERGE", SparaList);
            if (Result.Length == 1)
            {
                msg += Result[0].ToString();
            }
            else if (Result[0] == "0")
            {
                msg += Resources.Lang.OUT_TRANSPORTATIONPLANQuery_Msg1 + "!";//"合并成功!";
            }
            else
            {
                msg += Result[1].ToString();
            }
            #endregion
        }
        return sb.ToString();
    }
}