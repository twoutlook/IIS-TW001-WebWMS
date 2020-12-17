using DreamTek.ASRS.Business.Others;
using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_OUT_FrmOutOrder_DEdit : WMSBasePage
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

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ShowPARTDiv1.SetCINVCODE = this.txtCINVCODE.ClientID;
        this.ShowPARTDiv1.SetCINVNAME = this.txtCINVNAME.ClientID;
        this.ShowPARTDiv1.GetComp = true;
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.operation != SYSOperation.New)
            {
                ShowData();
            }
            else
            {
                string IDS = Request.QueryString["IDS"];
                LoadIDS(IDS);
            }
        }
    }

    public void InitPage()
    {
        this.operation = this.Operation();
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTORDER_D');return false;";
        //设置保存按钮的文字及其状态
        if (this.operation == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        
        //状态
        Help.DropDownListDataBind(GetParametersByFlagType("OUTORDER_D"), txtCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
    }

    public void ShowData()
    {
        OUTORDER_D entity = this.context.OUTORDER_D.Where(x => x.ids == this.KeyID).FirstOrDefault();
        if (entity != null)
        {
            this.txtIDS.Text = entity.ids;
            this.txtID.Text = entity.id;
            this.txtOrderLine.Text = entity.OrderLine.ToString();
            this.txtCINVCODE.Text = entity.CinvCode;
            this.txtCINVNAME.Text = entity.CinvName;
            this.txtIQUANTITY.Text = Convert.ToDecimal(entity.Iquantity).ToString("f2");
            //txtUNIT.Text = entity.unit;
            this.txtPrice.Text = entity.Price.ToString("f2");
            this.txtAmount.Text = entity.Amount.ToString("f2");
            this.txtCSTATUS.SelectedValue = entity.Status.ToString();
            this.txtCREATEOWNER.Text = entity.CreateOwner;
            this.txtCREATETIME.Text = entity.CreateTime.HasValue ? entity.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtLASTUPDATEOWNER.Text = entity.LastUpdateOwner;
            this.txtLASTUPDATETIME.Text = entity.LastUpdateTime.HasValue ? entity.LastUpdateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtSaleDate.Text = entity.SaleDate.HasValue ? entity.SaleDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtFinishQty.Text = entity.FinishQty.HasValue ? entity.FinishQty.Value.ToString("f2") : "0.00";
            this.txtOrderLine.Enabled = false;
            this.txtCINVCODE.Enabled = false;
            this.txtCINVNAME.Enabled = false;
            this.txtSaleDate.Enabled = false;

            var order = context.OUTORDER.Where(x => x.id == entity.id).FirstOrDefault();
            if (order != null) {//erp来的单号只能看，不能修改
                if (order.OrderSource == 0)
                {
                    btnSave.Enabled = false;
                }
                if (order.Status != 0) {
                    btnSave.Enabled = false;
                }
            }
        }
    }

    public void LoadIDS(string IDS)
    {
        this.txtID.Text = IDS;
        this.txtOrderLine.Text = aotuIDS(this.txtID.Text).ToString();
        this.txtID.Enabled = false;
        this.txtIDS.Text = Guid.NewGuid().ToString();
        this.txtIDS.Enabled = false;
        txtCREATEOWNER.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
        txtCREATETIME.Text = Comm_Function.GetDBDateTime("yyyy-MM-dd HH:mm:ss");
        this.txtCSTATUS.SelectedValue = "0";
    }

    /// <summary>
    /// 获取下一个子项的编号
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private int aotuIDS(string id)
    {
        int maxPoline = 0;
        if (db.OUTORDER_D.Where(x => x.id == id).Any())
        {
            maxPoline = db.OUTORDER_D.Where(x => x.id == id).Max(x => x.OrderLine);
        }
        maxPoline++;
        return maxPoline;
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (this.txtOrderLine.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOutOrder_DEdit_Tips_NeedOrderLine);//订单项次不允许空
            this.SetFocus(txtOrderLine);
            return false;
        }
        if (txtOrderLine.Text.Trim() != "")
        {
            if (this.operation == SYSOperation.New)
            {
                //订单项次不为空的时候，不允许重复
                if (CheckOrderLine(txtID.Text.Trim(), txtOrderLine.Text.Trim()))
                {
                    this.Alert(Resources.Lang.FrmOutOrder_DEdit_Tips_SameOrderLine + "[" + txtOrderLine.Text.Trim() + "]");//this.Alert("订单中已存在相同的订单项次号[" + txtOrderLine.Text.Trim() + "]");
                    this.SetFocus(txtOrderLine);
                    return false;
                }
            }
        }

        if ((this.txtCINVCODE.Text.Trim() == ""))
        {
            this.Alert(Resources.Lang.FrmOutOrder_DEdit_Tips_NeedCinvcode);//料号不允许为空
            this.SetFocus(txtCINVCODE);
            return false;
        }

        //检查料号是否存在
        if (!this.Check_Part_IsExist(txtCINVCODE.Text.Trim().ToUpper()))
        {
            this.Alert(Resources.Lang.FrmOutOrder_DEdit_Tips_NotExistsCinvcode);//料号不存在
            this.SetFocus(txtCINVCODE);
            return false;
        }

        if (this.operation == SYSOperation.New)
        {
            if (this.CheckPoCinvCode(txtID.Text.Trim(), txtCINVCODE.Text.Trim().ToUpper()))
            {
                this.Alert(Resources.Lang.FrmOutOrder_DEdit_Tips_HasCinvcode);//订单中已存在相同的料号
                this.SetFocus(txtCINVCODE);
                return false;
            }
        }

        if ((this.txtCINVNAME.Text.Trim() == ""))
        {
            this.Alert(Resources.Lang.FrmOutOrder_DEdit_Tips_NeedCinvname);//品名不允许为空
            this.SetFocus(txtCINVCODE);
            return false;
        }
        if ((this.txtIQUANTITY.Text.Trim() == ""))
        {
            this.Alert(Resources.Lang.FrmOutOrder_DEdit_Tips_NeedQuantity);//数量不允许为空
            this.SetFocus(txtIQUANTITY);
            return false;
        }

        string msg = string.Empty;

        if (this.txtIQUANTITY.Text.GetLengthByByte() > 12)
        {
            this.Alert(Resources.Lang.FrmOutOrder_DEdit_Tips_QuantityToLang);//数量位数超过指定的长度12！
            this.SetFocus(txtIQUANTITY);
            return false;
        }
        //检查数量，不允许小数，负数，0 CQ 2015-2-12 13:38:24
        if (!(Comm_Function.Fun_IsDecimal(txtIQUANTITY.Text.Trim(), 0, 0, 1, out msg)))
        {
            this.Alert(msg);
            this.SetFocus(txtIQUANTITY);
            return false;
        }

        if (this.txtPrice.Text.GetLengthByByte() > 12) {
            this.Alert(Resources.Lang.FrmOutOrder_DEdit_Tips_PriceToLang);//单价位数超过指定的长度12！
            this.SetFocus(txtPrice);
            return false;
        }

        //检查价格，允许小数，不允许负数，0 
        if (!(Comm_Function.Fun_IsDecimal(txtPrice.Text.Trim(), 0, 1, 1, out msg)))
        {
            this.Alert(Resources.Lang.FrmOutOrder_DEdit_Tips_NeedPrice);//请输入有效的单价
            this.SetFocus(txtPrice);
            return false;
        }
        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public OUTORDER_D SendData()
    {
        OUTORDER_D entity = new OUTORDER_D();
        entity.ids = this.txtIDS.Text.Trim();
        entity.id = this.txtID.Text.Trim();
        entity.OrderLine = Convert.ToInt32(this.txtOrderLine.Text.Trim());
        entity.CinvCode = this.txtCINVCODE.Text.Trim();
        entity.CinvName = this.txtCINVNAME.Text.Trim();
        entity.Iquantity = Convert.ToDecimal(this.txtIQUANTITY.Text.Trim());
        //entity.unit = this.txtUNIT.Text.Trim();
        if (this.txtPrice.Text.Trim().Length > 0)
        {
            entity.Price = Convert.ToDecimal(this.txtPrice.Text.Trim());
            entity.Amount = entity.Price * Convert.ToDecimal(entity.Iquantity);
        }
        
        if (this.operation == SYSOperation.New)
        {
            entity.Status = 0;
            entity.SaleDate = DateTime.Now;
        }
        else
        {
            entity.Status = Convert.ToInt32(txtCSTATUS.SelectedValue);
        }
        entity.CreateOwner = txtCREATEOWNER.Text;
        entity.CreateTime = Convert.ToDateTime(txtCREATETIME.Text);
        entity.LastUpdateOwner = txtLASTUPDATEOWNER.Text;
        if (txtLASTUPDATETIME.Text.Trim().Length > 0)
        {
            entity.LastUpdateTime = Convert.ToDateTime(txtLASTUPDATETIME.Text);
        }
        return entity;
    }

    /// 判断料号是否存在
    /// <summary>
    /// 判断料号是否存在
    /// </summary>
    /// <param name="cinvcode">料号</param>
    /// <returns>存在 true 不存在 false</returns>
    private bool Check_Part_IsExist(string cinvcode)
    {
        if (string.IsNullOrEmpty(cinvcode))
        {
            return false;
        }
        var obj = db.BASE_PART.Where(x => x.cpartnumber == cinvcode).FirstOrDefault();
        if (obj != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //检查采购单中是否存在相同料号
    public bool CheckPoCinvCode(string inpo_id, string cinvcode)
    {
        var obj = db.OUTORDER_D.Where(x => x.id == inpo_id && x.CinvCode == cinvcode).FirstOrDefault();
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
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.btnSave.Enabled = false; 
        if (this.CheckData())
        {
            string msg = string.Empty;
            OUTORDER_D entity = this.SendData();
            string strKeyID = "";
            strKeyID += entity.ids;
            try
            {
                bool isSuccess = true;
                if (this.operation == SYSOperation.Modify)
                {
                    using (var conn = this.context)
                    {
                        entity.LastUpdateOwner = WmsWebUserInfo.GetCurrentUser().UserNo;
                        entity.LastUpdateTime = Comm_Function.GetDBDateTime();
                        conn.OUTORDER_D.Attach(entity);
                        conn.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        int result = conn.SaveChanges();
                        if (result > 0)
                        {
                            var modOrder = conn.OUTORDER.Where(x => x.id == entity.id).FirstOrDefault();
                            if (modOrder != null) {
                                modOrder.Amount = 0;
                                modOrder.AfterTaxAmount = 0;
                                List<OUTORDER_D> modOrder_dList = conn.OUTORDER_D.Where(x => x.id == entity.id).ToList();
                                foreach (var item in modOrder_dList)
                                {
                                    modOrder.Amount += item.Amount;
                                }
                                modOrder.AfterTaxAmount = modOrder.Amount + modOrder.Amount * (modOrder.TaxRate == 0 ? 0 : (modOrder.TaxRate / 100));
                                conn.OUTORDER.Attach(modOrder);
                                conn.Entry(modOrder).State = System.Data.Entity.EntityState.Modified;
                                conn.SaveChanges();
                            }
                            msg = Resources.Lang.WMS_Common_Msg_SaveSuccess;//保存成功!
                        }
                        else
                        {
                            msg = Resources.Lang.WMS_Common_Msg_SaveFailed;//保存失败!
                            isSuccess = false;
                        }
                    }
                }
                else if (this.operation == SYSOperation.New)
                {
                    strKeyID = Guid.NewGuid().ToString();
                    entity.ids = strKeyID;
                    entity.FinishQty = 0;
                    using (var conn = this.context)
                    {
                        conn.OUTORDER_D.Add(entity);
                        int result = conn.SaveChanges();
                        if (result > 0)
                        {
                            var modOrder = conn.OUTORDER.Where(x => x.id == entity.id).FirstOrDefault();
                            if (modOrder != null)
                            {
                                modOrder.Amount += entity.Amount;
                                modOrder.AfterTaxAmount = modOrder.Amount + modOrder.Amount* (modOrder.TaxRate == 0 ? 0 : (modOrder.TaxRate / 100));
                                conn.OUTORDER.Attach(modOrder);
                                conn.Entry(modOrder).State = System.Data.Entity.EntityState.Modified;
                                conn.SaveChanges();
                            }
                            msg = Resources.Lang.WMS_Common_Msg_SaveSuccess;//保存成功!
                        }
                        else
                        {
                            msg = Resources.Lang.WMS_Common_Msg_SaveFailed;//保存失败!
                            isSuccess = false;
                        }
                    }
                }

                if (isSuccess)//保存成功!
                {
                    this.Alert(msg);
                    this.WriteScript("window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTORDER_D');");
                }
                else
                {
                    this.Alert(msg);
                }
            }
            catch (Exception ex)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.WMS_Common_Msg_Failed + ex.Message);//失败！
                this.btnSave.Enabled = true;
            }
        }
        this.btnSave.Enabled = true;
    }

    public bool CheckOrderLine(string outOrderId, string orderLine)
    {
         var item = this.context.OUTORDER_D.Where(x => x.id == outOrderId && x.OrderLine.ToString() == orderLine).FirstOrDefault();
         if (item != null)
         {
             return true;
         }
         else {
             return false;
         }
    }
}