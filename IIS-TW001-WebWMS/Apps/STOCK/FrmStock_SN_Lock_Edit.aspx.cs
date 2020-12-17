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

/// <summary>
/// 描述: SN冻结编辑页面
/// </summary>
public partial class FrmStock_SN_Lock_Edit : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData();
            }
        }
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
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('SN_Lock');return false;";//BUCKINGHA-894
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //ddpCstatus
        Help.DropDownListDataBind(GetParametersByFlagType("Common_EnableExp"), this.ddpCstatus, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        //ddpEnable
        Help.DropDownListDataBind(GetParametersByFlagType("Common_USE"), this.ddpEnable, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");

    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<STOCK_SN_LOCK> conn = new GenericRepository<STOCK_SN_LOCK>(db);
        STOCK_SN_LOCK entity = new STOCK_SN_LOCK();
        var caseList = from p in conn.Get()
                       where p.id == this.KeyID
                       select p;
        entity = caseList.ToList().FirstOrDefault();
        if (entity != null && !string.IsNullOrEmpty(entity.id))
        {
            txtID.Text = entity.id;
            txtCinvCode.Text = entity.cinvcode;
            txtCinvName.Text = entity.cinvname;
            txtCPositionCode.Text = entity.positioncode;
            txtCpositionName.Text = entity.positionname;
            txtSN.Text = entity.sncode;
            txtQty.Text = entity.snqty.ToString();
            txtDateCode.Text = entity.datecode.ToString();
            ddpCstatus.SelectedValue = entity.cstatus.ToString();
            ddpEnable.SelectedValue = entity.cenable.ToString();
            //冻结时间
            txtLockDate.Text = entity.locktime != null ? DateTime.Parse(entity.locktime.ToString()).ToString("yyyy-MM-dd") : "";

            HiField_LockDate.Value = txtLockDate.Text;

            //冻结结束时间
            txtUnLockdate.Text = entity.unlocktime != null ? DateTime.Parse(entity.unlocktime.ToString()).ToString("yyyy-MM-dd") : "";
            HiField_UnlockDate.Value = txtUnLockdate.Text;
            txtcreatetime.Text = entity.createtime != null ? DateTime.Parse(entity.createtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss") : "";
            txtcreateuser.Text = entity.createowner;
            txtupdatetime.Text = entity.lastupdatetime != null ? DateTime.Parse(entity.lastupdatetime.ToString()).ToString("yyyy-MM-dd HH:mm:ss") : "";
            txtupdateuser.Text = entity.lastupdateowner;

            txtLockDate.Enabled = false;
            if (entity.cstatus.ToString() == "1")
            {
                btnSave.Enabled = false;
                txtUnLockdate.Enabled = false;
                ddpEnable.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
                txtUnLockdate.Enabled = true;
                ddpEnable.Enabled = true;
            }
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {

        if (this.txtLockDate.Text.Trim().Length > 0)
        {
            //无效时修改冻结日期
            if (HiField_LockDate.Value != txtLockDate.Text)
            {
                if (Convert.ToDateTime(this.txtLockDate.Text) < DateTime.Now.Date)
                {
                    this.Alert(Resources.Lang.FrmStock_SN_Lock_Edit_MSG1);//修改冻结开始日期不能小于当前日期！
                    this.SetFocus(txtLockDate);
                    return false;
                }
            }
            if (txtUnLockdate.Text.Trim() != "")
            {
                if (Convert.ToDateTime(this.txtLockDate.Text) > Convert.ToDateTime(txtUnLockdate.Text))
                {
                    this.Alert(Resources.Lang.FrmStock_SN_Lock_Edit_MSG2);//修改冻结结束日期不能小于冻结开始日期！
                    this.SetFocus(txtUnLockdate);
                    return false;
                }
                if (Convert.ToDateTime(this.txtUnLockdate.Text) < DateTime.Now.Date)
                {
                    this.Alert(Resources.Lang.FrmStock_SN_Lock_Edit_MSG3);//修改冻结结束日期不能小于当前日期！
                    this.SetFocus(txtLockDate);
                    return false;
                }
            }

        }
        if (HiField_UnlockDate.Value.Trim().Length > 0)
        {
            if (txtUnLockdate.Text.Trim().Length == 0)
            {
                this.Alert(Resources.Lang.FrmStock_SN_Lock_Edit_MSG4);//不能修改冻结结束日期为空！
                this.SetFocus(txtUnLockdate);
                return false;
            }
        }
        return true;

    }


    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.CheckData())
        {
            string strKeyID = "";
            try
            {
                if (this.Operation() != SYSOperation.New)
                {
                    STOCK_SN_LOCK entity = new STOCK_SN_LOCK();
                    strKeyID = txtID.Text.Trim();
                    IGenericRepository<STOCK_SN_LOCK> conn = new GenericRepository<STOCK_SN_LOCK>(db);
                    var caseList = from p in conn.Get()
                                   where p.id == strKeyID
                                   select p;
                    entity = caseList.ToList().FirstOrDefault();
                    entity.id = strKeyID;
                    entity.cstatus = Convert.ToInt32(ddpCstatus.SelectedValue);
                    entity.cenable = Convert.ToInt32(ddpEnable.SelectedValue);
                    entity.locktime = Convert.ToDateTime(txtLockDate.Text.Trim());

                    if (txtUnLockdate.Text.Trim() != "")
                    {
                        entity.unlocktime = Convert.ToDateTime(txtUnLockdate.Text.Trim());
                    }
                    entity.lastupdatetime = DateTime.Now;
                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;

                    conn.Update(entity);
                    conn.Save();
                    this.AlertAndBack("FrmStock_SN_Lock_Edit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Msg_SaveSuccess);//保存成功！
                }

            }
            catch (Exception E)
            {
                this.Alert(Resources.Lang.WMS_Common_Msg_SaveFailed + E.Message);//保存失败！
            }
        }

    }

}

