using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Web;


public partial class RD_FrmInPoIA_IQCList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    #region 事件

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridDataBind();
    }

    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        GridDataBind();
    }

    /// <summary>
    /// 批量功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBatch_Click(object sender, EventArgs e)
    {
        int count = 0;
        string msg = string.Empty;
        using (var context = this.context)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    //string return_Value = string.Empty;
                    for (int i = 0; i < this.grdIA_QC.Rows.Count; i++)
                    {
                        if (this.grdIA_QC.Rows[i].Cells[0].Controls[1] is CheckBox)
                        {
                            CheckBox chkSelect = (CheckBox)this.grdIA_QC.Rows[i].Cells[0].Controls[1];
                            if (chkSelect.Checked)
                            {
                                string idS = this.grdIA_QC.DataKeys[i].Values[0].ToString();
                                var obj = context.INASN_IA_D.Where(x => x.ids == idS).FirstOrDefault();
                                if (obj != null)
                                {
                                    if (obj.status != 0)
                                    {
                                        msg = Resources.Lang.FrmInPoIA_IQCList_MSG3;// "预入库通知单明细状态不是[未处理]不能质检";
                                    }
                                    //获取预入库通知单
                                    var objIa = context.INASN_IA.Where(x => x.id == obj.id).FirstOrDefault();
                                    if (objIa != null)
                                    {
                                        //更新详情
                                        obj.status = 1;//已质检
                                        obj.qtypassed = obj.qtytotal;
                                        obj.qtyunpassed = 0;
                                        obj.qtypending = 0;
                                        obj.qtysampling = 0;
                                        obj.mpn = obj.cinvcode;
                                        obj.mfg = objIa.cvendercode;
                                        obj.lastupdatetime = DateTime.Now;
                                        obj.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                                        context.INASN_IA_D.Attach(obj);
                                        context.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                                        //存在未质检查完成的明细项
                                        if (context.INASN_IA_D.Where(x => x.id == objIa.id && x.status == 0).Any())
                                        {
                                            objIa.cstatus = "2";
                                        }
                                        else
                                        {
                                            objIa.cstatus = "3";
                                        }
                                        context.INASN_IA.Attach(objIa);
                                        context.Entry(objIa).State = System.Data.Entity.EntityState.Modified;

                                        context.SaveChanges();
                                        count++;
                                    }
                                    else
                                    {
                                        msg = Resources.Lang.FrmInPoIA_IQCList_MSG4;// "数据异常！操作失败！";
                                        break;
                                    }
                                }
                                else
                                {
                                    msg = Resources.Lang.FrmInPoIA_IQCList_MSG4;//"数据异常！操作失败！";
                                    break;
                                }


                                //string idS = this.grdIA_QC.DataKeys[i].Values[0].ToString();
                                //Proc_Change_PoIa_Status proc = new Proc_Change_PoIa_Status();
                                //proc.P_PO_IDS = "";
                                //proc.P_IA_ID = idS;
                                //proc.P_BZ = "3";
                                //proc.P_UserNo = WebUserInfo.GetCurrentUser().UserNo;
                                //proc.P_ReservedField1 = "";
                                //proc.P_ReservedField2 = "";
                                //proc.Execute();
                                //return_Value = proc.P_return_Value;
                                //if (return_Value != "0")
                                //{
                                //    msg = proc.errText;
                                //    break;
                                //}
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(msg))
                    {
                        if (count > 0)
                        {
                            dbContextTransaction.Commit();
                            this.Alert(Resources.Lang.FrmInPoIA_IQCList_MSG5 + "！");//质检成功
                            CurrendIndex = 1;
                            this.GridDataBind();
                        }
                        else
                        {
                            this.Alert(Resources.Lang.FrmInPoIA_IQCList_MSG6 + "！");//请选择要质检的数据

                        }
                    }
                    else
                    {
                        dbContextTransaction.Rollback();
                        this.Alert(msg);
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    this.Alert(Resources.Lang.FrmInPoIA_IQCList_MSG7 + "！" + ex.Message.ToJsString());//质检失败
                }
            }
        }
    }

    /// <summary>
    /// 行数据绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdIA_QC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            //"IQC质检"
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmInPoIA_IQCEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmInPoIA_IQCList_Content1, "IA_IQC");

        }
    }
    #endregion

    #region 方法
    private string GetKeyIDS(int rowIndex)
    {
        return this.grdIA_QC.DataKeys[rowIndex].Values[0].ToString();
    }

    /// <summary>
    /// 数据绑定
    /// </summary>
    public void GridDataBind()
    {
        using (var modContext = this.context)
        {
            var queryList = from iad in modContext.INASN_IA_D
                            join ia in modContext.INASN_IA.DefaultIfEmpty() on iad.id equals ia.id
                            join sp in modContext.SYS_PARAMETER.DefaultIfEmpty() on iad.status.ToString() equals sp.flag_id
                            where sp.flag_type == "IN_PO_IA_D" && (ia.cstatus == "1" || ia.cstatus == "2")                           
                            select new
                            {
                                iad.ids,
                                iad.id,
                                ia.cticketcode,
                                ia.cerpcode,
                                ia.batchno,
                                ia.pono,
                                iad.poline,
                                iad.cinvcode,
                                iad.cinvname,
                                iad.qtytotal,
                                ia.createowner,
                                ia.createtime,
                                iad.status,
                                flag_name = sp.flag_id
                            };

            #region 查询条件
            if (!string.IsNullOrEmpty(txtCTICKETCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cticketcode == txtCTICKETCODE.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtERP_No.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cerpcode == txtERP_No.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBatchNo.Text.Trim()))
            {
                queryList = queryList.Where(x => x.batchno == txtBatchNo.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPONo.Text.Trim()))
            {
                queryList = queryList.Where(x => x.pono == txtPONo.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPoLine.Text.Trim()))
            {
                queryList = queryList.Where(x => x.poline == txtPoLine.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim().ToUpper()))
            {
                queryList = queryList.Where(x => x.cinvcode.Contains(txtCinvcode.Text.Trim().ToUpper()));
            }
            if (!string.IsNullOrEmpty(txtCCREATEOWNERCODE.Text))
            {
                queryList = queryList.Where(x => x.createowner == txtCCREATEOWNERCODE.Text);
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
            if (!string.IsNullOrEmpty(txtCSTATUS.SelectedValue))
            {
                queryList = queryList.Where(x => x.status.ToString() == txtCSTATUS.SelectedValue);
            }
            #endregion

            queryList = queryList.OrderBy(x => new { x.cticketcode, x.pono });            
            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;

            //grdIA_QC.DataSource = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            var listResult = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            var source = GetGridSourceDataByList(listResult, "flag_name", "IN_PO_IA_D");
            grdIA_QC.DataSource = source;
            grdIA_QC.DataBind();
         
        }
    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
       
        this.grdIA_QC.DataKeyNames = new string[] { "IDS" };

       //状态
        Help.DropDownListDataBind(GetParametersByFlagType("IN_PO_IA_D"), txtCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        txtCSTATUS.SelectedValue = "0";
    }

    #endregion

    //protected DataTable grdNavigatorIA_QC_GetExportToExcelSource()
    //{
    //    PoIA_IQC_Query listQuery = new PoIA_IQC_Query();
    //    DataTable dtSource = null;
    //    try
    //    {
    //        dtSource = listQuery.GetList(txtCTICKETCODE.Text.Trim(), txtERP_No.Text.Trim(), txtBatchNo.Text.Trim(), txtPONo.Text.Trim(), txtPoLine.Text.Trim(), txtCinvcode.Text.Trim().ToUpper(), txtCCREATEOWNERCODE.Text, txtDCREATETIMEFrom.Text, txtDCREATETIMETo.Text, txtCSTATUS.SelectedValue, false, -1, -1);
    //    }
    //    catch (Exception)
    //    {

    //    }
    //    return dtSource;
    //}

}

