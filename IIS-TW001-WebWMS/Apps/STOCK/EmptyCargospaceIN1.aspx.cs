using DreamTek.ASRS.Business.SP.ProcedureModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_STOCK_EmptyCargospaceIN1 : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtSub.Text = Request.QueryString["sub"];
        txtArea.Text = Request.QueryString["area"];
        txtCPOSITIONCODE.Text = Request.QueryString["locCode"];
        txtLocName.Text = Request.QueryString["locName"];

        if (!IsPostBack)
        {
            this.btnBack.Attributes["onclick"] = "window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click(); CloseMySelf('CargospaceIN');return false;";
            DataTable dt = Comm_Sys.GetLineList();//获取有几条线
            if (dt.Rows.Count > 1)
            {
                ddlLine.Items.Insert(0, new ListItem(Resources.Lang.WMS_Common_Element_QingXuanZe, "0"));//请选择
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    ddlLine.Items.Insert(i, new ListItem(i.ToString(), i.ToString()));
                }
            }
            else
            {
                ddlLine.DataSource = dt;
                ddlLine.DataTextField = "CRANEID";
                ddlLine.DataBind();
            }
            ddlLine_SelectedIndexChanged(sender, e);
        }
    }


    public bool CheckData()
    {
        if (txtCPOSITIONCODE.Text.Trim() == string.Empty)
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_NeedCposition);//请输入储位！
            return false;
        }

        if (this.ddlLine.SelectedValue.ToString() == "0")
        {
            this.Alert(Resources.Lang.EmptyCargospaceIN1_NeedSelectLine);//请选择线别！
            return false;
        }

        if (this.ddlPoint.SelectedValue.ToString() == "0")
        {
            this.Alert(Resources.Lang.EmptyCargospaceIN1_NeedSelectSite);//请选择站点！
            return false;
        }
        return true;

    }

    protected void btnIN_Click(object sender, EventArgs e)
    {
        if (CheckData())
        {
            string errmsg = string.Empty;
            PROC_EmptyCargospaceInOrOut proc = new PROC_EmptyCargospaceInOrOut();
            proc.positionCode = txtCPOSITIONCODE.Text.Trim();
            proc.cmdMode = 1;
            proc.lineId = ddlLine.SelectedValue;
            proc.stnNo = ddlPoint.SelectedValue;
            proc.UserNo = WmsWebUserInfo.GetCurrentUser().UserNo;
            proc.Execute();
            if (proc.ReturnValue == 1)
            {
                Alert(proc.ErrorMessage);
            }
            else
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_OptionComplete);//操作完成!
            }
        }
    }

    protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPoint.Items.Clear();
        DataTable dt = Comm_Sys.GetSiteList(ddlLine.SelectedValue);//获取线下有几个站口
        if (dt.Rows.Count > 1)
        {
            ddlPoint.Items.Insert(0, new ListItem(Resources.Lang.WMS_Common_Element_QingXuanZe, "0"));//请选择
            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                ddlPoint.Items.Insert(i, new ListItem(i.ToString(), i.ToString()));
            }
        }
        else
        {
            ddlPoint.DataSource = dt;
            ddlPoint.DataTextField = "SiteId";
            ddlPoint.DataBind();
        }
    }
}