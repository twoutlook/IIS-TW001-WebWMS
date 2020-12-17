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
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Text.RegularExpressions;
using DreamTek.ASRS.Business;

/// <summary>
/// 描述: 物料详情-->FrmBASE_PARTEdit 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-09-29 15:02:32
/// </summary>
public partial class BASE_FrmBASE_PARTEdit : WMSBasePage// PageBase,IPageEdit
{

    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        ucShowWAREHOUSEDiv.SetCompName = txtCDEFAULTWARE_Name.ClientID;
        ucShowWAREHOUSEDiv.SetORGCode = hfCDEFAULTWARE_Id.ClientID;

        ucBASE_CARGOSPACEDiv.SetCompName = txtCDEFAULTCARGO_Name.ClientID;
        ucBASE_CARGOSPACEDiv.SetORGCode = hfCDEFAULTCARGO.ClientID;

        ucVENDORDiv.SetCompName = txtCDEFAULTVENDOR_Name.ClientID;
        ucVENDORDiv.SetORGCode = hfCDEFAULTVENDOR.ClientID;
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData(this.KeyID);
            }
        }
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_PART');return false;";
        txtCDEFAULTWARE_Name.Attributes["onclick"] = "Show('" + ucShowWAREHOUSEDiv.GetDivName + "');";
        txtCDEFAULTCARGO_Name.Attributes["onclick"] = "Show('" + ucBASE_CARGOSPACEDiv.GetDivName + "');";
        txtCDEFAULTVENDOR_Name.Attributes["onclick"] = "Show('" + ucVENDORDiv.GetDivName + "');";
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


        Help.DropDownListDataBind(GetParametersByFlagType("BASE_PART.CTYPE"), ddlCTYPE, "", "FLAG_NAME", "FLAG_ID", "");//类型
        Help.DropDownListDataBind(GetParametersByFlagType("ABCType"), DpD_ABCTYpe, "未设置", "FLAG_NAME", "FLAG_ID", "");//类别
        Help.DropDownListDataBind(GetParametersByFlagType("BASE_PART.CSTATUS"), ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(GetParametersByFlagType("TrueOrFalse"), drpNeedSerial, "请选择", "FLAG_NAME", "FLAG_ID", "");//是否序列号管控
        
    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData(string id)
    {

        txtCPARTNUMBER.Enabled = false;

        IGenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
        var caseList = from p in con.Get()
                       where p.id == id
                       select p;
        BASE_PART entity = caseList.ToList().FirstOrDefault<BASE_PART>();
        entity.id = this.KeyID;
        //Note by Qamar 2020-11-09
	    txtRANK_FINAL.Enabled = false;
        this.txtCPARTNUMBER.Text = entity.cpartnumber.Substring(0, entity.cpartnumber.Length - 2);
        string rankfinal = entity.cpartnumber.Substring(entity.cpartnumber.Length - 1, 1);
        if (rankfinal == "_")
            this.txtRANK_FINAL.Text = "";
        else
            this.txtRANK_FINAL.Text = rankfinal;

        this.txtCPARTNAME.Text = entity.cpartname;
        this.ddlCTYPE.SelectedValue = entity.ctype;
        this.txtCALIAS.Text = entity.calias;
        this.txtCERPCODE.Text = entity.cerpcode;
        this.txtCSPECIFICATIONS.Text = entity.cspecifications;
        this.txtICW.Text = entity.icw.ToString();
        this.txtINW.Text = entity.inw.ToString();
        this.txtCVOLUME.Text = entity.cvolume.ToString();
        this.txtCUNITS.Text = entity.cunits;
        this.cboINEEDCHECK.Checked = entity.ineedcheck == 1 ? true : false;
        this.txtCUSETYPE.Text = entity.cusetype;
        this.txtILENGTH.Text = entity.ilength.ToString();
        this.txtIWIDTH.Text = entity.iwidth.ToString();
        this.txtIHEIGHT.Text = entity.iheight.ToString();
        this.txtCSAFEQTY.Text = entity.csafeqty.HasValue ? entity.csafeqty.Value.ToString("f0") : "";
        this.txtCsafeQtyCeiling.Text = entity.csafeqtyceiling.HasValue ? entity.csafeqtyceiling.Value.ToString("f0") : "";
        this.cboINEEDWARN.Checked = entity.ineedwarn == 1 ? true : false;
        this.txtDEXPIREDATE.Text = entity.dexpiredate != null ? Convert.ToDateTime(entity.dexpiredate).ToString("yyyy-MM-dd HH:mm:ss") : "";
        //this.txtCDEFAULTWARE_Name.Text = entity.CDEFAULTWARE;
        //this.txtCDEFAULTCARGO_Name.Text = entity.CDEFAULTCARGO;
        //this.txtCDEFAULTVENDOR_Name.Text = entity.CDEFAULTVENDOR;
        if (!string.IsNullOrEmpty(entity.cdefaultware))
        {
            IGenericRepository<BASE_WAREHOUSE> wh = new GenericRepository<BASE_WAREHOUSE>(context);
            var caseListwh = from p in wh.Get()
                             orderby p.createtime descending
                             where p.id == entity.cdefaultware.Trim()
                             select p;
            if (caseListwh.Count() > 0)
            {
                this.txtCDEFAULTWARE_Name.Text = caseListwh.ToList().FirstOrDefault().cwarename;
                hfCDEFAULTWARE_Id.Value = entity.cdefaultware;
            }

        }

        if (!string.IsNullOrEmpty(entity.cdefaultcargo))
        {
            IGenericRepository<BASE_CARGOSPACE> cgp = new GenericRepository<BASE_CARGOSPACE>(context);
            var caseListcgp = from p in cgp.Get()
                              orderby p.createtime descending
                              where p.cpositioncode == entity.cdefaultcargo.Trim()
                              select p;
            if (caseListcgp.Count() > 0)
            {
                this.txtCDEFAULTCARGO_Name.Text = caseListcgp.ToList().FirstOrDefault().cposition;
                hfCDEFAULTCARGO.Value = entity.cdefaultcargo;
            }

            //BASE_CARGOSPACEEntity cgp = new BASE_CARGOSPACEEntity();

            //cgp.CPOSITIONCODE = entity.cdefaultcargo;
            //if (cgp.SelectByPropertys())
            //{
            //    this.txtCDEFAULTCARGO_Name.Text = cgp.CPOSITION;
            //    hfCDEFAULTCARGO.Value = entity.cdefaultcargo;
            //}
        }

        if (!string.IsNullOrEmpty(entity.cdefaultvendor))
        {
            IGenericRepository<BASE_VENDOR> bv = new GenericRepository<BASE_VENDOR>(context);
            var caseListbv = from p in bv.Get()
                             orderby p.createtime descending
                             where p.cvendorid == entity.cdefaultvendor.Trim()
                             select p;

            if (caseListbv.Count() > 0)
            {
                this.txtCDEFAULTVENDOR_Name.Text = caseListbv.ToList().FirstOrDefault().cvendor;
                hfCDEFAULTVENDOR.Value = entity.cdefaultvendor;
            }

            //BASE_VENDOREntity bv = new BASE_VENDOREntity();
            //bv.CVENDORID = entity.cdefaultvendor;
            //if (bv.SelectByPropertys())
            //{
            //    this.txtCDEFAULTVENDOR_Name.Text = bv.CVENDOR;
            //    hfCDEFAULTVENDOR.Value = entity.cdefaultvendor;
            //}
        }
        this.txtCINRULE.Text = entity.cinrule;
        this.txtCOUTRULE.Text = entity.coutrule;
        this.txtCBARRULE.Text = entity.cbarrule;
        this.txtCVERSION.Text = entity.cversion;
        this.ddlCSTATUS.SelectedValue = entity.cstatus;
        this.txtCMEMO.Text = entity.cmemo;
        this.txtBoxNum.Text = entity.boxnum != null ? entity.boxnum.ToString() : "0";
        this.txtID.Text = entity.id;
        this.txtproductcode.Text = entity.productcode;// pan gao 20160531
        this.CBox_Bonded.Checked = entity.bonded == 0 ? true : false;//CQ 2014-10-29 13:54:58 0保税 1非保税

        DpD_ABCTYpe.SelectedValue = entity.mtype != null ? entity.mtype.ToString() : "";

        txtcreatetime.Text = entity.createtime != null ? Convert.ToDateTime(entity.createtime).ToString("yyyy-MM-dd HH:mm:ss") : "";
        txtcreateuser.Text = entity.createowner;
        txtupdatetime.Text = entity.lastupdatetime != null ? Convert.ToDateTime(entity.lastupdatetime).ToString("yyyy-MM-dd HH:mm:ss") : "";
        txtupdateuser.Text = entity.lastupdateowner;

        drpNeedSerial.SelectedValue = entity.NeedSerial.HasValue ? entity.NeedSerial.Value.ToString() : "0";
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //
        if (this.txtCPARTNUMBER.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmMixed_DEdit_Msg01); //Resources.Lang.FrmMixed_DEdit_Msg01
            this.SetFocus(txtCPARTNUMBER);
            return false;
        }
        //
        if (this.txtCPARTNUMBER.Text.Trim().Length > 0)
        {
            if (this.txtCPARTNUMBER.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmMixed_DEdit_Msg02); //料号项超过指定的长度50！
                this.SetFocus(txtCPARTNUMBER);
                return false;
            }
            if (this.Operation() == SYSOperation.New)
            {
                //Note by Qamar 2020-11-09
                IGenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
                string partnumber = this.txtCPARTNUMBER.Text.Trim();
                if (txtRANK_FINAL.Text.Trim() == "")
                    partnumber += "-_";
                else if (txtRANK_FINAL.Text.Trim().Length == 1)
                    partnumber += "-" + txtRANK_FINAL.Text.Trim().ToUpper();
                else
                {
                    this.Alert("Rank超過指定的長度1！");
                    this.SetFocus(txtRANK_FINAL);
                    return false;
                }

                var exisBO = (from p in con.Get()
                              where p.cpartnumber == partnumber
                              select p).FirstOrDefault();
                if (exisBO != null && exisBO.id != null && exisBO.id.Length > 0)
                {
                    this.Alert(Resources.Lang.FrmBASE_PARTEdit_Msg01);//料号项已存在！
                    this.SetFocus(txtCPARTNUMBER);
                    return false;
                }
            }
        }
        //
        if (this.txtCPARTNAME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmMixed_DEdit_Msg03); //品名项不允许空！
            this.SetFocus(txtCPARTNAME);
            return false;
        }
        //
        if (this.txtCPARTNAME.Text.Trim().Length > 0)
        {
            if (this.txtCPARTNAME.Text.GetLengthByByte() > 300)
            {
                this.Alert("品名项超过指定的长度300！");
                this.SetFocus(txtCPARTNAME);
                return false;
            }
        }
        //
        if (this.txtCALIAS.Text.Trim().Length > 0)
        {
            if (this.txtCALIAS.Text.GetLengthByByte() > 200)
            {
                this.Alert("助记码项超过指定的长度200！");
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
        if (this.txtCSPECIFICATIONS.Text.Trim().Length > 0)
        {
            if (this.txtCSPECIFICATIONS.Text.GetLengthByByte() > 200)
            {
                this.Alert("规格项超过指定的长度200！");
                this.SetFocus(txtCSPECIFICATIONS);
                return false;
            }
        }
        //
        if (this.txtICW.Text.Trim().Length > 0)
        {
            if (StringExtension.IsValidNum(this.txtICW.Text))
            {
                this.Alert("毛重项不是有效的十进制数字！");
                this.SetFocus(txtICW);
                return false;
            }
        }
        //
        if (this.txtINW.Text.Trim().Length > 0)
        {
            if (StringExtension.IsValidNum(this.txtINW.Text))
            {
                this.Alert("净重项不是有效的十进制数字！");
                this.SetFocus(txtINW);
                return false;
            }
        }
        //
        if (this.txtCVOLUME.Text.Trim().Length > 0)
        {
            if (StringExtension.IsValidNum(this.txtCVOLUME.Text))
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg15);//体积项不是有效的十进制数字！
                this.SetFocus(txtCVOLUME);
                return false;
            }
        }
        //
        if (this.txtCUNITS.Text.Trim().Length > 0)
        {
            if (this.txtCUNITS.Text.GetLengthByByte() > 20)
            {
                this.Alert("重量单位项超过指定的长度20！");
                this.SetFocus(txtCUNITS);
                return false;
            }
        }
        //
        if (this.txtCUSETYPE.Text.Trim().Length > 0)
        {
            if (this.txtCUSETYPE.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg16); //用途项超过指定的长度50！
                this.SetFocus(txtCUSETYPE);
                return false;
            }
        }
        //
        if (this.txtILENGTH.Text.Trim().Length > 0)
        {
            if (!Regex.IsMatch(this.txtILENGTH.Text, "^[0-9]+$"))
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg12);//长项不是有效的十进制数字！
                this.SetFocus(txtILENGTH);
                return false;
            }
        }
        //
        if (this.txtIWIDTH.Text.Trim().Length > 0)
        {
            if (!Regex.IsMatch(this.txtIWIDTH.Text, "^[0-9]+$"))
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg13);//宽项不是有效的十进制数字！
                this.SetFocus(txtIWIDTH);
                return false;
            }
        }
        //
        if (this.txtIHEIGHT.Text.Trim().Length > 0)
        {
            if (!Regex.IsMatch(this.txtIHEIGHT.Text, "^[0-9]+$"))
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg14);//高项不是有效的十进制数字！
                this.SetFocus(txtIHEIGHT);
                return false;
            }
        }
        //
        if (this.txtCSAFEQTY.Text.Trim().Length > 0)
        {
            if (!Regex.IsMatch(this.txtCSAFEQTY.Text, "^[0-9]+$"))
            {
                this.Alert("安全库存下限项不是有效的十进制数字！");//
                this.SetFocus(txtCSAFEQTY);
                return false;
            }
        }
        if (this.txtCsafeQtyCeiling.Text.Trim().Length > 0)
        {
            if (!Regex.IsMatch(this.txtCsafeQtyCeiling.Text, "^[0-9]+$"))
            {
                this.Alert("安全库存上限项不是有效的十进制数字！");//
                this.SetFocus(txtCsafeQtyCeiling);
                return false;
            }
            else {
                if (this.txtCSAFEQTY.Text.Trim().Length > 0) {
                    var csafeqtylimit = Convert.ToDecimal(this.txtCSAFEQTY.Text.Trim());
                    var csafeqtyceiling = Convert.ToDecimal(this.txtCsafeQtyCeiling.Text.Trim());
                    if (csafeqtyceiling <= csafeqtylimit) {
                        this.Alert("安全库存上限需要大于安全库存下限！");//
                        this.SetFocus(txtCsafeQtyCeiling);
                        return false;
                    }
                }
            }
        }

        //
        //if(this.txtDEXPIREDATE.Text.Trim().Length > 0)
        //{
        //    if(this.txtDEXPIREDATE.Text.IsDate("yyyy-MM-dd HH:mm:ss")== false)
        //    {
        //        this.Alert("终止日期项不是有效的日期！");
        //        this.SetFocus(txtDEXPIREDATE);
        //        return false;
        //    }
        //}
        //
        if (this.txtCDEFAULTWARE_Name.Text.Trim().Length > 0)
        {
            if (this.txtCDEFAULTWARE_Name.Text.GetLengthByByte() > 20)
            {
                this.Alert("默认仓库项超过指定的长度20！");
                this.SetFocus(txtCDEFAULTWARE_Name);
                return false;
            }
        }
        //
        if (this.txtCDEFAULTCARGO_Name.Text.Trim().Length > 0)
        {
            if (this.txtCDEFAULTCARGO_Name.Text.GetLengthByByte() > 20)
            {
                this.Alert("默认储位项超过指定的长度20！");
                this.SetFocus(txtCDEFAULTCARGO_Name);
                return false;
            }
        }
        //
        //if (this.txtCDEFAULTVENDOR_Name.Text.Trim().Length > 0)
        //{
        //    if (this.txtCDEFAULTVENDOR_Name.Text.GetLengthByByte() > 20)
        //    {
        //        this.Alert("默认供应商项超过指定的长度20！");
        //        this.SetFocus(txtCDEFAULTVENDOR_Name);
        //        return false;
        //    }
        //}
        //
        if (this.txtCINRULE.Text.Trim().Length > 0)
        {
            if (this.txtCINRULE.Text.GetLengthByByte() > 1000)
            {
                this.Alert("上架规则项超过指定的长度1000！");
                this.SetFocus(txtCINRULE);
                return false;
            }
        }
        //
        if (this.txtCOUTRULE.Text.Trim().Length > 0)
        {
            if (this.txtCOUTRULE.Text.GetLengthByByte() > 1000)
            {
                this.Alert("下架规则项超过指定的长度1000！");
                this.SetFocus(txtCOUTRULE);
                return false;
            }
        }
        //
        if (this.txtCBARRULE.Text.Trim().Length > 0)
        {
            if (this.txtCBARRULE.Text.GetLengthByByte() > 1000)
            {
                this.Alert("条码规则项超过指定的长度1000！");
                this.SetFocus(txtCBARRULE);
                return false;
            }
        }
        //
        if (this.txtCVERSION.Text.Trim().Length > 0)
        {
            if (this.txtCVERSION.Text.GetLengthByByte() > 20)
            {
                this.Alert("版本项超过指定的长度20！");
                this.SetFocus(txtCVERSION);
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

        if (this.txtBoxNum.Text.Trim().Length > 0)
        {
            if (StringExtension.IsValidNum(this.txtBoxNum.Text))
            {
                this.Alert("包装数量项不是有效的十进制数字！");
                this.SetFocus(txtBoxNum);
                return false;
            }
            if (txtBoxNum.Text.Trim().ToDecimal() - Math.Truncate(txtBoxNum.Text.Trim().ToDecimal()) > 0)
            {
                this.Alert("包装数量项只能为正整数，不能包含小数部分！");
                this.SetFocus(txtBoxNum);
                return false;
            }

            if (Convert.ToDecimal(this.txtBoxNum.Text.Trim()) <= 0)
            {
                this.Alert("包装数量要大于0！");
                this.SetFocus(txtBoxNum);
                return false;
            }
        }

        if (this.drpNeedSerial.SelectedValue.Trim().Length == 0) {
            this.Alert("请选择是否需要序列号管控！");
            this.SetFocus(drpNeedSerial);
            return false;
        }

        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_PART SendData()
    {
        BASE_PART entity = new BASE_PART();

        //

        this.txtCPARTNUMBER.Text = this.txtCPARTNUMBER.Text.Trim();
        if (this.txtCPARTNUMBER.Text.Length > 0)
        {
            //entity.cpartnumber = txtCPARTNUMBER.Text;
            //Note by Qamar 2020-11-09
            IGenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
            string partnumber = this.txtCPARTNUMBER.Text.Trim();
            if (txtRANK_FINAL.Text.Trim() == "")
                partnumber += "-_";
            else if (txtRANK_FINAL.Text.Trim().Length == 1)
                partnumber += "-" + txtRANK_FINAL.Text.Trim().ToUpper();
            entity.cpartnumber = partnumber;
        }
        else
        {
            entity.cpartnumber = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CPARTNUMBER = null;
        }
        //
        this.txtCPARTNAME.Text = this.txtCPARTNAME.Text.Trim();
        if (this.txtCPARTNAME.Text.Length > 0)
        {
            entity.cpartname = txtCPARTNAME.Text;
        }
        else
        {
            entity.cpartname = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CPARTNAME = null;
        }
        entity.ctype = ddlCTYPE.SelectedValue;
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
            entity.cerpcode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CERPCODE = null;
        }
        //
        this.txtCSPECIFICATIONS.Text = this.txtCSPECIFICATIONS.Text.Trim();
        if (this.txtCSPECIFICATIONS.Text.Length > 0)
        {
            entity.cspecifications = txtCSPECIFICATIONS.Text;
        }
        else
        {
            entity.cspecifications = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSPECIFICATIONS = null;
        }
        //
        this.txtICW.Text = this.txtICW.Text.Trim();
        if (this.txtICW.Text.Length > 0)
        {
            entity.icw = txtICW.Text.ToDecimal();
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.ICW = null;
        }
        //
        this.txtINW.Text = this.txtINW.Text.Trim();
        if (this.txtINW.Text.Length > 0)
        {
            entity.inw = txtINW.Text.ToDecimal();
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.INW = null;
        }
        //
        this.txtCVOLUME.Text = this.txtCVOLUME.Text.Trim();
        if (this.txtCVOLUME.Text.Length > 0)
        {
            entity.cvolume = txtCVOLUME.Text.ToDecimal();
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CVOLUME = null;
        }
        //
        this.txtCUNITS.Text = this.txtCUNITS.Text.Trim();
        if (this.txtCUNITS.Text.Length > 0)
        {
            entity.cunits = txtCUNITS.Text;
        }
        else
        {
            entity.cunits = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CUNITS = null;
        }
        entity.ineedcheck = cboINEEDCHECK.Checked ? 1 : 0;


        entity.bonded = CBox_Bonded.Checked ? 0 : 1;//选择 0 保税 1 非保税

        DpD_ABCTYpe.SelectedValue = DpD_ABCTYpe.SelectedValue.Trim();
        if (DpD_ABCTYpe.SelectedValue.Length > 0)
        {
            entity.mtype = Convert.ToDecimal(DpD_ABCTYpe.SelectedValue);
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CUNITS = null;
        }

        //
        this.txtCUSETYPE.Text = this.txtCUSETYPE.Text.Trim();
        if (this.txtCUSETYPE.Text.Length > 0)
        {
            entity.cusetype = txtCUSETYPE.Text;
        }
        else
        {
            entity.cusetype = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CUSETYPE = null;
        }
        //
        this.txtILENGTH.Text = this.txtILENGTH.Text.Trim();
        if (this.txtILENGTH.Text.Length > 0)
        {
            entity.ilength = txtILENGTH.Text.ToDecimal();
        }
        else
        {

            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.ILENGTH = null;
        }
        //
        this.txtIWIDTH.Text = this.txtIWIDTH.Text.Trim();
        if (this.txtIWIDTH.Text.Length > 0)
        {
            entity.iwidth = txtIWIDTH.Text.ToDecimal();
        }
        else
        {

            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.IWIDTH = null;
        }
        //
        this.txtIHEIGHT.Text = this.txtIHEIGHT.Text.Trim();
        if (this.txtIHEIGHT.Text.Length > 0)
        {
            entity.iheight = txtIHEIGHT.Text.ToDecimal();
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.IHEIGHT = null;
        }
        //
        this.txtCSAFEQTY.Text = this.txtCSAFEQTY.Text.Trim();
        if (this.txtCSAFEQTY.Text.Length > 0)
        {
            entity.csafeqty = txtCSAFEQTY.Text.ToDecimal();
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSAFEQTY = null;
        }

        this.txtCsafeQtyCeiling.Text = this.txtCsafeQtyCeiling.Text.Trim();
        if (this.txtCsafeQtyCeiling.Text.Length > 0)
        {
            entity.csafeqtyceiling = txtCsafeQtyCeiling.Text.ToDecimal();
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSAFEQTY = null;
        }

        entity.ineedwarn = cboINEEDWARN.Checked ? 1 : 0;
        //
        this.txtDEXPIREDATE.Text = this.txtDEXPIREDATE.Text.Trim();
        if (this.txtDEXPIREDATE.Text.Length > 0)
        {
            if (this.txtDEXPIREDATE.Text.Length == 19)
            {
                entity.dexpiredate = Convert.ToDateTime(txtDEXPIREDATE.Text.Trim());
            }
            else
            {
                entity.dexpiredate = Convert.ToDateTime(txtDEXPIREDATE.Text.Trim() + " " + Help.DateTime_ToChar(DateTime.Now.Hour) + ":" + Help.DateTime_ToChar(DateTime.Now.Minute) + ":" + Help.DateTime_ToChar(DateTime.Now.Second));
            }
            if (this.txtDEXPIREDATE.Text.Length == 19)
            {

            }
            //entity.DEXPIREDATE = txtDEXPIREDATE.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.DEXPIREDATE = null;
        }
        //
        this.txtCDEFAULTWARE_Name.Text = this.txtCDEFAULTWARE_Name.Text.Trim();
        if (hfCDEFAULTWARE_Id.Value.Trim().Length > 0)
        {
            entity.cdefaultware = hfCDEFAULTWARE_Id.Value;
        }
        else
        {
            entity.cdefaultware = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CDEFAULTWARE = null;
        }
        //
        //this.txtCDEFAULTCARGO.Text = this.txtCDEFAULTCARGO.Text.Trim();
        if (hfCDEFAULTCARGO.Value.Trim().Length > 0)
        {
            entity.cdefaultcargo = hfCDEFAULTCARGO.Value.Trim();
        }
        else
        {
            entity.cdefaultcargo = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CDEFAULTCARGO = null;
        }
        //
        //this.txtCDEFAULTVENDOR.Text = this.txtCDEFAULTVENDOR.Text.Trim();
        if (hfCDEFAULTVENDOR.Value.Trim().Length > 0)
        {
            entity.cdefaultvendor = hfCDEFAULTVENDOR.Value.Trim();
        }
        else
        {
            entity.cdefaultvendor = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CDEFAULTVENDOR = null;
        }
        //
        this.txtCINRULE.Text = this.txtCINRULE.Text.Trim();
        if (this.txtCINRULE.Text.Length > 0)
        {
            entity.cinrule = txtCINRULE.Text;
        }
        else
        {
            entity.cinrule = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CINRULE = null;
        }
        //
        this.txtCOUTRULE.Text = this.txtCOUTRULE.Text.Trim();
        if (this.txtCOUTRULE.Text.Length > 0)
        {
            entity.coutrule = txtCOUTRULE.Text;
        }
        else
        {
            entity.coutrule = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.COUTRULE = null;
        }
        //PAN GAO 20160531
        this.txtproductcode.Text = this.txtproductcode.Text.Trim();
        if (this.txtproductcode.Text.Length > 0)
        {
            entity.productcode = txtproductcode.Text;
        }
        else
        {
            entity.productcode = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CBARRULE = null;
        }
        //
        this.txtCVERSION.Text = this.txtCVERSION.Text.Trim();
        if (this.txtCVERSION.Text.Length > 0)
        {
            entity.cversion = txtCVERSION.Text;
        }
        else
        {
            entity.cversion = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CVERSION = null;
        }
        entity.cstatus = ddlCSTATUS.SelectedValue;
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        else
        {
            entity.cmemo = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CMEMO = null;
        }
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        else
        {
            entity.cmemo = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CMEMO = null;
        }

        this.txtBoxNum.Text = this.txtBoxNum.Text.Trim();
        if (this.txtBoxNum.Text.Length > 0)
        {
            entity.boxnum = txtBoxNum.Text.ToDecimal();
        }
        else
        {
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            entity.boxnum = decimal.Zero;
        }

        this.txtCBARRULE.Text = this.txtCBARRULE.Text.Trim();
        if (this.txtCBARRULE.Text.Length > 0)
        {
            entity.cbarrule = txtCBARRULE.Text;
        }
        else
        {
            entity.cbarrule = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.COUTRULE = null;
        }
        if(!string.IsNullOrEmpty(drpNeedSerial.SelectedValue)){
            entity.NeedSerial = Convert.ToInt32(drpNeedSerial.SelectedValue);
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
        IGenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
        if (this.CheckData())
        {
            BASE_PART entity = this.SendData();
            if (this.Operation() != SYSOperation.New)
            {

            }
            else
            {
                //Note by Qamar 2020-11-09
                string partnumber = this.txtCPARTNUMBER.Text.Trim();
                if (txtRANK_FINAL.Text.Trim() == "")
                    partnumber += "-_";
                else if (txtRANK_FINAL.Text.Trim().Length == 1)
                    partnumber += "-" + txtRANK_FINAL.Text.Trim().ToUpper();
                IGenericRepository<BASE_PART> CheckPARTNUMBER = new GenericRepository<BASE_PART>(context);
                var caseList = from p in CheckPARTNUMBER.Get()
                               where p.cpartnumber == partnumber
                               select p;
                if (caseList.Count() > 0)
                {
                    this.Alert("此料号已经存在，禁止新增！");
                    this.SetFocus(txtCPARTNUMBER);
                    return;
                }
            }
            string strKeyID = "";

            try
            {
                if (this.Operation() == SYSOperation.Modify)
                {
                    strKeyID = txtID.Text.Trim();
                    entity.id = strKeyID;
                    entity.createtime = Convert.ToDateTime(txtcreatetime.Text.Trim());
                    entity.createowner = txtcreateuser.Text.Trim();
                    entity.lastupdatetime = DateTime.Now;
                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Update(entity);
                    con.Save();
                    this.AlertAndBack("FrmBASE_PARTEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                }
                /*
                else if(this.Operation == SysOperation.Apply)
                {
                    BASE_PARTRule.Apply(entity);
                    this.AlertAndBack("FrmBASE_PARTEdit.aspx?" + BuildQueryString(SysOperation.View, strKeyID),"申报成功"); 
                }
                else if(this.Operation == SysOperation.Audit)
                {
                    BASE_PARTRule.Audit(entity);
                    this.AlertAndBack("FrmBASE_PARTEdit.aspx?" + BuildQueryString(SysOperation.View, strKeyID),"审批成功"); 
                }
                */
                else if (this.Operation() == SYSOperation.New)
                {
                    entity.createtime = DateTime.Now;
                    strKeyID = Guid.NewGuid().ToString();
                    entity.id = strKeyID;
                    entity.createtime = DateTime.Now;
                    entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.lastupdatetime = DateTime.Now;
                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Insert(entity);
                    con.Save();
                    //默认增加物料区域关联关系
                    string areaCode = ConfigurationManager.AppSettings["AREA"].ToString();
                    string areaId = CommonHelp.GetAreaId(areaCode);
                    //物料区域关联
                    CommonHelp.Insert_PartArea(areaId, entity.cpartnumber, entity.createowner, 0);
                    this.AlertAndBack("FrmBASE_PARTEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
        try
        {
            String Id = this.KeyID.ToString();
            con.Delete(Id);
            con.Save();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_DelFail + E.Message); //删除失败！this.Alert("删除失败！" + E.Message);
#if Debug 
            this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
        }

    }
    #endregion
}

