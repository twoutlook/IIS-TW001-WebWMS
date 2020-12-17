using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using DreamTek.ASRS.Business.Base;
using System.Data.Entity.SqlServer;

public partial class Apps_BASE_FrmBase_InOutTypeStatusList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);        
        }      
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        //BASE_FrmBase_InOutTypeStatusList listQuery = new BASE_FrmBase_InOutTypeStatusList();
        //DataTable dtSource = listQuery.GetList(txtCERPCODE.Text.Trim(), txtTYPENAME.Text.Trim(), drpInOut.SelectedValue, ddlCSTATUS.SelectedValue, txtUser.Text.Trim(), txtFromDate.Text.Trim(), txtEndDate.Text.Trim(), false, this.grdNavigatorBASE_CARGOSPACE.CurrentPageIndex, this.grdBASE_CARGOSPACE.PageSize);
        //this.grdBASE_CARGOSPACE.DataSource = dtSource;
        //this.grdBASE_CARGOSPACE.DataBind();
    }

    public IQueryable<V_INOutType> GetQueryList()
    {
        IGenericRepository<V_INOutType> conn = new GenericRepository<V_INOutType>(db);
        var caseList = from p in conn.Get()
                       orderby p.CREATEDATE ascending
                       where 1==1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCERPCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CERPCODE) && x.CERPCODE.Contains(txtCERPCODE.Text.Trim()));
            }
            if (txtTYPENAME.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.TYPENAME) && x.TYPENAME.Contains(txtTYPENAME.Text.Trim()));
            }
            if (drpInOut.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.T) && x.T.Equals(drpInOut.SelectedValue));
            }
            if (ddlCSTATUS.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ENABLE) && x.ENABLE.Equals(ddlCSTATUS.SelectedValue));
            }
            if (txtUser.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CREATEUSER) && x.CREATEUSER.Contains(txtUser.Text.Trim()));
            }
            DateTime d;
            if (txtFromDate.Text != string.Empty && DateTime.TryParse(txtFromDate.Text,out d))
            {
                caseList = caseList.Where(x => x.CREATEDATE.HasValue && SqlFunctions.DateDiff("dd", d,x.CREATEDATE) >= 0);
            }
            DateTime d2;
            if (txtEndDate.Text != string.Empty && DateTime.TryParse(txtEndDate.Text, out d2))
            {
                caseList = caseList.Where(x => x.CREATEDATE.HasValue && SqlFunctions.DateDiff("dd", d2, x.CREATEDATE) <= 0 );
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
        }

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("T", "InOutType"));//类型分类
        flagList.Add(new Tuple<string, string>("IsMatchSo", "TrueOrFalse"));//是否匹配单据
        flagList.Add(new Tuple<string, string>("IsMatchVendor", "TrueOrFalse"));//是否匹配供应商
        flagList.Add(new Tuple<string, string>("ENABLE", "UsedOrCanceled"));//类别

        var srcdata = GetGridSourceDataByList(data, flagList);

        grdBASE_CARGOSPACE.DataSource = srcdata;
        grdBASE_CARGOSPACE.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }



    #endregion
    #region IPage 成员

    public void InitPage()
    {
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmBASE_InOutTypeStatusEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmBase_InOutTypeStatusList_Msg03+ "','BASE_InOutTypeStatus');return false;"; //新建入出库类型详情

        Help.DropDownListDataBind(GetParametersByFlagType("InOutType"), drpInOut, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//类型分类
        Help.DropDownListDataBind(GetParametersByFlagType("UsedOrCanceled"), ddlCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
    }		 
	#endregion

    //查询按钮
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
        }
        Bind("");
        IsFirstPage = true;//恢复默认值
    }

    //作废按钮
    protected void btnUnable_Click(object sender, EventArgs e)
    {
        string strKeyId="";
        //DBUtil.BeginTrans();
        try
        {
            BaseCommQuery bc = new BaseCommQuery();

            int no = 0;//作废数量
            int no1 = 0;//作废成功数量
            int no2 = 0;//作废失败数量
            int no3 = 0;//状态为作废的数量
            for (int i = 0; i < this.grdBASE_CARGOSPACE.Rows.Count; i++)
            {
                if (this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {

                        no++;
                        
                        //主键赋值
                        strKeyId = this.grdBASE_CARGOSPACE.DataKeys[i].Values[0].ToString();                        
                        if (grdBASE_CARGOSPACE.Rows[i].Cells[2].Text == "入库")
                        {
                            //判断入库通知单和入库单该状态下是否都完成
                            //bool inbool = listQuery.GetIN(grdBASE_CARGOSPACE.Rows[i].Cells[3].Text);
                            int count = bc.GetINTypeCount(grdBASE_CARGOSPACE.Rows[i].Cells[3].Text);
                            //if (inbool == false)
                            if (count>0)
                            {
                                this.Alert(grdBASE_CARGOSPACE.Rows[i].Cells[4].Text+"已生成入库通知单不可以作废");
                                no2++;
                                return;
                            }
                        }
                        else if (grdBASE_CARGOSPACE.Rows[i].Cells[2].Text == "出库")
                        {
                            //判断出库通知单和出库单该状态下是否都完成
                            //bool inbool = listQuery.GetOUT(grdBASE_CARGOSPACE.Rows[i].Cells[3].Text);
                            int count = bc.GetOutTypeCount(grdBASE_CARGOSPACE.Rows[i].Cells[3].Text);
                            //if (inbool == false)
                            if (count>0)
                            {
                                this.Alert(grdBASE_CARGOSPACE.Rows[i].Cells[4].Text + "已生成出库通知单不可以作废！");
                                no2++;
                                return;
                            }
                        }

                    }
                }
            }
            IGenericRepository<INTYPE> in_conn = new GenericRepository<INTYPE>(db);
            IGenericRepository<OUTTYPE> out_conn = new GenericRepository<OUTTYPE>(db);
            for (int i = 0; i < this.grdBASE_CARGOSPACE.Rows.Count; i++)
            {
                if (this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                         //主键赋值
                        strKeyId = this.grdBASE_CARGOSPACE.DataKeys[i].Values[0].ToString(); 
                        if (grdBASE_CARGOSPACE.Rows[i].Cells[2].Text == "入库")
                        {
                            INTYPE inBO = (from p in in_conn.Get()
                                           where p.typeid == strKeyId
                                           select p).FirstOrDefault();
                            if (inBO != null && inBO.typeid != null && inBO.typeid.Length > 0)
                            {
                                if (inBO.enable == "1")
                                {
                                    this.Alert(grdBASE_CARGOSPACE.Rows[i].Cells[4].Text + "已是作废状态！");
                                    Bind("");
                                    this.GridBind();
                                    return;
                                }
                                inBO.enable = "1";
                                inBO.enabledate = DateTime.Now;
                                inBO.enableuser = WmsWebUserInfo.GetCurrentUser().UserNo;
                                in_conn.Update(inBO);
                                in_conn.Save();

                            }
                        }
                        else if (grdBASE_CARGOSPACE.Rows[i].Cells[2].Text == "出库")
                        {
                            OUTTYPE outBO = (from p in out_conn.Get()
                                           where p.typeid == strKeyId
                                           select p).FirstOrDefault();
                            if (outBO != null && outBO.typeid != null && outBO.typeid.Length > 0)
                            {
                                if (outBO.enable == "1")
                                {
                                    this.Alert(grdBASE_CARGOSPACE.Rows[i].Cells[4].Text + "已是作废状态！");
                                    Bind("");
                                    this.GridBind();
                                    return;
                                }
                                outBO.enable = "1";
                                outBO.enabledate = DateTime.Now;
                                outBO.enableuser = WmsWebUserInfo.GetCurrentUser().UserNo;
                                out_conn.Update(outBO);
                                out_conn.Save();
                            }
                        }
                    }
                }
            }
            this.Alert("作废成功！");
            //this.Alert("共有" + no + "条记录作废；\r\n作废成功：" + no1 + "；\r\n作废失败：" + no2 + "；\r\n状态已为作废，勿作废：" + no3 + "；");
            //DBUtil.Commit();
            Bind("");
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert("作废失败！" + E.Message.ToJsString());
            //DBUtil.Rollback();
        }
    }

    protected void grdBASE_CARGOSPACE_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorBASE_CARGOSPACE.IsDbPager)
        //{
        //    grdNavigatorBASE_CARGOSPACE.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdBASE_CARGOSPACE.PageIndex = e.NewPageIndex;
        //}
    }

    protected void grdBASE_CARGOSPACE_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdBASE_CARGOSPACE.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBASE_CARGOSPACE.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdBASE_CARGOSPACE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBASE_InOutTypeStatusEdit.aspx", SYSOperation.Modify, strKeyID), "入出库类型详情", "BASE_InOutType");
        }
    }

    public bool CheckData()
    {
        throw new NotImplementedException();
    }

    protected DataTable grdNavigatorBASE_CARGOSPACE_GetExportToExcelSource()
    {
        //BASE_FrmBase_InOutTypeStatusList listQuery = new BASE_FrmBase_InOutTypeStatusList();
        //DataTable dtSource = listQuery.GetList(txtCERPCODE.Text.Trim(), txtTYPENAME.Text.Trim(), drpInOut.SelectedValue, ddlCSTATUS.SelectedValue, txtUser.Text.Trim(), txtFromDate.Text.Trim(), txtEndDate.Text.Trim(),false, this.grdNavigatorBASE_CARGOSPACE.CurrentPageIndex, this.grdBASE_CARGOSPACE.PageSize);
        ////将状态修改正确 20130605103658
        //var parameterValue = new Dictionary<string, string> { { "Base_InOutTypeStatus.FLAG", "FLAG" } };
        //var dtOutPut = CommonFunction.ModSattus(parameterValue, dtSource);
        //return dtOutPut;
        return null;
    }
}