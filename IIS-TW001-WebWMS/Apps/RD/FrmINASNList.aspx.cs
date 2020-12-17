using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Text;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.SqlClient;
using DreamTek.ASRS.Business.RD;
using DreamTek.ExternalService.XueLong;

/// <summary>
/// 描述: 入库管理-->FrmINASNList 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:50:50
/// </summary>
/*
Roger
2013/7/31 13:46:49
20130731134649
增加校验是否为正在修改中的料号
*/
public partial class RD_FrmINASNList : WMSBasePage //IPageGrid
{
    #region SQL
    //DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {          
            if (Page.IsPostBack == false)
            {               
                this.InitPage();
                //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
                //this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                //this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");

            }
        }
        catch (Exception)
        {
        }
    }


    #region IPageGrid 成员

    //public void GridBind()
    //{
    //    int pageCount = 0;
    //    INQuery qry = new INQuery();
    //    var workType = this.ddlWorkType.SelectedValue.Trim();
    //    DataTable dt = qry.InAsnQuery(txtID.Text, txtCTICKETCODE.Text.Trim(), txtCSTATUS.SelectedValue, txtCCREATEOWNERCODE.Text.Trim(),
    //        txtCPO.Text.Trim(), txtERP_No.Text.Trim(), txtITYPE.Text,
    //        txtDCREATETIMEFrom.Text.Trim(), txtDCREATETIMETo.Text.Trim(), txtDAUDITDATEFrom.Text.Trim(),
    //        txtDAUDITDATETo.Text.Trim(), ddlREASONCODE.SelectedValue, txtCinvcode.Text.Trim(), workType, CurrendIndex,
    //        PageSize, out pageCount
    //        );

    //    AspNetPager1.CustomInfoHTML = " 总页数:<b>" + "</b>";
    //    AspNetPager1.RecordCount = pageCount;
    //    AspNetPager1.PageSize = this.PageSize;
    //    grdINASN.DataSource = dt;
    //    grdINASN.DataBind();
    //}

    public void GridBind()
    {
        string userNo = WmsWebUserInfo.GetCurrentUser().UserNo;
        List<string> userCaseType = GetUserCaseTypeNew(userNo);

        using (var modContext = this.context)
        {
            var queryList = from p in modContext.V_INASN 
                            //left join  q in modContext.BASE_OPERATOR b                             
                            orderby p.oby descending,p.dcreatetime descending,p.cstatus ascending
                            where 1 == 1
                            select p;
           
            if (!string.IsNullOrEmpty(this.txtID.Text))
            {
                queryList = queryList.Where(x => x.id.Contains(txtID.Text.Trim()));
            }
            //作业方式
            if (!string.IsNullOrEmpty(this.ddlWorkType.SelectedValue.Trim()))
            {
                queryList = queryList.Where(x => x.WORKTYPE.Contains(ddlWorkType.SelectedValue.Trim()));
            }

            if (!string.IsNullOrEmpty(txtCTICKETCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cticketcode.Contains(txtCTICKETCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCSTATUS.SelectedValue))
            {
                queryList = queryList.Where(x => x.cstatus.Contains(txtCSTATUS.SelectedValue.Trim()));
            }
            /*
             * Note by Qamar 2020-11-13
            else
            {
                queryList = queryList.Where(x => !x.cstatus.Contains("6") && !x.cstatus.Contains("3")); //全部条件下不显示已撤销与已完成的单据 XL-137 20200520
            }
            */
            if (!string.IsNullOrEmpty(txtCCREATEOWNERCODE.Text.Trim()))
            {
                queryList = queryList.Where(x => x.ccreateownercode.Contains(txtCCREATEOWNERCODE.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCPO.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.INASN_D.Any(p => p.id == x.id && p.cpo.Contains(txtCPO.Text.Trim())));
            }
            if (!string.IsNullOrEmpty(txtERP_No.Text.Trim()))
            {
                queryList = queryList.Where(x => x.cerpcode.Contains(txtERP_No.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtITYPE.Text))
            {
                queryList = queryList.Where(x => x.itype.Contains(txtITYPE.Text.Trim()));
            }
            else {
                //过滤全部的类型
                queryList = queryList.Where(x => userCaseType.Contains(x.itype));
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

            if (!string.IsNullOrEmpty(ddlREASONCODE.SelectedValue))
            {
                queryList = queryList.Where(x => x.reasoncode.Contains(ddlREASONCODE.SelectedValue.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.INASN_D.Any(p => p.id == x.id && p.cinvcode.Contains(txtCinvcode.Text.Trim())));
            }
            if (!string.IsNullOrEmpty(txtRank_Final.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.INASN_D.Any(p => p.cinvcode.Contains("-") && p.cinvcode.EndsWith("-" + txtRank_Final.Text.Trim())));
            }
            //规格
            if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
            {
                queryList = queryList.Where(x => modContext.INASN_D.Any(p => p.id == x.id && modContext.BASE_PART.Any(y => y.cspecifications.Contains(txtcspec.Text.Trim()) && p.cinvcode == y.cpartnumber)));
            }

             
       

            //queryList = queryList.Distinct().AsQueryable();
            //queryList = queryList.OrderByDescending(x => x.dcreatetime);
            //queryList = queryList.OrderByDescending(x => x.oby);

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;
                      
            //this.grdINASN.DataSource = GetPageSize(queryList, PageSize, CurrendIndex).ToList() ;
            var listResult = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();           
            flagList.Add(new Tuple<string, string>("WORKTYPE", "WorkType"));
            flagList.Add(new Tuple<string, string>("reasoncode", "BASE_DOCUREASON"));
            flagList.Add(new Tuple<string, string>("itype", "INTYPE"));
            flagList.Add(new Tuple<string, string>("DDEFINE3", "YorN"));
            var source = GetGridSourceDataByList(listResult, flagList);
            List<Tuple<string, string,string>> flagList1 = new List<Tuple<string, string,string>>();
            flagList1.Add(new Tuple<string, string, string>("cstatus", "cstatusName", "IS"));
            source = GetGridDataByDataTable(source, flagList1);         

            grdINASN.DataSource = source;
            this.grdINASN.DataBind();


        }
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsFirstPage)//判断是否是首页
            {
                CurrendIndex = 1;
                AspNetPager1.CurrentPageIndex = 1;
            }
            this.GridBind();
            IsFirstPage = true;//恢复默认值
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }
    public bool CheckData()
    {
        if (this.txtID.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDecimal(this.txtID.Text) == false)
            {
                //ID项不是有效的十进制数字
                this.Alert(Resources.Lang.FrmINASNList_MSG6+ "！");
                this.SetFocus(txtID);
                return false;
            }
        }
        if (this.txtCTICKETCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtCSTATUS.Text.Trim().Length > 0)
        {
        }
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate(this.txtDCREATETIMEFrom.Text) == false)
            {
                //制单日期项不是有效的日期
                this.Alert(Resources.Lang.FrmINASNList_MSG7 + "！");
                this.SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        if (this.txtDCREATETIMETo.Text.Trim() == "")
        {
            //到项不允许空
            this.Alert(Resources.Lang.FrmINASNList_MSG8 + "！");
            this.SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (this.txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate(this.txtDCREATETIMETo.Text) == false)
            {
                //到项不是有效的日期
                this.Alert(Resources.Lang.FrmINASNList_MSG9 + "！");
                this.SetFocus(txtDCREATETIMETo);
                return false;
            }
        }

        if (this.txtCPO.Text.Trim().Length > 0)
        {
        }
        if (this.txtITYPE.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDecimal(this.txtITYPE.Text) == false)
            {
                //入库类型项不是有效的十进制数字
                this.Alert(Resources.Lang.FrmINASNList_MSG11 + "！");
                this.SetFocus(txtITYPE);
                return false;
            }
        }
        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";


        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        //新建入库通知单
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmINASNEdit.aspx?Flag=0", SYSOperation.New, "") + "','" + Resources.Lang.FrmINASNList_MSG12+ "','INASN');return false;";
        this.btnErpReturn.Attributes["onclick"] = "PopupFloatWin('" + BuildRequestPageURL("frmInAsnByCompleteDisc.aspx?Flag=0", SYSOperation.New, "") + "','" + Resources.Lang.FrmINASNList_MSG12 + "','INASN',800,600);return false;";

        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmINASNEdit.aspx", SYSOperation.New,""),800,600);

        Help.DropDownListDataBind(GetParametersByFlagType("IS"), this.txtCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(GetInType(true), this.txtITYPE, Resources.Lang.Common_ALL, "FUNCNAME", "EXTEND1", "");
        Help.DropDownListDataBind(GetReasonCode("1"), this.ddlREASONCODE, Resources.Lang.Common_ALL, "REASONCONTENT", "REASONCODE", "");
        Help.DropDownListDataBind(GetParametersByFlagType("WorkType"), this.ddlWorkType, Resources.Lang.Common_ALL, "flag_name", "flag_id", "");
        //MonthOrWeek
        Help.RadioButtonDataBind(GetParametersByFlagType("MonthOrWeek"), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");

        if (!string.IsNullOrEmpty(Request.QueryString["Cstatus"]))
        {
            this.txtCSTATUS.SelectedValue = this.Request.QueryString["Cstatus"].ToString();
        }
    }

    #endregion

    /// <summary>
    /// 获取入库通知单类型-查询
    /// </summary>
    /// <param name="IsSearch"></param>
    /// <returns></returns>
    public DataTable GetInType(bool IsSearch)
    {
        string sql = string.Format(@"select distinct s.FLAG_NAME as FUNCNAME,f.cerpcode as EXTEND1 
                       from intype f inner join ( select FUNCNAME,EXTEND1 from UserFunction where userno='{0}') t  on t.EXTEND1 = f.cerpcode 
                       INNER JOIN dbo.V_SYS_PARAMETER s WITH(NOLOCK) ON s.FLAG_ID= f.cerpcode AND s.FLAG_TYPE='INTYPE' AND s.LANGUAGEID='"+System.Threading.Thread.CurrentThread.CurrentCulture.Name +"'                     where 1=1 ", WmsWebUserInfo.GetCurrentUser().UserNo);
        if (!IsSearch)
        {
            //增删改
            sql += " and f.cerpcode != '101' and (DISABLE_DATE is null or DISABLE_DATE >= sysdate)";
        }
        //sql += " and f.cerpcode!=2";
        return DBHelp.ExecuteToDataTable(sql);
    }

    /// <summary>
    /// 获取入库理由码信息
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public DataTable GetReasonCode(string type)
    {
        string sql = string.Format(@"SELECT A.REASONCODE,s.FLAG_NAME AS REASONCONTENT FROM BASE_DOCUREASON A INNER JOIN dbo.V_SYS_PARAMETER s WITH(NOLOCK) ON s.FLAG_ID= A.REASONCODE AND s.FLAG_TYPE='BASE_DOCUREASON' AND s.LANGUAGEID='" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'  WHERE A.STATES = 'Y' AND A.ACTIONSCOPE = '{0}'", type);
        return DBHelp.ExecuteToDataTable(sql);
    }

    /// <summary>
    /// 验证出库通知单的明细是否都生成了出库单
    /// </summary>
    /// <param name="outAsnId"></param>
    /// <returns></returns>
    public bool ValidateInAsn_DIsAllCreateInBill(string InAsnId)
    {
        string strSQL = string.Format(@"SELECT  COUNT(*)
                                        FROM    ( SELECT    iad.cinvcode ,
                                                            ISNULL(SUM(iad.iquantity), 0) InAsn_Qty ,
                                                            ( SELECT    ISNULL(SUM(ibd.iquantity), 0) iquantity
                                                              FROM      INBILL_D ibd
                                                                        INNER JOIN INBILL ib ON ibd.id = ib.id
                                                              WHERE     ibd.cinvcode = iad.cinvcode
                                                                        AND ib.casnid = '{0}'
                                                            ) InBill_Qty
                                                  FROM      INASN_D iad
                                                  WHERE     iad.id = '{0}'
                                                  GROUP BY  iad.cinvcode
                                                ) newTable", InAsnId);
        return Convert.ToInt32(DBHelp.ExecuteScalar(strSQL)) == 0 ? false : true;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<INASN> con = new GenericRepository<INASN>(context);
        IGenericRepository<IN_MERGE_PALLETE> cond = new GenericRepository<IN_MERGE_PALLETE>(context);
        try
        {
            for (int i = 0; i < this.grdINASN.Rows.Count; i++)
            {
                if (this.grdINASN.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdINASN.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        var cticketCode = this.grdINASN.Rows[i].Cells[2].Text;
                        var queryList = from p in cond.Get()
                                        orderby p.CREATETIME descending
                                        where p.INASNCODE==cticketCode && p.CSTATUS !="2"
                                        select p;
                        if (queryList != null && queryList.Count() > 0)
                        {
                            //此通知单已做过拼板,无法执行删除操作
                            msg = Resources.Lang.FrmINASNList_MSG13 + "!!";
                            break;  
                        }

                        INTYPE INASN_type = GetINTYPEByID(this.grdINASN.DataKeys[i].Values[0].ToString(), "INASN");
                         if (INASN_type != null && INASN_type.Is_Query == 1)
                         {
                             //仅查询类型的单据不允许删除
                             msg = Resources.Lang.FrmINASNList_MSG14 + "!!";
                             break;
                         }
                         else
                         {
                             //未处理
                             if (
                                 this.grdINASN.DataKeys[i].Values[3].ToString().Trim().Equals("0") ////this.grdINASN.Rows[i].Cells[this.grdINASN.Rows[i].Cells.Count - 4].Text.Trim().Equals("未处理" )
                                 && this.grdINASN.DataKeys[i][1].ToString().Trim().Equals("0")
                                 && InAsn.GetInAsnStatusByInAsnId(this.grdINASN.DataKeys[i].Values[0].ToString()).Trim().Equals("0"))
                             {
                                 if (!InAsn.ValidateIsCreateInBill(this.grdINASN.DataKeys[i].Values[0].ToString()))
                                 {
                                     string ID = this.grdINASN.DataKeys[i].Values[0].ToString();
                                     con.Delete(ID);
                                     con.Save();
                                     //删除明细
                                     Delete_InasnD(ID);
                                 }
                                 else
                                 {
                                     var isSpecialReturn = InAsn.isSpecialWipReturn(this.grdINASN.DataKeys[i].Values[0].ToString());
                                     if (isSpecialReturn)
                                     {
                                         //删除调拨单和入库单
                                         var Result = context.Database.ExecuteSqlCommand("Exec Proc_Delete_AsnDetial @pIDS,@pUserNo,@pReserveField1,@pReserveField2,@pRetCode,@pRetMsg", new SqlParameter("@pIDS", this.grdINASN.DataKeys[i].Values[0].ToString()), new SqlParameter("@pUserNo", WmsWebUserInfo.GetCurrentUser().UserNo), new SqlParameter("@pReserveField1", "1"), new SqlParameter("@pReserveField2", "2"), new SqlParameter("@pRetCode", 1), new SqlParameter("@pRetMsg", ""));
                                         if (Result != 1)//非1-校验失败
                                         {
                                             throw new Exception(Result.ToString());
                                         }
                                     }
                                     else
                                     {
                                         //该入库通知单已生成了入库单 不能删除
                                         throw new Exception(Resources.Lang.FrmINASNList_MSG16 + ".");
                                     }
                                 }
                             }
                             else
                             {
                                 //只有状态为[未處理] 或 WMS系统新增 的单据才能删除
                                 throw new Exception(Resources.Lang.FrmINASNList_MSG17 + ".");
                             }
                         }
                    }
                }
            }
            if (msg.Length == 0)
            {
                //删除成功
                msg = Resources.Lang.CommonB_RemoveSuccess + "!";
            }

            this.GridBind();
        }
        catch (Exception Ex)
        {
            //删除失败

            msg += Resources.Lang.CommonB_RemoveFailed + "![" + Ex.Message + "]";
        }
        this.Alert(msg);
    }

    private void Delete_InasnD(string id)
    {
        IGenericRepository<INASN_D> cond = new GenericRepository<INASN_D>(context);
        string strSQL = string.Format(@"SELECT ids FROM dbo.INASN_D WHERE id = '{0}'", id);
        DataTable dt_d = DBHelp.ExecuteToDataTable(strSQL);
        if (dt_d != null && dt_d.Rows.Count > 0)
        {
            foreach (DataRow item in dt_d.Rows)
            {
                cond.Delete(item[0].ToString());
                cond.Save();
            }
        }
    }

    private string GetKeyIDS(int rowIndex)
    {
        return this.grdINASN.DataKeys[rowIndex].Values[0].ToString();
    }

    protected void grdINASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
                HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 3].Controls[0];
                linkModify.NavigateUrl = "#";
                //判断是否为特殊退料
                var isSpecialReturn = InAsn.isSpecialWipReturn(strKeyID);
                if (isSpecialReturn)
                {
                    //"入库通知单"
                    this.OpenFloatWinMax(linkModify, BuildRequestPageURL("frmInAsnByCompleteDisc.aspx?Flag=1&ids=" + strKeyID, SYSOperation.Modify, strKeyID), Resources.Lang.FrmINASNEdit_Title1, "INASN");

                }
                else
                {
                    this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmINASNEdit.aspx?Flag=1&ids=" + strKeyID + "&IsSpecialPage=0&IsSpecialWipReturn=" + this.grdINASN.DataKeys[e.Row.RowIndex][2], SYSOperation.Modify, strKeyID), Resources.Lang.FrmINASNEdit_Title1, "INASN");//入库通知单
                }
                HyperLink linkModify_Error = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
                linkModify_Error.NavigateUrl = "#";

                if (new SystemLogs().validateIsExistErrorMsg(this.grdINASN.DataKeys[e.Row.RowIndex][0].ToString()))
                {
                    linkModify_Error.Style.Add("color", "Red");
                    this.OpenFloatWin(linkModify_Error, BuildRequestPageURL("/Apps/SystemLogs/FrmLogSystemList.aspx?Flag=1&ids=" + strKeyID + "&TableName=INBILL", SYSOperation.Modify, strKeyID),Resources.Lang.FrmINBILLList_MSG10, "ErrorMsg", 800, 400);

                }
                else
                {
                    linkModify_Error.Enabled = false;
                }

                LinkButton lbtnCreateInBill = e.Row.FindControl("lbtnCreateInBill") as LinkButton;
               
                string userNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                switch (e.Row.Cells[e.Row.Cells.Count - 4].Text)
                {
                    //0 未处理,1,已指引，2,上架中，3 已完成,）
                    //未处理
                    case "0":
                        //statusStr = "未处理";
                        if (ValidateInAsn_DIsAllCreateInBill(strKeyID) && !isSpecialReturn)
                        {
                            lbtnCreateInBill.Enabled = true;
                        }
                        break;
                }
                
        }
        }
        catch (Exception)
        {
        }
    }



    /// <summary>
    /// 生成入库单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnCreateInBill_Click(object sender, EventArgs e)
    {
        List<string> SparaList = new List<string>();
        try
        {
            string InAsnId = (sender as LinkButton).CommandArgument;
            INTYPE INASN_type = GetINTYPEByID(InAsnId, "INASN");
            if (INASN_type.Is_Query == 0)
            {
                string InBill_Id = Guid.NewGuid().ToString();
                //20130731134649 存在修改中的料时，不允许生成出库单
                var Asn = new INASN();
                Asn.id = InAsnId;

                #region 调用存储过程
                SparaList.Add("@P_InAsn_id:" + InAsnId.Trim());
                SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                SparaList.Add("@P_InBill_Id:" + InBill_Id.Trim());
                SparaList.Add("@P_IsTemporary:" + "0");
                SparaList.Add("@P_ReturnValue:" + "");
                SparaList.Add("@INFOTEXT:" + "");
                string[] Result = DBHelp.ExecuteProc("Proc_CreateInBill", SparaList);
                if (Result.Length == 1)//调用存储过程有错误
                {
                    this.Alert(Result[0].ToString());
                }
                else if (Result[0] == "0")
                {
                    //生成成功 跳转到入库单页面
                    this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("FrmINBILLEdit.aspx", SYSOperation.Modify, InBill_Id) + "','" + Resources.Lang.FrmLogSystemList_Msg04 + "','INBILL');");
                }
                else
                {
                    this.Alert(Result[1]);
                }
                #endregion
            }
            else
            {
                //此入库通知单类型为仅查询，不能生成入库单
                Alert(Resources.Lang.FrmINASNList_MSG18 + "！");
            }
        }
        catch (Exception)
        {
        }
    }

    /// <summary>
    /// 手动获取ERP入库通知单数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSync_Click(object sender, EventArgs e)
    {
        string syncMsg = new DataSync().SyncINASNManual();
        if (syncMsg=="0")
        {
            this.Alert(Resources.Lang.Common_SyncInterface_Success);//"获取ERP通知单成功！"
            this.btnSearch_Click(null, null);
        }
        else
        {
            this.Alert(Resources.Lang.Common_SyncInterface_Fail + syncMsg);//获取ERP通知单失败！
        }
    }
    #endregion
    
    protected void BtnRevoke_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        int selectedCount = 0;
       
        IGenericRepository<INASN> inasnCon = new GenericRepository<INASN>(context);
        IGenericRepository<INASN_D> cond = new GenericRepository<INASN_D>(context);
        IGenericRepository<IN_MERGE_PALLETE> conds = new GenericRepository<IN_MERGE_PALLETE>(context);
        if (this.grdINASN.Rows.Count == 0)
        {
            //请选择要撤销的入库通知单
            Alert(Resources.Lang.FrmINASNList_MSG25 + "!");
            return;
        }

        for (int i = 0; i < this.grdINASN.Rows.Count; i++)
        {
            if (this.grdINASN.Rows[i].Cells[0].Controls[1] is CheckBox)
            {
                CheckBox chkSelect = (CheckBox)this.grdINASN.Rows[i].Cells[0].Controls[1];
                if (chkSelect.Checked)
                {
                    selectedCount = selectedCount + 1;

                    var cticketCode = this.grdINASN.Rows[i].Cells[2].Text;
                    var queryList = from p in conds.Get()
                                    orderby p.CREATETIME descending
                                    where p.INASNCODE == cticketCode && p.CSTATUS != "2"
                                    select p;
                    if (queryList != null && queryList.Count() > 0)
                    {
                        //此通知单已做过拼板,无法执行撤销操作
                        msg = Resources.Lang.FrmINASNList_MSG20 + "!!";
                        break;
                    }

                    //var itypeName = this.grdINASN.Rows[i].Cells[this.grdINASN.Rows[i].Cells.Count - 12].Text.Trim();
                    INTYPE INASN_type = GetINTYPEByID(this.grdINASN.DataKeys[i].Values[0].ToString(), "INASN");
                    if (INASN_type!=null&& INASN_type.Is_Query == 1)
                    {
                        //仅查询类型的单据不允许撤销
                        msg = Resources.Lang.FrmINASNList_MSG21 + "!!";
                        break;
                    }
                    else
                    {
                        var id = this.grdINASN.DataKeys[i].Values[0].ToString();
                        var inasnEntity = inasnCon.Get().Where(x => x.id == id).FirstOrDefault();

                        if (inasnEntity != null && inasnEntity.cstatus.Equals("0"))
                        {
                            var icount = 0;                      
                            var indEntity = (from p in cond.Get()
                                                where p.id == id
                                                select p).ToList<INASN_D>();

                            foreach (var item in indEntity)
                            {
                                if (item.cstatus != "0")
                                {
                                    icount = icount + 1;
                                }
                            }

                            if (icount == 0)
                            {
                                inasnEntity.cstatus = "6";
                                inasnCon.Update(inasnEntity);
                                inasnCon.Save();

                                foreach (var item in indEntity)
                                {
                                    item.cstatus = "3";
                                    cond.Update(item);
                                    cond.Save();
                                }
                            }
                            else
                            {
                                //您选中的入库通知单明细含有状态不是未处理的，不能执行撤销操作
                                msg = Resources.Lang.FrmINASNList_MSG22 + "!";
                                break;
                            }
                        }
                        else
                        {
                            //您选中的入库通知单含有状态不是未处理的，不能执行撤销操作
                            msg = Resources.Lang.FrmINASNList_MSG23 + "!";
                            break;
                        }
                    }
                }
                   
            }
        }

        if (selectedCount > 0)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                Alert(msg);
            }
            else
            {
                //撤销成功
                Alert(Resources.Lang.FrmINASNList_MSG24 + "!");
                this.btnSearch_Click(null, null);
            }
        }
        else
        {
            //请选择要撤销的入库通知单
            Alert(Resources.Lang.FrmINASNList_MSG25 + "!");
        }        
    }


}

