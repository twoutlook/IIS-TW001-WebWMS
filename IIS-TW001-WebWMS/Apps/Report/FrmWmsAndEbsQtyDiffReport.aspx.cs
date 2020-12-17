using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Data.Entity.SqlServer;
using System.Collections;
/// <summary>
/// 描述: 入库管理-->FrmINBILLList 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:57:39
/// </summary>
public partial class FrmWmsAndEbsQtyDiffReport :WMSBasePage
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
            txtDINDATEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            txtDINDATETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        btnExcel.GetExportToExcelSource3 += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler3(ToExcel);
    }


    #region IPageGrid 成员
    public IList ToExcel()
    {
        List<V_WmsAndEbsQtyDiffReport> list = new List<V_WmsAndEbsQtyDiffReport>();

        IGenericRepository<V_WmsAndEbsQtyDiffReport> con = new GenericRepository<V_WmsAndEbsQtyDiffReport>(context);
        var caseList = from p in con.Get()
                       orderby p.create_time descending
                       where 1 == 1
                       select p;
        if (txtWare.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.warehouseid) && x.warehouseid.Contains(txtWare.Text.Trim().ToUpper()));
        //if (txtCpositionCode.Text != string.Empty)
        //{
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cposition) && x.cposition.Contains(txtCpositionCode.Text.Trim().ToUpper()));
        //}
        if (txtCinvCode.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpartnumber) && x.cpartnumber.Contains(txtCinvCode.Text.Trim()));
        }
        if (txtDINDATEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.create_time != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.create_time) >= 0);
        if (txtDINDATETo.Text != string.Empty)
            caseList = caseList.Where(x => x.create_time != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.create_time) <= 0);
        if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
        {
            caseList = caseList.Where(x => x.cspecifications.ToString().Contains(txtcspec.Text.Trim()));
        }
        list = caseList.ToList();
        btnExcel.ExcelName = Resources.Lang.FrmWmsAndEbsQtyDiffReport_Msg4;//"WMS与ERP库存差异";
        return list;
    }
	
    public void GridBind()
    {
        IGenericRepository<V_WmsAndEbsQtyDiffReport> con = new GenericRepository<V_WmsAndEbsQtyDiffReport>(context);
        var caseList = from p in con.Get()
                       orderby p.create_time descending
                       where 1 == 1
                       select p;
        if (txtWare.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.warehouseid) && x.warehouseid.Contains(txtWare.Text.Trim().ToUpper()));
        //if (txtCpositionCode.Text != string.Empty)
        //{
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cposition) && x.cposition.Contains(txtCpositionCode.Text.Trim().ToUpper()));
        //}
        if (txtCinvCode.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpartnumber) && x.cpartnumber.Contains(txtCinvCode.Text.Trim()));
        }
       if (txtDINDATEFrom.Text != string.Empty)
           caseList = caseList.Where(x => x.create_time != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.create_time) >= 0);
        if (txtDINDATETo.Text != string.Empty)
            caseList = caseList.Where(x => x.create_time != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.create_time) <= 0);
        if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
        {
            caseList = caseList.Where(x => x.cspecifications.ToString().Contains(txtcspec.Text.Trim()));
        }
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.PageSize = this.PageSize;
        this.grdINBILL.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        this.grdINBILL.DataBind();
    }

  
    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
    }

    #endregion

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
  
 
    protected void grdINBILL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
         }

    }

    protected void dsGrdINBILL_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

  
}

