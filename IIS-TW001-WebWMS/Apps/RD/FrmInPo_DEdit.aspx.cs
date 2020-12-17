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
using DreamTek.ASRS.Business.Others;

public partial class RD_FrmInPo_DEdit : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ShowPARTDiv1.SetCINVCODE = this.txtCINVCODE.ClientID;
        this.ShowPARTDiv1.SetCINVNAME = this.txtCINVNAME.ClientID;
        this.ShowPARTDiv1.GetComp = true;
        if(this.IsPostBack == false)
        {
           // this.Operation = SysOperation.New;
            this.InitPage();
            if (this.operation != SYSOperation.New)
            {
                ShowData();
            }
            else
            {
                string IDS = Request.QueryString["IDS"];
                LoadIDS(IDS);
            }
        }
    }

    #region 页面属性

    /// <summary>
    /// 当前页面的操作
    /// </summary>
    public SYSOperation operation
    {
        get { return (SYSOperation)Enum.Parse(typeof(SYSOperation), this.hiddOperation.Value); }
        set { this.hiddOperation.Value = value.ToString(); }
    }

    

    #endregion

    public void LoadIDS(string IDS)
    {
        this.txtID.Text = IDS;
        this.txtPOLine.Text = aotuIDS(this.txtID.Text).ToString();
        this.txtID.Enabled = false;
        this.txtIDS.Text = Guid.NewGuid().ToString();
        this.txtIDS.Enabled = false;
        txtCREATEOWNER.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
        txtCREATETIME.Text = Comm_Function.GetDBDateTime("yyyy-MM-dd HH:mm:ss");
        txtCSTATUS.SelectedValue = "0";
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
        this.operation = this.Operation();
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INPO_D');return false;";
        //设置保存按钮的文字及其状态
        if (this.operation == SYSOperation.View)
        {
        	this.btnSave.Visible = false;
        }
        else if (this.operation == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.CommonB_Approve;//"审批";
        }

        //状态
        Help.DropDownListDataBind(GetParametersByFlagType("IN_PO_D"), txtCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
        //质检
        Help.DropDownListDataBind(GetParametersByFlagType("INSPECTION"), dpdQC, "", "FLAG_NAME", "FLAG_ID", "");
        
    }

    #endregion
   
    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
	{
        INPO_D entity = this.context.INPO_D.Where(x => x.ids == this.KeyID).FirstOrDefault();
        if (entity != null) {
            this.txtIDS.Text = entity.ids;
            this.txtID.Text = entity.id;
            txtPOLine.Text = entity.poline;
            this.txtCINVCODE.Text = entity.cinvcode;
            this.txtCINVNAME.Text = entity.cinvname;
            txtIQUANTITY.Text = Convert.ToDecimal(entity.iquantity).ToString("f2");
            txtUNIT.Text = entity.unit;
            txtPRICE.Text = (entity.price.HasValue && entity.price >0)? entity.price.Value.ToString("f2") : "";
            txtTotal.Text = (entity.price.HasValue && entity.price > 0) ? (entity.price.Value * Convert.ToDecimal(entity.iquantity)).ToString("f2") : "";
            txtCSTATUS.SelectedValue = entity.status.ToString();
            dpdQC.SelectedValue = entity.qc.ToString();
            txtCREATEOWNER.Text = OPERATOR.GetUserNameByAccountID(entity.createowner);
            txtCREATETIME.Text = entity.createtime.HasValue ? entity.createtime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            txtLASTUPDATEOWNER.Text =  OPERATOR.GetUserNameByAccountID(entity.lastupdateowner);
            txtLASTUPDATETIME.Text = entity.lastupdatetime.HasValue? entity.lastupdatetime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            txtPOLine.Enabled = false;
            txtCINVCODE.Enabled = false;
            txtCINVNAME.Enabled = false;
        }     
	}
    /// <summary>
    /// 获取下一个子项的编号
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private int aotuIDS (string id)
    {
        int temp = 0;
        string maxPoline = db.INPO_D.Where(x => x.id == id).Max(x => x.poline);
        if (!string.IsNullOrEmpty(maxPoline))
        {
            temp = Convert.ToInt32(maxPoline);
        }
        temp++;     
        return temp;
    }
    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (this.txtPOLine.Text.Trim() == "")
        {
            //PO项次不允许空
            this.Alert(Resources.Lang.FrmInPo_DEdit_Label13+ "！");
            this.SetFocus(txtPOLine);
            return false;
        }
        if (txtPOLine.Text.Trim() != "")
        {
            if (this.operation == SYSOperation.New)
            {
                //PO项次不为空的时候，不允许重复
                if (this.CheckPoLine(txtID.Text.Trim(), txtPOLine.Text.Trim()))
                {
                    //采购单中已存在相同的PO项次号
                    this.Alert(Resources.Lang.FrmInPo_DEdit_Label14 + "[" + txtPOLine.Text.Trim() + "]");
                    this.SetFocus(txtPOLine);
                    return false;
                }
            }
           
        }
       
        if ((this.txtCINVCODE.Text.Trim() == ""))
        {
            //料号不允许为空
            this.Alert(Resources.Lang.FrmInPo_DEdit_Label15 );
            this.SetFocus(txtCINVCODE);
            return false;
        }

        //检查料号是否存在
        if (!this.Check_Part_IsExist(txtCINVCODE.Text.Trim().ToUpper()))
        {
            //+ "料号不存在"
            this.Alert(Resources.Lang.FrmInPo_DEdit_Label16 );
            this.SetFocus(txtCINVCODE);
            return false;
        }

        if (this.operation == SYSOperation.New)
        {
            if (this.CheckPoCinvCode(txtID.Text.Trim(), txtCINVCODE.Text.Trim().ToUpper()))
            {
                //采购单中已存在相同的料号
                this.Alert(Resources.Lang.FrmInPo_DEdit_Label17 );
                this.SetFocus(txtPOLine);
                return false;
            }
        }
        
        if ((this.txtCINVNAME.Text.Trim() == ""))
        {
            //品名不允许为空
            this.Alert(Resources.Lang.FrmInPo_DEdit_Label18);
            this.SetFocus(txtCINVCODE);
            return false;
        }
        if((this.txtIQUANTITY.Text.Trim()==""))
        {
            //数量不允许为空
            this.Alert(Resources.Lang.FrmInPo_DEdit_Label19 );
            this.SetFocus(txtIQUANTITY);
            return false;
        }
 
        string msg = string.Empty;
       
        if (this.txtIQUANTITY.Text.GetLengthByByte() > 12)
        {
            //数量位数超过指定的长度12
            this.Alert(Resources.Lang.FrmInPo_DEdit_Label20 + "！");
            this.SetFocus(txtIQUANTITY);
            return false;
        }
        //检查数量，不允许小数，负数，0 CQ 2015-2-12 13:38:24
        if (!(Comm_Function.Fun_IsDecimal(txtIQUANTITY.Text.Trim(), 0, 0, 1, out msg)))
        {
            this.Alert(msg);
            this.SetFocus(txtIQUANTITY);
            return false;
        }

        if (this.txtPRICE.Text.Trim().Length > 0)
        {
            if (this.txtPRICE.Text.GetLengthByByte() > 8)
            {
                this.Alert(Resources.Lang.FrmInPo_DEdit_Label21 + "！");
                this.SetFocus(txtPRICE);
                return false;
            }
            //检查数量，允许小数，不允许负数，0 CQ 2015-2-12 13:38:24
            if (!(Comm_Function.Fun_IsDecimal(txtPRICE.Text.Trim(), 0, 0, 1, out msg)))
            {
                this.Alert(msg);
                this.SetFocus(txtPRICE);
                return false;
            }
        }
        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public INPO_D SendData()
    {
        INPO_D entity = new INPO_D();
        entity.ids = this.txtIDS.Text.Trim();
        entity.id = this.txtID.Text.Trim();
        entity.poline = this.txtPOLine.Text.Trim();
        entity.cinvcode = this.txtCINVCODE.Text.Trim();
        entity.cinvname = this.txtCINVNAME.Text.Trim();
        entity.iquantity = Convert.ToDecimal(this.txtIQUANTITY.Text.Trim());
        entity.unit = this.txtUNIT.Text.Trim();
        if (this.txtPRICE.Text.Trim().Length > 0)
        {
            entity.price = Convert.ToDecimal(this.txtPRICE.Text.Trim());
        }
        if (this.operation == SYSOperation.New)
        {
            entity.status = 0;
        }
        else
        {
            entity.status = Convert.ToInt32(txtCSTATUS.SelectedValue);          
        }
        entity.qc = Convert.ToInt32(dpdQC.SelectedValue);
        entity.createowner = txtCREATEOWNER.Text;
        entity.createtime = Convert.ToDateTime(txtCREATETIME.Text);
        entity.lastupdateowner = txtLASTUPDATEOWNER.Text;
        if (txtLASTUPDATETIME.Text.Trim().Length > 0)
        {
            entity.lastupdatetime = Convert.ToDateTime(txtLASTUPDATETIME.Text);
        }
  
        return entity;

    }

    /// 判断料号是否存在
    /// <summary>
    /// 判断料号是否存在
    /// </summary>
    /// <param name="cinvcode">料号</param>
    /// <returns>存在 true 不存在 false</returns>
    private bool Check_Part_IsExist(string cinvcode)
    {
        if (string.IsNullOrEmpty(cinvcode))
        {
            return false;
        }
        var obj = db.BASE_PART.Where(x => x.cpartnumber == cinvcode).FirstOrDefault();
        if (obj != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //检查采购单中是否存在相同料号
    public bool CheckPoCinvCode(string inpo_id, string cinvcode)
    {
        var obj = db.INPO_D.Where(x => x.id == inpo_id && x.cinvcode == cinvcode).FirstOrDefault();
        if (obj != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.btnSave.Enabled = false; //CQ 2013-5-13 13:47:51
        if (this.CheckData()) 
        {
            string msg = string.Empty;
            INPO_D entity = this.SendData();
        	string strKeyID="";
        	strKeyID += entity.ids;
        	try 
        	{
                if (this.operation == SYSOperation.Modify)
        		{
                    using (var conn = this.context) {
                        entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                        entity.lastupdatetime = Comm_Function.GetDBDateTime();
                        conn.INPO_D.Attach(entity);
                        conn.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        int result = conn.SaveChanges();
                        if (result > 0)
                        {
                            //保存成功
                            msg = Resources.Lang.CommonB_SaveSuccess + "!";
                        }
                        else
                        {
                            //保存失败
                            msg = Resources.Lang.CommonB_SaveFailed + "!";
                        }
                    }
        		}
                else if (this.operation == SYSOperation.New)
        		{
                    strKeyID = Guid.NewGuid().ToString();
                    entity.ids = strKeyID;
                    using (var conn = this.context)
                    {
                        conn.INPO_D.Add(entity);
                        int result = conn.SaveChanges();
                        if (result > 0)
                        {
                            //保存成功
                            msg = Resources.Lang.CommonB_SaveSuccess + "!";
                        }
                        else
                        {
                            //保存失败
                            msg = Resources.Lang.CommonB_SaveFailed + "!";
                        }
                    }
        		}
                //保存成功
                if (msg == Resources.Lang.CommonB_SaveSuccess + "!")
                {
                    this.Alert(msg);
                    this.WriteScript("window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INPO_D');");
                }
                else {
                    this.Alert(msg);
                }
        	}
        	catch (Exception ex) 
        	{
                //失败
                this.Alert(this.GetOperationName() + Resources.Lang.CommonB_Failed + "！" + ex.Message); 
                this.btnSave.Enabled = true;
        	}
        }
        this.btnSave.Enabled = true;
    }

    //判断相同的PO项次是否存在相同采购单中
    public bool CheckPoLine(string inpo_id, string poline)
    {
        return context.INPO_D.Where(x => x.id == inpo_id && x.poline == poline).Any();
    }
}

