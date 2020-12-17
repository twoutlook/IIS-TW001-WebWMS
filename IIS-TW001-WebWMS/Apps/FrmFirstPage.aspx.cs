using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DreamTek.ASRS.Business.Tools;
using System.Configuration;

    /// <summary>
    /// FrmFirstPage ��ժҪ˵����
    /// </summary>
public partial class FrmFirstPage : System.Web.UI.Page
{
    public int P_PreInNoticeCount = 0;
    public int P_IQCCount = 0;
    public int P_InNoticeCount = 0;
    public int P_InNoticeChangeCount = 0;
    public int P_InNoticeChangeVerifyCount = 0;
    public int P_AddedGuideCount = 0;
    public int P_InCount = 0;
    public int P_OutNoticeCount = 0;
    public int P_OutNoticeChangeCount = 0;
    public int P_OutNoticeChangeVerifyCount = 0;
    public int P_MinusGuideCount = 0;
    public int P_OutCount = 0;
    public int P_AllocateCount = 0;
    public int P_CycleCheckCount = 0;
    public int P_PhysicalcheckCount = 0;
    public int P_TaskCount = 0;
    public int P_TaskCount2 = 0;

    public string RightUrl
    {
        get
        {
            if (ViewState["RightUrl"] != null)
            {
                return ViewState["RightUrl"].ToString();
            }
            return "";
        }
        set
        {
            ViewState["RightUrl"] = value;
        }
    }

    public string CompayNO
    {
        get
        {
            if (ViewState["CompayNO"] != null)
            {
                return ViewState["CompayNO"].ToString();
            }
            return string.Empty;
        }
        set
        {
            ViewState["CompayNO"] = value;
        }
    }

    public string ProjectNO
    {
        get
        {
            if (ViewState["ProjectNO"] != null)
            {
                return ViewState["ProjectNO"].ToString();
            }
            return string.Empty;
        }
        set
        {
            ViewState["ProjectNO"] = value;
        }
    }

    public string Language
    {
        get
        {
            if (ViewState["Language"] != null)
            {
                return ViewState["Language"].ToString();
            }
            return "zh-CN";
        }
        set
        {
            ViewState["Language"] = value;
        }
    }

