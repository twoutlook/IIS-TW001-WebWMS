<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmINBILL_DEdit.aspx.cs"
    Inherits="RD_FrmINBILL_DEdit" Title="<%$ Resources:Lang, FrmInbill_InbillCticketCode %>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../BASE/ShowBASE_CARGOSPACE_ByCinvcodeDiv.ascx" TagName="ShowBASE_CARGOSPACE_ByCinvcodeDiv" TagPrefix="uc2" %>
<%@ Register Src="../BASE/ShowBASE_CARGOSPACEDiv.ascx" TagName="ShowBASE_CARGOSPACEDiv" TagPrefix="uc3" %>
<%@ Register Src="../BASE/ShowPart_Asn_IDS_Div.ascx" TagName="ShowPart_Asn_IDS_Div" TagPrefix="uc6" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Apps/BAR/ShowCartonSN_Div.ascx" TagName="ShowCartonSN_Div" TagPrefix="uc4" %>
<%@ Register Src="~/Apps/BAR/ShowPllentCarton_Div.ascx" TagName="ShowPllentCarton_Div" TagPrefix="uc5" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
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
    </style>
    <script type="text/javascript">
        function SetAsnPartValue(ControlName, Values, ControlName2, Values2, ControlName3, Values3, ControlName4, Values4, ControlName5, Values5) {
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
            document.all(ControlName3).value = Values3;
            document.all(ControlName4).value = Values4;
            document.all(ControlName5).value = Values5;            
            var asnids = Values5;
            //alert(asnids);
            GetInfo(asnids, 'IN');

            var cinvname = document.getElementById("<%=txtCINVNAME.ClientID %>");
            var verpline = document.getElementById("<%=txtCERPCODELINE.ClientID %>");            
            cinvname.readOnly = true;
            verpline.readOnly = true;
           

            var hfget = document.getElementById("<%=hfGetQty.ClientID %>");
            hfget.value = Values4;
        }
        function GetInfo(Inasn_Ids, intype) {
            var i = Math.random() * 10000 + 1;
            if (Inasn_Ids != "") {
                $.get("GetAsnInfo_IDS_AjAx.aspx?asn_d_ids=" + Inasn_Ids + "&type=" + intype + "&i=" + i,
                    "",
                    function (data) {
                        var datas = data.split("|");

                        $("#<%=txtIASNLINE.ClientID %>").val(datas[0]); //PO项次
                        $("#<%=txtCINVBARCODE.ClientID %>").val(datas[1]); //物料条码
                        $("#<%=txtCMEMO.ClientID %>").val(datas[2]); //备注

                    },
                    ""
                );
            }
        }

        function SetControlValue(ControlName, Values, ControlName2, Values2) {
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
        }

        function Show(divID) {
            document.getElementById("ctl00_ContentPlaceHolderMain_ShowBASE_CARGOSPACEDiv1_btnSearch").click();
            disponse_div(event, document.all(divID));
        }


       

        function clearCINVNAME() {
            $("#<%=txtCINVNAME.ClientID %>").val("");
        }

        //打开选择储位的窗体
        function openFrmShowBASE_SELECTCARGOSPACEList() {
            __doPostBack('ctl00$ContentPlaceHolderMain$btnbtnOpenCARGOSPACEListHtml', '');
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
            function logCPOSITION(lable, value) {
                //$("<div>").text(message).prependTo("#log");
                //$("#log").scrollTop(0);
                //alert(lable);
                //alert(value);
                $("#<%= txtCPOSITION.ClientID %>").val(lable);
            }
            var InCINVCODE;
            $("#<%=this.txtCPOSITIONCODE.ClientID %>").autocomplete({
                source: function (request, response) {
                    //alert(request.term);
                    $.ajax({
                        url: "../Server/Cargospan.ashx?PositionCode=" + request.term + "&CinvCode=&Type=In",
                        dataType: "xml",
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(XMLHttpRequest.status);
                            //alert(XMLHttpRequest.readyState);
                            //alert(errorThrown);
                            //alert(textStatus);
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
                minLength: 0,
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
        });
    </script>

    <script type="text/javascript">
        function ShowSN(divID, typeid, snid) {
            var type = document.all(typeid).innerText;
            var sn = document.all(snid).value;
            document.all("ctl00_ContentPlaceHolderMain_ShowPllentCarton_txtPllent").value = "";
            document.all("ctl00_ContentPlaceHolderMain_ShowPllentCarton_txtCarton").value = "";
            if (type == "1" || type == "<%=Resources.Lang.FrmINBILL_DEdit_MSG3%>") {//栈板
                document.all("ctl00_ContentPlaceHolderMain_ShowPllentCarton_txtPllent").value = sn;
            }
                //箱
            else if (type == "2" || type == "<%=Resources.Lang.FrmINBILL_DEdit_MSG4%>") {
                document.all("ctl00_ContentPlaceHolderMain_ShowPllentCarton_txtCarton").value = sn;
            }
            disponse_div(event, document.all(divID));
            document.getElementById("ctl00_ContentPlaceHolderMain_ShowPllentCarton_btnSearch").click();
        }
        function SetSN(divID, typeid, snid, qtyid) {
            disponse_div(event, document.all(divID));
            document.getElementById("compareIframe").src = "../OUT/sessionset.aspx?TypeID=" + typeid + "&SN=" + snid + "&QTY=" + qtyid;
        }

        function CheckDel() {
            var number = 0;
            var controls = document.getElementById("<%=grdSNDetial.ClientID %>").getElementsByTagName("input");

                     for (var i = 0; i < controls.length; i++) {
                         var e = controls[i];
                         if (e.type != "CheckBox") {
                             if (e.checked == true) {
                                 number = number + 1;
                             }
                         }
                     }
                     if (number == 0) {
                         alert("<%=Resources.Lang.FrmDispatchUnitList_MsgTitle8 %>");
                    return false;
                }
                if (confirm("<%= Resources.Lang.FrmDispatchUnitList_MsgTitle9 %>")) {
                  return true;
              }
              else {
                  return false;
              }
          }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt;<%=Resources.Lang.FrmINBILL_DEdit_MSG1%><%--入库单明细--%> &nbsp;
    
    <asp:Label ID="lblErrorMsg" runat="server" Text="" Style="color: Red;"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div style="display: none;">
        <iframe id="compareIframe" src=""></iframe>
    </div>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <%--<%@ Register Src="../BASE/ShowPARTByInAsnIdDiv.ascx" TagName="ShowPARTByInAsnIdDiv"
    TagPrefix="uc1" %>--%>
    <uc6:ShowPart_Asn_IDS_Div ID="ShowPart_Asn_IDS_Div1" runat="server" />

    <uc3:ShowBASE_CARGOSPACEDiv ID="ShowBASE_CARGOSPACEDiv1" runat="server" />
    <uc2:ShowBASE_CARGOSPACE_ByCinvcodeDiv Id="ShowBASE_CARGOSPACE_ByCinvcodeDiv1" runat="server" />

    <uc4:ShowCartonSN_Div ID="showCartonSn" runat="server" />
    <uc5:ShowPllentCarton_Div ID="ShowPllentCarton" runat="server" />

    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <table id="Table3" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
                    <tr style="display: none">
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
                    <tr style="display:none">
                        <td>
                            <asp:TextBox runat="server" ID="hdnWorkType" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIDS" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG18 %>"></asp:Label>：<%--子表编号：--%>
                        </td>
                        <td style="width: 20%">

                            <asp:TextBox ID="txtIDS" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="<%$ Resources:Lang, FrmINBILL_DEdit_MSG5 %>"
                                Enabled="False"></asp:TextBox><%--格式：最多10位整数，最多4位小数--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG19 %>"></asp:Label>：<%--主表编号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="<%$ Resources:Lang, FrmINBILL_DEdit_MSG5 %>"
                                Enabled="False"></asp:TextBox><%--格式：最多10位整数，最多4位小数--%>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmINBILL_DEdit_MSG6 %>"></asp:Label>：<%--通知单明细IDS：--%>
                        </td>
                        <td style="width: 20%" colspan="3">
                            <asp:TextBox ID="txtAsn_D_IDS" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="<%$ Resources:Lang, FrmINBILL_DEdit_MSG5 %>"></asp:TextBox><%--格式：最多10位整数，最多4位小数--%>
                        </td>

                    </tr>

                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Lang, FrmINBILL_DEdit_MSG7 %>"></asp:Label>：<%--项次：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLineId" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Lang, FrmINBILL_DEdit_MSG8 %>"></asp:Label>：<%--入库通知单明细项次--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtAsndLineID" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVCODE" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                            <img alt="" onclick="disponse_div(event,document.all('<%= ShowPart_Asn_IDS_Div1.GetDivName %>'));"
                                src="../../Images/Search.gif" class="select" />
                            <asp:RequiredFieldValidator ID="rfvtxtCINVCODE" runat="server" ControlToValidate="txtCINVCODE"
                                ErrorMessage="<%$ Resources:Lang, FrmINBILL_DEdit_MSG9 %>" Display="None"> </asp:RequiredFieldValidator><%--请填写料号!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">                           
                            <asp:Label ID="lblRANK_FINAL" runat="server" Text="批/序號(RANK)"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtRANK_FINAL" runat="server" CssClass="NormalInputText" Width="95%"  MaxLength="1"
                                ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVNAME" runat="server" Text="<%$ Resources:Lang, Common_CinvName %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCINVNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblIQUANTITY" runat="server" Text="<%$ Resources:Lang, Common_NUM %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIQUANTITY" runat="server" CssClass="NormalInputText" Width="95%" 
                                ToolTip="<%$ Resources:Lang, FrmINBILL_DEdit_MSG10 %>"></asp:TextBox><%--格式：最多12位整数，最多2位小数--%>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">                           
                            <asp:Label ID="lblcespec" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcspecifications" runat="server" CssClass="NormalInputText" Width="95%"  Enabled="false"
                                ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIASNLINE" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG30 %>"></asp:Label>：<%--入库通知单项次：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIASNLINE" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmINBILL_DEdit_MSG11 %>"></asp:TextBox><%--格式：最多38位整数--%>
                        </td>
                          <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCINVBARCODE" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG22 %>"></asp:Label>：<%--物料条码：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCINVBARCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODELINE" runat="server" Text="<%$ Resources:Lang, FrmINBILL_DEdit_MSG12 %>"></asp:Label>：<%--子表ERP单号项次：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCERPCODELINE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                           <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label11" runat="server"  ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINPERSONCODE1" runat="server" Text="<%$ Resources:Lang, FrmINBILL_DEdit_MSG13 %>"></asp:Label>：<%--生产编码：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPartNo" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
               
                    <tr runat="server" id="tr1">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label15" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Lang, Common_LineID %>"></asp:Label>：<%--线别：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlWire" runat="server" Width="95%" AutoPostBack="true" OnSelectedIndexChanged="ddlWire_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label10" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINPERSONCODE0" runat="server" Text="<%$ Resources:Lang, Common_SiteID %>"></asp:Label>：<%--站点：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddl_Pallet_Code" runat="server" Width="95%" Enabled="false">
                            </asp:DropDownList>
                            <%--<asp:DropDownList ID="ddl_Pallet_Code" runat="server" Width="95%">
                                <asp:ListItem Value="0">请选择</asp:ListItem>
                                <asp:ListItem Value="1">站点1</asp:ListItem>
                                <asp:ListItem Value="2">站点2</asp:ListItem>
                            </asp:DropDownList>--%>
                        </td>
                    </tr>

                    <tr>
                        <td class="InputLabel" style="width: 13%; display:none">
                            <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCPOSITIONCODE" runat="server" Text="<%$ Resources:Lang, Common_PartnumNO %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; display:none">
                            <asp:TextBox ID="txtCPOSITIONCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                                                    </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCPOSITION" runat="server" Text="<%$Resources:Lang,Common_PartnumName%>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPOSITION" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                            <img alt="" onclick="Show('<%= ShowBASE_CARGOSPACEDiv1.GetDivName %>');" src="../../Images/Search.gif"
                                class="select" />
                            <input id="btnbtnOpenCARGOSPACEListHtml" runat="server" causesvalidation="False"
                                onserverclick="btnOpenCARGOSPACEList_Click" type="button" style="display: none;"
                                value="..." visible="True" />
                            <asp:Button ID="btnOpenCARGOSPACEList" runat="server" Text="..." Style="display: none;"
                                OnClick="btnOpenCARGOSPACEList_Click" CausesValidation="False" />

                        </td>
                    </tr>
                    <tr>
                     
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label12" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINPERSONCODE2" runat="server" Text="<%$ Resources:Lang, FrmINBILL_DEdit_MSG14 %>"></asp:Label>：<%--物料类型：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddl_InBillType" runat="server" Width="95%">
                               <%-- <asp:ListItem Value="0">请选择</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">良品</asp:ListItem>
                                <asp:ListItem Value="2">不良品</asp:ListItem>
                                <asp:ListItem Value="3">其它用处</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                         <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINPERSONCODE" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG28 %>"></asp:Label>：<%--入库人：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCINPERSONCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblDINDATE" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG29 %>"></asp:Label>：<%--入库时间：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDINDATE" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmINBILLEdit_MSG4 %>"  ></asp:TextBox><%--格式：yyyy-MM-dd--%>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATE','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, FrmINBILL_DEdit_MSG19 %>"></asp:Label>：<%--入库单单号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtInbillCode" runat="server" Enabled="false" CssClass="NormalInputText"
                                Width="95%" ToolTip="<%$ Resources:Lang, FrmINBILL_DEdit_MSG11 %>"></asp:TextBox><%--格式：最多38位整数--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCSTATUS" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server"  Text="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"></asp:Label>：<%--备注：--%>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtCMEMO" runat="server" Width="99%" MaxLength="20" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
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
                            <asp:HiddenField ID="HidField_DateCode" runat="server" />
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
            <td align="center">
                <asp:HiddenField ID="hfInasn_id" runat="server" />
                <asp:HiddenField ID="hfOriginalQty" runat="server" Value="0" />
                <asp:HiddenField ID="hfGetQty" runat="server" Value="0" />
                <asp:HiddenField ID="hfWorkType" runat="server" Value="0" />
                <asp:HiddenField ID="hfAsnIds" runat="server" />
                <!--通知单明细ID-->
                <asp:HiddenField ID="hfTradeCode" runat="server" />
                <asp:HiddenField ID="hfCurrency" runat="server" />
                <asp:HiddenField ID="hfInType" runat="server" />
                <asp:HiddenField ID="hfCERPCODE" runat="server" />
                <!--入库单创建类型 1：pda掃描入库  2：通知单补单 -->
                <asp:HiddenField ID="hdnCreateType" runat="server" />
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClientClick="clearCINVNAME()"
                    OnClick="btnSave_Click" Text="<%$ Resources:Lang, Common_Save %>" />
                &nbsp;&nbsp; &nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="ButtonDel"
                    OnClick="btnDelete_Click" Text="<%$ Resources:Lang, Common_DelBtn %>" CausesValidation="false" Visible="false" />
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>" CausesValidation="false" />
            </td>
        </tr>
    </table>
    <div style="overflow-x: scroll;" >
        <table id="TabMain0" style="width: 100%" runat="server">
            <tr valign="top">
                <td valign="top" align="left">
                    <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="SN" OnClick="btnNew_Click"></asp:Button>&nbsp;&nbsp;
                    <%--<asp:Button ID="btnNewBN" runat="server" CssClass="ButtonAdd" Text="BN" OnClick="btnNewBN_Click" Visible="false"></asp:Button>&nbsp;&nbsp;--%>
                    <%--栈板/箱--%>
                   <asp:Button ID="btnCarton" runat="server" CssClass="ButtonAdd4"  Text="<%$ Resources:Lang, FrmINBILL_DEdit_MSG21 %>" OnClick="btnCarton_Click" Visible="False"></asp:Button>&nbsp;&nbsp;
                              <asp:Button ID="btnDeleteSn" runat="server" CssClass="ButtonDel" Text="<%$ Resources:Lang, Common_DelBtn %>" OnClick="btnDeleteSn_Click"  OnClientClick="return CheckDel()" />
                    &nbsp;&nbsp;
                    <%--保存SN--%>
                             <asp:Button ID="btnSavesn" runat="server" CssClass="ButtonSave4" OnClick="btnSaveSN_Click"
                                 Text="<%$ Resources:Lang, FrmINBILL_DEdit_MSG22 %>" />
                </td>
                <%--<uc1:showpartbyinasniddiv id="ShowPARTDiv1" runat="server" />--%>
                <td align="left" style="width: 5%">
                    <asp:Button ID="btnSerch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click"></asp:Button>
                </td>
            </tr>
       
            <tr>
                <td colspan="5">
                    <div style="height:auto; width: 100%" id="DivScroll">
                        <asp:GridView ID="grdSNDetial" runat="server" AllowPaging="True" BorderColor="Teal"
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                            CssClass="Grid" PageSize="15" DataKeyNames="ID" OnRowDataBound="grdSNDetial_RowDataBound">
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
                                    <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID" DataFormatString="" HeaderText="" Visible="false">
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                </asp:BoundField>
                                  <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_Type %>"> <%--类型--%>
                                    <ItemTemplate>
                                        <asp:Label ID="labtype" runat="server" Text='<%# Eval("DisplayTYPE") %>'></asp:Label>
                                         <%--<asp:TextBox ID="txtType" runat="server" Width="95%" Text='<%# Eval("DisplayTYPE") %>'></asp:TextBox>--%>
                                       <%-- <asp:Image alt="" src="../../Images/Search.gif" class="select" runat="server" ID="imgSearch" />--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"  Width="10%"/>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG4 %>"><%--SN/箱号/栈板号--%>
                                    <ItemTemplate>
                                        <%--<asp:Label ID="labtype" runat="server" Text='<%# Eval("SN_CODE") %>'></asp:Label>--%>
                                         <asp:TextBox ID="txtSN" runat="server" Width="95%" Text='<%# Eval("SN_CODE") %>'></asp:TextBox>
                                       <%-- <asp:Image alt="" src="../../Images/Search.gif" class="select" runat="server" ID="imgSearch" />--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="20%" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False"  Width="20%"/>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILL_DEdit_MSG23%>"><%--条码规则--%>
                                    <ItemTemplate>
                                        <%--<asp:Label ID="labtype" runat="server" Text='<%# Eval("SN_CODE") %>'></asp:Label>--%>
                                         <asp:TextBox ID="txtRuleCode" runat="server" Width="95%" Text='<%# Eval("RULECODE") %>'></asp:TextBox>
                                       <%-- <asp:Image alt="" src="../../Images/Search.gif" class="select" runat="server" ID="imgSearch" />--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="20%" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False"  Width="20%"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILL_DEdit_MSG24 %>" Visible="false"><%--生产批号（制令）--%>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSN1" runat="server" Width="95%" Text='<%# Eval("SN_CODE") %>'></asp:TextBox>
                                        <%--<asp:Image alt="" src="../../Images/Search.gif" class="select" runat="server" ID="imgSearch" />--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="30%" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_NUM %>">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" Width="95%" Text='<%# Eval("QUANTITY")!=System.DBNull.Value && !string.IsNullOrEmpty(Eval("QUANTITY").ToString()) ?Convert.ToDecimal(Eval("QUANTITY")).ToString("f2"):Eval("QUANTITY") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="20%" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmInbill_PalletCode %>">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPalletCode" runat="server" Width="95%" Text='<%# Eval("PalletCode") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="20%" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILL_DEdit_MSG25 %>"><%--材料炉号--%>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFurnaceNo" runat="server" Width="95%" Text='<%# Eval("furnaceno") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="20%" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILL_DEdit_MSG26 %>"><%--生产日期--%>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDateCode" runat="server" Width="95%" Text='<%# Eval("DateCode") %>'></asp:TextBox>
                                        <%-- <img border="0" align="absmiddle" id="imgDate" runat="server" alt="" 
                                                style="cursor: hand; position: relative;left: -30px; top: 0px" 
                                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar(&amp;#39txt','yyyyMMdd',0);"  />--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="30%" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" Width="30%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CREATEOWNER" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle28 %>">
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle18 %>">
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="left" Wrap="False" Width="80px" />
                                </asp:BoundField>
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
    </div>
    <input type="hidden" id="hiddIsTemporary" runat="server" />
</asp:Content>
