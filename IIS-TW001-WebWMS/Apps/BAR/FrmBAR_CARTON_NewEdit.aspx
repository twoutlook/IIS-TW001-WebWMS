<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBAR_CARTON_NewEdit.aspx.cs"
    Inherits="FrmBAR_CARTON_NewEdit" Title="BAR_CARTON" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Src="ShowBarTypeDiv.ascx" TagName="ShowBarTypeDiv" TagPrefix="ucType" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    条码管理-&gt;批量-箱 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <ucType:ShowBarTypeDiv ID="ucShowBarType" runat="server" />
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
                       
                        <td class="InputLabel" style="width: 15%">
                            <asp:Label ID="lblCNAME" runat="server" Text="箱數量："></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtNumber" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="100"></asp:TextBox>
                            <span class="requiredSign">*</span>
                            <asp:RequiredFieldValidator ID="rfvtxtNumber" runat="server" ControlToValidate="txtNumber"
                                ErrorMessage="请填写批量生成箱的數量!" Display="None"> 
                            </asp:RequiredFieldValidator>
                        </td>

                         <td class="InputLabel" style="width: 15%">
                            <asp:Label ID="lblICAPACITY" runat="server" Text="箱类型："></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:DropDownList ID="ddlTYPE_ID" runat="server" Width="95%" ></asp:DropDownList>
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
                            <asp:HiddenField id="hf_Id" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                                Text="保存" />
                            &nbsp;&nbsp; &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="返回" CausesValidation="false" />                       
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server"
                    DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
    </table>
</asp:Content>
