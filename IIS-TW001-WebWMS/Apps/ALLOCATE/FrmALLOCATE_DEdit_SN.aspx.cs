using System;
using System.Data;
using System.Linq;
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


/// <summary>
/// 描述: 11-->FrmALLOCATE_DEdit 页面后台类文件
/// 作者: --chenlei
/// 创建于: 2012-11-01 13:30:03
/// </summary>
public partial class ALLOCATE_FrmALLOCATE_DEdit_SN : WMSBasePage
{
    BaseCommQuery bcQry = new BaseCommQuery();
    AllocateQuery acQry = new AllocateQuery();

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.ucPART1.SetCINVNAME = this.txtCINVNAME.ClientID;
        //showSn.SetSearchCinvCode = this.txtCINVCODE.Text.Trim();
        if (this.IsPostBack == false)
        {
            this.InitPage();
            ShowData();
        }
        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
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
        txtIDS.Text = Request.QueryString["IDS"];
        txtID.Text = Request.QueryString["ID"];
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('ALLOCATE_SN');return false;";        
        ConFigvalue = this.GetConFig("000002");
    }

    /// GridView 绑定数据
    /// <summary>
    /// GridView 绑定数据
    /// </summary>
    public void GridBind()
    {

    }

    #endregion

    
    /// <summary>
    /// 记录状态
    /// </summary>
    public string AlloSPType
    {
        get
        {
            if (ViewState["AlloSPType"] != null)
            {
                return ViewState["AlloSPType"].ToString();
            }
            return "";
        }
        set { ViewState["AlloSPType"] = value; }
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<ALLOCATE_D> conn = new GenericRepository<ALLOCATE_D>(db);
        ALLOCATE_D entity = (from p in conn.Get()
                             where p.ids == txtIDS.Text
                             select p).FirstOrDefault();

        //ALLOCATE_DEntity entity = new ALLOCATE_DEntity();
        //entity.IDS = txtIDS.Text;
        //entity.SelectByPKeys();

        // this.txtIDS.Text = entity.IDS.ToString();
        if (entity != null && !string.IsNullOrEmpty(entity.ids))
        {
            this.txtID.Text = entity.id.ToString();
            this.txtIQUANTITY.Text = entity.iquantity.ToString("#0.00");
            this.txtCPOSITIONCODE.Text = entity.cpositioncode;
            this.txtCPOSITION.Text = entity.cposition;
            this.txtCINVCODE.Text = entity.cinvcode;
            this.txtCINVNAME.Text = entity.cinvname;
            this.txtDINDATE.Text = entity.dindate != null ? entity.dindate.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtCINPERSONCODE.Text = OPERATOR.GetUserNameByAccountID(entity.cinpersoncode);
            this.txtCMEMO.Text = entity.cmemo;
            this.txtModels.Text = entity.cdefine1;//机种 2013-7-25 11:26:20
            this.txtCTOPOSITIONCODE.Text = entity.ctopositioncode;
            this.txtCTOPOSITION.Text = entity.ctoposition;
        }
        AlloSPType = "0";

        IGenericRepository<ALLOCATE> adb = new GenericRepository<ALLOCATE>(db);
        ALLOCATE entity_Main = (from p in adb.Get()
                           where p.id == txtID.Text.Trim()
                           select p).FirstOrDefault();

        //判断调拨单是否是一般调拨单
        //if (Allocate_D_SN_SQL.GetAlloType(txtID.Text.Trim()) == "0")
        if (entity_Main != null && !string.IsNullOrEmpty(entity_Main.id) 
            && entity_Main.special.HasValue && entity_Main.special.Value == 0)
        {
            //判断调拨单是否是不良品调入良品
            AlloSPType = bcQry.GetWareType_Cargo(txtCPOSITIONCODE.Text.Trim().ToUpper(),
                                                             txtCTOPOSITIONCODE.Text.Trim().ToUpper());
        }  
        //显示SN列表信息
        DataToGridView();

    }
    /// GridView 绑定数据
    /// <summary>
    /// GridView 绑定数据
    /// </summary>
    public void DataToGridView()
    {
        //Allocate_D_SN_SQL da = new Allocate_D_SN_SQL();
        //DataTable dtSource = da.GetList(txtIDS.Text.Trim(), false, -1, -1);

        IGenericRepository<ALLOCATE_D_SN> conn = new GenericRepository<ALLOCATE_D_SN>(db);
        var snList = (from p in conn.Get()
                             where p.allocate_d_ids == txtIDS.Text.Trim()
                             select p).ToList();
        if (snList != null && snList.Count() > 0)
        {
            this.grdSNDetial.DataSource = snList;
            this.grdSNDetial.DataBind();
        }
        else {
            this.grdSNDetial.DataSource = null;
            this.grdSNDetial.DataBind();
        }
    }

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
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        BASE_CARGOSPACE_Query bc = new BASE_CARGOSPACE_Query();
        //校验数量和是否与明细数量一致
        var dtSN = GetGridViewData();
        var i = 0;
        foreach (DataRow dr in dtSN.Rows)
        {
            i++;
            var snCode = dr["SN_CODE"].ToString().Trim().ToUpper();
            if (snCode.IsNullOrEmpty())
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg06);//第  行,SN为空
                return false;
            }
            //判断sn是否被别的单据正在使用       
            string msg = bc.isSNCODEUsedByOthers(snCode, txtID.Text.Trim(),"");
            if (msg.Length > 0)
            {
                Alert(msg);
                return false;
            }
            //end
            IGenericRepository<view_get_palorcar_type> conn = new GenericRepository<view_get_palorcar_type>(db);
            var snBOType = ( from p in conn.Get()
                             where p.SNNo == snCode
                             select p ).FirstOrDefault();

            string sntype = string.Empty;
            if (snBOType != null && !snBOType.SNNo.IsNullOrEmpty())
            {
                sntype = snBOType.SNType.ToString();
            }

            //if (ConFigvalue == "0")
            //{
            //    if (sntype.Equals("") || sntype.Equals("0"))//非栈板箱
            //    {
            //        if (snCode.Length != 19 && snCode.Length != 16)
            //        {
            //            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "jsalert", "alert('第[" + i + "]行,[" + snCode + "]长度不为19码或16码')", true);
            //            //ScriptManager.RegisterClientScriptBlock(UpdatePanel2, UpdatePanel2.GetType(), "jsalert", "<script>alert('第[" + i + "]行,[" + snCode + "]长度不为19码或16码')</script>", false);
            //            //ScriptManager.RegisterStartupScript(this, GetType(), "jsalert", "<script>alert('第[" + i + "]行,[" + snCode + "]长度不为19码或16码')</script>", false);
            //            //ScriptManager.RegisterStartupScript(UpdatePanel2, UpdatePanel2.GetType(), "jsalert", "alert('第[" + i + "]行,[" + snCode + "]长度不为19码或16码');", true);
            //            Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]行,[" + snCode + "]长度不为19码或16码");
            //            return false;
            //        }
            //        //判断是否符合要求
            //        var msg = bcQry.CheckSNFormate(snCode).Trim();
            //        if (msg.Length != 6)
            //        {
            //            Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]行,[" + snCode + "]" + msg);
            //            return false;
            //        }
            //    }
            //}
            //不良品仓调入良品仓
            if (AlloSPType == "1")
            {
                if (bcQry.CheckSN_IsNew(snCode))
                {
                    Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg07 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg08 + "[" + snCode + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg09); //不良品仓调入良品仓，SN必须为新SN，第 //行, //已存在
                    return false;
                }
            }


            //判断数字
            var qty = dr["QUANTITY"].ToString();
            if (qty.IsNullOrEmpty())
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg10); //第  //行,数量为空
                return false;
            }
            //判断数量是否有效
            decimal num;
            try
            {
                num = decimal.Parse(qty);
            }
            catch (Exception ex)
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg08 + "[" + qty + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg11); //第 //行,//不是有效数字
                return false;
            }
            //检查数量是否正确
            string errmsg = string.Empty;
            if (!(DreamTek.ASRS.Business.Others.Comm_Function.Fun_IsDecimal(qty, 0, 0, 1, out errmsg)))
            {
                this.Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg08 + "[" + qty + "]" + errmsg);      //第   // 行,    
                return false;
            }
            //if (num <= 0)
            //{
            //    Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]行,[" + qty + "]必须大于0");
            //    return false;
            //}
            //if ((num - Math.Truncate(num)) > 0)
            //{
            //    Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]行,[" + qty + "]必须为正整数");
            //    return false;
            //}
            string errMsg = string.Empty;
            if (bcQry.CheckExistsSNQuantity(snCode, txtCPOSITIONCODE.Text,num, out errMsg))
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg08 + errMsg);  //第   // 行,    
                return false;
            }
            if (bcQry.CheckNotExistsSN(snCode, txtCTOPOSITIONCODE.Text, txtCPOSITIONCODE.Text, out errMsg))
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg08 + errMsg); //第   // 行,    
                return false;
            }

            #region 注销
            ////判断为入库SN，如果为入的SN，则判断数量是否一致
            //DataTable dt = ALLOCATE_DRule.GetSnFronIn(snCode);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    if (dt.Rows[0]["QTY"].ToDecimal() != num)
            //    {
            //        Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]行,[" + snCode + "][" + qty + "]必须与入库数量一致");
            //        return false;
            //    }
            //    ////入库未扣帐或者已经出库
            //    //OUTBILL_DRule listQuery = new OUTBILL_DRule();
            //    //DataTable dtRowCount = listQuery.GetSNList(snCode, "", "", "", true, -1, -1);
            //    //if (dtRowCount == null || dtRowCount.Rows.Count == 0 || dtRowCount.Rows[0][0].ToString().Trim().Equals("0"))
            //    //{
            //    //    Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]行,[" + snCode + "]对应入库单未扣帐或此SN已经出库");
            //    //    return false;
            //    //}
            //} 
            #endregion
            //判断SN是否入库
            if (bcQry.CheckSN_IN(snCode))
            {
                //判断数量是否一致
                decimal allsn = bcQry.Fun_GetNum_FromSN(snCode);
                //本单据数量
                //string bcID = dr["ID"].ToString();
                //decimal bcsl = ALLOCATE_DRule.GetAlloSNQty(bcID);
                //良品仓调拨至不良品仓，SN允许部分出库CQ 2014-5-6 16:48:24
                if (AlloSPType == "2")
                {
                    if (allsn < num)
                    {
                        Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg08 + "[" + qty + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg12); //第   // 行,     //不能大于SN数量
                        return false;
                    }
                }
                else
                {
                    
                    //if (allsn != num)
                    //{
                    //    Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]行,[" + qty + "]和SN数量不一致");
                    //    return false;
                    //}
                }
               
            }
           
            //判断栈板箱数量是否一致
            var strtype = dr["SNTYPE"].ToString();
            if (strtype == "1" || strtype == "2")
            {
                //获取栈板箱的数量
                decimal pcqty = bcQry.GetPallorCarQty(snCode);
                if (pcqty != num)
                {
                    Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg13);//第 //行,棧板箱不能修改數量！
                    return false;
                }
            }

            //判断输入的sn、栈板、箱是否已经扣帐
            if (acQry.IsBillSc(snCode))
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg08 + "[" + snCode + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg14);//已经存在未扣帐的扫描明细
                return false;
            }
            //判断是否存在sn一致，料号与调拨料号不一致的现象
            if (!bcQry.IsSnCinvCode(snCode, txtCINVCODE.Text.Trim()))
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg08 + "[" + snCode + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg15); //已经与其他料对应
                return false;
            }
            //判断储位CQ 2014-4-26 17:40:14
            string msgerr = string.Empty;
            if (bcQry.Fun_CheckSN_PositionCode(snCode, txtCINVCODE.Text.Trim(), txtCPOSITIONCODE.Text.ToUpper(),
                                                       WmsWebUserInfo.GetCurrentUser().UserNo, "3","","", out msgerr))
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg08 + ""+msgerr);
                return false;
            }
            //判断SN是否冻结CQ  2014-7-18 17:51:07
            if (bcQry.Fun_CheckLock_SN(txtCINVCODE.Text.Trim(), txtCPOSITIONCODE.Text.ToUpper(), snCode, 4, "",
                                               WmsWebUserInfo.GetCurrentUser().UserNo, "", "", out msgerr))
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg05 + "[" + i + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg08 + "" + msgerr + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg16);//不能调拨
                return false;
            }
        }
        //如果有相同的SN，则报错
        //判断数量合计
        var snQty = dtSN.AsEnumerable().Cast<DataRow>().Select(dr => decimal.Parse(dr["QUANTITY"].ToString())).Sum();
        var Qty = decimal.Parse(txtIQUANTITY.Text.Trim());
        if (!bcQry.IsCinSNPossible(txtCINVCODE.Text.Trim(), WmsWebUserInfo.GetCurrentUser().UserNo))//料号不必输SN
        {
            if (snQty > Qty)
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg17 + "[" + snQty + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg18+ "[" + Qty + "]!");  //SN数量总和  //大于明细数量
                return false;
            }
        }
        else
        {
            if (snQty != Qty)
            {
                Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg17 + "[" + snQty + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg19 + "[" + Qty + "]" + Resources.Lang.FrmALLOCATE_DEdit_SN_Msg20); //"SN数量总和[" + snQty + "]与明细数量[" + Qty + "]不一致!"); /
                return false;
            }
        }

        // WL 20160427 追加判断
        // 如果SN号不存在，输入的数量要少于旧库存数量
        decimal snOldQty = 0;
        foreach (DataRow dr in dtSN.Rows)
        {
            var snCode = dr["SN_CODE"].ToString().Trim().ToUpper();
            if (!bcQry.CheckSN_IN(snCode))
            {
                snOldQty = decimal.Parse(snOldQty.ToString()) + decimal.Parse(dr["QUANTITY"].ToString());
            }
        }
        if (snOldQty != 0)
        {
            //DataTable dtRow = ALLOCATE_DRule.GetDateCode(txtCPOSITIONCODE.Text, txtCINVCODE.Text);
            StockObject bo = bcQry.GetDateCode(txtCPOSITIONCODE.Text, txtCINVCODE.Text);
            if (bo!= null && !bo.id.IsNullOrEmpty())
            {
                if (snOldQty > bo.IQTY_Old)
                {
                    Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg21); //新的SN所对应的数量，超过旧库存量!
                    return false;
                }
            }
        }

        //重复SN判断
        var q =
            from p in dtSN.AsEnumerable().Cast<DataRow>()
            group p by p["SN_CODE"].ToString() into g
            select new { snCode = g.Key, snCount = g.Count() };

        if (q != null)
        {
            foreach (var gp in q)
            {
                if (gp.snCount > 1)
                {
                    Alert(Resources.Lang.FrmALLOCATE_DEdit_SN_Msg22 + gp.snCode + "!");//存在重复的SN：
                    return false;
                }
            }
        }
      
        return true;

    }


    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.btnSave.Enabled = false; //CQ 2013-5-13 12:07:58
        SaveToDB();
        this.btnSave.Enabled = true;
    }

    protected void btnNewSave_Click(object sender, EventArgs e)
    {
        if (CheckData())
        {
            lblSN.Text = string.Empty;
            var dtSN = GetGridViewData();
            int i = 0;
            bool bl = false;
            foreach (DataRow dr in dtSN.Rows)
            {
                i++;
                var snCode = dr["SN_CODE"].ToString().Trim().ToUpper();
                if (bcQry.CheckExistsSNInToPositionCode(snCode, txtCTOPOSITIONCODE.Text))
                {
                    bl = true;
                    lblSN.Text = snCode;
                    break;
                }
            }

            if (bl)
            {
                ModalPopupExtender.Show();
            }
            else
            {
                btnSave_Click(sender, e);
            }
        }
        
        //this.btnSave.Enabled = false; //CQ 2013-5-13 12:07:58
        //SaveToDB();
        //this.btnSave.Enabled = true;
    }


    protected void btnYesSave_Click(object sender, EventArgs e)
    {
        ModalPopupExtender.Hide();
        btnSave_Click(sender, e);
    }

    protected void btnNotSave_Click(object sender, EventArgs e)
    {
        ModalPopupExtender.Hide();
    }


    //新增网格行
    protected void btnNew_Click(object sender, EventArgs e)
    {
        DataTable table = GetGridViewData();
        DataRow newRow = table.NewRow();
        newRow["ID"] = Guid.NewGuid().ToString();
        table.Rows.Add(newRow);
        grdSNDetial.DataSource = table;
        grdSNDetial.DataBind();
    }
    //删除网格行
    protected void btnDeleteSn_Click(object sender, EventArgs e)
    {
        DataTable table = GetGridViewData();

        for (int i = 0; i < grdSNDetial.Rows.Count; i++)
        {
            if (this.grdSNDetial.Rows[i].Cells[0].FindControl("chkSelect") is CheckBox)
            {
                CheckBox chkSelect = (CheckBox)this.grdSNDetial.Rows[i].Cells[0].FindControl("chkSelect"); //20130614093155
                if (chkSelect.Checked)
                {
                    foreach (DataRow dtRow in table.Rows)
                    {
                        if (dtRow["ID"].ToString() == this.grdSNDetial.DataKeys[i].Values[0].ToString())
                        {
                            table.Rows.Remove(dtRow);
                            break;
                        }
                    }
                }
            }
        }

        grdSNDetial.DataSource = table;
        grdSNDetial.DataBind();
    }

    /// 保存数据到数据库
    /// <summary>
    /// 保存数据到数据库
    /// </summary>
    public void SaveToDB()
    {
        if (CheckData())
        {
            //DBUtil.BeginTrans();
            try
            {
                DataTable tb = GetGridViewData();
                
                //先删除后保存
                //ALLOCATE_DRule allocate_d_sn = new ALLOCATE_DRule();
                acQry.DeleteSn(txtIDS.Text.Trim());

                IGenericRepository<ALLOCATE_D_SN> conn = new GenericRepository<ALLOCATE_D_SN>(db);
                IGenericRepository<STOCK_CURRENT_SN> conn_sn = new GenericRepository<STOCK_CURRENT_SN>(db);

                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    ALLOCATE_D_SN entity = new ALLOCATE_D_SN();
                    
                    string sncode = tb.Rows[i]["SN_CODE"].ToString().ToUpper();

                    STOCK_CURRENT_SN stSN = (from p in conn_sn.Get()
                                            where p.sncode.ToUpper() == sncode
                                            select p).FirstOrDefault();

                    entity.id = tb.Rows[i]["ID"].ToString();
                    entity.allocate_id = txtID.Text;
                    entity.allocate_d_ids = txtIDS.Text;
                    entity.sn_code = sncode;
                    entity.cinvcode = txtCINVCODE.Text;
                    entity.quantity = Convert.ToDecimal(tb.Rows[i]["QUANTITY"]);
                    entity.createtime = DateTime.Now;
                    entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.lastvpdtime = DateTime.Now;
                    entity.lastvpdowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.worktype = "0";
                    entity.datecode = stSN.datecode.Value;
                    entity.RULECODE = stSN.RULECODE;  //---BUCKINGHA-838 条码管理修改CH
                    var sntype = ((Label)this.grdSNDetial.Rows[i].FindControl("labtype")).Text;

                    if (this.ConFigvalue.Equals("1") && stSN != null && !stSN.ids.IsNullOrEmpty())
                    {
                        //000002   0 SN管理  1 批次管理  2 箱
                        entity.sntype = 3;
                        entity.datecode = stSN.datecode.Value;
                        entity.furnaceno = stSN.furnaceno;

                    }
                    else
                    {
                        entity.sntype = 0;

                        //switch (sntype)
                        //{
                        //    case "SN":
                        //        sntype = "0";
                        //        break;
                        //    case "栈板":
                        //        sntype = "1";
                        //        break;
                        //    case "箱":
                        //        sntype = "2";
                        //        break;
                        //}
                        //if (sntype == "")
                        //{
                        //sntype = bcQry.GetSNType(entity.sn_code);
                        //}
                       // sntype = GetConfigSNValue();
                       
                        ///取消下面的逻辑，直接取库存的datacode ----20190719
                        ////针对SN
                        //if (entity.sntype == 0)
                        //{
                        //    //CQ 2014-4-10 14:12:52
                        //    if (AlloSPType == "1")
                        //    {
                        //        decimal dc = 0;
                        //        if (decimal.TryParse(bcQry.CheckSNFormate(sncode).Trim(), out dc))
                        //        {
                        //            entity.datecode = dc;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        string pDateCode = string.Empty;
                        //        decimal dc = 0;
                        //        if (bcQry.Fun_IsExist_SN(txtCINVCODE.Text.Trim().ToUpper(), sncode, WmsWebUserInfo.GetCurrentUser().UserNo, "", "", out pDateCode))
                        //        {
                        //            if (decimal.TryParse(pDateCode, out dc))
                        //            {
                        //                entity.datecode = dc;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            pDateCode = bcQry.GetSNCode(sncode).Trim();
                        //            if (decimal.TryParse(pDateCode, out dc))
                        //            {
                        //                entity.datecode = dc;
                        //            }
                        //            //entity.datecode = bcQry.CheckSNFormate(sncode).Trim();
                        //        }
                        //    }
                        //}
                        ///取消下面的逻辑，直接取库存的datacode ----20190719
                    }
                    //ALLOCATE_D_SNRule.Insert(entity);
                    conn.Insert(entity);
                    conn.Save();
                }
                //DBUtil.Commit();
                this.AlertAndBack("FrmALLOCATE_DEdit_SN.aspx?" + BuildQueryString(SYSOperation.Modify, txtID.Text.Trim()) + "&IDS=" + txtIDS.Text.Trim(), Resources.Lang.Common_SuccessSave); //保存成功
            }
            catch (Exception err)
            {
                Alert(err.Message);
                //DBUtil.Rollback();
            }
        }
    }

    //获取网格数据
    private DataTable GetGridViewData()
    {
        var dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID"));
        dt.Columns.Add(new DataColumn("SN_CODE"));
        dt.Columns.Add(new DataColumn("QUANTITY"));
        dt.Columns.Add(new DataColumn("SNTYPE"));
        for (int i = 0; i < grdSNDetial.Rows.Count; i++)
        {
            DataRow sourseRow = dt.NewRow();
            sourseRow["ID"] = this.grdSNDetial.DataKeys[i].Values[0].ToString();
            sourseRow["SN_CODE"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtSN")).Text;
            sourseRow["QUANTITY"] = ((TextBox)this.grdSNDetial.Rows[i].FindControl("txtQty")).Text;
            if (string.IsNullOrEmpty(sourseRow["SNTYPE"].ToString()) && !string.IsNullOrEmpty(sourseRow["SN_CODE"].ToString()))
            {
                sourseRow["SNTYPE"] = this.GetConfigSNName();

            }
            dt.Rows.Add(sourseRow);
        }
        return dt;
    }

   

    //网格绑定
    protected void grdSNDetial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtSN = e.Row.FindControl("txtSN") as TextBox;
            TextBox txtQty = e.Row.FindControl("txtQty") as TextBox;
            //类型
            var labtype = e.Row.FindControl("labtype") as Label;
            //if (string.IsNullOrEmpty(labtype.Text) && !string.IsNullOrEmpty(txtSN.Text.Trim()))
            //{
            //    labtype.Text = this.GetSNType(txtSN.Text.Trim());
            //}

            //转换


            labtype.Text = GetConfigSNName();

            //if (labtype.Text.Trim() == "0")
            //{
            //    labtype.Text = "SN";
            //}
            //else if (labtype.Text.Trim() == "1")
            //{
            //    labtype.Text = "栈板";
            //}
            //else if (labtype.Text.Trim() == "2")
            //{
            //    labtype.Text = "箱";
            //}

            //showSn.SetSN = txtSN.ClientID;
            //showSn.SetQty = txtQty.ClientID;
            //if (AlloSPType != "1")
            //{
            //  txtSN.Attributes["onclick"] = "ShowNewAll('" + showSn.GetDivName + "','" + txtSN.ClientID + "','" + txtQty.ClientID + "','" + labtype.ClientID + "','" + txtCINVCODE.Text.Trim() + "');";
            //    //    
            //}
        }
    }

    protected void grdSNDetial_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorSNDetial.IsDbPager)
        //{
        //    grdNavigatorSNDetial.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdSNDetial.PageIndex = e.NewPageIndex;
        //}
    }
    protected void grdSNDetial_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataToGridView();
    }

}