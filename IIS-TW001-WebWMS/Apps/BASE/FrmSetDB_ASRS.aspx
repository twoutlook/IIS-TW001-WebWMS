<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmSetDB_ASRS.aspx.cs"
    Inherits="FrmSetDB_ASRS" Title="--<%$ Resources:Lang, FrmSetDB_ASRS_Title01%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %><%--设置数据连接--%>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.FrmSetDB_ASRS_Title02%><%--基础资料-&gt;AS/RS数据库连接--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">    
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <%--<ucShowWAREHOUSEDiv:ShowWAREHOUSEDiv ID="ucShowWAREHOUSEDiv" runat="server" />
    <ucShowBASE_CARGOSPACEDiv:ShowBASE_CARGOSPACEDiv ID="ucBASE_CARGOSPACEDiv" runat="server" />
    <ucShowVENDORDiv:ShowVENDORDiv ID="ucVENDORDiv" runat="server" />--%>
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
                            <asp:Label ID="lblPALLETID" runat="server" Text="DB IP："></asp:Label>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtIP" runat="server" CssClass="NormalInputText" Width="85%"></asp:TextBox>
                             
                            
                        </td>  
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%"> 
                            <span class="requiredSign">*</span>
                            <asp:Label ID="Label3" runat="server" Text="DataBase："></asp:Label>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtdatabase" runat="server" CssClass="NormalInputText" Width="85%"></asp:TextBox>
                            
                           
                        </td>  
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                             <span class="requiredSign">*</span>
                            <asp:Label ID="lblPALLETNAME" runat="server" Text="AcCount"></asp:Label>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtAcCount" runat="server" CssClass="NormalInputText" Width="85%"></asp:TextBox>
                             
                          
                        </td>

                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="Label1" runat="server" Text="PassWord"></asp:Label>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="NormalInputText" Width="85%"></asp:TextBox>
                             
                           
                        </td>
                    </tr>
                      <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label><%--备注--%>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtMemo" runat="server" CssClass="NormalInputText" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                   <tr style="display:none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="txtID"></asp:Label>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="85%"></asp:TextBox>
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
            <td valign="top" align="center">
                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server"
                    DisplayMode="BulletList" ShowMessageBox="true" />
            </td>
        </tr>
    </table>
     <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <asp:Button ID="btnTest" runat="server" CssClass="ButtonDo" OnClick="btnTest_Click"
                    Text="<%$ Resources:Lang, FrmSetDB_ASRS_btnTest%>"/><%--测试--%>
                &nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" Text="<%$ Resources:Lang, Common_btnSave%>" 
                    onclick="btnSave_Click" Width="62px"  /><%--保存--%>
            </td>
        </tr>
         <tr>
             <td>
                 <asp:Label ID="lblMessage" runat="server" style="color:red;" ></asp:Label>
             </td>
         </tr>
    </table>
</asp:Content>
