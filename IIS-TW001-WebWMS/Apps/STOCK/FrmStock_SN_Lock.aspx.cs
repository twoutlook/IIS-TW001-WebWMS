using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Linq;
using System.Data.Entity.SqlServer;
/// <summary>
/// 描述: 库存SN锁定
/// 作者: 
/// 创建于: 2013-9-23 11:28:14
/// </summary>

public partial class Stock_FrmStock_SN_Lock : WMSBasePage
{

    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        this.btnSetstatus.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSetstatus) + ";this.disabled=true;";
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<STOCK_SN_LOCK> entity = new GenericRepository<STOCK_SN_LOCK>(context);
        var caseList = from p in entity.Get()
                       orderby p.createtime descending
                       where 1 == 1
                       select p;
        if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim().ToUpper()));
        if (txtSN.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.sncode) && x.cinvcode.Contains(txtSN.Text.Trim().ToUpper()));
        if (txtCpostionCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.positioncode) && x.positioncode.Contains(txtCpostionCode.Text.Trim().ToUpper()));
        if (txtCreateUser.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.createowner) && x.createowner.Contains(txtCreateUser.Text.Trim()));

        if (!string.IsNullOrEmpty(txtLockDate.Text.Trim()))
            caseList = caseList.Where(x => x.locktime != null && SqlFunctions.DateDiff("dd", txtLockDate.Text.Trim(), x.locktime) >= 0);
        if (txtLockdateTo.Text != string.Empty)
            caseList = caseList.Where(x => x.locktime != null && SqlFunctions.DateDiff("dd", txtLockdateTo.Text.Trim(), x.locktime) <= 0);

        if (!string.IsNullOrEmpty(txtUnLockdate.Text.Trim()))
            caseList = caseList.Where(x => x.unlocktime != null && SqlFunctions.DateDiff("dd", txtUnLockdate.Text.Trim(), x.unlocktime) >= 0);
        if (txtUnLockdateTo.Text != string.Empty)
            caseList = caseList.Where(x => x.unlocktime != null && SqlFunctions.DateDiff("dd", txtUnLockdateTo.Text.Trim(), x.unlocktime) <= 0);
        if (ddpIsorNo.SelectedValue != "")
            caseList = caseList.Where(x => x.cstatus.ToString().Equals(ddpIsorNo.SelectedValue));
        if (ddpEnable.SelectedValue != "")
            caseList = caseList.Where(x => x.cenable.ToString().Equals(ddpEnable.SelectedValue));
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
       // grdSNLock.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        var listResult = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        List<Tuple<string,  string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("CENABLE", "Common_USE"));//是否可用
        flagList.Add(new Tuple<string, string>("CSTATUS", "Common_EnableExp"));//状态
        var source = GetGridSourceDataByList(listResult, flagList);
        grdSNLock.DataSource = source;
        grdSNLock.DataBind();       
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //CurrendIndex = 1;
        //this.GridBind();
        //BUCKINGHA-894
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
        }       
        this.GridBind();
        IsFirstPage = true;//恢复默认值
        //BUCKINGHA-894
    }
    public bool CheckData()
    {
        return true;
    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdSNLock.DataKeyNames = new string[] { "ID" };
        //本页面打开新增窗口
        this.btnSNLock.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmStock_SN_LockEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.WMS_Common_Button_SNLock + "','SN_Lock');return false;";//SN冻结
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('SN_LockorUn');return false;";
        //ddpIsorNo 状态
        Help.DropDownListDataBind(GetParametersByFlagType("Common_EnableExp"), this.ddpIsorNo, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        //ddpEnable 是否占用
        Help.DropDownListDataBind(GetParametersByFlagType("Common_USE"), this.ddpEnable, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
    }

    #endregion
    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdSNLock.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdSNLock.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdSNLock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmStock_SN_Lock_Edit.aspx", SYSOperation.Modify, strKeyID), "", "SN_Lock", 800, 600);
            //e.Row.Cells[e.Row.Cells.Count - 3].Text = e.Row.Cells[e.Row.Cells.Count - 3].Text == "0" ? Resources.Lang.WMS_Common_ZhuangTai_YouXiao : Resources.Lang.WMS_Common_ZhuangTai_WuXiao; // "有效" : "无效";
            //e.Row.Cells[e.Row.Cells.Count - 4].Text = e.Row.Cells[e.Row.Cells.Count - 4].Text == "0" ? Resources.Lang.WMS_Common_JinYong_KeYong : Resources.Lang.WMS_Common_JinYong_BuKeYong; //"可用" : "不可用";
        }

    }
    /// 设置SN的状态
    /// <summary>
    /// 设置SN的状态
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSetstatus_Click(object sender, EventArgs e)
    {
        IGenericRepository<STOCK_SN_LOCK> con = new GenericRepository<STOCK_SN_LOCK>(context);
        string msg = string.Empty;
        try
        {
            if (this.grdSNLock.Rows.Count > 0)
            {
                for (int i = 0; i < this.grdSNLock.Rows.Count; i++)
                {
                    if (this.grdSNLock.Rows[i].FindControl("chkSelect") is CheckBox)
                    {
                        CheckBox chkSelect = (CheckBox)this.grdSNLock.Rows[i].FindControl("chkSelect");
                        if (chkSelect.Checked)
                        {
                            string id = this.grdSNLock.DataKeys[i].Values[0].ToString().Trim();
                            var caseList = from p in con.Get()
                                           where p.id == id
                                           select p;
                            STOCK_SN_LOCK entity = caseList.ToList().FirstOrDefault<STOCK_SN_LOCK>();
                            //判断SN是否已经和ErpCode关联，关联则不允许
                            if (Stock_Checked.CheckSNIs_ExistErp(entity.sncode))
                            {
                                msg += "[" + entity.sncode + "]";
                            }
                            else
                            {
                                if (entity.cstatus.ToString() == "0")
                                {
                                    entity.cstatus = Convert.ToInt32(dpdCstatus.SelectedValue);
                                    entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                                    entity.lastupdatetime = DateTime.Now;
                                    con.Update(entity);
                                    con.Save();
                                }
                                else
                                {
                                    Alert("[" + entity.sncode + "]" + Resources.Lang.FrmStock_SN_Lock_IsWuXiao);//已经是无效状态！
                                    return;
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                msg = Resources.Lang.FrmStock_SN_Lock_SelectSetOption;//请选择你需要设置的项！
                this.Alert(msg);
                return;
            }

            if (msg.Length == 0)
            {
                msg = Resources.Lang.WMS_Common_Element_ModifySuccess;//修改成功！
                this.Alert(msg);
            }
            else
            {
                this.Alert(msg + Resources.Lang.FrmStock_SN_Lock_YiGuanLian);//已关联ErpCode，请在【特殊出库维护】中设置无效后再修改!
            }
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.WMS_Common_Element_ModifyFailed + E.Message.ToJsString());//修改失败！
        }
    }

}

