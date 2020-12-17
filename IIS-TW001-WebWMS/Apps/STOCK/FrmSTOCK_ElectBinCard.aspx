<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" Title="--电子Bin卡" CodeFile="FrmSTOCK_ElectBinCard.aspx.cs" Inherits="FrmSTOCK_ElectBinCard" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Group_StockManage %>-&gt;<%= Resources.Lang.FrmSTOCK_ElectBinCard_PageName %><%--电子Bin卡--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top" class="style1">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="titleImg" />
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
                            <asp:Label ID="lblCINVCODE" runat="server" Text="<%$ Resources:Lang,FrmSTOCK_ElectBinCard_LingJianHao %>"></asp:Label>：<%--零件号--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CpositionCode %>"></asp:Label>：<%--储位编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPOSITIONCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CwareName %>"></asp:Label>：<%--仓库名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCWARENAME" runat="server" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCINVCODE0" runat="server" Text="<%$ Resources:Lang,FrmSTOCK_ElectBinCard_ZhiLing %>"></asp:Label>：<%--生产制令--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSNCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCINVCODE1" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_Furnaceno %>"></asp:Label>：<%--材料炉号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtFurnace" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang,FrmSTOCK_ElectBinCard_Supplier %>"></asp:Label></td>
                        :<%--供应商--%>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtVENDOR" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCINVCODE2" runat="server" Text="<%$ Resources:Lang,FrmSTOCK_ElectBinCard_ChanPinCode %>"></asp:Label>：<%--产品编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPROCODE" runat="server" CssClass="NormalInputText"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCINVCODE3" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_WuLiaoType %>"></asp:Label>：<%--物料类型--%>
                        </td>
                        <td style="width: 20%">

                            <asp:DropDownList ID="ddlWorkType" runat="server" Width="95%">
                                <asp:ListItem Value="0">请选择</asp:ListItem>
                                <%--请选择--%>
                                <asp:ListItem Value="1">良品</asp:ListItem>
                                <asp:ListItem Value="2">不良品</asp:ListItem>
                                <asp:ListItem Value="3">其它用处</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCINVCODE4" runat="server" Text="<%$ Resources:Lang,FrmSTOCK_ElectBinCard_GuiGe %>"></asp:Label>：<%--规格--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCATIONS" runat="server" CssClass="NormalInputText"
                                Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_MarkDate %>"></asp:Label>：<%--生产日期--%>
                        </td>
                        <td style="width: 21%;">
                            <asp:TextBox ID="txtDATECODE" runat="server" CssClass="NormalInputText" Style="width: 75%;"></asp:TextBox>
                            <img border="0" runat="server" id="imgDINDATE" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif"
                                onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDATECODE','y-mm-dd',0);" />
                        </td>

                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：<%--到--%>
                        </td>
                        <td style="width: 21%;">
                            <asp:TextBox ID="txtDATECODE2" runat="server" CssClass="NormalInputText" Style="width: 75%;"></asp:TextBox>
                            <img border="0" runat="server" id="img1" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif"
                                onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDATECODE2','y-mm-dd',0);" />
                        </td>

                        <td colspan="2"></td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="6">
                            <script type="text/javascript" language="javascript">
                                //调整“行”为不能换行的。为什么不在设计期就设置其样式为不换行的呢，因为一旦设置，输入控件莫名其妙地看不见了。
                                function ChangeTDStyle(inputTableID) {
                                    var tabMain = document.getElementById(inputTableID);
                                    if (tabMain == null) return;
                                    for (var i = 0; i < tabMain.rows.length; i++) {
                                        var tr = tabMain.rows[i];
                                        if (tr == null) continue;
                                        for (var j = 0; j < tr.cells.length; j++) {
                                            var td = tr.cells[j];
                                            if (td == null) continue;
                                            if (td.className == "" || td.className == null) {
                                                td.style.whiteSpace = "nowrap";
                                                td.style.borderRightWidth = "0px";
                                            }
                                        }
                                    }
                                }
                                ChangeTDStyle("ctl00_ContentPlaceHolderMain_TabMain");
                            </script>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdSTOCK_BinCard" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnPageIndexChanged="grdSTOCK_BinCard_PageIndexChanged" OnPageIndexChanging="grdSTOCK_BinCard_PageIndexChanging"
                        OnRowDataBound="grdSTOCK_BinCard_RowDataBound" CssClass="Grid" PageSize="15" ShowFooter="True">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmSTOCK_ElectBinCard_LingJianHao %>"><%--零件号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="qty" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Quantity %>"><%--数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CWARENAME" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CwareName %>"><%--仓库名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CpositionCode %>"><%--储位编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sncode" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmSTOCK_ElectBinCard_ZhiLing %>"><%--生产制令--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="furnaceno" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Furnaceno %>"><%--材料炉号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCTCODE" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmSTOCK_ElectBinCard_ChanPinCode %>"><%--产品编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cvendor" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmSTOCK_ElectBinCard_Supplier %>"><%--供应商--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmSTOCK_ElectBinCard_GuiGe %>"><%--规格--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="inBillTypeName" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_WuLiaoType %>"><%--物料类型--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DATECODE" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_MarkDate %>"><%--生产日期--%>
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
        <script type="text/javascript">
            function ChangeDivWidth() {
                document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
            }
            window.onresize = ChangeDivWidth;
            ChangeDivWidth();
        </script>
    </table>
</asp:Content>
