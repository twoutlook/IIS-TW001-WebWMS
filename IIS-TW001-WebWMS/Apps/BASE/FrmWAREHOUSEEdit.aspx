<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmWAREHOUSEEdit.aspx.cs"
    Inherits="BASE_FrmWAREHOUSEEdit" Title="--<%$ Resources:Lang, FrmWAREHOUSEEdit_Title01%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Src="ShowVENDORDiv.ascx" TagName="ShowVENDORDiv" TagPrefix="ucVENDORDiv" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmWAREHOUSEEdit_Title01%>-&gt;<%= Resources.Lang.FrmWAREHOUSEEdit_Title01%><%--仓库详情-&gt;仓库详情--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <ucVENDORDiv:ShowVENDORDiv ID="ucShowVENDORDiv" runat="server" />
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
                                    left: -5px;
                                    top: 2px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCWAREID" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblCWAREID%>"></asp:Label>： <%--编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCWAREID" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30" Enabled="true"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%"> 
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCWARENAME" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblCCARGONAME%>"></asp:Label>： <%--名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCWARENAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="true"></asp:TextBox>
                           
                            <asp:RequiredFieldValidator ID="rfvtxtCWARENAME" runat="server" ControlToValidate="txtCWARENAME"
                                ErrorMessage="<%$ Resources:Lang, FrmWAREHOUSEEdit_rfvtxtCWARENAME%>" Display="None"> <%--请填写名称!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            &nbsp;
                            <span class="requiredSign">*</span>
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmWAREHOUSEList_Label2%>"></asp:Label>：<%--仓库类型--%>
                        </td>
                        <td style="width: 20%">
                            <%--<asp:TextBox ID="txtCDEFINE2" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlWareHouseType" runat="server" Width="95%">
                            </asp:DropDownList>
                            
                            <asp:RequiredFieldValidator ID="rfvddlWareHouseType" runat="server" ControlToValidate="ddlWareHouseType"
                                ErrorMessage="<%$ Resources:Lang, FrmWAREHOUSEEdit_rfvddlWareHouseType%>" Display="None"> <%--请选择仓库类型!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblLEADERCODE" runat="server" Text="<%$ Resources:Lang, FrmWAREHOUSEList_LEADERCODE%>"></asp:Label>： <%--供应商编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLEADERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblLEADER" runat="server" Text="<%$ Resources:Lang, FrmWAREHOUSEList_LEADER%>"></asp:Label>：<%--供应商名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLEADER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblLEADERPHONE" runat="server" Text="<%$ Resources:Lang, FrmWAREHOUSEList_lblLEADERPHONE%>"></asp:Label>：<%--电话--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLEADERPHONE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbISBONDED" runat="server" Text="<%$ Resources:Lang, FrmWAREHOUSEList_BONDEDNAME%>"></asp:Label>：<%--是否保税仓--%>
                        </td>
                        <td style="width: 20%">
                            <asp:CheckBox ID="cbISBONDED" runat="server" Enabled="True" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmWAREHOUSEList_CDEFINE1NAME%>"></asp:Label>：<%--是否良品仓--%>
                        </td>
                        <td style="width: 20%">
                            <asp:CheckBox ID="cbCDEFINE1" runat="server" /><%-- Enabled="False" />--%>
                        </td>
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"> <%--创建人--%>
                                
                            </asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtcreateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label>：<%--创建日期--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtcreatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"  Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label3%>"></asp:Label>： <%--修改人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtupdateuser" runat="server" CssClass="NormalInputText" Width="95%"  
                            Enabled="False"></asp:TextBox> 
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label4%>"></asp:Label>： <%--修改时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtupdatetime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False"></asp:TextBox> 
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>： <%--备注--%>
                        </td>
                        <td style="width: 20%" colspan="5">
                            <asp:TextBox ID="txtCMEMO" runat="server" TextMode="MultiLine" Width="99%" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Visible="false" Text="ID："></asp:Label>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtID" runat="server" Visible="false" CssClass="NormalInputText"
                                Width="95%" MaxLength="36"></asp:TextBox>
                        </td>
                        <td colspan="4">
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
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>
</asp:Content>
