<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--<%$ Resources:Lang, FrmPositionStockRatioReport_title1 %>" CodeFile="FrmPositionStockRatioReport.aspx.cs" Inherits="FrmPositionStockRatioReport" %>
<%--區域儲位使用率報表--%>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ExcelBtn" Namespace="DreamTek.ASRS.Business" Assembly="DreamTek.ASRS.Business" %>
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
    <%=Resources.Lang.CommonB_ReportManagement %><%--報表管理--%>&gt;<%=Resources.Lang.FrmPositionStockRatioReport_title1 %><%--區域儲位使用率報表 --%>  
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
                        <td class="InputLabel" style="width: 15%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, CommonB_Area %>"></asp:Label>：<%--區域：--%>
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtArea" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 15%">
                           
                        </td>
                        <td style="width: 35%; white-space: nowrap;">
                         
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
                            <ExcelBtn:ExcelButton runat="server" GridID="grdPositionRatio" ID="btnExcel" ExcelName="">
                               <%--   <BlockList>
                                <ExcelBtn:Block Key="未使用" value="NOSTOCKCOUNT" seq="4"  />
                                </BlockList>--%>
                            </ExcelBtn:ExcelButton>
                            &nbsp;
                          <%--  <ExcelBtn:OutPutButton ID="OutPutButton1" runat="server" ExcelName="PositionRatio" GridID="grdPositionRatio"
                                OnGetExportToExcelSource="grdNavigatorPositionRatio_GetExportToExcelSource">
                                <BlockList>
                                <ExcelBtn:Block Key="未使用" value="NOSTOCKCOUNT" seq="4" />
                                </BlockList>
                            </ExcelBtn:OutPutButton>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr valign="top">
            <td valign="top">
               <%-- <cc1:DataGridNavigator3 ID="grdNavigatorPositionRatio" runat="server" GridID="grdPositionRatio"
                ExcelButtonVisible="false"  ShowPageNumber="false" ExcelName="PositionRatio" IsDbPager="True" OnGetExportToExcelSource="grdNavigatorPositionRatio_GetExportToExcelSource" />--%>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdPositionRatio" runat="server" AllowPaging="True"  DataKeyNames="ID,NOSTOCKCOUNT" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnPageIndexChanged="grdPositionRatio_PageIndexChanged" OnPageIndexChanging="grdPositionRatio_PageIndexChanging"
                        OnRowDataBound="grdPositionRatio_RowDataBound" CssClass="Grid" PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:BoundField DataField="ID" DataFormatString="" HeaderText="ID" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AREA_NAME" DataFormatString="" HeaderText="<%$ Resources:Lang, CommonB_AreaName %>"><%--區域名稱--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="240px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="POSCOUNT" DataFormatString="" HeaderText="<%$ Resources:Lang, CommonB_CpoctionNum %>"><%--儲位總數--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STOCKCOUNT" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmPositionStockRatioReport_STOCKCOUNT %>"><%--使用--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False"/>
                            </asp:BoundField>
                              <asp:BoundField DataField="NOSTOCKCOUNT" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmPositionStockRatioReport_NOSTOCKCOUNT %>"><%--未使用--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False"/>
                            </asp:BoundField>
                            <%-- <asp:TemplateField HeaderText="未使用">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlToNOSTOCKCOUNT_Info" ToolTip="查看区域未使用储位信息" runat="server"><%# Eval("NOSTOCKCOUNT")%></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="STOCKRATIO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmPositionStockRatioReport_STOCKRATIO %>"><%--使用率--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            
                        </Columns>
                    </asp:GridView>

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
                        //settingPad("<%= grdPositionRatio.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
