<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BASE_TVShowIN.aspx.cs" Inherits="Apps_BASE_BASE_TVShowIN" %>

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
           

            //显示不同的样式效果
            if ($('#lblLeftStatus').html() != null && $('#lblLeftStatus').html() != "出库中" && $('#lblLeftStatus').html() != "") {
                $('#divRK').attr("style", "display:");
                $('#divPick').attr("style", "display:none");
                if ($('#lblQty6').html().length > 0)
                    $('#yc').css("background-color", "red");
                else $('#yc').css("background-color", "");
            }
            else if ($('#lblLeftStatus_P').html() != null && $('#lblLeftStatus_P').html() == "出库中") {
                $('#divRK').attr("style", "display:none");
                $('#divPick').attr("style", "display:");
            }
            else {
                $('#divRK').attr("style", "display:");
                $('#divPick').attr("style", "display:none");
                if ($('#lblQty6').html().length > 0)
                    $('#yc').css("background-color", "red");
                else $('#yc').css("background-color", "");
            }
        }

        function btnRefresh() {
            var storey = $("#storey_id").attr("innerText");          
            $.ajax({
                type: "post",
                url: "BASE_TVShowIN.aspx/GetData",
                data: "{ 'Storey': " + storey + " }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d.split('||');
                    //显示不同的样式效果
                    if ((result[0] != null && result[0] != "出库中") || result[0] == null) {                       
                        $('#lblLeftStatus').html(result[0]);
                        $('#lblSiteCode').html(result[1]);
                        $('#lblCticketCode').html(result[2]);
                        $('#lblPalletCode').html(result[3]);
                        $('#lblCpositionCode').html(result[4]);
                        $('#lblCinvCode1').html(result[5]);
                        $('#lblQty1').html(result[6]);
                        $('#lblCinvCode2').html(result[7]);
                        $('#lblQty2').html(result[8]);
                        $('#lblCinvCode3').html(result[9]);
                        $('#lblQty3').html(result[10]);
                        $('#lblCinvCode4').html(result[11]);
                        $('#lblQty4').html(result[12]);
                        $('#lblCinvCode5').html(result[13]);
                        $('#lblQty5').html(result[14]);
                        $('#lblCinvCode6').html(result[15]);
                        $('#lblQty6').html(result[16]);
                        if ($('#lblQty6').html().length > 0)
                            $('#yc').css("background-color", "red");
                        else $('#yc').css("background-color", "");


                        $('#divRK').attr("style", "display:");
                        $('#divPick').attr("style", "display:none");
                    }
                    else if (result[0] != null && result[0] == "出库中") {
                        $('#divRK').attr("style", "display:none");
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
        #DivScroll
        {
            word-wrap:break-word;
            word-break:break-all;            
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
                        <asp:Label ID="storey_id" runat="server" Style="display: none"></asp:Label>
                         <asp:Label ID="lblRefreshtime" runat="server" Style="display: none"></asp:Label>
                        <div id="divRK" >
                        <table class="mainTb" style="width: 100%">
                            <tr>
                                <td style="padding-left: 0px; height: 100%;">
                                    <table border="1" cellspacing="0" style="width: 100%">

                                        <tr>
                                            <td rowspan="6" style="background-color: #5DF8F0; font-weight: bold; font-size: 127px; height: 497px;">
                                                <asp:Label ID="lblLeftStatus" runat="server" Text=""></asp:Label>
                                            </td>

                                        </tr>
                                        <tr style="height: 20%;">

                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label1"  runat="server" Text="站点"></asp:Label></font>
                                            </td>


                                            <td style="text-align: left; width: 40%" colspan="2">
                                                <font size="6px">
                                                    <asp:Label ID="lblSiteCode" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label3"  runat="server" Text="单号"></asp:Label></font>
                                            </td>
                                            <td style="text-align: left; width: 40%" colspan="2">
                                                <font size="6px">
                                                    <asp:Label ID="lblCticketCode" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr style="height: 20%;">

                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label5"  runat="server" Text="板号"></asp:Label></font>
                                            </td>
                                            <td style="text-align: left; width: 40%" colspan="2">
                                                <font size="6px">
                                                    <asp:Label ID="lblPalletCode" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label6"  runat="server" Text="储位"></asp:Label></font>
                                            </td>
                                            <td style="text-align: left; width: 40%" colspan="2">
                                                <font size="6px">
                                                    <asp:Label ID="lblCpositionCode" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr style="height: 20%;">

                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label7"  runat="server" Text="料号"></asp:Label></font>
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
                                                <font size="16px"><asp:Label ID="Label8"  runat="server" Text="料号"></asp:Label></font>
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
                                                <font size="16px"><asp:Label ID="Label9"  runat="server" Text="料号"></asp:Label></font>
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
                                                <font size="16px"><asp:Label ID="Label10"  runat="server" Text="料号"></asp:Label></font>
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
                                                <font size="16px"><asp:Label ID="Label11"  runat="server" Text="料号"></asp:Label></font>
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
                                                <font size="16px"><asp:Label ID="Label12"  runat="server" Text="异常"></asp:Label></font>
                                            </td>
                                            <td style="text-align: left; width: 30%;" colspan ="2" id="yc">
                                                <font size="6px" color="white">
                                                     <asp:Label ID="lblQty6" runat="server" Text="" ></asp:Label>
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
                          <div id="divPick">
                          <table class="mainTb" style="width: 100%" >
                            <tr>
                                <td style="padding-left: 0px; height: 100%;">
                                    <table border="1" cellspacing="0" style="width: 100%">

                                        <tr>
                                            <td rowspan="6" style="background-color:#33FFCC; font-weight: bold; font-size: 127px; height: 497px;">
                                                <asp:Label ID="lblLeftStatus_P" runat="server" Text=""></asp:Label>
                                            </td>

                                        </tr>
                                        <tr style="height: 10%;">

                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="lblpa"  runat="server" Text="栈板"></asp:Label></font>
                                            </td>

                                            <td style="text-align: left; width: 20%" >
                                                <font size="6px">
                                                    <asp:Label ID="lblPallteCode_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label14"  runat="server" Text="单号"></asp:Label></font>
                                            </td>
                                            <td style="text-align: left; width: 30%" >
                                                <font size="6px">
                                                    <asp:Label ID="lblCticketcode_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                               <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label18"  runat="server" Text="储位"></asp:Label></font>
                                            </td>
                                            <td style="text-align: left; width: 50%" >
                                                <font size="6px">
                                                    <asp:Label ID="lblPostitionCode_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr style="height: 20%;">
                                           
                                            <td style="text-align: left; width: 40%" colspan="2">
                                                <font size="6px">
                                                    <asp:Label ID="lblErpCode1_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                             <td style="text-align: left; width: 40%" colspan="3">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode1_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                             <td style="text-align: left; width: 40%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty1_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                         
                                        </tr>
                                        <tr style="height: 20%;">
                                           
                                            <td style="text-align: left; width: 40%" colspan="2">
                                                <font size="6px">
                                                    <asp:Label ID="lblErpCode2_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                             <td style="text-align: left; width: 40%" colspan="3">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode2_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                             <td style="text-align: left; width: 40%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty2_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                         
                                        </tr>
                                         <tr style="height: 20%;">
                                           
                                            <td style="text-align: left; width: 40%" colspan="2">
                                                <font size="6px">
                                                    <asp:Label ID="lblErpCode3_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                             <td style="text-align: left; width: 40%" colspan="3">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode3_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                             <td style="text-align: left; width: 40%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty3_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                         
                                        </tr>
                                         <tr style="height: 20%;">
                                           
                                            <td style="text-align: left; width: 40%" colspan="2">
                                                <font size="6px">
                                                    <asp:Label ID="lblErpCode4_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                             <td style="text-align: left; width: 40%" colspan="3">
                                                <font size="6px">
                                                    <asp:Label ID="lblCinvCode4_P" runat="server" Text=""></asp:Label>
                                                </font>
                                            </td>
                                             <td style="text-align: left; width: 40%">
                                                <font size="6px">
                                                    <asp:Label ID="lblQty4_P" runat="server" Text=""></asp:Label>
                                                </font>
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
