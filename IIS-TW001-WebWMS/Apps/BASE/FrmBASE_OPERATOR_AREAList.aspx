<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"  Title="--<%$ Resources:Lang, FrmBASE_OPERATOR_AREAEdit_Title01%>" CodeFile="FrmBASE_OPERATOR_AREAList.aspx.cs" Inherits="BASE_FrmBASE_OPERATOR_AREAList" %>
<%--操作人--%>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register  Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" Runat="Server">
<link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server"/>  
<script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
<script src="../../Layout/js/pad.js" type="text/javascript"></script>
<!--
<script type="text/javascript" src="../../Layout/My97DatePicker/WdatePicker.js">
</script>
-->
<!--
    <script type="text/javascript" src="../../Layout/Calendar/Calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
-->
<script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
<link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" Runat="Server">
     <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.FrmBASE_OPERATOR_AREAEdit_Title01%><%--基礎資料-&gt;操作人--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server"> 
    <ajaxToolkit:toolkitscriptmanager ID="scriptManager1" runat="server"></ajaxToolkit:toolkitscriptmanager>
    <table id="TabMain" style="height:100%;width:95%" > 
		<tr valign="top">				    
			<td valign="top" > 
				<table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="tabCondition">
					<tr>
						<th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5" height="11" alt="" runat="server" id="titleImg"/> 
                           <%= Resources.Lang.Common_JSCeria%> <%--检索条件--%>
                        </th>
					    <th style="border-left-width:0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align:center" onclick="CollapseCondition('../../');return false;" />                            
                        </th>
					</tr>
					<tr style="display:none;">
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
							<asp:Label ID="lblCACCOUNTID" runat="server" Text="<%$ Resources:Lang, FrmBASE_OPERATORList_lblCACCOUNTID%>"></asp:Label>：<%--操作人编码--%>
						</td>
						<td style="width:20%">
							<asp:TextBox ID="txtCACCOUNTID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
						</td>
						<td class="InputLabel" style="width:13%">
							<asp:Label ID="lblCCARGOID" runat="server" Text="<%$ Resources:Lang, FrmBASE_OPERATOR_AREAEdit_lblCCARGOID%>"></asp:Label>：<%--储位编码--%>
						</td>
						<td style="width:20%">
							<asp:TextBox ID="txtCCARGOID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
						</td>
						<td class="InputLabel" style="width:13%;">
                            <asp:button  id="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" onclick="btnSearch_Click"></asp:button><%--查询--%>
							<asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, Common_CreateDate%>" Visible="False"></asp:Label>：<%--创建日期--%>
						</td>
						<td style="width:20%;">
							<asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%" Visible="false" ></asp:TextBox>
							<img border="0" align="absmiddle" alt="" style="cursor:hand;position:relative;left:-30px;top:0px; display:none;" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('ctl00_ContentPlaceHolderMain_txtDCREATETIME','y-mm-dd',0);"/>
						</td>
						</tr>
						<tr style="display:none;">
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
						<td colspan="6" >
							<asp:button  id="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, FrmALLOCATEList_btnNew%>" ></asp:button><%--新增--%>
                            &nbsp;&nbsp;<asp:Button ID="btnDelete" onclick="btnDelete_Click" runat="server" Text="<%$ Resources:Lang, Common_Delete%>"   CssClass="ButtonDel"/><%--删除--%>
						</td>
					</tr>

				</table>
			</td>
		</tr>
       
		<tr>
			<td>
			    <div style="height:300px;overflow-x:scroll;width:100%" id="DivScroll">				 
			        <asp:GridView ID="grdBASE_OPERATOR_AREA" runat="server"  AllowPaging="True" BorderColor="Teal" 
				        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ShowHeaderWhenEmpty="true"
				        Width="100%" AutoGenerateColumns="False" 
				        onrowdatabound="grdBASE_OPERATOR_AREA_RowDataBound" CssClass="Grid" PageSize="15"> 
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
				        <asp:BoundField DataField="CACCOUNTID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_OPERATORList_lblCACCOUNTID%>"> <%--操作人编码--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>
				        <asp:BoundField DataField="CCARGOID" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_OPERATOR_AREAEdit_lblCCARGOID%>"> <%--储位编码--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>
				        <asp:BoundField DataField="DCREATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Lang, Common_CreateDate%>"> <%--创建日期--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="center"  Wrap="False" /> 
				         </asp:BoundField>
				        <asp:BoundField DataField="CCREATEOWNER" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"> <%--创建人--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>
				        <asp:BoundField DataField="ID" DataFormatString="" HeaderText="ID" Visible="False"> 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>
				        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>"  
				             DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>"> <%--编辑--%>
				             <HeaderStyle HorizontalAlign="Center"  Wrap="False"/> 
				             <ItemStyle HorizontalAlign="Center" Wrap="False" /> 
				         </asp:HyperLinkField>
				        </Columns>
			        </asp:GridView>
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdBASE_OPERATOR_AREA.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                    </script>
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
		function ChangeDivWidth()
		{
            document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
        }
        window.onresize= ChangeDivWidth;
        ChangeDivWidth();
    </script>
</table>
</asp:Content>
		
		
    

