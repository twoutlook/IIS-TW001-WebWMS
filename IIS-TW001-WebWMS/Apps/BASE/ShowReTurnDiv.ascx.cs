using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;


public partial class UserControls_ShowReTurnDiv : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (SetTypeCode.ContainsKey("intype"))
            {
                hfInType.Value = SetTypeCode["intype"].Trim();
            }
            if (SetTypeCode.ContainsKey("IsAll".ToLower()))
            {
                hfIsAll.Value = SetTypeCode["IsAll".ToLower()].Trim();
            }
            if (SetTypeCode.ContainsKey("erpcode"))
            {
                hfErpcode.Value = SetTypeCode["erpcode"].Trim();
            }
           
        }
    }

    //void Search(int iPage)
    //{
    //    BASE_FrmBASE_PARTListQuery listQuery = new BASE_FrmBASE_PARTListQuery();

    //    System.Data.DataTable pdat = null;

    //    if (hfInType.Value.Length > 0)
    //    {
    //        //销货退回15 -102
    //        if (hfInType.Value.Equals("102"))
    //        {
    //            //销货退回202-102 .
    //            pdat = listQuery.GetOutReturn_In(this.txtPartNumber.Text, hfErpcode.Value,"202","102", false, false, iPage, this.gvReport.PageSize);
    //        }
    //        else if (hfInType.Value.Equals("103"))
    //        {
    //            //工单退料203-103
    //            pdat = listQuery.GetOutReturn_In(this.txtPartNumber.Text, hfErpcode.Value, "203", "103", false, false, iPage, this.gvReport.PageSize);
    //        }
    //        else if (hfInType.Value.Equals("105"))
    //        {
    //            //工单超领退205-105
    //            pdat = listQuery.GetOutReturn_In(this.txtPartNumber.Text, hfErpcode.Value, "205", "105", false, false, iPage, this.gvReport.PageSize);
    //        }
    //    }

    //    //if (pdat == null)
    //    //    pdat = listQuery.GetList("", "", this.txtPartNumber.Text, this.txtName.Text, "", "", "", "", "", "", "", "", "", "", "", false, iPage, this.gvReport.PageSize);

    //    gvReport.PageIndex = iPage;
    //    gvReport.DataSource = pdat;
    //    gvReport.DataBind();
    //}

    void Search(int iPage)
    {
        CurrendIndex = 1;
        Bind("");
    }


    public IQueryable<V_OutReturn_In> GetQueryList()
    {
        IGenericRepository<V_OutReturn_In> pigeonBill = new GenericRepository<V_OutReturn_In>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtPartNumber.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtPartNumber.Text.Trim()));
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

        gvReport.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        gvReport.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }
	

    [Browsable(true), Description("刷新的控件名")]
    public string SetErpCodeLine { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetCINVNAME { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetCINVCODE { get; set; }


    Dictionary<string, string> _SetTypeCode = new Dictionary<string, string>();
    /// <summary>
    /// 获取
    /// </summary>
    public Dictionary<string, string> SetTypeCode
    {
        get
        {
            return _SetTypeCode;
        }
        set
        {
            _SetTypeCode = value;
        }
    }
    
    [Browsable(true), Description("Div名称")]
    public string GetDivName { get { return ajaxWebSearChComp.ClientID; } }

    [Browsable(true), Description("是否显示企业名称(企业代码)")]
    public bool GetComp { get; set; }

    void Alert(string Message)
    {
        Page.ClientScript.RegisterClientScriptBlock(GetType(), "f1", "<script>alert('" + Message + "！');</script>");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //BASE_FrmBASE_PARTListQuery listQuery = new BASE_FrmBASE_PARTListQuery();
        //DataTable dtRowCount = null;
        //if (hfInType.Value.Length > 0)
        //{
        //     //销货退回15 -102
        //    if (hfInType.Value.Equals("102"))
        //    {
        //        //销货退回202-102 . 只能退相同ERPcode下的料和数量
        //        dtRowCount = listQuery.GetOutReturn_In(this.txtPartNumber.Text, hfErpcode.Value, "202", "102", false, true, -1, -1);
        //    }
        //    else if (hfInType.Value.Equals("103"))
        //    {
        //        //工单退料203-103
        //        dtRowCount = listQuery.GetOutReturn_In(this.txtPartNumber.Text, hfErpcode.Value,"203","103", false, true, -1, -1);
        //    }
        //    else if (hfInType.Value.Equals("105"))
        //    {
        //        //工单超领退205-105
        //        dtRowCount = listQuery.GetOutReturn_In(this.txtPartNumber.Text, hfErpcode.Value, "205", "105", false, true, -1, -1);
        //    }
        //}
     
        //this.grdNavigator.CurrentPageIndex = 0;

        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdNavigator.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdNavigator.RowCount = 0;
        //}

        Search(0);

        //if (this.grdNavigator.Controls.Count >= 6)
        //{
        //    this.grdNavigator.Controls[6].Visible = false;
        //    this.grdNavigator.Controls[5].Visible = false;
        //}
    }

    protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //try
        //{
        //    this.grdNavigator.CurrentPageIndex = e.NewPageIndex;
        //    Search(e.NewPageIndex);
        //}
        //catch
        //{
        //    this.grdNavigator.CurrentPageIndex = 0;
        //    Search(0);
        //}
    }

    protected void gvReport_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow viewrow = gvReport.Rows[e.NewSelectedIndex];
        e.Cancel = true;
        if (GetComp)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                        "SetRMA_Value('" + SetCINVNAME + "','" + gvReport.DataKeys[e.NewSelectedIndex].Values[0].ToString().Trim().Replace("'", "\\'") + "','" + SetCINVCODE + "','" + viewrow.Cells[1].Text + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
                      
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                        "SetRMA_Value('" + SetCINVNAME + "','" + gvReport.DataKeys[e.NewSelectedIndex].Values[0].ToString().Trim().Replace("'", "\\'") + "','" + SetCINVCODE + "','" + viewrow.Cells[1].Text  + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);

         }
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {

    }
}
