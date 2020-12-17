<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBase_ShowTVBySiteNew.aspx.cs" Inherits="Apps_BASE_FrmBase_ShowTVBySiteNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../../Layout/Js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            window.setInterval("btnRefresh()", $("#lblRefreshtime").attr("innerText"));
            //if ($('#lblCinvCode1').html.length > 36) {
            //    ($('#lblCinvCode1').css('font-size', '22px'))
            //    alert('22px');
            //} else {
            //    $("#lblCinvCode1").css('font-size', "22px");

            //}

            changeStyle('#lblCinvCode', '40px', '40px', 6);
            // alert("1234322");   
            //alert($('#lblCinvCode1').text());

        });

        
        var userName = window.screen.height;
        window.onload = function () {
            var userName = window.screen.height;
            document.getElementById("DivScroll").style.height = userName - 150 + 'px';
            $("th").attr("height", "50px");
            $("td").attr("height", +userName / 100 + "px");
            userName = $("td").height();
            // $("td").css('font-size', userName - 127 + "px");


            //显示不同的样式效果
            if ($('#lblLeftStatus').html() != null && $('#lblLeftStatus').html() != "出库中" && $('#lblLeftStatus').html() != "") {
                $('#divRK').attr("style", "display:");
                $('#divPick').attr("style", "display:none");
                if ($('#lblQty7').html().length > 0)
                    $('#yc').css("background-color", "#85527D");
                else $('#yc').css("background-color", "#0B80A3");
            }
            else if ($('#lblLeftStatus_P').html() != null && $('#lblLeftStatus_P').html() == "出库中") {
                $('#divRK').attr("style", "display:none");
                $('#divPick').attr("style", "display:");
            }
            else {
                $('#divRK').attr("style", "display:");
                $('#divPick').attr("style", "display:none");
                if ($('#lblQty7').html().length > 0)
                    $('#yc').css("background-color", "#85527D");
                else $('#yc').css("background-color", "#0B80A3");
            }
            //alert($('#lblCinvCode1').text());


            //changeStyle('#lblCinvCode1', '28px', '22px');

        }



        function changeStyle(lblName, MaxfontSize, MinFontSize, length) {
            //debugger;
            for (var i = 1; i <= length; i++) {
                var name = lblName + i;
                //alert(name);
                if ($(name).text().length > 46) {

                    $(name).text($(name).text().substr(0, 46));
                    // $(name).val($(name).text().substr(0, 48));

                    // alert($(name).text());
                    // alert($(name).val());

                    $(name).css('font-size', MinFontSize);


                } else {
                    if ($(name).text().length > 36) {
                        $(name).css('font-size', MinFontSize);

                    } else {
                        $(name).css('font-size', MaxfontSize);
                    }
                }
            }


            //if ($('#lblCinvCode2').text().length > 48) {
            //    alert($('#lblCinvCode2').text().substr(0, 46));
            //    $('#lblCinvCode2').text($('#lblCinvCode2').text().substr(0, 46));
            //    // $(name).val($(name).text().substr(0, 48));

            //    // alert($(name).text());
            //    // alert($(name).val());

            //    $('#lblCinvCode2').css('font-size', MinFontSize);

            //} else {
            //    if ($('#lblCinvCode2').text().length > 36) {
            //        $('#lblCinvCode2').css('font-size', MaxfontSize);

            //    } else {
            //        $('#lblCinvCode2').css('font-size', MinFontSize);
            //    }
            //}

        }


        function btnRefresh() {
            var line = $("#<%=hiddlineid.ClientID%>").val();
            var site = $("#<%=hiddsiteid.ClientID%>").val();
            $.ajax({
                type: "post",
                url: "FrmBase_ShowTVBySiteNew.aspx/GetData",
                data: "{ 'lineid': " + line + ",'siteid':" + site + " }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d.split('||');
                    //显示不同的样式效果
                    if (result != null && result.length == 19 || result[0] == null) {
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
                        $('#lblCinvCode7').html(result[17]);
                        $('#lblQty7').html(result[18]);

                        changeStyle('#lblCinvCode', '40px', '40px', 6);

                        if ($('#lblQty7').html().length > 0)
                            $('#yc').css("background-color", "#85527D");
                        else $('#yc').css("background-color", "#0B80A3");



                        $('#divRK').attr("style", "display:");
                        $('#divPick').attr("style", "display:none");
                    }
                    else if (result!= null && result.length == 16) {
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

                        changeStyle('#lblCinvCode1_P', '40px', '40px', 1);
                        changeStyle('#lblCinvCode2_P', '40px', '40px', 1);
                        changeStyle('#lblCinvCode3_P', '40px', '40px', 1);
                        changeStyle('#lblCinvCode4_P', '40px', '40px', 1);

                    }

                },
                error: function (err) {

                }
            });
        }
        setTimeout("btnRefresh()", $("#lblRefreshtime").attr("innerText"));
    </script>
    <title>电视显示电视显示</title>
    <style type="text/css">
        #DivScroll {
            word-wrap: break-word;
            word-break: break-all;
        }

        /*#mainTb {
            width: 95%;
            padding-left: 10px;
            color: white;
            height: 570px;
        }*/

        .tv-table {
            width: 100%;
            height: 100%;
            border: none;
            border-collapse: separate;
            border-spacing: 0px 10px;
            table-layout: auto;
            font-size:28px;
        }

            .tv-table td {
                border: 1px solid #2693CB;
            }



            /*.tv-table tr:last-child {
                background-color: #0B80A3;
                height: 20%;
            }*/


            .tv-table tr:nth-child(1) {
                background-color: #0C5B9E;
                /*height: 10%;*/
            }



            .tv-table tr:nth-child(2) {
                background-color: #0C5B9E;
                /*height: 10%;*/
            }

            .tv-table tr:nth-child(3) {
                background-color: #0B80A3;
                /*height: 20%;*/
            }

            .tv-table tr:nth-child(4) {
                background-color: #0B80A3;
                /*height: 20%;*/
            }

            .tv-table tr:nth-child(5) {
                background-color: #0B80A3;
            }

            /*.tv-table tr:nth-child(6) {
                height: 20%;
            }*/

            .tv-table tr td:nth-child(1) {
                width: 10%;
            }

        .TDTile {
            width: 10%;
            font-size: 50px;
            /*font-weight: bold;*/
            text-align: center;
        }

         .TDContent {
            text-align: left;
        }
         .tv-table tr td:nth-child(1) {
           width:10%;
        }
         .tv-table tr td:nth-child(4) {
           width:10%;
        }
           .tv-table tr td:nth-child(6) {
           width:10%;
        }
    </style>
