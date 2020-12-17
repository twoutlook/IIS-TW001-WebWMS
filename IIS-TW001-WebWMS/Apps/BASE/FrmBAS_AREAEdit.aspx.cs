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
/// 描述: 111-->FrmBAS_AREAEdit 页面后台类文件
/// 作者: --wujingwei
/// 创建于: 2012-11-22 20:55:46
/// </summary>
public partial class FrmBAS_AREAEdit : WMSBasePage // PageBase, IPageEdit
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowBASE_CARGOSPACEDiv_T.SetCompName = txtcpoName.ClientID; //显示字段

        ShowBASE_CARGOSPACEDiv_T.SetORGCode = txtCPOSITIONCODE.ClientID; //隐藏ＩＤ

        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation()!= SYSOperation.New)
            {
                ShowData(this.KeyID);
            }
            else
            {
                txtCreateOwner.Text = OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
                txtCreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BAR');return false;";

        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INBILL_D');return false;";
        txtCPOSITIONCODE.Attributes["onclick"] = "ShowCARGOSPACE_BAS_FrmBAS_AREAEdit('" + ShowBASE_CARGOSPACEDiv_T.GetDivName + "');";
        
        //设置保存按钮的文字及其状态
        // this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + PageBase.BuildRequestPageURL("FrmBAR_DEdit.aspx", SysOperation.New, "") + "','新建','BAR_D');return false;";

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
        txtAreaName.Enabled = false;

        IGenericRepository<BASE_AREA> con = new GenericRepository<BASE_AREA>(context);
        var caseList = from p in con.Get()
                       where p.id == id
                       select p;
        BASE_AREA entity = caseList.ToList().FirstOrDefault<BASE_AREA>();
        this.txtId.Text = entity.id;
        txtAreaName.Text = entity.area_name;
        try
        {
            if (entity.flag.ToString() == "0")
                cbCF.Checked = true;
          
        }
        catch (System.Exception e)
        { 
        }

        if (!string.IsNullOrEmpty(entity.is_temporary_area) && entity.is_temporary_area == "1") {
            chkIsTemporaryArea.Checked = true;
        }

        this.txtCreateOwner.Text = OPERATOR.GetUserNameByAccountID(entity.createowner);
        this.txtCPOSITIONCODE.Text = entity.handover_cargo;
        this.txtcpoName.Text = entity.handover_cargo_name;
        txtCMEMO.Text = entity.memo;
        ddpControl.SelectedValue = entity.is_control.ToString();
        txtupdatetime.Text = entity.lastvpdtime != null ? Convert.ToDateTime(entity.lastvpdtime).ToString("yyyy-MM-dd HH:mm:ss") : "";
        txtupdateuser.Text = OPERATOR.GetUserNameByAccountID(entity.lastvpdowner);
        txtCreateTime.Text = entity.createtime != null ? Convert.ToDateTime(entity.createtime).ToString("yyyy-MM-dd HH:mm:ss") : "";
    }



    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {

        if (this.txtAreaName.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBAS_AREAEdit_Msg01);//区域名称项不允许空！
            this.SetFocus(txtAreaName);
            return false;
        }

        if (this.txtAreaName.Text.Trim() != "" && this.Operation() == SYSOperation.New)
        {
            IGenericRepository<BASE_AREA> con = new GenericRepository<BASE_AREA>(context);
            var exisBO = (from p in con.Get()
                          where p.area_name == this.txtAreaName.Text.Trim()
                          select p).FirstOrDefault();
            if (exisBO != null && exisBO.id != null && exisBO.id.Length > 0)
            {
                this.Alert(Resources.Lang.FrmBAS_AREAEdit_Msg02);//"区域名称项已存在！"
                this.SetFocus(txtAreaName);
                return false;
            }
        }

        //区域名称
        if (this.txtAreaName.Text.Trim().Length > 0)
        {
            if (this.txtAreaName.Text.GetLengthByByte() > 40)
            {
                this.Alert(Resources.Lang.FrmBAS_AREAEdit_Msg03);//区域名称项超过指定的长度40！
                this.SetFocus(txtAreaName);
                return false;
            }
        }



        if (this.txtCPOSITIONCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBAS_AREAEdit_Msg04);//"备料储位编码项不允许空！"
            this.SetFocus(txtCPOSITIONCODE);
            return false;
        }


        //
        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_AREA SendData()
    {
        BASE_AREA entity = new BASE_AREA();
        entity.id = Guid.NewGuid().ToString();
        //
        this.txtAreaName.Text = this.txtAreaName.Text.Trim();
        if (this.txtAreaName.Text.Length > 0)
        {
            entity.area_name = txtAreaName.Text;
        }
        else
        {
            entity.area_name = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCREATEOWNERCODE = null;
        }


        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.memo = txtCMEMO.Text;
        }
        else
        {
            entity.memo = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CMEMO = null;
        }


        this.txtCPOSITIONCODE.Text = this.txtCPOSITIONCODE.Text.Trim();
        this.txtcpoName.Text = this.txtcpoName.Text.Trim();
        if (this.txtCPOSITIONCODE.Text.Length > 0)
        {
            entity.handover_cargo = txtCPOSITIONCODE.Text;
            entity.handover_cargo_name = txtcpoName.Text;
        }
        else
        {

        }
        entity.flag = cbCF.Checked ? "0" : "1";
        entity.is_control =ddpControl.SelectedValue;
        entity.is_temporary_area = chkIsTemporaryArea.Checked ? "1" : "0";
        //entity.CREATETIME = DateTime.Now;
        //entity.CREATEOWNER = WebUserInfo.GetCurrentUser().UserNo;
        //entity.LASTVPDOWNER = WebUserInfo.GetCurrentUser().UserNo;

        return entity;

    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_AREA> con = new GenericRepository<BASE_AREA>(context);
        if (this.CheckData()) 
        {
            var entity = this.SendData(); 
            if (this.Operation() != SYSOperation.New)
            {
                entity.id = this.KeyIDS[0];
            }
            string strKeyID = "";
            strKeyID += entity.id;
            try
            {
                if (this.Operation() == SYSOperation.Modify || this.Operation() == SYSOperation.Preserved1)
                {
                    entity.lastvpdtime = DateTime.Now;
                    entity.lastvpdowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Update(entity);
                    con.Save();

                   
                    this.AlertAndBack("FrmBAS_AREAEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                }

                else if (this.Operation() == SYSOperation.New)
                {
                    entity.createtime = DateTime.Now;
                    entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;

                    entity.lastvpdtime =DateTime.Now;
                    entity.lastvpdowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Insert(entity);
                    con.Save();


                    strKeyID = "";
                    strKeyID += entity.id;
                    this.AlertAndBack("FrmBAS_AREAEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                }
                //区域设置备料储位后，需要把储位状态变成不可用
                string sql = "update BASE_CARGOSPACE set cstatus='4' where cpositioncode='" + entity.handover_cargo + "'; ";
                DBHelp.ExcuteScalarSQL(sql);
            }
            catch (Exception E)
            {
                //	this.Response.Write(entity.DBAccess().GetLastSQL());                
                this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATEEdit_Msg17 + E.Message); //"失败！" 
#if Debug 
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
            }
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_AREA> con = new GenericRepository<BASE_AREA>(context);
        BASE_AREA entity = new BASE_AREA();
        try
        {
            entity.id = this.KeyID.ToString();
            con.Delete(entity);
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_DelFail + E.Message); //删除失败！this.Alert("删除失败！" + E.Message);
#if Debug 
            this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
        }

    }

  

  
}

