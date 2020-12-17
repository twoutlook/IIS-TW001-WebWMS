<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmUPGDData.aspx.cs" Inherits="FrmUPGDData"
    Title="--111" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
     <%=Resources.Lang.CommonB_OUTBILLManagement%> <%--出库管理--%>-&gt;<%=Resources.Lang.CommonB_WorkOrderINFO%><%--工单信息维护--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
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
                   
                    <tr valign="top">
                        <td valign="top" align="left">
                            &nbsp;<font color="black"><%=Resources.Lang.FrmUPGDData_MSG1%>：<%--工单数据文件：--%></font>
                            <asp:FileUpload ID="fuFile" runat="server" />
                            &nbsp;<%--<asp:Button ID="btnNew" runat="server" CssClass="ButtonExcel" Text="上传" OnClick="btnNew_Click">
                </asp:Button>--%>
                        <a href="/ExcelTemplate/工单信息维护.xls"><%=Resources.Lang.FrmUPGDData_MSG2%><%--下载模版--%></a></td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" class="InputLabel_O" id="tdOut" runat="server" style="color: red;">
                            <%--<asp:Literal ID="ltMsg" runat="server"></asp:Literal>--%>
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
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <%--<asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                    Text="<%$ Resources:Lang, Common_Save %>" />--%>
                <%--上传--%>
                <asp:Button ID="btnNew" runat="server" CssClass="ButtonExcel" Text="<%=Resources.Lang.FrmUPGDData_MSG3%>"
                        OnClick="btnUP_Click"></asp:Button>
                &nbsp;&nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>" CausesValidation="false" />
            </td>
        </tr>
    </table>
   
</asp:Content>
