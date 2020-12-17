

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBase_RGV_PCLADDR.aspx.cs" Inherits="Apps_BASE_frmBase_RGV_PCLADDR"
     MasterPageFile="~/Apps/DefaultMasterPage.master"
     %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBase_RGV_PCLADDR_Title01%>-&gt;<%= Resources.Lang.FrmBase_RGV_D_Msg05%><%--RGV站点管理-&gt;PLC地址详情--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
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
                            <asp:Label ID="lblCraneID" runat="server" Text="<%$ Resources:Lang, FrmBase_RGV_PCLADDR_lblCraneID%>"></asp:Label>：<%--PLC区域--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPLCREGION" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtPLCREGION" runat="server" ControlToValidate="txtPLCREGION"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_RGV_PCLADDR_rfvtxtPLCREGION%>" Display="None"> </asp:RequiredFieldValidator><%--请填写PLC区域!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblSiteID" runat="server" Text="<%$ Resources:Lang, FrmBase_RGV_PCLADDR_lblSiteID%>"></asp:Label>：<%--PLC地址--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPLCADDRESS" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtPLCADDRESS" runat="server" ControlToValidate="txtPLCADDRESS"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_RGV_PCLADDR_rfvtxtPLCADDRESS%>" Display="None"> </asp:RequiredFieldValidator><%--请填写PLC地址!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblSiteName" runat="server" Text="<%$ Resources:Lang, FrmBase_RGV_D_MEANING%>"></asp:Label>：<%--定义--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtMEANING" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtMEANING" runat="server" ControlToValidate="txtMEANING"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_RGV_PCLADDR_rfvtxtMEANING%>" Display="None"> </asp:RequiredFieldValidator><%--请填写定义!--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblSiteTypes" runat="server" Text="<%$ Resources:Lang, FrmBase_RGV_D_ASCRIPTION%>"></asp:Label>：<%--填值--%>
                        </td>
                        <td style="width: 20%">

                            <asp:DropDownList ID="ddlASCRIPTION" runat="server" Width="95%">
                                <asp:ListItem Value="" Selected="True">请选择</asp:ListItem>
                                <asp:ListItem Value="PLC">PLC</asp:ListItem>
                                <asp:ListItem Value="PC">PC</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label> <%--创建人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtcreateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label><%--创建时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtcreatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label3%>"></asp:Label> <%--修改人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtupdateuser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False"></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label4%>"></asp:Label>  <%--修改时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtupdatetime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False"></asp:TextBox>

                        </td>
                        
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblRemark" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：<%--备注--%>
                        </td>
                        <td colspan="5" style="width: 20%">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="100" Height="33px" Rows="2" TextMode="MultiLine"></asp:TextBox>
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
                <!--主键-->
                        <asp:HiddenField ID="hdnID" runat="server" />
                        <asp:HiddenField ID="hdnIDS" runat="server" />
            </td> 
        </tr>
    </table>
</asp:Content>