using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;

/// <summary>
/// 捡货指引
/// </summary>
public partial class OUT_FrmOUTASSITList : WMSBasePage
{

    #region 页面属性

    #endregion

    #region 页面加载
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitPage();
            //this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            //this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    /// <summary>
    /// 页面初始化
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdOUTASSIT.DataKeyNames = new string[] { "ID" };

        //多国语更改,dropDownlist【begin】
        //var paraList = context.SYS_PARAMETER.Where(x => x.flag_type == "ASSIT").OrderBy(x => x.flag_id).ToList();
        //Help.DropDownListDataBind(paraList, this.ddlCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//全部
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "ASSIT", false, -1, -1), this.ddlCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "IS_MergeCode", false, -1, -1), this.ddlIS_MERGE, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(this.GetOutType(true), this.ddlOutType, Resources.Lang.WMS_Common_DrpAll, "FUNCNAME", "EXTEND1", "");//全部
        //多国语更改,dropDownlist【end】
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmOUTASSITEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmOUTASSITList_NewAssit + "','OUTASSIT');return false;";//新建拣货指引
        Help.RadioButtonDataBind(SysParameterList.GetList("", "", "MonthOrWeek", false, -1, -1), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");
    }

    #endregion

    #region 页面事件
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;//默认第一页
            AspNetPager1.CurrentPageIndex = 1;
        }
        IsFirstPage = true;
        this.GridBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            for (int i = 0; i < this.grdOUTASSIT.Rows.Count; i++)
            {
                if (this.grdOUTASSIT.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdOUTASSIT.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string id = this.grdOUTASSIT.DataKeys[i].Values[0].ToString();
                        var modOutAssit = context.OUTASSIT.Where(x => x.id == id).FirstOrDefault();

                        if (modOutAssit != null && modOutAssit.cstatus =="0")
                        {
                            #region 调用存储过程
                            List<string> SparaList = new List<string>();
                            SparaList.Add("@P_OutAssit_Id:" + id);
                            SparaList.Add("@pUserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                            SparaList.Add("@P_ReturnValue:" + "");
                            SparaList.Add("@INFOTEXT:" + "");
                            string[] Result = DBHelp.ExecuteProc("Proc_DeleteOutAssit", SparaList);
                            if (Result.Length == 1)//调用存储过程有错误
                            {
                                msg += Result[0];
                                break;
                            }
                            else if (Result[0] == "0")
                            {

                            }
                            else
                            {
                                msg += Result[1];
                                break;
                            }
                            #endregion
                        }
                        else
                        {
                            msg = Resources.Lang.FrmOUTASSITList_Tips_CannotDelete;//只有状态为[未處理]的单据才能删除.
                            break;
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;//删除成功!
            }
            this.GridBind();
        }
        catch
        {
            msg += Resources.Lang.WMS_Common_Msg_DeleteFailed;//删除失败!
        }
        this.Alert(msg);
    }

    /// <summary>
    /// 释放
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //Proc_ReleaseBaseCargoSpace proc = new Proc_ReleaseBaseCargoSpace();
        //proc.P_OutAssit_id = (sender as LinkButton).CommandArgument.Trim();
        //proc.Execute();
        //if (proc.P_ReturnValue == 0)
        //{
        //    Alert("释放成功!");
        //}
        //else
        //{
        //    Alert("释放失败!");
        //}
    }

    /// <summary>
    /// 分页控件事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void grdOUTASSIT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmOUTASSITEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmOUTASSITList_Menu_PageName, "OUTASSIT");//拣货指引
        }
    }
    #endregion

    #region 页面方法
    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdOUTASSIT.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdOUTASSIT.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    public void GridBind()
    {
       
        using (var modContext = context) {
            var queryList = from p in modContext.OUTASSIT
                            orderby p.dcreatetime descending
                            where 1 == 1
                            select p;

            if (!string.IsNullOrEmpty(txtCTICKETCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cticketcode.Contains(txtCTICKETCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(ddlCSTATUS.SelectedValue))
            {
                queryList = queryList.Where(x => x.cstatus.ToString().Equals(ddlCSTATUS.SelectedValue));
            }
            
            if (!string.IsNullOrEmpty(txtCinvCode.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.OUTASSIT_D.Any(p => p.id == x.id && p.cinvcode.Contains(txtCinvCode.Text.Trim())));
            }
            if (!string.IsNullOrEmpty(this.ddlOutType.SelectedValue))
            {
                queryList = queryList.Where(x => modContext.OUTASN.Any(p=>x.coutasnid == p.id && p.itype.ToString().Equals(this.ddlOutType.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(txtErpCode.Text.Trim())) {
                queryList = queryList.Where(x => modContext.OUTASN.Any(p => x.coutasnid == p.id && p.cerpcode.Equals(txtErpCode.Text.Trim()))
                    || modContext.OUTASN.Any(p => x.coutasnid == p.id && p.is_merge == '1' && p.merge_id !=null
                                  && modContext.OUTASN.Any(y => y.merge_id == p.merge_id  && y.is_merge == '0' && y.cerpcode.Equals(txtErpCode.Text.Trim()))
                       )
                );
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMEFrom.Text.Trim()))
            {
                DateTime createTimeFrom = Convert.ToDateTime(txtDCREATETIMEFrom.Text.Trim());
                queryList = queryList.Where(x => x.dcreatetime >= createTimeFrom);
            }
            if (!string.IsNullOrEmpty(txtDCREATETIMETo.Text.Trim()))
            {
                DateTime createTimeTo = Convert.ToDateTime(txtDCREATETIMETo.Text.Trim()).AddDays(1);
                queryList = queryList.Where(x => x.dcreatetime != null && x.dcreatetime < createTimeTo);
            }
            if (!string.IsNullOrEmpty(txtCOUTASNID.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.OUTASN.Any(p => x.coutasnid == p.id && p.cticketcode.Contains(txtCOUTASNID.Text.Trim())));
            }
            if (!string.IsNullOrEmpty(ddlIS_MERGE.SelectedValue))
            {
                queryList = queryList.Where(x => x.is_merge.ToString().Equals(ddlIS_MERGE.SelectedValue));
            }
            if (!string.IsNullOrEmpty(WmsWebUserInfo.GetCurrentUser().UserNo)) {
                string userno = WmsWebUserInfo.GetCurrentUser().UserNo;
                queryList = queryList.Where(x => modContext.OUTASN.Any(p => x.coutasnid == p.id && modContext.UserFunction.Any(y=>y.extend1 == p.itype.ToString() && y.userno ==userno )));
            }

            queryList = queryList.Distinct().AsQueryable();
            queryList = queryList.OrderByDescending(x => x.dcreatetime);

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;

            List<OUTASSIT> data = GetPageSize(queryList, PageSize, CurrendIndex).ToList();

            var source = from p in data
                         join asn in modContext.OUTASN on p.coutasnid equals asn.id into temp1
                         from tp in temp1.DefaultIfEmpty()
                         join oper in modContext.BASE_OPERATOR on p.ccreateownercode equals oper.caccountid into temp
                         from tt in temp.DefaultIfEmpty()
                         select new
                         {
                             p.id,
                             p.cticketcode,
                             p.cstatus,
                             CTCode = tp.cticketcode,
                             ccreateownercode = tt == null ? "" : tt.coperatorname,
                             IS_MERGE = p.is_merge,
                             p.dcreatetime,
                             tp.cerpcode,
                             tp.itype
                         };

            List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
            flagList.Add(new Tuple<string, string>("itype", "OUTTYPE"));
            flagList.Add(new Tuple<string, string>("cstatus", "ASSIT"));
            flagList.Add(new Tuple<string, string>("IS_MERGE", "IS_MergeCode"));

            var srcdata = GetGridSourceDataByList(source.ToList(), flagList);
            
            this.grdOUTASSIT.DataSource = srcdata;
            this.grdOUTASSIT.DataBind();
        
        }
    }

    #endregion

}

