using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq;

public partial class Apps_STOCK_FrmSTOCK_CHECKBILL_PLAN : WMSBasePage
{

    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            InitPage();
            btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }

    //界面初始化设置
    public void InitPage()
    {
        cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";        
        Help.DropDownListDataBind(new SysParameterList().GetParametersByFlagType("Common_Enable"), this.ddlStatus, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");      
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }
    //绑定网格
    public void GridBind()
    {
        IGenericRepository<STOCK_CHECK_PLAN> entity = new GenericRepository<STOCK_CHECK_PLAN>(context);
        var caseList = from p in entity.Get()
                       orderby p.freeze_date descending
                       where 1 == 1
                       select p;
        if (!string.IsNullOrEmpty(txtPlanID.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.planid) && x.planid.Contains(txtPlanID.Text.Trim()));
        if (txtPlan.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.plan_name) && x.plan_name.Contains(txtPlan.Text.Trim()));

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
     
        var listResult = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("CSTATUS", "CSTATUSName", "Common_Enable"));//状态
        var source =GetGridDataByAddColumns(listResult, flagList);        
        //grdPlan.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdPlan.DataBind();
    }
    public bool CheckData()
    {
        return true;
    }
    //状态更改
    protected void btnUpStatus_Click(object sender, EventArgs e)
    {
        var Status = ddlStatus.SelectedValue.Trim();
        try
        {
            if (Status.Equals("0"))//更改为无效状态
            {
                for (int i = 0; i < grdPlan.Rows.Count; i++)
                {
                    if (grdPlan.Rows[i].Cells[0].Controls[1] is CheckBox)
                    {
                        var chkSelect = (CheckBox)grdPlan.Rows[i].Cells[0].Controls[1];
                        if (chkSelect.Checked)
                        {
                            if (Stock_Checked.GetFlag("0", grdPlan.DataKeys[i].Values[0].ToString().Trim()))
                            {
                                Alert(Resources.Lang.FrmSTOCK_CHECKBILL_PLAN_PlanWuxiao);//此盘点计划状态已经为无效，无需更改
                                return;
                            }
                            //更改为无效
                            Stock_Checked.UpdateStatus("2", WmsWebUserInfo.GetCurrentUser().UserNo, grdPlan.DataKeys[i].Values[0].ToString().Trim());
                        }
                    }
                }
            }
            else
            {
                if (Stock_Checked.GetFlag("2", ""))
                {
                    msgBox.Show();
                    return;
                }
                UpdatePlanStatus();
            }

            AlertAndBack("FrmSTOCK_CHECKBILL_PLAN.aspx?", Resources.Lang.WMS_Common_Msg_UpdateSuccess);//更新成功
            return;
        }
        catch (Exception ex)
        {
            this.Alert(Resources.Lang.FrmSTOCK_CHECKBILL_PLAN_UpdateFailed + ex.Message);//更改状态失败！
        }
    }

    //ok
    protected void okbutton_Click(object sender, EventArgs e)
    {
        UpdatePlanStatus();
        AlertAndBack("FrmSTOCK_CHECKBILL_PLAN.aspx?", Resources.Lang.WMS_Common_Msg_UpdateSuccess);//更新成功
        msgBox.Hide();
    }

    //cancel
    protected void cancelbutton_Click(object sender, EventArgs e)
    {
        msgBox.Hide();
    }

    //更新
    private void UpdatePlanStatus()
    {
        Stock_Checked.UpdateStatus("0", WmsWebUserInfo.GetCurrentUser().UserNo, "");
        for (int i = 0; i < grdPlan.Rows.Count; i++)
        {
            if (grdPlan.Rows[i].Cells[0].Controls[1] is CheckBox)
            {
                var chkSelect = (CheckBox)grdPlan.Rows[i].Cells[0].Controls[1];
                if (chkSelect.Checked)
                {
                    if (Stock_Checked.GetFlag("1", grdPlan.DataKeys[i].Values[0].ToString().Trim()))
                    {
                        throw new Exception(Resources.Lang.FrmSTOCK_CHECKBILL_PLAN_PlanWuxiao);//此盘点计划状态已经为有效，无需更改
                    }
                    Stock_Checked.UpdateStatus("1", WmsWebUserInfo.GetCurrentUser().UserNo, grdPlan.DataKeys[i].Values[0].ToString().Trim());
                }
            }
        }
    }
}