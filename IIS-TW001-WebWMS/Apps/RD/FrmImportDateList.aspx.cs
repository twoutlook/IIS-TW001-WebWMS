using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.Entity.SqlServer;

/// <summary>
/// 描述: 入库管理-->FrmINASNList 页面后台类文件
/// 作者: -wjw
/// 创建于: 2012-12-14 18:50:50
/// </summary>
public partial class RD_FrmImportDateList :WMSBasePage// WebBasePage, IPageGrid
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
            txtCreateFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            txtCreateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        if (this.Operation() == SYSOperation.Modify)
        {
            BindSuccessBind();
        }
    }


    #region IPageGrid 成员

    public void GridBind()
    {
        //In_Mo_InfoListQuery listQuery = new In_Mo_InfoListQuery();
        //DataTable dtSource = listQuery.GetList(txtWO.Text, txtDateFrom.Text, txtDateTo.Text, txtCreateFrom.Text, txtCreateTo.Text, txtShift.Text, txtSpecial.Text, txtLine.Text, txtDepartmentno.Text, ddlIsCountJobTime.SelectedValue, txtOnLineTimeBeg.Text, txtOnLineTimeEnd.Text, false, this.grdNavigatorINASN.CurrentPageIndex, this.grdINASN.PageSize);
        //this.grdINASN.DataSource = dtSource;
        //this.grdINASN.DataBind();
        IGenericRepository<V_ImportDateList> entity = new GenericRepository<V_ImportDateList>(context);
        var caseList = from p in entity.Get()
                       orderby p.CREATE_TIME descending
                       where 1 == 1
                       select p;
        if (txtWO.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.WO) && x.WO.Contains(txtWO.Text));
        if (!string.IsNullOrEmpty(txtDateFrom.Text.Trim()))
            caseList = caseList.Where(x => x.START_DATE != null && SqlFunctions.DateDiff("dd", txtDateFrom.Text.Trim(),x.START_DATE) >= 0 );
        if (txtDateTo.Text != string.Empty)
            caseList = caseList.Where(x => x.START_DATE != null && SqlFunctions.DateDiff("dd", txtDateTo.Text.Trim(),x.START_DATE) <= 0 );
        if (!string.IsNullOrEmpty(txtCreateFrom.Text.Trim()))
            caseList = caseList.Where(x => x.CREATE_TIME != null && SqlFunctions.DateDiff("dd", txtCreateFrom.Text.Trim(),x.CREATE_TIME) >= 0);
        if (txtCreateTo.Text != string.Empty)
            caseList = caseList.Where(x => x.CREATE_TIME != null && SqlFunctions.DateDiff("dd", txtCreateTo.Text.Trim(),x.CREATE_TIME) <= 0 );
        if (txtShift.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.SHIFT) && x.SHIFT.Contains(txtShift.Text));
        }
        if (txtSpecial.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.SPECIAL) && x.SPECIAL.Contains(txtSpecial.Text));
        if (ddlIsCountJobTime.SelectedValue != "")
            caseList = caseList.Where(x => x.ISCOUNTJOBTIME.ToString().Equals(ddlIsCountJobTime.SelectedValue));
        if (txtLine.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.LINEBODY) && x.LINEBODY.Contains(txtLine.Text.Trim()));
        if (txtDepartmentno.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.DEPARTMENTNO) && x.DEPARTMENTNO.Contains(txtDepartmentno.Text.Trim()));
        if (!string.IsNullOrEmpty(txtOnLineTimeBeg.Text.Trim()))
            caseList = caseList.Where(x => x.ONLINETIME != null && SqlFunctions.DateDiff("dd", txtOnLineTimeBeg.Text.Trim(),x.ONLINETIME) >= 0 );
        if (txtOnLineTimeEnd.Text != string.Empty)
            caseList = caseList.Where(x => x.ONLINETIME != null && SqlFunctions.DateDiff("dd", txtOnLineTimeEnd.Text.Trim(),x.ONLINETIME) <= 0 );
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
        grdINASN.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdINASN.DataBind();
    }
    public void BindSuccessBind()
    {
        //In_Mo_InfoListQuery listQuery = new In_Mo_InfoListQuery();
        //DataTable dtSource = listQuery.GetList(txtWO.Text, txtDateFrom.Text, txtDateTo.Text, txtCreateFrom.Text, txtCreateTo.Text, txtShift.Text, txtSpecial.Text, txtLine.Text, txtDepartmentno.Text, ddlIsCountJobTime.SelectedValue, txtOnLineTimeBeg.Text, txtOnLineTimeEnd.Text, false, this.grdNavigatorINASN.CurrentPageIndex, this.grdINASN.PageSize);
        //this.grdINASN.DataSource = dtSource;
        //this.grdINASN.DataBind();
        IGenericRepository<TEMP_IN_MO_INFO> entity = new GenericRepository<TEMP_IN_MO_INFO>(context);
        var caseList = from p in entity.Get()
                       orderby p.create_time descending
                       where 1 == 1&&p.issave ==null
                       select p;
        if (txtWO.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.wo) && x.wo.Contains(txtWO.Text));
        if (!string.IsNullOrEmpty(txtDateFrom.Text.Trim()))
            caseList = caseList.Where(x => x.start_date != null && SqlFunctions.DateDiff("dd", txtDateFrom.Text.Trim(),x.start_date) >= 0 );
        if (txtDateTo.Text != string.Empty)
            caseList = caseList.Where(x => x.start_date != null  && SqlFunctions.DateDiff("dd", txtDateTo.Text.Trim(),x.start_date) <= 0 );
        if (!string.IsNullOrEmpty(txtCreateFrom.Text.Trim()))
            caseList = caseList.Where(x => x.create_time != null && SqlFunctions.DateDiff("dd", txtCreateFrom.Text.Trim(),x.create_time) >= 0  );
        if (txtCreateTo.Text != string.Empty)
            caseList = caseList.Where(x => x.create_time != null && SqlFunctions.DateDiff("dd", txtCreateTo.Text.Trim(),x.create_time) <= 0 );
        if (txtShift.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.shift) && x.shift.Contains(txtShift.Text));
        }
        if (txtSpecial.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.special) && x.special.Contains(txtSpecial.Text));
        if (ddlIsCountJobTime.SelectedValue != "")
            caseList = caseList.Where(x => x.iscountjobtime.ToString().Equals(ddlIsCountJobTime.SelectedValue));
        if (txtLine.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.linebody) && x.linebody.Contains(txtLine.Text.Trim()));
        if (txtDepartmentno.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.departmentno) && x.departmentno.Contains(txtDepartmentno.Text.Trim()));
        if (!string.IsNullOrEmpty(txtOnLineTimeBeg.Text.Trim()))
            caseList = caseList.Where(x => x.onlinetime != null && SqlFunctions.DateDiff("dd", txtOnLineTimeBeg.Text.Trim(),x.onlinetime) >= 0 );
        if (txtOnLineTimeEnd.Text != string.Empty)
            caseList = caseList.Where(x => x.onlinetime != null && SqlFunctions.DateDiff("dd", txtOnLineTimeEnd.Text.Trim(),x.onlinetime) <= 0 );
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
        grdINASN.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdINASN.DataBind();
    }
    public bool CheckData()
    {

        return true;

    }
    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //this.grdINASN.DataKeyNames = new string[]{"ID"};
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        //上传工单资料
        this.btnUp.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmUPGDDatat.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmImportDateList_MSG1+ "','UPGD');return false;";


        // this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WebBasePage.BuildRequestPageURL("FrmINASNEdit.aspx", SYSOperation.New, "") + "','新建入库通知单','INASN');return false;";
        //在新窗口打开的代码： this.OpenWin(btnNew,WebBasePage.BuildRequestPageURL("FrmINASNEdit.aspx", SYSOperation.New,""),800,600);
        //WebUtil.LoadDropDownList(this.txtCSTATUS, SysParameterListQuery.GetList("", "", "IS", false, -1, -1), "FLAG_NAME", "FLAG_ID", true);
        //WebUtil.LoadDropDownList(this.txtITYPE, SysParameterListQuery.GetList("", "", "IT", false, -1, -1), "FLAG_NAME", "FLAG_ID", true);
        //Help.DropDownListDataBind(SysParameterListQuery.GetList("", "", "IS", false, -1, -1), this.txtCSTATUS, "全部", "FLAG_NAME", "FLAG_ID", "");
        //Help.DropDownListDataBind(new InTypeQuery().GetInTypeAll(), this.txtITYPE, "全部", "TYPENAME", "CERPCODE", "");

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
    #endregion
    private bool CheckDataInput()
    {

        if (this.txtDateFrom.Text.Trim().Length > 0)
        {
            if (this.txtDateFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmImportDateList_MSG2 + "！");//制单日期起始项不是有效的日期
                this.SetFocus(txtDateFrom);
                return false;
            }
            else
            {
                if (this.txtDateTo.Text.Trim() == "")
                {
                    this.Alert(Resources.Lang.FrmImportDateList_MSG3 + "！");//到项不允许空
                    this.SetFocus(txtDateTo);
                    return false;
                }
                if (this.txtDateTo.Text.Trim().Length > 0)
                {
                    if (this.txtDateTo.Text.IsDate() == false)
                    {
                        this.Alert(Resources.Lang.FrmImportDateList_MSG4 + "！");//到项不是有效的日期
                        this.SetFocus(txtDateTo);
                        return false;
                    }
                }
            }
        }
        if (this.txtCreateFrom.Text.Trim().Length > 0)
        {
            if (this.txtCreateFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmImportDateList_MSG5+ "！");//创建日期起始项不是有效的日期
                this.SetFocus(txtCreateFrom);
                return false;
            }
            else
            {
                if (this.txtCreateTo.Text.Trim() == "")
                {
                    this.Alert(Resources.Lang.FrmImportDateList_MSG3 + "！");//到项不允许空
                    this.SetFocus(txtCreateTo);
                    return false;
                }
                if (this.txtCreateTo.Text.Trim().Length > 0)
                {
                    if (this.txtCreateTo.Text.IsDate() == false)
                    {
                        this.Alert(Resources.Lang.FrmImportDateList_MSG6 + "！");//创建日期到项不是有效的日期
                        this.SetFocus(txtCreateTo);
                        return false;
                    }
                }
            }
        }
        return true;

    }
  
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        //  ltMsg.Text = "";

    }



    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdINASN.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdINASN.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdINASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
        //    HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
        //    linkModify.NavigateUrl = "#";
        //    this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmINASNEdit.aspx", SYSOperation.Modify, strKeyID), "入库通知单", "INASN");

        //    string statusStr = string.Empty;
        //    switch (e.Row.Cells[e.Row.Cells.Count - 2].Text)
        //    {
        //        //0 未处理,1,已指引，2,上架中，3 已完成,）
        //        case "0":
        //            statusStr = "未处理";
        //            break;
        //        case "1":
        //            statusStr = "已指引";
        //            break;
        //        case "2":
        //            statusStr = "上架中";
        //            break;
        //        case "3":
        //            statusStr = "已完成";
        //            break;
        //    }
        //    e.Row.Cells[e.Row.Cells.Count - 2].Text = statusStr;
        //    e.Row.Cells[e.Row.Cells.Count - 7].Text = e.Row.Cells[e.Row.Cells.Count - 7].Text == "Y" ? "是" : "否";
        //}

    }

    protected void dsGrdINASN_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdINASN_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.ReturnValue is DataTable)
        {
            //if(grdNavigatorINASN.IsDbPager == false)
            //    this.grdNavigatorINASN.RowCount = ((DataTable)e.ReturnValue).Rows.Count;
        }

    }




    protected void btnNew_Click(object sender, EventArgs e)
    {

    }
    #endregion
   
}