using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Business.RD;
using DreamTek.ASRS.Business.SP.ProcedureModel;

/// <summary>
/// 描述: 入库管理-->FrmINBILLList 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:57:39
/// 
/*
 * Roger
 * 2013/5/14 14:12:18
 * 20130514141218
 * 扣帐增加权限限制
 */
/*
Roger
2013/7/2 8:44:29
20130702084429
解决狂点按钮重复提交问题
*/
/// </summary>
public partial class RD_FrmINBILLList :WMSBasePage//  PageBase, IPageGrid
{
    #region SQL
    //DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                this.InitPage();
                //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
            //    this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            //    this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        catch (Exception)
        {

        }
        //20130702084429
        this.btnUpdateSTOCK.Attributes["onclick"] = this.GetPostBackEventReference(this.btnUpdateSTOCK) + ";this.disabled=true;";
    }


    #region IPageGrid 成员

    public void GridBind()
    {
        string userNo = WmsWebUserInfo.GetCurrentUser().UserNo;
        int pageCount = 0;
        INQuery qry = new INQuery();
        var worktype = this.ddlWorkType.SelectedValue!=null? this.ddlWorkType.SelectedValue.Trim():string.Empty;
        DataTable dt = qry.InBillQuery(txtCTICKETCODE.Text.Trim(), txtCinvcode.Text,txtRank_Final.Text, txtCCREATEOWNERCODE.Text.Trim(),
            txtCAUDITPERSON.Text.Trim(), txtCASN_No.Text.Trim(), txtErp_No.Text,
            txtDCREATETIMEFrom.Text.Trim(), txtDCREATETIMETo.Text.Trim(), txtDINDATEFrom.Text.Trim(),
            txtDINDATETo.Text.Trim(), txtDAUDITTIMEFrom.Text, txtDAUDITTIMETo.Text.Trim(), ddlStatus.SelectedValue, ddlInType.SelectedValue, 
            txtPalletCode.Text.Trim(),
            worktype, drpOperationType.SelectedValue,txtcspec.Text.Trim(),userNo, CurrendIndex, PageSize, out pageCount
            );
      
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;
        grdINBILL.DataSource = dt;
        grdINBILL.DataBind();
    }

    public bool CheckData()
    {
        if (this.txtCTICKETCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDINDATEFrom.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate(this.txtDINDATEFrom.Text)== false)
            {
                //入库日期项不是有效的日期
                this.Alert(Resources.Lang.FrmINBILLList_MSG1+ "！");
                this.SetFocus(txtDINDATEFrom);
                return false;
            }
        }
        if (this.txtDINDATETo.Text.Trim() == "")
        {
            //到项不允许空
            this.Alert(Resources.Lang.FrmINBILLList_MSG2 + "！");
            this.SetFocus(txtDINDATETo);
            return false;
        }
        if (this.txtDINDATETo.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate(this.txtDINDATETo.Text) == false)
            {
                //到项不是有效的日期
                this.Alert(Resources.Lang.FrmINBILLList_MSG3 + "！");
                this.SetFocus(txtDINDATETo);
                return false;
            }
        }
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate(this.txtDCREATETIMEFrom.Text) == false)
            {
                //制单日期项不是有效的日期
                this.Alert(Resources.Lang.FrmINBILLList_MSG4 + "！");
                this.SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        if (this.txtDCREATETIMETo.Text.Trim() == "")
        {
            //到项不允许空
            this.Alert(Resources.Lang.FrmINBILLList_MSG2 + "！");
            this.SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (this.txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate( this.txtDCREATETIMETo.Text) == false)
            {
                //到项不是有效的日期
                this.Alert(Resources.Lang.FrmINBILLList_MSG3 + "！");
                this.SetFocus(txtDCREATETIMETo);
                return false;
            }
        }
        if (this.txtCAUDITPERSON.Text.Trim().Length > 0)
        {
        }
        if (this.txtDAUDITTIMEFrom.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate( this.txtDAUDITTIMEFrom.Text)== false)
            {
                //审核日期项不是有效的日期
                this.Alert(Resources.Lang.FrmINBILLList_MSG5 + "！");
                this.SetFocus(txtDAUDITTIMEFrom);
                return false;
            }
        }
        if (this.txtDAUDITTIMETo.Text.Trim() == "")
        {
            //到项不允许空
            this.Alert(Resources.Lang.FrmINBILLList_MSG2 + "！");
            this.SetFocus(txtDAUDITTIMETo);
            return false;
        }
        if (this.txtDAUDITTIMETo.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate( this.txtDAUDITTIMETo.Text) == false)
            {
                //到项不是有效的日期
                this.Alert(Resources.Lang.FrmINBILLList_MSG3 + "！");
                this.SetFocus(txtDAUDITTIMETo);
                return false;
            }
        }
        return true;

    }

    /// <summary>
    /// 入库单状态
    /// </summary>
    public DataTable InBillStatus
    {
        get { return ViewState["InBillStatus"] as DataTable; }
        set { ViewState["InBillStatus"] = value; }
    }
    #endregion

    #region IPage 成员

    public void InitPage()
    {
        try
        {
            this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
            this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
            this.grdINBILL.DataKeyNames = new string[] { "ID", "CTICKETCODE" };
            #region Disable and ReadOnly
            #endregion Disable and ReadOnly
            //本页面打开新增窗口
            this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmINBILLEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmINBILLList_MSG6 +"','INBILL');return false;";
           
            //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmINBILLEdit.aspx", SysOperation.New,""),800,600);
            Help.DropDownListDataBind(GetInType(true), this.ddlInType,  Resources.Lang.Common_ALL , "FUNCNAME", "EXTEND1", "");
            //InBillStatus = new SysParameterListQuery().LoadStatusByFlag_type("INBILL");
            Help.DropDownListDataBind(GetParametersByFlagType("INBILL"), this.ddlStatus, Resources.Lang.Common_ALL, "flag_name", "flag_id", "");
            Help.DropDownListDataBind(GetParametersByFlagType("WorkType"), this.ddlWorkType, Resources.Lang.Common_ALL, "flag_name", "flag_id", "");
            //MonthOrWeek
            Help.RadioButtonDataBind(GetParametersByFlagType("MonthOrWeek"), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");
            //drpOperationType 是否补单
            Help.DropDownListDataBind(GetParametersByFlagType("OperationType"), this.drpOperationType, Resources.Lang.Common_ALL, "flag_name", "flag_id", "");

            if (!string.IsNullOrEmpty(Request.QueryString["Cstatus"]))
            {
                this.ddlStatus.SelectedValue = this.Request.QueryString["Cstatus"].ToString();
            }
        }
        catch (Exception)
        {
        }
    }

    #endregion
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
            this.GridBind();
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorINBILL
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        string ReturnValue = string.Empty;
        string errText = string.Empty;
        try
        {
            for (int i = 0; i < this.grdINBILL.Rows.Count; i++)
            {
                if (this.grdINBILL.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdINBILL.Rows[i].Cells[0].Controls[1];
                    // string status = this.grdINBILL.Rows[i].Cells[10].Text;
                    if (chkSelect.Checked)
                    {

                        string bill_ID = this.grdINBILL.DataKeys[i].Values[0].ToString();
                        //var itypeName = this.grdINBILL.Rows[i].Cells[this.grdINBILL.Rows[i].Cells.Count - 9].Text.Trim();
                        var modInBill = context.INBILL.Where(x => x.id == bill_ID).FirstOrDefault();
                        if (modInBill != null)
                        {
                            INTYPE INBILL_type = GetINTYPEByID(bill_ID, "INBILL");
                            if (INBILL_type != null && INBILL_type.Is_Query == 1)
                            {
                                //仅查询类型的单据不允许删除
                                msg = Resources.Lang.FrmINBILLList_MSG7 + "!";
                                break;
                            }
                            else
                            {
                                if (modInBill.worktype == "0" && modInBill.operationtype == 0)//平库，PDA产生的入库单，单独的删除逻辑
                                {
                                    if (modInBill.cstatus == "0")
                                    { //未处理
                                        Proc_DelInBill_D proc = new Proc_DelInBill_D();
                                        proc.P_InBillID = modInBill.id;
                                        proc.P_ASN_ID = modInBill.casnid;
                                        proc.P_UserNO = WmsWebUserInfo.GetCurrentUser().UserNo;
                                        proc.Execute();
                                        errText = proc.P_INFO;
                                        if (proc.P_Return_value != 0)
                                        {
                                            msg += proc.P_INFO;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        //只有状态为[未處理]的单据才能删除
                                        msg = Resources.Lang.FrmINBILLList_MSG8 + "!";
                                        break;
                                    }
                                }

                                else
                                {

                                    //string bill_ID = this.grdINBILL.DataKeys[i].Values[0].ToString();
                                    #region 调用存储过程
                                    List<string> SparaList = new List<string>();
                                    SparaList.Add("@P_Bill_ID:" + bill_ID);
                                    SparaList.Add("@P_Bill_IDS:" + "");
                                    SparaList.Add("@P_BZ:" + "0");
                                    SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                                    SparaList.Add("@P_return_Value:" + "");
                                    SparaList.Add("@errText:" + "");
                                    string[] Result = DBHelp.ExecuteProc("Proc_Delete_WebBill_D", SparaList);
                                    if (Result.Length == 1)//调用存储过程有错误
                                    {
                                        msg += Result[0];
                                    }
                                    else if (Result[0] == "0")
                                    {
                                        //如果为补单，增新
                                    }
                                    else
                                    {
                                        msg += Result[1];
                                    }
                                    #endregion
                                }

                            }

                            #region 注销

                            //if (status.Trim().Equals("未处理")
                            //                   && INBILLRule.GetInBillStatusByInBillId(this.grdINBILL.DataKeys[i].Values[0].ToString().Trim()).Trim().Equals("0")
                            //                   && !INBILL_DRule.ValidateIsExistTemp_InBill_D(this.grdINBILL.Rows[i].Cells[2].Text.ToString().Trim())
                            //                  )
                            //{
                            //    DBUtil.BeginTrans();
                            //    try
                            //    {
                            //        var inbillEntity = new INBILLEntity().GetINBILLEntityById(this.grdINBILL.DataKeys[i].Values[0].ToString());
                            //        //特殊wip return
                            //        var isSpecialReturn = INASNRule.isSpecialWipReturn(inbillEntity.CASNID);
                            //        if (isSpecialReturn)
                            //        {
                            //            DBUtil.Rollback();
                            //            Alert("[" + this.grdINBILL.Rows[i].Cells[2].Text.Trim() + "]整盘退料产生的入库单不允许删除！");
                            //            return;
                            //        }

                            //        INBILLEntity entity = new INBILLEntity();
                            //        //主键赋值
                            //        entity.ID = this.grdINBILL.DataKeys[i].Values[0].ToString();
                            //        INBILLRule.Delete(entity);	//执行动作 
                            //        INBILLRule.UpdateTempInBill_D_ByInBillCticketCode(this.grdINBILL.Rows[i].Cells[2].Text.Trim(), "0");
                            //        DBUtil.Commit();
                            //    }
                            //    catch (Exception)
                            //    {
                            //        DBUtil.Rollback();
                            //        msg += "[" + this.grdINBILL.Rows[i].Cells[2].Text.Trim() + "]单号删除失败！\r\n";
                            //        //throw;
                            //    }
                            //}
                            //else
                            //{
                            //    //msg += "[" + this.grdINBILL.Rows[i].Cells[2].Text + "]单号已扣账不能删除!\r\n";
                            //    msg = "只有状态为[未處理]的且不是PDA生成的单据才能删除!";
                            //    break;
                            //} 

                            #endregion
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                //删除成功
                msg = Resources.Lang.CommonB_RemoveSuccess + "！";
            }

            //DBUtil.Commit(); 
            btnSearch_Click(null, null);
        }
        catch (Exception E)
        {
            //this.Alert("删除失败！" + E.Message.ToJsString()); 
            //删除失败
            msg += Resources.Lang.CommonB_RemoveFailed + "!";
            //DBUtil.Rollback(); 
        }
        this.Alert(msg);
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        try
        {
            for (int i = 0; i < this.grdINBILL.DataKeyNames.Length; i++)
            {
                strKeyId = this.grdINBILL.DataKeys[rowIndex].Values[0].ToString() + ",";
            }
            strKeyId = this.grdINBILL.DataKeys[rowIndex].Values[0].ToString();//strKeyId.TrimEnd(',');
        }
        catch (Exception)
        {
        }
        return strKeyId;
    }

    protected void grdINBILL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
                HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
                linkModify.NavigateUrl = "#";
                this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmINBILLEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmINBILLList_MSG9, "INBILL");

                HyperLink linkModify_Error = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
                linkModify_Error.NavigateUrl = "#";

                if (new SystemLogs().validateIsExistErrorMsg(strKeyID))
                {
                    linkModify_Error.Style.Add("color", "Red");
                    //异常信息
                    this.OpenFloatWin(linkModify_Error, BuildRequestPageURL("/Apps/SystemLogs/FrmLogSystemList.aspx?TableName=INBILL", SYSOperation.Modify, strKeyID), Resources.Lang.FrmINBILLList_MSG10 , "ErrorMsg", 800, 400);
                }
                else
                {
                    linkModify_Error.Enabled = false;
                }

                //e.Row.Cells[e.Row.Cells.Count - 4].Text = e.Row.Cells[e.Row.Cells.Count - 4].Text.Trim() == "0" ? "未处理" : "已完成";
                CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;
                //InBillStatus
                //e.Row.Cells[e.Row.Cells.Count - 5].Text = InBillStatus.Select("flag_id = '" + e.Row.Cells[e.Row.Cells.Count - 5].Text.Trim() + "'")[0]["flag_name"].ToString();

                //获取ID
                string id = this.grdINBILL.DataKeys[e.Row.RowIndex][0].ToString();
                //判断是否已在SelectIds集合中
                if (SelectIds.ContainsKey(id))
                {
                    //如果是控件处于选中状态
                    chkSelect.Checked = true;
                }
            }
        }
        catch (Exception)
        {
        }
    }

   

  
    /// <summary>
    /// 更新库存表数量
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {

            #region 调用存储过程
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_InBillId:" + (sender as LinkButton).CommandArgument.Trim());
            SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo.Trim());
            string Result = DBHelp.ExecuteProcReturnValue("Proc_InBillTOSTOCK_CURRENT", SparaList, "@P_ReturnValue");
            if (Result== "0")
            {
                //扣账成功
                Alert(Resources.Lang.FrmINBILLList_MSG11 + "!");
            }
            else{
                //扣账失败
                Alert(Resources.Lang.FrmINBILLList_MSG12 + "!");
            }
            #endregion
        }
        catch (Exception)
        {

        }
    }

    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }

    /// <summary>
    /// 获取选择的ID
    /// </summary>
    public void GetSelectedIds()
    {
        try
        {
            if (SelectIds == null)
            {
                SelectIds = new Dictionary<string, string>();
            }

            foreach (GridViewRow item in this.grdINBILL.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    CheckBox cbo = itemFindControl as CheckBox;

                    //Product product = item.DataItem as Product;
                    //获取ID
                    string id = this.grdINBILL.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                    //控件选中且集合中不存在添加
                    if (cbo.Enabled && cbo.Checked && !SelectIds.ContainsKey(id))
                    {
                        SelectIds.Add(id, this.grdINBILL.DataKeys[item.RowIndex][1].ToString());
                    }//未选中且集合中存在的移除                    
                    else if (!cbo.Checked && SelectIds.ContainsKey(id))
                    {
                        SelectIds.Remove(id);
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    /// <summary>
    /// 扣账按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdateSTOCK_Click(object sender, EventArgs e)
    {
        try
        {
            btnUpdateSTOCK.Enabled = false;//Roger 2013-4-24 18:33:44
            GetSelectedIds();
            if (SelectIds != null && SelectIds.Count > 0)
            {
                this.Alert(InBill.BatchInBillTOStock_Currnt_Proc(SelectIds, WmsWebUserInfo.GetCurrentUser().UserNo));
                this.btnSearch_Click(sender, e);
                this.SelectIds.Clear();
            }
            else
            {
                //请选择你要操作的入库单
                Alert(Resources.Lang.FrmINBILLList_MSG14 + "!");
            }
            btnUpdateSTOCK.Enabled = true;//Roger 2013-4-24 18:33:44
        }
        catch (Exception)
        {
            btnUpdateSTOCK.Enabled = true;//Roger 2013-4-24 18:33:44
        }
        finally
        {
            //20130702084429
            btnUpdateSTOCK.Style.Remove("disabled");
        }

    }

    /// <summary>
    /// 数据邦定前 获取上一页已选中的行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdINBILL_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }
    #endregion

}

