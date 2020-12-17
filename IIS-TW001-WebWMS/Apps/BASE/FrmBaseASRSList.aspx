<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBaseASRSList.aspx.cs"
     MasterPageFile="~/Apps/DefaultMasterPage.master" Inherits="Apps_BASE_FrmBaseASRSList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ExcelBtn" Namespace="DreamTek.ASRS.Business" Assembly="DreamTek.ASRS.Business" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/Js/Help.js" type="text/javascript"></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ui-autocomplete-loading
        {
            background: white url('../../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }
        .select
        {
            cursor: hand;
            position: relative;
            left: -25px;
            top: 4px;
        }
    </style>
    <script type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBaseASRSList_Title01%>-&gt;<%= Resources.Lang.FrmBaseASRSList_Title02%><%--AS/RS命令管理-&gt;列表--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
   
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
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_lblCTICKETCODE%>"></asp:Label>：<%--单据号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCASNID" runat="server" Text="<%$ Resources:Lang, FrmBaseASRSList_lblCASNID%>"></asp:Label>：<%--站口--%>
                        </td>
                        <td style="width: 20%">
                           <asp:DropDownList ID="ddlStn" runat="server" Width="95%" >
                                <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmBaseASRSList_Label3%>"></asp:Label>：<%--单据类型--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCaseType" runat="server" Width="95%" >
                                <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="IN">入库</asp:ListItem>
                                <asp:ListItem Value="OUT">出库</asp:ListItem>
                                <asp:ListItem Value="ALLOCATE">调拨</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                  
                    <tr>
                     <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBaseASRSList_Label1%>"></asp:Label>：<%--单据储位--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPositionCode" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td colspan="4" align="right" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" 
                                onclick="btnSearch_Click" ><%--查询--%>
                            </asp:Button> &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td>
                <table id="TabMain0" style="height: 100%; width: 100%" runat="server">
                    
                    <tr>
                        <td valign="top" align="left" colspan="5">
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" colspan="5">
                          <%--  <cc1:DataGridNavigator3 ID="grdNavigatorINBILL_D" runat="server" GridID="grdINBILL_D"
                                ExcelButtonVisible="false" ShowPageNumber="false" ExcelName="INBILL_D" IsDbPager="True"
                                OnGetExportToExcelSource="grdNavigatorINBILL_D_GetExportToExcelSource" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                                <asp:GridView ID="grdINBILL_D" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    OnPageIndexChanged="grdINBILL_D_PageIndexChanged" OnPageIndexChanging="grdINBILL_D_PageIndexChanging"
                                    OnRowDataBound="grdINBILL_D_RowDataBound" CssClass="Grid" PageSize="15" >
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
                                        <asp:BoundField DataField="IDS" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, Common_IDS%>" Visible="False"><%--子表编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                       
                                        <asp:BoundField DataField="wmstskid" DataFormatString="" HeaderText="wmstskid">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                                        </asp:BoundField>
                                       
                                        <asp:BoundField DataField="cmdsno" DataFormatString="" HeaderText="cmdsno">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="stnno" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBaseASRSList_lblCASNID%>"><%--站口--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cmdmodeName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBaseASRSList_cmdmodeName%>"><%--模式--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cmdstsName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBaseASRSList_cmdstsName%>"><%--命令状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="LOC_CODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBaseASRSList_LOC_CODE%>"><%--命令原始储位编码--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="LOC_NAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBaseASRSList_LOC_NAME%>"><%--命令原始储位名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="NEWLOC_CODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBaseASRSList_NEWLOC_CODE%>"><%--命令目的储位编码--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NEWLOC_NAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBaseASRSList_NEWLOC_NAME%>"><%--命令目的储位名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="cticketcode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmLogSystemList_Label1%>"><%--单据编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="caseTypeName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBaseASRSList_Label3%>"><%--单据类型--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="cpositioncode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBaseASRSList_Label1%>"><%--单据储位--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="acttime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmBaseASRSList_acttime%>"><%--命令执行时间--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>

                                      
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmMixedTemporaryStorageList_Process%>" ShowHeader="False"><%--操作--%>
                                            <ItemTemplate>
                                                <asp:Button ID="LinkASRS_STATUS" runat="server" CausesValidation="false" CommandArgument='<%#Eval("wmstskid") %>'
                                                    CommandName="" OnClick="LinkASRS_STATUS_Click"  Text="<%$ Resources:Lang, FrmBaseASRSList_LinkASRS_STATUS%>"><%--强制完成--%>
                                                </asp:Button>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:TemplateField>

                                      
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmMixedTemporaryStorageList_Process%>" ShowHeader="False"><%--操作--%>
                                            <ItemTemplate>
                                                <asp:Button ID="btnRefresh" runat="server" CausesValidation="false" CommandArgument='<%#Eval("wmstskid") %>'
                                                    CommandName=""  OnClick="btnRefresh_Click" Text="<%$ Resources:Lang, FrmBaseASRSList_btnRefresh%>"></asp:Button><%--取消命令--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" ForeColor="Blue" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                                <ul class="OneRowStyle">
	                            <li >
		                             <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
			                           FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
			                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
		                            </webdiyer:AspNetPager>
	                            </li>
	                            <li>
		                            <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
	                            </li>
                            </ul>

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
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
