/*
设置IFrame自适应高度
iframeName: iframe名称
level: 层次
*/
function reinitIframe(iframe) {
    try {
        if (iframe != null && iframe != undefined) {
            var bHeight = iframe.contentWindow.document.body.scrollHeight;
            var dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
            var height = Math.max(bHeight, dHeight);
            if (iframe.src != '')
                iframe.height = height;
            else
                iframe.height = 0;
        }
    } catch (ex) { }
}


function ChangeTDStyle(inputTableID) {
    var tabMain = document.getElementById(inputTableID);
    if (tabMain == null) return;
    for (var i = 0; i < tabMain.rows.length; i++) {
        var tr = tabMain.rows[i];
        if (tr == null) continue;
        for (var j = 0; j < tr.cells.length; j++) {
            var td = tr.cells[j];
            if (td == null) continue;
            if (td.className == "" || td.className == null) {
                td.style.whiteSpace = "nowrap";
                td.style.borderRightWidth = "0px";
            }
        }
    }
}

/*
显示表头查询界面
*/
function ShowHeadQuery(IfPQuery, IfPEdit, IfChild, btnQuery) {
    var ifQuery = document.getElementById(IfPQuery);
    var ifEdit = document.getElementById(IfPEdit);
    var ifChild = document.getElementById(IfChild);
    SetSelectedTabBtn(document.getElementById(btnQuery));
    ifQuery.style.display = "block";
    ifEdit.style.display = "none";
    ifChild.style.display = "none";
}

/*
显示表头编辑界面及表体信息
*/
function ShowHeadEdit(IfPQuery, IfPEdit, IfChild, btnEdit, editPage) {
    var ifQuery = document.getElementById(IfPQuery);
    var ifEdit = document.getElementById(IfPEdit);
    var ifChild = document.getElementById(IfChild);
    SetSelectedTabBtn(document.getElementById(btnEdit));
    ifQuery.style.display = "none";
    if (ifEdit.src == '')
        ifEdit.src = editPage + ".aspx?Action=View&ID=";
    ifEdit.style.display = "block";
    if (ifChild.src == '')
        ifChild.style.display = "none";
    else
        ifChild.style.display = "block";
}

/*
主键内容
*/
var primaryKey = '';

/*
当前所选行索引
*/
var SelectIndex = 'Select$-1';

/*
设置某行被选中
source: 行
rowIndex: 选中列
pk: 主键内容(如多个以','阁开)
*/
function SetSelectedRow(source, rowIndex, pk) {
    if (rowIndex == '' || rowIndex == undefined || rowIndex == null) {
        if (window.parent.window.SelectIndex != undefined
         && window.parent.window.SelectIndex != 'Select$-1'
         && pk != '' && pk != null && pk != undefined) {
            rowIndex = window.parent.window.SelectIndex;
        }
        else {
            rowIndex = 'Select$-1';
        }
    }
    __doPostBack(source, rowIndex);
    window.parent.window.primaryKey = pk;
    window.parent.window.SelectIndex = rowIndex;
}

/*
查询界面根据不同条件显示编辑界面
pk: 主键内容(如多个以','阁开)
iframe: iframe名称
showPage: 需要显示的页面的名称
obj: 编辑tab的按钮
*/
function ShowEdit(pk, iframe, showPage, source) {
    var obj = window.parent.document.getElementById('btnEdit');
    var iframe = window.parent.document.getElementById(iframe);
    var Iframes = window.parent.document.getElementsByTagName('iframe');
    if (iframe != null && iframe != undefined) {
        for (var i = 0; i < Iframes.length; i++) {
            if (Iframes[i] != null && Iframes[i] != undefined) {
                if (Iframes[i].id != iframe.id)
                    Iframes[i].style.display = "none";
                else
                    Iframes[i].style.display = "block";
            }
        }
        if (pk == "") {
            //新增
            iframe.src = showPage + "?Action=New&ID=" + getRandomNum();
            SetSelectedRow(source, 'Select$-1', '')
        }
        else {
            //修改,查看
            iframe.src = showPage + "?Action=Modify&ID=" + pk + getRandomNum();
        }
    }
    SetSelectedTabBtn(obj);
}

