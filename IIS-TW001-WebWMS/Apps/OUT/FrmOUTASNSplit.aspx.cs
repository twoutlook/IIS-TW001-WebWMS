using DreamTek.ASRS.Business.SP.ProcedureModel;
using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_OUT_FrmOUTASNSplit : WMSBasePage
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
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        this.GridBind();
    }

    /// <summary>
    /// 分页控件变更页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    #region IPageGrid 成員

    public void GridBind()
    {
        using (var modContext = context)
        {
            var queryList = from p in modContext.V_OUTASN_SPLIT
                            where p.OUTASNID == txtID.Text
                            select p;
            queryList = queryList.OrderBy(x => x.CINVCODE);

            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;

            this.grdOUTASN_D.DataSource = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            this.grdOUTASN_D.DataBind();
        }
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('FrmOUTASNSplit');return false;";
    }

    #endregion

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

        foreach (GridViewRow item in this.grdOUTASN_D.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //獲取ID
                string ids = this.grdOUTASN_D.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

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

 
    protected void grdOUTASN_D_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }

    private string GetKeyIDS(int rowIndex)
    {
        return this.grdOUTASN_D.DataKeys[rowIndex].Values[0].ToString();
    }

    private Dictionary<string, string> dict = new Dictionary<string, string>();

    protected void grdOUTASN_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var txtLessQty = e.Row.FindControl("txtLessQty") as TextBox;
            txtLessQty.Enabled = false;
        }
    }

    protected void dsGrdOUTASN_D_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //判斷選中幾條記錄
        var CheckCount = (from GridViewRow item in this.grdOUTASN_D.Rows
                          let itemFindControl = item.FindControl("chkSelect")
                          where itemFindControl != null && itemFindControl is CheckBox
                          let cbo = itemFindControl as CheckBox
                          where cbo.Checked
                          select item).Count();
        if (CheckCount == 0)
        {
            Alert(Resources.Lang.FrmOUTASNSplit_SelectCinvcode);//请选择需要拆解的料号!
            return;
        }

        for (int i = 0; i < grdOUTASN_D.Rows.Count; i++)
        {
            var chk = grdOUTASN_D.Rows[i].FindControl("chkSelect") as CheckBox;
            float qty = 0;
            float lessQty = 0;

            if (chk.Checked)
            {
                var cinQty = grdOUTASN_D.DataKeys[i].Values["IQUANTITY"].ToString();//物料数量
                var txtLessQty = grdOUTASN_D.Rows[i].FindControl("txtLessQty") as TextBox;//可拆解数量
                var txtCjQty = grdOUTASN_D.Rows[i].FindControl("txtCjQty") as TextBox;//拆解数量
                int cjQty = 0;

                if (int.TryParse(txtCjQty.Text,out cjQty))
                {
                    if (cjQty <= 0)
                    {
                        Alert(Resources.Lang.WMS_Common_Element_Di + (i + 1) + Resources.Lang.WMS_Common_Element_Hang + "，" + Resources.Lang.FrmOUTASNSplit_SplitQuantityDayuLing);// Alert("第" + (i + 1) + "行，拆解數量需要大于0！");
                        return;
                    }
                    else
                    {
                        float.TryParse(cinQty, out qty);
                        float.TryParse(txtLessQty.Text, out lessQty);
                        if (cjQty > lessQty)
                        {
                            Alert(Resources.Lang.WMS_Common_Element_Di + (i + 1) + Resources.Lang.WMS_Common_Element_Hang + "，" + Resources.Lang.FrmOUTASNSplit_ChaiJieShu + cjQty + Resources.Lang.FrmOUTASNSplit_BuNengDaYu + lessQty + "！");//Alert("第" + (i + 1) + "行，拆解數量" + cjQty + "不能大于可拆解數量" + lessQty + "！");
                            return;
                        }
                    }
                }
                else
                {
                    Alert(Resources.Lang.WMS_Common_Element_Di + (i + 1) + Resources.Lang.WMS_Common_Element_Hang + "，" + Resources.Lang.FrmOUTASNSplit_BuShiShuZi);//Alert("第" + (i + 1) + "行，拆解數量不是数字！");
                    return;
                }
            }
        }
        using (var modContext = context)
        {
            modContext.Database.ExecuteSqlCommand(" delete from OUTASN_D_SPLIT  where outasn_id = @OutasnId and flag='0' ", new SqlParameter("@OutasnId ", txtID.Text.Trim()));
            modContext.SaveChanges();

            try
            {
                for (int i = 0; i < grdOUTASN_D.Rows.Count; i++)
                {
                    var chk = grdOUTASN_D.Rows[i].FindControl("chkSelect") as CheckBox;
                    if (chk.Checked)
                    {
                        OUTASN_D_SPLIT bo = new OUTASN_D_SPLIT();
                        bo.Id = Guid.NewGuid().ToString();
                        bo.Outasn_Id = txtID.Text.Trim();
                        bo.Outasn_D_Ids = grdOUTASN_D.DataKeys[i].Values["IDS"].ToString();
                        bo.CinvCode = grdOUTASN_D.DataKeys[i].Values["CINVCODE"].ToString();
                        bo.Flag = "0";
                        bo.OldQty = grdOUTASN_D.DataKeys[i].Values["IQUANTITY"].ToString().ToDecimal();
                        var txtCjQty = grdOUTASN_D.Rows[i].FindControl("txtCjQty") as TextBox;//拆解数量
                        bo.CjQty = txtCjQty.Text.ToDecimal();
                        bo.CreateTime = DateTime.Now;
                        bo.CreateUser = WmsWebUserInfo.GetCurrentUser().UserNo;
                        modContext.OUTASN_D_SPLIT.Add(bo);
                        modContext.SaveChanges();
                    }
                }

                //SP拆解
                Proc_OutAsn_DSplit sp = new Proc_OutAsn_DSplit();
                sp.P_OUTASN_ID = txtID.Text.Trim();
                sp.P_UserNO = WmsWebUserInfo.GetCurrentUser().UserNo;
                sp.Execute();
                if (sp.ReturnValue == 0)
                {
                    Alert(sp.ErrorMessage);
                    btnSearch_Click(null, null);
                }
                else {
                    Alert(sp.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Alert(Resources.Lang.FrmOUTASNSplit_SplitException + "：" + ex.Message.ToString());//拆解异常
                modContext.Database.ExecuteSqlCommand(" delete from OUTASN_D_SPLIT  where outasn_id = @OutasnId and flag='0' ", new SqlParameter("@OutasnId ", txtID.Text.Trim()));
                modContext.SaveChanges();
            }

        }
    }
}