using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Text;
using System.ComponentModel;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using DreamTek.ASRS.Business.Allocate;
using Resources;
using DreamTek.ASRS.Business;

/// <summary>
/// 描述: 1111-->FrmALLOCATEList 页面后台类文件
/// 作者: --chenlei
/// 创建于: 2012-11-01 09:31:05
/// 
/// Modify by Roger
/// Time: 2013-4-10 11:05:26
/// Mark: 20130410110526
/// Reason: 增加功能点限制
/// 
/// Modify by Roger
/// Time: 2013/5/9 10:46:06
/// Mark: 20130509104606
/// Reason: 增加操作人员传入
/// </summary>
/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/
/*
Roger
2013/7/2 8:44:29
20130702084429
解决狂点按钮重复提交问题
*/
public partial class ALLOCATE_FrmALLOCATEList : WMSBasePage
{
    public string returnErrorMsg;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack == false)
        {
            this.InitPage();
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
            //txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");           
            this.ddlCSTATUS.SelectedValue = BaseStatus.UnDone;
            //this.btnSearch_Click(null, null);           
        }

        this.btnUpdateSTOCK.Attributes["onclick"] = this.GetPostBackEventReference(this.btnUpdateSTOCK) + ";this.disabled=true;";
        btnDelete.Attributes.Add("onclick", this.GetPostBackEventReference(btnDelete) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        btnCreateInOutBill.Attributes.Add("onclick", this.GetPostBackEventReference(btnCreateInOutBill) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        btnYesNo.Attributes.Add("onclick", this.GetPostBackEventReference(btnYesNo) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        btnSendMail.Attributes.Add("onclick", this.GetPostBackEventReference(btnSendMail) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
    }


    #region IPageGrid 成员

    public void Bind(string sortStr)
    {
        #region //21-10-2020 by Qamar
        int pageCount = 0;
        AllocateQuery query = new AllocateQuery();
        DataTable dt;
        if (txtRANK_FINAL.Text.Trim().Length == 1)
            dt = query.GetAllocateListIncludingRank(txtCCREATEOWNERCODE.Text.Trim(), txtDCREATETIMEFrom.Text.Trim(), txtDCREATETIMETo.Text.Trim(), txtCAUDITPERSON.Text.Trim(),
             txtDAUDITTIMEFrom.Text.Trim(), txtDAUDITTIMETo.Text.Trim(), txtCTICKETCODE.Text.Trim(), txtDINDATEFrom.Text.Trim(), txtDINDATETo.Text.Trim(), ddlCSTATUS.SelectedValue, txtERP.Text.Trim(), txtLH.Text.Trim(), txtRANK_FINAL.Text.Trim().ToUpper(), ddlAllocateType.SelectedValue, ddlcdefine1.SelectedValue, txtPalletCode.Text.Trim(), txtcspec.Text.Trim(), CurrendIndex, PageSize, out pageCount);
        else
            dt = query.GetAllocateList(txtCCREATEOWNERCODE.Text.Trim(), txtDCREATETIMEFrom.Text.Trim(), txtDCREATETIMETo.Text.Trim(), txtCAUDITPERSON.Text.Trim(),
             txtDAUDITTIMEFrom.Text.Trim(), txtDAUDITTIMETo.Text.Trim(), txtCTICKETCODE.Text.Trim(), txtDINDATEFrom.Text.Trim(), txtDINDATETo.Text.Trim(), ddlCSTATUS.SelectedValue, txtERP.Text.Trim(), txtLH.Text.Trim(), ddlAllocateType.SelectedValue, ddlcdefine1.SelectedValue, txtPalletCode.Text.Trim(), txtcspec.Text.Trim(), CurrendIndex, PageSize, out pageCount);
        #endregion

        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("cstatus", "newcstatus", "ALLOCATE"));//狀態
        flagList.Add(new Tuple<string, string, string>("ALLOTYPE", "newallotype", "AllocateType"));//业务类型
        flagList.Add(new Tuple<string, string, string>("cdefine1", "newcdefine1", "AllocateTypeInOROut"));//調撥單出入or 返庫類型
        var srcdata = GetGridDataByDataTable(dt, flagList);

        AspNetPager1.CustomInfoHTML = Lang.FrmALLOCATEList_TotalPages + ":<b>" + "</b>";
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;
        grdALLOCATE.DataSource = srcdata;
        grdALLOCATE.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    public void GridBind()
    {
        Bind("");
    }

    public bool CheckData()
    {
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMEFrom.Text.IsDate() == false)
            {
                this.Alert(Lang.FrmALLOCATEList_NoEfftxtDCREATETIMEFrom);//"制单日期项不是有效的日期！"
                this.SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        if (this.txtDCREATETIMETo.Text.Trim() == "")
        {
            this.Alert(Lang.FrmALLOCATEList_ToItemEmpty);
            this.SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (this.txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMETo.Text.IsDate() == false)
            {
                this.Alert(Lang.FrmALLOCATEList_NoEffToItem);
                this.SetFocus(txtDCREATETIMETo);
                return false;
            }
        }
        if (this.txtCAUDITPERSON.Text.Trim().Length > 0)
        {
        }
        if (this.txtDAUDITTIMEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDAUDITTIMEFrom.Text.IsDate() == false)
            {
                this.Alert(Lang.FrmALLOCATEList_NoEfftxtDAUDITTIMEFrom);
                this.SetFocus(txtDAUDITTIMEFrom);
                return false;
            }
        }
        if (this.txtDAUDITTIMETo.Text.Trim() == "")
        {
            this.Alert(Lang.FrmALLOCATEList_ToItemEmpty);
            this.SetFocus(txtDAUDITTIMETo);
            return false;
        }
        if (this.txtDAUDITTIMETo.Text.Trim().Length > 0)
        {
            if (this.txtDAUDITTIMETo.Text.IsDate() == false)
            {
                this.Alert(Lang.FrmALLOCATEList_NoEffToItem);
                this.SetFocus(txtDAUDITTIMETo);
                return false;
            }
        }
        if (this.txtCTICKETCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDINDATEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDINDATEFrom.Text.IsDate() == false)
            {
                this.Alert(Lang.FrmALLOCATEList_NoEfftxtDINDATEFrom);
                this.SetFocus(txtDINDATEFrom);
                return false;
            }
        }
        if (this.txtDINDATETo.Text.Trim() == "")
        {
            this.Alert(Lang.FrmALLOCATEList_ToItemEmpty);
            this.SetFocus(txtDINDATETo);
            return false;
        }
        if (this.txtDINDATETo.Text.Trim().Length > 0)
        {
            if (this.txtDINDATETo.Text.IsDate() == false)
            {
                this.Alert(Lang.FrmALLOCATEList_NoEffToItem);
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
        //this.grdALLOCATE.DataKeyNames = new string[] { "ID" };     
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmALLOCATEEdit.aspx", SYSOperation.New, "") + "','" + Lang.FrmALLOCATEList_AddAllocate + "','ALLOCATE');return false;";//新建调拨单
        Help.RadioButtonDataBind(SysParameterList.GetList("", "", "MonthOrWeek", false, -1, -1), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");//BUCKINGHA-894
        //调拨单状态
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "ALLOCATE", false, -1, -1), this.ddlCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        //调拨单
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "AllocateTypeInOROut", false, -1, -1), this.ddlcdefine1, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        //業務類型 
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "AllocateType", false, -1, -1), this.ddlAllocateType, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
    }

    #endregion
    protected void grdALLOCATE_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdALLOCATE_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //BUCKINGHA-894
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
        }
        this.Bind("");
        IsFirstPage = true;//恢复默认值
        //BUCKINGHA-894
        //CurrendIndex = 1;
        //AspNetPager1.CurrentPageIndex = 1;
        //Bind("");

    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorALLOCATE
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        //DBUtil.BeginTrans();
        IGenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
        IGenericRepository<ALLOCATE_D> connD = new GenericRepository<ALLOCATE_D>(db);
        AllocateQuery allQry = new AllocateQuery();
        string alloId = string.Empty;
        try
        {
            for (int i = 0; i < this.grdALLOCATE.Rows.Count; i++)
            {
                if (this.grdALLOCATE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdALLOCATE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        var allocatetype = this.grdALLOCATE.DataKeys[i].Values[1].ToString().Trim();
                        var statusid = this.grdALLOCATE.DataKeys[i].Values[2].ToString().Trim();
                        if (statusid == "0") // 0: 未处理 1:已审核 2:已完成 3:已抛转 4:已确认 5:调拨中 6:调拨完成
                        {
                            alloId = this.grdALLOCATE.DataKeys[i].Values[0].ToString().Trim();
                            ALLOCATE_D dBO = (from p in connD.Get()
                                              where p.id == alloId
                                              select p).FirstOrDefault();
                            if (dBO != null && dBO.asrs_status.HasValue)
                            {
                                if (dBO.asrs_status.Value != 0)
                                {
                                    msg += Lang.FrmALLOCATEList_ExCommondNoDel;
                                    break;
                                }
                            }
                            if (allocatetype != "0") //0:普通调拨  1:出库调拨 2: 返库调拨 3:阻挡移库
                            {
                                msg += Lang.FrmALLOCATEList_NoDelForGeneral;
                                break;
                            }

                        }
                    }
                }
            }
            if (msg.Length != 0)
            {
                this.Alert(msg);
                return;
            }

            for (int i = 0; i < this.grdALLOCATE.Rows.Count; i++)
            {
                if (this.grdALLOCATE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdALLOCATE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        var statusid = this.grdALLOCATE.DataKeys[i].Values[2].ToString().Trim();
                        if (statusid == "0")// 0: 未处理 1:已审核 2:已完成 3:已抛转 4:已确认 5:调拨中 6:调拨完成
                        {
                            string ids = this.grdALLOCATE.DataKeys[i].Values[0].ToString().Trim();
                            ALLOCATE_D dBO = (from p in connD.Get()//  conn.GetByID(this.grdALLOCATE_D.DataKeys[i].Values[0].ToString().Trim());
                                              where p.ids == ids
                                              select p).FirstOrDefault();
                            if (dBO != null && dBO.asrs_status.HasValue)
                            {
                                if (dBO.asrs_status.Value != 0)
                                {
                                    msg += Lang.FrmALLOCATEList_ExCommondNoDel;
                                    break;
                                }
                                else
                                {
                                    //conn.Delete(this.grdALLOCATE.DataKeys[i].Values[0].ToString().Trim());
                                    //conn.Save();
                                }
                            }
                            conn.Delete(this.grdALLOCATE.DataKeys[i].Values[0].ToString().Trim());
                            conn.Save();
                        }
                        else
                        {
                            msg = Lang.FrmALLOCATEList_OnlyWCLCanDel;
                            break;
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Lang.FrmALLOCATEList_DelSuccess;
            }

            //DBUtil.Commit();
            this.GridBind();
        }
        catch (Exception E)
        {
            msg += Lang.FrmALLOCATEList_DelFail;
            //this.Alert(Resources.Lang.Common_FailDel + E.Message.ToJsString()); //删除失败！
            //DBUtil.Rollback();
        }
        this.Alert(msg);
    }

    protected void grdALLOCATE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.grdALLOCATE.DataKeys[e.Row.RowIndex].Values[0].ToString();
            string AllocateType = this.grdALLOCATE.DataKeys[e.Row.RowIndex].Values[1].ToString();//普通调拨、出库调拨等等
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmALLOCATEEdit.aspx?AllocateType=" + AllocateType, SYSOperation.Modify, strKeyID), "" + Lang.FrmALLOCATEList_Title1 + "", "ALLOCATE");

            HyperLink linkModify_P = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify_P.NavigateUrl = "#";


            this.OpenFloatWin(linkModify_P, BuildRequestPageURL("/Apps/ALLOCATE_Report/ALLocate_Print.aspx", SYSOperation.Modify, strKeyID), "" + Lang.FrmALLOCATEList_PrintPage + "", "PrintAllocate", 800, 600);

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
        if (SelectIds == null)
        {
            SelectIds = new Dictionary<string, string>();
        }

        foreach (GridViewRow item in this.grdALLOCATE.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string id = this.grdALLOCATE.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                //控件选中且集合中不存在添加
                if (cbo.Enabled && cbo.Checked && !SelectIds.ContainsKey(id))
                {
                    SelectIds.Add(id, this.grdALLOCATE.DataKeys[item.RowIndex][1].ToString());
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(id))
                {
                    SelectIds.Remove(id);
                }
            }
        }
    }


    /// <summary>
    /// 调拨
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateInOutBill_Click(object sender, EventArgs e)
    {
        //string msg = string.Empty;
        //GetSelectedIds();
        //if (SelectIds != null && SelectIds.Count > 0)
        //{
        //    foreach (var item in SelectIds.Values)
        //    {
        //        Proc_CreateAllocate proc = new Proc_CreateAllocate();
        //        proc.P_Allocate_id = item.Trim();
        //        proc.Execute();
        //        if (proc.P_ReturnValue == 1)
        //        {
        //            msg += "[" + item + "]调拨失败!原因:" + proc.INFOTEXT + ";\r\n";
        //        }
        //    }
        //    if (msg.Length == 0)
        //    {
        //        msg = "调拨成功!";
        //    }
        //    this.btnSearch_Click(sender, e);
        //    Alert(msg);
        //}
        //else
        //{
        //    Alert("请选择你要操作的入库单!");
        //}
    }

    /// 审核
    /// <summary>
    /// 审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        //DBUtil.BeginTrans();
        string msg = string.Empty;
        try
        {
            IGenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
            foreach (GridViewRow item in grdALLOCATE.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    CheckBox cbo = itemFindControl as CheckBox;
                    if (cbo.Checked)
                    {
                        string id = this.grdALLOCATE.DataKeys[item.RowIndex][0].ToString();

                        var bo = (
                                  from p in conn.Get()
                                  where p.id == id
                                  select p
                                  ).FirstOrDefault();
                        if (bo != null && bo.id != null && bo.id.Length > 0)
                        {
                            var statusid = this.grdALLOCATE.DataKeys[item.RowIndex].Values[1].ToString().Trim();
                            if (statusid == "0" && bo.cstatus != null && bo.cstatus.Equals("0"))// 0: 未处理 1:已审核 2:已完成 3:已抛转 4:已确认 5:调拨中 6:调拨完成
                            {
                                //ALLOCATEEntity entity = new ALLOCATEEntity();
                                //entity.ID = id;
                                //if (entity.SelectByPKeys())
                                //{
                                //    entity.CAUDITPERSON = WebUserInfo.GetCurrentUser().UserNo;
                                //    entity.DAUDITTIME = DateTime.Now;
                                //    entity.CSTATUS = "1";
                                //    ALLOCATERule.Apply(entity);
                                //}
                                bo.cauditperson = WmsWebUserInfo.GetCurrentUser().UserNo;
                                bo.daudittime = DateTime.Now;
                                bo.cstatus = "1";
                                conn.Update(bo);
                                conn.Save();
                            }
                            else
                            {
                                //msg = "只有未处理的单据才能审核";
                                throw new Exception(Lang.FrmALLOCATEList_OnlyWCLCanCanReview);
                            }
                        }
                    }
                }

            }
            if (msg.Length == 0)
            {
                msg = Lang.FrmALLOCATEList_ReviewSuccess;
            }
            //DBUtil.Commit();
        }
        catch (Exception err)
        {
            //DBUtil.Rollback();
            //Alert(err.Message.ToJsString());
            msg = err.Message.ToJsString();
        }
        Alert(msg);
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    /// <summary>
    /// 扣账 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdateSTOCK_Click1(object sender, EventArgs e)
    {
        btnUpdateSTOCK.Enabled = false;//Roger 20130425
        try
        {
            //20130410110526
            string msg = string.Empty;

            IGenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.grdALLOCATE.Rows.Count; i++)
            {
                if (this.grdALLOCATE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdALLOCATE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        var allocatetype = this.grdALLOCATE.DataKeys[i].Values[1].ToString().Trim();
                        var ALLOTYPE = this.grdALLOCATE.DataKeys[i].Values[3].ToString().Trim();
                        if (allocatetype != "0")
                        {
                            msg += Lang.FrmALLOCATEList_Title1 + "[" + this.grdALLOCATE.Rows[i].Cells[1].Text + "]" + Lang.FrmALLOCATEList_Msg01;
                            break;
                        }
                        if (ALLOTYPE == "0")  //20190506确认只有平仓可以手动扣账  //this.grdALLOCATE.Rows[i].Cells[9].Text.Equals("立库仓内")   0:立库仓内
                        {
                            msg += Lang.FrmALLOCATEList_Title1 + "[" + this.grdALLOCATE.Rows[i].Cells[1].Text + "]" + Lang.FrmALLOCATEList_Msg02;
                            break;
                        }
                        if (ALLOTYPE == "4")  //20190506确认只有平仓可以手动扣账  //this.grdALLOCATE.Rows[i].Cells[9].Text.Equals("立库仓内")   0:立库仓内
                        {
                            msg += "调拨单" + "[" + this.grdALLOCATE.Rows[i].Cells[1].Text + "]" + "为暂存=>立库调拨单，不能手动扣账!";
                            break;
                        }
                        var alloCdefine1 = this.grdALLOCATE.DataKeys[i].Values[1].ToString().Trim();
                        if (alloCdefine1 == "3")
                        {
                            msg += "调拨单" + "[" + this.grdALLOCATE.Rows[i].Cells[1].Text + "]" + "为阻挡移库调拨单，不能手动扣账!";
                            break;
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                foreach (GridViewRow item in grdALLOCATE.Rows)
                {
                    Control itemFindControl = item.FindControl("chkSelect");
                    if (itemFindControl != null && itemFindControl is CheckBox)
                    {
                        CheckBox cbo = itemFindControl as CheckBox;
                        if (cbo.Checked)
                        {

                            string id = this.grdALLOCATE.DataKeys[item.RowIndex][0].ToString();
                            var bo = (
                                      from p in conn.Get()
                                      where p.id == id
                                      select p
                                      ).FirstOrDefault();
                            if (bo != null && bo.id != null && bo.id.Length > 0)
                            {
                                //如果为平库仓内调拨，不需要判断是否存在已完成的asrs命令
                                if (bo.ALLOTYPE == "1")
                                {
                                    //IGenericRepository<CMD_MST> con_mst = new GenericRepository<CMD_MST>(db);
                                    //var cmdcount = con_mst.Get().Where(x => x.CTICKETCODE.Equals(bo.cticketcode) && x.CmdSts.Equals("7"));
                                    //if (cmdcount.ToList().Count > 0)
                                    //{

                                    //扣账前检查库存
                                    ////检查sn是否满足确认要求
                                    string[] results = CheckAllocateSN(id);
                                    if (results[0] != "0")
                                    {
                                        sb.Append(this.grdALLOCATE.Rows[item.RowIndex].Cells[1].Text + "扣账失败!\r\n" + results[1]);
                                    }
                                    else
                                    {
                                        List<string> paraList = new List<string>();
                                        paraList.Clear();
                                        paraList = new List<string>();
                                        paraList.Add("@P_Allocate_id:" + id);
                                        paraList.Add("@P_Occup:" + "0");
                                        paraList.Add("@P_Allo_Status:" + "0");
                                        paraList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);

                                        paraList.Add("@P_return_Value:" + "");
                                        paraList.Add("@errText:" + "");
                                        string[] Rests = SqlDBHelp.ExecuteProc("PROC_ALLOCATE_OF_DEBIT", paraList);
                                        if (Rests[0] != "0")
                                        {
                                            sb.Append(this.grdALLOCATE.Rows[item.RowIndex].Cells[1].Text + "" + Lang.FrmALLOCATEList_DebitFail + "\r\n" + Rests[1]);
                                        }
                                        //}
                                        //else
                                        //    sb.Append(this.grdALLOCATE.Rows[item.RowIndex].Cells[1].Text + "扣账失败!\r\n" + "不存在已完成的ASRS命令！");
                                    }
                                }
                                else
                                {
                                    sb.Append("不是平库仓内调拨,无法手动扣账！");
                                }
                            }
                        }
                    }
                }
                if (sb.ToString().Length == 0)
                {
                    sb.Append(Lang.FrmALLOCATEList_DebitSuccess);
                }
                Alert(sb.ToString());
            }
            else
            {
                Alert(msg);
            }
            //DBUtil.Commit();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);

        }
        catch (Exception err)
        {
            //DBUtil.Rollback();
            btnUpdateSTOCK.Enabled = true;//Roger 20130425
            Alert(err.Message.ToJsString());
        }
        finally
        {
            //20130702084429
            btnUpdateSTOCK.Style.Remove("disabled");
        }
        btnUpdateSTOCK.Enabled = true;
    }

    private bool Check_Proc_OutBillTOStock_Currnt(string AllocateID, string UserNo, string P_Guid, ref string msg)
    {
        bool returnValue = true;

        List<string> paraList = new List<string>();
        paraList.Add("@P_AllocateID:" + AllocateID);
        paraList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
        paraList.Add("@P_Guid:" + Guid.NewGuid().ToString());

        string Result = SqlDBHelp.ExecuteProcReturnValue("Proc_CheckALLOCATEDebit", paraList, "@P_ReturnValue");

        if (Result != "0")
        //if (retuVAlue > 0)
        {
            string sql = "select ERRORMSG from CHECKERRORMSG error where error.userno='" + UserNo + "' and error.guid='" + P_Guid + "'";

            List<string> dt = db.Database.SqlQuery<List<string>>(sql).SingleOrDefault();

            foreach (var erMsg in dt)
            {
                msg += erMsg + "\r\n";
            }
            string sqlDelete = "delete from CHECKERRORMSG error where error.userno='" + UserNo + "' and error.guid='" + P_Guid + "'";
            db.Database.ExecuteSqlCommand(sqlDelete);
            returnValue = false;
        }

        return returnValue;
    }
    /// <summary>
    /// 记录单据号
    /// </summary>
    public Dictionary<string, string> SelectCode
    {
        set { ViewState["SelectCode"] = value; }
        get { return ViewState["SelectCode"] as Dictionary<string, string>; }
    }


    public string msgmail = "";
    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        try
        {
            GetSelectedId();
            //if (SelectIds == null)
            //{
            //    SelectIds = new Dictionary<string, string>();
            //}
            if (SelectCode == null)
            {
                SelectCode = new Dictionary<string, string>();
            }

            if (SelectIds != null && SelectIds.Count > 0)
            {
                foreach (var item in SelectIds)
                {
                    //  msgmail += GetMail(item.Key)+";";
                    string scode = "";
                    scode = GetMail(item.Key);
                    if (scode != "")
                    {
                        SelectCode.Add(scode, scode);
                    }
                }
            }
            if (SelectCode != null && SelectCode.Count > 0)
            {
                foreach (var items in SelectCode)
                {
                    msgmail += items.Key + ";";
                }

                if (SetSendMail(msgmail))
                {
                    Alert(Lang.FrmALLOCATEList_Msg03);
                }
                else
                {
                    Alert(Lang.FrmALLOCATEList_Msg04);
                }


            }//************************
            else
            {
                Alert(Lang.FrmALLOCATEList_Msg05);
            }
            if (SelectIds != null)
            {
                SelectIds.Clear();
            }
            if (SelectCode != null)
            {
                SelectCode.Clear();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }

    /// <summary>
    /// 发送邮件方法
    /// </summary>
    /// <param name="mailcode"></param>
    /// <returns></returns>
    public bool SetSendMail(string mailcode)
    {
        bool setmai = false;
        try
        {
            string htURL = "";
            htURL = Request.Url.AbsoluteUri;
            htURL = htURL.Substring(0, htURL.IndexOf("/Apps")) + "/Apps/ALLOCATE/FrmALLOCATE_Audit_Mail.aspx";
            string msgcode = "";
            msgcode = Lang.FrmALLOCATEList_lblCTICKETCODE + "：" + mailcode + "\r\n " + Lang.FrmALLOCATEList_LoginAddress + "：" + htURL; //<%--单据号--%>
            string host = "";
            host = Help.ReadWebconfig("Host");//smtp主机
            int port = 25;
            port = Convert.ToInt32(Help.ReadWebconfig("Prot")); //端口
            string Fromto = "";
            Fromto = Help.ReadWebconfig("Fromcount");//发件人
            string Pwd = "";
            Pwd = Help.ReadWebconfig("Password");//密码
            string Ssl = "";
            Ssl = Help.ReadWebconfig("EnableSsl");//是否SSL加密
            bool Ss = true;//是否SSL加密
            if (Ssl == "false")
            {
                Ss = false;
            }
            else
            {
                Ss = true;
            }
            string sendto = "";
            sendto = GetSendTo();
            if (SendEmail.IntoMail(host, Fromto, Pwd, Fromto, sendto, "" + Lang.FrmALLOCATEList_Msg06 + "", msgcode, port, Ss))
            {
                setmai = true;
            }

        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
        return setmai;
    }

    /// 获取收件人列表
    /// <summary>
    /// 获取收件人列表
    /// </summary>
    /// <returns></returns>
    public string GetSendTo()
    {
        string mailto = "";
        try
        {
            string Sql = @"SELECT D.pUserName,D.pEmail FROM v_User_Dept D WHERE D.userno='" + WmsWebUserInfo.GetCurrentUser().UserNo + "'";
            DataTable tb = db.Database.SqlQuery<DataTable>(Sql).SingleOrDefault();//DBUtil.Fill(Sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                for (int i = 0; i < tb.Rows.Count - 1; i++)
                {
                    mailto += tb.Rows[i]["pUserName"].ToString() + "<" + tb.Rows[i]["pEmail"].ToString() + ">;";
                }
                mailto = mailto + tb.Rows[tb.Rows.Count - 1]["pUserName"].ToString() + "<" +
                         tb.Rows[tb.Rows.Count - 1]["pEmail"].ToString() + ">";
            }
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
        return mailto;
    }


    /// 获取单据号
    /// <summary>
    /// 获取单据号
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetMail(string id)
    {
        string msg = "";
        try
        {
            string Sql = @"SELECT A.CTICKETCODE from ALLOCATE A  where A.CSTATUS='4' and A.id='" + id + "'";
            string tb = db.Database.SqlQuery<string>(Sql).SingleOrDefault(); //DBUtil.Fill(Sql);
            if (!tb.IsNullOrEmpty())
            {
                msg = tb;
            }

        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
        return msg;

    }

    public void GetSelectedId()
    {
        try
        {
            if (SelectIds == null)
            {
                SelectIds = new Dictionary<string, string>();
            }

            foreach (GridViewRow item in this.grdALLOCATE.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    CheckBox cbo = itemFindControl as CheckBox;

                    //Product product = item.DataItem as Product;
                    //获取ID
                    string id = this.grdALLOCATE.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                    //控件选中且集合中不存在添加
                    if (cbo.Enabled && cbo.Checked && !SelectIds.ContainsKey(id))
                    {
                        SelectIds.Add(id, this.grdALLOCATE.DataKeys[item.RowIndex][0].ToString());
                    }//未选中且集合中存在的移除                    
                    else if (!cbo.Checked && SelectIds.ContainsKey(id))
                    {
                        SelectIds.Remove(id);
                    }
                }
            }
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }


    public Dictionary<string, string> SelectYes
    {
        set { ViewState["SelectYes"] = value; }
        get { return ViewState["SelectYes"] as Dictionary<string, string>; }
    }

    /// <summary>
    /// 已确认按钮修改状态并发送邮件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnYesNo_Click(object sender, EventArgs e)
    {
        try
        {
            string returnMsg = string.Empty;
            GetSelectedId();
            if (SelectYes == null)
            {
                SelectYes = new Dictionary<string, string>();
            }
            if (SelectCode == null)
            {
                SelectCode = new Dictionary<string, string>();
            }
            if (SelectIds != null && SelectIds.Count > 0)
            {
                foreach (var item in SelectIds)
                {
                    if (CheckYesNo(item.Key))
                    {
                        SelectYes.Add(item.Key, item.Value);
                    }
                    //检查sn是否满足确认要求
                    string[] results = CheckAllocateSN(item.Key);
                    if (results[0] != "0")
                    {
                        returnMsg += results[1] + "\r\n";
                    }
                }
                if (SelectYes != null && SelectYes.Count > 0)
                {
                    foreach (var items in SelectYes)
                    {
                        string errMsg = string.Empty;
                        if (UPYesNo(items.Key, out errMsg)) //修改状态
                        {
                            Alert("确认成功");
                            string scode = GetMail(items.Key);//获取单据号
                            if (scode != "")
                            {
                                SelectCode.Add(scode, scode);
                            }
                        }
                        else
                        {
                            Alert(Lang.FrmALLOCATEList_lblCTICKETCODE + GetMail(items.Key) + Lang.FrmALLOCATEList_ConfirmFail + errMsg);
                        }
                    }
                    //发送邮件
                    if (SelectCode != null && SelectCode.Count > 0)
                    {
                        foreach (var items in SelectCode)
                        {
                            msgmail += items.Key + ";";
                        }
                        if (SetSendMail(msgmail))
                        {
                            Alert(Lang.FrmALLOCATEList_Msg03);

                        }
                        else
                        {
                            Alert(Lang.FrmALLOCATEList_Msg04);
                        }

                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(returnMsg))
                    {
                        Alert(Lang.FrmALLOCATEList_Msg07);
                    }
                    else
                    {
                        Alert("确认失败!\r\n" + returnMsg);
                    }

                }

                if (SelectIds != null)
                {
                    SelectIds.Clear();
                }
                if (SelectYes != null)
                {
                    SelectYes.Clear();
                }
                if (SelectCode != null)
                {
                    SelectCode.Clear();
                }
                GridBind();

            }
            else
            {
                Alert(Lang.FrmALLOCATEList_AlterConfirm);
            }

        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }

    //1.调拨单撤销审核 2.调拨单扣账/确认前检查库存和调拨单状态【begin】
    /// <summary>
    /// 修改未处理的为已确认
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool RevertCstatus(string id)
    {
        bool yesno = false;
        try
        {

            List<string> paraList = new List<string>();
            paraList.Add("@P_Allocate_id:" + id);
            paraList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
            paraList.Add("@P_return_Value:" + "");
            paraList.Add("@errText:" + "");
            string[] Result = SqlDBHelp.ExecuteProc("Proc_Allocate_RevertCstatus", paraList);
            if (Result[0] == "0")
            {
                yesno = true;
            }
            else
            {
                //Alert(Result[1]);
                returnErrorMsg = Result[1];

            }
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
        return yesno;

    }

    /// <summary>
    /// 检查调拨单SN状态，是否满足扣账要求
    /// 提示第几个SN不匹配
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private static string[] CheckAllocateSN(string id)
    {
        List<string> list = new List<string>();
        list.Clear();
        list = new List<string>();
        list.Add("@P_Allocate_id:" + id);
        list.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);

        list.Add("@P_return_Value:" + "");
        list.Add("@errText:" + "");
        string[] results = SqlDBHelp.ExecuteProc("Proc_CheckSNAndDateCodeQty", list);
        return results;
    }

    //1.调拨单撤销审核 2.调拨单扣账/确认前检查库存和调拨单状态【end】


    /// <summary>
    /// 检查状态是否是未处理的
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool CheckYesNo(string id)
    {
        bool yesno = false;
        try
        {
            string Sql = @"SELECT COUNT(1) FROM ALLOCATE A 
                            INNER JOIN ALLOCATE_D AD ON AD.ID=A.ID
                            WHERE A.CSTATUS='0' AND A.ID='" + id + "'";
            int tb = db.Database.SqlQuery<int>(Sql).SingleOrDefault(); //DBUtil.Fill(Sql);

            if (tb > 0)
            {
                yesno = true;
            }
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
        return yesno;
    }

    // 2016-04-20 ADD 
    /// <summary>
    /// 检查如果是旧库存，判读是否输入SN号
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool CheckYesNoSN(string id)
    {
        bool yesno = false;
        try
        {
            string Sql = @" select COUNT(1) from allocate_d_sn A
                            where 1=1 
                            and A.ALLOCATE_ID = '" + id + "' ";
            int tb = db.Database.SqlQuery<int>(Sql).SingleOrDefault(); //DBUtil.Fill(Sql);

            if (tb > 0)
            {
                yesno = true;
            }
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
        return yesno;
    }

    /// <summary>
    /// 检查是否是旧库存，如果存在旧库存，则执行下一步
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool CheckYesNoOldStock(string id)
    {
        bool yesno = false;
        try
        {

            string Sql = @" select isnull(sum(detail.qty), 0) qty
                            from Stock_Current_Detail detail where 1 = 1  
                            and detail.id = (SELECT ID from VIEW_STOCK_CURRENT where 1 = 1  and CPOSITIONCODE like
                            (select cpositioncode from allocate_d where id = '" + id + "') and CINVCODE like (select CINVCODE  from allocate_d where id = '" + id + "') ";
            Sql = Sql + " and CINVCODE not like 'M%' ) and detail.datecode like '13%'  ";
            decimal tb = db.Database.SqlQuery<decimal>(Sql).SingleOrDefault(); //DBUtil.Fill(Sql);


            string Sql_y = @"select isnull(sum(sn.qty), 0) qty
                            from  stock_current_sn sn  
                            where 1=1  and sn.stock_id  = (SELECT ID from VIEW_STOCK_CURRENT  where 1 = 1  and CPOSITIONCODE like 
                            (select cpositioncode from allocate_d where id = '" + id + "') and CINVCODE like (select CINVCODE  from allocate_d where id = '" + id + "')";
            Sql_y = Sql_y + " and CINVCODE not like 'M%' )  ";




            decimal tb_y = db.Database.SqlQuery<decimal>(Sql_y).SingleOrDefault(); //DBUtil.Fill(Sql_y);


            if (tb - tb_y > 0)
            {
                yesno = true;
            }
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
        return yesno;
    }


    // 2016-04-20 END 

    /// <summary>
    /// 修改未处理的为已确认
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool UPYesNo(string id, out string errMsg)
    {
        errMsg = string.Empty;
        bool yesno = false;
        try
        {

            List<string> paraList = new List<string>();
            paraList.Add("@P_Allocate_id:" + id);
            paraList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
            paraList.Add("@P_return_Value:" + "");
            paraList.Add("@errText:" + "");
            string[] Result = SqlDBHelp.ExecuteProc("Proc_Allocate_Occupancy", paraList);
            if (Result[0] == "0")
            {
                yesno = true;
            }
            else
            {
                errMsg = Result[1];
            }
        }
        catch (Exception err)
        {
            errMsg = err.Message;
        }
        return yesno;

    }

    /// <summary>
    /// 取消确认
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            IGenericRepository<ALLOCATE> con = new GenericRepository<ALLOCATE>(db);

            foreach (GridViewRow item in grdALLOCATE.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    CheckBox cbo = itemFindControl as CheckBox;
                    if (cbo.Checked)
                    {
                        string id = this.grdALLOCATE.DataKeys[item.RowIndex][0].ToString();
                        var bo = (from p in con.Get()
                                  where p.id == id
                                  select p).FirstOrDefault();
                        if (bo != null && !string.IsNullOrEmpty(bo.id) && bo.id.Length > 0)
                        {
                            if (this.grdALLOCATE.Rows[item.RowIndex].Cells[7].Text == "已确认" && bo.cstatus.Equals("4"))
                            {
                                //bo.cauditperson = WmsWebUserInfo.GetCurrentUser().UserNo;
                                //bo.daudittime = DateTime.Now;
                                //bo.cstatus = "0";
                                //con.Update(bo);
                                //con.Save();
                                if (!RevertCstatus(id)) //修改状态
                                {
                                    msg = "单据号撤销审核失败！" + returnErrorMsg;
                                }

                            }
                            else
                            {
                                //msg = "只有未处理的单据才能审核";
                                throw new Exception(Lang.FrmALLOCATEList_Msg08);
                            }
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Lang.FrmALLOCATEList_ReviewSuccess;
            }
            //DBUtil.Commit();
        }
        catch (Exception err)
        {
            //DBUtil.Rollback();
            //Alert(err.Message.ToJsString());
            msg = err.Message.ToJsString();
        }
        Alert(msg);
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    /// <summary>
    /// 判断SN的数量是否与主表一致
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool CheckSNQtySame(string id)
    {
        bool bl = false;
        string sql1 = string.Format(@" select isnull(sum(IQUANTITY),0) from allocate_d t where t.id='{0}'  ", id);
        string sql2 = string.Format(@" select isnull(sum(QUANTITY),0) from allocate_d_sn t where t.ALLOCATE_ID='{0}'  ", id);
        string r1 = db.Database.SqlQuery<string>(sql1).SingleOrDefault(); //DBUtil.ExecuteScalar(sql1).ToString();
        string r2 = db.Database.SqlQuery<string>(sql2).SingleOrDefault(); //DBUtil.ExecuteScalar(sql2).ToString();
        if (!r1.Equals(r2))
        {
            bl = true;
        }
        return bl;
    }
}

