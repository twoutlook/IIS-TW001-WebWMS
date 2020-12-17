<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--<%$ Resources:Lang, FrmBASE_OPERATOR_AREAPart_Title01%>" CodeFile="FrmBASE_OPERATOR_AREAPart.aspx.cs" Inherits="FrmBASE_OPERATOR_AREA" %><%--设置区域--%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="ShowAREADiv.ascx" TagName="ShowAREADiv" TagPrefix="ucArea" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script type="text/javascript">
        function CheckSel() {
            var number = 0;
            var controls = document.getElementById("<%=grdBAS_AREA.ClientID %>").getElementsByTagName("input");

            for (var i = 0; i < controls.length; i++) {
                var e = controls[i];
                if (e.type != "CheckBox") {
                    if (e.checked == true) {
                        number = number + 1;
                    }
                }
            }
            var selType = document.getElementById("ctl00_ContentPlaceHolderMain_ddlAllType").value;
            if (selType == "1") {
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmBASE_WL_AREA_Msg01%>");//请至少选择一个区域！
                    return false;
                }
            }
            return true;
        }

        function SelIDCancelAll() {
            document.GetElementById("ctl00_ContentPlaceHolderMain_cboxAllArea").checked = false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBASE_WL_AREA_Title03%><%--区域设置--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <ucArea:ShowAREADiv ID="ucShowArea" runat="server" />
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
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
                    <tr style="display: none;">
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
                            <asp:Label ID="lblPALLETID" runat="server" Text="<%$ Resources:Lang, FrmBASE_WL_AREA_lblPALLETID%>"></asp:Label>： <%--区域名称--%>
                        </td>
                        <td style="width: 15%">
                            <asp:TextBox ID="txtAreaName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label runat="server" ID="labBlCw" Text="<%$ Resources:Lang, FrmBASE_WL_AREA_labBlCw%>"></asp:Label>：<%--備料儲位--%>
                        </td>
                        <td style="width: 15%">
                            <asp:TextBox runat="server" ID="txtblCw" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblPALLETNAME" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：<%--创建人--%>
                        </td>
                        <td style="width: 15%">
                            <asp:TextBox ID="txtCreateUser" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_CreateDate%>"></asp:Label>： <%--创建日期--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtFromDate" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtFromDate','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao%>"></asp:Label>：<%--到--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtEndDate" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtEndDate','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmBASE_WL_AREA_Label3%>"></asp:Label>：<%--是否允许超发--%>
                        </td>
                        <td>
                            <asp:DropDownList ID="cbCF" runat="server" Width="95%">
                            </asp:DropDownList>
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
                        <td colspan="6" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"> <%--查询--%>
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="center">
                <%--<asp:CheckBox ID="cboxAllArea" runat="server" Text="关联所有储位" />--%>
                <asp:DropDownList ID="ddlAllType" runat="server" Width="150px">
                </asp:DropDownList>
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" Text="<%$ Resources:Lang, Common_btnSave%>" OnClick="btnSave_Click" OnClientClick="return CheckSel();" /><%--保存--%>
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
                &nbsp;&nbsp;
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>" Visible="false"><%--新增--%>
                </asp:Button>
                &nbsp;&nbsp;<asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_Delete%>"
                    CssClass="ButtonDel" Visible="false" OnClientClick="return CheckDel()" /> <%--删除--%>	
            </td>
        </tr>
       
        <tr>
            <td>
                <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdBAS_AREA" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                      
                        OnRowDataBound="grdBAS_AREA_RowDataBound" CssClass="Grid" PageSize="15" OnDataBinding="grdBAS_AREA_DataBinding">
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
                            <asp:BoundField DataField="AREA_NAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_WL_AREA_lblPALLETID%>"><%--区域名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MEMO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO%>"><%--备注--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HANDOVER_CARGO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_WL_AREA_labBlCw%>"> <%--備料儲位--%>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HANDOVER_CARGO_NAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_WL_AREA_HANDOVER_CARGO_NAME%>"><%--備料儲位名稱--%>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <HeaderStyle HorizontalAlign="Left" Wrap="False" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATEOWNER" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"><%--创建人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, Common_CreateDate%>"  >
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                      <ul class="OneRowStyle" >
                    <li>
                        <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                           FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                        </webdiyer:aspnetpager>
                    </li>
                    <li>
                        <div><%= Resources.Lang.Base_Gong%><%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                    </li>
                </ul>
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdBAS_AREA.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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

            function CheckDel() {
                var number = 0;
                var controls = document.getElementById("<%=grdBAS_AREA.ClientID %>").getElementsByTagName("input");

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
