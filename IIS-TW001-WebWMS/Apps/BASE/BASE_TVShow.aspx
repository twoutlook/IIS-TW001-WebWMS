<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BASE_TVShow.aspx.cs" Inherits="Apps_BASE_BASE_TVShow" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../Layout/Js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">     
        $(document).ready(function () {
            window.setInterval("btnRefresh()", 10000);
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
            var lineID = $("#line_ID").attr("innerText");
            var siteID = $("#site_ID").attr("innerText");            
            $.ajax({
                type: "post",
                url: "BASE_TVShow.aspx/GetData",
                data: "{'LineID': '" + lineID + "', 'SiteID': '" + siteID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var result = data.d.split('||');
                    $('#labPallatCodeCount').html(result[0] == null ? '无数据' : result[0]);
                    $('#lblLeftStatus').html(result[1] == null ? '无数据' : result[1]);
                    $('#labERPCode').html(result[2] == null ? '无数据' : result[2]);
                    $('#labBillCode').html(result[3] == null ? '无数据' : result[3]);
                    $('#labPositionCode').html(result[4] == null ? '无数据' : result[4]);
                    $('#labPallatCode').html(result[5] == null ? '无数据' : result[5]);
                    $('#labCinvCode1').html(result[6] == null ? '无数据' : result[6]);
                    $('#labOutBillQty1').html(result[7] == null ? '无数据' : result[7]);
                    $('#labOutJHQty1').html(result[8] == null ? '无数据' : result[8]);
                    $('#labOutFKQty1').html(result[9] == null ? '无数据' : result[9]);
                    $('#labCinvCode2').html(result[10] == null ? '无数据' : result[10]);
                    $('#labOutBillQty2').html(result[11] == null ? '无数据' : result[11]);
                    $('#labOutJHQty2').html(result[12] == null ? '无数据' : result[12]);
                    $('#labOutFKQty2').html(result[13] == null ? '无数据' : result[13]);
                    $('#labCinvCode3').html(result[14] == null ? '无数据' : result[14]);
                    $('#labOutBillQty3').html(result[15] == null ? '无数据' : result[15]);
                    $('#labOutJHQty3').html(result[16] == null ? '无数据' : result[16]);
                    $('#labOutFKQty3').html(result[17] == null ? '无数据' : result[17]);
                    $('#labCinvCode4').html(result[18] == null ? '无数据' : result[18]);
                    $('#labOutBillQty4').html(result[19] == null ? '无数据' : result[19]);
                    $('#labOutJHQty4').html(result[20] == null ? '无数据' : result[20]);
                    $('#labOutFKQty4').html(result[21] == null ? '无数据' : result[21]);
                    $('#labOutFB1').html(result[22] == null ? '无数据' : result[22]);
                    $('#labOutFB2').html(result[23] == null ? '无数据' : result[23]);
                    $('#labOutFB3').html(result[24] == null ? '无数据' : result[24]);
                    $('#labOutFB4').html(result[25] == null ? '无数据' : result[25]);
                    $('#labMessage').html(result[26] == null ? '无数据' : result[16]);                   
                },
                error: function (err) {

                }
            });      
        }
        setTimeout("btnRefresh()", 10000);
    </script>
    <title>电视显示</title>
    <style type="text/css">
        .auto-style1
        {
            height: 20px;
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
                            <table style="background-color: blue; width: 100%;height: 106px; text-align: center">
                                <tr>
                                    <td style="width: 30%">
                                        <span style="margin-left: 20px">
                                            <asp:Label ID="labTitle" runat="server" Text="百亨"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="width: 40%">
                                        <asp:Label ID="labLineID" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="labSiteID" runat="server" Text=""></asp:Label>
                                    </td>

                                    <td style="width: 30%">
                                        <asp:Label ID="labPallatCodeCount" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Label ID="line_ID" runat="server" Style="display: none"></asp:Label>
                        <asp:Label ID="site_ID" runat="server" Style="display: none"></asp:Label>

                        <table class="mainTb" style="width: 100%">
                            <tr>
                                <td style="padding-left: 0px;height: 100%;">
                                    <table border="1" cellspacing="0" style="width: 100%">

                                        <tr>
                                            <td rowspan="4" style="background-color: #5DF8F0; font-weight: bold; font-size:127px;height: 566px;">
                                                <asp:Label ID="lblLeftStatus" runat="server" Text="">入库中</asp:Label>
                                            </td>

                                        </tr>
                                        <tr style="height: 20%;">

                                            <td style="text-align: center; width: 10%">
                                                <font size="16px"><asp:Label ID="Label1"  runat="server" Text="订单"></asp:Label></font>
                                            </td>


                                            <td style="text-align: left; width: 90%">
                                                <div>
                                                    <font size="6px">
                                            <asp:Label ID="Label3"  runat="server" Text="ERP单号:"></asp:Label>
                                            <asp:Label ID="labERPCode" runat="server" Text=""></asp:Label>
                                            </font>
                                                    <font size="6px">
                                            <asp:Label ID="Label7"  runat="server" Text="单号:"></asp:Label>
                                            <asp:Label ID="labBillCode" runat="server" Text=""></asp:Label>
                                            </font>
                                                </div>
                                                <div>
                                                    <font size="6px">
                                            <asp:Label ID="Label9"  runat="server" Text="库位:"></asp:Label>
                                            <asp:Label ID="labPositionCode" runat="server" Text=""></asp:Label>
                                            </font>
                                                    <font size="6px">
                                            <asp:Label ID="Label12"  runat="server" Text="箱号:"></asp:Label>
                                            <asp:Label ID="labPallatCode" runat="server" Text=""></asp:Label>
                                            </font>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr style="height: 50%;">
                                            <td style="text-align: center;">
                                                <font size="16px">
                                                  <asp:Label ID="Label2" runat="server" Text="物料"></asp:Label>&nbsp;
                                         </font>
                                            </td>
                                            <td style="text-align: left;">
                                                <table border="1" cellspacing="0" aria-hidden="False" aria-readonly="False" style="height: 100%; width: 100%;">
                                                    <tr style="height: 50%">
                                                        <td style="border-top: none; border-left: none; width: 50%">
                                                            <div>
                                                                <font size="6">
                                                                <asp:Label ID="labCinvCode1" runat="server" Text=""></asp:Label>
                                                               </font>

                                                            </div>
                                                            <div>
                                                                <font size="6">
                                                                <asp:Label ID="labOutBillQty1" runat="server" Text=""></asp:Label>
                                                             
                                                                <asp:Label ID="labOutJHQty1" runat="server" Text=""></asp:Label>
                                                            
                                                                <asp:Label ID="labOutFKQty1" runat="server" Text=""></asp:Label>
                                                         
                                                              </font>
                                                            </div>


                                                        </td>
                                                        <td style="border-top: none; border-right: none; width: 50%">
                                                            <div>
                                                                <font size="6">
                                                                 <asp:Label ID="labCinvCode2" runat="server" Text=""></asp:Label>
                                                                     </font>
                                                            </div>
                                                            <div>
                                                                <font size="6">
                                                                <asp:Label ID="labOutBillQty2" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="labOutJHQty2" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="labOutFKQty2" runat="server" Text=""></asp:Label>
                                                                      </font>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 50%">
                                                        <td style="border-bottom: none; border-left: none;">
                                                            <div>
                                                                <font size="6">
                                                                <asp:Label ID="labCinvCode3" runat="server" Text=""></asp:Label>
                                                                     </font>
                                                            </div>
                                                            <div>
                                                                <font size="6">
                                                                <asp:Label ID="labOutBillQty3" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="labOutJHQty3" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="labOutFKQty3" runat="server" Text=""></asp:Label>
                                                                     </font>

                                                            </div>
                                                        </td>
                                                        <td style="border-bottom: none; border-right: none;">
                                                            <div>
                                                                <font size="6">
                                                                <asp:Label ID="labCinvCode4" runat="server" Text=""></asp:Label>
                                                                    </font>
                                                            </div>
                                                            <div>
                                                                <font size="6">
                                                                <asp:Label ID="labOutBillQty4" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="labOutJHQty4" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="labOutFKQty4" runat="server" Text=""></asp:Label>
                                                                     </font>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                        </tr>
                                        <tr style="height: 30%;">
                                            <td style="text-align: center;">
                                                <font size="16">
                                                出库<br />分布
                                               </font>
                                            </td>
                                            <td style="text-align: left;">
                                                <table border="1" cellspacing="0" aria-hidden="False" aria-readonly="False" style="height: 100%; width: 100%;">
                                                    <tr>
                                                        <td style="border-top: none; border-bottom: none; border-left: none; width: 50%; font-size: 20px">
                                                            <div>
                                                                 <font size="5">
                                                                <asp:Label ID="labOutFB1" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="labOutFB2" runat="server" Text=""></asp:Label>
                                                                     </font>
                                                            </div>
                                                            <div>
                                                                 <font size="5">
                                                                <asp:Label ID="labOutFB3" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="labOutFB4" runat="server" Text=""></asp:Label>
                                                                     </font>
                                                            </div>
                                                        </td>
                                                        <td id="isMessageTD" runat="server" style="border-top: none; border-bottom: none; border-right: none; width: 50%;">
                                                            <div>
                                                                <font size="16">
                                                               <asp:Label ID="Label4"  runat="server" Text="异常:"></asp:Label>
                                                            </font>
                                                            </div>
                                                            <div>
                                                                <font size="5">
                                                                <asp:Label ID="labMessage" runat="server" Text=""></asp:Label>
                                                                    </font>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
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
