using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_FrmBase_CAR : WMSBasePage
{
    #region 常量
    public static string CraneIDstr = "";
    public static string CraneIDS = "";
    public static string CraneName = "";
    DBContext context = new DBContext();
    #endregion

    #region load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            Help.DropDownListDataBind(GetParametersByFlagType("BASE_CLIENT"), dplCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");//状态
            if (this.Operation() != SYSOperation.New)
            {
                ShowData(this.KeyID);

                this.txtCraneID.Enabled = false;
                this.GridBind();
                //将CraneIDstr保存到session中
                if (CraneIDstr != "")
                {
                    Session["CraneIDstr"] = CraneIDstr;
                    Session["CraneName"] = CraneName;
                }
                if (CraneIDS != "")
                {
                    Session["CraneIDS"] = CraneIDS;

                }
                txtCraneID.Enabled = false;

            }
            else if (this.Operation() == SYSOperation.New)
            {
                hdnID.Value = Guid.NewGuid().ToString();
                txtCreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtModifyTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtCreateUser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtModifyUser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                btnNew.Enabled = false;
                btnDiscontinue.Enabled = false;
            }
            else
            {
                Session["CraneIDstr"] = "";
                Session["CraneIDS"] = "";
                Session["CraneName"] = "";
            }
            this.InitPage();
            GridBind();
        }
    }
    #endregion

    #region Funtion
    public void ShowData(string ID)
    {
        IGenericRepository<BASE_CRANECONFIG> con = new GenericRepository<BASE_CRANECONFIG>(context);
        var caseList = from p in con.Get()
                       where p.ID == ID
                       select p;
        BASE_CRANECONFIG entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG>();
        entity.ID = this.KeyID;
        //entity.SelectByPKeys();
        this.txtCraneID.Text = entity.CRANEID;
        CraneIDstr = entity.CRANEID;
        CraneIDS = entity.ID;
        CraneName = entity.CRANENAME;
        this.txtCraneNAME.Text = entity.CRANENAME;
        this.txtCRANEIP.Text = entity.CRANEIP;
        this.txtEQUIPMENTNAME.Text = entity.EQUIPMENTNAME;
        this.txtTTYPE.Text = entity.TTYPE;
        try
        {
            this.dplCSTATUS.SelectedIndex = int.Parse(entity.FLAG);
        }
        catch
        { }
        txtCreateUser.Text = entity.CREATEUSER;
        txtCreateTime.Text = Convert.ToDateTime(entity.CreateTime).ToString("yyyy-MM-dd");
        txtModifyUser.Text = entity.MODIFYUSER;
        txtModifyTime.Text = Convert.ToDateTime(entity.ModifyTime).ToString("yyyy-MM-dd");
        txtGROUPID.Text = entity.GROUPID;
        txtDISCONTINUETIME.Text = entity.DISCONTINUETIME != null ? Convert.ToDateTime(entity.DISCONTINUETIME).ToString("yyyy-MM-dd") : "";
        txtDISCONTINUEUSER.Text = entity.DISCONTINUEUSER;
        txtRemark.Text = entity.REMARK;
        this.hdnID.Value = entity.ID;
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

    /// 验证用户输入的是否是整数
    ///
    ///
    /// 返回bool类型
    public static bool IsInt(string intstring)
    {
        return Regex.IsMatch(intstring, @"[1-9]\d*$");
    }
    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //
        if (this.txtCraneID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_RGV_Msg01); //RGV编号项不允许空！
            this.SetFocus(txtCraneID);
            return false;
        }
        //
        if (this.txtCraneID.Text.Trim().Length > 0)
        {
            if (this.txtCraneID.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmBase_RGV_Msg02);//RGV编号项超过指定的长度30！
                this.SetFocus(txtCraneID);
                return false;
            }
        }
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtCraneID.Text.Trim(), "^[A-Za-z0-9-_]+$"))
        {
            this.Alert(Resources.Lang.FrmBase_RGV_Msg03); //RGV编号项只允许输入英文字母和数字,特殊符号-_！
            this.SetFocus(txtCraneID);
            return false;
        }
        //
        if (this.txtCraneNAME.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_RGV_Msg04); //RGV名称项不允许空！
            this.SetFocus(txtCraneNAME);
            return false;
        }
        //
        if (this.txtCraneNAME.Text.Trim().Length > 0)
        {
            if (this.txtCraneNAME.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmBase_RGV_Msg05);//RGV名称项超过指定的长度30！
                this.SetFocus(txtCraneNAME);
                return false;
            }
        }
        //
        if (this.txtCRANEIP.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_RGV_Msg06);//IP地址项不允许空！
            this.SetFocus(txtCRANEIP);
            return false;
        }
        if (this.txtCRANEIP.Text.Trim().Length > 0)
        {
            if (this.txtCRANEIP.Text.GetLengthByByte() > 16)
            {
                this.Alert(Resources.Lang.FrmBase_RGV_Msg07);//IP地址超过指定的长度16！
                this.SetFocus(txtCRANEIP);
                return false;
            }
        }
        //
        if (this.txtGROUPID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_Msg08);//组编号项不允许空！
            this.SetFocus(txtGROUPID);
            return false;
        }
        if (this.txtGROUPID.Text.Trim().Length > 0)
        {
            if (this.txtGROUPID.Text.GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_Msg09);//组编号超过指定的长度100！
                this.SetFocus(txtGROUPID);
                return false;
            }
        }
        //楼层只能是英文数字
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtGROUPID.Text.Trim(), "^[0-9]+$"))
        {
            this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_Msg10);//组编号项只允许输入数字！
            this.SetFocus(txtGROUPID);
            return false;
        }
        //
        if (this.txtEQUIPMENTNAME.Text.Trim().Length > 0)
        {
            if (this.txtEQUIPMENTNAME.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmBase_RGV_Msg08);//设备名称超过指定的长度50！
                this.SetFocus(txtEQUIPMENTNAME);
                return false;
            }
        }
        //
        if (this.txtTTYPE.Text.Trim().Length > 0)
        {
            if (this.txtTTYPE.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmBase_RGV_Msg09);//类型超过指定的长度20！
                this.SetFocus(txtTTYPE);
                return false;
            }
        }
        //楼层只能是英文数字
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtCraneID.Text.Trim(), "^[0-9]+$"))
        {
            this.Alert(Resources.Lang.FrmBase_RGV_Msg10); //RGV编号项只允许输入数字！
            this.SetFocus(txtCraneID);
            return false;
        }
        return true;

    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_CRANECONFIG SendData()
    {
        BASE_CRANECONFIG entity = new BASE_CRANECONFIG();
        //
        this.txtCraneID.Text = this.txtCraneID.Text.Trim();
        if (this.txtCraneID.Text.Length > 0)
        {
            entity.CRANEID = txtCraneID.Text;
        }
        else
        {
            entity.CRANEID = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCLIENTID = null;
        }
        //
        this.txtCraneNAME.Text = this.txtCraneNAME.Text.Trim();
        if (this.txtCraneNAME.Text.Length > 0)
        {
            entity.CRANENAME = txtCraneNAME.Text;
        }
        else
        {
            entity.CRANENAME = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CCLIENTNAME = null;
        }
        //
        this.txtCRANEIP.Text = this.txtCRANEIP.Text.Trim();
        if (this.txtCRANEIP.Text.Length > 0)
        {
            entity.CRANEIP = txtCRANEIP.Text;
        }
        else
        {
            entity.CRANEIP = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CALIAS = null;
        }
        //
        this.txtGROUPID.Text = this.txtGROUPID.Text.Trim();
        if (this.txtGROUPID.Text.Length > 0)
        {
            entity.GROUPID = txtGROUPID.Text;
        }
        else
        {
            entity.GROUPID = string.Empty;
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CALIAS = null;
        }
        //
        this.txtEQUIPMENTNAME.Text = this.txtEQUIPMENTNAME.Text.Trim();
        if (this.txtEQUIPMENTNAME.Text.Length > 0)
        {
            entity.EQUIPMENTNAME = txtEQUIPMENTNAME.Text;
        }
        else
        {
            entity.EQUIPMENTNAME = string.Empty;
        }
        this.txtTTYPE.Text = this.txtTTYPE.Text.Trim();
        if (this.txtTTYPE.Text.Length > 0)
        {
            entity.TTYPE = txtTTYPE.Text;
        }
        else
        {
            entity.TTYPE = string.Empty;
        }
        //备注
        if (this.txtRemark.Text.Trim().Length > 0)
        {
            entity.REMARK = txtRemark.Text.Trim();
        }
        else
        {
            entity.REMARK = string.Empty;
        }
        entity.FLAG = dplCSTATUS.SelectedValue;
        entity.PLCType = "CAR";
        if (this.Operation() == SYSOperation.New)
        {
            entity.CREATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.CreateTime = DateTime.Now;
            entity.MODIFYUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.ModifyTime = DateTime.Now;
            entity.SITECOUNT = 0;
        }
        else if (this.Operation() == SYSOperation.Modify)
        {
            entity.CREATEUSER = this.txtCreateUser.Text.Trim();
            entity.CreateTime = Convert.ToDateTime(this.txtCreateTime.Text.Trim());
            entity.MODIFYUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
            entity.ModifyTime = DateTime.Now;
            if (!string.IsNullOrEmpty(txtDISCONTINUETIME.Text.Trim()))
            {
                entity.DISCONTINUETIME = Convert.ToDateTime(txtDISCONTINUETIME.Text.Trim());
                entity.DISCONTINUEUSER = txtDISCONTINUEUSER.Text.Trim();
            }
        }
        return entity;

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
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearchCAR'].click(); CloseMySelf('BASE_CAR');return false;";
        this.grdBASE_CraneDetail.DataKeyNames = new string[] { "ID", "FLAG" };
        //本页面打开新增窗口        
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBase_CAR_D.aspx", SYSOperation.New, hdnID.Value) + "','" + Resources.Lang.FrmBase_CraneConfigEdit_Title02 + "','BASE_CAR_D');return false;";//线别详情
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }

    }

    public void GridBind()
    {
        IGenericRepository<BASE_CRANECONFIG_DETIAL> vcon = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        var caseList = from p in vcon.Get()
                       orderby p.SITEID ascending
                       where 1 == 1
                       select p;

        if (!string.IsNullOrEmpty(CraneIDstr))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CRANEID) && x.CRANEID.Contains(CraneIDstr));
        if (!string.IsNullOrEmpty(hdnID.Value))
            caseList = caseList.Where(x => x.IDS.Contains(hdnID.Value));

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("FLAG", "FLAGName", "BASE_CLIENT"));//状态
        flagList.Add(new Tuple<string, string, string>("DEFULSITE", "ISDEFULSITE", "YesOrNo"));//状态
        flagList.Add(new Tuple<string, string, string>("SITETYPE", "SITETYPEName", "RGVSITETYPE"));//状态

        var srcdata = GetGridDataByAddColumns(data, flagList);

        grdBASE_CraneDetail.DataSource = srcdata;
        grdBASE_CraneDetail.DataBind();

    }

    #endregion


    #region even
    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CRANECONFIG> con = new GenericRepository<BASE_CRANECONFIG>(context);
        IGenericRepository<BASE_CRANECONFIG_DETIAL_SCAN> con1 = new GenericRepository<BASE_CRANECONFIG_DETIAL_SCAN>(context);
        if (this.CheckData())
        {
            BASE_CRANECONFIG entity = (BASE_CRANECONFIG)this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";

            try
            {
                //检查IP是否有重复
                BASE_CRANECONFIG cq = new BASE_CRANECONFIG();

                BASE_CRANECONFIG_DETIAL_SCAN cqs = new BASE_CRANECONFIG_DETIAL_SCAN();

                if (this.Operation() == SYSOperation.Modify)
                {

                    strKeyID = hdnID.Value;
                    entity.ID = strKeyID;

                    con.Update(entity);
                    var cqIPRowCount = con.Get().Where(p => p.CRANEIP == entity.CRANEIP);
                    var cqsIPRowCount = con1.Get().Where(p => p.scanip == entity.CRANEIP);
                    if (cqIPRowCount.ToList().Count > 1 || cqsIPRowCount.ToList().Count > 0)
                    {
                        this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_Msg14); //您输入的IP在系统中已存在！
                    }
                    else
                    {
                        con.Save();
                        this.AlertAndBack("FrmBase_CAR.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                    }
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    var cqIPRowCount = con.Get().Where(p => p.CRANEIP == entity.CRANEIP);
                    var cqsIPRowCount = con1.Get().Where(p => p.scanip == entity.CRANEIP);
                    if (cqIPRowCount.ToList().Count > 0 || cqsIPRowCount.ToList().Count > 0)
                    {
                        this.Alert(Resources.Lang.FrmBase_CraneConfigEdit_Msg14); //您输入的IP在系统中已存在！
                    }
                    else
                    {
                        strKeyID = hdnID.Value;
                        entity.ID = strKeyID;

                        con.Insert(entity);
                        con.Save();

                        this.AlertAndBack("FrmBase_CAR.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
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

    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
        }
        this.GridBind();
        IsFirstPage = true;//恢复默认值
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void grdBASE_CraneDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.grdBASE_CraneDetail.DataKeyNames.Length > 0 ? this.grdBASE_CraneDetail.DataKeys[e.Row.RowIndex].Values[0].ToString() : ""; //this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";

            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBase_CAR_D.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBase_CraneConfigEdit_Msg15, "BASE_CAR_D"); //站点详情

            switch (e.Row.Cells[7].Text)
            {
                case "1": e.Row.Cells[7].Text = Resources.Lang.FrmALLOCATE_DEdit_SN_Msg03;//"是";
                    break;
                case "0": e.Row.Cells[7].Text = Resources.Lang.FrmALLOCATE_DEdit_SN_Msg04;//"否";
                    break;
                default: e.Row.Cells[7].Text = Resources.Lang.Common_Exception;//"异常"
                    break;
            }
        }


    }

    /// <summary>
    /// 台车站点停用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDiscontinue_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_CRANECONFIG_DETIAL> con = new GenericRepository<BASE_CRANECONFIG_DETIAL>(context);
        try
        {
            int number = 0;
            for (int i = 0; i < grdBASE_CraneDetail.Rows.Count; i++)
            {
                if (this.grdBASE_CraneDetail.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CraneDetail.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                        number++;
                }
            }
            if (number == 0)
            {
                throw new Exception(Resources.Lang.FrmBase_CraneConfigEdit_Msg16); //请选择要停用的项！
            }
            for (int i = 0; i < this.grdBASE_CraneDetail.Rows.Count; i++)
            {
                if (this.grdBASE_CraneDetail.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CraneDetail.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string status = this.grdBASE_CraneDetail.DataKeys[i].Values[1].ToString();
                        if (status == "0")
                        {
                            string id = this.grdBASE_CraneDetail.DataKeys[i].Values[0].ToString();
                            var caseList = from p in con.Get()
                                           where p.ID == id
                                           select p;
                            BASE_CRANECONFIG_DETIAL entity = caseList.ToList().FirstOrDefault<BASE_CRANECONFIG_DETIAL>();
                            entity.DISCONTINUEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                            entity.DISCONTINUETIME = DateTime.Now;
                            entity.FLAG = "1";
                            con.Update(entity);
                            con.Save();
                        }
                        else
                        {
                            throw new Exception(Resources.Lang.FrmBase_CraneConfig_Msg10);//"只有状态为[使用中]才能停用."
                        }
                    }
                }
            }
            this.GridBind();
            msg = Resources.Lang.FrmBase_CraneConfig_Msg11; //停用成功！
        }
        catch (Exception Ex)
        {
            msg += Resources.Lang.FrmBase_CraneConfig_Msg12 + "[" + Ex.Message + "]";//停用失败!
        }
        this.Alert(msg);
    }
    #endregion
}