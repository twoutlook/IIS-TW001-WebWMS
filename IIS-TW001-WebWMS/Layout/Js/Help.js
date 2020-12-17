
/*
方法功能：显示提示信息后跳转到新页面
参数说明：
    alertMsg：显示的提示文本.
    toUrl:要跳转的url地址.
*/
function fun_AlertAndRedirectNewPage(alertMsg, toUrl) {
    alert(alertMsg);
    window.location.href = toUrl;
}

/*
功能说明：实现详情页面的返回按钮功能
参数说明：
    parentPageHfId：父页面隐藏控件的ID
    hfValue：要传给父页面隐藏控件的值
    parentPageBtnSearchId：父页面查询按钮的ID
*/
function fun_DetailsBack(parentPageHfId, hfValue, parentPageBtnSearchId,closeId) {
    //window.parent.document.getElementById(parentPageHfId).value = hfValue;
    window.parent.document.all[parentPageBtnSearchId].click();
    CloseMySelf(closeId); //INASSIT_D
    return false;
}

function validate_DataTime2(dateTime1FormId, dateTime1ToId, msg) {
    var dt1From = document.getElementById(dateTime1FormId).value;
    var dt1To = document.getElementById(dateTime1ToId).value;

    if (dt1From != "" && dt1To != "" && dt1From > dt1To) {
        alert(msg);
        return false;
    }

    return true;
}

function validate_DataTime(dateTime1FormId, dateTime1ToId, dateTime2FormId, dateTime2ToId,msg1,msg2) {
    var dt1From = document.getElementById(dateTime1FormId).value;
    var dt1To = document.getElementById(dateTime1ToId).value;
    var dt2From = document.getElementById(dateTime2FormId).value;
    var dt2To = document.getElementById(dateTime2ToId).value;
    if (dt1From != "" && dt1To != "" && dt1From > dt1To) {
        alert(msg1);
        return false;
    }
    if (dt2From != "" && dt2To != "" && dt2From > dt2To) {
        alert(msg2);
        return false;
    }
    return true;
}

function CheckDel(gvId) {
    //alert(gvId);
    var number = 0;
    var controls = document.getElementById(gvId).getElementsByTagName("input");

    for (var i = 0; i < controls.length; i++) {
        var e = controls[i];
        if (e.type != "CheckBox") {
            if (e.checked == true) {
                number = number + 1;
            }
        }
    }
    if (number == 0) {
        alert("请选择需要删除的项！");
        return false;
    }
    if (confirm("你确认删除吗？")) {
        return true;
    }
    else {
        return false;
    }
}


function checkLength(b, a, c) { return b.length >= a && b.length <= c ? !0 : !1 } function checkString(b) { for (var a = 0; a < b.length; ++a) { var c = b.charCodeAt(a); if (48 > c || 57 < c && 65 > c || 90 < c && 97 > c || 122 < c) return !1 } return !0 } function checkNumber(b) { for (var a = 0; a < b.length; ++a) { var c = b.charCodeAt(a); if (48 > c || 57 < c) return !1 } return !0 }

function CheckDate(b) {
    var a, c; if (0 < b.indexOf("-")) {
        c = b.substring(0, b.indexOf("-")); a = b.substring(b.indexOf("-") + 1, b.lastIndexOf("-")); "0" == a.substring(0, 1) && 1 < a.length && (a = a.substring(1, a.length)); b = b.substring(b.lastIndexOf("-") + 1, b.length); "0" == b.substring(0, 1) && 1 < b.length && (b = b.substring(1, b.length)); if (!1 == checkNumber(b) || !1 == checkNumber(a) || !1 == checkNumber(c) || 12 < a || 1 > a || 31 < b || 1 > b || 1900 > c || 2900 < c || 2 == a && (0 == c % 4 && 29 < b || 0 == c % 400 && 29 < b || 0 != c % 4 && 28 < b)) return !1; for (c = 1; 12 >= c; c += 2) {
            if (a == c && 31 < b) return !1;
            7 == c && --c
        } for (c = 4; 11 >= c; c += 2) { if (a == c && 30 < b) return !1; 6 == c && c++ } return !0
    } return !1
} function checkEmail(b) { var a, c, d = 0; for (a = 0; a < b.length; ++a) if (c = b.charCodeAt(a), 32 == c || 92 == c || 47 == c || 58 == c || 42 == c || 63 == c || 34 == c || 60 == c || 62 == c || 124 == c) return !1; for (a = 1; a < b.length - 1; ++a) c = b.charCodeAt(a), 64 == c && (d += 1); return 1 == d ? !0 : !1 }

function IsValidInputLength() {
    var b = window.event ? window.event : event; if (b.srcElement && b.srcElement.form) {
        for (var b = b.srcElement.form, a, c = 0; c <= b.elements.length - 1; c++) if (a = b.elements[c], !a.readOnly && !(a.disabled || "none" == a.style.display)) if ("INPUT" == a.tagName && "text" == a.type && a.maxLength && 2147483647 > a.maxLength && 0 < a.maxLength) { if (GetLengthByByte(a.value) > a.maxLength) return alert("\u60a8\u53ea\u80fd\u8f93\u5165 " + a.maxLength + " \u5b57\u7b26\uff01"), a.focus(), !1 } else if ("TEXTAREA" == a.tagName && a.title &&
        !isNaN(a.title) && GetLengthByByte(a.value) > a.title) return alert("\u60a8\u53ea\u80fd\u8f93\u5165 " + a.title + " \u5b57\u7b26!"), a.focus(), !1; return !0
    } alert("IsValidInputLength \u51fd\u6570\u53ea\u80fd\u7528\u5728\u8868\u5355\u63d0\u4ea4\u6309\u94ae\u7684ONCLICK\u4e8b\u4ef6\uff01"); return !1
} function GetLengthByByte(b) { for (var a = b.length, c = 0; c < b.length - 1; c++) 127 < b.charCodeAt(c) && (a += 1); return a } var ignoreIdList = "";

