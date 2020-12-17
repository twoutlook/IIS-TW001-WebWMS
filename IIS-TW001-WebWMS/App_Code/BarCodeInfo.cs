using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// SNRuleCodeEntity 的摘要说明
/// </summary>
public class BarCodeInfo
{
    public BarCodeInfo()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    //public SNRuleCodeEntity(string snCode)
    //{
    //    string sql = string.Format(@"SELECT * FROM Fun_GetInfoFromBarCode('{0}')", snCode);
    //    var dt = DBHelp.ExecuteToDataTable(sql);
    //    foreach (DataRow row in dt.Rows)
    //    {
    //        ReturnValue = row["ReturnValue"].ToString();
    //        ErrorMsg = row["ErrorMsg"].ToString();
    //        this.RuleCode = row["RuleCode"].ToString();
    //        this.CinvCode = row["CinvCode"].ToString();
    //        this.DateCode = row["DateCode"].ToString();
    //        this.Quantity = row["Quantity"] != null ? Convert.ToDecimal(row["Quantity"]) : decimal.Zero;
    //    }
    //}

    public string ReturnValue
    {
        get;
        set;
    }
    public string ErrorMsg
    {
        get;
        set;
    }

    public string RuleCode
    {
        get;
        set;
    }
    public string CinvCode
    {
        get;
        set;
    }

    public string DateCode
    {
        get;
        set;
    }

    public Nullable<decimal> Quantity
    {
        get;
        set;
    }

}