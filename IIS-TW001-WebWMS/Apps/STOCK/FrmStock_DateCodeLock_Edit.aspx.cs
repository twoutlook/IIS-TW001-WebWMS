using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Linq;

/// <summary>
/// 描述: 库存Datecode锁定
/// 作者: 
/// 创建于: 2013-9-23 11:28:14
/// </summary>

public partial class Stock_FrmStock_DateCodeLock_Edit : WMSBasePage
{

    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
        }
        btnDateCodeLock.Attributes.Add("onclick", this.GetPostBackEventReference(btnDateCodeLock) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<V_Stock_DataCode_Edit> entity = new GenericRepository<V_Stock_DataCode_Edit>(context);
        var caseList = from p in entity.Get()
                       orderby p.datecode ascending
                       where 1 == 1
                       select p;
        if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim().ToUpper()));
        if (txtDateCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.datecode.ToString()) && x.datecode.ToString().Contains(txtDateCode.Text.Trim()));
        if (txtCpostionCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCpostionCode.Text.Trim().ToUpper()));

        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        grdDateCode_Lock.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdDateCode_Lock.DataBind();
    }

    public bool CheckData()
    {

        if (this.txtCinvcode.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOutOrder_DEdit_Tips_NeedCinvcode);//料号不允许为空！
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
        this.grdDateCode_Lock.DataKeyNames = new string[] { "IDS" };

        //本页面打开新增窗口
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('DateCode_Lock');return false;";
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
        if (CheckData())
        {
            this.GridBind();
        }
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdDateCode_Lock.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdDateCode_Lock.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdDateCode_Lock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void dsGrdDateCode_Lock_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    /// 鎖定按鈕
    /// <summary>
    /// 鎖定按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDateCodeLock_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        GetSelectedId();
        try
        {
            if (SelectCode != null && SelectCode.Count > 0)
            {
                //檢查選定的行可鎖定數量是否為0
                foreach (var item in SelectCode)
                {
                    if (Stock_CurrentQuery.GetDateCodeLockNum(item.Key) == 0)
                    {
                        Alert(Resources.Lang.FrmStock_DateCodeLock_ReSelect);//选择了可锁定数量为0的行，请重新选择！
                        //清空数据集
                        SelectCode = null;
                        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
                        return;
                    }
                }
                //循環鎖定選定的行
                foreach (var item in SelectCode)
                {
                    #region 调用存储过程
                    List<string> SparaList = new List<string>();
                    SparaList.Add("@P_IDS:" + item.Key);
                    SparaList.Add("@P_CinvCode:" + "");
                    SparaList.Add("@P_Position:" + "");
                    SparaList.Add("@P_Qty:" + "0");
                    SparaList.Add("@P_BZ:" + "0");//鎖定
                    SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
                    SparaList.Add("@P_return_Value:" + "");
                    SparaList.Add("@errText:" + "");
                    string[] Result = DBHelp.ExecuteProc("Proc_DateCode_LockorUnLock", SparaList);
                    if (Result.Length == 1)//调用存储过程有错误
                    {
                        msg = msg + Result + ",";
                    }
                    else if (Result[0] == "1")
                    {
                        msg = msg + Result[1] + ",";
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
                msg = Resources.Lang.WMS_Common_Tips_LockSuccess;//锁定成功！
                this.Alert(msg);
            }
            else
            {
                this.Alert(msg);
            }

        }
        catch (Exception err)
        {
            Alert(Resources.Lang.WMS_Common_Tips_LockFailed + err.Message);//锁定失败
        }
        //清空数据集
        SelectCode = null;
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
    }

    public Dictionary<string, string> SelectCode
    {
        set { ViewState["SelectCode"] = value; }
        get { return ViewState["SelectCode"] as Dictionary<string, string>; }
    }
    /// <summary>
    /// 获取选中的行
    /// </summary>
    public void GetSelectedId()
    {
        try
        {
            if (SelectCode == null)
            {
                SelectCode = new Dictionary<string, string>();
            }

            foreach (GridViewRow item in this.grdDateCode_Lock.Rows)
            {
                Control itemFindControl = item.FindControl("chkSelect");
                if (itemFindControl != null && itemFindControl is CheckBox)
                {
                    CheckBox cbo = itemFindControl as CheckBox;


                    //获取ID
                    string ids = this.grdDateCode_Lock.DataKeys[item.RowIndex][0].ToString();


                    //控件选中且集合中不存在添加
                    if (cbo.Enabled && cbo.Checked && !SelectCode.ContainsKey(ids))
                    {
                        SelectCode.Add(ids, this.grdDateCode_Lock.DataKeys[item.RowIndex][0].ToString());
                    }//未选中且集合中存在的移除                    
                    else if (!cbo.Checked && SelectCode.ContainsKey(ids))
                    {
                        SelectCode.Remove(ids);
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

