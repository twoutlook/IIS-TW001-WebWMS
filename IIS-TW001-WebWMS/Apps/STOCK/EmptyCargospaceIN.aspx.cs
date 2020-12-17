using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_STOCK_EmptyCargospaceIN : WMSBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowBASE_CARGOSPACEDiv1.SetORGCode = txtCPOSITIONCODE.ClientID;
        btnIN.Attributes.Add("onclick", this.GetPostBackEventReference(btnIN) + ";this.disabled=true;");//加到Page_Load方法中，不要放到IsPostBack中
    }

    public bool CheckData()
    {
        if (txtCPOSITIONCODE.Text.Trim() == string.Empty)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void btnIN_Click(object sender, EventArgs e)
    {
        if (CheckData())
        {
            string errmsg = string.Empty;

            if (WmsDBCommon_ASRS.EmptyCargospaceInOrOut(txtCPOSITIONCODE.Text.Trim(), out errmsg, 1, "", ""))
            {
                this.Alert(Resources.Lang.WMS_Common_Tips_OptionComplete);//操作完成!
            }
            else
            {
                this.Alert(errmsg);
            }
        }
        else
        {
            this.Alert(Resources.Lang.WMS_Common_Tips_NeedCposition);//请输入储位！
        }
    }
}