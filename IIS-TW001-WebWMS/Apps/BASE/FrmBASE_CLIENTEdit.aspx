<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBASE_CLIENTEdit.aspx.cs"
    Inherits="BASE_FrmBASE_CLIENTEdit" Title="--<%$ Resources:Lang, FrmBASE_CLIENTEdit_Title01%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBASE_CLIENTEdit_Title01%>-&gt;<%= Resources.Lang.FrmBASE_CLIENTEdit_Title01%><%--客户详情-&gt;客户详情--%>
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
                                    left: -5px;
                                    top: 2px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCCLIENTID" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCCLIENTID%>"></asp:Label>：<%--客户编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCLIENTID" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtCCLIENTID" runat="server" ControlToValidate="txtCCLIENTID"
                                ErrorMessage="<%$ Resources:Lang, FrmBASE_CLIENTEdit_rfvtxtCCLIENTID%>" Display="None">   <%--请填写客户编码--%>!
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCCLIENTNAME" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCCLIENTNAME%>"></asp:Label>：<%--客户名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCLIENTNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtCCLIENTNAME" runat="server" ControlToValidate="txtCCLIENTNAME"
                                ErrorMessage="<%$ Resources:Lang, FrmBASE_CLIENTEdit_rfvtxtCCLIENTNAME%>" Display="None"> 
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCALIAS" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCALIAS%>"></asp:Label>： <%--助记码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCALIAS" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCONTACTPERSON" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCCONTACTPERSON%>"></asp:Label>： <%--联系人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCONTACTPERSON" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCPHONE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCPHONE%>"></asp:Label>： <%--联系电话--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPHONE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCERPCODE%>"></asp:Label>： <%--ERP编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCADDRESS" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_CADDRESS%>"></asp:Label>：<%--联系地址--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCADDRESS" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTYPE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCTYPE%>"></asp:Label>：  <%--客户类型--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCTYPE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblILEVER" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblILEVER%>"></asp:Label>：  <%--级别--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtILEVER" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_txtIDS%>"></asp:TextBox><%--格式：最多10位整数，最多4位小数--%>
                            <asp:RegularExpressionValidator ID="revtxtILEVER" runat="server" ValidationExpression="^[0-9]{1,10}$"
                                ControlToValidate="txtILEVER" ErrorMessage="<%$ Resources:Lang, FrmBASE_CLIENTEdit_revtxtILEVER%>" Display="None"> <%--请填写有效的级别!正确的格式是：最多10位整数--%>
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dplCSTATUS" runat="server" Width="95%">
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="rfvtxtCSTATUS" runat="server" ControlToValidate="dplCSTATUS"
                                ErrorMessage="<%$ Resources:Lang, Common_SelectStatus%>" Display="None"> 
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
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label>：
                            <%--创建时间--%>
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
                            <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="False"></asp:Label>：
                            <%--编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"
                                Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：<%--备注--%>
                        </td>
                        <td colspan="5" style="width: 20%">
                            <asp:TextBox ID="txtCMEMO" runat="server" CssClass="NormalInputText" Width="95%"
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
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                    Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp; &nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="ButtonDel"
                    OnClick="btnDelete_Click" Text="<%$ Resources:Lang, Common_Delete%>" CausesValidation="false" Visible="False" />
                <%--删除--%>	
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>
</asp:Content>
