<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmError.aspx.cs" EnableSessionState="True"
    Inherits="FrmError" Title="<%$Resources:Lang,FrmError_MSG1 %>" %><%--错误处理页面--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<head>
    <title><%=Resources.Lang.FrmError_MSG1 %><%--错误处理页面--%></title>
      <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <link href="../../Layout/CSS/LG/MasterPage.css" rel="Stylesheet" type="text/css" id="cssUrlBackground" runat="server" /> 
    <script src="../../Layout/Js/Help.js" type="text/javascript"></script>
    <script src="../../Layout/Js/Cookes.js" type="text/javascript"></script>
     <link href="../styles/bootstrap33.css" rel="stylesheet" />


    <style type="text/css">
        .pagination span.active
        {
            z-index: 2;
            color: #fff;
            cursor: default;
            background-color: #337ab7;
            border-color: #337ab7;
        }

        .ui-autocomplete-loading
        {
            background: white url('../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }
        .select
        {
            cursor: hand;
            position: relative;
            left: -25px;
            top: 4px;
        }
    </style>
     <style type="text/css">
        /*先引入bootstrap.css*/
        .pagination a[disabled]
        {
            color: #777;
            cursor: not-allowed;
            background-color: #fff;
            border-color: #ddd;
        }

        .pagination span.active
        {
            z-index: 2;
            color: #fff;
            cursor: default;
            background-color: #337ab7;
            border-color: #337ab7;
        }

        body
        {
            margin: 0px;
            padding: 0px;
        }

        div.divmain ul
        {
            margin: 0px;
            padding: 0px;
        }

            div.divmain ul li
            {
                margin-top: 0px;
                margin-bottom: 5px;
                padding: 0px;
                list-style: none;
            }
    </style>
      <link href="~/Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="Link1"
        runat="server" />

</head>

<body>
 <%--<script src="../../Layout/Js/Language.js" type="text/javascript"></script>--%>
    <form id="form1" runat="server">
    <script src="../../Layout/Popupwindow/dhtmlxcommon.js" type="text/javascript"></script>
	<script src="../../Layout/Popupwindow/dhtmlxwindows.js" type="text/javascript"></script>
	<script src="../../Layout/Popupwindow/dhtmlxcontainer.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Popupwindow/Popup.js"></script>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table id="in_info_table" style="width: 100%; height: 100%" border="0" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td class="line1_1" width="8" height="7" >
                        </td>
                        <td class="line1_2" height="7" >
                        </td>
                        <td class="line1_3"  width="9" height="7">
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" class="line2_1" width="8" height="100%" >
                        </td>
                        <td align="center" height="33" valign="top">
                            <table width="100%" style="height:33px" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td height="33" width="55">   
                                    <img id="imgLineLeft" alt="" src="../../Layout/CSS/LG/Images/MasterPage/in_line_left.gif" runat="server"
                                            width="42" height="33" />                                     
                                    </td>
                                    <td width="100%" align="left" class="title1_1">
                                        <img id="imgHome" alt="" src="../../Layout/CSS/LG/Images/MasterPage/icon.gif" align="absmiddle"
                                            runat="server" />&nbsp;&nbsp;<span class="photo_title">
                                                   <asp:Label ID="lblTitle" runat="server" Text="<%$Resources:Lang,FrmError_MSG1 %>" CssClass="photo_title"></asp:Label>

                                            </span><%--错误处理页面--%>
                                    </td>
                                    <td width="55">
                                        <img id="imgLineRight" alt="" src="../Layout/CSS/LG/Images/MasterPage/in_line_right.gif" runat="server"
                                            width="42" height="33" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td rowspan="2" class="line2_2"
                            width="9">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" height="100%" align="center">
                          
    <table cellspacing="0" cellpadding="0" width="90%" border="0">
        <tr>
            <td align="right">
                &nbsp;</td>
        </tr>
    </table>
    <table id="Table3" style="height: 100%; width: 90%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width:15%">
                            <%=Resources.Lang.FrmError_MSG2 %><%--错误信息--%></td>
                        <td align="left" class="InputLabel" style="text-align:left">
                            <asp:Label ID="lblError" runat="server" Text="Error"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel">
                            <%=Resources.Lang.FrmError_MSG3 %><%--详细错误信息--%></td>
                        <td align="left" class="InputLabel" style="text-align:left">
                            <asp:Label ID="lblDetailError" runat="server" Text="DetailError"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" colspan="2">
                            <asp:HyperLink ID="hplLogin" runat="server">HyperLink</asp:HyperLink>
&nbsp;&nbsp;
                            <asp:HyperLink ID="hplReturn" runat="server">HyperLink</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" colspan="2">
                            &nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top" align="center">
            </td>
        </tr>
    </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="8" height="12" class="line3_1">
                        </td>
                        <td id="Td1" runat="server" class="line3_2" height="12">
                        </td>
                        <td width="9" height="12" class="line3_3">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
    <link rel="stylesheet" type="text/css" href="../../Layout/Popupwindow/dhtmlxwindows.css"/>
	<link rel="stylesheet" type="text/css" href="../../Layout/Popupwindow/skins/dhtmlxwindows_dhx_skyblue.css"/>
	
    <script type="text/javascript">
        function GetCookie(name) {
            var result = null;
            var myCookie = document.cookie + ";";
            var searchName = name + "=";
            var startOfCookie = myCookie.indexOf(searchName);
            var endOfCookie;
            if (startOfCookie != -1) {
                startOfCookie += searchName.length;
                endOfCookie = myCookie.indexOf(";", startOfCookie);
                result = unescape(myCookie.substring(startOfCookie, endOfCookie));
            }
            return result;
        }
        ///relativePath--相对路径，前后都加/
        function ChangeImagePath(relativePath) {
            var css_name = GetCookie("CSS_Name");
            if (css_name == null)
                return;
            var pattern = "LAYOUT\/CSS\/([A-Z][A-Z0-9_\-]*)" + relativePath + "[A-Z][A-Z0-9\-_]+\.(GIF|JPG|BMP|ICO|PNG|JPEG)";
            try {


                //更换Image路径
                for (var i = 0; i < document.images.length; i++) {
                    var r = new RegExp(pattern, "gi");
                    r.ignoreCase = true;
                    var result = r.exec(document.images[i].src);
                    if (result != null) {
                        //alert(document.images[i].src);
                        document.images[i].src = document.images[i].src.substr(0, result.index + 11) + css_name + document.images[i].src.substr(result.index + 11 + result[1].length);
                    }
                }
                //更换背景图片的路径
                var eleList = document.getElementsByTagName("*");
                for (var i = 0; i < eleList.length; i++) {
                    if (eleList[i].style != null && eleList[i].style.backgroundImage != null && eleList[i].style.backgroundImage != "") {
                        var pattern = "\.\.\/CSS\/([A-Z][A-Z0-9_\-]*)" + relativePath + "[A-Z][A-Z0-9\-_]+\.(GIF|JPG|BMP|ICO|PNG|JPEG)";
                        var r = new RegExp(pattern, "gi");
                        var result = r.exec(eleList[i].style.backgroundImage);
                        if (result != null) {
                            eleList[i].style.backgroundImage = eleList[i].style.backgroundImage.substr(0, result.index + 7) + css_name + eleList[i].style.backgroundImage.substr(result.index + 7 + result[1].length);
                            //alert(eleList[i].style.backgroundImage);
                        }
                    }
                }
                var aReturn = document.getElementsByTagName("input");
                for (var i = 0; i < aReturn.length; i++) {

                    if (aReturn[i].type == "image" && aReturn[i].src != null && aReturn[i].src != "") {
                        var pattern = "\.\.\/CSS\/([A-Z][A-Z0-9_\-]*)" + relativePath + "[A-Z][A-Z0-9\-_]+\.(GIF|JPG|BMP|ICO|PNG|JPEG)";
                        var r = new RegExp(pattern, "gi");
                        var result = r.exec(aReturn[i].src);
                        if (result != null) {
                            aReturn[i].src = aReturn[i].src.substr(0, result.index + 7) + css_name + aReturn[i].src.substr(result.index + 7 + result[1].length);
                            //alert(aReturn[i].src);
                        }
                    }
                }
            }
            catch (e) {
                alert(e.message);
            }
        }
        ChangeImagePath('\/IMAGES\/MasterPage\/');
        ChangeImagePath('\/IMAGES\/');
    </script>
</body>
