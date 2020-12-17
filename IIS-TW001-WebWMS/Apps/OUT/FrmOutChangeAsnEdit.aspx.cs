using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;

public partial class Apps_OUT_FrmOutChangeAsnEdit : WMSBasePage
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            InitPage();
            if (Operation() == SYSOperation.Modify)//修改
            {
                ShowData();
                btnOK.Visible = true;
                btnCheck.Visible = false;
                btnSave.Visible = true;
            }
            else if (Operation() == SYSOperation.Approve)//审核
            {
                ShowData();
                btnOK.Visible = false;
                btnSave.Visible = false;
                btnCheck.Visible = true;
            }
            else
            {
                //制单人
                txtCCREATEOWNERCODE.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                //制单时间
                txtDCREATETIME.Text =DateTime.Now.ToString();
                txtID.Text = Guid.NewGuid().ToString();
                //隐藏完成按钮
                btnOK.Visible = false;
                btnCheck.Visible = false;
                btnSave.Visible = true;
            }
            //查询
            btnSearch_Click(btnSearch, EventArgs.Empty);
        }
    
        btnSave.Attributes["onclick"] = GetPostBackEventReference(btnSave) + ";disabled=true;";
        btnOK.Attributes["onclick"] = GetPostBackEventReference(btnOK) + ";disabled=true;";
        btnCheck.Attributes["onclick"] = GetPostBackEventReference(btnCheck) + ";disabled=true;";
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
        cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OutChange');return false;";
        //状态
        Help.DropDownListDataBind(new SysParameterList().GetSys_ParameterByFLAG_TYPE("ASNCHANGE"), ddlCSTATUS, "全部", "FLAG_NAME", "FLAG_ID", "");
    }


    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        #region Old
        //OUTASNEntity entity = new OUTASNEntity();
        //entity.ID = KeyID;
        //entity.SelectByPKeys();
        //txtID.Text = entity.ID;
        //txtOutAsnCTICKETCODE.Text = entity.CTICKETCODE;
        //ddlCSTATUS.SelectedValue = entity.CSTATUS;
        //OutType = entity.CSTATUS;
        //txtITYPE.SelectedValue = entity.ITYPE.ToString();
        //txtCCLIENTCODE.Text = entity.CCLIENTCODE;
        //txtCCLIENT.Text = entity.CCLIENT;
        //txtCSO.Text = entity.CSO;
        //txtCCREATEOWNERCODE.Text = entity.CCREATEOWNERCODE;

        //if (entity.DCREATETIME != null)
        //{
        //    txtDCREATETIME.Text = entity.DCREATETIME.ToString("yyyy-MM-dd HH:mm:ss");
        //}
        //txtCERPCODE.Text = entity.CERPCODE;
        //txtCAUDITPERSONCODE.Text = entity.CAUDITPERSONCODE;
        //txtDAUDITDATE.Text = entity.DAUDITDATE.HasValue ? entity.DAUDITDATE.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
        //txtCMEMO.Text = entity.CMEMO;
        //txtCDEFINE1.Text = entity.CDEFINE1;
        //TabMain0.Visible = true;
        //Status = entity.CSTATUS;
        //CDEFINE2 = entity.CDEFINE2;
        //IsSpecialWIP_Issue = entity.IDEFINE5.HasValue ? entity.IDEFINE5.Value : 0;
        //OutType = entity.ITYPE.ToString();
        //btnImportExcel.Enabled = true;
        //if (!entity.CSTATUS.Equals("0") || !entity.CDEFINE2.Equals("0") || OutType.Equals("33"))
        //{
        //    SetTblFormControlEnabled(false);
        //}
        ////状态为 未处理 并且 数据来原为 1 [数据来源 ( 0 : WMS，1 :oracle ERP )]的生成出库单按钮可用
        //if (entity.CSTATUS.Equals("0") && entity.CDEFINE2.Equals("1"))
        //{
        //    btnCreateOutBill.Enabled = true;
        //}
        ////WIP Issue : 35 、Sales order issue ：33
        //if (string.IsNullOrEmpty(entity.CDEFINE2)/*Roegr 20130509 增加判空处理*/ || !entity.CDEFINE2.Equals("0") || OutType.Equals("33"))
        //{
        //    btnDelete.Enabled = false;
        //    btnNew.Enabled = false;
        //    btnSave.Enabled = false;
        //    btnImportExcel.Enabled = false;
        //}
        ////Return to Vendor 
        //if (OutType.Equals("36"))
        //{
        //    btnImportExcel.Enabled = false;
        //}
        ////特殊元件领料
        //try
        //{
        //    if (OutType.Equals("35") && Request.QueryString["IsSpecialPage"].Equals("0") && IsSpecialWIP_Issue == 1)//IsSpecialPage
        //    {
        //        SetTblFormControlEnabled(false);
        //    }
        //}
        //catch (Exception)
        //{

        //}
        //btnImportExcel.Attributes["onclick"] = "PopupFloatWin('" + BuildRequestPageURL("/Apps/Import/FrmImportAsnDetail.aspx", SYSOperation.New, "") + "&AsnId=" + KeyID + "&CTICKETCODE=" + txtOutAsnCTICKETCODE.Text.Trim() + "&ImportType=Out','上传出库通知单明细','OutChange_d',600,320); return false;";
        ////WIP Completion Return 工单完工退 17
        //if (OutType.Equals("17"))
        //{
        //    SetTblFormControlEnabled(false);
        //}

        ////Roger 2013-5-2 17:48:49 合并后通知单不允许补单
        //if (!OutChange_dRule.IsMerge(KeyID))
        //{
        //    btnDelete.Enabled = false;
        //    btnNew.Enabled = false;
        //    btnSave.Enabled = false;
        //    btnCreateOutBill.Enabled = false;
        //    btnImportExcel.Enabled = false;
        //    btnDisassembly.Enabled = false;
        //}

        ////20130617112810
        ////特殊超领的，啥都不允许操作
        //if (entity.SPECIAL_OUT.Equals("1"))
        //{
        //    SetTblFormControlEnabled(false);
        //    btnDisassembly.Enabled = false;
        //    btnSave.Enabled = true;
        //}
        #endregion Old

        TabMain0.Visible = true;
        IGenericRepository<OUTASNCHANGE> con = new GenericRepository<OUTASNCHANGE>(context);
        var caseList = from p in con.Get()
                       where p.id == this.KeyID
                       select p;
        if (caseList.Count() > 0)
        {
            OUTASNCHANGE entity = caseList.ToList().FirstOrDefault();
         
            txtCTICKETCODE.Text = entity.cticketcode;
            //ERP单号
            txtCERPCODE.Text = entity.erpcode;
            //状态
            ddlCSTATUS.SelectedValue = entity.cstatus;
            if (entity.cstatus != "0")
            {
                btnCheck.Enabled = false;
            }
            //制单人
            txtCCREATEOWNERCODE.Text = entity.create_owner;
            //制单时间
            txtDCREATETIME.Text = entity.create_time.Value.ToString("yyyy-MM-dd HH:mm:ss");
            txtCMEMO.Text = entity.cmemo;
            //设置文本框是否可编辑
            txtCTICKETCODE.Enabled = false;
            txtCERPCODE.Enabled = false;
            ddlCSTATUS.Enabled = false;
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        string msg = string.Empty;
        //
        if (txtID.Text.Trim() == "")
        {
            Alert("ID项不允许空！");
            SetFocus(txtID);
            return false;
        }
        //
        if (txtID.Text.Trim().Length > 0)
        {
            if (txtID.Text.GetLengthByByte() > 50)
            {
                Alert("ID项超过指定的长度50！");
                SetFocus(txtID);
                return false;
            }
        }
        //
        if (txtCCREATEOWNERCODE.Text.Trim() == "")
        {
            Alert("制单人项不允许空！");
            SetFocus(txtCCREATEOWNERCODE);
            return false;
        }
        //
        if (txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (txtCCREATEOWNERCODE.Text.GetLengthByByte() > 50)
            {
                Alert("制单人项超过指定的长度50！");
                SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }
        //
        if (txtDCREATETIME.Text.Trim() == "")
        {
            Alert("制单日期项不允许空！");
            SetFocus(txtDCREATETIME);
            return false;
        }
        //
        if (txtCMEMO.Text.Trim().Length > 0)
        {
            if (txtCMEMO.Text.GetLengthByByte() > 200)
            {
                Alert("备注项超过指定的长度200！");
                SetFocus(txtCMEMO);
                return false;
            }
        }
        //
        if (txtCERPCODE.Text.Trim().Length == 0)
        {
            //Alert("ERP单号不能為空！");
            Alert("請在ERPCODE 欄位輸入工單號！");
            SetFocus(txtCERPCODE);
            return false;
        }

        return true;

    }

   
    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
        //取消灰化
        btnSave.Style.Remove("disabled");
    }

    private void SaveData()
    {
        string msg = string.Empty;
        IGenericRepository<OUTASNCHANGE> con = new GenericRepository<OUTASNCHANGE>(context);
        if (CheckData())
        {
            var strKeyID = "";
            try
            {
                if (Operation() == SYSOperation.Modify)
                {

                    strKeyID = txtID.Text.Trim();

                    OUTASNCHANGE entity = new OUTASNCHANGE();
                    entity.id = strKeyID;
                    entity.cmemo = txtCMEMO.Text.Trim();
                    entity.last_upd_owner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.last_upd_time = DateTime.Now;
                    con.Update(entity);


                    AlertAndBack("FrmOutChangeAsnEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), "更新成功");
                }
                else if (Operation() == SYSOperation.New)
                {
                    try
                    {
                        strKeyID = Guid.NewGuid().ToString();
                        //保存数据
                        //var proc = new proc_save_outchange();
                        //proc.pID = strKeyID;
                        //proc.pErpCode = txtCERPCODE.Text.Trim();
                        //proc.pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                        //proc.pMemo = txtCMEMO.Text.Trim();
                        //proc.Execute();
                        //if (proc.pRetCode == 1)
                        //{
                        //    AlertAndBack("FrmOutChangeAsnEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), "保存成功");
                        //}
                        //else
                        //{
                        //    Alert(proc.pRetMsg + " 保存失败!");
                        //}
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Alert(GetOperationName() + "失败！[" + ex.Message + "]");
            }
        }
    }


    //绑定网格
    public void GridBind()
    {
        //var listQuery = new OUT_FrmOutChangeCtl();
        //var dtSource = listQuery.GetList_D(txtID.Text, txtCinvcode.Text.Trim(), grdNavigatorOutChange_d.CurrentPageIndex,
        //                                   grdOutChange_d.PageSize, "1");
        //grdOutChange_d.DataSource = dtSource;
        //grdOutChange_d.DataBind();


        IGenericRepository<OUTASNCHANGE_D> entity = new GenericRepository<OUTASNCHANGE_D>(context);
        var caseList = from p in entity.Get()
                       orderby p.cinvcode, p.cerpcodeline
                       where 1 == 1
                       select p;
        if (txtID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.Contains(txtID.Text));


        if (txtCinvcode.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));
        }

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        AspNetPager1.CustomInfoHTML = " 总页数:<b>" + "</b>";
        grdOutChange_d.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdOutChange_d.DataBind();
    }

    //审核
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        IGenericRepository<OUTASNCHANGE> con = new GenericRepository<OUTASNCHANGE>(context);
        var caseList1 = from p in con.Get()
                       where p.id == this.KeyID && p.cstatus=="0"
                       select p;
        try
        {
            if (ddlCSTATUS.SelectedValue == "0" && caseList1.Count()>0)
            {
                var caseList = from p in con.Get()
                               where p.id == this.KeyID
                               select p;
                if (caseList.Count()>0)
                {
                    OUTASNCHANGE entity = caseList.ToList().FirstOrDefault();
                    entity.cauditperson = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.daudittime = DateTime.Now;
                    entity.last_upd_owner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.last_upd_time = DateTime.Now;
                    entity.cstatus = "1";
                    con.Update(entity);
                    con.Save();
                    msg = "审核成功";
                }
            }
            else
            {
                throw new Exception("只有未处理的单据才能审核");
            }
        }
        catch (Exception err)
        {
            msg = err.Message.ToJsString();
        }
        finally
        {
            btnCheck.Style.Remove("disabled");
        }

        Alert(msg);
        ShowData();
        btnSearch_Click(btnSearch, EventArgs.Empty);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    

    //完成
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            //保存数据
            //var proc = new proc_deal_outasnchange { pID = KeyID, pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo };
            //proc.Execute();
            //if (proc.pRetCode == 1)
            //{
            //    AlertAndBack("FrmOutChangeAsnEdit.aspx?" + BuildQueryString(SYSOperation.Modify, KeyID), "处理成功");
            //}
            //else
            //{
            //    Alert(proc.pRetMsg + " 处理失败!");
            //}
        }
        catch (Exception ex)
        {
            Alert(GetOperationName() + "失败！[" + ex.Message + "]");
        }

    }
    #endregion
    //page load
    #region oracle
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (IsPostBack == false)
    //    {
    //        InitPage();
    //        if (Operation == SYSOperation.Modify)//修改
    //        {
    //            ShowData();
    //            btnOK.Visible = true;
    //            btnCheck.Visible = false;
    //            btnSave.Visible = true;
    //        }
    //        else if (Operation == SYSOperation.Approve)//审核
    //        {
    //            ShowData();
    //            btnOK.Visible = false;
    //            btnSave.Visible = false;
    //            btnCheck.Visible = true;
    //        }
    //        else
    //        {
    //            //制单人
    //            txtCCREATEOWNERCODE.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
    //            //制单时间
    //            txtDCREATETIME.Text = CommonFunction.GetDBNowTime().Value.ToString();
    //            txtID.Text = Guid.NewGuid().ToString();
    //            //隐藏完成按钮
    //            btnOK.Visible = false;
    //            btnCheck.Visible = false;
    //            btnSave.Visible = true;
    //        }
    //        //查询
    //        btnSearch_Click(btnSearch, EventArgs.Empty);
    //    }
    //    FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
    //    HasRight();

    //    btnSave.Attributes["onclick"] = GetPostBackEventReference(btnSave) + ";disabled=true;";
    //    btnOK.Attributes["onclick"] = GetPostBackEventReference(btnOK) + ";disabled=true;";
    //    btnCheck.Attributes["onclick"] = GetPostBackEventReference(btnCheck) + ";disabled=true;";
    //}

    


    ///// <summary>
    ///// 初始化页面。主要做一下动作
    /////1、DropDownList,ListBox数据装载,
    /////2、新增按钮、删除的按钮的Java脚本注册等
    /////一般在PageLoad 事件调用，
    /////并且IsPostBack = false时
    ///// </summary>
    //public void InitPage()
    //{
    //    cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
    //    btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OutChange');return false;";
    //    //状态
    //    Help.DropDownListDataBind(new SYS_PARAMETERQuery().GetSys_ParameterByFLAG_TYPE("ASNCHANGE"), ddlCSTATUS, "全部", "FLAG_NAME", "FLAG_ID", "");
    //}


    ///// <summary>
    ///// 根据主键值，数据库内容填入输入项控件
    ///// </summary>
    //public void ShowData()
    //{
    //    #region Old
    //    //OUTASNEntity entity = new OUTASNEntity();
    //    //entity.ID = KeyID;
    //    //entity.SelectByPKeys();
    //    //txtID.Text = entity.ID;
    //    //txtOutAsnCTICKETCODE.Text = entity.CTICKETCODE;
    //    //ddlCSTATUS.SelectedValue = entity.CSTATUS;
    //    //OutType = entity.CSTATUS;
    //    //txtITYPE.SelectedValue = entity.ITYPE.ToString();
    //    //txtCCLIENTCODE.Text = entity.CCLIENTCODE;
    //    //txtCCLIENT.Text = entity.CCLIENT;
    //    //txtCSO.Text = entity.CSO;
    //    //txtCCREATEOWNERCODE.Text = entity.CCREATEOWNERCODE;

    //    //if (entity.DCREATETIME != null)
    //    //{
    //    //    txtDCREATETIME.Text = entity.DCREATETIME.ToString("yyyy-MM-dd HH:mm:ss");
    //    //}
    //    //txtCERPCODE.Text = entity.CERPCODE;
    //    //txtCAUDITPERSONCODE.Text = entity.CAUDITPERSONCODE;
    //    //txtDAUDITDATE.Text = entity.DAUDITDATE.HasValue ? entity.DAUDITDATE.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
    //    //txtCMEMO.Text = entity.CMEMO;
    //    //txtCDEFINE1.Text = entity.CDEFINE1;
    //    //TabMain0.Visible = true;
    //    //Status = entity.CSTATUS;
    //    //CDEFINE2 = entity.CDEFINE2;
    //    //IsSpecialWIP_Issue = entity.IDEFINE5.HasValue ? entity.IDEFINE5.Value : 0;
    //    //OutType = entity.ITYPE.ToString();
    //    //btnImportExcel.Enabled = true;
    //    //if (!entity.CSTATUS.Equals("0") || !entity.CDEFINE2.Equals("0") || OutType.Equals("33"))
    //    //{
    //    //    SetTblFormControlEnabled(false);
    //    //}
    //    ////状态为 未处理 并且 数据来原为 1 [数据来源 ( 0 : WMS，1 :oracle ERP )]的生成出库单按钮可用
    //    //if (entity.CSTATUS.Equals("0") && entity.CDEFINE2.Equals("1"))
    //    //{
    //    //    btnCreateOutBill.Enabled = true;
    //    //}
    //    ////WIP Issue : 35 、Sales order issue ：33
    //    //if (string.IsNullOrEmpty(entity.CDEFINE2)/*Roegr 20130509 增加判空处理*/ || !entity.CDEFINE2.Equals("0") || OutType.Equals("33"))
    //    //{
    //    //    btnDelete.Enabled = false;
    //    //    btnNew.Enabled = false;
    //    //    btnSave.Enabled = false;
    //    //    btnImportExcel.Enabled = false;
    //    //}
    //    ////Return to Vendor 
    //    //if (OutType.Equals("36"))
    //    //{
    //    //    btnImportExcel.Enabled = false;
    //    //}
    //    ////特殊元件领料
    //    //try
    //    //{
    //    //    if (OutType.Equals("35") && Request.QueryString["IsSpecialPage"].Equals("0") && IsSpecialWIP_Issue == 1)//IsSpecialPage
    //    //    {
    //    //        SetTblFormControlEnabled(false);
    //    //    }
    //    //}
    //    //catch (Exception)
    //    //{

    //    //}
    //    //btnImportExcel.Attributes["onclick"] = "PopupFloatWin('" + BuildRequestPageURL("/Apps/Import/FrmImportAsnDetail.aspx", SYSOperation.New, "") + "&AsnId=" + KeyID + "&CTICKETCODE=" + txtOutAsnCTICKETCODE.Text.Trim() + "&ImportType=Out','上传出库通知单明细','OutChange_d',600,320); return false;";
    //    ////WIP Completion Return 工单完工退 17
    //    //if (OutType.Equals("17"))
    //    //{
    //    //    SetTblFormControlEnabled(false);
    //    //}

    //    ////Roger 2013-5-2 17:48:49 合并后通知单不允许补单
    //    //if (!OutChange_dRule.IsMerge(KeyID))
    //    //{
    //    //    btnDelete.Enabled = false;
    //    //    btnNew.Enabled = false;
    //    //    btnSave.Enabled = false;
    //    //    btnCreateOutBill.Enabled = false;
    //    //    btnImportExcel.Enabled = false;
    //    //    btnDisassembly.Enabled = false;
    //    //}

    //    ////20130617112810
    //    ////特殊超领的，啥都不允许操作
    //    //if (entity.SPECIAL_OUT.Equals("1"))
    //    //{
    //    //    SetTblFormControlEnabled(false);
    //    //    btnDisassembly.Enabled = false;
    //    //    btnSave.Enabled = true;
    //    //}
    //    #endregion Old

    //    TabMain0.Visible = true;
    //    var entity = new OutAsnChangeEntity();
    //    entity.ID = KeyID;
    //    entity.SelectByPKeys();
    //    txtID.Text = entity.ID;
    //    //单据号
    //    txtCTICKETCODE.Text = entity.CTICKETCODE;
    //    //ERP单号
    //    txtCERPCODE.Text = entity.ERPCODE;
    //    //状态
    //    ddlCSTATUS.SelectedValue = entity.CSTATUS;
    //    if (entity.CSTATUS!="0")
    //    {
    //        btnCheck.Enabled = false;
    //    }
    //    //制单人
    //    txtCCREATEOWNERCODE.Text = entity.CREATE_OWNER;
    //    //制单时间
    //    txtDCREATETIME.Text = entity.CREATE_TIME.Value.ToString("yyyy-MM-dd HH:mm:ss");
    //    txtCMEMO.Text = entity.CMEMO;
    //    //设置文本框是否可编辑
    //    txtCTICKETCODE.Enabled = false;
    //    txtCERPCODE.Enabled = false;
    //    ddlCSTATUS.Enabled = false;
    //}

    ///// <summary>
    ///// 校验数据
    ///// </summary>
    ///// <returns></returns>
    //public bool CheckData()
    //{
    //    string msg = string.Empty;
    //    //
    //    if (txtID.Text.Trim() == "")
    //    {
    //        Alert("ID项不允许空！");
    //        SetFocus(txtID);
    //        return false;
    //    }
    //    //
    //    if (txtID.Text.Trim().Length > 0)
    //    {
    //        if (txtID.Text.GetLengthByByte() > 50)
    //        {
    //            Alert("ID项超过指定的长度50！");
    //            SetFocus(txtID);
    //            return false;
    //        }
    //    }
    //    //
    //    if (txtCCREATEOWNERCODE.Text.Trim() == "")
    //    {
    //        Alert("制单人项不允许空！");
    //        SetFocus(txtCCREATEOWNERCODE);
    //        return false;
    //    }
    //    //
    //    if (txtCCREATEOWNERCODE.Text.Trim().Length > 0)
    //    {
    //        if (txtCCREATEOWNERCODE.Text.GetLengthByByte() > 50)
    //        {
    //            Alert("制单人项超过指定的长度50！");
    //            SetFocus(txtCCREATEOWNERCODE);
    //            return false;
    //        }
    //    }
    //    //
    //    if (txtDCREATETIME.Text.Trim() == "")
    //    {
    //        Alert("制单日期项不允许空！");
    //        SetFocus(txtDCREATETIME);
    //        return false;
    //    }
    //    //
    //    if (txtCMEMO.Text.Trim().Length > 0)
    //    {
    //        if (txtCMEMO.Text.GetLengthByByte() > 200)
    //        {
    //            Alert("备注项超过指定的长度200！");
    //            SetFocus(txtCMEMO);
    //            return false;
    //        }
    //    }
    //    //
    //    if (txtCERPCODE.Text.Trim().Length == 0)
    //    {
    //        //Alert("ERP单号不能為空！");
    //        Alert("請在ERPCODE 欄位輸入工單號！");
    //        SetFocus(txtCERPCODE);
    //        return false;
    //    }

    //    return true;

    //}

    ///// <summary>
    ///// 根据页面上的数据构造相应的实体类返回
    ///// </summary>
    ///// <returns></returns>
    //public BaseEntity SendData()
    //{
    //    #region Old

    //    //OUTASNEntity entity = new OUTASNEntity();
    //    ////
    //    //txtID.Text = txtID.Text.Trim();
    //    //if (txtID.Text.Length > 0)
    //    //{
    //    //    entity.ID = txtID.Text;
    //    //}
    //    //else
    //    //{
    //    //    entity.SetDBNull("ID", true);
    //    //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    //    //entity.ID = null;
    //    //}
    //    ////
    //    //if (Operation == SYSOperation.New)
    //    //{
    //    //    txtOutAsnCTICKETCODE.Text = txtOutAsnCTICKETCODE.Text.Trim();
    //    //    if (txtOutAsnCTICKETCODE.Text.Length > 0)
    //    //    {
    //    //        entity.CTICKETCODE = txtOutAsnCTICKETCODE.Text;
    //    //    }
    //    //    else
    //    //    {
    //    //        entity.CTICKETCODE = new Fun_CreateNo().CreateNo("OUTASN");
    //    //        //entity.SetDBNull("CTICKETCODE", true);
    //    //        //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    //        //entity.CTICKETCODE = null;
    //    //    }
    //    //    entity.CSTATUS = "0";
    //    //}

    //    ////
    //    ////txtITYPE.Text = txtITYPE.Text.Trim();
    //    ////if (txtITYPE.Text.Length > 0)
    //    ////{
    //    //entity.ITYPE = decimal.Parse(txtITYPE.SelectedValue);
    //    ////}
    //    ////else
    //    ////{
    //    ////    entity.SetDBNull("ITYPE", true);
    //    ////    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    ////    //entity.ITYPE = null;
    //    ////}
    //    ////
    //    //txtCCLIENTCODE.Text = txtCCLIENTCODE.Text.Trim();
    //    //if (txtCCLIENTCODE.Text.Length > 0)
    //    //{
    //    //    entity.CCLIENTCODE = txtCCLIENTCODE.Text;
    //    //}
    //    //else
    //    //{
    //    //    entity.SetDBNull("CCLIENTCODE", true);
    //    //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    //    //entity.CCLIENTCODE = null;
    //    //}
    //    ////
    //    //txtCCLIENT.Text = txtCCLIENT.Text.Trim();
    //    //if (txtCCLIENT.Text.Length > 0)
    //    //{
    //    //    entity.CCLIENT = txtCCLIENT.Text;
    //    //}
    //    //else
    //    //{
    //    //    entity.SetDBNull("CCLIENT", true);
    //    //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    //    //entity.CCLIENT = null;
    //    //}
    //    ////
    //    //txtCSO.Text = txtCSO.Text.Trim();
    //    //if (txtCSO.Text.Length > 0)
    //    //{
    //    //    entity.CSO = txtCSO.Text;
    //    //}
    //    //else
    //    //{
    //    //    entity.SetDBNull("CSO", true);
    //    //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    //    //entity.CSO = null;
    //    //}
    //    ////
    //    //txtCCREATEOWNERCODE.Text = txtCCREATEOWNERCODE.Text.Trim();
    //    //if (txtCCREATEOWNERCODE.Text.Length > 0)
    //    //{
    //    //    entity.CCREATEOWNERCODE = txtCCREATEOWNERCODE.Text;
    //    //}
    //    //else
    //    //{
    //    //    entity.SetDBNull("CCREATEOWNERCODE", true);
    //    //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    //    //entity.CCREATEOWNERCODE = null;
    //    //}
    //    ////
    //    //txtDCREATETIME.Text = txtDCREATETIME.Text.Trim();
    //    //if (txtDCREATETIME.Text.Length > 0)
    //    //{
    //    //    entity.DCREATETIME = txtDCREATETIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
    //    //}
    //    //else
    //    //{
    //    //    entity.SetDBNull("DCREATETIME", true);
    //    //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    //    //entity.DCREATETIME = null;
    //    //}
    //    ////
    //    //txtCERPCODE.Text = txtCERPCODE.Text.Trim();
    //    //if (txtCERPCODE.Text.Length > 0)
    //    //{
    //    //    entity.CERPCODE = txtCERPCODE.Text;
    //    //}
    //    //else
    //    //{
    //    //    entity.SetDBNull("CERPCODE", true);
    //    //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    //    //entity.CERPCODE = null;
    //    //}
    //    ////
    //    //txtCAUDITPERSONCODE.Text = txtCAUDITPERSONCODE.Text.Trim();
    //    //if (txtCAUDITPERSONCODE.Text.Length > 0)
    //    //{
    //    //    entity.CAUDITPERSONCODE = txtCAUDITPERSONCODE.Text;
    //    //}
    //    //else
    //    //{
    //    //    entity.SetDBNull("CAUDITPERSONCODE", true);
    //    //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    //    //entity.CAUDITPERSONCODE = null;
    //    //}
    //    ////
    //    //txtDAUDITDATE.Text = txtDAUDITDATE.Text.Trim();
    //    //if (txtDAUDITDATE.Text.Length > 0)
    //    //{
    //    //    entity.DAUDITDATE = txtDAUDITDATE.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
    //    //}
    //    //else
    //    //{
    //    //    entity.SetDBNull("DAUDITDATE", true);
    //    //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    //    //entity.DAUDITDATE = null;
    //    //}
    //    //txtCDEFINE1.Text = txtCDEFINE1.Text.Trim();
    //    //if (txtCDEFINE1.Text.Length > 0)
    //    //{
    //    //    entity.CDEFINE1 = txtCDEFINE1.Text;
    //    //}
    //    //else
    //    //{
    //    //    entity.SetDBNull("CDEFINE1", true);
    //    //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    //    //entity.CMEMO = null;
    //    //}
    //    ////
    //    //txtCMEMO.Text = txtCMEMO.Text.Trim();
    //    //if (txtCMEMO.Text.Length > 0)
    //    //{
    //    //    entity.CMEMO = txtCMEMO.Text;
    //    //}
    //    //else
    //    //{
    //    //    entity.SetDBNull("CMEMO", true);
    //    //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
    //    //    //entity.CMEMO = null;
    //    //}

    //    //entity.CDEFINE2 = "0";//数量来源 ( 0 : WMS，1 :oracle ERP )
    //    //entity.IDEFINE5 = IsSpecialWIP_Issue;

    //    ////20130516143828
    //    //if (txtITYPE.Text.Trim().Equals("35"))
    //    //{
    //    //    var dtMo = OUTASNRule.GetOraCode(entity.CERPCODE);
    //    //    if (dtMo != null && dtMo.Rows.Count > 0)
    //    //    {
    //    //        //工单完工料号
    //    //        entity.FG_CINVCODE = dtMo.Rows[0][0].ToString().Trim();
    //    //        //Class_Code
    //    //        entity.CLASS_CODE = dtMo.Rows[0][1].ToString().Trim();
    //    //    }
    //    //}


    //    //#region 界面上不可见的字段项
    //    ///*
    //    //entity.CDEFINE1 = ;
    //    //entity.CDEFINE2 = ;
    //    //entity.DDEFINE3 = ;
    //    //entity.DDEFINE4 = ;
    //    //entity.IDEFINE5 = ;
    //    //*/
    //    //#endregion
    //    //return entity;

    //    #endregion Old

    //    return null;
    //}

    ////保存
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    SaveData();
    //    //取消灰化
    //    btnSave.Style.Remove("disabled");
    //}

    //private void SaveData()
    //{
    //    string msg = string.Empty;
    //    if (CheckData())
    //    {
    //        var strKeyID = "";
    //        try
    //        {
    //            if (Operation == SYSOperation.Modify)
    //            {
    //                strKeyID = txtID.Text.Trim();
    //                var entity =new OutAsnChangeEntity {ID = strKeyID};
    //                entity.SelectByPKeys();
    //                entity.CMEMO = txtCMEMO.Text.Trim();
    //                entity.LAST_UPD_OWNER = WmsWebUserInfo.GetCurrentUser().UserNo;
    //                entity.LAST_UPD_TIME = CommonFunction.GetDBNowTime().Value;
    //                OUT_FrmOutChangeCtl.Update(entity);
    //                DBUtil.Commit();
    //                AlertAndBack("FrmOutChangeAsnEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), "更新成功");
    //            }
    //            else if (Operation == SYSOperation.New)
    //            {
    //                DBUtil.BeginTrans();
    //                try
    //                {
    //                    strKeyID = Guid.NewGuid().ToString();
    //                    //保存数据
    //                    var proc = new proc_save_outchange();
    //                    proc.pID = strKeyID;
    //                    proc.pErpCode = txtCERPCODE.Text.Trim();
    //                    proc.pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
    //                    proc.pMemo = txtCMEMO.Text.Trim();
    //                    proc.Execute();
    //                    if (proc.pRetCode == 1)
    //                    {
    //                        DBUtil.Commit();
    //                        AlertAndBack("FrmOutChangeAsnEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), "保存成功");
    //                    }
    //                    else
    //                    {
    //                        DBUtil.Rollback();
    //                        Alert(proc.pRetMsg + " 保存失败!");
    //                    }
    //                }
    //                catch (Exception)
    //                {
    //                    DBUtil.Rollback();
    //                    throw;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Alert(GetOperationName() + "失败！[" + ex.Message + "]");
    //        }
    //    }
    //}

    ////导出网格
    //protected DataTable grdNavigatorOutChange_d_GetExportToExcelSource()
    //{
    //    var listQuery = new OUT_FrmOutChangeCtl();
    //    var dtSource = listQuery.GetList_D(txtID.Text, txtCinvcode.Text.Trim(), 0, 0, "0");
    //    return dtSource;
    //}

    ////分页1
    //protected void grdOutChange_d_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    if (grdNavigatorOutChange_d.IsDbPager)
    //    {
    //        grdNavigatorOutChange_d.CurrentPageIndex = e.NewPageIndex;
    //    }
    //    else
    //    {
    //        grdOutChange_d.PageIndex = e.NewPageIndex;
    //    }
    //}

    ////分页2
    //protected void grdOutChange_d_PageIndexChanged(object sender, EventArgs e)
    //{
    //    GridBind();
    //}

    ////查询
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    //重新设置GridNavigator的RowCount
    //    var listQuery = new OUT_FrmOutChangeCtl();
    //    var dtRowCount = listQuery.GetList_D(txtID.Text, txtCinvcode.Text.Trim(), 0, 0, "0");
    //    if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
    //    {
    //        grdNavigatorOutChange_d.RowCount = dtRowCount.Rows.Count;
    //    }
    //    else
    //    {
    //        grdNavigatorOutChange_d.RowCount = 0;
    //    }
    //    //绑定网格
    //    GridBind();
    //}

    ////绑定网格
    //public void GridBind()
    //{
    //    var listQuery = new OUT_FrmOutChangeCtl();
    //    var dtSource = listQuery.GetList_D(txtID.Text, txtCinvcode.Text.Trim(), grdNavigatorOutChange_d.CurrentPageIndex,
    //                                       grdOutChange_d.PageSize, "1");
    //    grdOutChange_d.DataSource = dtSource;
    //    grdOutChange_d.DataBind();
    //}

    ////审核
    //protected void btnCheck_Click(object sender, EventArgs e)
    //{
    //    var msg = string.Empty;
    //    try
    //    {
    //        if (ddlCSTATUS.SelectedValue == "0" && OUT_FrmOutChangeCtl.ValidateOutChangeStatus(KeyID, 0))
    //        {
    //            var entity = new OutAsnChangeEntity {ID = KeyID};
    //            if (entity.SelectByPKeys())
    //            {
    //                entity.CAUDITPERSON = WmsWebUserInfo.GetCurrentUser().UserNo;
    //                entity.DAUDITTIME = CommonFunction.GetDBNowTime().Value;
    //                entity.LAST_UPD_OWNER = WmsWebUserInfo.GetCurrentUser().UserNo;
    //                entity.LAST_UPD_TIME = CommonFunction.GetDBNowTime().Value;
    //                entity.CSTATUS = "1";
    //                OUT_FrmOutChangeCtl.Update(entity);
    //                msg = "审核成功";
    //            }
    //        }
    //        else
    //        {
    //            throw new Exception("只有未处理的单据才能审核");
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        msg = err.Message.ToJsString();
    //    }
    //    finally
    //    {
    //        btnCheck.Style.Remove("disabled");
    //    }

    //    Alert(msg);
    //    ShowData();
    //    btnSearch_Click(btnSearch, EventArgs.Empty);
    //}

    ////完成
    //protected void btnOK_Click(object sender, EventArgs e)
    //{
    //    DBUtil.BeginTrans();
    //    try
    //    {
    //        //保存数据
    //        var proc = new proc_deal_outasnchange {pID = KeyID, pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo};
    //        proc.Execute();
    //        if (proc.pRetCode == 1)
    //        {
    //            DBUtil.Commit();
    //            AlertAndBack("FrmOutChangeAsnEdit.aspx?" + BuildQueryString(SYSOperation.Modify, KeyID), "处理成功");
    //        }
    //        else
    //        {
    //            DBUtil.Rollback();
    //            Alert(proc.pRetMsg + " 处理失败!");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        DBUtil.Rollback();
    //        Alert(GetOperationName() + "失败！[" + ex.Message + "]");
    //    }

    //}
    #endregion 
}