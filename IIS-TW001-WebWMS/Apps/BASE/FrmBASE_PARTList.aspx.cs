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
using DreamTek.WMS.DAL.Model.Base;
using DreamTek.WMS.Repository.Base;
using System.Globalization;
/// <summary>
/// 描述: 物料管理-->FrmBASE_PARTList 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-09-29 15:35:54
/// </summary>
/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/
public partial class BASE_FrmBASE_PARTList : WMSBasePage
{
    #region SQL
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
        }
    }


    #region IPageGrid 成员
    public void GridBind()
    {
        int total = 0;
        TW_BASE_PART whereObject = new TW_BASE_PART();
       
           

        if (txtPART.Text != string.Empty)
        {
            whereObject.PART = txtPART.Text.Trim();

        }
        if (txtRANK.Text != string.Empty)
        {
            whereObject.RANK_FINAL = txtRANK.Text.Trim();
        }

        if (txtCPARTNAME.Text != string.Empty)
        {
            whereObject.cpartname = txtCPARTNAME.Text.Trim();
        }
        //   caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpartname) && x.cpartname.Contains(txtCPARTNAME.Text.Trim()));
        if (txtCALIAS.Text != string.Empty)
        {
            whereObject.calias = txtCALIAS.Text.Trim();
        }
        if (txtCERPCODE.Text != string.Empty)
        {
            whereObject.cerpcode = txtCERPCODE.Text.Trim();
        }

        whereObject.bonded = TW_BASE_PART_Repository.IMPOSIBLE_DROP_DOWN_VAL;
        if (dpdBond.SelectedValue != "")
        {
            decimal val;
            if (Decimal.TryParse(dpdBond.SelectedValue, out val))
                whereObject.bonded = val;
            else
                whereObject.bonded = TW_BASE_PART_Repo.IMPOSIBLE_DROP_DOWN_VAL;
         }

        if (ddlCTYPE.SelectedValue != "")
        {
           
            whereObject.ctype = ddlCTYPE.SelectedValue;
           
        }

        // NOTE: by Mark, 09/15, 強制先給定一個 IMPOSIBLE_DROP_DOWN_VAL, 是因為在這裡 '全部', 沒有設值
        whereObject.mtype = TW_BASE_PART_Repo.IMPOSIBLE_DROP_DOWN_VAL;
        if (dpdABCType.SelectedValue != "")
        {
            decimal val;
            if (Decimal.TryParse(dpdABCType.SelectedValue, out val))
                whereObject.mtype = val;
            //else
            //    whereObject.mtype = TW_BASE_PART_Repo.IMPOSIBLE_DROP_DOWN_VAL;
         }

        whereObject.ineedwarn = TW_BASE_PART_Repo.IMPOSIBLE_DROP_DOWN_VAL;
        if (ddlINEEDWARN.SelectedValue != "")
        {
           // whereObject.ineedwarn = ddlINEEDWARN.SelectedValue
            decimal val;
            if (Decimal.TryParse(ddlINEEDWARN.SelectedValue, out val))
                whereObject.ineedwarn = val;
           
                

        }
        //是否 設置儲位 SZCW：
        if (ddlSZCW.SelectedValue != "")
        {
            whereObject.cdefaultcargo = ddlSZCW.SelectedValue;
        }

        //狀態：
        if (ddlCSTATUS.SelectedValue != "")
        {
            whereObject.cstatus = ddlCSTATUS.SelectedValue;
        }
        //    caseList = caseList.Where(x => x.cstatus.ToString().Equals(ddlCSTATUS.SelectedValue));


        //是否免檢：
        whereObject.ineedcheck = TW_BASE_PART_Repo.IMPOSIBLE_DROP_DOWN_VAL;
        if (ddlINEEDCHECK.SelectedValue != "")
        {
         //   whereObject.ineedcheck = ddlINEEDCHECK.SelectedValue;
            decimal val;
            if (Decimal.TryParse(ddlINEEDCHECK.SelectedValue, out val))
            {
                if (val == 1m) // NOTE by Mark, 09/15, 13:52 這是由現有數據看, 只看到 1 和 NULL
                    
                    whereObject.ineedcheck = val;
            }
              
        }

        //創建人：
        if (txtCreateuser.Text != string.Empty)
        {
            whereObject.createowner = txtCreateuser.Text.Trim();
        }

        //創建時間：	from  System.DateTime
        if (!string.IsNullOrEmpty(txtCreateTimeFrom.Text.Trim()))
        {
            //var cultureInfo = new CultureInfo("zh-CN");
            //var txt1 = txtCreateTimeFrom.Text.Trim();
            //whereObject.createtimeFROM = DateTime.Parse(txtCreateTimeFrom.Text.Trim(),  cultureInfo);
            whereObject.TXTcreatetime = txtCreateTimeFrom.Text.Trim();

        }
         //   caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtCreateTimeFrom.Text.Trim(), x.createtime) >= 0);


        //  caseList = caseList.Where(x => x.ineedcheck.ToString().Equals(ddlINEEDCHECK.SelectedValue));

        //if (ddlSZCW.SelectedValue == "1")
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cdefaultcargo));

        //    whereObject.cpositioncode = txtCPocitionCode.Text.Trim();

        IEnumerable<TW_BASE_PART> caseList = new TW_BASE_PART_Repo().Query(whereObject, PageSize, CurrendIndex, out total);
        AspNetPager1.RecordCount = total;
        AspNetPager1.PageSize = this.PageSize;

        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("BONDED", "YesOrNo"));//是否保税
        flagList.Add(new Tuple<string, string>("CTYPE", "BASE_PART.CTYPE"));//类型
        flagList.Add(new Tuple<string, string>("MTYPE", "ABCType"));//类别
        flagList.Add(new Tuple<string, string>("CSTATUS", "BASE_PART.CSTATUS"));//类别
        flagList.Add(new Tuple<string, string>("NeedSerial", "TrueOrFalse"));//是否管控序列号

        var srcdata = GetGridSourceDataByList(caseList.ToList(), flagList);

        grdBASE_PART.DataSource = srcdata;
        grdBASE_PART.DataBind();
    }
   
    public void XXXGridBind()
    {
     
        IGenericRepository<BASE_PART> entity = new GenericRepository<BASE_PART>(context);
        var caseList = from p in entity.Get()
                       orderby p.createtime descending
                       where 1 == 1
                       select p;

        if (!string.IsNullOrEmpty(txtCreateTimeFrom.Text.Trim()))
            caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtCreateTimeFrom.Text.Trim(), x.createtime) >= 0);
        if (txtCreateTimeTo.Text != string.Empty)
            caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtCreateTimeTo.Text.Trim(), x.createtime) <= 0);
        if (!string.IsNullOrEmpty(txtDEXPIREDATEFrom.Text.Trim()))
            caseList = caseList.Where(x => x.dexpiredate != null && SqlFunctions.DateDiff("dd", txtDEXPIREDATEFrom.Text.Trim(), x.dexpiredate) >= 0);
        if (txtDEXPIREDATETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dexpiredate != null && SqlFunctions.DateDiff("dd", txtDEXPIREDATETo.Text.Trim(), x.dexpiredate) <= 0);

        if (txtCreateuser.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.createowner) && x.createowner.Contains(txtCreateuser.Text.Trim()));
        //if (txtCPARTNUMBER.Text != string.Empty)
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpartnumber) && x.cpartnumber.Contains(txtCPARTNUMBER.Text.Trim()));

        if (txtCPARTNAME.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpartname) && x.cpartname.Contains(txtCPARTNAME.Text.Trim()));

        if (txtCALIAS.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.calias) && x.calias.Contains(txtCALIAS.Text.Trim()));
        if (txtCERPCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtCERPCODE.Text.Trim()));

        if (txtCALIAS.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.calias) && x.calias.Contains(txtCALIAS.Text.Trim()));

        if (txtCERPCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode.ToString()) && x.cerpcode.ToString().Contains(txtCERPCODE.Text.Trim()));



        if (txtCVERSION.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cversion.ToString()) && x.cversion.ToString().Contains(txtCVERSION.Text.Trim()));
        if (txtCUNITS.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cunits.ToString()) && x.cunits.ToString().Contains(txtCUNITS.Text.Trim()));
        if (txtCUSETYPE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cusetype.ToString()) && x.cusetype.ToString().Contains(txtCUSETYPE.Text.Trim()));
        if (dpdBond.SelectedValue != "")
            caseList = caseList.Where(x => x.bonded.ToString().Equals(dpdBond.SelectedValue));

        if (dpdBond.SelectedValue != "")
            caseList = caseList.Where(x => x.bonded.ToString().Equals(dpdBond.SelectedValue));
        if (dpdABCType.SelectedValue != "")
            caseList = caseList.Where(x => x.mtype.ToString().Equals(dpdABCType.SelectedValue));
        if (ddlCTYPE.SelectedValue != "")
            caseList = caseList.Where(x => x.ctype.ToString().Equals(ddlCTYPE.SelectedValue));
        if (ddlINEEDWARN.SelectedValue != "")
            caseList = caseList.Where(x => x.ineedwarn.ToString().Equals(ddlINEEDWARN.SelectedValue));
        if (ddlCSTATUS.SelectedValue != "")
            caseList = caseList.Where(x => x.cstatus.ToString().Equals(ddlCSTATUS.SelectedValue));
        if (ddlINEEDCHECK.SelectedValue != "")
            caseList = caseList.Where(x => x.ineedcheck.ToString().Equals(ddlINEEDCHECK.SelectedValue));
        if (ddlSZCW.SelectedValue != "")
        {
            if (ddlSZCW.SelectedValue == "1")
                caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cdefaultcargo));
            else
                caseList = caseList.Where(x => string.IsNullOrEmpty(x.cdefaultcargo));
        }
        if (!string.IsNullOrEmpty(drpNeedSerial.SelectedValue)) {
            int isneed = Convert.ToInt32(drpNeedSerial.SelectedValue);
            caseList = caseList.Where(x => x.NeedSerial.Value == isneed);
        }
        if (txtcspec.Text != string.Empty)
        {          
            caseList = caseList.Where(x => x.cspecifications.Contains(txtcspec.Text.Trim()));
        }


        //for (int i = 0; i < caseList.ToList().Count; i++)
        //{
        //    if (caseList.ToList()[i].cpartnumber.ToString().Length > 20)
        //    {
        //        string v = caseList.ToList()[i].cpartnumber.Substring(0, 20) + "...";
        //        caseList.ToList()[i].cpartnumber = v;
        //    }
        //    if (caseList.ToList()[i].cpartname.ToString().Length > 20)
        //    {
        //        string v = caseList.ToList()[i].cpartname.Substring(0, 20) + "...";
        //        caseList.ToList()[i].cpartname = v;
        //    }


        //}


        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        AspNetPager1.RecordCount = caseList.Count();
        AspNetPager1.PageSize = this.PageSize;

        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";
        var data = GetPageSize(caseList, PageSize, CurrendIndex).ToList();

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("BONDED", "YesOrNo"));//是否保税
        flagList.Add(new Tuple<string, string>("CTYPE", "BASE_PART.CTYPE"));//类型
        flagList.Add(new Tuple<string, string>("MTYPE", "ABCType"));//类别
        flagList.Add(new Tuple<string, string>("CSTATUS", "BASE_PART.CSTATUS"));//类别
        flagList.Add(new Tuple<string, string>("NeedSerial", "TrueOrFalse"));//是否管控序列号
        
        var srcdata = GetGridSourceDataByList(data, flagList);

        grdBASE_PART.DataSource = srcdata;
        grdBASE_PART.DataBind();
    }

    public bool CheckData()
    {
        //if (this.txtCPARTNUMBER.Text.Trim().Length > 0)
        //{
        //}

        if (this.txtPART.Text.Trim().Length > 0)
        {
        }


        if (this.txtCPARTNAME.Text.Trim().Length > 0)
        {
        }
        if (this.txtCALIAS.Text.Trim().Length > 0)
        {
        }
        if (this.txtCERPCODE.Text.Trim().Length > 0)
        {
        }
        if (this.txtCVERSION.Text.Trim().Length > 0)
        {
        }
        if (this.txtCUNITS.Text.Trim().Length > 0)
        {
        }
        if (this.txtDEXPIREDATEFrom.Text.Trim().Length > 0)
        {
            if (this.txtDEXPIREDATEFrom.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmBASE_PARTList_Msg01);//终止日期项不是有效的日期！
                this.SetFocus(txtDEXPIREDATEFrom);
                return false;
            }
        }
        if (this.txtDEXPIREDATETo.Text.Trim() == "")
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_ToItemEmpty);//到项不允许空！
            this.SetFocus(txtDEXPIREDATETo);
            return false;
        }
        if (this.txtDEXPIREDATETo.Text.Trim().Length > 0)
        {
            if (this.txtDEXPIREDATETo.Text.IsDate() == false)
            {
                this.Alert(Resources.Lang.FrmALLOCATEList_NoEffToItem);//到项不是有效的日期！
                this.SetFocus(txtDEXPIREDATETo);
                return false;
            }
        }
        if (this.txtCUSETYPE.Text.Trim().Length > 0)
        {
        }
        return true;

    }

    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        this.grdBASE_PART.DataKeyNames = new string[] { "ID" };
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWin('" + WMSBasePage.BuildRequestPageURL("FrmBASE_PARTEdit.aspx?Flag=0", SYSOperation.New, "") + "','" + Resources.Lang.FrmBASE_PARTList_Msg02 + "','BASE_PART',800,600);return false;"; //新建物料管理

        //在新窗口打开的代码： this.OpenWin(btnNew,PageBase.BuildRequestPageURL("FrmBASE_PARTEdit.aspx", SysOperation.New,""),800,600);        

        //初始化下拉框
        Help.DropDownListDataBind(GetParametersByFlagType("YesOrNo"), dpdBond, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//是否保税
        Help.DropDownListDataBind(GetParametersByFlagType("TrueOrFalse"), ddlINEEDWARN, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//是否预警
        Help.DropDownListDataBind(GetParametersByFlagType("TrueOrFalse"), ddlSZCW, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//是否设置储位
        Help.DropDownListDataBind(GetParametersByFlagType("TrueOrFalse"), ddlINEEDCHECK, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//是否免检
        Help.DropDownListDataBind(GetParametersByFlagType("BASE_PART.CTYPE"), ddlCTYPE, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//类型
        Help.DropDownListDataBind(GetParametersByFlagType("ABCType"), dpdABCType, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//类别
        Help.DropDownListDataBind(GetParametersByFlagType("BASE_PART.CSTATUS"), ddlCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(GetParametersByFlagType("TrueOrFalse"), drpNeedSerial, "请选择", "FLAG_NAME", "FLAG_ID", "");//是否序列号管控
    }

    #endregion





    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrendIndex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        this.GridBind();
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorBASE_PART
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_PART> con = new GenericRepository<BASE_PART>(context);
        BaseCommQuery bcq = new BaseCommQuery();
        int count = 0;
        try
        {
            for (int i = 0; i < this.grdBASE_PART.Rows.Count; i++)
            {
                if (this.grdBASE_PART.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_PART.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {

                        string ID = this.grdBASE_PART.DataKeys[i].Values[0].ToString();
                        var msg = bcq.CheckDelCondition(ID, BaseCommType.BASE_PART);
                        if (msg.ToUpper().Equals("OK"))
                        {
                            con.Delete(ID);
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
        for (int i = 0; i < this.grdBASE_PART.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBASE_PART.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return strKeyId;
    }

    protected void grdBASE_PART_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = this.GetKeyIDS(e.Row.RowIndex);
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
            linkModify.NavigateUrl = "#";

            this.OpenFloatWin(linkModify, BuildRequestPageURL("FrmBASE_PARTEdit.aspx?Flag=1&ids=" + strKeyID, SYSOperation.Modify, strKeyID), "", "BASE_PART", 800, 600);
            HyperLink linkModify1 = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            string lh = e.Row.Cells[1].Text;
            linkModify1.NavigateUrl = "#";
            this.OpenFloatWin(linkModify1, BuildRequestPageURL("FrmBASE_WL_AREA.aspx?Flag=1&ids=" + strKeyID, SYSOperation.Modify, strKeyID), "", "BASE_SELECTCARGOSPACE", 800, 600);


            //设置要换行的模板列 
            e.Row.Cells[2].Text = Server.HtmlDecode(e.Row.Cells[2].Text);
            e.Row.Cells[2].Style.Add("word-break", "break-all");
            e.Row.Cells[2].Text = AddBR(e.Row.Cells[2].Text, 30);
            e.Row.Cells[28].Text = OPERATOR.GetUserNameByAccountID(e.Row.Cells[28].Text); 
        }
    }
    #endregion

}

