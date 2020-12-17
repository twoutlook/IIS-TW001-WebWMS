using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Data.SqlClient;
using PageSize = DreamTek.ASRS.Business.SP;
using System.Data.Entity.SqlServer;

public partial class InterFaceLog : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
            this.txtDATEFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
            grdInterLog.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
            Help.DropDownListDataBind(GetParametersByFlagType("InterfaceLogType"), ddlTYPE, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//接口名称
        }
    }

    public void GridBind()
    {

        //string sql = " select * from BASE_INTERFACELOG where 1=1 ";
        //string sqlCount = " select count(1) from BASE_INTERFACELOG where 1=1 ";

        IGenericRepository<BASE_INTERFACELOG> con = new GenericRepository<BASE_INTERFACELOG>(context);
        var caseList = from p in con.Get()
                       orderby p.CREATEDATE descending
                       where 1 == 1
                       select p;

        if (ddlTYPE.SelectedValue != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.TYPEID) && x.TYPEID.Contains(ddlTYPE.SelectedValue.Trim()));
            //sql = sql + " and TYPEID='" + ddlTYPE.SelectedValue.Trim() + "'  ";
            //sqlCount = sqlCount + " and TYPEID='" + ddlTYPE.SelectedValue.Trim() + "'  ";
        }

        if (txtOUTRESULTS.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ERRORMSG) && x.ERRORMSG.Contains(txtOUTRESULTS.Text.Trim()));
            //sql = sql + " and ERRORMSG like '%" + ddlTYPE.SelectedValue.Trim() + "%'  ";
            //sqlCount = sqlCount + " and ERRORMSG like '%" + ddlTYPE.SelectedValue.Trim() + "%'  ";
        }

        if (txtINPARAMS.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.BO) && x.BO.Contains(txtINPARAMS.Text.Trim()));
            //sql = sql + " and BO like '%" + txtINPARAMS.Text.Trim() + "%'  ";
            //sqlCount = sqlCount + " and BO like '%" + txtINPARAMS.Text.Trim() + "%'  ";
        }

        if (txtDATEFrom.Text != string.Empty)
        {
            DateTime st = Convert.ToDateTime(txtDATEFrom.Text + " 00:00:01");
            caseList = caseList.Where(x => x.CREATEDATE != null && SqlFunctions.DateDiff("dd", st, x.CREATEDATE) >= 0);

            //sql = sql + " and CREATEDATE >= '" + txtDATEFrom.Text + "'  ";
            //sqlCount = sqlCount + " and CREATEDATE >= '" + txtDATEFrom.Text + "'  ";

        }

        if (txtDATETo.Text != string.Empty)
        {
            DateTime st = Convert.ToDateTime(txtDATETo.Text + " 23:59:59");
            caseList = caseList.Where(x => x.CREATEDATE != null && SqlFunctions.DateDiff("dd", st, x.CREATEDATE) <= 0);
            //sql = sql + " and CREATEDATE <= '" + txtDATETo.Text + "'  ";
            //sqlCount = sqlCount + " and CREATEDATE <= '" + txtDATETo.Text + "'  ";
        }

        //sql = sql + " order by CREATEDATE ";

        //PageSize.PageSpliter ps = new PageSize.PageSpliter(sql);
        //ps.OrderBySql = " order by CREATEDATE ";
        //ps.PageSize = this.PageSize;
        //ps.PageIndex = CurrendIndex;
        //var psSql = ps.GetPageSQL();
        //var tb = SqlDBHelp.ExecuteToDataTable(psSql);
        //int tbCount = 0;
        //Object obj = DBHelp.ExcuteScalarSQL(sqlCount);
        //if (obj != null)
        //{
        //    int.TryParse(obj.ToString(), out tbCount);
        //}

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
        }
        AspNetPager1.PageSize = this.PageSize;

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        var srcdata = GetGridSourceDataByList(data, "TYPEID", "InterfaceLogType");

        this.grdInterLog.DataSource = srcdata;
        this.grdInterLog.DataBind();

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }
    protected void grdInterLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
}