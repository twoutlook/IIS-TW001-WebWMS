using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Text;
using DreamTek.ASRS.Business.Import;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;

public partial class Apps_Import_FrmBaseImport : WMSBasePage
{
    DBContext context = new DBContext();
    /// <summary>
    /// 当前导入的类型
    /// </summary>
    public string CurrentXlsName
    {
        get
        {
            if (ViewState["CurrentXlsName"] == null)
            {
                ViewState["CurrentXlsName"] = string.Empty;
            }
            return ViewState["CurrentXlsName"].ToString();
        }
        set
        {
            ViewState["CurrentXlsName"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();

            string typeName = "";
            if (Request.QueryString["ImportType"] == "In")
            {
                typeName = Resources.Lang.FrmBaseImport_MSG61;// "入";
            }
            else
            {
                typeName = Resources.Lang.FrmBaseImport_MSG62;// "出";
            }

        }


    }



    #region IPageGrid 成员

    public void GridBind()
    {
    }

    public bool CheckData()
    {

        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTASN_D');return false;";
        //this.grdINASN.DataKeyNames = new string[]{"ID"};
        
    }

    #endregion

    protected DataTable grdNavigatorINASN_GetExportToExcelSource()
    {
        DataTable dtSource = null;//listQuery.GetList(this.txtID.Text, this.txtCTICKETCODE.Text, this.txtCSTATUS.SelectedValue, this.txtCCREATEOWNERCODE.Text, this.txtDCREATETIMEFrom.Text, this.txtDCREATETIMETo.Text, this.txtCAUDITPERSONCODE.Text, this.txtDAUDITDATEFrom.Text, this.txtDAUDITDATETo.Text, this.txtCPO.Text, txtERP_No.Text, this.txtITYPE.SelectedValue, false, -1, -1);
        return dtSource;
    }

    protected void grdINASN_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdINASN_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;

        try
        {

            if (msg.Length == 0)
            {
                msg = Resources.Lang.Common_SuccessDel;// "删除成功!";
            }

            this.GridBind();
        }
        catch (Exception E)
        {
            msg += Resources.Lang.Common_FailDel;// "删除失败!";
            //DBUtil.Rollback(); 
        }
        this.Alert(msg);
    }



    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";

