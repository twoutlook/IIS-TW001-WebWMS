<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG1 %>" CodeFile="FrmALLOCATE_DReport.aspx.cs" Inherits="FrmALLOCATE_DReport" %>
<%--調撥單--%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="DreamTek.ASRS.Business" namespace="DreamTek.ASRS.Business" tagprefix="HDTools" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.FrmReport_ReportManagement%>-&gt;<%=Resources.Lang.FrmALLOCATE_DReport_MSG1%><%--调拨单明细报表--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="titleImg" />
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
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG7%>"></asp:Label>：<%--单据号：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDINDATEFromFrom" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG3 %>"></asp:Label>：<%--调拨日期：--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtDINDATEFrom" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDINDATEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDINDATETo" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATETo','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, Common_CreateUser %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMETo" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSON" runat="server" Text="<%$ Resources:Lang, Common_UserOfApproval %>"></asp:Label>：
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCAUDITPERSON" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITTIMEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_DateOfApproval %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDAUDITTIMEFrom" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITTIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITTIMEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDAUDITTIMETo" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITTIMETo','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle15 %>"></asp:Label>：<%--状态：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%">
                               <%--<asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="0">未处理</asp:ListItem>
                                <asp:ListItem Value="4">已确认</asp:ListItem>
                                <asp:ListItem Value="1">已审核</asp:ListItem>
                                <asp:ListItem Value="5">調撥中</asp:ListItem>
                                <asp:ListItem Value="6">調撥完成</asp:ListItem>
                                <asp:ListItem Value="2">已完成</asp:ListItem> 
                                <asp:ListItem Value="3">已抛砖</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                           <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG4 %>"></asp:Label>：<%--ERP单号：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtERP" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
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

                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtLH" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
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
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click">
                            </asp:Button>
                            	  &nbsp;                       
                            <HDTools:ExcelButton runat="server" GridID="grdALLOCATE" ID="btnExcel" ExcelName=""></HDTools:ExcelButton>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdALLOCATE" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="grdALLOCATE_RowDataBound" CssClass="Grid" PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:BoundField DataField="CTICKETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG7%>"><%--单据号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="workTypeName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_WorkType %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG4 %>"><%--ERP单号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG5 %>"><%--料號--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>


                            <asp:BoundField DataField="RANK_FINAL" DataFormatString="" HeaderText="批/序號(RANK)" HtmlEncode="false"><%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>


                             <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False"/>
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px"/>
                            </asp:BoundField>
                               <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>" ><%--规格--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="IQUANTITY" DataFormatString="{0:f}" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG6 %>"><%--數量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG7 %>"><%--原儲位--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                               <asp:BoundField DataField="CPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG8 %>"><%--原儲位名稱--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CTOPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG9 %>"><%--目的儲位--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                               <asp:BoundField DataField="CTOPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG10 %>"><%--目的儲位名稱--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CINPERSONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG11 %>"><%--調撥人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="DINDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG12 %>"><%--調撥日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                         
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
