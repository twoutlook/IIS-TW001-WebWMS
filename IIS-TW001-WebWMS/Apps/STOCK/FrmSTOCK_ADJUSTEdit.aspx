<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmSTOCK_ADJUSTEdit.aspx.cs" Inherits="STOCK_FrmSTOCK_ADJUSTEdit" Title="--11" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
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
    <%= Resources.Lang.WMS_Common_Group_StockManage %><%--庫存管理--%>-&gt;<%= Resources.Lang.WMS_Common_Element_StockTiaoZheng %><%--库存调整单--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />

    <table id="Table3" style="width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top" class="tableCell">
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
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblcticketcode" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_Cticketcode %>"></asp:Label><%--单据号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCticketCode" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"
                                MaxLength="20"></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblcreateuser" runat="server" Text="<%$ Resources:Lang,FrmKUCUNTIAOZHENGReport_lbCINPERSONCODE %>"></asp:Label>：<%--调整人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcreateowner" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang,FrmBar_SNManagement_RuleStatus %>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="99%" OnSelectedIndexChanged="ddlCSTATUS_SelectdChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>

                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblcreatetime" runat="server" Text="<%$ Resources:Lang,FrmKUCUNTIAOZHENGReport_lblDINDATEFromFrom %>"></asp:Label>：<%--调整日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcreatetime" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang,FrmOUTASNList_AuditUser %>"></asp:Label>：<%--审核人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtReviewUser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang,FrmOUTASNList_AuditDate %>"></asp:Label>：<%--审核日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtReviewDate" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATE','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblReason" runat="server" Text="<%$ Resources:Lang,FrmSTOCK_ADJUSTEdit_TiaoZhengReason %>"></asp:Label>：<%--调整原因--%>
                        </td>
                        <td style="width: 21%" colspan="5">
                            <asp:TextBox ID="txtreason" runat="server" TextMode="MultiLine" Width="99%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>

        <tr class="tableCell">
            <td align="center" style="padding: 15px 0px;" colspan="6">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang,WMS_Common_Button_Save %>" /><%--保存--%>
                &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang,WMS_Common_Button_Back %>" CausesValidation="false" /><%--返回--%>
            </td>
        </tr>

        <tr valign="top" class="tableCell">
            <td valign="top" align="left">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang,WMS_Common_Button_New %>" OnClick="btnNew_Click"></asp:Button>
                &nbsp;&nbsp;<asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" CssClass="ButtonDel" OnClientClick="return CheckCancel()" /><%--删除--%>
            </td>
            <td align="right" style="width: 60%">
                <asp:Label ID="Label8" CssClass="InputLabel" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_SearchCinvcode %>"></asp:Label>：<%--查询料号--%>
                <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="20%" MaxLength="50"></asp:TextBox>
                <asp:Label ID="Label3" CssClass="InputLabel" runat="server" Text="RANK"></asp:Label>：<%--rank--%>
                <asp:TextBox ID="txtRANK_FINAL" runat="server" CssClass="NormalInputText" Width="40px" MaxLength="1"></asp:TextBox>
                <asp:Button ID="btnSearch1" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
            </td>
        </tr>
        <tr valign="top" style="display: none;" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="tabCondition">
                    <tr>
                        <th align="left" colspan="6">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.WMS_Common_SearchTitle %>
                        </th>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_ZhuBiaoCode %>"></asp:Label>：<%--主表编号--%>
                        </td>
                        <td style="width: 20%" colspan="5">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>

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
        <tr class="tableCell">
            <td>
                <div style="height: 480px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdSTOCK_ADJUST_D" DataSourceID="" runat="server"
                        AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                        Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="grdSTOCK_ADJUST_D_RowDataBound"
                        ShowHeader="True" CssClass="Grid" PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"
                            Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""
                            Wrap="False" />
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
                            <%-- 以下 29-10-2020 by Qamar --%>
                            <%-- DataField從CINVCODE改PART, 並且加入RANK_FINAL --%>
                            <asp:BoundField DataField="PART" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Cinvcode %>"><%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RANK_FINAL" DataFormatString="" HeaderText="批/序號(RANK)"><%--rank--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <%-- 以上 29-10-2020 by Qamar --%>
                            <asp:BoundField DataField="cinvname" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Cinvname %>"><%--品名--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                                 <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>" ><%--规格--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_Pallte %>" ItemStyle-Width="50%"> <%--栈板号--%>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSN" runat="server" Width="50%" Text='<%# Eval("SNCODE") %>' Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="cpositioncode" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CpositionCode %>"><%--储位编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>

                            <asp:BoundField DataField="cpositionname" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PartnumName %>"><%--储位名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="oriqty" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, Frm_STOCKINFO_Adiust_OriQuantity %>"><%--原始数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="newqty" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, Frm_STOCKINFO_Adiust_NewQuantity %>"><%--调整数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>

                            <asp:BoundField DataField="createtime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="<%$ Resources:Lang,WMS_Common_CreateDateFrom %>"><%--创建日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="createowner" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_CreateUser %>"><%--创建人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <ul class="OneRowStyle">
                        <li>
                            <webdiyer:aspnetpager id="AspNetPager1" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="AspNetPager1_PageChanged"
                                FirstPageText="<%$ Resources:Lang,WMS_Common_Pager_First %>" LastPageText="<%$ Resources:Lang,WMS_Common_Pager_Last %>" NextPageText="<%$ Resources:Lang,WMS_Common_Pager_Next %>" PrevPageText="<%$ Resources:Lang,WMS_Common_Pager_Front %>" ShowPageIndexBox="Never"
                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false" xmlns:webdiyer="wuqi.webdiyer">
                                   </webdiyer:aspnetpager>
                        </li>
                        <li>
                            <div><%= Resources.Lang.WMS_Common_Pager_Total %> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.WMS_Common_Pager_Count %></div>
                        </li>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function ChangeDivWidth() {
            document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
        }
        window.onresize = ChangeDivWidth;
        ChangeDivWidth();


        function CheckCancel() {
            var number = 0;
            var controls = document.getElementById("<%=grdSTOCK_ADJUST_D.ClientID%>").getElementsByTagName("input");
            for (var i = 0; i < controls.length; i++) {
                var e = controls[i];
                if (e.type != "CheckBox") {
                    if (e.checked == true) {
                        number = number + 1;
                    }
                }
            }
            if (number == 0) {
                alert("<%= Resources.Lang.WMS_Common_DeleteTips %>");//请选择需要删除的项！
                return false;
            }
        }
    </script>

</asp:Content>
