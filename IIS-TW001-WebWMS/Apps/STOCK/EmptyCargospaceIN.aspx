<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmptyCargospaceIN.aspx.cs" Inherits="Apps_STOCK_EmptyCargospaceIN" Title="--空栈板入库" MasterPageFile="~/Apps/DefaultMasterPage.master"  %>

<%@ Register Src="../BASE/ShowBASE_CARGOSPACE_ByCinvcodeDiv.ascx" TagName="ShowBASE_CARGOSPACE_ByCinvcodeDiv" TagPrefix="uc2" %>
<%@ Register Src="../BASE/ShowBASE_CARGOSPACEDiv.ascx" TagName="ShowBASE_CARGOSPACEDiv" TagPrefix="uc3" %>
<%@ Register Src="../BASE/ShowPart_Asn_IDS_Div.ascx" TagName="ShowPart_Asn_IDS_Div" TagPrefix="uc6" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Apps/BAR/ShowCartonSN_Div.ascx" TagName="ShowCartonSN_Div" TagPrefix="uc4" %>
<%@ Register Src="~/Apps/BAR/ShowPllentCarton_Div.ascx" TagName="ShowPllentCarton_Div"    TagPrefix="uc5" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <style type="text/css">
        .ui-autocomplete-loading
        {
            background: white url('../../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }
        .select
        {
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
    <%= Resources.Lang.WMS_Common_Group_StockManage %>-&gt;<%= Resources.Lang.WMS_Common_Menu_EmptyPalletCodeIn %> &nbsp;<asp:Label ID="lblErrorMsg" runat="server" Text="" Style="color: Red;"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div style="display: none;">
        <iframe id="compareIframe" src=""></iframe>
    </div>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

    <uc3:ShowBASE_CARGOSPACEDiv ID="ShowBASE_CARGOSPACEDiv1" runat="server" />

    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <table id="Table3" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIDS" runat="server" Text="<%$ Resources:Lang,EmptyCargospaceIN_InCposition %>"></asp:Label>：<%--入库储位--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPOSITIONCODE" runat="server" CssClass="NormalInputText" style="width:200px;"></asp:TextBox>
                            <img alt="" onclick="Show('<%= ShowBASE_CARGOSPACEDiv1.GetDivName %>');" src="../../Images/Search.gif"
                                class="select" />
                        </td>
                        <td  class="InputLabel" style="width: 20%">
                           <asp:Button runat="server" ID="btnIN" Text="<%$ Resources:Lang,WMS_Common_Button_ASRSInCposition %>" CssClass="ButtonConfig" onclick="btnIN_Click"/><%--ASRS入库--%> 
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       
    </table>
   
</asp:Content>