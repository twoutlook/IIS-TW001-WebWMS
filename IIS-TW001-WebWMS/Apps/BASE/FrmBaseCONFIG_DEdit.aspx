<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBaseCONFIG_DEdit.aspx.cs"
    Inherits="FrmBaseCONFIG_DEdit" Title="-<%$ Resources:Lang, FrmBaseCONFIG_DEdit_Title01%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>
<%@ Register Src="ShowBOM_CinvCode_Div.ascx" TagName="ShowBOM_CinvCode_Div" TagPrefix="uc1" %>
<%--<%@ Register Src="ShowPARTDivRTV.ascx" TagName="ShowPARTDivRTV" TagPrefix="ucRTV" %>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
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

 <%--<%@ Register Src="ShowPARTDivRTV.ascx" TagName="ShowPARTDivRTV" TagPrefix="ucRTV" %>--%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.FrmBaseCONFIG_DEdit_Title01%>  <%--基础资料-&gt;配置文件编辑--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    
    <%--<%@ Register Src="ShowPARTDivRTV.ascx" TagName="ShowPARTDivRTV" TagPrefix="ucRTV" %>--%><%--<%@ Register Src="ShowPARTDivRTV.ascx" TagName="ShowPARTDivRTV" TagPrefix="ucRTV" %>--%>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <uc1:ShowBOM_CinvCode_Div ID="ShowBOM_CinvCode_Div1" runat="server" />
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
                                .style1
                                {
                                    width: 30%;
                                    height: 25px;
                                }
                            </style>
                        </td>
                    </tr>
                     <tr>
                        <td class="InputLabel" style="width: 20%; height: 25px;">
                            <span class="requiredSign">*</span>
                         <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBaseCONFIG_List_Label2%>"></asp:Label>： <%--作用域--%>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="txtType" runat="server" Width="99%" AutoPostBack="false">
                            </asp:DropDownList>
                        </td>
                         <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtType" ErrorMessage="作用域不可为空请填入"></asp:RequiredFieldValidator>
      --%>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 20%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCINVCODE" runat="server" Text="<%$ Resources:Lang, FrmBaseCONFIG_List_Label4%>"></asp:Label>：&nbsp&nbsp <%--配置代码--%>
                             
                        </td>
                        <td style="width: 30%" colspan="3">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="NormalInputText" Width="99%" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCode" ErrorMessage="<%$ Resources:Lang, FrmBaseCONFIG_DEdit_RequiredFieldValidator2%>"></asp:RequiredFieldValidator>  <%--配置代码不可为空请填入--%>
                            <asp:Literal ID="ltSearch" runat="server"></asp:Literal>
                        </td>
                    </tr>
                      <tr>
                        <td class="InputLabel" style="width: 20%">
                             <span class="requiredSign">*</span>
                            <asp:Label ID="lblIQUANTITY" runat="server" Text="<%$ Resources:Lang, frmBase_Config_lblCERPCODE%>"></asp:Label>：&nbsp&nbsp <%--配置参数--%>
                           
                        </td>
                        <td colspan="3" style="width: 20%">
                            <asp:TextBox ID="txtValue" runat="server" CssClass="NormalInputText" 
                                Width="99%" MaxLength="40" ></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtValue" runat="server" ErrorMessage="<%$ Resources:Lang, FrmBaseCONFIG_DEdit_RequiredFieldValidator1%>"></asp:RequiredFieldValidator>      <%--配置参数不可为空请填入--%>
                        </td>
                          
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 20%"> 
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCINVNAME" runat="server" Text="<%$ Resources:Lang, FrmBaseCONFIG_List_CONFIG_DESC%>"></asp:Label>：&nbsp&nbsp<%--配置描述--%>
                            
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtCONFIG_DESC" runat="server" Width="99%" MaxLength="40" 
                                TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCONFIG_DESC" runat="server" ErrorMessage="<%$ Resources:Lang, FrmBaseCONFIG_DEdit_RequiredFieldValidator3%>"></asp:RequiredFieldValidator> <%--配置描述不可为空请填入--%>
                        </td>
                    </tr>
                  
                    <tr>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label id="lblRemark" runat = "server" Text ="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：</td>  <%--备注--%>
                        <td colspan="3">
                            <asp:TextBox ID="txtCMEMO" runat="server" Width="99%" MaxLength="100" 
                                TextMode="MultiLine" Height="82px"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<%@ Register Src="ShowPARTDiv.ascx" TagName="ShowPARTDiv" TagPrefix="uc1" %>--%>
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
                <%--<%@ Register Src="ShowPARTDivRTV.ascx" TagName="ShowPARTDivRTV" TagPrefix="ucRTV" %>--%>
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave"  Text="<%$ Resources:Lang, Common_btnSave%>"  OnClick="btnSave_Click" /><%--保存--%>
                &nbsp;&nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
            
        </tr>
    </table>
</asp:Content>
