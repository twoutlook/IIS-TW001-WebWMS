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
using System.Text.RegularExpressions;


/// <summary>
/// 描述: 入库管理-->FrmINBILL_DEdit 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 19:00:49
/// </summary>
public partial class RD_FrmINBILL_DEdit : WMSBasePage
{
    #region 页面属性
    /// <summary>
    /// 是否暂存单据
    /// </summary>
    public string IsTemporary
    {
        get { return this.hiddIsTemporary.Value; }
        set { this.hiddIsTemporary.Value = value; }
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        ShowPart_Asn_IDS_Div1.SetCINVCODE = txtCINVCODE.ClientID;
        ShowPart_Asn_IDS_Div1.SetCINVNAME = txtCINVNAME.ClientID;
        ShowPart_Asn_IDS_Div1.SetERPLine = txtCERPCODELINE.ClientID;
        ShowPart_Asn_IDS_Div1.SetIQTY = txtIQUANTITY.ClientID;
        ShowPart_Asn_IDS_Div1.SetAsn_D_IDS = txtAsn_D_IDS.ClientID;
        //ShowPart_Asn_IDS_Div1.SetCSPEC = txtcspecifications.ClientID;

        ShowBASE_CARGOSPACEDiv1.SetCompName = txtCPOSITION.ClientID;
        ShowBASE_CARGOSPACEDiv1.SetORGCode = txtCPOSITIONCODE.ClientID;
        //ShowBASE_CARGOSPACEDiv1.CinvCode = txtCINVCODE.Text;
        //Note by Qamar 2020-11-26
        ShowBASE_CARGOSPACEDiv1.CinvCode = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
        //ShowPart_Asn_IDS_Div1.SetCSPEC = txtcspecifications.ClientID;
        txtCPOSITION.Enabled = false;

        if (this.IsPostBack == false)
        {
            this.InitPage();
           
            if (this.Operation() != SYSOperation.New)
            {
                ShowData();
                //修改时只能修改数量
                this.txtCINVCODE.Enabled = false;
                this.txtRANK_FINAL.Enabled = false;
                //this.txtCINVNAME.Enabled = false;
                TabMain0.Visible = true;
                btnSearch_Click(null, null);
                //txtIQUANTITY.Enabled = true;
                hfInType.Value = Request.QueryString["InType"] == null ? hfInType.Value : hfInType.Value = Request.QueryString["InType"].ToString().Trim();
            }
            else
            {
                string ID = Request.QueryString["ID"];
                LoadIDS(ID);
                TabMain0.Visible = false;

                txtLineId.Text = (GetMaxLineId(ID) + 1).ToString();
                hfInType.Value = Request.QueryString["InType"].ToString().Trim();
            }
            hfInasn_id.Value = Request.QueryString["Inasn_id"];
          
            hfTradeCode.Value = Request.QueryString["TradeCode"];
            hfCurrency.Value = Request.QueryString["Currency"];
            if (Request.QueryString["CreateType"] != null)
            {
                hdnCreateType.Value = Request.QueryString["CreateType"];
            }            
            Session["inAsn_id"] = hfInasn_id.Value;
            RegisterClientScript();
            if (this.IsTemporary == "1")
            {
                ShowBASE_CARGOSPACEDiv1.DrpIsAll = "0";
            }
        }
        //查询参数
        ShowPart_Asn_IDS_Div1.SearchAsnID = hfInasn_id.Value;
        ShowPart_Asn_IDS_Div1.SearchType = "IN";
        ShowPart_Asn_IDS_Div1.SearchBillID = txtID.Text.Trim();
        ShowBASE_CARGOSPACEDiv1.workType = this.hdnWorkType.Text;
        ShowBASE_CARGOSPACEDiv1.IsTemporary = this.IsTemporary;

        showCartonSn.cinvcode = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
        if (CINVNAME.Length == 0)
        {
            IGenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
            /*
            var caseList = from p in con.Get()
                           where p.cpartnumber == txtCINVCODE.Text.Trim()
                           select p;
            */
            //Note by Qamar 2020-10-28
            string CINVCODE = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
            var caseList = from p in con.Get()
                           where p.cpartnumber == CINVCODE
                           select p;

            if (caseList.Count() > 0)
            {
                CINVNAME = caseList.ToList().FirstOrDefault<BASE_PART>().cpartname;
            }

        }
        else
        {
            //显示物料规格
            //Note by Qamar 2020-11-26
            string CINVCODE = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
            GenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
            var caseList1 = from p in con.Get()
                            where p.cpartnumber == CINVCODE
                            select p;
            if (caseList1.Count() > 0)
            {
                txtcspecifications.Text = caseList1.ToList().FirstOrDefault<BASE_PART>().cspecifications;
            }
        }        
        this.txtCINVNAME.Text = CINVNAME;
        IGenericRepository<INBILL> conInBill = new GenericRepository<INBILL>(context);
        var caseListBill = from p in conInBill.Get()
                           where p.id == txtID.Text.Trim()
                           select p;
        if (caseListBill.Count() > 0)
            txtInbillCode.Text = caseListBill.ToList().FirstOrDefault<INBILL>().cticketcode;
        RegisterClientScriptAsrs();
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        btnSavesn.Attributes.Add("onclick", this.GetPostBackEventReference(btnSavesn) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中

        //Note by Qamar 2020-11-11
        ddlWire.SelectedIndex = 1; //線別
        ddlWire.SelectedValue = "1";
        string wireid = !string.IsNullOrEmpty(ddlWire.SelectedValue) ? ddlWire.SelectedValue.ToString() : string.Empty;
        Help.DropDownListDataBind(new SysParameterList().GetSite(wireid, hfInType.Value.Trim()), this.ddl_Pallet_Code, Resources.Lang.FrmINBILL_DEdit_MSG31, "sitename", "siteid", "");
        ddl_Pallet_Code.SelectedIndex = 1; //站點
        ddlWire.Enabled = false;
        //ddl_Pallet_Code.Enabled = false;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    //protected override void Render(HtmlTextWriter writer)
    {
        //Note by Qamar 2020-11-11
        //ddlWire.SelectedValue = "1";
        //ddlWire.SelectedIndex = 1;
        //ddlWire_SelectedIndexChanged(ddlWire, null);
    }

    protected void btnNew_Click_1(object sender, EventArgs e)
    {
        this.GridBind();
        //  入庫單明細SN
        this.WriteScript("PopupFloatWin('" +
                         BuildRequestPageURL("FrmINBILL_D_SN.aspx", SYSOperation.New, txtID.Text.Trim()) +
                         "&INBILL_D_IDS=" + txtIDS.Text + "','" + Resources.Lang.FrmINBILL_DEdit_MSG28 + "','INBILL_D_SN',800,600);");
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
                                            url: ""../Server/Part.ashx?partNumber="" + request.term + ""&Asn_id=" + hfInasn_id.Value.Trim() + @"&Asn_type=INASN_D"",
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


    /// <summary>
    /// 2015-12-28 向页面注册ASRS脚本
    /// </summary>
    private void RegisterClientScriptAsrs()
    {
        if (ASRSFig == "1")
        {
            //2015-11-30 传递的参数要有数量Sum   [ASRS专用]
            string script = string.Format(@"<script type=""text/javascript"">
                                                     $(function () {{
                                                         $(""#{0}"").autocomplete({{
                                                             source: function (request, response) {{
                                                                 //alert(request.term);
                                                                 //alert($(this).attr(""CINVCODE""));
                                                                 $.ajax({{
                                                                     url: ""../Server/Cargospan.ashx?PositionCode="" + request.term + ""&CinvCode={1}&Type=In&Sum={3}&ASRSFig=1"",
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
                                                                  $.get(""../BASE/SubmitDate.aspx"",
                                                                        {{ Type: 'In', DataType: 'PositionCode', Ids: '" + txtIDS.Text.Trim() + @"', Qty: $(""#{2}"").val(), Line_Qty: 0, PositionCode: ui.item.value }},
                                                                        function (data) {{
                                                                            $(""#ctl00_ContentPlaceHolderMain_showMsgTd"").html(data);
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
                           txtCINVCODE.Text.Trim(), txtIQUANTITY.ClientID, txtIQUANTITY.Text.Trim());
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), txtCPOSITIONCODE.ClientID, script);
            //当储位默认为空时，绑定符合条件的储位
            if (txtCPOSITIONCODE.Text == "")
            {
                //BUCKINGHA-175
                //2015-11-30 储位编码默认为库余量符合入库单最小数量的储位   [ASRS专用]
                //DataTable pdat = null;
                ////2015-12-02 获得符合条件的第一个储位编号
                //pdat = new Base_Cargospace().GetCargoSpaceListByBaseData(txtCINVCODE.Text.Trim(), "", true, 15, txtIQUANTITY.Text.Trim());
                //if (pdat.Rows.Count > 0)
                //{
                //    txtCPOSITIONCODE.Text = pdat.Rows[0]["cpositioncode"].ToString();
                //}
            }
        }
    }


    public void LoadIDS(string ID)
    {
        this.txtID.Text = ID;
        this.txtID.Enabled = false;
        this.txtIDS.Text = Guid.NewGuid().ToString();
        this.txtIDS.Enabled = false;
        this.txtDINDATE.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        txtCINPERSONCODE.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INBILL_D');return false;";
        //请选择线别
        Help.DropDownListDataBind(new SysParameterList().GetCrane(), this.ddlWire, Resources.Lang.FrmINBILL_DEdit_MSG29, "cranename", "CRANEID", "");

        //ddl_InBillType 物料类型
        Help.DropDownListDataBind(GetParametersByFlagType("MATERIALTYPE"), this.ddl_InBillType, Resources.Lang.FrmINBILL_DEdit_MSG15, "FLAG_NAME", "FLAG_ID", "1"); //请选择
       // this.ddlWire_SelectedIndexChanged(null, null);
        //ConFigvalue = this.GetConFig("000002");
        //if (ConFigvalue == "1")
        //{
        //    btnNew.Text = "批号";
        //    btnSavesn.Text = "保存批号";
        //    btnCarton.Visible = false;
        //    grdSNDetial.Columns[3].HeaderText = "批号";
        //}
        //else if (ConFigvalue == "2")
        //{
        //    btnNew.Text = "箱号";
        //    btnSavesn.Text = "保存箱号";
        //    btnCarton.Visible = false;
        //    grdSNDetial.Columns[3].HeaderText = "箱号";
        //}
        //else
        //{
        //    btnNew.Text = "SN";
        //    btnSavesn.Text = "保存SN";
        //}
        //删除确认提示
        if (this.Operation() == SYSOperation.Modify)
        {
            //要删除
            this.btnDelete.Attributes["onclick"] = "var userNo = '" + this.KeyID + "'; return window.confirm('" + Resources.Lang.FrmINBILL_DEdit_MSG30 + "' + userNo + '?');";
        }
        else
        {
            this.btnDelete.Visible = false;
        }
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.Operation() == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.FrmINBILLEdit_MSG42; //"审批";
        }

        var workType = string.Empty;
        IGenericRepository<INBILL> con = new GenericRepository<INBILL>(context);
        var caseList = from p in con.Get()
                       where p.id == this.KeyID.Trim()
                       select p;

        INBILL entity = caseList.ToList().FirstOrDefault<INBILL>();
        this.hdnWorkType.Text = entity.worktype;
        if (entity.worktype != null)
        {
            workType = entity.worktype;
            IsLKorPC(workType);

        }        
        //根据配置值动态显示button的值
        this.btnNew.Text = CurrentConfigUnitName;
        this.btnSavesn.Text = Resources.Lang.Common_btnSave + CurrentConfigUnitName;
        if (SYSConfig == "1")//箱模式
        {
            grdSNDetial.Columns[7].Visible = false;
        }
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly

    }

    #endregion

    private void IsLKorPC(string workType)
    {
        if (workType == "1")
        {
            this.tr1.Visible = true;
        }
        else
        {
            this.tr1.Visible = false;
        }
    }

    public string P_Status
    {
        set { ViewState["P_Status"] = value; }
        get
        {
            if (ViewState["P_Status"] != null)
            {
                return ViewState["P_Status"].ToString();
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
    /// 出库通知明细的IDS
    /// </summary>
    public string Asn_D_IDS
    {
        get
        {
            if (ViewState["Asn_D_IDS"] != null)
            {
                return ViewState["Asn_D_IDS"].ToString();
            }
            return "";
        }
        set { ViewState["Asn_D_IDS"] = value; }
    }

    /// <summary>
    /// 获取最大的项次编号
    /// </summary>
    /// <param name="asnId"></param>
    /// <returns></returns>
    private int GetMaxLineId(string id)
    {
        int lineId = 0;
        IGenericRepository<INBILL_D> conn = new GenericRepository<INBILL_D>(db);
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
    /// 线别级联站点数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlWire_SelectedIndexChanged(object sender, EventArgs e)
    {
        string wireid = !string.IsNullOrEmpty(ddlWire.SelectedValue)?ddlWire.SelectedValue.ToString():string.Empty;
        //请选择站点
        Help.DropDownListDataBind(new SysParameterList().GetSite(wireid, hfInType.Value.Trim()), this.ddl_Pallet_Code, Resources.Lang.FrmINBILL_DEdit_MSG31 , "sitename", "siteid", "");
        this.ddl_Pallet_Code.Enabled = true;
    }

    /// <summary>
    /// Note by Qamar 2020-11-23
    /// 將本明細的 cpositioncode cposition iasnline pallet_code inbilltype wire 複製至其他明細
    /// 目的是複製 線別 站口 儲位
    /// 這樣的做法, 入庫單的所有明細都會是同一儲位.
    /// </summary>
    private void Save_Others_INBILL_D(INBILL_D inbill_d)
    {
        IGenericRepository<INBILL_D> con_d = new GenericRepository<INBILL_D>(context);
        //本入庫單的其他明細,不包含本明細
        var inbilldetail = (from p in con_d.Get() where p.id == inbill_d.id && p.ids != inbill_d.ids select p).ToList();

        foreach (var entity in inbilldetail)
        {
            /* 更新該明細entity */
            entity.cpositioncode = inbill_d.cpositioncode;
            entity.cposition = inbill_d.cposition;
            entity.iasnline = inbill_d.iasnline;
            entity.pallet_code = inbill_d.pallet_code;
            entity.inbilltype = inbill_d.inbilltype;
            entity.wire = inbill_d.wire;
            con_d.Update(entity);
            con_d.Save();
        }
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        string ids = Request.QueryString["IDS"].ToString();
        IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
        var caseList = from p in con.Get()
                       where p.ids == ids
                       select p;
        INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
        txtLineId.Text = entity.LineID.HasValue ? entity.LineID.Value.ToString() : "";
        txtAsndLineID.Text = entity.AsndLineID.HasValue ? entity.AsndLineID.Value.ToString() : "";

        Asn_D_IDS = entity.asn_d_ids;

        this.txtIDS.Text = entity.ids.ToString();
        this.txtID.Text = entity.id.ToString();
        this.txtAsn_D_IDS.Text = entity.asn_d_ids;
        HidField_DateCode.Value = InBill.GetAsn_DateCode(txtAsn_D_IDS.Text);
        this.txtCSTATUS.Text = entity.cstatus;
        //this.txtCINVCODE.Text = entity.cinvcode;
        this.txtCINVNAME.Text = entity.cinvname;
        //lblCINV_NAME.Text = entity.CINVNAME;
        //Note by Qamar 2020-11-26
        this.txtCINVCODE.Text = entity.cinvcode.Substring(0, entity.cinvcode.Length - 2);
        this.txtRANK_FINAL.Text = entity.cinvcode.Substring(entity.cinvcode.Length - 1, 1);
        if (txtRANK_FINAL.Text.Trim() == "_")
            txtRANK_FINAL.Text = "";
        //string partnumber = entity.cinvcode;

        this.txtIQUANTITY.Text = Convert.ToDecimal(entity.iquantity).ToString("f2");
        this.txtCINVBARCODE.Text = entity.cinvbarcode;
        this.txtCERPCODELINE.Text = entity.cerpcodeline;
        this.txtCMEMO.Text = entity.cmemo;
        this.txtCPOSITIONCODE.Text = entity.cpositioncode;
        this.txtCPOSITION.Text = entity.cposition;
        this.txtDINDATE.Text = Convert.ToDateTime(entity.dindate).ToString("yyyy-MM-dd HH:mm:ss");
        this.txtCINPERSONCODE.Text = entity.cinpersoncode;
        this.txtIASNLINE.Text = entity.iasnline.HasValue ? entity.iasnline.ToString() : "";
        CINVNAME = entity.cinvname;
        hfOriginalQty.Value = entity.iquantity.ToString();
       
        
        this.txtPartNo.Text =string.IsNullOrEmpty(entity.partno)? Base_Part.GetPRODUCTCODEByCpartNumber(entity.cinvcode):entity.partno;

        //非补单则不允许有增加
        if (!InBill.IsBD(entity.ids))
        {
            btnNew.Enabled = false;
            btnDeleteSn.Enabled = false;
            btnCarton.Enabled = false;
            btnSavesn.Enabled = false;
        }

        //Roger 2013/11/28 14:02:35 SN整合
        IGenericRepository<INBILL> conInbill = new GenericRepository<INBILL>(context);
        INBILL INBILLentity = conInbill.Get().Where(x => x.id == entity.id).FirstOrDefault();
        this.IsTemporary = INBILLentity.IsTemporary;
        P_Status = entity.cstatus;
        hdnCreateType.Value = INBILLentity.creatertype; //20200624 修复BUG XL-145 【补单入库】补单生成入库单选择储位保存后，再重新换成其他储位保存时，提示明细不一致
        var IsExistTempInBill_D = InBill.ValidateIsExistTemp_InBill_D(INBILLentity.cticketcode);
        if ((P_Status.Length > 0 && P_Status.Equals("0")) && !IsExistTempInBill_D)
        {
        }
        else
        {
            txtCINVCODE.Enabled = false;
            txtRANK_FINAL.Enabled = false;
            txtCINVNAME.Enabled = false;
            txtIQUANTITY.Enabled = false;
            txtIASNLINE.Enabled = false;
            txtCINVBARCODE.Enabled = false;
            txtCERPCODELINE.Enabled = false;
            txtCPOSITIONCODE.Enabled = false;
            txtCPOSITION.Enabled = false;
            txtCINPERSONCODE.Enabled = false;
            txtDINDATE.Enabled = false;
            txtCSTATUS.Enabled = false;
            txtCMEMO.Enabled = false;
            btnSave.Enabled = false;

            btnNew.Enabled = false;
            btnDeleteSn.Enabled = false;
            btnCarton.Enabled = false;
            btnSavesn.Enabled = false;
        }


        INTYPE INBILL_type = GetINTYPEByID(this.KeyID, "INBILL");
        if (INBILL_type.Is_Query == 1)
        {
            txtCINVCODE.Enabled = false;
            txtRANK_FINAL.Enabled = false;
            txtCINVNAME.Enabled = false;
            txtIQUANTITY.Enabled = false;
            txtIASNLINE.Enabled = false;
            txtCINVBARCODE.Enabled = false;
            txtCERPCODELINE.Enabled = false;
            txtCPOSITIONCODE.Enabled = false;
            txtCPOSITION.Enabled = false;
            txtCINPERSONCODE.Enabled = false;
            txtDINDATE.Enabled = false;
            txtCSTATUS.Enabled = false;
            txtCMEMO.Enabled = false;
            btnSave.Enabled = false;

            btnNew.Enabled = false;
            btnDeleteSn.Enabled = false;
            btnCarton.Enabled = false;
            btnSavesn.Enabled = false;
        }
        //特殊wip return
        var isSpecialReturn = InAsn.isSpecialWipReturn(INBILLentity.casnid);
        if (isSpecialReturn)
        {
            txtCINVCODE.Enabled = false;
            txtRANK_FINAL.Enabled = false;
            txtCINVNAME.Enabled = false;
            txtIQUANTITY.Enabled = false;
            txtIASNLINE.Enabled = false;
            txtCINVBARCODE.Enabled = false;
            txtCERPCODELINE.Enabled = false;
            txtCPOSITIONCODE.Enabled = false;
            txtCPOSITION.Enabled = false;
            txtCINPERSONCODE.Enabled = false;
            txtDINDATE.Enabled = false;
            txtCSTATUS.Enabled = false;
            txtCMEMO.Enabled = false;
            btnSave.Enabled = false;

            btnNew.Enabled = false;
            btnDeleteSn.Enabled = false;
            btnCarton.Enabled = false;
            btnSavesn.Enabled = false;
        }

        if (entity.asrs_status != null && entity.asrs_status.Value > 0)
        {
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnNew.Enabled = false;
            btnDeleteSn.Enabled = false;
            btnSavesn.Enabled = false;
        }
        this.hfWorkType.Value = INBILLentity.worktype;//工作模式
        
        //是否显示ASRS 1显示 0 不显示
        ASRSFig = this.GetConFig("000006");

        this.ddlWire.SelectedValue = entity.wire;
        this.ddlWire_SelectedIndexChanged(null, null);

        this.ddl_Pallet_Code.SelectedValue = entity.wire + "-" + entity.pallet_code;
        this.ddl_InBillType.SelectedValue = entity.inbilltype.IsNullOrEmpty() == true ? "1" : entity.inbilltype;

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
        string msg = string.Empty;
        string workType = "0";
        if (this.Operation() == SYSOperation.New)
        {
            workType = "0";
        }
        else
        {
            workType = "1";
        }

        if (this.txtIDS.Text.Trim() == "")
        {
            //子表编号项不允许空
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG32+ "！");
            this.SetFocus(txtIDS);
            return false;
        }
        //
        if (this.txtID.Text.Trim() == "")
        {
            //主表编号项不允许空
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG33 + "！");
            this.SetFocus(txtID);
            return false;
        }


        //检查入库单状态
        if (!(new InBill().CheckStatus(txtID.Text.Trim(), "0")))
        {
            //入库单状态不是未处理，不能保存
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG34 + "！");
            return false;
        }
        //
        if (this.txtCSTATUS.Text.Trim() == "")
        {
            //状态项不允许空
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG35 + "！");
            this.SetFocus(txtCSTATUS);
            return false;
        }
        //
        if (this.txtCSTATUS.Text.Trim().Length > 0)
        {
            if (this.txtCSTATUS.Text.GetLengthByByte() > 20)
            {
                //状态项超过指定的长度20
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG36 + "！");
                this.SetFocus(txtCSTATUS);
                return false;
            }
        }
        //
        if (this.txtCINVCODE.Text.Trim() == "")
        {
            //料号项不允许空
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG37 + "！");
            this.SetFocus(txtCINVCODE);
            return false;
        }
        //
        if (this.txtCINVCODE.Text.Trim().Length > 0)
        {
            if (this.txtCINVCODE.Text.GetLengthByByte() > 50)
            {
                //料号项超过指定的长度50
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG38 + "！");
                this.SetFocus(txtCINVCODE);
                return false;
            }
        }
        if (this.txtCINVBARCODE.Text.Trim().Length > 0)
        {
            if (this.txtCINVBARCODE.Text.GetLengthByByte() > 30)
            {
                //物料条码项超过指定的长度30
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG39 + "！");
                this.SetFocus(txtCINVBARCODE);
                return false;
            }
        }
        //
        if (this.txtCERPCODELINE.Text.Trim().Length > 0)
        {
            if (this.txtCERPCODELINE.Text.GetLengthByByte() > 20)
            {
                //ERP单号项次项超过指定的长度20
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG40 + "！");
                this.SetFocus(txtCERPCODELINE);
                return false;
            }
        }
        //
        if (this.txtDINDATE.Text.Trim() == "")
        {
            //入库项不允许空
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG41 + "！");
            this.SetFocus(txtDINDATE);
            return false;
        }
        //
        if (this.txtDINDATE.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate(this.txtDINDATE.Text) == false)
            {
                //入库项不是有效的日期
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG42 + "！");
                this.SetFocus(txtDINDATE);
                return false;
            }
        }
        //
        if (this.txtCINPERSONCODE.Text.Trim() == "")
        {
            //入库项不允许空
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG43 + "！");
            this.SetFocus(txtCINPERSONCODE);
            return false;
        }
        //
        if (this.txtCINPERSONCODE.Text.Trim().Length > 0)
        {
            if (this.txtCINPERSONCODE.Text.GetLengthByByte() > 20)
            {
                //入库项超过指定的长度20
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG44 + "！");
                this.SetFocus(txtCINPERSONCODE);
                return false;
            }
        }
        //
        if (this.txtIASNLINE.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDecimal(this.txtIASNLINE.Text) == false)
            {
                //入库通知单项次项不是有效的十进制数字
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG45 + "！");
                this.SetFocus(txtIASNLINE);
                return false;
            }
        }
        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 20)
            {
                //备注项超过指定的长度20
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG46 + "！");
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        //
        //if (this.txtCINVNAME.Text.Trim() == "")
        //{
        //    this.Alert("品名项不允许空！");
        //    this.SetFocus(txtCINVNAME);
        //    return false;
        //}
        //
        if (this.txtIQUANTITY.Text.Trim() == "")
        {
            //数量项不允许空
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG47 + "！");
            this.SetFocus(txtIQUANTITY);
            return false;
        }
        //检查数量，不允许小数，负数，0 CQ 2014-2-13 13:39:48
        if (!(Comm_Fun.Fun_IsDecimal(txtIQUANTITY.Text, 0, 0, 1, out msg)))
        {
            this.Alert(msg);
            this.SetFocus(txtIQUANTITY);
            return false;
        }
        

        if (this.Operation() == SYSOperation.New)
        {
            //判断是否存在该子项ERpCode的明细
            if (InBill.CheckInBillIs_ExistAsnIDS(txtAsn_D_IDS.Text.Trim(), txtID.Text.Trim()))
            {
                //子项ERP单号,已经存在入库单中不能重复添加
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG48 + "[" + txtCERPCODELINE.Text + Resources.Lang.FrmINBILL_DEdit_MSG49 + "]!");
                this.SetFocus(txtIQUANTITY);
                return false;
            }
        }
        else
        {
            if (this.txtAsn_D_IDS.Text.Trim() == "")
            {
                //入库通知单明细IDS项不允许空
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG50 + "！");
                this.SetFocus(txtAsn_D_IDS);
                return false;
            }

        }

        #region 测试代码
        //判断是否大于明细数量
        if (hfGetQty.Value.Trim() != "0" && Convert.ToDecimal(this.txtIQUANTITY.Text.Trim()) > Convert.ToDecimal(hfGetQty.Value.Trim()))
        {
            //数量不能大于
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG51 + "[" + hfGetQty.Value.Trim() + "]！");
            this.SetFocus(txtIQUANTITY);
            return false;
        }
        if (workType == "0")
        {
            txtIDS.Text = "";
        }
        if (this.Operation() == SYSOperation.Modify)
        {
            //检查入库单的数量
            if (!(Comm_Fun.Fun_Check_Asn_DQty(txtAsn_D_IDS.Text, txtIDS.Text, GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text),
                                                 txtIQUANTITY.Text.ToDecimal(), workType.ToInt32(), 0, out msg)))
            {
                this.Alert(msg);
                this.SetFocus(txtIQUANTITY);
                return false;
            }
        }

        if (this.hdnWorkType.Text == "1")
        {
            // WL 20160516
            if (string.IsNullOrEmpty(this.ddlWire.SelectedValue))//(this.ddlWire.SelectedValue.ToString() == "0")
            {
                //请选择线别
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG52 + "！");
                return false;
            }

            if (string.IsNullOrEmpty(this.ddl_Pallet_Code.SelectedValue))
            {
                //请选择站点
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG53 + "！");
                return false;
            }


            //判断线别站点是否与其他明细相同
            var siteID=ddl_Pallet_Code.SelectedItem.Value;
            if (InBill.GetInbillDCraneSite(txtID.Text.Trim(), txtIDS.Text.Trim(), ddlWire.SelectedValue, siteID.Substring(siteID.Length-1,1)))
            {
                //补单中,一个入库单的所有明细的线别、站点必须一致
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG54 + "," + Resources.Lang.FrmINBILL_DEdit_MSG55 + "、"+Resources.Lang.FrmINBILL_DEdit_MSG56+"！");
                return false;
            }
            if (this.ddl_InBillType.SelectedValue.ToString() == "0")
            {
                //请选择物料类型
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG57 + "！");
                return false;
            }

            var pallet_code = ddl_Pallet_Code.SelectedValue.ToString().Split('-')[1];
            if (!new SysParameterList().CheckTradeType(this.ddlWire.SelectedValue, pallet_code, hfInType.Value.Trim()))
            {
                //没有绑定此交易类型
                this.Alert(this.ddl_Pallet_Code.SelectedItem.Text +"," + Resources.Lang.FrmINBILL_DEdit_MSG58 + "!");
                return false;
            }


        }
        #region 储位相关

        // string P_BONDED = string.Empty;
        // bool Is_PO_Receipt = true;
        //
        if (this.txtCPOSITIONCODE.Text.Trim() == "")
        {
            //储位编码项不允许空
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG59 + "！");
            this.SetFocus(txtCPOSITIONCODE);
            return false;
        }
        else
        {
            txtCPOSITION.Text = Base_Cargospace.GetCpositionByCpositionCode(this.txtCPOSITIONCODE.Text.Trim());
        }
        //
        if (this.txtCPOSITIONCODE.Text.Trim().Length > 0)
        {
            if (this.txtCPOSITIONCODE.Text.GetLengthByByte() > 30)
            {
                //储位编码项超过指定的长度30
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG60 + "！");
                this.SetFocus(txtCPOSITIONCODE);
                return false;
            }

            //判断储位是否与料号做了关联
            if (!Base_Cargospace.CheckCinvcodeWithCpositioncode(this.txtCPOSITIONCODE.Text.Trim(), GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text))) {
                //该储位未与料号做关联，不能保存！
                this.Alert(Resources.Lang.WMS_Common_Element_CinvcodeCpositioncode + "！");
                this.SetFocus(txtCPOSITIONCODE);
                return false;
            }
        }
        //料号保税非保税属性
        string P_BONDED = string.Empty;
        P_BONDED = Base_Part.GetBond_FromPart(GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text));
        if (!Base_Cargospace.CheckInBill_CPOSITIONCODE(P_BONDED, txtCPOSITIONCODE.Text.Trim()))
        {
            string CINVCODE = GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text);
            if (P_BONDED == "Y")
            {
                //保税，料号,不能选择,非保税储位,请重新选择
                msg = "[" + Resources.Lang.FrmINBILL_DEdit_MSG61 + "]" + Resources.Lang.FrmINBILL_DEdit_MSG62 + "[" + CINVCODE + "]"+Resources.Lang.FrmINBILL_DEdit_MSG63
                    +"[" + txtCPOSITIONCODE.Text.Trim() + "]"+Resources.Lang.FrmINBILL_DEdit_MSG64+"!"+Resources.Lang.FrmINBILL_DEdit_MSG65+"!";
            }
            else
            {
                //非保税,料号,不能选择,保税储位,请重新选择
                msg =  "["+Resources.Lang.FrmINBILL_DEdit_MSG66 +"]"+Resources.Lang.FrmINBILL_DEdit_MSG62+"[" + CINVCODE + "]"+Resources.Lang.FrmINBILL_DEdit_MSG63
                    + "[" + txtCPOSITIONCODE.Text.Trim() + "]" + Resources.Lang.FrmINBILL_DEdit_MSG64 + "!" + Resources.Lang.FrmINBILL_DEdit_MSG65 + "!";
            }
            this.Alert(msg);
            this.SetFocus(txtCPOSITIONCODE);
            return false;
        }
        //检查是否是RMA整新单据入库 CQ 2014-8-22 17:37:28
        string inbill_ID = txtID.Text.Trim();
        string vReturnValue = string.Empty;
        if (!(Comm_Fun.Fun_RMA_To_WareHouse(inbill_ID, txtCPOSITIONCODE.Text.Trim().ToUpper(), WmsWebUserInfo.GetCurrentUser().UserNo, "",
                                               "", out vReturnValue)))
        {
            this.Alert(vReturnValue);
            this.SetFocus(txtCPOSITIONCODE);
            return false;
        }

        //判断储位是否冻结 1-冻结
        if (new Base_Cargospace().CheckCpositionStatus(txtCPOSITIONCODE.Text.Trim().ToUpper(), "1"))
        {
            //储位状态为[冻结],不可使用
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG67 + "[" + Resources.Lang.FrmINBILL_DEdit_MSG68 + "]," + Resources.Lang.FrmINBILL_DEdit_MSG69+ "！");
            this.SetFocus(txtCPOSITIONCODE);
            return false;
        }

        #endregion

        // WIP Return=43-103 工单退料 储位验证 和 数量验证
        if ((hfInType.Value.Length > 0 && hfInType.Value.Equals("103")) || hfInType.Value.Equals("105"))//INBILL_DRule.CheckChaoTuiType(hfInType.Value)
        {
            //验证数量 保税、非保税、入库通知单总数
            msg = InBill.CheckWipReturnQty_InBill(txtID.Text.Trim(), GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text), txtCPOSITIONCODE.Text.Trim(), txtIQUANTITY.Text.Trim(), workType, hfOriginalQty.Value.Trim());
            if (!msg.Equals("OK"))
            {
                Alert(msg);
                this.SetFocus(txtIQUANTITY);
                return false;
            }
        }

        //判断类型是否是43-103 CQ 2014-6-9 09:34:30
        if (InBill.CheckInType(txtID.Text.Trim(), "103"))
        {
            if (InBill.CheckWipReturn_Assit(txtID.Text.Trim()))
            {
                //Wip Return类型工单退料：工单下的入库通知单已生成指引则不能补单
                Alert(Resources.Lang.FrmINBILL_DEdit_MSG70 + ":" + Resources.Lang.FrmINBILL_DEdit_MSG71 + "!");
                this.SetFocus(txtIQUANTITY);
                return false;
            }
        }

        //料号检查
        msg = Base_Part.CheckWIP_PartByBillId(txtID.Text, GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text));
        if (!msg.Equals("OK"))
        {
            this.Alert(msg);
            this.SetFocus(txtCINVCODE);
            return false;
        }

        //料号储位区域检查 CQ 2014-2-21 09:25:20
        string vResult = string.Empty;
        if (!(Comm_Fun.Fun_IsControl_Area(GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text,txtRANK_FINAL.Text), txtCPOSITIONCODE.Text.Trim(),
                                             WmsWebUserInfo.GetCurrentUser().UserNo, 0, "", "", out vResult)))
        {
            this.Alert(vResult);
            this.SetFocus(txtCPOSITIONCODE);
            return false;
        }

        //20131105102731 通知单存在修改中的料时，不允许生成出库单
        var resultnew = Comm_Fun.CanAsnDebit(txtID.Text.Trim(), GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text), "3", "", "");
        if (!resultnew.Equals("OK"))
        {
            Alert(resultnew);
            return false;
        }

        //Roger SN整合 2013/12/12 11:39:54
        if (this.Operation() == SYSOperation.Modify && InBill.ExistSN(txtIDS.Text))
        {
            IGenericRepository<INBILL_D> conBase = new GenericRepository<INBILL_D>(context);
            var caseListBase = from p in conBase.Get()
                               where p.ids == txtIDS.Text
                               select p;
            INBILL_D entity = caseListBase.ToList().FirstOrDefault<INBILL_D>();
            if (entity.iquantity != Convert.ToDecimal(txtIQUANTITY.Text.Trim()))
            {
                //已经存在SN，请删除后修改数量
                Alert(Resources.Lang.FrmINBILL_DEdit_MSG72 + "，" + Resources.Lang.FrmINBILL_DEdit_MSG73 );
                return false;
            }
            if (!entity.cinvcode.Equals(GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text)))
            {
                //已经存在SN，请删除后修改料号
                Alert(Resources.Lang.FrmINBILL_DEdit_MSG72 + "，" + Resources.Lang.FrmINBILL_DEdit_MSG74);
                return false;
            }
        }
        #endregion
       
        if (txtIQUANTITY.Text.Trim().IsNullOrEmpty())
        {
            //数量不能为空
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG75 + "！");
            this.txtIQUANTITY.Focus();
            return false;
        }
        else
        {
            decimal d;
            if (decimal.TryParse(txtIQUANTITY.Text.Trim(), out d))
            {
                //新增逻辑：检查出库单的项次数量总和是否大于出库通知单项次的数量
                //Asn_D_IDS
                IGenericRepository<INBILL_D> ind = new GenericRepository<INBILL_D>(context);
                decimal? totalQty = (from p in ind.Get().AsEnumerable()
                                    where p.asn_d_ids == Asn_D_IDS && p.ids != txtIDS.Text.Trim()
                                    select p).Sum(x => x.iquantity);//获取入库通知单项次生成的所有入库单的总和，除了本项次以外
                IGenericRepository<INASN_D> asd = new GenericRepository<INASN_D>(context);
                decimal? asndQty = (from p in asd.Get()
                                   where p.ids == Asn_D_IDS
                                   select p
                                   ).FirstOrDefault().iquantity;//获取出库单项次数量

                if (asndQty < totalQty + d)
                {
                    //出库通知单项次的数量:小于已生出入库单数量+本次数量 的总和
                    Alert(Resources.Lang.FrmINBILL_DEdit_MSG76 + ":" + asndQty + Resources.Lang.FrmINBILL_DEdit_MSG77 + ":" + totalQty + 
                        "+" + Resources.Lang.FrmINBILL_DEdit_MSG78 + ":" + d + Resources.Lang.FrmINBILL_DEdit_MSG79);
                    this.txtIQUANTITY.Focus();
                    return false;
                }
            }
            else
            {
                //数量不是数字
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG80 + "！");
                this.txtIQUANTITY.Focus();
                return false;
            }
        }

        #region 补单特殊校验
        if (hdnCreateType.Value == "2")
        {
            if (this.hdnWorkType.Text == "1")
            {
                //判断储位是否在线别中
                string lineid = InBill.GetCpositionLineid(txtCPOSITIONCODE.Text.Trim());
                //储位是否维护线别
                if (lineid.IsNullOrEmpty())
                {
                    //储位未维护线别信息
                    this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG81 + "！");
                    this.txtCPOSITIONCODE.Focus();
                    return false;
                }
                //储位线别是否相符
                //if (lineid != InBill.GetCraneId(ddlWire.SelectedValue)) 
                if (lineid != ddlWire.SelectedValue)
                {
                    //储位,不在线别,中
                    this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG82 + "[" + txtCPOSITIONCODE.Text.Trim() + "]" + Resources.Lang.FrmINBILL_DEdit_MSG83 + "[" + ddlWire.SelectedItem.Text + "]" + 
                               Resources.Lang.FrmINBILL_DEdit_MSG84 + "！");
                    this.txtCPOSITIONCODE.Focus();
                    return false;
                }


                //判断当前明细储位是否与其他明细储位相同
                if (InBill.CpositionCodeIdentical(txtID.Text.Trim(), txtCPOSITIONCODE.Text.Trim(), txtIDS.Text.Trim()))
                {
                    //补单产生的入库单明细的储位必须一致
                    this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG85 + "！");
                    this.txtCPOSITIONCODE.Focus();
                    return false;
                }


                //判断储位是否被占用
                if (InBill.CpositionCodeIsOccupation(txtCPOSITIONCODE.Text.Trim(), txtID.Text.Trim()))
                {
                    //储位被占用
                    this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG86 + "！");
                    this.txtCPOSITIONCODE.Focus();
                    return false;
                }

                //判断储位是否存在库存
                if (InBill.IsHasStockCurrent(txtCPOSITIONCODE.Text.Trim()))
                {
                    //储位被占用
                    this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG86 + "！");
                    this.txtCPOSITIONCODE.Focus();
                    return false;
                }

               

            }
            else
            {
                IGenericRepository<V_BASE_CARGOSPACEListQuery> con = new GenericRepository<V_BASE_CARGOSPACEListQuery>(context);
                var caseList = (from p in con.Get()
                               where p.cpositioncode == txtCPOSITIONCODE.Text.Trim()
                               select p).FirstOrDefault<V_BASE_CARGOSPACEListQuery>();

                if (caseList.ipermitmix == 1)
                {

                    IGenericRepository<STOCK_CURRENT> stockcon = new GenericRepository<STOCK_CURRENT>(context);
                    STOCK_CURRENT  stockEntity= (from p in stockcon.Get()
                                    where p.cpositioncode == txtCPOSITIONCODE.Text.Trim()
                                    select p).FirstOrDefault<STOCK_CURRENT>();

                    if (stockEntity != null)
                    {
                       var sumStockQty= InBill.IsMaxCapacity(txtCPOSITIONCODE.Text.Trim());
                        //判断储位是否被占用
                        if (caseList.imaxcapacity==sumStockQty)
                        {
                            //储位最大量,此储位现有数量,储位已满
                            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG87 + "=[" + caseList.imaxcapacity + "]，" + Resources.Lang.FrmINBILL_DEdit_MSG88 + "=[" + sumStockQty + "]," + Resources.Lang.FrmINBILL_DEdit_MSG89+ "!");
                            this.txtCPOSITIONCODE.Focus();
                            return false;
                        }

                         var sumQTY=sumStockQty+Convert.ToDecimal(this.txtIQUANTITY.Text);
                         if (caseList.imaxcapacity < sumQTY)
                         {
                             //储位剩余空间不足
                             this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG90 + "!");
                             this.txtCPOSITIONCODE.Focus();
                             return false;
                         }
                    }

                }

            }

            //判断储位是否存在库存
            if (InBill.IsHasCheckBill(txtCPOSITIONCODE.Text.Trim()))
            {
                //储位,被占用，存在未完成的循环盘点单
                this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG82 + "[" + txtCPOSITIONCODE.Text.Trim() + "]" + Resources.Lang.FrmINBILL_DEdit_MSG91 + "," + Resources.Lang.FrmINBILL_DEdit_MSG92+ "!");
                this.txtCPOSITIONCODE.Focus();
                return false;
            }

        }
        #endregion

        return true;

    }


 
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public INBILL_D SendData(IGenericRepository<INBILL_D> con)
    {
        string ids = Request.QueryString["IDS"].ToString();
        INBILL_D entity = (from p in con.Get()
                           where p.ids == ids
                           select p).FirstOrDefault<INBILL_D>();
        //INBILL_D entity = new INBILL_D();
        //
        this.txtIDS.Text = this.txtIDS.Text.Trim();
        if (this.txtIDS.Text.Length > 0)
        {
            entity.ids = txtIDS.Text;
        }
        else
        {
            entity.ids = Guid.NewGuid().ToString();
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.IDS = null;
        }
        //
        this.txtID.Text = this.txtID.Text.Trim();
        if (this.txtID.Text.Length > 0)
        {
            entity.id = txtID.Text;
        }
        else
        {
            entity.id = Guid.NewGuid().ToString();
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.ID = null;
        }
        this.txtAsn_D_IDS.Text = this.txtAsn_D_IDS.Text.Trim();
        if (this.txtAsn_D_IDS.Text.Length > 0)
        {
            entity.asn_d_ids = txtAsn_D_IDS.Text;
        }
        else
        {
            entity.asn_d_ids = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.ID = null;
        }
        if (!txtLineId.Text.IsNullOrEmpty())
        {
            entity.LineID = txtLineId.Text.ToInt();
        }
        if (!txtAsndLineID.Text.IsNullOrEmpty())
        {
            entity.AsndLineID = txtAsndLineID.Text.ToInt();
        }
        //
        this.txtCSTATUS.Text = this.txtCSTATUS.Text.Trim();
        if (this.txtCSTATUS.Text.Length > 0)
        {
            entity.cstatus = txtCSTATUS.Text;
        }
        else
        {
            entity.cstatus = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSTATUS = null;
        }
        DataTable InAsn_D_Dt = null;
        //
        this.txtCINVCODE.Text = this.txtCINVCODE.Text.Trim();
        if (this.txtCINVCODE.Text.Length > 0)
        {
            //Note by Qamar 2020-11-26
            if (txtRANK_FINAL.Text.Trim().Length == 1)
            {
                entity.cinvcode = txtCINVCODE.Text + "-" + txtRANK_FINAL.Text.Trim().ToUpper();
            }
            else
            {
                entity.cinvcode = txtCINVCODE.Text + "-_";
            }

            IGenericRepository<BASE_PART> Basecon = new GenericRepository<BASE_PART>(context);
            var caseListBase = from p in Basecon.Get()
                               where p.cpartnumber == entity.cinvcode
                               select p;
            if (caseListBase.Count() > 0)
                entity.cinvname = caseListBase.ToList().FirstOrDefault<BASE_PART>().cpartname;
            InAsn_D_Dt = new InBill().GetInAsn_DByInBillIdAndCinv(txtID.Text.Trim(), GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text), txtIQUANTITY.Text.Trim().ToDecimal());
            if (InAsn_D_Dt.Rows.Count == 0) InAsn_D_Dt = null;
        }
        else
        {
            entity.cinvcode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CINVCODE = null;
        }
        //
        //this.txtCINVNAME.Text = this.txtCINVNAME.Text.Trim();
        //if (this.txtCINVNAME.Text.Length > 0)
        //{
        //    entity.CINVNAME = txtCINVNAME.Text;
        //}
        //else
        //{
        //    entity.SetDBNull("CINVNAME", true);
        //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        //    //entity.CINVNAME = null;
        //}
        //
        this.txtIQUANTITY.Text = this.txtIQUANTITY.Text.Trim();
        if (this.txtIQUANTITY.Text.Length > 0)
        {
            entity.iquantity = txtIQUANTITY.Text.ToDecimal();
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.IQUANTITY = null;
        }
        //
        this.txtCINVBARCODE.Text = this.txtCINVBARCODE.Text.Trim();
        if (this.txtCINVBARCODE.Text.Length > 0)
        {
            entity.cinvbarcode = txtCINVBARCODE.Text;
        }
        else
        {

            if (InAsn_D_Dt != null && !(InAsn_D_Dt.Rows[0]["CINVBARCODE"] is DBNull))
            {
                entity.cinvbarcode = InAsn_D_Dt.Rows[0]["CINVBARCODE"].ToString();
            }
            else
            {
                entity.cinvbarcode = string.Empty;
            }

            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CINVBARCODE = null;
        }
        //
        this.txtCERPCODELINE.Text = this.txtCERPCODELINE.Text.Trim();
        if (this.txtCERPCODELINE.Text.Length > 0)
        {
            entity.cerpcodeline = txtCERPCODELINE.Text;
        }
        else
        {
            if (InAsn_D_Dt != null && !(InAsn_D_Dt.Rows[0]["CERPCODELINE"] is DBNull))
            {
                entity.cerpcodeline = InAsn_D_Dt.Rows[0]["CERPCODELINE"].ToString();
            }
            else
            {
                entity.cerpcodeline = string.Empty;
            }

            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CERPCODELINE = null;
        }
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        else
        {
            if (InAsn_D_Dt != null && !(InAsn_D_Dt.Rows[0]["CMEMO"] is DBNull))
            {
                entity.cmemo = InAsn_D_Dt.Rows[0]["CMEMO"].ToString();
            }
            else
            {
                entity.cmemo = string.Empty;
            }
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CMEMO = null;
        }
        //
        this.txtCPOSITIONCODE.Text = this.txtCPOSITIONCODE.Text.Trim();
        if (this.txtCPOSITIONCODE.Text.Length > 0)
        {
            entity.cpositioncode = txtCPOSITIONCODE.Text;
        }
        else
        {
            entity.cpositioncode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CPOSITIONCODE = null;
        }
        //
        this.txtCPOSITION.Text = this.txtCPOSITION.Text.Trim();
        if (this.txtCPOSITION.Text.Length > 0)
        {
            entity.cposition = txtCPOSITION.Text;
        }
        else
        {
            if (this.txtCPOSITIONCODE.Text.Trim().Length > 0)
            {
                IGenericRepository<BASE_CARGOSPACE> conBase = new GenericRepository<BASE_CARGOSPACE>(context);
                var caseListBase = from p in conBase.Get()
                                   where p.cpositioncode == this.txtCPOSITIONCODE.Text.Trim()
                                   select p;
                entity.cposition = caseListBase.ToList().FirstOrDefault<BASE_CARGOSPACE>().cposition;

            }
            else
            {
                entity.cposition = string.Empty;
            }
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CPOSITION = null;
        }
        //
        this.txtDINDATE.Text = this.txtDINDATE.Text.Trim();
        if (this.txtDINDATE.Text.Length > 0)
        {
            entity.dindate = txtDINDATE.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        else
        {
            entity.dindate = DateTime.Now;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.DINDATE = null;
        }
        //
        this.txtCINPERSONCODE.Text = this.txtCINPERSONCODE.Text.Trim();
        if (this.txtCINPERSONCODE.Text.Length > 0)
        {
            entity.cinpersoncode = txtCINPERSONCODE.Text;
        }
        else
        {
            entity.cinpersoncode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CINPERSONCODE = null;
        }
        //
        this.txtIASNLINE.Text = this.txtIASNLINE.Text.Trim();
        if (this.txtIASNLINE.Text.Length > 0)
        {
            entity.iasnline = txtIASNLINE.Text.ToDecimal();
        }
        else
        {
            entity.iasnline = decimal.Zero;
        }

        if (this.hdnWorkType.Text == "1")
        {
            entity.wire = ddlWire.SelectedValue.ToString();
            entity.pallet_code = ddl_Pallet_Code.SelectedValue.ToString().Split('-')[1];
        }
        entity.partno = txtPartNo.Text.Trim().ToString();
        entity.inbilltype = ddl_InBillType.SelectedValue.ToString();

        return entity;
    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Note by Qamar 2020-11-26
        txtRANK_FINAL.Text = txtRANK_FINAL.Text.Trim().ToUpper();
        if (txtRANK_FINAL.Text.Length <= 1)
        {
            IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
            this.btnSave.Enabled = false; //CQ 2013-5-13 13:47:51
            if (this.CheckData())
            {
                INBILL_D entity = (INBILL_D)this.SendData(con);
                txtCINVNAME.Text = entity.cinvname;
                if (this.Operation() != SYSOperation.New)
                {
                }
                string strKeyID = "";
                try
                {
                    if (this.Operation() == SYSOperation.Modify)
                    {
                        strKeyID = entity.ids.ToString();
                        entity.asrs_status = 0;
                        con.Update(entity);
                        con.Save();

                        //Note by Qamar 2020-11-23
                        Save_Others_INBILL_D(entity);

                        //更新成功
                        this.AlertAndBack("FrmINBILL_DEdit.aspx?IDS=" + this.txtIDS.Text + "&" + BuildQueryString(SYSOperation.Modify, this.txtID.Text.Trim()) + "&InType=" + Request.QueryString["InType"], Resources.Lang.FrmINBILL_DEdit_MSG92);
                    }
                    else if (this.Operation() == SYSOperation.New)
                    {
                        this.txtIDS.Text = Guid.NewGuid().ToString();
                        entity.AsndLineID = txtAsndLineID.Text.ToInt();
                        entity.LineID = GetMaxLineId(txtID.Text) + 1;
                        entity.ids = txtIDS.Text;
                        entity.asrs_status = 0;
                        con.Insert(entity);
                        con.Save();
                        //新增该入库单选择的料号为补单状态
                        InBill.UpdateAsnStatus(txtID.Text, GetCINVCODE_from_CINVCODE_and_RANK_FINAL(txtCINVCODE.Text, txtRANK_FINAL.Text));
                        //保存成功
                        this.AlertAndBack("FrmINBILL_DEdit.aspx?IDS=" + this.txtIDS.Text + "&" + BuildQueryString(SYSOperation.Modify, this.txtID.Text.Trim()) + "&InType=" + Request.QueryString["InType"], Resources.Lang.FrmINBILL_DEdit_MSG93);
                    }
                }
                catch (Exception E)
                {
                    //失败
                    lblErrorMsg.Text = this.GetOperationName() + Resources.Lang.FrmINBILL_DEdit_MSG94 + "！" + E.Message;
#if Debug
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
                    this.btnSave.Enabled = true;
                }
            }
        }
        else
        {
            //Note by Qamar 2020-11-26
            this.Alert("批/序號(RANK)有誤");
        }
        this.btnSave.Enabled = true;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
        try
        {
            con.Delete(this.KeyID.ToString());
            con.Save();
        }
        catch (Exception E)
        {
            //删除失败
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG95 + "！" + E.Message);
            #if Debug 
            this.Response.Write(entity.DBAccess().GetLastSQL());                
            #endif
        }
    }

    /// <summary>
    /// 打开选择储位页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOpenCARGOSPACEList_Click(object sender, EventArgs e)
    {
        this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("/Apps/BASE/FrmShowBASE_SELECTCARGOSPACEList.aspx", SYSOperation.New, "") + "&IDS=" + hfInasn_id.Value + "&TradeCode=" + this.hfTradeCode.Value + "&Currency=" + hfCurrency.Value + "&PartNo=" + txtCINVCODE.Text + "&InType=" + this.hfInType.Value + "&InQty=" + txtIQUANTITY.Text + "','选择储位','INASN_D',650,500);");
    }

    public void GridBind()
    {
        IGenericRepository<V_INBILL_DSN> entity = new GenericRepository<V_INBILL_DSN>(context);
        var caseList = from p in entity.Get()
                       orderby p.createtime descending
                       where p.quantity > 0
                       select p;

        if (txtIDS.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.inbill_d_ids) && x.inbill_d_ids.Contains(txtIDS.Text.Trim()));
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
        grdSNDetial.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdSNDetial.DataBind();      
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
       // this.GridBNBind();
    }
    //**********************************************************************************************************
    //**********************************************************************************************************

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public INBILL_D_SN SendSNData()
    {

        INBILL_D_SN entity = new INBILL_D_SN();
        //
        txtIDS.Text = txtIDS.Text.Trim();
        if (this.txtIDS.Text.Length > 0)
        {
            entity.inbill_d_ids = txtIDS.Text;
        }
        else
        {
            entity.inbill_d_ids = string.Empty;
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
        entity.worktype = "0";
        //
        IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
        var caseList = from p in con.Get()
                       where p.ids == txtIDS.Text.Trim()
                       select p;
        if (caseList.Count() > 0) entity.inbill_id = caseList.ToList().FirstOrDefault<INBILL_D>().id;
        //
        entity.istransed = "0";

       

        // entity.sn_code=
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
    protected void btnSaveSN_Click(object sender, EventArgs e)
    {
        IGenericRepository<INBILL_D_SN> con = new GenericRepository<INBILL_D_SN>(context);

        if (this.CheckSNData())
        {
            try
            {

                //删除SN
                new InBill().DeleteSn(txtIDS.Text.Trim());
                //保存SN
                for (int i = 0; i < grdSNDetial.Rows.Count; i++)
                {
                    INBILL_D_SN entity = (INBILL_D_SN)this.SendSNData();
                    entity.id = this.grdSNDetial.DataKeys[i].Values[0].ToString();
                    entity.sn_code = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtSN")).Text.Trim().ToUpper();
                    var txtdatecode = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtDateCode")).Text.Trim();
                    var datecode = !string.IsNullOrEmpty(entity.sn_code) && entity.sn_code.Length>6 ? entity.sn_code.Substring(0, 8):
                        (!string.IsNullOrEmpty(txtdatecode)? txtdatecode : DateTime.Now.Date.ToString("yyyyMMdd").ToString());
                    entity.datecode=!string.IsNullOrEmpty(datecode)?Convert.ToDecimal(datecode):0;
                    entity.sntype =0;
                
                    string Quantity = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtQty")).Text.Trim();
                    if (!string.IsNullOrEmpty(Quantity)) entity.quantity = Convert.ToDecimal(Quantity);

                    BarCodeInfo snentity = GetBarCodeInfoBySn(entity.sn_code);
                    if (snentity != null && snentity.ReturnValue == "0")
                    {
                        entity.RULECODE = snentity.RuleCode; 
                    }

                    // 炉号
                    entity.furnaceno = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtFurnaceNo")).Text.Trim();

                    //栈板栈板模式、箱-箱模式的时候，直接把sncode的值赋值给栈板号，箱-栈板模式的时候，直接用用户输入的值
                    entity.PalletCode = SYSConfig=="1"? entity.sn_code:((TextBox)this.grdSNDetial.Rows[i].FindControl("txtPalletCode")).Text.Trim(); 

                    con.Insert(entity);
                    con.Save();
                    

                    //补单，update 栈板号
                    IGenericRepository<INBILL> inbillcon = new GenericRepository<INBILL>(context);
                    INBILL inbill = (from p in inbillcon.Get().AsEnumerable()
                                     where p.id == this.KeyID
                                     select p).ToList<INBILL>().FirstOrDefault();

                    inbill.palletcode = entity.PalletCode;
                    inbillcon.Update(inbill);
                    inbillcon.Save();

                }
                //保存成功
                this.AlertAndBack("FrmINBILL_DEdit.aspx?IDS=" + this.txtIDS.Text + "&" + BuildQueryString(SYSOperation.Modify, this.txtID.Text.Trim()) + "&InType=" + Request.QueryString["InType"], Resources.Lang.FrmINBILL_DEdit_MSG93);
            }
            catch (Exception ex)
            {
                //保存失败
                Alert(Resources.Lang.FrmINBILL_DEdit_MSG94 + "[" + ex.Message + "]");
            }
        }
    }

    //增行
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (SYSConfig == "1")//箱模式
        {
            //箱模式只能有一条记录
            if (grdSNDetial.Rows.Count >= 1)
            {
                Alert(Resources.Lang.FrmINBILL_DEdit_OnlyMsg);//已经存在一条记录
                return;
            }
        }
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
        IGenericRepository<INBILL_D_SN> con = new GenericRepository<INBILL_D_SN>(context);
        
        #region 无保存明细
        DataTable table = GetGridViewData();
        bool delSN = false;
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
                            var snEntity = (from p in con.Get().AsEnumerable()
                                            where p.id == dtRow["ID"].ToString()
                                            select p).FirstOrDefault();
                            if (snEntity != null && snEntity.id != null && snEntity.id.Length > 0)
                            {
                                con.Delete(snEntity.id);
                                con.Save();
                            }

                            table.Rows.Remove(dtRow);
                            delSN = true;
                            break;
                        }
                    }
                }
            }
        }

        grdSNDetial.DataSource = table;
        grdSNDetial.DataBind();
        if (delSN)
        {
            //删除成功
            this.Alert(Resources.Lang.FrmINBILL_DEdit_MSG97 + "!");
        }
        #endregion

    }

    //获取网格数据
    private DataTable GetGridViewData()
    {   
        var dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID"));
        dt.Columns.Add(new DataColumn("SN_CODE"));
        dt.Columns.Add(new DataColumn("QUANTITY"));
        dt.Columns.Add(new DataColumn("SNTYPE"));
        dt.Columns.Add(new DataColumn("DateCode"));
        dt.Columns.Add(new DataColumn("RULECODE"));
        dt.Columns.Add(new DataColumn("PalletCode"));       
        dt.Columns.Add(new DataColumn("furnaceno"));
        dt.Columns.Add(new DataColumn("CREATEOWNER"));
        dt.Columns.Add(new DataColumn("DisplayTYPE"));
        dt.Columns.Add(new DataColumn("CREATETIME"));
        for (int i = 0; i < grdSNDetial.Rows.Count; i++)
        {
            DataRow sourseRow = dt.NewRow();
            sourseRow["ID"] = this.grdSNDetial.DataKeys[i].Values[0].ToString();
           // sourseRow["SNTYPE"] = ((Label)this.grdSNDetial.Rows[i].FindControl("labtype")).Text.Trim();
            sourseRow["SN_CODE"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtSN")).Text.Trim().ToUpper();
            sourseRow["QUANTITY"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtQty")).Text.Trim();
            sourseRow["DisplayTYPE"] = GetConfigSNName();//((Label)this.grdSNDetial.Rows[i].FindControl("labtype")).Text.Trim();
            sourseRow["RULECODE"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtRuleCode")).Text.Trim();
            sourseRow["furnaceno"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtFurnaceNo")).Text.Trim();
            if (this.hdnWorkType.Text == "1")
            {
                sourseRow["PalletCode"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtPalletCode")).Text.Trim();
            }
            else
            {
                ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtPalletCode")).Enabled = false;
            }
            sourseRow["DateCode"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtDateCode")).Text.Trim();
           
            dt.Rows.Add(sourseRow);
        }


        return dt;
    }

    //栈板箱新增
    protected void btnCarton_Click(object sender, EventArgs e)
    {
        DataTable table = GetGridViewData();
        DataRow newRow = table.NewRow();
        newRow["ID"] = Guid.NewGuid().ToString();
        table.Rows.Add(newRow);
        grdSNDetial.DataSource = table;
        grdSNDetial.DataBind();
    }

    //设置
    protected void grdSNDetial_RowDataBound(object sender, GridViewRowEventArgs e)
    {       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var txtDateCode = e.Row.FindControl("txtDateCode") as TextBox; 
            //txtDateCode.Enabled = false;                                              
            var txtSN = e.Row.FindControl("txtSN") as TextBox;
            var txtRuleCode = e.Row.FindControl("txtRuleCode") as TextBox;
            txtRuleCode.Enabled = false;
            var txtFurnaceNo = e.Row.FindControl("txtFurnaceNo") as TextBox;
            var txtpalletCode = e.Row.FindControl("txtPalletCode") as TextBox;
            var labtype = e.Row.FindControl("labtype") as Label;
            labtype.Text = GetConfigSNName();
            if (this.hdnWorkType.Text == "0")
            {
               // e.Row.FindControl("txtPalletCode").Visible = false;
                txtpalletCode.Enabled = false;
            }
            if (!P_Status.Equals("0"))
            {
                var txtQty = e.Row.FindControl("txtQty") as TextBox;
                txtSN.Enabled = false;
                txtQty.Enabled = false;
                txtDateCode.Enabled = false;               
                txtFurnaceNo.Enabled = false;
                txtpalletCode.Enabled = false;
                txtRuleCode.Enabled = false;
            }
            else
            {
                txtDateCode.Enabled = true;
            }
        }
    }


    /// <summary>
    /// check 是否需要校验箱号
    /// </summary>
    /// <returns></returns>
    private static bool CheckIsPackageConfig()
    {
        bool bl = false;
        string sql = @" select dbo.FUN_CHECKIsPKG() ";
        string res = DBHelp.ExcuteScalarSQL(sql).ToString();
        if (res.Equals(1))
        {
            bl = true;
        }
        return bl;
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckSNData()
    {
        IGenericRepository<INBILL_D> con = new GenericRepository<INBILL_D>(context);
        var caseList = from p in con.Get()
                       where p.ids == txtIDS.Text.Trim()
                       select p;
        INBILL_D entity = caseList.ToList().FirstOrDefault<INBILL_D>();
        if (string.IsNullOrEmpty(entity.cpositioncode))
        {
            //没有指定入库储位,不能保存
            Alert(Resources.Lang.FrmINBILL_DEdit_MSG98 + "，" + Resources.Lang.FrmINBILL_DEdit_MSG99);
            return false;
        }

        //校验数量和是否与明细数量一致
        var dtSN = GetGridViewData();
        var i = 0;
        var validatesnCode = string.Empty;

        foreach (DataRow dr in dtSN.Rows)
        {
            i++;

            var snCode = dr["SN_CODE"] != null ? dr["SN_CODE"].ToString().Trim().ToUpper() : string.Empty;
            if (string.IsNullOrEmpty(snCode))
            {
                //第,行,栈板号/箱号/SN不能为空
                Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + "," + Resources.Lang.FrmINBILL_DEdit_MSG102);
                return false;
            }
            else
            {
                BarCodeInfo snentity = GetBarCodeInfoBySn(snCode);
                if (snentity != null && snentity.ReturnValue == "1")
                {
                    //第，行
                    Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + "," + snentity.ErrorMsg);
                    return false;
                }
                else
                {
                    if (SYSConfig == "2") //栈板
                    {
                        if (snentity.CinvCode != entity.cinvcode)
                        {
                            //第,行
                            Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + "," + Resources.Lang.FrmINBILL_DEdit_CinvcodeNotSame + "！");//SN中的料号与入库单明细的料号不一致
                            return false;
                        }
                    }
                }
            }

        
            //判断数字
            var qty = dr["QUANTITY"] != null ? dr["QUANTITY"].ToString() : string.Empty;

            if (string.IsNullOrEmpty(qty))
            {
                //第，行，数量不能为空
                Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + "," + Resources.Lang.FrmINBILL_DEdit_MSG103);
                return false;
            }
            decimal num;
            try
            {
                num = decimal.Parse(qty);

            }
            catch (Exception ex)
            {
                //第，行，数量，不是有效数字
                Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + "," +
                       Resources.Lang.FrmINBILL_DEdit_MSG104 + "[" + qty + "]" + Resources.Lang.FrmINBILL_DEdit_MSG105);
                return false;
            }
            if (num == 0 || num < 0)
            {
                //第,行,数量,必须大于0
                Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + "," + Resources.Lang.FrmINBILL_DEdit_MSG104 + "[" + qty + "]" + Resources.Lang.FrmINBILL_DEdit_MSG105);
                return false;
            }

            var palletCode = dr["PalletCode"] != null ? dr["PalletCode"].ToString().Trim().ToUpper() : string.Empty;
            //仅限立库才检查
            if (this.hdnWorkType.Text == "1")
            {
                if (SYSConfig == "1")//箱模式
                {
                    palletCode = snCode;
                }
                int result = 0;
                if (string.IsNullOrEmpty(palletCode))
                {
                    //第,行,栈板号不能为空
                    Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + "," + Resources.Lang.FrmINBILL_DEdit_MSG106);
                    return false;
                }
                BarCodeInfo snentity = GetBarCodeInfoBySn(palletCode);
                if (snentity != null && snentity.ReturnValue == "1")
                {
                    //第，行
                    Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + "," + snentity.ErrorMsg);
                    return false;
                }
                //if (Int32.TryParse(palletCode, out result))
                //{

                //if (palletCode.Length != 6)
                //{
                //    //第，行，栈板号长度不正确,只能是6位数字,并且以1或2开头
                //    Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + "," + Resources.Lang.FrmINBILL_DEdit_MSG102 + "，" + Resources.Lang.FrmINBILL_DEdit_MSG107
                //           + "，" + Resources.Lang.FrmINBILL_DEdit_MSG108+ "！");
                //    return false;
                //}
                //else if (palletCode.Length == 6 && (!palletCode.StartsWith("1") && !palletCode.StartsWith("2")))
                //{
                //    //第，行,栈板号格式不正确，栈板号必须是以1或2开头的6位数
                //    Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + "," + Resources.Lang.FrmINBILL_DEdit_MSG109 + "," 
                //          + Resources.Lang.FrmINBILL_DEdit_MSG110 + "！");
                //    return false;
                //}

                //}
                //else
                //{
                //    //第,行，栈板号不正确,栈板号只能是6位数字
                //    Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + "," + Resources.Lang.FrmINBILL_DEdit_MSG111 + "，" + Resources.Lang.FrmINBILL_DEdit_MSG112 + "！");
                //    return false;
                //}

                //datecode 验证 20200313               
                var str_datecode = dr["DateCode"] != null ? dr["DateCode"].ToString() : string.Empty;
                if (!string.IsNullOrEmpty(str_datecode))
                {
                    string checkmsg = string.Empty;
                    checkmsg = Comm_Fun.CheckDateCodeFormat(str_datecode);
                    if (checkmsg.Length > 0)
                    {
                        Alert(checkmsg);
                        return false;
                    }
                    //固定长度的datecode验证
                    //if (str_datecode.Length != 8)
                    //{
                    //    Alert(Resources.Lang.FrmINASN_DEdit_MSG12); //DateCode 必须为8位
                    //    return false;
                    //}
                    //else
                    //{   
                    //   try
                    //   {
                    //        dt_datecode = DateTime.ParseExact(str_datecode, "yyyyMMdd", null);
                    //        if (dt_datecode.CompareTo(DateTime.Now) > 0)
                    //        {
                    //            Alert(Resources.Lang.FrmINASN_DEdit_CanNotExceedCurrent); //DateCode 不能大于当前日期
                    //            return false;
                    //        }
                    //    }
                    //    catch
                    //   {
                    //        Alert(Resources.Lang.FrmINASN_DEdit_MSG13); //DateCode必须是年月日[YYYYMMDD]格式类型
                    //        return false;
                    //    }                      
                    //}
                }
                else
                {
                    Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG38); //DateCode项不能为空
                    return false;
                }              

                //判断是否与入库重复，如果重复，则校验是否为wip return、超领退
                if (InBill.ExistSNBySnCode(palletCode, snCode, this.txtIDS.Text.Trim()))
                {
                    //判断是否为wip return、超领退
                    if (InBill.IsWipReturn(this.txtIDS.Text.Trim()))
                    {
                        //获取工单号
                        //var ErpCode = INBILLRule.GetErpByCode(txtInbillCode.Text.Trim());

                        //调用统一方法校验WR
                        //获取入库通知单单号
                        var acnCode = InBill.GetAsnCodeByInbillCode(txtInbillCode.Text.Trim());
                        var msg = InBill.WRCheck(acnCode, snCode, qty, "1", dr["ID"].ToString().Trim());
                        if (!msg.Equals("0"))
                        {
                            Alert(msg);
                            return false;
                        }
                    }
                    else
                    {
                        //第，行，已经存在于其他入库单中或超发回调中
                        Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + ",[" + snCode + "]" + Resources.Lang.FrmINBILL_DEdit_MSG113);
                        return false;
                    }
                }

                //检查箱号是否已存在于其他栈板中
                if (InBill.ExistPalletCodeBySnCode(palletCode, snCode, this.KeyID))
                {
                    //已经存在于其他入库单中或超发回调中
                    Alert("[" + palletCode + "]" + Resources.Lang.FrmINBILL_DEdit_MSG113);
                    return false;

                }

                string billPalletCode = InBill.CheckPalletCodeByBill(this.KeyID);

                if (!string.IsNullOrEmpty(billPalletCode) && palletCode != billPalletCode)
                {
                    //此入库单中已包含栈板号,请使用此栈板号,新增明细
                    Alert(Resources.Lang.FrmINBILL_DEdit_MSG114 + "=[" + billPalletCode + "]," + Resources.Lang.FrmINBILL_DEdit_MSG115 + "[" + billPalletCode + "]" + Resources.Lang.FrmINBILL_DEdit_MSG116 + "！");
                    return false;
                }

                if (!string.IsNullOrEmpty(validatesnCode) && validatesnCode != palletCode)
                {
                    //一个入库单下面，只能指定一个栈板号
                    Alert(Resources.Lang.FrmINBILL_DEdit_MSG117 + "，" + Resources.Lang.FrmINBILL_DEdit_MSG118);
                    return false;
                }
                else
                {
                    validatesnCode = palletCode;
                }

            }

            if (this.hdnWorkType.Text == "0")
            {
                if (InBill.ExistPCSNBySnCode1(snCode, this.KeyID))
                {
                    //第，行,已经存在于其他入库单中或超发回调中
                    Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + ",[" + snCode + "]" + Resources.Lang.FrmINBILL_DEdit_MSG113);
                    return false;
                }
                else
                {
                    //验证当前sn是否存在于未完成的暂存调立库中；
                     if (InBill.CheckExistsInTempAllo(snCode))
                     {
                         //第，行,已存在于未完成的暂存调立库的调拨单中
                         Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + ",[" + snCode + "]" + Resources.Lang.FrmINBILL_DEdit_MSG113_hasTempAllo);
                         return false;
                     }                
                }
            }

            //如果有相同的SN，则报错
            var dts = GetGridViewData();
            var listq =
                from t in dts.AsEnumerable()
                group t by new { t1 = t.Field<string>("SN_CODE") }
                    into m
                    select new
                    {
                        rowcount = m.Count()
                    };
            var count = listq.ToList().Exists(q => q.rowcount > 1);
            if (count)
            {
                //SN入库列表存在SN一致的数据
                Alert(Resources.Lang.FrmINBILL_DEdit_MSG119);
                return false;
            }

            if (CheckIsPackageConfig())
            {                
                #region 箱号检查
                List<string> SparaList = new List<string>();
                SparaList.Add("@P_PalletCode:" + palletCode.Trim());
                SparaList.Add("@P_RowNum:" + i.ToString());
                SparaList.Add("@P_InbillId:" + txtID.Text.Trim());
                SparaList.Add("@P_CpositionCode:" + txtCPOSITIONCODE.Text.Trim());
                SparaList.Add("@P_LineId:" + ddlWire.SelectedValue);
                SparaList.Add("@P_SiteId:" + ddl_Pallet_Code.SelectedValue);
                SparaList.Add("@P_ReturnValue:" + "");
                SparaList.Add("@P_INFOTEXT:" + "");
                string[] Result = DBHelp.ExecuteProc("Proc_ReplacementOrder_PalletCodeCheck", SparaList);
                if (Result.Length > 1)//调用存储过程有错误
                {
                    if (Result[1].ToString().Length > 0)
                    {
                        this.Alert(Result[1].ToString());
                        return false;
                    }
                }

                if (dr["DateCode"].ToString().IsNullOrEmpty())
                {
                    //第,行,生产日期不能为空
                    Alert(Resources.Lang.FrmINBILL_DEdit_MSG100 + "[" + i + "]" + Resources.Lang.FrmINBILL_DEdit_MSG101 + "," + Resources.Lang.FrmINBILL_DEdit_MSG120);
                    return false;
                }
                #endregion
            }


        }

        #region   判断数量合计
        //判断数量合计

        var snQty = dtSN.AsEnumerable().Cast<DataRow>().Select(ds => decimal.Parse(ds["QUANTITY"].ToString())).Sum();
        var Qty = decimal.Parse(txtIQUANTITY.Text.Trim());

        if (snQty != Qty)
        {
            //栈板/箱号/SN 数量总和,与明细数量,不一致
            Alert(Resources.Lang.FrmINBILL_DEdit_MSG121 + "[" + snQty + "]" + Resources.Lang.FrmINBILL_DEdit_MSG122 + "[" + Qty + "]" + Resources.Lang.FrmINBILL_DEdit_MSG123);
            return false;
        }

        #endregion

        return true;
    }
    


    private static bool IsMoreThanZeroDecimal(string str)
    {
        return new Regex(@"^([\+ \-]?(([1-9]\d*)|(0)))([.]\d{2})?$").IsMatch(str);
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


