using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using DreamTek.ASRS.Business.SP.ProcedureModel;


/*
Roger
2013/7/2 8:44:29
20130702084429
解决狂点按钮重复提交问题
*/

public partial class Apps_STOCK_FrmStock_Check_Changes : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            InitPage();
        }
        this.btnModify.Attributes["onclick"] = this.GetPostBackEventReference(this.btnModify) + ";this.disabled=true;";
    }

    //界面初始化设置
    public void InitPage()
    {
        cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";

        //判断是否存在盘点计划
        IGenericRepository<STOCK_CHECK_PLAN> conn = new GenericRepository<STOCK_CHECK_PLAN>(db);
        STOCK_CHECK_PLAN bo = (from p in conn.Get()
                               where p.cstatus == "1"
                               select p).FirstOrDefault();

        if (bo == null || bo.id == null)
        {
            Alert(Resources.Lang.FrmStock_Check_Changes_Tips_WuPlan);//不存在有效的盘点计划，请确认
            return;
        }
        else
        {
            if (bo.flag.Equals("0"))
            {
                Alert(Resources.Lang.FrmStock_Check_Changes_Tips_WeiJieShu);//盘点未结束，不能进行差异调整
                return;
            }
            if (bo.flag.Equals("2"))
            {
                Alert(Resources.Lang.FrmStock_Check_Changes_Tips_YiTiaoZhen);//此盘点计划已经进行盘点库存调整
                return;
            }
            txtPlan.Text = bo.plan_name;
            HiddPlanId.Value = bo.planid;
        }
    }

    public IQueryable<view_stock_checkdiff> GetQueryList()
    {
        IGenericRepository<view_stock_checkdiff> conn = new GenericRepository<view_stock_checkdiff>(db);
        var caseList = from p in conn.Get()
                       orderby p.cinvcode descending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (HiddPlanId.Value != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ddefine3) && x.ddefine3.Contains(HiddPlanId.Value));
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
                caseList = caseList.OrderBy(" twodate desc ");
            }

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        grdStockCheck.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdStockCheck.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }


    //绑定网格
    public void GridBind()
    {
        Bind("");
    }

    public bool CheckData()
    {
        return true;
    }

    //分页1
    protected void grdStockCheck_PageIndexChanged(object sender, EventArgs e)
    {
        GridBind();
    }

    //分页2
    protected void grdStockCheck_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    //导出EXCEL
    protected DataTable NavgrdStockCheck_GetExportToExcelSource()
    {
        //var listQuery = new STOCK_FrmSTOCK_CHECKBILLListQuery();
        //var dtSource = listQuery.GetData(HiddPlanId.Value.Trim(), "0", -1, -1);
        //return dtSource;
        return null;

    }

    //查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        Bind("");
    }

    //库存调整
    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtPlan.Text.Trim()))
            {
                Alert(Resources.Lang.FrmStock_Check_Changes_Tips_NeedPlan);//请先输入盘点计划
                GridBind();
                return;
            }
            string msg = string.Empty;


            var proc = new Proc_Inventory_TurnOver
            {
                pPlanId = HiddPlanId.Value,
                pUserNo = WmsWebUserInfo.GetCurrentUser().UserNo
            };
            proc.Execute();
            if (proc.ReturnValue == 1) //非1-校验失败
            {
                msg = proc.ErrorMessage;
            }
            else
            {
                msg = Resources.Lang.WMS_Common_Msg_SaveSuccess;//保存成功！
            }
            Alert(msg);
        }
        catch (Exception ex)
        {
            Alert(ex.Message);
        }
        finally
        {
            this.btnModify.Style.Remove("disabled");
        }
    }
}