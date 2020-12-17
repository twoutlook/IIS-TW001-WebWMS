<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" Title="--盘点单" CodeFile="FrmSTOCK_InventoryCy.aspx.cs" Inherits="FrmSTOCK_InventoryCy" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"  runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Group_StockManage %>-&gt;<%= Resources.Lang.FrmSTOCK_InventoryCy_PageName %><%--循环盘点明细维护--%>  
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

                                .style1 {
                                    height: 25px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_PanDianCode %>"></asp:Label>：<%--盘点单号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIDS" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_PanDianName %>"></asp:Label>：<%--盘点单名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIDName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>： <%--规格--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtcspec" runat="server"  CssClass="NormalInputText" Width="95%"></asp:TextBox>                          
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"></asp:Label>：<%--料号--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblRANK_FINAL" runat="server" Text="批/序號(RANK)"></asp:Label>：<%--rank--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtRANK_FINAL" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="1"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>"></asp:Label>：<%--品名--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCinvName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CwareCode %>"></asp:Label>：<%--仓库编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCWare" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CpositionCode %>"></asp:Label>：<%--储位编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCposition" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang,WMS_Common_GridView_Status %>"></asp:Label><%--状态--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="lblDINDATEFromFrom" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_PanDianDate %>"></asp:Label>：<%--盘点日期--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDINDATEFrom" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%; white-space: nowrap;">
                            <asp:Label ID="lblDINDATEToTo" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：<%--到--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDINDATETo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATETo','y-mm-dd',0);" />
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

                                function CheckDel() {
                                    var number = 0;
                                    var controls = document.getElementById("<%=grdSTOCK_CHECKBILL.ClientID %>").getElementsByTagName("input");

                                    for (var i = 0; i < controls.length; i++) {
                                        var e = controls[i];
                                        if (e.type != "CheckBox") {
                                            if (e.checked == true) {
                                                number = number + 1;
                                            }
                                        }
                                    }
                                    if (number == 0) {                              
                                        alert("<%= Resources.Lang.WMS_Common_DeleteTips %>");//请选择需要删除的项！
                                        return false;
                                    }
                                    if (confirm("<%= Resources.Lang.WMS_Common_DeleteConfirm %>")) {//你确认删除吗？
                                        return true;
                                    }
                                    else {
                                        return false;
                                    }
                                }


                            </script>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" class="style1">&nbsp;
                            <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" CssClass="ButtonDel" OnClientClick="return CheckDel()" />
                            &nbsp;
                            &nbsp;
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdSTOCK_CHECKBILL" runat="server" AllowPaging="True"
                        BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%"
                        AutoGenerateColumns="False" OnPageIndexChanged="grdSTOCK_CHECKBILL_PageIndexChanged"
                        OnPageIndexChanging="grdSTOCK_CHECKBILL_PageIndexChanging"
                        OnRowDataBound="grdSTOCK_CHECKBILL_RowDataBound" CssClass="Grid"
                        PageSize="15" DataKeyNames="ID">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:TemplateField HeaderText="<input id='chkSelect' type='checkbox' onclick='SelectAll4Grid(this)' />">
                                <ControlStyle BorderWidth="0px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid"
                                        BorderWidth="0px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" DataFormatString="" HeaderText="ID">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CHECKBILL_D_IDS" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_PanDianCode %>"><%--盘点单号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CCHECKNAME" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_PanDianName %>"><%--盘点单名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CWAREID" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CwareCode %>"><%--仓库编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CpositionCode %>"><%--储位编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"><%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="150px" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="150px" />
                            </asp:BoundField>
                            <%-- 以下 21-10-2020 by Qamar --%>
                            <asp:TemplateField HeaderText="批/序號(RANK)"><%--rank--%>
                                <ItemTemplate>
                                    <asp:Label ID="lblRANK_FINAL" runat="server" Text="">
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="40px"/>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <%-- 以上 21-10-2020 by Qamar --%>
                            <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>"><%--品名--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="250px" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="250px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"><%--规格--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IQUANTITY" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Quantity %>"><%--数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DCHECKDATE" DataFormatString="{0:yyyy-MM-dd}" HeaderText="<%$ Resources:Lang,WMS_Common_Element_PanDianDate %>"><%--盘点日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cstatusName" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_GridView_Status %>"><%--状态--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
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
