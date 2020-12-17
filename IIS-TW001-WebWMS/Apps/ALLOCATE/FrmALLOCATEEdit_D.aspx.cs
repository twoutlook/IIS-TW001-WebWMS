using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;

public partial class FrmALLOCATEEdit_D : WMSBasePage
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() == SYSOperation.Modify)
            {
                ShowData();
            }
            else if (this.Operation() == SYSOperation.Preserved1)
            {
                ShowData();
                this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmALLOCATE_DEdit.aspx", SYSOperation.New, "") + "&parentId=" + this.KeyID + "','','ALLOCATE_D',600,350);");
            }
            else
            {
                txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// HH:mm:ss
                txtDINDATE.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// HH:mm:ss
                txtCCREATEOWNERCODE.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                //this.txtCTICKETCODE.Text = new Fun_CreateNo().CreateNo("ALLOCATE");
            }
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }

        this.btnCheck.Attributes["onclick"] = this.GetPostBackEventReference(this.btnCheck) + ";this.disabled=true;";
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中

    }

    #region IPage 成员

    /// <summary>
    /// 初始化页面。主要做一下动作
    ///1、DropDownList,ListBox数据装载,
    ///2、新增按钮、删除的按钮的Java脚本注册等
    ///一般在PageLoad 事件调用，
    ///并且IsPostBack = false时
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('ALLOCATE');return false;";

        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.Operation() == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.FrmALLOCATEEdit_Msg08;//审批;
        }
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdALLOCATE_D.DataKeyNames = new string[] { "IDS" };

        //调拨单状态
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "ALLOCATE", false, -1, -1), this.ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
    }

    #endregion

    public string Status
    {
        get
        {
            if (ViewState["Status"] != null)
            {
                return ViewState["Status"].ToString();
            }
            return "";
        }
        set { ViewState["Status"] = value; }
    }

    public void ShowData()
    {
        IGenericRepository<ALLOCATE> con = new GenericRepository<ALLOCATE>(context);
        var caseList = from p in con.Get().AsEnumerable()
                       where p.id == Request.QueryString["id"]
                       select p;
        ALLOCATE entity = caseList.ToList().FirstOrDefault<ALLOCATE>();
        this.txtCTICKETCODE.Text = entity.cticketcode;
        this.txtID.Text = entity.id;
        if (entity.dindate != null && entity.dindate.ToString().Length > 0)
        {
            this.txtDINDATE.Text = entity.dindate.ToString("yyyy-MM-dd HH:mm:ss");
        }
        else
        {
            this.txtDINDATE.Text = "";
        }
        if (entity.cerpcode != null && entity.cerpcode.Length > 0)
        {
            this.txtCERPCODE.Text = entity.cerpcode.ToString();
        }
        else
        {
            this.txtCERPCODE.Text = "";
        }

        if (entity.ccreateownercode != null && entity.ccreateownercode.Length > 0)
        {
            this.txtCCREATEOWNERCODE.Text = entity.ccreateownercode.ToString();
        }
        else
        {
            this.txtCCREATEOWNERCODE.Text = "";
        }
        if (entity.dcreatetime != null && entity.dcreatetime.ToString().Length > 0)
        {
            this.txtDCREATETIME.Text = entity.dcreatetime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        else
        {
            this.txtDCREATETIME.Text = "";
        }
        this.ddlCSTATUS.SelectedValue = entity.cstatus;  

        if (entity.cstatus == "1")
        {
            if (entity.cauditperson != null && entity.cauditperson.Length > 0)
            {
                this.txtCAUDITPERSON.Text = entity.cauditperson.ToString();
                this.txtDAUDITTIME.Text = entity.daudittime.ToString();
            }
            else
            {
                this.txtCAUDITPERSON.Text = "";
                this.txtDAUDITTIME.Text = "";
            }
        }


        this.txtCMEMO.Text = entity.cmemo.ToString();  
        Status = entity.cstatus;
        if (entity.cstatus != "0")
        {
            this.btnSave.Enabled = false;
            imgDINDATE.Visible = false;
            this.txtID.Enabled = false;
            this.txtCCREATEOWNERCODE.Enabled = false;
            this.txtDCREATETIME.Enabled = false;
            this.txtCAUDITPERSON.Enabled = false;
            this.txtDAUDITTIME.Enabled = false;
            this.txtCTICKETCODE.Enabled = false;
            this.ddlCSTATUS.Enabled = false;
            this.txtCERPCODE.Enabled = false;
            this.txtCMEMO.Enabled = false;
            this.txtDINDATE.Enabled = false;
        }

    }

    public bool CheckData()
    {
        if (this.txtCCREATEOWNERCODE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg11);//制单人项不允许空！
            this.SetFocus(txtCCREATEOWNERCODE);
            return false;
        }

        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (this.txtCCREATEOWNERCODE.Text.GetLengthByByte() > 100)
            {
               this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg12);//制单人项超过指定的长度100！
                this.SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }

        if (this.txtCAUDITPERSON.Text.Trim().Length > 0)
        {
            if (this.txtCAUDITPERSON.Text.GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg13); //审核人项超过指定的长度100！
                this.SetFocus(txtCAUDITPERSON);
                return false;
            }
        }

        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
            if (this.txtCERPCODE.Text.GetLengthByByte() > 30)
            {
                this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg14);//ERP单号项超过指定的长度30！
                this.SetFocus(txtCERPCODE);
                return false;
            }
        }

        ////
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 200)
            {
                this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg15); //备注项超过指定的长度200！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }
        //
        if (this.txtDINDATE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEEdit_Msg16);//调拨日期项不允许空！
            this.SetFocus(txtDINDATE);
            return false;
        }
        //
        if (this.txtDINDATE.Text.Trim().Length > 0)
        {
            if (this.txtDINDATE.Text.IsDate("yyyy-MM-dd HH:mm:ss") == false)
            {
                this.Alert(Resources.Lang.FrmALLOCATEList_NoEfftxtDINDATEFrom);//调拨日期项不是有效的日期！
                this.SetFocus(txtDINDATE);
                return false;
            }
        }

        return true;
    }




    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdALLOCATE_D.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdALLOCATE_D.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdALLOCATE_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            if (Status == "0")
            {
                linkModify.NavigateUrl = "#";
                this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmALLOCATE_DEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmALLOCATEList_Title1, "ALLOCATE_D", 600, 350); //"调拨单"
            }
            else
            {
                linkModify.Enabled = false;
            }


            //((LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0]).Enabled = e.Row.Cells[e.Row.Cells.Count - 3].Text == "0" ? false : true;

            e.Row.Cells[e.Row.Cells.Count - 2].Text = e.Row.Cells[e.Row.Cells.Count - 2].Text == "0" ? Resources.Lang.Common_Status01 : Resources.Lang.Common_Status02; //"未处理" : "已完成";
        }
    }
    protected void grdALLOCATE_D_DataBinding(object sender, EventArgs e)
    {

    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCSTATUS.SelectedValue == "4")
            {
                IGenericRepository<ALLOCATE> con = new GenericRepository<ALLOCATE>(context);
                ALLOCATE entity = new ALLOCATE();
                entity.id = txtID.Text.ToString();

                entity.cauditperson = WmsWebUserInfo.GetCurrentUser().UserNo;
                entity.daudittime = DateTime.Now;
                entity.cstatus = "1";
                con.Update(entity);
                Alert(Resources.Lang.FrmALLOCATEEdit_D_Msg01); //审核成功！
                this.WriteScript("window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('ALLOCATE');");
            }
            else
            {
                Alert(Resources.Lang.FrmALLOCATEEdit_D_Msg02); //只有已确认的才可以审核！
            }
        }
        catch (Exception err)
        {

            Alert(err.Message);
        }
        finally
        {
            //20130702084429
            btnCheck.Style.Remove("disabled");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.CheckData())
        {
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";
            try
            {
                IGenericRepository<ALLOCATE> con = new GenericRepository<ALLOCATE>(context);
                ALLOCATE entity = new ALLOCATE();
                if (this.Operation() == SYSOperation.Modify || SYSOperation.Preserved1 == this.Operation())
                {   
                    strKeyID = txtID.Text.Trim();
                    entity.id = txtID.Text.Trim();
                    con.Update(entity);
                    con.Save();
                    this.AlertAndBack("FrmALLOCATEEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.Common_SuccessSave); //保存成功
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    strKeyID = Guid.NewGuid().ToString();
                    entity.id = strKeyID;
                    con.Insert(entity);
                    con.Save();
                    this.AlertAndBack("FrmALLOCATEEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID),  Resources.Lang.Common_SuccessSave); //保存成功
                }
                if ((sender as Button).ID == "btnNew")
                {
                    Response.Redirect("FrmALLOCATEEdit.aspx?" + BuildQueryString(SYSOperation.Preserved1, strKeyID));
                }
            }
            catch (Exception ex)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.FrmALLOCATEEdit_Msg17 + ex.Message); //"失败！" 
            }

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }

    public void GridBind()
    {
        IGenericRepository<ALLOCATE_D> entity = new GenericRepository<ALLOCATE_D>(context);
        var caseList = from p in entity.Get()
                       orderby p.id descending
                       where 1 == 1
                       select p;

        if (txtID.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.ToUpper().Equals(txtID.Text.ToUpper()));

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("cstatus", "ALLOCATE"));
        var srcdata = GetGridSourceDataByList(data, flagList);

        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";
        this.grdALLOCATE_D.DataSource = srcdata;//GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        this.grdALLOCATE_D.DataBind();
    }
}