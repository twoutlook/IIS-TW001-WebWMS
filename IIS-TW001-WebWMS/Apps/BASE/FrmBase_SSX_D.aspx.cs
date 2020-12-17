using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_FrmBase_SSX_D : WMSBasePage
{
    #region 常量
    DBContext context = new DBContext();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();
            this.BindSite();
            if (this.Operation() == SYSOperation.New)
            {
                hdnIDS.Value = this.KeyID;
                hdnID.Value = Guid.NewGuid().ToString();
                txtcreateuser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtcreatetime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtupdateuser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtupdatetime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtCraneID.Text = GetCraneID(hdnIDS.Value);
            }
            if (this.Operation() == SYSOperation.Modify)
            {
                this.ShowData(this.KeyID);
                this.txtSiteID.Enabled = false;
            }
        }
    }

    #region even
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        if (this.CheckData())
        {
            BASE_CRANECONFIG_DETIAL entity = (BASE_CRANECONFIG_DETIAL)this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";

            try
            {
                //检查IP是否有重复
                BASE_CRANECONFIG cq = new BASE_CRANECONFIG();

                BASE_CRANECONFIG_DETIAL_SCAN cqs = new BASE_CRANECONFIG_DETIAL_SCAN();
                //检查绑定立库站点是否有重复,PLC区域是否有重复
                if (this.Operation() == SYSOperation.Modify)
                {
                    strKeyID = hdnID.Value;
                    entity.ID = strKeyID;
                    con.Update(entity);
                    if (!string.IsNullOrEmpty(entity.RELATIONSITE))
                    {
                        var lkcount = con.Get().Where(p => p.RELATIONSITE == entity.RELATIONSITE && p.ID != entity.ID);
                        if (lkcount.ToList().Count > 0)
                        {
                            this.Alert(Resources.Lang.FrmBase_RGV_D_Msg01 + "[" + ddlLKSite.SelectedItem.Text + "]" + Resources.Lang.FrmBase_CAR_D_Msg01);//所关联的立库站点[" + ddlLKSite.SelectedItem.Text + "]已被其他台车站点绑定！"
                            return;
                        }
                    }
                    if (!string.IsNullOrEmpty(entity.PLCREGION))
                    {
                        var list = con.Get().Where(p => p.PLCREGION == entity.PLCREGION && p.ID != entity.ID);
                        if (list.ToList().Count > 0)
                        {
                            this.Alert(Resources.Lang.FrmBase_RGV_D_Msg03 + "[" + entity.PLCREGION + "]" + Resources.Lang.FrmBase_CAR_D_Msg01);//PLC地址区[" + entity.PLCREGION + "]已被其他台车站点绑定！
                            return;
                        }
                    }
                    con.Save();
                    this.AlertAndBack("FrmBase_SSX_D.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    strKeyID = hdnID.Value;
                    entity.ID = strKeyID;
                    con.Insert(entity);
                    if (!string.IsNullOrEmpty(entity.RELATIONSITE))
                    {
                        var lkcount = con.Get().Where(p => p.RELATIONSITE == entity.RELATIONSITE);
                        if (lkcount.ToList().Count > 0)
                        {
                            this.Alert(Resources.Lang.FrmBase_RGV_D_Msg01 + "[" + ddlLKSite.SelectedItem.Text + "]" + Resources.Lang.FrmBase_CAR_D_Msg01); //所关联的立库站点[" + ddlLKSite.SelectedItem.Text + "]已被其他台车站点绑定！
                            return;
                        }
                    }
                    if (!string.IsNullOrEmpty(entity.PLCREGION))
                    {
                        var lkcount = con.Get().Where(p => p.PLCREGION == entity.PLCREGION);
                        if (lkcount.ToList().Count > 0)
                        {
                            this.Alert(Resources.Lang.FrmBase_RGV_D_Msg03 + "[" + entity.PLCREGION + "]" + Resources.Lang.FrmBase_CAR_D_Msg01);//"PLC地址区[" + ddlLKSite.SelectedItem.Text + "]已被其他台车站点绑定！");
                            return;
                        }
                    }
                    con.Save();
                    this.AlertAndBack("FrmBase_SSX_D.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                    //更新主表站点数量
                    var count = con.Get().Where(p => p.IDS == hdnIDS.Value && p.ID != hdnID.Value);
                    IGenericRepository<BASE_CRANECONFIG> con1 = new GenericRepository<BASE_CRANECONFIG>(context);
                    var caseList = from p in con1.Get()
                                   where p.ID == hdnIDS.Value
                                   select p;
                    BASE_CRANECONFIG entity_c = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG>();
                    entity_c.SITECOUNT = caseList.ToList().Count() + 1;
                    con1.Update(entity_c);
                    con1.Save();
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

    #endregion

    #region Function
    // <summary>
    /// 初始化页面。主要做一下动作
    ///1、DropDownList,ListBox数据装载,
    ///2、新增按钮、删除的按钮的Java脚本注册等
    ///一般在PageLoad 事件调用，
    ///并且IsPostBack = false时
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_CAR_D');return false;";

        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        Help.DropDownListDataBind(GetParametersByFlagType("RGVSITETYPE"), this.dplSiteTypes, "", "FLAG_NAME", "FLAG_ID", "");//站点类型
        Help.DropDownListDataBind(GetParametersByFlagType("BASE_CLIENT"), dplCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(GetParametersByFlagType("YesOrNo"), dplDefulSite, "", "FLAG_NAME", "FLAG_ID", "");//默认站点
        Help.DropDownListDataBind(GetParametersByFlagType("YorN"), ddlIsMonitor, Resources.Lang.Commona_PleaseSelect, "FLAG_NAME", "FLAG_ID", "");//是否监听
        Help.DropDownListDataBind(GetParametersByFlagType("CARGOSPACETYPE"), this.ddlFormat, "", "FLAG_NAME", "FLAG_ID", ""); //规格
    }

    protected void ShowData(string ids)
    {
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        var caseList = from p in con.Get()
                       where p.ID == ids
                       select p;
        BASE_CRANECONFIG_DETIAL entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG_DETIAL>();
        entity.ID = this.KeyID;
        txtCraneID.Text = entity.CRANEID;
        txtSiteID.Text = entity.SITEID;
        txtSiteName.Text = entity.SITENAME;
        dplSiteTypes.SelectedValue = entity.SITETYPE;
        dplDefulSite.SelectedValue = entity.DEFULSITE;
        ddlFormat.SelectedValue = entity.FORMAT;
        dplCSTATUS.SelectedValue = entity.FLAG;
        if (!string.IsNullOrEmpty(entity.RELATIONSITE))
            ddlLKSite.SelectedValue = entity.RELATIONSITE;
        txtcreateuser.Text = entity.CREATEUSER;
        txtcreatetime.Text = Convert.ToDateTime(entity.CREATETIME).ToString("yyyy-MM-dd");
        txtupdateuser.Text = entity.UPDATEUSER;
        txtupdatetime.Text = Convert.ToDateTime(entity.UPDATETIME).ToString("yyyy-MM-dd");
        txtRemark.Text = entity.REMARK;
        ddlIsMonitor.SelectedValue = entity.ISMONITOR;
        ddlIsWhole.SelectedValue = entity.ISWHOLE;
        txtDISCONTINUETIME.Text = entity.DISCONTINUETIME != null ? Convert.ToDateTime(entity.DISCONTINUETIME).ToString("yyyy-MM-dd") : "";
        txtDISCONTINUEUSER.Text = entity.DISCONTINUEUSER;
        txtPLCREGION.Text = entity.PLCREGION;
        txtSTOREY.Text = entity.STOREY;
        if (!string.IsNullOrEmpty(entity.AGVSITE))
            ddlAGVSITE.SelectedValue = entity.AGVSITE;
        if (!string.IsNullOrEmpty(entity.STORAGESITE))
            ddlStorageSite.SelectedValue = entity.STORAGESITE;
        hdnID.Value = entity.ID;
        hdnIDS.Value = entity.IDS;
        rdoDefaultIn.SelectedValue = entity.IS_DEFAULT_IN.Equals("1") ? Resources.Lang.FrmALLOCATE_DEdit_SN_Msg03 : Resources.Lang.FrmALLOCATE_DEdit_SN_Msg04;//"是""否";
    }

    public string GetCraneID(string id)
    {
        IGenericRepository<BASE_CRANECONFIG> con = new GenericRepository<BASE_CRANECONFIG>(context);
        var caseList = from p in con.Get()
                       where p.ID == id
                       select p;
        BASE_CRANECONFIG entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG>();
        return entity.CRANEID;
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
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //
        if (this.txtCraneID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_SSX_D_Msg04); //输送线编号项不允许空！
            this.SetFocus(txtCraneID);
            return false;
        }
        //
        if (this.txtCraneID.Text.Trim().Length > 0)
        {
            if (this.txtCraneID.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmBase_SSX_D_Msg05);//输送线编号项超过指定的长度30！
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
        //
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
        //站点编号是否重复
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        var count = con.Get().Where(p => p.SITEID == this.txtSiteID.Text.Trim());
        if (this.Operation() == SYSOperation.New && count.ToList().Count > 0)
        {
            this.Alert(Resources.Lang.FrmBase_AGV_D_Msg08);//"站点编号已存在！"
            this.SetFocus(txtSiteID);
            return false;
        }
        //楼层只能是英文数字
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtSTOREY.Text.Trim(), "^[0-9]+$"))
        {
            this.Alert(Resources.Lang.FrmBase_AGV_D_Msg07); //楼层项只允许输入数字！
            this.SetFocus(txtSTOREY);
            return false;
        }
        //如果agv站点不为空，验证该AGV站点是否被使用
        if (!string.IsNullOrEmpty(ddlAGVSITE.SelectedValue))
        {
            var caseList = from p in con.Get()
                           orderby p.CREATETIME descending
                           where 1 == 1
                           select p;
            if (this.Operation() == SYSOperation.New)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.AGVSITE) && x.AGVSITE.Equals(ddlAGVSITE.SelectedValue));
                if (caseList != null && caseList.Count() > 0)
                {
                    this.Alert(Resources.Lang.WMS_Common_Element_ShuSongXianSite + "[" + ddlAGVSITE.SelectedValue + "]," + Resources.Lang.FrmBase_SSX_D_Msg01);//AGV站点[" + ddlAGVSITE.SelectedValue + "],已被其他台车站点绑定！
                    this.SetFocus(ddlAGVSITE);
                    return false;
                }
            }
            if (this.Operation() == SYSOperation.Modify)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.AGVSITE) && x.AGVSITE.Equals(ddlAGVSITE.SelectedValue) && x.IDS != this.KeyID);
                if (caseList != null && caseList.Count() > 0)
                {
                    this.Alert(Resources.Lang.WMS_Common_Element_ShuSongXianSite + "[" + ddlAGVSITE.SelectedValue + "]," + Resources.Lang.FrmBase_SSX_D_Msg01); //AGV站点[" + ddlAGVSITE.SelectedValue + "],已被其他台车站点绑定！
                    this.SetFocus(ddlAGVSITE);
                    return false;
                }
            }
            //判断AGV站点，等待站点楼层与SJJ楼层是否相同
            if (GetSiteLife(ddlAGVSITE.SelectedValue, "6") != txtSTOREY.Text.Trim())
            {
                this.Alert(Resources.Lang.WMS_Common_Element_ShuSongXianSite + "[" + ddlAGVSITE.SelectedValue + "]," + Resources.Lang.FrmBase_SSX_D_Msg02);//AGV站点[" + ddlAGVSITE.SelectedValue + "],与台车站点楼层不同！
                this.SetFocus(ddlAGVSITE);
                return false;
            }
        }
        //如果agv等待站点不为空，验证该AGV站点是否被使用
        if (!string.IsNullOrEmpty(ddlAGVSITE.SelectedValue))
        {
            var caseList = from p in con.Get()
                           orderby p.CREATETIME descending
                           where 1 == 1
                           select p;
            if (this.Operation() == SYSOperation.New)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.STORAGESITE) && x.STORAGESITE.Equals(ddlStorageSite.SelectedValue));
                if (caseList != null && caseList.Count() > 0)
                {
                    this.Alert(Resources.Lang.FrmBase_RGV_D_Label12 + "[" + ddlStorageSite.SelectedValue + "]," + Resources.Lang.FrmBase_SSX_D_Msg03);//等待站点[" + ddlStorageSite.SelectedValue + "],已被其他台车或提升机站点绑定！
                    this.SetFocus(ddlStorageSite);
                    return false;
                }
            }
            if (this.Operation() == SYSOperation.Modify)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.STORAGESITE) && x.STORAGESITE.Equals(ddlStorageSite.SelectedValue) && x.IDS != this.KeyID);
                if (caseList != null && caseList.Count() > 0)
                {
                    this.Alert(Resources.Lang.FrmBase_RGV_D_Label12 + "[" + ddlStorageSite.SelectedValue + "]," + Resources.Lang.FrmBase_SSX_D_Msg03);//等待站点[" + ddlStorageSite.SelectedValue + "],已被其他台车或提升机站点绑定！
                    this.SetFocus(ddlStorageSite);
                    return false;
                }
            }
            //判断AGV站点，等待站点楼层与SJJ楼层是否相同
            if (GetSiteLife(ddlStorageSite.SelectedValue, "5") != txtSTOREY.Text.Trim())
            {
                this.Alert(Resources.Lang.FrmBase_RGV_D_Label12 + "[" + ddlStorageSite.SelectedValue + "]," + Resources.Lang.FrmBase_RGV_D_Msg08);//等待站点[" + ddlStorageSite.SelectedValue + "],与提升机站点楼层不同！
                this.SetFocus(ddlStorageSite);
                return false;
            }
        }
        //PLC地址区只能是英文数字
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtPLCREGION.Text.Trim(), "^[0-9a-zA-Z]+$"))
        {
            this.Alert(Resources.Lang.FrmBase_RGV_D_Msg09);//PLC地址项只允许输入英文和数字！
            this.SetFocus(txtPLCREGION);
            return false;
        }
        //默认入库站点配置
        if (rdoDefaultIn.SelectedValue == Resources.Lang.FrmALLOCATE_DEdit_SN_Msg03)
        { //"是"
            //判断入库类型
            if (dplSiteTypes.SelectedValue != "1" && dplSiteTypes.SelectedValue != "3")
            {
                this.Alert(Resources.Lang.FrmBase_RGV_D_Msg10);//只有站点类型为入或者全部的才能设置为默认入库站点！
                this.SetFocus(rdoDefaultIn);
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 获取AGV站点信息
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public DataTable GetAGVSite(string type)
    {
        string sql = string.Format(@"SELECT  SITEID ,
                                            SITENAME
                                    FROM    dbo.BASE_CRANECONFIG_DETIAL
                                    WHERE   FLAG = '0'
                                            AND AGVSITETYPE = '{0}'
                                            AND AGVSITETYPE IS NOT NULL", type);
        return DBHelp.ExecuteToDataTable(sql);
    }

    /// <summary>
    /// 获取AGV站点楼层
    /// </summary>
    /// <param name="sitecode"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public string GetSiteLife(string sitecode, string type)
    {
        string strSQL = string.Format(@"SELECT  STOREY
                                        FROM    dbo.BASE_CRANECONFIG_DETIAL
                                        WHERE   AGVSITETYPE = '{0}'
                                                AND SITEID = '{1}'", type, sitecode);
        return DBHelp.ExecuteScalar(strSQL);
    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_CRANECONFIG_DETIAL SendData()
    {
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
        if (!string.IsNullOrEmpty(ddlAGVSITE.SelectedValue))
        {
            entity.AGVSITE = ddlAGVSITE.SelectedValue;
        }
        if (!string.IsNullOrEmpty(ddlStorageSite.SelectedValue))
        {
            entity.STORAGESITE = ddlStorageSite.SelectedValue;
        }
        entity.SITETYPE = dplSiteTypes.SelectedValue;
        entity.DEFULSITE = dplDefulSite.SelectedValue;
        entity.FORMAT = ddlFormat.SelectedValue;
        entity.FLAG = dplCSTATUS.SelectedValue;
        entity.STOREY = this.txtSTOREY.Text.Trim();
        entity.ISWHOLE = ddlIsWhole.SelectedValue;
        entity.ISMONITOR = ddlIsMonitor.SelectedValue;
        //备注
        if (this.txtRemark.Text.Trim().Length > 0)
        {
            entity.REMARK = txtRemark.Text.Trim();
        }
        else
        {
            entity.REMARK = string.Empty;
        }
        entity.IDS = hdnIDS.Value;
        if (this.Operation() == SYSOperation.New)
        {
            entity.CREATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.CREATETIME = DateTime.Now;
            entity.UPDATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.UPDATETIME = DateTime.Now;
        }
        else if (this.Operation() == SYSOperation.Modify)
        {
            entity.CREATEUSER = this.txtcreateuser.Text.Trim();
            entity.CREATETIME = Convert.ToDateTime(this.txtcreatetime.Text.Trim());
            entity.UPDATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.UPDATETIME = DateTime.Now;
            if (!string.IsNullOrEmpty(txtDISCONTINUETIME.Text.Trim()))
            {
                entity.DISCONTINUETIME = Convert.ToDateTime(txtDISCONTINUETIME.Text.Trim());
                entity.DISCONTINUEUSER = txtDISCONTINUEUSER.Text.Trim();
            }
        }
        if (!string.IsNullOrEmpty(ddlLKSite.SelectedValue))
        {
            entity.RELATIONSITE = ddlLKSite.SelectedValue;
        }
        if (!string.IsNullOrEmpty(txtPLCREGION.Text.Trim()))
            entity.PLCREGION = txtPLCREGION.Text.Trim();
        entity.IS_DEFAULT_IN = rdoDefaultIn.SelectedValue == Resources.Lang.FrmALLOCATE_DEdit_SN_Msg03 ? "1" : "0";//"是"
        return entity;

    }

    /// <summary>
    /// 绑定立库站点
    /// </summary>
    protected void BindSite()
    {
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        var caseList = context.Database.SqlQuery<BASE_CRANECONFIG_DETIAL>(@"SELECT  *
                                                                            FROM    ( SELECT    ID ,
                                                                                                CRANEID ,
                                                                                                CRANEID + '线' + SITEID + '站' AS      siteid ,
                                                                                                SITENAME ,
                                                                                                SITETYPE ,
                                                                                                FLAG ,
                                                                                                CREATETIME ,
                                                                                                CREATEUSER ,
                                                                                                UPDATEUSER ,
                                                                                                UPDATETIME ,
                                                                                                DEFULSITE ,
                                                                                                FORMAT ,
                                                                                                REMARK ,
                                                                                                IDS ,
                                                                                                RELATIONSITE,
                                                                                                ISWHOLE,
                                                                                                ISMONITOR,
                                                                                                PLCREGION,
                                                                                                DISCONTINUEUSER,
                                                                                                DISCONTINUETIME,
                                                                                                STOREY,
                                                                                                STOREYLIMIT,
                                                                                                AGVSITE,
                                                                                                RGVSITE,
                                                                                                AGVSITETYPE,
                                                                                                STORAGESITE,
                                                                                                YORDERBY,
                                                                                                ISNULL(IS_OCCUPY,'0') AS IS_OCCUPY,
                                                                                                IS_DEFAULT_IN,
                                                                                                MONITORTYPE
                                                                                      FROM      BASE_CRANECONFIG_DETIAL
                                                                                      WHERE     IDS IN ( SELECT b.ID
                                                                                                         FROM   BASE_CRANECONFIG b
                                                                                                         WHERE  b.PLCType = 'LK' )
                                                                                      UNION ALL
                                                                                      SELECT    '' ,
                                                                                                '' ,
                                                                                                '无' ,
                                                                                                '' ,
                                                                                                '' ,
                                                                                                '' ,
                                                                                                '1997-01-01' ,
                                                                                                '' ,
                                                                                                '' ,
                                                                                                '' ,
                                                                                                '' ,
                                                                                                '' ,
                                                                                                '' ,
                                                                                                '' ,
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                '',
                                                                                                ''
                                                                                    ) t
                                                                            ORDER BY t.CREATETIME ASC ,
                                                                                    t.siteid ASC;");
        if (caseList != null)
        {
            ddlLKSite.DataSource = caseList.AsEnumerable<BASE_CRANECONFIG_DETIAL>().ToList();
            ddlLKSite.DataValueField = "ID";
            ddlLKSite.DataTextField = "SITEID";
            ddlLKSite.DataBind();
        }
        else
        {
            ddlLKSite.DataSource = null;
            ddlLKSite.DataBind();
        }

        Help.DropDownListDataBind(GetAGVSite("6"), this.ddlAGVSITE, "请选择AGV站点", "SITENAME", "SITEID", "");
        Help.DropDownListDataBind(GetAGVSite("5"), this.ddlStorageSite, "请选择等待站点", "SITENAME", "SITEID", "");
    }
    #endregion
}