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
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Business.Base;

/// <summary>
/// 描述: 11-->FrmBAR_CARTON_MEdit 页面后台类文件
/// 作者: --wjw
/// 创建于: 2013-1-03 16:13:00
/// </summary>
public partial class FrmBAR_CARTON_NewEdit :WMSBasePage //PageBase, IPageEdit
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        //ucShowBarType.SetCompName = txtPTYPENAME.ClientID;
        //ucShowBarType.SetORGCode = txtTypeId.ClientID;
        if (this.IsPostBack == false)
        {
            this.InitPage();

        }
        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
    }

    public string Status
    {
        get
        {
            if (ViewState["Status"] != null)
            {
                return ViewState["Status"].ToString();
            }
            return "";
        }
        set { ViewState["Status"] = value; }
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BAR_CARTON_N');return false;";
        Help.DropDownListDataBind(new BarQuery().GetBAR_TYPE("1"), ddlTYPE_ID, "", "TYPENAME", "ID", "");
    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        //BAR_CARTON_MEntity entity = new BAR_CARTON_MEntity();
        //entity.ID = this.KeyID;
        //entity.SelectByPKeys();
        //hf_Id.Value = entity.ID;
        //ddlTYPE_ID.SelectedValue = entity.TYPE_ID;

        IGenericRepository<BAR_CARTON_M> con = new GenericRepository<BAR_CARTON_M>(context);
        var caseList = from p in con.Get()
                       where p.id == this.KeyID
                       select p;
        BAR_CARTON_M entity = caseList.ToList().FirstOrDefault();
        hf_Id.Value = entity.id;
        ddlTYPE_ID.SelectedValue = entity.type_id;
    }

    #region IPageGrid 成员



    #endregion

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (this.txtNumber.Text.Trim() == "")
        {
            this.Alert("箱數量项不允许為空！");
            this.SetFocus(txtNumber);
            return false;
        }

        if (this.txtNumber.Text.Trim().Length > 0)
        {
            string strMessageIQUANTITY = this.txtNumber.Text;
            if (strMessageIQUANTITY != "")
            {
                this.Alert("数量项不是有效的十进制数字！" + strMessageIQUANTITY);
                this.SetFocus(txtNumber);
                return false;
            }
        }
        if (Convert.ToDecimal(this.txtNumber.Text.Trim()) <= 0)
        {
            this.Alert("数量要大于0！");
            this.SetFocus(txtNumber);
            return false;
        }
        ////
        if (this.txtNumber.Text.Trim().Length > 0)
        {
            if (this.txtNumber.Text.GetLengthByByte() > 3)
            {
                this.Alert("箱數量项超过指定的长度3！");
                this.SetFocus(txtNumber);
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BAR_CARTON_M SendData()
    {
        BAR_CARTON_M entity = new BAR_CARTON_M();

        // entity.TYPE_ID = ddlTYPE_ID.SelectedValue.Trim();

        return entity;

    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveToDB(sender);
    }

    private void SaveToDB(object sender)
    {
        IGenericRepository<BAR_CARTON_M> con = new GenericRepository<BAR_CARTON_M>(context);
        if (this.CheckData())
        {
            BAR_CARTON_M entity = (BAR_CARTON_M)this.SendData();

            string strKeyID = "";
            int num = 0;

            try
            {
                num = Convert.ToInt32(this.txtNumber.Text.Trim());

                if (this.Operation() == SYSOperation.New)
                {
                    for (int i = 0; i < num; i++)
                    {
                        entity.carton_no = new Fun_CreateNo().CreateNo("BAR_CARTON_M");
                        entity.carton_name = entity.carton_no;
                        strKeyID = Guid.NewGuid().ToString();
                        entity.id = strKeyID;
                        entity.type_id = ddlTYPE_ID.SelectedValue.Trim();
                        entity.createtime = DateTime.Now;
                        entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;

                        con.Insert(entity);
                        con.Save();
                    }
                    Alert("保存成功!");
                    //this.AlertAndBack("FrmBAR_CARTON_NewEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), "保存成功");
                }
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + "失败！" + E.Message);
#if Debug 
        		this.Response.Write(entity.DBAccess().GetLastSQL());                
#endif
            }
        }
    }
    #endregion
    #region oracle
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    //ucShowBarType.SetCompName = txtPTYPENAME.ClientID;
    //    //ucShowBarType.SetORGCode = txtTypeId.ClientID;
    //    if (this.IsPostBack == false)
    //    {
    //        this.InitPage();
           
    //    }
    //    this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
    //    this.HasRight();
    //}

    //public string Status
    //{
    //    get
    //    {
    //        if (ViewState["Status"] != null)
    //        {
    //            return ViewState["Status"].ToString();
    //        }
    //        return "";
    //    }
    //    set { ViewState["Status"] = value; }
    //}

   
    //#region IPage 成员

    ///// <summary>
    ///// 初始化页面。主要做一下动作
    /////1、DropDownList,ListBox数据装载,
    /////2、新增按钮、删除的按钮的Java脚本注册等
    /////一般在PageLoad 事件调用，
    /////并且IsPostBack = false时
    ///// </summary>
    //public void InitPage()
    //{
    //    this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
    //    this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BAR_CARTON_N');return false;";
    //    Help.DropDownListDataBind(new Bar_FrmBAR_TYPEListQuery().GetBAR_TYPE("1"), ddlTYPE_ID, "", "TYPENAME", "ID", "");
    //}

    //#endregion

    ///// <summary>
    ///// 根据主键值，数据库内容填入输入项控件
    ///// </summary>
    //public void ShowData()
    //{
    //    BAR_CARTON_MEntity entity = new BAR_CARTON_MEntity();
    //    entity.ID = this.KeyID;
    //    entity.SelectByPKeys();
    //    hf_Id.Value = entity.ID;
    //    ddlTYPE_ID.SelectedValue = entity.TYPE_ID;
    //}

    //#region IPageGrid 成员



    //#endregion

    ///// <summary>
    ///// 校验数据
    ///// </summary>
    ///// <returns></returns>
    //public bool CheckData()
    //{
    //   if (this.txtNumber.Text.Trim() == "")
    //   {
    //       this.Alert("箱數量项不允许為空！");
    //       this.SetFocus(txtNumber);
    //       return false;
    //   }

    //   if (this.txtNumber.Text.Trim().Length > 0)
    //   {
    //       string strMessageIQUANTITY = this.txtNumber.Text.IsValidNum(10, 2);
    //       if (strMessageIQUANTITY != "")
    //       {
    //           this.Alert("数量项不是有效的十进制数字！" + strMessageIQUANTITY);
    //           this.SetFocus(txtNumber);
    //           return false;
    //       }
    //   }
    //   if (Convert.ToDecimal(this.txtNumber.Text.Trim()) <= 0)
    //   {
    //       this.Alert("数量要大于0！");
    //       this.SetFocus(txtNumber);
    //       return false;
    //   }
    //    ////
    //   if (this.txtNumber.Text.Trim().Length > 0)
    //    {
    //        if (this.txtNumber.Text.GetLengthByByte() > 3)
    //        {
    //            this.Alert("箱數量项超过指定的长度3！");
    //            this.SetFocus(txtNumber);
    //            return false;
    //        }
    //    }
    //    return true;
    //}
    ///// <summary>
    ///// 根据页面上的数据构造相应的实体类返回
    ///// </summary>
    ///// <returns></returns>
    //public BaseEntity SendData()
    //{
    //    BAR_CARTON_MEntity entity = new BAR_CARTON_MEntity();        
      
    //   // entity.TYPE_ID = ddlTYPE_ID.SelectedValue.Trim();
 
    //    return entity;

    //}

    ///// <summary>
    ///// 保存输入内容到数据库
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    SaveToDB(sender);
    //}

    //private void SaveToDB(object sender)
    //{
    //    if (this.CheckData())
    //    {
    //        BAR_CARTON_MEntity entity = (BAR_CARTON_MEntity)this.SendData();
           
    //        string strKeyID = "";
    //        int num = 0;

    //        try
    //        {
    //            num= Convert.ToInt32(this.txtNumber.Text.Trim());

    //             if (this.Operation == SYSOperation.New)
    //            {
    //                for (int i = 0; i < num; i++)
    //                {
    //                    entity.CARTON_NO = new Fun_CreateNo().CreateNo("BAR_CARTON_M");
    //                    entity.CARTON_NAME = entity.CARTON_NO;
    //                    strKeyID = Guid.NewGuid().ToString();
    //                    entity.ID = strKeyID;
    //                    entity.TYPE_ID = ddlTYPE_ID.SelectedValue.Trim();
    //                    entity.CREATETIME = DateTime.Now;
    //                    entity.CREATEOWNER = WmsWebUserInfo.GetCurrentUser().UserNo;
    //                    BAR_CARTON_MRule.Insert(entity);
    //                }
    //                 Alert("保存成功!");
    //                //this.AlertAndBack("FrmBAR_CARTON_NewEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), "保存成功");
    //            }
    //        }
    //        catch (Exception E)
    //        {
    //            this.Alert(this.GetOperationName() + "失败！" + E.Message);
    //            #if Debug 
    //            this.Response.Write(entity.DBAccess().GetLastSQL());                
    //            #endif
    //        }
    //    }
    //}

    #endregion
}

