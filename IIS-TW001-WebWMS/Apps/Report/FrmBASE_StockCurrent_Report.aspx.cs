using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Collections;

public partial class Apps_Report_FrmBASE_StockCurrent_Report : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Help.DropDownListDataBind(GetParametersByFlagType("YunSuanFu"), ddlTJ, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
            // this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        btnExcel.GetExportToExcelSource3 += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler3(ToExcel);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (CheckData())
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
            this.GridBind();
        }
    }

    public IList ToExcel()
    {
        List<V_StockDiffTime_Report> list = new List<V_StockDiffTime_Report>();
        // this.CurrendIndex = 1;
        IGenericRepository<V_StockDiffTime_Report> entity = new GenericRepository<V_StockDiffTime_Report>(context);
        var caseList = from p in entity.Get()
                       orderby p.INdate descending
                       where p.diffDays > 0
                       select p;

        if (!string.IsNullOrEmpty(txtTime.Text))
        {
            if (!string.IsNullOrEmpty(ddlTJ.SelectedValue))
            {
                var diffTime = Convert.ToInt32(txtTime.Text.Trim());
                if (ddlTJ.SelectedValue.Equals(">"))
                {
                    caseList = caseList.Where(x => x.diffDays != null && x.diffDays > diffTime); ;
                }
                else if (ddlTJ.SelectedValue.Equals("="))
                {
                    caseList = caseList.Where(x => x.diffDays != null && x.diffDays == diffTime);
                }
                else
                {
                    caseList = caseList.Where(x => x.diffDays != null && x.diffDays < diffTime);
                }
            }
            else
            {

                caseList = caseList.Where(x => x.diffDays != null && x.diffDays.ToString().Contains(txtTime.Text.Trim()));
            }
        }

        if (!string.IsNullOrEmpty(txtWAREHOUSE.Text))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwarehousecode) && x.cwarehousecode.Contains(txtWAREHOUSE.Text.Trim()));


        if (!string.IsNullOrEmpty(txtCARGOSPACE.Text))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCARGOSPACE.Text.Trim()));


        if (!string.IsNullOrEmpty(txtPART.Text))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtPART.Text.Trim()));

        list = caseList.ToList();
        btnExcel.ExcelName = Resources.Lang.FrmBASE_StockCurrent_Report_MSG1;//"库龄报表";
        return list;
    }

    public bool CheckData()
    {
        if (string.IsNullOrEmpty(txtWAREHOUSE.Text.Trim()))
        {
            //Alert("仓库项不能为空！");
            Alert(Resources.Lang.FrmBASE_StockCurrent_Report_MSG9 + "！");
            txtWAREHOUSE.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtTime.Text.Trim()))
        {
            //Alert("库龄项不能为空！");
            Alert(Resources.Lang.FrmBASE_StockCurrent_Report_MSG10 + "！");
            txtTime.Focus();
            return false;
        }
        return true;
    }
    public void GridBind()
    {
        // this.CurrendIndex = 1;
        IGenericRepository<V_StockDiffTime_Report> entity = new GenericRepository<V_StockDiffTime_Report>(context);
        var caseList = from p in entity.Get()
                       orderby p.INdate descending
                       where p.diffDays > 0
                       select p;

        if (!string.IsNullOrEmpty(txtTime.Text))
        {
            if (!string.IsNullOrEmpty(ddlTJ.SelectedValue))
            {
                var diffTime = Convert.ToInt32(txtTime.Text.Trim());
                if (ddlTJ.SelectedValue.Equals(">"))
                {
                    caseList = caseList.Where(x => x.diffDays != null && x.diffDays > diffTime); ;
                }
                else if (ddlTJ.SelectedValue.Equals("="))
                {
                    caseList = caseList.Where(x => x.diffDays != null && x.diffDays == diffTime);
                }
                else
                {
                    caseList = caseList.Where(x => x.diffDays != null && x.diffDays < diffTime);
                }
            }
            else
            {

                caseList = caseList.Where(x => x.diffDays != null && x.diffDays.ToString().Contains(txtTime.Text.Trim()));
            }
        }

        if (!string.IsNullOrEmpty(txtWAREHOUSE.Text))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwarehousecode) && x.cwarehousecode.Contains(txtWAREHOUSE.Text.Trim()));


        if (!string.IsNullOrEmpty(txtCARGOSPACE.Text))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCARGOSPACE.Text.Trim()));


        if (!string.IsNullOrEmpty(txtPART.Text))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtPART.Text.Trim()));


        //if (caseList != null && caseList.Count() > 0)
        //{
        //    AspNetPager1.RecordCount = caseList.Count();
        //    AspNetPager1.PageSize = this.PageSize;
        //}

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
            AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
            grdINASN.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
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

    protected void grdINASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }

    }
}