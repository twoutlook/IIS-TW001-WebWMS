<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPLCCommandQuery_Report.aspx.cs" Inherits="Apps_Report_FrmPLCCommandQuery_Report" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />

    <style type="text/css">
        body
        {
            padding: 0;
            font: 12px "宋体";
        }
        /*Tab1*/ #lib_Tab1
        {
            width: 100%;
            margin: 0 auto;
            padding: 0px;
            margin-top: 2px;
            margin-bottom: 2px;
        }
        /*Tab2*/ #lib_Tab2
        {
            width: 1000px;
            margin: 0 auto;
            padding: 0px;
            margin-top: 4px;
            margin-bottom: 5px;
        }

        .lib_tabborder
        {
            border: 1px solid #D5E3F0;
        }

        .lib_Menubox
        {
            height: 28px;
            line-height: 28px;
            position: relative;
        }



            .lib_Menubox ul
            {
                margin: 0px;
                padding: 0px;
                list-style: none;
                position: absolute;
                top: 3px;
                left: 0;
                margin-left: 10px;
                height: 25px;
                text-align: center;
                width: 1100px;
            }


            .lib_Menubox li
            {
                float: left;
                display: block;
                cursor: pointer;
                width: 115px;
                color: #949694;
                font-weight: bold;
                margin-right: 2px;
                height: 25px;
                line-height: 25px;
                background-color: #F0F3FA;
            }

                .lib_Menubox li.hover
                {
                    padding: 0px;
                    background: #fff;
                    width: 115px;
                    border-left: 1px solid #95C9E1;
                    border-top: 1px solid #95C9E1;
                    border-right: 1px solid #95C9E1;
                    color: #739242;
                    height: 25px;
                    line-height: 25px;
                }

        .lib_Contentbox
        {
            clear: both;
            margin-top: 0px;
            border-top: none;
            min-height: 50px;
            text-align: center;
            padding-top: 8px;
        }

        #topimg
        {
            margin: 0px auto;
            width: 1000px;
            margin-top: 3px;
        }

        #top img
        {
            width: 1000px;
            height: 55px;
            margin: 0px auto;
        }

        .divcontent
        {
            width: 1000px;
            margin: 0 auto;
            margin-top: 3px;
            margin-bottom: 4px;
            border: 1px solid #D5E3F0;
        }

            .divcontent .divAbuy
            {
                height: 50px;
                width: 200px;
                size: 16px;
                margin-top: 10px;
                margin-left: 10px;
            }

        #search
        {
            width: 1000px;
            margin: 0 auto;
            height: 50px;
            margin-top: 3px;
            border: 1px solid #D5E3F0;
        }

            #search .sControl
            {
                margin-top: 10px;
                margin-left: 5px;
                height: 25px;
                width: 580px;
            }

                #search .sControl .leftDate
                {
                    float: left;
                    width: 420px;
                }

                #search .sControl .rightBtn
                {
                    float: right;
                    width: 160px;
                }

            #search .btncss
            {
                width: 60px;
                height: 30px;
            }

            #search .btnSearch
            {
                width: 63px;
                height: 22px;
                background: url( "../Templets/newimages/search.gif" );
                border: 0;
                cursor: pointer;
            }

        .btnExpert
        {
            width: 63px;
            height: 22px;
            background: url( "../Templets/newimages/export.gif" );
            border: 0;
            cursor: pointer;
        }

        .dzfw
        {
            padding: 5px 15px;
            border: 1px solid #d5e3f0;
            overflow: hidden;
            width: 968px;
            margin: 10px 0;
        }

        .dzfw_title
        {
            float: left;
            margin: 15px 8px 0 0;
        }

        .dzfw_list
        {
            float: left;
            width: 448px;
        }

            .dzfw_list img
            {
                float: left;
                margin: 5px 5px 5px 12px;
            }

        .dzfw_title1
        {
            float: left;
            margin: 2px 8px 0 0;
        }
    </style>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.FrmReport_ReportManagement%>-&gt;PLC命令报表<%--命令查询报表--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:HiddenField ID="hTabIndex" runat="server" Value="1" />
    <asp:HiddenField ID="hTabIndex1" runat="server" Value="1" />
    <div id="lib_Tab1" style="height: 100%; width: 95%">
        <!--立库-->
        <div id="con_one_1">
            <table id="TabMain" style="height: 100%; width: 100%">
                <tr valign="top">
                    <td valign="top">
                        <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                            id="tabCondition">
                            <tr>
                                <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
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
                            <tr>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblposition" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cposition %>"></asp:Label>：<%--储位：--%>
                                </td>
                                <td style="width: 20%; white-space: nowrap;">
                                    <asp:TextBox ID="txtPosition" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblLineID" runat="server" Text="<%$ Resources:Lang, Common_LineID %>"></asp:Label>：<%--线别：--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtLineID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblstnNO" runat="server" Text="<%$ Resources:Lang, Common_SiteID %>"></asp:Label>：<%--站点：--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtstnNO" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblRemark" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG3 %>"></asp:Label>：<%--命令类型：--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:DropDownList ID="ddlCmdType" runat="server" Width="95%">
                                        <%--<asp:ListItem Value="">全部</asp:ListItem>                                                                
                                <asp:ListItem Value="入库">入库</asp:ListItem>
                                <asp:ListItem Value="出库">出库</asp:ListItem>
                                <asp:ListItem Value="返库">返库</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblTrnDateFrom" runat="server" Text="结束时间"></asp:Label>：<%--接收时间：--%></td>
                                <td style="width: 20%; white-space: nowrap;">
                                    <asp:TextBox ID="txtTrnDateFrom" runat="server" onKeyPress="event.returnValue=false"
                                        CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                        src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtTrnDateFrom','y-mm-dd',0);" /></td>
                                <td class="InputLabel" style="width: 13%">
                                    <%--命令状态：--%>
                                    &nbsp;<asp:Label ID="lblTrnDateTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                                </td>
                                <td style="width: 20%; white-space: nowrap;">
                                    <asp:TextBox ID="txtTrnDateTo" runat="server" onKeyPress="event.returnValue=false"
                                        CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                        src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtTrnDateTo','y-mm-dd',0);" /></td>
                            </tr>
                            <tr>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label1" runat="server" Text="CmdSno"></asp:Label>：
                                        &nbsp;</td>
                                <td style="width: 21%; white-space: nowrap;">
                                    <asp:TextBox ID="txtCmdSNo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox></td>
                                  <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label30" runat="server" Text="返回状态"></asp:Label>：
                                        &nbsp;</td>
                                <td style="width: 21%; white-space: nowrap;">
                                    <asp:TextBox ID="txtResult" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox></td>
                                <td class="InputLabel" style="width: 13%">&nbsp;</td>
                                <td style="width: 21%; white-space: nowrap;">&nbsp;</td>
                            </tr>

                            <tr>
                                <td colspan="6" style="text-align: right">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                            <asp:GridView ID="grdCMD" runat="server" AllowPaging="True" BorderColor="Teal"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                CssClass="Grid" PageSize="15">
                                <PagerSettings Visible="False" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                <RowStyle HorizontalAlign="Left" Wrap="False" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                <PagerStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                <Columns>
                                    <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG18 %>" Visible="false"><%--命令ID--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LineID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lineid %>">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="cmdSno" DataFormatString="" HeaderText="CmdSno">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cmdModeName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG3 %>"><%-- 命令类型--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>
                                    <%--命令类型--%>
                                    <asp:BoundField DataField="siteNo" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_SiteID %>"><%--站点--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="positionCode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG26 %>"><%--原始储位--%>
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="topositionCode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmTransaction_SNList_Msg5 %>">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" />
                                    </asp:BoundField>

                                    <%--时间--%>
                                    <asp:BoundField DataField="runTime" DataFormatString="" HeaderText="结束时间">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>


                                    <%--返回状态--%>

                                    <asp:BoundField DataField="Result" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG14%>">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>
                                    <%--命令完成Index--%>
                                    <asp:BoundField DataField="completeIndex" DataFormatString="" HeaderText="命令完成Index">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="runposition" DataFormatString="" HeaderText="走行位置">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SJJposition" DataFormatString="" HeaderText="升降位置">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="difftimes" DataFormatString="" HeaderText="执行时间(s)">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundField>


                                </Columns>
                            </asp:GridView>
                            <ul class="OneRowStyle">
                                <li>
                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                        FirstPageText="<%$ Resources:Lang, Common_FirstPage %>" LastPageText="<%$ Resources:Lang, Common_LastPage %>" NextPageText="<%$ Resources:Lang, Common_NextPage %>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                                        AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                    </webdiyer:AspNetPager>
                                </li>
                                <li>
                                    <div><%=Resources.Lang.Common_TotalPage%> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
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
        </div>


    </div>

    <script type="text/javascript">
     
    </script>
</asp:Content>
