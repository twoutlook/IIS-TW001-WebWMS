using DreamTek.ASRS.Business.Others;
using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_OUT_FrmOutOrderEdit : WMSBasePage
{

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

    protected void Page_Load(object sender, EventArgs e)
    {
        ShowBASE_CLIENTDiv1.SetCompName = this.txtCustomName.ClientID;
        ShowBASE_CLIENTDiv1.SetORGCode = this.txtCustomId.ClientID;

        if (!this.IsPostBack)
        {
            this.InitPage();
            //int aa = Convert.ToInt32("1aa");
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
                this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmOutOrder_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID + "','" + Resources.Lang.FrmOutOrderEdit_OrderDetail + "','OUTORDER_D',800,600);");//订单明细信息
            }
            else
            {
                this.txtCREATEOWNER.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                txtCREATETIME.Text = Comm_Function.GetDBDateTime("yyyy-MM-dd HH:mm:ss");
                txtOrderDate.Text = Comm_Function.GetDBDateTime("yyyy-MM-dd HH:mm:ss");
                this.txtID.Value = Guid.NewGuid().ToString();
                txtStatus.SelectedValue = "0";
                drpSource.SelectedValue = "1";
                drpSource.Enabled = false;
            }
        }
        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
    }

    public void InitPage()
    {
        this.operation = this.Operation();//获取当前页面操作类型
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTORDER');return false;";
        this.grdOutOrder_D.DataKeyNames = new string[] { "IDS" };//
        //this.txtCVENDERCODE.Attributes["onclick"] = "Show('" + ShowVENDORDiv1.GetDivName + "');";
        //this.txtCVENDERCODE.Attributes["onclick"] = "Show('" + ShowVENDORDiv1.GetDivName + "');";
        this.txtCustomName.Attributes["onclick"] = "Show('" + ShowBASE_CLIENTDiv1.GetDivName + "');";
        this.txtCustomId.Attributes["onclick"] = "Show('" + ShowBASE_CLIENTDiv1.GetDivName + "');";
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "return PopupFloatWin('" + BuildRequestPageURL("FrmOutOrder_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID + "','" + Resources.Lang.FrmOutOrderEdit_OrderDetail + "','OUTORDER_D',800,600);";//订单明细信息
        //PO类型
        Help.DropDownListDataBind(GetParametersByFlagType("OUTORDER_TYPE"), drpOrderType, "", "FLAG_NAME", "FLAG_ID", "");
        //状态
        Help.DropDownListDataBind(GetParametersByFlagType("OUTORDER"), txtStatus, "", "FLAG_NAME", "FLAG_ID", "");
        txtStatus.Enabled = false;
        //删除确认提示
        if (this.operation == SYSOperation.Modify || this.operation == SYSOperation.Preserved1)
        {
            this.btnDelete.Attributes["onclick"] = "var userNo = '" + this.KeyID + "'; return window.confirm('" + Resources.Lang.FrmOutOrderEdit_Tips_YaoShanChu + "' + userNo + '?');";//要删除
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
    }

    public void ShowData()
    {
        OUTORDER entity = context.OUTORDER.Where(x => x.id == this.KeyID).FirstOrDefault();
        if (entity != null)
        {
            this.txtID.Value = entity.id.ToString();
            txtOrderNo.Text = entity.OrderNo;
            txtCustomOrderNo.Text = entity.CustomOrderNo;
            drpOrderType.SelectedValue = entity.OrderType.ToString();
            txtOrderDate.Text = entity.OrderDate.HasValue ? entity.OrderDate.Value.ToString("yyyy-MM-dd") : "";
            txtCustomId.Text = entity.CustomId;
            txtCustomName.Text = entity.CustomName;
            txtStatus.SelectedValue = entity.Status.ToString();
            drpSource.SelectedValue = entity.OrderSource.ToString();
            txtCREATEOWNER.Text = entity.CreateOwner;
            txtCREATETIME.Text = entity.CreateTime.HasValue ? entity.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            txtLASTUPDATEOWNER.Text = entity.LastUpdateOwner;
            txtLASTUPDATETIME.Text = entity.LastUpdateTime.HasValue ? entity.LastUpdateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            txtAmount.Text = entity.Amount.ToString("f2");
            txtTaxRate.Text = entity.TaxRate.HasValue ? entity.TaxRate.Value.ToString("f2"):"0.00";
            txtAfterTaxAmount.Text = entity.AfterTaxAmount.HasValue ? entity.AfterTaxAmount.Value.ToString("f2") : "0.00";
            txtDeliveryAddress.Text = entity.DeliveryAddress;
            txtSalesMan.Text = entity.SalesMan;

            #region 设置不可更改
            if (entity.Status !=0)
            {
                this.btnNew.Enabled = false;
                this.btnDelete.Enabled = false;
            }
            txtOrderNo.Enabled = false;
            txtCustomOrderNo.Enabled = false;
            drpOrderType.Enabled = false;
            txtOrderDate.Enabled = false;
            txtCustomId.Enabled = false;
            txtCustomName.Enabled = false;
            txtTaxRate.Enabled = false;
            txtDeliveryAddress.Enabled = false;
            txtSalesMan.Enabled = false;
            drpSource.Enabled = false;

            //单据来源是erp,不允许新增，修改，删除
            if (entity.OrderSource == 0) {
                this.btnNew.Enabled = false;
                this.btnDelete.Enabled = false;
            }
            #endregion

            TabMain0.Visible = true;
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;//默认第一页
            AspNetPager1.CurrentPageIndex = 1;
        }
        IsFirstPage = true;
        DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        DataBind();
    }

    /// <summary>
    /// 绑定GRIDVIEW数据
    /// </summary>
    public void DataBind()
    {
        using (var modContext = this.context)
        {
            var queryList = from p in context.OUTORDER_D
                            select p;
                    

            if (!string.IsNullOrEmpty(this.txtID.Value))
            {
                queryList = queryList.Where(x => x.id == this.txtID.Value);
            }
            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
            {
                queryList = queryList.Where(x => x.CinvCode == txtCinvcode.Text.Trim());
            }


            queryList = queryList.OrderBy(x => x.OrderLine);

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;

            //国际化，dropdownlist [begin]
            List<OUTORDER_D> data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            var source = from p in data
                        // join sp in modContext.SYS_PARAMETER.DefaultIfEmpty() on p.Status.ToString() equals sp.flag_id
                         join cv in modContext.BASE_PART.DefaultIfEmpty() on p.CinvCode equals cv.cpartnumber
                         //where sp.flag_type == "OUTORDER_D"
                         select new
                         {
                             p.id,
                             p.ids,
                             p.OrderLine,
                             p.CinvCode,
                             cv.calias,
                             p.CinvName,
                             p.Iquantity,
                             p.Price,
                             p.Amount,
                             p.FinishQty,
                             p.Status 
                         };

            //this.grdOutOrder_D.DataSource = source.ToList();
            
           // var data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();

            List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
            //flagList.Add(new Tuple<string, string>("OrderType", "OUTORDER_TYPE"));
            flagList.Add(new Tuple<string, string>("Status", "OUTORDER"));

            var srcdata = GetGridSourceDataByList(source.ToList(), flagList);
            this.grdOutOrder_D.DataSource = srcdata;
            this.grdOutOrder_D.DataBind();

            //国际化，dropdownlist [END]
            //刷新金额
            var modOrder = modContext.OUTORDER.Where(x => x.id == this.txtID.Value).FirstOrDefault();
            if (modOrder != null) {
                txtAmount.Text = modOrder.Amount.ToString("f2");
                txtAfterTaxAmount.Text = modOrder.AfterTaxAmount.HasValue ? modOrder.AfterTaxAmount.Value.ToString("f2") : "0.00";
            }

        }
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
                        strKeyID = txtID.Value.Trim();
                        OUTORDER entity = modContext.OUTORDER.Where(x => x.id == strKeyID).FirstOrDefault();
                        if (entity != null)
                        {
                            entity = this.SendData(entity);
                            modContext.OUTORDER.Attach(entity);
                            modContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                            int result = modContext.SaveChanges();
                            if (result > 0)
                            {
                                msg = Resources.Lang.WMS_Common_Msg_SaveSuccess;//保存成功!
                            }
                            else
                            {
                                isError = true;
                                msg = Resources.Lang.WMS_Common_Msg_SaveFailed;//保存失败!
                            }
                        }
                        else
                        {
                            isError = true;
                            msg = Resources.Lang.WMS_Common_Msg_SaveFailed;//保存失败!
                        }
                    }
                }
                else if (this.operation == SYSOperation.New)
                {
                    using (var modContext = this.context)
                    {
                        OUTORDER entity = new OUTORDER();
                        strKeyID = Guid.NewGuid().ToString();
                        entity.id = strKeyID;
                        entity = this.SendData(entity);
                        modContext.OUTORDER.Add(entity);
                        int result = modContext.SaveChanges();
                        if (result > 0)
                        {
                            msg = Resources.Lang.WMS_Common_Msg_SaveSuccess;//保存成功!
                        }
                        else
                        {
                            isError = true;
                            msg = Resources.Lang.WMS_Common_Msg_SaveFailed;//保存失败!
                        }
                    }
                }
                if (!isError)
                {
                    if ((sender as Button).ID == "btnNew")
                    {
                        Response.Redirect(BuildRequestPageURL("FrmOutOrderEdit.aspx?", SYSOperation.Preserved1, strKeyID));
                    }
                    else
                    {
                        this.AlertAndBack("FrmOutOrderEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), msg);
                    }
                }
                else
                {
                    Alert(msg);
                }
            }
            catch (Exception ex)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.WMS_Common_Msg_Failed + ex.ToString());//失败！
            }
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (this.txtOrderNo.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmOutOrderEdit_Tips_NeedOrderNo);//订单编号不允许空！
            return false;
        }

        if (IsOrderNoExist(this.txtOrderNo.Text.Trim()))
        {
            this.Alert(Resources.Lang.FrmOutOrderEdit_Tips_ExistsOrderNo);//订单编号已存在
            return false;
        }

        if (this.txtCustomOrderNo.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmOutOrderEdit_Tips_NeedCustomNo);//客户订单编号不允许空！
            return false;
        }

        if (this.txtCustomId.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmOutOrderEdit_Tips_NeedCustomCode);//客户编码不允许为空
            return false;
        }
        if (this.txtCustomId.Text.Trim().Length > 0)
        {
            if (this.txtCustomId.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmOutOrderEdit_Tips_CustomCodeLength);//客户编码项超过指定的长度50！
                return false;
            }
        }
        if (this.txtCustomName.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmOutOrderEdit_Tips_NeedCustomName);//客户名称不允许为空
            return false;
        }
        if (this.txtCustomName.Text.Trim().Length > 0)
        {
            if (this.txtCustomName.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmOutOrderEdit_Tips_CustomNameLength);//客户名称项超过指定的长度50！
                return false;
            }
        }
        if (this.txtTaxRate.Text.Trim().Length > 0)
        {
            string msg = "";
            //检查税率，不允许负数
            if (!(Comm_Function.Fun_IsDecimal(txtTaxRate.Text.Trim(), 0, 1, 1, out msg)))
            {
                this.Alert(msg);
                this.SetFocus(txtTaxRate);
                return false;
            }
        }
        if (this.txtSalesMan.Text.Trim().Length > 0) {
            if (this.txtSalesMan.Text.GetLengthByByte() > 50)
            {
                this.Alert(Resources.Lang.FrmOutOrderEdit_Tips_SalesManLength);//业务员名称项超过指定的长度50！
                return false;
            }
        }
        
        return true;
    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public OUTORDER SendData(OUTORDER entity)
    {
        entity.OrderNo = this.txtOrderNo.Text.Trim();
        entity.OrderType = Convert.ToInt32(drpOrderType.SelectedValue);
        entity.OrderDate = Convert.ToDateTime(txtOrderDate.Text);
        entity.CustomId = this.txtCustomId.Text.Trim();
        entity.CustomName = this.txtCustomName.Text.Trim();
        entity.CustomOrderNo = this.txtCustomOrderNo.Text.Trim();
        entity.SalesMan = this.txtSalesMan.Text.Trim();
        if (this.operation == SYSOperation.New)
        {
            entity.Status = 0;
        }
        else
        {
            entity.Status = Convert.ToInt32(txtStatus.SelectedValue);
        }
        //entity.currency = txtCURRENCY.Text;
        //entity.paymentterm = txtPAYMENTTERM.Text;
        //entity.shipfrom = txtSHIPFROM.Text;
        //entity.shipto = txtSHIPTO.Text;

        entity.Amount = 0;
        if (this.txtTaxRate.Text.Trim().Length > 0)
        {
            entity.TaxRate = Convert.ToDecimal(this.txtTaxRate.Text.Trim());
        }
        else {
            entity.TaxRate = 0;
        }
        entity.AfterTaxAmount = 0;
        entity.DeliveryAddress = this.txtDeliveryAddress.Text.Trim();
        entity.CreateOwner = txtCREATEOWNER.Text;
        entity.CreateTime = Convert.ToDateTime(txtCREATETIME.Text);
        entity.OrderSource = Convert.ToInt32(drpSource.SelectedValue);
        entity.Memo = this.txtMEMO.Text.Trim();
        return entity;
    }


    /// <summary>
    /// 获取数据库里存在没存在相同orderNo
    /// </summary>
    /// <param name="pono">订单单号</param>
    /// <returns></returns>
    private bool IsOrderNoExist(string orderNo)
    {
        var obj = db.OUTORDER.Where(x => x.OrderNo == orderNo).FirstOrDefault();
        if (obj != null)
        {
            return true;
        }
        else
        {
            return false;
        }
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
                    for (int i = 0; i < this.grdOutOrder_D.Rows.Count; i++)
                    {
                        if (this.grdOutOrder_D.Rows[i].Cells[0].Controls[1] is CheckBox)
                        {
                            CheckBox chkSelect = (CheckBox)this.grdOutOrder_D.Rows[i].Cells[0].Controls[1];
                            if (chkSelect.Checked)//
                            {
                                string key = this.grdOutOrder_D.DataKeys[i].Values[0].ToString();
                                OUTORDER_D entity = modContext.OUTORDER_D.Where(x => x.ids == key).FirstOrDefault();
                                string id = entity.id;
                                modContext.OUTORDER_D.Attach(entity);
                                modContext.OUTORDER_D.Remove(entity);
                                modContext.SaveChanges();

                                OUTORDER modOrder = modContext.OUTORDER.Where(x => x.id == id).FirstOrDefault();
                                modOrder.Amount = 0;
                                modOrder.AfterTaxAmount = 0;
                                List<OUTORDER_D> modOrder_dList = modContext.OUTORDER_D.Where(x=>x.id == id).ToList();
                                foreach (var item in modOrder_dList) {
                                    modOrder.Amount += item.Amount;
                                }
                                modOrder.AfterTaxAmount = modOrder.Amount + modOrder.Amount * (modOrder.TaxRate == 0 ? 0 : (modOrder.TaxRate / 100));
                                modContext.OUTORDER.Attach(modOrder);
                                modContext.Entry(modOrder).State = System.Data.Entity.EntityState.Modified;
                                modContext.SaveChanges();
                            }
                        }
                    }
                    if (msg.Length == 0)
                    {
                        msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;//删除成功！
                    }
                    else
                    {
                        msg = Resources.Lang.WMS_Common_Msg_DeleteFailed + "\r\n" + msg;//删除失败！
                    }
                    this.Alert(msg);
                    dbContextTransaction.Commit();
                    this.btnSearch_Click(sender, e);
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + ex.Message.ToJsString());//删除失败！
                }
            }
        }
    }

    /// <summary>
    /// 行数据绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdOutOrder_D_RowDataBound(object sender, GridViewRowEventArgs e)
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
                this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmOutOrder_DEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmOutOrderEdit_OrderDetail, "OUTORDER_D", 800, 600);//订单明细信息
            }

            HyperLink hySN = (HyperLink)e.Row.FindControl("hySN");
            if (txtStatus.SelectedValue != "3")
            {
                this.OpenFloatWin(hySN, BuildRequestPageURL("../../Apps/BAR/FrmBar_SNManagementEdit.aspx", SYSOperation.New, "&ids=" + strKeyID + "&from=OUTORDER"), "", Resources.Lang.WMS_Common_Element_SNWeiHu, 900, 700);//SN条码维护
            }
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
        strKeyId = this.grdOutOrder_D.DataKeys[rowIndex].Values[0].ToString();
        return strKeyId;
    }
}