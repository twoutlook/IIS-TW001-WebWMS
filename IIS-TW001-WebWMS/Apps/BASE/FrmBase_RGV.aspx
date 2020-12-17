<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBase_RGV.aspx.cs" Inherits="Apps_BASE_FrmBase_RGV" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBase_RGV_Title01%>-&gt;<%= Resources.Lang.FrmBase_RGV_Title02%><%--RGV线别管理-&gt;RGV线别详情--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>

    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
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
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCraneID" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label1%>"></asp:Label>：<%--RGV编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCraneID" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtCraneID" runat="server" ControlToValidate="txtCraneID"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_RGV_rfvtxtCraneID%>" Display="None"> </asp:RequiredFieldValidator>
                            <%--请填写RGV编号!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCraneNAME" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label2%>"></asp:Label>：<%--RGV名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCraneNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="30"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtCraneNAME" runat="server" ControlToValidate="txtCraneNAME"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_RGV_rfvtxtCraneNAME%>" Display="None"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCRANEIP" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label3%>"></asp:Label>：<%--IP地址--%>
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
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblGROUPID" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label4%>"></asp:Label>： <%--设备名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtEQUIPMENTNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtEQUIPMENTNAME" runat="server" ControlToValidate="txtEQUIPMENTNAME"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_RGV_rfvtxtEQUIPMENTNAME%>" Display="None"> </asp:RequiredFieldValidator>
                            <%--请填写设备名称!--%>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblGroupID%>"></asp:Label>：<%--组编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtGROUPID" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtGROUPID" runat="server" ControlToValidate="txtGROUPID"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_CraneConfigEdit_rfvtxtGROUPID%>" Display="None"> </asp:RequiredFieldValidator><%--请填写组编号!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span style="position: relative; left: -20px; top: 2px; color: #FF0000; font-weight: bold;">*</span>
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dplCSTATUS" runat="server" Width="95%">
                                <%--<asp:ListItem Value="0" Selected="True">使用中</asp:ListItem>
                                <asp:ListItem Value="1">停用</asp:ListItem>--%>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="rfvtxtCSTATUS" runat="server" ControlToValidate="dplCSTATUS"
                                ErrorMessage="<%$ Resources:Lang, Common_SelectStatus%>" Display="None"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblRemark" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：<%--备注--%>
                        </td>
                        <td style="width: 20%" colspan="5">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="NormalInputText" Width="98%" MaxLength="100" Height="33px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：<%--创建人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCreateUser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label>：<%--创建时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCreateTime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label3%>"></asp:Label>：<%--修改人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtModifyUser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label4%>"></asp:Label>：<%--修改时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtModifyTime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBase_AGV_Label1%>"></asp:Label><%--停用人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDISCONTINUEUSER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmBase_AGV_Label7%>"></asp:Label><%--停用时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDISCONTINUETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
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
                            <asp:HiddenField ID="hdnID" runat="server" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblTTYPE" runat="server" Text="<%$ Resources:Lang, Common_Type%>"></asp:Label>： <%--类型--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtTTYPE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server" DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>

    <table id="Table1" style="height: 100%; width: 100%">
        <tr valign="top">
            <td valign="top" colspan="6"></td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left" colspan="5">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button><%--新增--%>
                &nbsp;&nbsp;
                <asp:Button ID="btnDiscontinue" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_btnDiscontinue%>" OnClick="btnDiscontinue_Click"
                    CssClass="ButtonDel" /><%--停用--%>
            </td>

            <td align="left" style="width: 10%; display: none;">
                <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Style="margin-left: 60%;" Text="<%$ Resources:Lang, Common_btnSearch%>" CausesValidation="false" OnClick="btnSearch_Click"><%--查询--%>
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="6">

                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
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
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CRANEID" DataFormatString="" HeaderText="CraneID" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SITEID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label5%>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--站点编号--%>
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SITENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label10%>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--站点名称--%>
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SITETYPEName" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfig_Label11%>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--站点类型--%>
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ISDEFULSITE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_CraneConfigEdit_DEFULSITE%>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--默认站点--%>
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IS_DEFAULT_IN" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_RGV_IS_DEFAULT_IN%>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--默认入库站--%>
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FORMAT" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>" Visible="false">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--规格--%>
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FLAGName" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--状态--%>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REMARK" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO%>" Visible="false"><%--备注--%><HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PLCREGION" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_RGV_PLCREGION%>">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <%--PLC地址区--%>
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="150px" />
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
