<%@ Page Title="条码管理" Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="FrmBar_SNManagement.aspx.cs" Inherits="Apps_BAR_FrmBar_SNManagement" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/Css/weui.css" />
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <style type="text/css">
        .lib_tabborder {
            border: 1px solid #D5E3F0;
        }

        .lib_Menubox {
            height: 28px;
            line-height: 28px;
            position: relative;
        }

            .lib_Menubox ul {
                margin: 0px;
                padding: 0px;
                list-style: none;
                position: absolute;
                top: 3px;
                left: 0;
                margin-left: 10px;
                height: 25px;
                text-align: center;
                width: 1100px;
            }

            .lib_Menubox li {
                float: left;
                display: block;
                cursor: pointer;
                width: 115px;
                color: #949694;
                font-weight: bold;
                margin-right: 2px;
                height: 25px;
                line-height: 25px;
                background-color: #F0F3FA;
            }

                .lib_Menubox li.hover {
                    padding: 0px;
                    background: #fff;
                    width: 115px;
                    border-left: 1px solid #95C9E1;
                    border-top: 1px solid #95C9E1;
                    border-right: 1px solid #95C9E1;
                    color: #739242;
                    height: 25px;
                    line-height: 25px;
                }

        .lib_Contentbox {
            clear: both;
            margin-top: 0px;
            border-top: none;
            min-height: 50px;
            text-align: center;
            padding-top: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBar_SNManagement_Title %>-&gt;<asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="lib_Tab1" style="height: 100%; width: 95%">
        <asp:HiddenField ID="hTabIndex" runat="server" Value="1" />
        <div class="lib_Menubox lib_tabborder">
            <ul>
                <li id="one1" onclick="setTab('one',1,3)" class="hover"><span id="spRule1" runat="server"></span></li>
                <li id="one2" onclick="setTab('one',2,3)"><span id="spRule2" runat="server"></span></li>
                <li id="one3" onclick="setTab('one',3,3)"><span id="spRule3" runat="server"></span></li>
            </ul>
        </div>
        <div class="lib_Contentbox lib_tabborder">
            <%--********************************************************************Pcs条码规则****************************************************************--%>
            <div id="con_one_1">
                <table id="TabRule" style="width: 100%">
                    <tr valign="top">
                        <td>
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="Table1">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="Img1" />
                                        <%= Resources.Lang.WMS_Common_SearchTitle %> 
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <img id="img2" alt="<%= Resources.Lang.WMS_Common_FoldAlt %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center" onclick="CollapseCondition('../../');return false;" />
                                    </th>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang,FrmBar_SNManagement_RuleCode %>"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtRuleCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%;">
                                        <asp:Label runat="server" ID="Label3" Text="<%$ Resources:Lang,FrmBar_SNManagement_RuleName %>"></asp:Label>
                                    </td>
                                    <td style="width: 20%;">
                                        <asp:TextBox ID="txtRuleName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label runat="server" ID="Label4" Text="<%$ Resources:Lang,FrmBar_SNManagement_RuleStatus %>"></asp:Label>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:DropDownList ID="drpRuleStatus" runat="server" Width="95%">
                                        <%--    <asp:ListItem Value="">全部</asp:ListItem>
                                            <asp:ListItem Value="1">启用</asp:ListItem>
                                            <asp:ListItem Value="0">未启用</asp:ListItem>
                                            <asp:ListItem Value="2">作废</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblDINDATEFrom" runat="server" Text="<%$ Resources:Lang,WMS_Common_CreateDateFrom %>"></asp:Label>：
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:TextBox ID="txtCreateDateFrom" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateDateFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblDINDATEToTo" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:TextBox ID="txtCreateDateTo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateDateTo','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCreateUser" runat="server" Text="<%$ Resources:Lang,WMS_Common_CreateUser %>"></asp:Label>：
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCreateUser" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang,WMS_Common_EnableDateFrom %>"></asp:Label>：
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:TextBox ID="txtEnableDateFrom" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtEnableDateFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:TextBox ID="txtEnableDateTo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtEnableDateTo','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang,WMS_Common_EnableUser %>"></asp:Label>：
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtEnableUser" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="text-align: right;">
                                        <asp:Button ID="btnSearchRule" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, WMS_Common_Button_Search %>" OnClick="btnSearchRule_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left" colspan="6" style="padding: 15px 0px;">
                            <asp:Button ID="btnNewRule" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, WMS_Common_Button_New %>"></asp:Button>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnEnableRule" runat="server" Text="<%$ Resources:Lang, WMS_Common_Button_Enable %>" CssClass="ButtonDo" OnClick="btnEnableRule_Click" OnClientClick="return CheckSelect();" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnDisableRule" runat="server" Text="<%$ Resources:Lang, WMS_Common_Button_Disable %>" CssClass="ButtonClose" OnClientClick="return CheckSelect();" OnClick="btnDisableRule_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <div style="height: 500px; overflow-x: scroll; width: 100%">
                                <asp:GridView ID="grdSNRule" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="true"
                                    OnRowDataBound="grdSNRule_RowDataBound" CssClass="Grid" PageSize="15">
                                    <PagerSettings Visible="False" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <RowStyle HorizontalAlign="Left" Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
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
                                        <asp:BoundField DataField="ID" DataFormatString="" HeaderText="" Visible="false">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RULECODE" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmBar_SNManagement_RuleCode %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RULENAME" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmBar_SNManagement_RuleName %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RULELEN" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmBar_SNManagement_RuleLength %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="STATUS" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmBar_SNManagement_RuleStatus %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CREATEDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang,WMS_Common_CreateTimeFrom %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CREATEUSER" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_CreateUser %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ENABLEDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang,WMS_Common_EnableTimeFrom %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ENABLEUSER" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_EnableUser %>">
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
                                        <webdiyer:aspnetpager id="AspNetPager2" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="AspNetPager2_PageChanged"
                                            firstpagetext="<%$ Resources:Lang, WMS_Common_Pager_First %>" lastpagetext="<%$ Resources:Lang, WMS_Common_Pager_Last %>" nextpagetext="<%$ Resources:Lang, WMS_Common_Pager_Next %>" prevpagetext="<%$ Resources:Lang, WMS_Common_Pager_Front %>" showpageindexbox="Never"
                                            alwaysshow="true" reverseurlpageindex="True" urlpaging="false" xmlns:webdiyer="wuqi.webdiyer">
                                        </webdiyer:aspnetpager>
                                    </li>
                                    <li>
                                        <div><%= Resources.Lang.WMS_Common_Pager_Total %> <%=AspNetPager2.RecordCount  %> <%= Resources.Lang.WMS_Common_Pager_Count %></div>
                                    </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <%--********************************************************************Pcs条码维护****************************************************************--%>
            <div id="con_one_3" style="display: none">
                <table id="TabMain" style="height: 100%; width: 100%">
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
                                        <img id="imgCollapse" alt="<%= Resources.Lang.WMS_Common_FoldAlt %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                            onclick="CollapseCondition('../../');return false;" />
                                    </th>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCNAME" runat="server" Text="<%$ Resources:Lang,FrmBar_SNManagement_SNCode %>"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtSN" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>

                                    <td class="InputLabel" style="width: 10%">
                                        <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang,WMS_Common_CreateDateFrom %>"></asp:Label>：
                                    </td>
                                    <td style="width: 15%; white-space: nowrap;">
                                        <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="absmiddle" alt="" style="position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 10%">
                                        <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
                                    </td>
                                    <td style="width: 15%; white-space: nowrap;">
                                        <asp:TextBox ID="txtDCREATETIMETo" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="absmiddle" alt="" style="position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','y-mm-dd',0);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang,WMS_Common_CreateUser %>"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtCreateOwner" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td colspan="4" style="text-align: right;">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                                        <input id="btnExportToExcel" class="ButtonExcel" type="button" value="<%= Resources.Lang.WMS_Common_Button_Export %>" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" align="left">
                            <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, WMS_Common_Button_New %>"></asp:Button>
                            &nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Lang, WMS_Common_Button_Delete %>" CssClass="ButtonDel" OnClientClick="return CheckDel();" OnClick="btnDelete_Click" />
                            &nbsp;&nbsp;<asp:Button ID="btnprint" runat="server" Text="<%$ Resources:Lang, WMS_Common_Button_Print %>" CssClass=" ButtonPrint" OnClick="btnprint_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 500px; overflow-x: scroll; width: 100%" id="DivScroll">
                                <asp:GridView ID="grdSNBar" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    ShowHeader="True" CssClass="Grid" PageSize="100" OnRowDataBound="grdSNBar_RowDataBound">
                                    <PagerSettings Visible="False" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <RowStyle HorizontalAlign="Left" Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="<input id='chkSelect' type='checkbox' onclick='SelectAll4Grid(this)' />" ItemStyle-Width="3%">
                                            <ControlStyle BorderWidth="0px" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid"
                                                    BorderWidth="0px" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkSelect0" runat="server" BorderStyle="Solid" BorderWidth="0px" />
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" DataFormatString="" HeaderText="" Visible="false">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="" ItemStyle-Width="30%" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCodeRuleId" runat="server" Width="95%" Text='<%# Eval("CodeRuleId") %>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang,FrmBar_SNManagement_SNCode %>" ItemStyle-Width="30%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSN" runat="server" Width="95%" Text='<%# Eval("SN_CODE") %>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CREATE_OWNER" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_CreateUser %>" ItemStyle-Width="5%">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CREATE_TIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang,WMS_Common_CreateTimeFrom %>" ItemStyle-Width="8%">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" DataTextField=""
                                            DataTextFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" Text="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" ItemStyle-Width="5%">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                </asp:GridView>
                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                            FirstPageText="<%$ Resources:Lang, WMS_Common_Pager_First %>" LastPageText="<%$ Resources:Lang, WMS_Common_Pager_Last %>" NextPageText="<%$ Resources:Lang, WMS_Common_Pager_Next %>" PrevPageText="<%$ Resources:Lang, WMS_Common_Pager_Front %>" ShowPageIndexBox="Never"
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
                </table>
            </div>
            <%--********************************************************************Pcs条码维护****************************************************************--%>
            <div id="con_one_2" style="display: none">
                <table id="Table2" style="width: 100%">
                    <tr valign="top">
                        <td>
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="Table3">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="Img3" />
                                        <%= Resources.Lang.WMS_Common_SearchTitle %> 
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <img id="img4" alt="<%= Resources.Lang.WMS_Common_FoldAlt %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center" onclick="CollapseCondition('../../');return false;" />
                                    </th>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmBar_SNManagement_PrintCode %>"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtPrintCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%;">
                                        <asp:Label runat="server" ID="Label10" Text="<%$ Resources:Lang, FrmBar_SNManagement_PrintName %>"></asp:Label>
                                    </td>
                                    <td style="width: 20%;">
                                        <asp:TextBox ID="txtPrintName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label runat="server" ID="Label11" Text="<%$ Resources:Lang, FrmBar_SNManagement_RuleStatus %>"></asp:Label>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:DropDownList ID="drpPrintStatus" runat="server" Width="95%">
                                          <%--  <asp:ListItem Value="">全部</asp:ListItem>
                                            <asp:ListItem Value="1">启用</asp:ListItem>
                                            <asp:ListItem Value="0">未启用</asp:ListItem>
                                            <asp:ListItem Value="2">作废</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Lang, WMS_Common_CreateDateFrom %>"></asp:Label>：
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:TextBox ID="txtPrintCreateFrom" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtPrintCreateFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:TextBox ID="txtPrintCreateTo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtPrintCreateTo','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Lang,WMS_Common_CreateUser %>"></asp:Label>：
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtPrintCreate" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="text-align: right;">
                                        <asp:Button ID="btnSearchPrint" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, WMS_Common_Button_Search %>" OnClick="btnSearchPrint_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left" colspan="6" style="padding: 15px 0px;">
                            <asp:Button ID="btnNewPrint" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, WMS_Common_Button_New %>"></asp:Button>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnEnablePrint" runat="server" Text="<%$ Resources:Lang, WMS_Common_Button_Enable %>" CssClass="ButtonDo" OnClick="btnEnablePrint_Click" OnClientClick="return CheckPrintEnable();" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnDisablePrint" runat="server" Text="<%$ Resources:Lang, WMS_Common_Button_Disable %>" CssClass="ButtonClose" OnClick="btnDisablePrint_Click" OnClientClick="return CheckPrintDisable();" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <div style="height: 500px; overflow-x: scroll; width: 100%">
                                <asp:GridView ID="grdSNPrint" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="true"
                                    OnRowDataBound="grdSNPrint_RowDataBound" CssClass="Grid" PageSize="15">
                                    <PagerSettings Visible="False" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <RowStyle HorizontalAlign="Left" Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
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
                                        <asp:BoundField DataField="ID" DataFormatString="" HeaderText="" Visible="false">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PrintCode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBar_SNManagement_PrintCode %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PrintName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBar_SNManagement_PrintName %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Cstatus" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Status %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CreateTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang,WMS_Common_CreateTimeFrom %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CreateUser" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_CreateUser %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EnableTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang,WMS_Common_EnableTimeFrom %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EnableUser" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_EnableUser %>">
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
                                        <webdiyer:aspnetpager id="AspNetPager3" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="AspNetPager3_PageChanged"
                                            firstpagetext="<%$ Resources:Lang, WMS_Common_Pager_First %>" lastpagetext="<%$ Resources:Lang, WMS_Common_Pager_Last %>" nextpagetext="<%$ Resources:Lang, WMS_Common_Pager_Next %>" prevpagetext="<%$ Resources:Lang, WMS_Common_Pager_Front %>" showpageindexbox="Never"
                                            alwaysshow="true" reverseurlpageindex="True" urlpaging="false" xmlns:webdiyer="wuqi.webdiyer">
                                        </webdiyer:aspnetpager>
                                    </li>
                                    <li>
                                        <div><%= Resources.Lang.WMS_Common_Pager_Total %> <%=AspNetPager3.RecordCount  %> <%= Resources.Lang.WMS_Common_Pager_Count %></div>
                                    </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <!-- loading toast -->
    <div id="loadingToast" class="im_loading_toast" style="display: none;">
        <div class="im_mask_transparent"></div>
        <div class="im_toast">
            <div class="im_loading">
                <div class="im_loading_leaf im_loading_leaf_0"></div>
                <div class="im_loading_leaf im_loading_leaf_1"></div>
                <div class="im_loading_leaf im_loading_leaf_2"></div>
                <div class="im_loading_leaf im_loading_leaf_3"></div>
                <div class="im_loading_leaf im_loading_leaf_4"></div>
                <div class="im_loading_leaf im_loading_leaf_5"></div>
                <div class="im_loading_leaf im_loading_leaf_6"></div>
                <div class="im_loading_leaf im_loading_leaf_7"></div>
                <div class="im_loading_leaf im_loading_leaf_8"></div>
                <div class="im_loading_leaf im_loading_leaf_9"></div>
                <div class="im_loading_leaf im_loading_leaf_10"></div>
                <div class="im_loading_leaf im_loading_leaf_11"></div>
            </div>
            <p class="im_toast_content"><%= Resources.Lang.WMS_Common_Element_DataLoading %></p>
        </div>
    </div>
    <input type="hidden" id="hiddBarCodeType" runat="server" />

    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/Js/Help.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js"> </script>
    <script type="text/javascript" src="../../Layout/Js/jquery.json-2.2.min.js"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var indexT = document.getElementById("ctl00_ContentPlaceHolderMain_hTabIndex").value;
            document.getElementById("one" + indexT).click();
        }
        function setTab(name, cursel, n) {
            document.getElementById("ctl00_ContentPlaceHolderMain_hTabIndex").value = cursel;
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                var con = document.getElementById("con_" + name + "_" + i);
                menu.className = i == cursel ? "hover" : "";
                con.style.display = i == cursel ? "block" : "none";
            }
        }

        function CheckPrintEnable() {
            var number = 0;
            $.each($("#<%=grdSNPrint.ClientID%>").find("span input"), function (i, item) {
                if (item.checked == true) {
                    number = number + 1;
                }
            });
            if (number == 0) {
                alert("<%= Resources.Lang.FrmBar_SNManagement_EnableTips %>");
                return false;
            }
            if (confirm("<%= Resources.Lang.FrmBar_SNManagement_EnableConfirm %>")) {
                return true;
            } else {
                return false;
            }
        }

        function CheckPrintDisable() {
            var number = 0;
            $.each($("#<%=grdSNPrint.ClientID%>").find("span input"), function (i, item) {
                if (item.checked == true) {
                    number = number + 1;
                }
            });
            if (number == 0) {
                alert("<%= Resources.Lang.FrmBar_SNManagement_ZuofeiTips %>");
                return false;
            }
            if (confirm("<%= Resources.Lang.FrmBar_SNManagement_ZuofeiConfirm %>")) {
                return true;
            } else {
                return false;
            }
        }
        function CheckDel() {
            var number = 0;          
             $.each($("#<%=grdSNBar.ClientID%>").find("span input"), function (i, item) {
                 if (item.checked == true) {
                     number = number + 1;
                 }
             });
             if (number == 0) {
                 alert("<%=Resources.Lang.FrmDispatchUnitList_MsgTitle8 %>");
                    return false;
                }
                if (confirm("<%= Resources.Lang.FrmDispatchUnitList_MsgTitle9 %>")) {
                 return true;
             }
             else {
                 return false;
             }
         }

        function CheckSelect() {

            var number = 0;
            $.each($("#<%=grdSNRule.ClientID%>").find("span input"), function (i, item) {
                if (item.checked == true) {
                    number = number + 1;
                }
            });
            if (number == 0) {
                alert("<%= Resources.Lang.FrmBar_SNManagement_DoTips %>");
                return false;
            }
            var flag = true;
            var barcodeType = $("#<%=hiddBarCodeType.ClientID%>").val();
            $.ajax({
                type: "Post",
                async: false, //已經是同步請求了
                cache: false,
                global: false,
                url: "../WMSService/CodeRuleService.asmx/CheckRuleCount",
                data: $.toJSON({ RCount: number, BarCodeType: barcodeType }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    if (result.code == "0") {
                        flag = true;
                    } else {
                        if (confirm("<%= Resources.Lang.FrmBar_SNManagement_EnableMoreConfirm %>")) {
                    flag = true;
                } else {
                    flag = false;
                }
            }
                },
                error: function (err) {
                    flag = false;
                }
            });
    if (!flag) {
        return false;
    }
}

