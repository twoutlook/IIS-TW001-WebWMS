using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using DreamTek.WMS.Repository.Stock;
using DreamTek.WMS.DAL.Model;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business.SP.ProcedureModel;

public partial class FrmSTOCK_PalletCodeList1 : WMSBasePage
{
    public List<string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as List<string>; }
    }
    //DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            this.InitPage();        
        }
        this.Btn_BatchOut.Attributes["onclick"] = this.GetPostBackEventReference(this.Btn_BatchOut) + ";this.disabled=true;";
        this.Btn_UnionInAgain.Attributes["onclick"] = this.GetPostBackEventReference(this.Btn_UnionInAgain) + ";this.disabled=true;";     
    }


    #region IPageGrid 成员
    public void GridBind()
    {
       
        int total = 0;

        V_STOCK_POSTITON whereObject = new V_STOCK_POSTITON();
        whereObject.wareid = ddlWareHouse.SelectedValue;
        whereObject.ctype = DropCTYPE.SelectedValue;
        whereObject.cpositioncode = txtCPocitionCode.Text.Trim();
        whereObject.palletcode = txtPalledCode.Text.Trim();
        whereObject.cinvcode =  txtCinvcode.Text.Trim();
        whereObject.hasPallet =rbtList.SelectedValue ;
        whereObject.productcode=txtProductCode.Text.Trim();
        whereObject.palletcodes = whereObject.palletcode.Replace('，',',').Split(',');
        IEnumerable<V_STOCK_POSTITON> curStockList = new StockRepository().GetAllStockCurrentPosition(whereObject, PageSize, CurrendIndex, out total);
        AspNetPager1.RecordCount = total;
        AspNetPager1.PageSize = this.PageSize;
        grdStockPostition.DataSource = curStockList;
        grdStockPostition.DataBind();
    }
    #endregion
    
    #region IPage 成员
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        
        //储位种类
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "CARGOSPACETYPE", false, -1, -1), this.DropCTYPE, "全部", "FLAG_NAME", "FLAG_ID", "");
        //仓库
        Help.DropDownListDataBind(GetWareHouse(), this.ddlWareHouse, "", "cwarename", "id", "");
    }
    #endregion

    #region 方法
    /// <summary>
    /// 获取入库类型名称
    /// </summary>
    /// <param name="iType"></param>
    /// <returns></returns>
    public static string GetInTypeName(string iType)
    {
        string strSQL = string.Format(@"SELECT A.TYPENAME FROM INTYPE A WHERE A.CERPCODE = '{0}'", iType);
        return DBHelp.ExecuteScalar(strSQL).ToString();
    }

    public bool CheckData()
    {
        return false;
    }
    public DataTable GetWareHouse()
    {
        string sql = @"SELECT id,cwareid,cwarename FROM dbo.BASE_WAREHOUSE WITH(NOLOCK) WHERE cdefine2='1' ORDER BY cwareid DESC";
        return DBHelp.ExecuteToDataTable(sql);
    }
    #endregion
    #region 事件
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    /// <summary>
    /// 批量出库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_BatchOut_Click(object sender, EventArgs e)
    {
        Btn_BatchOut.Enabled = false;
        string userNo = WmsWebUserInfo.GetCurrentUser().UserNo;
        string return_value = string.Empty;
        string error_value = string.Empty;
      
        string msg = string.Empty;
        if (SelectIds != null)
        {
            SelectIds.Clear();
        }
        if (SelectIds == null)
        {
            SelectIds = new List<string>();
        }     
        try
        {
            for (int i = 0; i < this.grdStockPostition.Rows.Count; i++)
            {
                if (this.grdStockPostition.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdStockPostition.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        //主键赋值
                        var cpositioncode = grdStockPostition.DataKeys[i].Values[1].ToString();                      
                        SelectIds.Add(cpositioncode);
                    }
                }
            }
            if (SelectIds!=null)
            {
                foreach (var cpositioncode in SelectIds)
                {
                    //listQuery.UpdateStockAdjustData(vid, userNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "2");
                    Proc_CreateTicketsByPositioncode_OUT proc = new Proc_CreateTicketsByPositioncode_OUT();
                    proc.P_Positioncode = cpositioncode;
                    proc.P_UserNo = userNo;
                    proc.P_LineId = "";
                    proc.P_SITEID = "";
                    proc.Execute();
                    return_value = proc.P_Return_Value.ToString();
                    error_value = proc.P_ErrText;
                    if (return_value == "1")
                    {
                        this.Alert("批量出库失败！储位" + "[" + cpositioncode + "]" + error_value.ToJsString());
                        msg += error_value.ToJsString();
                        Btn_BatchOut.Enabled = true;
                        return;
                    }
                    else
                    {
                        msg += "";
                    }
                }
                if (string.IsNullOrEmpty(msg))
                {
                    this.Alert("批量出库成功！");                    
                }
            }
            else
            {
                this.Alert("批量出库失败！没有选择的项");
            }
        }
        catch (Exception E)
        {
            this.Alert("批量出库失败！" + E.Message.ToJsString());
        }
        this.GridBind();
        Btn_BatchOut.Enabled = true;
    }
    /// <summary>
    /// 合并重入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_UnionInAgain_Click(object sender, EventArgs e)
    {
        Btn_UnionInAgain.Enabled = false;
        string userNo = WmsWebUserInfo.GetCurrentUser().UserNo;
        string return_value = string.Empty;
        string error_value = string.Empty;
        string msg = string.Empty;       
        if (SelectIds != null)
        {
            SelectIds.Clear();
        }
        if (SelectIds == null)
        {
            SelectIds = new List<string>();
        }       
        try
        {
            for (int i = 0; i < this.grdStockPostition.Rows.Count; i++)
            {
                if (this.grdStockPostition.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdStockPostition.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        //主键赋值
                        var cpositioncode = grdStockPostition.DataKeys[i].Values[1].ToString();
                        SelectIds.Add(cpositioncode);
                    }
                }
            }
            if (SelectIds != null)
            {
                if (SelectIds.Count<2)
                {
                    this.Alert("至少选择两项进行合并重入！");
                    Btn_UnionInAgain.Enabled = true;
                    return;
                }
                foreach (var cpositioncode in SelectIds)
                {                   
                    Proc_UnionInAgain proc = new Proc_UnionInAgain();
                    proc.P_Positioncode = cpositioncode;
                    proc.P_UserNo = userNo;
                    proc.Execute();
                    return_value = proc.P_Return_Value.ToString();
                    error_value = proc.P_ErrText;
                    if (return_value == "1")
                    {
                        this.Alert("合并重入失败！储位" + "[" + cpositioncode + "]" + error_value.ToJsString());
                        msg += error_value.ToJsString();
                        Btn_UnionInAgain.Enabled = true;
                        return;
                    }
                    else
                    {
                        msg += "";
                    }                       
                }
                if (string.IsNullOrEmpty(msg))
                {
                    this.Alert("合并重入成功！");
                }               
            }
            else
            {
                this.Alert("合并重入失败！没有选择的项");
            }
        }
        catch (Exception E)
        {
            this.Alert("合并重入失败！" + E.Message.ToJsString());
        }
        this.GridBind();
        Btn_UnionInAgain.Enabled = true;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            CurrendIndex = 1;
            //AspNetPager1.CurrentPageIndex = 1;
            this.GridBind();
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }
    protected void grdStockPostition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = grdStockPostition.DataKeys[e.Row.RowIndex].Values[0].ToString();
            string positioncode =  grdStockPostition.DataKeys[e.Row.RowIndex].Values[1].ToString();
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, "FrmStockPallet_Out.aspx?cpositioncode=" + positioncode + "", "栈板出库", "BASE_CARGOSPACE");
            //BuildRequestPageURL("FrmStockPallet_Out.aspx", SYSOperation.Modify, strKeyID)
            ////hlToIOCCUPYQTY_Info
            HyperLink linkStork = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
            linkStork.NavigateUrl = "#";
            //this.OpenFloatWinMax(linkStork, BuildRequestPageURL("FrmSTOCK_CurrentQueryList.aspx", SYSOperation.Modify, strKeyID) + "&StockCurrentIndex=" + CurrendIndex, "库存明细", "STOCK_CURRENT_DETAIL");
            this.OpenFloatWinMax(linkStork, BuildRequestPageURL("/Apps/STOCK/FrmSTOCK_CurrentQueryList.aspx?Flag=1&ids=" + positioncode, SYSOperation.View, ""), "库存明细", "STOCK_Current");
        }



        //}
    }
    #endregion


}