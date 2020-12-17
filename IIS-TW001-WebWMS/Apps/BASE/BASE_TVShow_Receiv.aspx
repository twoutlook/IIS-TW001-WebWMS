<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BASE_TVShow_Receiv.aspx.cs" Inherits="Apps_BASE_BASE_TVShow_Receiv" %>
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
        }

        function btnRefresh() {
            
            $.ajax({
                type: "post",
                url: "BASE_TVShow_Receiv.aspx/GetData",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d.split('||');
                    $('#Label2').html(result[0] == null ? '无数据' : result[0]);
                    $('#Label3').html(result[1] == null ? '无数据' : result[1]);
                    $('#Label13').html(result[2] == null ? '无数据' : result[2]);
                    $('#Label14').html(result[3] == null ? '无数据' : result[3]);
                    $('#Label16').html(result[4] == null ? '无数据' : result[4]);
                    $('#StatusL1').html(result[5] == null ? '无数据' : result[5]);
                    $('#StatusL2').html(result[6] == null ? '无数据' : result[6]);
                    $('#StatusR1').html(result[7] == null ? '无数据' : result[7]);
                    $('#StatusR2').html(result[8] == null ? '无数据' : result[8]);
                    $('#StatusS1').html(result[9] == null ? '无数据' : result[9]);

                    var StatusL1 = $("#StatusL1").attr("innerText");
                    if (StatusL1 == '1') {                     
                        $("#Label2").css("color", "rgb(255,0,0)");
                        $("#Label6").css("color", "rgb(255,0,0)");
                        $("#Label17").css("color", "rgb(255,0,0)");
                        $("#Label7").css("color", "rgb(255,0,0)");
                        $("#Label19").css("color", "rgb(255,0,0)");
                    }

                    var StatusL2 = $("#StatusL2").attr("innerText");
                    if (StatusL2 == '1') {
                        $("#Label3").css("color", "rgb(255,0,0)");
                        $("#Label8").css("color", "rgb(255,0,0)");
                        $("#Label9").css("color", "rgb(255,0,0)");
                        $("#Label10").css("color", "rgb(255,0,0)");
                        $("#Label11").css("color", "rgb(255,0,0)");
                    }

                    var StatusR1 = $("#StatusR1").attr("innerText");
                    if (StatusR1 == '1') {
                        $("#Label13").css("color", "rgb(255,0,0)");                       
                    }

                    var StatusR2 = $("#StatusR2").attr("innerText");
                    if (StatusR2 == '1') {
                        $("#Label14").css("color", "rgb(255,0,0)");                       
                    }

                    var StatusS1 = $("#StatusS1").attr("innerText");
                    if (StatusS1 == '1') {
                        $("#Label16").css("color", "rgb(255,0,0)");
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
    </style>
    <style type="text/css">
         td{border-bottom:1px solid black;}
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
                        <asp:Label ID="StatusL1" runat="server" Style="display: none"></asp:Label>
                          <asp:Label ID="StatusL2" runat="server" Style="display: none"></asp:Label>
                          <asp:Label ID="StatusR1" runat="server" Style="display: none"></asp:Label>
                          <asp:Label ID="StatusR2" runat="server" Style="display: none"></asp:Label>
                          <asp:Label ID="StatusS1" runat="server" Style="display: none"></asp:Label>
                          <asp:Label ID="lblRefreshtime" runat="server" Style="display: none"></asp:Label>
                        <table class="mainTb" style="width: 100%">
                            <tr>
                                <td style="padding-left: 0px; height: 100%;">
                                    <table  cellspacing="0" style="width: 100%">

                                     <%--   <tr>
                                            <td rowspan="6" style="background-color: #5DF8F0; font-weight: bold; font-size: 127px; height: 666px;">
                                                <asp:Label ID="lblLeftStatus" runat="server" Text="">运作中</asp:Label>
                                            </td>

                                        </tr>--%>
                                        <tr style="height: 20%;">

                                            <td style="text-align: center; ">
                                                <font size="16px"><asp:Label ID="Label1"  runat="server" Text="立       库:"></asp:Label></font>
                                            </td>
                                            <td style=" text-align:left;">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3820.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-21px;margin-left:-56px;vertical-align:middle;">
                                                    <asp:Label ID="Label2" runat="server" Text="1" style="margin-top:0px"></asp:Label>
                                                </font>
                                                <%--<div style="background:url('../../Layout/Css/LG/Images/TVShow/u3820.png') no-repeat;  height:50%; width:19%; position:relative;">
                                                    <font size="6px" >
                                                    <asp:Label ID="Label2" runat="server" Text="1"></asp:Label>
                                                </font>
                                                </div>--%>
                                                
                                            </td>
                                            <td style="text-align: left; ">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3820.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-21px;margin-left:-56px;vertical-align:middle;">
                                                    <asp:Label ID="Label3" runat="server" Text="1" style="margin-top:0px"></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center;">
                                                <font size="16px"><asp:Label ID="Label4"  runat="server" Text="RGV:"></asp:Label></font>
                                            </td>
                                            <td style="text-align: left; ">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3820.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-21px;margin-left:-56px;vertical-align:middle;">
                                                    <asp:Label ID="Label13" runat="server" Text="1" style="margin-top:0px"></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; ">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3820.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-21px;margin-left:-56px;vertical-align:middle;">
                                                    <asp:Label ID="Label14" runat="server" Text="1" style="margin-top:0px"></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; ">
                                                <font size="16px"><asp:Label ID="Label15"  runat="server" Text="升降机:"></asp:Label></font>
                                            </td>
                                            <td style="text-align: left; ">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3820.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-21px;margin-left:-56px;vertical-align:middle;">
                                                    <asp:Label ID="Label16" runat="server" Text="1" style="margin-top:0px"></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr style="height: 20%;">

                                            <td style="text-align: center; ">
                                                <font size="16px"><asp:Label ID="Label5"  runat="server" Text="立库站口:"></asp:Label></font>
                                            </td>
                                            <td style=" text-align:left;">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3828.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-15px;margin-left:-72px;vertical-align:middle;">
                                                    <asp:Label ID="Label6" runat="server" Text="1-1" ></asp:Label>
                                                </font>
                                                <%--<div style="background:url('../../Layout/Css/LG/Images/TVShow/u3820.png') no-repeat;  height:50%; width:19%; position:relative;">
                                                    <font size="6px" >
                                                    <asp:Label ID="Label2" runat="server" Text="1"></asp:Label>
                                                </font>
                                                </div>--%>
                                                
                                            </td>
                                            <td style="text-align: left; ">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3828.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-21px;margin-left:-72px;vertical-align:middle;">
                                                    <asp:Label ID="Label17" runat="server" Text="1-2" style="margin-top:0px"></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left;">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3828.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-21px;margin-left:-72px;vertical-align:middle;">
                                                    <asp:Label ID="Label7" runat="server" Text="1-3" style="margin-top:0px"></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; ">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3828.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-21px;margin-left:-72px;vertical-align:middle;">
                                                    <asp:Label ID="Label19" runat="server" Text="1-4" style="margin-top:0px"></asp:Label>
                                                </font>
                                            </td>
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr style="height: 20%;">

                                            <td style="text-align: left; ">
                                                <%--<font size="16px"><asp:Label ID="Label7"  runat="server" Text="立库站口:"></asp:Label></font>--%>
                                            </td>
                                            <td style=" text-align:left;">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3828.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-21px;margin-left:-72px;vertical-align:middle;">
                                                    <asp:Label ID="Label8" runat="server" Text="2-1" style="margin-top:0px"></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; ">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3828.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-21px;margin-left:-72px;vertical-align:middle;">
                                                    <asp:Label ID="Label9" runat="server" Text="2-2" style="margin-top:0px"></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; ">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3828.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-21px;margin-left:-72px;vertical-align:middle;">
                                                    <asp:Label ID="Label10" runat="server" Text="2-3" style="margin-top:0px"></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: left; ">
                                                <img src="../../Layout/Css/LG/Images/TVShow/u3828.png" style="width: 82px;position:relative;vertical-align:middle;"></img>
                                                <font size="6px" style="position:relative;margin:auto;margin-top:-21px;margin-left:-72px;vertical-align:middle;">
                                                    <asp:Label ID="Label11" runat="server" Text="2-4" style="margin-top:0px"></asp:Label>
                                                </font>
                                            </td>
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr style="height: 20%;">
                                            <td colspan="8"></td>
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
