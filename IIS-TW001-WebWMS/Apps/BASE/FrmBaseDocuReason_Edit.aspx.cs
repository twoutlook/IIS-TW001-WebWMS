using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq;

public partial class BASE_FrmBaseDocuReason_Edit : WMSBasePage
{

    DBContext context = new DBContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData(this.KeyID);
                txtREASONCODE.Enabled = false;
                ddlSTATES.Enabled = true;
            }
            if (this.Operation() == SYSOperation.New)
            {
                hdnID.Value = Guid.NewGuid().ToString();
                ddlSTATES.Enabled = true;
                this.txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo); 
                //OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
                //this.txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            }
        }
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_DocuReason');return false;";
        Help.DropDownListDataBind(GetParametersByFlagType("ACTIONSCOPE"), ddlACTIONSCOPE, Resources.Lang.Commona_PleaseSelect, "FLAG_NAME", "FLAG_ID", "");//作用域
        Help.DropDownListDataBind(GetParametersByFlagType("ReasonCodeStatus"), ddlSTATES, "", "FLAG_NAME", "FLAG_ID", "");//状 态
    }
    #endregion

    #region 事件方法
    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData(string ID)
    {
    
        IGenericRepository<BASE_DOCUREASON> con = new GenericRepository<BASE_DOCUREASON>(context);
        var caseList = from p in con.Get()
                       where p.id == ID
                       select p;
        BASE_DOCUREASON entity = caseList.ToList().FirstOrDefault<BASE_DOCUREASON>();
        entity.id = this.KeyID;
        //entity.SelectByPKeys();
        try
        {
            ddlACTIONSCOPE.SelectedValue = entity.actionscope;
        }
        catch (Exception)
        {
            
            throw;
        }
        try
        {
            txtLASTUPDATETIME.Text = entity.lastupdatetime != null ? Convert.ToDateTime(entity.lastupdatetime).ToString("yyyy-MM-dd HH:mm:ss") : "";
        }
        catch (Exception)
        {
            
            throw;
        }
        

        txtREASONCODE.Text = entity.reasoncode;
        txtREASONCONTENT.Text = entity.reasoncontent;
        txtLASTUPDATEOWNER.Text = OPERATOR.GetUserNameByAccountID(entity.lastupdateowner);
        ddlSTATES.SelectedValue = entity.states;
        txtCCREATEOWNERCODE.Text = OPERATOR.GetUserNameByAccountID(entity.ccreateownercode); //OPERATOR.GetUserNameByAccountID(entity.ccreateownercode);
        try
        {
            txtDCREATETIME.Text = entity.dcreatetime != null ? Convert.ToDateTime(entity.dcreatetime).ToString("yyyy-MM-dd HH:mm:ss") : "";
        }
        catch (Exception)
        {
            
            throw;
        }

        if (entity.isfromerp != null && !string.IsNullOrEmpty(entity.isfromerp) && entity.isfromerp =="1")
        {
            ckbisfromerp.Checked = true;
        }
        else
        {
            ckbisfromerp.Checked = false;
        }
        this.txtID.Text = this.KeyID;
     }

    /// <summary>
    /// 校驗數據
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (this.ddlACTIONSCOPE.SelectedValue == "")
        {
            this.Alert(Resources.Lang.FrmBaseDocuReason_Edit_Msg01);//作用域不允許空！
            this.SetFocus(ddlACTIONSCOPE);
            return false;
        }
        //
        if (this.txtREASONCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBaseDocuReason_Edit_Msg02);//理由编码不允許空！
            this.SetFocus(txtREASONCODE);
            return false;
        }
        //
        if (this.txtREASONCODE.Text.Trim().Length > 0)
        {
            if (this.txtREASONCODE.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmBaseDocuReason_Edit_Msg03); //理由编码項超過指定的長度30！
                this.SetFocus(txtREASONCODE);
                return false;
            }
        }

    
        //
        if (this.txtREASONCONTENT.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBaseDocuReason_Edit_Msg04); //理由码说明項不允許空！
            this.SetFocus(txtREASONCONTENT);
            return false;
        }
        //
        if (this.txtREASONCONTENT.Text.Trim().Length > 0)
        {
            if (this.txtREASONCONTENT.Text.GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmBaseDocuReason_Edit_Msg05);//理由码说明項超過指定的長度100！
                this.SetFocus(txtREASONCONTENT);
                return false;
            }
        }
        //
        return true;

    }




    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_DOCUREASON SendData()
    {
        BASE_DOCUREASON entity = new BASE_DOCUREASON();

        this.ddlACTIONSCOPE.Text = this.ddlACTIONSCOPE.Text.Trim();
        if (this.ddlACTIONSCOPE.Text.Length > 0)
        {
            entity.actionscope = ddlACTIONSCOPE.SelectedValue;
        }
        else
        {
            entity.states =string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSTATUS = null;
        }

        
        if (this.txtREASONCODE.Text.Length > 0)
        {
            entity.reasoncode = txtREASONCODE.Text;
        }
        else
        {
            entity.reasoncode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CACCOUNTID = null;
        }
        //
        this.txtREASONCONTENT.Text = this.txtREASONCONTENT.Text.Trim();
        if (this.txtREASONCONTENT.Text.Length > 0)
        {
            entity.reasoncontent = txtREASONCONTENT.Text;
        }
        else
        {
            entity.reasoncontent = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.COPERATORNAME = null;
        }
        //
        this.ddlSTATES.Text = this.ddlSTATES.Text.Trim();
        if (this.ddlSTATES.Text.Length > 0)
        {
            entity.states = ddlSTATES.SelectedValue;
        }
        else
        {
            entity.states = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSTATUS = null;
        }

        this.txtDCREATETIME.Text = this.txtDCREATETIME.Text.Trim();
        if (this.txtDCREATETIME.Text.Length > 0)
        {
            entity.dcreatetime = txtDCREATETIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        else
        {
           // entity.dcreatetime = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CALIAS = null;
        }
        //
        this.txtCCREATEOWNERCODE.Text = this.txtCCREATEOWNERCODE.Text.Trim();
        if (this.txtCCREATEOWNERCODE.Text.Length > 0)
        {
            entity.ccreateownercode = OPERATOR.GetUserIDByUserName(txtCCREATEOWNERCODE.Text);
        }
        else
        {
            entity.ccreateownercode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CDEPARTMENT = null;
        }
        //
        this.lblLASTUPDATETIME.Text = this.lblLASTUPDATETIME.Text.Trim();
        if (this.lblLASTUPDATETIME.Text.Length > 0 && this.lblLASTUPDATETIME.Text != Resources.Lang.FrmMixed_D_Label7)//最后更新时间
        {
            entity.lastupdateowner = OPERATOR.GetUserIDByUserName(txtLASTUPDATEOWNER.Text);
        }
        else
        {
            //entity.cduty = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CDUTY = null;
        }
        //
        this.txtLASTUPDATEOWNER.Text = this.txtLASTUPDATEOWNER.Text.Trim();
        if (this.txtLASTUPDATEOWNER.Text.Length > 0)
        {
            entity.lastupdateowner = OPERATOR.GetUserIDByUserName(txtLASTUPDATEOWNER.Text); //txtLASTUPDATEOWNER.Text;
        }
        else
        {
            entity.lastupdateowner = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CPHONE = null;
        }
        //
        if (ckbisfromerp.Checked)
        {
            entity.isfromerp = "1";
        }
        else
        {
            entity.isfromerp = "0";
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




    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_DOCUREASON> con = new GenericRepository<BASE_DOCUREASON>(context);
        if (this.CheckData())
        {
            var entity = this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
                entity.id = this.KeyIDS[0];
            }
            string strKeyID = "";
            try
            {

                //
                if (this.Operation() == SYSOperation.Modify)
                {
                   
                   
                    strKeyID = txtID.Text.Trim();
                    entity.id = strKeyID;
                    //entity.dcreatetime = Convert.ToDateTime(txtDCREATETIME.Text.Trim());
                    entity.dcreatetime = txtDCREATETIME.Text.Trim().ToDateTime("yyyy-MM-dd HH:mm:ss");
                    entity.ccreateownercode = OPERATOR.GetUserIDByUserName(txtCCREATEOWNERCODE.Text.Trim()); //txtCCREATEOWNERCODE.Text.Trim();
                    entity.lastupdatetime = DateTime.Now;
                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Update(entity);
                    var caseList1 = con.Get().Where(p => p.reasoncode == txtREASONCODE.Text.Trim() && p.id != txtID.Text.Trim());
                    //判断理由编码是否重复
                    if (caseList1.ToList().Count > 1)
                    {
                        this.Alert(Resources.Lang.FrmBaseDocuReason_Edit_Msg06); //理由编码已存在，不允许重复！
                    }
                    else
                    {
                        con.Save();
                        this.AlertAndBack("FrmBaseDocuReason_Edit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
                    }
                   
                }
              
                else if (this.Operation() == SYSOperation.New)
                {
                    var caseList1 = con.Get().Where(p => p.reasoncode == txtREASONCODE.Text.Trim() && p.id != txtID.Text.Trim());
                    //判断理由编码是否重复
                    if (caseList1.ToList().Count > 0)
                    {
                        this.Alert(Resources.Lang.FrmBaseDocuReason_Edit_Msg06); //理由编码已存在，不允许重复！
                    }
                    else
                    {
                        strKeyID = Guid.NewGuid().ToString();
                        entity.id = strKeyID;
                        entity.dcreatetime = System.DateTime.Now;
                        entity.ccreateownercode = OPERATOR.GetUserIDByUserName(txtCCREATEOWNERCODE.Text.Trim()); //txtCCREATEOWNERCODE.Text.Trim();
                    
                        con.Insert(entity);
                        con.Save();
                        this.AlertAndBack("FrmBaseDocuReason_Edit.aspx?" + BuildQueryString(SYSOperation.New, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
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

    #endregion



}