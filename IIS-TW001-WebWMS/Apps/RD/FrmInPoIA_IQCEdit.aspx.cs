using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
using System.Data.SqlClient;
using DreamTek.ASRS.Business;

public partial class RD_FrmInPoIA_IQCEdit : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ShowPARTDiv1.SetCINVCODE = this.txtCINVCODE.ClientID;
        this.ShowPARTDiv1.SetCINVNAME = this.txtCINVNAME.ClientID;
        this.ShowPARTDiv1.GetComp = true;
        if(this.IsPostBack == false)
        {
            this.operation = this.Operation();
            this.InitPage();
            if (this.operation != SYSOperation.New)
            {
                ShowData();
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('IA_IQC');return false;";

        //状态
        Help.DropDownListDataBind(GetParametersByFlagType("IN_PO_IA_D"), dpdstatus, "", "FLAG_NAME", "FLAG_ID", "");
    }

    #endregion
   
    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
	{

        INASN_IA_D entity = context.INASN_IA_D.Where(x => x.ids == this.KeyID).FirstOrDefault();
        if (entity != null)
        {
            this.txtIDS.Text = entity.ids;
            txtID.Text = entity.id;
            txtPO.Text = entity.pono;
            txtPOLine.Text = entity.poline;
            txtVENDORLOTNO.Text = entity.vendorlotno;
            txtDateCode.Text = entity.datecode.ToString().PadLeft(6, '0');
            dpdstatus.SelectedValue = entity.status.ToString();
            txtCINVCODE.Text = entity.cinvcode;
            txtCINVNAME.Text = entity.cinvname;
            txtQTYTOTAL.Text = entity.qtytotal.HasValue ? entity.qtytotal.Value.ToString("f2"):"";
            txtQTYPASSED.Text = entity.qtypassed.HasValue ? entity.qtypassed.Value.ToString("f2"):"";
            txtQTYUNPASSED.Text = entity.qtyunpassed.HasValue ? entity.qtyunpassed.Value.ToString("f2"):"";
            txtQTYPENDING.Text = entity.qtypending.HasValue ? entity.qtypending.Value.ToString("f2"):"";
            txtQTYSAMPLING.Text = entity.qtysampling.HasValue ? entity.qtysampling.Value.ToString("f2"):"";
            txtNCRNO.Text = entity.ncrno;
            txtEVENTS.Text = entity.events;
            txtDISPOSITION.Text = entity.disposition;
            this.txtCMEMO.Text = entity.cmemo;
            txt_MFG.Text = entity.mfg;
            txt_MPN.Text = entity.mpn;
            CBox_Is_Mail.Checked = entity.is_mail == 1 ? true : false;
            if (!(entity.status == 0))
            {
                btnSave.Enabled = false;
            }
            if (txtQTYUNPASSED.Text.Trim() != "" && txtQTYPENDING.Text.Trim() != "")
            {
                if (entity.status == 0 || entity.status == 1)
                {
                    if (txtQTYUNPASSED.Text.ToDecimal() > 0 || txtQTYPENDING.Text.ToDecimal() > 0)
                    {
                        btnSendMail.Enabled = true;
                    }
                }
            }

            INASN_IA PoIA = context.INASN_IA.Where(x => x.id == txtID.Text).FirstOrDefault();
            if (PoIA != null)
            {
                txtCTICKETCODE.Text = PoIA.cticketcode;
                HidField_VendorCode.Value = PoIA.cvendercode;
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
            this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG1 + "！");//子表编号项不允许空
        	this.SetFocus(txtIDS);
        	return false;
        }
        if (this.txtIDS.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG2 + "！");//子表编号项不允许空
            this.SetFocus(txtIDS);
            return false;
        }
        if(this.txtCINVCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG3 + "！");//料号项不允许空
        	this.SetFocus(txtCINVCODE);
        	return false;
        }

        if(this.txtCINVCODE.Text.Trim().Length > 0)
        {
        	if(this.txtCINVCODE.Text.GetLengthByByte() > 20)
        	{
                this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG4 + "！");//料号项超过指定的长度20
        		this.SetFocus(txtCINVCODE);
        		return false;
        	}
        }
        if(this.txtCINVNAME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG5 + "！");//品名项不允许空
        	this.SetFocus(txtCINVNAME);
        	return false;
        }

        if(this.txtCINVNAME.Text.Trim().Length > 0)
        {
        	if(this.txtCINVNAME.Text.GetLengthByByte() > 300)
        	{
                this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG6 + "！");//品名指定的长度300
        		this.SetFocus(txtCINVNAME);
        		return false;
        	}
        }

        if(this.txtQTYPASSED.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG7 + "！");//通过数量项不允许空
            this.SetFocus(txtQTYPASSED);
        	return false;
        }

        if (this.txtQTYUNPASSED.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG8 + "！");//判退数量项不允许空
            this.SetFocus(txtQTYUNPASSED);
            return false;
        }
        if (this.txtQTYPENDING.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG9 + "！");//待检数量项不允许空
            this.SetFocus(txtQTYPENDING);
            return false;
        }

        if (this.txtQTYSAMPLING.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG10 + "！");//抽检数量项不允许空
            this.SetFocus(txtQTYSAMPLING);
            return false;
        }
        ////通过
        if (this.txtQTYPASSED.Text.Trim().Length > 0)
        {
            //检查数量，不允许小数，负数，允许为 0
            string msg = string.Empty;
            if (!(DreamTek.ASRS.Business.Others.Comm_Function.Fun_IsDecimal(txtQTYPASSED.Text.Trim(), 0,1, 1, out msg)))
            {
                this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG11 + ":" + msg);//通过数量项
                this.SetFocus(txtQTYPASSED);
                return false;
            }
        }
        ////判退
        if (this.txtQTYUNPASSED.Text.Trim().Length > 0)
        {
            //检查数量，不允许小数，负数，允许为 0
            string msg = string.Empty;
            if (!(DreamTek.ASRS.Business.Others.Comm_Function.Fun_IsDecimal(txtQTYUNPASSED.Text.Trim(), 0, 1, 1, out msg)))
            {
                this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG12 + ":" + msg);//判退数量项
                this.SetFocus(txtQTYUNPASSED);
                return false;
            }
        }
        ////待检
        if (this.txtQTYPENDING.Text.Trim().Length > 0)
        {
            //检查数量，不允许小数，负数，允许为 0
            string msg = string.Empty;
            if (!(DreamTek.ASRS.Business.Others.Comm_Function.Fun_IsDecimal(txtQTYPENDING.Text.Trim(), 0, 1, 1, out msg)))
            {
                this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG13 + ":" + msg);//待检数量项
                this.SetFocus(txtQTYPENDING);
                return false;
            }
        }
        ////抽检
        if (this.txtQTYSAMPLING.Text.Trim().Length > 0)
        {
            //检查数量，不允许小数，负数，允许为 0
            string msg = string.Empty;
            if (!(DreamTek.ASRS.Business.Others.Comm_Function.Fun_IsDecimal(txtQTYSAMPLING.Text.Trim(), 0, 1, 1, out msg)))
            {
                this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG14 + ":" + msg);//抽检数量项
                this.SetFocus(txtQTYSAMPLING);
                return false;
            }

            //抽检数量不能大于中数量
            if (txtQTYSAMPLING.Text.Trim().ToDecimal() > txtQTYTOTAL.Text.ToDecimal())
            {
                this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG15 + "");//抽检数量项不能大于总数量
                this.SetFocus(txtQTYSAMPLING);
                return false;
            }
        }

        if (this.txtQTYUNPASSED.Text.ToDecimal() + txtQTYPASSED.Text.ToDecimal() + txtQTYPENDING.Text.ToDecimal() != txtQTYTOTAL.Text.ToDecimal())
        {
            this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG16 + "");//通过数量+判退数量+待检数量 不等于 总数数量
            this.SetFocus(txtQTYPASSED);
            return false;

        }

        ////
        if (this.txt_MFG.Text.Trim()== "")
        {
            this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG17 + "！");//实物制造商(MFG)不能为空
            this.SetFocus(txt_MFG);
            return false;           
        }

        if (this.txt_MPN.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG18 + "！");//实物制造料号(MPN)不能为空
            this.SetFocus(txt_MPN);
            return false;
        }
            
        ////
        if(this.txtCMEMO.Text.Trim().Length > 0)
        {
        	if(this.txtCMEMO.Text.GetLengthByByte() > 200)
        	{
                this.Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG19 + "！");//备注项超过指定的长度200
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
    public INASN_IA_D SendData(INASN_IA_D entity)
    {
        //
        this.txtIDS.Text = this.txtIDS.Text.Trim();
        if(this.txtIDS.Text.Length > 0)
        {
        	entity.ids = txtIDS.Text;
        }
             
        //
        this.txt_MFG.Text = this.txt_MFG.Text.Trim();
        if (this.txt_MFG.Text.Length > 0)
        {
            entity.mfg = txt_MFG.Text;
        }
        
        this.txt_MPN.Text = this.txt_MPN.Text.Trim();
        if (this.txt_MPN.Text.Length > 0)
        {
            entity.mpn = txt_MPN.Text;
        }
        
        //通过
        this.txtQTYPASSED.Text = this.txtQTYPASSED.Text.Trim();
        if (this.txtQTYPASSED.Text.Length > 0)
        {
            entity.qtypassed = txtQTYPASSED.Text.ToDecimal();
        }
        
        //判退
        this.txtQTYUNPASSED.Text = this.txtQTYUNPASSED.Text.Trim();
        if (this.txtQTYUNPASSED.Text.Length > 0)
        {
            entity.qtyunpassed = txtQTYUNPASSED.Text.ToDecimal();
        }
       
        //待检
        this.txtQTYPENDING.Text = this.txtQTYPENDING.Text.Trim();
        if (this.txtQTYPENDING.Text.Length > 0)
        {
            entity.qtypending = txtQTYPENDING.Text.ToDecimal();
        }
        
        //抽检
        this.txtQTYSAMPLING.Text = this.txtQTYSAMPLING.Text.Trim();
        if (this.txtQTYSAMPLING.Text.Length > 0)
        {
            entity.qtysampling = txtQTYSAMPLING.Text.ToDecimal();
        }
        
        //不合格码
        this.txtNCRNO.Text = this.txtNCRNO.Text.Trim();
        if (this.txtNCRNO.Text.Length > 0)
        {
            entity.ncrno = txtNCRNO.Text.Trim();
        }
        
        //问题
        this.txtEVENTS.Text = this.txtEVENTS.Text.Trim();
        if (this.txtEVENTS.Text.Length > 0)
        {
            entity.events = txtEVENTS.Text.Trim();
        }
        
        //处置
        this.txtDISPOSITION.Text = this.txtDISPOSITION.Text.Trim();
        if (this.txtDISPOSITION.Text.Length > 0)
        {
            entity.disposition = txtDISPOSITION.Text.Trim();
        }
        
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if(this.txtCMEMO.Text.Length > 0)
        {
        	entity.cmemo = txtCMEMO.Text;
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
        	string strKeyID="";
        	try 
        	{
        		if (this.operation == SYSOperation.Modify)
        		{
                    using (var modContext = this.context) {
                        using (var dbContextTransaction = modContext.Database.BeginTransaction())
                        {
                            try
                            {
                                INASN_IA_D entity = modContext.INASN_IA_D.Where(x => x.ids == this.txtIDS.Text.Trim()).FirstOrDefault();
                                if (entity != null)
                                {
                                    entity = this.SendData(entity);
                                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                                    entity.lastupdatetime = DreamTek.ASRS.Business.Others.Comm_Function.GetDBDateTime();
                                    entity.status = 1;
                                    modContext.INASN_IA_D.Attach(entity);
                                    modContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                                    modContext.SaveChanges();

                                    INASN_IA IA = modContext.INASN_IA.Where(x => x.id == txtID.Text).FirstOrDefault();
                                    IA.cstatus = "2";//CQ 2015-2-12 14:29:54
                                    modContext.INASN_IA.Attach(IA);
                                    modContext.Entry(IA).State = System.Data.Entity.EntityState.Modified;
                                    modContext.SaveChanges();

                                    #region 质检
                                    // 在质检环节，按保存之后，查询数据库中所有单身的状态，取最小的状态，根据最小的状态来改变单头的状态
                                    // 单头：状态（0 未处理,1 已确认,2 质检中,3 已质检,4已完成）
                                    // 单身:0 未处理1 已质检 2已完成
                                    var str = modContext.INASN_IA_D.Where(x => x.id == txtID.Text).Min(x => x.status);
                                    var str2 = modContext.INASN_IA_D.Where(x => x.id == txtID.Text).Max(x => x.status);
                                    if (!string.IsNullOrEmpty(str.ToString()))
                                    {
                                        if (int.Parse(str.ToString()) == 0 && int.Parse(str2.ToString()) == 1)//如果单身最小状态是未处理，最大状态是已质检，，那么单身变成质检中
                                        {
                                            modContext.Database.ExecuteSqlCommand(" update  inasn_ia set cstatus='2' where id=@ID ", new SqlParameter("@ID", txtID.Text));
                                            modContext.SaveChanges();
                                        }
                                        if (int.Parse(str.ToString()) == 1)//如果单身最小状态是已质检，那么单头也变成已质检
                                        {
                                            modContext.Database.ExecuteSqlCommand(" update  inasn_ia set cstatus='3' where id=@ID ", new SqlParameter("@ID", txtID.Text));
                                            modContext.SaveChanges();
                                        }
                                    }

                                    #endregion

                                    int count = modContext.INASN_IA_D.Where(x => x.id == txtID.Text && x.status == 0).Count();
                                    if (count == 0)
                                    {
                                        IA.cstatus = "3";
                                    }
                                    modContext.INASN_IA.Attach(IA);
                                    modContext.Entry(IA).State = System.Data.Entity.EntityState.Modified;
                                    modContext.SaveChanges();
                                    dbContextTransaction.Commit();
                                }
                                else {
                                    this.Alert(Resources.Lang.CommonB_SaveFailed + "!");
                                    return;
                                }                           
                            }
                            catch (Exception ex) {
                                dbContextTransaction.Rollback();
                                this.Alert(this.GetOperationName() + Resources.Lang.CommonB_Failed + "！" + ex.Message);
                                return;
                            }
                        }
                    }
                    Send();
                    this.AlertAndBack("FrmInPoIA_IQCEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.FrmInPoIA_IQCEdit_MSG20 + "");//质检完成
        		}
        	}
        	catch (Exception E) 
        	{
                this.Alert(this.GetOperationName() + Resources.Lang.CommonB_Failed + "！" + E.Message); //失败
                this.btnSave.Enabled = true;
        	}
        }
        this.btnSave.Enabled = true;
    }

    /// <summary>
    /// 在质检环节，按保存之后，查询数据库中所有单身的状态，取最小的状态，根据最小的状态来改变单头的状态
    /// 单头：状态（0 未处理,1 已确认,2 质检中,3 已质检,4已完成）
    /// 单身:0 未处理1 已质检 2已完成
    /// </summary>
    private void INASN_IAStatusUpdate()
    {
        string id = this.txtID.Text;
        string Sql = String.Format(@"select min(d.status) from INASN_IA_D d where d.id='{0}'", id);
        string Sql2 = String.Format(@"select max(d.status) from INASN_IA_D d where d.id='{0}'", id);
        object str = SqlDBHelp.ExecuteScalar(Sql);
        object str2 = SqlDBHelp.ExecuteScalar(Sql2);
        if (!string.IsNullOrEmpty(str.ToString()))
        {
            if (int.Parse(str.ToString()) == 0 && int.Parse(str2.ToString()) == 1)//如果单身最小状态是未处理，最大状态是已质检，，那么单身变成质检中
            {
                string sql2 = String.Format(@" update  inasn_ia t set t.cstatus='2' where t.id='{0}'", id);
                SqlDBHelp.ExecuteNonQuery(sql2);

            }
            if (int.Parse(str.ToString()) == 1)//如果单身最小状态是已质检，那么单头也变成已质检
            {
                // return 3;
                string sql = String.Format(@" update  inasn_ia t set t.cstatus='3' where t.id='{0}'", id);
                SqlDBHelp.ExecuteNonQuery(sql);
            }
        }
    }

    public void Send()
    {
        try
        {
            using (var modContext = this.context)
            {
                INASN_IA_D entity = modContext.INASN_IA_D.Where(x => x.ids == txtIDS.Text.Trim()).FirstOrDefault();
                if (entity.qtypassed > 0 || entity.qtypending > 0)
                {
                    string msgmail = string.Empty;
                    string msg = string.Empty;
                    if (GetMsg(entity,out msg))
                    {
                        msgmail = msg;
                    }
                    else
                    {
                        Alert(msg);
                        return;
                    }
                    string subject = string.Empty;
                    subject = GetSubject();

                    if (SetSendMail(msgmail, subject))
                    {
                        
                        entity.is_mail = 1;
                        modContext.INASN_IA_D.Attach(entity);
                        modContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        modContext.SaveChanges();
                        Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG21 + "");//邮件发送成功
                    }
                    else
                    {
                        Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG22 + "");//邮件发送失败
                    }
                }
            }
        }
        catch (Exception err)
        {
            Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG22 + "：" + err.Message);//邮件发送失败
        }
       
    }

    /// <summary>
    /// 发送邮件方法
    /// </summary>
    /// <param name="mailcode"></param>
    /// <returns></returns>
    public bool SetSendMail(string mailcode,string subject)
    {
        bool setmai = false;
        try
        {
            //string htURL = "";
            //htURL = Request.Url.AbsoluteUri;
            //htURL = htURL.Substring(0, htURL.IndexOf("/Apps")) + "/Apps/ALLOCATE/FrmALLOCATE_Audit_Mail.aspx";
            string msgcode = "";
            msgcode = mailcode;
            string host = "";
            host = Help.ReadWebconfig("Host");//smtp主机
            int port = 25;
            port = Convert.ToInt32(Help.ReadWebconfig("Prot")); //端口
            string Fromto = "";
            Fromto = Help.ReadWebconfig("Fromcount");//发件人
            string Pwd = "";
            Pwd = Help.ReadWebconfig("Password");//密码
            string Ssl = "";
            Ssl = Help.ReadWebconfig("EnableSsl");//是否SSL加密
            bool Ss = true;//是否SSL加密
            if (Ssl == "false")
            {
                Ss = false;
            }
            else
            {
                Ss = true;
            }
            string sendto = "";
            //sendto = GetSendTo();//TODO
            string errormsg = "";
            if (SendEmail.IntoMail(host, Fromto, Pwd, Fromto, sendto, subject, msgcode, port, Ss,ref errormsg))
            {
                setmai = true;
            }

        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
        return setmai;
    }

    /// 获取收件人列表
    /// <summary>
    /// 获取收件人列表
    /// </summary>
    /// <returns></returns>
    public string GetSendTo()
    {
        string mailto = "";
        try
        {
            //Alert(txtCINVCODE.Text.Trim().Substring(0, 3));
            
            string Sql = @"SELECT D.pUserName,D.pEmail FROM view_iqc_sendmail D WHERE D.cpn='" + txtCINVCODE.Text.Trim().Substring(0,3) + "'";
            DataTable tb = SqlDBHelp.ExecuteToDataTable(Sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                for (int i = 0; i < tb.Rows.Count - 1; i++)
                {
                    mailto += tb.Rows[i]["pUserName"].ToString() + "<" + tb.Rows[i]["pEmail"].ToString() + ">;";
                }
                mailto = mailto + tb.Rows[tb.Rows.Count - 1]["pUserName"].ToString() + "<" +
                         tb.Rows[tb.Rows.Count - 1]["pEmail"].ToString() + ">";
            }
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
        return mailto;
    }

    public bool GetMsg(INASN_IA_D entity,out string MsgText)
    {
        string msg = string.Empty;
        bool boolTF = false;
        MsgText = "";
        
        try
        {
            msg = Resources.Lang.FrmInPoIA_IQCEdit_MSG23 + ": " + Resources.Lang.FrmInPoIA_IQCEdit_MSG24 + ""//Item/序号..当天发送邮件中序列号001
                  +"<br>"+ Resources.Lang.FrmInPoIA_IQCEdit_MSG25 + ": " + entity.ncrno //<br>NCR#/不合格报告号码
                   + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG26 + ": " + entity.datecode //<br>Open date/产生日期
                   + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG27 + ": " + entity.cinvcode //<br>PN/欧朗料号
                 + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG28 + ": " + entity.cinvcode.ToString().Substring(0, 3) //<br>Project/项目名
                  + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG29 + ": " + entity.mfg//<br>Supplier/供应商
                 + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG30 + ": " + entity.pono //
                 + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG31 + ": " + entity.mfg //<br>MFG/实物制造商
                  + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG32 + ": " + entity.mpn //<br>MPN/实物制造料号
                  + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG33 + ": " + entity.events//<br>Quality events/问题点
                  + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG34 + ": " + entity.qtytotal.ToString()//<br>Quantity/来料数量
                  + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG35 + ": " + entity.qtysampling.ToString()//<br>sampling/抽检数量
                 + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG36 + ": " + entity.qtyunpassed.ToString()//<br>defects/ 缺陷数量
                 + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG37 + ": IQA "//<br>spot/发生地点
                 + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG38 + ": " + entity.disposition
                  + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG39 + ""//<br>Close date/关闭日期: 此料从MRB库位处理掉的时间
                 + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG40 + ": null "//<br>Close Days处理时效
                  + "<br>" + Resources.Lang.FrmInPoIA_IQCEdit_MSG41 + @":mary.li\@eolane.cn,dean.pan\@eolane.cn"//<br>AR/责任人
                  + "<br>"+ Resources.Lang.FrmInPoIA_IQCEdit_MSG42 + ": " + entity.cmemo;//<br>备注/Remark
           
            MsgText= msg;
            boolTF = true;
        }
        catch (Exception err)
        {
            MsgText = Resources.Lang.FrmInPoIA_IQCEdit_MSG43 + ":" + err.Message;//获取发送内容错误
            boolTF = false;
        }
        return boolTF;
    }

    //获取主题
    public string GetSubject()
    {
        string msg = string.Empty;
        //MRB不良处理
        msg = DreamTek.ASRS.Business.Others.Comm_Function.GetDBDateTime("yyyy年MM月dd日") + "CPN[" + txt_MPN.Text + "] PO[" + txtPO.Text +"] "+Resources.Lang.FrmInPoIA_IQCEdit_MSG44 +"";
        return msg;
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        try
        {
            using (var modContext = this.context)
            {
                INASN_IA_D entity = modContext.INASN_IA_D.Where(x => x.ids == txtIDS.Text.Trim()).FirstOrDefault();
                if (entity.qtypassed > 0 || entity.qtypending > 0)
                {
                    string msgmail = string.Empty;
                    string msg = string.Empty;
                    if (GetMsg(entity, out msg))
                    {
                        msgmail = msg;
                    }
                    else
                    {
                        Alert(msg);
                        return;
                    }
                    string subject = string.Empty;
                    subject = GetSubject();

                    if (SetSendMail(msgmail, subject))
                    {
                        entity.is_mail = 1;
                        modContext.INASN_IA_D.Attach(entity);
                        modContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        modContext.SaveChanges();
                        Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG21 + "");//邮件发送成功
                    }
                    else
                    {
                        Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG22 + "");//邮件发送失败
                    }
                }
                else
                {
                    Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG45 + "");//没有判退数量或者待检数量，不能发送邮件
                }
            }
        }
        catch (Exception err)
        {
            Alert(Resources.Lang.FrmInPoIA_IQCEdit_MSG22 + "：" + err.Message);//邮件发送失败
        }
       
       
    }
}

