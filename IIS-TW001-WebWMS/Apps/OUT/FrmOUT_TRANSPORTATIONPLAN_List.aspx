<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" ResponseEncoding="gb2312"
    AutoEventWireup="true" Title="--出运计划" CodeFile="FrmOUT_TRANSPORTATIONPLAN_List.aspx.cs" Inherits="FrmOUT_TRANSPORTATIONPLAN_List" %>

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
    <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt;<%= Resources.Lang.FrmOUT_TRANPLAN_List_Menu_PageName %>
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
                            <asp:Label ID="lblDOCNO" runat="server" Text="<%$ Resources:Lang,FrmOUT_TRANPLAN_List_DOCNO %>"></asp:Label>：<%--出运计划单号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDOCNO" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang,FrmOUT_TRANPLAN_List_CERPCODE %>"></asp:Label>：<%--销售单号：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblTRANSTYPE" runat="server" Text="<%$ Resources:Lang,FrmOUT_TRANPLAN_List_TRANSTYPE %>"></asp:Label>：<%--运输方式：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtTRANSTYPE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblREQDELIVERYDATEFrom" runat="server" Text="<%$ Resources:Lang,FrmOUT_TRANPLAN_List_JiaoHuoRi %>"></asp:Label>：<%--交货日期：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtREQDELIVERYDATEFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtREQDELIVERYDATEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblREQDELIVERYDATETo" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtREQDELIVERYDATETo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtREQDELIVERYDATETo','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="lblCUSTOMERNO" runat="server" Text="<%$ Resources:Lang, FrmOUT_TRANPLAN_List_CustomOrderNo %>"></asp:Label>：<%--客户订单号：--%>
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCUSTOMERNO" runat="server"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblPRODUCETIMEFrom" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtPRODUCETIMEFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtPRODUCETIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblPRODUCETIMETo" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtPRODUCETIMETo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtPRODUCETIMETo','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="lblCVENDERCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ClientCode %>"></asp:Label>：<%--客户代码：--%>
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCVENDERCODE" runat="server"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="lblCINVCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cpartnumber %>"></asp:Label>：
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCINVCODE" runat="server"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, WMS_Common_GridView_Status %>"></asp:Label>：</td>
                        <td style="width: 20%;">
                            <asp:DropDownList ID="ddlIS_Sign" runat="server" Width="95%">
                                <%--<asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="0">已确认</asp:ListItem>
                                <asp:ListItem Value="1">未确认</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%;"></td>
                        <td style="width: 20%;"></td>
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
            <td colspan="6" align="left">&nbsp;&nbsp;
             <asp:Button ID="btnNew" runat="server" CssClass="ButtonMerge" Text="<%$ Resources:Lang,FrmOUT_TRANPLAN_List_MergeConfirm %>" OnClick="btnNew_Click"></asp:Button><%--确认合并--%>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left">&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdOUTTRANSPORTATIONPLAN" runat="server" AllowPaging="True" BorderColor="Teal" DataKeyNames="ID,DOCNO"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"
                        OnRowDataBound="grdOUTTRANSPORTATIONPLAN_RowDataBound"
                        OnPageIndexChanged="grdOUTTRANSPORTATIONPLAN_PageIndexChanged"
                        OnPageIndexChanging="grdOUTTRANSPORTATIONPLAN_PageIndexChanging"
                        OnDataBinding="grdOUTTRANSPORTATIONPLAN_DataBinding"
                        AllowSorting="true" CssClass="Grid" PageSize="15">
                        <PagerSettings Mode="NumericFirstLast" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <Columns>
                            <asp:TemplateField>
                                <ControlStyle BorderWidth="0px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid"
                                        BorderWidth="0px" onclick="SignCheck(this);" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DOCNO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUT_TRANPLAN_List_DOCNO %>"><%--出运计划单据号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TRANSTYPE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUT_TRANPLAN_List_TRANSTYPE %>"><%--运输方式--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUT_TRANPLAN_List_CERPCODE %>"><%--销售单号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODELINE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUT_TRANPLAN_List_CERPCODELINE %>"><%--销售单行号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cpartnumber %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IQUANTITY" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Quantity %>"><%--数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CUSTOMERNO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUT_TRANPLAN_List_CustomOrderNo %>"><%--客户订单号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Issign" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Status %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SINGNOWNER" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ConfirmUser %>"><%--确认人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SINGTIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ConfirmDate %>"><%--确认日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="createtime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang,WMS_Common_CreateDateFrom %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REQDELIVERYDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang,FrmOUT_TRANPLAN_List_JiaoHuoRi %>"><%--交货日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
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

            function CheckDel() {
                var number = 0;
                var controls = document.getElementById("<%=grdOUTTRANSPORTATIONPLAN.ClientID %>").getElementsByTagName("input");

                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
                if (number == 0) {
                    alert("<%= Resources.Lang.WMS_Common_DeleteTips %>");
                    return false;
                }
                if (confirm("<%= Resources.Lang.WMS_Common_DeleteConfirm %>")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            function CheckClose() {
                var number = 0;
                var controls = document.getElementById("<%=grdOUTTRANSPORTATIONPLAN.ClientID %>").getElementsByTagName("input");

                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmOUT_TRANPLAN_List_SelectOption %>");//请选择需要结案的项！
                    return false;
                }
                if (confirm("<%= Resources.Lang.FrmOUT_TRANPLAN_List_JieAnConfirm %>")) {//你确认结案吗？
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
    </table>
</asp:Content>
