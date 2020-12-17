using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.Entity.SqlServer;

public partial class Apps_RD_FrmMixed_D : WMSBasePage
{
    //DBContext context = new DBContext();    
    protected void Page_Load(object sender, EventArgs e)
    {
        ucShowOutASNMixed_Div.SetCompName = txtERP_No.ClientID;
        ucShowOutASNMixed_Div.SetORGCode = hiddenGuid.ClientID;
        //ucShowOutASNMixed_Div.SetErpCode = txtERP_No.ClientID;
        //ucShowOutASNMixed_Div.SetPalletCode = txtPalledCode.ClientID;
        if (IsPostBack == false)
        {
            this.InitPage();
            GridBind();
        }
    }

    #region IPageGrid 成员

    #endregion

    #region IPage 成员
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('Mixed_D');return false;";
        //this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        txtERP_No.Attributes["onclick"] = "Show('" + ucShowOutASNMixed_Div.GetDivName + "');";
        //本页面打开新增窗口

        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        Help.DropDownListDataBind(GetParametersByFlagType("MixedStatus"), this.ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(GetOutType(true), this.txtITYPE, Resources.Lang.Common_ALL, "FUNCNAME", "EXTEND1", "");//全部
        //Help.DropDownListDataBind(GetAGVSite("1"), this.ddlSiteType, "请选择", "SITENAME", "SITEID", "");
        BindMixedSiteType();
        Help.DropDownListDataBind(GetAGVSite("1"), this.ddlStartSite, Resources.Lang.Common_ALL, "SITENAME", "SITEID", ""); //全部
        Help.DropDownListDataBind(GetAGVSite("2"), this.ddlEndSite, Resources.Lang.Common_ALL, "SITENAME", "SITEID", "");  //全部      
        if (this.Operation() == SYSOperation.Modify)
        {
            this.txtID.Text = this.KeyID;
            ShowData();
            this.txtERP_No.Enabled = false;
            this.ddlSiteType.Enabled = false;
            //检查配料单是否已暂存叫车
            IGenericRepository<WCS_TaskProcess> con = new GenericRepository<WCS_TaskProcess>(context);
            var caseList = from p in con.Get()
                           orderby p.CREATETIME descending
                           where p.SOURCECODE == txtMixedCode.Text.Trim()
                           select p;
            if (caseList != null && caseList.ToList().Count > 0)
            {
                ControlBtn();
            }
            if (txtCDEFINE2.Text.Trim() != "2")
            {
                ControlBtn();
            }
        }
        if (this.Operation() == SYSOperation.View)
        {
            this.txtID.Text = this.KeyID;
            ShowData();
            ControlBtn();
        }
        if (this.Operation() == SYSOperation.New)
        {
            btnNew.Attributes["style"] = "color:#b5b5b5";
            btnNew.Enabled = false;
            btnDelete.Attributes["style"] = "color:#b5b5b5";
            btnDelete.Enabled = false;
            txtCreaterUser.Text =OPERATOR.GetUserNameByAccountID( WmsWebUserInfo.GetCurrentUser().UserNo);
            this.txtID.Text = Guid.NewGuid().ToString();
            txtCreaterTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtMixedCode.Text = GetMixedCode();
        }
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmMixed_DEdit.aspx?MixedID=" + this.txtID.Text + "&CerpCode=" + this.txtERP_No.Text.Trim() + "&PalletCode=" + this.hdnPalletCode.Value, SYSOperation.New, "") + "','"+ Resources.Lang.FrmMixed_D_Msg05 +"','Mixed_DEdit');return false;";
    }//新增配料明细
    #endregion

