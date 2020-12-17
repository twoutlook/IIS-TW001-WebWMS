using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Text;
using DreamTek.ASRS.Business.SP;
using DreamTek.ASRS.Business.OUT;
using DreamTek.ASRS.Business.SP.ProcedureModel;
using Resources;


public partial class FrmDispatchUnitEdit : WMSBasePage //  PageBase, IPageEdit // 
{
    #region SQL
    //DBContext context = new DBContext();
     //20130407161331定义全局变量，用于保存id
    string OutAsn_Ids = string.Empty;    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtCTICKETCODE.Enabled = false;
       
        if (this.IsPostBack == false)
        {
            this.InitPage();
            this.GridBind();
        }
       
    }


    private DataTable GetList(string id,int CurrendIndex, int PageSize, out int pageCount)
    {
        pageCount = 0;
        string sql = " select v.*,s.FLAG_NAME AS CSTATUSNAME from dbo.WCS_TaskProcess_D v INNER JOIN dbo.V_SYS_PARAMETER s WITH(NOLOCK) ON s.FLAG_ID= v.CSTATUS AND s.FLAG_TYPE='COMMONDSTATUS' AND s.LANGUAGEID='" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' where 1=1 and v.ID='" + id + "'";
        string sqlCount = " select count(1) from dbo.WCS_TaskProcess_D v where 1=1 and ID='"+id+"'";
        string whereSql = string.Empty;

       

        sql = sql + whereSql;
        sqlCount = sqlCount + whereSql;

        PageSpliter ps = new PageSpliter(sql);
        ps.OrderBySql = " order by steps asc ";
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

    #region IPageGrid 成员

    public void GridBind()
    {
        this.CurrendIndex = 1;
        int pageCount = 0;
        var dt = GetList(this.KeyID.ToString(),CurrendIndex, PageSize, out pageCount);
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;
        this.grdTask_D.DataSource = dt;
        grdTask_D.DataBind();    

        //this.txtNEWLOC.Text = dt.Rows
        //this.txtloc.Text = "4-02";
        //this.txtCSO.Text =taskP.
        if (dt != null && dt.Rows.Count ==1) //说明就是RGV明细 RGV的只有一条
        {
            this.ddlMachine.SelectedItem.Text = dt.Rows[0]["MACHINE"].ToString().Trim();
        }
        else
           this.ddlMachine.SelectedValue = "0";

        ShowData();

       
    }

    #endregion

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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTASN');return false;";
        Help.DropDownListDataBind(GetParametersByFlagType("WCS_TaskProcess_Status"), this.ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
        //ddlTYPE 调度类型
        Help.DropDownListDataBind(GetParametersByFlagType("SCHEDULINGTYPE"), this.ddlTYPE, "", "FLAG_NAME", "FLAG_ID", "");
        //ddlMachine 运作设备
        Help.DropDownListDataBind(GetParametersByFlagType("MACHINE"), this.ddlMachine, "", "FLAG_NAME", "FLAG_ID", ""); 
    }



    private void SetTblFormControlEnabled(bool value)
    {
        this.txtCTICKETCODE.Enabled = value;
        this.txtDCREATETIME.Enabled = value;
        this.txtCCREATEOWNERCODE.Enabled = value;
        this.txtID.Enabled = value;
        this.ddlTYPE.Enabled = value;
        this.TxtPallet.Enabled = value;          
        this.txtCSO.Enabled = value;           
       // this.txtNEWLOC.Enabled = value;
        this.ddlMachine.Enabled = value;
       // this.txtloc.Enabled = value;
        this.ddlCSTATUS.Enabled = value;
        txtCMEMO.Enabled = value;             
     
    }

   
    public void ShowData()
    {
        var id= Request.QueryString["ID"].ToString();
        IGenericRepository<WCS_TaskProcess> con = new GenericRepository<WCS_TaskProcess>(context);
        var caseList = from p in con.Get().AsEnumerable()                      
                       select p;
        var taskP = caseList.ToList().FirstOrDefault(p=>p.ID==id);
       
        //this.txtID.Text = entity.ID.ToString();
        this.txtCCREATEOWNERCODE.Text = taskP.CREATEUSER;
        this.txtDCREATETIME.Text = taskP.CREATETIME.HasValue ? taskP.CREATETIME.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
        this.ddlCSTATUS.SelectedValue = taskP.CSTATUS;
        this.txtCTICKETCODE.Text = taskP.CTICKETCODE;
        this.TxtPallet.Text =taskP.PACKAGENO;
        this.txtCMEMO.Text = taskP.REMARK;
        if (string.IsNullOrEmpty(taskP.TASKTYPE))
            this.ddlTYPE.SelectedValue = "2";
        else this.ddlTYPE.SelectedValue = taskP.TASKTYPE;
        this.txtCSO.Text=taskP.SOURCECODE;

        SetTblFormControlEnabled(false);  
      
    }



    #endregion

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    #endregion


    protected void grdTask_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //string strKeyID = this.grdTask_D.DataKeys[e.Row.RowIndex].Values[0].ToString();
           
            //switch (e.Row.Cells[5].Text)
            //{
            //    case "0":
            //        e.Row.Cells[5].Text =Resources.Lang.FrmDispatchUnitEdit_MsgTitle32; //"未处理";
            //        break;
            //    case "1":
            //        e.Row.Cells[5].Text = Resources.Lang.FrmDispatchUnitEdit_MsgTitle33; //"命令已产生";
            //        break;
            //    default: e.Row.Cells[5].Text = Resources.Lang.FrmDispatchUnitEdit_MsgTitle34; //"命令执行完成";
            //        break;
            //}
           
        }
    }

    /// <summary>
    /// 强制完成调度单明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnComplete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        string taskDetailProcessId = (sender as LinkButton).CommandArgument;
        string outBill_Id = Guid.NewGuid().ToString();
        //  OUTASN entity = context.OUTASN.Where(x => x.id == outAsnId).FirstOrDefault();

        WCS_TaskProcess_D entity = context.WCS_TaskProcess_D.Where(x => x.IDS == taskDetailProcessId).FirstOrDefault();        

        if (entity == null)
        {
           // Alert("数据异常！");
            Alert(Resources.Lang.FrmDispatchUnitEdit_MsgTitle35+"!");
            return;
        }
        #region 调用存储过程

        Proc_AbnormalCompleteTaskProcess proc = new Proc_AbnormalCompleteTaskProcess();
        proc.P_ID = "";
        proc.P_IDS = taskDetailProcessId.Trim();
        proc.P_UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
        proc.Execute();
        if (proc.ReturnValue == 1)
        {
            Alert(proc.ErrorMessage);
        }
        else
        {
            //强制完成成功
            //Alert("强制完成成功！");
            Alert(Resources.Lang.FrmDispatchUnitEdit_MsgTitle36+"!");
            GridBind();
        }

        #endregion

    }
}






























