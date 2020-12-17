<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBASE_VENDOREdit.aspx.cs" Inherits="BASE_FrmBASE_VENDOREdit"
    Title="--<%$ Resources:Lang, FrmBASE_VENDORList_Title01%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %><%--供应商管理--%>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css"
        id="cssUrl" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBASE_VENDORList_Title01%>-&gt;<%= Resources.Lang.FrmBASE_VENDOREdit_Title01%><%--供应商管理-&gt;供应商详情--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register  Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajaxToolkit" %> <ajaxToolkit:toolkitscriptmanager ID="scriptManager1" runat="server"></ajaxToolkit:toolkitscriptmanager> <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
   
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
                    			        left:-5px; 
                    			        top:2px; 
                    			} 
                    		</style> 
                    	</td>
                    </tr>
                    <tr>
                    	<td class="InputLabel" style="width:13%">                           
                            <span class="requiredSign">*</span>
                    		<asp:Label ID="lblCVENDORID" runat="server" Text="<%$ Resources:Lang, FrmWAREHOUSEList_LEADERCODE%>"></asp:Label>： <%--供应商编码--%>
                    	</td>
                    	<td style="width:20%">
                    	<asp:TextBox ID="txtCVENDORID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                    	
                    	<asp:RequiredFieldValidator ID="rfvtxtCVENDORID"   runat="server"  
                    	    ControlToValidate="txtCVENDORID" ErrorMessage="<%$ Resources:Lang, FrmBASE_VENDOREdit_rfvtxtCVENDORID%>" Display="None"> <%--请填写供应商编码!--%>
                    	</asp:RequiredFieldValidator> 
                    </td>
                    <td class="InputLabel" style="width:13%">                       
                        <span class="requiredSign">*</span>
                    	<asp:Label ID="lblCVENDOR" runat="server" Text="<%$ Resources:Lang, FrmWAREHOUSEList_LEADER%>"></asp:Label>：<%--供应商名称--%>
                    </td>
                    <td style="width:20%">
                    <asp:TextBox ID="txtCVENDOR" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator ID="rfvtxtCVENDOR"   runat="server"  
                        ControlToValidate="txtCVENDOR" ErrorMessage="<%$ Resources:Lang, FrmBASE_VENDOREdit_rfvtxtCVENDOR%>" Display="None"> <%--请填写供应商名称!--%>
                    </asp:RequiredFieldValidator> 
                    </td>
                    <td class="InputLabel" style="width:13%">
                    	<asp:Label ID="lblCALIAS" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCALIAS%>"></asp:Label>： <%--助记码--%>
                    </td>
                    <td style="width:20%">
                    <asp:TextBox ID="txtCALIAS" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>
                    </td>
                    </tr>
                    <tr>
                    	<td class="InputLabel" style="width:13%">
                    		<asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCERPCODE%>"></asp:Label>： <%--ERP编码--%>
                    	</td>
                    	<td style="width:20%">
                    	<asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>
                    </td>
                    <td class="InputLabel" style="width:13%">
                    	<asp:Label ID="lblCCONTACTPERSON" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCCONTACTPERSON%>"></asp:Label>： <%--联系人--%>
                    </td>
                    <td style="width:20%">
                    <asp:TextBox ID="txtCCONTACTPERSON" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                    </td>
                    <td class="InputLabel" style="width:13%">
                    	<asp:Label ID="lblCPHONE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCPHONE%>"></asp:Label>： <%--联系电话--%>
                    </td>
                    <td style="width:20%">
                    <asp:TextBox ID="txtCPHONE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                    </td>
                    </tr>
                    <tr>
                    	<td class="InputLabel" style="width:13%">
                    		<asp:Label ID="lblCTNPE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_CTNPE%>"></asp:Label>：<%--供应商类型--%>
                    	</td>
                    	<td style="width:21%">
                    	<asp:TextBox ID="txtCTNPE" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                    </td>
                    <td class="InputLabel" style="width:13%">
                    	<asp:Label ID="lblCADDRESS" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_CADDRESS%>"></asp:Label>：<%--联系地址--%>
                    </td>
                    <td style="width:21%">
                    <asp:TextBox ID="txtCADDRESS" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"></asp:TextBox>
                    </td>
                    <td class="InputLabel" style="width:13%">
                    	<asp:Label ID="lblILEVEL" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblILEVER%>"></asp:Label>：  <%--级别--%>
                    </td>
                    <td style="width:21%">
                    <asp:TextBox ID="txtILEVEL" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_txtIDS%>"></asp:TextBox><%--格式：最多10位整数，最多4位小数--%>
                    <asp:RegularExpressionValidator ID="revtxtILEVEL"   runat="server" ValidationExpression="^[0-9]{1,10}$" ControlToValidate="txtILEVEL" ErrorMessage="<%$ Resources:Lang, FrmBASE_CLIENTEdit_revtxtILEVER%>" Display="None"> 
                    </asp:RegularExpressionValidator> <%--请填写有效的级别!正确的格式是：最多10位整数--%>
                    </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width:13%">
                    	    <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, FrmBASE_VENDOREdit_lblCSTATUS%>" ></asp:Label>： <%--是否启用--%>
                        </td>
                        <td style="width:20%">
                            <asp:CheckBox ID="cboCSTATUS" runat="server" Checked="true" />
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label> <%--创建人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcreateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label><%--创建时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtcreatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"  Enabled="False"></asp:TextBox>
                        </td>


                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label3%>"></asp:Label> <%--修改人--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtupdateuser" runat="server" CssClass="NormalInputText" Width="95%"  Enabled="False"></asp:TextBox>
                           
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label4%>"></asp:Label>  <%--修改时间--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtupdatetime" runat="server" CssClass="NormalInputText" Width="95%"  Enabled="False"></asp:TextBox> 
                        </td>
                        <td class="InputLabel" style="width:13%">
                    	    <asp:Label ID="lblIDS" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="False"></asp:Label><%--： --%><%--编号--%>
                        </td>
                        <td style="width:20%">
                        <asp:TextBox ID="txtIDS" runat="server" CssClass="NormalInputText" Width="95%" 
                                MaxLength="36" Visible="False"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                    	<td class="InputLabel" style="width:13%">
                    		<asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：<%--备注--%>
                    	</td>
                    	<td style="width:20%;" colspan="5" >
                    	    <asp:TextBox ID="txtCMEMO" runat="server" TextMode="MultiLine" Width="95%" MaxLength="100"></asp:TextBox>
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
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                    Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp; &nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="ButtonDel"  
                             onclick="btnDelete_Click" Text="<%$ Resources:Lang, Common_Delete%>" CausesValidation="false" 
                    Visible="False" /> <%--删除--%>	
 &nbsp;&nbsp; <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>"  CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>
</asp:Content>

