using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Data.OleDb;
using DreamTek.ASRS.Business.Import;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;

/// <summary>
/// 描述: 111-->FrmBAS_AREAEdit 页面后台类文件
/// 作者: --wujingwei
/// 创建于: 2012-11-22 20:55:46
/// 
/// Roger
/// 2013/5/10 10:55:28
/// 20130510105528
/// 工单合并发料，增加线体等上传
/// </summary>
/*
* Roger
* 2013/7/5 11:01:50
* 20130705110150
* 增加部门等上传
*/
/*
* Roger
* 2013/7/26 15:35:06
* 20130726153506
* 如果已经合并或者拣货了，只能修改【是否计算工时】【上线日期】这两个栏位
*/
/*
* Roger
* 2013/8/1 9:39:12
* 20138193912
* 时间统一获取DB时间
*/
public partial class FrmUPGDDatat : WMSBasePage //PageBase, IPageEdit
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();
            ShowData();
        }
       
        IsShow();

        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();

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
        // this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BAR');return false;";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BAR');return false;";

        this.btnSelect_Sucess.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmImportDateList.aspx", SYSOperation.Modify, "") + "','上传工单资料','UPGD');return false;";

        
    }

    #endregion

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        //BAS_AREAEntity entity = new BAS_AREAEntity();
        //entity.ID = this.KeyID;
        //entity.SelectByPKeys();
        //this.txtId.Text = entity.ID;
        //txtAreaName.Text = entity.AREA_NAME;
        //this.txtCreateOwner.Text = entity.CREATEOWNER;
        //this.txtCreateTime.Text = entity.CREATETIME.ToString("yyyy-MM-dd hh:mm:ss");

        //txtCMEMO.Text = entity.MEMO;


    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {

        //if (this.txtAreaName.Text.Trim() == "")
        //{
        //    this.Alert("区域名称项不允许空！");
        //    this.SetFocus(txtAreaName);
        //    return false;
        //}
        ////区域名称
        //if (this.txtAreaName.Text.Trim().Length > 0)
        //{
        //    if (this.txtAreaName.Text.GetLengthByByte() > 100)
        //    {
        //        this.Alert("区域名称项超过指定的长度100！");
        //        this.SetFocus(txtAreaName);
        //        return false;
        //    }
        //}

        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public BASE_AREA SendData()
    {
        BASE_AREA entity = new BASE_AREA();
        //entity.ID = Guid.NewGuid().ToString();
        ////
        //this.txtAreaName.Text = this.txtAreaName.Text.Trim();
        //if (this.txtAreaName.Text.Length > 0)
        //{
        //    entity.AREA_NAME = txtAreaName.Text;
        //}
        //else
        //{
        //    entity.SetDBNull("AREA_NAME");
        //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        //    //entity.CCREATEOWNERCODE = null;
        //}


        //this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        //if (this.txtCMEMO.Text.Length > 0)
        //{
        //    entity.MEMO = txtCMEMO.Text;
        //}
        //else
        //{
        //    entity.SetDBNull("MEMO");
        //    //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
        //    //entity.CMEMO = null;
        //}


        // entity.CREATETIME=DateTime.Now;
        // entity.CREATEOWNER = WmsWebUserInfo.GetCurrentUser().UserNo;

        //entity.LASTVPDOWNER = WmsWebUserInfo.GetCurrentUser().UserNo;

        return entity;

    }

    /// <summary>
    /// execl的导入类型
    /// </summary>
    enum ExcelToDSType
    {
        /// <summary>
        /// 验证
        /// </summary>
        Check = 1,
        /// <summary>
        /// 导入
        /// </summary>
        Importing = 2
    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUP_Click(object sender, EventArgs e)
    {
        int count = 0;
        int upCount = 0;
        int saveCount = 0;
        int errCount = 0;
        string date = string.Empty;
        string errMsg = string.Empty;
        try
        {
            //AppLog.Write("---------------文件上传结束开始导入---------------");
            if (fuFile.PostedFile.FileName == "")
            {
                //"请选择要上载的文件"
                base.Alert(Resources.Lang.FrmUPGDDatat_MSG1);

                return;
            }
            var extension = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
            if (extension != null && (extension.ToLower() != ".xls" && extension.ToLower() != ".xlsx"))
            {
                //上载的文件类型不正确，必须为,或者
                base.Alert(Resources.Lang.FrmUPGDDatat_MSG2 + "，" + Resources.Lang.FrmUPGDDatat_MSG3 + "*.xls" + Resources.Lang.FrmUPGDDatat_MSG4 + "*.xlsx");

                return;
            }
            DateTime dt = Comm_Fun.GetDBNowTime().Value;//20138193912;

            var s = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
            if (s != null)
            {
                string fileName = dt.Year + dt.Month.ToString() + dt.Day + dt.Hour + dt.Minute + dt.Second + s.ToLower();
                string requestUrl = Request.Path.ToLower();
                int index = requestUrl.IndexOf("apps/", 0);
                string savePath = Server.MapPath(requestUrl.Substring(0, index)) + @"TempFile\" + fileName;
                fuFile.PostedFile.SaveAs(@savePath);

                DataTable ds = ExcelToDS(savePath);

                int i = 0;
                bool isTrueCol = false;
                bool isTrulBB = false;//班别
                using (DataTable dt1 = ds)
                {
                    errCount = 0;
                    count = 0;
                    saveCount = 0;
                    upCount = 0;
                    if (dt1.Columns.Contains(Resources.Lang.FrmUPGDDatat_MSG5))//"备料日期"
                        isTrueCol = true;
                    if (dt1.Columns.Contains(Resources.Lang.FrmUPGDDatat_MSG6))//"班别"
                        isTrulBB = true;
                    #region 模板检查
                    if (dt1.Rows.Count > 0)
                    {
                        //20130705110150
                        if (!dt1.Columns.Contains("机种") || !dt1.Columns.Contains("工单") || !dt1.Columns.Contains("工单量") || !dt1.Columns.Contains("备料日期") || !dt1.Columns.Contains("班别")
                             || !dt1.Columns.Contains("线体") || !dt1.Columns.Contains("特殊") || !dt1.Columns.Contains("二次用料") || !dt1.Columns.Contains("部门编码")
                             || !dt1.Columns.Contains("计算工时") || !dt1.Columns.Contains("上线日期") || !dt1.Columns.Contains("SIDE")  
                            )
                        {
                            //未使用新模板导入工单信息
                            errMsg += Resources.Lang.FrmUPGDDatat_MSG7+"<br />";
                            i++;
                            return;
                        }
                        else
                        {
                            ImportDate idr = new ImportDate();
                            int SucCount, NGCount;
                            idr.ImportIN_MO_Info(dt1, WmsWebUserInfo.GetCurrentUser().UserName, out SucCount, out NGCount);
                            lblOKCount.Text = SucCount.ToString();
                            lblNGCount.Text = NGCount.ToString();
                            lblTotalCount.Text = (SucCount + NGCount).ToString();

                            btnSearch_Click(null, null);
                            IsUp.Value = "0";
                            IsShow();

                        }
                    }
                    else
                    {
                        //未使用正确模板或工单信息为空,请确认
                        errMsg += Resources.Lang.FrmUPGDDatat_MSG8+"！<br />";
                    }
                    #endregion
                }
            }

            tdOut.InnerHtml = errMsg;
            //ltMsg.Text += date;
        }
        catch (Exception ex)
        {
            //异常信息
            this.Alert(Resources.Lang.FrmUPGDDatat_MSG9+"：" + ex.Message);
            // errCount++;
        }

    }
  
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        IsUp.Value = "1";
        IsShow();
    }

    void IsShow()
    {
        if (IsUp.Value == "1")
        {
            div2.Visible = false;
            div1.Visible = true;
        }
        else
        {
            div1.Visible = false;
            div2.Visible = true;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ImportDate exp = new ImportDate();
        //      ImportQuery qry = new ImportQuery();
        DataTable dt = new DataTable();
        string fileName = "";
        string filePath = Server.MapPath("~/TempFile/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        fileName = System.Web.HttpUtility.UrlEncode("工单维护信息.xls", System.Text.Encoding.UTF8); ;

        In_Mo_InfoQuery listQuery = new In_Mo_InfoQuery();
        dt = listQuery.GetTempList();
        //生成excel
        exp.DataTableExport(dt, filePath);
        //导出excel
        //Response.Clear();
        //Response.Buffer = true;
        //Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
        //Response.ContentType = "application/vnd.ms-excel";
        //Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        //Response.Charset = "gb2312";
        //Response.WriteFile(filePath);
        //Response.Flush();
        //Response.End();

        Response.Clear();
        Response.Buffer = true;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.Charset = "gb2312";
        Response.WriteFile(filePath);
        Response.Flush();
        Response.End();
    }

    #region execl操作
    /// <summary>
    /// 将excel转换成DataSet
    /// </summary>
    /// <param name="savePath"></param>
    /// <returns></returns>
    private DataSet ExcelToDS(string savePath, ExcelToDSType type)
    {
        string strPath = savePath;
        string strConn = string.Empty;
        if (strPath.ToLower().IndexOf(".xlsx") > 0)
        {
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + strPath + "';Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
        }
        if (strPath.ToLower().IndexOf(".xls") > 0 && strPath.EndsWith("xls"))
        {
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + strPath + "';Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";
        }

        DataSet ds = new DataSet();
        for (int i = 1; i <= 1; i++)
        {
            ds.Tables.Add(ExeclTable(i.ToString() + Resources.Lang.FrmUPGDDatat_MSG10, strConn, type));//"线"
        }

        return ds;
    }

    private DataTable ExcelToDS(string savePath)
    {
        string strPath = savePath;
        string strConn = string.Empty;
        ExcelToDSType type = ExcelToDSType.Check;
        if (strPath.ToLower().IndexOf(".xlsx") > 0)
        {
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + strPath + "';Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
        }
        if (strPath.ToLower().IndexOf(".xls") > 0 && strPath.EndsWith("xls"))
        {
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + strPath + "';Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";
        }

        DataTable dt = new DataTable();

        dt = (ExeclTable("1线", strConn, type));


        return dt;
    }


    ////获取excel对应所有sheet名
    //public static string[] GetExcelSheetNames(string filePath)
    //{
    //    var excelApp = new Excel.Application();
    //    var wbs = excelApp.Workbooks;
    //    var wb = wbs.Open(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
    //                      Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
    //                      Type.Missing, Type.Missing, Type.Missing, Type.Missing);
    //    var count = wb.Worksheets.Count;
    //    var names = new string[count];
    //    for (var i = 1; i <= count; i++)
    //    {
    //        names[i - 1] = ((Excel.Worksheet)wb.Worksheets[i]).Name;
    //    }
    //    return names;
    //}
    #endregion
    /// <summary>
    /// 获取execl表的内容
    /// </summary>
    /// <param name="TableName"></param>
    /// <param name="strConn"></param>
    /// <returns></returns>
    private DataTable ExeclTable(string TableName, string strConn, ExcelToDSType type)
    {

        //临时表单存放excel里面结果
        DataTable temp = new DataTable();
        //结果表单最终校验完的表单
        DataTable resultTable = new DataTable();
        try
        {
            //Oracle工单,工單量,备料日期
            OleDbDataAdapter myCommand = null;
            string strExcel = "select * from [" + TableName + "$]  ";
            myCommand = new OleDbDataAdapter(strExcel, strConn);

            myCommand.Fill(temp);

            return temp;
        }
        catch (Exception ex)
        {
             base.Alert(ex.Message);
        }
        return temp;
    }

    #region 错误数据显示

    public void GridBind()
    {
        try
        {
            IGenericRepository<TEMP_IN_MO_INFO> entity = new GenericRepository<TEMP_IN_MO_INFO>(context);
            var caseList = from p in entity.Get()
                           orderby p.create_time descending
                           where 1 == 1&& p.issave=="1"
                           select p;

            //if (txtWO.Text != string.Empty)
            //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.Contains(txtWO.Text));
            //if (!string.IsNullOrEmpty(txtDateFrom.Text.Trim()))
            //    caseList = caseList.Where(x => x.dcreatetime != null && x.dcreatetime >= Convert.ToDateTime(txtDateFrom.Text.Trim()));
            //if (txtDateTo.Text != string.Empty)
            //    caseList = caseList.Where(x => x.dcreatetime != null && x.dcreatetime <= Convert.ToDateTime(txtDateTo.Text.Trim()).AddDays(1));
       
            if (caseList != null && caseList.Count() > 0)
            {
                AspNetPager1.RecordCount = caseList.Count();
                AspNetPager1.PageSize = this.PageSize;
            }

            AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";
            grdtempINMo.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
            grdtempINMo.DataBind();

            //In_Mo_InfoQuery listQuery = new In_Mo_InfoQuery();
            //DataTable dtSource = listQuery.GetTempList(txtWO.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false, this.grdNavigatorINMO.CurrentPageIndex, this.grdtempINMo.PageSize);
            //this.grdtempINMo.DataSource = dtSource;
            //this.grdtempINMo.DataBind();
        }
        catch (Exception)
        {

        }
    }
   
    protected void grdtempINMo_PageIndexChanged(object sender, EventArgs e)
    {
        GridBind();
    }

   

    protected void grdtempINMo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void grdtempINMo_DataBinding(object sender, EventArgs e)
    {
    }
    #endregion

}

