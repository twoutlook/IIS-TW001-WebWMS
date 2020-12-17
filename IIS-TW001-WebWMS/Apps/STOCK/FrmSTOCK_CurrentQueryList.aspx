<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" Title="--库存查询" CodeFile="FrmSTOCK_CurrentQueryList.aspx.cs" Inherits="FrmSTOCK_CurrentQueryList" %>

<%@ Register Assembly="DreamTek.ASRS.Business" Namespace="DreamTek.ASRS.Business" TagPrefix="HDTools" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <!---->
    <style type="text/css">
        .td {
            word-break: break-all;
            word-wrap: break-word;
            width: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Group_StockManage %>-&gt;<%= Resources.Lang.WMS_Common_Menu_StockSearch %><%--库存查询--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top" class="style1">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="tabCondition">
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
                    <tr>
                        <td class="InputLabel" style="width: 10%">
                            <asp:Label ID="lbCWAREHOUSECODE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CwareName %>"></asp:Label>：<%--仓库名称--%>
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="txtCWAREHOUSEName" runat="server" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 10%">
                            <asp:Label ID="lbCPOSITIONCODE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CpositionCode %>"></asp:Label>：<%--储位编码--%>
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="txtCPOSITIONCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 10%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, Common_Pallte %>"></asp:Label>：<%--栈板号--%>
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="txtPackgeNo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 10%">
                            <asp:Label ID="lbCINVCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"></asp:Label>：<%--料号--%>
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 10%">
                            <asp:Label ID="Label4" runat="server" Text="批/序號(RANK)"></asp:Label>：<%--批/序號(RANK)--%>
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="txtRank_Final" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="1"></asp:TextBox>
                        </td>


                        <td class="InputLabel" style="width: 10%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmSTOCK_CurrentQueryList_SnOrBox %>"></asp:Label>：<%--SN/箱号/栈板--%>
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="txtPalletCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
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
                            <asp:HiddenField ID="hfieldGUID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>： <%--规格--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtcspec" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 10%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmSTOCK_CurrentQueryList_AnyInPalletcode %>"></asp:Label>：<%--栈板上是否有货--%>
                        </td>
                        <td style="width: 20%">
                            <asp:RadioButtonList ID="rbtList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" BorderStyle="None">
                                <asp:ListItem Text="没有" Value="0"></asp:ListItem>
                                <asp:ListItem Text="有" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="全部" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td colspan="6" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                            &nbsp;<HDTools:ExcelButton runat="server" GridID="grdSTOCK_DURATION" ID="btnExcel" ExcelName="STOCK"></HDTools:ExcelButton>
                            &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang,WMS_Common_Button_Back %>" CausesValidation="false" OnClick="btnBack_Click" Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td>
                <div style="height: 470px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdSTOCK_DURATION" runat="server" AllowPaging="True" DataKeyNames="ID,IOCCUPYQTY,LOCKNUM"
                        BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%"
                        AutoGenerateColumns="False" OnRowDataBound="grdSTOCK_DURATION_RowDataBound"
                        CssClass="Grid" PageSize="15" ShowFooter="true">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:BoundField DataField="ID" DataFormatString="" HeaderText="ID" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>

                            <%--NOTE by Mark 10/22, Hide it for 2 reasons: 1.only one WH 2. easy to fit summary --%>
                            <asp:BoundField DataField="CWAREHOUSECODE"  Visible="False" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CwareCode %>"><%--仓库编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>


                            <asp:BoundField DataField="cwarename" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CwareName %>"><%--仓库名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CpositionCode %>"><%--储位编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CpositionName %>"><%--储位名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="palletcode" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_PalletCode %>"><%--栈板号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>

                            <%--NOTE by Mark 09/20--%>
                            <%--NOTE by Mark 10/22, It's time to hide it!--%>

                            <asp:BoundField DataField="CINVCODE" Visible="False" DataFormatString="" HeaderText="DB料號(過渡時期參考)"><%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                            </asp:BoundField>


                            <asp:BoundField DataField="PART" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"><%--PART--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RANK_FINAL" DataFormatString="" HeaderText="批/序號(RANK)"><%--RANK--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                            </asp:BoundField>



                            <asp:BoundField DataField="calias" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Calias %>"><%--助记码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>"><%--品名--%>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="left" Width="300px" CssClass="td" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"><%--规格--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CDATECODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmSTOCK_CurrentQueryList_MakeDate %>" Visible="False"><%--制造日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IQTY" DataFormatString="{0:F}" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Quantity %>"><%--数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang,WMS_Common_Element_ZhanYongQuantity %>"><%--占用数量--%>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlToIOCCUPYQTY_Info" ToolTip="<%$ Resources:Lang,FrmSTOCK_CurrentQueryList_ViewZhanYong %>" runat="server" DataFormatString="{0:F}"><%# Eval("IOCCUPYQTY") %></asp:HyperLink><%--查看上架指引占用详情--%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang,FrmSTOCK_CurrentQueryList_LockedQty %>"><%--锁定数量--%>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HL_DateCode_Info" ToolTip="<%$ Resources:Lang,FrmSTOCK_CurrentQueryList_ViewDateCodeDetail %>" runat="server" DataFormatString="{0:F}"><%# Eval("LOCKNUM")%></asp:HyperLink><%--查看DateCode锁定详情--%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                            </asp:TemplateField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang,FrmOUTASNList_YiChang %>" DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_MingXi %>" Text="<%$ Resources:Lang,FrmOUTASNList_ChaKang %>"><%--异常--%><%--明细--%><%--查看--%>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                        </Columns>

                    </asp:GridView>
                    <br />
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
