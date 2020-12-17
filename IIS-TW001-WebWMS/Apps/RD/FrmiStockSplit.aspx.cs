using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business.SP.ProcedureModel;

public partial class Apps_RD_FrmiStockSplit : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtID.Text = Request.QueryString["ID"].ToString();
            this.InitPage();
            btnSearch_Click(null, null);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ////RD_FrmINASN_DList listQuery = new RD_FrmINASN_DList();
        //DataTable dtRowCount = RD_FrmINASN_DList.GetList_Split(txtID.Text, "", true, 0, 0);
        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdNavigatorINASN_D.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //    hfIsConfirm.Value = "1";
        //}
        //else
        //{
        //    this.grdNavigatorINASN_D.RowCount = 0;
        //}
        this.GridBind();
    }

    #region IPageGrid 成員

    public void GridBind()
    {
        CurrendIndex = 1;
        IGenericRepository<V_INASNSplit> con = new GenericRepository<V_INASNSplit>(context);
        var list = from p in con.Get() 
                    orderby p.CERPCODELINE 
                     where p.ID==txtID.Text  
                       select p;


        if (list != null && list.Count() > 0)
        {
            AspNetPager1.RecordCount = list.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }
        grdINASN_D.DataSource = GetPageSize(list, PageSize, CurrendIndex).ToList();
        grdINASN_D.DataBind();

        //this.grdINASN_D.DataSource = list;
        //this.grdINASN_D.DataBind();
        //RD_FrmINASN_DList listQuery = new RD_FrmINASN_DList();
        //DataTable dtSource = RD_FrmINASN_DList.GetList_Split(txtID.Text, string.Empty, false, this.grdNavigatorINASN_D.CurrentPageIndex, this.grdINASN_D.PageSize);
        //this.grdINASN_D.DataSource = dtSource;
        //this.grdINASN_D.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    public bool CheckData()
    {
       

        return true;
    }

    #endregion

    #region IPage 成員

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('FrmiStockSplit');return false;";
    }

    #endregion

    protected void grdINASN_D_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdNavigatorINASN_D.IsDbPager)
        //{
        //    grdNavigatorINASN_D.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.grdINASN_D.PageIndex = e.NewPageIndex;
        //}
    }

    protected void grdINASN_D_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }

    /// <summary>
    /// 獲取選擇的ID
    /// </summary>
    public void GetSelectedIds()
    {
        if (SelectIds == null)
        {
            SelectIds = new Dictionary<string, string>();
        }

        foreach (GridViewRow item in this.grdINASN_D.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //獲取ID
                string ids = this.grdINASN_D.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                //控件選中且集合中不存在添加
                if (cbo.Checked && !SelectIds.ContainsKey(ids))
                {
                    SelectIds.Add(ids, ids);
                }//未選中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(ids))
                {
                    SelectIds.Remove(ids);
                }
            }
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorOUTASN
    }

    protected void grdINASN_D_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }

    private string GetKeyIDS(int rowIndex)
    {
        return this.grdINASN_D.DataKeys[rowIndex].Values[0].ToString();
    }

    private Dictionary<string, string> dict = new Dictionary<string, string>();

    protected void grdINASN_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var txtLessQty = e.Row.FindControl("txtLessQty") as TextBox;
            txtLessQty.Enabled = false;
        }
    }

    protected void grdINASN_D_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdOUTASN_D_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.ReturnValue is DataTable)
        {
            //if (grdNavigatorINASN_D.IsDbPager == false)
            //    this.grdNavigatorINASN_D.RowCount = ((DataTable)e.ReturnValue).Rows.Count;
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //判斷選中幾條記錄
        var CheckCount = (from GridViewRow item in this.grdINASN_D.Rows
                          let itemFindControl = item.FindControl("chkSelect")
                          where itemFindControl != null && itemFindControl is CheckBox
                          let cbo = itemFindControl as CheckBox
                          where cbo.Checked
                          select item).Count();
        if (CheckCount == 0)
        {
            Alert(Resources.Lang.FrmiStockSplit_MSG1+"!");//請選擇需要拆解的料號
            return;
        }

        for (int i = 0; i < grdINASN_D.Rows.Count; i++)
        {
            var chk = grdINASN_D.Rows[i].FindControl("chkSelect") as CheckBox;
            float CjQty = 0;
            float qty = 0;
            float lessQty = 0;

            if (chk.Checked)
            {
                var cinQty = grdINASN_D.DataKeys[i].Values["IQUANTITY"].ToString();//物料数量
                var txtLessQty = grdINASN_D.Rows[i].FindControl("txtLessQty") as TextBox;//可拆解数量
                var txtCjQty = grdINASN_D.Rows[i].FindControl("txtCjQty") as TextBox;//拆解数量

                var result=0;

                string pkNumMSG = string.Empty;
                if (!(Comm_Fun.Fun_IsDecimal(txtCjQty.Text, 0, 0, 1, out pkNumMSG)))
                {
                    //第..行
                    this.Alert(Resources.Lang.FrmiStockSplit_MSG2 + "" + (i + 1) + Resources.Lang.FrmiStockSplit_MSG3 + "，" + pkNumMSG);
                    this.SetFocus(txtCjQty);
                    return;
                }
                //if (Int32.TryParse(txtCjQty.Text,out result))
                //{
                //    CjQty = txtCjQty.Text.ToInt();
                //    if (CjQty < 0)
                //    {
                //        Alert("第" + (i + 1) + "行，拆解數量不能小于0！");
                //        return;
                //    }
                //    else if (CjQty == 0)
                //    {
                //        Alert("第" + (i + 1) + "行，拆解數量不能等于0！");
                //        return;
                //    }
                //    else
                //    {

                float.TryParse(cinQty, out qty);
                float.TryParse(txtLessQty.Text, out lessQty);
                if (CjQty > lessQty)
                {
                    //+ "第"行，拆解數量+ "不能大于可拆解數量"
                    Alert(Resources.Lang.FrmiStockSplit_MSG2 + (i + 1) + Resources.Lang.FrmiStockSplit_MSG4 + CjQty + Resources.Lang.FrmiStockSplit_MSG5  + lessQty + "！");
                    return;
                }
                //    }
                //}
                //else
                //{
                //    Alert("第" + (i + 1) + "行，拆解數量不是数字！");
                //    return;
                //}

            }
        }

        //删除已存在的未处理的数据
        IGenericRepository<INASN_D_ISTOCK_SPLIT> con = new GenericRepository<INASN_D_ISTOCK_SPLIT>(context);

        List<INASN_D_ISTOCK_SPLIT> isplit = (from p in con.Get()
                                   where p.ASN_ID == txtID.Text.Trim() && p.FLAG == "0"
                                   select p).ToList();
        if (isplit != null)
        {
            foreach (var item in isplit)
            {
                con.Delete(item.id);
                con.Save(); 
            }
        }
       
        //INASN_D_ISTOCK_SPLITRule.DeleteExistSplit(txtID.Text.Trim());
        try
        {
            for (int i = 0; i < grdINASN_D.Rows.Count; i++)
            {
                var chk = grdINASN_D.Rows[i].FindControl("chkSelect") as CheckBox;
                if (chk.Checked)
                {
                    INASN_D_ISTOCK_SPLIT bo = new INASN_D_ISTOCK_SPLIT();
                    bo.id = Guid.NewGuid().ToString();
                    bo.ASN_ID = txtID.Text.Trim();
                    bo.ASN_D_IDS = grdINASN_D.DataKeys[i].Values["IDS"].ToString();
                    bo.CINVCODE = grdINASN_D.DataKeys[i].Values["CINVCODE"].ToString();
                    bo.FLAG = "0";
                    bo.OLDQTY = grdINASN_D.DataKeys[i].Values["IQUANTITY"].ToString().ToDecimal();
                    var txtCjQty = grdINASN_D.Rows[i].FindControl("txtCjQty") as TextBox;//拆解数量
                    bo.CJQTY = txtCjQty.Text.ToDecimal();
                    bo.CREATETIME = DateTime.Now;
                    bo.CREATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                    con.Insert(bo);
                    con.Save();
                   // INASN_D_ISTOCK_SPLITRule.Insert(bo);
                }
            }

            //SP拆解
            Proc_InAsn_DSplit proc = new Proc_InAsn_DSplit();
            proc.P_ASN_ID = txtID.Text.Trim();
            proc.P_UserNO = WmsWebUserInfo.GetCurrentUser().UserNo;
            proc.Execute();
            var errText = proc.P_Info;
            var  P_return_Value = proc.P_ReturnValue.ToString();

            if (P_return_Value == "1")
            {
                //拆解失败
                Alert(Resources.Lang.FrmiStockSplit_MSG6+ "：" + proc.P_Info);
                return;
            }
            else
            {
                Alert(proc.P_Info);
                //this.AlertAndBack("FrmINASNList.aspx" + BuildQueryString(SYSOperation.Modify, ""), proc.P_Info);
               // Response.Redirect("FrmINASNList.aspx");
                
            }
           
            btnSearch_Click(null, null);
        }
        catch (Exception ex)
        {
            //INASN_D_ISTOCK_SPLITRule.DeleteExistSplit(txtID.Text.Trim());
            //拆解异常
            Alert(Resources.Lang.FrmiStockSplit_MSG7 + "：" + ex.Message.ToString());
        }

    }

}