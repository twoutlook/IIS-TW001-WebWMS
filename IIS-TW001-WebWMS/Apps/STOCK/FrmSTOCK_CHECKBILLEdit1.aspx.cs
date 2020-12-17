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
using DreamTek.ASRS.Business;
using System.Linq.Dynamic;

/// <summary>
/// 描述: 盘点单-->FrmSTOCK_CHECKBILLEdit 页面后台类文件
/// 作者:
/// 创建于: 2012-10-17 16:02:04this.txtCTICKETCODE.Text = new InAsn()
/// </summary>
public partial class STOCK_FrmSTOCK_CHECKBILLEdit1 : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.grdSTOCK_CHECKBILL_D.Columns[1].Visible = false;
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() == SYSOperation.Modify)
            {
                ShowData();
            }
            else if (this.Operation() == SYSOperation.Preserved1)
            {
                ShowData();
                this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("Frm_SELECTSTOCKINFO.aspx", SYSOperation.New, "") + "&IDH=" + this.KeyID + "&WorkType=" + drpWorkType.SelectedValue + "&Flag=1','" + Resources.Lang.FrmSTOCK_CHECKBILLEdit_PanDainDan + "','Frm_SELECTSTOCKINFO_D');");//盘点单
            }
            else
            {
                this.txtCCREATEOWNERCODE.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                this.txtID.Text = Guid.NewGuid().ToString();
                this.txtDCHECKDATE.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                btnDelete0.Enabled = false;
                this.txtCTICKETCODE.Text = new InAsn().CreateNo("STOCK_CHECKBILL");
                this.ddlLevel.SelectedValue = "2";
                ddlLevel_SelectedIndexChanged(null,null);
                //20200511 配置循环盘点：每日最大盘点料号数;根据配置来显示默认数量【begin】
                var config = context.SYS_CONFIG.Where(x => x.code == "120006").FirstOrDefault();
                string configVal = "";
                if (config != null && !string.IsNullOrEmpty(config.config_value))
                {
                    configVal = config.config_value.ToString();
                }             
                this.txtMAX_PART_IUM.Text = configVal;
                //20200511 配置循环盘点：每日最大盘点料号数;根据配置来显示默认数量【end】
            }
            drpWorkType_SelectedIndexChanged(null, null);            
        }
        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
        this.btnDelete0.Attributes["onclick"] = this.GetPostBackEventReference(this.btnDelete0) + ";this.disabled=true;";
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
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('STOCK_CHECKBILL');return false;";
        this.grdSTOCK_CHECKBILL_D.DataKeyNames = new string[] { "IDS" };
        //绑定楼层数据
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "Level", false, -1, -1), this.ddlLevel, "", "FLAG_NAME", "FLAG_ID", "");
        
        var modConfig = db.SYS_CONFIG.Where(x => x.code == "140200").FirstOrDefault(); //是否启用指定站点出库
        if (modConfig.config_value == "1")
        {
            //绑定台车下拉框
            Help.DropDownListDataBind(GetCARList(this.ddlLevel.SelectedValue.Trim()), ddlCAR, Resources.Lang.Common_ALL, "CRANENAME", "CRANEID", "");//台车
            ddlCAR.Visible = true;
            lblTaiChe.Visible = true;
            Span1.Visible = true;
        }
        else
        {
            ddlLevel.Items.Add(new ListItem(Resources.Lang.Common_ALL, ""));
            ddlCAR.Visible = false;
            lblTaiChe.Visible = false;
            Span1.Visible = false;
        }
       
        Help.DropDownListDataBind(GetParametersByFlagType("CHECKTYPE"), txtCHECKTYPE, "", "FLAG_NAME", "FLAG_ID", "");//盘点类型
        txtCHECKTYPE.SelectedValue = "1";
        var list = GetParametersByFlagType("CHECKSTATE");
        var item2 = list.Where(x => x.FLAG_ID == "2").FirstOrDefault();
        if (item2 != null)
        {
            list.Remove(item2);
        }
        var item3 = list.Where(x => x.FLAG_ID == "3").FirstOrDefault();
        if (item3 != null)
        {
            list.Remove(item3);
        }
        var item4 = list.Where(x => x.FLAG_ID == "4").FirstOrDefault();
        if (item4 != null)
        {
            list.Remove(item4);
        }
        Help.DropDownListDataBind(list, dpdCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(GetParametersByFlagType("WareHouseType"), drpWorkType, "", "FLAG_NAME", "FLAG_ID", "");//作业方式
        
        //删除确认提示
        if (this.Operation() == SYSOperation.Modify)
        {
            this.btnDelete.Attributes["onclick"] = "var userNo = '" + this.KeyID + "'; return window.confirm('" + Resources.Lang.FrmSTOCK_CHECKBILLEdit_YaoDelete + "' + userNo + '?');";//要删除
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
            this.btnSave.Text = Resources.Lang.FrmOUTASNEdit_ShengPi;//审批
        }
        drpWorkType.SelectedValue = "1";
      

    }

    #endregion
    #region IPageGrid 成员

    public void GridBind()
    {
        Bind("");
    }

    public IQueryable<STOCK_CHECKBILL_D> GetQueryList()
    {
        IGenericRepository<STOCK_CHECKBILL_D> conn = new GenericRepository<STOCK_CHECKBILL_D>(db);
        var caseList = from p in conn.Get()
                       orderby p.cinvcode descending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtID.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.Contains(txtID.Text.Trim()));
            }

        }
        return caseList;
    }

    public void Bind(string sortStr)
    {
        var caseList = GetQueryList();

        if (caseList != null && caseList.Count() > 0)
        {
            //按照列名排序
            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;


            var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
            var source = from p in data
                         join oper in db.BASE_PART on p.cinvcode equals oper.cpartnumber into temp
                         from tt in temp.DefaultIfEmpty()
                         select new
                         {
                             p.ids
                            ,
                             p.id
                            ,
                             p.cstatus
                            ,
                             p.iquantity
                            ,
                             p.cpositioncode
                            ,
                             p.cposition
                            ,
                             p.cinvcode
                            ,
                             p.cinvname
                            ,
                             p.cerpcode
                            ,
                             p.cerpcodeline
                            ,
                             p.cmemo
                            ,
                             p.cdefine1
                            ,
                             p.cdefine2
                            ,
                             p.ddefine3
                            ,
                             p.ddefine4
                            ,
                             p.idefine5
                            ,
                             p.asrs_status
                            ,
                             p.wmstskid
                            ,
                             p.palletcode
                             ,
                             cspecifications = tt == null ? "" : tt.cspecifications
                         };

            grdSTOCK_CHECKBILL_D.DataSource = source.ToList();
            grdSTOCK_CHECKBILL_D.DataBind();
        }
        else
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = grdSTOCK_CHECKBILL_D.PageSize;
            grdSTOCK_CHECKBILL_D.DataSource = null;
            grdSTOCK_CHECKBILL_D.DataBind();
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }




    #endregion
    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);
        STOCK_CHECKBILL entity = new STOCK_CHECKBILL();
        entity = (from p in conn.Get()
                  where p.id == this.KeyID
                  select p
                                  ).FirstOrDefault();
        if (entity != null && !entity.id.IsNullOrEmpty())
        {
            this.txtID.Text = entity.id.ToString();
            this.txtCTICKETCODE.Text = entity.cticketcode;
            this.txtCCHECKNAME.Text = entity.ccheckname;
            this.txtDCHECKDATE.Text = entity.dcheckdate.ToString("yyyy-MM-dd");
            this.txtCERPCODE.Text = entity.cerpcode;
            this.txtDCIRCLECHECKBEGINDATE.Text = entity.dcirclecheckbegindate != null ? entity.dcirclecheckbegindate.Value.ToString("yyyy-MM-dd") : "";
            this.txtDCIRCLECHECKENDDATE.Text = entity.dcirclecheckenddate != null ? entity.dcirclecheckenddate.Value.ToString("yyyy-MM-dd") : "";
            txtCHECKTYPE.SelectedValue = entity.checktype;
            this.txtDCREATETIME.Text = entity.dcreatetime != null ? entity.dcreatetime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtCCREATEOWNERCODE.Text = entity.ccreateownercode;
            dpdCSTATUS.SelectedValue = entity.cstatus;
            this.txtDAUDITTIME.Text = entity.daudittime != null ? entity.daudittime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtCAUDITPERSON.Text = entity.cauditperson;
            this.txtID.Text = entity.id;
            this.txtCMEMO.Text = entity.cmemo;
            this.txtMAX_PART_IUM.Text = entity.max_part_ium.HasValue ? decimal.Parse(entity.max_part_ium.ToString()).ToString("#0.00") : "";
            this.drpWorkType.SelectedValue = entity.worktype;
            if (!string.IsNullOrEmpty(entity.lineid))
            {
                this.ddlLevel.SelectedValue = entity.lineid;
                ddlLevel_SelectedIndexChanged(null,null);
            }
            if (string.IsNullOrEmpty(entity.cdefine1))
            {
                this.chkISCHECKBILL.Checked = entity.cdefine1.ToBoolean();
            }
            if (!string.IsNullOrEmpty(entity.cdefine2))
            {
                this.ddlCAR.SelectedValue = entity.cdefine2;
            }
            if (entity.cstatus != "0")
            {
                this.btnSave.Enabled = false;
                this.btnDelete0.Enabled = false;
                this.btnNew.Enabled = false;
                this.txtDCHECKDATE.Enabled = false;
                imgDCHECKDATE.Attributes["onclick"] = "";
                txtCERPCODE.Enabled = false;
                txtCCHECKNAME.Enabled = false;
                txtDCIRCLECHECKBEGINDATE.Enabled = false;
                imgDCIRCLECHECKBEGINDATE.Attributes["onclick"] = "";
                txtDCIRCLECHECKENDDATE.Enabled = false;
                imgDCIRCLECHECKENDDATE.Attributes["onclick"] = "";
                txtMAX_PART_IUM.Enabled = false;
                chkISCHECKBILL.Enabled = false;
                txtCMEMO.Enabled = false;
                drpWorkType.Enabled = false;
            }
            if (entity.cstatus == "1")
            {
                this.txtDCHECKDATE.Enabled = false;
                imgDCHECKDATE.Attributes["onclick"] = "";
                txtCERPCODE.Enabled = false;
                txtCCHECKNAME.Enabled = false;
                txtDCIRCLECHECKBEGINDATE.Enabled = false;
                imgDCIRCLECHECKBEGINDATE.Attributes["onclick"] = "";
                txtDCIRCLECHECKENDDATE.Enabled = false;
                imgDCIRCLECHECKENDDATE.Attributes["onclick"] = "";
                txtMAX_PART_IUM.Enabled = false;
                chkISCHECKBILL.Enabled = false;
                txtCMEMO.Enabled = false;
                drpWorkType.Enabled = false;
            }
            btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //
        if (this.txtCTICKETCODE.Text.Trim().Length > 0)
        {
            if (this.txtCTICKETCODE.Text.GetLengthByByte() > 40)
            {
                this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit1_CticketcodeLength);//单据号项超过指定的长度40！
                this.SetFocus(txtCTICKETCODE);
                return false;
            }
        }
        //
        if (this.txtDCHECKDATE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_NeedPanDianDate);//盘点日期项不允许空！
            this.SetFocus(txtDCHECKDATE);
            return false;
        }
        //
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
            if (this.txtCERPCODE.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_ERPCodeLength);//ERP单号项超过指定的长度30！
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        if (txtCCHECKNAME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_NeedPanDianName);//盘点单名称项不能为空！
            this.SetFocus(txtCCHECKNAME);
            return false;
        }
        if (txtDCIRCLECHECKBEGINDATE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit1_NeedBeginDate);//循环盘点开始日期项不能为空！
            this.SetFocus(txtDCIRCLECHECKBEGINDATE);
            return false;
        }
        //
        if (txtDCIRCLECHECKENDDATE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit1_NeedEndDate);//循环盘点結束日期项不能为空！
            this.SetFocus(txtDCIRCLECHECKENDDATE);
            return false;
        }
        if (this.txtDCIRCLECHECKBEGINDATE.Text.Trim().Length > 0)
        {
            if (!(this.txtDCIRCLECHECKBEGINDATE.Text.IsDate("yyyy-MM-dd HH:mm:ss") || this.txtDCIRCLECHECKBEGINDATE.Text.IsDate("yyyy-MM-dd")))
            {
                this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit1_BeginDateError);//循环盘点开始日期项不是有效的日期！
                this.SetFocus(txtDCIRCLECHECKBEGINDATE);
                return false;
            }
        }

        if (this.txtDCIRCLECHECKENDDATE.Text.Trim().Length > 0)
        {
            if (!(this.txtDCIRCLECHECKENDDATE.Text.IsDate("yyyy-MM-dd HH:mm:ss") || this.txtDCIRCLECHECKENDDATE.Text.IsDate("yyyy-MM-dd")))
            {
                this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit1_EndDateError);//循环盘点结束日期项不是有效的日期！
                this.SetFocus(txtDCIRCLECHECKENDDATE);
                return false;
            }
            DateTime dateToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            if (Convert.ToDateTime(this.txtDCIRCLECHECKENDDATE.Text.Trim()) < dateToday)
            {
                this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit1_DateBuNengZaoCurrent);//循环盘点结束日期项不能早于当前日期！
                this.SetFocus(txtDCIRCLECHECKBEGINDATE);
                return false;
            }
        }

        if (Convert.ToDateTime(txtDCIRCLECHECKBEGINDATE.Text.Trim()) >
            Convert.ToDateTime(txtDCIRCLECHECKENDDATE.Text.Trim()))
        {
            this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit1_DateBuNengZao);//循环盘点结束日期项不能早于开始日期！
            this.SetFocus(txtDCIRCLECHECKBEGINDATE);
            return false;
        }

        if (this.txtCHECKTYPE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_NeedPanDianType);//盘点类型项不允许空！
            this.SetFocus(txtCHECKTYPE);
            return false;
        }

        if (this.txtDCREATETIME.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIME.Text.IsDate("yyyy-MM-dd HH:mm:ss") == false)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_ZhiDanRiError);//制单日期项不是有效的日期！
                this.SetFocus(txtDCREATETIME);
                return false;
            }
        }
        //
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (this.txtCCREATEOWNERCODE.Text.GetLengthByByte() > 40)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_ZhiDanRenLength);//制单人项超过指定的长度40！
                this.SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }
        if (this.txtCAUDITPERSON.Text.Trim().Length > 0)
        {
            if (this.txtCAUDITPERSON.Text.GetLengthByByte() > 40)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_AuditorLength);//审核人项超过指定的长度40！
                this.SetFocus(txtCAUDITPERSON);
                return false;
            }
        }
        //
        if (this.txtID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_IdBuNengKong);//ID项不允许空！
            this.SetFocus(txtID);
            return false;
        }
        //
        if (this.txtID.Text.Trim().Length > 0)
        {
            if (this.txtID.Text.GetLengthByByte() > 36)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_IdLength);//ID项超过指定的长度36！
                this.SetFocus(txtID);
                return false;
            }
        }
        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 200)
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_BeiZhuChangDu);//备注项超过指定的长度200！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }

        if (this.txtMAX_PART_IUM.Text.Trim().Length > 0)
        {
            //检查数量是否正确
            string errmsg = string.Empty;
            if (!(DreamTek.ASRS.Business.Others.Comm_Function.Fun_IsDecimal(txtMAX_PART_IUM.Text.Trim(), 0, 0, 1, out errmsg)))
            {
                this.Alert(errmsg);
                this.SetFocus(txtMAX_PART_IUM);
                return false;
            }
        }
        if (ddlLevel.Visible && ddlLevel.SelectedValue.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmBase_AGV_D_rfvtxtSTOREY);//请填写楼层
            this.SetFocus(ddlLevel);
            return false;
        }
        //台车
         //台车
        if (ddlCAR.Visible && this.ddlCAR.SelectedValue.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit1_SelectCar);//请选择台车！
            this.SetFocus(ddlCAR);
            return false;
        }

        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public STOCK_CHECKBILL SendData()
    {

        STOCK_CHECKBILL entity = new STOCK_CHECKBILL();

        if (this.Operation() != SYSOperation.New)
        {
            IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);
            entity = (from p in conn.Get()
                      where p.id == txtID.Text
                      select p
                                      ).FirstOrDefault();

        }

        this.txtCTICKETCODE.Text = this.txtCTICKETCODE.Text.Trim();
        if (this.txtCTICKETCODE.Text.Length > 0)
        {
            entity.cticketcode = txtCTICKETCODE.Text;
        }


        //盘点单名称
        this.txtCCHECKNAME.Text = this.txtCCHECKNAME.Text.Trim();
        if (this.txtCCHECKNAME.Text.Length > 0)
        {
            entity.ccheckname = txtCCHECKNAME.Text;
        }


        //
        this.txtDCHECKDATE.Text = this.txtDCHECKDATE.Text.Trim();
        if (this.txtDCHECKDATE.Text.Length > 0)
        {
            entity.dcheckdate = Convert.ToDateTime(txtDCHECKDATE.Text);// txtDCHECKDATE.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }

        //
        this.txtCERPCODE.Text = this.txtCERPCODE.Text.Trim();
        if (this.txtCERPCODE.Text.Length > 0)
        {
            entity.cerpcode = txtCERPCODE.Text;
        }

        //盘点开始日期
        this.txtDCIRCLECHECKBEGINDATE.Text = this.txtDCIRCLECHECKBEGINDATE.Text.Trim();
        if (this.txtDCIRCLECHECKBEGINDATE.Text.Length > 0)
        {
            if (this.txtDCIRCLECHECKBEGINDATE.Text.Length == 19)
            {
                entity.dcirclecheckbegindate = Convert.ToDateTime(txtDCIRCLECHECKBEGINDATE.Text.Trim());
            }
            else
            {
                entity.dcirclecheckbegindate = Convert.ToDateTime(txtDCIRCLECHECKBEGINDATE.Text.Trim());
            }
        }

        //循环盘点结束日期
        this.txtDCIRCLECHECKENDDATE.Text = this.txtDCIRCLECHECKENDDATE.Text.Trim();
        if (this.txtDCIRCLECHECKENDDATE.Text.Length > 0)
        {
            if (this.txtDCIRCLECHECKENDDATE.Text.Length == 19)
            {
                entity.dcirclecheckenddate = Convert.ToDateTime(txtDCIRCLECHECKENDDATE.Text.Trim());
            }
            else
            {
                entity.dcirclecheckenddate = Convert.ToDateTime(txtDCIRCLECHECKENDDATE.Text.Trim());
            }
        }

        //
        this.txtCHECKTYPE.Text = this.txtCHECKTYPE.Text.Trim();
        if (this.txtCHECKTYPE.Text.Length > 0)
        {
            entity.checktype = txtCHECKTYPE.Text;
        }

        //
        this.txtDCREATETIME.Text = this.txtDCREATETIME.Text.Trim();
        if (this.txtDCREATETIME.Text.Length > 0)
        {
            entity.dcreatetime = Convert.ToDateTime(txtDCREATETIME.Text);
        }

        //
        this.txtCCREATEOWNERCODE.Text = this.txtCCREATEOWNERCODE.Text.Trim();
        if (this.txtCCREATEOWNERCODE.Text.Length > 0)
        {
            entity.ccreateownercode = txtCCREATEOWNERCODE.Text;
        }

        //
        this.dpdCSTATUS.Text = this.dpdCSTATUS.Text.Trim();
        if (this.dpdCSTATUS.Text.Length > 0)
        {
            entity.cstatus = dpdCSTATUS.SelectedValue;
        }

        //
        this.txtDAUDITTIME.Text = this.txtDAUDITTIME.Text.Trim();
        if (this.txtDAUDITTIME.Text.Length > 0)
        {
            entity.daudittime = Convert.ToDateTime(txtDAUDITTIME.Text);
        }
        //
        this.txtCAUDITPERSON.Text = this.txtCAUDITPERSON.Text.Trim();
        if (this.txtCAUDITPERSON.Text.Length > 0)
        {
            entity.cauditperson = txtCAUDITPERSON.Text;
        }
        //
        this.txtID.Text = this.txtID.Text.Trim();
        if (this.txtID.Text.Length > 0)
        {
            entity.id = txtID.Text;
        }
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }

        entity.cdefine1 = this.chkISCHECKBILL.Checked.ToString();

        this.txtMAX_PART_IUM.Text = this.txtMAX_PART_IUM.Text.Trim();
        if (this.txtMAX_PART_IUM.Text.Length > 0)
        {
            entity.max_part_ium = Convert.ToDecimal(this.txtMAX_PART_IUM.Text);
        }
        entity.worktype = drpWorkType.SelectedValue;
     

        //楼层
        if (this.ddlLevel.SelectedValue.Trim().Length > 0 && ddlLevel.Visible)
        {
            entity.lineid = this.ddlLevel.SelectedValue;
        }
        else
        {
            entity.lineid = null;
        }
        //台车
        if (this.ddlCAR.SelectedValue.Trim().Length > 0 && ddlCAR.Visible)
        {
            entity.cdefine2 = this.ddlCAR.SelectedValue;
        }
        else
        {
            entity.cdefine2 = null;
        }
        #region 界面上不可见的字段项
        entity.dcreatetime = DateTime.Now;
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

    /// 保存输入内容到数据库
    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.CheckData())
        {
            STOCK_CHECKBILL entity = (STOCK_CHECKBILL)this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";
            strKeyID += entity.id;
            try
            {
                IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);

                if (this.Operation() == SYSOperation.Modify || this.Operation() == SYSOperation.Preserved1)
                {
                    conn.Update(entity);
                    conn.Save();
                    this.AlertAndBack("FrmSTOCK_CHECKBILLEdit1.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Msg_SaveSuccess); //保存成功！
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    //保存前判断单据号是否已存在 
                    DreamTek.ASRS.Business.Stock.StockQuery query = new DreamTek.ASRS.Business.Stock.StockQuery();
                    if (query.IsExistsCheckCticketcode(entity.cticketcode.Trim()))
                    {
                        entity.cticketcode = new InAsn().CreateNo("STOCK_CHECKBILL");
                    }
                    conn.Insert(entity);
                    conn.Save();
                    strKeyID = "";
                    strKeyID += entity.id;
                    this.AlertAndBack("FrmSTOCK_CHECKBILLEdit1.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Msg_SaveSuccess); //保存成功！
                }
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.WMS_Common_Msg_Failed + E.Message); //失败！
#if Debug 
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
            }
            finally
            {
                btnSave.Style.Remove("disabled");
            }
        }
        else
        {
            btnSave.Style.Remove("disabled");
        }

    }

    /// 导出Excel
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <returns></returns>
    protected DataTable grdNavigatorSTOCK_CHECKBILL_D_GetExportToExcelSource()
    {
        //STOCK_FrmSTOCK_CHECKBILL_DListQuery listQuery = new STOCK_FrmSTOCK_CHECKBILL_DListQuery();
        //DataTable dtSource = listQuery.GetList(txtID.Text, false, -1, -1);
        //return dtSource;
        return null;
    }

    protected void grdSTOCK_CHECKBILL_D_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdSTOCK_CHECKBILL_D_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }
    /// 查询按钮
    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.CurrendIndex = 1;
        this.GridBind();
    }

    /// 删除按钮
    /// <summary>
    /// 删除按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        btnDelete.Enabled = false;
        try
        {
            int count = 0;
            IGenericRepository<STOCK_CHECKBILL_D> conn = new GenericRepository<STOCK_CHECKBILL_D>(db);

            for (int i = 0; i < this.grdSTOCK_CHECKBILL_D.Rows.Count; i++)
            {
                if (this.grdSTOCK_CHECKBILL_D.Rows[i].Cells[0].FindControl("chkSelect") is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_CHECKBILL_D.Rows[i].Cells[0].FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        //主键赋值  
                        string strIDS = this.grdSTOCK_CHECKBILL_D.DataKeys[i].Values[0].ToString();
                        STOCK_CHECKBILL_D bo = (from p in conn.Get().AsEnumerable()
                                                where p.ids == strIDS
                                                select p).FirstOrDefault();
                        conn.Delete(bo.ids);
                        conn.Save();
                        count++;
                    }
                }
            }
            if (count > 0)
                this.Alert(Resources.Lang.WMS_Common_Msg_DeleteSuccess);//删除成功！
            else
                this.Alert(Resources.Lang.WMS_Common_Tips_SelectDelete);//请选择要删除的行
            btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + E.Message.ToJsString());//删除失败！
        }
        btnDelete.Enabled = true;
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdSTOCK_CHECKBILL_D.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdSTOCK_CHECKBILL_D.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdSTOCK_CHECKBILL_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            #region //21-10-2020 by Qamar
            var partNumber = e.Row.Cells[2].Text;
            e.Row.Cells[2].Text = partNumber.Substring(0, partNumber.Length - 2);
            var rankfinal = partNumber.Substring(partNumber.Length - 1, 1);
            if (rankfinal == "_")
                e.Row.Cells[3].Text = "";
            else
                e.Row.Cells[3].Text = rankfinal;
            #endregion
        }
    }

    protected void dsGrdSTOCK_CHECKBILL_D_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdSTOCK_CHECKBILL_D_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
    }

    /// 新增按钮
    /// <summary>
    /// 新增按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (this.CheckData())
        {
            STOCK_CHECKBILL entity = (STOCK_CHECKBILL)this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";
            strKeyID += entity.id;
            try
            {
                IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);

                if (this.Operation() == SYSOperation.Modify || this.Operation() == SYSOperation.Preserved1)
                {
                    conn.Update(entity);
                    conn.Save();
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    conn.Insert(entity);
                    conn.Save();

                    strKeyID = "";
                    strKeyID += entity.id;
                }
                Response.Redirect(BuildRequestPageURL("FrmSTOCK_CHECKBILLEdit1.aspx", SYSOperation.Preserved1, strKeyID));
            }
            catch (Exception E)
            {
            }
        }
    }
    protected void drpWorkType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpWorkType.SelectedValue.Trim() == "0") //平库
        {
            lblLevel.Visible = false;
            ddlLevel.Visible = false;
            sap1.Visible = false;
            ddlCAR.Visible = false;
            lblTaiChe.Visible = false;
            Span1.Visible = false;
        }
        else
        {
            lblLevel.Visible = true;
            ddlLevel.Visible = true;
            sap1.Visible = true;            
            //ddlCAR.Visible = true;
            //lblTaiChe.Visible = true;
            //Span1.Visible = true;
        }

    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLevel.Visible && ddlCAR.Visible)
        {          
            //绑定台车下拉框
            Help.DropDownListDataBind(GetCARList(this.ddlLevel.SelectedValue.Trim()), ddlCAR, Resources.Lang.Common_ALL, "CRANENAME", "CRANEID", "");//台车                 
        }       
    }
}

