using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using DreamTek.ASRS.Business.Base;

public partial class Apps_BASE_FrmBASE_InOutTypeStatusEdit : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData();
            }
            else
            {
                this.txtCreateOwner.Text = OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
                this.txtCreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            }
        }
    }

    public string InOrOut 
    {
        get 
        {
            if (ViewState["InOrOut"] == null)
            {
                ViewState["InOrOut"] = string.Empty;
            }
            return ViewState["InOrOut"].ToString();
        }
        set
        {
            ViewState["InOrOut"] = value;
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_InOutTypeStatus');return false;";

        Help.DropDownListDataBind(GetParametersByFlagType("InOutType"), drpInOut, "", "FLAG_NAME", "FLAG_ID", "");//类型分类
        Help.DropDownListDataBind(GetParametersByFlagType("UsedOrCanceled"), ddpStatus, "", "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(GetParametersByFlagType("TrueOrFalse"), drpIsMatchSo, "", "FLAG_NAME", "FLAG_ID", "");//是否匹配单据
        Help.DropDownListDataBind(GetParametersByFlagType("TrueOrFalse"), drpIsMatchVendor, "", "FLAG_NAME", "FLAG_ID", "");//是否匹配供应商
    }
        #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<INTYPE> in_conn = new GenericRepository<INTYPE>(db);
        IGenericRepository<OUTTYPE> out_conn = new GenericRepository<OUTTYPE>(db);

        INTYPE inBO = (from p in in_conn.Get()
                       where p.typeid == KeyID
                        select p).FirstOrDefault();
        if (inBO != null && inBO.typeid != null && inBO.typeid.Length > 0)
        {
            InOrOut = "IN";

            this.txtCERPCODE.Text = inBO.cerpcode; ;
            this.txtTYPENAME.Text = inBO.typename;
            //this.txtCreateOwner.Text = inBO.createuser;
            //this.txtEnableName.Text = inBO.enableuser;
            this.txtCreateOwner.Text = OPERATOR.GetUserNameByAccountID(inBO.createuser);
            this.txtEnableName.Text = OPERATOR.GetUserNameByAccountID(inBO.enableuser);
            //入库时间不为空，绑定时间
            if (inBO.createdate.HasValue)
            {
                this.txtCreateTime.Text = inBO.createdate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            //作废时间不为空，绑定时间
            if (inBO.enabledate.HasValue)
            {
                this.txtEnableTime.Text = inBO.enabledate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }

            //入库
            this.drpInOut.SelectedIndex = 0;
            if (inBO.Is_Query.HasValue && inBO.Is_Query.Value == 1)
            {
                ckbQuery.Checked = true;
            }
            ////类型
            //if (inBO.t == "I")
            //{
            //    //入库
            //    this.drpInOut.SelectedIndex = 0;
            //}
            //else
            //{
            //    //出库
            //    this.drpInOut.SelectedIndex = 1;
            //}
            //状态
            if (inBO.enable == "0")
            {
                //使用
                this.ddpStatus.SelectedIndex = 0;
            }
            else
            {
                //作废
                this.ddpStatus.SelectedIndex = 1;
            }

            //this.txtCERPCODE.Enabled = false;
            this.drpInOut.Enabled = false;
            //状态为作废的，编辑情况下的功能不可用
            if (inBO.enable == "1")
            {
                this.txtCERPCODE.Enabled = false;
                this.txtTYPENAME.Enabled = false;
                this.btnSave.Enabled = false;
            }
            //drpIsMatchSo.SelectedValue = "0";
            //drpIsMatchSo.Enabled = false;
            drpIsMatchSo.SelectedValue = inBO.IsMatchSo;
            drpIsMatchVendor.SelectedValue = inBO.IsMatchVendor;
        }
        else 
        { 
             OUTTYPE outBO = (from p in out_conn.Get()
                              where p.typeid == KeyID
                              select p).FirstOrDefault();
             if (outBO != null && outBO.typeid != null && outBO.typeid.Length > 0)
             {
                 InOrOut = "OUT";

                 this.txtCERPCODE.Text = outBO.cerpcode; ;
                 this.txtTYPENAME.Text = outBO.typename;
                 this.txtCreateOwner.Text = outBO.createuser;
                 this.txtEnableName.Text = outBO.enableuser;
                 //入库时间不为空，绑定时间
                 if (outBO.createdate.HasValue)
                 {
                     this.txtCreateTime.Text = outBO.createdate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                 }
                 //作废时间不为空，绑定时间
                 if (outBO.enabledate.HasValue)
                 {
                     this.txtEnableTime.Text = outBO.enabledate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                 }

                 //出库
                 this.drpInOut.SelectedIndex = 1;

                 if (outBO.Is_Query.HasValue && outBO.Is_Query.Value == 1)
                 {
                     ckbQuery.Checked = true;
                 }

                 ////类型
                 //if (inBO.t == "I")
                 //{
                 //    //入库
                 //    this.drpInOut.SelectedIndex = 0;
                 //}
                 //else
                 //{
                 //    //出库
                 //    this.drpInOut.SelectedIndex = 1;
                 //}
                 //状态
                 if (outBO.enable == "0")
                 {
                     //使用
                     this.ddpStatus.SelectedIndex = 0;
                 }
                 else
                 {
                     //作废
                     this.ddpStatus.SelectedIndex = 1;
                 }

                 this.txtCERPCODE.Enabled = false;
                 this.drpInOut.Enabled = false;
                 //状态为作废的，编辑情况下的功能不可用
                 if (outBO.enable == "1")
                 {
                     txtCERPCODE.Enabled = false;
                     this.txtTYPENAME.Enabled = false;
                     this.btnSave.Enabled = false;
                 }
                 drpIsMatchSo.SelectedValue = outBO.IsMatchSo;
                 drpIsMatchVendor.SelectedValue = outBO.IsMatchVendor;
             }
        }   
    }

    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.CheckData())
        {
            string strKeyID = "";
            int sum = 0;

            IGenericRepository<INTYPE> in_conn = new GenericRepository<INTYPE>(db);
            IGenericRepository<OUTTYPE> out_conn = new GenericRepository<OUTTYPE>(db);

            if (this.Operation() == SYSOperation.New)
            {
                BaseCommQuery bc = new BaseCommQuery();

                if (this.drpInOut.Text == "I")
                {
                    
                    if (bc.GetINTypeIDCount(this.txtCERPCODE.Text.Trim()) > 0)
                    {
                        this.Alert(Resources.Lang.FrmBASE_InOutTypeStatusEdit_Msg01);//入库类型编号已存在
                        this.txtCERPCODE.Focus();
                        return;
                    }
  
                    INTYPE bo = new INTYPE();
                    strKeyID = Guid.NewGuid().ToString();
                    bo.typeid = strKeyID;
                    bo.cerpcode = txtCERPCODE.Text.Trim();
                    bo.typename = txtTYPENAME.Text.Trim();
                    bo.transaction_action_id = "27";
                    bo.transaction_source_type_id = "13";
                    bo.attribute1 = "N";
                    bo.type_class = "2";
                    bo.enable = "0";
                    bo.createdate = DateTime.Now;
                    bo.createuser = WmsWebUserInfo.GetCurrentUser().UserNo;
                    //bo.enabledate = DateTime.Now;
                    //bo.enableuser = WmsWebUserInfo.GetCurrentUser().UserNo;
                    if (ckbQuery.Checked)
                    {
                        bo.Is_Query = 1;
                    }
                    else {
                        bo.Is_Query = 0;
                    }
                    bo.IsMatchSo = drpIsMatchSo.SelectedValue;
                    bo.IsMatchVendor = drpIsMatchVendor.SelectedValue;
                    in_conn.Insert(bo);
                    in_conn.Save();
                    this.AlertAndBack("FrmBASE_InOutTypeStatusEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.FrmBASE_InOutTypeStatusEdit_Msg02);//入库保存成功

                    //DataTable dt = listQuery.GetInType(this.txtCERPCODE.Text.Trim());
                    //if (dt.Rows.Count > 0)
                    //{
                    //    this.Alert("入库类型编号已存在");
                    //    this.txtCERPCODE.Focus();
                    //}
                    //else
                    //{
                    //sum = listQuery.InsertIN(txtCERPCODE.Text.Trim(), txtTYPENAME.Text.Trim(), WmsWebUserInfo.GetCurrentUser().UserNo);
                    //if (sum > 0)
                    //{
                    //    strKeyID = Guid.NewGuid().ToString();
                    //    this.AlertAndBack("FrmBASE_InOutTypeStatusEdit.aspx?" + BuildQueryString(SYSOperation.New, strKeyID), "入库保存成功");
                    //}
                    //else
                    //{
                    //    this.Alert("入库新增失败！");
                    //}
                    //}
                }
                else if (this.drpInOut.Text == "O")
                {
                    if (bc.GetOutTypeIDCount(this.txtCERPCODE.Text.Trim()) > 0)
                    {
                        this.Alert(Resources.Lang.FrmBASE_InOutTypeStatusEdit_Msg03);//出库类型编号已存在
                        this.txtCERPCODE.Focus();
                        return;
                    }
                    strKeyID = Guid.NewGuid().ToString();
                    OUTTYPE bo = new OUTTYPE();
                    bo.typeid = strKeyID;
                    bo.cerpcode = txtCERPCODE.Text.Trim();
                    bo.typename = txtTYPENAME.Text.Trim();
                    bo.transaction_action_id = "27";
                    bo.transaction_source_type_id = "13";
                    bo.attribute1 = "N";
                    bo.type_class = "2";
                    bo.enable = "0";
                    bo.createdate = DateTime.Now;
                    bo.createuser = WmsWebUserInfo.GetCurrentUser().UserNo;
                    //bo.enabledate = DateTime.Now;
                    //bo.enableuser = WmsWebUserInfo.GetCurrentUser().UserNo;
                    if (ckbQuery.Checked)
                    {
                        bo.Is_Query = 1;
                    }
                    else {
                        bo.Is_Query = 0;
                    }
                    bo.IsMatchSo = drpIsMatchSo.SelectedValue;
                    bo.IsMatchVendor = drpIsMatchVendor.SelectedValue;
                    out_conn.Insert(bo);
                    out_conn.Save();
                    this.AlertAndBack("FrmBASE_InOutTypeStatusEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.FrmBASE_InOutTypeStatusEdit_Msg04); //出库保存成功
                    //DataTable dt = listQuery.GetOutType(this.txtCERPCODE.Text.Trim());
                    // if (dt.Rows.Count > 0)
                    // {
                    //     this.Alert("出库类型编号已存在");
                    //     this.txtCERPCODE.Focus();
                    // }
                    // else
                    // {
                    //sum = listQuery.InsertOUT(txtCERPCODE.Text.Trim(), txtTYPENAME.Text.Trim(), WmsWebUserInfo.GetCurrentUser().UserNo);
                    //if (sum > 0)
                    //{
                    //    strKeyID = Guid.NewGuid().ToString();
                    //    this.AlertAndBack("FrmBASE_InOutTypeStatusEdit.aspx?" + BuildQueryString(SYSOperation.New, strKeyID), "出库保存成功");
                    //}
                    //else
                    //{
                    //    this.Alert("出库新增失败！");
                    //}
                    //}
                }
                
            }
            else if (this.Operation() == SYSOperation.Modify)
            {
                if (this.drpInOut.Text == "I")
                {

                    INTYPE inBO = (from p in in_conn.Get()
                                   where p.typeid == KeyID
                                   select p).FirstOrDefault();
                    if (inBO != null && inBO.typeid != null && inBO.typeid.Length > 0)
                    {
                        inBO.typename = txtTYPENAME.Text.Trim();
                        //inBO.enable = "1";
                        //inBO.enabledate = DateTime.Now;
                        //inBO.enableuser = WmsWebUserInfo.GetCurrentUser().UserNo;
                        if (ckbQuery.Checked)
                        {
                            inBO.Is_Query = 1;
                        }
                        else 
                        {
                            inBO.Is_Query = 0;
                        }
                        inBO.IsMatchSo = drpIsMatchSo.SelectedValue;
                        inBO.IsMatchVendor = drpIsMatchVendor.SelectedValue;
                        in_conn.Update(inBO);
                        in_conn.Save();

                    }
                    this.AlertAndBack("FrmBASE_InOutTypeStatusEdit.aspx?" + BuildQueryString(SYSOperation.Modify, KeyID), Resources.Lang.FrmBASE_InOutTypeStatusEdit_Msg05);//"入库修改成功"
                    //sum = listQuery.UpdateIN(txtTYPENAME.Text.Trim(), this.KeyID);
                    //if (sum > 0)
                    //{
                    //    strKeyID = this.KeyID;
                    //    this.AlertAndBack("FrmBASE_InOutTypeStatusEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), "入库修改成功");
                    //}
                    //else
                    //{
                    //    this.Alert("入库修改失败！");
                    //}
                }
                else if (this.drpInOut.Text == "O")
                {

                    OUTTYPE outBO = (from p in out_conn.Get()
                                     where p.typeid == KeyID
                                     select p).FirstOrDefault();
                    if (outBO != null && outBO.typeid != null && outBO.typeid.Length > 0)
                    {
                        outBO.typename = txtTYPENAME.Text.Trim();
                        //outBO.enable = "1";
                        //outBO.enabledate = DateTime.Now;
                        //outBO.enableuser = WmsWebUserInfo.GetCurrentUser().UserNo;
                        if (ckbQuery.Checked)
                        {
                            outBO.Is_Query = 1;
                        }
                        else
                        {
                            outBO.Is_Query = 0;
                        }
                        outBO.IsMatchSo = drpIsMatchSo.SelectedValue;
                        outBO.IsMatchVendor = drpIsMatchVendor.SelectedValue;
                        out_conn.Update(outBO);
                        out_conn.Save();
                    }
                    this.AlertAndBack("FrmBASE_InOutTypeStatusEdit.aspx?" + BuildQueryString(SYSOperation.Modify, KeyID), Resources.Lang.FrmBASE_InOutTypeStatusEdit_Msg06);//出库修改成功

                    //sum = listQuery.UpdateOUT(txtTYPENAME.Text.Trim(), this.KeyID);
                    //if (sum > 0)
                    //{
                    //    strKeyID = this.KeyID;
                    //    this.AlertAndBack("FrmBASE_InOutTypeStatusEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), "出库修改成功");
                    //}
                    //else
                    //{
                    //    this.Alert("出库修改失败！");
                    //}
                }
            }
        }

    }

    public bool CheckData()
    {
        if (this.txtCERPCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBASE_InOutTypeStatusEdit_Msg07);//类型编码不允许空！
            this.SetFocus(txtCERPCODE);
            return false;
        }
        else 
        {
            int i = 0;
            if (int.TryParse(this.txtCERPCODE.Text.Trim(), out i))
            {
                if (this.txtCERPCODE.Text.Trim().Contains("."))
               {
                   this.Alert(Resources.Lang.FrmBASE_InOutTypeStatusEdit_Msg08);//类型编码只能是整形数字！
                   this.SetFocus(txtCERPCODE);
                   return false;
               }
            }
            else {
                this.Alert(Resources.Lang.FrmBASE_InOutTypeStatusEdit_Msg09); //类型编码只能是整形数字！
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }

        if (this.txtTYPENAME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBASE_InOutTypeStatusEdit_Msg10); //类型名称项不允许空！
            this.SetFocus(txtTYPENAME);
            return false;
        }
        return true;
    }
}