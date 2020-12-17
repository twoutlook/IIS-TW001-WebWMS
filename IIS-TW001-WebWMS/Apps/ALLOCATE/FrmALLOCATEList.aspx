<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="<%$ Resources:Lang, FrmALLOCATEList_Title1 %>" CodeFile="FrmALLOCATEList.aspx.cs" Inherits="ALLOCATE_FrmALLOCATEList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>

    <script type="text/javascript">
		function CheckKZ() {
			var number = 0;
			$.each($("#<%=grdALLOCATE.ClientID%>").find("span input"), function (i, item) {
				if (item.checked == true) {
					number = number + 1;
				}
			});
			if (number == 0) {
				alert("<%= Resources.Lang.FrmALLOCATEList_AlterKouZhang %>");//请选择需要扣帐的项！
				return false;
			}
		}

		function CheckMail() {
			var number = 0;
			$.each($("#<%=grdALLOCATE.ClientID%>").find("span input"), function (i, item) {
			if (item.checked == true) {
				number = number + 1;
			}
		});
		if (number == 0) {
			alert("<%= Resources.Lang.FrmALLOCATEList_AlterConfirm %>"); //请选择需要确认的项！
				return false;
			}
		}
		function CheckCancel() {
			var number = 0;
			$.each($("#<%=grdALLOCATE.ClientID%>").find("span input"), function (i, item) {
			if (item.checked == true) {
				number = number + 1;
			}
		});
		if (number == 0) {
			alert("<%= Resources.Lang.FrmALLOCATEList_AlterCancle %>");//请选择需要取消的项！
				return false;
			}
		}

	</script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmALLOCATEList_Title %>-&gt;<%= Resources.Lang.FrmALLOCATEList_Title1 %> <%--库存管理-&gt;调拨单--%> 
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
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.Common_JSCeria %><%--检索条件--%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.Common_ZheDie %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblCTICKETCODE %>"></asp:Label>：<%--单据号--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDINDATEFromFrom" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblDINDATEFromFrom%>"></asp:Label>：<%--调拨日期--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtDINDATEFrom" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDINDATEToTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao %>"></asp:Label>：<%--到--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtDINDATETo" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATETo','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblCCREATEOWNERCODE %>"></asp:Label>：<%--制单人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblDCREATETIMEFromFrom %>"></asp:Label>：<%--制单日期--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao %>"></asp:Label>：<%--到--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMETo" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSON" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblCAUDITPERSON %>"></asp:Label>：<%--审核人--%>
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCAUDITPERSON" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITTIMEFromFrom" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblDAUDITTIMEFromFrom %>"></asp:Label>：<%--审核日期--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDAUDITTIMEFrom" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITTIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITTIMEToTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao %>"></asp:Label>：<%--到--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDAUDITTIMETo" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITTIMETo','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Label2 %>"></asp:Label>：<%--ERP单号--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtERP" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Label3 %>"></asp:Label>：<%--料号--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtLH" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="批/序號(RANK)"></asp:Label>：<%--RANK_FINAL--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtRANK_FINAL" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Label6 %>"></asp:Label>：<%--栈板号--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtPalletCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Label1 %>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%">
                               <%-- <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="0">未处理</asp:ListItem>
                                <asp:ListItem Value="4">已确认</asp:ListItem>
                                <asp:ListItem Value="1">已审核</asp:ListItem>
                                <asp:ListItem Value="5">調撥中</asp:ListItem>
                                <asp:ListItem Value="6">調撥完成</asp:ListItem>
                                <asp:ListItem Value="2">已完成</asp:ListItem>
                                <asp:ListItem Value="3">已抛砖</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Label4 %>"></asp:Label>：<%--业务类型--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlAllocateType" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Label5 %>"></asp:Label>：<%--调拨类型--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlcdefine1" runat="server" Width="95%">
                                <%--<asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="普通调拨" Value="0"></asp:ListItem>
                                <asp:ListItem Text="出库调拨"  Value="1"></asp:ListItem>
                                <asp:ListItem Text="返库调拨"  Value="2"></asp:ListItem>
                                <asp:ListItem Text="阻挡移库"  Value="3"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>： <%--规格--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtcspec" runat="server"  CssClass="NormalInputText" Width="95%"></asp:TextBox>                          
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, Commona_TimePeriod%>"></asp:Label>：<%--周期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:RadioButtonList ID="rbtList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" BorderStyle="None" CssClass="DateTypeRadio">
                            </asp:RadioButtonList>
                            <div style="float:right">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"></asp:Button> <%--查询--%>
                            </div>
                            <div style="clear:both; float:none; height:0; overflow:hidden"></div> <%--清除浮動 20-10-2020 by Qamar --%>
                        </td>
                        <%--<td colspan="2" style="text-align: right;">--%>
                        <%--    <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"></asp:Button>--%> <%--查询--%>
                        <%--</td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left" style="padding:15px 0px;">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, FrmALLOCATEList_btnNew %>"></asp:Button> <%--新增--%>
                &nbsp;&nbsp;<asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_btnDelete %>" CssClass="ButtonDel" OnClientClick="return CheckDel()" /> <%--删除--%>
                <asp:Button ID="btnCreateInOutBill" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, FrmALLOCATEList_btnCreateInOutBill %>" OnClick="btnCreateInOutBill_Click" Visible="False"></asp:Button><%--调拨--%>
                &nbsp;&nbsp;<asp:Button runat="server" ID="btnYesNo" CssClass="ButtonDo" Text="<%$ Resources:Lang, FrmALLOCATEList_btnYesNo %>" OnClientClick="return CheckMail()" OnClick="btnYesNo_Click" /><%--确认--%>
                <asp:Button runat="server" ID="btnCancel" CssClass="ButtonCancel" Text="撤销" OnClientClick="return CheckMail()" Width="64px" OnClick="btnCancel_Click" Visible="false" /><%--撤销审核--%>
                <asp:Button runat="server" ID="btnSendMail" CssClass="ButtonYY" Text="<%$ Resources:Lang, FrmALLOCATEList_btnSendMail %>" OnClick="btnSendMail_Click" OnClientClick="return CheckMail()" Visible="false" /><%--发送邮件--%>
                &nbsp;&nbsp;<asp:Button ID="btnUpdateSTOCK" runat="server" CssClass="ButtonDo" Text="<%$ Resources:Lang, FrmALLOCATEList_btnUpdateSTOCK %>" OnClick="btnUpdateSTOCK_Click1" OnClientClick="return CheckKZ()"></asp:Button><%--扣账--%>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdALLOCATE" runat="server" AllowPaging="True" BorderColor="Teal" DataKeyNames="id,cdefine1,CSTATUS,ALLOTYPE"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="grdALLOCATE_RowDataBound" CssClass="Grid" PageSize="15">
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
                            <asp:BoundField DataField="CTICKETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_CTICKETCODE %>"><%--单据号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_CERPCODE %>"><%--ERP单号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PALLETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_PALLETCODE %>"><%--栈板号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="username" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_username %>"><%--制单人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DCREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_DCREATETIME %>"><%--制单日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                            </asp:BoundField>                       
                            <asp:BoundField DataField="DEBITTIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_DEBITTIME %>"><%--扣帐时间--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DDEFINE3" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_DDEFINE3 %>"><%--抛转时间--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="newcstatus" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_CSTATUS %>"><%--状态--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="newallotype" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_ALLOTYPE %>"><%--业务类型--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="newcdefine1" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_CDEFINE1 %>"><%--调拨类型--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, FrmALLOCATEList_BianJi %>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_BianJi %>" Text="<%$ Resources:Lang, FrmALLOCATEList_BianJi %>"><%--编辑--%>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, FrmALLOCATEList_Print %>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_Print %>" Text="<%$ Resources:Lang, FrmALLOCATEList_Print %>"><%--打印--%>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>

                        </Columns>
                    </asp:GridView>

                    <ul class="OneRowStyle">
                        <li>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                            </webdiyer:AspNetPager>
                        </li>
                        <li>
                            <div><%= Resources.Lang.Base_Gong %> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data %></div>
                        </li>
                    </ul>
                </div>
            </td>
        </tr>
        <script type="text/javascript">
            ////BUCKINGHA-894
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
            //////BUCKINGHA-894
            function ChangeDivWidth() {
                document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
            }
            window.onresize = ChangeDivWidth;
            ChangeDivWidth();

            function CheckDel() {
                var number = 0;     
                $.each($("#<%=grdALLOCATE.ClientID%>").find("span input"), function (i, item) {
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
        </script>
    </table>
</asp:Content>