/*
用于切换子表列表页面地址
childPath: 子表列表页面地址
editIf: 存放子表列表页面地址的iframe
childIf: 存放子表编辑的iframe
Trbtn: 存放标签和保存按钮的tr
Trhead: 存放主表编辑信息tr
obj: 点击的tab按钮
*/
function showChild(childPath, editIf, childIf, Trbtn, Trhead, obj) {
    var realCPath = childPath + '?Action=View&ID=' + getQueryStringByName('ID');
    var ifEdit = document.getElementById(editIf);
    var IfChild = window.parent.window.document.getElementById(childIf);
    var btnTr = document.getElementById(Trbtn);
    var headTr = document.getElementById(Trhead);
    ShowPage(realCPath, ifEdit, IfChild, btnTr, headTr);
    SetSelectedTabBtn(obj);
    var btnSave = null;     //表体编辑信息的保存按钮
    var titleTd = null;     //(可触发该动作的)标签所在td
    var btnTd = null;       //包含表体编辑信息的保存按钮的td
    var trHeadBtn = document.getElementById(Trbtn);
    var hideTr = document.getElementById(Trhead);
    if (trHeadBtn != null && trHeadBtn != undefined) {
        var Tds = trHeadBtn.getElementsByTagName("td");
        if (Tds.length > 1) {
            for (var i = 0; i < Tds.length; i++) {
                if (Tds[i].innerHTML.indexOf('showHideHead(') > -1 && titleTd == null)
                    titleTd = Tds[i];
                if (Tds[i].innerHTML.indexOf('ButtonSave') > -1 && btnTd == null) {
                    btnTd = Tds[i];
                    var inputs = btnTd.getElementsByTagName("input");
                    if (inputs.length > 0) {
                        for (var j = 0; j < inputs.length; j++) {
                            if (inputs[j].className == 'ButtonSave') {
                                btnSave = inputs[j];
                                j = inputs.length;
                            }
                        }
                    }
                }
            }
        }
    }
    hideTr.style.display = 'block';
    if (titleTd != null && titleTd != undefined && titleTd.className == 'LeftTdbyLabelFrame')
        titleTd.className = 'LeftTdbyLabelHalfFrame';
    if (btnTd != null && btnTd != undefined && btnTd.className == 'RightTdBtnFrame')
        btnTd.className = 'RightTdBtnHalfFrame';
    if (btnSave != null && btnSave != undefined)
        btnSave.disabled = false;
}

/*
新增子表信息(显示子表详细信息编辑界面，隐藏主表编辑界面)
headEitBtnTr：表头标题所在tr
headEditTr：表头所在tr
IfChild：表体详细信息所在iframe
cEditPath：表体详细信息所在页面
*/
function addChild(headEitBtnTr, headEditTr, IfChild, cEditPath) {
    var iframes = window.parent.document.getElementsByTagName('IFRAME');
    if (iframes != null && iframes != undefined && iframes.length > 0) {
        for (var i = 0; i < iframes.length; i++) {
            if (iframes[i].id == 'IfEdit') {
                var oldSrc = iframes[i].src;
                iframes[i].src = ChangeRandom(oldSrc);
                i = iframes.length;
            }
        }
    }
    var trHeadBtn = window.parent.document.getElementById(headEitBtnTr);
    var trHead = window.parent.document.getElementById(headEditTr);
    var ifmChild = window.parent.window.parent.document.getElementById(IfChild);
    ifmChild.style.display = "block";
    var parentPK = getQueryStringByName('ID');
    var showPage = cEditPath + '?Action=New&ID=' + parentPK + getRandomNum();
    showHeadOrChild(false, trHeadBtn, trHead, ifmChild, showPage);
}

