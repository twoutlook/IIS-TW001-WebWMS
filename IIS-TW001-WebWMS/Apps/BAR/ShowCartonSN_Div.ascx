<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowCartonSN_Div.ascx.cs" Inherits="ShowCartonSN_Div" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<link href="../../Layout/Css/LG/Page.css" rel="Stylesheet" type="text/css" />
<style type="text/css">
    .ajaxWebSearChBox {
        position: absolute;
        background-color: #0d1e4a;
        width: 500px;
        padding: 1px;
        display: none;
    }

    .ajaxWebSearchHeading {
        position: relative;
        background-color: #1162cc;
        font: bold 14px 宋体;
        height: 0;
        color: White;
        padding: 3px 0px 0px 2px;
    }

    .ajaxWebSearchCloseLink {
        position: absolute;
        right: 5px;
        text-decoration: none;
        color: Red;
        cursor: pointer;
        font-size: large;
    }

    .ajaxWebSearchCloesLink:hover {
        color: Red;
    }

    .ajaxWebSearchResults {
        background-color: #d3e5fa;
        padding: 5px;
    }

    div.ajaxWebSearchResult {
        text-align: center;
        font: bold 14px 宋体;
        color: #0a246a;
    }

    a.ajaxWebSearchLink {
        font: 12px 宋体;
        padding: 2px;
        display: block;
        color: #0a246a;
    }

    a.ajaxSearchLink:hover {
        color: White;
        background-color: #316ac5;
    }

    a.ajaxSeachLink:visited {
        color: Purple;
    }

    .tableFilter {
        border: 1px solid #ccc;
        padding: 2px;
        margin: 5px 0 10px 0;
    }

        .tableFilter input {
            border: 1px solid #ccc;
        }
</style>
<script type="text/javascript">
    function SetSnBack(snid, typeid, qtyid, pllent, carton, qty) {
        pllent = pllent.replace("&nbsp;", "");
        carton = carton.replace("&nbsp;", "");
        if (pllent != "") {
            document.all(snid).value = pllent;
            document.all(typeid).innerText = "<%= Resources.Lang.ShowCartonSN_Div_Pallet %>";
        }
        else {
            document.all(snid).value = carton;
            document.all(typeid).innerText = "<%= Resources.Lang.ShowCartonSN_Div_Box %>";
        }
        document.all(qtyid).value = qty;
    }
</script>
<div id="ajaxWebSearChComp" runat="server" class="ajaxWebSearChBox">
    <div id="divHeading">
        <div class="ajaxWebSearchCloseLink" title="关闭" id="aCloseLink" onclick="document.all('<%=ajaxWebSearChComp.ClientID %>').style.display='none'">
            <img src="../../Images/zs1.png" />
        </div>
        <div runat="server" id="div_Info" class="ajaxWebSearchResults">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="tableFilter" style="width: 95%;">
                        <tr>
                            <td style="width: 15%; white-space: nowrap;">
                                <%= Resources.Lang.WMS_Common_Element_PalletCode %>：
                            </td>
                            <td style="width: 20%;">
                                <asp:TextBox ID="txtPllent" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 15%; white-space: nowrap;">
                                <%= Resources.Lang.ShowCartonSN_Div_BoxCode %>：
                            </td>
                            <td style="width: 20%;">
                                <asp:TextBox ID="txtCarton" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 20%">
                                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click" ValidationGroup="0" CausesValidation="False" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:GridView ID="grdCartonSN" runat="server" AllowPaging="True" BackColor="White"
                                    BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                                    Width="100%" AutoGenerateColumns="False"
                                    OnSelectedIndexChanging="grdCartonSN_SelectedIndexChanging" AutoGenerateSelectButton="True">
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, WMS_Common_Element_PalletCode %>" DataField="PALLETNO" />
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, ShowCartonSN_Div_BoxCode %>" DataField="CARTON_NO" />
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>" DataField="CINVBARCODE" />
                                        <asp:BoundField HeaderText="<%$ Resources:Lang, WMS_Common_Element_Quantity %>" DataField="QTY" />
                                    </Columns>
                                </asp:GridView>
                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                            FirstPageText="<%$ Resources:Lang,WMS_Common_Pager_First %>" LastPageText="<%$ Resources:Lang,WMS_Common_Pager_Last %>" NextPageText="<%$ Resources:Lang,WMS_Common_Pager_Next %>" PrevPageText="<%$ Resources:Lang,WMS_Common_Pager_Front %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                        <div><%= Resources.Lang.WMS_Common_Pager_Total %> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.WMS_Common_Pager_Count %></div>
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
