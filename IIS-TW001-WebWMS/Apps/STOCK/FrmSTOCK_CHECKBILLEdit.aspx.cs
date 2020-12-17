using System;
using System.Collections;
using System.Configuration;
using System.Data;
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
using DreamTek.ASRS.Business;
using System.Linq.Dynamic;

/// <summary>
/// 描述: 盘点单-->FrmSTOCK_CHECKBILLEdit 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-10-17 16:02:04
/// </summary>
/*
Roger
2013/7/2 8:44:29
20130702084429
解决狂点按钮重复提交问题
*/
public partial class STOCK_FrmSTOCK_CHECKBILLEdit : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.grdSTOCK_CHECKBILL_D.Columns[1].Visible = false;
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() == SYSOperation.Modify)
            {
                ShowData();
                //btnDelete0.Enabled = true;
            }
            else if (this.Operation() == SYSOperation.Preserved1)//FrmSTOCK_CHECKBILL_DEdit
            {
                ShowData();
                //Flag=0 选择添加料号只能添加一条 
                this.WriteScript("PopupFloatWinMax('" + BuildRequestPageURL("Frm_SELECTSTOCKINFO.aspx", SYSOperation.New, "") + "&IDH=" + this.KeyID + "&Flag=1','" + Resources.Lang.FrmSTOCK_CHECKBILLEdit_PanDainDan + "','Frm_SELECTSTOCKINFO_D');");//盘点单
                btnDelete0.Enabled = true;
            }
            else
            {
                this.txtCCREATEOWNERCODE.Text = WmsWebUserInfo.GetCurrentUser().UserNo;
                this.txtID.Text = Guid.NewGuid().ToString();
                this.txtDCHECKDATE.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtDCREATETIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                btnDelete0.Enabled = false;
            }
        }
        this.btnSave.Attributes["onclick"] = this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true;";
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch_Roblck'].click(); CloseMySelf('STOCK_CHECKBILL');return false;";
        this.grdSTOCK_CHECKBILL_D.DataKeyNames = new string[] { "IDS" };
        //删除确认提示
        if (this.Operation() == SYSOperation.Modify)
        {
            this.btnDelete.Attributes["onclick"] = "var userNo = '" + this.KeyID + "'; return window.confirm('" + Resources.Lang.FrmSTOCK_CHECKBILLEdit_YaoDelete + "' + userNo + '?');";//要删除
        }
        else
        {
            this.btnDelete.Visible = false;
        }
        //设置保存按钮的文字及其状态
        if (this.Operation() == SYSOperation.View)
        {
            this.btnSave.Visible = false;
        }
        else if (this.Operation() == SYSOperation.Approve)
        {
            this.btnSave.Text = Resources.Lang.FrmOUTASNEdit_ShengPi;//审批
        }

        var list = GetParametersByFlagType("CHECKSTATE");
        var item2 = list.Where(x => x.FLAG_ID == "2").FirstOrDefault();
        if (item2 != null) {
            list.Remove(item2);
        }
        var item3 = list.Where(x => x.FLAG_ID == "3").FirstOrDefault();
        if (item3 != null)
        {
            list.Remove(item3);
        }
        var item4 = list.Where(x => x.FLAG_ID == "4").FirstOrDefault();
        if (item4 != null)
        {
            list.Remove(item4);
        }
        Help.DropDownListDataBind(list, txtCSTATUS, "", "FLAG_NAME", "FLAG_ID", "");
        Help.DropDownListDataBind(GetParametersByFlagType("CHECKTYPE"), txtCHECKTYPE, "", "FLAG_NAME", "FLAG_ID", "");
    }

    #endregion
    #region IPageGrid 成员

    public void GridBind()
    {
        Bind("");
    }

    public IQueryable<STOCK_CHECKBILL_D> GetQueryList()
    {
        IGenericRepository<STOCK_CHECKBILL_D> conn = new GenericRepository<STOCK_CHECKBILL_D>(db);
        var caseList = from p in conn.Get()
                       orderby p.cinvcode descending
                       where 1 == 1
                       select p;
        if (caseList != null && caseList.Count() > 0)
        {
            if (txtID.Text != string.Empty)
            {
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.id) && x.id.Contains(txtID.Text.Trim()));
            }

        }
        return caseList;
    }

    public void Bind(string sortStr)
    {
        var caseList = GetQueryList();
        if (caseList != null && caseList.Count() > 0)
        {
            //按照列名排序
            if (!string.IsNullOrEmpty(sortStr))
            {
                caseList = caseList.OrderBy(sortStr);
            }
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        grdSTOCK_CHECKBILL_D.DataSource = GetPageSize(caseList, PageSize, CurrendIndex).ToList();
        grdSTOCK_CHECKBILL_D.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        Bind("");
    }

    #endregion
    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData()
    {
        IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);
        STOCK_CHECKBILL entity = new STOCK_CHECKBILL();
        entity = (from p in conn.Get()
                  where p.id == this.KeyID
                  select p
                                  ).FirstOrDefault();

        if (entity != null && !entity.id.IsNullOrEmpty())
        {
            this.txtID.Text = entity.id.ToString();
            this.txtCTICKETCODE.Text = entity.cticketcode;
            this.txtCCHECKNAME.Text = entity.ccheckname;
            this.txtDCHECKDATE.Text = entity.dcheckdate.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtCERPCODE.Text = entity.cerpcode;
            this.txtDCIRCLECHECKBEGINDATE.Text = entity.dcirclecheckbegindate != null ? entity.dcirclecheckbegindate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtDCIRCLECHECKENDDATE.Text = entity.dcirclecheckenddate != null ? entity.dcirclecheckenddate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            txtCHECKTYPE.SelectedValue = entity.checktype;
            this.txtDCREATETIME.Text = entity.dcreatetime != null ? entity.dcreatetime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtCCREATEOWNERCODE.Text = entity.ccreateownercode;
            txtCSTATUS.SelectedValue = entity.cstatus;
            this.txtDAUDITTIME.Text = entity.daudittime != null ? entity.daudittime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
            this.txtCAUDITPERSON.Text = entity.cauditperson;
            this.txtPLANNAME.Text = entity.planname;
            this.txtID.Text = entity.id;
            this.txtCMEMO.Text = entity.cmemo;
            if (!string.IsNullOrEmpty(entity.cdefine1))
            {
                this.chkISCHECKBILL.Checked = entity.cdefine1.ToBoolean();
            }
            if (entity.cstatus != "0")
            {
                this.btnSave.Enabled = false;
                this.btnDelete0.Enabled = false;
                this.btnNew.Enabled = false;
                this.txtDCHECKDATE.Enabled = false;
                imgDCHECKDATE.Attributes["onclick"] = "";
                txtCERPCODE.Enabled = false;
                txtCCHECKNAME.Enabled = false;
                txtPLANNAME.Enabled = false;
                chkISCHECKBILL.Enabled = false;
                txtCMEMO.Enabled = false;
            }
            if (entity.cstatus == "1")
            {
                this.txtDCHECKDATE.Enabled = false;
                imgDCHECKDATE.Attributes["onclick"] = "";
                txtCERPCODE.Enabled = false;
                txtCCHECKNAME.Enabled = false;
                txtPLANNAME.Enabled = false;
                chkISCHECKBILL.Enabled = false;
                txtCMEMO.Enabled = false;
            }
            btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }

    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (this.txtDCHECKDATE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_NeedPanDianDate);//盘点日期项不允许空！
            this.SetFocus(txtDCHECKDATE);
            return false;
        }

        if (this.txtCCHECKNAME.Text.Trim().IsNullOrEmpty())
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_NeedPanDianName);//盘点单名称项不能为空！
            this.SetFocus(txtCCHECKNAME);
            return false;
        }

        if (this.txtCHECKTYPE.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_NeedPanDianType);//盘点类型项不允许空！
            this.SetFocus(txtCHECKTYPE);
            return false;
        }
        if (this.txtDCREATETIME.Text.Trim().Length > 0)
        {
            if (this.txtDCREATETIME.Text.IsDate("yyyy-MM-dd HH:mm:ss") == false)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_ZhiDanRiError);//制单日期项不是有效的日期！
                this.SetFocus(txtDCREATETIME);
                return false;
            }
        }
        //
        if (this.txtCCREATEOWNERCODE.Text.Trim().Length > 0)
        {
            if (this.txtCCREATEOWNERCODE.Text.GetLengthByByte() > 40)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_ZhiDanRenLength);//制单人项超过指定的长度40！
                this.SetFocus(txtCCREATEOWNERCODE);
                return false;
            }
        }
        //
        if (this.txtCSTATUS.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit_NeedCstatus);//状态项不允许空！
            this.SetFocus(txtCSTATUS);
            return false;
        }
        //
        if (this.txtCSTATUS.Text.Trim().Length > 0)
        {
            if (this.txtCSTATUS.Text.GetLengthByByte() > 20)
            {
                this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit_CstatusLength);//状态项超过指定的长度20！
                this.SetFocus(txtCSTATUS);
                return false;
            }
        }

        if (this.txtCAUDITPERSON.Text.Trim().Length > 0)
        {
            if (this.txtCAUDITPERSON.Text.GetLengthByByte() > 40)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_AuditorLength);//审核人项超过指定的长度40！
                this.SetFocus(txtCAUDITPERSON);
                return false;
            }
        }
        //
        if (this.txtID.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_IdBuNengKong);//ID项不允许空！
            this.SetFocus(txtID);
            return false;
        }
        //
        if (this.txtID.Text.Trim().Length > 0)
        {
            if (this.txtID.Text.GetLengthByByte() > 36)
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_IdLength);//ID项超过指定的长度36！
                this.SetFocus(txtID);
                return false;
            }
        }
        //
        if (this.txtCMEMO.Text.Trim().Length > 0)
        {
            if (this.txtCMEMO.Text.GetLengthByByte() > 200)
            {
                this.Alert(Resources.Lang.FrmOUTASNEdit_Tips_BeiZhuChangDu);//备注项超过指定的长度200！
                this.SetFocus(txtCMEMO);
                return false;
            }
        }

        if (this.txtPLANNAME.Text.Trim().Length > 0)
        {
            if (this.txtPLANNAME.Text.GetLengthByByte() > 100)
            {
                this.Alert(Resources.Lang.FrmSTOCK_CHECKBILLEdit_PlanNameLength);//盘点计划名称超过指定的长度100！
                this.SetFocus(txtPLANNAME);
                return false;
            }
        }
        return true;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public STOCK_CHECKBILL SendData()
    {
        STOCK_CHECKBILL entity = new STOCK_CHECKBILL();
        if (this.Operation() != SYSOperation.New)
        {
            IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);
            entity = (from p in conn.Get()
                      where p.id == txtID.Text
                      select p
                          ).FirstOrDefault();
        }
        this.txtCTICKETCODE.Text = this.txtCTICKETCODE.Text.Trim();
        if (this.txtCTICKETCODE.Text.Length > 0)
        {
            entity.cticketcode = txtCTICKETCODE.Text;
        }
        else
        {
            entity.cticketcode = new InAsn().CreateNo("STOCK_CHECKBILL");
            //entity.SetDBNull("CTICKETCODE",true);
            //对于值类型的字段，系统也可以更新或者插入为NULL值，但是实体定义的地方需要改动，例如 public int UserName==>public int? UserName
            //entity.CTICKETCODE = null;
        }

        //盘点单名称
        this.txtCCHECKNAME.Text = this.txtCCHECKNAME.Text.Trim();
        if (this.txtCCHECKNAME.Text.Length > 0)
        {
            entity.ccheckname = txtCCHECKNAME.Text;
        }
        //盘点计划名称
        this.txtPLANNAME.Text = this.txtPLANNAME.Text.Trim();
        if (this.txtPLANNAME.Text.Length > 0)
        {
            entity.planname = txtPLANNAME.Text;
        }
        //
        this.txtDCHECKDATE.Text = this.txtDCHECKDATE.Text.Trim();
        if (this.txtDCHECKDATE.Text.Length > 0)
        {
            entity.dcheckdate = txtDCHECKDATE.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
            if (this.txtDCHECKDATE.Text.Length == 19)
            {
                entity.dcheckdate = Convert.ToDateTime(txtDCHECKDATE.Text.Trim());
            }
            else
            {
                entity.dcheckdate = Convert.ToDateTime(txtDCHECKDATE.Text.Trim() + " " + Help.DateTime_ToChar(DateTime.Now.Hour) + ":" + Help.DateTime_ToChar(DateTime.Now.Minute) + ":" + Help.DateTime_ToChar(DateTime.Now.Second));
            }
            //entity.DCHECKDATE = txtDCHECKDATE.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }
        //
        this.txtCERPCODE.Text = this.txtCERPCODE.Text.Trim();
        if (this.txtCERPCODE.Text.Length > 0)
        {
            entity.cerpcode = txtCERPCODE.Text;
        }
        //
        this.txtDCIRCLECHECKBEGINDATE.Text = this.txtDCIRCLECHECKBEGINDATE.Text.Trim();
        if (this.txtDCIRCLECHECKBEGINDATE.Text.Length > 0)
        {
            if (this.txtDCIRCLECHECKBEGINDATE.Text.Length == 19)
            {
                entity.dcirclecheckbegindate = Convert.ToDateTime(txtDCIRCLECHECKBEGINDATE.Text.Trim());
            }
            else
            {
                entity.dcirclecheckbegindate = Convert.ToDateTime(txtDCIRCLECHECKBEGINDATE.Text.Trim() + " " + Help.DateTime_ToChar(DateTime.Now.Hour) + ":" + Help.DateTime_ToChar(DateTime.Now.Minute) + ":" + Help.DateTime_ToChar(DateTime.Now.Second));
            }
        }
        //
        this.txtDCIRCLECHECKENDDATE.Text = this.txtDCIRCLECHECKENDDATE.Text.Trim();
        if (this.txtDCIRCLECHECKENDDATE.Text.Length > 0)
        {
            if (txtDCIRCLECHECKENDDATE.Text.Trim().Length == 19)
            {
                entity.dcirclecheckenddate = Convert.ToDateTime(txtDCIRCLECHECKENDDATE.Text.Trim());
            }
            else
            {
                entity.dcirclecheckenddate = Convert.ToDateTime(txtDCIRCLECHECKENDDATE.Text.Trim() + " " + Help.DateTime_ToChar(DateTime.Now.Hour) + ":" + Help.DateTime_ToChar(DateTime.Now.Minute) + ":" + Help.DateTime_ToChar(DateTime.Now.Second));
            }
        }
        //
        this.txtCHECKTYPE.Text = this.txtCHECKTYPE.Text.Trim();
        if (this.txtCHECKTYPE.Text.Length > 0)
        {
            entity.checktype = txtCHECKTYPE.Text;
        }

        //
        this.txtDCREATETIME.Text = this.txtDCREATETIME.Text.Trim();
        if (this.txtDCREATETIME.Text.Length > 0)
        {
            entity.dcreatetime = txtDCREATETIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }

        //
        this.txtCCREATEOWNERCODE.Text = this.txtCCREATEOWNERCODE.Text.Trim();
        if (this.txtCCREATEOWNERCODE.Text.Length > 0)
        {
            entity.ccreateownercode = txtCCREATEOWNERCODE.Text;
        }

        //
        this.txtCSTATUS.Text = this.txtCSTATUS.Text.Trim();
        if (this.txtCSTATUS.Text.Length > 0)
        {
            entity.cstatus = txtCSTATUS.Text;
        }

        //
        this.txtDAUDITTIME.Text = this.txtDAUDITTIME.Text.Trim();
        if (this.txtDAUDITTIME.Text.Length > 0)
        {
            entity.daudittime = txtDAUDITTIME.Text.ToDateTime("yyyy-MM-dd HH:mm:ss");
        }

        //
        this.txtCAUDITPERSON.Text = this.txtCAUDITPERSON.Text.Trim();
        if (this.txtCAUDITPERSON.Text.Length > 0)
        {
            entity.cauditperson = txtCAUDITPERSON.Text;
        }

        //
        this.txtID.Text = this.txtID.Text.Trim();
        if (this.txtID.Text.Length > 0)
        {
            entity.id = txtID.Text;
        }

        //
        this.txtCMEMO.Text = this.txtCMEMO.Text.Trim();
        if (this.txtCMEMO.Text.Length > 0)
        {
            entity.cmemo = txtCMEMO.Text;
        }

        entity.cdefine1 = this.chkISCHECKBILL.Checked.ToString();
        entity.dcreatetime = DateTime.Now;

        //增加盘点计划ID Roger 20130604
        //entity.ddefine3 = STOCK_CHECKBILLRule.GetActivesPlan("1");
        entity.cdefine2 = "0";

        #region 界面上不可见的字段项
        /*
        entity.CDEFINE1 = ;
        entity.CDEFINE2 = ;
        entity.DDEFINE3 = ;
        entity.DDEFINE4 = ;
        entity.IDEFINE5 = ;
        */
        #endregion
        return entity;

    }

    /// <summary>
    /// 保存输入内容到数据库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.CheckData())
        {
            STOCK_CHECKBILL entity = (STOCK_CHECKBILL)this.SendData();
            if (this.Operation() != SYSOperation.New)
            { }
            string strKeyID = "";
            strKeyID += entity.id;
            try
            {
                IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);
                if (this.Operation() == SYSOperation.Modify || this.Operation() == SYSOperation.Preserved1)
                {
                    conn.Update(entity);
                    conn.Save();
                    this.AlertAndBack("FrmSTOCK_CHECKBILLEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Msg_SaveSuccess);//保存成功！
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    conn.Insert(entity);
                    conn.Save();

                    //STOCK_CHECKBILLRule.Insert(entity); 
                    strKeyID = "";
                    strKeyID += entity.id;
                    this.AlertAndBack("FrmSTOCK_CHECKBILLEdit.aspx?" + BuildQueryString(SYSOperation.Modify, strKeyID), Resources.Lang.WMS_Common_Msg_SaveSuccess);//保存成功！
                }
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.WMS_Common_Msg_Failed + E.Message); //失败！
            }
            finally
            {
                //20130702084429
                btnSave.Style.Remove("disabled");
            }
        }
        else
        {
            //20130702084429
            btnSave.Style.Remove("disabled");
        }

    }

    protected DataTable grdNavigatorSTOCK_CHECKBILL_D_GetExportToExcelSource()
    {

        return null;
    }

    protected void grdSTOCK_CHECKBILL_D_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void grdSTOCK_CHECKBILL_D_PageIndexChanged(object sender, EventArgs e)
    {
        this.GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.CurrendIndex = 1;
        this.GridBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            IGenericRepository<STOCK_CHECKBILL_D> conn = new GenericRepository<STOCK_CHECKBILL_D>(db);

            for (int i = 0; i < this.grdSTOCK_CHECKBILL_D.Rows.Count; i++)
            {
                if (this.grdSTOCK_CHECKBILL_D.Rows[i].Cells[0].FindControl("chkSelect") is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdSTOCK_CHECKBILL_D.Rows[i].Cells[0].FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        //主键赋值
                        string strIDS = this.grdSTOCK_CHECKBILL_D.DataKeys[i].Values[0].ToString();
                        STOCK_CHECKBILL_D bo = (from p in conn.Get()
                                                where p.ids == strIDS
                                                select p).FirstOrDefault();
                        conn.Delete(bo.ids);
                        conn.Save();
                        count++;
                    }
                }
            }
            if (count > 0)
                this.Alert(Resources.Lang.WMS_Common_Msg_DeleteSuccess);//删除成功！
            else
                this.Alert(Resources.Lang.WMS_Common_Tips_SelectDelete);//请选择要删除的行
            btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.WMS_Common_Msg_DeleteFailed + E.Message.ToJsString());//删除失败！
        }
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdSTOCK_CHECKBILL_D.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdSTOCK_CHECKBILL_D.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdSTOCK_CHECKBILL_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

    protected void dsGrdSTOCK_CHECKBILL_D_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void dsGrdSTOCK_CHECKBILL_D_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {

        if (this.CheckData())
        {
            STOCK_CHECKBILL entity = (STOCK_CHECKBILL)this.SendData();
            if (this.Operation() != SYSOperation.New)
            {
            }
            string strKeyID = "";
            strKeyID += entity.id;
            try
            {
                IGenericRepository<STOCK_CHECKBILL> conn = new GenericRepository<STOCK_CHECKBILL>(db);
                if (this.Operation() == SYSOperation.Modify || this.Operation() == SYSOperation.Preserved1)
                {
                    conn.Update(entity);
                    conn.Save();
                }
                else if (this.Operation() == SYSOperation.New)
                {
                    conn.Insert(entity);
                    conn.Save();
                    strKeyID = "";
                    strKeyID += entity.id;
                }
                Response.Redirect(BuildRequestPageURL("FrmSTOCK_CHECKBILLEdit.aspx", SYSOperation.Preserved1, strKeyID));
            }
            catch (Exception E)
            {
                this.Alert(this.GetOperationName() + Resources.Lang.WMS_Common_Msg_Failed + E.Message);//失败！
            }
        }
    }

}

