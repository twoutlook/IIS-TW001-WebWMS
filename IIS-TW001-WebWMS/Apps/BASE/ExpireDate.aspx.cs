using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using DreamTek.ASRS.Business.Base;
using DreamTek.ASRS.Business.Others;


public partial class Apps_BASE_ExpireDate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["CurrentUser"] == null)
            {
                this.Response.Redirect("../../Layout/BaseLayout/Login.aspx?F=ExpireDate");
            }
            var userInfo = WmsWebUserInfo.GetCurrentUser();
            if (userInfo.UserNo.ToLower().Equals("admin"))
            {
                ShowData();
            }
            else
            {
                this.Response.Redirect("../../Layout/BaseLayout/Login.aspx?F=ExpireDate");
            }
        }
    }

    private void ShowData()
    {
        string dateValue = CommFunction.GetConFig("999999");
        string info = string.Empty;// dt.Rows[0]["MEMO"].ToString().Trim();
        DateTime expDate;
        if (DateTime.TryParse(dateValue, out expDate))//日期直接存在系統中，沒有加密
        {

        }
        else
        {
            dateValue = Comm_Function.DESDecrypt(dateValue);//解密成日期字符串
        }

        this.txtDate.Text = dateValue;
        if (info.Length > 0)
        {
            this.txtInfo.Text = Comm_Function.DESDecrypt(info);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string dateValue = CommFunction.DESEncrypt(txtDate.Text);
            string info = CommFunction.DESEncrypt(txtInfo.Text);
            string sql = string.Format(" update sys_config set config_value='{0}', MEMO = '{1}',lastupdatetime=getdate()  where code='999999' ", dateValue, info);
            DBHelp.ExecuteNonQuery(sql);
            //DBUtil.ExecuteNonQuery(sql);
            this.Alert(Resources.Lang.Common_SuccessSave);//保存成功！
        }
        catch
        {
            this.Alert("保存失敗！");
        }

        ShowData();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Session["returnUrl"] != null)
        {
            Session["ContentPage"] = "../../Apps/FrmFirstPage.aspx";
            Response.Redirect("../../Apps/FrmFirstPage.aspx", false);
        }
    }

    /// <summary>
    /// 显示提示信息
    /// </summary>
    /// <param name="p_Message">提示信息字符串</param>
    public void Alert(string p_Message)
    {
        if (p_Message == null)
        {
            return;
        }
        if (p_Message.Contains("\r\n")) p_Message = p_Message.Replace("\r\n", "\\r\\n");
        if (p_Message.Contains("'")) p_Message = p_Message.Replace("'", "\'");
        if (p_Message.Contains(";")) p_Message = p_Message.Replace(";", "\\;");
        if (p_Message.Contains("\"")) p_Message = p_Message.Replace("\"", "");
        string p_Script = "alert('" + p_Message.ToJsString() + "')";
        this.Page.Page.Response.Write("<script type='text/javascript'>alert('" + p_Message + "');</script>");
    }
}