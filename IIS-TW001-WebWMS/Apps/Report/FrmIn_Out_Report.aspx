<%@ Page Title="<%$ Resources:Lang, FrmUnionPalletList_MSG2 %>" Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master"
    AutoEventWireup="true" CodeFile="FrmIn_Out_Report.aspx.cs" Inherits="Apps_Report_FrmIn_Out_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="DreamTek.ASRS.Business" namespace="DreamTek.ASRS.Business" tagprefix="HDTools" %>
<%@ Register Src="../BASE/ShowAREADiv.ascx" TagName="ShowAREADiv" TagPrefix="ucArea" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <!--
<script type="text/javascript" src="../../Layout/My97DatePicker/WdatePicker.js">
</script>
-->
    <!--
    <script type="text/javascript" src="../../Layout/Calendar/Calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
-->
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
        <style type="text/css">
                                span.requiredSign
                                {
                                    color: red;
                                    font-weight: bold;
                                    position: relative;
                                    left: -15px;
                                    top: 2px;
                                }
                            .auto-style1 {
                width: 22%;
            }
                            </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.FrmReport_ReportManagement%>-&gt;<%=Resources.Lang.FrmIn_Out_Report_MSG1%><%--进出明细报表--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
        <ucArea:ShowAREADiv ID="ucShowArea" runat="server" />
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
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
                        <td colspan="6">
                        
                        </td>
                    </tr>
                    <tr>
                        <%--<td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, Common_WareHouse %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtCK" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, Common_PartNum %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtCW" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>--%>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtLH" runat="server"  Width="95%" CssClass="NormalInputText"></asp:TextBox>          
                          <%--  <span class="requiredSign" >*</span>--%>
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
                        <td style="white-space: nowrap;" class="auto-style1">
                            <asp:TextBox ID="txtcspec" runat="server"  CssClass="NormalInputText" Width="95%"></asp:TextBox>                          
                        </td>    
                    </tr>

                    <tr>
                        
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_DateTime %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                        </td>
                        <td style=" white-space: nowrap;" >
                            <asp:TextBox ID="txtDCREATETIMETo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','y-mm-dd',0);" />
                        </td>
                        
                        <%--<td colspan="2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click">
                            </asp:Button>
                        </td>--%>
                    </tr>
                        <tr>
                        <td colspan="6" align="center" style="text-align: right;">
                            <asp:Button ID="Button1" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click">
                            </asp:Button>
                             &nbsp;                       
                            <HDTools:ExcelButton runat="server" GridID="grdINASN" ID="btnExcel" ExcelName=""></HDTools:ExcelButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
     <%--   <tr valign="top">
            <td valign="top">
                <cc1:DataGridNavigator3 ID="grdNavigatorINASN" runat="server" GridID="grdINASN" ShowPageNumber="false"
                    ExcelName="INASN" IsDbPager="True" OnGetExportToExcelSource="grdNavigatorINASN_GetExportToExcelSource" />
            </td>
        </tr>--%>
        <tr>
            <td>
                <div style="height: 500px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdINASN" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False" 
                        OnRowDataBound="grdINASN_RowDataBound"                 
                        CssClass="Grid" PageSize="15">
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
                      <%--      <asp:BoundField DataField="" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_WareHouseCode %>">仓库编号
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="" DataFormatString=""  HeaderText="<%$ Resources:Lang, Common_WareHouseName %>"></asp:BoundField>仓库名称
                            <asp:BoundField DataField="" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PartNum %>" Visible="false">储位编号
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PartnumName %>"  Visible="false">储位名称

                            </asp:BoundField>--%>
                            <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>



                            <asp:BoundField DataField="RANK_FINAL" DataFormatString="" HeaderText="批/序號(RANK)" HtmlEncode="false"><%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>

                             <asp:BoundField DataField="cspecifications" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>" ><%--规格--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="beginstocksummary" DataFormatString="{0:f}" HeaderText="<%$ Resources:Lang, FrmIn_Out_Report_initCount %>"><%--期初数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="insummary"  DataFormatString="{0:f}" HeaderText="<%$ Resources:Lang, FrmIn_Out_Report_inCount %>" /><%--期间入--%>
                            <asp:BoundField DataField="outsummary" DataFormatString="{0:f}" HeaderText="<%$ Resources:Lang, FrmIn_Out_Report_outCount %>" /><%--期间出--%>
                            <asp:BoundField DataField="endstocksunmary" DataFormatString="{0:f}" HeaderText="<%$ Resources:Lang, FrmIn_Out_Report_finalCount %>" /><%--期末数量--%>
                        </Columns>
                        
                    </asp:GridView>
                     <ul class="OneRowStyle">
                        <li>
                            <webdiyer:aspnetpager id="AspNetPager1" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="AspNetPager1_PageChanged"
                                firstpagetext="<%$ Resources:Lang, Common_FirstPage %>" lastpagetext="<%$ Resources:Lang, Common_LastPage %>" nextpagetext="<%$ Resources:Lang, Common_NextPage %>" prevpagetext="<%$ Resources:Lang, Common_PrePage %>" showpageindexbox="Never"
                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false">
                            </webdiyer:aspnetpager>
                        </li>
                        <li>
                            <div><%=Resources.Lang.Common_TotalPage%> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
                        </li>
                    </ul>
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdINASN.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                    </script>
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
