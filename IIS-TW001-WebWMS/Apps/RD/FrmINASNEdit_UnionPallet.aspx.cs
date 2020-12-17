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
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Linq.Dynamic;
using DreamTek.ASRS.Business.SP.ProcedureModel;
/*
Roger
2013/7/2 8:44:29
20130702084429
解决狂点按钮重复提交问题
*/
/*
Roger
2013/7/31 13:46:49
20130731134649
增加校验是否为正在修改中的料号
*/
/*
Roger
2013/8/22 10:06:12
20130822100612
更新通知单状态
*/
/// <summary>
/// 描述: 入库管理-->FrmINASNEdit 页面后台类文件
/// 作者: --陈磊
/// 创建于: 2012-09-24 18:40:51
/// </summary>
public partial class FrmINASNEdit_UnionPallet : WMSBasePage// PageBase, IPageEdit
{


    /// <summary>
    /// 当前页数 -- 立库
    /// </summary>
    public int CurrendIndex1
    {
        get
        {
            if (ViewState["CurrendIndex1"] == null)
            {
                ViewState["CurrendIndex1"] = 1;
            }
            return (int)ViewState["CurrendIndex1"];
        }
        set
        {
            ViewState["CurrendIndex1"] = value;
        }
    }

    /// <summary>
    /// 当前页数 -- AGV
    /// </summary>
    public int CurrendIndex2
    {
        get
        {
            if (ViewState["CurrendIndex2"] == null)
            {
                ViewState["CurrendIndex2"] = 1;
            }
            return (int)ViewState["CurrendIndex2"];
        }
        set
        {
            ViewState["CurrendIndex2"] = value;
        }
    }

    #region SQL
    //DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtID.Text = Request.QueryString["ID"];
        this.txtCinvcode.Text = GetSelectedCinvcodes().Replace("'","");

