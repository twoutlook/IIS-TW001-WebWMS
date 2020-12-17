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
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Business.Others;

/// <summary>
/// 
/// </summary>
public partial class OUT_FrmOUTBILL_DEdit : WMSBasePage
{
    /// <summary>
    /// 作业方式 0：平库  1：立库
    /// </summary>
    public string WorkType
    {
        get { return this.hiddWorkType.Value; }
        set { this.hiddWorkType.Value = value.ToString(); }
    }

    /// <summary>
    /// 是否暂存区
    /// </summary>
    public string IsTemporary
    {
        get { return this.hiddIsTemporary.Value; }
        set { this.hiddIsTemporary.Value = value.ToString(); }
    }

    /// <summary>
    /// 出库单 erp单号
    /// </summary>
    public string UseErpCode
    {
        get { return this.hiddUseErpCode.Value; }
        set { this.hiddUseErpCode.Value = value.ToString(); }
    }

    /// <summary>
    /// 是否匹配供应商
    /// </summary>
    public string UseVendorCode
    {
        get { return this.hiddUseVendor.Value; }
        set { this.hiddUseVendor.Value = value.ToString(); }
    }

    /// <summary>
    /// 出库单对应料号指定cso
    /// </summary>
    public string CSO
    {
        get { return this.hiddCSO.Value; }
        set { this.hiddCSO.Value = value.ToString(); }
    }

    /// <summary>
    /// 通知单对应供应商编号
    /// </summary>
    public string VendorCode
    {
        get { return this.hiddVendorCode.Value; }
        set { this.hiddVendorCode.Value = value.ToString(); }
    }


    /// <summary>
    /// 出库单状态
    /// </summary>
    public string OutBillStatus
    {
        get { return this.hiddOutbillStatus.Value; }
        set { this.hiddOutbillStatus.Value = value.ToString(); }
    }

    /// <summary>
    /// 出库单据编号，工单发料203有相关配置
    /// </summary>
    public string BillNo {
        get { return this.hiddBillNo.Value; }
        set { this.hiddBillNo.Value = value.ToString(); }
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
    #region SQL

    protected void Page_Load(object sender, EventArgs e)
    {
        ShowPart_Asn_IDS_Div1.SetCINVCODE = txtCINVCODE.ClientID;
        ShowPart_Asn_IDS_Div1.SetCINVNAME = txtCINVNAME.ClientID;
        ShowPart_Asn_IDS_Div1.SetERPLine = txtCERPCODELINE.ClientID;
        ShowPart_Asn_IDS_Div1.SetIQTY = txtIQUANTITY.ClientID;
        ShowPart_Asn_IDS_Div1.SetAsn_D_IDS = txtAsn_D_IDS.ClientID;       

        ShowBASE_CARGOSPACEDiv2.SetCompName = txtCPOSITION.ClientID;
        ShowBASE_CARGOSPACEDiv2.SetORGCode = txtCPOSITIONCODE.ClientID;
        //ShowBASE_CARGOSPACEDiv2.OutCinvCode = txtCINVCODE.Text;
        //Note by Qamar 2020-11-27
        ShowBASE_CARGOSPACEDiv2.OutCinvCode = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
        ShowBASE_CARGOSPACEDiv2.workType = this.WorkType;
        ShowBASE_CARGOSPACEDiv2.DrpIsAll = "2";
        txtCPOSITION.Enabled = false;

        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData();
                TabMain0.Visible = true;
                btnSearch_Click(null, null);
            }
            else
            {
                string ID = Request.QueryString["ID"];
                this.txtID.Text = ID;
                this.txtID.Enabled = false;
                txtDOUTDATE.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtCOUTPERSONCODE.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtCOUTPERSONCODE.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                TabMain0.Visible = false;
                txtLineId.Text = (GetMaxLineId(ID) + 1).ToString();
            }

            LoadIDS();

            Session["Outasn_id"] = hfOutAsn_id.Value;
            if (OutType.Equals("202") || OutType.Equals("203"))
            {
                this.txtCINVCODE.Attributes.Remove("onclick");
                this.txtCINVNAME.Attributes.Remove("onclick");
                this.txtCINVCODE.Enabled = false;
                this.txtRANK_FINAL.Enabled = false;
                this.txtCINVNAME.Enabled = false;
            }

            /*
             * Note by Qamar 2020-11-27
             * 台惟案應該用不到
            //验证是否可以超发
            try
            {
                if (txtCINVCODE.Text.Trim().Length > 4)
                {
                    string CINVCODE = txtCINVCODE.Text.Trim().Substring(0, 4);

                    int head = Convert.ToInt32(CINVCODE);
                    //非工单类型不允许超发
                    if ((OutType != "203") || (OutType != "204") || (OutType != "205") || (txtCINVCODE.Text.Trim().Length > 0 && head < 2111 && head > 2117))
                    {
                        txtLINE_QTY.Enabled = false;
                    }
                }
            }
            catch (Exception)
            {
            }
            */
            this.GridBind();
        }
        //查询参数
        ShowPart_Asn_IDS_Div1.SearchAsnID = hfOutAsn_id.Value;
        ShowPart_Asn_IDS_Div1.SearchType = "OUT";
        ShowPart_Asn_IDS_Div1.SearchBillID = txtID.Text.Trim();

        showSn.SetSearchCinvCode = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
        showSn.SetSearchCposition = this.txtCPOSITIONCODE.Text.Trim();
        showSn.SetConFig = ConFigvalue;
        showSn.SetErpCode = this.hiddCSO.Value;
        showSn.SetIsNeedErp = this.hiddUseErpCode.Value;
        showSn.SetIsNeedVendor = this.hiddUseVendor.Value;
        showSn.SetVendor = this.VendorCode;
        SetSNEnable();

        ShowBASE_CARGOSPACEDiv2.IsTemporary = this.IsTemporary;

