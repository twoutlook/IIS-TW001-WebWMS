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
using DreamTek.ASRS.Business.RD;


public partial class Apps_RD_FrmChangeAuditByInAsn : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            InitPage();
            //btnSearch_Click(btnSearch, EventArgs.Empty);
            this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        //if (grdNavigatorInChange.IsDbPager)
        //    grdNavigatorInChange.GetExportToExcelSource += grdNavigatorInChange_GetExportToExcelSource;
        //FunctionNo = ""; //TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //HasRight();
        //灰化
        btnCheck.Attributes["onclick"] = GetPostBackEventReference(btnCheck) + ";disabled=true;";
    }

    //校验
    public bool CheckData()
    {
        if (txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
        }
        if (txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
            if (txtDCREATETIMEFrom.Text.IsDate() == false)
            {
                //制单日期项不是有效的日期
                Alert(Resources.Lang.FrmChangeAuditByInAsn_MSG3+ "！");
                SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        if (txtDCREATETIMETo.Text.Trim() == "")
        {
            //到项不允许空
            Alert(Resources.Lang.FrmChangeAuditByInAsn_MSG4 + "！");
            SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            if (txtDCREATETIMETo.Text.IsDate() == false)
            {
                //到项不是有效的日期
                Alert(Resources.Lang.FrmChangeAuditByInAsn_MSG5 + "！");
                SetFocus(txtDCREATETIMETo);
                return false;
            }
        }
        if (txtCAUDITPERSON.Text.Trim().Length > 0)
        {
        }
        if (txtDAUDITTIMEFrom.Text.Trim().Length > 0)
        {
            if (txtDAUDITTIMEFrom.Text.IsDate() == false)
            {
                //审核日期项不是有效的日期
                Alert(Resources.Lang.FrmChangeAuditByInAsn_MSG6 + "！");
                SetFocus(txtDAUDITTIMEFrom);
                return false;
            }
        }
        if (txtDAUDITTIMETo.Text.Trim() == "")
        {
            //到项不允许空
            Alert(Resources.Lang.FrmChangeAuditByInAsn_MSG4 + "！");
            SetFocus(txtDAUDITTIMETo);
            return false;
        }
        if (txtDAUDITTIMETo.Text.Trim().Length > 0)
        {
            if (txtDAUDITTIMETo.Text.IsDate() == false)
            {
                //到项不是有效的日期
                Alert(Resources.Lang.FrmChangeAuditByInAsn_MSG5 + "！");
                SetFocus(txtDAUDITTIMETo);
                return false;
            }
        }
        if (txtCTICKETCODE.Text.Trim().Length > 0)
        {
        }

        return true;
    }

    //界面初始化
    public void InitPage()
    {
        cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
        titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";
        //添加网格主键
        grdInChange.DataKeyNames = new[] { "ID" };
        //状态
        Help.DropDownListDataBind(GetParametersByFlagType("ASNCHANGE"), ddlCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
    }

    //导出excel
    protected DataTable grdNavigatorInChange_GetExportToExcelSource()
    {
        //var listQuery = new RD_FrmInChangeCtl();
        //var dtSource = listQuery.GetList_AuDit(txtCCREATEOWNERCODE.Text, txtDCREATETIMEFrom.Text, txtDCREATETIMETo.Text, txtCAUDITPERSON.Text, txtDAUDITTIMEFrom.Text, txtDAUDITTIMETo.Text, txtCTICKETCODE.Text, txtAsnCode.Text, ddlCSTATUS.SelectedValue,  txtLH.Text, -1, -1, "0");
        //foreach (DataRow dr in dtSource.Rows)
        //{
        //    //状态转换
        //    switch (dr["CSTATUS"].ToString().Trim())
        //    {
        //        case "0":
        //            dr["CSTATUS"] = Resources.Lang.CommonB_CSTATUS_undisposed ;//+ @"未处理";
        //            break;
        //        case "1":
        //            dr["CSTATUS"] = Resources.Lang.CommonB_CSTATUS_APPROVED ;//+ @"已审核";
        //            break;
        //        case "2":
        //            dr["CSTATUS"] = Resources.Lang.CommonB_CSTATUS_PROCESSING ;//+ @"处理中";
        //            break;
        //        case "3":
        //            dr["CSTATUS"] = Resources.Lang.CommonB_CSTATUS_COMPLETE ;//+ @"已完成";
        //            break;
        //    }
        //}
        //return dtSource;
        return new DataTable();
    }

    //分页1
    protected void grdInChange_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorInChange.IsDbPager)
        //{
        //    grdNavigatorInChange.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    grdInChange.PageIndex = e.NewPageIndex;
        //}
    }

    //分页2
    protected void grdInChange_PageIndexChanged(object sender, EventArgs e)
    {
        //GridBind();
    }


    public IQueryable<INASNCHANGE> GetQueryList()
    {
        IGenericRepository<INASNCHANGE> conn = new GenericRepository<INASNCHANGE>(db);
        var caseList = from p in conn.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCTICKETCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtCTICKETCODE.Text.Trim()));
            }
            if (txtAsnCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.inasn_cticketcode) && x.inasn_cticketcode.Contains(txtAsnCode.Text.Trim()));
            }

            if (txtCCREATEOWNERCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.create_owner) && x.create_owner.Contains(txtCCREATEOWNERCODE.Text.Trim()));
            }
            //DateTime d;
            //if (txtDCREATETIMEFrom.Text != string.Empty)
            //{
            //    if(DateTime.TryParse(txtDCREATETIMEFrom.Text.Trim(),out d))
            //    {
            //        caseList = caseList.Where(x => !x.create_time.HasValue && x.create_time.Value>=d);
            //    }
            //}
            //if (txtDCREATETIMETo.Text != string.Empty)
            //{
            //    if (DateTime.TryParse(txtDCREATETIMETo.Text.Trim(), out d))
            //    {
            //        caseList = caseList.Where(x => !x.create_time.HasValue && x.create_time.Value < d.AddDays(1));
            //    }
            //}

            if (ddlCSTATUS.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !x.cstatus.IsNullOrEmpty() && x.cstatus.Equals(ddlCSTATUS.SelectedValue));
            }

            DateTime d;
            if (!txtDCREATETIMEFrom.Text.IsNullOrEmpty())
            {
                if (DateTime.TryParseExact(txtDCREATETIMEFrom.Text, "yyyy-MM-dd",
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.None, out d))
                {
                    caseList = caseList.Where(x => x.create_time.HasValue && x.create_time.Value >= d);
                }
            }
            DateTime d2;
            if (!txtDCREATETIMETo.Text.IsNullOrEmpty())
            {
                if (DateTime.TryParseExact(txtDCREATETIMETo.Text, "yyyy-MM-dd",
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.None, out d2))
                {
                    caseList = caseList.Where(x => x.create_time.HasValue && x.create_time.Value < d2.AddDays(1));
                }
            }

            if (txtCAUDITPERSON.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cauditperson) && x.cauditperson.Contains(txtCAUDITPERSON.Text.Trim()));
            }
            if (txtDAUDITTIMEFrom.Text != string.Empty)
            {
                if (DateTime.TryParse(txtDAUDITTIMEFrom.Text.Trim(), out d))
                {
                    caseList = caseList.Where(x => !x.daudittime.HasValue && x.daudittime.Value >= d);
                }
            }
            if (txtDAUDITTIMETo.Text != string.Empty)
            {
                if (DateTime.TryParse(txtDAUDITTIMETo.Text.Trim(), out d))
                {
                    caseList = caseList.Where(x => !x.daudittime.HasValue && x.daudittime.Value < d.AddDays(1));
                }
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
            //if (!string.IsNullOrEmpty(sortStr))
            //{
            caseList = caseList.OrderBy(" CTICKETCODE desc ");
            //}

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        grdInChange.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdInChange.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    //查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ////重新设置GridNavigator的RowCount
        //grdNavigatorInChange.CurrentPageIndex = 0;
        //var listQuery = new RD_FrmInChangeCtl();
        //var dtRowCount = listQuery.GetList_AuDit(txtCCREATEOWNERCODE.Text, txtDCREATETIMEFrom.Text, txtDCREATETIMETo.Text, txtCAUDITPERSON.Text, txtDAUDITTIMEFrom.Text, txtDAUDITTIMETo.Text, txtCTICKETCODE.Text, txtAsnCode.Text, ddlCSTATUS.SelectedValue, txtLH.Text, 0, 0, "0");
        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    grdNavigatorInChange.RowCount = dtRowCount.Rows.Count;
        //}
        //else
        //{
        //    grdNavigatorInChange.RowCount = 0;
        //}
        ////网格绑定显示
        //GridBind();
        CurrendIndex = 1;
        Bind("");
    }

    //
    protected void grdInChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //状态转换
            switch (e.Row.Cells[e.Row.Cells.Count - 1].Text.Trim())
            {
                case "0":
                    e.Row.Cells[e.Row.Cells.Count - 1].Text = Resources.Lang.CommonB_CSTATUS_undisposed;// + @"未处理";
                    break;
                case "1":
                    e.Row.Cells[e.Row.Cells.Count - 1].Text = Resources.Lang.CommonB_CSTATUS_APPROVED ;//+ @"已审核";
                    break;
                case "2":
                    e.Row.Cells[e.Row.Cells.Count - 1].Text = Resources.Lang.CommonB_CSTATUS_PROCESSING ;//+ @"处理中";
                    break;
                case "3":
                    e.Row.Cells[e.Row.Cells.Count - 1].Text = Resources.Lang.CommonB_CSTATUS_COMPLETE ;//+ @"已完成";
                    break;
            }
        }
    }

    //审核
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        try
        {
            INQuery inQry = new INQuery();

            foreach (GridViewRow item in grdInChange.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    var cbo = itemFindControl as CheckBox;
                    if (cbo.Checked)
                    {
                        var id = grdInChange.DataKeys[item.RowIndex][0].ToString();
                        string ermsg = "";
                        if (inQry.Fun_Change_InAsn(id, out ermsg))
                        {
                            IGenericRepository<INASNCHANGE> conn = new GenericRepository<INASNCHANGE>(db);
                            INASNCHANGE bo = ( from p in conn.Get()
                                               where p.id == id
                                               select p).FirstOrDefault();

                            //var entity = new InAsnChangeEntity { ID = id };
                            //if (entity.SelectByPKeys())
                            //{
                            bo.cauditperson = WmsWebUserInfo.GetCurrentUser().UserNo;
                            bo.daudittime = DateTime.Now;
                            bo.last_upd_owner = WmsWebUserInfo.GetCurrentUser().UserNo;
                            bo.last_upd_time = DateTime.Now;
                            bo.cstatus = "1";
                            conn.Update(bo);
                            conn.Save();
                                //RD_FrmInChangeCtl.Update(entity);
                           // }
                        }
                        else
                        {
                            throw new Exception(ermsg);
                        }

                        //if (grdInChange.Rows[item.RowIndex].Cells[6].Text == @"未处理" && RD_FrmInChangeCtl.ValidateInChangeStatus(id, 0))
                        //{
                        //    var entity = new InAsnChangeEntity { ID = id };
                        //    if (entity.SelectByPKeys())
                        //    {
                        //        entity.CAUDITPERSON = WebUserInfo.GetCurrentUser().UserNo;
                        //        entity.DAUDITTIME = CommonFunction.GetDBNowTime().Value;
                        //        entity.LAST_UPD_OWNER = WebUserInfo.GetCurrentUser().UserNo;
                        //        entity.LAST_UPD_TIME = CommonFunction.GetDBNowTime().Value;
                        //        entity.CSTATUS = "1";
                        //        RD_FrmInChangeCtl.Update(entity);
                        //    }
                        //}
                        //else
                        //{
                        //    throw new Exception("只有未处理的单据才能审核");
                        //}
                    }
                }
            }
            if (msg.Length == 0)
            {
                //审核操作成功
                msg = Resources.Lang.FrmChangeAuditByInAsn_MSG7 + "!";
            }
        }
        catch (Exception err)
        {
            msg = err.Message.ToJsString();
        }
        finally
        {
            btnCheck.Style.Remove("disabled");
        }

        Alert(msg);
        btnSearch_Click(btnSearch, EventArgs.Empty);
    }
}