using System;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System.Linq;
using DreamTek.ASRS.Business.Base;

/// <summary>
/// 描述: 11-->FrmBAR_PALLETEdit 页面后台类文件
/// 作者: --wjw
/// 创建于: 2013-1-03 16:13:00
/// </summary>
public partial class FrmBAR_PALLET_NEdit : WMSBasePage
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        ucShowBarType.SetCompName = txtPTYPENAME.ClientID;
        ucShowBarType.SetORGCode = txtTypeId.ClientID;
        if (this.IsPostBack == false)
        {
            this.InitPage();
            //this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
            if (this.Operation() != SYSOperation.New)
            {
                ShowData();
            }
            else
            {
                //txtCCREATEOWNERCODE.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                //txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BAR_PALLET');return false;";
        // this.grdPallet_D.DataKeyNames = new string[] { "IDS" };
        txtPTYPENAME.Attributes["onclick"] = "Show('" + ucShowBarType.GetDivName + "');";

        //本页面打开新增窗口
        // this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + PageBase.BuildRequestPageURL("FrmBAR_PALLET_DEdit.aspx?IDM=" + this.KeyID, SYSOperation.New, "") + "','關聯棧板和箱','BAR_PALLET_D');return false;";
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        // GridBind();
    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<BAR_PALLET_M> con = new GenericRepository<BAR_PALLET_M>(context);
        IGenericRepository<BAR_TYPE> con1 = new GenericRepository<BAR_TYPE>(context);
        var caseList = from p in con.Get()
                       where p.id == this.KeyID
                       select p;
        BAR_PALLET_M entity = caseList.ToList().FirstOrDefault();

        var caseList1 = from p in con1.Get()
                        where p.id == entity.type_id
                        select p;

        //this.drCSTATUS.SelectedValue = entity.CSTATUS;
      
        this.txtPTYPENAME.Text = caseList1.ToList().FirstOrDefault().typename;
    }

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
        //
        if (this.txtPTYPENAME.Text.Trim() == "")
        {
            this.Alert("栈板类型项不能为空！");
            this.SetFocus(txtPTYPENAME);
            return false;
        }
        //////
        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BAR_PALLET_M SendData()
    {
        BAR_PALLET_M entity = new BAR_PALLET_M ();
        return entity;

    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IGenericRepository<BAR_PALLET_M> con = new GenericRepository<BAR_PALLET_M>(context);
        if (this.CheckData())
        {
            BAR_PALLET_M entity = (BAR_PALLET_M)this.SendData();
            string strKeyID = "";

            int num = 0;
            try
            {
                num = Convert.ToInt32(this.txtNumber.Text.Trim());
                if (this.Operation() == SYSOperation.New)
                {
                    for (int i = 0; i < num; i++)
                    {
                        entity.id = Guid.NewGuid().ToString();
                        entity.palletno = new Fun_CreateNo().CreateNo("BAR_PALLET_M");
                        entity.palletname = entity.palletno;
                        entity.type_id = txtTypeId.Text;
                        entity.createtime = DateTime.Now;
                        entity.createowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                        con.Insert(entity);
                        con.Save();
                    }
                    Alert("保存成功!");
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


    //    }
    #region IPageGrid 成员

    #endregion

    #region IPage 成员

    #endregion
    #endregion
}