    public string UserNO
    {
        get
        {
            var userNO = WmsWebUserInfo.GetCurrentUser().UserNo;
            return userNO;
        }

    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        imgUrl.Attributes.Remove("href");
        imgUrl.Attributes.Add("href", "../Layout/multi/css/" + Resources.Lang.WMS_Common_MultiUrl + "/backImage.css?v=" + DateTime.Now.ToString("yyyyMMdd"));

        //ExecProc_FirstPage_Count("", ref P_PreInNoticeCount, ref P_IQCCount, ref P_InNoticeCount, ref P_InNoticeChangeCount, ref P_InNoticeChangeVerifyCount, ref P_AddedGuideCount, ref P_InCount, ref P_OutNoticeCount, ref P_OutNoticeChangeCount, ref P_OutNoticeChangeVerifyCount, ref P_MinusGuideCount, ref P_OutCount, ref P_AllocateCount, ref P_CycleCheckCount, ref P_PhysicalcheckCount
        //    , ref P_TaskCount, ref P_TaskCount2);
        string sql = string.Format("exec Proc_FirstPage_Count '{0}' ", WmsWebUserInfo.GetCurrentUser().UserNo);
        DataTable dt = DBHelp.ExecuteToDataTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            P_PreInNoticeCount = dt.Rows[0]["PreInNoticeCount"].ToString().ToInt();
            P_IQCCount = dt.Rows[0]["IQCCount"].ToString().ToInt();
            P_InNoticeCount = dt.Rows[0]["InNoticeCount"].ToString().ToInt();
            P_InNoticeChangeCount = dt.Rows[0]["InNoticeChangeCount"].ToString().ToInt();
            P_InNoticeChangeVerifyCount = dt.Rows[0]["InNoticeChangeVerifyCount"].ToString().ToInt();
            P_AddedGuideCount = dt.Rows[0]["AddedGuideCount"].ToString().ToInt();
            P_InCount = dt.Rows[0]["InCount"].ToString().ToInt();
            P_OutNoticeCount = dt.Rows[0]["OutNoticeCount"].ToString().ToInt();
            P_OutNoticeChangeCount = dt.Rows[0]["OutNoticeChangeCount"].ToString().ToInt();
            P_OutNoticeChangeVerifyCount = dt.Rows[0]["OutNoticeChangeVerifyCount"].ToString().ToInt();
            P_MinusGuideCount = dt.Rows[0]["MinusGuideCount"].ToString().ToInt();
            P_OutCount = dt.Rows[0]["OutCount"].ToString().ToInt();
            P_AllocateCount = dt.Rows[0]["AllocateCount"].ToString().ToInt();
            P_CycleCheckCount = dt.Rows[0]["CycleCheckCount"].ToString().ToInt();
            P_PhysicalcheckCount = dt.Rows[0]["PhysicalcheckCount"].ToString().ToInt();
            P_TaskCount = dt.Rows[0]["TaskAllCount"].ToString().ToInt();
            P_TaskCount2 = dt.Rows[0]["TaskAllCount2"].ToString().ToInt();
        }
        if (!IsPostBack)
        {
            #region 获取权限菜单，并给JS赋值
            if (Session["RightMenuList"] == null)
            {
                string buttonUrl = ConfigurationManager.AppSettings["RightUrl"] + "RightsAPI/GetBtnListByUserJson";
                RightUrl = ConfigurationManager.AppSettings["RightUrl"] + "RightsAPI/GetMenuListByUserJson";
                CompayNO = ConfigurationManager.AppSettings["CompayNO"];
                ProjectNO = ConfigurationManager.AppSettings["ProjectNO"];
                //1.读取菜单列表
                string url = string.Format(@"{0}?userno={1}&companyno={2}&projectno={3}&language={4}", RightUrl, UserNO, CompayNO, ProjectNO, Language);
                string jsonStr = WebPageExtension.ExecuteGetUrl(url);
                string script = string.Format(@" var RightMenuList = {0}; ", jsonStr);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "addScript", script, true);
            }
            else {
                string jsonStr = Session["RightMenuList"].ToString();
                string script = string.Format(@" var RightMenuList = {0}; ", jsonStr);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "addScript", script, true);
            }
            #endregion
        }
    }

    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN���õ����� ASP.NET Web ���������������ġ�
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
    /// �˷��������ݡ�
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion

    /// <summary>
    /// 执行统计方法调用存储过程
    /// </summary>
    /// <param name="P_UserNo">操作人id</param>
    /// <param name="P_PreInNoticeCount">预入库通知单_未操作数量</param>
    /// <param name="P_IQCCount">IQC检验_未操作数量</param>
    /// <param name="P_InNoticeCount">入库通知单_未操作数量</param>
    /// <param name="P_InNoticeChangeCount">入库通知变更单_未操作数量</param>
    /// <param name="P_InNoticeChangeVerifyCount">入库通知变更单审核_未操作数量</param>
    /// <param name="P_AddedGuideCount">上架指引_未操作数量</param>
    /// <param name="P_InCount">入库单_未操作数量</param>
    /// <param name="P_OutNoticeCount">出库通知单_未操作数量</param>
    /// <param name="P_OutNoticeChangeCount">出库通知变更单_未操作数量</param>
    /// <param name="P_OutNoticeChangeVerifyCount">出库通知变更单审核_未操作数量</param>
    /// <param name="P_MinusGuideCount">检货指引_未操作数量</param>
    /// <param name="P_OutCount">出库单_未操作数量</param>
    /// <param name="P_AllocateCount">调拨单_未操作数量</param>
    /// <param name="P_CycleCheckCount">循环盘点单_未操作数量</param>
    /// <param name="P_PhysicalcheckCount">物理盘点单_未操作数量</param>
    public void ExecProc_FirstPage_Count(string P_UserNo, ref int P_PreInNoticeCount, ref int P_IQCCount, ref int P_InNoticeCount, ref int P_InNoticeChangeCount, ref int P_InNoticeChangeVerifyCount, ref int P_AddedGuideCount, ref int P_InCount, ref int P_OutNoticeCount, ref int P_OutNoticeChangeCount, ref int P_OutNoticeChangeVerifyCount, ref int P_MinusGuideCount, ref int P_OutCount, ref int P_AllocateCount, ref int P_CycleCheckCount, ref int P_PhysicalcheckCount
        , ref int P_TaskCount, ref int P_TaskCount2)
    {
        //Proc_FirstPage_Count proc = new Proc_FirstPage_Count();
        //proc.P_UserNo = P_UserNo;
        //proc.Execute();
        //P_PreInNoticeCount = proc.P_PreInNoticeCount;
        //P_IQCCount = proc.P_IQCCount;
        //P_InNoticeCount = proc.P_InNoticeCount;
        //P_InNoticeChangeCount = proc.P_InNoticeChangeCount;
        //P_InNoticeChangeVerifyCount = proc.P_InNoticeChangeVerifyCount;
        //P_AddedGuideCount = proc.P_AddedGuideCount;
        //P_InCount = proc.P_InCount;
        //P_OutNoticeCount = proc.P_OutNoticeCount;
        //P_OutNoticeChangeCount = proc.P_OutNoticeChangeCount;
        //P_OutNoticeChangeVerifyCount = proc.P_OutNoticeChangeVerifyCount;
        //P_MinusGuideCount = proc.P_MinusGuideCount;
        //P_OutCount = proc.P_OutCount;
        //P_AllocateCount = proc.P_AllocateCount;
        //P_CycleCheckCount = proc.P_CycleCheckCount;
        //P_PhysicalcheckCount = proc.P_PhysicalcheckCount;
        //P_TaskCount = proc.P_TaskCount;
        //P_TaskCount2 = proc.P_TaskCount2;
    }
}
