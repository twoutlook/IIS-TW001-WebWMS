<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmSeralEdit.aspx.cs" Inherits="Apps_BAR_FrmSeralEdit" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link type="text/css" rel="Stylesheet" href="../../Layout/CSS/LG/Page.css" id="cssUrl" runat="server" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/Calendar/calendar-blue.css" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/Css/weui.css" />
    <style type="text/css">
        html {
            height: 100%;
        }

        body {
            height: 100%;
        }

        #aspnetForm {
            height: 100%;
        }

        .master_container {
            height: 100%;
        }

        .tableCell {
            display: table;
            width: 100%;
        }

        span.requiredSign {
            color: #FF0000;
            font-weight: bold;
            position: relative;
            left: -15px;
            top: 2px;
        }

        .tdodyRules td {
            padding: 0px;
            text-align: center;
        }

        .tdodyRules .dropDownList {
            height: 23px;
            line-height: 23px;
        }

        .numbercss {
            width: 20px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <asp:Literal ID="ltPageTable" runat="server" Text="序列号管理"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table style="width: 95%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblRuleCode" runat="server" Text="通知单号"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtAsnCode" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="10"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblITYPE" runat="server" Text="单据号"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtBillCode" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="display:none;">
            <td>
                <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, WMS_Common_Button_Search %>" OnClick="btnSearch_Click" ></asp:Button>
            </td>
        </tr>
        <tr class="tableCell" id="trRuleDetail" runat="server">
            <td>
                <div style="height: 500px; overflow-x: scroll; width: 100%;margin-top:15px;">
                    <asp:GridView ID="grdBill" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="true"
                        OnRowDataBound="grdBill_RowDataBound" CssClass="Grid" PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:BoundField DataField="ID" DataFormatString="" HeaderText="" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cinvcode" DataFormatString="" HeaderText="物料编号">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cinvname" DataFormatString="" HeaderText="物料名称">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="quantity" DataFormatString="{0:F2}" HeaderText="数量">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SavedCount" DataFormatString="" HeaderText="已维护数">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" Width="100px" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" Text="<%$ Resources:Lang, WMS_Common_GridView_Edit %>">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                        </Columns>
                    </asp:GridView>
                    <ul class="OneRowStyle">
                        <li>
                            <webdiyer:aspnetpager id="AspNetPager1" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="AspNetPager1_PageChanged"
                                firstpagetext="<%$ Resources:Lang, WMS_Common_Pager_First %>" lastpagetext="<%$ Resources:Lang, WMS_Common_Pager_Last %>" nextpagetext="<%$ Resources:Lang, WMS_Common_Pager_Next %>" prevpagetext="<%$ Resources:Lang, WMS_Common_Pager_Front %>" showpageindexbox="Never"
                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false" xmlns:webdiyer="wuqi.webdiyer">
                                        </webdiyer:aspnetpager>
                        </li>
                        <li>
                            <div><%= Resources.Lang.WMS_Common_Pager_Total %> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.WMS_Common_Pager_Count %></div>
                        </li>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
    
    <input type="hidden" id="hiddcticketcode" runat="server" />
    <input type="hidden" id="hiddInOrOut" runat="server" />

</asp:Content>
