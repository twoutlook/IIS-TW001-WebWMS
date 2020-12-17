

//contorlID:需要滚动的控件ID(客户端ID)，setting:设定参数{HolderHeight:200,HolderWidth:100,CtrlWidth}
function settingPad(ControlID, settings) {

    var sUserAgent = navigator.userAgent.toLowerCase();
    var bIsIpad = sUserAgent.match(/ipad/i) == "ipad";
    var bIsAndroid = sUserAgent.match(/android/i) == "android";

    var bIsPad = bIsIpad || bIsAndroid;
    //alert(sUserAgent);
    if (!bIsPad) { return; }
    
    var DivScroll = null;
    var ctr= document.getElementById(ControlID);
    if (ctr) {
        DivScroll = document.createElement("div"); //.insertBefore(ctr)
        ctr.parentNode.appendChild(DivScroll);
        DivScroll.insertBefore(ctr);
        if (arguments.length == 1) {
            DivScroll.style.height = "180px";
            DivScroll.style.width = "800px";
            ctr.style.width = ctr.style.clientWidth; //"2000px";
        }
        else{
        
            DivScroll.style.height = settings.HolderHeight + "px";
            DivScroll.style.width = settings.HolderWidth + "px";

            ctr.style.width = settings.CtrlWidth + "px";
        }
    }
    
    var myScroll;
    function loaded() {
        myScroll = new iScroll(DivScroll);

    }

    document.addEventListener('touchmove', function(e) { e.preventDefault(); }, false);

    document.addEventListener('DOMContentLoaded', loaded, false);


}
