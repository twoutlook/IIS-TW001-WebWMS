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
/// 描述: 储位管理-->FrmBASE_CARGOSPACEList 页面后台类文件
/// 作者: --陈建华
/// 创建于: 2012-10-09 18:29:23
/// </summary>
/*
Roger
解决导出问题
2013/6/5 10:36:58
20130605103658
*/
public partial class BASE_FrmBASE_CARGOSPACEList :WMSBasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ucShowArea.SetCompName = txtCDEFINE1.ClientID;
        ucShowArea.SetORGCode = txtAreaID.ClientID;
        if (Page.IsPostBack == false)
        {
            this.InitPage();
            this.btnSearch_Click(this.btnSearch, EventArgs.Empty);
           
        }
        btnSearch.DataBind();
        //btnNew.Attributes.Add("onclick", this.GetPostBackEventReference(btnNew) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
    }

    #region IPageGrid 成员

    public void GridBind()
    {
        IGenericRepository<V_BASE_CARGOSPACE_L> con = new GenericRepository<V_BASE_CARGOSPACE_L>(context);
        var caseList = from p in con.Get()
                       orderby p.createtime descending
                       where 1 == 1
                       select p;
        if (ddlLineID.SelectedValue != "-1" && ddlLineID.SelectedValue != "")//线别
            caseList = caseList.Where(x => x.lineid.ToString().Equals(ddlLineID.SelectedValue));
        if (!string.IsNullOrEmpty(ddlStock.SelectedValue))//是否有货
            caseList = caseList.Where(x => x.vid.ToString().Equals(ddlStock.SelectedValue));
        if (!string.IsNullOrEmpty(ddlPalletcode.SelectedValue))//是否存在栈板
            caseList = caseList.Where(x => x.apallet_code.ToString().Equals(ddlPalletcode.SelectedValue));
        if (txtCreateTimeFrom.Text != string.Empty)//创建时间从
            caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtCreateTimeFrom.Text.Trim(),x.createtime) >= 0  );
        if (txtCreateTimeTo.Text != string.Empty)//时间到
            caseList = caseList.Where(x => x.createtime != null && SqlFunctions.DateDiff("dd", txtCreateTimeTo.Text.Trim(),x.createtime) <= 0 );
        if (GetAreaId() != string.Empty)//所属区域
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.acdefine1) && x.acdefine1.Contains(txtAreaID.Text.Trim()));//GetAreaId()));
        if(txtCCARGOID.Text!=string.Empty)//编号
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cpositioncode) && x.cpositioncode.Contains(txtCCARGOID.Text.Trim()));
        if (txtCCARGONAME.Text != string.Empty)//名称
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cposition) && x.cposition.Contains(txtCCARGONAME.Text.Trim()));
        if (ddlCSTATUS.SelectedValue != "")//状态
            caseList = caseList.Where(x => x.cstatus.ToString().Equals(ddlCSTATUS.SelectedValue));
        if (txtCALIAS.Text != string.Empty)//助记码
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.calias) && x.calias.Contains(txtCALIAS.Text.Trim()));
        //ERP编码
        if (txtCERPCODE.Text != string.Empty)
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cerpcode) && x.cerpcode.Contains(txtCERPCODE.Text.Trim()));
        ////种类
        if (!string.IsNullOrEmpty(DropCTYPE.SelectedValue))
            caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ctype) && x.ctype.Equals(DropCTYPE.SelectedValue));
        //if (txtCTYPE.Text != string.Empty)
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.ctype) && x.ctype.Contains(txtCTYPE.Text.Trim()));
        //终止日期
        if (txtDEXPIREDATEFrom.Text != string.Empty)
            caseList = caseList.Where(x => x.dexpiredate != null && SqlFunctions.DateDiff("dd", txtDEXPIREDATEFrom.Text.Trim(),x.dexpiredate) >= 0 );
        if (txtDEXPIREDATETo.Text != string.Empty)
            caseList = caseList.Where(x => x.dexpiredate != null && SqlFunctions.DateDiff("dd", txtDEXPIREDATETo.Text.Trim(),x.dexpiredate) <= 0 );
        //是否允许混放
        if (ddlIPERMITMIX.SelectedValue != "")
            caseList = caseList.Where(x => x.ipermitmix.ToString().Equals(ddlIPERMITMIX.SelectedValue));
        //所属仓库
        //if (txtWareHouse.Text != string.Empty)
        //    caseList = caseList.Where(x => !string.IsNullOrEmpty(x.cwarename) && x.cwarename.Contains(txtWareHouse.Text.Trim()));
        if (ddlWareHouse.SelectedValue != "")
            caseList = caseList.Where(x => x.warehouseid.Contains(ddlWareHouse.SelectedValue.Trim()));
        //是否设置了最大量
        if (ddlZDL.SelectedValue != "")
            caseList = caseList.Where(x => x.zdl.ToString().Equals(ddlZDL.SelectedValue));
        //是否设置了区域
        if (ddlQY.SelectedValue != "")
            caseList = caseList.Where(x => x.qy.ToString().Equals(ddlQY.SelectedValue));

        if (caseList != null)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }

        var data =  GetPageSize(caseList.AsQueryable(), PageSize, CurrendIndex).ToList();

        List<Tuple<string, string>> flagList = new List<Tuple<string, string>>();
        flagList.Add(new Tuple<string, string>("cstatus", "C"));//状态
        flagList.Add(new Tuple<string, string>("is_allo", "BASE_CARGOSPACE.IS_ALLO"));//是否允许调拨
        flagList.Add(new Tuple<string, string>("ipermitmix", "TrueOrFalse"));//是否允许混放
        

        var srcdata = GetGridSourceDataByList(data, flagList);

        this.grdBASE_CARGOSPACE.DataSource = srcdata;
        this.grdBASE_CARGOSPACE.DataBind();
    }
    #endregion

    #region IPage 成员

    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/page.css";
        this.titleImg.Attributes["src"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Images/Login/icon1.gif";
        #region Disable and ReadOnly
        #endregion Disable and ReadOnly
        //本页面打开新增窗口
        this.btnNew.Attributes["onclick"] = "PopupFloatWinMax('" + WMSBasePage.BuildRequestPageURL("FrmBASE_CARGOSPACEEdit.aspx?Flag=0", SYSOperation.New, "") + "','" + Resources.Lang.FrmBASE_CARGOSPACEList_Mag03+ "','BASE_CARGOSPACE',850,470);return false;";//新建储位管理
      
        txtCDEFINE1.Attributes["onclick"] = "Show('" + ucShowArea.GetDivName + "');";


        Help.DropDownListDataBind(GetLineID(), this.ddlLineID, Resources.Lang.Common_ALL, "FUNCNAME", "EXTEND1", "");
        Help.DropDownListDataBind(SysParameterList.GetList("", "", "CARGOSPACETYPE", false, -1, -1), this.DropCTYPE, Resources.Lang.Common_ALL, "FLAG_NAME", "FLAG_ID", ""); //全部

        Help.DropDownListDataBind(GetWareHouse(), this.ddlWareHouse, Resources.Lang.Common_ALL, "cwarename", "id", ""); //全部

        Help.DropDownListDataBind(GetParametersByFlagType("TrueOrFalse"), ddlIPERMITMIX, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//是否允许混放
        Help.DropDownListDataBind(GetParametersByFlagType("C"), ddlCSTATUS, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//状态
        Help.DropDownListDataBind(GetParametersByFlagType("IsSetOrNotSet"), ddlZDL, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//是否已设置最大量
        Help.DropDownListDataBind(GetParametersByFlagType("IsSetOrNotSet"), ddlQY, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//是否已设置区域
        Help.DropDownListDataBind(GetParametersByFlagType("AGVSITE"), ddlPalletcode, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//是否已设置区域
        Help.DropDownListDataBind(GetParametersByFlagType("TrueOrFalse"), ddlStock, Resources.Lang.WMS_Common_DrpAll, "FLAG_NAME", "FLAG_ID", "");//是否有货
        
        var list = GetParametersByFlagType("C");
        list.Remove(list.Where(x => x.FLAG_ID == "1").FirstOrDefault());
        Help.DropDownListDataBind(list, ddlStatus, "", "FLAG_NAME", "FLAG_ID", "");//状态
        
    }
    #endregion
    public DataTable GetLineID()
    {
        string sql = @"select distinct t.CRANENAME as FUNCNAME,t.CRANEID as EXTEND1  from  BASE_CRANECONFIG t  order by t.CRANEID asc ";

        return DBHelp.ExecuteToDataTable(sql);
    }
    public DataTable GetWareHouse()
    {
        string sql = @"SELECT id,cwareid,cwarename FROM dbo.BASE_WAREHOUSE WITH(NOLOCK)  ORDER BY cwareid DESC";
        return DBHelp.ExecuteToDataTable(sql);
    }
    private string GetAreaId()
    {
        string areaId = string.Empty;
        IGenericRepository<BASE_AREA> con = new GenericRepository<BASE_AREA>(context);
        var caseList = from p in con.Get()
                       where p.area_name == txtCDEFINE1.Text
                       select p.id;
        if (caseList != null && caseList.Count() > 0)
            areaId = caseList.ToList()[0];
        return areaId;   
    }
    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //grdNavigatorBASE_CARGOSPACE
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CARGOSPACE> con = new GenericRepository<BASE_CARGOSPACE>(context);
        BaseCommQuery bcq = new BaseCommQuery();
        int count = 0;
        try
        {
            for (int i = 0; i < this.grdBASE_CARGOSPACE.Rows.Count; i++)
            {
                if (this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        string ids = this.grdBASE_CARGOSPACE.DataKeys[i].Values[0].ToString();
                        var msg = bcq.CheckDelCondition(ids, BaseCommType.BASE_CARGOSPACE);
                        if (msg.ToUpper().Equals("OK"))
                        {
                            con.Delete(ids);
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
        for (int i = 0; i < this.grdBASE_CARGOSPACE.DataKeyNames.Length; i++)
        {
            strKeyId += this.grdBASE_CARGOSPACE.DataKeys[rowIndex].Values[i].ToString() + ",";
        }
        strKeyId = strKeyId.TrimEnd(',');
        return this.grdBASE_CARGOSPACE.DataKeys[rowIndex].Values[0].ToString();
    }

    protected void grdBASE_CARGOSPACE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strKeyID = grdBASE_CARGOSPACE.DataKeys[e.Row.RowIndex].Values[0].ToString();
            HyperLink linkModify = (HyperLink)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            linkModify.NavigateUrl = "#";
            this.OpenFloatWinMax(linkModify, BuildRequestPageURL("FrmBASE_CARGOSPACEEdit.aspx?Flag=1", SYSOperation.Modify, strKeyID), Resources.Lang.FrmBASE_CARGOSPACEList_Title01, "BASE_CARGOSPACE"); //储位管理
     
            //hlToIOCCUPYQTY_Info
            HyperLink hlToIOCCUPYQTY_Info = (HyperLink)e.Row.FindControl("hlToIOCCUPYQTY_Info");
            hlToIOCCUPYQTY_Info.NavigateUrl = "#";
            decimal IoccupyQty = 0;
            var ss = this.grdBASE_CARGOSPACE.DataKeys[e.Row.RowIndex].Values[1];
            if (ss != DBNull.Value)
            {
                IoccupyQty = Convert.ToDecimal(this.grdBASE_CARGOSPACE.DataKeys[e.Row.RowIndex].Values[1]);
            }

            if (IoccupyQty > 0)
            {
                //this.OpenFloatWin(hlToIOCCUPYQTY_Info, BuildRequestPageURL("FrmINASSITListByPositionCode.aspx", SYSOperation.Modify, e.Row.Cells[2].Text.Trim()), Resources.Lang.FrmBASE_CARGOSPACEList_Mag04, "INASSITListByPositionCode", 600, 470); //上架指引占用详情
            }
        }
    }

    protected void dsGrdBASE_CARGOSPACE_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.IsPostBack == false)
        {
            e.Cancel = true;
        }
    }

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        //if (BASE_CARGOSPACERule.UpdateStatus_New())
        //{
        //    this.btnSearch_Click(sender, e);
        //    Alert("释放成功");
        //}
        //else
        //{
        //    Alert("释放失败");
        //}
    }
    protected void btnUpStatus_Click(object sender, EventArgs e)
    {
        IGenericRepository<BASE_CARGOSPACE> con = new GenericRepository<BASE_CARGOSPACE>(context);
        IGenericRepository<BASE_AREA> areacon = new GenericRepository<BASE_AREA>(context);
        IGenericRepository<STOCK_CURRENT> stock = new GenericRepository<STOCK_CURRENT>(context);

        try
        {
            for (int i = 0; i < this.grdBASE_CARGOSPACE.Rows.Count; i++)
            {
                if (this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_CARGOSPACE.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        
                        BASE_CARGOSPACE entity = SendData(this.grdBASE_CARGOSPACE.DataKeys[i].Values[0].ToString());
                        //如果存在截止日期则不可更改 CQ 2014-6-17 13:38:48
                        //if (entity.dexpiredate.ToString().Trim() != "")
                        //{
                        //    throw new Exception("储位["+entity.cpositioncode+"]存在终止日期,不能修改状态");
                        //}

                        //如果区域中已经用了该储位则不可更改 CQ 2014-6-17 13:38:48


                        var stockCurrent = stock.Get().Where(p=>p.cpositioncode==entity.cpositioncode);

                        if (stockCurrent.ToList().Count > 0)
                        {
                            throw new Exception(Resources.Lang.FrmBASE_CARGOSPACEList_Mag05 + "[" + entity.cpositioncode + "]" + Resources.Lang.FrmBASE_CARGOSPACEList_Mag06);//"储位[" + entity.cpositioncode + "]在库存管理中已使用,不能修改状态"
                        }

                        var cl = areacon.Get().Where(p => p.handover_cargo == entity.cpositioncode);

                        if (cl.ToList().Count>0)
                        {
                            throw new Exception(Resources.Lang.FrmBASE_CARGOSPACEList_Mag05 + "[" + entity.cpositioncode + "]" + Resources.Lang.FrmBASE_CARGOSPACEList_Mag07);//储位[" + entity.cpositioncode + "]在区域管理中已使用,不能修改状态
                        }
                        //原先状态与目前状态不一致才修改，否则不修改
                        if (!entity.cstatus.Trim().Equals(ddlStatus.SelectedValue))
                        {
                            entity.lastcstatus = entity.cstatus;
                            entity.cstatus = ddlStatus.SelectedValue;
                            entity.lastupdatetime = DateTime.Now;
                            entity.lastupdateowner = WmsWebUserInfo.GetCurrentUser().UserNo;
                        }
                        ;	//执行动作 
                        con.Update(entity);
                        con.Save();
                    }
                }
            }
            this.Alert(Resources.Lang.FrmBASE_CARGOSPACEList_Mag08); //修改状态成功！
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.FrmBASE_CARGOSPACEList_Mag09 + E.Message.ToJsString()); //("修改状态失败！" + E.Message.ToJsString());
        }
    }

    public BASE_CARGOSPACE SendData(string id)
    {
        IGenericRepository<BASE_CARGOSPACE> con = new GenericRepository<BASE_CARGOSPACE>(context);
        var caseList = from p in con.Get()
                       where p.id == id
                       select p;
        return caseList.ToList().FirstOrDefault<BASE_CARGOSPACE>();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
//        try
//        {
//            DataTable dt = DBUtil.Fill(@"select temp.cpositioncode,temp.warehouse_no,temp.area_name,temp.ipermitmix,temp.imaxcapacity
//                                   from temp_import_space_part_area temp
//                                   where temp.cpositioncode is not null
//                                   group by temp.cpositioncode,temp.warehouse_no,temp.area_name,temp.ipermitmix,temp.imaxcapacity");
//            foreach (DataRow item in dt.Rows)
//            {
//                //是否允许混放(0: 否 ， 1: 是)
//                string ipermitmixStr = item["ipermitmix"].ToString().Trim() == "Y" ? "1" : "0";
//                string updateSql = @"update base_cargospace b
//                           set b.warehouseid=(select bw.id from base_warehouse bw where bw.cwareid='" + item["warehouse_no"].ToString().Trim() + @"' and rownum=1),
//                               b.imaxcapacity='" + Convert.ToDecimal(item["imaxcapacity"].ToString().Trim()) + @"',
//                               b.CDEFINE1=(select ba.id from base_area ba where ba.area_name='" + item["area_name"].ToString().Trim() + @"' and rownum=1),
//                               b.ipermitmix='" + ipermitmixStr + @"'
//                           where b.cpositioncode='" + item["cpositioncode"].ToString().Trim() + @"'";
//                DBUtil.ExecuteNonQuery(updateSql);
//            }
//            Alert("成功");
//        }
//        catch (Exception ex)
//        {            
//            //throw ex.Message;
//            Alert(ex.Message);
//        }        
    }

    /// <summary>
    /// 显示占用量详情
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnToIOCCUPYQTY_Info_Click(object sender, EventArgs e)
    {

    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

}

 