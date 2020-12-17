<%@ Page Title="--<%$ Resources:Lang, FrmChangeByInAsnEdit_title1 %>" Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master"
    AutoEventWireup="true" CodeFile="FrmChangeByInAsnEdit.aspx.cs" Inherits="Apps_RD_FrmChangeByInAsnEdit" %>
<%--入库变更单(通知单)--%>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Src="ShowINASN.ascx" TagName="ShowINASN" TagPrefix="ucIA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js"
        type="text/javascript"></script>
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css"
        id="cssUrl" runat="server" />
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="Link1" runat="server"/>  
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />

    <script type="text/javascript">
        //失去焦点提交修改，隐藏显示框
        function submitData(type, dataType, ids, qty, line_Qty, positionCode, txt) {
            var isOk = true;
            //非空验证
            if (type == "") {
                alert(<%=Resources.Lang.FrmChangeByInAsnEdit_JSMsg1 %>);// "类型不能为空！"
                isOk = false;
            }
            else if (ids == "") {
                alert(<%=Resources.Lang.FrmChangeByInAsnEdit_JSMsg2 %>);//"IDS不能为空！"
                isOk = false;
            }
            else if (dataType == "") {
                alert(<%=Resources.Lang.FrmChangeByInAsnEdit_JSMsg3 %>);//"数据类型不能为空！"
                isOk = false;
            }
            if (isOk) {
                if (dataType == "Qty") {
                    //                        alert(qty);
                    if (qty == "") {
                        alert(<%=Resources.Lang.FrmChangeByInAsnEdit_JSMsg4 %>);//"数量不能为空！"
                        isOk = false;
                    }
                    else if (isNaN(qty)) {
                        alert(<%=Resources.Lang.FrmChangeByInAsnEdit_JSMsg5 %>);//"数量必须为数值型数据！"
                        isOk = false;
                    }
                    else if (qty < 0) {
                        alert(<%=Resources.Lang.FrmChangeByInAsnEdit_JSMsg6 %>);//"数量不能小于 0 ！"
                        isOk = false;
                    }
                }
                else {
                    alert(<%=Resources.Lang.FrmChangeByInAsnEdit_JSMsg7 %>);//"数据类型错误不能提交！"
                    isOk = false;
                }

                if (isOk) {
                    var i = Math.random() * 10000 + 1;
                    $.get("../BASE/SubmitDate.aspx?i=" + i,
                          { Type: type, DataType: dataType, Ids: ids, Qty: qty, Line_Qty: line_Qty, PositionCode: positionCode },
                          function (data) {
                              $("#showMsgTd").html(data);
                          });
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt;<%=Resources.Lang.FrmChangeByInAsnEdit_Content2%><%--入库变更单(通知单)--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

    <ucIA:ShowINASN ID="ucINASN_Div" runat="server" />  

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
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, Common_CticketCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="40" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmChangeByInAsnEdit_lblCERPCODE %>"></asp:Label>：<%--通知单单号：--%>

                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtOutAsnCticketCode" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                            <asp:TextBox ID="txtOutAsnCticketID" runat="server" CssClass="NormalInputText"
                                Width="95%" MaxLength="30" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, Common_CreateUser %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false" MaxLength="50" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, Common_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server"  Text="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"></asp:Label>：<%--备注：--%>
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
                    <tr align="center">
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                                Text="<%$ Resources:Lang, Common_Save %>" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnOK" runat="server" CssClass="ButtonConfig5" OnClick="btnOK_Click"
                                Text="<%$ Resources:Lang, FrmChangeByInAsnEdit_btnOK %>" /><%--执行变更--%>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>" CausesValidation="false" />
                             &nbsp;&nbsp;
                            <asp:Button ID="btnPrint" runat="server" CssClass="ButtonPrint" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG12 %>" OnClick="btnPrint_Click">
                            </asp:Button>   
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
    <table id="TabMain0" style="height: 100%; width: 100%">
        <tr valign="top">
            <td valign="top" align="left">
                <asp:Button ID="btnDel" runat="server" Visible="false" CssClass="ButtonDel" Text="<%$ Resources:Lang, Common_DelBtn %>"
                    OnClick="btnDel_Click"></asp:Button>
            </td>
            <td id="showMsgTd" style="width: 200px; color: Red; vertical-align: middle;">
                &nbsp;
            </td>
            <td class="InputLabel" align="right" style="width: 9%" valign="middle">
                <asp:Label ID="Label8" CssClass="InputLabel" runat="server" Visible="false" Text="<%$ Resources:Lang, FrmINASNEdit_Label5 %>"></asp:Label>：<%--查询料号：--%>
            </td>
            <td style="width: 9%" valign="middle">
                <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="95%"
                    Visible="false" MaxLength="50"></asp:TextBox>
            </td>
            <td align="left" style="width: 6%" valign="middle">
                <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" Visible="false"
                    OnClick="btnSearch_Click"></asp:Button>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" colspan="10">
              <%--  <cc1:DataGridNavigator3 ID="grdNavigatorInChange_d" runat="server" GridID="grdInChange_d"
                    Visible="false" ShowPageNumber="false" ExcelName="InChange_d" IsDbPager="True"
                    OnGetExportToExcelSource="grdNavigatorInChange_d_GetExportToExcelSource" />--%>
            </td>
        </tr>
        <tr>
            <td colspan="10">
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdInChange_d.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                    </script>
                    <asp:GridView ID="grdInChange_d" Visible="false" runat="server" AllowPaging="True"
                        BorderColor="Teal" DataKeyNames="IDS" BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                        Width="100%" AutoGenerateColumns="False" OnPageIndexChanged="grdInChange_d_PageIndexChanged"
                        OnPageIndexChanging="grdInChange_d_PageIndexChanging" CssClass="Grid" PageSize="15"
                        OnRowDataBound="grdInChange_d_RowDataBound">
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
                            <asp:BoundField DataField="IDS" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG18 %>" Visible="False"><%--子表编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODELINE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmChangeByInAsnEdit_CERPCODELINE %>"><%--子项ERP单号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="OLDNUM" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmChangeByInAsnEdit_OLDNUM %>"><%--原数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmChangeByInAsnEdit_txtNowNum %>"><%--现数量--%>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNowNum" runat="server" Text='<%# Eval("NOWNUM") %>' MaxLength="8"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" Wrap="False" Width="120px" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CSTATUS" HeaderText="<%$ Resources:Lang, Common_Cstatus %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATE_OWNER" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle28 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATE_TIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle18 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px"/>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>

                    <ul class="OneRowStyle">
	                    <li >
		                     <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
			                    FirstPageText="<%$ Resources:Lang, Common_FirstPage%>" LastPageText="<%$ Resources:Lang, Common_LastPage%>" NextPageText="<%$ Resources:Lang, Common_NextPage%>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
			                    AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
		                    </webdiyer:AspNetPager>
	                    </li>
	                    <li>
		                    <div><%=Resources.Lang.Common_TotalPage %> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_TotalPage1 %></div>
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
</asp:Content>
