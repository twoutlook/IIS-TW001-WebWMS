using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using System.Data.Entity.SqlServer;

/// <summary>
/// 描述: 设置操作人对应区域-->FrmBASE_CARGOSPACEList 页面后台类文件
/// 作者: --wjw
/// 创建于: 2013-01-01 16:29:23
/// </summary>
public partial class FrmBASE_OPERATOR_AREA :WMSBasePage
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        //ucShowArea.SetCompName = txtCDEFINE1.ClientID;
        //ucShowArea.SetORGCode = txtAreaID.ClientID;
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            string type = string.Empty; //操作类型

            LoadOPERATOR_AREA();


            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }

        btnSave.Attributes.Add("onclick", this.GetPostBackEventReference(btnSave) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中

    }

    /// <summary>
    /// 加载已设置的储位编码
    /// </summary>
    private void LoadBASE_PART_CARGOSPACE()
    {
        SelectIds =OPERATOR.GetBasePartCargoSpaceByPartId(Request.QueryString["ID"].ToString());
    }

    /// <summary>
    /// 加载操作人的已设置储位编号
    /// </summary>
    private void LoadOPERATOR_AREA()
    {
        SelectIds = OPERATOR.GetOperatorAreaByPartId(Request.QueryString["ID"].ToString());
    }


    #region IPageGrid 成员

    public void GridBind()
    {
        using (var modContext = context)
        {
            IGenericRepository<BASE_AREA> entity = new GenericRepository<BASE_AREA>(context);
            var caseList = from p in entity.Get()
                           orderby p.createtime descending
                           where 1 == 1
                           select p;

            if (!string.IsNullOrEmpty(txtFromDate.Text.Trim()))
                caseList = caseList.Where(x => x.area_name != null && SqlFunctions.DateDiff("dd", txtFromDate.Text.Trim(), x.createtime) >= 0);
            if (txtEndDate.Text != string.Empty)
                caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtEndDate.Text.Trim(), x.createtime) <= 0);
            if (txtAreaName.Text != string.Empty)
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.area_name) && x.area_name.Contains(txtAreaName.Text.Trim()));
            if (txtblCw.Text != string.Empty)
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.handover_cargo) && x.handover_cargo.Contains(txtblCw.Text.Trim()));

            if (txtCreateUser.Text != string.Empty)
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.createowner) && x.createowner.Contains(txtCreateUser.Text.Trim()));
            if (cbCF.SelectedValue != "")
                caseList = caseList.Where(x => x.flag.ToString().Equals(cbCF.SelectedValue));
            if (caseList != null && caseList.Count() > 0)
            {
                AspNetPager1.RecordCount = caseList.Count();
                AspNetPager1.PageSize = this.PageSize;
            }
            var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

            var source = from p in data
                         join oper in modContext.BASE_OPERATOR on p.createowner equals oper.caccountid into temp
                         from tt in temp.DefaultIfEmpty()
                         select new
                         {
                             p.id,
                             p.area_name,
                             p.memo,
                             p.handover_cargo,
                             p.handover_cargo_name,
                             createowner = tt == null ? "" : tt.coperatorname,
                             p.createtime                          
                         };

            grdBAS_AREA.DataSource = source.ToList();
            grdBAS_AREA.DataBind();
        }
    }

    public bool CheckData()
    {

        if (this.txtFromDate.Text.Trim().Length > 0)
        {
            if (StringExtension.IsDate( this.txtFromDate.Text)== false)
            {
                this.Alert(Resources.Lang.FrmBASE_OPERATOR_AREAPart_Msg01);//创建日期起始项不是有效的日期！
                this.SetFocus(txtFromDate);
                return false;
            }
            else
            {
                if (this.txtEndDate.Text.Trim() == "")
                {
                    this.Alert(Resources.Lang.FrmALLOCATEList_ToItemEmpty);//到项不允许空！
                    this.SetFocus(txtEndDate);
                    return false;
                }
                if (this.txtEndDate.Text.Trim().Length > 0)
                {
                    if (StringExtension.IsDate( this.txtEndDate.Text)== false)
                    {
                        this.Alert(Resources.Lang.FrmALLOCATEList_NoEffToItem);//到项不是有效的日期！
                        this.SetFocus(txtEndDate);
                        return false;
                    }
                }
            }
        }
        return true;

    }
    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdBAS_AREA.DataKeyNames = new string[] { "ID" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口

        this.btnBack.Attributes["onclick"] = "window.parent.document.getElementById('ctl00_txtPageSizeDetail').value='1';window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('BAR');return false;";

        this.btnNew.Attributes["onclick"] = "PopupFloatWin('" + WMSBasePage.BuildRequestPageURL("FrmBASE_CARGOSPACEEdit.aspx?Flag=0", SYSOperation.New, "") + "','" + Resources.Lang.FrmBASE_CARGOSPACEList_Mag03+ "','BASE_CARGOSPACE',800,600);return false;";//新建储位管理 

        Help.DropDownListDataBind(GetParametersByFlagType("BASE_CARGOSPACE.IS_ALLO"), cbCF, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//是否允许超发
        Help.DropDownListDataBind(GetParametersByFlagType("AREAPartSet"), ddlAllType, "", "FLAG_NAME", "FLAG_ID", "");//关联区域设置
        
    }

    #endregion

    private string GetAreaId()
    {
        string areaId = string.Empty;
        //if (string.IsNullOrEmpty(txtCDEFINE1.Text))
        //    txtAreaID.Text = string.Empty;
        //DataTable dtArea = new BAS_AREAListQuery().GetAREANAMEBYName(txtCDEFINE1.Text);
        //if (dtArea.Rows.Count > 0)
        //    areaId = dtArea.Rows[0]["Id"].ToString();
        //else areaId = txtAreaID.Text;
        return areaId;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        this.GridBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_OPERATOR> con = new GenericRepository<BASE_OPERATOR>(context);
        try
        {
            for (int i = 0; i < this.grdBAS_AREA.Rows.Count; i++)
            {
                if (this.grdBAS_AREA.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBAS_AREA.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                      
                        string ID = this.grdBAS_AREA.DataKeys[i].Values[0].ToString();
                        con.Delete(ID);
                        con.Save();
                    }
                }
            }
              this.Alert(Resources.Lang.Common_SuccessDel); //删除成功!         
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.Common_FailDel + E.Message.ToJsString()); //删除失败！
        }
    }



    private string GetKeyIDS(int rowIndex)
    {
        string strKeyId = "";
        for (int i = 0; i < this.grdBAS_AREA.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBAS_AREA.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdBAS_AREA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            //HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            //linkModify.NavigateUrl = "#";
            //this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBASE_CARGOSPACEEdit.aspx", SysOperation.Modify, strKeyID), "储位管理", "BASE_CARGOSPACE", 600, 370);

            //（0 正常,1,盘点冻结，2占用
            //switch (e.Row.Cells[e.Row.Cells.Count - 2].Text)
            //{
            //    case "0":
            //        e.Row.Cells[e.Row.Cells.Count - 2].Text = "正常";
            //        break;
            //    case "1":
            //        e.Row.Cells[e.Row.Cells.Count - 2].Text = "盘点冻结";
            //        break;
            //    case "2":
            //        e.Row.Cells[e.Row.Cells.Count - 2].Text = "占用";
            //        break;
            //    default:
            //        break;
            //}

            //DataTable dt1 = new BAS_AREAListQuery().GetAREANAMEBYId(e.Row.Cells[e.Row.Cells.Count - 14].Text);
            //if (dt1.Rows.Count > 0)
            //    e.Row.Cells[e.Row.Cells.Count - 14].Text = dt1.Rows[0]["Area_name"].ToString();


            //e.Row.Cells[e.Row.Cells.Count - 13].Text = e.Row.Cells[e.Row.Cells.Count - 13].Text == "Y" ? "是" : "否";
            //获取ID
            string id = this.grdBAS_AREA.DataKeys[e.Row.RowIndex][0].ToString();
            CheckBox cbo = e.Row.FindControl("chkSelect") as CheckBox;
            cbo.Attributes.Add("onclick", "SelIDCancelAll()");


            DataTable dt = OPERATOR.GetAllOperArea(Request.QueryString["ID"].ToString());
            if (dt.Rows.Count > 0)
            {
                //if (dt.Rows.Count == 1 && dt.Rows[0]["AREA_ID"].ToString() == "*")
                //{
                //    cbo.Checked = true;
                //}
                //else
                //{
                if (SelectIds.ContainsKey(id))
                {
                    //如果是控件处于选中状态
                    cbo.Checked = true;
                }
                //}
            }

        }

    }


  
    public Dictionary<string, string> SelectIds
    {
        set { ViewState["SelectIds"] = value; }
        get { return ViewState["SelectIds"] as Dictionary<string, string>; }
    }
    public Dictionary<string, string> NoSelectIds
    {
        set { ViewState["NoSelectIds"] = value; }
        get { return ViewState["NoSelectIds"] as Dictionary<string, string>; }
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

        foreach (GridViewRow item in this.grdBAS_AREA.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;

                //Product product = item.DataItem as Product;
                //获取ID
                string code = this.grdBAS_AREA.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;

                //控件选中且集合中不存在添加
                if (cbo.Checked && !SelectIds.ContainsKey(code))
                {
                    SelectIds.Add(code, code);
                }//未选中且集合中存在的移除                    
                else if (!cbo.Checked && SelectIds.ContainsKey(code))
                {
                    SelectIds.Remove(code);
                }
            }
        }
    }
    /// <summary>
    /// 获取没有选择的编号
    /// </summary>
    public void GetNoSelectedIds()
    {
        if (NoSelectIds == null)
            NoSelectIds = new Dictionary<string, string>();
        foreach (GridViewRow item in this.grdBAS_AREA.Rows)
        {
            Control itemFindControl = item.FindControl("chkSelect");
            if (itemFindControl != null && itemFindControl is CheckBox)
            {
                CheckBox cbo = itemFindControl as CheckBox;
                //获取ID
                string code = this.grdBAS_AREA.DataKeys[item.RowIndex][0].ToString();//(item.FindControl("lblId") as Label).Text;
                //控件选中且集合中不存在添加
                if (!cbo.Checked && !NoSelectIds.ContainsKey(code))
                {
                    NoSelectIds.Add(code, code);
                }
            }
        }
    }
    /// <summary>
    /// 保存按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {

        GetSelectedIds();
        string msg = "";

        GetSelectedIds();



        //1设置所选区域 2设置所有区域3取消所有区域关联
        string selType = ddlAllType.SelectedValue;

        if (selType == "1")
        {
            if (OPERATOR.SetOperorArea(SelectIds, Request.QueryString["ID"].ToString(), WmsWebUserInfo.GetCurrentUser().UserNo + "(" + WmsWebUserInfo.GetCurrentUser().UserName + ")"))
            {
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg03; //设置成功!
            }
            else
            {
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg04;//设置失败
            }
        }
        else if (selType == "2")
        {
            if (OPERATOR.SetOperAreaRel(Request.QueryString["ID"].ToString(), WmsWebUserInfo.GetCurrentUser().UserNo + "(" + WmsWebUserInfo.GetCurrentUser().UserName, true))
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg03; //设置成功!
            else
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg04;//设置失败
        }
        else
        {
            if (OPERATOR.SetCancelAllOperArea(Request.QueryString["ID"].ToString()))
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg03; //设置成功!
            else
                msg = Resources.Lang.FrmBASE_WL_AREA_Msg04;//设置失败
        }
        this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        Alert(msg);
    }

    /// <summary>
    /// 数据邦定前 获取上一页已选中的行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdBAS_AREA_DataBinding(object sender, EventArgs e)
    {
        GetSelectedIds();
    }

    #endregion
  

}

