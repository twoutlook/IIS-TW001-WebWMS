using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.Entity.SqlServer;

public partial class FrmSTOCK_PalletCodeList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            this.InitPage();
        }
    }

    #region IPageGrid 成员
    public void GridBind()
    {
        IGenericRepository<Stock_CURRENT_PalletCode> entity = new GenericRepository<Stock_CURRENT_PalletCode>(context);
        var caseList = from p in entity.Get()
                       orderby p.dCreateDate descending
                       where 1 == 1
                       select p;

        if (!this.txtPalledCode.Text.IsNullOrEmpty())
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.PalletCode) && x.PalletCode.Contains(txtPalledCode.Text.Trim()));

        //if (!txtCPocition.Text.IsNullOrEmpty())
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCPocition.Text.Trim())); 

        if (!txtCPocition.Text.IsNullOrEmpty())
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.AGVSiteCode) && x.AGVSiteCode.Contains(txtCPocition.Text.Trim()));

        if (!txtCinvcode.Text.IsNullOrEmpty())
            caseList = caseList.Where(x => x.Cinvcode.ToString().Equals(txtCinvcode.Text));

        if (!txtCCREATEOWNERCODE.Text.IsNullOrEmpty())
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.dCreateOwner) && x.dCreateOwner.Contains(txtCCREATEOWNERCODE.Text.Trim()));

        if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
            caseList = caseList.Where(x => x.dCreateDate != null && SqlFunctions.DateDiff("dd", txtDCREATETIMEFrom.Text.Trim(), x.dCreateDate) >= 0);

        if (txtDCREATETIMETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dCreateDate != null && SqlFunctions.DateDiff("dd", txtDCREATETIMETo.Text.Trim(), x.dCreateDate) <= 0);

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
            AspNetPager1.RecordCount = 0;
        AspNetPager1.CustomInfoHTML = " " + Resources.Lang.WMS_Common_Pager_PageCount + ":<b>" + "</b>";//总页数
        grdINASN.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdINASN.DataBind();
    }
    #endregion

    #region IPage 成员
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        //this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //this.grdINASN.DataKeyNames = new string[] { "ID,errorLogCount" };
    }
    #endregion

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            CurrendIndex = 1;
            //AspNetPager1.CurrentPageIndex = 1;
            this.GridBind();
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }
    protected void grdINASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    /// <summary>
    /// 获取入库类型名称
    /// </summary>
    /// <param name="iType"></param>
    /// <returns></returns>
    public static string GetInTypeName(string iType)
    {
        string strSQL = string.Format(@"SELECT A.TYPENAME FROM INTYPE A WHERE A.CERPCODE = '{0}'", iType);
        return DBHelp.ExecuteScalar(strSQL).ToString();
    }

    public bool CheckData()
    {
        return false;
    }
}