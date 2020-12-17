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
using DreamTek.ASRS.Business;
using DreamTek.ASRS.Business.SP.ProcedureModel;
using System.Data.SqlClient;


/// <summary>
/// 出库单详情页
/// </summary>
public partial class OUT_FrmOUTBILLEdit : WMSBasePage
{

    #region 页面属性

    public string ConFigvalue
    {
        set { ViewState["ConFigvalue"] = value; }
        get
        {
            if (ViewState["ConFigvalue"] != null)
            {
                return ViewState["ConFigvalue"].ToString();
            }
            return "";
        }
    }

    public string ASRSFig
    {
        get
        {
            if (ViewState["ASRSFig"] != null)
            {
                return ViewState["ASRSFig"].ToString();
            }
            return "";
        }
        set { ViewState["ASRSFig"] = value; }
    }

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

    public string OutType
    {
        get
        {
            if (ViewState["OutType"] != null)
            {
                return ViewState["OutType"].ToString();
            }
            return "";
        }
        set { ViewState["OutType"] = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool IsExistTempOutBill_D
    {
        get
        {
            if (ViewState["IsExistTempOutBill_D"] != null)
            {
                return Convert.ToBoolean(ViewState["IsExistTempOutBill_D"].ToString());
            }
            return false;
        }
        set { ViewState["IsExistTempOutBill_D"] = value; }
    }

    public string WorkType
    {
        get { return this.hiddWorkType.Value; }
        set { this.hiddWorkType.Value = value.ToString(); }
    }
    /// <summary>
    /// 仅查询
    /// </summary>
    public bool IsQuery
    {
        get
        {
            if (ViewState["IsQuery"] != null)
            {
                return bool.Parse(ViewState["IsQuery"].ToString());
            }
            return false;
        }
        set { ViewState["IsQuery"] = value; }
    }
    #endregion

    #region 页面加载

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtCTICKETCODE.Enabled = false;
        this.btnSetCARGOSPACE.Enabled = false;
        ShowBASE_CLIENTDiv1.SetCompName = this.txtCCLIENT.ClientID;
        ShowBASE_CLIENTDiv1.SetORGCode = this.txtCCLIENTCODE.ClientID;
        ucOutASN_Div.SetCompName = txtCOUTASNID.ClientID;
        ucOutASN_Div.SetORGCode = hiddenGuid.ClientID;

        if (this.IsPostBack == false)
        {
            PageLoadFunction();
        }       
       
        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
        this.btnDeliverAndUpdateStockCurrent.Attributes["onclick"] = this.GetPostBackEventReference(this.btnDeliverAndUpdateStockCurrent) + ";this.disabled=true;";
        this.btnASRS.Attributes["onclick"] = this.GetPostBackEventReference(this.btnASRS) + ";this.disabled=true;";
        btnDelete.Attributes["onclick"] = this.GetPostBackEventReference(this.btnDelete) + ";this.disabled=true;";
        btnCreateOutBill.Attributes["onclick"] = this.GetPostBackEventReference(this.btnCreateOutBill) + ";this.disabled=true;";
    }

    /// <summary>
    /// 页面加载方法
    /// </summary>
    private void PageLoadFunction()
    {
        btnASRS.Style.Remove("disabled");
        this.btnASRS.Enabled = false;
        btnDeliverAndUpdateStockCurrent.Style.Remove("disabled");
        this.btnDeliverAndUpdateStockCurrent.Enabled = false;
        this.InitPage();
        if (this.Operation() == SYSOperation.Modify)
        {
            ShowData();
        }
        else if (this.Operation() == SYSOperation.Preserved1)
        {
            ShowData();
            this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmOUTBILL_DEdit.aspx", SYSOperation.New, this.KeyID) + "&Outasn_id=" + this.hiddenGuid.Value + "','" + Resources.Lang.FrmOUTBILLEdit_Tips_ChuKuMingXi + "','OUTBILL_D');");
        }
        else
        {
            this.txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
            this.txtID.Text = Guid.NewGuid().ToString();
            this.txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtDINDATE.Text = this.txtDCREATETIME.Text;
            btnDelete0.Enabled = false;       
        }
    }

    /// <summary>
    /// 页面初始化
    /// </summary>
    public void InitPage()
    {
        //NOTE by Mark, 09/22
        //*** 必需要做 權限平台 才能顯示BTN
        btn0922.Visible = true;


        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTBILL');return false;";
        this.grdOUTBILL_D.DataKeyNames = new string[] { "IDS" };
        this.txtCCLIENT.Attributes["onclick"] = "Show('" + ShowBASE_CLIENTDiv1.GetDivName + "');";
        this.txtCCLIENTCODE.Attributes["onclick"] = "Show('" + ShowBASE_CLIENTDiv1.GetDivName + "');";

        txtCOUTASNID.Attributes["onclick"] = "Show('" + ucOutASN_Div.GetDivName + "');";

        if (this.Operation() == SYSOperation.New)
        {
            Help.DropDownListDataBind(GetOutType(false), this.drOType, "", "FUNCNAME", "EXTEND1", "");
        }
        else
        {
            Help.DropDownListDataBind(GetOutType(true), this.drOType, "", "FUNCNAME", "EXTEND1", "");
        }
        Help.DropDownListDataBind(new SysParameterList().GetSys_ParameterByFLAG_TYPE("OUTBILL"), dplCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
        if (this.Operation() == SYSOperation.New)
        {
            this.btnDelete.Visible = false;
            btnNew.Visible = false;
            btnDelete0.Visible = false;

        }
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.Operation() == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.FrmOUTASNEdit_ShengPi;
        }
        ConFigvalue = this.GetConFig("000002");
        //是否显示ASRS 1显示 0 不显示
        ASRSFig = this.GetConFig("000006");
        if (ASRSFig == "1")
        {
            btnDeliverAndUpdateStockCurrent.Visible = false;
            btnASRS.Visible = true;
            //btnOutput.Visible = true;
            //grdOUTBILL_D.Columns[10].Visible = true;
            //grdOUTBILL_D.Columns[11].Visible = true;
            //grdOUTBILL_D.Columns[12].Visible = true;
        }
        else
        {
            btnDeliverAndUpdateStockCurrent.Visible = true;
            btnASRS.Visible = false;
            btnOutput.Visible = false;
            //grdOUTBILL_D.Columns[10].Visible = false;
            //grdOUTBILL_D.Columns[11].Visible = false;
            //grdOUTBILL_D.Columns[12].Visible = false;
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
        SaveToDB(sender);
        btnSave.Enabled = true;
        btnSave.Style.Remove("disabled");
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //Roger 2013-5-2 14:31:49
        //增加出库单状态校验
        if (!CheckStatue("1", "1"))
        {
            return;
        }
        try
        {
            for (int i = 0; i < this.grdOUTBILL_D.Rows.Count; i++)
            {
                if (this.grdOUTBILL_D.Rows[i].Cells[0].FindControl("chkSelect") is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdOUTBILL_D.Rows[i].Cells[0].FindControl("chkSelect");//20130614093155
                    if (chkSelect.Checked)
                    {
                        #region 调用存储过程
                        List<string> SparaList = new List<string>();
                        SparaList.Add("@pID:" + this.grdOUTBILL_D.DataKeys[i].Values[0].ToString());
                        SparaList.Add("@pUserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                        SparaList.Add("@pReserveField1:" + "");
                        SparaList.Add("@pReserveField2:" + "");
                        SparaList.Add("@pRetCode:" + "");
                        SparaList.Add("@pRetMsg:" + "");
                        string[] Result = DBHelp.ExecuteProc("proc_delete_outbill", SparaList);
                        if (Result.Length == 1)//调用存储过程有错误
                        {
                            throw new Exception(Result.ToString());
                        }
                        else if (Result[0] != "0")
                        {
                            throw new Exception(Result[1].ToString());
                        }
                        #endregion
                    }
                }
            }
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteSuccess);
            this.GridBind();
        }
        catch (Exception E)
        {
            this.GridBind();//删除其中某一条数据出错后，需要重新绑定数据，否则数据会残留在界面上
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + E.Message.ToJsString());
        }
    }

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
        this.GridBind();
    }

    /// 打印功能
    /// <summary>
    /// 打印功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string printid = this.txtID.Text.Trim();
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmOUTBILLEdit_Print.aspx", SYSOperation.New, printid) + "','" + Resources.Lang.FrmOUTBILLEdit_DaYingChuKuDan + "','BAR_REPACK',800,600);");
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNew_Click(object sender, EventArgs e)
    {
        //增加出库单状态校验
        if (!CheckStatue("1", "1"))
        {
            return;
        }
        Response.Redirect(BuildRequestPageURL("FrmOUTBILLEdit.aspx", SYSOperation.Preserved1, txtID.Text.Trim()));
    }

