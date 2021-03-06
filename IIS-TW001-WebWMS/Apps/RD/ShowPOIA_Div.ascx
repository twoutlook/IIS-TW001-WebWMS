﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowPOIA_Div.ascx.cs" Inherits="UserControls_ShowPOIA_Div" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>


<link href="../../Layout/Css/LG/Page.css" rel="Stylesheet" type="text/css" />
<style type="text/css">
    .ajaxWebSearChBox
    {
        position: absolute;
        background-color: #0d1e4a;
        width: 400px;
        padding: 1px;
        display: none;
    }
    .ajaxWebSearchHeading
    {
        position: relative;
        background-color: #1162cc;
        font: bold 14px 宋体;
        height: 0;
        color: White;
        padding: 3px 0px 0px 2px;
    }
    .ajaxWebSearchCloseLink
    {
        position: absolute;
        right: 5px;
        text-decoration: none;
        color: Red;
        cursor: hand;
        font-size: large;
    }
    .ajaxWebSearchCloesLink:hover
    {
        color: Red;
    }
    .ajaxWebSearchResults
    {
        background-color: #d3e5fa;
        padding: 5px;
    }
    .ajaxWebSearchResult:div
    {
        text-align: center;
        font: bold 14px 宋体;
        color: #0a246a;
    }
    a.ajaxWebSearchLink
    {
        font: 12px 宋体;
        padding: 2px;
        display: block;
        color: #0a246a;
    }
    a.ajaxSearchLink:hover
    {
        color: White;
        background-color: #316ac5;
    }
    a.ajaxSeachLink:visited
    {
        color: Purple;
    }
    .tableFilter
    {
        border: 1px solid #ccc;
        padding: 2px;
        margin: 5px 0 10px 0;
    }
    .tableFilter input
    {
        border: 1px solid #ccc;
    }
</style>

<div id="ajaxWebSearChComp" runat="server" class="ajaxWebSearChBox">
    <div id="divHeading">
        <%--关闭--%>
        <div class="ajaxWebSearchCloseLink" title="<%= Resources.Lang.ShowINASN_aCloseLink %>" id="aCloseLink" onclick="document.all('<%=ajaxWebSearChComp.ClientID %>').style.display='none'">
            <img src="../../Images/zs1.png" /></div>
        <div runat="server" id="div_Info" class="ajaxWebSearchResults">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="95%" class="tableFilter">
                        <tr>
                            <td width="15%" nowrap="nowrap">
                                PO：
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="txtPONO" runat="server" Width="104"></asp:TextBox>
                            </td>
                            <td style="width: 20%">
                                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Lang, CommonB_View %>" OnClick="btnSearch_Click" CausesValidation="False" /><%--查看--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" id="DataGridNavigator">
                             <%--   <cc1:DataGridNavigator3 ID="grdNavigator" runat="server" GridID="gvReport" ShowPageNumber="false"
                                    ExcelName="gvReport" IsDbPager="True" ExcelButtonVisible="false" SetPageSizeButtonVisible="false" />
                           --%> </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="gvReport" runat="server" AllowPaging="True" BackColor="White"  DataKeyNames="ID"
                                    BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                                    Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="gvReport_PageIndexChanging"
                                    OnSelectedIndexChanging="gvReport_SelectedIndexChanging" 
                                    AutoGenerateSelectButton="True" ondatabound="gvReport_DataBound">
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundField HeaderText="PO" DataField="PONO" />
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, ShowPOIA_Div_VENDORID %>" DataField="VENDORID" /><%--供应商--%>
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG24 %>" DataField="VENDORNAME" /><%--供应商名称--%>
                                    </Columns>
                                </asp:GridView>

                                <ul class="OneRowStyle">
                                    <li >
                                         <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" 
                                             PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" 
                                             CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                            FirstPageText="<%$ Resources:Lang, Common_FirstPage%>" 
                                             LastPageText="<%$ Resources:Lang, Common_LastPage%>" NextPageText="<%$ Resources:Lang, Common_NextPage%>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                        <div><%=Resources.Lang.Common_TotalPage %> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_TotalPage1 %></div>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                        
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/Layout/Js/div_show.js" />
    </Scripts>
</asp:ScriptManagerProxy>
