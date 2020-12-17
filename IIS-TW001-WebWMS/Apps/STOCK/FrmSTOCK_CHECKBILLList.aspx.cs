using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Data.Entity.SqlServer;
/// <summary>
/// 描述: 盘点单-->FrmSTOCK_CHECKBILLList 页面后台类文件
/// 作者:
/// 创建于: 2012-10-17 16:04:25
/// </summary>

public partial class STOCK_FrmSTOCK_CHECKBILLList : WMSBasePage
{
    #region SQL
    protected void Page_Load(object sender, EventArgs e)
    {
        this.grdSTOCK_CHECKBILL.Columns[1].Visible = false;
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.txtPlanName.Text = this.GetCheckName();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);

            if (InAsn.CheckPoint(WmsWebUserInfo.GetCurrentUser().UserNo, "6020"))
            {
                this.btnImportExcel.Enabled = true;
            }
            else
            {
                this.btnImportExcel.Enabled = false;
            }

        }
        this.btnNew0.Attributes["onclick"] = this.GetPostBackEventReference(this.btnNew0) + ";this.disabled=true;";
    }

    public string GetCheckName()
    {
        try
        {
            var modPlan = db.STOCK_CHECK_PLAN.Where(x => x.cstatus == "1").FirstOrDefault();
            if (modPlan != null)
            {
                return modPlan.plan_name;
            }
            else {
                return "";
            }
        }
        catch (Exception)
        {
            return "";
        }
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        Bind("dcreatetime");
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
            AspNetPager1.PageSize = this.PageSize;
        }

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("cstatus", "CHECKSTATE"));//状态

        var srcdata = GetGridSourceDataByList(data, flagList);

        grdSTOCK_CHECKBILL.DataSource = srcdata;
        grdSTOCK_CHECKBILL.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("dcreatetime");
    }

    public IQueryable<V_STOCK_CHECKBILLList> GetQueryList()
    {
        IGenericRepository<V_STOCK_CHECKBILLList> conn = new GenericRepository<V_STOCK_CHECKBILLList>(db);
        var caseList = from p in conn.Get()
                       orderby p.dcreatetime descending
                       where 1 == 1
                       select p;
        if (caseList != null && caseList.Count() > 0)
        {
            if (txtPlanName.Text.Trim() != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.planname) && x.planname.Contains(txtPlanName.Text.Trim()));
            }
            if (txtCCHECKNAME.Text.Trim().IsNullOrEmpty() == false)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccheckname) && x.ccheckname.Contains(txtCCHECKNAME.Text.Trim()));
            }
            if (txtCTICKETCODE.Text.Trim().IsNullOrEmpty() == false)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtCTICKETCODE.Text.Trim()));
            }
            if (txtCERPCODE.Text.Trim().IsNullOrEmpty() == false)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtCERPCODE.Text.Trim()));
            }
            if (dplCSTATUS.SelectedValue != string.Empty)
            {
                //if(dplCSTATUS.SelectedValue == "6")
                //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cstatus) &&((x.cdefine1.Equals(bool.TrueString) &&(x.statusf.Equals("1") || x.statusf.Equals("2") || x.statusf.Equals("3"))) || (x.cdefine1.Equals(bool.FalseString) &&x.statusf.Equals("1"))));
                //else
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cstatus) && x.cstatus.Equals(dplCSTATUS.SelectedValue));
            }
            DateTime dtime;
            if (txtDCHECKDATEFrom.Text != string.Empty && DateTime.TryParse(txtDCHECKDATEFrom.Text, out dtime))
            {
                caseList = caseList.Where(x => x.dcheckdate != null && SqlFunctions.DateDiff("dd", txtDCHECKDATEFrom.Text.Trim(), x.dcheckdate) >= 0);
            }
            if (txtDCHECKDATETo.Text != string.Empty && DateTime.TryParse(txtDCHECKDATETo.Text, out dtime))
            {
                caseList = caseList.Where(x => x.dcheckdate != null && SqlFunctions.DateDiff("dd", txtDCHECKDATETo.Text.Trim(), x.dcheckdate) <= 0);
            }

            if (txtDCREATETIMEFrom.Text != string.Empty && DateTime.TryParse(txtDCREATETIMEFrom.Text, out dtime))
            {
                caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMEFrom.Text.Trim(), x.dcreatetime) >= 0);
            }
            if (txtDCREATETIMETo.Text != string.Empty && DateTime.TryParse(txtDCREATETIMETo.Text, out dtime))
            {
                caseList = caseList.Where(x => x.dcreatetime != null && SqlFunctions.DateDiff("dd", txtDCREATETIMETo.Text.Trim(), x.dcreatetime) <= 0);
            }

            if (dplCHECKTYPE.SelectedValue != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.checktype) && x.checktype.Equals(dplCHECKTYPE.SelectedValue));
            }

            if (txtCCREATEOWNERCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.CCREATEOWNERCODE) && x.CCREATEOWNERCODE.Contains(txtCCREATEOWNERCODE.Text.Trim()));
            }
            if (txtDAUDITTIMEFrom.Text != string.Empty && DateTime.TryParse(txtDAUDITTIMEFrom.Text, out dtime))
            {
                caseList = caseList.Where(x => x.daudittime != null && SqlFunctions.DateDiff("dd", txtDAUDITTIMEFrom.Text.Trim(), x.daudittime) >= 0);
            }
            if (txtDAUDITTIMETo.Text != string.Empty && DateTime.TryParse(txtDAUDITTIMETo.Text, out dtime))
            {
                caseList = caseList.Where(x => x.daudittime != null && SqlFunctions.DateDiff("dd", txtDAUDITTIMETo.Text.Trim(), x.daudittime) <= 0);
            }
            if (txtCAUDITPERSON.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cauditperson) && x.cauditperson.Contains(txtCAUDITPERSON.Text.Trim()));
            }
        }
        return caseList;
    }

    public bool CheckData()
    {
        if (this.txtCTICKETCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
        }
        if (this.dplCSTATUS.Text.Trim().Length > 0)
        {
        }
        if (this.txtDCHECKDATEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDCHECKDATEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_PanDianDateError);//盘点日期项不是有效的日期！
                this.SetFocus(txtDCHECKDATEFrom);
                return false;
            }
        }
        if (this.txtDCHECKDATETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_NeedDao);//到项不允许空！
            this.SetFocus(txtDCHECKDATETo);
            return false;
        }
        if (this.txtDCHECKDATETo.Text.Trim().Length > 0)
        {
            if (this.txtDCHECKDATETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_DaoWuXiao);//到项不是有效的日期！
                this.SetFocus(txtDCHECKDATETo);
                return false;
            }
        }
        if (this.dplCHECKTYPE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_ZhiDanRiError);//制单日期项不是有效的日期！
                this.SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        if (this.txtDCREATETIMETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_NeedDao);//到项不允许空！
            this.SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (this.txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_DaoWuXiao);//到项不是有效的日期！
                this.SetFocus(txtDCREATETIMETo);
                return false;
            }
        }
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDAUDITTIMEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDAUDITTIMEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_AuditDateError);//审核日期项不是有效的日期！
                this.SetFocus(txtDAUDITTIMEFrom);
                return false;
            }
        }
        if (this.txtDAUDITTIMETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_NeedDao);//到项不允许空！
            this.SetFocus(txtDAUDITTIMETo);
            return false;
        }
        if (this.txtDAUDITTIMETo.Text.Trim().Length > 0)
        {
            if (this.txtDAUDITTIMETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_DaoWuXiao);//到项不是有效的日期！
                this.SetFocus(txtDAUDITTIMETo);
                return false;
            }
        }
        if (this.txtCAUDITPERSON.Text.Trim().Length > 0)
        {
        }

        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";
        this.grdSTOCK_CHECKBILL.DataKeyNames = new string[] { "ID" };

        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmSTOCK_CHECKBILLEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmSTOCK_CHECKBILLList_NewPanDian + "','STOCK_CHECKBILL');return false;";//新建盘点单

        this.btnImportExcel.Attributes["onclick"] = "PopupFloatWin('" + BuildRequestPageURL("/Apps/Import/FrmImportStock_CheckedBill.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmSTOCK_CHECKBILLList_UpLoadMingXi + "','Stock_CheckedBill',680,320); return false;";//上传盘点单明细

        Help.DropDownListDataBind(GetParametersByFlagType("CHECKSTATE"), dplCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态

        Help.DropDownListDataBind(GetParametersByFlagType("CHECKTYPE"), dplCHECKTYPE, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//盘点类型
        dplCHECKTYPE.SelectedValue = "0";
    }

    #endregion



    protected void grdSTOCK_CHECKBILL_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdSTOCK_CHECKBILL_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 删除按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int count = 0;
        int num = 0;
        try
        {
            IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);
            for (int i = 0; i < this.grdSTOCK_CHECKBILL.Rows.Count; i++)
            {
                if (this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        STOCK_CHECKBILL bo = new STOCK_CHECKBILL();
                        bo = (from p in conn.Get().AsEnumerable()
                              where p.id == this.grdSTOCK_CHECKBILL.DataKeys[i].Values[0].ToString()
                              select p
                             ).FirstOrDefault();

                        if (bo != null && !bo.id.IsNullOrEmpty() && !bo.cstatus.IsNullOrEmpty() && bo.cstatus.Equals("0"))
                        {
                            conn.Delete(bo.id);
                            conn.Save();
                            count++;
                        }
                        else
                        {
                            throw new Exception(Resources.Lang.FrmSTOCK_CHECKBILLList_UnDoDelete);//只有未处理的才可以删除
                        }
                    }
                }
            }
            if (count > 0)
            {
                this.Alert(Resources.Lang.WMS_Common_Msg_DeleteSuccess);//删除成功！
            }
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
        for (int i = 0; i < this.grdSTOCK_CHECKBILL.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdSTOCK_CHECKBILL.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdSTOCK_CHECKBILL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmSTOCK_CHECKBILLEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmSTOCK_CHECKBILLEdit_PanDainDan, "STOCK_CHECKBILL");//盘点单
        }

    }

    protected void dsGrdSTOCK_CHECKBILL_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdSTOCK_CHECKBILL_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
    }

    protected void btnNew0_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);
            IGenericRepository<STOCK_CHECKBILL_D> conn_d = new GenericRepository<STOCK_CHECKBILL_D>(db);
            string id = string.Empty;
            for (int i = 0; i < this.grdSTOCK_CHECKBILL.Rows.Count; i++)
            {
                if (this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1];
                    id = this.grdSTOCK_CHECKBILL.DataKeys[i].Values[0].ToString();

                    if (chkSelect.Checked)
                    {
                        var dList = from p in conn_d.Get()
                                    where p.id == id
                                    select p;
                        if (dList != null && dList.Count() > 0)
                        {

                            STOCK_CHECKBILL bo = new STOCK_CHECKBILL();
                            bo = (from p in conn.Get()
                                  where p.id == id
                                  select p
                                      ).FirstOrDefault();
                            if (bo != null && !bo.id.IsNullOrEmpty()
                                && !bo.cstatus.IsNullOrEmpty() && bo.cstatus.Equals("0"))
                            {

                                bo.cauditperson = WmsWebUserInfo.GetCurrentUser().UserNo;
                                bo.daudittime = DateTime.Now;
                                bo.cstatus = "1";
                                conn.Update(bo);
                                conn.Save();
                                count++;
                            }
                            else
                            {
                                throw new Exception(Resources.Lang.FrmSTOCK_CHECKBILLList_UnDoShenPi);//只有未处理的才可以审批
                            }
                        }
                        else
                        {
                            throw new Exception(Resources.Lang.FrmSTOCK_CHECKBILLList_NoMingXi);//没有明细不可审批
                        }
                    }
                }
            }
            this.GridBind();
            if (count > 0)
            {
                Alert(Resources.Lang.FrmSTOCK_CHECKBILLList_ShenPiSuccess);//审批成功
            }
            else
            {
                Alert(Resources.Lang.FrmSTOCK_CHECKBILLList_SelectShenPi);//请选择要审批的项
            }
        }
        catch (Exception err)
        {
            Alert(err.Message.ToJsString());
        }
        finally
        {
            btnNew0.Style.Remove("disabled");
        }
    }
    protected void btnNew1_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string id = string.Empty;
            IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);
            for (int i = 0; i < this.grdSTOCK_CHECKBILL.Rows.Count; i++)
            {
                if (this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        id = this.grdSTOCK_CHECKBILL.DataKeys[i].Values[0].ToString();
                        STOCK_CHECKBILL bo = new STOCK_CHECKBILL();
                        bo = (from p in conn.Get()
                              where p.id == id
                              select p
                                    ).FirstOrDefault();

                        if (bo != null && !bo.id.IsNullOrEmpty() && !bo.cstatus.IsNullOrEmpty())
                        {
                            if (bo.cstatus == "1")
                            {
                                //锁定关联储位
                                DataTable dt_list = Stock_CheckBill.GetCheckBill_List(bo.id);
                                foreach (DataRow row in dt_list.Rows)
                                {
                                    if (!(row[0] is DBNull))
                                    {
                                        Stock_CheckBill.UpdateCpoStatus(row[0].ToString(), bo.id);
                                    }
                                }
                                bo.cstatus = "2";
                                bo.statusf = "1";
                                conn.Update(bo);
                                conn.Save();
                                count++;
                            }
                            else if (bo.cdefine1 == true.ToString() && bo.cstatus == "4")
                            {
                                //锁定关联储位
                                DataTable dt_list = Stock_CheckBill.GetCheckBill_List(bo.id);
                                foreach (DataRow row in dt_list.Rows)
                                {
                                    if (!(row[0] is DBNull))
                                    {
                                        Stock_CheckBill.UpdateCpoStatus(row[0].ToString(), bo.id);
                                    }
                                }
                                bo.statusf = "3";
                                bo.cstatus = "3";
                                conn.Update(bo);
                                conn.Save();
                                count++;
                            }
                            else if (bo.cstatus == "0")
                            {
                                throw new Exception(bo.cticketcode + Resources.Lang.FrmSTOCK_CHECKBILLList_BuNengKaiShi);//未处理状态不可开始
                            }
                            else
                            {
                                //还原关联储位状态
                                DataTable dt_list = Stock_CheckBill.GetCheckBill_List(bo.id);
                                foreach (DataRow row in dt_list.Rows)
                                {
                                    if (!(row[0] is DBNull))
                                    {
                                        Stock_CheckBill.UpdateCpoStatus(row[0].ToString());
                                    }
                                }
                                bo.cstatus = "5";
                                conn.Update(bo);
                                conn.Save();
                                count++;
                            }
                        }
                    }
                }
            }
            this.GridBind();
            if (count > 0)
            {
                Alert(Resources.Lang.FrmSTOCK_CHECKBILLList_DanJuKaiShi);//单据开始！
            }
            else
            {
                Alert(Resources.Lang.FrmSTOCK_CHECKBILLList_XuanzeKaishi);//请选择开始的项
            }
        }
        catch (Exception err)
        {
            Alert(err.Message.ToJsString());
        }
    }

    /// 结束按钮
    /// <summary>
    /// 结束按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNew2_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string id = string.Empty;
            IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);

            for (int i = 0; i < this.grdSTOCK_CHECKBILL.Rows.Count; i++)
            {
                if (this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        id = this.grdSTOCK_CHECKBILL.DataKeys[i].Values[0].ToString();
                        STOCK_CHECKBILL bo = new STOCK_CHECKBILL();
                        bo = (from p in conn.Get()
                              where p.id == id
                              select p
                                    ).FirstOrDefault();
                        if (bo != null && !bo.id.IsNullOrEmpty()
                            && !bo.cstatus.IsNullOrEmpty())
                        {
                            if (bo.cdefine1 == false.ToString() && bo.cstatus == "2")//&& bo.statusf =="1" )
                            {
                                //还原关联储位状态
                                DataTable dt_list = Stock_CheckBill.GetCheckBill_List(bo.id);
                                foreach (DataRow row in dt_list.Rows)
                                {
                                    if (!(row[0] is DBNull))
                                    {
                                        Stock_CheckBill.UpdateCpoStatus(row[0].ToString());
                                    }
                                }
                                bo.cstatus = "5";
                                conn.Update(bo);
                                conn.Save();
                                count++;
                            }
                            else if (bo.cstatus == "2")//(bo.statusf == "1")
                            {
                                //bo.statusf = "2";
                                bo.cstatus = "4";
                                conn.Update(bo);
                                conn.Save();
                                count++;
                            }
                            else if (bo.cstatus == "3" || bo.cstatus == "4")//(bo.statusf == "3")
                            {
                                //还原关联储位状态
                                DataTable dt_list = Stock_CheckBill.GetCheckBill_List(bo.id);
                                foreach (DataRow row in dt_list.Rows)
                                {
                                    if (!(row[0] is DBNull))
                                    {
                                        Stock_CheckBill.UpdateCpoStatus(row[0].ToString());
                                    }
                                }
                                bo.cstatus = "5";
                                conn.Update(bo);
                                conn.Save();
                                count++;
                            }
                            else
                            {
                                throw new Exception(bo.cticketcode + "，" + Resources.Lang.FrmSTOCK_CHECKBILLList_BuNengJieShu);//状态为：未处理、已审核、盘点完成不可结束！
                            }
                        }
                    }
                }
            }
            this.GridBind();
            if (count > 0)
            {
                Alert(Resources.Lang.FrmSTOCK_CHECKBILLList_PanDianJieShu);//盘点已结束！
            }
            else
            {
                Alert(Resources.Lang.FrmSTOCK_CHECKBILLList_SelectJieShu);//请选择要结束的项！
            }

        }
        catch (Exception err)
        {
            Alert(err.Message.ToJsString());
        }
    }
    #endregion

    protected void btnSearch_Roblck_Click(object sender, EventArgs e)
    {
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        this.AspNetPager1_PageChanged(null, null);
    }
}

