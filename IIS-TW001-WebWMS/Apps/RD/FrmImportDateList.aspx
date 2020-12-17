<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--工单信息维护" CodeFile="FrmImportDateList.aspx.cs" Inherits="RD_FrmImportDateList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
     <%=Resources.Lang.CommonB_OUTBILLManagement%> <%--出库管理--%>-&gt;<%=Resources.Lang.CommonB_WorkOrderINFO%><%--工单信息维护--%>
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
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, FrmUPGDDatat_MSG12 %>"></asp:Label>：<%--工单号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtWO" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, FrmUPGDDatat_lblDCREATETIMEFromFrom %>"></asp:Label>：<%--备料日期：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDateFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDateFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：:
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDateTo" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText"
                                Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDateTo','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmINASNEMTList_MSG5 %>"></asp:Label>：<%--创建日期：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtCreateFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：:
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtCreateTo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateTo','y-mm-dd',0);" />
                        </td>
                           <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmUPGDDatat_SHIFT %>">:</asp:Label>：<%--班别：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtShift" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmUPGDDatat_SPECIAL %>"></asp:Label>：<%--特殊：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtSpecial" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmUPGDDatat_LINEBODY %>"></asp:Label>：<%--线体：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtLine" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmUPGDDatat_DEPARTMENTNO %>"></asp:Label>：<%--部门编码：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDepartmentno" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmUPGDDatat_Label7 %>"></asp:Label>：<%--计算工时：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:DropDownList ID="ddlIsCountJobTime" runat="server" Width="95%">
                                <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="Y">是</asp:ListItem>
                                <asp:ListItem Value="N">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, FrmUPGDDatat_Label8 %>"></asp:Label>：<%--上线日期：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtOnLineTimeBeg" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtOnLineTimeBeg','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：:
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtOnLineTimeEnd" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtOnLineTimeEnd','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right;">
                            <asp:Button ID="btnUp" runat="server" CssClass="ButtonExcel" Text="<%=Resources.Lang.FrmUPGDData_MSG3%>" ><%--上传--%>
                            </asp:Button>&nbsp;
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--<tr valign="top">
            <td valign="top" align="left">
                &nbsp;
                <asp:FileUpload ID="fuFile" runat="server" />
                &nbsp;<asp:Button ID="btnNew" runat="server" CssClass="ButtonExcel" Text="上传" OnClick="btnNew_Click">
                </asp:Button>&nbsp;<asp:Button ID="btnDELMSG" OnClick="btnDelete_Click" runat="server"
                    Text="清空" ToolTip="清空日志" CssClass="ButtonDel" />
            </td>
        </tr>--%>
    
     <%--   <tr valign="top">
            <td valign="top">
                <cc1:DataGridNavigator3 ID="grdNavigatorINASN" runat="server" GridID="grdINASN" ShowPageNumber="false"
                    ExcelName="INASN" IsDbPager="True" GetExportToExcelSource="grdNavigatorINASN_GetExportToExcelSource" />
            </td>
        </tr>--%>
        <tr>
            <td>
                <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdINASN" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                       
                        OnRowDataBound="grdINASN_RowDataBound" CssClass="Grid" PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:BoundField DataField="WO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmUPGDDatat_MSG12 %>"><%--工单号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DEPARTMENTNO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmUPGDDatat_DEPARTMENTNO %>"><%--部门编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WO_QTY" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmUPGDDatat_MSG15 %>"><%--工单量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ISCOUNTJOBTIME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmUPGDDatat_Label7 %>"><%--计算工时--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SPECIAL" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmUPGDDatat_SPECIAL %>"><%--特殊--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LINEBODY" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmUPGDDatat_LINEBODY %>"><%--线体--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SHIFT" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmUPGDDatat_SHIFT %>"><%--班别--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="SEC_CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmUPGDDatat_SEC_CINVCODE %>"><%--二次用料--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ONLINETIME" DataFormatString="{0:yyyy-MM-dd}" HeaderText="<%$ Resources:Lang, FrmUPGDDatat_Label8 %>"><%--上线日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="START_DATE" DataFormatString="{0:yyyy-MM-dd}" HeaderText="<%$ Resources:Lang, FrmUPGDDatat_lblDCREATETIMEFromFrom %>"><%--备料日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="MODELS"  HeaderText="<%$ Resources:Lang, FrmUPGDDatat_MSG14 %>"><%--机种--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATE_OWNER"  HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle28 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATE_TIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmINASNEMTList_MSG5 %>"><%--创建日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False"   Width="80px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="LAST_UPD_OWNER"  HeaderText="<%$ Resources:Lang, FrmImportDateList_LAST_UPD_OWNER %>"><%--最后修改人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LAST_UPD_TIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmImportDateList_LAST_UPD_TIME %>"><%--最后修改时间--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px"/>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                     <ul class="OneRowStyle" >
                    <li>
                        <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                            FirstPageText="<%$ Resources:Lang, Common_FirstPage%>" LastPageText="<%$ Resources:Lang, Common_LastPage%>" NextPageText="<%$ Resources:Lang, Common_NextPage%>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                        </webdiyer:aspnetpager>
                    </li>
                    <li>
                        <div><%=Resources.Lang.Common_TotalPage %> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_TotalPage1 %></div>
                    </li>
                </ul>
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdINASN.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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

            function CheckDel() {
                var number = 0;
                var controls = document.getElementById("<%=grdINASN.ClientID %>").getElementsByTagName("input");

                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
                if (number == 0) {
                    alert("<%=Resources.Lang.FrmDispatchUnitList_MsgTitle8 %>");
                    return false;
                }
                if (confirm("<%= Resources.Lang.FrmDispatchUnitList_MsgTitle9 %>")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
    </table>
</asp:Content>
