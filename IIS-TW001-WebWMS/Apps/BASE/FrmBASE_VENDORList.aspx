<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--" CodeFile="FrmBASE_VENDORList.aspx.cs" Inherits="BASE_FrmBASE_VENDORList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.FrmBASE_VENDORList_Title01%><%--基礎資料-&gt;供应商管理--%>
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
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                            height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr style="display: none;">
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
                            <asp:Label ID="lblCVENDORID" runat="server" Text="<%$ Resources:Lang, FrmWAREHOUSEList_LEADERCODE%>"></asp:Label>： <%--供应商编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCVENDORID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCVENDOR" runat="server" Text="<%$ Resources:Lang, FrmWAREHOUSEList_LEADER%>"></asp:Label>：<%--供应商名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCVENDOR" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTNPE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_CTNPE%>"></asp:Label>：<%--供应商类型--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTNPE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCALIAS" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCALIAS%>"></asp:Label>： <%--助记码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCALIAS" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCERPCODE%>"></asp:Label>： <%--ERP编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCONTACTPERSON" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCCONTACTPERSON%>"></asp:Label>： <%--联系人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCONTACTPERSON" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="drCSTATUS" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblILEVEL" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblILEVER%>"></asp:Label>：  <%--级别--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtILEVEL" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCreateTimeFrom" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label>：  <%--创建时间--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtCreateTimeFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateTimeFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCreateTimeTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao%>"></asp:Label>： <%--到--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtCreateTimeTo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateTimeTo','y-mm-dd',0);" />
                            <span class="requiredSign" style="left: -50px">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"><%--查询--%>
                            </asp:Button>
                        </td>
                    </tr>
                    <tr style="display: none;">
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

                                function CheckDel() {
                                    var number = 0;
                                    var controls = document.getElementById("<%=grdBASE_VENDOR.ClientID %>").getElementsByTagName("input");

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
                                    if (confirm("<%= Resources.Lang.FrmALLOCATEEdit_Msg07%>")) { //你确认删除吗？
                                        return true;
                                    }
                                    else {
                                        return false;
                                    }
                                }
                            </script>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"><%--新增--%>
                </asp:Button>
                <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_Delete%>" CssClass="ButtonDel"
                    OnClientClick="return CheckDel()" />
                <%--删除--%>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdBASE_VENDOR" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="grdBASE_VENDOR_RowDataBound" CssClass="Grid" PageSize="15" ShowHeaderWhenEmpty="true">
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
                            <asp:BoundField DataField="IDS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="false"><%--编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CVENDORID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmWAREHOUSEList_LEADERCODE%>"><%--供应商编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CVENDOR" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmWAREHOUSEList_LEADER%>"><%--供应商名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CCONTACTPERSON" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCCONTACTPERSON%>"><%--联系人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPHONE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCPHONE%>"><%--联系电话--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CADDRESS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_CADDRESS%>" Visible="False"><%--联系地址--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CTNPE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_CTNPE%>"><%--供应商类型--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ILEVEL" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_lblILEVER%>"><%--级别--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CALIAS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCALIAS%>"><%--助记码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCERPCODE%>"><%--ERP编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CMEMO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO%>" Visible="False"><%--备注--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CSTATUS" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"><%--状态--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATEOWNER" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"><%--创建人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, Common_CreateDate%>"><%--创建日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="80px" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>"><%--编辑--%>
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
                            <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                        </li>
                    </ul>
                </div>
                <%--<cc1:DataGridNavigator3 ID="grdNavigatorBASE_CLIENT" runat="server" GridID="grdBASE_CLIENT"
                    ShowPageNumber="false" ExcelName="BASE_CLIENT" IsDbPager="True" GetExportToExcelSource="grdNavigatorBASE_CLIENT_GetExportToExcelSource" />--%>

                <script type="text/javascript" language="javascript">
                    //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                    //settingPad("<%= grdBASE_VENDOR.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                </script>
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
