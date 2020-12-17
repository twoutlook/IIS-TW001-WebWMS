<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmOUTBILLEdit.aspx.cs" Inherits="OUT_FrmOUTBILLEdit" Title="--出库单" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../BASE/ShowBASE_CLIENTDiv.ascx" TagName="ShowBASE_CLIENTDiv" TagPrefix="uc1" %>
<%@ Register Src="ShowOutASN_Div.ascx" TagName="ShowOutASN_Div" TagPrefix="ucOA" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link type="text/css" rel="Stylesheet" href="../../Layout/CSS/LG/Page.css" id="cssUrl" runat="server" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/Calendar/calendar-blue.css" />
    <style type="text/css">
        .displaynone {
            display: none;
        }
        html {
            height: 100%;
            overflow-x: hidden;
        }

        body {
            height: 100%;
        }

        #aspnetForm {
            height: 100%;
        }

        .master_container {
            height: 100%;
        }

        .tableCell {
            display: table;
            width: 100%;
        }

        body {
            font: 76%/1.5 Arial,sans-serif;
            background: #FFF;
            color: #333;
        }

        div#container {
            width: 500px;
            margin: 0 auto;
        }

        h1 {
            color: #3CA3FF;
            margin: 1em 0 0;
            letter-spacing: -2px;
        }

        p {
            margin: 0 0 1.7em;
        }

        a {
            color: #F60;
            font-weight: bold;
        }

            a:hover {
                color: #F00;
            }

        span.requiredSign {
            color: #FF0000;
            font-weight: bold;
            position: relative;
            left: -15px;
            top: 2px;
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

        .btn_margin {
            margin-left: 15px;
        }

        input[type='submit'][disabled], input[type='button'][disabled] {
            opacity: 0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70); /*兼容ie8及以下*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt;<%= Resources.Lang.FrmOUTBILLList_Menu_PageName %> <%--出库管理-&gt;出库单--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:ShowBASE_CLIENTDiv ID="ShowBASE_CLIENTDiv1" runat="server" />
    <ucOA:ShowOutASN_Div ID="ucOutASN_Div" runat="server" />
    <table id="Table3" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top" class="tableCell">
            <td valign="top" colspan="3">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    
                    <%-- Note by Qamar 2020-11-24 把部分元素class設displaynone並將要顯示的元素放到此處 --%>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cticketcode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ErpCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCOUTASNID" runat="server" Text="<%$ Resources:Lang, FrmOUTBILLList_CticketCode %>"></asp:Label>：<%--出库通知单单号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCOUTASNID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateOwner %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" Enabled="false" CssClass="NormalInputText" Width="95%" ToolTip="格式：yyyy-MM-dd"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, WMS_Common_GridView_Status %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dplCSTATUS" runat="server" Width="95%" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_KouZhangRen %>"></asp:Label>： <%--扣帐人：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtdebituser" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_KouZhangShiJian %>"></asp:Label>：<%--扣帐时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtdebittime" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>


                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbOType" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_OutType %>"></asp:Label>： <%--出库类型：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="drOType" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="SO号："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSO" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="投料点："></asp:Label>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCDEFINE1" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCLIENTCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ClientCode %>"></asp:Label>：<%--客户编码--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCCLIENTCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCLIENT" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ClientName %>"></asp:Label>：<%--客户名称：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCLIENT" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblDINDATE" runat="server" Text="<%$ Resources:Lang, FrmOUTBILLList_OutDate %>"></asp:Label>：<%--出库日期--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDINDATE" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="格式：yyyy-MM-dd" MaxLength="19"></asp:TextBox>
                            <img border="0" runat="server" id="imgDINDATE" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif"
                                onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATE','y-mm-dd',0);" />
                            <asp:RequiredFieldValidator ID="rfvtxtDINDATE" runat="server" ControlToValidate="txtDINDATE"
                                ErrorMessage="请填写出库日期!" Display="None"> 
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSON" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_AuditUser %>"></asp:Label>：<%--审核人：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCAUDITPERSON" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="100" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITTIME" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_AuditDate %>"></asp:Label>：<%--审核日期：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDAUDITTIME" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="格式：yyyy-MM-dd" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_PaoZhuangShiJian %>"></asp:Label>：<%--抛转时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtpaozhuantime" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">&nbsp;
                        </td>
                        <td style="width: 20%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Remark %>"></asp:Label>：
                        </td>
                        <td colspan="5" style="width: 20%">
                            <asp:TextBox ID="txtCMEMO" runat="server" TextMode="MultiLine" Width="99%" MaxLength="200"
                                Rows="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="格式：最多10位整数，最多4位小数"></asp:TextBox>
                        </td>
                        <td>
                            <asp:HiddenField ID="hiddenGuid" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="HiddenSpecial" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td align="center" style="padding: 15px 0px;" colspan="3">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, WMS_Common_Button_Save %>" />
                <asp:Button ID="btnCreateOutBill" runat="server" CssClass="ButtonSearch btn_margin" Visible="false" Text="<%$ Resources:Lang, FrmOUTBILLEdit_Button_ShengCheng %>" OnClick="btnCreateOutBill_Click"></asp:Button>
                
                <%--交付扣帳--%>
                <asp:Button ID="btnDeliverAndUpdateStockCurrent" runat="server" CssClass="ButtonConfig btn_margin" Text="<%$ Resources:Lang, FrmOUTBILLEdit_Button_JiaoFuKouZhang %>" OnClick="btnDeliverAndUpdateStockCurrent_Click"></asp:Button>
                
                <asp:Button ID="btnASRS" runat="server" CssClass="ButtonConfig btn_margin" OnClick="btnASRS_Click" Text="<%$ Resources:Lang, FrmOUTBILLEdit_Button_ASRSGuoZhang %>" /><%--AS/RS过账--%>
               
             <%--   NOTE by Mark, 09/22 
                小仲: 扣账之后再出现返库的按钮
                Edward: 有需要返库的才出现
                 *** 必需要做 權限平台 才能顯示BTN--%>
             
                <asp:Button ID="btn0922" runat="server" CssClass="ButtonConfig btn_margin" OnClick="btn0922_Click" Text="返库" /><%--返库--%>
              
              
                
                <asp:Button ID="btnDelete" runat="server" Visible="false" CssClass="ButtonDel btn_margin" OnClick="btnDelete_Click" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" CausesValidation="false" />
                <div class="displaynone">
                <asp:Button ID="btnRefres" runat="server" CssClass="ButtonRef btn_margin" Text="<%$ Resources:Lang,WMS_Common_Button_Refresh %>" OnClick="btnRefres_Click"></asp:Button><%--刷新--%>
                </div>
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack btn_margin" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
                <asp:Button ID="btnPrint" runat="server" CssClass="ButtonPrint btn_margin" Text="<%$ Resources:Lang,WMS_Common_Button_Print %>" OnClick="btnPrint_Click"></asp:Button>
                <asp:Button ID="btnSeral" runat="server" CssClass="ButtonConfig5" OnClick="btnSeral_Click" Text="<%$ Resources:Lang,WMS_Common_Element_SerialNo %>" Visible="false"></asp:Button>
                <%--序列号--%>
            </td>
        </tr>
        <tr valign="top" class="tableCell">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server" DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
        <%--</table>
    <table id="TabMain0" style="height: 100%; width: 100%">--%>
        <tr valign="top" class="tableCell">
            <td valign="top" align="left" style="padding: 0px 0px 15px 0px;">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang,WMS_Common_Button_New %>" OnClick="btnNew_Click"></asp:Button>
                &nbsp;<asp:Button ID="btnDelete0" OnClick="btnDelete_Click" runat="server" OnClientClick="return CheckDel();" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" CssClass="ButtonDel" />
                &nbsp;<asp:Button ID="btnSetCARGOSPACE" runat="server" CssClass="ButtonConfig" Text="<%$ Resources:Lang,FrmOUTBILLEdit_Button_XiangTongChuWei %>" OnClick="btnSetCARGOSPACE_Click" Enabled="false" /><%--相同储位--%>
                &nbsp;<asp:Button ID="btnOutput" runat="server" CssClass="ButtonReg4" Text="<%$ Resources:Lang,FrmOUTBILLEdit_Button_ASRSChuKu %>" OnClick="btnOutput_Click" OnClientClick="return checkdata()" Enabled="false"></asp:Button><%--AS/RS出库--%>
                &nbsp;<asp:Button ID="btnCancle" runat="server" CssClass="ButtonCancel" Text="<%$ Resources:Lang, WMS_Common_Button_Cancel %>" OnClick="btnCancle_Click" OnClientClick="return  check()" Enabled="false"></asp:Button>
            </td>
            <td id="showMsgTd" style="width: 200px; color: Red; display: none">&nbsp;
            </td>

            <td align="right" style="display:none ">

                <asp:Label ID="Label8" CssClass="InputLabel" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_SearchCinvcode %>"></asp:Label>：
                <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="25%" MaxLength="50"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
            </td>
        </tr>
        <tr class="tableCell">
            <td align="left" colspan="5">
                <asp:Panel ID="Panel1" runat="server">
                    <div style="min-height: 300px; overflow-x: auto; width: 100%;" id="DivScroll">
                        <asp:GridView ID="grdOUTBILL_D" runat="server" AllowPaging="True" BorderColor="Teal"
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                            OnRowDataBound="grdOUTBILL_D_RowDataBound" CssClass="Grid" PageSize="15">
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
                                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid"
                                            BorderWidth="0px" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="IDS" DataFormatString="{0:0}" HeaderText="子表编号" Visible="False">
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="right" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="主表编号" Visible="False">
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="right" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="lineid" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_XiangCi %>"><%--项次--%>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="center" Wrap="False" Width="50px" />
                                </asp:BoundField>

                          

                                
                                        <%--NOTE by Mark, 09/19--%>
                                        <asp:BoundField Visible="false" DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PART" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RANK_FINAL" DataFormatString="" HeaderText="批/序號(RANK)">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                                        </asp:BoundField>






                                <asp:TemplateField HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>">
                                    <ItemTemplate>
                                        <div id="showDiv">
                                            <%#WmsDBCommon_ASRS.CutString(Eval("CINVNAME").ToString(),20)%>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"><%--规格--%>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="<%$ Resources:Lang, WMS_Common_Element_Quantity %>">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGv_IQUANTITY" runat="server" Text='<%# Eval("IQUANTITY") %>'
                                            Width="60px" MaxLength="8" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Lang, WMS_Common_Element_CpositionCode %>">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGv_CPOSITIONCODE" runat="server" class="POSITIONCODE" Text='<%# Eval("CPOSITIONCODE") %>' Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cposition %>">
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="<%$ Resources:Lang, WMS_Common_Element_ChaoFaShuLiang %>" Visible="False"><%--超发数量--%>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGv_LINE_QTY" runat="server" Text='<%# Eval("LINE_QTY") %>' Width="60px"
                                            MaxLength="8"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CERPCODELINE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ErpCode %>">
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                                </asp:BoundField>


                                <%--NOTE by Mark, 09/21, debug why 2 PARTs failed 出庫--%>
                                <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmOUTBILLEdit_Button_ASRSMingLing %>" ShowHeader="False"><%--ASRS命令--%>
                                    <ItemTemplate>
                                        <asp:Button ID="LinkASRS_STATUS" runat="server" CausesValidation="false" CommandArgument='<%#Eval("IDS") %>'
                                            CommandName="" OnClick="LinkASRS_STATUS_Click" Text='<%# Eval("ASRS_STATUS")%>'></asp:Button>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmOUTBILLEdit_Button_ASRSZhuangTai %>" ShowHeader="False"><%--ASRS状态--%>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkStatus" runat="server" CausesValidation="false" CommandArgument='<%#Eval("CPOSITIONCODE") %>'
                                            CommandName="" OnClick="LinkButton2_Click" Text='<%#Eval("ASRS_STATUS") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:Lang,WMS_Common_Button_Refresh %>" ShowHeader="False"><%--刷新--%>
                                    <ItemTemplate>
                                        <asp:Button ID="btnRefresh" runat="server" CausesValidation="false" CommandArgument='<%#Eval("IDS") %>'
                                            CommandName="" OnClick="btnRefresh_Click" Text="<%$ Resources:Lang,WMS_Common_Button_Refresh %>"></asp:Button><%--刷新--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="center" Wrap="False" ForeColor="Blue" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="<%$ Resources:Lang,WMS_Common_Menu_PalletCodeIn %>" ShowHeader="False" Visible="false"><%--栈板入库--%>
                                    <ItemTemplate>
                                        <asp:Button ID="LinkPALLET_STATUS_I" runat="server" CausesValidation="false" CommandArgument='<%#Eval("IDS") %>'
                                            CommandName="" OnClick="LinkPALLET_STATUS_I_Click" Text='<%# Eval("ASRS_STATUS")%>'></asp:Button>
                                        <asp:HiddenField ID="hdISGOBACK" runat="server" Value='<%# Eval("ISGOBACK")%>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="center" Wrap="False" ForeColor="Blue" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="<%$ Resources:Lang,FrmOUTBILLEdit_JiaoFuShu %>" Visible="false"><%--交付数量--%>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlToHande_Info" ToolTip="<%$ Resources:Lang,FrmOUTBILLEdit_ChaKanJiaoFu %>" runat="server" Text='<%# Eval("DeliveriesQTY")%>'></asp:HyperLink><%--查看交付信息详情--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CDEFINE1" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmOUTBILLEdit_JiaoFuChuWei %>"><%--交付储位--%>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IOUTASNLINE" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang,WMS_Common_Element_TongZhiDanXiangCi %>" Visible="False"><%--出库通知单项次--%>
                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="right" Wrap="False" />
                                </asp:BoundField>
                                <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" DataTextField=""
                                    DataTextFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" Text="<%$ Resources:Lang, WMS_Common_GridView_Edit %>">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                </asp:HyperLinkField>
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
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div id="showResultDiv" onmouseout="closeDisplay()" style="background: #DFF8FF; position: absolute; z-index: 10; display: block;">
        <select id="showResultSelect" name="showResultSelect" size="15" style="display: none; background: #DFF8FF;"
            onchange="setTextValue(this.value)">
        </select>
    </div>
    <input type="hidden" id="hiddWorkType" runat="server" />
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/Js/Help.js" type="text/javascript"></script>
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
        function show() {
            $("#showDiv").hide();
        }
        function hide() {
            $("#showDiv").show();
        }

        function check() {
            var number = 0;
            $.each($("#<%=grdOUTBILL_D.ClientID%>").find("span input"), function (i, item) {
                if (item.checked == true) {
                    number = number + 1;
                }
            });
            if (number == 0) {
                alert("<%= Resources.Lang.FrmOUTBILLEdit_Tips_XuanZeXiang %>"); //请至少选择一项！
                return false;
            }
            if (confirm("<%= Resources.Lang.FrmOUTBILLEdit_Tips_CancelConfirm %>")) {//你确认取消吗？
                return true;
            }
            else {
                return false;
            }
        }

        function CheckDel() {
            var number = 0;
            $.each($("#<%=grdOUTBILL_D.ClientID%>").find("span input"), function (i, item) {
                if (item.checked == true) {
                    number = number + 1;
                }
            });
            if (number == 0) {
                alert("<%= Resources.Lang.WMS_Common_DeleteTips %>");
                return false;
            }
            if (confirm("<%= Resources.Lang.WMS_Common_DeleteConfirm %>")) {
                return true;
            }
            else {
                return false;
            }
        }

        function checkdata() {
            var number = 0;
            $.each($("#<%=grdOUTBILL_D.ClientID%>").find("span input"), function (i, item) {
                if (item.checked == true) {
                    number = number + 1;
                }
            });
            if (number == 0) {
                alert("<%= Resources.Lang.FrmOUTBILLEdit_Tips_ChuKuXuanZe %>");//请至少选择一项出库！
                return false;
            }
            if (confirm("<%= Resources.Lang.FrmOUTBILLEdit_Tips_ChuKuConfirm %>")) {//你确认出库吗？
                return true;
            }
            else {
                return false;
            }
        }


        //失去焦点提交修改，隐藏显示框
        function submitData(type, dataType, ids, qty, line_Qty, positionCode, txt) {
            var isOk = true;
            //非空验证
            if (type == "") {
                alert("<%= Resources.Lang.FrmOUTBILLEdit_Tips_NeedType %>");//类型不能为空！
                isOk = false;
            }
            else if (ids == "") {
                alert("<%= Resources.Lang.FrmOUTBILLEdit_Tips_NeedIDS %>");//IDS不能为空！
                isOk = false;
            }
            else if (dataType == "") {
                alert("<%= Resources.Lang.FrmOUTBILLEdit_Tips_NeedShuJuType %>");//数据类型不能为空！
                isOk = false;
            }
            if (isOk) {
                if (dataType == "Qty") {
                    if (qty == "") {
                        alert("<%= Resources.Lang.FrmOUTBILLEdit_Tips_NeedShuLiang %>");//数量不能为空！
                        isOk = false;
                    }
                    else if (isNaN(qty)) {
                        alert("<%= Resources.Lang.FrmOUTBILLEdit_Tips_ShuLiangError %>");//数量必须为数值型数据！
                        isOk = false;
                    }
                    else if (qty <= 0) {
                        alert("<%= Resources.Lang.FrmOUTBILLEdit_Tips_ShuLiangDaYuLing %>");//数量不能小于等于 0 ！
                        isOk = false;
                    }
                }
                else if (dataType == "Line_Qty") {
                    if (line_Qty != "") {
                        if (isNaN(line_Qty)) {
                            alert("<%= Resources.Lang.FrmOUTBILLEdit_Tips_ChaFaShuGeShi %>");//超发数必须为数值型数据！
                            isOk = false;
                        }
                    }
                }
                else if (dataType == "PositionCode") {
                    if (positionCode == "") {
                        isOk = false;
                    }
                }
                else {
                    alert("<%= Resources.Lang.FrmOUTBILLEdit_Tips_TypeError %>");//数据类型错误不能提交！
                    isOk = false;
                }

                if (isOk) {
                    var i = Math.random() * 10000 + 1;
                    $.get("../BASE/SubmitDate.aspx?i=" + i,
                        { Type: type, DataType: dataType, Ids: ids, Qty: qty, Line_Qty: line_Qty, PositionCode: positionCode },
                        function (data) {
                            $("#showMsgTd").html(data);
                        });
                }
            }
        }
    </script>
    <script type="text/javascript">
        function ChangeDivWidth() {
            document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
        }
        window.onresize = ChangeDivWidth;
        ChangeDivWidth();
    </script>
</asp:Content>
