<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowNotcieDetail.ascx.cs"
    Inherits="ShowNotcieDetail" %>


<script language="javascript" type="text/javascript">
    function closeShowNotcieDetail() {
        ShowNoFlash("DivNotcieDetail");
        return true;
    }
</script>
<asp:UpdatePanel ID="UpdatePanel6" runat="server">
<ContentTemplate>
<link href="../CSS/LG/login.css" rel="stylesheet" type="text/css" runat="server" id="cssUrl" /> 
<table style="padding:0; margin:0; " width="480px" border="1" align="left" cellpadding="0" cellspacing="0" class="info">
                <tr>
                  <td height="32" style="background-image:url(../CSS/LG/images/Login/td_bg.gif)">
                    <table width="96%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="4%">
                                <img src="../CSS/LG/images/Login/icon1.gif" width="5" height="11" alt="" />
                            </td>
                            <td width="61%" align="left">
                                <%=Resources.Lang.Login_MSG2 %><%--公告--%>
                            </td>
                            <td align="right">

                                <input id="btnClose" runat="server" type="button" value=" <%$Resources:Lang,Login_btnClose %>" class="buttonclose"
                                    onclick="closeShowNotcieDetail();" /><%--关闭--%>
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
                <tr>
                   <td align="center" style="border-bottom-color: #b7e5ff; background-color: #e5f5ff;
                        padding: 5px; height:20px">
                        <span style="width: 100%; color: #ff66cc; font-family: Arial, Helvetica, sans-serif;
                            font-size: 16px; font-weight: bold;">
                            <asp:Label ID="lbTitle" runat="server"></asp:Label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="height: 300px; padding: 5px;" valign="top">
                        <span style="width: 100%;">
                            <asp:Label ID="lbContent" runat="server"></asp:Label>
                        </span>
                    </td>
                </tr>
</table> 
</ContentTemplate>
</asp:UpdatePanel>