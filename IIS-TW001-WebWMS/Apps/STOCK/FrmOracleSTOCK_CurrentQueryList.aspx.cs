using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ExternalService.NCS;
using DreamTek.ExternalService;
/// <summary>
/// 描述: 期间库存-->FrmSTOCK_DURATIONList 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-10-17 15:48:44
/// </summary>
public partial class FrmOracleSTOCK_CurrentQueryList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.grdSTOCK_DURATION.Columns[1].Visible = false;
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            //btnSearch_Click(null, null);
        }      
    }
    

    #region IPageGrid 成员   
    public bool CheckData()
    {
        //if(this.txtCTICKETCODE.Text.Trim().Length > 0)
        //{
        //}
        //if(this.txtDURATIONBEGIN.Text.Trim().Length > 0)
        //{
        //    if(this.txtDURATIONBEGIN.Text.IsDate()== false)
        //    {
        //        this.Alert("本期开始日期项不是有效的日期！");
        //        this.SetFocus(txtDURATIONBEGIN);
        //        return false;
        //    }
        //}
        //if(this.txtDURATIONEND.Text.Trim().Length > 0)
        //{
        //    if(this.txtDURATIONEND.Text.IsDate()== false)
        //    {
        //        this.Alert("本期结束日期项不是有效的日期！");
        //        this.SetFocus(txtDURATIONEND);
        //        return false;
        //    }
        //}
        //if(this.txtCPOSITIONCODE.Text.Trim().Length== 0)
        //{
        //    Alert("储位必须输入!");
        //    SetFocus(txtCPOSITIONCODE);
        //    return false;
        //}
        //if (this.txtCPOSITIONCODE.Text.Trim().Length > 0)
        //{
        //}
        
        return true;

    }

    #endregion

    #region IPage 成员
   
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";     
                
    }

    #endregion
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    public void GridBind()
    {

        var caseList = new StockCompare().CompareStockByCeriaFromErp(txtCINVCODE.Text.Trim());
        if (caseList != null && caseList.Count() > 0)
        {               
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
            grdSTOCK_DURATION.DataSource = GetPageSize(caseList.AsQueryable(), PageSize, CurrendIndex).ToList();
        }
        else
        {
            AspNetPager1.RecordCount = 0;
            grdSTOCK_DURATION.DataSource = null;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";         
    }
  
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (CheckData())
        {
            CurrendIndex = 1;
            GridBind();
           
        }
    }  

    protected void grdSTOCK_DURATION_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }

  
    
  
}

