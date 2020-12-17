<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmAllocateCancelEdit.aspx.cs"
    Inherits="ALLOCATE_FrmAllocateCancelEdit" Title="--1111" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmALLOCATEList_Title%>-&gt;<%= Resources.Lang.FrmAllocateCancel_Title01%> <%--库存管理-&gt;调拨单调账--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
                    <tr style="display: none">
                        <td colspan="6">
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
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblCTICKETCODE%>"></asp:Label>：<%--单据号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false" MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDINDATE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblDINDATEFromFrom%>"></asp:Label>：<%--调拨日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDINDATE" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false" ToolTip="<%$ Resources:Lang, Common_Format%>" Enabled="False"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                            <img border="0" id="imgDINDATE" runat="server" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATE','y-mm-dd',0);" />
                            <span class="requiredSign" style="left: -50px">*</span>
                            <asp:RequiredFieldValidator ID="rfvtxtDINDATE" runat="server" ControlToValidate="txtDINDATE"
                                ErrorMessage="<%$ Resources:Lang, FrmALLOCATEEdit_Msg05%>" Display="None"> </asp:RequiredFieldValidator> <%--请填写调拨日期!--%>
                            <%--<asp:RegularExpressionValidator ID="revtxtDINDATE" runat="server" ValidationExpression="[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]"
                                ControlToValidate="txtDINDATE" ErrorMessage="请填写有效的调拨日期!正确的格式是：yyyy-MM-dd" Display="None"> </asp:RegularExpressionValidator>--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Label2%>"></asp:Label>：<%--ERP单号--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblCCREATEOWNERCODE%>"></asp:Label>：<%--制单人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblDCREATETIMEFromFrom %>"></asp:Label>：<%--制单日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%">
                               <%-- <asp:ListItem Value="0">未处理</asp:ListItem>--%>
                             <%--   <asp:ListItem Value="4">已確認</asp:ListItem>--%>
                               <%-- <asp:ListItem Value="1">已审核</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSON" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblCAUDITPERSON%>"></asp:Label>：<%--审核人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCAUDITPERSON" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITTIME" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblDAUDITTIMEFromFrom %>"></asp:Label>：<%--审核日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDAUDITTIME" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false" ToolTip="<%$ Resources:Lang, Common_Format%>"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">

                            <asp:Label ID="lblID" runat="server" Text="ID：" Visible="false"></asp:Label>
                        </td>
                        <td style="width: 20%">

                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：<%--备注--%>
                        </td>
                        <td style="width: 21%" colspan="6">
                            <asp:TextBox ID="txtCMEMO" runat="server" TextMode="MultiLine" Width="99%" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none;">

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCDEFINE1" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_lblCDEFINE1%>"></asp:Label>：<%--自定义1--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCDEFINE1" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCDEFINE2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_lblCDEFINE2%>"></asp:Label>：<%--自定义2--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCDEFINE2" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDDEFINE3" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_lblDDEFINE3%>"></asp:Label>：<%--自定义3--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDDEFINE3" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, Common_Format%>"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDDEFINE3','y-mm-dd',0);" />
                            <asp:RegularExpressionValidator ID="revtxtDDEFINE3" runat="server" ValidationExpression="[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]"
                                ControlToValidate="txtDDEFINE3" ErrorMessage="<%$ Resources:Lang, FrmAllocateCancelEdit_revtxtDDEFINE3%>" Display="None"> </asp:RegularExpressionValidator><%--请填写有效的自定义3!正确的格式是：yyyy-MM-dd--%>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDDEFINE4" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_lblDDEFINE4%>"></asp:Label>：<%--自定义4--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDDEFINE4" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, Common_Format%>"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDDEFINE4','y-mm-dd',0);" />
                            <asp:RegularExpressionValidator ID="revtxtDDEFINE4" runat="server" ValidationExpression="[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]"
                                ControlToValidate="txtDDEFINE4" ErrorMessage="<%$ Resources:Lang, FrmAllocateCancelEdit_revtxtDDEFINE4%>" Display="None"> </asp:RegularExpressionValidator><%--请填写有效的自定义4!正确的格式是：yyyy-MM-dd--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIDEFINE5" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_lblIDEFINE5%>"></asp:Label>：<%--自定义5--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIDEFINE5" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_Msg01%>"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                            <asp:RegularExpressionValidator ID="revtxtIDEFINE5" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,16}(\.[0-9]{1,2}){0,1}"
                                ControlToValidate="txtIDEFINE5" ErrorMessage="<%$ Resources:Lang, FrmAllocateCancelEdit_revtxtIDEFINE5%>"
                                Display="None"> </asp:RegularExpressionValidator> <%--请填写有效的自定义5!正确的格式是：最多16位整数，最多2位小数--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">&nbsp;
                        </td>
                        <td style="width: 20%">&nbsp;
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
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server"
                    DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                    Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>
    <table id="Table1" style="height: 100%; width: 95%">
        <tr valign="top" style="display: none;">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                            height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.Common_JSCeria%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr style="display: none">
                        <td colspan="6">
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
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_Label1%>"></asp:Label>：<%--主表编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td colspan="1">&nbsp;
                        </td>
                        <td colspan="1">&nbsp;
                        </td>
                        <td colspan="1">&nbsp;
                        </td>
                        <td colspan="1">&nbsp;
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
                    <tr>
                        <td colspan="6" align="center">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"></asp:Button> <%--查询--%>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>" OnClick="btnNew_Click" Visible="False"></asp:Button><%--新增--%>
                &nbsp;&nbsp;<asp:Button ID="btnImportExcel" runat="server" CssClass="ButtonExcel"
                    Text="<%$ Resources:Lang, Common_Export%>" Enabled="False" Visible="False"></asp:Button><%--导入--%>
                &nbsp;&nbsp;<asp:Button ID="btnCreateInOutBill" runat="server" Visible="false" CssClass="ButtonAdd"
                    Text="调拨" OnClick="btnCreateInOutBill_Click"></asp:Button>
                <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_Delete%>"
                    CssClass="ButtonDel" OnClientClick="return CheckDel()" /> <%--删除--%>	
            </td>
            <td class="InputLabel" align="right" style="width: 10%">
                <asp:Label ID="Label8" CssClass="InputLabel" runat="server" Text="<%$ Resources:Lang, Common_SearchCinv%>"></asp:Label>：<%--查询料号--%>
            </td>
            <td style="width: 10%">
                <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="95%"
                    MaxLength="50"></asp:TextBox>
            </td>
            <td align="left" style="width: 10%">
                <asp:Button ID="Button1" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"></asp:Button> <%--查询--%>
            </td>

        </tr>
        <tr>
            <td colspan="4">
                <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdALLOCATE_D" runat="server" AllowPaging="True"
                        BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%"
                        AutoGenerateColumns="False"
                        OnRowDataBound="grdALLOCATE_D_RowDataBound"
                        ShowHeader="True" CssClass="Grid" PageSize="15">
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
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid" BorderWidth="0px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="IDS" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, Common_IDS %>" Visible="false"><%--子表编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, Common_ID%>" Visible="false"><%--主表编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_LiaoHao%>"><%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName1%>"><%--品名--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IQUANTITY" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, Common_IQUANTITY%>"><%--数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CPOSITIONCODE%>"><%--原始储位--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CPOSITION%>"><%--原始储位名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CTOPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CTOPOSITIONCODE%>"><%--目的储位--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CTOPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CTOPOSITION%>"><%--目的储位名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="150px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CINVBARCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CINVBARCODE%>" Visible="false"><%--物料条码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DINDATE" DataFormatString="{0:yyyy-MM-dd}" HeaderText="<%$ Resources:Lang, FrmALLOCATEEdit_DINDATE%>"><%--调拨日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINPERSONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEEdit_CINPERSONCODE%>"><%--调拨人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CMIDPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEEdit_CMIDPOSITIONCODE%>"><%--原始目的储位--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CDEFINE1" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEEdit_CDEFINE1%>"><%--机种--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CMEMO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO%>" Visible="false"> <%--备注--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CSTATUS" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>" Visible="false"><%--状态--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField="" Visible="false"
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                        </Columns>
                    </asp:GridView>
                    <ul class="OneRowStyle">
                        <li>
                            <webdiyer:aspnetpager id="AspNetPager1" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="AspNetPager1_PageChanged"
                                FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false" xmlns:webdiyer="wuqi.webdiyer">
                               </webdiyer:aspnetpager>
                        </li>
                        <li>
                            <div><%= Resources.Lang.Base_Gong%><%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
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
                var controls = document.getElementById("<%=grdALLOCATE_D.ClientID %>").getElementsByTagName("input");

                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmALLOCATEEdit_Msg06%>"); //请选择需要删除的项！
                    return false;
                }
                 if (confirm("<%= Resources.Lang.FrmALLOCATEEdit_Msg07%>")) {  //你确认删除吗？
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
    </table>
</asp:Content>
