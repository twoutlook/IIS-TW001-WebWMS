var dhxWins, w1,w2,w3;
var left = 10;
var top = 10;
function CascadePopupFloatWin(url,title, width, height) {
    dhxWins = new dhtmlXWindows();
    dhxWins.setImagePath("../../../codebase/imgs/");
    w3 = dhxWins.createWindow("w3", left, top, width, height);
    left += 10;
    top += 10;
    w1.setText(title);
    w1.attachURL(url);
    return false;
}

function PopupFloatWinMax(url,title,winID)
{
    dhxWins = new dhtmlXWindows();
    dhxWins.setImagePath("../../Layout/Popupwindow/imgs/");
    w1 = dhxWins.createWindow(winID, 0, 0, document.body.offsetWidth, document.body.offsetHeight);
    w1.setText(title);
    w1.attachURL(url);
    w1.maximize();
    var id = "SharkXu20110923" + winID;
    input = document.getElementById(id);
    if (input == null) {
        input = document.createElement("input");
        input.type = "hidden";
        input.id = id;
        input.value = winID;
        input.onclick = CloseFloatWin;
        document.body.appendChild(this.input);
    }
    return false;
}

var input;
function PopupFloatWin(url,title,winID,width, height) {
    dhxWins = new dhtmlXWindows();
    dhxWins.setImagePath("../../Layout/Popupwindow/imgs/");
    var mytop = ((document.body.offsetHeight - height) / 2)-100;
    var myleft = (document.body.offsetWidth - width) / 2;
    if (mytop < 0)
        mytop = 0;
    if (myleft < 0)
        myleft = 0;
    w1 = dhxWins.createWindow(winID, myleft, mytop, width, height);
    left += 10;
    top += 10;
    w1.setText(title);
    w1.attachURL(url);    
    var id = "SharkXu20110923" + winID;
    input = document.getElementById(id);
    if (input == null) {
        input = document.createElement("input");
        input.type = "hidden";
        input.id = id;
        input.value = winID;
        input.onclick = CloseFloatWin;
        document.body.appendChild(this.input);
    }
    return false;
}

function CloseMySelf(winID) {
    parent.document.all("SharkXu20110923" + winID).click();
}

function CloseFloatWin() {    
    if (window.event.srcElement!=null)
    {
        var myInput = window.event.srcElement;        
        dhxWins._closeWindow(dhxWins.wins[myInput.value]);
    }
}