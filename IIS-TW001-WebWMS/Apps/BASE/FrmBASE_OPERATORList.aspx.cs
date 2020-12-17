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
using DreamTek.ASRS.Business.Base;

/// <summary>
/// 描述: 操作人-->FrmBASE_OPERATORList 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-09-29 15:29:06
/// </summary>
/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/
public partial class BASE_FrmBASE_OPERATORList :WMSBasePage
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
        IGenericRepository<V_BASE_OPERATOR> entity = new GenericRepository<V_BASE_OPERATOR>(context);
        var caseList = from p in entity.Get()
                       orderby p.dcreatetime descending
                       where 1 == 1
                       select p;

        if (txtCACCOUNTID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.caccountid) && x.caccountid.Contains(txtCACCOUNTID.Text.Trim()));

        if (txtCOPERATORNAME.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.coperatorname) && x.coperatorname.Contains(txtCOPERATORNAME.Text.Trim()));

        if (txtCALIAS.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.calias) && x.calias.Contains(txtCALIAS.Text.Trim()));
        if (txtCERPCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtCERPCODE.Text.Trim()));

        if (txtCALIAS.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.calias) && x.calias.Contains(txtCALIAS.Text.Trim()));

        if (dplCSTATUS.SelectedValue != "")
            caseList = caseList.Where(x => x.cstatus.ToString().Equals(dplCSTATUS.SelectedValue));
        if (txtCERPCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode.ToString()) && x.cerpcode.ToString().Contains(txtCERPCODE.Text.Trim()));

        if (txtCPHONE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cphone.ToString()) && x.cphone.ToString().Contains(txtCPHONE.Text.Trim()));
        if (txtCDEPARTMENT.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cdepartment.ToString()) && x.cdepartment.ToString().Contains(txtCDEPARTMENT.Text.Trim()));
        if (txtCDUTY.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cduty.ToString()) && x.cduty.ToString().Contains(txtCDUTY.Text.Trim()));
        if (txtCSHIFT.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cshift.ToString()) && x.cshift.ToString().Contains(txtCSHIFT.Text.Trim()));
        if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMEFrom.Text.Trim(),x.dcreatetime) >= 0 );
        if (txtDCREATETIMETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMETo.Text.Trim(), x.dcreatetime) <= 0);
        
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

        var srcdata = GetGridSourceDataByList(data, "cstatus", "BASE_OPERATOR.CSTATUS");

        grdBASE_OPERATOR.DataSource = srcdata;
        grdBASE_OPERATOR.DataBind();
    }

    public bool CheckData()
    {
        if (this.txtCACCOUNTID.Text.Trim().Length > 0)
        {
        }
        if (this.txtCOPERATORNAME.Text.Trim().Length > 0)
        {
        }
        if (this.dplCSTATUS.Text.Trim().Length > 0)
        {
        }
        if (this.txtCALIAS.Text.Trim().Length > 0)
        {
        }
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtCPHONE.Text.Trim().Length > 0)
        {
        }
        if (this.txtCDEPARTMENT.Text.Trim().Length > 0)
        {
        }
        if (this.txtCDUTY.Text.Trim().Length > 0)
        {
        }
        if (this.txtCSHIFT.Text.Trim().Length > 0)
        {
        }
        if (this.txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate(this.txtDCREATETIMEFrom.Text)== false)
            {
                this.Alert(Resources.Lang.FrmBASE_OPERATORList_Msg01);//创建日期项不是有效的日期！
                this.SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        if (this.txtDCREATETIMETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_ToItemEmpty);//到项不允许空！
            this.SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (this.txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate( this.txtDCREATETIMETo.Text)== false)
            {
                this.Alert(Resources.Lang.FrmALLOCATEList_NoEffToItem);//到项不是有效的日期！
                this.SetFocus(txtDCREATETIMETo);
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
        this.grdBASE_OPERATOR.DataKeyNames = new string[] { "ID" };

        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWin('" + WMSBasePage.BuildRequestPageURL("FrmBASE_OPERATOREdit.aspx?Flag=0", SYSOperation.New, "") + "','" + Resources.Lang.FrmBASE_OPERATORList_Msg02+ "','BASE_OPERATOR',800,600);return false;"; //新建操作人
        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmBASE_OPERATOREdit.aspx", SysOperation.New,""),800,600);

        Help.DropDownListDataBind(GetParametersByFlagType("BASE_OPERATOR.CSTATUS"), dplCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
    }

    #endregion

    protected void grdBASE_OPERATOR_PageIndexChanged(object sender, EventArgs e)
    {
        //if(grdNavigatorBASE_OPERATOR.IsDbPager)
        {
            this.GridBind();
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
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
        //grdNavigatorBASE_OPERATOR
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_OPERATOR> con = new GenericRepository<BASE_OPERATOR>(context);
        DreamTek.ASRS.Business.Base.ZSICT1_USERQuery deleteQuery = new DreamTek.ASRS.Business.Base.ZSICT1_USERQuery();
        try
        {
            for (int i = 0; i < this.grdBASE_OPERATOR.Rows.Count; i++)
            {
                if (this.grdBASE_OPERATOR.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_OPERATOR.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                       string Id= this.grdBASE_OPERATOR.DataKeys[i].Values[0].ToString(); 
                        //删除部门信息，以及区域信息
                       if (deleteQuery.DeleteInfoByOperationID(Id))
                       {
                           con.Delete(Id);
                           con.Save();
                       }
                       else
                       {
                           this.Alert(Resources.Lang.Common_FailDel); //删除失败！
                       }
                    }
                }
            }
              this.Alert(Resources.Lang.Common_SuccessDel); //删除成功!         
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.Common_FailDel + E.Message.ToJsString()); //删除失败！
        }
    }



    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdBASE_OPERATOR.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBASE_OPERATOR.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdBASE_OPERATOR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBASE_OPERATOREdit.aspx?Flag=1&ids=" + strKeyID, SYSOperation.Modify, strKeyID), Resources.Lang.FrmBASE_OPERATORList_Title01, "BASE_OPERATOR", 800, 600);//操作人
            HyperLink linkModify1 = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify1.NavigateUrl = "#";


            //HyperLink linkModify1 = (HyperLink)e.Row.Cells[e.Row.Cells.Count-1].Controls[0];
            //linkModify1.NavigateUrl = "#";
            //this.OpenFloatWin(linkModify1, BuildRequestPageURL("FrmBASE_SELECTCARGOSPACEList.aspx", SysOperation.Modify, strKeyID), "选择储位", "BASE_SELECTCARGOSPACE", 630, 450);
            //选择储位
            //this.OpenFloatWin(linkModify1, BuildRequestPageURL("FrmBASE_OPERATOR_AREA.aspx?OperType=Oper", SysOperation.Modify, strKeyID), "", "BASE_OPERATOR_AREA", 680, 590);
            //选择区域
            this.OpenFloatWin(linkModify1, BuildRequestPageURL("FrmBASE_OPERATOR_AREAPart.aspx?OperType=Oper&Flag=1&ids=" + strKeyID, SYSOperation.Modify, strKeyID), "", "BASE_OPERATOR_AREA", 800, 600);
            //this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmOUTBILL_DEdit.aspx", SysOperation.New, "") + "&IDS=" + this.KeyID + "','出库单','OUTBILL_D',600,350);");
        }
    }

    protected void dsGrdBASE_OPERATOR_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdBASE_OPERATOR_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //if (e.ReturnValue is DataTable)
        //{
        //    if (grdNavigatorBASE_OPERATOR.IsDbPager == false)
        //        this.grdNavigatorBASE_OPERATOR.RowCount = ((DataTable)e.ReturnValue).Rows.Count;
        //}

    }
    #endregion
}

