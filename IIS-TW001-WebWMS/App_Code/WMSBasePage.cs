using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using System.Data.Entity.SqlServer;
using DreamTek.WMS.DAL.Model.Base;
using DreamTek.WMS.Repository.Base;
using System.Reflection;

public class WMSBasePage : Page
{
    // NOTE by Mark, 09/19 for TAIWAY PART+RANK
    //https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/dataset-datatable-dataview/adding-columns-to-a-datatable
    public DataTable GetGridSourceData_PART_RANK(DataTable srcData)
    {
        srcData.Columns.Add("PART", typeof(String));
        srcData.Columns.Add("RANK_FINAL", typeof(String));
        // var result = new DataTable();
        foreach (DataRow row in srcData.Rows)
        {

            string part = row["CINVCODE"].ToString();

            /*
           Length cannot be less than zero. Parameter name: length
详细错误信息	at System.String.Substring(Int32 startIndex, Int32 length)
at WMSBasePage.GetGridSourceData_PART_RANK(DataTable srcData) in c:\GITHUB\IIS\IIS-TW001\IIS-TW001\App_Code\WMSBasePage.cs:line 36
             */
            try
            {


                row["PART"] = part.Substring(0, part.Length - 2);// 	QBAM0030 => QBAM00
                row["RANK_FINAL"] = part.Substring(part.Length - 1, 1);// 	QBAM0030 => 0
                if (row["RANK_FINAL"].ToString() == "_")
                    row["RANK_FINAL"] = "";
            }
            catch
            {
                row["PART"] = "?????";// 	QBAM0030 => QBAM00
                row["RANK_FINAL"] = "?";
            }
        }
        return srcData;
    }



    public DBContext db
    {
        get
        {
            var efDB = new DBContext();
            efDB.Configuration.LazyLoadingEnabled = true;
            return efDB;
        }
    }

    public DBContext context
    {
        get
        {
            var efDB = new DBContext();
            efDB.Configuration.LazyLoadingEnabled = true;
            return efDB;
        }
    }
    /// <summary>
    /// 系统流程配置参数
    /// </summary>
    public string SYSConfig
    {
        get
        {
            var config = this.GetConFig("120003");
            return config;
        }
    }
    /// <summary>
    /// 记录是否是首页
    /// </summary>
    public bool IsFirstPage
    {
        get
        {
            TextBox txtPageSizeDetail = this.Master.FindControl("txtPageSizeDetail") as TextBox;
            if (txtPageSizeDetail == null)
            {
                return true;
            }
            else
            {
                if (txtPageSizeDetail.Text.Equals("1"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        set
        {
            TextBox txtPageSizeDetail = this.Master.FindControl("txtPageSizeDetail") as TextBox;
            if (txtPageSizeDetail == null)
            {
                value = true;
            }
            else
            {
                if (value == true)
                {
                    txtPageSizeDetail.Text = "0";
                }
                else
                {
                    txtPageSizeDetail.Text = "1";
                }
            }
        }
    }
    /// <summary>
    /// 查询字符中,动作Key名称
    /// </summary>
    public const string ACTION = "Action";

    /// <summary>
    /// 主关键字的Key名称
    /// </summary>
    public const string QID = "ID";

    /// <summary>
    /// 当前页面进行的操作
    /// </summary>
    public SYSOperation Operation()
    {
        if (string.IsNullOrEmpty(Request.QueryString["action"]))
            return SYSOperation.New;
        switch (Request.QueryString["action"])
        {
            case "New":
                return SYSOperation.New;
            case "Modify":
                return SYSOperation.Modify;
            case "View":
                return SYSOperation.View;
            case "Query":
                return SYSOperation.Query;
            case "Delete":
                return SYSOperation.Delete;
            case "Apply":
                return SYSOperation.Apply;
            case "Approve":
                return SYSOperation.Approve;
            case "Reject":
                return SYSOperation.Reject;
            case "SecondAudit":
                return SYSOperation.SecondAudit;
            case "Preserved1":
                return SYSOperation.Preserved1;
            case "Preserved2":
                return SYSOperation.Preserved2;
            case "Preserved3":
                return SYSOperation.Preserved3;
            case "Preserved4":
                return SYSOperation.Preserved4;
            case "Preserved5":
                return SYSOperation.Preserved5;
            default:
                return SYSOperation.Preserved6;

        }
    }
    /// <summary>
    /// 当前页面进行的操作
    /// </summary>
    public string GetOperationName()
    {
        if (string.IsNullOrEmpty(Request.QueryString["action"]))
            return "新增";
        switch (Request.QueryString["action"])
        {
            case "New":
                return "新增";
            case "Modify":
                return "修改";
            case "View":
                return "查看";
            case "Query":
                return "查询";
            case "Delete":
                return "删除";
            case "Apply":
                return "申请";
            case "Approve":
                return "审批";
            case "Reject":
                return "驳回";
            case "SecondAudit":
                return "更高一级审批";
            case "Preserved1":
                return "预留动作1";
            case "Preserved2":
                return "预留动作2";
            case "Preserved3":
                return "预留动作3";
            case "Preserved4":
                return "预留动作4";
            case "Preserved5":
                return "预留动作5";
            default:
                return "预留动作6";

        }
    }


    /// <summary>
    /// 单条数据显示页面的数据的主键编号
    /// </summary>
    public string KeyID
    {
        get
        {

            try
            {
                if (ViewState["ID"] == null)
                {
                    ViewState["ID"] = this.Page.Request.QueryString["ID"];
                }
            }
            catch (Exception innerException)
            {
                throw new Exception("未传递Querystring:QID", innerException);
            }
            return ViewState["ID"].ToString()
                ;
        }
        set
        {
            ViewState["ID"] = value;
        }
    }



    /// <summary>
    /// 正在操作的数据的关键字列表
    /// </summary>
    public string[] KeyIDS
    {
        get
        {
            return this.KeyID.Split(new char[]
                {
                    ','
                });
        }
    }

    /// <summary>
    /// 注册CSS文件在head部分
    /// </summary>
    /// <param name="path">CSS文件路径</param>        
    public void RegisterCssFile(string path)
    {
        //HtmlLink htmlLink = new HtmlLink();
        //htmlLink.Attributes["type"] = "text/css";
        //htmlLink.Attributes["rel"] = "stylesheet";
        //htmlLink.Attributes["href"] = path;
        //base.Header.Controls.Add(htmlLink);
    }


    /// <summary>
    /// 注册脚本
    /// </summary>
    /// <param name="p_Script">JAVA脚本</param>
    public void WriteScript(string p_Script)
    {
        string script = "<Script Language=\"javascript\">\n" + p_Script + "</Script>";
        if (ScriptManager.GetCurrent(this.Page) == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), script);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script, false);
    }


    public string GetRelativeURL(string url)
    {
        if (url.IsNullOrEmpty())
        {
            return string.Empty;
        }
        if (url.ToLower().Contains("http"))
        {
            return url;
        }
        if (url.StartsWith("/"))
        {
            url = "../.." + url;
        }
        else
        {
            url = "../../" + url;
        }
        return url;
    }

    /// <summary>
    /// 重新装载页面
    /// </summary>
    public void Reload()
    {
        //this.Reload();
    }


    /// <summary>
    /// 重新装载打开本页面的父页面
    /// </summary>
    public void ReloadOpener()
    {
        //this.ReloadOpener();
    }

    /// <summary>
    /// 显示提示信息
    /// </summary>
    /// <param name="p_Message">提示信息字符串</param>
    public void Alert(string p_Message)
    {
        if (p_Message == null)
        {
            return;
        }
        if (p_Message.Contains("\r\n")) p_Message = p_Message.Replace("\r\n", "\\r\\n");
        if (p_Message.Contains("\n")) p_Message = p_Message.Replace("\n", "\\n");
        if (p_Message.Contains("'")) p_Message = p_Message.Replace("'", "\'");
        if (p_Message.Contains(";")) p_Message = p_Message.Replace(";", "\\;");
        if (p_Message.Contains("\"")) p_Message = p_Message.Replace("\"", "");
        string p_Script = "alert('" + p_Message.ToJsString() + "')";
        this.Page.Page.Response.Write("<script type='text/javascript'>alert('" + p_Message + "');</script>");
        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "pageAlert", "<script type='text/javascript'>alert('" + p_Message + "');</script>", true);
        //ScriptManager.RegisterStartupScript(this, GetType(), "pageAlert", "<script type='text/javascript'>alert('" + p_Message + "');</script>", true);
    }


    /// <summary>
    /// 显示提示信息而且返回上一页面
    /// </summary>
    /// <param name="url">要转到的页面地址</param>
    /// <param name="p_Message">提示信息</param>
    public void AlertAndBack(string url, string p_Message)
    {
        string str = "alert('" + StringExtension.ToJsString(p_Message) + "');";
        while (url.Trim() != "")
        {
            str = str + "window.location.href = '" + url + "';";
            break;
        }
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "str", str, true);
    }

