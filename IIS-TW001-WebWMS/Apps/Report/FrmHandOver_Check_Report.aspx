<%@ Page Title="" Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master"
    AutoEventWireup="true" CodeFile="FrmHandOver_Check_Report.aspx.cs" Inherits="Report_FrmHandOver_Check_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <!--
<script type="text/javascript" src="../../Layout/My97DatePicker/WdatePicker.js">
</script>
-->
    <!--
    <script type="text/javascript" src="../../Layout/Calendar/Calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
-->
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.FrmReport_ReportManagement%>-&gt;  <%=Resources.Lang.FrmHandOver_Check_Report_Content1%><%--分發明細核對报表 --%>

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
                        <th align="left" colspan="7">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                height="11" alt="" runat="server" id="titleImg" />
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
                            <style type="text/css;">
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
                                    width: 35%;
                                    height: 25px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmHandOver_Check_Report_Label2 %>"></asp:Label>：<%--Erp號--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtErpCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG5 %>"></asp:Label>：<%--料號：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCinvCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmHandOver_Check_Report_Label4 %>"></asp:Label>：<%--核對人：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCheckUser" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                          <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmHandOver_Check_Report_MSG1 %>"></asp:Label>：<%--核對時間：--%>
                        </td>
                        <td style="white-space: nowrap;" style="width: 20%">
                            <asp:TextBox ID="txtBeginDate" runat="server" CssClass="NormalInputText" Width="85%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtBeginDate','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmHandOver_Check_Report_MSG2 %>"></asp:Label>：<%--至：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="NormalInputText" Width="85%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;left: -30px; top: 0px" 
                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtEndDate','y-mm-dd',0);" />
                        </td>

                         <td class="InputLabel" style="width: 13%">
                            
                        </td>
                        <td style="width: 20%">
                            
                        </td>
                        
                    </tr>
                  
                   
                    <tr>
                         <td colspan="6" style="text-align: right">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top">
              <%--  <cc1:DataGridNavigator3 ID="grdNavigatorSNList" runat="server" GridID="grdSNList" ShowPageNumber="false"
                    ExcelName="SNList" IsDbPager="True" OnGetExportToExcelSource="grdNavigatorSNList_GetExportToExcelSource" />--%>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <ASP:GridView ID="grdSNList" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnPageIndexChanged="grdSNList_PageIndexChanged" OnPageIndexChanging="grdSNList_PageIndexChanging"
                        OnRowDataBound="grdSNList_RowDataBound" CssClass="Grid" PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                          <%--  <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="ID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmHandOver_Check_Report_Label2 %>"><%--Erp號--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False"/>
                            </asp:BoundField>
                             <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG5 %>"><%--料號--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False"/>
                            </asp:BoundField>
                             <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="240px"/>
                            </asp:BoundField>
                          <%--  <asp:BoundField DataField="QTY_DEMAND" DataFormatString="" HeaderText="需求量">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" Wrap="False"/>
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="OUT_QTY" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNEdit_Column_ChuKuLiang %>"><%--出库量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" Wrap="False"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="HAND_QTY" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmHandOver_Check_Report_MSG3 %>"><%--已交付量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" Wrap="False"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="NOHAND_QTY" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmHandOver_Check_Report_MSG4 %>"><%--未交付量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" Wrap="False"/>
                            </asp:BoundField>
                             <asp:BoundField DataField="CHECK_QTY" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmHandOver_Check_Report_MSG5 %>"><%--已核對量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" Wrap="False"/>
                            </asp:BoundField>
                             <asp:BoundField DataField="NOCHECK_QTY" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmHandOver_Check_Report_MSG6 %>"><%--未核對量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" Wrap="False"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="IQTY" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmHandOver_Check_Report_MSG7 %>"><%--核對量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" Wrap="False"/>
                            </asp:BoundField>
                             <asp:BoundField DataField="CREATEOWNER" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmHandOver_Check_Report_Label4 %>"><%--核對人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmHandOver_Check_Report_MSG1 %>"><%--核對時間--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px"/>
                            </asp:BoundField>
                            

                        </Columns>
                    </ASP:GridView>

                    <ul class="OneRowStyle">
	                    <li >
		                     <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
			                    FirstPageText="<%$ Resources:Lang, Common_FirstPage %>" LastPageText="<%$ Resources:Lang, Common_LastPage %>" NextPageText="<%$ Resources:Lang, Common_NextPage %>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
			                    AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
		                    </webdiyer:AspNetPager>
	                    </li>
	                    <li>
		                    <div><%=Resources.Lang.Common_TotalPage%> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
	                    </li>
                    </ul>

                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdSNList.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                    </script>
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
