<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" Title="--库存明细" CodeFile="FrmSTOCK_CURRENT_DETAIL_Edit.aspx.cs" Inherits="FrmSTOCK_CURRENT_DETAIL_Edit" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script type="text/javascript">

        function setTab(name, cursel, n) {
            document.getElementById("ctl00_ContentPlaceHolderMain_hTabIndex").value = cursel;
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                var con = document.getElementById("con_" + name + "_" + i);
                menu.className = i == cursel ? "hover" : "";
                con.style.display = i == cursel ? "block" : "none";

            }
        }

        window.onload = function () {
            var indexT = document.getElementById("ctl00_ContentPlaceHolderMain_hTabIndex").value;
            document.getElementById("one" + indexT).click();
        }
        function hidtrbn() {
            var trbn = document.getElementById("tr_bncode");
            trbn.style.display = "none";
        }
    </script>
    <style type="text/css">
        body {
            padding: 0;
            font: 12px "宋体";
        }
        /*Tab1*/ #lib_Tab1 {
            width: 100%;
            margin: 0 auto;
            padding: 0px;
            margin-top: 2px;
            margin-bottom: 2px;
        }
        /*Tab2*/ #lib_Tab2 {
            width: 1000px;
            margin: 0 auto;
            padding: 0px;
            margin-top: 4px;
            margin-bottom: 5px;
        }

        .lib_tabborder {
            border: 1px solid #D5E3F0;
        }

        .lib_Menubox {
            height: 28px;
            line-height: 28px;
            position: relative;
        }

            .lib_Menubox ul {
                margin: 0px;
                padding: 0px;
                list-style: none;
                position: absolute;
                top: 3px;
                left: 0;
                margin-left: 10px;
                height: 25px;
                text-align: center;
                width: 240px;
            }

            .lib_Menubox li {
                float: left;
                display: block;
                cursor: pointer;
                width: 115px;
                color: #949694;
                font-weight: bold;
                margin-right: 2px;
                height: 25px;
                line-height: 25px;
                background-color: #F0F3FA;
            }

                .lib_Menubox li.hover {
                    padding: 0px;
                    background: #fff;
                    width: 115px;
                    border-left: 1px solid #95C9E1;
                    border-top: 1px solid #95C9E1;
                    border-right: 1px solid #95C9E1;
                    color: #739242;
                    height: 25px;
                    line-height: 25px;
                }

        .lib_Contentbox {
            clear: both;
            margin-top: 0px;
            border-top: none;
            min-height: 50px;
            text-align: center;
            padding-top: 8px;
        }

        #topimg {
            margin: 0px auto;
            width: 1000px;
            margin-top: 3px;
        }

        #top img {
            width: 1000px;
            height: 55px;
            margin: 0px auto;
        }

        .divcontent {
            width: 1000px;
            margin: 0 auto;
            margin-top: 3px;
            margin-bottom: 4px;
            border: 1px solid #D5E3F0;
        }

            .divcontent .divAbuy {
                height: 50px;
                width: 200px;
                size: 16px;
                margin-top: 10px;
                margin-left: 10px;
            }

        #search {
            width: 1000px;
            margin: 0 auto;
            height: 50px;
            margin-top: 3px;
            border: 1px solid #D5E3F0;
        }

            #search .sControl {
                margin-top: 10px;
                margin-left: 5px;
                height: 25px;
                width: 580px;
            }

                #search .sControl .leftDate {
                    float: left;
                    width: 420px;
                }

                #search .sControl .rightBtn {
                    float: right;
                    width: 160px;
                }

            #search .btncss {
                width: 60px;
                height: 30px;
            }

            #search .btnSearch {
                width: 63px;
                height: 22px;
                background: url( "../Templets/newimages/search.gif" );
                border: 0;
                cursor: pointer;
            }

        .btnExpert {
            width: 63px;
            height: 22px;
            background: url( "../Templets/newimages/export.gif" );
            border: 0;
            cursor: pointer;
        }

        .dzfw {
            padding: 5px 15px;
            border: 1px solid #d5e3f0;
            overflow: hidden;
            width: 968px;
            margin: 10px 0;
        }

        .dzfw_title {
            float: left;
            margin: 15px 8px 0 0;
        }

        .dzfw_list {
            float: left;
            width: 448px;
        }

            .dzfw_list img {
                float: left;
                margin: 5px 5px 5px 12px;
            }

        .dzfw_title1 {
            float: left;
            margin: 2px 8px 0 0;
        }

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

        .gridLineHeight {
            line-height: 22px;
        }

            .gridLineHeight input {
                line-height: 1;
            }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%--库存查询--%><%= Resources.Lang.WMS_Common_Menu_StockSearch %>-&gt;<%= Resources.Lang.FrmSTOCK_CURRENT_DETAIL_Edit_PageName %><%--库存明细--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="width: 95%">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                            height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.WMS_Common_SearchTitle %>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.WMS_Common_FoldAlt %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr style="display: none">
                        <td colspan="6">
                            <style type="text/css">
                                span.requiredSign {
                                    color: #FF0000;
                                    font-weight: bold;
                                    position: relative;
                                    left: -15px;
                                    top: 2px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CwareCode %>"></asp:Label>：<%--仓库编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtWAREHOUSE_NO" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CpositionCode %>"></asp:Label>：<%--储位编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPOSITIONCODE" runat="server" Enabled="false" CssClass="NormalInputText"
                                Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CpositionName %>"></asp:Label>：<%--储位名称--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCPOSITION" runat="server" Enabled="false" CssClass="NormalInputText"
                                Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCPO" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"></asp:Label>：<%--料号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCINVCODE" runat="server" Enabled="false" CssClass="NormalInputText"
                                Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblRANK_FINAL" runat="server" Text="批/序號(RANK)"></asp:Label>：<%--rank--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtRANK_FINAL" runat="server" Enabled="false" CssClass="NormalInputText"
                                Width="95%" MaxLength="1"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>"></asp:Label>：<%--品名--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCINVNAME" runat="server" Enabled="false" CssClass="NormalInputText"
                                Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_StockQuantity %>"></asp:Label>：<%--库存数量--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIQTY" runat="server" Enabled="false" CssClass="NormalInputText"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCVENDER" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_ZhanYongQuantity %>"></asp:Label>：<%--占用数量--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIOCCUPYQTY" runat="server" Enabled="false" CssClass="NormalInputText"
                                Width="95%" MaxLength="50"></asp:TextBox>
                        </td>
                        <td colspan="2" class="InputLabel" style="text-align: right;">
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang,WMS_Common_Button_Back %>" CausesValidation="false" />
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" Visible="false"
                                OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="6">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td colspan="6">
                <div id="lib_Tab1" style="height: 100%; width: 100%; margin-top: 15px;">
                    <div class="lib_Menubox lib_tabborder">
                        <ul>
                            <li id="one1" onclick="setTab('one',1,2)" class="hover"><%= Resources.Lang.FrmSTOCK_CURRENT_DETAIL_Edit_DateCodeList %></li>
                            <%--DateCode列表--%>
                            <li id="one2" onclick="setTab('one',2,2)">
                                <asp:Label ID="Label_BS1" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_SNType %>"></asp:Label><%--SN/箱/栈板--%>
                            </li>
                        </ul>
                    </div>
                    <div class="lib_Contentbox lib_tabborder">
                        <div id="con_one_1">
                            <table id="Table1" style="height: 100%; width: 100%">
                                <tr style="display: none">
                                    <asp:HiddenField ID="hTabIndex" runat="server" Value="1" />
                                </tr>
                                <tr>
                                    <td>
                                        <div style="height: 500px; overflow-x: scroll; width: 100%" id="Div2">
                                            <asp:GridView ID="grdINASN" runat="server" AllowPaging="True" BorderColor="Teal"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                                OnRowDataBound="grdINASN_RowDataBound" CssClass="Grid gridLineHeight" PageSize="15">
                                                <PagerSettings Visible="False" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                                <RowStyle HorizontalAlign="Left" Wrap="False" />
                                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                                <PagerStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                                <Columns>
                                                    <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="IDS" Visible="False">
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="right" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="ID" Visible="False">
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="right" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DATECODE" DataFormatString="" HeaderText="DateCode">
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="WEEKS" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmSTOCK_CURRENT_DETAIL_Edit_Weeks %>"><%--周数--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTY" DataFormatString="{0:F}" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Quantity %>"><%--数量--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="right" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CREATEUSER" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_CreateUser %>"><%--创建人--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CREATEDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss }" HeaderText="<%$ Resources:Lang,WMS_Common_CreateTimeFrom %>"><%--创建时间--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="left" Wrap="False" Width="200px" />
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

                            </table>
                        </div>
                        <div id="con_one_2" style="display: none">
                            <div style="padding: 5px 0px 15px 0px; text-align: right;">
                                <asp:Label ID="Label_BS2" runat="server" Text="SN："></asp:Label>
                                <asp:TextBox ID="txtSNCode" runat="server" CssClass="NormalInputText" Width="43%"></asp:TextBox>
                                <asp:Button ID="btnSearch1" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch1_Click"></asp:Button>
                            </div>
                            <table id="Table3" style="height: 100%; width: 100%">
                                <tr>
                                    <td colspan="5">
                                        <div style="height: 500px; overflow-x: scroll; width: 100%" id="Div1">
                                            <asp:GridView ID="grdSNList" runat="server" AllowPaging="True" BorderColor="Teal"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                                OnRowDataBound="grdSNList_RowDataBound" CssClass="Grid gridLineHeight" PageSize="15">
                                                <PagerSettings Visible="False" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                                <RowStyle HorizontalAlign="Left" Wrap="False" />
                                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                                <PagerStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Lang,WMS_Common_Element_SNType %>" ItemStyle-Width="600px"><%--SN/箱/栈板--%>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSN" runat="server" Width="95%" Text='<%# Eval("SNCODE") %>' Enabled="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="QTY" DataFormatString="{0:F}" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Quantity %>"><%--数量--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="palletcode" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_PalletCode %>"><%--栈板号--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FURNACENO" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Furnaceno %>"><%--材料炉号--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DateCode" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_MarkDate %>"><%--生产日期--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="WEEKS" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmSTOCK_CURRENT_DETAIL_Edit_Weeks %>"><%--周数--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="inBillTypeName" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_WuLiaoType %>"><%--物料类型--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_ZhiDingBill %>"><%--指定单据--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VENDORCODE" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_ZhiDingSupplier %>"><%--指定供应商--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CREATEUSER" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_CreateUser %>"><%--创建人--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang,WMS_Common_CreateTimeFrom %>"><%--创建时间--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="left" Wrap="False" Width="150px" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                            <ul class="OneRowStyle">
                                                <li>
                                                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager2_PageChanged"
                                                        FirstPageText="<%$ Resources:Lang,WMS_Common_Pager_First %>" LastPageText="<%$ Resources:Lang,WMS_Common_Pager_Last %>" NextPageText="<%$ Resources:Lang,WMS_Common_Pager_Next %>" PrevPageText="<%$ Resources:Lang,WMS_Common_Pager_Front %>" ShowPageIndexBox="Never"
                                                        AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                                    </webdiyer:AspNetPager>
                                                </li>
                                                <li>
                                                    <div><%= Resources.Lang.WMS_Common_Pager_Total %> <%=AspNetPager2.RecordCount  %> <%= Resources.Lang.WMS_Common_Pager_Count %></div>
                                                </li>
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                                <tr id="tr_bncode">
                                    <td valign="top" colspan="3">
                                        <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div3">
                                            <asp:GridView ID="gridBN" runat="server" AllowPaging="True" BorderColor="Teal"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False" CssClass="Grid gridLineHeight" PageSize="15">
                                                <PagerSettings Visible="False" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                                <RowStyle HorizontalAlign="Left" Wrap="False" />
                                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                                <PagerStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                                <Columns>
                                                    <asp:BoundField DataField="BNCODE" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmOUTASN_DEdit_PiCiCode %>"><%--批次号--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="DateCode" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_MarkDate %>"><%--生产日期--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="QTY" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Quantity %>"><%--数量--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="createuser" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_CreateUser %>"><%--创建人--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="createtime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang,WMS_Common_CreateTimeFrom %>"><%--创建时间--%>
                                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>

                                            <ul class="OneRowStyle">
                                                <li>
                                                    <webdiyer:AspNetPager ID="AspNetPager3" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager3_PageChanged"
                                                        FirstPageText="<%$ Resources:Lang,WMS_Common_Pager_First %>" LastPageText="<%$ Resources:Lang,WMS_Common_Pager_Last %>" NextPageText="<%$ Resources:Lang,WMS_Common_Pager_Next %>" PrevPageText="<%$ Resources:Lang,WMS_Common_Pager_Front %>" ShowPageIndexBox="Never"
                                                        AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                                    </webdiyer:AspNetPager>
                                                </li>
                                                <li>
                                                    <div><%= Resources.Lang.WMS_Common_Pager_Total %> <%=AspNetPager3.RecordCount  %> <%= Resources.Lang.WMS_Common_Pager_Count %></div>
                                                </li>
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>
