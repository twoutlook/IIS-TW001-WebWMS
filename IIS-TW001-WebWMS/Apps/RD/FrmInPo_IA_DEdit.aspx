<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmInPo_IA_DEdit.aspx.cs"
    Inherits="RD_FrmInPo_IA_DEdit" Title="--<%$ Resources:Lang, Common_InbillMangement %>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>
<%--入库管理--%>
<%@ Register Src="ShowPO_Div.ascx" TagName="ShowPO_Div" TagPrefix="uc1" %>
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
        function SetControlPOValue(ControlName, Values, ControlName2, Values2, ControlName3, Values3, ControlName4, Values4, ControlName5, Values5) {
           
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
            document.all(ControlName3).value = Values3;
            document.all(ControlName4).value = Values4;
            document.all(ControlName5).value = Values5;
            //alert(Values5);
        }

        function Show(divID) {
            //alert(divID);
            disponse_div(event, document.all(divID));
        }

       
    </script>
    <script type="text/javascript">
        function SetDateCode() {
            var vdatecode = $("#<%=txtDateCode.ClientID %>").val();
           
            //var volddatecode = $("#<%=txtOldDateCode.ClientID %>").val();
            var odlcode = document.getElementById("<%=txtOldDateCode.ClientID %>");
            if (odlcode.value == "") {
                odlcode.value = vdatecode;
            }
        }    
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt; <%=Resources.Lang.FrmInPo_IA_DEdit_Content1%><%--预入库明细--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <uc1:ShowPO_Div ID="ShowPO_Div1" runat="server" />
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
                           <%-- <span class="requiredSign">*</span>
                            <asp:RequiredFieldValidator ID="rfvtxtID" runat="server" ControlToValidate="txtID"
                                ErrorMessage="请填写主表编号!" Display="None"> 
                            </asp:RequiredFieldValidator>--%>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label13" runat="server" Text="PO_IDS："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPO_IDS" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="PO："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPO" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                           
                        </td>
                         <td class="InputLabel" style="width: 13%">
                             <asp:Label ID="Label14" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, CommonB_IPOLINE %>"></asp:Label>：<%--PO项次：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPOLine" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300"></asp:TextBox>
                              <img alt="" onclick="disponse_div(event,document.all('<%= ShowPO_Div1.GetDivName %>'));"
                                src="../../Images/Search.gif" class="select" />
                        </td>
                         <td class="InputLabel" style="width: 13%">
                             <asp:Label ID="Label15" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblQTYTOTAL" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG21 %>"></asp:Label>：<%--总数量：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtQTYTOTAL" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, CommonB_QtyFormat1 %>"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                        </td>
                    </tr>
                
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                             <asp:Label ID="Label16" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVCODE" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>"></asp:Label>：:
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                          
                           
                        </td>
                         <td class="InputLabel" style="width: 13%">
                             <asp:Label ID="Label19" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVNAME" runat="server" Text="<%$ Resources:Lang, Common_CinvName %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCINVNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmINASSIT_DEdit_MSG5 %>"></asp:Label>：<%--供应商批次号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtVENDORLOTNO" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300"></asp:TextBox>
                        </td>
                        
                    </tr>
                        <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmInPo_IA_DEdit_MSG12 %>"></asp:Label>：<%--DateCode类型：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtDateCodeType" runat="server" Width="95%">
                               <%--  <asp:ListItem Value="YYWW" Selected="True">年周(YYWW)</asp:ListItem>
                                <asp:ListItem Value="YYMMDD" >年月日(YYMMDD)</asp:ListItem>
                                 <asp:ListItem Value="YYMM">年月(YYMM)</asp:ListItem>--%>
                                 
                            </asp:DropDownList>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                             <asp:Label ID="Label18" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label11" runat="server" Text="DateCode："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDateCode" runat="server" CssClass="NormalInputText" Width="95%"
                                OnBlur="SetDateCode()"></asp:TextBox> 
                        </td>
                         <td class="InputLabel" style="width: 13%">
                             <asp:Label ID="Label17" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmInPo_IA_DEdit_MSG13 %>"></asp:Label>：<%--原始DateCode：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtOldDateCode" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblQTYPASSED" runat="server" Text="<%$ Resources:Lang, FrmInPo_IAEdit_QTYPASSED %>"></asp:Label>：<%--通过数量--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtQTYPASSED" runat="server" CssClass="NormalInputText" Width="95%"
                               ToolTip="<%$ Resources:Lang, CommonB_QtyFormat1 %>" Enabled="false"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                           
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblQTYUNPASSED" runat="server" Text="<%$ Resources:Lang, CommonB_lineRejectNum %>"></asp:Label>：<%--判退数量：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtQTYUNPASSED" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False"
                                ToolTip="<%$ Resources:Lang, CommonB_QtyFormat1 %>"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmInPo_IAEdit_QTYPENDING %>"></asp:Label>：<%--待检数量：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtQTYPENDING" runat="server" CssClass="NormalInputText" Width="95%"
                                 ToolTip="<%$ Resources:Lang, CommonB_QtyFormat1 %>" Enabled="false"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                        </td>
                        
                       
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label20" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG23 %>"></asp:Label>：<%--ERP项次：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtErpCodeline" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle28 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcreateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td> 
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG25 %>"></asp:Label>：<%--创建时间：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcreatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>
                        
                        
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dpdstatus" runat="server" Width="95%" Enabled="False">
                            </asp:DropDownList>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmImportDateList_LAST_UPD_OWNER %>"></asp:Label>：<%--最后修改人：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtupdateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Lang, FrmImportDateList_LAST_UPD_TIME %>"></asp:Label>：<%--最后修改时间：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtupdatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label21" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Lang, FrmInPo_IA_DEdit_Label22 %>"></asp:Label>：<%--预入库通知单项次：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLine" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="300" Enabled="False"></asp:TextBox>
                        </td>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server"  Text="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"></asp:Label>：<%--备注：--%>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtCMEMO" runat="server" CssClass="NormalInputText" Width="98%"
                                MaxLength="20" Height="64px" TextMode="MultiLine"></asp:TextBox>
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
