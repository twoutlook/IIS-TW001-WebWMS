using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_OUT_FrmSeralList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.InitPage();
        }
    }

    /// <summary>
    /// 页面初始化
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdSerial.DataKeyNames = new string[] { "ID" };

        //出入类型
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "ACTIONSCOPE", false, -1, -1), this.drIType, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        //状态
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "IN_PO", false, -1, -1), dplCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
  
        Help.RadioButtonDataBind(SysParameterList.GetList("", "", "MonthOrWeek", false, -1, -1), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");
    }

    /// <summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;//默认第一页
            AspNetPager1.CurrentPageIndex = 1;
        }
        IsFirstPage = true;
        this.GridBind();
    }


    /// <summary>
    /// 分页控件翻页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    /// <summary>
    /// 列表数据绑定
    /// </summary>
    public void GridBind()
    {
        using (var modContext = this.context)
        {
            var queryList = from p in modContext.V_SerialNumberInfo
                            orderby p.dcreatetime descending
                            where 1 == 1
                            select p;


            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cinvcode.Contains(txtCinvcode.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCTICKETCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtCTICKETCODE.Text));
            }
            if (dplCSTATUS.SelectedValue != "")
            {
                queryList = queryList.Where(x => x.cstatus.ToString().Equals(dplCSTATUS.SelectedValue));
            }
            if (!string.IsNullOrEmpty(txtCERPCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtCERPCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
            {
                queryList = queryList.Where(x => x.serialno.Contains(txtSerialNo.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
            {
                DateTime createTimeFrom = Convert.ToDateTime(txtDCREATETIMEFrom.Text.Trim());
                queryList = queryList.Where(x => x.dcreatetime != null && x.dcreatetime >= createTimeFrom);
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMETo.Text.Trim()))
            {
                DateTime createTimeTo = Convert.ToDateTime(txtDCREATETIMETo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.dcreatetime != null && x.dcreatetime < createTimeTo);
            }
            
            if (drIType.SelectedValue != "")
            {
                queryList = queryList.Where(x => x.dtype.ToString().Equals(drIType.SelectedValue));
            }

            if (queryList != null)
            {
                AspNetPager1.RecordCount = queryList.Count();
                AspNetPager1.PageSize = this.PageSize;
            }

            var data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
            flagList.Add(new Tuple<string, string>("dtype", "ACTIONSCOPE"));
            flagList.Add(new Tuple<string, string>("cstatus", "IN_PO"));

            var srcdata = GetGridSourceDataByList(data, flagList);

            grdSerial.DataSource = srcdata;
            grdSerial.DataBind();
        }
    }

    /// <summary>
    /// 列表行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdSerial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
}