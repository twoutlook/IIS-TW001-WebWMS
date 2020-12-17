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
public partial class Apps_BASE_FrmBase_AGV_D : WMSBasePage
{
    #region 常量
    DBContext context = new DBContext();

    
    #endregion

    #region load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() == SYSOperation.New)
            {
                //hdnIDS.Value = this.KeyID;
                hdnID.Value = Guid.NewGuid().ToString();
                txtcreateuser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtcreatetime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtupdateuser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtupdatetime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //txtCraneID.Text = GetCraneID(hdnIDS.Value);
            }
            if (this.Operation() == SYSOperation.Modify)
            {
                hdnID.Value = this.KeyID;
                this.ShowData();
                this.txtSiteID.Enabled = false;
                if (Request["Index"]!=null)
                    Session["Index6"] = Request["Index"].ToString();
            }
            
        }
    }
    #endregion

    #region even
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        if (this.CheckData())
        {
            BASE_CRANECONFIG_DETIAL entity = (BASE_CRANECONFIG_DETIAL)this.SendData();
            string strKeyID = "";
            try
            {
                //检查绑定立库站点是否有重复,PLC区域是否有重复
                if (this.Operation() == SYSOperation.Modify)
                {
                    strKeyID = hdnID.Value;
                    entity.ID = strKeyID;
                    con.Update(entity);
                    con.Save();
                    this.AlertAndBack("FrmBase_AGV_D.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    strKeyID = hdnID.Value;
                    entity.ID = strKeyID;
                    con.Insert(entity);
                    con.Save();
                    this.AlertAndBack("FrmBase_AGV_D.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
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
        this.btnBack.Attributes["onclick"] = @"window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearchAGVS'].click(); CloseMySelf('BASE_AGV_D');return false;";
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        Help.DropDownListDataBind(GetParametersByFlagType("SITETYPE"), this.ddlAGVSITETYPE, "", "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(GetParametersByFlagType("BASE_CLIENT"), dplCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(GetParametersByFlagType("CARGOSPACETYPE"), this.ddlFormat, "", "FLAG_NAME", "FLAG_ID", ""); //规格
    }


    protected void ShowData()
    {
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        var caseList = from p in con.Get()
                       where p.ID == this.KeyID
                       select p;
        BASE_CRANECONFIG_DETIAL entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG_DETIAL>();
        txtSiteID.Text = entity.SITEID;
        txtSiteName.Text = entity.SITENAME;
        ddlAGVSITETYPE.SelectedValue = entity.AGVSITETYPE;
        rdSTOREYLIMIT.SelectedValue = entity.STOREYLIMIT.Equals("Y") ? Resources.Lang.FrmALLOCATE_DEdit_SN_Msg03 : Resources.Lang.FrmALLOCATE_DEdit_SN_Msg04;//"是""否";;
        ddlFormat.SelectedValue = entity.FORMAT;
        dplCSTATUS.SelectedValue = entity.FLAG;
        txtSTOREY.Text = entity.STOREY;
        txtcreateuser.Text = entity.CREATEUSER;
        txtcreatetime.Text = Convert.ToDateTime(entity.CREATETIME).ToString("yyyy-MM-dd");
        txtupdateuser.Text = entity.UPDATEUSER;
        txtupdatetime.Text = Convert.ToDateTime(entity.UPDATETIME).ToString("yyyy-MM-dd");
        txtRemark.Text = entity.REMARK;
        txtDISCONTINUETIME.Text = entity.DISCONTINUETIME != null ? Convert.ToDateTime(entity.DISCONTINUETIME).ToString("yyyy-MM-dd") : "";
        txtDISCONTINUEUSER.Text = entity.DISCONTINUEUSER;
        if (!string.IsNullOrEmpty(entity.IS_OCCUPY))
        {
            hdn_is_occupy.Value = entity.IS_OCCUPY;
        }
        else
        {           
            hdn_is_occupy.Value = "0";
        }
        //hdnID.Value = entity.ID;
        //hdnIDS.Value = entity.IDS;
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
                throw new Exception(Resources.Lang.FrmBase_AGV_D_Msg01 + "Querystring:QID", innerException); //未传递
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
        if (this.txtSiteID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_AGV_D_Msg02); //站点编号项不允许空！
            this.SetFocus(txtSiteID);
            return false;
        }
        //
        if (this.txtSiteID.Text.Trim().Length > 0)
        {
            if (this.txtSiteID.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmBase_AGV_D_Msg03); //站点编号项超过指定的长度30！
                this.SetFocus(txtSiteID);
                return false;
            }
        }
        //站点编号只能是英文数字
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtSiteID.Text.Trim(), "^[A-Za-z0-9-_]+$"))
        {
            this.Alert(Resources.Lang.FrmBase_AGV_D_Msg04); //站点编号项只允许输入英文字母和数字,特殊符号-_！
            this.SetFocus(txtSiteID);
            return false;
        }
        //
        if (this.txtSiteName.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_AGV_D_Msg05); //站点名称项不允许空！
            this.SetFocus(txtSiteName);
            return false;
        }
        if (this.txtSiteName.Text.Trim().Length > 0)
        {
            if (this.txtSiteName.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmBase_AGV_D_Msg06); //站点名称超过指定的长度30！
                this.SetFocus(txtSiteName);
                return false;
            }
        }
        //楼层只能是数字
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtSTOREY.Text.Trim(), "^[0-9]+$"))
        {
            this.Alert(Resources.Lang.FrmBase_AGV_D_Msg07); //楼层项只允许输入数字！
            this.SetFocus(txtSTOREY);
            return false;
        }
        //站点编号是否重复
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        var count = con.Get().Where(p => p.SITEID == this.txtSiteID.Text.Trim());
        if (this.Operation() == SYSOperation.New && count.ToList().Count > 0)
        {
            this.Alert(Resources.Lang.FrmBase_AGV_D_Msg08); //站点编号已存在！
            this.SetFocus(txtSiteID);
            return false;
        }
        return true;
    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_CRANECONFIG_DETIAL SendData()
    {
        BASE_CRANECONFIG_DETIAL entity = new BASE_CRANECONFIG_DETIAL();
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
        entity.STOREY = txtSTOREY.Text.Trim();
        entity.AGVSITETYPE = ddlAGVSITETYPE.SelectedValue;
        entity.STOREYLIMIT = rdSTOREYLIMIT.SelectedValue.Equals(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg03) ? "Y" : "N";//"是"
        entity.FORMAT = ddlFormat.SelectedValue;
        entity.FLAG = dplCSTATUS.SelectedValue;
        //备注
        if (this.txtRemark.Text.Trim().Length > 0)
        {
            entity.REMARK = txtRemark.Text.Trim();
        }
        else
        {
            entity.REMARK = string.Empty;
        }
        if (this.Operation() == SYSOperation.New)
        {
            entity.CREATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.CREATETIME = DateTime.Now;
            entity.UPDATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.UPDATETIME = DateTime.Now;
            entity.IS_OCCUPY = "0";  //0:未占用  1：占用
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
            entity.IS_OCCUPY = hdn_is_occupy.Value;
        }
        entity.IS_DEFAULT_IN = "0";
        return entity;

    }

    
    #endregion
}