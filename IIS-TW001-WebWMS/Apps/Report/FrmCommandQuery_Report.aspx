<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Apps/DefaultMasterPage.master" CodeFile="FrmCommandQuery_Report.aspx.cs" Inherits="Apps_Report_FrmCommandQuery_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />

    <style type="text/css">
        body {
            padding: 0;
            font: 12px "宋体";
        }
        /*Tab1*/ #lib_Tab1 {
            width: 100%;
            margin: 0 auto;
            padding: 0px;
            margin-top: 2px;
            margin-bottom: 2px;
        }
        /*Tab2*/ #lib_Tab2 {
            width: 1000px;
            margin: 0 auto;
            padding: 0px;
            margin-top: 4px;
            margin-bottom: 5px;
        }

        .lib_tabborder {
            border: 1px solid #D5E3F0;
        }

        .lib_Menubox {
            height: 28px;
            line-height: 28px;
            position: relative;
        }



            .lib_Menubox ul {
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


            .lib_Menubox li {
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

                .lib_Menubox li.hover {
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

        .lib_Contentbox {
            clear: both;
            margin-top: 0px;
            border-top: none;
            min-height: 50px;
            text-align: center;
            padding-top: 8px;
        }

        #topimg {
            margin: 0px auto;
            width: 1000px;
            margin-top: 3px;
        }

        #top img {
            width: 1000px;
            height: 55px;
            margin: 0px auto;
        }

        .divcontent {
            width: 1000px;
            margin: 0 auto;
            margin-top: 3px;
            margin-bottom: 4px;
            border: 1px solid #D5E3F0;
        }

            .divcontent .divAbuy {
                height: 50px;
                width: 200px;
                size: 16px;
                margin-top: 10px;
                margin-left: 10px;
            }

        #search {
            width: 1000px;
            margin: 0 auto;
            height: 50px;
            margin-top: 3px;
            border: 1px solid #D5E3F0;
        }

            #search .sControl {
                margin-top: 10px;
                margin-left: 5px;
                height: 25px;
                width: 580px;
            }

                #search .sControl .leftDate {
                    float: left;
                    width: 420px;
                }

                #search .sControl .rightBtn {
                    float: right;
                    width: 160px;
                }

            #search .btncss {
                width: 60px;
                height: 30px;
            }

            #search .btnSearch {
                width: 63px;
                height: 22px;
                background: url( "../Templets/newimages/search.gif" );
                border: 0;
                cursor: pointer;
            }

        .btnExpert {
            width: 63px;
            height: 22px;
            background: url( "../Templets/newimages/export.gif" );
            border: 0;
            cursor: pointer;
        }

        .dzfw {
            padding: 5px 15px;
            border: 1px solid #d5e3f0;
            overflow: hidden;
            width: 968px;
            margin: 10px 0;
        }

        .dzfw_title {
            float: left;
            margin: 15px 8px 0 0;
        }

        .dzfw_list {
            float: left;
            width: 448px;
        }

            .dzfw_list img {
                float: left;
                margin: 5px 5px 5px 12px;
            }

        .dzfw_title1 {
            float: left;
            margin: 2px 8px 0 0;
        }
    </style>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.FrmReport_ReportManagement%>-&gt;<%=Resources.Lang.FrmCommandQuery_Report_MSG1%><%--命令查询报表--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:HiddenField ID="hTabIndex" runat="server" Value="1" />
    <asp:HiddenField ID="hTabIndex1" runat="server" Value="1" />
    <div id="lib_Tab1" style="height: 100%; width: 95%">
        <div class="lib_Menubox lib_tabborder">
            <ul>
                <li id="tabLK" runat="server" class="one1" onclick="WMSUI.SetTopTab('one',1,5)"><%= Resources.Lang.Common_Worktype1 %></li><%--立库--%>
                <li id="tabAGV" runat="server" class="one2" onclick="WMSUI.SetTopTab('one',2,5)">AGV</li><%--AGV--%>
                <li id="tabRGV" runat="server" class="one3" onclick="WMSUI.SetTopTab('one',3,5)">RGV</li><%--RGV--%>
                <li id="tabCar" runat="server" class="one4" onclick="WMSUI.SetTopTab('one',4,5)">台车</li><%--台车--%>
            </ul>
        </div>
        <div class="lib_Contentbox lib_tabborder">
            <!--立库-->
            <div id="con_one_1" style="display:none;">
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
                                        <asp:Label ID="lblPackageNo" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG2 %>"></asp:Label>：<%--箱号/栈板号：--%>
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:TextBox ID="txtPackageNo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
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
                                        <asp:Label ID="lblCticketCode" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG7 %>"></asp:Label>：<%--单据号: --%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCticketCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblRemark" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG3 %>"></asp:Label>：<%--命令类型：--%>
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:DropDownList ID="ddlRemark" runat="server" Width="95%">
                                            <%--<asp:ListItem Value="">全部</asp:ListItem>                                                                
                                <asp:ListItem Value="入库">入库</asp:ListItem>
                                <asp:ListItem Value="出库">出库</asp:ListItem>
                                <asp:ListItem Value="返库">返库</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCmdSts" runat="server" Text="<%$ Resources:Lang,FrmCommandQuery_Report_MSG10 %>"></asp:Label>：<%--命令状态：--%>
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:DropDownList ID="ddlCmdSts" runat="server" Width="95%">
                                            <%--<asp:ListItem Value="">全部</asp:ListItem>                                                    
                                <asp:ListItem Value="未执行">未执行</asp:ListItem>
                                <asp:ListItem Value="执行中">执行中</asp:ListItem>
                                <asp:ListItem Value="已完成">已完成</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblTrnDateFrom" runat="server" Text="<%$ Resources:Lang,FrmCommandQuery_Report_MSG12%>"></asp:Label>：<%--接收时间：--%>
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="txtTrnDateFrom" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtTrnDateFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblTrnDateTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="txtTrnDateTo" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtTrnDateTo','y-mm-dd',0);" />
                                    </td>
                                      <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label29" runat="server" Text="CmdSno"></asp:Label>：
                                        &nbsp;</td>
                                <td style="width: 21%; white-space: nowrap;">
                                    <asp:TextBox ID="txtCmdSNo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblEndTimeFrom" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG11 %>"></asp:Label>：<%--完成时间：--%>
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="txtEndTimeFrom" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtEndTimeFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblEndTimeTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="txtEndTimeTo" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtEndTimeTo','y-mm-dd',0);" />
                                    </td>
                                       <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label30" runat="server" Text="返回状态"></asp:Label>：
                                        &nbsp;</td>
                                <td style="width: 21%; white-space: nowrap;">
                                    <asp:TextBox ID="txtResult" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox></td>
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
                                        <asp:BoundField DataField="WmsTskId" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG18 %>"><%--命令ID--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="cmdSno" DataFormatString="" HeaderText="CmdSno">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRT" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG19 %>"><%--优先级--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>


                                        <asp:BoundField DataField="PACKAGENO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG2 %>"><%--箱号/栈板号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LineID" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_LineID %>"><%--线别--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="stnNO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_SiteID %>"><%--站点--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Loc" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG26 %>"><%--原始储位--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="NewLoc" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmTransaction_SNList_Msg5 %>"></asp:BoundField>
                                        <asp:BoundField DataField="CTICKETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG7 %>"><%--单据号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ModeName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG3 %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <%--命令类型--%>
                                         <asp:BoundField DataField="REMARK" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO %>"><%--备注--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CmdStsName" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG10%>"><%--命令状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="TaskNO" DataFormatString="" HeaderText="TaskNO">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="TrnDate" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG12%>"><%--接收时间--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActTime" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG13%>"><%--开始时间--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EndTime" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG11 %>"><%--完成时间--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <%--返回状态--%>
                                        <asp:BoundField DataField="Result" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG14%>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="MSG" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmINASNEMTList_MSG3%>"><%--信息--%>
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
            <!--AGV-->
            <div id="con_one_2" style="display:none;">
                <table id="Table1" style="height: 100%; width: 100%">
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                id="Table2">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                        height="11" alt="" runat="server" id="Img1" />
                                        <%=Resources.Lang.Common_JSCeria%>
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                                        </script>
                                        <img id="img2" alt="<%=Resources.Lang.Common_Fold%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                            onclick="CollapseCondition('../../');return false;" />
                                    </th>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">

                                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG2 %>"></asp:Label>：<%--箱号/栈板号：--%>
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:TextBox ID="txtAGVPk" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG19 %>"></asp:Label>：<%--命令编号：--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtReqCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG20 %>"></asp:Label>：<%--任务编号：--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="TxtTaskCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG20 %> "></asp:Label>：<%--任务类型:--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="TxtTaskType" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, Common_PartnumNO %>"></asp:Label>：
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtValue" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang,FrmCommandQuery_Report_MSG10%>"></asp:Label>：<%--命令状态：--%>
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:DropDownList ID="ddlstatusAGV" runat="server" Width="95%">
                                            <%--<asp:ListItem Value=""><%=Resources.Lang.Common_ALL%></asp:ListItem>                                                    
                                <asp:ListItem Value="未执行">未执行</asp:ListItem>
                                <asp:ListItem Value="执行中">执行中</asp:ListItem>
                                <asp:ListItem Value="已完成">已完成</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%--     <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="接收时间："></asp:Label>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtreqDatefROM" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtreqDatefROM','y-mm-dd',0);" />                       
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtreqDateTo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtreqDateTo','y-mm-dd',0);" />                       
                        </td>                                              
                    </tr>--%>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG25 %>"></asp:Label>：<%--创建时间：--%>
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="txtAGVCdateFrom" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtAGVCdateFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="txtAGVCdateTo" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtAGVCdateTo','y-mm-dd',0);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="text-align: right">
                                        <asp:Button ID="btnSearchAGV" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearchAGV_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div2">
                                <asp:GridView ID="grdAGV" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    CssClass="Grid" PageSize="15">
                                    <PagerSettings Visible="False" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <RowStyle HorizontalAlign="Left" Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                    <Columns>
                                        <asp:BoundField DataField="reqcode" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG19 %>"><%--命令编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="data" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG2 %>"><%--箱号/栈板号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="taskcode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG20 %>"><%--任务编号--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tasktyp" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG21 %>"><%--任务类型--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="interfacename" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG22 %>"><%--接口名--%>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="agvcode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG23 %>"><%--AGV编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="value" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PartnumNO %>"></asp:BoundField>
                                        <asp:BoundField DataField="status" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG10%>"><%--命令状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vendorname" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG24 %>"></asp:BoundField>
                                        <%--供应商名--%>
                                        <asp:BoundField DataField="reqtime" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG12%>"><%--接收时间--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="createtime" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle18 %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="createOwner" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle28 %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="Result" DataFormatString="" HeaderText="返回状态">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>--%>
                                    </Columns>
                                </asp:GridView>
                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager2_PageChanged"
                                            FirstPageText="<%$ Resources:Lang, Common_FirstPage %>" LastPageText="<%$ Resources:Lang, Common_LastPage %>" NextPageText="<%$ Resources:Lang, Common_NextPage %>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                        <div><%=Resources.Lang.Common_TotalPage%> <%=AspNetPager2.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
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
            <!--RGV-->
            <div id="con_one_3" style="display:none;">
                <table id="Table3" style="height: 100%; width: 100%">
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                id="Table4">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                        height="11" alt="" runat="server" id="Img3" />
                                        <%=Resources.Lang.Common_JSCeria%>
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                                        </script>
                                        <img id="img4" alt="<%=Resources.Lang.Common_Fold%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                            onclick="CollapseCondition('../../');return false;" />
                                    </th>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG2 %>"></asp:Label>：<%--箱号/栈板号：--%>
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:TextBox ID="txtRGVPK" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Lang, Common_LineID %>"></asp:Label>：<%--线别：--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtRGVlINEID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Lang, Common_SiteID %>"></asp:Label>：<%--站点：--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtRGVSiteID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG7 %> "></asp:Label>：<%--单据号:--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtRGVCticketCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG3 %>"></asp:Label>：<%--命令类型--%>
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:DropDownList ID="ddlRGVtype" runat="server" Width="95%">
                                            <%--<asp:ListItem Value="">全部</asp:ListItem>                                                                
                                <asp:ListItem Value="入库">入库</asp:ListItem>
                                <asp:ListItem Value="出库">出库</asp:ListItem>
                                <asp:ListItem Value="返库">返库</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Lang,FrmCommandQuery_Report_MSG10%>"></asp:Label>：<%--命令状态：--%>
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:DropDownList ID="ddlRGVCstatus" runat="server" Width="95%">
                                            <%--<asp:ListItem Value="">全部</asp:ListItem>                                                    
                                <asp:ListItem Value="未执行">未执行</asp:ListItem>
                                <asp:ListItem Value="执行中">执行中</asp:ListItem>
                                <asp:ListItem Value="已完成">已完成</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Lang,FrmCommandQuery_Report_MSG12%>"></asp:Label>：<%--接收时间：--%>
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="ddlRGV_trnDateFrom" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_ddlRGV_trnDateFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="ddlRGV_trnDateTo" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_ddlRGV_trnDateTo','y-mm-dd',0);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG11 %>"></asp:Label>：<%--完成时间：--%>
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="txtRGV_EndTimeFrom" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtRGV_EndTimeFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label20" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="txtRGV_EndTimeTo" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtRGV_EndTimeTo','y-mm-dd',0);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="text-align: right">
                                        <asp:Button ID="btnSearchRGV" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearchRGV_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div3">
                                <asp:GridView ID="grdRGV" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    CssClass="Grid" PageSize="15">
                                    <PagerSettings Visible="False" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <RowStyle HorizontalAlign="Left" Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                    <Columns>
                                        <asp:BoundField DataField="WmsTskId" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG18 %>"><%--命令ID--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cmdSno" DataFormatString="" HeaderText="CmdSno">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRT" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG19 %>"><%--优先级--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PACKAGENO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG2 %>"><%--箱号/栈板号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LineID" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_LineID %>"><%--线别--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="stnNO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_SiteID %>"><%--站点--%>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Loc" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG5 %>"><%--开始站点--%>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NewLoc" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG6 %>"><%--目的站点--%>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="CTICKETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG7 %>"><%--单据号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ModeName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG3 %>"></asp:BoundField>
                                        <%--命令类型--%>
                                         <asp:BoundField DataField="REMARK" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO %>"><%--备注--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CmdStsName" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG10%>"><%--命令状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TrnDate" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG12%>"><%--接收时间--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActTime" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG13%>"><%--开始时间--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EndTime" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG11 %>"><%--完成时间--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <%--返回状态--%>
                                        <%--<asp:BoundField DataField="Result" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG14%>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:BoundField>--%>
                                    </Columns>
                                </asp:GridView>
                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:AspNetPager ID="AspNetPager3" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager3_PageChanged"
                                            FirstPageText="<%$ Resources:Lang, Common_FirstPage %>" LastPageText="<%$ Resources:Lang, Common_LastPage %>" NextPageText="<%$ Resources:Lang, Common_NextPage %>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                        <div><%=Resources.Lang.Common_TotalPage%> <%=AspNetPager3.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
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
            <!--台车-->
            <div id="con_one_4" style="display:none;">
                <table id="Table5" style="height: 100%; width: 100%">
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                id="Table6">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                        height="11" alt="" runat="server" id="Img5" />
                                        <%=Resources.Lang.Common_JSCeria%>
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                                        </script>
                                        <img id="img6" alt="<%=Resources.Lang.Common_Fold%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                            onclick="CollapseCondition('../../');return false;" />
                                    </th>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG2 %>"></asp:Label>：<%--箱号/栈板号：--%>
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:TextBox ID="txtCARPK" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, Common_LineID %>"></asp:Label>：<%--线别：--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCARlINEID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Lang, Common_SiteID %>"></asp:Label>：<%--站点：--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCARSiteID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG7 %> "></asp:Label>：<%--单据号:--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCARCticketCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG3 %>"></asp:Label>：<%--命令类型--%>
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:DropDownList ID="ddlCARtype" runat="server" Width="95%">
                                            <%--<asp:ListItem Value="">全部</asp:ListItem>                                                                
                                <asp:ListItem Value="入库">入库</asp:ListItem>
                                <asp:ListItem Value="出库">出库</asp:ListItem>
                                <asp:ListItem Value="返库">返库</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Lang,FrmCommandQuery_Report_MSG10%>"></asp:Label>：<%--命令状态：--%>
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:DropDownList ID="ddlCARCstatus" runat="server" Width="95%">
                                            <%--<asp:ListItem Value="">全部</asp:ListItem>                                                    
                                <asp:ListItem Value="未执行">未执行</asp:ListItem>
                                <asp:ListItem Value="执行中">执行中</asp:ListItem>
                                <asp:ListItem Value="已完成">已完成</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label25" runat="server" Text="<%$ Resources:Lang,FrmCommandQuery_Report_MSG12%>"></asp:Label>：<%--接收时间：--%>
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="ddlCAR_trnDateFrom" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_ddlCAR_trnDateFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label26" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="ddlCAR_trnDateTo" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_ddlCAR_trnDateTo','y-mm-dd',0);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label27" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG11 %>"></asp:Label>：<%--完成时间：--%>
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="txtCAR_EndTimeFrom" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCAR_EndTimeFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label28" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="txtCAR_EndTimeTo" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="middle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCAR_EndTimeTo','y-mm-dd',0);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="text-align: right">
                                        <asp:Button ID="btnSearchCAR" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearchCAR_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div4">
                                <asp:GridView ID="grdCAR" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    CssClass="Grid" PageSize="15">
                                    <PagerSettings Visible="False" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <RowStyle HorizontalAlign="Left" Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                    <Columns>
                                        <asp:BoundField DataField="WmsTskId" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG18 %>"><%--命令ID--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cmdSno" DataFormatString="" HeaderText="CmdSno">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRT" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG19 %>"><%--优先级--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PACKAGENO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG2 %>"><%--箱号/栈板号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LineID" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_LineID %>"><%--线别--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="stnNO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_SiteID %>"><%--站点--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Loc" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG5 %>"><%--开始站点--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NewLoc" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG6 %>"><%--目的站点--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <%--单据号--%>
                                       <%-- <asp:BoundField DataField="CTICKETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG7 %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>--%>
                                         <asp:BoundField DataField="ModeName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG3 %>">  <%--命令类型--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="REMARK" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO %>"><%--备注--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                      
                                        <asp:BoundField DataField="CmdStsName" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG10%>"><%--命令状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TrnDate" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG12%>"><%--接收时间--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActTime" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG13%>"><%--开始时间--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EndTime" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG11 %>"><%--完成时间--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <%--返回状态--%>
                                       <%-- <asp:BoundField DataField="Result" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmCommandQuery_Report_MSG14%>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:BoundField>--%>
                                    </Columns>
                                </asp:GridView>
                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:AspNetPager ID="AspNetPager4" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager4_PageChanged"
                                            FirstPageText="<%$ Resources:Lang, Common_FirstPage %>" LastPageText="<%$ Resources:Lang, Common_LastPage %>" NextPageText="<%$ Resources:Lang, Common_NextPage %>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                        <div><%=Resources.Lang.Common_TotalPage%> <%=AspNetPager4.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
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
    </div>

    <script type="text/javascript">
        var WMSUI = {
            FormFed: {
                TopTab: $("#<%= hTabIndex.ClientID %>")
            },
            Init: function () {
                var indexT = this.FormFed.TopTab.val();
                $(".one" + indexT).first().click();
            },
            SetTopTab: function (name, cursel, n) {
                var _self = WMSUI;
                _self.FormFed.TopTab.val(cursel);//将当前选中的tab项赋值给影藏控件
                for (i = 1; i <= n; i++) {
                    var tabitem = $("." + name + i).first();
                    var con = $("#con_" + name + "_" + i);
                    if (i == cursel) {
                        tabitem.addClass("hover");
                        con.show();
                    } else {
                        tabitem.removeClass("hover");
                        con.hide();
                    }
                }
            }
        };
        WMSUI.Init();
    </script>
</asp:Content>

