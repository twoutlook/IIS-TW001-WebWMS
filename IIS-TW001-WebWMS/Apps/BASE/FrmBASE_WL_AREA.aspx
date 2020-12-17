<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--<%$ Resources:Lang, FrmBASE_PARTList_SetArea%>" CodeFile="FrmBASE_WL_AREA.aspx.cs" Inherits="FrmBASE_WL_AREA" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="ShowAREADiv.ascx" TagName="ShowAREADiv" TagPrefix="ucArea" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script type="text/javascript">
        function CheckSel() {
            var number = 0;
            var controls = document.getElementById("<%=grdBASE_CARGOSPACE.ClientID %>").getElementsByTagName("input");

            for (var i = 0; i < controls.length; i++) {
                var e = controls[i];
                if (e.type != "CheckBox") {
                    if (e.checked == true) {
                        number = number + 1;
                    }
                }
            }
            var selType = document.getElementById("ctl00_ContentPlaceHolderMain_ddlAllType").value;
            if (selType == "1") {
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmBASE_OPERATOR_AREA_Msg01%>"); //请至少选择一个储位！
                    return false;
                }
            }
            return true;
        }

        function SelIDCancelAll() {
            document.getElementById("ctl00_ContentPlaceHolderMain_cboxAllArea").checked = false;
        }

    </script>
    <script type="text/javascript">
    <!--
        function setTab(name, cursel, n) {
            document.getElementById("ctl00_ContentPlaceHolderMain_hfTabIndex").value = cursel;
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                var con = document.getElementById("con_" + name + "_" + i);
                menu.className = i == cursel ? "hover" : "";
                con.style.display = i == cursel ? "block" : "none";

            }
        }

        window.onload = function () {
            var indexT = document.getElementById("ctl00_ContentPlaceHolderMain_hfTabIndex").value;
            document.getElementById("one" + indexT).click();
        }
    //-->
    </script>
    <style type="text/css">
        body
        {
            padding: 0;
            font: 12px "宋体";
        }
        /*Tab1*/#lib_Tab1
        {
            width: 100%;
            margin: 0 auto;
            padding: 0px;
            margin-top: 2px;
            margin-bottom: 2px;
        }
        /*Tab2*/#lib_Tab2
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
            width: 240px;
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
            background: url(    "../Templets/newimages/search.gif" );
            border: 0;
            cursor: pointer;
        }
        .btnExpert
        {
            width: 63px;
            height: 22px;
            background: url(    "../Templets/newimages/export.gif" );
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
    <%= Resources.Lang.FrmBASE_WL_AREA_Title01%>  <%--物料设置储位或区域--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <ucArea:ShowAREADiv ID="ucShowArea" runat="server" />
    <div id="lib_Tab1">
        <div class="lib_Menubox lib_tabborder">
            <ul>
                <li id="one1" onclick="setTab('one',1,2)" class="hover"><%= Resources.Lang.FrmBASE_WL_AREA_Title02%></li>
                <li id="one2" onclick="setTab('one',2,2)"><%= Resources.Lang.FrmBASE_WL_AREA_Title03%></li>
            </ul>
        </div>
        <div class="lib_Contentbox lib_tabborder">
            <div id="con_one_1">
                <table id="TabMain" style="height: 100%; width: 95%">
                    <tr valign="top">
                        <td valign="top">
                            <asp:HiddenField ID="hfTabIndex" runat="server" Value="1" />
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                id="tabCondition">
                                <tr>
                                    <th align="left" colspan="5">
                                        &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                            height="11" alt="" runat="server" id="titleImg" />
                                        <%= Resources.Lang.Common_JSCeria%>
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                                        </script>
                                        <img id="imgCollapse" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                            onclick="CollapseCondition('../../');return false;" />
                                    </th>
                                </tr>
                                <tr style="display: none;">
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
                                        <asp:Label ID="lblCCARGOID" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>"></asp:Label>：<%--编号--%></asp:Label>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCCARGOID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCCARGONAME" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblCCARGONAME%>"></asp:Label>： <%--名称--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCCARGONAME" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%">
                                            <asp:ListItem Value="">全部</asp:ListItem>
                                            <asp:ListItem Value="0">正常</asp:ListItem>
                                            <asp:ListItem Value="1">盘点冻结</asp:ListItem>
                                            <asp:ListItem Value="2">占用</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBASE_WL_AREA_Label1%>"></asp:Label>： <%--所属区域--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCDEFINE1" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox><asp:TextBox
                                            ID="txtAreaID" runat="server" Style="display: none;"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_Label1%>"></asp:Label>： <%--是否保稅--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:DropDownList ID="ddlBSC" runat="server" Width="95%">
                                            <asp:ListItem Value="">全部</asp:ListItem>
                                            <asp:ListItem Value="Y">是</asp:ListItem>
                                            <asp:ListItem Value="N">否</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="InputLabel" style="width: 13%" runat="server" id="tdSZCWNAME">
                                        <asp:Label ID="lblStatus" runat="server" Text="<%$ Resources:Lang, FrmBASE_WL_AREA_lblStatus%>"></asp:Label>： <%--是否已设置储位--%>
                                    </td>
                                    <td style="width: 20%" runat="server" id="tdSetType">
                                        <asp:DropDownList ID="ddlISSetArea" runat="server" Width="95%">
                                            <asp:ListItem Value="">全部</asp:ListItem>
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <%-- <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="区域："></asp:Label>
                        </td>
                        <td style="width: 80%" colspan="5">
                            <asp:TextBox ID="txtQY" runat="server" CssClass="NormalInputText" Width="25%"></asp:TextBox>
                        </td>--%>
                                </tr>
                                <tr style="display: none;">
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCALIAS" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCALIAS%>"></asp:Label>： <%--助记码--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCALIAS" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCERPCODE%>"></asp:Label>： <%--ERP编码--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCTYPE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblCTYPE%>"></asp:Label>： <%--种类--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCTYPE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblDEXPIREDATEFromFrom" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblDEXPIREDATEFromFrom%>"></asp:Label>： <%--终止日期--%>
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="txtDEXPIREDATEFrom" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                            left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDEXPIREDATEFrom','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblDEXPIREDATEToTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao%>"></asp:Label>：
                                    </td>
                                    <td style="width: 21%; white-space: nowrap;">
                                        <asp:TextBox ID="txtDEXPIREDATETo" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                            left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDEXPIREDATETo','y-mm-dd',0);" />
                                        <span class="requiredSign" style="left: -50px">*</span>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblIPERMITMIX" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblIPERMITMIX%>"></asp:Label>： <%--是否允许混放--%>
                                    </td>
                                    <td style="width: 21%">
                                        <asp:DropDownList ID="ddlIPERMITMIX" runat="server" Width="95%">
                                            <asp:ListItem Value="">全部</asp:ListItem>
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:DropDownList>
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
                                        <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"> <%--查询--%>
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" align="center">
                            <%--<asp:CheckBox ID="cboxAllArea" runat="server" Text="关联所有储位" />--%>
                            <asp:DropDownList ID="ddlAllType" runat="server" Width="150px">
                                <asp:ListItem Value="1">设置所选储位</asp:ListItem>
                                <asp:ListItem Value="2">设置所有储位</asp:ListItem>
                                <asp:ListItem Value="3">取消所有储位关联</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" Text="<%$ Resources:Lang, Common_btnSave%>" OnClick="btnSave_Click"
                                OnClientClick="return CheckSel()" /><%--保存--%>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>" Visible="false">
                            </asp:Button><%--新增--%>
                            &nbsp;&nbsp;<asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_Delete%>"
                                CssClass="ButtonDel" Visible="false" OnClientClick="return CheckDel()" /> <%--删除--%>	
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top">
                          <%--  <cc1:DataGridNavigator3 ID="grdNavigatorBASE_CARGOSPACE" runat="server" GridID="grdBASE_CARGOSPACE"
                                ShowPageNumber="false" ExcelName="BASE_CARGOSPACE" IsDbPager="True" GetExportToExcelSource="grdNavigatorBASE_CARGOSPACE_GetExportToExcelSource" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                                <asp:GridView ID="grdBASE_CARGOSPACE" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    OnPageIndexChanged="grdBASE_CARGOSPACE_PageIndexChanged" OnPageIndexChanging="grdBASE_CARGOSPACE_PageIndexChanging"
                                    OnRowDataBound="grdBASE_CARGOSPACE_RowDataBound" CssClass="Grid" PageSize="15"
                                    OnDataBinding="grdBASE_CARGOSPACE_DataBinding">
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
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" DataFormatString="" HeaderText="ID" Visible="False">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" > <%--编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblCCARGONAME%>">  <%--名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CALIAS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCALIAS%>" Visible="False"> <%--助记码--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CERPCODE" DataFormatString=""  Visible="False" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCERPCODE%>"> <%--ERP编码--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IMAXCAPACITY" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IMAXCAPACITY%>"
                                            Visible="False">  <%--载重--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CMEMO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO%>" Visible="False">  <%--备注--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="CTYPE" DataFormatString="" HeaderText="种类">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>--%>
                                        <asp:BoundField DataField="CWARENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_CWARENAME%>"> <%--所属仓库--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CDEFINE1" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_WL_AREA_Label1%>">  <%--所属区域--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BONDED" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_WL_AREA_BONDED%>"><%--是否是保税仓--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IPRIORITY" DataFormatString="{0:0}"  HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IPRIORITY%>" Visible="False"> <%--优先级--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ILENGTH" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_ILENGTH%>" Visible="False"> <%--长--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IWIDTH" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IWIDTH%>" Visible="False"> <%--宽--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IHEIGHT" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IHEIGHT%>" Visible="False"> <%--高--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IVOLUME" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IVOLUME%>" Visible="False"> <%--体积--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CUSETYPE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_lblCUSETYPE%>" Visible="False"><%--用途--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CX" DataFormatString="" HeaderText="x" Visible="False">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CY" DataFormatString="" HeaderText="y" Visible="False">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CZ" DataFormatString="" HeaderText="z" Visible="False">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="IPERMITMIX" DataFormatString="{0:0}" HeaderText="是否允许混放">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>--%>
                                        <asp:BoundField DataField="DEXPIREDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblDEXPIREDATEFromFrom%>"
                                            Visible="False"><%--终止日期--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CSTATUS" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>" Visible="False"><%--状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                            DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>" Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                </asp:GridView>

                                <ul class="OneRowStyle">
                                    <li >
                                         <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                           FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                        <div><%= Resources.Lang.Base_Gong%><%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                                    </li>
                                </ul>

                                <script type="text/javascript" language="javascript">
                                    //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                                    //settingPad("<%= grdBASE_CARGOSPACE.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
                            var controls = document.getElementById("<%=grdBASE_CARGOSPACE.ClientID %>").getElementsByTagName("input");

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
                             if (confirm("<%= Resources.Lang.FrmALLOCATEEdit_Msg07%>")) {  //你确认删除吗？
                                return true;
                            }
                            else {
                                return false;
                            }
                        }
                    </script>
                </table>
            </div>
            <div id="con_one_2" style="display: none">
                <script type="text/javascript">
                    function CheckSelArea() {
                        var number = 0;
                        var controls = document.getElementById("<%=grdBAS_AREA.ClientID %>").getElementsByTagName("input");

                        for (var i = 0; i < controls.length; i++) {
                            var e = controls[i];
                            if (e.type != "CheckBox") {
                                if (e.checked == true) {
                                    number = number + 1;
                                }
                            }
                        }
                        var selType = document.getElementById("ctl00_ContentPlaceHolderMain_ddlAllType2").value;
                   
                        if (selType == "1") {
                            if (number == 0) {
                                alert("<%= Resources.Lang.FrmBASE_WL_AREA_Msg01%>");//请至少选择一个区域！
                                return false;
                            }
                        }
//                        if (selType == "2") {
//                            return true;
//                        }
                        return true;
                    }

                    function SelIDCancelAll() {
                        document.getElementById("ctl00_ContentPlaceHolderMain_cboxAllArea").checked = false;
                    }
                </script>
                <table id="Table1" style="height: 100%; width: 95%">
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                id="Table2">
                                <tr>
                                    <th align="left" colspan="5">
                                        &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                            height="11" alt="" runat="server" id="Img1" />
                                        <%= Resources.Lang.Common_JSCeria%>
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                                        </script>
                                        <img id="img2" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                            onclick="CollapseCondition('../../');return false;" />
                                    </th>
                                </tr>
                                <tr style="display: none;">
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
                                        <asp:Label ID="lblPALLETID" runat="server" Text="<%$ Resources:Lang, FrmBASE_WL_AREA_lblPALLETID%>"></asp:Label>： <%--区域名称--%>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:TextBox ID="txtAreaName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label runat="server" ID="labBlCw" Text="<%$ Resources:Lang, FrmBASE_WL_AREA_labBlCw%>"></asp:Label>：<%--備料儲位--%>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:TextBox runat="server" ID="txtblCw" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblPALLETNAME" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：<%--创建人--%>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:TextBox ID="txtCreateUser" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%;">
                                        <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, Common_CreateDate%>"></asp:Label>： <%--创建日期--%>
                                    </td>
                                    <td style="width: 15%; white-space: nowrap;">
                                        <asp:TextBox ID="txtFromDate" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                            left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtFromDate','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel" style="width: 13%;">
                                        <asp:Label ID="lblDCREATETIMEToTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao%>"></asp:Label>： <%--到--%>
                                    </td>
                                    <td style="width: 15%; white-space: nowrap;">
                                        <asp:TextBox ID="txtEndDate" runat="server" onKeyPress="event.returnValue=false"
                                            CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                            left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtEndDate','y-mm-dd',0);" />
                                    </td>
                                    <td class="InputLabel">
                                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmBASE_WL_AREA_Label3%>"></asp:Label>：<%--是否允许超发--%>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cbCF" runat="server">
                                            <asp:ListItem Value="">全部</asp:ListItem>
                                            <asp:ListItem Value="0">允许</asp:ListItem>
                                            <asp:ListItem Value="1">不允许</asp:ListItem>
                                        </asp:DropDownList>
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
                                <tr>
                                    <td colspan="6" style="text-align: right;">
                                        <asp:Button ID="btnSearch1" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch1_Click"> <%--查询--%>
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" align="center">
                            <%--<asp:CheckBox ID="cboxAllArea" runat="server" Text="关联所有储位" />--%>
                            <asp:DropDownList ID="ddlAllType2" runat="server" Width="150px">
                                <asp:ListItem Value="1">设置所选区域</asp:ListItem>
                                <asp:ListItem Value="2">设置所有区域</asp:ListItem>
                                <asp:ListItem Value="3">取消所有区域关联</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnSave1" runat="server" CssClass="ButtonSave" Text="<%$ Resources:Lang, Common_btnSave%>" OnClick="btnSave1_Click"
                                OnClientClick="return CheckSelArea();" /><%--保存--%>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack1" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
                            &nbsp;&nbsp;
                            <asp:Button ID="Button4" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>" Visible="false">
                            </asp:Button><%--新增--%>
                            &nbsp;&nbsp;<asp:Button ID="Button5" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_Delete%>"
                                CssClass="ButtonDel" Visible="false" OnClientClick="return CheckDel()" /> <%--删除--%>	
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top">
                         <%--   <cc1:DataGridNavigator3 ID="grdBAS_AREA1" runat="server" GridID="grdBAS_AREA" ShowPageNumber="false"
                                ExcelName="BASE_AREA" IsDbPager="True" GetExportToExcelSource="grdBAS_AREA1_GetExportToExcelSource" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 300px; overflow-x: scroll; width: 100%" id="Div1">
                                <asp:GridView ID="grdBAS_AREA" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    OnPageIndexChanged="grdBAS_AREA_PageIndexChanged" OnPageIndexChanging="grdBAS_AREA_PageIndexChanging"
                                    OnRowDataBound="grdBAS_AREA_RowDataBound" CssClass="Grid" PageSize="15" OnDataBinding="grdBAS_AREA_DataBinding">
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
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="AREA_NAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_WL_AREA_lblPALLETID%>"><%--区域名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MEMO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO%>"><%--备注--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="25%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HANDOVER_CARGO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_WL_AREA_labBlCw%>"> <%--備料儲位--%>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="False" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HANDOVER_CARGO_NAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_WL_AREA_HANDOVER_CARGO_NAME%>"><%--備料儲位名稱--%>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <HeaderStyle HorizontalAlign="Left" Wrap="False" Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FLAG" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_WL_AREA_FLAG%>"><%--是否超发--%>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <HeaderStyle HorizontalAlign="Left" Wrap="False" Width="10%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IPRIORITY%>" HeaderStyle-HorizontalAlign="Center"><%--优先级--%>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtYXJ" runat="server" Text="0" ToolTip="<%$ Resources:Lang, FrmBASE_WL_AREA_txtYXJ%>" Width="50%" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox><%--数字小优先级高--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <%--   <asp:BoundField DataField="CREATEOWNER" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, Common_CreateDate%>"  >
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>--%>
                                    </Columns>
                                </asp:GridView>

                                <ul class="OneRowStyle">
                                    <li >
                                         <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager2_PageChanged"
                                           FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                        <div><%= Resources.Lang.Base_Gong%><%=AspNetPager2.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                                    </li>
                                </ul>

                                <script type="text/javascript" language="javascript">
                                    //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                                    //settingPad("<%= grdBAS_AREA.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
                            var controls = document.getElementById("<%=grdBAS_AREA.ClientID %>").getElementsByTagName("input");

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
                             if (confirm("<%= Resources.Lang.FrmALLOCATEEdit_Msg07%>")) {  //你确认删除吗？
                                return true;
                            }
                            else {
                                return false;
                            }
                        }
                    </script>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
