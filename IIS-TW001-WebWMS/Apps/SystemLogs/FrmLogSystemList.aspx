<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" Title="<%$ Resources:Lang, FrmLogSystemList_Title01%>" CodeFile="FrmLogSystemList.aspx.cs" Inherits="RD_FrmLogSystemList" %><%--系统日志--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.FrmLogSystemList_Title01%><%--基础资料-&gt;系统日志--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.Common_JSCeria%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center" onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
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
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmLogSystemList_Label1%>"></asp:Label>：<%--单据编号--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;" >
                           
                            <asp:TextBox ID="txtCaseNO" runat="server" Width="95%"
                                CssClass="NormalInputText"></asp:TextBox>
                          
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="ERPCODE："></asp:Label>
                        </td>
                        <td style="width: 20%; white-space: nowrap;" >
                           
                            <asp:TextBox ID="txtErpCode" runat="server" Width="95%"
                                CssClass="NormalInputText"></asp:TextBox>
                          
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmLogSystemList_Label3%>"></asp:Label>：<%--异常信息--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;" >
                           
                            <asp:TextBox ID="txtErrorMsg" runat="server" Width="95%"
                                CssClass="NormalInputText"></asp:TextBox>
                          
                        </td>
                    </tr>
                     <tr style="display:none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmLogSystemList_Label4%>"></asp:Label>：<%--模块--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;" >
                           
                           <asp:DropDownList ID="ddlModule" runat="server" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged" AutoPostBack="true" Width="95%"></asp:DropDownList>
                          
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmLogSystemList_Label5%>"></asp:Label>：<%--子模块--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;" >
                         <asp:DropDownList ID="ddlSubModule" runat="server" Enabled="false" Width="95%"></asp:DropDownList>
                          
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmLogSystemList_Label6%>"></asp:Label>：<%--错误源--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;" >
                           
                           <asp:DropDownList ID="ddlSource" runat="server" Width="95%"></asp:DropDownList>
                          
                        </td>
                    </tr>

                      <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, Common_CreateDate%>"></asp:Label>： <%--创建日期--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;" >
                           <asp:TextBox ID="txtDATEFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" id="imgDATEFrom" runat="server" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDATEFrom','y-mm-dd',0);" />
                          
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao%>"></asp:Label>：<%--到--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;" >
                           
                           <asp:TextBox ID="txtDATETo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" id="imgDATETo" runat="server" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDATETo','y-mm-dd',0);" />
                          
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label> <%--创建人--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;" >
                           
                            <asp:TextBox ID="txtUser" runat="server" Width="95%"
                                CssClass="NormalInputText"></asp:TextBox>
                          
                        </td>
                    </tr>

                     <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Lang, FrmLogSystemList_Label10%>"></asp:Label>：<%--错误码--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;" >
                           
                            <asp:TextBox ID="txtErrorNumber" runat="server" Width="95%"
                                CssClass="NormalInputText"></asp:TextBox>
                          
                        </td>
                         <td style="width: 97%; text-align: right;" colspan="4">
                             <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"></asp:Button>  <%--查询--%>
                         </td>
                         </tr>
                    
                   <%-- <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblTitle" runat="server" Text="单据编号："></asp:Label>
                        </td>
                        <td style="width: 20%; white-space: nowrap;" colspan="4">
                           
                            
                            <asp:Label ID="lblDAUDITDATEToTo" runat="server" Text="到：" ForeColor="#3580C9"></asp:Label>
                           
                            &nbsp;
                            <asp:Label ID="lblLogsType" runat="server" Text="日志类型：" ForeColor="#3580C9"></asp:Label>
                            &nbsp;
                            <!--类型( 0 : 系统日志 ，1: 上架指引，2: 拣货指引, 3 ：入库扣帐, 4：出库扣帐)-->
                            <asp:DropDownList ID="ddlLogsType" runat="server"></asp:DropDownList>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="查询" OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>--%>

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
                    <tr style="display: none;">
                        <td colspan="6" align="center">&nbsp;&nbsp;<asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"   Visible="false"></asp:Button><%--新增--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top" style="display: none;">
            <td valign="top" align="left"></td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdINASN_D" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                        Width="100%" AutoGenerateColumns="False" OnSorting="grdINASN_D_Sorting" AllowSorting="true"
                        CssClass="Grid" PageSize="15">
                        <PagerSettings Mode="NumericFirstLast" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"
                            Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""
                            Wrap="False" />
                        <Columns>
                            <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmALLOCATEEdit_IDS%>" Visible="false"><%--子表编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ErrorNumber" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmLogSystemList_Label10%>"><%--错误码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ErrorMsg" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmLogSystemList_Label3%>"> <%--异常信息--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="300px" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CaseNO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmLogSystemList_Label1%>"><%--单据编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ERPCode" DataFormatString="" HeaderText="ERPCode">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ModuleTypeName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmLogSystemList_Label4%>" Visible="false"><%--模块--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SubModuleTypeName" HeaderText="<%$ Resources:Lang, FrmLogSystemList_Label5%>" Visible="false"><%--子模块--%>
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="SourceTypeName" HeaderText="<%$ Resources:Lang, FrmLogSystemList_Label6%>" Visible="false"><%--错误源--%>
                                <ItemStyle Width="100px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CREATEDATE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CreateDate%>"  >
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="140px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CreateUser" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"> <%--创建人--%>
                                <ItemStyle Width="100px" />
                            </asp:BoundField>

                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" Visible="false"
                                DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                        </Columns>
                    </asp:GridView>
                    <ul class="OneRowStyle">
                        <li>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                               FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                            </webdiyer:AspNetPager>
                        </li>
                        <li>
                            <div><%= Resources.Lang.Base_Gong%><%=AspNetPager1.RecordCount %> <%= Resources.Lang.Base_Data%></div>
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
    <input type="hidden" runat="server" id="hiddTableName" />
    <input type="hidden" runat="server" id="hiddId" />
</asp:Content>




