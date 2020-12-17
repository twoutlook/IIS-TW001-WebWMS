<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmUPGDDatat.aspx.cs" Inherits="FrmUPGDDatat"
    Title="--111" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <style type="text/css">
        .spanT
        {
            font-size: 18px;
            color: #3580C9;
        }

        img
        {
            border: 0px;
        }

        .pD
        {
            margin: 0;
        }

            .pD span
            {
                font-size: 14px;
                color: #000;
            }

        div1, div2
        {
            width: 100%;
        }

            div1 table, div2 table
            {
                width: 100%;
            }

        .error
        {
            color: red;
            font-weight: bold;
        }

        #div1
        {
            display: block;
        }

        #div2
        {
            display: none;
        }

        .ButtonExcelError
        {
            border-right: 0px;
            border-top: 0px;
            font-size: 12px;
            font-weight: bold;
            border-left: 0px;
            cursor: hand;
            border-bottom: 0px;
            padding-left: 22px;
            padding-top: 2px;
            font-family: "Arial", "Helvetica", "sans-serif";
            height: 24px;
            color: #3580c9;
            background-image: url(../../Layout/CSS/LG/Images/in_s_bg_excel_error.gif);
            width: 120px;
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            //   ShowDiv(1);
        }

        function ShowDiv(isScurre) {
            if (isScurre == 0) {
                document.getElementById("div1").style.display = "none";
                document.getElementById("div2").style.display = "block";
            }
            if (isScurre == 1) {
                document.getElementById("div1").style.display = "block";
                document.getElementById("div2").style.display = "none";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
   <%=Resources.Lang.CommonB_OUTBILLManagement%> <%--出库管理--%>-&gt;<%=Resources.Lang.CommonB_WorkOrderINFO%><%--工单信息维护--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <div id="div1" runat="server">
        <asp:HiddenField runat="server" ID="IsUp" Value="1"/>
        <table class="InputTable" cellspacing="8" cellpadding="1" width="100%" border="1"
            runat="server" id="TabMain1">
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
                <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                    height="11" alt="" runat="server" id="titleImg" />
                    <%=Resources.Lang.FrmUPGDData_MSG4%><%--工单数据导入--%>
                </th>

            </tr>
            <tr valign="top">
                <td valign="top" align="left">
                    <asp:FileUpload ID="fuFile" runat="server" />
                </td>
            </tr>
            <tr class="trT">
                <td>
                    <asp:Button ID="btnNew" runat="server" CssClass="ButtonExcel" Text="<%=Resources.Lang.FrmUPGDData_MSG3%>"
                        OnClick="btnUP_Click"></asp:Button><%--上传--%>
                    &nbsp;&nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>" CausesValidation="false"  />
                </td>
            </tr>
            <tr>
                <td>
                    <p><span class="spanT"><%=Resources.Lang.FrmUPGDData_MSG5%>：<%--模版下载：--%></span></p>
                    <a href="/ExcelTemplate/工单信息维护.xls">&nbsp<img alt="<%= Resources.Lang.FrmUPGDData_MSG2%>" src="../../Layout/Css/LG/Images/excl.jpg" height="47px" width="43px" /><%--下载模版--%>
                        <br />
                        <%=Resources.Lang.CommonB_WorkOrderINFO%><%--工单信息维护--%></a>
                </td>
            </tr>
            <tr>
                <td>
                    <p><span class="spanT"><%=Resources.Lang.FrmUPGDDatat_MSG1%>：<%--导入说明：--%></span></p>
                    <p class="pD"><span>1、<%=Resources.Lang.FrmUPGDDatat_MSG2%><%--导入文件必须按照模板格式填写，模板文件可从本页面下载--%></span></p>
                    <p class="pD"><span>2、<%=Resources.Lang.FrmUPGDDatat_MSG3%><%--导入数据量较大时，可能需要时间较长，请耐心等待--%></span></p>
                    <p class="pD"><span>3、<%=Resources.Lang.FrmUPGDDatat_MSG4%><%--导入过程中若遇到数据格式错误或其他不符时，系统将记录该记录并在导入完成后统一返回--%></span></p>
                    <p class="pD"><span>4、<%=Resources.Lang.FrmUPGDDatat_MSG5%><%--导入过程中的错误记录并不影响后续正确数据的导入，您可以根据反馈数据修订异常数据后重新上传--%></span></p>
                </td>
            </tr>
            <tr valign="top">
                <td valign="top" class="InputLabel_O" id="tdOut" runat="server" style="color: red;"></td>
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
        </table>
    </div>
    <div id="div2"  runat="server">

        <table id="TabMain" style="height: 100%; width: 95%" >
            <tr>
                <td style="color: Black; font-family: Arial Normal, Arial; font-weight: 400;"><%=Resources.Lang.FrmUPGDDatat_MSG6%>：<%--本次导入数据量总计：--%><asp:Label runat="server" ID="lblTotalCount"></asp:Label>条<br />
                    <%=Resources.Lang.FrmUPGDDatat_MSG7%>：<%--正确数据：--%><asp:Label runat="server" ID="lblOKCount" Style="color: Blue;"></asp:Label><%=Resources.Lang.FrmUPGDDatat_MSG11%><%--条--%><br />
                    <%=Resources.Lang.FrmUPGDDatat_MSG8%>：<%--异常数据记录：--%><asp:Label runat="server" ID="lblNGCount" Style="color: Red;"></asp:Label><%=Resources.Lang.FrmUPGDDatat_MSG11%><%--条--%><br />
                    <%=Resources.Lang.FrmUPGDDatat_MSG9%>：<%--异常数据记录如下：--%><br />
                </td>

                <td>
                    <asp:Button ID="btnBack_D" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>" OnClick="btnBack_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" CssClass="ButtonExcelError" Text="<%$ Resources:Lang,FrmUPGDDatat_MSG10%>"
                        CausesValidation="false" OnClick="btnExport_Click" />&nbsp;&nbsp;<%--错误信息导出--%>
                    <asp:Button ID="btnSelect_Sucess" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, CommonB_View %>" /><%--查看--%>
                    
                   
                </td>
            </tr>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <tr valign="top">
                        <td valign="top" colspan="2">

                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="tabCondition" style="display: none;">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                        height="11" alt="" runat="server" id="Img1" />
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
                                        <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDateFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：:
                                    </td>
                                    <td style="width: 20%; white-space: nowrap;">
                                        <asp:TextBox ID="txtDateTo" runat="server" onKeyPress="event.returnValue=false" CssClass="NormalInputText"
                                            Width="95%"></asp:TextBox>
                                        <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                            src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDateTo','y-mm-dd',0);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" style="text-align: right;">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>

                    <tr valign="top">
                        <td valign="top" colspan="2">
                           <%-- <cc1:DataGridNavigator3 ID="grdNavigatorINMO" runat="server" GridID="grdtempINMo"
                                ShowPageNumber="false" ExcelName="INMO" IsDbPager="True" getexporttoexcelsource="grdNavigatorINBILL_GetExportToExcelSource" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                                <asp:GridView ID="grdtempINMo" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                   
                                    OnRowDataBound="grdtempINMo_RowDataBound" CssClass="Grid" PageSize="15"
                                    OnDataBinding="grdtempINMo_DataBinding" DataKeyNames="ID">
                                    <PagerSettings Visible="False" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <RowStyle HorizontalAlign="Left" Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmUPGDDatat_MSG14 %>"><%--机种--%>
                                            <ItemTemplate>
                                                <div><%# Eval("MODELS")%> </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmUPGDDatat_MSG12 %>"><%--工单号--%>
                                            <ItemTemplate>
                                                <div><%# Eval("WO")%> </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmUPGDDatat_MSG15 %>"><%--工单量--%>
                                            <ItemTemplate>
                                                <div><%# Eval("WO_QTY")%> </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmUPGDDatat_lblDCREATETIMEFromFrom %>"><%--备料日期--%>
                                            <ItemTemplate>
                                                <div><%# Eval("START_DATE")%> </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmUPGDDatat_SHIFT %>"><%--班别--%>
                                            <ItemTemplate>
                                                <div><%# Eval("SHIFT")%> </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmUPGDDatat_LINEBODY %>"><%--线体--%>
                                            <ItemTemplate>
                                                <div><%# Eval("LINEBODY")%> </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmUPGDDatat_SPECIAL %>"><%--特殊--%>
                                            <ItemTemplate>
                                                <div><%# Eval("SPECIAL")%> </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmUPGDDatat_SEC_CINVCODE %>"><%--二次用料--%>
                                            <ItemTemplate>
                                                <div><%# Eval("SEC_CINVCODE")%> </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmUPGDDatat_DEPARTMENTNO %>"><%--部门编码--%>
                                            <ItemTemplate>
                                                <div><%# Eval("DEPARTMENTNO")%> </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmUPGDDatat_Label7 %>"><%--计算工时--%>
                                            <ItemTemplate>
                                                <div><%# Eval("ISCOUNTJOBTIME")%> </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmUPGDDatat_Label8 %>"><%--上线日期--%>
                                            <ItemTemplate>
                                                <div><%# Eval("ONLINETIME")%> </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <ul class="OneRowStyle"  >
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
                                    //settingPad("<%= grdtempINMo.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                                </script>
                            </div>
                        </td>
                    </tr>
                </ContentTemplate>
            </asp:UpdatePanel>
        </table>

    </div>
</asp:Content>