    /// <summary>
    /// AS/RS出库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOutput_Click(object sender, EventArgs e)
    {

        try
        {

            bool dbflag = false;
            string errmsg;

            if (WmsDBCommon_ASRS.DataBaseConn(out errmsg))
            {
                dbflag = true;
            }

            //测试连接成功
            if (dbflag)
            {

                for (int i = 0; i < this.grdOUTBILL_D.Rows.Count; i++)
                {
                    if (this.grdOUTBILL_D.Rows[i].Cells[0].FindControl("chkSelect") is CheckBox)
                    {
                        CheckBox chkSelect = (CheckBox)this.grdOUTBILL_D.Rows[i].Cells[0].FindControl("chkSelect");//20130614093155
                        if (chkSelect.Checked)
                        {
                            string pID = this.grdOUTBILL_D.DataKeys[i].Values[0].ToString();

                            string msg = string.Empty;
                            IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
                            OUTBILL_D entity = new OUTBILL_D();
                            var caseList = from p in con.Get()
                                           where p.ids == pID
                                           select p;
                            entity = caseList.ToList().FirstOrDefault();
                            PROC_ASRS_OutChangeStatus proc = new PROC_ASRS_OutChangeStatus();
                            proc.Ids = pID;
                            proc.Space = entity.pallet_code;
                            proc.Lineid = entity.lineid.ToString();
                            proc.UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                            proc.Execute();
                            if (proc.ReturnValue == 1)
                            {
                                Alert(proc.ErrorMessage);
                            }
                            //if (!WmsDBCommon_ASRS.ASRS_OutChangeStatus(pID, entity.pallet_code, out msg, entity.lineid.ToString(), Server.MapPath("~/EQ2008_Dll_Set.ini")))
                            //{
                            //    Alert(msg);
                            //}
                            else
                            {
                                this.Alert(Resources.Lang.FrmOUTBILLEdit_CaoZuoSuccess);//操作成功！
                            }
                            btnSearch_Click(this.btnSearch, EventArgs.Empty);
                        }
                        else
                        {
                            this.Alert(Resources.Lang.FrmOUTBILLEdit_ZhiShaoXuanZe);//至少选择一条数据！
                            return;
                        }
                    }
                }
                this.GridBind();

            }
            WmsDBCommon_ASRS.DBConnClose();
        }
        catch (Exception ex)
        {
            this.Alert(Resources.Lang.FrmOUTASNList_YiChang+":" + ex.Message.ToJsString());
            WmsDBCommon_ASRS.DBConnClose();
        }

    }

