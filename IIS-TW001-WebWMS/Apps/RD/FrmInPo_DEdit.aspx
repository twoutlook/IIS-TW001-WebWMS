<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmInPo_DEdit.aspx.cs"
    Inherits="RD_FrmInPo_DEdit" Title="--<%$ Resources:Lang, Common_InbillMangement %>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>
<%--入库管理--%>
<%@ Register Src="../BASE/ShowPARTDiv.ascx" TagName="ShowPARTDiv" TagPrefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="Link1"
        runat="server" />
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css"
        rel="stylesheet" type="text/css" />
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

        span.requiredSign {
            color: #FF0000;
            font-weight: bold;
            position: relative;
            left: -15px;
            top: 2px;
        }
    </style>
    <script type="text/javascript">
        function SetControlValue(ControlName, Values, ControlName2, Values2) {
            //alert(ControlName + ":" + ControlName2);
            //alert(ControlName2);
            //alert(Values + ":" + Values2);
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;

        }

        function Show(divID) {
            //alert(divID);
            disponse_div(event, document.all(divID));
        }

    </script>
    <script type="text/javascript">
        $(function () {
            function logCINVNAME(lable, value) {
                //$("<div>").text(message).prependTo("#log");
                //$("#log").scrollTop(0);
                //alert(lable);
                //alert(value);

                $("#<%= txtCINVNAME.ClientID %>").val(lable);


            }

            $("#<%=  this.txtCINVCODE.ClientID %>").autocomplete({
                source: function (request, response) {
                    //alert(request.term);
                    $.ajax({
                        url: "../Server/Part.ashx?partNumber=" + request.term + "&Asn_type=INASN_D",
                        dataType: "xml",

                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(XMLHttpRequest.status);
                            alert(XMLHttpRequest.readyState);
                            alert(textStatus);
                        },
                        success: function (data) {


                            response($("reuslt", data).map(function () {

                                return {
                                    value: $("CPARTNUMBER", this).text(),
                                    label: $("CPARTNUMBER", this).text(),
                                    id: $("CPARTNAME", this).text()
                                }
                            }));

                        }
                    });
                },
                autoFocus: true,
                minLength: 1,
                select: function (event, ui) {
                    logCINVNAME(
                     ui.item.id, ui.item.value);
                },
                open: function () {
                    $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                },
                close: function () {
                    $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt; <%=Resources.Lang.FrmInPo_DEdit_Content1%><%--采购单明细 --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <uc1:ShowPARTDiv ID="ShowPARTDiv1" runat="server" />

    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />

    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIDS" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG18 %>"></asp:Label>：<%--子表编号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIDS" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG19 %>"></asp:Label>：<%--主表编号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">&nbsp;
                        </td>
                        <td style="width: 20%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, CommonB_IPOLINE %>"></asp:Label>：<%--PO项次--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPOLine" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVCODE" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>"></asp:Label>：:
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                            <img alt="" onclick="disponse_div(event,document.all('<%= ShowPARTDiv1.GetDivName %>'));"
                                src="../../Images/Search.gif" class="select" />

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVNAME" runat="server" Text="<%$ Resources:Lang, Common_CinvName %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCINVNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label11" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblQTYTOTAL" runat="server" Text="<%$ Resources:Lang, Common_NUM %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIQUANTITY" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmInPo_DEdit_txtIQUANTITY %>"></asp:TextBox><%--格式：位数最大为12位数--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblQTYPASSED" runat="server" Text="<%$ Resources:Lang, FrmInPoEdit_UNIT %>"></asp:Label>：<%--单位：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtUNIT" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip=""></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblQTYUNPASSED" runat="server" Text="<%$ Resources:Lang, FrmInPoEdit_PRICE %>"></asp:Label>：<%--单价：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPRICE" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmInPo_DEdit_txtPRICE %>"></asp:TextBox><%--格式：位数最大为8位数--%>
                        </td>

                    </tr>
                    <tr>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmInPoEdit_TOTAL %>"></asp:Label>：<%--小计：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtCSTATUS" runat="server" Width="95%" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmInPo_DEdit_Label2 %>"></asp:Label>：<%--质检：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dpdQC" runat="server" Width="95%">
                               <%-- <asp:ListItem Value="0">需质检</asp:ListItem>
                                <asp:ListItem Value="1">无需质检</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle28 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCREATEOWNER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG25 %>"></asp:Label>：<%--创建时间：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmInPo_DEdit_Label9 %>"></asp:Label>：<%--最后修改人：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLASTUPDATEOWNER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Lang, FrmInPo_DEdit_Label10 %>"></asp:Label>：<%--最后修改时间：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLASTUPDATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
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
            <td style="text-align:center;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                    Text="<%$ Resources:Lang, Common_Save %>" />
                &nbsp;&nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>" CausesValidation="false" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="hiddOperation" runat="server" />
</asp:Content>
