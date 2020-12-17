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
public partial class FrmUPGDData :WMSBasePage// PageBase, IPageEdit
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() != SYSOperation.New)
            {
                //ShowData();
                //grdBAR_D.DataBind();
                // sPanel.Visible = true;
            }
            //else
            //{
            //    //txtID.Text = Guid.NewGuid().ToString();
            //    txtCreateOwner.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
            //    txtCreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            //}
        }
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BAR');return false;";
        //设置保存按钮的文字及其状态
        // this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + PageBase.BuildRequestPageURL("FrmBAR_DEdit.aspx", SYSOperation.New, "") + "','新建','BAR_D');return false;";

        //if (this.Operation == SYSOperation.View)
        //{
        //    this.btnSave.Visible = false;
        //}
        //else if (this.Operation == SYSOperation.Approve)
        //{
        //    this.btnSave.Text =  Resources.Lang.CommonB_Approve;//"审批";
        //}
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly

    }

    #endregion



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
        IGenericRepository<IN_MO_INFO> con = new GenericRepository<IN_MO_INFO>(context);
        try
        {
            //AppLog.Write("---------------文件上传结束开始导入---------------");
            if (fuFile.PostedFile.FileName == "")
            {
                //请选择要上载的文件
                base.Alert(Resources.Lang.FrmUPGDData_MSG6);

                return;
            }
            var extension = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
            if (extension != null && (extension.ToLower() != ".xls" && extension.ToLower() != ".xlsx"))
            {
                //上载的文件类型不正确..必须为
                base.Alert(Resources.Lang.FrmUPGDData_MSG7 + "，" + Resources.Lang.FrmUPGDData_MSG8 + "*.xls" + Resources.Lang.FrmUPGDData_MSG9 + "*.xlsx");

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

                DataSet ds = ExcelToDS(savePath, ExcelToDSType.Check);

                int i = 0;
                bool isTrueCol = false;
                bool isTrulBB = false;//班别
                foreach (DataTable dt1 in ds.Tables)
                {
                    errCount = 0;
                    count = 0;
                    saveCount = 0;
                    upCount = 0;
                    if (dt1.Columns.Contains(Resources.Lang.FrmUPGDData_MSG11))//"备料日期"
                        isTrueCol = true;
                    if (dt1.Columns.Contains(Resources.Lang.FrmUPGDData_MSG10))//班别
                        isTrulBB = true;
                    //if (!dt1.Columns.Contains("线体"))
                    //{
                    //    errMsg = "模块格式不正确![线体]列不存在";
                    //    break;
                    //}

                    #region 模板检查
                    if (dt1.Rows.Count > 0)
                    {
                        //20130705110150
                        if (!dt1.Columns.Contains("线体") || !dt1.Columns.Contains("特殊") || !dt1.Columns.Contains("部门编码") || !dt1.Columns.Contains("计算工时") || !dt1.Columns.Contains("上线日期"))
                        {
                            errMsg += (i + 1) + Resources.Lang.FrmUPGDData_MSG12 + "<br />";//线，未使用新模板导入工单信息
                            i++;
                            continue;
                        }
                    }
                    #endregion

                    foreach (DataRow dr in dt1.Rows)
                    {

                        #region 工单检查

                        bool isOk = false;

                        //判断工单号是否为空
                        //Oracle工单,工單量,备料日期
                        if (!string.IsNullOrEmpty(dr["Oracle工单"].ToString().Trim()))
                        {
                            //20130726153506
                            var isChange = false;
                            count++;
                            //
                            if (dr["线体"].ToString().Trim().Length == 0)
                            {
                                //线，工单号,的线体未输入
                                errMsg += (i + 1) + Resources.Lang.FrmUPGDData_MSG13 + ":" + dr["Oracle工单"].ToString().Trim() + Resources.Lang.FrmUPGDData_MSG14 + "。<br />";
                                continue;
                            }
                            //Roger 20120615 线体不在对应数据中报错
                            if (!In_Mo_InfoQuery.GetLine(dr["线体"].ToString().Trim()))
                            {
                                //线，工单号...的线体不在可维护线体中
                                errMsg += (i + 1) + Resources.Lang.FrmUPGDData_MSG13 + ":" + dr["Oracle工单"].ToString().Trim() + Resources.Lang.FrmUPGDData_MSG15 + "。<br />";
                                continue;
                            }
                            //CQ 2014-8-7 16:11:01新增SWR状态检查
                            string StrSWR = string.Empty;
                            StrSWR = Comm_Fun.Fun_Check_SWR_Status(dr["Oracle工单"].ToString().Trim(), 0);
                            if (StrSWR != "OK")
                            {
                                //线
                                errMsg += (i + 1) + Resources.Lang.FrmUPGDData_MSG16 + "，" + StrSWR + "<br />";
                                continue;
                            }


                       

                            IN_MO_INFO info = new IN_MO_INFO();
                            info.id = Guid.NewGuid().ToString().Trim();
                            info.wo = dr["Oracle工单"].ToString().Trim();
                            //20130510105528--如果工单已指引、已合并、已补单则不允许修改备料日等信息
                            var changeDate = In_Mo_InfoQuery.CanChangeDate(info.wo);
                            if (!changeDate)
                            {
                                //errMsg += (i + 1) + "线，工单号:" + dr["Oracle工单"].ToString().Trim() + "已经备料，不允许修改备料日。<br />";
                                //continue;
                                //20130726153506
                                isChange = true;
                            }

                            //20130705110150
                            #region 部门编码
                            //部门编码
                            if (dr["部门编码"] != null)
                            {
                                info.departmentno = dr["部门编码"].ToString().Trim().ToUpper();
                                //判断部门编码是否在维护表中
                                var isRightDepartmentno = In_Mo_InfoQuery.HasMaintenance(info.departmentno);
                                if (!isRightDepartmentno)
                                {
                                    //线，工单号..的部门编码不在可维护部门编码中
                                    errMsg += (i + 1) + Resources.Lang.FrmUPGDData_MSG13 + ":" + dr["Oracle工单"].ToString().Trim() +
                                               Resources.Lang.FrmUPGDData_MSG17 + "。<br />";
                                    continue;
                                }
                            }
                            else
                            {
                                //线，工单号..部门编码为空
                                errMsg += (i + 1) + Resources.Lang.FrmUPGDData_MSG13 + ":" + dr["Oracle工单"].ToString().Trim() + Resources.Lang.FrmUPGDData_MSG18 + "。<br />";
                                continue;
                            }
                            #endregion

                            #region 计算工时
                            //计算工时
                            if (dr["计算工时"] != null)
                            {
                                info.iscountjobtime = dr["计算工时"].ToString().Trim().ToUpper();
                                if (info.iscountjobtime != "Y" && info.iscountjobtime != "N")
                                {
                                    //线，工单号..计算工时不为.'Y'或者'N'
                                    errMsg += (i + 1) + Resources.Lang.FrmUPGDData_MSG13 + ":" + dr["Oracle工单"].ToString().Trim()
                                        + Resources.Lang.FrmUPGDData_MSG19 + "'Y'" + Resources.Lang.FrmUPGDData_MSG20 + "'N'。<br />";
                                    continue;
                                }
                            }
                            else
                            {
                                errMsg += (i + 1) + Resources.Lang.FrmUPGDData_MSG11 + "线，工单号:" + dr["Oracle工单"].ToString().Trim() + Resources.Lang.FrmUPGDData_MSG11 + "计算工时为空。<br />";
                                continue;
                            }
                            #endregion

                            #region 上线日期
                            //上线日期
                            if (dr["上线日期"] != null)
                            {
                                try
                                {
                                    info.onlinetime = DateTime.Parse(dr["上线日期"].ToString().Trim());
                                }
                                catch (Exception)
                                {
                                    //线，工单号..上线日期不为有效时间格式
                                    errMsg += (i + 1) + Resources.Lang.FrmUPGDData_MSG13 + ":" + dr["Oracle工单"].ToString().Trim() + Resources.Lang.FrmUPGDData_MSG21 + "。<br />";
                                    continue;
                                }
                            }
                            else
                            {
                                //线，工单号..上线日期为空
                                errMsg += (i + 1) + Resources.Lang.FrmUPGDData_MSG13 + ":" + dr["Oracle工单"].ToString().Trim() + Resources.Lang.FrmUPGDData_MSG22 + "。<br />";
                                continue;
                            }
                            #endregion

                            info.wo_qty = dr["工單量"].ToString().Trim();

                            info.sec_cinvcode = dr["二次用料"].ToString().Trim();

                            info.models = dr["机种"].ToString().Trim();

                            #region 备料日期
                            if (isTrueCol)
                            {
                                if (!(dr["备料日期"] is DBNull))
                                {
                                    //dr["备料日期"].ToString().IsDateTime()
                                    info.start_date = Convert.ToDateTime(dr["备料日期"].ToString().Trim());
                                }
                            }
                            #endregion

                            #region 班别
                            if (isTrulBB)
                            {
                                if (dr["班别"].ToString().Trim() == "白" || dr["班别"].ToString().Trim() == "夜")
                                {
                                    info.shift = dr["班别"].ToString().Trim();
                                }
                                else
                                {
                                    //线，工单号..的班别只能是[白]或者[夜]
                                    errMsg += (i + 1) + Resources.Lang.FrmUPGDData_MSG13 + ":" + info.wo + Resources.Lang.FrmUPGDData_MSG23 + "。<br />";
                                    continue;
                                }
                            }
                            #endregion

                            //20130510105528
                            info.create_owner = WmsWebUserInfo.GetCurrentUser().UserNo;
                            info.create_time = DateTime.Now;
                            info.last_upd_owner = WmsWebUserInfo.GetCurrentUser().UserNo;
                            info.last_upd_time = Comm_Fun.GetDBNowTime().Value;//20138193912;
                            //20130510105528
                            info.linebody = dr["线体"].ToString().Trim();
                            //info.SIDE = dr["Side"].ToString();
                            info.special = string.IsNullOrEmpty(dr["特殊"].ToString().Trim())
                                               ? "0"
                                               : dr["特殊"].ToString().Trim();

                            #region 判断是更新还是保存
                            DataTable dt2 = new In_Mo_InfoQuery().GetIn_InfoBYIdName(info.wo);
                            //info.START_DATE.ToString("yyyy-MM-dd hh:mm:ss")
                            if (dt2.Rows.Count > 0)
                            {
                                #region 更新
                                //更新
                                info.id = dt2.Rows[0]["ID"].ToString().Trim();
                                //20130726153506
                                if (isChange)
                                {
                                    info.iscountjobtime = dr["计算工时"].ToString().Trim().ToUpper();
                                    info.sec_cinvcode = dr["二次用料"].ToString().Trim();//CQ 2014-6-26 10:56:11
                                    info.models = dr["机种"].ToString().Trim();//CQ 2014-9-1 10:46:14
                                    info.onlinetime = DateTime.Parse(dr["上线日期"].ToString().Trim());
                                    info.last_upd_owner = WmsWebUserInfo.GetCurrentUser().UserNo;
                                    info.last_upd_time = Comm_Fun.GetDBNowTime().Value;//20138193912;
                                    //线，工单号。。已经备料，只能修改【是否计算工时】【上线日期】【二次用料】这三个栏位
                                    errMsg += (i + 1) + Resources.Lang.FrmUPGDData_MSG13 + ":" + dr["Oracle工单"].ToString().Trim() +
                                               Resources.Lang.FrmUPGDData_MSG24 + "。<br />";
                                }
                                con.Delete(info.id);
                                con.Save();
                                con.Insert(info);
                                con.Save();
                                upCount++;
                                #endregion
                            }
                            else
                            {
                                info.create_owner = WmsWebUserInfo.GetCurrentUser().UserNo;
                                info.create_time = Comm_Fun.GetDBNowTime().Value;//20138193912;
                                con.Insert(info);
                                con.Save();
                                saveCount++;
                            }
                            #endregion
                        }
                        #endregion

                        //else
                        //{
                        //    errCount++;
                        //    continue;
                        //}

                    }
                    isTrueCol = false;
                    isTrulBB = false;
                    i++;
                    //if (errCount > 0)
                    //    errMsg += i + "线导入成功" + saveCount.ToString() + "条,更新" + upCount.ToString() + "条，失败" + errCount.ToString() + "条。<br />";
                    //else
                    //线新增成功,条,更新
                    errMsg += i + Resources.Lang.FrmUPGDData_MSG25 + "" + saveCount.ToString() + Resources.Lang.FrmUPGDData_MSG26 + "" + upCount.ToString() + Resources.Lang.FrmUPGDData_MSG11 + "条。<br />";
                }
            }
            #region 查询临时的存的数据
            // this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
            #endregion
            tdOut.InnerHtml = errMsg;
            //ltMsg.Text += date;
        }
        catch (Exception ex)
        {
            this.Alert(Resources.Lang.FrmUPGDData_MSG27 + "：" + ex.Message);//异常信息
            // errCount++;
        }

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
        for (int i = 1; i <= 30; i++)
        {
            ds.Tables.Add(ExeclTable(i.ToString() + Resources.Lang.FrmUPGDData_MSG28, strConn, type));//+ "线"
        }

        return ds;
    }

  
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
            // base.Alert(ex.Message);
        }
        return temp;
    }

    #endregion
}

