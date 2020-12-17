<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeftNav.aspx.cs" Inherits="Layout_BaseLayout_LeftNav" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../content/nav/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css"  href="../../content/nav/leftnav.css" media="screen"/>
    <script src="../Js/jquery-2.1.1.min.js"></script>

    <script>
        function Goto(id, title, link) {
            parent.top.ifrContent.NewTabPage(id, title, link);
        }
    </script>
        <style>
        .submenu::-webkit-scrollbar {
  /*滚动条整体样式*/
  width : 6px;  /*高宽分别对应横竖滚动条的尺寸*/
  height: 1px;
  }
  .submenu::-webkit-scrollbar-thumb {
  /*滚动条里面小方块*/
  border-radius: 6px;
  box-shadow   : inset 0 0 5px rgba(0, 0, 0, 0.2);
  background   : #9C9C9C;
  }
  .submenu::-webkit-scrollbar-track {
  /*滚动条里面轨道*/
  box-shadow   : inset 0 0 5px rgba(0, 0, 0, 0.2);
  border-radius: 6px;
  background   : #ededed;
  }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <div style="width: 167px;height:600px; position: relative;overflow: hidden;" id="nav_container">
      <div style="overflow-x: hidden;overflow-y: scroll;position: absolute;">

        <div id="divScroll" class="account-l fl" style="width: 167px;height: 600px;">
            <ul id="accordion" class="accordion" >
            </ul>
        </div>

      </div>
    </div>

    <script>
        var menuIndex = 1001;

        //动态绑定菜单
        function GetLoadNav()
        {
            var data = JSON.parse(RightMenuList.authorizeMenu);
            var _html = "";
            $.each(data, function (i) {
                var row = data[i];
                if (row.F_ParentId == "0") {
                    _html += '<li>';
                    _html += '<div class="link" ><i class=""></i>' + row.F_Name + '<i class="fa fa-chevron-down"></i></div>';//' + row.F_Icon + '//TODO 图标待加工
                    var childNodes = row.ChildNodes;
                    if (childNodes.length > 0) {
                        //var j = 1;//小图标
                        _html += '<ul class="submenu"  >';
                        $.each(childNodes, function (i) {
                           
                            var subrow = childNodes[i];
                            menuIndex = menuIndex + 1;
                            //var menuId = subrow.F_Id.replace("/-/g","");
                            var menuUrl = "../../" + subrow.F_Data;
                            if (subrow.F_Data.substr(0, 1) == "/")
                            {
                                menuUrl = "../.." + subrow.F_Data;
                            }                           
                            _html += '<li>';
                            //var a = "menuItem itemIndexIco icoCss" + j; //小图标
                            //_html += "<a class='" + a + "' style='margin-top: 4px;' ></a>";//小图标margin-left: 8px;
                            _html += "<a class='menuItem' style='font-family: 宋体;' onclick=Goto('" + menuIndex + "','" + subrow.F_Name + "','" + menuUrl + "') href='#' mid='" + menuIndex + "'  > " + subrow.F_Name + "</a>";
                            _html += '</li>';
                            //j++;
                        });
                        _html += '</ul>';
                    }
                    _html += '</li>';
                }
            });

            $("#accordion").prepend(_html);

            var Accordion = function (el, multiple) {
                this.el = el || {};
                this.multiple = multiple || false;
                // Variables privadas
                var links = this.el.find('.link');
                // Evento
                links.on('click', { el: this.el, multiple: this.multiple }, this.dropdown)
            };

            Accordion.prototype.dropdown = function (e) {
                var $el = e.data.el;
                $this = $(this);
                $next = $this.next();//返回被选元素的后一个同级元素。
                $next.slideToggle();//在被选元素上进行 slideUp() 和 slideDown() 之间的切换。
                $this.parent().toggleClass('open');//如果不存在则添加类，如果已设置则删除之。这就是所谓的切换效果。
                if (!e.data.multiple) {
                    $el.find('.submenu').not($next).slideUp().parent().removeClass('open');
                }
            };

            var accordion = new Accordion($('#accordion'), false);
            $('.submenu li').click(function () {
               // $(this).addClass('current').siblings('li').removeClass('current');//siblings() 方法返回被选元素的所有同级元素。  小图标注释掉的
            });

            SetDivScroll();

        }

        window.setTimeout(GetLoadNav, 500);

        var timer = null;
        $(window).resize(function () {
            if (timer) {
                clearTimeout(timer);
            }
            timer = setTimeout(function () {
               SetDivScroll();
            }, 600)
        });

        function SetDivScroll()
        {
            //设置DIV可以滚动
            //var height = window.top.document.body.clientHeight;
            //var divscr = document.getElementById("divScroll");
            //if (height > 600) {
            //    divscr.style.height = "600px";
            //}
            //else {
            //    divscr.style.height = height+"px";
            //}

            var windowheight = window.top.document.body.clientHeight;
            var scrollHeight = windowheight - 71;
            if (scrollHeight < 0) {
                scrollHeight = 600;
            }
            $("#nav_container").css("max-height", scrollHeight + "px");
            $("#nav_container").css("height", scrollHeight + "px");
            $("#divScroll").css("height", scrollHeight + "px");
            var groupCount = $("#accordion").children('li').length;
            var submenuHeight = scrollHeight - groupCount * 36;
            $("#accordion .submenu").css("max-height", submenuHeight + "px");
            $("#accordion .submenu").css("overflow-y", "auto");
        }

    </script>

    </form>
</body>
</html>
