using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.Entity.SqlServer;


/// <summary>
/// 描述: 仓库管理-->FrmWAREHOUSEList 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-09-29 14:36:59
/// </summary>
/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/
public partial class BASE_FrmWAREHOUSEList :WMSBasePage
{

    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            // this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<V_BASE_WAREHOUSE> con = new GenericRepository<V_BASE_WAREHOUSE>(context);
        var caseList = from p in con.Get()
                       orderby p.createtime descending
                       where 1 == 1
                       select p;
        if (!string.IsNullOrEmpty(txtCreateTimeFrom.Text.Trim()))
            caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtCreateTimeFrom.Text.Trim(),x.createtime) >= 0 );
        if (txtCreateTimeTo.Text != string.Empty)
            caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtCreateTimeTo.Text.Trim(),x.createtime) <= 0 );
        if (txtCWAREID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwareid) && x.cwareid.Contains(txtCWAREID.Text.Trim()));
        if (txtCWARENAME.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwarename) && x.cwarename.Contains(txtCWARENAME.Text.Trim()));
        if (txtLEADERPHONE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.leaderphone) && x.leaderphone.Contains(txtLEADERPHONE.Text.Trim()));
        if (txtLEADERCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.leadercode) && x.leadercode.Contains(txtLEADERCODE.Text.Trim()));
        if (txtLEADER.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.leader) && x.leader.Contains(txtLEADER.Text.Trim()));
        if (ddlCSTATUS.SelectedValue != "")
            caseList = caseList.Where(x => x.cstatus.ToString().Equals(ddlCSTATUS.SelectedValue));
        if (drBONDED.SelectedValue != "")
            caseList = caseList.Where(x => x.bonded.ToString().Equals(drBONDED.SelectedValue));
        if (drCDEFINE1.SelectedValue != "")
            caseList = caseList.Where(x => x.cdefine1.ToString().Equals(drCDEFINE1.SelectedValue));
        if(!string.IsNullOrEmpty(ddlWareHouseType.SelectedValue))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cdefine2) && x.cdefine2.Equals(ddlWareHouseType.SelectedValue));
        AspNetPager1.RecordCount = caseList.Count();
        AspNetPager1.PageSize = this.PageSize;

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("cdefine2", "WareHouseType"));//仓库类型
        flagList.Add(new Tuple<string, string>("bonded", "YorN"));//保税仓
        flagList.Add(new Tuple<string, string>("cdefine1", "YorN"));//良品仓
        flagList.Add(new Tuple<string, string>("cstatus", "BASE_WAREHOUSE.CSTATUS"));//状态

        var srcdata = GetGridSourceDataByList(data, flagList);

        this.grdWAREHOUSE.DataSource = srcdata;
        this.grdWAREHOUSE.DataBind();
    }
    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";

        Help.DropDownListDataBind(GetParametersByFlagType("WareHouseType"), ddlWareHouseType, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//仓库类型
        Help.DropDownListDataBind(GetParametersByFlagType("YorN"), drBONDED, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//保税仓
        Help.DropDownListDataBind(GetParametersByFlagType("YorN"), drCDEFINE1, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//良品仓
        Help.DropDownListDataBind(GetParametersByFlagType("BASE_WAREHOUSE.CSTATUS"), ddlCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
        
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWin('" + WMSBasePage.BuildRequestPageURL("FrmWAREHOUSEEdit.aspx?Flag=0", SYSOperation.New, "") + "','"+Resources.Lang.FrmWAREHOUSEList_Msg01+"','WAREHOUSE',800,600);return false;";//新建仓库管理
    }

    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;//默认第一页
            AspNetPager1.CurrentPageIndex = 1;
        }
        IsFirstPage = true;
        this.GridBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //DBUtil.BeginTrans();

        //try
        //{
        //    for (int i = 0; i < this.grdWAREHOUSE.Rows.Count; i++)
        //    {
        //        if (this.grdWAREHOUSE.Rows[i].Cells[0].Controls[1] is CheckBox)
        //        {
        //            CheckBox chkSelect = (CheckBox)this.grdWAREHOUSE.Rows[i].Cells[0].Controls[1];

        //            if (chkSelect.Checked)
        //            {
        //                WAREHOUSEEntity entity = new WAREHOUSEEntity();

        //                //主键赋值
        //                entity.ID = this.grdWAREHOUSE.DataKeys[i].Values[0].ToString();
        //                //执行动作
        //                WAREHOUSERule.Delete(entity);

        //                //DBUtil.ExecuteNonQuery("delete from BASE_WAREHOUSE where id='" + this.grdWAREHOUSE.DataKeys[i].Values[0].ToString() + "'");
        //            }
        //        }
        //    }
        //      this.Alert(Resources.Lang.Common_SuccessDel); //删除成功!         
        //    DBUtil.Commit();
        //    this.GridBind();
        //}
        //catch (Exception E)
        //{
        //    this.Alert(Resources.Lang.Common_FailDel + E.Message.ToJsString()); //删除失败！
        //    DBUtil.Rollback();
        //}
    }

    protected void grdWAREHOUSE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = grdWAREHOUSE.DataKeys[e.Row.RowIndex].Values[0].ToString();
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmWAREHOUSEEdit.aspx?Flag=1",SYSOperation.Modify, strKeyID), "", "WAREHOUSE", 800, 600);

        }
    }

    protected void grdWAREHOUSE_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortName = e.SortExpression;
        if (SortedField.Equals(sortName))
        {
            if (SortedAD.Equals(Ascending))
            {
                SortedAD = Descending;//取反
            }
            else
            {
                SortedAD = Ascending;
            }
        }
        else
        {
            SortedField = sortName;
            SortedAD = Ascending;
        }
        GridBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

}

