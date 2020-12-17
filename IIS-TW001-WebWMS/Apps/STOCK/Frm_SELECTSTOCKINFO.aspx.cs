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


/// <summary>
/// 描述: 储位管理-->FrmBASE_CARGOSPACEList 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-10-09 18:29:23
/// </summary>
public partial class Frm_SELECTSTOCKINFO : WMSBasePage
{
    public List<STOCK_CHECKBILL_D> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as List<STOCK_CHECKBILL_D>; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
        }
        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
    }


    #region IPageGrid 成员

    public void GridBind()
    {
        Bind("");
    }


    public IQueryable<v_stock_checkbill_d> GetQueryList()
    {
        IGenericRepository<v_stock_checkbill_d> conn = new GenericRepository<v_stock_checkbill_d>(db);
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
            //Note by Qamar 2020-11-22
            if (txtCPOSITION.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cposition) && x.cposition.Contains(txtCPOSITION.Text.Trim()));
            }
            if (txtCPOSITIONCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCPOSITIONCODE.Text.Trim()));
            }
            if (txtCINVCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCINVCODE.Text.Trim()));
            }
            //Note by Qamar 2020-10-22
            if (txtRANK_FINAL.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && (x.cinvcode.Substring(x.cinvcode.Length-1,1)==(txtRANK_FINAL.Text.Trim().ToUpper())));
            }
            if (!string.IsNullOrEmpty(Request["WorkType"]))
            {
                string worktype = Request["WorkType"].ToString();
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.worktype) && x.worktype.Equals(worktype));
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
                caseList = caseList.OrderBy(sortStr);
            }
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = grdBASE_CARGOSPACE.PageSize;      

            var data = GetPageSize(caseList, grdBASE_CARGOSPACE.PageSize, CurrendIndex).ToList();
            var source = from p in data
                         join oper in db.BASE_PART on p.cinvcode equals oper.cpartnumber into temp
                         from tt in temp.DefaultIfEmpty()
                         select new
                         {
                            p.id
                            ,p.cstatus
                            ,p.cwarehouse
                            ,p.cwarehousecode
                            ,p.cpositioncode
                            ,p.cposition
                            ,p.cinvcode
                            ,p.cinvname
                            ,p.iqty
                            ,p.cunits
                            ,p.cdatecode
                            ,p.cbarcode
                            ,p.ioccupyqty
                            ,p.cmemo
                            ,p.cdefine1
                            ,p.cdefine2
                            ,p.ddefine3
                            ,p.ddefine4
                            ,p.idefine5
                            ,p.iplanin
                            ,p.iplanout
                            ,p.palletcode
                            ,p.worktype
                            ,p.PackgeNo
                            ,p.DiffDays
                            ,cspecifications = tt == null ? "" : tt.cspecifications
                         };
            grdBASE_CARGOSPACE.DataSource = source.ToList();

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
        Bind("");
    }


    public bool CheckData()
    {
        return true;
    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('Frm_SELECTSTOCKINFO_D');return false;";
        this.grdBASE_CARGOSPACE.DataKeyNames = new string[] { "ID" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        //this.btnNew.Attributes["onclick"]= "PopupFloatWin('" +  PageBase.BuildRequestPageURL("FrmBASE_CARGOSPACEEdit.aspx", SysOperation.New,"") + "','新建储位管理','BASE_CARGOSPACE',600,370);return false;";
        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmBASE_CARGOSPACEEdit.aspx", SysOperation.New,""),800,600);
        //string strID = Request.QueryString["IDH"];
        //获取所有的明细ID

    }

    #endregion

    protected DataTable grdNavigatorBASE_CARGOSPACE_GetExportToExcelSource()
    {
        return null;
    }

    protected void grdBASE_CARGOSPACE_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GetSelectedIds();
    }
    protected void grdBASE_CARGOSPACE_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IGenericRepository<STOCK_CHECKBILL_D> conn = new GenericRepository<STOCK_CHECKBILL_D>(db);
        this.SelectIds = (from p in conn.Get().AsEnumerable()
                          where p.id == Request.QueryString["IDH"].ToString()
                          select p).ToList();
        this.CurrendIndex = 1;
        this.GridBind();
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IGenericRepository<BASE_CARGOSPACE> conn = new GenericRepository<BASE_CARGOSPACE>(db);

            for (int i = 0; i < this.grdBASE_CARGOSPACE.Rows.Count; i++)
            {
                if (this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {

                        BASE_CARGOSPACE entity = (from p in conn.Get()
                                                  where p.id == this.grdBASE_CARGOSPACE.DataKeys[i].Values[0].ToString()
                                                  select p).FirstOrDefault();
                        if (entity != null && !entity.id.IsNullOrEmpty())
                        {
                            conn.Delete(entity);
                            conn.Save();
                        }

                    }
                }
            }
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteSuccess);//删除成功！
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + E.Message.ToJsString());//删除失败！
        }
    }



    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdBASE_CARGOSPACE.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBASE_CARGOSPACE.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdBASE_CARGOSPACE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region //21-10-2020 by Qamar
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var partNumber = e.Row.Cells[6].Text;
            e.Row.Cells[6].Text = partNumber.Substring(0, partNumber.Length - 2);
            var rankfinal = partNumber.Substring(partNumber.Length - 1, 1);
            if (rankfinal == "_")
                e.Row.Cells[7].Text = "";
            else
                e.Row.Cells[7].Text = rankfinal;
        }
        #endregion
    }

    protected void dsGrdBASE_CARGOSPACE_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdBASE_CARGOSPACE_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    /// <summary>
    /// 获取选择的ID
    /// </summary>
    public void GetSelectedIds()
    {
        foreach (GridViewRow item in this.grdBASE_CARGOSPACE.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                int i = item.RowIndex;
                if (cbo.Checked &&
                    !SelectIds.Any(m => m.cinvcode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[6].Text)
                        && m.cpositioncode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[5].Text)))
                {
                    if (!string.IsNullOrEmpty(Request["WorkType"]))
                    {
                        STOCK_CHECKBILL_D entity = new STOCK_CHECKBILL_D();
                        entity.ids = Guid.NewGuid().ToString();
                        entity.id = Request.QueryString["IDH"].ToString();
                        entity.cstatus = "0";
                        entity.asrs_status = 0;
                        entity.iquantity = this.grdBASE_CARGOSPACE.Rows[i].Cells[10].Text.ToDecimal(); //數量
                        entity.cpositioncode = this.grdBASE_CARGOSPACE.Rows[i].Cells[5].Text;
                        entity.cposition = Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[i].Cells[4].Text);
                        
                        //以下 29-10-2020 by Qamar
                        if (this.grdBASE_CARGOSPACE.Rows[i].Cells[7].Text.Trim().Length == 1)
                            entity.cinvcode = this.grdBASE_CARGOSPACE.Rows[i].Cells[6].Text + "-" + this.grdBASE_CARGOSPACE.Rows[i].Cells[7].Text.Trim().ToUpper();
                        else
                            entity.cinvcode = this.grdBASE_CARGOSPACE.Rows[i].Cells[6].Text + "-_";
                        //以上 29-10-2020 by Qamar

                        entity.cinvname = Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[i].Cells[8].Text);
                        if (Request["WorkType"].ToString().Equals("1")) //立库
                        {
                            entity.palletcode = Comm_Function.GetPalletCodeByPositionCode(entity.cpositioncode);
                        }
                        else
                        {
                            entity.palletcode = "";
                        }
                        SelectIds.Add(entity);
                    }
                }
                else if (!cbo.Checked &&
                    SelectIds.Any(m => m.cinvcode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[6].Text)
                        && m.cpositioncode == Server.HtmlDecode(this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[5].Text)))
                {
                    SelectIds.Remove(SelectIds.Single(m => m.cinvcode == this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[6].Text
                        && m.cpositioncode == this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[5].Text));
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

    //ok
    protected void okbutton_Click(object sender, EventArgs e)
    {
        SaveToDataBase();
        msgBox.Hide();
    }

    //cancel
    protected void cancelbutton_Click(object sender, EventArgs e)
    {
        msgBox.Hide();
    }

    //保存
    private void SaveToDataBase()
    {
        try
        {
            string strID = Request.QueryString["IDH"];
            string worktype = "";
            if (!string.IsNullOrEmpty(Request["WorkType"]))
            {
                worktype = Request["WorkType"].ToString();
            }

            GetSelectedIds();

            int processCount = 0;
            BASE_CARGOSPACE_Query bcQry = new BASE_CARGOSPACE_Query();
            IGenericRepository<STOCK_CHECKBILL_D> conn = new GenericRepository<STOCK_CHECKBILL_D>(db);
            if (SelectIds != null && SelectIds.Count > 0)
            {
                foreach (STOCK_CHECKBILL_D entity in this.SelectIds)
                {
                    // 29-10-2020 by Qamar
                    string t1 = entity.cinvcode;
                    string t2 = entity.cpositioncode;
                    string t3 = entity.id;
                    if (!bcQry.HasBill(entity.cinvcode, entity.cpositioncode, entity.id))
                    {
                        if (worktype == "1") //立库
                        {
                            string msg = bcQry.isPositionUsedByOthers(entity.cpositioncode, entity.id, "");
                            if (msg.Length > 0)
                            {
                                Alert(msg);
                                return;
                            }
                        }
                        processCount++;
                        conn.Insert(entity);
                        conn.Save();
                    }
                }
            }
            else
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_NeedAddShuJu);//请添加数据！
            }
            if (processCount > 0)
            {
                this.Alert(Resources.Lang.WMS_Common_Msg_AddSuccess);//添加成功！
                this.GridBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    protected void grdBASE_CARGOSPACE_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow item in this.grdBASE_CARGOSPACE.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //获取ID
                string id = this.grdBASE_CARGOSPACE.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;
                int i = item.RowIndex;

                if (SelectIds.Any(m => m.cinvcode == this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[6].Text
                    && m.cpositioncode == this.grdBASE_CARGOSPACE.Rows[item.RowIndex].Cells[5].Text))
                {
                    //如果是控件处于选中状态
                    cbo.Checked = true;
                    cbo.Enabled = false;
                }
                else
                    cbo.Checked = false;
            }
        }
    }
    protected void btnSaveALL_Click(object sender, EventArgs e)
    {
        string strID = Request.QueryString["IDH"];
        string strSql = "";
        string strWhere = string.Empty;

        try
        {
            strSql = string.Format("delete from stock_checkbill_d  where id='" + strID + "'");

            db.Database.ExecuteSqlCommand(strSql);

            string sql = @"insert into STOCK_CHECKBILL_D(IDS,ID,CSTATUS,IQUANTITY,CPOSITIONCODE,CPOSITION,CINVCODE,CINVNAME)
                           select sys_guid(),'" + strID + @"',0,t.iqty,t.cpositioncode,t.cposition,t.cinvcode,t.cinvname 
                           from stock_current t 
                           where 1=1 ";

            if (txtCINVCODE.Text.Trim().Length > 0)
            {
                strWhere += " and t.CINVCODE like '%" + txtCINVCODE.Text.Trim() + "%'";
            }
            if (txtCPOSITIONCODE.Text.Trim().Length > 0)
            {
                strWhere += " and t.CPOSITIONCODE like '%" + txtCPOSITIONCODE.Text.Trim() + "%'";
            }
            if (txtCWAREHOUSE.Text.Trim().Length > 0)
            {
                strWhere += @"and t.cpositioncode in
                                    (select bcs.cpositioncode
                                        from base_cargospace bcs
                                        where bcs.warehouseid =
                                            (select bwh.id from base_warehouse bwh where bwh.cwareid like '%" + txtCWAREHOUSE.Text.Trim() + "%'))";
            }
            strWhere += "";
            sql += strWhere;
            db.Database.ExecuteSqlCommand(sql);
            this.WriteScript("window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('Frm_SELECTSTOCKINFO_D');");
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }
}

