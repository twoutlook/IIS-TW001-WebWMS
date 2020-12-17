using System;
using System.Data;


public partial class Apps_FrmStock_CheckDiff_Print : System.Web.UI.Page
{
    #region Init
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

    /// <summary>
    /// 盘点单号
    /// </summary>
    public string pcticketcode
    {
        get
        {
            if (ViewState["pcticketcode"] != null)
            {
                return ViewState["pcticketcode"].ToString();
            }
            return "";
        }
        set { ViewState["pcticketcode"] = value; }
    }

    /// <summary>
    /// 盘点单名称
    /// </summary>
    public string pcheckname
    {
        get
        {
            if (ViewState["pcheckname"] != null)
            {
                return ViewState["pcheckname"].ToString();
            }
            return "";
        }
        set { ViewState["pcheckname"] = value; }
    }

    /// <summary>
    /// 差异类型
    /// </summary>
    public string pchecktype
    {
        get
        {
            if (ViewState["pchecktype"] != null)
            {
                return ViewState["pchecktype"].ToString();
            }
            return "";
        }
        set { ViewState["pchecktype"] = value; }
    }

    /// <summary>
    /// 仓库编码
    /// </summary>
    public string pwareno
    {
        get
        {
            if (ViewState["pwareno"] != null)
            {
                return ViewState["pwareno"].ToString();
            }
            return "";
        }
        set { ViewState["pwareno"] = value; }
    }

    /// <summary>
    /// 储位
    /// </summary>
    public string pcposition
    {
        get
        {
            if (ViewState["pcposition"] != null)
            {
                return ViewState["pcposition"].ToString();
            }
            return "";
        }
        set { ViewState["pcposition"] = value; }
    }

    /// <summary>
    /// 料号
    /// </summary>
    public string pcinvcode
    {
        get
        {
            if (ViewState["pcinvcode"] != null)
            {
                return ViewState["pcinvcode"].ToString();
            }
            return "";
        }
        set { ViewState["pcinvcode"] = value; }
    }

    /// <summary>
    /// oracle盘点单号
    /// </summary>
    public string pOracle
    {
        get
        {
            if (ViewState["pOracle"] != null)
            {
                return ViewState["pOracle"].ToString();
            }
            return "";
        }
        set { ViewState["pOracle"] = value; }
    }
    /// <summary>
    /// 开始时间
    /// </summary>
    public string pdatefrom
    {
        get
        {
            if (ViewState["pdatefrom"] != null)
            {
                return ViewState["pdatefrom"].ToString();
            }
            return "";
        }
        set { ViewState["pdatefrom"] = value; }
    }

    /// <summary>
    /// 结束时间
    /// </summary>
    public string pdateto
    {
        get
        {
            if (ViewState["pdateto"] != null)
            {
                return ViewState["pdateto"].ToString();
            }
            return "";
        }
        set { ViewState["pdateto"] = value; }
    }
    
    #endregion

    /// 初始化页面
    /// <summary>
    /// 初始化页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["pcticketcode"] != null)
        {
            pcticketcode = Session["pcticketcode"].ToString();
            //Session.Remove("cinvcode");
        }
        if (Session["pcheckname"] != null)
        {
            pcheckname = Session["pcheckname"].ToString();
            //Session.Remove("cinvcode");
        }
        if (Session["pchecktype"] != null)
        {
            pchecktype = Session["pchecktype"].ToString();
            //Session.Remove("cinvcode");
        }
        if (Session["pwareno"] != null)
        {
            pwareno = Session["pwareno"].ToString();
            //Session.Remove("cinvcode");
        }
        if (Session["pcinvcode"] != null)
        {
            pcinvcode = Session["pcinvcode"].ToString();
            //Session.Remove("cinvcode");
        }
        if (Session["pdatefrom"] != null)
        {
            pdatefrom = Session["pdatefrom"].ToString();
            //Session.Remove("cinvcode");
        }
        if (Session["pdateto"] != null)
        {
            pdateto = Session["pdateto"].ToString();
            //Session.Remove("cinvcode");
        }
        if (Session["pcposition"] != null)
        {
            pcposition = Session["pcposition"].ToString();
            //Session.Remove("cinvcode");
        }
        if (Session["pOracle"] != null)
        {
            pOracle = Session["pOracle"].ToString();
            //Session.Remove("cinvcode");
        }
        

        if (Session["pbz"] != null)
        {
            pbz = Session["pbz"].ToString();
            //Session.Remove("cinvcode");
        }
         //ReportDataSource da=new ReportDataSource();
         RPDataSource da = new RPDataSource();
         // The page is first loaded, no data is displayed
         if (pbz != "1")
        {
            pcticketcode = "null";
            pcheckname = "null";
        }
        DataTable tb_h = null;
        DataTable tb = null;
        tb_h = da.GetCheckDiffHeader(pcheckname);
        
        tb_h.TableName = "TB_CHECKHEAD";

        tb = da.GetCheckDiff(pcticketcode, pcheckname, pchecktype, pwareno, pcposition, pcinvcode,pOracle, pdatefrom,
                                    pdateto);
        if (tb != null)
        {
            tb.TableName = "TB_CHECKDIFF";


            DataSet ds = new DataSet();

            ds.Tables.Add(tb);
            ds.Tables.Add(tb_h);

            Stock_CheckDiffReport order = new Stock_CheckDiffReport();
            order.DataSource = ds;
            CheckDiffReport.Report = order;
            CheckDiffReport.Report.FillDataSource();
            CheckDiffReport.DataBind();
        
        }
        
    }
}