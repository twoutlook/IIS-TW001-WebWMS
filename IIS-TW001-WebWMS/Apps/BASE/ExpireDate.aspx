<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpireDate.aspx.cs" Inherits="Apps_BASE_ExpireDate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">

                     <tr>
                        <th align="left" colspan="5">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
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

                    <tr>
                        <td class="InputLabel" style="width: 13%">
                             <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblDINDATEFromFrom" runat="server" Text="<%$ Resources:Lang, ExpireDate_lblDINDATEFromFrom%>"></asp:Label>：<%--系统过期日期--%>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                            <asp:TextBox ID="txtDate" runat="server" CssClass="NormalInputText" Width="25%" ></asp:TextBox>
                           <img border="0" align="absmiddle" alt="" style="cursor: hand; position: relative;
                                left: -30px; top: 0px" src="../../Layout/Calendar/Button.gif" onclick="return showCalendar('txtDate','y-mm-dd',0);" />
                                
                        </td>
                       
                    </tr>
                  
                    
                    <tr>                        
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, ExpireDate_Label1%>"></asp:Label>：<%--过期提示信息--%>
                            
                        </td>
                        <td style="width: 86%; white-space: nowrap;" >
                            <asp:TextBox ID="txtInfo" runat="server" CssClass="NormalInputText" 
                                Width="50%" TextMode="MultiLine" Height="100px"></asp:TextBox>
                            
                        </td>

                    </tr>                    
                 
                  
                </table>


                
     <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
               
                &nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" Text="<%$ Resources:Lang, Common_btnSave%>" 
                    onclick="btnSave_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" 
                    CausesValidation="false" onclick="btnBack_Click" /><%--返回--%>
            </td>
        </tr>
    </table>

            </td>
        </tr>
       
     
    </table>
    </div>
    </form>
</body>
</html>
