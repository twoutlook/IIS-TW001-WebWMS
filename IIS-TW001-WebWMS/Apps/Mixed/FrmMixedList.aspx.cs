using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.Entity.SqlServer;
using DreamTek.ASRS.Business.Mixed;

public partial class Apps_RD_FrmMixedList : WMSBasePage
{
    //DBContext context = new DBContext();
    MixedQuery query = new MixedQuery();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            this.InitPage();        
        }
    }

    #region IPageGrid 成员
    public void GridBind()
    {
        int pageCount = 0;
        DataTable dt = query.MixedQueryList(this.txtMixedCode.Text.Trim(), txtERP_No.Text.Trim(), txtPalledCode.Text.Trim(), txtITYPE.SelectedValue, txtCinvcode.Text.Trim(), ddlCSTATUS.SelectedValue, this.txtCCREATEOWNERCODE.Text.Trim(), txtDCREATETIMEFrom.Text.Trim(), txtDCREATETIMETo.Text.Trim(), CurrendIndex, PageSize, out pageCount);
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + ":<b>" + "</b>"; //总页数
        AspNetPager1.RecordCount = pageCount;
        AspNetPager1.PageSize = this.PageSize;
        grdMIXED.DataSource = dt;
        grdMIXED.DataBind();
    }
    #endregion
    
    #region IPage 成员
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        //this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdMIXED.DataKeyNames = new string[] { "ID", "CSTATUS" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口       
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "MixedStatus", false, -1, -1), this.ddlCSTATUS, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", ""); //全部
        Help.DropDownListDataBind(GetOutType(true), this.txtITYPE, Resources.Lang.Common_ALL, "FUNCNAME", "EXTEND1", "");//"全部"
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + BuildRequestPageURL("FrmMixed_D.aspx", SYSOperation.New, "") + "','" + Resources.Lang.FrmMixedList_Msg03+ "','Mixed_D');return false;";//新建配料单
    }
    #endregion

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsFirstPage)//判断是否是首页
            {
                CurrendIndex = 1;
                AspNetPager1.CurrentPageIndex = 1;
            }
            this.GridBind();
            IsFirstPage = true;//恢复默认值
        }
        catch (Exception err)
        {
            DBLog.Log(err.Message.ToString());
        }
    }
    protected void grdINASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[5].Text = GetOUTTypeName(e.Row.Cells[5].Text);
                string strKeyID = this.grdMIXED.DataKeys[e.Row.RowIndex].Values[0].ToString();//this.GetKeyIDS(e.Row.RowIndex);
                HyperLink linkView = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
                linkView.NavigateUrl = "#";
                this.OpenFloatWinMax(linkView, BuildRequestPageURL("FrmMixed_D.aspx", SYSOperation.View, strKeyID), Resources.Lang.FrmMixedList_Msg04, "Mixed_D");//配料详情
                string status = this.grdMIXED.DataKeys[e.Row.RowIndex].Values[1].ToString();//状态
                if (status == "0") //配料中 //0	配料中  1	暂存中   3	已完成  2	已交付  if (e.Row.Cells[6].Text.Equals("配料中"))
                {
                    HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
                    linkModify.NavigateUrl = "#";
                    this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmMixed_D.aspx", SYSOperation.Modify, strKeyID), Resources.Lang.FrmMixedList_Msg04, "Mixed_D"); //配料详情
                }
                //e.Row.Cells[6].Text = query.GetStatusName(e.Row.Cells[6].Text);
            }
        }
        catch (Exception)
        {
        }
    }

    /// <summary>
    /// 获取入库类型名称
    /// </summary>
    /// <param name="iType"></param>
    /// <returns></returns>
    public static string GetInTypeName(string iType)
    {
        string strSQL = string.Format(@"SELECT A.TYPENAME FROM INTYPE A WHERE A.CERPCODE = '{0}'", iType);
        return DBHelp.ExecuteScalar(strSQL).ToString();
    }
    /// <summary>
    /// 获取出库类型名称
    /// </summary>
    /// <param name="iType"></param>
    /// <returns></returns>
    public static string GetOUTTypeName(string iType)
    {
        string strSQL = string.Format(@"SELECT A.TYPENAME FROM OUTTYPE A WHERE A.CERPCODE = '{0}'", iType);
        return DBHelp.ExecuteScalar(strSQL).ToString();
    }
    public bool CheckData()
    {
        return false;
    }

    //private string GetKeyIDS(int rowIndex)
    //{
    //    string strKeyId = "";
    //    for (int i = 0; i < this.grdMIXED.DataKeyNames.Length; i++)
    //    {
    //        strKeyId += this.grdMIXED.DataKeys[rowIndex].Values[i].ToString() + ",";
    //    }
    //    strKeyId = strKeyId.TrimEnd(',');
    //    return strKeyId;
    //}
   
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            for (int i = 0; i < this.grdMIXED.Rows.Count; i++)
            {
                if (this.grdMIXED.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdMIXED.Rows[i].Cells[0].Controls[1];
                    // string status = this.grdINBILL.Rows[i].Cells[10].Text;
                    if (chkSelect.Checked)
                    {
                        var strStatus = this.grdMIXED.DataKeys[i].Values[1].ToString();
                        string mixedcode = this.grdMIXED.Rows[i].Cells[2].Text;
                        if (strStatus!="0")  //配料中 //0	配料中  1	暂存中   3	已完成  2	已交付  this.grdMIXED.Rows[i].Cells[6].Text.Equals("配料中")
                        {
                            msg += Resources.Lang.FrmMixedList_Title02 + "[" + mixedcode + "]" + Resources.Lang.FrmMixedList_Msg05;  //"配料单[" + mixedcode + "]状态必须为配料中！";
                            break;
                        }
                        else
                        {
                            //如果该配料单下已经有配料明细或者配料单扫描明细则不可以删除
                            if (query.IsExistsMixedDetails(mixedcode) == "1") //存在明细数据
                            {
                                msg += Resources.Lang.FrmMixedList_Title02 + "[" + mixedcode + "]" + Resources.Lang.FrmMixedList_Msg06; //"配料单[" + mixedcode + "]已经有明细数据，不可以删除！";
                                break;
                            }
                        }
                      
                        //检查配料单是否已暂存叫车
                        IGenericRepository<WCS_TaskProcess> con = new GenericRepository<WCS_TaskProcess>(context);
                        var caseList = from p in con.Get()
                                       orderby p.CREATETIME descending
                                       where p.SOURCECODE == mixedcode
                                       select p;
                        if (caseList != null && caseList.ToList().Count > 0)
                        {
                            msg += Resources.Lang.FrmMixedList_Title02 + "[" + this.grdMIXED.Rows[i].Cells[2].Text + "]" + Resources.Lang.FrmMixedList_Msg07; //"配料单[" + this.grdMIXED.Rows[i].Cells[2].Text + "]已产生调度任务！";
                            break;
                        }
                        //检查是否为PDA生成
                        IGenericRepository<OUTMIXED> conn = new GenericRepository<OUTMIXED>(context);
                        string id = this.grdMIXED.DataKeys[i].Values[0].ToString();
                        OUTMIXED entity = (from p in conn.Get()
                                           where p.ID == id
                                           select p).ToList().FirstOrDefault();
                        if (entity.CDEFIEND2 == "1")
                        {
                            msg += Resources.Lang.FrmMixedList_Title02 + "[" + this.grdMIXED.Rows[i].Cells[2].Text + "]" + Resources.Lang.FrmMixedList_Msg08; //"配料单[" + this.grdMIXED.Rows[i].Cells[2].Text + "]为PDA生成,不允许操作！";
                            break;
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                for (int i = 0; i < this.grdMIXED.Rows.Count; i++)
                {
                     CheckBox chkSelect = (CheckBox)this.grdMIXED.Rows[i].Cells[0].Controls[1];
                    // string status = this.grdINBILL.Rows[i].Cells[10].Text;
                    if (chkSelect.Checked)
                    {
                        string mixedid = this.grdMIXED.DataKeys[i].Values[0].ToString();
                        //删除明细，删除单头
                        DeleteOutMixed(mixedid);
                    }
                }
                msg = Resources.Lang.Common_SuccessDel;//删除成功！
            }
            else
            {
                msg = Resources.Lang.Common_FailDel + msg; //"删除失败！"
            }
            btnSearch_Click(null, null);
        }
        catch (Exception E)
        {
            //this.Alert(Resources.Lang.Common_SuccessDel + E.Message.ToJsString());  //删除失败！
            msg += Resources.Lang.Common_FailDel;  //"删除失败！"
            //DBUtil.Rollback(); 
        }
        this.Alert(msg);
    }

    private void DeleteOutMixed(string id)
    {
        string strSQL = string.Format(@"
                                         DELETE  dbo.TEMP_OUTMIXED_D
                WHERE   inbillcticketcode = ( SELECT    MIXEDCODE
                                              FROM      dbo.OUTMIXED
                                              WHERE     ID = '{0}'
                                            )
                                        DELETE dbo.OUTMIXED_D WHERE ID = '{0}'
                                        DELETE dbo.OUTMIXED WHERE ID = '{0}'", id);
        DBHelp.ExecuteNonQuery(strSQL);
    }

    /// <summary>
    /// 配料完成
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnComplete_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            for (int i = 0; i < this.grdMIXED.Rows.Count; i++)
            {
                if (this.grdMIXED.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdMIXED.Rows[i].Cells[0].Controls[1];
                    var strStatus = this.grdMIXED.DataKeys[i].Values[1].ToString();
                    // string status = this.grdINBILL.Rows[i].Cells[10].Text;
                    if (chkSelect.Checked)
                    {
                        if (strStatus!="0")  //配料中 //0	配料中  1	暂存中   3	已完成  2	已交付  if (!this.grdMIXED.Rows[i].Cells[6].Text.Equals("配料中")) 
                        {
                            msg += Resources.Lang.FrmMixedList_Title02 + "[" + this.grdMIXED.Rows[i].Cells[2].Text + "]" + Resources.Lang.FrmMixedList_Msg05; //"配料单[" + this.grdMIXED.Rows[i].Cells[2].Text + "]状态必须为配料中！";
                            break;
                        }
                    }
                }
            }
            if (msg.Length == 0)
            {
                for (int i = 0; i < this.grdMIXED.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)this.grdMIXED.Rows[i].Cells[0].Controls[1];
                    // string status = this.grdINBILL.Rows[i].Cells[10].Text;
                    if (chkSelect.Checked)
                    {
                        string mixedid = this.grdMIXED.DataKeys[i].Values[0].ToString();
                        //删除明细，删除单头
                        DeleteOutMixed(mixedid);
                    }
                }
                msg = Resources.Lang.Common_SuccessDel;//删除成功！
            }
            else
            {
                msg = Resources.Lang.Common_FailDel + msg; //"删除失败！"
            }
            btnSearch_Click(null, null);
        }
        catch (Exception E)
        {
            //this.Alert(Resources.Lang.Common_SuccessDel + E.Message.ToJsString());  //删除失败！
            msg += Resources.Lang.Common_FailDel;  //"删除失败！"
            //DBUtil.Rollback(); 
        }
        this.Alert(msg);
    }
}