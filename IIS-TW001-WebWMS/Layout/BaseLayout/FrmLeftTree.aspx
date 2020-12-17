<%@ Page Language="C#" CodeFile="FrmLeftTree.aspx.cs" Inherits="FrmLeftTree" %>
<html>
<head runat="server">
    <link rel="Stylesheet" type="text/css" href="../TreeView/dhtmlxtree.css" />
    <title>菜单</title>
    <style type="text/css">
        *
        {
            scrollbar-highlight-color: buttonface;
            scrollbar-shadow-color: buttonface;
            scrollbar-3dlight-color: buttonhighlight;
            scrollbar-track-color: #eeeeee;
            scrollbar-face-color: #CEDFEF;
            scrollbar-darkshadow-color: buttonshadow;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" align="left" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" valign="top">
                <script src="../TreeView/dhtmlxcommon.js" type="text/javascript"></script>
                <script src="../TreeView/dhtmlxtree.js" type="text/javascript"></script>
                <div id="treeBox">
                </div>
                <script language="JavaScript" type="text/javascript">
                    rnd.today = new Date();
                    rnd.seed = rnd.today.getTime();
                    function rnd() {
                        rnd.seed = (rnd.seed * 9301 + 49297) % 233280;
                        return rnd.seed / (233280.0);
                    };
                    function rand(number) {
                        return Math.ceil(rnd() * number);
                    };
                </script>
                <script language="javascript" type="text/javascript">
                    tree = new dhtmlXTreeObject(document.getElementById('treeBox'), "", "", 0);
                    tree.setImagePath('../TreeView/imgs/<% Response.Write(WmsWebUserInfo.GetCurrentUser().CSS_Name); %>/');
                    tree.enableCheckBoxes(false);
                    tree.enableDragAndDrop(false);
                    tree.setXMLAutoLoading("FrmMenu.aspx?ID=" + rand(65535));
                    tree.loadXML("FrmMenu.aspx"); //
                    tree.attachEvent("onClick", onNodeSelect)//
                    function onNodeSelect(nodeId) {
                        var url = tree.getUserData(nodeId, "url");
                        var js = tree.getUserData(nodeId, "js");
                        if (js == null) {
                            if (url != null && url != "") {
                                window.parent.ifrContent.NewTabPage(nodeId, tree.getItemText(nodeId), url);       
                            }
                        }
                        else
                            eval(js);
                    }
                </script>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
