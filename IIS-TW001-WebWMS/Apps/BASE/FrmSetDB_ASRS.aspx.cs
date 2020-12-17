using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;

/// <summary>
/// 描述: 物料详情-->FrmBASE_PARTEdit 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-09-29 15:02:32
/// </summary>
public partial class FrmSetDB_ASRS : WMSBasePage
{
    /// <summary>
    /// 0:新增   1：编辑
    /// </summary>
    //public string Flag
    //{
    //    get 
    //    {
    //        if (!string.IsNullOrEmpty(Request.QueryString["Flag"]))
    //        {
    //            return Request.QueryString["Flag"];
    //        }
    //        return "";
    //    }
    //    set { Flag = value; }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if(this.IsPostBack == false)
        {
            this.InitPage();
            IGenericRepository<BASE_ASRS_DB> com = new GenericRepository<BASE_ASRS_DB>(db);
            var caseList = from p in com.Get()
                     select p.id;
            string status = caseList.ToList()[0];
            if (status == "")
            {
                //Flag = "0";
                //this.Operation=SYSOperation.New;
            }
            else
            {
                //Flag = "1";
               // this.Operation = SYSOperation.Modify;
                txtID.Text = status;
            }
            ShowData();
        }
        //this.FunctionNo = ""; ///TODO:请设置正确的功能点编号，多个功能点请以逗号隔开，例如100,101 
        //this.HasRight();
        btnTest.Attributes.Add("onclick", this.GetPostBackEventReference(btnTest) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
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
        //this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + Session["CSS_Name"].ToString() + "/Page.css";
       // this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BASE_PART');return false;";
       
    }

