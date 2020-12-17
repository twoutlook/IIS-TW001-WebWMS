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
using DreamTek.ASRS.Business.OUT;
using DreamTek.ASRS.Business.SP.ProcedureModel;
using DreamTek.ASRS.Business.SP;


public partial class FrmDispatchUnitList : WMSBasePage//PageBase, IPageGrid
{
    #region SQL
    //DBContext context = new DBContext();    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            
            this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
       
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        int pageCount = 0;
        DataTable dt = new DataTable();       
        var ticketCode = this.txtCTICKETCODE.Text.Trim();
        var cStatus= this.txtCSTATUS.Text.Trim();
        var erpCode=string.Empty;
        var createdUser=this.txtCCREATEOWNERCODE.Text.Trim();
        var type = this.txtITYPE.Text.Trim();
        var pallet = this.txtPalletCode.Text.Trim();
        var createdTimeFrom=this.txtDCREATETIMEFrom.Text.Trim();
        var createdTimeTo=this.txtDCREATETIMETo.Text.Trim();
        var dauditTimeFrom=this.txtDAUDITDATEFrom.Text.Trim();
        var dauditTimeTo = this.txtDAUDITDATETo.Text.Trim();
        var sourceCode=this.txtSO.Text.Trim();
        var loc = this.txtFrmSiteNo.Text.Trim();
        var newloc = this.txtToSiteNo.Text.Trim();

