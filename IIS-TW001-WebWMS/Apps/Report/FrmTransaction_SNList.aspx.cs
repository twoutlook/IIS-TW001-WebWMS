using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.SqlServer;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Collections;

public partial class FrmTransaction_SNList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            this.InitPage();
            this.txtBeginDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
    }


    #region IPageGrid 成员
    public DataTable ToExcel()
    {
        List<view_transaction_sn> list = new List<view_transaction_sn>();

        list = GetQueryList().ToList();
        btnExcel.ExcelName = Resources.Lang.FrmTransaction_SNList_Msg2;//"SN交易明细查询";

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("worktype", "WareHouseType"));//类型

        var srcdata = GetGridSourceDataByList(list, flagList);

        return srcdata;
    }
    public void GridBind()
    {
        //DataTable dtSource = FrmSTOCK_TradeQuery.GetSNList(txtErpCode.Text.Trim(), dpdInOutType.SelectedValue, txtSNCode.Text.Trim().ToUpper(), txtCINVCODE.Text.Trim().ToUpper(), 
        //    txtCPOSITIONCODE.Text.Trim().ToUpper(), txtCToPositionCode.Text.Trim().ToUpper(), txtBeginDate.Text.Trim(), txtEndDate.Text.Trim(), ddlsType.SelectedValue, false, this.grdNavigatorSTOCK_DURATION.CurrentPageIndex, this.grdSTOCK_DURATION.PageSize);
        //this.grdSTOCK_DURATION.DataSource = dtSource;
        //this.grdSTOCK_DURATION.DataBind();
    }



    public IQueryable<view_transaction_sn> GetQueryList()
    {
        IGenericRepository<view_transaction_sn> conn = new GenericRepository<view_transaction_sn>(db);
        var caseList = from p in conn.Get()
                       orderby p.CREATETIME descending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtErpCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtErpCode.Text.Trim()));
            }
            if (dpdInOutType.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.typecode) && x.typecode.Equals(dpdInOutType.SelectedValue));
            }
            if (txtSNCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.SN_CODE) && x.SN_CODE.Contains(txtSNCode.Text.Trim()));
            }
            if (txtCINVCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CINVCODE) && x.CINVCODE.Contains(txtCINVCODE.Text.Trim()));
            }
            if (txtCPOSITIONCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CPOSITIONCODE) && x.CPOSITIONCODE.Contains(txtCPOSITIONCODE.Text.Trim()));
            }
            if (txtCToPositionCode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CTOPOSITIONCODE) && x.CTOPOSITIONCODE.Contains(txtCToPositionCode.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()))
            {
                DateTime dtFrom = Convert.ToDateTime(txtBeginDate.Text.Trim());
                caseList = caseList.Where(x => x.CREATETIME.HasValue && x.CREATETIME >= dtFrom);
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                DateTime dtTo = Convert.ToDateTime(txtEndDate.Text.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59);
                caseList = caseList.Where(x => x.CREATETIME.HasValue && x.CREATETIME <= dtTo);
            }
            if (ddlsType.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.STYPE) && x.STYPE.Equals(ddlsType.SelectedValue));
            }
            if (ddlworktype.SelectedValue != "")
                caseList = caseList.Where(x => x.worktype.ToString().Equals(ddlworktype.SelectedValue));

        }
        return caseList;
    }

    public void Bind(string sortStr)
    {
        var caseList = GetQueryList();

        if (caseList != null && caseList.Count() > 0)
        {
            //按照列名排序
            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("worktype", "WareHouseType"));//类型

        var srcdata = GetGridSourceDataByList(data, flagList);

        grdSTOCK_DURATION.DataSource = srcdata;
        grdSTOCK_DURATION.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }


    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";

        Help.DropDownListDataBind(GetInOutAlloType(""), this.dpdInOutType, Resources.Lang.Common_ALL, "TYPENAME", "TYPECODE", "");
        Help.DropDownListDataBind(GetParametersByFlagType("WareHouseType"), this.ddlworktype, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", ""); 

    }

    #endregion

    protected DataTable grdNavigatorSTOCK_DURATION_GetExportToExcelSource()
    {
        DataTable dtSource = new DataTable();
        //DataTable dtSource = FrmSTOCK_TradeQuery.GetSNList(txtErpCode.Text.Trim(), dpdInOutType.SelectedValue, txtSNCode.Text.Trim().ToUpper(), txtCINVCODE.Text.Trim().ToUpper(), txtCPOSITIONCODE.Text.Trim().ToUpper(), txtCToPositionCode.Text.Trim().ToUpper(), txtBeginDate.Text.Trim(), txtEndDate.Text.Trim(), ddlsType.SelectedValue, false, -1, -1);
        ////将状态修改正确 20130605103658
        //foreach (DataRow dr in dtSource.Rows)
        //{
        //    switch (dr["STYPE"].ToString().Trim())
        //    {
        //        case "0":
        //            dr["STYPE"] = "<%$ Resources:Lang, Common_MSG1 %>";
        //            break;
        //        case "1":
        //            dr["STYPE"] = "<%$ Resources:Lang, Common_MSG2 %>";
        //            break;
        //        case "2":
        //            dr["STYPE"] = "<%$ Resources:Lang, Common_MSG3 %>";
        //            break;
        //    }
        //}
        return dtSource;
    }

    protected void grdSTOCK_DURATION_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorSTOCK_DURATION.IsDbPager)
        //{
        //    grdNavigatorSTOCK_DURATION.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdSTOCK_DURATION.PageIndex = e.NewPageIndex;
        //}
    }
    protected void grdSTOCK_DURATION_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
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
        Bind("");
    }


    protected void grdSTOCK_DURATION_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //}
    }

    protected void dsGrdSTOCK_DURATION_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdSTOCK_DURATION_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //if (e.ReturnValue is DataTable)
        //{
        //    if (grdNavigatorSTOCK_DURATION.IsDbPager == false)
        //        this.grdNavigatorSTOCK_DURATION.RowCount = ((DataTable)e.ReturnValue).Rows.Count;
        //}
    }
    //交易类型改变
    protected void ddlsType_TextChanged(object sender, EventArgs e)
    {
        Help.DropDownListDataBind(GetInOutAlloType(ddlsType.SelectedValue), this.dpdInOutType, Resources.Lang.Common_ALL, "TYPENAME", "TYPECODE", ""); 
    }
}