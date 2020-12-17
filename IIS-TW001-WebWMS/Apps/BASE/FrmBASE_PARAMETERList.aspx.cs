using DreamTek.ASRS.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_FrmBASE_PARAMETERList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBASE_PARAMETEREdit.aspx", SYSOperation.New, "") + "','新建代码组','PARAMETER',850,470);return false;";//新建代码组

    }

    /// <summary>
    /// 分页控件事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;//默认第一页
            AspNetPager1.CurrentPageIndex = 1;
        }
        IsFirstPage = true;
        this.GridBind();
    }

    public void GridBind()
    {
        using (var modContext = this.context)
        {
            var queryList = from p in modContext.SYS_PARAMETERGROUP
                            orderby p.CREATETIME descending
                            where 1 == 1
                            select p;
            if (!string.IsNullOrEmpty(txtFlagType.Text.Trim()))
            {
                queryList = queryList.Where(x => x.FLAG_TYPE.Contains(txtFlagType.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtFlagName.Text.Trim()))
            {
                queryList = queryList.Where(x => x.REMARK.Contains(txtFlagName.Text.Trim()));
            }
           
            AspNetPager1.RecordCount = queryList.Count();
            AspNetPager1.PageSize = this.PageSize;
            this.grdSys_Parameter.DataSource = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
            this.grdSys_Parameter.DataBind();
        }
    }

    /// <summary>
    /// 列表行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdSys_Parameter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBASE_PARAMETEREdit.aspx", SYSOperation.Modify, strKeyID), "修改代码组", "PARAMETER");
        }
    }

    /// <summary>
    /// 获取对应列表行id
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <returns></returns>
    private string GetKeyIDS(int rowIndex)
    {
        return this.grdSys_Parameter.DataKeys[rowIndex].Values[0].ToString();
    }

    /// <summary>
    /// 删除通知单事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        string updateStatusMsg = string.Empty;
        try
        {
            string userno = WmsWebUserInfo.GetCurrentUser().UserNo;           

            for (int i = 0; i < this.grdSys_Parameter.Rows.Count; i++)
            {
                if (this.grdSys_Parameter.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSys_Parameter.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string outasnId = this.grdSys_Parameter.DataKeys[i].Values[0].ToString();
                        var modOutAsn = context.SYS_PARAMETERGROUP.Where(x => x.ID == outasnId).FirstOrDefault();
                        if (modOutAsn != null)
                        {
                            //删除名称
                            string strSqld = string.Format("delete from SYS_PARAMETERNAME where FLAG_GUID in (select id from SYS_PARAMETER where flag_type = '{0}' )", modOutAsn.FLAG_TYPE);
                            DBHelp.ExecuteNonQuery(strSqld);
                            //删除明细
                            string strSql = string.Format("delete from SYS_PARAMETER  where flag_type = '{0}'", modOutAsn.FLAG_TYPE);
                            DBHelp.ExecuteNonQuery(strSql);

                            //删除自己
                            string strSqll = string.Format("delete from SYS_PARAMETERGROUP  where id = '{0}'", modOutAsn.ID);
                            DBHelp.ExecuteNonQuery(strSqll);
                        }
                        else
                        {
                            msg = Resources.Lang.FrmOUTASNList_Tips_YiChang_DeleteFailed;
                            break;
                        }
                    }
                }
            }
            CacheHelper.RemoveAllCache();
            if (msg.Length == 0)
            {
                msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;
            }
            else
            {
                msg = msg + Resources.Lang.WMS_Common_Msg_DeleteFailed;
            }
            this.GridBind();
        }
        catch
        {
            msg += Resources.Lang.WMS_Common_Msg_DeleteFailed;
        }
        this.Alert(msg);
    }
}