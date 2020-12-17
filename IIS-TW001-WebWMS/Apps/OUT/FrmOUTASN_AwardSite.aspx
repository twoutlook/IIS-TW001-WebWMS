<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmOUTASN_AwardSite.aspx.cs" Inherits="Apps_OUT_FrmOUTASN_AwardSite" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link type="text/css" rel="Stylesheet" href="../../Layout/CSS/LG/Page.css" id="cssUrl" runat="server" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css" />
    <style type="text/css">
        .ModeRadio {
	    width:160px;
	}
	.ModeRadio input {
	    vertical-align:middle;
	    margin:0px;
	}
	.ModeRadio label {
	    vertical-align:middle;
	    margin-right:10px;
	    margin-bottom:0px;
	}
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt;<%= Resources.Lang.FrmOUTASNList_Menu_PageName %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">

    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="通知单号"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCticketcode" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblCINVCODE" runat="server" Text="出库模式"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:RadioButtonList ID="rbtList" runat="server" RepeatDirection="Horizontal" OnTextChanged="rbtList_TextChanged" AutoPostBack="True" RepeatLayout="Flow" BorderStyle="None" CssClass="ModeRadio">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="tdSiteSelect" runat="server" visible="false">
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblID" runat="server" Text="出库站点"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:DropDownList ID="drpSites" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td colspan="4" style="text-align: center; padding: 15px 0px;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="出库" />
                &nbsp;&nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
            </td>
        </tr>
    </table>

<input id="hiddAsnGuid" type="hidden" runat="server" />
<input id="hiddOutMode" type="hidden" runat="server" />
</asp:Content>
