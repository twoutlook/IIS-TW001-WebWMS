using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Business;
using System.Text;

public partial class Apps_Report_FrmIn_Out_Report1 : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            this.txtDCREATETIMEFrom.Text = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
            this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            InitPage();
            grdInOutDetails.DataSource = null;
            grdInOutDetails.DataBind();      
        }
        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
    }
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
    }
    public DataTable ToExcel()
    {
        DataTable dtExport = new DataTable();
        DataSet dsSource = In_Out_Report.FrmInOut_ReprotNew(txtCinvCode.Text, txtcspec.Text, txtDCREATETIMEFrom.Text, txtDCREATETIMETo.Text);
        dtExport.Columns.Add(new DataColumn("createDate", typeof(string)));
        dtExport.Columns.Add(new DataColumn("cticketcode", typeof(string)));
        dtExport.Columns.Add(new DataColumn("typename", typeof(string)));
        dtExport.Columns.Add(new DataColumn("ERPCODE", typeof(string)));
        dtExport.Columns.Add(new DataColumn("insummary", typeof(string)));
        dtExport.Columns.Add(new DataColumn("outsummary", typeof(string)));
        dtExport.Columns.Add(new DataColumn("stocksummary", typeof(string)));

        if (dsSource != null && dsSource.Tables[0] != null && dsSource.Tables[0].Rows.Count > 0)
        {
            string str_ymonth = "";
            DataRow drExport = null;
            foreach (DataRow row in dsSource.Tables[0].Rows)
            {
                drExport = dtExport.NewRow();

                if (str_ymonth != "" && str_ymonth != row["ymonth"].ToString())
                {
                    drExport["createDate"] = "";
                    drExport["cticketcode"] = "";
                    drExport["typename"] = "";
                    drExport["ERPCODE"] = "本月合计";
                    var result = (from s1 in dsSource.Tables[1].AsEnumerable()
                                  where s1.Field<string>("ymonth") == str_ymonth
                                  select s1).FirstOrDefault();
                    if (result != null)
                    {
                        drExport["insummary"] = result["summaryIn"];
                        drExport["outsummary"] = result["summaryOut"];
                        drExport["stocksummary"] = result["stocksummary"];//供应商编号    
                    }
                    else
                    {
                        drExport["insummary"] = "0.00";
                        drExport["outsummary"] = "0.00";
                        drExport["stocksummary"] = "0.00";
                    }
                    dtExport.Rows.Add(drExport);
                }
                
                drExport = dtExport.NewRow();
                drExport["createDate"] = row["createDate"];
                drExport["cticketcode"] = row["cticketcode"];
                drExport["typename"] = row["typename"];
                drExport["ERPCODE"] = row["ERPCODE"];
                drExport["insummary"] = row["insummary"];
                drExport["outsummary"] = row["outsummary"];
                drExport["stocksummary"] = row["stocksummary"];//供应商编号             
                dtExport.Rows.Add(drExport);
                str_ymonth = row["ymonth"].ToString();
            }
            drExport = dtExport.NewRow();
            drExport["createDate"] = "";
            drExport["cticketcode"] = "";
            drExport["typename"] = "";
            drExport["ERPCODE"] = "本月合计";
            var result1 = (from s1 in dsSource.Tables[1].AsEnumerable()
                           where s1.Field<string>("ymonth") == str_ymonth
                           select s1).FirstOrDefault();
            if (result1 != null)
            {
                drExport["insummary"] = result1["summaryIn"];
                drExport["outsummary"] = result1["summaryOut"];
                drExport["stocksummary"] = result1["stocksummary"];//供应商编号    
            }
            else
            {
                drExport["insummary"] = "0.00";
                drExport["outsummary"] = "0.00";
                drExport["stocksummary"] = "0.00";
            }
            dtExport.Rows.Add(drExport);

            drExport = dtExport.NewRow();
            drExport["createDate"] = "";
            drExport["cticketcode"] = "";
            drExport["typename"] = "";
            drExport["ERPCODE"] = "合计";
            var result2 = (from s1 in dsSource.Tables[2].AsEnumerable()
                           where 1 == 1
                           select s1).FirstOrDefault();
            if (result2 != null)
            {
                drExport["insummary"] = result2["AllIn"];
                drExport["outsummary"] = result2["AllOut"];
                drExport["stocksummary"] = result2["stocksummary"];//供应商编号    
            }
            else
            {
                drExport["insummary"] = "0.00";
                drExport["outsummary"] = "0.00";
                drExport["stocksummary"] = "0.00";
            }
            dtExport.Rows.Add(drExport);
        }
        return dtExport;
    }
    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //
        if (this.txtCinvCode.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmIn_Out_Report_MSG2 + "！");//查询料号不能为空
            this.SetFocus(txtCinvCode);
            return false;
        }
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
            if (Convert.ToDateTime(txtDCREATETIMEFrom.Text) < Convert.ToDateTime(txtDCREATETIMETo.Text).AddMonths(-12))
            {
                this.Alert("日期区间不能大于十二个月！");//日期区间不能大于三个月！
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (CheckData())
        {                
            this.GridBind();
        }
    }
    public void GridBind()
    {
        DataSet dsSource = In_Out_Report.FrmInOut_ReprotNew(txtCinvCode.Text, txtcspec.Text, txtDCREATETIMEFrom.Text, txtDCREATETIMETo.Text);
        grdInOutDetails.DataSource = ToExcel();
        grdInOutDetails.DataBind();       
    }
    protected void grdInOutDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[e.Row.Cells.Count - 4].Text.Contains("合计"))
            {
                //e.Row.Cells[0].Visible = false;
                //e.Row.Cells[1].Visible = false;
                //e.Row.Cells[2].Visible = false;
                //e.Row.Cells[e.Row.Cells.Count - 4].ColumnSpan = 4;
                e.Row.Cells[e.Row.Cells.Count - 4].Style.Add("text-align", "right");// = HorizontalAlign.Right;
                //e.Row.Cells[e.Row.Cells.Count - 4].BackColor = System.Drawing.Color.Yellow;
                //e.Row.Cells[e.Row.Cells.Count - 3].BackColor = System.Drawing.Color.Yellow;
                //e.Row.Cells[e.Row.Cells.Count - 2].BackColor = System.Drawing.Color.Yellow;
                //e.Row.Cells[e.Row.Cells.Count - 1].BackColor = System.Drawing.Color.Yellow;
            }           
        }
    }

}