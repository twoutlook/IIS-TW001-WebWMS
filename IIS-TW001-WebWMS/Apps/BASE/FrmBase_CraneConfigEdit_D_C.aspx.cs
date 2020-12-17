using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Text.RegularExpressions;
using DreamTek.ASRS.Business.Base;

public partial class BASE_FrmBase_CraneConfigEdit_D_C : WMSBasePage
{


    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();

            if (this.Operation() != SYSOperation.New)
            {
                ShowData(this.KeyID);
                //this.txtSiteID.Enabled = false;
                this.txtScanID.Enabled = false;
            }
            else
            {
                txtcreateuser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtcreatetime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtupdateuser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtupdatetime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindLine(ddlScanType.SelectedValue);
                BindSite(ddlLineID.SelectedValue);
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
        //BASE_CraneDetialScan
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnScanSearch'].click(); CloseMySelf('BASE_CraneDetialScan');return false;";
        //this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_CRANECONFIG_DETIAL_Scan');return false;";
        //删除确认提示
        if (this.Operation() == SYSOperation.Modify)
        {
            this.btnDelete.Attributes["onclick"] = "var userNo = '" + this.KeyID + "'; return window.confirm('" + Resources.Lang.FrmBASE_CLIENTEdit_Msg01 + "' + userNo + '?');";//要删除
        }
        else
        {
            this.btnDelete.Visible = false;
        }

        Help.DropDownListDataBind(GetParametersByFlagType("BASE_CLIENT"), dplCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");//状态
        var ScanTypes = GetParametersByFlagType("PLCType");

        //是否配置了RGV
        var isNeedRGV = this.GetConFig("140111");
        if (isNeedRGV != "1")
        {
            var rgv = ScanTypes.Where(x => x.FLAG_ID == "RGV").FirstOrDefault();
            if (rgv != null)
            {
                ScanTypes.Remove(rgv);
            }
        }

        var Agv = ScanTypes.Where(x => x.FLAG_ID == "AGV").FirstOrDefault();
        if (Agv != null)
        {
            ScanTypes.Remove(Agv);
        }

        //是否配置了SJJ
        var isNeedSJJ = this.GetConFig("140117");
        if (isNeedSJJ != "1")
        {
            var sjj = ScanTypes.Where(x => x.FLAG_ID == "SJJ").FirstOrDefault();
            if (sjj != null)
            {
                ScanTypes.Remove(sjj);
            }
        }
        //是否配置有台车
        var isNeedCar = this.GetConFig("140112");
        if (isNeedCar != "1")
        {
            var car = ScanTypes.Where(x => x.FLAG_ID == "CAR").FirstOrDefault();
            if (car != null)
            {
                ScanTypes.Remove(car);
            }
        }
        //是否配置了输送线
        var isNeedSSX = this.GetConFig("140119");
        if (isNeedSSX != "1")
        {
            var ssx = ScanTypes.Where(x => x.FLAG_ID == "SSX").FirstOrDefault();
            if (ssx != null)
            {
                ScanTypes.Remove(ssx);
            }
        }

        Help.DropDownListDataBind(ScanTypes, ddlScanType, "", "FLAG_NAME", "FLAG_ID", "");//状态
    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData(string ID)
    {
        //BASE_CRANEDetialScanEntity entity = new BASE_CRANEDetialScanEntity();
        IGenericRepository<BASE_CRANECONFIG> con_c = new GenericRepository<BASE_CRANECONFIG>(context);
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con_d = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);

        IGenericRepository<BASE_CRANECONFIG_DETIAL_SCAN> con = new GenericRepository<BASE_CRANECONFIG_DETIAL_SCAN>(context);
        var caseList = from p in con.Get()
                       where p.id == ID
                       select p;
        BASE_CRANECONFIG_DETIAL_SCAN entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG_DETIAL_SCAN>();

        entity.id = this.KeyID;
        //entity.SelectByPKeys();
        //this.txtSiteID.Text = entity.siteid;
        this.txtIDS.Text = entity.ids;
        if (!string.IsNullOrEmpty(entity.ids))
        {
            //线别类型
            ddlScanType.SelectedValue = entity.ScanType;
            //线别
            ddlScanType_SelectedIndexChanged(null, null);
            //选择线别
            var caseList_d = from p in con_d.Get()
                             where p.ID == entity.ids
                             select p;
            BASE_CRANECONFIG_DETIAL entity_d = caseList_d.ToList().FirstOrDefault<BASE_CRANECONFIG_DETIAL>();
            var caseList_c = from p in con_c.Get()
                             where p.ID == entity_d.IDS
                             select p;
            BASE_CRANECONFIG entity_c = caseList_c.ToList().FirstOrDefault<BASE_CRANECONFIG>();
            ddlLineID.SelectedValue = entity_c.ID;
            //加载站点
            ddlLineID_SelectedIndexChanged(null, null);
            ddlSite.SelectedValue = entity.ids;

        }
        this.txtScanID.Text = entity.scanid;
        this.txtScanName.Text = entity.scanname;
        this.txtScanIP.Text = entity.scanip;
        this.txtcreateuser.Text = entity.createuser;
        this.txtcreatetime.Text = entity.createtime.HasValue ? entity.createtime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";

        this.txtupdatetime.Text = entity.updatetime.HasValue ? entity.updatetime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
        this.txtupdateuser.Text = entity.updateuser;
        this.txtDISCONTINUETIME.Text = entity.DISCONTINUETIME != null ? Convert.ToDateTime(entity.DISCONTINUETIME).ToString("yyyy-MM-dd") : "";
        this.txtDISCONTINUEUSER.Text = entity.DISCONTINUEUSER;
        try
        {
            this.dplCSTATUS.SelectedIndex = int.Parse(entity.flag);
        }
        catch
        { }

        this.txtID.Text = entity.id;
        this.txtServerScanIP.Text = entity.serverscanip;
        this.txtPortNO.Text = entity.scanportno;


    }
    /// <summary>
    /// 单<%= Resources.Lang.Base_Data%>显示页面的数据的主键编号
    /// </summary>
    public string KeyID
    {
        get
        {

            try
            {
                if (ViewState["ID"] == null)
                {
                    ViewState["ID"] = this.Page.Request.QueryString["ID"];
                }
            }
            catch (Exception innerException)
            {
                throw new Exception(Resources.Lang.FrmBase_AGV_D_Msg01 + "Querystring:QID", innerException);//未传递
            }
            return ViewState["ID"].ToString()
                ;
        }
        set
        {
            ViewState["ID"] = value;
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //if (string.IsNullOrEmpty(ddlSite.SelectedValue))
        //{
        //    this.Alert("请选择扫描器所属站点！");
        //    this.SetFocus(ddlSite);
        //    return false;
        //}

        if (this.txtScanID.Text.Trim().Length > 0)
        {
            if (this.txtScanID.Text.GetLengthByByte() > 500)
            {
                this.Alert(Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Msg01); //扫描器编号项超过指定的长度500！
                this.SetFocus(txtScanID);
                return false;
            }
        }
        //@"^(?![A-Z]+$)(?![a-z]+$)(?!\d+$)(?![\W_]+$)\S+$"
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtScanID.Text.Trim(), @"^[A-Za-z0-9-_]+$"))//
        {
            this.Alert(Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Msg02);//扫描器编号项只允许输入英文字母和数字,特殊符号-_！
            this.SetFocus(txtScanID);
            return false;
        }
        //
        if (this.txtScanName.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Msg03); //扫描器名称项不允许空！
            this.SetFocus(txtScanName);
            return false;
        }
        if (this.txtScanName.Text.Trim().Length > 0)
        {
            if (this.txtScanName.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Msg04); //扫描器名称项超过指定的长度20！
                this.SetFocus(txtScanName);
                return false;
            }
        }
        //服务器端IP
        if (this.txtServerScanIP.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Msg05);//服务器端IP项不允许空！
            this.SetFocus(txtServerScanIP);
            return false;
        }
        if (this.txtServerScanIP.Text.Trim().Length > 0)
        {
            if (this.txtServerScanIP.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Msg06); //服务器端IP项超过指定的长度20！
                this.SetFocus(txtServerScanIP);
                return false;
            }
        }

        if (this.txtScanIP.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Msg07);//扫描器IP项不允许空！
            this.SetFocus(txtScanIP);
            return false;
        }
        if (this.txtScanIP.Text.Trim().Length > 0)
        {
            if (this.txtScanIP.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Msg08); //扫描器IP项超过指定的长度20！
                this.SetFocus(txtScanIP);
                return false;
            }
        }

        //
        if (this.dplCSTATUS.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmMixed_D_Msg13);//状态项不允许空！
            this.SetFocus(dplCSTATUS);
            return false;
        }
        //
        if (this.dplCSTATUS.Text.Trim().Length > 0)
        {
            if (this.dplCSTATUS.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg15);//状态项超过指定的长度20！
                this.SetFocus(dplCSTATUS);
                return false;
            }
        }
        if (this.txtPortNO.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Msg09); //端口号项不允许空！
            this.SetFocus(txtPortNO);
            return false;
        }
        if (this.txtPortNO.Text.Trim().Length > 0)
        {
            int portno = 0;
            bool isInt = int.TryParse(txtPortNO.Text, out portno);
            if (!isInt || portno == 0)
            {
                this.Alert(Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Msg10); //端口号项不是有效的端口号！
                this.SetFocus(txtPortNO);
                return false;
            }
        }

        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_CRANECONFIG_DETIAL_SCAN SendData()
    {
        //BASE_CRANEDetialScanEntity entity = new BASE_CRANEDetialScanEntity();

        BASE_CRANECONFIG_DETIAL_SCAN entity = new BASE_CRANECONFIG_DETIAL_SCAN();

        //
        //this.txtSiteID.Text = this.txtSiteID.Text.Trim();
        if (ddlSite.Items.Count > 0)
        {
            entity.siteid = ddlSite.Items[ddlSite.SelectedIndex].Text;
            entity.ids = ddlSite.SelectedValue;
        }
        else
        {
            entity.siteid = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCLIENTID = null;
        }
        //
        this.txtScanID.Text = this.txtScanID.Text.Trim();
        if (this.txtScanID.Text.Length > 0)
        {
            entity.scanid = txtScanID.Text;
        }
        else
        {
            entity.scanid = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCLIENTNAME = null;
        }
        //
        this.txtScanName.Text = this.txtScanName.Text.Trim();
        if (this.txtScanName.Text.Length > 0)
        {
            entity.scanname = txtScanName.Text;
        }
        else
        {
            entity.scanname = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CALIAS = null;
        }
        //
        this.txtScanIP.Text = this.txtScanIP.Text.Trim();
        if (this.txtScanIP.Text.Length > 0)
        {
            entity.scanip = txtScanIP.Text;
        }
        else
        {
            entity.scanip = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCONTACTPERSON = null;
        }
        //


        //
        this.txtcreateuser.Text = this.txtcreateuser.Text.Trim();
        if (this.txtcreateuser.Text.Length > 0)
        {
            entity.createuser = txtcreateuser.Text;
        }
        else
        {
            entity.createuser = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CADDRESS = null;
        }
        //
        this.txtupdateuser.Text = this.txtupdateuser.Text.Trim();
        if (this.txtupdateuser.Text.Length > 0)
        {
            entity.updateuser = txtupdateuser.Text;
        }
        else
        {
            entity.updateuser = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CTYPE = null;
        }


        this.dplCSTATUS.Text = this.dplCSTATUS.Text.Trim();
        if (this.dplCSTATUS.Text.Length > 0)
        {
            entity.flag = dplCSTATUS.SelectedValue;
        }
        else
        {
            entity.flag = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CSTATUS = null;
        }
        entity.ScanType = ddlScanType.SelectedValue;

        if (this.Operation() == SYSOperation.New)
        {
            entity.createtime = DateTime.Now;
            entity.createuser = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.updatetime = DateTime.Now;
            entity.updateuser = WmsWebUserInfo.GetCurrentUser().UserNo;
        }
        if (this.Operation() == SYSOperation.Modify)
        {
            entity.createtime = txtcreatetime.Text.Trim().ToDateTime();
            entity.createuser = txtcreateuser.Text.Trim();
            entity.updateuser = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.updatetime = DateTime.Now;
            if (!string.IsNullOrEmpty(this.txtDISCONTINUETIME.Text.Trim()))
            {
                entity.DISCONTINUETIME = Convert.ToDateTime(txtDISCONTINUETIME.Text.Trim());
                entity.DISCONTINUEUSER = txtDISCONTINUEUSER.Text.Trim();
            }
        }
        this.txtServerScanIP.Text = this.txtServerScanIP.Text.Trim();
        if (this.txtServerScanIP.Text.Length > 0)
        {
            entity.serverscanip = txtServerScanIP.Text;
        }
        else
        {
            entity.serverscanip = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCONTACTPERSON = null;
        }
        this.txtPortNO.Text = this.txtPortNO.Text.Trim();
        if (this.txtPortNO.Text.Length > 0)
        {
            entity.scanportno = txtPortNO.Text;
        }
        else
        {
            entity.scanportno = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCONTACTPERSON = null;
        }
        //添加服务器端IP，以及端口号
        //  entity.serverscanip 
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
        IGenericRepository<BASE_CRANECONFIG> con1 = new GenericRepository<BASE_CRANECONFIG>(context);
        IGenericRepository<BASE_CRANECONFIG_DETIAL_SCAN> con = new GenericRepository<BASE_CRANECONFIG_DETIAL_SCAN>(context);
        if (this.CheckData())
        {
            BASE_CRANECONFIG_DETIAL_SCAN entity = (BASE_CRANECONFIG_DETIAL_SCAN)this.SendData();

            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";

            try
            {

                //检查IP是否有重复
                //BASE_FrmBASE_CraneDetialQuery listQuery = new BASE_FrmBASE_CraneDetialQuery();

                //检查IP是否有重复
                //BASE_FrmBASE_CraneQuery cq = new BASE_FrmBASE_CraneQuery();
                //DataTable cqIPRowCount = cq.GetIPList(entity.SCANIP);

                //BASE_FrmBASE_CraneDetialScanQuery cqs = new BASE_FrmBASE_CraneDetialScanQuery();
                //DataTable cqsIPRowCount = cqs.GetIPList(entity.SCANIP);




                if (this.Operation() == SYSOperation.Modify)
                {
                    //if (cqIPRowCount.Rows.Count > 0 || cqsIPRowCount.Rows.Count > 1)
                    //{
                    //    this.Alert("您输入的IP在系统中已存在！");
                    //}
                    //else
                    //{
                    strKeyID = txtID.Text.Trim();
                    entity.id = strKeyID;
                    //entity.ids = txtIDS.Text.Trim();
                    entity.updatetime = DateTime.Now;
                    entity.updateuser = WmsWebUserInfo.GetCurrentUser().UserNo;
                    //BASE_CRANEDetialScanRule.Update(entity);
                    con.Update(entity);
                    var cqIPRowCount = con.Get().Where(p => p.scanip == entity.scanip);
                    var cqsIPRowCount = con1.Get().Where(p => p.CRANEIP == entity.scanip);
                    var idCount = con.Get().Where(p => p.scanid == entity.scanid);
                    if (cqIPRowCount.ToList().Count > 1 || cqsIPRowCount.ToList().Count > 0)
                    {
                        this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_Msg14); //您输入的IP在系统中已存在！
                    }
                    if (idCount.ToList().Count > 1)
                    {
                        this.Alert(Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Msg11); //您输入的扫描器ID在系统中已存在！
                    }
                    else
                    {
                        con.Save();
                        this.AlertAndBack("FrmBase_CraneConfigEdit_D_C.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                    }

                    //}

                }

                else if (this.Operation() == SYSOperation.New)
                {
                    var cqIPRowCount = con.Get().Where(p => p.scanip == entity.scanip);
                    var cqsIPRowCount = con1.Get().Where(p => p.CRANEIP == entity.scanip);
                    var idCount = con.Get().Where(p => p.scanid == entity.scanid);
                    if (cqIPRowCount.ToList().Count > 0 || cqsIPRowCount.ToList().Count > 0)
                    {
                        this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_Msg14); //您输入的IP在系统中已存在！
                    }
                    if (idCount.ToList().Count > 0)
                    {
                        this.Alert(Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Msg11); //您输入的扫描器ID在系统中已存在！
                    }
                    else
                    {

                        strKeyID = Guid.NewGuid().ToString();
                        entity.id = strKeyID;
                        //entity.ids = txtIDS.Text.Trim();
                        entity.createtime = DateTime.Now;
                        entity.createuser = WmsWebUserInfo.GetCurrentUser().UserNo;
                        entity.updatetime = DateTime.Now;
                        entity.updateuser = WmsWebUserInfo.GetCurrentUser().UserNo;
                        con.Insert(entity);
                        con.Save();

                        this.AlertAndBack("FrmBase_CraneConfigEdit_D_C.aspx?" + BuildQueryString(SYSOperation.New, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
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



    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //        BASE_CRANEDetialScanEntity entity = new BASE_CRANEDetialScanEntity();
        //        try
        //        {
        //            entity.ID = this.KeyID.ToString();
        //            BASE_CRANEDetialScanRule.Delete(entity);
        //        }
        //        catch (Exception E)
        //        {
        //            this.Alert("删除失败！" + E.Message);
        //#if Debug 
        //            this.Response.Write(entity.DBAccess().GetLastSQL());                
        //#endif
        //        }



    }


    protected void ddlScanType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //绑定线别
        BindLine(ddlScanType.SelectedValue);
        this.ddlLineID_SelectedIndexChanged(null, null);
    }

    protected void ddlLineID_SelectedIndexChanged(object sender, EventArgs e)
    {
        //绑定站点
        BindSite(ddlLineID.SelectedValue);
    }

    private void BindLine(string scanType)
    {
        //立库
        IGenericRepository<BASE_CRANECONFIG> bcConn = new GenericRepository<BASE_CRANECONFIG>(context);
        var LineList = from p in bcConn.Get()
                       where p.PLCType == scanType
                       select p;
        if (LineList != null)
        {
            ddlLineID.DataSource = LineList.AsEnumerable<BASE_CRANECONFIG>().ToList();
            ddlLineID.DataValueField = "ID";
            ddlLineID.DataTextField = "CRANEID";
            ddlLineID.DataBind();
        }
    }

    private void BindSite(string lineId)
    {
        IGenericRepository<BASE_CRANECONFIG_DETIAL> bcDetailsConn = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        var LineList = from p in bcDetailsConn.Get()
                       where p.IDS == lineId
                       select p;
        if (LineList != null)
        {
            ddlSite.DataSource = LineList.AsEnumerable<BASE_CRANECONFIG_DETIAL>().ToList();
            ddlSite.DataValueField = "ID";
            ddlSite.DataTextField = "SITEID";
            ddlSite.DataBind();
        }

        else
        {
            ddlSite.DataSource = null;
            ddlSite.DataBind();
        }
    }
}