<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmResetPassword.aspx.cs" Inherits="FrmResetPassword" EnableEventValidation="false" Title="重置密码" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <link href="../../Layout/multi/css/cn/backImage.css" rel="Stylesheet" type="text/css" runat="server" id="multiUrl" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></ajaxToolkit:ToolkitScriptManager>
        </div>
        <table id="Table3" style="height: 100%; width: 50%" cellspacing="1" cellpadding="1" border="0">
            <tr valign="top">
                <td valign="top">
                    <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                        runat="server" id="TabMain">
                        <tr>
                            <td colspan="2" style="text-align: center">重置密码
                            </td>
                        </tr>
                        <tr>
                            <td height="20" id="tdUser" style="text-align: right; width: 175px; height: 28px; line-height: 28px;">
                                <%=Resources.Lang.Login_MSG3%></td>
                            <%--用户名--%>
                            <td class="InputLabel" style="width: 35%">
                                <asp:TextBox ID="txtUserName" runat="server" DESIGNTIMEDRAGDROP="514" MaxLength="16"
                                    CssClass="RequiredInput" onkeydown="if(event.keyCode==9){$get('aa').focus();}"
                                    Width="120px" BackColor="White"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName"
                                    ErrorMessage="<%$ Resources:Lang, Login_MSG15%>" Display="None"> <%--用户名不能为空!--%>
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="InputLabel" style="text-align: right; width: 35%">
                                <asp:Label ID="lblPASSWORD" runat="server" Text="<%$ Resources:Lang, FrmChangePassword_lblPASSWORD%>"></asp:Label><%--原 密 码--%>
                            </td>
                            <td style="width: 65%">
                                <asp:TextBox ID="txtPASSWORD" runat="server" CssClass="RequiredInput" TextMode="Password" Text=""
                                    MaxLength="20" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPASSWORD"
                                    ErrorMessage="<%$ Resources:Lang, FrmChangePassword_RequiredFieldValidator1%>" Display="None"> <%--原密码不能为空!--%>
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="InputLabel" style="text-align: right; width: 35%">
                                <asp:Label ID="lblPASSWORDN" runat="server" Text="<%$ Resources:Lang, FrmChangePassword_lblPASSWORDN%>"></asp:Label><%--新 密 码--%>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPASSWORDNew" runat="server" CssClass="RequiredInput" TextMode="Password" Text=""
                                    MaxLength="20" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtPASSWORDNew" runat="server" ControlToValidate="txtPASSWORDNew"
                                    ErrorMessage="<%$ Resources:Lang, FrmChangePassword_rfvtxtPASSWORDNew%>" Display="None"> <%--新密码不能为空!--%>
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="InputLabel" style="text-align: right; width: 35%">
                                <asp:Label ID="lblPASSWORDQ" runat="server" Text="<%$ Resources:Lang, FrmChangePassword_lblPASSWORDQ%>"></asp:Label><%--确认密码--%>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPASSWORDNew1" runat="server" CssClass="RequiredInput" TextMode="Password" Text=""
                                    MaxLength="20" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtPASSWORDNew1" runat="server" ControlToValidate="txtPASSWORDNew1"
                                    ErrorMessage="<%$ Resources:Lang, FrmChangePassword_rfvtxtPASSWORDNew1%>" Display="None"> <%--确认密码不能为空!--%>
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                         <tr>
                <td style="text-align: center;"  colspan="2">
                    <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>                  
                </td>
            </tr>
                    </table>
                </td>
            </tr>
            <tr valign="top">
                <td valign="top" align="center">
                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" runat="server"  
                        DisplayMode="BulletList" ShowMessageBox="true"  />
                </td>
            </tr>
           
        </table>
    </form>
</body>
</html>






