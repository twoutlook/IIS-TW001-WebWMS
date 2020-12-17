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
using DreamTek.WMS.DAL.Model.Base;
using DreamTek.ASRS.Business.Tools;


public partial class Sys_FrmChangePassword : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {        
            this.InitPage();
        }
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
    }
    #endregion
   

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        
        if (String.IsNullOrEmpty(txtPASSWORDNew.Text.Trim()) || String.IsNullOrEmpty(txtPASSWORDNew1.Text.Trim()))
        {
            this.txtPASSWORDNew.Focus();
            this.Alert(Resources.Lang.FrmChangePassword_Msg01);//新密码不能为空！
            return false;
        }
        if (this.txtPASSWORDNew.Text != this.txtPASSWORDNew1.Text)
        {
            this.txtPASSWORDNew.Focus();
            this.txtPASSWORDNew1.Focus();
            this.Alert(Resources.Lang.FrmChangePassword_Msg02); //两次输入的新密码不一致！
            return false;
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
            try
            {
                string changeUrl = ConfigurationManager.AppSettings["RightUrl"] + "RightsAPI/UdpateUserPasswordJson";
                string CompayNO = ConfigurationManager.AppSettings["CompayNO"];
                string ProjectNO = ConfigurationManager.AppSettings["ProjectNO"];
                string Language = string.Empty;
                string UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                RightLogin loginUser = null;

                if (Request.Cookies["language"] != null)
                {
                    Language = Request.Cookies["language"].Value;
                }
                string url = string.Format(@"{0}?userno={1}&password={2}&newpassword={3}&companyno={4}&projectno={5}&language={6}", changeUrl, UserNo, txtPASSWORD.Text, txtPASSWORDNew.Text, CompayNO, ProjectNO, Language);
                string jsonStr = WebPageExtension.ExecuteGetUrl(url);
                try
                {
                    loginUser = WebPageExtension.DeserializeObject<RightLogin>(jsonStr);
                }
                catch (Exception ex)
                {
                    loginUser = null;
                }
                if (loginUser == null)
                {
                    this.Alert(Resources.Lang.frmBase_Config_Msg10); //修改失败！
                }
                this.AlertAndBack("FrmChangePassword.aspx", loginUser.message);
            }
            catch (Exception E)
            {
                this.Alert(Resources.Lang.frmBase_Config_Msg10 + E.Message); //修改失败！
            } 
        }
    }    
}

