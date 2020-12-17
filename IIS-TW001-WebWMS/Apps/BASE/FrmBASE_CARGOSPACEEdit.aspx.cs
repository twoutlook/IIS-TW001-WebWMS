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
using System.Text;

/// <summary>
/// 描述: 储位详情-->FrmBASE_CARGOSPACEEdit 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-10-09 18:25:13
/// </summary>
public partial class BASE_FrmBASE_CARGOSPACEEdit : WMSBasePage
{
    #region SQL
    //DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        //ucShowWAREHOUSEDiv.SetCompName = txtWAREHOUSE_Name.ClientID;
        //ucShowWAREHOUSEDiv.SetORGCode = txthfWareHouseId.ClientID;

        ucShowArea.SetCompName = txtCDEFINE1.ClientID;
        ucShowArea.SetORGCode = txtAreaID.ClientID;
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() == SYSOperation.New)
            {
                txtCCARGOID.Enabled = true;
                txtCCARGONAME.Enabled = true;
            }
            else
            {
                txtCCARGOID.Enabled = false;
                txtCCARGONAME.Enabled = false;
                ShowData();
            }
        }
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
    }

    #region IPage 成员
    public string ASRSFig
    {
        get
        {
            if (ViewState["ASRSFig"] != null)
            {
                return ViewState["ASRSFig"].ToString();
            }
            return "";
        }
        set { ViewState["ASRSFig"] = value; }
    }

    public DataTable GetLineID()
    {
        string sql = @"select distinct t.CRANENAME as FUNCNAME,t.CRANEID as EXTEND1  from  BASE_CRANECONFIG t  order by t.CRANEID asc ";

        return DBHelp.ExecuteToDataTable(sql);
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_CARGOSPACE');return false;";
        //this.txtWAREHOUSE_Name.Attributes["onclick"] = "Show('" + ucShowWAREHOUSEDiv.GetDivName + "');";
        txtCDEFINE1.Attributes["onclick"] = "Show('" + ucShowArea.GetDivName + "');";
        Help.DropDownListDataBind(GetLineID(), this.ddlLineID, Resources.Lang.Common_ALL, "FUNCNAME", "EXTEND1", "");
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "CARGOSPACETYPE", false, -1, -1), this.DropCTYPE, Resources.Lang.Commona_PleaseSelect, "FLAG_NAME", "FLAG_ID", ""); //请选择
        Help.DropDownListDataBind(GetWareHouse(), this.ddlWareHouse, Resources.Lang.Common_ALL, "cwarename", "id", "");
        //删除确认提示
        if (this.Operation() == SYSOperation.Modify)
        {
            this.btnDelete.Attributes["onclick"] = "var userNo = '" + this.KeyID + "'; return window.confirm('" + Resources.Lang.FrmBASE_CLIENTEdit_Msg01 + "' + userNo + '?');"; //要删除
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
        //是否显示ASRS 1显示 0 不显示
        IGenericRepository<SYS_CONFIG> con = new GenericRepository<SYS_CONFIG>(context);
        var caseList = from p in con.Get()
                       where p.code == "000006"
                       select p.config_value;
        if (caseList != null && caseList.Count() > 0)
            ASRSFig = caseList.ToList()[0];

        if (ASRSFig == "1")
        {
            Lab_X.Visible = true;
            Lab_Y.Visible = true;
            Lab_Z.Visible = true;
            Lab_Ctype.Visible = true;
        }
        else
        {
            Lab_X.Visible = false;
            Lab_Y.Visible = false;
            Lab_Z.Visible = false;
            Lab_Ctype.Visible = false;
        }

        Help.DropDownListDataBind(GetParametersByFlagType("C"), ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(GetParametersByFlagType("BASE_CARGOSPACE.IS_ALLO"), ddlallo, "", "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(GetParametersByFlagType("AGVSITE"), Droppalletcode, "", "FLAG_NAME", "FLAG_ID", "");//是否存在栈板
    }

    #endregion

    public DataTable GetWareHouse()
    {
        string sql = @"SELECT id,cwareid,cwarename FROM dbo.BASE_WAREHOUSE WITH(NOLOCK)  ORDER BY cwareid DESC";
        return DBHelp.ExecuteToDataTable(sql);
    }

    public string GetWorkTypeWareHouse(string id)
    {
        string sql = string.Format(@"SELECT cdefine2 FROM BASE_WAREHOUSE WHERE id='{0}'", id);
        return DBHelp.ExecuteScalar(sql);
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<V_BASE_CARGOSPACEListQuery> con = new GenericRepository<V_BASE_CARGOSPACEListQuery>(context);
        V_BASE_CARGOSPACEListQuery entity = new V_BASE_CARGOSPACEListQuery();
        var caseList = from p in con.Get()
                       where p.id == this.KeyID
                       select p;
        if (caseList != null && caseList.Count() > 0)
        {
            entity = caseList.ToList().FirstOrDefault<V_BASE_CARGOSPACEListQuery>();
            this.txtCCARGOID.Text = entity.cpositioncode;
            this.txtCCARGONAME.Text = entity.cposition;
            this.txtIMAXCAPACITY.Text = entity.imaxcapacity.ToString();
            this.txtCALIAS.Text = entity.calias;
            this.txtCERPCODE.Text = entity.cerpcode;
            DropCTYPE.SelectedValue = entity.ctype;
            Session["DropCTYPEs"] = entity.ctype;

            this.ddlWareHouse.SelectedValue = entity.wID;
            this.txthfWareHouseId.Text = entity.wID;
            //this.hfHouseID.Value = entity.wID;
            this.txtIPRIORITY.Text = entity.ipriority.ToString();
            if (entity.dexpiredate != null && !string.IsNullOrEmpty(entity.dexpiredate.ToString()))
            {
                this.txtDEXPIREDATE.Text = DateTime.Parse(entity.dexpiredate.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            this.txtILENGTH.Text = entity.ilength.ToString();
            this.txtIWIDTH.Text = entity.iwidth.ToString();
            this.txtIHEIGHT.Text = entity.iheight.ToString();
            this.txtIVOLUME.Text = entity.ivolume.ToString();
            this.txtCUSETYPE.Text = entity.cusetype;
            this.cboIPERMITMIX.Checked = entity.ipermitmix.ToString() == "1" ? true : false;
            this.txtCX.Text = entity.cx;
            this.txtCY.Text = entity.cy;
            this.txtCZ.Text = entity.cz;
            this.txtCMEMO.Text = entity.cmemo;
            this.ddlCSTATUS.SelectedValue = entity.cstatus;
            this.ddlallo.SelectedValue = entity.is_allo.ToString();
            this.txtID.Text = entity.id;
            this.txtProductCode.Text = entity.productcode;//产品编码
            string areaid = entity.cdefine1;
            if (entity.createtime != null && !string.IsNullOrEmpty(entity.createtime.ToString()))
            {
                this.txtcreatetime.Text = DateTime.Parse(entity.createtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (entity.lastupdatetime != null && !string.IsNullOrEmpty(entity.lastupdatetime.ToString()))
            {
                this.txtupdatetime.Text = DateTime.Parse(entity.lastupdatetime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            txtcreateuser.Text = OPERATOR.GetUserNameByAccountID(entity.createowner);
            txtupdateuser.Text = OPERATOR.GetUserNameByAccountID(entity.lastupdateowner);
            IGenericRepository<BASE_AREA> area = new GenericRepository<BASE_AREA>(context);
            var area_id = from p in area.Get()
                          where p.id == areaid
                          select p.area_name;
            if (area_id != null && area_id.Count() > 0)
                this.txtCDEFINE1.Text = area_id.ToList()[0];
            Droppalletcode.SelectedValue = entity.pallet_code;
            this.ddlLineID.SelectedValue = entity.lineid;// dt.Rows[0]["LINEID"].ToString();
            Session["ddlLineID"] = entity.lineid;

            txtAreaID.Text = areaid;
            IGenericRepository<BASE_WAREHOUSE> wareCon = new GenericRepository<BASE_WAREHOUSE>(context);
            var wareHouse = (from p in wareCon.Get()
                             where p.id == this.ddlWareHouse.SelectedValue
                             select p).FirstOrDefault<BASE_WAREHOUSE>();

            if (wareHouse != null && wareHouse.cdefine2 == "0")
            {
                lab_xyz.Visible = false;
                Lab_Line.Visible = false;
                Lab_Ctype.Visible = false;
            }
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        ASCIIEncoding strData = new ASCIIEncoding();
        //
        if (this.txtCCARGOID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg01); //编号项不允许空！
            this.SetFocus(txtCCARGOID);
            return false;
        }
        //
        if (this.txtCCARGOID.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCCARGOID.Text).Length > 30)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg04);//编码项超过指定的长度30！
                this.SetFocus(txtCCARGOID);
                return false;
            }
        }
       
        if (this.Operation() == SYSOperation.New)
        {
            //检查编号是否重复
            txtCCARGOID.Text = txtCCARGOID.Text.Trim().ToUpper();
            IGenericRepository<BASE_CARGOSPACE> con = new GenericRepository<BASE_CARGOSPACE>(context);
            var caseList = from p in con.Get()
                           where p.cpositioncode == txtCCARGOID.Text
                           select p;
            if (caseList != null && caseList.Count() > 0)
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg02);//储位编码重复,不能保存！
                this.SetFocus(txtCCARGOID);
                return false;
            }

        }

       

        //
        if (this.txtCCARGONAME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg03);//名称项不允许空！
            this.SetFocus(txtCCARGONAME);
            return false;
        }
        //
        if (this.txtCCARGONAME.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCCARGONAME.Text).Length > 100)
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg04);//名称项超过指定的长度100！
                this.SetFocus(txtCCARGONAME);
                return false;
            }
        }
        //
        if (this.txtIMAXCAPACITY.Text.Trim().Length > 0)
        {
            if (!IsValidNum(txtIMAXCAPACITY.Text))
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg05);//最大量不是有效的十进制数字！
                this.SetFocus(txtIMAXCAPACITY);
                return false;
            }
        }
        //
        if (this.txtCALIAS.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCALIAS.Text).Length > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg06);//助记码项超过指定的长度20！
                this.SetFocus(txtCALIAS);
                return false;
            }
        }
        //
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCERPCODE.Text).Length > 50)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg10);//ERP编码项超过指定的长度50！
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }
        ////

        //
        if (string.IsNullOrEmpty(this.ddlWareHouse.SelectedValue))
        {
            this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg09); //所属仓库ID项不允许空！
            this.SetFocus(this.ddlWareHouse);
            return false;
        }
        //
        //if (this.hfWareHouseId.Value.Trim().Length > 0)
        //{
        //    if (this.hfWareHouseId.Value.GetLengthByByte() > 50)
        //    {
        //        this.Alert("所属仓库ID项超过指定的长度50！");
        //        this.SetFocus(txtWAREHOUSE_Name);
        //        return false;
        //    }
        //}

        //
        if (this.txtIPRIORITY.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg10);//优先级项不允许空！
            this.SetFocus(txtIPRIORITY);
            return false;
        }
        //
        if (this.txtIPRIORITY.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDecimal(this.txtIPRIORITY.Text) == false)
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg11); //"优先级项不是有效的十进制数字！"
                this.SetFocus(txtIPRIORITY);
                return false;
            }
        }
        if (this.txtILENGTH.Text.Trim().Length > 0)
        {
            
            if (!IsValidNum(txtILENGTH.Text))
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg12);//长项不是有效的十进制数字！
                this.SetFocus(txtILENGTH);
                return false;
            }
        }
        //
        if (this.txtIWIDTH.Text.Trim().Length > 0)
        {
            if (!IsValidNum(txtIWIDTH.Text))
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg13);//宽项不是有效的十进制数字！
                this.SetFocus(txtIWIDTH);
                return false;
            }
        }
        //
        if (this.txtIHEIGHT.Text.Trim().Length > 0)
        {
            if (!IsValidNum(txtIHEIGHT.Text))
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg14);//高项不是有效的十进制数字！
                this.SetFocus(txtIHEIGHT);
                return false;
            }
        }
        //
        if (this.txtIVOLUME.Text.Trim().Length > 0)
        {
            if (!IsValidNum(txtIVOLUME.Text))
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg15);//体积项不是有效的十进制数字！
                this.SetFocus(txtIVOLUME);
                return false;
            }
        }
        //
        if (this.txtCUSETYPE.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCUSETYPE.Text).Length > 50)
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg16); //用途项超过指定的长度50！
                this.SetFocus(txtCUSETYPE);
                return false;
            }
        }

        IGenericRepository<BASE_WAREHOUSE> wareCon = new GenericRepository<BASE_WAREHOUSE>(context);
        var wareHouse =(from p in wareCon.Get()
                        where p.id == this.ddlWareHouse.SelectedValue
                       select p).FirstOrDefault<BASE_WAREHOUSE>();


        if (wareHouse.cdefine2 == "1")
        {

            if (string.IsNullOrEmpty(this.ddlLineID.SelectedValue))
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg17);//线别不允许空！
                this.SetFocus(ddlLineID);
                return false;
            }

            if (ASRSFig == "1")
            {

                #region ASRS
                if (this.DropCTYPE.SelectedValue == "")
                {
                    this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg18);//种类项未设置！
                    this.SetFocus(DropCTYPE);
                    return false;
                }

                if (this.txtCX.Text.Trim().Length > 0)
                {
                    if (strData.GetBytes(txtCX.Text).Length != 2)
                    {
                        this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg19); //x项指定的长度为2位！
                        this.SetFocus(txtCX);
                        return false;
                    }
                    //if (txtCX.Text.Trim() != "01" && txtCX.Text.Trim() != "02")
                    //{
                    //    this.Alert("x项必须为01或02！");
                    //    this.SetFocus(txtCX);
                    //    return false;
                    //}
                }
                else
                {
                    this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg20); //x项不允许为空！
                    this.SetFocus(txtCX);
                    return false;
                }

                //
                if (this.txtCY.Text.Trim().Length > 0)
                {
                    //if (this.txtCY.Text.Trim().GetLengthByByte() != 3)
                    if (strData.GetBytes(txtCY.Text).Length != 3)
                    {
                        this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg21);//y项指定的长度为3位！
                        this.SetFocus(txtCY);
                        return false;
                    }
                    int YNum = 0;
                    //检查Y
                    try
                    {
                        YNum = Convert.ToInt32(this.txtCY.Text.Trim());
                        if (YNum < 1 || YNum > 249)
                        {
                            this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg22);//y项范围为001至249！
                            this.SetFocus(txtCY);
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg23);//y项不是三位数字类型！
                        this.SetFocus(txtCY);
                        return false;
                    }
                }
                else
                {
                    this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg24);//y项不允许为空！
                    this.SetFocus(txtCY);
                    return false;
                }
                //
                if (this.txtCZ.Text.Trim().Length > 0)
                {
                    if (strData.GetBytes(txtCZ.Text).Length != 2)
                    {
                        this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg25); //z项指定的长度为2位！
                        this.SetFocus(txtCZ);
                        return false;
                    }
                    int ZNum = 0;
                    //检查Y
                    try
                    {
                        ZNum = Convert.ToInt32(this.txtCZ.Text.Trim());

                        if (ZNum < 1 || ZNum > 50)
                        {
                            this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg26); //z项范围为01至50！
                            this.SetFocus(txtCZ);
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg27);//z项不是两位数字类型！
                        this.SetFocus(txtCZ);
                        return false;
                    }
                }
                else
                {
                    this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg28);//z项不允许为空！
                    this.SetFocus(txtCZ);
                    return false;
                }
                #endregion

                #region 判断列格层是否重复
                if (context.BASE_CARGOSPACE.Any(x => x.cx == this.txtCX.Text.Trim()
                                                  && x.cy == this.txtCY.Text.Trim()
                                                  && x.cz == this.txtCZ.Text.Trim()
                                                  && x.cpositioncode != txtCCARGOID.Text.Trim()
                                                  && x.lineid == ddlLineID.SelectedValue))
                {
                    this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg29);//该线路已存在相同x项y项z项的数据！
                    this.SetFocus(txtCZ);
                    return false;
                }

                #endregion
            }
            else
            {
                #region 其他
                if (this.txtCX.Text.Trim().Length > 0)
                {
                    if (strData.GetBytes(txtCX.Text).Length > 20)
                    {
                        this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg30); //x项超过指定的长度20！
                        this.SetFocus(txtCX);
                        return false;
                    }
                }
                //
                if (this.txtCY.Text.Trim().Length > 0)
                {
                    if (strData.GetBytes(txtCY.Text).Length > 20)
                    {
                        this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg31); //y项超过指定的长度20！
                        this.SetFocus(txtCY);
                        return false;
                    }
                }
                //
                if (this.txtCZ.Text.Trim().Length > 0)
                {
                    if (strData.GetBytes(txtCZ.Text).Length > 20)
                    {
                        this.Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg32);//z项超过指定的长度20！
                        this.SetFocus(txtCZ);
                        return false;
                    }
                }
                #endregion
            }
        }

        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (strData.GetBytes(txtCMEMO.Text).Length > 200)
            {
                this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg15); //备注项超过指定的长度200！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        return true;

    }
    public bool IsValidNum(string text)
    {
        bool b = true;
        string message = string.Empty;
        try
        {
            decimal number = decimal.Parse(text);
            if (number <= 0)
                b = false;
        }
        catch
        {
            b = false;
        }
        return b;
    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_CARGOSPACE SendData()
    {
        BASE_CARGOSPACE entity = new BASE_CARGOSPACE();
        if (this.Operation() == SYSOperation.Modify)
        {
            IGenericRepository<BASE_CARGOSPACE> con = new GenericRepository<BASE_CARGOSPACE>(context);
            var caseList = from p in con.Get()
                           where p.id == this.KeyID
                           select p;
            if (caseList != null && caseList.Count() > 0)
                entity = caseList.ToList().FirstOrDefault();
        }
        this.txtCCARGOID.Text = this.txtCCARGOID.Text.Trim().ToUpper();
        if (this.txtCCARGOID.Text.Length > 0)
        {
            entity.cpositioncode = txtCCARGOID.Text;
        }
        //
        this.txtCCARGONAME.Text = this.txtCCARGONAME.Text.Trim();
        if (this.txtCCARGONAME.Text.Length > 0)
        {
            entity.cposition = txtCCARGONAME.Text;
        }
        //
        this.txtIMAXCAPACITY.Text = this.txtIMAXCAPACITY.Text.Trim();
        if (this.txtIMAXCAPACITY.Text.Length > 0)
        {
            entity.imaxcapacity = txtIMAXCAPACITY.Text.ToDecimal();
        }
        //
        this.txtCALIAS.Text = this.txtCALIAS.Text.Trim();
        if (this.txtCALIAS.Text.Length > 0)
        {
            entity.calias = txtCALIAS.Text;
        }
        //this.txtCALIAS.Text = this.txtCALIAS.Text.Trim();
        //if (this.txtCALIAS.Text.Length > 0)
        //{
        //    entity.lineid = txtCALIAS.Text;
        //}

        entity.lineid = ddlLineID.SelectedValue;
        //
        this.txtCERPCODE.Text = this.txtCERPCODE.Text.Trim();
        if (this.txtCERPCODE.Text.Length > 0)
        {
            entity.cerpcode = txtCERPCODE.Text;
        }
        entity.ctype = DropCTYPE.SelectedValue;

        this.txthfWareHouseId.Text = this.ddlWareHouse.SelectedValue;
        if (this.txthfWareHouseId.Text.Length > 0)
        {
            entity.warehouseid = txthfWareHouseId.Text;
        }
        this.txtIPRIORITY.Text = this.txtIPRIORITY.Text.Trim();

        if (this.txtIPRIORITY.Text.Length > 0)
        {
            entity.ipriority = txtIPRIORITY.Text.ToDecimal();
        }

        this.txtDEXPIREDATE.Text = this.txtDEXPIREDATE.Text.Trim();

        if (this.txtDEXPIREDATE.Text.Length > 0)
        {
            entity.dexpiredate = txtDEXPIREDATE.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
     
        this.txtILENGTH.Text = this.txtILENGTH.Text.Trim();
        if (this.txtILENGTH.Text.Length > 0)
        {
            entity.ilength = txtILENGTH.Text.ToDecimal();
        }

        this.txtIWIDTH.Text = this.txtIWIDTH.Text.Trim();
        if (this.txtIWIDTH.Text.Length > 0)
        {
            entity.iwidth = txtIWIDTH.Text.ToDecimal();
        }

        this.txtIHEIGHT.Text = this.txtIHEIGHT.Text.Trim();
        if (this.txtIHEIGHT.Text.Length > 0)
        {
            entity.iheight = txtIHEIGHT.Text.ToDecimal();
        }

        this.txtIVOLUME.Text = this.txtIVOLUME.Text.Trim();
        if (this.txtIVOLUME.Text.Length > 0)
        {
            entity.ivolume = txtIVOLUME.Text.ToDecimal();
        }

        this.txtCUSETYPE.Text = this.txtCUSETYPE.Text.Trim();
        if (this.txtCUSETYPE.Text.Length > 0)
        {
            entity.cusetype = txtCUSETYPE.Text;
        }

        entity.ipermitmix = cboIPERMITMIX.Checked ? 1 : 0;

        this.txtCX.Text = this.txtCX.Text.Trim();
        if (this.txtCX.Text.Length > 0)
        {
            entity.cx = txtCX.Text;
        }

        this.txtCY.Text = this.txtCY.Text.Trim();
        if (this.txtCY.Text.Length > 0)
        {
            entity.cy = txtCY.Text;
        }

        this.txtCZ.Text = this.txtCZ.Text.Trim();
        if (this.txtCZ.Text.Length > 0)
        {
            entity.cz = txtCZ.Text;
        }

        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
       
        if (this.txtProductCode.Text.Length > 0)
        {
            entity.productcode = txtProductCode.Text;
        }

        txtCDEFINE1.Text = txtCDEFINE1.Text.Trim();
        if (txtCDEFINE1.Text.Length > 0)
        {
            entity.cdefine1 = txtAreaID.Text;
        }
        else
        {
            entity.cdefine1 = "";
        }
        if (!string.IsNullOrEmpty(this.txtcreatetime.Text))
        {
            entity.createtime = Convert.ToDateTime(this.txtcreatetime.Text.Trim());
        }
        entity.createowner = OPERATOR.GetUserNameByAccountID(this.txtcreateuser.Text.Trim());
        entity.cstatus = ddlCSTATUS.SelectedValue;
        entity.is_allo = Convert.ToDecimal(ddlallo.SelectedValue);
        entity.pallet_code = Droppalletcode.SelectedValue;
        return entity;
    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CARGOSPACE> bcConn = new GenericRepository<BASE_CARGOSPACE>(context);
        if (this.CheckData())
        {
            BASE_CARGOSPACE entity = this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";
            try
            {
                if (this.Operation() == SYSOperation.Modify)
                {

                    IGenericRepository<V_SUM_STOCKCURRENT> con = new GenericRepository<V_SUM_STOCKCURRENT>(context);
                    var caseList = from p in con.Get()
                                   where p.cpositioncode == txtCCARGOID.Text
                                   select p.SUMKC;
                    bool b = false;
                    if (caseList != null && caseList.Count() > 0)
                    {
                        if (caseList.ToList()[0].ToString() != "0")
                            b = true;
                    }
                    if (b && hfHouseID.Value != txthfWareHouseId.Text)
                    {
                        Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg33);//该储位有库存，不能修改所属仓库!
                    }
                    else
                    {
                        strKeyID = txtID.Text.Trim();
                        entity.id = strKeyID;
                        entity.lastupdatetime = DateTime.Now;
                        entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                        bcConn.Update(entity);

                       string dt= Session["DropCTYPEs"].ToString();
                      
                        IGenericRepository<V_STOCK_CURRENT_List> cons = new GenericRepository<V_STOCK_CURRENT_List>(context);
                        var   ca=cons.Get().Where(p=>p.cpositioncode==txtCCARGOID.Text && p.iqty > 0);
                        if (ca.Count() > 0 && dt!=entity.ctype)
                        {
                            Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg34);//该储位有库存，不能修改种类（储位）!
                        }
                        else if (ca.Count() > 0 && Session["ddlLineID"].ToString() != entity.lineid)
                        {
                            Alert(Resources.Lang.FrmBASE_CARGOSPACEEdit_Msg35);//该储位有库存，不能修改线别!
                        }
                        else
                        {
                            bcConn.Save();
                            //this.AlertAndBack("FrmBASE_CARGOSPACEEdit.aspx?Flag=1" + BuildQueryString(SYSOperation.Modify, strKeyID), "修改成功！");
                            this.AlertAndBack("FrmBASE_CARGOSPACEEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                        }

                        
                   
                    }

                }
                else if (this.Operation() == SYSOperation.New)
                {
                    entity.createtime = DateTime.Now;
                    strKeyID = Guid.NewGuid().ToString();
                    entity.id = strKeyID;
                    entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.lastupdatetime = DateTime.Now;
                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    bcConn.Insert(entity);
                    bcConn.Save();
                    //this.AlertAndBack("FrmBASE_CARGOSPACEEdit.aspx?Flag=1" + BuildQueryString(SYSOperation.New, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
                    this.AlertAndBack("FrmBASE_CARGOSPACEEdit.aspx?" + BuildQueryString(SYSOperation.New, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                }
            }
            catch (Exception E)
            {
                // this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATEEdit_Msg17 + E.Message); //"失败！" 
                this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg17 + E.Message); //"失败！"
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
//        BASE_CARGOSPACEEntity entity = new BASE_CARGOSPACEEntity();
//        try
//        {
//            entity.ID = this.KeyID.ToString();
//            BASE_CARGOSPACERule.Delete(entity);
//        }
//        catch (Exception E)
//        {
//            this.Alert("删除失败！" + E.Message);
//#if Debug 
//            this.Response.Write(entity.DBAccess().GetLastSQL());                
//#endif
        //}

    }
    #endregion
   
    protected void ddlWareHouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        var id = this.ddlWareHouse.SelectedValue;
        this.txthfWareHouseId.Text = this.ddlWareHouse.SelectedValue;
        var workType=GetWorkTypeWareHouse(id);
        if (workType == "0")
        {
            this.lab_xyz.Visible = false;
            this.Lab_Line.Visible = false;
            this.Lab_Ctype.Visible = false;
        }
        else
        {
            this.lab_xyz.Visible = true;
            this.Lab_Line.Visible = true;
            this.Lab_Ctype.Visible = true;
        }
    }
}


