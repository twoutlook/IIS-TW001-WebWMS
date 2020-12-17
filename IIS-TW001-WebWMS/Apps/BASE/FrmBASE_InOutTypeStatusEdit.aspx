<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
 Title="<%$ Resources:Lang, FrmBASE_InOutTypeStatusEdit_Title01%>" CodeFile="FrmBASE_InOutTypeStatusEdit.aspx.cs" Inherits="Apps_BASE_FrmBASE_InOutTypeStatusEdit" %><%--入出库类型详情--%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
<link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HolderTitle" Runat="Server">
<%= Resources.Lang.FrmBase_InOutTypeStatusList_Title01%>-&gt;<%= Resources.Lang.FrmBASE_InOutTypeStatusEdit_Title01%><%--入出库类型管理-&gt;入出库类型详情--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
<ajaxToolkit:toolkitscriptmanager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
  <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblInOut" runat="server" Text="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_lblInOut%>"></asp:Label>：<%--类型分类--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="drpInOut" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_lblCERPCODE%>"></asp:Label>：<%--类型编码--%>
                        </td>
                        <td  style="width: 20%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>                                                     
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblTYPENAME" runat="server" Text="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_lblTYPENAME%>"></asp:Label>：<%--类型名称--%>
                        </td>
                        <td  style="width: 20%">
                            <asp:TextBox ID="txtTYPENAME" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
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
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblStatus" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                       <td style="width: 20%">
                           <asp:DropDownList ID="ddpStatus" runat="server" Width="95%" Enabled="false">
                           </asp:DropDownList>
                        </td>
                    </tr>   
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblEnableName" runat="server" Text="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_ENABLEUSER%>"></asp:Label>：<%--作废人员--%>
                        </td>
                       <td style="width: 20%">
                            <asp:TextBox ID="txtEnableName" runat="server" CssClass="NormalInputText" Width="95%"
                               Enabled="false"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblEnableTime" runat="server" Text="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_ENABLEDATE%>"></asp:Label>：<%--作废日期--%>
                        </td>
                       <td style="width: 20%">
                            <asp:TextBox ID="txtEnableTime" runat="server" CssClass="NormalInputText" Width="95%"
                               Enabled="false"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBASE_InOutTypeStatusEdit_Label1%>"></asp:Label>：<%--仅查询--%>
                        </td>
                       <td style="width: 20%">
                            <asp:CheckBox runat="server"  ID="ckbQuery"/>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_IsMatchSo%>"></asp:Label>：<%--是否匹配单据--%>
                        </td>
                       <td style="width: 20%">
                            <asp:DropDownList ID="drpIsMatchSo" runat="server" Width="95%">
                           </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_IsMatchVendor%>"></asp:Label>：<%--是否匹配供应商--%>
                        </td>
                       <td style="width: 20%">
                            <asp:DropDownList ID="drpIsMatchVendor" runat="server" Width="95%">
                           </asp:DropDownList>
                        </td>
                        <td colspan="2"></td>
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

