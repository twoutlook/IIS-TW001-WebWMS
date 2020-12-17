<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowSNOutAllo.ascx.cs" Inherits="Apps_OUT_ShowSNOutAllo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<link href="../../Layout/Css/LG/Page.css" rel="Stylesheet" type="text/css" />
<style type="text/css">
    .ajaxWebSearChBox {
        position: absolute;
        background-color: #0d1e4a;
        width: 900px;
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

    .snclass {
        width: 280px;
    }
</style>
<script type="text/javascript">
    function SetSNSplitOutAllo2(ControlName1, Values1, ControlName2, ControlName3, Values2, typeid, type, DateCodeID, DATECODE, CSOID, CSO, VENDORID, VENDORCODE, NeedQty) {
        //debugger;
        if (ControlName1 != "") {
            document.all(ControlName1).value = Values1;
        }
        if (ControlName2 != "") {
            var SNQty = parseFloat(Values2);
            var NeedQty = parseFloat(NeedQty);
            var qty = NeedQty > SNQty ? SNQty.toFixed(2) : NeedQty.toFixed(2);
            document.all(ControlName2).value = qty;
        }
        if (ControlName3 != "") {
            document.all(ControlName3).value = Values2;
        }
        if (typeid != "") {
            document.all(typeid).innerText = type;
        }
        if (DateCodeID != "") {
            document.all(DateCodeID).value = DATECODE;
        }
        if (CSOID != "") {
            document.all(CSOID).value = CSO.replace(/(\s|&nbsp;)+/g, '');
        }
        if (VENDORID != "") {
            document.all(VENDORID).value = VENDORCODE.replace(/(\s|&nbsp;)+/g, '');
        }
    }
    function SetSNSplitOutAllo2_New(ControlName1, Values1, ControlName2, Values2, typeid, type, DateCodeID, DATECODE) {
        if (ControlName1 != "") {
            document.all(ControlName1).value = Values1;
        }
        if (ControlName2 != "") {
            document.all(ControlName2).value = Values2;
        }
        if (typeid != "") {
            document.all(typeid).innerText = type;
        }
        if (DateCodeID != "") {
            document.all(DateCodeID).value = DATECODE;
        }
    }
</script>
<div id="ajaxWebSearChComp" runat="server" class="ajaxWebSearChBox">
    <div id="divHeading">
        <div class="ajaxWebSearchCloseLink" title="关闭" id="aCloseLink" onclick="document.all('<%=ajaxWebSearChComp.ClientID %>').style.display='none'">
            <img src="../../Images/zs1.png" />
        </div>
    </div>
    <div runat="server" id="div_Info" class="ajaxWebSearchResults">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="tableFilter" style="width: 98%;">
                    <tr>
                        <td style="width: 60px;">
                            <%= Resources.Lang.ShowCartonSN_Div_BoxCode %>：
                        </td>
                        <td style="width: 280px;">
                            <asp:TextBox ID="txtSN" runat="server" CssClass="snclass"></asp:TextBox>
                        </td>
                        <td style="width: 60px;">
                            <%= Resources.Lang.WMS_Common_Element_Cinvcode %>：
                        </td>
                        <td style="width: 130px">
                            <asp:TextBox ID="txtCinvCode" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 60px;">
                            <%= Resources.Lang.WMS_Common_Element_Cposition %>：
                        </td>
                        <td style="width: 130px">
                            <asp:TextBox ID="txtCpositionCode" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 80px">
                            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click" ValidationGroup="0" CausesValidation="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:GridView ID="grdSN" runat="server" AllowPaging="false" BackColor="White"
                                BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                                Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="grdSN_PageIndexChanging"
                                OnRowDataBound="grdSN_RowDataBound" OnSelectedIndexChanging="grdSN_SelectedIndexChanging"
                                AutoGenerateSelectButton="True">
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <PagerStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
                                    <asp:BoundField HeaderText="<%$ Resources:Lang, WMS_Common_Element_LeiXing %>" DataField="SNTYPE" ItemStyle-BorderWidth="1" ItemStyle-Width="5%" />
                                    <asp:BoundField HeaderText="<%$ Resources:Lang, ShowCartonSN_Div_BoxCode %>" DataField="SN_CODE" ItemStyle-BorderWidth="1" />
                                    <asp:BoundField HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>" DataField="CINVCODE" ItemStyle-BorderWidth="1" />
                                    <asp:BoundField HeaderText="<%$ Resources:Lang, WMS_Common_Element_Quantity %>" DataField="QUANTITY" DataFormatString="{0:F}" ItemStyle-BorderWidth="1" ItemStyle-Width="10%" />
                                    <asp:BoundField HeaderText="DATECODE" DataField="DATECODE" ItemStyle-BorderWidth="1" />
                                    <asp:BoundField HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cposition %>" DataField="cpositioncode" ItemStyle-BorderWidth="1" />
                                    <asp:BoundField HeaderText="<%$ Resources:Lang, ShowSNOutAllo_ZhiDingDingDan %>" DataField="ERPCODE" ItemStyle-BorderWidth="1" />
                                    <asp:BoundField HeaderText="<%$ Resources:Lang, WMS_Common_Element_ZhiDingSupplier %>" DataField="VENDORCODE" ItemStyle-BorderWidth="1" />
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
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/Layout/Js/div_show.js" />
    </Scripts>
</asp:ScriptManagerProxy>
<input type="hidden" id="hiddErpCode" runat="server" />
<input type="hidden" id="hiddIsNeedErp" runat="server" value="" />
<input type="hidden" id="hiddIsNeedVendor" runat="server" />
<input type="hidden" id="hiddVendorCode" runat="server" value="" />