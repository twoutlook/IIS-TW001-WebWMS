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
using DreamTek.ASRS.Business.Base;
using System.Data.SqlClient;
using DreamTek.ASRS.Business.Others;

/// <summary>
/// 描述: 预入库通知-->FrmINASN_IAEdit 页面后台类文件
/// 作者: --chenlei
/// 创建于: 2013-01-19 15:16:01
/// </summary>
public partial class RD_FrmInPo_IAEdit : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ShowPOIA_Div1.SetPO = txtPO.ClientID;
        ShowPOIA_Div1.SetVendorCode = txtCVENDERCODE.ClientID;
        ShowPOIA_Div1.SetVendorName = txtCVENDER.ClientID;
        ShowPOIA_Div1.SetPO_ID = txtPO_ID.ClientID;
        ShowPOIA_Div1.GetComp = true;

        if (this.IsPostBack == false)
        {
            this.operation = this.Operation();
            this.InitPage();
            if (this.operation == SYSOperation.Modify)
            {
                this.TabMain0.Visible = true;
                ShowData();
            }
            else if (this.operation == SYSOperation.Preserved1)
            {
                this.operation = SYSOperation.Modify;
                this.TabMain0.Visible = true;
                ShowData();
                //预入库通知单明细
                this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmInPo_IA_DEdit.aspx", SYSOperation.New, "") +
                    "&parentId=" + this.KeyID + "','" + Resources.Lang.FrmInPo_IAEdit_MSG1+ "','INPO_IA_D');");//Roger 2013/12/25 13:59:12
            }
            else if (this.operation == SYSOperation.New)
            {
                this.txtcreateuser.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                this.txtcreatetime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtID.Text = Guid.NewGuid().ToString();
                this.TabMain0.Visible = false;
            }
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
       
        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
        this.btnCreate.Attributes["onclick"] = this.GetPostBackEventReference(this.btnCreate) + ";this.disabled=true;";
        this.btnConfirm.Attributes["onclick"] = this.GetPostBackEventReference(this.btnConfirm) + ";this.disabled=true;";
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INPO_IA');return false;";

        this.btnNew.Attributes["onclick"] = "return PopupFloatWin('" + BuildRequestPageURL("FrmInPo_IA_DEdit.aspx", SYSOperation.New, "") + "&parentId=" + this.KeyID + "','"+Resources.Lang.FrmInPo_IAEdit_MSG1+"','INPO_IA_D',800,600);";
        
        this.grdINASN_IA_D.DataKeyNames = new string[] { "IDS" };
        //状态
        Help.DropDownListDataBind(GetParametersByFlagType("IN_PO_IA"), txtCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");

        //设置保存按钮的文字及其状态
        if (this.operation == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.operation == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.CommonB_Approve;// Resources.Lang.CommonB_Approve;//"审批";
        }
    }

    #endregion

    #region 事件

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        DataBind();
    }

    /// <summary>
    /// 查询事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.CurrendIndex = 1;//索引同步
        DataBind();
    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.btnSave.Enabled = false; //CQ 2013-5-13 13:47:51
        if (this.CheckData())
        {
            string strKeyID = "";
            try
            {
                if (this.operation == SYSOperation.Modify || this.operation == SYSOperation.Preserved1)
                {
                    using (var conn = this.context)
                    {
                        INASN_IA entity = conn.INASN_IA.Where(x => x.id == this.txtID.Text.Trim()).FirstOrDefault();
                        entity = this.SendData(entity);
                        strKeyID = entity.id;
                        entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                        entity.lastupdatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                        conn.INASN_IA.Attach(entity);
                        conn.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        int result = conn.SaveChanges();
                        if (result > 0)
                        {
                            this.AlertAndBack("FrmInPo_IAEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.FrmInPo_IAEdit_MSG1 + Resources.Lang.CommonB_SaveSuccess);
                        }
                        else
                        {
                            this.Alert(Resources.Lang.CommonB_SaveFailed + "!");//保存失败
                        }
                    }
                }
                else if (this.operation == SYSOperation.New)
                {
                    INASN_IA entity = this.SendData(new INASN_IA());
                    strKeyID = Guid.NewGuid().ToString();
                    entity.id = strKeyID;
                    entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                    entity.ccreateownercode = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.dcreatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                    
                    using (var conn = this.context)
                    {
                        conn.INASN_IA.Add(entity);
                        int result = conn.SaveChanges();
                        if (result > 0)
                        {
                            this.AlertAndBack("FrmInPo_IAEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.FrmInPo_IAEdit_MSG1 + Resources.Lang.CommonB_SaveSuccess);
                        }
                        else
                        {
                            Alert(Resources.Lang.CommonB_SaveFailed + "!");
                        }
                    }
                }
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.CommonB_Failed + "！" + E.Message);
                this.btnSave.Enabled = true;
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
        this.btnSave.Enabled = true;
    }

    /// <summary>
    /// 删除功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        string errmsg = string.Empty;
        using (var modContext = this.context)
        {
            using (var dbContextTransaction = modContext.Database.BeginTransaction())
            {
                try
                {
                    var modInasn_ia = modContext.INASN_IA.Where(x => x.id == txtID.Text.Trim()).FirstOrDefault();
                    if (modInasn_ia != null && modInasn_ia.cstatus.Equals("0"))//验证通知单状态
                    { 
                        for (int i = 0; i < this.grdINASN_IA_D.Rows.Count; i++)
                        {
                            if (this.grdINASN_IA_D.Rows[i].Cells[0].Controls[1] is CheckBox)
                            {
                                CheckBox chkSelect = (CheckBox)this.grdINASN_IA_D.Rows[i].Cells[0].Controls[1];
                                if (chkSelect.Checked)
                                {
                                    string ids = this.grdINASN_IA_D.DataKeys[i].Values[0].ToString();
                                    var modIaDetail = modContext.INASN_IA_D.Where(x => x.ids == ids).FirstOrDefault();
                                    if (modIaDetail != null)
                                    {
                                        //获取对应采购单详情
                                        var modInpo_d = modContext.INPO_D.Where(x => x.ids == modIaDetail.inpo_d_ids).FirstOrDefault();
                                        //采购单
                                        var modInpo = modContext.INPO.Where(x => x.id == modInpo_d.id).FirstOrDefault();

                                        //删除自己
                                        modContext.INASN_IA_D.Attach(modIaDetail);
                                        modContext.INASN_IA_D.Remove(modIaDetail);
                                        modContext.SaveChanges();

                                        //判断删除后是否仍然存在预入库单详情
                                        if (modContext.INASN_IA_D.Where(x => x.inpo_d_ids == modInpo_d.ids).Any())
                                        {
                                            if (modInpo_d.status == 2)
                                            {
                                                modInpo_d.status = 1;
                                                modContext.INPO_D.Attach(modInpo_d);
                                                modContext.Entry(modInpo_d).State = System.Data.Entity.EntityState.Modified;
                                            }
                                            //采购单状态为已完成的需变成处理中
                                            if (modInpo.status == 2)
                                            {
                                                modInpo.status = 1;
                                                modContext.INPO.Attach(modInpo);
                                                modContext.Entry(modInpo).State = System.Data.Entity.EntityState.Modified;
                                            }
                                            modContext.SaveChanges();
                                        }
                                        else
                                        {
                                            //不存在明细时
                                            modInpo_d.status = 0;
                                            modContext.INPO_D.Attach(modInpo_d);
                                            modContext.Entry(modInpo_d).State = System.Data.Entity.EntityState.Modified;
                                            modContext.SaveChanges();

                                            //判断采购单明细中是否有已处理过的数据
                                            if (modContext.INPO_D.Where(x => x.status != 0 && x.id == modInpo_d.id).Any())
                                            {
                                                modInpo.status = 1;
                                                modContext.INPO.Attach(modInpo);
                                                modContext.Entry(modInpo).State = System.Data.Entity.EntityState.Modified;
                                            }
                                            else
                                            {
                                                modInpo.status = 0;
                                                modContext.INPO.Attach(modInpo);
                                                modContext.Entry(modInpo).State = System.Data.Entity.EntityState.Modified;
                                            }
                                            modContext.SaveChanges();
                                        }
                                        
                                    }
                                    else {
                                        msg += Resources.Lang.FrmInPo_IAEdit_MSG2 + "！";//数据异常，删除失败
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        msg += Resources.Lang.FrmInPo_IAEdit_MSG3 + "";//预入库通知单状态不是未处理，不能删除
                    }

                    if (msg.Length == 0)
                    {
                        dbContextTransaction.Commit();
                        msg = Resources.Lang.CommonB_RemoveSuccess + "！";//;删除成功
                        this.DataBind();
                        this.Alert(msg);            
                    }
                    else
                    {
                        dbContextTransaction.Rollback();
                        msg = Resources.Lang.CommonB_RemoveFailed + "！\r\n" + msg;//删除失败
                        this.Alert(msg);
                    }                 
                }
                catch (Exception E)
                {
                    dbContextTransaction.Rollback();
                    this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG1 + Resources.Lang.CommonB_RemoveFailed +  "!"  + E.Message.ToJsString());
                }
            }
        }
    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        this.btnCreate.Enabled = false;
        try
        {
            string msg = string.Empty;
            using (var modContext = this.context) {
                if (SavePOInIa(modContext, txtID.Text.Trim(), WmsWebUserInfo.GetCurrentUser().UserNo, 1, out msg)) {
                    this.AlertAndBack("FrmInPo_IAEdit.aspx?" + BuildQueryString(SYSOperation.Modify, txtID.Text.Trim()),  Resources.Lang.CommonB_GenerateSuccess);
                }
                else
                {
                    Alert(msg);
                }
            }
        }
        catch (Exception err)
        {
            Alert(err.Message.ToJsString());
            this.btnCreate.Enabled = true;
        }
        finally
        {
            btnCreate.Style.Remove("disabled");
        }
        this.btnCreate.Enabled = true;
    }

    /// <summary>
    /// 确认按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        this.btnConfirm.Enabled = false;
        try
        {
            string msg = string.Empty;
            using (var modContext = this.context)
            {
                if (SavePOInIa(modContext, txtID.Text.Trim(), WmsWebUserInfo.GetCurrentUser().UserNo, 0, out msg))
                {
                    this.AlertAndBack("FrmInPo_IAEdit.aspx?" + BuildQueryString(SYSOperation.Modify, txtID.Text.Trim()), Resources.Lang.FrmInPo_IAEdit_MSG4 + "");//确认成功

                }
                else
                {
                    Alert(msg);
                }
            }
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
        finally
        {
            btnConfirm.Style.Remove("disabled");
            this.btnConfirm.Enabled = true;
        }
        this.btnConfirm.Enabled = true;
    }

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
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmInPo_IAEdit_Print.aspx", SYSOperation.New, printid) + "','" + Resources.Lang.FrmInPo_IAEdit_MSG1 + "','BAR_REPACK',840,600);");//打印预入库通知单
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }

    protected void grdINASN_IA_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            //+ "预入库通知单明细"
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmInPo_IA_DEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmInPo_IAEdit_MSG1 , "INPO_IA_D");
        }
    }
    #endregion

    #region 方法

    /// <summary>
    /// 获取采购单明细查询条件
    /// </summary>
    /// <returns></returns>
    public IQueryable<INASN_IA_D> GetQueryList()
    {
        var queryList = from p in db.INASN_IA_D
                        select p;
        if (queryList != null)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                queryList = queryList.Where(x => x.id == this.txtID.Text);
            }
            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cinvcode == txtCinvcode.Text.Trim());
            }
        }
        return queryList;
    }

    /// <summary>
    /// 绑定GRIDVIEW数据
    /// </summary>
    public void DataBind()
    {
        var queryList = GetQueryList();

        queryList = queryList.OrderBy(x => x.createtime);

        AspNetPager1.RecordCount = queryList.Count();
        AspNetPager1.PageSize = this.PageSize;
        var data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
        var IN_PO_IA_D = GetParametersByFlagType("IN_PO_IA_D");
        var source = from a in data
                     join sp in IN_PO_IA_D on a.status.ToString() equals sp.FLAG_ID                    
                     select new
                     {
                         a.ids,
                         a.id,
                         a.iline,
                         a.erpcodeline,
                         a.pono,
                         a.poline,
                         a.cinvcode,
                         a.cinvname,
                         a.datecode,
                         a.qtytotal,
                         a.qtypassed,
                         a.qtyunpassed,
                         a.qtypending,
                         sp.FLAG_NAME
                     };
        this.grdINASN_IA_D.DataSource = source.ToList();
        this.grdINASN_IA_D.DataBind();
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        INASN_IA entity = db.INASN_IA.Where(x => x.id == this.KeyID).FirstOrDefault();
        if (entity != null)
        {
            this.txtID.Text = entity.id;
            this.txtCTICKETCODE.Text = entity.cticketcode;
            txtBatchNo.Text = entity.batchno;
            txtPO.Text = entity.pono;
            txtERPCODE.Text = entity.cerpcode;
            txtMESDDEFINE1.Text = entity.ddefine1;
            txtTRADECODE.Text = entity.tradecode;
            txtCURRENCY.Text = entity.currency;
            txtCSTATUS.SelectedValue = entity.cstatus;
            txtCVENDERCODE.Text = entity.cvendercode;
            txtCVENDER.Text = entity.cvender;
            this.field_code.Value = entity.cvendercode;
            this.field_name.Value = entity.cvender;

            txtcreateuser.Text = entity.createowner;
            txtcreatetime.Text = entity.createtime.HasValue ? entity.createtime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            txtupdateuser.Text = entity.lastupdateowner;
            txtupdatetime.Text = entity.lastupdatetime.HasValue ? entity.lastupdatetime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";

            txtCMEMO.Text = entity.cmemo;

            txtPO.Enabled = false;
            txtCVENDERCODE.Enabled = false;
            txtCVENDER.Enabled = false;
            if (entity.cstatus.Equals("0"))
            {
                //确认-未处理
                this.btnConfirm.Enabled = true;
            }
            if (entity.cstatus.Equals("3") || entity.cstatus.Equals("2"))
            {
                //生成-已质检
                this.btnCreate.Enabled = true;
            }
            if (entity.cstatus != "0")
            {
                txtERPCODE.Enabled = false;
                txtMESDDEFINE1.Enabled = false;
                this.btnSave.Enabled = false;
                this.btnNew.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (this.txtID.Text.Trim().Length > 0)
        {
            if (this.txtID.Text.GetLengthByByte() > 36)
            {
                this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG6 + "！");//ID项超过指定的长度36
                this.SetFocus(txtID);
                return false;
            }
        }

        if (this.txtCTICKETCODE.Text.Trim().Length > 0)
        {
            if (this.txtCTICKETCODE.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG7 + "！");//单据号项超过指定的长度20
                this.SetFocus(txtCTICKETCODE);
                return false;
            }
        }
        //
        if (this.txtPO.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG8 + "！");//PO项不允许空
            this.SetFocus(txtPO);
            return false;
        }

        if (this.txtERPCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG9 + "！");//ERPCODE项不允许空
            this.SetFocus(txtERPCODE);
            return false;
        }
        string vFig = Comm_Function.GetConFig("000002");
        if (vFig == "1")
        {
            //获取单据号
            if (txtBatchNo.Text.Trim() == "")
            {
                txtBatchNo.Text = Comm_Function.Fun_GetBatchNo("INASN_IA", txtPO.Text.Trim(), "");
            }
        }
        if (this.txtBatchNo.Text.Trim().Length > 0)
        {

            //检查批次号格式
            string msg = string.Empty;
            if (!(Comm_Function.CheckFun_GetBatchNo(this.txtBatchNo.Text.Trim(), "", out msg)))
            {
                Alert(msg);
                this.SetFocus(txtBatchNo);
                return false;
            }
        }

        if (this.txtMESDDEFINE1.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG10 + "！");//MES料盘数不能为空
            this.SetFocus(txtMESDDEFINE1);
            return false;
        }
        //MES检查是否是数字
        if (this.txtMESDDEFINE1.Text.Trim().Length > 0)
        {
            //检查数量，不允许小数，负数，0 CQ 2014-2-13 13:39:48
            string msg = string.Empty;
            if (!(Comm_Function.Fun_IsDecimal(txtMESDDEFINE1.Text.Trim(), 0, 0, 0, out msg)))
            {
                this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG11 + "" + msg);//MES料盘数

                this.SetFocus(txtMESDDEFINE1);
                return false;
            }

            if (this.txtMESDDEFINE1.Text.GetLengthByByte() > 3)
            {
                this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG12 + "！");//MES料盘数项不能超过3位数
                this.SetFocus(txtMESDDEFINE1);
                return false;
            }

        }

        if (this.txtCSTATUS.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG13 + "！");//状态项不允许空
            this.SetFocus(txtCSTATUS);
            return false;
        }

        if (this.field_code.Value.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG14 + "！");//供应商编码不能为空
            this.SetFocus(txtCVENDERCODE);
            return false;
        }
        txtCVENDERCODE.Text = field_code.Value;

        if (this.field_name.Value.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG15 + "！");//供应商名称项不能为空
            this.SetFocus(txtCVENDER);
            return false;
        }
        txtCVENDER.Text = field_name.Value;

        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 200)
            {
                this.Alert(Resources.Lang.FrmInPo_IAEdit_MSG16 + "！");//备注项超过指定的长度200
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public INASN_IA SendData(INASN_IA entity)
    {
        //单据号
        this.txtCTICKETCODE.Text = this.txtCTICKETCODE.Text.Trim();
        if (this.txtCTICKETCODE.Text.Length > 0)
        {
            entity.cticketcode = txtCTICKETCODE.Text;
        }
        else
        {
            entity.cticketcode = new Fun_CreateNo().CreateNo("INASN_IA");
        }

        this.txtPO.Text = this.txtPO.Text.Trim();
        if (this.txtPO.Text.Length > 0)
        {
            entity.pono = txtPO.Text;
        }

        this.txtBatchNo.Text = this.txtBatchNo.Text.Trim();
        if (this.txtBatchNo.Text.Length > 0)
        {
            entity.batchno = txtBatchNo.Text;
        }

        //Erp单号
        this.txtERPCODE.Text = this.txtERPCODE.Text.Trim();
        if (this.txtERPCODE.Text.Length > 0)
        {
            entity.cerpcode = txtERPCODE.Text;
        }

        //MES料号盘数
        this.txtMESDDEFINE1.Text = this.txtMESDDEFINE1.Text.Trim();
        if (this.txtMESDDEFINE1.Text.Length > 0)
        {
            entity.ddefine1 = txtMESDDEFINE1.Text;
        }

        //贸易代码
        this.txtTRADECODE.Text = this.txtTRADECODE.Text.Trim();
        if (this.txtTRADECODE.Text.Length > 0)
        {
            entity.tradecode = txtTRADECODE.Text;
        }

        //币别
        this.txtCURRENCY.Text = this.txtCURRENCY.Text.Trim();
        if (this.txtCURRENCY.Text.Length > 0)
        {
            entity.currency = txtCURRENCY.Text;
        }

        if (this.txtCSTATUS.SelectedValue.Length > 0)
        {
            entity.cstatus = txtCSTATUS.SelectedValue;
        }
        else
        {
            entity.cstatus = "0";
        }

        this.txtCVENDERCODE.Text = this.txtCVENDERCODE.Text.Trim();
        if (this.txtCVENDERCODE.Text.Length > 0)
        {
            entity.cvendercode = txtCVENDERCODE.Text;
        }

        this.txtCVENDER.Text = this.txtCVENDER.Text.Trim();
        if (this.txtCVENDER.Text.Length > 0)
        {
            entity.cvender = txtCVENDER.Text;
        }

        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }
        return entity;

    }

    public bool SavePOInIa(DBContext modContext,string id, string useNo, int option,out string msg) { 
        bool result = false;
        msg = string.Empty;
        using (var dbContextTransaction = modContext.Database.BeginTransaction())
        {
            //确认
            if (option == 0)
            {
                
                #region 确认流程
                msg = Resources.Lang.FrmInPo_IAEdit_MSG17 + "";//确认
                try
                {
                    #region 验证数据有效性
                    var modInasnIa = modContext.INASN_IA.Where(x => x.id == id).FirstOrDefault();
                    if (modInasnIa == null) {
                        msg += Resources.Lang.CommonB_Failed + "：" + Resources.Lang.FrmInPo_IAEdit_MSG18 + "！";//失败:数据异常
                        return false;
                    }

                    if (modInasnIa.cstatus != "0") {
                        msg = Resources.Lang.FrmInPo_IAEdit_MSG19 + "!";//预入库通知单状态不是[未处理]不能确认

                        return false;
                    }

                    List<INASN_IA_D> modIaDetailList = modContext.INASN_IA_D.Where(x => x.id == id).ToList();
                    if (!modIaDetailList.Any())
                    {
                        msg = Resources.Lang.FrmInPo_IAEdit_MSG20 + "!";//预入库通知单没有明细不能确认
                        return false;
                    }

                    #endregion

                    #region 明细检查处理
                    int vWCount = 0;//未处理数量
                    int vYCount = 0;//质检查数量
                    foreach (var dItem in modIaDetailList) {
                        var poDetail = modContext.INPO_D.Where(x => x.ids == dItem.inpo_d_ids).FirstOrDefault();
                        if (poDetail != null && poDetail.qc == 1)
                        {
                            //如果是免检采购单 
                            dItem.status = 1;
                            dItem.qtypassed = dItem.qtytotal;
                            dItem.qtyunpassed = 0;
                            dItem.qtypending = 0;
                            dItem.qtysampling = 0;
                            dItem.lastupdatetime = DateTime.Now;
                            dItem.lastupdateowner = useNo;

                            modContext.INASN_IA_D.Attach(dItem);
                            modContext.Entry(dItem).State = System.Data.Entity.EntityState.Modified;
                            modContext.SaveChanges();
                            vYCount++;
                        }
                        else {
                            vWCount++;
                        }
                    }
                    #endregion

                    #region 根据明细质检数量修改质检状态
                    if (vYCount == 0 && vWCount > 0)
                    { //全部为未处理的 状态为1已确认
                        modInasnIa.cstatus = "1";
                        modInasnIa.lastupdatetime  = DateTime.Now;
                        modInasnIa.lastupdateowner = useNo;
                        modContext.INASN_IA.Attach(modInasnIa);
                        modContext.Entry(modInasnIa).State = System.Data.Entity.EntityState.Modified;
                        modContext.SaveChanges();
                    }
                    else if (vYCount > 0 && vWCount > 0)
                    { //部分已质检 部分未处理状态为 2 质检中
                        modInasnIa.cstatus = "2";
                        modInasnIa.lastupdatetime = DateTime.Now;
                        modInasnIa.lastupdateowner = useNo;
                        modContext.INASN_IA.Attach(modInasnIa);
                        modContext.Entry(modInasnIa).State = System.Data.Entity.EntityState.Modified;
                        modContext.SaveChanges();
                    }
                    else if (vYCount > 0 && vWCount == 0)
                    {//全部为 已质检 状态为 3 已质检
                        modInasnIa.cstatus = "3";
                        modInasnIa.lastupdatetime = DateTime.Now;
                        modInasnIa.lastupdateowner = useNo;
                        modContext.INASN_IA.Attach(modInasnIa);
                        modContext.Entry(modInasnIa).State = System.Data.Entity.EntityState.Modified;
                        modContext.SaveChanges();
                    }
                    #endregion

                    dbContextTransaction.Commit();
                    msg += Resources.Lang.CommonB_successfully + "！";//成功
                    result = true;
                }
                catch (Exception ex) {
                    dbContextTransaction.Rollback();
                    msg += Resources.Lang.CommonB_Failed + "：" + ex.ToString();//失败
                }
                #endregion

            }
            else if (option == 1)
            {//生成

                #region 生成入库通知单流程
                msg = Resources.Lang.FrmInPo_IAEdit_MSG21 + "";//生成
                try
                {
                    #region 验证数据有效性
                    var modInasnIa = modContext.INASN_IA.Where(x => x.id == id).FirstOrDefault();
                    if (modInasnIa == null)
                    {
                        msg += Resources.Lang.CommonB_Failed + "：" + Resources.Lang.FrmInPo_IAEdit_MSG18 + "！";//失败:数据异常
                        return false;
                    }

                    if (modInasnIa.cstatus != "2" && modInasnIa.cstatus != "3")
                    {
                        msg = Resources.Lang.FrmInPo_IAEdit_MSG28 + "!";//预入库通知单状态不是[质检中、已质检]不能生成入库通知单
                        return false;
                    }

                    List<INASN_IA_D> modIaDetailList = modContext.INASN_IA_D.Where(x => x.id == id).ToList();
                    if (!modIaDetailList.Any(x => x.status == 1)) {
                        msg = Resources.Lang.FrmInPo_IAEdit_MSG27 + "!";//预入库通知单不存在[已质检]的明细不能生成入库通知单
                        return false;
                    }

                    if (string.IsNullOrEmpty(modInasnIa.cerpcode))
                    {
                        msg = Resources.Lang.FrmInPo_IAEdit_MSG26 + "!";//预入库通知单没有ERPcode
                        return false;                  
                    }

                    var query = from t in modContext.INASN
                                join d in modContext.INASN_D on t.id equals d.id
                                where t.inasn_ia_id == id
                                select new
                                {
                                    d.inasn_ia_d_ids
                                };
                    if (query.ToList().Distinct().Count() == modIaDetailList.Count) {
                        msg = Resources.Lang.FrmInPo_IAEdit_MSG25 + "!";//预入库通知单明细已全部生成入库通知单
                        return false;
                    }

                    if (!modIaDetailList.Any(x => !string.IsNullOrEmpty(x.erpcodeline))) {
                        msg = Resources.Lang.FrmInPo_IAEdit_MSG24 + "!";//预入库通知单明细没有ERPcodeLine
                        return false;
                    }
                    #endregion

                    #region 更新明细状态
                    modContext.Database.ExecuteSqlCommand(" update inasn_ia_d  set status = 9 where status = 1 and id = @P_InIa_ID", new SqlParameter("@P_InIa_ID", id));
                    modContext.SaveChanges();
                    #endregion

                    #region 检查是否存在通过的通知单
                    List<INASN_IA_D> modIaDList = modContext.INASN_IA_D.Where(x => x.id == id && x.status == 9 && x.qtypassed > 0).ToList();
                    if (modIaDList.Any())
                    {
                        string Asn_ID = Guid.NewGuid().ToString();
                        string Asn_Code = new Fun_CreateNo().CreateNo("INASN");

                        #region 生成入库通知单表头
                        INASN modInasn = new INASN();
                        modInasn.id = Asn_ID;
                        modInasn.ccreateownercode = useNo;
                        modInasn.dcreatetime = DateTime.Now;
                        modInasn.cauditpersoncode = null;
                        modInasn.dauditdate = null;
                        modInasn.cticketcode = Asn_Code;
                        modInasn.cstatus = "0";
                        modInasn.cpo = modInasnIa.pono;
                        modInasn.itype = "18";//单据类型
                        modInasn.cmemo = Resources.Lang.FrmInPo_IAEdit_MSG23;// +"预入库通知单生成-通过";
                        modInasn.cerpcode = modInasnIa.cerpcode;
                        modInasn.cdefine1 = modInasnIa.tradecode;
                        modInasn.cdefine2 = modInasnIa.currency;
                        modInasn.ddefine3 = "N";//是否判退
                        modInasn.ddefine4 = "1";//数据来源
                        modInasn.idefine5 = 0;//特殊元件退料 0:正常 1:特殊
                        modInasn.cvendercode = modInasnIa.cvendercode;
                        modInasn.cvender = modInasnIa.cvender;
                        modInasn.inasn_ia_id = modInasnIa.id;
                        modInasn.specil_return = 0;//1-工单整盘退料 0-其他
                        modInasn.critical_part = 0;//0-一般 1 急料
                        modContext.INASN.Add(modInasn);
                        modContext.SaveChanges();
                        #endregion

                        #region 生成入库通知单明细
                        foreach (var mod in modIaDList) {
                            //判断明细有没生成过
                            if (!modContext.INASN_D.Any(x => x.inasn_ia_d_ids == mod.ids))
                            {
                                INASN_D modIn_d = new INASN_D();
                                modIn_d.ids = Guid.NewGuid().ToString();
                                modIn_d.id = Asn_ID;
                                modIn_d.cstatus = "0";
                                modIn_d.cinvcode = mod.cinvcode;
                                modIn_d.cinvname = mod.cinvname;
                                modIn_d.iquantity = mod.qtypassed.HasValue ? mod.qtypassed.Value : 0;
                                modIn_d.cinvbarcode = null;
                                modIn_d.cbatch = modInasnIa.batchno;
                                modIn_d.cmemo = null;
                                modIn_d.cerpcodeline = mod.erpcodeline;
                                modIn_d.cdefine1 = null;
                                modIn_d.cdefine2 = null;
                                modIn_d.ddefine3 = null;
                                modIn_d.ddefine4 = null;
                                modIn_d.idefine5 = null;
                                modIn_d.cpo = mod.pono;
                                modIn_d.ipoline = Convert.ToDecimal(mod.poline);
                                //modIn_d.ILINE = mod.iline;
                                modIn_d.erpcodelinenum = null;
                                modIn_d.po_numbername = null;
                                modIn_d.po_linenumbername = null;
                                modIn_d.manual = 0;
                                modIn_d.datecode = mod.datecode;
                                modIn_d.inasn_ia_d_ids = mod.ids;
                                modContext.INASN_D.Add(modIn_d);
                                modContext.SaveChanges();
                            }
                        }
                        #endregion

                    }
                    #endregion

                    #region 检查待检数量
                    List<INASN_IA_D> modPending = modContext.INASN_IA_D.Where(x => x.id == id && x.status == 9 && x.qtypending > 0).ToList();
                    if (modPending.Any())
                    {
                        string Asn_ID = Guid.NewGuid().ToString();
                        string Asn_Code = new Fun_CreateNo().CreateNo("INASN");

                        #region 生成入库通知单表头
                        INASN modInasn = new INASN();
                        modInasn.id = Asn_ID;
                        modInasn.ccreateownercode = useNo;
                        modInasn.dcreatetime = DateTime.Now;
                        modInasn.cauditpersoncode = null;
                        modInasn.dauditdate = null;
                        modInasn.cticketcode = Asn_Code;
                        modInasn.cstatus = "0";
                        modInasn.cpo = modInasnIa.pono;
                        modInasn.itype = "18";//单据类型
                        modInasn.cmemo = Resources.Lang.FrmInPo_IAEdit_MSG22 + "";//预入库通知单生成-待检
                        modInasn.cerpcode = modInasnIa.cerpcode;
                        modInasn.cdefine1 = modInasnIa.tradecode;
                        modInasn.cdefine2 = modInasnIa.currency;
                        modInasn.ddefine3 = "N";//是否判退
                        modInasn.ddefine4 = "1";//数据来源
                        modInasn.idefine5 = 0;//特殊元件退料 0:正常 1:特殊
                        modInasn.cvendercode = modInasnIa.cvendercode;
                        modInasn.cvender = modInasnIa.cvender;
                        modInasn.inasn_ia_id = modInasnIa.id;
                        modInasn.specil_return = 0;//1-工单整盘退料 0-其他
                        modInasn.critical_part = 0;//0-一般 1 急料
                        modContext.INASN.Add(modInasn);
                        modContext.SaveChanges();
                        #endregion

                        #region 生成入库通知单明细
                        List<INASN_D> list_Inasn_d = new List<INASN_D>();
                        foreach (var mod in modPending)
                        {
                            INASN_D modIn_d = new INASN_D();
                            modIn_d.ids = Guid.NewGuid().ToString();
                            modIn_d.id = Asn_ID;
                            modIn_d.cstatus = "4";
                            modIn_d.cinvcode = mod.cinvcode;
                            modIn_d.cinvname = mod.cinvname;
                            modIn_d.iquantity = mod.qtypending.HasValue ? mod.qtypending.Value : 0;
                            modIn_d.cinvbarcode = null;
                            modIn_d.cbatch = modInasnIa.batchno;
                            modIn_d.cmemo = null;
                            modIn_d.cerpcodeline = mod.erpcodeline;
                            modIn_d.cdefine1 = null;
                            modIn_d.cdefine2 = null;
                            modIn_d.ddefine3 = null;
                            modIn_d.ddefine4 = null;
                            modIn_d.idefine5 = null;
                            modIn_d.cpo = mod.pono;
                            modIn_d.ipoline = Convert.ToDecimal(mod.poline);
                            //modIn_d.ILINE = mod.iline;
                            modIn_d.erpcodelinenum = null;
                            modIn_d.po_numbername = null;
                            modIn_d.po_linenumbername = null;
                            modIn_d.manual = 1;
                            modIn_d.datecode = mod.datecode;
                            modIn_d.inasn_ia_d_ids = mod.ids;
                            modContext.INASN_D.Add(modIn_d);
                            modContext.SaveChanges();
                            list_Inasn_d.Add(modIn_d);
                        }
                        #endregion

                        #region 获取保税待检的储位
                        string vYPositionCode = "";
                        string vYPositionName = "";
                        
                        //获取保税待检的储位
                        var a = modContext.SYS_PARAMETER.Where(x => x.flag_type == "PO_PENDING" && x.sortid == 0).FirstOrDefault();
                        if (a != null) {
                            vYPositionCode = a.flag_id;
                            vYPositionName = a.flag_name;
                        }

                        string vNPositionCode = "";
                        string vNPositionName = "";
                        //获取非保税待检的储位
                        var b = modContext.SYS_PARAMETER.Where(x => x.flag_type == "PO_PENDING" && x.sortid == 1).FirstOrDefault();
                        if (b != null) {
                            vNPositionCode = b.flag_id;
                            vNPositionName = b.flag_name;
                        }
                        #endregion

                        #region 生成入库单
                        string Bill_ID = Guid.NewGuid().ToString();
                        string Bill_Code = new Fun_CreateNo().CreateNo("INBILL");

                        #region 生成入库单表头
                        INBILL modInbill = new INBILL();
                        modInbill.id = Bill_ID;
                        modInbill.ccreateownercode = useNo;
                        modInbill.dcreatetime = DateTime.Now;
                        modInbill.cauditperson = null;
                        modInbill.daudittime = null;
                        modInbill.cticketcode = Bill_Code;
                        modInbill.cstatus = "0";
                        modInbill.casnid = Asn_ID;
                        modInbill.dindate = DateTime.Now;
                        modInbill.cmemo = modInasn.cmemo;
                        modInbill.cerpcode = modInasn.cerpcode;
                        modInbill.cdefine1 = modInasn.cdefine1;
                        modInbill.cdefine2 = modInasn.cdefine2;
                        modInbill.idefine5 = null;
                        modInbill.itype = Convert.ToDecimal(modInasn.itype);
                        modInbill.cvendercode = modInasn.cvendercode;
                        modInbill.cvender = modInasn.cvender;
                        modContext.INBILL.Add(modInbill);
                        modContext.SaveChanges();
                        #endregion

                        #region 生成入库单详情
                        List<INBILL_D> list_inbill_d = new List<INBILL_D>();
                        foreach (var mod in list_Inasn_d)
                        {
                            INBILL_D mod_bill_d = new INBILL_D();
                            mod_bill_d.ids = Guid.NewGuid().ToString();
                            mod_bill_d.id = Bill_ID;
                            mod_bill_d.cstatus = "0";
                            mod_bill_d.cinvcode = mod.cinvcode;
                            mod_bill_d.cinvname = mod.cinvname;
                            mod_bill_d.iquantity = mod.iquantity;
                            mod_bill_d.cinvbarcode = null;
                            mod_bill_d.cerpcodeline = mod.cerpcodeline;
                            mod_bill_d.cmemo = null;
                            var mod_base_part = modContext.BASE_PART.Where(x => x.cpartnumber == mod.cinvcode).FirstOrDefault();
                            mod_bill_d.cpositioncode = mod_base_part.bonded == 0 ? vYPositionCode : vNPositionCode;
                            mod_bill_d.cposition = mod_base_part.bonded == 0 ? vYPositionName : vNPositionName;
                            mod_bill_d.dindate = DateTime.Now;
                            mod_bill_d.cinpersoncode = useNo;
                            mod_bill_d.iasnline = 1;
                            mod_bill_d.cdefine1 = mod.datecode.ToString();
                            mod_bill_d.cdefine2 = null;
                            mod_bill_d.ddefine3 = null;
                            mod_bill_d.ddefine4 = null;
                            mod_bill_d.idefine5 = null;
                            mod_bill_d.asn_d_ids = mod.ids;

                            modContext.INBILL_D.Add(mod_bill_d);
                            modContext.SaveChanges();
                            list_inbill_d.Add(mod_bill_d);
                        }

                        #endregion

                        #endregion

                        #region 插入批次号表数据
                        string vBSFig = string.Empty;
                        var config = modContext.SYS_CONFIG.Where(x=>x.code == "000002").FirstOrDefault();
                        if (config != null)
                        {
                            vBSFig = config.config_value;
                        }
                        if (vBSFig == "1") {
                            foreach (var mod in list_inbill_d)
                            {
                                INBILL_D_SN mod_inbill_d_sn = new INBILL_D_SN();
                                mod_inbill_d_sn.id = Guid.NewGuid().ToString();
                                mod_inbill_d_sn.inbill_id = mod.id;
                                mod_inbill_d_sn.inbill_d_ids = mod.ids;
                                mod_inbill_d_sn.sn_code = modInasnIa.batchno;
                                mod_inbill_d_sn.datecode = Convert.ToDecimal(mod.cdefine1);
                                mod_inbill_d_sn.cinvcode = mod.cinvcode;
                                mod_inbill_d_sn.quantity = mod.iquantity;
                                mod_inbill_d_sn.createtime = DateTime.Now;
                                mod_inbill_d_sn.createowner = useNo;
                                mod_inbill_d_sn.lastvpdtime = DateTime.Now;
                                mod_inbill_d_sn.lastvpdowner = useNo;
                                mod_inbill_d_sn.worktype = "0";//0 补单
                                mod_inbill_d_sn.istransed = "0";//0未抛转
                                mod_inbill_d_sn.transedtime = null;
                                mod_inbill_d_sn.union_id = null;
                                mod_inbill_d_sn.scan_ids = null;
                                mod_inbill_d_sn.sntype = 3;
                                mod_inbill_d_sn.line_qty = null;
                                modContext.INBILL_D_SN.Add(mod_inbill_d_sn);
                                modContext.SaveChanges();
                            }
                        }
                        #endregion

                    }
                    #endregion

                    #region 更改预入库通知单状态为已完成
                    modContext.Database.ExecuteSqlCommand(" update inasn_ia_d set status = 2 where status = 9 and id = @P_InIa_ID", new SqlParameter("@P_InIa_ID", id));
                    modContext.SaveChanges();
                    #endregion

                    if (modContext.INASN_IA_D.Where(x => x.status != 2 && x.id == id).Any()) {
                        modInasnIa.cstatus = "4";
                        modContext.INASN_IA.Attach(modInasnIa);
                        modContext.Entry(modInasnIa).State = System.Data.Entity.EntityState.Modified;
                        modContext.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                    msg += Resources.Lang.CommonB_successfully + "！"; //成功
                    result = true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    msg += Resources.Lang.CommonB_Failed + "：" + ex.ToString();//失败
                }
                #endregion

            }
        }
        return result;
    }

    #endregion

    #region IPageGrid 成员

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdINASN_IA_D.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdINASN_IA_D.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }
    #endregion

}

