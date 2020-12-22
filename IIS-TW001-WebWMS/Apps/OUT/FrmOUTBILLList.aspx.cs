using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Business.OUT;
using DreamTek.ASRS.Business.SP.ProcedureModel;

/// <summary>
/// 出库单列表页
/// </summary>
public partial class OUT_FrmOUTBILLList : WMSBasePage
{

    #region 页面属性

    private Dictionary<string, string> dict = new Dictionary<string, string>();

    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }

    #endregion

    #region 页面加载

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.InitPage();
            //this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            //this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        this.btnUpdateSTOCK.Attributes["onclick"] = this.GetPostBackEventReference(this.btnUpdateSTOCK) + ";this.disabled=true;";
    }

    /// <summary>
    /// 页面初始化
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdOUTBILL.DataKeyNames = new string[] { "ID", "CTICKETCODE" };

        Help.DropDownListDataBind(GetOutType(true), this.drIType, Resources.Lang.WMS_Common_DrpAll, "FUNCNAME", "EXTEND1", "");
        //国际化【begin】
        //Help.DropDownListDataBind(new SysParameterList().GetSys_ParameterByFLAG_TYPE("OUTBILL"), dplCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "OUTBILL", false, -1, -1), dplCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "WorkType", false, -1, -1), this.drpWorkType, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        //补单 or非补单
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "OperationType", false, -1, -1), this.drpOperationType, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        //国际化【END】

        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmOUTBILLEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmOUTBILLList_NewOutBill + "','OUTBILL');return false;";
       
        Help.RadioButtonDataBind(SysParameterList.GetList("", "", "MonthOrWeek", false, -1, -1), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");


        if (!string.IsNullOrEmpty(Request.QueryString["Cstatus"]))
        {
            this.dplCSTATUS.SelectedValue = this.Request.QueryString["Cstatus"].ToString();
        }
    }

    #endregion

    #region 页面事件

    /// <summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;//默认第一页
            AspNetPager1.CurrentPageIndex = 1;
        }
        IsFirstPage = true;

        //Note by Qamar 2020-12-12
        //如果在.aspx設定Visible為false, 會取不到數值, 因此寫在.aspx.cs這裡
        try { grdOUTBILL.Columns[19].Visible = true; }
        catch { }
        this.GridBind();
        try { grdOUTBILL.Columns[19].Visible = false; }
        catch { }
    }

    /// <summary>
    /// 删除出库单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        string ReturnValue = string.Empty;
        string errText = string.Empty;
        try
        {
            for (int i = 0; i < this.grdOUTBILL.Rows.Count; i++)
            {
                if (this.grdOUTBILL.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdOUTBILL.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string bill_ID = this.grdOUTBILL.DataKeys[i].Values[0].ToString();
                        var modOutBill = context.OUTBILL.Where(x => x.id == bill_ID).FirstOrDefault();
                        if (modOutBill != null)
                        {
                            //根据当前单据号ID而确认当前单据是否可以进行操作  
                            OUTTYPE OUTBILL_type = GetOUTTYPEByID(bill_ID, "OUTBILL");
                            if (OUTBILL_type != null && !string.IsNullOrEmpty(OUTBILL_type.Is_Query.ToString()))
                            {
                                if (OUTBILL_type.Is_Query.ToString() == "1")
                                {
                                    msg = Resources.Lang.FrmOUTBILLList_BuNengChangChu;
                                    break;
                                }
                            }
                            //根据当前单据号ID而确认当前单据是否可以进行操作 
                            if (modOutBill.worktype == "0" && modOutBill.operationtype == 0)//平库，PDA产生的入库单，单独的删除逻辑
                            {
                                if (modOutBill.cstatus == "0")
                                { //未处理
                                    Proc_OutBill_Del proc = new Proc_OutBill_Del();
                                    proc.P_OutBill_Id = modOutBill.id;
                                    proc.P_UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                                    proc.Execute();
                                    errText = proc.P_ReturnMsg;
                                    if (proc.P_ReturnValue != 0)
                                    {
                                        msg += proc.P_ReturnMsg;
                                        break;
                                    }
                                }
                                else
                                {
                                    msg = Resources.Lang.FrmOUTBILLList_WeiChuLi;
                                    break;
                                }
                            }
                            else
                            {
                                if (modOutBill.cstatus == "0")
                                { //未处理
                                    #region 调用存储过程
                                    List<string> SparaList = new List<string>();
                                    SparaList.Add("@P_Bill_ID:" + bill_ID);
                                    SparaList.Add("@P_Bill_IDS:" + "");
                                    SparaList.Add("@P_BZ:" + "2");
                                    SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                                    SparaList.Add("@P_return_Value:" + "");
                                    SparaList.Add("@errText:" + "");
                                    string[] Result = DBHelp.ExecuteProc("Proc_Delete_WebBill_D", SparaList);
                                    if (Result.Length == 1)//调用存储过程有错误
                                    {
                                        msg += Result[0];
                                    }
                                    else if (Result[0] == "0")
                                    {

                                    }
                                    else
                                    {
                                        msg += Result[1];
                                    }
                                    #endregion
                                }
                                else
                                {
                                    msg = Resources.Lang.FrmOUTBILLList_WeiChuLi;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            msg = Resources.Lang.FrmOUTASNList_Tips_ShuJuYiChang;
                            break;
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;
            }
            this.GridBind();
        }
        catch
        {
            msg += Resources.Lang.WMS_Common_Msg_DeleteFailed;
        }
        this.Alert(msg);
    }

    /// <summary>
    /// 批量扣账
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdateSTOCK_Click(object sender, EventArgs e)
    {
        btnUpdateSTOCK.Enabled = false;     

        GetSelectedIds();
        if (SelectIds != null && SelectIds.Count > 0)
        {
            Alert(OutBillQuery.BatchOutBillTOStock_Currnt(SelectIds, WmsWebUserInfo.GetCurrentUser().UserNo));
            this.btnSearch_Click(sender, e);
        }
        else
        {
            Alert(Resources.Lang.FrmOUTBILLList_XuanZeChuKu);
        }
        btnUpdateSTOCK.Enabled = true;//Roger 2013-4-24 18:33:44
        btnUpdateSTOCK.Style.Remove("disabled");
    }

    /// <summary>
    /// 测试
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdateSTOCK_Test_Click(object sender, EventArgs e)
    {
        GetSelectedIds();
        if (SelectIds != null && SelectIds.Count > 0)
        {
            Alert(OutBillQuery.BatchOutBillTOStock_Currnt_Test(SelectIds, WmsWebUserInfo.GetCurrentUser().UserNo));
            this.btnSearch_Click(sender, e);
            SelectIds.Clear();
        }
        else
        {
            Alert(Resources.Lang.FrmOUTBILLList_XuanZeChuKu);
        }
    }

    /// <summary>
    /// 分页控件翻页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    /// <summary>
    /// 列表行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdOUTBILL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmOUTBILLEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmOUTASNList_OutBill, "OUTBILL");

            HyperLink linkModify_Error = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify_Error.NavigateUrl = "#";

            if (new SystemLogs().validateIsExistErrorMsg(strKeyID))
            {
                linkModify_Error.Style.Add("color", "Red");
                this.OpenFloatWin(linkModify_Error, BuildRequestPageURL("/Apps/SystemLogs/FrmLogSystemList.aspx", SYSOperation.Modify, strKeyID + "&TableName=OUTBILL"), Resources.Lang.FrmOUTASNList_Tips_YiChangXingXi, "ErrorMsg", 1000, 600);
            }
            else
            {
                linkModify_Error.Enabled = false;
            }

            LinkButton link = e.Row.FindControl("lbtnOutBillBack") as LinkButton;

            link.Enabled = false;

            string Outbill_id = this.grdOUTBILL.DataKeys[e.Row.RowIndex][0].ToString();

            OUTBILL entity = context.OUTBILL.Where(x => x.id == Outbill_id).FirstOrDefault();
            TableCell cellTaskMode = e.Row.Cells[e.Row.Cells.Count - 5];
            if (entity.cstatus == "2" && cellTaskMode.Text == "1")
            {
                string Istrue = OUTBILL_XDRule.CheckISBack(Outbill_id);
                if (Istrue == "1")
                {
                    link.Enabled = true;
                }
            }


            if (cellTaskMode.Text == "1")
            {
                cellTaskMode.Text = Resources.Lang.WMS_Common_WorkType_LiKu;
            }
            else if (cellTaskMode.Text == "0")
            {
                cellTaskMode.Text = Resources.Lang.WMS_Common_WorkType_PingKu;
            }

            CheckBox cbo = e.Row.FindControl("chkSelect") as CheckBox;

            //获取ID
            string id = this.grdOUTBILL.DataKeys[e.Row.RowIndex][0].ToString();

            //判断是否已在SelectIds集合中
            if (SelectIds.ContainsKey(id))
            {
                //如果是控件处于选中状态
                cbo.Checked = true;
            }
        }

    }

    protected void grdOUTBILL_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }

    /// <summary>
    /// 返库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnOutBillBack_Click(object sender, EventArgs e)
    {

        string msg = string.Empty;
        try
        {
            string OutBillID = (sender as LinkButton).CommandArgument;

            IGenericRepository<OUTBILL_D> con = new GenericRepository<OUTBILL_D>(context);
            var caseList = from p in con.Get()
                           where p.id == OutBillID
                           select p;

            OUTBILL_D entity = caseList.ToList().FirstOrDefault();

            IGenericRepository<OUTBILL_D_SN> con_sn = new GenericRepository<OUTBILL_D_SN>(context);

            var casesnList = from p in con_sn.Get()
                             where p.outbill_d_ids == entity.ids
                             select p;
            OUTBILL_D_SN snentity = casesnList.ToList().FirstOrDefault();

            string Crane = entity.wire.ToString();
            string Site = entity.pallet_code;
            string Cposition = entity.cpositioncode;
            string PalletCode = snentity.palletcode;

            List<string> SparaList = new List<string>();
            SparaList.Add("@P_Crane:" + Crane);
            SparaList.Add("@P_Site:" + Site);
            SparaList.Add("@P_PalletCode:" + PalletCode);
            SparaList.Add("@P_Cposition:" + Cposition);
            SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
            SparaList.Add("@P_ReturnValue:" + "");
            SparaList.Add("@P_INFOTEXT:" + "");
            string[] Result = DBHelp.ExecuteProc("Proc_InBillBackLibrary", SparaList);
            if (Result.Length == 1)//调用存储过程有错误
            {
                msg += Result[0];
            }
            else if (Result[0] == "0")
            {

            }
            else
            {
                msg += Result[1];
            }

            if (msg.Length == 0)
            {
                msg = Resources.Lang.FrmOUTBILLList_FanKuChengGong;// "返库成功!";
            }
        }
        catch
        {
            msg += Resources.Lang.FrmOUTBILLList_FanKuShiBai;// "返库失败!";
        }
        this.Alert(msg);
        this.btnSearch_Click(null, null);
    }

    #endregion

    #region 页面方法

    private string GetKeyIDS(int rowIndex)
    {
        return this.grdOUTBILL.DataKeys[rowIndex].Values[0].ToString();
    }

    /// <summary>
    /// 列表数据绑定
    /// </summary>
    public void GridBind()
    {
        string userNo = WmsWebUserInfo.GetCurrentUser().UserNo;
        List<string> userCaseType = GetUserCaseTypeNew(userNo);

        using (var modContext = this.context)
        {
            IGenericRepository<V_OUTBILL> entity = new GenericRepository<V_OUTBILL>(context);
            var queryList = from p in modContext.V_OUTBILL
                            orderby p.dcreatetime descending
                            where 1 == 1
                            select p;


            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.OUTBILL_D.Any(p => p.id == x.id && p.cinvcode.Contains(txtCinvcode.Text.Trim())));
            }

            // NOTE by Mark, 10/22, 捲捲發現查詢結果有問題
            // 影片我會搜尋料號95, rank2, 是因為我想找1-138#DOJ95-2的出庫單.
            // 結果我找出了不具備1-138#DOJ95-2的出庫單啊, 我找出的單是1-138#DOJ95-1和1-122#EBJ87MA-2混著的單.
            // 目前顯示的結果像是 PART OR RANK
            // 精確地講是回了,任何出庫單的明細具有任意的 (PART,RANK)=(1-138#DOJ95,2), 但沒有限制在同一個明細上.
            // PART 和 RANK 應該要一起比對,
            // 在其它頁面, 我都儘可能使用[完整料號]的概念.
            // 在這裡, 試著用 PART + RANK 的組合
            // PART RANK
            //   -   -      NOTHING
            //   -   V      只比 RANK
            //   V   -      只比 PART
            //   V   V      組合比較, 上面三種情況,其實已經滿足, 按條件多這部分即可。

            if (!string.IsNullOrEmpty(txtRank_Final.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.OUTBILL_D.Any(p => p.id == x.id && p.cinvcode.Contains("-")&&p.cinvcode.EndsWith("-"+txtRank_Final.Text.Trim())));
            }

            //  V   V  <<< 組合比較, 上面三種情況,其實已經滿足, 按條件多這部分即可。
            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()) && !string.IsNullOrEmpty(txtRank_Final.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.OUTBILL_D.Any(p => p.id == x.id && p.cinvcode.Contains(txtCinvcode.Text.Trim()) && p.cinvcode.EndsWith("-" + txtRank_Final.Text.Trim())));
            }




            if (!string.IsNullOrEmpty(txtCTICKETCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtCTICKETCODE.Text));
            }
            if (dplCSTATUS.SelectedValue != "")
            {
                queryList = queryList.Where(x => x.cstatus.ToString().Equals(dplCSTATUS.SelectedValue));
            }
            //箱号/栈板号
            if (!string.IsNullOrEmpty(TxtSNCode.Text.Trim()))
            {
                queryList = queryList.Where(x => x.sn_code.Contains(TxtSNCode.Text.Trim()) || modContext.OUTBILL_D_SN.Any(p => p.outbill_id == x.id && p.sn_code.Contains(TxtSNCode.Text.Trim())));
            }
            if (!string.IsNullOrEmpty(txtCCREATEOWNERCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.ccreateownercode) && x.ccreateownercode.Contains(txtCCREATEOWNERCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
            {
                DateTime createTimeFrom = Convert.ToDateTime(txtDCREATETIMEFrom.Text.Trim());
                queryList = queryList.Where(x => x.dcreatetime != null && x.dcreatetime >= createTimeFrom);
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMETo.Text.Trim()))
            {
                DateTime createTimeTo = Convert.ToDateTime(txtDCREATETIMETo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.dcreatetime != null && x.dcreatetime < createTimeTo);
            }
            if (!string.IsNullOrEmpty(txtCAUDITPERSON.Text.Trim()))
            {
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.cauditperson) && x.cauditperson.Contains(txtCAUDITPERSON.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtDINDATEFrom.Text.Trim()))
            {
                DateTime dindateFrom = Convert.ToDateTime(txtDINDATEFrom.Text.Trim());
                queryList = queryList.Where(x => x.dindate != null && x.dindate >= dindateFrom);
            }
            if (!string.IsNullOrEmpty(txtDINDATETo.Text.Trim()))
            {
                DateTime dindateTo = Convert.ToDateTime(txtDINDATETo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.dindate != null && x.dindate < dindateTo);
            }
            if (!string.IsNullOrEmpty(txtDAUDITTIMEFrom.Text.Trim()))
            {
                DateTime dauditdateFrom = Convert.ToDateTime(txtDAUDITTIMEFrom.Text.Trim());
                queryList = queryList.Where(x => x.dindate != null && x.daudittime >= dauditdateFrom);
            }
            if (!string.IsNullOrEmpty(txtDAUDITTIMETo.Text.Trim()))
            {
                DateTime dauditdateTo = Convert.ToDateTime(txtDAUDITTIMETo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.dindate != null && x.dindate < dauditdateTo);
            }
            if (!string.IsNullOrEmpty(txtCCLIENTCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.cclientcode) && x.cclientcode.Contains(txtCCLIENTCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCERPCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtCERPCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCOUTASNID.Text.Trim()))
            {
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.bccode) && x.bccode.Contains(txtCOUTASNID.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCCLIENT.Text.Trim()))
            {
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.cclient) && x.cclient.Contains(txtCCLIENT.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtso.Text.Trim()))
            {
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.cso) && x.cso.Contains(txtso.Text));
            }
            if (drIType.SelectedValue != "")
            {
                queryList = queryList.Where(x => x.otype.ToString().Equals(drIType.SelectedValue));
            }
            else
            {
                //过滤全部的类型
                queryList = queryList.Where(x => userCaseType.Contains(x.otype.ToString()));
            }

            /*else
            {
                queryList = queryList.Where(x => x.otype.ToString()!="2");
            }*/
            if (!string.IsNullOrEmpty(drpWorkType.SelectedValue))
            {
                queryList = queryList.Where(x => x.worktype.Equals(drpWorkType.SelectedValue));
            }
            if (!string.IsNullOrEmpty(drpOperationType.SelectedValue))
            {
                int operationType = Convert.ToInt32(drpOperationType.SelectedValue);
                queryList = queryList.Where(x => x.operationtype == operationType);
            }
            if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.OUTBILL_D.Any(p =>p.id == x.id && modContext.BASE_PART.Any(y=>y.cspecifications.Contains(txtcspec.Text.Trim()) && p.cinvcode == y.cpartnumber)));
            }

            if (queryList != null)
            {
                AspNetPager1.RecordCount = queryList.Count();
                AspNetPager1.PageSize = this.PageSize;
            }

            var data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
            flagList.Add(new Tuple<string, string>("otype", "OUTTYPE"));
            flagList.Add(new Tuple<string, string>("worktype", "WorkType"));
            flagList.Add(new Tuple<string, string>("cstatus", "OUTBILL"));


            var srcdata = GetGridSourceDataByList(data, flagList);


            grdOUTBILL.DataSource = srcdata;//GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            grdOUTBILL.DataBind();
        }
    }

    public bool CheckData()
    {
        if (this.txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmOUTBILLList_Tips_RiQiWuXiao);//制单日期项不是有效的日期！
                this.SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        if (this.txtDCREATETIMETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILLList_Tips_NeedDao);//到项不允许空！
            this.SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (this.txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmOUTBILLList_Tips_DaoWuXiao);//到项不是有效的日期！
                this.SetFocus(txtDCREATETIMETo);
                return false;
            }
        }
        if (this.txtDAUDITTIMEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDAUDITTIMEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmOUTBILLList_Tips_ShenHeRiWuXiao);//审核日期项不是有效的日期！
                this.SetFocus(txtDAUDITTIMEFrom);
                return false;
            }
        }
        if (this.txtDAUDITTIMETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILLList_Tips_NeedShenHeDao);//到项不允许空！
            this.SetFocus(txtDAUDITTIMETo);
            return false;
        }
        if (this.txtDAUDITTIMETo.Text.Trim().Length > 0)
        {
            if (this.txtDAUDITTIMETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmOUTBILLList_Tips_ShenHeDaoWuXiao);//到项不是有效的日期！
                this.SetFocus(txtDAUDITTIMETo);
                return false;
            }
        }
        if (this.txtDINDATEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDINDATEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmOUTBILLList_Tips_ChuKuRiWuXiao);//出库日期项不是有效的日期！
                this.SetFocus(txtDINDATEFrom);
                return false;
            }
        }
        if (this.txtDINDATETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTBILLList_Tips_NeedChuKuDao);//到项不允许空！
            this.SetFocus(txtDINDATETo);
            return false;
        }
        if (this.txtDINDATETo.Text.Trim().Length > 0)
        {
            if (this.txtDINDATETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmOUTBILLList_Tips_ChuKuDaoWuXiao);//到项不是有效的日期！
                this.SetFocus(txtDINDATETo);
                return false;
            }
        }
        return true;

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

        foreach (GridViewRow item in this.grdOUTBILL.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string id = this.grdOUTBILL.DataKeys[item.RowIndex][0].ToString();

                //控件选中且集合中不存在添加
                if (cbo.Enabled && cbo.Checked && !SelectIds.ContainsKey(id))
                {
                    SelectIds.Add(id, this.grdOUTBILL.DataKeys[item.RowIndex][1].ToString());
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(id))
                {
                    SelectIds.Remove(id);
                }
            }
        }
    }

    #endregion

}

