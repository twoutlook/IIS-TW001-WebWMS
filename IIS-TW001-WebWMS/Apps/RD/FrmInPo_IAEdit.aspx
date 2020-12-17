<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmInPo_IAEdit.aspx.cs"
    Inherits="RD_FrmInPo_IAEdit" Title="--<%$ Resources:Lang, Common_InbillMangement %>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>
<%--入库管理--%>
<%@ Register Src="ShowPOIA_Div.ascx" TagName="ShowPOIA_Div" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script type="text/javascript">
        function SetControlValue(ControlName, Values, ControlName2, Values2, ControlName3, Values3, ControlName4, Values4) {
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
            document.all("ctl00_ContentPlaceHolderMain_field_code").value = Values2;
            document.all(ControlName2).disabled = "disabled";
            document.all(ControlName3).value = Values3;
            document.all("ctl00_ContentPlaceHolderMain_field_name").value = Values3;
            document.all(ControlName3).disabled = "disabled";
            document.all(ControlName4).value = Values4;
        }

        function Show(divID) {
            //alert(divID);
            disponse_div(event, document.all(divID));
        }
    </script>
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
        input[type='submit'][disabled],input[type='button'][disabled]
        {       
            opacity:0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70);/*兼容ie8及以下*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt; <%=Resources.Lang.FrmInPo_IAEdit_Content1%><%--预入库通知单--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc2:ShowPOIA_Div ID="ShowPOIA_Div1" runat="server" />
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
    </table>
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top" class="tableCell">
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
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="36"></asp:TextBox>
                          
                        </td>
                       
                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="PO_ID："></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtPO_ID" runat="server" CssClass="NormalInputText" Width="95%" ></asp:TextBox>
                          
                        </td>
                       
                    </tr>
                   
                     <tr>
                          <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, Common_CticketCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                             <asp:Label ID="Label11" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text="PO："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPO" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                              <img alt="" onclick="disponse_div(event,document.all('<%=ShowPOIA_Div1.GetDivName %>'));"
                                src="../../Images/Search.gif" class="select" />
                                
                        </td> 
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server"  Text="<%$ Resources:Lang, FrmINASSIT_DEdit_MSG3 %>"></asp:Label>：<%--批次号：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtBatchNo" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False"></asp:TextBox>
                        </td> 
                        
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                              <asp:Label ID="Label9" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmInbill_ErpCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtERPCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                            
                        </td> 
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblTRADECODE" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG1 %>"></asp:Label>：<%--贸易代码：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtTRADECODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                          
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCURRENCY" runat="server" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG2 %>"></asp:Label>：<%--币别：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCURRENCY" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                           
                        </td>

                    </tr>
                    <tr>
                       <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label10" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmInPo_IAEdit_Label1 %>"></asp:Label>：<%--MES料盘数：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtMESDDEFINE1" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                           
                        </td> 
                      
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCVENDERCODE" runat="server" Text="<%$ Resources:Lang, CommonB_CVENDERCODE %>"></asp:Label>：<%--供应商编码：--%>
                        </td>
                        <td style="width: 21%">
                           <asp:TextBox ID="txtCVENDERCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                                <input  runat="server" type="hidden" name="field_code"  id="field_code"  value=""/>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCVENDER" runat="server" Text="<%$ Resources:Lang, FrmCommandQuery_Report_MSG24 %>"></asp:Label>：<%--供应商名称：--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCVENDER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50"></asp:TextBox>
                             <input  runat="server" type="hidden" name="field_name"  id="field_name"  value=""/>
                           <%-- <span class="requiredSign">*</span>--%>
                        </td>

                    </tr>
                    <tr>
                          
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="txtCSTATUS" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False">
                            </asp:DropDownList>
                          
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, Common_CreateUser %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcreateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, Common_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcreatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                     ToolTip="<%$ Resources:Lang, FrmINBILLEdit_MSG4 %>"  Enabled="false"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                           </td>
                        
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmImportDateList_LAST_UPD_OWNER %>"></asp:Label>：<%--最后修改人：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtupdateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                          
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmImportDateList_LAST_UPD_TIME %>"></asp:Label>：<%--最后修改日期：--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtupdatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                ToolTip="<%$ Resources:Lang, FrmINBILLEdit_MSG4 %>"  Enabled="false"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                           <%-- <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('<% =txtupdatetime.ClientID  %>','y-mm-dd',0);" />
                       --%> </td>
                         <td class="InputLabel" style="width: 13%">
                            
                        </td>
                        <td style="width: 20%">
                           
                        </td>
                         
                        
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server"  Text="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"></asp:Label>：<%--备注：--%>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtCMEMO" runat="server" CssClass="NormalInputText" Width="99%"
                                MaxLength="200" Height="52px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                   
                  
                    <tr>
                        <td colspan="6" style="text-align: center; padding:15px 0px;">
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                                Text="<%$ Resources:Lang, Common_Save %>" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnConfirm" runat="server" CssClass="ButtonDo" 
                                Text="<%$ Resources:Lang, FrmInPo_IAEdit_btnConfirm %>"  Enabled="False" onclick="btnConfirm_Click"/><%--确认--%>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnCreate" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_GenerateBtn %>" OnClick="btnCreate_Click"
                                Enabled="False"/>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>" CausesValidation="false" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnPrint" runat="server" CssClass="ButtonPrint" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG12 %>" OnClick="btnPrint_Click"></asp:Button><%--打印--%>
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
        <tr class="tableCell">
            <td colspan="6">
                <table id="TabMain0" style="width: 100%" runat="server"  visible="false">
                    <tr valign="top">
                        <td align="left" valign="top" style="padding:15px 0px;">                       
                            <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_AddBtn %>" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnDelete" runat="server" CssClass="ButtonDel" OnClick="btnDelete_Click"
                                Text="<%$ Resources:Lang, Common_DelBtn %>" OnClientClick="return confirm('<%$ Resources:Lang, FrmInPo_IAEdit_btnDelete %>');"/>
                        &nbsp;&nbsp;&nbsp; </td><%--是否需要删除!--%>
                        <td align="right" style="width: 50%;padding:15px 0px;">
                            <asp:Label ID="Label5" CssClass="InputLabel" runat="server" Text="<%$ Resources:Lang, FrmINASNEdit_Label5 %>"></asp:Label>：<%--查询料号：--%>
                            <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="25%" MaxLength="50"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" OnClick="btnSearch_Click">
                            </asp:Button>
                        </td>
                           
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div id="DivScroll">
                                        <asp:GridView ID="grdINASN_IA_D" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" CssClass="Grid"
                                            OnRowDataBound="grdINASN_IA_D_RowDataBound" PageSize="15" ShowHeader="True" Width="100%">
                                            <PagerSettings Visible="False" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                            <RowStyle HorizontalAlign="Left" Wrap="False" />
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                            <PagerStyle HorizontalAlign="Right" />
                                            <HeaderStyle CssClass="" Font-Bold="True" HorizontalAlign="Center" Wrap="False" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="&lt;input id='chkSelect' type='checkbox' onclick='SelectAll4Grid(this)' /&gt;">
                                                    <ControlStyle BorderWidth="0px" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid"
                                                            BorderWidth="0px" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkSelect0" runat="server" BorderStyle="Solid" BorderWidth="0px" />
                                                    </EditItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="IDS" DataFormatString="" HeaderText="IDS" Visible="False">
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="ID" DataFormatString="" HeaderText="ID" Visible="False">
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ILINE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG20 %>" ><%--项次--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ERPCODELINE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG23 %>"><%--ERP项次--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PONO" DataFormatString="" HeaderText="PO">
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="POLINE" DataFormatString="" HeaderText="<%$ Resources:Lang, CommonB_IPOLINE %>"><%--PO项次--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>">
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName %>">
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="False"  Width="200px"/>
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="DATECODE" DataFormatString="" HeaderText="DateCode">
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QTYTOTAL" DataFormatString="{0:0.00}"  HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG21 %>"><%--总数量--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="right" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QTYPASSED" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmInPo_IAEdit_QTYPASSED %>"><%--通过数量--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="right" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QTYUNPASSED" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, CommonB_lineRejectNum %>"><%--判退数量--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="right" Wrap="False" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="QTYPENDING" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmInPo_IAEdit_QTYPENDING %>"><%--待检数量--%>
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="right" Wrap="False" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="FLAG_NAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Cstatus %>">
                                                    <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="center" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_EditBtn %>" DataTextField=""
                                                    DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_EditBtn %>" Text="<%$ Resources:Lang, Common_EditBtn %>">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                </asp:HyperLinkField>
                                            </Columns>
                                        </asp:GridView>
                                        <ul class="OneRowStyle">
                                        <li>
                                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                                FirstPageText="<%$ Resources:Lang, Common_FirstPage%>" LastPageText="<%$ Resources:Lang, Common_LastPage%>" NextPageText="<%$ Resources:Lang, Common_NextPage%>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                            </webdiyer:AspNetPager>
                                        </li>
                                        <li>
                                            <div><%=Resources.Lang.Common_TotalPage %> <%=AspNetPager1.RecordCount%> <%=Resources.Lang.Common_TotalPage1 %></div>
                                        </li>
                                    </ul>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
        <tr valign="top" class="tableCell">
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server"
                    DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="hiddOperation" runat="server" />
</asp:Content>
