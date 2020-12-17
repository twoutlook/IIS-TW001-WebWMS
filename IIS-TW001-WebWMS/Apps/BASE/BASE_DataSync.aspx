<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BASE_DataSync.aspx.cs" Inherits="Apps_BASE_BASE_DataSync"  MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.BASE_DataSync_Title01%>  <%-- 基礎資料-&gt;数据同步管理--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <ajaxToolkit:toolkitscriptmanager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
      <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <table  id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td class="InputLabel" style="width: 8%">
                 <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, BASE_DataSync_lblAreaame%>"></asp:Label>：<%--权限同步--%>
            </td>
             <td valign="top">
                   <asp:Button ID="btnRight" runat="server" CssClass="ButtonTB"
                    Text="<%$ Resources:Lang, BASE_DataSync_btnAccess%>" OnClick="btnRight_Click" /><%--同步--%>
            </td>
        </tr>
        
    </table>
</asp:Content>