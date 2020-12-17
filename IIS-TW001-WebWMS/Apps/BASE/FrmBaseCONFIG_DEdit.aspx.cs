using System;
using System.Collections.Generic;
using System.Linq;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Data;
public partial class FrmBaseCONFIG_DEdit : WMSBasePage//PageBase, IPageEdit
{
    public string id { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() == SYSOperation.Modify)
            {
                ShowData();
            }
            //if (string.IsNullOrEmpty(Request.QueryString["ConfigID"]))
            //{ 
            //    ShowData(); 
            //}
            //else
            //{
            //    id = Request.QueryString["ConfigID"]; ;
            //    Update(id);
            //}
        }
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
    }
    public void Update(string id)
    {
        IGenericRepository<SYS_CONFIG> sysconfig = new GenericRepository<SYS_CONFIG>(context);
        var caseList = from p in sysconfig.Get()
                       where p.id == id
                       select p;
        SYS_CONFIG entity = caseList.ToList().FirstOrDefault<SYS_CONFIG>();
        txtType.Enabled = false;
        txtCode.Enabled = false;
        txtCode.Text = entity.code;
        txtType.SelectedValue = entity.type;
        txtValue.Text = entity.config_value;
        txtCONFIG_DESC.Text = entity.config_desc;
        txtCMEMO.Text = entity.memo;
        #region 改前
        //string sql = "SELECT ID, TYPE, CODE, CONFIG_DESC, CONFIG_VALUE, MEMO FROM  sys_config where id in '" + id + "'";
        //DataTable dt = DBUtil.Fill(sql);
        //txtType.Enabled = false;
        //txtCode.Enabled = false;
        //txtCode.Text = dt.Rows[0]["CODE"].ToString();
        //txtValue.Text = dt.Rows[0]["CONFIG_VALUE"].ToString();
        //txtCONFIG_DESC.Text = dt.Rows[0]["CONFIG_DESC"].ToString();
        //txtCMEMO.Text = dt.Rows[0]["MEMO"].ToString();
        #endregion
    }
    /// <summary>
    /// 初始化页面。主要做一下动作
    ///1、DropDownList,ListBox数据装载,
    ///2、新增按钮、删除的按钮的Java脚本注册等
    ///一般在PageLoad 事件调用，
    ///并且IsPostBack = false时
    /// </summary>
    public void InitPage()
    {
        //this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTASN_D');return false;";
        //SysParameterListQuery SysParameterListQuery = new SysParameterListQuery();
        Help.DropDownListDataBind(GetParametersByFlagType("SYSConfigType"), txtType, "", "FLAG_NAME", "FLAG_ID", "");//作用域
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<SYS_CONFIG> con = new GenericRepository<SYS_CONFIG>(context);
        var caseList = from p in con.Get()
                       orderby p.createtime descending
                       where p.id == this.KeyID
                       select p;
        SYS_CONFIG entity = caseList.ToList().FirstOrDefault<SYS_CONFIG>();
        txtType.Enabled = false;
        txtCode.Enabled = false;
        txtCode.Text = entity.code;
        txtType.SelectedValue = entity.type;
        txtValue.Text = entity.config_value;
        txtCONFIG_DESC.Text = entity.config_desc;
        txtCMEMO.Text = entity.memo;
    }
    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {

        if (txtCONFIG_DESC.Text.Trim().Length > 100)
        {
            this.Alert(Resources.Lang.FrmBaseCONFIG_DEdit_Msg02);//配置描述长度不得超过100！
            this.SetFocus(txtCONFIG_DESC);
            return true;
        }

        if (txtCMEMO.Text.Trim().Length > 100)
        {
            this.Alert(Resources.Lang.FrmBaseCONFIG_DEdit_Msg03);//备注长度不得超过100！
            this.SetFocus(txtCMEMO);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<SYS_CONFIG> sysconfig = new GenericRepository<SYS_CONFIG>(context);
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add(Resources.Lang.FrmBaseCONFIG_DEdit_Msg04, txtValue.Text); //配置参数不可为空请填写…………
        dictionary.Add(Resources.Lang.FrmBaseCONFIG_DEdit_Msg05, txtCONFIG_DESC.Text);//配置描述不可为空请填写…………
        dictionary.Add(Resources.Lang.FrmBaseCONFIG_DEdit_Msg06, txtCode.Text);//配置代码不可为空请填写…………

        if (!Tool.IsNullCheck(dictionary) || CheckData()) { btnSave.Visible = true; }
        else
        {
            if(this.Operation()==SYSOperation.New)
            {
                //新增前验证代码是否存在
                var caseList = from p in sysconfig.Get()
                               where p.code == txtCode.Text.Trim()
                               select p;
                if (caseList.ToList().Count > 0)
                {
                    Alert("【" + txtCode.Text + "】" + Resources.Lang.FrmBaseCONFIG_DEdit_Msg07); //配置代码已在系统存在。不可重复，请修正
                    return;
                }
                else
                {
                    var entity = SendData();
                    entity.id = Guid.NewGuid().ToString();
                    entity.createtime = DateTime.Now;
                    entity.status = 1;
                    entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    sysconfig.Insert(entity);
                    sysconfig.Save();
                    this.Alert(Resources.Lang.FrmBaseCONFIG_DEdit_Msg08);//新增成功！
                }
            }
            else
            {
                var entity = SendData();
                entity.id = this.KeyID;//Request.QueryString["ConfigID"];
                entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                entity.lastupdatetime = DateTime.Now;
                sysconfig.Update(entity);
                sysconfig.Save();
                this.Alert(Resources.Lang.FrmBaseCONFIG_DEdit_Msg09); //"修改成功！"
            }
        }
    }

    public SYS_CONFIG SendData()
    {
        IGenericRepository<SYS_CONFIG> con = new GenericRepository<SYS_CONFIG>(context);
        SYS_CONFIG entity = new SYS_CONFIG();
        if (this.Operation() == SYSOperation.Modify)
        {
            var caseList = from p in con.Get().AsEnumerable()
                           where p.id == this.KeyID//Request.QueryString["ConfigID"]
                           select p;
            entity = caseList.ToList().FirstOrDefault();
        }
        txtCode.Text=txtCode.Text.Trim();
        if (txtCode.Text.Length > 0)
            entity.code = txtCode.Text;
        entity.type = txtType.SelectedValue;
        txtValue.Text = txtValue.Text.Trim();
        if (txtValue.Text.Length > 0)
            entity.config_value = txtValue.Text;
        txtCONFIG_DESC.Text = txtCONFIG_DESC.Text.Trim();
        if (txtCONFIG_DESC.Text.Length > 0)
            entity.config_desc = txtCONFIG_DESC.Text;
        txtCMEMO.Text = txtCMEMO.Text.Trim();
        if (txtCMEMO.Text.Length > 0)
            entity.memo = txtCMEMO.Text;
        return entity;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmBaseBOM_Edit_D.aspx", SysOperation.New, Session["BOMID"].ToString()) + "','BOM管理','BASE_BOM_D');");
    }
}