    #region even
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<OUTMIXED> con = new GenericRepository<OUTMIXED>(context);
        try
        {
            if (this.CheckData())
            {
                OUTMIXED entity = this.SendData();
                string strKeyID = "";
                strKeyID = this.txtID.Text.Trim();
                if (this.Operation() == SYSOperation.Modify)
                {
                    con.Update(entity);
                    con.Save();
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    con.Insert(entity);
                    con.Save();
                }
                this.AlertAndBack("FrmMixed_D.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
            }
        }
        catch (Exception E)
        {
            this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATEEdit_Msg17 + E.Message); //"失败！"
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            for (int i = 0; i < this.grdMIXED_D.Rows.Count; i++)
            {
                if (this.grdMIXED_D.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdMIXED_D.Rows[i].Cells[0].Controls[1];
                    // string status = this.grdINBILL.Rows[i].Cells[10].Text;
                    if (chkSelect.Checked)
                    {
                        if (this.ddlCSTATUS.SelectedValue.Equals("0")) //配料中 //0	配料中  1	暂存中   3	已完成  2	已交付  // if (this.ddlCSTATUS.SelectedValue.Equals("配料中"))
                        {
                            msg += Resources.Lang.FrmMixedList_Title02 + "[" + this.txtMixedCode.Text + "]" + Resources.Lang.FrmMixedList_Msg05; //"配料单[" + this.txtMixedCode.Text + "]状态必须为配料中！";
                            break;
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                for (int i = 0; i < this.grdMIXED_D.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)this.grdMIXED_D.Rows[i].Cells[0].Controls[1];
                    // string status = this.grdINBILL.Rows[i].Cells[10].Text;
                    if (chkSelect.Checked)
                    {
                        string mixedids = this.grdMIXED_D.DataKeys[i].Values[0].ToString();
                        //删除明细，删除配料信息
                        DeleteOutMixed(mixedids, this.grdMIXED_D.Rows[i].Cells[2].Text);
                    }
                }
                msg = Resources.Lang.Common_SuccessDel;//删除成功！
            }
            else
            {
                msg = Resources.Lang.Common_FailDel + msg; //"删除失败！"
            }
            btnSearch_Click(null, null);
        }
        catch (Exception E)
        {
            //this.Alert(Resources.Lang.Common_SuccessDel + E.Message.ToJsString());  //删除失败！
            msg += Resources.Lang.Common_FailDel;  //"删除失败！"
            //DBUtil.Rollback(); 
        }
        this.Alert(msg);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            CurrendIndex = 1;
            this.GridBind();
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }
    #endregion

    #region funtion
    private void ControlBtn()
    {
        btnNew.Attributes["style"] = "color:#b5b5b5";
        btnNew.Enabled = false;
        btnDelete.Attributes["style"] = "color:#b5b5b5";
        btnDelete.Enabled = false;
        btnSave.Attributes["style"] = "color:#b5b5b5";
        btnSave.Enabled = false;
    }

    private void DeleteOutMixed(string ids, string cinvcode)
    {
        string strSQL = string.Format(@"DELETE  dbo.OUTMIXED_D
                                        WHERE   IDS = '{0}';
                                        DELETE  dbo.TEMP_OUTMIXED_D
                                        WHERE   CERPCODE = '{1}'
                                                AND cinvcode = '{2}'
                                                AND inbillcticketcode = '{3}'
                                                AND cstatus = '1';", ids, this.txtERP_No.Text.Trim(), cinvcode, this.txtMixedCode.Text.Trim());
        DBHelp.ExecuteNonQuery(strSQL);
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void GridBind()
    {
        IGenericRepository<V_OUTMIXED_D> con = new GenericRepository<V_OUTMIXED_D>(context);
        var caseList = from p in con.Get()
                       orderby p.CREATERTIME ascending
                       where p.ID == this.txtID.Text.Trim()
                       select p;
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
            AspNetPager1.RecordCount = 0;
        AspNetPager1.CustomInfoHTML =Resources.Lang.FrmALLOCATEList_TotalPages + ":<b>" + "</b>"; //Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";
        grdMIXED_D.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdMIXED_D.DataBind();
    }

    public void ShowData()
    {
        IGenericRepository<OUTMIXED> con = new GenericRepository<OUTMIXED>(context);
        var caseList = from p in con.Get()
                       where p.ID == this.txtID.Text.Trim()
                       select p;
        OUTMIXED entity = caseList.ToList().FirstOrDefault<OUTMIXED>();
        entity.ID = this.txtID.Text.Trim();
        this.txtMixedCode.Text = entity.MIXEDCODE;
        //this.txtOutCticketCode.Text = entity.OUTCTICKETCODE;
        this.txtERP_No.Text = entity.ERPCODE;
        this.hdnCerpCode.Value = entity.ERPCODE;
        this.txtPalledCode.Text = entity.CINVBARCODE;
        this.hdnPalletCode.Value = entity.CINVBARCODE;
        this.txtITYPE.SelectedValue = entity.OTYPE.ToString();
        this.hdnOtype.Value = entity.OTYPE.ToString();
        this.ddlCSTATUS.SelectedValue = entity.CSTATUS;
        //this.txtStartSite.Text =entity.STARTSITE;
        //this.txtEndSite.Text = entity.ENDSITE;
        this.ddlStartSite.SelectedValue = entity.STARTSITE;
        this.ddlEndSite.SelectedValue = entity.ENDSITE;

        this.txtCreaterUser.Text = entity.CREATERUSER;
        this.txtCreaterTime.Text = Convert.ToDateTime(entity.CREATERTIME).ToString("yyyy-MM-dd HH:mm:ss");
        if (!string.IsNullOrEmpty(entity.MODIFYUSER))
        {
            this.txtModifUser.Text = entity.MODIFYUSER;
            this.txtModifTime.Text = Convert.ToDateTime(entity.MODIFYTIME).ToString("yyyy-MM-dd HH:mm:ss");
        }
        this.txtCDEFINE2.Text = entity.CDEFIEND2;
        if(!string.IsNullOrEmpty(entity.MIXEDSITE))
           this.ddlSiteType.SelectedValue = entity.MIXEDSITE;
    }

    private OUTMIXED SendData()
    {
        OUTMIXED entity = new OUTMIXED();
        entity.ID = this.txtID.Text.Trim();
        if (this.Operation() == SYSOperation.New)
        {
            entity.CREATERUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.CREATERTIME = DateTime.Now;
        }
        if (this.Operation() == SYSOperation.Modify)
        {
            entity.MODIFYTIME = DateTime.Now;
            entity.MODIFYUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.CREATERUSER = this.txtCreaterUser.Text.Trim();
            entity.CREATERTIME = Convert.ToDateTime(this.txtCreaterTime.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtMixedCode.Text.Trim()))
        {
            entity.MIXEDCODE = txtMixedCode.Text.Trim();
        }
        //if (!string.IsNullOrEmpty(txtOutCticketCode.Text.Trim()))
        //{
        //    entity.OUTCTICKETCODE = txtOutCticketCode.Text.Trim();
        //}
        if (!string.IsNullOrEmpty(txtERP_No.Text.Trim()))
        {
            entity.ERPCODE = txtERP_No.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txtPalledCode.Text.Trim()))
        {
            entity.CINVBARCODE = txtPalledCode.Text.Trim();
        }
        if (!string.IsNullOrEmpty(hdnOtype.Value))
        {
            entity.OTYPE = Convert.ToDecimal(hdnOtype.Value);
        }
        if (!string.IsNullOrEmpty(ddlCSTATUS.SelectedValue))
        {
            entity.CSTATUS = ddlCSTATUS.SelectedValue;
        }
        //if (!string.IsNullOrEmpty(txtStartSite.Text.Trim()))
        //{
        //    entity.STARTSITE = txtStartSite.Text.Trim();
        //}
        //if (!string.IsNullOrEmpty(txtEndSite.Text.Trim()))
        //{
        //    entity.ENDSITE = txtEndSite.Text.Trim();
        //}
        if (!string.IsNullOrEmpty(ddlStartSite.SelectedValue))
        {
            entity.STARTSITE = ddlStartSite.SelectedValue;
        }
        if (!string.IsNullOrEmpty(ddlEndSite.SelectedValue))
        {
            entity.ENDSITE = ddlEndSite.SelectedValue;
        }
        if (!string.IsNullOrEmpty(txtRemark.Text.Trim()))
        {
            entity.REMARK = txtRemark.Text.Trim();
        }
        if (!string.IsNullOrEmpty(ddlSiteType.SelectedValue.Trim()))
        {
            entity.MIXEDSITE = ddlSiteType.SelectedValue;
        }
        entity.CDEFIEND2 = "2";  // 1 :PDA   2:Web
        return entity;
    }

    public bool CheckData()
    {
        //配料单单号
        if (this.txtMixedCode.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmMixed_D_Msg06);//配料单单号项不允许空！
            this.SetFocus(txtMixedCode);
            return false;
        }
        //
        if (this.txtMixedCode.Text.Trim().Length > 0)
        {
            if (this.txtMixedCode.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmMixed_D_Msg07);//配料单单号项超过指定的长度50！
                this.SetFocus(txtMixedCode);
                return false;
            }
        }

        //出库单单号
        //if (this.txtOutCticketCode.Text.Trim() == "")
        //{
        //    this.Alert("出库单单号项不允许空！");
        //    this.SetFocus(txtOutCticketCode);
        //    return false;
        //}
        ////
        //if (this.txtOutCticketCode.Text.Trim().Length > 0)
        //{
        //    if (this.txtOutCticketCode.Text.GetLengthByByte() > 30)
        //    {
        //        this.Alert("出库单单号项超过指定的长度30！");
        //        this.SetFocus(txtOutCticketCode);
        //        return false;
        //    }
        //}

        //ERPCODE
        if (this.txtERP_No.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmMixed_D_Msg08);//ERPCODE项不允许空！
            this.SetFocus(txtERP_No);
            return false;
        }

