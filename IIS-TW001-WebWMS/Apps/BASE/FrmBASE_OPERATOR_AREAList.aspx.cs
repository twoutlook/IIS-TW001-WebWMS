using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;

/// <summary>
/// 描述: 操作人-->FrmBASE_OPERATOR_AREAList 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-09-29 15:22:26
/// </summary>
public partial class BASE_FrmBASE_OPERATOR_AREAList :WMSBasePage
{
    #region 
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
        //BASE_FrmBASE_OPERATOR_AREAListQuery listQuery = new BASE_FrmBASE_OPERATOR_AREAListQuery();
        //DataTable dtSource = listQuery.GetList(txtCACCOUNTID.Text, txtCCARGOID.Text, txtDCREATETIME.Text, false, this.grdNavigatorBASE_OPERATOR_AREA.CurrentPageIndex, this.grdBASE_OPERATOR_AREA.PageSize);
        //this.grdBASE_OPERATOR_AREA.DataSource = dtSource;
        //this.grdBASE_OPERATOR_AREA.DataBind();
        //;
        IGenericRepository<BASE_OPERATOR_AREA> entity = new GenericRepository<BASE_OPERATOR_AREA>(context);
        var caseList = from p in entity.Get()
                       orderby p.dcreatetime descending
                       where 1 == 1
                       select p;

        if (txtCACCOUNTID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.caccountid) && x.caccountid.Contains(txtCACCOUNTID.Text.Trim()));

        if (txtCCARGOID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccargoid) && x.ccargoid.Contains(txtCCARGOID.Text.Trim()));

        if (txtDCREATETIME.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.dcreatetime.ToString()) && x.dcreatetime.ToString().Contains(txtDCREATETIME.Text.Trim()));
        
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";
        grdBASE_OPERATOR_AREA.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdBASE_OPERATOR_AREA.DataBind();
        
    }

    public bool CheckData()
    {
        if (this.txtCACCOUNTID.Text.Trim().Length > 0)
        {
        }
        if (this.txtCCARGOID.Text.Trim().Length > 0)
        {
        }
        if (this.txtDCREATETIME.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIME.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmBASE_OPERATORList_Msg01);//创建日期项不是有效的日期！
                this.SetFocus(txtDCREATETIME);
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
        this.grdBASE_OPERATOR_AREA.DataKeyNames = new string[] { "ID" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWin('" + WMSBasePage.BuildRequestPageURL("FrmBASE_OPERATOR_AREAEdit.aspx?Flag=0", SYSOperation.New, "") + "','" + Resources.Lang.FrmBASE_OPERATORList_Msg02+ "','BASE_OPERATOR_AREA',800,600);return false;"; //新建操作人
        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmBASE_OPERATOR_AREAEdit.aspx", SysOperation.New,""),800,600);

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
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorBASE_OPERATOR_AREA
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_OPERATOR> con = new GenericRepository<BASE_OPERATOR>(context);
        try
        {
            for (int i = 0; i < this.grdBASE_OPERATOR_AREA.Rows.Count; i++)
            {
                if (this.grdBASE_OPERATOR_AREA.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_OPERATOR_AREA.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string Id=this.grdBASE_OPERATOR_AREA.DataKeys[i].Values[0].ToString();
                        con.Delete(Id);	//执行动作 
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
        for (int i = 0; i < this.grdBASE_OPERATOR_AREA.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBASE_OPERATOR_AREA.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdBASE_OPERATOR_AREA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBASE_OPERATOR_AREAEdit.aspx?OperType=Oper&Flag=1&ids=" + strKeyID, SYSOperation.Modify, strKeyID), Resources.Lang.FrmBASE_OPERATOR_AREAEdit_Title01, "BASE_OPERATOR_AREA", 800, 600);//操作人
          
        }

    }

    #endregion
  
}

