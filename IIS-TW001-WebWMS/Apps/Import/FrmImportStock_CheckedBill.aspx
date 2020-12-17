<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="<%$ Resources:Lang FrmImportStock_CheckedBill_title%>--" CodeFile="FrmImportStock_CheckedBill.aspx.cs" Inherits="Import_FrmImportStock_CheckedBill" %>
<%--物理实盘单明细导入--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.FrmImportStock_CheckedBill_content1%><%--导入--%>-&gt;<%=Resources.Lang.FrmImportStock_CheckedBill_content2%><%--导入明细--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                   <%-- <tr>
                        <th align="left" colspan="5">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                height="11" alt="" runat="server" id="titleImg" />
                            <%=Resources.Lang.Common_JSCeria%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%=Resources.Lang.Common_Fold%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>--%>
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
                        <td colspan="6">
                            <asp:FileUpload ID="fuFile" runat="server" />
                            &nbsp;
                            <asp:DropDownList id="ddlTeyp" runat="server">
                                <asp:ListItem Value="2" Selected="True">初盘-物理盘点</asp:ListItem>
                                <asp:ListItem Value="3">复盘-物理盘点</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnUp" runat="server" CssClass="ButtonExcel" Text="<%$ Resources:Lang, CommonB_ImportFile %>" OnClientClick="return CheckIsImportExcel()" 
                            onclick="btnUp_Click"></asp:Button><%--导入--%>
                            &nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>" CausesValidation="false" />
                            <a href="/ExcelTemplate/Stock_Checkedbill.xls"><%=Resources.Lang.FrmUPGDData_MSG2%><%--下载模版--%></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
          <tr valign="top">
                        <td valign="top" class="InputLabel_O" id="tdOut" runat="server">
                            
                        </td><asp:Label ID="lblCaption" runat="server" Text=""></asp:Label>
                    </tr>
        <tr valign="top">
            <td valign="top">
                
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 300px; overflow-x: scroll; width: 100%; text-align:left; color:Red; " id="DivScroll">
                    <asp:Label ID="lblMsg" runat="server" Text="<%$ Resources:Lang, FrmImportStock_CheckedBill_lblMsg %>"></asp:Label>：<%--上传消息--%>
                </div>
            </td>
        </tr>
        <script type="text/javascript">
            function ChangeDivWidth() {
                document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
            }
            window.onresize = ChangeDivWidth;
            ChangeDivWidth();
            
            function CheckIsImportExcel() {
                if (confirm(<%= Resources.Lang.FrmImportStock_CheckedBill_JSMsg1 %>)) {//"请再次确认是否导入？"
                    return true;
                }
                else {
                    return false;
                }
            }
            
        </script>
    </table>
</asp:Content>
