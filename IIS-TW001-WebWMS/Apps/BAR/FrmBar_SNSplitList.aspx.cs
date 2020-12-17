using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;

//SN拆解
public partial class Apps_BAR_FrmBar_SNSplitList : WMSBasePage
{
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
        IGenericRepository<V_BAR_SN_SPLITSQuery> entity = new GenericRepository<V_BAR_SN_SPLITSQuery>(context);
        var caseList = from p in entity.Get()
                       orderby p.createtime descending
                       where 1 == 1
                       select p;
        if (txtSN.Text.Trim() != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.split_sn) && x.split_sn.Contains(txtSN.Text.Trim()));      
        if (txtCinvCode.Text.Trim() != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvCode.Text.Trim()));

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = PageSize;
        }
        AspNetPager1.CustomInfoHTML = " "+Resources.Lang.WMS_Common_Pager_PageCount+":<b>" + "</b>";//总页数

        grdSNBar.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdSNBar.DataBind();
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
    protected void grdSNBar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          
            //ID
            string strKeyID = grdSNBar.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //查看
            var linkView = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkView.NavigateUrl = "#";
            OpenFloatWinMax(linkView, BuildRequestPageURL("FrmBar_SNSplit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBar_SNSplitList_ChaKanSNBarcode, "SnCodeSplit"); //查看SN条码     
        }
    }


    public Dictionary<string, string> SelectIdCode
    {
        set { ViewState["SelectIdCode"] = value; }
        get { return ViewState["SelectIdCode"] as Dictionary<string, string>; }
    }
    //打印
    protected void btnprint_Click(object sender, EventArgs e)
    {
        //GetSelectedId();

        //if (SelectIdCode.Count == 0)
        //{
        //    Alert("请选择需要打印的项！");
        //    return;
        //}

        //string punionid = "";

        //foreach (var item in SelectIdCode)
        //{
        //    punionid = punionid + "'" + item.Key + "',";
        //}
        //if (punionid != "")
        //{
        //    punionid = punionid.Substring(0, punionid.Length - 1);
        //}
        ////获取拆分后SN
        //DataTable dt = BARRule.GetSplitSn(punionid);
        //dt.TableName = "SNList";
        //var exist19Bar =
        //    dt.Rows.Cast<DataRow>().AsEnumerable().Where(dr => dr["SN"].ToString().Length == 19).Select(dr => dr).Count() >
        //    0
        //        ? true
        //        : false;
        //var exist16Bar =
        //    dt.Rows.Cast<DataRow>().AsEnumerable().Where(dr => dr["SN"].ToString().Length == 16).Select(dr => dr).Count() >
        //    0
        //        ? true
        //        : false;
        //if (exist19Bar & exist16Bar)
        //{
        //    Alert("SN不能19位与16位混合打印");
        //    return;
        //}
        //var exist39Code =
        //    dt.Rows.Cast<DataRow>().AsEnumerable().Where(dr => dr["BAR_TYPE"].ToString() == "0").Select(dr => dr).Count() >
        //    0
        //        ? true
        //        : false;
        //var exist128Code =
        //    dt.Rows.Cast<DataRow>().AsEnumerable().Where(dr => dr["BAR_TYPE"].ToString() == "1").Select(dr => dr).Count() >
        //    0
        //        ? true
        //        : false;
        //if (exist39Code & exist128Code)
        //{
        //    Alert("不能39码与128码混合打印");
        //    return;
        //}

        //Session["SNLength"] = "19";
        //Session["CodeType"] = "128";
        //Session["DT"] = dt;

        //if (exist16Bar)
        //{
        //    Session["SNLength"] = "16";
        //}

        //if (exist39Code)
        //{
        //    Session["CodeType"] = "39";
        //}

        //this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("/Apps/BAR/Form_SN70X25_Print.aspx", SysOperation.New, "") + "','打印SN','BAR_SN',800,600);");       
    }

    //获取ID
    public void GetSelectedId()
    {
        try
        {
            if (SelectIdCode == null)
            {
                SelectIdCode = new Dictionary<string, string>();
            }

            foreach (GridViewRow item in this.grdSNBar.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    CheckBox cbo = itemFindControl as CheckBox;

                    //Product product = item.DataItem as Product;
                    //获取ID
                    string id = this.grdSNBar.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                    //控件选中且集合中不存在添加
                    if (cbo.Enabled && cbo.Checked && !SelectIdCode.ContainsKey(id))
                    {
                        SelectIdCode.Add(id, this.grdSNBar.DataKeys[item.RowIndex][0].ToString());
                    }//未选中且集合中存在的移除                    
                    else if (!cbo.Checked && SelectIdCode.ContainsKey(id))
                    {
                        SelectIdCode.Remove(id);
                    }
                }
            }
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }

    //新增
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.WriteScript("PopupFloatWin('" +
                         BuildRequestPageURL("FrmBar_SNSplit.aspx", SYSOperation.New, "") +
                         "','SN'" + Resources.Lang.WMS_Common_Button_Split + ",'SnCodeSplit',800,600);");//SN拆解
        ////this.WriteScript("PopupFloatMax('" +
        ////                BuildRequestPageURL("FrmBar_SNSplit.aspx", SYSOperation.New, "") +
        ////                "','SN拆解','SnCodeSplit');");
        ////绑定网格
        //GridBind();
    }
    //拆解打印按钮
    protected void btnSplitprint_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    GetSelectedId();
        //    if (SelectIdCode.Count == 0)
        //    {
        //        Alert("请选择需要打印的项！");
        //        return;
        //    }

        //    string punionid = "";

        //    foreach (var item in SelectIdCode)
        //    {
        //        punionid = punionid + "'" + item.Key + "',";
        //    }
        //    if (punionid != "")
        //    {
        //        punionid = punionid.Substring(0, punionid.Length - 1);
        //    }
        //    // Alert(punionid);
        //    //获取拆分后SN
        //    DataTable dt = BARRule.GetSplitSNPrint(punionid);
        //    string strid = "";
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        strid += dt.Rows[i]["CINVCODE"].ToString() + ";" + dt.Rows[i]["SN"].ToString() + ";" +
        //                dt.Rows[i]["QTY"].ToString() + ";" + dt.Rows[i]["CPOSITIONCODE"].ToString() + ";" +
        //                dt.Rows[i]["PO"].ToString() + ";" + dt.Rows[i]["PCOUNT"].ToString() + ";" + "\r\n";

        //    }

        //    HidField_Split.Value = strid;
        //    Page.RegisterStartupScript("gggg", "<script>WriteToTxt();</script>");

        //}
        //catch (Exception err)
        //{
        //    Alert(err.Message);
        //}
    }
}