<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmOUTASNEdit.aspx.cs" Inherits="OUT_FrmOUTASNEdit" Title="--出库通知单" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="ShowFG_CinvCode_Div.ascx" TagName="ShowFG_CinvCode_Div" TagPrefix="uc1" %>

<%@ Register Src="../BASE/ShowBASE_CLIENTDiv.ascx" TagName="ShowBASE_CLIENTDiv" TagPrefix="uc3" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link type="text/css" rel="Stylesheet" href="../../Layout/CSS/LG/Page.css" id="cssUrl" runat="server" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/Calendar/calendar-blue.css" />
    <style type="text/css">
        .displaynone {
            display: none;
        }
        html {
            height: 100%;
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

        span.requiredSign {
            color: #FF0000;
            font-weight: bold;
            position: relative;
            left: -15px;
            top: 2px;
        }

        .gridLineHeight {
            line-height: 22px;
        }

        input[type='submit'][disabled], input[type='button'][disabled] {
            opacity: 0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70); /*兼容ie8及以下*/
        }

        .btnContainer input:not(:first-child) {
            margin-left: 12px;
        }
    </style>
    <style type="text/css">
        .btnSync {
            width: 118px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt;<asp:Literal ID="ltPageTable" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_Menu_PageName %>"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:ShowFG_CinvCode_Div ID="ShowFG_CinvCode_Div1" runat="server" />
    <uc3:ShowBASE_CLIENTDiv ID="ShowBASE_CLIENTDiv1" runat="server" />

    <table id="Table3" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    
                    <%-- Note by Qamar 2020-11-24 把部分元素class設displaynone並將要顯示的元素放到此處 --%>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cticketcode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtOutAsnCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ErpCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmOUTASNEdit_LiYouMa %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="ddlReason" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateOwner %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false" MaxLength="50" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtCCREATEOWNERCODE" runat="server" ControlToValidate="txtCCREATEOWNERCODE"
                                ErrorMessage="请填写制单人!" Display="None"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, WMS_Common_GridView_Status %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="drCSTATUS" runat="server" Width="95%" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    

                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_OutType %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtITYPE" runat="server" Width="95%" AutoPostBack="True" OnTextChanged="txtITYPE_TextChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSO" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_SourceCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCSO" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCLIENTCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ClientCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCLIENTCODE" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCLIENT" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ClientName %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCLIENT" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSONCODE" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_AuditUser %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCAUDITPERSONCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITDATE" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_AuditDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDAUDITDATE" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="格式:yyyy-MM-dd" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmOUTASNEdit_TouLiaoDian %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCDEFINE1" runat="server" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Lang, FrmOUTASNEdit_WanGongLiaoHao %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txt_FG_CinvCode" runat="server" Width="95%" Enabled="False"></asp:TextBox>
                            <asp:Literal ID="ltSearch" runat="server"></asp:Literal>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Lang, FrmOUTASNEdit_WanGongShuLiang %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txt_FG_Qty" runat="server" Width="95%" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmOUTASNEdit_ZhengPiFenPi %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="ddl_Is_Whole" runat="server" Width="95%" Enabled="False">
                                <%--<asp:ListItem Value="0" Selected="True">分批</asp:ListItem>
                                <asp:ListItem Value="1">整批</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <%= Resources.Lang.WMS_Common_Element_WorkType %>:
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="drpWorkType" runat="server" Width="95%">
                                <%-- <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="0">平库</asp:ListItem>
                                <asp:ListItem Value="1">立库</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmOUTASNEdit_ErpCode %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="drpBillNo" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Remark %>"></asp:Label>：
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtCMEMO" runat="server" TextMode="MultiLine" Width="99%" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="6">
                            <asp:Label ID="lblID" runat="server" Text="ID:"></asp:Label>
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"
                                Enabled="False"></asp:TextBox>
                            <asp:HiddenField ID="hfInAsn_Id" runat="server" />
                            <asp:HiddenField ID="Hf" runat="server" />
                            <asp:HiddenField ID="hfIsConfirm" runat="server" Value="0" />
                            <!--是否提示-->
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="text-align: center; padding: 15px 0px;">
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, WMS_Common_Button_Save %>" />
                            &nbsp;&nbsp;<asp:Button ID="btnInBom" runat="server" CssClass="ButtonConfig" Text="BOM" OnClick="btnInBom_Click" Visible="False" Style="display: none;"></asp:Button>
                            <asp:Button ID="btnSync" runat="server" CssClass="ButtonRef btnSync" OnClick="btnSync_Click" Text="<%$ Resources:Lang,FrmOUTASNEdit_Button_Sync %>" Visible="false" />
                            <%--数据抛转--%>
                            &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />

                        </td>
                    </tr>
                </table>
                <table id="TabMain0" style="height: 100%; width: 100%" runat="server" visible="false">
                    <tr valign="top">
                        <td valign="top" align="left" style="padding: 0px 0px 15px 0px;" class="btnContainer">
                            <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang,WMS_Common_Button_New %>" OnClick="btnNew_Click"></asp:Button>
                            <asp:Button ID="btnImportExcel" runat="server" CssClass="ButtonExcel" Text="<%$ Resources:Lang,FrmOUTASNEdit_Button_DaoRu %>"></asp:Button>
                            <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" OnClientClick="return CheckDel();" CssClass="ButtonDel" />
                            <asp:Button ID="btnCreateOutBill" runat="server" CssClass="ButtonConfig" Text="<%$ Resources:Lang,FrmOUTASNEdit_Button_ShengChengChuKu %>" OnClick="btnCreateOutBill_Click"></asp:Button>
                            <asp:Button ID="btnCreateTemporary" runat="server" CssClass="ButtonConfig" Text="<%$ Resources:Lang,FrmOUTASNEdit_Button_CreateTemporary %>" OnClick="btnCreateTemporary_Click" /><%--生成暂存单--%>
                            <asp:Button ID="btnPrint" runat="server" CssClass="ButtonPrint" Text="<%$ Resources:Lang,WMS_Common_Button_Print %>" OnClick="btnPrint_Click"></asp:Button>
                            <asp:Button ID="btnISplit" runat="server" CssClass="ButtonConfig5" Text="<%$ Resources:Lang,FrmOUTASNEdit_Button_ZuoYeChaiJie %>" OnClick="btnISplit_Click"></asp:Button>
                            <%--<asp:Button ID="btnDisassembly" runat="server" CssClass="ButtonConfig" Text="通知单拆解" OnClick="btnDisassembly_Click" Visible="False"></asp:Button>--%>
                        </td>

                        <td  style="display: none" align="right" >
                            <asp:Label ID="Label8" CssClass="InputLabel" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_SearchCinvcode %>"></asp:Label>：
                            <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="25%" MaxLength="50"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="min-height: 300px; overflow-x: auto; width: 100%" id="DivScroll">
                                <asp:GridView ID="grdOUTASN_D" runat="server" AllowPaging="True" BorderColor="Teal"
                                    DataKeyNames="IDS,CINVCODE" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ShowHeaderWhenEmpty="true"
                                    Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdOUTASN_D_RowDataBound"
                                    CssClass="Grid gridLineHeight" PageSize="15" OnDataBinding="grdOUTASN_D_DataBinding">
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
                                        <asp:BoundField DataField="LineId" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_XiangCi %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="120px" />
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



                                        <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="260px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"><%--规格--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IQUANTITY" DataFormatString="{0:F}" HeaderText="<%$ Resources:Lang, FrmOUTASNEdit_Column_ZongShuLiang %>"><%--总数量--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OutBill_Qty" DataFormatString="{0:F}" HeaderText="<%$ Resources:Lang, FrmOUTASNEdit_Column_ChuKuLiang %>"><%--出库量--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OutBilled_Qty" DataFormatString="{0:F}" HeaderText="<%$ Resources:Lang, FrmOUTASNEdit_Column_KouZhangLiang %>"><%--扣帐量--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CSO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNList_SourceCode %>"><%--来源单号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ISOLINE" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmOUTASNEdit_Column_LaiYuanDanXiangCi %>" Visible="false"><%--来源单号项次--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CERPCODELINE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNEdit_Column_ERPXiangCi %>"> <%--ERP单号项次--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cstatus" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Status %>"> <%--状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandArgument='<%#Eval("IDS") %>'
                                                    CommandName="" OnClick="LinkButton1_Click" Text="<%$ Resources:Lang, WMS_Common_GridView_Edit %>"></asp:LinkButton><%--编辑--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
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
            </td>
        </tr>
        <tr class="tableCell" valign="top">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server"
                    DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>

    <script type="text/javascript">
        function SetControlValue(ControlName, Values, ControlName1, Values1) {
            document.all(ControlName).value = Values;
            if (ControlName1 != null) {
                document.all(ControlName1).value = Values1;
            }
        }

        function Show(divID) {
            disponse_div(event, document.all(divID));
        }
    </script>
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
        function CheckDel() {
            var number = 0;
            $.each($("#<%=grdOUTASN_D.ClientID%>").find("span input"), function (i, item) {
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

        function del() {
            if (confirm("<%= Resources.Lang.FrmOUTASNEdit_Tips_QueRengDaoRu %>")) {
                true;
            } else { false; }
        }
    </script>
</asp:Content>
