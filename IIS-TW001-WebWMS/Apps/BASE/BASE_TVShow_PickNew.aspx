<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BASE_TVShow_PickNew.aspx.cs" Inherits="Apps_BASE_BASE_TVShow_PickNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../../Layout/Js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            window.setInterval("btnRefresh()", $("#lblRefreshtime").attr("innerText"));
            // alert("1234322");
        });


        var userName = window.screen.height;
        window.onload = function () {
            var userName = window.screen.height;
            document.getElementById("DivScroll").style.height = userName - 150 + 'px';
            $("th").attr("height", "50px");
            $("td").attr("height", +userName / 100 + "px");
            userName = $("td").height();
            //$("td").css('font-size', userName - 127 + "px");
            if ($('#lblLeftStatus').html() != null && $('#lblLeftStatus').html() == "盘点中") {
                $('#divPan').attr("style", "display:");
                $('#divPick').attr("style", "display:none");
            }
            else if ($('#lblLeftStatus_P').html() != null && $('#lblLeftStatus_P').html() == "拣料中") {
                $('#divPan').attr("style", "display:none");
                $('#divPick').attr("style", "display:");
            }
            else {
                $('#divPan').attr("style", "display:");
                $('#divPick').attr("style", "display:none");
            }

        }

        function btnRefresh() {
            var storey = $("#storey_id").attr("innerText");

            $.ajax({
                type: "post",
                url: "BASE_TVShow_Pick.aspx/GetData",
                data: "{ 'plcstr':'" + storey + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d.split('||');
                    if ((result[0] != null && result[0] == "盘点中") || result[0] == "无数据") {
                        $('#divPan').attr("style", "display:");
                        $('#divPick').attr("style", "display:none");
                        $('#lblLeftStatus').html(result[0] == null ? '无数据' : result[0]);
                        $('#lblErpCode').html(result[1] == null ? '无数据' : result[1]);
                        $('#lblCticketCode').html(result[2] == null ? '无数据' : result[2]);
                        $('#lblPalletCode').html(result[3] == null ? '无数据' : result[3]);
                        $('#lblCpositionCode').html(result[4] == null ? '无数据' : result[4]);
                        $('#lblCinvCode1').html(result[5] == null ? '无数据' : result[5]);
                        $('#lblQty1').html(result[6] == null ? '无数据' : result[6]);
                        $('#lblCinvCode2').html(result[7] == null ? '无数据' : result[7]);
                        $('#lblQty2').html(result[8] == null ? '无数据' : result[8]);
                        $('#lblCinvCode3').html(result[9] == null ? '无数据' : result[9]);
                        $('#lblQty3').html(result[10] == null ? '无数据' : result[10]);
                        $('#lblCinvCode4').html(result[11] == null ? '无数据' : result[11]);
                        $('#lblQty4').html(result[12] == null ? '无数据' : result[12]);
                        $('#lblCinvCode5').html(result[13] == null ? '无数据' : result[13]);
                        $('#lblQty5').html(result[14] == null ? '无数据' : result[14]);
                        $('#lblCinvCode6').html(result[15] == null ? '无数据' : result[15]);
                        $('#lblQty6').html(result[16] == null ? '无数据' : result[16]);
                        changeStyle('#lblCinvCode', '28px', '22px', 6);
                    }
                    else if (result[0] != null && result[0] == "拣料中") {
                        $('#divPan').attr("style", "display:none");
                        $('#divPick').attr("style", "display:");
                        $('#lblLeftStatus_P').html(result[0] == null ? '无数据' : result[0]);
                        $('#lblPallteCode_P').html(result[1] == null ? '无数据' : result[1]);
                        $('#lblCticketcode_P').html(result[2] == null ? '无数据' : result[2]);
                        $('#lblPostitionCode_P').html(result[3] == null ? '无数据' : result[3]);
                        $('#lblErpCode1_P').html(result[4] == null ? '无数据' : result[4]);
                        $('#lblCinvCode1_P').html(result[5] == null ? '无数据' : result[5]);
                        $('#lblQty1_P').html(result[6] == null ? '无数据' : result[6]);
                        $('#lblErpCode2_P').html(result[7] == null ? '无数据' : result[7]);
                        $('#lblCinvCode2_P').html(result[8] == null ? '无数据' : result[8]);
                        $('#lblQty2_P').html(result[9] == null ? '无数据' : result[9]);
                        $('#lblErpCode3_P').html(result[10] == null ? '无数据' : result[10]);
                        $('#lblCinvCode3_P').html(result[11] == null ? '无数据' : result[11]);
                        $('#lblQty3_P').html(result[12] == null ? '无数据' : result[12]);
                        $('#lblErpCode4_P').html(result[13] == null ? '无数据' : result[13]);
                        $('#lblCinvCode4_P').html(result[14] == null ? '无数据' : result[14]);
                        $('#lblQty4_P').html(result[15] == null ? '无数据' : result[15]);

                        changeStyle('#lblCinvCode1_P', '28px', '22px', 1);
                        changeStyle('#lblCinvCode2_P', '28px', '22px', 1);
                        changeStyle('#lblCinvCode3_P', '28px', '22px', 1);
                        changeStyle('#lblCinvCode4_P', '28px', '22px', 1);
                    }

                },
                error: function (err) {

                }
            });
        }
        setTimeout("btnRefresh()", $("#lblRefreshtime").attr("innerText"));
    </script>
    <title>电视显示</title>
    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }

        #DivScroll {
            word-wrap: break-word;
            word-break: break-all;
            /*overflow:hidden;*/
        }

        .tv-table {
            width: 100%;
            height: 100%;
            border: none;
            border-spacing: 0px 10px;
        }

            .tv-table td {
                border: 1px solid #2693CB;
            }

            .tv-table tr:last-child {
                background-color: #85527D;
            }

            .tv-table tr:nth-child(1) {
                background-color: #0C5B9E;
            }

            .tv-table tr:nth-child(2) {
                background-color: #0B80A3;
            }

            .tv-table tr:nth-child(3) {
                background-color: #0B80A3;
            }

            .tv-table tr:nth-child(4) {
                background-color: #0B80A3;
            }

            .tv-table tr:nth-child(5) {
                background-color: #0B80A3;
            }
    </style>

    <style type="text/css">
        .td_title {
            width: 10%;
            font-size:36px;
            text-align: center;
            color: white;
        }

        .td_palletcode {
            width: 15%;
            font-size:30px;
            padding-left: 5px;
            text-align: left;
            color: white;
        }

        .td_cticketcode {
            width: 20%;
            font-size:30px;
            padding-left: 5px;
            text-align: left;
            color: white;
        }

        .td_right {
            width: 20%;
            font-size:28px;
            text-align: left;
            padding-left: 5px;
            color: white;
        }

        .td_cinvcode {
            text-align: left;
            padding-left: 5px;
            font-size:28px;
            color: white;
        }

        .td_cinvname {
            text-align: left;
            padding-left: 5px;
            font-size:28px;
            color: white;
        }
    </style>

    <style type="text/css">
        .pan_title {
            text-align:center;
            width:15%;
            font-size:38px;
            color:white;
        }

        .pan_code {
            text-align:left;
            width:30%;
            padding-left:5px;
            font-size:32px;
            color:white;
        }

        .pan_liaohao {
            text-align:center;
            width:8%;
            font-size:38px;
            color:white;
        }

        .pan_chuwei {
            text-align:left;
            padding-left:5px;
            font-size:32px;
            color:white;
        }

        .pan_cinvcode {
            text-align:left;
            padding-left:5px;
            font-size:28px;
            color:white;
        }
        .pan_quantity {
            text-align:left;
            padding-left:5px;
            font-size:28px;
            color:white;
            width:9%;
        }
    </style>