        if (this.txtERP_No.Text.Trim().Length > 0)
        {
            if (this.txtERP_No.Text.Trim().GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmMixed_D_Msg09);//ERPCODE项超过指定的长度30！
                this.SetFocus(txtERP_No);
                return false;
            }
        }

        //栈板/箱号
        if (this.txtPalledCode.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmMixed_D_Msg10); //栈板/箱号项不允许空！
            this.SetFocus(txtPalledCode);
            return false;
        }
        //
        if (this.txtPalledCode.Text.Trim().Length > 0)
        {
            if (this.txtPalledCode.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmMixed_D_Msg11); //栈板/箱号项超过指定的长度50！
                this.SetFocus(txtPalledCode);
                return false;
            }
            // ---BUCKINGHA-838 条码管理修改CH Start
            BarCodeInfo barcodeentity = new BarCodeInfo();
            barcodeentity = GetBarCodeInfoBySn(txtPalledCode.Text.Trim());
            if (barcodeentity != null && barcodeentity.ReturnValue == "1")
            {
                this.Alert(barcodeentity.ErrorMsg);
                this.SetFocus(txtPalledCode);
                return false;
            }
            // ---BUCKINGHA-838 条码管理修改CH END

        }
        string retVal = JudgePC(txtERP_No.Text.Trim(), this.txtPalledCode.Text.Trim());
        if (retVal != "OK")
        {
            this.Alert(retVal);
            this.SetFocus(txtPalledCode);
            return false;
        }
        //检查栈板/箱号是否已存在

        //出库类型
        if (string.IsNullOrEmpty(hdnOtype.Value))
        {
            this.Alert(Resources.Lang.FrmMixed_D_Msg12); //出库类型项不允许空！
            this.SetFocus(txtITYPE);
            return false;
        }

        //状态
        if (string.IsNullOrEmpty(ddlCSTATUS.SelectedValue))
        {
            this.Alert(Resources.Lang.FrmMixed_D_Msg13);//状态项不允许空！
            this.SetFocus(ddlCSTATUS);
            return false;
        }
        return true;
    }

    private string JudgePC(string erpcode, string palletcode)
    {
        string strSQL = string.Format(@"SELECT dbo.Fun_CheckPCRepeat('{0}','{1}','{2}')", erpcode, palletcode, txtMixedCode.Text.Trim());
        return DBHelp.ExecuteScalar(strSQL);
    }

    public string GetMixedCode()
    {
        string strSQL = string.Format("SELECT dbo.Fun_CreateNo('OUTMIXED')");
        return DBHelp.ExecuteScalar(strSQL);
    }  
