<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Content.aspx.cs" Inherits="Layout_BaseLayout_Content" %>
<html>
<head id="Head1" runat="server">
    <title></title>
     <link rel="Stylesheet" type="text/css" href="../TabControl/dhtmlxtabbar.css"/>
</head>
<body style="margin:0px;">
    <%--<script src="../Js/Language.js" type="text/javascript"></script>--%>
	<script  src="../TabControl/dhtmlxcommon.js" type="text/javascript"></script>
	<script  src="../TabControl/dhtmlxtabbar.js" type="text/javascript"></script>
	<script src="../TabControl/dhtmlxcontainer.js" type="text/javascript"></script>

	<div id="a_tabbar" style="width:100%;height:100%"/>
			
    <script type="text/javascript">

        var tabbar = new dhtmlXTabBar("a_tabbar", "top");
        tabbar.setOnActiveFunction(OnTabSetActive); 
        //tabbar
        tabbar.setHrefMode("iframes-on-demand");
        var css_name = '<% Response.Write(WmsWebUserInfo.GetCurrentUser().CSS_Name); %>';
        if (css_name == "LG" || css_name == "LG")
            tabbar.setSkin("dhx_skyblue");
        else
            tabbar.setSkin("default");
        tabbar.setImagePath("../TabControl/imgs/");
        tabbar.enableTabCloseButton(true);
        tabbar.enableAutoReSize(true);
        function OnTabSetActive(id) {
            window.top.findMenu(id.replace('tabPage', ''));
        }
    </script>
    <script type="text/javascript">
        var i = 0;
        function ChangeDivWidth() {
            document.getElementById("a_tabbar").style.width = window.document.body.offsetWidth - 2;
            document.getElementById("a_tabbar").style.height = window.document.body.offsetHeight - 2;
        }

        window.onresize = ChangeDivWidth;
        ChangeDivWidth();
    </script>  
        <script type="text/javascript">

            function GetLengthByByte(value) {
                var li_len = value.length;
                for (var i = 0; i < value.length - 1; i++) {

                    if (value.charCodeAt(i) > 127)
                        li_len = li_len + 1;
                }
                return li_len;
            }
            

            function NewTabPage(id, title, link) {
                //1:先判断该页面是否已经打开,如已打开则直接将已打开的标签置为活动状态
                //alert("Context,NewTabPage," + id + " " + title + " " + link);
                var v_tabPageID = "tabPage" + id;
                var tab = eval("tabbar._tabs." + v_tabPageID);

                if (tab) {
                    tabbar.setTabActive(v_tabPageID);
                    //return;
                }

                //----------------------------------------------------------------------------
                //2:打开一个新的标签页

                //v_tabPageName = "tabPage"+id;

                var titleElementLen = GetLengthByByte(title) * 10 + 25;
                titleElementLen = Math.max(titleElementLen, 100);
                 if (tab == null) {
                    tabbar.addTab(v_tabPageID, title, titleElementLen);
                }
                tabbar.setContentHref(v_tabPageID, link);
                tabbar.setTabActive(v_tabPageID);
                //opendIDlist.push(id);

            } //end function
            //首页
            NewTabPage("z9999", '<%=Resources.Lang.Content_MSG1%>', '<% Response.Write(Session["ContentPage"]); %>');
            
        </script>
    
    <script language="javascript" type="text/javascript">
        function SetFormSize() {
            var v_form = document.getElementById("form1");
            v_form.clientWidth = window.parent.document.getElementById("ifrContent").clientWidth;
            //v_form.style.width = document.body.clientWidth;
            v_form.style.position = "absolute";
            v_form.style.left = -8;
            // alert("Form width styleWidth:"+v_form.style.width+" clientWidth:"+v_form.clientWidth +"<br/>"+"document.body width styleWidth:"+document.body.style.width+" clientWidth:"+document.body.clientWidth);
            var tabContainer = document.getElementById("a_tabbar");
            tabContainer.offsetLeft = 0;
            tabContainer.offsetTop = 0;
        }
        // SetFormSize();
    </script>
</body>
</html>