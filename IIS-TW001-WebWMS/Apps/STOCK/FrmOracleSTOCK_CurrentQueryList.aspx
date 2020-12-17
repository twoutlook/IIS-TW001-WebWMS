<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--库存查询" CodeFile="FrmOracleSTOCK_CurrentQueryList.aspx.cs" Inherits="FrmOracleSTOCK_CurrentQueryList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>

    <style type="text/css">
    .td{word-break: break-all; word-wrap:break-word; width:300px;}
    </style>
     
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
     <%=Resources.Lang.WMS_Common_Group_StockManage%>-&gt;<%=Resources.Lang.FrmOracleSTOCK_CurrentQueryList_Title01%><%--庫存管理-&gt;ERP库存查询--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top" class="style1">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                height="11" alt="" runat="server" id="titleImg" />
                           <%=Resources.Lang.Common_JSCeria%>  <%--检索条件--%>
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
                            <asp:Label ID="lbCWAREHOUSECODE" runat="server" Text="<%$ Resources:Lang,Common_WareHouseCode %>"></asp:Label>：<%--仓库编码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCWAREHOUSECODE" runat="server" Width="95%"></asp:TextBox>
                        </td>
                     <%--   <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCPOSITIONCODE" runat="server" Text="储位："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPOSITIONCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>                            
                        </td>--%>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCINVCODE" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"></asp:Label>：<%--料号--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>                 
                    <tr style="display: none">
                        <td colspan="6">
                            <script type="text/javascript" language="javascript">
                                //调整“行”为不能换行的。为什么不在设计期就设置其样式为不换行的呢，因为一旦设置，输入控件莫名其妙地看不见了。
                                function ChangeTDStyle(inputTableID) {
//                                    var tabMain = document.getElementById(inputTableID);
//                                    if (tabMain == null) return;
//                                    for (var i = 0; i < tabMain.rows.length; i++) {
//                                        var tr = tabMain.rows[i];
//                                        if (tr == null) continue;
//                                        for (var j = 0; j < tr.cells.length; j++) {
//                                            var td = tr.cells[j];
//                                            if (td == null) continue;
//                                            if (td.className == "" || td.className == null) {
//                                                td.style.whiteSpace = "nowrap";
//                                                td.style.borderRightWidth = "0px";
//                                            }
//                                        }
//                                    }
                                }
                                ChangeTDStyle("ctl00_ContentPlaceHolderMain_TabMain"); 
                            </script>
                            <asp:HiddenField ID="hfieldGUID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click"><%--查询--%>
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>      
        <tr>
            <td>
                <div style="overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdSTOCK_DURATION" runat="server" AllowPaging="True"
                        BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%"
                        AutoGenerateColumns="False"                      
                        OnRowDataBound="grdSTOCK_DURATION_RowDataBound" CssClass="Grid" 
                        PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                         
                            <asp:BoundField DataField="warehouseid" DataFormatString="" HeaderText="<%$ Resources:Lang,Common_WareHouseCode %>"><%--仓库编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="True"  CssClass="td" Width="80px"/>
                            </asp:BoundField>
                         
                          <%--  <asp:BoundField DataField="segment1" DataFormatString="" HeaderText="储位编码">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="description_loc" DataFormatString="" HeaderText="储位">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="True" CssClass="td" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="cpartnumber" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvcode %>"><%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="True" CssClass="td" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cpartname" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_Cinvname %>"><%--品名--%>
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="left" Width="300px"  CssClass="td" Wrap="True" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="ebs_stock_qty" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmWmsAndEbsQtyDiffReport_Msg1 %>"><%--ERP数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" Wrap="True" />
                            </asp:BoundField>
                           <asp:BoundField DataField="wms_stock_qty" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmWmsAndEbsQtyDiffReport_Msg2 %>"><%--WMS数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="discrepency_qty" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_Diff %>"> <%--差异--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" Wrap="True" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>                  
                      <ul class="OneRowStyle">
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
