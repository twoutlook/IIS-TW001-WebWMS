using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;

/// <summary>
/// 描述: 储位管理-->FrmBASE_CARGOSPACEList 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-10-09 18:29:23
/// </summary>
public partial class BASE_FrmBASE_CARGOSPACEList1 : WMSBasePage
{

    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
        }

        this.btnFreezePart.Attributes["onclick"] = this.GetPostBackEventReference(this.btnFreezePart) + ";this.disabled=true;";
        this.btnReleasePart.Attributes["onclick"] = this.GetPostBackEventReference(this.btnReleasePart) + ";this.disabled=true;";
        this.btnFreeze.Attributes["onclick"] = this.GetPostBackEventReference(this.btnFreeze) + ";this.disabled=true;";
        this.btnRelease.Attributes["onclick"] = this.GetPostBackEventReference(this.btnRelease) + ";this.disabled=true;";

    }


    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<V_Stock_Base_Cargospace> entity = new GenericRepository<V_Stock_Base_Cargospace>(context);
        var caseList = from p in entity.Get()
                       orderby p.cposition descending
                       where 1 == 1
                       select p;

        if (txtcposition.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cposition) && x.cposition.Contains(txtcposition.Text.Trim()));

        if (txtcpositioncode.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtcpositioncode.Text.Trim()));
        if (txtcwarename.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwarename) && x.cwarename.Contains(txtcwarename.Text.Trim()));
        if (txtcwareid.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwareid) && x.cwareid.Contains(txtcwareid.Text.Trim()));
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("cstatus", "Common_USE"));
        var srcdata = GetGridSourceDataByList(data, flagList);

        AspNetPager1.CustomInfoHTML = " " + Resources.Lang.WMS_Common_Pager_PageCount + ":<b>" + "</b>";//总页数
        grdBASE_CARGOSPACE.DataSource = srcdata;//GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdBASE_CARGOSPACE.DataBind();
    }

    public bool CheckData()
    {
        if (this.txtcposition.Text.Trim().Length > 0)
        {
        }

        if (this.txtCALIAS.Text.Trim().Length > 0)
        {
        }
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtCTYPE.Text.Trim().Length > 0)
        {
        }
        if (this.txtDEXPIREDATEFrom.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate(this.txtDEXPIREDATEFrom.Text) == false)
            {
                this.Alert(Resources.Lang.FrmBASE_CARGOSPACEList1_ZhongZhiDateError);//终止日期项不是有效的日期！
                this.SetFocus(txtDEXPIREDATEFrom);
                return false;
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
        this.grdBASE_CARGOSPACE.DataKeyNames = new string[] { "ID" };
        //本页面打开新增窗口
        this.btnDateCode.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmStock_DateCodeLock.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmBASE_CARGOSPACEList1_DatecodeLock + "','DateCode_LockorUN');return false;";//DateCode锁定
        this.btn_SN.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmStock_SN_Lock.aspx", SYSOperation.New, "") + "','" + Resources.Lang.WMS_Common_Button_SNLock + "','SN_LockorUn');return false;";//SN冻结
        //判断是否有权限锁定库存信息
        if (Stock_Checked.CheckPoint(WmsWebUserInfo.GetCurrentUser().UserNo, "6023"))
        {
            this.btnDateCode.Enabled = true;
        }
        //判断是否有SN冻结权限 暂不使用
        if (Stock_Checked.CheckPoint(WmsWebUserInfo.GetCurrentUser().UserNo, "6027"))
        {
            this.btn_SN.Visible = true;
        }
        else
        {
            this.btn_SN.Visible = false;
        }
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


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CARGOSPACE> con = new GenericRepository<BASE_CARGOSPACE>(context);
        try
        {
            for (int i = 0; i < this.grdBASE_CARGOSPACE.Rows.Count; i++)
            {
                if (this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {

                    CheckBox chkSelect = (CheckBox)this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        con.Delete(this.grdBASE_CARGOSPACE.DataKeys[i].Values[0].ToString());
                        con.Save();	//执行动作 
                    }
                }
            }
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteSuccess);//删除成功！
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
        for (int i = 0; i < this.grdBASE_CARGOSPACE.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBASE_CARGOSPACE.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdBASE_CARGOSPACE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }

    }

    protected void dsGrdBASE_CARGOSPACE_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < this.grdBASE_CARGOSPACE.Rows.Count; i++)
            {
                if (this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        IGenericRepository<BASE_CARGOSPACE> con = new GenericRepository<BASE_CARGOSPACE>(context);
                        var caseList = from p in con.Get()
                                       where p.id == this.grdBASE_CARGOSPACE.DataKeys[i].Values[0].ToString()
                                       select p;
                        BASE_CARGOSPACE entity = caseList.ToList().FirstOrDefault<BASE_CARGOSPACE>();
                        if (caseList.Count() > 0)
                        {

                            if (entity.cstatus == "1")//冻结的记录为非冻结
                            {
                                entity.cstatus = entity.lastcstatus;
                                entity.lastcstatus = "1";
                            }
                            else//非冻结->冻结
                            {
                                entity.lastcstatus = entity.cstatus;
                                entity.cstatus = "1";
                            }
                            con.Update(entity);
                            con.Save();
                        }

                    }
                }
            }
            this.Alert(Resources.Lang.WMS_Common_Msg_ReleaseSuccess);//释放成功!
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + E.Message.ToJsString());//删除失败！
        }
    }

    //全部冻结
    protected void btnFreeze_Click(object sender, EventArgs e)
    {
        try
        {
            //优化上面的逻辑通过在脚本里处理数据
            Base_Cargospace bc = new Base_Cargospace();
            bc.UpdateBASE_CARGOSPACE_Status("1", txtcpositioncode.Text.Trim(), txtcposition.Text.Trim(), txtcwarename.Text.Trim(), txtcwareid.Text.Trim());
            this.Alert(Resources.Lang.WMS_Common_Msg_LockSuccess);//冻结成功！
            GridBind();
        }
        catch (Exception ex)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_LockFailed + ex.Message.ToJsString());//冻结失败！
        }
        finally
        {
            btnFreeze.Style.Remove("disabled");
        }
    }

    //全部释放
    protected void btnRelease_Click(object sender, EventArgs e)
    {
        try
        {
            //优化上面的逻辑通过在脚本里处理数据
            Base_Cargospace bc = new Base_Cargospace();
            bc.UpdateBASE_CARGOSPACE_Status("0", txtcpositioncode.Text.Trim(), txtcposition.Text.Trim(), txtcwarename.Text.Trim(), txtcwareid.Text.Trim());
            this.Alert(Resources.Lang.WMS_Common_Msg_ReleaseSuccess);//释放成功!
            GridBind();
        }
        catch (Exception ex)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_ReleaseFailed + ex.Message.ToJsString());//释放失败！
        }
        finally
        {
            btnRelease.Style.Remove("disabled");
        }
    }
    //选择冻结
    protected void btnFreezePart_Click(object sender, EventArgs e)
    {
        try
        {
            int j = 0;
            for (int i = 0; i < this.grdBASE_CARGOSPACE.Rows.Count; i++)
            {
                if (this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        j++;
                        IGenericRepository<BASE_CARGOSPACE> con = new GenericRepository<BASE_CARGOSPACE>(context);
                        var caseList = from p in con.Get().AsEnumerable()
                                       where p.id == this.grdBASE_CARGOSPACE.DataKeys[i].Values[0].ToString()
                                       select p;
                        BASE_CARGOSPACE entity = caseList.ToList().FirstOrDefault<BASE_CARGOSPACE>();
                        if (caseList.Count() > 0)
                        {
                            if (entity.cstatus != "1")//非冻结的进行冻结，已经冻结的状态不做调整
                            {
                                entity.lastcstatus = entity.cstatus; //记录冻结前的状态
                                entity.cstatus = "1"; //设置状态为冻结
                                con.Update(entity);
                                con.Save();
                            }
                        }

                    }
                }
            }
            if (j > 0)
            {
                this.Alert(Resources.Lang.WMS_Common_Msg_LockSuccess);//冻结成功！
                this.GridBind();
            }
            else
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_NeedCheckData);//请勾选数据！
            }

        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + E.Message.ToJsString());//删除失败！
        }
        finally
        {
            btnFreezePart.Style.Remove("disabled");
        }
    }

    //选择释放
    protected void btnReleasePart_Click(object sender, EventArgs e)
    {
        try
        {
            int j = 0;
            for (int i = 0; i < this.grdBASE_CARGOSPACE.Rows.Count; i++)
            {
                if (this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        j++;
                        IGenericRepository<BASE_CARGOSPACE> con = new GenericRepository<BASE_CARGOSPACE>(context);
                        var caseList = from p in con.Get().AsEnumerable()
                                       where p.id == this.grdBASE_CARGOSPACE.DataKeys[i].Values[0].ToString()
                                       select p;
                        BASE_CARGOSPACE entity = caseList.ToList().FirstOrDefault<BASE_CARGOSPACE>();
                        if (caseList.Count() > 0)
                        {
                            if (entity.cstatus == "1")//冻结的状态恢复为原先状态，非冻结的状态不变
                            {
                                entity.cstatus = entity.lastcstatus;
                                con.Update(entity);
                                con.Save();
                            }
                        }

                    }
                }
            }
            if (j > 0)
            {
                this.Alert(Resources.Lang.WMS_Common_Msg_ReleaseSuccess);//释放成功!
                this.GridBind();
            }
            else
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_NeedCheckData);//请勾选数据！
            }

        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + E.Message.ToJsString());//删除失败！
        }
        finally
        {
            btnReleasePart.Style.Remove("disabled");
        }
    }
}

