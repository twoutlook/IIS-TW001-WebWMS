using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Collections;
using System.Web.Security;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;

public partial class FrmError : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.Title = "错误处理页面";
            //lblTitle.Text = "错误处理页面";
            //string css_name = this.GetCSS_Name();
            //if (this.Request != null && this.Request.FilePath.IsNullOrEmpty() == false)
            //{
            //    //this.cssUrl.Attributes["Href"] = WebPageExtension.GetRelativeURL(this, this.Request.FilePath, "Layout/CSS/" + css_name + "/Page.css");
            //}
            //else
            //{
            //    //this.cssUrl.Attributes["Href"] = this.GetRelativeURL("Layout/CSS/" + css_name + "/Page.css");
            //}
            this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/Page.css";

            Exception E = Server.GetLastError();
            if (E != null)
            {

                if (E is HttpUnhandledException && E.InnerException != null)
                {
                    E = E.InnerException;
                    this.lblError.Text = E.Message;

                    if (E.Message.IndexOf("文件不存在") >= 0)
                    {
                        //this.Request.FilePath
                        int iii = 0;
                        iii++;
                    }
                    if (E.StackTrace != null)
                    {
                        this.lblDetailError.Text = E.StackTrace.Replace("\r\n", "<br/>");
                    }
                    else
                    {
                        this.lblDetailError.Text = "";
                    }
                    //清除异常
                    Server.ClearError();

                }
                else
                {

                    //string exValue = ConfigurationManager.AppSettings["exValue"].ToString();
                    //if (exValue.Equals("1"))
                    //{

                    //}

                    if (Server.GetLastError() != null)
                    {
                        Exception ex = Server.GetLastError().GetBaseException();
                        if (ex != null)
                        {
                            // 错误的信息
                            this.lblError.Text = ex.Message;

                            // 错误的堆栈
                            if (ex.StackTrace != null)
                            {
                                this.lblDetailError.Text = ex.StackTrace.Replace("\r\n", "<br/>");

                            }
                            //// 出错的方法名
                            //this.Label4.Text = ex.TargetSite.Name;
                            //// 出错的类名
                            //this.Label5.Text = ex.TargetSite.DeclaringType.FullName; 

                            //清除异常
                            Server.ClearError();
                        }
                    }

                }

            }
            else
            {

                //this.Page.WriteScript("");
                this.lblError.Text = Resources.Lang.FrmError_MSG4;//"未捕捉到错误";
                this.lblDetailError.Text = "";


            }
            this.hplLogin.Text = Resources.Lang.FrmError_MSG5; //"重新登录";
            this.hplLogin.Target = "_top";
            this.hplLogin.NavigateUrl = "../../Layout/BaseLayout/Login.aspx";
            this.hplReturn.Text = Resources.Lang.Common_btnBack; //"返回";
            this.hplReturn.NavigateUrl = "#";
            this.hplReturn.Attributes["OnClick"] = "javascript:history.go(-1);return false;";
        }
    }


}

