using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Business.SP.ProcedureModel;

public partial class Apps_OUT_FrmChangeByOutAsn : WMSBasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            InitPage();
            this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    public void InitPage()
    {
        cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //设定网格主键
        grdOutChange.DataKeyNames = new string[] { "ID" };
        //ddlCSTATUS
        Help.DropDownListDataBind(new SysParameterList().GetSys_ParameterByFLAG_TYPE("ASNCHANGE"), ddlCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//全部
        //本页面打开新增窗口
        btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmChangeByOutAsnEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmChangeByOutAsn_NewOutAsnChange + "','ChangeByOutAsn');return false;";//新建出库通知单变更单
    }

    //网格绑定
    public void GridBind()
    {
        IGenericRepository<OUTASNCHANGE> conn_m = new GenericRepository<OUTASNCHANGE>(db);
        IGenericRepository<OUTASNCHANGE_D> conn_d = new GenericRepository<OUTASNCHANGE_D>(db);

        var caseList = from m in conn_m.Get().AsEnumerable()
                       join d in conn_d.Get().AsEnumerable() on m.id equals d.id into temp
                       from t in temp.DefaultIfEmpty()
                       orderby m.create_time descending
                       select new
                       {
                           ID = m.id,
                           CTICKETCODE = m.cticketcode,
                           OUTASN_CTICKETCODE = m.outasn_cticketcode,
                           CREATE_OWNER = m.create_owner,
                           CREATE_TIME = m.create_time,
                           CSTATUS = m.cstatus,
                           CinvCode = (t != null && !string.IsNullOrEmpty(t.cinvcode)) ? t.cinvcode : ""
                       };
        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCTICKETCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CTICKETCODE) && x.CTICKETCODE.Contains(txtCTICKETCODE.Text.Trim()));
            }
            if (txtCOUTASNCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.OUTASN_CTICKETCODE) && x.OUTASN_CTICKETCODE.Contains(txtCOUTASNCode.Text.Trim()));
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
                if (DateTime.TryParse(txtDCREATETIMEFrom.Text, out d))
                {
                    caseList = caseList.Where(x => x.CREATE_TIME != null && x.CREATE_TIME.Value >= d);
                }
            }
            if (!txtDCREATETIMETo.Text.IsNullOrEmpty())
            {
                if (DateTime.TryParse(txtDCREATETIMETo.Text, out d))
                {
                    caseList = caseList.Where(x => x.CREATE_TIME != null && x.CREATE_TIME.Value < d.AddDays(1));
                }
            }
        }

        if (caseList != null && caseList.Count() > 0)
        {

            AspNetPager1.RecordCount = caseList.DistinctBy(x => x.ID).Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = " " + Resources.Lang.WMS_Common_Pager_PageCount + ":<b>" + "</b>";//总页数
        grdOutChange.DataSource = GetPageSize(caseList.DistinctBy(x => x.ID).AsQueryable(), PageSize, CurrendIndex).ToList();
        grdOutChange.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    

    //网格绑定
    protected void grdOutChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //反向调拨单链接
            string strKeyID = grdOutChange.DataKeys[e.Row.RowIndex].Values[0].ToString();

            //编辑
            var linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
            linkModify.NavigateUrl = "#";
            OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmChangeByOutAsnEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmChangeByOutAsn_ModifyOutAsnChange, "ChangeByOutAsn");//编辑出库通知单变更单
            //状态转换
            switch (e.Row.Cells[e.Row.Cells.Count - 3].Text.Trim())
            {
                case "0":
                    e.Row.Cells[e.Row.Cells.Count - 3].Text = Resources.Lang.WMS_Common_DropOption_UnDo;// @"未处理";
                    break;
                case "1":
                    e.Row.Cells[e.Row.Cells.Count - 3].Text = Resources.Lang.WMS_Common_DropOption_HasBeAudited;//  @"已审核";
                    break;
                case "2":
                    e.Row.Cells[e.Row.Cells.Count - 3].Text = Resources.Lang.WMS_Common_DropOption_Doing;// @"处理中";
                    break;
                case "3":
                    e.Row.Cells[e.Row.Cells.Count - 3].Text = Resources.Lang.WMS_Common_DropOption_Conplete;// @"已完成";
                    break;
            }
        }
    }

    //删除
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        int cnt = 0;
        try
        {
            for (int i = 0; i < grdOutChange.Rows.Count; i++)
            {
                var chkSelect = (CheckBox)grdOutChange.Rows[i].Cells[0].FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    string id = this.grdOutChange.DataKeys[i].Values[0].ToString();
                    var modOutChange = context.OUTASNCHANGE.Where(x => x.id == id).FirstOrDefault();
                    if(modOutChange != null && modOutChange.cstatus == "0"){
                         //未处理的才允许删除
                        //只有新增的单据才允许删除，删除完毕同时调整控制表状态
                        var procDel = new Proc_Del_OutChangeByAsn
                        {
                            pID = this.grdOutChange.DataKeys[i].Values[0].ToString(),
                            pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo,
                            pReserveField1 = string.Empty,
                            pReserveField2 = string.Empty,
                            pRetCode = string.Empty,
                            pRetMsg = string.Empty
                        };
                        procDel.Execute();
                        cnt++;
                        if (procDel.ReturnValue != 0)
                        {
                            throw new Exception(procDel.ErrorMessage);
                        }    
                    }
                    else
                    {
                        msg = Resources.Lang.FrmOutChangeAsn_UnDoCanDelete;//只有状态为[未處理]的单据才能删除.
                        break;
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;//删除成功!
            }
            if (cnt == 0 && msg=="")
            {
                msg = Resources.Lang.WMS_Common_DeleteTips;//请选择需要删除的项!
            }
            GridBind();
        }
        catch (Exception ex)
        {
            msg += Resources.Lang.WMS_Common_Msg_DeleteFailed + ex.Message;//删除失败!
        }
        Alert(msg);
    }
    
}