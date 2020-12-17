using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;

/// <summary>
/// 描述: 供应商管理-->FrmBASE_VENDOREdit 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-10-08 14:23:22
/// </summary>
public partial class BASE_FrmBASE_VENDOREdit :WMSBasePage// PageBase,IPageEdit
{

    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData(this.KeyID);
            }
        }
        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_VENDOR');return false;";
        //删除确认提示
        if (this.Operation() == SYSOperation.Modify)
        {
            this.btnDelete.Attributes["onclick"] = "var userNo = '" + this.KeyID + "'; return window.confirm('" + Resources.Lang.FrmBASE_CLIENTEdit_Msg01 + "' + userNo + '?');";//要删除
        }
        else
        {
            this.btnDelete.Visible = false;
        }
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.Operation() == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.FrmALLOCATEEdit_Msg08;//审批;
        }

        #region Disable and ReadOnly
        #endregion Disable and ReadOnly

    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData(string id)
    {

        IGenericRepository<BASE_VENDOR> con = new GenericRepository<BASE_VENDOR>(context);
        var caseList = from p in con.Get()
                       where p.ids == id
                       select p;
        BASE_VENDOR entity = caseList.ToList().FirstOrDefault<BASE_VENDOR>();
        entity.ids = this.KeyID;
        this.txtCVENDORID.Text = entity.cvendorid;
        this.txtCVENDOR.Text = entity.cvendor;
        this.txtCALIAS.Text = entity.calias;
        this.txtCERPCODE.Text = entity.cerpcode;
        this.txtCCONTACTPERSON.Text = entity.ccontactperson;
        this.txtCPHONE.Text = entity.cphone;
        this.txtCTNPE.Text = entity.ctnpe;
        this.txtCADDRESS.Text = entity.caddress;
        this.txtILEVEL.Text = entity.ilevel.ToString();
        this.txtCMEMO.Text = entity.cmemo;
        this.cboCSTATUS.Checked = entity.cstatus == "0" ? true : false;
        this.txtIDS.Text = entity.ids;

        txtcreatetime.Text = entity.createtime != null ? Convert.ToDateTime(entity.createtime).ToString("yyyy-MM-dd HH:mm:ss") : "";
        txtcreateuser.Text = OPERATOR.GetUserNameByAccountID(entity.createowner);
        txtupdatetime.Text = entity.lastupdatetime != null ?Convert.ToDateTime(entity.lastupdatetime).ToString("yyyy-MM-dd HH:mm:ss") : "";
        txtupdateuser.Text = OPERATOR.GetUserNameByAccountID(entity.lastupdateowner);

    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //
        if (this.txtCVENDORID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBASE_VENDOREdit_Msg01);//供应商编码项不允许空！
            this.SetFocus(txtCVENDORID);
            return false;
        }
        //
        if (this.txtCVENDORID.Text.Trim().Length > 0)
        {
            if (this.txtCVENDORID.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_VENDOREdit_Msg02); //供应商编码项超过指定的长度20！
                this.SetFocus(txtCVENDORID);
                return false;
            }
        }

        if (this.txtCVENDORID.Text.Trim() != "" && this.Operation() == SYSOperation.New)
        {
            IGenericRepository<BASE_VENDOR> con = new GenericRepository<BASE_VENDOR>(context);
            var exisBO = (from p in con.Get()
                          where p.cvendorid == this.txtCVENDORID.Text.Trim()
                          select p).FirstOrDefault();
            if (exisBO != null && exisBO.ids != null && exisBO.ids.Length > 0)
            {
                this.Alert(Resources.Lang.FrmBASE_VENDOREdit_Msg03); //供应商编码项已存在！
                this.SetFocus(txtCVENDORID);
                return false;
            }
        }

        //
        if (this.txtCVENDOR.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBASE_VENDOREdit_Msg04);//供应商名称项不允许空！
            this.SetFocus(txtCVENDOR);
            return false;
        }
        //
        if (this.txtCVENDOR.Text.Trim().Length > 0)
        {
            if (this.txtCVENDOR.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmBASE_VENDOREdit_Msg05); //供应商名称项超过指定的长度25！
                this.SetFocus(txtCVENDOR);
                return false;
            }
        }
        //
        if (this.txtCALIAS.Text.Trim().Length > 0)
        {
            if (this.txtCALIAS.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg07);//助记码项超过指定的长度50！
                this.SetFocus(txtCALIAS);
                return false;
            }
        }
        //
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
            if (this.txtCERPCODE.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg10);//ERP编码项超过指定的长度50！
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        //
        if (this.txtCCONTACTPERSON.Text.Trim().Length > 0)
        {
            if (this.txtCCONTACTPERSON.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg08);//联系人项超过指定的长度20！
                this.SetFocus(txtCCONTACTPERSON);
                return false;
            }
        }
        //
        if (this.txtCPHONE.Text.Trim().Length > 0)
        {
            if (this.txtCPHONE.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg09);//电话项超过指定的长度20！
                this.SetFocus(txtCPHONE);
                return false;
            }
        }
        //
        if (this.txtCTNPE.Text.Trim().Length > 0)
        {
            if (this.txtCTNPE.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_VENDOREdit_Msg06);//供应商类型项超过指定的长度20！
                this.SetFocus(txtCTNPE);
                return false;
            }
        }
        //
        if (this.txtCADDRESS.Text.Trim().Length > 0)
        {
            if (this.txtCADDRESS.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg11);//联系地址项超过指定的长度50！
                this.SetFocus(txtCADDRESS);
                return false;
            }
        }
        //
        if (this.txtILEVEL.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDecimal(this.txtILEVEL.Text) == false)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg13);//级别项不是有效的十进制数字！
                this.SetFocus(txtILEVEL);
                return false;
            }
        }
        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg14);//备注项超过指定的长度100！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        //
        //if(this.txtCSTATUS.Text.Trim() == "")
        //{
        //    this.Alert("状态项不允许空！");
        //    this.SetFocus(txtCSTATUS);
        //    return false;
        //}
        ////
        //if(this.txtCSTATUS.Text.Trim().Length > 0)
        //{
        //    if(this.txtCSTATUS.Text.GetLengthByByte() > 20)
        //    {
        //        this.Alert("状态项超过指定的长度20！");
        //        this.SetFocus(txtCSTATUS);
        //        return false;
        //    }
        //}
        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_VENDOR SendData()
    {
        BASE_VENDOR entity = new BASE_VENDOR();

        //
        this.txtCVENDORID.Text = this.txtCVENDORID.Text.Trim();
        if (this.txtCVENDORID.Text.Length > 0)
        {
            entity.cvendorid = txtCVENDORID.Text;
        }
        else
        {
            entity.cvendorid = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CVENDORID = null;
        }
        //
        this.txtCVENDOR.Text = this.txtCVENDOR.Text.Trim();
        if (this.txtCVENDOR.Text.Length > 0)
        {
            entity.cvendor = txtCVENDOR.Text;
        }
        else
        {
            entity.cvendor = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CVENDOR = null;
        }
        //
        this.txtCALIAS.Text = this.txtCALIAS.Text.Trim();
        if (this.txtCALIAS.Text.Length > 0)
        {
            entity.calias = txtCALIAS.Text;
        }
        else
        {
            entity.calias = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CALIAS = null;
        }
        //
        this.txtCERPCODE.Text = this.txtCERPCODE.Text.Trim();
        if (this.txtCERPCODE.Text.Length > 0)
        {
            entity.cerpcode = txtCERPCODE.Text;
        }
        else
        {
            entity.cerpcode=string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CERPCODE = null;
        }
        //
        this.txtCCONTACTPERSON.Text = this.txtCCONTACTPERSON.Text.Trim();
        if (this.txtCCONTACTPERSON.Text.Length > 0)
        {
            entity.ccontactperson = txtCCONTACTPERSON.Text;
        }
        else
        {
            entity.ccontactperson=string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCONTACTPERSON = null;
        }
        //
        this.txtCPHONE.Text = this.txtCPHONE.Text.Trim();
        if (this.txtCPHONE.Text.Length > 0)
        {
            entity.cphone = txtCPHONE.Text;
        }
        else
        {
            entity.cphone=string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CPHONE = null;
        }
        //
        this.txtCTNPE.Text = this.txtCTNPE.Text.Trim();
        if (this.txtCTNPE.Text.Length > 0)
        {
            entity.ctnpe = txtCTNPE.Text;
        }
        else
        {
            entity.ctnpe=string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CTNPE = null;
        }
        //
        this.txtCADDRESS.Text = this.txtCADDRESS.Text.Trim();
        if (this.txtCADDRESS.Text.Length > 0)
        {
            entity.caddress = txtCADDRESS.Text;
        }
        else
        {
            entity.caddress=string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CADDRESS = null;
        }
        //
        this.txtILEVEL.Text = this.txtILEVEL.Text.Trim();
        if (this.txtILEVEL.Text.Length > 0)
        {
            entity.ilevel = txtILEVEL.Text.ToDecimal();
        }
        else
        {
            //entity.ilevel=string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.ILEVEL = null;
        }
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        else
        {
            entity.cmemo=string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CMEMO = null;
        }
        entity.cstatus = cboCSTATUS.Checked ? "0" : "1";
        //
        //this.txtCSTATUS.Text = this.txtCSTATUS.Text.Trim();
        //if(this.txtCSTATUS.Text.Length > 0)
        //{
        //    entity.CSTATUS = txtCSTATUS.Text;
        //}
        //else
        //{
        //    entity.SetDBNull("CSTATUS",true);
        //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        //    //entity.CSTATUS = null;
        //}
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
        if (Page.IsValid)
        {
        IGenericRepository<BASE_VENDOR> con = new GenericRepository<BASE_VENDOR>(context);
        if (this.CheckData())
        {
            BASE_VENDOR entity = this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
              
            }
            string strKeyID = "";

            try
            {
                if (this.Operation() == SYSOperation.Modify)
                {
                    strKeyID = this.KeyID;
                    entity.ids = strKeyID;
                    entity.createtime = Convert.ToDateTime(txtcreatetime.Text.Trim());
                    entity.createowner =  OPERATOR.GetUserIDByUserName(txtcreateuser.Text.Trim());
                    entity.lastupdatetime =DateTime.Now;
                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Update(entity);
                    con.Save();
                    this.AlertAndBack("FrmBASE_VENDOREdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
                }
                /*
                else if(this.Operation == SysOperation.Apply)
                {
                    BASE_VENDORRule.Apply(entity);
                    this.AlertAndBack("FrmBASE_VENDOREdit.aspx?" + BuildQueryString(SysOperation.View, strKeyID),"申报成功"); 
                }
                else if(this.Operation == SysOperation.Audit)
                {
                    BASE_VENDORRule.Audit(entity);
                    this.AlertAndBack("FrmBASE_VENDOREdit.aspx?" + BuildQueryString(SysOperation.View, strKeyID),"审批成功"); 
                }
                */
                else if (this.Operation() == SYSOperation.New)
                {
                    entity.createtime = DateTime.Now;
                    strKeyID = Guid.NewGuid().ToString();
                    entity.ids = strKeyID;
                    entity.createtime =DateTime.Now;
                    entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.lastupdatetime = DateTime.Now;
                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Insert(entity);
                    con.Save();
                    this.AlertAndBack("FrmBASE_VENDOREdit.aspx?" + BuildQueryString(SYSOperation.New, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                }
            }
            catch (Exception E)
            {
                // this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATEEdit_Msg17 + E.Message); //"失败！" 
#if Debug 
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
            }
        }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        BASE_VENDOR entity = new BASE_VENDOR();
        IGenericRepository<BASE_VENDOR> con = new GenericRepository<BASE_VENDOR>(context);
        try
        {
            entity.ids = this.KeyID.ToString();
            con.Delete(entity.ids);
            con.Save();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_DelFail + E.Message); //删除失败！
#if Debug 
            this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
        }

    }
    #endregion  
}

