using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;

/// <summary>
/// 描述: 库存Datecode锁定
/// 作者: 
/// 创建于: 2013-9-23 11:28:14
/// </summary>

public partial class Stock_FrmStock_DateCodeLock : WMSBasePage
{

    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<V_Stock_DateCode> entity = new GenericRepository<V_Stock_DateCode>(context);
        var caseList = from p in entity.Get()
                       orderby p.datecode ascending
                       where 1 == 1
                       select p;
        if (!string.IsNullOrEmpty(txtDateCode.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.datecode.ToString()) && x.datecode.ToString().Contains(txtDateCode.Text.Trim()));
        if (txtCinvcode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim().ToUpper()));
        if (txtCpostionCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCpostionCode.Text.Trim().ToUpper()));
        if (txtLockUser.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.lockuser) && x.lockuser.Contains(txtLockUser.Text.Trim()));
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        grdDateCodeLock.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdDateCodeLock.DataBind();
    }

    public bool CheckData()
    {

        if (this.txtCinvcode.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOutOrder_DEdit_Tips_NeedCinvcode);//料号不允许为空
            this.SetFocus(txtCinvcode);
            return false;
        }

        return true;
    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdDateCodeLock.DataKeyNames = new string[] { "IDS" };

        //本页面打开新增窗口
        this.btnDateCodeLock.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmStock_DateCodeLock_Edit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmStock_DateCodeLock_PageName + "','DateCode_Lock');return false;";//DateCode锁定
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('DateCode_LockorUN');return false;";
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
        this.GridBind();
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdDateCodeLock.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdDateCodeLock.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdDateCodeLock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
    /// 解鎖按鈕
    /// <summary>
    /// 解鎖按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDateCodeUnLock_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        GetSelectedId();
        try
        {
            if (SelectIds != null && SelectIds.Count > 0)
            {
                string errmsg = "";
                //循環鎖定選定的行
                foreach (var item in SelectIds)
                {
                    #region 调用存储过程
                    List<string> SparaList = new List<string>();
                    SparaList.Add("@P_IDS:" + item.Key);
                    SparaList.Add("@P_CinvCode:" + "");
                    SparaList.Add("@P_Position:" + "");
                    SparaList.Add("@P_Qty:" + "0");
                    SparaList.Add("@P_BZ:" + "1");//解锁
                    SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                    SparaList.Add("@P_return_Value:" + "");
                    SparaList.Add("@errText:" + "");
                    string[] Result = DBHelp.ExecuteProc("Proc_DateCode_LockorUnLock", SparaList);
                    if (Result.Length == 1)//调用存储过程有错误
                    {
                        errmsg = Result[0].ToString();

                        if (msg != "")
                        {
                            msg = msg + errmsg + ",";
                        }
                        else
                        {
                            msg = msg + errmsg;
                        }
                    }
                    else if (Result[0] == "1")
                    {
                        errmsg = Result[1].ToString();
                        if (msg != "")
                        {
                            msg = msg + errmsg + ",";
                        }
                        else
                        {
                            msg = msg + errmsg;
                        }

                    }
                    #endregion
                }
            }
            else
            {
                msg = Resources.Lang.WMS_Common_Tips_NeedSelect;//请选择数据！
            }
            if (msg.Length == 0)
            {
                msg = Resources.Lang.WMS_Common_Tips_UnLockSuccess;//解锁成功！
                this.Alert(msg);
            }
            else
            {
                this.Alert(msg);
            }
        }
        catch (Exception err)
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_UnLockFailed + err.Message);//解锁失敗
        }
        //清空数据集
        SelectIds = null;
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }
    /// <summary>
    /// 获取选中的行
    /// </summary>
    public void GetSelectedId()
    {
        try
        {
            if (SelectIds == null)
            {
                SelectIds = new Dictionary<string, string>();
            }

            foreach (GridViewRow item in this.grdDateCodeLock.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    CheckBox cbo = itemFindControl as CheckBox;


                    //获取ID
                    string ids = this.grdDateCodeLock.DataKeys[item.RowIndex][0].ToString();


                    //控件选中且集合中不存在添加
                    if (cbo.Enabled && cbo.Checked && !SelectIds.ContainsKey(ids))
                    {
                        SelectIds.Add(ids, this.grdDateCodeLock.DataKeys[item.RowIndex][0].ToString());
                    }//未选中且集合中存在的移除                    
                    else if (!cbo.Checked && SelectIds.ContainsKey(ids))
                    {
                        SelectIds.Remove(ids);
                    }
                }
            }
        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }

}

