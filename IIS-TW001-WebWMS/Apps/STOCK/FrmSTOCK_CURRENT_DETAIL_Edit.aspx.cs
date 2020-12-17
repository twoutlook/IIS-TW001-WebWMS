using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;

/// <summary>
/// 描述: 入库管理-->FrmINASNEdit 页面后台类文件
/// 作者: 
/// 创建于: 2012-09-24 18:40:51
/// </summary>
public partial class FrmSTOCK_CURRENT_DETAIL_Edit : WMSBasePage
{

    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();
            ShowData();
        }
    }

    #region IPage 成员

    /// <summary>
    /// 初始化页面。主要做一下动作
    ///1、DropDownList,ListBox数据装载,
    ///2、新增按钮、删除的按钮的Java脚本注册等
    ///一般在PageLoad 事件调用，
    ///并且IsPostBack = false时
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('STOCK_CURRENT_DETAIL');return false;";
        //this.grdINASN.DataKeyNames = new string[] { "IDS" };
        Session["StockCurrentIndex"] = Request["StockCurrentIndex"] != null ? Request["StockCurrentIndex"].ToString() : string.Empty;
        string Figvalue = this.GetConFig("000002");
        if (Figvalue == "1")
        {
            //批号
            Label_BS1.Text = Resources.Lang.FrmSTOCK_CURRENT_DETAIL_Edit_BoxList;// "箱号列表";
            Label_BS2.Text = Resources.Lang.FrmSTOCK_CURRENT_DETAIL_Edit_Box + ":";// "箱号:";
        }
        else
        {
            Label_BS1.Text = Resources.Lang.WMS_Common_Element_SNTypeList;//"SN/箱/栈板列表";
            Label_BS2.Text = Resources.Lang.WMS_Common_Element_SNType + ":";//SN/箱/栈板
        }

    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        Stock_Current_Id = Request.QueryString["ID"];
        IGenericRepository<STOCK_CURRENT> con = new GenericRepository<STOCK_CURRENT>(context);
        var caseList = from p in con.Get()
                       where p.id == Stock_Current_Id
                       select p;
        if (caseList.ToList().Count() > 0)
        {
            STOCK_CURRENT entity = caseList.ToList().FirstOrDefault<STOCK_CURRENT>();
            txtWAREHOUSE_NO.Text = entity.cwarehouse;
            txtCPOSITIONCODE.Text = entity.cpositioncode;
            txtCPOSITION.Text = entity.cposition;
            //Note by Qamar 2020-11-23
            txtCINVCODE.Text = entity.cinvcode.Substring(0, entity.cinvcode.Length - 2);
            txtRANK_FINAL.Text = entity.cinvcode.Substring(entity.cinvcode.Length - 1, 1);
            if (txtRANK_FINAL.Text == "_")
                txtRANK_FINAL.Text = "";
            txtCINVNAME.Text = entity.cinvname;
            txtIQTY.Text = entity.iqty.ToString("f2");
            txtIOCCUPYQTY.Text = entity.ioccupyqty.ToString("f2");
        }
        this.btnSearch1_Click(this.btnSearch1, EventArgs.Empty);
        btnSearch_Click(this.btnSearch, EventArgs.Empty);

    }

    /// <summary>
    /// 库存ID
    /// </summary>
    public string Stock_Current_Id
    {
        get { return ViewState["Stock_Current_Id"].ToString(); }
        set { ViewState["Stock_Current_Id"] = value; }
    }

    public int CurrentIndex_BN
    {
        get
        {
            if (ViewState["CurrentIndex_BN"] == null)
            {
                ViewState["CurrentIndex_BN"] = 1;
            }
            return (int)ViewState["CurrentIndex_BN"];
        }
        set
        {
            ViewState["CurrentIndex_BN"] = value;
        }
    }

    #region IPageGrid 成员

    //获取DateCode
    public void GridBind()
    {
        IGenericRepository<Stock_Current_Detail_Fun> entity = new GenericRepository<Stock_Current_Detail_Fun>(context);
        var caseList = from p in entity.Get()
                       orderby p.datecode, p.qty
                       where 1 == 1
                       select p;

        if (Stock_Current_Id != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.Contains(Stock_Current_Id));

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = " " + Resources.Lang.WMS_Common_Pager_PageCount + ":<b>" + "</b>";//总页数
        grdINASN.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdINASN.DataBind();
    }
    //获取SN
    public void GridBindSN()
    {
        IGenericRepository<stock_current_sn_Fun> entity = new GenericRepository<stock_current_sn_Fun>(context);
        var caseList = from p in entity.Get()
                       orderby p.sncode
                       where 1 == 1
                       select p;

        if (Stock_Current_Id != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.stock_id) && x.stock_id.Contains(Stock_Current_Id));

        if (txtSNCode.Text.Trim() != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.sncode) && x.sncode.ToLower().Contains(txtSNCode.Text.Trim().ToLower()));
        }
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager2.RecordCount = caseList.Count();
            AspNetPager2.PageSize = this.PageSize;
        }
        AspNetPager2.CustomInfoHTML = " " + Resources.Lang.WMS_Common_Pager_PageCount + ":<b>" + "</b>";//总页数
        grdSNList.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdSNList.DataBind();
    }

    public void BindBN()
    {
        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>hidtrbn();</script>");
    }
    #endregion

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager2.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        this.CurrentIndex_BN = AspNetPager3.CurrentPageIndex;//索引同步
        BindBN();
    }
    protected void btnSearch1_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBindSN();
        BindBN();
    }

    //DateCode 行绑定
    protected void grdINASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

    //DateCode
    protected void dsgrdINASN_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    //SN 换行
    protected void grdSNList_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBindSN();
    }

    //SN 行判定
    protected void grdSNList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

}