var WMSUI = {
    FormFed: {
        btnExport: $("#btnExportToExcel"),
        txtSN: $("#<%=txtSN.ClientID%>"),
                txtCreateFrom: $("#<%=txtDCREATETIMEFrom.ClientID%>"),
                txtCreateTo: $("#<%=txtDCREATETIMETo.ClientID%>"),
                txtCreateUser: $("#<%=txtCreateOwner.ClientID%>"),
                hiddBarCodeType: $("#<%=hiddBarCodeType.ClientID%>")
            },
            FormVal: {
                sn: "",
                CreateFrom: "",
                CreateTo: "",
                CreateUser: "",
                BarCodeType: ""
            },
            Init: function () {
                var _self = WMSUI;
                _self.FormFed.btnExport.bind('click', _self.ExportToExcel);
            },
            ExportToExcel: function () {
                $("#loadingToast").show();
                var _self = WMSUI;
                _self.FormVal.sn = _self.FormFed.txtSN.val();
                _self.FormVal.CreateFrom = _self.FormFed.txtCreateFrom.val();
                _self.FormVal.CreateTo = _self.FormFed.txtCreateTo.val();
                _self.FormVal.CreateUser = _self.FormFed.txtCreateUser.val();
                _self.FormVal.BarCodeType = _self.FormFed.hiddBarCodeType.val();
                $.ajax({
                    type: "Post",
                    async: true, //已經是同步請求了
                    cache: false,
                    global: false,
                    url: "../WMSService/CodeRuleService.asmx/ExportToExcel",
                    data: $.toJSON({ parameter: _self.FormVal }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#loadingToast").hide();
                        var result = data.d;
                        if (result.code == "0") {
                            window.location.href = result.msg;
                        }
                    },
                    error: function (err) {
                        $("#loadingToast").hide();
                    }
                });
            }
        }
        WMSUI.Init();

    </script>
</asp:Content>
