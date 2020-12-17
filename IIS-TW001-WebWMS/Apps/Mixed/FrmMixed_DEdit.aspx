<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="FrmMixed_DEdit.aspx.cs" Inherits="Apps_Mixed_FrmMixed_DEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="ShowMixedCinvCode_Div.ascx" TagName="ShowMixedCinvCode_Div" TagPrefix="ucOA" %>
<%@ Register Assembly="DreamTek.ASRS.Business" Namespace="DreamTek.ASRS.Business" TagPrefix="ExcelBtn" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/Js/Help.js" type="text/javascript"></script>
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js" type="text/javascript">></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            font: 76%/1.5 Arial,sans-serif;
            background: #FFF;
            color: #333;
        }

        div#container
        {
            width: 500px;
            margin: 0 auto;
        }

        h1
        {
            color: #3CA3FF;
            margin: 1em 0 0;
            letter-spacing: -2px;
        }

        p
        {
            margin: 0 0 1.7em;
        }

        a
        {
            color: #F60;
            font-weight: bold;
        }

            a:hover
            {
                color: #F00;
            }
    </style>
    <style type="text/css">
        .ui-autocomplete-loading
        {
            background: white url('../../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }

        .select
        {
            cursor: hand;
            position: relative;
            left: -25px;
            top: 4px;
        }
    </style>
    <script type="text/javascript">

        function SetOutMixedValue(ControlName, Values, ControlName2, Values2,Values3/*, ControlName3, Values3, ControlName4, Values4*/) {
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2.replace(/##/g,"''");
            $("#ctl00_ContentPlaceHolderMain_hdnCinvName").val(Values2.replace(/##/g, "''"));

            $("#ctl00_ContentPlaceHolderMain_hdnOutBilldIDS").val(Values3);
            // document.all(ControlName3).value = Values3;
            //document.all(ControlName4).value = Values4;
            //GetOutBillData(Values2);
        }
        function GetOutBillData(outBill) {
            var outbillcode = outBill;
            var i = Math.random() * 10000 + 1;
            if (outbillcode != "") {
                $.get(
                "GetOutBillInfoById.aspx?OutBillId=" + outbillcode + "&i=" + i,
                "",
                function (data) {
                    var datas = data.split("|");

                    document.getElementById("ctl00_ContentPlaceHolderMain_txtERP_No").value = datas[0];
                    document.getElementById("ctl00_ContentPlaceHolderMain_txtPalledCode").value = datas[1];
                    document.getElementById("ctl00_ContentPlaceHolderMain_txtITYPE").value = datas[2];

                    $("#ctl00_ContentPlaceHolderMain_hdnCerpCode").val(datas[0]); //erpcode
                    $("#ctl00_ContentPlaceHolderMain_hdnPalletCode").val(datas[1]); //栈板/箱号
                    $("#ctl00_ContentPlaceHolderMain_hdnOtype").val(datas[2]); //出库类型

                    //$("#ctl00_ContentPlaceHolderMain_txtERP_No").text(datas[0]); //erpcode
                    //$("#ctl00_ContentPlaceHolderMain_txtPalledCode").text(datas[1]); //栈板/箱号

                },
                ""
            );
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmMixedList_Title02%>-&gt;<%= Resources.Lang.FrmMixed_D_Title01%>
  <%--配料单-&gt;配料单详情--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <ucOA:ShowMixedCinvCode_Div ID="ucShowMixedCinvCode_Div" runat="server" />
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="3">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
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

                                .style1
                                {
                                    height: 29px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label21"  runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label>： <%--料号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCinvCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                           
                            <asp:RequiredFieldValidator ID="rfvtxtCinvCode" runat="server" ControlToValidate="txtCinvCode"
                                ErrorMessage="<%$ Resources:Lang, FrmMixed_DEdit_SelectCinv%>" Display="None"> </asp:RequiredFieldValidator> <%--请选择料号!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6"  runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, Common_CinvName1%>"></asp:Label>： <%--品名--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCinvName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                           
                            <asp:RequiredFieldValidator ID="rfvtxtCinvName" runat="server" ControlToValidate="txtCinvName"
                                ErrorMessage="<%$ Resources:Lang, FrmMixed_DEdit_SelectCinvName%>" Display="None"> </asp:RequiredFieldValidator>  <%--请选择品名!--%>
                            <%--<asp:HiddenField ID="hdnCinvName" runat="server" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3"  runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, Common_IQUANTITY%>"></asp:Label>： <%--数量--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtQty" runat="server" CssClass="NormalInputText" Width="95%" ></asp:TextBox>
                           
                            <asp:RequiredFieldValidator ID="rfvtxtQty" runat="server" ControlToValidate="txtQty"
                                ErrorMessage="<%$ Resources:Lang, FrmMixed_DEdit_InputQty%>" Display="None"> </asp:RequiredFieldValidator>  <%--请填写数量!--%>
                            <asp:HiddenField ID="hdnCerpCode" runat="server" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7"  runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>： <%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%" Enabled="false">
                            </asp:DropDownList>
                           
                            <asp:RequiredFieldValidator ID="rfvddlCSTATUS" runat="server" ControlToValidate="ddlCSTATUS"
                                ErrorMessage="<%$ Resources:Lang, Common_SelectStatus%>" Display="None"> </asp:RequiredFieldValidator>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5"  runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmMixedList_Label4%>"></asp:Label>： <%--栈板/箱号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPalledCode" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false" onfocus="this.blur()"></asp:TextBox>
                           
                            <asp:RequiredFieldValidator ID="rfvtxtPalledCode" runat="server" ControlToValidate="txtPalledCode"
                                ErrorMessage="<%$ Resources:Lang, FrmMixed_D_Msg03%>" Display="None"> </asp:RequiredFieldValidator>  <%--请填写栈板/箱号!--%>
                            <asp:HiddenField ID="hdnPalletCode" runat="server" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, FrmMixedList_lblCCREATEOWNERCODE%>"></asp:Label>：  <%--配料人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCreaterUser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>


                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, FrmMixedList_lblDCREATETIMEFromFrom%>"></asp:Label>： <%--配料日期--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtCreaterTime" runat="server"
                                CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblRemark" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>： <%--备注--%>
                        </td>
                        <td style="width: 20%" colspan="5">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="NormalInputText" Width="98%" MaxLength="100" Height="33px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="4">
                            <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <asp:TextBox ID="txtIDS" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <asp:HiddenField ID="hdnOutBilldIDS" runat="server" />
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
        <tr valign="top">
            <td valign="top" align="left" class="style1">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                    Text="<%$ Resources:Lang, Common_btnSave%>" />  <%--保存--%>
                &nbsp;&nbsp;
               <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /> <%--返回--%>
            </td>
        </tr>
    </table>
    <div id="showResultDiv" onmouseout="closeDisplay()" style="background: #DFF8FF; position: absolute; z-index: 10; display: block;">
        <select id="showResultSelect" name="showResultSelect" size="15" style="display: none; background: #DFF8FF;"
            onchange="setTextValue(this.value)">
        </select>
    </div>
    <script type="text/javascript">
        function ChangeDivWidth() {
            document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
        }
        window.onresize = ChangeDivWidth;
        ChangeDivWidth();
    </script>
</asp:Content>
