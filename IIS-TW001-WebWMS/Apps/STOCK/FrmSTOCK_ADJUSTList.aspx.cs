using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Business.Stock;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business.SP.ProcedureModel;
using DreamTek.WMS.DAL.Model.Base;

/// <summary>
/// 描述: 1111-->FrmSTOCK_ADJUSTList 页面后台类文件
/// 作者: --chenlei
/// 创建于: 2012-11-02 13:23:34
/// </summary>
public partial class STOCK_FrmSTOCK_ADJUSTList :WMSBasePage
{
    StockQuery listQuery = new StockQuery();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
        }
        this.btnConfirm.Attributes["onclick"] = this.GetPostBackEventReference(this.btnConfirm) + ";this.disabled=true;";
        this.btnDelete.Attributes["onclick"] = this.GetPostBackEventReference(this.btnDelete) + ";this.disabled=true;";
        this.btnReview.Attributes["onclick"] = this.GetPostBackEventReference(this.btnReview) + ";this.disabled=true;";
    }

    public List<string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as List<string>; }
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        int pageCount = 0;
        DataTable dtSource = listQuery.GetStockAdjustQueryList(txtCTICKETCODE.Text.Trim(), ddlCSTATUS.SelectedValue.Trim(), txtDINDATEFrom.Text.Trim(), txtDINDATETo.Text.Trim(), CurrendIndex, PageSize, out pageCount);

        AspNetPager1.CustomInfoHTML = " " + Resources.Lang.WMS_Common_Pager_PageCount + ":<b>" + "</b>";//总页数
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;
       


        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("cstatus", "cstatusname", "STOCK_CURRENT_ADJUST"));//狀態     
        var srcdata = GetGridDataByDataTable(dtSource, flagList);       
        this.grdSTOCK_ADJUST.DataSource = srcdata;
        this.grdSTOCK_ADJUST.DataBind();
    }        

    public bool CheckData()
    {
        if(this.txtCTICKETCODE.Text.Trim().Length > 0)
        {
        }
        if(this.txtCASNID.Text.Trim().Length > 0)
        {
        }
        if(this.txtDINDATEFrom.Text.Trim().Length > 0)
        {
        	if(this.txtDINDATEFrom.Text.IsDate()== false)
        	{
                this.Alert(Resources.Lang.FrmSTOCK_ADJUSTList_RuKuRiQiWuXiao);//入库日期项不是有效的日期！
        		this.SetFocus(txtDINDATEFrom);
        		return false;
        	}
        }
        if(this.txtDINDATETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_ToItemEmpty);//到项不允许空！
        	this.SetFocus(txtDINDATETo);
        	return false;
        }
        if(this.txtDINDATETo.Text.Trim().Length > 0)
        {
        	if(this.txtDINDATETo.Text.IsDate()== false)
        	{
                this.Alert(Resources.Lang.FrmOUTBILLList_Tips_ChuKuDaoWuXiao);//到项不是有效的日期！
        		this.SetFocus(txtDINDATETo);
        		return false;
        	}
        }
        return true;

    }

    #endregion

    #region IPage 成员
   
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdSTOCK_ADJUST.DataKeyNames = new string[]{"ID"};
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "STOCK_CURRENT_ADJUST", false, -1, -1), this.ddlCSTATUS,Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//全部
        
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmSTOCK_ADJUSTEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmSTOCK_ADJUSTList_NewKuCunTiaoZheng + "','STOCK_ADJUST');return false;";//新建库存调整单
             
    }

    #endregion 
    /// <summary>
    /// 确认按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        if (SelectIds != null)
        {
            SelectIds.Clear();
        }
        if (SelectIds == null)
        {
            SelectIds = new List<string>();
        }
       //只有未处理的且有明细的才可以进行确认
        try
        {
            for (int i = 0; i < this.grdSTOCK_ADJUST.Rows.Count; i++)
            {
                if (this.grdSTOCK_ADJUST.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_ADJUST.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {                    
                        //主键赋值
                        var id = this.grdSTOCK_ADJUST.DataKeys[i].Values[0].ToString();
                        var canConfirm = listQuery.CanStockDoAction(id, "1");
                        if (canConfirm == "OK")
                        {
                            SelectIds.Add(id);                         
                        }
                        else
                        {
                            msg = canConfirm;
                            break;
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {               
                foreach (var vid in SelectIds)
                {              
                   listQuery.UpdateStockAdjustData(vid, WmsWebUserInfo.GetCurrentUser().UserNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),"1");
                }
                this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG4);//确认成功！
                this.GridBind();
            }
            else
            {
                this.Alert(msg);
            }           
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_ConfirmFail + E.Message.ToJsString());//确认失败！
        }
    }
    protected void btnReview_Click(object sender, EventArgs e)
    {
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
        //只有确认状态的才可以做审核
        try
        {
            for (int i = 0; i < this.grdSTOCK_ADJUST.Rows.Count; i++)
            {
                if (this.grdSTOCK_ADJUST.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_ADJUST.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {  
                        //主键赋值
                        var id = this.grdSTOCK_ADJUST.DataKeys[i].Values[0].ToString();
                        var canConfirm = listQuery.CanStockDoAction(id, "2");
                        if (canConfirm == "OK")
                        {
                            SelectIds.Add(id);
                        }
                        else
                        {
                            msg = canConfirm;
                            break;
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                foreach (var vid in SelectIds)
                {
                    //listQuery.UpdateStockAdjustData(vid, userNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "2");
                    Proc_ReviewStockAdjust proc = new Proc_ReviewStockAdjust();
                    proc.P_ID = vid;
                    proc.P_UserNo = userNo;
                    proc.Execute();
                    return_value = proc.P_Return_Value;
                    error_value = proc.P_Error_Value;
                    if (return_value == "1")
                    {
                        this.Alert(Resources.Lang.WMS_Common_Msg_Failed + error_value.ToJsString());//审核失败！
                        return;
                    }
                    else
                    {
                        this.Alert(Resources.Lang.FrmALLOCATEEdit_D_Msg01);//审核成功！
                        this.GridBind();
                    }
                }               
            }
            else
            {
                this.Alert(msg);
            }
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_Failed + E.Message.ToJsString());//审核失败！
        }
    }
    
    /// <summary>
    /// 查询事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //CurrendIndex = 1;
        //AspNetPager1.CurrentPageIndex = 1;       
        //this.GridBind();
        //BUCKINGHA-894
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
        }
        this.GridBind();
        IsFirstPage = true;//恢复默认值
        //BUCKINGHA-894
    }  
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void btnDelete_Click(object sender,EventArgs e)
    {
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
    	    for (int i = 0; i < this.grdSTOCK_ADJUST.Rows.Count; i++) 
    	    { 
    	        if (this.grdSTOCK_ADJUST.Rows[i].Cells[0].Controls[1] is CheckBox) 
    	        { 
    	            CheckBox chkSelect = (CheckBox)this.grdSTOCK_ADJUST.Rows[i].Cells[0].Controls[1]; 
    	            if (chkSelect.Checked) 
    	            {
                        //主键赋值
                        var id = this.grdSTOCK_ADJUST.DataKeys[i].Values[0].ToString();
                        var canConfirm = listQuery.CanStockDoAction(id, "0");
                        if (canConfirm == "OK")
                        {
                            SelectIds.Add(id);
                        }
                        else
                        {
                            msg = canConfirm;
                            break;
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                IGenericRepository<STOCK_CURRENT_ADJUST> conn = new GenericRepository<STOCK_CURRENT_ADJUST>(db);
                STOCK_CURRENT_ADJUST entity = new STOCK_CURRENT_ADJUST();
                foreach (var vid in SelectIds)
                {
                    entity.id = vid;
                    conn.Delete(entity.id);	//执行动作   
                    conn.Save();
                }
                this.Alert(Resources.Lang.WMS_Common_Msg_DeleteSuccess);//删除成功！
                this.GridBind();
            }
            else
            {
                this.Alert(msg);
            }
    	} 
    	catch(Exception E) 
    	{
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + E.Message.ToJsString());//删除失败！
    	}
    } 
    protected void grdSTOCK_ADJUST_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.grdSTOCK_ADJUST.DataKeys[e.Row.RowIndex].Values[0].ToString();
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
            linkModify.ID = "linkModify";
        	linkModify.NavigateUrl = "#";          
            HyperLink linkView = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkView.NavigateUrl = "#";
            this.OpenFloatWinMax(linkView, BuildRequestPageURL("FrmSTOCK_ADJUSTEdit.aspx", SYSOperation.View, strKeyID), Resources.Lang.WMS_Common_Element_StockTiaoZheng, "STOCK_ADJUST");//库存调整单

            //switch (e.Row.Cells[e.Row.Cells.Count - 3].Text)
            //{
            //    case "0":
            //        e.Row.Cells[e.Row.Cells.Count - 3].Text = Resources.Lang.FrmINASNList_MSG15;// "未处理";
            //        break;
            //    case "1":
            //        e.Row.Cells[e.Row.Cells.Count - 3].Text = Resources.Lang.FrmALLOCATE_DReport_MSG13;// "已确认";
            //        break;
            //    case "2": e.Row.Cells[e.Row.Cells.Count - 3].Text = Resources.Lang.FrmALLOCATE_DReport_MSG14;// "已审核";

            //        break;  
            //}
            string userNo = WmsWebUserInfo.GetCurrentUser().UserNo;
            string currentUrl = Request.Url.AbsoluteUri.ToLower();//完整路径
            List<V_GetBtnFunctionListEntity> allButtonList = Session["ButtonList"] as List<V_GetBtnFunctionListEntity>;
            //List<V_GetBtnFunctionListEntity> currentButtonList = null;
            if (allButtonList != null)
            {
                CurrentButtonList = allButtonList.FindAll(x => currentUrl.Contains(x.F_Data.ToLower().Trim()));               
            }
            bool bl = CurrentButtonList.Exists(x => !string.IsNullOrEmpty(x.F_Code) && x.F_Code.ToLower().Trim().Contains(linkModify.ID.ToLower()));
            STOCK_CURRENT_ADJUST modStock = context.STOCK_CURRENT_ADJUST.Where(x => x.id == strKeyID).FirstOrDefault();
            if (!bl)
            {
                if (modStock.cstatus != "0")
                {
                    linkModify.Enabled = false;
                }
                else
                {
                    linkModify.Enabled = true;
                    this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmSTOCK_ADJUSTEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Element_StockTiaoZheng, "STOCK_ADJUST");//库存调整单
                }
            }
            else
            {
                if (modStock.cstatus != "2")
                {
                    linkModify.Enabled = true;
                    this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmSTOCK_ADJUSTEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Element_StockTiaoZheng, "STOCK_ADJUST");//库存调整单
                }
                else
                {
                    linkModify.Enabled = false;
                }
            }
            //var functions = context.UserFunction.Where(x => x.userno == userNo && x.funcno == "6807").ToList();  //6807
            //STOCK_CURRENT_ADJUST modStock = context.STOCK_CURRENT_ADJUST.Where(x => x.id == strKeyID).FirstOrDefault();
            //    if (!functions.Any())
            //    {
            //        if (modStock.cstatus != "0")
            //        {
            //            linkModify.Enabled = false;
            //        }
            //        else
            //        {
            //            linkModify.Enabled = true;
            //            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmSTOCK_ADJUSTEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Element_StockTiaoZheng, "STOCK_ADJUST");//库存调整单
            //        }
            //    }
            //    else
            //    {
            //        if (modStock.cstatus != "2")
            //        {
            //            linkModify.Enabled = true;
            //            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmSTOCK_ADJUSTEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Element_StockTiaoZheng, "STOCK_ADJUST");//库存调整单
            //        }
            //        else
            //        {
            //            linkModify.Enabled = false;
            //        }
            //    }
        }
    } 
}

