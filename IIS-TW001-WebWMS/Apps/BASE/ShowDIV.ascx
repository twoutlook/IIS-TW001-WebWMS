<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowDIV.ascx.cs" Inherits="Apps_BASE_ShowDIV" %>
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

        <div runat="server" id="div_Info" class="ajaxWebSearchResults">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="95%" class="tableFilter">
                        <tr>
                            <td width="15%" nowrap="nowrap">
                                <%= Resources.Lang.FrmBASE_CARGOSPACEList_lblCWAREID%>：<%--编码--%>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="txtCinvCode" runat="server" Width="104"></asp:TextBox>
                            </td>
                            <td width="15%" nowrap="nowrap">
                                <%= Resources.Lang.FrmBASE_CARGOSPACEList_lblCCARGONAME%>：<%--名称--%>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="txtCinvName" runat="server" Width="104"></asp:TextBox>
                            </td>
                            <td style="width: 20%">
                                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Lang, Common_View%>" OnClick="btnSearch_Click" CausesValidation="False" /><%--查看--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" id="DataGridNavigator">
                               <%-- <cc1:DataGridNavigator3 ID="grdNavigator" runat="server" GridID="gvReport" ShowPageNumber="false"
                                    ExcelName="gvReport" IsDbPager="True" ExcelButtonVisible="false" SetPageSizeButtonVisible="false" />
                         --%>   </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:GridView ID="gvReport" runat="server" AllowPaging="False" BackColor="White" DataKeyNames="IDS"
                                    BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                                    Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="gvReport_PageIndexChanging"
                                    OnSelectedIndexChanging="gvReport_SelectedIndexChanging" AutoGenerateSelectButton="True">
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, Common_LiaoHao%>" DataField="cinvcode" /><%--料号--%>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_LiaoHao%>"> <%--料号--%>
                                           <ItemTemplate>
                                               <asp:LinkButton ID="lbtncinvcode" runat="server" Text='<%#Eval("cinvcode") %>' OnClick="LinkBtn_Click"></asp:LinkButton>
                                           </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, Common_CinvName1%>" DataField="cinvname" /><%--品名--%>
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, ShowBomList_Div_cerpcodeline%>" DataField="cerpcodeline" /><%--子项ERP--%>
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, Common_IQUANTITY%>" DataField="iquantity" /><%--数量--%>
                                    </Columns>
                                </asp:GridView>

                                <ul class="OneRowStyle">
                                    <li >
                                         <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                           FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                        <div><%= Resources.Lang.Base_Gong%><%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
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