    /// <summary>
    /// 批量取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        try
        {

            bool dbflag = false;
            string errmsg;

            if (WmsDBCommon_ASRS.DataBaseConn(out errmsg))
            {
                dbflag = true;
            }

            if (dbflag)
            {

                for (int i = 0; i < this.grdOUTBILL_D.Rows.Count; i++)
                {
                    if (this.grdOUTBILL_D.Rows[i].Cells[0].FindControl("chkSelect") is CheckBox)
                    {
                        CheckBox chkSelect = (CheckBox)this.grdOUTBILL_D.Rows[i].Cells[0].FindControl("chkSelect");//20130614093155
                        if (chkSelect.Checked)
                        {
                            string pID = this.grdOUTBILL_D.DataKeys[i].Values[0].ToString();
                            string msg = string.Empty;

                            PROC_ASRS_OutCancel proc = new PROC_ASRS_OutCancel();
                            proc.Ids = pID;
                            proc.Execute();
                            if (proc.ReturnValue == 1)
                            {
                                Alert(proc.ErrorMessage);
                            }
                            //if (!WmsDBCommon_ASRS.ASRS_OutCancel(pID, out msg))
                            //{
                            //    Alert(msg);
                            //}
                            else
                            {
                                this.Alert(Resources.Lang.FrmOUTBILLEdit_CaoZuoSuccess);//操作成功！
                            }
                            btnSearch_Click(this.btnSearch, EventArgs.Empty);
                        }
                        else
                        {
                            this.Alert(Resources.Lang.FrmOUTBILLEdit_ZhiShaoXuanZe);//至少选择一条数据！
                            return;
                        }
                    }
                }
                this.GridBind();
                WmsDBCommon_ASRS.DBConnClose();
            }
        }
        catch (Exception ex)
        {
            this.Alert(Resources.Lang.FrmOUTASNList_YiChang + ":" + ex.Message.ToJsString());
            WmsDBCommon_ASRS.DBConnClose();
        }
    }

    /// 刷新
    /// <summary>
    /// 刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRefres_Click(object sender, EventArgs e)
    {
        try
        {
            bool dbflag = false;
            string errmsg;

            if (WmsDBCommon_ASRS.DataBaseConn(out errmsg))
            {
                dbflag = true;
            }

            if (dbflag)
            {

                string pID = txtID.Text.Trim();
                //抓取更新SN的状态
                string msg = string.Empty;
                if (!WmsDBCommon_ASRS.ASRS_OutRefresh_All(pID, out msg))
                {
                    Alert(msg);
                }
                else
                {
                    Alert(Resources.Lang.FrmOUTBILLEdit_Tips_ShuaXinChengGong);
                }
            }
            this.GridBind();
            WmsDBCommon_ASRS.DBConnClose();
        }

        catch (Exception ex)
        {
            this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_ShuaXinShiBai + ex.Message.ToJsString());
            WmsDBCommon_ASRS.DBConnClose();
        }
    }

    /// <summary>
    /// 设置为同一储位
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSetCARGOSPACE_Click(object sender, EventArgs e)
    {
        #region 调用存储过程
        List<string> SparaList = new List<string>();
        SparaList.Add("@P_Bill_Id:" + txtID.Text.Trim());
        SparaList.Add("@P_ReturnValue:" + "");
        SparaList.Add("@INFOTEXT:" + "");
        string[] Result = DBHelp.ExecuteProc("Proc_SetBillCARGOSPACE", SparaList);
        if (Result.Length == 1)
        {

            Alert(Resources.Lang.FrmOUTBILLEdit_Tips_SheZhiShiBai);
        }
        else if (Result[0] == "1")
        {
            Alert(Resources.Lang.FrmOUTBILLEdit_Tips_SheZhiShiBai);
        }
        else
        {
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        #endregion
    }

    /// <summary>
    /// 翻页控件翻页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    /// <summary>
    /// 列表行数据绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdOUTBILL_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            TextBox txtGv_CPOSITIONCODE = e.Row.FindControl("txtGv_CPOSITIONCODE") as TextBox;
            TextBox txtGv_IQUANTITY = e.Row.FindControl("txtGv_IQUANTITY") as TextBox;
            if(!string.IsNullOrEmpty(txtGv_IQUANTITY.Text.Trim())){
                txtGv_IQUANTITY.Text = Convert.ToDecimal(txtGv_IQUANTITY.Text.Trim()).ToString("f2");
            }

            TextBox txtGv_LINE_QTY = e.Row.FindControl("txtGv_LINE_QTY") as TextBox;

            // WL 20160511 修改状态 
            Button LinkPALLET_STATUS_I = (Button)e.Row.FindControl("LinkPALLET_STATUS_I");
            LinkPALLET_STATUS_I.Style.Add("color", "Blue");

            //修改状态
            Button LinkASRS_STATUS = (Button)e.Row.FindControl("LinkASRS_STATUS");
            LinkButton lnkStatus = (LinkButton)e.Row.FindControl("lnkStatus");
            LinkASRS_STATUS.Style.Add("color", "Blue");
            Button btnRefresh = (Button)e.Row.FindControl("btnRefresh");
            HyperLink hlToHande_Info = (HyperLink)e.Row.FindControl("hlToHande_Info");
            hlToHande_Info.NavigateUrl = "#";
            if (!hlToHande_Info.Text.Equals(""))
            {
                decimal HandeQty = Convert.ToDecimal(hlToHande_Info.Text.Trim());
                hlToHande_Info.Text = HandeQty.ToString("f2");
                if (HandeQty > 0)
                {
                    this.OpenFloatWin(hlToHande_Info,
                                      BuildRequestPageURL("FrmOutHandOverList.aspx", SYSOperation.Modify, strKeyID),
                                      Resources.Lang.FrmOUTBILLEdit_Tips_JiaoFuMingXi, "outhandover", 700, 470);//交付明细详情
                }
            }

            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmOUTBILL_DEdit.aspx", SYSOperation.Modify, txtID.Text.Trim() + "&IDS=" + strKeyID + "&OutType=" + OutType), Resources.Lang.FrmOUTASNList_Tips_ChuKuDan, "OUTBILL_D");//出库单
            //WIP Issue : 35 、Sales order issue ：33
            if ((Status.Length > 0 && Status.Equals("0")) && !IsExistTempOutBill_D)//|| OutType == "35" || OutType == "33"
            {

                #region 注册代码
                //2015-12-02  出库单自动带出出库储位   [ASRS]专用
                string script = "";
                if (ASRSFig == "1")
                {
                    script = string.Format(@"<script type=""text/javascript"">
                                                     $(function () {{
                                                          
                                                         $(""#{0}"").autocomplete({{
                                                             source: function (request, response) {{
                                                                 //alert(request.term);
                                                                 //alert($(this).attr(""CINVCODE""));
                                                                 $.ajax({{
                                                                     url: ""../Server/Cargospan.ashx?PositionCode="" + request.term + ""&CinvCode={1}&Type=Out&Sum={2}&ASRSFig=1"",
                                                                     dataType: ""xml"",
                                                                     error: function (XMLHttpRequest, textStatus, errorThrown) {{
                                                                         //alert(XMLHttpRequest.status);
                                                                         //alert(XMLHttpRequest.readyState);
                                                                         //alert(errorThrown);
                                                                         //alert(textStatus);
                                                                     }},
                                                                     success: function (data) {{
                                                                         //alert(data);
                                                                         response($(""reuslt"", data).map(function () {{

                                                                             return {{
                                                                                 value: $(""CPOSITIONCODE"", this).text(),
                                                                                 label: $(""CPOSITIONCODE"", this).text() + "" ["" + $(""IQTY"", this).text() + ""]""+ "" ["" + $(""SNCODE"", this).text() + ""]""+ "" ["" + $(""DATECODE"", this).text() + ""]"",
                                                                                 id: $(""CPOSITION"", this).text()
                                                                             }}
                                                                         }}));
                                                                         //alert(data.xml);
                                                                     }}
                                                                 }});
                                                             }},
                                                             autoFocus: true,
                                                             minLength: 0,
                                                             minChars:0,
                                                             delay: 0,
                                                             select: function (event, ui) {{   
                                                                  //alert(ui.item.value);
                                                                  var i = Math.random() * 10000 + 1;
                                                                  $.get(""../BASE/SubmitDate.aspx?I=i"",
                                                                        {{ Type: 'Out', Special: '" + HiddenSpecial.Value.Trim() + "', DataType: 'PositionCode', Ids: '" + strKeyID.Trim() + @"', Qty: 0, Line_Qty: 0, PositionCode: ui.item.value }},
                                                                        function (data) {{
                                                                            $(""#showMsgTd"").html(data);
                                                                        }});
                                                             }},
                                                             open: function () {{
                                                                 $(this).removeClass(""ui-corner-all"").addClass(""ui-corner-top"");
                                                             }},
                                                             close: function () {{
                                                                 $(this).removeClass(""ui-corner-top"").addClass(""ui-corner-all"");
                                                             }}
                                                         }});
                                                     }});
                                                    </script> ",
                                           txtGv_CPOSITIONCODE.ClientID,
                                           e.Row.Cells[3].Text.Trim(), txtGv_IQUANTITY.Text.Trim());
                }
                else
                {
                    script = string.Format(@"<script type=""text/javascript"">
                                                     $(function () {{
                                                          
                                                         $(""#{0}"").autocomplete({{
                                                             source: function (request, response) {{
                                                                 //alert(request.term);
                                                                 //alert($(this).attr(""CINVCODE""));
                                                                 $.ajax({{
                                                                     url: ""../Server/Cargospan.ashx?PositionCode="" + request.term + ""&CinvCode={1}&Type=Out&ASRSFig=0"",
                                                                     dataType: ""xml"",
                                                                     error: function (XMLHttpRequest, textStatus, errorThrown) {{
                                                                         //alert(XMLHttpRequest.status);
                                                                         //alert(XMLHttpRequest.readyState);
                                                                         //alert(errorThrown);
                                                                         //alert(textStatus);
                                                                     }},
                                                                     success: function (data) {{
                                                                         //alert(data);
                                                                         response($(""reuslt"", data).map(function () {{

                                                                             return {{
                                                                                 value: $(""CPOSITIONCODE"", this).text(),
                                                                                 label: $(""CPOSITIONCODE"", this).text() + "" ["" + $(""IQTY"", this).text() + ""]"",
                                                                                 id: $(""CPOSITION"", this).text()
                                                                             }}
                                                                         }}));
                                                                         //alert(data.xml);
                                                                     }}
                                                                 }});
                                                             }},
                                                             autoFocus: true,
                                                             minLength: 0,
                                                             minChars:0,
                                                             delay: 0,
                                                             select: function (event, ui) {{   
                                                                  //alert(ui.item.value);
                                                                  var i = Math.random() * 10000 + 1;
                                                                  $.get(""../BASE/SubmitDate.aspx?I=i"",
                                                                        {{ Type: 'Out', Special: '" + HiddenSpecial.Value.Trim() + "', DataType: 'PositionCode', Ids: '" + strKeyID.Trim() + @"', Qty: 0, Line_Qty: 0, PositionCode: ui.item.value }},
                                                                        function (data) {{
                                                                            $(""#showMsgTd"").html(data);
                                                                        }});
                                                             }},
                                                             open: function () {{
                                                                 $(this).removeClass(""ui-corner-all"").addClass(""ui-corner-top"");
                                                             }},
                                                             close: function () {{
                                                                 $(this).removeClass(""ui-corner-top"").addClass(""ui-corner-all"");
                                                             }}
                                                         }});
                                                     }});
                                                    </script> ",
                                           txtGv_CPOSITIONCODE.ClientID,
                                           e.Row.Cells[3].Text.Trim());
                }
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), txtGv_CPOSITIONCODE.ClientID, script);

                #endregion

                txtGv_IQUANTITY.Attributes["onBlur"] = "submitData('Out','Qty','" + strKeyID.Trim() + "',this.value,'','',this);";

                //20130617112810
                if (HiddenSpecial.Value.Trim().Equals("1"))
                {
                    txtGv_CPOSITIONCODE.Enabled = false;
                    txtGv_LINE_QTY.Enabled = false;
                }

                //验证是否可以超发
                if (e.Row.Cells[4].Text.Trim().Length > 4)
                {
                    string CINVCODE = e.Row.Cells[4].Text.Trim();
                    txtGv_LINE_QTY.Attributes["onBlur"] = "submitData('Out','Line_Qty','" + strKeyID.Trim() + "','',this.value,'',this);";
                }
            }
            else
            {
                txtGv_CPOSITIONCODE.Enabled = false;
                txtGv_IQUANTITY.Enabled = false;
                txtGv_LINE_QTY.Enabled = false;
            }
            if (ASRSFig == "1")
            {
                #region ASRS
                if (e.Row.Cells[13].Text != "0")
                {
                    txtGv_CPOSITIONCODE.Enabled = false;
                    txtGv_IQUANTITY.Enabled = false;
                    txtGv_LINE_QTY.Enabled = false;
                }

                if (e.Row.Cells[13].Text == "7" || Status.Equals("2") || Status.Equals("3"))
                {
                    LinkASRS_STATUS.Enabled = false;
                }

                if (e.Row.Cells[13].Text == "0")
                {
                    btnRefresh.Enabled = false;
                    //当储位默认为空时，绑定符合条件的储位
                    if (txtGv_CPOSITIONCODE.Text == "")
                    {
                        //2015-12-02 储位编码默认为库存中最小符合出库单的数量   [ASRS专用]
                        DataTable pdat = null;
                        pdat = new Base_Cargospace().GetCargoSpaceListByStock(e.Row.Cells[3].Text.Trim(), "", true, 15, txtGv_IQUANTITY.Text.Trim());
                        if (pdat.Rows.Count > 0)
                        {
                            txtGv_CPOSITIONCODE.Text = pdat.Rows[0]["cpositioncode"].ToString();
                        }
                    }
                }
                switch (LinkASRS_STATUS.Text.Trim())//10
                {
                    case "0":
                        LinkASRS_STATUS.Text = "[" + Resources.Lang.FrmOUTBILLEdit_ASRSButton_ChuKu + "]";
                        break;
                    case "1":
                        LinkASRS_STATUS.Text = "[" + Resources.Lang.WMS_Common_Button_Cancel + "]";
                        LinkASRS_STATUS.Enabled = false;
                        break;
                    case "7":

                        btnCancle.Enabled = false;
                        btnOutput.Enabled = false;
                        //  btnRefres.Enabled = false;
                        LinkASRS_STATUS.Visible = false;
                        break;
                    case "8":
                        LinkASRS_STATUS.Text = "[" + Resources.Lang.FrmOUTBILLEdit_ASRSButton_ChongShi + "]";
                        break;

                }
                LinkPALLET_STATUS_I.Enabled = false;
                LinkPALLET_STATUS_I.Style.Add("color", "");
                if (new InBill().CheckBASE_CARGOSPACE(e.Row.Cells[10].Text.ToString()))
                {
                    //OUT_FrmOUTBILL_DListQuery OUT_Bill_D = new OUT_FrmOUTBILL_DListQuery();
                    //OUT_Bill_D.GetList(strKeyID,true,0,1)
                    HiddenField hdGOBACK = e.Row.FindControl("hdISGOBACK") as HiddenField;
                    if (hdGOBACK != null && hdGOBACK.Value.Equals("1")) //(e.Row.Cells[11].Text == "7")
                    {
                        LinkPALLET_STATUS_I.Enabled = false;
                        LinkPALLET_STATUS_I.Style.Add("color", "");
                    }
                    else
                    {
                        LinkPALLET_STATUS_I.Enabled = true;
                        LinkPALLET_STATUS_I.Style.Add("color", "blue");
                    }
                }
                LinkPALLET_STATUS_I.Text = "[" + Resources.Lang.FrmOUTBILLEdit_Button_FanKu + "]";

                switch (lnkStatus.Text)
                {
                    case "0":
                        lnkStatus.Text = Resources.Lang.FrmOUTBILLEdit_ASRSStatus_WeiChuLi;//未处理
                        break;
                    case "1":
                        lnkStatus.Text = Resources.Lang.FrmOUTBILLEdit_ASRSStatus_YunZuoZhong;//运作中
                        break;
                    case "7":
                        lnkStatus.Text = Resources.Lang.FrmOUTBILLEdit_ASRSStatus_WanCheng;//处理完成
                        lnkStatus.Enabled = false;
                        break;
                    case "8":
                        lnkStatus.Text = Resources.Lang.FrmOUTBILLEdit_ASRSStatus_YiChang;//处理异常
                        break;
                }
                #endregion
            }
            //拼板出库不可以操作grid的任何按钮             
            if(IsQuery)//if (drOType.SelectedValue.Trim().Equals("1303") || drOType.SelectedValue.Trim().Equals("1302"))  //1303 拼板出库  1302库存调整出库
            {
                //chk.Enabled = false;
                LinkASRS_STATUS.Enabled = false;
                lnkStatus.Enabled = false;
            }
            

        }
    }

    /// <summary>
    /// 出库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkASRS_STATUS_Click(object sender, EventArgs e)
    {
        (sender as Button).Enabled = false;

        /*
         * Note by Qamar 2020-11-19
         * 加入卡控, 目標是解決出站口有貨時, 繼續下出庫命令可能造成的問題.
         * palletcode若為NULL, 代表未下出庫命令
         * #Rule1: 如果有"未處理"或"已交付"的出庫單, 且palletcode不為NULL, 則不允許出庫
         * #Rule2: 如果有"已完成"的出庫單, 且cclient為"~", 則不允許出庫
         * #Rule3: 如果存在未扣帳的返庫命令, 則不允許出庫
        */
        int shouldBlock = 0; //0阻擋出庫  1有未完成且出庫的出庫單  2有部分出庫但未返庫的出庫單  3有未扣帳的返庫命令

        /* 判斷出庫單 */
        using (var modContext = this.context)
        {
            IGenericRepository<V_OUTBILL> entity = new GenericRepository<V_OUTBILL>(context);
            var queryList = from p in modContext.V_OUTBILL
                            orderby p.dcreatetime descending
                            where p.cticketcode != txtCTICKETCODE.Text.Trim()
                            select p;
            //queryList = queryList.Where(x => x.cstatus == "0" || x.cstatus == "1"); //狀態須為 未處理 或 已交付
            //var rule1list = queryList.Where(x => (x.cstatus == "0" || x.cstatus == "1") && x.sn_code != null);
            //var rule2list = queryList.Where(x => x.cstatus == "2" && x.cclient == "~");
            /* debug用
            var enumeator = rule1list.GetEnumerator();
            enumeator.MoveNext();
            var testrow = enumeator.Current;
            */
            if (queryList.Where(x => (x.cstatus == "0" || x.cstatus == "1") && x.sn_code != null).Count() != 0)
                shouldBlock = 1;
            else if (queryList.Where(x => x.cstatus == "2" && x.cclient == "~").Count() != 0)
                shouldBlock = 2;
            else
            {
                var queryList2 = from p in modContext.CMD_MST
                            orderby p.TaskNO descending
                            where (p.REMARK == "返库" && p.BILLINGSTATUS == 0) //未扣帳
                            select p;
                if (queryList2.Count() != 0)
                    shouldBlock = 3;
            }
        }

        if (shouldBlock == 1)
        {
            this.Alert("存在未完成且有出庫的出庫單, 無法出庫.");
        }
        else if (shouldBlock == 2)
        {
            this.Alert("存在未返庫的揀料出庫, 無法出庫.");
        }
        else if (shouldBlock == 3)
        {
            //this.Alert("存在未扣帳的返庫命令, 無法出庫."); //使用者會看不懂
            this.Alert("存在運作中的返庫命令, 無法出庫.");
        }
        else
        {
            string msg = string.Empty;
            string id = txtID.Text;
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_OUTBillID:" + id);
            SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
            SparaList.Add("@P_Type:" + 0);
            SparaList.Add("@P_return_Value:" + "");
            SparaList.Add("@P_ErrText:" + "");

            string[] result = DBHelp.ExecuteProc("PRC_OUTCONFIRM_BD", SparaList);

            if (result.Length == 1)//调用存储过程有错误
            {
                msg += result[0];
            }
            else if (result[0] == "0") //出库成功!
            {
                msg += Resources.Lang.FrmOUTASNList_Tips_ChuKuChengGong;//FrmOUTASNList_Tips_ChuKuChengGong
                /*
                 * Note by Qamar 2020-11-20
                 * 初始cclient=null
                 * 部分出庫成功cclient="~"
                 * 部分出庫返庫成功cclient=""
                */
                IGenericRepository<OUTBILL> con = new GenericRepository<OUTBILL>(context);
                var entity = (from p in con.Get() where p.id == id select p).ToList().FirstOrDefault<OUTBILL>(); ;
                if (entity.idefine5 == 0) //部分出庫
                    entity.cclient = "~";
                con.Update(entity);
                con.Save();
            }
            else
            {
                msg += result[1];
            }
            this.Alert(msg);
        }
        (sender as Button).Enabled = true;
        btnSearch_Click(this.btnSearch, EventArgs.Empty);

    }

    /// <summary>
    /// 刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        string ids = (sender as Button).CommandArgument;

        //抓取更新SN的状态
        string msg = string.Empty;
        if (!WmsDBCommon_ASRS.ASRS_OutRefresh(ids, out msg))
        {
            Alert(msg);
        }
        btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateOutBill_Click(object sender, EventArgs e)
    {
        string msg = "";
        btnCreateOutBill.Enabled = false;

        #region 调用存储过程
        List<string> SparaList = new List<string>();
        SparaList.Add("@P_Outasn_id:" + hiddenGuid.Value.Trim());
        SparaList.Add("@P_OutBill_id:" + txtID.Text.Trim());
        SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
        SparaList.Add("@P_ReturnValue:" + "");
        SparaList.Add("@INFOTEXT:" + "");
        string[] Result = DBHelp.ExecuteProc("Proc_CreateOutBillByOutAsn", SparaList);
        if (Result.Length == 1)
        {

            msg = Resources.Lang.FrmOUTBILLEdit_Tips_ChuKuDanShiBai;//出库单生成失败！
        }
        else if (Result[0] == "1")
        {
            msg = Resources.Lang.FrmOUTBILLEdit_Tips_ChuKuDanShiBai;//出库单生成失败！
        }
        else
        {
            btnSearch_Click(sender, e);
        }
        #endregion

        if (msg.Length > 0)
        {
            Alert(msg);
        }
        btnCreateOutBill.Enabled = true;
    }

    /// <summary>
    /// 交付扣帐
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDeliverAndUpdateStockCurrent_Click(object sender, EventArgs e)
    {     
        //增加出库单状态校验
        if (!CheckStatue("1", "0"))
        {
            btnDeliverAndUpdateStockCurrent.Style.Remove("disabled");
            return;
        }

        this.btnDeliverAndUpdateStockCurrent.Enabled = false;

        string guid = Guid.NewGuid().ToString();
        string msg = "";

        #region 调用存储过程
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Proc_DeliverAndOUTStockCurrent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@P_Guid", guid));
                cmd.Parameters.Add(new SqlParameter("@P_OutBill_Id", txtID.Text.Trim()));
                cmd.Parameters.Add(new SqlParameter("@P_UserNo", WmsWebUserInfo.GetCurrentUser().UserNo));

                //返回值
                var returnValue = new SqlParameter("@P_ReturnValue", "");
                returnValue.Direction = ParameterDirection.Output;
                returnValue.SqlDbType = SqlDbType.Float;
                cmd.Parameters.Add(returnValue);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (returnValue.Value.ToString() == "1")
                {
                    msg = new ErrorMsg().GetErrorMsg(guid);//" 操作失败！";
                    this.btnDeliverAndUpdateStockCurrent.Enabled = true;
                    btnDeliverAndUpdateStockCurrent.Style.Remove("disabled");
                }
                else
                {
                    msg = Resources.Lang.FrmOUTBILLEdit_CaoZuoSuccess;// " 操作成功！";
                    PageLoadFunction();
                }
            }
        }
        catch (Exception ex)
        {
            msg = Resources.Lang.FrmOUTBILLEdit_Tips_KouZhangShiBai + ":" + ex.Message;//扣账失败:
        }

        //List<string> SparaList = new List<string>();
        //SparaList.Add("@P_Guid:" + guid);
        //SparaList.Add("@P_OutBill_Id:" + txtID.Text.Trim());
        //SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
        //SparaList.Add("@P_ReturnValue:" + "");
        ////SparaList.Add("@INFOTEXT:" + "");
        //string[] Result = DBHelp.ExecuteProc("Proc_DeliverAndOUTStockCurrent", SparaList);
        //if (Result.Length == 1)
        //{

        //    msg = new ErrorMsg().GetErrorMsg(guid);//" 操作失败！";
        //    this.btnDeliverAndUpdateStockCurrent.Enabled = true;
        //    //20130702084429
        //    btnDeliverAndUpdateStockCurrent.Style.Remove("disabled");
        //}
        //else if (Result[0] == "1")
        //{
        //    msg = new ErrorMsg().GetErrorMsg(guid);//" 操作失败！";
        //    this.btnDeliverAndUpdateStockCurrent.Enabled = true;
        //    //20130702084429
        //    btnDeliverAndUpdateStockCurrent.Style.Remove("disabled");
        //}
        //else
        //{
        //    msg = " 操作成功！";
        //    //btnSearch_Click(this.btnSearch, EventArgs.Empty);
        //    PageLoadFunction();
        //}
        #endregion
        Alert(msg);
    }

    /// <summary>
    /// 交付扣帐
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnASRS_Click(object sender, EventArgs e)
    {
        //增加出库单状态校验
        if (!CheckStatue("1", "0"))
        {
            btnASRS.Style.Remove("disabled");
            this.btnASRS.Enabled = true;
            return;
        }

        this.btnASRS.Enabled = false;
        string guid = Guid.NewGuid().ToString();
        string msg = "";
        #region 调用存储过程
        List<string> SparaList = new List<string>();
        SparaList.Add("@P_Guid:" + guid);
        SparaList.Add("@P_OutBill_Id:" + txtID.Text.Trim());
        SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
        string Result = DBHelp.ExecuteProcReturnValue("Proc_DeliverAndOUTStockCurrent", SparaList, "@P_ReturnValue");
        if (Result == "1")
        {

            msg = new ErrorMsg().GetErrorMsg(guid);//" 操作失败！";
            this.btnASRS.Enabled = true;
            //20130702084429
            btnASRS.Style.Remove("disabled");
        }
        else
        {
            msg = Resources.Lang.FrmOUTBILLEdit_CaoZuoSuccess;//操作成功！
            //btnSearch_Click(this.btnSearch, EventArgs.Empty);
            PageLoadFunction();

            //pan gao led web test
            //string ledWEB = ConfigurationManager.AppSettings["LEDWEB"];
            //if (ledWEB == "1")
            //{
            //    LEDSendText led = new LEDSendText(Server.MapPath("~/EQ2008_Dll_Set.ini"));
            //    led.LED_ShowCompleted();
            //}
        }
        #endregion
        Alert(msg);
    }

    /// <summary>
    /// ASRS状态
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string positonCode = (sender as LinkButton).CommandArgument;
        this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("../Base/FrmBaseASRSList.aspx", SYSOperation.Modify, txtID.Text.Trim()) + "&code=" + txtCTICKETCODE.Text + "&caseType=OUT&positonCode=" + positonCode + "','" + Resources.Lang.FrmOUTBILLEdit_Tips_ASRSMingLing + "','ASRS_LIST');");//命令列表
    }

    /// <summary>
    /// WL 2016056 栈板入库：
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkPALLET_STATUS_I_Click(object sender, EventArgs e)
    {
        //pan gao 把返库和AS/RS出库整合到一起
        //string ids = (sender as Button).CommandArgument;
        //string msg = string.Empty;
        //if (!DBCommon_ASRS.ASRS_InChangeStatus_Out_I(ids, out msg))
        //{
        //    Alert(msg);
        //}
        //btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }


    #endregion

    #region 页面方法

    /// <summary>
    /// 列表数据绑定
    /// </summary>
    public void GridBind()
    {
        using (var modContext = this.context)
        {
            var queryList = from p in modContext.View_OUTBILLEdit
                            orderby p.lineid ascending
                            where 1 == 1
                            select p;

            if (txtID.Text != string.Empty)
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.Equals(txtID.Text.Trim()));

            if (txtCinvcode.Text != string.Empty)
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text));

            if (queryList != null)
            {
                AspNetPager1.RecordCount = queryList.Count();
                AspNetPager1.PageSize = this.PageSize;
            }
            //AspNetPager1.CustomInfoHTML = " 总页数:<b>" + "</b>";
           
            //NOTE by Mark, 09/19
            //grdOUTBILL_D.DataSource = GetPageSize(queryList, PageSize, CurrendIndex).ToList();


            var data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
            flagList.Add(new Tuple<string, string>("otype", "OUTTYPE"));
            flagList.Add(new Tuple<string, string>("worktype", "WorkType"));
            flagList.Add(new Tuple<string, string>("cstatus", "OUTBILL"));


            var srcdata = GetGridSourceDataByList(data, flagList);
            srcdata = GetGridSourceData_PART_RANK(srcdata);
            grdOUTBILL_D.DataSource = srcdata;


            grdOUTBILL_D.DataBind();

            //平库时隐藏相关的
            if (this.WorkType == "0")
            {
                grdOUTBILL_D.Columns[12].Visible = false;
                grdOUTBILL_D.Columns[13].Visible = false;
                grdOUTBILL_D.Columns[14].Visible = false;
                grdOUTBILL_D.Columns[16].Visible = true;
                grdOUTBILL_D.Columns[17].Visible = true;
            }
            else {
                //库存调整出库情况下不显示asrs相关列
                if (OutType == "208") {
                    grdOUTBILL_D.Columns[12].Visible = false;
                    grdOUTBILL_D.Columns[13].Visible = false;
                    grdOUTBILL_D.Columns[14].Visible = false;
                }
                grdOUTBILL_D.Columns[16].Visible = false;
                grdOUTBILL_D.Columns[17].Visible = false;
            }
        }
    }


    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        V_OUTBILLEdit modEntity = context.V_OUTBILLEdit.Where(x => x.id == this.KeyID).FirstOrDefault();
        if (modEntity!=null)
        {
            
            this.txtID.Text = modEntity.id;

            //根据当前单据号ID而确认当前单据是否可以进行编辑操作      
            OUTTYPE OUTBILL_type = GetOUTTYPEByID(this.txtID.Text, "OUTBILL");
            if (OUTBILL_type != null && !string.IsNullOrEmpty(OUTBILL_type.Is_Query.ToString()))
            {
                IsQuery = OUTBILL_type.Is_Query.ToString() == "1" ? true : false;
            }
            else
            {
                IsQuery = false;
            }
            //根据当前单据号ID而确认当前单据是否可以进行编辑操作      
            OUTBILL modOutBill = context.OUTBILL.Where(x => x.id == modEntity.id).FirstOrDefault();
            this.WorkType = modOutBill.worktype;


            this.txtCTICKETCODE.Text = modEntity.cticketcode;
            this.dplCSTATUS.SelectedValue = modEntity.cstatus;
            this.txtCERPCODE.Text = modEntity.cerpcode;
            this.txtCOUTASNID.Text = modEntity.OUTASNNO;
            if (modEntity.dindate.ToString() != string.Empty)
            {
                this.txtDINDATE.Text = DateTime.Parse(modEntity.dindate.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            this.txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(modEntity.ccreateownercode);

            if (modEntity.dcreatetime.ToString() != string.Empty)
            {
                this.txtDCREATETIME.Text = DateTime.Parse(modEntity.dcreatetime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            this.txtCCLIENTCODE.Text = modEntity.cclientcode;
            this.txtCAUDITPERSON.Text = modEntity.cauditperson;

            if (modEntity.daudittime.ToString() != string.Empty)
            {
                this.txtDAUDITTIME.Text = DateTime.Parse(modEntity.daudittime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            txtCDEFINE1.Text = modEntity.cdefine1;
            this.txtCCLIENT.Text = modEntity.cclient;
            this.txtCMEMO.Text = modEntity.cmemo;
            txtSO.Text = modEntity.cso;
            drOType.SelectedValue = modEntity.otype.ToString();
            OutType = modEntity.otype.ToString();

            //抛转时间
            if (modEntity.ddefine3.ToString() != string.Empty)
            {
                this.txtpaozhuantime.Text = DateTime.Parse(modEntity.ddefine3.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            //扣帐时间
            if (modEntity.debittime.ToString() != string.Empty)
            {
                this.txtdebittime.Text = DateTime.Parse(modEntity.debittime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            //扣帐人
            txtdebituser.Text = modEntity.debitowner;

            //20130617112810
            HiddenSpecial.Value = modEntity.special_out.ToString();

            hiddenGuid.Value = modEntity.coutasnid.ToString();
            Status = modEntity.cstatus.ToString();
            IsExistTempOutBill_D = OUTBILL_XDRule.ValidateIsExistTemp_OutBill_D(modEntity.cticketcode.ToString().Trim());

            //33-202
            if (!OutType.Equals("202") && Status.Equals("0") && !IsExistTempOutBill_D)
            {
                SetTblFormControlEnabled(true);
                this.txtCAUDITPERSON.Enabled = false;
                this.txtCCLIENT.Enabled = false;
                this.txtCCLIENTCODE.Enabled = false;
                this.txtCCREATEOWNERCODE.Enabled = false;
                this.txtCERPCODE.Enabled = false;
                this.txtCOUTASNID.Enabled = false;
                this.txtCTICKETCODE.Enabled = false;
                this.txtDAUDITTIME.Enabled = false;
                this.txtDCREATETIME.Enabled = false;
                this.txtDINDATE.Enabled = false;
                this.txtSO.Enabled = false;
                this.drOType.Enabled = false;
            }
            else
            {
                SetTblFormControlEnabled(false);
            }

            //平库补单，状态为未处理，显示扣账按钮
            if (this.WorkType == "0" && Status.Equals("0") && modOutBill.operationtype.Value == 1) {
                this.btnDeliverAndUpdateStockCurrent.Enabled = true;
                this.btnDeliverAndUpdateStockCurrent.Visible = true;
            }
            else if (this.WorkType == "1" && Status.Equals("1") && modOutBill.operationtype.Value == 1)
            {//立库补单，状态已交付，显示扣账按钮
                this.btnDeliverAndUpdateStockCurrent.Enabled = true;
                this.btnDeliverAndUpdateStockCurrent.Visible = true;
            }
            else if (this.WorkType == "1" && Status.Equals("0") && modOutBill.operationtype.Value == 1)
            {//Note by Qamar 2020-11-30 立库补单，状态未處理，显示扣账按钮
                this.btnDeliverAndUpdateStockCurrent.Enabled = true;
                this.btnDeliverAndUpdateStockCurrent.Visible = true;
            }
            else {
                this.btnDeliverAndUpdateStockCurrent.Enabled = false;
                this.btnDeliverAndUpdateStockCurrent.Visible = false;
            }
            btnASRS.Visible = false;

            if (Status.Equals("0") && !IsExistTempOutBill_D)
            {
                btnDelete0.Enabled = true;
                this.btnASRS.Enabled = true;
            }
            else
            {
                btnDelete0.Enabled = false;
                this.btnASRS.Enabled = false;
            }

            btnSearch_Click(this.btnSearch, EventArgs.Empty);

            //20130617112810 只能删除和交付扣帐
            if (HiddenSpecial.Value.Trim().Equals("1"))
            {
                btnSave.Enabled = false;
                btnNew.Enabled = false;
                btnSetCARGOSPACE.Enabled = false;
            }

            if (Status.Equals("2") || Status.Equals("3"))
            {
                btnOutput.Enabled = false;
                btnRefres.Enabled = false;
                btnCancle.Enabled = false;
            }
            //平仓模式时需要隐藏立库用的按钮
            if (this.WorkType == "0") {
                btnASRS.Visible = false;
                btnRefres.Visible = false;
                btnOutput.Visible = false;
                btnCancle.Visible = false;
            }
            //拼板出库的只能查看信息，不可做任何操作
            if(IsQuery)//if (drOType.SelectedValue.Trim() == "1303" || drOType.SelectedValue.Trim() == "1302")  //1303 是拼板出库 1302库存调整出库
            {               
                btnSave.Enabled = false;
                btnCreateOutBill.Enabled = false;
                btnDeliverAndUpdateStockCurrent.Enabled = false;
                btnDelete.Enabled = false;
                btnNew.Enabled = false;
                btnDelete0.Enabled = false;
                btnSetCARGOSPACE.Enabled = false;
                btnOutput.Enabled = false;
                btnCancle.Enabled = false;
                btnDeliverAndUpdateStockCurrent.Enabled = false;
                btnASRS.Enabled = false;
                btnRefres.Enabled = false;
            }

            string isNeedSeral = this.GetConFig("140115");
            if (isNeedSeral == "1")
            {
                using (var modContext = new DBContext())
                {
                    //存在需要管控序列号的物料
                    if (modContext.OUTBILL_D.Any(x => x.id == modEntity.id && modContext.BASE_PART.Any(y => y.cpartnumber == x.cinvcode && y.NeedSerial == 1)))
                    {
                        btnSeral.Visible = true;
                    }
                }
            }
        }
    }

    private void SetTblFormControlEnabled(bool value)
    {
        this.txtCAUDITPERSON.Enabled = value;
        this.txtCCLIENT.Enabled = value;
        this.txtCCLIENTCODE.Enabled = value;
        this.txtCCREATEOWNERCODE.Enabled = value;
        this.txtCERPCODE.Enabled = value;
        this.txtCOUTASNID.Enabled = value;
        this.txtCTICKETCODE.Enabled = value;
        this.txtDAUDITTIME.Enabled = value;
        this.txtDCREATETIME.Enabled = value;
        this.txtDINDATE.Enabled = value;
        this.txtSO.Enabled = value;
        //this.dplCSTATUS.Enabled = value;
        this.drOType.Enabled = value;

        txtCOUTASNID.Attributes.Remove("onclick");
        txtCCLIENTCODE.Attributes.Remove("onclick");
        txtCCLIENT.Attributes.Remove("onclick");
        imgDINDATE.Attributes.Remove("onclick");
        txtDAUDITTIME.Attributes.Remove("onclick");

        btnCreateOutBill.Enabled = value;
        btnDelete.Enabled = value;
        btnNew.Enabled = value;
        btnSave.Enabled = value;
        btnDelete0.Enabled = value;

        //Sales order issue ：33-202
        if (OutType.Equals("202") && Status.Equals("0"))
        {
            if (!IsExistTempOutBill_D)
            {
                btnSetCARGOSPACE.Enabled = true;
            }
        }
        else
        {
            //btnSetCARGOSPACE.Enabled = value;
        }
        if (Status.Length > 0 && Status.Equals("0"))
        {
            value = true;
        }
        btnDeliverAndUpdateStockCurrent.Enabled = value;
        btnASRS.Enabled = value;
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        #region 基础信息检查
        //
        if (this.txtID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_NeedId);//ID项不允许空！
            this.SetFocus(txtID);
            return false;
        }
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
            if (this.txtCERPCODE.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_ERPCodeChangDu);//ERP单号项超过指定的长度30！
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        //
        if (this.txtCOUTASNID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_NeedAsnCode);//出库通知单单号项不允许空！
            this.SetFocus(txtCOUTASNID);
            return false;
        }
        //
        if (this.txtCOUTASNID.Text.Trim().Length > 0)
        {
            if (this.txtCOUTASNID.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_AsnCodeLength);//出库通知单单号项超过指定的长度30！
                this.SetFocus(txtCOUTASNID);
                return false;
            }
        }
        #region 调用存储过程
        List<string> SparaList = new List<string>();
        SparaList.Add("@P_Asn_id:" + this.hiddenGuid.Value.Trim());
        SparaList.Add("@P_ReturnValue:" + "");
        SparaList.Add("@INFOTEXT:" + "");
        string[] result = DBHelp.ExecuteProc("Proc_ValidateOutInBill", SparaList);
        if (result.Length == 1)//调用存储过程有错误
        {
            this.Alert(result.ToString() + "！");
            this.SetFocus(txtCOUTASNID);
            return false;
        }
        else if (result[0] == "1")
        {
            this.Alert(result[1].ToString() + "！");
            this.SetFocus(txtCOUTASNID);
            return false;
        }
        #endregion
        //
        if (this.txtDINDATE.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_NeedChuKuRiQi);//出库日期项不允许空！
            this.SetFocus(txtDINDATE);
            return false;
        }


        if (Convert.ToDateTime(this.txtDINDATE.Text.Trim()) < Convert.ToDateTime(DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd"))
            || Convert.ToDateTime(this.txtDINDATE.Text.Trim()) > Convert.ToDateTime(DateTime.Now.AddDays(10).ToString("yyyy-MM-dd")))
        {
            this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_ChuKuRiQiError);//出库日期必须在今天的前后10天！
            this.SetFocus(txtDINDATE);
            return false;
        }
        //
        if (this.txtCCREATEOWNERCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_NeedZhiDanRen);//制单人项不允许空！
            this.SetFocus(txtCCREATEOWNERCODE);
            return false;
        }
        //
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (this.txtCCREATEOWNERCODE.Text.GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_ZhiDanRenLength);//制单人项超过指定的长度100！
                this.SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }

        if (this.txtDCREATETIME.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_NeedZhiDanRiQi);//制单日期项不允许空！
            this.SetFocus(txtDCREATETIME);
            return false;
        }

        if (this.txtDCREATETIME.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate(this.txtDCREATETIME.Text) == false)
            {
                this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_ZhiDanRiQiError);//制单日期项不是有效的日期！
                this.SetFocus(txtDCREATETIME);
                return false;
            }
        }

        if (this.txtCAUDITPERSON.Text.Trim().Length > 0)
        {
            if (this.txtCAUDITPERSON.Text.GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_AuditorLength);//审核人项超过指定的长度100！
                this.SetFocus(txtCAUDITPERSON);
                return false;
            }
        }

        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 200)
            {
                this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_BeiZhuLength);//备注项超过指定的长度200！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }

        if (drOType.SelectedValue.Length == 0)
        {
            this.Alert(Resources.Lang.FrmOUTBILLEdit_Tips_NeedChuKuLeiXing);//请选择出库类型！
            this.SetFocus(txtCMEMO);
            return false;
        }
        #endregion

        //工单修改机制的检查
        if (drOType.SelectedValue.Equals("203"))//WIP Issue :工单发料203- 35
        {
            string erp = string.Empty;
            erp = OUTBILL_XDRule.CheckWipIssueStatusByErpCode(txtCERPCODE.Text.Trim()).ToUpper();
            if (!erp.Equals("ok".ToUpper()))
            {
                this.Alert(erp);
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }

        //增加出库单状态校验
        if (!CheckStatue("1", "0"))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public OUTBILL SendData(IGenericRepository<OUTBILL> conn)
    {
        OUTBILL entity = (from p in conn.Get()
                         where p.id == this.txtID.Text.ToString()
                         select p).FirstOrDefault<OUTBILL>();

        if (entity == null)
        {
            entity = new OUTBILL();
        }
        //OUTBILL entity = new OUTBILL();
        ////
        //this.txtID.Text = this.txtID.Text.Trim();
        //if (this.txtID.Text.Length > 0)
        //{
        //    entity.id = txtID.Text.ToString();
        //}
        //else
        //{
        //    entity.id = string.Empty;
        //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        //    //entity.ID = null;
        //}
        //
        this.txtCTICKETCODE.Text = this.txtCTICKETCODE.Text.Trim();
        if (this.txtCTICKETCODE.Text.Length > 0)
        {
            entity.cticketcode = txtCTICKETCODE.Text;
        }
        else
        {
            entity.cticketcode = new Fun_CreateNo_Wms().CreateNo("OUTBILL");
            //entity.SetDBNull("CTICKETCODE",true);
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CTICKETCODE = null;
        }
        //
        this.dplCSTATUS.SelectedValue = this.dplCSTATUS.SelectedValue.Trim();
        if (this.dplCSTATUS.SelectedValue.Length > 0)
        {
            entity.cstatus = dplCSTATUS.SelectedValue;
        }
        else
        {
            entity.cstatus = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSTATUS = null;
        }

        //
        IGenericRepository<OUTASN> con = new GenericRepository<OUTASN>(context);
        OUTASN outASNEntity = new OUTASN();
        if (this.txtCOUTASNID.Text.Length > 0)
        {
            entity.coutasnid = this.hiddenGuid.Value.Trim();//txtCOUTASNID.Text;
            var caseList = from p in con.Get()
                           where p.id == entity.coutasnid
                           select p;
            outASNEntity = caseList.ToList().FirstOrDefault();
        }
        else
        {
            entity.coutasnid = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.COUTASNID = null;
        }
        //
        this.txtCERPCODE.Text = this.txtCERPCODE.Text.Trim();
        if (this.txtCERPCODE.Text.Length > 0)
        {
            entity.cerpcode = txtCERPCODE.Text;
        }
        else
        {
            if (outASNEntity != null)
            {
                entity.cerpcode = outASNEntity.cerpcode;
            }
            else
            {
                entity.cerpcode = string.Empty;
            }
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CERPCODE = null;
        }
        //
        this.txtDINDATE.Text = this.txtDINDATE.Text.Trim();
        if (this.txtDINDATE.Text.Length > 0)
        {
            if (this.txtDINDATE.Text.Length == 19)
            {
                entity.dindate = Convert.ToDateTime(txtDINDATE.Text.Trim());
            }
            else
            {
                entity.dindate = Convert.ToDateTime(txtDINDATE.Text.Trim() + " " + Help.DateTime_ToChar(DateTime.Now.Hour) + ":" + Help.DateTime_ToChar(DateTime.Now.Minute) + ":" + Help.DateTime_ToChar(DateTime.Now.Second));
            }
        }
        else
        {
            //entity.SetDBNull("DINDATE", true);
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.DINDATE = null;
        }
        //
        this.txtCCREATEOWNERCODE.Text = this.txtCCREATEOWNERCODE.Text.Trim();
        if (this.txtCCREATEOWNERCODE.Text.Length > 0)
        {
            entity.ccreateownercode = OPERATOR.GetUserIDByUserName(txtCCREATEOWNERCODE.Text);
        }
        else
        {
            entity.ccreateownercode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCREATEOWNERCODE = null;
        }
        //
        this.txtDCREATETIME.Text = this.txtDCREATETIME.Text.Trim();
        if (this.txtDCREATETIME.Text.Length > 0)
        {
            entity.dcreatetime = txtDCREATETIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.DCREATETIME = null;
        }
        //
        this.txtCCLIENTCODE.Text = this.txtCCLIENTCODE.Text.Trim();
        if (this.txtCCLIENTCODE.Text.Length > 0)
        {
            entity.cclientcode = txtCCLIENTCODE.Text;
        }
        else
        {
            entity.cclientcode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCLIENTCODE = null;
        }
        //
        this.txtCAUDITPERSON.Text = this.txtCAUDITPERSON.Text.Trim();
        if (this.txtCAUDITPERSON.Text.Length > 0)
        {
            entity.cauditperson = txtCAUDITPERSON.Text;
        }
        else
        {
            entity.cauditperson = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CAUDITPERSON = null;
        }
        //
        this.txtDAUDITTIME.Text = this.txtDAUDITTIME.Text.Trim();
        if (this.txtDAUDITTIME.Text.Length > 0)
        {
            entity.daudittime = txtDAUDITTIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.DAUDITTIME = null;
        }
        //
        this.txtCCLIENT.Text = this.txtCCLIENT.Text.Trim();
        if (this.txtCCLIENT.Text.Length > 0)
        {
            entity.cclient = txtCCLIENT.Text;
        }
        else
        {
            entity.cclient = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCLIENT = null;
        }
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        else
        {
            if (outASNEntity != null)
            {
                entity.cmemo = outASNEntity.cmemo;
            }
            else
            {
                entity.cmemo = string.Empty;
            }
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CMEMO = null;
        }

        if (txtSO.Text.Length > 0)
        {
            entity.cso = txtSO.Text;
        }
        else
        {
            if (outASNEntity != null)
            {
                entity.cso = outASNEntity.cso;
            }
            else
            {
                entity.cso = string.Empty;
            }
        }
        //投料点
        if (txtCDEFINE1.Text.Length > 0)
        {
            entity.cdefine1 = txtCDEFINE1.Text;
        }
        else
        {
            if (outASNEntity != null)
            {
                entity.cdefine1 = outASNEntity.cdefine1;
            }
            else
            {
                entity.cdefine1 = string.Empty;
            }
        }
        if (drOType.SelectedValue != string.Empty)
            entity.otype = Convert.ToDecimal(drOType.SelectedValue);
        return entity;

    }

    private void SaveToDB(object sender)
    {
        IGenericRepository<OUTBILL> con = new GenericRepository<OUTBILL>(context);
        if (this.CheckData())
        {
            OUTBILL entity = (OUTBILL)this.SendData(con);
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";
            string msg = string.Empty;
            try
            {
                if (this.Operation() == SYSOperation.Modify || this.Operation() == SYSOperation.Preserved1)
                {
                    strKeyID = txtID.Text.Trim();
                    entity.id = strKeyID;
                    con.Update(entity);
                    con.Save();
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    strKeyID = Guid.NewGuid().ToString();
                    entity.id = strKeyID;
                    //con.Insert(entity);
                    //con.Save();

                    //Roger 2013-5-2 17:48:49 合并后通知单不允许补单 临时注销
                    if (!OUTBILL_XDRule.IsMerge(hiddenGuid.Value.Trim()))
                    {
                        throw new Exception(Resources.Lang.FrmOUTBILLEdit_Tips_TongZhiDan + this.txtCTICKETCODE.Text.Trim() + Resources.Lang.FrmOUTBILLEdit_Tips_IsHeBing);//为合并后通知单，不允许补单
                    }

                    #region 调用存储过程
                    List<string> SparaList = new List<string>();
                    SparaList.Add("@P_OutAsn_id:" + hiddenGuid.Value.Trim());
                    SparaList.Add("@P_OutBill_Id:" + strKeyID);
                    SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                    SparaList.Add("@P_IsTemporary:" + "0");
                    SparaList.Add("@P_ReturnValue:" + "");
                    SparaList.Add("@INFOTEXT:" + "");
                    string[] result = DBHelp.ExecuteProc("Proc_CreateOutBill", SparaList);
                    if (result.Length == 1)//调用存储过程有错误
                    {
                        msg = result.ToString();
                        this.AlertAndBack("FrmOUTBILLEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), msg);
                        return;
                    }
                    else if (result[0] == "0")
                    {
                        this.AlertAndBack("FrmOUTBILLEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Msg_SaveSuccess);//保存成功
                        return;
                    }
                    else {
                        this.Alert(Resources.Lang.WMS_Common_Msg_SaveFailed + result[1].ToString());//保存失败！
                        return;
                    }
                    #endregion
                }
                if ((sender as Button).ID == "btnNew")
                {
                    Response.Redirect(BuildRequestPageURL("FrmOUTBILLEdit.aspx", SYSOperation.Preserved1, strKeyID));
                }
                else
                {
                    this.AlertAndBack("FrmOUTBILLEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Msg_SaveSuccess + msg);//保存成功
                }
            }
            catch (Exception E)
            {
                this.Alert(Resources.Lang.WMS_Common_Msg_SaveFailed + E.Message);//保存失败！
            }
        }
    }

    /// <summary>
    /// 增加出库单状态校验
    /// </summary>
    /// <param name="pStatueType"></param>
    /// <param name="pjfType"></param>
    /// <returns></returns>
    public bool CheckStatue(string pStatueType, string pjfType)
    {
        //增加校验此出库单是否已经扣帐 Roger 2013-5-2 14:31:49
        var dtRowCount = new OUTBILL_XDRule().IsDebit(txtCTICKETCODE.Text.Trim());
        if (dtRowCount == null || dtRowCount.Rows.Count == 0)
        {
        }
        else
        {
            if (pStatueType == "1")
            {
                Alert(Resources.Lang.FrmOUTBILLList_Menu_PageName + txtCTICKETCODE.Text.Trim() + Resources.Lang.FrmOUTBILLEdit_Tips_YiKouZhang);//已经扣帐
                return false;
            }
        }
        //存在交付明细不允许删除
        if (OUTBILL_XDRule.ValidateIsExistTemp_OutBill_D(txtCTICKETCODE.Text.Trim()))
        {
            if (pjfType == "1")
            {

                Alert(Resources.Lang.FrmOUTBILLList_Menu_PageName + txtCTICKETCODE.Text.Trim() + Resources.Lang.FrmOUTBILLEdit_Tips_HasJiaoFu);//存在交付明细
                return false;
            }
        }

        return true;
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdOUTBILL_D.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdOUTBILL_D.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    /// <summary>
    /// pan gao
    /// 把返库和AS/RS出库整合到一起
    /// 这里把返库的逻辑复制到这个方法中
    /// </summary>
    private void PalletIn(string ids)
    {
        IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
        OUTBILL_D entity = new OUTBILL_D();
        var caseList = from p in con.Get()
                       where p.ids == ids
                       select p;
        entity = caseList.ToList().FirstOrDefault();
        string msg = string.Empty;
        PROC_ASRS_InChangeStatus_Out_I proc = new PROC_ASRS_InChangeStatus_Out_I();
        proc.Ids = ids;
        proc.Lineid = entity.lineid.ToString();
        proc.UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
        proc.Execute();
        if (proc.ReturnValue == 1)
        {
            Alert(proc.ErrorMessage);
        }
    }

    #endregion


    /// 刷新全部按钮
    /// <summary>
    /// 刷新全部按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSeral_Click(object sender, EventArgs e)
    {
        string code = txtCTICKETCODE.Text;
        this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("../BAR/FrmSeralEdit.aspx", SYSOperation.Modify, "" + "&TYPE=OUT&CODE=" + code) + "','序列号管理','SERALCONTROL',800,700);");
    }


    // NOTE by Mark, 10/21
    // 針對客戶的撿料需求, 撿完料之後須要由原出庫站口(2號站)把剩餘的料直接返庫, 
    // 目前的設計是客戶必須把撿完料的棧板移到入庫專用站口(1號站)才能返庫, 
    // 客戶認為這個移動棧板的動作很不方便
    protected void btn0922_Click(object sender, EventArgs e)
    {
        var num = txtCTICKETCODE.Text.Trim();
        var p = GetPalletByOutbill(num);
        string msg="";
        // NOTE by Mark, 10/09, 看是不是要強制 由 101 返庫--- 【101】目前的編號是 1
        //string msg = String.Format("要知道是那個栈板号 ? {0}, 目前出庫單號是  {1}", p,num);

        //Alert("要知道是那個栈板号?...調試中....讓用戶觸發[返庫],TODO: 1. 運行存儲過程 2.扣账之后再出现返库的按钮  3.有需要返库的才出现 4. UI 是可以調用彈性欄位5, when =1, 整棧板出庫, 5.不要讓用戶有機會, 已返庫, 仍能再點擊[返庫] 6.注意併发（同时点）的可能性，一点按钮即灰避免发生");
        //Alert(msg);


        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Proc_OutBillAutoBack", con);//[dbo].[Proc_OutBillAutoBack]
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@P_CraneID", 1));//立库的线别

                //部分出庫後返庫
                //{ "lineId":"1","taskNo":"07569","palletNo":"20201009155416887","bussinessType":"1","locationFrom":"101","locationTo":"0200102","deliveryTime":"2020-10-09 16:23:35"}
                //cmd.Parameters.Add(new SqlParameter("@P_SiteID", 2));//立库的站点,


                //cmd.Parameters.Add(new SqlParameter("@P_SiteID", 1));//立库的站点, NOTE by Mark 10/09, 應該是 目前預定的 入口
                //目前配置是一進一出, 只好先用2進
                //等調為 可進可出, 這裡要改為出庫站點

                // NOTE by Mark, 10/21, 
                // 台惟案只有一線, 兩個入出口, 
                // 項目設計為 
                // 1, 101 進
                // 2, 102 出
                // 返庫是局部出庫後的操作, 因此先改為 2
                // 這裡的 1, 2 應該是 【基礎資料->WCS管理】 上的 【線別編號】
                cmd.Parameters.Add(new SqlParameter("@P_SiteID", 2));



                cmd.Parameters.Add(new SqlParameter("@P_Palletcode", p));//栈板号
                cmd.Parameters.Add(new SqlParameter("@P_CType", 3));//高度 1 低 2中 3. 高, 目前儲位以 1 為主

                //返回值                            @P_return_Value
                var returnValue = new SqlParameter("@P_return_Value", "");
                returnValue.Direction = ParameterDirection.Output;
                returnValue.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(returnValue);

                //Note by Qamar 2020-12-01
                //@P_Assign是返庫要回到的儲位, 若不指定該儲位就不給@P_Assign值即可
                //台惟案要回原儲位
                IGenericRepository<OUTBILL> con3 = new GenericRepository<OUTBILL>(context);
                var entity3 = (from p3 in con3.Get() where p3.cticketcode == num select p3).ToList();
                IGenericRepository<OUTBILL_D> con4 = new GenericRepository<OUTBILL_D>(context);
                string entity3id = entity3[0].id;
                var entity4 = (from p4 in con4.Get() where p4.id == entity3id select p4).ToList();
                cmd.Parameters.Add(new SqlParameter("@P_Assign", entity4[0].cpositioncode));


                var errText = new SqlParameter("@P_ErrText", "");
                errText.Direction = ParameterDirection.Output;
                errText.SqlDbType = SqlDbType.VarChar;
                errText.Size = 1000;

                cmd.Parameters.Add(errText);
                //parameter.SqlDbType = SqlDbType.VarChar;
                //parameter.Direction = ParameterDirection.Output;
                //parameter.Size = 88;


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (returnValue.Value.ToString() == "1")
                {
                    //msg = new ErrorMsg().GetErrorMsg(guid);//" 操作失败！";
                    //this.btnDeliverAndUpdateStockCurrent.Enabled = true;
                    //btnDeliverAndUpdateStockCurrent.Style.Remove("disabled");
                    Alert(errText.Value.ToString()); //箱号不存在备料储位中！存在未处理完成的出库单！
                }
                else
                {
                    //msg = Resources.Lang.FrmOUTBILLEdit_CaoZuoSuccess;// " 操作成功！";
                    //PageLoadFunction();
                    Alert("返庫成功");

                    /*
                     * Note by Qamar 2020-11-20
                     * 初始cclient=null
                     * 部分出庫成功cclient="~"
                     * 部分出庫返庫成功cclient=""
                    */
                    try
                    {
                        IGenericRepository<OUTBILL> con2 = new GenericRepository<OUTBILL>(context);
                        var entity = (from p2 in con2.Get() where p2.cticketcode == num select p2).ToList();
                        /*
                        if (entity[0].cclient.Substring(0, 1) == "~")
                            entity[0].cclient = entity[0].cclient.Substring(1, entity[0].cclient.Length - 1);
                        */
                        entity[0].cclient = "";
                        con2.Update(entity[0]);
                        con2.Save();
                    }
                    catch { }
                }
            }
        }
        catch (Exception ex)
        {
            msg = Resources.Lang.FrmOUTBILLEdit_Tips_KouZhangShiBai + ":" + ex.Message;//扣账失败:
            Alert("返庫異常. " + ex.Message);
        }





        //  this.btn0922.Enabled = false;
    }
}

