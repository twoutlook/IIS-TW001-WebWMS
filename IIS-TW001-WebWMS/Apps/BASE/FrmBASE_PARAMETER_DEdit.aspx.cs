using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_BASE_FrmBASE_PARAMETER_DEdit : WMSBasePage
{
    /// <summary>
    /// 规则ID
    /// </summary>
    public string ParameterId
    {
        get { return this.hiddGuid.Value; }
        set { this.hiddGuid.Value = value; }
    }

    public string GroupId {
        get { return this.hiddGroupGuid.Value; }
        set { this.hiddGroupGuid.Value = value; }
    }


    /// <summary>
    /// 当前页面的操作
    /// </summary>
    public SYSOperation operation
    {
        get { return (SYSOperation)Enum.Parse(typeof(SYSOperation), this.hiddOperation.Value); }
        set { this.hiddOperation.Value = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            GetParameters();
            InitPage();
        }
    }

    /// <summary>
    /// 获取传入的页面参数
    /// </summary>
    private void GetParameters()
    {
        if (string.IsNullOrEmpty(this.Request.QueryString["ID"]))
        {
            this.ParameterId = Guid.Empty.ToString();
        }
        else
        {
            this.ParameterId = this.Request.QueryString["ID"];
        }

        if (string.IsNullOrEmpty(this.Request.QueryString["GroupId"]))
        {
            this.GroupId = Guid.Empty.ToString();
        }
        else
        {
            this.GroupId = this.Request.QueryString["GroupId"];
        }

        this.operation = this.Operation(); 
    }

    private void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('PARAMETER_D');return false;";
        
        if (this.Operation() == SYSOperation.New)
        {
            NewData();
        }
        else
        {
            ShowData();
        }
    }

    public void ShowData()
    {
        var modParameter = context.SYS_PARAMETER.Where(x => x.ID == this.ParameterId).FirstOrDefault();
        if (modParameter != null)
        {
            string isNeedStr = "<span class=\"requiredSign\">*</span>";
            //拼接数据列表
            StringBuilder nodeHtml = new StringBuilder();
            nodeHtml.Append("<tr data-itype=\"\">");
            nodeHtml.Append("<td class=\"InputLabel\">" + isNeedStr + "子项编号</td>");
            nodeHtml.Append("<td><input id=\"txtFlagId\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width: 150px;\" value=\"" + modParameter.flag_id + "\" /></td>");
            nodeHtml.Append("</tr>");

            nodeHtml.Append("<tr data-itype=\"\">");
            nodeHtml.Append("<td class=\"InputLabel\">" + isNeedStr + "排序</td>");
            nodeHtml.Append("<td><input id=\"txtSortId\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width: 150px;\" value=\"" + modParameter.sortid + "\" /></td>");
            nodeHtml.Append("</tr>");

            var LanguageList = context.SYS_PARAMETER.Where(x=>x.flag_type == "Language").ToList();
            if (LanguageList != null && LanguageList.Any()) {
                var parameterNames = context.SYS_PARAMETERNAME.Where(x => x.FLAG_GUID == modParameter.ID).ToList();
                foreach (var item in LanguageList) {
                    nodeHtml.Append("<tr data-itype=\"lan\" data-lan=\""+item.flag_id+"\">");
                    nodeHtml.Append("<td class=\"InputLabel\">" + isNeedStr + "名称(" + item.flag_name + ")</td>");
                    var currentName = parameterNames != null ? parameterNames.Where(x => x.LANGUAGEID.ToUpper() == item.flag_id.ToUpper()).FirstOrDefault() : null;
                    string name = currentName != null ? currentName.FLAG_NAME : "";
                    nodeHtml.Append("<td> <input id=\"txtName\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width: 150px;\" value=\"" + name + "\" /></td>");
                    nodeHtml.Append("</tr>");
                }
            }

            tbodyParameters.InnerHtml = nodeHtml.ToString();
        }
    }

    public void NewData()
    {
        string isNeedStr = "<span class=\"requiredSign\">*</span>";
        //拼接数据列表
        StringBuilder nodeHtml = new StringBuilder();
        nodeHtml.Append("<tr data-itype=\"\">");
        nodeHtml.Append("<td class=\"InputLabel\">" + isNeedStr + "子项编号</td>");
        nodeHtml.Append("<td><input id=\"txtFlagId\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width: 150px;\" value=\"\" /></td>");
        nodeHtml.Append("</tr>");

        nodeHtml.Append("<tr data-itype=\"\">");
        nodeHtml.Append("<td class=\"InputLabel\">" + isNeedStr + "排序</td>");
        nodeHtml.Append("<td><input id=\"txtSortId\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width: 150px;\" value=\"\" /></td>");
        nodeHtml.Append("</tr>");

        var LanguageList = context.SYS_PARAMETER.Where(x => x.flag_type == "Language").ToList();
        if (LanguageList != null && LanguageList.Any())
        {
            foreach (var item in LanguageList)
            {
                nodeHtml.Append("<tr data-itype=\"lan\" data-lan=\"" + item.flag_id + "\">");
                nodeHtml.Append("<td class=\"InputLabel\">" + isNeedStr + "名称(" + item.flag_name + ")</td>");
                nodeHtml.Append("<td> <input id=\"txtName\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width: 150px;\" value=\"\" /></td>");
                nodeHtml.Append("</tr>");
            }
        }
        tbodyParameters.InnerHtml = nodeHtml.ToString();
    }

}