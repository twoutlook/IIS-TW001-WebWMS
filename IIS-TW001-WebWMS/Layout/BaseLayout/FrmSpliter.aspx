<%@ Page language="c#" Inherits="FrmSpliter" CodeFile="FrmSpliter.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server">
		<title></title>		
				
		
	</head>
	<script type="text/javascript">
    var imagePath = '../../Layout/CSS/LG/images/Spliter/';
	function menuSwitch()
	{
		if(parent.MainFrameSet.cols=="0,15,*")
		{				
			parent.MainFrameSet.cols=OldCols;
			splitter.title = "点击，隐藏菜单！";
			document.getElementById("arrow").src=imagePath + 'arrow_left.gif';
		}
		else
		{
		    OldCols = parent.MainFrameSet.cols;
			parent.MainFrameSet.cols="0,15,*";				
			splitter.title = "点击，展开菜单！";
			document.getElementById("arrow").src = imagePath + 'arrow_right.gif';
		}
}
function tollget(e) {
    window.parent.hide(this);
    var obj = document.getElementById("splitter");
    var imgObj = document.getElementById("arrow");
    if (window.parent.document.getElementById("menu").style.display != "block") {
        obj.title = "点击，隐藏菜单！";
        imgObj.src = imagePath + 'arrow_right.gif';
    }
    else {
        obj.title == "点击，展开菜单！"
        imgObj.src = imagePath + 'arrow_left.gif';
    }
}
	
	</script>
	<body style="MARGIN: 0px" >
	    <form runat="server">
	    <table style="width:100%;height:100%" border="0" cellpadding="0" cellspacing="0" id="Table2">
			<tr>
				<td height="66"><img alt="" src="../../Layout/CSS/LG/images/Spliter/arrow_top_bg.gif" id="splittertop"/></td>
			</tr>
			<tr>
				<td style="Cursor:Hand;background-image:url(../../Layout/CSS/LG/images/Spliter/arrow_bg.gif)" id="splitter"  title="点击，隐藏菜单！" onclick="tollget()"
					height="100%" width="5"><img alt="" src="../../Layout/CSS/LG/images/Spliter/arrow_left.gif" id="arrow"/>
					
				</td>
			</tr>
			<tr>
				<td height="66"><img src="../../Layout/CSS/LG/images/Spliter/arrow_bottom_bg.gif" alt="" id="splitterbottom"/></td>
			</tr>
		</table> 
		<script type="text/javascript">
		    document.getElementById("arrow").src = imagePath + 'arrow_left.gif';	
	        document.getElementById("splitter").style.backgroundImage = "url(" + imagePath + "arrow_bg.gif)";
	        document.getElementById("splittertop").src = imagePath + "arrow_top_bg.gif";
	        document.getElementById("splitterbottom").src = imagePath + "arrow_bottom_bg.gif";
		</script>
		</form>
	</body>
</html>
