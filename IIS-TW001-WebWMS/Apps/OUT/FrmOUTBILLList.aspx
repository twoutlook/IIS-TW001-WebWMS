<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" Title="--出库单" CodeFile="FrmOUTBILLList.aspx.cs" Inherits="OUT_FrmOUTBILLList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
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
            height: 100%;
        }

        .tableCell {
            display: table;
            width: 100%;
        }

        .gridLineHeight {
            line-height: 22px;
        }

        .btnContainer input:not(:first-child) {
            margin-left: 12px;
        }

        /*.Grid .aspNetDisabled {
            color:#cccccc;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt;<%= Resources.Lang.FrmOUTBILLList_Menu_PageName %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="width: 95%">
        <tr valign="top" class="tableCell">
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
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cticketcode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCOUTASNID" runat="server" Text="<%$ Resources:Lang, FrmOUTBILLList_CticketCode %>"></asp:Label>： <%--出库通知单单号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCOUTASNID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_OutType %>"></asp:Label>： <%--出库类型：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="drIType" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmOUTBILLList_BoxOrPalletCode %>"></asp:Label>： <%--箱号/栈板号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox runat="server" ID="TxtSNCode" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCinvcode" runat="server"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                         <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label6" runat="server" Text="批/序號(RANK)"></asp:Label>：
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtRank_Final" runat="server"
                                CssClass="NormalInputText" Width="95%" MaxLength="1"></asp:TextBox>
                        </td>

                     
                    </tr>
                    <tr>
                           <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, WMS_Common_GridView_Status %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dplCSTATUS" runat="server" Width="95%"></asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCLIENT" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ClientName %>"></asp:Label>： <%--客户名称：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCLIENT" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCLIENTCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ClientCode %>"></asp:Label>：   <%--客户编码：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCLIENTCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ErpCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDINDATEFromFrom" runat="server" Text="<%$ Resources:Lang, FrmOUTBILLList_OutDate %>"></asp:Label>： <%--出库日期：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDINDATEFrom" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDINDATEToTo" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDINDATETo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATETo','y-mm-dd',0);" />
                        </td>
                       
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSON" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_AuditUser %>"></asp:Label>： <%--审核人：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCAUDITPERSON" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITTIMEFromFrom" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_AuditDate %>"></asp:Label>： <%--审核日期：--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtDAUDITTIMEFrom" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITTIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITTIMEToTo" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtDAUDITTIMETo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITTIMETo','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%"><%= Resources.Lang.WMS_Common_Element_WorkType %>:<%--作业方式:--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="drpWorkType" runat="server" Width="95%">
                                <%--<asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="0">平库</asp:ListItem>
                                <asp:ListItem Value="1">立库</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%"><%= Resources.Lang.FrmOUTBILLList_IsBuDan %>:<%--是否补单:--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="drpOperationType" runat="server" Width="95%">
                               <%-- <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="0">非补单</asp:ListItem>
                                <asp:ListItem Value="1">补单</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang,FrmOUTASNList_SourceCode %>"></asp:Label>：<%--来源单号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtso" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                   
                   
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateOwner %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMETo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','y-mm-dd',0);" />
                        </td>
                      
                    </tr>
                     <tr>
                          <td class="InputLabel" style="width: 13%"><asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang,Commona_TimePeriod %>"></asp:Label>：</td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:RadioButtonList ID="rbtList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" BorderStyle="None" CssClass="DateTypeRadio">                          
                            </asp:RadioButtonList>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>： <%--规格--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtcspec" runat="server"  CssClass="NormalInputText" Width="95%"></asp:TextBox>                          
                        </td>                     
                         <td colspan="2" align="center" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                        </td>   
                    </tr>
                      </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td colspan="6" align="left" style="padding: 15px 0px;" class="btnContainer">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang,WMS_Common_Button_New %>"></asp:Button>
                <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" CssClass="ButtonDel" OnClientClick="return CheckDel()" />
                <asp:Button ID="btnUpdateSTOCK" runat="server" CssClass="ButtonDo" Text="<%$ Resources:Lang,WMS_Common_Button_KouZhang %>" OnClick="btnUpdateSTOCK_Click" Visible="False"></asp:Button>
                <asp:Button ID="btnUpdateSTOCK_Test" runat="server" CssClass="ButtonDo" Text="扣账T" OnClick="btnUpdateSTOCK_Test_Click" Visible="False"></asp:Button>
            </td>
        </tr>
        <tr class="tableCell">
            <td>
                <div style="min-height: 460px; overflow-x: auto; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdOUTBILL" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="grdOUTBILL_RowDataBound" CssClass="Grid gridLineHeight" PageSize="15" OnDataBinding="grdOUTBILL_DataBinding">
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
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkSelect0" runat="server" BorderStyle="Solid" BorderWidth="0px" />
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINASNEMTList_MSG4 %>" InsertVisible="False"> <%--序号--%>
                                <ItemTemplate> 
                                <%#Container.DataItemIndex+1%> 
                                </ItemTemplate> 
                                <ItemStyle HorizontalAlign="Center" />
                             </asp:TemplateField> 
                            <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="ID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CTICKETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cticketcode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SN_CODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_PalletCode %>"><%--栈板号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bCCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTBILLList_CticketCode %>"><%--出库通知单单号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="otype" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNList_OutType %>"><%--出库类型--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ErpCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CSO" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmOUTASNList_SourceCode %>"><%--来源单号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CCREATEOWNERCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_CreateOwner %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DCREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_CreateDate %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CMEMO" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Remark %>" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DINDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmOUTBILLList_OutDate %>"><%--出库日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DEBITTIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_KouZhangShiJian %>"><%--扣帐时间--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DDEFINE3" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_PaoZhuangShiJian %>"><%--抛转时间--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="worktype" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_WorkType %>"><%--作业方式--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cstatus" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Status %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, WMS_Common_Element_FanKu %>" ShowHeader="False" Visible="false"><%--返库--%>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnOutBillBack" runat="server" CausesValidation="false" CommandArgument='<%#Eval("ID") %>'
                                        CommandName="" Text="<%$ Resources:Lang, WMS_Common_Element_FanKu %>" OnClick="lbtnOutBillBack_Click"></asp:LinkButton><%--返库--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" Text="<%$ Resources:Lang, WMS_Common_GridView_Edit %>">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, FrmOUTASNList_YiChang %>" DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNList_YiChang %>" Text="<%$ Resources:Lang, FrmOUTASNList_ChaKang %>"><%--查看--%>
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
                            <div><%= Resources.Lang.WMS_Common_Pager_Total %> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.WMS_Common_Pager_Count %></div>
                        </li>
                    </ul>
                </div>
            </td>
        </tr>
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
        <script type="text/javascript">

            $(document).ready(function () {
                var fsy = get_request("Cstatus");
                if (fsy.length > 0) {
                    var IsPostBack = "<%=IsPostBack%>";
                    if (IsPostBack == "False") {
                        //第一次加载要执行的东西  
                        $("#<%=btnSearch.ClientID %>").click();
                    }
                } else {
                    if ($("#<%=txtDCREATETIMEFrom.ClientID %>").val() == "" && $("#<%=txtDCREATETIMETo.ClientID %>").val() == "") {
                        FormatSetDate('<%=txtDCREATETIMEFrom.ClientID %>', '<%=txtDCREATETIMETo.ClientID %>');    //第一个参数是开始日期，第二个参数是结束日期
                    }
                }
                $("#<%=rbtList.ClientID %>").change(function () {
                    FormatSetDate('<%=txtDCREATETIMEFrom.ClientID %>', '<%=txtDCREATETIMETo.ClientID %>'); //第一个参数是开始日期，第二个参数是结束日期
                 });
            });

            function ChangeDivWidth() {
                document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
            }
            window.onresize = ChangeDivWidth;
            ChangeDivWidth();

            function CheckDel() {
                var number = 0;
                $.each($("#<%=grdOUTBILL.ClientID%>").find("span input"), function (i, item) {
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

        </script>
    </table>
</asp:Content>
