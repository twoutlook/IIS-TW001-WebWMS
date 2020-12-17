using System;
using System.Data;


public partial class Apps_FrmOrderCompIssue_Print : System.Web.UI.Page
{
    /// <summary>
    /// 记录ERPCODE
    /// </summary>
    public string printId
    {
        get
        {
            if (ViewState["printId"] != null)
            {
                return ViewState["printId"].ToString();
            }
            return "";
        }
        set { ViewState["printId"] = value; }
    }

    /// <summary>
    /// 標識
    /// </summary>
    public string pbz
    {
        get
        {
            if (ViewState["pbz"] != null)
            {
                return ViewState["pbz"].ToString();
            }
            return "";
        }
        set { ViewState["pbz"] = value; }
    }

    /// 料号
    /// <summary>
    /// 料号
    /// </summary>
    public string strCin
    {
        get
        {
            if (ViewState["strCin"] != null)
            {
                return ViewState["strCin"].ToString();
            }
            return "";
        }
        set { ViewState["strCin"] = value; }
    }

    /// 初始化页面
    /// <summary>
    /// 初始化页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["pbz"] != null)
        {
            pbz = Session["pbz"].ToString();
            //Session.Remove("cinvcode");
        }

        if (Session["cerpcode"] != null)
        {
            printId = Session["cerpcode"].ToString();
            //Session.Remove("cerpcode");
        }

        if (Session["cinvcode"] != null)
        {
            strCin = Session["cinvcode"].ToString();
            //Session.Remove("cinvcode");
        }
        if (pbz != "1")
        {
            printId = "null";
            strCin = "null";
        }
        WmsOrderCompIssue odc = new WmsOrderCompIssue();
        DataTable tb = odc.OrderCompIssue(printId, WmsWebUserInfo.GetCurrentUser().UserNo);
        tb.TableName = "TB_ORDER";

        DataTable tb_d = odc.OrderCompIssue_D(printId, strCin, WmsWebUserInfo.GetCurrentUser().UserNo);
        tb_d.TableName = "TB_ORDER_D";

        DataSet ds=new DataSet();

        ds.Tables.Add(tb_d);
        ds.Tables.Add(tb);

        OrderCompReport order = new OrderCompReport();
        order.DataSource = ds;
        OrderCompIssueReport.Report = order;
        OrderCompIssueReport.Report.FillDataSource();
        OrderCompIssueReport.DataBind();
    }
}