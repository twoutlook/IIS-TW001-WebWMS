<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBar_CodeMakeEdit.aspx.cs" Inherits="Apps_BAR_FrmBar_CodeMakeEdit_aspx" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link type="text/css" rel="Stylesheet" href="../../Layout/CSS/LG/Page.css" id="cssUrl" runat="server" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/Calendar/calendar-blue.css" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/Css/weui.css" />
    <style type="text/css">
        html {
            height: 100%;
        }

        body {
            height: 100%;
        }

        #aspnetForm {
            height: 100%;
        }

        .master_container {
            height: 100%;
        }

        .tableCell {
            display: table;
            width: 100%;
        }

        span.requiredSign {
            color: #FF0000;
            font-weight: bold;
            padding: 0px 3px;
        }

        .tdodyRules td {
            padding: 0px;
            text-align: center;
        }

        .tdodyRules .dropDownList {
            height: 23px;
            line-height: 23px;
        }

        .tableContainer {
            width: 900px;
        }

            .tableContainer td, th {
                height: 25px;
                line-height: 25px;
                text-align: center;
            }

        .txtTitle {
            text-align: right !important;
            padding-right: 15px;
            width: 150px;
            color: #3580c9;
            height: 35px !important;
            line-height: 35px !important;
        }

        .txtItemTitle {
            width: 150px;
            text-align: right !important;
            padding-right: 15px !important;
            background-color: #d6eefe !important;
            color: #3580c9;
        }


        .RuleSelect {
            width: 300px;
            height: 25px;
            line-height: 25px;
        }

        .PrintSelect {
            width: 194px;
            height: 25px;
            line-height: 25px;
        }

        .txtSN {
            width: 500px;
            height: 25px;
            line-height: 25px;
        }

        .ruleItemTable input {
            height: 25px;
            line-height: 25px;
            padding-left: 3px;
        }

        .weul {
            border: solid 1px #d6eefe;
            list-style: none;
            width: 400px;
            max-height: 300px;
            padding: 0px;
            position: absolute;
            z-index: 999;
            background: rgba(183, 171, 171, 0.73);
            overflow-y: auto;
            display: none;
        }

            .weul li {
                background-color: white;
                color: #3580c9;
                margin-bottom: 1px;
                text-align: left;
                padding-left: 5px;
                cursor: pointer;
            }

        .errorMsgBox {
            display: block;
            position: absolute;
            height: 30px;
            line-height: 30px;
            width: 200px;
            left: 8px;
            top: -7px;
            transform: translate(-2%, -100%);
            z-index: 0;
            border-radius: 4px;
            font-size: 14px;
            color: #333;
            border: 1px solid #ddd;
        }

        .errorTip {
            position: absolute;
            width: 7px;
            height: 7px;
            bottom: 0;
            left: 15px;
            background-color: #fff;
            z-index: -1;
            border: 1px solid #ddd;
            transform: translate(-50%, 50%) rotate(45deg);
        }

        .errorCon {
            position: relative;
            color: #000;
            background-color: #fff;
            border-radius: 3px;
            height: 28px;
        }

        .errorMsg {
            text-align: center;
            color: red;
            font-weight: bolder;
            font-size: 13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <asp:Literal ID="ltPageTable" runat="server" Text=""></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="background-color: white; text-align: left; height: 100%; padding: 15px;">
        <table class="tableContainer" cellspacing="1" cellpadding="1" border="0">
            <thead>
                <tr valign="top">
                    <td class="txtTitle">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBar_CodeMakeEdit_BarcodeRule %>"></asp:Label>：</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="drpRuleSelect" runat="server" CssClass="RuleSelect">
                        </asp:DropDownList>
                        <asp:DropDownList ID="drpPrintSelect" runat="server" CssClass="PrintSelect">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="txtTitle">
                        <asp:Label ID="Label2" runat="server" Text="SN："></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtSN" runat="server" CssClass="txtSN" MaxLength="200" disabled="disabled"></asp:TextBox>
                    </td>
                </tr>
            </thead>
            <tbody id="tbRuleItems" runat="server" style="padding: 10px;">
            </tbody>
            <tfoot>
                <tr>
                    <td style="text-align: center; padding: 15px 0px;" colspan="2">
                        <input id="btnSaveRule" runat="server" type="button" class="ButtonSave" value="<%$ Resources:Lang,WMS_Common_Button_Save %>" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    <!-- loading toast -->
    <div id="loadingToast" class="im_loading_toast" style="display: none;">
        <div class="im_mask_transparent"></div>
        <div class="im_toast">
            <div class="im_loading">
                <div class="im_loading_leaf im_loading_leaf_0"></div>
                <div class="im_loading_leaf im_loading_leaf_1"></div>
                <div class="im_loading_leaf im_loading_leaf_2"></div>
                <div class="im_loading_leaf im_loading_leaf_3"></div>
                <div class="im_loading_leaf im_loading_leaf_4"></div>
                <div class="im_loading_leaf im_loading_leaf_5"></div>
                <div class="im_loading_leaf im_loading_leaf_6"></div>
                <div class="im_loading_leaf im_loading_leaf_7"></div>
                <div class="im_loading_leaf im_loading_leaf_8"></div>
                <div class="im_loading_leaf im_loading_leaf_9"></div>
                <div class="im_loading_leaf im_loading_leaf_10"></div>
                <div class="im_loading_leaf im_loading_leaf_11"></div>
            </div>
            <p class="im_toast_content"><%= Resources.Lang.WMS_Common_Element_SaveLoading %></p>
        </div>
    </div>
    <input type="hidden" id="hiddId" runat="server" />
    <input type="hidden" id="hiddRuleType" runat="server" />
    <input id="hiddDropDownListOfRuleType" runat="server" type="hidden" />
    <input id="hiddDropDownListOfDateFormat" runat="server" type="hidden" />
    <input id="hiddDropDownListOfSnoFormat" runat="server" type="hidden" />
    <input id="hiddUser" runat="server" type="hidden" />
    <script type="text/javascript" src="../../scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="../../Layout/Js/jquery.json-2.2.min.js"></script>
    <script type="text/javascript">
        var WMSUI = {
            FormFed: {
                ID: $("#<%=hiddId.ClientID%>"),
                RuleType: $("#<%=hiddRuleType.ClientID%>"),
                PrintType: $("#<%=drpPrintSelect.ClientID%>"),
                RuleItems: $("#<%=tbRuleItems.ClientID%>"),
                RuleSelect: $("#<%=drpRuleSelect.ClientID%>"),
                btnSaveRule: $("#<%=btnSaveRule.ClientID%>"),
                User: $("#<%=hiddUser.ClientID%>"),
                BtnBack: $("#<%=btnBack.ClientID%>")
            },
            FormVal: {
                RuleId: "",
                SNId: "",
                CreateUser: "",
                PrintId: "",
                RuleVales: []
            },
            //初始化页面
            Init: function () {
                var _self = WMSUI;
                //类型选择变更事件
                _self.FormFed.RuleSelect.change(function () {
                    var ruleID = $(this).val();
                    var defPrint = $(this).find("option:selected").attr("defprint");
                    _self.FormFed.PrintType.val(defPrint);
                    var printId = _self.FormFed.PrintType.val();
                    $.ajax({
                        type: "Post",
                        async: false, //已經是同步請求了
                        cache: false,
                        global: false,
                        url: "../WMSService/CodeRuleService.asmx/LoadCodeRules",
                        data: $.toJSON({ ruleid: ruleID, printid: printId }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            _self.FormFed.RuleItems.empty();
                            var result = data.d;
                            if (result.code == "0") {
                                _self.FormFed.RuleItems.append(result.msg);
                            } else {
                                alert(result.msg);
                            }
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                });

                //类型选择变更事件
                _self.FormFed.PrintType.change(function () {
                    var printId = $(this).val();
                    var ruleID = _self.FormFed.RuleSelect.val();
                    $.ajax({
                        type: "Post",
                        async: false, //已經是同步請求了
                        cache: false,
                        global: false,
                        url: "../WMSService/CodeRuleService.asmx/LoadCodeRules",
                        data: $.toJSON({ ruleid: ruleID, printid: printId }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            _self.FormFed.RuleItems.empty();
                            var result = data.d;
                            if (result.code == "0") {
                                _self.FormFed.RuleItems.append(result.msg);
                            } else {
                                alert(result.msg);
                            }
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                });

                //输入框输入事件
                _self.FormFed.RuleItems.on('keyup', ".textBox", function () {
                    _self.ShowDataSinc(this);
                });

                //输入框输入事件
                _self.FormFed.RuleItems.on('paste', ".textBox", function () {
                    _self.ShowDataSinc(this);
                });
                //选择事件
                _self.FormFed.RuleItems.on('click', ".weul li", function () {
                    var inputVal = $(this).data("number");
                    $(this).parent().parent().find("input").val(inputVal);
                    $(this).parent().hide();
                });
                //输入框输入事件
                _self.FormFed.RuleItems.on('blur', ".textBox", function (e) {
                    $(this).parent().find(".weul").fadeOut(400);
                    var obj = $(this);
                    setTimeout(function () { _self.CheckMsgAsnc(obj) }, 600);
                });
                _self.FormBtnInit();
            },
            CheckMsgAsnc: function (obj) {
                var _self = WMSUI;
                var container = $(obj).parent().parent("tr");
                var type = container.data("ruletype");
                var source = parseInt(container.data("source"));//来源 0：条码规则，1：打印规则
                if (source == 0) {
                    if (type == 2 || type == 4 || type == 7 || type == 9 || type == 10 || type == 11) {
                        return;
                    }
                    var codeVal = $(obj).val().trim();
                    if (codeVal.length == 0) {
                        return;
                    }
                    var typeCode = type + '';

                    $.ajax({
                        type: "Post",
                        async: false, //已經是同步請求了
                        cache: false,
                        global: false,
                        url: "../WMSService/CodeRuleService.asmx/CheckSnDate",
                        data: $.toJSON({ code: codeVal, codetype: typeCode }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var result = data.d;
                            if (result.code == "1") {
                                _self.ErrorTips(obj, true, "<%= Resources.Lang.FrmBar_CodeMakeEdit_Tips_Wuxiaoshuju %>");
                    }
                        },
                        error: function (err) {
                        }
                    });
        }
            },
            //操作执行中
            FormBtnLoading: function () {
                var _self = WMSUI;
                _self.FormFed.btnSaveRule.unbind("click");
            },
            //初始化按钮
            FormBtnInit: function () {
                var _self = WMSUI;
                _self.FormFed.btnSaveRule.bind("click", _self.SaveRuleItems);
            },
            CheckForm: function () {
                var _self = WMSUI;
                var obj;
                var returnValue = true;
                if (_self.FormFed.RuleSelect.val().length == 0) {
                    alert("<%= Resources.Lang.FrmBar_CodeMakeEdit_Tips_XuanTiaoma %>");
                    returnValue = false;
                }
                var totallen = 0;
                if (returnValue) {
                    _self.FormFed.RuleItems.find(".tbRuleItems").find("tr").each(function (i) {
                        var len = $(this).data("len");//长度
                        var ruleType = $(this).data("ruletype");//规则类型
                        var isNeed = $(this).data("isneed");//是否必填
                        var fmat = $(this).data("fmat");//格式
                        var defval = $(this).data("defval");//默认值
                        var rulename = $(this).data("name");//名称
                        var source = parseInt($(this).data("source"));//来源 0：条码规则，1：打印规则
                        obj = $(this).find("input[id='txtValue']");
                        if (parseInt(isNeed) == "1") {
                            if (obj.val().trim().length == 0) {
                                //alert("请输入内容！");
                                _self.ErrorTips(obj, true, "<%= Resources.Lang.FrmBar_CodeMakeEdit_Tips_ShuruNeirong %>");
                                obj.focus();
                                returnValue = false;
                                return false;
                            }
                        }

                        if (source == 0) {

                            if (obj.val().length > parseInt(len)) {
                                //alert(rulename + "长度不符合条码规则");
                                _self.ErrorTips(obj, true, "<%= Resources.Lang.FrmBar_CodeMakeEdit_Tips_ChangduBufu %>");
                                obj.focus();
                                returnValue = false;
                                return false;
                            }

                            if (obj.val().trim().length > 0) {
                                switch (ruleType) {
                                    case 1://料号
                                        break;
                                    case 2://生产日期
                                        if (!_self.CheckDateCode(obj.val(), fmat)) {
                                            //alert("格式不正确！");
                                            _self.ErrorTips(obj, true, "<%= Resources.Lang.FrmBar_CodeMakeEdit_Tips_GeshiBudui %>");
                                            obj.focus();
                                            returnValue = false;
                                            return false;
                                        }
                                        break;
                                    case 3://采购单单号
                                        break;
                                    case 4://采购单项次
                                        if (!_self.CheckInt(obj.val().trim())) {
                                            //alert("采购单项次格式不正确！");
                                            _self.ErrorTips(obj, true, "<%= Resources.Lang.FrmBar_CodeMakeEdit_Tips_GeshiBudui %>");
                                            obj.focus();
                                            returnValue = false;
                                            return false;
                                        }
                                    case 5://供应商代码
                                        break;
                                    case 6://销售单单号
                                        break;
                                    case 7://销售单项次
                                        if (!_self.CheckInt(obj.val().trim())) {
                                            //alert("销售单项次格式不正确！");
                                            _self.ErrorTips(obj, true, "<%= Resources.Lang.FrmBar_CodeMakeEdit_Tips_GeshiBudui %>");
                                            obj.focus();
                                            returnValue = false;
                                            return false;
                                        }
                                    case 8://客户代码
                                        break;
                                    case 9://数量
                                        if (!_self.CheckAllDecimal(obj.val().trim())) {
                                            _self.ErrorTips(obj, true, "<%= Resources.Lang.FrmBar_CodeMakeEdit_Tips_GeshiBudui %>");
                                            //alert("数量格式不正确！");
                                                obj.focus();
                                                returnValue = false;
                                                return false;
                                            }
                                            break;
                                        case 10://任意值

                                            break;
                                        case 11://流水号

                                            break;
                                        default:
                                            break;
                                    }
                                }
                            } else {
                                if (ruleType == 1) {
                                    if (obj.val().trim().length > 0) {
                                        if (!_self.CheckDecimal(obj.val().trim())) {
                                            //alert("数量格式不正确！");
                                            _self.ErrorTips(obj, true, "<%= Resources.Lang.FrmBar_CodeMakeEdit_Tips_GeshiBudui %>");
                                        obj.focus();
                                        returnValue = false;
                                        return false;
                                    }
                                }
                            }
                        }
                    });
                }
                return returnValue;
            },
            SaveRuleItems: function () {
                var _self = WMSUI;
                $("#loadingToast").show();
                _self.FormBtnLoading();
                if (!_self.CheckForm()) {
                    _self.FormBtnInit(); //初始化按钮
                    $("#loadingToast").hide();
                    return;
                }

                //获取规则ID
                _self.FormVal.RuleId = _self.FormFed.RuleSelect.val();
                _self.FormVal.CreateUser = _self.FormFed.User.val();
                _self.FormVal.SNId = _self.FormFed.ID.val();
                _self.FormVal.PrintId = _self.FormFed.PrintType.val();
                //定义规则对象
                var ruleInfoVal = {
                    Sno: 0,
                    Source: 0,
                    RuleVal: ""
                };

                //获取规则明细数据
                _self.FormVal.RuleVales = [];
                _self.FormFed.RuleItems.find(".tbRuleItems").find("tr").each(function (i) {
                    ruleInfoVal.Sno = $(this).data("seq");//当前项的序号
                    ruleInfoVal.Source = parseInt($(this).data("source"));
                    ruleInfoVal.RuleVal = $.trim($(this).find("input[id='txtValue']").val());

                    //行数据赋值
                    _self.FormVal.RuleVales.push({
                        Sno: ruleInfoVal.Sno,
                        Source: ruleInfoVal.Source,
                        RuleVal: ruleInfoVal.RuleVal
                    });
                });

                $.ajax({
                    type: "Post",
                    async: false, //已經是同步請求了
                    cache: false,
                    global: false,
                    url: "../WMSService/CodeRuleService.asmx/SaveCodeNo",
                    data: $.toJSON({ parameter: _self.FormVal }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#loadingToast").hide();
                        var result = data.d;
                        if (result.code == "0") {
                            alert("<%= Resources.Lang.WMS_Common_Msg_SaveSuccess %>");
                            window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click();
                            CloseMySelf('SnCode');
                            return false;
                            //self.FormFed.BtnBack.trigger("click");
                            //self.location.reload(true);
                        } else {
                            alert(result.msg);
                            _self.FormBtnInit(); //初始化按钮
                        }
                    },
                    error: function (err) {
                        $("#loadingToast").hide();
                        alert(err);
                        _self.FormBtnInit(); //初始化按钮
                    }
                });
            },
            CheckDateCode: function (P_DateCode, P_Format) {
                switch (P_Format) {
                    case "YYMMDD":
                        if (P_DateCode.length != 6) {
                            return false;
                        }
                        var DATE_FORMAT = /^[0-9]{2}[0-1]?[0-9]{1}[0-3]?[0-9]{1}$/;
                        if (!DATE_FORMAT.test(P_DateCode)) {
                            return false;
                        }
                        break;
                    case "MMDDYY":
                        var DATE_FORMAT = /^[0-1]?[0-9]{1}[0-3]?[0-9]{1}[0-9]{2}$/;
                        if (!DATE_FORMAT.test(P_DateCode)) {
                            return false;
                        }
                        break;
                    case "YYWW":
                        var DATE_FORMAT = /^[0-9]{2}[0-5]?[0-9]{1}$/;
                        if (!DATE_FORMAT.test(P_DateCode)) {
                            return false;
                        }
                        break;
                    default:
                        return false;
                        break;
                }
                return true;
            },
            CheckInt: function (P_value) {
                var DATE_FORMAT = /^[1-9]*[1-9][0-9]*$/;
                if (!DATE_FORMAT.test(P_value)) {
                    return false;
                } else {
                    return true;
                }
            },
            CheckDecimal: function (P_value) {
                //var format = /(^d*.?d*[0-9] d*$)|(^[0-9]d*.d*$)/;
                var format = /^([1-9]\d*)(\.\d{1,2})?$/;
                if (!format.test(P_value)) {
                    return false;
                } else {
                    return true;
                }
            },
            CheckAllDecimal: function (P_value) {
                //var format = /(^d*.?d*[0-9] d*$)|(^[0-9]d*.d*$)/;
                var format = /^([1-9]\d*)(\.\d{1,6})?$/;
                if (!format.test(P_value)) {
                    return false;
                } else {
                    return true;
                }
            },
            ShowDataSinc: function (obj) {
                var container = $(obj).parent().parent("tr");
                var type = container.data("ruletype");
                var source = parseInt(container.data("source"));//来源 0：条码规则，1：打印规则
                if (source == 0) {
                    if (type == 2 || type == 4 || type == 7 || type == 9 || type == 10 || type == 11) {
                        return;
                    }
                    var codeVal = $(obj).val().trim();
                    if (codeVal.length == 0) {
                        return;
                    }
                    var typeCode = type + '';
                    var weul = container.find(".weul");
                    weul.empty();
                    $.ajax({
                        type: "Post",
                        async: false, //已經是同步請求了
                        cache: false,
                        global: false,
                        url: "../WMSService/CodeRuleService.asmx/GetSnDate",
                        data: $.toJSON({ code: codeVal, codetype: typeCode }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var result = data.d;
                            if (result.code == "0") {
                                weul.append(result.msg);
                                weul.show();
                            } else {

                            }
                        },
                        error: function (err) {
                        }
                    });
                }
            },
            ErrorTips: function (obj, isShow, msg) {
                var thisParent = obj.parent();
                thisParent.css("position", "relative");
                thisParent.find(".errorMsgBox").remove();
                thisParent.append('<div class="errorMsgBox"><div class=\"errorTip\"></div><div class=\"errorCon\"><div class=\"errorMsg\">' + msg + '</div></div></div>');
                thisParent.find(".errorMsgBox").show(100).delay(2500).hide(100);
            }
        };
        WMSUI.Init();
    </script>
</asp:Content>
