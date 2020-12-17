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
public partial class FrmALLOCATE_Audit_Mail : WMSBasePage
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
        }
        this.btnCheck.Attributes["onclick"] = this.GetPostBackEventReference(this.btnCheck) + ";this.disabled=true;";

    }

    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<ALLOCATE> entity = new GenericRepository<ALLOCATE>(context);
        var caseList = from p in entity.Get()
                       orderby p.dcreatetime descending
                       where 1 == 1
                       select p;
        if (this.txtCTICKETCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtCTICKETCODE.Text));

        if (!string.IsNullOrEmpty(txtDINDATEFrom.Text.Trim()))
            caseList = caseList.Where(x => x.dindate != null && SqlFunctions.DateDiff("dd", txtDINDATEFrom.Text.Trim(), x.dindate) >= 0);
        if (txtDINDATETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dindate != null && SqlFunctions.DateDiff("dd", txtDINDATETo.Text.Trim(), x.dindate) <= 0); 

        if (this.txtCCREATEOWNERCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccreateownercode) && x.ccreateownercode.Contains(txtCCREATEOWNERCODE.Text));

        if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMEFrom.Text.Trim(), x.dcreatetime) >= 0); 
        if (txtDCREATETIMETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMETo.Text.Trim(), x.dcreatetime) <= 0); 

        if (this.ddlCSTATUS.SelectedValue != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cstatus) && x.cstatus.Contains(ddlCSTATUS.SelectedValue));

        if (this.txtERP.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtERP.Text));

      
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        if (WmsWebUserInfo.GetCurrentUser().UserNo != string.Empty)
        {

        }

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("cstatus", "ALLOCATE"));
        var srcdata = GetGridSourceDataByList(data, flagList);

        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATE_Audit_Mail_Msg01+":<b>" + "</b>";//总页数
        this.grdALLOCATE.DataSource = srcdata; //GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        this.grdALLOCATE.DataBind();
    }
    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdALLOCATE.DataKeyNames = new string[] { "ID", "CSTATUS" };
        //调拨单状态
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "ALLOCATE", false, -1, -1), this.ddlCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
    }
    #endregion

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        strKeyId = this.grdALLOCATE.DataKeys[rowIndex].Values[0].ToString();
        return strKeyId;
    }
    private Dictionary<string, string> dict = new Dictionary<string, string>();

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        this.GridBind();
    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            foreach (GridViewRow item in grdALLOCATE.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    CheckBox cbo = itemFindControl as CheckBox;
                    if (cbo.Checked)
                    {
                        string id = this.grdALLOCATE.DataKeys[item.RowIndex][0].ToString();
                        var strstatus = this.grdALLOCATE.DataKeys[item.RowIndex].Values[1].ToString();
                        if (strstatus == "4") //this.grdALLOCATE.Rows[item.RowIndex].Cells[7].Text == "已确认"  4:已确认
                        {
                            IGenericRepository<ALLOCATE> con = new GenericRepository<ALLOCATE>(context);
                            ALLOCATE entity = new ALLOCATE();
                            entity.id = id;
                            entity.cauditperson = WmsWebUserInfo.GetCurrentUser().UserNo;
                            entity.daudittime = DateTime.Now;
                            entity.cstatus = "1";
                            con.Update(entity);
                            con.Save();
                        }
                        else
                        {
                            //msg = "只有未处理的单据才能审核";
                            throw new Exception(Resources.Lang.FrmALLOCATE_Audit_Mail_Msg02);//只有已确认的单据才能审核
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.FrmALLOCATEList_ReviewSuccess;//审核操作成功!
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message.ToJsString();
        }
        Alert(msg);
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }
    protected void btnCheckCancel_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            foreach (GridViewRow item in grdALLOCATE.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    CheckBox cbo = itemFindControl as CheckBox;
                    if (cbo.Checked)
                    {
                        string id = this.grdALLOCATE.DataKeys[item.RowIndex][0].ToString();
                        var strstatus = this.grdALLOCATE.DataKeys[item.RowIndex].Values[1].ToString();
                        if (strstatus == "1") //this.grdALLOCATE.Rows[item.RowIndex].Cells[7].Text == "已审核"  1:已审核                      
                        {
                            IGenericRepository<ALLOCATE> con = new GenericRepository<ALLOCATE>(context);
                            ALLOCATE entity = new ALLOCATE();
                            entity.id = id;
                            entity.cauditperson = WmsWebUserInfo.GetCurrentUser().UserNo;
                            entity.daudittime = DateTime.Now;
                            entity.cstatus = "0";
                            con.Update(entity);
                            con.Save();
                        }
                        else
                        {
                            //msg = "只有未处理的单据才能审核";
                            throw new Exception(Resources.Lang.FrmALLOCATE_Audit_Mail_Msg02);//只有已确认的单据才能审核
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.FrmALLOCATEList_ReviewSuccess;//审核操作成功!
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message.ToJsString();
        }
        Alert(msg);
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
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

        foreach (GridViewRow item in this.grdALLOCATE.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;
                //Product product = item.DataItem as Product;
                //获取ID
                string id = this.grdALLOCATE.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;  
                //控件选中且集合中不存在添加
                if (cbo.Enabled && cbo.Checked && !SelectIds.ContainsKey(id))
                {
                    //SelectIds.Add(id, this.grdALLOCATE.DataKeys[item.RowIndex][1].ToString());
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(id))
                {
                    SelectIds.Remove(id);
                }
            }
        }
    }


    protected void grdALLOCATE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmALLOCATEEdit_D.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmALLOCATEList_Title1, "ALLOCATE"); //调拨单
           
            //((LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0]).Enabled = e.Row.Cells[e.Row.Cells.Count - 3].Text == "0" ? false : true;
            switch (e.Row.Cells[e.Row.Cells.Count - 2].Text)
            {
                case "0":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = Resources.Lang.Common_Status01;//未处理
                    break;
                case "1":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = Resources.Lang.Common_Status02;//已审核
                    break;
                case "2": e.Row.Cells[e.Row.Cells.Count - 2].Text = Resources.Lang.Common_Status02; //已完成

                    break;
                case "3":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = Resources.Lang.Common_Status03;//"已抛转";
                    break;
                case "4":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = Resources.Lang.Common_Status04;// "已确认";
                    break;
                case "5":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = Resources.Lang.Common_Status05;// "調撥中";
                    break;
                case "6":
                    e.Row.Cells[e.Row.Cells.Count - 2].Text = Resources.Lang.Common_Status06;//"調撥完成";
                    break;

            }

            CheckBox cbo = e.Row.FindControl("chkSelect") as CheckBox;
            string id = this.grdALLOCATE.DataKeys[e.Row.RowIndex][0].ToString();

            //判断是否已在SelectIds集合中
            if (SelectIds.ContainsKey(id))
            {
                //如果是控件处于选中状态
                cbo.Checked = true;
            }
        }
    }
    protected void grdALLOCATE_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
}