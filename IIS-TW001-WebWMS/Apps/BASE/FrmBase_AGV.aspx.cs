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
public partial class Apps_BASE_FrmBase_AGV : WMSBasePage
{
    #region 常量
    DBContext context = new DBContext();
    #endregion
    #region load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            
            if (this.Operation() == SYSOperation.New)
            {
                hdnID.Value = Guid.NewGuid().ToString();
                txtCreateUser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtCreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtModifyUser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtModifyTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (this.Operation() == SYSOperation.Modify)
            {
                this.ShowData(this.KeyID);
                this.txtCraneID.Enabled = false;
            }
            this.InitPage();
        }
    }
    #endregion

    #region even
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CRANECONFIG> con = new GenericRepository<BASE_CRANECONFIG>(context);
        if (this.CheckData())
        {
            BASE_CRANECONFIG entity = (BASE_CRANECONFIG)this.SendData();
            string strKeyID = "";
            try
            {
                if (this.Operation() == SYSOperation.Modify)
                {
                    strKeyID = hdnID.Value;
                    entity.ID = strKeyID;
                    con.Update(entity);
                    //检查编号是否重复
                    var cut = con.Get().Where(p => p.CRANEID == entity.CRANEID && p.ID != entity.ID && p.PLCType == "AGV");
                    if (cut.ToList().Count > 0)
                    {
                        this.Alert(Resources.Lang.FrmBase_AGV_Msg01);//您输入的AGV编号在系统中已存在！
                    }
                    else
                    {
                        con.Save();
                        this.AlertAndBack("FrmBase_AGV.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
                    }
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    strKeyID = hdnID.Value;
                    entity.ID = strKeyID;
                    con.Insert(entity);
                    //检查编号是否重复
                    var cut = con.Get().Where(p => p.CRANEID == entity.CRANEID && p.PLCType == "AGV");
                    if (cut.ToList().Count > 0)
                    {
                        this.Alert(Resources.Lang.FrmBase_AGV_Msg01);//您输入的AGV编号在系统中已存在！
                    }
                    else
                    {
                        strKeyID = hdnID.Value;
                        entity.ID = strKeyID;
                        con.Save();
                        this.AlertAndBack("FrmBase_AGV.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                    }
                }
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATEEdit_Msg17 + E.Message); //"失败！" 
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearchAGV'].click(); CloseMySelf('BASE_AGV');return false;";
      
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        Help.DropDownListDataBind(GetParametersByFlagType("BASE_CLIENT"), dplCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");//状态
    }

    protected void ShowData(string id)
    {
        IGenericRepository<BASE_CRANECONFIG> con = new GenericRepository<BASE_CRANECONFIG>(context);
        var caseList = from p in con.Get()
                       where p.ID == id
                       select p;
        BASE_CRANECONFIG entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG>();
        entity.ID = this.KeyID;
        txtCraneID.Text = entity.CRANEID;
        txtCraneNAME.Text = entity.CRANENAME;
        ddlSUPPLIER.SelectedValue = entity.SUPPLIER;
        txtTTYPE.Text = entity.TTYPE;
        dplCSTATUS.SelectedValue = entity.FLAG;
        txtReamark.Text = entity.REMARK;
        hdnID.Value = entity.ID;
        txtCreateTime.Text = Convert.ToDateTime(entity.CreateTime).ToString("yyyy-MM-dd");
        txtCreateUser.Text = entity.CREATEUSER;
        txtModifyTime.Text = Convert.ToDateTime(entity.ModifyTime).ToString("yyyy-MM-dd");
        txtModifyUser.Text = entity.MODIFYUSER;
        txtDISCONTINUETIME.Text = entity.DISCONTINUETIME != null ? Convert.ToDateTime(entity.DISCONTINUETIME).ToString("yyyy-MM-dd") : "";
        txtDISCONTINUEUSER.Text = entity.DISCONTINUEUSER;
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
            this.Alert(Resources.Lang.FrmBase_AGV_Msg02); //AGV编号项不允许空！
            this.SetFocus(txtCraneID);
            return false;
        }
        //
        if (this.txtCraneID.Text.Trim().Length > 0)
        {
            if (this.txtCraneID.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmBase_AGV_Msg03); //AGV编号项超过指定的长度30！
                this.SetFocus(txtCraneID);
                return false;
            }
        }
        //
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtCraneID.Text.Trim(), "^[A-Za-z0-9-_]+$"))
        {
            this.Alert(Resources.Lang.FrmBase_AGV_Msg04); //AGV编号项只允许输入英文字母和数字,特殊符号-_！
            this.SetFocus(txtCraneID);
            return false;
        }

        //
        if (this.txtCraneNAME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_AGV_Msg05);//AGV名称项不允许空！
            this.SetFocus(txtCraneNAME);
            return false;
        }
        //
        if (this.txtCraneNAME.Text.Trim().Length > 0)
        {
            if (this.txtCraneNAME.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmBase_AGV_Msg06); //AGV名称项超过指定的长度30！
                this.SetFocus(txtCraneNAME);
                return false;
            }
        }

        if (this.txtReamark.Text.Trim().Length > 0)
        {
            if (this.txtReamark.Text.GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmBase_AGV_Msg07);//备注超过指定的长度100！
                this.SetFocus(txtReamark);
                return false;
            }
        }
        
        if (this.txtTTYPE.Text.Trim().Length > 0)
        {
            if (this.txtTTYPE.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBase_AGV_Msg08); //型号超过指定的长度20！
                this.SetFocus(txtTTYPE);
                return false;
            }
        }
        return true;

    }

    public BASE_CRANECONFIG SendData()
    {
        BASE_CRANECONFIG entity = new BASE_CRANECONFIG();
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
        this.txtCraneNAME.Text = this.txtCraneNAME.Text.Trim();
        if (this.txtCraneNAME.Text.Length > 0)
        {
            entity.CRANENAME = txtCraneNAME.Text;
        }
        else
        {
            entity.CRANENAME = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCLIENTNAME = null;
        }
        this.txtTTYPE.Text = this.txtTTYPE.Text.Trim();
        if (this.txtTTYPE.Text.Length > 0)
        {
            entity.TTYPE = txtTTYPE.Text;
        }
        else
        {
            entity.TTYPE = string.Empty;
        }
        //备注
        if (this.txtReamark.Text.Trim().Length > 0)
        {
            entity.REMARK = txtReamark.Text.Trim();
        }
        else
        {
            entity.REMARK = string.Empty;
        }
        entity.FLAG = dplCSTATUS.SelectedValue;
        entity.PLCType = "AGV";
        entity.SUPPLIER = ddlSUPPLIER.SelectedValue;
        if (this.Operation() == SYSOperation.New)
        {
            entity.CREATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.CreateTime = DateTime.Now;
            entity.MODIFYUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.ModifyTime = DateTime.Now;
        }
        else if (this.Operation() == SYSOperation.Modify)
        {
            entity.CREATEUSER = this.txtCreateUser.Text.Trim();
            entity.CreateTime = Convert.ToDateTime(this.txtCreateTime.Text.Trim());
            entity.MODIFYUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.ModifyTime = DateTime.Now;
            if (!string.IsNullOrEmpty(txtDISCONTINUETIME.Text.Trim()))
            {
                entity.DISCONTINUETIME = Convert.ToDateTime(txtDISCONTINUETIME.Text.Trim());
                entity.DISCONTINUEUSER = txtDISCONTINUEUSER.Text.Trim();
            }
        }
        return entity;
    }
   
    #endregion
    
}