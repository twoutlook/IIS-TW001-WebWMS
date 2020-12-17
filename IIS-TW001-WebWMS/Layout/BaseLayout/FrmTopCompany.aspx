<%@ Page language="c#" Inherits="DreamTek.WebWMS.Web.FrmTopCompany" CodeFile="FrmTopCompany.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
<%--<meta http-equiv="refresh" content="400"/> --%>
<title></title>
<style type="text/css">

body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	background-color: #ededed;
}
.toggleopacity img{ 
filter:progid:DXImageTransform.Microsoft.Alpha(opacity=70); 
-moz-opacity: 0.7; 
} 

.toggleopacity:hover img{ 
filter:progid:DXImageTransform.Microsoft.Alpha(opacity=100); 
-moz-opacity: 1; 
} 

.toggleborder:hover{ 
color: red; /* Dummy definition to overcome IE bug */ 
} 


</style>
<link href="../CSS/LG/top.css" rel="stylesheet" type="text/css" id="cssUrl" runat="server" />
<script type="text/javascript" language="JavaScript">
<!--

function SwapImage(imgID)
{
    var altImg = document.getElementById(imgID).src;
    document.getElementById(imgID).src = document.getElementById(imgID).alt; 
    document.getElementById(imgID).alt = altImg;
}

function ShowClick() {
    window.top.document.getElementById('aShowCompany').click();
    //document.getElementById('btnShowChangeCompany').click;
    return false;
}


//-->
</script>
</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                        <td width="398" >
                            <asp:Image ID="imgLogo" runat="server" Height="51px" Width="398px" onerror="this.onload = null; this.src='../../Layout/CSS/LG/Images/Top/in_logo.gif';"/>
                        </td>
                        <td style="background-image:url(../CSS/LG/images/Top/in_top_bg.gif);background-repeat:repeat-x" id="ImgTop1" >
                        <table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
                          <tr>
                            <td align="center" style="font-size:12px"><%=Resources.Lang.FrmTopCompany_MSG1 %><%--用户--%>：<span class="in_user" id="lblSessionUserName" runat="server">王晓鸥</span> <span class="in_time" id="lblDate" runat="server">(2009-10-04 12:16)</span></td>
                            <td width="280" align="right">

                            <a href="#" runat="server"  id="aChange" onclick="return ShowClick()" onmouseout="SwapImage('img4')" onmouseover="SwapImage('img4')" class="toggleopacity" target="top" style=" font-size:14px;font-weight:bold;display:none;"  >
                            <img  src="../CSS/LG/images/Top/in_company.gif"  alt="../CSS/LG/images/Top/in_company_over.gif"
                                  title="<%=Resources.Lang.FrmTopCompany_MSG3 %>" id="img4" width="65" height="20" border="0"/></a><%--切换公司--%>

                            <a href="#" onclick="window.top.location.href = 'login.aspx';return false" onmouseout="SwapImage('img1')" onmouseover="SwapImage('img1')" class="toggleopacity" target="top">
                            <img  src="../CSS/LG/images/Top/in_relogin.gif"  alt="../CSS/LG/images/Top/in_relogin_over.gif" 
                                  title="<%= Resources.Lang.FrmTopCompany_MSG4 %>" id="img1" width="65" height="20" border="0"/></a><%--重新登录--%>
                            <a href="#" onclick="window.top.ifrContent.NewTabPage('z9999','<%=Resources.Lang.FrmTopCompany_MSG5 %>','../../Apps/FrmFirstPage.aspx');return false" onmouseout="SwapImage('img2')" onmouseover="SwapImage('img2')" class="toggleopacity" target="right"><%--首页--%>      
                                <img src="../CSS/LG/images/Top/in_home.gif" alt="../CSS/LG/images/Top/in_home_over.gif" title="<%= Resources.Lang.FrmTopCompany_MSG6 %>" id="img2" width="65" height="20" border="0" class="" /></a><%--返回首页--%>
                            <a href="#" onmouseout="SwapImage('img3')" onmouseover="SwapImage('img3')" class="toggleopacity">
                            <img src="../CSS/LG/images/Top/in_help.gif" alt="../CSS/LG/images/Top/in_help_over.gif" title="<%=Resources.Lang.FrmTopCompany_MSG7 %>" id="img3" width="46" height="20" border="0" /></a><%--帮助--%>
                            </td>
                            </tr>
                        </table>
                        </td>
                        <td width="114" align="right">
                        <img src="../CSS/LG/images/Top/in_logo_2.gif" alt="" width="114" height="51" id="ImgLogo2" />
                        </td>
                 </tr>
                  <%--<tr>
                  <td></td>
                  </tr>--%>
                </table>
                <%--<cc1:MainMenu  ID="MainMenu1" runat="server" LeftFrame="Menu" ContentFrame="Content" Width="0px" Visible="false"/>--%>
    <script type="text/javascript">
        var css_name = "<% Response.Write(WmsWebUserInfo.GetCurrentUser().CSS_Name); %>";
        var imagePath = "../CSS/" + css_name + "/Images/Top/";


        var multi_name = "<% Response.Write(Resources.Lang.WMS_Common_MultiUrl); %>";
        var multiPath = "../multi/images/" + multi_name + "/";


        document.getElementById("ImgTop1").style.backgroundImage = "url(" + imagePath + "in_top_bg.gif)";
        document.getElementById("img1").src = multiPath + "in_relogin.gif";
        document.getElementById("img1").alt = multiPath + "in_relogin_over.gif";
        document.getElementById("img2").src = multiPath + "in_home.gif";
        document.getElementById("img2").alt = multiPath + "in_home_over.gif";
        document.getElementById("img3").src = multiPath + "in_help.gif";
        document.getElementById("img3").alt = multiPath + "in_help_over.gif";
        document.getElementById("ImgLogo2").src = imagePath + "in_logo_2.gif";
        function Goto(leftMenuurl, firstPage,title) {

            window.top.Menu.location.href = leftMenuurl;
            window.top.Content.document.getElementById("spUrl").title = title
            window.top.Content.document.getElementById("spUrl").value = firstPage;
            window.top.Content.document.getElementById("spUrl").click();
            return false;
        }  
    </script>
		</form>
	</body>
</html>