/*
编辑子表信息(显示子表详细信息编辑界面，隐藏主表编辑界面)
headEitBtnTr：表头标题所在tr
headEditTr：表头所在tr
IfChild：表体详细信息所在iframe
cEditPath：表体详细信息所在页面
primaryKey：主键信息
*/
function ModifyChild(headEitBtnTr, headEditTr, IfChild, cEditPath, primaryKey) {
    var trHeadBtn = window.parent.document.getElementById(headEitBtnTr);
    var trHead = window.parent.document.getElementById(headEditTr);
    var ifmChild = window.parent.window.parent.document.getElementById(IfChild);
    if (ifmChild.style.display != "block")
        ifmChild.style.display = "block";
    var showPage = cEditPath + '?Action=Modify&ID=' + primaryKey + getRandomNum();
    showHeadOrChild(false, trHeadBtn, trHead, ifmChild, showPage);
}

/*
刷新主表查询界面(在主表信息修改或新增后做该动作)
*/
function RefreshHeadQuery(headQuery) {
    var ifQuery = document.parentWindow.parent.document.getElementById('IfPQuery');
    if (ifQuery != null && ifQuery != undefined) {
        ifQuery.src = headQuery + ".aspx?random=" + Math.random();
    }
}








/*
在IFrame中显示页面
pageName: 需要显示的页面的名称
mainIf: iframe名称
childIf: 显示子窗体的iframe名称
btnTr: 存放表头编辑标签的行
headTr: 存放表头编辑信息的行
*/
function ShowPage(pageName, mainIf, childIf, btnTr, headTr) {
    if (mainIf != null && mainIf != undefined) {
        if (mainIf.src != pageName)
            mainIf.src = pageName + primaryKey + getRandomNum();
    }
    showHeadOrChild(true, btnTr, headTr, childIf, '')
}

/*
用于切换主表的查询和编辑界面
headPath：主表页面地址
mainIf：存放主表的iframe
childIf：存放子表编辑的iframe
obj: 点击的tab按钮
*/
function showHead(headPath, mainIf, childIf, obj) {
    var ifMain = document.getElementById(mainIf);
    var ifChild = document.getElementById(childIf);
    ShowPage(headPath, ifMain, ifChild, null, null);
    SetSelectedTabBtn(obj);
}

/*
设置选中tab的class
obj: 点击的tab按钮
*/
function SetSelectedTabBtn(obj) {
    if (obj != null && obj != undefined) {
        if (obj.className = 'ButtonTab') {
            obj.className = 'ButtonSelectedTab';
            var otherBtns = obj.parentNode.getElementsByTagName("input");
            if (otherBtns != null && otherBtns != undefined && otherBtns.length > 0) {
                for (var i = 0; i < otherBtns.length; i++) {
                    if (otherBtns[i].id != obj.id && otherBtns[i].className == 'ButtonSelectedTab') {
                        otherBtns[i].className = 'ButtonTab';
                    }
                }
            }
        }
    }
}





/*
当显示主表编辑界面时默认显示的子表列表界面
ifEdit: 用于显示子表列表界面的iframe
showPage: 需要显示的子表列表界面
obj: 点击的tab按钮
*/
function showDefaultPage(ifEdit, showPage, obj) {
    var IfEdit = document.getElementById(ifEdit);
    if (IfEdit != null && IfEdit != undefined) {
        IfEdit.src = showPage + "?ID=" + getQueryStringByName('ID') + getRandomNum();
    }
    SetSelectedTabBtn(obj);
}



/*
给鼠标所在行设置新的前(背)景色
source: 行
*/
function SetNewColor(source) {
    _oldColor = source.style.backgroundColor;
    _oldForeColor = source.style.color;
    source.style.backgroundColor = '#DFDFDF';
    source.style.color = 'Black';
}

/*
鼠标离开后恢复该行原有的前(背)景色
source: 行
*/
function SetOldColor(source) {
    source.style.backgroundColor = _oldColor;
    source.style.color = _oldForeColor;
}

/*
下载Grid数据
btnName: 按钮名称
*/
function DownExcel(btnName) {
    var btnExcel = document.getElementById(btnName);
    if (btnExcel != null && btnExcel != undefined)
        btnExcel.click();
}

/*
选中本grid中所有的选择项
obj: 选择框自身
*/
function SelectSelfAll(obj) {
    var aReturn = obj.parentNode.parentNode.parentNode.getElementsByTagName("input");
    for (i = 0; i < aReturn.length; i++) {
        if (aReturn[i].type == "checkbox") {
            aReturn[i].checked = obj.checked;
        }
    }
}

