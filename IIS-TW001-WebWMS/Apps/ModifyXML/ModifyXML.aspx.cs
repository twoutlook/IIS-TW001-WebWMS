using DreamTek.WMS.DAL.Common;
using DreamTek.WMS.DAL.Model.Base;
using DreamTek.WMS.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_ModifyXML_ModifyXML : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request["id"].ToString();
            Init();
            if (this.Operation() != SYSOperation.New)
            {
                ShowData(id);
                GridBind();
            }
        }
    }

    public void Init()
    {
        Help.DropDownListDataBind(new SysParameterList().LoadStatusByFlag_type("Language"), ddlLang, "全部", "flag_name", "flag_id", "");
        Help.DropDownListDataBind(new SysParameterList().LoadStatusByFlag_type("BASE_CLIENT"), ddlStatus, "全部", "flag_name", "flag_id", "");
        this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('ResourceEdit');return false;";
        if (this.Operation() == SYSOperation.New)
        {
            txtKey.Enabled = true;
            ddlLang.Enabled = true;
            ddlModel.Enabled = true;
            ddlStatus.Enabled = true;
        }
    }

    public void ShowData(string id)
    {
        Base_ResourcesRepository brr = new Base_ResourcesRepository();
        Base_Resources bo = brr.GetById(id);
        txtId.Text = bo.ID;
        txtKey.Text = bo.SourceKey;
        txtValue.Text = bo.SourceValue;
        txtReamrk.Text = bo.Remark;
        ddlLang.SelectedValue = bo.LanguageId;
        ddlModel.SelectedValue = bo.ModuleId;
        ddlStatus.SelectedValue = bo.CStatus;

        //if (bo.CreateTime.HasValue)
        //{
        //    txtCreateTime.Text = bo.CreateTime.Value.ToString();
        //}
        //if (bo.Modifytime.HasValue)
        //{
        //    txtModifyTime.Text = bo.Modifytime.Value.ToString();
        //}

        if (bo.CreateTime != null)
        {
            txtCreateTime.Text = bo.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        if (bo.Modifytime != null)
        {
            txtModifyTime.Text = bo.Modifytime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        txtCreateUser.Text = bo.CreateUser;
        txtModifyUser.Text = bo.ModifyUser;

        

    }

    public void GridBind()
    {
        Base_ResourcesRepository brr = new Base_ResourcesRepository();
        Base_ResourcesQuery bq = new Base_ResourcesQuery();
        bq.LanguageId = ddlLang.SelectedValue;
        bq.SourceKey = txtKey.Text.Trim();
        int total = 0;
        var list = brr.QueryDetail(bq, PageSize, CurrendIndex, out total);
        AspNetPager2.RecordCount = total;
        AspNetPager2.PageSize = this.PageSize;
        grdDetail.DataSource = list;
        grdDetail.DataBind();
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        this.CurrendIndex = AspNetPager2.CurrentPageIndex;//索引同步
        GridBind();
    }
    public Base_Resources SendData()
    {
        Base_Resources bo = new Base_Resources();
        if (this.Operation() == SYSOperation.Modify)
        {
            bo.ID = txtId.Text;
            bo.ModifyUser = WmsWebUserInfo.GetCurrentUser().UserNo;
            bo.Modifytime = DateTime.Now;
            bo.CreateTime = txtCreateTime.Text.ToDateTime();
            bo.CreateUser = txtCreateUser.Text;
        }
        else if (this.Operation() == SYSOperation.New)
        {
            bo.ID = Guid.NewGuid().ToString();
            bo.CreateTime = DateTime.Now;
            bo.CreateUser = WmsWebUserInfo.GetCurrentUser().UserNo;
            bo.Modifytime = DateTime.Now;
            bo.CreateUser = WmsWebUserInfo.GetCurrentUser().UserNo;
        }
        bo.SourceKey = txtKey.Text.Trim();
        bo.SourceValue = txtValue.Text.Trim();
        bo.Remark = txtReamrk.Text.Trim();
        bo.LanguageId = ddlLang.SelectedValue;
        bo.ModuleId = ddlModel.SelectedValue;
        bo.CStatus = ddlStatus.SelectedValue;
        return bo;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Base_Resources bo = SendData();
        if (this.Operation() == SYSOperation.Modify)
        {
            Base_ResourcesRepository brr = new Base_ResourcesRepository();
            brr.Update(bo);
        }
        else if (this.Operation() == SYSOperation.New)
        {
            SqlRepository<Base_Resources> db = new SqlRepository<Base_Resources>();
            var obj = db.Insert(bo);
        }
        this.Alert("保存成功");
        string id = bo.ID;
        Response.Redirect("ModifyXML.aspx?" + BuildQueryString(SYSOperation.Modify, id));
    }
}