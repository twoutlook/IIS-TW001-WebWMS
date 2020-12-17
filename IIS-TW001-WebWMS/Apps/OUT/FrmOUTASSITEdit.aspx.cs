using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business.Base;

/// <summary>
/// 捡货指引修改页
/// </summary>
public partial class OUT_FrmOUTASSITEdit : WMSBasePage
{
    #region 页面属性
    public string Status
    {
        get
        {
            if (ViewState["Status"] != null)
            {
                return ViewState["Status"].ToString();
            }
            return "";
        }
        set { ViewState["Status"] = value; }
    }

    /// <summary>
    /// 当前页面的操作
    /// </summary>
    public SYSOperation operation
    {
        get { return (SYSOperation)Enum.Parse(typeof(SYSOperation), this.hiddOperation.Value); }
        set { this.hiddOperation.Value = value.ToString(); }
    }
    #endregion

    #region 页面加载
    protected void Page_Load(object sender, EventArgs e)
    {
        ucOutASN_Div.SetCompName = txtCOUTASNID.ClientID;
        ucOutASN_Div.SetORGCode = txtOutAsn_Id.ClientID;
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.operation == SYSOperation.New) {
                trSubSearch.Visible = false;
                trGridView.Visible = false;
                btnPrint.Visible = false;
                txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
                txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                btnDelete.Enabled = false;
            }
            else if (this.operation == SYSOperation.Modify)
            {
                ShowData();
                txtCTICKETCODE.Enabled = false;
                txtCOUTASNID.Enabled = false;
                txtCMEMO.Enabled = false;
                ddlCSTATUS.Enabled = false;
                btnAutoCreate.Enabled = false;
            }
            else if (this.operation == SYSOperation.Preserved1)
            {
                ShowData();
                this.operation = SYSOperation.Modify;
                this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmOUTASSIT_DEdit.aspx", SYSOperation.New, "") + "&parentId=" + this.KeyID + "','','OUTASSIT_D',600,350);");
            }
            else
            {
                txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
                txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                btnDelete.Enabled = false;
            }
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        this.btnAutoCreate.Attributes["onclick"] = this.GetPostBackEventReference(this.btnAutoCreate) + ";this.disabled=true;";
    }

    /// <summary>
    /// 页面初始化
    /// </summary>
    public void InitPage()
    {
        this.operation = this.Operation();
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.grdOUTASSIT_D.DataKeyNames = new string[] { "IDS" };

        //多国语更改,dropDownlist【begin】
        //var paraList = context.SYS_PARAMETER.Where(x => x.flag_type == "ASSIT").OrderBy(x => x.flag_id).ToList();
        //Help.DropDownListDataBind(paraList, this.ddlCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//全部
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "ASSIT", false, -1, -1), this.ddlCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        //多国语更改,dropDownlist【end】

        Help.DropDownListDataBind(this.GetOutType(false), this.ddlOutType, Resources.Lang.FrmOUTASNList_OutType, "FUNCNAME", "EXTEND1", "");//出库类型
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTASSIT');return false;";
        txtCOUTASNID.Attributes["onclick"] = "Show('" + ucOutASN_Div.GetDivName + "');";

        //设置保存按钮的文字及其状态
        if (this.operation == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.operation == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.FrmOUTASNEdit_ShengPi;//审批
        }
    }

    #endregion

    #region 页面事件
    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = false;
        IGenericRepository<OUTASSIT> con = new GenericRepository<OUTASSIT>(context);
        OUTASSIT entity = this.SendData();
        string strKeyID = "";
        if (entity == null) {
            return;
        }
        try
        {
            if (this.operation == SYSOperation.Modify)
            {
                strKeyID = txtID.Text.Trim();
                entity.id = strKeyID;
                con.Update(entity);
                con.Save();
            }
            else if (this.operation == SYSOperation.New)
            {
                strKeyID = Guid.NewGuid().ToString();
                entity.id = strKeyID;
                txtID.Text = strKeyID;
                con.Insert(entity);
                con.Save();
                this.btnAutoCreate.Enabled = true;
                txtCTICKETCODE.Text = entity.cticketcode;
            }
            if ((sender as Button).ID == "btnNew")
            {
                Response.Redirect(BuildRequestPageURL("FrmOUTASSITEdit.aspx", SYSOperation.Preserved1, strKeyID));
            }
            else if ((sender as Button).ID != "btnAutoCreate" && (sender as Button).ID != "btnAutoCreateTest")
            {
                Response.Redirect(BuildRequestPageURL("FrmOUTASSITEdit.aspx", SYSOperation.Modify, strKeyID));
            }
        }
        catch (Exception ex)
        {
            btnSave.Enabled = true;
            this.Alert(this.GetOperationName() + Resources.Lang.WMS_Common_Msg_Failed + ex.Message);//失败！
        }
        btnSave.Enabled = true;
    }

    protected void btnDeleteParent_Click(object sender, EventArgs e)
    {
        try
        {
            IGenericRepository<OUTASSIT> con = new GenericRepository<OUTASSIT>(context);
            con.Delete(this.KeyID);
            con.Save();
        }
        catch (Exception ex)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + ex.Message);//删除失败！
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;//默认第一页
            AspNetPager1.CurrentPageIndex = 1;
        }
        IsFirstPage = true;
        this.GridBind();
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        using (var modContext = this.context)
        {
            using (var dbContextTransaction = modContext.Database.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < this.grdOUTASSIT_D.Rows.Count; i++)
                    {
                        if (this.grdOUTASSIT_D.Rows[i].Cells[0].Controls[0] is CheckBox)
                        {
                            CheckBox chkSelect = (CheckBox)this.grdOUTASSIT_D.Rows[i].Cells[0].Controls[0];
                            if (chkSelect.Checked)
                            {
                                string key = this.grdOUTASSIT_D.DataKeys[i].Values[0].ToString();
                                OUTASSIT_D entity = modContext.OUTASSIT_D.Where(x => x.ids == key).FirstOrDefault();
                                if (entity != null) {

                                    var modAssit = modContext.OUTASSIT.Where(x=>x.id == entity.id).FirstOrDefault();
                                    var modAsn = modContext.OUTASN.Where(x=>x.id == modAssit.coutasnid).FirstOrDefault();
                                    //超发判断
                                    if (modContext.TEMP_OUTBILL_D.Where(x => x.cinvcode == entity.cinvcode && x.line_qty > 0 && x.id == modAsn.cticketcode).Any())
                                    {
                                        msg = Resources.Lang.FrmOUTASSITList_Tips_CiLiaoHao + entity.cinvcode + Resources.Lang.FrmOUTASSITList_Tips_CunZaiChaoLin;//"此料号" + entity.cinvcode + "对应通知单存在超领";
                                        break;
                                    }
                                    else
                                    {
                                        modContext.OUTASSIT_D.Attach(entity);
                                        modContext.OUTASSIT_D.Remove(entity);
                                        modContext.SaveChanges();
                                    }
                                }
                                else
                                {
                                    msg = Resources.Lang.FrmOUTASNList_Tips_YiChang_DeleteFailed;// "数据异常,删除失败!";
                                    break;
                                }
                            }
                        }
                    }
                    if (msg.Length == 0)
                    {
                        msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;//删除成功!
                    }
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    msg += Resources.Lang.WMS_Common_Msg_DeleteFailed + "[" + ex.Message + "]";//删除失败!
                }
            }
            this.Alert(msg);
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        btnSave_Click(sender, e);
    }

    //打印功能
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string printid = this.txtID.Text.Trim();
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmOUTASSITEdit_Print.aspx", SYSOperation.New, printid) + "','" + Resources.Lang.FrmOUTASSITList_Tips_PrintZhiYin + "','BAR_REPACK',800,600);");//打印揀貨指引單
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }

    /// <summary>
    /// 生成指引
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAutoCreate_Click(object sender, EventArgs e)
    {
        this.btnAutoCreate.Enabled = false;

        string msg = string.Empty;
        try
        {
            if (this.CheckData())
            {
                btnSave_Click(sender, e);
                string guid = Guid.NewGuid().ToString();

                #region 调用存储过程
                List<string> SparaList = new List<string>();
                SparaList.Add("@P_OutAssit_id:" + txtID.Text.Trim());
                SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                SparaList.Add("@P_Guid:" + guid);
                SparaList.Add("@P_ReturnValue:" + "");
                SparaList.Add("@INFOTEXT:" + "");
                string[] results = DBHelp.ExecuteProc("Proc_AutoCreateOutAssit_D", SparaList);
                if (results.Length == 1)//调用存储过程有错误
                {
                    msg = results[0].ToString();               
                }
                else if (results[0] == "0")
                {
                    //成功
                }
                else
                {
                    msg = results[1].ToString();   
                }

                this.btnAutoCreate.Enabled = true;
                btnAutoCreate.Style.Remove("disabled");
                #endregion

                if (msg.Length == 0)
                {
                    //生成成功 跳转到出库单页面
                    this.AlertAndBack("FrmOUTASSITEdit.aspx?" + BuildQueryString(SYSOperation.Modify, txtID.Text.Trim()), Resources.Lang.WMS_Common_Msg_SaveSuccess);//保存成功
                }
                else {
                    Alert(msg);
                }               
            }
            else
            {
                this.btnAutoCreate.Enabled = true;

                btnAutoCreate.Style.Remove("disabled");
            }
        }
        catch (Exception ex)
        {
            this.btnAutoCreate.Enabled = true;
            btnAutoCreate.Style.Remove("disabled");
            Alert(ex.Message);
        }
    }

    /// <summary>
    /// 生成指引 测试
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAutoCreateTest_Click(object sender, EventArgs e)
    {
        this.btnAutoCreateTest.Enabled = false;

        string msg = string.Empty;

        if (this.CheckData())
        {
            //btnSave_Click(sender, e);
            //string guid = Guid.NewGuid().ToString();
            //Proc_AutoCreateOutAssit_D_Test proc = new Proc_AutoCreateOutAssit_D_Test();
            //proc.P_OutAssit_id = txtID.Text.Trim();
            //proc.P_Guid = guid;
            //proc.P_UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
            //proc.Execute();
            //if (proc.P_ReturnValue == 0)
            //{
            //    msg = "生成成功!";
            //    this.btnAutoCreateTest.Enabled = false;
            //}
            //else
            //{
            //    msg = "生成失败!";
            //    this.btnAutoCreateTest.Enabled = true;
            //}
            //Alert(new ErrorMsgQuery().GetErrorMsg(guid) + msg);
            ////获取已生成的上架指引
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }

    }

    /// <summary>
    /// 分页控件事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void grdOUTASSIT_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            //HyperLink linkDelete = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            //linkDelete.NavigateUrl = "#";
            //this.OpenFloatWinMax(linkDelete, BuildRequestPageURL("FrmOUTASSIT_DEdit.aspx", SysOperation.Modify, strKeyID), "出库单", "OUTASSIT_D");

            //e.Row.Cells[e.Row.Cells.Count - 2].Text = e.Row.Cells[e.Row.Cells.Count - 2].Text == "0" ? "未处理" : "已完成";
        }

    }

    #endregion

    #region 页面方法
    public void GridBind()
    {
        using (var modContext = context)
        {
            var queryList = from p in modContext.OUTASSIT_D
                            where p.id == txtID.Text.Trim()
                            select p;

            if (!string.IsNullOrEmpty(txtCinvCode.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cinvcode.Contains(txtCinvCode.Text.Trim()));
            }
            if (this.cboIsLineCargoSpace.Checked)
            {
                queryList = queryList.Where(x => modContext.BASE_LINE_INFO.Any(p => p.cpositioncode == x.cpositioncode));
            }
            queryList = queryList.Distinct().AsQueryable();
            queryList = queryList.OrderByDescending(x => x.cinvcode);

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;

            List<OUTASSIT_D> data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            var source = from p in data
                         join oper in modContext.BASE_OPERATOR on p.coperatorcode equals oper.caccountid into temp
                         from tt in temp.DefaultIfEmpty()
                         select new
                         {
                             p.ids,
                             p.cinvcode,
                             p.cinvname,
                             p.cpositioncode,
                             p.cposition,
                             p.cinvbarcode,
                             p.inum,
                             p.cbatch,
                             p.sourcecode,
                             p.cmemo,
                             p.coperatorcode,
                             coperator = tt == null ?"":tt.coperatorname,
                             p.cstatus
                         };
            this.grdOUTASSIT_D.DataSource = source.ToList();
            this.grdOUTASSIT_D.DataBind();
        }
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        btnDelete.Enabled = true;
        using (var modContext = context)
        {
            OUTASSIT entity = modContext.OUTASSIT.Where(x => x.id == this.KeyID).FirstOrDefault();
            if (entity != null)
            {
                this.txtID.Text = entity.id.ToString();
                this.txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(entity.ccreateownercode);
                this.txtDCREATETIME.Text = entity.dcreatetime.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtCTICKETCODE.Text = entity.cticketcode;
                ddlCSTATUS.SelectedValue = entity.cstatus;

                OUTASN asn = modContext.OUTASN.Where(x => x.id == entity.coutasnid).FirstOrDefault();
                if (asn != null)
                {
                    txtOutAsn_Id.Text = entity.coutasnid;
                    ddlOutType.SelectedValue = asn.itype.ToString();
                    this.txtCOUTASNID.Text = asn.cticketcode;
                }
                this.txtCMEMO.Text = entity.cmemo;
                Status = entity.cstatus;
                if (entity.cstatus != "0")
                {
                    SetTblFormControlEnabled(false);
                }
            }
        }
    }

    private void SetTblFormControlEnabled(bool value)
    {
        txtCCREATEOWNERCODE.Enabled = value;
        txtCOUTASNID.Enabled = value;
        txtCTICKETCODE.Enabled = value;
        txtDCREATETIME.Enabled = value;
        txtID.Enabled = value;
        txtOutAsn_Id.Enabled = value;
        ddlCSTATUS.Enabled = value;

        txtCOUTASNID.Attributes.Remove("onclick");

        btnDelete.Enabled = value;
        btnNew.Enabled = value;
        btnSave.Enabled = value;
        btnAutoCreate.Enabled = value;
        btnDeleteParent.Enabled = value;
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (this.txtCCREATEOWNERCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTASSITList_Tips_NeedZhiDanRen);//制单人项不允许空！
            this.SetFocus(txtCCREATEOWNERCODE);
            return false;
        }
        //
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (this.txtCCREATEOWNERCODE.Text.GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmOUTASSITList_Tips_ZhiDanRenLength);//制单人项超过指定的长度100！
                this.SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }
        //
        if (this.txtDCREATETIME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTASSITList_Tips_NeedRiQi);//制单日期项不允许空！
            this.SetFocus(txtDCREATETIME);
            return false;
        }
        //
        if (this.txtDCREATETIME.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIME.Text.IsDate("yyyy-MM-dd HH:mm:ss") == false)
            {
                this.Alert(Resources.Lang.FrmOUTASSITList_Tips_RiQiError);//制单日期项不是有效的日期！
                this.SetFocus(txtDCREATETIME);
                return false;
            }
        }
        if (this.txtCOUTASNID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTASSITList_Tips_NeedXiangCi);//出库通知单号项不允许空！
            this.SetFocus(txtCOUTASNID);
            return false;
        }
        //
        if (this.txtCOUTASNID.Text.Trim().Length > 0)
        {
            if (this.txtCOUTASNID.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmOUTASSITList_Tips_XiangCiLength);//出库通知单号项超过指定的长度30！
                this.SetFocus(txtCOUTASNID);
                return false;
            }
        }
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 200)
            {
                this.Alert(Resources.Lang.FrmOUTASSITList_Tips_CmemoLength);//备注项超过指定的长度200！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }

        return true;

    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public OUTASSIT SendData()
    {
        var modAsn = context.OUTASN.Where(x => x.id == txtOutAsn_Id.Text.Trim()).FirstOrDefault();
        if (modAsn == null) {
            this.Alert(Resources.Lang.FrmOUTASSITList_Tips_OutAsnError);//出库通知单异常!
            return null;
        }

        if (this.operation == SYSOperation.New) {
            if (modAsn.worktype != "0") {
                this.Alert(Resources.Lang.FrmOUTASSITList_Tips_OnlyPingKu);//只有平库的通知单才能生成指引!
                return null;
            }
        }

        OUTASSIT entity = new OUTASSIT();
        this.txtCCREATEOWNERCODE.Text = this.txtCCREATEOWNERCODE.Text.Trim();
        if (this.txtCCREATEOWNERCODE.Text.Length > 0)
        {
            entity.ccreateownercode = OPERATOR.GetUserIDByUserName(txtCCREATEOWNERCODE.Text);
        }
        this.txtDCREATETIME.Text = this.txtDCREATETIME.Text.Trim();
        if (this.txtDCREATETIME.Text.Length > 0)
        {
            entity.dcreatetime = txtDCREATETIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        this.txtCTICKETCODE.Text = this.txtCTICKETCODE.Text.Trim();
        if (this.txtCTICKETCODE.Text.Length > 0)
        {
            entity.cticketcode = txtCTICKETCODE.Text;
        }
        else
        {
            entity.cticketcode = new Fun_CreateNo().CreateNo("OUTASSIT");
        }
        if (this.operation == SYSOperation.New)
        {
            entity.cstatus = "0";
        }
        else
        {
            entity.cstatus = ddlCSTATUS.SelectedValue;
        }

        this.txtOutAsn_Id.Text = this.txtOutAsn_Id.Text.Trim();

        if (this.txtCOUTASNID.Text.Length > 0)
        {
            entity.coutasnid = txtOutAsn_Id.Text;
        }

        if (modAsn.is_merge == 1 && !string.IsNullOrEmpty(modAsn.merge_id))
        {
            entity.is_merge = 1;
        }
        else
        {
            entity.is_merge = 0;
        }
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        return entity;

    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdOUTASSIT_D.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdOUTASSIT_D.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    #endregion

}

