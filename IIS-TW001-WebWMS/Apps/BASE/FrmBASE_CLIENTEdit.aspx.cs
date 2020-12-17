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
using System.Text;

/// <summary>
/// 描述: 客户详情-->FrmBASE_CLIENTEdit 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-09-29 15:43:22
/// </summary>
public partial class BASE_FrmBASE_CLIENTEdit : WMSBasePage//PageBase,IPageEdit
{
    #region SQL

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData(Request.QueryString["ids"]);
            }
        }
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_CLIENT');return false;";
        //删除确认提示
        if (this.Operation() != SYSOperation.New)
        {
            this.btnDelete.Attributes["onclick"] = "var userNo = '" + Request.QueryString["ids"] + "'; return window.confirm('" + Resources.Lang.FrmBASE_CLIENTEdit_Msg01 + "' + userNo + '?');"; //要删除
        }
        else
        {
            this.btnDelete.Visible = false;
        }
        //设置保存按钮的文字及其状态
        if (Request.QueryString["Flag"] == "2")//查看
        {
            this.btnSave.Visible = false;
        }
        else if (Request.QueryString["Flag"] == "6")//审批
        {
            this.btnSave.Text = Resources.Lang.FrmALLOCATEEdit_Msg08;//审批
        }

        Help.DropDownListDataBind(GetParametersByFlagType("BASE_CLIENT"), dplCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");//状态
    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData(string id)
    {
        txtCCLIENTID.Enabled = false;

        IGenericRepository<BASE_CLIENT> con = new GenericRepository<BASE_CLIENT>(context);
        var caseList = from p in con.Get()
                       where p.id == id
                       select p;
        BASE_CLIENT entity = caseList.ToList().FirstOrDefault<BASE_CLIENT>();
        this.txtCCLIENTID.Text = entity.cclientid;
        this.txtCCLIENTNAME.Text = entity.cclientname;
        this.txtCALIAS.Text = entity.calias;
        this.txtCCONTACTPERSON.Text = entity.ccontactperson;
        this.txtCPHONE.Text = entity.cphone;
        this.txtCERPCODE.Text = entity.cerpcode;
        this.txtCADDRESS.Text = entity.caddress;
        this.txtCTYPE.Text = entity.ctype;
        this.txtILEVER.Text = entity.ilever.ToString();
        this.txtCMEMO.Text = entity.cmemo;
        //this.txtCSTATUS.Text = entity.CSTATUS;
        try
        {
            this.dplCSTATUS.SelectedIndex = int.Parse(entity.cstatus);
        }
        catch
        { }
        this.txtID.Text = entity.id;
        txtcreatetime.Text = entity.createtime != null ? Convert.ToDateTime(entity.createtime).ToString("yyyy-MM-dd HH:mm:ss") : "";
        txtcreateuser.Text = OPERATOR.GetUserNameByAccountID(entity.createowner);
        txtupdatetime.Text = entity.lastupdatetime != null ? Convert.ToDateTime(entity.lastupdatetime).ToString("yyyy-MM-dd HH:mm:ss") : "";
        txtupdateuser.Text = OPERATOR.GetUserNameByAccountID(entity.lastupdateowner);
    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_CLIENT SendData()
    {
        BASE_CLIENT entity = new BASE_CLIENT();
        //
        this.txtCCLIENTID.Text = this.txtCCLIENTID.Text.Trim();
        if (this.txtCCLIENTID.Text.Length > 0)
        {
            entity.cclientid = txtCCLIENTID.Text;
        }
        this.txtCCLIENTNAME.Text = this.txtCCLIENTNAME.Text.Trim();
        if (this.txtCCLIENTNAME.Text.Length > 0)
        {
            entity.cclientname = txtCCLIENTNAME.Text;
        }
        this.txtCALIAS.Text = this.txtCALIAS.Text.Trim();
        if (this.txtCALIAS.Text.Length > 0)
        {
            entity.calias = txtCALIAS.Text;
        }
        this.txtCCONTACTPERSON.Text = this.txtCCONTACTPERSON.Text.Trim();
        if (this.txtCCONTACTPERSON.Text.Length > 0)
        {
            entity.ccontactperson = txtCCONTACTPERSON.Text;
        }
        this.txtCPHONE.Text = this.txtCPHONE.Text.Trim();
        if (this.txtCPHONE.Text.Length > 0)
        {
            entity.cphone = txtCPHONE.Text;
        }
        this.txtCERPCODE.Text = this.txtCERPCODE.Text.Trim();
        if (this.txtCERPCODE.Text.Length > 0)
        {
            entity.cerpcode = txtCERPCODE.Text;
        }
        this.txtCADDRESS.Text = this.txtCADDRESS.Text.Trim();
        if (this.txtCADDRESS.Text.Length > 0)
        {
            entity.caddress = txtCADDRESS.Text;
        }
        this.txtCTYPE.Text = this.txtCTYPE.Text.Trim();
        if (this.txtCTYPE.Text.Length > 0)
        {
            entity.ctype = txtCTYPE.Text;
        }
        this.txtILEVER.Text = this.txtILEVER.Text.Trim();
        if (this.txtILEVER.Text.Length > 0)
        {
            entity.ilever = txtILEVER.Text.ToDecimal();
        }
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        this.dplCSTATUS.Text = this.dplCSTATUS.Text.Trim();
        if (this.dplCSTATUS.Text.Length > 0)
        {
            entity.cstatus = dplCSTATUS.SelectedValue;
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
        if (Page.IsValid)
        {
            IGenericRepository<BASE_CLIENT> con = new GenericRepository<BASE_CLIENT>(context);
            if (this.CheckData())
            {
                var entity = this.SendData();
                if (Request.QueryString["Flag"] != "0")
                {
                }
                string strKeyID = "";
                try
                {
                    if (this.Operation() == SYSOperation.Modify)
                    {
                        strKeyID = txtID.Text.Trim();
                        entity.id = strKeyID;
                        entity.createtime = Convert.ToDateTime(txtcreatetime.Text.Trim());
                        entity.createowner = OPERATOR.GetUserIDByUserName(txtcreateuser.Text.Trim());
                        entity.lastupdatetime = DateTime.Now;
                        entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                        con.Update(entity);
                    }
                    else if (this.Operation() == SYSOperation.New)
                    {
                        entity.createtime = DateTime.Now;
                        strKeyID = Guid.NewGuid().ToString();
                        entity.id = strKeyID;
                        entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                        entity.lastupdatetime = DateTime.Now;
                        entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                        con.Insert(entity);

                    }
                    this.Alert(Resources.Lang.Common_SuccessSave); //保存成功
                    con.Save();
                    //if (Request.QueryString["Flag"] == "1")
                    //    this.AlertAndBack("FrmBASE_CLIENTEdit.aspx?Flag=1&ids=" + strKeyID + BuildQueryString(SYSOperation.Modify, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
                    //else
                    //    this.AlertAndBack("FrmBASE_CLIENTEdit.aspx?Flag=0" + BuildQueryString(SYSOperation.New, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                }
                catch (Exception E)
                {
                    this.Alert(Request.QueryString["Flag"] == "0" ? Resources.Lang.Common_Add : Resources.Lang.Commona_Update + Resources.Lang.FrmALLOCATE_DEdit_Msg47 + E.Message); //this.Alert(Request.QueryString["Flag"]=="0"?"新增":"修改" + "失败！" + E.Message); 
#if Debug 
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
                }
            }
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        ASCIIEncoding strData = new ASCIIEncoding();
        //
        if (this.txtCCLIENTID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg02);//客户编码项不允许空！
            this.SetFocus(txtCCLIENTID);
            return false;
        }
        else
        {
            if (this.Operation() == SYSOperation.New)
            {
                IGenericRepository<BASE_CLIENT> con = new GenericRepository<BASE_CLIENT>(context);
                var exisBO = (from p in con.Get()
                              where p.cclientid == this.txtCCLIENTID.Text.Trim()
                              select p).FirstOrDefault();
                if (exisBO != null && exisBO.id != null && exisBO.id.Length > 0)
                {
                    this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg03);//客户编码项已存在！
                    this.SetFocus(txtCCLIENTID);
                    return false;
                }
            }

        }
        //
        if (this.txtCCLIENTID.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCCLIENTID.Text).Length > 30)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg04); //客户编码项超过指定的长度30！
                this.SetFocus(txtCCLIENTID);
                return false;
            }
        }
        //
        if (this.txtCCLIENTNAME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg05);//客户名称项不允许空！
            this.SetFocus(txtCCLIENTNAME);
            return false;
        }
        //
        if (this.txtCCLIENTNAME.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCCLIENTNAME.Text).Length > 30)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg06); //客户名称项超过指定的长度30！
                this.SetFocus(txtCCLIENTNAME);
                return false;
            }
        }
        //
        if (this.txtCALIAS.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCALIAS.Text).Length > 50)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg07); //助记码项超过指定的长度50！
                this.SetFocus(txtCALIAS);
                return false;
            }
        }
        //
        if (this.txtCCONTACTPERSON.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCCONTACTPERSON.Text).Length > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg08);//联系人项超过指定的长度20！
                this.SetFocus(txtCCONTACTPERSON);
                return false;
            }
        }
        //
        if (this.txtCPHONE.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCPHONE.Text).Length > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg09);//联系电话项超过指定的长度20！
                this.SetFocus(txtCPHONE);
                return false;
            }
        }
        //
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCERPCODE.Text).Length > 50)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg10);//ERP编码项超过指定的长度50！
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        //
        if (this.txtCADDRESS.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCADDRESS.Text).Length > 50)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg11);//联系地址项超过指定的长度50！
                this.SetFocus(txtCADDRESS);
                return false;
            }
        }
        //
        if (this.txtCTYPE.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCTYPE.Text).Length > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg12);//客户类型项超过指定的长度20！
                this.SetFocus(txtCTYPE);
                return false;
            }
        }
        //
        if (this.txtILEVER.Text.Trim().Length > 0)
        {
            decimal number;
            if (!Decimal.TryParse(this.txtILEVER.Text, out number))
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg13);//级别项不是有效的十进制数字！
                this.SetFocus(txtILEVER);
                return false;
            }
        }
        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCMEMO.Text).Length > 100)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg14);//备注项超过指定的长度100！
                this.SetFocus(txtCMEMO);
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
            if (strData.GetBytes(dplCSTATUS.Text).Length > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg15);//状态项超过指定的长度20！
                this.SetFocus(dplCSTATUS);
                return false;
            }
        }
        return true;

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //BASE_CLIENT entity = new BASE_CLIENT(); 
        //try 
        //{ 
        //    entity.ID = this.KeyID.ToString();
        //    BASE_CLIENTRule.Delete(entity); 
        //} 
        //catch (Exception E) 
        //{ 
        //    this.Alert("删除失败！" + E.Message); 
        //    #if Debug 
        //    this.Response.Write(entity.DBAccess().GetLastSQL());                
        //    #endif 
        //}

    }
    #endregion



}

