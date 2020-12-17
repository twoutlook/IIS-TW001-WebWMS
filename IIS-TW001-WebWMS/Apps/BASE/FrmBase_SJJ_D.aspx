<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBase_SJJ_D.aspx.cs" Inherits="Apps_BASE_FrmBase_SJJ_D"
     MasterPageFile="~/Apps/DefaultMasterPage.master"
     %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBase_SJJ_D_Title01%>-&gt;<%= Resources.Lang.FrmBase_AGV_D_Title01%><%--提升机线别管理-&gt;站点管理--%>
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
                            <span style="position: relative;left: -10px;top: 2px;color: #FF0000;font-weight: bold;">*</span>
                            <asp:Label ID="lblCraneID" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label13%>"></asp:Label>：<%--提升机编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCraneID" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtCraneID" runat="server" ControlToValidate="txtCraneID"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_SJJ_D_rfvtxtCraneID%>" Display="None"> </asp:RequiredFieldValidator> <%--请填写提升机编号!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblSiteID" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label5%>"></asp:Label>：<%--站点编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSiteID" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtSiteID" runat="server" ControlToValidate="txtSiteID"
                               ErrorMessage="<%$ Resources:Lang, FrmBase_AGV_D_rfvtxtSiteID%>" Display="None"> </asp:RequiredFieldValidator><%--请填写站点编号!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span style="position: relative;left: -8px;top: 2px;color: #FF0000;font-weight: bold;">*</span>
                            <asp:Label ID="lblSiteName" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label10%>"></asp:Label>： <%--站点名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSiteName" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtSiteName" runat="server" ControlToValidate="txtSiteName"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_AGV_D_rfvtxtSiteName%>" Display="None"> </asp:RequiredFieldValidator><%--请填写站点名称!--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblSiteTypes" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label11%>"></asp:Label>：<%--站点类型--%>
                        </td>
                        <td style="width: 20%">

                            <asp:DropDownList ID="dplSiteTypes" runat="server" Width="95%">
                                <%--<asp:ListItem Value="3">全部</asp:ListItem>
                                <asp:ListItem Value="1">入</asp:ListItem>
                                <asp:ListItem Value="2">出</asp:ListItem>--%>
                            </asp:DropDownList>


                            
                            <asp:RequiredFieldValidator ID="rfvtxtSiteTypes" runat="server" ControlToValidate="dplSiteTypes"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_AGV_D_rfvddlAGVSITETYPE%>" Display="None"> </asp:RequiredFieldValidator> <%--请填写站点类型!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%"> 
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblDefulSite" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfigEdit_DEFULSITE%>"></asp:Label>：<%--默认站点--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dplDefulSite" runat="server" Width="95%">
                                <%--<asp:ListItem Value="0">是</asp:ListItem>
                                <asp:ListItem Value="1">否</asp:ListItem>--%>
                            </asp:DropDownList>
                           
                            <asp:RequiredFieldValidator ID="rfvtxtDefulSite" runat="server" ControlToValidate="dplDefulSite"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_SJJ_D_rfvtxtDefulSite%>" Display="None"> </asp:RequiredFieldValidator><%--请填写默认站点!--%>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblFormat" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>：<%--规格--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlFormat" runat="server" Width="95%">
                                <%--<asp:ListItem Value="1">110</asp:ListItem>
                                <asp:ListItem Value="2">115</asp:ListItem>--%>
                            </asp:DropDownList>
                            
                            <asp:RequiredFieldValidator ID="rfvddlFormat" runat="server" ControlToValidate="ddlFormat"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_AGV_D_rfvddlFormat%>" Display="None"> </asp:RequiredFieldValidator><%--请填写规格!--%>
                        </td>
                    </tr>
                
                     <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_two2%>"></asp:Label>：<%--AGV站点--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlAGVSITE" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, FrmBase_RGV_D_Label12%>"></asp:Label>：<%--等待站点--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlStorageSite" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                             <span class="requiredSign">*</span>
                            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label19%>"></asp:Label>：<%--楼层--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSTOREY" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtSTOREY" runat="server" ControlToValidate="txtSTOREY"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_AGV_D_rfvtxtSTOREY%>" Display="None"> </asp:RequiredFieldValidator><%--请填写楼层!--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span style="position: relative;left: -25px;top: 2px;color: #FF0000;font-weight: bold;">*</span>
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dplCSTATUS" runat="server" Width="95%">
                                <%--<asp:ListItem Value="0">使用中</asp:ListItem>
                                <asp:ListItem Value="1">停用</asp:ListItem>--%>
                            </asp:DropDownList>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtCSTATUS" runat="server" ControlToValidate="dplCSTATUS"
                                ErrorMessage="<%$ Resources:Lang, Common_SelectStatus%>" Display="None"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Lang, FrmBase_RGV_PLCREGION%>"></asp:Label><%--PLC地址区--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtPLCREGION" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtPLCREGION" runat="server" ControlToValidate="txtPLCREGION"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_RGV_D_rfvtxtPLCREGION%>" Display="None"> </asp:RequiredFieldValidator><%--请填写PLC地址区!--%>
                        </td>
                       <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label> <%--创建人--%>
                        </td>
                         <td style="width: 21%">
                            <asp:TextBox ID="txtcreateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        
                        
                        
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label><%--创建时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtcreatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>
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
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmBase_AGV_Label1%>"></asp:Label><%--停用人--%>
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
                                MaxLength="20"  Enabled="False"></asp:TextBox>
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
            <td style="text-align:center;padding:15px 0px;">
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