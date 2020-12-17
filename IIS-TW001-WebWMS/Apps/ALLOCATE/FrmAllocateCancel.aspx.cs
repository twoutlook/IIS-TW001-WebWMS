using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Text;
using System.ComponentModel;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using DreamTek.ASRS.Business.Allocate;



/// <summary>
/// 描述: 1111-->FrmALLOCATEList 页面后台类文件
/// 作者: --chenlei
/// 创建于: 2012-11-01 09:31:05
/// </summary>
public partial class ALLOCATE_FrmAllocateCancel : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }    
    }


    #region IPageGrid 成员
    public int PageSize = 15;
    /// <summary>
    /// 当前页数
    /// </summary>
    public int CurrendIndex
    {
        get
        {
            if (ViewState["CurrendIndex"] == null)
            {
                ViewState["CurrendIndex"] = 1;
            }
            return (int)ViewState["CurrendIndex"];
        }
        set
        {
            ViewState["CurrendIndex"] = value;
        }
    }
    public void GridBind()
    {
        int pageCount = 0;
        AllocateQuery query = new AllocateQuery();
        DataTable dt = query.GetAllocateList(txtCCREATEOWNERCODE.Text.Trim(), txtDCREATETIMEFrom.Text.Trim(), txtDCREATETIMETo.Text.Trim(), txtCAUDITPERSON.Text.Trim(),
             txtDAUDITTIMEFrom.Text.Trim(), txtDAUDITTIMETo.Text.Trim(), txtCTICKETCODE.Text.Trim(), txtDINDATEFrom.Text.Trim(), txtDINDATETo.Text.Trim(), ddlCSTATUS.SelectedValue, txtERP.Text.Trim(), txtLH.Text.Trim(), "1", "", "","", CurrendIndex, PageSize, out pageCount);
       
        List<Tuple<string, string, string>> flagList = new List<Tuple<string, string, string>>();
        flagList.Add(new Tuple<string, string, string>("cstatus", "newcstatus", "AllocateCancelStatus"));//狀態
       
        var srcdata = GetGridDataByDataTable(dt, flagList);
        
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;
        grdALLOCATE.DataSource = srcdata;
        grdALLOCATE.DataBind();
        ;
    }

    public bool CheckData()
    {
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmALLOCATEList_NoEfftxtDCREATETIMEFrom);//"制单日期项不是有效的日期！"
                this.SetFocus(txtDCREATETIMEFrom);
                return false;
            }
        }
        if (this.txtDCREATETIMETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_ToItemEmpty);//到项不允许空！
            this.SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (this.txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIMETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmALLOCATEList_NoEffToItem);//到项不是有效的日期！
                this.SetFocus(txtDCREATETIMETo);
                return false;
            }
        }
        if (this.txtCAUDITPERSON.Text.Trim().Length > 0)
        {
        }
        if (this.txtDAUDITTIMEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDAUDITTIMEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmALLOCATEList_NoEfftxtDAUDITTIMEFrom); //审核日期项不是有效的日期！
                this.SetFocus(txtDAUDITTIMEFrom);
                return false;
            }
        }
        if (this.txtDAUDITTIMETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_ToItemEmpty);//到项不允许空！
            this.SetFocus(txtDAUDITTIMETo);
            return false;
        }
        if (this.txtDAUDITTIMETo.Text.Trim().Length > 0)
        {
            if (this.txtDAUDITTIMETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmALLOCATEList_NoEffToItem);//到项不是有效的日期！
                this.SetFocus(txtDAUDITTIMETo);
                return false;
            }
        }
        if (this.txtCTICKETCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDINDATEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDINDATEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmALLOCATEList_NoEfftxtDINDATEFrom);//调拨日期项不是有效的日期！
                this.SetFocus(txtDINDATEFrom);
                return false;
            }
        }
        if (this.txtDINDATETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_ToItemEmpty);//到项不允许空！
            this.SetFocus(txtDINDATETo);
            return false;
        }
        if (this.txtDINDATETo.Text.Trim().Length > 0)
        {
            if (this.txtDINDATETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmALLOCATEList_NoEffToItem);//到项不是有效的日期！
                this.SetFocus(txtDINDATETo);
                return false;
            }
        }
        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/LG/Images/Login/icon1.gif";
        this.grdALLOCATE.DataKeyNames = new string[] { "ID", "CSTATUS" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
       // this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + PageBase.BuildRequestPageURL("FrmALLOCATEEdit.aspx", SysOperation.New, "") + "','新建调拨单','ALLOCATE');return false;";
        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmALLOCATEEdit.aspx", SysOperation.New,""),800,600);
        //调拨单状态
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "AllocateCancelStatus", false, -1, -1), this.ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
    }

    #endregion

   
   
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        GridBind();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        //DBUtil.BeginTrans();
        IGenericRepository<ALLOCATE> conn = new GenericRepository<ALLOCATE>(db);
        IGenericRepository<ALLOCATE_D> connD = new GenericRepository<ALLOCATE_D>(db);
        AllocateQuery allQry = new AllocateQuery();
        string alloId = string.Empty;
        try
        {
            for (int i = 0; i < this.grdALLOCATE.Rows.Count; i++)
            {
                if (this.grdALLOCATE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdALLOCATE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        if (this.grdALLOCATE.Rows[i].Cells[this.grdALLOCATE.Rows[i].Cells.Count - 1].Text == "未处理")
                        {
                            alloId = this.grdALLOCATE.DataKeys[i].Values[0].ToString().Trim();
                            ALLOCATE_D dBO = (from p in connD.Get()//  conn.GetByID(this.grdALLOCATE_D.DataKeys[i].Values[0].ToString().Trim());
                                              where p.id == alloId
                                              select p).FirstOrDefault();
                            if (dBO != null && dBO.asrs_status.HasValue)
                            {
                                if (dBO.asrs_status.Value != 0)
                                {
                                    msg += "已经产生AS/RS命令，不能删除";
                                    break;
                                }
                            }
                            if (this.grdALLOCATE.Rows[i].Cells[10].Text != "普通调拨")
                            {
                                msg += "调拨单类型不是普通调拨，不能删除";
                                break;
                            }
                            //if (allQry.AllocateFromOutBill(alloId))
                            //{
                            //    msg += "调拨单由出库单自动产生，不能删除";
                            //    break;
                            //}

                        }
                    }
                }
            }
            if (msg.Length != 0)
            {
                this.Alert(msg);
                return;
            }

            for (int i = 0; i < this.grdALLOCATE.Rows.Count; i++)
            {
                if (this.grdALLOCATE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdALLOCATE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string status = this.grdALLOCATE.DataKeys[i].Values[1].ToString().Trim();
                        if (status=="0")//if (this.grdALLOCATE.Rows[i].Cells[this.grdALLOCATE.Rows[i].Cells.Count - 2].Text == "未处理") 0:未处理 
                        {
                            string ids = this.grdALLOCATE.DataKeys[i].Values[0].ToString().Trim();
                            ALLOCATE_D dBO = (from p in connD.Get()//  conn.GetByID(this.grdALLOCATE_D.DataKeys[i].Values[0].ToString().Trim());
                                              where p.ids == ids
                                              select p).FirstOrDefault();
                            if (dBO != null && dBO.asrs_status.HasValue)
                            {
                                if (dBO.asrs_status.Value != 0)
                                {
                                    msg += Resources.Lang.FrmALLOCATEList_ExCommondNoDel; //已经产生AS/RS命令，不能删除
                                    break;
                                }
                                else
                                {
                                    //conn.Delete(this.grdALLOCATE.DataKeys[i].Values[0].ToString().Trim());
                                    //conn.Save();
                                }
                            }
                            conn.Delete(this.grdALLOCATE.DataKeys[i].Values[0].ToString().Trim());
                            conn.Save();
                        }
                        else
                        {
                            msg = Resources.Lang.FrmALLOCATEList_OnlyWCLCanDel; //只有状态为[未處理]的单据才能删除.
                            break;
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.Common_SuccessDel; //删除成功!
            }

            //DBUtil.Commit();
            this.GridBind();
        }
        catch (Exception E)
        {
            msg += Resources.Lang.Common_FailDel; //"删除失败!";
            //this.Alert(Resources.Lang.Common_FailDel + E.Message.ToJsString()); //删除失败！
            //DBUtil.Rollback();
        }
        this.Alert(msg);
    }



    //private string GetKeyIDS(int rowIndex)
    //{
    //    string strKeyId = "";
    //    for (int i = 0; i < this.grdALLOCATE.DataKeyNames.Length; i++)
    //    {
    //        strKeyId += this.grdALLOCATE.DataKeys[rowIndex].Values[i].ToString() + ",";
    //    }
    //    strKeyId = strKeyId.TrimEnd(',');
    //    return strKeyId;
    //}

    protected void grdALLOCATE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.grdALLOCATE.DataKeys[e.Row.RowIndex].Values[0].ToString();//this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmAllocateCancelEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmALLOCATEList_Title1, "ALLOCATE");//调拨单
           
            //HyperLink linkModify_P = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            //linkModify_P.NavigateUrl = "#";


            //this.OpenFloatWin(linkModify_P, BuildRequestPageURL("/Apps/ALLOCATE_Report/ALLocate_Print.aspx", SYSOperation.Modify, strKeyID), "打印页面", "PrintAllocate", 800, 600);


            //((LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0]).Enabled = e.Row.Cells[e.Row.Cells.Count - 3].Text == "0" ? false : true;
            //switch (e.Row.Cells[e.Row.Cells.Count - 2].Text)
            //{
            //    case "0":
            //        e.Row.Cells[e.Row.Cells.Count - 2].Text = "未处理";
            //        break;
            //    case "1":
            //        e.Row.Cells[e.Row.Cells.Count - 2].Text = "已审核";
            //        break;
            //    case "2": e.Row.Cells[e.Row.Cells.Count - 2].Text = "已完成";

            //        break;
            //    case "3":
            //        e.Row.Cells[e.Row.Cells.Count - 2].Text = "已抛转";
            //        break;
            //    case "4":
            //        e.Row.Cells[e.Row.Cells.Count - 2].Text = "已确认";
            //        break;
            //    case "5":
            //        e.Row.Cells[e.Row.Cells.Count - 2].Text = "調撥中";
            //        break;
            //    case "6":
            //        e.Row.Cells[e.Row.Cells.Count - 2].Text = "調撥完成";
            //        break;

            //}
            //ddlCSTATUS
            //try
            //{
            //    e.Row.Cells[e.Row.Cells.Count - 2].Text = ddlCSTATUS.Items.FindByValue(e.Row.Cells[e.Row.Cells.Count - 2].Text.Trim()).Text;
            //}
            //catch
            //{
            //    e.Row.Cells[e.Row.Cells.Count - 2].Text = Resources.Lang.Common_ExceptionStatus; //异常状态
            //}
            //e.Row.Cells[e.Row.Cells.Count - 2].Text = e.Row.Cells[e.Row.Cells.Count - 2].Text == "0" ? "未处理" : "已完成";
        }
    }

    protected void dsGrdALLOCATE_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }
   

    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }

    /// <summary>
    /// 获取选择的ID
    /// </summary>
    public void GetSelectedIds()
    {
        if (SelectIds == null)
        {
            SelectIds = new Dictionary<string, string>();
        }

        foreach (GridViewRow item in this.grdALLOCATE.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string id = this.grdALLOCATE.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                //控件选中且集合中不存在添加
                if (cbo.Enabled && cbo.Checked && !SelectIds.ContainsKey(id))
                {
                    SelectIds.Add(id, this.grdALLOCATE.DataKeys[item.RowIndex][1].ToString());
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(id))
                {
                    SelectIds.Remove(id);
                }
            }
        }
    }  


    public void GetSelectedId()
    {
        if (SelectIds == null)
        {
            SelectIds = new Dictionary<string, string>();
        }

        foreach (GridViewRow item in this.grdALLOCATE.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string id = this.grdALLOCATE.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                //控件选中且集合中不存在添加
                if (cbo.Enabled && cbo.Checked && !SelectIds.ContainsKey(id))
                {
                    SelectIds.Add(id, this.grdALLOCATE.DataKeys[item.RowIndex][0].ToString());
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(id))
                {
                    SelectIds.Remove(id);
                }
            }
        }
    }

}

