using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;

public partial class Apps_RD_TestLED : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    public string GetInfo()
    {
        string str = string.Empty;
        return str;
    }
    protected void btnSendTextToLed_Click(object sender, EventArgs e)
    {
        //Web Test
        string texts = this.txtMSG.Text + "\n";
        string msg = "";
       // LEDSendText led = new LEDSendText(Server.MapPath("~/EQ2008_Dll_Set.ini"));
        string id = ddlID.SelectedValue;
        //led.SendTextToLED(Convert.ToInt32(id), texts, out msg);
        lblShow.Text = msg;
        

    }

    protected void imgSelect_Click(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
    }

    protected void SelectOK_Click(object sender, EventArgs e)
    {
        string code = ((LinkButton)sender).Text;
        txtCINVCODE.Text = code;
        ModalPopupExtender.Hide();
    }



    private void ZoneText_TextChanged(object sender, EventArgs e)
    {

    }


   
}