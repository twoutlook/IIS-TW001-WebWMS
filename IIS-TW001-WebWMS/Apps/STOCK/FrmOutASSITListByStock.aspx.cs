using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using DreamTek.ASRS.Business.SP.ProcedureModel;


/// <summary>
/// 描述: 入库管理-->FrmINASSITList 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:52:03
/// </summary>
/*
Roger
2013/7/15 16:05:20
20130715160520
增加UserNo传入
*/
public partial class FrmOutASSITListByStock : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        using (var modContext = context)
        {
            var queryList = from p in modContext.V_STOCK_OCCUPY
                            select p;
            if (!string.IsNullOrEmpty(StrCinvcode))
            {
                queryList = queryList.Where(x => x.cinvcode == StrCinvcode);
            }
            if (!string.IsNullOrEmpty(StrCpositionCode))
            {
                queryList = queryList.Where(x => x.cpositioncode == StrCpositionCode);
            }

            queryList = queryList.OrderBy(x => x.cticketcode);

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;

            this.grdINASSIT.DataSource = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            this.grdINASSIT.DataBind();
        }
    }

    public bool CheckData()
    {
        if (this.txtCASNID.Text.Trim().Length > 0)
        {
        }
        if (this.txtCTICKETCODE.Text.Trim().Length > 0)
        {
        }
        if (this.ddlCSTATUS.SelectedValue.Trim().Length > 0)
        {
        }
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_ZhiDanRiError);//制单日期项不是有效的日期！
                this.SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        if (this.txtDCREATETIMETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_NeedDao);//到项不允许空！
            this.SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (this.txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_DaoWuXiao);//到项不是有效的日期！
                this.SetFocus(txtDCREATETIMETo);
                return false;
            }
        }
        return true;

    }

    #endregion

    #region IPage 成员


    public void InitPage()
    {
        GetCinvCPosition();
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //this.grdINASSIT.DataKeyNames = new string[] { "ID,itype,CDEFINE1,CDEFINE2" };

        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmINASSITEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmOutASSITListByStock_NewInAssit + "','INASSIT');return false;";//新建上架指引单
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OutASSITListByStock');return false;";
        Session["StockCurrentIndex"] = Request["StockCurrentIndex"] != null ? Request["StockCurrentIndex"].ToString() : string.Empty;
    }

    #endregion

    /// <summary>
    /// 料号
    /// </summary>
    public string StrCinvcode
    {
        get
        {
            if (ViewState["StrCinvcode"] != null)
            {
                return ViewState["StrCinvcode"].ToString();
            }
            return "";
        }
        set { ViewState["StrCinvcode"] = value; }
    }
    /// <summary>
    /// 储位
    /// </summary>
    public string StrCpositionCode
    {
        get
        {
            if (ViewState["StrCpositionCode"] != null)
            {
                return ViewState["StrCpositionCode"].ToString();
            }
            return "";
        }
        set { ViewState["StrCpositionCode"] = value; }
    }

    /// 获取料号和储位
    /// <summary>
    /// 获取料号和储位
    /// </summary>
    public void GetCinvCPosition()
    {
        if (Request.QueryString["ID"] != null)
        {
            string id = Request.QueryString["ID"].ToString();
            var stock_current = context.STOCK_CURRENT.Where(x => x.id == id).FirstOrDefault();
            if (stock_current != null)
            {
                StrCinvcode = stock_current.cinvcode;
                StrCpositionCode = stock_current.cpositioncode;
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        this.GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = 1;
        this.GridBind();
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorINASSIT
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        //DBUtil.BeginTrans(); 
        try
        {
            //for (int i = 0; i < this.grdINASSIT.Rows.Count; i++) 
            //{
            //    if (this.grdINASSIT.Rows[i].FindControl("chkSelect") is CheckBox) 
            //    { 
            //        CheckBox chkSelect = (CheckBox)this.grdINASSIT.Rows[i].FindControl("chkSelect");
            //        if (chkSelect.Checked) 
            //        {
            //            //Proc_DeleteOutAssit proc = new Proc_DeleteOutAssit();
            //            //proc.P_OutAssit_Id = this.grdINASSIT.DataKeys[i].Values[0].ToString();
            //            //proc.Execute();
            //            //if (proc.P_ReturnValue == 1)
            //            //{
            //            //    msg += proc.INFOTEXT;
            //            //}

            //            //20130409154954
            //            if (this.grdINASSIT.Rows[i].Cells[2].Text.Trim().Substring(0, 4).ToUpper() == "ALLO")
            //            {
            //                msg += this.grdINASSIT.Rows[i].Cells[2].Text.Trim() + "不允许删除";
            //                continue;
            //            }

            //            Proc_RELEASE_STOCK proc = new Proc_RELEASE_STOCK();
            //            proc.vGuideId = this.grdINASSIT.DataKeys[i].Values[0].ToString();
            //            proc.vStorage = StrCpositionCode;
            //            proc.vItemId = StrCinvcode;
            //            //20130715160520
            //            proc.pUserNo = WebUserInfo.GetCurrentUser().UserNo;
            //            proc.Execute();
            //            if (!proc.vReturnMsg.ToUpper().Equals("OK".ToUpper()))
            //            {
            //                msg += proc.vReturnMsg;
            //            }
            //        }                    
            //    } 
            //}
            //if (msg.Length == 0)
            //{
            //    msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;//删除成功！
            //}            
            //DBUtil.Commit(); 
        }
        catch (Exception E)
        {
            //msg += Resources.Lang.WMS_Common_Msg_DeleteFailed;//删除失败！
            //DBUtil.Rollback(); 
        }
        this.btnSearch_Click(sender, e);
        this.Alert(msg);
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdINASSIT.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdINASSIT.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdINASSIT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text.Trim().Substring(0, 4).ToUpper() == "ALLO")
            {
                ((TextBox)e.Row.FindControl("txtSfl")).Enabled = false;
                ((Button)e.Row.FindControl("btnSf")).Enabled = false;
            }
        }
    }


    protected void ReleaseStock(object sender, EventArgs e)
    {
        try
        {
            string ReleaseQty = string.Empty;
            string IoccupyQty = string.Empty;
            string CID = (sender as Button).CommandArgument.Trim();

            foreach (GridViewRow row in grdINASSIT.Rows)
            {
                if (grdINASSIT.DataKeys[row.RowIndex][0].ToString() == CID)
                {
                    ReleaseQty = ((TextBox)row.FindControl("txtSfl")).Text.Trim();
                    IoccupyQty = row.Cells[4].Text.Trim();
                    break;
                }
            }

            //判断是否输入为空
            if (string.IsNullOrEmpty(ReleaseQty))
            {
                throw new Exception(Resources.Lang.FrmOutASSITListByStock_NeedShiFangQty);//请输入释放量！
            }

            //判断输入数量是否大于0
            if (decimal.Parse(ReleaseQty) < 0)
            {
                throw new Exception(Resources.Lang.FrmOutASSITListByStock_ShiFangQtyError);//请输入正确的释放量！
            }

            //判断释放量是否大于占有量
            if (decimal.Parse(ReleaseQty) > decimal.Parse(IoccupyQty))
            {
                throw new Exception(Resources.Lang.FrmOutASSITListByStock_ShiFangQtyXiaoYuZhanYong);//释放量需小于占有量！
            }

            var proc = new PROC_RELEASE_STOCK_OCCUPANCY();
            proc.pId = CID;
            proc.pStorage = StrCpositionCode;
            proc.pItemId = StrCinvcode;
            proc.pReleaseQty = Convert.ToDecimal(ReleaseQty);
            proc.pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
            proc.Execute();
            if (proc.ReturnValue != 0)
            {
                Alert(proc.ErrorMessage);
            }
            else
            {
                btnSearch_Click(null, null);
                Alert(proc.ErrorMessage);
            }
        }
        catch (Exception ex)
        {
            Alert(ex.Message);
        }
    }

    protected void dsGrdINASSIT_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdINASSIT_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }

    /// <summary>
    /// 释放占用储位
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
}

