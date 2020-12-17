using DreamTek.WMS.DAL.Model.Base;
using DreamTek.WMS.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_ModifyXML_XMLList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Init();
            GridBind();
        }
    }

    public void Init()
    {
        this.OpenFloatWinMax(btnNew, BuildRequestPageURL("ModifyXML.aspx", SYSOperation.New, ""), "新增", "ResourceEdit");
        Help.DropDownListDataBind(new SysParameterList().LoadStatusByFlag_type("Language"), ddllang, "全部", "flag_name", "flag_id", "");
        Help.DropDownListDataBind(new SysParameterList().LoadStatusByFlag_type("BASE_CLIENT"), ddlStatus, "全部", "flag_name", "flag_id", "");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridBind();
    }

    public void GridBind()
    {
        Base_ResourcesRepository brr = new Base_ResourcesRepository();
        Base_ResourcesQuery bq = new Base_ResourcesQuery();
        bq.LanguageId = ddllang.SelectedValue;
        bq.SourceKey = txtKey.Text.Trim();
        bq.SourceValue = txtValue.Text.Trim();
        bq.CStatus = ddlStatus.SelectedValue;
        int total = 0;
        var list = brr.Query(bq,PageSize,CurrendIndex, out total);
        AspNetPager1.RecordCount = total;
        AspNetPager1.PageSize = this.PageSize;
        grdLangSource.DataSource = list;
        grdLangSource.DataBind();
    }

    protected void grdLangSource_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = grdLangSource.DataKeys[e.Row.RowIndex].Values[0].ToString();
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            //this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBASE_CLIENTEdit.aspx", SysOperation.Modify, strKeyID), "客户管理", "BASE_CLIENT", 800, 600);
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("ModifyXML.aspx", SYSOperation.Modify, strKeyID), "编辑", "ResourceEdit");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
}