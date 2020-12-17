<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="FrmPositionStockByLineReport.aspx.cs" Inherits="Apps_Report_FrmPositionStockByLineReport"
    Title="--<%$ Resources:Lang,CommonB_FrmPositionStockByLineReportName %>" %>
<%--线别库位使用率报表--%>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="DreamTek.ASRS.Business" namespace="DreamTek.ASRS.Business" tagprefix="HDTools" %>
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
   <%=Resources.Lang.InterFaceLog_Title02 %> <%--报表管理--%>&gt;<%=Resources.Lang.CommonB_FrmPositionStockByLineReportName %><%--线别库位使用率报表--%>
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
                            <%=Resources.Lang.Common_JSCeria %> <%--检索条件--%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%=Resources.Lang.Common_Fold %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" /><%--折叠--%>
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
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang,Common_LineID %>"></asp:Label>：<%--线别：--%>
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtLine" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
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
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,Common_btnSearch %>" OnClick="btnSearch_Click"><%--查询--%>
                            </asp:Button>
                            &nbsp;                                                
                            <HDTools:excelbutton runat="server" GridID="grdPositionRatio" ID="btnExcel" ExcelName="<%$ Resources:Lang,CommonB_FrmPositionStockByLineReportName %>"><%--线别库位使用率报表--%>
                                 <BlockList>
                                <HDTools:Block Key="<%$ Resources:Lang,CommonB_UNusedNum %>" value="NOSTOCKCOUNT" seq="4" /><%--未使用数--%>
                                </BlockList>
                            </HDTools:excelbutton>
                           <%-- <ExcelBtn:OutPutButton ID="OutPutButton1" runat="server" ExcelName="PositionRatio" GridID="grdPositionRatio"
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
                    <asp:GridView ID="grdPositionRatio" runat="server" AllowPaging="True"  DataKeyNames="LINE,NOSTOCKCOUNT" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"   OnRowDataBound="grdPositionRatio_RowDataBound"                       
                        CssClass="Grid" PageSize="15">
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
                            <asp:BoundField DataField="line" DataFormatString="" HeaderText="<%$ Resources:Lang,Common_LineID %>"><%--线别--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="240px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="POSCOUNT" DataFormatString="" HeaderText="<%$ Resources:Lang,CommonB_CpoctionNum %>"><%--储位总数--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STOCKCOUNT" DataFormatString="" HeaderText="<%$ Resources:Lang,CommonB_UsedNum %>"><%--使用数--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False"/>
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="<%$ Resources:Lang,CommonB_UNusedNum %>"><%--未使用数--%>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlToNOSTOCKCOUNT_Info" ToolTip="<%$ Resources:Lang,CommonB_CheckCpotionInfo %>" runat="server"><%# Eval("NOSTOCKCOUNT")%></asp:HyperLink><%--查看区域未使用储位信息--%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="STOCKRATIO" DataFormatString="" HeaderText="<%$ Resources:Lang,CommonB_usedPercent %>"><%--使用率--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            
                        </Columns>
                    </asp:GridView>

                     <ul class="OneRowStyle">
                        <li>
                            <webdiyer:aspnetpager id="AspNetPager1" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="AspNetPager1_PageChanged"
                                firstpagetext="<%$ Resources:Lang, Common_FirstPage %>" lastpagetext="<%$ Resources:Lang, Common_LastPage %>" nextpagetext="<%$ Resources:Lang, Common_NextPage %>" prevpagetext="<%$ Resources:Lang, Common_PrePage %>" showpageindexbox="Never"
                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false">
                            </webdiyer:aspnetpager>
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
