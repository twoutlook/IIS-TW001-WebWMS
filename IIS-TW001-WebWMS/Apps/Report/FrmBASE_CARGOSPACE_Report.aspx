<%@ Page Title="" Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master"
    AutoEventWireup="true" CodeFile="FrmBASE_CARGOSPACE_Report.aspx.cs" Inherits="Apps_Report_FrmBASE_CARGOSPACE_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DreamTek.ASRS.Business" Namespace="DreamTek.ASRS.Business" TagPrefix="HDTools" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.FrmReport_ReportManagement%>-&gt; <%=Resources.Lang.FrmBASE_CARGOSPACE_Report_MSG1%><%--储位明细报表--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                            height="11" alt="" runat="server" id="titleImg" />
                            <%=Resources.Lang.Common_JSCeria%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%=Resources.Lang.Common_Fold%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr style="display: none">
                        <td colspan="7">
                            <style type="text/css;">
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
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, Common_WareHouse %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, Common_PartNum %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                       <%-- <td class="InputLabel" style="width: 13%; display:none">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>" ></asp:Label>：
                        </td>
                        <td style="width: 20%; display:none">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>--%>
                          <td class="InputLabel" style="width: 13%">

                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：<%--状态：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                      
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACE_Report_MSG2 %>"></asp:Label>：<%--是否混放：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="95%">
                                <%--<asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click"></asp:Button>
                            &nbsp;                       
                            <HDTools:ExcelButton runat="server" GridID="grdINASN" ID="btnExcel" ExcelName=""></HDTools:ExcelButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdINASN" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="grdINASN_RowDataBound" CssClass="Grid" PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="ID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CWAREID" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_WareHouseCode %>"><%--仓库编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CWARENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_WareHouseName %>"></asp:BoundField>
                            <%--仓库名称--%>
                            <asp:BoundField DataField="Cpositioncode" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PartnumNO %>"><%--储位编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Cposition" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PartnumName %>"><%--储位名称--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="cstatus" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACE_Report_MSG3 %>"><%--储位状态--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="ipermitmix" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACE_Report_MSG2 %>"><%--是否混放--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IMAXCAPACITY" DataFormatString="{0:f}" HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG44 %>"><%--最大量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CALIAS" DataFormatString="" HeaderText="<%$ Resources:Lang, CommonB_calias %>"><%--助记码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CTYPE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACE_Report_MSG4 %>"><%--种类--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>

                        </Columns>
                    </asp:GridView>
                    <ul class="OneRowStyle">
                        <li>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                FirstPageText="<%$ Resources:Lang, Common_FirstPage %>" LastPageText="<%$ Resources:Lang, Common_LastPage %>" NextPageText="<%$ Resources:Lang, Common_NextPage %>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                            </webdiyer:AspNetPager>
                        </li>
                        <li>
                            <div><%=Resources.Lang.Common_TotalPage%> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
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
