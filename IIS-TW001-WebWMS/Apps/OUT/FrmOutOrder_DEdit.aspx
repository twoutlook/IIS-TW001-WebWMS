<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Apps/DefaultMasterPage.master" Title="--出库管理" CodeFile="FrmOutOrder_DEdit.aspx.cs" Inherits="Apps_OUT_FrmOutOrder_DEdit" %>

<%@ Register Src="../BASE/ShowPARTDiv.ascx" TagName="ShowPARTDiv" TagPrefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
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
            position:absolute;
            right: 12px;
            top: 4px;
            left: auto;
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
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;

        }

        function Show(divID) {
            disponse_div(event, document.all(divID));
        }

    </script>
    <script type="text/javascript">
        $(function () {
            function logCINVNAME(lable, value) {
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
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
     <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt;  <%= Resources.Lang.FrmOutOrder_DEdit_Menu_PageName %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <uc1:ShowPARTDiv ID="ShowPARTDiv1" runat="server" />
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIDS" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ZiBiaoCode %>"></asp:Label>：<%--子表编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIDS" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ZhuBiaoCode %>"></asp:Label>：<%--主表编号--%>
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
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmOutOrder_DEdit_OrderLineId %>"></asp:Label>：<%--订单项次--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtOrderLine" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; position:relative;" >
                            <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                            <img alt="" onclick="disponse_div(event,document.all('<%= ShowPARTDiv1.GetDivName %>'));" src="../../Images/Search.gif" class="select" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVNAME" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCINVNAME" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label11" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblQTYTOTAL" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Quantity %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIQUANTITY" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="格式：位数最大为12位数"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label14" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Price %>"></asp:Label>：<%--单价(元)--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="格式：位数最大为12位数"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_TotalPrice %>"></asp:Label>：<%--总价(元)--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="格式：位数最大为12位数" Enabled="false"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_FinishQuantity %>"></asp:Label>：<%--完成数量--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtFinishQty" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ChuHuoRiQi %>"></asp:Label>：<%--出货日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSaleDate" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Lang, WMS_Common_GridView_Status %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtCSTATUS" runat="server" Width="95%" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang,WMS_Common_CreateUser %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCREATEOWNER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>                     
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang,WMS_Common_CreateTimeFrom %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_LastUpdator %>"></asp:Label>：<%--最后修改人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLASTUPDATEOWNER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_LastUpdateDate %>"></asp:Label><%--最后修改日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLASTUPDATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>
                        <td colspan="4"></td>
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
            <td style="text-align:center; padding:15px 0px;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, WMS_Common_Button_Save %>" />
                &nbsp;&nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="hiddOperation" runat="server" />
</asp:Content>
