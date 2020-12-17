<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--<%$ Resources:Lang, FrmBASE_CARGOSPACEList_Title01%>" CodeFile="FrmBASE_CARGOSPACEList.aspx.cs" Inherits="BASE_FrmBASE_CARGOSPACEList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="ShowAREADiv.ascx" TagName="ShowAREADiv" TagPrefix="ucArea" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    &nbsp;&nbsp;&nbsp;  <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.FrmBASE_CARGOSPACEList_Title01%><%--基礎資料-&gt;储位管理--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <ucArea:ShowAREADiv ID="ucShowArea" runat="server" />
    <table id="TabMain" style="height: 100%; width: 95%">
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
                            <asp:Label ID="lblCCARGOID" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>"></asp:Label>：<%--编号--%>
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
                            </asp:DropDownList>
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
                            <asp:Label ID="lblCTYPE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblCTYPE%>"></asp:Label>： <%--种类--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="DropCTYPE" runat="server" Width="95%" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDEXPIREDATEFromFrom" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblDEXPIREDATEFromFrom%>"></asp:Label>： <%--终止日期--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtDEXPIREDATEFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDEXPIREDATEFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDEXPIREDATEToTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao%>"></asp:Label>： <%--到--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtDEXPIREDATETo" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDEXPIREDATETo','y-mm-dd',0);" />
                            <span class="requiredSign" style="left: -50px">*</span>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIPERMITMIX" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblIPERMITMIX%>"></asp:Label>： <%--是否允许混放--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="ddlIPERMITMIX" runat="server" Width="95%">
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
                            <asp:Label ID="lbCreateTimeFrom" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label>：  <%--创建时间--%>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:TextBox ID="txtCreateTimeFrom" runat="server" onKeyPress="event.returnValue=false"
                                CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            <img border="0" align="absmiddle" alt="" style="cursor: pointer; position: relative; left: -30px; top: 0px"
                                src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtCreateTimeFrom','y-mm-dd',0);" />
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lbCreateTimeTo" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEList_Dao%>">：<%--到--%>
                                
                            </asp:Label>
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
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_CWARENAME%>"></asp:Label>：<%--所属仓库--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlWareHouse" runat="server"  Width="95%"> </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_Label3%>"></asp:Label>：<%--是否已设置最大量--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="ddlZDL" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_Label4%>"></asp:Label>：<%--是否已设置区域--%>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="ddlQY" runat="server" Width="95%">
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
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_Label7%>"></asp:Label>：<%--是否存在栈板--%> 
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlPalletcode" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_Label6%>"></asp:Label>：<%--是否有货--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlStock" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                          <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lineid%>"></asp:Label>：<%--线别--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlLineID" runat="server" Width="95%">   
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_Click"
                                OnClientClick=<%# "return validate_DataTime2('"+txtDEXPIREDATEFrom.ClientID+ "', '" +txtDEXPIREDATETo.ClientID+"','"+ Resources.Lang.FrmBASE_PARTList_Msg03+"');" %> /><%--查询--%><%--终止的开始时间不能大于结束时间。--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="left"><asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button> <%--新增--%>
                &nbsp;&nbsp;<asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_Delete%>"
                    CssClass="ButtonDel" OnClientClick="return CheckDel()" /><%--删除--%>
                &nbsp;&nbsp;
                <asp:Button ID="btnUpdateStatus" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, WMS_Common_Button_Release%>" OnClick="btnUpdateStatus_Click"
                    Visible="False"></asp:Button><%--释放--%>
                &nbsp;&nbsp;
                <asp:DropDownList ID="ddlStatus" runat="server" Width="100">
                </asp:DropDownList>
                <asp:Button ID="btnUpStatus" runat="server" CssClass="ButtonConfig" Text="<%$ Resources:Lang, FrmSTOCK_CHECKBILL_PLAN_TypeSet%>" ToolTip="设置状态可用不可用"
                    OnClientClick="return CheckStatus()" OnClick="btnUpStatus_Click"></asp:Button><%--状态设置--%>
                <asp:Button ID="Button1" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_Button1%>" Visible="false"
                    OnClick="Button1_Click"></asp:Button>                <%--更新--%> 
            </td>                 
        </tr>
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdBASE_CARGOSPACE" runat="server" AllowPaging="True" BorderColor="Teal"
                        DataKeyNames="ID,IOCCUPYQTY" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ShowHeaderWhenEmpty="true"
                        Width="100%" AutoGenerateColumns="False"  OnRowDataBound="grdBASE_CARGOSPACE_RowDataBound"
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
                            <asp:BoundField DataField="ID" DataFormatString="" HeaderText="ID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>"> <%--编号--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblCCARGONAME%>">  <%--名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="area_name" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_area_name%>"> <%--区域--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCERPCODE%>"> <%--ERP编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IMAXCAPACITY" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IMAXCAPACITY%>"
                                Visible="False"> <%--载重--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CMEMO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CMEMO%>" Visible="False">  <%--备注--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CWARENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_CWARENAME%>"> <%--所属仓库--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>

                            <asp:BoundField DataField="lineid" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lineid%>"> <%--线别--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>


                            <asp:BoundField DataField="IPRIORITY" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IPRIORITY%>" Visible="False"> <%--优先级--%>
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
                            <asp:BoundField DataField="IMAXCAPACITY" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IMAXCAPACITY%>"> <%--最大量--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_ZYLaing%>">  <%--占用量--%>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlToIOCCUPYQTY_Info" ToolTip="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_ViewassitDetail%>" runat="server"><%# Eval("IOCCUPYQTY") %></asp:HyperLink>  <%--查看上架指引占用详情--%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" />
                            </asp:TemplateField>
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
                            <asp:BoundField DataField="ipermitmix" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IPERMITMIXNAME%>"> <%--混放--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DEXPIREDATE" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblDEXPIREDATEFromFrom%>" Visible="false">  <%--终止日期--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                            </asp:BoundField>
                            <%-- <asp:BoundField DataField="CREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="创建日期">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="is_allo" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IS_ALLONAME%>"> <%--允許調撥--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cstatus" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"> <%--状态--%>
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
                if (confirm("<%= Resources.Lang.FrmALLOCATEEdit_Msg07%>")) { //你确认删除吗？
                    return true;
                }
                else {
                    return false;
                }

            }
            function CheckStatus() {
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
                    alert("<%= Resources.Lang.FrmBASE_CARGOSPACEList_Mag01%>"); //请选择需要设置状态的项！
                    return false;
                }
                if (confirm("<%= Resources.Lang.FrmBASE_CARGOSPACEList_Mag02%>")) { //你确认设置该储位的状态吗？
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
    </table>
</asp:Content>