        RegisterClientScript();
        RegisterClientScriptAsrs();       

        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        btnSave_SN.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave_SN) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        if (txtcspecifications.Text.Length==0)
        {
            IGenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
            string CINVCODE = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
            var caseList = from p in con.Get()
                           where p.cpartnumber == CINVCODE
                           select p;
            if (caseList.Count() > 0)
            {
                BASE_PART dr = caseList.ToList().FirstOrDefault();
               
                //显示物料规格
                if (!string.IsNullOrEmpty(dr.cspecifications))
                {
                    txtcspecifications.Text = dr.cspecifications.ToString();
                }
            }
        }
        //Note by Qamar 2020-11-11
        ddl_Line_ID.SelectedIndex = 1; //線別
        ddl_Line_ID.SelectedValue = "1";
        Help.DropDownListDataBind(GetOutPallet(ddl_Line_ID.SelectedValue, OutType, this.BillNo), this.ddl_Pallet_Code, Resources.Lang.FrmOUTBILL_DEdit_Tips_ZhanDianSelect, "FUNCNAME", "EXTEND1", "");
        ddl_Pallet_Code.SelectedIndex = 1; //站點
        ddl_Line_ID.Enabled = false;
        ddl_Pallet_Code.Enabled = false;
    }
    /// <summary>
    /// 获取最大的项次编号
    /// </summary>
    /// <param name="asnId"></param>
    /// <returns></returns>
    private int GetMaxLineId(string id)
    {
        int lineId = 0;
        IGenericRepository<OUTBILL_D> conn = new GenericRepository<OUTBILL_D>(db);
        int count = (from p in conn.Get()
                     where p.id == id
                     select p).Count();
        if (count > 0)
        {
            lineId = count;
        }
        return lineId;
    }
    /// <summary>
    /// 向页面注册脚本
    /// </summary>
    private void RegisterClientScript()
    {
        string script = @"<script type=""text/javascript"">
                            $(function () {
                                $(""#" + this.txtCINVCODE.ClientID + @""").autocomplete({
                                    source: function (request, response) {
                                        //alert(request.term);
                                        $.ajax({
                                            url: ""../Server/Part.ashx?partNumber="" + request.term + ""&Asn_id=" + hfOutAsn_id.Value.Trim() + @"&Asn_type=OUTASN_D"",
                                            dataType: ""xml"",
                                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                                //alert(XMLHttpRequest.status);
                                                //alert(XMLHttpRequest.readyState);
                                                //alert(textStatus);
                                            },
                                            success: function (data) {
                                                response($(""reuslt"", data).map(function () {
                                                    return {
                                                        value: $(""CPARTNUMBER"", this).text(),
                                                        label: $(""CPARTNUMBER"", this).text(),
                                                        id: $(""CPARTNAME"", this).text()
                                                    }
                                                }));
                                            }
                                        });
                                    },
                                    autoFocus: true,
                                    minLength: 0,
                                    select: function (event, ui) {
                                        InCinvCode = ui.item.id;
                                        $(""#" + txtCINVNAME.ClientID + @""").val(ui.item.id);
                                    },
                                    open: function () {
                                        $(this).removeClass(""ui-corner-all"").addClass(""ui-corner-top"");
                                    },
                                    close: function () {
                                        $(this).removeClass(""ui-corner-top"").addClass(""ui-corner-all"");
                                    }
                                });
                            });
                        </script>";
        Page.ClientScript.RegisterClientScriptBlock(GetType(), txtCINVCODE.ClientID, script);
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }

    /// <summary>
    /// 2015-12-28 向页面注册ASRS脚本
    /// </summary>
    private void RegisterClientScriptAsrs()
    {
        if (ASRSFig == "1")
        {
            string script = string.Format(@"<script type=""text/javascript"">
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
                                                                        {{ Type: 'Out', Special: '" + HidField_Enable.Value.Trim() + "', DataType: 'PositionCode', Ids: '" + this.txtIDS.Text.Trim() + @"', Qty: 0, Line_Qty: 0, PositionCode: ui.item.value }},
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
                                   txtCPOSITIONCODE.ClientID,
                                   GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text), txtIQUANTITY.Text.Trim());
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), txtCPOSITIONCODE.ClientID, script);

            //当储位默认为空时，绑定符合条件的储位
            //if (txtCPOSITIONCODE.Text == "")
            //{
            //    //2015-12-02 储位编码默认为库存中最小符合出库单的数量   [ASRS专用]
            //    DataTable pdat = null;

            //    //2015-12-02 获得符合条件的第一个储位编号
            //    pdat = new Base_Cargospace().GetCargoSpaceListByStock(GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text), "", true, 15, txtIQUANTITY.Text.Trim());
            //    if (pdat.Rows.Count > 0)
            //    {
            //        txtCPOSITIONCODE.Text = pdat.Rows[0]["cpositioncode"].ToString();
            //    }
            //}
        }
    }
    public void LoadIDS()
    {
        DataRow dr = new OutBillQuery().GetOUTBILLLByID(txtID.Text.Trim());

        if (dr != null)
        {
            OutType = dr["OType"].ToString();
            hfOutAsn_id.Value = dr["COUTASNID"].ToString();
        }
        txtOutbillCode.Text = OutBillQuery.GetOutCode(txtID.Text.Trim());
        //获取出库单储位信息CQ 2014-4-26 18:08:45
        CPositonCode = OUTBILL_XDRule.GetOutBill_Position(txtIDS.Text);
    }
    public string OutType
    {
        get { return ViewState["OutType"].ToString(); }
        set { ViewState["OutType"] = value; }
    }

    #region IPage 成员

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

    //2015-12-28 添加
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTBILL_D');return false;";

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
        if (ConFigvalue == "1")
        {
            btnNew.Text = Resources.Lang.FrmOUTBILL_DEdit_Tips_PiHaoEdit;// "批号编辑";
            btnSave_SN.Text = Resources.Lang.FrmOUTBILL_DEdit_Tips_PiHaoSave; //"保存批号";
            grdSNDetial.Columns[3].HeaderText = Resources.Lang.FrmOUTBILL_DEdit_Tips_PiHao;// "批号";
        }
        else
        {
            btnNew.Text = Resources.Lang.ShowCartonSN_Div_BoxCode;
            btnSave_SN.Text = Resources.Lang.FrmOUTBILL_DEdit_Tips_SaveXiangHao;// "保存箱号";
        }
        //2015-12-28 添加
        //是否显示ASRS 1显示 0 不显示
        ASRSFig = this.GetConFig("000006");

        Help.DropDownListDataBind(GetLKLineID(), this.ddl_Line_ID, Resources.Lang.FrmOUTBILL_DEdit_Tips_XianBieSelect, "FUNCNAME", "EXTEND1", "");//请选择线别

        string ID = Request.QueryString["ID"];
        var modOUTbill = context.OUTBILL.Where(x => x.id == ID).FirstOrDefault();
        if (modOUTbill != null)
        {
            this.WorkType = modOUTbill.worktype;
            if (modOUTbill.worktype == "0") { //平库没有线别信息
                trLineInfo.Visible = false;
            }
            this.OutBillStatus = modOUTbill.cstatus;
            this.OutType = modOUTbill.otype.ToString();
            
            var modOutType = context.OUTTYPE.Where(x => x.cerpcode == modOUTbill.otype.ToString()).FirstOrDefault();
            if (modOutType.IsMatchSo == "1")
            {
                UseErpCode = "1";
            }
            else {
                UseErpCode = "0";
            }
            if (modOutType.IsMatchVendor == "1")
            {
                UseVendorCode = "1";
            }
            else {
                UseVendorCode = "0";
            }

            var modOutAsn = context.OUTASN.Where(x => x.id == modOUTbill.coutasnid).FirstOrDefault();
            if (modOutAsn != null) {
                this.BillNo = !string.IsNullOrEmpty(modOutAsn.billno) ? modOutAsn.billno : "";
                this.VendorCode = !string.IsNullOrEmpty(modOutAsn.cclientcode) ? modOutAsn.cclientcode : "";
            }
        }
        //栈板号的字段的显示根据配置的模式进行动态判断是否显示栈板号列
        if (SYSConfig == "1")//栈板-栈板/箱-箱模式
        {
            grdSNDetial.Columns[4].Visible = false;
        }
        //根据配置值动态显示button的值
        this.btnNew.Text = CurrentConfigUnitName;
        this.btnSave_SN.Text = Resources.Lang.Common_btnSave + CurrentConfigUnitName;

    }



    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        string ids = Request.QueryString["IDS"];
        OUTBILL_D entity = this.context.OUTBILL_D.Where(x=>x.ids == ids).FirstOrDefault();
        if (entity != null)
        {
            this.txtIDS.Text = entity.ids;
            this.txtID.Text = entity.id;
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
            this.txtAsn_D_IDS.Text = entity.asn_d_ids;
            txtLineId.Text = entity.lineid.HasValue ? entity.lineid.Value.ToString() : "";
            txtOutAsndLineID.Text = entity.outasndlineid.HasValue ? entity.outasndlineid.Value.ToString() : "";
            this.dplCSTATUS.SelectedValue = entity.cstatus;
            this.txtIQUANTITY.Text = Convert.ToDecimal(entity.iquantity).ToString("f2");
            this.txtCPOSITIONCODE.Text = entity.cpositioncode;
            this.txtCPOSITION.Text = entity.cposition;
            this.txtCINVBARCODE.Text = entity.cinvbarcode;
            //Note by Qamar 2020-11-27
            this.txtCINVCODE.Text = entity.cinvcode.Substring(0, entity.cinvcode.Length - 2);
            this.txtRANK_FINAL.Text = entity.cinvcode.Substring(entity.cinvcode.Length - 1, 1);
            if (txtRANK_FINAL.Text.Trim() == "_")
                txtRANK_FINAL.Text = "";
            //this.txtCINVCODE.Text = entity.cinvcode;
            this.txtCINVNAME.Text = entity.cinvname;
            this.txtCERPCODELINE.Text = entity.cerpcodeline;
            this.txtDOUTDATE.Text = Convert.ToDateTime(entity.doutdate).ToString("yyyy-MM-dd HH:mm:ss");
            this.txtCOUTPERSONCODE.Text = entity.coutpersoncode;
            this.txtCMEMO.Text = entity.cmemo;
            this.hfOriginalQty.Value = entity.iquantity.ToString();
            this.txtLINE_QTY.Text = entity.line_qty.HasValue ? entity.line_qty.Value.ToString("f2") : "";

            CINVNAME = entity.cinvname;

            //Roger 2013/11/28 14:02:35 SN整合
            DataRow dr = new OUTBILL_XDRule().GetOUTBILLLByID(entity.id.ToString());
            if (entity.asrs_status == 0 && dr != null)
            {
                var IsExistTempOutBill_D = OUTBILL_XDRule.ValidateIsExistTemp_OutBill_D(dr["CTICKETCODE"].ToString().Trim());
                var IsSpecialBill = OUTBILL_XDRule.IsSpecialBill(this.txtID.Text.Trim());
                if ((dr["CSTATUS"].ToString().Length > 0 && dr["CSTATUS"].ToString().Equals("0")) && !IsExistTempOutBill_D && !IsSpecialBill)//|| OutType == "35" || OutType == "33"
                {
                    HidField_Enable.Value = "0";
                }
                else
                {
                    SetFalse();
                }
            }
            else
            {
                SetFalse();
            }


            var modOutBill = context.OUTBILL.Where(x => x.id == entity.id).FirstOrDefault();
            this.IsTemporary = modOutBill.IsTemporary == null ? "0" : modOutBill.IsTemporary;

            if (this.WorkType != "0")
            {
                this.ddl_Line_ID.SelectedValue = entity.wire;

                if (!string.IsNullOrEmpty(ddl_Line_ID.SelectedValue))
                {
                    Help.DropDownListDataBind(GetOutPallet(ddl_Line_ID.SelectedValue, OutType), this.ddl_Pallet_Code, Resources.Lang.FrmOUTBILL_DEdit_Tips_ZhanDianSelect, "FUNCNAME", "EXTEND1", "");//请选择站点
                }

                // WL 20160516 
                this.ddl_Pallet_Code.SelectedValue = entity.pallet_code;
            }
            else
            {
                
                if (modOutBill != null && modOutBill.operationtype == 1)
                {
                    if (!string.IsNullOrEmpty(entity.cpositioncode))
                    {
                        if (entity.cstatus != "2")
                        {
                            btnNew.Enabled = true;
                            btnDeleteSn.Visible = true;
                            btnSave_SN.Enabled = true;
                        }
                        else
                        {
                            btnNew.Enabled = false;
                            btnSave_SN.Enabled = false;
                        }
                    }
                }
            }
            showSn.SetSearchCinvCode = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);

            if (UseErpCode=="1"){
                var modOutAsn_d = context.OUTASN_D.Where(x=>x.ids == entity.asn_d_ids).FirstOrDefault();
                if(modOutAsn_d!=null){
                    CSO = modOutAsn_d.cso != null ? modOutAsn_d.cso : string.Empty;
                }
            }
            //拼板出库不可以做任何操作
            //var modOutBill1 = context.OUTBILL.Where(x => x.id == entity.id).FirstOrDefault();
            if(IsQuery)//if (modOutBill1 != null && (modOutBill1.otype.ToString().Equals("1303") || modOutBill1.otype.ToString().Equals("1302"))) //1302 库存调整出库
            {
                btnSave.Enabled = false;
                btnNew.Visible = false;
                btnDeleteSn.Enabled = false;
                btnGetLikeCargoSpace.Enabled = false;
                btnSave_SN.Enabled = false;              
            }
        }
    }
    public void SetFalse()
    {
        txtCINVCODE.Enabled = false;
        txtRANK_FINAL.Enabled = false;
        txtCINVNAME.Enabled = false;
        txtCINVBARCODE.Enabled = false;
        txtIQUANTITY.Enabled = false;
        txtLINE_QTY.Enabled = false;
        txtCPOSITIONCODE.Enabled = false;
        txtCPOSITION.Enabled = false;
        txtCERPCODELINE.Enabled = false;
        txtIOUTASNLINE.Enabled = false;
        txtDOUTDATE.Enabled = false;
        txtCOUTPERSONCODE.Enabled = false;
        txtCMEMO.Enabled = false;
        btnSave.Enabled = false;
        btnNew.Enabled = false;
        btnDeleteSn.Enabled = false;
        btnGetLikeCargoSpace.Enabled = false;
        btnSave_SN.Enabled = false;
        HidField_Enable.Value = "1";
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

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        #region 基础信息检查
        if (CINVNAME == null || CINVNAME.Length == 0)
        {
            string CINVCODE = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
            IGenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
            var caseList = from p in con.Get()
                           where p.cpartnumber == CINVCODE
                           select p;
            if (caseList.Count() > 0)
            {
                BASE_PART dr = caseList.ToList().FirstOrDefault();
                CINVNAME = dr.cpartname.ToString();
            }
        }
        else
        {
            //显示物料规格
            string CINVCODE = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
            IGenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
            var caseList = from p in con.Get()
                           where p.cpartnumber == CINVCODE
                           select p;
            if (caseList.Count() > 0)
            {
                BASE_PART dr = caseList.ToList().FirstOrDefault();
                if (!string.IsNullOrEmpty(dr.cspecifications))
                {
                    txtcspecifications.Text = dr.cspecifications.ToString();
                }
            }
        }
        this.txtCINVNAME.Text = CINVNAME;

        if (this.txtID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_NeedZhuBiaoID);//主表编号项不允许空！
            this.SetFocus(txtID);
            return false;
        }
        //
        if (this.dplCSTATUS.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_NeedZhuangTai);//状态项不允许空！
            this.SetFocus(dplCSTATUS);
            return false;
        }
        //
        if (this.dplCSTATUS.Text.Trim().Length > 0)
        {
            if (this.dplCSTATUS.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_ZhuangTaiLength);//状态项超过指定的长度20！
                this.SetFocus(dplCSTATUS);
                return false;
            }
        }
        //
        if (this.txtIQUANTITY.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_NeedShuLiang);//数量项不允许空！
            this.SetFocus(txtIQUANTITY);
            return false;
        }
        //检查数量，不允许小数，负数，0 CQ 2014-2-13 13:39:48
        string errmsg = string.Empty;
        if (!(Comm_Fun.Fun_IsDecimal(txtIQUANTITY.Text.Trim(), 0, 0, 1, out errmsg)))
        {
            this.Alert(errmsg);
            this.SetFocus(txtIQUANTITY);
            return false;
        }

        if (this.txtLINE_QTY.Text.Trim().Length > 0)
        {
            //检查数量，不允许小数，负数，0 CQ 2014-2-13 13:39:48
            if (!(Comm_Fun.Fun_IsDecimal(txtLINE_QTY.Text.Trim(), 0, 0, 0, out errmsg)))
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_ChaoFa + errmsg);//超發
                this.SetFocus(txtLINE_QTY);
                return false;
            }

        }
        if (this.txtCINVBARCODE.Text.Trim().Length > 0)
        {
            if (this.txtCINVBARCODE.Text.GetLengthByByte() > 300)
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_BarCodeLength);//物料条码项超过指定的长度300！
                this.SetFocus(txtCINVBARCODE);
                return false;
            }
        }
        //
        if (GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text) == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_NeedCinvcode);//料号项不允许空！
            this.SetFocus(txtCINVCODE);
            return false;
        }
        //
        if (GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text).Length > 0)
        {
            if (this.txtCINVCODE.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_CinvcodeLength);//料号项超过指定的长度50！
                this.SetFocus(txtCINVCODE);
                return false;
            }
        }

        string msg2 = string.Empty;
        //CQ 2014-12-3 16:31:55 新增检查税别是否为空
        if (!(Comm_Fun.Fun_CheckTax_SameBond(GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text).ToUpper(), out msg2)))
        {
            this.Alert(msg2);
            this.SetFocus(txtCINVCODE);
            return false;
        }


        if (this.txtCERPCODELINE.Text.Trim().Length > 0)
        {
            if (this.txtCERPCODELINE.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_ErpCodeLength);//子表ERP单号项次项超过指定的长度20！
                this.SetFocus(txtCERPCODELINE);
                return false;
            }
        }
        //
        if (this.txtDOUTDATE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_NeedChuKuXiang);//出库项不允许空！
            this.SetFocus(txtDOUTDATE);
            return false;
        }
        //
        if (this.txtDOUTDATE.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate(this.txtDOUTDATE.Text) == false)
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_RiQiWuXiao);//出库项不是有效的日期！
                this.SetFocus(txtDOUTDATE);
                return false;
            }
        }
        //
        if (this.txtCOUTPERSONCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_NeedChuKuRen);//出库人不允许空！
            this.SetFocus(txtCOUTPERSONCODE);
            return false;
        }
        //
        if (this.txtCOUTPERSONCODE.Text.Trim().Length > 0)
        {
            if (this.txtCOUTPERSONCODE.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_ChuKuRenLength);//出库人超过指定的长度20！
                this.SetFocus(txtCOUTPERSONCODE);
                return false;
            }
        }
        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 200)
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_BeiZhuLength);//备注项超过指定的长度200！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        //
        if (this.txtIOUTASNLINE.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDecimal(this.txtIOUTASNLINE.Text) == false)
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_XiangCiError);//出库通知单项次项不是有效的十进制数字！
                this.SetFocus(txtIOUTASNLINE);
                return false;
            }
        }
        string workType = "0";
        if (this.Operation() == SYSOperation.Modify)
        {
            workType = "1";
        }
        if (this.txtAsn_D_IDS.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_NeedMingXiIDS);//出库通知单明细IDS项不允许空！
            this.SetFocus(txtAsn_D_IDS);
            return false;
        }

        if (this.txtCPOSITIONCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_NeedCpositionCode);//储位编码项不允许空！
            this.SetFocus(txtCPOSITIONCODE);
            return false;
        }
        //
        if (this.txtCPOSITIONCODE.Text.Trim().Length > 0)
        {
            if (this.txtCPOSITIONCODE.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_CpositionCodeLeng);//储位编码项超过指定的长度30！
                this.SetFocus(txtCPOSITIONCODE);
                return false;
            }
        }
        #endregion

        //判断是否大于明细数量
        if (hfGetQty.Value.Trim() != "0" && Convert.ToDecimal(this.txtIQUANTITY.Text.Trim()) > Convert.ToDecimal(hfGetQty.Value.Trim()))
        {
            this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_ShuLiangBuNeng + "[" + hfGetQty.Value.Trim() + "]！");//数量不能大于
            this.SetFocus(txtIQUANTITY);
            return false;
        }
        if (workType == "0")
        {
            txtIDS.Text = "";
        }
        //检查出库单的数量
        string msg1 = string.Empty;
        if (!(Comm_Fun.Fun_Check_Asn_DQty(txtAsn_D_IDS.Text, txtIDS.Text, GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text), txtIQUANTITY.Text.ToDecimal(), workType.ToInt32(), 1, out msg1)))
        {
            this.Alert(msg1);
            this.SetFocus(txtIQUANTITY);
            return false;
        }

        //判断储位是否冻结 1-冻结
        if (new Base_Cargospace().CheckCpositionStatus(txtCPOSITIONCODE.Text.Trim().ToUpper(), "1"))
        {
            this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_CpositionStatus);//储位状态为[冻结]不能保存
            this.SetFocus(txtCPOSITIONCODE);
            return false;
        }
        //
        if (this.txtLINE_QTY.Text.Length > 0 && Convert.ToDecimal(this.txtLINE_QTY.Text) > 0 && this.txtCPOSITION.Text.Trim() != "")
        {
            //批次号管理
            if (!(Comm_Fun.Fun_CheckCinvCode_ChaoFa(txtIDS.Text.Trim(), GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text),
                                                        WmsWebUserInfo.GetCurrentUser().UserNo, "0", txtLINE_QTY.Text.Trim(), "")))
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_BuNengChaoFa);//当前料号不允许超发！
                this.SetFocus(txtCPOSITION);
                return false;
            }
        }
        //检查料号是否在通知单内
        string msg = Base_Part.CheckWIP_PartByBillId(txtID.Text, GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text));
        if (!msg.Equals("OK"))
        {
            this.Alert(msg);
            this.SetFocus(txtCINVCODE);
            return false;
        }

        //20131105102731 通知单存在修改中的料时，不允许生成出库单
        var resultnew = Comm_Fun.CanAsnDebit(txtID.Text.Trim(), GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text), "2", "", "");
        if (!resultnew.Equals("OK"))
        {
            Alert(resultnew);
            return false;
        }
        //Roger SN整合 2013/12/12 11:39:54
        if (this.Operation() == SYSOperation.Modify && OUTBILL_XDRule.ExistSN(txtIDS.Text))
        {
            IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
            var caseList = from p in con.Get()
                           where p.ids == txtIDS.Text
                           select p;
            if (caseList.Count() > 0)
            {
                OUTBILL_D entity = caseList.ToList().FirstOrDefault();
                if (entity.iquantity != Convert.ToDecimal(txtIQUANTITY.Text.Trim()))
                {
                    Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_DeleteAndUpdate);//已经存在SN，请删除后修改数量
                    return false;
                }
                if ((entity.line_qty ?? 0) != Convert.ToDecimal(txtLINE_QTY.Text.Trim() == string.Empty ? "0" : txtLINE_QTY.Text.Trim()))
                {
                    Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_UpdateChaoFa);//已经存在SN，请删除后修改超发数量
                    return false;
                }
                if (!entity.cinvcode.Equals(GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text)))
                {
                    Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_UpdateCinvcode);//已经存在SN，请删除后修改料号
                    return false;
                }
            }
        }

        if (this.WorkType != "0")//非平库时需提供线别，站点信息
        {
            // WL 20160516
            if (this.ddl_Line_ID.SelectedValue.ToString() == "")
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_XianBieSelect + "！");//请选择线别！
                return false;
            }
            // WL 20160516
            if (this.ddl_Pallet_Code.SelectedValue.ToString() == "")
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_ZhanDianSelect + "！");//请选择站点！
                return false;
            }

            var modCposition = context.BASE_CARGOSPACE.Where(x => x.cpositioncode == txtCPOSITIONCODE.Text.Trim().ToUpper()).FirstOrDefault();
            if (modCposition == null)
            {
                this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_CpositionError);//储位信息异常！
                return false;
            }
            else
            {
                if (!modCposition.lineid.Equals(this.ddl_Line_ID.SelectedValue.ToString()))
                {
                    this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_BuPiPei);//储位与线别不匹配，请确认！
                    return false;
                }
            }

            #region 判断储位是否在未完成的入库单和调拨单中
            using (var modContext = context)
            {           
                if (modContext.ALLOCATE.Where(x => (x.cstatus == "0" || x.cstatus == "1") && modContext.ALLOCATE_D.Any(y => y.id == x.id && y.cpositioncode == modCposition.cpositioncode)).Any())
                {
                    this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_AllocateZhanYong);//您选择的储位已被其它未完成的调拨单占用！
                    return false;
                }

                if (modContext.OUTBILL.Where(x => x.cstatus == "0" && !x.id.Equals(txtID.Text.Trim().ToString()) && modContext.OUTBILL_D.Any(y => y.id == x.id && y.cpositioncode == modCposition.cpositioncode)).Any())
                {
                    this.Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_OutBillZhanYong);//您选择的储位已被其它未处理的出库单占用！
                    return false;
                }
            }
            #endregion

        }

        return true;
    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public OUTBILL_D SendData()
    {
        OUTBILL_D entity = new OUTBILL_D();
        entity.isgoback = 0;//默认没有做返库的动作 pan gao 20160529
        //
        this.txtIDS.Text = this.txtIDS.Text.Trim();
        if (this.txtIDS.Text.Trim().Length > 0)
        {
            entity.ids = txtIDS.Text.Trim().ToString();
        }
        else
        {
            entity.ids = Guid.NewGuid().ToString();
        }
        this.txtID.Text = this.txtID.Text.Trim();
        if (this.txtID.Text.Trim().Length > 0)
        {
            entity.id = txtID.Text.Trim().ToString();
        }
        else
        {
            entity.id = Guid.NewGuid().ToString();
        }

        this.txtAsn_D_IDS.Text = this.txtAsn_D_IDS.Text.Trim();
        if (this.txtAsn_D_IDS.Text.Trim().Length > 0)
        {
            entity.asn_d_ids = txtAsn_D_IDS.Text.Trim().ToString();
        }
        else
        {
            entity.asn_d_ids = "";
        }
        if (!txtLineId.Text.IsNullOrEmpty())
        {
            entity.lineid = txtLineId.Text.ToInt();
        }
        if (!txtOutAsndLineID.Text.IsNullOrEmpty())
        {
            entity.outasndlineid = txtOutAsndLineID.Text.ToInt();

        }
        this.dplCSTATUS.SelectedValue = this.dplCSTATUS.SelectedValue;
        if (this.dplCSTATUS.SelectedValue.Trim().Length > 0)
        {
            entity.cstatus = this.dplCSTATUS.SelectedValue.Trim();
        }
        
        this.txtIQUANTITY.Text = this.txtIQUANTITY.Text.Trim();
        if (this.txtIQUANTITY.Text.Trim().Length > 0)
        {
            entity.iquantity = txtIQUANTITY.Text.ToDecimal();
        }
        else
        {
            entity.iquantity = 0;
        }
        this.txtLINE_QTY.Text = this.txtLINE_QTY.Text.Trim();
        if (this.txtLINE_QTY.Text.Trim().Length > 0)
        {
            entity.line_qty = this.txtLINE_QTY.Text.Trim().ToDecimal();
        }

        this.txtCPOSITIONCODE.Text = this.txtCPOSITIONCODE.Text.Trim();
        if (this.txtCPOSITIONCODE.Text.Trim().Length > 0)
        {
            entity.cpositioncode = txtCPOSITIONCODE.Text.Trim();
            IGenericRepository<BASE_CARGOSPACE> con = new GenericRepository<BASE_CARGOSPACE>(context);
            var caseList = from p in con.Get()
                           where p.cpositioncode == entity.cpositioncode
                           select p;
            if (caseList.Count() > 0)
            {
                txtCPOSITION.Text = caseList.ToList().FirstOrDefault<BASE_CARGOSPACE>().cposition; ;
            }
        }
        else
        {
            entity.cpositioncode = string.Empty;
        }

        this.txtCPOSITION.Text = this.txtCPOSITION.Text.Trim();
        if (this.txtCPOSITION.Text.Trim().Length > 0)
        {
            entity.cposition = txtCPOSITION.Text.Trim();
        }
        else
        {
            entity.cposition = string.Empty;
        }

        this.txtCINVBARCODE.Text = this.txtCINVBARCODE.Text.Trim();
        if (this.txtCINVBARCODE.Text.Trim().Length > 0)
        {
            entity.cinvbarcode = txtCINVBARCODE.Text.Trim();
        }
        else
        {
            entity.cinvbarcode = string.Empty;
        }

        this.txtCINVCODE.Text = txtCINVCODE.Text.Trim();
        if (txtCINVCODE.Text.Trim().Length > 0)
        {
            entity.cinvcode = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
            IGenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
            var caseList = from p in con.Get()
                           where p.cpartnumber == entity.cinvcode
                           select p;
            if (caseList.Count() > 0)
            {
                entity.cinvname = caseList.ToList().FirstOrDefault<BASE_PART>().cpartname; ;
            }
        }

        this.txtCERPCODELINE.Text = this.txtCERPCODELINE.Text.Trim();
        if (this.txtCERPCODELINE.Text.Trim().Length > 0)
        {
            entity.cerpcodeline = txtCERPCODELINE.Text.Trim();
        }
        else
        {
            entity.cerpcodeline = string.Empty;
        }
        this.txtDOUTDATE.Text = this.txtDOUTDATE.Text.Trim();
        if (this.txtDOUTDATE.Text.Trim().Length > 0)
        {
            entity.doutdate = txtDOUTDATE.Text.Trim().ToDateTime("yyyy-MM-dd HH:mm:ss");
        }

        this.txtCOUTPERSONCODE.Text = this.txtCOUTPERSONCODE.Text.Trim();
        if (this.txtCOUTPERSONCODE.Text.Trim().Length > 0)
        {
            entity.coutpersoncode = txtCOUTPERSONCODE.Text.Trim();
        }
        else
        {
            entity.coutpersoncode = string.Empty;
        }

        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            entity.cmemo = txtCMEMO.Text.Trim();
        }
        else
        {
            entity.cmemo = string.Empty;
        }
        this.txtIOUTASNLINE.Text = this.txtIOUTASNLINE.Text.Trim();
        if (this.txtIOUTASNLINE.Text.Trim().Length > 0)
        {
            entity.ioutasnline = txtIOUTASNLINE.Text.Trim().ToDecimal();
        }
        if (this.WorkType != "0")
        {
            entity.pallet_code = ddl_Pallet_Code.SelectedValue.ToString();
            entity.wire = ddl_Line_ID.SelectedValue.ToString();
            entity.cdefine1 = entity.cpositioncode;
            entity.cdefine2 = entity.cposition;
        }

        
        return entity;

    }


    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
        btnSave.Enabled = false;//20130513154835
        if (this.CheckData())
        {
            OUTBILL_D entity = (OUTBILL_D)this.SendData();
            txtCINVNAME.Text = entity.cinvname;
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";

            try
            {
                if (this.Operation() == SYSOperation.Modify)
                {
                    entity.ids = txtIDS.Text.Trim();
                    strKeyID = entity.ids;
                    entity.asrs_status = 0;
                    con.Update(entity);
                    con.Save();
                    string saveSnMsg = string.Empty;
                    if (this.WorkType != "0")//非平库时自动产生sn
                    {
                        saveSnMsg = SaveSN();
                        //Note by Qamar 2020-11-22
                        Save_Others_OUTBILL_D_and_SN(entity);
                    }
                    this.AlertAndBack("FrmOUTBILL_DEdit.aspx?IDS=" + strKeyID + "&" + BuildQueryString(SYSOperation.Modify, this.txtID.Text.Trim()), Resources.Lang.WMS_Common_Msg_UpdateSuccess + saveSnMsg);//更新成功!
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    entity.lineid = GetMaxLineId(txtID.Text) + 1;
                    entity.ids = Guid.NewGuid().ToString();
                    strKeyID = entity.ids;
                    entity.asrs_status = 0;
                    con.Insert(entity);
                    con.Save();
                    string saveSnMsg = string.Empty;
                    if (this.WorkType != "0")//非平库时自动产生sn
                    {
                        saveSnMsg = SaveSN();
                    }
                    this.AlertAndBack("FrmOUTBILL_DEdit.aspx?IDS=" + strKeyID + "&" + BuildQueryString(SYSOperation.New, this.txtID.Text.Trim()), Resources.Lang.WMS_Common_Msg_SaveSuccess  + saveSnMsg);//保存成功!
                }
               
            }
            catch (Exception E)
            {
                btnSave.Enabled = true;
                lblErrorMsg.Text = Resources.Lang.WMS_Common_Msg_SaveFailed + E.Message;//失败
            }
            this.GridBind();
        }
        btnSave.Enabled = true;
    }

    /// <summary>
    /// 查看线边储位
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetLikeCargoSpace_Click(object sender, EventArgs e)
    {
        if (txtCPOSITIONCODE.Text.Trim().Length > 0)
        {
            this.txtCMEMO.Text = new Base_Part().GetLikeCargoSpaceByOutBill_id(txtID.Text.Trim(), txtCPOSITIONCODE.Text.Trim());
        }
        else
        {
            Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_XuanzeCposition);//请先选择储位!
        }
    }

    public void GridBind()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtCPOSITIONCODE.Text.Trim()) && !string.IsNullOrEmpty(GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text)))
            {
                var ids = this.txtIDS.Text.Trim();
                using (var modContext = context) {
                    var queryList = from p in modContext.V_OUTBILL_SN
                                   orderby p.id descending
                                   where p.outbill_d_ids.ToString() == ids 
                                   select p;
                    if (queryList != null)
                    {
                        AspNetPager1.RecordCount = queryList.Count();
                        AspNetPager1.PageSize = this.PageSize;
                    }
                    AspNetPager1.CustomInfoHTML = " " + Resources.Lang.WMS_Common_Pager_PageCount + ":<b>" + "</b>";//总页数
                    grdSNDetial.DataSource = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
                    grdSNDetial.DataBind();
                
                }
            }
        }
        catch(Exception ex)
        {
            string error = ex.Message.ToString();
        }
    }










    /******************************************************************************************************************
    ********************************************************************************************************************/

    //记录SN的状态 0 必须一次 1 可以部分
    public static string SNEnable = "0";

    //根据料号设置SN的状态
    public void SetSNEnable()
    {
        //SN必须一次出库
        if (Base_Part.CheckGroupPart(GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text)))
        {
            SNEnable = "0";
        }
        else
        {
            SNEnable = "1";
        }

    }

    public string CPositonCode
    {
        set { ViewState["CPositonCode"] = value; }
        get
        {
            if (ViewState["CPositonCode"] != null)
            {
                return ViewState["CPositonCode"].ToString();
            }
            return "";
        }
    }

    //pan gao 20160607
    public bool CheckSNQuantity()
    {
        bool bl = true;
        var dtSN = GetGridViewData();
        var i = 0;
        foreach (DataRow dr in dtSN.Rows)
        {
            i++;
            if (dr["IsSelected"].ToString() == "1")
            {
                var qty = dr["QUANTITY"].ToString();

                if (string.IsNullOrEmpty(qty))
                {
                    Alert(Resources.Lang.WMS_Common_Element_Di + "[" + i + "]" + Resources.Lang.WMS_Common_Element_Hang + "," + Resources.Lang.FrmOUTBILL_DEdit_Tips_QuantityGeShi);//Alert("第[" + i + "]行,数量必须是数字！");
                    return false;
                }
                else {
                    string msg = string.Empty;
                    //检查数量，不允许小数，负数，0 CQ 2015-2-12 13:38:24
                    if (!(Comm_Function.Fun_IsDecimal(qty, 0, 0, 1, out msg)))
                    {
                        this.Alert(Resources.Lang.WMS_Common_Element_Di + "[" + i + "]" + Resources.Lang.WMS_Common_Element_Hang + "," + msg);//this.Alert("第[" + i + "]行,"+ msg);
                        return false;
                    }
                }
                var ALLQTY = dr["ALLQTY"].ToString();
                if (!string.IsNullOrEmpty(qty) && !string.IsNullOrEmpty(ALLQTY))
                {
                    decimal m = 0;
                    decimal n = 0;
                    if (decimal.TryParse(qty, out m) && decimal.TryParse(ALLQTY, out n))
                    {
                        if (m > n)
                        {
                            Alert(Resources.Lang.WMS_Common_Element_Di + "[" + i + "]" + Resources.Lang.WMS_Common_Element_Hang + "," + Resources.Lang.FrmOUTBILL_DEdit_Tips_ZongKuCunLiang);//Alert("第[" + i + "]行,出库数量不能大于库存总数量！");
                            return false;
                        }
                    }
                }

                var SNCode = dr["SN_CODE"].ToString();

                //验证当前sn是否存在于未完成的暂存调立库中；
                if (InBill.CheckExistsInTempAllo(SNCode))
                {
                    //第，行,已存在于未完成的暂存调立库的调拨单中
                    Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + ",[" + SNCode + "]" + Resources.Lang.FrmINBILL_DEdit_MSG113_hasTempAllo);
                    return false;
                }           
            }
        }

        //判断是否有选择记录
        var seleSum = dtSN.AsEnumerable().Cast<DataRow>().Where(dr => dr["IsSelected"].ToString() == "1").Count<DataRow>();

        //有选择之后，再判断选择的数量与入库单的数量是否一致
        if (seleSum > 0)
        {
            var snQty = dtSN.AsEnumerable().Cast<DataRow>().
                Where(dr => dr["IsSelected"].ToString() == "1").
                Select(dr => decimal.Parse(dr["QUANTITY"].ToString())).Sum();

            decimal allQ = 0;
            if (decimal.TryParse(txtIQUANTITY.Text.Trim(), out allQ))
            {
                if (snQty != allQ)
                {
                    Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_ShuLiangBuDeng);//出库总数量与出库单中的数量不相等！
                    return false;
                }
            }

        }

        return bl;
    }
    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckSNData()
    {
        if (CPositonCode.Trim() == "")
        {
            Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_NoPosition);//该明细没有出库储位,请确定储位后编辑!
            return false;
        }

        //校验数量和是否与明细数量一致
        var dtSN = GetGridViewData();
        var i = 0;
        foreach (DataRow dr in dtSN.Rows)
        {
            i++;
            var snCode = dr["SN_CODE"].ToString().Trim();

            var snDatecode = dr["datecode"].ToString().Trim();

            var snFurnaceno = dr["furnaceno"].ToString().Trim();

            #region 检查数量
            //判断数字
            var qty = dr["QUANTITY"].ToString();
            if (string.IsNullOrEmpty(qty))
            {
                Alert(Resources.Lang.WMS_Common_Element_Di + "[" + i + "]" + Resources.Lang.WMS_Common_Element_Hang + "," + Resources.Lang.FrmOUTBILL_DEdit_Tips_NoQuantity);//Alert("第[" + i + "]行,数量为空");
                return false;
            }
            string errmsg = string.Empty;
            if (!Comm_Fun.Fun_IsDecimal(qty, 0, 0, 0, out errmsg))
            {
                Alert(Resources.Lang.WMS_Common_Element_Di + "[" + i + "]" + Resources.Lang.WMS_Common_Element_Hang + ",[" + qty + "]" + errmsg);//Alert("第[" + i + "]行,[" + qty + "]" + errmsg);
                return false;
            }
            //判断超发数量
            var lineqty = dr["LINE_QTY"].ToString();
            if (string.IsNullOrEmpty(lineqty))
            {
                if (!Comm_Fun.Fun_IsDecimal(lineqty, 0, 0, 0, out errmsg))
                {
                    Alert(Resources.Lang.WMS_Common_Element_Di + "[" + i + "]" + Resources.Lang.WMS_Common_Element_Hang + ",[" + lineqty + "]" + errmsg);//Alert("第[" + i + "]行,[" + lineqty + "]" + errmsg);
                    return false;
                }
            }
            #endregion

            decimal snysl = 0;

            var bcID = dr["ID"].ToString();
            decimal bcsl = 0;
            bcsl = OUTBILL_XDRule.GetSN_DEditByID(bcID);

            if (ConFigvalue == "1")
            {
                #region 批号管理
                //批次管理 
                //检查批次号格式
                string msg = string.Empty;
                //if (!(Comm_Function.CheckFun_GetBatchNo(snCode, "", out msg)))
                //{
                //    Alert("第[" + i + "]行," + msg);
                //    return false;
                //}


                //检查批次号是否存在于该料号储位上
                //if (!Comm_Function.Fun_GetDateNum_FromBatch(snCode, GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text), CPositonCode, 1, snFurnaceno, snDatecode, out snysl, out sdatecode))
                //{
                //    Alert("第[" + i + "]行,[" + snCode + "]不存在或不存在料号[" + GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text) + "]储位[" + CPositonCode + "]上");
                //    return false;
                //}

                decimal num = decimal.Parse(qty);
                decimal line = 0;
                if (!string.IsNullOrEmpty(lineqty))
                {
                    line = decimal.Parse(lineqty);
                }
                decimal snSum = snysl + bcsl;
                if (num + line > snSum)
                {
                    Alert(Resources.Lang.WMS_Common_Element_Di + "[" + i + "]" + Resources.Lang.WMS_Common_Element_Hang + "," + Resources.Lang.FrmOUTBILL_DEdit_Tips_DaYuPiHaoQuantity + "[" + snSum + "]");// Alert("第[" + i + "]行,數量+超發數量>批号數量[" + snSum + "]");
                    return false;
                }

                #endregion
            }
            else
            {
                #region SN管理

                //判断是否为栈板或箱
                string sntype = OUTBILL_XDRule.GetSNType(snCode);
                if (sntype.Equals(""))//非栈板箱
                {
                    //判断是否符合要求
                    //var msg = OUTBILL_DRule.CheckSNFormate(snCode).Trim();
                    //if (msg.Length != 6)
                    //{
                    //    Alert("第[" + i + "]行,[" + snCode + "]" + msg);
                    //    return false;
                    //}
                }

                //判断SN是否存在，如果不存在，则报错
                //var SnExist = OUTBILL_DRule.CheckSnExist(snCode, dr["ID"].ToString().Trim());
                //if (!SnExist)
                //{
                //    Alert("第[" + i + "]行,[" + snCode + "]不存在于入库SN或栈板箱中或此SN已经被使用");
                //    return false;
                //}


                //if (SNEnable == "1")
                //{
                //获取该条SN的本身数量

                //获取SN数量
                snysl = Comm_Fun.Fun_GetNum_FromSN(snCode);

                decimal num = decimal.Parse(qty);
                decimal line = 0;
                if (!string.IsNullOrEmpty(lineqty))
                {
                    line = decimal.Parse(lineqty);
                }
                //decimal snSum = snysl + bcsl;
                //if (num + line > snSum)
                //{
                //    Alert("第[" + i + "]行,數量+超發數量>SN數量[" + snSum + "]");
                //    return false;
                //}
                //if (SNEnable == "0")
                //{
                //    if (num + line != snSum)
                //    {
                //        Alert("第[" + i + "]行,數量+超發數量与SN數量[" + snSum + "]不一致");
                //        return false;
                //    }
                //}


                var strtype = dr["SNTYPE"].ToString();
                if (strtype == "1" || strtype == "2")
                {
                    //获取栈板箱的数量
                    //decimal pcqty = OUTBILL_DRule.GetPallorCarQty(snCode);
                    //if (pcqty != (num + line))
                    //{
                    //    Alert("第[" + i + "]行,棧板箱不能修改數量！");
                    //    return false;
                    //}
                }

                //  }

                //判断储位CQ 2014-4-26 17:40:14
                string msgerr = string.Empty;
                //if (Comm_Function.Fun_CheckSN_PositionCode(snCode, GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text), CPositonCode,
                //                                           WmsWebUserInfo.GetCurrentUser().UserNo, "2", "", "", out msgerr))
                //{
                //    Alert("第[" + i + "]行," + msgerr);
                //    return false;
                //}

                //判断SN是否冻结CQ  2014-7-18 17:51:07
                //if (Comm_Function.Fun_CheckLock_SN(GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text), CPositonCode, snCode, 6, txtOutbillCode.Text,
                //                                   WmsWebUserInfo.GetCurrentUser().UserNo, "", "", out msgerr))
                //{
                //    Alert("第[" + i + "]行," + msgerr + "不能保存");
                //    return false;
                //}

                #endregion
            }


        }

        if (ConFigvalue == "1")
        {
            #region 批号管理

            //判断数量合计
            var snQty = dtSN.AsEnumerable().Cast<DataRow>().Select(dr => decimal.Parse(dr["QUANTITY"].ToString())).Sum();
            var Qty = decimal.Parse(txtIQUANTITY.Text.Trim());
            if (snQty > Qty)
            {
                Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_PiHaoTotal + "[" + snQty + "]" + Resources.Lang.FrmOUTBILL_DEdit_Tips_DaYuMingXi + "[" + Qty + "]");//Alert("批号数量总和[" + snQty + "]大于明细数量[" + Qty + "]");
                return false;
            }

            //超发数量
            var snLineQty = dtSN.AsEnumerable().Cast<DataRow>().Select(dr => decimal.Parse(string.IsNullOrEmpty(dr["LINE_QTY"].ToString()) ? "0" : dr["LINE_QTY"].ToString())).Sum();
            var LineQty = decimal.Parse(string.IsNullOrEmpty(txtLINE_QTY.Text.Trim()) ? "0" : txtLINE_QTY.Text.Trim());
            if (snLineQty > LineQty)
            {
                Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_PiHaoChaoFaTotal + "[" + snLineQty + "]" + Resources.Lang.FrmOUTBILL_DEdit_Tips_DaYuMingXiChaoFa + "[" + LineQty + "]");//Alert("批号超发数量总和[" + snLineQty + "]大于明细超发数量[" + LineQty + "]");
                return false;
            }
            //如果有相同的SN，则报错
            var dtfg = GetGridViewData();
            var listq =
                from t in dtfg.AsEnumerable()
                group t by new { t1 = t.Field<string>("SN_CODE") }
                    into m
                    select new
                    {
                        rowcount = m.Count()
                    };
            var count = listq.ToList().Exists(q => q.rowcount > 1);
            if (count)
            {
                Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_PiHaoXiangTong);//批号出库列表存在批号一致的数据
                return false;
            }

            #endregion
        }
        else
        {
            #region SN管理

            //判断SN是否必须
            var IsSNPossible = false;

            //判断数量合计
            var snQty = dtSN.AsEnumerable().Cast<DataRow>().Select(dr => decimal.Parse(dr["QUANTITY"].ToString())).Sum();
            var Qty = decimal.Parse(txtIQUANTITY.Text.Trim());
            if (!IsSNPossible)
            {
                if (snQty > Qty)
                {
                    Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_SNTotal + "[" + snQty + "]" + Resources.Lang.FrmOUTBILL_DEdit_Tips_DaYuMingXi + "[" + Qty + "]");//Alert("SN数量总和[" + snQty + "]大于明细数量[" + Qty + "]");
                    return false;
                }
            }
            else
            {
                if (snQty != Qty)
                {
                    Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_SNTotal + "[" + snQty + "]" + Resources.Lang.FrmOUTBILL_DEdit_Tips_WithMingXiQuantity + "[" + Qty + "]" + Resources.Lang.FrmOUTBILL_DEdit_Tips_BuYiZhi);// Alert("SN数量总和[" + snQty + "]与明细数量[" + Qty + "]不一致");
                    return false;
                }
            }
            //超发数量
            var snLineQty = dtSN.AsEnumerable().Cast<DataRow>().Select(dr => decimal.Parse(string.IsNullOrEmpty(dr["LINE_QTY"].ToString()) ? "0" : dr["LINE_QTY"].ToString())).Sum();
            var LineQty = decimal.Parse(string.IsNullOrEmpty(txtLINE_QTY.Text.Trim()) ? "0" : txtLINE_QTY.Text.Trim());
            if (!IsSNPossible)
            {
                if (snLineQty > LineQty)
                {
                    Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_SNChaoFaTotal+"[" + snLineQty + "]" + Resources.Lang.FrmOUTBILL_DEdit_Tips_DaYuMingXiChaoFa + "[" + LineQty + "]");//Alert("SN超发数量总和[" + snLineQty + "]大于明细超发数量[" + LineQty + "]");
                    return false;
                }
            }
            else
            {
                if (snLineQty != LineQty)
                {
                    Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_SNChaoFaTotal + "[" + snLineQty + "]" + Resources.Lang.FrmOUTBILL_DEdit_Tips_WithMingXiChaoFa + "[" + LineQty + "]" + Resources.Lang.FrmOUTBILL_DEdit_Tips_BuYiZhi);//Alert("SN超发数量总和[" + snLineQty + "]与明细超发数量[" + LineQty + "]不一致");
                    return false;
                }
            }

            //如果有相同的SN，则报错
            var dtfg = GetGridViewData();
            var listq =
                from t in dtfg.AsEnumerable()
                group t by new { t1 = t.Field<string>("SN_CODE") }
                    into m
                    select new
                    {
                        rowcount = m.Count()
                    };
            var count = listq.ToList().Exists(q => q.rowcount > 1);
            if (count)
            {
                Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_SNYiZhi);//SN出库列表存在SN一致的数据
                return false;
            }

            #endregion
        }



        return true;
    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public OUTBILL_D_SN SendDataSN()
    {
        OUTBILL_D_SN entity = new OUTBILL_D_SN();
        //
        txtIDS.Text = txtIDS.Text.Trim();
        if (this.txtIDS.Text.Length > 0)
        {
            entity.outbill_d_ids = txtIDS.Text;
        }
        else
        {
            entity.outbill_d_ids = string.Empty;
        }
        //
        txtCINVCODE.Text = txtCINVCODE.Text.Trim();
        if (this.txtCINVCODE.Text.Length > 0)
        {
            entity.cinvcode = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
        }
        else
        {
            entity.cinvcode = string.Empty;
        }
        //
        txtIQUANTITY.Text = txtIQUANTITY.Text.Trim();
        if (this.txtIQUANTITY.Text.Length > 0)
        {
            entity.quantity = Convert.ToDecimal(txtIQUANTITY.Text);
        }
        else
        {

        }
        //
        entity.worktype = "0";
        //
        IGenericRepository<OUTBILL_D> OUTBILL_Dentity = new GenericRepository<OUTBILL_D>(context);
        var caseList = from p in OUTBILL_Dentity.Get()
                       where p.ids == txtIDS.Text.Trim()
                       select p;
        if (caseList.Count() > 0)
            entity.outbill_id = caseList.ToList().FirstOrDefault<OUTBILL_D>().id; ;

        entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
        entity.createtime = DateTime.Now;
        entity.lastvpdowner = WmsWebUserInfo.GetCurrentUser().UserNo;
        entity.lastvpdtime = DateTime.Now;
        return entity;

    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_SN_Click(object sender, EventArgs e)
    {
        int count = 0;
        for (int i = 0; i < grdSNDetial.Rows.Count; i++)
        {
            var hdnSelected = this.grdSNDetial.Rows[i].FindControl("chkSelect") as CheckBox;
            if (hdnSelected.Checked)
            {
                count++;
            }
        }
        if (count == 0)
        {
            Alert(Resources.Lang.FrmOUTBILL_DEdit_Tips_SelectMingXi);//请选择明细！
            return;
        }
        IGenericRepository<OUTBILL_D_SN> con = new GenericRepository<OUTBILL_D_SN>(context);
        if (this.CheckSNQuantity())
        {
            try
            {
                //删除SN
                var outbilldsn = new OUTBILL_XDRule();
                outbilldsn.DeleteSn(txtIDS.Text.Trim());
                string strpalletcode = "";
                //保存SN
                for (int i = 0; i < grdSNDetial.Rows.Count; i++)
                {
                    OUTBILL_D_SN entity = (OUTBILL_D_SN)this.SendDataSN();
                    var hdnSelected = this.grdSNDetial.Rows[i].FindControl("chkSelect") as CheckBox;
                    if (!hdnSelected.Checked)
                    {
                        continue;
                    }
                    entity.id = Guid.NewGuid().ToString();
                    entity.sn_code = ((TextBox)this.grdSNDetial.Rows[i].FindControl("lblSN")).Text;
                    entity.quantity = Convert.ToDecimal(((TextBox)this.grdSNDetial.Rows[i].FindControl("txtquantity")).Text);
                   // entity.outbilltype = ((HiddenField)this.grdSNDetial.Rows[i].FindControl("hdnoutbilltype")).Value;
                    string line_qty = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtLineQty")).Text;
                    if (!string.IsNullOrEmpty(line_qty))
                        entity.line_qty = Convert.ToDecimal(line_qty);
                    entity.furnaceno = ((Label)this.grdSNDetial.Rows[i].FindControl("labFurnaceno")).Text;
                    var sntype = ((Label)this.grdSNDetial.Rows[i].FindControl("labtype")).Text;
                    
                    entity.sntype = 0;
                    entity.datecode = Convert.ToDecimal(((TextBox)this.grdSNDetial.Rows[i].FindControl("labDATECODE")).Text);
                    var labcso = ((TextBox)this.grdSNDetial.Rows[i].FindControl("labcso")).Text;
                    if (!string.IsNullOrEmpty(labcso)) {
                        entity.cso = labcso;
                    }
                    var labVendorCode = ((TextBox)this.grdSNDetial.Rows[i].FindControl("labVendorCode")).Text;
                    if (!string.IsNullOrEmpty(labVendorCode))
                    {
                        entity.vendorcode = labVendorCode;
                    }
                    if (!string.IsNullOrEmpty(entity.sn_code)) {
                        entity.RULECODE = context.STOCK_CURRENT_SN.Where(x => x.sncode == entity.sn_code).FirstOrDefault().RULECODE;
                    }
                    //栈板栈板模式、箱-箱模式的时候，直接把sncode的值赋值给栈板号，箱-栈板模式的时候，直接""
                    entity.palletcode = SYSConfig == "1" ? entity.sn_code : "";
                    strpalletcode = entity.palletcode;
                    con.Insert(entity);
                }             
                con.Save();

                //暂存出库单补单，update 栈板号 20200409
                IGenericRepository<OUTBILL> outbillcon = new GenericRepository<OUTBILL>(context);
                OUTBILL outbill = (from p in outbillcon.Get().AsEnumerable()
                                   where p.id == this.txtID.Text
                                   select p).ToList<OUTBILL>().FirstOrDefault();
                if (outbill != null && !string.IsNullOrEmpty(outbill.IsTemporary) && outbill.IsTemporary == "1")
                {
                    outbill.palletcode = strpalletcode;
                    outbillcon.Update(outbill);
                    outbillcon.Save();
                }

                //暂存出库单补单，update 栈板号 20200409
                //返回
                this.AlertAndBack("FrmOUTBILL_DEdit.aspx?IDS=" + this.txtIDS.Text + "&" + BuildQueryString(SYSOperation.Modify, this.txtID.Text.Trim()), Resources.Lang.WMS_Common_Msg_SaveSuccess);//保存成功
            }
            catch (Exception ex)
            {
                Alert(Resources.Lang.WMS_Common_Msg_SaveFailed + "[" + ex.Message + "]");//保存失败
            }
        }
    }

    //增行
    protected void btnNew_Click(object sender, EventArgs e)
    {
        // SetSNEnable();
        DataTable table = GetGridViewData();
        DataRow newRow = table.NewRow();
        newRow["ID"] = Guid.NewGuid().ToString();
        table.Rows.Add(newRow);
        grdSNDetial.DataSource = table;
        grdSNDetial.DataBind();
    }

    //删除
    protected void btnDeleteSn_Click(object sender, EventArgs e)
    {
        //检查SN明细是否已经保存过
        if (OUTBILL_XDRule.CheckIsExistOutBill_D_SN(txtIDS.Text.Trim()))
        {
            #region 存在SN明细

            string msg = string.Empty;
            try
            {
                OUTBILL_XDRule list = new OUTBILL_XDRule();

                if (list.CheckStatus(txtID.Text.Trim(), "0") && list.isBD(txtID.Text.Trim()))
                {
                    var outbill_d_sn = new OUTBILL_XDRule();
                    //主键赋值
                    outbill_d_sn.DeleteSn(txtIDS.Text.Trim());	//执行动作 
                }
                else
                {
                    msg = Resources.Lang.FrmOUTBILL_DEdit_Tips_CannotDeleteSn; //"只有状态为未处理的出库补单可以删除SN!";
                }
                if (msg.Length == 0)
                {
                    msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;//删除成功！
                }
                this.GridBind();
            }
            catch (Exception ex)
            {
                msg += Resources.Lang.WMS_Common_Msg_DeleteFailed + "[" + ex.Message + "]!";//删除失败
            }
            this.Alert(msg);
            #endregion
        }
        else
        {
            #region 无保存明细

            DataTable table = GetGridViewData();

            for (int i = 0; i < grdSNDetial.Rows.Count; i++)
            {
                if (this.grdSNDetial.Rows[i].Cells[0].FindControl("chkSelect") is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSNDetial.Rows[i].Cells[0].FindControl("chkSelect"); //20130614093155
                    if (chkSelect.Checked)
                    {
                        foreach (DataRow dtRow in table.Rows)
                        {
                            if (dtRow["ID"].ToString() == this.grdSNDetial.DataKeys[i].Values[0].ToString())
                            {
                                table.Rows.Remove(dtRow);
                                break;
                            }
                        }
                    }
                }
            }

            grdSNDetial.DataSource = table;
            grdSNDetial.DataBind();

            #endregion
        }
    }

    //获取网格数据
    private DataTable GetGridViewData()
    {
        var dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID"));
        dt.Columns.Add(new DataColumn("SN_CODE"));
        dt.Columns.Add(new DataColumn("palletcode"));
        dt.Columns.Add(new DataColumn("QUANTITY"));
        dt.Columns.Add(new DataColumn("LINE_QTY"));
        dt.Columns.Add(new DataColumn("ALLQTY"));
        dt.Columns.Add(new DataColumn("SNTYPE"));
        dt.Columns.Add(new DataColumn("DATECODE"));
        dt.Columns.Add(new DataColumn("Furnaceno"));
        dt.Columns.Add(new DataColumn("IsSelected"));
        dt.Columns.Add(new DataColumn("cso"));
        dt.Columns.Add(new DataColumn("vendorcode"));

        string palletcode = string.Empty;
        var modOutBill_d = context.OUTBILL_D.Where(x => x.ids == txtIDS.Text).FirstOrDefault();
        if (modOutBill_d != null && !string.IsNullOrEmpty(modOutBill_d.cpositioncode)) {
            var modStock = context.STOCK_CURRENT.Where(x => x.cpositioncode == modOutBill_d.cpositioncode && x.cinvcode == modOutBill_d.cinvcode).FirstOrDefault();
            if (modStock != null) {
                palletcode = modStock.palletcode;
            }
        }

        for (int i = 0; i < grdSNDetial.Rows.Count; i++)
        {
            DataRow sourseRow = dt.NewRow();
            if (((CheckBox)this.grdSNDetial.Rows[i].Cells[0].FindControl("chkSelect")).Checked)
            {
                sourseRow["IsSelected"] = "1";
            }
            else
            {
                sourseRow["IsSelected"] = "0";
            }
            sourseRow["ID"] = this.grdSNDetial.DataKeys[i].Values[0].ToString();
            sourseRow["SN_CODE"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("lblSN")).Text;
            sourseRow["palletcode"] = palletcode;
            sourseRow["QUANTITY"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtquantity")).Text;
            sourseRow["LINE_QTY"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtLineQty")).Text;
            sourseRow["ALLQTY"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("lblAllQty")).Text;
            if (string.IsNullOrEmpty(sourseRow["SNTYPE"].ToString()) && !string.IsNullOrEmpty(sourseRow["SN_CODE"].ToString()))
            {
                if (ConFigvalue == "1")
                {
                    sourseRow["SNTYPE"] = "3";
                }
                else
                {
                    sourseRow["SNTYPE"] = this.GetSNType(sourseRow["SN_CODE"].ToString());
                }
            }
            sourseRow["DATECODE"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("labDATECODE")).Text;
            sourseRow["Furnaceno"] = ((Label)this.grdSNDetial.Rows[i].FindControl("labFurnaceno")).Text;
            sourseRow["cso"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("labcso")).Text;
            sourseRow["vendorcode"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("labVendorCode")).Text;
            dt.Rows.Add(sourseRow);
        }
        return dt;
    }

    //网格绑定
    protected void grdSNDetial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtSN = e.Row.FindControl("lblSN") as TextBox;
            TextBox txtQty = e.Row.FindControl("txtquantity") as TextBox;
            if (!string.IsNullOrEmpty(txtQty.Text.Trim())) {
                txtQty.Text = Convert.ToDecimal(txtQty.Text.Trim()).ToString("f2");
            }
            HiddenField hdv = e.Row.FindControl("hdnSelected") as HiddenField;
            CheckBox cbx = e.Row.Cells[0].FindControl("chkSelect") as CheckBox;
            if (hdv.Value.Trim() == "1")
            {
                cbx.Checked = true;
            }
            TextBox txtLineQty = e.Row.FindControl("txtLineQty") as TextBox;
            if (!string.IsNullOrEmpty(txtLineQty.Text.Trim()))
            {
                txtLineQty.Text = Convert.ToDecimal(txtLineQty.Text.Trim()).ToString("f2");
            }
            TextBox txtAllQty = e.Row.FindControl("lblAllQty") as TextBox;
            if (!string.IsNullOrEmpty(txtAllQty.Text.Trim()))
            {
                txtAllQty.Text = Convert.ToDecimal(txtAllQty.Text.Trim()).ToString("f2");
            }
            TextBox txtDateCode = e.Row.FindControl("labDATECODE") as TextBox;
            TextBox labcso = e.Row.FindControl("labcso") as TextBox;
            TextBox labVendorCode = e.Row.FindControl("labVendorCode") as TextBox;

            //txtAllQty.ReadOnly = true;
            if (HidField_Enable.Value == "1")
            {
            }
            else
            {
                SetSNEnable();
                if (ConFigvalue == "1")
                {
                    SNEnable = "1";
                }
            }

            //类型
            var labtype = e.Row.FindControl("labtype") as Label;
            //if (string.IsNullOrEmpty(labtype.Text) && !string.IsNullOrEmpty(txtSN.Text.Trim()))
            //{
            //    if (ConFigvalue == "1")
            //    {
            //        labtype.Text = "3";
            //    }
            //    else
            //    {
            //        labtype.Text = OUTBILL_XDRule.GetSNType(txtSN.Text.Trim());
            //    }
            //}

            ////labtype.Text = GetConfigSNName();
            //labtype.Text = "箱";
            labtype.Text = GetConfigSNName();
            //转换
            //if (labtype.Text.Trim() == "0")
            //{
            //    labtype.Text = "SN";
            //}
            //else if (labtype.Text.Trim() == "1")
            //{
            //    labtype.Text = "栈板";
            //}
            //else if (labtype.Text.Trim() == "2")
            //{
            //    labtype.Text = "箱";
            //}
            //else if (labtype.Text.Trim() == "3")
            //{
            //    labtype.Text = "批号";
            //}

            showSn.itype = "0";
            showSn.SetSearchCinvCode = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);

            HiddenField hdnquantity = e.Row.FindControl("hdnquantity") as HiddenField;

            CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;
            HiddenField hdnSelected = e.Row.FindControl("hdnSelected") as HiddenField;
            chkSelect.Attributes["onblur"] = "SetHinddenValue('" + hdnSelected.ClientID + "','" + chkSelect.ClientID + "') ";
            if (this.OutBillStatus == "0")
            {
                if (this.WorkType == "0")
                {
                    txtSN.Attributes["onclick"] = "ShowNew('" + showSn.GetDivName + "','" + txtSN.ClientID + "','" + txtQty.ClientID + "','" + txtAllQty.ClientID + "','" + labtype.ClientID + "','','" + txtDateCode.ClientID + "','" + labcso.ClientID + "','" + labVendorCode.ClientID + "','" + e.Row.RowIndex.ToString() + "');";
                }
                txtLineQty.Attributes["onblur"] = "SetQtyValue('" + txtQty.ClientID + "','" + txtLineQty.ClientID + "','" + txtAllQty.ClientID + "');";
                //txtQty.Attributes["onblur"] = "SetQtyRead('" + txtQty.ClientID + "','" + txtLineQty.ClientID + "','" + txtAllQty.ClientID + "');";
                //txtDateCode.Attributes["onblur"] = "SetQtyRead('" + txtQty.ClientID + "','" + txtLineQty.ClientID + "','" + txtAllQty.ClientID + "');";

                if (SNEnable == "1")
                {
                    txtAllQty.Attributes["onblur"] = "SetAllQtyValue('" + txtQty.ClientID + "','" + txtLineQty.ClientID + "','" + txtAllQty.ClientID + "','" + txtSN.ClientID + "');";
                }
            }
        }
    }
    #endregion

    protected void ddl_Line_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddl_Line_ID.SelectedValue))
        {
            Help.DropDownListDataBind(GetOutPallet(ddl_Line_ID.SelectedValue, OutType, this.BillNo), this.ddl_Pallet_Code, Resources.Lang.FrmOUTBILL_DEdit_Tips_ZhanDianSelect, "FUNCNAME", "EXTEND1", "");
        }
    }

    /// <summary>
    /// Note by Qamar 2020-11-22
    /// 嘗試生成本出庫單其他明細的SN
    /// 並將本明細的 cpositioncode cposition cinvbarcode cmemo cdefine1 cdefine2 pallet_code isgoback wire 複製至其他明細
    /// 目的是複製 線別 站口 儲位
    /// </summary>
    private void Save_Others_OUTBILL_D_and_SN(OUTBILL_D outbill_d)
    {
        IGenericRepository<OUTBILL_D_SN> con_sn = new GenericRepository<OUTBILL_D_SN>(context);
        //找出本明細的SN
        var outbill_d_sn = (from p in con_sn.Get() where p.outbill_d_ids == outbill_d.ids select p).ToList().FirstOrDefault<OUTBILL_D_SN>();

        IGenericRepository<view_stock_current> con_stock = new GenericRepository<view_stock_current>(context);
        //本明細指定儲位的所有庫存
        var stockcurrent = (from p in con_stock.Get() where p.cpositioncode == outbill_d.cpositioncode select p).ToList();

        //IGenericRepository<v_outbill_detail> con_d = new GenericRepository<v_outbill_detail>(context);
        IGenericRepository<OUTBILL_D> con_d = new GenericRepository<OUTBILL_D>(context);
        //本出庫單的其他明細,不包含本明細
        var outbilldetail = (from p in con_d.Get() where p.id == outbill_d.id && p.ids != outbill_d.ids select p).ToList();

        foreach (var entity in outbilldetail)
        {
            IGenericRepository<OUTBILL_D_SN> con = new GenericRepository<OUTBILL_D_SN>(context);
            //如果該明細enitiy尚未生成SN
            if ((from p in con.Get() where p.outbill_d_ids == entity.id select p).ToList().Count() == 0)
            {
                //如果該stockcurrent有足夠的此物料
                if (stockcurrent.Where(x => x.cinvcode == entity.cinvcode && x.iqty > entity.iquantity).ToList().Count() > 0)
                {
                    /* 為該明細entity生成SN entity_sn */
                    OUTBILL_D_SN entity_sn = new OUTBILL_D_SN();
                    entity_sn.id = Guid.NewGuid().ToString();
                    entity_sn.outbill_id = outbill_d_sn.outbill_id;
                    entity_sn.outbill_d_ids = entity.ids;
                    entity_sn.sn_code = outbill_d_sn.sn_code;
                    entity_sn.datecode = outbill_d_sn.datecode;
                    entity_sn.cinvcode = entity.cinvcode;
                    entity_sn.quantity = entity.iquantity;
                    entity_sn.worktype = outbill_d_sn.worktype;
                    entity_sn.createtime = outbill_d_sn.createtime;
                    entity_sn.createowner = outbill_d_sn.createowner;
                    entity_sn.lastvpdtime = outbill_d_sn.lastvpdtime;
                    entity_sn.lastvpdowner = outbill_d_sn.lastvpdowner;
                    entity_sn.scan_ids = outbill_d_sn.scan_ids;
                    entity_sn.line_qty = outbill_d_sn.line_qty;
                    entity_sn.sntype = outbill_d_sn.sntype;
                    entity_sn.furnaceno = outbill_d_sn.furnaceno;
                    entity_sn.outbilltype = outbill_d_sn.outbilltype;
                    entity_sn.palletcode = outbill_d_sn.palletcode;
                    entity_sn.cso = outbill_d_sn.cso;
                    entity_sn.RULECODE = outbill_d_sn.RULECODE;
                    con_sn.Insert(entity_sn);
                    con_sn.Save();

                    /* 更新該明細entity */
                    entity.cpositioncode = outbill_d.cpositioncode;
                    entity.cposition = outbill_d.cposition;
                    entity.cinvbarcode = outbill_d.cinvbarcode;
                    entity.cmemo = "";
                    entity.cdefine1 = outbill_d.cdefine1;
                    entity.cdefine2 = outbill_d.cdefine2;
                    entity.pallet_code = outbill_d.pallet_code;
                    entity.isgoback = outbill_d.isgoback;
                    entity.wire = outbill_d.wire;
                    con_d.Update(entity);
                    con_d.Save();
                }
            }
        }
    }

    /// <summary>
    /// 补单,保存明细后，自动生成SN
    /// </summary>
    private string SaveSN()
    {
        string saveMsg = string.Empty;
        string ids = txtIDS.Text.Trim();
        using (var modContext = context) {
            //删除原有的
            string sql = string.Format(@" delete from outbill_d_sn where OUTBILL_D_ids='{0}' ", ids);
            modContext.Database.ExecuteSqlCommand(sql);
            List<STOCK_CURRENT_SN> snList = null;
            var modOutBill = modContext.OUTBILL.Where(x => x.id == txtID.Text.Trim()).FirstOrDefault();
            var modOutType = modContext.OUTTYPE.Where(x => x.cerpcode == modOutBill.otype.ToString()).FirstOrDefault();
            //Note by Qamar 2020-11-27
            string CINVCODE = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
            if (modOutType.IsMatchSo == "1")
            {
                snList = modContext.STOCK_CURRENT_SN.Where(x => modContext.STOCK_CURRENT.Any(y => y.id == x.stock_id && y.cinvcode.Equals(CINVCODE) && y.cpositioncode.Equals(txtCPOSITIONCODE.Text.Trim())) && x.ERPCODE.ToString() == CSO).ToList();
            }
            else {
                snList = modContext.STOCK_CURRENT_SN.Where(x => modContext.STOCK_CURRENT.Any(y => y.id == x.stock_id && y.cinvcode.Equals(CINVCODE) && y.cpositioncode.Equals(txtCPOSITIONCODE.Text.Trim()))).ToList();
            }
            //匹配供应商
            if (modOutType.IsMatchVendor == "1") {
                if (snList != null && snList.Any()) {
                    if (!string.IsNullOrEmpty(this.VendorCode))
                    {
                        snList = snList.Where(x => x.VENDORCODE != null && x.VENDORCODE.ToString() == this.VendorCode).ToList();
                    }
                    else {
                        snList = snList.Where(x => x.VENDORCODE == null || x.VENDORCODE =="").ToList();
                    }
                }
            }

            if (snList != null && snList.Any())
            {
                snList = snList.OrderBy(x => x.datecode).ToList();
                decimal needQuantity = Convert.ToDecimal(txtIQUANTITY.Text.Trim());//需要的数量                
                foreach (var item in snList)
                {
                    //剩余需要数量为0时直接退出
                    if (needQuantity == 0)
                    {
                        break;
                    }
                    decimal currentQty = 0;//当前sn需扣数量
                    if (item.qty > Convert.ToDecimal(needQuantity))
                    {
                        currentQty = needQuantity;
                    }
                    else
                    {
                        currentQty = item.qty.HasValue ? Convert.ToDecimal(item.qty.Value) : 0;
                    }
                    needQuantity = needQuantity - currentQty;

                    OUTBILL_D_SN snObj = new OUTBILL_D_SN();
                    snObj.id = Guid.NewGuid().ToString();
                    snObj.outbill_id = txtID.Text.Trim();
                    snObj.outbill_d_ids = txtIDS.Text.Trim();
                    snObj.sn_code = item.sncode;
                    snObj.datecode = item.datecode;
                    snObj.cinvcode = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
                    snObj.quantity = Convert.ToDecimal(currentQty);
                    snObj.worktype = modOutBill.worktype;
                    snObj.createtime = DateTime.Now;
                    snObj.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    snObj.sntype = 0;
                    snObj.line_qty = 0;
                    snObj.palletcode = item.palletcode;
                    snObj.cso = item.ERPCODE;
                    snObj.vendorcode = item.VENDORCODE;
                    snObj.RULECODE = item.RULECODE;                  
                    modContext.OUTBILL_D_SN.Add(snObj);
                    modContext.SaveChanges();                  
                }              
            }
            else {
                saveMsg = Resources.Lang.FrmOUTBILL_DEdit_Tips_NoSn; //"但没有找到匹配的SN!";
            }
        };
        return saveMsg;
    }

    public string GetSNType(string carton)
    {
        var modEntity = context.view_get_palorcar_type.Where(x => x.SNNo == carton).FirstOrDefault();
        if (modEntity != null)
        {
            return modEntity.SNType.ToString();
        }
        else {
            return "0";
        }
    }

    //Note by Qamar 2020-11-26
    private string GetCINVCODE_from_CINVCODE_and_RANK_FINAL(string cinvcodewithoutrank, string rankfinal)
    {
        string CINVCODE = cinvcodewithoutrank.Trim();
        if (rankfinal == "")
            CINVCODE += "-_";
        else
            CINVCODE += "-" + rankfinal.Trim().ToUpper();
        return CINVCODE;
    }
}