using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DreamTek.ASRS.DAL;
using DreamTek.ASRS.Repository;

public partial class Apps_BASE_FrmBase_CraneConfigEdit_Trade : WMSBasePage
{
    DBContext context = new DBContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack == false)
        {
            this.InitPage();
            if (this.Operation() == SYSOperation.New)
            {
                GridBind();
            }
        }
    }
    public void InitPage()
    {
        this.cssUrl.Attributes["Href"] = "../../Layout/CSS/" + WmsWebUserInfo.GetCurrentUser().CSS_Name + "/Page.css";
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch1'].click(); CloseMySelf('BASE_CraneDetialTrade');return false;";
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < this.grdBASE_trande.Rows.Count; i++)
            {
                if (this.grdBASE_trande.Rows[i].Cells[0].Controls[1] is CheckBox)
                {
                    CheckBox chkSelect = (CheckBox)this.grdBASE_trande.Rows[i].Cells[0].Controls[1];
                    if (chkSelect.Checked)
                    {
                        IGenericRepository<BASE_CRANECONFIG_TRADETYPE> con = new GenericRepository<BASE_CRANECONFIG_TRADETYPE>(context);
                        IGenericRepository<V_InOrOutType> cons = new GenericRepository<V_InOrOutType>(context);
                        string id = this.grdBASE_trande.DataKeys[i].Values[0].ToString();
                        var caseList = from p in cons.Get()
                                       where p.typeid == id
                                       select p;
                        V_InOrOutType entity_v = caseList.ToList().FirstOrDefault<V_InOrOutType>();
                        BASE_CRANECONFIG_TRADETYPE entity = new BASE_CRANECONFIG_TRADETYPE();
                        entity.ID = this.KeyID;
                        entity.IDS = Guid.NewGuid().ToString();
                        entity.TYPEID = id;
                        entity.TYPENAME = entity_v.typename;
                        entity.INOROUTNAME = entity_v.T == "I" ? Resources.Lang.FrmBase_CraneConfigEdit_D_Msg15 : Resources.Lang.FrmBase_CraneConfigEdit_D_Msg16;//"入库" : "出库";
                        entity.INOROUTCODE = entity_v.T;
                        entity.CSTATUS = entity_v.enable;
                        entity.CERPCODE = entity_v.cerpcode;
                        con.Insert(entity);
                        con.Save();
                    }
                }
            }
            this.Alert(Resources.Lang.FrmBaseCONFIG_DEdit_Msg08); //新增成功！
            this.GridBind();
        }
        catch (Exception E)
        {
            this.Alert(Resources.Lang.FrmALLOCATEList_DelFail + E.Message); //删除失败！this.Alert("删除失败！" + E.Message);
        }
    }

    public void GridBind()
    {
        IGenericRepository<V_InOrOutType> con = new GenericRepository<V_InOrOutType>(context);
        IGenericRepository<BASE_CRANECONFIG_TRADETYPE> cons = new GenericRepository<BASE_CRANECONFIG_TRADETYPE>(context);
        var caseList = from p in con.Get().AsEnumerable()
                       where !(from t in cons.Get().AsEnumerable()
                               where t.ID == this.KeyID
                               select t.CERPCODE).Contains(p.cerpcode)
                       select p;
        if (caseList != null && caseList.Count() > 0)
        {
            AspNetPager1.RecordCount = caseList.Count();
            AspNetPager1.PageSize = this.PageSize;
        }
        AspNetPager1.CustomInfoHTML = Resources.Lang.FrmALLOCATEList_TotalPages + " :<b>" + "</b>";  //" 总页数:<b>" + "</b>";
        grdBASE_trande.DataSource = GetPageSize(caseList.AsQueryable(), PageSize, CurrendIndex).ToList();
        grdBASE_trande.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager1.CurrentPageIndex;//索引同步
        GridBind();
    }

    protected void grdBASE_trande_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[5].Text)
            {
                case "0":
                    e.Row.Cells[5].Text = Resources.Lang.FrmBase_CraneConfig_Msg13; //使用中
                    break;
                case "1":
                    e.Row.Cells[5].Text = Resources.Lang.FrmBase_InOutTypeStatusList_btnUnable; //作废
                    break;
                default:
                    break;
            }
            //类型分类
            switch (e.Row.Cells[2].Text)
            {
                case "O":
                    e.Row.Cells[2].Text = Resources.Lang.FrmBase_CraneConfigEdit_D_Msg16; //出库
                    break;
                case "I":
                    e.Row.Cells[2].Text = Resources.Lang.FrmBase_CraneConfigEdit_D_Msg15; //入库
                    break;
                default:
                    break;
            }
        }
    }
}