    /// <summary>
    /// 显示提示信息而且返回上一页面
    /// </summary>
    /// <param name="panel">要转到的页面的UpdatePanel</param>
    /// <param name="url">要转到的页面地址</param>
    /// <param name="p_Message">提示信息</param>
    public void AlertAndBack(Control panel, string url, string p_Message)
    {
        string str = "alert('" + StringExtension.ToJsString(p_Message) + "');";
        while (url.Trim() != "")
        {
            str = str + "window.location.href = '" + url + "';";
            break;
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(panel, this.GetType(), "str", str, true);
    }

    /// <summary>
    /// 显示提示信息而且返回上一页面
    /// </summary>
    /// <param name="panel">要转到的页面的UpdatePanel</param>
    /// <param name="url">要转到的页面地址</param>
    /// <param name="p_Message">提示信息</param>
    public void Alert(Control panel, string p_Message)
    {
        string str = "alert('" + StringExtension.ToJsString(p_Message) + "');";
        System.Web.UI.ScriptManager.RegisterStartupScript(panel, this.GetType(), "str", str, true);
    }

    //public string BuildQueryString(SYSOperation op,string id)
    //{
    //    string bqs = string.Empty;
    //    if (op == SYSOperation.New)
    //    {
    //        bqs = "&Action=New&ID=" + id;
    //    }
    //    else if (op == SYSOperation.Modify)
    //    {
    //        bqs = "&Action=Modify&ID=" + id;
    //    }

    //    return bqs;
    //}

    /// <summary>
    /// 确认后跳转
    /// </summary>
    /// <param name="url">跳转地址</param>
    /// <param name="confirmQuestion">需要确认的信息</param>
    public void ConfirmThenBack(string url, string confirmQuestion)
    {
        //this.ConfirmThenBack(url, confirmQuestion);
    }

    /// <summary>
    /// 确认后跳转
    /// </summary>
    /// <param name="url">跳转地址</param>
    /// <param name="confirmQuestion">需要确认的信息</param>
    /// <param name="urlWhenCancel">当回答Cancel时跳转地址</param>
    public void ConfirmThenBack(string url, string confirmQuestion, string urlWhenCancel)
    {
        //this.ConfirmThenBack(url, confirmQuestion, urlWhenCancel);
    }


    /// <summary>
    /// 获取DataGrid中控件的客户端ID
    /// </summary>
    /// <param name="controlID">控件ID</param>
    /// <returns></returns>
    public string GetDataGridControlClientID(string controlID)
    {
        return controlID.Replace("__", ":_");
    }


    /// <summary>
    /// 设置控件焦点(且弹出错误信息)
    /// </summary>
    /// <param name="c">控件</param>
    /// <param name="message">错误信息</param>
    public void SetFocus(Control c, string message)
    {
        string str = "document.getElementById('" + c.ClientID + "').focus();" + "alert('" + message.ToJsString() + "');";
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), str, true);

    }

    /// <summary>
    /// 设置控件焦点(且弹出错误信息)
    /// </summary>
    /// <param name="controlID">控件</param>
    /// <param name="message">错误信息</param>
    public void SetFocus(string controlID, string message)
    {
        //this.SetFocus(controlID, message);
    }

    /// <summary>
    /// 根据代码获取，显示在界面上的格式：代码名称+'('+代码+')'
    /// 界面显示数据时调用
    /// </summary>
    /// <param name="tname">代码所在数据表</param>
    /// <param name="value">代码值</param>
    /// <returns>显示在界面上的格式</returns>
    /// 调用示例：this.txtUnit.Text=this.SetCode("Sys_Unit",entity.Unit);	
    public string SetCode(string tname, string value)
    {
        throw new NotImplementedException("尚未实现此方法");
    }

    /// <summary>
    /// 注册一段Javascripe，关闭页面
    /// </summary>
    public void Close()
    {
        //this.Close();
    }

    /// <summary>
    /// 获取入库通知单类型-查询
    /// </summary>
    /// <param name="IsSearch"></param>
    /// <returns></returns>
    /// <summary>
    /// 获取交易类型
    /// </summary>
    /// <param name="type">空全部，0入库 1出库 2调拨</param>
    /// <returns></returns>
    public DataTable GetInOutAlloType(string ctype)
    {
        string Sql = @"select *
                          from (select tp.cerpcode typecode, (tp.typename) typename, 0 type
                                  from intype tp
                                 where tp.cerpcode != '2'
                                union
                                select '2' typecode, '调拨' typename, 1 type
                                 
                                union
                                select tp.cerpcode typecode, (tp.typename) typename, 2 type
                                  from outtype tp
                                 where tp.cerpcode != '2') t
                         where 1 = 1 ";
        if (ctype.Trim() != "")
        {
            Sql += " and t.type =" + ctype.Trim() + " ";
        }
        Sql += "  order by t.type asc,t.typename asc";
        return DBHelp.ExecuteToDataTable(Sql);
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    public List<V_GetBtnFunctionListEntity> CurrentButtonList { get; set; }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        #region 依据权限，设定按钮是否可用
        string currentUrl = Request.Url.AbsoluteUri.ToLower();//完整路径
        if (currentUrl.ToLower().Contains("login.aspx") || currentUrl.ToLower().Contains("frmtopcompany.aspx") || currentUrl.ToLower().Contains("frmresetpassword.aspx") || currentUrl.ToLower().Contains(".ascx"))
        {
            return;
        }
        List<V_GetBtnFunctionListEntity> allButtonList = Session["ButtonList"] as List<V_GetBtnFunctionListEntity>;
        //List<V_GetBtnFunctionListEntity> currentButtonList = null;
        if (allButtonList != null)
        {
            CurrentButtonList = allButtonList.FindAll(x => currentUrl.Contains(x.F_Data.ToLower().Trim()));
            //CurrentButtonList = currentButtonList;
            IterateThroughChildren(this.Page);
        }
        #endregion
    }

    void IterateThroughChildren(Control parent)
    {
        foreach (Control control in parent.Controls)
        {
            if (control is Button || control is HtmlButton)
            {
                if (CurrentButtonList != null && CurrentButtonList.Count() > 0)
                {
                    bool bl = CurrentButtonList.Exists(x => !string.IsNullOrEmpty(x.F_Code) && x.F_Code.ToLower().Trim().Contains(control.ID.ToLower()));
                    if (bl)
                    {
                        //control.Visible = true;//不覆盖子页面的逻辑
                    }
                    else
                    {
                        control.Visible = false;
                    }
                }
                else
                {
                    control.Visible = false;
                }
            }
            if (control.Controls.Count > 0)       // 判断该控件是否有下属控件。
            {
                IterateThroughChildren(control);    //递归，访问该控件的下属控件集。
            }
        }
    }

    /// <summary>
    /// 获取人员的出库/入库的类型
    /// </summary>
    /// <param name="userNO"></param>
    /// <returns></returns>
    public List<DreamTek.WMS.DAL.Model.Base.UserFunction> GetUserCaseType(string userNO)
    {
        UserFunctionRepository ufr = new UserFunctionRepository();
        List<DreamTek.WMS.DAL.Model.Base.UserFunction> list = ufr.GetUserFunction(userNO);
        return list;
    }

    /// <summary>
    /// 获取人员的出库/入库的类型
    /// </summary>
    /// <param name="userNO"></param>
    /// <returns></returns>
    public List<string> GetUserCaseTypeNew(string userNO)
    {
        UserFunctionRepository ufr = new UserFunctionRepository();
        List<string> list = ufr.GetUserCaseTypeNew(userNO);
        return list;
    }

    #region TranslateLanguage
    /// <summary>
    /// 将消息代码转换成当前语言版本文字
    /// </summary>
    /// <param name="strKey"></param>
    /// <returns></returns>
    public string TranslateLanguage(string strKey)
    {
        return TranslateLanguage("msg", strKey);
    }

    public string TranslateLanguage(string className, string strKey)
    {
        if (GetGlobalResourceObject(className, strKey) == null)
            return strKey;

        return GetGlobalResourceObject(className, strKey).ToString();
    }
    #endregion

    #region 自定义消息提示框

    #region ShowTips
    /// <summary>
    /// 格式化消息提示
    /// </summary>
    /// <param name="message"></param>
    /// <param name="messageType"></param>
    /// <returns></returns>
    public string ShowTips(string message, MessageType messageType)
    {
        string className = "";

        switch (messageType)
        {
            case MessageType.Right:
                className = "success";
                break;
            case MessageType.Error:
                className = "failure";
                break;
            case MessageType.Warning:
                className = "warning";
                break;
            case MessageType.None:
                break;
            default:
                break;
        }

        message = "<div class='" + className + "'>" + message + "</div>";

        return message;
    }
    #endregion

    #region ShowMessageBox

    /// <summary>
    /// 自定义消息提示框
    /// </summary>
    public void ShowMessageBox(Page page, string message)
    {
        ShowMessageBox(page, message, MessageType.None);
    }

    public void ShowMessageBox(Page page, string message, MessageType messageType)
    {
        ShowMessageBox(page, message, messageType, "");
    }

    public void ShowMessageBox(Page page, string message, MessageType messageType, string returnUrl)
    {
        ShowMessageBox(page, message, messageType, returnUrl, false);
    }

    public void ShowMessageBox(Page page, string message, MessageType messageType, string returnUrl, bool isScriptBlock)
    {
        ShowMessageBox(page, message, messageType, returnUrl, isScriptBlock, "系统消息");
    }

    public void ShowMessageBox(Page page, string message, MessageType messageType, string returnUrl, bool isScriptBlock, string title)
    {
        message = message.Replace("'", "\\'");
        message = message.Replace("\r\n", "");

        //if (isScriptBlock)
        //{
        //    if (!ClientScript.IsClientScriptBlockRegistered("jAlert"))
        //        ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "jAlert", "showAlert('" + message + "', '" + page.ResolveClientUrl(returnUrl) + "');", true);
        //}
        //else
        //{
        //    if (!ClientScript.IsStartupScriptRegistered("jAlert"))
        //        ScriptManager.RegisterStartupScript(page, page.GetType(), "jAlert", "showAlert('" + message + "', '" + page.ResolveClientUrl(returnUrl) + "');", true);
        //}
    }
    #endregion

    #region ShowPage
    public void ShowPage(string pageUrl)
    {
        ShowPage("", pageUrl);
    }

    public void ShowPage(string title, string pageUrl)
    {
        ShowPage(title, pageUrl, false);
    }

    public void ShowPage(string title, string pageUrl, bool isModal)
    {
        ShowPage(title, pageUrl, 500, 400, isModal);
    }

    public void ShowPage(string title, string pageUrl, int width, int height, bool isModal)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tickpage", "showPage('" + title + "','" + pageUrl + "', " + width + ", " + height + ", " +
        //    "," + isModal + ");return false;", true);
    }
    #endregion

    public enum MessageType
    {
        None,
        Right,
        Error,
        Warning
    }

    #endregion

    #region 关闭弹出窗口
    public void ClosePopAndReloadParent()
    {
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "jsClosePop", "self.parent.window.location.href=self.parent.window.location.href;", true);
    }

    public void ClosePopAndReloadParent(Page page)
    {
        //ScriptManager.RegisterStartupScript(page, page.GetType(), "jsClosePop", "self.parent.window.location.href=self.parent.window.location.href;", true);
    }
    public void ShowPopPage(string strjs)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tickpage", strjs, true);
    }
    public void CloseWindowAndRef()
    {
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "jsCloseWindow", "winClose();", true);
    }
    #endregion

    #region GridView表格

    //表格绑定
    public void BindGridRowData(GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='hoverStyle';");
            e.Row.Attributes.Add("onmouseout", "this.className='outStyle';");
        }
    }

    private string strEmptyText = string.Empty;
    //显示无数据的GridView的表头
    public void BindNoDataGridHeader(GridView gridView, DataTable dt)
    {
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());

            gridView.DataSource = dt;
            gridView.DataBind();

            int columnCount = gridView.Rows[0].Cells.Count;

            gridView.Rows[0].Cells.Clear();
            gridView.Rows[0].Cells.Add(new TableCell());
            gridView.Rows[0].Cells[0].ColumnSpan = columnCount;
            gridView.Rows[0].Cells[0].Text = strEmptyText;
            gridView.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
    }

    public void ResetGridView(GridView gridview)
    {
        //如果数据为空则重新构造Gridview
        if (gridview.Rows.Count == 1 && gridview.Rows[0].Cells[0].Text == strEmptyText)
        {
            int columnCount = gridview.Columns.Count;
            gridview.Rows[0].Cells.Clear();
            gridview.Rows[0].Cells.Add(new TableCell());
            gridview.Rows[0].Cells[0].ColumnSpan = columnCount;
            gridview.Rows[0].Cells[0].Text = strEmptyText;
            gridview.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
    }

    //获取多选Checkbox主键列表
    public string GetGridDataKeysList(GridView gridview)
    {
        return GetGridDataKeysList(gridview, "chkThis");
    }

    public string GetGridDataKeysList(GridView gridview, string checkboxName)
    {
        if (checkboxName == null || checkboxName == "")
            return "";

        string listId = "";

        foreach (GridViewRow row in gridview.Rows)
        {

            CheckBox chkItem = (CheckBox)row.FindControl(checkboxName);

            if (chkItem.Checked)
            {
                listId += "'" + gridview.DataKeys[row.RowIndex].Value.ToString() + "',";
            }
        }

        listId = listId.TrimEnd(',');

        return listId;
    }

    #endregion



    #region 绑定DropDownList
    public void BindDropDowList(DropDownList ddl, DataTable dt, string strValueField, string strTextField)
    {
        ddl.Items.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            ddl.Items.Add(new ListItem(dr[strTextField].ToString(), dr[strValueField].ToString()));
        }
        if (ddl.Items.Count > 1)
            ddl.Items.Insert(0, new ListItem("全部", ""));
    }
    #endregion


    #region 延时
    //add by baker
    public static void Delay(int Seconds)
    {
        DateTime Now = DateTime.Now;
        while (Now.AddSeconds(Seconds) > DateTime.Now)
        {
        }
    }
    #endregion

    public static int PageIndex = 0;

    //public static string Traditional2Simplified(string str)
    //{ //繁体转简体    
    //    return (Microsoft.VisualBasic.Strings.StrConv(str, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese, 0));

    //}
    //public static string Simplified2Traditional(string str)
    //{ //简体转繁体    
    //    return (Microsoft.VisualBasic.Strings.StrConv(str as String, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, 0));

    //}

    public static readonly string Ascending = "Ascending";
    public static readonly string Descending = "Descending";

    /// <summary>
    ///  当前排序的列名
    /// </summary>
    public string SortedField
    {
        get
        {
            if (ViewState["SortedField"] == null)
            {
                ViewState["SortedField"] = string.Empty;
            }
            return ViewState["SortedField"].ToString();
        }
        set { ViewState["SortedField"] = value; }
    }

    /// <summary>
    ///  排序方式 Ascending Descending
    /// </summary>
    public string SortedAD
    {
        get
        {
            if (ViewState["SortedAD"] == null)
            {
                ViewState["SortedAD"] = string.Empty;
            }
            return ViewState["SortedAD"].ToString();
        }
        set { ViewState["SortedAD"] = value; }
    }

    /// <summary>
    /// 获取当前排序的字符串
    /// </summary>
    public string SortedStr
    {
        get
        {
            if (!string.IsNullOrEmpty(SortedField))
            {
                return " " + SortedField + " " + SortedAD + " ";
            }
            else
            {
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// 获取分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="pageSize"></param>
    /// <param name="currendIndex"></param>
    /// <returns></returns>
    public static IEnumerable<T> GetPageSize<T>(IQueryable<T> source, int pageSize, int currendIndex)
    {
        //return source.Take(pageSize * currendIndex).Skip(pageSize * (currendIndex - 1));
        return source.Skip(pageSize * (currendIndex - 1)).Take(pageSize);
    }



    public class Block
    {
        public string Key { get; set; }
        public string value { get; set; }
        public string seq { get; set; }
    }

    private List<Block> _BlockList;

    public List<Block> BlockList
    {
        get { return _BlockList ?? (_BlockList = new List<Block>()); }
    }

    /// <summary>
    /// excel的快捷导出方法
    /// </summary>
    /// <param name="GridID">gridview ID</param>
    /// <param name="ExcelName">excel名称</param>
    /// <param name="dtSource">数据源</param>
    protected void ExportToExcel<T>(GridView fg, string ExcelName, List<T> dtSource)
    {
        ////var fg = (GridView)Parent.FindControl(GridID);
        //var sb = new StringBuilder();
        //var sw = new StringWriter(sb);
        //var htw = new HtmlTextWriter(sw);

        //var page = new Page();
        //var form = new HtmlForm();Action

        ////var dtSource = dtOutPut();
        //var fgOut = new GridView { AutoGenerateColumns = false };
        ////循环添加数据
        //for (var col = 0; col < fg.Columns.Count; col++)
        //{
        //    if (fg.Columns[col] is BoundField)
        //    {
        //        var ColNew = (BoundField)fg.Columns[col];
        //        fgOut.Columns.Add(new BoundField { DataField = ColNew.DataField, HeaderText = ColNew.HeaderText });
        //    }
        //    else if (fg.Columns[col] is TemplateField)
        //    {
        //        var ColNew = (TemplateField)fg.Columns[col];
        //        if (!string.IsNullOrEmpty(ColNew.SortExpression))
        //        {
        //            fgOut.Columns.Add(new BoundField { DataField = ColNew.SortExpression, HeaderText = ColNew.HeaderText });
        //        }
        //    }
        //}

        //fgOut.DataSource = dtSource;
        //fgOut.DataBind();

        //fgOut.EnableViewState = false;
        ////添加格式
        //fgOut.Attributes.Add("style", "vnd.ms-excel.numberformat:@");

        //page.EnableEventValidation = false;
        //page.DesignerInitialize();

        //page.Controls.Add(form);
        //form.Controls.Add(fgOut);
        //page.RenderControl(htw);

        //Page.Response.Clear();
        //Page.Response.Buffer = true;
        //Page.Response.ContentType = "application/vnd.ms-excel";
        //Page.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(ExcelName, Encoding.UTF8).ToString() + ".xls");
        //Page.Response.Charset = "UTF-8";

        //Page.Response.ContentEncoding = System.Text.Encoding.Unicode;
        //var rgByteLeader = new byte[] { 0xFF, 0xFE };
        //Page.Response.BinaryWrite(rgByteLeader);
        //Page.Response.Write(sb.ToString());
        //Page.Response.End();
    }


    public int _PageSize = 15;

    /// <summary>
    /// 分页大小
    /// </summary>
    public int PageSize
    {
        get
        {
            if (ViewState["PageSize"] == null)
            {
                ViewState["PageSize"] = _PageSize;
            }
            return (int)ViewState["PageSize"];
        }
        set
        {
            ViewState["PageSize"] = value;
        }
    }

    /// <summary>
    /// 当前页数
    /// </summary>
    public int CurrendIndex
    {
        get
        {
            if (ViewState["CurrendIndex"] == null)
            {
                ViewState["CurrendIndex"] = 1;
            }
            return (int)ViewState["CurrendIndex"];
        }
        set
        {
            ViewState["CurrendIndex"] = value;
        }
    }


    /// 获取配置表中的值
    /// <summary>
    /// 获取配置表中的值
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public string GetConFig(string code)
    {
        string RetValue = "";
        IGenericRepository<SYS_CONFIG> con = new GenericRepository<SYS_CONFIG>(context);
        var query = from p in con.Get()
                    where p.code == code.Trim()
                    select p;
        if (query.Count() > 0)
            RetValue = query.ToList().FirstOrDefault<SYS_CONFIG>().config_value;
        return RetValue;


    }


    //SELECT palletcode,* FROM OUTBILL
    //WHERE cticketcode = '022009180002'
    public string GetPalletByOutbill(string num)
    {
        string RetValue = "";
        IGenericRepository<OUTBILL> con = new GenericRepository<OUTBILL>(context);
        var query = from p in con.Get()
                    where p.cticketcode == num.Trim()
                    select p;
        if (query.Count() > 0)
            RetValue = query.ToList().FirstOrDefault<OUTBILL>().palletcode;
        return RetValue;


    }

    /// <summary>
    /// 依据配置，获取SN的实际类型
    /// </summary>
    /// <returns></returns>
    public string GetConfigSNName()
    {
        string sql = @"
                        select sp.flag_name from sys_parameter sp
                        where sp.flag_type = 'SNTYPE'
                        and sp.flag_id in 
                        (
                          select s.config_value from sys_config s
                          where s.code='120001'
                        )
                        ";
        return DBHelp.ExecuteScalar(sql);
    }


    /// <summary>
    /// 依据配置，获取SN的实际类型
    /// </summary>
    /// <returns></returns>
    public string GetConfigSNValue()
    {
        string sql = @"
                        select sp.flag_id from sys_parameter sp
                        where sp.flag_type = 'SNTYPE'
                        and sp.flag_id in 
                        (
                          select s.config_value from sys_config s
                          where s.code='120001'
                        )
                        ";
        return DBHelp.ExecuteScalar(sql);
    }


    /// <summary>
    /// 当前系统入库单位的名称
    /// </summary>
    public string CurrentConfigUnitName
    {
        get
        {
            if (ViewState["CurrentConfigUnitName"] == null)
            {
                ViewState["CurrentConfigUnitName"] = GetConfigSNName();
            }
            return (string)ViewState["CurrentConfigUnitName"];
        }
        set
        {
            ViewState["CurrentConfigUnitName"] = value;
        }
    }
    public void OpenFloatWinMax(WebControl btn, string url, string title, string winID)
    {
        btn.Attributes["OnClick"] = string.Concat(new string[]
            {
                "PopupFloatWinMax('",
				//url.ToJsString(),
                url,
                "','",
                title,
                "','",
                winID,
                "');return false;"
            });
    }

    public void OpenFloatWinMax(string url, string title, string winID)
    {
        this.WriteScript(string.Concat(new string[]
            {
                "PopupFloatWinMax('",
                url.ToJsString(),
                "','",
                title,
                "','",
                winID,
                "');return false;"
            }));
    }


    /// <summary>
    /// 在屏幕正中打开一个浮动窗口
    /// </summary>
    /// <param name="btn">按钮</param>
    /// <param name="url">页面地址</param>
    /// <param name="title">标题</param>
    /// <param name="winID">窗体的编号，允许英文数字下划线</param>
    /// <param name="width">宽度</param>
    /// <param name="height">高度</param>
    public void OpenFloatWin(WebControl btn, string url, string title, string winID, int width, int height)
    {
        btn.Attributes["OnClick"] = string.Concat(new string[]
            {
                "PopupFloatWin('",
				//url.ToJsString(),
                url,
                "','",
                title,
                "','",
                winID,
                "',",
                width.ToString(),
                ",",
                height.ToString(),
                ");return false;"
            });
    }

    public string AddBR(string str, int c)
    {
        string res = string.Empty;
        string v = string.Empty;
        int j = 0;
        if (!string.IsNullOrEmpty(str) && str.Length > c)
        {
            for (int i = 0; i < str.Length; i++)
            {
                //int.TryParse((i / 30).ToString(), out j);
                if (int.TryParse((i / 30).ToString(), out j) && (i - j * c) == 0)
                {
                    str = str.Insert(i, "<br/>");
                    i = i + 5;
                }
            }
            res = str;
        }
        else
        {
            res = str;
        }
        return res;
    }

    /// <summary>
    /// 在屏幕正中打开一个浮动窗口
    /// </summary>
    /// <param name="url">页面地址</param>
    /// <param name="title">标题</param>
    /// <param name="winID">窗体的编号，允许英文数字下划线</param>
    /// <param name="width">宽度</param>
    /// <param name="height">高度</param>
    public void OpenFloatWin(string url, string title, string winID, int width, int height)
    {
        this.WriteScript(string.Concat(new string[]
            {
                "PopupFloatWin('",
                url.ToJsString(),
                "','",
                title,
                "','",
                winID,
                "',",
                width.ToString(),
                ",",
                height.ToString(),
                ");return false;"
            }));
    }

    /// <summary>
    /// 生成编辑页面的请求URL
    /// </summary>
    /// <param name="pageURL">页面地址</param>
    /// <param name="oper">动作</param>
    /// <param name="ID">编号</param>
    /// <returns></returns>
    public static string BuildRequestPageURL(string pageURL, SYSOperation oper, string ID)
    {
        return BuildRequestPageURLS(pageURL, oper, ID);
    }

    public static string BuildRequestPageURLS(string pageURL, SYSOperation oper, string ID)
    {

        if (pageURL.IndexOf("?") < 0)
        {
            return pageURL + "?" + BuildQueryString(oper, ID);
        }
        return pageURL + "&" + BuildQueryString(oper, ID);
    }
    /// <summary>
    /// 生成页面的QueryString
    /// </summary>
    /// <param name="oper">动作</param>
    /// <param name="ID">编号</param>
    /// <returns></returns>
    public static string BuildQueryString(SYSOperation oper, string ID)
    {
        //return "Action=" + oper.ToString() + "&ID=" + ID.ToJsString();
        return "Action=" + oper.ToString() + "&ID=" + ID;
    }

    /// <summary>
    /// 允许做特殊元件领料的出库类型
    /// </summary>
    /// <returns></returns>
    /// 2015-12-08 去除作废的状态
    public DataTable GetOutTypeBySpecialIssue2(bool IsSearch)
    {
        string sql = @"select typename,cerpcode from outtype ot 
                        where ot.transaction_source_type_id=5 
                        and ot.attribute1='N'
                        and transaction_action_id in ('1', '27', '33', '34')
                        and ot.enable='0'";
        if (!IsSearch)
        {
            sql += " and (DISABLE_DATE is null or DISABLE_DATE >= sysdate)";
        }
        return DBHelp.ExecuteToDataTable(sql);
    }

    //2015-12-08 去除作废的状态
    public DataTable GetOutTypeByOutAsn2(bool IsSearch)
    {
        string sql = string.Format(@"select distinct f.typename as FUNCNAME ,f.cerpcode as EXTEND1 
                       from outtype f inner join ( select FUNCNAME,EXTEND1 from UserFunction where userno= '{0}' ) t  on t.EXTEND1 = f.cerpcode 
                       where 1=1 and f.enable='0'", WmsWebUserInfo.GetCurrentUser().UserNo);
        if (!IsSearch)
        {
            //增删改
            sql += " and f.Is_Query !='1' and (DISABLE_DATE is null or DISABLE_DATE >= Getdate()) and f.cerpcode not in (select t.ERPCODE from OUTASNNOTUSEOUTTYPE t)";
        }
        return DBHelp.ExecuteToDataTable(sql);
    }

    /// <summary>
    /// 判断是否十进制格式字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public bool IsDecimal(string str)
    {
        const string PATTERN = @"[0-9]+$";
        return System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
    }

    /// <summary>
    /// 是否2位以内小数
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public bool IsDecimalwithTwoDigit(string str)
    {
        const string PATTERN = @"(^(([1-9][0-9]*)|(([0]\.\d{1,2}|[1-9][0-9]*\.\d{1,2})))$)";
        return System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
    }


    public DataTable GetOutType(bool IsSearch)
    {
        string sql = string.Format(@"select distinct s.FLAG_NAME as FUNCNAME ,f.cerpcode as EXTEND1 
                       from outtype f inner join ( select FUNCNAME,EXTEND1 from UserFunction where userno= '{0}' ) t  on t.EXTEND1 = f.cerpcode 
                       INNER JOIN dbo.V_SYS_PARAMETER s WITH(NOLOCK) ON s.FLAG_ID= f.cerpcode AND s.FLAG_TYPE='OUTTYPE' AND s.LANGUAGEID='" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'                       where 1=1 ", WmsWebUserInfo.GetCurrentUser().UserNo);
        if (!IsSearch)
        {
            //增删改
            sql += " and f.Is_Query!='1' and (DISABLE_DATE is null or DISABLE_DATE >= GetDate())";
        }
        //过滤调拨出
        // sql += " and f.cerpcode!=2";
        return DBHelp.ExecuteToDataTable(sql);
    }
    /// <summary>
    /// 获取入库通知单类型-查询
    /// </summary>
    /// <param name="IsSearch"></param>
    /// <returns></returns>
    public DataTable GetInType(bool IsSearch)
    {
        string sql = string.Format(@"select distinct s.FLAG_NAME as FUNCNAME,f.cerpcode as EXTEND1 
                       from intype f inner join ( select FUNCNAME,EXTEND1 from UserFunction where userno='{0}') t  on t.EXTEND1 = f.cerpcode 
                        INNER JOIN dbo.V_SYS_PARAMETER s WITH(NOLOCK) ON s.FLAG_ID= f.cerpcode AND s.FLAG_TYPE='INTYPE' AND s.LANGUAGEID='" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' where f.enable=0 and 1=1 ", WmsWebUserInfo.GetCurrentUser().UserNo);
        if (!IsSearch)
        {
            //增删改
            sql += " and f.Is_Query!='1' and (DISABLE_DATE is null or DISABLE_DATE >= GetDate())";
        }
        //过滤调拨入
        //sql += " and f.cerpcode!=2";
        return DBHelp.ExecuteToDataTable(sql);
    }
    /// <summary>
    /// 获取入库理由码信息
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public DataTable GetReasonCode(string type, bool IsShowERP)
    {
        string sql = string.Format(@"SELECT A.REASONCODE,s.FLAG_NAME AS REASONCONTENT FROM BASE_DOCUREASON A INNER JOIN dbo.V_SYS_PARAMETER s WITH(NOLOCK) ON s.FLAG_ID= A.REASONCODE AND s.FLAG_TYPE='BASE_DOCUREASON' AND s.LANGUAGEID='" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'  WHERE A.STATES = 'Y' AND A.ACTIONSCOPE = '{0}'", type);
        if (!IsShowERP)
        {
            //增删改
            sql += " and A.isfromerp!='1'";
        }
        return DBHelp.ExecuteToDataTable(sql);
    }

    /// <summary>
    /// 依据入库通知单，入库单ID查询INTYPE
    /// </summary>
    /// <param name="id">入库通知单，入库单ID</param>
    /// <param name="caseType">INASN 或 INBILL</param>
    /// <returns></returns>
    public INTYPE GetINTYPEByID(string id, string caseType)
    {
        INTYPE inType = new INTYPE();
        if (caseType == "INASN")
        {
            using (DBContext dbcontex = context)
            {
                IGenericRepository<INASN> con = new GenericRepository<INASN>(dbcontex);
                INASN ASN = (from p in con.Get()
                             where p.id == id
                             select p).FirstOrDefault();
                return GetINTYPE(ASN.itype);
            }
        }
        else if (caseType == "INBILL")
        {
            using (DBContext dbcontex = context)
            {
                IGenericRepository<INBILL> con = new GenericRepository<INBILL>(dbcontex);
                INBILL bill = (from p in con.Get()
                               where p.id == id
                               select p).FirstOrDefault();
                return GetINTYPE(bill.itype.ToString());
            }
        }
        return inType;
    }

    /// <summary>
    ///  依据出库通知单，出库单ID查询OUTTYPE
    /// </summary>
    /// <param name="id">出库通知单，出库单ID</param>
    /// <param name="caseType">OUTASN 或 OUTBILL</param>
    /// <returns></returns>
    public OUTTYPE GetOUTTYPEByID(string id, string caseType)
    {
        OUTTYPE inType = new OUTTYPE();
        if (caseType == "OUTASN")
        {
            using (DBContext dbcontex = context)
            {
                IGenericRepository<OUTASN> con = new GenericRepository<OUTASN>(dbcontex);
                OUTASN ASN = (from p in con.Get()
                              where p.id == id
                              select p).FirstOrDefault();
                return GetOUTTYPE(ASN.itype.ToString());
            }
        }
        else if (caseType == "OUTBILL")
        {
            using (DBContext dbcontex = context)
            {
                IGenericRepository<OUTBILL> con = new GenericRepository<OUTBILL>(dbcontex);
                OUTBILL bill = (from p in con.Get()
                                where p.id == id
                                select p).FirstOrDefault();
                return GetOUTTYPE(bill.otype.ToString());
            }
        }
        return inType;
    }

    /// <summary>
    /// 获取INTYPE对象
    /// </summary>
    /// <param name="typeCode">单据编号</param>
    /// <returns></returns>
    public INTYPE GetINTYPE(string typeCode)
    {
        using (DBContext dbcontex = context)
        {
            IGenericRepository<INTYPE> con = new GenericRepository<INTYPE>(dbcontex);
            INTYPE inType = (from p in con.Get()
                             where p.cerpcode == typeCode
                             select p).FirstOrDefault();
            return inType;
        }
    }

    /// <summary>
    /// 获取OUTTYPE对象
    /// </summary>
    /// <param name="typeCode">单据编号</param>
    /// <returns></returns>
    public OUTTYPE GetOUTTYPE(string typeCode)
    {
        using (DBContext dbcontex = context)
        {
            IGenericRepository<OUTTYPE> con = new GenericRepository<OUTTYPE>(dbcontex);
            OUTTYPE outType = (from p in con.Get()
                               where p.cerpcode == typeCode
                               select p).FirstOrDefault();
            return outType;
        }
    }

    public DataTable GetLineID()
    {
        string sql = @"select distinct t.CRANENAME as FUNCNAME,t.CRANEID as EXTEND1  from  BASE_CRANECONFIG t  order by t.CRANEID asc ";
        return DBHelp.ExecuteToDataTable(sql);
    }

    /// <summary>
    /// 获取立库线路
    /// </summary>
    /// <returns></returns>
    public DataTable GetLKLineID()
    {
        string sql = @"select distinct t.CRANENAME as FUNCNAME,t.CRANEID as EXTEND1  from  BASE_CRANECONFIG t with(nolock) where t.PLCType = 'LK'  order by t.CRANEID asc ";
        return DBHelp.ExecuteToDataTable(sql);
    }

    public DataTable GetPallet(string lineId)
    {
        string sql = string.Format(@"select  distinct crane.siteid as EXTEND1,crane.sitename as FUNCNAME   
                        from base_craneconfig_detial crane   
                       where crane.flag='0'  and crane.sitetype in ('2','3')
                       and crane.CRANEID='{0}'
                       order by crane.siteid  asc ", lineId);
        return DBHelp.ExecuteToDataTable(sql);
    }

    /// <summary>
    /// 根据出库类型及选择线路，返回可使用的站点
    /// </summary>
    /// <param name="lineId"></param>
    /// <param name="outtype"></param>
    /// <returns></returns>
    public DataTable GetOutPallet(string lineId, string outtype)
    {
        string sql = string.Format(@"select  distinct crane.siteid as EXTEND1,crane.sitename as FUNCNAME   
                        from base_craneconfig_detial crane with(nolock) 
                       where crane.flag='0'  and crane.sitetype in ('2','3')
                       and crane.CRANEID='{0}'
                       and exists (select 1 from BASE_CRANECONFIG_TRADETYPE bct with(nolock) where bct.id = crane .id  and bct.CSTATUS ='0'  and bct.INOROUTCODE = 'O' and bct.cerpcode = '{1}')
                       order by crane.siteid  asc ", lineId, outtype);
        return DBHelp.ExecuteToDataTable(sql);
    }

    /// <summary>
    /// 根据出库类型及选择线路，返回可使用的站点
    /// </summary>
    /// <param name="lineId"></param>
    /// <param name="outtype"></param>
    /// <returns></returns>
    public DataTable GetOutPallet(string lineId, string outtype, string billno)
    {
        string sql = "";
        if (string.IsNullOrEmpty(billno) || outtype != "203")
        {
            sql = string.Format(@"select  distinct crane.siteid as EXTEND1,crane.sitename as FUNCNAME   
                        from base_craneconfig_detial crane with(nolock) 
                       where crane.flag='0'  and crane.sitetype in ('2','3')
                       and crane.CRANEID='{0}'
                       and exists (select 1 from BASE_CRANECONFIG_TRADETYPE bct with(nolock) where bct.id = crane .id  and bct.CSTATUS ='0'  and bct.INOROUTCODE = 'O' and bct.cerpcode = '{1}')
                       order by crane.siteid  asc ", lineId, outtype);
        }
        else
        {
            sql = string.Format(@"select  distinct crane.siteid as EXTEND1,crane.sitename as FUNCNAME   
                        from base_craneconfig_detial crane with(nolock) 
                       where crane.flag='0'  and crane.sitetype in ('2','3')
                       and crane.CRANEID='{0}'
                       and exists (select 1 from BASE_CRANECONFIG_TRADETYPE bct with(nolock) 
                                   where bct.id = crane .id  
                                     and bct.CSTATUS ='0'  
                                     and bct.INOROUTCODE = 'O' 
                                     and bct.cerpcode = '{1}'
                                     and (NOT EXISTS (SELECT 1 FROM BASE_CRANECONFIG_TRADETYPE_D TD with(nolock) where TD.tradetypeids = bct.ids) 
									      OR EXISTS (SELECT 1 FROM BASE_CRANECONFIG_TRADETYPE_D TD2 with(nolock) where TD2.tradetypeids = bct.ids AND TD2.billno = '{2}' ) )
                       )
                       order by crane.siteid  asc ", lineId, outtype, billno);
        }
        return DBHelp.ExecuteToDataTable(sql);
    }


    //    public DataTable GetReason()
    //    {
    //        string sql = @"select  distinct reason.reasoncode as EXTEND1,reason.reasoncontent as  FUNCNAME 
    //                       from base_docureason reason where reason.actionscope='2' and   reason.states='Y'  order by reason.reasoncode  asc";
    //        return DBHelp.ExecuteToDataTable(sql);
    //    }


    /// <summary>
    /// 根据条码获取条码规则及部分信息
    /// </summary>
    /// <param name="sncode"></param>
    /// <returns></returns>
    public BarCodeInfo GetBarCodeInfoBySn(string sncode)
    {
        BarCodeInfo entity = new BarCodeInfo();
        string sql = string.Format(@"SELECT * FROM Fun_GetInfoFromBarCode('{0}')", sncode);
        var dt = DBHelp.ExecuteToDataTable(sql);
        foreach (DataRow row in dt.Rows)
        {
            entity.ReturnValue = row["ReturnValue"].ToString();
            entity.ErrorMsg = row["ErrorMsg"].ToString();
            entity.RuleCode = row["RuleCode"].ToString();
            entity.CinvCode = row["CinvCode"].ToString();
            entity.DateCode = row["DateCode"].ToString();
            entity.Quantity = row["Quantity"] != DBNull.Value ? Convert.ToDecimal(row["Quantity"]) : decimal.Zero;
        }

        return entity;
    }
    /// <summary>
    /// 根据flagType 获取所有子项
    /// </summary>
    /// <param name="flagType"></param>
    /// <returns></returns>
    public List<V_SYS_PARAMETER> GetParametersByFlagType(string flagType)
    {
        if (string.IsNullOrEmpty(flagType))
        {
            return null;
        }
        //var parameterList = this.GetAllParameterList();
        //if (parameterList != null && parameterList.Any())
        //{
        //    return parameterList.Where(x => x.FLAG_TYPE == flagType).OrderBy(x => x.SORTID).ToList();
        //}
        //else {//以防出现问题没有数据时，再去取一遍
        string sysLang = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        var paraList = new SysParameterList().GetPARAMETERList(sysLang);
        if (paraList != null && paraList.Any())
        {
            return paraList.Where(x => x.FLAG_TYPE == flagType).OrderBy(x => x.SORTID).ToList();
        }
        else
        {
            return null;
        }
        //}
    }

    /// <summary>
    /// 获取匿名对象某一列的值
    /// </summary>
    /// <param name="obj">匿名对象</param>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    private string GetObjectValue(object obj, string columnName)
    {
        string columnValue = "";
        var property = obj.GetType().GetProperties().Where(x => x.Name == columnName).FirstOrDefault();
        if (property != null)
        {
            return property.GetValue(obj).ToString();
        }
        else
        {
            return columnValue;
        }
    }

    public DataTable GetGridSourceDataByList(IList srcData, string srcColumnName, string flagType, string srcColumnName1 = "", string flagType1 = "")
    {
        var result = new DataTable();
        //源数据为null直接返回null
        if (srcData == null)
        {
            return result;
        }
        //源数据无数据时直接返回源数据
        if (!srcData.Any())
        {
            return result;
        }
        ////源字段或新字段为空时直接返回源数据
        if (string.IsNullOrEmpty(srcColumnName) || string.IsNullOrEmpty(flagType))
        {
            return result;
        }

        var parameterList = GetParametersByFlagType(flagType);
        var parameterList1 = GetParametersByFlagType(flagType1);

        int seq = 0;
        foreach (var item in srcData)
        {
            if (seq == 0)
            {
                foreach (var property in item.GetType().GetProperties())
                {
                    var propertyType = property.PropertyType;
                    if ((propertyType.IsGenericType) && (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        propertyType = propertyType.GetGenericArguments()[0];
                    if (property.Name.ToUpper() == srcColumnName.ToUpper() || property.Name.ToUpper() == srcColumnName1.ToUpper())
                    {
                        result.Columns.Add(property.Name, typeof(string));
                    }
                    else
                    {
                        result.Columns.Add(property.Name, propertyType);
                    }
                }
            }

            DataRow row = result.NewRow();

            foreach (var property in item.GetType().GetProperties())
            {
                if (property.Name.ToUpper() == srcColumnName.ToUpper())
                {
                    var valObj = property.GetValue(item);
                    string srcValue = valObj != null ? property.GetValue(item).ToString() : "";
                    string newValue = "";
                    var currentPa = parameterList != null ? parameterList.Where(x => x.FLAG_ID == srcValue).FirstOrDefault() : null;
                    newValue = currentPa != null ? currentPa.FLAG_NAME : "";
                    row[property.Name] = newValue;
                }
                else if (property.Name.ToUpper() == srcColumnName1.ToUpper())
                {
                    var valObj = property.GetValue(item);
                    string srcValue = valObj != null ? property.GetValue(item).ToString() : "";
                    string newValue = "";
                    var currentPa = parameterList1 != null ? parameterList1.Where(x => x.FLAG_ID == srcValue).FirstOrDefault() : null;
                    newValue = currentPa != null ? currentPa.FLAG_NAME : "";
                    row[property.Name] = newValue;
                }
                else
                {
                    row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                }
            }
            result.Rows.Add(row);
            seq++;
        }
        return result;
    }

    /// <summary>
    /// 获取数据源(修改其中状态值)
    /// </summary>
    /// <param name="srcData"></param>
    /// <param name="flagList"></param>
    /// <returns></returns>
    public DataTable GetGridSourceDataByList(IList srcData, List<Tuple<string, string>> flagList)
    {
        var result = new DataTable();
        //源数据为null直接返回null
        if (srcData == null || flagList == null)
        {
            return result;
        }
        //源数据无数据时直接返回源数据
        if (!srcData.Any() || !flagList.Any())
        {
            return result;
        }

        List<Tuple<string, List<V_SYS_PARAMETER>>> parameterList = new List<Tuple<string, List<V_SYS_PARAMETER>>>();

        foreach (var item in flagList)
        {
            var parameters = GetParametersByFlagType(item.Item2);
            parameterList.Add(new Tuple<string, List<V_SYS_PARAMETER>>(item.Item2, parameters));
        }

        int seq = 0;
        foreach (var item in srcData)
        {
            if (seq == 0)
            {
                foreach (var property in item.GetType().GetProperties())
                {
                    var propertyType = property.PropertyType;
                    if ((propertyType.IsGenericType) && (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        propertyType = propertyType.GetGenericArguments()[0];

                    if (flagList.Any(x => x.Item1.ToUpper() == property.Name.ToUpper()))
                    {
                        result.Columns.Add(property.Name, typeof(string));
                    }
                    else
                    {
                        result.Columns.Add(property.Name, propertyType);
                    }
                }
            }

            DataRow row = result.NewRow();

            foreach (var property in item.GetType().GetProperties())
            {
                if (flagList.Any(x => x.Item1.Trim().ToUpper() == property.Name.ToUpper()))
                {
                    var valObj = property.GetValue(item);
                    string srcValue = valObj != null ? property.GetValue(item).ToString().Trim() : "";
                    string newValue = "";
                    var key = flagList.Where(x => x.Item1.Trim().ToUpper() == property.Name.ToUpper()).FirstOrDefault();
                    var currParameters = parameterList.Where(x => x.Item1.Trim().Equals(key.Item2)).FirstOrDefault().Item2;
                    var currentPa = currParameters != null ? currParameters.Where(x => x.FLAG_ID == srcValue).FirstOrDefault() : null;
                    newValue = currentPa != null ? currentPa.FLAG_NAME : "";
                    row[property.Name] = newValue;
                }
                else
                {
                    row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                }
            }
            result.Rows.Add(row);
            seq++;
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="srcData"></param>
    /// <param name="flagList">第一个参数：源字段 , 第二个参数:对应新增的字段，第三个参数：源字段对应代码组</param>
    /// <returns></returns>
    public DataTable GetGridDataByAddColumns(IList srcData, List<Tuple<string, string, string>> flagList)
    {
        var result = new DataTable();
        //源数据为null直接返回null
        if (srcData == null || flagList == null)
        {
            return result;
        }
        //源数据无数据时直接返回源数据
        if (!srcData.Any() || !flagList.Any())
        {
            return result;
        }

        List<Tuple<string, List<V_SYS_PARAMETER>>> parameterList = new List<Tuple<string, List<V_SYS_PARAMETER>>>();

        foreach (var item in flagList)
        {
            var parameters = GetParametersByFlagType(item.Item3);
            parameterList.Add(new Tuple<string, List<V_SYS_PARAMETER>>(item.Item3, parameters));
        }

        int seq = 0;
        foreach (var item in srcData)
        {
            if (seq == 0)
            {
                foreach (var property in item.GetType().GetProperties())
                {
                    var propertyType = property.PropertyType;
                    if ((propertyType.IsGenericType) && (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        propertyType = propertyType.GetGenericArguments()[0];

                    result.Columns.Add(property.Name, propertyType);
                }
                foreach (var flag in flagList)
                {
                    result.Columns.Add(flag.Item2, typeof(string));
                }
            }

            DataRow row = result.NewRow();

            foreach (var property in item.GetType().GetProperties())
            {
                if (flagList.Any(x => x.Item1.ToUpper() == property.Name.ToUpper()))
                {
                    var valObj = property.GetValue(item);
                    string srcValue = valObj != null ? property.GetValue(item).ToString() : "";
                    string newValue = "";
                    var key = flagList.Where(x => x.Item1.ToUpper() == property.Name.ToUpper()).FirstOrDefault();
                    var currParameters = parameterList.Where(x => x.Item1.Equals(key.Item3)).FirstOrDefault().Item2;
                    var currentPa = currParameters != null ? currParameters.Where(x => x.FLAG_ID == srcValue).FirstOrDefault() : null;
                    newValue = currentPa != null ? currentPa.FLAG_NAME : "";

                    row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                    row[key.Item2] = newValue;
                }
                else
                {
                    row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                }
            }
            result.Rows.Add(row);
            seq++;
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="srcData"></param>
    /// <param name="flagList">第一个参数：源字段 , 第二个参数:对应新增的字段，第三个参数：源字段对应代码组</param>
    /// <returns></returns>
    public DataTable GetGridDataByDataTable(DataTable srcData, List<Tuple<string, string, string>> flagList)
    {

        //源数据为null直接返回null
        if (srcData == null || flagList == null)
        {
            return srcData;
        }
        //源数据无数据时直接返回源数据
        if (srcData.Rows.Count == 0 || !flagList.Any())
        {
            return srcData;
        }

        List<Tuple<string, List<V_SYS_PARAMETER>>> parameterList = new List<Tuple<string, List<V_SYS_PARAMETER>>>();

        foreach (var item in flagList)
        {
            var parameters = GetParametersByFlagType(item.Item3);
            parameterList.Add(new Tuple<string, List<V_SYS_PARAMETER>>(item.Item3, parameters));
        }

        foreach (var flag in flagList)
        {
            srcData.Columns.Add(flag.Item2, typeof(string));
        }

        foreach (DataRow item in srcData.Rows)
        {
            foreach (var flag in flagList)
            {
                var valObj = item[flag.Item1];
                string srcValue = valObj != null ? valObj.ToString() : "";
                string newValue = "";
                var currParameters = parameterList.Where(x => x.Item1.Equals(flag.Item3)).FirstOrDefault().Item2;
                var currentPa = currParameters != null ? currParameters.Where(x => x.FLAG_ID == srcValue).FirstOrDefault() : null;
                newValue = currentPa != null ? currentPa.FLAG_NAME : "";
                item[flag.Item2] = newValue;
            }
        }
        return srcData;
    }



    /// <summary>
    /// 更新list某列的值，根据值取名称更新到值这一列
    /// </summary>
    /// <param name="queryList"></param>
    /// <param name="groupKEY"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public IList UpdateListColumnValue(IList queryList, string groupKEY, string columnName)
    {
        if (queryList == null || !queryList.Any())
        {
            return queryList;
        }
        string name = "";
        string keyid = "";
        for (int i = 0; i < queryList.Count; i++)
        {
            var obj = queryList[i];
            var property = obj.GetType().GetProperties().Where(x => x.Name == columnName).FirstOrDefault();
            if (property != null)
            {
                try
                {
                    keyid = property.GetValue(obj).ToString();
                    name = new SysParameterList().GetParametersNameByFlagType(groupKEY, keyid);
                    property.SetValue(obj, name);
                }
                catch
                {
                    property.SetValue(obj, "");
                }
            }
        }

        return queryList;
    }
    /// <summary>
    /// 获得台车数据源
    /// </summary>
    /// <param name="story"></param>
    /// <returns></returns>
    public DataTable GetCARList(string story)
    {
        string strSQL = string.Empty;
        strSQL = @"SELECT DISTINCT crane.CRANEID,crane.CRANENAME FROM dbo.BASE_CRANECONFIG crane WITH(NOLOCK)
                    JOIN dbo.BASE_CRANECONFIG_DETIAL cd with(NOLOCK) ON crane.CRANEID = cd.CRANEID
                    WHERE crane.PLCType = 'CAR' AND crane.FLAG=0 AND cd.STOREY = '" + story + "'";
        var dt = DBHelp.ExecuteToDataTable(strSQL);
        return dt;
    }

    /// <summary>
    /// list转datatable
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static DataTable ListToDataTable(IList list)
    {
        System.Data.DataTable result = new System.Data.DataTable();
        if (list.Count > 0)
        {
            PropertyInfo[] propertys = list[0].GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                //获取类型
                Type colType = pi.PropertyType;
                //当类型为Nullable<>时
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                result.Columns.Add(pi.Name, colType);
            }
            for (int i = 0; i < list.Count; i++)
            {
                ArrayList tempList = new ArrayList();
                foreach (PropertyInfo pi in propertys)
                {
                    object obj = pi.GetValue(list[i], null);
                    tempList.Add(obj);
                }
                object[] array = tempList.ToArray();
                result.LoadDataRow(array, true);
            }
        }
        return result;
    }

}

/// <summary>
/// 静态扩展方法
/// </summary>
public static class ToolsFunction
{
    public static bool IsDate(this string str)
    {
        bool bl = false;
        DateTime dt = new DateTime();
        bl = DateTime.TryParse(str, out dt);
        return bl;
    }

    public static bool IsDate(this string str, string formatStr)
    {
        bool bl = false;
        DateTime dt;
        if (DateTime.TryParseExact(str, formatStr, null, DateTimeStyles.None, out dt))
        {
            bl = true;
        }
        return bl;
    }

    public static bool IsNullOrEmpty(this string str)
    {
        bool bl = false;
        bl = string.IsNullOrEmpty(str);
        return bl;
    }

    public static int ToInt32(this decimal d)
    {
        int i = 0;
        i = Decimal.ToInt32(d);
        return i;
    }

    public static int ToInt(this string d)
    {
        int i = 0;
        int.TryParse(d, out i);
        return i;
    }

    public static bool IsDecimal(this string str)
    {
        decimal d = 0;
        bool bl = decimal.TryParse(str, out d);
        return bl;
    }

}

public static class MyEnumerableExtensions
{
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        HashSet<TKey> seenKeys = new HashSet<TKey>();
        foreach (TSource element in source)
        {
            if (seenKeys.Add(keySelector(element))) { yield return element; }
        }
    }
}
