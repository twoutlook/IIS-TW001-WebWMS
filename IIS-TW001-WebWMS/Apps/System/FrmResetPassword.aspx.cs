using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using ClassBuilder;
using DreamTek.ASRS.Business.Base;
using System.Threading;
using System.Globalization;
using DreamTek.WMS.DAL;
using DreamTek.WMS.DAL.Model.Base;
using DreamTek.WMS.DAL.Common;
using DreamTek.WMS.Repository.Base;
using DreamTek.ASRS.Business.Tools;
public partial class FrmResetPassword : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtUserName.Text = Request.QueryString["userno"].ToString().Trim();           
            this.txtPASSWORDNew.Text = "";
            this.txtPASSWORDNew1.Text = "";      
             
        }    

    }
    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {

        if (String.IsNullOrEmpty(txtUserName.Text.Trim()) || String.IsNullOrEmpty(txtUserName.Text.Trim()))
        {
            this.txtUserName.Focus();
            this.Alert(Resources.Lang.Login_MSG15);//登录名不能为空！
            return false;
        }
        if (String.IsNullOrEmpty(txtPASSWORD.Text.Trim()) || String.IsNullOrEmpty(txtPASSWORD.Text.Trim()))
        {
            this.txtPASSWORD.Focus();
            this.Alert(Resources.Lang.FrmChangePassword_RequiredFieldValidator1);//登录名不能为空！
            return false;
        }
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

    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    string url = "../../Layout/Baselayout/Login.aspx";
    //    string str = "window.location.href = '" + url + "';";
    //    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "str", str, true);
    //  //  this.AlertAndBack("../../Layout/Baselayout/Login.aspx", "11");
    //   //Response.Write("<script>window.location='../../Layout/Baselayout/Login.aspx'</script>");
    //}
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
                string UserNo = txtUserName.Text.Trim();
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
                if (loginUser.message.Contains("成功"))
                {
                    this.AlertAndBack("../../Layout/Baselayout/Login.aspx", loginUser.message);
                }
                else
                {
                    this.Alert(loginUser.message);
                }
            }
            catch (Exception E)
            {
                this.Alert(Resources.Lang.frmBase_Config_Msg10 + E.Message); //修改失败！
            }
        }
    }    
}