using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Text;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business.Base;

public partial class BASE_FrmBase_LOGOCHANGE : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitPage();
    }

    #region IPageGrid 成员

    public void GridBind()
    {

    }

    public bool CheckData()
    {

        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        //this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTASN_D');return false;";
        this.fuFile.Attributes.Add("onchange", "showPic()");
        this.fuFileZ.Attributes.Add("onchange", "showPicZ()");
        if (!IsPostBack)
        {
            //txtTitle.Text = Comm_Function.GetConFig("200002");
            IGenericRepository<SYS_CONFIG> sysconfig = new GenericRepository<SYS_CONFIG>(context);
            var caseList = from p in sysconfig.Get()
                           where p.code == "200002"
                           select p;
            SYS_CONFIG entity = caseList.ToList().FirstOrDefault<SYS_CONFIG>();
            txtTitle.Text = entity.config_value;

            this.Image1.ImageUrl = "~/Layout/Css/LG/Images/Top/top_log.gif"+ "?v="+ DateTime.Now.ToString("yyyyMMddHHmmss");
            this.Image2.ImageUrl = "~/Layout/Css/LG/Images/Top/top_zlog.gif" + "?v=" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }

    #endregion

    #region 上传图片
    protected void btnUp_Click(object sender, EventArgs e)
    {
        //IGenericRepository<V_SYS_CONFIG> con = new GenericRepository<V_SYS_CONFIG>(context);
        IGenericRepository<SYS_CONFIG> sysconfig = new GenericRepository<SYS_CONFIG>(context);

       
        if (fuFile.PostedFile.FileName == "")
        {
            base.Alert(Resources.Lang.FrmBase_LOGOCHANGE_Msg10); //请选择要上载的文件
            return;
        }
        var extension = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
        if (extension != null && (extension.ToLower() != ".gif") && (extension.ToLower() != ".png") && (extension.ToLower() != ".jpg"))
        {
            base.Alert(Resources.Lang.FrmBase_LOGOCHANGE_Msg11); //上载的文件类型不正确，必须为*.gif,*.png,*.jpg
            return;
        }
        this.lblMsg.Text = Resources.Lang.FrmBase_LOGOCHANGE_Msg12; //开始上传文件...
        DateTime dt = DateTime.Now;
        var s = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
        if (s != null)
        {
            string fileName = "top_log.gif";
            string requestUrl = Request.Path.ToLower();
            int index = requestUrl.IndexOf("apps/", 0);
            string savePath = Server.MapPath(requestUrl.Substring(0, index)) + "Layout\\Css\\LG\\Images\\Top\\" + fileName;
            fuFile.PostedFile.SaveAs(@savePath);

            //oralce
            //string sql = string.Format(" update sys_config set config_value='{0}' where code='200002' ", txtTitle.Text);
            //DBUtil.ExecuteNonQuery(sql);

            //sql
            var caseList = from p in sysconfig.Get()
                           where p.code == "200002"
                           select p;
            SYS_CONFIG entity = caseList.ToList().FirstOrDefault<SYS_CONFIG>();
            entity.config_value = txtTitle.Text;
            sysconfig.Update(entity);
            entity.lastupdatetime = System.DateTime.Now;
            sysconfig.Save();
            this.lblMsg.Text = "文件上传并保存成功！<br/>"; //文件上传并保存成功！
            this.Image1.ImageUrl = "~/Layout/Css/LG/Images/Top/top_log.gif" + "?v=" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }

    protected void btnUpZ_Click(object sender, EventArgs e)
    {
        IGenericRepository<SYS_CONFIG> sysconfig = new GenericRepository<SYS_CONFIG>(context);
        if (fuFileZ.PostedFile.FileName == "")
        {
            base.Alert(Resources.Lang.FrmBase_LOGOCHANGE_Msg10); //请选择要上载的文件
            return;
        }
        var extension = System.IO.Path.GetExtension(fuFileZ.PostedFile.FileName);
        if (extension != null && (extension.ToLower() != ".gif") && (extension.ToLower() != ".png") && (extension.ToLower() != ".jpg"))
        {
            base.Alert(Resources.Lang.FrmBase_LOGOCHANGE_Msg11); //上载的文件类型不正确，必须为*.gif,*.png,*.jpg"
            return;
        }
        this.lblMsg.Text = Resources.Lang.FrmBase_LOGOCHANGE_Msg12; //开始上传文件...
        DateTime dt = DateTime.Now;
        var s = System.IO.Path.GetExtension(fuFileZ.PostedFile.FileName);
        if (s != null)
        {
            string fileName = "top_zlog.gif";
            string requestUrl = Request.Path.ToLower();
            int index = requestUrl.IndexOf("apps/", 0);
            string savePath = Server.MapPath(requestUrl.Substring(0, index)) + "Layout\\Css\\LG\\Images\\Top\\" + fileName;
            fuFileZ.PostedFile.SaveAs(@savePath);

            //string sql = string.Format(" update sys_config set config_value='{0}' where code='200002' ", txtTitle.Text);
            //DBUtil.ExecuteNonQuery(sql);

            //sql
            var caseList = from p in sysconfig.Get()
                           where p.code == "200002"
                           select p;
            SYS_CONFIG entity = caseList.ToList().FirstOrDefault<SYS_CONFIG>();
            entity.config_value = txtTitle.Text;
            entity.lastupdatetime = System.DateTime.Now;
            sysconfig.Update(entity);
            sysconfig.Save();

            this.lblMsg.Text = Resources.Lang.FrmBase_LOGOCHANGE_Msg13 + "<br/>"; //文件上传并保存成功！
            this.Image2.ImageUrl = "~/Layout/Css/LG/Images/Top/top_zlog.gif" + "?v=" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
    #endregion
    
}