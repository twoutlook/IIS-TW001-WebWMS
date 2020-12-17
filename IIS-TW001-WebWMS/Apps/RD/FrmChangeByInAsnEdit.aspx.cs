using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using DreamTek.ASRS.Business.SP.ProcedureModel;
using DreamTek.ASRS.Business.RD;


public partial class Apps_RD_FrmChangeByInAsnEdit : WMSBasePage
{
    //page load
    protected void Page_Load(object sender, EventArgs e)
    {
        ucINASN_Div.SetCompName = txtOutAsnCticketCode.ClientID;
        // ucINASN_Div.SetORGCode = hfINASN_id.ClientID;
        ucINASN_Div.SetORGCode = txtOutAsnCticketID.ClientID;

        if (IsPostBack == false)
        {
            InitPage();
            if (Operation() == SYSOperation.Modify)//修改
            {
                ShowData();
                btnOK.Visible = true;
                btnSave.Visible = true;
            }
            else if (Operation() == SYSOperation.Approve)//审核
            {
                ShowData();
                btnOK.Visible = false;
                btnSave.Visible = false;
            }
            else
            {
                //制单人
                txtCCREATEOWNERCODE.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                //制单时间
                txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//CommonFunction.GetDBNowTime().Value.ToString();
                txtID.Text = Guid.NewGuid().ToString();
                //隐藏完成按钮
                btnOK.Visible = false;
                btnSave.Visible = true;
            }
            //查询
            btnSearch_Click(btnSearch, EventArgs.Empty);
        }
        //FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //HasRight();

        btnSave.Attributes["onclick"] = GetPostBackEventReference(btnSave) + ";disabled=true;";
        btnOK.Attributes["onclick"] = GetPostBackEventReference(btnOK) + ";disabled=true;";
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
        cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/Page.css";
        btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('ChangeByOutAsn');return false;";
        //状态
        Help.DropDownListDataBind(GetParametersByFlagType("ASNCHANGE"), ddlCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        txtOutAsnCticketCode.Attributes["onclick"] = "Show('" + ucINASN_Div.GetDivName + "');";
    }

    private INASNCHANGE GetBO()
    {
        IGenericRepository<INASNCHANGE> conn = new GenericRepository<INASNCHANGE>(db);
        var entity = (from p in conn.Get()
                      where p.id == KeyID
                      select p).FirstOrDefault();
        return entity;
    }

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {

        var entity = GetBO();
        grdInChange_d.Visible = true;
        //grdNavigatorInChange_d.Visible = true;
        btnSearch.Visible = true;
        txtCinvcode.Visible = true;
        Label8.Visible = true;
        btnDel.Visible = true;
        //var entity = new InAsnChangeEntity { ID = KeyID };
        //entity.SelectByPKeys();
        txtID.Text = entity.id;
        //单据号
        txtCTICKETCODE.Text = entity.cticketcode;
        //通知单单号
        txtOutAsnCticketCode.Text = entity.inasn_cticketcode;
        //状态
        ddlCSTATUS.SelectedValue = entity.cstatus;
        //制单人
        txtCCREATEOWNERCODE.Text = entity.create_owner;
        //制单时间
        txtDCREATETIME.Text = entity.create_time.Value.ToString("yyyy-MM-dd HH:mm:ss");
        txtCMEMO.Text = entity.cmemo;
        //设置文本框是否可编辑
        txtCTICKETCODE.Enabled = false;
        txtOutAsnCticketCode.Enabled = false;
        ddlCSTATUS.Enabled = false;
        if (entity.cstatus != "0")
        {
            btnDel.Enabled = false;
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        string msg = string.Empty;
        //
        if (txtID.Text.Trim() == "")
        {
            Alert(Resources.Lang.FrmChangeByInAsnEdit_MSG1 + "！");//ID项不允许空
            SetFocus(txtID);
            return false;
        }
        //
        if (txtID.Text.Trim().Length > 0)
        {
            if (txtID.Text.GetLengthByByte() > 50)
            {
                Alert(Resources.Lang.FrmChangeByInAsnEdit_MSG2 + "！");//ID项超过指定的长度50
                SetFocus(txtID);
                return false;
            }
        }
        //
        if (txtCCREATEOWNERCODE.Text.Trim() == "")
        {
            Alert(Resources.Lang.FrmChangeByInAsnEdit_MSG3 + "！");//制单人项不允许空
            SetFocus(txtCCREATEOWNERCODE);
            return false;
        }
        //
        if (txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (txtCCREATEOWNERCODE.Text.GetLengthByByte() > 50)
            {
                Alert(Resources.Lang.FrmChangeByInAsnEdit_MSG4 + "！");//制单人项超过指定的长度50
                SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }
        //
        if (txtDCREATETIME.Text.Trim() == "")
        {
            Alert(Resources.Lang.FrmChangeByInAsnEdit_MSG5 + "！");//制单日期项不允许空
            SetFocus(txtDCREATETIME);
            return false;
        }
        //
        if (txtCMEMO.Text.Trim().Length > 0)
        {
            if (txtCMEMO.Text.GetLengthByByte() > 200)
            {
                Alert(Resources.Lang.FrmChangeByInAsnEdit_MSG6 + "！");//备注项超过指定的长度200
                SetFocus(txtCMEMO);
                return false;
            }
        }
        //
        if (txtOutAsnCticketCode.Text.Trim().Length == 0)
        {
            //Alert("ERP单号不能為空！");
            Alert(Resources.Lang.FrmChangeByInAsnEdit_MSG7 + "！");//請输入通知单单号
            SetFocus(txtOutAsnCticketCode);
            return false;
        }

        if (SYSOperation.New == this.Operation())
        {
            INQuery qry = new INQuery();
            string str = qry.Fun_CheckINChange(txtOutAsnCticketCode.Text.Trim());
            if (!str.IsNullOrEmpty() && str.ToUpper().Equals("OK"))
            {
                return true;
            }
            else
            {
                Alert(str);
                return false;
            }
        }


        return true;

    }

    ///// <summary>
    ///// 根据页面上的数据构造相应的实体类返回
    ///// </summary>
    ///// <returns></returns>
    //public BaseEntity SendData()
    //{
    //    return null;
    //}

    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
        //取消灰化
        btnSave.Style.Remove("disabled");
    }

    private void SaveData()
    {
        string msg = string.Empty;
        if (CheckData())
        {
            var strKeyID = "";
            try
            {
                if (Operation() == SYSOperation.Modify)
                {
                    strKeyID = txtID.Text.Trim();

                    //var entity = new InAsnChangeEntity { ID = strKeyID };
                    //entity.SelectByPKeys();
                    var entity = GetBO();
                    entity.cmemo = txtCMEMO.Text.Trim();
                    entity.last_upd_owner = WmsWebUserInfo.GetCurrentUser().UserNo;
                    entity.last_upd_time = DateTime.Now;

                    IGenericRepository<INASNCHANGE> conn = new GenericRepository<INASNCHANGE>(db);
                    conn.Update(entity);
                    conn.Save();

                    //RD_FrmInChangeCtl.Update(entity);
                    //DBUtil.Commit();
                    AlertAndBack("FrmChangeByInAsnEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.FrmChangeByInAsnEdit_MSG8);// + "更新成功"
                }
                else if (Operation() == SYSOperation.New)
                {
                    //DBUtil.BeginTrans();
                    try
                    {
                        strKeyID = Guid.NewGuid().ToString();
                        //保存数据
                        //var proc = new proc_save_inchangeByAsn
                        //{
                        //    pID = strKeyID,
                        //    pInAsnCticketCode = txtOutAsnCticketCode.Text.Trim(),
                        //    pUserNo = WebUserInfo.GetCurrentUser().UserNo,
                        //    pMemo = txtCMEMO.Text.Trim()
                        //};
                        //proc.Execute();

                        var proc = new Proc_Save_InchangeByAsn
                        {
                            pID = strKeyID,
                            pInAsnCticketCode = txtOutAsnCticketCode.Text.Trim(),
                            pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo,
                            pReserveField1 = string.Empty,
                            pReserveField2 = string.Empty,
                            pRetCode = string.Empty,
                            pRetMsg = string.Empty,
                            pMemo = txtCMEMO.Text.Trim()
                        };
                        proc.Execute();

                        if (proc.ReturnValue == 0)
                        {
                            //DBUtil.Commit();
                            AlertAndBack("FrmChangeByInAsnEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.CommonB_SaveSuccess);
                        }
                        else
                        {
                            //DBUtil.Rollback();
                            Alert(proc.ErrorMessage + Resources.Lang.CommonB_SaveFailed + "!");
                        }
                    }
                    catch (Exception)
                    {
                        //DBUtil.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Alert(GetOperationName() + Resources.Lang.CommonB_Failed + "！[" + ex.Message + "]");//失败
            }
        }
    }

    //导出网格
    protected DataTable grdNavigatorInChange_d_GetExportToExcelSource()
    {
        //var listQuery = new RD_FrmInChangeCtl();
        //var dtSource = listQuery.GetListAsn_D(txtID.Text, txtCinvcode.Text.Trim(), 0, 0, "0");
        //return dtSource;
        return new DataTable();
    }

    //分页1
    protected void grdInChange_d_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorInChange_d.IsDbPager)
        //{
        //    grdNavigatorInChange_d.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    grdInChange_d.PageIndex = e.NewPageIndex;
        //}
    }

    //分页2
    protected void grdInChange_d_PageIndexChanged(object sender, EventArgs e)
    {
        //GridBind();
    }

    //查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //重新设置GridNavigator的RowCount
        //var listQuery = new RD_FrmInChangeCtl();
        //var dtRowCount = listQuery.GetListAsn_D(txtID.Text, txtCinvcode.Text.Trim(), 0, 0, "0");
        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    grdNavigatorInChange_d.RowCount = dtRowCount.Rows.Count;
        //}
        //else
        //{
        //    grdNavigatorInChange_d.RowCount = 0;
        //}

        this.CurrendIndex = 1;
        Bind("");
    }

    public IQueryable<INASNCHANGE_D> GetQueryList()
    {
        IGenericRepository<INASNCHANGE_D> conn = new GenericRepository<INASNCHANGE_D>(db);
        var caseList = from p in conn.Get()
                       where p.id == txtID.Text
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCinvcode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));
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
        }

        grdInChange_d.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdInChange_d.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }


    //绑定网格
    //public void GridBind()
    //{
    //    var listQuery = new RD_FrmInChangeCtl();
    //    var dtSource = listQuery.GetListAsn_D(txtID.Text, txtCinvcode.Text.Trim(), grdNavigatorInChange_d.CurrentPageIndex,
    //                                       grdInChange_d.PageSize, "1");
    //    grdInChange_d.DataSource = dtSource;
    //    grdInChange_d.DataBind();
    //}

    //完成
    protected void btnOK_Click(object sender, EventArgs e)
    {
        //DBUtil.BeginTrans();
        try
        {
            //保存数据
            //var proc = new proc_deal_inasnchangeByAsn { pID = KeyID, pUserNo = WebUserInfo.GetCurrentUser().UserNo };
            var proc = new Proc_Deal_InasnChangeByAsn 
            {
                pID = KeyID, 
                pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo ,
                pReserveField1 = string.Empty,
                pReserveField2 = string.Empty,
                pRetCode = string.Empty,
                pRetMsg = string.Empty
            };
            proc.Execute();
            if (proc.ReturnValue == 0)
            {
                //DBUtil.Commit();
                AlertAndBack("FrmChangeByInAsnEdit.aspx?" + BuildQueryString(SYSOperation.Modify, KeyID), Resources.Lang.CommonB_ProcessSucess + "");//处理成功
            }
            else
            {
                //DBUtil.Rollback();
                Alert(proc.ErrorMessage + Resources.Lang.CommonB_ProcessFailed + " !");//处理失败
            }
        }
        catch (Exception ex)
        {
            //DBUtil.Rollback();
            Alert(GetOperationName() + Resources.Lang.CommonB_Failed + "！[" + ex.Message + "]");//失败
        }

    }

