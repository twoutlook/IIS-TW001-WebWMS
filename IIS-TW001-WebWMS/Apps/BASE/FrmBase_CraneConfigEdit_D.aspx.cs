using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Text.RegularExpressions;

public partial class BASE_FrmBase_CraneConfigEdit_D : WMSBasePage
{
    public static string SiteIDstr = "";
    public static string SiteIDS = "";
    public static string CraneIDS = "";
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData(this.KeyID);
                this.txtCraneID.Enabled = false;
                this.txtSiteID.Enabled = false;
                //--------
                //默认加载搜索数据 
                Search();
                this.GridBind();
                //将CraneIDstr保存到session中
                if (SiteIDstr != "")
                {
                    Session["SiteIDstr"] = SiteIDstr;

                }
                if (SiteIDS != "")
                {
                    Session["SiteIDS"] = SiteIDS;

                }
                if (CraneIDS != "")
                {
                    Session["CraneIDS"] = CraneIDS;

                }
                //--------

            }

            else
            {
                Session["SiteIDstr"] = "";
                Session["SiteIDS"] = "";
                Session["CraneName"] = "";
                this.txtSiteID.Enabled = true;
                this.btnAddTrade.Enabled = false;
                this.btnAddTrade.Attributes.Add("style", "color:#b5b5b5;");
            }

            if (Session["CraneIDstr"] != "" && Session["CraneIDstr"] != null)
            {
                this.txtCraneID.Text = Session["CraneIDstr"].ToString();
            }
            if (Session["CraneIDS"] != "" && Session["CraneIDS"] != null)
            {
                CraneIDS = Session["CraneIDS"].ToString();
            }

            //if (this.dplSiteTypes.SelectedValue == "1")
            //{
            //    this.btnAddTrade.Visible = false;
            //    this.Button2.Visible = false;
            //    this.btn_BlockUp.Visible = false;
            //    this.btnSearch1.Visible = false;
            //    this.grdBASE_trande.Visible = false;
            //}
            //else
            //{
                this.GridBind1();
            //}

        }

        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_CRANECONFIG_DETIAL');return false;";
        this.btnAddTrade.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBase_CraneConfigEdit_Trade.aspx", SYSOperation.New, this.KeyID) + "','" + Resources.Lang.FrmBase_CraneConfigEdit_D_Msg01+ "','BASE_CraneDetialTrade',800,600);return false;"; //新增交易类型

        this.grdBASE_trande.DataKeyNames = new string[] { "TYPEID" };
        //本页面打开新增窗口        
        //删除确认提示
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }

        Help.DropDownListDataBind(GetParametersByFlagType("BASE_CLIENT"), dplCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(GetParametersByFlagType("YesOrNo"), dplDefulSite, "", "FLAG_NAME", "FLAG_ID", "");//是否默认站点
        Help.DropDownListDataBind(GetParametersByFlagType("LiKuSiteType"), dplSiteTypes, "", "FLAG_NAME", "FLAG_ID", "");//站点类型
        Help.DropDownListDataBind(GetParametersByFlagType("INORDERBY"), ddlSorting, "", "FLAG_NAME", "FLAG_ID", "");//站点类型
        Help.DropDownListDataBind(GetParametersByFlagType("Base_CraneConfig_Detail"), ddlFormat, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(GetParametersByFlagType("CARGOSPACETYPE"), this.ddlFormat, "", "FLAG_NAME", "FLAG_ID", ""); //规格
        var InOutType = GetParametersByFlagType("InOutType");

        this.OutTypeName = InOutType.Where(x => x.FLAG_ID == "O").FirstOrDefault().FLAG_NAME;
    }

    public DataTable GetSys_ParameterByFLAGTYPE(string FLAG_TYPE)
    {
        string sql = "SELECT *  FROM [dbo].[SYS_PARAMETER] where [flag_type]='Base_CraneConfig_Detail'";
        DataTable dtBASE_CARGOSPACE = DBHelp.ExecuteToDataTable(sql);
        return dtBASE_CARGOSPACE;
    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData(string ID)
    {
        //BASE_CRANEDetialEntity entity = new BASE_CRANEDetialEntity();
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        var caseList = from p in con.Get()
                       where p.ID == ID
                       select p;
        BASE_CRANECONFIG_DETIAL entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG_DETIAL>();
        entity.ID = this.KeyID;
        Session["DETIAL_ID"] = entity.ID;
        //entity.SelectByPKeys();
        this.txtCraneID.Text = entity.CRANEID;
        this.txtSiteID.Text = entity.SITEID;
        this.txtSiteName.Text = entity.SITENAME;
        this.txtIDS.Text = entity.IDS;
        //this.txtDefulSite.Text = entity.DEFULSITE;
        SiteIDstr = entity.SITEID;

        SiteIDS = entity.ID.ToString();
        this.txtRemark.Text = entity.REMARK;
        this.ddlFormat.SelectedValue = entity.FORMAT;
        this.ddlSorting.SelectedValue = entity.YORDERBY;
        this.txtcreateuser.Text = entity.CREATEUSER;
        this.txtcreatetime.Text = entity.CREATETIME != null ? Convert.ToDateTime(entity.CREATETIME).ToString("yyyy-MM-dd HH:mm:ss") : "";
        this.txtupdateuser.Text = entity.UPDATEUSER;
        this.txtupdatetime.Text = entity.UPDATETIME != null ? Convert.ToDateTime(entity.UPDATETIME).ToString("yyyy-MM-dd HH:mm:ss") : "";
        try
        {
            //前台页面控件问题这里特殊处理一下
            string sitety = "";
            if (entity.SITETYPE == "1")
            {
                sitety = "1";
            }
            else if (entity.SITETYPE == "2")
            {
                sitety = "2";
            }
            else if (entity.SITETYPE == "3")
            {
                sitety = "3";
            }

            this.dplSiteTypes.SelectedIndex = int.Parse(sitety);
        }
        catch
        { }
        try
        {
            this.dplCSTATUS.SelectedIndex = int.Parse(entity.FLAG);
        }
        catch
        { }
        try
        {
            this.dplDefulSite.SelectedIndex = int.Parse(entity.DEFULSITE);
        }
        catch
        { }
        txtDISCONTINUETIME.Text = entity.DISCONTINUETIME != null ? Convert.ToDateTime(entity.DISCONTINUETIME).ToString("yyyy-MM-dd") : "";
        txtDISCONTINUEUSER.Text = entity.DISCONTINUEUSER;

        this.txtID.Text = entity.ID;
        this.txtStorey.Text = entity.STOREY;


    }

    /// <summary>
    /// 单<%= Resources.Lang.Base_Data%>显示页面的数据的主键编号
    /// </summary>
    public string KeyID
    {
        get
        {

            try
            {
                if (ViewState["ID"] == null)
                {
                    ViewState["ID"] = this.Page.Request.QueryString["ID"];
                }
            }
            catch (Exception innerException)
            {
                throw new Exception(Resources.Lang.FrmBase_AGV_D_Msg01 + "Querystring:QID", innerException);//未传递
            }
            return ViewState["ID"].ToString()
                ;
        }
        set
        {
            ViewState["ID"] = value;
        }
    }

    /// <summary>
    /// 出库类型名称
    /// </summary>
    public string OutTypeName
    {
        get { return this.hiddOutTypeName.Value; }

        set { this.hiddOutTypeName.Value = value; }
    }



    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //
        if (this.txtCraneID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_Msg01); //"线别编号项不允许空！"
            this.SetFocus(txtCraneID);
            return false;
        }
        //
        if (this.txtCraneID.Text.Trim().Length > 0)
        {
            if (this.txtCraneID.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_Msg02);//"线别编号项超过指定的长度30！"
                this.SetFocus(txtCraneID);
                return false;
            }
        }
        //
        if (this.txtSiteID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_AGV_D_Msg02);//"站点编号项不允许空！"
            this.SetFocus(txtSiteID);
            return false;
        }

        if (this.txtSiteID.Text.Trim().Length > 0)
        {
            if (this.txtSiteID.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmBase_AGV_D_Msg03);//"站点编号项超过指定的长度30！"
                this.SetFocus(txtSiteID);
                return false;
            }
        }


        //
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtSiteID.Text.Trim(), "^[A-Za-z0-9-_]+$"))
        {
            this.Alert(Resources.Lang.FrmBase_AGV_D_Msg04);//"站点编号项只允许输入英文字母和数字,特殊符号-_！"
            this.SetFocus(txtSiteID);
            return false;
        }
        //站点编号是否重复
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        string lineid = this.txtCraneID.Text.Trim();
        var count = con.Get().Where(p => p.SITEID == this.txtSiteID.Text.Trim() && !string.IsNullOrEmpty(p.CRANEID) && p.CRANEID == lineid);
        if (this.Operation() == SYSOperation.New && count.ToList().Count > 0)
        {
            this.Alert(Resources.Lang.FrmBase_AGV_D_Msg08);//"站点编号已存在！"
            this.SetFocus(txtSiteID);
            return false;
        }
        //
        if (this.txtSiteName.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_AGV_D_Msg05);//"站点名称项不允许空！"
            this.SetFocus(txtSiteName);
            return false;
        }
        if (this.txtSiteName.Text.Trim().Length > 0)
        {
            if (this.txtSiteName.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg02);//"站点名称项超过指定的长度30！"
                this.SetFocus(txtSiteName);
                return false;
            }
        }
        //
        if (this.dplDefulSite.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg03);//"默认站点项不允许空！"
            this.SetFocus(dplDefulSite);
            return false;
        }
        if (this.dplDefulSite.Text.Trim().Length > 0)
        {
            if (this.dplDefulSite.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg04);//"默认站点项超过指定的长度20！"
                this.SetFocus(dplDefulSite);
                return false;
            }
        }
        //
        if (this.ddlFormat.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg05);//"规格项不允许空！")
            this.SetFocus(ddlFormat);
            return false;
        }
        if (this.ddlFormat.Text.Trim().Length > 0)
        {
            if (this.ddlFormat.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg06);//"规格项超过指定的长度20！"
                this.SetFocus(ddlFormat);
                return false;
            }
        }
        //



        //
        if (this.dplSiteTypes.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg07);//"站点类型项不允许空！"
            this.SetFocus(dplSiteTypes);
            return false;
        }
        //
        if (this.dplSiteTypes.Text.Trim().Length > 0)
        {
            if (this.dplSiteTypes.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg08);//"站点类型项超过指定的长度20！"
                this.SetFocus(dplSiteTypes);
                return false;
            }
        }
        //
        if (this.dplCSTATUS.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmMixed_D_Msg13);//状态项不允许空！
            this.SetFocus(dplCSTATUS);
            return false;
        }
        //
        if (this.dplCSTATUS.Text.Trim().Length > 0)
        {
            if (this.dplCSTATUS.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg15);//状态项超过指定的长度20！
                this.SetFocus(dplCSTATUS);
                return false;
            }
        }

        if (string.IsNullOrEmpty(this.txtStorey.Text))
        {
            this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg09);//"楼层项不允许空！"
            this.SetFocus(txtStorey);
            return false;
        }
        else
        {
            int result=0;
            if (!Int32.TryParse(this.txtStorey.Text, out result))
            {
                this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg10);//"楼层项格式不正确！"
                this.SetFocus(txtStorey);
                return false;
            }
        }



        if (this.txtRemark.Text.Trim().Length > 0)
        {
            if (this.txtRemark.Text.GetLengthByByte() > 200)
            {
                this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg15); //备注项超过指定的长度200！
                this.SetFocus(txtRemark);
                return false;
            }
        }
        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_CRANECONFIG_DETIAL SendData()
    {
        //BASE_CRANEDetialEntity entity = new BASE_CRANEDetialEntity();

        BASE_CRANECONFIG_DETIAL entity = new BASE_CRANECONFIG_DETIAL();

        //
        this.txtCraneID.Text = this.txtCraneID.Text.Trim();
        if (this.txtCraneID.Text.Length > 0)
        {
            entity.CRANEID = txtCraneID.Text;
        }
        else
        {
            entity.CRANEID = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCLIENTID = null;
        }
        //
        this.txtSiteID.Text = this.txtSiteID.Text.Trim();
        if (this.txtSiteID.Text.Length > 0)
        {
            entity.SITEID = txtSiteID.Text;
        }
        else
        {
            entity.SITEID = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCLIENTNAME = null;
        }
        //
        this.txtSiteName.Text = this.txtSiteName.Text.Trim();
        if (this.txtSiteName.Text.Length > 0)
        {
            entity.SITENAME = txtSiteName.Text;
        }
        else
        {
            entity.SITENAME = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CALIAS = null;
        }
        //
        this.dplDefulSite.Text = this.dplDefulSite.Text.Trim();
        if (this.dplDefulSite.Text.Length > 0)
        {
            entity.DEFULSITE = dplDefulSite.SelectedValue;
        }
        else
        {
            entity.DEFULSITE = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCONTACTPERSON = null;
        }
        //
        this.ddlFormat.Text = this.ddlFormat.Text.Trim();
        if (this.ddlFormat.Text.Length > 0)
        {
            entity.FORMAT = ddlFormat.SelectedValue;
        }
        else
        {
            entity.FORMAT = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CPHONE = null;
        }
        //
        this.ddlSorting.SelectedValue = this.ddlSorting.SelectedValue.Trim();
        if (this.ddlSorting.SelectedValue.Length > 0)
        {
            entity.YORDERBY = ddlSorting.SelectedValue;
        }
        else
        {
            entity.YORDERBY = string.Empty;
        }

        this.txtRemark.Text = this.txtRemark.Text.Trim();
        if (this.txtRemark.Text.Length > 0)
        {
            entity.REMARK = txtRemark.Text;
        }
        else
        {
            entity.REMARK = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CERPCODE = null;
        }
        //
        this.txtcreateuser.Text = this.txtcreateuser.Text.Trim();
        if (this.txtcreateuser.Text.Length > 0)
        {
            entity.CREATEUSER = txtcreateuser.Text;
        }
        else
        {
            entity.CREATEUSER = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CADDRESS = null;
        }
        //
        this.txtupdateuser.Text = this.txtupdateuser.Text.Trim();
        if (this.txtupdateuser.Text.Length > 0)
        {
            entity.UPDATEUSER = txtupdateuser.Text;
        }
        else
        {
            entity.UPDATEUSER = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CTYPE = null;
        }

        //
        this.dplSiteTypes.Text = this.dplSiteTypes.Text.Trim();
        if (this.dplSiteTypes.Text.Length > 0)
        {
            entity.SITETYPE = dplSiteTypes.SelectedValue;
        }
        else
        {
            entity.SITETYPE = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSTATUS = null;
        }
        this.dplCSTATUS.Text = this.dplCSTATUS.Text.Trim();
        if (this.dplCSTATUS.Text.Length > 0)
        {
            entity.FLAG = dplCSTATUS.SelectedValue;
        }
        else
        {
            entity.FLAG = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSTATUS = null;
        }
        entity.STOREY = !string.IsNullOrEmpty(this.txtStorey.Text) ? this.txtStorey.Text.Trim() : string.Empty;

        if (this.Operation() == SYSOperation.New)
        {
            entity.CREATETIME = DateTime.Now;
            entity.CREATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.UPDATETIME = DateTime.Now;
            entity.UPDATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
        }
        else if (this.Operation() == SYSOperation.Modify)
        {
            entity.CREATETIME = Convert.ToDateTime(txtcreatetime.Text.Trim());
            entity.CREATEUSER = txtcreateuser.Text.Trim();
            entity.UPDATETIME = DateTime.Now;
            entity.UPDATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            if (!string.IsNullOrEmpty(txtDISCONTINUETIME.Text.Trim()))
            {
                entity.DISCONTINUETIME = Convert.ToDateTime(txtDISCONTINUETIME.Text.Trim());
                entity.DISCONTINUEUSER = txtDISCONTINUEUSER.Text.Trim();
            }
        }

        #region 界面上不可见的字段项
        /*
        entity.CDEFINE1 = ;
        entity.CDEFINE2 = ;
        entity.DDEFINE3 = ;
        entity.DDEFINE4 = ;
        entity.IDEFINE5 = ;
        */
        #endregion
        return entity;

    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        if (this.CheckData())
        {
            //BASE_CRANEDetialEntity entity = (BASE_CRANEDetialEntity)this.SendData();
            BASE_CRANECONFIG_DETIAL entity = (BASE_CRANECONFIG_DETIAL)this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";

            try
            {
                BASE_CRANECONFIG_DETIAL listQuery = new BASE_CRANECONFIG_DETIAL();
                //BASE_FrmBASE_CraneDetialQuery listQuery = new BASE_FrmBASE_CraneDetialQuery();
                //检查同一个CraneID下的SiteID是否有重复
                //DataTable SiteRowCount = listQuery.GetCheckList(entity.CRANEID, entity.SITEID);


                if (this.Operation() == SYSOperation.Modify)
                {


                    entity.IDS = txtIDS.Text.Trim();
                    entity.CREATETIME = Convert.ToDateTime(txtcreatetime.Text.Trim());

                    strKeyID = txtID.Text.Trim();
                    entity.ID = strKeyID;
                    entity.UPDATETIME = DateTime.Now;
                    entity.UPDATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Update(entity);

                    //检查同一个CraneID下的SiteID是否有重复
                    //DataTable SiteRowCount = listQuery.GetCheckList(entity.CRANEID, entity.SITEID);

                    //当默认站点保存为认认（0）时，先遍历将同一个craneid下所有默认站点先设置成1（否），最后在更新
                    //SetDefulSite(entity, listQuery, con);

                    var SiteRowCount = con.Get().Where(p => p.CRANEID == entity.CRANEID && p.SITEID == entity.SITEID);
                    if (SiteRowCount.ToList().Count > 1)
                    {
                        string msg = Resources.Lang.FrmBase_CraneConfigEdit_D_Msg11 + entity.CRANEID + Resources.Lang.FrmBase_CraneConfigEdit_D_Msg12 + entity.SITEID + Resources.Lang.FrmBase_CraneConfigEdit_D_Msg13; //"同一个线别ID" + entity.CRANEID + "下已经存在该站点ID" + entity.SITEID + ",不能重复填写！"
                        this.Alert(msg);
                    }
                    else
                    {
                        con.Save();
                        this.AlertAndBack("FrmBase_CraneConfigEdit_D.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
                    }



                }

                else if (this.Operation() == SYSOperation.New)
                {
                    //检查同一个CraneID下的总数是否有超过16条
                    //DataTable CraneRowCount = listQuery.GetCheckList(entity.CRANEID, "");
                    var CraneRowCount = con.Get().Where(p => p.CRANEID == entity.CRANEID);

                    var SiteRowCount = con.Get().Where(p => p.CRANEID == entity.CRANEID && p.SITEID == entity.SITEID);
                    if (SiteRowCount.ToList().Count > 0)
                    {
                        string msg = Resources.Lang.FrmBase_CraneConfigEdit_D_Msg11 + entity.CRANEID + Resources.Lang.FrmBase_CraneConfigEdit_D_Msg12 + entity.SITEID + Resources.Lang.FrmBase_CraneConfigEdit_D_Msg13;//"同一个线别ID" + entity.CRANEID + "下已经存在该站点ID" + entity.SITEID + ",不能重复填写！";
                        this.Alert(msg);
                    }
                    else if (CraneRowCount.ToList().Count > 16)
                    {
                        this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg14);//同一个线别ID总数不能超过16条！
                    }
                    else
                    {

                        //当默认站点保存为认认（0）时，先遍历将同一个craneid下所有默认站点先设置成1（否），最后在更新
                        //SetDefulSite(entity, listQuery,con);

                        entity.CREATETIME = DateTime.Now;
                        strKeyID = Guid.NewGuid().ToString();
                        entity.ID = strKeyID;
                        entity.IDS = CraneIDS;
                        entity.CREATETIME = DateTime.Now;
                        entity.CREATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                        entity.UPDATETIME = DateTime.Now;
                        entity.UPDATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                        con.Insert(entity);
                        con.Save();
                        //根据类型保存站点交易类型
                        var caselist = GetQueryList();
                        foreach (var item in caselist)
                        {
                            IGenericRepository<BASE_CRANECONFIG_TRADETYPE> tcon = new GenericRepository<BASE_CRANECONFIG_TRADETYPE>(context);
                            BASE_CRANECONFIG_TRADETYPE entity_t = new BASE_CRANECONFIG_TRADETYPE();
                            entity_t.IDS = Guid.NewGuid().ToString();
                            entity_t.ID = strKeyID;
                            entity_t.TYPENAME = item.TYPENAME;
                            entity_t.TYPEID = item.TYPEID;
                            entity_t.CERPCODE = item.CERPCODE;
                            entity_t.CSTATUS = "0";
                            entity_t.INOROUTNAME = item.T == "I" ? Resources.Lang.FrmBase_CraneConfigEdit_D_Msg15 : Resources.Lang.FrmBase_CraneConfigEdit_D_Msg16;//"入库" : "出库";
                            entity_t.INOROUTCODE = item.T;
                            tcon.Insert(entity_t);
                            tcon.Save();
                            //item.TRANSACTION_ACTION_ID;
                        }
                        this.AlertAndBack("FrmBase_CraneConfigEdit_D.aspx?" + BuildQueryString(SYSOperation.New, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                    }


                }
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATEEdit_Msg17 + E.Message); //"失败！" 
#if Debug 
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
            }
        }

    }

    private static void SetDefulSite(BASE_CRANECONFIG_DETIAL entity, BASE_CRANECONFIG_DETIAL listQuery, IGenericRepository<BASE_CRANECONFIG_DETIAL> con)
    {
        if (entity.DEFULSITE == "0")
        {
            //设置默认站点时（是），对其他数据置置成否（设置的必须是同一个线别下的所有默认状态）
            //DataTable DefulSiteCount = listQuery.GetDefulSiteList(entity.CRANEID, entity.SITETYPE, "0");
            //var DefulSiteCount = con.Get().Where(p => p.CRANEID == entity.CRANEID && p.SITETYPE == entity.SITETYPE && p.DEFULSITE=="0");
            string sql = "SELECT *  FROM [dbo].[BASE_CRANECONFIG_DETIAL] where [CRANEID]='" + entity.CRANEID + "' and [SITETYPE] = '" + entity.SITETYPE + "' and [DEFULSITE] = '0'";
            DataTable DefulSiteCount = DBHelp.ExecuteToDataTable(sql);
            if (DefulSiteCount.Rows.Count > 0)
            {
                for (int i = 0; i < DefulSiteCount.Rows.Count; i++)
                {
                    //遍历全部设置成否
                    BASE_CRANECONFIG_DETIAL entitys = new BASE_CRANECONFIG_DETIAL();

                    entitys.ID = DefulSiteCount.Rows[i][0].ToString();
                    //entitys.CRANEID = DefulSiteCount.Rows[i][1].ToString();
                    //entitys.SITEID = DefulSiteCount.Rows[i][2].ToString();
                    //entitys.SITENAME = DefulSiteCount.Rows[i][3].ToString();
                    //entitys.SITETYPE = DefulSiteCount.Rows[i][4].ToString();
                    //entitys.FLAG = DefulSiteCount.Rows[i][5].ToString();
                    //entitys.CREATETIME = Convert.ToDateTime(DefulSiteCount.Rows[i][6].ToString());
                    //entitys.CREATEUSER = DefulSiteCount.Rows[i][7].ToString();
                    //entitys.UPDATEUSER = DefulSiteCount.Rows[i][8].ToString();
                    //entitys.UPDATETIME = Convert.ToDateTime(DefulSiteCount.Rows[i][9].ToString());
                    //entitys.DEFULSITE = "1";
                    //entitys.FORMAT = DefulSiteCount.Rows[i][11].ToString();
                    //entitys.REMARK = DefulSiteCount.Rows[i][12].ToString();
                    //entitys.IDS = DefulSiteCount.Rows[i][13].ToString();


                    string sql1 = "update [dbo].[BASE_CRANECONFIG_DETIAL] set  [DEFULSITE] = '1' where [ID]='" + entitys.ID + "' ";
                    DBHelp.ExcuteScalarSQL(sql1);

                    //con.Update(entitys);
                    //con.Save();
                }

            }
        }
    }

    ///查詢方法
    /// <summary>
    /// 查詢方法
    /// </summary>
    public void Search()
    {
        ////重新设置GridNavigator的RowCount
        //BASE_FrmBASE_CraneDetialScanQuery listQuery = new BASE_FrmBASE_CraneDetialScanQuery();
        //DataTable dtRowCount = listQuery.GetList(SiteIDS, SiteIDstr, "", "", "", "", true, 0, 0);
        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdNavigatorBASE_CraneDetailScan.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdNavigatorBASE_CraneDetailScan.RowCount = 0;
        //}
    }

    public void GridBind()
    {
        
    }






    public string Status
    {
        get
        {
            if (ViewState["Status"] != null)
            {
                return ViewState["Status"].ToString();
            }
            return "";
        }
        set { ViewState["Status"] = value; }
    }



    protected void btnSearch1_Click(object sender, EventArgs e)
    {

        this.GridBind1();
    }

    public void GridBind1()
    {
        if (this.Operation() == SYSOperation.Modify)
        {
            if (GetTradeTypeList(txtID.Text.Trim()) != null && GetTradeTypeList(txtID.Text.Trim()).Count() > 0)
            {
                var list = GetTradeTypeList(txtID.Text.Trim());
                if (list != null && list.Count() > 0)
                {
                    AspNetPager2.RecordCount = list.Count();
                    AspNetPager2.PageSize = this.PageSize;
                }
                AspNetPager2.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

                var data = GetPageSize(list, PageSize, CurrendIndex).ToList();
                List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
                flagList.Add(new Tuple<string, string>("ENABLE", "UsedOrCanceled"));//是否保税
                flagList.Add(new Tuple<string, string>("T", "InOutType"));//类型

                var srcdata = GetGridSourceDataByList(data, flagList);

                grdBASE_trande.DataSource = srcdata;
                grdBASE_trande.DataBind();
            }
        }
        else
        {
            var caseList = GetQueryList();
            if (caseList != null && caseList.Count() > 0)
            {
                AspNetPager2.RecordCount = caseList.Count();
                AspNetPager2.PageSize = this.PageSize;
            }
            AspNetPager2.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

            var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
            List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
            flagList.Add(new Tuple<string, string>("ENABLE", "UsedOrCanceled"));//是否保税
            flagList.Add(new Tuple<string, string>("T", "InOutType"));//类型

            var srcdata = GetGridSourceDataByList(data, flagList);

            grdBASE_trande.DataSource = srcdata;
            grdBASE_trande.DataBind();
        }

    }
    public IQueryable<V_INOutType> GetQueryList()
    {
        IGenericRepository<V_INOutType> conn = new GenericRepository<V_INOutType>(db);
        var caseList = from p in conn.Get()
                       orderby p.CREATEDATE descending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {

            string v = "";
            if (dplSiteTypes.SelectedValue == "1")
            {
                v = "I";
            }
            else if (dplSiteTypes.SelectedValue == "2")
            {
                v = "O";
            }
            //else
            //{
            //    v = "";
            //}
            if (v != "")
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.T) && x.T.Equals(v));
            }


            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ENABLE) && x.ENABLE.Equals("0"));





        }
        return caseList;
    }

    /// <summary>
    /// 查询站点下交易类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IQueryable<V_TRADETYPE> GetTradeTypeList(string id)
    {
        IGenericRepository<V_TRADETYPE> conn = new GenericRepository<V_TRADETYPE>(db);
        var caseList = from p in conn.Get()
                       orderby p.TYPENAME descending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ID) && x.ID.Equals(id));
        return caseList;
    }

    protected void btnSave1_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CRANECONFIG_TRADETYPE> con = new GenericRepository<BASE_CRANECONFIG_TRADETYPE>(context);
        IGenericRepository<V_TRADETYPE> cons = new GenericRepository<V_TRADETYPE>(context);
        try
        {
            int n = 0;
            for (int i = 0; i < this.grdBASE_trande.Rows.Count; i++)
            {
                if (this.grdBASE_trande.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_trande.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        //主键赋值
                        string ID = this.grdBASE_trande.DataKeys[i].Values[0].ToString();

                        var caseLists = from p in con.Get()
                                        where p.IDS == ID
                                        select p;
                        BASE_CRANECONFIG_TRADETYPE entitys = caseLists.ToList().FirstOrDefault<BASE_CRANECONFIG_TRADETYPE>();
                        if (entitys.CSTATUS == "0")
                            n++;
                    }
                }
            }
            if (n > 0)
            {
                this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg17);//状态为使用中，请勿重复启用！
                return;
            }
            else
            {
                for (int i = 0; i < this.grdBASE_trande.Rows.Count; i++)
                {
                    if (this.grdBASE_trande.Rows[i].Cells[0].Controls[1] is CheckBox)
                    {
                        CheckBox chkSelect = (CheckBox)this.grdBASE_trande.Rows[i].Cells[0].Controls[1];
                        if (chkSelect.Checked)
                        {
                            //主键赋值
                            string ID = this.grdBASE_trande.DataKeys[i].Values[0].ToString();

                            var caseLists = from p in con.Get()
                                            where p.IDS == ID
                                            select p;
                            BASE_CRANECONFIG_TRADETYPE entitys = caseLists.ToList().FirstOrDefault<BASE_CRANECONFIG_TRADETYPE>();
                            entitys.CSTATUS = "0";
                            con.Update(entitys);
                            con.Save();
                        }
                    }
                }
                this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg18);//启用成功！
                this.GridBind1();
            }
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg19 + E.Message); //启用失败！
#if Debug 
            this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
        }     
    }


    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager2.CurrentPageIndex;//索引同步
        GridBind1();
    }

    protected void grdBASE_trande_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDq(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];

            if (e.Row.Cells[2].Text == this.OutTypeName && e.Row.Cells[3].Text == "203")
            {
                linkModify.NavigateUrl = "#";
                this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBase_CraneConfigEdit_D_BillNo.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBase_CraneConfigEdit_D_Msg20, "BILLNO", 600, 400); //关联单据类型
            }
            else {
                linkModify.Visible = false;
            }
        }
    }

    private string GetKeyIDq(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdBASE_trande.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBASE_trande.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }








    /// <summary>
    /// 站点交易类型停用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_BlockUp_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CRANECONFIG_TRADETYPE> con = new GenericRepository<BASE_CRANECONFIG_TRADETYPE>(context);
        IGenericRepository<V_TRADETYPE> cons = new GenericRepository<V_TRADETYPE>(context);
        try
        {
            int n = 0;
            for (int i = 0; i < this.grdBASE_trande.Rows.Count; i++)
            {
                if (this.grdBASE_trande.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_trande.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        //主键赋值
                        string ID = this.grdBASE_trande.DataKeys[i].Values[0].ToString();

                        var caseLists = from p in con.Get()
                                        where p.IDS == ID
                                        select p;
                        BASE_CRANECONFIG_TRADETYPE entitys = caseLists.ToList().FirstOrDefault<BASE_CRANECONFIG_TRADETYPE>();
                        if (entitys.CSTATUS == "1")
                            n++;
                    }
                }
            }
            if (n > 0)
            {
                this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg21); //状态为作废，请勿重复作废！
                return;
            }
            else
            {
                for (int i = 0; i < this.grdBASE_trande.Rows.Count; i++)
                {
                    if (this.grdBASE_trande.Rows[i].Cells[0].Controls[1] is CheckBox)
                    {
                        CheckBox chkSelect = (CheckBox)this.grdBASE_trande.Rows[i].Cells[0].Controls[1];
                        if (chkSelect.Checked)
                        {
                            //主键赋值
                            string ID = this.grdBASE_trande.DataKeys[i].Values[0].ToString();

                            var caseLists = from p in con.Get()
                                            where p.IDS == ID
                                            select p;
                            BASE_CRANECONFIG_TRADETYPE entitys = caseLists.ToList().FirstOrDefault<BASE_CRANECONFIG_TRADETYPE>();
                            entitys.CSTATUS = "1";
                            con.Update(entitys);
                            con.Save();
                        }
                    }
                }
                this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg22);//作废成功！
                this.GridBind1();
            }

        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_D_Msg23 + E.Message); //作废失败！
#if Debug 
            this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
        }
    }
}