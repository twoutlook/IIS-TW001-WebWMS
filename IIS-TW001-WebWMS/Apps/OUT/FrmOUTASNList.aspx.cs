using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Data.SqlClient;
using DreamTek.ASRS.Business.SP.ProcedureModel;
using DreamTek.ExternalService.XueLong;

public partial class OUT_FrmOUTASNList : WMSBasePage
{
    #region 页面属性

    private Dictionary<string, string> dict = new Dictionary<string, string>();

    #endregion

    #region 页面加载
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            //this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            //this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        btnClose.Attributes.Add("onclick", this.GetPostBackEventReference(btnClose) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
    }

    /// <summary>
    /// 页面初始化
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";

        //多语言改动【begin】
        //var caseList = context.SYS_PARAMETER.Where(x => x.flag_type == "OS").OrderBy(x => x.sortid).ToList();
        //Help.DropDownListDataBind(caseList.ToList(), this.txtCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "OS", false, -1, -1), this.txtCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        //权限平台
        Help.DropDownListDataBind(GetOutType(true), this.txtITYPE, Resources.Lang.WMS_Common_DrpAll, "FUNCNAME", "EXTEND1", "");        
        //ddlIS_MERGE
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "IS_MergeCode", false, -1, -1), this.ddlIS_MERGE, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "SMTType", false, -1, -1), this.ddlSmt, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "WorkType", false, -1, -1), this.drpWorkType, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.RadioButtonDataBind(SysParameterList.GetList("", "", "MonthOrWeek", false, -1, -1), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");
        //多语言改动【end】

        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmOUTASNEdit.aspx", SYSOperation.New, "&Flag=0&IsSpecialPage=0&IsSpecialWIP_Issue=0") + "','" + Resources.Lang.FrmOUTASNList_NewOutAsnPage + "','OUTASN');return false;";

        // NOTE by Mark, 11/04, 首頁和應對頁面數量一致, based on 小仲's advice, NX rev 634
        if (!string.IsNullOrEmpty(Request.QueryString["Cstatus"]))
        {
            this.txtCSTATUS.SelectedValue = this.Request.QueryString["Cstatus"].ToString();
        }
    }

    #endregion

    #region 页面事件
    /// <summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;//默认第一页
            AspNetPager1.CurrentPageIndex = 1;
        }
        IsFirstPage = true;

        //Note by Qamar 2020-12-12
        //如果在.aspx設定Visible為false, 會取不到數值, 因此寫在.aspx.cs這裡
        try { grdOUTASN.Columns[18].Visible = true; }
        catch { }
        this.GridBind();
        try { grdOUTASN.Columns[18].Visible = false; }
        catch { }
    }

    /// <summary>
    /// 删除通知单事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        string updateStatusMsg = string.Empty;
        try
        {          

            for (int i = 0; i < this.grdOUTASN.Rows.Count; i++)
            {
                if (this.grdOUTASN.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdOUTASN.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string outasnId = this.grdOUTASN.DataKeys[i].Values[0].ToString();
                        var modOutAsn = context.OUTASN.Where(x => x.id == outasnId).FirstOrDefault();
                        if (modOutAsn != null)
                        {
                            if (modOutAsn.cstatus == "0")
                            {
                                //根据当前单据号ID而确认当前单据是否可以进行操作
                                OUTTYPE OUTASN_type = GetOUTTYPE(modOutAsn.itype.ToString());
                                if (OUTASN_type != null && !string.IsNullOrEmpty(OUTASN_type.Is_Query.ToString()))
                                {
                                    if (OUTASN_type.Is_Query.ToString() == "1")
                                    {
                                        msg = Resources.Lang.FrmOUTASNList_Tips_JingChaXun;
                                        break;
                                    }
                                }

                                //WIP ISSUE ， erp接口过来的数据， 无权限删除
                                if (grdOUTASN.Rows[i].Cells[2].Text.Trim().ToUpper() == "WIP ISSUE" || modOutAsn.cdefine2 == "1" )
                                {
                                    msg = Resources.Lang.FrmOUTASNList_Tips_WuQuanShanChu;
                                    break;
                                }
                                if (modOutAsn.special_out == 1)
                                {
                                    msg = Resources.Lang.FrmOUTASNList_Tips_TeShuChaoLingShanChu;
                                    break;
                                }
                                #region 调用存储过程
                                List<string> SparaList = new List<string>();
                                SparaList.Add("@P_OutAsn_id:" + this.grdOUTASN.DataKeys[i].Values[0].ToString());
                                SparaList.Add("@P_ReturnValue:" + "");
                                SparaList.Add("@INFOTEXT:" + "");
                                string[] result = DBHelp.ExecuteProc("Proc_DeleteOutAsn", SparaList);
                                if (result.Length == 1)//调用存储过程有错误
                                {
                                    this.Alert(result[0].ToString());
                                }
                                else if (result[0] == "0")
                                {
                                }
                                else
                                    msg = result[1].ToString();
                                #endregion


                            }
                            else {
                                msg = Resources.Lang.FrmOUTASNList_Tips_WeiChuLi_DeleteFailed;
                                break;
                            }
                        }
                        else {
                            msg = Resources.Lang.FrmOUTASNList_Tips_YiChang_DeleteFailed;
                            break;              
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;
            }
            else
            {
                msg = msg + Resources.Lang.WMS_Common_Msg_DeleteFailed;
            }
            this.GridBind();
        }
        catch
        {
            msg += Resources.Lang.WMS_Common_Msg_DeleteFailed;
        }
        this.Alert(msg);
    }

    /// <summary>
    /// 生成出库单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnCreateOutBill_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        string outAsnId = (sender as LinkButton).CommandArgument;
        string outBill_Id = Guid.NewGuid().ToString();         

        OUTASN entity = context.OUTASN.Where(x => x.id == outAsnId).FirstOrDefault();
        if (entity == null)
        {
            Alert(Resources.Lang.FrmOUTASNList_Tips_ShuJuYiChang);
            return;
        }

        if (entity.itype != 206)
        {
            var result1 = OutAsnQuery.CanModDebit(entity.cerpcode, "", "2", outAsnId, "", "");
            if (!result1.Equals("1"))
            {
                Alert(result1);
                return;
            }
        }
        //通知单存在修改中的料时，不允许生成出库单
        var result = OutAsnQuery.CanAsnDebit(entity.cticketcode, "", "1", "", "");
        if (!result.Equals("OK"))
        {
            Alert(result);
            return;
        }

        #region 调用存储过程
        List<string> SparaList = new List<string>();
        SparaList.Add("@P_OutAsn_id:" + outAsnId.Trim());
        SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
        SparaList.Add("@P_OutBill_Id:" + outBill_Id.Trim());
        SparaList.Add("@P_IsTemporary:" + "0");
        SparaList.Add("@P_ReturnValue:" + "");
        SparaList.Add("@INFOTEXT:" + "");
        string[] results = DBHelp.ExecuteProc("Proc_CreateOutBill", SparaList);
        if (results.Length == 1)//调用存储过程有错误
        {
            this.Alert(results[0].ToString());
            return;
        }
        else if (results[0] == "0")
        {
            //生成成功 跳转到出库单页面
            this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmOUTBILLEdit.aspx", SYSOperation.Modify, outBill_Id) + "','" + Resources.Lang.FrmOUTASNList_Tips_ChuKuDan + "','OUTBILL');");
        }
        else
        {
            Alert(results[1].ToString());
        }
        #endregion

    }

    /// <summary>
    /// 结案按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            for (int i = 0; i < this.grdOUTASN.Rows.Count; i++)
            {
                if (this.grdOUTASN.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdOUTASN.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        //获取通知单号ID  
                        string asnid = this.grdOUTASN.DataKeys[i].Values[0].ToString();

                        #region 调用存储过程
                        List<string> SparaList = new List<string>();
                        SparaList.Add("@P_Asn_ID:" + asnid);
                        SparaList.Add("@pRetMsg:" + "");
                        SparaList.Add("@pRetCode:" + "");
                        string[] result = DBHelp.ExecuteProc("Proc_wms_Close_Asn", SparaList);
                        if (result.Length == 1)//调用存储过程有错误
                        {
                            this.Alert(result[0].ToString());
                        }
                        else if (result[0] == "0")
                        {
                            //成功
                        }
                        else
                            msg = result[1].ToString();
                        #endregion
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.FrmOUTASNList_Tips_JieAnChengGong;
            }
            else
            {
                msg = msg + Resources.Lang.FrmOUTASNList_Tips_JieAnShiBai;
            }
            this.GridBind();
        }
        catch
        {
            msg += Resources.Lang.FrmOUTASNList_Tips_JieAnShiBai;
        }
        this.Alert(msg);
    }

    /// <summary>
    /// 合并通知单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Merge_Click(object sender, EventArgs e)
    {
        IGenericRepository<TEMP_OUTASN_ID> con = new GenericRepository<TEMP_OUTASN_ID>(context);
        string msg = string.Empty;
        int count = 0;
        string ASNCode = "";
        string GROUPID = Guid.NewGuid().ToString();
        try
        {
            for (int i = 0; i < this.grdOUTASN.Rows.Count; i++)
            {
                if (this.grdOUTASN.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdOUTASN.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        count++;
                        ASNCode += this.grdOUTASN.DataKeys[i].Values[0].ToString() + ',';
                    }
                }
            }

            if (count <= 1)
            {
                Alert(Resources.Lang.FrmOUTASNList_Tips_HeBingTiaoShu);
                return;
            }
            else
            {
                ASNCode = ASNCode.Substring(0, ASNCode.Length - 1);
                for (int ii = 0; ii < ASNCode.Split(',').Length; ii++)
                {
                    TEMP_OUTASN_ID entity = new TEMP_OUTASN_ID();
                    string AsnCodeSplitID = ASNCode.Split(',')[ii];
                    entity.id = Guid.NewGuid().ToString();
                    entity.asnid = AsnCodeSplitID;
                    entity.groupid = GROUPID;
                    con.Insert(entity);
                    con.Save();
                }
                List<string> SparaList = new List<string>();
                SparaList.Add("@P_ASNOUT_ID:" + GROUPID.Trim());
                SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                SparaList.Add("@P_return_Value:" + "");
                SparaList.Add("@P_ErrText:" + "");
                string[] result = DBHelp.ExecuteProc("PRC_OUTASN_MERGE", SparaList);
                if (result.Length == 1)//调用存储过程有错误
                {
                    msg += result[0].ToString();
                }
                else if (result[0] == "0")
                {
                    msg += Resources.Lang.FrmOUTASNList_Tips_HeBingChengGong;
                }
                else
                {
                    msg += result[1].ToString();
                }
            }
            this.GridBind();
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }
        this.Alert(msg);
    }

    /// <summary>
    /// 取消合并
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancleMerge_Click(object sender, EventArgs e)
    {
        //取消合并
        string msg = string.Empty;
        string asnid = string.Empty;
        string code = string.Empty;
        try
        {
            for (int i = 0; i < this.grdOUTASN.Rows.Count; i++)
            {
                if (this.grdOUTASN.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdOUTASN.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        //获取通知单号ID  
                        asnid = this.grdOUTASN.DataKeys[i].Values[0].ToString();
                        code = this.grdOUTASN.DataKeys[i].Values[6].ToString();

                        List<string> SparaList = new List<string>();
                        SparaList.Add("@P_ASNOUT_ID:" + asnid);
                        SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                        SparaList.Add("@P_ReturnValue:" + "");
                        SparaList.Add("@P_INFOTEXT:" + "");
                        string[] result = DBHelp.ExecuteProc("PRC_Cancel_MERGEAsn", SparaList);
                        if (result.Length == 1)//调用存储过程有错误
                        {
                            msg += result[0].ToString();
                        }
                        else if (result[0] == "0")
                        {
                            msg += code + Resources.Lang.WMS_Common_Msg_CancelSuccess + " \r\n ";
                        }
                        else
                        {
                            msg += code + result[1].ToString();
                        }                    
                    }
                }
            }

            if (string.IsNullOrEmpty(msg))
            {
                Alert(Resources.Lang.FrmOUTASNList_Tips_XuanZeTongZhiDan);
            }
            else
            {
                Alert(msg);
            }

            this.GridBind();

        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }
    }

    /// <summary>
    /// 确认出库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_Out_Click(object sender, EventArgs e)
    {
        bool isZhiDingSite = false;
        //是否指定站点出库 0:否 1：是
        var modConfig = context.SYS_CONFIG.Where(x => x.code == "140200").FirstOrDefault();
        if (modConfig != null && modConfig.config_value == "1")
        {
            isZhiDingSite = true;
        }
        if (isZhiDingSite)
        {
            string asnid = string.Empty;
            int count = 0;
            for (int i = 0; i < this.grdOUTASN.Rows.Count; i++)
            {
                if (this.grdOUTASN.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdOUTASN.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        asnid = this.grdOUTASN.DataKeys[i].Values[0].ToString();
                        count++;
                    }
                }
            }
            if (count != 1)
            {
                this.Alert("请选择一个出库通知单！");
                return;
            }

            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmOUTASN_AwardSite.aspx", SYSOperation.Modify, asnid + "&OutMode=0") + "','指定站点出库','OUTASNSITE',500,400);");
        }
        else
        {
            string msg = string.Empty;
            try
            {
                string procName = "PRC_OUTCONFIRM";
                //判断立库入.出库单位
                var config = context.SYS_CONFIG.Where(x => x.code == "120003").FirstOrDefault();
                if (config != null && config.config_value == "1")
                { //设置为箱时,走博雷逻辑
                    procName = "PRC_OUTCONFIRM_BOX";
                }

                for (int i = 0; i < this.grdOUTASN.Rows.Count; i++)
                {
                    if (this.grdOUTASN.Rows[i].Cells[0].Controls[1] is CheckBox)
                    {
                        CheckBox chkSelect = (CheckBox)this.grdOUTASN.Rows[i].Cells[0].Controls[1];
                        if (chkSelect.Checked)
                        {
                            //获取通知单号ID  
                            string asnid = this.grdOUTASN.DataKeys[i].Values[0].ToString();
                            List<string> SparaList = new List<string>();
                            SparaList.Add("@P_OUTASNID:" + asnid.Trim());
                            SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                            SparaList.Add("@P_Type:" + 0);
                            SparaList.Add("@P_Craneid:" + "");
                            SparaList.Add("@P_return_Value:" + "");
                            SparaList.Add("@P_ErrText:" + "");

                            string[] result = DBHelp.ExecuteProc(procName, SparaList);

                            if (result.Length == 1)//调用存储过程有错误
                            {
                                msg += result[0];
                            }
                            else if (result[0] == "0")
                            {
                                msg += Resources.Lang.FrmOUTASNList_Tips_ChuKuChengGong;
                            }
                            else
                            {
                                msg += result[1];
                            }
                        }
                    }
                }

                this.GridBind();
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }
            this.Alert(msg);
        }

    }

    /// <summary>
    /// 紧急出库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_EmergencyOut_Click(object sender, EventArgs e)
    {
        bool isZhiDingSite = false;
        //是否指定站点出库 0:否 1：是
        var modConfig = context.SYS_CONFIG.Where(x => x.code == "140200").FirstOrDefault();
        if (modConfig != null && modConfig.config_value == "1")
        {
            isZhiDingSite = true;
        }
        if (isZhiDingSite)
        {
            string asnid = string.Empty;
            int count = 0;
            for (int i = 0; i < this.grdOUTASN.Rows.Count; i++)
            {
                if (this.grdOUTASN.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdOUTASN.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        asnid = this.grdOUTASN.DataKeys[i].Values[0].ToString();
                        count++;
                    }
                }
            }
            if (count != 1)
            {
                this.Alert("请选择一个出库通知单！");
                return;
            }

            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmOUTASN_AwardSite.aspx", SYSOperation.Modify, asnid + "&OutMode=1") + "','指定站点出库','OUTASNSITE',500,400);");
        }
        else
        {
            string msg = string.Empty;
            string procName = "PRC_OUTCONFIRM";
            //判断立库入.出库单位
            var config = context.SYS_CONFIG.Where(x => x.code == "120003").FirstOrDefault();
            if (config != null && config.config_value == "1")
            { //设置为箱时,走博雷逻辑
                procName = "PRC_OUTCONFIRM_BOX";
            }
            try
            {
                for (int i = 0; i < this.grdOUTASN.Rows.Count; i++)
                {
                    if (this.grdOUTASN.Rows[i].Cells[0].Controls[1] is CheckBox)
                    {
                        CheckBox chkSelect = (CheckBox)this.grdOUTASN.Rows[i].Cells[0].Controls[1];
                        if (chkSelect.Checked)
                        {
                            //获取通知单号ID  
                            string asnid = this.grdOUTASN.DataKeys[i].Values[0].ToString();
                            List<string> SparaList = new List<string>();
                            SparaList.Add("@P_OUTASNID:" + asnid.Trim());
                            SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                            SparaList.Add("@P_Type:" + 1);
                            SparaList.Add("@P_Craneid:" + "");
                            SparaList.Add("@P_return_Value:" + "");
                            SparaList.Add("@P_ErrText:" + "");
                            string[] result = DBHelp.ExecuteProc(procName, SparaList);
                            if (result.Length == 1)//调用存储过程有错误
                            {
                                msg += result[0];
                            }
                            else if (result[0] == "0")
                            {
                                msg += Resources.Lang.FrmOUTASNList_Tips_ChuKuChengGong;
                            }
                            else
                            {
                                msg += result[1];
                            }

                        }
                    }
                }
                this.GridBind();

            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }
            this.Alert(msg);
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

    /// <summary>
    /// 列表行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdOUTASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 3].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmOUTASNEdit.aspx", SYSOperation.Modify, strKeyID + "&IsSpecialPage=0&IsSpecialWIP_Issue=" + this.grdOUTASN.DataKeys[e.Row.RowIndex][4]), Resources.Lang.FrmOUTASNList_Menu_PageName, "OUTASN");
            if (Convert.ToInt32(this.grdOUTASN.DataKeys[e.Row.RowIndex][5]) > 0)
            {
                linkModify.Style.Add("color", "Green");
                linkModify.Style.Add("font-weight", "bold");
            }

            HyperLink linkModify_Error = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify_Error.NavigateUrl = "#";
            //判断是否有错误信息
            if (OutAsnQuery.validateIsExistErrorMsg(this.grdOUTASN.DataKeys[e.Row.RowIndex][0].ToString()))
            {
                linkModify_Error.Style.Add("color", "Red");
                this.OpenFloatWin(linkModify_Error, BuildRequestPageURL("/Apps/SystemLogs/FrmLogSystemList.aspx", SYSOperation.Modify, strKeyID + "&TableName=OutAsn"), Resources.Lang.FrmOUTASNList_Tips_YiChangXingXi, "ErrorMsg", 900, 530);
            }
            else
            {
                linkModify_Error.Enabled = false;
            }

            TableCell cellTaskMode = e.Row.Cells[e.Row.Cells.Count - 7];
            if (cellTaskMode.Text == "1")
            {
                cellTaskMode.Text = Resources.Lang.WMS_Common_WorkType_LiKu;
            }
            else if (cellTaskMode.Text == "0")
            {
                cellTaskMode.Text = Resources.Lang.WMS_Common_WorkType_PingKu;
            }

            LinkButton lbtnCreateOutBill = e.Row.FindControl("lbtnCreateOutBill") as LinkButton;
            lbtnCreateOutBill.Enabled = false;
            TableCell cell1 = e.Row.Cells[e.Row.Cells.Count - 5];

            var modOutAsn = context.OUTASN.Where(x => x.id == strKeyID).FirstOrDefault();

            if (modOutAsn.cstatus == "0")
            {
                if (OutAsnQuery.ValidateOutAsn_DIsAllCreateOutBill(strKeyID))
                {
                    lbtnCreateOutBill.Enabled = true;
                }
                //Roger 2013-5-2 17:48:49 合并后通知单不允许补单
                //if (!OutAsnQuery.IsMerge(strKeyID))
                if(modOutAsn.is_merge == 1 && !string.IsNullOrEmpty(modOutAsn.merge_id))
                {
                    lbtnCreateOutBill.Enabled = false;
                }
            }
            //20130617112810
            TableCell cell2 = e.Row.Cells[e.Row.Cells.Count - 4];
            if (modOutAsn.is_merge == 1)
            {
                lbtnCreateOutBill.Enabled = false;
            }
            //拼板出库不可以操作grid的任何按钮    
            //根据当前单据号ID而确认当前单据是否可以进行操作  
            OUTTYPE OUTASN_type = GetOUTTYPE(modOutAsn.itype.ToString());
            if (OUTASN_type != null && !string.IsNullOrEmpty(OUTASN_type.Is_Query.ToString()))
            {
                if (OUTASN_type.Is_Query.ToString() == "1")
                {
                    lbtnCreateOutBill.Enabled = false;
                }
            }          
        }
    }

    /// <summary>
    /// 列表排序
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdOUTASN_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortName = e.SortExpression;
        if (SortedField.Equals(sortName))
        {
            if (SortedAD.Equals(Ascending))
            {
                SortedAD = Descending;//取反
            }
            else
            {
                SortedAD = Ascending;
            }
        }
        else
        {
            SortedField = sortName;
            SortedAD = Ascending;
        }
        GridBind();
    }
    /// <summary>
    /// 手动获取ERP出库通知单数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSync_Click(object sender, EventArgs e)
    {
        string syncMsg = new DataSync().SyncOutManual();
        if (syncMsg == "0")
        {
            this.Alert(Resources.Lang.Common_SyncInterface_Success);//"获取ERP通知单成功！"
            this.btnSearch_Click(null, null);
        }
        else
        {
            this.Alert(Resources.Lang.Common_SyncInterface_Fail + syncMsg);//获取ERP通知单失败！
        }
    }
    #endregion

    #region 页面方法

    /// <summary>
    /// 获取对应列表行id
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <returns></returns>
    private string GetKeyIDS(int rowIndex)
    {
        return this.grdOUTASN.DataKeys[rowIndex].Values[0].ToString();
    }
    /// <summary>
    /// 列表数据绑定
    /// </summary>
    public void GridBind()
    {
        string userNo = WmsWebUserInfo.GetCurrentUser().UserNo;
        List<string> userCaseType = GetUserCaseTypeNew(userNo);

        using (var modContext = this.context)
        {
            var queryList = from p in modContext.V_OUTASNList
                            orderby p.dcreatetime descending
                            where 1 == 1
                            select p;
            if (!string.IsNullOrEmpty(txtCCREATEOWNERCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.ccreateownercode.Contains(txtCCREATEOWNERCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.OUTASN_D.Any(p => p.id == x.id && p.cinvcode.Contains(txtCinvcode.Text.Trim())));
            }
            if (!string.IsNullOrEmpty(txtRank_Final.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.OUTASN_D.Any(p => p.id == x.id && p.cinvcode.Contains("-")&&p.cinvcode.EndsWith("-"+txtRank_Final.Text.Trim())));
            }

            if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
            {
                DateTime createTimeFrom = Convert.ToDateTime(txtDCREATETIMEFrom.Text.Trim());
                queryList = queryList.Where(x => x.dcreatetime >= createTimeFrom);
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMETo.Text.Trim()))
            {
                DateTime createTimeTo = Convert.ToDateTime(txtDCREATETIMETo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.dcreatetime != null && x.dcreatetime < createTimeTo);
            }
            if (!string.IsNullOrEmpty(txtCAUDITPERSONCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cauditpersoncode.Contains(txtCAUDITPERSONCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtDAUDITDATEFrom.Text.Trim()))
            {
                DateTime dauditDateFrom = Convert.ToDateTime(txtDAUDITDATEFrom.Text.Trim());
                queryList = queryList.Where(x => x.dauditdate != null && x.dauditdate >= dauditDateFrom);
            }
            if (!string.IsNullOrEmpty(txtDAUDITDATETo.Text))
            {
                DateTime dauditDateTo = Convert.ToDateTime(txtDAUDITDATETo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.dauditdate < dauditDateTo);
            }
            if (!string.IsNullOrEmpty(txtCTICKETCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cticketcode.Contains(txtCTICKETCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCERPCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cerpcode.Contains(txtCERPCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCSTATUS.SelectedValue))
            {
                queryList = queryList.Where(x => x.cstatus.ToString().Equals(txtCSTATUS.SelectedValue));
            }
            /*
             * Note by Qamar 2020-11-24
            else
            {
                queryList = queryList.Where(x => !x.cstatus.Contains("3")); //全部条件下不显示已完成的单据 XL-137 20200520
            }
            */
            if (!string.IsNullOrEmpty(ddlSmt.SelectedValue))
            {
                queryList = queryList.Where(x => x.smttypeint.ToString().Equals(ddlSmt.SelectedValue));
            }
            if (!string.IsNullOrEmpty(txtSO.Text.Trim()))
            {
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.cso) && x.cso.Contains(txtSO.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtITYPE.SelectedValue))
            {
                queryList = queryList.Where(x => x.itype.ToString().Equals(txtITYPE.SelectedValue));
            }
            else {
                //过滤全部的类型
                queryList = queryList.Where(x => userCaseType.Contains(x.itype.ToString()));
            }
            /*else {
               queryList = queryList.Where(x => x.itype.ToString()!="2");
            }*/
            if (!string.IsNullOrEmpty(ddlIS_MERGE.SelectedValue))
            {
                queryList = queryList.Where(x => x.is_merge.ToString().Equals(ddlIS_MERGE.SelectedValue));
            }
            if (!string.IsNullOrEmpty(drpWorkType.SelectedValue))
            {
                queryList = queryList.Where(x => x.worktype.Equals(drpWorkType.SelectedValue));
            }
            //规格
            if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.OUTASN_D.Any(p => p.id == x.id && modContext.BASE_PART.Any(y => y.cspecifications.Contains(txtcspec.Text.Trim()) && p.cinvcode == y.cpartnumber)));
            }
            queryList = queryList.Distinct().AsQueryable();
            queryList = queryList.OrderByDescending(x => x.dcreatetime);

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;

            var data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
            flagList.Add(new Tuple<string, string>("itype", "OUTTYPE"));
            flagList.Add(new Tuple<string, string>("worktype", "WorkType"));
            flagList.Add(new Tuple<string, string>("cstatus", "OS"));
           

            var srcdata = GetGridSourceDataByList(data, flagList);

            this.grdOUTASN.DataSource = srcdata;
            this.grdOUTASN.DataBind();
        }
    }

    #endregion

    protected void BtnRevoke_Click(object sender, EventArgs e)
    {
        var msg="";
        int selectedCount = 0;       
   
        IGenericRepository<OUTASN> asnCon = new GenericRepository<OUTASN>(context);
        IGenericRepository<OUTASN_D> cond = new GenericRepository<OUTASN_D>(context);
        if (this.grdOUTASN.Rows.Count == 0)
        {
            Alert(Resources.Lang.FrmOUTASNList_Tips_XuanZeCheXiao);
            return;
        }

        for (int i = 0; i < this.grdOUTASN.Rows.Count; i++)
        {
            if (this.grdOUTASN.Rows[i].Cells[0].Controls[1] is CheckBox)
            {
                CheckBox chkSelect = (CheckBox)this.grdOUTASN.Rows[i].Cells[0].Controls[1];
                if (chkSelect.Checked)
                {
                    selectedCount = selectedCount + 1;

                    var id = this.grdOUTASN.DataKeys[i].Values[0].ToString();
                    var modOutAsn = context.OUTASN.Where(x => x.id == id).FirstOrDefault();
                    if (modOutAsn != null)
                    {
                        if (modOutAsn.cstatus == "0")
                        {
                            //根据当前单据号ID而确认当前单据是否可以进行操作  
                            OUTTYPE OUTASN_type = GetOUTTYPE(modOutAsn.itype.ToString());
                            if (OUTASN_type != null && !string.IsNullOrEmpty(OUTASN_type.Is_Query.ToString()))
                            {
                                if (OUTASN_type.Is_Query.ToString() == "1")
                                {
                                    msg = Resources.Lang.FrmOUTASNList_Tips_BuNengCheXiao;
                                    break;
                                }
                            }

                            var icount = 0;
                            //根据当前单据号ID而确认当前单据是否可以进行操作 
                            var indEntity = (from p in cond.Get()
                                                where p.id == id
                                                select p).ToList<OUTASN_D>();

                            foreach (var item in indEntity)
                            {
                                if (item.cstatus != "0")
                                {
                                    icount = icount + 1;
                                }
                            }

                            if (icount == 0)
                            {

                                var asnEntity = (from p in asnCon.Get()
                                                    where p.id == id
                                                    select p).FirstOrDefault<OUTASN>();

                                asnEntity.cstatus = "6";
                                asnCon.Update(asnEntity);
                                asnCon.Save();

                                foreach (var item in indEntity)
                                {
                                    item.cstatus = "3";
                                    cond.Update(item);
                                    cond.Save();
                                }
                            }
                            else
                            {
                                msg = Resources.Lang.FrmOUTASNList_Tips_BuNengCheXiao1;
                                break;
                            }
                        }
                        else {
                            msg = Resources.Lang.FrmOUTASNList_Tips_BuNengCheXiao2;
                            break;                        
                        }
                    }
                    else {
                        msg = Resources.Lang.FrmOUTASNList_Tips_ShuJuYiChang;
                        break;
                    }
                }
            } 
        }
        if (selectedCount > 0)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                Alert(msg);
            }
            else
            {
                Alert(Resources.Lang.FrmOUTASNList_Tips_CheXiaoChengGong);
                this.btnSearch_Click(null, null);
            }
        }
        else
        {
            Alert(Resources.Lang.FrmOUTASNList_Tips_XuanZeCheXiao);
        }
    }   
}