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
using DreamTek.ASRS.Business.SP.ProcedureModel;
using DreamTek.WMS.DAL.Common;
using DreamTek.WMS.DAL.Model.Base;
/// <summary>
/// 描述: 盘点单-->FrmSTOCK_CHECKBILLList 页面后台类文件
/// 作者: 
/// 创建于: 2012-10-17 16:04:25
/// </summary>

public partial class STOCK_FrmSTOCK_CHECKBILLList1 : WMSBasePage
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.grdSTOCK_CHECKBILL.Columns[1].Visible = false;
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            
            //txtDCHECKDATEFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        this.btnNew0.Attributes["onclick"] = this.GetPostBackEventReference(this.btnNew0) + ";this.disabled=true;";
        this.btnDelete.Attributes["onclick"] = this.GetPostBackEventReference(this.btnDelete) + ";this.disabled=true;";
        this.btnNew2.Attributes["onclick"] = this.GetPostBackEventReference(this.btnNew2) + ";this.disabled=true;";
    }


    #region IPageGrid 成员

    public void GridBind()
    {
        Bind("dcreatetime");
    }


    public IQueryable<view_STOCK_CHECKBILL_D> GetQueryList()
    {
        IGenericRepository<view_STOCK_CHECKBILL_D> conn = new GenericRepository<view_STOCK_CHECKBILL_D>(db);
        var caseList = from p in conn.Get()
                       orderby p.dcreatetime descending
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            if (txtCCHECKNAME.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccheckname) && x.ccheckname.Contains(txtCCHECKNAME.Text.Trim()));
            }
            if (txtCTICKETCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cticketcode) && x.cticketcode.Contains(txtCTICKETCODE.Text.Trim()));
            }
            if (txtCERPCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtCERPCODE.Text.Trim()));
            }
            if (dplCSTATUS.SelectedValue != string.Empty)
            {
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


            if (txtDCREATETIMEFrom.Text != string.Empty)
            {
                DateTime st = Convert.ToDateTime(txtDCREATETIMEFrom.Text + " 00:00:01");
                caseList = caseList.Where(x => x.dcreatetime != null && x.dcreatetime >= st);
            }
            if (txtDCREATETIMETo.Text != string.Empty)
            {
                DateTime ed = Convert.ToDateTime(txtDCREATETIMETo.Text + " 23:59:59");
                caseList = caseList.Where(x => x.dcreatetime != null && x.dcreatetime < ed);
            }

            if (txtCCREATEOWNERCODE.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ccreateownercode) && x.ccreateownercode.Contains(txtCCREATEOWNERCODE.Text.Trim()));
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

            if (txtDCIRCLECHECKBEGINDATE.Text != string.Empty && DateTime.TryParse(txtDCIRCLECHECKBEGINDATE.Text, out dtime))
            {
                caseList = caseList.Where(x => x.dcirclecheckbegindate != null && SqlFunctions.DateDiff("dd", txtDCIRCLECHECKBEGINDATE.Text.Trim(), x.dcirclecheckbegindate) >= 0);
            }
            if (txtDCIRCLECHECKENDDATE.Text != string.Empty && DateTime.TryParse(txtDCIRCLECHECKENDDATE.Text, out dtime))
            {
                caseList = caseList.Where(x => x.dcirclecheckenddate != null && SqlFunctions.DateDiff("dd", txtDCIRCLECHECKENDDATE.Text.Trim(), x.dcirclecheckenddate) <= 0);
            }
            if (!string.IsNullOrEmpty(drpWorkType.SelectedValue))
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.worktype) && x.worktype.Equals(drpWorkType.SelectedValue));
            }           
            //caseList = caseList.OrderByDescending(p => p.dcreatetime);
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
                caseList = caseList.OrderByDescending(p => p.dcreatetime);
            }

            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = " " + Resources.Lang.WMS_Common_Pager_PageCount + ":<b>" + "</b>";//总页数

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("cstatus", "CHECKSTATE"));//状态
        flagList.Add(new Tuple<string, string>("worktype", "WareHouseType"));//状态
        var srcdata = GetGridSourceDataByList(data, flagList);

        grdSTOCK_CHECKBILL.DataSource = srcdata;
        grdSTOCK_CHECKBILL.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("dcreatetime");
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
        if (this.txtDCIRCLECHECKBEGINDATE.Text.Trim().Length > 0)
        {
            if (this.txtDCIRCLECHECKBEGINDATE.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit1_BeginDateError);//循环盘点开始日期项不是有效的日期！
                this.SetFocus(txtDCIRCLECHECKBEGINDATE);
                return false;
            }
        }
        if (this.txtDCIRCLECHECKENDDATE.Text.Trim().Length > 0)
        {
            if (this.txtDCIRCLECHECKENDDATE.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit1_EndDateError);//循环盘点结束日期项不是有效的日期！
                this.SetFocus(txtDCIRCLECHECKENDDATE);
                return false;
            }
        }
        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";

        //https://docs.microsoft.com/en-us/dotnet/api/system.web.ui.webcontrols.gridview.datakeys?view=netframework-4.8
        //NOTEbyMark, 09/26, got it
        this.grdSTOCK_CHECKBILL.DataKeyNames = new string[] { "ID" };
        // this.grdSTOCK_CHECKBILL.DataKeyNames = new string[] { "ID,CTICKETCODE" };

        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmSTOCK_CHECKBILLEdit1.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmSTOCK_CHECKBILLList_NewPanDian + "','STOCK_CHECKBILL');return false;";//新建盘点单

        Help.DropDownListDataBind(GetParametersByFlagType("CHECKTYPE"), dplCHECKTYPE1, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//盘点类型
        dplCHECKTYPE1.SelectedValue = "1";
        var list = GetParametersByFlagType("CHECKSTATE");
        var item2 = list.Where(x => x.FLAG_ID == "2").FirstOrDefault();
        if (item2 != null) {
            list.Remove(item2);
        }
        var item3 = list.Where(x => x.FLAG_ID == "3").FirstOrDefault();
        if (item3 != null)
        {
            list.Remove(item3);
        }
        var item4 = list.Where(x => x.FLAG_ID == "4").FirstOrDefault();
        if (item4 != null)
        {
            list.Remove(item4);
        }
        Help.DropDownListDataBind(list, dplCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(GetParametersByFlagType("WareHouseType"), drpWorkType, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//作业方式


        if (!string.IsNullOrEmpty(Request.QueryString["Cstatus"]))
        {
            this.dplCSTATUS.SelectedValue = this.Request.QueryString["Cstatus"].ToString();
        }
        else
        {
            txtDCHECKDATEFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
        this.AspNetPager1_PageChanged(null, null);
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        btnDelete.Enabled = false;
        //DBUtil.BeginTrans();
        int count = 0;
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
                        if (bo != null && !bo.id.IsNullOrEmpty()
                            && !bo.cstatus.IsNullOrEmpty() && bo.cstatus.Equals("0"))
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
        btnDelete.Enabled = true;
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
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmSTOCK_CHECKBILLEdit1.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmSTOCK_CHECKBILLEdit_PanDainDan, "STOCK_CHECKBILL");////盘点单
        }

    }

    protected void dsGrdSTOCK_CHECKBILL_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    /// 审核
    /// <summary>
    /// 审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNew0_Click(object sender, EventArgs e)
    {
        //DBUtil.BeginTrans();
        try
        {
            int count = 0;
            int countSelect = 0;
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
                        countSelect++;
                        var dList = from p in conn_d.Get()
                                    where p.id == id
                                    select p;

                        if (dList != null && dList.Count() > 0)
                        {

                            STOCK_CHECKBILL bo = new STOCK_CHECKBILL();
                            bo = (from p in conn.Get().AsEnumerable()
                                  where p.id == id
                                  select p
                                      ).FirstOrDefault();
                            if (bo != null && !bo.id.IsNullOrEmpty()
                                && !bo.cstatus.IsNullOrEmpty() && bo.cstatus.Equals("0"))  //检查明细储位是否存在未完成的ASRS指令
                            {
                                //只能审核在当前日期或者当前日期之后的盘点单，已经过期的盘点单不能再审核了。
                                DateTime dateToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                if (bo.dcirclecheckenddate < dateToday)
                                {
                                    Alert(Resources.Lang.Common_CheckDiffTicketCode + bo.cticketcode + Resources.Lang.FrmSTOCK_CHECKBILLList_UnDoShenHe);//盘点单 + bo.cticketcode 盘点日期已过，不可审核；
                                    return;
                                }
                                //生成对应的出库的ASRS命令  2019-05-14
                                //盘点审核时，确认物料是否在该储位上，确认栈板号与储位是否一致 START BUCKINGHA-651  
                                DreamTek.ASRS.Business.Stock.StockQuery squery = new DreamTek.ASRS.Business.Stock.StockQuery();
                                var caselist = (from p in conn_d.Get()
                                                where p.id == id
                                                select p);
                                foreach (STOCK_CHECKBILL_D cd in caselist)
                                {
                                    string result = squery.IsSameWithStock(id, cd.cinvcode, cd.cpositioncode, cd.palletcode);
                                    if (result.Trim() != "OK")
                                    {
                                        Alert(result);
                                        return;
                                    }
                                }
                                //盘点审核时，确认物料是否在该储位上，确认栈板号与储位是否一致 END   BUCKINGHA-651

                                if (bo.worktype == "1")//立库 审核按钮时同时生成ASRS命令
                                {
                                    #region 调用存储过程
                                    Proc_CheckBillForAsrs proc = new Proc_CheckBillForAsrs();
                                    proc.P_CticketCode = bo.cticketcode;  //循环盘点单号
                                    proc.P_Storey = bo.lineid;   //楼层
                                    proc.P_UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                                    proc.Execute();
                                    if (proc.ReturnValue == 1)
                                    {
                                        //生成ASRS命令失败
                                        Alert(proc.ErrorMessage);
                                    }
                                    else
                                    {
                                        //生成ASRS命令成功，审核成功                                 
                                        count++;
                                    }
                                    #endregion
                                }
                                else if (bo.worktype == "0")//平库
                                {
                                    bo.cauditperson = WmsWebUserInfo.GetCurrentUser().UserNo;
                                    bo.daudittime = DateTime.Now;
                                    bo.cstatus = "1";
                                    conn.Update(bo);
                                    conn.Save();
                                    count++;
                                }


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
                Alert(Resources.Lang.FrmSTOCK_CHECKBILLList_ShenPiFinish);//审批完成
            }
            else if (countSelect == 0)
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
                        if (bo != null && !bo.id.IsNullOrEmpty()
                            && !bo.cstatus.IsNullOrEmpty() && bo.cstatus.Equals("0"))
                        {
                            bo.cstatus = "5";
                            conn.Update(bo);
                            conn.Save();
                            count++;
                        }
                    }
                }
            }
            this.GridBind();
            if (count > 0)
            {
                //Alert("申报成功");
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
        btnNew2.Enabled = false;
        try
        {
            int count = 0;
            int countSelect = 0;
            string id = string.Empty;
            string worktype = string.Empty;
            string return_value = string.Empty;
            string error_value = string.Empty;
            IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);

            for (int i = 0; i < this.grdSTOCK_CHECKBILL.Rows.Count; i++)
            {
                if (this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        countSelect++;
                        id = this.grdSTOCK_CHECKBILL.DataKeys[i].Values[0].ToString();
                        STOCK_CHECKBILL bo = new STOCK_CHECKBILL();
                        bo = (from p in conn.Get()
                              where p.id == id
                              select p
                                    ).FirstOrDefault();
                        if (bo != null && !bo.id.IsNullOrEmpty()
                            && !bo.cstatus.IsNullOrEmpty())
                        {
                            if (bo.worktype.Equals("0")) //平库 只有平库可以通过WMS进行循环盘点单结束
                            {
                                if (bo.cstatus.Equals("6"))
                                {
                                    //bo.cstatus = "5";
                                    //conn.Update(bo);
                                    //conn.Save();
                                    //用脚本去update 数据
                                    Proc_UpdateCheckBillStatus proc = new Proc_UpdateCheckBillStatus();
                                    proc.P_ID = bo.id;
                                    proc.Execute();
                                    return_value = proc.P_Return_Value;
                                    error_value = proc.P_Error_Value;
                                    if (return_value == "0")
                                    {
                                        count++;
                                    }
                                    else
                                    {
                                        throw new Exception(error_value);
                                    }
                                }
                                else
                                {
                                    throw new Exception(Resources.Lang.FrmSTOCK_CHECKBILLList_PanDianZhong);//只有盘点中的才可以结束！
                                }
                            }

                        }
                    }
                }
            }
            if (count != countSelect)
            {
                throw new Exception(Resources.Lang.FrmSTOCK_CHECKBILLList_PingkuKeShouDong);//只有平库的盘点单可以手动结束！
            }
            this.GridBind();
            if (count > 0)
            {
                Alert(Resources.Lang.FrmSTOCK_CHECKBILLList_PanDianJieShu);//盘点已结束！
            }
            else if (countSelect == 0)
            {
                Alert(Resources.Lang.FrmSTOCK_CHECKBILLList_SelectJieShu);//请选择要结束的项！
            }
        }
        catch (Exception err)
        {
            Alert(err.Message.ToJsString());
        }
        btnNew2.Enabled = true; ;
    }

    #endregion


    protected void btn0925_Click(object sender, EventArgs e)
    {
        Alert("ASRS  ");
        //Alert("update CMD_MST set cmdsts='7' where WmsTskId='7406' EXEC dbo.Proc_ModifyStatus_FromASRS_ID '7406'; ");
        try
        {
            int count = 0;
            int countSelect = 0;
            //NOTE by Mark, 09/26
            IGenericRepository<CMD_MST> connCMD_MST = new GenericRepository<CMD_MST>(db);
            
            IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);
            IGenericRepository<STOCK_CHECKBILL_D> conn_d = new GenericRepository<STOCK_CHECKBILL_D>(db);
            string id = string.Empty;
            string ticket = string.Empty;
            for (int i = 0; i < this.grdSTOCK_CHECKBILL.Rows.Count; i++)
            {
                if (this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_CHECKBILL.Rows[i].Cells[0].Controls[1];
                  //NOTE by Mark, 09/26 why cannot find def
                //https://docs.microsoft.com/en-us/dotnet/api/system.web.ui.webcontrols.gridview.datakeys?view=netframework-4.8
                    id = this.grdSTOCK_CHECKBILL.DataKeys[i].Values[0].ToString();
                    //ticket = this.grdSTOCK_CHECKBILL.DataKeys[i].Values[1].ToString();
                    //Alert(id);
                   

                    if (chkSelect.Checked)
                    {
                        countSelect++;
                        //NOTE by Mark,09/26
                        var m1= (from p in conn.Get()
                                    where p.id == id 
                                    select p).FirstOrDefault();
                        //Alert("ID->ticket is " + m1.cticketcode);

                        var m2 = (from p in connCMD_MST.Get()
                                  where p.CTICKETCODE == m1.cticketcode 
                                  && p.CmdSts == "0"
                                  select p).FirstOrDefault();
                        if (m2 == null)
                        {
                            //Alert("ticket->CMD_MST ID is 沒有 ");
                            continue;
                        }
                        Alert("立庫指令 ID 是 " + m2.WmsTskId);


                        //List<CmdMst> actionList = new List<CmdMst>();
                        //string sql = string.Format(@" EXEC dbo.Proc_LK_GetCommands '{0}'  ", lineId);
                        //actionList = DapperHelper.Query<CmdMst>(sql).ToList();
                        string sql = string.Format(@"  update CMD_MST set cmdsts = '7' where WmsTskId = '{0}'  ", m2.WmsTskId);
                        var r1= DBHelp.ExecuteToInt(sql);
                        Alert(""+r1);
                        if (r1==1)
                        {
                            Alert(String.Format("CMD_MST WmsTskId {0} 狀態已改變", m2.WmsTskId));

                            string sql2 = string.Format(@"  EXEC dbo.Proc_ModifyStatus_FromASRS_ID  '{0}'  ", m2.WmsTskId);
                            var r2 = DBHelp.ExecuteToInt(sql2);
                            Alert(String.Format("dbo.Proc_ModifyStatus_FromASRS_ID 的返回值為 {0} ", r2));
                            if (r2 == 1)
                            {
                                Alert("請使用 WebPDA 進行盤點!");
                                return;
                            }

                        }

                       
                        continue;
                        // 以下代碼不會在這裡運行

                     
                        var dList = from p in conn_d.Get()
                                    where p.id == id
                                    select p;

                        if (dList != null && dList.Count() > 0)
                        {

                            STOCK_CHECKBILL bo = new STOCK_CHECKBILL();
                            bo = (from p in conn.Get().AsEnumerable()
                                  where p.id == id
                                  select p
                                      ).FirstOrDefault();
                            if (bo != null && !bo.id.IsNullOrEmpty()
                                && !bo.cstatus.IsNullOrEmpty() && bo.cstatus.Equals("0"))  //检查明细储位是否存在未完成的ASRS指令
                            {
                                //只能审核在当前日期或者当前日期之后的盘点单，已经过期的盘点单不能再审核了。
                                DateTime dateToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                if (bo.dcirclecheckenddate < dateToday)
                                {
                                    Alert(Resources.Lang.Common_CheckDiffTicketCode + bo.cticketcode + Resources.Lang.FrmSTOCK_CHECKBILLList_UnDoShenHe);//盘点单 + bo.cticketcode 盘点日期已过，不可审核；
                                    return;
                                }
                                //生成对应的出库的ASRS命令  2019-05-14
                                //盘点审核时，确认物料是否在该储位上，确认栈板号与储位是否一致 START BUCKINGHA-651  
                                DreamTek.ASRS.Business.Stock.StockQuery squery = new DreamTek.ASRS.Business.Stock.StockQuery();
                                var caselist = (from p in conn_d.Get()
                                                where p.id == id
                                                select p);
                                foreach (STOCK_CHECKBILL_D cd in caselist)
                                {
                                    string result = squery.IsSameWithStock(id, cd.cinvcode, cd.cpositioncode, cd.palletcode);
                                    if (result.Trim() != "OK")
                                    {
                                        Alert(result);
                                        return;
                                    }
                                }
                                //盘点审核时，确认物料是否在该储位上，确认栈板号与储位是否一致 END   BUCKINGHA-651

                                if (bo.worktype == "1")//立库 审核按钮时同时生成ASRS命令
                                {
                                    #region 调用存储过程
                                    Proc_CheckBillForAsrs proc = new Proc_CheckBillForAsrs();
                                    proc.P_CticketCode = bo.cticketcode;  //循环盘点单号
                                    proc.P_Storey = bo.lineid;   //楼层
                                    proc.P_UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                                    proc.Execute();
                                    if (proc.ReturnValue == 1)
                                    {
                                        //生成ASRS命令失败
                                        Alert(proc.ErrorMessage);
                                    }
                                    else
                                    {
                                        //生成ASRS命令成功，审核成功                                 
                                        count++;
                                    }
                                    #endregion
                                }
                                else if (bo.worktype == "0")//平库
                                {
                                    bo.cauditperson = WmsWebUserInfo.GetCurrentUser().UserNo;
                                    bo.daudittime = DateTime.Now;
                                    bo.cstatus = "1";
                                    conn.Update(bo);
                                    conn.Save();
                                    count++;
                                }


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
                //Alert(Resources.Lang.FrmSTOCK_CHECKBILLList_ShenPiFinish);//审批完成
            }
            else if (countSelect == 0)
            {
                Alert("請選擇要處理ASRS的項次");//请选择要审批的项 --請選擇要處理ASRS的項次
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
}

