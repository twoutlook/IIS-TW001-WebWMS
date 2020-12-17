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


public partial class Login : WMSBasePage
{

   
  // System.Web.HttpContext.Current.User.Identity.Name

    /// <summary>
    /// 当前系统默认语言(在只配置一个语言或未设置时使用)
    /// </summary>
    private string currentLang
    {
        get { return this.hiddLang.Value; }
        set { this.hiddLang.Value = value; }
    }

    /// <summary>
    /// 注入企业信息
    /// </summary>
    private void RegisterCompanyInfo(DataTable dtCompany)
    {

    }

    protected void Page_Load(object sender, EventArgs e)    
    {
        if (!this.IsPostBack)
        {
            //if (!string.IsNullOrEmpty(Request.QueryString["Message"]))
            //    this.Alert(this.Request.QueryString["Message"]);
            WmsWebUserInfo.LoginPage = "Layout/Baselayout/Login.aspx";
            if( Request.Cookies["CSS_Name"] != null)
                this.cssUrl.Href = this.GetRelativePath() + "Layout/CSS/" + Request.Cookies["CSS_Name"].Value + "/login.css";
            
            if (Request.Cookies["Chk"] != null && Boolean.Parse(Request.Cookies["Chk"].Value))
            {
                if (Request.Cookies["UserName"] != null)
                {
                    txtUserName.Text = Request.Cookies["UserName"].Value;
                }                
                this.chkSave.Checked = true;
            }
            this.currentLang = string.Empty;
            Session.Clear();

            //加载语言选择
            LoadLanguage();
            this.imgLogo.ImageUrl = "../Css/LG/Images/Top/top_zlog.gif" + "?v=" + DateTime.Now.ToString("yyyyMMddHHmmss");
            this.multiUrl.Href = "../../Layout/multi/css/" + Resources.Lang.WMS_Common_MultiUrl + "/backImage.css?v="+ DateTime.Now.ToString("yyyyMMdd");
            
            GetFriendlyLinks();

            DataSet ds = GetBanns();            
            
            //this.GVNotice.DataSource = ds.Tables[0];
            //this.GVNotice.DataBind();
           // this.GridView1.DataSource = ds.Tables[1];
           // this.GridView1.DataBind();

            DataTable dtNoticeMore = GetBannsForMore();
           // this.GVNoticeMore.DataSource = dtNoticeMore;
           // this.GVNoticeMore.DataBind();

        }
        this.txtUserName.Focus();
        btnLogin.ServerClick += new ImageClickEventHandler(btnLogin_ServerClick);

        LoadImages();
    }

    private void LoadImages() {
        this.btnLogin.Src = "../multi/images/" + Resources.Lang.WMS_Common_MultiUrl + "/login.png";
    }

