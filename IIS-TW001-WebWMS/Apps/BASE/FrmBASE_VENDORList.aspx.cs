using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Business.Base;
using System.Data.Entity.SqlServer;

/// <summary>
/// 描述: -->FrmBASE_VENDORList 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-10-08 14:27:19
/// </summary>
/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/
public partial class BASE_FrmBASE_VENDORList : WMSBasePage // PageBase, IPageGrid
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }

    }


    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<V_BASE_VENDOR> entity = new GenericRepository<V_BASE_VENDOR>(context);
        var caseList = from p in entity.Get()
                       orderby p.createtime descending
                       where 1 == 1
                       select p;

        if (!string.IsNullOrEmpty(txtCreateTimeFrom.Text.Trim()))
            caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtCreateTimeFrom.Text.Trim(), x.createtime) >= 0);
        if (txtCreateTimeTo.Text != string.Empty)
            caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtCreateTimeTo.Text.Trim(), x.createtime) <= 0);
        if (txtCVENDORID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cvendorid) && x.cvendorid.Contains(txtCVENDORID.Text.Trim()));
        if (txtCVENDOR.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cvendor) && x.cvendor.Contains(txtCVENDOR.Text.Trim()));

        if (txtCTNPE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ctnpe) && x.ctnpe.Contains(txtCTNPE.Text.Trim()));

        if (txtCALIAS.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.calias) && x.calias.Contains(txtCALIAS.Text.Trim()));
        if (txtCERPCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtCERPCODE.Text.Trim()));

        if (txtCCONTACTPERSON.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccontactperson) && x.ccontactperson.Contains(txtCCONTACTPERSON.Text.Trim()));

        if (txtILEVEL.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ilevel.ToString()) && x.ilevel.ToString().Contains(txtILEVEL.Text.Trim()));
        if (drCSTATUS.SelectedValue != "")
            caseList = caseList.Where(x => x.cstatus.ToString().Equals(drCSTATUS.SelectedValue));
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        var srcdata = GetGridSourceDataByList(data, "CSTATUS", "BASE_CLIENT");
        grdBASE_VENDOR.DataSource = srcdata;
        grdBASE_VENDOR.DataBind();

    }

    public bool CheckData()
    {
        if (this.txtCVENDORID.Text.Trim().Length > 0)
        {
        }
        if (this.txtCVENDOR.Text.Trim().Length > 0)
        {
        }
        if (this.txtCTNPE.Text.Trim().Length > 0)
        {
        }
        if (this.txtCALIAS.Text.Trim().Length > 0)
        {
        }
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtCCONTACTPERSON.Text.Trim().Length > 0)
        {
        }
        //if(this.txtCSTATUS.Text.Trim().Length > 0)
        //{
        //}
        if (this.txtILEVEL.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDecimal(this.txtILEVEL.Text) == false)
            {
                this.Alert(Resources.Lang.FrmBASE_CLIENTEdit_Msg13);//级别项不是有效的十进制数字！
                this.SetFocus(txtILEVEL);
                return false;
            }
        }
        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdBASE_VENDOR.DataKeyNames = new string[] { "IDS" };
        this.btnNew.Attributes["onclick"] = "PopupFloatWin('" + WMSBasePage.BuildRequestPageURL("FrmBASE_VENDOREdit.aspx?Flag=0", SYSOperation.New, "") + "','" + Resources.Lang.Common_Add + "','BASE_VENDOR',800,600);return false;";//新建

        Help.DropDownListDataBind(GetParametersByFlagType("BASE_CLIENT"), drCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
    }

    #endregion

    //protected DataTable grdNavigatorBASE_VENDOR_GetExportToExcelSource()
    //{
    //    BASE_FrmBASE_VENDORListQuery listQuery = new BASE_FrmBASE_VENDORListQuery();
    //    DataTable dtSource = listQuery.GetList(txtCreateTimeFrom.Text, txtCreateTimeTo.Text, txtCVENDORID.Text, txtCVENDOR.Text, txtCTNPE.Text, txtCALIAS.Text, txtCERPCODE.Text, txtCCONTACTPERSON.Text, drCSTATUS.SelectedValue, txtILEVEL.Text, false, -1, -1);
    //    //将状态修改正确 20130605103658
    //    var parameterValue = new Dictionary<string, string> { { "BASE_VENDOR.CSTATUS", "CSTATUS" } };
    //    var dtOutPut = CommonFunction.ModSattus(parameterValue, dtSource);
    //    return dtOutPut;
    //}

    protected void grdBASE_VENDOR_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorBASE_VENDOR.IsDbPager)
        //{
        //    grdNavigatorBASE_VENDOR.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdBASE_VENDOR.PageIndex = e.NewPageIndex;
        //}
    }
    protected void grdBASE_VENDOR_PageIndexChanged(object sender, EventArgs e)
    {
        //if(grdNavigatorBASE_VENDOR.IsDbPager)
        {
            this.GridBind();
        }
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
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorBASE_VENDOR
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_VENDOR> con = new GenericRepository<BASE_VENDOR>(context);
        BaseCommQuery bcq = new BaseCommQuery();
        int count = 0;
        try
        {
            for (int i = 0; i < this.grdBASE_VENDOR.Rows.Count; i++)
            {
                if (this.grdBASE_VENDOR.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_VENDOR.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string IDS = this.grdBASE_VENDOR.DataKeys[i].Values[0].ToString();
                        var msg = bcq.CheckDelCondition(IDS, BaseCommType.BASE_VENDOR);
                        if (msg.ToUpper().Equals("OK"))
                        {
                            con.Delete(IDS);	//执行动作
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

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdBASE_VENDOR.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBASE_VENDOR.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdBASE_VENDOR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBASE_VENDOREdit.aspx?Flag=1&ids=" + strKeyID, SYSOperation.Modify, strKeyID), Resources.Lang.FrmBASE_VENDORList_Title01, "BASE_VENDOR", 800, 600);//供应商管理

        }
    }

    protected void dsGrdBASE_VENDOR_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdBASE_VENDOR_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //if (e.ReturnValue is DataTable)
        //{
        //    if (grdNavigatorBASE_VENDOR.IsDbPager == false)
        //        this.grdNavigatorBASE_VENDOR.RowCount = ((DataTable)e.ReturnValue).Rows.Count;
        //}
    }
    #endregion
}

