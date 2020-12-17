<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmASRS_Report.aspx.cs" Inherits="FrmASRS_Report" 
    Title="--<%$Resources:Lang,FrmASRS_Report_content1 %>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%--线别工作量使用率报表--%>

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
    <%=Resources.Lang.InterFaceLog_Title02 %> <%--报表管理--%>&gt;<%=Resources.Lang.FrmASRS_Report_content1 %><%--线别工作量统计报表--%>
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
                            <img id="imgCollapse" alt="<%=Resources.Lang.Common_Fold %>"  src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
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
                            <asp:Label ID="lblDate" runat="server" Text="<%$Resources:Lang,Common_DateTime %>"></asp:Label>：<%--日期：--%>
                        </td>
                       <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDate" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDate','y-mm-dd',0);" />
                        </td>
                       <td class="InputLabel" style="width: 15%">
                            <asp:Label ID="Label1" runat="server" Text="<%$Resources:Lang,Common_LineID %>"></asp:Label>：<%--线别：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtLine" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
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
                            <HDTools:excelbutton runat="server" GridID="grdASRS" ID="btnExcel" ExcelName="<%$Resources:Lang,FrmASRS_Report_content1 %>">  <%--线别工作量统计报表--%>                            
                            </HDTools:excelbutton>
                          
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr valign="top">
            <td valign="top">            
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 620px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdASRS" runat="server" AllowPaging="True"  DataKeyNames="ASRSDATE" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"   OnRowDataBound="grdASRS_RowDataBound"                       
                        CssClass="Grid" PageSize="24" ShowFooter="true">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                          <%--  <asp:BoundField DataField="ID" DataFormatString="" HeaderText="ID" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>--%>
                              <asp:BoundField DataField="ASRSDATE" DataFormatString="" HeaderText="<%$Resources:Lang,Common_DateTime %>"><%--日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="240px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LineName" DataFormatString="" HeaderText="<%$Resources:Lang,Common_LineID %>"><%--线别--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="240px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StartTime" DataFormatString="" HeaderText="<%$Resources:Lang,FrmASRS_Report_StartTime %>"><%--开始时段--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EndTime" DataFormatString="" HeaderText="<%$Resources:Lang,FrmASRS_Report_EndTime %>"><%--结束时段--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False"/>
                            </asp:BoundField>
                             <asp:BoundField DataField="RKQty" DataFormatString="" HeaderText="<%$Resources:Lang,FrmASRS_Report_RKQty %>"><%--入库数--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False"/>
                            </asp:BoundField>
                             <asp:BoundField DataField="CKQty" DataFormatString="" HeaderText="<%$Resources:Lang,FrmASRS_Report_CKQty %>"><%--出库数--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False"/>
                            </asp:BoundField>
                             <asp:BoundField DataField="FKQty" DataFormatString="" HeaderText="<%$Resources:Lang,FrmASRS_Report_FKQty %>"><%--返库数--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False"/>
                            </asp:BoundField>
                             <asp:BoundField DataField="SUMQty" DataFormatString="" HeaderText="<%$Resources:Lang,FrmASRS_Report_SUMQty %>"><%--总数--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False"/>
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
                        //settingPad("<%= grdASRS.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
