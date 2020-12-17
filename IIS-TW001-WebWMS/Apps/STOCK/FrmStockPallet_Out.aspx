<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" Title="--栈板出库" AutoEventWireup="true" CodeFile="FrmStockPallet_Out.aspx.cs" Inherits="FrmStockPallet_Out" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link type="text/css" rel="Stylesheet" href="../../Layout/CSS/LG/Page.css" id="cssUrl" runat="server" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/Calendar/calendar-blue.css" />
    <style type="text/css">
        html
        {
            height: 100%;
        }

        body
        {
            height: 100%;
        }

        #aspnetForm
        {
            height: 100%;
        }

        .master_container
        {
            height: 100%;
        }

        .tableCell
        {
            display: table;
            width: 100%;
        }

        span.requiredSign
        {
            color: #FF0000;
            font-weight: bold;
            position: relative;
            left: -15px;
            top: 2px;
        }

        .gridLineHeight
        {
            line-height: 22px;
        }

        input[type='submit'][disabled], input[type='button'][disabled]
        {
            opacity: 0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70); /*兼容ie8及以下*/
        }

        .btnContainer input:not(:first-child)
        {
            margin-left: 12px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    库存管理-&gt;<asp:Literal ID="ltPageTable" runat="server" Text="栈板出库"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%--  <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>--%>
    <table id="Table1" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblWareHouse" runat="server" Text="仓库："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlWareHouse" runat="server" Width="95%"></asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblPalletcode" runat="server" Text="栈板号："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPalledCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="储位编号："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPocitionCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="储位名称："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPositionName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="出库线别："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlOutCrane" runat="server" Width="95%"  OnSelectedIndexChanged="ddlOutCrane_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="出库站点："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlOutSite" runat="server" Width="95%"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="text-align: center; padding: 15px 0px;">
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonConfig5" 
                                Text="AS/RS出库" OnClick="btnSave_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="返回" CausesValidation="false" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                  <table id="TabMain0" style="height: 100%; width: 100%" runat="server" >
                       <tr>
                        <td colspan="4">
                            <div style="min-height: 300px; overflow-x: auto; width: 100%" id="DivScroll">
                                <asp:GridView ID="grdStockPalletOut" runat="server" AllowPaging="True" BorderColor="Teal"
                                     BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ShowHeaderWhenEmpty="true"
                                    Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdStockPalletOut_RowDataBound"
                                    CssClass="Grid gridLineHeight" PageSize="15">
                                    <PagerSettings Visible="False" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <RowStyle HorizontalAlign="Left" Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                    <Columns>                                       
                                       
                                        <asp:BoundField DataField="cinvcode" DataFormatString="" HeaderText="料号">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="360px" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="calias" DataFormatString="" HeaderText="助记码">
				                             <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                                            <ItemStyle HorizontalAlign="left"  Wrap="False" Width="360px" /> 
				                     </asp:BoundField>
                                        <asp:BoundField DataField="cinvname" DataFormatString="" HeaderText="品名">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="360px" />
                                        </asp:BoundField>
                                       
                                        <asp:BoundField DataField="iqty" DataFormatString="{0:F}" HeaderText="数量">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>                                      
                                    </Columns>
                                </asp:GridView>
                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                            FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                        <div>共 <%=AspNetPager1.RecordCount  %> 条数据</div>
                                    </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                   </table>
            </td>
        </tr>
    </table>
</asp:Content>
