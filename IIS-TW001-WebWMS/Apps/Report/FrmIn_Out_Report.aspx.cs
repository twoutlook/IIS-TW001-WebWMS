using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Business;
/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/
public partial class Apps_Report_FrmIn_Out_Report : WMSBasePage// PageBase, IPageGrid
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtDCREATETIMEFrom.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            InitPage();
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
           // BindDate();
        }

        //this.FunctionNo = "";
        //this.HasRight();
        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
    }

    public DataTable ToExcel()
    {      
        DataTable dt = new DataTable();      
        int pageCount = 0;
        btnExcel.ExcelName = Resources.Lang.FrmIn_Out_Report_MSG1;// "进出明细报表";
        dt = In_Out_Report.FrmInOut_ReprotSummay(txtLH.Text, txtcspec.Text, txtDCREATETIMEFrom.Text, txtDCREATETIMETo.Text,txtRANK_FINAL.Text, -1, PageSize, out pageCount);      
        return dt;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (CheckData())
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
            this.GridBind();
        }
    }

    public void GridBind()
    {
        //DataTable dtSource = In_Out_Report.FrmInOut_Report(txtDCREATETIMEFrom.Text, txtDCREATETIMETo.Text, txtCK.Text, txtCW.Text, txtLH.Text,txtcspec.Text);

        //var caseList = In_Out_Report.Menus(dtSource);
        //if (caseList != null && caseList.Count() > 0)
        //{
        //    AspNetPager1.RecordCount = caseList.Count();
        //    AspNetPager1.PageSize = this.PageSize;
        //}
        //else
        //{
        //    AspNetPager1.RecordCount = 0;
        //    AspNetPager1.PageSize = this.PageSize;
        //}
        //AspNetPager1.PageSize = this.PageSize;
        //this.grdINASN.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        //this.grdINASN.DataBind();
        //20200423根据雪龙需求修改该报表用以下逻辑
        int pageCount = 0;

        DataTable dt = In_Out_Report.FrmInOut_ReprotSummay(txtLH.Text, txtcspec.Text, txtDCREATETIMEFrom.Text, txtDCREATETIMETo.Text,txtRANK_FINAL.Text, CurrendIndex, PageSize, out pageCount);

        #region  显示批/序號(RANK) 代码 2020-09-16 李舟蕾
        dt.Columns.Add("RANK_FINAL", Type.GetType("System.String")); //批/序號(RANK)

        DataTable newdt = dt.Clone();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToString(dt.Rows[i]["cspecifications"]) == "合计")
            {
                newdt.ImportRow(dt.Rows[i]);
            }
            else
            {
                string Cinvcode = Convert.ToString(dt.Rows[i]["cinvcode"]);
                string[] array = Cinvcode.Split(new char[] { '-' });

                if (array.Count() > 1 && array[array.Count() - 1].ToString().Length == 1)
                {
                    int s1 = Cinvcode.LastIndexOf("-");
                    for (int k = 0; k < Cinvcode.Length - s1; k++)
                    {
                        dt.Rows[i]["cinvcode"] = Cinvcode.Remove(Cinvcode.Length - k - 1);
                    }
                    if (array[array.Count() - 1].ToString() == "_")
                    {
                        dt.Rows[i]["RANK_FINAL"] = "";
                    }
                    else
                    {
                        dt.Rows[i]["RANK_FINAL"] = array[array.Count() - 1].ToString().ToUpper();
                    }

                    newdt.ImportRow(dt.Rows[i]);
                }
            }
        }
        #endregion

        AspNetPager1.CustomInfoHTML = " " + Resources.Lang.WMS_Common_Pager_PageCount + ":<b>" + "</b>";//总页数
        if (newdt.Rows.Count != 0)
        {
            pageCount = newdt.Rows.Count;
        }
        AspNetPager1.RecordCount = pageCount;

            
        if (newdt != null && newdt.Rows.Count > 0)
        {
            grdINASN.DataSource = newdt;
            grdINASN.DataBind();
        }
        else
        {
            grdINASN.DataSource = newdt;
            grdINASN.DataBind();
        }
       
    }
    public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
    {
        if (PageIndex == 0)
            return dt;//0页代表每页数据，直接返回

        if (dt == null)
        {
            DataTable table = new DataTable();
            return table;
        }

        DataTable newdt = dt.Copy();
        newdt.Clear();//copy dt的框架

        int rowbegin = (PageIndex - 1) * PageSize;
        int rowend = PageIndex * PageSize;//要展示的数据条数

        if (rowbegin >= dt.Rows.Count)
            return newdt;//源数据记录数小于等于要显示的记录，直接返回dt

        if (rowend > dt.Rows.Count)
            rowend = dt.Rows.Count;
        for (int i = rowbegin; i <= rowend - 1; i++)
        {
            DataRow newdr = newdt.NewRow();
            DataRow dr = dt.Rows[i];
            foreach (DataColumn column in dt.Columns)
            {
                newdr[column.ColumnName] = dr[column.ColumnName];
            }
            newdt.Rows.Add(newdr);
        }
        return newdt;
    }
    protected void grdINASN_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

   
    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //
        //if (this.txtLH.Text.Trim() == "")
        //{
        //    this.Alert(Resources.Lang.FrmIn_Out_Report_MSG2 + "！");//查询料号不能为空
        //    this.SetFocus(txtLH);
        //    return false;
        //}
        if (txtDCREATETIMEFrom.Text.Trim().Length == 0 || txtDCREATETIMETo.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmSTOCK_TradeQueryList_NeedDateQuJian);//日期区间不能为空！
            this.SetFocus(txtDCREATETIMEFrom);
            return false;
        }
        try
        {
            if (Convert.ToDateTime(this.txtDCREATETIMETo.Text.Trim()) < Convert.ToDateTime(this.txtDCREATETIMEFrom.Text.Trim()))
            {
                this.Alert("结束日期不能小于开始日期" + "！");//查询料号不能为空
                this.SetFocus(txtDCREATETIMETo);
                return false;
            }
            if (Convert.ToDateTime(txtDCREATETIMEFrom.Text) < Convert.ToDateTime(txtDCREATETIMETo.Text).AddMonths(-1))
            {
                this.Alert(Resources.Lang.FrmSTOCK_TradeQueryList_DateQuJianLarge);//日期区间不能大于一个月！
                this.SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        catch
        {
            this.Alert("日期格式不正确！");//查询料号不能为空
            return false;
        }
        return true;

    }

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
    }
    protected void grdINASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[e.Row.Cells.Count - 5].Text.Contains("合计"))
            {
                //e.Row.Cells[0].Visible = false;
                //e.Row.Cells[1].Visible = false;
                //e.Row.Cells[2].Visible = false;
                //e.Row.Cells[e.Row.Cells.Count - 4].ColumnSpan = 4;
                e.Row.Cells[e.Row.Cells.Count - 5].Style.Add("text-align", "right");// = HorizontalAlign.Right;
                //e.Row.Cells[e.Row.Cells.Count - 4].BackColor = System.Drawing.Color.Yellow;
                //e.Row.Cells[e.Row.Cells.Count - 3].BackColor = System.Drawing.Color.Yellow;
                //e.Row.Cells[e.Row.Cells.Count - 2].BackColor = System.Drawing.Color.Yellow;
                //e.Row.Cells[e.Row.Cells.Count - 1].BackColor = System.Drawing.Color.Yellow;
            }
        }
    }
}