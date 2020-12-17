<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBase_CraneConfigEdit_D_C.aspx.cs"
    Inherits="BASE_FrmBase_CraneConfigEdit_D_C" Title="<%$ Resources:Lang, BASE_FrmBase_CraneConfigEdit_D_C_Title01%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%--站点扫描器详情--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBase_AGV_D_Title01%>-&gt;<%= Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_Title02%><%--站点管理-&gt;扫描器详情--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
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
                    <tr>

                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblScanID" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblScanId%>"></asp:Label>：<%--扫描器编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtScanID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtScanID" runat="server" ControlToValidate="txtScanID"
                                ErrorMessage="<%$ Resources:Lang, BASE_FrmBase_CraneConfigEdit_D_C_rfvtxtScanID%>" Display="None"> <%--请填写扫描器编号!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblScanName" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblCCLIENTNAME%>"></asp:Label>：<%--扫描器名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtScanName" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtScanName" runat="server" ControlToValidate="txtScanName"
                                ErrorMessage="<%$ Resources:Lang, BASE_FrmBase_CraneConfigEdit_D_C_rfvtxtScanName%>" Display="None"> <%--请填写扫描器名称!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblSiteID" runat="server" Text="<%$ Resources:Lang, BASE_FrmBase_CraneConfigEdit_D_C_lblSiteID%>"></asp:Label>：<%--站点--%>
                        </td>
                        <td style="width: 20%">
                            <%= Resources.Lang.Common_Type%>：<%--类型--%>
                            <asp:DropDownList runat="server" ID="ddlScanType" OnSelectedIndexChanged="ddlScanType_SelectedIndexChanged" AutoPostBack="true">
                                <%--<asp:ListItem Text="立库" Value="LK"></asp:ListItem>
                                <asp:ListItem Text="RGV" Value="RGV"></asp:ListItem>
                                <asp:ListItem Text="提升机" Value="SJJ"></asp:ListItem>--%>
                            </asp:DropDownList>
                            <%= Resources.Lang.FrmBASE_CARGOSPACEList_lineid%>：<%--线别--%>
                            <asp:DropDownList runat="server" ID="ddlLineID" OnSelectedIndexChanged="ddlLineID_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>

                            <%= Resources.Lang.BASE_FrmBase_CraneConfigEdit_D_C_lblSiteID%>：<%--站点--%>
                            <asp:DropDownList runat="server" ID="ddlSite">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span style="position: relative; left: -20px; top: 2px; color: #FF0000; font-weight: bold;">*</span>
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, BASE_FrmBase_CraneConfigEdit_D_C_Label5%>"></asp:Label>：<%--服务器端IP--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtServerScanIP" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtServerScanIP"
                                ErrorMessage="<%$ Resources:Lang, BASE_FrmBase_CraneConfigEdit_D_C_RequiredFieldValidator1%>" Display="None"> <%--请填写服务器端IP!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span style="position: relative; left: -20px; top: 2px; color: #FF0000; font-weight: bold;">*</span>
                            <asp:Label ID="lblScanIP" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_SCANIP%>"></asp:Label>：<%--扫描器IP--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtScanIP" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtScanIP" runat="server" ControlToValidate="txtScanIP"
                                ErrorMessage="<%$ Resources:Lang, BASE_FrmBase_CraneConfigEdit_D_C_rfvtxtScanIP%>" Display="None"> <%--请填写扫描器IP!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span style="position: relative; left: -30px; top: 2px; color: #FF0000; font-weight: bold;">*</span>
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">

                            <asp:DropDownList ID="dplCSTATUS" runat="server" Width="95%">
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="rfvtxtCSTATUS" runat="server" ControlToValidate="dplCSTATUS"
                                ErrorMessage="<%$ Resources:Lang, Common_SelectStatus%>" Display="None"> 
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span style="position: relative; left: -20px; top: 2px; color: #FF0000; font-weight: bold;">*</span>
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, BASE_FrmBase_CraneConfigEdit_D_C_Label8%>"></asp:Label>：<%--端口号--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtPortNO" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPortNO"
                                ErrorMessage="<%$ Resources:Lang, BASE_FrmBase_CraneConfigEdit_D_C_RequiredFieldValidator2%>" Display="None"> <%--请填写端口号!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：
                            <%--创建人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtcreateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label>：<%--创建时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtcreatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label3%>"></asp:Label>：
                            <%--修改人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtupdateuser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False"></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label4%>"></asp:Label>：
                            <%--修改时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtupdatetime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False"></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmBase_AGV_Label1%>"></asp:Label>：<%--停用人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDISCONTINUEUSER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmBase_AGV_Label7%>"></asp:Label>：<%--停用时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDISCONTINUETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
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
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="False"></asp:Label>： <%--编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"
                                Visible="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIDS" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_Label1%>" Visible="False"></asp:Label>：<%--主表编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIDS" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"
                                Visible="False"></asp:TextBox>
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
                &nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="ButtonDel" OnClick="btnDelete_Click" Text="<%$ Resources:Lang, Common_Delete%>" CausesValidation="false" Visible="False" />
                <%--删除--%>	
                &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>

</asp:Content>