    //删除明细
    protected void btnDel_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        //DBUtil.BeginTrans();
        try
        {
            for (int i = 0; i < grdInChange_d.Rows.Count; i++)
            {
                var chkSelect = (CheckBox)grdInChange_d.Rows[i].Cells[0].FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    //未处理的才允许删除
                    if (grdInChange_d.Rows[i].Cells[grdInChange_d.Rows[i].Cells.Count - 3].Text.Trim().Equals("未处理"))
                    {
                        //只有新增的单据才允许删除，删除完毕同时调整控制表状态
                        //var procPHL = new proc_del_inchangeByAsn_detial
                        //{
                        //    pID = this.grdInChange_d.DataKeys[i].Values[0].ToString(),
                        //    pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo
                        //};
                        //procPHL.Execute();

                        var procPHL = new Proc_Del_InChangeByAsn_Detial
                        {
                            pID = this.grdInChange_d.DataKeys[i].Values[0].ToString(),
                            pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo,
                            pReserveField1 = string.Empty,
                            pReserveField2 = string.Empty,
                            pRetCode = string.Empty,
                            pRetMsg = string.Empty
                        };
                        procPHL.Execute();
                        
                    }
                    else
                    {
                        msg = Resources.Lang.FrmChangeByInAsnEdit_MSG8 + ".";//只有状态为[未處理]的单据明细才能删除
                        break;
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.CommonB_RemoveSuccess + "！";//;删除成功
            }
            //DBUtil.Commit();
            //GridBind();
            this.CurrendIndex = 1;
            Bind("");
        }
        catch (Exception ex)
        {
            msg +=Resources.Lang.CommonB_RemoveFailed +  "!" + ex.Message;//删除失败
            //DBUtil.Rollback();
        }
        Alert(msg);
    }

    //网格显示
    protected void grdInChange_d_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //状态转换
            switch (e.Row.Cells[e.Row.Cells.Count - 3].Text.Trim())
            {
                case "0":
                    e.Row.Cells[e.Row.Cells.Count - 3].Text = Resources.Lang.CommonB_CSTATUS_undisposed;//+ @"未处理";
                    break;
                case "1":
                    e.Row.Cells[e.Row.Cells.Count - 3].Text = Resources.Lang.CommonB_CSTATUS_COMPLETE;// + @"已完成";
                    break;
            }

            //修改数量时提交
            var txtNowNum = e.Row.FindControl("txtNowNum") as TextBox;
            txtNowNum.Attributes["onBlur"] = "submitData('InChange','Qty','" + grdInChange_d.DataKeys[e.Row.RowIndex].Values[0] + "',this.value,'','',this);";
            //出库通知单变更单审核后，不可以进行编辑
            if (ddlCSTATUS.SelectedValue != "0")
            {
                txtNowNum.Enabled = false;
            }
        }
    }
    //打印功能
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string printid = this.txtID.Text.Trim();
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmChangeByInAsnEdit_Print.aspx", SYSOperation.New, printid) + "','" + Resources.Lang.FrmChangeByInAsnEdit_MSG9 + "','BAR_REPACK',840,600);");//打印入库通知变更单

        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }
}