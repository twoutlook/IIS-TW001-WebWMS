<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPartReport.aspx.cs" MasterPageFile="~/Apps/DefaultMasterPage.master" Inherits="Apps_Report_FrmPartReport" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="DreamTek.ASRS.Business" namespace="DreamTek.ASRS.Business" tagprefix="HDTools" %>

<%@ Register Src="../BASE/ShowWAREHOUSEDiv.ascx" TagName="ShowWAREHOUSEDiv" TagPrefix="ucwh" %>
<%@ Register Src="../BASE/ShowAREADiv.ascx" TagName="ShowAREADiv" TagPrefix="ucArea" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.FrmReport_ReportManagement%>&gt;  <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
    <%=Resources.Lang.FrmPartReport_Content1%><%--物料统计报表 --%> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <ucarea:showareadiv ID="ucShowArea" runat="server" />
    <ucwh:showwarehousediv ID="ucShowWAREHOUSEDiv" runat="server" />
     <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">
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
                    <tr id="trwarehouse" runat="server">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblwarehouse" runat="server" Text="<%$ Resources:Lang, Common_WareHouse %>"></asp:Label>：                            
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtwarehouse" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <asp:TextBox ID="txtwarehousename" runat="server" style ="display:none"></asp:TextBox>                        
                         
                              <img alt="" onclick="disponse_div(event,document.all('<%= ucShowWAREHOUSEDiv.GetDivName %>'));" 
                                src="../../Images/Search.gif" class="select" />
                           
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>： <%--规格--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtcspec" runat="server"  CssClass="NormalInputText" Width="95%"></asp:TextBox>                          
                        </td>    
                      
                        <td class="InputLabel" style="width: 13%">
                          
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                         
                        </td>
                    </tr>
                           <tr id="trarea" runat="server">
                        <td class="InputLabel" style="width: 13%">                           
                             <asp:Label ID="lblarea" runat="server" Text="区域："></asp:Label>

                        </td>
                        <td style="width: 20%; white-space: nowrap;">                      
                            <asp:TextBox ID="txtarea" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                               <asp:TextBox ID="txtareaname" runat="server" style ="display:none"></asp:TextBox>
                             <img alt="" onclick="disponse_div(event,document.all('<%= ucShowArea.GetDivName %>'));" 
                                src="../../Images/Search.gif" class="select" />
                        </td>
                       <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>： <%--规格--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtcspec1" runat="server"  CssClass="NormalInputText" Width="95%"></asp:TextBox>                          
                        </td>    
                        <td class="InputLabel" style="width: 13%">
                          
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                         
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
                    <tr>
                        <td colspan="6" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click">
                            </asp:Button>
                              &nbsp;
                            <HDTools:ExcelButton runat="server" GridID="grdPartSpace" ID="btnExcel" ExcelName=""></HDTools:ExcelButton>
                            
                        </td>
                    </tr>
                      </table>
            </td>
        </tr>
             <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <ASP:GridView ID="grdPartSpace" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnPageIndexChanged="grdPartSpace_PageIndexChanged" 
                        OnRowDataBound="grdPartSpace_RowDataBound" CssClass="Grid" PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>

                            <asp:BoundField DataField="cwarehousecode" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_WareHouseCode %>"><%--仓库编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cwarename" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_WareHouseName %>"><%--仓库名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="area_code" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmPartReport_area_code %>"><%--区域编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="area_name" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmPartReport_area_name %>"><%--区域名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cinvcode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="calias" DataFormatString=""  HeaderText="<%$ Resources:Lang, CommonB_calias %>"> <%--助记码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="300px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="cpartname" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cspecifications" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACE_Report_MSG5 %>"><%--规格--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                              <asp:BoundField DataField="sumqty" DataFormatString="{0:f}" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG6 %>"><%--數量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="cunits" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInPoEdit_UNIT %>"><%--单位--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
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
                        //settingPad("<%= grdPartSpace.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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