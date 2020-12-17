<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--<%$ Resources:Lang, FrmUnionPalletList_MSG2 %>" CodeFile="FrmUnionPalletList.aspx.cs" Inherits="FrmUnionPalletList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script src="../../Layout/Js/Help.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt;<%=Resources.Lang.FrmUnionPalletList_MSG3%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
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
                                .style1
                                {
                                    height: 29px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmInbill_PalletCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPalletCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, WmsDBCommon_ASRS_Msg15 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtINBillCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                       <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmUnionPalletList_MSG4 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtOutBillCticketCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, Common_OutAsnTicket %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtOASNTicketCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, Common_InasnCticketCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtINASNTicketCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                       <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmUnionPalletList_MSG1 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtUnionASNCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','y-mm-dd',0);" />
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMETo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle28 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCreateUser" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                          <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, Commona_TimePeriod %>"></asp:Label>：<%--周期：--%>
                        </td>
                        <td style="width: 21%">
                            <%--<asp:DropDownList runat="server" ID="ddlMonthOrWeek" Width="95%" OnSelectedIndexChanged="ddlMonthOrWeek_SelectedIndexChanged">
                            </asp:DropDownList>--%>
                            <%--<asp:RadioButtonList   runat="server"  ID="rbtList"  CssClass="DateTypeRadio" RepeatLayout="Flow" RepeatDirection="Horizontal" TextAlign="Left"/>--%>
                            <asp:RadioButtonList ID="rbtList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" BorderStyle="None" CssClass="DateTypeRadio">                          
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="6">
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="6">
                            <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClientClick="return validate_DataTime('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','ctl00_ContentPlaceHolderMain_txtDAUDITDATEFrom','ctl00_ContentPlaceHolderMain_txtDAUDITDATETo','制单日期的第一个日期应该小于第二个日期!','审核日期的第一个日期应该小于第二个日期!');"
                                OnClick="btnSearch_Click1"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdINASN" runat="server" AllowPaging="True" BorderColor="Teal"
                        DataKeyNames="ID" BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        CssClass="Grid" PageSize="15" OnRowDataBound="grdINASN_RowDataBound">
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
                            <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="ID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PALLETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_PalletCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>

                            <asp:BoundField DataField="INASNCODE" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmUnionPalletList_MSG5 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="INASNCODE_PALLET" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_InasnCticketCode %>"> 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>   
                             <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>
                            <asp:BoundField DataField="INBILLCODE_PALLET" DataFormatString="" HeaderText="<%$ Resources:Lang, WmsDBCommon_ASRS_Msg15 %>"> 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>   
                             <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>
                            <asp:BoundField DataField="OUTASNCODE_PALLET" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmUnionPalletList_MSG5 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OUTBILLCODE_PALLET" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmUnionPalletList_MSG4 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CSTATUS" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Cstatus %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATEUSER" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle28 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>                            
                            <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle18 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                           
                        </Columns>
                    </asp:GridView>
                     <ul class="OneRowStyle"  >
                    <li>
                        <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                            FirstPageText="<%$ Resources:Lang, Common_FirstPage%>" LastPageText="<%$ Resources:Lang, Common_LastPage%>" NextPageText="<%$ Resources:Lang, Common_NextPage%>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                        </webdiyer:aspnetpager>
                    </li>
                    <li>
                        <div><%=Resources.Lang.Common_TotalPage %> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_TotalPage1 %></div>
                    </li>
                </ul>
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdINASN.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                    </script>
                </div>
            </td>
        </tr>
        <script type="text/javascript">
            $(document).ready(function () {
                if ($("#<%=txtDCREATETIMEFrom.ClientID %>").val() == "" && $("#<%=txtDCREATETIMETo.ClientID %>").val() == "") {
                              FormatSetDate('<%=txtDCREATETIMEFrom.ClientID %>', '<%=txtDCREATETIMETo.ClientID %>');    //第一个参数是开始日期，第二个参数是结束日期
                }
                          $("#<%=rbtList.ClientID %>").change(function () {
                              FormatSetDate('<%=txtDCREATETIMEFrom.ClientID %>', '<%=txtDCREATETIMETo.ClientID %>');
                     //第一个参数是开始日期，第二个参数是结束日期

                 });
                      });
        </script>
    </table>
</asp:Content>
