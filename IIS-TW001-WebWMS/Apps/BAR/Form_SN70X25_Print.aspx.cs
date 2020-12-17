using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraReports.Web;
using Resources;

public partial class Apps_BAR_Form_SN70X25_Print : System.Web.UI.Page
{

    /// <summary>
    /// 记录code类型
    /// </summary>
    public string CodeType
    {
        get { return ViewState["CodeType"].ToString(); }
        set { ViewState["CodeType"] = value; }
    }

    /// <summary>
    /// 记录SN是16码还是19码
    /// </summary>
    public string SNLength
    {
        get { return ViewState["SNLength"].ToString(); }
        set { ViewState["SNLength"] = value; }
    }
    public DataTable SNTable
    {
        get { return ViewState["SNTable"] as DataTable; }
        set { ViewState["SNTable"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SNTable == null)
        {
            SNTable=new DataTable();
        }
        if (Session["DT"] != null)
        {
            SNTable = (DataTable)Session["DT"];
            foreach (DataColumn dc in SNTable.Columns)
            {
                dc.ColumnName = dc.ColumnName.ToUpper();
            }
            Session.Remove("DT");
        }
        if (Session["SNLength"] != null)
        {
            SNLength = Session["SNLength"].ToString();
            Session.Remove("SNLength");
        }
        if (Session["CodeType"] != null)
        {
            CodeType = Session["CodeType"].ToString();
            Session.Remove("CodeType");
        }
        SNTable.TableName = "SNList";

        if (SNLength == "16" && CodeType=="128")
        {
            SN100X80_Report SN_X = new SN100X80_Report();
            SN_X.DataSource = SNTable;
            SN_XRP.Report = SN_X;
            SN_XRP.Report.FillDataSource();
            SN_XRP.DataBind();
        } 
        else if (SNLength == "16" && CodeType == "39")
        {
            SN100X80_39Report SN_X = new SN100X80_39Report();
            SN_X.DataSource = SNTable;
            SN_XRP.Report = SN_X;
            SN_XRP.Report.FillDataSource();
            SN_XRP.DataBind();
        }
        else if (SNLength == "19" && CodeType == "128")
        {
            SN70X25_Report SN_X = new SN70X25_Report();
            SN_X.DataSource = SNTable;
            SN_XRP.Report = SN_X;
            SN_XRP.Report.FillDataSource();
            SN_XRP.DataBind();
        }
        else if (SNLength == "19" && CodeType == "39")
        {
            SN70X25_39Report SN_X = new SN70X25_39Report();
            SN_X.DataSource = SNTable;
            SN_XRP.Report = SN_X;
            SN_XRP.Report.FillDataSource();
            SN_XRP.DataBind();
        }
        
    }
}