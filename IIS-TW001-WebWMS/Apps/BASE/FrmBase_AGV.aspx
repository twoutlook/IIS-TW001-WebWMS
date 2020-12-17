<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBase_AGV.aspx.cs" Inherits="Apps_BASE_FrmBase_AGV"
    MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBase_CraneConfig_Msg17%>-&gt;<%= Resources.Lang.FrmBase_AGV_Title01%><%--AGV管理-&gt;AGV详情--%>
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
                        <td class="InputLabel" style="width: 13%"> 
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCraneID" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label7%>"></asp:Label>：<%--AGV编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCraneID" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                           
                            <asp:RequiredFieldValidator ID="rfvtxtCraneID" runat="server" ControlToValidate="txtCraneID"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_AGV_rfvtxtCraneID%>" Display="None"> <%--请填写AGV编号!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCraneNAME" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label8%>"></asp:Label>：<%--AGV名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCraneNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtCraneNAME" runat="server" ControlToValidate="txtCraneNAME"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_AGV_rfvtxtCraneNAME%>" Display="None"> <%--请填写AGV名称!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCRANEIP" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label9%>"></asp:Label>：<%--供应商--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlSUPPLIER" runat="server" Width="95%"
                                runat="server">
                                <asp:ListItem Value="0">海康威视</asp:ListItem>
                                <asp:ListItem Value="1">其它</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblTTYPE" runat="server" Text="<%$ Resources:Lang, FrmBase_AGV_lblTTYPE%>"></asp:Label>：<%--型号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtTTYPE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span style="position: relative;left: -18px;top: 2px;color: #FF0000;font-weight: bold;">*</span>
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dplCSTATUS" runat="server" Width="95%">
                                <asp:ListItem Value="0">使用中</asp:ListItem>
                                <asp:ListItem Value="1">停用</asp:ListItem>
                            </asp:DropDownList>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtCSTATUS" runat="server" ControlToValidate="dplCSTATUS"
                                ErrorMessage="<%$ Resources:Lang, Common_SelectStatus%>" Display="None"> 
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：<%--创建人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCreateUser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"
                                MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label>：<%--创建时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCreateTime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"
                                MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label3%>"></asp:Label>：<%--修改人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtModifyUser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label4%>"></asp:Label>：<%--修改时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtModifyTime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"
                                MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBase_AGV_Label1%>"></asp:Label> <%--停用人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDISCONTINUEUSER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmBase_AGV_Label7%>"></asp:Label><%--停用时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDISCONTINUETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblReamark" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：<%--备注--%>
                        </td>
                        <td style="width: 20%" colspan="5">
                            <asp:TextBox ID="txtReamark" runat="server" CssClass="NormalInputText" Width="99%" MaxLength="100" Rows="2" Height="33px" TextMode="MultiLine"></asp:TextBox>
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
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                    Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
                <asp:HiddenField ID="hdnID" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

