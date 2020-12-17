using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Collections;
using System.Reflection;


public partial class UserControls_ShowPARTDiv : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (SetTypeCode.ContainsKey("intype"))
            {
                hfInType.Value = SetTypeCode["intype"].Trim();
            }
            if (SetTypeCode.ContainsKey("IsAll".ToLower()))
            {
                hfIsAll.Value = SetTypeCode["IsAll".ToLower()].Trim();
            }
            if (SetTypeCode.ContainsKey("erpcode"))
            {
                hfErpcode.Value = SetTypeCode["erpcode"].Trim();
            }
            //特殊元件领料
            if (SetTypeCode.ContainsKey("IsSpecialWIP_Issue".ToLower()))
            {
                hfIsSpecialWIP_Issue.Value = SetTypeCode["IsSpecialWIP_Issue".ToLower()].Trim();
            }
            //20130516174204 Roger 工单完工分组维护
            if (SetTypeCode.ContainsKey("flag".ToLower()))
            {
                hfGroupFlag.Value = SetTypeCode["flag".ToLower()].Trim();
            }
            //btnSearch_Click(null, null);
        }
    }

    void Search(int iPage)
    {
        //BASE_FrmBASE_PARTListQuery listQuery = new BASE_FrmBASE_PARTListQuery();

        //System.Data.DataTable pdat = null;

        //if (hfInType.Value.Length > 0)
        //{
        //    // Wip Negative Issue : 38 工单负发料
        //    if (hfInType.Value.Equals("38"))
        //    {
        //        pdat = listQuery.GetWipNegativeIssuePartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, hfIsAll.Value.Trim(), false, false, iPage, this.gvReport.PageSize);
        //    }// Wip Return 43 -103工单退料
        //    else if (hfInType.Value.Equals("103"))
        //    {
        //        pdat = listQuery.GetWipReturnPartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, hfIsAll.Value.Trim(), false, false, iPage, this.gvReport.PageSize);
        //    }//出      Wip Issue 35-203 工单发料 ；      GNT維修工單領料 : 460 。
        //    else if (hfInType.Value.Equals("203") || hfInType.Value.Equals("460"))
        //    {
        //        pdat = listQuery.GetWipIssuePartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, hfIsSpecialWIP_Issue.Value.Trim(), false, false, iPage, this.gvReport.PageSize);
        //    }//Return to Vendor : 36 -201. 只能退相同ERPcode下的料和数量
        //    else if (hfInType.Value.Equals("201"))
        //    {
        //        pdat = listQuery.GetReturnToVendorPartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, false, false, iPage, this.gvReport.PageSize);
        //    }//WIP Negative Return 48 工单负退料； 
        //    else if (hfInType.Value.Equals("48"))
        //    {
        //        pdat = listQuery.GetWipNegativeReturnPartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, hfIsSpecialWIP_Issue.Value.Trim(), false, false, iPage, this.gvReport.PageSize);
        //    }//  工单超领退只能退工单超领相同工单号下的料
        //    else if (hfInType.Value.Equals("105"))//(new InTypeQuery().ValidateInTypeIsWipIssue(hfInType.Value.Trim()))
        //    {
        //        //工单超领退只能退工单超领相同工单号下的料
        //        pdat = listQuery.GetWipIssueInTypePartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, false, false, iPage, this.gvReport.PageSize);
        //    }//     工单超领只能领wip issue 相同工单下的料
        //    else if (hfInType.Value.Equals("205"))//(new OUT_FrmOUTTYPEListQuery().ValidateOutTypeIsWipIssue(hfInType.Value.Trim()))
        //    {
        //        //工单超领只能领wip issue 相同工单下的料
        //        pdat = listQuery.GetWipIssueOutTypePartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, false, false, iPage, this.gvReport.PageSize);
        //    }
        //}

        ////20130516174204 Roger 工单完工分组维护
        //listQuery.flag = "0";
        //if(hfGroupFlag.Value.Length>0)
        //{
        //    listQuery.flag = "1";
        //    pdat = listQuery.GetList("", "", this.txtPartNumber.Text, this.txtName.Text, "", "", "", "", "0", "", "", "", "", "", "", false, iPage, this.gvReport.PageSize);
        //}

        //if (pdat == null)
        //    pdat = listQuery.GetList("", "", this.txtPartNumber.Text, this.txtName.Text, "", "", "", "", "", "", "", "", "", "", "", false, iPage, this.gvReport.PageSize);

        //gvReport.PageIndex = iPage;
        //gvReport.DataSource = pdat;
        //gvReport.DataBind();

        CurrendIndex = 1;
        Bind("");
    }

    public IQueryable<DreamTek.ASRS.DAL.BASE_PART> GetQueryList()
    {
        IGenericRepository<DreamTek.ASRS.DAL.BASE_PART> pigeonBill = new GenericRepository<DreamTek.ASRS.DAL.BASE_PART>(db);
        var caseList = from p in pigeonBill.Get()
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtPartNumber.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpartnumber) && x.cpartnumber.Contains(txtPartNumber.Text.Trim()));
            }
            if (txtRank_Final.Text!=string.Empty)
            {
                caseList = caseList.Where(x => x.cpartnumber.Contains("-")&&x.cpartnumber.EndsWith("-" + txtRank_Final.Text.Trim()));
            }
            if (txtName.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpartname) && x.cpartnumber.Contains(txtName.Text.Trim()));
            }
            if (txtcspec.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cspecifications) && x.cspecifications.Contains(txtcspec.Text.Trim()));
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
        var srcdata = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        #region  显示批/序號(RANK) 代码 2020-09-16 李舟蕾
        DataTable dt = ListToDataTable(srcdata);
        dt.Columns.Add("RANK_FINAL", Type.GetType("System.String")); //批/序號(RANK)

        DataTable newdt = dt.Clone();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string Cinvcode = Convert.ToString(dt.Rows[i]["cpartnumber"]);
            string[] array = Cinvcode.Split(new char[] { '-' });

            if (array.Count() > 1 && array[array.Count() - 1].ToString().Length == 1)
            {
                int s1 = Cinvcode.LastIndexOf("-");
                for (int k = 0; k < Cinvcode.Length - s1; k++)
                {
                    dt.Rows[i]["cpartnumber"] = Cinvcode.Remove(Cinvcode.Length - k - 1);
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
        if (newdt != null && newdt.Rows.Count > 0)
        {
            gvReport.DataSource = newdt;
        }
        else { gvReport.DataSource = null; }
        gvReport.DataBind();
    }
    public static System.Data.DataTable ListToDataTable(IList list)
    {
        System.Data.DataTable result = new System.Data.DataTable();
        if (list.Count > 0)
        {
            PropertyInfo[] propertys = list[0].GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                //获取类型
                Type colType = pi.PropertyType;
                //当类型为Nullable<>时
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                result.Columns.Add(pi.Name, colType);
            }
            for (int i = 0; i < list.Count; i++)
            {
                ArrayList tempList = new ArrayList();
                foreach (PropertyInfo pi in propertys)
                {
                    object obj = pi.GetValue(list[i], null);
                    tempList.Add(obj);
                }
                object[] array = tempList.ToArray();
                result.LoadDataRow(array, true);
            }
        }
        return result;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    [Browsable(true), Description("刷新的控件名")]
    public string SetCINVBARCODE { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetCINVNAME { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetCINVCODE { get; set; }

    [Browsable(true), Description("刷新的控件名")]
    public string SetBoxNum { get; set; }

    [Browsable(true), Description("规格")]
    public string SetCspec { get; set; }
    

    Dictionary<string, string> _SetTypeCode = new Dictionary<string, string>();
    /// <summary>
    /// 获取
    /// </summary>
    public Dictionary<string, string> SetTypeCode
    {
        get
        {
            return _SetTypeCode;
        }
        set
        {
            _SetTypeCode = value;
        }
    }
    
    [Browsable(true), Description("Div名称")]
    public string GetDivName { get { return ajaxWebSearChComp.ClientID; } }

    [Browsable(true), Description("是否显示企业名称(企业代码)")]
    public bool GetComp { get; set; }

    void Alert(string Message)
    {
        Page.ClientScript.RegisterClientScriptBlock(GetType(), "f1", "<script>alert('" + Message + "！');</script>");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //BASE_FrmBASE_PARTListQuery listQuery = new BASE_FrmBASE_PARTListQuery();
        //DataTable dtRowCount = null;
        //if (hfInType.Value.Length > 0)
        //{
        //    //
        //    if (hfInType.Value.Equals("38"))
        //    {
        //        //入 WIP Negative issue 工单负发料 
        //        dtRowCount = listQuery.GetWipNegativeIssuePartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, hfIsAll.Value.Trim(), false, true, -1, -1);
        //    }
        //    else if (hfInType.Value.Equals("103"))
        //    {
        //        //入 WIP Return 43-103工单退料
        //        dtRowCount = listQuery.GetWipReturnPartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, hfIsAll.Value.Trim(), false, true, -1, -1);
        //    }//出      Wip Issue 35-203 工单发料 ；    WIP Negative Return 48 工单负退料；    GNT維修工單領料 : 460 。
        //    else if (hfInType.Value.Equals("203") || hfInType.Value.Equals("48") || hfInType.Value.Equals("460"))
        //    {
        //        //出 WIP Issue 工单发料
        //        dtRowCount = listQuery.GetWipIssuePartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, hfIsSpecialWIP_Issue.Value, false, true, -1, -1);
        //    }//Return to Vendor : 36-201 . 只能退相同ERPcode下的料和数量
        //    else if (hfInType.Value.Equals("201"))
        //    {
        //        dtRowCount = listQuery.GetReturnToVendorPartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, false, true, -1, -1);
        //    }//  工单超领退只能退工单超领相同工单号下的料
        //    else if (hfInType.Value.Equals("105"))//else if (new InTypeQuery().ValidateInTypeIsWipIssue(hfInType.Value.Trim()))
        //    {
        //        //工单超领退只能退工单超领相同工单号下的料
        //        dtRowCount = listQuery.GetWipIssueInTypePartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, false, true, -1, -1);
        //    }//    工单超领只能领wip issue 相同工单下的料
        //    else if (hfInType.Value.Equals("205"))//else if (new OUT_FrmOUTTYPEListQuery().ValidateOutTypeIsWipIssue(hfInType.Value.Trim()))
        //    {
        //        //工单超领只能领wip issue 相同工单下的料
        //        dtRowCount = listQuery.GetWipIssueOutTypePartList(this.txtPartNumber.Text, this.txtName.Text, hfErpcode.Value, false, true, -1, -1);
        //    }
        //}
        //if (dtRowCount == null)
        //    dtRowCount = listQuery.GetList("", "", this.txtPartNumber.Text, this.txtName.Text, "", "", "", "", "", "", "", "", "", "", "", true, -1, -1);
        ////DataTable dtRowCount = list.GetMASTCOMPANYLIST(this.txtName.Text, true, 0, 0);

        //this.grdNavigator.CurrentPageIndex = 0;

        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdNavigator.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdNavigator.RowCount = 0;
        //}

        Search(0);

        //// System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        //// System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        //// this.grdNavigator.RenderControl(oHtmlTextWriter);
        ////this.DataGridNavigator.InnerHtml =   oStringWriter.ToString();
        //if (this.grdNavigator.Controls.Count >= 6)
        //{
        //    this.grdNavigator.Controls[6].Visible = false;
        //    this.grdNavigator.Controls[5].Visible = false;
        //}
    }

    protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //try
        //{
        //    this.grdNavigator.CurrentPageIndex = e.NewPageIndex;
        //    Search(e.NewPageIndex);
        //}
        //catch
        //{
        //    this.grdNavigator.CurrentPageIndex = 0;
        //    Search(0);
        //}

    }

    protected void gvReport_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow viewrow = gvReport.Rows[e.NewSelectedIndex];
        e.Cancel = true;
        GetComp = true;
        if (GetComp)
        {
            if (SetBoxNum != null)
            {
                //28-10-2020 by Qamar
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                           "SetControlValue3('" + SetCINVCODE + "','" + Server.HtmlDecode(viewrow.Cells[1].Text) + "','" 
                           + SetCINVNAME + "','" + Server.HtmlDecode(viewrow.Cells[3].Text).ToJsString().Replace("''", "’’")
                           + "','" + SetBoxNum + "','" + Server.HtmlDecode(viewrow.Cells[4].Text) + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
            }
            else
            {
                string strCspec = "";
                if (gvReport.DataKeys[e.NewSelectedIndex][1]!=null && !string.IsNullOrEmpty(gvReport.DataKeys[e.NewSelectedIndex][1].ToString()))
                {
                    strCspec = Server.HtmlDecode(gvReport.DataKeys[e.NewSelectedIndex][1].ToString());
                }
                //28-10-2020 by Qamar
                //string t1 = viewrow.Cells[1].Text; //無RANK的料號
                //string t2 = viewrow.Cells[2].Text; //一碼RANK_FINAL
                //string t3 = viewrow.Cells[3].Text; //品名
                //string t4 = viewrow.Cells[4].Text; //boxNum
                //string t5 = viewrow.Cells[5].Text; //規格
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                //           "SetControlValue('" + SetCINVCODE + "','" + Server.HtmlDecode(viewrow.Cells[1].Text) + "','" + SetCINVNAME + "','" + Server.HtmlDecode(viewrow.Cells[3].Text).ToJsString().Replace("''", "’’") + "','" + SetCspec + "','" + strCspec + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
                string SetRANKFINAL = "ctl00_ContentPlaceHolderMain_txtRANK_FINAL";
                if (viewrow.Cells[2].Text == "_")
                    viewrow.Cells[2].Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                           "SetControlValue('" + SetCINVCODE + "','" + Server.HtmlDecode(viewrow.Cells[1].Text) + "','" + SetCINVNAME + "','" + Server.HtmlDecode(viewrow.Cells[3].Text).ToJsString().Replace("''", "’’") + "','" + SetCspec + "','" + strCspec + "','" + SetRANKFINAL + "','" + Server.HtmlDecode(viewrow.Cells[2].Text) + "');"
                           + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "aaa",
                        "SelectPart('" + SetCINVCODE + "','" + Server.HtmlDecode(viewrow.Cells[1].Text) + "');" + ajaxWebSearChComp.ClientID + ".style.display = 'none'", true);

        }
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //28-10-2020 by Qamar
            this.gvReport.Columns[4].Visible = false; //boxNum
        }
    }
}
