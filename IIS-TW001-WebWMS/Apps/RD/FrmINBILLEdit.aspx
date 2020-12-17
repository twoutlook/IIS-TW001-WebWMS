<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmINBILLEdit.aspx.cs" Inherits="RD_FrmINBILLEdit"
    Title="<%$ Resources:Lang, FrmInbill_InbillCticketCode %>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="ShowINASN_Div.ascx" TagName="ShowINASN" TagPrefix="ucIA" %>
<%@ Register TagPrefix="ExcelBtn" Namespace="DreamTek.ASRS.Business" Assembly="DreamTek.ASRS.Business" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="../../Layout/Js/Help.js" type="text/javascript"></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .displaynone {
            display: none;
        }

        .ui-autocomplete-loading {
            background: white url('../../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }

        .select {
            cursor: pointer;
            position: relative;
            left: -25px;
            top: 4px;
        }

        .SubButton {
            padding: 0px 0px 15px 0px;
        }

            .SubButton input {
                margin-right: 15px;
            }
    </style>
    <script src="../../scripts/AlertJS/js/alert.js" type="text/javascript"></script>
    <script type="text/javascript">
        //失去焦点提交修改，隐藏显示框
        function submitData(type, dataType, ids, qty, line_Qty, positionCode, txt) {

            var isOk = true;
            //非空验证
            if (type == "") {
                //类型不能为空！
                alert("<%=Resources.Lang.FrmINBILLEdit_MSG31 %>");
                isOk = false;
            }
            else if (ids == "") {
                //IDS不能为空！
                alert("<%=Resources.Lang.FrmINBILLEdit_MSG32 %>");
                isOk = false;
            }
            else if (dataType == "") {
                //数据类型不能为空！
                alert("<%=Resources.Lang.FrmINBILLEdit_MSG33 %>");
                    isOk = false;
                }
        if (isOk) {
            if (dataType == "Qty") {
                //                        alert(qty);
                if (qty == "") {
                    //数量不能为空！
                    alert("<%=Resources.Lang.FrmINBILLEdit_MSG34 %>");
                        isOk = false;
                    }
                    else if (isNaN(qty)) {
                        //数量必须为数值型数据！
                        alert("<%=Resources.Lang.FrmINBILLEdit_MSG35 %>");
                        isOk = false;
                    }
                    else if (qty <= 0) {
                        //数量不能小于等于 0 ！
                        alert("<%=Resources.Lang.FrmINBILLEdit_MSG36 %>");
                            isOk = false;
                        }
            }
            else if (dataType == "PositionCode") {
                if (positionCode == "") {
                    isOk = false;
                }
            }
            else {
                //数据类型错误不能提交！
                alert("<%=Resources.Lang.FrmINBILLEdit_MSG37 %>");
                    isOk = false;
                }

            if (isOk) {
                var i = Math.random() * 10000 + 1;
                $.get("../BASE/SubmitDate.aspx?i=" + i,
                    { Type: type, DataType: dataType, Ids: ids, Qty: qty, Line_Qty: line_Qty, PositionCode: $("#" + positionCode).val() },
                    function (data) {
                        //alert(data);
                        $("#ctl00_ContentPlaceHolderMain_showMsgTd").html(data);
                    });
            }
        }
    }

    </script>
    <script type="text/javascript">
        function CheckInbill() {
            var number = 0;
            var controls = document.getElementById("<%=grdINBILL_D.ClientID %>").getElementsByTagName("input");

            for (var i = 0; i < controls.length; i++) {
                var e = controls[i];
                if (e.type != "CheckBox") {
                    if (e.checked == true) {
                        number = number + 1;
                    }
                }
            }
            if (number == 0) {
                //请选择需要入库的项！
                alert("<%=Resources.Lang.FrmINBILLEdit_MSG38 %>");
                return false;
            }
            //你确认入库吗？
            if (confirm("<%= Resources.Lang.FrmINBILLEdit_MSG39 %>")) {
                return true;
            }
            else {
                return false;
            }
        }
        function CheckCancel() {
            var number = 0;
            var controls = document.getElementById("<%=grdINBILL_D.ClientID %>").getElementsByTagName("input");

            for (var i = 0; i < controls.length; i++) {
                var e = controls[i];
                if (e.type != "CheckBox") {
                    if (e.checked == true) {
                        number = number + 1;
                    }
                }
            }
            if (number == 0) {
                //请选择需要取消的项！
                alert("<%=Resources.Lang.FrmINBILLEdit_MSG40 %>");
                return false;
            }
            //你确认取消吗？
            if (confirm("<%= Resources.Lang.FrmINBILLEdit_MSG41 %>")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt;<%--入库单--%><%=Resources.Lang.FrmInbill_InbillCticketCode%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <ucIA:ShowINASN ID="ucINASN_Div" runat="server" />
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

                    <%-- Note by Qamar 2020-11-24 把部分元素class設displaynone並將要顯示的元素放到此處 --%>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, Common_CticketCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmInbill_ErpCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30" onFocus="GetInAsnInfo()"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCASNID" runat="server" Text="<%$ Resources:Lang, FrmInbill_InasnCticketCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCASNID" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>

                            <asp:TextBox ID="txtINASN_id" runat="server" CssClass="NormalInputText" Style="display: none;"
                                Width="95%" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, Common_CreateUser %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, Common_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmINBILLEdit_MSG4 %>" Enabled="False"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%" Enabled="false">
                                <%--  <asp:ListItem Value="0">未处理</asp:ListItem>
                                <asp:ListItem Value="1">已完成</asp:ListItem>
                                <asp:ListItem Value="2">已抛转</asp:ListItem>--%>
                                <%--     <asp:ListItem Value="0"><%= Resources.Lang.FrmDispatchUnitEdit_MsgTitle32 %></asp:ListItem>
                                <asp:ListItem Value="1"><%= Resources.Lang.FrmDispatchUnitList_MsgTitle3 %></asp:ListItem>
                                <asp:ListItem Value="2"><%= Resources.Lang.FrmINBILLEdit_MSG5 %></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG6 %>"></asp:Label>：<%--扣帐人：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtdebituser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG7 %>"></asp:Label>：<%--扣帐时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtdebittime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>


                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, FrmINASNEMTList_MSG2 %>"></asp:Label>：<%--箱号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPalletCode" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmInbill_InBillTYPE %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlIType" runat="server" Width="95%" OnSelectedIndexChanged="ddlIType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG1 %>"></asp:Label>：<%--贸易代码：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCDEFINE1" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">

                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG2 %>"></asp:Label>：<%--币别：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCDEFINE2" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDINDATE" runat="server" Text="<%$ Resources:Lang, FrmInbill_InBillDate %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDINDATE" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmINBILLEdit_MSG4 %>" MaxLength="19"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                            <img border="0" runat="server" id="imgDINDATE" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif"
                                onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATE','y-mm-dd',0);" />
                            <span class="requiredSign" style="left: -50px">*</span>
                            <asp:RequiredFieldValidator ID="rfvtxtDINDATE" runat="server" ControlToValidate="txtDINDATE"
                                ErrorMessage="<%$ Resources:Lang, FrmINBILLEdit_MSG3 %>" Display="None"> </asp:RequiredFieldValidator><%--请填写入库日期!--%>
                            <%--<asp:RegularExpressionValidator ID="revtxtDINDATE" runat="server" ValidationExpression="[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]"
                                ControlToValidate="txtDINDATE" ErrorMessage="请填写有效的入库日期!正确的格式是：yyyy-MM-dd" Display="None"> </asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSON" runat="server" Text="<%$ Resources:Lang, Common_UserOfApproval %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCAUDITPERSON" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITTIME" runat="server" Text="<%$ Resources:Lang, Common_DateOfApproval %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDAUDITTIME" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmINBILLEdit_MSG4 %>" Enabled="false"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                            <%--<img border="0" runat="server" id="imgDAUDITTIME" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITTIME','y-mm-dd',0);" />--%>
                            <%--<asp:RegularExpressionValidator ID="revtxtDAUDITTIME" runat="server" ValidationExpression="[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]"
                                ControlToValidate="txtDAUDITTIME" ErrorMessage="请填写有效的审核日期!正确的格式是：yyyy-MM-dd"
                                Display="None"> </asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG8 %>"></asp:Label>：<%--抛转时间：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtpaozhuantime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmInbill_WorkType %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList runat="server" ID="ddlWorkType" Width="95%">
                                <%--  <asp:ListItem Value=""><%=Resources.Lang.Common_ALL%></asp:ListItem>
                               <asp:ListItem Value=""><%=Resources.Lang.Common_Worktype0%></asp:ListItem>
                               <asp:ListItem Value=""><%=Resources.Lang.Common_Worktype1%></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"></asp:Label>：<%--备注：--%>
                        </td>
                        <td style="width: 20%" colspan="6">
                            <asp:TextBox ID="txtCMEMO" runat="server" Width="98%" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="ID：" Visible="false"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Style="display: none;"
                                Width="95%" ToolTip="<%= Resources.Lang.FrmINBILLEdit_MSG10 %>" Enabled="False"></asp:TextBox><%--格式：最多10位整数，最多4位小数--%>
                        </td>
                        <td class="InputLabel" style="width: 13%"></td>
                        <td style="width: 20%"></td>
                        <td class="InputLabel" style="width: 13%"></td>
                        <td style="width: 20%"></td>
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
            <td style="text-align: center; padding: 15px 0px">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                    Text="<%$ Resources:Lang, Common_Save %>" />

                &nbsp;&nbsp;<asp:Button ID="btnInSTOCK_CURRENT" runat="server" CssClass="ButtonConfig5"
                    Text="<%$ Resources:Lang, FrmINBILLEdit_MSG11 %>" OnClick="btnInSTOCK_CURRENT_Click" Visible="false" /><%--扣帐--%>
                <asp:Button ID="btnCreateInBill" runat="server" CssClass="ButtonSearch" Visible="false"
                    Text="<%$ Resources:Lang, Common_GenerateBtn %>" OnClick="btnCreateInBill_Click"></asp:Button>
                <asp:Button ID="btnInSTOCK_ASRS" runat="server" CssClass="ButtonConfig5" OnClick="btnInSTOCK_ASRS_Click"
                    Text="<%$ Resources:Lang, FrmINBILLEdit_btnInSTOCK_ASRSGuoZhang %>" Visible="false" />&nbsp;<%--AS/RS过账--%>
                <asp:Button ID="btnPrint" runat="server" CssClass="ButtonPrint" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG12 %>" OnClick="btnPrint_Click" Visible="false"><%--打印--%>
                </asp:Button>&nbsp;
                <asp:Button ID="btnRefreshAll" runat="server" CssClass="ButtonRef" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG13 %>" OnClick="btnRefreshAll_Click"></asp:Button>
                <asp:Button ID="btnSeral" runat="server" CssClass="ButtonConfig5" OnClick="btnSeral_Click" Text="<%$ Resources:Lang, WMS_Common_Element_SerialNo %>" Visible="false"></asp:Button><%--序列号--%>
                &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>" CausesValidation="false" />
            </td>
        </tr>
        <tr>
            <td>
                <table id="TabMain0" style="height: 100%; width: 100%" runat="server">
                    <tr valign="top">
                        <td valign="top" align="left" class="SubButton">
                            <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_AddBtn %>" OnClick="btnNew_Click" Visible="false"></asp:Button>
                            <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" OnClientClick="return CheckDel('ctl00_ContentPlaceHolderMain_grdINBILL_D');" Text="<%$ Resources:Lang, Common_DelBtn %>" CssClass="ButtonDel" Visible="false" />
                            <asp:Button ID="btnSetCARGOSPACE" runat="server" CssClass="ButtonConfig" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG14 %>" OnClick="btnSetCARGOSPACE_Click" Enabled="false" Visible="false" /><%--相同储位--%>
                            <asp:Button ID="btnInbill" runat="server" CssClass="ButtonReg4" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG15 %>" OnClientClick="return CheckInbill();" OnClick="btnInbill_Click" Visible="false"></asp:Button><%--AS/RS入库--%>
                            <asp:Button ID="btnCancel" runat="server" CssClass="ButtonCancel" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG16 %>" OnClientClick="return CheckCancel();" OnClick="btnCancel_Click" Visible="false"></asp:Button><%--取消--%>
                        </td>
                        <td id="showMsgTd" style="width: 200px; color: Red;"></td>

                        <%--NOTE by Mark, 09/21, fixed mistaken set visible false--%>
                        <td align="right" style="width: 40%">
                            <asp:Label Visible="true" ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG17 %>"></asp:Label>：<%--查询料号：--%>
                            <asp:TextBox  Visible="true" ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="25%"
                                MaxLength="50"></asp:TextBox>
                            <asp:Button Visible="true"  ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click"></asp:Button>&nbsp;
                            <asp:HiddenField ID="hdnCreateType" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left" colspan="5"></td>
                    </tr>

                    <tr>
                        <td colspan="5">
                            <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                                <asp:GridView ID="grdINBILL_D" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    DataKeyNames="id"
                                    OnRowDataBound="grdINBILL_D_RowDataBound" CssClass="Grid" PageSize="16">
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
                                        <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG19 %>" Visible="False"><%--主表编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="LineID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG20 %>"><%--项次--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                                        </asp:BoundField>

                                        <asp:BoundField visible="false" DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                                        </asp:BoundField>

                                        <%--NOTE by Mark, 09/19--%>
                                         <asp:BoundField DataField="PART" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                                        </asp:BoundField>

                                         <asp:BoundField DataField="RANK_FINAL" DataFormatString="" HeaderText="批/序號(RANK)">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                                        </asp:BoundField>



                                        <%--<asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_CinvName %>">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("CINVNAME") %>' CssClass="mlength"
                                                    ToolTip='<%# Eval("CINVNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>" ><%--规格--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_NUM %>">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGv_IQUANTITY" runat="server" Text='<%# Convert.ToDecimal(Eval("IQUANTITY")).ToString("f2")%>' Enabled="false"
                                                    Width="60px" MaxLength="8"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CINVBARCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG22 %>" Visible="False"><%--物料条码--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CERPCODELINE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG23 %>"><%--ERP项次--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CSTATUS" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Cstatus %>" Visible="False">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CMEMO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Remark %>" Visible="False">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_PartnumNO %>">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGv_CPOSITIONCODE" runat="server" Text='<%# Eval("CPOSITIONCODE") %>'
                                                    Style="width: 120px;"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang, WmsDBCommon_ASRS_Msg26 %>"><%--储位--%>

                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <%--  <asp:TemplateField HeaderText="栈板出入库" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="LinkSPACE_STATUS" runat="server" CausesValidation="false" CommandArgument='<%#Eval("IDS") %>'
                                                    CommandName="" OnClick="LinkSPACE_STATUS_Click" Text='<%# Eval("ASRS_STATUS")%>'>
                                                </asp:Button>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG24 %>" ShowHeader="False"><%--ASRS命令--%>
                                            <ItemTemplate>
                                                <asp:Button ID="LinkASRS_STATUS" runat="server" CausesValidation="false" CommandArgument='<%#Eval("IDS") %>'
                                                    CommandName="" OnClick="LinkASRS_STATUS_Click" Text='<%# Eval("ASRS_STATUS")%>'></asp:Button>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG25 %>" ShowHeader="false"><%--ASRS状态--%>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkStatus" runat="server" CausesValidation="false" CommandArgument='<%#Eval("CPOSITIONCODE") %>'
                                                    CommandName="" OnClick="LinkButton2_Click" Text='<%#Eval("ASRS_STATUS") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG13 %>" ShowHeader="False"><%--刷新--%>
                                            <ItemTemplate>
                                                <asp:Button ID="btnRefresh" runat="server" CausesValidation="false" CommandArgument='<%#Eval("IDS") %>'
                                                    CommandName="" OnClick="btnRefresh_Click" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG13 %>"></asp:Button><%--刷新--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" ForeColor="Blue" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG26 %>" ShowHeader="False" Visible="false"><%--栈板入库--%>
                                            <ItemTemplate>
                                                <asp:Button ID="LinkSPACE_STATUS_I" runat="server" CausesValidation="false" CommandArgument='<%#Eval("IDS") %>'
                                                    CommandName="" OnClick="LinkSPACE_STATUS_I_Click" Text='<%# Eval("pallet_code")%>'></asp:Button>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG27 %>" ShowHeader="False" Visible="false"><%--栈板出库--%>
                                            <ItemTemplate>
                                                <asp:Button ID="LinkSPACE_STATUS_O" runat="server" CausesValidation="false" CommandArgument='<%#Eval("IDS") %>'
                                                    CommandName="" OnClick="LinkSPACE_STATUS_O_Click" Text='<%# Eval("pallet_code")%>'></asp:Button>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CINPERSONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG28 %>"><%--入库人--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DINDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG29 %>"><%--入库时间--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IASNLINE" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG30 %>"
                                            Visible="False"><%--入库通知单项次--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_EditBtn %>" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandArgument='<%#Eval("IDS") %>'
                                                    CommandName="" OnClick="LinkButton1_Click" Text="<%$ Resources:Lang, Common_EditBtn %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <ul class="OneRowStyle">
                                    <li>
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
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server" DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td></td>
        </tr>
    </table>
</asp:Content>
