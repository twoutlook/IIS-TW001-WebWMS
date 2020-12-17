<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmIn_Out_Report1.aspx.cs" Inherits="Apps_Report_FrmIn_Out_Report1" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="DreamTek.ASRS.Business" namespace="DreamTek.ASRS.Business" tagprefix="HDTools" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>   
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
        <style type="text/css">
                                span.requiredSign
                                {
                                    color: red;
                                    font-weight: bold;
                                    position: relative;
                                    left: -15px;
                                    top: 2px;
                                }
                            </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.FrmReport_ReportManagement%>-&gt;物料进出明细报表<%--进出明细报表--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
     
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%"  border="1"
                    id="tabCondition">
                    <tr >
                        <th align="left" colspan="3">
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
                    <tr >                   
                        <td class="InputLabel" style="width: 13%" align="right">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtCinvCode" runat="server"  Width="60%" CssClass="NormalInputText"></asp:TextBox>          
                            <span class="requiredSign" >*</span>
                        </td>
                            <td class="InputLabel" style="width: 13%" align="right">
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>： <%--规格--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtcspec" runat="server"  CssClass="NormalInputText" Width="60%"></asp:TextBox>                          
                        </td>    
                    </tr>
                    <tr>
                        
                        <td class="InputLabel" style="width: 13%;" align="right">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_DateTime %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" CssClass="NormalInputText" Width="60%"></asp:TextBox>
                           
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','y-mm-dd',0);" />
                           
                        </td>
                        <td class="InputLabel" style="width: 13%" align="right">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, Common_TO %>"></asp:Label>：
                        </td>
                        <td style=" white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtDCREATETIMETo" runat="server" CssClass="NormalInputText" Width="60%"></asp:TextBox>                         
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','y-mm-dd',0);" />                          
                        </td>                     
                       
                    </tr>
                    <tr>
                    <td colspan="4" align="center" style="text-align: right;">
                        <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click">
                        </asp:Button>
                        <%--   <input id="btnExportToExcel" class="ButtonExcel" type="button" value="<%= Resources.Lang.WMS_Common_Button_Export %>" />--%>
                            <HDTools:ExcelButton runat="server" GridID="grdInOutDetails" ID="btnExcel" ExcelName="物料进出明细报表"></HDTools:ExcelButton>
                            &nbsp;      
                    </td>
                </tr>   
                     </table>
                </td>  
         </tr>
        <tr>

        <td  colspan="4">
            <div style="height:450px; overflow-x: scroll;overflow-y: scroll; width: 100%; ">
                <asp:GridView ID="grdInOutDetails" runat="server"  BorderColor="Teal"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="grdInOutDetails_RowDataBound"
                    CssClass="Grid" >                    
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />      
                     <RowStyle HorizontalAlign="center" Wrap="False" />        
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />                 
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                    <Columns>                         
                        <asp:BoundField DataField="createDate" DataFormatString="" HeaderText="单据日期"><%--单据日期--%>
                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                            <ItemStyle HorizontalAlign="center" Wrap="False"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="cticketcode" DataFormatString=""  HeaderText="单据号">
                             <HeaderStyle HorizontalAlign="center" Wrap="False" />
                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                        </asp:BoundField><%--单据号--%>
                        <asp:BoundField DataField="typename" DataFormatString="" HeaderText="类型"> <%--类型--%>
                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ERPCODE" DataFormatString="" HeaderText="ERPCODE"><%--ERPCODE--%>
                              <HeaderStyle HorizontalAlign="center" Wrap="False" />
                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="insummary" DataFormatString="{0:f}" HeaderText="入库量">
                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                        </asp:BoundField>
                            <asp:BoundField DataField="outsummary" DataFormatString="{0:f}" HeaderText="出库量" ><%--出库量--%>
                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="stocksummary" DataFormatString="{0:f}" HeaderText="现存量"><%--现存量--%>
                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                        </asp:BoundField>                           
                    </Columns>
                </asp:GridView>                   
            </div>
        </td>
    </tr>   
        <script type="text/javascript">
          
        </script>
    </table>
</asp:Content>
