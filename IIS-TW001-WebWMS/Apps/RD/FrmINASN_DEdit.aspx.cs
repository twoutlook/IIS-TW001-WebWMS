using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Windows.Forms;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Text.RegularExpressions;

/// <summary>
/// 描述: 入库管理-->FrmINASN_DEdit 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:47:15
/// 
/// Roger
/// 2013/5/13 13:50:51
/// 20130513135051
/// 解决工单输入自相erpcode与主表erpcode不一致问题
/// 
/*
Roger
2013/5/15 14:09:19
20130515140920
解决问题：产线习惯不输入erpcode
处理办法：工单类型增设默认值
*/

/*
Roger
2013/5/20 9:23:12
20130520092312
解决问题：导入及新增明细存在小数点问题
处理办法：增加卡控
*/
/*
Roger
2013/7/31 13:46:49
20130731134649
增加校验是否为正在修改中的料号
*/
/// </summary>
public partial class RD_FrmINASN_DEdit : WMSBasePage// PageBase, IPageEdit
{
    #region SQL
    //DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.ShowReTurnDiv1.SetCINVCODE = this.txtCINVCODE.ClientID;
        //this.ShowReTurnDiv1.SetCINVNAME = this.txtCINVNAME.ClientID;

        this.ShowPARTDiv1.SetCINVCODE = this.txtCINVCODE.ClientID;
        this.ShowPARTDiv1.SetCINVNAME = this.txtCINVNAME.ClientID;
        this.ShowPARTDiv1.SetCspec = this.txtcspecifications.ClientID;


        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData();
            }
            LoadIDS();

            //20130515140920
            if (this.Operation() == SYSOperation.New)
            {
                txtIQUANTITY.Enabled = true;
                //工单类型取消，使用流水号 CQ2015-4-2 11:23:16
                //var NoticesOfMo = INASN_DRule.IsMoNotice(InType.Trim());
                //if (NoticesOfMo) //工单
                //{
                //    txtCERPCODELINE.Text = ErpCode.Trim();
                //}
                DpdStatus.SelectedValue = "0";//新增状态为未处理
                txtCERPCODELINE.Text = InAsn.Fun_GetNo(txtID.Text, "1", "", "");
                //txtLineId.Text = (GetMaxLineId(txtID.Text) + 1).ToString();
            }
            //ErpCode = Request.QueryString["ErpCode"];
            //InType = Request.QueryString["InType"];
            if (InType.Length > 0 && InType == "104")//44-104
            {
                this.txtCBATCH.Enabled = false;
                this.txtCERPCODELINE.Enabled = false;
                this.txtCINVBARCODE.Enabled = false;
                this.txtCINVCODE.Enabled = false;
                this.txtCINVNAME.Enabled = false;
                this.txtCPO.Enabled = false;
                // this.txtCSTATUS.Enabled = false;
                this.txtID.Enabled = false;
                this.txtIDS.Enabled = false;
                this.txtIPOLINE.Enabled = false;
                this.txtCINVCODE.Attributes.Remove("onclick");
            }
        }

        RegisterClientScript();

        if (CINVNAME.Length == 0)
        {
            IGenericRepository<BASE_PART> entity = new GenericRepository<BASE_PART>(context);
            /*
            var caseList = from p in entity.Get()
                           where p.cpartnumber == this.txtCINVCODE.Text
                           select p;
            */
            //Note by Qamar 2020-10-28
            string CINVCODE = txtCINVCODE.Text.Trim();
            if (txtRANK_FINAL.Text.Trim() == "")
                CINVCODE += "-_";
            else
                CINVCODE += "-" + txtRANK_FINAL.Text.Trim().ToUpper();
            var caseList = from p in entity.Get()
                           where p.cpartnumber == CINVCODE
                           select p;

            if (caseList.Count() > 0)
            {
                string t1 = caseList.ToList().FirstOrDefault<BASE_PART>().cpartname;
                this.txtCINVNAME.Text = caseList.ToList().FirstOrDefault<BASE_PART>().cpartname;
                CINVNAME = this.txtCINVNAME.Text.Trim();
            }
        }
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
    }

    public string ErpCode
    {
        set { ViewState["ErpCode"] = value; }
        get
        {
            if (ViewState["ErpCode"] != null)
            {
                return ViewState["ErpCode"].ToString().Trim();
            }
            return "";
        }
    }

    public string InType
    {
        set { ViewState["InType"] = value; }
        get
        {
            if (ViewState["InType"] != null)
            {
                return ViewState["InType"].ToString();
            }
            return "";
        }
    }

    public void LoadIDS()
    {
        IGenericRepository<INASN> con = new GenericRepository<INASN>(context);
        var caseList = from p in con.Get()
                       where p.id == this.txtID.Text.Trim()
                       select p;
        INASN entity = caseList.ToList().FirstOrDefault<INASN>();
        if (caseList.Count() > 0)
        {
            InType = entity.itype.ToString();
            ErpCode = entity.cerpcode;
            hfIsAll.Value = entity.idefine5.ToString();

            this.ShowPARTDiv1.SetTypeCode.Add("intype".ToLower(), InType);
            this.ShowPARTDiv1.SetTypeCode.Add("ErpCode".ToLower(), ErpCode);
            this.ShowPARTDiv1.SetTypeCode.Add("IsAll".ToLower(), entity.idefine5.ToString());
            hfInType.Value = InType;
            hfErpCode.Value = ErpCode;
            this.ShowReTurnDiv1.SetTypeCode.Add("intype".ToLower(), InType);
            this.ShowReTurnDiv1.SetTypeCode.Add("ErpCode".ToLower(), ErpCode);
            if (this.Operation() == SYSOperation.New)
            {
                //采购单收货新增明细
                if (InType == "101")
                {
                    txtCPO.Text = entity.cpo.Trim();
                    txtCPO.Enabled = false;
                    txtIPOLINE.Text = InAsn.Fun_GetNo(txtID.Text, "2", "", "");
                }
            }
        }
    }

    /// <summary>
    /// 向页面注册脚本
    /// </summary>
    private void RegisterClientScript()
    {
        //销货退回15-102,工单退料103 工单超领退105
        if (InType.Equals("102") || InType.Equals("103") || InType.Equals("105"))
        {
            ltSearch.Text = @"<img alt='' onclick=""disponse_div(event,document.all('ctl00_ContentPlaceHolderMain_ShowReTurnDiv1_ajaxWebSearChComp'));""
                                    src=""../../Images/Search.gif"" class=""select"" />";
        }
        else
        {
            ltSearch.Text = @"<img alt="""" onclick=""disponse_div(event,document.all('ctl00_ContentPlaceHolderMain_ShowPARTDiv1_ajaxWebSearChComp'));""
                                    src=""../../Images/Search.gif"" class=""select"" />";

            //            string script = @"<script type=""text/javascript"">
            //                                $(function () {
            //                                    $(""#" + this.txtCINVCODE.ClientID + @""").autocomplete({
            //                                        source: function (request, response) {
            //                                            //alert(request.term);
            //                                            $.ajax({
            //                                                url: ""../Server/Part.ashx?partNumber="" + request.term + ""&intype=" + InType.Trim() + @"&erpcode=" + ErpCode.Trim() + @"&Asn_type=INASN_D"",
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
            //            Page.ClientScript.RegisterClientScriptBlock(GetType(), txtCINVCODE.ClientID, script);
        }

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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INASN_D');return false;";
        //this.txtCINVCODE.Attributes["onclick"] = "Show('" + ShowPARTDiv1.GetDivName + "');";
        //this.txtCINVNAME.Attributes["onclick"] = "Show('" + ShowPARTDiv1.GetDivName + "');";
        //this.txtCINVBARCODE.Attributes["onclick"] = "Show('" + ShowPARTDiv1.GetDivName + "');";
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "ASN_D_STATUS", false, -1, -1), this.DpdStatus, "", "FLAG_NAME", "FLAG_ID", "");

        this.txtID.Text = Request.QueryString["IDS"];
        //删除确认提示
        if (this.Operation() == SYSOperation.Modify)
        {
            //要删除
            this.btnDelete.Attributes["onclick"] = "var userNo = '" + this.KeyID + "'; return window.confirm('<%$ Resources:Lang, FrmINBILL_DEdit_MSG30 %>' + userNo + '?');";
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
            this.btnSave.Text = Resources.Lang.FrmINBILLEdit_MSG42;//"审批";
        }

        ConFigvalue = this.GetConFig("000002");

        #region Disable and ReadOnly
        #endregion Disable and ReadOnly

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

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<INASN_D> con = new GenericRepository<INASN_D>(context);
        var caseList = from p in con.Get()
                       where p.ids == this.KeyID
                       select p;
        INASN_D entity = caseList.ToList().FirstOrDefault<INASN_D>();
        //txtLineId.Text = entity.LineID.HasValue ? entity.LineID.Value.ToString(): "";
        this.txtIDS.Text = entity.ids.ToString();
        this.txtID.Text = entity.id.ToString();
        //this.txtCSTATUS.Text = entity.CSTATUS;
        DpdStatus.SelectedValue = entity.cstatus;
        //Note by Qamar 2020-10-28
        this.txtCINVCODE.Text = entity.cinvcode.Substring(0, entity.cinvcode.Length - 2);
        this.txtRANK_FINAL.Text = entity.cinvcode.Substring(entity.cinvcode.Length - 1, 1);
        if (txtRANK_FINAL.Text.Trim() == "_")
            txtRANK_FINAL.Text = "";
        string partnumber = entity.cinvcode;

        this.txtCINVNAME.Text = entity.cinvname;
        this.txtIQUANTITY.Text = entity.iquantity != null ? Convert.ToDecimal(entity.iquantity).ToString("f2") : "0.00";
        this.txtCINVBARCODE.Text = entity.cinvbarcode;
        this.txtCBATCH.Text = entity.cbatch;
        this.txtCMEMO.Text = entity.cmemo;
        this.txtCERPCODELINE.Text = entity.cerpcodeline;
        //this.erpline_code.Value = entity.CERPCODELINE;

        //txtIQUANTITY1.Value = entity.IQUANTITY.ToString();
        this.txtCPO.Text = entity.cpo;
        this.txtIPOLINE.Text = entity.ipoline.ToString();
        txtDateCode.Text = entity.datecode.ToString();

        IGenericRepository<INASN> icon = new GenericRepository<INASN>(context);
        var icaseList = from p in icon.Get()
                        where p.id == entity.id
                        select p;
        INASN iEntity = icaseList.ToList().FirstOrDefault<INASN>();
        if (icaseList.Count() > 0)
        {
            ErpCode = iEntity.cerpcode;
            InType = iEntity.itype.ToString();
        }
        hfWorkType.Value = "1";
        hfOriginalQty.Value = entity.iquantity.ToString();
        CINVNAME = entity.cinvname;

        txtCERPCODELINE.Enabled = false;
        if (iEntity.ddefine4 == "1")
        {
            txtCPO.Enabled = false;
            txtIPOLINE.Enabled = false;
        }

        txtDateCode.Enabled = false;

        //显示物料规格
        if (txtcspecifications.Text.Length == 0)
        {
            IGenericRepository<BASE_PART> icon_p = new GenericRepository<BASE_PART>(context);
            /*
            var icaseList1 = from p in icon_p.Get()
                             where p.cpartnumber == txtCINVCODE.Text.Trim()
                             select p;
            if (icaseList.Count() > 0)
                txtcspecifications.Text = icaseList1.ToList().FirstOrDefault<BASE_PART>().cspecifications;
            */
            //25-10-2020 by Qamar
            var icaseList1 = from p in icon_p.Get()
                             where p.cpartnumber == partnumber
                             select p;
            if (icaseList.Count() > 0)
            {
                try
                {
                    txtcspecifications.Text = icaseList1.ToList().FirstOrDefault<BASE_PART>().cspecifications;
                }
                catch { }
            }
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        string msg = string.Empty;

        #region 基础信息

        if (CINVNAME == null || CINVNAME.Length == 0)
        {
            IGenericRepository<BASE_PART> icon = new GenericRepository<BASE_PART>(context);
            var icaseList = from p in icon.Get()
                            where p.cpartnumber == txtCINVCODE.Text.Trim()
                            select p;
            if (icaseList.Count() > 0)
                CINVNAME = icaseList.ToList().FirstOrDefault<BASE_PART>().cpartname;
        }
        //显示物料规格
        if (txtcspecifications.Text.Length == 0)
        {
            IGenericRepository<BASE_PART> icon = new GenericRepository<BASE_PART>(context);
            var icaseList = from p in icon.Get()
                            where p.cpartnumber == txtCINVCODE.Text.Trim()
                            select p;
            if (icaseList.Count() > 0)
                txtcspecifications.Text = icaseList.ToList().FirstOrDefault<BASE_PART>().cspecifications;
        }
        this.txtCINVNAME.Text = CINVNAME;
        if (this.txtID.Text.Trim() == "")
        {
            //主表编号项不允许空
            this.Alert(Resources.Lang.FrmINASN_DEdit_MSG6 + "！");
            this.SetFocus(txtID);
            return false;
        }

        //
        if (this.txtCINVCODE.Text.Trim() == "")
        {
            //料号项不允许空
            this.Alert(Resources.Lang.FrmINASN_DEdit_MSG7 + "！");
            this.SetFocus(txtCINVCODE);
            return false;
        }
        //
        if (this.txtCINVCODE.Text.Trim().Length > 0)
        {
            if (this.txtCINVCODE.Text.GetLengthByByte() > 200)
            {
                //料号项超过指定的长度50
                this.Alert(Resources.Lang.FrmINASN_DEdit_MSG8 + "！");
                this.SetFocus(txtCINVCODE);
                return false;
            }
        }
        if (this.txtIQUANTITY.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmINASN_DEdit_MSG9 + "！");
            this.SetFocus(txtIQUANTITY);
            return false;
        }
        //
        if (this.txtIQUANTITY.Text.Trim().Length > 0)
        {
            if (StringExtension.IsValidNum(this.txtIQUANTITY.Text))
            {
                this.Alert("数量项不是有效的十进制数字！");
                this.SetFocus(txtIQUANTITY);
                return false;
            }
            if (Convert.ToDecimal(txtIQUANTITY.Text) > Convert.ToDecimal(9999999999))
            {
                //输入数量不能大于9999999999
                this.Alert(Resources.Lang.FrmINASN_DEdit_MSG10 + "!");
                this.SetFocus(txtIQUANTITY);
                return false;
            }


            //检查数量是否正确
            string errmsg = string.Empty;
            if (!(Comm_Fun.Fun_IsDecimal(txtIQUANTITY.Text.Trim(), 0, 0, 1, out errmsg)))
            {
                this.Alert(errmsg);
                this.SetFocus(txtIQUANTITY);
                return false;
            }


            // WL 20160525 注释
            //20130520092312    判断是否存在小数点问题
            //if (txtIQUANTITY.Text.Trim().ToDecimal() - Math.Truncate(txtIQUANTITY.Text.Trim().ToDecimal()) > 0)
            //{
            //    this.Alert("数量项只能为正整数，不能包含小数部分！");
            //    this.SetFocus(txtIQUANTITY);
            //    return false;
            //}
        }
        //if (Convert.ToDecimal(this.txtIQUANTITY.Text.Trim()) <= 0)
        //{
        //    this.Alert("数量要大于0！");
        //    this.SetFocus(txtIQUANTITY);
        //    return false;
        //}
        //
        if (this.txtCINVBARCODE.Text.Trim().Length > 0)
        {
            if (this.txtCINVBARCODE.Text.GetLengthByByte() > 200)
            {
                //料号项超过指定的长度50
                this.Alert(Resources.Lang.FrmINASN_DEdit_MSG8 + "！");
                this.SetFocus(txtCINVBARCODE);
                return false;
            }
        }

        //if (this.Operation() == SYSOperation.New)
        //{
        //    IGenericRepository<INASN_D> icon = new GenericRepository<INASN_D>(context);
        //    var caseList = from p in icon.Get()
        //                    where p.id == txtID.Text.Trim() && p.cinvcode == txtCINVCODE.Text.Trim().ToUpper()
        //                    select p;
        //    //检查入库通知单是否存在相同的料号
        //    if (caseList.Count()>0)
        //    {
        //        this.Alert("入库通知单中已存在该料号,不能保存！");
        //        this.SetFocus(txtCINVBARCODE);
        //        return false;
        //    }
        //}

        if (ConFigvalue == "1")
        {
            //
            if (this.txtCBATCH.Text.Trim().Length > 0)
            {
                //检查批次号格式
                msg = "";
                if (!(Comm_Fun.CheckFun_GetBatchNo(this.txtCBATCH.Text.Trim(), "", out msg)))
                {
                    Alert(msg);
                    this.SetFocus(txtCBATCH);
                    return false;
                }
            }
        }
        else
        {
            //
            if (this.txtCBATCH.Text.Trim().Length > 0)
            {
                if (this.txtCBATCH.Text.GetLengthByByte() > 20)
                {
                    //批次号项超过指定的长度20
                    this.Alert(Resources.Lang.FrmINASN_DEdit_MSG11 + "！");
                    this.SetFocus(txtCBATCH);
                    return false;
                }
            }
        }
        if (this.txtDateCode.Text.Trim().Length > 0)
        {
            if (this.txtDateCode.Text.GetLengthByByte() != 6)
            {
                //DateCode 必须为6位
                this.Alert(Resources.Lang.FrmINASN_DEdit_MSG12 + "！");
                this.SetFocus(txtDateCode);
                return false;
            }
            else
            {
                //检查日期格式
                if (!(Comm_Fun.Check_Is_Date(txtDateCode.Text.Trim())))
                {
                    //DateCode必须是年月日[YYMMDD]格式类型
                    this.Alert(Resources.Lang.FrmINASN_DEdit_MSG13 + "！");
                    this.SetFocus(txtDateCode);
                    return false;
                }
            }
        }

        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 20)
            {
                //备注项超过指定的长度20
                this.Alert(Resources.Lang.FrmINASN_DEdit_MSG14 + "！");
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        //
        if (this.txtCERPCODELINE.Text.Trim().Length > 0)
        {
            if (this.txtCERPCODELINE.Text.GetLengthByByte() > 20)
            {
                //ERP单号项超过指定的长度20
                this.Alert(Resources.Lang.FrmINASN_DEdit_MSG15 + "！");
                this.SetFocus(txtCERPCODELINE);
                return false;
            }
        }
        //
        var po = this.txtCPO.Text;
        if (po.Trim().Length > 0)
        {
            if (po.GetLengthByByte() > 30)
            {
                //po号项超过指定的长度30
                this.Alert(Resources.Lang.FrmINASN_DEdit_MSG16 + "！");
                this.SetFocus(txtCPO);
                return false;
            }


            if (Regex.IsMatch(po, "^[A-Za-z0-9]+$") == false)
            {
                //po格式不正确
                this.Alert(Resources.Lang.FrmINASN_DEdit_MSG17 + "！");
                this.SetFocus(txtCPO);
                return false;
            }
        }
        //
        if (this.txtIPOLINE.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDecimal(this.txtIPOLINE.Text) == false)
            {
                //PO项次项不是有效的十进制数字
                this.Alert(Resources.Lang.FrmINASN_DEdit_MSG18 + "！");
                this.SetFocus(txtIPOLINE);
                return false;
            }
        }

        #endregion

        string workType = "0";
        if (this.Operation() == SYSOperation.Modify)
        {
            workType = "1";
        }
        if (InType.Length > 0)
        {
            #region Wip Negative Issue 38 工单负发料
            if (InType.Equals("38"))//Wip Negative Issue 38 工单负发料
            {
                //料号检查
                msg = InAsn.CheckWIP_Part(txtID.Text, txtCINVCODE.Text);
                if (!msg.Equals("OK"))
                {
                    this.Alert(msg);
                    this.SetFocus(txtCINVCODE);
                    return false;
                }
                msg = InAsn.CheckWipNegativeIssueQty(txtID.Text.Trim(), txtCINVCODE.Text.Trim(), txtIQUANTITY.Text.Trim(), hfWorkType.Value, hfOriginalQty.Value);
                if (!msg.Equals("OK"))
                {
                    this.Alert(msg);
                    this.SetFocus(txtIQUANTITY);
                    return false;
                }
            }
            #endregion
            //Note by Qamar 2020-10-25
            //料号检查
            if (txtRANK_FINAL.Text.Trim() == "")
                msg = InAsn.CheckWIP_Part(txtID.Text, txtCINVCODE.Text + "-_");
            else
                msg = InAsn.CheckWIP_Part(txtID.Text, txtCINVCODE.Text + "-" + txtRANK_FINAL.Text.Trim().ToUpper());

            if (!msg.Equals("OK"))
            {
                this.Alert(msg);
                this.SetFocus(txtCINVCODE);
                return false;
            }
            //if (this.erpline_code.Value.Trim() == "")
            //{
            //    txtCERPCODELINE.Text = Comm_Sys.Fun_GetNo(txtID.Text, "1", "", "");
            //}
            //else
            //{
            //    txtCERPCODELINE.Text = erpline_code.Value.Trim();
            //}

            if (!InAsn.ValidateAsn_D_IsExist(txtID.Text.Trim(), txtCINVCODE.Text.Trim(), txtCERPCODELINE.Text.Trim(), txtIDS.Text.Trim()))
            {
                //相同料号、相同子ERPcode（或空）只能添加一条
                this.Alert(Resources.Lang.FrmINASN_DEdit_MSG19 + "！");
                this.SetFocus(txtCERPCODELINE);
                return false;
            }
            #region 101采购单收货
            if (InType.Equals("101"))
            {
                if (this.txtCPO.Text.Trim() == "")
                {
                    //采购单收货PO/SO号不能为空
                    this.Alert(Resources.Lang.FrmINASN_DEdit_MSG20 + "！");
                    this.SetFocus(txtCPO);
                    return false;
                }
                //
                if (this.txtIPOLINE.Text.Trim() == "")
                {
                    //PO项次项不是有效的十进制数字
                    this.Alert(Resources.Lang.FrmINASN_DEdit_MSG21 + "！");
                    this.SetFocus(txtIPOLINE);
                    return false;
                }
            }
            #endregion


            #region 102-15 销货退回 RMA Receipt
            if (InType.Equals("102"))//验证RMA Receipt的数量是否有效
            {
                if (txtCERPCODELINE.Text.Length == 0)
                {
                    //ErpCode 不能为空
                    this.Alert(Resources.Lang.FrmINASN_DEdit_MSG22 + "！");
                    this.SetFocus(txtCERPCODELINE);
                    return false;
                }
                //料号检查
                msg = InAsn.CheckWIP_Part(txtID.Text, txtCINVCODE.Text);
                if (!msg.Equals("OK"))
                {
                    this.Alert(msg);
                    this.SetFocus(txtCINVCODE);
                    return false;
                }
                //检查销货退回数量是否正确
                msg = InAsn.Fun_CheckRMA_ReceiptQty(txtID.Text.Trim(), txtCINVCODE.Text.Trim(), txtIQUANTITY.Text.Trim(), workType, hfOriginalQty.Value, txtCERPCODELINE.Text);
                if (!msg.Equals("OK"))
                {
                    this.Alert(msg);
                    this.SetFocus(txtIQUANTITY);
                    return false;
                }
            }
            #endregion

            #region 103-43 工单退料
            if (InType.Equals("103"))//WIP Return : 43 工单退料数量验证
            {
                //不是其他用料 为 0
                if (hfIsAll.Value.Equals("0"))
                {
                    //料号检查
                    msg = InAsn.CheckWIP_Part(txtID.Text, txtCINVCODE.Text);
                    if (!msg.Equals("OK"))
                    {
                        this.Alert(msg);
                        this.SetFocus(txtCINVCODE);
                        return false;
                    }
                    msg = InAsn.ValidateCheckWipReturnQty(txtID.Text.Trim(), txtCINVCODE.Text.Trim(), txtIQUANTITY.Text.Trim(), hfWorkType.Value, hfOriginalQty.Value);
                    if (!msg.Equals("OK"))
                    {
                        this.Alert(msg);
                        this.SetFocus(txtIQUANTITY);
                        return false;
                    }
                }//2013-3-22 12:27:37 陆健提出特殊元件退料不控制退的数量
                else
                {
                    //料号检查
                    msg = InAsn.CheckWIP_Part(txtID.Text, txtCINVCODE.Text);
                    if (!msg.Equals("OK"))
                    {
                        this.Alert(msg);
                        this.SetFocus(txtCINVCODE);
                        return false;
                    }
                    //msg = INASN_DRule.CheckWipReturnSpeciaQty(txtID.Text.Trim(), txtCINVCODE.Text.Trim(), txtIQUANTITY.Text.Trim(), hfWorkType.Value, hfOriginalQty.Value);
                    //if (!msg.Equals("OK"))
                    //{
                    //    this.Alert(msg);
                    //    this.SetFocus(txtIQUANTITY);
                    //    return false;
                    //}
                }
            }
            #endregion

            #region 104-44完工入库
            if (InType.Equals("104"))//WIP Completion :44 工单完工入库
            {
                if (ErpCode.Length > 0)
                {
                    //工单完工数量检查
                    msg = InAsn.CheckWIPCompletionQty(txtIDS.Text.Trim(), Convert.ToDecimal(txtIQUANTITY.Text.Trim()));
                    if (!msg.Equals("OK"))
                    {
                        this.Alert(msg);
                        this.SetFocus(txtIQUANTITY);
                        return false;
                    }
                }
                if (!InAsn.FUN_CHECKCOMPLETESTATUS(ErpCode, Convert.ToDecimal(txtIQUANTITY.Text.Trim()), ref msg))
                {
                    this.Alert(msg);
                    this.SetFocus(txtIQUANTITY);
                    return false;
                }
            }
            #endregion

            #region 注销
            //工单超领料号检查
            //if (new OUT_FrmOUTTYPEListQuery().ValidateOutTypeIsWipIssue(InType.Trim()))
            //{
            //    if (!new BASE_FrmBASE_PARTListQuery().CheckWIP_CL_PARTINFO(txtCINVCODE.Text.Trim()))
            //    {
            //        this.Alert("当前料号不能超领!");
            //        this.SetFocus(txtCINVCODE);
            //        return false;
            //    }
            //    //料号检查
            //    msg = BASE_PARTRule.CheckWIP_Part(txtID.Text, txtCINVCODE.Text);
            //    if (!msg.Equals("OK"))
            //    {
            //        this.Alert(msg);
            //        this.SetFocus(txtCINVCODE);
            //        return false;
            //    }
            //} 
            #endregion

            #region 105工单超领退
            //  工单超领退只能退工单超领相同工单号下的料 ,量不能超过超领的数量
            if (InType.Equals("105"))
            {
                //料号检查
                msg = InAsn.CheckWIP_Part(txtID.Text, txtCINVCODE.Text);
                if (!msg.Equals("OK"))
                {
                    this.Alert(msg);
                    this.SetFocus(txtCINVCODE);
                    return false;
                }
                //工单超领退数量检查
                msg = InAsn.CheckWip_CLT_Qty(txtID.Text.Trim(), txtCINVCODE.Text.Trim(), txtIQUANTITY.Text.Trim(), hfWorkType.Value, hfOriginalQty.Value);
                if (!msg.Equals("OK"))
                {
                    this.Alert(msg);
                    this.SetFocus(txtIQUANTITY);
                    return false;
                }
            }
            #endregion
        }

        msg = InAsn.GetInAsnStatusByInAsnId(txtID.Text.Trim());
        if (!msg.Equals("0"))
        {
            this.Alert(Resources.Lang.FrmINASN_DEdit_MSG23 + "!");
            this.SetFocus(txtCINVCODE);
            return false;
        }


        #region 工单检查
        ////20130513135051 增加判断主表erpcode与子表输入erpcode是否一致问题
        //var NoticesOfMo = INASN_DRule.IsMoNotice(InType.Trim());
        //if (NoticesOfMo)//工单
        //{
        //    //20130515140920
        //    if (string.IsNullOrEmpty(txtCERPCODELINE.Text.Trim()))
        //    {
        //        Alert("子项ErpCode为空!");
        //        return false;
        //    }

        //    var MainErpCode = INASN_DRule.GetMainErpCode(txtID.Text.Trim());
        //    if (!MainErpCode.Trim().Equals(txtCERPCODELINE.Text.Trim()))//主表erpcode与子表输入erpcode不一致，报错
        //    {
        //        Alert("子项ErpCode[" + txtCERPCODELINE.Text.Trim() + "]与主表ErpCode[" + MainErpCode.Trim() + "]不一致!");
        //        return false;
        //    }
        //}

        #endregion

        //20130731134649
        var result = InAsn.CanModDebit(txtCERPCODELINE.Text.Trim(), txtCINVCODE.Text.Trim(), "0", "", InType.Trim(), "");
        if (!result.Equals("1"))
        {
            Alert(result);
            return false;
        }

        return true;
    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public INASN_D SendData(IGenericRepository<INASN_D> con)
    {
        INASN_D entity = null;
        if (this.Operation() == SYSOperation.New)
        {
            entity = new INASN_D();
        }
        else if (this.Operation() == SYSOperation.Modify)
        {
            entity = (from p in con.Get()
                      where p.ids == txtIDS.Text.Trim()
                      select p).FirstOrDefault<INASN_D>();
        }
        //
        this.txtIDS.Text = this.txtIDS.Text.Trim();
        if (this.txtIDS.Text.Length > 0)
        {
            entity.ids = txtIDS.Text;
        }
        else
        {
            entity.ids = string.Empty;
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
            entity.id = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.ID = null;
        }
        //entity.LineID = int.Parse(txtLineId.Text);
        //txtLineId.Text = (GetMaxLineId(txtID.Text) + 1).ToString();
        //
        //this.txtCSTATUS.Text = this.txtCSTATUS.Text.Trim();
        if (DpdStatus.SelectedValue.Length > 0)
        {
            entity.cstatus = DpdStatus.SelectedValue;
        }
        else
        {
            entity.cstatus = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSTATUS = null;
        }
        //
        this.txtCINVCODE.Text = this.txtCINVCODE.Text.Trim();
        if (this.txtCINVCODE.Text.Length > 0)
        {
            //Note by Qamar 2020-10-25
            if (txtRANK_FINAL.Text.Trim().Length == 1)
            {
                entity.cinvcode = txtCINVCODE.Text + "-" + txtRANK_FINAL.Text.Trim().ToUpper();
            }
            else
            {
                entity.cinvcode = txtCINVCODE.Text + "-_";
            }

            IGenericRepository<BASE_PART> Base_Part = new GenericRepository<BASE_PART>(context);
            var caseList = from p in Base_Part.Get()
                           where p.cpartnumber == entity.cinvcode
                           select p;
            if (caseList.Count() > 0)
                entity.cinvname = caseList.ToList().FirstOrDefault<BASE_PART>().cpartname; ;
        }
        else
        {
            entity.cinvcode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CINVCODE = null;
        }
        //
        //this.txtCINVNAME.Text = this.txtCINVNAME.Text.Trim();
        //if(this.txtCINVNAME.Text.Length > 0)
        //{

        //}
        //else
        //{
        //    entity.SetDBNull("CINVNAME",true);
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
            entity.iquantity = 0;
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
            entity.cinvbarcode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CINVBARCODE = null;
        }
        //
        this.txtCBATCH.Text = this.txtCBATCH.Text.Trim();
        if (this.txtCBATCH.Text.Length > 0)
        {
            entity.cbatch = txtCBATCH.Text;
        }
        else
        {
            entity.cbatch = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CBATCH = null;
        }
        //CQ 2014-10-31 09:47:00
        this.txtDateCode.Text = this.txtDateCode.Text.Trim();
        if (this.txtDateCode.Text.Length > 0)
        {
            entity.datecode = Convert.ToInt32(txtDateCode.Text);
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CINVCODE = null;
        }
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        else
        {
            entity.cmemo = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CMEMO = null;
        }
        //
        this.txtCERPCODELINE.Text = this.txtCERPCODELINE.Text.Trim();
        if (this.txtCERPCODELINE.Text.Length > 0)
        {
            entity.cerpcodeline = txtCERPCODELINE.Text;
        }
        else
        {
            entity.cerpcodeline = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CERPCODELINE = null;
        }
        //
        this.txtCPO.Text = this.txtCPO.Text.Trim();
        if (this.txtCPO.Text.Length > 0)
        {
            entity.cpo = txtCPO.Text;
        }
        else
        {
            entity.cpo = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CPO = null;
        }
        //
        //this.txtIPOLINE.Text = this.txtIPOLINE.Text.Trim();
        //if (this.txtIPOLINE.Text.Length > 0)
        //{
        //    entity.ipoline = txtIPOLINE.Text.ToDecimal();
        //}
        //else
        //{

        //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        //    //entity.IPOLINE = null;
        //}

        #region 界面上不可见的字段项
        /*
        entity.CDEFINE1 = ;
        entity.CDEFINE2 = ;
        entity.DDEFINE3 = ;
        entity.DDEFINE4 = ;
        entity.IDEFINE5 = ;
        */
        #endregion
        return entity;

    }

    //判断是否允许整盘特退
    [WebMethod]
    public static bool CanSpecialReturn(string cinvcode, string AsnId)
    {
        return InAsn.IsCanSpecialReturn(cinvcode, AsnId); ;
    }

    /// <summary>
    /// 获取最大的项次编号
    /// </summary>
    /// <param name="asnId"></param>
    /// <returns></returns>
    private int GetMaxLineId(string asnId)
    {
        int lineId = 0;
        IGenericRepository<INASN_D> conn = new GenericRepository<INASN_D>(context);
        var inasnd = (from p in conn.Get()
                      where p.id == asnId
                      orderby p.LineID descending
                      select p).FirstOrDefault();
        if (inasnd != null)
        {
            lineId = Convert.ToInt32(inasnd.LineID);
        }
        return lineId;
    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Note by Qamar 2020-10-25
        txtRANK_FINAL.Text = txtRANK_FINAL.Text.Trim().ToUpper();
        if (txtRANK_FINAL.Text.Length <= 1)
        {
            IGenericRepository<INASN_D> con = new GenericRepository<INASN_D>(context);
            this.btnSave.Enabled = false; //CQ 2013-5-13 13:47:51
            if (this.CheckData())
            {
                INASN_D entity = (INASN_D)this.SendData(con);
                if (this.Operation() != SYSOperation.New)
                {
                }
                string strKeyID = "";
                //strKeyID += entity.IDS.ToString();
                try
                {
                    if (this.Operation() == SYSOperation.Modify)
                    {
                        entity.ids = txtIDS.Text.Trim();
                        strKeyID = entity.ids;
                        con.Update(entity);
                        con.Save();
                        this.AlertAndBack("FrmINASN_DEdit.aspx?IDS=" + this.txtID.Text.Trim() + "&" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Msg_SaveSuccess);//保存成功
                    }
                    else if (this.Operation() == SYSOperation.New)
                    {
                        entity.LineID = GetMaxLineId(txtID.Text) + 1;
                        entity.ids = Guid.NewGuid().ToString();
                        strKeyID = entity.ids;
                        con.Insert(entity);
                        con.Save();
                        this.AlertAndBack("FrmINASN_DEdit.aspx?IDS=" + this.txtID.Text.Trim() + "&" + BuildQueryString(SYSOperation.New, strKeyID), Resources.Lang.WMS_Common_Msg_SaveSuccess);//保存成功
                    }
                    //this.WriteScript("alert('保存成功');window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INASN_D');");
                }
                catch (Exception E)
                {
                    DBLog.Log(E.Message.ToString());
                    //失败
                    this.Alert(this.GetOperationName() + Resources.Lang.FrmINBILLEdit_MSG54 + "！" + E.Message);
#if Debug
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
                    this.btnSave.Enabled = true;
                }
            }
        }
        else
        {
            //Note by Qamar 2020-10-19
            this.Alert("批/序號(RANK)有誤");
        }
        this.btnSave.Enabled = true;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<INASN_D> con = new GenericRepository<INASN_D>(context);
        try
        {
            con.Delete(this.KeyID.ToString());
            con.Save();
        }
        catch (Exception E)
        {
            //删除失败
            this.Alert(Resources.Lang.FrmINBILLEdit_MSG56 + "！" + E.Message);
#if Debug 
            this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
        }

    }
    protected void btnLoadCount_Click(object sender, EventArgs e)
    {
        decimal iquantity = 0;
        decimal wipiquantity = 0;
        if (InType == "38")
        {
            //通过料号和ERP获取数量
            iquantity = new InAsn().GetInQtyByCinvcode(this.txtCINVCODE.Text, ErpCode);
            wipiquantity = new InAsn().GetWipnegativeissue_tempByCinvcode(this.txtCINVCODE.Text, ErpCode);
            this.txtIQUANTITY.Text = (wipiquantity - iquantity).ToString();

        }
        if (InType == "103")
        {
            //通过料号和ERP获取数量
            iquantity = new InAsn().GetInQtyByCinvcodeOut(this.txtCINVCODE.Text, ErpCode);
            wipiquantity = new InAsn().GetOutQtyByCinvcode(this.txtCINVCODE.Text, ErpCode);
            this.txtIQUANTITY.Text = (wipiquantity - iquantity).ToString();
        }
    }
    #endregion

}

