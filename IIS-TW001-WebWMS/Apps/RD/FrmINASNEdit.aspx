<%@ Page ValidateRequest="false" Language="C#" AutoEventWireup="true" CodeFile="FrmINASNEdit.aspx.cs"
    Inherits="RD_FrmINASNEdit" Title="--<%$ Resources:Lang, FrmINASNEdit_Title1 %>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%--入库通知单--%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../BASE/ShowVENDORDiv.ascx" TagName="ShowVENDORDiv" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>

    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
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
    <style type="text/css">
        .displaynone {
            display: none;
        }

        .btnSync {
            width: 118px !important;
        }

        span.requiredSign {
            color: #FF0000;
            font-weight: bold;
            position: relative;
            left: -15px;
            top: 2px;
        }

        .auto-style1 {
            width: 509px;
        }
            .auto-style1 input {
                margin-right:15px;
            }
        input[type='submit'][disabled], input[type='button'][disabled] {
            opacity: 0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70); /*兼容ie8及以下*/
        }
    </style>
    <link type="text/css" href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt;<asp:Literal ID="ltPageTable" runat="server" Text="<%$ Resources:Lang, FrmUnionPalletList_MSG2 %>"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:ShowVENDORDiv ID="ShowVENDORDiv1" runat="server" />
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>

    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    
                    <%-- Note by Qamar 2020-11-24 把部分元素class設displaynone並將要顯示的元素放到此處 --%>
                    <tr>
                        <td class="InputLabel" style="width: 13%; ">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, Common_CticketCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; ">
                            <asp:TextBox ID="txtInAsnCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Lab_ErpCode" runat="server" Text="*" ForeColor="Red" Visible="False"></asp:Label>
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmInbill_ErpCode %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmInbill_ReasonCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlREASONCODE" runat="server" Width="95%" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, Common_CreateUser %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, Common_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                Enabled="false" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtCSTATUS" runat="server" Width="95%" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>


                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%; ">
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, FrmInbill_InBillTYPE %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; ">
                            <asp:DropDownList ID="txtITYPE" runat="server" Width="95%" AutoPostBack="True" OnSelectedIndexChanged="txtITYPE_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%; ">
                            <asp:Label ID="Lab_Po" runat="server" Text="*" ForeColor="Red" Visible="False"></asp:Label>
                            <asp:Label ID="lblCPO" runat="server" Text="<%$ Resources:Lang, FrmInbill_SourceTicketCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; ">
                            <asp:TextBox ID="txtCPO" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%; ">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG1 %>"></asp:Label>：<%--贸易代码：--%>
                        </td>
                        <td style="width: 20%; ">
                            <asp:TextBox ID="txtCDEFINE1" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%; ">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG2 %>"></asp:Label>：<%--币别：--%>
                        </td>
                        <td style="width: 20%; ">
                            <asp:TextBox ID="txtCDEFINE2" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%; ">
                            <asp:Label ID="Lab_Vender" runat="server" Text="*" ForeColor="Red" Visible="False"></asp:Label>
                            <asp:Label ID="lblCVENDER" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG24 %>"></asp:Label>：<%--供应商名称：--%>
                        </td>
                        <td style="width: 21%; ">
                            <asp:TextBox ID="txtCVENDER" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false" MaxLength="50" onclick="Show('<%= ShowVENDORDiv1.GetDivName %>');"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%; ">
                            <asp:Label ID="Lab_Vendercode" runat="server" Text="*" ForeColor="Red" Visible="False"></asp:Label>
                            <asp:Label ID="lblCVENDERCODE" runat="server" Text="<%$ Resources:Lang, CommonB_CVENDERCODE %>"></asp:Label>：<%--供应商编码：--%>
                        </td>
                        <td style="width: 21%; ">
                            <asp:TextBox ID="txtCVENDERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false" onclick="Show('<%= ShowVENDORDiv1.GetDivName %>');"
                                MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSONCODE" runat="server" Text="<%$ Resources:Lang, Common_UserOfApproval %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCAUDITPERSONCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, Common_DateOfApproval %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDAUDITDATE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="displaynone">
                        <td class="InputLabel" style="width: 13%">
                            <%=Resources.Lang.CommonB_lineReject %>：<%--判退：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlDDEFINE3" Enabled="false" runat="server" Width="95%">
                                <%--  <asp:ListItem Value="N">否</asp:ListItem>
                                <asp:ListItem Value="Y">是</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="ID：" Style="display: none"></asp:Label>
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmINASNEdit_Label6 %>"></asp:Label>

                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" Style="display: none"></asp:TextBox>
                            <asp:CheckBox ID="cboIsAll" runat="server" Enabled="false" />
                        </td>
                    </tr>
                    <tr class="displaynone">

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmInbill_WorkType %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList runat="server" ID="ddlWorkType" Width="95%">
                                <%-- <asp:ListItem Value="">全部</asp:ListItem>
                               <asp:ListItem Value="0">平仓</asp:ListItem>
                               <asp:ListItem Value="1">立库</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtddefine4" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"></asp:Label>：<%--备注：--%>
                        </td>
                        <td style="width: 20%" colspan="6">
                            <asp:TextBox ID="txtCMEMO" runat="server" TextMode="MultiLine" Width="98%" MaxLength="200"></asp:TextBox>
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
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, Common_Save %>" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnSync" runat="server" CssClass="ButtonRef btnSync" OnClick="btnSync_Click" Text="数据抛转" Visible="false" />
                            &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="TabMain0" style="height: 100%; width: 100%" runat="server" visible="false">
                    <tr valign="top">
                        <td valign="top" align="left" class="auto-style1" style="padding-bottom:15px;">
                            <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_AddBtn %>" OnClick="btnNew_Click"></asp:Button>
                            <asp:Button ID="btnDelete0" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_DelBtn %>" CssClass="ButtonDel"
                                OnClientClick="return CheckDel('ctl00_ContentPlaceHolderMain_grdINASN_D');" />
                            <asp:Button ID="btnImportExcel" runat="server" CssClass="ButtonExcel" Text="<%$ Resources:Lang, CommonB_ImportFile %>"><%--导入--%>
                            </asp:Button>
                            <asp:Button ID="btnCreateInBill" runat="server" CssClass="ButtonConfig" Text="<%$ Resources:Lang, FrmINASNEdit_btnCreateInBill %>" OnClick="btnCreateInBill_Click"></asp:Button><%--  生成入库单--%>
                            <asp:Button ID="btnCreateTemporary" runat="server" CssClass="ButtonConfig" Text="<%$ Resources:Lang, FrmINASNEdit_btnCreateTemporary %>" OnClick="btnCreateTemporary_Click" Visible="false" /><%--生成暂存单--%>
                            <asp:Button ID="ButtonUnion" runat="server" Text="<%$ Resources:Lang, FrmINASNEdit_ButtonUnion %>" CssClass="ButtonAdd" OnClick="ButtonUnion_Click" /><%--拼板--%>
                            <asp:Button ID="btnPrint" runat="server" CssClass="ButtonPrint" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG12 %>" OnClick="btnPrint_Click"></asp:Button><%--打印--%>
                            <asp:Button ID="btnISplit" runat="server" CssClass="ButtonConfig5" Text="<%$ Resources:Lang, FrmINASNEdit_btnISplit %>" OnClick="btnISplit_Click" Width="108px" Visible="false"></asp:Button><%--作业方式拆解--%>
                        </td>

                        <%--NOTE by Mark, 09/21, visible false causes failing return to it--%>
                        <td align="right" style="width: 30%">
                            <%--查询料号--%>
                            <asp:Label visible="true" ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmINASNEdit_Label5 %>"></asp:Label>
                            <asp:TextBox  visible="true"   ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="25%"
                                MaxLength="50"></asp:TextBox>
                            <asp:Button  visible="true"  ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click"></asp:Button>
                       
                            </td>
                    </tr>

                    <tr>
                        <td colspan="4">
                            <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                                <asp:Panel ID="panel1" runat="server">
                                    <asp:GridView ID="grdINASN_D" runat="server" AllowPaging="True" BorderColor="Teal"
                                        DataKeyNames="IDS,InBill_Qty" BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                                        Width="100%" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"
                                        OnRowDataBound="grdINASN_D_RowDataBound" CssClass="Grid" PageSize="15">
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
                                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid" BorderWidth="0px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IDS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG18 %>" Visible="False"><%--子表编号--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG19 %>" Visible="False"><%--主表编号--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LineID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG20 %>"><%--项次--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="80px" />
                                            </asp:BoundField>

                                        <%--    <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>">
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="140px" />
                                            </asp:BoundField>--%>

                                            <%--NOTE by Mark, 09/19--%>
                                            <asp:BoundField DataField="PART" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>">
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="140px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RANK_FINAL" DataFormatString="" HeaderText="批/序號(RANK)">
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="140px" />
                                            </asp:BoundField>



                                            <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName %>">
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="260px" />
                                            </asp:BoundField>
                                              <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"><%--规格--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                </asp:BoundField>
                                            <asp:BoundField DataField="IQUANTITY" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG21 %>"><%--总数量--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="InBill_Qty" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang, FrmINASN_Report_MSG1 %>"><%--入库量--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="InBilled_Qty" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang, FrmINASN_Report_MSG2 %>"><%--扣帐量--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CERPCODELINE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG23 %>"><%--ERP项次--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CPO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINASN_Report_MSG6 %>"><%--po号--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IPOLINE" DataFormatString="" HeaderText="<%$ Resources:Lang, CommonB_IPOLINE %>"><%--PO项次--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cstatus" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Cstatus %>"><%--状态--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_EditBtn %>"
                                                DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_EditBtn %>" Text="<%$ Resources:Lang, Common_EditBtn %>">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:HyperLinkField>

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
                                            <div><%=Resources.Lang.Common_TotalPage %> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_TotalPage1 %></div>
                                        </li>
                                    </ul>
                                </asp:Panel>
                            </div>
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
</asp:Content>
