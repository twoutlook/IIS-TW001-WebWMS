<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmOUTBILL_DEdit.aspx.cs"
    Inherits="OUT_FrmOUTBILL_DEdit" Title="--出库单" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../BASE/ShowPARTByInAsnIdDiv.ascx" TagName="ShowPARTByInAsnIdDiv"  TagPrefix="uc1" %>
<%@ Register Src="../BASE/ShowBASE_CARGOSPACEDiv.ascx" TagName="ShowBASE_CARGOSPACEDiv" TagPrefix="uc2" %>
<%@ Register Src="../BASE/ShowBASE_CARGOSPACE_ByCinvcodeDiv.ascx" TagName="ShowBASE_CARGOSPACE_ByCinvcodeDiv" TagPrefix="uc3" %>
<%@ Register Src="../BASE/ShowPart_Asn_IDS_Div.ascx" TagName="ShowPart_Asn_IDS_Div" TagPrefix="uc6" %>
<%@ Register Src="~/Apps/OUT/ShowSNOutAllo.ascx" TagName="ShowSNOutAllo" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />

    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .displaynone {
            display: none;
        }
        .ui-autocomplete-loading {
            background: white url('../../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }

        .select {
            cursor: pointer;
            position: relative;
            left: -25px;
            top: 4px;
        }

        span.requiredSign {
            color: #FF0000;
            font-weight: bold;
            position: relative;
            left: -15px;
            top: 2px;
        }

        input[type='submit'][disabled], input[type='button'][disabled] {
            opacity: 0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70); /*兼容ie8及以下*/
        }

        .ajaxWebSearChBox {
            width:500px !important;
        }
        #ctl00_ContentPlaceHolderMain_showSn_ajaxWebSearChComp {
            width:900px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt;<%= Resources.Lang.FrmOUTBILL_DEdit_Menu_PageName %> &nbsp;<asp:Label ID="lblErrorMsg" runat="server" Text="" Style="color: Red;"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div style="display: none;">
        <iframe id="compareIframe" src=""></iframe>
    </div>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

    <uc1:ShowPARTByInAsnIdDiv ID="ShowPARTDiv1" runat="server" />
    <uc2:ShowBASE_CARGOSPACEDiv ID="ShowBASE_CARGOSPACEDiv2" runat="server" />
    <uc3:ShowBASE_CARGOSPACE_ByCinvcodeDiv Id="ShowBASE_CARGOSPACE_ByCinvcodeDiv1" runat="server" />
    <uc4:ShowSNOutAllo ID="showSn" runat="server" />
    <uc6:ShowPart_Asn_IDS_Div ID="ShowPart_Asn_IDS_Div1" runat="server" />
    <table id="Table3" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblIDS" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ZiBiaoCode %>"></asp:Label>：<%--子表编号--%>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtIDS" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="格式：最多10位整数，最多4位小数"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ZhuBiaoCode %>"></asp:Label>：<%--主表编号--%>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="格式：最多10位整数，最多4位小数"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmOUTBILL_DEdit_OutAsnIds %>"></asp:Label>：<%--通知单明细IDS--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtAsn_D_IDS" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="格式：最多10位整数，最多4位小数"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmOUTBILL_DEdit_OutBillCode %>"></asp:Label>：<%--出库单单号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtOutbillCode" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="格式：最多10位整数，最多4位小数"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_XiangCi %>"></asp:Label>：<%--项次--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLineId" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Lang, FrmOUTBILL_DEdit_OutAsnDetailLineid %>"></asp:Label>：<%--出库通知单明细项次--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtOutAsndLineID" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                            <img alt="" onclick="disponse_div(event,document.all('<%= ShowPart_Asn_IDS_Div1.GetDivName %>'));"
                                src="../../Images/Search.gif" class="select" />
                        </td>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblRANK_FINAL" runat="server" Text="批/序號(RANK)"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtRANK_FINAL" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">                           
                            <asp:Label ID="lblcespec" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcspecifications" runat="server" CssClass="NormalInputText" Width="95%"  Enabled="false"
                                ></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblDOUTDATE" runat="server" Text="<%$ Resources:Lang, FrmOUTBILL_DEdit_OutBillTime %>"></asp:Label>：<%--出库时间：--%>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtDOUTDATE" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="格式：yyyy-MM-dd"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDOUTDATE','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblCINVBARCODE" runat="server" Text="<%$ Resources:Lang, FrmOUTBILL_DEdit_BarCode %>"></asp:Label>：<%--条码--%>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtCINVBARCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCINVNAME" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtCINVNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="100"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblIQUANTITY" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Quantity %>"></asp:Label>：
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, WMS_Common_GridView_Status %>" Visible="false" Style="display: none;"></asp:Label>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtIQUANTITY" runat="server" CssClass="NormalInputText"
                                Width="95%" ToolTip="格式：最多12位整数，最多2位小数"></asp:TextBox>
                            <asp:DropDownList ID="dplCSTATUS" runat="server" Enabled="false" Visible="false"
                                Width="95%" runat="server" Style="display: none;">
                                <asp:ListItem Value="0">未处理</asp:ListItem>
                                <asp:ListItem Value="1">已完成</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ChaoFaShuLiang %>"></asp:Label>：<%--超发数量--%>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtLINE_QTY" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="格式：最多16位整数，最多2位小数"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 20%; display:none">
                            <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCPOSITIONCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CpositionCode %>"></asp:Label>：
                        </td>
                        <td style="width: 30%; display:none">
                            <asp:TextBox ID="txtCPOSITIONCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <%--Note by Qamar 2020-11-27 Resources:Lang, WMS_Common_Element_Cposition改--%>
                            <asp:Label ID="lblCPOSITION" runat="server" Text="<%$ Resources:Lang, Common_PartnumName %>"></asp:Label>：
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtCPOSITION" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>
                            <img alt="" onclick="Show('<%= ShowBASE_CARGOSPACEDiv2.GetDivName %>');" src="../../Images/Search.gif"
                                class="select" />
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblCERPCODELINE" runat="server" Text="<%$ Resources:Lang, FrmOUTBILL_DEdit_ErpCodeLineId %>"></asp:Label>：<%--ERP单号项次--%>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtCERPCODELINE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblIOUTASNLINE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_TongZhiDanXiangCi %>" Style="display: none;"></asp:Label><%--出库通知单项次--%>
                            <asp:Label ID="lblCOUTPERSONCODE" runat="server" Text="<%$ Resources:Lang, FrmOUTBILL_DEdit_OutBillUser %>"></asp:Label>：<%--出库人--%>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtIOUTASNLINE" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="格式：最多10位整数，最多4位小数" Enabled="false" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="txtCOUTPERSONCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>                  
                    <tr id="trLineInfo" runat="server">
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Lang, FrmOUTBILL_DEdit_XianBie %>"></asp:Label>：<%--线别--%>
                        </td>
                        <td style="width: 30%">
                            <asp:DropDownList ID="ddl_Line_ID" runat="server" Width="95%" AutoPostBack="true" OnSelectedIndexChanged="ddl_Line_ID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="Label10" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCPOSITIONCODE0" runat="server" Text="<%$ Resources:Lang, FrmOUTBILL_DEdit_ZhanDain %>"></asp:Label>：<%--站点--%>
                        </td>
                        <td style="width: 30%">
                            <asp:DropDownList ID="ddl_Pallet_Code" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Remark %>"></asp:Label>：
                        </td>
                        <td colspan="3" style="width: 30%">
                            <asp:TextBox ID="txtCMEMO" runat="server" Width="95%" MaxLength="20" Rows="2" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="4">
                            <asp:HiddenField ID="HidField_Enable" runat="server" Value="0" />
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
            <td style="text-align: center; padding: 15px 0px;">
                <asp:HiddenField ID="hfOutAsn_id" runat="server" />
                <asp:HiddenField ID="hfOriginalQty" runat="server" Value="0" />
                <asp:HiddenField ID="hfGetQty" runat="server" Value="0" />
                <asp:HiddenField ID="hfWorkType" runat="server" Value="0" />
                <asp:HiddenField ID="hfAssitIds" runat="server" Value="0" />
                <asp:Button ID="btnGetLikeCargoSpace" runat="server" CssClass="ButtonSearch" Text="线边仓" OnClick="btnGetLikeCargoSpace_Click" Visible="False"></asp:Button>
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClientClick="clearCINVNAME()" OnClick="btnSave_Click" Text="<%$ Resources:Lang, WMS_Common_Button_Save %>" />
                &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
            </td>
        </tr>
    </table>
    <table id="TabMain0" style="width: 100%" runat="server" visible="False">
        <tr valign="top">
            <td valign="top" align="left" style="padding-bottom: 15px;">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd4" Text="<%$ Resources:Lang,ShowCartonSN_Div_BoxCode %>" OnClick="btnNew_Click" Enabled="false"></asp:Button><%--箱号--%>
                <asp:Button ID="btnDeleteSn" runat="server" CssClass="ButtonDel" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" OnClick="btnDeleteSn_Click"  OnClientClick="return CheckDel();" />
                <asp:Button ID="btnSave_SN" runat="server" CssClass="ButtonSave4" OnClick="btnSave_SN_Click" Text="<%$ Resources:Lang,FrmOUTBILL_DEdit_SaveSN %>" Enabled="false" /><%--保存SN--%>
            </td>
            <td align="left" style="width: 5%">
                <asp:Button ID="btnSerch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">

                    <asp:GridView ID="grdSNDetial" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        CssClass="Grid" PageSize="15" DataKeyNames="id" OnRowDataBound="grdSNDetial_RowDataBound">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:TemplateField HeaderText="<input id='chkSelect' type='checkbox' onclick='SelectAll4Grid(this)' />">
                                <ControlStyle BorderWidth="0px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" BorderStyle="Solid"
                                        BorderWidth="0px" />
                                    <asp:HiddenField ID="hdnSelected" Value="1" runat="server" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkSelect0" runat="server" BorderStyle="Solid" BorderWidth="0px" />
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" DataFormatString="" HeaderText="" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Wrap="False"/>
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang,WMS_Common_Element_LeiXing %>"><%--类型--%>
                                <ItemTemplate>
                                    <asp:Label ID="labtype" runat="server" Text='<%# Eval("SNTYPE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False"  Width="5%" />
                                <ItemStyle HorizontalAlign="center" Wrap="False"  Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang,WMS_Common_Element_SNOrBarCode %>"><%--SN/箱号/栈板号--%>
                                <ItemTemplate>
                                    <asp:TextBox ID="lblSN" runat="server" Width="95%" Text='<%# Eval("SN_CODE") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="32%" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="32%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang,WMS_Common_Element_PalletCode %>"><%--栈板号--%>
                                <ItemTemplate>
                                    <asp:Label ID="labpalletcode" runat="server" Text='<%# Eval("palletcode") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="6%" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="6%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang,WMS_Common_Element_Furnaceno %>"><%--材料炉号--%>
                                <ItemTemplate>
                                    <asp:Label ID="labFurnaceno" runat="server" Text='<%# Eval("Furnaceno") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="6%" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="6%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang,WMS_Common_Element_MarkDate %>"><%--生产日期--%>
                                <ItemTemplate>
                                    <asp:TextBox ID="labDATECODE" Width="95%" runat="server" onfocus="this.blur();" Text='<%# Eval("datecode") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="6%" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="6%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang,WMS_Common_Element_ZhiDingBill %>"><%--指定单据--%>
                                <ItemTemplate>
                                    <asp:TextBox ID="labcso" runat="server" onfocus="this.blur();" Text='<%# Eval("cso") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="8%" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang,WMS_Common_Element_ZhiDingSupplier %>"><%--指定供应商--%>
                                <ItemTemplate>
                                    <asp:TextBox ID="labVendorCode" runat="server" onfocus="this.blur();" Text='<%# Eval("vendorcode") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="6%" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="6%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang,WMS_Common_Element_StockAllQty %>"><%--库存总量--%>
                                <ItemTemplate>
                                    <asp:TextBox ID="lblAllQty" runat="server" Width="95%" onfocus="this.blur();" Text='<%# Eval("ALLQTY") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="8%" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, WMS_Common_Element_Quantity %>">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtquantity" runat="server" Width="95%" Text='<%# Eval("quantity") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="8%" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, WMS_Common_Element_ChaoFaShuLiang %>" Visible="false"><%--超发数量--%>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLineQty" runat="server" Width="95%" Text='<%# Eval("LINE_QTY") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="10%" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="10%" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <ul class="OneRowStyle">
                        <li>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                FirstPageText="<%$ Resources:Lang,WMS_Common_Pager_First %>" LastPageText="<%$ Resources:Lang,WMS_Common_Pager_Last %>" NextPageText="<%$ Resources:Lang,WMS_Common_Pager_Next %>" PrevPageText="<%$ Resources:Lang,WMS_Common_Pager_Front %>" ShowPageIndexBox="Never"
                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                            </webdiyer:AspNetPager>
                        </li>
                        <li>
                            <div><%= Resources.Lang.WMS_Common_Pager_Total %> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.WMS_Common_Pager_Count %></div>
                        </li>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
    <input type="hidden" id="hiddWorkType" runat="server" />
    <input type="hidden" id="hiddUseErpCode" runat="server" />
    <input type="hidden" id="hiddCSO" runat="server" />
    <input type="hidden" id="hiddVendorCode" runat="server" />
    <input type="hidden" id="hiddOutbillStatus" runat="server" />
    <input type="hidden" id="hiddBillNo" runat="server" />
    <input type="hidden" id="hiddUseVendor" runat="server" />
    <input type="hidden" id="hiddIsTemporary" runat="server" />
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
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
    <script type="text/javascript">

        function SetHinddenValue(id1, id2) {
            document.getElementById(id1).value = document.getElementById(id2).value;
        }

        function SetHinddenValue2(id2, chid) {
            if (document.getElementById(chid).checked) {
                document.getElementById(id2).value = '1';
            } else {
                document.getElementById(id2).value = '2';
            }
        }

        function ShowNew(divID, sn, qty, allqty, typeid, lineqty, DateCode, cso, vendor, rowIndex) {
            var totalQty = parseFloat($("#<%=txtIQUANTITY.ClientID %>").val());
            var SelectQty = 0;
            $("#<%=grdSNDetial.ClientID %>").find("tr").each(function (i) {
                if (i > 0) {
                    if ((i - 1) != parseInt(rowIndex)) {
                        var obj = $(this).find("input").last();
                        if (obj.val().length > 0) {
                            SelectQty += parseFloat(obj.val().trim());
                        }
                    }
                }
            });
            var needqty = totalQty > SelectQty ? (totalQty - SelectQty).toFixed(2) : 0;
            disponse_div(event, document.all(divID));
            document.getElementById("compareIframe").src = "sessionset.aspx?SN=" + sn + "&QTY=" + qty + "&LINEQTY=" + lineqty + "&iType=1&ALLQTY=" + allqty + "&TypeID=" + typeid + "&DateCode=" + DateCode + "&CSO=" + cso + "&Vendor=" + vendor + "&NeedQty=" + needqty;

            var qtyread = document.getElementById(qty);
            //qtyread.readOnly = true;

        }
        //设置文本框数量
        function SetQtyReadOnly(qty) {
            var qtyread = document.getElementById(qty);
            //qtyread.readOnly = true;
        }
        //设置文本框数量
        function SetQtyRead(qty, lineqty, allqty) {
            var qtyvalue = document.all(qty).value;
            var lineqtyvalue = document.all(lineqty).value;
            var allqtyvalue = document.all(allqty).value;
            var numqty = Number(qtyvalue);
            var numlineqty = Number(lineqtyvalue);
            var numallqty = Number(allqtyvalue);
            //校验超发数量
            if (lineqtyvalue != "") {
                if (numqty != numallqty - numlineqty) {
                    alert("<%= Resources.Lang.FrmOUTBILL_DEdit_QuantityModify %>");//数量不能修改！
                }
                document.all(qty).value = (numallqty - numlineqty).toString();
            }
            else {
                if (numallqty != numqty) {
                    alert("<%= Resources.Lang.FrmOUTBILL_DEdit_QuantityModify %>");//数量不能修改！
                }
                document.all(qty).value = document.all(allqty).value;
            }
            var qtyread = document.getElementById(qty);
            //qtyread.readOnly = true;
        }

        //设置文本框数量
        function SetQtyValue(qty, lineqty, allqty) {
            var qtyvalue = document.all(qty).value;
            var lineqtyvalue = document.all(lineqty).value;
            var allqtyvalue = document.all(allqty).value;
            //校验超发数量
            if (lineqtyvalue != "") {
                if (isNaN(lineqtyvalue)) {
                    alert("<%= Resources.Lang.FrmOUTBILL_DEdit_Tips_ChaoFaLiangError %>");//超发数量必须为数值型数据！
                    document.all(lineqty).value = "";
                    document.all(qty).value = document.all(allqty).value;
                    return;
                }
                if (lineqtyvalue < 0) {
                    alert("<%= Resources.Lang.FrmOUTBILL_DEdit_Tips_ChaoFaLiangLing %>");//超发数量不能小于0 ！
                    document.all(lineqty).value = "";
                    document.all(qty).value = document.all(allqty).value;
                    return;
                }
                if (Number(lineqtyvalue) - parseInt(lineqtyvalue) > 0) {
                    alert("<%= Resources.Lang.FrmOUTBILL_DEdit_Tips_ChaoFaLiangGeShi %>");//超发数量必须为整数 ！
                    document.all(lineqty).value = "";
                    document.all(qty).value = document.all(allqty).value;
                    return;
                }
                var numqty = Number(qtyvalue);
                var numlineqty = Number(lineqtyvalue);
                var numallqty = Number(allqtyvalue);
                if (numlineqty >= numallqty) {
                    alert("<%= Resources.Lang.FrmOUTBILL_DEdit_Tips_ChaoFaLiangSN %>");//超发数量不能大于等于SN/批号数量 ！
                    document.all(lineqty).value = "";
                    document.all(qty).value = document.all(allqty).value;
                    return;
                }
                document.all(qty).value = (numallqty - numlineqty).toString();
            }
            else {
                document.all(qty).value = document.all(allqty).value;
            }
        }

        //设置文本框数量
        function SetAllQtyValue(qty, lineqty, allqty, strsn) {
            var qtyvalue = document.all(qty).value;
            var lineqtyvalue = document.all(lineqty).value;
            var allqtyvalue = document.all(allqty).value;
            var strcode = document.all(strsn).value;
            // alert(strcode);
            if (strcode != "") {
                //  alert("112");
                var strhead = strcode.substr(0, 1);
                var vlength = strcode.length;
                //alert(vlength);
                if ((strhead == "P" || strhead == "C") && vlength < 19) {
                    alert("<%= Resources.Lang.FrmOUTBILL_DEdit_Tips_BuNengGai %>");//栈板箱不能修改总量！
                    if (lineqtyvalue == "") {
                        document.all(allqty).value = document.all(qty).value;
                    } else {
                        document.all(allqty).value = (Number(qtyvalue) + Number(lineqtyvalue));
                    }
                    return;
                }
            }
            //校验总量
            if (allqtyvalue != "") {
                if (isNaN(allqtyvalue)) {
                    alert("<%= Resources.Lang.FrmOUTBILL_DEdit_Tips_ZongLiangGeShi %>");//总量必须为数值型数据！
                    document.all(lineqty).value = "";
                    document.all(allqty).value = document.all(qty).value;
                    return;
                }
                if (allqtyvalue < 0) {
                    alert("<%= Resources.Lang.FrmOUTBILL_DEdit_Tips_ZongLiangLing %>");//总量不能小于0 ！
                    document.all(lineqty).value = "";
                    document.all(allqty).value = document.all(qty).value;
                    return;
                }
                //
                //WL 20160525
                if (Number(allqtyvalue) - parseInt(allqtyvalue) > 0) {
                    alert("<%= Resources.Lang.FrmOUTBILL_DEdit_Tips_ZongLiangZhengShu %>");//总量必须为整数 ！
                    document.all(lineqty).value = "";
                    document.all(allqty).value = document.all(qty).value;
                    return;
                }

                //超发数量为空
                if (lineqtyvalue != "") {
                    if (Number(allqtyvalue) <= Number(lineqtyvalue)) {
                        alert("<%= Resources.Lang.FrmOUTBILL_DEdit_Tips_ZongLiangChaoFa %>");//总量必须大于超发数量 ！
                        document.all(allqty).value = (Number(qtyvalue) + Number(lineqtyvalue));
                        return;
                    }
                }


                //超发数量为空
                if (isNaN(lineqtyvalue)) {
                    document.all(qty).value = document.all(allqty).value;
                }
                else {
                    //超发不为空
                    var numlineqty = Number(lineqtyvalue);
                    var numallqty = Number(allqtyvalue);
                    document.all(qty).value = (numallqty - numlineqty).toString();
                }

            }
            else {
                document.all(allqty).value = document.all(qty).value;
            }
        }
      
    </script>
    <script type="text/javascript">
        function SetAsnPartValue(ControlName, Values, ControlName2, Values2, ControlName3, Values3, ControlName4, Values4, ControlName5, Values5) {
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
            document.all(ControlName3).value = Values3;
            document.all(ControlName4).value = Values4;
            document.all(ControlName5).value = Values5;
            var asnids = Values5;
            //alert(asnids);
            GetInfo(asnids, 'OUT');

            var cinvname = document.getElementById("<%=txtCINVNAME.ClientID %>");
            var verpline = document.getElementById("<%=txtCERPCODELINE.ClientID %>");
            //cinvname.readOnly = true;
            //verpline.readOnly = true;

            var hfget = document.getElementById("<%=hfGetQty.ClientID %>");
            hfget.value = Values4;
        }
        function GetInfo(Outasn_Ids, type) {
            var i = Math.random() * 10000 + 1;
            if (Outasn_Ids != "") {
                $.get("/APPS/RD/GetAsnInfo_IDS_AjAx.aspx?asn_d_ids=" + Outasn_Ids + "&type=" + type + "&i=" + i,
                    "",
                    function (data) {
                        var datas = data.split("|");

                        $("#<%=txtIOUTASNLINE.ClientID %>").val(datas[0]); //PO项次
                        $("#<%=txtCINVBARCODE.ClientID %>").val(datas[1]); //物料条码
                        $("#<%=txtCMEMO.ClientID %>").val(datas[2]); //备注

                    },
                    ""
                );
            }
        }

        function Show(divID) {
            document.getElementById("ctl00_ContentPlaceHolderMain_ShowBASE_CARGOSPACEDiv2_btnSearch").click();
            disponse_div(event, document.all(divID));
        }


        function setPreatNo() {
            alert("setPreatNo");
            $("#ctl00_ContentPlaceHolderMain_ShowBASE_CARGOSPACE_ByCinvcodeDiv1_txtPartNo").val($("#<%=txtCINVCODE.ClientID %>").val());
            //onclick="Show(&#39;ctl00_ContentPlaceHolderMain_ShowPARTDiv1_ajaxWebSearChComp&#39;);"
            //$("#<%=txtCINVCODE.ClientID %>").attr("onclick", "Show('ctl00_ContentPlaceHolderMain_ShowPARTDiv1_ajaxWebSearChComp')");
        }

        function CheckDel() {
            var number = 0;
            if (document.getElementById("<%=grdSNDetial.ClientID%>") != null) {
                var controls = document.getElementById("<%=grdSNDetial.ClientID%>").getElementsByTagName("input");
                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
            }
            if (number == 0) {
                alert("<%= Resources.Lang.FrmBar_SNManagement_DoTips %>");
            return false;
        }
    }



    </script>
    <script type="text/javascript">
        $(function () {
            function logCINVNAME(lable, value) {
                $("#<%= txtCINVNAME.ClientID %>").val(lable);
            }

            function logCPOSITION(lable, value) {
                $("#<%= txtCPOSITION.ClientID %>").val(lable);
            }
            //选择的料号
            var InCinvCode;

            $("#<%=this.txtCPOSITIONCODE.ClientID %>").autocomplete({
                source: function (request, response) {
                    //alert(request.term);
                    $.ajax({
                        url: "../Server/Cargospan.ashx?PositionCode=" + request.term + "&CinvCode=&Type=Out",
                        dataType: "xml",
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(XMLHttpRequest.status);
                            //alert(XMLHttpRequest.readyState);
                            //alert(errorThrown);
                            //alert(textStatus);
                        },
                        success: function (data) {
                            response($("reuslt", data).map(function () {

                                return {
                                    value: $("CPOSITIONCODE", this).text(),
                                    label: $("CPOSITIONCODE", this).text(),
                                    id: $("CPOSITION", this).text()
                                }
                            }));
                            //alert(data.xml);
                        }
                    });
                },
                autoFocus: true,
                minLength: 0,
                delay: 0,
                select: function (event, ui) {
                    logCPOSITION(
                        ui.item.id, ui.item.value);
                },
                open: function () {
                    $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                },
                close: function () {
                    $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                }
            });
        });
    </script>

</asp:Content>
