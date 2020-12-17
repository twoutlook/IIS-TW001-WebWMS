<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--<%$ Resources:Lang, FrmBase_CraneConfig_Title01%>" CodeFile="FrmBase_CraneConfig.aspx.cs" Inherits="BASE_FrmBASE_CraneConfig" %><%--线别管理--%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />

    <style type="text/css">
        body
        {
            padding: 0;
            font: 12px "宋体";
        }
        /*Tab1*/ #lib_Tab1
        {
            width: 100%;
            margin: 0 auto;
            padding: 0px;
            margin-top: 2px;
            margin-bottom: 2px;
        }
        /*Tab2*/ #lib_Tab2
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
                width: 1100px;
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
                background: url( "../Templets/newimages/search.gif" );
                border: 0;
                cursor: pointer;
            }

        .btnExpert
        {
            width: 63px;
            height: 22px;
            background: url( "../Templets/newimages/export.gif" );
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
     <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.FrmBase_CraneConfig_Title02%><%--基礎資料-&gt;WCS管理--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:HiddenField ID="hTabIndex" runat="server" Value="1" />
    <asp:HiddenField ID="hTabIndex1" runat="server" Value="1" />
    <div id="lib_Tab1" style="height: 100%; width: 95%">
        <div class="lib_Menubox lib_tabborder">
            <ul>
                <li id="one1" runat="server" class="one1" onclick="WMSUI.SetTopTab('one',1,7)"><%= Resources.Lang.FrmBase_CraneConfig_one1%></li><%--立库--%>
                <li id="one2" runat="server" class="one2" onclick="WMSUI.SetTopTab('one',2,7)"><%= Resources.Lang.FrmBase_CraneConfig_one2%></li><%--固定式扫描器--%>
                <li id="tabRGV" runat="server" class="one3" onclick="WMSUI.SetTopTab('one',3,7)"><%= Resources.Lang.FrmBase_CraneConfig_one3%></li><%--RGV--%>
                <li id="tabAGV" runat="server" class="one4" onclick="WMSUI.SetTopTab('one',4,7)"><%= Resources.Lang.FrmBase_CraneConfig_one4%></li><%--AGV--%>
                <li id="tabSJJ" runat="server" class="one5" onclick="WMSUI.SetTopTab('one',5,7)"><%= Resources.Lang.FrmBase_CraneConfig_one5%></li><%--提升机--%>
                <li id="tabCar" runat="server" class="one6" onclick="WMSUI.SetTopTab('one',6,7)"><%= Resources.Lang.FrmSTOCK_CHECKBILLEdit1_TaiChe %></li><%--台车--%>
                <li id="tabSSX" runat="server" class="one7" onclick="WMSUI.SetTopTab('one',7,7)"><%= Resources.Lang.WMS_Common_Element_ShuSongXian %></li><%--输送线--%>
            </ul>
        </div>
        <div class="lib_Contentbox lib_tabborder">
            <div id="con_one_1" style="display: none">
                <table id="TabMain" style="height: 100%; width: 100%">       
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                id="tabCondition">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                        height="11" alt="" runat="server" id="titleImg" />
                                        <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                                        </script>
                                        <img id="imgCollapse" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
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
                                        <asp:Label ID="lblCraneID" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblCraneID%>"></asp:Label>： <%--线别编号--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCraneNAME" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblCraneNAME%>"></asp:Label>：<%--线别名称--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneNAME" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>

                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCraneIP" runat="server" Text="CraneIP："></asp:Label>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneIP" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>


                                </tr>
                                <tr>


                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblGroupID" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblGroupID%>"></asp:Label>：<%--组编号--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtGroupID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblSiteCount" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblSiteCount%>"></asp:Label>：<%--站点数量--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtSiteCount" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>


                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCraneSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:DropDownList ID="ddlCraneSTATUS" runat="server" Width="95%">
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
                                        <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"></asp:Button>  <%--查询--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" align="left">&nbsp;&nbsp;<asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button> <%--新增--%>
                            &nbsp;&nbsp;<asp:Button ID="btnDiscontinue" OnClick="btnDiscontinue_Click" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_btnDiscontinue%>"
                                CssClass="ButtonDel" OnCraneClick="return CheckDiscontinue()" /> <%--停用--%>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">

                                <asp:GridView ID="grdBASE_Crane" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="true"
                                    OnRowDataBound="grdBASE_Crane_RowDataBound" CssClass="Grid" PageSize="15">
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
                                        <asp:BoundField DataField="ID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="false"> <%--编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRANEID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_lblCraneID%>"> <%--线别编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRANENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_lblCraneNAME%>"><%--线别名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="CRANEIP" DataFormatString="" HeaderText="CraneIP">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="GROUPID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_lblGroupID%>"><%--组编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="SITECOUNT" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_lblSiteCount%>"><%--站点数量--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                        </asp:BoundField>




                                        <%--     <asp:BoundField DataField="CREATEOWNER" DataFormatString="" HeaderText="创建人">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                    </asp:BoundField>--%>
                                        <%-- <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="创建日期">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                                    </asp:BoundField>--%>
                                        <asp:BoundField DataField="FLAGName" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"> <%--状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                            DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%>
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

                            </div>
                        </td>
                    </tr>
                    <script type="text/javascript">
                        function ChangeDivWidth() {
                            document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
                        }
                        window.onresize = ChangeDivWidth;
                        ChangeDivWidth();

                        function CheckDiscontinue() {
                            alert('1');
                            var number = 0;
                            var controls = document.getElementById("<%=grdBASE_Crane.ClientID %>").getElementsByTagName("input");
                            alert('2');
                            for (var i = 0; i < controls.length; i++) {
                                var e = controls[i];
                                if (e.type != "CheckBox") {
                                    if (e.checked == true) {
                                        number = number + 1;
                                    }
                                }
                            }
                            if (number == 0) {
                                alert("<%= Resources.Lang.FrmBase_CraneConfig_Msg01%>"); //请选择需要停用的项！
                                return false;
                            }
                            if (confirm("<%= Resources.Lang.FrmBase_CraneConfig_Msg02%>")) { //你确认停用吗？
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
                <table id="TableScan" style="height: 100%; width: 100%">
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                id="Table2">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                        height="11" alt="" runat="server" id="Img1" />
                                        <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                                        </script>
                                        <img id="img2" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
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
                                        <asp:Label ID="lblScanId" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblScanId%>"></asp:Label>：<%--扫描器编号--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtSacnID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCCLIENTNAME" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblCCLIENTNAME%>"></asp:Label>：<%--扫描器名称--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtSacnName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%"></td>
                                    <td style="width: 20%"></td>
                                </tr>

                                <tr>
                                    <td colspan="6" style="text-align: right;">
                                        <asp:Button ID="btnScanSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="BtnScanSearch_Click"></asp:Button><%--查询--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" align="left">
                            <asp:Button ID="btnSacnAdd" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button> <%--新增--%>
                            &nbsp;&nbsp;<asp:Button ID="btnDiscontinue_Scane" OnClick="btnDiscontinue_Scane_Click" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_btnDiscontinue%>"
                                CssClass="ButtonDel" /><%--停用--%>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top"></td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div1">

                                <asp:GridView ID="grdBASE_CraneDetailScan" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="true"
                                    OnRowDataBound="grdBASE_CraneDetailScan_RowDataBound" CssClass="Grid" PageSize="15">
                                    <PagerSettings Visible="False" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <RowStyle HorizontalAlign="Left" Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
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

                                        <asp:BoundField DataField="ID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="false"> <%--编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="right" Wrap="False" />
                                        </asp:BoundField>


                                        <asp:BoundField DataField="SCANID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_lblScanId%>"><%--扫描器编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SCANNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_lblCCLIENTNAME%>"><%--扫描器名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SCANIP" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_SCANIP%>"><%--扫描器IP--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SITEID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label5%>"><%--站点编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FLAGName" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"> <%--状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                            DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                </asp:GridView>
                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:aspnetpager id="AspNetPager2" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="AspNetPager2_PageChanged"
                                           FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                            alwaysshow="true" reverseurlpageindex="True" urlpaging="false" xmlns:webdiyer="wuqi.webdiyer">
                                        </webdiyer:aspnetpager>
                                    </li>
                                    <li>
                                        <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                                    </li>
                                </ul>
                                <script type="text/javascript" language="javascript">
                                    //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                                    //settingPad("<%= grdBASE_CraneDetailScan.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
            </div>
            <div id="con_one_3" style="display: none">
                <table id="TableRGV" style="height: 100%; width: 100%">
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                id="Table3">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                        height="11" alt="" runat="server" id="Img3" />
                                        <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                                        </script>
                                        <img id="img4" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
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
                                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label1%>"></asp:Label>：<%--RGV编号--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneID_RGV" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label2%>"></asp:Label>：<%--RGV名称--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneName_RGV" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>

                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label3%>"></asp:Label>：<%--IP地址--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneIP_RGV" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label4%>"></asp:Label>： <%--设备名称--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtEQUIPMENTNAME_RGV" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>

                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:DropDownList ID="ddlSTATUS_RGV" runat="server" Width="95%">
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
                                        <asp:Button ID="btnSearchRGV" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearchRGV_Click"></asp:Button><%--查询--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" align="left">&nbsp;&nbsp;<asp:Button ID="btnRGVAdd" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button> <%--新增--%>
                            &nbsp;&nbsp;<asp:Button ID="btnDiscontinue_RGV" OnClick="btnDiscontinue_RGV_Click" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_btnDiscontinue%>"
                                CssClass="ButtonDel" /><%--停用--%>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div2">

                                <asp:GridView ID="grdRGV" runat="server" AllowPaging="True" BorderColor="Teal" OnRowDataBound="grdRGV_RowDataBound"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="true"
                                    CssClass="Grid" PageSize="15">
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
                                        <asp:BoundField DataField="ID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="false"> <%--编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRANEID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label1%>"><%--RGV编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRANENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label2%>"><%--RGV名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="CRANEIP" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label3%>"><%--IP地址--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="EQUIPMENTNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label4%>"><%--设备名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FLAGName" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"> <%--状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                            DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                </asp:GridView>

                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:AspNetPager ID="AspNetPager3" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager3_PageChanged"
                                           FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                         <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager3.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
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

                        function CheckDel() {
                            var number = 0;
                            var controls = document.getElementById("<%=grdBASE_Crane.ClientID %>").getElementsByTagName("input");

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
            </div>
            <div id="con_one_4" style="display: none;">
                <div class="lib_Menubox lib_tabborder">
                    <ul>
                        <li id="two1" onclick="setTab1('two',1,2)" class="hover"><%= Resources.Lang.FrmBase_CraneConfig_two1%></li><%--AGV车--%>
                        <li id="two2" onclick="setTab1('two',2,2)"><%= Resources.Lang.FrmBase_CraneConfig_two2%></li><%--AGV站点--%>
                    </ul>
                </div>
                <div class="lib_Contentbox lib_tabborder">
                    <div id="con_two_1">
                        <table id="TableAGV" style="height: 100%; width: 100%">
                            <tr valign="top">
                                <td valign="top">
                                    <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                        id="Table4">
                                        <tr>
                                            <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                                height="11" alt="" runat="server" id="Img5" />
                                                <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                                            </th>
                                            <th style="border-left-width: 0px" align="right">
                                                <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                                                </script>
                                                <img id="img6" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
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
                                                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label7%>"></asp:Label>： <%--AGV编号--%>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txtCraneID_AGV" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                            </td>
                                            <td class="InputLabel" style="width: 13%">
                                                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label8%>"></asp:Label>：<%--AGV名称--%>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txtCraneName_AGV" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                            </td>
                                            <td class="InputLabel" style="width: 13%">
                                                <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label9%>"></asp:Label>： <%--供应商--%>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:DropDownList ID="ddlSUPPLIER_AGV" runat="server" Width="95%">
                                                    <asp:ListItem Selected="True" Value="">全部</asp:ListItem>
                                                    <asp:ListItem Value="0">海康威视</asp:ListItem>
                                                    <asp:ListItem Value="1">其他</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>

                                            <%--<td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label11" runat="server" Text="类型："></asp:Label>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtTTYPE_AGV" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>--%>
                                            <td class="InputLabel" style="width: 13%">
                                                <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:DropDownList ID="ddlSTATUS_AGV" runat="server" Width="95%">
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
                                                <asp:Button ID="btnSearchAGV" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearchAGV_Click"></asp:Button><%--查询--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td valign="top" align="left">&nbsp;&nbsp;<asp:Button ID="btnAGVAdd" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button> <%--新增--%>
                                    &nbsp;&nbsp;<asp:Button ID="btnDiscontinue_AGV" OnClick="btnDiscontinue_AGV_Click" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_btnDiscontinue%>"
                                        CssClass="ButtonDel" /><%--停用--%>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div3">

                                        <asp:GridView ID="grdAGV" runat="server" AllowPaging="True" BorderColor="Teal"
                                            BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="true" OnRowDataBound="grdAGV_RowDataBound"
                                            CssClass="Grid" PageSize="15">
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
                                                <asp:BoundField DataField="ID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="false"> <%--编号--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CRANEID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label7%>"><%--AGV编号--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CRANENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label8%>"><%--AGV名称--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="SUPPLIER" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label9%>"><%--供应商--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                </asp:BoundField>

                                                <%--<asp:BoundField DataField="TTYPE" DataFormatString="" HeaderText="类型">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>--%>
                                                <asp:BoundField DataField="FLAGName" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"> <%--状态--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                                    DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                </asp:HyperLinkField>
                                            </Columns>
                                        </asp:GridView>

                                        <ul class="OneRowStyle">
                                            <li>
                                                <webdiyer:AspNetPager ID="AspNetPager4" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager4_PageChanged"
                                                   FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                                    AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                                </webdiyer:AspNetPager>
                                            </li>
                                            <li>
                                                 <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager4.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
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

                                function CheckDel() {
                                    var number = 0;
                                    var controls = document.getElementById("<%=grdBASE_Crane.ClientID %>").getElementsByTagName("input");

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
                    </div>
                    <div id="con_two_2">
                        <table id="Table1" style="height: 100%; width: 100%">
                            <tr valign="top">
                                <td valign="top">
                                    <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                        id="Table5">
                                        <tr>
                                            <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                                height="11" alt="" runat="server" id="Img9" />
                                                <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                                            </th>
                                            <th style="border-left-width: 0px" align="right">
                                                <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                                                </script>
                                                <img id="img10" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
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
                                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label5%>"></asp:Label>：<%--站点编号--%>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txtSiteID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                            </td>
                                            <td class="InputLabel" style="width: 13%">
                                                <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label10%>"></asp:Label>：<%--站点名称--%>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txtSiteName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                            </td>
                                            <td class="InputLabel" style="width: 13%">
                                                <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label11%>"></asp:Label>：<%--站点类型--%>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:DropDownList ID="ddlAGVSITETYPE" runat="server" Width="95%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InputLabel" style="width: 13%">
                                                <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label19%>"></asp:Label>：<%--楼层--%>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txtSTOREY" runat="server" CssClass="NormalInputText" Width="95%"
                                                    MaxLength="30"></asp:TextBox>
                                            </td>

                                            <td class="InputLabel" style="width: 13%">
                                                <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:DropDownList ID="ddlCSTATUS_AS" runat="server" Width="95%">
                                                </asp:DropDownList>
                                            </td>
                                              <td class="InputLabel" style="width: 13%">
                                                <asp:Label ID="Label20" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label20%>"></asp:Label> ： <%--是否占用--%>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:DropDownList ID="ddlISOCCUPY" runat="server" Width="95%">                                                
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
                                                <asp:Button ID="btnSearchAGVS" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearchAGVS_Click"></asp:Button><%--查询--%>
                                                <asp:HiddenField ID="hdnblckIndex" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td valign="top" align="left">&nbsp;&nbsp;<asp:Button ID="btnNewAGVS" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button> <%--新增--%>
                                    &nbsp;&nbsp;<asp:Button ID="btnDiscontinueAGVS" OnClick="btnDiscontinueAGVS_Click" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_btnDiscontinue%>"
                                        CssClass="ButtonDel" /><%--停用--%>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div5">

                                        <asp:GridView ID="grdAGVSite" runat="server" AllowPaging="True" BorderColor="Teal"
                                            BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="true" OnRowDataBound="grdAGVSite_RowDataBound"
                                            CssClass="Grid" PageSize="15">
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
                                                <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="ID" Visible="false">
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="right" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SITEID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label5%>"><%--站点编号--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SITENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label10%>"><%--站点名称--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AGVSITETYPEName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label11%>"><%--站点类型--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="STOREY" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label19%>"><%--楼层--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="STOREYLIMIT" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_STOREYLIMIT%>"><%--楼层限制--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                </asp:BoundField>                                              
                                                <asp:BoundField DataField="FLAGName" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"> <%--状态--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                </asp:BoundField>
                                                  <asp:BoundField DataField="OCCUPYName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_IS_OCCUPY%>"><%--占用状态--%>
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="REMARK" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO%>" Visible="false"> <%--备注--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                                    DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                </asp:HyperLinkField>
                                            </Columns>
                                        </asp:GridView>

                                        <ul class="OneRowStyle">
                                            <li>
                                                <webdiyer:AspNetPager ID="AspNetPager6" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager6_PageChanged"
                                                   FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                                    AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                                </webdiyer:AspNetPager>
                                            </li>
                                            <li>
                                                <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager6.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
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

                                function CheckDel() {
                                    var number = 0;
                                    var controls = document.getElementById("<%=grdAGVSite.ClientID %>").getElementsByTagName("input");

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
                    </div>
                </div>
            </div>
            <div id="con_one_5" style="display: none">
                <table id="TableSJJ" style="height: 100%; width: 100%">
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                id="Table6">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                        height="11" alt="" runat="server" id="Img7" />
                                        <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                                        </script>
                                        <img id="img8" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
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
                                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label13%>"></asp:Label>：<%--提升机编号--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneID_SJJ" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label14%>"></asp:Label>：<%--提升机名称--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneName_SJJ" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>

                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label15%>"></asp:Label>：<%--提升机IP--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneIP_SJJ" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>


                                </tr>
                                <tr>


                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label16%>"></asp:Label>：<%--设备名称--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtEQUIPMENTNAME_SJJ" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <%--<td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label17" runat="server" Text="类型："></asp:Label>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtTTYPE_SJJ" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>--%>


                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:DropDownList ID="ddlSTATUS_SJJ" runat="server" Width="95%">
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
                                        <asp:Button ID="btnSearchSJJ" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearchSJJ_Click"></asp:Button><%--查询--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" align="left">&nbsp;&nbsp;<asp:Button ID="btnSJJAdd" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button> <%--新增--%>
                            &nbsp;&nbsp;<asp:Button ID="btnDiscontinue_SJJ" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_btnDiscontinue%>" OnClick="btnDiscontinue_SJJ_Click"
                                CssClass="ButtonDel" /><%--停用--%>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div4">

                                <asp:GridView ID="grdSJJ" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="true" OnRowDataBound="grdSJJ_RowDataBound"
                                    CssClass="Grid" PageSize="15">
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
                                        <asp:BoundField DataField="ID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="false"> <%--编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRANEID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label13%>"><%--提升机编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRANENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label14%>"><%--提升机名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="CRANEIP" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label15%>"><%--提升机IP--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="EQUIPMENTNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label16%>"><%--设备名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>

                                        <%--<asp:BoundField DataField="TTYPE" DataFormatString="" HeaderText="类型">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="FLAGName" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"> <%--状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                            DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                </asp:GridView>

                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:AspNetPager ID="AspNetPager5" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager5_PageChanged"
                                           FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                         <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager5.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
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

                        function CheckDel() {
                            var number = 0;
                            var controls = document.getElementById("<%=grdBASE_Crane.ClientID %>").getElementsByTagName("input");

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
            </div>
            <div id="con_one_6" style="display: none">
                <table id="TableCAR" style="height: 100%; width: 100%">
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="Table8">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="Img11" />
                                        <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <img id="img12" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center" onclick="CollapseCondition('../../');return false;" />
                                    </th>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_TaiCheNo %>"></asp:Label>：<%--台车编号--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneID_CAR" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_TaiCheName %>"></asp:Label>：<%--台车名称--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneName_CAR" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>

                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label3%>"></asp:Label>：<%--IP地址--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneIP_CAR" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label4%>"></asp:Label>： <%--设备名称--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtEQUIPMENTNAME_CAR" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>

                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label25" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:DropDownList ID="ddlSTATUS_CAR" runat="server" Width="95%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="text-align: right;">
                                        <asp:Button ID="btnSearchCAR" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearchCAR_Click"></asp:Button><%--查询--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" align="left" style="padding:15px 0px;">
                            <asp:Button ID="btnCARAdd" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button> <%--新增--%>
                            &nbsp;&nbsp;<asp:Button ID="btnDiscontinue_CAR" OnClick="btnDiscontinue_CAR_Click" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_btnDiscontinue%>" CssClass="ButtonDel" /><%--停用--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div6">

                                <asp:GridView ID="grdCAR" runat="server" AllowPaging="True" BorderColor="Teal" OnRowDataBound="grdCAR_RowDataBound"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="true"
                                    CssClass="Grid" PageSize="15">
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
                                        <asp:BoundField DataField="ID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="false"> <%--编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRANEID" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_TaiCheNo %>"><%--台车编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRANENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_TaiCheName %>"><%--台车名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EQUIPMENTNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label4%>"><%--设备名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRANEIP" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label3%>"><%--IP地址--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FLAGName" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"> <%--状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                            DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                </asp:GridView>

                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:AspNetPager ID="AspNetPager7" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager7_PageChanged"
                                           FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                         <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager7.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
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

                        function CheckDel() {
                            var number = 0;
                            var controls = document.getElementById("<%=grdBASE_Crane.ClientID %>").getElementsByTagName("input");

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
            </div>
            <div id="con_one_7" style="display: none">
                <table id="TableSSX" style="height: 100%; width: 100%">
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="Table9">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="Img13" />
                                        <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <img id="img14" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center" onclick="CollapseCondition('../../');return false;" />
                                    </th>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCraneID_SSX" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ShuSongXianNo %>"></asp:Label>：<%--输送线编号--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneID_SSX" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label27" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_ShuSongXianName %>"></asp:Label>：<%--输送线名称--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneName_SSX" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>

                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label28" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label3%>"></asp:Label>：<%--IP地址--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCraneIP_SSX" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label29" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label4%>"></asp:Label>： <%--设备名称--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtEQUIPMENTNAME_SSX" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>

                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label30" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:DropDownList ID="ddlSTATUS_SSX" runat="server" Width="95%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="text-align: right;">
                                        <asp:Button ID="btnSearchSSX" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearchSSX_Click"></asp:Button><%--查询--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" align="left" style="padding:15px 0px;">
                            <asp:Button ID="btnSSXAdd" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button> <%--新增--%>
                            &nbsp;&nbsp;<asp:Button ID="btnDiscontinue_SSX" OnClick="btnDiscontinue_SSX_Click" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_btnDiscontinue%>" CssClass="ButtonDel" /><%--停用--%>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div style="height: 450px; overflow-x: scroll; width: 100%" id="Div8">

                                <asp:GridView ID="grdSSX" runat="server" AllowPaging="True" BorderColor="Teal" OnRowDataBound="grdSSX_RowDataBound"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="true"
                                    CssClass="Grid" PageSize="15">
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
                                        <asp:BoundField DataField="ID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="false"> <%--编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRANEID" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ShuSongXianNo %>"><%--输送线编号--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRANENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_Element_ShuSongXianName %>"><%--输送线名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EQUIPMENTNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label4%>"><%--设备名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CRANEIP" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label3%>"><%--IP地址--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FLAGName" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"> <%--状态--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                            DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>">  <%--编辑--%>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                </asp:GridView>

                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:AspNetPager ID="AspNetPager8" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager8_PageChanged"
                                           FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                        </webdiyer:AspNetPager>
                                    </li>
                                    <li>
                                         <div><%= Resources.Lang.Base_Gong%> <%=AspNetPager7.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                                    </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var WMSUI = {
            FormFed: {
                TopTab: $("#<%= hTabIndex.ClientID %>"),
                SubTab: $("#<%= hTabIndex1.ClientID %>")
            },
            Init: function () {
                var indexT = this.FormFed.TopTab.val();
                var indexT1 = this.FormFed.SubTab.val();
                $(".one" + indexT).first().click();
                document.getElementById("two" + indexT1).click();
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

        function setTab1(name, cursel, n) {
            document.getElementById("ctl00_ContentPlaceHolderMain_hTabIndex1").value = cursel;
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                var con = document.getElementById("con_" + name + "_" + i);
                menu.className = i == cursel ? "hover" : "";
                con.style.display = i == cursel ? "block" : "none";

            }
        }
        window.onload = function () {
            var indexT1 = document.getElementById("ctl00_ContentPlaceHolderMain_hTabIndex1").value;
            document.getElementById("two" + indexT1).click();
        }

    </script>

</asp:Content>
