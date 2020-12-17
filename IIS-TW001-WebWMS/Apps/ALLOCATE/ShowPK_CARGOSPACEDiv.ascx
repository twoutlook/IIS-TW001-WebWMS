<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowPK_CARGOSPACEDiv.ascx.cs" Inherits="Apps_ALLOCATE_ShowPK_CARGOSPACEDiv" %>

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
        <div class="ajaxWebSearchCloseLink" title="<%= Resources.Lang.Commona_Close%>" id="aCloseLink" onclick="document.all('<%=ajaxWebSearChComp.ClientID %>').style.display='none'">
            <img src="../../Images/zs1.png" />
        </div>
        <div runat="server" id="div_Info" class="ajaxWebSearchResults">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="95%" class="tableFilter">
                        <tr>
                            <td width="10%" nowrap="nowrap" align="right"><%= Resources.Lang.FrmBASE_CARGOSPACEList_lblCWAREID%>：
                            </td>
                            <%--编码--%>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtCode" runat="server" Width="74"></asp:TextBox>
                            </td>
                            <td width="10%" nowrap="nowrap" align="right"><%= Resources.Lang.FrmBASE_CARGOSPACEList_lblCCARGONAME%>：
                            </td>
                            <%--名称--%>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtName" runat="server" Width="74"></asp:TextBox>
                            </td>
                            <td width="15%" nowrap="nowrap"></td>
                            <td width="25%">
                                <%-- <asp:DropDownList ID ="ddlIsAll" runat="server">
                                  <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                  <asp:ListItem Text="没有" Value="1" Selected="True"></asp:ListItem>
                                  <asp:ListItem Text="有" Value="2"></asp:ListItem>
                               </asp:DropDownList>--%>
                            </td>
                            <td style="width: 15%">
                                <%-- <asp:Button ID="btnSearch" runat="server" Text="查看" OnClick="btnSearch_Click" CausesValidation="False" style="display:none;" />
                                <asp:Button ID="btnSearchAll" runat="server" Text="查看" OnClick="btnSearchALL_Click" CausesValidation="False" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" nowrap="nowrap" align="right"><%= Resources.Lang.ShowBASE_CARGOSPACEDiv_Title02%>：
                            </td>
                            <%--储位上是否有货--%>
                            <td width="25%" align="left">
                                <asp:DropDownList ID="ddlIsAll" runat="server" Width="100%">
                                  <%--  <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="没有" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="有" Value="2"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                            <td width="10%" nowrap="nowrap" align="right"><%= Resources.Lang.FrmBASE_CARGOSPACEList_lblCTYPE%>：</td>
                            <%--种类--%>
                            <td width="25%" align="left">
                                <asp:DropDownList ID="ddlCtype" runat="server" Width="100%">
                                  <%--  <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="低库位" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="中库位" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="高库位" Value="3"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                            <td width="10%" nowrap="nowrap" align="right"></td>
                            <td style="width: 35%" align="left" colspan="2">
                                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Lang, Common_View%>" OnClick="btnSearch_Click" CausesValidation="False" Style="display: none;" /><%--查看--%>
                                <asp:Button ID="btnSearchAll" runat="server" Text="<%$ Resources:Lang, Common_View%>" OnClick="btnSearchALL_Click" CausesValidation="False" Style="width: 78px;" /><%--查看--%>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="7" id="DataGridNavigator">
                                <%-- <cc1:DataGridNavigator3 ID="grdNavigator" runat="server" GridID="gvReport" ShowPageNumber="false"
                                    ExcelName="gvReport" IsDbPager="True" ExcelButtonVisible="false" SetPageSizeButtonVisible="false"  />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <asp:GridView ID="gvReport" runat="server" AllowPaging="false" BackColor="White"
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
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, FrmBASE_OPERATOR_AREAEdit_lblCCARGOID%>" DataField="CPOSITIONCODE" />
                                        <%--储位编号->储位编码--%>
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, FrmBase_Line_Info_CPOSITION%>" DataField="CPOSITION" />
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, ShowBASE_CARGOSPACEDiv_iqty%>" DataField="iqty" />
                                        <%--库存量--%>
                                    </Columns>
                                </asp:GridView>

                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                            FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                        <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                                    </li>
                                </ul>

                            </td>
                        </tr>
                        <%-- <tr>
                            <td colspan="3" align="center" style="color: #FF0000">
                                提示：请在企业名称中输入企业部分名称，模糊查询较快
                            </td>
                        </tr>--%>
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
<asp:HiddenField ID="txtCinvCode" runat="server" />

