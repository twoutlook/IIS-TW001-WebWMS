using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_ALLOCATE_GetSTOCK_CURRENT_Qty : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["CINVCODE"] != null && Request.QueryString["CINVCODE"].Length > 0 && Request.QueryString["CPOSITIONCODE"] != null && Request.QueryString["CPOSITIONCODE"].Length > 0)
            {
                string qty = this.GetQtyByPartCodeAndCPositionCode(Request.QueryString["CINVCODE"], Request.QueryString["CPOSITIONCODE"]).ToString();
                Response.Write(qty);                
            }
            Response.End();
        }
    }

    /// <summary>
    /// 获取该料号在指定货位的现库存量
    /// </summary>
    /// <param name="PartCode"></param>
    /// <param name="PositionCode"></param>
    /// <returns></returns>
    public decimal GetQtyByPartCodeAndCPositionCode(string PartCode, string PositionCode)
    {
        var modStock = db.STOCK_CURRENT.Where(x => x.cpositioncode == PositionCode && x.cinvcode == PartCode).FirstOrDefault();
        if (modStock != null)
        {
            return modStock.iqty - modStock.ioccupyqty;
        }
        else {
            return 0;
        }
    }
}