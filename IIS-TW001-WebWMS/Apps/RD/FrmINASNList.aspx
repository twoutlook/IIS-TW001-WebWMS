<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--<%$ Resources:Lang, FrmUnionPalletList_MSG2 %>" CodeFile="FrmINASNList.aspx.cs" Inherits="RD_FrmINASNList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script src="../../Layout/Js/Help.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt;<%= Resources.Lang.FrmUnionPalletList_MSG2 %>
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
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
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
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, Common_CticketCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmInbill_ErpCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtERP_No" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, FrmInbill_InBillTYPE %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtITYPE" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmInbill_ReasonCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%;">
                            <asp:DropDownList ID="ddlREASONCODE" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>"></asp:Label>：</td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label4" runat="server" Text="批/序號(RANK)"></asp:Label>：</td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtRank_Final" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                            <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmInbill_SourceTicketCode %>"></asp:Label>：</td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPO" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtCSTATUS" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSONCODE" runat="server" Text="<%$ Resources:Lang, Common_PreInasnCticketCode %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtINASN_IA_CTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
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
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：</td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMETo" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','y-mm-dd',0);" />
                        </td>
                       
                    </tr>
                    <tr style="display: none">
                        <td colspan="6">
                            <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, Commona_TimePeriod %>"></asp:Label>：<%--周期：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:RadioButtonList ID="rbtList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" BorderStyle="None" CssClass="DateTypeRadio">
                            </asp:RadioButtonList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmInbill_WorkType %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList runat="server" ID="ddlWorkType" Width="95%">
                                <%--<asp:ListItem Value=""><%=Resources.Lang.Common_ALL%></asp:ListItem>
                               <asp:ListItem Value=""><%=Resources.Lang.Common_Worktype0%></asp:ListItem>
                               <asp:ListItem Value=""><%=Resources.Lang.Common_Worktype1%></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                          <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>： <%--规格--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtcspec" runat="server"  CssClass="NormalInputText" Width="95%"></asp:TextBox>                          
                        </td>   
                        
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align:right;">

                                 <%--NOTE by Mark,11/04,
                                     原本由首頁  , click link 入庫通知單
                                     入庫通知單 無法觸發 查詢,
                                     按 小仲 提示, 查看 browser JS 錯誤
                                     ------------------------------
                                     VM265 Help.js
                                     function validate_DataTime(dateTime1FormId, dateTime1ToId, dateTime2FormId, dateTime2ToId,msg1,msg2) {
                                        var dt1From = document.getElementById(dateTime1FormId).value;
                                        var dt1To = document.getElementById(dateTime1ToId).value;
                        這裡報錯 XXX    var dt2From = document.getElementById(dateTime2FormId).value;
                                        var dt2To = document.getElementById(dateTime2ToId).value;
                                        if (dt1From != "" && dt1To != "" && dt1From > dt1To) {
                                            alert(msg1);
                                            return false;
                                        }
                                        if (dt2From != "" && dt2To != "" && dt2From > dt2To) {
                                            alert(msg2);
                                            return false;
                                        }
                                        return true;
                                    }
                                     ---------------
                                     也就是 OnClientClick調用的function validate_DataTime 出了問題,
                                     以實驗的方式, 不運行, xxxOnClientClick
                                     即得到預期效果.

                                     --%>
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch"
                                Text="<%$ Resources:Lang, Common_QueryBtn %>"
                                xxxOnClientClick="return validate_DataTime('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom',
                                'ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','ctl00_ContentPlaceHolderMain_txtDAUDITDATEFrom',
                                'ctl00_ContentPlaceHolderMain_txtDAUDITDATETo','<%$ Resources:Lang, FrmINASNList_MSG1 %>',
                                '<%$ Resources:Lang, FrmINASNList_MSG2 %>');"


                                OnClick="btnSearch_Click"></asp:Button><%--1.制单日期的第一个日期应该小于第二个日期!,审核日期的第一个日期应该小于第二个日期--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left" class="style1" style="padding: 15px 0px;">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_AddBtn %>" OnClick="btnNew_Click"></asp:Button>
                &nbsp;&nbsp;<asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_DelBtn %>" CssClass="ButtonDel" OnClientClick="return CheckDel()" /><%--新增工单整盘退料--%>
                &nbsp;&nbsp;<asp:Button ID="btnErpReturn" runat="server" CssClass="ButtonAdd6" Text="<%$ Resources:Lang, FrmINASNList_MSG3 %>" Visible="False"></asp:Button>
                <asp:Button ID="BtnRevoke" runat="server" CssClass="ButtonDel" Text="<%$ Resources:Lang, Common_RevokeBtn %>" Visible="true" OnClick="BtnRevoke_Click" OnClientClick="return CheckRevoke()" ></asp:Button>
                &nbsp;&nbsp;<asp:Button ID="btnSync" runat="server" CssClass="ButtonRef btnSync" OnClick="btnSync_Click" Text="<%$ Resources:Lang, Common_SyncInterface_SJTB %>"  Width="86px" />
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdINASN" runat="server" AllowPaging="True" BorderColor="Teal"
                        DataKeyNames="ID,DDEFINE4,IsSpecialWipReturn,cstatus" BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="grdINASN_RowDataBound"
                        CssClass="Grid" PageSize="15">
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
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINASNEMTList_MSG4 %>" InsertVisible="False"> <%--序号--%>
                                <ItemTemplate> 
                                <%#Container.DataItemIndex+1%> 
                                </ItemTemplate> 
                                <ItemStyle HorizontalAlign="Center" />
                             </asp:TemplateField> 
                            <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="ID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CTICKETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CticketCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="itype" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmInbill_InBillTYPE %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WORKTYPE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_WorkType %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_SourceTicketCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_ErpCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="reasoncode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_ReasonCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DDEFINE3" DataFormatString="" HeaderText="<%$ Resources:Lang, CommonB_lineReject %>"><%--判退--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CCREATEOWNERCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CreateUser %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DCREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, Common_CreateDate %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CAUDITPERSONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_UserOfApproval %>" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DAUDITDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, Common_DateOfApproval %>" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cstatusName" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Cstatus %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_EditBtn %>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_EditBtn %>" Text="<%$ Resources:Lang, Common_EditBtn %>">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>

                            <%--NOTE by Mark 09/19 10:45--%>
                            <%--NOTE by Mark 09/21 18:45 潘高已調試 入庫通知單 整單 生成 入庫單, 重新顯示--%>

                            <asp:TemplateField Visible="true" HeaderText="<%$ Resources:Lang, FrmInbill_InbillCticketCode %>" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnCreateInBill" runat="server" CausesValidation="false" CommandArgument='<%#Eval("ID") %>'
                                        CommandName="" Text="<%$ Resources:Lang, Common_GenerateBtn %>" OnClick="lbtnCreateInBill_Click" OnClientClick="return confirmprocess();"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, FrmINASNList_MSG4 %>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, FrmINASNList_MSG4 %>" Text="<%$ Resources:Lang, FrmINASNList_MSG5 %>"><%--异常&&查看--%>
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
                </div>
            </td>
        </tr>
        <script type="text/javascript">
            $(document).ready(function () {
                var fsy = get_request("Cstatus");
                if (fsy.length > 0) {
                    var IsPostBack = "<%=IsPostBack%>";
                    if (IsPostBack == "False") {
                        //第一次加载要执行的东西  
                        $("#<%=btnSearch.ClientID %>").click();
                    }
                } else {
                    if ($("#<%=txtDCREATETIMEFrom.ClientID %>").val() == "" && $("#<%=txtDCREATETIMETo.ClientID %>").val() == "") {
                        FormatSetDate('<%=txtDCREATETIMEFrom.ClientID %>', '<%=txtDCREATETIMETo.ClientID %>');    //第一个参数是开始日期，第二个参数是结束日期
                    }
                }
                $("#<%=rbtList.ClientID %>").change(function () {
                    FormatSetDate('<%=txtDCREATETIMEFrom.ClientID %>', '<%=txtDCREATETIMETo.ClientID %>'); //第一个参数是开始日期，第二个参数是结束日期
                 });
            });

            function CheckDel() {
                var number = 0;            
                $.each($("#<%=grdINASN.ClientID%>").find("span input"), function (i, item) {
                    if (item.checked == true) {
                        number = number + 1;
                    }
                });
                if (number == 0) {
                    alert("<%=Resources.Lang.FrmDispatchUnitList_MsgTitle8 %>");
                    return false;
                }
                if (confirm("<%=Resources.Lang.FrmDispatchUnitList_MsgTitle9 %>")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            
            function CheckRevoke() {
                var number = 0;
                $.each($("#<%=grdINASN.ClientID%>").find("span input"), function (i, item) {
                    if (item.checked == true) {
                        number = number + 1;
                    }
                });
                if (number == 0) {
                    alert("请选择要撤销的单据!");
                    return false;
                }
                if (confirm("确定要撤销这些单据?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            function confirmprocess() {
                if (confirm("确认生成入库单？")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
    </table>
</asp:Content>
