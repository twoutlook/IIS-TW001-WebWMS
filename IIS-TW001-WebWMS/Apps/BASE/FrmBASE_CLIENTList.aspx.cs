using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;

using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business.Base;
using System.Data.Entity.SqlServer;

/// <summary>
/// 描述: 客户管理-->FrmBASE_CLIENTList 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-09-29 15:45:49
/// </summary> 
/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/
public partial class BASE_FrmBASE_CLIENTList : WMSBasePage//PageBase, IPageGrid
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }

    #region SQL
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //this.grdBASE_CLIENT.DataKeyNames = new string[]{"ID"};
        //本页面打开新增窗口        
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBASE_CLIENTEdit.aspx?Flag=0", SYSOperation.New, "") + "','" + Resources.Lang.FrmBASE_CLIENTList_Msg01 + "','BASE_CLIENT',800,600);return false;";//新建客户管理

        Help.DropDownListDataBind(GetParametersByFlagType("BASE_CLIENT"), ddlCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
    }
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
    public void GridBind()
    {
        IGenericRepository<V_BASE_CLIENT> entity = new GenericRepository<V_BASE_CLIENT>(context);
        var caseList = from p in entity.Get()
                       orderby p.createtime descending
                       where 1 == 1
                       select p;
        if (!string.IsNullOrEmpty(txtCreateTimeFrom.Text.Trim()))
            caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtCreateTimeFrom.Text.Trim(), x.createtime) >= 0);
        if (txtCreateTimeTo.Text != string.Empty)
            caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtCreateTimeTo.Text.Trim(), x.createtime) <= 0);
        if (txtCCLIENTID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cclientid) && x.cclientid.Contains(txtCCLIENTID.Text.Trim()));
        if (txtCCLIENTNAME.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cclientname) && x.cclientname.Contains(txtCCLIENTNAME.Text.Trim()));
        if (ddlCSTATUS.SelectedValue != "")
            caseList = caseList.Where(x => x.cstatus.ToString().Equals(ddlCSTATUS.SelectedValue));
        if (txtCALIAS.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.calias) && x.calias.Contains(txtCALIAS.Text.Trim()));
        if (txtCERPCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtCERPCODE.Text.Trim()));
        if (txtILEVER.Text != string.Empty)
            caseList = caseList.Where(x => x.ilever.ToString().Equals(txtILEVER.Text.Trim()));
        if (txtCCONTACTPERSON.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccontactperson) && x.ccontactperson.Contains(txtCCONTACTPERSON.Text.Trim()));
        if (txtCPHONE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cphone) && x.cphone.Contains(txtCPHONE.Text.Trim()));
        if (txtCTYPE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ctype) && x.ctype.Contains(txtCTYPE.Text.Trim()));
        AspNetPager1.RecordCount = caseList.Count();
        AspNetPager1.PageSize = this.PageSize;
        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        var srcdata = GetGridSourceDataByList(data, "cstatus", "BASE_CLIENT");

        grdBASE_CLIENT.DataSource = srcdata;
        grdBASE_CLIENT.DataBind();

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int count = 0;
        IGenericRepository<BASE_CLIENT> con = new GenericRepository<BASE_CLIENT>(context);
        BaseCommQuery bcq = new BaseCommQuery();
        try
        {
            for (int i = 0; i < this.grdBASE_CLIENT.Rows.Count; i++)
            {
                if (this.grdBASE_CLIENT.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CLIENT.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {

                        string ids = this.grdBASE_CLIENT.DataKeys[i].Values[0].ToString();
                        var msg = bcq.CheckDelCondition(ids, BaseCommType.BASE_CLIENT);
                        if (msg.ToUpper().Equals("OK"))
                        {
                            con.Delete(ids);
                            con.Save();
                            count++;
                        }
                        else
                        {
                            this.Alert(msg);
                            break;
                        }
                    }
                }
            }
            if (count > 0)
            {
                this.Alert(Resources.Lang.Common_SuccessDel); //删除成功!
                this.GridBind();
            }
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.Common_FailDel + E.Message.ToJsString()); //删除失败！
        }
    }

    protected void grdBASE_CLIENT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = grdBASE_CLIENT.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            //this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBASE_CLIENTEdit.aspx", SysOperation.Modify, strKeyID), "客户管理", "BASE_CLIENT", 800, 600);
            //this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBASE_CLIENTEdit.aspx?Flag=1&ids=" + strKeyID, SYSOperation.Modify, strKeyID), "客户管理", "BASE_CLIENT");
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBASE_CLIENTEdit.aspx?Flag=1&ids=" + strKeyID + "", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBASE_CLIENTList_Title01, "BASE_CLIENT"); //客户管理


        }

    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void grdBASE_CLIENT_Sorting(object sender, GridViewSortEventArgs e)
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
    #endregion


    protected void BtnNew_Click(object sender, EventArgs e)
    {
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdBASE_CLIENT.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBASE_CLIENT.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void dsGrdBASE_CLIENT_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }
}

