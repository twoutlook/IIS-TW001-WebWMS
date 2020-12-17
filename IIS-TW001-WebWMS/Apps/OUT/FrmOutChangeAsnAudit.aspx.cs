using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;

public partial class Apps_OUT_FrmOutChangeAsnAudit :WMSBasePage
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            InitPage();
            //btnSearch_Click(btnSearch, EventArgs.Empty);
            this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
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
                Alert("制单日期项不是有效的日期！");
                SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        if (txtDCREATETIMETo.Text.Trim() == "")
        {
            Alert("到项不允许空！");
            SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            if (txtDCREATETIMETo.Text.IsDate() == false)
            {
                Alert("到项不是有效的日期！");
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
                Alert("审核日期项不是有效的日期！");
                SetFocus(txtDAUDITTIMEFrom);
                return false;
            }
        }
        if (txtDAUDITTIMETo.Text.Trim() == "")
        {
            Alert("到项不允许空！");
            SetFocus(txtDAUDITTIMETo);
            return false;
        }
        if (txtDAUDITTIMETo.Text.Trim().Length > 0)
        {
            if (txtDAUDITTIMETo.Text.IsDate() == false)
            {
                Alert("到项不是有效的日期！");
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
        cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //添加网格主键
        grdOutChange.DataKeyNames = new string[] { "ID" };
        //状态
        Help.DropDownListDataBind(new SysParameterList().GetSys_ParameterByFLAG_TYPE("ASNCHANGE"), ddlCSTATUS, "全部", "FLAG_NAME", "FLAG_ID", "");
    }
    public void GridBind()
    {
        //var listQuery = new OUT_FrmOutChangeCtl();
        //var dtSource = listQuery.GetList_AuDit(txtCCREATEOWNERCODE.Text, txtDCREATETIMEFrom.Text, txtDCREATETIMETo.Text, txtCAUDITPERSON.Text, txtDAUDITTIMEFrom.Text, txtDAUDITTIMETo.Text, txtCTICKETCODE.Text, txtAsnCode.Text, ddlCSTATUS.SelectedValue, txtERP.Text, txtLH.Text, grdNavigatorOutChange.CurrentPageIndex, grdOutChange.PageSize, "1");
        //grdOutChange.DataSource = dtSource;
        //grdOutChange.DataBind();

        IGenericRepository<OUTASNCHANGE> conn_m = new GenericRepository<OUTASNCHANGE>(db);
        IGenericRepository<OUTASNCHANGE_D> conn_d = new GenericRepository<OUTASNCHANGE_D>(db);
        var caseList = from m in conn_m.Get().AsEnumerable()
                       join d in conn_d.Get().AsEnumerable() on m.id equals d.id into temp
                       from t in temp.DefaultIfEmpty()
                       orderby m.create_time descending
                       select new
                       {
                           ID = m.id,
                           ERPCODE=m.erpcode,
                           CTICKETCODE = m.cticketcode,
                           OUTASN_CTICKETCODE = m.outasn_cticketcode,
                           CREATE_OWNER = m.create_owner,
                           CREATE_TIME = m.create_time,
                           CAUDITPERSON=m.cauditperson,
                           DAUDITTIME=m.daudittime,
                           CSTATUS = m.cstatus,
                           CinvCode = (t != null && !string.IsNullOrEmpty(t.cinvcode)) ? t.cinvcode : ""
                       };
        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCTICKETCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CTICKETCODE) && x.CTICKETCODE.Contains(txtCTICKETCODE.Text.Trim()));
            }
            if (txtAsnCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.OUTASN_CTICKETCODE) && x.OUTASN_CTICKETCODE.Contains(txtAsnCode.Text.Trim()));
            }
            if (txtERP.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ERPCODE) && x.ERPCODE.Contains(txtERP.Text.Trim()));
            }
            if (txtLH.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CinvCode) && x.CinvCode.Contains(txtLH.Text.Trim()));
            }

            if (txtCAUDITPERSON.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CAUDITPERSON) && x.CAUDITPERSON.Contains(txtCAUDITPERSON.Text.Trim()));
            }
            if (ddlCSTATUS.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CSTATUS) && x.CSTATUS.Equals(ddlCSTATUS.SelectedValue));
            }
            if (txtCCREATEOWNERCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CREATE_OWNER) && x.CREATE_OWNER.Contains(txtCCREATEOWNERCODE.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
                caseList = caseList.Where(x => x.CREATE_TIME != null && x.CREATE_TIME >= Convert.ToDateTime(txtDCREATETIMEFrom.Text.Trim()));
            if (txtDCREATETIMETo.Text != string.Empty)
                caseList = caseList.Where(x => x.CREATE_TIME != null && x.CREATE_TIME <= Convert.ToDateTime(txtDCREATETIMETo.Text.Trim()).AddDays(1));

            if (!string.IsNullOrEmpty(txtDAUDITTIMEFrom.Text.Trim()))
                caseList = caseList.Where(x => x.DAUDITTIME != null && x.DAUDITTIME >= Convert.ToDateTime(txtDAUDITTIMEFrom.Text.Trim()));
            if (txtDAUDITTIMETo.Text != string.Empty)
                caseList = caseList.Where(x => x.DAUDITTIME != null && x.DAUDITTIME <= Convert.ToDateTime(txtDAUDITTIMETo.Text.Trim()).AddDays(1));
            //DateTime d;
            //if (!txtDCREATETIMEFrom.Text.IsNullOrEmpty())
            //{
            //    if (DateTime.TryParse(txtDCREATETIMEFrom.Text, out d))
            //    {
            //        caseList = caseList.Where(x => x.CREATE_TIME != null && x.CREATE_TIME.Value >= d);
            //    }
            //}
            //if (!txtDCREATETIMETo.Text.IsNullOrEmpty())
            //{
            //    if (DateTime.TryParse(txtDCREATETIMETo.Text, out d))
            //    {
            //        caseList = caseList.Where(x=>x.CREATE_TIME != null && x.CREATE_TIME.Value < d.AddDays(1));
            //    }
            //}
            //if (!txtDAUDITTIMEFrom.Text.IsNullOrEmpty())
            //{
            //    if (DateTime.TryParse(txtDAUDITTIMEFrom.Text, out d))
            //    {
            //        caseList = caseList.Where(x => x.DAUDITTIME != null && x.DAUDITTIME.Value >= d);
            //    }
            //}
            //if (!txtDAUDITTIMETo.Text.IsNullOrEmpty())
            //{
            //    if (DateTime.TryParse(txtDAUDITTIMETo.Text, out d))
            //    {
            //        caseList = caseList.Where(x => x.DAUDITTIME != null && x.DAUDITTIME.Value < d.AddDays(1));
            //    }
            //}
        }

        if (caseList != null && caseList.Count() > 0)
        {
            //caseList = caseList.OrderBy(" CTICKETCODE desc ");
            AspNetPager1.RecordCount = caseList.DistinctBy(x => x.ID).Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = " 总页数:<b>" + "</b>";
        grdOutChange.DataSource = GetPageSize(caseList.DistinctBy(x => x.ID).AsQueryable(), PageSize, CurrendIndex).ToList();
        grdOutChange.DataBind();
    }

    //
    protected void grdOutChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var strKeyID = grdOutChange.DataKeys[e.Row.RowIndex][0].ToString();
            //var linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            //linkModify.NavigateUrl = "#";
            //OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmOutChangeAsnEdit.aspx", SYSOperation.Approve, strKeyID), "出变更单审核", "OutChange");
            //状态转换
            switch (e.Row.Cells[e.Row.Cells.Count - 2].Text.Trim())
            {
                case "0":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = @"未处理";
                    break;
                case "1":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = @"已审核";
                    break;
                case "2":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = @"处理中";
                    break;
                case "3":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = @"已完成";
                    break;
            }
        }
    }

    //审核
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        int cnt = 0;
        try
        {
            foreach (GridViewRow item in grdOutChange.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    var cbo = itemFindControl as CheckBox;
                    if (cbo.Checked)
                    {
                        var id = grdOutChange.DataKeys[item.RowIndex][0].ToString();
                        if (grdOutChange.Rows[item.RowIndex].Cells[7].Text == @"未处理" && this.ValidateOutChangeStatus(id, 0))
                        {
                                IGenericRepository<OUTASNCHANGE> con = new GenericRepository<OUTASNCHANGE>(context);
                                var caseList = from p in con.Get()
                                               where p.id == id
                                               select p;
                                OUTASNCHANGE entity = caseList.ToList().FirstOrDefault();
                                entity.cauditperson = WmsWebUserInfo.GetCurrentUser().UserNo;
                                entity.daudittime = DateTime.Now;
                                entity.last_upd_owner = WmsWebUserInfo.GetCurrentUser().UserNo;
                                entity.last_upd_time = DateTime.Now;
                                entity.cstatus = "1";
                                con.Update(entity);
                                con.Save();
                                cnt++;
                        }
                        else
                        {
                            throw new Exception("只有未处理的单据才能审核");
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = "审核操作成功!";
            }
            if (cnt == 0)
            {
                msg = "请选择审核的项!";
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
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }
    // 取消审核
    protected void btnCheckCancel_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        int cnt = 0;
        try
        {
            foreach (GridViewRow item in grdOutChange.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    CheckBox cbo = itemFindControl as CheckBox;
                    if (cbo.Checked)
                    {
                        string id = grdOutChange.DataKeys[item.RowIndex][0].ToString();
                        if (grdOutChange.Rows[item.RowIndex].Cells[7].Text == @"已审核" && this.ValidateOutChangeStatus(id, 1))
                        {
                            //var entity = new OutAsnChangeEntity { ID = id };
                            //if (entity.SelectByPKeys())
                            //{
                            //    entity.CAUDITPERSON = WmsWebUserInfo.GetCurrentUser().UserNo;
                            //    entity.DAUDITTIME = CommonFunction.GetDBNowTime().Value;
                            //    entity.LAST_UPD_OWNER = WmsWebUserInfo.GetCurrentUser().UserNo;
                            //    entity.LAST_UPD_TIME = CommonFunction.GetDBNowTime().Value;
                            //    entity.CSTATUS = "0";
                            //    OUT_FrmOutChangeCtl.Update(entity);
                            //    cnt++;
                            //}
                        }
                        else
                        {
                            throw new Exception("只有已审核的单据才能取消审核");
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = "审核操作成功!";
            }
            if (cnt == 0)
            {
                msg = "请选择取消审核的项!";
            }
        }
        catch (Exception err)
        {
            msg = err.Message.ToJsString();
        }
        Alert(msg);
        btnSearch_Click(btnSearch, EventArgs.Empty);
    }

    //获取变更通知单状态
    public bool ValidateOutChangeStatus(string ID, int status)
    {
        IGenericRepository<OUTASNCHANGE> con = new GenericRepository<OUTASNCHANGE>(context);
        var modChange = con.Get().Where(x => x.id == ID).FirstOrDefault();
        if (modChange != null && modChange.cstatus == status.ToString())
        {
            return true;
        }
        else {
            return false;
        }
    }

    #endregion
}