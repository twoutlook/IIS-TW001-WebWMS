<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Apps/DefaultMasterPage.master" CodeFile="FrmBase_CraneConfigEdit_D_BillNo.aspx.cs" Inherits="Apps_BASE_FrmBase_CraneConfigEdit_D_BillNo" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />

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
            height:100%;
        }
        .tableCell {
            display:table;
            width:100%;
        }

        #tbBillInfo td{
            text-align:center;
        }
        #tbBillInfo thead td {
            color: #3580c9;
            background-color: #d6eefe;
        }


    </style>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBase_CraneConfig_Msg09%>-&gt;<%= Resources.Lang.FrmBase_AGV_D_Title01%><%--立库线别管理-&gt;站点管理--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table id="TabMain" style="width: 95%">
        <tr valign="top" class="tableCell">
            <td valign="top" class="tableCell">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1" id="tbBillInfo">
                    <thead>
                        <tr>
                            <td style="width:50px;"></td>
                            <td><%= Resources.Lang.FrmLogSystemList_Label1%></td><%--单据编号--%>
                            <td><%= Resources.Lang.FrmBase_CraneConfigEdit_D_BillNo_Title01%></td><%--单据名称--%>
                            <td><%= Resources.Lang.FrmBase_InOutTypeStatusList_lblCERPCODE%></td><%--类型编码--%>
                            <td><%= Resources.Lang.FrmBase_InOutTypeStatusList_lblTYPENAME%></td><%--类型名称--%>                            
                        </tr>
                    </thead>
                    <tbody id="tbBillNo" runat="server">

                    </tbody>
                </table>
            </td>
        </tr>
        <tr class="tableCell">
            <td valign="top" style="text-align:center;padding:15px 0px;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>

    <input type="hidden" id="hiddId" runat="server" />
    <input type="hidden" id="hiddUser" runat="server" />
    <script type="text/javascript" src="../../scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        var WMSUI = {
            FormFed: {
                billList: $("#<%=tbBillNo.ClientID%>"),
                btnSave: $("#<%=btnSave.ClientID%>"),
                ids: $("#<%=hiddId.ClientID%>"),
                user: $("#<%=hiddUser.ClientID%>")
            },
            //操作执行中
            FormBtnLoading: function () {
                var _self = WMSUI;
                _self.FormFed.btnSave.unbind("click");
            },
            //初始化按钮
            FormBtnInit: function () {
                var _self = WMSUI;
                _self.FormFed.btnSave.bind("click", _self.SaveBillNos);
            },
            Init: function () {
                this.FormBtnInit();
            },
            SaveBillNos: function () {
                var _self = WMSUI;
                _self.FormBtnLoading();
                var billnos = "";

                _self.FormFed.billList.find("tr").each(function (i) {
                    if ($(this).find("input[id='chkmandatory']").is(':checked')) {
                        if (billnos.length == 0) {
                            billnos += $(this).find(".erpcode").text();
                        } else {
                            billnos += "," + $(this).find(".erpcode").text();
                        }
                    }
                });
                var ids = _self.FormFed.ids.val();
                var user = _self.FormFed.user.val();
                $.ajax({
                    type: "Post",
                    url: "FrmBase_CraneConfigEdit_D_BillNo.aspx/SaveBills",
                    data: JSON.stringify({
                        ids: ids,
                        billnos: billnos,
                        user:user
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var result = data.d;

                        if (result == "0") {
                            alert("<%= Resources.Lang.Common_SuccessSave%>");//保存成功！
                            location.replace(location);
                            //self.location.reload(false);
                        } else {
                            alert(result);
                            _self.FormBtnInit(); //初始化按钮
                        }
                    },
                    error: function (err) {
                        alert(JSON.stringify(err));
                        _self.FormBtnInit(); //初始化按钮
                    }
                });

            }
        }
        WMSUI.Init();
    </script>

</asp:Content>
