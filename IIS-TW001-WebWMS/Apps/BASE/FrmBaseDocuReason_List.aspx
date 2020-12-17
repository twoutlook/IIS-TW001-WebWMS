<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" 
    CodeFile="FrmBaseDocuReason_List.aspx.cs" Inherits="BASE_FrmBaseDocuReason_List" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HolderTitle" Runat="Server">
&nbsp;&nbsp;&nbsp;  <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.FrmBaseDocuReason_List_Title01%><%--基礎資料-&gt;理由码设置--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
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
                            <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr style="display: none;">
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
                            <asp:Label ID="lblInOut" runat="server" Text="<%$ Resources:Lang, FrmBaseCONFIG_List_Label2%>"></asp:Label>：<%--作用域--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlACTIONSCOPE" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblREASONCODE" runat="server" Text="<%$ Resources:Lang, FrmBaseDocuReason_List_lblREASONCODE%>"></asp:Label>：<%--理由码编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtREASONCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblSTATES" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状 态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlSTATES" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                     </tr>
                     <tr>                        
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：<%--创建人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_CreateDate%>"></asp:Label>： <%--创建日期--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtFromDate" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtFromDate','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao%>"></asp:Label>： <%--到--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtEndDate" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtEndDate','y-mm-dd',0);" />
                        </td>
                       
                     </tr>
                     <tr style="display: none;">
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
                     <tr>
                        <td colspan="6" style="text-align: right;"><asp:Button ID="btnSearch" 
                                runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" onclick="btnSearch_Click"/></td><%--查询--%>
                     </tr>               
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd"  Text="<%$ Resources:Lang, Common_Add %>"></asp:Button> <%--新增--%>
                &nbsp;&nbsp;<asp:Button ID="btnUnable" runat="server" Text="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_btnUnable %>" CssClass="ButtonClose" OnClientClick="return CheckDel()" onclick="btnUnable_Click"/><%--作废--%>
                &nbsp;&nbsp;<asp:Button ID="btnQy" runat="server" Text="<%$ Resources:Lang, FrmBaseDocuReason_List_btnQy %>" CssClass="ButtonDo" OnClientClick="return CheckQy()" onclick="btnQy_Click"/><%--启用--%>
            </td>
        </tr>    
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">              
                    <asp:GridView ID="grdBASE_CARGOSPACE" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                      ShowHeaderWhenEmpty="true"
                        OnRowDataBound="grdBASE_CARGOSPACE_RowDataBound" CssClass="Grid" PageSize="15">
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
                             <asp:BoundField DataField="ACTIONSCOPE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBaseCONFIG_List_Label2%>"> <%--作用域--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REASONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBaseDocuReason_List_lblREASONCODE%>"><%--理由码编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REASONCONTENT" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBaseDocuReason_List_REASONCONTENT%>"><%--理由码说明--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STATES" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"> <%--状态--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="coperatorname" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"> <%--创建人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DCREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HeaderText="<%$ Resources:Lang, Common_CreateDate%>"> <%--创建日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False"  Width="80px"/>
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="ENABLEUSER" DataFormatString="" HeaderText="作废人员">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ENABLEDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HeaderText="作废日期">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False"  Width="80px"/>
                            </asp:BoundField>                           --%>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                        </Columns>
                   </asp:GridView>
                       <ul class="OneRowStyle">
                        <li>
                            <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                               FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                            </webdiyer:aspnetpager>
                        </li>
                        <li>
                            <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
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

            function CheckDel() {
                var number = 0;
                var controls = document.getElementById("<%=grdBASE_CARGOSPACE.ClientID %>").getElementsByTagName("input");

                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmBase_InOutTypeStatusList_Msg01%>");//请选择需要作废的项！
                    return false;
                }
                if (confirm("<%= Resources.Lang.FrmBase_InOutTypeStatusList_Msg02%>")) {//你确认作废吗？
                    return true;
                }
                else {
                    return false;
                }

            }
            function CheckQy() {
                var number = 0;
                var controls = document.getElementById("<%=grdBASE_CARGOSPACE.ClientID %>").getElementsByTagName("input");

                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmBaseDocuReason_List_Msg01%>");//请选择需要启用的项！
                    return false;
                }
                if (confirm("<%= Resources.Lang.FrmBaseDocuReason_List_Msg02%>")) {//你确认启用吗？
                    return true;
                }
                else {
                    return false;
                }

            }
        </script>
    </table>
</asp:Content>