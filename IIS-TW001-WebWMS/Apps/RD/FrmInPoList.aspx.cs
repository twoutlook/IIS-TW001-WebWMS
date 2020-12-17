using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq;
using System.Activities.Statements;

public partial class RD_FrmInPoList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitPage();
            //this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            //this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grd_PO.DataKeyNames = new string[] { "ID" };

        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmInPoEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmInPoList_MSG3+ "','INPO');return false;";
        //PO类型
        Help.DropDownListDataBind(GetParametersByFlagType("IN_PO_TYPE"), dpd_POType, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");//全部
        //状态
        Help.DropDownListDataBind(GetParametersByFlagType("IN_PO"), txtCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
     //MonthOrWeek
        Help.RadioButtonDataBind(GetParametersByFlagType("MonthOrWeek"), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");
    
    
    }

    #endregion
    

    private string GetKeyIDS(int rowIndex)
    {
        return this.grd_PO.DataKeys[rowIndex].Values[0].ToString();
    }


    #region 数据查询
    public IQueryable<V_QueryINPO> GetQueryList(DBContext modContext)
    {
        var queryList = from p in modContext.V_QueryINPO
                       select p;
        if (queryList != null)
        {
            if (!string.IsNullOrEmpty(txtPO.Text.Trim()))
            {
                queryList = queryList.Where(x => x.pono.Contains(txtPO.Text.Trim())); //支持模糊查询
            }
            if (!string.IsNullOrEmpty(dpd_POType.SelectedValue)) {
                queryList = queryList.Where(x => x.potype == dpd_POType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtCSTATUS.SelectedValue))
            {
                decimal selectStatus = Convert.ToDecimal(txtCSTATUS.SelectedValue);
                queryList = queryList.Where(x => x.status == selectStatus);
            }
            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim().ToUpper()))
            {
                queryList = queryList.Where(x => modContext.INPO_D.Any(p => p.cinvcode == txtCinvcode.Text.Trim().ToUpper() && p.id == x.id));
            }
            if (!string.IsNullOrEmpty(txtPO_DateFrom.Text.Trim()))
            {
                DateTime podateFrom = Convert.ToDateTime(txtPO_DateFrom.Text.Trim());
                queryList = queryList.Where(x => x.podate >= podateFrom);
            }
            if (!string.IsNullOrEmpty(txtPO_DateTo.Text.Trim()))
            {
                DateTime podateTo = Convert.ToDateTime(txtPO_DateTo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.podate < podateTo);
            }
            if (!string.IsNullOrEmpty(txtVendorCode.Text.Trim()))
            {
                queryList = queryList.Where(x => x.vendorid.Contains(txtVendorCode.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtVendorName.Text.Trim()))
            {
                queryList = queryList.Where(x => x.vendorname.Contains(txtVendorName.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCCREATEOWNERCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.createowner.Contains(txtCCREATEOWNERCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
            {
                DateTime createtimeFrom = Convert.ToDateTime(txtDCREATETIMEFrom.Text.Trim());
                queryList = queryList.Where(x => x.createtime >= createtimeFrom);
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMETo.Text.Trim()))
            {
                DateTime createtimeTo = Convert.ToDateTime(txtDCREATETIMETo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.createtime < createtimeTo);
            }
        }
        return queryList;
    }

    public void GridDataBind()
    {
        using (var modContext = this.context)
        {
            var queryList = GetQueryList(modContext);

            queryList = queryList.OrderByDescending(x => x.createtime);

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;
            List<V_QueryINPO> data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            var IN_PO = GetParametersByFlagType("IN_PO");
            var IN_PO_TYPE = GetParametersByFlagType("IN_PO_TYPE"); 
            var source = from p in data
                         join sp in IN_PO.DefaultIfEmpty() on p.status.ToString() equals sp.FLAG_ID
                         join pa in IN_PO_TYPE.DefaultIfEmpty() on p.potype.ToString() equals pa.FLAG_ID                      
                         select new
                         {
                             p.id,
                             p.pono,
                             potype = pa.FLAG_NAME,
                             p.podate,
                             p.vendorid,
                             p.vendorname,
                             status = sp.FLAG_NAME
                         };

            grd_PO.DataSource = source.ToList();         
            grd_PO.DataBind();
        }
    }

    private void Search(int iPage)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        GridDataBind();
    }

    #endregion

    #region 各种事件
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
        Search(0);
    }

    protected void grd_PO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            //"采购单"
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmInPoEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmInPoList_MSG4, "INPO");
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
                    for (int i = 0; i < this.grd_PO.Rows.Count; i++)
                    {
                        if (this.grd_PO.Rows[i].Cells[0].Controls[1] is CheckBox)
                        {
                            CheckBox chkSelect = (CheckBox)this.grd_PO.Rows[i].Cells[0].Controls[1];
                            if (chkSelect.Checked)
                            {
                                string key = this.grd_PO.DataKeys[i].Values[0].ToString();
                                INPO inpo = modContext.INPO.Single(x => x.id == key);
                                if (inpo != null)
                                {
                                    if (inpo.status.ToString() != "0")
                                    {
                                        msg = Resources.Lang.FrmInPoList_MSG5;// "只有未处理的可以删除";
                                        break;
                                    }
                                    else
                                    {
                                        modContext.INPO.Attach(inpo);
                                        modContext.INPO.Remove(inpo);
                                        modContext.SaveChanges();
                                    }
                                }
                                else
                                {
                                    //数据异常,删除失败
                                    msg = Resources.Lang.FrmInPoList_MSG6+"!";
                                    break;
                                }
                            }
                        }
                    }
                    if (msg.Length == 0)
                    {
                        //删除成功
                        msg = Resources.Lang.CommonB_RemoveSuccess + "!";
                    }
                    dbContextTransaction.Commit();
                    Search(0);
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    //删除失败
                    msg += Resources.Lang.CommonB_RemoveFailed + "![" + ex.Message + "]";
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
                    for (int i = 0; i < this.grd_PO.Rows.Count; i++)
                    {
                        if (this.grd_PO.Rows[i].Cells[0].Controls[1] is CheckBox)
                        {
                            CheckBox chkSelect = (CheckBox)this.grd_PO.Rows[i].Cells[0].Controls[1];
                            if (chkSelect.Checked)
                            {
                                string key = this.grd_PO.DataKeys[i].Values[0].ToString();
                                INPO modInpo = modContext.INPO.Single(x => x.id == key);
                                if (modInpo != null)
                                {
                                    if (modInpo.status != 0)
                                    {
                                        //只有未处理的可以撤消
                                        msg = Resources.Lang.FrmInPoList_MSG8 + "!";
                                        break;
                                    }
                                    else
                                    {
                                        modInpo.status = 3;//已撤消
                                        modInpo.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                                        modInpo.lastupdatetime = DateTime.Now;
                                        modContext.INPO.Attach(modInpo);
                                        modContext.Entry(modInpo).State = System.Data.Entity.EntityState.Modified;
                                        modContext.SaveChanges();
                                    }
                                }
                                else
                                {
                                    //数据异常,撤消失败
                                    msg = Resources.Lang.FrmInPoList_MSG7 + "!";
                                    break;
                                }
                            }
                        }
                    }
                    if (msg.Length == 0)
                    {
                        //撤消成功
                        msg = Resources.Lang.CommonB_RevokeSuccess + "!";
                    }
                    dbContextTransaction.Commit();
                    CurrendIndex = 1;
                    AspNetPager1.CurrentPageIndex = 1;
                    GridDataBind();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    //撤消失败
                    msg += Resources.Lang.CommonB_RevokeFailed + "![" + ex.Message + "]";
                }
            }
            this.Alert(msg);
        }
    }
    #endregion
}

