﻿<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="<%$ Resources:Lang, FrmInbill_InbillCticketCode %>" CodeFile="FrmOUTBILLReport.aspx.cs" Inherits="FrmOUTBILLReport" %>

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
    <%=Resources.Lang.FrmReport_ReportManagement%>-&gt;<%=Resources.Lang.FrmOUTBILLReport_Content1%><%--出库单报表--%>
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
                            <asp:Label ID="lbcticketcode" runat="server" Text="<%$ Resources:Lang, FrmUnionPalletList_MSG4 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcticketcode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, Common_OutType %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlOutType" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, Common_WareHouse %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtWAREHOUSE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label5" runat="server" Text="ErpCode："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtErpCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, Common_PartNum %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCARGOSPACE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                       
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmInbill_WorkType %>"></asp:Label>：
                        </td>
                        <td style="width: 20%;">
                            <asp:DropDownList ID="ddlworktype" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPART" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                         <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label8" runat="server" Text="批/序號(RANK)"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtRANK_FINAL" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="1"></asp:TextBox>
                        </td>
                          
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>： <%--规格--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtcspec" runat="server"  CssClass="NormalInputText" Width="95%"></asp:TextBox>                          
                        </td>    
                        <td colspan="4"></td> 


                    
                      
                    </tr>
                    <tr>
                            <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="lblDINDATEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_DateTime %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDINDATEFrom" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="lblDINDATEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDINDATETo" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATETo','y-mm-dd',0);" />
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
                    <tr>
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
                            <asp:BoundField DataField="cticketcode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmUnionPalletList_MSG4 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="otype" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmJIANHUOReport_MSG4 %>"><%--出库类型--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="worktype" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_WorkType %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="ERP">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cso" DataFormatString="" HeaderText="SO">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CWAREID" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_WareHouseCode %>"><%--仓库--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PartNum %>"><%--储位--%>

                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>



                            <asp:BoundField DataField="RANK_FINAL" DataFormatString="" HeaderText="批/序號(RANK)" HtmlEncode="false"><%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>

                              <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>" ><%--规格--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="iquantity" DataFormatString="{0:f}" HeaderText="<%$ Resources:Lang, Common_NUM %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CCREATEOWNERCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CreateUser %>"><%--制单人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DCREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmINASN_Report_MSG5 %>"><%--制单日期--%>
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
