<%@ Page ValidateRequest="false" Language="C#" AutoEventWireup="true" CodeFile="FrmINASN_DEdit.aspx.cs"
    Inherits="RD_FrmINASN_DEdit" Title="--<%$ Resources:Lang, FrmINASN_DEdit_Content1%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Src="../BASE/ShowPARTDiv.ascx" TagName="ShowPARTDiv" TagPrefix="uc1" %>
<%@ Register Src="../BASE/ShowReTurnDiv.ascx" TagName="ShowReTurnDiv" TagPrefix="ucRTV" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        .displaynone
        {
            display:none;
        }
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
        function SetControlValue(ControlName, Values, ControlName2, Values2, ControlName3, Values3) {
            //alert(ControlName + ":" + ControlName2);
            //alert(ControlName2);
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
            document.all(ControlName3).value = Values3;
            //alert("SetControlValue(); " + ControlName + "=" + Values + ", " + ControlName2 + "=" + Values2 + ", " + ControlName3 + "=" + Values3);
        }

        //還需傳RANKFINAL故四個Values 18-10-2020 by Qamar
		function SetControlValue(ControlName, Values, ControlName2, Values2, ControlName3, Values3, ControlName4, Values4) {
			//alert(ControlName + ":" + ControlName2);
			//alert(ControlName2);
			document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
            document.all(ControlName3).value = Values3;
            document.all(ControlName4).value = Values4;
            //alert("SetControlValue(); " + ControlName + "=" + Values + ", " + ControlName2 + "=" + Values2 + ", " + ControlName3 + "=" + Values3 + ", " + ControlName4 + "=" + Values4);
		}

        function SelectPart(ControlName, Values, ControlName2, Values2) {
            //alert(ControlName + ":" + ControlName2);
            //alert(ControlName2);
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
            document.all('<%= btnLoadCount.ClientID %>').click();
        }
        
        function SetRMA_Value(ControlName, Values, ControlName2, Values2) {
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
//            document.all(ControlName3).value = Values3;
//            document.all("ctl00_ContentPlaceHolderMain_erpline_code").value = Values3;
//            document.all(ControlName3).disabled = "disabled";

        }
        
        function Show(divID) {
            //alert(divID);
            disponse_div(event, document.all(divID));
        }
        //校验是否可以特殊退料
        function checkspecialreturn() {
            var result = false;
            $.ajax({
                type: "Post",
                async: false, //已经是同步请求了
                cache: false,
                global: false,
                url: "FrmINASN_DEdit.aspx/CanSpecialReturn",
                data: "{'cinvcode':'" + document.all('<%=txtCINVCODE.ClientID %>').value + "','AsnId':'" + document.all('<%=txtID.ClientID %>').value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d) {
                        //"此料号可以整批退料，确认继续保存？")
                        if (confirm("<%= Resources.Lang.FrmINASN_DEdit_MSG1 %>")) {
                            result = true;
                        }
                        else {
                            result = false;
                        }
                    }
                    else {
                        result = true;
                    }
                },
                error: function (err) {
                    result = false;
                    alert(err);
                }
            });
            
            return result;
        };
	</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt;<%=Resources.Lang.FrmINASN_DEdit_Content1%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:ShowPARTDiv ID="ShowPARTDiv1" runat="server" />
     <ucRTV:ShowReTurnDiv ID="ShowReTurnDiv1" runat="server" />
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
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
                                .auto-style1 {
                                    width: 20%;
                                    height: 25px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIDS" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG18 %>"></asp:Label>：<%--子表编号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIDS" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="<%$ Resources:Lang, FrmINBILL_DEdit_MSG5 %>"></asp:TextBox><%--格式：最多10位整数，最多4位小数--%>
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG19 %>"></asp:Label>：<%--主表编号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="<%$ Resources:Lang, FrmINBILL_DEdit_MSG5 %>"></asp:TextBox><%--格式：最多10位整数，最多4位小数--%>
                                Enabled="False"></asp:TextBox>
                        </td>
                    </tr>

                    <%-- Note by Qamar 2020-11-26 把部分元素class設displaynone並將要顯示的元素放到此處 --%>
                    <tr> <%--料號 --%>
                        <td class="InputLabel" style="width: 13%; height: 25px;">
                            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVCODE" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>"></asp:Label>：
                        </td>
                        <td  style="width: 20%">
                            <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                           <%-- <img alt="" onclick="disponse_div(event,document.all('<%= ShowPARTDiv1.GetDivName %>'));"
                                src="../../Images/Search.gif" class="select" />--%>
                                <asp:Literal ID="ltSearch" runat="server"></asp:Literal>
                                <%-- <span class="requiredSign">*</span> --%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            &nbsp;
                            <asp:Label ID="Label9" runat="server" Text="批/序號(RANK)"></asp:Label>：<%--RANK--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtRANK_FINAL" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%"> <%--品名--%>
                            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVNAME1" runat="server" Text="<%$ Resources:Lang, Common_CinvName %>"></asp:Label>：
                        </td>
                        <td style="width: 20%" colspan="1">
                            <asp:Label ID="lblCINVNAME" runat="server" Text="<%$ Resources:Lang, FrmINASN_DEdit_lblCINVNAME %>" Style="display: none;"></asp:Label><%--请选择料号--%>
                            <asp:TextBox ID="txtCINVNAME" runat="server" CssClass="NormalInputText" Width="99%"
                                MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%"> <%--數量--%>
                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblIQUANTITY" runat="server" Text="<%$ Resources:Lang, Common_NUM %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIQUANTITY" runat="server" DataFormatString="{0:N2}" CssClass="NormalInputText" Width="95%" 
                                ToolTip="<%$ Resources:Lang, FrmINASN_DEdit_txtIQUANTITY %>"></asp:TextBox><%--格式：最多8位整数，最多2位小数--%>
                        </td>
                    </tr>


                    <tr class="displaynone"> <%--規格--%>
                        <td class="InputLabel" style="width: 13%">                           
                            <asp:Label ID="lblcespec" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcspecifications" runat="server" CssClass="NormalInputText" Width="95%"  Enabled="false"
                                ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone"> <%--物料條碼 批次號--%>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCINVBARCODE" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG22 %>"></asp:Label>：<%--物料条码：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCINVBARCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            &nbsp;
                            <asp:Label ID="lblCBATCH" runat="server" Text="<%$ Resources:Lang, FrmINASSIT_DEdit_MSG3 %>"></asp:Label>：<%--批次号：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCBATCH" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCPO" runat="server" Text="<%$ Resources:Lang, FrmINASN_DEdit_lblCPO %>"></asp:Label>：<%--PO/SO号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPO" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            &nbsp;
                            <asp:Label ID="Label4" runat="server" Text="DateCode："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDateCode" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIPOLINE" runat="server" Text="<%$ Resources:Lang, FrmINASN_DEdit_lblIPOLINE %>"></asp:Label>：<%--PO/SO项次：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIPOLINE" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmINBILL_DEdit_MSG5 %>"></asp:TextBox><%--格式：最多10位整数，最多4位小数--%>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                           <asp:DropDownList runat="server" ID="DpdStatus"  Width="95%" Enabled="False">  
                           </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            &nbsp;
                            <asp:Label ID="lblCERPCODELINE" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG23 %>"></asp:Label>：<%--ERP项次：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCERPCODELINE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                            <%-- <input  runat="server" type="hidden" name="erpline_code"  id="erpline_code"  value=""/>--%>
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
            <td>
                <asp:HiddenField ID="hfInType" runat="server" />
                <asp:HiddenField ID="hfErpCode" runat="server" />
                <asp:HiddenField ID="hfOriginalQty" runat="server" Value="0" />
                <!--修改前数量-->
                <asp:HiddenField ID="hfWorkType" runat="server" Value="0" />
                <!--工作状态 0：新增，1：修改-->
                <asp:HiddenField ID="hfIsAll" runat="server" Value="0" />
                <!--是否其他用料-->
                <%--<asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClientClick="return checkspecialreturn();"
                    OnClick="btnSave_Click" Text="<%$ Resources:Lang, Common_Save %>" />--%>
                   <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" 
                    OnClick="btnSave_Click" Text="<%$ Resources:Lang, Common_Save %>" />
                &nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="ButtonDel" Visible="false"
                    OnClick="btnDelete_Click" Text="<%$ Resources:Lang, Common_DelBtn %>" CausesValidation="false" />
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>" CausesValidation="false" />
            </td>
        </tr>
    </table>
    <div style="display: none">
        <asp:Button ID="btnLoadCount" runat="server" Text="Button" OnClick="btnLoadCount_Click" />
    </div>
</asp:Content>