        if (this.IsPostBack == false)
        {
            InitPage();

        }
        
    }


    public Dictionary<string,string> SelectedCinvcodes
    {
        get { return Session["SelectCinvcodes"] as Dictionary<string, string>; }
        set { Session["SelectCinvcodes"] = value; }
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
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_Button1'].click(); CloseMySelf('IN_MERGE_PALLETE');return false;";
      
       
        //本页面打开新增窗口
        //this.btnNew.Attributes["onclick"] = "return PopupFloatWin('" + BuildRequestPageURL("FrmINASN_DEdit.aspx", SYSOperation.New, "") + "&IDS=" + this.KeyID + "','','INASN_D',600,400);";
        //Help.DropDownListDataBind(new SysParameterList().GetInType(true), this.ddlITYPE, "全部", "FUNCNAME", "EXTEND1", "");
        //Help.DropDownListDataBind(GetReasonCode("1"), this.ddlREASONCODE, "请选择类型", "REASONCONTENT", "REASONCODE", "");
               
        GridBind_grdStock();
        GridBind_grdStock_D();

        if (this.grdStock.DataSource == null && this.GrdStock_d.DataSource == null)
        {
            //库存中没有所选料号的信息，或此料号正在出库，循环盘点
            Alert(Resources.Lang.FrmINASNEdit_UnionPallet_MSG4+"！");
        }
       // ddlsiteType 储位规格
        Help.DropDownListDataBind(GetParametersByFlagType("POSICITIONTYPE"), this.ddlsiteType, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", "");  //全部
        
    }
    /// <summary>
    /// 获取入库理由码信息
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public DataTable GetReasonCode(string type)
    {
        string sql = string.Format(@"SELECT A.REASONCODE,A.REASONCONTENT FROM BASE_DOCUREASON A WHERE A.STATES = 'Y' AND A.ACTIONSCOPE = '{0}'", type);
        return DBHelp.ExecuteToDataTable(sql);
    }
    #endregion

    

    /// <summary>
    /// 根据主键值，数据库内容填入输入项控件
    /// </summary>
    public void ShowData(string id)
    {
        
    }

    #region IPageGrid 成员
    private string GetSelectedCinvcodes()
    {
        var cinvcodes=string.Empty;
        if (SelectedCinvcodes != null)
        {
            foreach (var item in SelectedCinvcodes)
            {
                cinvcodes =cinvcodes+ "'" + item.Value + "',";
            }

            cinvcodes = cinvcodes.Substring(0, cinvcodes.Length-1);
            
        }

        return cinvcodes;
    }

    private string[] GetSelectedCinvcodeVals()
    {
        var cinvcodes = new string[SelectedCinvcodes.Count];
        int i = 0;
        if (SelectedCinvcodes != null)
        {
            foreach (var item in SelectedCinvcodes)
            {
                cinvcodes[i] = item.Value;
                i++;
            }

        }

        return cinvcodes;
    }



    public void GridBind_grdStock()
    {
        
        IGenericRepository<V_STOCK_Current_ALL> con = new GenericRepository<V_STOCK_Current_ALL>(db);
        IGenericRepository<STOCK_CURRENT> con1 = new GenericRepository<STOCK_CURRENT>(db);

        var caseList = from p in con.Get().AsEnumerable()
                       join s in con1.Get().AsEnumerable() on p.cpositioncode equals s.cpositioncode
                       where p.cdefine2 == "1" && s.cstatus == "0"
                       && (GetSelectedCinvcodeVals()).Contains(s.cinvcode)
                       select new
                       {
                           p.cposition,
                           p.cpositioncode,
                           p.ctype,
                           p.cdefine2,
                           p.cwarehouse,
                           p.cwarehousecode,
                           p.palletcode,
                           p.iqty,
                           p.id,
                           p.ERPCODE,
                           s.cinvcode,
                           s.cinvname
                       };



        if (caseList != null && caseList.Count() > 0)
        {

            if (txtcpositioncode.Text != string.Empty)
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtcpositioncode.Text.Trim()));
                }
            //if (txtCinvcode.Text != string.Empty)
            //    {
            //        caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cin) && x.cposition.Contains(txtcposition.Text.Trim()));
            //    }

                if (!string.IsNullOrEmpty(ddlsiteType.SelectedValue))
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ctype) && x.ctype.Equals(ddlsiteType.SelectedValue));
                }


                if (!string.IsNullOrEmpty(this.txtPalletCode.Text))
                {
                    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.palletcode) && x.palletcode.Equals(txtPalletCode.Text));
                }
              

                //if (ddlIsAll.SelectedValue.Equals("1"))//没有
                //{
                //    caseList = caseList.Where(x => x.IDS == null || string.IsNullOrEmpty(x.IDS));
                //}
                //if (ddlIsAll.SelectedValue.Equals("2"))//有
                //{
                //    caseList = caseList.Where(x => x.IDS != null && !string.IsNullOrEmpty(x.IDS));
                //}


            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize =this.PageSize;
            AspNetPager1.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";

            grdStock.DataSource = GetPageSize(caseList.OrderBy(x=>x.ctype).AsQueryable(), PageSize, CurrendIndex1).ToList();
            grdStock.DataBind();

        }
        else
        {
            grdStock.DataSource = null;
            grdStock.DataBind();
            this.panel1.Visible = false;
            
            AspNetPager1.RecordCount = 0;
            AspNetPager1.PageSize = this.PageSize;

          
        }
    }
    
    public void GridBind_grdStock_D()
    {
        IGenericRepository<V_IN_MERGE_PALLETE> con = new GenericRepository<V_IN_MERGE_PALLETE>(db);
        var caseList = from p in con.Get()
                       where p.INASNID==this.txtID.Text
                       orderby p.cinvcode
                       select p;

        if (caseList != null && caseList.Count() > 0)
        {
            this.panel2.Visible = true;
            AspNetPager2.RecordCount = caseList.Count();
            AspNetPager2.PageSize = this.PageSize;
            AspNetPager2.CustomInfoHTML = Resources.Lang.FrmDispatchUnitList_MsgTitle12 + " :<b>" + "</b>";// " 总页数:<b>" + "</b>";

            GrdStock_d.DataSource = GetPageSize(caseList, PageSize, CurrendIndex2).ToList();
            GrdStock_d.DataBind();

        }
        else
        {
            GrdStock_d.DataSource = null;
            GrdStock_d.DataBind();
            this.panel2.Visible = false;
            AspNetPager2.RecordCount = 0;
            AspNetPager2.PageSize = this.PageSize;

        }
      
    }


    #endregion
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {

        this.CurrendIndex1 = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind_grdStock();
    }


    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex2 = AspNetPager2.CurrentPageIndex;//索引同步
        GridBind_grdStock_D();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex1 = 1;
        AspNetPager1.CurrentPageIndex = 1;
        this.GridBind_grdStock();
    }

    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        //for (int i = 0; i < this.grdINASN_D.DataKeyNames.Length; i++)
        //{
        //    strKeyId += this.grdINASN_D.DataKeys[rowIndex].Values[i].ToString() + ",";
        //}
        strKeyId = this.grdStock.DataKeys[rowIndex].Values[0].ToString();//strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdINASN_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }

    protected void dsGrdINASN_D_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

   
    /// <summary>
    /// 校验数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        string msg = string.Empty;
        return false;

    }
    /// <summary>
    /// 根据页面上的数据构造相应的实体类返回
    /// </summary>
    /// <returns></returns>
    public INASN SendData( IGenericRepository<INASN> con)
    {
        return null;
    }


    protected void btnNew_Click(object sender, EventArgs e)
    {
        int selectedCount = 0;
        this.btnNew.Enabled = false;
        string msg = string.Empty;
        try
        {
            #region 
            
            for (int i = 0; i < this.grdStock.Rows.Count; i++)
            {
                if (this.grdStock.Rows[i].Cells[0].Controls[0] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdStock.Rows[i].Cells[0].Controls[0];
                    if (chkSelect.Checked)
                    {
                        selectedCount = selectedCount + 1;
                        //保存单头
                        string palletCode = this.grdStock.Rows[i].Cells[2].Text;

                        #region sql server 调用存储过程
                        PROC_CreateUnionPallet proc = new PROC_CreateUnionPallet();
                        proc.P_PalletCode = palletCode;
                        proc.P_Original_INAsnID = this.txtID.Text;
                        proc.Execute();
                        var P_ErrText = proc.P_INFOTEXT;
                        var P_Return_Value = proc.P_ReturnValue;

                        if (P_Return_Value ==1)
                        {
                            msg += "栈板号=["+palletCode + "]生成失败!" + P_ErrText;
                            this.btnNew.Enabled = true;

                            //GrdStock_d.DataSource = null;
                            //GrdStock_d.DataBind();
                            //this.panel2.Visible = false;
                            //AspNetPager2.RecordCount = 0;
                            //AspNetPager2.PageSize = this.PageSize;
                            ////20130702084429
                            //btnNew.Style.Remove("disabled");
                           // break;
                        }
                        else
                        {
                            msg +="栈板号=["+ palletCode+"]生成成功!";
                            this.btnNew.Enabled = true;
                           // break;
                        }
                        #endregion
                    }
                    //else
                    //{
                    //    msg = "请选择入库通知单!";
                    //    this.btnNew.Enabled = true;
                    //    //20130702084429
                    //    btnNew.Style.Remove("disabled");
                    //    break;
                    //}
                }
                else
                {
                    //20130702084429
                    btnNew.Style.Remove("disabled");
                    this.btnNew.Enabled = true;
                    break;
                }
               
            #endregion
            }

            if (selectedCount == 0)
            {
                //请选择栈板号
                msg = Resources.Lang.FrmINASNEdit_UnionPallet_MSG5+ "!";
                this.btnNew.Enabled = true;
            }
           
            Alert(msg);
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
            btnRefresh_Click(this.btnRefresh, EventArgs.Empty);
        }
        catch (Exception err)
        {
            Alert(err.Message);
            //20130702084429
            btnNew.Style.Remove("disabled");
            this.btnNew.Enabled = true;
        }
        //this.btnNew.Enabled = false;
        //SaveToDB(sender);
        //this.btnNew.Enabled = true;
       
    }

    private void SaveToDB(object sender)
    {
      
    }

   
    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }

    /// <summary>
    /// 获取选择的ID
    /// </summary>
    public void GetSelectedIds()
    {
        if (SelectIds == null)
        {
            SelectIds = new Dictionary<string, string>();
        }

        foreach (GridViewRow item in this.grdStock.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string ids = this.grdStock.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                //控件选中且集合中不存在添加
                if (cbo.Checked && !SelectIds.ContainsKey(ids))
                {
                    SelectIds.Add(ids, ids);
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(ids))
                {
                    SelectIds.Remove(ids);
                }
            }
        }
    }

    /// <summary>
    /// 生成入库单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateInBill_Click(object sender, EventArgs e)
    {
       
    }

    protected void grdINASN_D_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }

    /// <summary>
    /// 打印功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string printid = this.txtID.Text.Trim();
            //打印入庫通知單
            this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmINASNEdit_Print.aspx", SYSOperation.New, printid) + "','" + Resources.Lang.FrmINASNEdit_UnionPallet_MSG6+ "','BAR_REPACK',840,600);");

        }
        catch (Exception err)
        {
            Alert(err.Message);
        }
    }



    protected void btnISplit_Click(object sender, EventArgs e)
    {
        this.WriteScript("PopupFloatWin('" + BuildRequestPageURL("FrmiStockSplit.aspx", SYSOperation.New, txtID.Text.Trim()) + "','','FrmiStockSplit',1200,900);");
    }

    #endregion

  
    protected void GrdStock_d_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grdStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
                HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
                linkModify.NavigateUrl = "#";

                var cpositionCode = e.Row.Cells[e.Row.Cells.Count - 5].Text;
                var cinvcode = ((HiddenField)e.Row.FindControl("hdcinvcode")).Value;
                //库存明细
                this.OpenFloatWinMax(linkModify, BuildRequestPageURL("/Apps/STOCK/FrmSTOCK_CurrentQueryList.aspx?Flag=1&ids=" + cpositionCode, SYSOperation.View, cinvcode), Resources.Lang.FrmINASNEdit_UnionPallet_MSG7, "STOCK_Current");

            }
        }
        catch (Exception)
        {
        }

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        CurrendIndex2 = 1;
        AspNetPager2.CurrentPageIndex = 1;
        this.GridBind_grdStock_D();
        //this.panel2.Visible = true;

    }
   
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }

   
}

