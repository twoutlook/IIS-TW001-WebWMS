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
using System.Linq.Dynamic;
using DreamTek.ExternalService.NCS;
using DreamTek.WMS.DAL.Model.Base;
/*
Roger
2013/7/2 8:44:29
20130702084429
解决狂点按钮重复提交问题
*/
/*
Roger
2013/7/31 13:46:49
20130731134649
增加校验是否为正在修改中的料号
*/
/*
Roger
2013/8/22 10:06:12
20130822100612
更新通知单状态
*/
/// <summary>
/// 描述: 入库管理-->FrmINASNEdit 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:40:51
/// </summary>
public partial class RD_FrmINASNEdit :WMSBasePage// PageBase, IPageEdit
{
    #region SQL
    //DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowVENDORDiv1.SetCompName = this.txtCVENDER.ClientID;
        ShowVENDORDiv1.SetORGCode = this.txtCVENDERCODE.ClientID;
        if (this.IsPostBack == false)
        {
            this.cboIsAll.Enabled = false;
            this.InitPage();

            

            if (this.Operation() == SYSOperation.Modify)
            {
                ShowData(this.KeyID);

                IGenericRepository<V_Inasn_D_Edit> entity = new GenericRepository<V_Inasn_D_Edit>(context);
                int caseCount = (from p in entity.Get()
                               where p.id==this.KeyID && p.cstatus!="0"
                               select p).Count();


                //编辑不可修改类型
                if (this.ddlWorkType.SelectedValue == "0" //|| INASN_type.Is_Query==1//this.txtITYPE.SelectedItem.Text == "拼板入库" 
                    || caseCount>0)
                {
                    this.ButtonUnion.Visible = false;
                }
                txtITYPE.Enabled = false;
                this.ddlWorkType.Enabled = false;
            }
            else if (this.Operation() == SYSOperation.Preserved1)
            {
                ShowData(this.KeyID);
                this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASN_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID + "','" + Resources.Lang.FrmINASN_DEdit_Content1 + "','INASN_D',1000,500);");
            }
            else
            {
                ButtonUnion.Visible = false;
                btnCreateInBill.Enabled = false;
                this.txtCCREATEOWNERCODE.Text =OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
                txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtID.Text = Guid.NewGuid().ToString();
                //this.txtInAsnCTICKETCODE.Text = new Fun_CreateNo().CreateNo("INASN");
                //雪龙没有平库，所以锁定立库，并且不能编辑
                ddlWorkType.SelectedValue = "1";
                this.ddlWorkType.Enabled = false;

                //Note by Qamar 2020-11-11
                //台惟鎖定"其他入庫", 並且不能編輯
                txtITYPE.SelectedValue = "1110";
                txtITYPE_SelectedIndexChanged(txtITYPE, null);
                txtITYPE.Enabled = false;

                //Note by Qamar 2020-12-07
                //台惟預設"外購件收料(RA)"
                ddlREASONCODE.SelectedIndex = 4;
            }
            //接收 参数 :&IsSpecialPage=1 &IsSpecialWipReturn
            if (IsSpecialWIP_Return == 1)
            {
                ltPageTable.Text = Resources.Lang.FrmINASNEdit_MSG2;//"特殊元件退料";
                //this.txtITYPE.SelectedValue = "43";//WIP Return 工单退料
                //this.txtITYPE.Enabled = false;
                this.cboIsAll.Checked = true;
            }