function KeyEnter(b) {
    var b = b || window.event, a = b.srcElement ? b.srcElement : b.target; if (13 != b.keyCode || "INPUT" == a.tagName && ("button" == a.type || "submit" == a.type || "reset" == a.type)) return !1; if ("INPUT" == a.tagName || "SELECT" == a.tagName) {
        for (var c = 0; a != a.form.elements[c];) if (c++, a.form.elements.length == c) return !1; var d = a.form.elements[c + 1]; for (j = c + 1; j < a.form.elements.length; j++) if (d = a.form.elements[j], d.readOnly || !0 == d.disabled || "disabled" == d.disabled || "none" == d.style.display) d = null; else if ("INPUT" == d.tagName &&
        "hidden" == d.type) d = null; else break; if (d) { if (!("INPUT" == a.tagName && "file" == a.type)) b.keyCode = 0; try { d.focus() } catch (e) { } "INPUT" == d.tagName && ("text" == d.type || "password" == d.type) && d.select() }
    } b && b.stopPropagation ? (b.stopPropagation(), b.preventDefault()) : b.cancelBubble = !0; return !1
}

function keyUpDown(b) {
    var b = b || window.event, a = b.srcElement ? b.srcElement : b.target, c = b.keyCode; if (40 != c && 38 != c || "INPUT" == a.tagName && ("button" == a.type || "submit" == a.type || "reset" == a.type)) return !1; if ("INPUT" == a.tagName) {
        for (var d = 0; a != a.form.elements[d];) if (d++, a.form.elements.length == d) return !1; var e = a.form.elements[d + 1], c = 40 == c ? 1 : -1; for (j = d + c; 1 == c ? j < a.form.elements.length : 0 <= j; j += c) if (e = a.form.elements[j], e.readOnly || !0 == e.disabled || "disabled" == e.disabled || "none" == e.style.display) e = null; else if ("INPUT" ==
        e.tagName && "hidden" == e.type) e = null; else break; if (e) { if (!("INPUT" == a.tagName && "file" == a.type)) b.keyCode = 0; try { e.focus() } catch (f) { } "INPUT" == e.tagName && ("text" == e.type || "password" == e.type) && e.select() }
    } return !1
}
function GetNameAtOnce(b, a, c) { var d = window.event ? window.event : event; if (a) d.srcElement.form && d.srcElement.form[a] ? d.srcElement.form[a].value = GetNameByCode(b, d.srcElement.value, !1, c) : d.srcElement.form ? alert("ToChangeName \u53c2\u6570\u9519\u8bef") : alert("GetNameAtonce \u8868\u5355\u8f93\u5165\u9879\u4e0a"); else if (d.srcElement.value) d.srcElement.value = GetNameByCode(b, d.srcElement.value, !0, c) }

function GetNameByCode(b, a, c, d) {
    var e; e = window.location.toString().lastIndexOf("/"); 0 <= e && window.location.toString().substring(0, e); e = new ActiveXObject("MSXML"); e.url = "../Sicsoft/CreateCodeXML.aspx?TName=" + b + "&CodeValue=" + a; b = e.root; if (null != b.children) {
        b = b.children.item(0); if ("" != b.GetAttribute("Name")) return window.status = "", c ? b.GetAttribute("Name") + "(" + b.GetAttribute("Code") + ")" : b.GetAttribute("Name"); if (c) return window.status = d ? "\u4ee3\u7801\u8f93\u5165\u4e0d\u6b63\u786e" : "", a; window.status = d ?
        "\u4ee3\u7801\u8f93\u5165\u4e0d\u6b63\u786e" : ""; return ""
    }
} function GetCode(b) { var a, c; a = b.lastIndexOf("("); c = b.length - 1; if (-1 == c || ")" != b.charAt(c)) c = -1; return c < a + 2 ? isNaN(b) ? "" : b : b.substr(a + 1, c - a - 1) } function funSelectAll(b) { var a = document.getElementsByTagName("input"); for (i = 0; i < a.length; i++) if ("checkbox" == a[i].type) a[i].checked = b.checked }

function SelectAll4Grid(b) { var a = b.id, c = document.getElementsByTagName("input"); for (i = 0; i < c.length; i++) { var d = c[i].id; if ("checkbox" == c[i].type && d.length > a.length && d.substr(d.length - a.length) == a ) c[i].checked = b.checked } }

function OpenPage(b, a, c, d) { b = window.open(b, a, "width=" + c + ",height=" + d + ",left=" + ((window.screen.width - c) / 2 - 5) + ",top=" + ((window.screen.height - d) / 2 - 14) + ",scrollbars=0,toolbar=0,status=1,menubar=0,location=0,directories=0"); if (null != document.window && !b.opener) b.opener = document.window; b.focus(); return !1 } function OpenRightWin(b, a, c, d) { window.open(b, a, "resizable = yes,scrollbars = yes,width = " + d + ",height = " + c + ",left = " + (screen.width - d - 15) + ",top = " + (screen.height - c - 60)).focus(); return !1 };