        dt = GetList(ticketCode, cStatus,erpCode,createdUser,type,sourceCode,createdTimeFrom,createdTimeTo,dauditTimeFrom,dauditTimeTo,pallet,loc,newloc, CurrendIndex, PageSize, out pageCount);
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + ":<b>" + "</b>";//" 总页数:<b>" + "</b>";
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;
        grdOUTASN.DataSource = dt;
        grdOUTASN.DataBind();
    }

    private DataTable GetList(string ticketCode, string cStatus, string erpCode, string createdUser, string type, string sourceCode, string createdTimeFrom, string createdTimeTo, 
                               string dauditTimeFrom, string dauditTimeTo, string pallet,string loc,string newloc, int CurrendIndex, int PageSize, out int pageCount)
    {
        pageCount = 0;
        string sql = @" select v.*,s.FLAG_NAME AS CSTATUSNAME,t.FLAG_NAME AS TASKTYPENAME from dbo.WCS_TaskProcess v
										 INNER JOIN dbo.V_SYS_PARAMETER s WITH(NOLOCK) ON s.FLAG_ID= v.CSTATUS AND s.FLAG_TYPE='WCS_TaskProcess_Status' AND s.LANGUAGEID='" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' INNER JOIN dbo.V_SYS_PARAMETER t WITH(NOLOCK) ON t.FLAG_ID= v.TASKTYPE AND t.FLAG_TYPE='SCHEDULINGTYPE' AND t.LANGUAGEID='" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' where 1=1 ";
        string sqlCount = " select count(1) from dbo.WCS_TaskProcess v where 1=1";
        string whereSql = string.Empty;

        if (!string.IsNullOrEmpty(ticketCode))
        {
            whereSql = whereSql + string.Format(" and v.CTICKETCODE like '%{0}%' ", ticketCode);
        }
       
        if (!string.IsNullOrEmpty(cStatus))
        {
            whereSql = whereSql + string.Format(" and v.CSTATUS like '%{0}%' ", cStatus);
        }

        
        if (!string.IsNullOrEmpty(createdUser))
        {
            whereSql = whereSql + string.Format(" and v.CREATEUSER='{0}' ", createdUser);
        }
        if (!string.IsNullOrEmpty(type))
        {
            if (type == "2") 
                whereSql = whereSql + string.Format(" and (v.TASKTYPE IS NULL OR v.TASKTYPE = '')");
            else
                whereSql = whereSql + string.Format(" and v.TASKTYPE like '%{0}%' ", type);
        }
        if (!string.IsNullOrEmpty(createdTimeFrom))
        {
            whereSql = whereSql + string.Format(" and CONVERT(varchar(10),v.CREATETIME,120) >= '{0}' ", createdTimeFrom);
        }
        if (!string.IsNullOrEmpty(createdTimeTo))
        {
            whereSql = whereSql + string.Format(" and CONVERT(varchar(10),v.CREATETIME,120) <= '{0}' ", createdTimeTo);
        }
        if (!string.IsNullOrEmpty(sourceCode))
        {
            whereSql = whereSql + string.Format(" and v.SOURCECODE like '%{0}%' ", sourceCode);
        }     

        if (!string.IsNullOrEmpty(dauditTimeFrom))
        {
            whereSql = whereSql + string.Format(" and convert(varchar(10),v.dindate,120) >= '{0}' ", dauditTimeFrom);
        }
        if (!string.IsNullOrEmpty(dauditTimeTo))
        {
            whereSql = whereSql + string.Format(" and convert(varchar(10),v.dindate,120)  <= '{0}' ", dauditTimeTo);
        }                             
       
        if (!string.IsNullOrEmpty(pallet))
        {
            whereSql = whereSql + string.Format(" and v.PACKAGENO like '%{0}%' ", pallet);
        }
        if (!string.IsNullOrEmpty(loc))  //原始站点
        {
            whereSql = whereSql + string.Format(" and EXISTS (SELECT 1 FROM dbo.WCS_TaskProcess_D d WHERE d.id=v.ID AND d.loc LIKE '%{0}%')", loc);
        }
        if (!string.IsNullOrEmpty(newloc))  //目的站点
        {
            whereSql = whereSql + string.Format(" and EXISTS (SELECT 1 FROM dbo.WCS_TaskProcess_D d WHERE d.id=v.ID AND d.NEWLOC LIKE '%{0}%')", newloc);
        }
       
        sql = sql + whereSql;
        sqlCount = sqlCount + whereSql;

        PageSpliter ps = new PageSpliter(sql);
        ps.OrderBySql = " order by CREATETIME desc ";
        ps.PageSize = PageSize;
        ps.PageIndex = CurrendIndex;
        var psSql = ps.GetPageSQL();

        DataTable tb = SqlDBHelp.ExecuteToDataTable(psSql);
        Object obj = SqlDBHelp.ExcuteScalarSQL(sqlCount);
        if (obj != null)
        {
            int.TryParse(obj.ToString(), out pageCount);
        }

        return tb;
    }


    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";

       // this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmDispatchUnitEdit.aspx", SYSOperation.New, "") + "','新建调度单','WCS_TaskProcess');return false;";
        //txtITYPE 调度类型
        Help.DropDownListDataBind(GetParametersByFlagType("SCHEDULINGTYPE"), this.txtITYPE, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");   //全部  
        //txtCSTATUS 状态 WCS_TaskProcess_Status
        Help.DropDownListDataBind(GetParametersByFlagType("WCS_TaskProcess_Status"), this.txtCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");   //全部  
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
            this.GridBind();
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }

   

    private string GetKeyIDS(int rowIndex)
    {
        return this.grdOUTASN.DataKeys[rowIndex].Values[0].ToString();
    }

    private Dictionary<string, string> dict = new Dictionary<string, string>();

    protected void grdOUTASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string strKeyID =  this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            //this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmDispatchUnitEdit.aspx", SYSOperation.Modify, strKeyID), "调度通知单", "WCS_TaskProcess_D");
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmDispatchUnitEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmDispatchUnitList_MsgTitle13, "WCS_TaskProcess_D");

            //switch (e.Row.Cells[3].Text)
            //{
            //    case "0":
            //        e.Row.Cells[3].Text = Resources.Lang.FrmDispatchUnitEdit_MsgTitle32; //"未处理";
            //        break;
            //    case "1":
            //        e.Row.Cells[3].Text = Resources.Lang.FrmDispatchUnitList_MsgTitle2;//"处理中";
            //        break;
            //    default: e.Row.Cells[3].Text = Resources.Lang.FrmDispatchUnitList_MsgTitle3;//"已完成";
            //        break;
            //}

            //switch (e.Row.Cells[2].Text)
            //{
            //    case "0":
            //        e.Row.Cells[2].Text = Resources.Lang.FrmDispatchUnitEdit_MsgTitle6;//"手动叫车";
            //        break;
            //    case "1":
            //        e.Row.Cells[2].Text = Resources.Lang.FrmDispatchUnitEdit_MsgTitle7;//"产线叫车";
            //        break;
            //    case "OUT":
            //        e.Row.Cells[2].Text = Resources.Lang.FrmDispatchUnitEdit_MsgTitle8;//"出库";
            //        break;
            //    default: e.Row.Cells[2].Text = Resources.Lang.FrmDispatchUnitEdit_MsgTitle9;//"其它";
            //        break;
            //}

        }
    }

   

    #endregion
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();

    }
    protected void grdOUTASN_Sorting(object sender, GridViewSortEventArgs e)
    {
         string sortName = e.SortExpression;
        if (SortedField.Equals(sortName))
        {
            if (SortedAD.Equals(Ascending))
            {
                SortedAD = Descending;//取反
            }
            else
            {
                SortedAD = Ascending;
            }
        }
        else
        {
            SortedField = sortName;
            SortedAD = Ascending;
        }
        GridBind();

    }

    /// <summary>
    /// 强制完成调度单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnComplete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        string taskProcessId = (sender as LinkButton).CommandArgument;
        string outBill_Id = Guid.NewGuid().ToString();
      //  OUTASN entity = context.OUTASN.Where(x => x.id == outAsnId).FirstOrDefault();
        WCS_TaskProcess entity = context.WCS_TaskProcess.Where(x => x.ID == taskProcessId).FirstOrDefault();

        if (entity == null)
        {
            //Alert("数据异常！");
            Alert(Resources.Lang.FrmDispatchUnitEdit_MsgTitle35+"!");
            return;
        }   
        #region 调用存储过程

        Proc_AbnormalCompleteTaskProcess proc = new Proc_AbnormalCompleteTaskProcess();
        proc.P_ID = taskProcessId.Trim();
        proc.P_IDS = "";
        proc.P_UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
        proc.Execute();
        if (proc.ReturnValue == 1)
        {
            Alert(proc.ErrorMessage);
        }
        else
        {
            //强制完成成功
            Alert(Resources.Lang.FrmDispatchUnitEdit_MsgTitle36 + "!");
            btnSearch_Click(null, null);
        }
       
        #endregion

    }

}