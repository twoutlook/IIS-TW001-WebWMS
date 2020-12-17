
//获取Cooks
function getCookies(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
}

//设置Cooks
function setCookies(name, value) {
    var Days = 1; //此 cookie 将被保存 30 天
    var exp = new Date(); //new Date("December 31, 9998");
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    delCookies(name);
    document.cookie = name + "=" + escape(value) + ";expire=" + exp.toGMTString() + ";path=/";

}

//删除Cooks
function delCookies(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookies(name);
    if (cval != null) document.cookie = name + "=" + cval + ";expire=" + exp.toGMTString() + ";path=/";
}
