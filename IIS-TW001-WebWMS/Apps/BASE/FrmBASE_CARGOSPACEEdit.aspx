<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBASE_CARGOSPACEEdit.aspx.cs"
    Inherits="BASE_FrmBASE_CARGOSPACEEdit" Title="--<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_Title01%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Src="ShowWAREHOUSEDiv.ascx" TagName="ShowWAREHOUSEDiv" TagPrefix="ucwh" %>
<%@ Register Src="ShowAREADiv.ascx" TagName="ShowAREADiv" TagPrefix="ucArea" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBASE_CARGOSPACEEdit_Title01%>-&gt;<%= Resources.Lang.FrmBASE_CARGOSPACEEdit_Title01%><%--储位详情-&gt;储位详情--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <ucwh:ShowWAREHOUSEDiv ID="ucShowWAREHOUSEDiv" runat="server" />
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <ucArea:ShowAREADiv ID="ucShowArea" runat="server" />
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
                    <tr style="display: none;">
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

                                .auto-style1
                                {
                                    width: 16px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCCARGOID" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblCWAREID%>"></asp:Label>： <%--编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCARGOID" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30" Enabled="False"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtCCARGOID" runat="server" ControlToValidate="txtCCARGOID"
                                ErrorMessage="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_rfvtxtCCARGOID%>" Display="None"> <%--请填写编号!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCCARGONAME" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_Mag05%>"></asp:Label>： <%--储位--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCARGONAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30" Enabled="False"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtCCARGONAME" runat="server" ControlToValidate="txtCCARGONAME"
                                ErrorMessage="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_rfvtxtCCARGONAME%>" Display="None"> <%--请填写储位名称!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIMAXCAPACITY" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IMAXCAPACITY%>"></asp:Label>： <%--最大量--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIMAXCAPACITY" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="12"></asp:TextBox>
                            <%-- <asp:RegularExpressionValidator ID="revtxtIMAXCAPACITY" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,12}(\.[0-9]{1,2}){0,1}"
                                ControlToValidate="txtIMAXCAPACITY" ErrorMessage="请填写有效的最大量!正确的格式是：最多12位整数，最多2位小数"
                                Display="None"> 
                            </asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCALIAS" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCALIAS%>"></asp:Label>： <%--助记码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCALIAS" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCERPCODE%>"></asp:Label>： <%--ERP编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">
                                <asp:Label ID="Lab_Ctype" runat="server" Visible="false" Text="*"></asp:Label></span>
                            <asp:Label ID="lblCTYPE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblCTYPE%>"></asp:Label>： <%--种类--%>
                        </td>
                        <td style="width: 20%">
                            <%--<asp:TextBox ID="txtCTYPE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>--%>
                            <asp:DropDownList ID="DropCTYPE" runat="server" Width="95%">
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCDEFINE1" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_area_name%>"></asp:Label>： <%--区 域--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCDEFINE1" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                            <asp:TextBox ID="txtAreaID" runat="server"
                                Style="display: none;"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblWAREHOUSEID" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_CWARENAME%>"></asp:Label>： <%--所属仓库--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="ddlWareHouse" runat="server" Width="95%" AutoPostBack="true" OnSelectedIndexChanged="ddlWareHouse_SelectedIndexChanged"></asp:DropDownList>
                            <asp:HiddenField ID="hfHouseID" runat="server" />

                            <%--<asp:TextBox ID="txtWAREHOUSE_Name" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Visible="false"></asp:TextBox>     
                            
                            
                            <asp:RequiredFieldValidator ID="rfvtxtWAREHOUSEID" runat="server" ControlToValidate="txtWAREHOUSE_Name"
                                ErrorMessage="请填写所属仓库ID!" Display="None"> 
                            </asp:RequiredFieldValidator>--%>
                           

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblIPRIORITY" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IPRIORITY%>"></asp:Label>：  <%--优先级--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtIPRIORITY" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_txtIDS%>">0</asp:TextBox>
                            <%--格式：最多10位整数，最多4位小数--%>

                            <asp:RequiredFieldValidator ID="rfvtxtIPRIORITY" runat="server" ControlToValidate="txtIPRIORITY"
                                ErrorMessage="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_rfvtxtIPRIORITY%>" Display="None"> <%--请填写优先级!--%>
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtIPRIORITY" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,10}(\.[0-9]{1,4}){0,1}"
                                ControlToValidate="txtIPRIORITY" ErrorMessage="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_revtxtIPRIORITY%>"
                                Display="None"> <%--请填写有效的优先级!正确的格式是：最多10位整数，最多4位小数--%>
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblILENGTH" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_ILENGTH%>"></asp:Label>： <%--长--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtILENGTH" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_Msg01%>"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                            <asp:RegularExpressionValidator ID="revtxtILENGTH" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,16}(\.[0-9]{1,2}){0,1}"
                                ControlToValidate="txtILENGTH" ErrorMessage="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_revtxtILENGTH%>" Display="None"> 
                            </asp:RegularExpressionValidator><%--请填写有效的长!正确的格式是：最多16位整数，最多2位小数--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIWIDTH" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IWIDTH%>"></asp:Label>： <%--宽--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIWIDTH" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_Msg01%>"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                            <asp:RegularExpressionValidator ID="revtxtIWIDTH" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,16}(\.[0-9]{1,2}){0,1}"
                                ControlToValidate="txtIWIDTH" ErrorMessage="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_revtxtIWIDTH%>" Display="None"> 
                            </asp:RegularExpressionValidator><%--请填写有效的宽!正确的格式是：最多16位整数，最多2位小数--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIHEIGHT" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IHEIGHT%>"></asp:Label>：<%--高--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIHEIGHT" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_Msg01%>"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                            <asp:RegularExpressionValidator ID="revtxtIHEIGHT" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,16}(\.[0-9]{1,2}){0,1}"
                                ControlToValidate="txtIHEIGHT" ErrorMessage="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_revtxtIHEIGHT%>" Display="None"> 
                            </asp:RegularExpressionValidator><%--请填写有效的高!正确的格式是：最多16位整数，最多2位小数--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIVOLUME" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IVOLUME%>"></asp:Label>：<%--体积--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIVOLUME" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_Msg01%>"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                            <asp:RegularExpressionValidator ID="revtxtIVOLUME" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,16}(\.[0-9]{1,2}){0,1}"
                                ControlToValidate="txtIVOLUME" ErrorMessage="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_revtxtIVOLUME%>"
                                Display="None"> <%--请填写有效的体积!正确的格式是：最多16位整数，最多2位小数--%>
                            </asp:RegularExpressionValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCUSETYPE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_lblCUSETYPE%>"></asp:Label><%--用途--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCUSETYPE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIPERMITMIX" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblIPERMITMIX%>"></asp:Label>： <%--是否允许混放--%>
                        </td>
                        <td style="width: 20%">
                            <asp:CheckBox ID="cboIPERMITMIX" runat="server" Checked="true" />
                        </td>
                    </tr>
                    <tr id="lab_xyz">
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">
                                <asp:Label ID="Lab_X" runat="server" Visible="false" Text="*"></asp:Label></span>
                            <asp:Label ID="lblCX" runat="server" Text="x："></asp:Label>

                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCX" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20" ToolTip="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_txtCX%>"></asp:TextBox><%--格式：为两位数字编码--%>
                             
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">
                                <asp:Label ID="Lab_Y" runat="server" Visible="false" Text="*"></asp:Label></span>
                            <asp:Label ID="lblCY" runat="server" Text="y："></asp:Label>

                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCY" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20" ToolTip="格式：为三位数字编码"></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">
                                <asp:Label ID="Lab_Z" runat="server" Visible="false" Text="*"></asp:Label></span>
                            <asp:Label ID="lblCZ" runat="server" Text="z："></asp:Label>

                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCZ" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20" ToolTip="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_txtCX%>"></asp:TextBox><%--格式：为两位数字编码--%>
                        
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDEXPIREDATE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblDEXPIREDATEFromFrom%>"></asp:Label>： <%--终止日期--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDEXPIREDATE" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_Label1%>"></asp:Label>：<%--是否允许调拨--%>
                        </td>
                        <td style="width: 20%;">
                            <asp:DropDownList ID="ddlallo" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txthfWareHouseId" runat="server" Style="display: none" />
                            <asp:TextBox ID="txthfWorkType" runat="server" Style="display: none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_Label7%>"></asp:Label>：
                            <%--是否存在栈板--%></td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="Droppalletcode" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">
                            <asp:Label ID="Lab_Line" runat="server" Visible="true" Text="*"></asp:Label></span>
                            <asp:Label ID="Label8" runat="server" Visible="true" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lineid%>"></asp:Label>：<%--线别--%> 
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlLineID" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>

                        <td class="InputLabel" style="width: 13%">&nbsp;</td>
                        <td style="width: 20%">&nbsp;</td>

                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：<%--创建人--%>
                            <%--创建人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcreateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label>：<%--创建时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcreatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label3%>"></asp:Label>：
                            <%--修改人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtupdateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label4%>"></asp:Label>：
                            <%--修改时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtupdatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="False">
                                
                            </asp:TextBox>

                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_Label6%>" Style="display: none;"></asp:Label>：</td><%--产品编码--%>
                        <td>
                            <asp:TextBox ID="txtProductCode" runat="server" Width="95%" Style="display: none;"></asp:TextBox></td>
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="ID：" Visible="False"></asp:Label>
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
                        <td style="width: 20%;" colspan="5">
                            <asp:TextBox ID="txtCMEMO" runat="server" Width="99%" MaxLength="200" TextMode="MultiLine"
                                Rows="2"></asp:TextBox>
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
                &nbsp;&nbsp; &nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="ButtonDel"
                    OnClick="btnDelete_Click" Text="<%$ Resources:Lang, Common_Delete%>" CausesValidation="false" Visible="false" /> <%--删除--%>	
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>
</asp:Content>
