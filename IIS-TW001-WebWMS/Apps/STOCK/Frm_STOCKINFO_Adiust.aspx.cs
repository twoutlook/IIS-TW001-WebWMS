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
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using DreamTek.ASRS.Business.Base;
using DreamTek.ASRS.Business.Others;
using DreamTek.ASRS.Business.Stock;

public partial class Apps_STOCK_Frm_STOCKINFO_Adiust : WMSBasePage
{

    public List<STOCK_CURRENT_ADJUST_D> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as List<STOCK_CURRENT_ADJUST_D>; }
    }
    public bool IsCheckOK
    {
        set { ViewState["IsCheckOK"] = value; }
        get
        {
            if (ViewState["IsCheckOK"] != null)
                return bool.Parse(ViewState["IsCheckOK"].ToString());
            else return true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
        }
        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
    }
    #region


    public IQueryable<v_stock_current_adjust_d> GetQueryList()
    {
        IGenericRepository<v_stock_current_adjust_d> conn = new GenericRepository<v_stock_current_adjust_d>(db);
        var caseList = from p in conn.Get()
                       orderby p.cinvcode descending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCWAREHOUSE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwarehousecode) && x.cwarehousecode.Contains(txtCWAREHOUSE.Text.Trim()));
            }
            if (txtCPOSITIONCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCPOSITIONCODE.Text.Trim()));
            }
            if (txtCINVCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCINVCODE.Text.Trim()));
            }
            #region //22-10-2020 by Qamar
            if (txtRANK_FINAL.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && (x.cinvcode.Substring(x.cinvcode.Length - 1, 1) == (txtRANK_FINAL.Text.Trim().ToUpper())));
            }
            #endregion
            if (!string.IsNullOrEmpty(txtcspec.Text.Trim()))
            {
                caseList = caseList.Where(x => x.cspecifications.ToString().Contains(txtcspec.Text.Trim()));
            }
        }
        return caseList;
    }

    public void GridBind(string sortStr)
    {
        var caseList = GetQueryList();

        if (caseList != null && caseList.Count() > 0)
        {
            //按照列名排序
            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = grdBASE_CARGOSPACE.PageSize;
            grdBASE_CARGOSPACE.DataSource = GetPageSize(caseList, grdBASE_CARGOSPACE.PageSize, CurrendIndex).ToList();
            grdBASE_CARGOSPACE.DataBind();
        }
        else
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = grdBASE_CARGOSPACE.PageSize;
            grdBASE_CARGOSPACE.DataSource = null;
            grdBASE_CARGOSPACE.DataBind();
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind("");
    }


    public bool CheckData()
    {
        return true;
    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('STOCK_ADJUST_D');return false;";
        this.grdBASE_CARGOSPACE.DataKeyNames = new string[] { "ID" };
    }

    #endregion

    protected void grdBASE_CARGOSPACE_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GetSelectedIds();
    }
    protected void grdBASE_CARGOSPACE_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind("");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IGenericRepository<STOCK_CURRENT_ADJUST_D> conn = new GenericRepository<STOCK_CURRENT_ADJUST_D>(db);
        this.SelectIds = (from p in conn.Get().AsEnumerable()
                          where p.id == Request.QueryString["ID"].ToString()
                          select p).ToList();

        this.CurrendIndex = 1;
        this.GridBind("");
    }



    /// <summary>
    /// 获取选择的ID
    /// </summary>
    public void GetSelectedIds()
    {
        string errmsg = string.Empty;
        IsCheckOK = true;
        foreach (GridViewRow item in this.grdBASE_CARGOSPACE.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            TextBox txtNewQty = item.FindControl("txtNewQty") as TextBox;
            var x = txtNewQty.Text.ToString();
            TextBox txtSN = item.FindControl("txtSN") as TextBox;
            decimal newQty = 0;
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;
                int i = item.RowIndex;
                if (cbo.Checked &&
                    !SelectIds.Any(m => m.cinvcode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[6].Text)
                        && m.cpositioncode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[5].Text)
                        && m.sncode == txtSN.Text.Trim()))
                {
                    if (txtNewQty.Text.Trim().Length == 0)
                    {
                        IsCheckOK = false;
                        Alert(Resources.Lang.Frm_STOCKINFO_Adiust_NeedNewQuantity);//请填写调整数量！
                        return;
                    }


                    // NOTE by Mark, 11/02, 原來的代碼驗証用戶輸入的值是否為 Decimail, 看似有問題
                    //if (!(DreamTek.ASRS.Business.Others.Comm_Function.Fun_IsDecimal(txtNewQty.Text.Trim(), 0, 1, 1, out errmsg)))
                    //{
                    //    IsCheckOK = false;
                    //    this.Alert(errmsg);
                    //    this.SetFocus(txtNewQty);
                    //    return;
                    //}


                    decimal result;
                    if (decimal.TryParse(x, out result))
                    {
                        // the value was decimal
                        Console.WriteLine(result);
                    }
                    else
                    {
                        this.Alert("請輸入數字格式!");
                        this.SetFocus(txtNewQty);
                        return;
                    }


                    STOCK_CURRENT_ADJUST_D entity = new STOCK_CURRENT_ADJUST_D();
                    entity.ids = Guid.NewGuid().ToString();
                    entity.id = Request.QueryString["ID"].ToString();
                    entity.sncode = txtSN.Text.Trim();

                    //NOTE by Mark, 11/02, 因為 原料號變成 兩個欄位 PART RANK, 需要做位移
                    //entity.oriqty = this.grdBASE_CARGOSPACE.Rows[i].Cells[10].Text.ToDecimal();
                    entity.oriqty = this.grdBASE_CARGOSPACE.Rows[i].Cells[11].Text.ToDecimal();

                    //bool a = decimal.TryParse(txtNewQty.Text.Trim(), out newQty);
                    //entity.newqty = newQty;
                    entity.newqty = result;




                    entity.cpositioncode = this.grdBASE_CARGOSPACE.Rows[i].Cells[5].Text;
                    entity.cpositionname = Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[i].Cells[4].Text);
                    entity.cinvcode = this.grdBASE_CARGOSPACE.Rows[i].Cells[6].Text;
                    entity.cinvname = Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[i].Cells[7].Text);
                    SelectIds.Add(entity);

                }
                else if (cbo.Checked &&
                    SelectIds.Any(m => m.cinvcode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[6].Text)
                        && m.cpositioncode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[5].Text)
                        && m.sncode == txtSN.Text.Trim()))
                {
                    if (txtNewQty.Text.Trim().Length == 0)
                    {
                        IsCheckOK = false;
                        Alert(Resources.Lang.Frm_STOCKINFO_Adiust_NeedNewQuantity);//请填写调整数量！
                        return;
                    }
                    if (!(DreamTek.ASRS.Business.Others.Comm_Function.Fun_IsDecimal(txtNewQty.Text.Trim(), 0, 1, 1, out errmsg)))
                    {
                        IsCheckOK = false;
                        this.Alert(errmsg);
                        this.SetFocus(txtNewQty);
                        return;
                    }
                    STOCK_CURRENT_ADJUST_D entity = SelectIds.Find(m => m.cinvcode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[6].Text)
                                                                  && m.cpositioncode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[5].Text)
                                                                  && m.sncode == txtSN.Text.Trim());
                    bool a = decimal.TryParse(txtNewQty.Text.Trim(), out newQty);
                    if (newQty != entity.newqty || this.grdBASE_CARGOSPACE.Rows[i].Cells[10].Text.ToDecimal() != entity.oriqty)
                    {
                        entity.id = Request.QueryString["ID"].ToString();
                        entity.sncode = txtSN.Text.Trim();
                        entity.oriqty = this.grdBASE_CARGOSPACE.Rows[i].Cells[10].Text.ToDecimal();
                        entity.newqty = newQty;
                        entity.cpositioncode = this.grdBASE_CARGOSPACE.Rows[i].Cells[5].Text;
                        entity.cpositionname = Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[i].Cells[4].Text);
                        entity.cinvcode = this.grdBASE_CARGOSPACE.Rows[i].Cells[6].Text;
                        entity.cinvname = Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[i].Cells[7].Text);
                        entity.id = entity.id;
                        entity.ids = entity.ids;
                        SelectIds.Remove(SelectIds.Single(m => m.cinvcode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[6].Text)
                            && m.cpositioncode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[5].Text)
                            && m.sncode == txtSN.Text.Trim()));
                        SelectIds.Add(entity);
                    }

                }
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = false;
        try
        {
            SaveToDataBase();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_AddFailed + E.Message.ToJsString());//添加失败！
        }
        btnSave.Enabled = true;
    }

    //保存
    private void SaveToDataBase()
    {
        try
        {
            string strID = Request.QueryString["ID"];
            GetSelectedIds();
            int processCount = 0;
            StockQuery bcQry = new StockQuery();
            BASE_CARGOSPACE_Query bcQry1 = new BASE_CARGOSPACE_Query();
            IGenericRepository<STOCK_CURRENT_ADJUST_D> conn = new GenericRepository<STOCK_CURRENT_ADJUST_D>(db);
            if (SelectIds != null && SelectIds.Count > 0)
            {
                foreach (STOCK_CURRENT_ADJUST_D entity in this.SelectIds)
                {
                    //1. 当前调整单中是否存在该储位与料号
                    if (!bcQry.HasStockAdjust(entity.cinvcode, entity.cpositioncode, entity.id, entity.sncode)) //不存在
                    {
                        //验证当前储位有没有别的单据使用中----20200325
                        string msg = bcQry1.isPositionUsedByOthers(entity.cpositioncode, "", entity.id);
                        if (msg.Length > 0)
                        {
                            Alert(msg);
                            return;
                        }
                        // //验证当前储位有没有别的单据使用中----20200325

                        processCount++;
                        entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                        entity.createtime = DateTime.Now;
                        conn.Insert(entity);
                        conn.Save();
                    }
                    else  //更新数据
                    {
                        processCount++;
                        entity.lastupdateuser = WmsWebUserInfo.GetCurrentUser().UserNo;
                        entity.lastupdatetime = DateTime.Now;
                        conn.Update(entity);
                        conn.Save();
                    }
                }
            }
            else if ((SelectIds == null || SelectIds.Count == 0) && IsCheckOK)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_NeedAddShuJu);//请添加数据！
            }
            if (processCount > 0 && processCount == SelectIds.Count && IsCheckOK)
            {
                this.Alert(Resources.Lang.WMS_Common_Msg_AddSuccess);//添加成功！
                this.GridBind("");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    protected void grdBASE_CARGOSPACE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // NOTE by Mark, 11/03, 這裡解釋了之前遇到 "奇怪" 的現象
        #region //21-10-2020 by Qamar
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    var partNumber = e.Row.Cells[6].Text;
        //    e.Row.Cells[6].Text = partNumber.Substring(0, partNumber.Length - 2);
        //    var rankfinal = partNumber.Substring(partNumber.Length - 1, 1);
        //    if (rankfinal == "_")
        //        e.Row.Cells[7].Text = "";
        //    else
        //        e.Row.Cells[7].Text = rankfinal;
        //}
        #endregion
    }

    protected void grdBASE_CARGOSPACE_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow item in this.grdBASE_CARGOSPACE.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            TextBox txtNewQty = item.FindControl("txtNewQty") as TextBox;
            TextBox txtSN = item.FindControl("txtSN") as TextBox;
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;
                //获取ID
                string id = this.grdBASE_CARGOSPACE.DataKeys[item.RowIndex][0].ToString();
                int i = item.RowIndex;
                if (SelectIds.Any(m => m.cinvcode == this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[6].Text
                    && m.cpositioncode == this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[5].Text
                    && m.sncode == txtSN.Text.Trim()))
                {
                    //如果是控件处于选中状态
                    cbo.Checked = true;
                    cbo.Enabled = false;
                    STOCK_CURRENT_ADJUST_D entity = SelectIds.Find(m => m.cinvcode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[6].Text)
                                                                    && m.cpositioncode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[5].Text)
                                                                   && m.sncode == txtSN.Text.Trim());
                    if (entity.newqty != null)
                        txtNewQty.Text = entity.newqty.ToString("f2");
                    else txtNewQty.Text = "";
                }
                else
                {
                    cbo.Checked = false;
                    txtNewQty.Text = "";
                }
            }
        }
    }
}

