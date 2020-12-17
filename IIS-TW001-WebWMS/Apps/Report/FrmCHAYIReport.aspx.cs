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
/// <summary>
/// 描述: 入库管理-->FrmINBILLList 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:57:39
/// </summary>
public partial class FrmCHAYIReport : WMSBasePage// PageBase, IPageGrid
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        btnExcel.GetExportToExcelSource += new DreamTek.ASRS.Business.ExcelButton.GetExportToExcelSourceHandler(ToExcel);
    }


    #region IPageGrid 成员
    public DataTable ToExcel()
    {
        List<v_CHAYIReport> list = new List<v_CHAYIReport>();

        IGenericRepository<v_CHAYIReport> con = new GenericRepository<v_CHAYIReport>(context);
        var caseList = from p in con.Get()
                       orderby p.dcheckdate descending
                       where 1 == 1
                       select p;
        if (TextBox2.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(TextBox2.Text.Trim()));
        if (txtDINDATEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.dcheckdate != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.dcheckdate) >= 0);
        if (txtDINDATETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dcheckdate != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.dcheckdate) <= 0);
        if (TextBox5.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccheckname) && x.ccheckname.Contains(TextBox5.Text.Trim()));
        }
        if (TextBox3.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(TextBox3.Text.Trim()));
        }
        if (TextBox4.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(TextBox4.Text.Trim()));
        }
        if (TextBox1.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwareid) && x.cwareid.Contains(TextBox1.Text.Trim()));
        }
        if (dplCHECKTYPE.SelectedValue != "")
            caseList = caseList.Where(x => x.CHECKCode.ToString().Equals(dplCHECKTYPE.SelectedValue));
        if (ddlworktype.SelectedValue != "")
            caseList = caseList.Where(x => x.worktype.ToString().Equals(ddlworktype.SelectedValue));
        if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
        {
            caseList = caseList.Where(x => x.cspecifications.ToString().Contains(txtcspec.Text.Trim()));
        }
        list = caseList.ToList();
        btnExcel.ExcelName = Resources.Lang.FrmCHAYIReport_content1;// "盘点差异报表";

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("worktype", "WareHouseType"));//是否保税
        flagList.Add(new Tuple<string, string>("CHECKCode", "CHECKTYPE"));//类型

        var srcdata = GetGridSourceDataByList(list, flagList);

        return srcdata;
    }
    public void GridBind()
    {
        IGenericRepository<v_CHAYIReport> con = new GenericRepository<v_CHAYIReport>(context);
        var caseList = from p in con.Get()
                       orderby p.dcheckdate descending
                       where 1 == 1
                       select p;
        if (TextBox2.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(TextBox2.Text.Trim()));
        if (txtDINDATEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.dcheckdate != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.dcheckdate) >= 0);
        if (txtDINDATETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dcheckdate != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.dcheckdate) <= 0);
        if (TextBox5.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccheckname) && x.ccheckname.Contains(TextBox5.Text.Trim()));
        }
        if (TextBox3.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(TextBox3.Text.Trim()));
        }
        if (TextBox4.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(TextBox4.Text.Trim()));
        }
        if (TextBox1.Text != string.Empty)
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwareid) && x.cwareid.Contains(TextBox1.Text.Trim()));
        }
        if (dplCHECKTYPE.SelectedValue != "")
            caseList = caseList.Where(x => x.CHECKCode.ToString().Equals(dplCHECKTYPE.SelectedValue));
        if (ddlworktype.SelectedValue != "")
            caseList = caseList.Where(x => x.worktype.ToString().Equals(ddlworktype.SelectedValue));
        if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
        {
            caseList = caseList.Where(x => x.cspecifications.ToString().Contains(txtcspec.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(txtRANK_FINAL.Text.Trim()))
        {
            caseList = caseList.Where(x =>x.cinvcode.Contains("-") && x.cinvcode.EndsWith("-" + txtRANK_FINAL.Text.Trim()));
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

        #region  显示批/序號(RANK) 代码 2020-09-16 李舟蕾
        DataTable dt = srcdata;
        dt.Columns.Add("RANK_FINAL", Type.GetType("System.String")); //批/序號(RANK)

        DataTable newdt = dt.Clone();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string Cinvcode = Convert.ToString(dt.Rows[i]["cinvcode"]);
            string[] array = Cinvcode.Split(new char[] { '-' });

            if (array.Count() > 1 && array[array.Count() - 1].ToString().Length == 1)
            {
                int s1 = Cinvcode.LastIndexOf("-");
                for (int k = 0; k < Cinvcode.Length - s1; k++)
                {
                    dt.Rows[i]["cinvcode"] = Cinvcode.Remove(Cinvcode.Length - k - 1);
                }
                if (array[array.Count() - 1].ToString() == "_")
                {
                    dt.Rows[i]["RANK_FINAL"] = "";
                }
                else
                {
                    dt.Rows[i]["RANK_FINAL"] = array[array.Count() - 1].ToString().ToUpper();
                }

                newdt.ImportRow(dt.Rows[i]);
            }
        }
        #endregion

        this.grdINBILL.DataSource = newdt;
        this.grdINBILL.DataBind();
    }


    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";

        Help.DropDownListDataBind(GetParametersByFlagType("WareHouseType"), this.ddlworktype, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        var checkTypes = GetParametersByFlagType("CHECKTYPE");
        //var chou = checkTypes.Where(x => x.FLAG_ID == "2").FirstOrDefault();
        //if (chou != null)
        //{
        //    checkTypes.Remove(chou);
        //}
        Help.DropDownListDataBind(checkTypes, this.dplCHECKTYPE, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        dplCHECKTYPE.SelectedValue = "1";
        dplCHECKTYPE.Enabled = false;
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
        AspNetPager1.CurrentPageIndex = 1;
        this.GridBind();
    }



    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorINBILL
    }



    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdINBILL.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdINBILL.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdINBILL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            //HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count-1].Controls[0];
            //linkModify.NavigateUrl = "#";
            //this.OpenFloatWinMax(linkModify,BuildRequestPageURL("FrmINBILLEdit.aspx",SysOperation.Modify, strKeyID),"入库单","INBILL");
            //e.Row.Cells[2].Text = e.Row.Cells[2].Text == "0" ? "物理" : "循环";
        }

    }

    protected void dsGrdINBILL_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }



}

