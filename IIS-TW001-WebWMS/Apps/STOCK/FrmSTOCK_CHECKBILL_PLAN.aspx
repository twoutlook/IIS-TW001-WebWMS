<%@ Page Title="盘点计划维护" Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="FrmSTOCK_CHECKBILL_PLAN.aspx.cs" Inherits="Apps_STOCK_FrmSTOCK_CHECKBILL_PLAN" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/Js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="Link1" runat="server" />
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Group_StockManage %>-&gt;<%= Resources.Lang.FrmSTOCK_CHECKBILL_PLAN_PageName %><%--盘点计划维护--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
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
                    <span style="margin: 0px 7px 20px 0px; float: left" class="ui-icon ui-icon-alert"></span><%= Resources.Lang.FrmSTOCK_CHECKBILL_PLAN_Tips_Msg %><%--已经存在有效的盘点计划，是否继续设置此计划为有效？--%>
                </p>
            </div>
            <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
                <div class="ui-dialog-buttonset">
                    <asp:Button ID="okbutton" CssClass="ButtonSave" Text="<%$ Resources:Lang,FrmSTOCK_CHECKBILL_PLAN_Tips_Shi %>" OnClick="okbutton_Click" runat="server" /><%--是--%>
                    <asp:Button ID="cancelbutton" CssClass="ButtonBack" Text="<%$ Resources:Lang,FrmSTOCK_CHECKBILL_PLAN_Tips_Fou %>" OnClick="cancelbutton_Click" runat="server" /><%--否--%>
                </div>
            </div>
        </div>
    </asp:Panel>

    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.WMS_Common_SearchTitle %>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.WMS_Common_FoldAlt %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
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
                            <asp:Label ID="lblPlanID" runat="server" Text="<%$ Resources:Lang,FrmSTOCK_CHECKBILL_PLAN_PlanId %>"></asp:Label>：<%--盘点计划ID--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPlanID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblPlan" runat="server" Text="<%$ Resources:Lang,FrmSTOCK_CHECKBILL_PLAN_PlanName %>"></asp:Label>：<%--盘点名--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPlan" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
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
                        <td colspan="6" align="center" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left">&nbsp;&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" Width="100">
          <%--      <asp:ListItem Value="1">有效</asp:ListItem>
                <asp:ListItem Value="0">无效</asp:ListItem>--%>
            </asp:DropDownList>
                &nbsp;<asp:Button ID="btnUpStatus" runat="server" CssClass="ButtonConfig" Text="<%$ Resources:Lang,FrmSTOCK_CHECKBILL_PLAN_TypeSet %>" OnClick="btnUpStatus_Click" OnClientClick="return CheckDel();"></asp:Button><%--状态设置--%>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left"></td>
        </tr>

        <tr>
            <td>
                <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdPlan" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        CssClass="Grid" PageSize="15" DataKeyNames="ID">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:TemplateField>
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
                            <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="ID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PLANID" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmSTOCK_CHECKBILL_PLAN_PlanId %>"><%--盘点计划ID--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PLAN_NAME" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmSTOCK_CHECKBILL_PLAN_PlanName %>"><%--盘点名--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PLAN_DESCRIBE" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmSTOCK_CHECKBILL_PLAN_PlanDesc %>"><%--盘点描述--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FREEZE_DATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HeaderText="<%$ Resources:Lang,WMS_Common_Element_PanDianDate %>"><%--盘点日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CSTATUSName" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_GridView_Status %>"><%--状态--%>
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
                //var controls = document.getElementById("<%=grdPlan.ClientID %>").getElementsByTagName("input");

                //for (var i = 0; i < controls.length; i++) {
                //    var e = controls[i];
                //    if (e.type != "CheckBox") {
                //        if (e.checked == true) {
                //            number = number + 1;
                //        }
                //    }
                //}
                $.each($("#<%=grdPlan.ClientID%>").find("span input"), function (i, item) {
                    if (item.checked == true) {
                        number = number + 1;
                    }
                });
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmSTOCK_CHECKBILL_PLAN_NeedSelect %>");//请选择需要更改的项！
                    return false;
                }
                else if (number > 1) {
                    alert("<%= Resources.Lang.FrmSTOCK_CHECKBILL_PLAN_OnlyOne %>");//只能选择一条更改的项！
                        return false;
                    }
            }
        </script>
    </table>
</asp:Content>
