<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" Title="--出库通知单" CodeFile="FrmOUTASNList.aspx.cs" Inherits="OUT_FrmOUTASNList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
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

        input[type='submit'][disabled],input[type='button'][disabled]
        {       
            opacity:0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70);/*兼容ie8及以下*/
        }
        .ButtonDo1 {
            border-right: 0px;
            border-top: 0px;
            font-size: 12px;
            font-weight: bold;
            border-left: 0px;
            cursor: pointer;
            border-bottom: 0px;
            padding-left: 22px;
            padding-top: 2px;
            font-family: "Arial", "Helvetica", "sans-serif";
            height: 24px;
            color: #3580c9;
            background-image: url(../../Layout/Css/LG/Images/in_s_bg_do1.jpg);
            width: 120px;
        }
        /*.Grid .aspNetDisabled {
            color:#cccccc;
        }*/
        .btnContainer input:not(:first-child) {
            margin-left:12px;
        }
        .auto-style1 {
            width: 21%;
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.WMS_Common_Menu_ChuKu %>-&gt;<%= Resources.Lang.FrmOUTASNList_Menu_PageName %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="width: 95%">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.WMS_Common_SearchTitle %>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js"></script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.WMS_Common_FoldAlt %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center" onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cticketcode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ErpCode %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_OutType %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtITYPE" runat="server" Width="96%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCinvcode" runat="server"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                         <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label4" runat="server" Text="批/序號(RANK)"></asp:Label>：
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtRank_Final" runat="server"
                                CssClass="NormalInputText" Width="95%" MaxLength="1"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbSO" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_SourceCode %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtSO" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                      
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, WMS_Common_GridView_Status %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="txtCSTATUS" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%; height: 23px;">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_IsMergeCode %>"></asp:Label>：
                        </td>
                        <td class="auto-style1">
                            <!--（0-非合并拣货指引；1-合并拣货指引）-->
                            <asp:DropDownList ID="ddlIS_MERGE" runat="server" Width="95%">
                                <%--<asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="0">非合并</asp:ListItem>
                                <asp:ListItem Value="1">合并</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%; height: 23px;">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmOUTASNList_SMTType %>"></asp:Label>：
                        </td>
                        <td class="auto-style1">
                            <asp:DropDownList ID="ddlSmt" runat="server" Width="95%">
                                <%--<asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="0">厂内SMT-A</asp:ListItem>
                                <asp:ListItem Value="1">厂内SMT-B</asp:ListItem>
                                <asp:ListItem Value="2">厂内非SMT</asp:ListItem>
                                <asp:ListItem Value="3">厂外SMT</asp:ListItem>
                                <asp:ListItem Value="4">厂外非SMT</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                          <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateOwner %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMETo" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','y-mm-dd',0);" />
                        </td>
                       
                    </tr>
                    <tr style="display: none;">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCAUDITPERSONCODE" runat="server" Text="<%$ Resources:Lang,FrmOUTASNList_AuditUser %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCAUDITPERSONCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITDATEFromFrom" runat="server" Text="<%$ Resources:Lang,FrmOUTASNList_AuditDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap">
                            <asp:TextBox ID="txtDAUDITDATEFrom" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITDATEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITDATEToTo" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDAUDITDATETo" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITDATETo','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%"><asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang,Commona_TimePeriod %>"></asp:Label>：</td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:RadioButtonList ID="rbtList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" BorderStyle="None" CssClass="DateTypeRadio">                          
                            </asp:RadioButtonList>
                        </td>
                        <td class="InputLabel" style="width: 13%"><%= Resources.Lang.WMS_Common_Element_WorkType %>：</td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:DropDownList ID="drpWorkType" runat="server" Width="95%">
                               <%-- <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="0">平库</asp:ListItem>
                                <asp:ListItem Value="1">立库</asp:ListItem>--%>
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
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td colspan="6" align="left" style="padding: 15px 0px;" class="btnContainer">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang,WMS_Common_Button_New %>"></asp:Button>
                <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" CssClass="ButtonDel" OnClientClick="return CheckDel()" />
                <asp:Button ID="btnClose" runat="server" CssClass="ButtonDo" Text="<%$ Resources:Lang,FrmOUTASNList_Button_JieAn %>" OnClientClick="return CheckClose()" OnClick="btnClose_Click" Visible="False"></asp:Button>
                <asp:Button ID="Merge" runat="server" CssClass="ButtonMerge" Text="<%$ Resources:Lang,FrmOUTASNList_Button_Merge %>" OnClick="Merge_Click" OnClientClick="return CheckOption();" ></asp:Button>
                <asp:Button ID="btnCancleMerge" runat="server" CssClass="ButtonDel5" Text="<%$ Resources:Lang,FrmOUTASNList_Button_CancelMerge %>" OnClick="btnCancleMerge_Click" OnClientClick="return CheckOption();" ></asp:Button>
                <asp:Button ID="Btn_Out" runat="server" CssClass="ButtonDo1" Text="<%$ Resources:Lang,FrmOUTASNList_Button_ConfirmOut %>" OnClick="Btn_Out_Click" OnClientClick="return CheckConfirm();"></asp:Button>
                <asp:Button ID="Btn_EmergencyOut" runat="server" CssClass="ButtonDo1" Text="<%$ Resources:Lang,FrmOUTASNList_Button_EmergencyOut %>" OnClick="Btn_EmergencyOut_Click" OnClientClick="return CheckOption();"></asp:Button>
                <asp:Button ID="BtnRevoke" runat="server" CssClass="ButtonClose" Text="<%$ Resources:Lang,FrmOUTASNList_Button_Revoke %>" Visible="true" OnClick="BtnRevoke_Click"></asp:Button>
                 <asp:Button ID="btnSync" runat="server" CssClass="ButtonRef btnSync" OnClick="btnSync_Click" Text="<%$ Resources:Lang, Common_SyncInterface_SJTB %>"  Width="86px" />
            </td>
        </tr>
        <tr class="tableCell">
            <td>
                <div style="min-height: 460px; overflow-x: auto; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdOUTASN" runat="server" AllowPaging="True" BorderColor="Teal" DataKeyNames="ID,CDEFINE2,ITYPE,CERPCODE,IsSpecialWipIssue,Is_CJ,CTICKETCODE"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnSorting="grdOUTASN_Sorting"
                        OnRowDataBound="grdOUTASN_RowDataBound" AllowSorting="true" CssClass="Grid gridLineHeight" PageSize="15" >
                        <PagerSettings Mode="NumericFirstLast" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
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
                            <asp:BoundField DataField="CTICKETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cticketcode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="itype" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNList_OutType %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ErpCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CSO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNList_SoCode %>" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="START_DATES" DataFormatString="{0:d}" HeaderText="<%$ Resources:Lang, FrmOUTASNList_StartDate %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SHIFTS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNList_Shift %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CCREATEOWNERCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_CreateOwner %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DCREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, WMS_Common_Element_CreateDate %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CAUDITPERSONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNList_AuditUser %>" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DAUDITDATE" DataFormatString="{0:yyyy-MM-dd}" HeaderText="<%$ Resources:Lang, FrmOUTASNList_AuditDate %>" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="worktype" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_WorkType %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IS_MERGENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNList_IsMerge %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cstatus" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Status %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SPECIAL_OUT" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNList_SpecialOut %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" Text="<%$ Resources:Lang, WMS_Common_GridView_Edit %>">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmOUTASNList_OutBill %>" ShowHeader="False" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnCreateOutBill" runat="server" CausesValidation="false" CommandArgument='<%#Eval("ID") %>'
                                        CommandName="" Text="<%$ Resources:Lang, FrmOUTASNList_ShengCheng %>" OnClick="lbtnCreateOutBill_Click" OnClientClick="return confirmprocess();"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, FrmOUTASNList_YiChang %>" DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNList_YiChang %>" Text="<%$ Resources:Lang, FrmOUTASNList_ChaKang %>">
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
            </td>
        </tr>
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

            function ChangeDivWidth() {
                document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
            }
            window.onresize = ChangeDivWidth;
            ChangeDivWidth();

            function CheckDel() {
                var number = 0;
                $.each($("#<%=grdOUTASN.ClientID%>").find("span input"), function (i, item) {
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
            function CheckClose() {
                var number = 0;
                $.each($("#<%=grdOUTASN.ClientID%>").find("span input"), function (i, item) {
                    if (item.checked == true) {
                        number = number + 1;
                    }
                });
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmOUTASNList_Tips_XuanZeJieAn %>");
                    return false;
                }
                if (confirm("<%= Resources.Lang.FrmOUTASNList_Tips_QueDingJieAn %>")) {
                    return true;
                }
                else {
                    return false;
                }
            }

            function CheckConfirm() {
                var number = 0;
                $.each($("#<%=grdOUTASN.ClientID%>").find("span input"), function (i, item) {
                    if (item.checked == true) {
                        number = number + 1;
                    }
                });
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmOUTASNList_Tips_XuanZeChuKu %>");
                    return false;
                }
                if (confirm("<%= Resources.Lang.FrmOUTASNList_Tips_QueDingChuKu %>")) {
                    return true;
                }
                else {
                    return false;
                }
            }

            function CheckOption() {
                var number = 0;
                $.each($("#<%=grdOUTASN.ClientID%>").find("span input"), function (i, item) {
                    if (item.checked == true) {
                        number = number + 1;
                    }
                });
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmOUTASNList_Tips_XuanZeCaoZuo %>");
                    return false;
                }
                return true;
            }

            function confirmprocess() {
                if (confirm("确认生成出库单？")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
    </table>
</asp:Content>
