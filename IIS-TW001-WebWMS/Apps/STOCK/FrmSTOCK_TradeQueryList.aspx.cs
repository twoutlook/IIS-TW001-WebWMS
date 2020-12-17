using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Business.SP.ProcedureModel;
using DreamTek.ASRS.Business.SP;
using System.Data.Entity.SqlServer;
/// <summary>
/// 描述: 期间库存-->FrmSTOCK_DURATIONList 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-10-17 15:48:44
/// </summary>
public partial class FrmSTOCK_TradeQueryList : WMSBasePage
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.grdSTOCK_DURATION.Columns[1].Visible = false;
        if (Page.IsPostBack == false)
        {
            BindingType();
            this.InitPage();
            this.txtBeginDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }


    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<TEMP_INANDOUTBILL> entity = new GenericRepository<TEMP_INANDOUTBILL>(context);
        var caseList = from p in entity.Get()
                       orderby p.dcreatetime descending
                       where 1 == 1
                       select p;
        if (txtErpCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtErpCode.Text));
        if (ddlOutorIn.SelectedValue != "")
        {
            caseList = caseList.Where(x => x.typecode != null && x.typecode.Trim().ToString() == (ddlOutorIn.SelectedValue).Trim().ToString());
        }

        if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()))
        {
            DateTime st = Convert.ToDateTime(txtBeginDate.Text + " 00:00:01");
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtBeginDate.Text.Trim(), x.dcreatetime) >= 0);
        }

        if (txtEndDate.Text != string.Empty)
        {
            DateTime ed = Convert.ToDateTime(txtEndDate.Text + " 23:59:59");
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtEndDate.Text.Trim(), x.dcreatetime) <= 0);
        }

        if (txtCPOSITIONCODE.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCPOSITIONCODE.Text));
        }
        if (txtCINVCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCINVCODE.Text));

        //02-11-2020 by Qamar
        if (txtRank_Final.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.EndsWith("-" + txtRank_Final.Text.Trim()));

        if (txtCWARENAME.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwarename) && x.cwarename.Contains(txtCWARENAME.Text.Trim()));
        if (ddlWorkType.SelectedValue != "")
        {
            string ddl = ddlWorkType.SelectedValue.ToString();
            caseList = caseList.Where(x => x.worktype.Trim() == ddl);
        }      
        string userno = WmsWebUserInfo.GetCurrentUser().UserNo;
        caseList = caseList.Where(x => !string.IsNullOrEmpty(x.createuser) && x.createuser.Contains(userno));
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = " " + Resources.Lang.WMS_Common_Pager_PageCount + ":<b>" + "</b>";//总页数
        var listResult = GetPageSize(caseList, PageSize, CurrendIndex).ToList();      
        var source1 = from p in listResult
                     join oper in db.BASE_PART on p.cinvcode equals oper.cpartnumber into temp                      
                     from tt in temp.DefaultIfEmpty()
                     
                     select new
                     {
                        p.cwarename
                        ,p.cpositioncode
                        ,p.cposition
                        ,p.cinvcode
                        ,p.qty
                        ,p.worktype
                        ,p.cticketcode
                        ,p.iquantity
                        ,p.dcreatetime
                        ,p.createuser
                        ,p.cerpcode
                        ,p.typename
                        ,p.typecode
                        ,p.ID
                        ,p.PalletCode
                        ,p.OldCinvCode
                        ,p.CurrentStockQTY
                        ,p.WorkTypeName
                        , cspecifications = tt == null ? "" : tt.cspecifications
                     };

        //规格
        if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
        {
            source1 = source1.Where(x => x.cspecifications.Contains(txtcspec.Text.Trim()));
        }

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("worktype", "Operation_Type"));//操作类型
        //flagList.Add(new Tuple<string, string>("typecode", "INTYPE"));//出入库类型
        //flagList.Add(new Tuple<string, string>("typecode", "OUTTYPE"));//出入库类型
        var source = GetGridSourceDataByList(source1.ToList(), flagList);

        //NOTE by Mark, 09/20
        source = GetGridSourceData_PART_RANK(source);


        grdSTOCK_DURATION.DataSource = source;
        //grdSTOCK_DURATION.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdSTOCK_DURATION.DataBind();
    }

    public bool CheckData()
    {
        return true;
    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //this.grdSTOCK_DURATION.DataKeyNames = new string[]{"ID"};

        //本页面打开新增窗口
        this.btnINCHANG.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmSTOCK_DURATIONEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmSTOCK_TradeQueryList_NewQiJianStock + "','STOCK_DURATION');return false;";//新建期间库存
        //Operation_Type
        Help.DropDownListDataBind(new SysParameterList().GetParametersByFlagType("Operation_Type"), this.ddlWorkType, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");    //操作类型 
 
        this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }

    #endregion
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //CurrendIndex = 1;
        //AspNetPager1.CurrentPageIndex = 1;
        //BUCKINGHA-894
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
        }        
        IsFirstPage = true;//恢复默认值
        //BUCKINGHA-894
        if (txtBeginDate.Text.Trim().Length == 0 || txtEndDate.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmSTOCK_TradeQueryList_NeedDateQuJian);//日期区间不能为空！
            return;
        }
        else if (Convert.ToDateTime(txtBeginDate.Text) < Convert.ToDateTime(txtEndDate.Text).AddMonths(-1))
        {
            this.Alert(Resources.Lang.FrmSTOCK_TradeQueryList_DateQuJianLarge);//日期区间不能大于一个月！
            return;
        }
        #region 调用存储过程
        GET_INANDOUTBILLINFO proc = new GET_INANDOUTBILLINFO();
        proc.P_BeginDate = Convert.ToDateTime(txtBeginDate.Text.Trim());
        proc.P_EndDate = Convert.ToDateTime(txtEndDate.Text.Trim()).AddDays(1);
        proc.P_UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
        proc.Execute();
        #endregion
        try
        {
            //重新设置GridNavigator的RowCount
            CurrendIndex = 1;
            this.GridBind();
        }
        catch (Exception error)
        {
            this.Alert(error.Message);
        }
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < this.grdSTOCK_DURATION.Rows.Count; i++)
            {
                if (this.grdSTOCK_DURATION.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_DURATION.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        IGenericRepository<STOCK_DURATION> con = new GenericRepository<STOCK_DURATION>(context);
                        //主键赋值
                        con.Delete(this.grdSTOCK_DURATION.DataKeys[i].Values[0].ToString());	//执行动作 
                        con.Save();

                    }
                }
            }
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteSuccess);//删除成功！
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + E.Message.ToJsString());//删除失败！
        }
    }

    /// 绑定出入库类型
    /// <summary>
    /// 绑定出入库类型
    /// </summary>
    public void BindingType()
    {
        DataTable tb = GetType();
        tb.TableName = "tbType";
        DataRow row = tb.NewRow();
        row[0] = "";
        row[1] = Resources.Lang.WMS_Common_DrpAll;//全部
        tb.Rows.InsertAt(row, 0);
        this.ddlOutorIn.DataSource = tb;
        this.ddlOutorIn.DataValueField = tb.Columns["CERPCODE"].ColumnName;
        this.ddlOutorIn.DataTextField = tb.Columns["TYPENAME"].ColumnName;
        this.ddlOutorIn.DataBind();
    }
    /// <summary>
    /// 获取出入库类型
    /// </summary>
    /// <returns></returns>
    public DataTable GetType()
    {
        string Sql = @"select *
                           from (
                             select ot.cerpcode, s.FLAG_NAME AS typename from outtype ot INNER JOIN dbo.V_SYS_PARAMETER s WITH(NOLOCK) ON s.FLAG_ID=ot.cerpcode AND s.FLAG_TYPE='OUTTYPE' AND s.LANGUAGEID='"+System.Threading.Thread.CurrentThread.CurrentCulture.Name +"' where ot.cerpcode != '2' union all  select it.cerpcode, s.FLAG_NAME AS typename from intype it INNER JOIN V_SYS_PARAMETER s WITH(NOLOCK) ON s.FLAG_ID=it.cerpcode AND s.FLAG_TYPE='INTYPE' AND s.LANGUAGEID='"+System.Threading.Thread.CurrentThread.CurrentCulture.Name +"' where it.cerpcode != '2')A";
        DataTable tbtype = DBHelp.ExecuteToDataTable(Sql);
        return tbtype;
    }
    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdSTOCK_DURATION.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdSTOCK_DURATION.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdSTOCK_DURATION_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string outOrIn = e.Row.Cells[6].Text;
            if (!string.IsNullOrEmpty(outOrIn))
            {
                var ddl = ddlOutorIn.Items.FindByValue(e.Row.Cells[6].Text.Trim());
                if (ddl != null)
                {
                    e.Row.Cells[6].Text = ddl.Text.Trim();
                }
            }
            //switch (e.Row.Cells[5].Text.Trim())
            //{
            //    case "0":
            //        e.Row.Cells[5].Text = Resources.Lang.WMS_Common_StockType_InStock; //"入库";
            //        break;
            //    case "2":
            //        e.Row.Cells[5].Text = Resources.Lang.WMS_Common_StockType_AllocateIn; //"调拨入";
            //        break;
            //    case "1":
            //        e.Row.Cells[5].Text = Resources.Lang.WMS_Common_StockType_OutStock;// "出库";
            //        break;
            //    case "3":
            //        e.Row.Cells[5].Text = Resources.Lang.WMS_Common_StockType_AllocateOut;// "调拨出";
            //        break;
            //}
        }
    }
    #endregion
}

