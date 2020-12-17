<%@ Page Title="--出库变更通知单" Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master"
    AutoEventWireup="true" CodeFile="FrmOutChangeAsnEdit.aspx.cs" Inherits="Apps_OUT_FrmOutChangeAsnEdit" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HolderTitle" runat="Server">
    出库管理-&gt;出库变更单
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
                    <tr style="display: none">
                        <td colspan="4">
                            <style type="text/css">
                                span.requiredSign
                                {
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
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="单据号："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText"
                                Width="95%" MaxLength="40" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="ERP单号："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="状态："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="制单人："></asp:Label>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false" MaxLength="50" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="制单日期："></asp:Label>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="备注："></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtCMEMO" runat="server" TextMode="MultiLine" Width="99%" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="6">
                            <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"
                                Enabled="False"></asp:TextBox>
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
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Button ID="btnCheck" runat="server" CssClass="ButtonYY" Text="审核" 
                                OnClick="btnCheck_Click">
                            </asp:Button>
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                                Text="保存" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnOK" runat="server" CssClass="ButtonConfig5" OnClick="btnOK_Click"
                                Text="执行变更" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="返回" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
                <table id="TabMain0" style="height: 100%; width: 100%" runat="server" visible="false">
                    <tr valign="top">
                        <td valign="top" align="left">
                        </td>
                        <td class="InputLabel" align="right" style="width: 10%">
                            <asp:Label ID="Label8" CssClass="InputLabel" runat="server" Text="查询料号："></asp:Label>
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 6%">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="查询" OnClick="btnSearch_Click">
                            </asp:Button>
                        </td>
                    </tr>
                   <%-- <tr valign="top">
                        <td valign="top" colspan="4">
                            <cc1:DataGridNavigator3 ID="grdNavigatorOutChange_d" runat="server" GridID="grdOutChange_d"
                                ShowPageNumber="false" ExcelName="OutChange_d" IsDbPager="True" OnGetExportToExcelSource="grdNavigatorOutChange_d_GetExportToExcelSource" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="4">
                            <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                                <script type="text/javascript" language="javascript">
                                    //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                                    //settingPad("<%= grdOutChange_d.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                                </script>
                                <asp:GridView ID="grdOutChange_d" runat="server" AllowPaging="True" BorderColor="Teal"
                                    DataKeyNames="IDS,CINVCODE" BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                                    Width="100%" AutoGenerateColumns="False" 
                                   
                                    CssClass="Grid" PageSize="15">
                                    <PagerSettings Visible="False" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <RowStyle HorizontalAlign="Left" Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                    <Columns>
                                        <asp:BoundField DataField="OUTASN_CTICKETCODE" DataFormatString="" HeaderText="出库通知单号">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="180px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="料号">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="品名">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OLDNUM" DataFormatString="{0:0.00}" HeaderText="原数量">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NOWNUM" DataFormatString="{0:0.00}" HeaderText="现数量">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CREATE_OWNER" HeaderText="创建人">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CREATE_TIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                            HeaderText="创建时间">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                  <ul class="OneRowStyle">
                        <li>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                            </webdiyer:AspNetPager>
                        </li>
                        <li>
                            <div>共 <%=AspNetPager1.RecordCount  %> 条数据</div>
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
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server"
                    DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
    </table>
</asp:Content>
