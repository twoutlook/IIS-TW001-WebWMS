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

using System.Data.OleDb;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business.Others;

/// <summary>
/// 描述: 入库管理-->FrmINASNEdit 页面后台类文件
/// </summary>
public partial class RD_FrmInPoEdit : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowVENDORDiv1.SetCompName = this.txtCVENDERName.ClientID;
        ShowVENDORDiv1.SetORGCode = this.txtCVENDERCODE.ClientID;
        if (this.IsPostBack == false)
        {
            this.InitPage();

            if (this.operation == SYSOperation.Modify)
            {
                ShowData();
                //编辑不可修改类型
                btnSave.Enabled = false;
            }
            else if (this.operation == SYSOperation.Preserved1)
            {
                ShowData();
                this.operation = SYSOperation.Modify;
                //采购单明细信息
                this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmInPo_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID + "','" +Resources.Lang.FrmInPoEdit_MSG1+ "','INPO_D',800,600);");
            }
            else
            {
                this.txtCREATEOWNER.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtCREATETIME.Text = Comm_Function.GetDBDateTime("yyyy-MM-dd HH:mm:ss");
                txtPODate.Text = Comm_Function.GetDBDateTime("yyyy-MM-dd HH:mm:ss");
                this.txtID.Text = Guid.NewGuid().ToString();
                txtCSTATUS.SelectedValue = "0";    
            }
        }

        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
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

    public string Status
    {
        get
        {
            if (ViewState["Status"] != null)
            {
                return ViewState["Status"].ToString();
            }
            return "";
        }
        set { ViewState["Status"] = value; }
    }

    #endregion

    #region 页面初始化

    /// <summary>
    /// 初始化页面。主要做一下动作
    ///1、DropDownList,ListBox数据装载,
    ///2、新增按钮、删除的按钮的Java脚本注册等
    ///一般在PageLoad 事件调用，
    ///并且IsPostBack = false时
    /// </summary>
    public void InitPage()
    {
        this.operation = this.Operation();//获取当前页面操作类型
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INPO');return false;";
        this.grdINPO_D.DataKeyNames = new string[] { "IDS" };//
        this.txtCVENDERCODE.Attributes["onclick"] = "Show('" + ShowVENDORDiv1.GetDivName + "');";
        this.txtCVENDERCODE.Attributes["onclick"] = "Show('" + ShowVENDORDiv1.GetDivName + "');";

        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "return PopupFloatWin('" + BuildRequestPageURL("FrmINPO_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID + "','"+Resources.Lang.FrmInPoEdit_MSG1+"','INPO_D',800,600);";
        //PO类型
        Help.DropDownListDataBind(GetParametersByFlagType("IN_PO_TYPE"), dpdPOType, "", "FLAG_NAME", "FLAG_ID", "");
        //状态
        Help.DropDownListDataBind(GetParametersByFlagType("IN_PO"), txtCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(GetParametersByFlagType("SOURCE"), dpdSOURCE, "", "FLAG_NAME", "FLAG_ID", "");//单据来源
        //删除确认提示
        if (this.operation == SYSOperation.Modify || this.operation == SYSOperation.Preserved1)
        {
            //要删除
            this.btnDelete.Attributes["onclick"] = "var userNo = '" + this.KeyID + "'; return window.confirm('" + Resources.Lang.CommonB_NeedRemove+ "' + userNo + '?');";
        }
        else
        {
            this.btnDelete.Visible = false;
        }
        //设置保存按钮的文字及其状态
        if (this.operation == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.operation == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.CommonB_Approve;// + "审批";
        }
    }

    #endregion

    #region 事件
    /// <summary>
    /// 打印功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string printid = this.txtID.Text.Trim();
            //打印采购单
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmInPoEdit_Print.aspx", SYSOperation.New, printid) + "','"+Resources.Lang.FrmInPoEdit_MSG2+"','BAR_REPACK',840,600);");
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.CurrendIndex = 1;//索引同步
        DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        DataBind();
    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.btnSave.Enabled = false;
        SaveToDB(sender);
        this.btnSave.Enabled = true;
    }

    /// <summary>
    /// 删除按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        using (var modContext = this.context)
        {
            using (var dbContextTransaction = modContext.Database.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < this.grdINPO_D.Rows.Count; i++)
                    {
                        if (this.grdINPO_D.Rows[i].Cells[0].Controls[1] is CheckBox)
                        {
                            CheckBox chkSelect = (CheckBox)this.grdINPO_D.Rows[i].Cells[0].Controls[1];
                            if (chkSelect.Checked)//
                            {
                                string key = this.grdINPO_D.DataKeys[i].Values[0].ToString();
                                INPO_D entity = modContext.INPO_D.Where(x => x.ids == key).FirstOrDefault();
                                modContext.INPO_D.Attach(entity);
                                modContext.INPO_D.Remove(entity);
                                modContext.SaveChanges();
                            }
                        }
                    }
                    if (msg.Length == 0)
                    {
                        //删除成功
                        msg = Resources.Lang.CommonB_RemoveSuccess + "！";
                    }
                    else
                    {
                        //删除失败
                        msg = Resources.Lang.CommonB_RemoveFailed + "！\r\n" + msg;
                    }
                    this.Alert(msg);                   
                    dbContextTransaction.Commit();
                    this.btnSearch_Click(sender, e);
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    //删除失败
                    this.Alert(Resources.Lang.CommonB_RemoveFailed + "！" + ex.Message.ToJsString());
                }
            }
        }
    }
    /// <summary>
    /// 行数据绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdINPO_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
            if (Status.Length > 0 && Status != "0")
            {
                linkModify.Enabled = false;
            }
            else
            {
                linkModify.NavigateUrl = "#";
                //+ "采购单明细信息"
                this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmINPO_DEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmInPoEdit_MSG1 , "INPO_D", 800, 600);
            }

            HyperLink hySN = (HyperLink)e.Row.FindControl("hySN");
            if (txtCSTATUS.SelectedValue != "3")
            {
                //编辑SN条码
                this.OpenFloatWin(hySN, BuildRequestPageURL("../../Apps/BAR/FrmBar_SNManagementEdit.aspx", SYSOperation.New, "&ids=" + strKeyID + "&from=INAPO"), "", Resources.Lang.FrmInPoEdit_MSG3, 900, 700);
            }
           // OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBar_SNManagementEdit.aspx", SYSOperation.Modify, strKeyID), "编辑SN条码", "SnCode");

        }
    }
    #endregion

    #region 方法
    /// <summary>
    /// 查询数据并显示
    /// </summary>
    public void ShowData()
    {
        INPO entity = context.INPO.Where(x => x.id == this.KeyID).FirstOrDefault();
        this.txtID.Text = entity.id.ToString();
        txtPONO.Text = entity.pono;
        dpdPOType.SelectedValue = entity.potype;
        txtPODate.Text = entity.podate.HasValue ? entity.podate.Value.ToString("yyyy-MM-dd") : "";
        txtCVENDERCODE.Text = entity.vendorid;
        txtCVENDERName.Text = entity.vendorname;
        txtCSTATUS.SelectedValue = entity.status.ToString();       
        txtCURRENCY.Text = entity.currency;
        txtPAYMENTTERM.Text = entity.paymentterm;
        txtSHIPFROM.Text = entity.shipfrom;
        txtSHIPTO.Text = entity.shipto;
        dpdSOURCE.SelectedValue = entity.source.ToString();
        txtCREATEOWNER.Text =OPERATOR.GetUserNameByAccountID(entity.createowner);
        txtCREATETIME.Text = entity.createtime.HasValue ? entity.createtime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
        txtLASTUPDATEOWNER.Text = OPERATOR.GetUserNameByAccountID(entity.lastupdateowner);
        txtLASTUPDATETIME.Text = entity.lastupdatetime.HasValue ? entity.lastupdatetime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";

        #region 设置不可更改
        if (Convert.ToInt32(entity.status.ToString()) == 2 || Convert.ToInt32(entity.status.ToString()) == 1)
        {
            this.btnNew.Enabled = false;
            this.btnDelete.Enabled = false;
        }
        txtPONO.Enabled = false;
        dpdPOType.Enabled = false;
        txtPODate.Enabled = false;
        txtCVENDERCODE.Enabled = false;
        txtCVENDERName.Enabled = false;
        #endregion

        TabMain0.Visible = true;
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    /// <summary>
    /// 绑定GRIDVIEW数据
    /// </summary>
    public void DataBind()
    {
        using (var modContext = context)
        {
            var queryList = from p in modContext.INPO_D
                            select p;
            if (queryList != null)
            {
                if (!string.IsNullOrEmpty(this.txtID.Text))
                {
                    queryList = queryList.Where(x => x.id == this.txtID.Text);
                }
                if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
                {
                    queryList = queryList.Where(x => x.cinvcode == txtCinvcode.Text.Trim());
                }
            }

            queryList = queryList.OrderBy(x => x.createtime);

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;

            List<INPO_D> data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            var source = from x in data
                         join cv in modContext.BASE_PART.DefaultIfEmpty() on x.cinvcode equals cv.cpartnumber
                         select new
                         {
                             x.ids,
                             x.id,
                             x.poline,
                             x.cinvcode,
                             cv.calias,
                             x.cinvname,
                             x.iquantity,
                             x.unit,
                             x.price,
                             total = x.price.HasValue ? x.price * Convert.ToDecimal(x.iquantity) : 0
                         };

            this.grdINPO_D.DataSource = source.ToList();
            this.grdINPO_D.DataBind();
        }
    }
    /// <summary>
    /// 获取当前列的主键
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <returns></returns>
    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        strKeyId = this.grdINPO_D.DataKeys[rowIndex].Values[0].ToString();
        return strKeyId;
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (this.txtPONO.Text.Trim().Length == 0)
        {
            //PO项不允许空
            this.Alert(Resources.Lang.FrmInPoEdit_MSG4 + "！");
            return false;
        }
        if (this.txtPONO.Text.Trim().Length != 9)
        {
            //PO项不是9位
            this.Alert(Resources.Lang.FrmInPoEdit_MSG5 + "！");
            return false;
        }
        if (this.txtPONO.Text.Substring(3, 6).IsDecimal() == false)
        {
            this.Alert(Resources.Lang.FrmInPoEdit_MSG6 + "！");
            return false;
        }
        if (IsPONOExist(this.txtPONO.Text.Trim()))
        {
            this.Alert(Resources.Lang.FrmInPoEdit_MSG7 + "！");
            return false;
        }
        if (this.txtCVENDERCODE.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmInPoEdit_MSG8 + "！");
            return false;
        }
        if (this.txtCVENDERCODE.Text.Trim().Length > 0)
        {
            if (this.txtCVENDERCODE.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmInPoEdit_MSG9 + "！");
                return false;
            }
        }
        if (this.txtCVENDERName.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmInPoEdit_MSG10 + "！");
            return false;
        }
        if (this.txtCVENDERName.Text.Trim().Length > 0)
        {
            if (this.txtCVENDERName.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmInPoEdit_MSG11 + "！");
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public INPO SendData(INPO entity)
    {
        entity.pono = this.txtPONO.Text.Trim();
        entity.potype = dpdPOType.SelectedValue;
        entity.podate = Convert.ToDateTime(txtPODate.Text);
        entity.vendorid = this.txtCVENDERCODE.Text.Trim();
        entity.vendorname = this.txtCVENDERName.Text.Trim();
        if (this.operation == SYSOperation.New)
        {
            entity.status = 0;
        }
        else
        {
            entity.status = Convert.ToInt32(txtCSTATUS.SelectedValue);
        }
        entity.currency = txtCURRENCY.Text;
        entity.paymentterm = txtPAYMENTTERM.Text;
        entity.shipfrom = txtSHIPFROM.Text;
        entity.shipto = txtSHIPTO.Text;
        entity.createowner = txtCREATEOWNER.Text;
        entity.createtime = Convert.ToDateTime(txtCREATETIME.Text);
        entity.source = Convert.ToInt32(dpdSOURCE.SelectedValue);
        entity.memo = this.txtMEMO.Text.Trim();
        return entity;
    }

    /// <summary>
    /// 保存数据到数据库
    /// </summary>
    /// <param name="sender"></param>
    private void SaveToDB(object sender)
    {
        bool isError = false;
        if (this.CheckData())
        {
            string msg = string.Empty;
            string strKeyID = "";
            try
            {
                if (this.operation == SYSOperation.Modify)
                {
                    using (var modContext = this.context)
                    {
                        strKeyID = txtID.Text.Trim();
                        INPO entity = modContext.INPO.Where(x => x.id == strKeyID).FirstOrDefault();
                        if (entity != null)
                        {
                            entity = this.SendData(entity);
                            modContext.INPO.Attach(entity);
                            modContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                            int result = modContext.SaveChanges();
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
                        else
                        {
                            //保存失败
                            msg = Resources.Lang.CommonB_SaveFailed + "!";
                        }
                    }
                }
                else if (this.operation == SYSOperation.New)
                {
                    using (var modContext = this.context)
                    {
                        INPO entity = new INPO();
                        strKeyID = Guid.NewGuid().ToString();
                        entity.id = strKeyID;
                        entity = this.SendData(entity);
                        modContext.INPO.Add(entity);
                        int result = modContext.SaveChanges();
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
                    if ((sender as Button).ID == "btnNew")
                    {
                        Response.Redirect(BuildRequestPageURL("FrmInPoEdit.aspx?", SYSOperation.Preserved1, strKeyID));
                    }
                    else
                    {
                        this.AlertAndBack("FrmInPoEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), msg);
                    }
                }
                else
                {
                    Alert(msg);
                }
            }
            catch (Exception ex)
            {
                if (!isError)
                {
                    //失败
                    this.Alert(this.GetOperationName() + Resources.Lang.CommonB_Failed + "！" + ex.ToString());
                }

            }
        }
    }

    #endregion
 
  
    /// <summary>
    /// 获取数据库里存在没存在相同PONO
    /// </summary>
    /// <param name="pono">采购单号</param>
    /// <returns></returns>
    private bool IsPONOExist(string pono)
    {
        var obj =  db.INPO.Where(x => x.pono == pono).FirstOrDefault();
        if (obj != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }

    /// <summary>
    /// 获取选择的ID
    /// </summary>
    public void GetSelectedIds()
    {
        if (SelectIds == null)
        {
            SelectIds = new Dictionary<string, string>();
        }

        foreach (GridViewRow item in this.grdINPO_D.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string ids = this.grdINPO_D.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                //控件选中且集合中不存在添加
                if (cbo.Checked && !SelectIds.ContainsKey(ids))
                {
                    SelectIds.Add(ids, ids);
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(ids))
                {
                    SelectIds.Remove(ids);
                }
            }
        }
    }

    
}

