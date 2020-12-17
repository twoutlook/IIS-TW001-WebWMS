<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmOUTASN_DEdit.aspx.cs" Inherits="OUT_FrmOUTASN_DEdit" Title="--出库通知单" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Src="../BASE/ShowPARTDiv.ascx" TagName="ShowPARTDiv" TagPrefix="uc1" %>
<%@ Register Src="../BASE/ShowPARTDivRTV.ascx" TagName="ShowPARTDivRTV" TagPrefix="ucRTV" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link type="text/css" rel="Stylesheet" href="../../Layout/CSS/LG/Page.css" id="cssUrl" runat="server" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/Calendar/calendar-blue.css" />
    <style type="text/css">
        .displaynone
        {
            display:none;
        }
        html {
            height: 100%;
            overflow-x: hidden;
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

        .ui-autocomplete-loading {
            background: white url('../../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }

        .select {
            cursor: pointer;
            position: relative;
            left: -25px;
            top: 4px;
        }

        input[type='submit'][disabled], input[type='button'][disabled] {
            opacity: 0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70); /*兼容ie8及以下*/
        }
    </style>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt;<%= Resources.Lang.FrmOUTASNList_Menu_PageName %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <uc1:ShowPARTDiv ID="ShowPARTDiv1" runat="server" />
    <ucRTV:ShowPARTDivRTV ID="ShowPARTDivRTV1" runat="server" />

    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    
                    <%-- Note by Qamar 2020-11-26 把部分元素class設displaynone並將要顯示的元素放到此處 --%>
                    <tr>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="99%" MaxLength="50"></asp:TextBox>
                            <asp:Literal ID="ltSearch" runat="server"></asp:Literal>
                        </td>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="Label7" runat="server" Text="批/序號(RANK)"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtRANK_FINAL" runat="server" CssClass="NormalInputText" Width="99%" MaxLength="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVNAME" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>"></asp:Label>：
                        </td>
                        <td style="width: 30%" colspan="1">
                            <asp:TextBox ID="txtCINVNAME" runat="server" CssClass="NormalInputText" Width="99%" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblIQUANTITY" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Quantity %>"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtIQUANTITY" runat="server" CssClass="NormalInputText" Width="95%" onFocus="GetQty()" ToolTip="格式：最多8位整数，最多2位小数"></asp:TextBox>
                        </td>
                    </tr>

                    
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_XiangCi %>"></asp:Label>：
                        </td>
                        <td style="width: 20%" colspan="3">
                            <asp:TextBox ID="txtLineId" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
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
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, WMS_Common_GridView_Status %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList runat="server" ID="DpdStatus" Width="95%" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblCINVBARCODE" runat="server" Text="<%$ Resources:Lang, FrmOUTASN_DEdit_TiaoMa %>"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtCINVBARCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="60"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblCSO" runat="server" Text="<%$ Resources:Lang, FrmOUTASN_DEdit_SOPOCode %>"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtCSO" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblISOLINE" runat="server" Text="<%$ Resources:Lang, FrmOUTASN_DEdit_SOPOXiangCi %>"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtISOLINE" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="格式：最多10位整数，最多4位小数"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblCERPCODELINE" runat="server" Text="<%$ Resources:Lang, FrmOUTASN_DEdit_ERPXiangCi %>"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtCERPCODELINE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblCBATCH" runat="server" Text="<%$ Resources:Lang, FrmOUTASN_DEdit_PiCiCode %>"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtCBATCH" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>                   
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 20%">
                            <%= Resources.Lang.WMS_Common_Element_Remark %>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtCMEMO" runat="server" Width="95%" MaxLength="40" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>

                    <tr style="display: none">
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ZhuBiaoCode %>"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 20%"></td>
                        <td style="width: 30%">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td colspan="4" style="text-align: center; padding: 15px 0px;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClientClick="clearCINVNAME()" OnClick="btnSave_Click" Text="<%$ Resources:Lang, WMS_Common_Button_Save %>" />
                &nbsp;&nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server" DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="txtIDS" runat="server" />
    <asp:HiddenField ID="hfInasn_id" runat="server" />
    <asp:HiddenField ID="hfOriginalQty" runat="server" Value="0" />
    <asp:HiddenField ID="hfGetQty" runat="server" Value="0" />
    <asp:HiddenField ID="hfWorkType" runat="server" Value="0" />
    <asp:HiddenField ID="hfAssitIds" runat="server" Value="0" />
    <asp:HiddenField ID="hfOutType" runat="server" />
    <asp:HiddenField ID="hfErpCode" runat="server" />

    <script type="text/javascript" src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
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
    <script type="text/javascript">
        function SetControlValue(ControlName, Values, ControlName2, Values2) {
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
        }
        function SetControlValue(ControlName, Values, ControlName2, Values2, ControlName3, Values3) {
            //alert(ControlName + ":" + ControlName2);
            //alert(ControlName2);
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
            document.all(ControlName3).value = Values3;
        }
		//還需傳RANKFINAL故四個Values 18-10-2020 by Qamar
		function SetControlValue(ControlName, Values, ControlName2, Values2, ControlName3, Values3, ControlName4, Values4) {
			document.all(ControlName).value = Values;
			document.all(ControlName2).value = Values2;
			document.all(ControlName3).value = Values3;
			document.all(ControlName4).value = Values4;
			//alert("SetControlValue(); " + ControlName + "=" + Values + ", " + ControlName2 + "=" + Values2 + ", " + ControlName3 + "=" + Values3 + ", " + ControlName4 + "=" + Values4);
		}
        function clearCINVNAME() {
            $("#<%=txtCINVNAME.ClientID %>").val("");
        }

        function Show(divID) {
            disponse_div(event, document.all(divID));
        }
        function GetQty() {
            var CINVCODE = $("#<%=txtCINVCODE.ClientID %>").val();
            var InAsn_Id = $("#<%=hfInasn_id.ClientID %>").val();
            var workType = $("#<%=hfWorkType.ClientID %>").val();
            var i = Math.random() * 10000 + 1;
            if (workType == "0") {
                $.get(
                      "GetInAsnQty.aspx?CINVCODE=" + CINVCODE + "&InAsn_Id=" + InAsn_Id + "&i=" + i,
                      "",
                      function (data) {
                          //alert(data);
                          if (data != 0) {
                              $("#<%=txtIQUANTITY.ClientID %>").val(data);
                          }
                          $("#<%=hfGetQty.ClientID %>").val(data);
                      },
                      ""
                    );
            }
        }
	</script>
</asp:Content>
