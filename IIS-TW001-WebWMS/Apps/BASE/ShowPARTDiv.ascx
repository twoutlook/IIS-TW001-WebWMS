<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowPARTDiv.ascx.cs" Inherits="UserControls_ShowPARTDiv" %>
<link href="../../Layout/Css/LG/Page.css" rel="Stylesheet" type="text/css" />

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>


<style type="text/css">
    .ajaxWebSearChBox
    {
        position: absolute;
        background-color: #0d1e4a;
        width:650px;       
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
        <div class="ajaxWebSearchCloseLink" title="<%= Resources.Lang.Commona_Close%>" id="aCloseLink" onclick="document.all('<%=ajaxWebSearChComp.ClientID %>').style.display='none'"><%--关闭--%>
            <img src="../../Images/zs1.png" /></div>
        <div runat="server" id="div_Info" class="ajaxWebSearchResults">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 95%" class="tableFilter">
                        <tr>
                            <td width="15%" nowrap="nowrap">
                               <%= Resources.Lang.FrmBASE_CARGOSPACEList_lblCWAREID%>：<%--编码--%>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="txtPartNumber" runat="server" Width="104"></asp:TextBox>
                            </td>

                            <td width="15%" nowrap="nowrap">
                               批/序號(RANK)：<%--批/序號(RANK)--%>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="txtRank_Final" runat="server" Width="104"></asp:TextBox>
                            </td>

                            <td width="15%" nowrap="nowrap">
                                <%= Resources.Lang.FrmBASE_CARGOSPACEList_lblCCARGONAME%>：<%--名称--%>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="txtName" runat="server" Width="104"></asp:TextBox>
                            </td>
                                        
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <%= Resources.Lang.FrmBASE_PARTList_CSPECIFICATIONS%>：<%--规格--%>
                            </td>
                             <td width="15%" nowrap="nowrap">
                                <asp:TextBox ID="txtcspec" runat="server" Width="104"></asp:TextBox>
                            </td>
                          <td style="text-align:center" colspan="2">
                                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Lang, Common_View%>" OnClick="btnSearch_Click" CausesValidation="False" /><%--查看--%>
                            </td>                
                        </tr>
                        <tr>
                            <td colspan="4" id="DataGridNavigator">
                                <%--<cc1:DataGridNavigator3 ID="grdNavigator" runat="server" GridID="gvReport" ShowPageNumber="false"
                                    ExcelName="gvReport" IsDbPager="True" ExcelButtonVisible="false" SetPageSizeButtonVisible="false" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:GridView ID="gvReport" runat="server" AllowPaging="false" BackColor="White"  DataKeyNames="cpartnumber,cspecifications"
                                    BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="6" GridLines="Horizontal"
                                    Width="100%" AutoGenerateColumns="false" OnPageIndexChanging="gvReport_PageIndexChanging"
                                    OnSelectedIndexChanging="gvReport_SelectedIndexChanging" 
                                    AutoGenerateSelectButton="True" ondatabound="gvReport_DataBound" OnRowDataBound="gvReport_RowDataBound">
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />                                    
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>                                       
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, Common_LiaoHao%>" DataField="CPARTNUMBER" /><%--料号--%>
                                        <asp:BoundField HeaderText="RANK" DataField="Rank_Final" />  <%--RANK--%>
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, Common_CinvName1%>" DataField="CPARTNAME" /> <%--品名--%>
                                        <asp:BoundField HeaderText="规格" DataField="CSPECIFICATIONS" />  <%--规格--%>  
                                        <asp:BoundField DataField="boxNum" />
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
                        <tr>
                            <td colspan="4">
                                <asp:HiddenField ID="hfInType" runat="server" Value="" />
                                <asp:HiddenField ID="hfIsAll" runat="server" Value="0" />
                                <asp:HiddenField ID="hfErpcode" runat="server" Value="" />
                                <asp:HiddenField ID="hfIsSpecialWIP_Issue" runat="server" Value="0" />
                                <asp:HiddenField ID="hfGroupFlag" runat="server" Value="" /><%--20130516174204--%>
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
