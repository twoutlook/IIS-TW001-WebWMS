﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmSTOCK_CHECKBILLEdit.aspx.cs" Inherits="STOCK_FrmSTOCK_CHECKBILLEdit" Title="--盘点单" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Group_StockManage %>-&gt;<%= Resources.Lang.FrmSTOCK_CHECKBILLEdit_PageName %><%--物理盘点--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>

    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
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
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_PanDianCode %>"></asp:Label>：<%--盘点单号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="40" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblDCHECKDATE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_PanDianDate %>"></asp:Label>：<%--盘点日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDCHECKDATE" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="格式：yyyy-MM-dd hh:mm:ss" MaxLength="19"></asp:TextBox>
                            <img border="0" id="imgDCHECKDATE" runat="server" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCHECKDATE','y-mm-dd',0);" />
                            <asp:RequiredFieldValidator ID="rfvtxtDCHECKDATE" runat="server" ControlToValidate="txtDCHECKDATE"
                                ErrorMessage="请填写盘点日期!" Display="None"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_ErpCode %>"></asp:Label>：<%--ERP单号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_PanDianName %>"></asp:Label><%--盘点单名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCHECKNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                            <span class="requiredSign">*</span>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCHECKTYPE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_PanDianType %>"></asp:Label>：<%--盘点类型--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtCHECKTYPE" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False">
                                <%--<asp:ListItem Text="物理" Value="0"></asp:ListItem>
                                <asp:ListItem Text="循环" Value="1"></asp:ListItem>
                                <asp:ListItem Text="抽盘" Value="2"></asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvtxtCHECKTYPE" runat="server" ControlToValidate="txtCHECKTYPE"
                                ErrorMessage="请填写盘点类型!" Display="None"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang,WMS_Common_GridView_Status %>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtCSTATUS" runat="server" Width="95%" Enabled="False">
                                <%--<asp:ListItem Value="0" Text="未處理"></asp:ListItem>
                                <asp:ListItem Value="1" Text="已審核"></asp:ListItem>
                                <asp:ListItem Value="2" Text="初盤"></asp:ListItem>
                                <asp:ListItem Value="4" Text="初盤完成"></asp:ListItem>
                                <asp:ListItem Value="3" Text="複盤"></asp:ListItem>
                                <asp:ListItem Value="6" Text="盤點中"></asp:ListItem>
                                <asp:ListItem Value="5" Text="盤點完成"></asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvtxtCSTATUS" runat="server" ControlToValidate="txtCSTATUS"
                                ErrorMessage="请填写状态!" Display="None"> 
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang,FrmSTOCK_CHECKBILLEdit_IsFuPan %>"></asp:Label>：<%--是否复盘--%>
                        </td>
                        <td style="width: 20%">
                            <asp:CheckBox ID="chkISCHECKBILL" runat="server" />
                        </td>
                        <td class="InputLabel" style="width: 13%">&nbsp;
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CheckPlanName %>"></asp:Label>：<%--盘点计划名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPLANNAME" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">&nbsp;
                        </td>
                        <td style="width: 20%">&nbsp;
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CreateOwner %>"></asp:Label>：<%--制单人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CreateDate %>"></asp:Label>：<%--制单日期--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="格式：yyyy-MM-dd"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIME','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">&nbsp;
                        </td>
                        <td style="width: 21%">&nbsp;
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">&nbsp;
                        </td>
                        <td style="width: 20%">&nbsp;
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSON" runat="server" Text="<%$ Resources:Lang,FrmOUTASNList_AuditUser %>"></asp:Label>：<%--审核人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCAUDITPERSON" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITTIME" runat="server" Text="<%$ Resources:Lang,FrmOUTASNList_AuditDate %>"></asp:Label>：<%--审核日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDAUDITTIME" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="格式：yyyy-MM-dd" MaxLength="19"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITTIME','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCIRCLECHECKBEGINDATE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CheckBeginData %>"></asp:Label>：<%--循环盘点开始日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDCIRCLECHECKBEGINDATE" runat="server" CssClass="NormalInputText"
                                Width="95%" ToolTip="格式：yyyy-MM-dd" MaxLength="19"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCIRCLECHECKBEGINDATE','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCIRCLECHECKENDDATE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CheckEndData %>"></asp:Label>：<%--循环盘点结束日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDCIRCLECHECKENDDATE" runat="server" CssClass="NormalInputText"
                                Width="95%" ToolTip="格式：yyyy-MM-dd" MaxLength="19"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCIRCLECHECKENDDATE','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">&nbsp;
                        </td>
                        <td style="width: 20%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_Remark %>"></asp:Label>：<%--备注--%>
                        </td>
                        <td colspan="5" style="width: 20%">
                            <asp:TextBox ID="txtCMEMO" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="200" Height="41px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" Visible="false" runat="server" Text="ID："></asp:Label>
                        </td>
                        <td colspan="5" style="width: 20%">
                            <asp:TextBox ID="txtID" Visible="false" runat="server" CssClass="NormalInputText"
                                Width="95%" MaxLength="36"></asp:TextBox>
                            <%--<span class="requiredSign">*</span>--%>
                            <asp:RequiredFieldValidator ID="rfvtxtID" runat="server" ControlToValidate="txtID"
                                ErrorMessage="请填写ID!" Display="None"> 
                            </asp:RequiredFieldValidator>
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
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server" DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang,WMS_Common_Button_Save %>" />
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang,WMS_Common_Button_Back %>" CausesValidation="false" />
            </td>
        </tr>
        <tr>
            <td colspan="6" align="left">
                <div style="display: none">
                    &nbsp;&nbsp;
                    <asp:Button ID="btnDelete" runat="server" CssClass="ButtonDel" OnClick="btnDelete_Click" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" CausesValidation="false" />
                    <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                </div>
                &nbsp;&nbsp;<asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang,WMS_Common_Button_New %>" OnClick="btnNew_Click"></asp:Button>
                &nbsp;&nbsp;<asp:Button ID="btnDelete0" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" CssClass="ButtonDel" CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server">
                    <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                        <asp:GridView ID="grdSTOCK_CHECKBILL_D" runat="server" AllowPaging="True" BorderColor="Teal"
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                            OnPageIndexChanged="grdSTOCK_CHECKBILL_D_PageIndexChanged" OnPageIndexChanging="grdSTOCK_CHECKBILL_D_PageIndexChanging"
                            OnRowDataBound="grdSTOCK_CHECKBILL_D_RowDataBound" ShowHeader="True" CssClass="Grid"
                            PageSize="15">
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
                                <asp:BoundField DataField="IDS" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ZiBiaoCode %>"><%--子表编号--%>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"><%--料号--%>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>"><%--品名--%>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("CINVNAME") %>' CssClass="mlength" ToolTip='<%# Eval("CINVNAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CpositionCode %>"><%--储位编码--%>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CpositionName %>"><%--储位名称--%>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IQUANTITY" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Quantity %>"><%--数量--%>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="right" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CMEMO" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Remark %>"><%--备注--%>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
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
                </asp:Panel>
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
