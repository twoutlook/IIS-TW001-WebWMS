<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmChangePassword.aspx.cs" 
    Inherits="Sys_FrmChangePassword" Title="<%$ Resources:Lang, FrmChangePassword_Title02%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %><%-- 更改密码--%>


<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmChangePassword_Title01%> -&gt; <%= Resources.Lang.FrmChangePassword_Title02%><%--个人助理 -&gt; 更改密码--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
     <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
   
    <table id="Table3" style="height: 100%; width: 60%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width:35%">
                            <asp:Label ID="lblPASSWORD" runat="server" Text="<%$ Resources:Lang, FrmChangePassword_lblPASSWORD%>"></asp:Label><%--原 密 码--%>
                        </td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtPASSWORD" runat="server" CssClass="RequiredInput" TextMode="Password"
                                MaxLength="20" Width="96%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPASSWORD"
                                ErrorMessage="<%$ Resources:Lang, FrmChangePassword_RequiredFieldValidator1%>" Display="None"> <%--原密码不能为空!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel">
                            <asp:Label ID="lblPASSWORDN" runat="server" Text="<%$ Resources:Lang, FrmChangePassword_lblPASSWORDN%>"></asp:Label><%--新 密 码--%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPASSWORDNew" runat="server" CssClass="RequiredInput" TextMode="Password"
                                MaxLength="20" Width="96%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtPASSWORDNew" runat="server" ControlToValidate="txtPASSWORDNew"
                                ErrorMessage="<%$ Resources:Lang, FrmChangePassword_rfvtxtPASSWORDNew%>" Display="None"> <%--新密码不能为空!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel">
                            <asp:Label ID="lblPASSWORDQ" runat="server" Text="<%$ Resources:Lang, FrmChangePassword_lblPASSWORDQ%>"></asp:Label><%--确认密码--%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPASSWORDNew1" runat="server" CssClass="RequiredInput" TextMode="Password"
                                MaxLength="20" Width="96%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtPASSWORDNew1" runat="server" ControlToValidate="txtPASSWORDNew1"
                                ErrorMessage="<%$ Resources:Lang, FrmChangePassword_rfvtxtPASSWORDNew1%>" Display="None"> <%--确认密码不能为空!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" runat="server"
                    DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
    </table>

     <table cellspacing="0" cellpadding="0" width="60%" border="0">
        <tr>
            <td style="text-align:center;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
            </td>
        </tr>
    </table>
</asp:Content>
