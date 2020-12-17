<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainFrameNew.aspx.cs" Inherits="MainFrameNew" %>

<html>
<head runat="server">
    <title><%=Resources.Lang.Login_MSG1 %><%--仓库管理系统--%> WebWMS</title>

    
    <!-- Attach our CSS -->
    <link rel="stylesheet" href="../reveal/reveal.css">
    <!-- Attach necessary scripts -->
    <script type="text/javascript" src="../reveal/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../reveal/jquery.reveal.js"></script>
 
    <style type="text/css">
    body { font-family: "HelveticaNeue", "Helvetica-Neue", "Helvetica", "Arial", sans-serif; }
    .big-link { display: block; margin-top: 100px; text-align: center; font-size: 70px; color: #06f; }
    
    #BgDiv
    {
        background-color:#e3e3e3; position:absolute; z-index:99; left:0; top:0; display:none; width:100%; height:1000px;opacity:0.5;filter: alpha(opacity=50);-moz-opacity: 0.5;
    }
    
    .ulCompanystyle
    {
        padding: 0px; list-style-type: none;
        overflow:hidden;width:100%;
     }
    .liCompanystyle
    {
         text-align:center;float:left;margin-right: 10px;display: inline-block;
         width:30%; margin: 5px 10px 0px 0px;
    }
    .aCompanystyle
    {
        display: block;
        border:none;
     }

     .imgCompanystyle
     {
         border:none;
         width:100px;
         height:60px;
     }
    img {border-width: 0px 0px 0px 0px} 
    
    .pCompanystyle
    {
        height:14px; line-height:14px; display:block; font-size:11px;
    }
    </style>


      <script language="javascript" type="text/javascript">
          function ShowDIV(thisObjID) {
              $("#BgDiv").css({ display: "block", height: $(document).height() });
              var yscroll = document.documentElement.scrollTop;
              //$("#" + thisObjID).css("top", "100px");
              //$("#" + thisObjID).css("display", "block");
              //document.documentElement.scrollTop = 0;
              //alert("show");
          }

          function closeDiv(thisObjID) {
              $("#BgDiv").css("display", "none");
              //alert("hidden");
              //$("#" + thisObjID).css("display", "none");
          }
 </script>

    <style type="text/css">
        *
        {
            margin: 0px;
            padding: 0px;
        }
        html, body
        {
            height: 100%;
            overflow: hidden;
        }
        body
        {
            border: 1px solid #ffffff;
        }
        .container
        {
            width: 100%;
            height: 100%;
        }
        .container .header
        {
            width: 100%;
            height: 50px;
            background-color: #00CCFF;
        }
        .container .footer
        {
            width: 100%;
            height: 20px;
            margin-bottom: 0px;
            /*position: absolute;*/
        }
        .container .mainContent
        {
            width: 100%;
        }
        .mainContent .menu
        {
            width: 167px;
            background-color: #ffffff;
            top: 50px;
        }
        .mainContent .split
        {
            width: 15px;
            background-color: #00FF33;
            position: absolute;
            left: 167px;
            top: 50px;
        }
        .mainContent .content
        {
            background-color: #ffffff;
            position: absolute;
            left: 182px;
            top: 50px;
        }
    </style>
</head>
<body>
    <form runat="server">
   <%-- <script src="../Js/Language.js" type="text/javascript"></script>--%>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server" />
    
    <div id="container" class="container">
        <div id="header" class="header">
           <iframe id="ifrHead" name="head" src="<% =Request.QueryString["TopPage"] %>" width="100%" height="<% =Request.QueryString["TopSize"] %>" scrolling="no"  marginwidth="0" marginheight="0" frameborder="0"></iframe>
        </div>
        <div id="mainContent" class="mainContent">
           <div id="menu" class="menu" style="display: block;"><%--width:167px;--%>
             <iframe id="LeftMenu1" name="LeftMenu1" src="LeftNav.aspx" target="ifrContent" scrolling="no"  width="100%" height="100%"  frameborder="0"></iframe>
             </div>
            <div id="split" class="split" onclick="hide(this);">
                 <iframe id="ifrSplit" name="ifrSplit" src="FrmSpliter.aspx"  scrolling="no" height="100%" width="15px"  marginwidth="0" marginheight="0" frameborder="0"></iframe>
             </div>
            <div id="content" class="content">
                <iframe id="ifrContent" name="ifrContent" src="Content.aspx" scrolling="no" height="100%" width="100%"  frameborder="0"></iframe>
            </div>
        </div>
        <div id="footer" class="footer">
            <iframe id="Iframe2" name="Bottom" src="FrmBottom.aspx" scrolling="no" height="20px" width="100%" marginwidth="0" marginheight="0" frameborder="0"></iframe>
        </div>
    </div>

    <a href="#" id="aShowCompany" class="big-link" onclick="ShowDIV()" data-reveal-id="myModal" data-animation="fade" style=" display:none;"></a>
    <div id="myModal" class="reveal-modal" 
         style="background-color:White; width:900px; height:300px;overflow:scroll;padding:0px; position:absolute;top:30%;left:40%; "> 
        <span style=" background-image:url(../Css/LG/Images/slidingMenuLine.gif); background-repeat:repeat-x; width:100%; margin: 0px 0px 10px 0px; font-size:16px; height:40px;">
             <span style=" margin: 5px 0px 5px 5px"><b><%=Resources.Lang.MainFrameNew_MSG1 %><%--请选择公司--%></b></span>
        </span>	
        <div ID="pnlCompanyList" class="layer" runat="server" style=" overflow:scroll;"> </div>
        <a class="close-reveal-modal" onclick="closeDiv()">×</a>
    </div>

    <div id="BgDiv"></div>
    </form>

<script type="text/javascript" language="javascript">
    //----------------------------------------------------------------------
    //----------------------------------------------------------------------
    //此函数用于菜单导航
    function Goto(id, title, link)
    {
        window.ifrContent.NewTabPage(id,title,link);
    }
    //----------------------------------------------------------------------
    //此函数仅给出一个示例：您可以在javascript中通过如下的方法访问LeftMenu实例
    //并调用其中的方法
    function findMenu(id)
    {
        var menu=$find("LeftMenu1");
        menu.menuItemSetCurrentCSS(this,{"id":"menuItem_" + id,"from":""});
    }
    //----------------------------------------------------------------------
    //此函数用于菜单的显示与隐藏，并调整相关元素的位置、大小
	function hide(e){
	   var menu=document.getElementById("menu");
	   var displayValue = menu.style.display;
	   var split = document.getElementById("split");
	   var content = document.getElementById("content");
	   
	   if(displayValue=="block"){	
	       menu.style.display="none";
	       //$common.setVisible($get("LeftMenu1"),false);
	       split.style.left ="0px";
	       content.style.left = "15px";
	       content.style.width = parseInt(content.style.width) + 160;
	       $get("ifrContent").style.width = content.style.width;
        }
        else{
	           menu.style.display="block";
	           //$common.setVisible($get("LeftMenu1"),true);
	           split.style.left ="167px";
	           content.style.left = "182px";	
	           content.style.width = parseInt(content.style.width) - 160;
	           $get("ifrContent").style.width = content.style.width;
	           
	           windowAutosize();
	           
	           var leftMenu = $find("LeftMenu1");
	           
	           leftMenu._setSize();
        }
	
	}

	function init(){
	
		var header=document.getElementById("header");
		var footer=document.getElementById("footer");
		var mainContent = document.getElementById("mainContent");
		var menu=document.getElementById("menu");
		var split=document.getElementById("split");
		var content=document.getElementById("content");
		
		var height = document.body.clientHeight;
		var width = document.body.clientWidth;
		
		mainContent.style.height = height - header.clientHeight -  footer.clientHeight;
		menu.style.height = split.style.height=content.style.height=mainContent.style.height;
		footer.top = header.clientHeight + mainContent.clientHeight;
		mainContent.style.width = width;
		content.style.width = width - menu.clientWidth - split.clientWidth;
	}
	
	init();

	$(window).resize(function () {
	    init();
	});

	var screenWd = window.screen.width;
	var screenHg = window.screen.height;
	window.moveTo(0, 0);
	window.resizeTo(screenWd, screenHg);

    //-----------------------------------------------------------------------

    </script>

</body>
</html>
