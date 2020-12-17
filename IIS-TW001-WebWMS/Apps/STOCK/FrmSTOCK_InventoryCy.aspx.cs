using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using CheckBox = System.Web.UI.WebControls.CheckBox;

using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Data.Entity.SqlServer;
/// <summary>
/// 描述: 盘点单-->FrmSTOCK_CHECKBILLList 页面后台类文件
/// </summary>
public partial class FrmSTOCK_InventoryCy : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.grdSTOCK_CHECKBILL.Columns[1].Visible = false;
        if (Page.IsPostBack == false)
        {
            this.InitPage();
        }
    }


    #region IPageGrid 成员

    public void GridBind()
    {
        Bind("");
    }

    public IQueryable<V_STOCK_CHECKEDBILL_CY> GetQueryList()
    {
        IGenericRepository<V_STOCK_CHECKEDBILL_CY> conn = new GenericRepository<V_STOCK_CHECKEDBILL_CY>(db);
        var caseList = from p in conn.Get()
                       orderby p.cinvcode descending
                       where p.CHECKTYPE == "1"
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtIDS.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.checkbill_D_IDS) && x.checkbill_D_IDS.Contains(txtIDS.Text.Trim()));
            }
            if (txtIDName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccheckname) && x.ccheckname.Contains(txtIDName.Text.Trim()));
            }
            if (txtCinvName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvname) && x.cinvname.Contains(txtCinvName.Text.Trim()));
            }
            if (txtCWare.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwareid) && x.cwareid.Contains(txtCWare.Text.Trim()));
            }
            if (txtCposition.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCposition.Text.Trim()));
            }
            if (txtCinvcode.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));
            }
            #region //22-10-2020 by Qamar
            if (txtRANK_FINAL.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && (x.cinvcode.Substring(x.cinvcode.Length - 1, 1) == (txtRANK_FINAL.Text.Trim().ToUpper())));
            }
            #endregion
            DateTime d;
            if (!txtDINDATEFrom.Text.Trim().IsNullOrEmpty() && DateTime.TryParse(txtDINDATEFrom.Text.Trim(), out d))
            {
                caseList = caseList.Where(x => x.DCHECKDATE != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.DCHECKDATE) >= 0);
            }
            if (!txtDINDATETo.Text.Trim().IsNullOrEmpty() && DateTime.TryParse(txtDINDATETo.Text.Trim(), out d))
            {
                caseList = caseList.Where(x => x.DCHECKDATE != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.DCHECKDATE) <= 0);
            }
            //添加状态筛选
            if (!ddlStatus.SelectedValue.Trim().IsNullOrEmpty())
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cstatus) && x.cstatus.Contains(ddlStatus.SelectedValue.Trim()));
            }
            //添加规格筛选
            if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cspecifications) && x.cspecifications.Contains(txtcspec.Text.Trim()));
            }
        }
        return caseList;
    }

    public void Bind(string sortStr)
    {
        var caseList = GetQueryList();

        if (caseList != null && caseList.Count() > 0)
        {
            //按照列名排序
            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }

        //grdSTOCK_CHECKBILL.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        //
        var listResult = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("CSTATUS", "cstatusName", "CYCLE"));//状态
        var source = GetGridDataByAddColumns(listResult, flagList);
        grdSTOCK_CHECKBILL.DataSource = source;
        grdSTOCK_CHECKBILL.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdSTOCK_CHECKBILL.DataKeyNames = new string[] { "ID" };
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "CYCLE", false, -1, -1), this.ddlStatus, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//全部
        ListItem listitem = new ListItem(Resources.Lang.WMS_Common_DropOption_UnDo, "0");//未处理
        this.ddlStatus.Items.RemoveAt(2);
        this.ddlStatus.Items.RemoveAt(1);
        this.ddlStatus.SelectedValue = "6"; //默认是盘点中的状态
    }

    #endregion

    protected DataTable grdNavigatorSTOCK_CHECKBILL_GetExportToExcelSource()
    {
        return null;
    }

    protected void grdSTOCK_CHECKBILL_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void grdSTOCK_CHECKBILL_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }
    /// 查询按钮
    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        Bind("");
    }


    /// 刪除按鈕
    /// <summary>
    /// 刪除按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string id = string.Empty;

            IGenericRepository<STOCK_CHECKEDBILL> conn = new GenericRepository<STOCK_CHECKEDBILL>(db);

            for (int i = 0; i < this.grdSTOCK_CHECKBILL.Rows.Count; i++)
            {
                if (this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        id = this.grdSTOCK_CHECKBILL.DataKeys[i].Values[0].ToString();
                        conn.Delete(id);
                        conn.Save();
                        //执行动作 
                        count++;
                    }
                }
            }
            if (count > 0)
            {
                this.Alert(Resources.Lang.WMS_Common_Msg_DeleteSuccess);//删除成功！
            }
            else
            {
                Alert(Resources.Lang.WMS_Common_Tips_NeedDeleteOptions);//请选择要删除的项!
            }
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + E.Message.ToJsString());//删除失败！
        }
    }

    /// 獲取IDs
    /// <summary>
    /// 獲取IDs
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <returns></returns>
    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdSTOCK_CHECKBILL.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdSTOCK_CHECKBILL.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdSTOCK_CHECKBILL_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            #region //21-10-2020 by Qamar
            var partNumber = e.Row.Cells[6].Text;
            e.Row.Cells[6].Text = partNumber.Substring(0, partNumber.Length - 2);
            var rankfinal = partNumber.Substring(partNumber.Length - 1, 1);
            if (rankfinal == "_")
                e.Row.Cells[7].Text = "";
            else
                e.Row.Cells[7].Text = rankfinal;
            #endregion
        }
    }

    protected void dsGrdSTOCK_CHECKBILL_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdSTOCK_CHECKBILL_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }

}

