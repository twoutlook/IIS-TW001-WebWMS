using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business.Stock;

/// <summary>
/// 描述: 11-->FrmSTOCK_ADJUSTEdit 页面后台类文件
/// 作者: --chenlei
/// 创建于: 2012-11-02 13:22:44
/// </summary>
public partial class STOCK_FrmSTOCK_ADJUSTEdit : WMSBasePage
{   
    protected void Page_Load(object sender, EventArgs e)
    {
         if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() == SYSOperation.Modify || this.Operation() == SYSOperation.View)
            {
                ShowData();            
            }
            else if (this.Operation() == SYSOperation.Preserved1)
            {
                ShowData();
                //this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASN_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID + "','','INASN_D',600,500);");
                this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("Frm_STOCKINFO_Adiust.aspx", SYSOperation.New, this.KeyID) + "&Flag=1','新建库存调整单明细','STOCK_ADJUST_D');"); 
            }           
            else
            {
                this.txtID.Text = Guid.NewGuid().ToString();
                this.txtCticketCode.Text = new Fun_CreateNo_Wms().CreateNo("StockCurrentAdjust");
                this.txtcreateowner.Text = OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);//WmsWebUserInfo.GetCurrentUser().UserNo //BUCKINGHA-894
                this.txtcreatetime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //txtID.Text = Request.QueryString["parentId"];
                ddlCSTATUS.Enabled = false;
            }        
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
         this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
         this.btnDelete.Attributes["onclick"] = this.GetPostBackEventReference(this.btnDelete) + ";this.disabled=true;";
      
    }    

    #region IPage 成员

    /// <summary>
    /// 初始化页面。主要做一下动作
    ///1、DropDownList,ListBox数据装载,
    ///2、新增按钮、删除的按钮的Java脚本注册等
    ///一般在PageLoad 事件调用，
    ///并且IsPostBack = false时
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('STOCK_ADJUST');return false;"; //BUCKINGHA-894      
      
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "STOCK_CURRENT_ADJUST", false, -1, -1), this.ddlCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");   

        this.grdSTOCK_ADJUST_D.DataKeyNames = new string[] { "IDS" }; 
        //设置保存按钮的文字及其状态
        if(this.Operation() == SYSOperation.View)
        {
            this.btnSave.Enabled = false;
            this.ddlCSTATUS.Enabled = false;
            this.txtcreateowner.Enabled = false;
            this.txtcreatetime.Enabled = false;
            this.txtreason.Enabled = false;
            this.btnNew.Enabled = false;
            this.btnDelete.Enabled = false;
        }
       
    }

    #endregion

    #region IPageGrid 成员

    public void GridBind()
    {
        //STOCK_FrmSTOCK_ADJUST_DListQuery listQuery = new STOCK_FrmSTOCK_ADJUST_DListQuery();
        //DataTable dtSource = listQuery.GetList(txtID.Text, false, this.grdNavigatorSTOCK_ADJUST_D.CurrentPageIndex, this.grdSTOCK_ADJUST_D.PageSize);

        //以下 29-10-2020 by Qamar
        int pageCount = 0;
        StockQuery listQuery = new StockQuery();
        DataTable dtSource;
        if (txtRANK_FINAL.Text.Trim().Length == 1)
            dtSource = listQuery.GetStockAdjustDQueryListIncludingRank(txtID.Text.Trim(), txtCinvcode.Text.Trim(), txtRANK_FINAL.Text.Trim().ToUpper(), CurrendIndex, PageSize, out pageCount);
        else
            dtSource = listQuery.GetStockAdjustDQueryList(txtID.Text.Trim(), txtCinvcode.Text.Trim(), CurrendIndex, PageSize, out pageCount);
        dtSource = GetGridSourceData_PART_RANK(dtSource);
        //以上 29-10-2020 by Qamar

        AspNetPager1.CustomInfoHTML = " 总页数:<b>" + "</b>";
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;
        this.grdSTOCK_ADJUST_D.DataSource = dtSource;
        this.grdSTOCK_ADJUST_D.DataBind();
    }

    //public bool CheckData()
    //{
    //    if (this.txtID.Text.Trim().Length > 0)
    //    {
    //    }
    //    return true;

    //}

    #endregion
    /// <summary>
    /// 状态下拉事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ddlCSTATUS_SelectdChanged(object sender, EventArgs e)
    {
        if (ddlCSTATUS.SelectedValue.Trim() == "1")
        {
            btnSave.Enabled = false;
        }
        else
        {
            btnSave.Enabled = true;
        }
    }
    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
	{
        IGenericRepository<STOCK_CURRENT_ADJUST> conn = new GenericRepository<STOCK_CURRENT_ADJUST>(db);
        STOCK_CURRENT_ADJUST entity = new STOCK_CURRENT_ADJUST();
        entity = (from p in conn.Get()
                  where p.id == this.KeyID
                  select p
                     ).ToList().FirstOrDefault<STOCK_CURRENT_ADJUST>();
        if (entity != null && !entity.id.IsNullOrEmpty())
        {
            txtID.Text = entity.id;
            this.ddlCSTATUS.SelectedValue = entity.cstatus;
            this.txtCticketCode.Text = entity.cticketcode;
            this.txtcreateowner.Text = OPERATOR.GetUserNameByAccountID(entity.createowner);//entity.createowner //BUCKINGHA-894
            this.txtcreatetime.Text = entity.createtime.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtReviewUser.Text = OPERATOR.GetUserNameByAccountID(entity.reviewuser);//BUCKINGHA-894
            if (entity.reviewtime != null)
                this.txtReviewDate.Text = DateTime.Parse(entity.reviewtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            this.txtreason.Text = entity.reason;
        }
        if (ddlCSTATUS.SelectedValue.Trim() != "0")
        {
            btnNew.Enabled = false;
            btnDelete.Enabled = false;
        }
        else
        {
            ddlCSTATUS.Enabled = false;
        }
        if (ddlCSTATUS.SelectedValue.Trim() == "1") //已确认状态只能修改到未处理，所以这里要删除下拉的已审核
        {          
            btnSave.Enabled = false;           
            if(ddlCSTATUS.Items.Count == 3)
               ddlCSTATUS.Items.RemoveAt(2);
        }     
	}

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        //
        if (this.txtcreateowner.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmSTOCK_ADJUSTEdit_TiaoZhengRenBuNengKong);//调整人项不允许为空！
            this.SetFocus(txtcreateowner);
            return false;
        }
        if (this.txtcreateowner.Text.Trim().Length > 0)
        {
            if (OPERATOR.GetUserIDByUserName(this.txtcreateowner.Text).GetLengthByByte() > 50)//BUCKINGHA-894
            {
                this.Alert(Resources.Lang.FrmSTOCK_ADJUSTEdit_TiaoZhengRenLength);//调整人项超过指定的长度50！
                this.SetFocus(txtcreateowner);
                return false;
            }
        }
        if (this.txtcreatetime.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmSTOCK_ADJUSTEdit_TiaoZhengRiQi);//调整日期项不允许为空！
            this.SetFocus(txtcreatetime);
            return false;
        }
       
        if (this.txtreason.Text.Trim().Length == 0)
        {
            this.Alert(Resources.Lang.FrmSTOCK_ADJUSTEdit_TiaoZhengYuanYin);//调整原因项不允许为空！
            this.SetFocus(txtreason);
            return false;
        }
        
        if (this.txtreason.Text.Trim().Length > 0)
        {
            if (this.txtreason.Text.GetLengthByByte() > 200)
            {
                this.Alert(Resources.Lang.FrmSTOCK_ADJUSTEdit_TiaoZhengYuanYinLength);//调整原因项超过指定的长度200！
                this.SetFocus(txtreason);
                return false;
            }
        }
        if (this.txtReviewUser.Text.Trim().Length > 0)
        {
            if (OPERATOR.GetUserIDByUserName(this.txtReviewUser.Text).GetLengthByByte() > 50) //BUCKINGHA-894
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_ShenHeRenChangDu);//审核人项超过指定的长度50！
                this.SetFocus(txtReviewUser);
                return false;
            }
        }       
        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public STOCK_CURRENT_ADJUST SendData()
    {
        STOCK_CURRENT_ADJUST entity = new STOCK_CURRENT_ADJUST();
        if(!string.IsNullOrEmpty(txtCticketCode.Text.Trim()))
        {
            entity.cticketcode = txtCticketCode.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txtcreateowner.Text.Trim()))
        {
            entity.createowner = OPERATOR.GetUserIDByUserName(txtcreateowner.Text.Trim()); ////BUCKINGHA-894
        }
        if (!string.IsNullOrEmpty(txtcreatetime.Text.Trim()))
        {
            entity.createtime = txtcreatetime.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        if (!string.IsNullOrEmpty(txtReviewUser.Text.Trim()))
        {
            entity.reviewuser = OPERATOR.GetUserIDByUserName(txtReviewUser.Text.Trim());  //BUCKINGHA-894
        }
        if (!string.IsNullOrEmpty(txtReviewDate.Text.Trim()))
        {
            entity.reviewtime = txtReviewDate.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        if (!string.IsNullOrEmpty(txtreason.Text.Trim()))
        {
            entity.reason = txtreason.Text.Trim();
        }
        if (this.Operation() == SYSOperation.Modify)
        {
            entity.lastupdatetime = DateTime.Now;
            entity.lastupdateuser = WmsWebUserInfo.GetCurrentUser().UserNo;
        }
        entity.cstatus = ddlCSTATUS.SelectedValue;
        //entity.SetDBNull("DAUDITTIME", true);    
        
        return entity;

    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        this.GridBind();
    }
    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.btnSave.Enabled = false;
        SaveDataToDB(sender);
        this.btnSave.Enabled = true;
    }
    /// <summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch1_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        this.GridBind();
    }
    private void SaveDataToDB(object sender)
    {
        IGenericRepository<STOCK_CURRENT_ADJUST> conn = new GenericRepository<STOCK_CURRENT_ADJUST>(db);
        string msg = string.Empty;
        bool isSuccess = false;//是否保存成功
        if (this.CheckData())
        {          
            STOCK_CURRENT_ADJUST entity = (STOCK_CURRENT_ADJUST)this.SendData();      
                    
            try
            {
                if (this.Operation() == SYSOperation.Modify)
                {                   
                    KeyID = txtID.Text.Trim();
                    entity.id = txtID.Text.Trim();
                    conn.Update(entity);
                    conn.Save();
                    msg = Resources.Lang.WMS_Common_Msg_SaveSuccess;//保存成功!
                    isSuccess = true;
                }               
                else if (this.Operation() == SYSOperation.New)
                {
                    KeyID = Guid.NewGuid().ToString();
                    entity.id = KeyID;                    
                    try
                    {
                        //保存前判断单据号是否已存在 BUCKINGHA-651
                        StockQuery query = new StockQuery();
                        if (query.IsExistsAdjCticketcode(entity.cticketcode.Trim()))
                        {
                            entity.cticketcode = new Fun_CreateNo_Wms().CreateNo("StockCurrentAdjust");
                        }   
                        conn.Insert(entity);
                        conn.Save();
                        msg = Resources.Lang.WMS_Common_Msg_SaveSuccess;//保存成功!
                        isSuccess = true;
                    }
                    catch (Exception)
                    {
                        msg = Resources.Lang.WMS_Common_Msg_SaveFailed;//保存失败!
                    }
                }
                else if (this.Operation() == SYSOperation.Preserved1)
                {
                    KeyID = txtID.Text.Trim();
                    msg = Resources.Lang.WMS_Common_Msg_SaveSuccess;//保存成功!
                    isSuccess = true;
                }
                if (isSuccess)
                {
                    if ((sender as Button).ID == "btnNew")
                    {
                        Response.Redirect(BuildRequestPageURL("FrmSTOCK_ADJUSTEdit.aspx", SYSOperation.Preserved1, KeyID));
                    }
                    else
                    {
                        this.AlertAndBack("FrmSTOCK_ADJUSTEdit.aspx?" + BuildQueryString(SYSOperation.Modify, KeyID), msg);
                    }
                }
                else
                {
                    Alert(msg);
                }
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.WMS_Common_Msg_Failed + E.Message);//失败！
#if Debug 
                this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;        
        this.GridBind();
    }  

    protected void btnDelete_Click(object sender, EventArgs e)
    {      
        btnDelete.Enabled = false;
        try
        {
            for (int i = 0; i < this.grdSTOCK_ADJUST_D.Rows.Count; i++)
            {
                if (this.grdSTOCK_ADJUST_D.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_ADJUST_D.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        IGenericRepository<STOCK_CURRENT_ADJUST_D> conn = new GenericRepository<STOCK_CURRENT_ADJUST_D>(db);
                        STOCK_CURRENT_ADJUST_D entity = new STOCK_CURRENT_ADJUST_D();
                        //主键赋值
                        entity.ids = this.grdSTOCK_ADJUST_D.DataKeys[i].Values[0].ToString();
                        if (!string.IsNullOrEmpty(entity.ids))
                        {
                            conn.Delete(entity.ids);
                            conn.Save();
                        }

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
        btnDelete.Enabled = true;
    }
  

    protected void grdSTOCK_ADJUST_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    } 

    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.btnNew.Enabled = false;
        SaveDataToDB(sender);
        this.btnNew.Enabled = true;
   }
}

