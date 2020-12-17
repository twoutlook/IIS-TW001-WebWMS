<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--<%$ Resources:Lang, Common_InbillMangement %>" CodeFile="FrmInPo_IAList.aspx.cs" Inherits="RD_FrmInPo_IAList" %>
<%--入库管理--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <style type="text/css">

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
            height:100%;
        }
        .tableCell {
            display:table;
            width:100%;
        }
        .gridLineHeight {
            line-height:22px;
        }
    </style>
     <script type="text/javascript">
         function CheckDel() {
             var number = 0;
             var controls = document.getElementById("<%=grdINASN_IA.ClientID %>").getElementsByTagName("input");

             for (var i = 0; i < controls.length; i++) {
                 var e = controls[i];
                 if (e.type != "CheckBox") {
                     if (e.checked == true) {
                         number = number + 1;
                     }
                 }
             }
             if (number == 0) {
                 alert("<%=Resources.Lang.FrmDispatchUnitList_MsgTitle8 %>");
                 return false;
             }
             if (confirm("<%= Resources.Lang.FrmDispatchUnitList_MsgTitle9 %>")) {
                 return true;
             }
             else {
                 return false;
             }
         }
     </script>
  
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt;<%=Resources.Lang.FrmInPo_IAList_content1%><%--预入库通知单--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                height="11" alt="" runat="server" id="titleImg" />
                            <%--<%=Resources.Lang.Common_JSCeria%>--%>
                            <%=Resources.Lang.Common_JSCeria%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%=Resources.Lang.Common_Fold%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr style="display: none">
                        <td colspan="6">
                            <style type="text/css">
                                span.requiredSign
                                {
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
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, Common_CticketCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="PO："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPONO" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td> 
                          <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, CommonB_IPOLINE %>"></asp:Label>：<%--PO项次--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPO_Line" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td> 
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmINASSIT_DEdit_MSG3 %>"></asp:Label>：<%--批次号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtBatchNo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td> 
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="ERPCODE："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtErpCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td> 
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>"></asp:Label>：:
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCinvCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td> 
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, Common_CreateUser %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('<% =txtDCREATETIMEFrom.ClientID  %>','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：:
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDCREATETIMETo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('<% =txtDCREATETIMETo.ClientID  %>','y-mm-dd',0);" />
                          <%--  <span class="requiredSign" style="left: -50px">*</span>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCVENDERCODE" runat="server" Text="<%$ Resources:Lang, CommonB_CVENDERCODE %>"></asp:Label>：<%--供应商编码：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCVENDERCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCVENDER" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG24 %>"></asp:Label>：<%--供应商名称：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCVENDER" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                           <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dpdStatus" runat="server" Width="96%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" align="right" colspan="6">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click">
                            </asp:Button>
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
                                ChangeTDStyle("tabCondition"); 
                            </script>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td colspan="6" style="text-align:left;padding:15px 0px;">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_AddBtn %>"></asp:Button>
                            &nbsp;&nbsp;<asp:Button ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return CheckDel()"
                                runat="server" Text="<%$ Resources:Lang, Common_DelBtn %>" CssClass="ButtonDel" />
                            &nbsp;&nbsp;<asp:Button ID="btnNew0" runat="server" CssClass="ButtonCancel" Text="<%$ Resources:Lang, Common_RevokeBtn %>"
                                OnClick="btnNew0_Click" OnClientClick="return  confirm(<%= Resources.Lang.FrmInPo_IAList_btnNew0 %>);" Visible="False"></asp:Button>
            </td>
        <tr class="tableCell">
            <td>
                <div style="min-height: 300px; overflow-x: auto; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdINASN_IA" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="grdINASN_IA_RowDataBound" CssClass="Grid gridLineHeight" PageSize="15">
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
                                <EditItemTemplate>
                             <asp:CheckBox ID="chkSelect0" runat="server" BorderStyle="Solid" BorderWidth="0px" />
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="ID" DataFormatString="" HeaderText="ID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CTICKETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CticketCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="PONO" DataFormatString="" HeaderText="PO">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="BATCHNO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINASSIT_DEdit_MSG3 %>"> <%--批次号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="ERPCODE">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CURRENCY" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG2 %>"><%--币别--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TRADECODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG1 %>"><%--贸易代码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STATUS" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Cstatus %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_EditBtn %>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_EditBtn %>" Text="<%$ Resources:Lang, Common_EditBtn %>">
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
                            <div><%=Resources.Lang.Common_TotalPage %> <%=AspNetPager1.RecordCount%> <%=Resources.Lang.Common_TotalPage1 %></div>
                        </li>
                    </ul>
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
</asp:Content>
