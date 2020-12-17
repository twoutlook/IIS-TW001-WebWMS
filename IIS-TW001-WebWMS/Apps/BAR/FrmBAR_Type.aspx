<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--111" CodeFile="FrmBAR_Type.aspx.cs" Inherits="Apps_Bar_FrmBAR_Type" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
<script type="text/javascript">
    function CheckDel() {
        var number = 0;
        var controls = document.getElementById("<%=grdBAR_Type.ClientID %>").getElementsByTagName("input");

        for (var i = 0; i < controls.length; i++) {
            var e = controls[i];
            if (e.type != "CheckBox") {
                if (e.checked == true) {
                    number = number + 1;
                }
            }
        }
        if (number == 0) {
            alert("请选择需要删除的项！");
            return false;
        }
        if (confirm("你确认删除吗？")) {
            return true;
        }
        else {
            return false;
        }
    }
      </script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    条码管理-&gt;類型維護
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
                        <th align="left" colspan="3">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                height="11" alt="" runat="server" id="titleImg" />
                            检索条件
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="折叠" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr style="display: none">
                        <td colspan="4">
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
                            <asp:Label ID="lblCNAME" runat="server" Text="名称："></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtCNAME" runat="server" CssClass="NormalInputText" Width="80%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label runat="server" ID="Label1" Text="是否混放"></asp:Label>
                        </td>
                        <td style="width: 35%">
                           <asp:DropDownList runat="server" ID="dplYN" Width="80%">
                              <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                             <asp:ListItem Value="Y">是</asp:ListItem>
                             <asp:ListItem Value="N">否</asp:ListItem> 
                           </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label runat="server" ID="lbaType" Text="類型"></asp:Label>
                        </td>
                        <td style="width: 35%">
                           <asp:DropDownList runat="server" ID="dplType" Width="80%">
                              <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                             <asp:ListItem Value="0">棧板</asp:ListItem>
                             <asp:ListItem Value="1">箱</asp:ListItem> 
                           </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label runat="server" ID="lblCode" Text="條碼類型"></asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList runat="server" ID="dplCode" Width="80%">
                               <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                               <asp:ListItem Value="39">Code39</asp:ListItem>
                               <asp:ListItem Value="128">Code128</asp:ListItem>
                           </asp:DropDownList>
                        </td>
                    </tr>
                   
                    <tr style="display: none">
                        <td colspan="4">
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
                        <td colspan="4" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="查询" OnClick="btnSearch_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left">
                &nbsp;&nbsp;<asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="新增">
                </asp:Button>
                <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="删除" CssClass="ButtonDel"   OnClientClick="return CheckDel();" />
            </td>
        </tr>
        <tr valign="top">
            <td valign="top">
             <%--   <cc1:DataGridNavigator3 ID="grdNavigatorBAR_Type" runat="server" GridID="grdBAR_Type"
                    ShowPageNumber="false" ExcelName="BAR_Type" IsDbPager="True" />--%>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: auto; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdBAR_Type"  runat="server"
                        AllowPaging="True" BorderColor="Teal" BorderStyle="Solid" 
                        BorderWidth="1px" CellPadding="1"
                        Width="100%" AutoGenerateColumns="False"  OnRowDataBound="grdBAR_Type_RowDataBound"
                        ShowHeader="True" CssClass="Grid" PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <Columns>
                            <asp:TemplateField HeaderText="<input id='chkSelect' type='checkbox' onclick='SelectAll4Grid(this)' />">
                                <ControlStyle BorderWidth="0px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid"
                                        BorderWidth="0px" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkSelect0" runat="server" BorderStyle="Solid" BorderWidth="0px" />
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        <%--    <asp:BoundField DataField="ID" DataFormatString="" HeaderText="编码">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="TYPENAME" DataFormatString="" HeaderText="名称">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="TYPE" DataFormatString="" HeaderText="類型">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                              <asp:BoundField DataField="MIX" HeaderText="是否混放">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BARCODE_TYPE" HeaderText="條碼類型">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField> 
                                <asp:BoundField DataField="MAX_QTY" HeaderText="容量">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
 
                            <asp:BoundField DataField="coperatorname" DataFormatString="" HeaderText="创建人">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd}" HeaderText="创建日期">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                 
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="编辑" DataTextField=""
                                DataTextFormatString="" HeaderText="编辑" Text="编辑">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                          
                        </Columns>
                    </asp:GridView>
                     <ul class="OneRowStyle">
                        <li>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                            </webdiyer:AspNetPager>
                        </li>
                        <li>
                            <div>共 <%=AspNetPager1.RecordCount  %> 条数据</div>
                        </li>
                    </ul>
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdBAR_Type.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
