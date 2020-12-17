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
using DreamTek.ASRS.Business.SP.ProcedureModel;
using DreamTek.ASRS.Business.Others;

public partial class Apps_Mixed_FrmMixed_DEdit : WMSBasePage
{

    //public string MixedID
    //{
    //    get
    //    {
    //        if (Request["MixedID"] == null)
    //        {
    //            MixedID = string.Empty;
    //        }
    //        return Request["MixedID"];
    //    }
    //    set
    //    {
    //        ViewState["MixedID"] = value;
    //    }
    //}

    //public string OutCticketCode
    //{
    //    get
    //    {
    //        if (ViewState["OutCticketCode"] == null)
    //        {
    //            ViewState["OutCticketCode"] = string.Empty;
    //        }
    //        return ViewState["OutCticketCode"].ToString();
    //    }
    //    set
    //    {
    //        ViewState["OutCticketCode"] = value;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        ucShowMixedCinvCode_Div.SetCinvCode = txtCinvCode.ClientID;
        ucShowMixedCinvCode_Div.SetCinvName = txtCinvName.ClientID;
        if (IsPostBack == false)
        {
            this.InitPage();
        }
    }

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('Mixed_DEdit');return false;";
        //this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        txtCinvCode.Attributes["onclick"] = "Show('" + ucShowMixedCinvCode_Div.GetDivName + "');";
        //本页面打开新增窗口
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "MixedStatus", false, -1, -1), this.ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
        if (this.Operation() == SYSOperation.Modify)
        {
            this.txtID.Text = Request["MixedID"].ToString();
            this.txtIDS.Text = this.KeyID;
            ShowData();

        }
        if (this.Operation() == SYSOperation.View)
        {
            this.txtID.Text = Request["MixedID"].ToString();
            this.txtIDS.Text = this.KeyID;
            ShowData();
            btnSave.Attributes["style"] = "color:#b5b5b5";
            btnSave.Enabled = false;
        }
        if (this.Operation() == SYSOperation.New)
        {
            txtCreaterUser.Text = OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
            this.txtID.Text = Request["MixedID"].ToString();
            this.txtIDS.Text = Guid.NewGuid().ToString();
            txtCreaterTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtPalledCode.Text = Request["PalletCode"].ToString();
        }
        ucShowMixedCinvCode_Div.CerpCode = Request["CerpCode"].ToString();
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<OUTMIXED_D> con = new GenericRepository<OUTMIXED_D>(context);
        IGenericRepository<TEMP_OUTMIXED_D> conn = new GenericRepository<TEMP_OUTMIXED_D>(context);
        int retVal = 0;
        string retMsg = string.Empty;
        try
        {
            if (this.CheckData())
            {
                OUTMIXED_D entity = this.SendData();
                string strKeyID = "";
                strKeyID = this.txtIDS.Text.Trim();
                if (this.Operation() == SYSOperation.Modify)
                {
                    con.Update(entity);
                    con.Save();
                }
                else if (this.Operation() == SYSOperation.New)
                {                  
                    //同时生成配料信息
                    Proc_SaveMixed proc = new Proc_SaveMixed();
                    proc.P_UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                    proc.P_ErpCode = Request["CerpCode"].ToString();
                    proc.P_Otype = GetOtype(Request["CerpCode"].ToString());
                    proc.P_PackageNo = this.txtPalledCode.Text.Trim();
                    proc.P_CinvCode = this.txtCinvCode.Text.Trim();
                    proc.P_Qty = Convert.ToDecimal(this.txtQty.Text.Trim());
                    proc.P_MixedCode =  GetMixedCode();                    
                    proc.Execute();
                    retVal=proc.ReturnValue;
                    retMsg = proc.ErrorMessage;
                    //TEMP_OUTMIXED_D entity_tmep = this.SendData_Temp();
                    //conn.Insert(entity_tmep);
                    //conn.Save();
                    if (retVal == 0) //只有当生成配料单成功后再进行保存OUTMIXED_D表信息
                    {
                        con.Insert(entity);
                        con.Save();
                    }
                }
                if(retVal != 0)
                {
                    this.Alert(Resources.Lang.Common_SuccessFail + retMsg); //保存失败！
                }
                else
                    this.AlertAndBack("FrmMixed_DEdit.aspx?MixedID=" + this.txtID.Text + "&CerpCode=" + Request["CerpCode"].ToString() + "&PalletCode=" + this.txtPalledCode.Text.Trim() + "&" + BuildQueryString(SYSOperation.New, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
            }
        }
        catch (Exception E)
        {
            this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATEEdit_Msg17 + E.Message); //"失败！"
        }
    }

    public bool CheckData()
    {
        //料号
        if (this.txtCinvCode.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmMixed_DEdit_Msg01);//料号项不允许空！
            this.SetFocus(txtCinvCode);
            return false;
        }
        //
        if (this.txtCinvCode.Text.Trim().Length > 0)
        {
            if (this.txtCinvCode.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmMixed_DEdit_Msg02); //料号项超过指定的长度50！
                this.SetFocus(txtCinvCode);
                return false;
            }
        }

        //品名
        if (string.IsNullOrEmpty(this.txtCinvName.Text.Trim()))
        {
            this.Alert(Resources.Lang.FrmMixed_DEdit_Msg03); //品名项不允许空！
            this.SetFocus(txtCinvName);
            return false;
        }
        //
        if (this.txtCinvName.Text.Trim().Length > 0)
        {
            if (this.txtCinvName.Text.GetLengthByByte() > 300)
            {
                this.Alert(Resources.Lang.FrmMixed_DEdit_Msg04); //品名项超过指定的长度300！
                this.SetFocus(txtCinvName);
                return false;
            }
        }

        //数量
        if (this.txtQty.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmMixed_DEdit_Msg05);//数量项不允许空！
            this.SetFocus(txtQty);
            return false;
        }

        //检查数量是否正确
        string errmsg = string.Empty;
        if (!(Comm_Function.Fun_IsDecimal(txtQty.Text.Trim(), 0, 0, 1, out errmsg)))
        {
            this.Alert(errmsg);
            this.SetFocus(txtQty);
            return false;
        }
        //decimal qty;
        //if (!decimal.TryParse(this.txtQty.Text.Trim(), out qty))
        //{
        //    this.Alert("数量项不是有效的数值！");
        //    this.SetFocus(txtQty);
        //    return false;
        //}
        string msg = JudgeQty(Request["CerpCode"].ToString(), Request["MixedID"].ToString(), this.txtCinvCode.Text.Trim(), this.txtQty.Text.Trim());
        //料号可配数量检查
        if (msg != "OK")
        {
            this.Alert(msg);
            this.SetFocus(txtQty);
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

    private string JudgeQty(string erpcode, string mixedid, string cinvcode, string qty)
    {
        string strSQL = string.Format(@"SELECT dbo.Fun_CheckMixedDList('{0}','{1}','{2}',{3})", erpcode, mixedid, cinvcode, Convert.ToDecimal(qty));
        return DBHelp.ExecuteScalar(strSQL);
    }

    private string GetAsnCode(string cticketcode)
    {
        string strSQL = string.Format(@"SELECT  cticketcode
                                        FROM    dbo.OUTASN
                                        WHERE   id = ( SELECT coutasnid
                                                       FROM   dbo.OUTBILL
                                                       WHERE  cticketcode = '{0}'
                                                     )", cticketcode);
        return DBHelp.ExecuteScalar(strSQL);
    }

    private string GetOtype(string erpcode)
    {
        string strSQL = string.Format(@"SELECT TOP 1
                                                itype
                                        FROM    dbo.OUTASN
                                        WHERE   cerpcode = '{0}'", erpcode);
        return DBHelp.ExecuteScalar(strSQL);
    }

    private string GetOutErpCode(string cticketcode)
    {
        string strSQL = string.Format(@"SELECT  cerpcode
                                        FROM    dbo.OUTASN
                                        WHERE   id = ( SELECT coutasnid
                                                       FROM   dbo.OUTBILL
                                                       WHERE  cticketcode = '{0}'
                                                     )", cticketcode);
        return DBHelp.ExecuteScalar(strSQL);
    }

    private string GetMixedCode()
    {
        string strSQL = string.Format(@"SELECT  MIXEDCODE
                                            FROM    dbo.OUTMIXED
                                            WHERE   ID = '{0}'", this.txtID.Text.Trim());
        return DBHelp.ExecuteScalar(strSQL);
    }
    //获取出库单单号
    public string GetOutCtickCode()
    {      
        string strOtype = GetOtype(Request["CerpCode"].ToString());
        string strErpCode = Request["CerpCode"].ToString();
        string strCinvCode = this.txtCinvCode.Text.Trim();
        string strSQL = string.Format(@" SELECT TOP 1
                            a.cticketcode                          
                    FROM    dbo.OUTBILL a
                            LEFT JOIN dbo.OUTBILL_D b ON a.id = b.id
                    WHERE   a.cerpcode = '{0}'
                            AND a.cstatus = '2'
                            AND a.otype = CONVERT(DECIMAL, '{1}')
                            AND b.cinvcode = '{2}';", strErpCode, strOtype, strCinvCode);
        return DBHelp.ExecuteScalar(strSQL);
    }

    private TEMP_OUTMIXED_D SendData_Temp()
    {
        TEMP_OUTMIXED_D entity = new TEMP_OUTMIXED_D();
        entity.id = Guid.NewGuid().ToString();
        entity.outasncticketcode = GetAsnCode(Request["OutCticketCode"].ToString());
        entity.outbillcticketcode = Request["OutCticketCode"].ToString();
        entity.outbilldids = hdnOutBilldIDS.Value;
        entity.CERPCODE = GetOutErpCode(Request["OutCticketCode"].ToString());
        entity.cstatus = "0";
        entity.cinvcode = this.txtCinvCode.Text.Trim();
        entity.cinvname = this.txtCinvName.Text.Trim();
        entity.iquantity = Convert.ToDecimal(this.txtQty.Text.Trim());
        entity.cinvbarcode = this.txtPalledCode.Text.Trim();
        entity.cmemo = Resources.Lang.FrmMixed_DEdit_Msg06;//出库单配料
        entity.dindate = DateTime.Now;
        entity.cinpersoncode = WmsWebUserInfo.GetCurrentUser().UserNo;
        entity.inbillcticketcode = GetMixedCode();
        entity.sntype = 0;
        return entity;
    }

    private OUTMIXED_D SendData()
    {
        OUTMIXED_D entity = new OUTMIXED_D();
        entity.ID = this.txtID.Text.Trim();
        entity.IDS = this.txtIDS.Text.Trim();
        if (this.Operation() == SYSOperation.New)
        {
            entity.CREATERUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.CREATERTIME = DateTime.Now;
        }
        if (this.Operation() == SYSOperation.Modify)
        {
            entity.CREATERUSER =OPERATOR.GetUserIDByUserName(this.txtCreaterUser.Text.Trim());
            entity.CREATERTIME = Convert.ToDateTime(this.txtCreaterTime.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtCinvCode.Text.Trim()))
        {
            entity.CINVCODE = txtCinvCode.Text.Trim();
        }
        if (!string.IsNullOrEmpty(this.txtCinvName.Text.Trim()))
        {
            entity.CINVNAME = this.txtCinvName.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txtQty.Text.Trim()))
        {
            entity.QTY = Convert.ToDecimal(txtQty.Text.Trim());
        }
        if (!string.IsNullOrEmpty(this.txtPalledCode.Text.Trim()))
        {
            entity.CINVBARCODE = this.txtPalledCode.Text.Trim();
        }
        if (!string.IsNullOrEmpty(ddlCSTATUS.SelectedValue))
        {
            entity.CSTATUS = ddlCSTATUS.SelectedValue;
        }
        if (!string.IsNullOrEmpty(txtRemark.Text.Trim()))
        {
            entity.REMARK = txtRemark.Text.Trim();
        }       
        //
      //  entity.OUTCTICKETCODE = Request["PalletCode"].ToString();
        entity.OUTCTICKETCODE = "";
        //entity.OUTCTICKETCODE = GetOutCtickCode();
        return entity;
    }

    public void ShowData()
    {
        IGenericRepository<OUTMIXED_D> con = new GenericRepository<OUTMIXED_D>(context);
        var caseList = from p in con.Get()
                       where p.IDS == this.txtIDS.Text.Trim()
                       select p;
        OUTMIXED_D entity = caseList.ToList().FirstOrDefault<OUTMIXED_D>();
        entity.ID = this.txtIDS.Text.Trim();
        this.txtCinvCode.Text = entity.CINVCODE;
        this.txtCinvName.Text = entity.CINVNAME;
        //this.hdnCinvName.Value = entity.CINVNAME;
        this.hdnPalletCode.Value = entity.CINVNAME;
        this.txtPalledCode.Text = entity.CINVBARCODE;
        this.txtQty.Text = entity.QTY.ToString();
        this.txtRemark.Text = entity.REMARK;
        this.txtCreaterTime.Text = Convert.ToDateTime(entity.CREATERTIME).ToString("yyyy-MM-dd HH:mm:ss");
        this.txtCreaterUser.Text =OPERATOR.GetUserNameByAccountID(entity.CREATERUSER);
        this.ddlCSTATUS.SelectedValue = entity.CSTATUS;
    }
}