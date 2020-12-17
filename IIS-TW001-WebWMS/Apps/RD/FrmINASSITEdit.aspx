<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmINASSITEdit.aspx.cs" Inherits="RD_FrmINASSITEdit"
    Title="--<%$ Resources:Lang, FrmINASSIT_DEdit_MSG1 %>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>
<%--上架指引单--%>

<%@ Register  Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="ShowINASN_Div.ascx" TagName="ShowINASN" TagPrefix="ucIA" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <!--<script src="../../Layout/Js/jquery-1.4.1.min.js" type="text/javascript"></script>-->
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js"
        type="text/javascript"></script>
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css"
        id="cssUrl" runat="server" />
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="Link1" runat="server"/>  
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
<script type="text/javascript">
    function swhoValue(id) {
        //alert($("#"+id).val());
        alert(document.getElementById(id).value);
    }
</script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt;<%=Resources.Lang.FrmINASSIT_DEdit_MSG1%><%--上架指引单--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">    
    <ajaxToolkit:toolkitscriptmanager ID="scriptManager1" runat="server"></ajaxToolkit:toolkitscriptmanager>
    <ucIA:ShowINASN ID="ucINASN_Div" runat="server" />    
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                &nbsp;&nbsp; <asp:Button ID="btnDelete" runat="server" CssClass="ButtonDel" 
                    onclick="btnDelete_Click" Text="<%$ Resources:Lang, Common_DelBtn %>" CausesValidation="false" Visible="False" />
            </td>
        </tr>
    </table>
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    runat="server" id="TabMain">
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
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="<%$ Resources:Lang, Common_CticketCode %>"></asp:Label>：
                    	</td>
                    	<td style="width:20%">
                            <asp:TextBox ID="txtCTICKETCODE" runat="server" Enabled="false" CssClass="NormalInputText" Width="95%" MaxLength="40"></asp:TextBox>
                    	              	
                        </td>                    
                        <td class="InputLabel" style="width:13%"> 
                            <span class="requiredSign">*</span>
                    	    <asp:Label ID="lblCASNID" runat="server" Text="<%$ Resources:Lang, Common_InasnCticketCode %>"></asp:Label>
                        </td>
                        <td style="width:20%">
                            <asp:TextBox ID="txtCASNID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30" onKeyPress="event.returnValue=false"></asp:TextBox>
                           
                        </td>
                        <td class="InputLabel" style="width:13%">
                    	    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmInbill_InBillTYPE %>"></asp:Label>：
                        </td>
                        <td style="width:20%">
                            <asp:DropDownList ID="ddlInType" runat="server" Width="95%" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                    	<td class="InputLabel" style="width:13%">
                    		<asp:Label ID="lblCCREATEOWNERCODE" runat="server" Text="<%$ Resources:Lang, Common_CreateUser %>"></asp:Label>：
                    	</td>
                    	<td style="width:20%">
                    	    <asp:TextBox ID="txtCCREATEOWNERCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="100" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width:13%">
                    	    <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, Common_CreateDate %>"></asp:Label>：
                        </td>
                        <td style="width:20%">
                            <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" 
                                Width="95%"  ToolTip="<%$ Resources:Lang, FrmINBILLEdit_MSG4 %>"  Enabled="false"></asp:TextBox><%--格式：yyyy-MM-dd--%>                          
                        </td>
                        <td class="InputLabel" style="width:13%">
                    	    <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_Cstatus %>"></asp:Label>：
                        </td>
                        <td style="width:20%">
                            <asp:DropDownList ID="ddlCSTATUS" runat="server" Width="95%" Enabled="False">                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width:13%">
                    	    <asp:Label ID="Label1" runat="server"  Text="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"></asp:Label>：<%--备注：--%>
                        </td>
                        <td class="InputLabel" colspan="5" >
                            <asp:TextBox ID="txtCMEMO" runat="server" TextMode="MultiLine" Width="100%" ></asp:TextBox>
                            <asp:TextBox ID="txtINASN_id" runat="server" style="display:none;" ></asp:TextBox>
                        </td>
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
    <table id="Table1" style="height:100%;width:95%;" >         
		<tr>
			<td align="center">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, Common_Save %>" Visible="false" />
				&nbsp;&nbsp;<asp:button  id="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_AddBtn %>" onclick="btnNew_Click" Visible="false" ></asp:button>
                <asp:button  id="btnAutoCreate" runat="server" CssClass="ButtonAdd" 
                    Text="<%$ Resources:Lang, Common_GenerateBtn %>" onclick="btnAutoCreate_Click" ></asp:button>
                 &nbsp;&nbsp;<asp:button  id="btnAutoCreateTest" runat="server" CssClass="ButtonAdd" Visible="false"
                    Text="PO判退" onclick="btnAutoCreateTest_Click" ></asp:button>
                &nbsp;&nbsp;<asp:Button ID="btnDeleteItem" Visible="false" onclick="btnDeleteItem_Click" runat="server" Text="<%$ Resources:Lang, Common_DelBtn %>" CssClass="ButtonDel" OnClientClick="return CheckDel('ctl00_ContentPlaceHolderMain_grdINASSIT_D');"/>
                
                &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>"  CausesValidation="false" />
                <asp:HiddenField ID="hfId" runat="server" />      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnPrint" runat="server" CssClass="ButtonPrint" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG12 %>" OnClick="btnPrint_Click"><%--打印--%>
                            </asp:Button>                          
			</td>
        </tr>
        <tr>
            <td style="text-align:right;">
                料号：<asp:TextBox ID="txtCinvCode" runat="server"></asp:TextBox>
                <asp:button  id="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>" onclick="btnSearch_Click" ></asp:button>
            </td>
		</tr>
   
		<tr>
			<td colspan="2">
			    <div style="height:300px;overflow-x:scroll;width:100%" id="DivScroll">				 
			        <asp:gridview ID="grdINASSIT_D" runat="server"  AllowPaging="True" BorderColor="Teal" 
				        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" 
				        Width="100%" AutoGenerateColumns="False"  
				       
				        onrowdatabound="grdINASSIT_D_RowDataBound" CssClass="Grid" PageSize="15" 
                        ondatabinding="grdINASSIT_D_DataBinding"> 
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
				        <asp:BoundField DataField="IDS" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG18 %>" Visible="False"><%--子表编号--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG19 %>" Visible="False"><%--主表编号--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="Inasn_Code" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_InasnCticketCode %>" 
                                Visible="False"> 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="IASNLINE" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG20 %>" 
                                Visible="False"> <%--项次--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="CPOSITIONCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PartnumNO %>"> 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="CPOSITION" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PartnumName %>"><%--储位名称--%> 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="CINVBARCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG22 %>" Visible="False"><%--物料条码--%> 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>"> 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                            <ItemStyle HorizontalAlign="left"  Wrap="False" Width="120px" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName %>"> 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                            <ItemStyle HorizontalAlign="left"  Wrap="False" Width="300px" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="CBATCH" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINASSIT_DEdit_MSG3 %>"  Visible="False"> <%--批次--%>
                              
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="INUM" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, Common_NUM %>"> 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="Center"  Wrap="False" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="CMEMO" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Remark %>" 
                                Visible="False"> 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="COPERATOR" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINASSIT_DEdit_MSG12 %>"  Visible="false"><%--操作人--%> 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="COPERATORCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINASSIT_DEdit_MSG13 %>" 
                                Visible="False"> <%--操作人编码--%>
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				            </asp:BoundField>
				        <asp:BoundField DataField="CSTATUS" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Cstatus %>" Visible="false" > 
				            <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				            </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_EditBtn %>" Visible="false" 
				             DataTextField="" DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_EditBtn %>" Text="<%$ Resources:Lang, Common_EditBtn %>"> 
				             <HeaderStyle HorizontalAlign="Center"  Wrap="False"/> 
				             <ItemStyle HorizontalAlign="Center" Wrap="False" /> 
				         </asp:HyperLinkField>
				        </Columns>
			        </asp:gridview>
                    <ul class="OneRowStyle"  >
                    <li>
                        <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" 
                            PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0"
                             CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                            FirstPageText="<%$ Resources:Lang, Common_FirstPage%>" LastPageText="<%$ Resources:Lang, Common_LastPage%>" NextPageText="<%$ Resources:Lang, Common_NextPage%>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                        </webdiyer:aspnetpager>
                    </li>
                    <li>
                        <div><%=Resources.Lang.Common_TotalPage %> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_TotalPage1 %></div>
                    </li>
                </ul>
                    
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdINASSIT_D.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
</asp:Content>

