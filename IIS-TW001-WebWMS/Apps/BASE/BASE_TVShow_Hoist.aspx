<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BASE_TVShow_Hoist.aspx.cs" Inherits="Apps_BASE_BASE_TVShow_Hoist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
            $("td").css('font-size', userName - 127 + "px");
            if ($('#lblQty6').html().length > 0)
                $('#yc').css("background-color", "red");
            else $('#yc').css("background-color", "");
        }

        function btnRefresh() {           
            var CRANE = $("#storey_id").attr("innerText"); //线别
            $.ajax({
                type: "post",
                url: "BASE_TVShow_Hoist.aspx/GetData",
                data: "{ 'strCRANE': '" + CRANE + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d.split('||');                   
                    $('#lblLeftStatus').html(result[0] == null ? '无数据' : result[0]);
                    $('#lblStartPoint').html(result[1] == null ? '无数据' : result[1]);
                    $('#lblCticketCode').html(result[2] == null ? '无数据' : result[2]);
                    $('#lblPalletCode').html(result[3] == null ? '无数据' : result[3]);
                    $('#lblTerminalPoint').html(result[4] == null ? '无数据' : result[4]);
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
                    $('#lblCinvCode6').html(result[15] == null ? '' : result[15]);
                    $('#lblQty6').html(result[16] == null ? '' : result[16]);
                    if ($('#lblQty6').html().length > 0)
                        $('#yc').css("background-color", "red");
                    else $('#yc').css("background-color", "");
                   
                },
                error: function (err) {

                }
            });
        }
        setTimeout("btnRefresh()", $("#lblRefreshtime").attr("innerText"));
    </script>
    <title>电视显示</title>
    <style type="text/css">
        .auto-style1
        {
            height: 20px;
        }
        #DivScroll
        {
            word-wrap:break-word;
            word-break:break-all;
            /*overflow:hidden;*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="smg1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upl1" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click" Style="display: none" />
                <div style="padding-top: 0px; height: 710; width: 1265px;" id="DivScroll">
                    <div align="center" style="border: 5px solid #F00">
                        <div align="left" style="background-color: blue; font-size: 60px; width: 100%; color: white">
                            <table style="background-color: blue; width: 100%; height: 106px; text-align: center">
                                <tr>
                                    <td style="width: 100%;">
                                        <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                      
                          <asp:Label ID="storey_id" runat="server" Style="display: none" ></asp:Label>
                          <asp:Label ID="lblRefreshtime" runat="server" Style="display: none"></asp:Label>
                        <table class="mainTb" style="width: 100%">
                            <tr>
                                <td style="padding-left: 0px; height: 100%;">
                                    <table border="1" cellspacing="0" style="width: 100%">

                                        <tr>
                                            <td rowspan="6" style="background-color: #5DF8F0; font-weight: bold; font-size: 127px; height: 497px;">
                                                <asp:Label ID="lblLeftStatus" runat="server" Text="">运作中</asp:Label>
                                            </td>

                                        </tr>
                                        <tr style="height: 20%;">

                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label1"  runat="server" Text="单号"></asp:Label></font>
                                            </td>


                                            <td style="text-align: left; width: 40%" colspan="2">
                                                <font size="6px">
                                                    <asp:Label ID="lblCticketCode" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label3"  runat="server" Text="<%$ Resources:Lang, Common_PallteCode%>"></asp:Label></font><%--板号--%>
                                            </td>
                                            <td style="text-align: left; width: 40%" colspan="2">
                                                <font size="6px">
                                                    <asp:Label ID="lblPalletCode" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr style="height: 20%;">

                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label5"  runat="server" Text="起点"></asp:Label></font>
                                            </td>
                                            <td style="text-align: left; width: 40%" colspan="2">
                                                <font size="6px">
                                                    <asp:Label ID="lblStartPoint" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label6"  runat="server" Text="终点"></asp:Label></font>
                                            </td>
                                            <td style="text-align: left; width: 40%" colspan="2">
                                                <font size="6px">
                                                    <asp:Label ID="lblTerminalPoint" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr style="height: 20%;">

                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label7"  runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label></font><%--料号--%>
                                            </td>
                                            <td style="text-align: left; width: 30%">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode1" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 10%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty1" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label8"  runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label></font><%--料号--%>
                                            </td>
                                            <td style="text-align: left; width: 30%">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode2" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 10%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty2" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr style="height: 20%;">

                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label9"  runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label></font><%--料号--%>
                                            </td>
                                            <td style="text-align: left; width: 30%">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode3" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 10%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty3" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label10"  runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label></font><%--料号--%>
                                            </td>
                                            <td style="text-align: left; width: 30%">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode4" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 10%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty4" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr style="height: 20%;">

                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label11"  runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label></font><%--料号--%>
                                            </td>
                                            <td style="text-align: left; width: 30%">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode5" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 10%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty5" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label12"  runat="server" Text="<%$ Resources:Lang, Common_Exception%>"></asp:Label></font><%--异常--%>
                                            </td>
                                            <td style="text-align: left; width: 30%;" colspan="2" id="yc">
                                                <font size="6px" color="white">
                                                    <asp:Label ID="lblQty6" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="lblCinvCode6" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 10%">
                                                <font size="6px">
                                                    
                                                </font>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
