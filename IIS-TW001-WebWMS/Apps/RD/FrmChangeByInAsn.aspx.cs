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

public partial class Apps_RD_FrmChangeByInAsn : WMSBasePage
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
    }

    public void InitPage()
    {
        cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
        titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";
        //设定网格主键
        grdInChange.DataKeyNames = new string[] { "ID" };
        //ddlCSTATUS
        Help.DropDownListDataBind(GetParametersByFlagType("ASNCHANGE"), ddlCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        //本页面打开新增窗口
        //新建入库变更单(通知单)
        btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmChangeByInAsnEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmChangeByInAsn_MSG1 + "','ChangeByInAsn');return false;";
    }

    //网格绑定
    public void GridBind()
    {
        IGenericRepository<INASNCHANGE> conn_m = new GenericRepository<INASNCHANGE>(db);
        IGenericRepository<INASNCHANGE_D> conn_d = new GenericRepository<INASNCHANGE_D>(db);

        var caseList = from m in conn_m.Get().AsEnumerable()
                       join d in conn_d.Get().AsEnumerable() on m.id equals d.id into temp
                       from t in temp.DefaultIfEmpty()
                       orderby m.create_time descending
                       select new
                       {
                           ID = m.id,
                           CTICKETCODE = m.cticketcode,
                           INASN_CTICKETCODE = m.inasn_cticketcode,
                           CREATE_OWNER = m.create_owner,
                           CREATE_TIME = m.create_time,
                           CSTATUS = m.cstatus,
                           CinvCode = (t != null  &&  !string.IsNullOrEmpty(t.cinvcode)) ? t.cinvcode: ""
                       };

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCTICKETCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CTICKETCODE) && x.CTICKETCODE.Contains(txtCTICKETCODE.Text.Trim()));
            }
            if (txtInASNCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.INASN_CTICKETCODE) && x.INASN_CTICKETCODE.Contains(txtInASNCode.Text.Trim()));
            }
            if (txtCinvCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CinvCode) && x.CinvCode.Contains(txtCinvCode.Text.Trim()));
            }
            if (ddlCSTATUS.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CSTATUS) && x.CSTATUS.Equals(ddlCSTATUS.SelectedValue));
            }
            if (txtCCREATEOWNERCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CREATE_OWNER) && x.CREATE_OWNER.Contains(txtCCREATEOWNERCODE.Text.Trim()));
            }
            DateTime d;
            if (!txtDCREATETIMEFrom.Text.IsNullOrEmpty())
            {
                if (DateTime.TryParseExact(txtDCREATETIMEFrom.Text, 
                                            "yyyy-MM-dd", 
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.None, out d))
                {
                    caseList = caseList.Where(x => x.CREATE_TIME != null && x.CREATE_TIME.Value >= d);
                }
            }
            DateTime d2;
            if (!txtDCREATETIMETo.Text.IsNullOrEmpty())
            {
                if (DateTime.TryParseExact(txtDCREATETIMETo.Text, "yyyy-MM-dd",
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.None, out d2))
                {
                    caseList = caseList.Where(x => x.CREATE_TIME != null && x.CREATE_TIME.Value < d2.AddDays(1));
                }
            }
        }

        if (caseList != null && caseList.Count() > 0)
        {
            caseList = caseList.OrderBy(" CTICKETCODE desc ");
            AspNetPager1.RecordCount = caseList.DistinctBy(x => x.ID).Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
        grdInChange.DataSource = GetPageSize(caseList.DistinctBy(x => x.ID).AsQueryable(), PageSize, CurrendIndex).ToList();
        grdInChange.DataBind();


    }


    

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }



    //导出网格
    protected DataTable grdNavigatorInChange_GetExportToExcelSource()
    {
        //var listQuery = new RD_FrmInChangeCtl();
        //var dtSource = listQuery.GetList(txtCTICKETCODE.Text, txtInASNCode.Text, txtCinvCode.Text,
        //                                   ddlCSTATUS.SelectedValue, txtCCREATEOWNERCODE.Text, txtDCREATETIMEFrom.Text,
        //                                   txtDCREATETIMETo.Text, -1, -1, "0");
        //foreach (DataRow dr in dtSource.Rows)
        //{
        //    //状态转换
        //    switch (dr["CSTATUS"].ToString().Trim())
        //    {
        //        case "0":
        //            dr["CSTATUS"] = Resources.Lang.CommonB_CSTATUS_undisposed;//@"未处理";
        //            break;
        //        case "1":
        //            dr["CSTATUS"] =Resources.Lang.CommonB_CSTATUS_APPROVED;// @"已审核";
        //            break;
        //        case "2":
        //            dr["CSTATUS"] =Resources.Lang.CommonB_CSTATUS_PROCESSING;// @"处理中";
        //            break;
        //        case "3":
        //            dr["CSTATUS"] =Resources.Lang.CommonB_CSTATUS_COMPLETE;// @"已完成";
        //            break;
        //    }
        //}

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
        GridBind();
    }

    //查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //grdNavigatorInChange.CurrentPageIndex = 0;
        ////重新设置GridNavigator的RowCount
        //var listQuery = new RD_FrmInChangeCtl();
        //var dtRowCount = listQuery.GetList(txtCTICKETCODE.Text, txtInASNCode.Text, txtCinvCode.Text,
        //                                   ddlCSTATUS.SelectedValue, txtCCREATEOWNERCODE.Text, txtDCREATETIMEFrom.Text,
        //                                   txtDCREATETIMETo.Text, 0, 0, "0");

        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    grdNavigatorInChange.RowCount = dtRowCount.Rows.Count;
        //}
        //else
        //{
        //    grdNavigatorInChange.RowCount = 0;
        //}
        //绑定网格
        this.CurrendIndex = 1;
        GridBind();
    }

    //网格绑定
    protected void grdInChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = grdInChange.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //编辑
            var linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmChangeByInAsnEdit.aspx", SYSOperation.Modify, strKeyID), "编辑入库变更单(通知单)", "ChangeByInAsn");
            //状态转换
            switch (e.Row.Cells[e.Row.Cells.Count - 2].Text.Trim())
            {
                case "0":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = Resources.Lang.CommonB_CSTATUS_undisposed;//@"未处理";
                    break;
                case "1":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text =Resources.Lang.CommonB_CSTATUS_APPROVED;// @"已审核";
                    break;
                case "2":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = Resources.Lang.CommonB_CSTATUS_PROCESSING;//@"处理中";
                    break;
                case "3":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = Resources.Lang.CommonB_CSTATUS_COMPLETE;//@"已完成";
                    break;
            }
        }
    }

    //删除
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        //DBUtil.BeginTrans();
        try
        {
            for (int i = 0; i < grdInChange.Rows.Count; i++)
            {
                var chkSelect = (CheckBox)grdInChange.Rows[i].Cells[0].FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    //未处理的才允许删除
                    //
                    var cstatus = this.grdInChange.DataKeys[i].Values[0].ToString();

                    if (grdInChange.Rows[i].Cells[grdInChange.Rows[i].Cells.Count - 2].Text.Trim().Equals("未处理"))
                    {
                        //只有新增的单据才允许删除，删除完毕同时调整控制表状态
                        //var procPHL = new proc_del_InChangeByAsn
                        //{
                        //    pID = this.grdInChange.DataKeys[i].Values[0].ToString(),
                        //    pUserNo = WebUserInfo.GetCurrentUser().UserNo
                        //};
                        //procPHL.Execute();
                        //if (procPHL.pRetCode != 1)//非1-校验失败
                        //{
                        //    throw new Exception(procPHL.pRetMsg);
                        //}

                        var procDel = new Proc_Del_InChangeByAsn
                        {
                            pID = this.grdInChange.DataKeys[i].Values[0].ToString(),
                            pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo,
                            pReserveField1 = string.Empty,
                            pReserveField2 = string.Empty,
                            pRetCode = string.Empty,
                            pRetMsg = string.Empty
                        };
                        procDel.Execute();
                        if (procDel.ReturnValue != 0)
                        {
                            throw new Exception(procDel.ErrorMessage);
                        }
                    }
                    else
                    {
                        msg = Resources.Lang.FrmChangeByInAsn_MSG2;//"只有状态为[未處理]的单据才能删除.";
                        break;
                    }
                }
            }
            if (msg.Length == 0)
            {
                //删除成功
                msg = Resources.Lang.CommonB_RemoveSuccess + "!";
            }
            //DBUtil.Commit();
            GridBind();
        }
        catch (Exception ex)
        {
            //删除失败
            msg += Resources.Lang.CommonB_RemoveFailed + "!" + ex.Message;
            //DBUtil.Rollback();
        }
        Alert(msg);
    }
}