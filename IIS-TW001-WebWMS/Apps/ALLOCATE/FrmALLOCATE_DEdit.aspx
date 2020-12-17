<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmALLOCATE_DEdit.aspx.cs"
    Inherits="ALLOCATE_FrmALLOCATE_DEdit" Title="--11" MasterPageFile="~/Apps/DefaultMasterPage.master" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>


<%@ Register Src="../BASE/ShowPARTDiv.ascx" TagName="ShowPARTDiv" TagPrefix="ucPART" %>
<%@ Register Src="ShowPK_CARGOSPACEDiv.ascx" TagName="ShowPK_CARGOSPACEDiv"  TagPrefix="ucCARGOSPACEDiv"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/Js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="Link1" runat="server" />
    <style type="text/css">
        .ui-autocomplete-loading
        {
            background: white url('../../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }

        .select
        {
            cursor: pointer;
            position: relative;
            left: -25px;
            top: 4px;
        }
    </style>
    <script type="text/javascript">
        function Show1(divID) {
            document.getElementById("ctl00_ContentPlaceHolderMain_ucCARGOSPACEDiv1_btnSearch").click();
            //document.getElementById("ctl00_ContentPlaceHolderMain_ucCARGOSPACEDiv1_btnSearchAll").click();
            //document.getElementById("ctl00_ContentPlaceHolderMain_ucCARGOSPACEDiv2_btnSearch").click();
            disponse_div(event, document.all(divID));
        }
        function Show2(divID) {
            document.getElementById("ctl00_ContentPlaceHolderMain_ucCARGOSPACEDiv2_btnSearch").click();
            //document.getElementById("ctl00_ContentPlaceHolderMain_ucCARGOSPACEDiv2_btnSearchAll").click();
            disponse_div(event, document.all(divID));
        }
        $(function () {
            function logCTOPOSITIONCODE(lable, value) {
                $("#<%= txtCTOPOSITION.ClientID %>").val(lable);
            }
            $("#<%=  this.txtCTOPOSITIONCODE.ClientID %>").autocomplete({
                source: function (request, response) {
                    // alert(request.term);
                    $.ajax({
                        url: "../Server/Cargospan.ashx?PositionCode=" + request.term + "&CinvCode=" + $("#<%=txtCINVCODE.ClientID %>").val() + "&Type='In'",
                        dataType: "xml",
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            // alert(XMLHttpRequest.status);
                            // alert(XMLHttpRequest.readyState);
                            //alert(errorThrown);
                            // alert(textStatus);
                        },
                        success: function (data) {
                            response($("reuslt", data).map(function () {

                                return {
                                    value: $("CPOSITIONCODE", this).text(),
                                    label: $("CPOSITIONCODE", this).text(),
                                    id: $("CPOSITION", this).text()
                                }
                            }));
                            //alert(data.xml);

                        }
                    });
                },
                autoFocus: true,
                minLength: 1,
                select: function (event, ui) {
                    logCTOPOSITIONCODE(
                     ui.item.id, ui.item.value);
                },
                open: function () {
                    $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                },
                close: function () {
                    $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                }
            });

        });</script>
    <script type="text/javascript">
        $(function () {
            function logCINVNAME(lable, value) {
                $("#<%= txtCINVNAME.ClientID %>").val(lable);
            }

            function logCPOSITION(lable, value) {
                $("#<%= txtCPOSITION.ClientID %>").val(lable);

            }
            $("#<%=  this.txtCPOSITIONCODE.ClientID %>").autocomplete({
                source: function (request, response) {
                    //alert(request.term);
                    $.ajax({

                        url: "../Server/Cargospan.ashx?PositionCode=" + request.term + "&CinvCode=" + $("#<%=txtCINVCODE.ClientID %>").val() + "&Type='In'",
                        dataType: "xml",
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(XMLHttpRequest.status);
                            //alert(XMLHttpRequest.readyState);
                            //alert(errorThrown);
                            // alert(textStatus);
                        },
                        success: function (data) {
                            response($("reuslt", data).map(function () {

                                return {
                                    value: $("CPOSITIONCODE", this).text(),
                                    label: $("CPOSITIONCODE", this).text(),
                                    id: $("CPOSITION", this).text()
                                }
                            }));
                            //alert(data.xml);

                        }
                    });
                },
                autoFocus: true,
                minLength: 1,
                select: function (event, ui) {
                    logCPOSITION(
                     ui.item.id, ui.item.value);
                },
                open: function () {
                    $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                },
                close: function () {
                    $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                }
            });

            $("#<%=this.txtCINVCODE.ClientID %>").autocomplete({
                source: function (request, response) {
                    //alert(request.term);
                    $.ajax({
                        url: "../Server/Part.ashx?partNumber=" + request.term,
                        dataType: "xml",

                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            // alert(XMLHttpRequest.status);
                            //  alert(XMLHttpRequest.readyState);
                            // alert(textStatus);
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
    <script type="text/javascript">
        function CheckDel() {
            var number = 0;
            var controls = document.getElementById("").getElementsByTagName("input");

            for (var i = 0; i < controls.length; i++) {
                var e = controls[i];
                if (e.type != "CheckBox") {
                    if (e.checked == true) {
                        number = number + 1;
                    }
                }
            }
            if (number == 0) {
                alert("<%= Resources.Lang.FrmALLOCATEEdit_Msg06%>");//请选择需要删除的项！
                return false;
            }
            if (confirm("<%= Resources.Lang.FrmALLOCATEEdit_Msg07%>")) {//你确认删除吗？
                return true;
            }
            else {
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        //获取数量
        function getQty() {
            var ids = $("#<%=txtIDS.ClientID %>").val();
            if (ids.length == 0) {
                var CINVCODE = $("#<%=txtCINVCODE.ClientID %>").val();
                var CPOSITIONCODE = $("#<%=txtCPOSITIONCODE.ClientID %>").val();
                $.get(
                "GetSTOCK_CURRENT_Qty.aspx?CINVCODE=" + CINVCODE + "&CPOSITIONCODE=" + CPOSITIONCODE,
                "",
                function (date) {
                    if (date > 0) {
                        $("#<%=hfQty.ClientID %>").val(date);
                        //$("#<%=txtIQUANTITY.ClientID %>").val(date);
                    }
                },
                ""
                );
            }
        }
        function clearCINVNAME() {
            $("#<%=txtCINVNAME.ClientID %>").val("");
        }
        function gethfQty() {
            alert($("#<%=hfQty.ClientID %>").val());
        }
        //
         
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmALLOCATEList_Title%>-&gt;<%= Resources.Lang.FrmALLOCATE_DEdit_Title%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>   
    <ucPART:ShowPARTDiv ID="ucPART1" runat="server" />      
    <ucCARGOSPACEDiv:ShowPK_CARGOSPACEDiv ID="ucCARGOSPACEDiv1" runat="server" IsAllocate="true" />
    <ucCARGOSPACEDiv:ShowPK_CARGOSPACEDiv ID="ucCARGOSPACEDiv2" runat="server" IsAllocate="true" />    
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <ajaxToolkit:ModalPopupExtender runat="server" ID="msgBox" TargetControlID="hiddenTargetControlForModalPopup"
        PopupControlID="messageboxPanel" BackgroundCssClass="messagebox_parent" DropShadow="False"
        RepositionMode="RepositionOnWindowResizeAndScroll" />
    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none;" />
    <%--<asp:Panel runat="server" ID="messageboxPanel" Style="display: none; font-weight: bold; position: relative; left: -15px; top: 2px; width: 300px; height: 100px;">
        <div style="z-index: 1002; outline-style: none; outline-color: invert; outline-width: 0px; width: 300px; height: auto;"
            class="ui-dialog ui-widget ui-widget-content ui-corner-all  ui-dialog-buttons">
            <div class="ui-dialog-titlebar ui-corner-all ui-helper-clearfix" style="border: 1px solid #aaaaaa; background: #BACBE2 url(images/ui-bg_highlight-soft_75_cccccc_1x100.png) 50% 50% repeat-x; color: #222222; font-weight: bold;">
                <span id="ui-id-4" class="ui-dialog-title">提示信息</span><a class="ui-dialog-titlebar-close ui-corner-all"></a>
            </div>
            <div style="min-height: 0px; width: auto; height: 32px" id="DIV1" class="ui-dialog-content ui-widget-content">
                <p>
                    <span style="margin: 0px 7px 20px 0px; float: left" class="ui-icon ui-icon-alert"></span>該料号已有調撥單明細，是否添加？
                </p>
            </div>
            <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
                <div class="ui-dialog-buttonset">
                    <asp:Button ID="okbutton" CssClass="ButtonSave" Text="是" OnClick="okbutton_Click"
                        runat="server" />
                    <asp:Button ID="cancelbutton" CssClass="ButtonBack" Text="否" OnClick="cancelbutton_Click"
                        runat="server" />
                </div>
            </div>
        </div>
    </asp:Panel>--%>
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Block">
        <ContentTemplate>--%>
            <table id="Table3" style="width: 100%" cellspacing="1" cellpadding="1"
                border="0">
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
                            <tr id="cinvcode" runat="server">
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="lblCINVCODE" runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label>：<%--料号--%>
                                </td>
                                <td style="width: 20%; white-space: nowrap;">
                                    <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                        MaxLength="20"></asp:TextBox>
                                    <img alt="" onclick="disponse_div(event,document.all('<%= ucPART1.GetDivName %>'));"
                                        src="../../Images/Search.gif" class="select" />
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="lblCINVNAME" runat="server" Text="<%$ Resources:Lang, CommonB_CinvName%>"></asp:Label>：
                                </td>
                                <td style="width: 20%" colspan="3">
                                    <asp:TextBox ID="txtCINVNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                        Enabled="false" MaxLength="60"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="lblCPOSITIONCODE" runat="server" Text="<%$ Resources:Lang, Common_CPOSITIONCODE%>"></asp:Label>：<%--原始储位--%>
                                </td>
                                <td style="width: 20%; white-space: nowrap;">
                                    <asp:TextBox ID="txtCPOSITIONCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                        MaxLength="60"></asp:TextBox>
                                    <img alt="" onclick="Show1('<%= ucCARGOSPACEDiv1.GetDivName %>');" src="../../Images/Search.gif"
                                        class="select" />
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="lblCPOSITION" runat="server" Text="<%$ Resources:Lang, Common_CPOSITION%>"></asp:Label>：<%--原始储位名称--%>
                                </td>
                                <td style="width: 20%" colspan="3">
                                    <asp:TextBox ID="txtCPOSITION" runat="server" CssClass="NormalInputText" Width="95%"
                                        Enabled="false" MaxLength="60"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="lblCTOPOSITIONCODE" runat="server" Text="<%$ Resources:Lang, Common_CTOPOSITIONCODE%>"></asp:Label>：<%--目的储位--%>
                                </td>
                                <td style="width: 20%; white-space: nowrap;">
                                    <asp:TextBox ID="txtCTOPOSITIONCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                        MaxLength="60"></asp:TextBox>
                                    <img alt="" onclick="Show2('<%= ucCARGOSPACEDiv2.GetDivName %>');" src="../../Images/Search.gif"
                                        class="select" />
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label7" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="lblCTOPOSITION" runat="server" Text="<%$ Resources:Lang, Common_CTOPOSITION%>"></asp:Label>：<%--目的储位名称--%>
                                </td>
                                <td style="width: 20%" colspan="3">
                                    <asp:TextBox ID="txtCTOPOSITION" runat="server" CssClass="NormalInputText" Width="95%"
                                        Enabled="false" MaxLength="60"></asp:TextBox>


                                </td>
                            </tr>
                            <tr id="tr_qty" runat="server">
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="lblQTY" runat="server" Text="<%$ Resources:Lang, Common_IQUANTITY%>"></asp:Label>：<%--数量--%>
                                </td>
                                <td style="width: 20%" colspan="3">
                                    <asp:TextBox ID="txtIQUANTITY" runat="server" CssClass="NormalInputText" Width="95%"
                                        MaxLength="8"  ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_Msg01%>"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblDINDATE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblDINDATEFromFrom%>"></asp:Label>：<%--调拨日期--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtDINDATE" runat="server" CssClass="NormalInputText" Width="95%"
                                        Enabled="false" ToolTip="<%$ Resources:Lang, Common_Format%>"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                                    <img border="0" align="absmiddle" alt="" style="display: none; cursor: hand; position: relative; left: -30px; top: 0px"
                                        src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATE','y-mm-dd',0);" />
                                    <span style="display: none;" class="requiredSign" style="left: -50px">*</span>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblCINPERSONCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_CINPERSONCODE%>"></asp:Label>：<%--调拨人--%>
                                </td>
                                <td style="width: 20%" colspan="3">
                                    <asp:TextBox ID="txtCINPERSONCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                        Enabled="false" MaxLength="40"></asp:TextBox>

                                    <asp:Label ID="lblCDEFINE1" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_CDEFINE1%>" Style="display: none;"></asp:Label><%--机种--%>
                                    &nbsp;<asp:HiddenField ID="hfQty" runat="server" Value="0" />

                                    <asp:TextBox ID="txtModels" runat="server" CssClass="NormalInputText" Width="95%" Style="display: none;"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="InputLabel" style="width: 13%">                                   
                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_XiangCi%>"></asp:Label>：<%--项次--%>
                                </td>
                                 <td style="width: 20%">
                                    <asp:TextBox ID="txtLineID" runat="server" CssClass="NormalInputText" Width="95%"
                                        Enabled="false" MaxLength="60"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：<%--备注--%>
                                </td>
                                <td style="width: 20%" colspan="5">
                                    <asp:TextBox ID="txtCMEMO" runat="server" Width="95%" TextMode="MultiLine" MaxLength="40"></asp:TextBox>
                                </td>
                                 
                            </tr>
                            <tr style="display: none;">
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>" Visible="false"></asp:Label>：<%--状态--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%" Visible="false">
                                        <%--<asp:ListItem Value="0">未处理</asp:ListItem>
                                        <asp:ListItem Value="1">已完成</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblCDEFINE2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_lblCDEFINE2%>"></asp:Label>：<%--自定义2--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtCDEFINE2" runat="server" CssClass="NormalInputText" Width="95%"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblDDEFINE3" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_lblDDEFINE3%>"></asp:Label>：<%--自定义3--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtDDEFINE3" runat="server" CssClass="NormalInputText" Width="95%"
                                        ToolTip="<%$ Resources:Lang, Common_Format%>"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                                    <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                        src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDDEFINE3','y-mm-dd',0);" />
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblDDEFINE4" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_lblDDEFINE4%>"></asp:Label>：<%--自定义4--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtDDEFINE4" runat="server" CssClass="NormalInputText" Width="95%"
                                        ToolTip="<%$ Resources:Lang, Common_Format%>"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                                    <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                        src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDDEFINE4','y-mm-dd',0);" />
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblIDEFINE5" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_lblIDEFINE5%>"></asp:Label>：<%--自定义5--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtIDEFINE5" runat="server" CssClass="NormalInputText" Width="95%"
                                        ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_Msg01%>"></asp:TextBox>
                                </td>
                                <td class="InputLabel" style="width: 13%">&nbsp;
                                </td>
                                <td style="width: 20%">&nbsp;
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblIDS" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_IDS%>"></asp:Label>：<%--子表编号--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtIDS" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_txtIDS%>"></asp:TextBox> <%--格式：最多10位整数，最多4位小数--%>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_ID%>"></asp:Label>：
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_txtIDS%>"></asp:TextBox> <%--格式：最多10位整数，最多4位小数--%>
                                </td>
                                <td class="InputLabel" style="width: 13%;">
                                    <asp:Label ID="lblCINVBARCODE" runat="server" Text="<%$ Resources:Lang, Common_CINVBARCODE%>"></asp:Label>：<%--物料条码--%>
                                </td>
                                <td style="width: 21%;" colspan="3">
                                    <asp:TextBox ID="txtCINVBARCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                        MaxLength="60"></asp:TextBox>
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

                                        //function window.confirm(str) {
                                        //    execScript("n = (msgbox('" + str + "',vbYesNo, '提示')=vbYes)", "vbscript"); return (n);
                                        //} 
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
                        <asp:Button ID="btnNewDetital" runat="server" CssClass="ButtonAdd4"
                            Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_btnNewDetital%>" OnClick="btnNewDetital_Click" Visible="false" /><%--添加新明细--%>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                            Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
                    </td>
                   
                </tr>
                  <tr valign="top" id="tr_add" runat="server">
                    <td align="left" valign="top" style="padding-bottom:15px;"> 
                        <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd4" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_btnNew%>" ></asp:Button><%--SN编辑--%>
                        &nbsp;&nbsp;<asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_Delete%>" 
                            CssClass="ButtonDel" /> <%--删除--%>	
                    </td>
                    <td align="right" style="display: none">
                        <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" OnClick="btnSearch_Click"  
                            Text="<%$ Resources:Lang, Common_btnSearch%>" /> <%--查询--%>
                    </td>
                </tr>           
            </table>
            <table id="Table1" style="width: 100%">
               
                <tr valign="top">
                    <td valign="top" colspan="2"></td>
                </tr>
                <tr id="tr_lk" runat="server">
                    <td valign="top" colspan="2">
                        <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div2">
                            <asp:GridView ID="grdSNList" runat="server" AllowPaging="True" BorderColor="Teal"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"                             
                                OnRowDataBound="grdSNList_RowDataBound" CssClass="Grid" PageSize="15">
                                <PagerSettings Visible="False" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                <RowStyle HorizontalAlign="Left" Wrap="False" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                <PagerStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                <Columns>
                                    <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_Type%>"> <%--类型--%>
                                        <ItemTemplate>
                                            <asp:Label ID="labtype" runat="server" Text='<%# Eval("SNTYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SN_CODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_SN_CODE%>"> <%--SN/箱号/栈板--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                    </asp:BoundField>
                                     <%-----BUCKINGHA-838 条码管理修改CH--%>
                                      <asp:BoundField DataField="RULECODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_RULECODE%>"><%--规则--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                    </asp:BoundField>
                                      <%-----BUCKINGHA-838 条码管理修改CH--%>
                                    <asp:BoundField DataField="quantity" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang, Common_IQUANTITY%>"> <%--数量--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="palletcode" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Pallte%>">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DateCode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_DateCode%>">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="furnaceno" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_furnaceno%>">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="createowner" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"><%--创建人--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="createtime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"><%--创建时间--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                    </asp:BoundField>
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
                                    <div><%= Resources.Lang.Base_Gong%><%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                                </li>
                            </ul>


                        </div>
                    </td>
                </tr>
                <tr id="tr_pk" runat="server">
                    <td valign="top" colspan="2">
                        <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div3">
                            <asp:GridView ID="gridBN" runat="server" AllowPaging="True" BorderColor="Teal"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                OnRowDataBound="gridBN_RowDataBound" CssClass="Grid" PageSize="15">
                                <PagerSettings Visible="False" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                <RowStyle HorizontalAlign="Left" Wrap="False" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                <PagerStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                <Columns>                                  
                                    <asp:TemplateField HeaderText="SN" ItemStyle-Width="50%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSN" runat="server" Width="95%" Text='<%# Eval("SN_CODE") %>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-----BUCKINGHA-838 条码管理修改CH--%>
                                      <asp:BoundField DataField="RULECODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_RULECODE%>"><%--规则--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                    </asp:BoundField>
                                      <%-----BUCKINGHA-838 条码管理修改CH--%>
                                    <asp:BoundField DataField="DateCode" DataFormatString="" HeaderText="DateCode">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="QUANTITY" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang, Common_IQUANTITY%>"><%--数量--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="CREATEOWNER" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"><%--创建人--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"><%--创建时间--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>

                            <ul class="OneRowStyle">
                                <li>
                                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager2_PageChanged"
                                       FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                        AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                    </webdiyer:AspNetPager>
                                </li>
                                <li>
                                    <div><%= Resources.Lang.Base_Gong%><%=AspNetPager2.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                                </li>
                            </ul>


                        </div>
                    </td>
                </tr>
                <script type="text/javascript">
                    //function ChangeDivWidth() {
                    //    document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
                    //}
                    //window.onresize = ChangeDivWidth;
                    //ChangeDivWidth();
                </script>
            </table>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
