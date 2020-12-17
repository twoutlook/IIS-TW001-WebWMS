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
public partial class Apps_BASE_frmBase_RGV_PCLADDR : WMSBasePage
{
    #region 常量
    public static string CraneIDstr = "";
    public static string CraneIDS = "";
    public static string CraneName = "";
    DBContext context = new DBContext();
    #endregion
    #region load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            if (this.Operation() == SYSOperation.Modify)
            {
                ShowData();
            }
            else if (this.Operation() == SYSOperation.New)
            {
                hdnID.Value = Guid.NewGuid().ToString();
                hdnIDS.Value = this.KeyID;
                txtPLCREGION.Text = Request["PLCREGION"].ToString();
                txtcreatetime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtupdatetime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtcreateuser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtupdateuser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
            }
            this.InitPage();
        }
    }
    #endregion

    #region even
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CRANECONFIG_PLCADDR> con = new GenericRepository<BASE_CRANECONFIG_PLCADDR>(context);
        if (this.CheckData())
        {
            BASE_CRANECONFIG_PLCADDR entity = (BASE_CRANECONFIG_PLCADDR)this.SendData();
            string strKeyID = "";
            try
            {
                //检查地址是否重复
                if (this.Operation() == SYSOperation.Modify)
                {

                    strKeyID = hdnID.Value;
                    entity.ID = strKeyID;

                    con.Update(entity);
                    var plcCout = con.Get().Where(p => p.PLCADDRESS == entity.PLCADDRESS && p.ID != entity.ID);
                    if (plcCout.ToList().Count > 0)
                    {
                        this.Alert(Resources.Lang.FrmBase_RGV_D_PLCADDRESS + "[" + entity.PLCADDRESS + "]" + Resources.Lang.FrmBase_RGV_PCLADDR_Msg01);//地址[" + entity.PLCADDRESS + "]重复！
                    }
                    else
                    {
                        con.Save();
                        this.AlertAndBack("frmBase_RGV_PCLADDR.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                    }
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    var plcCout = con.Get().Where(p => p.PLCADDRESS == entity.PLCADDRESS);
                    if (plcCout.ToList().Count > 0)
                    {
                        this.Alert(Resources.Lang.FrmBase_RGV_D_PLCADDRESS + "[" + entity.PLCADDRESS + "]" + Resources.Lang.FrmBase_RGV_PCLADDR_Msg01);//地址[" + entity.PLCADDRESS + "]重复！
                    }
                    else
                    {
                        strKeyID = hdnID.Value;
                        entity.ID = strKeyID;
                        con.Insert(entity);
                        con.Save();

                        this.AlertAndBack("frmBase_RGV_PCLADDR.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
                    }

                }
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATEEdit_Msg17 + E.Message);//"失败！"
            }
        }
    }
    #endregion

    #region Function

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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_RGV_PLCADDR');return false;";
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
    }

    public void ShowData()
    {
        IGenericRepository<BASE_CRANECONFIG_PLCADDR> con = new GenericRepository<BASE_CRANECONFIG_PLCADDR>(context);
        var caseList = from p in con.Get()
                       where p.ID == this.KeyID
                       select p;
        BASE_CRANECONFIG_PLCADDR entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG_PLCADDR>();
        hdnID.Value = entity.ID;
        hdnIDS.Value = entity.IDS;
        txtPLCREGION.Text = entity.PLCREGION;
        txtPLCADDRESS.Text = entity.PLCADDRESS;
        txtMEANING.Text = entity.MEANING;
        ddlASCRIPTION.SelectedValue = entity.ASCRIPTION;
        txtcreatetime.Text = Convert.ToDateTime(entity.CREATERTIME).ToString("yyyy-MM-dd");
        txtcreateuser.Text = entity.CREATERUSER;
        txtupdatetime.Text = Convert.ToDateTime(entity.UPDATETIME).ToString("yyyy-MM-dd");
        txtupdateuser.Text = entity.UPDATEUSER;
        txtRemark.Text = entity.REMARK;
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //
        if (this.txtPLCREGION.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_RGV_PCLADDR_Msg02);//PLC区域项不允许空！
            this.SetFocus(txtPLCREGION);
            return false;
        }
        //
        if (this.txtPLCREGION.Text.Trim().Length > 0)
        {
            if (this.txtPLCREGION.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmBase_RGV_PCLADDR_Msg03);//PLC区域项超过指定的长度50！
                this.SetFocus(txtPLCREGION);
                return false;
            }
        }
        //
        if (this.txtPLCADDRESS.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_RGV_PCLADDR_Msg04);//PLC地址项不允许空！
            this.SetFocus(txtPLCADDRESS);
            return false;
        }
        //
        if (this.txtPLCADDRESS.Text.Trim().Length > 0)
        {
            if (this.txtPLCADDRESS.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmBase_RGV_PCLADDR_Msg05); //PLC地址项超过指定的长度50！
                this.SetFocus(txtPLCADDRESS);
                return false;
            }
        }
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtPLCADDRESS.Text.Trim(), "^[A-Za-z0-9-_]+$"))
        {
            this.Alert(Resources.Lang.FrmBase_RGV_PCLADDR_Msg06);//PLC地址项只允许输入英文字母和数字,特殊符号-_！
            this.SetFocus(txtPLCADDRESS);
            return false;
        }
        //
        if (this.txtMEANING.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_RGV_PCLADDR_Msg07); //定义项不允许空！
            this.SetFocus(txtMEANING);
            return false;
        }
        if (this.txtMEANING.Text.Trim().Length > 0)
        {
            if (this.txtMEANING.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmBase_RGV_PCLADDR_Msg08);//定义超过指定的长度50！
                this.SetFocus(txtMEANING);
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_CRANECONFIG_PLCADDR SendData()
    {
        BASE_CRANECONFIG_PLCADDR entity = new BASE_CRANECONFIG_PLCADDR();
        //
        this.txtPLCREGION.Text = this.txtPLCREGION.Text.Trim();
        if (this.txtPLCREGION.Text.Length > 0)
        {
            entity.PLCREGION= txtPLCREGION.Text;
        }
        else
        {
            entity.PLCREGION = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCLIENTID = null;
        }
        //
        this.txtPLCADDRESS.Text = this.txtPLCADDRESS.Text.Trim();
        if (this.txtPLCADDRESS.Text.Length > 0)
        {
            entity.PLCADDRESS = txtPLCADDRESS.Text;
        }
        else
        {
            entity.PLCADDRESS = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCLIENTNAME = null;
        }
        //
        this.txtMEANING.Text = this.txtMEANING.Text.Trim();
        if (this.txtMEANING.Text.Length > 0)
        {
            entity.MEANING = txtMEANING.Text;
        }
        else
        {
            entity.MEANING = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CALIAS = null;
        }
        //
        entity.ASCRIPTION = ddlASCRIPTION.SelectedValue;
        entity.REMARK = txtRemark.Text.Trim();
        if (this.Operation() == SYSOperation.New)
        {
            entity.CREATERUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.CREATERTIME = DateTime.Now;
            entity.UPDATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.UPDATETIME = DateTime.Now;
        }
        else if (this.Operation() == SYSOperation.Modify)
        {
            entity.CREATERUSER = this.txtcreateuser.Text.Trim();
            entity.CREATERTIME = Convert.ToDateTime(this.txtcreatetime.Text.Trim());
            entity.UPDATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.UPDATETIME = DateTime.Now;
        }
        entity.IDS = hdnIDS.Value;
        return entity;

    }
    #endregion

}