﻿<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="<%$ Resources:Lang, FrmInbill_InbillCticketCode %>" CodeFile="FrmPANDIANReport.aspx.cs" Inherits="FrmPANDIANReport" %>

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
    <%=Resources.Lang.FrmReport_ReportManagement%>-&gt;<%=Resources.Lang.FrmPANDIANReport_Content1%><%--盘点报表--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="tabCondition">
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
                    <tr>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, Common_CheckDiffTicketCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPDDH" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, Common_WareHouse %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, Common_PartNum %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                         <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label8" runat="server" Text="批/序號(RANK)"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtRANK_FINAL" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="1"></asp:TextBox>
                        </td>

                          <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmCHAYIReport_Label5 %>"></asp:Label>：<%--盘点单名称：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCHECKNAME" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                      
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCHECKTYPE" runat="server" Text="<%$ Resources:Lang, FrmCHAYIReport_lblCHECKTYPE %>"></asp:Label>：<%--盘点类型：--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:DropDownList ID="dplCHECKTYPE" runat="server" Width="95%">
                                <%--<asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="0">物理</asp:ListItem>
                                <asp:ListItem Value="1">循环</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                         <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="lblDINDATEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_CheckDiffDateTime %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDINDATEFrom" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="lblDINDATEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDINDATETo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATETo','y-mm-dd',0);" />
                            <span class="requiredSign" style="left: -50px"></span>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, Common_CreateUser %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                                                 <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMETo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','y-mm-dd',0);" />
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
                        </td>
                    </tr>
                    <tr style="display: none">
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSON" runat="server" Text="<%$ Resources:Lang, Common_UserOfApproval %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCAUDITPERSON" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITTIMEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_DateOfApproval %>"></asp:Label>：
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtDAUDITTIMEFrom" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITTIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITTIMEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtDAUDITTIMETo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITTIMETo','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr>
                          <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmInbill_WorkType %>"></asp:Label>：
                        </td>
                         <td style="width: 20%;">
                            <asp:DropDownList ID="ddlworktype" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                           <td colspan="6" align="center" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click"></asp:Button>
                            &nbsp;                       
                            <HDTools:ExcelButton runat="server" GridID="grdINBILL" ID="btnExcel" ExcelName=""></HDTools:ExcelButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdINBILL" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="grdINBILL_RowDataBound" CssClass="Grid" PageSize="15">
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
                            <asp:BoundField DataField="Cticketcode" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CheckDiffTicketCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CCHECKNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CheckDiffTicketName %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="checktype" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CheckDiffType %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="worktype" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_WorkType %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CWAREID" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_WareHouseCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PartnumNO %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Cinvcode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="150px" />
                            </asp:BoundField>


                            
                            <asp:BoundField DataField="RANK_FINAL" DataFormatString="" HeaderText="批/序號(RANK)" HtmlEncode="false"><%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="150px" />
                            </asp:BoundField>


                            <asp:BoundField DataField="Cinvname" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="iquantity" DataFormatString="{0:f}" HeaderText="<%$ Resources:Lang, Common_NUM %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DCHECKDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmPANDIANReport_DCHECKDATE %>"><%--盘点日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
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
