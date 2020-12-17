<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBase_CraneConfigEdit.aspx.cs"
    Inherits="BASE_FrmBase_CraneConfigEdit" Title="--<%$ Resources:Lang, FrmBase_CraneConfigEdit_Title01%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%--Crane详情--%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBase_CraneConfig_Msg09%>-&gt;<%= Resources.Lang.FrmBase_CraneConfigEdit_Title02%><%--立库线别管理-&gt;线别详情--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />

    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
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
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCraneID" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblCraneID%>"></asp:Label>：<%--线别编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCraneID" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtCraneID" runat="server" ControlToValidate="txtCraneID"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_CraneConfigEdit_rfvtxtCraneID%>" Display="None"> </asp:RequiredFieldValidator><%--请填写线别编号!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCraneNAME" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblCraneNAME%>"></asp:Label>：<%--线别名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCraneNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtCraneNAME" runat="server" ControlToValidate="txtCraneNAME"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_CraneConfigEdit_rfvtxtCraneNAME%>!" Display="None"> </asp:RequiredFieldValidator><%--请填写线别名称!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCRANEIP" runat="server" Text="CRANEIP："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCRANEIP" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtCRANEIP" runat="server" ControlToValidate="txtCRANEIP"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_CraneConfigEdit_rfvtxtCRANEIP%>" Display="None"> </asp:RequiredFieldValidator><%--请填写IP!--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span style="position: relative; left: -20px; top: 2px; color: #FF0000; font-weight: bold;">*</span>
                            <asp:Label ID="lblGROUPID" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblGroupID%>"></asp:Label>：<%--组编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtGROUPID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtGROUPID" runat="server" ControlToValidate="txtGROUPID"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_CraneConfigEdit_rfvtxtGROUPID%>" Display="None"> </asp:RequiredFieldValidator>
                            <%--请填写组编号!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblSITECOUNT" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblSiteCount%>"></asp:Label>：<%--站点数量--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSITECOUNT" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtSITECOUNT" runat="server" ControlToValidate="txtSITECOUNT"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_CraneConfigEdit_rfvtxtSITECOUNT%>" Display="None"> </asp:RequiredFieldValidator>
                            <%--请填写站点数量!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span style="position: relative; left: -20px; top: 2px; color: #FF0000; font-weight: bold;">*</span>
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dplCSTATUS" runat="server" Width="95%">
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="rfvtxtCSTATUS" runat="server" ControlToValidate="dplCSTATUS"
                                ErrorMessage="<%$ Resources:Lang, Common_SelectStatus%>" Display="None"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：<%--创建人--%>
                            <%--创建人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtcreateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label>：<%--创建时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtcreatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label3%>"></asp:Label>：
                            <%--修改人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtupdateuser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label4%>"></asp:Label>：
                            <%--修改时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtupdatetime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False"></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmBase_AGV_Label1%>"></asp:Label>：
                            <%--停用人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDISCONTINUEUSER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmBase_AGV_Label7%>"></asp:Label>：<%--停用时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDISCONTINUETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="False"></asp:Label>： <%--编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"
                                Visible="False"></asp:TextBox>
                        </td>
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
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="padding:15px 0px;text-align:center;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>

    <table id="Table1" style="height: 100%; width: 100%">
        <tr valign="top" style="display: none;">
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
                            </style>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_Label1%>"></asp:Label>：<%--主表编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td colspan="1">&nbsp;
                        </td>
                        <td colspan="1">&nbsp;
                        </td>
                        <td colspan="1">&nbsp;
                        </td>
                        <td colspan="1">&nbsp;
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
        <tr valign="top">
            <td valign="top" align="left" colspan="5" style="padding-bottom:15px;">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>" ></asp:Button><%--新增--%>
                &nbsp;&nbsp;
                <asp:Button ID="btnDiscontinue" OnClick="btnDiscontinue_Click" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_btnDiscontinue%>" CssClass="ButtonDel" /><%--停用--%>
            </td>

            <td align="left" style="width: 10%; display: none;">
                <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" CausesValidation="false" OnClick="btnSearch_Click"></asp:Button> <%--查询--%>
            </td>
        </tr>
        <tr>
            <td colspan="6">

                <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdBASE_CraneDetail" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="true"
                        OnRowDataBound="grdBASE_CraneDetail_RowDataBound" CssClass="Grid" PageSize="15">
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
                            <asp:BoundField DataField="ID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="false"><%--编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CRANEID" DataFormatString="" HeaderText="CraneID" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SITEID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label5%>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--站点编号--%>
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SITENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label10%>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--站点名称--%>
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SITETYPEName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label11%>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--站点类型--%>
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DEFULSITE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfigEdit_DEFULSITE%>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--默认站点--%>
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FORMATName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--规格--%>
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REMARK" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO%>"><%--备注--%><HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="150px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="FLAGName" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"><%--状态--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>"><%--编辑--%><HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                        </Columns>
                    </asp:GridView>


                    <ul class="OneRowStyle">
                        <li>
                            <webdiyer:aspnetpager id="AspNetPager1" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="AspNetPager1_PageChanged"
                                firstpagetext="<%$ Resources:Lang, Base_FirstPage %>" lastpagetext="<%$ Resources:Lang, Base_EndPage %>" nextpagetext="<%$ Resources:Lang, Base_NextPage %>" prevpagetext="<%$ Resources:Lang, Base_LastPage %>" showpageindexbox="Never"
                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false" xmlns:webdiyer="wuqi.webdiyer">
                                                              </webdiyer:aspnetpager>
                        </li>
                        <li>
                            <div><%= Resources.Lang.Base_Gong%><%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                        </li>
                    </ul>

                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdBASE_CraneDetail.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                    </script>
                </div>
                <%--</asp:Panel>--%>
            </td>
        </tr>
        <%-- <script type="text/javascript">
            function ChangeDivWidth() {
                document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
            }
            window.onresize = ChangeDivWidth;
            ChangeDivWidth();

            function CheckDel() {
                var number = 0;
                var controls = document.getElementById("<%=grdBASE_CraneDetail.ClientID %>").getElementsByTagName("input");

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
        </script>--%>


        <script type="text/javascript">
            function ChangeDivWidth() {
                document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
            }
            window.onresize = ChangeDivWidth;
            ChangeDivWidth();

            function CheckDel() {
                var number = 0;
                var controls = document.getElementById("<%=grdBASE_CraneDetail.ClientID %>").getElementsByTagName("input");

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

</asp:Content>