</head>
<body style="margin:0px;">
    <form id="form1" runat="server"  >
        <asp:ScriptManager ID="smg1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upl1" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click" Style="display: none" />
                <div style="padding-top:0px; height: 710px; width: 1265px;" id="DivScroll">
                    <div align="center" style="background: url(../../Images/TV_Background.png) no-repeat;width: 1265px; height: 710px; background-size: 1265px 710px; ">
                        <div align="center" style="font-size: 56px; width: 100%; color: white; padding-top:13px ">
                            <table style="width: 100%; height:108px; ">
                                <tr>                                    
                                    <td style="width: 48%; text-align:right">
                                        <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                                    </td>
                                     <td style="width: 12%; text-align:center">
                                        
                                     </td>

                                    <td style="width: 15%; text-align:center;font-size:40px;color:red; ">
                                     <asp:Label ID="lblTitleMsg" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Label ID="storey_id" runat="server" Style="display: none"></asp:Label>
                        <asp:Label ID="lblRefreshtime" runat="server" Style="display: none"></asp:Label>

                        <div id="divRK" >
                            <table border="0" style="width: 100%;  color: white;  " >
                                <tr >
                                    <td >
                                        <table border="1" class="tv-table" style="width: 95%;  color: white;height:570px;margin-left: 2.5%; font-size:80px">
                                           <%-- <tr>
                                               
                                            </tr>--%>
                                            <tr style="height: 15%;">
                                                 <td rowspan="6" style="font-weight: bold; font-size: 100px; color: white; background-color: #1A6793; width: 10%;  text-align:center;">
                                                    <asp:Label ID="lblLeftStatus" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td class="TDTile">
                                                    <asp:Label ID="Label1"  runat="server" Text="站点"></asp:Label>
                                                </td>

                                                <td style="text-align: left;font-size: 40px" colspan="2">
                                                    
                                                    <asp:Label ID="lblSiteCode" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td class="TDTile">
                                                    <asp:Label ID="Label3"  runat="server" Text="单号"></asp:Label>
                                                </td>
                                                <td style="text-align: left;font-size: 40px" colspan="2" >
                                                    
                                                    <asp:Label ID="lblCticketCode" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                            </tr>
                                            <tr style="height: 15%;">

                                                <td class="TDTile">
                                                    <asp:Label ID="Label5"  runat="server" Text="板号"></asp:Label>
                                                </td>
                                                <td style="text-align: left; font-size: 40px" colspan="2">
                                                    
                                                    <asp:Label ID="lblPalletCode" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td class="TDTile">
                                                    <asp:Label ID="Label6"  runat="server" Text="储位"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 40%;font-size: 40px" colspan="2">
                                                    
                                                    <asp:Label ID="lblCpositionCode" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                            </tr>
                                            <tr style="height: 15%;">

                                                <td class="TDTile">
                                                    <asp:Label ID="Label7"  runat="server" Text="料号"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 25%;font-size: 40px">
                                                    
                                                    <asp:Label ID="lblCinvCode1" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 10%;font-size: 35px">
                                                    
                                                    <asp:Label ID="lblQty1" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td class="TDTile">
                                                    <asp:Label ID="Label8"  runat="server" Text="料号"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 25%;font-size: 40px">
                                                    
                                                    <asp:Label ID="lblCinvCode2" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 10%;font-size: 35px">
                                                    
                                                    <asp:Label ID="lblQty2" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                            </tr>
                                            <tr style="height: 15%;">

                                                <td class="TDTile">
                                                    <asp:Label ID="Label9"  runat="server" Text="料号"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 25%;font-size: 40px">
                                                    
                                                    <asp:Label ID="lblCinvCode3" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 10%;font-size: 35px">
                                                    
                                                    <asp:Label ID="lblQty3" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td class="TDTile">
                                                    <asp:Label ID="Label10"  runat="server" Text="料号"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 25%;font-size: 40px">
                                                    
                                                    <asp:Label ID="lblCinvCode4" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 10%;font-size: 35px">
                                                    
                                                    <asp:Label ID="lblQty4" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                            </tr>
                                            <tr style="height: 15%;">
                                                <td class="TDTile">
                                                    <asp:Label ID="Label11"  runat="server" Text="料号"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 25%;font-size: 40px">
                                                    
                                                    <asp:Label ID="lblCinvCode5" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 10%;font-size: 35px">
                                                    
                                                    <asp:Label ID="lblQty5" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                 <td class="TDTile">
                                                    <asp:Label ID="Label2"  runat="server" Text="料号"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 25%;font-size: 40px">
                                                    
                                                    <asp:Label ID="lblCinvCode6" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 10%;font-size: 35px">
                                                    
                                                    <asp:Label ID="lblQty6" runat="server" Text=""></asp:Label>
                                                
                                                </td>

                                            </tr>
                                            <tr style="height: 25%;" >
                                                <td class="TDTile" style="background-color:#85527D;">
                                                    <asp:Label ID="Label13"  runat="server" Text="异常"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 40%;font-size: 40px" colspan="5" id="yc">                                                   
                                                    <asp:Label ID="lblQty7" runat="server" Text="" ></asp:Label>
                                                    <asp:Label ID="lblCinvCode7" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <%--<td style="text-align: left; width: 10%">
                                                    
                                                
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divPick" >
                            <table class="mainTb" style="width:100%">
                                <tr>
                                    <td style="padding-left: 0px; height: 100%; text-align:center">
                                        <table border="1" class="tv-table" style="width: 95%;height: 570px; margin-left: 2.5%; color:white;">

                                           <%-- <tr>
                                                <td rowspan="6" style="font-weight: bold; font-size: 127px; height: 497px;">
                                                    <asp:Label ID="lblLeftStatus_P" runat="server" Text=""></asp:Label>
                                                </td>

                                            </tr>--%>
                                            <tr style="height: 80px;">
                                                 <td rowspan="5" style="font-weight: bold; font-size: 100px; text-align:center; ">
                                                    <asp:Label ID="lblLeftStatus_P" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="TDTile">
                                                    <asp:Label ID="lblpa"  runat="server" Text="栈板"></asp:Label>
                                                </td>

                                                <td style="text-align: left; width: 15%;font-size: 40px">
                                                    
                                                    <asp:Label ID="lblPallteCode_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td class="TDTile">
                                                    <asp:Label ID="Label14"  runat="server" Text="单号"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 25%;font-size: 40px">
                                                    
                                                    <asp:Label ID="lblCticketcode_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td class="TDTile">
                                                    <asp:Label ID="Label18"  runat="server" Text="储位"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 50%;font-size: 40px">
                                                    
                                                    <asp:Label ID="lblPostitionCode_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                            </tr>
                                            <tr style="height: 80px; background-color: #0B80A3;">

                                                <td style="text-align: left; width: 40%;font-size: 40px" colspan="2">
                                                    
                                                    <asp:Label ID="lblErpCode1_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 30%;font-size: 40px" colspan="3">
                                                    
                                                    <asp:Label ID="lblCinvCode1_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 50%;font-size: 40px">
                                                    
                                                    <asp:Label ID="lblQty1_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>

                                            </tr>
                                            <tr style="height: 80px;">

                                                <td style="text-align: left; width: 40%;font-size: 40px" colspan="2">
                                                    
                                                    <asp:Label ID="lblErpCode2_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 40%;font-size: 40px" colspan="3">
                                                    
                                                    <asp:Label ID="lblCinvCode2_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 40%;font-size: 40px">
                                                    
                                                    <asp:Label ID="lblQty2_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>

                                            </tr>
                                            <tr style="height: 80px;">

                                                <td style="text-align: left; width: 40%;font-size: 40px" colspan="2">
                                                    
                                                    <asp:Label ID="lblErpCode3_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 40%;font-size: 40px" colspan="3">
                                                    
                                                    <asp:Label ID="lblCinvCode3_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 40%;font-size: 40px">
                                                    
                                                    <asp:Label ID="lblQty3_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>

                                            </tr>
                                            <tr style="height: 80px;">

                                                <td style="text-align: left; width: 40%;font-size: 40px" colspan="2">
                                                    
                                                    <asp:Label ID="lblErpCode4_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 40%;font-size: 40px" colspan="3">
                                                    
                                                    <asp:Label ID="lblCinvCode4_P" runat="server" Text=""></asp:Label>
                                                
                                                </td>
                                                <td style="text-align: left; width: 40%;font-size: 40px">
                                                    
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
    <input type="hidden" id="hiddlineid" runat="server" />
    <input type="hidden" id="hiddsiteid" runat="server" />
</body>
</html>
