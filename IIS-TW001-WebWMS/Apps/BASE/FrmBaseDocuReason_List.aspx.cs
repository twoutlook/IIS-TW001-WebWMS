using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Data.Entity.SqlServer;


public partial class BASE_FrmBaseDocuReason_List : WMSBasePage
{

    DBContext context = new DBContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);

        }
        //this.HasRight();
        btnSearch.DataBind();
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<BASE_DOCUREASON> vcon = new GenericRepository<BASE_DOCUREASON>(context);
        IGenericRepository<BASE_OPERATOR> entity1 = new GenericRepository<BASE_OPERATOR>(context);
        var caseList = from p in vcon.Get()
                       join q in entity1.Get() on p.ccreateownercode equals q.caccountid
                       orderby p.dcreatetime descending
                       where 1 == 1
                       select new
                       {
                           p.actionscope,
                           p.ccreateownercode,
                           p.dcreatetime,
                           p.id,
                           p.lastupdateowner,
                           p.lastupdatetime,
                           p.reasoncode,
                           p.reasoncontent,
                           p.states,
                           q.coperatorname
                       };


        if (!string.IsNullOrEmpty(txtREASONCODE.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.reasoncode) && x.reasoncode.Contains(txtREASONCODE.Text.Trim()));
        if (ddlSTATES.SelectedValue != "")
        {
            caseList = caseList.Where(x => x.states.ToString().Equals(ddlSTATES.SelectedValue));
        }

        if (!string.IsNullOrEmpty(txtCCREATEOWNERCODE.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.coperatorname) && x.coperatorname.Contains(txtCCREATEOWNERCODE.Text.Trim()));
        //caseList = caseList.Where(x => x.status.ToString().Equals("1"));

        if (!string.IsNullOrEmpty(txtFromDate.Text.Trim()))
            caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtFromDate.Text.Trim(), x.dcreatetime) >= 0);
        if (txtEndDate.Text != string.Empty)
            caseList = caseList.Where(x => x != null && SqlFunctions.DateDiff("dd", txtEndDate.Text.Trim(), x.dcreatetime) <= 0);



        if (ddlACTIONSCOPE.SelectedValue != "")
        {
            caseList = caseList.Where(x => x.actionscope.ToString().Equals(ddlACTIONSCOPE.SelectedValue));
        }

        //AspNetPager1.RecordCount = caseList.Count();
        //grdBASE_CARGOSPACE.PageSize = this.PageSize;
        //grdBASE_CARGOSPACE.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        //grdBASE_CARGOSPACE.DataBind();

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("actionscope", "ACTIONSCOPE"));//作用域
        flagList.Add(new Tuple<string, string>("states", "ReasonCodeStatus"));//状 态

        var srcdata = GetGridSourceDataByList(data, flagList);

        grdBASE_CARGOSPACE.DataSource = srcdata;
        grdBASE_CARGOSPACE.DataBind();

    }
    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdBASE_CARGOSPACE.DataKeyNames = new string[] { "ID" };
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBaseDocuReason_Edit.aspx", SYSOperation.New, "") + "','"+Resources.Lang.FrmBaseDocuReason_List_Msg03+"','BASE_DocuReason');return false;";//新建理由码

        Help.DropDownListDataBind(GetParametersByFlagType("ACTIONSCOPE"), ddlACTIONSCOPE, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//作用域
        Help.DropDownListDataBind(GetParametersByFlagType("ReasonCodeStatus"), ddlSTATES, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状 态
        
    }
    #endregion

    #region 事件
    //查询按钮
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
        }
        this.GridBind();
        IsFirstPage = true;//恢复默认值
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        
    }
    //作废按钮
    protected void btnUnable_Click(object sender, EventArgs e)
    {
        string strKeyId = "";
        //DBUtil.BeginTrans();
        IGenericRepository<BASE_DOCUREASON> con = new GenericRepository<BASE_DOCUREASON>(context);
        try
        {
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
                        var v = con.Get().Where(p => p.id == strKeyId.ToString()).ToList();
                        string act = v[0].actionscope;
                        string ccr = v[0].ccreateownercode;
                        DateTime dt =Convert.ToDateTime( v[0].dcreatetime);
                        string id = v[0].id;
                        string re = v[0].reasoncode;
                        string rea = v[0].reasoncontent;
                        string st = v[0].states;
                        BASE_DOCUREASON entity = new BASE_DOCUREASON();
                        entity.id = strKeyId;
                        if (entity.states == "N")
                        {
                            no2++;
                            no3++;
                        }
                        else
                        {
                            no1++;
                            //entity.states = "N";
                            //entity.lastupdatetime = DateTime.Now;
                            //entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;//WebUserInfo.GetCurrentUser().UserNo;

                            //entity.actionscope = act;
                            //entity.ccreateownercode = ccr;
                            //entity.dcreatetime = dt;
                            //entity.id = id;
                            //entity.reasoncode = re;
                            //entity.reasoncontent = rea;
                            //con.Update(entity);


                            string sql = "update BASE_DOCUREASON   set states='N' ,lastupdateowner='" + WmsWebUserInfo.GetCurrentUser().UserNo + "',lastupdatetime= GETDATE() where id='" + entity.id + "'";
                            DBHelp.ExcuteScalarSQL(sql);

                        }

                        //BASE_DOCUREASONEntity entity = new BASE_DOCUREASONEntity();
                        //entity.ID = strKeyId;
                        //entity.SelectByPKeys();
                        //if (entity.STATES == "N")
                        //{
                        //    no2++;
                        //    no3++;
                        //}
                        //else
                        //{
                        //    no1++;
                        //    entity.STATES = "N";
                        //    entity.LASTUPDATETIME = DateTime.Now;
                        //    entity.LASTUPDATEOWNER = WebUserInfo.GetCurrentUser().UserNo;
                        //    BASE_DOCUREASONRule.Update(entity);
                        //}
                    }
                }
            }
            //this.Alert("共有" + no + "条记录作废；\r\n作废成功：" + no1 + "；\r\n作废失败：" + no2 + "；\r\n状态已为作废，勿作废：" + no3 + "；");
            this.AlertAndBack("FrmBaseDocuReason_List.aspx?" + BuildQueryString(SYSOperation.New, strKeyId), Resources.Lang.FrmBaseDocuReason_List_Msg04 + no + Resources.Lang.FrmBaseDocuReason_List_Msg05 + no1 + Resources.Lang.FrmBaseDocuReason_List_Msg06 + no2 + Resources.Lang.FrmBaseDocuReason_List_Msg07 + no3 + "；");// this.AlertAndBack("FrmBaseDocuReason_List.aspx?" + BuildQueryString(SYSOperation.New, strKeyId), "共有" + no + "条记录作废;作废成功：" + no1 + ";作废失败：" + no2 + ";状态已为作废，勿作废：" + no3 + "；");
            //DBUtil.Commit();
            //con.Save();
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert("作废失败！" + E.Message.ToJsString());
        }
    }

    protected void btnQy_Click(object sender, EventArgs e)
    {
        string strKeyId = "";
        IGenericRepository<BASE_DOCUREASON> con = new GenericRepository<BASE_DOCUREASON>(context);
        try
        {
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
                        BASE_DOCUREASON entity = new BASE_DOCUREASON();
                        entity.id = strKeyId;
                        if (entity.states == "Y")
                        {
                            no2++;
                            no3++;
                        }
                        else
                        {
                            no1++;
                            //entity.states = "Y";
                            //entity.lastupdatetime = DateTime.Now;
                            //entity.lastupdateowner = WebUserInfo.GetCurrentUser().UserNo;
                            //con.Update(entity);


                            string sql = "update BASE_DOCUREASON   set states='Y' ,lastupdateowner='" + WmsWebUserInfo.GetCurrentUser().UserNo + "',lastupdatetime= GETDATE() where id='" + entity.id + "'";
                            DBHelp.ExcuteScalarSQL(sql);




                        }

                        //BASE_DOCUREASONEntity entity = new BASE_DOCUREASONEntity();
                        //entity.ID = strKeyId;
                        //entity.SelectByPKeys();
                        //if (entity.STATES == "Y")
                        //{
                        //    no2++;
                        //    no3++;
                        //}
                        //else
                        //{
                        //    no1++;
                        //    entity.STATES = "Y";
                        //    entity.LASTUPDATETIME = DateTime.Now;
                        //    entity.LASTUPDATEOWNER = WebUserInfo.GetCurrentUser().UserNo;
                        //    BASE_DOCUREASONRule.Update(entity);
                        //}
                    }
                }
            }
            this.AlertAndBack("FrmBaseDocuReason_List.aspx?" + BuildQueryString(SYSOperation.New, strKeyId), Resources.Lang.FrmBaseDocuReason_List_Msg04 + no + Resources.Lang.FrmBaseDocuReason_List_Msg08 + no1 + "；\r\n" + Resources.Lang.FrmBaseDocuReason_List_Msg09 + no2 + "；\r\n" + Resources.Lang.FrmBaseDocuReason_List_Msg10 + no3 + "；");
            //this.AlertAndBack("FrmBaseDocuReason_List.aspx?" + BuildQueryString(SYSOperation.New, strKeyId), "共有" + no + "条记录启用；\r\n启用成功：" + no1 + "；\r\n启用失败：" + no2 + "；\r\n状态已为启用，勿启用：" + no3 + "；");
            //this.Alert();
            //con.Save();
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.FrmBaseDocuReason_List_Msg11 + E.Message.ToJsString()); //启用失败！

        }
    }

    //protected void grdBASE_CARGOSPACE_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    if (grdNavigatorBASE_CARGOSPACE.IsDbPager)
    //    {
    //        grdNavigatorBASE_CARGOSPACE.CurrentPageIndex = e.NewPageIndex;
    //    }
    //    else
    //    {
    //        this.grdBASE_CARGOSPACE.PageIndex = e.NewPageIndex;
    //    }
    //}

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
            //this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBaseDocuReason_Edit.aspx", SysOperation.Modify, strKeyID), "理由码详情", "BASE_DocuReason");
            this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBaseDocuReason_Edit.aspx?Flag=1&ids=" + strKeyID, SYSOperation.Modify, strKeyID), Resources.Lang.FrmBaseDocuReason_List_Msg12, "BASE_DocuReason", 800, 600); //理由码详情

        }
    }

    public bool CheckData()
    {
        throw new NotImplementedException();
    }

    //protected DataTable grdNavigatorBASE_CARGOSPACE_GetExportToExcelSource()
    //{
    //    //DocuReasonQuery listQuery = new DocuReasonQuery();
    //    DataTable dtSource = listQuery.GetList(ddlACTIONSCOPE.SelectedValue, txtREASONCODE.Text.Trim(), ddlSTATES.SelectedValue, txtCCREATEOWNERCODE.Text.Trim(), txtFromDate.Text.Trim(), txtEndDate.Text.Trim(), false, this.grdNavigatorBASE_CARGOSPACE.CurrentPageIndex, this.grdBASE_CARGOSPACE.PageSize);
    //    ////将状态修改正确 20130605103658
    //    var parameterValue = new Dictionary<string, string> { { "Base_InOutTypeStatus.FLAG", "FLAG" } };
    //    var dtOutPut = CommonFunction.ModSattus(parameterValue, dtSource);
    //    return dtOutPut;
    //}
    #endregion

}