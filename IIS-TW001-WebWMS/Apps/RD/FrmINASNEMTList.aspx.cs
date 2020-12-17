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

public partial class Apps_RD_FrmINASNEMTList : WMSBasePage
{
    //DBContext context = new DBContext();
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
        IGenericRepository<V_InAsnEmt> entity = new GenericRepository<V_InAsnEmt>(context);
        var caseList = from p in entity.Get()
                       orderby p.ddefine3 descending
                       where 1 == 1
                       select p;
                       //select new
                       //{
                       //    p.ids,
                       //    p.cinvbarcode,
                       //    p.id,
                       //    p.cerpcode,
                       //    p.itype,
                       //    p.cerpcodeline,
                       //    p.cinvcode,
                       //    p.iquantity,
                       //    p.cstatus,
                       //    p.cdefine2,
                       //    p.ddefine3,
                       //    p.cticketcode
                       //};
        if (!this.txtSNCode.Text.IsNullOrEmpty())
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvbarcode) && x.cinvbarcode.Contains(txtSNCode.Text.Trim()));
        if (!txtPalledCode.Text.IsNullOrEmpty())
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.PalletCode) && x.PalletCode.Contains(txtPalledCode.Text.Trim()));

        if (!txtCTICKETCODE.Text.IsNullOrEmpty())
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtCTICKETCODE.Text.Trim()));
        if(!txtERP_No.Text.IsNullOrEmpty())
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtERP_No.Text.Trim()));
        if(ddlITYPE.SelectedValue != "")
            caseList = caseList.Where(x =>!string.IsNullOrEmpty(x.itype)&& x.itype.Equals(ddlITYPE.SelectedValue));

        caseList = this.ddlWorkType.SelectedValue != "" ? 
                   caseList.Where(x => !string.IsNullOrEmpty(x.WORKTYPE) && x.WORKTYPE.Equals(ddlWorkType.SelectedValue)) : caseList;

        if (!txtCinvcode.Text.IsNullOrEmpty())
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));
        if (ddlCSTATUS.SelectedValue != "")
            caseList = caseList.Where(x => x.cstatus.ToString().Equals(ddlCSTATUS.SelectedValue));
        if (!txtCCREATEOWNERCODE.Text.IsNullOrEmpty())
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cdefine2) && x.cdefine2.Contains(txtCCREATEOWNERCODE.Text.Trim()));
        if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
            caseList = caseList.Where(x => x.ddefine3 != null && SqlFunctions.DateDiff("dd", txtDCREATETIMEFrom.Text.Trim(),x.ddefine3) >= 0);
        if (txtDCREATETIMETo.Text != string.Empty)
            caseList = caseList.Where(x => x.ddefine3 != null && SqlFunctions.DateDiff("dd", txtDCREATETIMETo.Text.Trim(), x.ddefine3) <= 0 );

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
            AspNetPager1.RecordCount = 0;
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("ITYPE", "INTYPE"));//入库类型
        flagList.Add(new Tuple<string, string>("WORKTYPE", "WorkType"));//作业方式
        flagList.Add(new Tuple<string, string>("CSTATUS", "EMT"));//状态
        var listResult = GetPageSize(caseList, PageSize, CurrendIndex).ToList();     
        var source = GetGridSourceDataByList(listResult, flagList);
        grdINASN.DataSource = source;
        grdINASN.DataBind();       
    }
    #endregion
    
    #region IPage 成员
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        //this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //this.grdINASN.DataKeyNames = new string[] { "ID,errorLogCount" };

        //本页面打开新增窗口
        Help.DropDownListDataBind(GetParametersByFlagType("EMT"), this.ddlCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(GetInType(true), this.ddlITYPE, Resources.Lang.Common_ALL, "FUNCNAME", "EXTEND1", "");
        Help.DropDownListDataBind(GetParametersByFlagType("WorkType"), this.ddlWorkType, Resources.Lang.Common_ALL, "flag_name", "flag_id", "");
        //MonthOrWeek
        Help.RadioButtonDataBind(GetParametersByFlagType("MonthOrWeek"), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");
        if (SYSConfig == "1")//箱模式
        {
            grdINASN.Columns[3].HeaderText = CurrentConfigUnitName;
            grdINASN.Columns[2].Visible = false;
        }
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
            AspNetPager1.CurrentPageIndex = 1;
            this.GridBind();
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }
    protected void grdINASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
            }
        }
        catch (Exception)
        {
        }
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