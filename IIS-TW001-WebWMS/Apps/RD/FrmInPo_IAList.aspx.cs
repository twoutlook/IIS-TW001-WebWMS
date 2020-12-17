using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq;
using System.Data.SqlClient;
/// <summary>
/// 描述: 预入库通知单
/// 作者: --CQ
/// 创建于: 2014-10-9 14:06:56
/// </summary>
public partial class RD_FrmInPo_IAList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    #region 页面初始化
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdINASN_IA.DataKeyNames = new string[] { "ID" };

        //状态
        Help.DropDownListDataBind(GetParametersByFlagType("IN_PO_IA"), dpdStatus, "全部", "FLAG_NAME", "FLAG_ID", "");
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmInPo_IAEdit.aspx", SYSOperation.New, "") + "','新建预入库通知单','INPO_IA');return false;";
    }

    #endregion

    #region 事件
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridDataBind();
    }
    /// <summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        this.GridDataBind();
    }

    protected void btnNew0_Click(object sender, EventArgs e)
    {
        //DBUtil.BeginTrans();
        //try
        //{
        //    int count = 0;
        //    for (int i = 0; i < this.grdINASN_IA.Rows.Count; i++)
        //    {
        //        if (this.grdINASN_IA.Rows[i].Cells[0].Controls[1] is CheckBox)
        //        {
        //            CheckBox chkSelect = (CheckBox)this.grdINASN_IA.Rows[i].Cells[0].Controls[1];
        //            if (chkSelect.Checked)
        //            {

        //                INASN_IAEntity entity = new INASN_IAEntity();
        //                //主键赋值
        //                entity.ID = this.grdINASN_IA.DataKeys[i].Values[0].ToString();
        //                BAR_FrmINASN_IAListQuery listquer = new BAR_FrmINASN_IAListQuery();
        //                if (entity.SelectByPKeys())
        //                {
        //                    if (entity.CSTATUS != "1")
        //                    {
        //                        throw new Exception("只有已完成的可以撤销");
        //                    }

        //                    if (listquer.HasDelete(entity.ID))
        //                    {
        //                        entity.CSTATUS = "0";
        //                        INASN_IARule.Update(entity);	//执行动作 


        //                        INASNRule.DeleteByProperty(new INASNEntity() { CSTATUS = "0", INASN_IA_ID = entity.ID });
        //                        count++;
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("当前单据入库通知单含有已操作的数据不能删除！");
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    DBUtil.Commit();
        //    if (count > 0)
        //        this.Alert("撤销成功！");
        //    else
        //        this.Alert("请选择行！");
        //    this.GridDataBind();
        //}
        //catch (Exception E)
        //{
        //    this.Alert("撤销失败！" + E.Message.ToJsString());
        //    DBUtil.Rollback();
        //}
    }

    /// <summary>
    /// 删除操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        int count = 0;
        using (var modContext = this.context)
        {
            using (var dbContextTransaction = modContext.Database.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < this.grdINASN_IA.Rows.Count; i++)
                    {
                        if (this.grdINASN_IA.Rows[i].Cells[0].Controls[1] is CheckBox)
                        {
                            CheckBox chkSelect = (CheckBox)this.grdINASN_IA.Rows[i].Cells[0].Controls[1];
                            if (chkSelect.Checked)
                            {
                                #region 执行删除操作
                                //主键赋值
                                string id = this.grdINASN_IA.DataKeys[i].Values[0].ToString();
                                INASN_IA ia = modContext.INASN_IA.Where(x => x.id == id).FirstOrDefault();
                                if (ia != null)
                                {
                                    if (ia.cstatus != "0") {
                                        msg = "预入库通知单状态不是[未处理]不能删除！";
                                        break;
                                    }

                                    #region 检查是否在交叉转运配置中（TODO：目前暂无该功能，预留）
                                    //int inCrossCount = context.IN_CROSSDOCK.Where(x => x.inasn_ia_id == id).Count();
                                    //if (inCrossCount > 0) {
                                    //    msg = "预入库通知单在交叉转运配置中,不能删除";
                                    //    break;
                                    //}
                                    #endregion

                                    List<INASN_IA_D> dList = modContext.INASN_IA_D.Where(x => x.id == id).ToList();
                                    if (dList != null && dList.Any())
                                    {
                                        string poId = modContext.INPO_D.Where(x=>db.INASN_IA_D.Any(d=>d.id == id && d.inpo_d_ids == x.ids)).FirstOrDefault().id;
                                        //删除预入库通知单详情
                                        modContext.Database.ExecuteSqlCommand(" delete from inasn_ia_d where id = @PIAID ", new SqlParameter("@PIAID", id));
                                        //删除预入库通知单
                                        modContext.INASN_IA.Attach(ia);
                                        modContext.INASN_IA.Remove(ia);
                                        modContext.SaveChanges();
                                        modContext.Database.ExecuteSqlCommand(@" update inpo_d set status = 0 where id = @vPO_ID 
                                                                              and ids not in (select iad.inpo_d_ids from inasn_ia_d iad where iad.id = @P_IA_ID) ", new SqlParameter("@vPO_ID", poId), new SqlParameter("P_IA_ID", id));
                                        if (!modContext.INPO_D.Where(x => x.status != 0).Any()) {
                                            modContext.Database.ExecuteSqlCommand(" update inpo  set status = 0 where id = @vPO_ID", new SqlParameter("@vPO_ID", poId));
                                        }
                                    }
                                    else
                                    {
                                        //无明细时直接删除预入库通知单
                                        modContext.INASN_IA.Attach(ia);
                                        modContext.INASN_IA.Remove(ia);
                                        modContext.SaveChanges();
                                    }
                                    
                                }
                                else {
                                    msg = "数据异常，无法删除！";
                                    break;
                                }
                                count++;
                                #endregion
                            }
                        }
                    }
                    if(string.IsNullOrEmpty(msg)){
                        if(count>0){
                            dbContextTransaction.Commit();
                            this.Alert("删除成功！");
                            this.GridDataBind();
                        }else{
                            this.Alert("请选择要删除的数据！");
                        }             
                    }else{
                        dbContextTransaction.Rollback();
                        this.Alert(msg);
                    }
                }
                catch (Exception E)
                {
                    dbContextTransaction.Rollback();
                    this.Alert("删除失败！" + E.Message.ToJsString());
                }
            }
        }
    }

    /// <summary>
    /// 行数据绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdINASN_IA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmInPo_IAEdit.aspx", SYSOperation.Modify, strKeyID), "预入库通知单", "INASN_IA");
        }
    }

    #endregion

    #region 方法

    #region 列表数据查询

    public IQueryable<INASN_IA> GetQueryList(DBContext modContext)
    {
        var queryList = from ia in modContext.INASN_IA
                        select ia;
        if (queryList != null)
        {
            if (!string.IsNullOrEmpty(txtCTICKETCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cticketcode.Contains(txtCTICKETCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtPONO.Text.Trim()))
            {
                queryList = queryList.Where(x => x.pono == txtPONO.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPO_Line.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.INASN_IA_D.Any(p => p.poline == txtPO_Line.Text.Trim() && p.id == x.id));
            }
            if (!string.IsNullOrEmpty(txtBatchNo.Text.Trim().ToUpper()))
            {
                queryList = queryList.Where(x => x.batchno.Contains(txtBatchNo.Text.Trim().ToUpper()));
            }
            if (!string.IsNullOrEmpty(txtErpCode.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cerpcode == txtErpCode.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtCinvCode.Text.Trim().ToUpper()))
            {
                queryList = queryList.Where(x => modContext.INASN_IA_D.Any(p => p.cinvcode.Contains(txtCinvCode.Text.Trim().ToUpper()) && p.id == x.id));
            }
            if (!string.IsNullOrEmpty(txtCCREATEOWNERCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.createowner == txtCCREATEOWNERCODE.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
            {
                DateTime podateFrom = Convert.ToDateTime(txtDCREATETIMEFrom.Text.Trim());
                queryList = queryList.Where(x => x.createtime >= podateFrom);
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMETo.Text.Trim()))
            {
                DateTime podateTo = Convert.ToDateTime(txtDCREATETIMETo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.createtime < podateTo);
            }
            if (!string.IsNullOrEmpty(txtCVENDERCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cvendercode == txtCVENDERCODE.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtCVENDER.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cvender.Contains(txtCVENDER.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(dpdStatus.SelectedValue.Trim()))
            {
                queryList = queryList.Where(x => x.cstatus == dpdStatus.SelectedValue.Trim());
            }
        }
        return queryList;
    }

    public void GridDataBind()
    {
        using (var modContext = this.context)
        {
            var queryList = GetQueryList(modContext);
            queryList = queryList.OrderByDescending(x => x.cticketcode);

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;
            List<INASN_IA> data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            var IN_PO_IA = GetParametersByFlagType("IN_PO_IA"); 
            var source = from ia in data
                         join sp in IN_PO_IA.DefaultIfEmpty() on ia.cstatus equals sp.FLAG_ID                         
                         select new
                         {
                             ia.id,
                             ia.cticketcode,
                             ia.pono,
                             ia.batchno,
                             ia.cerpcode,
                             ia.currency,
                             ia.tradecode,
                             status = sp.FLAG_NAME
                         };

            grdINASN_IA.DataSource = source.ToList();
            grdINASN_IA.DataBind();
        }
    }
    #endregion

    /// <summary>
    /// 检查查询数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (this.txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMEFrom.Text.IsDate() == false)
            {
                this.Alert("制单日期项不是有效的日期！");
                this.SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        if (this.txtDCREATETIMETo.Text.Trim() == "")
        {
            this.Alert("到项不允许空！");
            this.SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (this.txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMETo.Text.IsDate() == false)
            {
                this.Alert("到项不是有效的日期！");
                this.SetFocus(txtDCREATETIMETo);
                return false;
            }
        }
        return true;
    }

    #endregion

    //protected DataTable grdNavigatorINASN_IA_GetExportToExcelSource()
    //{
    //    BAR_FrmINASN_IAListQuery listQuery = new BAR_FrmINASN_IAListQuery();
    //    DataTable dtSource = listQuery.GetIAList(txtCTICKETCODE.Text.Trim(),txtPONO.Text.Trim(),txtPO_Line.Text.Trim(),txtBatchNo.Text.Trim().ToUpper(),txtErpCode.Text.Trim(),txtCinvCode.Text.Trim().ToUpper(),txtCCREATEOWNERCODE.Text.Trim(),txtDCREATETIMEFrom.Text.Trim(),txtDCREATETIMETo.Text.Trim(),txtCVENDERCODE.Text.Trim(),txtCVENDER.Text.Trim(),dpdStatus.SelectedValue.Trim(), false, -1, -1);
    //    return dtSource;
    //}

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdINASN_IA.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdINASN_IA.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

}

