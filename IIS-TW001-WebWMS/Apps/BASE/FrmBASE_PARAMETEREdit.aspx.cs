using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_FrmBASE_PARAMETEREdit : WMSBasePage
{
    #region 页面属性
    /// <summary>
    /// 当前页面的操作
    /// </summary>
    public SYSOperation operation
    {
        get { return (SYSOperation)Enum.Parse(typeof(SYSOperation), this.hiddOperation.Value); }
        set { this.hiddOperation.Value = value.ToString(); }
    }

    /// <summary>
    /// 当前页面的操作
    /// </summary>
    public string GroupGuid
    {
        get { return this.hiddGuid.Value; }
        set { this.hiddGuid.Value = value.ToString(); }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            GetParameters();
            this.InitPage();
            if (this.Operation() == SYSOperation.Modify)
            {
                ShowData();
            }
            else
            {
                this.txtCCREATEOWNERCODE.Text =  OPERATOR.GetUserNameByAccountID(WmsWebUserInfo.GetCurrentUser().UserNo);
                this.txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
    }

    /// <summary>
    /// 获取传入的页面参数
    /// </summary>
    private void GetParameters()
    {
        if (string.IsNullOrEmpty(this.Request.QueryString["ID"]))
        {
            this.GroupGuid = Guid.Empty.ToString();
        }
        else
        {
            this.GroupGuid = this.Request.QueryString["ID"];
        }

        this.operation = this.Operation();      
    }

    /// <summary>
    /// 页面初始化
    /// </summary>
    public void InitPage()
    {
        
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('PARAMETER');return false;";
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "return PopupFloatWin('" + BuildRequestPageURL("FrmBASE_PARAMETER_DEdit.aspx", SYSOperation.New, "&GroupId=" + this.GroupGuid) + "','新增子项','PARAMETER_D',600,500);";//新增子项
    }

    /// <summary>
    /// 分页控件变更页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    /// 查询按钮事件
    /// <summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;//默认第一页
            AspNetPager1.CurrentPageIndex = 1;
        }
        IsFirstPage = true;
        this.GridBind();
    }

    /// 保存输入内容到数据库
    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            btnSave.Enabled = false;
            SaveToDB(sender);
            btnSave.Enabled = true;
            this.btnSave.Style.Remove("disabled");
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }

    /// <summary>
    /// 获取列表对应列id
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <returns></returns>
    private string GetKeyIDS(int rowIndex)
    {
        return this.grdSys_Parameter_D.DataKeys[rowIndex].Values[0].ToString();
    }

    /// <summary>
    /// 列表行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdSys_Parameter_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);

            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];

            this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBASE_PARAMETER_DEdit.aspx", SYSOperation.Modify, strKeyID + "&GroupId=" + this.GroupGuid), "编辑子项信息", "PARAMETER_D", 600, 500);//编辑子项信息
        }
    }

    /// <summary>
    /// 列表数据绑定
    /// </summary>
    public void GridBind()
    {
        using (var modContext = this.context)
        {
            var modGroup = modContext.SYS_PARAMETERGROUP.Where(x=>x.ID == this.GroupGuid).FirstOrDefault();
            if (modGroup != null)
            {
                var queryList = from p in modContext.SYS_PARAMETER
                                orderby p.sortid
                                where p.flag_type == modGroup.FLAG_TYPE
                                select p;

                if (!string.IsNullOrEmpty(txtFlagCode.Text.Trim())) {
                    queryList = queryList.Where(x => x.flag_id == txtFlagCode.Text.Trim());
                }

                AspNetPager1.RecordCount = queryList.Count();
                AspNetPager1.PageSize = this.PageSize;
                this.grdSys_Parameter_D.DataSource = GetPageSize(queryList, PageSize, CurrendIndex).ToList();
                this.grdSys_Parameter_D.DataBind();
            }
        }
    }

    public void ShowData()
    {
        SYS_PARAMETERGROUP entity = context.SYS_PARAMETERGROUP.Where(x => x.ID == this.GroupGuid).FirstOrDefault();
        if (entity != null)
        {

            txtFlagType.Text = entity.FLAG_TYPE;
            txtFlagName.Text = entity.REMARK;
            txtCCREATEOWNERCODE.Text = entity.CREATEUSER;
            txtDCREATETIME.Text = entity.CREATETIME.HasValue ? entity.CREATETIME.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";

            txtFlagType.Enabled = false;
            txtFlagName.Enabled = false;
            btnSave.Enabled = false;

            TabMain0.Visible = true;
            GridBind();
        }
    }

    /// <summary>
    /// 保存数据到数据库
    /// </summary>
    /// <param name="sender"></param>
    private void SaveToDB(object sender)
    {
        bool isError = false;
        if (this.CheckData())
        {
            string msg = string.Empty;
            string strKeyID = "";
            try
            {
                if (this.operation == SYSOperation.New)
                {
                    using (var modContext = this.context)
                    {
                        SYS_PARAMETERGROUP entity = new SYS_PARAMETERGROUP();
                        strKeyID = Guid.NewGuid().ToString();
                        entity.ID = strKeyID;
                        entity.FLAG_TYPE = txtFlagType.Text.Trim();
                        entity.REMARK = txtFlagName.Text.Trim();
                        entity.CREATEUSER = WmsWebUserInfo.GetCurrentUser().UserNo;
                        entity.CREATETIME = DateTime.Now;
                        modContext.SYS_PARAMETERGROUP.Add(entity);
                        int result = modContext.SaveChanges();
                        if (result > 0)
                        {
                            msg = Resources.Lang.WMS_Common_Msg_SaveSuccess;//保存成功!
                        }
                        else
                        {
                            isError = true;
                            msg = Resources.Lang.WMS_Common_Msg_SaveFailed;//保存失败!
                        }
                    }
                }
                if (!isError)
                {
                    this.AlertAndBack("FrmBASE_PARAMETEREdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), msg);
                }
                else
                {
                    Alert(msg);
                }
            }
            catch (Exception ex)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.WMS_Common_Msg_Failed + ex.ToString());//失败！
            }
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (this.txtFlagType.Text.Trim().Length == 0)
        {
            this.Alert("请输入代码组编号！");//请输入代码组编号！
            return false;
        }

        if (context.SYS_PARAMETERGROUP.Any(x=>x.FLAG_TYPE ==this.txtFlagType.Text.Trim()))
        {
            this.Alert("代码组编号已存在！");//代码组编号已存在！
            return false;
        }

        if (this.txtFlagName.Text.Trim().Length == 0)
        {
            this.Alert("请输入代码组名称！");//请输入代码组名称！
            return false;
        }
        return true;
    }

    /// 删除明细
    /// <summary>
    /// 删除明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            for (int i = 0; i < this.grdSys_Parameter_D.Rows.Count; i++)
            {
                if (this.grdSys_Parameter_D.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSys_Parameter_D.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string parameterdId = this.grdSys_Parameter_D.DataKeys[i].Values[0].ToString();
                        //删除明细
                        string strSql = string.Format("delete from SYS_PARAMETER  where id = '{0}'", parameterdId);
                        DBHelp.ExecuteNonQuery(strSql);
                        //删除
                        string strSqld = string.Format("delete from SYS_PARAMETERNAME where FLAG_GUID = '{0}'", parameterdId);
                        DBHelp.ExecuteNonQuery(strSqld);

                    }
                }
            }
            CacheHelper.RemoveAllCache();
            if (msg.Length == 0)
            {
                msg = Resources.Lang.WMS_Common_Msg_DeleteSuccess;
            }
            else
            {
                msg = Resources.Lang.WMS_Common_Msg_DeleteFailed + "\r\n" + msg;
            }
            this.Alert(msg);
            this.GridBind();
        }
        catch (Exception ex)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + ex.Message.ToString());
        }
    }
}