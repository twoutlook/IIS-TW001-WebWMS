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
/// 描述: 仓库详情-->FrmWAREHOUSEEdit 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-09-29 14:26:08
/// </summary>
public partial class BASE_FrmWAREHOUSEEdit : WMSBasePage
{
    #region SQL
    protected void Page_Load(object sender, EventArgs e)
    {
        ucShowVENDORDiv.SetORGCode = txtLEADERCODE.ClientID;
        ucShowVENDORDiv.SetCompName = txtLEADER.ClientID;
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData();
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('WAREHOUSE');return false;";
        this.txtLEADERCODE.Attributes["onclick"] = "Show('" + ucShowVENDORDiv.GetDivName + "');";

        Help.DropDownListDataBind(GetParametersByFlagType("WareHouseType"), ddlWareHouseType, "", "FLAG_NAME", "FLAG_ID", "");//仓库类型
        Help.DropDownListDataBind(GetParametersByFlagType("BASE_WAREHOUSE.CSTATUS"), ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");//状态
        //删除确认提示
        if (this.Operation() == SYSOperation.Modify)
        {
            //this.btnDelete.Attributes["onclick"] = "var userNo = '" + this.KeyID + "'; return window.confirm('要删除' + userNo + '?');";
        }
        else
        {
            //this.btnDelete.Visible = false;
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
    public void ShowData()
    {
        txtCWAREID.Enabled = false;

        IGenericRepository<BASE_WAREHOUSE> con = new GenericRepository<BASE_WAREHOUSE>(context);
        var caseList = from p in con.Get()
                       where p.id == this.KeyID
                       select p;
        BASE_WAREHOUSE entity = caseList.ToList().FirstOrDefault();
        this.txtCWAREID.Text = entity.cwareid;
        this.txtCWARENAME.Text = entity.cwarename;
        this.txtLEADERPHONE.Text = entity.leaderphone;
        this.txtLEADERCODE.Text = entity.leadercode;
        this.txtLEADER.Text = entity.leader;
        this.txtCMEMO.Text = entity.cmemo;
        this.ddlCSTATUS.SelectedValue = entity.cstatus;
        this.txtID.Text = entity.id;
        this.cbISBONDED.Checked = entity.bonded == "Y" ? true : false;
        this.cbCDEFINE1.Checked = entity.cdefine1 == "Y" ? true : false;
        this.ddlWareHouseType.SelectedValue = entity.cdefine2;
        //this.txtCDEFINE2.Text = entity.cdefine2;
        txtcreatetime.Text = entity.createtime.ToString();
        txtcreateuser.Text = OPERATOR.GetUserNameByAccountID(entity.createowner);
        txtupdatetime.Text = entity.lastupdatetime.ToString();
        txtupdateuser.Text = OPERATOR.GetUserNameByAccountID(entity.lastupdateowner);
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        ASCIIEncoding strData = new ASCIIEncoding();

        if (this.txtCWAREID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmWAREHOUSEEdit_Msg01);//编码项不允许空！
            this.SetFocus(txtCWAREID);
            return false;
        }

        if (this.txtCWAREID.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCWAREID.Text).Length > 30)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg04);//编码项超过指定的长度30！
                this.SetFocus(txtCWAREID);
                return false;
            }
        }
        if (this.txtCWAREID.Text.Trim() != "" && this.Operation() == SYSOperation.New)
        {
            IGenericRepository<BASE_WAREHOUSE> con = new GenericRepository<BASE_WAREHOUSE>(context);
            var exisBO = (from p in con.Get()
                          where p.cwareid == this.txtCWAREID.Text.Trim()
                          select p).FirstOrDefault();
            if (exisBO != null && exisBO.id != null && exisBO.id.Length > 0)
            {
                this.Alert(Resources.Lang.FrmWAREHOUSEEdit_Msg02);//编码项已存在！
                this.SetFocus(txtCWAREID);
                return false;
            }
        }

        //
        if (this.txtCWARENAME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg03);//名称项不允许空！
            this.SetFocus(txtCWARENAME);
            return false;
        }
        //
        if (this.txtCWARENAME.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCWARENAME.Text).Length > 20)
            {
                this.Alert(Resources.Lang.FrmWAREHOUSEEdit_Msg03);//名称项超过指定的长度20！
                this.SetFocus(txtCWARENAME);
                return false;
            }
        }
        //
        if (this.txtLEADERPHONE.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtLEADERPHONE.Text).Length > 50)
            {
                this.Alert(Resources.Lang.FrmWAREHOUSEEdit_Msg04);//电话项超过指定的长度50！
                this.SetFocus(txtLEADERPHONE);
                return false;
            }
        }
        //
        if (this.txtLEADERCODE.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtLEADERCODE.Text).Length > 50)
            {
                this.Alert("供应商编码项超过指定的长度50！");
                this.SetFocus(txtLEADERCODE);
                return false;
            }
        }
        //
        if (this.txtLEADER.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtLEADER.Text).Length > 50)
            {
                this.Alert("供应商名称项超过指定的长度50！");
                this.SetFocus(txtLEADER);
                return false;
            }
        }
        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCMEMO.Text).Length > 200)
            {
                this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg15); //备注项超过指定的长度200！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_WAREHOUSE SendData()
    {
        BASE_WAREHOUSE entity = new BASE_WAREHOUSE();
        IGenericRepository<BASE_WAREHOUSE> con = new GenericRepository<BASE_WAREHOUSE>(context);
        if (!string.IsNullOrEmpty(this.KeyID))
        {
            var caseList = from p in con.Get()
                           where p.id == this.KeyID
                           select p;
            entity = caseList.ToList().FirstOrDefault();
        }
        this.txtCWAREID.Text = this.txtCWAREID.Text.Trim();
        if (this.txtCWAREID.Text.Length > 0)
        {
            entity.cwareid = txtCWAREID.Text;
        }
        //
        this.txtCWARENAME.Text = this.txtCWARENAME.Text.Trim();
        if (this.txtCWARENAME.Text.Length > 0)
        {
            entity.cwarename = txtCWARENAME.Text;
        }
        //
        this.txtLEADERPHONE.Text = this.txtLEADERPHONE.Text.Trim();
        if (this.txtLEADERPHONE.Text.Length > 0)
        {
            entity.leaderphone = txtLEADERPHONE.Text;
        }
        //
        this.txtLEADERCODE.Text = this.txtLEADERCODE.Text.Trim();
        if (this.txtLEADERCODE.Text.Length > 0)
        {
            entity.leadercode = txtLEADERCODE.Text;
        }
        //
        this.txtLEADER.Text = this.txtLEADER.Text.Trim();
        if (this.txtLEADER.Text.Length > 0)
        {
            entity.leader = txtLEADER.Text;
        }
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        if (!string.IsNullOrEmpty(ddlWareHouseType.SelectedValue))
        {
            entity.cdefine2 = ddlWareHouseType.SelectedValue;
        }
        //
        entity.cstatus = ddlCSTATUS.SelectedValue;

        if (cbISBONDED.Checked)
        {
            entity.bonded = "Y";
        }
        else
        {
            entity.bonded = "N";
        }

        if (cbCDEFINE1.Checked)
        {
            entity.cdefine1 = "Y";
        }
        else
        {
            entity.cdefine1 = "N";
        }
        return entity;
    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_WAREHOUSE> con = new GenericRepository<BASE_WAREHOUSE>(context);
        if (this.CheckData())
        {
            BASE_WAREHOUSE entity = SendData();
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";
            try
            {
                if (this.Operation() == SYSOperation.Modify)
                {
                    strKeyID = txtID.Text.Trim();
                    entity.id = strKeyID;
                    entity.lastupdatetime = DateTime.Now;
                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Update(entity);
                    con.Save();
                    this.AlertAndBack("FrmWAREHOUSEEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
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
                    con.Save();
                    this.AlertAndBack("FrmWAREHOUSEEdit.aspx?" + BuildQueryString(SYSOperation.New, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                }
                //context.SaveChanges();
                //this.AlertAndBack("FrmWAREHOUSEEdit.aspx?Flag=1" + BuildQueryString(SYSOperation.Modify, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
            }
            catch (Exception E)
            {
                this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg17 + E.Message);//"失败！"
#if Debug 
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
            }
        }

    }
    #endregion
  
}

