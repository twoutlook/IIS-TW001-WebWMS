using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;

/// <summary>
/// 描述: 出库管理-->FrmOUTASNList 页面后台类文件
/// 作者: --chenlei
/// 创建于: 2012-09-27 20:28:11
/// 
/// Modify by Roger
/// Time: 2013-4-10 11:05:26
/// Mark: 20130410110526
/// Reason: 增加功能点限制
/// </summary>
public partial class FrmDigitalSignageTest : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        //this.FunctionNo = ""; //TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
    }

    #region IPageGrid 成员

    public void GridBind()
    {

    }

    public bool CheckData()
    {
        if (this.txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmDigitalSignageTest_MSG1 + "！");//制单日期项不是有效的日期
                this.SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        if (this.txtDCREATETIMETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmDigitalSignageTest_MSG2 + "！");//到项不允许空
            this.SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (this.txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmDigitalSignageTest_MSG3 + "！");//到项不是有效的日期
                this.SetFocus(txtDCREATETIMETo);
                return false;
            }
        }
        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";

        //本页面打开新增窗口
        //新建出库通知单
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmOUTASNEdit.aspx", SYSOperation.New, "&IsSpecialPage=0&IsSpecialWIP_Issue=0") + "','"+Resources.Lang.FrmDigitalSignageTest_MSG4+"','OUTASN');return false;";
        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmOUTASNEdit.aspx", SysOperation.New,""),800,600);

    }

    #endregion


    protected void grdOUTASN_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
    }

    protected void grdOUTASN_PageIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
    //    FrmDigitalSignageQuery query = new FrmDigitalSignageQuery();

    //    DataTable dtSource = query.GetList(txtCERPCODE.Text.Trim(), txtDCREATETIMEFrom.Text.Trim(), txtDCREATETIMETo.Text.Trim(),"","",false,-1,-1);
    //    this.grdOUTASN.DataSource = dtSource;
    //    this.grdOUTASN.DataBind();
          CurrendIndex = 1;
          Bind("");
    }


    public IQueryable<v_digitalsignage> GetQueryList()
    {
        IGenericRepository<v_digitalsignage> conn = new GenericRepository<v_digitalsignage>(db);
        var caseList = from p in conn.Get()
                       select p;

        DateTime d;
        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCERPCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ERPCODE) && x.ERPCODE.Contains(txtCERPCODE.Text.Trim()));
            }
            if (txtDCREATETIMEFrom.Text != string.Empty)
            {
                if(DateTime.TryParse(txtDCREATETIMEFrom.Text,out d))
                {
                    caseList = caseList.Where(x => x.start_date_mo.HasValue && x.start_date_mo.Value>=d);
                }
            }

            if (txtDCREATETIMETo.Text != string.Empty)
            {
                if (DateTime.TryParse(txtDCREATETIMETo.Text, out d))
                {
                    caseList = caseList.Where(x => x.start_date_mo.HasValue && x.start_date_mo.Value < d.AddDays(1));
                }
            }
        }
        return caseList;
    }

    public void Bind(string sortStr)
    {
        var caseList = GetQueryList();

        if (caseList != null && caseList.Count() > 0)
        {
            //按照列名排序
            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }

        grdOUTASN.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdOUTASN.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }



    protected void BtnNew_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        
    }

    private string GetKeyIDS(int rowIndex)
    {
        //string strKeyId = "";
        //for (int i = 0; i < this.grdOUTASN.DataKeyNames.Length; i++)
        //{
        //    strKeyId += this.grdOUTASN.DataKeys[rowIndex].Values[i].ToString() + ",";
        //}
        //strKeyId = strKeyId.TrimEnd(',');
        return "";
    }

    private Dictionary<string, string> dict = new Dictionary<string, string>();

    protected void grdOUTASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void dsGrdOUTASN_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        
    }

    protected void dsGrdOUTASN_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        
    }

    /// <summary>
    /// 生成出库单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnCreateOutBill_Click(object sender, EventArgs e)
    {
        
    }
}

