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
/// 描述: 电子Bin卡
/// 作者: 
/// 创建于: 2013-5-3 15:03:29
/// </summary>
public partial class FrmSTOCK_ElectBinCard : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack == false)
        {
            this.InitPage();
        }
    }


    #region IPageGrid 成员

    /// 绑定数据
    /// <summary>
    /// 绑定数据
    /// </summary>
    public void GridBind()
    {
        Bind("");
    }


    public IQueryable<V_STOCK_ElectBinCard> GetQueryList()
    {
        IGenericRepository<V_STOCK_ElectBinCard> conn = new GenericRepository<V_STOCK_ElectBinCard>(db);
        var caseList = from p in conn.Get()
                       select p;

        string dc = string.Empty;
        decimal d;
        if (caseList != null && caseList.Count() > 0)
        {

            if (txtDATECODE.Text != string.Empty)
            {
                dc = txtDATECODE.Text.Trim().Replace("-", "").Replace("/", "");
                if (decimal.TryParse(dc, out d))
                {
                    caseList = caseList.Where(x => x.DATECODE.HasValue && x.DATECODE.Value >= d);
                }
            }
            if (txtDATECODE2.Text != string.Empty)
            {
                dc = txtDATECODE2.Text.Trim().Replace("-", "").Replace("/", "");
                if (decimal.TryParse(dc, out d))
                {
                    caseList = caseList.Where(x => x.DATECODE.HasValue && x.DATECODE.Value <= d);
                }
            }

            if (txtCPOSITIONCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCPOSITIONCODE.Text.Trim()));
            }
            if (txtCINVCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCINVCODE.Text.Trim()));
            }
            if (txtSNCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.sncode) && x.sncode.Contains(txtSNCODE.Text.Trim()));
            }
            if (txtFurnace.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.furnaceno) && x.furnaceno.Contains(txtFurnace.Text.Trim()));
            }
            if (txtVENDOR.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cvendor) && x.cvendor.Contains(txtVENDOR.Text.Trim()));
            }
            if (txtPROCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.PRODUCTCODE) && x.PRODUCTCODE.Contains(txtPROCODE.Text.Trim()));
            }
            if (ddlWorkType.SelectedValue != string.Empty && ddlWorkType.SelectedValue != "0")
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.STOCKTYPE) && x.STOCKTYPE.Equals(ddlWorkType.SelectedValue));
            }
            if (txtCATIONS.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CSPECIFICATIONS) && x.CSPECIFICATIONS.Contains(txtCATIONS.Text.Trim()));
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

        grdSTOCK_BinCard.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdSTOCK_BinCard.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }



    public bool CheckData()
    {
        txtCINVCODE.Text = txtCINVCODE.Text.Trim().ToUpper();
        if (!txtCINVCODE.Text.IsNullOrEmpty())
        {
            IGenericRepository<BASE_PART> conn = new GenericRepository<BASE_PART>(db);
            BASE_PART bo = (from p in conn.Get()
                            where p.cpartnumber.Equals(txtCINVCODE.Text.Trim())
                            select p).FirstOrDefault();

            if (bo == null || bo.cpartnumber.IsNullOrEmpty())
            {
                this.Alert(Resources.Lang.FrmSTOCK_ElectBinCard_CinvcodeError);//输入料号错误，请输入正确的料号！
                this.SetFocus(txtCINVCODE);
                return false;
            }
        }

        //检查储位是否存在
        if (txtCPOSITIONCODE.Text.Trim().Length != 0)
        {
            txtCPOSITIONCODE.Text = txtCPOSITIONCODE.Text.Trim().ToUpper();

            IGenericRepository<BASE_CARGOSPACE> conn_bc = new GenericRepository<BASE_CARGOSPACE>(db);
            BASE_CARGOSPACE bc = (from p in conn_bc.Get()
                                  where p.cpositioncode.Equals(txtCPOSITIONCODE.Text.Trim())
                                  select p).FirstOrDefault();

            if (bc == null || bc.cpositioncode.IsNullOrEmpty())
            {
                this.Alert(Resources.Lang.FrmSTOCK_ElectBinCard_CpositioncodeError);//输入储位错误，请输入正确的储位或不输入储位！
                this.SetFocus(txtCPOSITIONCODE);
                return false;
            }

        }
        //检查仓库是否存在
        if (txtCWARENAME.Text.Trim().Length != 0)
        {
            txtCWARENAME.Text = txtCWARENAME.Text.Trim().ToUpper();

            IGenericRepository<BASE_WAREHOUSE> conn_bw = new GenericRepository<BASE_WAREHOUSE>(db);
            BASE_WAREHOUSE bw = (from p in conn_bw.Get()
                                 where p.cwarename.Equals(txtCWARENAME.Text.Trim())
                                 select p).FirstOrDefault();

            if (bw == null || bw.cwarename.IsNullOrEmpty())
            {
                this.Alert(Resources.Lang.FrmSTOCK_ElectBinCard_CwarehouseError);//输入仓库名称错误，请输入正确的仓库名称或不输入仓库名称！
                this.SetFocus(txtCWARENAME);
                return false;
            }
        }

        return true;
    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //this.grdSTOCK_BinCard.DataKeyNames = new string[]{"ID"};
    }

    #endregion

    /// 导出数据
    /// <summary>
    /// 导出数据
    /// </summary>
    /// <returns></returns>
    protected DataTable grdNavigatorSTOCK_BinCard_GetExportToExcelSource()
    {
        //FrmSTOCK_TradeQuery listQuery = new FrmSTOCK_TradeQuery();
        ////DataTable dtSource = listQuery.GetList_Bin(strGuid, txtCINVCODE.Text.Trim(), txtCPOSITIONCODE.Text.Trim(), txtCWARENAME.Text.Trim(), txtBeginDate.Text.Trim(), txtEndDate.Text.Trim(), ddlWorkType.SelectedValue, ddlOutorIn.SelectedValue, WebUserInfo.GetCurrentUser().UserNo, false, -1, -1);
        //DataTable dt = new DataTable();
        //dt = listQuery.GetList_OBO(txtDATECODE.Text.Trim(), txtDATECODE2.Text.Trim(),
        //                                                txtCWARENAME.Text.Trim(),
        //                                                txtCPOSITIONCODE.Text.Trim(),
        //                                                txtCINVCODE.Text.Trim(),
        //                                                txtSNCODE.Text.Trim(),
        //                                                txtFurnace.Text.Trim(),
        //                                                txtVENDOR.Text.Trim(),
        //                                                txtPROCODE.Text.Trim(),
        //                                                ddlWorkType.SelectedValue.ToString(),
        //                                                txtCATIONS.Text.Trim(),
        //                                                false, -1, 0);//
        //return dt;

        return null;

    }

    /// <summary>
    ///  换页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdSTOCK_BinCard_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    /// 换页
    /// <summary>
    /// 换页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdSTOCK_BinCard_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }
    /// 记录查询Guid
    /// <summary>
    /// 记录查询Guid
    /// </summary>
    public string strGuid
    {
        get
        {
            return ViewState["strGuid"].ToString();
        }
        set { ViewState["strGuid"] = value; }
    }

    /// 查询按钮
    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.CurrendIndex = 1;
        //获取查询标识
        strGuid = Guid.NewGuid().ToString();
        string msg = string.Empty;
        try
        {
            if (CheckData())
            {

                if (!string.IsNullOrEmpty(txtCINVCODE.Text.Trim())
                    && !string.IsNullOrEmpty(txtCPOSITIONCODE.Text.Trim())
                    && !string.IsNullOrEmpty(txtCWARENAME.Text.Trim()))
                {

                    var paraList = new List<string>();
                    paraList.Add("@P_GUID:" + strGuid);
                    paraList.Add("@P_Cinvcode:" + txtCINVCODE.Text.Trim());
                    paraList.Add("@P_CposCode:" + txtCPOSITIONCODE.Text.Trim());
                    paraList.Add("@P_CwareName:" + txtCWARENAME.Text.Trim());
                    paraList.Add("@P_TypCode:");
                    paraList.Add("@P_BeginTime:");
                    paraList.Add("@P_EndTime:");
                    paraList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);

                    string Rests = DBHelp.ExecuteProcReturnMsg("Proc_Elect_BinCard", paraList, "@P_return_Value");
                    if (Rests != "0")
                    {
                        Alert(Rests);
                        return;
                    }

                }
            }

            this.GridBind();
        }
        catch (Exception error)
        {
            this.Alert(error.Message);
        }
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdSTOCK_BinCard.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdSTOCK_BinCard.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdSTOCK_BinCard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)//判断此行是否是页尾，如果是则开始统计数据
        {
            //FrmSTOCK_TradeQuery listQuery = new FrmSTOCK_TradeQuery();
            //DataTable dtSum = listQuery.GetList_OBO(txtDATECODE.Text.Trim(),
            //                                             txtDATECODE2.Text.Trim(),
            //                                             txtCWARENAME.Text.Trim(),
            //                                             txtCPOSITIONCODE.Text.Trim(),
            //                                             txtCINVCODE.Text.Trim(),
            //                                             txtSNCODE.Text.Trim(),
            //                                             txtFurnace.Text.Trim(),
            //                                             txtVENDOR.Text.Trim(),
            //                                             txtPROCODE.Text.Trim(),
            //                                             ddlWorkType.SelectedValue.ToString(),
            //                                             txtCATIONS.Text.Trim(),
            //                                             true, 0, 0);

            //decimal total = GetQueryList().Sum(x => x.qty).Value;
            //e.Row.Cells[0].Text = Resources.Lang.FrmSTOCK_CurrentQueryList_HeJi; //合计         
            //e.Row.Cells[1].Text = total.ToString();
        }

    }

    protected void dsGrdSTOCK_BinCard_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdSTOCK_BinCard_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }

    /// <summary>
    /// 切换输入类型
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlWorkType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

