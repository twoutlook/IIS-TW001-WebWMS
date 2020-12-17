<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" Title="--<%$ Resources:Lang, FrmBASE_PARTList_Title01%>" CodeFile="FrmBASE_PARTList.aspx.cs" Inherits="BASE_FrmBASE_PARTList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.FrmBASE_PARTList_Title01%>
    <%--基礎資料-&gt;物料管理--%>
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
                            <asp:Label ID="lblCPARTNUMBER" runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label>： <%--料号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPART" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCPARTNAME" runat="server" Text="批/序號(RANK)"></asp:Label>：<%--批/序號(RANK)--%>
                        </td>
                        <td colspan="1" style="width: 20%">
                            <asp:TextBox ID="txtRANK" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, Common_CinvName1%>"></asp:Label>：<%--品名--%>
                        </td>
                        <td colspan="1" style="width: 20%">
                            <asp:TextBox ID="txtCPARTNAME" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
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
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_Label1%>"></asp:Label>： <%--是否保税--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dpdBond" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTYPE" runat="server" Text="<%$ Resources:Lang, Common_Type%>"></asp:Label><%--类型--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlCTYPE" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_Label2%>"></asp:Label>：<%--类别--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dpdABCType" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblINEEDWARN" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_lblINEEDWARN%>"></asp:Label>：<%--是否预警--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlINEEDWARN" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIsSZCW" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_lblIsSZCW%>"></asp:Label>：<%--是否设置储位--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlSZCW" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblINEEDCHECK" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_lblINEEDCHECK%>"></asp:Label>：<%--是否免检：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlINEEDCHECK" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：<%--创建人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCreateuser" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCreateTimeFrom" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label>：<%--创建时间--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtCreateTimeFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateTimeFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCreateTimeTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao%>"></asp:Label>：<%--到--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtCreateTimeTo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateTimeTo','y-mm-dd',0);" />
                            <span class="requiredSign" style="left: -50px">*</span>
                        </td>

                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDEXPIREDATEFromFrom" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblDEXPIREDATEFromFrom%>"></asp:Label>： <%--终止日期--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDEXPIREDATEFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDEXPIREDATEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDEXPIREDATEToTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao%>"></asp:Label>：<%--到--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDEXPIREDATETo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDEXPIREDATETo','y-mm-dd',0);" />
                            <span class="requiredSign" style="left: -50px">*</span>
                        </td>
                        <td class="InputLabel" style="width: 13%"><asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_DRPNEEDSERIAL%>"></asp:Label>：<%--是否序列号管控--%></td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="drpNeedSerial" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                        <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>： <%--规格--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtcspec" runat="server"  CssClass="NormalInputText" Width="95%"></asp:TextBox>                          
                        </td>                     
                        <td colspan="4"></td>                     
                    </tr>


                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCUSETYPE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_lblCUSETYPE%>"></asp:Label>：<%--用途--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCUSETYPE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCVERSION" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_lblCVERSION%>"></asp:Label>：<%--版本--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCVERSION" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCUNITS" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_lblCUNITS%>"></asp:Label>：<%--重量单位--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCUNITS" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"
                                OnClientClick=<%# "return validate_DataTime2('"+txtDEXPIREDATEFrom.ClientID+ "', '" +txtDEXPIREDATETo.ClientID+"','"+Resources.Lang.FrmBASE_PARTList_Msg03+"');" %>><%--终止的开始时间不能大于结束时间。--%>

                            </asp:Button>
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

                                function CheckDel() {
                                    var number = 0;
                                    var controls = document.getElementById("<%=grdBASE_PART.ClientID %>").getElementsByTagName("input");

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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left">
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd"
                    Text="<%$ Resources:Lang, Common_Add%>"><%--新增--%>
                </asp:Button>
                &nbsp;&nbsp;<asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_Delete%>"
                    CssClass="ButtonDel" OnClientClick="return CheckDel()" />
                <%--删除--%>	
            </td>
        </tr>

        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%;" id="DivScroll">
                    <asp:GridView ID="grdBASE_PART" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="True"
                        OnRowDataBound="grdBASE_PART_RowDataBound" CssClass="Grid" PageSize="15">
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
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="PART" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_LiaoHao%>" HtmlEncode="false"><%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RANK_FINAL" DataFormatString="" HeaderText="批/序號(RANK)" HtmlEncode="false"><%--料号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPARTNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName1%>" HtmlEncode="false"><%--品名--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                              <asp:BoundField DataField="CSPECIFICATIONS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"><%--规格--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CTYPE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Type%>"><%--类型--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                          
                            <%--<asp:BoundField DataField="INW" DataFormatString="{0:0.00}" HeaderText="净重">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ICW" DataFormatString="{0:0.00}" HeaderText="毛重">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CUNITS" DataFormatString="" HeaderText="重量单位">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="INEEDCHECK" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_lblINEEDCHECK%>"
                                Visible="False"><%--是否免检--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CINRULE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CINRULE%>" Visible="False"><%--上架规则--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COUTRULE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_COUTRULE%>" Visible="False"><%--下架规则--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ILENGTH" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_ILENGTH%>" Visible="False"><%--长--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IWIDTH" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IWIDTH%>" Visible="False"><%--宽--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IHEIGHT" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IHEIGHT%>" Visible="False"><%--高--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CUSETYPE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_lblCUSETYPE%>"
                                Visible="False"><%--用途--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CMEMO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO%>" Visible="False"><%--备注--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DEXPIREDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblDEXPIREDATEFromFrom%>"
                                Visible="False"><%--终止日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CALIAS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCALIAS%>" Visible="False"><%--助记码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" Visible="False" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCERPCODE%>"><%--ERP编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="CSAFEQTY" DataFormatString="{0:0.00}" HeaderText="安全库存">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="INEEDWARN" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_lblINEEDWARN%>"
                                Visible="False"><%--是否预警--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ID" DataFormatString="" HeaderText="ID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CDEFAULTWARE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CDEFAULTWARE%>" Visible="False"><%--默认仓库--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CDEFAULTCARGO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CDEFAULTCARGO%>" Visible="False"><%--默认储位--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CVERSION" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_lblCVERSION%>"
                                Visible="False"><%--版本--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CBARRULE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CBARRULE%>" Visible="False"><%--条码规则--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CDEFAULTVENDOR" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_CDEFAULTVENDOR%>"
                                Visible="False"><%--默认供应商--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CVOLUME" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IVOLUME%>" Visible="False"><%--体积--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MTYPE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_Label2%>"><%--类别--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BONDED" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_Label1%>"><%--是否保税--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NeedSerial" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_DRPNEEDSERIAL%>"><%--是否序列号管控--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CSTATUS" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"><%--状态--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATEOWNER" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"><%--创建人--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"><%--创建时间--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>"><%--编辑--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:HyperLinkField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, FrmBASE_PARTList_SetArea%>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_PARTList_SetArea%>" Text="<%$ Resources:Lang, FrmBASE_PARTList_SetArea%>"><%--设置储位区域--%>  <%--设置--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
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
        </script>
    </table>
</asp:Content>
