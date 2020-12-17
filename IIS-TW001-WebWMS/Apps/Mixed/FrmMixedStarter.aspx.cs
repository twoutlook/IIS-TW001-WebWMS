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
using DreamTek.ASRS.Business.Mixed;

public partial class Apps_Mixed_FrmMixedStarter : WMSBasePage
{
 
    protected void Page_Load(object sender, EventArgs e)
    {
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('Mixed_Starter');return false;";
        //本页面打开新增窗口

        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        Help.DropDownListDataBind(GetParametersByFlagType("MixedStatus"), this.ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(GetOutType(true), this.txtITYPE, Resources.Lang.Common_ALL, "FUNCNAME", "EXTEND1", ""); //"全部"
        //加载AGV站点
        Help.DropDownListDataBind(GetAGVSite("1"), this.ddlStartSite, Resources.Lang.Common_ALL, "SITENAME", "SITEID", ""); //"全部"
        Help.DropDownListDataBind(GetAGVSite("2"), this.ddlEndSite, Resources.Lang.Common_ALL, "SITENAME", "SITEID", "");    //"全部"     

        if (this.Operation() == SYSOperation.Modify)
        {
            this.txtID.Text = this.KeyID;
            ShowData();
            this.txtERP_No.Enabled = false;          
        }
        if (this.Operation() == SYSOperation.View)
        {
            this.txtID.Text = this.KeyID;
            ShowData();
            btnSave.Attributes["style"] = "color:#b5b5b5";
            btnSave.Enabled = false;
        }
        //this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmMixed_DEdit.aspx?MixedID=" + this.txtID.Text + "&CerpCode=" + this.txtERP_No.Text.Trim() + "&PalletCode=" + this.hdnPalletCode.Value, SYSOperation.New, "") + "','新增配料明细','Mixed_DEdit');return false;";
    }  
    #endregion

    #region even
    /// <summary>
    /// 暂存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<OUTMIXED> con = new GenericRepository<OUTMIXED>(context);
        string errText = string.Empty;
        int retVal = 1;
        try
        {
            if (this.CheckData())
            {
                PROC_CreateDispatch proc = new PROC_CreateDispatch();
                proc.P_PackageNO = txtPalledCode.Text.Trim();
                proc.P_UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                proc.P_TYpe = "0";
                proc.P_SourceCode = this.txtMixedCode.Text.Trim();
                proc.P_BeginSiteCode = this.ddlStartSite.SelectedValue;
                proc.P_EndSiteCode = this.ddlEndSite.SelectedValue;
                proc.Execute();
                errText = proc.ErrorMessage;
                retVal = proc.ReturnValue;
                if (retVal == 0)
                {
                    //成功更新配料单起始目的站点
                    UpdateMixedData();
                    this.AlertAndBack("FrmMixedStarter.aspx?" + BuildQueryString(SYSOperation.View, this.txtID.Text.Trim()), Resources.Lang.FrmMixedStarter_Msg01); //叫车成功
                }
                else
                {
                    this.Alert(Resources.Lang.FrmMixedStarter_Msg02 + errText.Replace('\n', '!')); //叫车失败
                }
                
            }
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.FrmMixedStarter_Msg02 + E.Message); //叫车失败！
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    #endregion

    #region funtion

    private void UpdateMixedData()
    {
        string strSQL = string.Format(@"UPDATE  dbo.OUTMIXED
                                            SET     STARTSITE = '{0}' ,
                                                    ENDSITE = '{1}'
                                            WHERE   ID = '{2}'", ddlStartSite.SelectedValue, ddlEndSite.SelectedValue.Trim(), this.txtID.Text.Trim());
        DBHelp.ExecuteNonQuery(strSQL);
        //更新目的站点为占用状态
        strSQL = string.Format(@"UPDATE  dbo.BASE_CRANECONFIG_DETIAL
                                            SET     IS_OCCUPY = '1',
                                                    UPDATETIME = GETDATE(),
                                                    UPDATEUSER ={0}                                                            
                                            WHERE   SITEID = '{1}' AND AGVSITETYPE ='2' ", ddlEndSite.SelectedValue.Trim(),WmsWebUserInfo.GetCurrentUser().UserNo);
        DBHelp.ExecuteNonQuery(strSQL);
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
        IGenericRepository<OUTMIXED_D> con = new GenericRepository<OUTMIXED_D>(context);
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
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + ":<b>" + "</b>";//总页数
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
        this.txtPalledCode.Text = entity.CINVBARCODE;
        this.txtITYPE.SelectedValue = entity.OTYPE.ToString();
        this.ddlCSTATUS.SelectedValue = entity.CSTATUS;
        this.ddlStartSite.SelectedValue = entity.MIXEDSITE;//entity.STARTSITE;
        this.ddlStartSite.Enabled = false;
        this.ddlEndSite.SelectedValue = entity.ENDSITE;
        this.txtCreaterUser.Text = entity.CREATERUSER;
        this.txtCreaterTime.Text = Convert.ToDateTime(entity.CREATERTIME).ToString("yyyy-MM-dd HH:mm:ss");
        if (!string.IsNullOrEmpty(entity.MODIFYUSER))
        {
            this.txtModifUser.Text = entity.MODIFYUSER;
            this.txtModifTime.Text = Convert.ToDateTime(entity.MODIFYTIME).ToString("yyyy-MM-dd HH:mm:ss");
        }
        //if (!string.IsNullOrEmpty(entity.ENDSITE))
        //{
        //    this.ddlStartSite.Enabled = false;
        //    this.ddlEndSite.Enabled = false;
        //    btnSave.Enabled = false;
        //}
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
        if (!string.IsNullOrEmpty(txtITYPE.SelectedValue))
        {
            entity.OTYPE = Convert.ToDecimal(txtITYPE.SelectedValue);
        }
        if (!string.IsNullOrEmpty(ddlCSTATUS.SelectedValue))
        {
            entity.CSTATUS = ddlCSTATUS.SelectedValue;
        }
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
        return entity;
    }

    public bool CheckData()
    {
        //起始站点
        if (string.IsNullOrEmpty(ddlStartSite.SelectedValue))
        {
            this.Alert(Resources.Lang.FrmMixedStarter_Msg03); //起始站点项不允许空！
            this.SetFocus(ddlStartSite);
            return false;
        }
        //目的站点
        if (string.IsNullOrEmpty(ddlEndSite.SelectedValue))
        {
            this.Alert(Resources.Lang.FrmMixedStarter_Msg04); //目的站点项不允许空！
            this.SetFocus(ddlEndSite);
            return false;
        }
        //目的站点是否逻辑占用
        MixedQuery query = new MixedQuery();
        var isoccupy=query.IsExistsSiteWithMixed(txtMixedCode.Text.Trim(),ddlEndSite.SelectedValue.Trim());
        if (isoccupy == "1") //已逻辑占用
        {
            this.Alert(Resources.Lang.FrmMixedStarter_Msg05);//目的站点项已有配料单占用！
            this.SetFocus(ddlEndSite);
            return false;
        }

        //配料单是否有明细
        IGenericRepository<OUTMIXED_D> con = new GenericRepository<OUTMIXED_D>(context);
        var caseList = from p in con.Get()
                       orderby p.CREATERTIME descending
                       where p.ID == txtID.Text.Trim()
                       select p;
        if (caseList == null || caseList.ToList().Count == 0)
        {
            this.Alert(Resources.Lang.FrmMixedStarter_Msg06); //配料单没有配料明细，不允许叫车！
            return false;
        }
        
        return true;
    }

    private string JudgePC(string erpcode, string palletcode)
    {
        string strSQL = string.Format(@"SELECT dbo.Fun_CheckPCRepeat('{0}','{1}')", erpcode, palletcode);
        return DBHelp.ExecuteScalar(strSQL);
    }

    public string GetMixedCode()
    {
        string strSQL = string.Format("SELECT dbo.Fun_CreateNo('OUTMIXED')");
        return DBHelp.ExecuteScalar(strSQL);
    }
    #endregion
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
}