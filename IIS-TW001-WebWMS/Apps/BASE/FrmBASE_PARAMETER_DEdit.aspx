<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Apps/DefaultMasterPage.master" CodeFile="FrmBASE_PARAMETER_DEdit.aspx.cs" Inherits="Apps_BASE_FrmBASE_PARAMETER_DEdit" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link type="text/css" rel="Stylesheet" href="../../Layout/CSS/LG/Page.css" id="cssUrl" runat="server" />
    <link type="text/css" rel="Stylesheet" href="../../Layout/Calendar/calendar-blue.css" />
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
            left: -10px;
            top: 2px;
        }

        .gridLineHeight {
            line-height: 22px;
        }

        input[type='submit'][disabled], input[type='button'][disabled] {
            opacity: 0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=70); /*兼容ie8及以下*/
        }
        #Table3 td {
            height:30px;
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
    <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02 %>-&gt;<asp:Literal ID="ltPageTable" runat="server" Text="代码组管理"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

    <table id="Table3" style="width: 98%" class="InputTable" cellspacing="1" cellpadding="1" border="0">
        <tbody id="tbodyParameters" runat="server">

        </tbody>
        <tfoot>
            <tr>
                <td colspan = "2" style="text-align:center;padding:15px 0px;">
                    <input id="btnSaveParameter" runat="server" type="button" class="ButtonSave" value="<%$ Resources:Lang,WMS_Common_Button_Save %>" />
                    <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" CausesValidation="false" />
                </td>
            </tr>     
        </tfoot>
    </table>
    <input type="hidden" id="hiddGuid" runat="server" />
    <input type="hidden" id="hiddOperation" runat="server" />
    <input type="hidden" id="hiddGroupGuid" runat="server" />
    <script type="text/javascript" src="../../scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="../../Layout/Js/jquery.json-2.2.min.js"></script>

    <script type="text/javascript">
        var WMSUI = {
            FormFed: {
                ID: $("#<%=hiddGuid.ClientID%>"),
                GroupID:$("#<%=hiddGroupGuid.ClientID%>"),
                Parameters: $("#<%=tbodyParameters.ClientID%>"),
                Operation: $("#<%=hiddOperation.ClientID%>"),
                SaveParameter: $("#<%=btnSaveParameter.ClientID%>")
            },
            FormVal: {
                ID: "",
                GroupID: "",
                Operation: "",
                FlagId: "",
                SortId: "",
                FlagNames: []
            },
            //初始化页面
            Init: function () {
                var _self = WMSUI;
                _self.FormBtnInit();
            },
            //操作执行中
            FormBtnLoading: function () {
                var _self = WMSUI;
                _self.FormFed.SaveParameter.unbind("click");
            },
            //初始化按钮
            FormBtnInit: function () {
                var _self = WMSUI;
                _self.FormFed.SaveParameter.bind("click", _self.SaveParameterItem);
            },
            CheckForm: function () {
                var _self = WMSUI;
                var obj;
                var returnValue = true;
                obj = _self.FormFed.Parameters.find("input[id='txtFlagId']");
                if (obj.val().length == 0) {
                    _self.ErrorTips(obj, true, "请输入子项编号");
                    obj.focus();
                    returnValue = false;
                    return false;
                }

                obj = _self.FormFed.Parameters.find("input[id='txtSortId']");
                if (obj.val().length == 0) {
                    _self.ErrorTips(obj, true, "请输入排序号");
                    obj.focus();
                    returnValue = false;
                    return false;
                } else {
                    if (!_self.CheckInt(obj.val())) {
                        _self.ErrorTips(obj, true, "不是整数");
                        obj.focus();
                        returnValue = false;
                        return false;
                    }
                }

                if (returnValue) {
                    _self.FormFed.Parameters.find("tr").each(function (i) {
                        var itype = $(this).data("itype");//长度
                        if (itype == "lan") {
                            obj = $(this).find("input[id='txtName']");
                            if (obj.val().trim().length == 0) {
                                _self.ErrorTips(obj, true, "请输入名称");
                                obj.focus();
                                returnValue = false;
                                return false;
                            }
                        }
                    });
                }
                return returnValue;
            },
            SaveParameterItem: function () {
                var _self = WMSUI;
                $("#loadingToast").show();
                _self.FormBtnLoading();
                if (!_self.CheckForm()) {
                    _self.FormBtnInit(); //初始化按钮
                    $("#loadingToast").hide();
                    return;
                }

                //获取规则ID
                _self.FormVal.ID = _self.FormFed.ID.val();
                _self.FormVal.GroupID = _self.FormFed.GroupID.val();
                _self.FormVal.Operation = _self.FormFed.Operation.val();
                _self.FormVal.FlagId = _self.FormFed.Parameters.find("input[id='txtFlagId']").val();
                _self.FormVal.SortId = _self.FormFed.Parameters.find("input[id='txtSortId']").val();
                //定义规则对象
                var ruleInfoVal = {
                    Name: "",
                    Lan: ""
                };

                //获取规则明细数据
                _self.FormVal.FlagNames = [];
                _self.FormFed.Parameters.find("tr").each(function (i) {
                    var itype = $(this).data("itype");//长度
                    if (itype == "lan") {
                        var obj = $(this).find("input[id='txtName']");
                        ruleInfoVal.Name = obj.val().trim();
                        ruleInfoVal.Lan = $(this).data("lan");

                        //行数据赋值
                        _self.FormVal.FlagNames.push({
                            Name: ruleInfoVal.Name,
                            Lan: ruleInfoVal.Lan
                        });
                    }
                });

                $.ajax({
                    type: "Post",
                    async: false, //已經是同步請求了
                    cache: false,
                    global: false,
                    url: "../WMSService/CommonService.asmx/SaveParameter",
                    data: $.toJSON({ parameter: _self.FormVal }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#loadingToast").hide();
                        var result = data.d;
                        if (result.code == "0") {
                            alert("<%= Resources.Lang.WMS_Common_Msg_SaveSuccess %>");
                            window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click();
                            CloseMySelf('PARAMETER_D');
                            return false;
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
            CheckInt: function (P_value) {
                var DATE_FORMAT = /^[1-9]*[1-9][0-9]*$/;
                if (!DATE_FORMAT.test(P_value)) {
                    return false;
                } else {
                    return true;
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
