using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq;

/// <summary>
/// 描述: DateCode 锁定明细
/// 作者: CQ
/// 创建于: 2013-9-27 16:36:14
/// </summary>
public partial class FrmDateCode_Lock_Detail : WMSBasePage
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
        GetCinvCPosition();
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('STOCK_CURRENT_DETAIL');return false;";
        this.grdDateCode.DataKeyNames = new string[] { "IDS" };
        Session["StockCurrentIndex"] = Request["StockCurrentIndex"] != null ? Request["StockCurrentIndex"].ToString() : string.Empty;
    }

    #endregion
    /// <summary>
    /// 料号
    /// </summary>
    public string StrCinvcode
    {
        get
        {
            if (ViewState["StrCinvcode"] != null)
            {
                return ViewState["StrCinvcode"].ToString();
            }
            return "";
        }
        set { ViewState["StrCinvcode"] = value; }
    }
    /// <summary>
    /// 储位
    /// </summary>
    public string StrCpositionCode
    {
        get
        {
            if (ViewState["StrCpositionCode"] != null)
            {
                return ViewState["StrCpositionCode"].ToString();
            }
            return "";
        }
        set { ViewState["StrCpositionCode"] = value; }
    }

    /// 获取料号和储位
    /// <summary>
    /// 获取料号和储位
    /// </summary>
    public void GetCinvCPosition()
    {
        IGenericRepository<STOCK_CURRENT> con = new GenericRepository<STOCK_CURRENT>(context);
        var caseList = from p in con.Get()
                       where p.id == Request.QueryString["ID"].Trim()
                       select p;
        if (caseList.ToList().Count() > 0)
        {
            STOCK_CURRENT entity = caseList.ToList().FirstOrDefault<STOCK_CURRENT>();
            if (caseList.Count() > 0)
            {
                StrCinvcode = entity.cinvcode;
                StrCpositionCode = entity.cpositioncode;
            }
        }
    }

    /// <summary>
    /// 库存ID
    /// </summary>
    public string Stock_Current_Id
    {
        get { return ViewState["Stock_Current_Id"].ToString(); }
        set { ViewState["Stock_Current_Id"] = value; }
    }


    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {

        IGenericRepository<DATECODE_STOCK_CURRENT> con = new GenericRepository<DATECODE_STOCK_CURRENT>(context);
        var caseList = from p in con.Get()
                       where p.cinvcode == StrCinvcode && p.cpositioncode == StrCpositionCode
                       select p;
        if (caseList.Count() > 0)
        {
            DATECODE_STOCK_CURRENT entity = caseList.ToList().FirstOrDefault<DATECODE_STOCK_CURRENT>();
            txtWAREHOUSE_NO.Text = entity.cwarehouse;
            txtCPOSITIONCODE.Text = entity.cpositioncode;
            txtCPOSITION.Text = entity.cposition;
            txtCINVCODE.Text = entity.cinvcode;
            txtCINVNAME.Text = entity.cinvname;
            txtIQTY.Text = entity.iqty.ToString();
            txtIOCCUPYQTY.Text = entity.ioccupyqty.ToString();
        }
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }


    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<DATECODE_STOCK_CURRENT_DETAIL> entity = new GenericRepository<DATECODE_STOCK_CURRENT_DETAIL>(context);
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
        grdDateCode.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdDateCode.DataBind();
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

    protected void grdDateCode_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        strKeyId = this.grdDateCode.DataKeys[rowIndex].Values[0].ToString();//strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdDateCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
        }
    }

    protected void dsgrdDateCode_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        return true;
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

        foreach (GridViewRow item in this.grdDateCode.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string ids = this.grdDateCode.DataKeys[item.RowIndex][0].ToString();

                //控件选中且集合中不存在添加
                if (cbo.Checked && !SelectIds.ContainsKey(ids))
                {
                    SelectIds.Add(ids, ids);
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(ids))
                {
                    SelectIds.Remove(ids);
                }
            }
        }
    }

    protected void grdDateCode_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }
}

