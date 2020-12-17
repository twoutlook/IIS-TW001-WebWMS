using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraReports.Web;
using Resources;

public partial class Apps_BAR_Form_SNBarCode_Print : System.Web.UI.Page
{

    public string printId
    {
        get { return ViewState["printId"].ToString(); }
        set { ViewState["printId"] = value; }
    }

    ///// <summary>
    ///// 记录code类型
    ///// </summary>
    //public string CodeType
    //{
    //    get { return ViewState["CodeType"].ToString(); }
    //    set { ViewState["CodeType"] = value; }
    //}

    ///// <summary>
    ///// 记录SN是16码还是19码
    ///// </summary>
    public string SNLength
    {
        get { return Session["SNLength"].ToString(); }
        set { Session["SNLength"] = value; }
    }
    public DataTable SNTable
    {
        get { return Session["DT"] as DataTable; }
        set { Session["DT"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

//        printId = Request.QueryString["ID"];
//        var sql =
//         string.Format(@"select 
//                  sn_code SN, 
//                  cinvcode CINVCODE, 
//                  cast(quantity as int) QTY, 
//                  bar_type, 
//                  CPOSITIONCODE CPOSITIONCODE, 
//                  po_number PO, 
//                VENDOR_NAME Supplier,
//                 vendor,
//                 datecode,
//               case when sn_code!=null or sn_code!='' then RIGHT(sn_code,1) ELSE 1  END AS sersnum
//             from base_bar_sn bss where bss.id in ({0})", printId);

//        DataTable tb = DBHelp.ExecuteToDataTable(sql);
       // tb.TableName = "SNList";
        //SNTable.TableName = "SNList";

        DataTable tb= SNTable;
        SNQR_Report SN_X = new SNQR_Report();
        SN_X.DataSource = tb;
        SN_XRP.Report = SN_X;
        SN_XRP.Report.FillDataSource();
        SN_XRP.DataBind();


      

        #region

        //    if (SNTable == null)
        //    {
        //        SNTable=new DataTable();
        //    }
        //    if (Session["DT"] != null)
        //    {
        //        SNTable = (DataTable)Session["DT"];
        //        foreach (DataColumn dc in SNTable.Columns)
        //        {
        //            dc.ColumnName = dc.ColumnName.ToUpper();
        //        }
        //        Session.Remove("DT");
        //    }
        //    if (Session["SNLength"] != null)
        //    {
        //        SNLength = Session["SNLength"].ToString();
        //        Session.Remove("SNLength");
        //    }
        //    if (Session["CodeType"] != null)
        //    {
        //        CodeType = Session["CodeType"].ToString();
        //        Session.Remove("CodeType");
        //    }
        //if (SNLength == "16" && CodeType=="128")
        //{
        //    SN100X80_Report SN_X = new SN100X80_Report();
        //    SN_X.DataSource = SNTable;
        //    SN_XRP.Report = SN_X;
        //    SN_XRP.Report.FillDataSource();
        //    SN_XRP.DataBind();
        //} 
        //else if (SNLength == "16" && CodeType == "39")
        //{
        //    SN100X80_39Report SN_X = new SN100X80_39Report();
        //    SN_X.DataSource = SNTable;
        //    SN_XRP.Report = SN_X;
        //    SN_XRP.Report.FillDataSource();
        //    SN_XRP.DataBind();
        //}
        //else if (SNLength == "19" && CodeType == "128")
        //{
        //    SN70X25_Report SN_X = new SN70X25_Report();
        //    SN_X.DataSource = SNTable;
        //    SN_XRP.Report = SN_X;
        //    SN_XRP.Report.FillDataSource();
        //    SN_XRP.DataBind();
        //}
        //else if (SNLength == "19" && CodeType == "39")
        //{
        //    SN70X25_39Report SN_X = new SN70X25_39Report();
        //    SN_X.DataSource = SNTable;
        //    SN_XRP.Report = SN_X;
        //    SN_XRP.Report.FillDataSource();
        //    SN_XRP.DataBind();
        //}
        #endregion

    }
}