<%@ Page Language="C#" CodeFile="Login.aspx.cs" Inherits="Login" AutoEventWireup="true" EnableEventValidation="false" %>

<%@ Register Src="ShowNotcieDetail.ascx" TagName="ShowNotcieDetail" TagPrefix="uc1" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head runat="server">
    <title>WebWMS <%=Resources.Lang.Login_MSG1 %><%--仓库管理系统--%></title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <link href="../CSS/LG/login.css" rel="stylesheet" type="text/css" runat="server" id="cssUrl" />
    <link href="../../Layout/multi/css/cn/backImage.css" rel="Stylesheet" type="text/css" runat="server" id="multiUrl" />

    <script type="text/javascript">
        var flag = 0;
        if (window.top != null && window.top != self) {
            window.top.location.href = window.location.href;
        }
        function showFloatEx() {
            var objLog = document.getElementById("divQualm");
            objLog.style.display = "block";
            flag = 1;
        }
        function ShowNoFlash(divName) {
            document.getElementById(divName).style.display = "none";
        }
        function CloseFrush(divName) {
            ShowNoFlash(divName);
            flag = 0;
            SetFocus($get('txtUserName'));
        }
        function Navigater(value) {
            if (value != "")
                window.open(top.href = value);
        }
        //根据屏幕的大小显示两个层
        function showFloat(divName) {
            var W = screen.width; //取得屏幕分辨率宽度
            var H = screen.height; //取得屏幕分辨率高度
            var yScroll; //取滚动条高度
            if (self.pageYOffset) {
                yScroll = self.pageYOffset;
            } else if (document.documentElement && document.documentElement.scrollTop) {
                yScroll = document.documentElement.scrollTop;
            } else if (document.body) {
                yScroll = document.body.scrollTop;
            }

            //            document.getElementById("DivHide").style.filter="Alpha(Opacity=50)";
            //            document.getElementById("DivHide").style.visibility = "visible";

            var objLog = document.getElementById(divName);

            objLog.style.marginLeft = (W / 2 - 300) + "px";
            objLog.style.top = (H / 2 - 90 - 225 + yScroll) + "px";
            objLog.style.display = "";
        }

        function ShowNoticeDetail(divName) {
            var W = screen.width; //取得屏幕分辨率宽度
            var H = screen.height; //取得屏幕分辨率高度
            var yScroll; //取滚动条高度
            if (self.pageYOffset) {
                yScroll = self.pageYOffset;
            } else if (document.documentElement && document.documentElement.scrollTop) {
                yScroll = document.documentElement.scrollTop;
            } else if (document.body) {
                yScroll = document.body.scrollTop;
            }

            var objLog = document.getElementById(divName);

            objLog.style.marginLeft = (W / 2 - 250) + "px";
            objLog.style.top = (H / 2 - 90 - 225 + yScroll) + "px";
            objLog.style.display = "";
        }
        function ShowMyDetail() {
            showFloat("DivNotcieDetail");
            return true;
        }
        function closeNoticeMore() {
            ShowNoFlash("DivNoticeMore");
            return true;
        }
    </script>
    <style type="text/css">
        .textBox {
            height: 25px;
            line-height: 25px;
            padding-left: 5px;
            padding-top: 0px !important;
        }

        .checkBox input {
            vertical-align: middle;
        }
    </style>
