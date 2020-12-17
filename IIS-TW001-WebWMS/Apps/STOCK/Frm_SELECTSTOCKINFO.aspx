<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" Title="--储位管理" CodeFile="Frm_SELECTSTOCKINFO.aspx.cs" Inherits="Frm_SELECTSTOCKINFO" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="Link1" runat="server" />
    <script src="../../Layout/Js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="Link2" runat="server" />
    <style type="text/css">
        .ui-autocomplete-loading {
            background: white url('../../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }

        .select {
            cursor: pointer;
            position: relative;
            left: -25px;
            top: 4px;
        }

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
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.Frm_SELECTSTOCKINFO_PageName %><%--选择盘点库存--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

    <ajaxToolkit:ModalPopupExtender runat="server" ID="msgBox" TargetControlID="hiddenTargetControlForModalPopup"
        PopupControlID="messageboxPanel" BackgroundCssClass="messagebox_parent" DropShadow="False"
        RepositionMode="RepositionOnWindowResizeAndScroll" />
    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none;" />
    <asp:Panel runat="server" ID="messageboxPanel" Style="display: none; font-weight: bold; position: relative; left: -15px; top: 2px; width: 300px; height: 100px;">
        <div style="z-index: 1002; outline-style: none; outline-color: invert; outline-width: 0px; width: 300px; height: auto;"
            class="ui-dialog ui-widget ui-widget-content ui-corner-all  ui-dialog-buttons">
            <div class="ui-dialog-titlebar ui-corner-all ui-helper-clearfix"
                style="border: 1px solid #aaaaaa; background: #BACBE2 url(images/ui-bg_highlight-soft_75_cccccc_1x100.png) 50% 50% repeat-x; color: #222222; font-weight: bold;">
                <span id="ui-id-4" class="ui-dialog-title"><%= Resources.Lang.WMS_Common_Tips_TiShi %></span><a class="ui-dialog-titlebar-close ui-corner-all"></a>
            </div>
            <div style="min-height: 0px; width: auto; height: 32px" id="DIV1" class="ui-dialog-content ui-widget-content">
                <p>
                    <span style="margin: 0px 7px 20px 0px; float: left" class="ui-icon ui-icon-alert"></span><%= Resources.Lang.Frm_SELECTSTOCKINFO_YiCunZai %><%--已经存在盘点单明细，是否继续（选择"是"将删除原先盘点单明细）？--%>
                </p>
            </div>
            <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
                <div class="ui-dialog-buttonset">
                    <asp:Button ID="okbutton" CssClass="ButtonSave"
                        Text="是" OnClick="okbutton_Click" runat="server" />
                    <asp:Button ID="cancelbutton" CssClass="ButtonBack"
                        Text="否" OnClick="cancelbutton_Click" runat="server" />
                </div>
            </div>
        </div>
    </asp:Panel>


    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="3">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                            height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.WMS_Common_SearchTitle %>
                        </th>
                        <th style="border-left-width: 0px" align="right" colspan="1">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.WMS_Common_FoldAlt %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr style="display: none;">
                        <td colspan="4">
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
                        <td class="InputLabel" style="width: 13%; display:none;">
                            <asp:Label ID="lblCCARGOID" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CwareCode %>"></asp:Label>：<%--仓库编码--%>
                        </td>
                        <td style="width: 20%; display:none;">
                            <asp:TextBox ID="txtCWAREHOUSE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="lblCPOSITION" runat="server" Text="<%$ Resources:Lang, Common_CPOSITION%>"></asp:Label>：<%--原始储位名称--%>
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCPOSITION" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCPOSITIONCODE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_CpositionCode %>"></asp:Label>：<%--储位编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPOSITIONCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"></asp:Label>：<%--料号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblRANK_FINAL" runat="server" Text="批/序號(RANK)"></asp:Label>：<%--rank--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtRANK_FINAL" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="1"></asp:TextBox>
                        </td>
                    </tr>

                    <tr style="display: none;">
                        <td colspan="4">
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
                        <td colspan="4" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td align="center">&nbsp;<asp:Button ID="btnSaveALL" runat="server" CssClass="ButtonSave4" Text="<%$ Resources:Lang,Frm_SELECTSTOCKINFO_SaveAll %>" OnClick="btnSaveALL_Click" Visible="False"></asp:Button><%--保存全部--%>
                &nbsp;<asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang,WMS_Common_Button_Save %>" OnClientClick="return CheckDel();" />
                &nbsp;&nbsp;<asp:Button ID="btnDelete" Visible="false" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" CssClass="ButtonDel" />
                <asp:Button ID="btnBack" runat="server" CausesValidation="false" CssClass="ButtonBack" Text="<%$ Resources:Lang,WMS_Common_Button_Back %>" />
            </td>
        </tr>
        <tr class="tableCell">
            <td>
                <div style="height: 450px; overflow-x: scroll; overflow-y: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdBASE_CARGOSPACE" runat="server" BorderColor="Teal" BorderStyle="Solid"
                        BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False" OnPageIndexChanged="grdBASE_CARGOSPACE_PageIndexChanged"
                        OnPageIndexChanging="grdBASE_CARGOSPACE_PageIndexChanging" OnRowDataBound="grdBASE_CARGOSPACE_RowDataBound" PageSize="50"
                        CssClass="Grid" AllowPaging="True" OnDataBound="grdBASE_CARGOSPACE_DataBound">
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
                            <asp:BoundField DataField="ID" DataFormatString="" HeaderText="ID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CWAREHOUSE" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CwareName %>"><%--仓库名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CWAREHOUSECODE" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CwareCode %>"><%--仓库编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CpositionName %>"><%--储位名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CpositionCode %>"><%--储位编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"><%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <%-- 以下21-10-2020 by Qamar --%>
                            <asp:TemplateField HeaderText="批/序號(RANK)"><%--rank--%>
                                <ItemTemplate>
                                    <asp:Label ID="lblRANK_FINAL" runat="server" Text="">
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="40px"/>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <%-- 以上21-10-2020 by Qamar --%>
                            <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>"><%--品名--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="100px" />
                            </asp:BoundField>
                               <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>" ><%--规格--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IQTY" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Quantity %>"><%--数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IOCCUPYQTY" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang,WMS_Common_Element_ZhanYongQuantity %>"><%--占用数量--%>
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
                var controls = document.getElementById("<%=grdBASE_CARGOSPACE.ClientID %>").getElementsByTagName("input");

                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
                if (number == 0) {
                    alert("<%= Resources.Lang.WMS_Common_Tips_NeedSaveOptions %>");//请选择需要保存的项！
                    return false;
                }
                return true;
            }

        </script>
    </table>

</asp:Content>
