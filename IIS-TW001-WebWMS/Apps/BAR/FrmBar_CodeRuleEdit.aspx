<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBar_CodeRuleEdit.aspx.cs" Inherits="Apps_BAR_FrmBar_CodeRuleEdit" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

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
            position: relative;
            left: -15px;
            top: 2px;
        }

        .tdodyRules td {
            padding: 0px;
            text-align: center;
        }

        .tdodyRules .dropDownList {
            height: 23px;
            line-height: 23px;
        }

        .numbercss {
            width: 20px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <asp:Literal ID="ltPageTable" runat="server" Text=""></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table style="width: 100%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblRuleCode" runat="server" Text="<%$ Resources:Lang, FrmBar_SNManagement_RuleCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtRuleCode" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="10"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, FrmBar_SNManagement_RuleName %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtRuleName" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblRuleLen" runat="server" Text="<%$ Resources:Lang, FrmBar_SNManagement_RuleLength %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtRuleLen" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCodeType" runat="server" Text="<%$ Resources:Lang, FrmBar_CodeRuleEdit_BarCodeType %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="drpCodeType" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCreateTime" runat="server" Text="<%$ Resources:Lang,WMS_Common_CreateTimeFrom %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtCreateTime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCreateUser" runat="server" Text="<%$ Resources:Lang,WMS_Common_CreateUser %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCreateUser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang,FrmBar_CodeRuleEdit_MoRenPrint %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="drpPrintRule" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang,FrmBar_CodeRuleEdit_ModifyDate %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtUpdateTime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang,FrmBar_CodeRuleEdit_ModifyUser %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtUpdateUser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Remark %>"></asp:Label>：
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtCMEMO" runat="server" TextMode="MultiLine" Width="99%" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td style="text-align: center; padding: 15px 0px;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, WMS_Common_Button_Save %>" OnClientClick="WMSUI.ClearHidd();" />
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
            </td>
        </tr>
        <tr class="tableCell" id="trRuleDetail" runat="server">
            <td>
                <div>
                    <div style="padding: 0px 0px 15px 0px;">
                        <input id="btnNew" runat="server" type="button" class="ButtonAdd" value="<%$ Resources:Lang,WMS_Common_Button_New %>" />
                        <input id="btnSaveRule" runat="server" type="button" class="ButtonSave" value="<%$ Resources:Lang,WMS_Common_Button_Save %>" />
                    </div>
                    <table class="InputTable" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
                        <thead>
                            <tr>
                                <th style="width: 5%; min-width: 50px;"><%= Resources.Lang.WMS_Common_Element_XiangCi %></th>
                                <th style="width: 15%; min-width: 170px;"><%= Resources.Lang.FrmBar_CodeRuleEdit_Th_Mingcheng %></th>
                                <th style="width: 8%; min-width: 70px;"><%= Resources.Lang.FrmBar_CodeRuleEdit_Th_Changdu %></th>
                                <th style="width: 8%; min-width: 120px;"><%= Resources.Lang.FrmBar_CodeRuleEdit_Th_Weishu %></th>
                                <th style="width: 8%; min-width: 120px;"><%= Resources.Lang.WMS_Common_Element_LeiXing %></th>
                                <th style="width: 7%; min-width: 120px;"><%= Resources.Lang.FrmBar_CodeRuleEdit_Th_Geshi %></th>
                                <th style="width: 10%; min-width: 100px;"><%= Resources.Lang.FrmBar_CodeRuleEdit_Th_SheDingzhi %></th>
                                <th style="width: 5%; min-width: 60px;"><%= Resources.Lang.FrmBar_CodeRuleEdit_Th_Biyao %></th>
                                <th style="width: 27%;"><%= Resources.Lang.FrmBar_CodeRuleEdit_Th_Shuoming %></th>
                                <th style="width: 7%; min-width: 80px;" id="thOption" runat="server"></th>
                            </tr>
                        </thead>
                        <tbody id="tbRuleItems" runat="server" class="tdodyRules">
                        </tbody>
                    </table>
                </div>
            </td>
        </tr>
    </table>
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
                RuleItems: $("#<%=tbRuleItems.ClientID%>"),
                RuleLen: $("#<%=txtRuleLen.ClientID%>"),
                DropDownListOfRuleType: $("#<%=hiddDropDownListOfRuleType.ClientID%>"),
                DropDownListOfFormat: $("#<%=hiddDropDownListOfDateFormat.ClientID%>"),
                DropDownListOfSno: $("#<%=hiddDropDownListOfSnoFormat.ClientID%>"),
                btnNew: $("#<%=btnNew.ClientID%>"),
                btnSaveRule: $("#<%=btnSaveRule.ClientID%>"),
                User: $("#<%=hiddUser.ClientID%>")
            },
            FormVal: {
                id: "",
                Rules: []
            },
            //初始化页面
            Init: function () {
                var _self = WMSUI;
                _self.FormFed.btnNew.bind('click', _self.AppendRuleItem);
                _self.FormFed.btnSaveRule.bind("click", _self.SaveRuleItems);
                _self.FormFed.RuleItems.on("keyup", "input[id='txtlen']", function () {
                    //校验上面规则的数量是否填完整
                    var parentTr = $(this).parent().parent();
                    var seq = parentTr.children().first().text();
                    if (parseInt(seq) > 1) {
                        var num = 0;
                        _self.FormFed.RuleItems.find("tr").each(function (i) {
                            if (seq > i + 1) {
                                var objItem = $(this).find("input[id = 'txtlen']");
                                if (objItem.val().length == 0) {
                                    num++;
                                }
                            }
                        });
                        if (num > 0) {
                            $(this).val("");
                            alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_Wanshan %>");
                            return;
                        }
                    }
                    _self.ReloadRuleQty();
                });
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
            ClearHidd: function () {
                var _self = WMSUI;
                _self.FormFed.DropDownListOfFormat.val("");
                _self.FormFed.DropDownListOfRuleType.val("");
                _self.FormFed.DropDownListOfSno.val("");
                return true;
            },
            CheckForm: function () {
                var _self = WMSUI;
                var obj;
                var returnValue = true;
                if (_self.FormFed.RuleItems.find("tr").length == 0) {
                    alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_XuanzeMingxi %>");
                    returnValue = false;
                }
                var totallen = 0;
                if (returnValue) {
                    //验证类型是否相同
                    var idArr = [];
                    _self.FormFed.RuleItems.find("tr").each(function (i) {
                        var currentLen = 0;
                        obj = $(this).find("input[id='txtName']");
                        if (obj.val().trim().length == 0) {
                            alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_NeedMingcheng %>");
                            obj.focus();
                            returnValue = false;
                            return false;
                        }

                        obj = $(this).find("input[id='txtlen']");
                        if (obj.val().trim().length == 0) {
                            alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_NeedChangDu %>");
                            obj.focus();
                            returnValue = false;
                            return false;
                        }
                        currentLen = parseInt(obj.val());
                        totallen += currentLen;

                        obj = $(this).find("input[id='txtIFrom']");
                        if (obj.val().trim().length == 0) {
                            alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_QishiYichang %>");
                            obj.focus();
                            returnValue = false;
                            return false;
                        }

                        obj = $(this).find("input[id='txtITo']");
                        if (obj.val().trim().length == 0) {
                            alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_JieShuYichang %>");
                            obj.focus();
                            returnValue = false;
                            return false;
                        }

                        obj = $(this).find("select[id='drpRuleType']");
                        if (obj.val().trim().length == 0) {
                            alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_NeedType %>");
                            obj.focus();
                            returnValue = false;
                            return false;
                        }

                        var currentType = obj.val().trim();

                        if (currentType != "10") {
                            //如果包含则代表存在相同类型
                            if ($.inArray(currentType, idArr) > -1) {
                                var typeText = obj.find("option:selected").text();
                                alert("<%= Resources.Lang.WMS_Common_Element_LeiXing %>" + "[" + typeText + "]" + "<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_TypeCheck2 %>");
                                obj.focus();
                                returnValue = false;
                                return false;
                            }
                            else {
                                idArr.push(currentType);
                            }
                        }

                        if (currentType == "2") {//生产日期
                            obj = $(this).find("select[id='drpDateFormat']");
                            if (obj.val().trim().length == 0) {
                                alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_NeedGeShi %>");
                                obj.focus();
                                returnValue = false;
                                return false;
                            } else {
                                if ((obj.val().trim() == "YYMMDD" || obj.val().trim() == "MMDDYY") && currentLen != 6) {
                                    alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_RiqiChangdu %>");
                                    obj.focus();
                                    returnValue = false;
                                    return false;
                                } else if (obj.val().trim() == "YYWW" && currentLen != 4) {
                                    alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_RiqiChangdu %>");
                                        obj.focus();
                                        returnValue = false;
                                        return false;
                                    }
                            }
                        }
                        else if (currentType == "9") {//数量
                            obj = $(this).find("input[id='txtInteger']");
                            if (obj.val().trim().length == 0) {
                                alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_NeedInterger %>");
                                obj.focus();
                                returnValue = false;
                                return false;
                            } else {
                                var r = /^\+?[0-9]*$/;//正整数
                                if (!r.test(obj.val().trim())) {
                                    alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_IntergerError %>");
                                    obj.focus();
                                    returnValue = false;
                                    return false;
                                }
                            }

                            var interger = parseInt(obj.val());
                            if (interger > 12) {
                                alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_IntergerLength %>");
                                obj.focus();
                                returnValue = false;
                                return false;
                            }

                            obj = $(this).find("input[id='txtDecimal']");
                            if (obj.val().trim().length == 0) {
                                alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_NeedDecimal %>");
                                obj.focus();
                                returnValue = false;
                                return false;
                            } else {
                                var r = /^\+?[0-9]*$/;//正整数
                                if (!r.test(obj.val().trim())) {
                                    alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_DecimalError %>");
                                    obj.focus();
                                    returnValue = false;
                                    return false;
                                }
                            }

                            var dec = parseInt(obj.val());
                            if (dec > 6) {
                                alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_DecimalLength %>");
                                obj.focus();
                                returnValue = false;
                                return false;
                            }

                            if (dec > 0) {
                                if (currentLen != (interger + dec + 1)) {
                                    alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_Shuzhibufu %>");
                                    returnValue = false;
                                    return false;
                                }
                            } else {
                                if (currentLen != interger) {
                                    alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_Shuzhibufu %>");
                                    returnValue = false;
                                    return false;
                                }
                            }
                        }
                        else if (currentType == "11") {//流水号
                            obj = $(this).find("select[id='drpSnoFormat']");
                            if (obj.val().trim().length == 0) {
                                alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_NeedGeShi %>");
                                obj.focus();
                                returnValue = false;
                                return false;
                            }
                        } else if (currentType == "10") {//任意值
                            obj = $(this).find("input[id='txtDefaultValue']");
                            if (obj.val().trim().length > 0) {
                                var arr = obj.val().trim().split(',');
                                $.each(arr, function (index, item) {
                                    if (item.length != currentLen) {
                                        alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_RenYiChangdu %>");
                                        obj.focus();
                                        returnValue = false;
                                        return false;
                                    }
                                });
                            }
                        }
                    });
    }
                if (returnValue) {
                    if (totallen != parseInt(_self.FormFed.RuleLen.val())) {
                        alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_ZongChangDu %>");
                        returnValue = false;
                    }
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
                _self.FormVal.id = _self.FormFed.ID.val();

                //定义规则对象
                var ruleInfoVal = {
                    Sno: 0,
                    Name: "",
                    Len: 0,
                    IFrom: 0,
                    ITo: 0,
                    IType: "",
                    IFormat: "",
                    IDefault: "",
                    IMandatory: false,
                    Remark: "",
                    CreateUser: ""
                };

                //获取规则明细数据
                _self.FormVal.Rules = [];
                _self.FormFed.RuleItems.find("tr").each(function (i) {
                    ruleInfoVal.Sno = $(this).children().first().text();
                    ruleInfoVal.Name = $.trim($(this).find("input[id='txtName']").val());
                    ruleInfoVal.Len = parseInt($(this).find("input[id='txtlen']").val());
                    ruleInfoVal.IFrom = parseInt($(this).find("input[id='txtIFrom']").val());
                    ruleInfoVal.ITo = parseInt($(this).find("input[id='txtITo']").val());
                    ruleInfoVal.IType = $(this).find("select[id='drpRuleType']").val();
                    ruleInfoVal.IFormat = "";
                    if (ruleInfoVal.IType == "2") {//生产日期
                        ruleInfoVal.IFormat = $(this).find("select[id='drpDateFormat']").val();
                    }
                    else if (ruleInfoVal.IType == "9") {
                        var interger = parseInt($(this).find("input[id='txtInteger']").val());
                        var dec = parseInt($(this).find("input[id='txtDecimal']").val());
                        ruleInfoVal.IFormat = interger + "," + dec;
                    }
                    else if (ruleInfoVal.IType == "11") {//流水号
                        ruleInfoVal.IFormat = $(this).find("select[id='drpSnoFormat']").val();
                    }
                    ruleInfoVal.IDefault = $(this).find("input[id='txtDefaultValue']").val();
                    ruleInfoVal.IMandatory = $(this).find("input[id='chkmandatory']").is(':checked');
                    ruleInfoVal.CreateUser = _self.FormFed.User.val();
                    ruleInfoVal.Remark = $(this).find("input[id='txtRemark']").val();

                    //行数据赋值
                    _self.FormVal.Rules.push({
                        Sno: ruleInfoVal.Sno,
                        Name: ruleInfoVal.Name,
                        Len: ruleInfoVal.Len,
                        IFrom: ruleInfoVal.IFrom,
                        ITo: ruleInfoVal.ITo,
                        IType: ruleInfoVal.IType,
                        IFormat: ruleInfoVal.IFormat,
                        IDefault: ruleInfoVal.IDefault,
                        IMandatory: ruleInfoVal.IMandatory,
                        Remark: ruleInfoVal.Remark,
                        CreateUser: ruleInfoVal.CreateUser
                    });
                });

                $.ajax({
                    type: "Post",
                    async: true, //已經是同步請求了
                    cache: false,
                    global: false,
                    url: "../WMSService/CodeRuleService.asmx/SaveCodeRules",
                    data: $.toJSON({ parameter: _self.FormVal }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#loadingToast").hide();
                        var result = data.d;

                        if (result.code == "0") {
                            alert("<%= Resources.Lang.WMS_Common_Msg_SaveSuccess %>");
                            location.replace(location);
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
            //新增规则明细
            AppendRuleItem: function () {
                var _self = WMSUI;

                //开始组装新建项
                var html = [];
                var num = _self.FormFed.RuleItems.find("tr").length + 1;
                if (num > 15) {
                    alert("<%= Resources.Lang.FrmBar_CodeRuleEdit_Tips_MingXiGeShu %>");
                    return false;
                }
                html.push("<tr>");
                html.push("<td >" + num + "</td>");
                html.push("<td class=\"td-left\"><input id=\"txtName\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width: 150px;\" maxlength=\"50\" /></td>");
                html.push("<td class=\"td-left\"><input id=\"txtlen\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width:60px;text-align:center;\" maxlength=\"50\" /></td>");
                html.push("<td class=\"td-left\" style=\"position: relative;\">");
                html.push("<input id=\"txtIFrom\" type=\"text\" class=\"textBox\" style=\"width: 45px;text-align: center;\" disabled=\"disabled\" maxlength=\"5\" />");
                html.push(" - ");
                html.push("<input id=\"txtITo\" type=\"text\" class=\"textBox\" style=\"width: 45px;text-align: center;\" disabled=\"disabled\" maxlength=\"5\" />");
                html.push("</td>");
                html.push("<td class=\"td-left\">");
                html.push(_self.FormFed.DropDownListOfRuleType.val());
                html.push("</td>");
                html.push("<td class=\"tdFormat\">");
                html.push("");
                html.push("</td>");
                html.push("<td><input id=\"txtDefaultValue\" type=\"text\" class=\"textBox\" style=\"width: 98%;min-width:80px\" maxlength=\"100\" disabled=\"disabled\" /></td>");
                html.push("<td><span id=\"spanmandatory\"></span><input id=\"chkmandatory\" type=\"checkbox\" /></td>");
                html.push("<td><input type=\"text\" id=\"txtRemark\" style=\"width: 98%;min-width: 200px;\" /></td>")
                html.push("<td>");
                html.push("<a href=\"javascript:;\" class=\"cancelButton\" onclick=\"WMSUI.DeleteRuleItem(this);\">" +"<%= Resources.Lang.FrmBar_CodePrintEdit_Th_ShangChu %>" +"</a>");
                html.push("</td>");
                html.push("</tr>");
                _self.FormFed.RuleItems.append(html.join(""));

            },
            //删除规则
            DeleteRuleItem: function (obj) {
                var currRow = $(obj).parent().parent();
                currRow.remove();
                WMSUI.ReloadRuleItems();
                WMSUI.ReloadRuleQty();
            },
            //重新加载规则信息
            ReloadRuleItems: function () {
                var _self = WMSUI;
                var num = 1;
                _self.FormFed.RuleItems.find("tr").each(function (i) {
                    $(this).children().first().text(num);
                    num++;
                });
            },
            //重新加载数量
            ReloadRuleQty: function () {
                var _self = WMSUI;
                var num = 0;
                var isclear = false;
                _self.FormFed.RuleItems.find("tr").each(function (i) {
                    if (!isclear) {
                        var len = $(this).find("input[id = 'txtlen']").val();
                        if (len.length > 0) {
                            var fromSeq = num + 1;
                            var toSeq = fromSeq + parseInt(len) - 1;
                            num = toSeq;
                            $(this).find("input[id = 'txtIFrom']").val(fromSeq);
                            $(this).find("input[id = 'txtITo']").val(toSeq);
                        } else {
                            $(this).find("input[id = 'txtIFrom']").val("");
                            $(this).find("input[id = 'txtITo']").val("");
                            isclear = true;
                        }
                    } else {
                        $(this).find("input[id = 'txtlen']").val("");
                        $(this).find("input[id = 'txtIFrom']").val("");
                        $(this).find("input[id = 'txtITo']").val("");
                    }
                });
            },
            //规则变动
            RuleTypeChange: function (objRuleType) {
                var format = $(objRuleType).parent().parent().find(".tdFormat");
                format.empty();
                var dfVal = $(objRuleType).parent().parent().find("input[id='txtDefaultValue']");
                dfVal.val("");
                dfVal.prop("disabled", true);
                var chkmandatory = $(objRuleType).parent().parent().find("input[id='chkmandatory']");
                chkmandatory.prop("checked", false);
                chkmandatory.prop("disabled", false);
                switch ($(objRuleType).val()) {
                    case "1"://料号
                        break;
                    case "2"://生产日期
                        format.append(WMSUI.FormFed.DropDownListOfFormat.val());
                        break;
                    case "3"://采购单单号
                        break;
                    case "4"://采购单项次
                        break;
                    case "5"://供应商代码
                        break;
                    case "6"://销售单单号
                        break;
                    case "7"://销售单项次
                        break;
                    case "8"://客户代码
                        break;
                    case "9"://数量
                        format.append("<%= Resources.Lang.FrmBar_CodeRuleEdit_Format_zhengshu %>" + "<input id=\"txtInteger\" type=\"text\" class=\"numbercss\" autocomplete=\"off\" />" + "<%= Resources.Lang.FrmBar_CodeRuleEdit_Format_xiaoshu %>" + "<input id=\"txtDecimal\" type=\"text\" class=\"numbercss\" autocomplete=\"off\" /> ");
                        break;
                    case "10"://任意值
                        dfVal.prop("disabled", false);
                        break;
                    case "11"://流水号
                        format.append(WMSUI.FormFed.DropDownListOfSno.val());
                        chkmandatory.prop("checked", true);
                        chkmandatory.prop("disabled", true);
                        break;
                    default:
                        break;
                }
            }
        };
        WMSUI.Init();
    </script>
</asp:Content>
