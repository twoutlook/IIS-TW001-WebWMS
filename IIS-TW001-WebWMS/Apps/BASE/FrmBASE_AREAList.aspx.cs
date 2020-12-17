using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using DreamTek.ASRS.Repository;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Business.Base;
using System.Data.Entity.SqlServer;

/// <summary>
/// 描述: -->FrmBASE_AREAList 页面后台类文件
/// 作者: --武敬伟
/// 创建于: 2012-11-22 19:50:19
/// </summary>
/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/
public partial class FrmBASE_AREAList : WMSBasePage
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }
    public void InitPage()
    {
        this.btnNew.Attributes["onclick"] = "PopupFloatWin('" + WMSBasePage.BuildRequestPageURL("FrmBAS_AREAEdit.aspx?Flag=0", SYSOperation.New, "") + "','" + Resources.Lang.FrmBASE_AREAList_Msg01+ "','BASE_AREA',800,600);return false;";//新建区域

        Help.DropDownListDataBind(GetParametersByFlagType("YesOrNo"), cbCF, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//超发
        Help.DropDownListDataBind(GetParametersByFlagType("YesOrNo"), dpd_Control, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//是否控制区域
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    public void GridBind()
    {
        IGenericRepository<V_BASE_AREA> entity = new GenericRepository<V_BASE_AREA>(context);
        var caseList = from p in entity.Get()
                       orderby p.createtime descending
                       where 1 == 1
                       select p;
        if (!string.IsNullOrEmpty(txtAreaName.Text.Trim()))
            caseList = caseList.Where(x =>!string.IsNullOrEmpty(x.area_name) && x.area_name.Contains(txtAreaName.Text.Trim()));
        if (txtblCw.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.handover_cargo) && x.handover_cargo.Contains(txtblCw.Text.Trim()));
        if (txtCreateUser.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.createowner) && x.createowner.Contains(txtCreateUser.Text.Trim()));
       
        if (txtFromDate.Text != string.Empty)
            caseList = caseList.Where(x => x != null && x.createtime!=null && SqlFunctions.DateDiff("dd", txtFromDate.Text.Trim(),x.createtime) >= 0 );
        if (txtEndDate.Text != string.Empty)
            caseList = caseList.Where(x => x != null && x.createtime!=null && SqlFunctions.DateDiff("dd", txtEndDate.Text.Trim(), x.createtime) <= 0 );
        if (cbCF.SelectedValue != "")
            caseList = caseList.Where(x => x.flag.ToString().Equals(cbCF.SelectedValue));
        if (dpd_Control.SelectedValue != "")
            caseList = caseList.Where(x => x.is_control.ToString().Equals(dpd_Control.SelectedValue));
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
        }
        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("flag", "YesOrNo"));//超发
        flagList.Add(new Tuple<string, string>("is_control", "YesOrNo"));//是否控制区域

        var srcdata = GetGridSourceDataByList(data, flagList);

        grdBAS_AREA.DataSource = srcdata;
        grdBAS_AREA.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsFirstPage)//判断是否是首页
        {
            CurrendIndex = 1;//默认第一页
            AspNetPager1.CurrentPageIndex = 1;
        }
        IsFirstPage = true;
        this.GridBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_AREA> con = new GenericRepository<BASE_AREA>(context);
        BaseCommQuery bcq = new BaseCommQuery();
        int count = 0;
        try
        {
            for (int i = 0; i < this.grdBAS_AREA.Rows.Count; i++)
            {
                if (this.grdBAS_AREA.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBAS_AREA.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string Id = this.grdBAS_AREA.DataKeys[i].Values[0].ToString();
                        var msg = bcq.CheckDelCondition(Id, BaseCommType.BASE_AREA);
                        if (msg.ToUpper().Equals("OK"))
                        {
                            con.Delete(Id);	//执行动作 
                            con.Save();
                            count++;
                        }
                        else
                        {
                            this.Alert(msg);
                            break;
                        }
                    }
                }
            }
            if (count > 0)
            {
                  this.Alert(Resources.Lang.Common_SuccessDel); //删除成功!         
                this.GridBind();
            }
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
    public bool CheckData()
    {

        if (this.txtFromDate.Text.Trim().Length > 0)
        {
            if(StringExtension.IsDate(this.txtFromDate.Text) == false)
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
                    if (StringExtension.IsDate(this.txtEndDate.Text)== false)
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
    protected void grdBAS_AREA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBAS_AREAEdit.aspx?Flag=1&ids=" + strKeyID, SYSOperation.Modify, strKeyID), Resources.Lang.FrmBASE_AREAList_Msg02, "BAS_AREA", 800, 600);//储位区域
        }

    }
    #endregion   
}

