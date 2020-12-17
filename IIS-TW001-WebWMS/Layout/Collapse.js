var collapsed = false;
function GetCookie(name) {
    var result = null;
    var myCookie = document.cookie + ";";
    var searchName = name + "=";
    var startOfCookie = myCookie.indexOf(searchName);
    var endOfCookie;
    if (startOfCookie != -1) {
        startOfCookie += searchName.length;
        endOfCookie = myCookie.indexOf(";", startOfCookie);
        result = unescape(myCookie.substring(startOfCookie, endOfCookie));
    }
    return result;
}
//相对路径 relativePath，最后包含请包含/
function CollapseCondition(relativePath) {
    var css_name = GetCookie("CSS_Name");
    if (css_name == null)
        css_name="LG";
    if (collapsed) {
        document.getElementById("imgCollapse").src = relativePath + "Layout/Css/" + css_name + "/Images/Up.gif";
        document.getElementById("imgCollapse").alt = "折叠";
    }
    else {
        document.getElementById("imgCollapse").src = relativePath + "Layout/Css/" + css_name + "/Images/Down.gif";
        document.getElementById("imgCollapse").alt = "展开";
    }
    for (var j = 1; j < tabCondition.rows.length; j++) {
        if (collapsed)
            tabCondition.rows[j].style.display = "";
        else
            tabCondition.rows[j].style.display = "none";
    }
    collapsed = !collapsed;
}