using System;
using System.Data;
using System.Data.OleDb;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.Business;
using System.Linq.Dynamic;
using System.Text;

/// <summary>
/// 描述: 导入物理盘点单初盘或复盘明细
/// 作者: --CQ
/// 创建于:2013-6-27 13:39:51
/// </summary>
public partial class Import_FrmImportStock_CheckedBill : WMSBasePage
{
    #region SQL
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            lblCaption.Text = Resources.Lang.FrmImportStock_CheckedBill_Msg1;// "正在导入物理盘点实盘单明细!";
        }

    }

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/LG/page.css";
       // this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('OUTASN_D');return false;";
        
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
        ds.Tables.Add(ExeclTable(strConn, "select * from [Sheet1$]", type));

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

    /// 導入按鈕
    /// <summary>
    /// 導入按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUp_Click(object sender, EventArgs e)
    {
        try
        {
            //AppLog.Write("---------------文件上传结束开始导入---------------");
            if (fuFile.PostedFile.FileName == "")
            {
                base.Alert(Resources.Lang.FrmImportStock_CheckedBill_Msg2);//"请选择要上载的文件"
                return;
            }
            var extension = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
            if (extension != null && (extension.ToLower() != ".xls" && extension.ToLower() != ".xlsx"))
            {
                base.Alert(Resources.Lang.FrmImportStock_CheckedBill_Msg3);//上载的文件类型不正确，必须为*.xls或者*.xlsx
                return;
            }
            this.lblMsg.Text = Resources.Lang.FrmImportStock_CheckedBill_Msg4 + "...";//开始上传文件
            DateTime dt = DateTime.Now;
            var s = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
            if (s != null)
            {
                string fileName = dt.Year + dt.Month.ToString() + dt.Day + dt.Hour + dt.Minute + dt.Second + s.ToLower();
                string requestUrl = Request.Path.ToLower();
                int index = requestUrl.IndexOf("apps/", 0);
                string savePath = Server.MapPath(requestUrl.Substring(0, index)) + @"TempFile\" + fileName;
                fuFile.PostedFile.SaveAs(@savePath);

                DataSet ds = ExcelToDS(savePath, ExcelToDSType.Check);
                this.lblMsg.Text = Resources.Lang.FrmImportStock_CheckedBill_Msg5 + "！<br/>";//文件上传成功
                this.lblMsg.Text += Resources.Lang.FrmImportStock_CheckedBill_Msg6 + "...<br/>";//正在验证上传数据
                StringBuilder errorMsg = new StringBuilder();
                //导入方法
                //errorMsg.Append(new ImportDateRule().ImportStock_CheckBill(ds.Tables[0], ddlTeyp.SelectedValue,WmsWebUserInfo.GetCurrentUser().UserNo).Replace("\r\n", "<br/>"));
                     
                
                this.lblMsg.Text = errorMsg.ToString();
              

            }
        }
        catch (Exception ex)
        {
            base.Alert(Resources.Lang.FrmImportStock_CheckedBill_Msg7 + "--" + ex.Message);//上传文件失败

        }

    }
    #endregion
}