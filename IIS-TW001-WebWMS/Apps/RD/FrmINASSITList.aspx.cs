using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business;
using DreamTek.ASRS.Business.SP.ProcedureModel;

/// <summary>
/// 描述: 入库管理-->FrmINASSITList 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:52:03
/// </summary>
public partial class RD_FrmINASSITList : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                this.InitPage();
                //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);

                //this.txtDCREATETIMEFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                //this.txtDCREATETIMETo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        catch (Exception ex)
        {
        }
    }
    

    #region IPageGrid 成员

    public void GridBind()
    {
        //RD_FrmINASSITListQuery listQuery = new RD_FrmINASSITListQuery();
        //this.CurrendIndex = 1;
        int count = 0;
        
        DataTable dtSource = INASSIT_DComRule.GetList(txtCASNID.Text, txtCTICKETCODE.Text, ddlCSTATUS.SelectedValue, ddlInType.SelectedValue, 
                                                        txtCCREATEOWNERCODE.Text, txtDCREATETIMEFrom.Text, txtDCREATETIMETo.Text, txtCinvCode.Text.Trim(),
                                                        true, this.CurrendIndex, this.PageSize, out count, WmsWebUserInfo.GetCurrentUser().UserNo);

        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
        AspNetPager1.RecordCount = count;
        AspNetPager1.PageSize = this.PageSize;
        this.grdINASSIT.DataSource = dtSource;
        this.grdINASSIT.DataBind();
    }

    public bool CheckData()
    {
        if(this.txtCASNID.Text.Trim().Length > 0)
        {
        }
        if(this.txtCTICKETCODE.Text.Trim().Length > 0)
        {
        }
        if (this.ddlCSTATUS.SelectedValue.Trim().Length > 0)
        {
        }
        if(this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
        }
        if(this.txtDCREATETIMEFrom.Text.Trim().Length > 0)
        {
        	if(this.txtDCREATETIMEFrom.Text.IsDate()== false)
        	{
                //制单日期项不是有效的日期
                this.Alert(Resources.Lang.FrmINASSITList_MSG5+ "！");
        		this.SetFocus(txtDCREATETIMEFrom);
        		return false;
        	}
        }
        if(this.txtDCREATETIMETo.Text.Trim() == "")
        {
            //到项不允许空
            this.Alert(Resources.Lang.FrmINASSITList_MSG6 + "！");
        	this.SetFocus(txtDCREATETIMETo);
        	return false;
        }
        if(this.txtDCREATETIMETo.Text.Trim().Length > 0)
        {
        	if(this.txtDCREATETIMETo.Text.IsDate()== false)
        	{
                //到项不是有效的日期
                this.Alert(Resources.Lang.FrmINASSITList_MSG7 + "！");
        		this.SetFocus(txtDCREATETIMETo);
        		return false;
        	}
        }
        return true;

    }

    #endregion

    #region IPage 成员
   
    public void InitPage()
    {
        //新建上架指引单
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmINASSITEdit.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmINASSITList_MSG8  +"','INASSIT',850,470);return false;";

        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
       // this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
       // this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        //this.grdINASSIT.DataKeyNames = new string[] { "ID,itype,CDEFINE1,CDEFINE2" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //ddlCSTATUS
        Help.DropDownListDataBind(GetInType(true), this.ddlInType, Resources.Lang.Common_ALL , "FUNCNAME", "EXTEND1", "");
        Help.DropDownListDataBind(GetParametersByFlagType("ASSIT"), this.ddlCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");
        //MonthOrWeek
        Help.RadioButtonDataBind(GetParametersByFlagType("MonthOrWeek"), this.rbtList, "", "FLAG_NAME", "FLAG_ID", "");
    }

    #endregion

    protected DataTable grdNavigatorINASSIT_GetExportToExcelSource()
    {
       // RD_FrmINASSITListQuery listQuery = new RD_FrmINASSITListQuery();
        int count = 0;
        this.CurrendIndex = 1;
        DataTable dtSource = INASSIT_DComRule.GetList(this.txtCASNID.Text, this.txtCTICKETCODE.Text, this.ddlCSTATUS.SelectedValue,
            this.ddlInType.SelectedValue, this.txtCCREATEOWNERCODE.Text, this.txtDCREATETIMEFrom.Text, this.txtDCREATETIMETo.Text, txtCinvCode.Text.Trim(), false, CurrendIndex, this.PageSize, out count, WmsWebUserInfo.GetCurrentUser().UserNo);
        return dtSource;
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void grdINASSIT_PageIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.GridBind();
        }
        catch (Exception)
        {
        }
    }    
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsFirstPage)//判断是否是首页
            {
                this.CurrendIndex = 1;
                //重新设置GridNavigator的RowCount
                //RD_FrmINASSITListQuery listQuery = new RD_FrmINASSITListQuery();
                //int count = 0;

                //DataTable dtRowCount = INASSIT_DComRule.GetList(txtCASNID.Text, txtCTICKETCODE.Text, ddlCSTATUS.SelectedValue, this.ddlInType.SelectedValue, txtCCREATEOWNERCODE.Text,
                //                                       txtDCREATETIMEFrom.Text, txtDCREATETIMETo.Text, txtCinvCode.Text.Trim(), true, CurrendIndex, this.PageSize, out count, WmsWebUserInfo.GetCurrentUser().UserNo);
                AspNetPager1.CurrentPageIndex = 1;
            }
            this.GridBind();
            IsFirstPage = true;//恢复默认值
        }
        catch (Exception)
        {
        }
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorINASSIT
    }
    
    protected void btnDelete_Click(object sender,EventArgs e)
    {
        string msg = string.Empty;
        IGenericRepository<INASSIT> con = new GenericRepository<INASSIT>(context);
    	try 
    	{ 
    	    for (int i = 0; i < this.grdINASSIT.Rows.Count; i++) 
    	    { 
    	        if (this.grdINASSIT.Rows[i].Cells[0].Controls[1] is CheckBox) 
    	        { 
    	            CheckBox chkSelect = (CheckBox)this.grdINASSIT.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        if (this.grdINASSIT.Rows[i].Cells[this.grdINASSIT.Rows[i].Cells.Count - 3].Text == "未处理")
                        {
                            string ID = this.grdINASSIT.DataKeys[i].Values[0].ToString();
                            //Delete_IassitID(ID);

                            //con.Delete(ID);
                            //con.Save();


                            Proc_DeleteInAssit proc = new Proc_DeleteInAssit();
                            proc.P_InAssit_Id = ID;
                            proc.Execute();
                           var P_ErrText = proc.INFOTEXT;
                           var P_Return_Value = proc.P_ReturnValue;

                           if (P_Return_Value == 1)
                           {
                               msg = P_ErrText;
                               break;
                           }
                            
                        }
                        else
                        {
                            //只有状态为[未處理]的单据才能删除
                            msg = Resources.Lang.FrmINASSITList_MSG9 + ".";
                            break;
                        }
                    }
    	        } 
    	    }
            if (msg.Length == 0)
            {
                //删除成功
                msg = Resources.Lang.CommonB_RemoveSuccess + "！";
            }            
    	} 
    	catch(Exception Ex) 
    	{
            //删除失败
            msg += Resources.Lang.CommonB_RemoveFailed + "![" + Ex.Message + "]";
    	    //this.Alert("删除失败！" + E.Message.ToJsString()); 
    	}
        this.btnSearch_Click(sender, e);
        this.Alert(msg); 
    }

    private void Delete_IassitID(string ID)
    {
        IGenericRepository<INASSIT_D> cond = new GenericRepository<INASSIT_D>(context);
        string strSQL = string.Format(@"SELECT ids FROM dbo.INASSIT_D WHERE id = '{0}'", ID);
        DataTable dt_d = DBHelp.ExecuteToDataTable(strSQL);
        if (dt_d != null && dt_d.Rows.Count > 0)
        {
            foreach (DataRow item in dt_d.Rows)
            {
                cond.Delete(item[0].ToString());
                cond.Save();
            }
        }
    }
    


    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        try
        {
            for (int i = 0; i < this.grdINASSIT.DataKeyNames.Length; i++)
            {
                strKeyId += this.grdINASSIT.DataKeys[rowIndex].Values[i].ToString() + ",";
            }
            strKeyId = strKeyId.TrimEnd(',');
        }
        catch (Exception)
        {
        }
        return strKeyId;
    }    

    protected void grdINASSIT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strKeyID = this.grdINASSIT.DataKeys[e.Row.RowIndex][0].ToString();
                HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
                linkModify.NavigateUrl = "#";
                this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmINASSITEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmINASSITList_Title1, "INASSIT");

                if (Convert.ToInt32(this.grdINASSIT.DataKeys[e.Row.RowIndex][1]) == 18)
                {
                    if (this.grdINASSIT.DataKeys[e.Row.RowIndex][2].ToString().Equals("0615") || this.grdINASSIT.DataKeys[e.Row.RowIndex][2].ToString().Equals("0654"))
                    {
                        e.Row.Cells[6].Text = Resources.Lang.Common_YES;// +"是";
                    }
                    else
                    {
                        e.Row.Cells[6].Text = Resources.Lang.Common_NO;//+ "否";
                    }
                }
                else
                {
                    e.Row.Cells[6].Text = Resources.Lang.Common_NO;//+ "否";
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void dsGrdINASSIT_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        try
        {
            if (this.IsPostBack == false)
            {
                e.Cancel = true;
            }
        }
        catch (Exception)
        {
            
        }     
    }   
    
   

    /// <summary>
    /// 释放占用储位
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            //if (INASSITRule.UpdateBase_CargosPaceStatus((sender as LinkButton).CommandArgument, "2"))
            //{
            //    Alert(Resources.Lang.FrmINASSITList_MSG5 + "释放成功!");
            //}
            //else
            //{
            //    Alert(Resources.Lang.FrmINASSITList_MSG5 + "释放失败!");
            //}
        }
        catch (Exception)
        {
            
        }
    }
}

