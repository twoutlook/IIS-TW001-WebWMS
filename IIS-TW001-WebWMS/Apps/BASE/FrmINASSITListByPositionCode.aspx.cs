using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.SqlClient;

/// <summary>
/// 描述: 入库管理-->FrmINASSITList 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:52:03
/// </summary>
public partial class FrmINASSITListByPositionCode : WMSBasePage
{
    #region SQL
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
        IGenericRepository<V_RD_INASSITListQuery> con = new GenericRepository<V_RD_INASSITListQuery>(context);
        var caseList = from p in con.Get()
                       orderby p.cticketcode descending
                       where 1 == 1
                       select p;
        if (!string.IsNullOrEmpty(Request.QueryString["ID"].Trim()))
            caseList = caseList.Where(x => x.cticketcode != null && x.cticketcode.ToString().Equals(Request.QueryString["ID"].Trim()));
        var resoult = caseList.ToList();
        this.grdINASSIT.DataSource = resoult;
        this.grdINASSIT.DataBind();
    }

    public bool CheckData()
    {
        if (this.txtCASNID.Text.Trim().Length > 0)
        {
        }
        if (this.txtCTICKETCODE.Text.Trim().Length > 0)
        {
        }
        if (this.ddlCSTATUS.SelectedValue.Trim().Length > 0)
        {
        }
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
            //if (this.txtDCREATETIMEFrom.Text.IsDate() == false)
            //{
            //    this.Alert(Resources.Lang.FrmALLOCATEList_NoEfftxtDCREATETIMEFrom);//"制单日期项不是有效的日期！"
            //    this.SetFocus(txtDCREATETIMEFrom);
            //    return false;
            //}
        }
        if (this.txtDCREATETIMETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_ToItemEmpty);//到项不允许空！
            this.SetFocus(txtDCREATETIMETo);
            return false;
        }
        if (this.txtDCREATETIMETo.Text.Trim().Length > 0)
        {
            //if (this.txtDCREATETIMETo.Text.IsDate() == false)
            //{
            //    this.Alert(Resources.Lang.FrmALLOCATEList_NoEffToItem);//到项不是有效的日期！
            //    this.SetFocus(txtDCREATETIMETo);
            //    return false;
            //}
        }
        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";

        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmINASSITEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmINASSITListByPositionCode_AddNew + "','INASSIT');return false;"; //新建上架指引单
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('INASSITListByPositionCode');return false;";
    }

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorINASSIT
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<BASE_CARGOSPACE> con = new GenericRepository<BASE_CARGOSPACE>(context);
        try
        {
            for (int i = 0; i < this.grdINASSIT.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)this.grdINASSIT.Rows[i].Cells[0].Controls[1];
                if (chkSelect.Checked)
                {
                    string ids = this.grdINASSIT.DataKeys[i].Values[0].ToString();
                    #region 调用存储过程
                    List<string> SparaList = new List<string>();
                    SparaList.Add("@P_InAssit_Id:" + ids);
                    SparaList.Add("@retVal:" + "");
                    string[] result = DBHelp.ExecuteProc("Proc_DeleteInAssit", SparaList);
                    if (result[0] == "0")
                    {
                        //成功
                        //msg = result[1].ToString();
                    }
                    else
                    {
                        msg = result[0].ToString();
                        break;
                    }
                    #endregion
                }
            }
            if (msg.Length == 0)
                Alert(Resources.Lang.Common_SuccessDel); //删除成功！
            else
                Alert(msg + Resources.Lang.Common_FailDel);//"删除失败！"
        }
        catch (Exception ex)
        {
            Alert(ex.Message + Resources.Lang.Common_FailDel);//",删除失败！"
            throw;
        }
        this.btnSearch_Click(sender, e);
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdINASSIT.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdINASSIT.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdINASSIT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string strKeyID = this.grdINASSIT.DataKeys[e.Row.RowIndex][0].ToString();
            //HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count-2].Controls[0];
            //linkModify.NavigateUrl = "#";
            //this.OpenFloatWinMax(linkModify,BuildRequestPageURL("FrmINASSITEdit.aspx",SysOperation.Modify, strKeyID),"上架指引单","INASSIT");
        }
    }

    /// <summary>
    /// 释放占用储位
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CARGOSPACE> con = new GenericRepository<BASE_CARGOSPACE>(context);
        IGenericRepository<BASE_PART_CARGOSPACE> con1 = new GenericRepository<BASE_PART_CARGOSPACE>(context);
        IGenericRepository<INASN_D> con2 = new GenericRepository<INASN_D>(context);
        try
        {
            //入库通知单明细
            var inasnList = from c in con2.Get()
                            where c.id == (sender as LinkButton).CommandArgument
                            select c;
            string inas = string.Empty;
            foreach (var inasn in inasnList)
            {
                inas += "\"" + inasn.cinvcode + "\",";
            }
            if (inas.Length > 0)
            {
                inas = inas.Substring(0, inas.Length - 1);
                //物料货位关联表
                var partList = from b in con1.Get()
                               where (new string[] { inas }).Contains(b.cpartnumber)
                               select b;
                string parts = string.Empty;
                foreach (var par in partList)
                {
                    parts += "\"" + par.cpositioncode + "\",";
                }
                if (parts.Length > 0)
                {
                    parts = parts.Substring(0, parts.Length - 1);
                    //货位管理
                    var carList = from a in con.Get()
                                  where (new string[] { parts }).Contains(a.cpositioncode)
                                  select a;
                    foreach (var car in carList)
                    {
                        BASE_CARGOSPACE entity = car;
                        entity.cstatus = "2";
                        con.Update(entity);
                        con.Save();
                    }
                }
            }
            Alert(Resources.Lang.FrmINASSITListByPositionCode_RealeaseSuncess); //释放成功!
        }
        catch (Exception ex)
        {

            Alert(Resources.Lang.FrmINASSITListByPositionCode_RealeaseFail + ex.Message); //释放失败!
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    #endregion

  
    
}

