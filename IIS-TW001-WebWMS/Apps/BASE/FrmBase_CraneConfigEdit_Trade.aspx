<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Apps/DefaultMasterPage.master" CodeFile="FrmBase_CraneConfigEdit_Trade.aspx.cs" Inherits="Apps_BASE_FrmBase_CraneConfigEdit_Trade" %>

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
    <%= Resources.Lang.FrmBase_AGV_D_Title01%>-&gt;<%= Resources.Lang.FrmBase_CraneConfigEdit_Trade_Title01%><%--站点管理-&gt;交易类型管理--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table style="height: 100%; width: 95%" id="TabMain">
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" OnClientClick="return CheckAdd();"
                    Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp; 
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
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
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">

                    <asp:GridView ID="grdBASE_trande" runat="server" AllowPaging="True" BorderColor="Teal"
                        DataKeyNames="TYPEID"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="true"
                        OnRowDataBound="grdBASE_trande_RowDataBound" CssClass="Grid" PageSize="15">
                        <PagerSettings Mode="NumericFirstLast" />
                        <%--Visible="False" />--%>
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
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:BoundField DataField="TYPEID" DataFormatString="" HeaderText="TYPEID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="T" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Type%>"><%--类型--%> 
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />

                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_lblCERPCODE%>"><%--类型编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TYPENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_lblTYPENAME%>"><%--类型名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ENABLE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"><%--状态--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>


                    <ul class="OneRowStyle">
                        <li>
                            <webdiyer:aspnetpager id="AspNetPager1" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="AspNetPager1_PageChanged"
                                FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false" xmlns:webdiyer="wuqi.webdiyer">
                                                        </webdiyer:aspnetpager>
                        </li>
                        <li>
                            <div><%= Resources.Lang.Base_Gong%><%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                        </li>
                    </ul>
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdBASE_trande.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                    </script>
                </div>
            </td>
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
    <script type="text/javascript">
        function ChangeDivWidth() {
            document.getElementById("DivScrolls").style.width = window.document.body.offsetWidth - 25;
        }
        window.onresize = ChangeDivWidth;
        ChangeDivWidth();
        function CheckAdd() {
            var number = 0;
            var controls = document.getElementById("<%=grdBASE_trande.ClientID %>").getElementsByTagName("input");

            for (var i = 0; i < controls.length; i++) {
                var e = controls[i];
                if (e.type != "CheckBox") {
                    if (e.checked == true) {
                        number = number + 1;
                    }
                }
            }
            if (number == 0) {
                alert("<%= Resources.Lang.FrmBase_CraneConfigEdit_Trade_Msg01%>");//请选择需要新增的项！
                return false;
            }
            if (confirm("<%= Resources.Lang.FrmBase_CraneConfigEdit_Trade_Msg02%>")) { //你确认新增吗？
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
