<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" Title="<%$ Resources:Lang, FrmBaseDocuReason_List_Msg12%>" CodeFile="FrmBaseDocuReason_Edit.aspx.cs" Inherits="BASE_FrmBaseDocuReason_Edit" %><%--理由码详情--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBaseDocuReason_List_Title01%>-&gt;<%= Resources.Lang.FrmBaseDocuReason_List_Msg12%><%--理由码设置-&gt;理由码详情--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
                     <tr style="display: none;">
                        <td colspan="6">
                            <style type="text/css">
                                span.requiredSign {
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
                            <span class="requiredSign">
                                <asp:Label ID="Lab_ACTIONSCOPE" runat="server" Visible="true" Text="*"></asp:Label></span>
                         
                            <asp:Label ID="lblInOut" runat="server" Text="<%$ Resources:Lang, FrmBaseCONFIG_List_Label2%>"></asp:Label>：<%--作用域--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlACTIONSCOPE" runat="server" Width="95%">
                            </asp:DropDownList>
                              
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign" style="color: red;">*</span>
                            <asp:Label ID="lblREASONCODE" runat="server" Text="<%$ Resources:Lang, FrmBaseDocuReason_List_lblREASONCODE%>"></asp:Label>：<%--理由码编号--%>
                        </td>
                        <td style="width: 20%">

                            <asp:TextBox ID="txtREASONCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign" style="color: red;">*</span>
                            <asp:Label ID="lblREASONCONTENT" runat="server" Text="<%$ Resources:Lang, FrmBaseDocuReason_List_REASONCONTENT%>"></asp:Label>：<%--理由码说明--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtREASONCONTENT" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">
                                <asp:Label ID="Lab_STATES" runat="server" Visible="true" Text="*"></asp:Label></span>
                            <asp:Label ID="lblSTATES" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlSTATES" runat="server" Width="95%" Enabled="false">
                            </asp:DropDownList>
                             
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：<%--创建人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="100" Enabled="false"> </asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, Common_CreateDate%>"></asp:Label>： <%--创建日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblLASTUPDATEOWNER" runat="server" Text="<%$ Resources:Lang, FrmMixed_D_Label6%>"></asp:Label>：<%--最后修改人--%>
                        </td> 
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLASTUPDATEOWNER" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblLASTUPDATETIME" runat="server" Text="<%$ Resources:Lang, FrmMixed_D_Label7%>"></asp:Label>： <%--最后修改时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLASTUPDATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBaseDocuReason_Edit_ISFROMERP%>"></asp:Label>：<%--仅查询--%>
                        </td>
                       <td style="width: 20%">
                            <asp:CheckBox runat="server"  ID="ckbisfromerp" Enabled="false"/>
                        </td>

                    </tr>
                       <tr style="display: none;">
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="36"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                    <td colspan="2">
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
                <asp:HiddenField ID ="hdnID" runat="server" />
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                    Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>
</asp:Content>