</head>
<body style="overflow: hidden">
    <!--加一个iframe用于遮挡下拉框-->
    <iframe id="MyiFm" src="javascript:false" style="left: 30px; top: 20px; height: 240px"
        class="HideIframe"></iframe>
    <!--密码修改层-->
    <form id="Form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></ajaxToolkit:ToolkitScriptManager>

        <!--公告层-->
        <div id="DivNoticeMore" runat="server" class="DivShowFloat" style="display: none; overflow: hidden; width: 440px; height: 265px;">
            <p style="text-align: left">
                &nbsp;<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <table style="padding: 0; margin: 0; height: 252px" width="440px" border="1" align="left"
                            cellpadding="0" cellspacing="0" class="info">
                            <tr>
                                <td height="32" style="background-image: url(../CSS/LG/Images/login/td_bg.gif)" id="tdNeedImg1">
                                    <table width="96%" border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="2%">
                                                <img src="../CSS/LG/Images/login/icon1.gif" width="5" height="11" alt="" />
                                            </td>
                                            <td width="61%" align="left">
                                                <%=Resources.Lang.Login_MSG2 %><%--公告--%>
                                            </td>

                                            <td align="right">
                                                <input id="btnClose" runat="server" type="button" value="<%$ Resources:Lang,Login_btnClose %>" class="buttonclose"
                                                    onclick="closeNoticeMore();" /><%--关闭--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="height: 100%">
                                <td valign="top" align="center">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="height: 100%">
                                        <tr>
                                            <td style="width: 96%; padding-left: 4px; height: 200px" valign="top">
                                                <asp:GridView ID="GVNoticeMore" runat="server" AllowPaging="True"
                                                    AutoGenerateColumns="false" BorderStyle="None" BorderWidth="0px"
                                                    EmptyDataText="(无)" OnPageIndexChanging="GVNoticeMore_PageIndexChanging"
                                                    OnRowDataBound="GVNoticeMore_RowDataBound"
                                                    OnSelectedIndexChanged="GVNoticeMore_SelectedIndexChanged"
                                                    PagerSettings-Visible="false" RowStyle-Height="20px" ShowHeader="false"
                                                    Width="98%">
                                                    <RowStyle Height="20px" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <img align="absmiddle" alt="" src="../CSS/LG/images/Login/point.gif" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <a href="JavaScript:void(0);">
                                                                    <%#(DataBinder.Eval(Container.DataItem, "title").ToString().Length > 20) ? (DataBinder.Eval(Container.DataItem, "title").ToString().Substring(0, 15) + "...") : (DataBinder.Eval(Container.DataItem, "title"))%>
                                                                </a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbContent" runat="server" Text='<%# Eval("comment")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbTitle" runat="server" Text='<%# Eval("title")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="navigatorInTd"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </p>
        </div>
        <!--公告单条显示层-->
        <div id="DivNotcieDetail" runat="server" class="DivShowFloat" style="display: none; overflow: hidden; width: 480px; height: 363px;">
            <p style="text-align: left">
                &nbsp;<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <uc1:ShowNotcieDetail ID="ShowNotcieDetail1" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </p>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="87" style="background-image: url(../CSS/LG/Images/login/top_bg1.gif)">
                    <table width="990" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="18"></td>
                            <td width="356" height="87">
                                <asp:Image ID="imgLogo" runat="server" Height="87px" Width="356px" onerror="this.onload = null; this.src='../../Layout/multi/images/cn/top_logo.png';" />
                            </td>
                            <td align="right">
                                <div runat="server" id="selectr" style="float: right; width: 300px;">
                                </div>
                            </td>
                            <td width="10">&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="15" style="background-image: url(../CSS/LG/Images/login/top_bg2.gif)"></td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="340" style="background-image: url(../CSS/LG/Images/login/center_bg.gif)">
                    <table width="990" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="990" height="340" class="login_center_bg">
                                <table width="660" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td height="108">&nbsp;
                                        </td>
                                        <td width="326">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="202" height="120">&nbsp;
                                        
                                        </td>
                                        <td valign="top">
                                            <table width="450px" border="0" cellspacing="0" cellpadding="0" style="margin-left: 490px; margin-top: 50px;">
                                                <tr>
                                                    <td height="20" id="tdUser" style="text-align: right; width: 175px; height: 28px; line-height: 28px;">
                                                        <%=Resources.Lang.Login_MSG3%>：</td>
                                                    <%--用户名--%>
                                                    <td style="width: 120px;">
                                                        <asp:TextBox ID="txtUserName" runat="server" DESIGNTIMEDRAGDROP="514" MaxLength="16"
                                                            CssClass="de textBox" onkeydown="if(event.keyCode==9){$get('aa').focus();}"
                                                            Width="120px" BackColor="White"></asp:TextBox>
                                                    </td>
                                                    <td style="padding-left: 5px;">
                                                        <a id="aa" href="JavaScript:var obj=$get('ResetPassword1_txtID');obj.disabled=false;obj.value='';obj.className='deEx';$get('ResetPassword1_lblText').innerHTML='';showFloatEx();obj.focus();" style="display:none;">
                                                            <%=Resources.Lang.Login_MSG4 %><%--修改密码--%></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="20px" style="text-align: right; height: 28px; line-height: 28px; white-space: pre;"><%=Resources.Lang.Login_MSG5 %>：</td>
                                                    <%--密码--%>
                                                    <td style="width: 120px;">
                                                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" DESIGNTIMEDRAGDROP="514"
                                                            MaxLength="16" CssClass="de textBox"
                                                            onkeydown="if (event.keyCode==13){event.keyCode=9;$get('chkSave').focus();$get('btnLogin').click();} if(event.keyCode==9){$get('chkSave').focus();}"
                                                            Width="120px" BackColor="White"></asp:TextBox>
                                                    </td>
                                                    <td style="padding-left: 5px;">
                                                        <a href="mailto:eservice@dreamtek.net.cn" id="resetpassword" ><%=Resources.Lang.Login_MSG7 %><%--忘记密码了?--%></a>                                                      
                                                    </td>

                                                </tr>
                                                <tr id="trLanguage" runat="server">
                                                    <td height="20px" style="text-align: right; height: 28px; line-height: 28px; white-space: pre;"><%=Resources.Lang.Login_MSG13 %>：</td>
                                                    <%--语言--%>
                                                    <td style="width: 120px;">
                                                        <asp:DropDownList runat="server" ID="ddlLanguage" Width="120px" CssClass="textBox" AutoPostBack="True" OnTextChanged="ddlLanguage_SelectedIndexChanged">
                                                            <asp:ListItem Value="zh-cn" Text="简体中文"></asp:ListItem>
                                                            <%--简体中文"--%>
                                                            <asp:ListItem Value="en-us" Text="English"></asp:ListItem>
                                                            <%--英语--%>
                                                            <asp:ListItem  Selected="True" Value="zh-TW" Text="繁體中文"></asp:ListItem>
                                                            <%--繁体中文--%>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding-left: 5px;"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1">
                                                            <tr>
                                                                <td style="width: 228px; text-align: right;">
                                                                    <asp:CheckBox ID="chkSave" runat="server" CssClass="checkBox" Text="<%$Resources:Lang,Login_MSG8 %>" /></td>
                                                                <%--记住用户名--%>
                                                                <td>
                                                                    <input type="image" height="24" src="../CSS/LG/Images/login/login.gif" border="0" name="imageField" class="login_logbutton"
                                                                        id="btnLogin" runat="server" alt="<%=Resources.Lang.Login_MSG9 %>" onkeydown="if(event.keyCode==9){$get('tdUser').focus();}" />
                                                                </td>
                                                                <%--登录--%>
                                                            </tr>
                                                        </table>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="70">&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
            <tr>
                <td>
                    <table width="991" height="106" border="0" align="center" cellpadding="0" cellspacing="0"
                        style="background-color: #FFFFFF" id="info">
                        <tr>
                            <td rowspan="4" valign="top" style="background-repeat: no-repeat; background-image: url(../CSS/LG/Images/login/index_info_left.gif)" width="36" height="106"></td>
                            <td colspan="4" style="background-image: url(../CSS/LG/Images/login/index_info_top.gif); background-repeat: no-repeat;" width="911" height="22"></td>
                            <td rowspan="4" valign="top" style="background-image: url(../CSS/LG/Images/login/index_info_right.gif); background-repeat: no-repeat;" width="43"
                                height="106"></td>
                            <td style="background-image: url(../CSS/LG/Images/login/spacer.gif); background-repeat: no-repeat;" width="1" height="22"></td>
                        </tr>
                        <tr>
                            <td class="login_gonggao"  width="420" height="30"></td>
                            <td>
                                <a href="JavaScript:showFloat('DivNoticeMore');">
                                    <img src="../CSS/LG/Images/login/index_info_news_more.gif" width="59" height="30" alt="<%=Resources.Lang.Login_MSG10 %>" border="0" /></a><%--更多--%>
                            </td>
                            <td rowspan="3" valign="top" style="background-repeat: no-repeat; background-image: url(../CSS/LG/Images/login/index_info_06.gif)" width="9" height="84"></td>
                            <td rowspan="3" width="423" height="84" valign="top">
                                <table width="423" height="84" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="66">
                                            <a href="#">
                                                <img src="../CSS/LG/Images/login/index_info_faq.gif" width="66" height="84" alt="FAQ" border="0" /></a>
                                        </td>
                                        <td width="4">
                                            <img src="../CSS/LG/Images/login/index_info_08.gif" width="4" height="84" alt="" />
                                        </td>
                                        <td width="69">
                                            <a href="#" class="login_help"></a><%--帮助--%>
                                        </td>
                                        <td width="4">
                                            <img src="../CSS/LG/Images/login/index_info_10.gif" width="4" height="84" alt="" />
                                        </td>
                                        <td width="68">
                                            <a href="mailto:eservice@dreamtek.net.cn" class="login_applyAccount"></a><%--申请账号--%>
                                        </td>
                                        <td width="4">
                                            <img src="../CSS/LG/Images/login/index_info_12.gif" width="4" height="84" alt="" />
                                        </td>
                                        <td width="208" class="login_support copyright" align="right" valign="bottom">(02)77448089<br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="background-image: url(../CSS/LG/Images/login/spacer.gif); background-repeat: no-repeat" width="1" height="30"></td>
                        </tr>
                        <tr>
                            <td colspan="2" rowspan="2" width="479" height="40" valign="top">
                                <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="50%" height="25" valign="top">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="50%">
                                                                <asp:GridView ID="GVNotice" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                                                    Width="98%" BorderStyle="None" BorderWidth="0px" RowStyle-Height="20px" EmptyDataText="无"
                                                                    OnRowDataBound="GVNotice_RowDataBound" OnSelectedIndexChanged="GVNotice_SelectedIndexChanged">
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-Width="10px">
                                                                            <ItemTemplate>
                                                                                <img src="../CSS/LG/Images/login/point.gif" align="absmiddle" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <a href="JavaScript:void(0);">
                                                                                    <%#(DataBinder.Eval(Container.DataItem, "title").ToString().Length > 18) ? (DataBinder.Eval(Container.DataItem, "title").ToString().Substring(0, 18) + "...") : (DataBinder.Eval(Container.DataItem, "title"))%>
                                                                                </a>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbContent" runat="server" Text='<%# Eval("comment")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbTitle" runat="server" Text='<%# Eval("title")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                            <td width="50%">
                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                                                    Width="98%" BorderStyle="None" BorderWidth="0px" RowStyle-Height="20px" OnRowDataBound="GVNotice1_RowDataBound"
                                                                    OnSelectedIndexChanged="GVNotice1_SelectedIndexChanged">
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-Width="10px">
                                                                            <ItemTemplate>
                                                                                <img src="../CSS/LG/Images/login/point.gif" align="absmiddle" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <a href="JavaScript:void(0);">
                                                                                    <%#(DataBinder.Eval(Container.DataItem, "title").ToString().Length > 18) ? (DataBinder.Eval(Container.DataItem, "title").ToString().Substring(0, 18) + "...") : (DataBinder.Eval(Container.DataItem, "title"))%>
                                                                                </a>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbContent" runat="server" Text='<%# Eval("comment")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbTitle" runat="server" Text='<%# Eval("title")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="background-image: url(../CSS/LG/Images/login/spacer.gif); background-repeat: no-repeat" width="1" height="40"></td>
                        </tr>
                        <tr>
                            <td height="5">
                                <img src="../CSS/LG/Images/login/spacer.gif" width="1" height="1" alt="" style="background-repeat: no-repeat;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <input type="hidden" id="hiddLang" runat="server" />
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="center" style="height: 42px; background-image: url(../CSS/LG/Images/login/bottom_bg.gif)" class="CopyRight">
                Copyright DreamTek. All Rights Reserved                
            </tr>
        </table>
        <script type="text/javascript">
            function SetFocus(ctl) {
                var text = ctl.createTextRange();
                text.collapse(false);
                text.select();
            }
            if (flag != null) {
                if (flag == 0) {
                    SetFocus($get('txtUserName'));
                }
                else if (flag == 2) {
                    SetFocus($get('txtPassword'));
                }
                else {
                    $get('ResetPassword1_txtCurrentPW').focus();
                }
            }



        </script>
    </form>
    <script type="text/javascript">
        //window.onload = function () {
        //    var resetpassword = document.getElementById("resetpassword");
        //    //退出监听点击事件
        //    resetpassword.addEventListener("tap", function () {
        //    location.href = "../../Apps/System/FrmResetPassword.aspx";
        //    //location.target = "_blank";
        //    });
        //    resetpassword.addEventListener("click", function () {
        //        var userno = document.getElementById("<%=txtUserName.ClientID %>").value;
        //        location.href = "../../Apps/System/FrmResetPassword.aspx?userno=" + userno;
        //       // location.target = "_blank";
        //    });
        // };

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

        function ChangeImagePath() {
            var css_name = GetCookie("CSS_Name");
            if (css_name == null)
                return;
            var pattern = "LAYOUT\/CSS\/([A-Z][A-Z0-9_\-]*)\/IMAGES\/Login\/[A-Z][A-Z0-9\-_]+\.(GIF|JPG|BMP|ICO|PNG|JPEG)";
            try {


                //更换Image路径
                for (var i = 0; i < document.images.length; i++) {
                    var r = new RegExp(pattern, "gi");
                    r.ignoreCase = true;
                    var result = r.exec(document.images[i].src);
                    if (result != null) {
                        //alert(document.images[i].src);
                        document.images[i].src = document.images[i].src.substr(0, result.index + 11) + css_name + document.images[i].src.substr(result.index + 11 + result[1].length);
                    }
                }
                //更换背景图片的路径
                var eleList = document.getElementsByTagName("*");
                for (var i = 0; i < eleList.length; i++) {
                    if (eleList[i].style != null && eleList[i].style.backgroundImage != null && eleList[i].style.backgroundImage != "") {
                        var pattern = "\.\.\/CSS\/([A-Z][A-Z0-9_\-]*)\/IMAGES\/Login\/[A-Z][A-Z0-9\-_]+\.(GIF|JPG|BMP|ICO|PNG|JPEG)";
                        var r = new RegExp(pattern, "gi");
                        var result = r.exec(eleList[i].style.backgroundImage);
                        if (result != null) {

                            eleList[i].style.backgroundImage = eleList[i].style.backgroundImage.substr(0, result.index + 7) + css_name + eleList[i].style.backgroundImage.substr(result.index + 7 + result[1].length);


                        }
                    }
                }
                
                var aReturn = document.getElementsByTagName("input");
                for (var i = 0; i < aReturn.length; i++) {

                    if (aReturn[i].type == "image" && aReturn[i].src != null && aReturn[i].src != "") {
                        var pattern = "\.\.\/CSS\/([A-Z][A-Z0-9_\-]*)\/IMAGES\/Login\/[A-Z][A-Z0-9\-_]+\.(GIF|JPG|BMP|ICO|PNG|JPEG)";
                        var r = new RegExp(pattern, "gi");
                        var result = r.exec(aReturn[i].src);
                        if (result != null) {
                            aReturn[i].src = aReturn[i].src.substr(0, result.index + 7) + css_name + aReturn[i].src.substr(result.index + 7 + result[1].length);
                            //alert(aReturn[i].src);
                        }
                    }
                }
            }
            catch (e) {
                alert(e.message);
            }
        }
        ChangeImagePath();
    </script>


</body>
</html>
