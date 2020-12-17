using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_OUT_FrmOutOrderList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitPage();
            this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grd_Order.DataKeyNames = new string[] { "ID" };

        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmOutOrderEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmOutOrderList_NewOrder + "','OUTORDER');return false;";//新建订单
        //PO类型
        Help.DropDownListDataBind(GetParametersByFlagType("OUTORDER_TYPE"), drpOrderType, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//全部
        //状态
        Help.DropDownListDataBind(GetParametersByFlagType("OUTORDER"), drpStatus, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");

        Help.RadioButtonDataBind(SysParameterList.GetList("", "", "MonthOrWeek", false, -1, -1), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");
    }

    public void GridDataBind()
    {
        using (var modContext = this.context)
        {
            var queryList = from p in modContext.OUTORDER
                            select p;
            if (!string.IsNullOrEmpty(txtOrderNo.Text.Trim()))
            {
                queryList = queryList.Where(x => x.OrderNo.Contains(txtOrderNo.Text.Trim()) || x.CustomOrderNo.Contains(txtOrderNo.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(drpOrderType.SelectedValue))
            {
                queryList = queryList.Where(x => x.OrderType.ToString() == drpOrderType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(drpStatus.SelectedValue))
            {
                int selectStatus = Convert.ToInt32(drpStatus.SelectedValue);
                queryList = queryList.Where(x => x.Status == selectStatus);
            }
            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim().ToUpper()))
            {
                queryList = queryList.Where(x => modContext.OUTORDER_D.Any(p => p.CinvCode == txtCinvcode.Text.Trim().ToUpper() && p.id == x.id));
            }
            if (!string.IsNullOrEmpty(txtOrder_DateFrom.Text.Trim()))
            {
                DateTime podateFrom = Convert.ToDateTime(txtOrder_DateFrom.Text.Trim());
                queryList = queryList.Where(x => x.OrderDate >= podateFrom);
            }
            if (!string.IsNullOrEmpty(txtOrder_DateTo.Text.Trim()))
            {
                DateTime podateTo = Convert.ToDateTime(txtOrder_DateTo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.OrderDate < podateTo);
            }
            if (!string.IsNullOrEmpty(txtCustomId.Text.Trim()))
            {
                queryList = queryList.Where(x => x.CustomId.Contains(txtCustomId.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCustomName.Text.Trim()))
            {
                queryList = queryList.Where(x => x.CustomName.Contains(txtCustomName.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCCREATEOWNERCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.CreateOwner.Contains(txtCCREATEOWNERCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
            {
                DateTime createtimeFrom = Convert.ToDateTime(txtDCREATETIMEFrom.Text.Trim());
                queryList = queryList.Where(x => x.CreateTime >= createtimeFrom);
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMETo.Text.Trim()))
            {
                DateTime createtimeTo = Convert.ToDateTime(txtDCREATETIMETo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.CreateTime < createtimeTo);
            }

            queryList = queryList.OrderByDescending(x => x.CreateTime);

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;

            var data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();

            List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
            flagList.Add(new Tuple<string, string>("OrderType", "OUTORDER_TYPE"));
            flagList.Add(new Tuple<string, string>("Status", "OUTORDER"));

            var srcdata = GetGridSourceDataByList(data, flagList);

            grd_Order.DataSource = srcdata;
            grd_Order.DataBind();
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridDataBind();
    }

    /// 查询按钮
    /// <summary>
    /// 查询按钮
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
        GridDataBind();
    }

    private string GetKeyIDS(int rowIndex)
    {
        return this.grd_Order.DataKeys[rowIndex].Values[0].ToString();
    }

    protected void grd_Order_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmOutOrderEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmOutOrderList_Order, "OUTORDER");//订单
        }
    }

    /// <summary>
    /// 删除按钮
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
                    for (int i = 0; i < this.grd_Order.Rows.Count; i++)
                    {
                        if (this.grd_Order.Rows[i].Cells[0].Controls[1] is CheckBox)
                        {
                            CheckBox chkSelect = (CheckBox)this.grd_Order.Rows[i].Cells[0].Controls[1];
                            if (chkSelect.Checked)
                            {
                                string key = this.grd_Order.DataKeys[i].Values[0].ToString();
                                OUTORDER modOutOrder = modContext.OUTORDER.Single(x => x.id == key);
                                if (modOutOrder != null)
                                {
                                    if (modOutOrder.Status.ToString() != "0")
                                    {
                                        msg = Resources.Lang.FrmOutOrderList_OnlyWeiChuLi;//只有未处理的可以删除
                                        break;
                                    }
                                    else
                                    {
                                        if (modOutOrder.OrderSource != 0)
                                        {
                                            modContext.OUTORDER.Attach(modOutOrder);
                                            modContext.OUTORDER.Remove(modOutOrder);
                                            modContext.SaveChanges();
                                        }
                                        else
                                        {
                                            msg = Resources.Lang.FrmOutOrderList_Order + ":" + modOutOrder.OrderNo + Resources.Lang.FrmOutOrderList_CannotDelete;//"订单:" + modOutOrder.OrderNo + "来自接口,不允许删除！";
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    msg = Resources.Lang.FrmOUTASNList_Tips_YiChang_DeleteFailed;//数据异常,删除失败!
                                    break;
                                }
                            }
                        }
                    }
                    if (msg.Length == 0)
                    {
                        msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;//删除成功!
                    }
                    dbContextTransaction.Commit();
                    CurrendIndex = 1;
                    AspNetPager1.CurrentPageIndex = 1;
                    GridDataBind();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    msg += Resources.Lang.WMS_Common_Msg_DeleteFailed + "[" + ex.Message + "]";//删除失败!
                }
            }
            this.Alert(msg);
        }
    }

    /// <summary>
    /// 撤消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnRevoke_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        using (var modContext = this.context)
        {
            using (var dbContextTransaction = modContext.Database.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < this.grd_Order.Rows.Count; i++)
                    {
                        if (this.grd_Order.Rows[i].Cells[0].Controls[1] is CheckBox)
                        {
                            CheckBox chkSelect = (CheckBox)this.grd_Order.Rows[i].Cells[0].Controls[1];
                            if (chkSelect.Checked)
                            {
                                string key = this.grd_Order.DataKeys[i].Values[0].ToString();
                                OUTORDER modOutOrder = modContext.OUTORDER.Single(x => x.id == key);
                                if (modOutOrder != null)
                                {
                                    if (modOutOrder.Status.ToString() != "0")
                                    {
                                        msg = Resources.Lang.FrmOutOrderList_CannotRevoke;//只有未处理的可以撤消
                                        break;
                                    }
                                    else
                                    {
                                        modOutOrder.Status = 3;//已撤消
                                        modOutOrder.LastUpdateOwner = WmsWebUserInfo.GetCurrentUser().UserNo;
                                        modOutOrder.LastUpdateTime = DateTime.Now;
                                        modContext.OUTORDER.Attach(modOutOrder);
                                        modContext.Entry(modOutOrder).State = System.Data.Entity.EntityState.Modified;
                                        modContext.SaveChanges();
                                    }
                                }
                                else
                                {
                                    msg = Resources.Lang.WMS_Common_Msg_RevokeException;//数据异常,撤消失败!
                                    break;
                                }
                            }
                        }
                    }
                    if (msg.Length == 0)
                    {
                        msg = Resources.Lang.WMS_Common_Msg_RevokeSuccess;//撤消成功!
                    }
                    dbContextTransaction.Commit();
                    CurrendIndex = 1;
                    AspNetPager1.CurrentPageIndex = 1;
                    GridDataBind();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    msg += Resources.Lang.WMS_Common_Msg_RevokeFailed + "[" + ex.Message + "]";//撤消失败!
                }
            }
            this.Alert(msg);
        }
    }

}