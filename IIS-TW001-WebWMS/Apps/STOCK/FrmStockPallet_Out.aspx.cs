using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.Business.SP.ProcedureModel;
using DreamTek.WMS.DAL.Model;
using DreamTek.WMS.Repository.Stock;

public partial class FrmStockPallet_Out : WMSBasePage
{
    StockRepository storkquery = new StockRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();
            ShowData();
        }
        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";     

    }
    #region 方法
    /// <summary>
    /// 页面初始化
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTASN');return false;";

        var paraList = context.SYS_PARAMETER.Where(x => x.flag_type == "OS").OrderBy(x => x.flag_id).ToList();
        Help.DropDownListDataBind(GetWareHouse(), this.ddlWareHouse, "", "cwarename", "id", "");//仓库
        Help.DropDownListDataBind(GetLKLineID(), this.ddlOutCrane, "", "FUNCNAME", "EXTEND1",""); //线别
        Help.DropDownListDataBind(GetPallet(ddlOutCrane.SelectedValue), this.ddlOutSite, "", "FUNCNAME", "EXTEND1", ""); //站点
        

    }
    /// <summary>
    /// 页面加载信息
    /// </summary>
    public void ShowData()
    {
        string cposiitoncode = string.Empty;
        int total = 0;
        string crane = "";
        string site = "";
        if (Request.QueryString["cpositioncode"] != null)
        {
            cposiitoncode = Request.QueryString["cpositioncode"].ToString().Trim();
            V_STOCK_POSTITON whereObject = new V_STOCK_POSTITON();         
            whereObject.cpositioncode = cposiitoncode;
            V_STOCK_POSTITON entity = new V_STOCK_POSTITON();
            entity = storkquery.GetAllStockCurrentPosition(whereObject, PageSize, CurrendIndex, out total).FirstOrDefault();
            if (entity != null && !string.IsNullOrEmpty(entity.cpositioncode))
            {
                this.ddlWareHouse.SelectedValue = entity.wareid;
                this.txtPalledCode.Text = entity.palletcode;
                this.txtCPocitionCode.Text = entity.cpositioncode;
                this.txtCPositionName.Text = entity.cposition;
                GetOutSiteInfo(entity.cpositioncode, out crane, out site);
                ddlOutCrane.SelectedValue = crane;
                ddlOutCrane_SelectedIndexChanged(null,null);
                ddlOutSite.SelectedValue = site;
                GridBind();
            }
        }
    }
    /// <summary>
    /// 根据储位编码获得出库线别以及出库站点
    /// </summary>
    /// <param name="cpositionCode"></param>
    /// <param name="crane"></param>
    /// <param name="site"></param>
    public void GetOutSiteInfo(string cpositionCode, out string crane, out string site)
    {
        DreamTek.ASRS.Business.Stock.StockQuery query = new DreamTek.ASRS.Business.Stock.StockQuery();
        crane = "";
        site = "";
        DataTable dt = query.GetSiteInfoByCpositionCode(cpositionCode);
        if(dt!=null && dt.Rows.Count>0)
        {
            crane = dt.Rows[0][0].ToString();
            site = dt.Rows[0][1].ToString();
        }
    }
    /// <summary>
    /// 绑定列表信息
    /// </summary>
    public void GridBind()
    {
        int total = 0;
        Query_Stock_Current_Details whereObject = new Query_Stock_Current_Details();
        whereObject.cpositioncode = txtCPocitionCode.Text.Trim();
        IEnumerable<Query_Stock_Current_Details> curStockList = storkquery.GetStockCurrentDetails(whereObject, PageSize, CurrendIndex, out total).ToList();       
        AspNetPager1.RecordCount = total;
        AspNetPager1.PageSize = this.PageSize;
        grdStockPalletOut.DataSource = curStockList;
        grdStockPalletOut.DataBind();
    }
    /// <summary>
    /// 获得仓库列表信息
    /// </summary>
    /// <returns></returns>
    public DataTable GetWareHouse()
    {
        string sql = @"SELECT id,cwareid,cwarename FROM dbo.BASE_WAREHOUSE WITH(NOLOCK) WHERE cdefine2='1' ORDER BY cwareid DESC";
        return DBHelp.ExecuteToDataTable(sql);
    }

    #endregion
    #region 事件
    public void grdStockPalletOut_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    /// <summary>
    /// 分页控件变更页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        //GridBind();
    }
    /// <summary>
    /// 线别的选择改变事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlOutCrane_SelectedIndexChanged(object sender, EventArgs e)
    {
        Help.DropDownListDataBind(GetPallet(ddlOutCrane.SelectedValue), this.ddlOutSite, "", "FUNCNAME", "EXTEND1", ""); //站点
    }
    /// <summary>
    /// AS/RS出库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = false;
        string userNo = WmsWebUserInfo.GetCurrentUser().UserNo;
        string return_value = string.Empty;
        string error_value = string.Empty;

        string cpositioncode = string.Empty;
        cpositioncode = txtCPocitionCode.Text.Trim();
        try
        {
            if (!string.IsNullOrEmpty(cpositioncode))
            {
                Proc_CreateTicketsByPositioncode_OUT proc = new Proc_CreateTicketsByPositioncode_OUT();
                proc.P_Positioncode = cpositioncode;
                proc.P_UserNo = userNo;
                proc.P_LineId = ddlOutCrane.SelectedValue.Trim();
                proc.P_SITEID = ddlOutSite.SelectedValue.Trim();
                proc.Execute();
                return_value = proc.P_Return_Value.ToString();
                error_value = proc.P_ErrText;                
                if (return_value == "1")
                {
                    this.Alert("AS/RS出库失败！储位" + "[" + cpositioncode + "]" + error_value.ToJsString());
                    return;
                }
                else
                {
                    this.Alert("AS/RS出库成功！");
                    btnSave.Enabled = false;
                }
            }
            else
            {
                this.Alert("储位编码为空");
            }           
        }
        catch (Exception E)
        {
            this.Alert("AS/RS出库失败！" + E.Message.ToJsString());
        }
        btnSave.Enabled = true;
    }
    #endregion
   
}