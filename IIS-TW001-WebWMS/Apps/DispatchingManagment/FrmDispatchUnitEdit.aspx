<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDispatchUnitEdit.aspx.cs" Inherits="FrmDispatchUnitEdit"
    Title="--<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/Apps/OUT/ShowFG_CinvCode_Div.ascx" TagName="ShowFG_CinvCode_Div" TagPrefix="uc1" %>



<%@ Register Src="../BASE/ShowBASE_CLIENTDiv.ascx" TagName="ShowBASE_CLIENTDiv" TagPrefix="uc3" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />

    <script type="text/javascript">
        function SetControlValue(ControlName, Values) {
            document.all(ControlName).value = Values;
        }

        //function SetControlValue(ControlName, Values, ControlName2, Values2) {
        //    document.all(ControlName).value = Values;
        //    document.all(ControlName2).value = Values2;
        //}
        function del() {
            //confirm("确认是否导入");
            if (confirm(<%= Resources.Lang.FrmDispatchUnitEdit_MsgTitle31 %>)) {
                // if (confirm(msg)) {
                true;
            } else { false; }
        }
        function Show(divID) {
            //alert(divID);
            disponse_div(event, document.all(divID));
        }

    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmDispatchUnitEdit_MsgTitle2 %> -&gt;<asp:Literal ID="ltPageTable" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle3 %>"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:ShowFG_CinvCode_Div ID="ShowFG_CinvCode_Div1" runat="server" />
    <uc3:ShowBASE_CLIENTDiv ID="ShowBASE_CLIENTDiv1" runat="server" />
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
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

                                .auto-style1
                                {
                                    width: 21%;
                                    height: 24px;
                                }

                                .auto-style2
                                {
                                    width: 20%;
                                    height: 24px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle4 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText"
                                Width="95%" MaxLength="40"></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle5 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlTYPE" runat="server" Width="95%" AutoPostBack="True">

                               <%-- <asp:ListItem Value="0" Text="手动叫车"></asp:ListItem>
                                <asp:ListItem Value="1" Text="产线叫车"></asp:ListItem>
                                <asp:ListItem Value="OUT" Text="出库"></asp:ListItem>
                                <asp:ListItem Value="2" Text="其它(待增加)"></asp:ListItem>--%>
                            </asp:DropDownList>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSO" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle10 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCSO" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>

                        <%--<td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCLIENTCODE" runat="server" Text="原始站点编号："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtloc" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>
                            
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCLIENT" runat="server" Text="目的站点编号："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtNEWLOC" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>
                             
                        </td>--%>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle11 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">

                            <asp:DropDownList ID="ddlMachine" runat="server" Width="95%" Enabled="false">
                               <%-- <asp:ListItem Value="0" Text="升降梯"></asp:ListItem>
                                <asp:ListItem Value="1" Text="AGV"></asp:ListItem>
                                <asp:ListItem Value="2" Text="RGV"></asp:ListItem>
                                <asp:ListItem Value="3" Text="其它(待增加)"></asp:ListItem>--%>
                            </asp:DropDownList>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle14 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="TxtPallet" runat="server" Width="95%"></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle15 %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%" Enabled="false">

                              <%--  <asp:ListItem Value="0" Text="未处理"></asp:ListItem>
                                <asp:ListItem Value="1" Text="处理中"></asp:ListItem>
                                <asp:ListItem Value="2" Text="已完成"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%; height: 24px;">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle16 %>"></asp:Label>：
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                onKeyPress="event.returnValue=false" MaxLength="50" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtCCREATEOWNERCODE" runat="server" ControlToValidate="txtCCREATEOWNERCODE"
                                ErrorMessage="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle17 %>!" Display="None"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%; height: 24px;">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle18 %>"></asp:Label>：
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle19 %>：yyyy-MM-dd" MaxLength="19"></asp:TextBox>


                        </td>
                        <td colspan="4" class="InputLabel"></td>


                    </tr>

                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle20 %>"></asp:Label>：
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtCMEMO" runat="server" TextMode="MultiLine" Width="99%" MaxLength="200"></asp:TextBox>
                        </td>



                    </tr>

                    <tr style="display: none">
                        <td colspan="6">
                            <asp:Label ID="lblID" runat="server" Text="ID：" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"
                                Enabled="False"></asp:TextBox>
                            <!--是否提示-->
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
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <%-- <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                                Text="<%$ Resources:Lang, Common_Save %>" />--%>

                            <%--<asp:Button ID="btnInBom" runat="server" CssClass="ButtonConfig" Text="BOM"
                                OnClick="btnInBom_Click" Visible="False" Style="display: none;"></asp:Button>--%>

                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle21 %>" CausesValidation="false" />

                        </td>
                    </tr>
                </table>
                <table id="TabMain0" style="height: 100%; width: 100%" runat="server">

                    <tr valign="top">
                        <td valign="top" colspan="4">
                            <%--<cc1:datagridnavigator3 id="grdNavigatorOUTASN_D" runat="server" gridid="grdOUTASN_D"
                                showpagenumber="false" excelname="OUTASN_D" isdbpager="True" ongetexporttoexcelsource="grdNavigatorOUTASN_D_GetExportToExcelSource" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">

                                <asp:GridView ID="grdTask_D" runat="server" AllowPaging="True" BorderColor="Teal" ShowHeaderWhenEmpty="true"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                                    Width="100%" AutoGenerateColumns="False"
                                    CssClass="Grid" PageSize="15" OnRowDataBound="grdTask_D_RowDataBound">
                                    <%--OnPageIndexChanged="grdOUTASN_D_PageIndexChanged"
                                    OnPageIndexChanging="grdOUTASN_D_PageIndexChanging"--%>
                                    <PagerSettings Visible="true" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <RowStyle HorizontalAlign="Left" Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                    <Columns>
                                        <asp:BoundField DataField="STEPS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle22 %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="120px" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="LOC" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle23 %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="120px" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="NEWLOC" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle24 %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MACHINE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle25 %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ASRSID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle26 %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="180px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CSTATUSNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle27 %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="120px" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="CREATEUSER" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle28 %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="120px" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle29 %>">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="120px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle30 %>" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnComplete" runat="server" CausesValidation="false" CommandArgument='<%#Eval("IDS") %>'
                                                    CommandName="" Text="<%$ Resources:Lang, FrmDispatchUnitEdit_MsgTitle30 %>" OnClick="lbtnComplete_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"  Width="120px"/>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <ul class="OneRowStyle">
                                    <li>
                                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
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
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server"
                    DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
    </table>
</asp:Content>
