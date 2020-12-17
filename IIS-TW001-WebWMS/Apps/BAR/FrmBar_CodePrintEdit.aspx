<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBar_CodePrintEdit.aspx.cs" Inherits="Apps_BAR_FrmBar_CodePrintEdit" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

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
                            <asp:Label ID="lblPrintCode" runat="server" Text="<%$ Resources:Lang, FrmBar_SNManagement_PrintCode %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPrintCode" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="10"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, FrmBar_SNManagement_PrintName %>"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPrintName" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCodeType" runat="server" Text="<%$ Resources:Lang, FrmBar_CodePrintEdit_BarCodeType %>"></asp:Label>：
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="drpCodeType" runat="server" Width="95%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
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
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, WMS_Common_Element_Remark %>"></asp:Label>：
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="99%" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td style="text-align: center; padding: 15px 0px;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, WMS_Common_Button_Save %>" />
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
            </td>
        </tr>
        <tr class="tableCell" id="trRuleDetail" runat="server">
            <td>
                <div>
                    <div style="padding: 0px 0px 15px 0px;">
                        <input id="btnNew" runat="server" type="button" class="ButtonAdd" value="<%$ Resources:Lang, WMS_Common_Button_New %>" />
                        <input id="btnSaveRule" runat="server" type="button" class="ButtonSave" value="<%$ Resources:Lang, WMS_Common_Button_Save %>" />
                    </div>
                    <table class="InputTable" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
                        <thead>
                            <tr>
                                <th style="width: 5%; min-width: 50px;"><%= Resources.Lang.WMS_Common_Element_XiangCi %></th>
                                <th style="width: 15%; min-width: 170px;"><%= Resources.Lang.FrmBar_CodePrintEdit_Th_mingcheng %></th>
                                <th style="width: 8%; min-width: 120px;"><%= Resources.Lang.WMS_Common_Element_LeiXing %></th>
                                <th style="width: 5%; min-width: 60px;"><%= Resources.Lang.FrmBar_CodePrintEdit_Th_BiYao %></th>
                                <th style="width: 27%;"><%= Resources.Lang.FrmBar_CodePrintEdit_Th_ShuoMing %></th>
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
                DropDownListOfRuleType: $("#<%=hiddDropDownListOfRuleType.ClientID%>"),
                btnNew: $("#<%=btnNew.ClientID%>"),
                btnSaveRule: $("#<%=btnSaveRule.ClientID%>"),
                User: $("#<%=hiddUser.ClientID%>")
            },
            FormVal: {
                id: "",
                CreateUser: "",
                Rules: []
            },
            //初始化页面
            Init: function () {
                var _self = WMSUI;
                _self.FormFed.btnNew.bind('click', _self.AppendRuleItem);
                _self.FormFed.btnSaveRule.bind("click", _self.SaveRuleItems);
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
                if (_self.FormFed.RuleItems.find("tr").length == 0) {
                    alert("<%= Resources.Lang.FrmBar_CodePrintEdit_Tips_XuanzeMingxi %>");
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
                            alert("<%= Resources.Lang.FrmBar_CodePrintEdit_Tips_NeedName %>");
                            obj.focus();
                            returnValue = false;
                            return false;
                        }

                        obj = $(this).find("select[id='drpRuleType']");
                        if (obj.val().trim().length == 0) {
                            alert("<%= Resources.Lang.FrmBar_CodePrintEdit_Tips_NeedType %>");
                            obj.focus();
                            returnValue = false;
                            return false;
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
                _self.FormVal.id = _self.FormFed.ID.val();
                _self.FormVal.CreateUser = _self.FormFed.User.val();

                //定义规则对象
                var ruleInfoVal = {
                    Seq: 0,
                    Name: "",
                    IType: "",
                    IsNeed: false,
                    Remark: ""
                };

                //获取规则明细数据
                _self.FormVal.Rules = [];
                _self.FormFed.RuleItems.find("tr").each(function (i) {
                    ruleInfoVal.Seq = $(this).children().first().text();
                    ruleInfoVal.Name = $.trim($(this).find("input[id='txtName']").val());
                    ruleInfoVal.IType = $(this).find("select[id='drpRuleType']").val();
                    ruleInfoVal.IsNeed = $(this).find("input[id='chkmandatory']").is(':checked');
                    ruleInfoVal.Remark = $(this).find("input[id='txtRemark']").val();

                    //行数据赋值
                    _self.FormVal.Rules.push({
                        Seq: ruleInfoVal.Seq,
                        Name: ruleInfoVal.Name,
                        IType: ruleInfoVal.IType,
                        IsNeed: ruleInfoVal.IsNeed,
                        Remark: ruleInfoVal.Remark
                    });
                });

                $.ajax({
                    type: "Post",
                    async: true, //已經是同步請求了
                    cache: false,
                    global: false,
                    url: "../WMSService/CodeRuleService.asmx/SavePrintRules",
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
                if (num > 10) {
                    alert("<%= Resources.Lang.FrmBar_CodePrintEdit_Tips_MingxiGeshu %>");
                    return false;
                }
                html.push("<tr>");
                html.push("<td >" + num + "</td>");
                html.push("<td class=\"td-left\"><input id=\"txtName\" type=\"text\" class=\"textBox\" style=\"width:98%;min-width: 150px;\" maxlength=\"50\" autocomplete=\"off\" /></td>");
                html.push("<td class=\"td-left\">");
                html.push(_self.FormFed.DropDownListOfRuleType.val());
                html.push("</td>");
                html.push("<td><span id=\"spanmandatory\"></span><input id=\"chkmandatory\" type=\"checkbox\" /></td>");
                html.push("<td><input type=\"text\" id=\"txtRemark\" style=\"width: 98%;min-width: 200px;\" autocomplete=\"off\" /></td>")
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
            },
            //重新加载规则信息
            ReloadRuleItems: function () {
                var _self = WMSUI;
                var num = 1;
                _self.FormFed.RuleItems.find("tr").each(function (i) {
                    $(this).children().first().text(num);
                    num++;
                });
            }
        };
        WMSUI.Init();
    </script>
</asp:Content>
