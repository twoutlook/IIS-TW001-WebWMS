<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmOUTASSITEdit.aspx.cs" Inherits="OUT_FrmOUTASSITEdit" Title="--拣货指引" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="ShowOutASN_Div.ascx" TagName="ShowOutASN_Div" TagPrefix="ucOA" %>
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
            height: 100%;
        }

        .tableCell {
            display: table;
            width: 100%;
        }

        .gridLineHeight {
            line-height: 22px;
        }

        input[type='submit'][disabled], input[type='button'][disabled] {
            opacity: 0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70); /*兼容ie8及以下*/
        }

        span.requiredSign {
            color: #FF0000;
            font-weight: bold;
            position: relative;
            left: -15px;
            top: 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt;<%= Resources.Lang.FrmOUTASSITList_Menu_PageName %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <ucOA:ShowOutASN_Div ID="ucOutASN_Div" runat="server" />
    <table id="Table3" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cticketcode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false" MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCOUTASNID" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Outasncode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCOUTASNID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                            <asp:TextBox ID="txtOutAsn_Id" runat="server" CssClass="NormalInputText" Width="95%" Style="display: none;"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 12%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_OutType %>"></asp:Label>：<%--出库类型：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlOutType" runat="server" Enabled="false" Width="99%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="格式：yyyy-MM-dd" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateOwner %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Enabled="false" Width="95%" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, WMS_Common_GridView_Status %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Enabled="false" Width="99%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%"></td>
                        <td style="width: 20%"></td>
                        <td class="InputLabel" style="width: 13%"></td>
                        <td style="width: 20%"></td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="ID：" Visible="false"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Visible="false" Width="95%" ToolTip="格式：最多10位整数，最多4位小数"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Remark %>"></asp:Label>：
                        </td>
                        <td style="width: 20%;" colspan="6">
                            <asp:TextBox ID="txtCMEMO" runat="server" TextMode="MultiLine" Width="99%" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="6"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td colspan="6" style="text-align: center; padding: 15px 0px;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Visible="false" Text="<%$ Resources:Lang, WMS_Common_Button_Save %>" />
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Visible="false" Text="<%$ Resources:Lang,WMS_Common_Button_New %>" OnClick="btnNew_Click"></asp:Button>
                &nbsp;&nbsp;<asp:Button ID="btnAutoCreate" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang,FrmOUTBILLEdit_Button_ShengCheng %>" OnClick="btnAutoCreate_Click"></asp:Button><%--生成--%>
                <asp:Button ID="btnAutoCreateTest" runat="server" CssClass="ButtonAdd" Text="DateCode" OnClick="btnAutoCreateTest_Click" Visible="False"></asp:Button>
                <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" OnClientClick="return CheckDel('ctl00_ContentPlaceHolderMain_grdOUTASSIT_D');" CssClass="ButtonDel" Visible="false" />
                &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
                &nbsp;&nbsp;<asp:Button ID="btnPrint" runat="server" CssClass="ButtonPrint" Text="<%$ Resources:Lang,WMS_Common_Button_Print %>" OnClick="btnPrint_Click"></asp:Button>
            </td>
        </tr>
        <tr valign="top" class="tableCell">
            <td valign="top" align="center" colspan="6">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server" DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
        <tr valign="top" class="tableCell" id="trSubSearch" runat="server">
            <td valign="top" colspan="6">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ZhuBiaoCode %>"></asp:Label>：<%--主表编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 350px">
                            <asp:CheckBox ID="cboIsLineCargoSpace" runat="server" Text="<%$ Resources:Lang, FrmOUTASSITList_XianBianMingXi %>" Style="color: #3580C9;" Visible="false" /><%--线边仓明细--%>
                            &nbsp;&nbsp;
                            <asp:Label ID="lblCinvCode" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>" Style="color: #3580C9;"></asp:Label>：
                            <asp:TextBox ID="txtCinvCode" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell" id="trGridView" runat="server">
            <td colspan="6">
                <div style="min-height: 300px; overflow-x: auto; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdOUTASSIT_D" DataSourceID="" runat="server" AllowPaging="True"
                        BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%"
                        AutoGenerateColumns="False" OnRowDataBound="grdOUTASSIT_D_RowDataBound"
                        ShowHeader="True" CssClass="Grid gridLineHeight" PageSize="15">
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
                            <asp:BoundField Visible="False" DataField="IDS" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ZiBiaoCode %>"><%--子表编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Width="200px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_CpositionCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="160px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cposition %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVBARCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASSITList_CinvcodeBar %>" Visible="False"><%--物料条码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="INUM" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Quantity %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CBATCH" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASSITList_PiCi %>"><%--批次--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sourcecode" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_ZhiDingBill %>"><%--指定单据--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField Visible="False" DataField="CMEMO" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Remark %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField Visible="False" DataField="COPERATORCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_OperatorCode %>"><%--操作人编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COPERATOR" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Operator %>"><%--操作人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CSTATUS" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Status %>" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" Visible="False"
                                DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" Text="<%$ Resources:Lang, WMS_Common_GridView_Edit %>">
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
        <tr class="tableCell">
            <td colspan="6">&nbsp;&nbsp; &nbsp;&nbsp;<asp:Button ID="btnDeleteParent" runat="server" CssClass="ButtonDel"
                OnClick="btnDeleteParent_Click" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" CausesValidation="false" Visible="false" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="hiddOperation" runat="server" />
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
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
</asp:Content>
