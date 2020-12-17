<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBAS_AREAEdit.aspx.cs"
    Inherits="FrmBAS_AREAEdit" Title="--111" MasterPageFile="~/Apps/DefaultMasterPage.master" %>
<%@ Register Src="ShowPARTByInAsnIdDiv.ascx" TagName="ShowPARTByInAsnIdDiv" TagPrefix="uc1" %>
<%@ Register Src="ShowBASE_CARGOSPACEDiv_T.ascx" TagName="ShowBASE_CARGOSPACEDiv_T" TagPrefix="uc2" %>


<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
     <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.FrmBASE_AREAList_Title01%><%--基礎資料-&gt;区域管理--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <ajaxToolkit:toolkitscriptmanager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <uc1:ShowPARTByInAsnIdDiv ID="ShowPARTDiv1" runat="server" />
  
    <uc2:ShowBASE_CARGOSPACEDiv_T ID="ShowBASE_CARGOSPACEDiv_T" runat="server" />

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
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblAreaame" runat="server" Text="<%$ Resources:Lang, FrmBASE_WL_AREA_lblPALLETID%>"></asp:Label>： <%--区域名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtAreaName" runat="server" CssClass="NormalInputText" Width="85%"
                                MaxLength="40"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAreaName"
                                ErrorMessage="<%$ Resources:Lang, FrmBAS_AREAEdit_RequiredFieldValidator1%>" Display="None"> </asp:RequiredFieldValidator><%--请填写区域名称!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBASE_WL_AREA_labBlCw%>"></asp:Label>：<%--備料儲位--%>
                        </td>
                        <td  style="width: 20%">
                            <asp:TextBox ID="txtCPOSITIONCODE" runat="server" CssClass="NormalInputText" Width="85%"
                                MaxLength="30"></asp:TextBox> 
                                
                            &nbsp;<asp:RequiredFieldValidator ID="rfvtxtCPOSITIONCODE" runat="server" ControlToValidate="txtCPOSITIONCODE"
                                ErrorMessage="<%$ Resources:Lang, FrmBAS_AREAEdit_rfvtxtCPOSITIONCODE%>" Display="None"> </asp:RequiredFieldValidator> <%--请填写储位编码!--%>
                        
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmBAS_AREAEdit_Label2%>"></asp:Label>：<%--備料儲位名稱--%>
                        </td>
                        <td  style="width: 20%">
                            <asp:TextBox runat="server" ID="txtcpoName" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                       
                       <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmBASE_AREAList_Label3%>"></asp:Label>：<%--超发--%>
                        </td>
                       <td  style="width: 20%">
                            <asp:CheckBox ID="cbCF" runat="server" />
                        </td>
                       <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：<%--创建人--%>
                        </td>
                       <td style="width: 20%">
                            <asp:TextBox ID="txtCreateOwner" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="100" Enabled="false"> </asp:TextBox>
                        </td>
                       <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCreateTime" runat="server" Text="<%$ Resources:Lang, Common_CreateDate%>"></asp:Label>： <%--创建日期--%>
                        </td>
                       <td style="width: 20%">
                            <asp:TextBox ID="txtCreateTime" runat="server" CssClass="NormalInputText" Width="95%"
                               Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_AREAList_Label1%>"></asp:Label>：<%--是否控制区域--%>
                        </td>
                        <td  style="width: 20%">
                             <asp:DropDownList ID="ddpControl" runat="server" Width="95%" >
                                <asp:ListItem Value="0" Selected="True">是</asp:ListItem>
                                <asp:ListItem Value="1">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label3%>"></asp:Label>： <%--修改人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtupdateuser" runat="server" CssClass="NormalInputText" Width="95%"  Enabled="False"></asp:TextBox>
                           
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label4%>"></asp:Label>：  <%--修改时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtupdatetime" runat="server" CssClass="NormalInputText" Width="95%"  Enabled="False"></asp:TextBox> 
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_TEMPORARYAREA%>"></asp:Label>：<%--暂存区--%>
                        </td>
                        <td style="width: 20%">
                            <asp:CheckBox ID="chkIsTemporaryArea" runat="server" />
                        </td>
                        <td colspan="4">

                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：<%--备注--%>
                        </td>
                        <td colspan="6">
                            <asp:TextBox ID="txtCMEMO" runat="server" CssClass="NormalInputText" Width="99%"
                                TextMode="MultiLine" Height="50"></asp:TextBox>      
                            <asp:TextBox ID="txtId" runat="server" 
                                 Height="21px"  Visible="false" Width="293px"></asp:TextBox>
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
                &nbsp;&nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>
  
</asp:Content>
