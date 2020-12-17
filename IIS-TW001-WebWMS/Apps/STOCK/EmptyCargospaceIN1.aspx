<%@ Page Language="C#" AutoEventWireup="true" Title="--栈板入库" MasterPageFile="~/Apps/DefaultMasterPage.master" CodeFile="EmptyCargospaceIN1.aspx.cs" Inherits="Apps_STOCK_EmptyCargospaceIN1" %>


<%@ Register Src="../BASE/ShowBASE_CARGOSPACE_ByCinvcodeDiv.ascx" TagName="ShowBASE_CARGOSPACE_ByCinvcodeDiv" TagPrefix="uc2" %>
<%@ Register Src="../BASE/ShowBASE_CARGOSPACEDiv.ascx" TagName="ShowBASE_CARGOSPACEDiv" TagPrefix="uc3" %>
<%@ Register Src="../BASE/ShowPart_Asn_IDS_Div.ascx" TagName="ShowPart_Asn_IDS_Div" TagPrefix="uc6" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Apps/BAR/ShowCartonSN_Div.ascx" TagName="ShowCartonSN_Div" TagPrefix="uc4" %>
<%@ Register Src="~/Apps/BAR/ShowPllentCarton_Div.ascx" TagName="ShowPllentCarton_Div" TagPrefix="uc5" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <style type="text/css">
        .ui-autocomplete-loading {
            background: white url('../../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }

        .select {
            cursor: pointer;
            position: relative;
            left: -25px;
            top: 4px;
        }
    </style>
    <script type="text/javascript">
        function Show(divID) {
            document.getElementById("ctl00_ContentPlaceHolderMain_ShowBASE_CARGOSPACEDiv1_btnSearch").click();
            disponse_div(event, document.all(divID));
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Group_StockManage %>-&gt;<%= Resources.Lang.WMS_Common_Menu_PalletCodeIn %> &nbsp;<asp:Label ID="lblErrorMsg" runat="server" Text="" Style="color: Red;"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div style="display: none;">
        <iframe id="compareIframe" src=""></iframe>
    </div>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

    <uc3:ShowBASE_CARGOSPACEDiv ID="ShowBASE_CARGOSPACEDiv1" runat="server" />

    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>

    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CwareName %>"></asp:Label>：<%--仓库名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSub" runat="server" CssClass="NormalInputText" Style="width: 200px;" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_Area %>"></asp:Label>：<%--区域--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtArea" runat="server" CssClass="NormalInputText" Style="width: 200px;" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIDS" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CpositionCode %>"></asp:Label>：<%--储位编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPOSITIONCODE" runat="server" CssClass="NormalInputText" Style="width: 200px;" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CpositionName %>"></asp:Label>：<%--储位名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLocName" runat="server" CssClass="NormalInputText" Style="width: 200px;" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang,EmptyCargospaceIN1_InLine %>"></asp:Label>：<%--入库线别--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlLine" runat="server" Style="width: 200px" AutoPostBack="True" OnSelectedIndexChanged="ddlLine_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang,EmptyCargospaceIN1_InSite %>"></asp:Label>：<%--入库站点--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlPoint" runat="server" Style="width: 200px">
                                <asp:ListItem Value="0">请先选择线别</asp:ListItem><%--请先选择线别--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <asp:Button runat="server" ID="btnIN" Text="<%$ Resources:Lang,WMS_Common_Button_ASRSInCposition %>" CssClass="ButtonConfig" OnClick="btnIN_Click" /><%--ASRS入库--%>
                &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang,WMS_Common_Button_Back %>" CausesValidation="false" />
            </td>
        </tr>
    </table>

</asp:Content>
