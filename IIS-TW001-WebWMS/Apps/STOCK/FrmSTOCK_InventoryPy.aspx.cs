using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business;
using System.Linq.Dynamic;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using System.Data.Entity.SqlServer;
using DreamTek.WMS.DAL.Model.Base;
/// <summary>
/// 描述: 盘点单-->FrmSTOCK_CHECKBILLList 页面后台类文件
/// </summary>
public partial class FrmSTOCK_InventoryPy : WMSBasePage
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
        Bind("DCHECKDATE");
    }
    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdSTOCK_CHECKBILL.DataKeyNames = new string[] { "ID" };

        List<V_SYS_PARAMETER> newList = new List<V_SYS_PARAMETER>();
        var list = GetParametersByFlagType("CHECKSTATE");
        var item2 = list.Where(x => x.FLAG_ID == "2").FirstOrDefault();
        if (item2 != null)
        {
            newList.Add(item2);
        }
        var item3 = list.Where(x => x.FLAG_ID == "3").FirstOrDefault();
        if (item3 != null)
        {
            newList.Add(item3);
        }
        Help.DropDownListDataBind(newList, dplCHECKStatus, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
    }

    #endregion

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
        this.GridBind();
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

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("cstatus", "CHECKSTATE"));//状态

        var srcdata = GetGridSourceDataByList(data, flagList);

        grdSTOCK_CHECKBILL.DataSource = srcdata;
        grdSTOCK_CHECKBILL.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("DCHECKDATE");
    }

    public IQueryable<V_STOCK_BILLList> GetQueryList()
    {
        IGenericRepository<V_STOCK_BILLList> conn = new GenericRepository<V_STOCK_BILLList>(db);
        var caseList = from p in conn.Get()
                       orderby p.cposition descending
                       where 1 == 1
                       select p;
        if (caseList != null && caseList.Count() > 0)
        {
            if (txtcinvname.Text.Trim().ToUpper() != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvname) && x.cinvname.Contains(txtcinvname.Text.Trim().ToUpper()));
            }
            if (txtIDS.Text.Trim().IsNullOrEmpty() == false)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.checkbill_d_ids) && x.checkbill_d_ids.Contains(txtIDS.Text.Trim()));
            }
            if (txtIDName.Text.Trim().IsNullOrEmpty() == false)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccheckname) && x.ccheckname.Contains(txtIDName.Text.Trim()));
            }
            if (txtCWare.Text.Trim().IsNullOrEmpty() == false)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwareid) && x.cwareid.Contains(txtCWare.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(dplCHECKStatus.SelectedValue.Trim()))
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cstatus) && x.cstatus.Equals(dplCHECKStatus.SelectedValue.Trim()));
            }
            if (txtCposition.Text.Trim().IsNullOrEmpty() == false)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCposition.Text.Trim()));
            }
            if (txtCinvcode.Text.Trim().IsNullOrEmpty() == false)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim()));
            }
            DateTime dtime;
            if (txtDINDATEFrom.Text.Trim() != string.Empty && DateTime.TryParse(txtDINDATEFrom.Text.Trim(), out dtime))
            {
                caseList = caseList.Where(x => x.dcheckdate != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.dcheckdate) >= 0);
            }
            if (txtDINDATETo.Text.Trim() != string.Empty && DateTime.TryParse(txtDINDATETo.Text.Trim(), out dtime))
            {
                caseList = caseList.Where(x => x.dcheckdate != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.dcheckdate) <= 0);
            }
        }
        return caseList;
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
                        id = this.grdSTOCK_CHECKBILL.DataKeys[i].ToString();
                        conn.Delete(id);
                        conn.Save();
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

