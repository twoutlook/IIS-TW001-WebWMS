<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BASE_TVShow_ProductionMent.aspx.cs" Inherits="Apps_BASE_BASE_TVShow_ProductionMent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../../Layout/Js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () { 
            window.setInterval("btnRefresh()", $("#lblRefreshtime").attr("innerText"));
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
            var storey = $("#pageIndex").attr("innerText");        
            $.ajax({
                type: "post",
                url: "BASE_TVShow_ProductionMent.aspx/GetData",
                data: "{ 'Storey': " + storey + " }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {                  
                    var result = data.d.split('||');
                    $('#indexPage').html(result[0] == null ? '无数据' : result[0]);
                    $('#pageIndex').html(result[1] == null ? '无数据' : result[1]);

                    $('#erpcode1').html(result[2] == null ? '无数据' : result[2]);
                    $('#lkpm1').html(result[3] == null ? '无数据' : result[3]);
                    $('#pkpm1').html(result[4] == null ? '无数据' : result[4]);

                    $('#erpcode2').html(result[5] == null ? '无数据' : result[5]);
                    $('#lkpm2').html(result[6] == null ? '无数据' : result[6]);
                    $('#pkpm2').html(result[7] == null ? '无数据' : result[7]);

                    $('#erpcode3').html(result[8] == null ? '无数据' : result[8]);
                    $('#lkpm3').html(result[9] == null ? '无数据' : result[9]);
                    $('#pkpm3').html(result[10] == null ? '无数据' : result[10]);

                    $('#erpcode4').html(result[11] == null ? '无数据' : result[11]);
                    $('#lkpm4').html(result[12] == null ? '无数据' : result[12]);
                    $('#pkpm4').html(result[13] == null ? '无数据' : result[13]);

                    $('#erpcode5').html(result[14] == null ? '无数据' : result[14]);
                    $('#lkpm5').html(result[15] == null ? '无数据' : result[15]);
                    $('#pkpm5').html(result[16] == null ? '无数据' : result[16]);

                    $('#erpcode6').html(result[17] == null ? '无数据' : result[17]);
                    $('#lkpm6').html(result[18] == null ? '无数据' : result[18]);
                    $('#pkpm6').html(result[19] == null ? '无数据' : result[19]);

                    $('#erpcode7').html(result[20] == null ? '无数据' : result[20]);
                    $('#lkpm7').html(result[21] == null ? '无数据' : result[21]);
                    $('#pkpm7').html(result[22] == null ? '无数据' : result[22]);

                    $('#erpcode8').html(result[23] == null ? '无数据' : result[23]);
                    $('#lkpm8').html(result[24] == null ? '无数据' : result[24]);
                    $('#pkpm8').html(result[25] == null ? '无数据' : result[25]);
                    for (var i = 1; i < 9; i++) {
                        //var erpcode = $("#erpcode" + i).attr("innerText");
                        //if (erpcode != '无数据')
                        //{
                        //    $("#erpcode" + i).css("color", "red");
                        //}
                        var lkpm = $("#lkpm" + i).attr("innerText");                       
                        if (lkpm != '无数据') {
                            var lkpmlist = lkpm.split('/');
                            if (lkpmlist[0] == lkpmlist[1]) {
                                $("#lkpm" + i).css("color", "rgba(35, 157, 47, 1)");
                            }
                            else {
                                $("#lkpm" + i).css("color", "black");
                            }
                        }
                        else {
                            $("#lkpm" + i).css("color", "black");
                        }
                        var pkpm = $("#pkpm" + i).attr("innerText");
                        if (pkpm != '无数据') {
                            var pkpmlist = pkpm.split('/');
                            if (pkpmlist[0] == pkpmlist[1]) {
                                $("#pkpm" + i).css("color", "rgba(35, 157, 47, 1)");
                            }
                            else {
                                $("#pkpm" + i).css("color", "black");
                            }
                        }
                        else {
                            $("#pkpm" + i).css("color", "black");
                        }
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
        .auto-style1
        {
            height: 20px;
        }

        .cticketcode
        {
            text-align: center;
            /*width: 33%;*/
            background-color: #9999FF;
        }

        .lk
        {
            text-align: center;
            /*width: 33%;*/
            background-color: #CCCC00;
        }

        .pk
        {
            text-align: center;
            /*width: 33%;*/
            background-color: #99FFCC;
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
                                        <asp:Label ID="lblTitle" runat="server" Text="出库进度"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="indexPage" runat="server" Text="0/0"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Label ID="pageIndex" runat="server" Text="0" Style="display: none"></asp:Label>    
                          <asp:Label ID="lblRefreshtime" runat="server" Style="display: none"></asp:Label>                   
                        <table class="mainTb" style="width: 100%; ">                            
                            <tr>
                                <td style="padding-left: 0px; height: 497px;">
                                    <table border="1" cellspacing="0" style="width: 100%; height:497px">
                                        <tr >

                                            <td class="cticketcode" style="width:38%">
                                                <font size="16px"><asp:Label ID="Label1"  runat="server" Text="领料单号"></asp:Label></font>
                                            </td>
                                            <td class="lk">
                                                <font size="16px"><asp:Label ID="Label2"  runat="server" Text="立库"></asp:Label></font>
                                            </td>
                                            <td class="pk">
                                                <font size="16px"><asp:Label ID="Label4"  runat="server" Text="平库"></asp:Label></font>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td class="cticketcode">
                                                <font size="16px"><asp:Label ID="erpcode1"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="lk">
                                                <font size="16px"><asp:Label ID="lkpm1"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="pk">
                                                <font size="16px"><asp:Label ID="pkpm1"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td class="cticketcode">
                                                <font size="16px"><asp:Label ID="erpcode2"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="lk">
                                                <font size="16px"><asp:Label ID="lkpm2"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="pk">
                                                <font size="16px"><asp:Label ID="pkpm2"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td class="cticketcode">
                                                <font size="16px"><asp:Label ID="erpcode3"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="lk">
                                                <font size="16px"><asp:Label ID="lkpm3"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="pk">
                                                <font size="16px"><asp:Label ID="pkpm3"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td class="cticketcode">
                                                <font size="16px"><asp:Label ID="erpcode4"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="lk">
                                                <font size="16px"><asp:Label ID="lkpm4"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="pk">
                                                <font size="16px"><asp:Label ID="pkpm4"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td class="cticketcode">
                                                <font size="16px"><asp:Label ID="erpcode5"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="lk">
                                                <font size="16px"><asp:Label ID="lkpm5"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="pk">
                                                <font size="16px"><asp:Label ID="pkpm5"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td class="cticketcode">
                                                <font size="16px"><asp:Label ID="erpcode6"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="lk">
                                                <font size="16px"><asp:Label ID="lkpm6"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="pk">
                                                <font size="16px"><asp:Label ID="pkpm6"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td class="cticketcode">
                                                <font size="16px"><asp:Label ID="erpcode7"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="lk">
                                                <font size="16px"><asp:Label ID="lkpm7"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="pk">
                                                <font size="16px"><asp:Label ID="pkpm7"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td class="cticketcode">
                                                <font size="16px"><asp:Label ID="erpcode8"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="lk">
                                                <font size="16px"><asp:Label ID="lkpm8"  runat="server" Text="无数据"></asp:Label></font>
                                            </td>
                                            <td class="pk">
                                                <font size="16px"><asp:Label ID="pkpm8"  runat="server" Text="无数据"></asp:Label></font>
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
