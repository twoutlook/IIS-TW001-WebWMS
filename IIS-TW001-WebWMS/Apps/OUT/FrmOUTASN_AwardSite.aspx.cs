using DreamTek.ASRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_OUT_FrmOUTASN_AwardSite : WMSBasePage
{
    #region 页面属性
    /// <summary>
    /// 当前页面的操作
    /// </summary>
    public string OutMode
    {
        get { return  this.hiddOutMode.Value; }
        set { this.hiddOutMode.Value = value.ToString(); }
    }

    /// <summary>
    /// 当前页面的操作
    /// </summary>
    public string AsnGuid
    {
        get { return this.hiddAsnGuid.Value; }
        set { this.hiddAsnGuid.Value = value.ToString(); }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetParameters();
            this.InitPage();
            LoadData();
        }
    }

    private void GetParameters()
    {
        if (string.IsNullOrEmpty(this.Request.QueryString["ID"]))
        {
            this.AsnGuid = Guid.Empty.ToString();
        }
        else
        {
            this.AsnGuid = this.Request.QueryString["ID"];
        }

        if (string.IsNullOrEmpty(this.Request.QueryString["OutMode"]))
        {
            this.OutMode = "0";
        }
        else
        {
            this.OutMode = this.Request.QueryString["OutMode"];
        }
    }

    /// <summary>
    /// 页面初始化
    /// </summary>
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTASNSITE');return false;";
        rbtList.Items.Clear();
        rbtList.Items.Add(new ListItem("系统推荐", "1"));
        rbtList.Items.Add(new ListItem("指定站点", "2"));
        rbtList.SelectedValue = "1";
    }

    public void LoadData() {
        if (!this.AsnGuid.Equals(Guid.Empty.ToString())) {
            var modOutAsn = context.OUTASN.Where(x => x.id == this.AsnGuid).FirstOrDefault();
            if (modOutAsn != null) {
                txtCticketcode.Text = modOutAsn.cticketcode;

                List<BASE_CRANECONFIG> carList = context.BASE_CRANECONFIG.Where(x => x.PLCType == "CAR" && x.FLAG == "0").ToList();
                drpSites.Items.Clear();
                drpSites.Items.Add(new ListItem("请选择", ""));
                if (carList != null && carList.Any()) {
                    foreach (var item in carList) {
                        drpSites.Items.Add(new ListItem(item.CRANENAME, item.CRANEID));
                    }
                }
            }
        }
    
    }

    /// 保存输入内容到数据库
    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            string carid = "";
            if (rbtList.SelectedValue == "2") {
                if (drpSites.SelectedValue == "")
                {
                    this.drpSites.Focus();
                    this.Alert("请选择出库站点！");
                    return;
                }
                else {
                    carid = drpSites.SelectedValue;
                }
            }

            string procName = "PRC_OUTCONFIRM";
            //判断立库入.出库单位
            var config = context.SYS_CONFIG.Where(x => x.code == "120003").FirstOrDefault();
            if (config != null && config.config_value == "1")
            { //设置为箱时,走博雷逻辑
                procName = "PRC_OUTCONFIRM_BOX";
            }

            //获取通知单号ID  
            string asnid = this.AsnGuid;
            List<string> SparaList = new List<string>();
            SparaList.Add("@P_OUTASNID:" + asnid.Trim());
            SparaList.Add("@P_UserNo:" + WmsWebUserInfo.GetCurrentUser().UserNo);
            SparaList.Add("@P_Type:" + Convert.ToInt32(this.OutMode));
            SparaList.Add("@P_Craneid:" + carid);
            SparaList.Add("@P_return_Value:" + "");
            SparaList.Add("@P_ErrText:" + "");

            string[] result = DBHelp.ExecuteProc(procName, SparaList);

            if (result.Length == 1)//调用存储过程有错误
            {
                this.Alert(result[0]);
            }
            else if (result[0] == "0")
            {
                msg += Resources.Lang.FrmOUTASNList_Tips_ChuKuChengGong;

                string str = "alert('" + StringExtension.ToJsString(msg) + "'); window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTASNSITE');";
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "str", str, true);
            }
            else
            {
                this.Alert(result[1]);
            }
        }
        catch (Exception ex)
        {
            this.Alert(ex.ToString());
        }       
    }


    protected void rbtList_TextChanged(object sender, EventArgs e)
    {
        if (this.rbtList.SelectedValue == "2")
        {
            tdSiteSelect.Visible = true;
        }
        else {
            tdSiteSelect.Visible = false;
        }
    }
}