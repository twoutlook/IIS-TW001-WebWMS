//去空格函数
//用法： var str = "  hello "; alert(str.trim());
String.prototype.trim = function()
{
    return this.replace(/(^[\\s]*)|([\\s]*$)/g, "");
}
String.prototype.lTrim = function()
{
    return this.replace(/(^[\\s]*)/g, "");
}
String.prototype.rTrim = function()
{
    return this.replace(/([\\s]*$)/g, "");
}

//弹出消息
function showAlert(message, returnUrl)
{        
    alert(message);    
    if (returnUrl && returnUrl != "")
         window.location.href = returnUrl;
}

function showPage(title, pageUrl, width, height, isModal)
{ 
    $(function(){ 
       
        if (pageUrl.indexOf('?') >0)
            tb_show(title, pageUrl + "&KeepThis=true&TB_iframe=true&width=" + width + "&height=" + height + "&modal=" + isModal, null);
        else
            tb_show(title, pageUrl + "?KeepThis=true&TB_iframe=true&width=" + width + "&height=" + height + "&modal=" + isModal, null);
    });
}

//全选/取消全选
function selectAll(involker)
{ 
    var inputElements = document.getElementsByTagName('input');

    for (var i = 0 ; i < inputElements.length ; i++)
    {
        var myElement = inputElements[i];
        
        if (myElement.type === "checkbox" && myElement.id.indexOf('chkThis') > 0)
            myElement.checked = involker.checked;

    }
}

//多选删除确认
function confirmPop(msg)
{
    var frm = document.forms[0];

    var j = 0;
    for (i=0; i<frm.length; i++)
    {
        if (frm.elements[i].name.indexOf('chkThis') > 0)
        {
            if(frm.elements[i].checked) 
            {
                j += 1;
                return confirm(msg);
            }
        }
    }
    
    if (j == 0)
    {
        //alert("您至少需要选择一条记录！");
        return false;
    }
}

function checkAll(name)
{
    var objs = document.getElementsByTagName("input");  
    for(var i=0; i<objs.length; i++)
    {  
        if(objs[i].type.toLowerCase() == "checkbox" )  
        {
            if (objs[i].name.indexOf(name) >= 0)
                objs[i].checked = true; 
        }
    }    
}

function uncheckAll(name)
{
    var objs = document.getElementsByTagName("input");  
    for(var i=0; i<objs.length; i++)
    {  
        if(objs[i].type.toLowerCase() == "checkbox" )  
        {
            if (objs[i].name.indexOf(name) >= 0)
                objs[i].checked = false; 
        }
    }          
}
//add by train
    function winClose()
    {
        $(function(){
           self.window.parent.window.document.getElementById("ctl00_ContentPlaceHolder1_btnSearch").click();
        self.window.close();
        });
    }
//

$(function() {
    //定时隐藏消息提示
    	window.setTimeout(function () {
                 $('#lblMsg').empty();
           }, 3000);

	window.setTimeout(function () {
                 $('#ctl00_ContentPlaceHolder1_lblMsg').empty();
           }, 3000);
           
    //首页toggle效果
    $(".collapse").toggle(function(){
		    $(this).addClass("expand"); 
		    }, function () {
		    $(this).removeClass("expand");
    });

    $(".collapse").click(function(event){
        $(this).parent().parent().parent().next(".dockbody").slideToggle("slow,");
   });
});