    #endregion
   
    
    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {

        if (this.txtIP.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmSetDB_ASRS_Msg01);//DB IP项不允许空！
            this.SetFocus(txtIP);
            return false;
        }
        //
        if (this.txtdatabase.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmSetDB_ASRS_Msg02); //DataBase 项不允许空！
            this.SetFocus(txtdatabase);
            return false;
        }
        //
        if (this.txtAcCount.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmSetDB_ASRS_Msg03);//AcCount 项不允许空！
            this.SetFocus(txtAcCount);
            return false;
        }
        //
        if (this.txtdatabase.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmSetDB_ASRS_Msg04);//PassWord 项不允许空！
            this.SetFocus(txtdatabase);
            return false;
        }
        //
       
        return true;

    }

    public int ljbz = 0;
    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTest_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckData())
            {
                string errmsg = string.Empty;
                if (DataBaseConn(txtIP.Text.Trim(), txtdatabase.Text.Trim(), txtAcCount.Text.Trim(),
                                 txtPassword.Text.Trim(), out errmsg))
                {
                    lblMessage.Text = Resources.Lang.FrmSetDB_ASRS_Msg05; //测试连接成功
                    this.Alert(Resources.Lang.FrmSetDB_ASRS_Msg05); //测试连接成功
                }
                else
                {
                    lblMessage.Text = errmsg;
                   this. Alert(errmsg);
                }
            }
           
        }
        catch (Exception err)
        {
            lblMessage.Text = err.Message;
            this.Alert(err.Message);
        }
    }

    /// <summary>
    /// 控件数据绑定
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<BASE_ASRS_DB> com = new GenericRepository<BASE_ASRS_DB>(db);
        var baseList = from p in com.Get()
                       where p.id == txtID.Text
                       select p;
        BASE_ASRS_DB bs = baseList.ToList().FirstOrDefault<BASE_ASRS_DB>();
        this.txtIP.Text = bs.db_ip;
        this.txtdatabase.Text = bs.db_datebase;
        this.txtAcCount.Text = bs.account;
        this.txtPassword.Text = bs.password;
        this.txtMemo.Text = bs.memo;
    }

    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>  
    public BASE_ASRS_DB SendData()
    {
        BASE_ASRS_DB entity = new BASE_ASRS_DB();
        //

        this.txtIP.Text = this.txtIP.Text.Trim();
        if (this.txtIP.Text.Length > 0)
        {
            entity.db_ip = txtIP.Text;
        }
        //
        this.txtdatabase.Text = this.txtdatabase.Text.Trim();
        if (this.txtdatabase.Text.Length > 0)
        {
            entity.db_datebase = txtdatabase.Text;
        }
        this.txtAcCount.Text = this.txtAcCount.Text.Trim();
        if (this.txtAcCount.Text.Length > 0)
        {
            entity.account = txtAcCount.Text;
        }
        this.txtPassword.Text = this.txtPassword.Text.Trim();
        if (this.txtPassword.Text.Length > 0)
        {
            entity.password = txtPassword.Text;
        }
        this.txtMemo.Text = this.txtMemo.Text.Trim();
        if (this.txtMemo.Text.Length > 0)
        {
            entity.memo = txtMemo.Text;
        }
        if (string.IsNullOrEmpty(this.txtID.Text.Trim()))
        {
            entity.createtime = DateTime.Now;
            //entity.createuser = Session["UserNo"].ToString();
            entity.lastupdatetime = DateTime.Now;
            //entity.lastupdateuser = Session["UserNo"].ToString();
            entity.id = Guid.NewGuid().ToString();
        }
        else
        {
            entity.id = txtID.Text.Trim();
            entity.lastupdatetime = DateTime.Now;
            //entity.lastupdateuser = Session["UserNo"].ToString();
        }
        return entity;

    }
    /// 完成按钮
    /// <summary>
    /// 完成按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DBContext context = new DBContext();
        IGenericRepository<BASE_ASRS_DB> asrs = new GenericRepository<BASE_ASRS_DB>(context);
        try
        {
            if (!CheckData())
            {
                return;
            }
            string strKeyID = string.Empty;
            string errmsg = string.Empty;
            if (DataBaseConn(txtIP.Text.Trim(), txtdatabase.Text.Trim(), txtAcCount.Text.Trim(),
                                txtPassword.Text.Trim(), out errmsg))
            {

                
                var entity = SendData();
                if (string.IsNullOrEmpty(this.txtID.Text.Trim()))
                {
                    asrs.Insert(entity);

                    //this.AlertAndBack("FrmSetDB_ASRS.aspx?Flag=1",  Resources.Lang.Common_SuccessSave); //保存成功
                }
                else
                {
                    asrs.Update(entity);
                    //this.AlertAndBack("FrmSetDB_ASRS.aspx?Flag=1",  Resources.Lang.Common_SuccessSave); //保存成功
                }
                asrs.Save();
                ShowData();
                this.Alert(Resources.Lang.Common_SuccessSave); //保存成功
                //OnePigeonHelp.MessBoxShow(this, "保存成功", "提示", OnePigeonHelp.BoxShowType.success);
            }
            else
            {
                lblMessage.Text = errmsg + Resources.Lang.FrmSetDB_ASRS_Msg06; //不能保存
                //this.Alert(errmsg+"不能保存");
            }
        }
        catch (Exception err)
        {
          lblMessage.Text = err.Message;
          this.Alert(err.Message);
        }
    }

    //测试连接是否正确
    public bool DataBaseConn(string strip, string strInitlog, string struser, string strPassword,out string msg)
    {
        bool boolTF = false;
        msg = "";
        try
        {
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3};", strip, strInitlog, struser, strPassword);
            Conn.Open();
            if (Conn.State == ConnectionState.Open)
            {
                boolTF = true;
            }
            else
            {
                msg = Resources.Lang.FrmSetDB_ASRS_Msg07; //打开连接失败！
            }
        }
        catch (Exception err)
        {
            msg = Resources.Lang.FrmSetDB_ASRS_Msg08 + "：" + err.Message; //连接失败
           boolTF = false;
        }
        return boolTF;
    }
}

