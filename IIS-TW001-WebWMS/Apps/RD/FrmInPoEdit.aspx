<%@ Page ValidateRequest="false" Language="C#" AutoEventWireup="true" CodeFile="FrmInPoEdit.aspx.cs"  Inherits="RD_FrmInPoEdit" Title="--采购单" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Src="../BASE/ShowVENDORDiv.ascx" TagName="ShowVENDORDiv" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js"
        type="text/javascript"></script>
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="Link1"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script src="../../Layout/Js/Help.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SetControlValue(ControlName, Values, ControlName2, Values2) {
            //alert(ControlName + ":" + ControlName2);
            //alert(ControlName2);
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
        }

        function Show(divID) {
            //alert(divID);
            disponse_div(event, document.all(divID));
        }

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
     <%=Resources.Lang.Common_InbillMangement%><%--入库管理--%>-&gt<%=Resources.Lang.FrmInPoEdit_Content1%><%--采购单--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:ShowVENDORDiv ID="ShowVENDORDiv1" runat="server" />
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
                                    left: -15px;
                                    top: 2px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="PO："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPONO" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, FrmInPoEdit_lblITYPE %>"></asp:Label>：<%--PO类型：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dpdPOType" runat="server" Width="95%" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmInPoEdit_lblCERPCODE %>"></asp:Label>：<%--PO日期：--%>
                        </td>

                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtPODate" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtPODate','y-mm-dd',0);" />
                        </td>

                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCVENDERCODE" runat="server" Text="<%$ Resources:Lang, CommonB_CVENDERCODE %>"></asp:Label>：<%--供应商编码：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCVENDERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false" onclick="Show('<%= ShowVENDORDiv1.GetDivName %>');"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblCVENDER" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG24 %>"></asp:Label>：<%--供应商名称：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCVENDERName" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false" MaxLength="50" onclick="Show('<%= ShowVENDORDiv1.GetDivName %>');"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtCSTATUS" runat="server" Width="95%" disabled="disabled">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCPO" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG2 %>"></asp:Label>：<%--币别：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCURRENCY" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmInPoEdit_Label1 %>"></asp:Label>：<%--付款条件：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPAYMENTTERM" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmInPoEdit_Label2 %>"></asp:Label>：<%--出货来源：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSHIPFROM" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmInPoEdit_Label4 %>"></asp:Label>：<%--出货地：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSHIPTO" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, Common_CreateUser %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCREATEOWNER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, Common_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSONCODE" runat="server" Text="<%$ Resources:Lang, FrmInPoEdit_lblCAUDITPERSONCODE %>"></asp:Label>：<%--单据来源--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dpdSOURCE" runat="server" Width="95%" disabled="disabled">
                          <%--      <asp:ListItem Value="0">WMS</asp:ListItem>
                                <asp:ListItem Value="1">ERP</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmInPoEdit_Label3 %>"></asp:Label>：<%--最后修改人：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLASTUPDATEOWNER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, FrmInPoEdit_lblDCREATETIME %>"></asp:Label>：<%--最后修改日期：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLASTUPDATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" Style="display: none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server"  Text="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"></asp:Label>：<%--备注：--%>
                        </td>
                        <td style="width: 20%" colspan="6">
                            <asp:TextBox ID="txtMEMO" runat="server" TextMode="MultiLine" Width="98%" MaxLength="200"></asp:TextBox>
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
                            <asp:HiddenField ID="hfIsConfirm" runat="server" Value="0" />
                            <!--是否提示-->
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="text-align:center;">
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                                Text="<%$ Resources:Lang, Common_Save %>" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnPrint" runat="server" CssClass="ButtonPrint" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG12 %>" OnClick="btnPrint_Click"></asp:Button><%--打印--%>
                            &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>"
                                CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="TabMain0" style="height: 100%; width: 100%" runat="server" visible="false">
                    <tr valign="top">
                        <td valign="top" align="left" style="padding:15px 0px;">
                            <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_AddBtn %>"></asp:Button>
                            &nbsp;
                            <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_DelBtn %>" CssClass="ButtonDel"
                                OnClientClick="return CheckDel('ctl00_ContentPlaceHolderMain_grdINPO_D');" />
                        </td>
                        <td align="right" style="width: 50%;padding:15px 0px;">
                            <asp:Label ID="Label5" CssClass="InputLabel" runat="server" Text="<%$ Resources:Lang, FrmINASNEdit_Label5 %>"></asp:Label>：<%--查询料号：--%>
                            <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="25%" MaxLength="50"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                                <asp:Panel ID="panel1" runat="server">
                                    <asp:GridView ID="grdINPO_D" runat="server" AllowPaging="True" BorderColor="Teal"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                                        Width="100%" AutoGenerateColumns="False" 
                                        OnRowDataBound="grdINPO_D_RowDataBound" CssClass="Grid" PageSize="15">
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
                                            <asp:BoundField DataField="IDS" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG18 %>" Visible="False"><%--子表编号--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG19 %>" Visible="False"><%--主表编号--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="POLINE" DataFormatString="" HeaderText="<%$ Resources:Lang, CommonB_IPOLINE %>"><%--PO项次--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>">
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="calias" DataFormatString=""  HeaderText="<%$ Resources:Lang, CommonB_calias %>"> <%--助记码--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName %>">
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IQUANTITY" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, Common_NUM %>">
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UNIT" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmInPoEdit_UNIT %>"><%--单位--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PRICE" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmInPoEdit_PRICE %>"><%--单价--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="TOTAL" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmInPoEdit_TOTAL %>"><%--小计--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                                            </asp:BoundField>

                                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_EditBtn %>" DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_EditBtn %>" Text="<%$ Resources:Lang, Common_EditBtn %>">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:HyperLinkField>

                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmInPoEdit_hySN %>" ItemStyle-HorizontalAlign="Center"><%--SN条码维护--%>
                                               <ControlStyle BorderWidth="0px" />
                                               <ItemTemplate>
                                                  <asp:HyperLink ID="hySN" runat="server" NavigateUrl="#" ><%=Resources.Lang.FrmInPoEdit_hySN %><%--SN条码维护--%></asp:HyperLink>                                    
				                             </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    <ul class="OneRowStyle">
                                        <li>
                                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                                FirstPageText="<%$ Resources:Lang, Common_FirstPage%>" LastPageText="<%$ Resources:Lang, Common_LastPage%>" NextPageText="<%$ Resources:Lang, Common_NextPage%>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                            </webdiyer:AspNetPager>
                                        </li>
                                        <li>
                                            <div><%=Resources.Lang.Common_TotalPage %> <%=AspNetPager1.RecordCount%> <%=Resources.Lang.Common_TotalPage1 %></div>
                                        </li>
                                    </ul>
                                </asp:Panel>
                            </div>

                        </td>
                    </tr>
                    <script type="text/javascript">
                        function ChangeDivWidth() {
                            document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
                        }
                        window.onresize = ChangeDivWidth;
                        ChangeDivWidth();
                    </script>
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
    <input type="hidden" id="hiddOperation" runat="server" />
</asp:Content>
