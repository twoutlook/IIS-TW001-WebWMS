using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_FrmBASE_MappingList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdMappingList.DataKeyNames = new string[] { "ID" };

        drpType.Items.Clear();
        drpType.Items.Add(new ListItem(Resources.Lang.Common_ALL, ""));

        var types = context.BASE_TypeMapping.Where(x => x.CStatus == "0").GroupBy(x => x.type).Select(x => new { TypeItem = x.FirstOrDefault().type}).ToList();
        if (types != null && types.Any()) {
            foreach (var item in types) {
                drpType.Items.Add(new ListItem(item.TypeItem, item.TypeItem));
            }
        }

        Help.DropDownListDataBind(GetParametersByFlagType("EnableOrForbidden"), drpStatus, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态

    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        this.GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }

    public void GridBind()
    {
        IGenericRepository<BASE_TypeMapping> vcon = new GenericRepository<BASE_TypeMapping>(context);
        var caseList = from p in vcon.Get()
                       orderby p.dcreatetime descending
                       where 1 == 1
                       select p;
        if (drpType.SelectedValue != "")
        {
            caseList = caseList.Where(x => x.type.ToString().Equals(drpType.SelectedValue));
        }
        if (!string.IsNullOrEmpty(txtWMS.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.WMS_TypeCode) && x.WMS_TypeCode.Contains(txtWMS.Text.Trim()));
        if (!string.IsNullOrEmpty(txtERP.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ERP_TypeCode) && x.ERP_TypeCode.Contains(txtERP.Text.Trim()));
        if (!string.IsNullOrEmpty(drpStatus.SelectedValue))
        {
            caseList = caseList.Where(x => x.CStatus.Equals(drpStatus.SelectedValue));
        }

        AspNetPager1.RecordCount = caseList.Count();
        AspNetPager1.PageSize = this.PageSize;

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        var srcdata = GetGridSourceDataByList(data, "CStatus", "EnableOrForbidden");

        grdMappingList.DataSource = srcdata;
        grdMappingList.DataBind();
    }

    protected void grdMappingList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = grdMappingList.DataKeys[e.Row.RowIndex].Values[0].ToString();

            string desc = e.Row.Cells[7].Text;
            
        }
    }
}