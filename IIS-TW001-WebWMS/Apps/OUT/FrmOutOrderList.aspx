<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" Title="--订单管理" AutoEventWireup="true" CodeFile="FrmOutOrderList.aspx.cs" Inherits="Apps_OUT_FrmOutOrderList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />   
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <style type="text/css">
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
            height:100%;
        }
        .tableCell {
            display:table;
            width:100%;
        }
        .gridLineHeight {
            line-height:22px;
        }

        input[type='submit'][disabled],input[type='button'][disabled]
        {       
            opacity:0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70);/*兼容ie8及以下*/
        }
        
    </style>   
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt;<%= Resources.Lang.FrmOutOrderList_Menu_PageName %> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table id="TabMain" style="width: 95%">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"  height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.WMS_Common_SearchTitle %>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <img id="imgCollapse" alt="<%= Resources.Lang.WMS_Common_FoldAlt %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblOrderNo" runat="server" Text="<%$ Resources:Lang, FrmOutOrderList_OrderNo %>"></asp:Label>：<%--订单/客户订单编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtOrderNo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_OrderType %>"></asp:Label>：<%--订单类型--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="drpOrderType" runat="server" Width="95%" >
                            </asp:DropDownList>
                        
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSONCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_GridView_Status %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="drpStatus" runat="server" Width="95%" >
                            </asp:DropDownList>
                        </td>
                       
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_OrderDate %>"></asp:Label>：<%--订单日期--%>
                        </td>
                       <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtOrder_DateFrom" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtOrder_DateFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, WMS_Common_DateTo %>"></asp:Label>：
                        </td>
                         <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtOrder_DateTo" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtOrder_DateTo','y-mm-dd',0);" />
                        </td> 
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ClientCode %>"></asp:Label>：<%--客户编码：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCustomId" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ClientName %>"></asp:Label><%--客户名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCustomName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateOwner %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMETo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%"><asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang,Commona_TimePeriod %>"></asp:Label>：</td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:RadioButtonList ID="rbtList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" BorderStyle="None" CssClass="DateTypeRadio">                          
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                   
                    <tr>
                        <td colspan="6" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClientClick="return validate_DataTime('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','ctl00_ContentPlaceHolderMain_txtDAUDITDATEFrom','ctl00_ContentPlaceHolderMain_txtDAUDITDATETo','制单日期的第一个日期应该小于第二个日期!','审核日期的第一个日期应该小于第二个日期!');"
                                OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top" class="tableCell">
            <td valign="top" align="left" class="style1" style="padding:15px 0px;">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang,WMS_Common_Button_New %>" Enabled="true"></asp:Button>
                &nbsp;&nbsp;<asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" CssClass="ButtonDel" OnClientClick="return CheckDel();" Enabled="true" />
                &nbsp;&nbsp;<asp:Button ID="BtnRevoke" runat="server" CssClass="ButtonDel" Text="<%$ Resources:Lang,FrmOUTASNList_Button_Revoke %>" Visible="true" OnClick="BtnRevoke_Click"></asp:Button><%--撤销--%>
            </td>
        </tr>
        <tr class="tableCell">
            <td>
                <div style="min-height: 460px; width: 100%" id="DivScroll">
                    <asp:GridView ID="grd_Order" runat="server" AllowPaging="True" BorderColor="Teal" DataKeyNames="ID" BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="1" Width="100%" AutoGenerateColumns="False"  OnRowDataBound="grd_Order_RowDataBound" CssClass="Grid gridLineHeight" PageSize="15">
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
                            <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="ID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OrderNo" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_OrderCode %>"><%--订单编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CustomOrderNo" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_CustomOrderCode %>"><%--客户订单编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OrderType" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_OrderType %>"><%--订单类型--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OrderDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_OrderDate %>"><%--订单日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CustomId" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ClientCode %>"><%--客户编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CustomName" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ClientName %>"><%--客户名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Amount" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_AmountBeforeTax %>"><%--税前总额(元)--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SalesMan" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_SalesMan %>"><%--业务员名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="status" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Status %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" Text="<%$ Resources:Lang, WMS_Common_GridView_Edit %>">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
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
                            <div><%= Resources.Lang.WMS_Common_Pager_Total %> <%=AspNetPager1.RecordCount%> <%= Resources.Lang.WMS_Common_Pager_Count %></div>
                        </li>
                    </ul>
                </div>
            </td>
        </tr>       
    </table>

    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <%--<script src="../../Layout/Js/Help.js" type="text/javascript"></script>--%>
    <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js"></script>
    <script type="text/javascript">
        /* Date函数原型扩展 */
        Date.prototype.format = function (params) {
            /* 默认参数设置 */
            var params=params || {};
            var value=params.value || 'dateTime';
            var chinese=params.chinese || false;
            /* 是否个位数 */
            var digit=function (val) {
                return val >= 10 ? val : '0'+val;
            };
            /* 日期数据 */
            var data = {
                "Y": this.getFullYear(),
                "M": digit(this.getMonth() + 1),
                "D": digit(this.getDate()),
                "h": digit(this.getHours()),
                "m": digit(this.getMinutes()),
                "s": digit(this.getSeconds())
            };
            /* 字符拼接 */
            var joinStr={
                date:function(){
                    var txtArr=chinese ? ['年','月','日'] : ['-','-',''];
                    return data.Y+txtArr[0]+data.M+txtArr[1]+data.D+txtArr[2];
                },
                time:function(){
                    var txtArr=chinese ? ['时','分','秒'] : [':',':',''];
                    return data.h+txtArr[0]+data.m+txtArr[1]+data.s+txtArr[2];
                },
                dateTime:function () {
                    return this.date()+' '+this.time();
                }
            };
            /* 返回结果 */
            return joinStr[value]();
        };

        $(document).ready(function () {
            if ($("#<%=txtDCREATETIMEFrom.ClientID %>").val() == "" && $("#<%=txtDCREATETIMETo.ClientID %>").val() == "") {
                FormatSetDate1('<%=txtDCREATETIMEFrom.ClientID %>', '<%=txtDCREATETIMETo.ClientID %>');    //第一个参数是开始日期，第二个参数是结束日期
            }
            $("#<%=rbtList.ClientID %>").change(function () {
                FormatSetDate1('<%=txtDCREATETIMEFrom.ClientID %>', '<%=txtDCREATETIMETo.ClientID %>'); //第一个参数是开始日期，第二个参数是结束日期
            });
        });

        function CheckDel() {
            var number = 0;
            $.each($("#<%=grd_Order.ClientID%>").find("span input"), function (i, item) {
                if (item.checked == true) {
                    number = number + 1;
                }
            });
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

        //根据单选框选择的值系统自动赋值
        function FormatSetDate1(start, end) {
            var myDate = new Date();     //获取当前日期
            var SelectVal = $("input[type='radio']:checked").val();
            var currentDate = myDate.format({ value: 'date' });
            myDate.setDate(myDate.getDate() - SelectVal);
            var nextDate = myDate.format({ value: 'date' });
            if (start != null && start != 'undefined' && start != '') {
                $('#' + start).val(nextDate);
            }
            if (end != null && end != 'undefined') {
                $('#' + end).val(currentDate);
            }
        }
    </script>
</asp:Content>