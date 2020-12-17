<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Apps/DefaultMasterPage.master" CodeFile="FrmBASE_PARAMETEREdit.aspx.cs" Inherits="Apps_BASE_FrmBASE_PARAMETEREdit" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link type="text/css" rel="Stylesheet" href="../../Layout/CSS/LG/Page.css" id="cssUrl" runat="server" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/Calendar/calendar-blue.css" />
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

        span.requiredSign {
            color: #FF0000;
            font-weight: bold;
            position: relative;
            left: -15px;
            top: 2px;
        }

        .gridLineHeight {
            line-height: 22px;
        }

            .gridLineHeight a {
                cursor:pointer;
            }

        input[type='submit'][disabled], input[type='button'][disabled] {
            opacity: 0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70); /*兼容ie8及以下*/
        }

    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02 %>-&gt;<asp:Literal ID="ltPageTable" runat="server" Text="代码组管理"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

<table id="Table3" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="代码组编号:"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtFlagType" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblITYPE" runat="server" Text="代码组名称:"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtFlagName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateOwner %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                        <td  colspan="4">

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="text-align: center; padding: 15px 0px;">
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, WMS_Common_Button_Save %>" />
                            &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
                <table id="TabMain0" style="height: 100%; width: 100%" runat="server" visible="false">
                    <tr valign="top">
                        <td valign="top" align="left" style="padding: 0px 0px 15px 0px;" class="btnContainer">
                            <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang,WMS_Common_Button_New %>"></asp:Button>
                            <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" OnClientClick="return CheckDel();" CssClass="ButtonDel" />
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Label ID="Label5" CssClass="InputLabel" runat="server" Text="子项编号"></asp:Label>：
                            <asp:TextBox ID="txtFlagCode" runat="server" CssClass="NormalInputText"  Width="25%" MaxLength="50"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="min-height: 300px; overflow-x: auto; width: 100%" id="DivScroll">
                                <asp:GridView ID="grdSys_Parameter_D" runat="server" AllowPaging="True" BorderColor="Teal"
                                    DataKeyNames="ID" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ShowHeaderWhenEmpty="true"
                                    Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdSys_Parameter_D_RowDataBound"
                                    CssClass="Grid gridLineHeight" PageSize="15" >
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
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="flag_id" DataFormatString="" HeaderText="子项编号">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="flag_name" DataFormatString="" HeaderText="子项默认名称">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sortid" DataFormatString="" HeaderText="子项排序">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="200px" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" Text="<%$ Resources:Lang, WMS_Common_GridView_Edit %>">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
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

                </table>
            </td>
        </tr>
    </table>
    <input type="hidden" id="hiddGuid" runat="server" />
    <input type="hidden" id="hiddOperation" runat="server" />
</asp:Content>
