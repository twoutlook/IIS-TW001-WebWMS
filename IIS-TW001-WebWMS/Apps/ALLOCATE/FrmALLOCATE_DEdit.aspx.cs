using System;
using System.Data;
using System.Web.UI.WebControls;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Text;
using System.ComponentModel;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using DreamTek.ASRS.Business.Base;
using DreamTek.ASRS.Business;
using DreamTek.ASRS.Business.Allocate;
using DreamTek.ASRS.Business.SP.ProcedureModel;

/// <summary>
/// 描述: 11-->FrmALLOCATE_DEdit 页面后台类文件
/// 作者: --chenlei
/// 创建于: 2012-11-01 13:30:03
/// 
/*
Roger
2013/5/20 9:23:12
20130520092312
解决问题：导入及新增明细存在小数点问题
处理办法：增加卡控
*/
/// </summary>
public partial class ALLOCATE_FrmALLOCATE_DEdit : WMSBasePage
{
    AllocateQuery acQry = new AllocateQuery();
    BaseCommQuery bcQry = new BaseCommQuery();
    BASE_CARGOSPACE_Query bgQry = new BASE_CARGOSPACE_Query();
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.ucPART1.SetCINVNAME = this.txtCINVNAME.ClientID;
        this.ucPART1.SetCINVCODE = this.txtCINVCODE.ClientID;
        this.ucPART1.SetCINVNAME = this.txtCINVNAME.ClientID;

        this.ucCARGOSPACEDiv1.SetCompName = this.txtCPOSITION.ClientID;
        this.ucCARGOSPACEDiv1.SetORGCode = this.txtCPOSITIONCODE.ClientID;
        this.ucCARGOSPACEDiv1.CinvCode = this.txtCINVCODE.Text;        


