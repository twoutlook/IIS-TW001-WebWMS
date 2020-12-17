using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;

public partial class Apps_BAR_FrmBarSNItemControlList :WMSBasePage
{

    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            InitPage();
            btnSearch_Click(btnSearch, EventArgs.Empty);
        }
    }

    public void InitPage()
    {
        cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
    }

    //网格绑定
    public void GridBind()
    {
          IGenericRepository<V_sn_item_head_config> entity = new GenericRepository<V_sn_item_head_config>(context);
        var caseList = from p in entity.Get()
                       orderby p.create_time descending
                       where 1 == 1
                       select p;

        if (txtCinvCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.item_head) && x.item_head.Contains(txtCinvCode.Text));


        
      
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        AspNetPager1.CustomInfoHTML = " "+Resources.Lang.WMS_Common_Pager_PageCount+":<b>" + "</b>";//总页数
        grdSNItem.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdSNItem.DataBind();
        
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
 
    //网格绑定
    protected void grdSNItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //查看
        //    var linkView = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
        //    linkView.NavigateUrl = "#";
        //    OpenFloatWinMax(linkView, BuildRequestPageURL("FrmBar_SNSplit.aspx", SysOperation.Modify, grdSNItem.DataKeys[e.Row.RowIndex].Values[0].ToString()), "查看SN条码", "SnCodeSplit");
        //}
    }

    #region add/print
    //public Dictionary<string, string> SelectIdCode
    //{
    //    set { ViewState["SelectIdCode"] = value; }
    //    get { return ViewState["SelectIdCode"] as Dictionary<string, string>; }
    //}

    ////打印
    //protected void btnprint_Click(object sender, EventArgs e)
    //{
    //    GetSelectedId();

    //    string punionid = "";

    //    foreach (var item in SelectIdCode)
    //    {
    //        punionid = punionid + "'" + item.Key + "',";
    //    }
    //    if (punionid != "")
    //    {
    //        punionid = punionid.Substring(0, punionid.Length - 1);
    //    }
    //    //获取拆分后SN
    //    DataTable dt = BARRule.GetSplitSn(punionid);
    //    dt.TableName = "SNList";
    //    var exist19Bar =
    //        dt.Rows.Cast<DataRow>().AsEnumerable().Where(dr => dr["SN"].ToString().Length == 19).Select(dr => dr).Count() >
    //        0
    //            ? true
    //            : false;
    //    var exist16Bar =
    //        dt.Rows.Cast<DataRow>().AsEnumerable().Where(dr => dr["SN"].ToString().Length == 16).Select(dr => dr).Count() >
    //        0
    //            ? true
    //            : false;
    //    if (exist19Bar & exist16Bar)
    //    {
    //        Alert("SN不能19位与16位混合打印");
    //        return;
    //    }
    //    var exist39Code =
    //        dt.Rows.Cast<DataRow>().AsEnumerable().Where(dr => dr["BAR_TYPE"].ToString() == "0").Select(dr => dr).Count() >
    //        0
    //            ? true
    //            : false;
    //    var exist128Code =
    //        dt.Rows.Cast<DataRow>().AsEnumerable().Where(dr => dr["BAR_TYPE"].ToString() == "1").Select(dr => dr).Count() >
    //        0
    //            ? true
    //            : false;
    //    if (exist39Code & exist128Code)
    //    {
    //        Alert("不能39码与128码混合打印");
    //        return;
    //    }

    //    Session["SNLength"] = "19";
    //    Session["CodeType"] = "128";
    //    Session["DT"] = dt;

    //    if (exist16Bar)
    //    {
    //        Session["SNLength"] = "16";
    //    }

    //    if (exist39Code)
    //    {
    //        Session["CodeType"] = "39";
    //    }

    //    this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("/Apps/BAR/Form_SN70X25_Print.aspx", SysOperation.New, "") + "','打印SN','BAR_SN',800,600);");
    //}

    ////获取ID
    //public void GetSelectedId()
    //{
    //    try
    //    {
    //        if (SelectIdCode == null)
    //        {
    //            SelectIdCode = new Dictionary<string, string>();
    //        }

    //        foreach (GridViewRow item in this.grdSNItem.Rows)
    //        {
    //            Control itemFindControl = item.FindControl("chkSelect");
    //            if (itemFindControl != null && itemFindControl is CheckBox)
    //            {
    //                CheckBox cbo = itemFindControl as CheckBox;

    //                //Product product = item.DataItem as Product;
    //                //获取ID
    //                string id = this.grdSNItem.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

    //                //控件选中且集合中不存在添加
    //                if (cbo.Enabled && cbo.Checked && !SelectIdCode.ContainsKey(id))
    //                {
    //                    SelectIdCode.Add(id, this.grdSNItem.DataKeys[item.RowIndex][0].ToString());
    //                }//未选中且集合中存在的移除                    
    //                else if (!cbo.Checked && SelectIdCode.ContainsKey(id))
    //                {
    //                    SelectIdCode.Remove(id);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        Alert(err.Message);
    //    }
    //}

    ////新增
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    this.WriteScript("PopupFloatWin('" +
    //                     BuildRequestPageURL("FrmBar_SNSplit.aspx", SysOperation.New, "") +
    //                     "','SN拆解','SnCodeSplit',800,600);");
    //    //绑定网格
    //    GridBind();
    //}
    #endregion
   
}