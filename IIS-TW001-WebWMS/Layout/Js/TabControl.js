//查询事件
function Query() {
    try {
        ShowNo(true);
        SetSaveDisabled(true);
        return true;
    }
    catch (e) {
        alert(e.message);
        return false;
    }
}

//主表双击事件
function grdDblclick(grdName, rowIndex) {
    htabs('ctl00_ContentPlaceHolderMain_TabMain', 1);
    ShowNo(true);
    SetSelectedRow(grdName, rowIndex)
    SetSaveDisabled(false);
}

//给鼠标所在行设置新的前(背)景色
function SetNewColor(source) {
    _oldColor = source.style.backgroundColor;
    _oldForeColor = source.style.color;
    source.style.backgroundColor = '#DFDFDF';
    source.style.color = 'Black';
}

//鼠标离开后恢复该行原有的前(背)景色
function SetOldColor(source) {
    source.style.backgroundColor = _oldColor;
    source.style.color = _oldForeColor;
}

//设置某行被选中
function SetSelectedRow(source, rowIndex) {
    __doPostBack(source, rowIndex);
}

//子表双击事件
function childGrdDblclick(grdChild, rowIndex, pageName, id, btnID, width, height) {
    SetSelectedRow(grdChild, rowIndex);
    OpenWin(pageName + '?Action=Modify&ID=' + id + '&buttonId=' + btnID, 'ChildEdit', width, height);
}

//选中本grid中所有的选择项
function SelectSelfAll(obj) {
    var aReturn = obj.parentNode.parentNode.parentNode.getElementsByTagName("input");
    for (i = 0; i < aReturn.length; i++) {
        if (aReturn[i].type == "checkbox") {
            aReturn[i].checked = obj.checked;
        }
    }
}

//新增表头信息
function AddHead() {
    htabs('ctl00_ContentPlaceHolderMain_TabMain', 1);
    ShowNo(true);
}

//下载Grid数据
function DownExcel(btnName) {
    var btnExcel = document.getElementById(btnName);
    if (btnExcel != null && btnExcel != undefined)
        btnExcel.click();
}

//设置保存按钮可用性
function SetSaveDisabled(canDo) {
    var btnSave = document.getElementById("ctl00_ContentPlaceHolderMain_btnSave");
    if (btnSave != null && btnSave != undefined) {
        btnSave.disabled = canDo;
    }
}

//隐藏显示层
function ShowNo(isShow) {
    var divHoldOn = document.getElementById("ctl00_ContentPlaceHolderMain_divHoldon");
    if (divHoldOn != undefined && divHoldOn != undefined) {
        if (isShow) {
            divHoldOn.style.display = "block";
            divHoldOn.style.width = window.screen.availWidth - 20;
            var s_height = document.body.scrollHeight;
            divHoldOn.style.height = s_height + "px"; //设置高度
            divHoldOn.style.top = s_height / 2 + "px";
            divHoldOn.style.left = parseInt(divHoldOn.style.width) / 2 + "px";
        }
        else
            divHoldOn.style.display = "none";
    }
}