        return strKeyId;
    }

    protected void grdINASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmINASNEdit.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBaseImport_MSG63, "INASN");//"入库通知单"

            string statusStr = string.Empty;
            switch (e.Row.Cells[e.Row.Cells.Count - 2].Text)
            {
                //0 未处理,1,已指引，2,上架中，3 已完成,）
                case "0":
                    statusStr = Resources.Lang.CommonB_CSTATUS_undisposed;//"未处理";
                    break;
                case "1":
                    statusStr = Resources.Lang.CommonB_CSTATUS_GUIDE;//"已指引";
                    break;
                case "2":
                    statusStr =Resources.Lang.CommonB_CSTATUS_PUTAWAY;// "上架中";
                    break;
                case "3":
                    statusStr = Resources.Lang.CommonB_CSTATUS_COMPLETE;//"已完成";
                    break;
            }
            e.Row.Cells[e.Row.Cells.Count - 2].Text = statusStr;
            e.Row.Cells[e.Row.Cells.Count - 7].Text = e.Row.Cells[e.Row.Cells.Count - 7].Text == "Y" ? Resources.Lang.Common_YES : Resources.Lang.Common_NO;//"是" : "否";
        }

    }

    protected void dsGrdINASN_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdINASN_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.ReturnValue is DataTable)
        {

        }

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

        switch (CurrentXlsName)
        {
            case "TEMP_IMPORT_SPACE_PART_AREA"://料号和储位、区域关联 WMS上架规则
                // int ExcelSize = Convert.ToInt32(Help.ReadWebconfig("ExcelSize").Trim());
                //for (int i = 1; i <= ExcelSize; i++)
                //{
                //string sql = "select 序號,倉別名称,儲位,倉別,區塊,是否混放,储位最大容量,料號,单位,單位最小数量,优先区域1,优先区域2,优先区域3 from [" + Help.ReadWebconfig("ExcelName" + i.ToString()).Trim() + "$]";
                string sql = "select * from [Sheet1$]";
                ds.Tables.Add(ExeclTable(strConn, sql, type));
                //}
                break;
            case "BASE_AREA"://区域信息
                string sqlArea = "select * from [Sheet1$]";
                //ds.Tables.Add(ExeclTable(strConn, sqlArea, type));
                ds.Tables.Add(ExeclTable(strConn, "select * from [Sheet1$]", type));
                break;
            case "BASE_OPERATOR_AREA_NEW"://人员对应区域
                string sqlOPERATOR_AREA = "select * from [Sheet1$]";
                ds.Tables.Add(ExeclTable(strConn, sqlOPERATOR_AREA, type));
                break;
            case "In_Out_AsnD"://出入库通知单明细
                //出入库通知单明细上传 select 料号,数量 from [Sheet1$]
                ds.Tables.Add(ExeclTable(strConn, "select * from [Sheet1$]", type));
                break;
            case "BASE_CLIENT"://客户
                ds.Tables.Add(ExeclTable(strConn, "select * from [Sheet1$]", type));
                break;
            case "Base_VENDOR"://供应商
                ds.Tables.Add(ExeclTable(strConn, "select * from [Sheet1$]", type));
                break;
            case "BASE_PART"://物料
                ds.Tables.Add(ExeclTable(strConn, "select * from [Sheet1$]", type));
                break;
            case "BASE_CARGOSPACE"://储位
                ds.Tables.Add(ExeclTable(strConn, "select * from [Sheet1$]", type));
                break;
            case "BASE_WAREHOUSE"://仓库
                ds.Tables.Add(ExeclTable(strConn, "select * from [Sheet1$]", type));
                break;

            case "BASE_DEPARTMENT"://部门
                ds.Tables.Add(ExeclTable(strConn, "select * from [Sheet1$]", type));
                break;

            case "BASE_LINE_LIST"://线体
                ds.Tables.Add(ExeclTable(strConn, "select * from [Sheet1$]", type));
                break;
            default:
                //出入库通知单明细上传 select 料号,数量 from [Sheet1$]
                ds.Tables.Add(ExeclTable(strConn, "select * from [Sheet1$]", type));
                break;
        }

        return ds;
    }

    /// <summary>
    /// 获取execl表的内容
    /// </summary>
    /// <param name="TableName"></param>
    /// <param name="strConn"></param>
    /// <returns></returns>
    private DataTable ExeclTable(string strConn, string strExcel, ExcelToDSType type)
    {

        //临时表单存放excel里面结果
        DataTable temp = new DataTable();
        //结果表单最终校验完的表单
        DataTable resultTable = new DataTable();
        try
        {
            OleDbDataAdapter myCommand = null;
            //Sheet1
            //string strExcel = "select 料号,数量 from [" + TableName + "$]";
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

    #endregion
    protected void btnUp_Click(object sender, EventArgs e)
    {
        try
        {
            //AppLog.Write("---------------文件上传结束开始导入---------------");

            CurrentXlsName = string.Empty;//根据excel的名字，确定导入excel的类型

            if (fuFile.PostedFile.FileName == "")
            {
                //base.Alert("请选择要上载的文件");
                base.Alert(Resources.Lang.FrmBaseImport_MSG64);
                return;
            }
            var extension = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
            if (extension != null && (extension.ToLower() != ".xls" && extension.ToLower() != ".xlsx"))
            {
                base.Alert(Resources.Lang.FrmBaseImport_MSG65 + "，" + Resources.Lang.FrmUPGDData_MSG8 + "*.xls" + Resources.Lang.FrmUPGDData_MSG9 + "*.xlsx");//"上载的文件类型不正确，必须为*.xls或者*.xlsx"
                return;
            }
            //this.lblMsg.Text = "开始上传文件...";
            DateTime dt = DateTime.Now;
            var s = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);

            if (s != null)
            {
                var xlsName = fuFile.PostedFile.FileName;
                if (xlsName.IndexOf("物料信息") >= 0)
                {
                    CurrentXlsName = "BASE_PART";
                }
                else if (xlsName.IndexOf("储位信息") >= 0)
                {
                    CurrentXlsName = "BASE_CARGOSPACE";
                }
                else if (xlsName.IndexOf("仓库资料") >= 0)
                {
                    CurrentXlsName = "BASE_WAREHOUSE";
                }
                else if (xlsName.IndexOf("区域资料") >= 0)
                {
                    CurrentXlsName = "BASE_AREA";
                }
                else if (xlsName.IndexOf("部门信息") >= 0)
                {
                    CurrentXlsName = "BASE_DEPARTMENT";
                }
                else if (xlsName.IndexOf("线体信息") >= 0)
                {
                    CurrentXlsName = "BASE_LINE_LIST";
                }
                else if (xlsName.IndexOf("库存信息") >= 0)
                {
                    CurrentXlsName = "BASE_STOCK";
                }

               // #region sxm
                else if (xlsName.IndexOf("供应商信息") >= 0)
                {
                    CurrentXlsName = "BASE_VENDOR";
                }
                else if (xlsName.IndexOf("客户信息") >= 0)
                {
                    CurrentXlsName = "BASE_CLIENT";
                }
                else if (xlsName.IndexOf("料号和储位、区域关联") >= 0)
                {
                    CurrentXlsName = "TEMP_IMPORT_SPACE_PART_AREA";
                }
                //#endregion

                string fileName = dt.Year + dt.Month.ToString() + dt.Day + dt.Hour + dt.Minute + dt.Second + s.ToLower();
                string requestUrl = Request.Path.ToLower();
                int index = requestUrl.IndexOf("apps/", 0);
                string savePath = Server.MapPath(requestUrl.Substring(0, index)) + @"TempFile\" + fileName;
                fuFile.PostedFile.SaveAs(@savePath);

                DataSet ds = ExcelToDS(savePath, ExcelToDSType.Check);

                StringBuilder errorMsg = new StringBuilder();


                decimal okCount = 0;
                decimal ngCount = 0;
                string cmsg = string.Empty;

                switch (CurrentXlsName)
                {
                    case "TEMP_IMPORT_SPACE_PART_AREA"://料号和储位、区域关联 WMS上架规则
                        cmsg = CheckTemplate(CurrentXlsName, ds.Tables[0]);
                        if (string.IsNullOrEmpty(cmsg))
                        {
                            (new ImportDate()).ImportPART_AREA_PAET_WAREHOUSE(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo, ref okCount, ref ngCount);
                            tabCondition.Attributes.CssStyle["display"] = "none";
                            tbResult.Attributes.CssStyle["display"] = "";
                            //grdNavigatorPart.CurrentPageIndex = 0;
                            //BindPARTCARGOSPACENCount();
                            BASE_PART_CARGOSPACE();
                            lblTitle.Text = Resources.Lang.FrmBaseImport_MSG65;//"料号和储位、区域关联信息";
                            lblTotalCount.Text = (okCount + ngCount).ToString();
                            lblOKCount.Text = okCount.ToString();
                            lblNGCount.Text = ngCount.ToString();
                            DisplayGV(CurrentXlsName);
                        }
                        else
                        {
                            this.Alert(cmsg);
                        }


                        //errorMsg.Append(new ImportDateRule().ImportPart_CargoSpace(ds, WmsWebUserInfo.GetCurrentUser().UserNo).Replace("\r\n", "<br/>"));

                        break;
                    case "BASE_AREA"://区域信息
                        cmsg = CheckTemplate(CurrentXlsName, ds.Tables[0]);
                        if (string.IsNullOrEmpty(cmsg))
                        {

                            //判断当前数据量是不是大于设置的最大上传量
                            var config = db.SYS_CONFIG.Where(x => x.code == "600001").FirstOrDefault();
                            int maxcount = 0;
                            bool isOK = false;
                            if (config != null)
                            {
                                isOK = int.TryParse(config.config_value, out maxcount);
                            }
                            if (isOK && ds.Tables[0].Rows.Count > maxcount)
                            {
                                cmsg = "超出系统设定最大上传量" + maxcount;
                                this.Alert(cmsg);
                                break;
                            }
                            (new ImportDate()).ImportBase_Area(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo, ref okCount, ref ngCount);
                            tabCondition.Attributes.CssStyle["display"] = "none";
                            tbResult.Attributes.CssStyle["display"] = "";
                            BindArea();
                            lblTitle.Text = Resources.Lang.FrmImportAsnDetail_BASE_AREA;// "区域信息";
                            lblTotalCount.Text = (okCount + ngCount).ToString();
                            lblOKCount.Text = okCount.ToString();
                            lblNGCount.Text = ngCount.ToString();
                            DisplayGV(CurrentXlsName);
                        }
                        else
                        {
                            this.Alert(cmsg);
                        }
                        ; break;

                    //errorMsg.Append(new ImportDateRule().ImportBase_Area(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo).Replace("\r\n", "<br/>"));
                    //break;
                    case "BASE_OPERATOR_AREA_NEW"://人员对应区域
                        errorMsg.Append(new ImportDate().ImportBASE_OPERATOR_AREA_NEW(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo).Replace("\r\n", "<br/>"));
                        break;
                    case "In_Out_AsnD"://出入库通知单明细
                        errorMsg.Append(new ImportDate().ImportAsnDetail(ds.Tables[0], Request.QueryString["ImportType"], Request.QueryString["AsnId"], WmsWebUserInfo.GetCurrentUser().UserNo).Replace("\r\n", "<br/>"));
                        break;
                    case "stock_current"://库存明细
                        errorMsg.Append(new ImportDate().ImportStock_Current(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo).Replace("\r\n", "<br/>"));
                        break;//stock_current
                    case "BASE_CLIENT"://客户

                        #region 客户
                        cmsg = CheckTemplate(CurrentXlsName, ds.Tables[0]);
                        if (string.IsNullOrEmpty(cmsg))
                        {
                            (new ImportDate()).ImportBASE_CLIENT(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo, ref okCount, ref ngCount);
                            tabCondition.Attributes.CssStyle["display"] = "none";
                            tbResult.Attributes.CssStyle["display"] = "";
                            //grdNavigatorPart.CurrentPageIndex = 0;
                            //BindCLIENTSpaceCount();
                            BindCLIENT();
                            lblTitle.Text = Resources.Lang.FrmBaseImport_MSG4;//"客户信息";
                            lblTotalCount.Text = (okCount + ngCount).ToString();
                            lblOKCount.Text = okCount.ToString();
                            lblNGCount.Text = ngCount.ToString();
                            DisplayGV(CurrentXlsName);
                        }
                        else
                        {
                            this.Alert(cmsg);
                        }
                        #endregion

                        // errorMsg.Append(new ImportDateRule().ImportBASE_CLIENT(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo).Replace("\r\n", "<br/>"));
                        ; break;
                    case "BASE_VENDOR"://供应商
                        #region 供应商
                        cmsg = CheckTemplate(CurrentXlsName, ds.Tables[0]);
                        if (string.IsNullOrEmpty(cmsg))
                        {
                            (new ImportDate()).ImportBase_VENDOR(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo, ref okCount, ref ngCount);
                            tabCondition.Attributes.CssStyle["display"] = "none";
                            tbResult.Attributes.CssStyle["display"] = "";
                            //grdNavigatorPart.CurrentPageIndex = 0;
                            //BindVENDORCount();
                            BindVENDORGoSpace();
                            lblTitle.Text = Resources.Lang.FrmBaseImport_MSG5;//"供应商信息";
                            lblTotalCount.Text = (okCount + ngCount).ToString();
                            lblOKCount.Text = okCount.ToString();
                            lblNGCount.Text = ngCount.ToString();
                            DisplayGV(CurrentXlsName);
                        }
                        else
                        {
                            this.Alert(cmsg);
                        }
                        #endregion

                        ; break;


                    case "BASE_PART"://物料
                        cmsg = CheckTemplate(CurrentXlsName, ds.Tables[0]);
                        if (string.IsNullOrEmpty(cmsg))
                        {
                            (new ImportDate()).ImportAllBase_PART(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo, ref okCount, ref ngCount);
                            tabCondition.Attributes.CssStyle["display"] = "none";
                            tbResult.Attributes.CssStyle["display"] = "";
                            //grdNavigatorPart.CurrentPageIndex = 0;
                            //BindPartCount();
                            BindPart();
                            lblTitle.Text = Resources.Lang.FrmBaseImport_MSG1;//"物料信息";
                            lblTotalCount.Text = (okCount + ngCount).ToString();
                            lblOKCount.Text = okCount.ToString();
                            lblNGCount.Text = ngCount.ToString();
                            DisplayGV(CurrentXlsName);
                        }
                        else
                        {
                            this.Alert(cmsg);
                        }
                        ; break;
                    case "BASE_CARGOSPACE"://储位
                        cmsg = CheckTemplate(CurrentXlsName, ds.Tables[0]);
                        if (string.IsNullOrEmpty(cmsg))
                        {
                            new ImportDate().ImportAllBASE_CARGOSPACE(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo, ref okCount, ref ngCount);
                            tabCondition.Attributes.CssStyle["display"] = "none";
                            tbResult.Attributes.CssStyle["display"] = "";
                            BindCarGoSpace();
                            //BindCarGoSpaceCount();
                            //BindCarGoSpace();
                            lblTitle.Text = Resources.Lang.FrmBaseImport_MSG2;//"储位信息";
                            lblTotalCount.Text = (okCount + ngCount).ToString();
                            lblOKCount.Text = okCount.ToString();
                            lblNGCount.Text = ngCount.ToString();
                            DisplayGV(CurrentXlsName);
                        }
                        else
                        {
                            this.Alert(cmsg);
                        }
                        break;
                    case "BASE_WAREHOUSE"://仓库
                        cmsg = CheckTemplate(CurrentXlsName, ds.Tables[0]);
                        if (string.IsNullOrEmpty(cmsg))
                        {
                            new ImportDate().ImportAllBASE_WAREHOUSE(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo, ref okCount, ref ngCount);
                            tabCondition.Attributes.CssStyle["display"] = "none";
                            tbResult.Attributes.CssStyle["display"] = "";
                            //grdWareHouseNavigator.CurrentPageIndex = 0;
                            //BindWareHouseCount();
                            BindWareHouse();
                            lblTitle.Text = Resources.Lang.FrmBaseImport_MSG66;//"仓库信息";
                            lblTotalCount.Text = (okCount + ngCount).ToString();
                            lblOKCount.Text = okCount.ToString();
                            lblNGCount.Text = ngCount.ToString();
                            DisplayGV(CurrentXlsName);
                        }
                        else
                        {
                            this.Alert(cmsg);
                        }
                        break;


                    case "BASE_DEPARTMENT"://部门信息
                        cmsg = CheckTemplate(CurrentXlsName, ds.Tables[0]);
                        if (string.IsNullOrEmpty(cmsg))
                        {
                            (new ImportDate()).ImportBASE_DEPARTMENT(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo, ref okCount, ref ngCount);
                            tabCondition.Attributes.CssStyle["display"] = "none";
                            tbResult.Attributes.CssStyle["display"] = "";
                            //grdDepartmentNavigator.CurrentPageIndex = 0;
                            //BindDepartmentCount();
                            BindDepartment();
                            lblTitle.Text = Resources.Lang.FrmBaseImport_MSG8;//"部门信息";
                            lblTotalCount.Text = (okCount + ngCount).ToString();
                            lblOKCount.Text = okCount.ToString();
                            lblNGCount.Text = ngCount.ToString();
                            DisplayGV(CurrentXlsName);
                        }
                        else
                        {
                            this.Alert(cmsg);
                        }
                        ; break;
                        //errorMsg.Append(new ImportDate().ImportBASE_DEPARTMENT(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo).Replace("\r\n", "<br/>"));
                        //; break;
                    case "BASE_LINE_LIST"://线体信息
                        cmsg = CheckTemplate(CurrentXlsName, ds.Tables[0]);
                        if (string.IsNullOrEmpty(cmsg))
                        {
                            (new ImportDate()).ImportBASE_LINE_LIST(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo, ref okCount, ref ngCount);
                            tabCondition.Attributes.CssStyle["display"] = "none";
                            tbResult.Attributes.CssStyle["display"] = "";
                            //grdLineNavigator.CurrentPageIndex = 0;
                            //BindLineCount();
                            BindLine();
                            lblTitle.Text = Resources.Lang.FrmBaseImport_MSG9;//"线体信息";
                            lblTotalCount.Text = (okCount + ngCount).ToString();
                            lblOKCount.Text = okCount.ToString();
                            lblNGCount.Text = ngCount.ToString();
                            DisplayGV(CurrentXlsName);
                        }
                        else
                        {
                            this.Alert(cmsg);
                        }
                        ; break;
                        //errorMsg.Append(new ImportDate().ImportBASE_LINE_LIST(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo).Replace("\r\n", "<br/>"));
                        //; break;



                    case "BASE_STOCK"://库存信息
                        cmsg = CheckTemplate(CurrentXlsName, ds.Tables[0]);
                        if (string.IsNullOrEmpty(cmsg))
                        {
                            (new ImportDate()).ImportBASE_STOCK(ds.Tables[0], WmsWebUserInfo.GetCurrentUser().UserNo, ref okCount, ref ngCount);
                            tabCondition.Attributes.CssStyle["display"] = "none";
                            tbResult.Attributes.CssStyle["display"] = "";
                          
                            BindStock();
                            lblTitle.Text = Resources.Lang.FrmBaseImport_MSG10;// "库存信息";
                            lblTotalCount.Text = (okCount + ngCount).ToString();
                            lblOKCount.Text = okCount.ToString();
                            lblNGCount.Text = ngCount.ToString();
                            DisplayGV(CurrentXlsName);
                        }
                        else
                        {
                            this.Alert(cmsg);
                        }
                        ; break;
                    default:
                        //errorMsg.Append(new ImportDateRule().ImportAsnDetail(ds.Tables[0], Request.QueryString["ImportType"], Request.QueryString["AsnId"], WmsWebUserInfo.GetCurrentUser().UserNo).Replace("\r\n", "<br/>"));
                        break;
                }

                //this.lblMsg.Text = errorMsg.ToString();
                #region 查询临时的存的数据



                #endregion

            }
        }
        catch (Exception ex)
        {
            base.Alert(Resources.Lang.FrmImportStock_CheckedBill_Msg7 + "--" + ex.Message);//上传文件失败

        }

    }

    /// <summary>
    /// 检查模板格式
    /// </summary>
    /// <param name="tempName">模板名称</param>
    /// <param name="dt">数据集</param>
    /// <returns></returns>
    public string CheckTemplate(string tempName, DataTable dt)
    {
        StringBuilder str = new StringBuilder("");
        if (dt != null && dt.Rows.Count > 0)
        {
            switch (tempName)
            {
                case "BASE_PART"://物料
                    if (!dt.Columns.Contains("料号")
                        && !dt.Columns.Contains("品名")
                        && !dt.Columns.Contains("类型")
                        && !dt.Columns.Contains("类别")
                        && !dt.Columns.Contains("ERP编码")

                        && !dt.Columns.Contains("毛重")
                        && !dt.Columns.Contains("净重")
                        && !dt.Columns.Contains("重量单位")
                        && !dt.Columns.Contains("单位")

                        && !dt.Columns.Contains("参考单位")
                        && !dt.Columns.Contains("长")
                        && !dt.Columns.Contains("宽")
                        && !dt.Columns.Contains("高")

                        && !dt.Columns.Contains("材积")
                        && !dt.Columns.Contains("材积单位")
                        && !dt.Columns.Contains("终止日期")
                        && !dt.Columns.Contains("状态")

                        && !dt.Columns.Contains("是否保税")
                        && !dt.Columns.Contains("是否免检")
                        && !dt.Columns.Contains("是否预警")
                        && !dt.Columns.Contains("默认仓库")

                        && !dt.Columns.Contains("默认储位")
                        && !dt.Columns.Contains("默认供应商")
                        && !dt.Columns.Contains("上架规则")
                        && !dt.Columns.Contains("下架规则")

                        && !dt.Columns.Contains("条码规则")
                        && !dt.Columns.Contains("版本")
                        && !dt.Columns.Contains("企业编号")
                        && !dt.Columns.Contains("据点编号")

                        && !dt.Columns.Contains("应用组织")
                        && !dt.Columns.Contains("用途")
                        && !dt.Columns.Contains("备注")
                        && !dt.Columns.Contains("助记码")
                        && !dt.Columns.Contains("规格")
                        && !dt.Columns.Contains("是否序列号管控")
                        )
                        str.Append(Resources.Lang.FrmBaseImport_MSG67+"！");//物料导入的模板不正确
                    break;
                case "BASE_CARGOSPACE"://储位
                    if (!dt.Columns.Contains("储位编码")
                        && !dt.Columns.Contains("储位名称")
                        && !dt.Columns.Contains("最大量")
                        && !dt.Columns.Contains("助记码")
                        && !dt.Columns.Contains("ERP编码")

                        && !dt.Columns.Contains("种类")
                        && !dt.Columns.Contains("区域")
                        && !dt.Columns.Contains("所属仓库ID")
                        && !dt.Columns.Contains("线别")
                        && !dt.Columns.Contains("优先级")
                        && !dt.Columns.Contains("长")

                        && !dt.Columns.Contains("宽")
                        && !dt.Columns.Contains("高")
                        && !dt.Columns.Contains("体积")
                        && !dt.Columns.Contains("用途")
                        && !dt.Columns.Contains("是否允许混放")

                        && !dt.Columns.Contains("X")
                        && !dt.Columns.Contains("Y")
                        && !dt.Columns.Contains("Z")
                        && !dt.Columns.Contains("重量")
                        && !dt.Columns.Contains("重量单位")

                        && !dt.Columns.Contains("材积")
                        && !dt.Columns.Contains("材积单位")
                        && !dt.Columns.Contains("终止日期")
                        && !dt.Columns.Contains("状态")
                        && !dt.Columns.Contains("是否允许调拨")

                        && !dt.Columns.Contains("企业编号")
                        && !dt.Columns.Contains("据点编号")
                        && !dt.Columns.Contains("应用组织")
                        && !dt.Columns.Contains("备注")
                        )
                        str.Append(Resources.Lang.FrmBaseImport_MSG68 + "！");//储位导入的模板不正确
                    break;
                case "BASE_WAREHOUSE"://仓库
                    if (!dt.Columns.Contains("仓库编码")
                        && !dt.Columns.Contains("仓库名称")
                        && !dt.Columns.Contains("仓库类型")
                        && !dt.Columns.Contains("供应商编码")
                        && !dt.Columns.Contains("供应商名称")

                        && !dt.Columns.Contains("电话")
                        && !dt.Columns.Contains("状态")
                        && !dt.Columns.Contains("是否保税仓")
                        && !dt.Columns.Contains("是否良品仓")
                        && !dt.Columns.Contains("企业编号")

                        && !dt.Columns.Contains("据点编号")
                        && !dt.Columns.Contains("应用组织")
                        && !dt.Columns.Contains("备注")
                        )
                        str.Append(Resources.Lang.FrmBaseImport_MSG69 + "！");//仓库导入的模板不正确
                    break;
                case "Base_VENDOR"://供应商
                    if (!dt.Columns.Contains("供应商编码")
                        && !dt.Columns.Contains("供应商名称")
                        && !dt.Columns.Contains("状态")
                        && !dt.Columns.Contains("联系人")
                        && !dt.Columns.Contains("联系电话")
                        && !dt.Columns.Contains("联系地址")
                        && !dt.Columns.Contains("供应商类型")
                        && !dt.Columns.Contains("助记码")
                        && !dt.Columns.Contains("级别")
                        && !dt.Columns.Contains("备注")
                        && !dt.Columns.Contains("ERP编码")
                        && !dt.Columns.Contains("企业编号")
                        && !dt.Columns.Contains("据点编号")
                        && !dt.Columns.Contains("应用组织"))
                        str.Append(Resources.Lang.FrmBaseImport_MSG70 + "！");//供应商导入的模板不正确
                    break;
                case "BASE_CLIENT"://客户
                    if (!dt.Columns.Contains("客户编码")
                        && !dt.Columns.Contains("客户名称")
                        && !dt.Columns.Contains("状态")
                        && !dt.Columns.Contains("联系人")
                        && !dt.Columns.Contains("联系电话")
                        && !dt.Columns.Contains("联系地址")
                        && !dt.Columns.Contains("客户类型")
                        && !dt.Columns.Contains("助记码")
                        && !dt.Columns.Contains("级别")
                        && !dt.Columns.Contains("备注")
                        && !dt.Columns.Contains("ERP编码")
                        && !dt.Columns.Contains("企业编号")
                        && !dt.Columns.Contains("据点编号")
                        && !dt.Columns.Contains("应用组织"))
                        str.Append(Resources.Lang.FrmBaseImport_MSG71 + "！");//客户导入的模板不正确
                    break;
                case "TEMP_IMPORT_SPACE_PART_AREA"://物料，储位区域关联
                    if (!dt.Columns.Contains("区域名称")
                        && !dt.Columns.Contains("仓库编码")
                        && !dt.Columns.Contains("储位编码")
                        && !dt.Columns.Contains("料号"))

                        str.Append(Resources.Lang.FrmBaseImport_MSG72 + "！");//物料、储位区关联域导入的模板不正确
                    break;
                case "BASE_AREA"://区域
                    if (!dt.Columns.Contains("区域名称")
                        && !dt.Columns.Contains("备料储位编号")
                        && !dt.Columns.Contains("备料储位名称")
                        && !dt.Columns.Contains("是否允许超发")
                        )
                        str.Append(Resources.Lang.FrmBaseImport_MSG73 + "！");//区域导入的模板不正确
                    break;
                case "BASE_DEPARTMENT"://部门
                    if (!dt.Columns.Contains("部门编码")
                        && !dt.Columns.Contains("部门名称")
                        )
                        str.Append(Resources.Lang.FrmBaseImport_MSG74 + "！");//部门导入的模板不正确
                    break;
                case "BASE_LINE_LIS"://线体
                    if (!dt.Columns.Contains("线体")
                        )
                        str.Append(Resources.Lang.FrmBaseImport_MSG75 + "！");//线体导入的模板不正确
                    break;
                case "BASE_STOCK":
                    if (!dt.Columns.Contains("仓库编码")
                      && !dt.Columns.Contains("储位编码")
                      && !dt.Columns.Contains("物料编码")
                      && !dt.Columns.Contains("数量")
                      )
                        str.Append(Resources.Lang.FrmBaseImport_MSG76 + "！");//库存导入的模板不正确
                    break;
                default:
                    break;
            }
        }
        return str.ToString();
    }

    #region 物料信息绑定
    protected void Aspgv_Part_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = Aspgv_Part.CurrentPageIndex;//索引同步
        BindPart();
    }
   

    public void BindPart()
    {
        IGenericRepository<v_base_part_temp> entity = new GenericRepository<v_base_part_temp>(context);
        var caseList = from p in entity.Get()
                       orderby p.ERP编码
                       where 1==1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            Aspgv_Part.RecordCount = caseList.Count();
            Aspgv_Part.PageSize = this.PageSize;
        }
        gv_Part.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        gv_Part.DataBind();
        
    }
    #endregion

    #region 储位信息绑定
    protected void AspCarGoSpace_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = Aspgv_WareHouse.CurrentPageIndex;//索引同步
        BindCarGoSpace();
    }
   

    public void BindCarGoSpace()
    {
        IGenericRepository<v_base_cargospace_temp> entity = new GenericRepository<v_base_cargospace_temp>(context);
        var caseList = from p in entity.Get()
                       orderby p.终止日期 
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            AspCarGoSpace.RecordCount = caseList.Count();
            AspCarGoSpace.PageSize = this.PageSize;
        }
        gv_CarGoSpace.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        gv_CarGoSpace.DataBind();
    }

    #endregion

    #region 仓库信息绑定
    


    protected void Aspgv_WareHouse_Part_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = Aspgv_WareHouse.CurrentPageIndex;//索引同步
        BindWareHouse();
    }
    public void BindWareHouse()
    {
        IGenericRepository<v_base_warehouse_temp> entity = new GenericRepository<v_base_warehouse_temp>(context);
        var caseList = from p in entity.Get()
                       orderby p.仓库编码
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            Aspgv_WareHouse.RecordCount = caseList.Count();
            Aspgv_WareHouse.PageSize = this.PageSize;
        }
        gv_WareHouse.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        gv_WareHouse.DataBind();
    //    ImportQuery query = new ImportQuery();
    //    DataTable dt = query.GetWareHouseTempList(false, this.grdWareHouseNavigator.CurrentPageIndex, this.gv_WareHouse.PageSize);
    //    gv_WareHouse.DataSource = dt;
    //    gv_WareHouse.DataBind();
    }

    #endregion



  
    #region 区域信息绑定
    protected void Aspgv_Area_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = Aspgv_Area.CurrentPageIndex;//索引同步
        BindArea();
    }

    public void BindArea()
    {
        IGenericRepository<v_temp_base_area> entity = new GenericRepository<v_temp_base_area>(context);
        var caseList = from p in entity.Get()
                       orderby p.备料储位编号
                       where 1 == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            Aspgv_Area.RecordCount = caseList.Count();
            Aspgv_Area.PageSize = this.PageSize;
        }
        gv_Area.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        gv_Area.DataBind();
    }


    #endregion


    #region 部门信息绑定
    protected void Aspgv_Department_PageChanged(object sender, EventArgs e)    
    {
        this.CurrendIndex = Aspgv_Department.CurrentPageIndex;//索引同步
        BindDepartment();
    }
    public void BindDepartment()
    {
        IGenericRepository<TEMP_BASE_DEPARTMENT> entity = new GenericRepository<TEMP_BASE_DEPARTMENT>(context);
        var caseList = from p in entity.Get()
                       orderby p.DEPARTMENTNO descending
                       where p.error_flag == 1
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            Aspgv_Department.RecordCount = caseList.Count();
            Aspgv_Department.PageSize = this.PageSize;
        }
        gv_Department.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        gv_Department.DataBind();
  
    }

    #endregion



    #region 线体绑定
    protected void Aspgv_Line_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = Aspgv_Line.CurrentPageIndex;//索引同步
        BindLine();
    }
    public void BindLine()
    {
        IGenericRepository<TEMP_BASE_LINE_LIST> entity = new GenericRepository<TEMP_BASE_LINE_LIST>(context);
        var caseList = from p in entity.Get()
                       orderby p.LINEID
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            Aspgv_Line.RecordCount = caseList.Count();
            Aspgv_Line.PageSize = this.PageSize;
        }
        gv_Line.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        gv_Line.DataBind();

    }

    #endregion



    #region 客户信息绑定
    protected void Aspgv_CLIENT_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = Aspgv_CLIENT.CurrentPageIndex;//索引同步
        BindCLIENT();
    }

    public void BindCLIENT()
    {
        IGenericRepository<v_temp_base_client> entity = new GenericRepository<v_temp_base_client>(context);
        var caseList = from p in entity.Get()
                       orderby p.ERP编码
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            Aspgv_CLIENT.RecordCount = caseList.Count();
            Aspgv_CLIENT.PageSize = this.PageSize;
        }
        gv_CLIENT.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        gv_CLIENT.DataBind();

     
    }

    #endregion

    #region 供应商信息绑定


    protected void Aspgv_VENDOR_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = Aspgv_VENDOR.CurrentPageIndex;//索引同步
        BindVENDORGoSpace();
    }

  

    public void BindVENDORGoSpace()
    {
        IGenericRepository<v_temp_base_vendor> entity = new GenericRepository<v_temp_base_vendor>(context);
        var caseList = from p in entity.Get()
                       orderby p.ERP编码
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            Aspgv_VENDOR.RecordCount = caseList.Count();
            Aspgv_VENDOR.PageSize = this.PageSize;
        }
        gv_VENDOR.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        gv_VENDOR.DataBind();
        //ImportQuery query = new ImportQuery();
        //DataTable dt = query.GetVENDORempList(false, this.grdCarGoSpaceNavigator.CurrentPageIndex, this.gv_CarGoSpace.PageSize);
        //gv_VENDOR.DataSource = dt;
        //gv_VENDOR.DataBind();
    }

    #endregion


    #region 物料储位信息关联



    protected void Aspgv_PARTCARGOSPACEN_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = Aspgv_PARTCARGOSPACEN.CurrentPageIndex;//索引同步
        BASE_PART_CARGOSPACE();
    }

   
    public void BASE_PART_CARGOSPACE()
    {

        IGenericRepository<v_temp_area_paet_warehouse> entity = new GenericRepository<v_temp_area_paet_warehouse>(context);
        var caseList = from p in entity.Get()
                       orderby p.仓库编码
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            Aspgv_PARTCARGOSPACEN.RecordCount = caseList.Count();
            Aspgv_PARTCARGOSPACEN.PageSize = this.PageSize;
        }
        gv_PARTCARGOSPACEN.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        gv_PARTCARGOSPACEN.DataBind();
      
    }

    #endregion

    #region 隐藏显示gv
    public void DisplayGV(string gvName)
    {
        switch (gvName)
        {
            case "BASE_PART"://物料
                divPart.Visible = true;
                divCarGoSpace.Visible = false;
                divWareHouse.Visible = false;

                divVENDOR.Visible = false;
                divCLIENT.Visible = false;
                divBASE_PART_CARGOSPACE.Visible = false;

                divArea.Visible = false;
                divDepartment.Visible = false;
                divLine.Visible = false;
                divStock.Visible = false;
                //...后续添加的gv要添加到这里 todo

                break;
            case "BASE_CARGOSPACE"://储位
                divCarGoSpace.Visible = true;
                divPart.Visible = false;
                divWareHouse.Visible = false;

                divVENDOR.Visible = false;
                divCLIENT.Visible = false;
                divBASE_PART_CARGOSPACE.Visible = false;
                divArea.Visible = false;
                divDepartment.Visible = false;
                divLine.Visible = false;
                divStock.Visible = false;
                //...后续添加的gv要添加到这里 todo

                break;
            case "BASE_WAREHOUSE"://仓库
                divWareHouse.Visible = true;
                divPart.Visible = false;
                divCarGoSpace.Visible = false;

                divVENDOR.Visible = false;
                divCLIENT.Visible = false;
                divBASE_PART_CARGOSPACE.Visible = false;
                divArea.Visible = false;
              divDepartment.Visible = false;
                divLine.Visible = false;
                divStock.Visible = false;
                //...后续添加的gv要添加到这里 todo

                break;

            case "BASE_CLIENT"://客户
                divWareHouse.Visible = false;
                divPart.Visible = false;
                divCarGoSpace.Visible = false;

                divVENDOR.Visible = false;
                divCLIENT.Visible = true;
                divBASE_PART_CARGOSPACE.Visible = false;
                divArea.Visible = false;
               divDepartment.Visible = false;
                divLine.Visible = false;
                divStock.Visible = false;
                //...后续添加的gv要添加到这里 todo
                break;

            case "BASE_VENDOR"://供应商
                divWareHouse.Visible = false;
                divPart.Visible = false;
                divCarGoSpace.Visible = false;

                divVENDOR.Visible = true;
                divCLIENT.Visible = false;
                divBASE_PART_CARGOSPACE.Visible = false;
                divArea.Visible = false;
               divDepartment.Visible = false;
                divLine.Visible = false;
                divStock.Visible = false;
                //...后续添加的gv要添加到这里 todo
                break;
            case "BASE_AREA"://区域
                divWareHouse.Visible = false;
                divPart.Visible = false;
                divCarGoSpace.Visible = false;

                divVENDOR.Visible = false;
                divCLIENT.Visible = false;
                divBASE_PART_CARGOSPACE.Visible = false;
                divArea.Visible = true;
             divDepartment.Visible = false;
                divLine.Visible = false;
                divStock.Visible = false;
                break;
            case "BASE_DEPARTMENT"://部门
                divWareHouse.Visible = false;
                divPart.Visible = false;
                divCarGoSpace.Visible = false;

                divVENDOR.Visible = false;
                divCLIENT.Visible = false;
                divBASE_PART_CARGOSPACE.Visible = false;
                divArea.Visible = false;
                divDepartment.Visible = true;
                divLine.Visible = false;
                divStock.Visible = false;
                break;
            case "BASE_LINE_LIST"://线体
                divWareHouse.Visible = false;
                divPart.Visible = false;
                divCarGoSpace.Visible = false;

                divVENDOR.Visible = false;
                divCLIENT.Visible = false;
                divBASE_PART_CARGOSPACE.Visible = false;
                divArea.Visible = false;
               divDepartment.Visible = false;
               divLine.Visible = true;
                divStock.Visible = false;
                break;
            case "BASE_STOCK"://库存
                divWareHouse.Visible = false;
                divPart.Visible = false;
                divCarGoSpace.Visible = false;

                divVENDOR.Visible = false;
                divCLIENT.Visible = false;
                divBASE_PART_CARGOSPACE.Visible = false;
                divArea.Visible = false;
              divDepartment.Visible = false;
                divLine.Visible = false;
                divStock.Visible = true;
                break;
            case "TEMP_IMPORT_SPACE_PART_AREA"://库存
                divWareHouse.Visible = false;
                divPart.Visible = false;
                divCarGoSpace.Visible = false;

                divVENDOR.Visible = false;
                divCLIENT.Visible = false;
                divBASE_PART_CARGOSPACE.Visible = true;
                divArea.Visible = false;
              
                divStock.Visible = false;
                break;



            default: break;
        }
    }
    #endregion

    //返回
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        tabCondition.Attributes.CssStyle["display"] = "";
        tbResult.Attributes.CssStyle["display"] = "none";
    }

    //导出excel
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ImportDate exp = new ImportDate();
        DataTable dt = new DataTable();
        string fileName = "";
        string filePath = Server.MapPath("~/TempFile/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        switch (CurrentXlsName)
        {
            case "BASE_PART"://物料
                fileName = System.Web.HttpUtility.UrlEncode("物料信息.xls", System.Text.Encoding.UTF8); ;
                dt = new ImportQry().GetAllPartTempList();
                break;
            case "BASE_CARGOSPACE"://储位
                fileName = System.Web.HttpUtility.UrlEncode("储位信息.xls", System.Text.Encoding.UTF8); ;
                dt = new ImportQry().GetAllCarGoSpaceTempList();
                break;
            case "BASE_WAREHOUSE"://仓库
                fileName = System.Web.HttpUtility.UrlEncode("仓库资料.xls", System.Text.Encoding.UTF8); ;
                dt = new ImportQry().GetAllWareHouseTempList();
                break;
            case "BASE_AREA"://区域
                fileName = System.Web.HttpUtility.UrlEncode("区域资料.xls", System.Text.Encoding.UTF8); ;
                dt = new ImportQry().GetAllAreaTempList();
                break;
           
           
            case "BASE_STOCK"://库存
                fileName = System.Web.HttpUtility.UrlEncode("库存信息.xls", System.Text.Encoding.UTF8); ;
                dt = new ImportQry().GetAllStockTempList();
                break;

            case "BASE_VENDOR"://供应商信息
                fileName = System.Web.HttpUtility.UrlEncode("供应商信息.xls", System.Text.Encoding.UTF8); ;
                dt = new ImportQry().GetVENDORempList();
                break;

            case "BASE_CLIENT"://客户信息
                fileName = System.Web.HttpUtility.UrlEncode("客户信息.xls", System.Text.Encoding.UTF8); ;
                dt = new ImportQry().GetCLIENTTempList();
                break;

            case "TEMP_IMPORT_SPACE_PART_AREA":// 料号和储位、区域关联
                fileName = System.Web.HttpUtility.UrlEncode("料号和储位、区域关联.xls", System.Text.Encoding.UTF8); ;
                dt = new ImportQry().GetAllStockTempList();
                break;

            case "BASE_DEPARTMENT"://部门信息
                fileName = System.Web.HttpUtility.UrlEncode("部门信息.xls", System.Text.Encoding.UTF8); ;
                dt = new ImportQry().GetCLIENTTempList();
                break;


            case "BASE_LINE_LIST"://线体信息
                fileName = System.Web.HttpUtility.UrlEncode("线体信息.xls", System.Text.Encoding.UTF8); ;
                dt = new ImportQry().GetCLIENTTempList();
                break;



            default:

                break;
        }

        //生成excel
        exp.DataTableExport(dt, filePath);
        //导出excel
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

    #region 区域信息绑定
    public void BindAreaCount()
    {
        //ImportQuery query = new ImportQuery();
        //DataTable dtRowCount = query.GetAreaTempList(true, 0, 0);
        //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
        //{
        //    this.grdAreaNavigator.RowCount = dtRowCount.Rows[0][0].ToInt32();
        //}
        //else
        //{
        //    this.grdAreaNavigator.RowCount = 0;
        //}
    }

 

  

    protected void gv_Area_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (grdAreaNavigator.IsDbPager)
        //{
        //    grdAreaNavigator.CurrentPageIndex = e.NewPageIndex;
        //}
        //else
        //{
        //    this.gv_Area.PageIndex = e.NewPageIndex;
        //}
    }
   
    #endregion

    //#region 部门信息绑定
    //public void BindDepartmentCount()
    //{
    //    //ImportQuery query = new ImportQuery();
    //    //DataTable dtRowCount = query.GetDepartmentTempList(true, 0, 0);
    //    //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
    //    //{
    //    //    this.grdDepartmentNavigator.RowCount = dtRowCount.Rows[0][0].ToInt32();
    //    //}
    //    //else
    //    //{
    //    //    this.grdDepartmentNavigator.RowCount = 0;
    //    //}
    //}

    //public void BindDepartment()
    //{
    //    //ImportQuery query = new ImportQuery();
    //    //DataTable dt = query.GetDepartmentTempList(false, this.grdDepartmentNavigator.CurrentPageIndex, this.gv_Department.PageSize);
    //    //gv_Department.DataSource = dt;
    //    //gv_Department.DataBind();
    //}
    //protected void gv_Department_PageIndexChanged(object sender, EventArgs e)
    //{
    //    //BindDepartment();
    //}

    //protected void gv_Department_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    //if (grdDepartmentNavigator.IsDbPager)
    //    //{
    //    //    grdDepartmentNavigator.CurrentPageIndex = e.NewPageIndex;
    //    //}
    //    //else
    //    //{
    //    //    this.gv_Department.PageIndex = e.NewPageIndex;
    //    //}
    //}
    //#endregion

    //#region 线体信息绑定
    //public void BindLineCount()
    //{
    //    //ImportQuery query = new ImportQuery();
    //    //DataTable dtRowCount = query.GetLineTempList(true, 0, 0);
    //    //if (dtRowCount != null && dtRowCount.Rows.Count >= 1)
    //    //{
    //    //    this.grdLineNavigator.RowCount = dtRowCount.Rows[0][0].ToInt32();
    //    //}
    //    //else
    //    //{
    //    //    this.grdLineNavigator.RowCount = 0;
    //    //}
    //}

    //public void BindLine()
    //{
    //    //ImportQuery query = new ImportQuery();
    //    //DataTable dt = query.GetLineTempList(false, this.grdLineNavigator.CurrentPageIndex, this.gv_Line.PageSize);
    //    //gv_Line.DataSource = dt;
    //    //gv_Line.DataBind();
    //}
    //protected void gv_Line_PageIndexChanged(object sender, EventArgs e)
    //{
    //    //BindLine();
    //}

    //protected void gv_Line_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    //if (grdLineNavigator.IsDbPager)
    //    //{
    //    //    grdLineNavigator.CurrentPageIndex = e.NewPageIndex;
    //    //}
    //    //else
    //    //{
    //    //    this.gv_Line.PageIndex = e.NewPageIndex;
    //    //}
    //}
    //#endregion

    #region 库存信息绑定
    protected void Aspgv_Stock_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = Aspgv_WareHouse.CurrentPageIndex;//索引同步
        BindStock();
    }


    public void BindStock()
    {
        try
        {
            IGenericRepository<v_temp_base_stock_current> entity = new GenericRepository<v_temp_base_stock_current>(context);
            var caseList = from p in entity.Get()
                           orderby p.储位编码
                           //where 1 == 1
                           select p;

            if (caseList != null && caseList.Count() > 0)
            {
                Aspgv_Stock.RecordCount = caseList.Count();
                Aspgv_Stock.PageSize = this.PageSize;
            }
            gv_Stock.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
            gv_Stock.DataBind();
        }
        catch (Exception ex)
        {
            
            throw;
        }
       
    }
  
    #endregion
}