            //检查入库通知单是否有明细
            if (InAsn.CheckInAsn_DCount(txtID.Text.Trim()))
            {
                txtITYPE.Enabled = false;
                txtCERPCODE.Enabled = false;
                this.ddlWorkType.Enabled = false;
            }
        }
        if (!string.IsNullOrEmpty(this.KeyID))
        {
            INTYPE INASN_type = GetINTYPEByID(this.KeyID, "INASN");
            if (INASN_type.Is_Query == 1)
            {
                this.btnCreateInBill.Enabled = false;
                this.ButtonUnion.Visible = false;
                this.btnCreateTemporary.Enabled = false;
            }
            SetMerageControlEnabled(this.KeyID);
        }
        IGenericRepository<IN_MERGE_PALLETE> cond = new GenericRepository<IN_MERGE_PALLETE>(context);


        this.btnCreateInBill.Attributes["onclick"] = this.GetPostBackEventReference(this.btnCreateInBill) + ";this.disabled=true;";
        this.btnCreateTemporary.Attributes["onclick"] = this.GetPostBackEventReference(this.btnCreateTemporary) + ";this.disabled=true;";
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中      
    }

    #region IPage 成员

    /// <summary>
    /// 初始化页面。主要做一下动作
    ///1、DropDownList,ListBox数据装载,
    ///2、新增按钮、删除的按钮的Java脚本注册等
    ///一般在PageLoad 事件调用，
    ///并且IsPostBack = false时
    /// </summary>
    public void InitPage()
    {
        try
        {
            if (IsSpecialWIP_Return == 0)
            {
                IsSpecialWIP_Return = Convert.ToDecimal(Request.QueryString["IsSpecialWIP_Return"]);
            }
        }
        catch (Exception)
        {

        }
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INASN');return false;";
        //this.grdINASN_D.DataKeyNames = new string[] { "IDS,InBill_Qty" };//
        this.txtCVENDER.Attributes["onclick"] = "Show('" + ShowVENDORDiv1.GetDivName + "');";
        this.txtCVENDERCODE.Attributes["onclick"] = "Show('" + ShowVENDORDiv1.GetDivName + "');";
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
      //  this.ButtonUnion.Attributes["onclick"] = "return PopupFloatWinMax('" + BuildRequestPageURL("FrmINASNEdit_UnionPallet.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID + "','','IN_MERGE_PALLETE',1000,900);";
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "IS", false, -1, -1), this.txtCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
        //FrmINASNEdit_MSG3->请选择类型
        //理由码
        // Help.DropDownListDataBind(GetReasonCode("1"), this.ddlREASONCODE, Resources.Lang.FrmINASNEdit_MSG3, "REASONCONTENT", "REASONCODE", "");
        if (this.Operation() == SYSOperation.New)
        {
            Help.DropDownListDataBind(GetReasonCode("1", false), this.ddlREASONCODE, Resources.Lang.FrmINASNEdit_MSG3, "REASONCONTENT", "REASONCODE", "");
        }
        else
        {
            //加上来源于ERP的理由码
            Help.DropDownListDataBind(GetReasonCode("1",true), this.ddlREASONCODE, Resources.Lang.FrmINASNEdit_MSG3, "REASONCONTENT", "REASONCODE", "");
        }      
        //Common_ALL-》"全部"
        Help.DropDownListDataBind(GetParametersByFlagType("WorkType"), this.ddlWorkType, Resources.Lang.Common_ALL, "flag_name", "flag_id", "");
        //判退 ddlDDEFINE3
        Help.DropDownListDataBind(GetParametersByFlagType("YorN"), this.ddlDDEFINE3, Resources.Lang.Common_ALL, "flag_name", "flag_id", "");
        if (this.Operation() == SYSOperation.New)
        {
            
            if (IsSpecialWIP_Return == 1)
            {
                //请选择类型
                Help.DropDownListDataBind(InAsn.GetInTypeBySpecialReturn2(false), this.txtITYPE, Resources.Lang.FrmINASNEdit_MSG3, "typename", "cerpcode", "");
            }
            else
            {
                //请选择类型
                Help.DropDownListDataBind(GetInType(false), this.txtITYPE, Resources.Lang.FrmINASNEdit_MSG3, "FUNCNAME", "EXTEND1", "");
            }
        }
        else
        {
            if (IsSpecialWIP_Return == 1)
            {
                //请选择类型
                Help.DropDownListDataBind(InAsn.GetInTypeBySpecialReturn2(false), this.txtITYPE, Resources.Lang.FrmINASNEdit_MSG3, "typename", "cerpcode", "");
            }
            else
            {
                //请选择类型
                Help.DropDownListDataBind(GetInType(true), this.txtITYPE, Resources.Lang.FrmINASNEdit_MSG3, "FUNCNAME", "EXTEND1", "");
            }
        }
        if (txtITYPE.Items.Count > 0)
        {
            switch (this.txtITYPE.Items[0].Value.Trim())
            {
                case "104"://WIP Completion : 44-104
                    if (this.Operation() == SYSOperation.New)
                    {
                        this.lblDCREATETIME.Visible = false;
                        this.txtDCREATETIME.Visible = false;
                    }
                    break;
                case "103"://WIP Return ：43
                    if (IsSpecialWIP_Return == 1)
                    {
                        this.cboIsAll.Checked = true;
                    }
                    this.lblDCREATETIME.Visible = true;
                    this.txtDCREATETIME.Visible = true;
                    break;
                default:
                    break;
            }
        }
        //删除确认提示
        if (this.Operation() == SYSOperation.Modify || this.Operation() == SYSOperation.Preserved1)
        {
            //要删除
            this.btnDelete0.Enabled = true;
        }
        else
        {
            this.btnDelete0.Enabled = false;
        }
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.Operation() == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.CommonB_Approve;// "审批";
        }


        #region Disable and ReadOnly
        #endregion Disable and ReadOnly

    }
    ///// <summary>
    ///// 获取入库理由码信息
    ///// </summary>
    ///// <param name="type"></param>
    ///// <returns></returns>
    //public DataTable GetReasonCode(string type, bool IsShowERP)
    //{
    //    string sql = string.Format(@"SELECT A.REASONCODE,s.FLAG_NAME AS REASONCONTENT FROM BASE_DOCUREASON A INNER JOIN dbo.V_SYS_PARAMETER s WITH(NOLOCK) ON s.FLAG_ID= A.REASONCODE AND s.FLAG_TYPE='BASE_DOCUREASON' AND s.LANGUAGEID='" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'  WHERE A.STATES = 'Y' AND A.ACTIONSCOPE = '{0}'", type);
    //    if (!IsShowERP)
    //    {
    //        //增删改
    //        sql += " and A.isfromerp!='1'";
    //    }
    //    return DBHelp.ExecuteToDataTable(sql);
    //}
    #endregion

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

    public string InType
    {
        get
        {
            if (ViewState["InType"] != null)
            {
                return ViewState["InType"].ToString();
            }
            return "";
        }
        set { ViewState["InType"] = value; }
    }

    //是否是特殊元件退料
    public decimal IsSpecialWIP_Return
    {
        get
        {
            if (ViewState["IsSpecialWIP_Return"] != null)
            {
                return Convert.ToDecimal(ViewState["IsSpecialWIP_Return"]);
            }
            return 0;
        }
        set { ViewState["IsSpecialWIP_Return"] = value; }
    }

    public string IsSpecialPage
    {
        get
        {
            if (ViewState["IsSpecialPage"] != null)
            {
                return ViewState["IsSpecialPage"].ToString();
            }
            return "0";
        }
        set { ViewState["IsSpecialPage"] = value; }
    }

    private void SetTblFormControlEnabled(bool value)
    {
        this.txtCAUDITPERSONCODE.Enabled = value;
        this.txtCCREATEOWNERCODE.Enabled = value;
        this.txtCDEFINE1.Enabled = value;
        this.txtCDEFINE2.Enabled = value;
        this.txtCERPCODE.Enabled = value;
        this.txtCPO.Enabled = value;
        this.txtCSTATUS.Enabled = value;
        this.txtInAsnCTICKETCODE.Enabled = value;
        this.txtCVENDER.Enabled = value;
        this.txtCVENDERCODE.Enabled = value;
        this.txtDAUDITDATE.Enabled = value;
        this.txtDCREATETIME.Enabled = value;
        this.txtITYPE.Enabled = value;
        this.ddlDDEFINE3.Enabled = value;//是否判退
        this.ddlWorkType.Enabled = value;


        txtCVENDER.Attributes.Remove("onclick");
        txtCVENDERCODE.Attributes.Remove("onclick");

        btnDelete0.Enabled = value;
        btnNew.Enabled = value;
        btnSave.Enabled = value;
       // btnSearch.Enabled = value;
        btnImportExcel.Enabled = value;
        btnImportExcel.Visible = value;
        btnCreateInBill.Enabled = value;
        this.btnISplit.Enabled = value;

      
        //this.ButtonUnion.Enabled = value;

        //Status = entity.CSTATUS;
        //if (entity.CSTATUS != "0")
        //{
        //    SetTblFormControlEnabled(false);
        //}
    }


    public void SetMerageControlEnabled(string id)
    {
        IGenericRepository<INASN> con = new GenericRepository<INASN>(context);
        IGenericRepository<IN_MERGE_PALLETE> cond = new GenericRepository<IN_MERGE_PALLETE>(context);
        var caseList = from p in con.Get()
                       where p.id == id
                       select p;
        INASN entity = caseList.ToList().FirstOrDefault<INASN>();
        var queryList = from p in cond.Get()
                        orderby p.CREATETIME descending
                        where p.INASNCODE == entity.cticketcode && p.CSTATUS != "2"
                        select p;
        if (queryList != null && queryList.Count() > 0)
        {
            SetTblFormControlEnabled(false);
        }

    }



    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData(string id)
    {
        IGenericRepository<INASN> con = new GenericRepository<INASN>(context);
        IGenericRepository<IN_MERGE_PALLETE> cond = new GenericRepository<IN_MERGE_PALLETE>(context);
        var caseList = from p in con.Get()
                       where p.id == id
                       select p;
        INASN entity = caseList.ToList().FirstOrDefault<INASN>();
        entity.id = this.KeyID;
        this.txtID.Text = entity.id.ToString();
        this.txtInAsnCTICKETCODE.Text = entity.cticketcode;
        this.txtCSTATUS.SelectedValue = entity.cstatus;
        this.txtddefine4.Text = entity.ddefine4;

        this.txtCPO.Text = entity.cpo;
        this.txtITYPE.Text = entity.itype.ToString();
        InType = entity.itype.ToString();
        this.txtCMEMO.Text = entity.cmemo;
        this.txtCERPCODE.Text = entity.cerpcode;
        this.txtCVENDERCODE.Text = entity.cvendercode;
        this.txtCVENDER.Text = entity.cvender;
        this.txtCCREATEOWNERCODE.Text =OPERATOR.GetUserNameByAccountID(entity.ccreateownercode);
        this.txtCAUDITPERSONCODE.Text = OPERATOR.GetUserNameByAccountID(entity.cauditpersoncode);
        txtDCREATETIME.Text =!string.IsNullOrEmpty(entity.dcreatetime.ToString())? Convert.ToDateTime(entity.dcreatetime).ToString("yyyy-MM-dd HH:mm:ss") : "";
        this.txtCDEFINE1.Text = entity.cdefine1;
        this.txtCDEFINE2.Text = entity.cdefine2;
        ddlDDEFINE3.SelectedValue = entity.ddefine3;//是否判退
        ddlREASONCODE.SelectedValue = entity.reasoncode.IsNullOrEmpty() ? "" : entity.reasoncode;
        this.ddlWorkType.SelectedValue = entity.WORKTYPE.IsNullOrEmpty() ? "" : entity.WORKTYPE;

        TabMain0.Visible = true;
        cboIsAll.Checked = entity.idefine5 == 1 ? true : false;
        IsSpecialWIP_Return = entity.idefine5.HasValue ? entity.idefine5.Value : 0;

        Status = entity.cstatus;
        //if (entity.CSTATUS != "0" || entity.DDEFINE4!="0")
        //{
        //    SetTblFormControlEnabled(false);
        //}
        INTYPE intype = GetINTYPE(InType);
        if (!entity.cstatus.Equals("0") || !entity.ddefine4.Equals("0") || intype.Is_Query == 1//InType.Equals("2201")
            
            )
        {
            SetTblFormControlEnabled(false);
        }
        //状态为 未处理 并且 数据来原为 1 [数量来源 ( 0 : WMS，1 :oracle ERP )]的生成入库单按钮可用
        //if (entity.CSTATUS == "0" && entity.DDEFINE4 == "1")
        //{
        //    btnCreateInBill.Enabled = true;
        //}
        if (entity.WORKTYPE == "0" || entity.WORKTYPE == "")
        {
            this.ButtonUnion.Visible = false;
        }
        else
        {
            ButtonUnion.Visible = true;
        }
        //暂存补单按钮显示
        if (entity.cstatus == "0" || entity.cstatus == "1" || entity.cstatus == "2") {
            string isUse = this.GetConFig("140201");
            if (isUse == "1")
            {
                btnCreateTemporary.Visible = true;
            }
        }
   
        INTYPE INASN_type = GetINTYPEByID(this.KeyID, "INASN");
        if (entity.cstatus.Equals("0") && entity.ddefine4.Equals("1") &&  INASN_type.Is_Query != 1)
        {
            btnCreateInBill.Enabled = true;
        }
        else
        {
            //判断是否存在补单中的明细CQ 2014-10-15 18:06:07
            if (InBill.CheckAsn_DStatus(txtID.Text))
            {
                btnCreateInBill.Enabled = true;
            }
        }

       //// INTYPE INASN_type = GetINTYPEByID(this.KeyID, "INASN");
        if (INASN_type.Is_Query == 1)
        {
            this.btnCreateInBill.Enabled = false;
            this.ButtonUnion.Visible = false;
        }

        if (entity.cstatus == "2" || entity.cstatus == "3")
        {
            btnSync.Visible = true;
        }
        else {
            btnSync.Visible = false;
        }

        if (entity.cstatus.Equals("0") && IsSpecialWIP_Return == 1)
        {
            this.cboIsAll.Checked = true;
        }
        if (entity.itype == "104" || !entity.ddefine4.Equals("0"))//WIP Completion 44-104工单完工入库
        {
            this.btnNew.Enabled = false;
            this.btnDelete0.Enabled = false;
            btnImportExcel.Enabled = false;
        }
        //特殊元件退料
        try
        {
            if (InType.Equals("43") && Request.QueryString["IsSpecialPage"].Equals("0") && IsSpecialWIP_Return == 1)//IsSpecialPage
            {
                SetTblFormControlEnabled(false);
                //this.ButtonUnion.Enabled = false;
            }
        }
        catch (Exception)
        {

        }
        //显示PO Receipt 当工单类型为18时，显示类型 CQ 2014-4-24 13:26:00
        if (entity.itype == "101")
        {
            DataTable tb = InTypeLogic.GetInTypeName("101");
            txtITYPE.SelectedValue = tb.Rows[0]["CERPCODE"].ToString();
            txtITYPE.SelectedItem.Text = tb.Rows[0]["TYPENAME"].ToString();
        }


        var queryList = from p in cond.Get()
                        orderby p.CREATETIME descending
                        where p.INASNCODE == entity.cticketcode && p.CSTATUS != "2"
                        select p;
        if (queryList != null && queryList.Count() > 0)
        {
            SetTblFormControlEnabled(false);
        }

        this.btnImportExcel.Attributes["onclick"] = "PopupFloatWin('" + BuildRequestPageURL("/Apps/Import/FrmImportAsnDetail.aspx", SYSOperation.New, "") + "&AsnId=" + this.KeyID + "&CTICKETCODE=" + txtInAsnCTICKETCODE.Text.Trim() + "&ImportType=In','上传入库通知单明细','OUTASN_D',600,320); return false;";
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        //        SELECT* FROM
        //V_Inasn_D_Edit T1
        //INNER JOIN TW_BASE_PART T2
        //ON T1.cinvcode = T2.PART_FULL
        IGenericRepository<V_Inasn_D_Edit> entity = new GenericRepository<V_Inasn_D_Edit>(context);
        IGenericRepository<TW_BASE_PART> tw_part = new GenericRepository<TW_BASE_PART>(context);

        var caseList = from p in entity.Get()
                       where 1 == 1

                       select p;

        //https://www.tektutorialshub.com/entity-framework/join-query-entity-framework/
        //var caseList = from p in entity.Get()
        //               join t in tw_part.Get()
        //               on p.cinvcode equals t.cpartnumber
        //               where 1 == 1

        //               select new
        //               {
        //                   ids = p.ids,
        //                   id = p.id,
        //                   lineId = p.lineId,
        //                   cspecifications = p.cspecifications,
        //                   IQUANTITY=p.iquantity,
        //                   InBill_Qty=p.inbill_qty,
        //                   InBilled_Qty=p.inbilled_qty,
        //                   CERPCODELINE=p.cerpcodeline,
        //                   CPO=p.cpo,
        //                   IPOLINE=p.ipoline,
        //                   cstatus=p.cstatus,

        //                   cinvcode = p.cinvcode,
        //                   PART =t.PART,
        //                   RANK_FINAL =t.RANK_FINAL,
        //                   cinvname=p.cinvname,



        //               };

        if (txtID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.Contains(txtID.Text.Trim()));
        if (txtCinvcode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));


        if (caseList != null && caseList.Count() > 0)
        {
            caseList = caseList.OrderBy("LineID asc");
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else {
            AspNetPager1.RecordCount = 0;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
        //grdINASN_D.DataSource = GetPageSize(caseList.OrderBy(x=>x.lineId), PageSize, CurrendIndex).ToList();
        var listResult = GetPageSize(caseList.OrderBy(x => x.lineId), PageSize, CurrendIndex).ToList();
        var source = GetGridSourceDataByList(listResult, "cstatus", "ASN_D_STATUS");

        var src2 = GetGridSourceDataSplitPart(source);

//        grdINASN_D.DataSource = source;
        grdINASN_D.DataSource = src2;

        grdINASN_D.DataBind();
    }


    //https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/dataset-datatable-dataview/adding-columns-to-a-datatable
    public DataTable GetGridSourceDataSplitPart(DataTable srcData)
    {
        srcData.Columns.Add("PART", typeof(String));
        srcData.Columns.Add("RANK_FINAL", typeof(String));
        // var result = new DataTable();
        foreach (DataRow row in srcData.Rows)
        {
           
            string part = row["CINVCODE"].ToString();
        

            row["PART"] = part.Substring(0,part.Length - 2);// 	QBAM0030 => QBAM00
            row["RANK_FINAL"] = part.Substring(part.Length-1,1);// 	QBAM0030 => 0
            if (row["RANK_FINAL"].ToString() == "_")
                row["RANK_FINAL"] = "";
        }
        return srcData;
    }

    #endregion
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


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        INTYPE intype = GetINTYPE(InType);
        //DBUtil.BeginTrans();
        string msg = string.Empty;
        try
        {
            for (int i = 0; i < this.grdINASN_D.Rows.Count; i++)
            {
                if (this.grdINASN_D.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdINASN_D.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)//
                    {
                        if (Convert.ToDecimal(this.grdINASN_D.DataKeys[i].Values[1].ToString()) == 0
                           // this.grdINASN_D.Rows[i].Cells[this.grdINASN_D.Rows[i].Cells.Count-2].Text=="未处理"
                            && InAsn.ValidateIsCreateInBill(this.grdINASN_D.Rows[i].Cells[4].Text.Trim(), this.grdINASN_D.Rows[i].Cells[9].Text.Trim(), txtID.Text.Trim())
                            && !InAsn.CheckIsExistInAssitByInAsn_Id(txtID.Text.Trim())
                            && intype.Is_Query == 0 // //!InType.Equals("2201")
                            )
                        {
                            IGenericRepository<INASN_D> entity = new GenericRepository<INASN_D>(context);
                            var caseList = from p in entity.Get().AsEnumerable()
                                           where p.ids == this.grdINASN_D.DataKeys[i].Values[0].ToString() && p.cstatus == "0"
                                           select p;
                            if (caseList.Count() > 0)
                            {
                                entity.Delete(this.grdINASN_D.DataKeys[i].Values[0].ToString());
                                entity.Save();	//执行动作 
                                //20130822100612
                                //InAsn.UpdateStatus(txtID.Text.Trim());
                            }
                            else
                            {
                                //if (InType.Equals("2201"))
                                if (intype!=null && intype.Is_Query==1)
                                {
                                    //拼板入库通知单不能删除
                                    msg += Resources.Lang.FrmINASNEdit_MSG4+"!\r\n";
                                }
                                else
                                {
                                    string ErpCode = string.Empty;
                                    if (this.grdINASN_D.Rows[i].Cells[9].Text.Trim() != "&nbsp;")
                                    {
                                        ErpCode = this.grdINASN_D.Rows[i].Cells[9].Text.Trim();
                                    }
                                    msg += "[" + this.grdINASN_D.Rows[i].Cells[4].Text.Trim() + "]" + Resources.Lang.FrmINASNEdit_MSG5 + "[" + ErpCode + "]" +
                                        Resources.Lang.FrmINASNEdit_MSG6+ "！\r\n";
                                }
                            }

                        }
                        else
                        {
                            //if (InType.Equals("2201"))
                            if (intype != null && intype.Is_Query == 1)
                            {
                                msg += Resources.Lang.FrmINASNEdit_MSG4 + "!\r\n";
                            }
                            else
                            {
                                string ErpCode = string.Empty;
                                if (this.grdINASN_D.Rows[i].Cells[9].Text.Trim() != "&nbsp;")
                                {
                                    ErpCode = this.grdINASN_D.Rows[i].Cells[9].Text.Trim();
                                }
                                //料号ERP项次
                                msg += "[" + this.grdINASN_D.Rows[i].Cells[4].Text.Trim() + "]"+ Resources.Lang.FrmINASNEdit_MSG5 + "[" + ErpCode + "]"+ Resources.Lang.FrmINASNEdit_MSG7 +"！\r\n";
                            }
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                //删除成功
                msg = Resources.Lang.CommonB_RemoveSuccess + "！";
            }
            else
            {
                //删除失败
                msg = Resources.Lang.CommonB_RemoveFailed + "！\r\n" + msg;
            }
            this.Alert(msg);
            //DBUtil.Commit();
            this.btnSearch_Click(sender, e);
        }
        catch (Exception E)
        {
            //删除失败
            this.Alert(Resources.Lang.CommonB_RemoveFailed + "！" + E.Message.ToJsString());
            //DBUtil.Rollback();
        }
    }


    protected void btnSync_Click(object sender, EventArgs e) {
        string id = this.KeyID;
        var inasn = context.INASN.Where(x => x.id == id).FirstOrDefault();
        if (inasn == null) {
            this.Alert("通知单数据异常！");
            return;
        }

        if (inasn.cstatus != "1" && inasn.cstatus != "2" && inasn.cstatus != "3") {
            this.Alert("当前通知单不能进行该操作！");
            return;
        }
        string msg = string.Empty;
        bool isSuccess = new InAsnSync().InAsnSyncById(id, out msg);
        if (isSuccess)
        {
            this.AlertAndBack("FrmINASNEdit.aspx?" + BuildQueryString(SYSOperation.Modify, id + "&IsSpecialPage=" + Request.QueryString["IsSpecialPage"] + "&IsSpecialWIP_Return=" + Request.QueryString["IsSpecialPage"]), "抛转成功！");
        }
        else {
            this.Alert(msg.Replace("'"," "));
        }
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        //for (int i = 0; i < this.grdINASN_D.DataKeyNames.Length; i++)
        //{
        //    strKeyId += this.grdINASN_D.DataKeys[rowIndex].Values[i].ToString() + ",";
        //}
        strKeyId = this.grdINASN_D.DataKeys[rowIndex].Values[0].ToString();//strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdINASN_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         INTYPE intype = GetINTYPE(InType);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            //特殊元件领料
            IGenericRepository<IN_MERGE_PALLETE> cond = new GenericRepository<IN_MERGE_PALLETE>(context);
            var queryList = from p in cond.Get()
                            orderby p.CREATETIME descending
                            where p.INASNCODE == this.txtInAsnCTICKETCODE.Text && p.CSTATUS != "2"
                            select p;

            if (
                (Status.Length > 0 && Status != "0")|| this.txtddefine4.Text !="0"  
                || Convert.ToDecimal(this.grdINASN_D.DataKeys[e.Row.RowIndex].Values[1].ToString()) > 0
                || (queryList!=null && queryList.Count()>0)
                
                )//
            {
                linkModify.Enabled = false;
            }
            else
            {
                try
                {
                    if ((InType.Equals("43") && Request.QueryString["IsSpecialPage"].Equals("0") && IsSpecialWIP_Return == 1)//IsSpecialPage
                        || intype.Is_Query == 1 //InType.Equals("2201")
                        )
                    {
                        linkModify.Enabled = false;
                    }
                    else
                    {
                        linkModify.NavigateUrl = "#";                        
                        this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmINASN_DEdit.aspx?Flag=1&ids=" + strKeyID + "&ErpCode=" + txtCERPCODE.Text.Trim() + "&InType=" + txtITYPE.SelectedValue.Trim(), SYSOperation.Modify, strKeyID), Resources.Lang.FrmINASN_DEdit_Content1, "INASN_D", 1000, 500);
                        //this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASN_DEdit.aspx", SYSOperation.New, "") + "&InType=" + txtITYPE.SelectedValue.Trim() + "','','INASN_D',600,350);");
                    }
                }
                catch (Exception)
                {

                }
            }

            //获取ID
            //string id = this.grdBASE_CARGOSPACE.DataKeys[e.Row.RowIndex][0].ToString();
            CheckBox cbo = e.Row.FindControl("chkSelect") as CheckBox;
            //cbo.Attributes.Add("onclick", "SelIDCancelAll()");

            //判断是否已在SelectIds集合中
            GetSelectedIds();
            if (SelectIds.ContainsKey(strKeyID))
            {
                //如果是控件处于选中状态
                cbo.Checked = true;
            }

          

            ////物料名称
            //var partName = e.Row.Cells[4].Text;
            //if (!string.IsNullOrEmpty(partName) && partName.Length > 20)
            //{
            //    e.Row.Cells[4].Text = partName.Substring(0, 20) + "...";
            //}
        }
    }

    protected void dsGrdINASN_D_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

   
    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        string msg = string.Empty;

        #region 字段为空判断
        //
        if (this.txtID.Text.Trim().Length == 0)
        {
            //ID项不允许空
            this.Alert(Resources.Lang.FrmINASNEdit_MSG8 + "！");
            this.SetFocus(txtID);
            return false;
        }

        if (this.txtITYPE.SelectedValue.Trim().Length == 0)
        {
            //请选择类型
            this.Alert(Resources.Lang.FrmINASNEdit_MSG3 + "！");
            this.SetFocus(txtITYPE);
            return false;
        }
        //
        if (this.txtCSTATUS.SelectedValue.Trim().Length == 0)
        {
            //状态项不允许空
            this.Alert(Resources.Lang.FrmINASNEdit_MSG9 + "！");
            this.SetFocus(txtCSTATUS);
            return false;
        }
        //
        if (this.txtCSTATUS.SelectedValue.Trim().Length > 0)
        {
            if (this.txtCSTATUS.SelectedValue.GetLengthByByte() > 20)
            {
                //状态项超过指定的长度20
                this.Alert(Resources.Lang.FrmINASNEdit_MSG10 + "！");
                this.SetFocus(txtCSTATUS);
                return false;
            }
        }
        //
        if (this.txtCPO.Text.Trim().Length > 0)
        {
            if (this.txtCPO.Text.GetLengthByByte() > 30)
            {
                //po号项超过指定的长度30
                this.Alert(Resources.Lang.FrmINASNEdit_MSG11 + "！");
                this.SetFocus(txtCPO);
                return false;
            }
        }
        //
        if (this.txtITYPE.SelectedValue.Trim().Length > 0)
        {
            if (StringExtension.IsDecimal(this.txtITYPE.SelectedValue)== false)
            {
                //入库类型项不是有效的十进制数字
                this.Alert(Resources.Lang.FrmINASNEdit_MSG12 + "！");
                this.SetFocus(txtITYPE);
                return false;
            }
        }
        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 200)
            {
                //备注项超过指定的长度200
                this.Alert(Resources.Lang.FrmINASNEdit_MSG13 + "！");
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        //
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
            if (this.txtCERPCODE.Text.GetLengthByByte() > 30)
            {
                //主表ERP单号项超过指定的长度30
                this.Alert(Resources.Lang.FrmINASNEdit_MSG14 + "！");
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        //
        if (this.txtCVENDERCODE.Text.Trim().Length > 0)
        {
            if (this.txtCVENDERCODE.Text.GetLengthByByte() > 50)
            {
                //供应商编码项超过指定的长度50
                this.Alert(Resources.Lang.FrmINASNEdit_MSG15 + "！");
                this.SetFocus(txtCVENDERCODE);
                return false;
            }
        }
        //
        if (this.txtCVENDER.Text.Trim().Length > 0)
        {
            if (this.txtCVENDER.Text.GetLengthByByte() > 150)
            {
                //供应商名称项超过指定的长度150
                this.Alert(Resources.Lang.FrmINASNEdit_MSG16 + "！");
                this.SetFocus(txtCVENDER);
                return false;
            }
        }

        if (string.IsNullOrEmpty(this.ddlWorkType.SelectedValue))
        {
            //作业方式不允许空
            this.Alert(Resources.Lang.FrmINASNEdit_MSG17 + "！");
            this.SetFocus(ddlWorkType);
            return false;
        }

        //
        if (this.txtCCREATEOWNERCODE.Text.Trim() == "")
        {
            //制单人项不允许空
            this.Alert(Resources.Lang.FrmINASNEdit_MSG18 + "！");
            this.SetFocus(txtCCREATEOWNERCODE);
            return false;
        }
        //
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (this.txtCCREATEOWNERCODE.Text.GetLengthByByte() > 50)
            {
                //制单人项超过指定的长度50
                this.Alert(Resources.Lang.FrmINASNEdit_MSG19 + "！");
                this.SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }
        //
        if (this.txtCAUDITPERSONCODE.Text.Trim().Length > 0)
        {
            if (this.txtCAUDITPERSONCODE.Text.GetLengthByByte() > 50)
            {
                //审核人项超过指定的长度50
                this.Alert(Resources.Lang.FrmINASNEdit_MSG20 + "！");
                this.SetFocus(txtCAUDITPERSONCODE);
                return false;
            }
        }
        #endregion

        #region ERPCODE不能为空判断
        //2015-3-18 17:34:10 吴总 要求除 其他入库出库类型都必须输入CERPCODE
        if (txtITYPE.SelectedValue.Trim() != "106")
        {
            if (txtCERPCODE.Text.Trim().Length == 0)
            {
                //this.Alert("入库类型为 WIP Completion(工單完工入库)時ERP單号不能為空！");
                //請在ERPCODE 欄位輸入工單號
                this.Alert(Resources.Lang.FrmINASNEdit_MSG21 + "！");
                this.SetFocus(txtCERPCODE);
                return false;
            }
            //检查所有类型的工单状态是否可用
             msg = InAsn.Check_WIP_STATUS(this.txtCERPCODE.Text);
            if (!msg.Equals("OK"))
            {
                this.Alert(msg);
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        #endregion

        #region 38 WIP Negative Issue
        if (this.txtITYPE.SelectedValue.Trim().Equals("38"))//WIP Negative Issue
        {
            if (txtCERPCODE.Text.Trim().Length == 0)
            {
                //請在ERPCODE 欄位輸入工單號
                this.Alert(Resources.Lang.FrmINASNEdit_MSG21 + "！");
                this.SetFocus(txtCERPCODE);
                return false;
            }
            msg = new InAsn().CheckWipNegativeIssueHead(txtCERPCODE.Text.Trim());
            if (!msg.Equals("OK"))
            {
                this.Alert(msg);
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        #endregion

        #region PO Receipt 101
        //采购单收货新增检查
        if (this.txtITYPE.Text.Trim().Equals("101"))
        {
            //
            if (this.txtCPO.Text.Trim().Length == 0)
            {
                //[采购单收货]来源单号项不允许空
                this.Alert(Resources.Lang.FrmINASNEdit_MSG22 + "！");
                this.SetFocus(txtCPO);
                return false;
            }
            if (this.txtCPO.Text.Trim().Length != 9)
            {
                //[采购单收货]来源单号位数不是9位
                this.Alert(Resources.Lang.FrmINASNEdit_MSG23 + "！");
                this.SetFocus(txtCPO);
                return false;
            }
            //
            if (this.txtCVENDERCODE.Text.Trim().Length == 0)
            {
                this.Alert(Resources.Lang.FrmINASNEdit_MSG24 + "！");
                this.SetFocus(txtCVENDERCODE);
                return false;
            }
            //
            if (this.txtCVENDER.Text.Trim().Length == 0)
            {
                //[采购单收货]供应商名称项不允许空
                this.Alert(Resources.Lang.FrmINASNEdit_MSG25 + "！");
                this.SetFocus(txtCVENDERCODE);
                return false;
            }
        }
        #endregion

        #region 102-15 RMA Receipt 销货退回
        //RMA Receipt : 102-15        
        if (this.txtITYPE.Text.Trim().Equals("102"))
        {
            if (this.txtCERPCODE.Text.Trim().Length == 0)
            {
                //this.Alert("ERP单号不能為空！");
                //請在ERPCODE 欄位輸入工單號
                this.Alert(Resources.Lang.FrmINASNEdit_MSG26 + "！");
                this.SetFocus(txtCERPCODE);
                return false;
            }

            msg = InAsn.Fun_CheckRMA_ReceiptHead(txtCERPCODE.Text.Trim());
            if (!msg.Equals("OK"))
            {
                this.Alert(msg);
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        #endregion

        #region 103-43 WIP Return
        if (this.txtITYPE.SelectedValue.Trim().Equals("103"))//43 :WIP Return-103
        {
            if (txtCERPCODE.Text.Trim().Length == 0)
            {
                //this.Alert("入库类型为 WIP Completion(工單完工入库)時ERP單号不能為空！");
                //請在ERPCODE 欄位輸入工單號
                this.Alert(Resources.Lang.FrmINASNEdit_MSG26 + "！");
                this.SetFocus(txtCERPCODE);
                return false;
            }
            if (!cboIsAll.Checked)
            {
                msg = new InAsn().CheckWIP_Return_Head(txtCERPCODE.Text.Trim());
                if (!msg.Equals("OK"))
                {
                    this.Alert(msg);
                    this.SetFocus(txtCERPCODE);
                    return false;
                }
            }
        }
        #endregion

        #region 104-44 WIP Completion
        if (this.txtITYPE.SelectedValue.Trim().Equals("104"))//WIP Completion(工单完工入库)-104
        {//44 : WIP Completion 

            if (txtCERPCODE.Text.Trim().Length == 0)
            {
                //this.Alert("入库类型为 WIP Completion(工單完工入库)時ERP單号不能為空！");
                //請在ERPCODE 欄位輸入工單號
                this.Alert(Resources.Lang.FrmINASNEdit_MSG26 + "！");
                this.SetFocus(txtCERPCODE);
                return false;
            }
            //判断是否存在未扣帐的物料配送
            if (Stock_AutoAllocate.CheckAutoAlloCount(txtCERPCODE.Text.Trim()))
            {
                //该工单存在未扣帐的物料配送调拨单，不能完工入库
                this.Alert(Resources.Lang.FrmINASNEdit_MSG28 + "！");
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        #endregion

        #region 105 工单超领退
        //工单超领退
        if (txtITYPE.SelectedValue.Trim().Equals("105"))//(new InTypeQuery().ValidateInTypeIsWipIssue(txtITYPE.SelectedValue.Trim()))
        {
            msg = new InAsn().CheckWip_CLT_Head(txtCERPCODE.Text.Trim());
            if (!msg.Equals("OK"))
            {
                this.Alert(msg);
                this.SetFocus(txtCERPCODE);
                return false;
            }
            if (this.txtCMEMO.Text.Trim().Length == 0)
            {
                //请在备注处填写flow单号
                this.Alert(Resources.Lang.FrmINASNEdit_MSG29 + "！");
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        #endregion

        #region 单据状态检查
        //判断单据状态
        if (this.Operation() == SYSOperation.Modify)
        {
            msg = InAsn.GetInAsnStatusByInAsnId(txtID.Text.Trim());
            if (!msg.Equals("0"))
            {
                //只有未处理的单据才能修改
                this.Alert(Resources.Lang.FrmINASNEdit_MSG30 + "!");
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        #endregion

        //#region 特殊元件
        ////特定元件只能使用與非標準工單Roger 20130509
        //if (IsSpecialWIP_Return == 1)
        //{
        //    //工单负退料检查
        //    var StarderMo = OUTASNRule.IsStarderMo(txtCERPCODE.Text.Trim());
        //    if (StarderMo)
        //    {
        //        this.Alert("特定元件只能使用與非標準工單!");
        //        this.SetFocus(txtCERPCODE);
        //        return false;
        //    }
        //}
        //#endregion
        #region 其它入库判断理由码
        if (txtITYPE.SelectedValue == "1110")
        {
            if (ddlREASONCODE.SelectedValue.IsNullOrEmpty())
            {
                this.Alert(Resources.Lang.FrmINASNEdit_MSG31 + "！");
                this.SetFocus(ddlREASONCODE);
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
    public INASN SendData( IGenericRepository<INASN> con)
    {
        INASN entity = new INASN();
        if (!string.IsNullOrEmpty(this.KeyID))
        {
             entity = (from p in con.Get().AsEnumerable()
                          where p.id == this.KeyID
                          select p).FirstOrDefault<INASN>();
        }
        
        //INASN entity = new INASN();
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
        
        if (this.Operation()== SYSOperation.New)
        {
            this.txtInAsnCTICKETCODE.Text = this.txtInAsnCTICKETCODE.Text.Trim();

            if (this.txtInAsnCTICKETCODE.Text.Length > 0)
            {
                entity.cticketcode = txtInAsnCTICKETCODE.Text;
            }
            else
            {
                entity.cticketcode = new InAsn().CreateNo("INASN");
                //entity.SetDBNull("CTICKETCODE", true);
                //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
                //entity.CTICKETCODE = null;
            }

            entity.cstatus = "0";
        }

        //
        //if (this.Operation == SYSOperation.New)
        //{
        //    entity.CSTATUS = "0";
        //}
        //else
        //{
        //    entity.SetDBNull("CSTATUS", true);
        //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        //    //entity.CSTATUS = null;
        //}
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
        //this.txtITYPE.Text = this.txtITYPE.Text.Trim();
        if (this.txtITYPE.SelectedValue.Length > 0)
        {

            entity.itype =txtITYPE.SelectedValue.ToString();
        }
        else
        {
            entity.itype = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.ITYPE = null;
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
        this.txtCERPCODE.Text = this.txtCERPCODE.Text.Trim();

        if (this.txtCERPCODE.Text.Length > 0)
        {
            entity.cerpcode = txtCERPCODE.Text;
        }
        else
        {
            entity.cerpcode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CERPCODE = null;
        }
        //
        this.txtCVENDERCODE.Text = this.txtCVENDERCODE.Text.Trim();

        if (this.txtCVENDERCODE.Text.Length > 0)
        {
            entity.cvendercode = txtCVENDERCODE.Text;
        }
        else
        {
            entity.cvendercode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CVENDERCODE = null;
        }
        //
        this.txtCVENDER.Text = this.txtCVENDER.Text.Trim();

        if (this.txtCVENDER.Text.Length > 0)
        {
            entity.cvender = txtCVENDER.Text;
        }
        else
        {
            entity.cvender = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CVENDER = null;
        }
        //
        this.txtCCREATEOWNERCODE.Text = this.txtCCREATEOWNERCODE.Text.Trim();

        if (this.txtCCREATEOWNERCODE.Text.Length > 0)
        {
            entity.ccreateownercode =OPERATOR.GetUserIDByUserName(txtCCREATEOWNERCODE.Text);
        }
        else
        {
            entity.ccreateownercode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCREATEOWNERCODE = null;
        }
        //
        this.txtCAUDITPERSONCODE.Text = this.txtCAUDITPERSONCODE.Text.Trim();

        if (this.txtCAUDITPERSONCODE.Text.Length > 0)
        {
            entity.cauditpersoncode = OPERATOR.GetUserIDByUserName(txtCAUDITPERSONCODE.Text);
        }
        else
        {
            entity.cauditpersoncode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CAUDITPERSONCODE = null;
        }
        entity.dcreatetime = DateTime.Now;

        if (txtCDEFINE1.Text.Length > 0)
        {
            entity.cdefine1 = txtCDEFINE1.Text;
        }
        else
        {
            entity.cdefine1 = string.Empty;
        }

        if (txtCDEFINE2.Text.Length > 0)
        {
            entity.cdefine2 = txtCDEFINE2.Text;
        }
        else
        {
            entity.cdefine2 = string.Empty;
        }
        //加上作业方式
        var worktypeVal=this.ddlWorkType.SelectedValue;
        entity.WORKTYPE = !worktypeVal.IsNullOrEmpty() ? worktypeVal : string.Empty;
                                                                                        

        entity.ddefine3 = ddlDDEFINE3.SelectedValue.Trim();
        entity.ddefine4 = "0";//数量来源 ( 0 : WMS，1 :oracle ERP )
        entity.idefine5 = cboIsAll.Checked ? 1 : 0;//是否其它用料  0: 否，1:是
        if (!ddlREASONCODE.SelectedValue.IsNullOrEmpty())
        {
            entity.reasoncode = ddlREASONCODE.SelectedValue;
            entity.reasoncontent = ddlREASONCODE.SelectedItem.Text;
        }

        //entity.cticketcode = this.txtInAsnCTICKETCODE.Text;
        //entity.cstatus = this.txtCSTATUS.SelectedValue;
       

       

        #region 界面上不可见的字段项
        /*
        entity.DDEFINE3 = ;
        entity.DDEFINE4 = ;
        entity.IDEFINE5 = ;
        entity.DCREATETIME = ;
        entity.DAUDITDATE = ;
        */
        #endregion

        return entity;

    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Note by Qamar 2020-11-23
        if (txtCERPCODE.Text.Trim() == "")
            txtCERPCODE.Text = DateTime.Now.ToString("yyyyMMddHHmmss");

        try
        {
            this.btnSave.Enabled = false;//CQ 2013-5-13 13:47:51
            SaveToDB(sender);
            this.btnSave.Enabled = true;
            //20130702084429
            btnSave.Style.Remove("disabled");
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.btnNew.Enabled = false;
        SaveToDB(sender);
        this.btnNew.Enabled = true;
    }

    private void SaveToDB(object sender)
    {
        bool isError = false;
        
        IGenericRepository<INASN> con = new GenericRepository<INASN>(context);
        if (this.CheckData())
        {
            string msg = string.Empty;
            INASN entity = this.SendData(con);
            if (this.Operation() != SYSOperation.New)
            {

            }
            string strKeyID = "";
            try
            {
                if (this.Operation() == SYSOperation.Modify)
                {
                    strKeyID = txtID.Text.Trim();
                    entity.id = txtID.Text.Trim();
                    con.Update(entity);
                    con.Save();
                    #region 20130303修改 临时使用
                    //20130303修改 临时使用
                    //if (entity.ITYPE == 44)//WIP Completion
                    //{
                    //    RD_FrmINASNListQuery query = new RD_FrmINASNListQuery();
                    //    if (query.GetProcCreateInAsn_D(txtCERPCODE.Text.Trim(), strKeyID, ref msg))
                    //    {
                    //        msg = "保存成功!";
                    //    }
                    //    else
                    //    {
                    //        msg += "保存失败!";
                    //    }
                    //}
                    //else
                    //{
                    //    msg = "保存成功!";
                    //}
                    //if (entity.ITYPE == 38)//WIP Negative Issue
                    //{
                    //    INASNRule.Get_FUN_CHECKWIPNEGATIVEISSUE(entity.CERPCODE);
                    //}
                    //20130303修改 临时使用 
                    #endregion
                    //保存成功
                    msg = Resources.Lang.CommonB_SaveSuccess + "!";
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    strKeyID = Guid.NewGuid().ToString();
                    entity.id = strKeyID;
                    con.Insert(entity);
                    con.Save();
                    if (entity.itype == "104")//WIP Completion 44-104
                    {
                    }
                    else
                    {
                        //保存成功
                        msg = Resources.Lang.CommonB_SaveSuccess + "!";
                    }
                    if (entity.itype == "38")//WIP Negative Issue
                    {
                        InAsn.Get_FUN_CHECKWIPNEGATIVEISSUE(entity.cerpcode);
                    }
                    //this.AlertAndBack("FrmINASNEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID),"保存成功"); 
                }
                else if (this.Operation() == SYSOperation.Preserved1)
                {
                    strKeyID = txtID.Text.Trim();
                    //保存成功
                    msg = Resources.Lang.CommonB_SaveSuccess + "!";
                }
                //保存成功
                if (msg == Resources.Lang.CommonB_SaveSuccess + "!")
                {
                    if ((sender as Button).ID == "btnNew")
                    {
                        isError = true;
                        Response.Redirect(BuildRequestPageURL("FrmINASNEdit.aspx", SYSOperation.Preserved1, strKeyID + "&IsSpecialPage=" + Request.QueryString["IsSpecialPage"] + "&IsSpecialWIP_Return=" + Request.QueryString["IsSpecialPage"]));
                    }
                    else
                    {
                        isError = true;
                        this.AlertAndBack("FrmINASNEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID + "&IsSpecialPage=" + Request.QueryString["IsSpecialPage"] + "&IsSpecialWIP_Return=" + Request.QueryString["IsSpecialPage"]), msg);
                    }
                }
                else
                {
                    Alert(msg);
                }
            }
            catch (Exception E)
            {
                if (!isError)
                {
                    //删除通知单
                    con.Delete(strKeyID);
                    con.Save();
                    //失败
                    this.Alert(Resources.Lang.WMS_Common_Msg_SaveFailed + E.Message);//E.Message
                }
#if Debug 
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
            }
        }
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    string ids = (sender as LinkButton).CommandArgument;
    //    this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASN_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + ids + "&InType=" + txtITYPE.SelectedValue.Trim() + "','','INASN_D',600,350);");
    //}

    protected void txtITYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (txtITYPE.SelectedValue.Trim())
        {
            case "1110"://WIP Completion : 44-104
                ddlREASONCODE.Enabled = true;
                break;
            case "104"://WIP Completion : 44-104
                this.btnNew.Enabled = false;
                this.btnDelete0.Enabled = false;
                cboIsAll.Enabled = false;
                cboIsAll.Checked = false;
                Lab_Po.Visible = false;
                Lab_Vender.Visible = false;
                Lab_Vendercode.Visible = false;
                if (this.Operation() == SYSOperation.New)
                {
                    this.lblDCREATETIME.Visible = false;
                    this.txtDCREATETIME.Visible = false;
                    Lab_ErpCode.Visible = true;
                }
                else
                {
                    this.lblDCREATETIME.Visible = true;
                    this.txtDCREATETIME.Visible = true;
                    Lab_ErpCode.Visible = false;
                }
                break;
            case "103"://WIP Return ：43-103
                if (IsSpecialWIP_Return == 1)
                {
                    cboIsAll.Checked = true;
                }
                Lab_Po.Visible = false;
                Lab_Vender.Visible = false;
                Lab_Vendercode.Visible = false;
                Lab_ErpCode.Visible = true;
                this.lblDCREATETIME.Visible = true;
                this.txtDCREATETIME.Visible = true;
                break;
            case "101"://PO Receipt 采购单收货18-101
                cboIsAll.Enabled = false;
                cboIsAll.Checked = false;
                if (this.Operation() == SYSOperation.New)
                {
                    ddlDDEFINE3.Enabled = true;
                    Lab_ErpCode.Visible = true;
                    Lab_Po.Visible = true;
                    Lab_Vender.Visible = true;
                    Lab_Vendercode.Visible = true;
                }
                else
                {
                    ddlDDEFINE3.Enabled = false;
                    Lab_ErpCode.Visible = false;
                    Lab_Po.Visible = false;
                    Lab_Vender.Visible = false;
                    Lab_Vendercode.Visible = false;
                }
                break;
            default:
                this.btnNew.Enabled = true;
                this.btnDelete0.Enabled = true;
                cboIsAll.Enabled = false;
                cboIsAll.Checked = false;
                this.lblDCREATETIME.Visible = true;
                this.txtDCREATETIME.Visible = true;
                Lab_ErpCode.Visible = false;
                Lab_Po.Visible = false;
                Lab_Vender.Visible = false;
                Lab_Vendercode.Visible = false;
                ddlREASONCODE.Enabled = false;
                break;
        }

        ddlREASONCODE.Enabled = true;
    }

    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }

    public Dictionary<string, string> SelectCinvcodes
    {
        set { Session["SelectCinvcodes"] = value; }
        get { return Session["SelectCinvcodes"] as Dictionary<string, string>; }
    }



    /// <summary>
    /// 获取选择的ID
    /// </summary>
    public void GetSelectedCinvCodes()
    {
        //if (SelectCinvcodes == null)
        //{
            SelectCinvcodes = new Dictionary<string, string>();
        //}

        foreach (GridViewRow item in this.grdINASN_D.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string ids = this.grdINASN_D.DataKeys[item.RowIndex][0].ToString();
                string cinvcodes = item.Cells[4].Text.ToString();

                //控件选中且集合中不存在添加
                if (cbo.Checked && !SelectCinvcodes.ContainsKey(ids))
                {
                    SelectCinvcodes.Add(ids, cinvcodes);
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectCinvcodes.ContainsKey(ids))
                {
                    SelectCinvcodes.Remove(ids);
                }
            }
        }
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

        foreach (GridViewRow item in this.grdINASN_D.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string ids = this.grdINASN_D.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                //Note by Qamar 2020-11-28
                //台惟案 強制勾選
                cbo.Checked = true;

                //控件选中且集合中不存在添加
                if (cbo.Checked && !SelectIds.ContainsKey(ids))
                {
                    SelectIds.Add(ids, ids);
                }//未选中且集合中存在的移除
                else if (!cbo.Checked && SelectIds.ContainsKey(ids))
                {
                    SelectIds.Remove(ids);
                }
            }
        }
    }

    /// <summary>
    /// 生成入库单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateInBill_Click(object sender, EventArgs e)
    {
        GetSelectedIds();

        string InBill_id = Guid.NewGuid().ToString();
        string msg = string.Empty;
        if (SelectIds.Count > 0)
        {
            foreach (var item in SelectIds.Values)
            {
                IGenericRepository<INASN_D> con = new GenericRepository<INASN_D>(context);
                var caseList = from p in con.Get()
                               where p.ids == item.Trim()
                               select p;
                INASN_D OutAsn_d = caseList.ToList().FirstOrDefault<INASN_D>();
                var result = Comm_Fun.CanModDebit(txtCERPCODE.Text.Trim(), OutAsn_d.cinvcode, "0", "", this.txtITYPE.SelectedValue.Trim(), "");
                if (!result.Equals("1"))
                {
                    Alert(result);
                    return;
                }
                //20131105102731 通知单存在修改中的料时，不允许生成出库单
                result = Comm_Fun.CanAsnDebit(txtInAsnCTICKETCODE.Text.Trim(), OutAsn_d.cinvcode, "0", "", "");
                if (!result.Equals("OK"))
                {
                    Alert(result);
                    return;
                }
            }
            if (InAsn.CreateInBillByInAsn_D(SelectIds, txtID.Text.Trim(), InBill_id, WmsWebUserInfo.GetCurrentUser().UserNo,"0", ref msg))
            {
                //20130702084429
                btnCreateInBill.Style.Remove("disabled");
                //生成成功 跳转到入库单页面
                this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmINBILLEdit.aspx", SYSOperation.Modify, InBill_id) + "','" + Resources.Lang.FrmINBILLList_MSG9 + "','INBILL');");
            }
        }
        else
        {
            //请选择入库通知单明细
            msg = Resources.Lang.FrmINASNEdit_MSG32 + "!";
            //20130702084429
            btnCreateInBill.Style.Remove("disabled");
        }
        if (msg.Length > 0)
        {
            Alert(msg);
        }
    }

    /// <summary>
    /// 生成暂存入库单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateTemporary_Click(object sender, EventArgs e)
    {
        GetSelectedIds();

        string InBill_id = Guid.NewGuid().ToString();
        string msg = string.Empty;
        if (SelectIds.Count > 0)
        {
            foreach (var item in SelectIds.Values)
            {
                IGenericRepository<INASN_D> con = new GenericRepository<INASN_D>(context);
                var caseList = from p in con.Get()
                               where p.ids == item.Trim()
                               select p;
                INASN_D OutAsn_d = caseList.ToList().FirstOrDefault<INASN_D>();
                var result = Comm_Fun.CanModDebit(txtCERPCODE.Text.Trim(), OutAsn_d.cinvcode, "0", "", this.txtITYPE.SelectedValue.Trim(), "");
                if (!result.Equals("1"))
                {
                    Alert(result);
                    return;
                }
                //20131105102731 通知单存在修改中的料时，不允许生成出库单
                result = Comm_Fun.CanAsnDebit(txtInAsnCTICKETCODE.Text.Trim(), OutAsn_d.cinvcode, "0", "", "");
                if (!result.Equals("OK"))
                {
                    Alert(result);
                    return;
                }
            }
            if (InAsn.CreateInBillByInAsn_D(SelectIds, txtID.Text.Trim(), InBill_id, WmsWebUserInfo.GetCurrentUser().UserNo,"1", ref msg))
            {
                //20130702084429
                btnCreateInBill.Style.Remove("disabled");
                //生成成功 跳转到入库单页面
                this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmINBILLEdit.aspx", SYSOperation.Modify, InBill_id) + "','" + Resources.Lang.FrmINBILLList_MSG9 + "','INBILL');");
            }
        }
        else
        {
            //请选择入库通知单明细
            msg = Resources.Lang.FrmINASNEdit_MSG32 + "!";
            //20130702084429
            btnCreateInBill.Style.Remove("disabled");
        }
        if (msg.Length > 0)
        {
            Alert(msg);
        }
    }

    protected void grdINASN_D_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }

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
            //打印入库通知单
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASNEdit_Print.aspx", SYSOperation.New, printid) + "','" + Resources.Lang.FrmINASNEdit_UnionPallet_MSG6 + "','BAR_REPACK',840,600);");

        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }



    protected void btnISplit_Click(object sender, EventArgs e)
    {
        this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmiStockSplit.aspx", SYSOperation.New, txtID.Text.Trim()) + "','','FrmiStockSplit',1200,900);");
    }

    #endregion
   
    protected void ButtonUnion_Click(object sender, EventArgs e)
    {

        GetSelectedCinvCodes();

        if (SelectCinvcodes.Count == 0)
        {
            //请选择要拼板的料号
            this.Alert(Resources.Lang.FrmINASNEdit_MSG33 + "！");
        }
        else
        {
            //拼板入库
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASNEdit_UnionPallet.aspx", SYSOperation.New, this.KeyID) + "','" + Resources.Lang.CommonB_UnionPalletInbill+ "','IN_MERGE_PALLETE',1000,900);");

        }
    }
    
}

