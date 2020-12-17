<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Apps/DefaultMasterPage.master" CodeFile="FrmOUTASNSplit.aspx.cs" Inherits="Apps_OUT_FrmOUTASNSplit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt;<%= Resources.Lang.FrmOUTASNSplit_Menu_PageName %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">

    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top">
            <td valign="top">
                <div style="height: 460px; width: 100%" id="DivScroll">

                    <asp:GridView ID="grdOUTASN_D" runat="server" AllowPaging="True" BorderColor="Teal"
                        DataKeyNames="IDS,CINVCODE,IQUANTITY,LessQty" BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                        Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdOUTASN_D_RowDataBound"
                        CssClass="Grid" PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:TemplateField HeaderText="<input id='chkSelect' type='checkbox' onclick='SelectAll4Grid(this)' />">
                                <ControlStyle BorderWidth="0px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid"
                                        BorderWidth="0px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IQUANTITY" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmOUTASNEdit_Column_ZongShuLiang %>"><%--总数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OutBill_Qty" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmOUTASNEdit_Column_ChuKuLiang %>"><%--出库量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OutBilled_Qty" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmOUTASNSplit_KouZhangLiang %>"><%--扣账量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmOUTASNSplit_CanSplitQuantity %>"><%--可拆解数量--%>
                                <ControlStyle BorderWidth="0px" />
                                <ItemTemplate>
                                    <asp:TextBox CssClass="NormalInputText" ID="txtLessQty" runat="server" Enable="false" Text='<%# Eval("LessQty") %>' Width="95%"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmOUTASNSplit_SplitQuantity %>"><%--拆解数量--%>
                                <ItemTemplate>
                                    <asp:TextBox CssClass="NormalInputText" ID="txtCjQty" runat="server" Width="95%"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CSO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNList_SourceCode %>"><%--来源单号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ISOLINE" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmOUTASNEdit_Column_LaiYuanDanXiangCi %>" Visible="false"><%--来源单号项次--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODELINE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTBILL_DEdit_ErpCodeLineId %>"><%--ERP单号项次--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODELINE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNSplit_ZiBiaoERPCode %>" Visible="False"><%--子表ERP单号项次--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>

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
                </div>
            </td>
        </tr>

        <tr style="display: none">
            <td>
                <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50" Enabled="False"></asp:TextBox>
                <asp:HiddenField ID="hfInAsn_Id" runat="server" />
                <asp:HiddenField ID="hfIsConfirm" runat="server" Value="0" />
                <!--是否提示-->
            </td>
        </tr>

    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0" style="text-align: center;">
        <tr>
            <td align="center" style="text-align: center;">
                <asp:Button ID="btnCj" runat="server" CssClass="ButtonConfig" OnClick="btnSave_Click" Text="<%$ Resources:Lang,WMS_Common_Button_Split %>" />&nbsp;&nbsp; <%--拆解--%>
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
