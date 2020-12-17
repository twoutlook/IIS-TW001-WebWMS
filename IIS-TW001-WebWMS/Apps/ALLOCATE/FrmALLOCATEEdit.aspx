<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmALLOCATEEdit.aspx.cs"
    Inherits="ALLOCATE_FrmALLOCATEEdit" Title="--1111" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmALLOCATEList_Title %>-&gt;<%= Resources.Lang.FrmALLOCATEList_Title1 %><%--库存管理-&gt;调拨单--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script type="text/javascript">

        function check() {

            var number = 0;
          //  var controls = document.getElementById("<%=grdALLOCATE_D.ClientID %>").getElementsByTagName("input");

            //for (var i = 0; i < controls.length; i++) {
            //    var e = controls[i];
            //    if (e.type != "CheckBox") {
            //        if (e.checked == true) {
            //            number = number + 1;
            //        }
            //    }
            //}
            $.each($("#<%=grdALLOCATE_D.ClientID%>").find("span input"), function (i, item) {
                if (item.checked == true) {
                    number = number + 1;
                }
            });
            if (number == 0) {
                alert("<%= Resources.Lang.FrmALLOCATEEdit_Msg01%>");//请至少选择一项！
                return false;
            }
            if (confirm("<%= Resources.Lang.FrmALLOCATEEdit_Msg02%>")) {//你确认取消吗？
                return true;
            }
            else {
                return false;
            }
        }

        function checkdata() {

            var number = 0;
            //var controls = document.getElementById("<%=grdALLOCATE_D.ClientID %>").getElementsByTagName("input");

            //for (var i = 0; i < controls.length; i++) {
            //    var e = controls[i];
            //    if (e.type != "CheckBox") {
            //        if (e.checked == true) {
            //            number = number + 1;
            //        }
            //    }
            //}
            $.each($("#<%=grdALLOCATE_D.ClientID%>").find("span input"), function (i, item) {
                if (item.checked == true) {
                    number = number + 1;
                }
            });
            if (number == 0) {
                alert("<%= Resources.Lang.FrmALLOCATEEdit_Msg01%>");//请至少选择一项！
                return false;
            }
            if (confirm("<%= Resources.Lang.FrmALLOCATEEdit_Msg03%>")) {//你确认调拨吗？
                return true;
            }
            else {
                return false;
            }


        }
        //调拨类型单选效果
        window.onload = function () {
            var cbl = document.getElementById('<%= ckbIstockType.ClientID%>')
            var inputs = cbl.getElementsByTagName("input");          
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    inputs[i].onclick = function () {
                        var cbs = inputs;
                        for (var i = 0; i < cbs.length; i++) {
                            if (cbs[i].type == "checkbox" && cbs[i] != this && this.checked) {
                                cbs[i].checked = false;
                            }
                        }
                    }
                }
            }
        }

    </script>
    <table id="Table3" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
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
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblCTICKETCODE %>"></asp:Label>：<%--单据号--%>
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
                                onKeyPress="event.returnValue=false" ToolTip="<%$ Resources:Lang, Common_Format%>" MaxLength="19"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                            <img border="0" id="imgDINDATE" runat="server" align="absmiddle" alt="" style="cursor: pointer;
                                position: relative; left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif"
                                onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATE','y-mm-dd',0);" />
                            <span class="requiredSign" style="left: -50px">*</span>
                            <asp:RequiredFieldValidator ID="rfvtxtDINDATE" runat="server" ControlToValidate="txtDINDATE"
                                ErrorMessage="<%$ Resources:Lang, FrmALLOCATEEdit_Msg05%>" Display="None"> </asp:RequiredFieldValidator><%--请填写调拨日期！--%>
                            <%--<asp:RegularExpressionValidator ID="revtxtDINDATE" runat="server" ValidationExpression="[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]"
                                ControlToValidate="txtDINDATE" ErrorMessage="请填写有效的调拨日期!正确的格式是：yyyy-MM-dd" Display="None"> </asp:RegularExpressionValidator>--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Label2 %>"></asp:Label>：<%--ERP单号--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_username%>"></asp:Label>：<%--制单人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_DCREATETIME%>"></asp:Label>：<%--制单日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%" Enabled="false">
                               <%-- <asp:ListItem Value="0">未处理</asp:ListItem>
                                <asp:ListItem Value="4">已確認</asp:ListItem>
                                <asp:ListItem Value="1">已审核</asp:ListItem>
                                <asp:ListItem Value="2">已完成</asp:ListItem>
                                <asp:ListItem Value="3">已抛砖</asp:ListItem>
                                <asp:ListItem Value="5">調撥中</asp:ListItem>
                                <asp:ListItem Value="6">調撥完成</asp:ListItem>--%>
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
                            <asp:Label ID="lblDAUDITTIME" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblDAUDITTIMEFromFrom%>"></asp:Label>：<%--审核日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDAUDITTIME" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false" ToolTip="<%$ Resources:Lang, Common_Format%>" MaxLength="19"></asp:TextBox> <%--格式：yyyy-MM-dd--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_Label2%>"></asp:Label>：<%--调拨限制--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlallow" runat="server" Width="95%" Enabled="false">
                               <%-- <asp:ListItem Value="0" Selected="True">是</asp:ListItem>
                                <asp:ListItem Value="1">否</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_Label5%>"></asp:Label>：<%--扣账人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtdebituser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_DEBITTIME%>"></asp:Label>： <%--扣账时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtdebittime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_DDEFINE3%>"></asp:Label>：<%--抛转时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtpaozhuantime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="ID：" Visible="false"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_ALLOTYPE%>"></asp:Label>：<%--业务类型--%>
                        </td>
                        <td style="width: 27%">
                            <asp:CheckBoxList runat="server" ID="ckbIstockType">
                                <%--<asp:ListItem Text="立库仓内" onclick="checkBoxList_Click(this)"  Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="平仓仓内" onclick="checkBoxList_Click(this)" Value="1"></asp:ListItem>
                                <asp:ListItem Text="立库=>平仓" onclick="checkBoxList_Click(this)" Value="2"></asp:ListItem>
                                <asp:ListItem Text="平仓=>立库" onclick="checkBoxList_Click(this)" Value="3"></asp:ListItem>--%>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_lblCMEMO%>"></asp:Label>：<%--备注--%>
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
                            <asp:TextBox ID="txtCDEFINE1" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCDEFINE2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_lblCDEFINE2%>"></asp:Label>：<%--自定义2--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCDEFINE2" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                        </td>
                        <td style="width: 20%">
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDDEFINE4" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_lblDDEFINE4%>"></asp:Label>：<%--自定义4--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDDEFINE4" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, Common_Format%>"></asp:TextBox> <%--格式：yyyy-MM-dd--%>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDDEFINE4','y-mm-dd',0);" />
                            <asp:RegularExpressionValidator ID="revtxtDDEFINE4" runat="server" ValidationExpression="[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]"
                                ControlToValidate="txtDDEFINE4" ErrorMessage="<%$ Resources:Lang, FrmALLOCATEEdit_revtxtDDEFINE4%>" Display="None"> </asp:RegularExpressionValidator> <%--请填写有效的自定义4!正确的格式是：yyyy-MM-dd  --%>      

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIDEFINE5" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_lblIDEFINE5%>"></asp:Label>：<%--自定义5--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIDEFINE5" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_Msg01%>"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                            <asp:RegularExpressionValidator ID="revtxtIDEFINE5" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,16}(\.[0-9]{1,2}){0,1}"
                                ControlToValidate="txtIDEFINE5" ErrorMessage="<%$ Resources:Lang, FrmALLOCATEEdit_revtxtIDEFINE5%>" Display="None"> </asp:RegularExpressionValidator><%--请填写有效的自定义5！正确的格式是：最多16位整数，最多2位小数--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            &nbsp;
                        </td>
                        <td style="width: 20%">
                            &nbsp;
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
            <td align="center" style="padding:15px 0px;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /> <%--返回--%>
                &nbsp;&nbsp;
                <asp:Button ID="btnPrint" runat="server" CssClass="ButtonPrint" OnClick="btnPrint_Click" Text="<%$ Resources:Lang, Common_btnPrint%>" /> <%--打印--%>
            </td>
        </tr>
    </table>
    <table id="Table1" style="height: 100%; width: 100%">
        <tr valign="top" style="display: none;">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.Common_ZheDie %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
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
                        <td colspan="1">
                            &nbsp;
                        </td>
                        <td colspan="1">
                            &nbsp;
                        </td>
                        <td colspan="1">
                            &nbsp;
                        </td>
                        <td colspan="1">
                            &nbsp;
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
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"> <%--查询--%>
                            </asp:Button>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left" style="padding-bottom:15px;" >
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>" OnClick="btnNew_Click"></asp:Button><%--新增--%>
                &nbsp;<asp:Button ID="btnImportExcel" runat="server" CssClass="ButtonExcel" Text="<%$ Resources:Lang, Common_Export%>" Enabled="False" Visible="false"></asp:Button><%--导入--%>
                &nbsp;<asp:Button ID="btnCreateInOutBill" runat="server" Visible="false" CssClass="ButtonAdd" Text="<%$ Resources:Lang, FrmALLOCATEEdit_btnCreateInOutBill%>" OnClick="btnCreateInOutBill_Click"></asp:Button><%--调拨--%>
                &nbsp;<asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_Delete%>" CssClass="ButtonDel" OnClientClick="return CheckDel()" /><%--删除--%>
                &nbsp;<asp:Button ID="btnOutput" runat="server" CssClass="ButtonReg4" Text="<%$ Resources:Lang, FrmALLOCATEEdit_btnOutput%>" Visible="false" OnClick="btnOutput_Click" OnClientClick="return checkdata()" ></asp:Button> <%--AS/RS调拨--%>
                &nbsp;<asp:Button ID="btnCancle" runat="server" CssClass="ButtonCancel" Text="<%$ Resources:Lang, Common_Cancle%>" OnClick="btnCancle_Click" OnClientClick="return check()"></asp:Button><%--取消--%>
            </td>
            <td align="right" style="width: 60%">
                <asp:Label ID="Label8" CssClass="InputLabel" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_SearchCinvcode %>"></asp:Label>：<%--查询料号--%>
                <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="20%" MaxLength="50"></asp:TextBox>
                <asp:Label ID="Label3" CssClass="InputLabel" runat="server" Text="RANK"></asp:Label>：<%--rank--%>
                <asp:TextBox ID="txtRANK_FINAL" runat="server" CssClass="NormalInputText" Width="40px" MaxLength="1"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
            </td>
        </tr>
        <tr id="trOutBill" runat="server">
            <td align="left" colspan="5">
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" colspan="4">            
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <%--<asp:Panel ID="Panel1" runat="server">--%>
                    <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                        <asp:GridView id="grdALLOCATE_D" runat="server" allowpaging="True" bordercolor="Teal"
                            borderstyle="Solid" borderwidth="1px" cellpadding="1" width="100%" autogeneratecolumns="False"
                            onrowdatabound="grdALLOCATE_D_RowDataBound" showheader="True" cssclass="Grid"
                            pagesize="15" >
                            <pagersettings visible="False" />
                            <alternatingrowstyle cssclass="AlternatingRowStyle" />
                            <rowstyle horizontalalign="Left" wrap="False" />
                            <selectedrowstyle backcolor="#738A9C" font-bold="True" forecolor="#F7F7F7" wrap="False" />
                            <pagerstyle horizontalalign="Right" />
                            <headerstyle font-bold="True" horizontalalign="Center" cssclass="" wrap="False" />
                            <columns>
                            <asp:TemplateField HeaderText="<input id='chkSelect' type='checkbox' onclick='SelectAll4Grid(this)' />">
                                <ControlStyle BorderWidth="0px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid"
                                        BorderWidth="0px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="IDS" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmALLOCATEEdit_IDS%>" Visible="false"><%--子表编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmALLOCATEEdit_ID%>" Visible="false"><%--主表编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LINEID" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_XiangCi%>"><%--项次--%>
                               <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CPOSITIONCODE%>"><%--原始储位--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CPOSITION%>"><%--原始储位名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="150px" />
                            </asp:BoundField>
                                <%-- 以下 28-10-2020 by Qamar --%>
                                
                            <%--<asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CINVCODE%>" >--%><%--物料编码--%>
                            <%--    <HeaderStyle HorizontalAlign="center" Wrap="False" />--%>
                            <%--    <ItemStyle HorizontalAlign="left" Wrap="False" />--%>
                            <%--</asp:BoundField>--%>
                                
                            <asp:BoundField DataField="PART" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>" ><%--物料编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="170px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="RANK_FINAL" DataFormatString="" HeaderText="批/序號(RANK)" ><%--rank--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="170px" />
                            </asp:BoundField>
                                <%-- 以上 28-10-2020 by Qamar --%>
                            <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, CommonB_CinvName%>" ><%--物料名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="170px" />
                            </asp:BoundField>
                                   <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>" ><%--规格--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IQUANTITY" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang, Common_IQUANTITY%>" ><%--数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CTOPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CTOPOSITIONCODE%>"><%--目的储位--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CTOPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CTOPOSITION%>"><%--目的储位名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="150px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_ASRS%>" ShowHeader="False" >
                                <ItemTemplate>
                                    <asp:Button ID="LinkASRS_STATUS" runat="server" CausesValidation="false" CommandArgument='<%# Eval("IDS") %>'
                                        CommandName="" OnClick="LinkASRS_STATUS_Click" Text='<%# Eval("ASRS_STATUS")%>'></asp:Button>
                                </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:TemplateField>
                                <%--<asp:BoundField DataField="ASRS_STATUS" DataFormatString="" HeaderText="ASRS状态">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" ForeColor="Blue"/>
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_ASRSStatus%>" ShowHeader="False"> <%--ASRS状态--%>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkStatus" runat="server" CausesValidation="false" CommandArgument='<%#Eval("CPOSITIONCODE") %>'
                                                    CommandName="" OnClick="LinkButton2_Click" Text='<%#Eval("ASRS_STATUS") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_Refresh%>" ShowHeader="False" > <%--刷新--%>
                                <ItemTemplate>
                                    <asp:Button ID="btnRefresh" runat="server" CausesValidation="false" CommandArgument='<%#Eval("IDS") %>'
                                        CommandName="" OnClick="btnRefresh_Click" Text="<%$ Resources:Lang, Common_Refresh%>" ></asp:Button> <%--刷新--%>
                                </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="center" Wrap="False" ForeColor="Blue"/>
                            </asp:TemplateField>
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
                            <asp:BoundField DataField="CMIDPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEEdit_CMIDPOSITIONCODE%>" Visible="false"> <%--原始目的储位--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CDEFINE1" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATEEdit_CDEFINE1%>" Visible="false"><%--机种--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CMEMO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO%>" Visible="false"><%--备注--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CSTATUS" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>" Visible="false"><%--状态--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                        </columns>
                        </asp:GridView>

                       <ul class="OneRowStyle">
	                    <li >
		                     <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
			                    FirstPageText="<%$ Resources:Lang, Base_FirstPage%>" LastPageText="<%$ Resources:Lang, Base_EndPage%>" NextPageText="<%$ Resources:Lang, Base_NextPage%>" PrevPageText="<%$ Resources:Lang, Base_LastPage%>" ShowPageIndexBox="Never"
			                    AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
		                    </webdiyer:AspNetPager>
	                    </li>
	                    <li>
		                    <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
	                    </li>
                    </ul>

                    </div>
                <%--</asp:Panel>--%>
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

                //for (var i = 0; i < controls.length; i++) {
                //    var e = controls[i];
                //    if (e.type != "CheckBox") {
                //        if (e.checked == true) {
                //            number = number + 1;
                //        }
                //    }
                //}
                $.each($("#<%=grdALLOCATE_D.ClientID%>").find("span input"), function (i, item) {
                    if (item.checked == true) {
                        number = number + 1;
                    }
                });
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmALLOCATEEdit_Msg06%>");//请选择需要删除的项！
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
    </table>
</asp:Content>