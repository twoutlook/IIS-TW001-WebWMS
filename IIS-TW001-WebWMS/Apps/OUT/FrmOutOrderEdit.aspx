<%@ Page Language="C#" AutoEventWireup="true" Title="--订单管理" MasterPageFile="~/Apps/DefaultMasterPage.master" CodeFile="FrmOutOrderEdit.aspx.cs" Inherits="Apps_OUT_FrmOutOrderEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../BASE/ShowBASE_CLIENTDiv.ascx" TagName="ShowBASE_CLIENTDiv" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script type="text/javascript">
        function SetControlValue(ControlName, Values, ControlName2, Values2) {
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
        }

        function Show(divID) {
            disponse_div(event, document.all(divID));
        }

    </script>

    <style type="text/css">
        span.requiredSign {
            color: #FF0000;
            font-weight: bold;
            position: relative;
            left: -15px;
            top: 2px;
        }
    </style>
    <style type="text/css">

        html {
            height: 100%;
            overflow-x:hidden;
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
        input[type='submit'][disabled],input[type='button'][disabled]
        {       
            opacity:0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70);/*兼容ie8及以下*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt<%= Resources.Lang.FrmOutOrderList_Menu_PageName %> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:ShowBASE_CLIENTDiv ID="ShowBASE_CLIENTDiv1" runat="server" />
    <table id="Table3" style="width: 100%" cellspacing="1" cellpadding="1"border="0">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_OrderCode %>"></asp:Label>：<%--订单编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtOrderNo" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_OrderType %>"></asp:Label>：<%--订单类型--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="drpOrderType" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_OrderDate %>"></asp:Label>：<%--订单日期--%>
                        </td>

                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtOrderDate" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtOrderDate','y-mm-dd',0);" />
                        </td>

                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCVENDERCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ClientCode %>"></asp:Label>：<%--客户编码：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCustomId" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false" onclick="Show('<%= ShowBASE_CLIENTDiv1.GetDivName %>');"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCVENDER" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ClientName %>"></asp:Label>： <%--客户名称--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCustomName" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false" MaxLength="50" onclick="Show('<%= ShowBASE_CLIENTDiv1.GetDivName %>');"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, WMS_Common_GridView_Status %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtStatus" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CustomOrderCode %>"></asp:Label>：<%--客户订单编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCustomOrderNo" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_SalesMan %>"></asp:Label>：<%--业务员名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSalesMan" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50" ></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_AmountBeforeTax %>"></asp:Label>：<%--税前总额(元)--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Lang, FrmOutOrderEdit_Tax %>"></asp:Label>：<%--税率(百分比)--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtTaxRate" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Lang, FrmOutOrderEdit_AmountAfterTax %>"></asp:Label>：<%--税后总额(元)--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtAfterTaxAmount" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSONCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_OrderSource %>"></asp:Label>：<%--单据来源--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="drpSource" runat="server" Width="95%">
                                <asp:ListItem Value="0">ERP</asp:ListItem>
                                <asp:ListItem Value="1">WMS</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateOwner %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCREATEOWNER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_LastUpdator %>"></asp:Label>：<%--最后修改人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLASTUPDATEOWNER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_LastUpdateDate %>"></asp:Label>：<%--最后修改日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLASTUPDATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_FaHuoAddress %>"></asp:Label>：<%--发货地址--%>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDeliveryAddress" runat="server" CssClass="NormalInputText" Width="95%"  MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Remark %>"></asp:Label>：
                        </td>
                        <td style="width: 20%" colspan="5">
                            <asp:TextBox ID="txtMEMO" runat="server" TextMode="MultiLine" Width="98%" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, WMS_Common_Button_Save %>" />
                            &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td>
                <table id="TabMain0" style="height: 100%; width: 100%" runat="server" visible="false">
                    <tr valign="top">
                        <td valign="top" align="left"  style="padding: 0px 0px 15px 0px;">
                            <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang,WMS_Common_Button_New %>"></asp:Button>
                            &nbsp;
                            <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" CssClass="ButtonDel" OnClientClick="return CheckDel();" />
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Label ID="Label5" CssClass="InputLabel" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_SearchCinvcode %>"></asp:Label>：
                            <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText"  Width="25%" MaxLength="50"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="min-height: 460px; width: 100%" id="DivScroll">
                                <asp:Panel ID="panel1" runat="server">
                                    <asp:GridView ID="grdOutOrder_D" runat="server" AllowPaging="True" BorderColor="Teal"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                                        Width="100%" AutoGenerateColumns="False"
                                        OnRowDataBound="grdOutOrder_D_RowDataBound" CssClass="Grid" PageSize="15">
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
                                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IDS" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ZiBiaoCode %>" Visible="False"><%--子表编号--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ZhuBiaoCode %>" Visible="False"><%--主表编号--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OrderLine" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_XiangCi %>"><%--项次--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="100px" />
                                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CinvCode" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>">
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="180px" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="180px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="calias" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Calias %>"><%--助记码--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="180px" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="180px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CinvName" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>">
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IQUANTITY" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Quantity %>">
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="120px" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PRICE" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Price %>"><%--单价(元)--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Amount" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_TotalPrice %>"><%--总价(元)--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FinishQty" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_FinishQuantity %>"><%--完成数量--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="STATUS"  HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Status %>">
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" Text="<%$ Resources:Lang, WMS_Common_GridView_Edit %>">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="100px" />
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, WMS_Common_Element_SNWeiHu %>" ItemStyle-HorizontalAlign="Center"><%--SN条码维护--%>
                                               <ControlStyle BorderWidth="0px" />
                                               <ItemTemplate>
                                                  <asp:HyperLink ID="hySN" runat="server" NavigateUrl="#" ><%= Resources.Lang.WMS_Common_Element_SNWeiHu %></asp:HyperLink><%--SN条码维护 --%>                       
				                             </ItemTemplate>
                                            </asp:TemplateField>
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
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server"
                    DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
    </table>

    <input type="hidden" id="txtID" runat="server" />
    <asp:HiddenField ID="hfIsConfirm" runat="server" Value="0" />
    <input type="hidden" id="hiddOperation" runat="server" />

    <script type="text/javascript" language="javascript">
        function CheckDel() {
            var number = 0;
            $.each($("#<%=grdOutOrder_D.ClientID%>").find("span input"), function (i, item) {
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
</asp:Content>