        this.ucCARGOSPACEDiv2.SetCompName = this.txtCTOPOSITION.ClientID;
        this.ucCARGOSPACEDiv2.SetORGCode = this.txtCTOPOSITIONCODE.ClientID;
        this.ucCARGOSPACEDiv2.CinvCode = string.Empty;


        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData();
            }
            else
            {
                txtID.Text = Request.QueryString["parentId"];
                this.txtDINDATE.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtCINPERSONCODE.Text = OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
                this.txtLineID.Text = InAsn.Fun_GetNo(txtID.Text, "4", "", "");
                //UpdatePanel2.Visible = false;

            }

            IGenericRepository<ALLOCATE> con = new GenericRepository<ALLOCATE>(context);
            var caseList = from p in con.Get()
                           where p.id == this.txtID.Text.Trim()
                           select p;
            ALLOCATE entity = caseList.ToList().FirstOrDefault();
            if (entity != null)
            {
                if (entity.ALLOTYPE != "1")
                {
                    cinvcode.Attributes.Add("style", "display:none");
                    tr_qty.Attributes.Add("style", "display:none");
                }
                switch (entity.ALLOTYPE)
                {
                    case "0": ucCARGOSPACEDiv1.WType = "1";
                        ucCARGOSPACEDiv2.WType = "1";
                        break;
                    case "1": ucCARGOSPACEDiv1.WType = "0";
                        ucCARGOSPACEDiv2.WType = "0";
                        break;
                    default:
                        break;
                }

                if (entity.ALLOTYPE == "0")
                {
                    tr_pk.Attributes.Add("style", "display:none");
                    tr_add.Attributes.Add("style", "display:none");
                }
                if (entity.ALLOTYPE == "1")
                {
                    tr_lk.Attributes.Add("style", "display:none");
                    GridBindSN_PK();
                }

                if (entity.cdefine1 == "3") { //阻挡移库类型不能修改
                    btnNewDetital.Enabled = false;
                    btnSave.Enabled = false;
                    tr_add.Attributes.Add("style", "display:none");
                }
                //暂存=》立库
                if (entity.ALLOTYPE == "4") {
                    btnSave.Enabled = false;
                    btnNewDetital.Enabled = false;
                    btnNew.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
          
        }
        LoadPart();
    }

    #region IPage 成员
    public string ConFigvalue
    {
        set { ViewState["ConFigvalue"] = value; }
        get
        {
            if (ViewState["ConFigvalue"] != null)
            {
                return ViewState["ConFigvalue"].ToString();
            }
            return "";
        }
    }
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('ALLOCATE_D');return false;";//BUCKINGHA-894
        ConFigvalue = CommFunction.GetConFig("120001");
        if (ConFigvalue == "1")
        {
            //btnNew.Text = "批号编辑";
            //grdSN_DateCode.Columns[1].HeaderText = "批号";
        }
        else
        {
            //btnNew.Text = "SN编辑";
            //grdSN_DateCode.Columns[1].HeaderText = "SN";
        }

        //调拨单状态
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "ALLOCATE", false, -1, -1), this.ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");

        //本页面打开新增窗口
        // this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + PageBase.BuildRequestPageURL("FrmALLOCATE_DEdit_SN.aspx", SysOperation.New,txtIDS.Text.Trim()) + "','新增调拨单SN明细','ALLOCATE_SN');return false;";
        //栈板号的字段的显示根据配置的模式进行动态判断是否显示栈板号列
        if (SYSConfig == "1")//栈板-栈板/箱-箱模式
        {
            grdSNList.Columns[4].Visible = false;
        }       
    }

    /// GridView 绑定数据
    /// <summary>
    /// GridView 绑定数据
    /// </summary>
    public void GridBind()
    {     
        Bind("");       
    }

    #endregion

    public string ASRS_STATUS
    {

        get
        {
            if (ViewState["ASRS_STATUS"] == null)
            {
                ViewState["ASRS_STATUS"] = "0";
            }
            return ViewState["ASRS_STATUS"].ToString();
        }
        set
        {
            ViewState["ASRS_STATUS"] = value;
        }
    }

    public int CurrentIndex_BN
    {
        get
        {
            if (ViewState["CurrentIndex_BN"] == null)
            {
                ViewState["CurrentIndex_BN"] = 1;
            }
            return (int)ViewState["CurrentIndex_BN"];
        }
        set
        {
            ViewState["CurrentIndex_BN"] = value;
        }
    }

    public void LoadPart()
    {
        //BASE_PARTEntity part = new BASE_PARTEntity();

        //if (part.SelectOneByExpression("CPARTNUMBER ='" + this.txtCINVCODE.Text + "'"))
        //{
        //    this.txtCINVNAME.Text = part.CPARTNAME;
        //}
        //this.txtCINVNAME.Text = "测试品名";
    }
    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        if (KeyID != null && !string.IsNullOrEmpty(KeyID))
        {
            string ids = this.KeyID;
            IGenericRepository<ALLOCATE_D> conn = new GenericRepository<ALLOCATE_D>(db);
            ALLOCATE_D entity = (from p in conn.Get()
                                 where p.ids == ids
                                 select p).ToList<ALLOCATE_D>().FirstOrDefault();
            if (entity != null)
            {
                this.txtIDS.Text = entity.ids.ToString();
                this.txtID.Text = entity.id.ToString();
                this.ddlCSTATUS.SelectedValue = entity.cstatus;
                this.txtIQUANTITY.Text = entity.iquantity.ToString("#0.00");
                this.txtCPOSITIONCODE.Text = entity.cpositioncode;
                this.txtCPOSITION.Text = entity.cposition;
                this.txtCINVBARCODE.Text = entity.cinvbarcode;
                this.txtCINVCODE.Text = entity.cinvcode;
                this.txtCINVNAME.Text = entity.cinvname;
                this.txtDINDATE.Text = entity.dindate.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtCINPERSONCODE.Text = OPERATOR.GetUserNameByAccountID(entity.cinpersoncode);
                this.txtCMEMO.Text = entity.cmemo;
                this.txtModels.Text = entity.cdefine1;//机种 2013-7-25 11:26:20
                this.txtCTOPOSITIONCODE.Text = entity.ctopositioncode;
                this.txtCTOPOSITION.Text = entity.ctoposition;
                this.txtLineID.Text = entity.lineid;
            }
            this.btnNew.Attributes["onclick"] = "PopupFloatWin('" + BuildRequestPageURL("FrmALLOCATE_DEdit_SN.aspx", SYSOperation.New, txtID.Text.Trim()) + "&IDS=" + txtIDS.Text.Trim() + "','" + Resources.Lang.FrmALLOCATE_DEdit_Msg03+ "','ALLOCATE_SN',1000,600);return false;";
            AllocateQuery allQry = new AllocateQuery();
            if (allQry.AllocateFromOutBill(txtID.Text))
            {
                btnSave.Enabled = false;
            }
            //显示SN列表信息
            Search();
        }

    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        AllocateQuery query = new AllocateQuery();
        if (query.CheckAlloStatus(this.txtID.Text.Trim()) && this.Operation() == SYSOperation.New)
        {
            this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg04);//此调拨单不是未处理状态，不能添加明細！
            return false;
        }

        if (this.txtCPOSITIONCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg05);//原始储位编码项不允许空！
            this.SetFocus(txtCPOSITIONCODE);
            return false;
        }
        ////
        if (this.txtCPOSITIONCODE.Text.Trim().Length > 0)
        {
            if (this.txtCPOSITIONCODE.Text.GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg06); //原始储位编码项超过指定的长度100！
                this.SetFocus(txtCPOSITIONCODE);
                return false;
            }
        }
        else
        {
            var bo = bgQry.GetBaseCargoByCode(txtCPOSITIONCODE.Text.Trim());
            if (bo != null && !bo.id.IsNullOrEmpty())
                txtCPOSITION.Text = bo.cposition;
        }
        //
        if (this.txtCPOSITION.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg07); //原始储位名稱项不允许空！
            this.SetFocus(txtCPOSITION);
            return false;
        }
        ////
        if (this.txtCPOSITION.Text.Trim().Length > 0)
        {
            if (this.txtCPOSITION.Text.GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg08);//原始储位名稱项超过指定的长度100！
                this.SetFocus(txtCPOSITION);
                return false;
            }
        }
        IGenericRepository<BASE_CARGOSPACE> conn = new GenericRepository<BASE_CARGOSPACE>(db);
        BASE_CARGOSPACE bca = (from p in conn.Get()
                               where p.cpositioncode == this.txtCPOSITIONCODE.Text.Trim()
                               select p).FirstOrDefault();

        //ALLOCATE_FrmALLOCATE_DListQuery query = new ALLOCATE_FrmALLOCATE_DListQuery();
        //if (query.CheckCposition(this.txtCPOSITIONCODE.Text.Trim()))
        if (bca != null && bca.is_allo.HasValue && bca.is_allo.Value == 1)
        {
            this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg09);//原始储位不允许调拨！
            this.txtCPOSITION.Text = "";
            this.txtCPOSITIONCODE.Text = "";
            this.SetFocus(txtCPOSITIONCODE);
            return false;
        }

        //判断储位是否冻结 1-冻结//CQ 2013-5-13 12:07:58
        //BASE_CARGOSPACERule ba = new BASE_CARGOSPACERule();
        //if (ba.CheckCpositionStatus(txtCPOSITIONCODE.Text.Trim().ToUpper(), "1"))
        if (bca != null && !string.IsNullOrEmpty(bca.cstatus) && bca.cstatus.Equals("1"))
        {
            this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg10);//原始储位状态为[冻结]，不能调拨！
            this.txtCPOSITION.Text = "";
            this.txtCPOSITIONCODE.Text = "";
            this.SetFocus(txtCPOSITIONCODE);
            return false;
        }



        ////
        if (this.txtCINVBARCODE.Text.Trim().Length > 0)
        {
            if (this.txtCINVBARCODE.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg02); //"物料条码项超过指定的长度30！"
                this.SetFocus(txtCINVBARCODE);
                return false;
            }
        }


        if (this.txtDINDATE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg16);//调拨日期项不允许空！
            this.SetFocus(txtDINDATE);
            return false;
        }
        //
        if (this.txtDINDATE.Text.Trim().Length > 0)
        {
            if (this.txtDINDATE.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmALLOCATEList_NoEfftxtDINDATEFrom);//调拨日期项不是有效的日期！
                this.SetFocus(txtDINDATE);
                return false;
            }
        }
        //
        if (this.txtCINPERSONCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg11);//调拨人项不允许空！
            this.SetFocus(txtCINPERSONCODE);
            return false;
        }
        ////
        if (this.txtCINPERSONCODE.Text.Trim().Length > 0)
        {
            if (OPERATOR.GetUserIDByUserName(this.txtCINPERSONCODE.Text).GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg12);//调拨人项超过指定的长度20！
                this.SetFocus(txtCINPERSONCODE);
                return false;
            }
        }
        ////机种 2013-7-25 11:50:39
        if (this.txtModels.Text.Trim().Length > 0)
        {
            if (this.txtModels.Text.GetLengthByByte() > 40)
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg13);//机种项超过指定的长度40！
                this.SetFocus(txtModels);
                return false;
            }
        }

        ////
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 200)
            {
                this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg15); //备注项超过指定的长度200！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        //目的储位
        if (this.txtCTOPOSITIONCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg14);//目的储位编码项不允许空！
            this.SetFocus(txtCTOPOSITIONCODE);
            return false;
        }
        else
        {
            var bo = bgQry.GetBaseCargoByCode(txtCTOPOSITIONCODE.Text.Trim());
            if (bo != null && !bo.id.IsNullOrEmpty())
                txtCTOPOSITION.Text = bo.cposition;
        }
        //
        if (this.txtCTOPOSITION.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg15); //目的储位项名稱不允许空！
            this.SetFocus(txtCTOPOSITION);
            return false;
        }
        ////
        if (this.txtCTOPOSITION.Text.Trim().Length > 0)
        {
            if (this.txtCTOPOSITION.Text.GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg16); //目的储位项超过指定的长度100！
                this.SetFocus(txtCTOPOSITION);
                return false;
            }
        }


        ////
        if (this.txtCTOPOSITIONCODE.Text.Trim().Length > 0)
        {
            if (this.txtCTOPOSITIONCODE.Text.Trim().ToUpper() == this.txtCPOSITIONCODE.Text.Trim().ToUpper())
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg17); //目的储位不能和原储位相同
                this.SetFocus(txtCTOPOSITIONCODE);
                return false;
            }
        }
        BASE_CARGOSPACE bcat = (from p in conn.Get()
                                where p.cpositioncode == this.txtCTOPOSITIONCODE.Text.Trim()
                                select p).FirstOrDefault();

        //目的储位是不是允许调拨
        //if (query.CheckCposition(this.txtCTOPOSITIONCODE.Text.Trim().ToUpper()))
        if (bcat != null && bcat.is_allo.HasValue && bcat.is_allo.Value == 1)
        {
            this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg18); //目的储位不允许调拨！
            this.txtCTOPOSITION.Text = "";
            this.txtCTOPOSITIONCODE.Text = "";
            this.SetFocus(txtCTOPOSITIONCODE);
            return false;
        }
        //判断储位是否冻结 1-冻结//CQ 2013-5-13 12:07:58
        //if (ba.CheckCpositionStatus(txtCTOPOSITIONCODE.Text.Trim().ToUpper(), "1"))
        if (bcat != null && !string.IsNullOrEmpty(bcat.cstatus) && bcat.cstatus.Equals("1"))
        {
            this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg19);//目的储位状态为[冻结]，不能调拨！
            this.txtCTOPOSITION.Text = "";
            this.txtCTOPOSITIONCODE.Text = "";
            this.SetFocus(txtCTOPOSITIONCODE);
            return false;
        }

        string msg1 = string.Empty;
        //CQ 2014-12-5 16:55:37 检查料号税别是否为空和目的储位税别是否同料号一致
        //if (!(Comm_Function.Fun_CheckTax_SameBond(txtCINVCODE.Text.Trim().ToUpper(), txtCTOPOSITIONCODE.Text.Trim().ToUpper(), out msg1)))
        //{
        //    this.Alert(msg1);
        //    this.SetFocus(txtCINVCODE);
        //    return false;
        //}

        //当表头字段is_allow为1时，调拨不做限制。。
        //将数量带到目的储位去验证是否可以存放(验证)
        //非保税不能调拨到保税

        IGenericRepository<ALLOCATE> adb = new GenericRepository<ALLOCATE>(db);
        ALLOCATE entity = (from p in adb.Get()
                           where p.id == txtID.Text.Trim()
                           select p).FirstOrDefault();

        //ALLOCATE_FrmALLOCATE_DListQuery lis = new ALLOCATE_FrmALLOCATE_DListQuery();
        //if (lis.CheckAllow(txtID.Text.Trim()))
        if (entity != null && entity.is_allow.HasValue && entity.is_allow.Value == 0)
        {
            //BASE_FrmWAREHOUSEListQuery listquery = new BASE_FrmWAREHOUSEListQuery();
            //string CPOSITIONCODE_BONDED = listquery.GetWareByBARCODE(this.txtCPOSITIONCODE.Text);
            //string CTOPOSITIONCODE_BONDED = listquery.GetWareByBARCODE(this.txtCTOPOSITIONCODE.Text);

            IGenericRepository<BASE_CARGOSPACE> BCG = new GenericRepository<BASE_CARGOSPACE>(db);
            IGenericRepository<BASE_WAREHOUSE> BWH = new GenericRepository<BASE_WAREHOUSE>(db);

            string b1 = (from p in BCG.Get().AsEnumerable()
                         join b in BWH.Get().AsEnumerable() on p.warehouseid equals b.id
                         where p.cpositioncode == txtCPOSITIONCODE.Text
                         select b.bonded).FirstOrDefault();

            string b2 = (from p in BCG.Get().AsEnumerable()
                         join b in BWH.Get().AsEnumerable() on p.warehouseid equals b.id
                         where p.cpositioncode == txtCTOPOSITIONCODE.Text
                         select b.bonded).FirstOrDefault();

            //if (CPOSITIONCODE_BONDED != CTOPOSITIONCODE_BONDED) //CPOSITIONCODE_BONDED != "Y" && 
            if (!string.IsNullOrEmpty(b1) && !string.IsNullOrEmpty(b2) && !b1.Equals(b2))
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg20); //非保税仓和保税仓不能互调,請重新輸入
                this.SetFocus(txtCTOPOSITION);
                return false;
            }
        }
        //判断原储位是不是線边仓储位
        IGenericRepository<BASE_LINE_INFO> bli = new GenericRepository<BASE_LINE_INFO>(db);
        BASE_LINE_INFO bliBO = (from p in bli.Get()
                                where p.cpositioncode.ToUpper() == txtCPOSITIONCODE.Text.Trim().ToUpper() && p.vendor_code.Equals("0")
                                select p).FirstOrDefault();

        if (bliBO != null && !string.IsNullOrEmpty(bliBO.id))
        {
            Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg21); //原儲位不能是線邊倉儲位
            this.txtCPOSITION.Text = "";
            this.txtCPOSITIONCODE.Text = "";
            this.SetFocus(txtCPOSITIONCODE);
            return false;
        }
        //判断目的储位是线边仓储位
        bliBO = (from p in bli.Get()
                 where p.cpositioncode.ToUpper() == txtCTOPOSITIONCODE.Text.Trim().ToUpper() && p.vendor_code.Equals("0")
                 select p).FirstOrDefault();

        if (bliBO != null && !string.IsNullOrEmpty(bliBO.id))
        {
            Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg22); //目的儲位不能是線邊倉儲位
            this.txtCTOPOSITION.Text = "";
            this.txtCTOPOSITIONCODE.Text = "";
            this.SetFocus(txtCTOPOSITIONCODE);
            return false;
        }

        decimal qty = 0;
        if (decimal.TryParse(txtIQUANTITY.Text, out qty))
        {
            if (bcQry.GetSurplusQtyByPartCodeAndCPositionCode(this.txtCTOPOSITIONCODE.Text.Trim()) < qty)
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg23);//目的儲位可用空間不足,或目的儲位未設置最大量!不可調撥！
                return false;
            }
        }


        //根据调拨类型 判断储位是否允许调拨
        IGenericRepository<ALLOCATE> con_at = new GenericRepository<ALLOCATE>(context);
        ALLOCATE entity_at = con_at.Get().Where(x => x.id == txtID.Text.Trim()).FirstOrDefault();

        #region 立库仓内调拨检查
        if (entity_at.ALLOTYPE == "0") //立库仓内调拨
        {
            //原始储位上必须有货物
            int count = 0;
            IGenericRepository<STOCK_CURRENT> ISC = new GenericRepository<STOCK_CURRENT>(db);
            count = (from p in ISC.Get()
                     where p.cpositioncode.ToUpper() == txtCPOSITIONCODE.Text.Trim()
                     select p).Count();
            if (count == 0)
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg24);//原始储位上必须有货物！
                this.SetFocus(txtCPOSITIONCODE);
                return false;
            }

            //目标储位上必须没有货物
            ISC = new GenericRepository<STOCK_CURRENT>(db);
            count = (from p in ISC.Get()
                     where p.cpositioncode.ToUpper() == txtCTOPOSITIONCODE.Text.Trim()
                     select p).Count();
            if (count > 0)
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg25);//目标储位上必须没有货物！
                this.SetFocus(txtCTOPOSITIONCODE);
                return false;
            }
            //判断原储位与目的储位是否为立库储位
            if (query.IsLKCargospace(txtCPOSITIONCODE.Text.Trim()))
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg26);//原始储位必须为立库的储位！
                this.SetFocus(txtCPOSITIONCODE);
                return false;
            }
            if (query.IsLKCargospace(txtCTOPOSITIONCODE.Text.Trim()))
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg27); //目的储位必须为立库的储位！
                this.SetFocus(txtCTOPOSITIONCODE);
                return false;
            }
            //判断原储位与目的储位线别是否相同
            if (query.GetCargospaceLine(txtCPOSITIONCODE.Text.Trim()) != query.GetCargospaceLine(txtCTOPOSITIONCODE.Text.Trim()))
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg28); //原始储位与目的储位所属线别必须一致！
                this.SetFocus(txtCPOSITIONCODE);
                return false;
            }
            //判断原始储位与目的储位种类是否相符
            string yzl = query.GetCpositionZL(txtCPOSITIONCODE.Text.Trim());
            string mzl = query.GetCpositionZL(txtCTOPOSITIONCODE.Text.Trim());
            if (string.IsNullOrEmpty(yzl))
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg29); //原始储位未设置种类！
                this.SetFocus(txtCPOSITIONCODE);
                return false;
            }
            if (string.IsNullOrEmpty(yzl))
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg30); //目的储位未设置种类！
                this.SetFocus(txtCTOPOSITIONCODE);
                return false;
            }
            if (mzl == "2")
            {
                if (yzl == "3")
                {
                    this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg31); //目的储位为中库位,原始储位为高库位,不允许调拨！
                    this.SetFocus(txtCPOSITIONCODE);
                    return false;
                }
            }
            if (mzl == "1")
            {
                if (yzl == "3")
                {
                    this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg32); //目的储位为低库位,原始储位为高库位,不允许调拨！
                    this.SetFocus(txtCPOSITIONCODE);
                    return false;
                }
                if (yzl == "2")
                {
                    this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg33); //目的储位为低库位,原始储位为中库位,不允许调拨！
                    this.SetFocus(txtCPOSITIONCODE);
                    return false;
                }
            }
        }
        #endregion
        
        #region 平库仓内调拨检查
        if (entity_at.ALLOTYPE == "1")//平库仓内调拨
        {
            if (this.txtIQUANTITY.Text.Trim() == "")
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg34);//數量項不允許空！
                this.SetFocus(txtIQUANTITY);
                return false;
            }
            //檢查數量，不允許小數，負數，0 CQ 2014-2-13 13:39:48
            //检查数量是否正确
            string errmsg = string.Empty;
            if (!(DreamTek.ASRS.Business.Others.Comm_Function.Fun_IsDecimal(txtIQUANTITY.Text.Trim(), 0, 0, 1, out errmsg)))
            {
                this.Alert(errmsg);
                this.SetFocus(txtIQUANTITY);
                return false;
            }
            //string msg = string.Empty;
            //if (!(query.Fun_IsDecimal(txtIQUANTITY.Text.Trim(), 0, 0, 1, out msg)))
            //{
            //    this.Alert(msg);
            //    this.SetFocus(txtIQUANTITY);
            //    return false;
            //}
            if (this.txtCINVCODE.Text.Trim() == "")
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg35); //料號項不允許空！
                this.SetFocus(txtCINVCODE);
                return false;
            }
            ////
            if (this.txtCINVCODE.Text.Trim().Length > 0)
            {
                if (this.txtCINVCODE.Text.GetLengthByByte() > 50)
                {
                    this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg36); //料號項超過指定的長度50！
                    this.SetFocus(txtCINVCODE);
                    return false;
                }
            }

            //判断原储位与目的储位是否为立库储位
            if (query.IsPKCargospace(txtCPOSITIONCODE.Text.Trim()))
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg37); //原始储位必须为平库的储位！
                this.SetFocus(txtCPOSITIONCODE);
                return false;
            }
            if (query.IsPKCargospace(txtCTOPOSITIONCODE.Text.Trim()))
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg38); //目的储位必须为平库的储位！
                this.SetFocus(txtCTOPOSITIONCODE);
                return false;
            }
            #region 检查原储位库存量

            //原储位库存数量检查，原储位剩余物料数量=原库存数量-占用物料数量
            if (this.txtIQUANTITY.Text.Trim().Length > 0)
            {
                //獲取源庫存數
                string IOCcupynum = "0";
                DataTable tb = query.GetList_Stock(this.txtCPOSITIONCODE.Text, txtCINVCODE.Text);
                if (tb.Rows.Count > 0)
                {
                    hfQty.Value = tb.Rows[0]["IQTY"].ToString(); //库存数量
                    IOCcupynum = tb.Rows[0]["IOCCUPYQTY"].ToString();//被占用数量
                }
                if (hfQty.Value != "")
                {

                    decimal KCSyNum = 0;//库存剩余数量
                    KCSyNum = Convert.ToDecimal(hfQty.Value) - Convert.ToDecimal(IOCcupynum);
                    if (Convert.ToDecimal(txtIQUANTITY.Text) > KCSyNum)
                    {
                        if (Convert.ToDecimal(hfQty.Value) == 0)//库存量为0
                        {
                            //System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "str", "该料号在[" + this.txtCPOSITIONCODE.Text.Trim() + "]储位没有库存！", true);
                            this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg39 + "[" + this.txtCPOSITIONCODE.Text.Trim() + "]" + Resources.Lang.FrmALLOCATE_DEdit_Msg40); //该料号在    储位没有库存！
                        }
                        else
                        {
                            if (IOCcupynum == "0")//占用量为0
                            {
                                //this.Alert("调拨数量不能大于库存剩余数量" + KCSyNum); 
                                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg41 + hfQty.Value); //调拨库存不足，库存量为
                            }
                            else
                            {
                                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg42 + hfQty.Value + Resources.Lang.FrmALLOCATE_DEdit_Msg43+ IOCcupynum); //调拨库存不足，其中库存量为:    占用量为：
                            }

                        }
                        this.SetFocus(txtIQUANTITY);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            #endregion
            //料号目的储位区域检查 CQ 2014-2-21 09:25:20
            //string vResult = string.Empty;
            //if (!(bcQry.Fun_IsControl_Area(txtCINVCODE.Text.Trim(), txtCTOPOSITIONCODE.Text.Trim(),
            //                                     WmsWebUserInfo.GetCurrentUser().UserNo, 0, "", "", out vResult)))
            //{
            //    this.Alert(vResult);
            //    this.SetFocus(txtCTOPOSITIONCODE);
            //    return false;
            //}

            //Roger SN整合 2013/12/12 11:39:54
            if (this.Operation() == SYSOperation.Modify && query.ExistSN(txtIDS.Text))
            {
                IGenericRepository<ALLOCATE_D> con_ad = new GenericRepository<ALLOCATE_D>(context);
                ALLOCATE_D entity_d = (from p in con_ad.Get()
                                       where p.ids == txtIDS.Text
                                       select p).ToList().FirstOrDefault();
                if (entity_d.iquantity != Convert.ToDecimal(txtIQUANTITY.Text.Trim()))
                {
                    Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg44);//已经存在SN，请删除后修改数量
                    return false;
                }
                if (!entity_d.cinvcode.Equals(txtCINVCODE.Text.Trim()))
                {
                    Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg45); //已经存在SN，请删除后修改料号
                    return false;
                }
                //CQ 2014-4-28 12:03:39
                if (!entity_d.cpositioncode.Equals(txtCPOSITIONCODE.Text.Trim().ToUpper()))
                {
                    Alert(Resources.Lang.FrmALLOCATE_DEdit_Msg46); //已经存在SN，请删除后修改原储位
                    return false;
                }
            }
        }
        #endregion
        
        

        return true;

    }    

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public ALLOCATE_D SendData()
    {
        ALLOCATE_D entity = new ALLOCATE_D();
        //
        this.txtIDS.Text = this.txtIDS.Text.Trim();
        if (this.txtIDS.Text.Length > 0)
        {
            // entity.IDS = txtIDS.Text.Trim();
            IGenericRepository<ALLOCATE_D> conn = new GenericRepository<ALLOCATE_D>(db);
            entity = (from p in conn.Get()
                      where p.ids == txtIDS.Text.Trim()
                      select p).FirstOrDefault();


        }

        //
        this.txtID.Text = this.txtID.Text.Trim();
        if (this.txtID.Text.Length > 0)
        {
            entity.id = txtID.Text.Trim();
        }

        //
        this.txtIQUANTITY.Text = this.txtIQUANTITY.Text.Trim();
        if (this.txtIQUANTITY.Text.Length > 0)
        {
            entity.iquantity = txtIQUANTITY.Text.ToDecimal();
        }

        //
        this.txtCPOSITIONCODE.Text = this.txtCPOSITIONCODE.Text.Trim();
        if (this.txtCPOSITIONCODE.Text.Length > 0)
        {
            entity.cpositioncode = txtCPOSITIONCODE.Text;
        }

        //
        this.txtCPOSITION.Text = this.txtCPOSITION.Text.Trim();
        if (this.txtCPOSITION.Text.Length > 0)
        {
            entity.cposition = txtCPOSITION.Text;
        }

        //
        this.txtCINVBARCODE.Text = this.txtCINVBARCODE.Text.Trim();
        if (this.txtCINVBARCODE.Text.Length > 0)
        {
            entity.cinvbarcode = txtCINVBARCODE.Text;
        }

        //
        this.txtCINVCODE.Text = this.txtCINVCODE.Text.Trim();
        if (this.txtCINVCODE.Text.Length > 0)
        {
            entity.cinvcode = txtCINVCODE.Text;
            entity.cinvname = (new BasePartListQuery()).GetBasePartByNumber(txtCINVCODE.Text).cpartname;
        }


        //
        this.txtDINDATE.Text = this.txtDINDATE.Text.Trim();
        if (this.txtDINDATE.Text.Length > 0)
        {
            entity.dindate = txtDINDATE.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }

        //
        //this.txtCINPERSONCODE.Text = this.txtCINPERSONCODE.Text.Trim();
        //if(this.txtCINPERSONCODE.Text.Length > 0)
        //{
        entity.cinpersoncode = WmsWebUserInfo.GetCurrentUser().UserNo;
        //}
        //else
        //{
        //    entity.SetDBNull("CINPERSONCODE",true);
        //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        //    //entity.CINPERSONCODE = null;
        //}
        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }

        //机种 2013-7-25 13:34:24
        this.txtModels.Text = this.txtModels.Text.Trim();
        if (this.txtModels.Text.Length > 0)
        {
            entity.cdefine1 = txtModels.Text;
        }       

        this.txtCTOPOSITIONCODE.Text = this.txtCTOPOSITIONCODE.Text.Trim();
        if (this.txtCTOPOSITIONCODE.Text.Length > 0)
        {
            entity.ctopositioncode = txtCTOPOSITIONCODE.Text;
        }

        //
        this.txtCTOPOSITION.Text = this.txtCTOPOSITION.Text.Trim();
        if (this.txtCTOPOSITION.Text.Length > 0)
        {
            entity.ctoposition = txtCTOPOSITION.Text;
        }
        entity.cinvname = AllocateQuery.GetCinvName(entity.cinvcode);
        entity.cstatus = "0";
        if (this.txtLineID.Text.Trim().Length > 0)
        {
            entity.lineid = txtLineID.Text.Trim();
        }
        return entity;
    }

    //保存
    private void SaveToDB()
    {

        string strKeyID = "";
        try
        {
            PRC_CreateAllocateDetails sp = new PRC_CreateAllocateDetails();
            sp.P_AllocateID = txtID.Text.Trim();
            sp.P_PostionFrom = txtCPOSITIONCODE.Text.Trim();
            sp.P_PostionTo = txtCTOPOSITIONCODE.Text.Trim();
            sp.P_UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
            sp.Execute();
            if (sp.ReturnValue == 0)
            {
                strKeyID = sp.ErrorMessage;
                //this.AlertAndBack(UpdatePanel1, "FrmALLOCATE_DEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                this.AlertAndBack("FrmALLOCATE_DEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave ); //保存成功
            }
            else
            {
                Alert(Resources.Lang.Common_SuccessFail + "：" + sp.ErrorMessage);//保存失败
            }
        }
        catch (Exception E)
        {
            this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATE_DEdit_Msg47 + E.Message);//失败！
            this.btnSave.Enabled = true;
        }
        this.btnSave.Enabled = true;
    }

    private void SavePKToDB()
    {
        IGenericRepository<ALLOCATE_D> con = new GenericRepository<ALLOCATE_D>(context);
        string strKeyID = "";
        try
        {
            ALLOCATE_D entity = this.SendData();
            if (this.Operation() == SYSOperation.Modify)
            {
                strKeyID = txtIDS.Text.Trim();
                entity.ids = txtIDS.Text.Trim();
                con.Update(entity);
                con.Save();
                //this.AlertAndBack(this.UpdatePanel1, "FrmALLOCATE_DEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
                this.AlertAndBack("FrmALLOCATE_DEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave ); //保存成功
            }
            else if (this.Operation() == SYSOperation.New)
            {
                //--判断同一料号，同一原始储位，同一目的储位在同一个调拨单中只能有一条调拨明细
                ALLOCATE_D uentity = (from p in con.Get()
                                       where p.cpositioncode == this.txtCPOSITIONCODE.Text
                                       && p.ctopositioncode == this.txtCTOPOSITIONCODE.Text
                                       && p.cinvcode == this.txtCINVCODE.Text
                                       && p.id == this.txtIDS.Text
                                       select p).ToList().FirstOrDefault();
                if (uentity!=null) //存在同一料号同一原始储位，同一目的储位的调拨单明细
                {
                    //做update操作            
                    
                    entity.ids = uentity.ids;
                    strKeyID = entity.ids;
                    entity.lineid = uentity.lineid;  //项次保留原来的项次
                    entity.iquantity = entity.iquantity + uentity.iquantity;
                    entity.ioriquantity = entity.ioriquantity + uentity.ioriquantity;
                    entity.dindate = DateTime.Now.Date;
                    con.Update(entity);
                    con.Save();
                }
                else
                {
                    strKeyID = Guid.NewGuid().ToString();
                    entity.ids = strKeyID;
                    entity.ctopositioncode = txtCTOPOSITIONCODE.Text;
                    entity.cmidpositioncode = txtCTOPOSITIONCODE.Text;
                    entity.ioriquantity = Convert.ToDecimal(this.txtIQUANTITY.Text.Trim());
                    con.Insert(entity);
                    con.Save();
                }
                //this.AlertAndBack(this.UpdatePanel1, "FrmALLOCATE_DEdit.aspx?parentId=" + txtID.Text.Trim() + "&" + BuildQueryString(SYSOperation.Modify, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
                this.AlertAndBack("FrmALLOCATE_DEdit.aspx?parentId=" + txtID.Text.Trim() + "&" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
            }
        }
        catch (System.Exception E)
        {
            this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATE_DEdit_Msg47 + E.Message);//失败！
            this.btnSave.Enabled = true;
        }
        this.btnSave.Enabled = true;
    }
    //ok
    protected void okbutton_Click(object sender, EventArgs e)
    {
        SaveToDB();
        msgBox.Hide();
    }

    //Cancle
    protected void cancelbutton_Click(object sender, EventArgs e)
    {
        this.btnSave.Enabled = true;
        msgBox.Hide();
    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.btnSave.Enabled = false; //CQ 2013-5-13 12:07:58
        if (this.CheckData())
        {
            //CQ 20130520
            var list = new AllocateQuery();
            //if (list.CheckRepeat(txtID.Text.Trim(), txtCINVCODE.Text.Trim().ToUpper()))
            //2013/8/1 10:25:29 调整增加明细IDS
            var IDS = string.Empty;
            if (this.Operation() == SYSOperation.New)
            {
                IDS = "";
            }
            else
            {
                IDS = this.KeyID.Trim();
            }
            IGenericRepository<ALLOCATE> con = new GenericRepository<ALLOCATE>(context);
            ALLOCATE entity = (from p in con.Get()
                               where p.id == txtID.Text
                               select p).ToList().FirstOrDefault();
            if (entity.ALLOTYPE == "0") //立库
                SaveToDB();
            else if (entity.ALLOTYPE == "1") //平库
                SavePKToDB();
        }
        this.btnSave.Enabled = true;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            IGenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
            ALLOCATE entity = (from p in conn.Get()
                               where p.id == txtID.Text.Trim()
                               select p).FirstOrDefault();

            //ALLOCATE_DRule allocate_d_sn = new ALLOCATE_DRule();
            //未扣帐的补单可以删除
            //状态未处理 补单
            if (entity != null && !string.IsNullOrEmpty(entity.cstatus) && entity.cstatus.Equals("0") && acQry.isBD(txtID.Text.Trim()))
            {
                //DBUtil.BeginTrans();
                //主键赋值
                acQry.DeleteSn(txtIDS.Text.Trim());	//执行动作 
                //DBUtil.Commit();
            }
            else
            {
                msg = Resources.Lang.FrmALLOCATE_DEdit_Msg48;//只有状态为未处理的调拨单补单可以删除SN!
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.Common_SuccessDel;//删除成功！
            }
            this.Alert(msg);
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.Common_FailDel + E.Message.ToJsString());//删除失败！
            //DBUtil.Rollback();
        }
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    /// 导出Excel绑定
    /// <summary>
    /// 导出Excel绑定
    /// </summary>
    /// <returns></returns>
    //protected DataTable grdNavigatorSN_DateCode_GetExportToExcelSource()
    //{
    //    Allocate_D_SN_SQL da = new Allocate_D_SN_SQL();
    //    DataTable dtSource = da.GetList(txtIDS.Text.Trim(), false, -1, -1);
    //    return dtSource;
    //}

    /// 查询方法
    /// <summary>
    /// 查询方法
    /// </summary>
    public void Search()
    {
        GridBind();
    }
    protected void grdSN_DateCode_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void grdSN_DateCode_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    protected void grdSN_DateCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

    protected void grdSN_DateCode_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void grdSN_DateCode_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
        GridBindSN_PK();
    }

    //添加新明细
    protected void btnNewDetital_Click(object sender, EventArgs e)
    {
        this.ConfirmThenBack("FrmALLOCATE_DEdit.aspx?parentId=" + txtID.Text.Trim() + "&" + BuildQueryString(SYSOperation.New, Guid.NewGuid().ToString()), Resources.Lang.FrmALLOCATE_DEdit_Msg49);//是否添加新的调拨单明细?
        //WriteScript("window.location.href = 'FrmALLOCATE_DEdit.aspx?parentId=" + txtID.Text.Trim() + "&" + BuildQueryString(SysOperation.New, Guid.NewGuid().ToString()) + "';");
    }


    public IQueryable<ALLOCATE_D_SN> GetQueryList()
    {
        IGenericRepository<ALLOCATE_D_SN> conn = new GenericRepository<ALLOCATE_D_SN>(db);
        var caseList = from p in conn.Get()
                       orderby p.createtime descending
                       where p.allocate_d_ids == txtIDS.Text.Trim()
                       select p;

        //if (caseList != null && caseList.Count() > 0)
        //{
        //    if (txtIDS.Text.Trim() != string.Empty)
        //    {
        //        caseList = caseList.Where(x => !string.IsNullOrEmpty(x.allocate_d_ids) && x.allocate_d_ids.Equals(txtIDS.Text.Trim()));
        //    }
        //}
        return caseList;
    }

    public void BindBN()
    {
        string strSQL = string.Format(@"SELECT  *
                                        FROM    dbo.ALLOCATE_D_BN
                                        WHERE   SNIDS IN ( SELECT   id
                                                           FROM     dbo.ALLOCATE_D_SN
                                                           WHERE    allocate_d_ids = '{0}' )
                                        ORDER BY BNCODE ASC", txtIDS.Text.Trim());
        DataTable dt = DBHelp.ExecuteToDataTable(strSQL);
        if (dt != null && dt.Rows.Count > 0)
        {
            AspNetPager2.RecordCount = dt.Rows.Count;
            AspNetPager2.PageSize = this.PageSize;
        }
        gridBN.DataSource = dt;
        gridBN.DataBind();
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
            grdSNList.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
            grdSNList.DataBind();
        }
        else
        {
            grdSNList.DataSource = null;
            grdSNList.DataBind();
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
           
        }

      
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    ////获取SN
    //public void GridBindSN()
    //{
    //    //if (txtCPOSITIONCODE.Text.Trim() != string.Empty)
    //    //{
    //    //    FrmSTOCK_CURRENT_DETAILListQuery listQuery = new FrmSTOCK_CURRENT_DETAILListQuery();

    //    //    DataTable dtCount = listQuery.GetSNDetailsList(txtIDS.Text.Trim(), true, 0, 0);


    //    //    if (dtCount != null)
    //    //    {
    //    //        this.grdNavigatorSNList.RowCount = int.Parse(dtCount.Rows[0][0].ToString());
    //    //    }
    //    //    else
    //    //    {
    //    //        this.grdNavigatorSNList.RowCount = 0;
    //    //    }

    //    //    DataTable dtSource = listQuery.GetSNDetailsList(txtIDS.Text.Trim(), false, this.grdNavigatorSNList.CurrentPageIndex, this.grdSNList.PageSize);
    //    //    this.grdSNList.DataSource = dtSource;
    //    //    this.grdSNList.DataBind();
    //    //}
    //}

    public void GridBindSN_PK()
    {
        IGenericRepository<ALLOCATE_D_SN> con = new GenericRepository<ALLOCATE_D_SN>(context);
        //var caseList = from p in con.Get()
        //               orderby p.createtime descending
        //               where p.allocate_id == txtID.Text
        //               select p;
        var caseList = from p in con.Get()
                       orderby p.createtime descending
                       where p.allocate_d_ids == txtIDS.Text.Trim()
                       select p;
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager2.RecordCount = caseList.Count();
            AspNetPager2.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }
        gridBN.DataSource = GetPageSize(caseList, PageSize, CurrentIndex_BN).ToList();
        gridBN.DataBind();
    }
  
    //SN 行判定
    protected void grdSNList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //类型
            var labtype = e.Row.FindControl("labtype") as Label;
            labtype.Text = GetConfigSNName();
            //if (!string.IsNullOrEmpty(ConFigvalue))
            //{
            //    switch (ConFigvalue)
            //    {
            //        case "0": labtype.Text = Resources.Lang.Common_SN;//SN
            //            break;
            //        case "1": labtype.Text = Resources.Lang.Common_Xiang;//箱
            //            break;
            //        case "2": labtype.Text = Resources.Lang.Common_ZhanBan; //栈板
            //            break;
            //        default: labtype.Text = Resources.Lang.Common_SN; //SN
            //            break;
            //    }
            //}
            //创建人        
            e.Row.Cells[7].Text = OPERATOR.GetUserNameByAccountID(e.Row.Cells[7].Text);  //---BUCKINGHA-838 条码管理修改CH
        }

    }
    protected void gridBN_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {           
            //创建人        
            e.Row.Cells[4].Text = OPERATOR.GetUserNameByAccountID(e.Row.Cells[4].Text);//---BUCKINGHA-838 条码管理修改CH
        }
    }
    protected void AspNetPager2_PageChanged(object sender, System.EventArgs e)
    {
        this.CurrentIndex_BN = AspNetPager2.CurrentPageIndex;//索引同步
        GridBindSN_PK();
    }
}