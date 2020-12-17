using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Text;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.SqlClient;
using DreamTek.ASRS.Business.RD;
using System.Data.Entity.SqlServer;

/// <summary>
/// 描述: 入库管理-->FrmINASNList 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:50:50
/// </summary>
/*
Roger
2013/7/31 13:46:49
20130731134649
增加校验是否为正在修改中的料号
*/
public partial class FrmUnionPalletList : WMSBasePage //IPageGrid
{
    //DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                this.InitPage();
                //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
                this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        catch (Exception)
        {
        }
    }

    public void GridBind()
    {
        IGenericRepository<IN_MERGE_PALLETE> con = new GenericRepository<IN_MERGE_PALLETE>(db);
        var caseList = from p in con.Get()
                       select p;


        if (caseList != null && caseList.Count() > 0)
        {

            if (this.txtPalletCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.PALLETCODE) && x.PALLETCODE.Contains(txtPalletCode.Text.Trim()));
            }
            if (this.txtINASNTicketCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.INASNCODE_PALLET) && x.INASNCODE_PALLET.Contains(txtINASNTicketCode.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(this.txtOASNTicketCode.Text))
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.OUTASNCODE_PALLET) && x.OUTASNCODE_PALLET.Equals(txtOASNTicketCode.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(this.txtOutBillCticketCode.Text))
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.OUTBILLCODE_PALLET) && x.OUTBILLCODE_PALLET.Equals(this.txtOutBillCticketCode.Text));
            }
            if (!string.IsNullOrEmpty(this.ddlCSTATUS.Text))
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CSTATUS) && x.CSTATUS.Equals(this.ddlCSTATUS.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtCreateUser.Text))
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CREATEUSER) && x.CREATEUSER.Equals(this.txtCreateUser.Text));
            }
            if (!string.IsNullOrEmpty(this.txtINBillCTICKETCODE.Text))
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.INBILLCODE_PALLET) && x.INBILLCODE_PALLET.Equals(this.txtINBillCTICKETCODE.Text));
            }

            if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
            {
                caseList = caseList.Where(x => x.CREATETIME != null && SqlFunctions.DateDiff("dd", txtDCREATETIMEFrom.Text.Trim(), x.CREATETIME) >= 0);
            }

            if (txtDCREATETIMETo.Text != string.Empty)
            {
                caseList = caseList.Where(x => x.CREATETIME != null && SqlFunctions.DateDiff("dd", txtDCREATETIMETo.Text.Trim(), x.CREATETIME) <= 0);
            }


            if (!string.IsNullOrEmpty(this.txtUnionASNCode.Text))
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.INASNCODE) && x.INASNCODE.Equals(this.txtUnionASNCode.Text));
            }


            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
            AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";

            //grdINASN.DataSource = GetPageSize(caseList.OrderByDescending(x => x.CREATETIME), PageSize, CurrendIndex).ToList();
            var listResult = GetPageSize(caseList.OrderByDescending(x => x.CREATETIME), PageSize, CurrendIndex).ToList();
            var source = GetGridSourceDataByList(listResult,"CSTATUS", "IN_MERGE_PALLETE");
            grdINASN.DataSource = source;
            grdINASN.DataBind();

        }
        else
        {
            grdINASN.DataSource = null;
            grdINASN.DataBind();

            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;

        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        this.GridBind();
    }

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        Help.DropDownListDataBind(GetParametersByFlagType("IN_MERGE_PALLETE"), this.ddlCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        //MonthOrWeek
        Help.RadioButtonDataBind(GetParametersByFlagType("MonthOrWeek"), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");
    }

    protected void btnSearch_Click1(object sender, EventArgs e)
    {
        this.CurrendIndex = 1;
        this.GridBind();
    }

    protected void grdINASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ////状态转换
            //switch (e.Row.Cells[e.Row.Cells.Count - 3].Text.Trim())
            //{
            //    case "0":
            //        e.Row.Cells[e.Row.Cells.Count - 3].Text = Resources.Lang.FrmDispatchUnitEdit_MsgTitle32;//@"未处理";
            //        break;
            //    case "1":
            //        e.Row.Cells[e.Row.Cells.Count - 3].Text = Resources.Lang.FrmDispatchUnitList_MsgTitle2;//@"处理中";
            //        break;
            //    case "2":
            //        e.Row.Cells[e.Row.Cells.Count - 3].Text =Resources.Lang.FrmDispatchUnitList_MsgTitle3;// @"已完成";
            //        break;
            //}
            //
            var accountid = e.Row.Cells[e.Row.Cells.Count - 2].Text.Trim();
            e.Row.Cells[e.Row.Cells.Count - 2].Text = OPERATOR.GetUserNameByAccountID(accountid);
        }
    }
}

