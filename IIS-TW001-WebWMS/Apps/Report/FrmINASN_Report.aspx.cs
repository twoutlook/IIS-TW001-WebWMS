using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.Entity.SqlServer;
using System.Collections;
using DevExpress.XtraRichEdit.Import.OpenDocument;
/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/
public partial class Apps_FrmINASN_Report : WMSBasePage
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInType();
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
    }

    private void LoadInType()
    {

        Help.DropDownListDataBind(GetInType(true), this.ddlITYPE, Resources.Lang.Common_ALL, "FUNCNAME", "EXTEND1", "");
        Help.DropDownListDataBind(GetParametersByFlagType("WareHouseType"), this.ddlworktype, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(GetParametersByFlagType("TrueOrFalse"), drpOperationType, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//是否补单
    }
    public DataTable ToExcel()
    {
        List<V_INASN_REPORT> list = new List<V_INASN_REPORT>();

        IGenericRepository<V_INASN_REPORT> con = new GenericRepository<V_INASN_REPORT>(context);
        var caseList = from p in con.Get()
                       orderby p.cticketcode descending
                       where 1 == 1
                       select p;
        if (txtcticketcode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtcticketcode.Text.Trim()));
        if (txtErpCode.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtErpCode.Text.Trim()));
        }
        if (ddlITYPE.SelectedValue != "")
            caseList = caseList.Where(x => x.itype.ToString().Equals(ddlITYPE.SelectedValue));

        if (txtPart.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtPart.Text.Trim().ToUpper()));
        }
        if (txtDCREATETIMEFrom.Text != string.Empty)
        {
            DateTime st = Convert.ToDateTime(txtDCREATETIMEFrom.Text + " 00:00:01");
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMEFrom.Text.Trim(), x.dcreatetime) >= 0);
        }

        if (txtDCREATETIMETo.Text != string.Empty)
        {
            DateTime st = Convert.ToDateTime(txtDCREATETIMETo.Text + " 23:59:59");
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMETo.Text.Trim(), x.dcreatetime) <= 0);
        }
        if (ddlworktype.SelectedValue != "")
            caseList = caseList.Where(x => x.worktype.ToString().Equals(ddlworktype.SelectedValue));
        if (drpOperationType.SelectedValue != "")
        {
            caseList = caseList.Where(x => x.iswebcreatebill.ToString().Contains(drpOperationType.SelectedValue));
        }
        if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
        {
            caseList = caseList.Where(x => x.cspecifications.ToString().Contains(txtcspec.Text.Trim()));
        }
        list = caseList.ToList();
        btnExcel.ExcelName = Resources.Lang.FrmUnionPalletList_MSG2;//"入库通知单";

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("itype", "INTYPE"));//入库类型
        flagList.Add(new Tuple<string, string>("worktype", "WareHouseType"));//作业方式
        flagList.Add(new Tuple<string, string>("iswebcreatebill", "TrueOrFalse"));//是否补单

        var srcdata = GetGridSourceDataByList(list, flagList);

        return srcdata;
    }
	
    public void GridBind()
    {
        IGenericRepository<V_INASN_REPORT> con = new GenericRepository<V_INASN_REPORT>(context);
        var caseList = from p in con.Get()
                       orderby p.cticketcode descending
                       where 1 == 1
                       select p;
        if (txtcticketcode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtcticketcode.Text.Trim()));
        if (txtErpCode.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtErpCode.Text.Trim()));
        }
        if (ddlITYPE.SelectedValue != "")
            caseList = caseList.Where(x => x.itype.ToString().Equals(ddlITYPE.SelectedValue));

        if (txtPart.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtPart.Text.Trim().ToUpper()));
        }
        if (txtDCREATETIMEFrom.Text != string.Empty)
        {
            DateTime st = Convert.ToDateTime(txtDCREATETIMEFrom.Text + " 00:00:01");
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMEFrom.Text.Trim(), x.dcreatetime) >= 0); 
        }

        if (txtDCREATETIMETo.Text != string.Empty)
        {
            DateTime st = Convert.ToDateTime(txtDCREATETIMETo.Text + " 23:59:59");
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMETo.Text.Trim(), x.dcreatetime) <= 0);
        }
        if (ddlworktype.SelectedValue != "")
            caseList = caseList.Where(x => x.worktype.ToString().Equals(ddlworktype.SelectedValue));
        if (drpOperationType.SelectedValue != "")
        {
            caseList = caseList.Where(x => x.iswebcreatebill.ToString().Contains(drpOperationType.SelectedValue));
        }
        if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
        {
            caseList = caseList.Where(x => x.cspecifications.ToString().Contains(txtcspec.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(txtRANK_FINAL.Text.Trim()))
        {
            caseList = caseList.Where(x => x.cinvcode.Contains("-") && x.cinvcode.EndsWith("-" + txtRANK_FINAL.Text.Trim()));
        }
        else
        {
            caseList = caseList.Where(x => x.cinvcode.Contains("-") && x.cinvcode.Substring(x.cinvcode.Length - 2, 1).Equals("-"));
        }
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
        int Count = caseList.Count();
        AspNetPager1.PageSize = this.PageSize;

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("worktype", "WareHouseType"));//作业方式
        flagList.Add(new Tuple<string, string>("TypeCode", "INTYPE"));//类型

        var srcdata = GetGridSourceDataByList(data, flagList);
      

        //#region  显示批/序號(RANK) 代码 2020-09-16 李舟蕾
        //DataTable dt = srcdata;


        //dt.Columns.Add("RANK_FINAL", Type.GetType("System.String")); //批/序號(RANK)

        //DataTable newdt = dt.Clone();

        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    string Cinvcode = Convert.ToString(dt.Rows[i]["cinvcode"]);
        //    string[] array = Cinvcode.Split(new char[] { '-' });

        //    if (Cinvcode.Length > 5 && array.Count() > 1)
        //    {
        //        int s1 = Cinvcode.LastIndexOf("-");
        //        for (int k = 0; k < Cinvcode.Length - s1; k++)
        //        {
        //            dt.Rows[i]["cinvcode"] = Cinvcode.Remove(Cinvcode.Length - k - 1);
        //        }
        //        dt.Rows[i]["RANK_FINAL"] = array[array.Count() - 1].ToString().ToUpper();
        //    }
        //    newdt.ImportRow(dt.Rows[i]);
        //}
        //#endregion

        //this.grdINASN.DataSource = newdt;

        //NOTE by Mark 09/22 07:22
        srcdata = GetGridSourceData_PART_RANK(srcdata);

        this.grdINASN.DataSource = srcdata;
        this.grdINASN.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        this.GridBind();
    }
    protected void grdINASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }

    }
}