/*
设置窗体宽度与iframe自适应
tbName: table名称
function SetTableWidth(tbName) {
var tb = document.getElementById(tbName);
try {
if (tb != null && tb != null)
tb.style.width = window.parent.document.body.offsetWidth;
} catch (ex) { }
}
*/

/*
显示表头或表体详细信息
两者互斥，显示表头则隐藏表体，反之亦然
hc: true:显示表头信息;false:显示表体信息
trHeadBtn: 表头标题所在tr
trHead: 表头所在tr
ifmChild :表体详细信息所在iframe
showPage : 表体详细信息所在页面
*/
function showHeadOrChild(hc, trHeadBtn, trHead, ifmChild, showPage) {
    var btnSave = null;     //表体编辑信息的保存按钮
    var titleTd = null;     //(可触发该动作的)标签所在td
    var btnTd = null;       //包含表体编辑信息的保存按钮的td
    if (trHeadBtn != null && trHeadBtn != undefined) {
        var Tds = trHeadBtn.getElementsByTagName("td");
        if (Tds.length > 1) {
            for (var i = 0; i < Tds.length; i++) {
                if (Tds[i].innerHTML.indexOf('showHideHead(') > -1 && titleTd == null)
                    titleTd = Tds[i];
                if (Tds[i].innerHTML.indexOf('ButtonSave') > -1 && btnTd == null) {
                    btnTd = Tds[i];
                    var inputs = btnTd.getElementsByTagName("input");
                    if (inputs.length > 0) {
                        for (var j = 0; j < inputs.length; j++) {
                            if (inputs[j].className == 'ButtonSave') {
                                btnSave = inputs[j];
                                j = inputs.length;
                            }
                        }
                    }
                }
            }
        }
    }
    if (hc) {
        //        if (trHeadBtn != null && trHeadBtn != undefined)
        //            trHeadBtn.style.display = 'block';
        //        if (trHead != null && trHead != undefined)
        //            trHead.style.display = 'block';
        ShrinkHead(trHead, btnSave, titleTd, btnTd, false);
        if (ifmChild != null && ifmChild != undefined)
            ifmChild.src = '';
    }
    else {
        //        if (trHeadBtn != null && trHeadBtn != undefined)
        //            trHeadBtn.style.display = 'none';
        //        if (trHead != null && trHead != undefined)
        //            trHead.style.display = 'none';
        ShrinkHead(trHead, btnSave, titleTd, btnTd, true);
        if (ifmChild != null && ifmChild != undefined)
            ifmChild.src = showPage;
    }
}

/*
根据QueryString参数名称获取值
*/
function getQueryStringByName(name) {
    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
    if (result == null || result.length < 1) {
        return "";
    }
    return result[1];
}

