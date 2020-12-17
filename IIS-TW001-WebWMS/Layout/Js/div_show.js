function disponse_div(e, obj) {
    var x;
    var y;
    if ((e.clientX + 400 + 000) >= screen.width) {
        x = e.clientX - 310;
        y = e.clientY + document.documentElement.scrollTop;
    }
    else {

        if (e.clientX + 400 > document.body.clientWidth) {
            //x = document.body.clientWidth - 400;
            //y = e.clientY + document.documentElement.scrollTop + 10;
            x = (document.body.clientWidth - 400) / 2
            y = (document.body.clientHeight - 200) / 2
            if (y < 0) {
                y = 50;
            }

        }
        else {
            x = e.clientX + document.documentElement.scrollLeft;
            y = e.clientY + document.documentElement.scrollTop + 10;
        }

    }


    obj.style.position = "absolute"
    obj.style.left = x + "px";
    obj.style.top = y + "px";
    obj.style.zIndex = 999;

    if (obj.style.display == "block") {
        obj.style.display = "none";
    }
    else {
        obj.style.display = "block";
    }
}