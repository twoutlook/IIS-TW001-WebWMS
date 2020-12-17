<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"  Title="--111" CodeFile="FrmSTOCK_ADJUSTList.aspx.cs" Inherits="STOCK_FrmSTOCK_ADJUSTList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" Runat="Server">
<link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server"/>  
<script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
<script src="../../Layout/js/pad.js" type="text/javascript"></script>
        <style type="text/css">
            .ui-autocomplete-loading
        {
            background: white url('../../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }
        .select
        {
            cursor: pointer;
            position: relative;
            left: -25px;
            top: 4px;
        }
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
    </style>
<script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
     <script type="text/javascript">
         function CheckSH() {
             var number = 0;
             var controls = document.getElementById("<%=grdSTOCK_ADJUST.ClientID %>").getElementsByTagName("input");
            for (var i = 0; i < controls.length; i++) {
                var e = controls[i];
                if (e.type != "CheckBox") {
                    if (e.checked == true) {
                        number = number + 1;
                    }
                }
            }
            if (number == 0) {
                alert("<%= Resources.Lang.FrmBar_SNManagement_DoTips %>");
                return false;
            }
        }

        function CheckConfirm() {
            var number = 0;
            var controls = document.getElementById("<%=grdSTOCK_ADJUST.ClientID%>").getElementsByTagName("input");
        for (var i = 0; i < controls.length; i++) {
            var e = controls[i];
            if (e.type != "CheckBox") {
                if (e.checked == true) {
                    number = number + 1;
                }
            }
        }
        if (number == 0) {
            alert("<%= Resources.Lang.FrmBar_SNManagement_DoTips %>");
            return false;
        }
    }
    function CheckCancel() {
        var number = 0;
        var controls = document.getElementById("<%=grdSTOCK_ADJUST.ClientID%>").getElementsByTagName("input");
        for (var i = 0; i < controls.length; i++) {
            var e = controls[i];
            if (e.type != "CheckBox") {
                if (e.checked == true) {
                    number = number + 1;
                }
            }
        }
        if (number == 0) {
            alert("<%= Resources.Lang.FrmBar_SNManagement_DoTips %>"); 
            return false;
        }
    }

    </script>
<link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" Runat="Server">
    <%= Resources.Lang.WMS_Common_Group_StockManage %><%--庫存管理--%>-&gt;<%= Resources.Lang.WMS_Common_Element_StockChange %><%--库存调整--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server"> 
    <ajaxToolkit:toolkitscriptmanager ID="scriptManager1" runat="server"></ajaxToolkit:toolkitscriptmanager>
    <table id="TabMain" style="width:95%" > 
		<tr valign="top" class="tableCell">				    
			<td valign="top" > 
				<table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="tabCondition">
					<tr>
						<th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="titleImg"/> 
                            <%= Resources.Lang.WMS_Common_SearchTitle %>
                        </th>
					    <th style="border-left-width:0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.WMS_Common_FoldAlt %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align:center" onclick="CollapseCondition('../../');return false;" />                            
                        </th>
					</tr>
					<tr style="display:none">
						<td colspan="6">
							<style type="text/css">
								span.requiredSign
								{
									color:#FF0000; 
									font-weight: bold; 
									position:relative; 
									left:-15px; 
									top:2px; 
								}
							</style>
						</td>
					</tr>
					<tr>
						<td class="InputLabel" style="width:13%">
							<asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang,WMS_Common_Element_Cticketcode %>"></asp:Label>：<%--单据号--%>
						</td>
						<td style="width:20%">
							<asp:TextBox ID="txtCTICKETCODE" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
						</td>
                        <td class="InputLabel" style="width:13%">
							<asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang,FrmBar_SNManagement_RuleStatus %>"></asp:Label>：<%--状态--%>
						</td>
						<td style="width:20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%">
                          <%--      <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="0">未处理</asp:ListItem>
                                <asp:ListItem Value="1">已完成</asp:ListItem>--%>
                            </asp:DropDownList>
						</td>
                        <td class="InputLabel" style="width:13%">
							<asp:Label ID="lblCASNID" runat="server" Text="单号：" style="display:none;" ></asp:Label>
						</td>
						<td style="width:20%">
							<asp:TextBox ID="txtCASNID" runat="server" CssClass="NormalInputText" style="display:none;"  Width="95%"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="InputLabel" style="width:13%">
							<asp:Label ID="lblDINDATEFromFrom" runat="server" Text="<%$ Resources:Lang,FrmKUCUNTIAOZHENGReport_lblDINDATEFromFrom %>"></asp:Label>：<%--调整日期--%>
						</td>
						<td style="width:20%; white-space: nowrap;">
							<asp:TextBox ID="txtDINDATEFrom" runat="server" CssClass="NormalInputText" Width="95%" ></asp:TextBox>
							<img border="0" align="absmiddle" alt="" style="cursor:pointer;position:relative;left:-30px;top:0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATEFrom','y-mm-dd',0);"/>
						</td>
						<td class="InputLabel" style="width:13%">
							<asp:Label ID="lblDINDATEToTo" runat="server" Text="<%$ Resources:Lang,WMS_Common_DateTo %>"></asp:Label>：
						</td>
						<td style="width:20%; white-space: nowrap;">
							<asp:TextBox ID="txtDINDATETo" runat="server" CssClass="NormalInputText" Width="95%" ></asp:TextBox>
							<img border="0" align="absmiddle" alt="" style="cursor:pointer;position:relative;left:-30px;top:0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDINDATETo','y-mm-dd',0);"/>
							
						</td>
						<td class="InputLabel" colspan="1">&nbsp;</td>
						<td colspan="1">&nbsp;</td>
						</tr>
						<tr style="display:none">
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
						<td colspan="6" style="text-align:right;" >
							<asp:button  id="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang,WMS_Common_Button_Search %>" onclick="btnSearch_Click"></asp:button>
							
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top" class="tableCell">				    
			<td valign="top" align="left">
                    <asp:button  id="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang,WMS_Common_Button_New %>" ></asp:button> 
                	&nbsp;&nbsp;<asp:Button ID="btnDelete" onclick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Delete %>" OnClientClick=" return CheckCancel()" CssClass="ButtonDel"/><%--删除--%>
                    &nbsp;&nbsp;<asp:Button ID="btnConfirm" onclick="btnConfirm_Click" runat="server" Text="<%$ Resources:Lang,FrmInPo_IAEdit_btnConfirm %>" CssClass="ButtonDo" OnClientClick="return CheckConfirm()"/><%--确认--%>	
                    &nbsp;&nbsp;<asp:Button ID="btnReview" onclick="btnReview_Click" runat="server" Text="<%$ Resources:Lang,WMS_Common_Button_Audit %>" CssClass="ButtonDo" OnClientClick=" return CheckSH()"/>	<%--审核--%>
            </td>
        </tr>	
		<tr class="tableCell">
			<td>
			    <div style="height:500px;overflow-x:scroll;width:100%" id="DivScroll">				 
			        <asp:GridView ID="grdSTOCK_ADJUST" runat="server"  AllowPaging="True" BorderColor="Teal" 
				        BorderStyle="Solid" BorderWidth="1px" CellPadding="1"  
				        Width="100%" AutoGenerateColumns="False"  				       
				        onrowdatabound="grdSTOCK_ADJUST_RowDataBound" CssClass="Grid" PageSize="15"> 
				        <PagerSettings Visible="False" /> 
				        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
				        <RowStyle HorizontalAlign="Left" Wrap="False" /> 
				        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"  
				            Wrap="False" /> 
				        <PagerStyle HorizontalAlign="Right" /> 
				        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""  
				            Wrap="False" /> 
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
				        <asp:BoundField DataField="CTICKETCODE" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_Cticketcode %>"><%--单据号--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>
				        <asp:BoundField DataField="createtime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang,FrmKUCUNTIAOZHENGReport_lblDINDATEFromFrom %>"> <%--调整日期--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="center"  Wrap="False" /> 
				         </asp:BoundField>
				        <asp:BoundField DataField="createowner" DataFormatString="" HeaderText="<%$ Resources:Lang,WMS_Common_Element_CreateOwner %>"><%--制单人--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>				        
				        <asp:BoundField DataField="reviewuser" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmOUTASNList_AuditUser %>"> <%--审核人--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>
				        <asp:BoundField DataField="reviewtime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang,FrmOUTASNList_AuditDate %>"> <%--审核日期--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="center"  Wrap="False" /> 
				         </asp:BoundField>
				        <asp:BoundField DataField="cstatusname" DataFormatString="" HeaderText="<%$ Resources:Lang,FrmBar_SNManagement_RuleStatus %>"> <%--状态--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>
				        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, WMS_Common_GridView_Edit %>"  
				             DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, WMS_Common_GridView_Edit %>" Text="<%$ Resources:Lang, WMS_Common_GridView_Edit %>"> 
				             <HeaderStyle HorizontalAlign="Center"  Wrap="False"/> 
				             <ItemStyle HorizontalAlign="Center" Wrap="False" /> 
				         </asp:HyperLinkField>
                         <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, FrmOUTASNList_ChaKang %>"  
				             DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, FrmOUTASNList_ChaKang %>" Text="<%$ Resources:Lang, FrmOUTASNList_ChaKang %>" >
				             <HeaderStyle HorizontalAlign="Center"  Wrap="False"/> 
				             <ItemStyle HorizontalAlign="Center" Wrap="False" /> 
				         </asp:HyperLinkField>
				        </Columns>
			        </asp:GridView>

                       <ul class="OneRowStyle">
                        <li>
                            <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                FirstPageText="<%$ Resources:Lang,WMS_Common_Pager_First %>" LastPageText="<%$ Resources:Lang,WMS_Common_Pager_Last %>" NextPageText="<%$ Resources:Lang,WMS_Common_Pager_Next %>" PrevPageText="<%$ Resources:Lang,WMS_Common_Pager_Front %>" ShowPageIndexBox="Never"
                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                            </webdiyer:aspnetpager>
                        </li>
                        <li>
                            <div><%= Resources.Lang.WMS_Common_Pager_Total %> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.WMS_Common_Pager_Count %></div>
                        </li>
                    </ul>
			    </div>
			</td>
		</tr>
		<script type="text/javascript">	
		function ChangeDivWidth()
		{
            document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
        }
        window.onresize= ChangeDivWidth;
        ChangeDivWidth();
    </script>
</table>
</asp:Content>
		
		
    

