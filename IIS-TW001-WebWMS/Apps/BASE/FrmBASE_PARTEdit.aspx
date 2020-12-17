<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBASE_PARTEdit.aspx.cs"
    Inherits="BASE_FrmBASE_PARTEdit" Title="--<%$ Resources:Lang, FrmBASE_PARTEdit_Title01%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Src="ShowWAREHOUSEDiv.ascx" TagName="ShowWAREHOUSEDiv" TagPrefix="ucShowWAREHOUSEDiv" %>
<%@ Register Src="ShowBASE_CARGOSPACEDiv.ascx" TagName="ShowBASE_CARGOSPACEDiv" TagPrefix="ucShowBASE_CARGOSPACEDiv" %>
<%@ Register Src="ShowVENDORDiv.ascx" TagName="ShowVENDORDiv" TagPrefix="ucShowVENDORDiv" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <style type="text/css">
        span.requiredSign {
            color: #FF0000;
            font-weight: bold;
            position: relative;
            left: -5px;
            top: 2px;
        }

    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBASE_PARTList_Title01%>-&gt;<%= Resources.Lang.FrmBASE_PARTEdit_Title01%><%--物料管理-&gt;物料详情--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <ucShowWAREHOUSEDiv:ShowWAREHOUSEDiv ID="ucShowWAREHOUSEDiv" runat="server" />
    <ucShowBASE_CARGOSPACEDiv:ShowBASE_CARGOSPACEDiv ID="ucBASE_CARGOSPACEDiv" runat="server" />
    <ucShowVENDORDiv:ShowVENDORDiv ID="ucVENDORDiv" runat="server" />
    
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCPARTNUMBER" runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label>：<%--料号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPARTNUMBER" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50" Enabled="True"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtCPARTNUMBER" runat="server" ControlToValidate="txtCPARTNUMBER"
                                ErrorMessage="<%$ Resources:Lang, FrmBASE_PARTEdit_rfvtxtCPARTNUMBER%>" Display="None"> <%--请填写料号!--%>
                            </asp:RequiredFieldValidator>
                        </td>

                        <%-- Note By Qamar 2020-11-09 --%>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblRANK_FINAL" runat="server" Text="批/序號(RANK)"></asp:Label>：<%--rank--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtRANK_FINAL" runat="server" CssClass="NormalInputText" Width="98%" MaxLength="1" Enabled="True"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCPARTNAME" runat="server" Text="<%$ Resources:Lang, Common_CinvName1%>"></asp:Label>：<%--品名--%>
                        </td>
                        <td style="width: 20%" colspan="1">
                            <asp:TextBox ID="txtCPARTNAME" runat="server" CssClass="NormalInputText" Width="98%" MaxLength="100" Enabled="True"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtCPARTNAME" runat="server" ControlToValidate="txtCPARTNAME"
                                ErrorMessage="<%$ Resources:Lang, FrmBASE_PARTEdit_rfvtxtCPARTNAME%>" Display="None"> <%--请填写品名!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTYPE" runat="server" Text="<%$ Resources:Lang, Common_Type%>"></asp:Label>： <%--类型--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCTYPE" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_Label2%>"></asp:Label>：<%--类别--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="DpD_ABCTYpe" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCERPCODE%>"></asp:Label>： <%--ERP编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCALIAS" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCALIAS%>"></asp:Label>： <%--助记码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCALIAS" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSPECIFICATIONS" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>：<%--规格--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCSPECIFICATIONS" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCVOLUME" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IVOLUME%>"></asp:Label>：<%--体积--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCVOLUME" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_Msg01%>"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                            <asp:RegularExpressionValidator ID="revtxtCVOLUME" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,16}(\.[0-9]{1,2}){0,1}"
                                ControlToValidate="txtCVOLUME" ErrorMessage="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_revtxtIVOLUME%>"
                                Display="None"> <%--请填写有效的体积!正确的格式是：最多16位整数，最多2位小数--%>
                            </asp:RegularExpressionValidator>
                        </td>
                        

                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblICW" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTEdit_lblICW%>"></asp:Label>：<%--毛重--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtICW" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_Msg01%>"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                            <asp:RegularExpressionValidator ID="revtxtICW" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,16}(\.[0-9]{1,2}){0,1}"
                                ControlToValidate="txtICW" ErrorMessage="<%$ Resources:Lang, FrmBASE_PARTEdit_revtxtICW%>" Display="None"> <%--请填写有效的毛重!正确的格式是：最多16位整数，最多2位小数--%>
                            </asp:RegularExpressionValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblINW" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTEdit_lblINW%>"></asp:Label>：<%--净重--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtINW" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_Msg01%>"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                            <asp:RegularExpressionValidator ID="revtxtINW" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,16}(\.[0-9]{1,2}){0,1}"
                                ControlToValidate="txtINW" ErrorMessage="<%$ Resources:Lang, FrmBASE_PARTEdit_revtxtINW%>" Display="None">  <%--请填写有效的净重!正确的格式是：最多16位整数，最多2位小数--%>
                            </asp:RegularExpressionValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCUNITS" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTEdit_lblCUNITS%>"></asp:Label>：<%--单位--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCUNITS" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblILENGTH" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_ILENGTH%>">：</asp:Label><%--长--%>
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
                            <asp:Label ID="lblCSAFEQTY" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_SafeStockLimit%>"></asp:Label>：<%--安全库存--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCSAFEQTY" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_Msg01%>"></asp:TextBox><%--格式：最多16位整数，最多2位小数--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_SafeStockCeiling%>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCsafeQtyCeiling" runat="server" CssClass="NormalInputText" Width="95%" ></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_Label1%>"></asp:Label>： <%--是否保税--%>
                        </td>
                        <td style="width: 20%">
                            <asp:CheckBox ID="CBox_Bonded" runat="server" Checked="true" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblINEEDCHECK" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_lblINEEDCHECK%>"></asp:Label>：<%--是否免检--%>
                        </td>
                        <td style="width: 20%">
                            <asp:CheckBox ID="cboINEEDCHECK" runat="server" Checked="true" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblINEEDWARN" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_lblINEEDWARN%>"></asp:Label>：<%--是否预警--%>
                        </td>
                        <td style="width: 20%">
                            <asp:CheckBox ID="cboINEEDWARN" runat="server" Checked="true" />
                        </td>

                    </tr>

                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCDEFAULTWARE" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CDEFAULTWARE%>"></asp:Label>：<%--默认仓库--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCDEFAULTWARE_Name" runat="server" CssClass="NormalInputText"
                                Width="95%" MaxLength="20"></asp:TextBox>
                            <asp:HiddenField ID="hfCDEFAULTWARE_Id" runat="server" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCDEFAULTCARGO" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CDEFAULTCARGO%>"></asp:Label>：<%--默认储位--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCDEFAULTCARGO_Name" runat="server" CssClass="NormalInputText"
                                Width="95%" MaxLength="20"></asp:TextBox>
                            <asp:HiddenField ID="hfCDEFAULTCARGO" runat="server" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCDEFAULTVENDOR" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CDEFAULTVENDOR%>"></asp:Label>： <%--默认供应商--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCDEFAULTVENDOR_Name" runat="server" CssClass="NormalInputText"
                                Width="95%" MaxLength="20"></asp:TextBox>
                            <asp:HiddenField ID="hfCDEFAULTVENDOR" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCINRULE" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CINRULE%>"></asp:Label>：<%--上架规则--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCINRULE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="1000"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCOUTRULE" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_COUTRULE%>"></asp:Label>：<%--下架规则--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCOUTRULE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="1000"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCBARRULE" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CBARRULE%>"></asp:Label>：<%--条码规则--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCBARRULE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="1000"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCVERSION" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_lblCVERSION%>"></asp:Label>：<%--版本：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCVERSION" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
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
                            <asp:Label ID="lblDEXPIREDATE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblDEXPIREDATEFromFrom%>"></asp:Label>： <%--终止日期--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDEXPIREDATE" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, Common_Format%>"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDEXPIREDATE','y-mm-dd',0);" />

                        </td>
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
                        <td style="width: 20%">
                            <asp:TextBox ID="txtupdatetime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False"></asp:TextBox>
                        </td>                 
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="ID：" Visible="false"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Visible="false"
                                Width="95%" MaxLength="36"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblpcode" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_Label6%>"></asp:Label>：<%--产品编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtproductcode" runat="server" CssClass="" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTEdit_Label7%>"></asp:Label>：<%--包装数量--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtBoxNum" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmBASE_PARTEdit_txtBoxNum%>"></asp:TextBox><%--格式：整数--%>
                            <%--<asp:RegularExpressionValidator ID="revtxtBoxNum" runat="server" ValidationExpression="^\+?[1-9][0-9]*$"
                                ControlToValidate="txtBoxNum" ErrorMessage="请填写有效的包装数量!正确的格式是：整数"
                                Display="None"> 
                            </asp:RegularExpressionValidator>--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_DRPNEEDSERIAL%>"></asp:Label>：<%--是否序列号管控--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="drpNeedSerial" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCUSETYPE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_lblCUSETYPE%>"></asp:Label>：<%--用途--%>
                        </td>
                        <td style="width: 20%" colspan="5">
                            <asp:TextBox ID="txtCUSETYPE" runat="server" TextMode="MultiLine" Width="99%" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：<%--备注--%>
                        </td>
                        <td style="width: 20%" colspan="5">
                            <asp:TextBox ID="txtCMEMO" runat="server" TextMode="MultiLine" Width="99%" MaxLength="100"></asp:TextBox>
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

                                //选择储位
                                function SetCARGOSPACEValue(ControlName, Values, ControlName2, Values2) {
                                    if (ControlName != "") {
                                        document.all(ControlName).value = Values;
                                    }
                                    if (ControlName2 != "") {
                                        document.all(ControlName2).value = Values2;
                                    }
                                    //getPartNameByPartCode($("#ctl00_ContentPlaceHolderMain_rfvtxtCPARTNUMBER").val());
                                    getPartNameByPartCode("ctl00_ContentPlaceHolderMain_rfvtxtCPARTNUMBER");
                                }

                                //通过储位获取品名
                                function getPartNameByPartCode(PartCode) {
                                    if (PartCode != "") {
                                        var desc = document.all(PartCode).value;
                                    }
                                }
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
            <td style="text-align:center;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                    Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="ButtonDel" OnClick="btnDelete_Click"
                    Text="<%$ Resources:Lang, Common_Delete%>" CausesValidation="false" Visible="false" />
                <%--删除--%>	
                &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>"
                    CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>
</asp:Content>
