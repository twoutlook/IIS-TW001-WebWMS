using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Linq;
using DreamTek.ASRS.Business.SP.ProcedureModel;

/// <summary>
/// 描述: 库存Datecode锁定
/// 作者: 
/// 创建于: 2013-9-23 11:28:14
/// </summary>

public partial class FrmStock_SN_LockEdit : WMSBasePage
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
        }
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<view_get_sn_locklist> entity = new GenericRepository<view_get_sn_locklist>(context);
        var caseList = from p in entity.Get()
                       orderby p.datecode ascending
                       where 1 == 1
                       select p;
        if (!string.IsNullOrEmpty(txtCinvcode.Text.Trim()))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cinvcode) && x.cinvcode.Contains(txtCinvcode.Text.Trim().ToUpper()));

        if (txtCpostionCode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCpostionCode.Text.Trim().ToUpper()));
        if (!string.IsNullOrEmpty(txtSN_Begin.Text.Trim()) && string.IsNullOrEmpty(txtSN_End.Text.Trim()))
        {
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.sncode) && x.sncode.Contains(txtSN_Begin.Text.Trim().ToUpper()));
        }
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
            grdSN_Lock.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
            grdSN_Lock.DataBind();
        }
        else
        {
            grdSN_Lock.DataSource = null;
            grdSN_Lock.DataBind();
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;
        }
    }

    public bool CheckData()
    {

        if (txtSN_Begin.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmStock_SN_LockEdit_MSG1);//开始SN不能为空！
            this.SetFocus(txtCinvcode);
            return false;
        }
        else
        {
            if (txtSN_End.Text.Trim() == "" && txtSN_Begin.Text.Trim().Length != 75)
            {
                this.Alert(Resources.Lang.FrmStock_SN_LockEdit_MSG2);//开始SN不为空时，SN位数必须为75位
                this.SetFocus(txtSN_Begin);
                return false;
            }
        }

        if (txtSN_Begin.Text.Trim() != "" && txtSN_End.Text != "")
        {
            //检查SN长度是否是16位或者19位
            if (txtSN_Begin.Text.Trim().Length != 75)
            {
                this.Alert(Resources.Lang.FrmStock_SN_LockEdit_MSG3);//开始SN和结束SN都不为空时，SN位数必须为75位！
                this.SetFocus(txtSN_Begin);
                return false;
            }

            if (txtSN_Begin.Text.Trim().Length != txtSN_End.Text.Trim().Length)
            {
                this.Alert(Resources.Lang.FrmStock_SN_LockEdit_MSG4);//结束SN和开始SN位数不一致！
                this.SetFocus(txtSN_End);
                return false;
            }

            if (txtSN_Begin.Text.Length == 75)
            {
                #region 19位检查后四位

                string strfrom = txtSN_Begin.Text.Substring(0, 71);
                string strto = txtSN_End.Text.Substring(0, 71);
                if (strfrom == strto)
                {
                    //檢查后四位是否是有效的數字
                    int fromQty;
                    int toQty;
                    if (!Stock_SNLock.IsInteger(txtSN_Begin.Text.Substring(71), out fromQty))
                    {
                        Alert(Resources.Lang.FrmStock_SN_LockEdit_MSG5);//开始SN的后四位不是有效的數字,请清空结束SN后查询
                        this.SetFocus(txtSN_Begin);
                        return false;
                    }
                    if (!Stock_SNLock.IsInteger(txtSN_End.Text.Substring(71), out toQty))
                    {
                        Alert(Resources.Lang.FrmStock_SN_LockEdit_MSG6);//结束SN的后四位不是有效的數字，不能使用连续查询
                        this.SetFocus(txtSN_End);
                        return false;
                    }
                    //起始SN需要小於等於結束SN
                    if (fromQty > toQty)
                    {
                        Alert(Resources.Lang.FrmStock_SN_LockEdit_MSG7);//开始SN的后四位數應該小於等於结束SN的后四位數!
                        this.SetFocus(txtSN_Begin);
                        return false;
                    }
                }
                else
                {
                    Alert(Resources.Lang.FrmStock_SN_LockEdit_MSG8);//輸入的开始SN和结束SN除后四位以外需要一致
                    this.SetFocus(txtSN_Begin);
                    return false;
                }

                #endregion
            }
        }
        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //this.grdDateCode_Lock.DataKeyNames = new string[] { "IDS" };

        //本页面打开新增窗口
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('SN_Lock');return false;";
        txtLockDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
        for (int i = 0; i < this.grdSN_Lock.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdSN_Lock.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }
    protected void dsGrdSN_Lock_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    /// 冻结按鈕
    /// <summary>
    /// 冻结按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSNLock_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        if (CheckData())
        {
            try
            {
                #region 调用存储过程
                decimal enable = 0m;
                string resule_value = "";
                string errormsg = "";
                Proc_Lock_Stock_SN proc = new Proc_Lock_Stock_SN();
                proc.P_CinvCode = txtCinvcode.Text.Trim().ToUpper();
                proc.P_PositionCode = txtCpostionCode.Text.Trim().ToUpper();
                proc.P_BeginSN = txtSN_Begin.Text.Trim().ToUpper();
                proc.P_EndSN = txtSN_End.Text.Trim().ToUpper();
                proc.P_LockDate = txtLockDate.Text.Trim();
                proc.P_UnLockDate = "";
                bool a = decimal.TryParse(dpdEnable.SelectedValue.Trim(), out enable);
                proc.P_Enable = enable;
                proc.P_BZ = "0";
                proc.P_UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
                proc.Execute();
                errormsg = proc.errText;
                resule_value = proc.P_return_Value;

                if (resule_value == "1")//调用存储过程有错误
                {
                    msg = Resources.Lang.WMS_Common_Msg_LockFailed + "\r\n" + errormsg;//冻结失败！
                    Alert(msg);
                }
                else if (resule_value == "0")
                {
                    Alert(Resources.Lang.WMS_Common_Msg_LockSuccess);//冻结成功！
                }
                #endregion
            }
            catch (Exception err)
            {

                Alert(err.Message);
            }

            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }

    }
    #endregion

}

