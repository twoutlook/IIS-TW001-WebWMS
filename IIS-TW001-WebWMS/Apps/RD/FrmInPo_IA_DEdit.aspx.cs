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
using DreamTek.ASRS.Business.Others;

/// <summary>
/// 描述: 预入库明细-->FrmINASN_IA_DEdit 页面后台类文件
/// 作者: --chenlei
/// 创建于: 2013-01-19 15:31:32
/// </summary>
public partial class RD_FrmInPo_IA_DEdit : WMSBasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
 
        this.ShowPO_Div1.SetPOLine = txtPOLine.ClientID;
        this.ShowPO_Div1.SetCINVCODE = this.txtCINVCODE.ClientID;
        this.ShowPO_Div1.SetCINVNAME = this.txtCINVNAME.ClientID;
        this.ShowPO_Div1.SetIqty = this.txtQTYTOTAL.ClientID;
        this.ShowPO_Div1.SetIDS = this.txtPO_IDS.ClientID;
        this.ShowPO_Div1.GetComp = true; 

        if(this.IsPostBack == false)
        {
            this.operation = this.Operation();
            this.InitPage();
            if (this.operation == SYSOperation.Modify)
            {
                ShowData();
            }
            else if(this.operation==SYSOperation.New)
            {
                string ID = Request.QueryString["parentId"];
                LoadIDS(ID);
                txtcreateuser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtcreatetime.Text = Comm_Function.GetDBDateTime("yyyy-MM-dd HH:mm:ss");
            }
        }

        ShowPO_Div1.SetSearchPO = txtPO.Text.Trim();
        ShowPO_Div1.SetSearchIA_ID = txtID.Text.Trim();
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

    public void LoadIDS(string ID)
    {
        this.txtID.Text = ID;
        this.txtID.Enabled = false;
        this.txtIDS.Text = Guid.NewGuid().ToString();
        this.txtIDS.Enabled = false;
        this.txtLine.Text = this.CreateLine(ID).ToString();

        INASN_IA entity = db.INASN_IA.Where(x => x.id == ID).FirstOrDefault();
        if (entity != null) {
            txtPO.Text = entity.pono;
            if (entity.cstatus != "0")
            {
                this.btnSave.Enabled = false;
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INPO_IA_D');return false;";
        //设置保存按钮的文字及其状态
        if(this.operation == SYSOperation.View)
        {
        	this.btnSave.Visible = false;
        }
        else if(this.operation == SYSOperation.Approve)
        {
        	this.btnSave.Text =  Resources.Lang.CommonB_Approve;//"审批";
        }

        //状态
        Help.DropDownListDataBind(GetParametersByFlagType("IN_PO_IA_D"), dpdstatus, "", "FLAG_NAME", "FLAG_ID", "");
        //datecode类型
        Help.DropDownListDataBind(GetParametersByFlagType("DateCodeTYPE"), txtDateCodeType, "", "FLAG_NAME", "FLAG_ID", "");
        
    }

    #endregion
   
    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
	{
        INASN_IA_D entity = db.INASN_IA_D.Where(x => x.ids == this.KeyID).FirstOrDefault();
        if (entity != null)
        {
            this.txtIDS.Text = entity.ids;
            this.txtID.Text = entity.id;
            this.txtPO.Text = entity.pono;
            this.txtPOLine.Text = entity.poline;
            this.txtPO_IDS.Text = entity.inpo_d_ids;
            this.txtQTYTOTAL.Text = entity.qtytotal.HasValue ? entity.qtytotal.Value.ToString("f2") : "0";
            this.txtCINVCODE.Text = entity.cinvcode;
            this.txtCINVNAME.Text = entity.cinvname;
            this.txtVENDORLOTNO.Text = entity.vendorlotno;
            this.txtDateCodeType.SelectedValue = entity.datecodetype;
            this.txtDateCode.Text = entity.datecode.ToString().PadLeft(6, '0');
            this.txtOldDateCode.Text = entity.oridatecode;
            this.txtQTYPASSED.Text = entity.qtypassed.HasValue ? entity.qtypassed.Value.ToString("f2") : "0";
            this.txtQTYUNPASSED.Text = entity.qtyunpassed.HasValue ? entity.qtyunpassed.Value.ToString("f2"): "0";
            this.txtQTYPENDING.Text = entity.qtypending.HasValue ? entity.qtypending.Value.ToString("f2"): "0";
            this.txtErpCodeline.Text = entity.erpcodeline;
            this.dpdstatus.SelectedValue = entity.status.ToString();
            
            this.txtcreateuser.Text = entity.createowner;
            this.txtcreatetime.Text = entity.createtime.HasValue ? entity.createtime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtupdateuser.Text = entity.lastupdateowner;
            this.txtupdatetime.Text = entity.lastupdatetime.HasValue ? entity.lastupdatetime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtCMEMO.Text = entity.cmemo;
            this.txtLine.Text = entity.iline.ToString();

            this.txtPO.Enabled = false;
            this.txtPOLine.Enabled = false;
            this.txtCINVCODE.Enabled = false;
            this.txtCINVNAME.Enabled = false;

            string status_d = entity.status.ToString();
            INASN_IA entity1 = db.INASN_IA.Where(x=>x.id == entity.id).FirstOrDefault();
            string status = entity1.cstatus.ToString();

            if (status_d != "0" || status != "0")
            {
                this.btnSave.Enabled = false;
                this.txtPO.Enabled = false;
                this.txtPOLine.Enabled = false;
                this.txtQTYTOTAL.Enabled = false;
                this.txtCINVCODE.Enabled = false;
                this.txtCINVNAME.Enabled = false;
                this.txtVENDORLOTNO.Enabled = false;
                this.txtDateCode.Enabled = false;
                this.txtDateCodeType.Enabled = false;
                this.txtOldDateCode.Enabled = false;
            }
        }
	}

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if(this.txtIDS.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG17+ "！");//子表编号项不允许空
        	this.SetFocus(txtIDS);
        	return false;
        }
        if(this.txtIDS.Text.Trim().Length > 0)
        {
        	if(this.txtIDS.Text.GetLengthByByte() > 50)
        	{
                this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG18 + "！");//子表编号项超过指定的长度50
        		this.SetFocus(txtIDS);
        		return false;
        	}
        }
        if(this.txtID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG19 + "！");//主表编号项不允许空
        	this.SetFocus(txtID);
        	return false;
        }
        if (this.txtPO_IDS.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG20 + "！");//PO项次选择错误，请重新选择
            this.SetFocus(txtPOLine);
            return false;
        }
        if(this.txtID.Text.Trim().Length > 0)
        {
        	if(this.txtID.Text.GetLengthByByte() > 50)
        	{
                this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG21 + "！");//主表编号项超过指定的长度50
        		this.SetFocus(txtID);
        		return false;
        	}
        }
        if (this.txtPO.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG22 + "！");//PO不允许空
            this.SetFocus(txtPO);
            return false;
        }
        if (this.txtPOLine.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG23 + "！");//PO项次不允许空
            this.SetFocus(txtPO);
            return false;
        }
        if (this.txtQTYTOTAL.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG24 + "！");//总数量项不允许空
            this.SetFocus(txtPO);
            return false;
        }
        if(this.txtCINVCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG25 + "！");//料号项不允许空
            this.SetFocus(txtPO);
        	return false;
        }
        if (this.txtCINVNAME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG26 + "！");//品名项不允许空
            this.SetFocus(txtCINVNAME);
            return false;
        }
        if(this.txtCINVCODE.Text.Trim().Length > 0)
        {
        	if(this.txtCINVCODE.Text.GetLengthByByte() > 50)
        	{
                this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG27 + "！");//料号项超过指定的长度50
        		this.SetFocus(txtCINVCODE);
        		return false;
        	}
        }
        if(this.txtCINVNAME.Text.Trim().Length > 0)
        {
        	if(this.txtCINVNAME.Text.GetLengthByByte() > 300)
        	{
                this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG28 + "！");//品名项超过指定的长度300
        		this.SetFocus(txtCINVNAME);
        		return false;
        	}
        }
        if(this.txtQTYTOTAL.Text.Trim().Length > 0)
        {
            decimal quantity = 0;
        	if( !decimal.TryParse(this.txtQTYTOTAL.Text.Trim(),out quantity))
        	{
                this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG29 + "！");//总数量项不是有效的十进制数字
        		this.SetFocus(txtQTYTOTAL);
        		return false;
        	}
            //判断是否存在小数点问题
            if (txtQTYTOTAL.Text.Trim().ToDecimal() - Math.Truncate(txtQTYTOTAL.Text.Trim().ToDecimal()) > 0)
            {
                this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG30 + "！");//数量项只能为正整数，不能包含小数部分
                this.SetFocus(txtQTYTOTAL);
                return false;
            }
        }

        if (this.operation == SYSOperation.New && this.CheckLine(this.txtID.Text, txtLine.Text.Trim()))
        {
            this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG31 + "");//预入库通知单项次不能重复
            this.SetFocus(txtLine);
            return false;
        }

        #region POLine选择检查
        //检查PO+PO项次+数量+料号+品名是否正确
        DataTable tb = this.GetPoInFo(txtPO_IDS.Text, txtIDS.Text.Trim());
        if (tb != null && tb.Rows.Count>0)
        {
            string poline = tb.Rows[0]["POLINE"].ToString();
            string cinvcode = tb.Rows[0]["CINVCODE"].ToString();
            string cinvname = tb.Rows[0]["CINVNAME"].ToString();
            string totalnum = tb.Rows[0]["IQTY"].ToString();
            if (poline != txtPOLine.Text.Trim())
            {
                this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG32+ "！");//PO项次被修改,请重新选择
                this.SetFocus(txtPO);
                return false;
            }
            if (cinvcode != txtCINVCODE.Text.Trim())
            {
                this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG33 + "！");//料号被修改,请重新选择
                this.SetFocus(txtPO);
                return false;
            }
            if (cinvname != txtCINVNAME.Text.Trim())
            {
                this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG34 + "！");//品名被修改,请重新选择
                this.SetFocus(txtPO);
                return false;
            }
            if (Convert.ToDecimal(txtQTYTOTAL.Text.Trim()) > Convert.ToDecimal(totalnum))
            {
                //总数量,不能大于,请重新输入
                this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG35+ "[" + txtQTYTOTAL.Text + "]" + Resources.Lang.FrmInPo_IA_DEdit_MSG36 + "[" + totalnum + "]," + Resources.Lang.FrmInPo_IA_DEdit_MSG37 +"！");
                this.SetFocus(txtQTYTOTAL);
                return false;
            }
        } 
        #endregion
        
        if (this.txtDateCode.Text.Trim()=="")
        {
            this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG38 + "！");//DateCode项不能为空
            this.SetFocus(txtDateCode);
            return false;
        }
       
        if (this.txtDateCode.Text.Trim().Length > 0)
        {
            string msg = string.Empty;
            string vdatecode = string.Empty;
            //TODO：转成SQL版本
            if (!(Comm_Function.CheckDateCode(txtDateCode.Text.Trim(), txtDateCodeType.SelectedValue.Trim(), out vdatecode,out msg)))
            {
                this.Alert(msg);
                this.SetFocus(txtDateCode);
                return false;
            }
        }
       
        if (this.txtOldDateCode.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG39+ "！");//原始DateCode项不能为空
            this.SetFocus(txtOldDateCode);
            return false;
        }
        if(this.txtCMEMO.Text.Trim().Length > 0)
        {
        	if(this.txtCMEMO.Text.GetLengthByByte() > 20)
        	{
                this.Alert(Resources.Lang.FrmInPo_IA_DEdit_MSG40 + "！");//备注项超过指定的长度20
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
    public INASN_IA_D SendData()
    {
        INASN_IA_D entity = new INASN_IA_D();

        this.txtIDS.Text = this.txtIDS.Text.Trim();
        if(this.txtIDS.Text.Length > 0)
        {
        	entity.ids = txtIDS.Text;
        }
        
        this.txtID.Text = this.txtID.Text.Trim();
        if(this.txtID.Text.Length > 0)
        {
        	entity.id = txtID.Text;
        }
       
        this.txtPO.Text = this.txtPO.Text.Trim();
        if (this.txtPO.Text.Length > 0)
        {
            entity.pono = txtPO.Text;
        }

        this.txtPOLine.Text = this.txtPOLine.Text.Trim();
        if (this.txtPOLine.Text.Length > 0)
        {
            entity.poline = txtPOLine.Text;
        }

        this.txtQTYTOTAL.Text = this.txtQTYTOTAL.Text.Trim();
        if (this.txtQTYTOTAL.Text.Length > 0)
        {
            entity.qtytotal = txtQTYTOTAL.Text.ToDecimal();
        }

        this.txtCINVCODE.Text = this.txtCINVCODE.Text.Trim();
        if(this.txtCINVCODE.Text.Length > 0)
        {
        	entity.cinvcode = txtCINVCODE.Text;
        }

        this.txtCINVNAME.Text = this.txtCINVNAME.Text.Trim();
        if(this.txtCINVNAME.Text.Length > 0)
        {
        	entity.cinvname = txtCINVNAME.Text;
        }

        this.txtVENDORLOTNO.Text = this.txtVENDORLOTNO.Text.Trim();
        if (this.txtVENDORLOTNO.Text.Length > 0)
        {
            entity.vendorlotno = txtVENDORLOTNO.Text;
        }

        entity.datecodetype = txtDateCodeType.SelectedValue;

        this.txtDateCode.Text = this.txtDateCode.Text.Trim();
        if (this.txtDateCode.Text.Length > 0)
        {
            entity.datecode = txtDateCode.Text.ToDecimal();
        }

        this.txtOldDateCode.Text = this.txtOldDateCode.Text.Trim();
        if (this.txtOldDateCode.Text.Length > 0)
        {
            entity.oridatecode = txtOldDateCode.Text;
        }

        this.txtQTYPASSED.Text = this.txtQTYPASSED.Text.Trim();
        if (this.txtQTYPASSED.Text.Length > 0)
        {
            entity.qtypassed = txtQTYPASSED.Text.ToDecimal();
        }

        this.txtQTYUNPASSED.Text = this.txtQTYUNPASSED.Text.Trim();
        if (this.txtQTYUNPASSED.Text.Length > 0)
        {
            entity.qtyunpassed = txtQTYUNPASSED.Text.ToDecimal();
        }

        this.txtQTYPENDING.Text = this.txtQTYPENDING.Text.Trim();
        if (this.txtQTYPENDING.Text.Length > 0)
        {
            entity.qtypending = txtQTYPENDING.Text.ToDecimal();
        }

        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if(this.txtCMEMO.Text.Length > 0)
        {
        	entity.cmemo = txtCMEMO.Text;
        }
       
        entity.status = Convert.ToInt32(dpdstatus.SelectedValue);

        this.txtPO_IDS.Text = this.txtPO_IDS.Text.Trim();
        if (this.txtPO_IDS.Text.Length > 0)
        {
            entity.inpo_d_ids = txtPO_IDS.Text;
        }
        if (this.txtLine.Text.Length > 0)
        {
            entity.iline = Convert.ToInt32(txtLine.Text.Trim());
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
        this.btnSave.Enabled = false; //CQ 2013-5-13 13:47:51
       
        if (this.CheckData()) 
        {
        	INASN_IA_D entity = this.SendData(); 

        	string strKeyID="";
        	try 
        	{
        		if (this.operation == SYSOperation.Modify) 
        		{
                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.lastupdatetime = Comm_Function.GetDBDateTime();
                    using (var modContext = this.context)
                    {
                        using (var dbContextTransaction = modContext.Database.BeginTransaction())
                        {
                            try
                            {
                                strKeyID = entity.ids;
                                modContext.INASN_IA_D.Attach(entity);
                                modContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                                modContext.SaveChanges();
                                ChangePoIaStatus(modContext, txtPO_IDS.Text.Trim());
                                dbContextTransaction.Commit();
                                this.AlertAndBack("FrmInPo_IA_DEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID + "&parentId=" + txtID.Text), Resources.Lang.CommonB_SaveSuccess);
                            }
                            catch(Exception ex){
                                dbContextTransaction.Rollback();
                                this.AlertAndBack("FrmInPo_IA_DEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID + "&parentId=" + txtID.Text), Resources.Lang.CommonB_SaveFailed + ":" + ex.Message);
                            }
                        }
                    }
        		}
        		else if(this.operation == SYSOperation.New)
        		{
                    entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.createtime = Comm_Function.GetDBDateTime();
        		    entity.erpcodeline = Comm_Sys.Fun_GetNo(txtID.Text, "0", "", "");
                    strKeyID = Guid.NewGuid().ToString();
                    entity.ids = strKeyID;
                    using (var modContext = this.context)
                    {
                        using (var dbContextTransaction = modContext.Database.BeginTransaction())
                        {
                            try
                            {
                                modContext.INASN_IA_D.Add(entity);
                                modContext.SaveChanges();
                                ChangePoIaStatus(modContext, txtPO_IDS.Text.Trim());
                                dbContextTransaction.Commit();
                                this.AlertAndBack("FrmInPo_IA_DEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID + "&parentId=" + txtID.Text), Resources.Lang.CommonB_SaveSuccess);
                            }
                            catch (Exception ex) {
                                dbContextTransaction.Rollback();
                                this.AlertAndBack("FrmInPo_IA_DEdit.aspx?" + BuildQueryString(SYSOperation.New, strKeyID + "&parentId=" + txtID.Text), Resources.Lang.CommonB_SaveFailed  + ex.Message);
                            }
                        }
                    }
        		}
               
            }
        	catch (Exception E) 
        	{
                this.Alert(this.GetOperationName() + Resources.Lang.CommonB_Failed + "！" + E.Message); 
                this.btnSave.Enabled = true;
        	}
        }
        this.btnSave.Enabled = true;
    }

    /// 检查料号datecode是否超过保固期
    /// <summary>
    /// 检查料号datecode是否超过保固期
    /// </summary>
    /// <param name="P_DateCode">DateCode</param>
    /// <param name="cinvcode">料号</param>
    /// <param name="P_BZ">标识 0 </param>
    /// <param name="pReservedField1">预留1</param>
    /// <param name="pReservedField2">预留2</param>
    /// <param name="errmsg">返回错误信息</param>
    /// <returns></returns>
    public bool Fun_CheckPart_ExpDays(string P_DateCode, string cinvcode, string P_BZ, string pReservedField1,
                                             string pReservedField2, out string errmsg)
    {
        errmsg = "";
        string msg = string.Empty;
        bool boolTF = false;
        try
        {
            string Sql = string.Format(@"select dbo.Fun_CheckPart_ExpDays('{0}','{1}','{2}','{3}','{4}')",
                                       P_DateCode, cinvcode, P_BZ, "", "");
            msg = SqlDBHelp.ExecuteScalar(Sql).ToString();
            if (msg.Equals("0"))
            {
                boolTF = true;
            }
            else
            {
                errmsg = msg;
            }
        }
        catch (Exception err)
        {
            boolTF = false;
            errmsg = err.Message;
        }
        return boolTF;
    }
    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="poIds"></param>
    /// <param name="userNo"></param>
    private void ChangePoIaStatus(DBContext modContext, string poIds) {
        var inpoDetail = modContext.INPO_D.Where(x => x.ids == poIds).FirstOrDefault();
        if (inpoDetail != null) {

            var vYrkNum = modContext.INASN_IA_D.Where(x => x.inpo_d_ids == inpoDetail.ids).Sum(x => x.qtypassed);        
            if (inpoDetail.iquantity == vYrkNum)
            {
                inpoDetail.status = 2;//符合最大容量后修改为已完成
            }
            else
            {
                inpoDetail.status = 1;//采购单状态改成处理中
            }
            modContext.INPO_D.Attach(inpoDetail);
            modContext.Entry(inpoDetail).State = System.Data.Entity.EntityState.Modified;
            modContext.SaveChanges();

            var inpo = modContext.INPO.Where(x => x.id == inpoDetail.id).FirstOrDefault();
            if (inpo != null) {
                //未完成的数量
                int count = modContext.INPO_D.Where(x => x.id == inpo.id && x.status != 2).Count();
                if (count > 0)
                {
                    inpo.status = 1;//存在未完成的子项，状态改为处理中
                }
                else {
                    inpo.status = 2;//不存在未完成子项，状态改为已完成
                }
            }
            modContext.SaveChanges();
        }
    }


    public int CreateLine(string id)
    {
        int temp = 0;
        var maxIline = this.context.INASN_IA_D.Where(x => x.id == id).Max(x => x.iline);
        if (maxIline != null) {
            temp = Convert.ToInt32(maxIline) + 1;
        }
        else
        {
            temp = 1;
        }
        return temp;
    }
    /// <summary>
    /// 判断相同的预入库通知单的项次是否存在相同
    /// </summary>
    /// <param name="inia_id"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    public  bool CheckLine(string inia_id, string line)
    {
        int iline = Convert.ToInt32(line);
        return this.context.INASN_IA_D.Where(x => x.id == inia_id && x.iline == iline).Any();
    }

    public DataTable GetPoInFo(string PO_D_IDS, string asn_ia_IDS)
    {
        DataTable tb = null;
        string Sql = string.Format(@"select  pd.ids, p.pono,pd.poline,pd.cinvcode,pd.cinvname,
                                        (pd.iquantity -isnull(t.iqty,0) + isnull(t1.iaqty,0)) iqty
                                        from inpo_d pd inner join inpo p on p.id=pd.id
                                        left join (select ia.inpo_d_ids,sum(ia.qtytotal) iqty from inasn_ia_d ia group by ia.inpo_d_ids)t on t.inpo_d_ids=pd.ids
                                        left join (select ia.inpo_d_ids,sum(ia.qtytotal) iaqty from inasn_ia_d ia where ia.ids='{0}' group by ia.inpo_d_ids) t1 on t1.inpo_d_ids=pd.ids
                                        where pd.ids='{1}'", asn_ia_IDS, PO_D_IDS);
        tb = SqlDBHelp.ExecuteToDataTable(Sql);
        return tb;
    }
}