/*
编辑完子表信息后关闭自身并显示主表编辑信息同时刷新表体列表信息
ifMain: 存放主界面信息的iframe名称
trTitle: 主表标题所在行名称
trHead: 主表编辑信息所在行名称
IfEdit: 存放子表列表信息的iframe名称
*/
function BackToChildList(ifMain, trTitle, trHead, IfEdit) {
    var parentIfrme = window.frameElement;
    if (parentIfrme != null && parentIfrme != undefined && 'IFRAME' == parentIfrme.tagName) {
        parentIfrme.src = '';
    }
    var ifMain = window.parent.document.getElementById(ifMain);
    if (ifMain != null && ifMain != undefined && 'IFRAME' == ifMain.tagName) {
        if (ifMain.src != '') {
            //显示表头编辑信息
            var trHead = ifMain.contentWindow.document.getElementById(trHead);
            if (trHead != null && trHead != undefined) {
                var trTitle = ifMain.contentWindow.document.getElementById(trTitle);
                var btnSave = null;     //表体编辑信息的保存按钮
                var titleTd = null;     //(可触发该动作的)标签所在td
                var btnTd = null;       //包含表体编辑信息的保存按钮的td
                if (trTitle != null && trTitle != undefined) {
                    //                trTitle.style.display = 'block';
                    var Tds = trTitle.getElementsByTagName("td");
                    if (Tds.length > 1) {
                        for (var i = 0; i < Tds.length; i++) {
                            if (Tds[i].innerHTML.indexOf('showHideHead(') > -1 && titleTd == null)
                                titleTd = Tds[i];
                            if (Tds[i].innerHTML.indexOf('ButtonSave') > -1 && btnTd == null) {
                                btnTd = Tds[i];
                                var inputs = btnTd.getElementsByTagName("input");
                                if (inputs.length > 0) {
                                    for (var j = 0; j < inputs.length; j++) {
                                        if (inputs[j].className == 'ButtonSave') {
                                            btnSave = inputs[j];
                                            j = inputs.length;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                ShrinkHead(trHead, btnSave, titleTd, btnTd, false);
            }

            //刷新子表列表
            var ifChild = ifMain.contentWindow.document.getElementById(IfEdit);
            if (ifChild != null && ifChild != undefined) {
                ifChild.src = ChangeRandom(ifChild.src);
            }
        }
    }
}

/*
将原有随机数转换为新的，如不存在则直接添加在后面
*/
function ChangeRandom(oldPath) {
    if (oldPath.indexOf('&Random=') > -1) {
        var result = oldPath.match(new RegExp("[\?\&]Random=([^\&]+)", "i"));
        if (result == null || result.length < 1) {
            return "";
        }
        var oldValue = result[1];
        return oldPath.replace(oldValue, getRandomNum().replace('&Random=', ''));
    }
    else {
        return oldPath + getRandomNum();
    }
}

/*
生成随机数
*/
function getRandomNum() {
    return "&Random=" + Math.random();
}

/*
隐藏/显示表头编辑信息
hideTr：包含表体编辑信息的tr
btnSave：表体编辑信息的保存按钮
titleTd：(可触发该动作的)标签所在td
btnTd: 包含表体编辑信息的保存按钮的td
isHide: 是否隐藏
*/
function ShrinkHead(hideTr, btnSave, titleTd, btnTd, isHide) {
    if (hideTr != null && hideTr != undefined) {
        if (hideTr.style.display == '' || hideTr.style.display == 'block' || isHide) {
            hideTr.style.display = 'none';
            if (titleTd != null && titleTd != undefined && titleTd.className == 'LeftTdbyLabelHalfFrame')
                titleTd.className = 'LeftTdbyLabelFrame';
            if (btnTd != null && btnTd != undefined && btnTd.className == 'RightTdBtnHalfFrame')
                btnTd.className = 'RightTdBtnFrame';
            if (btnSave != null && btnSave != undefined)
                btnSave.disabled = true;
        }
        else {
            hideTr.style.display = 'block';
            if (titleTd != null && titleTd != undefined && titleTd.className == 'LeftTdbyLabelFrame')
                titleTd.className = 'LeftTdbyLabelHalfFrame';
            if (btnTd != null && btnTd != undefined && btnTd.className == 'RightTdBtnFrame')
                btnTd.className = 'RightTdBtnHalfFrame';
            if (btnSave != null && btnSave != undefined)
                btnSave.disabled = false;
        }
    }
}

/*
标题直接触发的表头信息收缩展开事件
hide：包含表体编辑信息的tr
btn：表体编辑信息的保存按钮
titleTd：(可触发该动作的)标签所在td
btnTd: 包含表体编辑信息的保存按钮的td
*/
function showHideHead(hide, btn, titleTd, btnTd) {
    var hideTr = document.getElementById(hide);
    var btnSave = document.getElementById(btn);
    var HTitleTd = document.getElementById(titleTd);
    var HTBtnTd = document.getElementById(btnTd);
    ShrinkHead(hideTr, btnSave, HTitleTd, HTBtnTd, false);
}

/*
显示自适应的js时间控件
以table为容器判断位置后自动定位显示
*/
function ShowSuitCalendar(id, format) {
    var obj = document.getElementById(id);
    var html = getOuterHtml(obj);
    var outLeft = 0;    //如外部存在iframe则该iframe相对于其父窗体的横坐标
    var outTop = 0;     //如外部存在iframe则该iframe相对于其父窗体的纵坐标
    var parentHtml;
    if (html != null && html != undefined) {
        var od = html.ownerDocument;
        if (od != undefined && od != null) {
            try {
                var url = od.location.href;
                parentHtml = od.parentWindow.parent;
                if (parentHtml != null && parentHtml != undefined) {
                    var currIframe = GetCurrentIfrme(parentHtml, url);
                    if (currIframe != null && currIframe != undefined) {
                        outLeft = currIframe.offsetLeft
                        outTop = currIframe.offsetTop;
                        while (currIframe != null && currIframe != undefined) {
                            currIframe = currIframe.offsetParent
                            outLeft += currIframe.offsetLeft;
                            outTop += currIframe.offsetTop;
                            if (currIframe.tagName == 'HTML')
                                currIframe = null;
                        }
                    }
                }
            }
            catch (e) {
                outLeft = 0;
                outTop = 0;
            }
        }
    }
    if (obj != null && obj != undefined) {
        var allHeight = 0;      //正文全高
        var clientHeight = 0;   //正文可见部分全高
        var allWidth = 0;       //正文全宽
        var clientWidth = 0;    //正文可见部分全宽
        var scrollTop = 0;      //正文上部隐藏部分高(即滚动后不可见部分)
        var scrollLeft = 0;
        var docElement = document.documentElement;
        if (docElement != null && docElement != undefined) {
            var allHeight = docElement.scrollHeight;
            var clientHeight = docElement.clientHeight;
            var allWidth = docElement.scrollWidth;
            var clientWidth = docElement.clientWidth;
            var scrollTop = docElement.scrollTop;
            var scrollLeft = docElement.scrollLeft;
        }
        if (parentHtml != null && parentHtml != null) {
            docElement = parentHtml.document.documentElement;
            if (docElement != null && docElement != undefined) {
                allHeight = docElement.scrollHeight;
                clientHeight = docElement.clientHeight;
                allWidth = docElement.scrollWidth;
                clientWidth = docElement.clientWidth;
                scrollTop = docElement.scrollTop;
                scrollLeft = docElement.scrollLeft;
            }
        }
        var minTop = scrollTop;                    //可见部分最顶侧坐标
        var maxRight = scrollLeft + clientWidth;   //可见部分最右侧坐标
        var maxBottom = scrollTop + clientHeight;  //可见部分最下侧坐标
        var minLeft = scrollLeft;                  //可见部分最左侧坐标

        var calHeight = 166; //180、250               //时间控件的高
        var calWidth = 244;                           //时间控件的宽
        var objWidth = obj.scrollWidth;               //input的宽
        var objHeight = obj.scrollHeight;             //input的高
        //时间控件(Input)的相对坐标
        var x = obj.offsetLeft, y = obj.offsetTop;
        while (obj != null && obj != undefined) {
            x += obj.offsetLeft;
            y += obj.offsetTop;
            if (obj.tagName == 'HTML')
                obj = null;
            else
                obj = obj.offsetParent;
        }
        var realLeft = x + outLeft - 1;              //时间空间原横坐标(左侧)
        var realRight = realLeft + calWidth;         //时间空间原横坐标(右侧)
        var realTop = y + calHeight + outTop - 1;    //时间控件原纵坐标(顶部)
        var realBottom = realTop + calHeight;        //时间控件原纵坐标(底部)

        var xMove = 0;  //横向移动量(正即左移，反之右移)
        var yMove = 0;  //纵向移动量(正即上移，反之下移)
        if (realLeft < minLeft) {
            xMove = 0 - minLeft; //向右移动
            //(时间空间左侧超出边界)因目前不使用横向滚动条故此情况不会发生
            if (realRight <= maxRight) {
                if (realTop < minTop) {
                    //向下移动
                    yMove = realTop - minTop;
                }
                else if (realBottom > maxBottom) {
                    if (calHeight > y) {
                        //向上移动
                        yMove = y + objHeight;
                    }
                    else {
                        if (realTop > calHeight + objHeight + 2 + scrollTop) {
                            //向上移动
                            yMove = calHeight + objHeight + 2;
                        }
                        else {
                            //向上移动
                            yMove = realTop - scrollTop;
                        }
                    }
                }
                else {
                }
            }
            else {
                if (realTop < minTop) {
                    //向下移动
                    yMove = realTop - minTop;
                }
                else if (realBottom > maxBottom) {
                    if (calHeight > y) {
                        //向上，向右移动
                        xMove = 0 - minLeft;
                        yMove = y + objHeight;
                    }
                    else {
                        if (realTop > calHeight + objHeight + 2 + scrollTop) {
                            //向上，向右移动
                            yMove = calHeight + objHeight + 2;
                        }
                        else {
                            //向上，向右移动
                            yMove = realTop - scrollTop;
                        }
                    }
                }
                else {
                }
            }
            showCalendarXY(id, format, xMove, yMove);
        }
        else {
            if (realRight <= maxRight) {
                //此时横向正常(判断纵向)
                if (realTop < minTop) {
                    //向下移动  
                    yMove = realTop - minTop;
                }
                else if (realBottom > maxBottom) {
                    var xMove = calWidth - 2;
                    if (x < xMove) {
                        xMove = x - 3;
                    }
                    if (calHeight > y) {
                        if (realTop - scrollTop > calHeight + objHeight + 2) {
                            //此时上访高度不够(相对于内部iframe)上移
                            yMove = y + objHeight - 1;
                        }
                        else {
                            //此时上访高度不够(内外均不够，取小的值移动)
                            yMove = Math.min(y + objHeight - 1, realTop - scrollTop);
                        }
                    }
                    else {
                        if (realTop - scrollTop > calHeight + objHeight + 2) {
                            //正常上移
                            xMove = 0;
                            yMove = calHeight + objHeight + 2;
                        }
                        else {
                            //此时上部高度不够(相对于外部iframe的滚动可见区域)上移
                            yMove = realTop - scrollTop;
                        }
                    }
                }
            }
            else {
                //此时横向右侧超出边界(判断纵向)
                if (realTop < minTop) {
                    //向左、向下移动
                    xMove = realRight - maxRight;
                    yMove = realTop - minTop;
                }
                else if (realBottom > maxBottom) {
                    var xMove = calWidth - 2;
                    if (x < xMove) {
                        xMove = x - 3;
                    }
                    if (maxRight - x < 198)
                        calHeight = 250;
                    else if (maxRight - x < 244)
                        calHeight = 180;
                    else
                        calHeight = 166;
                    if (calHeight > y) {
                        if (realTop - scrollTop > calHeight + objHeight + 2) {
                            //向上，向左移动
                            yMove = y + objHeight - 1;
                        }
                        else {
                            //向上，向左移动
                            yMove = Math.min(y + objHeight - 1, realTop - scrollTop);
                        }
                    }
                    else {
                        if (realTop - scrollTop > calHeight + objHeight + 2) {
                            //向上，向右移动
                            xMove = calWidth - objWidth;
                            yMove = calHeight + objHeight + 2;
                        }
                        else {
                            //向上，向左移动
                            yMove = realTop - scrollTop;
                        }
                    }
                }
                else {
                    //向左移动
                    xMove = realRight - maxRight;
                }
            }
            showCalendarXY(id, format, xMove, yMove);
        }
    }
}

/*
循环查询包含时间控件的HTML
*/
function getOuterHtml(obj) {
    if (obj.parentElement != null && obj.parentElement != undefined) {
        if (obj.parentElement.tagName == 'HTML') {
            return obj.parentElement;
        }
        else
            return getOuterHtml(obj.parentElement);
    }
    else {
        return null;
    }
}

/*
获取当前页面所在iframe
parent: 父页
url: iframe中src的值
*/
function GetCurrentIfrme(parent, url) {
    var urls = String(url).split('/');
    var realUrl = urls[urls.length - 1];
    if (realUrl != null && realUrl != undefined) {
        for (var i = 0; i < parent.window.document.all.length; i++) {
            if (parent.window.document.all[i].tagName == 'IFRAME') {
                if (parent.window.document.all[i].src.indexOf(realUrl) > -1) {
                    return parent.window.document.all[i];
                }
            }
        }
    }
    return null;
}

/*
显示自适应的js时间控件(相对于table)
以table为容器判断位置后自动定位显示
*/
function ShowSuitCalendarByTable(id, format) {
    var obj = document.getElementById(id);
    if (obj != null && obj != undefined) {
        var table = getOuterTable(obj);
        if (table == null) {
            showCalendar(id, format, 0);
        }
        else {
            var allHeight = table.clientHeight; //TABLE全高(可见部分)
            var allWidth = table.clientWidth;   //TABLE全宽(可见部分)
            var calHeight = 180;                //时间控件的高
            var calWidth = 244;                 //时间控件的宽
            var objWidth = obj.scrollWidth;     //input的宽
            var objHeight = obj.scrollHeight;   //input的高

            /*//时间控件(Input)的绝对坐标
            var x, y;
            oRect = obj.getBoundingClientRect();
            x = oRect.left;
            y = oRect.top;*/

            //时间控件(Input)的相对坐标(相对于table)
            var xt = obj.offsetLeft, yt = obj.offsetTop;
            while (obj != null && obj != undefined) {
                obj = obj.offsetParent
                xt += obj.offsetLeft;
                yt += obj.offsetTop;
                if (obj.tagName == 'TABLE')
                    obj = null;
            }
            if (xt + calWidth <= allWidth) {
                //此时宽度不超过table范围, x坐标无需调整
                if (yt + calHeight + objHeight <= allHeight) {
                    //此时高度不超过table范围, y坐标无需调整
                    showCalendarXY(id, format, 0, 0);
                }
                else {
                    //此时高度超过table范围, y坐标也需要调整
                    var rX = calWidth - objWidth - 3;
                    var rY = calHeight + 4;
                    if (rX > xt) {
                        //此时时间控件超出左边界
                        rX = -1;
                    }
                    showCalendarXY(id, format, rX, rY);
                }
            }
            else {
                //此时宽度超过table范围，x坐标需要调整
                var rX = calWidth - objWidth - 3;
                if (yt + calHeight + objHeight <= allHeight) {
                    //此时高度不超过table范围, y坐标无需调整
                    showCalendarXY(id, format, rX, 0);
                }
                else {
                    //此时高度超过table范围, y坐标也需要调整
                    var rY = calHeight + 4;
                    showCalendarXY(id, format, rX, rY);
                }
            }
        }
    }
}

/*
循环查询包含时间控件的TABLE
*/
function getOuterTable(obj) {
    if (obj.parentElement != null && obj.parentElement != undefined) {
        if (obj.parentElement.tagName == 'TABLE')
            return obj.parentElement;
        else
            return getOuterTable(obj.parentElement);
    }
    else {
        return null;
    }
}

/*
新增表体时获取操作状态
*/
function getActionString() {
    var ifmChildEdit = document.parentWindow.parent.parent.document.getElementById("IfChild");
    if (ifmChildEdit != null && ifmChildEdit != undefined) {
        var pathstr = ifmChildEdit.src;
        if (pathstr.length > 0) {
            var results = pathstr.split('?');
            if (results != null && results.length > 1) {
                var result = results[1].split('&');
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].indexOf('Action') > -1) {
                            var realResult = result[i].replace('Action', '').replace('=', '');
                            return realResult;
                        }
                    }
                }
            }
        }
    }
    return "";
}

/*
刷新表体列表信息
*/
function refreshChildList(ifChild, grdList) {
    var ifm = document.parentWindow.parent.parent.document.getElementById(ifChild);
    var grd = document.getElementById(grdList);
    if (grd != null && grd != undefined) {
        if (grd.rows.length > 1) {
            /*当存在数据时继续*/
            for (var i = 1; i < grd.rows.length; i++) {
                if (grd.rows[i].style.fontWeight == 'bold')
                    return;
                /*当存在已选择行时返回*/
            }
        }
    }
    var act = getActionString();
    if (ifm != null && ifm != undefined && act != "New")
        ifm.src = "";
}