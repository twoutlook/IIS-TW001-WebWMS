using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;

public partial class FrmBaseCONFIG_List : WMSBasePage
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

    public void GridBind()
    {
        IGenericRepository<V_SYS_CONFIG> vcon = new GenericRepository<V_SYS_CONFIG>(context);
        var caseList = from p in vcon.Get()
                       orderby p.createtime descending
                       where 1 == 1
                       select p;
        if (txtType.SelectedValue != "")
        {
            caseList = caseList.Where(x => x.type.ToString().Equals(txtType.SelectedValue));
        }
        if (!string.IsNullOrEmpty(txtNo.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.code) && x.code.Contains(txtNo.Text.Trim()));
        if (!string.IsNullOrEmpty(txtValue.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.config_value) && x.config_value.Contains(txtValue.Text.Trim()));
        caseList = caseList.Where(x => x.status.ToString().Equals("1"));
        


        AspNetPager1.RecordCount = caseList.Count();
        AspNetPager1.PageSize = this.PageSize;

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        var srcdata = GetGridSourceDataByList(data, "type", "SYSConfigType");

        grdCONFIGList.DataSource = srcdata;
        grdCONFIGList.DataBind();
    }
   
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //this.grdINASN_D.DataKeyNames = new string[]{"IDS"};
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBaseCONFIG_DEdit.aspx?Flag=0", SYSOperation.New, "") + "','" + Resources.Lang.FrmBaseCONFIG_List_Msg01+ "','SYS_CONFIG',800,600);return false;"; //系统参数配置编辑

        Help.DropDownListDataBind(GetParametersByFlagType("SYSConfigType"), txtType, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//作用域

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
        }
        IsFirstPage = true;
        this.GridBind();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //删除
        int count = 0;
        IGenericRepository<SYS_CONFIG> sysconfig = new GenericRepository<SYS_CONFIG>(context);

        try
        {
            for (int i = 0; i < this.grdCONFIGList.Rows.Count; i++)
            {
                if (this.grdCONFIGList.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdCONFIGList.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string ids = this.grdCONFIGList.DataKeys[i].Values[0].ToString();
                        sysconfig.Delete(ids);
                        sysconfig.Save();
                        count++;
                    }
                }
            }
            if (count > 0)
            {
                this.Alert(Resources.Lang.FrmALLOCATEList_DelSuccess); //删除成功！
                this.GridBind();
            }
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_DelFail + E.Message.ToJsString()); //"删除失败！" + E.Message.ToJsString()
        }
    }

    protected void grdCONFIGList_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = grdCONFIGList.DataKeys[e.Row.RowIndex].Values[0].ToString();
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBaseCONFIG_DEdit.aspx?Flag=1&ConfigID=" + strKeyID + "", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBaseCONFIG_List_Msg01, "SYS_CONFIG");//"系统参数配置编辑"
            Session["ConfigEDit"] = strKeyID;

            string desc = e.Row.Cells[4].Text;
            if (desc.Length > 10)
            {
                e.Row.Cells[4].Text = "<span class=\"wap_word_200\" data-toggle=\"tooltip\" title=\"" + desc + "\">" + desc + "</span>";
            }
            string memo = e.Row.Cells[6].Text;
            if (memo.Length > 10)
            {
                e.Row.Cells[6].Text = "<span class=\"wap_word_200\" data-toggle=\"tooltip\" title=\"" + memo + "\">" + memo + "</span>";
            }
        }
    }
    protected void grdCONFIGList_Sorting(object sender, GridViewSortEventArgs e)
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
        this.GridBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        this.GridBind();
    }
    #endregion
  
}

