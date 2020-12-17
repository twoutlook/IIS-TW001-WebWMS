<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" Title="--<%$ Resources:Lang, InterFaceLog_Title01%>"
    CodeFile="InterFaceLog.aspx.cs" Inherits="InterFaceLog" %>

<%--接口日志--%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
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
            height: 100%;
        }

        .tableCell {
            display: table;
            width: 100%;
        }

        .tdWordWarp {
            white-space: normal !important;
            word-wrap: break-word !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.InterFaceLog_Title02%>-&gt;<%= Resources.Lang.InterFaceLog_Title01%><%--报表管理-&gt;接口日志--%>
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
                        <th align="left" colspan="4">
                            <img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
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
                    <tr style="display: none">
                        <td colspan="6">
                            <style type="text/css;">
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
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="lblTYPE" runat="server" Text="<%$ Resources:Lang, InterFaceLog_lblTYPE%>"></asp:Label>：<%--接口名称--%>
                        </td>
                        <td style="width: 30%; white-space: nowrap;">
                            <asp:DropDownList ID="ddlTYPE" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 20%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, InterFaceLog_Label2%>"></asp:Label>：<%--返回結果--%>
                        </td>
                        <td style="width: auto; white-space: nowrap;" colspan="2">
                            <asp:TextBox ID="txtOUTRESULTS" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 15%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, InterFaceLog_Label7%>"></asp:Label>：<%--传入参数--%>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtINPARAMS" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 15%">
                            <asp:Label ID="lblTitle" runat="server" Text="<%$ Resources:Lang, Common_CreateDate%>"></asp:Label>： <%--创建日期--%>
                        </td>
                        <td style="width: 30%; white-space: nowrap;" colspan="2">
                            <asp:TextBox ID="txtDATEFrom" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" id="imgDATEFrom" runat="server" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDATEFrom','y-mm-dd',0);" />

                        </td>
                    </tr>

                    <tr>
                        <td class="InputLabel" style="width: 15%">
                            <asp:Label ID="Label1" runat="server" Text="ERPCODE："></asp:Label>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtERPCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 15%">
                            <asp:Label ID="lblDAUDITDATEToTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao%>" ForeColor="#3580C9"></asp:Label>：<%--到--%>
                            
                        </td>
                        <td style="width: 30%; white-space: nowrap;" colspan="2">
                            <asp:TextBox ID="txtDATETo" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" id="imgDATETo" runat="server" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDATETo','y-mm-dd',0);" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"></asp:Button>
                            <%--查询--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td>
                <div style="height: 550px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdInterLog" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="grdInterLog_RowDataBound" CssClass="Grid" PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:BoundField DataField="TYPEID" DataFormatString="" HeaderText="<%$ Resources:Lang, InterFaceLog_lblTYPE%>"><%--接口名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="10%" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="10%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ERPCODE" DataFormatString="" HeaderText="ERPCode" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="10%" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="10%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="BO" DataFormatString="" HeaderText="<%$ Resources:Lang, InterFaceLog_Label7%>"><%--传入参数--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="65%" />
                                <ItemStyle HorizontalAlign="left" Wrap="true" Width="65%" CssClass="tdWordWarp" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ERRORMSG" DataFormatString="" HeaderText="<%$ Resources:Lang, InterFaceLog_Label2%>"><%--返回结果--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="15%" />
                                <ItemStyle HorizontalAlign="left" Wrap="true" Width="15%" CssClass="tdWordWarp" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CREATEDATE" DataFormatString="{0:yyyy-MM-dd}" HeaderText="<%$ Resources:Lang, Common_CreateDate%>"><%--创建日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="10%" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="10%" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <ul class="OneRowStyle">
                        <li>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList"
                                PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                            </webdiyer:AspNetPager>
                        </li>
                        <li>
                            <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                        </li>
                    </ul>
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdInterLog.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
