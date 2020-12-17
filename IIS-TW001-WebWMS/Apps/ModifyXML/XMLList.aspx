<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XMLList.aspx.cs"
    MasterPageFile="~/Apps/DefaultMasterPage.master"
     Inherits="Apps_ModifyXML_XMLList" %>

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
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    基础资料-&gt;语言文件列表
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
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
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
                        <td colspan="6">
                            <style type="text/css">
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
                         <td class="InputLabel" style="width: 13%">
                           <asp:Label ID="Label5" runat="server" Text="语言："></asp:Label>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                           <asp:DropDownList ID="ddllang" runat="server">
                              <%-- <asp:ListItem Text="全部" Value=""></asp:ListItem>
                               <asp:ListItem Text="中文（简体）" Value="zh-cn"></asp:ListItem>
                               <asp:ListItem Text="中文（繁体）" Value="zh-TW"></asp:ListItem>
                               <asp:ListItem Text="英文（美国）" Value="英文（美国）"></asp:ListItem>--%>
                           </asp:DropDownList>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="模块："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="DropDownList1" runat="server">
                               <asp:ListItem Text="全部" Value=""></asp:ListItem>
                               <asp:ListItem Text="DB" Value="DB"></asp:ListItem>
                               <asp:ListItem Text="PDA" Value="PDA"></asp:ListItem>
                           </asp:DropDownList>
                        </td>

                       
                        <td class="InputLabel" style="width: 13%">
                           
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                           
                        </td>
                    </tr>

                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCLIENTID" runat="server" Text="源文件Key："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtKey" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCLIENTNAME" runat="server" Text="源文件值："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtValue" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="状态："></asp:Label>
                        </td>
                        <td style="width: 20%">
                           <asp:DropDownList ID="ddlStatus" runat="server">
                              <%-- <asp:ListItem Text="全部" Value=""></asp:ListItem>
                               <asp:ListItem Text="启用" Value="0"></asp:ListItem>
                               <asp:ListItem Text="作废" Value="1"></asp:ListItem>--%>
                           </asp:DropDownList>
                        </td>
                    </tr>
                  
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="创建人："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCreateTimeFrom" runat="server" Text="创建时间："></asp:Label>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtCreateTimeFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateTimeFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCreateTimeTo" runat="server" Text="到："></asp:Label>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtCreateTimeTo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateTimeTo','y-mm-dd',0);" />
                            
                        </td>
                    </tr>

                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="修改人："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="修改时间："></asp:Label>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="TextBox1" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateTimeFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="到："></asp:Label>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="TextBox2" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateTimeTo','y-mm-dd',0);" />
                            
                        </td>
                    </tr>

                    

                    <tr style="display: none;">
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
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="查询" OnClick="btnSearch_Click" ></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       
        <tr valign="top">
            <td valign="top" align="left"><asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="新增"></asp:Button>
                &nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="ButtonDel" OnClientClick="return CheckDel()" />
                &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="一键复制" CssClass="ButtonAdd4"  />
                &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" Text="批量修改" CssClass="ButtonConfig"  />
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdLangSource" runat="server" AllowPaging="True" BorderColor="Teal" DataKeyNames="ID"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"
                        OnRowDataBound="grdLangSource_RowDataBound"
                         AllowSorting="true" CssClass="Grid" PageSize="15" >
                       
                        <PagerSettings Mode="NumericFirstLast" />
                       
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                       
                        <Columns>
                            <asp:TemplateField HeaderText="<input id='chkSelect' type='checkbox' onclick='SelectAll4Grid(this)' />">
                                <ControlStyle BorderWidth="0px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid"
                                        BorderWidth="0px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" DataFormatString="" HeaderText="编号" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>

                            <asp:BoundField DataField="LanguageName" DataFormatString="" HeaderText="语言">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ModuleId" DataFormatString="" HeaderText="模块">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>

                            <asp:BoundField DataField="SourceKey" DataFormatString="" HeaderText="源文件Key">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SourceValue" DataFormatString="" HeaderText="源文件值">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StatusName" DataFormatString="" HeaderText="状态">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="Remark" DataFormatString="" HeaderText="备注">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="CreateUser" DataFormatString="" HeaderText="创建人">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreateTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="创建日期">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="ModifyUser" DataFormatString="" HeaderText="修改人">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Modifytime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="修改时间">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
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
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" OnPageChanged="AspNetPager1_PageChanged" PagingButtonSpacing="0" CurrentPageButtonClass="active" 
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
                        //settingPad("<%= grdLangSource.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
                var controls = document.getElementById("<%=grdLangSource.ClientID %>").getElementsByTagName("input");

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
    </table>
</asp:Content>