</head>
<body style="margin:0px;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="smg1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upl1" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click" Style="display: none" />
                <div style="padding-top: 0px; height: 710px; width: 1265px;" id="DivScroll">
                    <div align="center" style="background: url(../../Images/TV_Background.png) no-repeat; width: 1265px; height: 710px; background-size: 1265px 710px;">
                        <div align="left" style="font-size: 60px; width: 100%; color: white">
                            <table style="width: 100%; height: 108px; text-align: center">
                                <tr></tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Label ID="storey_id" runat="server" Style="display: none"></asp:Label>
                        <asp:Label ID="lblRefreshtime" runat="server" Style="display: none"></asp:Label>
                        <div id="divPan">
                            <table class="mainTb" style="width: 1210px; height:570px">
                                <tr>
                                    <td style="padding-left: 0px; height: 100%;">
                                        <table border="1"  style="width: 100%" class="tv-table">
                                            <tr style="height: 15%;">
                                                <td rowspan="5" style="background-color: #1A6793; font-weight: bold; font-size: 100px; height: 500px; color: white; width: 10%; text-align: center;">
                                                    <asp:Label ID="lblLeftStatus" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_title" colspan="2">
                                                    <asp:Label ID="Label1"  runat="server" Text="ERP单号"></asp:Label>
                                                </td>
                                                <td class="pan_code" colspan="2">
                                                    <asp:Label ID="lblErpCode" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_title" colspan="2">
                                                    <asp:Label ID="Label3"  runat="server" Text="出库单号"></asp:Label>
                                                </td>
                                                <td class="pan_code" colspan="2">
                                                    <asp:Label ID="lblCticketCode" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 15%;">

                                                <td class="pan_liaohao">
                                                    <asp:Label ID="Label5"  runat="server" Text="板号"></asp:Label>
                                                </td>
                                                <td class="pan_chuwei" colspan="3">
                                                    <asp:Label ID="lblPalletCode" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_liaohao">
                                                    <asp:Label ID="Label6"  runat="server" Text="储位"></asp:Label>
                                                </td>
                                                <td class="pan_chuwei" colspan="3">
                                                    <asp:Label ID="lblCpositionCode" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 15%;">

                                                <td class="pan_liaohao">
                                                    <asp:Label ID="Label7"  runat="server" Text="料号"></asp:Label>
                                                </td>
                                                <td class="pan_cinvcode" colspan="2">
                                                    <asp:Label ID="lblCinvCode1" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_quantity">
                                                    <asp:Label ID="lblQty1" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_liaohao">
                                                    <asp:Label ID="Label8"  runat="server" Text="料号"></asp:Label>
                                                </td>
                                                <td class="pan_cinvcode" colspan="2">
                                                    <asp:Label ID="lblCinvCode2" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_quantity">
                                                    <asp:Label ID="lblQty2" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 15%;">

                                                <td class="pan_liaohao">
                                                    <asp:Label ID="Label9"  runat="server" Text="料号"></asp:Label>
                                                </td>
                                                <td class="pan_cinvcode" colspan="2">
                                                    <asp:Label ID="lblCinvCode3" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_quantity">
                                                    <asp:Label ID="lblQty3" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_liaohao">
                                                    <asp:Label ID="Label10"  runat="server" Text="料号"></asp:Label>
                                                </td>
                                                <td class="pan_cinvcode" colspan="2">
                                                    <asp:Label ID="lblCinvCode4" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_quantity">
                                                    <asp:Label ID="lblQty4" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 15%;">

                                                <td class="pan_liaohao">
                                                    <asp:Label ID="Label11"  runat="server" Text="料号"></asp:Label>
                                                </td>
                                                <td class="pan_cinvcode" colspan="2">
                                                    <asp:Label ID="lblCinvCode5" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_quantity">
                                                    <asp:Label ID="lblQty5" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_liaohao">
                                                    <asp:Label ID="Label12"  runat="server" Text="料号"></asp:Label>
                                                </td>
                                                <td class="pan_cinvcode" colspan="2">
                                                    <asp:Label ID="lblCinvCode6" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_quantity">
                                                    <asp:Label ID="lblQty6" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divPick">
                            <table class="mainTb " style="width: 1210px; height:570px">
                                <tr>
                                    <td style="padding-left: 0px; height: 100%;">
                                        <table border="1" cellspacing="0" style="width: 100%" class="tv-table">
                                            <tr style="height: 15%;">
                                                <td rowspan="5" style="background-color: #1A6793; font-weight: bold; font-size: 100px; height: 500px; color: white; width: 10%; text-align: center;">
                                                    <asp:Label ID="lblLeftStatus_P" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="td_title">
                                                    <asp:Label ID="lblpa"  runat="server" Text="栈板"></asp:Label>
                                                </td>

                                                <td class="td_palletcode">
                                                    <asp:Label ID="lblPallteCode_P" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="td_title">
                                                    <asp:Label ID="Label14"  runat="server" Text="单号"></asp:Label>
                                                </td>
                                                <td class="td_cticketcode">
                                                    <asp:Label ID="lblCticketcode_P" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="td_title">
                                                    <asp:Label ID="Label18"  runat="server" Text="储位"></asp:Label>
                                                </td>
                                                <td class="td_right" style="font-size:30px;">
                                                    <asp:Label ID="lblPostitionCode_P" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 15%;">
                                                <td class="td_cinvcode" colspan="2">
                                                    <asp:Label ID="lblErpCode1_P" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="td_cinvname" colspan="3">
                                                    <asp:Label ID="lblCinvCode1_P" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="td_right">
                                                    <asp:Label ID="lblQty1_P" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 15%;">
                                                <td class="td_cinvcode" colspan="2">
                                                    <asp:Label ID="lblErpCode2_P" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="td_cinvname" colspan="3">
                                                    <asp:Label ID="lblCinvCode2_P" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="td_right">
                                                    <asp:Label ID="lblQty2_P" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 15%;">
                                                <td class="td_cinvcode" colspan="2">
                                                    <asp:Label ID="lblErpCode3_P" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="td_cinvname" colspan="3">
                                                    <asp:Label ID="lblCinvCode3_P" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="td_right">
                                                    <asp:Label ID="lblQty3_P" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 15%;">
                                                <td class="td_cinvcode" colspan="2">
                                                    <asp:Label ID="lblErpCode4_P" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="td_cinvname" colspan="3">
                                                    <asp:Label ID="lblCinvCode4_P" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="td_right">
                                                    <asp:Label ID="lblQty4_P" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
