<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--" CodeFile="FrmBaseCONFIG_List.aspx.cs" Inherits="FrmBaseCONFIG_List" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <script src="../../scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <link href="../../styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/bootstrap.min.js" type="text/javascript"></script>
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
            height:100%;
        }
        .tableCell {
            display:table;
            width:100%;
        }
        .gridLineHeight {
            line-height:22px;
        }
        .wap_word_200 {
            display: inline-block;
            max-width: 300px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            text-overflow: -o-ellipsis-lastline;
            cursor:pointer;
        }
        .Grid th {
            text-align:center;
        }

    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                        </th>
                        <th style="border-left-width: 0px;text-align:right;" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center" onclick="CollapseCondition('../../');return false;" />  <%--折叠--%>
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
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmBaseCONFIG_List_Label2%>"></asp:Label>： <%--作用域--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtType" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <%--<span class="requiredSign">*</span>--%>
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBaseCONFIG_List_Label4%>"></asp:Label>： <%--配置代码--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtNo" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmBaseCONFIG_List_Label5%>"></asp:Label>： <%--配置值--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtValue" runat="server" CssClass="NormalInputText"
                                Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>"
                                OnClick="btnSearch_Click" Width="62px" Style="men"></asp:Button>  <%--查询--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td colspan="6" align="left" style="padding:15px 0px;">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button> <%--新增--%>
                &nbsp;&nbsp;<asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_Delete%>" CssClass="ButtonDel" OnClientClick="return CheckDel()" />  <%--删除--%>
            </td>
        </tr>
        <tr class="tableCell">
            <td>
                <div style="min-height: 450px; overflow-x: auto; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdCONFIGList" runat="server" AllowPaging="True" BorderColor="Teal" DataKeyNames="ID"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"
                        OnRowDataBound="grdCONFIGList_RowDataBound1" AllowSorting="true" CssClass="Grid gridLineHeight" PageSize="15" OnSorting="grdCONFIGList_Sorting">
                        <PagerSettings Mode="NumericFirstLast" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <%-- <PagerStyle HorizontalAlign="Right" />--%>
                        <%-- <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />--%>
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
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                            <asp:BoundField DataField="type" HeaderText="<%$ Resources:Lang, FrmBaseCONFIG_List_Label2%>" ItemStyle-Width="50px"><%--作用域--%>
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CODE" HeaderText="<%$ Resources:Lang, FrmBaseCONFIG_List_Label4%>" ItemStyle-Width="80px"> <%--配置代码--%>
                                <ItemStyle Width="80px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CONFIG_DESC" HeaderText="<%$ Resources:Lang, FrmBaseCONFIG_List_CONFIG_DESC%>"
                                ItemStyle-Width="100px"><%--配置描述--%>
                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CONFIG_VALUE" HeaderText="<%$ Resources:Lang, FrmBaseCONFIG_List_Label5%>"
                                ItemStyle-Width="50px">         <%--配置值--%>
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MEMO" HeaderText="<%$ Resources:Lang, Common_CMEMO%>"> <%--备注--%>
                                <ItemStyle Width="400px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATEOWNER" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_lblCCREATEOWNERCODE%>"></asp:BoundField> <%--制单人--%>
                            <asp:BoundField DataField="CREATETIME" HeaderText="<%$ Resources:Lang, FrmALLOCATEList_lblDCREATETIMEFromFrom %>"
                                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" /> <%--制单时间--%>
                            <asp:BoundField DataField="LASTUPDATEOWNER" HeaderText="<%$ Resources:Lang, FrmBaseCONFIG_List_LASTUPDATEOWNER%>"></asp:BoundField> <%--最近操作人--%>
                            <asp:BoundField DataField="LASTUPDATETIME" HeaderText="<%$ Resources:Lang, FrmBaseCONFIG_List_LASTUPDATETIME%>" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"></asp:BoundField> <%--最近操作时间--%>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%> <%--编辑--%>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                        </Columns>
                    </asp:GridView>
                    <ul class="OneRowStyle">
                        <li>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
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
                        //settingPad("<%= grdCONFIGList.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
                var controls = document.getElementById("<%=grdCONFIGList.ClientID %>").getElementsByTagName("input");

                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmALLOCATEEdit_Msg06%>"); //请选择需要删除的项！
                    return false;
                }
                if (confirm("<%= Resources.Lang.FrmALLOCATEEdit_Msg07%>")) { //你确认删除吗？
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
    </table>

<script type="text/javascript">
    $(function () { $("[data-toggle='tooltip']").tooltip(); });
</script>
</asp:Content>




