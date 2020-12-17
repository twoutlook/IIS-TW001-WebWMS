<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" ResponseEncoding="gb2312"
     AutoEventWireup="true" Title="--<%= Resources.Lang.FrmDispatchUnitEdit_MsgTitle %>" CodeFile="FrmDispatchUnitList.aspx.cs" Inherits="FrmDispatchUnitList" %>

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
    <%= Resources.Lang.FrmDispatchUnitEdit_MsgTitle2 %>-&gt;<%= Resources.Lang.FrmDispatchUnitEdit_MsgTitle3 %>
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
                            <%= Resources.Lang.Common_JSCeria %>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>

                            <img id="imgCollapse" alt="<%= Resources.Lang.Common_Fold %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
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
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server"  Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle4 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <%--<td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmInbill_ErpCode %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>--%>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server"  Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle16 %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>                       
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblITYPE" runat="server"  Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle5 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtITYPE" runat="server" Width="96%">                               
                             <%--   <asp:ListItem Value=""  Text="全部"></asp:ListItem>                               
                                <asp:ListItem Value="0"  Text="手动叫车"></asp:ListItem>                              
                                <asp:ListItem Value="1"  Text="产线叫车"></asp:ListItem>                             
                                <asp:ListItem Value="OUT"  Text="出库"></asp:ListItem>                               
                                <asp:ListItem Value="2"  Text="其它(待增加)"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>                    
                    <tr>
                          <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server"  Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle14 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPalletCode" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30" ></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbSO" runat="server"  Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle10 %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtSO" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server"  Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：<%--状态：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="txtCSTATUS" runat="server" Width="95%">                               
                              <%--  <asp:ListItem Value=""  Text="全部"></asp:ListItem>                               
                                <asp:ListItem Value="0"  Text="未处理"></asp:ListItem>                                
                                 <asp:ListItem Value="1"  Text="处理中"></asp:ListItem>                                
                                 <asp:ListItem Value="2"  Text="已完成"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        
                           <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server"  Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle29 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMEFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style=" position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEToTo" runat="server"  Text="<%$ Resources:Lang, FrmDispatchUnitList_MsgTitle4 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDCREATETIMETo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style=" position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','y-mm-dd',0);" />
                        </td>
                        <td  class="InputLabel" colspan="4"></td>
                    </tr>
                      <tr>
                          <td class="InputLabel" style="width: 13%">
                               <asp:Label ID="lblFrmSiteNo" runat="server"  Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle23 %>"></asp:Label>：
                          </td>
                           <td style="width: 20%">
                               <asp:TextBox ID="txtFrmSiteNo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                          </td>
                           <td class="InputLabel" style="width: 13%">
                                <asp:Label ID="lblToSiteNo" runat="server"  Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle24 %>"></asp:Label>：
                          </td>
                           <td style="width: 20%">
                                <asp:TextBox ID="txtToSiteNo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                          </td>

                    </tr>
                    <tr style="display:none;" >
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITDATEFromFrom" runat="server"  Text="<%$ Resources:Lang, FrmDispatchUnitList_MsgTitle5 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap">
                            <asp:TextBox ID="txtDAUDITDATEFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style=" position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITDATEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDAUDITDATEToTo" runat="server"  Text="<%$ Resources:Lang, FrmDispatchUnitList_MsgTitle4 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDAUDITDATETo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style=" position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDAUDITDATETo','y-mm-dd',0);" />
                        </td>
                        <td style="width: 20%; white-space: nowrap;" class="InputLabel" colspan="4"></td>
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
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch"  Text="<%$ Resources:Lang, FrmDispatchUnitList_MsgTitle6 %>" OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       
        <tr valign="top">
            <td valign="top" align="left">
                &nbsp;
            </td>
        </tr>
       
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdOUTASN" runat="server" AllowPaging="True" BorderColor="Teal" 
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" DataKeyNames="ID" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnSorting="grdOUTASN_Sorting"
                        OnRowDataBound="grdOUTASN_RowDataBound" AllowSorting="true" CssClass="Grid" PageSize="15">
                        <PagerSettings Mode="NumericFirstLast" />
                        <%--OnPageIndexChanged="grdOUTASN_PageIndexChanged" OnPageIndexChanging="grdOUTASN_PageIndexChanging"--%>
                        <%--<PagerSettings Visible="False" />--%>
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                       <%-- <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />--%>
                        <Columns>
                                <asp:BoundField DataField="ID" DataFormatString="" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>        
                            <asp:BoundField DataField="CTICKETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle4 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>                             
                            <asp:BoundField DataField="TASKTYPENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle5 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>  
                             <asp:BoundField DataField="CSTATUSNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle15 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                           <%-- <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_ErpCode %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="PACKAGENO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle14 %>"  >
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                              <asp:BoundField DataField="SOURCECODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle10 %>"  >
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>                              
                            <asp:BoundField DataField="CREATEUSER" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle16 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle18 %>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle30 %>" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnComplete" runat="server" CausesValidation="false" CommandArgument='<%#Eval("ID") %>'
                                        CommandName=""  Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle30 %>" OnClick="lbtnComplete_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%=Resources.Lang.FrmDispatchUnitEdit_MsgTitle7 %>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle7 %>"  Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle7 %>">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                                              
                        </Columns>
                    </asp:GridView>
                    <ul class="OneRowStyle">
                        <li>
                              <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" 
                                  CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                            FirstPageText="<%$ Resources:Lang, Common_FirstPage %>" LastPageText="<%$ Resources:Lang, Common_LastPage %>" 
                                            NextPageText="<%$ Resources:Lang, Common_NextPage %>" 
                                            PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                        </li>
                        <li>
                            <div><%=Resources.Lang.Common_TotalPage%> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_FirstPage%></div>
                        </li>
                    </ul>
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdOUTASN.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                    </script>
                
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
                var controls = document.getElementById("<%=grdOUTASN.ClientID %>").getElementsByTagName("input");

                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
                if (number == 0) {
                    //请选择需要删除的项！
                    alert("<%=Resources.Lang.FrmDispatchUnitList_MsgTitle8 %>");//+"!"
                    return false;
                }
                //你确认删除吗？
                if (confirm("<%=Resources.Lang.FrmDispatchUnitList_MsgTitle9 %>")) {//+"?"
                    return true;
                }
                else {
                    return false;
                }
            }
            function CheckClose() {
                var number = 0;
                var controls = document.getElementById("<%=grdOUTASN.ClientID %>").getElementsByTagName("input");
              
                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
                if(number == 0) {
                    //请选择需要结案的项！
                    alert("<%=Resources.Lang.FrmDispatchUnitList_MsgTitle10 %>");//+"!"
                    return false;
                }
                //"你确认结案吗？
                if(confirm("<%=Resources.Lang.FrmDispatchUnitList_MsgTitle11 %>")) {//+"?"
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
    </table>
</asp:Content>