    private bool CheckData()
    {
        //if (IsSSO == false)
        {
            if (txtUserName.Text.Trim() == "")
            {
                this.WriteScript("window.flag=0;");
                this.Alert(Resources.Lang.Login_MSG15+"!");//用户名不能为空
                return false;
            }

            if (txtPassword.Text.Trim() == "")
            {
                this.WriteScript("window.flag=2;");
                this.Alert(Resources.Lang.Login_MSG16 + "!");//密码不能为空
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 创建数据库连接
    /// </summary>
    private bool SetDB()
    {
        DBHelp DbObject = new DBHelp();
        string Msg = DbObject.GetConn();
        if(Msg!="ok")
        {
            this.Alert(Resources.Lang.Login_MSG17 + "!\r\n" + Msg);//连接数据库失败
            return false;
        }
        return true;
    }
    protected void btnLogin_ServerClick(object sender, System.Web.UI.ImageClickEventArgs e)
    {   
        if (this.SetDB() == false)
        {
            return;
        }

        // NOTE by Mark, 2020-12-17, 這應該是之前註譯掉的部份

        //if ( //!this.txtUserName.Text.Trim().ToLower().Equals("admin") &&
        //     CheckLoginExpire("WEB"))
        //{
        //    return;
        //}


        if ( //!this.txtUserName.Text.Trim().ToLower().Equals("admin") &&
             CheckLoginExpire("WEB"))
        {
            return;
        }


        Session.Clear();
        string strName;
        string strPassWord;
        string appNo = ConfigurationManager.AppSettings["AppNo"];
        strName = this.txtUserName.Text.ToString();
        strPassWord = this.txtPassword.Text.ToString();

        string language = string.Empty;
        if (string.IsNullOrEmpty(this.currentLang))
        {
            language = this.ddlLanguage.SelectedValue;//"zh-cn";//默认值 //TODO:登陆时选择语言类别
        }
        else {
            language = this.currentLang;
        }

        //登陆时，依据用户选择的语言来设置
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);

        HttpCookie langCookie = new HttpCookie("language", language);
        Response.Cookies.Add(langCookie);
        WmsWebUserInfo CurrentUser = new WmsWebUserInfo();
        if (CheckData() == true)
        {
            
            try
            {

                RightLogin loginUser = null;
                string LoginUrl = ConfigurationManager.AppSettings["RightUrl"] + "RightsAPI/CheckUserLoginJson";
                string CompayNO = ConfigurationManager.AppSettings["CompayNO"];
                string ProjectNO = ConfigurationManager.AppSettings["ProjectNO"];

                //1.在权限平台验证登陆
                string url = string.Format(@"{0}?userno={1}&password={2}&companyno={3}&projectno={4}&language={5}", LoginUrl, strName, strPassWord, CompayNO, ProjectNO, language);
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
                    return;
                }
                if (!loginUser.state.ToLower().Equals("success"))
                {
                    this.Alert(loginUser.message);
                    return;
                }

                //记录登陆信息
                Base_UserLanguage bul = new Base_UserLanguage();
                bul.ID = Guid.NewGuid().ToString();
                bul.UserNo = strName;
                bul.UserName = strName;
                bul.Project = "Web";
                bul.LoginTime = DateTime.Now;
                bul.LanguageId = this.ddlLanguage.SelectedValue;//"zh-cn";//TODO:登陆时选择语言类别
                SqlRepository<Base_UserLanguage> db = new SqlRepository<Base_UserLanguage>();
                db.Insert(bul);

                string login_guid = loginUser.data.F_Id; //dtLogin.Rows[0]["GUID"].ToString();
                CurrentUser.UserName = loginUser.data.F_UserName;
                CurrentUser.UserNo = strName;
                CurrentUser.GUID=login_guid;
                CurrentUser.AppNo=appNo;
                CurrentUser.Password = strPassWord;
                CurrentUser.CompID = "";// dtUser.Rows[0]["COMPANY"].ToString();// dtUser.Rows[0]["COMPANY"].ToString();
                CurrentUser.CSS_Name = "";// dtUser.Rows[0]["EXTEND1"].ToString();// dtUser.Rows[0]["EXTEND1"].ToString();
                CurrentUser.CompType = EnumCompType.Manufacturing; //制造业
                CurrentUser.CSS_Name = "LG";
                CurrentUser.CSS_DIR = "LayOut/CSS/LG";
                CurrentUser.Language = this.ddlLanguage.SelectedValue;// "zh-cn";//TODO:登陆时选择语言类别
                System.Web.HttpCookie cookie = new HttpCookie("CSS_Name", CurrentUser.CSS_Name);
                cookie.Expires = new DateTime(2049, 12, 31, 0, 0, 0, 0);
                Response.Cookies.Add(cookie); 
                Session.Timeout = 60;

                Session[WmsWebUserInfo.SESSION_KEY] = CurrentUser;
                #region ------ Add By CK 2009-08-03 ------

                #endregion

                #region 记住用户名
                if (chkSave.Checked)
                {
                    cookie = new HttpCookie("UserName", txtUserName.Text);
                    cookie.Expires = new DateTime(2049, 12, 31, 0, 0, 0, 0);
                    Response.Cookies.Add(cookie);

                    cookie = new HttpCookie("Chk", chkSave.Checked.ToString());
                    cookie.Expires = new DateTime(2049, 12, 31, 0, 0, 0, 0);
                    Response.Cookies.Add(cookie);
                }
                #endregion
                Session["CurrentUser"] = CurrentUser;

                #region 转到主界面
                string topPage = "FrmTopCompany.aspx";
                if (CurrentUser.CompType.Equals(EnumCompType.Custom))
                {
                    topPage = "FrmTopCustoms.aspx";
                }
                string menuPage = "";
                string topSize = "50";
                string contentPage = "../../Apps/FrmFirstPage.aspx";
                if (Request["F"] != null && Request["F"].ToString().Equals("ExpireDate"))
                {
                    string returnUrl = "MainFrameNew.aspx?RootMenuID=&TopPage=" + HttpUtility.UrlEncode(topPage) + "&MenuPage=" + HttpUtility.UrlEncode(menuPage) + "&TopSize=" + topSize;
                    Response.Redirect(returnUrl, false);
                    Session["ContentPage"] = "../../apps/base/expiredate.aspx";
                    Session["returnUrl"] = returnUrl;
                }
                else
                {
                    Response.Redirect("MainFrameNew.aspx?RootMenuID=&TopPage=" + HttpUtility.UrlEncode(topPage) + "&MenuPage=" + HttpUtility.UrlEncode(menuPage) + "&TopSize=" + topSize, false);
                    Session["ContentPage"] = contentPage;
                }
                
                #endregion
            
            } 
            catch (Exception E)
            {
                txtPassword.Text = "";
                this.Alert(E.ToString());
                
            }
        }
    }   

    /// <summary>
    /// 检查登录是否过期
    /// </summary>
    /// <returns></returns>
    public bool CheckLoginExpire(string modelName)
    {
        bool bl = false;
        bl = BaseCommQuery.CheckLoginExpire(modelName);
        return bl;
    }

    
    protected void GetFriendlyLinks()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("Friendly-Link.xml"));
        XmlNodeList xmlNodeList = xmlDoc.DocumentElement.SelectNodes("//link");
        //宋体;快速連接
        string strSel = "<span style=\"font-family: " + Resources.Lang.Login_MSG21 + "; font-size: 11pt;color:blue;font-weight:bold;\">" + Resources.Lang.Login_MSG22 + "</span>&nbsp;<select style=\"width:150px;\" onchange=\"Navigater(this.options[this.selectedIndex].value);\">";
        for (int i = 0; i < xmlNodeList.Count; i++)
        {
            if (i == 0)
                strSel += "<option selected value=''>" + xmlNodeList[i].Attributes["title"].Value + "</option>";
            else
                strSel += "<option value='" + xmlNodeList[i].FirstChild.InnerText.Replace("\r", "").Replace("\n", "").Trim() + "'>" + xmlNodeList[i].Attributes["title"].Value + "</option>";
        }
        strSel += "</select>";
        selectr.InnerHtml = strSel;
    }

    public void SetShowNoticeDetail(string strContent, string strTitle)
    {
        ShowNotcieDetail1.SetNoteciDetail(strContent, strTitle);
    }

    protected void GVNotice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes["onmouseover"] = "JavaScript:this.style.cursor='hand';";
            e.Row.Cells[1].Attributes["onmouseout"] = "JavaScript:this.style.cursor='default';";
            e.Row.Cells[1].Attributes.Add("onclick", "__doPostBack('GVNotice','Select$" + e.Row.RowIndex + "');showFloat('DivNotcieDetail');");
        }
    }
    protected void GVNotice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GVNotice.SelectedIndex > -1 && GVNotice.Rows.Count > 0)
        {
            GridViewRow gvr = GVNotice.SelectedRow;
            Label lbContent = (Label)gvr.FindControl("lbContent");
            Label lbTitle = (Label)gvr.FindControl("lbTitle");
            string strContent = lbContent.Text.Trim();
            string strTitle = lbTitle.Text.Trim();

            ShowNotcieDetail1.SetNoteciDetail(strContent, strTitle);
        }
    }
    protected void GVNotice1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes["onmouseover"] = "JavaScript:this.style.cursor='hand';";
            e.Row.Cells[1].Attributes["onmouseout"] = "JavaScript:this.style.cursor='default';";
            e.Row.Cells[1].Attributes.Add("onclick", "__doPostBack('GridView1','Select$" + e.Row.RowIndex + "');showFloat('DivNotcieDetail');");
        }
    }
    protected void GVNotice1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.SelectedIndex > -1 && GridView1.Rows.Count > 0)
        {
            GridViewRow gvr = GridView1.SelectedRow;
            Label lbContent = (Label)gvr.FindControl("lbContent");
            Label lbTitle = (Label)gvr.FindControl("lbTitle");
            string strContent = lbContent.Text.Trim();
            string strTitle = lbTitle.Text.Trim();

            ShowNotcieDetail1.SetNoteciDetail(strContent, strTitle);
        }
    }
    protected DataSet GetBanns()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("Banns.xml"));
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtEx = new DataTable();
        XmlNodeList xmlNodeList = xmlDoc.SelectNodes("//banns");
        dt.Columns.Add(new DataColumn("title", typeof(string)));
        dt.Columns.Add(new DataColumn("comment", typeof(string)));
        dt.Columns.Add(new DataColumn("date", typeof(string)));
        dtEx.Columns.Add(new DataColumn("title", typeof(string)));
        dtEx.Columns.Add(new DataColumn("comment", typeof(string)));
        dtEx.Columns.Add(new DataColumn("date", typeof(string)));

        int i = 0;
        int iCount = xmlNodeList.Count;
        foreach (XmlNode node in xmlNodeList)
        {
            if (iCount >= 4)
            {
                if (i >= 2)
                {
                    DataRow dr = dtEx.NewRow();
                    dr["title"] = node.Attributes["title"].Value;
                    dr["comment"] = node.FirstChild.InnerText;
                    dr["date"] = node.Attributes["date"].Value;
                    dtEx.Rows.Add(dr);

                    if (i == 4)
                        break;

                } else
                {
                    DataRow dr = dt.NewRow();
                    dr["title"] = node.Attributes["title"].Value;
                    dr["comment"] = node.FirstChild.InnerText;
                    dr["date"] = node.Attributes["date"].Value;
                    dt.Rows.Add(dr);
                }
            } else
            {
                int iIndex = 0;
                if (iCount <= 2)
                    iIndex = 0;
                if (iCount == 3)
                    iIndex = 1;

                if (i > iIndex)
                {
                    DataRow dr = dtEx.NewRow();
                    dr["title"] = node.Attributes["title"].Value;
                    dr["comment"] = node.FirstChild.InnerText;
                    dr["date"] = node.Attributes["date"].Value;
                    dtEx.Rows.Add(dr);

                } else
                {
                    DataRow dr = dt.NewRow();
                    dr["title"] = node.Attributes["title"].Value;
                    dr["comment"] = node.FirstChild.InnerText;
                    dr["date"] = node.Attributes["date"].Value;
                    dt.Rows.Add(dr);
                }
            }
            i++;
        }

        ds.Tables.Add(dt);
        ds.Tables.Add(dtEx);

        return ds;
    }

    protected DataTable GetBannsForMore()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("Banns.xml"));
        DataTable dt = new DataTable();
        XmlNodeList xmlNodeList = xmlDoc.SelectNodes("//banns");
        dt.Columns.Add(new DataColumn("title", typeof(string)));
        dt.Columns.Add(new DataColumn("comment", typeof(string)));
        dt.Columns.Add(new DataColumn("date", typeof(string)));

        foreach (XmlNode node in xmlNodeList)
        {
            DataRow dr = dt.NewRow();
            dr["title"] = node.Attributes["title"].Value;
            dr["comment"] = node.FirstChild.InnerText;
            dr["date"] = node.Attributes["date"].Value;
            dt.Rows.Add(dr);
        }

        return dt;
    }

    protected void GVNoticeMore_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVNoticeMore.PageIndex = e.NewPageIndex;
        DataTable dtNotice = GetBannsForMore();
        GVNoticeMore.DataSource = dtNotice;
        GVNoticeMore.DataBind();
    }

    protected void GVNoticeMore_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes["onmouseover"] = "JavaScript:this.style.cursor='hand';";
            e.Row.Cells[1].Attributes["onmouseout"] = "JavaScript:this.style.cursor='default';";
            e.Row.Cells[1].Attributes.Add("onclick", "__doPostBack('GVNoticeMore','Select$" + e.Row.RowIndex + "');ShowMyDetail();");
        }
    }

    protected void GVNoticeMore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GVNoticeMore.SelectedIndex > -1 && GVNoticeMore.Rows.Count > 0)
        {
            GridViewRow gvr = GVNoticeMore.SelectedRow;
            Label lbContent = (Label)gvr.FindControl("lbContent");
            Label lbTitle = (Label)gvr.FindControl("lbTitle");
            string strContent = lbContent.Text.Trim();
            string strTitle = lbTitle.Text.Trim();

            ShowNotcieDetail1.SetNoteciDetail(strContent, strTitle);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //登陆时，依据用户选择的语言来设置
        string language = this.ddlLanguage.SelectedValue;//"zh-cn";//默认值 //TODO:登陆时选择语言类别
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
        HttpCookie langCookie = new HttpCookie("language", language);
        Response.Cookies.Add(langCookie);
        Response.Redirect("Login.aspx");
    }

    private void LoadLanguage() {
        List<Sys_Language> langList = new SYS_RelatedRepository().GetSysLanguages();
        if (langList != null && langList.Any())
        {
            //系统只设置了默认的一种语言，那默认就是该语言，不显示语言切换
            if (langList.Count == 1)
            {
                trLanguage.Visible = false;
                this.currentLang = langList.First().code;
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.currentLang);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(this.currentLang);
            }
            else {
                trLanguage.Visible = true;
                ddlLanguage.Items.Clear();
                foreach (var item in langList) {
                    ddlLanguage.Items.Add(new ListItem(item.text, item.code));
                }

                HttpCookie cokie = new HttpCookie("language");//初使化
                if (Request.Cookies["language"] != null)
                {
                    string lan = Request.Cookies["language"].Value;
                    ddlLanguage.SelectedValue = lan;
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lan);
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lan);
                }
                    //设置默认语言为繁体
                else
                {
                    string lan ="zh-TW";
                    HttpCookie langCookie = new HttpCookie("language", lan);
                    Response.Cookies.Add(langCookie);
                    ddlLanguage.SelectedValue = lan;
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lan);
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lan);
                }
            }
        }
        else { 
            //系统没有设置语言，默认为中文简体
            trLanguage.Visible = false;
            this.currentLang = "zh-cn";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.currentLang);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(this.currentLang);
        }
    }
    public string GetRelativePath()
    {
        return "../../";
    }
}
