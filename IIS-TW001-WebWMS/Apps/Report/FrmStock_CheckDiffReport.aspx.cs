using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq.Dynamic;

/// <summary>
/// 描述: 物理盤點差異報表
/// 作者: --CQ
/// 创建于: 2013-6-25 16:37:01
/// </summary>
public partial class FrmStock_CheckDiffReport : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.txtCheckName.Text = this.GetCheckName();
            //
            if (Session["pbz"] != null)
            {
                Session.Remove("pbz");
            }
        }
    }

    /// 获取盘点计划名称
    /// <summary>
    /// 获取盘点计划名称
    /// </summary>
    /// <returns></returns>
    public string GetCheckName()
    {
        try
        {
            var modPlan = db.STOCK_CHECK_PLAN.Where(x => x.cstatus == "1").FirstOrDefault();
            if (modPlan != null)
            {
                return modPlan.plan_name;
            }
            else
            {
                return "";
            }
        }
        catch (Exception)
        {
            return "";
        }
    }

    #region IPageGrid 成员

    /// 檢查數據
    /// <summary>
    /// 檢查數據
    /// </summary>
    /// <returns></returns>
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
        Help.DropDownListDataBind(GetParametersByFlagType("IsDifference"), this.dplCHECKTYPE, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");

    }

    #endregion

    /// 查詢按鈕
    /// <summary>
    /// 查詢按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckData())
            {
                Session["pbz"] = "1";
                Session["pcticketcode"] = txtCheckCode.Text.Trim().ToUpper();
                Session["pcheckname"] = txtCheckName.Text.Trim();
                Session["pchecktype"] = dplCHECKTYPE.SelectedValue;
                Session["pwareno"] = txtWareNo.Text.Trim().ToUpper();
                Session["pcinvcode"] = txtCinvCode.Text.Trim().ToUpper();
                Session["pOracle"] = txtOracle.Text.Trim();
                if (txtDINDATEFrom.Text.Trim() == "")
                {
                    Session["pdatefrom"] = "";
                }
                else
                {
                    Session["pdatefrom"] = txtDINDATEFrom.Text.Trim().ToDate().ToString();
                }
                if (txtDINDATETo.Text.Trim() == "")
                {
                    Session["pdateto"] = "";
                }
                else
                {
                    Session["pdateto"] = txtDINDATETo.Text.Trim().ToDate().AddDays(1).ToString();
                }
                Session["pcposition"] = txtPositionCode.Text.Trim().Trim();

            }
        }
        catch (Exception er)
        {
            Alert(er.Message);
        }
    }

}

