<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true"
    Title="--<%$ Resources:Lang, FrmBase_LOGOCHANGE_Title01%>" CodeFile="FrmBase_LOGOCHANGE.aspx.cs" Inherits="BASE_FrmBase_LOGOCHANGE" %><%--客户信息设置--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />


    <script type="text/jscript">

        function selected(obj) {
            var str = "carpoolpic";
            str = str + obj.value;
            var imgSrc = document.getElementById(str).value;
            if (imgSrc == "" || imgSrc == null) {
                alert("<%= Resources.Lang.FrmBase_LOGOCHANGE_Msg01%>");//此项没有图片
                obj.checked = false;
            }
            changesrc(str);
        }

        var flag = true;
        function showPic() {
            var sender = "<%=fuFile.ClientID%>";

            if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
                var imgSrc = document.getElementById(sender).value;
                var t = document.getElementById(sender);
                if (imgSrc == "") {
                    flag = false;
                    return false;
                }
                if ((/(.*?)\.gif/.test(imgSrc.toLowerCase()) == false) &
                    (/(.*?)\.png/.test(imgSrc.toLowerCase()) == false) &
                    (/(.*?)\.gpj/.test(imgSrc.toLowerCase()) == false)) {
                    document.getElementById(sender).value = "";
                    alert("<%= Resources.Lang.FrmBase_LOGOCHANGE_Msg02%>");//只能选择gif,png,gpj格式!
                    flag = false;
                    return false;
                } else {
                    var imgs = document.createElement("img");
                    imgs.src = imgSrc;
                    if (imgs.fileSize > 50 * 1024) {
                        document.getElementById(sender).value = "";
                        alert("<%= Resources.Lang.FrmBase_LOGOCHANGE_Msg03%>");//图片大小不能超过 50 KB!
                        flag = false;
                        return false;
                    }

                    flag = true;
                }
                document.getElementById("<%=Image1.ClientID%>").src = window.URL.createObjectURL(t.files[0]);
            } else {
                document.getElementById(sender).select();
                var imgSrc = document.selection.createRange().text;
                if (imgSrc == "") {
                    flag = false;
                    return false;
                }
                if (/(.*?)\.gif/.test(imgSrc.toLowerCase()) == false) {
                    document.getElementById(sender).value = "";
                    alert("<%= Resources.Lang.FrmBase_LOGOCHANGE_Msg04%>");//只能选择gif格式!
                    flag = false;
                    return false;
                } else {
                    var imgs = document.createElement("img");
                    imgs.src = imgSrc;
                    flag = true;
                }
                document.getElementById("<%=Image1.ClientID%>").src = imgSrc;
            }
        }

        function showPicZ() {
            var sender = "<%=fuFileZ.ClientID%>";

            if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
                var imgSrc = document.getElementById(sender).value;
                var t = document.getElementById(sender);
                if (imgSrc == "") {
                    flag = false;
                    return false;
                }
                if ((/(.*?)\.gif/.test(imgSrc.toLowerCase()) == false) &
                    (/(.*?)\.png/.test(imgSrc.toLowerCase()) == false) &
                    (/(.*?)\.gpj/.test(imgSrc.toLowerCase()) == false)) {
                    document.getElementById(sender).value = "";
                    alert("<%= Resources.Lang.FrmBase_LOGOCHANGE_Msg02%>");//只能选择gif,png,gpj格式!
                    flag = false;
                    return false;
                } else {
                    var imgs = document.createElement("img");
                    imgs.src = imgSrc;
                    if (imgs.fileSize > 50 * 1024) {
                        document.getElementById(sender).value = "";
                        alert("<%= Resources.Lang.FrmBase_LOGOCHANGE_Msg03%>");//图片大小不能超过 50 KB!
                        flag = false;
                        return false;
                    }

                    flag = true;
                }
                document.getElementById("<%=Image2.ClientID%>").src = window.URL.createObjectURL(t.files[0]);
            } else {
                document.getElementById(sender).select();
                var imgSrc = document.selection.createRange().text;
                if (imgSrc == "") {
                    flag = false;
                    return false;
                }
                if (/(.*?)\.gif/.test(imgSrc.toLowerCase()) == false) {
                    document.getElementById(sender).value = "";
                    alert("<%= Resources.Lang.FrmBase_LOGOCHANGE_Msg04%>");//只能选择gif格式!
                    flag = false;
                    return false;
                } else {
                    var imgs = document.createElement("img");
                    imgs.src = imgSrc;
                    flag = true;
                }
                document.getElementById("<%=Image2.ClientID%>").src = imgSrc;
            }
        }

        function checkpic() {
            var raFlag = false;
            var obj = document.getElementsByName("select");
            for (var i = 0; i < obj.length; i++) {
                if (obj[i].checked) {
                    raFlag = true;
                    break;
                } else {
                    raFlag = false;
                }
            }
            if (raFlag == false && flag == true) {
                alert("<%= Resources.Lang.FrmBase_LOGOCHANGE_Msg05%>");//请选择最新的照片
            }
            var subFlag = false;
            subFlage = flag && raFlag;
            flag = false;
            return subFlage;
        }

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.FrmBase_LOGOCHANGE_Title01%><%--基础资料-&gt;客户信息设置--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="tabCondition">
                    <tr>
                        <th align="left" colspan="6">&nbsp;&nbsp;
                             <%= Resources.Lang.FrmBase_LOGOCHANGE_Msg06%><%--Logo更换--%>
                        </th>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <span><%= Resources.Lang.FrmBase_LOGOCHANGE_Msg07%></span><%--公司名稱--%>
                            &nbsp;                          
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="NormalInputText"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="width: 30%">
                            <asp:FileUpload ID="fuFile" runat="server" />
                        </td>
                        <td rowspan="3">
                            <asp:Image ID="Image1" runat="server" Height="51px" Width="398px" onerror="this.onload = null; this.src='../../Layout/CSS/LG/Images/Top/in_logo.gif';"/>
                            &nbsp;  
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <span style="color: #FF0000; font-weight: bold">建议图片高度为51px，宽度为398px,否则会拉伸！</span><%--建议图片高度为51px，宽度为398px,否则会拉伸！--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Button ID="btnUp" runat="server" CssClass="ButtonExcel" Text="<%$ Resources:Lang, FrmBase_LOGOCHANGE_btnUp%>" OnClick="btnUp_Click"></asp:Button><%--上传--%>
                        </td>
                    </tr>                
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"  style="margin-top:30px;">
                    <tr>
                        <th align="left" colspan="6">&nbsp;&nbsp;
                         <%= Resources.Lang.FrmBase_LOGOCHANGE_Msg09%><%--登陆页面图片更换--%>
                        </th>
                    </tr>
                    <tr>
                        <td colspan="5" style="width: 30%">
                            <asp:FileUpload ID="fuFileZ" runat="server" />
                        </td>
                        <td rowspan="3">
                            <asp:Image ID="Image2" runat="server" Height="87px" Width="356px" onerror="this.onload = null; this.src='../../Layout/multi/images/cn/top_logo.png';" />
                            &nbsp;  
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <span style="color: #FF0000; font-weight: bold">建议图片高度为87px，宽度为356px,否则会拉伸！</span><%--建议图片高度为87px，宽度为356px,否则会拉伸！--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Button ID="btnUpZ" runat="server" CssClass="ButtonExcel" Text="<%$ Resources:Lang, FrmBase_LOGOCHANGE_btnUp%>" OnClick="btnUpZ_Click"></asp:Button> <%--上传--%>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" class="InputLabel_O" id="tdOut" runat="server"></td>
            <asp:Label ID="lblCaption" runat="server" Text=""></asp:Label>
        </tr>
        <tr valign="top">
            <td valign="top"></td>
        </tr>
        <tr>
            <td>
                <div style="height: 300px; overflow-x: scroll; width: 100%; text-align: left; color: Red;" id="DivScroll">
                    <asp:Label ID="lblMsg" runat="server" Text="<%$ Resources:Lang, FrmBase_LOGOCHANGE_lblMsg%>"></asp:Label><%--上传消息--%>
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
