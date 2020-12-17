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
using System.Text;
using System.Text.RegularExpressions;


/// <summary>
/// 出库通知单明细页
/// </summary>
public partial class OUT_FrmOUTASN_DEdit : WMSBasePage
{

    #region 页面属性

    public string ErpCode
    {
        set { ViewState["ErpCode"] = value; }
        get
        {
            if (ViewState["ErpCode"] != null)
            {
                return ViewState["ErpCode"].ToString();
            }
            return "";
        }
    }

    public string OutType
    {
        set { ViewState["OutType"] = value; }
        get
        {
            if (ViewState["OutType"] != null)
            {
                return ViewState["OutType"].ToString();
            }
            return "";
        }
    }

    public string CINVNAME
    {
        set { ViewState["CINVNAME"] = value; }
        get
        {
            if (ViewState["CINVNAME"] != null)
            {
                return ViewState["CINVNAME"].ToString();
            }
            return "";
        }
    }
    //是否是特殊元件领料 WIP Issue
    public decimal IsSpecialWIP_Issue
    {
        set { ViewState["IsSpecialWIP_Issue"] = value; }
        get
        {
            if (ViewState["IsSpecialWIP_Issue"] != null)
            {
                return Convert.ToDecimal(ViewState["IsSpecialWIP_Issue"]);
            }
            return 0;
        }
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
        this.ShowPARTDivRTV1.SetCINVCODE = this.txtCINVCODE.ClientID;
        this.ShowPARTDivRTV1.SetCINVNAME = this.txtCINVNAME.ClientID;
        this.ShowPARTDivRTV1.SetSO_LINE = this.txtISOLINE.ClientID;
        this.ShowPARTDivRTV1.SetSo_Num = this.txtCSO.ClientID;
        this.ShowPARTDiv1.SetCINVCODE = this.txtCINVCODE.ClientID;
        this.ShowPARTDiv1.SetCINVNAME = this.txtCINVNAME.ClientID;
        this.ShowPARTDiv1.SetCspec = this.txtcspecifications.ClientID;
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData();
                hfWorkType.Value = "1";
            }
            else
            {
                txtLineId.Text = (GetMaxLineId(txtID.Text) + 1).ToString();
            }
            LoadIDS();
            //20130515140920
            if (this.Operation() == SYSOperation.New)
            {
                DpdStatus.SelectedValue = "0";//新增状态为未处理
                txtCERPCODELINE.Text = OutAsnQuery.Fun_GetNo(txtID.Text, "3", "", "");
            }
        }
        RegisterClientScript();
        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
    }

    /// <summary>
    /// 初始化页面。主要做一下动作
    ///1、DropDownList,ListBox数据装载,
    ///2、新增按钮、删除的按钮的Java脚本注册等
    ///一般在PageLoad 事件调用，
    ///并且IsPostBack = false时
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTASN_D');return false;";
        txtID.Text = Request.QueryString["IDS"];
        //IGenericRepository<SYS_PARAMETER> con = new GenericRepository<SYS_PARAMETER>(context);
        //var caseList = from p in con.Get()
        //               orderby p.flag_id ascending
        //               where p.flag_type == "OASN_D_STATUS"
        //               select p;
        //Help.DropDownListDataBind(caseList.ToList(), this.DpdStatus, "", "FLAG_NAME", "FLAG_ID", "");
        //
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "OASN_D_STATUS", false, -1, -1), this.DpdStatus, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");

        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.Operation() == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.FrmOUTASN_DEdit_Button_ShenPi;
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
        #region //Note by Qamar 2020-10-20
        string rankfinal = txtRANK_FINAL.Text.Trim().ToUpper();
        if (rankfinal == "")
            rankfinal = "_";
        #endregion
        if (rankfinal.Length == 1)
        {
            txtCINVCODE.Text += "-" + rankfinal;
            IGenericRepository<OUTASN_D> con = new GenericRepository<OUTASN_D>(context);
            btnSave.Enabled = false;//20130513154835
            if (this.CheckData())
            {
                OUTASN_D entity = this.SendData();
                txtCINVNAME.Text = entity.cinvname;
                string strKeyID = "";

                try
                {
                    if (this.Operation() == SYSOperation.Modify)
                    {
                        entity.ids = txtIDS.Value.Trim();
                        strKeyID = entity.ids;
                        con.Update(entity);
                        con.Save();
                        //this.AlertAndBack("FrmOUTASN_DEdit.aspx?" + BuildQueryString(SysOperation.Modify, strKeyID),"保存成功"); 
                        this.WriteScript("window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTASN_D');alert('" + Resources.Lang.WMS_Common_Msg_SaveSuccess + "');");
                    }
                    else if (this.Operation() == SYSOperation.New)
                    {
                        entity.lineid = GetMaxLineId(txtID.Text) + 1;
                        entity.ids = Guid.NewGuid().ToString();
                        strKeyID = entity.ids;
                        con.Insert(entity);
                        con.Save();
                        //this.AlertAndBack("FrmOUTASN_DEdit.aspx?" + BuildQueryString(SysOperation.Modify, strKeyID),"保存成功"); 
                        this.AlertAndBack("FrmOUTASN_DEdit.aspx?IDS=" + this.txtID.Text.Trim() + "&" + BuildQueryString(SYSOperation.New, strKeyID), Resources.Lang.WMS_Common_Msg_SaveSuccess);
                    }

                }
                catch (Exception E)
                {
                    btnSave.Enabled = true;//20130513154835
                    DBLog.Log(E.Message.ToString());
                    this.Alert(this.GetOperationName() + Resources.Lang.WMS_Common_Msg_SaveFailed + E.Message);
                }
            }
            else
            {
                #region //Note by Qamar 2020-10-20
                txtCINVCODE.Text = txtCINVCODE.Text.Substring(0, txtCINVCODE.Text.Length - 2);
                #endregion
            }
        }
        else
        {
            #region //NOte by Qamar 2020-10-20
            this.Alert("批/序號(RANK)有誤");
            #endregion
        }
        btnSave.Enabled = true;//20130513154835
    }

    #endregion

    #region 页面方法
    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        ASCIIEncoding strData = new ASCIIEncoding();
        string msg = string.Empty;
        try
        {
            #region 基础信息检查
            if (CINVNAME == null || CINVNAME.Length == 0)
            {
                CINVNAME = OutAsnQuery.GetCPARTNAMEByCPARTNUMBER(txtCINVCODE.Text.Trim());
            }
            this.txtCINVNAME.Text = CINVNAME;

            //
            if (this.txtCINVCODE.Text.Trim() == "")
            {
                this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_NeedLiaoHao);
                this.SetFocus(txtCINVCODE);
                return false;
            }
            //
            if (this.txtCINVCODE.Text.Trim().Length > 0)
            {
                if (strData.GetBytes(txtCINVCODE.Text).Length > 50)
                {
                    this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_LiaoHaoChangDu);
                    this.SetFocus(txtCINVCODE);
                    return false;
                }
            }
            //CQ 2014-12-3 16:31:55 新增检查税别是否为空
            //stirng str_CINVCODE = txtCINVCODE.Text.Trim().ToUpper();
            if (!(OutAsnQuery.Fun_CheckTax_SameBond(txtCINVCODE.Text.Trim().ToUpper(), out msg)))
            {
                this.Alert(msg);
                this.SetFocus(txtCINVCODE);
                return false;
            }

            if (this.txtIQUANTITY.Text.Trim() == "")
            {
                this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_NeedShuLiang);
                this.SetFocus(txtIQUANTITY);
                return false;
            }
            //
            if (this.txtIQUANTITY.Text.Trim().Length > 0)
            {
                if (!IsDecimal(this.txtIQUANTITY.Text))
                {
                    this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_ShuLiangError);
                    this.SetFocus(txtIQUANTITY);
                    return false;
                }
                //if (!IsDecimalwithTwoDigit(this.txtIQUANTITY.Text.Trim())) {
                //    this.Alert("数量项不是两位以内小数！");
                //    this.SetFocus(txtIQUANTITY);
                //    return false;
                //}
            }
            if (Convert.ToDecimal(this.txtIQUANTITY.Text.Trim()) <= 0)
            {
                this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_ShuLiangDaYuLing);
                this.SetFocus(txtIQUANTITY);
                return false;
            }

            string workType = "0";
            if (this.Operation() == SYSOperation.Modify)
            {
                workType = "1";
            }
            //
            if (this.txtCINVBARCODE.Text.Trim().Length > 0)
            {
                if (strData.GetBytes(txtCINVBARCODE.Text).Length > 60)
                {
                    this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_LiaoHaoChangDu);
                    this.SetFocus(txtCINVBARCODE);
                    return false;
                }
            }

            if (this.txtCBATCH.Text.Trim().Length > 0)
            {
                if (strData.GetBytes(txtCBATCH.Text).Length > 20)
                {
                    this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_PiCiXiangChangDu);
                    this.SetFocus(txtCBATCH);
                    return false;
                }
            }

            if (this.txtCSO.Text.Trim().Length > 0)
            {
                if (Regex.IsMatch(this.txtCSO.Text.Trim(), @"[\u4e00-\u9fa5]"))
                {
                    this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_BuNengHanZhongWen);
                    this.SetFocus(txtCSO);
                    return false;
                }
            }

            if (this.txtCMEMO.Text.Trim().Length > 0)
            {
                if (strData.GetBytes(txtCMEMO.Text).Length > 40)
                {
                    this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_BeiZhuChangDu);
                    this.SetFocus(txtCMEMO);
                    return false;
                }
            }
            if (!OutAsnQuery.ValidateAsn_D_IsExist(txtID.Text.Trim(), txtCINVCODE.Text.Trim(), txtCERPCODELINE.Text.Trim(), txtIDS.Value.Trim()))
            {
                this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_ZhiNengYiTiao);
                this.SetFocus(txtCERPCODELINE);
                return false;
            }

            #endregion

            #region WIP Issue : 35-203 工单发料检查,  WIP Negative Return 48 工单负退料, GNT維修工單領料 : 460
            //WIP Issue : 35-203 工单发料检查,  WIP Negative Return 48 工单负退料, GNT維修工單領料 : 460 
            //1 .發齊了不可用WIP ISSUE
            //2 .退料超領未退完不可退WIP 
            if (OutType.Equals("203") || OutType.Equals("48") || OutType.Equals("460"))
            {
                //vConfig 判断是否连接ERP 1连接 0 不连接
                string vConfig = OutAsnQuery.GetConFig("000004");
                if (vConfig == "1")
                {
                    #region l连接检查
                    //料号检查
                    msg = OutAsnQuery.CheckWIP_Part(txtID.Text, txtCINVCODE.Text);
                    if (!msg.Equals("OK"))
                    {
                        this.Alert(msg);
                        this.SetFocus(txtCINVCODE);
                        return false;
                    }
                    if (IsSpecialWIP_Issue == 0)
                    {
                        //正常
                        decimal IsSpecial = IsSpecialWIP_Issue;
                        if (OutType.Equals("48"))
                        {
                            IsSpecial = 1;
                        }
                        //正常发料数量控制
                        msg = OutAsnQuery.CheckWipIssueQty(txtID.Text.Trim(), txtCINVCODE.Text.Trim(), txtIQUANTITY.Text.Trim(), workType, hfOriginalQty.Value);
                        if (!msg.Equals("OK"))
                        {
                            this.Alert(msg);
                            this.SetFocus(txtIQUANTITY);
                            return false;
                        }
                    }

                    #endregion
                }
                else
                {
                    #region 0 不连接
                    #endregion
                }

                if (txtCERPCODELINE.Text.Trim() == "")
                {
                    Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_NeedErpCodeLine);
                    this.SetFocus(txtCERPCODELINE);
                    return false;
                }
            }
            #endregion

            #region 完工退料17-204
            //完工退料17-204
            if (OutType.Equals("204"))//验证Return to Vendor (36)的数量是否有效
            {
                msg = OutAsnQuery.CheckCheckWip_C_R_Qty(txtID.Text.Trim(), txtCINVCODE.Text.Trim(), txtIQUANTITY.Text.Trim(), workType, hfOriginalQty.Value);
                if (!msg.Equals("OK"))
                {
                    this.Alert(msg);
                    this.SetFocus(txtIQUANTITY);
                    return false;
                }
            }
            #endregion

            #region Return to Vendor (36)-201
            if (OutType.Equals("201"))//验证Return to Vendor (36)-201的数量是否有效
            {
                if (txtCERPCODELINE.Text.Length == 0)
                {
                    this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_NeedErpCode);
                    this.SetFocus(txtCERPCODELINE);
                    return false;
                }
                //料号检查
                msg = OutAsnQuery.CheckWIP_Part(txtID.Text, txtCINVCODE.Text);
                if (!msg.Equals("OK"))
                {
                    this.Alert(msg);
                    this.SetFocus(txtCINVCODE);
                    return false;
                }
                msg = OutAsnQuery.CheckReturnToVendorQty(txtID.Text.Trim(), txtCINVCODE.Text.Trim(), txtIQUANTITY.Text.Trim(), workType, hfOriginalQty.Value, txtCERPCODELINE.Text);
                if (!msg.Equals("OK"))
                {
                    this.Alert(msg);
                    this.SetFocus(txtIQUANTITY);
                    return false;
                }
            }
            #endregion

            #region 工单超领 205
            //超发检查
            if (OutType.Equals("205"))
            {
                //料号不允许超领
                if (!OutAsnQuery.CheckWIP_CL_PARTINFO(txtCINVCODE.Text, "CL"))
                {
                    this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_CannotChaoLing);
                    this.SetFocus(txtCINVCODE);
                    return false;
                }
                //料号检查是否在工单内
                msg = OutAsnQuery.CheckWIP_Part(txtID.Text, txtCINVCODE.Text);
                if (!msg.Equals("OK"))
                {
                    this.Alert(msg);
                    this.SetFocus(txtCINVCODE);
                    return false;
                }
                //料号发齐才允许超领
                msg = OutAsnQuery.CheckWIP_CF_ByPartNumber(txtID.Text, txtCINVCODE.Text);
                if (!msg.Equals("OK"))
                {
                    this.Alert(msg);
                    this.SetFocus(txtCINVCODE);
                    return false;
                }
            }

            //超领检查完工入库后不允许，SMT检查 CQ 2013-9-17 14:55:15
            msg = OutAsnQuery.CheckOverCollar(txtID.Text.Trim(), txtCINVCODE.Text.Trim().ToUpper(), 0, WmsWebUserInfo.GetCurrentUser().UserNo, "2");
            if (!msg.Equals("0"))
            {
                this.Alert(msg);
                this.SetFocus(txtCINVCODE);
                return false;
            }
            #endregion

            //只有未生成指引或未生出库单的才能修改!
            if (OutAsnQuery.CheckIsExistOutAssitByOutAsnId(txtID.Text.Trim()) || OutAsnQuery.ValidateIsCreateOutBill(txtID.Text.Trim()))
            {
                this.Alert(Resources.Lang.FrmOUTASN_DEdit_Tips_MingXiZengJia);
                return false;
            }

            //20130731134649
            var result = OutAsnQuery.CanModDebit(txtCERPCODELINE.Text.Trim(), txtCINVCODE.Text.Trim(), "0", "", OutType.Trim(), "");
            if (!result.Equals("1"))
            {
                Alert(result);
                return false;
            }
        }
        catch (Exception er)
        {
            Alert(er.Message);
            return false;
        }
        return true;
    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public OUTASN_D SendData()
    {
        //根据出库通知单ID和料号获取入库通知单中和ERP单号对应的字段信息
        //例 物料条码 批次号 备注 子表ERP单号项次 po号 PO项次 等
        //IGenericRepository<OUTASN_D> con = new GenericRepository<OUTASN_D>(context);
        //var caseList = from p in con.Get()
        //               where p.ids == txtID.Text.Trim()
        //               select p;
        OUTASN_D entity = new OUTASN_D();
        entity.ids = Guid.NewGuid().ToString();
        this.txtCINVCODE.Text = this.txtCINVCODE.Text.Trim();
        if (this.txtCINVCODE.Text.Length > 0)
        {
            entity.cinvcode = txtCINVCODE.Text;
            entity.cinvname = OutAsnQuery.GetCINVNAMEByCINVCODE(entity.cinvcode);
        }
        this.txtCINVBARCODE.Text = this.txtCINVBARCODE.Text.Trim();
        if (this.txtCINVBARCODE.Text.Length > 0)
        {
            entity.cinvbarcode = txtCINVBARCODE.Text;
        }
        //
        this.txtIQUANTITY.Text = this.txtIQUANTITY.Text.Trim();
        if (this.txtIQUANTITY.Text.Length > 0)
        {
            entity.iquantity = txtIQUANTITY.Text.ToDecimal();
        }
        //
        this.txtCSO.Text = this.txtCSO.Text.Trim();
        if (this.txtCSO.Text.Length > 0)
        {
            entity.cso = txtCSO.Text;
        }
        //
        this.txtISOLINE.Text = this.txtISOLINE.Text.Trim();
        if (this.txtISOLINE.Text.Length > 0)
        {
            entity.isoline = txtISOLINE.Text.ToDecimal();
        }
        //
        this.txtCERPCODELINE.Text = this.txtCERPCODELINE.Text.Trim();
        if (this.txtCERPCODELINE.Text.Length > 0)
        {
            entity.cerpcodeline = txtCERPCODELINE.Text;
        }
        //
        this.txtCBATCH.Text = this.txtCBATCH.Text.Trim();
        if (this.txtCBATCH.Text.Length > 0)
        {
            entity.cbatch = txtCBATCH.Text;
        }
        //
        this.txtID.Text = this.txtID.Text.Trim();
        if (this.txtID.Text.Length > 0)
        {
            entity.id = txtID.Text;
        }
        //
        //this.txtCSTATUS.Text = this.txtCSTATUS.Text.Trim();
        if (DpdStatus.SelectedValue.Length > 0)
        {
            entity.cstatus = DpdStatus.SelectedValue;
        }
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        this.txtLineId.Text = this.txtLineId.Text.Trim();
        if (!string.IsNullOrEmpty(txtLineId.Text))
        {
            entity.lineid = Convert.ToInt32(this.txtLineId.Text);
        }
        return entity;
    }


    public void LoadIDS()
    {
        IGenericRepository<OUTASN> con = new GenericRepository<OUTASN>(context);
        var caseList = from p in con.Get()
                       where p.id == txtID.Text.Trim()
                       select p;
        if (caseList != null && caseList.Count() > 0)
        {
            OUTASN entity = caseList.ToList().FirstOrDefault();
            OutType = entity.itype.ToString();
            ErpCode = entity.cerpcode;
            IsSpecialWIP_Issue = entity.idefine5.HasValue ? entity.idefine5.Value : 0; ;
            this.ShowPARTDiv1.SetTypeCode.Add("intype".ToLower(), OutType);
            this.ShowPARTDiv1.SetTypeCode.Add("ErpCode".ToLower(), ErpCode);
            this.ShowPARTDiv1.SetTypeCode.Add("IsSpecialWIP_Issue".ToLower(), IsSpecialWIP_Issue.ToString());
            this.ShowPARTDivRTV1.SetTypeCode.Add("intype".ToLower(), OutType);
            this.ShowPARTDivRTV1.SetTypeCode.Add("ErpCode".ToLower(), ErpCode);
            //完工退料 17-204
            if (OutType.Equals("204"))
            {
                this.txtCINVCODE.Enabled = false;
                this.txtCINVNAME.Enabled = false;
            }
        }
    }

    /// <summary>
    /// 向页面注册脚本
    /// </summary>
    private void RegisterClientScript()
    {
        //完工退料17-204
        if (!OutType.Equals("204"))
        {
            //收货退回36-201
            if (OutType.Equals("201"))
            {
                ltSearch.Text = @"<img alt='' onclick=""disponse_div(event,document.all('ctl00_ContentPlaceHolderMain_ShowPARTDivRTV1_ajaxWebSearChComp'));""
                                    src=""../../Images/Search.gif"" class=""select"" />";
            }
            else
            {
                ltSearch.Text = @"<img alt="""" onclick=""disponse_div(event,document.all('ctl00_ContentPlaceHolderMain_ShowPARTDiv1_ajaxWebSearChComp'));""
                                    src=""../../Images/Search.gif"" class=""select"" />";

//                string script = @"<script type=""text/javascript"">
//                                $(function () {
//                                    $(""#" + this.txtCINVCODE.ClientID + @""").autocomplete({
//                                        source: function (request, response) {
//                                            //alert(request.term);
//                                            $.ajax({
//                                                url: ""../Server/Part.ashx?partNumber="" + request.term + ""&intype=" + OutType.Trim() + @"&erpcode=" + ErpCode.Trim() + @"&IsSpecialWIP_Issue=" + IsSpecialWIP_Issue + @"&Asn_type=OUTASN_D"",
//                                                dataType: ""xml"",
//                                                error: function (XMLHttpRequest, textStatus, errorThrown) {
//                                                    //alert(XMLHttpRequest.status);
//                                                    //alert(XMLHttpRequest.readyState);
//                                                    //alert(textStatus);
//                                                },
//                                                success: function (data) {
//                                                    response($(""reuslt"", data).map(function () {
//                                                        return {
//                                                            value: $(""CPARTNUMBER"", this).text(),
//                                                            label: $(""CPARTNUMBER"", this).text(),
//                                                            id: $(""CPARTNAME"", this).text()
//                                                        }
//                                                    }));
//                                                }
//                                            });
//                                        },
//                                        autoFocus: true,
//                                        minLength: 0,
//                                        select: function (event, ui) {
//                                            $(""#" + txtCINVNAME.ClientID + @""").val(ui.item.id);
//                                        },
//                                        open: function () {
//                                            $(this).removeClass(""ui-corner-all"").addClass(""ui-corner-top"");
//                                        },
//                                        close: function () {
//                                            $(this).removeClass(""ui-corner-top"").addClass(""ui-corner-all"");
//                                        }
//                                    });
//                                });
//                            </script>";
//                Page.ClientScript.RegisterClientScriptBlock(GetType(), txtCINVCODE.ClientID, script);
            }
        }
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<OUTASN_D> con = new GenericRepository<OUTASN_D>(context);
        var caseList = from p in con.Get()
                       where p.ids == this.KeyID.ToString()
                       select p;
        OUTASN_D entity = caseList.ToList().FirstOrDefault();
        txtLineId.Text = entity.lineid.HasValue ? entity.lineid.Value.ToString() : "";
        this.txtIDS.Value = entity.ids.ToString();
        this.txtID.Text = entity.id.ToString();
        DpdStatus.SelectedValue = entity.cstatus;
        //以下 28-10-2020 by Qamar
        this.txtCINVCODE.Text = entity.cinvcode.Substring(0, entity.cinvcode.Length - 2);
        this.txtRANK_FINAL.Text = entity.cinvcode.Substring(entity.cinvcode.Length - 1, 1);
        if (txtRANK_FINAL.Text.Trim() == "_")
            txtRANK_FINAL.Text = "";
        string partnumber = entity.cinvcode;
        //以上 28-10-2020 by Qamar
        this.txtCINVNAME.Text = entity.cinvname;
        this.txtIQUANTITY.Text = entity.iquantity.ToString("f2");
        hfOriginalQty.Value = entity.iquantity.ToString();
        this.txtCINVBARCODE.Text = entity.cinvbarcode;
        this.txtCBATCH.Text = entity.cbatch;
        this.txtCERPCODELINE.Text = entity.cerpcodeline;
        this.txtCMEMO.Text = entity.cmemo;
        this.txtCSO.Text = entity.cso;
        this.txtISOLINE.Text = entity.isoline.HasValue ? entity.isoline.Value.ToString() : "";
        CINVNAME = entity.cinvname;
        //显示物料规格
        if (txtcspecifications.Text.Length == 0)
        {
            //29-10-2020 by Qamar
            IGenericRepository<BASE_PART> icon = new GenericRepository<BASE_PART>(context);
            var icaseList = from p in icon.Get()
                            where p.cpartnumber == partnumber
                            select p;
            if (icaseList.Count() > 0)
            {
                try
                {
                    txtcspecifications.Text = icaseList.ToList().FirstOrDefault<BASE_PART>().cspecifications;
                }
                catch { }
            }
        }
    }

    /// <summary>
    /// 获取最大的项次编号
    /// </summary>
    /// <param name="asnId"></param>
    /// <returns></returns>
    private int GetMaxLineId(string asnId)
    {
        int lineId = 0;
        var inlineId = context.OUTASN_D.Where(x => x.id == asnId).Max(x => x.lineid);
        lineId = inlineId.HasValue ? inlineId.Value : 0;
        return lineId;
    }
    #endregion

}

