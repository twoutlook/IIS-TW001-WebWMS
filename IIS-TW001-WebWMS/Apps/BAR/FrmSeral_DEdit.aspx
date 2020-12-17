<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmSeral_DEdit.aspx.cs" Inherits="Apps_BAR_FrmSeral_DEdit" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

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
    <asp:Literal ID="ltPageTable" runat="server" Text="序列号"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table style="width: 95%" cellspacing="1" cellpadding="1" border="0">
        <tr valign="top" class="tableCell">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="TabMain">
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblITYPE" runat="server" Text="单据号"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtBillCode" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblRuleCode" runat="server" Text="物料编号"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="需要总数"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtTotalCount" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="目前总数"></asp:Label>：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSavedCount" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%"><asp:Label ID="Label1" runat="server" Text="序列号"></asp:Label>：</td>
                        <td colspan="3">
                            <input type="text" id="txtSeral" style="width:300px"  class="NormalInputText" autocomplete="off" placeholder="输入序列号后按回车键"/>
                            <%--<asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" Text="保存" />--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="padding:15px 0px;">
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, WMS_Common_Button_Back %>" UseSubmitBehavior="false" CausesValidation="false" />
            </td>
        </tr>
        <tr class="tableCell" id="trRuleDetail" runat="server">
            <td>
                <div style="height: 420px; overflow-y: auto; width: 100%;">
                    <table class="InputTable" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
                        <thead>
                            <tr>
                            <th>序号</th>
                            <th>序列号</th>
                            <th>维护时间</th>
                            <th>维护人</th>
                            <th>操作</th>
                            </tr>
                        </thead>
                        <tbody id="tbSerals" class="tdodyRules">

                        </tbody>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <input type="hidden" id="hiddcticketcode" runat="server" />
    <input type="hidden" id="hiddInOrOut" runat="server" />
    <input type="hidden" id="hiddCinvCode" runat="server" />
    <input type="hidden" id="hiddUser" runat="server" />
    <script type="text/javascript" src="../../scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="../../Layout/Js/jquery.json-2.2.min.js"></script>
    <script type="text/javascript">
        var WMSUI = {
            FormFed: {
                CinvCode: $("#<%=hiddCinvCode.ClientID%>"),
                CticketCode: $("#<%=hiddcticketcode.ClientID%>"),
                OrderType: $("#<%=hiddInOrOut.ClientID%>"),
                User: $("#<%=hiddUser.ClientID%>"),
                SeralNo: $("#txtSeral"),
                Serals: $("#tbSerals"),
                SavedCount: $("#<%=txtSavedCount.ClientID%>")
            },
            FormVal: {
                CinvCode: "",
                CticketCode: "",
                OrderType: "",
                SeralNo:"",
                User:""
            },
            //初始化页面
            Init: function () {
                var _self = WMSUI;
                _self.LoadData();
                //_self.FormFed.SaveButton.bind("click", _self.SaveSeral);
                _self.FormFed.SeralNo.keydown(function (event) {
                    var e = event || window.event || arguments.callee.caller.arguments[0];
                    if (e && e.keyCode == 13) { // enter 键
                        _self.SaveSeral();
                    }
                });
            },
           
            ClearHidd: function () {
                var _self = WMSUI;
                _self.FormFed.SeralNo.val("");
                _self.FormFed.SeralNo.focus();
                return true;
            },
            SaveSeral: function () {
                var _self = WMSUI;
                if (_self.FormFed.SeralNo.val().trim().length == 0) {
                    alert("请输入序列号！", function () {
                        _self.FormFed.SeralNo.focus();
                    });
                    return false;
                }
                _self.FormVal.CinvCode = _self.FormFed.CinvCode.val();
                _self.FormVal.CticketCode = _self.FormFed.CticketCode.val();
                _self.FormVal.OrderType = _self.FormFed.OrderType.val();
                _self.FormVal.SeralNo = _self.FormFed.SeralNo.val().trim();
                _self.FormVal.User = _self.FormFed.User.val();
                $.ajax({
                    type: "Post",
                    async: true, //已經是同步請求了
                    cache: false,
                    global: false,
                    url: "../WMSService/SeralService.asmx/SaveSeral",
                    data: $.toJSON({ parameter: _self.FormVal }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var result = data.d;
                        if (result.code == "0") {
                            //window.parent.document.all['ctl00_ContentPlaceHolderMain_btnSearch'].click();
                            _self.LoadData();
                            
                        } else {
                            alert(result.msg);
                        }
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            },
            LoadData: function () {
                var _self = WMSUI;
                _self.FormFed.Serals.empty();
                $.ajax({
                    type: "Post",
                    async: true, //已經是同步請求了
                    cache: false,
                    global: false,
                    url: "../WMSService/SeralService.asmx/GetSerals",
                    data: $.toJSON({ OrderType:_self.FormFed.OrderType.val(),OrderCode:_self.FormFed.CticketCode.val(),CinvCode:_self.FormFed.CinvCode.val() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var result = data.d;
                        if (result.code != "-1") {
                            _self.FormFed.SavedCount.val(result.code);
                            _self.FormFed.Serals.append(result.msg);
                            _self.ClearHidd();
                        } else {
                            alert(result.msg);
                        }
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            },
            DeleteSeralNo: function (obj) {
                var _self = WMSUI;
                var seralid = $(obj).data("id");
                $.ajax({
                    type: "Post",
                    async: true, //已經是同步請求了
                    cache: false,
                    global: false,
                    url: "../WMSService/SeralService.asmx/DeleteSeralNo",
                    data: $.toJSON({ id: seralid }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var result = data.d;
                        if (result.code == "0") {
                            alert("删除成功！");
                            _self.LoadData();
                        } else {
                            alert(result.msg);
                        }
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            }
        };
        WMSUI.Init();

    </script>
</asp:Content>