//    private DataTable GetAGVSite(string sitetype)
//    {
//        string strSQL = string.Format(@"SELECT  SITEID ,
//                                                SITENAME + SITEID SITENAME
//                                        FROM    dbo.BASE_CRANECONFIG_DETIAL
//                                        WHERE   FLAG = '0'
//                                                AND AGVSITETYPE = '{0}'", sitetype);
//        return DBHelp.ExecuteToDataTable(strSQL);
//    }
    public void BindMixedSiteType()
    {
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);       
        var caseList = from s in con.Get()
                       where s.AGVSITETYPE == "1" //配料位
                       select new
                       {
                           SITEID = s.SITEID,
                           SITENAME = s.SITENAME
                       };
        if (caseList != null && caseList.Count() > 0)
        {
            ddlSiteType.DataSource = caseList.ToList();
            ddlSiteType.DataValueField = "SITEID";
            ddlSiteType.DataTextField = "SITENAME";
            ddlSiteType.DataBind();
            ddlSiteType.Items.Insert(0, new ListItem(Resources.Lang.Commona_PleaseSelect, "")); //请选择
        }
    }
    private DataTable GetAGVSite(string sitetype)
    {
        string strSQL = string.Format(@"SELECT  SITEID ,
                                                SITENAME 
                                        FROM    dbo.BASE_CRANECONFIG_DETIAL
                                        WHERE   FLAG = '0'
                                                AND AGVSITETYPE = '{0}'", sitetype);
        return DBHelp.ExecuteToDataTable(strSQL);
    }
    #endregion
   
}