using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Data.SqlClient;
using System.Data.Entity.SqlServer;
public partial class FrmOUT_TRANSPORTATIONPLAN_List : WMSBasePage
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.txtREQDELIVERYDATEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.txtREQDELIVERYDATETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

    }

    public bool CheckData()
    {
        return true;
    }

    #region IPageGrid 成员
    public void GridBind()
    {

        IGenericRepository<OUT_TRANSPORTATIONPLAN> con = new GenericRepository<OUT_TRANSPORTATIONPLAN>(context);
        var caseList = from p in con.Get()
                       orderby p.createtime descending
                       where 1 == 1
                       select p;

        if (ddlIS_Sign.SelectedValue != "")
            caseList = caseList.Where(x => x.ISSIGN.ToString().Equals(ddlIS_Sign.SelectedValue));

        if (txtDOCNO.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.docno) && x.docno.Contains(txtDOCNO.Text.Trim()));
        if (txtCERPCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtCERPCODE.Text.Trim()));

        if (txtTRANSTYPE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.transtype) && x.transtype.Contains(txtTRANSTYPE.Text.Trim()));

        if (txtCUSTOMERNO.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.customerno) && x.customerno.Contains(txtCUSTOMERNO.Text.Trim()));

        if (txtCVENDERCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cvendercode) && x.cvendercode.Contains(txtCVENDERCODE.Text.Trim()));

        if (txtCINVCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCINVCODE.Text.Trim()));

        if (txtREQDELIVERYDATEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.reqdeliverydate != null && SqlFunctions.DateDiff("dd", txtREQDELIVERYDATEFrom.Text.Trim(), x.reqdeliverydate) >= 0);
        if (txtREQDELIVERYDATETo.Text != string.Empty)
            caseList = caseList.Where(x => x.reqdeliverydate != null && SqlFunctions.DateDiff("dd", txtREQDELIVERYDATETo.Text.Trim(), x.reqdeliverydate) <= 0);

        if (txtPRODUCETIMEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.PRODUCETIME != null && SqlFunctions.DateDiff("dd", txtPRODUCETIMEFrom.Text.Trim(), x.PRODUCETIME) >= 0); 
        if (txtPRODUCETIMETo.Text != string.Empty)
            caseList = caseList.Where(x => x.PRODUCETIME != null && SqlFunctions.DateDiff("dd", txtPRODUCETIMETo.Text.Trim(), x.PRODUCETIME) <= 0);

 
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.RecordCount = caseList.Count();

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("cstatus", "IS_Sign"));
        var srcdata = GetGridSourceDataByList(data, flagList);

        AspNetPager1.PageSize = this.PageSize;
        this.grdOUTTRANSPORTATIONPLAN.DataSource = srcdata;

        this.grdOUTTRANSPORTATIONPLAN.DataBind();
    }
    #endregion


    #region IPage 成员
    public void InitPage()
    {

        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";

        //多国语更改,dropDownlist【begin】
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "IS_Sign", false, -1, -1), this.ddlIS_Sign, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        //多国语更改,dropDownlist【end】
    }
    #endregion


    protected System.Data.DataTable grdNavigatorOUTTRANSPORTATIONPLAN_GetExportToExcelSource()
    {
        DataTable dt = new DataTable();
        return dt;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            for (int i = 0; i < this.grdOUTTRANSPORTATIONPLAN.Rows.Count; i++)
            {
                if (this.grdOUTTRANSPORTATIONPLAN.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdOUTTRANSPORTATIONPLAN.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string id = this.grdOUTTRANSPORTATIONPLAN.DataKeys[i].Values[0].ToString();
                        #region 调用存储过程

                        List<string> SparaList = new List<string>();
                        SparaList.Add("@P_DOCNO_ID:" + id);
                        SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                        SparaList.Add("@P_return_Value:" + "");
                        SparaList.Add("@P_ErrText:" + "");
                        string[] Result = DBHelp.ExecuteProc("OUT_TRANSPORTATIONPLAN_MERGE", SparaList);
                        if (Result.Length == 1)
                        {
                            msg += Result[0].ToString();
                        }
                        else if (Result[0] == "0")
                        {
                            msg += Resources.Lang.FrmOUTASNList_Tips_HeBingChengGong;// "合并成功!";
                        }
                        else
                        {
                            msg += Result[1].ToString();
                        }
                        #endregion
                    }
                }
            }
            this.btnSearch_Click(sender, e);
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }
        this.Alert(msg);
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.GridBind();
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";

        strKeyId = this.grdOUTTRANSPORTATIONPLAN.DataKeys[rowIndex].Values[0].ToString();
        return strKeyId;
    }

    protected void grdOUTTRANSPORTATIONPLAN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            CheckBox cbo = e.Row.FindControl("chkSelect") as CheckBox;

            string id = this.grdOUTTRANSPORTATIONPLAN.DataKeys[e.Row.RowIndex][0].ToString();

            switch (e.Row.Cells[e.Row.Cells.Count - 5].Text)
            {
                case "0": e.Row.Cells[e.Row.Cells.Count - 5].Text = Resources.Lang.WMS_Common_DropOption_Confirmed; break;//已确认
                case "1": e.Row.Cells[e.Row.Cells.Count - 5].Text = Resources.Lang.WMS_Common_DropOption_NotConfirm; break;//未确认

            }
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void grdOUTTRANSPORTATIONPLAN_PageIndexChanged(object sender, EventArgs e)
    {
        //this.GridBind();
    }
    protected void grdOUTTRANSPORTATIONPLAN_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }
    /// <summary>
    /// 获取选择的ID
    /// </summary>
    public void GetSelectedIds()
    {
        if (SelectIds == null)
        {
            SelectIds = new Dictionary<string, string>();
        }

        for (int i = 0; i < this.grdOUTTRANSPORTATIONPLAN.Rows.Count; i++)
        {
            if (this.grdOUTTRANSPORTATIONPLAN.Rows[i].Cells[0].Controls[1] is CheckBox)
            {
                CheckBox chkSelect = (CheckBox)this.grdOUTTRANSPORTATIONPLAN.Rows[i].Cells[0].Controls[1];
                if (chkSelect.Checked)
                {
                    string id = this.grdOUTTRANSPORTATIONPLAN.DataKeys[i].Values[0].ToString();
                    SelectIds.Add(id, this.grdOUTTRANSPORTATIONPLAN.DataKeys[i].Values[1].ToString());
                }
            }
        }

    }
    protected void grdOUTTRANSPORTATIONPLAN_DataBinding(object sender, EventArgs e)
    {
    }
}