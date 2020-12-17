<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="FrmMixed_D.aspx.cs" Inherits="Apps_RD_FrmMixed_D" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="ShowOutASNMixed_Div.ascx" TagName="ShowOutASNMixed_Div" TagPrefix="ucOA" %>
<%@ Register Assembly="DreamTek.ASRS.Business" Namespace="DreamTek.ASRS.Business" TagPrefix="ExcelBtn" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/Js/Help.js" type="text/javascript"></script>
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js" type="text/javascript">></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            font: 76%/1.5 Arial,sans-serif;
            background: #FFF;
            color: #333;
        }

        div#container
        {
            width: 500px;
            margin: 0 auto;
        }

        h1
        {
            color: #3CA3FF;
            margin: 1em 0 0;
            letter-spacing: -2px;
        }

        p
        {
            margin: 0 0 1.7em;
        }

        a
        {
            color: #F60;
            font-weight: bold;
        }

            a:hover
            {
                color: #F00;
            }
    </style>
    <style type="text/css">
        .ui-autocomplete-loading
        {
            background: white url('../../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }

        .select
        {
            cursor: hand;
            position: relative;
            left: -25px;
            top: 4px;
        }
    </style>
    <script type="text/javascript">

        function SetOutMixedValue(ControlName, Values, ControlName2, Values2/*, ControlName3, Values3, ControlName4, Values4*/) {
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
            // document.all(ControlName3).value = Values3;
            //document.all(ControlName4).value = Values4;
            GetOutBillData(Values2);
        }
        function GetOutBillData(outAsnID) {
            var outbillcode = outAsnID;
            var i = Math.random() * 10000 + 1;
            if (outbillcode != "") {
                $.get(
                "GetOutBillInfoById.aspx?cerpcode=" + outbillcode + "&i=" + i,
                "",
                function (data) {
                    var datas = data.split("|");

                    //document.getElementById("ctl00_ContentPlaceHolderMain_txtERP_No").value = datas[0];
                    //document.getElementById("ctl00_ContentPlaceHolderMain_txtPalledCode").value = datas[1];
                    document.getElementById("ctl00_ContentPlaceHolderMain_txtITYPE").value = datas[0];

                    //$("#ctl00_ContentPlaceHolderMain_hdnCerpCode").val(datas[0]); //erpcode
                    //$("#ctl00_ContentPlaceHolderMain_hdnPalletCode").val(datas[1]); //栈板/箱号
                    $("#ctl00_ContentPlaceHolderMain_hdnOtype").val(datas[0]); //出库类型

                    //$("#ctl00_ContentPlaceHolderMain_txtERP_No").text(datas[0]); //erpcode
                    //$("#ctl00_ContentPlaceHolderMain_txtPalledCode").text(datas[1]); //栈板/箱号

                },
                ""
            );
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmMixedList_Title01%>-&gt;<%= Resources.Lang.FrmMixed_D_Title01%>      <%-- 配料管理-&gt;配料单详情--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <ucOA:ShowOutASNMixed_Div ID="ucShowOutASNMixed_Div" runat="server" />
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
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

                                .style1
                                {
                                    height: 29px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                           <asp:Label ID="Label21"  runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmMixedList_Label1%>"></asp:Label>：<%--配料单单号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtMixedCode" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtMixedCode" runat="server" ControlToValidate="txtMixedCode"
                                ErrorMessage="<%$ Resources:Lang, FrmMixed_D_Msg01%>" Display="None"> </asp:RequiredFieldValidator>  <%--请填写配料单单号!--%>
                            <asp:HiddenField ID="hiddenGuid" runat="server" />
                        </td>
                        <%--<td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="出库单单号："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtOutCticketCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                           <asp:Label ID="Label21"  runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvtxtOutCticketCode" runat="server" ControlToValidate="txtOutCticketCode"
                                ErrorMessage="请选择出库单单号!" Display="None"> </asp:RequiredFieldValidator>
                            <asp:HiddenField ID="hiddenGuid" runat="server" />
                        </td>--%>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label22"  runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="ERPCODE："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtERP_No" runat="server" CssClass="NormalInputText" Width="95%" ></asp:TextBox>                           
                            <asp:RequiredFieldValidator ID="rfvtxtERP_No" runat="server" ControlToValidate="txtERP_No"
                                ErrorMessage="<%$ Resources:Lang, FrmMixed_D_Msg02%>" Display="None"> </asp:RequiredFieldValidator> <%-- 请选择ERPCODE!--%>
                            <asp:HiddenField ID="hdnCerpCode" runat="server" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                         <asp:Label ID="Label9"  runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmMixedList_Label4%>"></asp:Label>：  <%--栈板/箱号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPalledCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>                            
                            <asp:RequiredFieldValidator ID="rfvtxtPalledCode" runat="server" ControlToValidate="txtPalledCode"
                                ErrorMessage="<%$ Resources:Lang, FrmMixed_D_Msg03%>" Display="None"> </asp:RequiredFieldValidator>  <%--请填写栈板/箱号!--%>
                            <asp:HiddenField ID="hdnPalletCode" runat="server" />
                        </td>

                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label10"  runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, FrmMixedList_lblITYPE%>"></asp:Label>： <%--出库类型：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="txtITYPE" runat="server" Width="95%" Enabled="false">
                            </asp:DropDownList>                           
                            <%--<asp:RequiredFieldValidator ID="rfvtxtITYPE" runat="server" ControlToValidate="txtITYPE"
                                ErrorMessage="请选择出库类型!" Display="None"> </asp:RequiredFieldValidator>--%>
                            <asp:HiddenField ID="hdnOtype" runat="server" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label11"  runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：  <%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%" Enabled="false">
                            </asp:DropDownList>
                           
                            <asp:RequiredFieldValidator ID="rfvddlCSTATUS" runat="server" ControlToValidate="ddlCSTATUS"
                                ErrorMessage="<%$ Resources:Lang, Common_SelectStatus%>" Display="None"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmMixed_D_Label5%>"></asp:Label>： <%--起始站点：--%>
                        </td>
                        <td style="width: 20%;">
                         <%--   <asp:TextBox ID="txtStartSite" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>--%>
                             <asp:DropDownList ID="ddlStartSite" runat="server" Width="95%" Enabled="false">
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        
                        <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmMixed_D_Label3%>"></asp:Label>：<%-- 目的站点：--%>
                        </td>
                        <td style="width: 20%;">
                           <%-- <asp:TextBox ID="txtEndSite" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>--%>
                              <asp:DropDownList ID="ddlEndSite" runat="server" Width="95%"  Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, FrmMixedList_lblCCREATEOWNERCODE%>"></asp:Label> ：<%--配料人：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCreaterUser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="<%$ Resources:Lang, FrmMixedList_lblDCREATETIMEFromFrom%>"></asp:Label>：<%--配料时间：--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtCreaterTime" runat="server"
                                CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmMixed_D_Label6%>"></asp:Label>：<%-- 最后修改人：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtModifUser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmMixed_D_Label7%>"></asp:Label>： <%--最后修改时间：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtModifTime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                             <asp:Label ID="Label12"  runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, FrmMixed_D_Label12%>"></asp:Label>：<%--配料位：--%>
                        </td>
                         <td style="width: 20%">
                            <asp:DropDownList ID="ddlSiteType" runat="server" Width="95%" >
                            </asp:DropDownList>                           
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSiteType"
                                ErrorMessage="<%$ Resources:Lang, FrmMixed_D_Msg04%>" Display="None"> </asp:RequiredFieldValidator> <%--请选择配料位!--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblRemark" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>： <%--备注--%>
                        </td>
                        <td style="width: 20%" colspan="5">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="NormalInputText" Width="98%" MaxLength="100" Height="33px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="6">
                            <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <asp:TextBox ID="txtCDEFINE2" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" 
                                OnClick="btnSearch_Click"></asp:Button> <%--查询--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server"
                    DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left" class="style1">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" 
                    Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp;
               <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" />  <%--返回--%>
            </td>
        </tr>
    </table>

    <table id="Table1" style="height: 100%; width: 100%">
        <tr valign="top">
            <td valign="top" align="left" colspan="4">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button><%--新增--%>
                &nbsp;&nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Lang, Common_Delete%>" OnClick="btnDelete_Click"
                    CssClass="ButtonDel"   OnClientClick="return CheckDel()"/> <%--删除--%>	
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdMIXED_D" runat="server" AllowPaging="True" BorderColor="Teal"
                        DataKeyNames="IDS"
                        BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="1" Width="100%" AutoGenerateColumns="False"
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
                            <asp:BoundField DataField="IDS" DataFormatString="{0:0}" HeaderText="IDS" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_LiaoHao%>">  <%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="QTY" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang, Common_IQUANTITY%>">  <%--数量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATERUSERName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmMixedList_lblCCREATEOWNERCODE%>"> <%--配料人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATERTIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HeaderText="<%$ Resources:Lang, FrmMixedList_lblDCREATETIMEFromFrom%>"> <%--配料时间--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
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
                            <div><%= Resources.Lang.Base_Gong%><%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                        </li>
                    </ul>
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdMIXED_D.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
                var controls = document.getElementById("<%=grdMIXED_D.ClientID %>").getElementsByTagName("input");

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
    <div id="showResultDiv" onmouseout="closeDisplay()" style="background: #DFF8FF; position: absolute; z-index: 10; display: block;">
        <select id="showResultSelect" name="showResultSelect" size="15" style="display: none; background: #DFF8FF;"
            onchange="setTextValue(this.value)">
        </select>
    </div>
    <script type="text/javascript">
        function ChangeDivWidth() {
            document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
        }
        window.onresize = ChangeDivWidth;
        ChangeDivWidth();
    </script>
</asp:Content>
