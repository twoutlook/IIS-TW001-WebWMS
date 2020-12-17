<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BASE_TVShow_Mixed.aspx.cs" Inherits="Apps_BASE_BASE_TVShow_Mixed" %>

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
            btnRefresh();
        }

        function btnRefresh() {           
            var pageindex = $("#pageIndex").attr("innerText");
            var storey_id = $("#storey_id").attr("innerText");
            var lblcurrMixed = $("#lblcurrMixed").attr("innerText");
            var currPageIndex = $("#currPageIndex").attr("innerText");
            
            $.ajax({
                type: "post",
                url: "BASE_TVShow_Mixed.aspx/GetData",
                data: "{ 'pageindex': '" + pageindex + "', 'storey_id': '" + storey_id + "','currPageIndex': '" + currPageIndex + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d.split('||');

                    $('#indexPage').html(result[0] == null ? '无数据' : result[0]);
                    $('#pageIndex').html(result[1] == null ? '无数据' : result[1]);

                    //$('#lblLeftStatus').html(result[0]);
                    //$('#lblSiteCode').html(result[1]);
                    //$('#lblCticketCode').html(result[2]);
                    //$('#lblCpositionCode').html(result[4]);
                    //$('#lblPalletCode').html(result[3]);

                    $('#lblErpCode1').html((result[2] == null || result[2] == '') ? '无数据' : result[2]);
                    $('#lblSchedule1').html((result[3] == null || result[3] == '') ? '无数据' : result[3]);
                    $('#lblErpCode2').html((result[4] == null || result[4] == '') ? '无数据' : result[4]);
                    $('#lblSchedule2').html((result[5] == null || result[5] == '') ? '无数据' : result[5]);
                    $('#lblErpCode3').html((result[6] == null || result[6] == '') ? '无数据' : result[6]);
                    $('#lblSchedule3').html((result[7] == null || result[7] == '') ? '无数据' : result[7]);
                    $('#lblErpCode4').html((result[8] == null || result[8] == '') ? '无数据' : result[8]);
                    $('#lblSchedule4').html((result[9] == null || result[9] == '') ? '无数据' : result[9]);
                    $('#lblErpCode5').html((result[10] == null || result[10] == '') ? '无数据' : result[10]);
                    $('#lblSchedule5').html((result[11] == null || result[11] == '') ? '无数据' : result[11]);
                    $('#lblErpCode6').html((result[12] == null || result[12] == '') ? '无数据' : result[12]);                    
                    $('#lblSchedule6').html((result[13] == null || result[13] == '') ? '无数据' : result[13]);

                    for (var i = 1; i < 7; i++) {                       
                        var lkpm = $("#lblSchedule" + i).attr("innerText");
                        if (lkpm != '无数据') {
                            var lkpmlist = lkpm.split('/');
                            if (lkpmlist[0] == lkpmlist[1] && lkpm != '0/0') {
                                $("#lblSchedule" + i).css("color", "rgba(35, 157, 47, 1)");
                            }
                        }                     
                    }
                    $('#currpageindex').html(result[14] == null ? '无数据' : result[14]);

                    $('#lblcurrMixed').html(result[15] == null ? '无数据' : result[15]);
                    $('#lblCinvCode1').html(result[16] == null ? '无数据' : result[16]);
                    $('#lblQty1').html(result[17] == null ? '无数据' : result[17]);
                    $('#lblCinvCode2').html(result[18] == null ? '无数据' : result[18]);
                    $('#lblQty2').html(result[19] == null ? '无数据' : result[19]);
                    $('#lblCinvCode3').html(result[20] == null ? '无数据' : result[20]);
                    $('#lblQty3').html(result[21] == null ? '无数据' : result[21]);
                    $('#lblCinvCode4').html(result[22] == null ? '无数据' : result[22]);
                    $('#lblQty4').html(result[23] == null ? '无数据' : result[23]);
                    $('#lblCinvCode5').html((result[24] == null) ? '无数据' : result[24]);
                    $('#lblQty5').html(result[25] == null ? '无数据' : result[25]);
                    $('#lblCinvCode6').html(result[26] == null ? '无数据' : result[26]);
                    $('#lblQty6').html(result[27] == null ? '无数据' : result[27]);
                   
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
                                    <td style="width: 90%;">
                                        <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                                    </td>
                                      <td>
                                        <asp:Label ID="indexPage" runat="server" Text="0/0"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                         <asp:Label ID="pageIndex" runat="server" Text="1" Style="display: none"></asp:Label>          
                             <asp:Label ID="currPageIndex" runat="server" Text="1" Style="display: none"></asp:Label>          
                         <asp:Label ID="storey_id" runat="server" Style="display: none" ></asp:Label>
                          <asp:Label ID="lblRefreshtime" runat="server" Style="display: none"></asp:Label>
                        <table class="mainTb" style="width: 100%">
                            <tr>
                                <td style="padding-left: 0px; height: 100%;">
                                    <table border="1" cellspacing="0" style="width: 100%">

                                        <%--<tr>
                                            <td rowspan="6" style="background-color: #5DF8F0; font-weight: bold; font-size: 127px; height: 666px;">
                                                <asp:Label ID="lblLeftStatus" runat="server" Text="">入库中</asp:Label>
                                            </td>

                                        </tr>--%>
                                        <tr>
                                            <td style="text-align: center; width: 20%;background-color: #5DF8F0;">
                                                <font size="16px"><asp:Label ID="Label2"  runat="server" Text="ERP单号"></asp:Label></font>
                                            </td>
                                            <td style="text-align: center; width: 20%;background-color: #5DF8F0;">
                                                <font size="16px"><asp:Label ID="Label4"  runat="server" Text="进度"></asp:Label></font>
                                            </td>
                                            <td style="text-align: left; width: 60%;background-color: #00FFFF;" colspan="3">
                                                <font size="16px"><asp:Label ID="Label13"  runat="server" Text="当前配料单号："></asp:Label>
                                                    <asp:Label id="lblcurrMixed"  runat="server"  ></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr style="height: 20%;">
                                            <td style="text-align: left; width: 20%">
                                                <font size="6px">
                                                    <asp:Label ID="lblErpCode1" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 20%">
                                                <font size="6px">
                                                    <asp:Label ID="lblSchedule1" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 15%">
                                                <font size="16px"><asp:Label ID="Label7"  runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label></font><%--料号--%>
                                            </td>
                                            <td style="text-align: left; width: 30%">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode1" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 15%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty1" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr style="height: 20%;">

                                            <td style="text-align: left; width: 20%">
                                                <font size="6px">
                                                    <asp:Label ID="lblErpCode2" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 20%">
                                                <font size="6px">
                                                    <asp:Label ID="lblSchedule2" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 15%">
                                                <font size="16px"><asp:Label ID="Label8"  runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label></font><%--料号--%>
                                            </td>
                                            <td style="text-align: left; width: 30%">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode2" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 15%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty2" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr style="height: 20%;">
                                            <td style="text-align: left; width: 20%">
                                                <font size="6px">
                                                    <asp:Label ID="lblErpCode3" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 20%">
                                                <font size="6px">
                                                    <asp:Label ID="lblSchedule3" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 15%">
                                                <font size="16px"><asp:Label ID="Label9"  runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label></font><%--料号--%>
                                            </td>
                                            <td style="text-align: left; width: 30%">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode3" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 15%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty3" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 20%">
                                                <font size="6px">
                                                    <asp:Label ID="lblErpCode4" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 20%">
                                                <font size="6px">
                                                    <asp:Label ID="lblSchedule4" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 15%">
                                                <font size="16px"><asp:Label ID="Label10"  runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label></font><%--料号--%>
                                            </td>
                                            <td style="text-align: left; width: 30%">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode4" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 15%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty4" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr style="height: 20%;">
                                            <td style="text-align: left; width: 20%">
                                                <font size="6px">
                                                    <asp:Label ID="lblErpCode5" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 20%">
                                                <font size="6px">
                                                    <asp:Label ID="lblSchedule5" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 15%">
                                                <font size="16px"><asp:Label ID="Label11"  runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label></font><%--料号--%>
                                            </td>
                                            <td style="text-align: left; width: 30%">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode5" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 15%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty5" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 20%">
                                                <font size="6px">
                                                    <asp:Label ID="lblErpCode6" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 20%">
                                                <font size="6px">
                                                    <asp:Label ID="lblSchedule6" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 15%">
                                                <font size="16px"><asp:Label ID="Label12"  runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label></font><%--料号--%>
                                            </td>
                                            <td style="text-align: left; width: 30%">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode6" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; width: 15%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty6" runat="server" Text=""></asp:Label>
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
