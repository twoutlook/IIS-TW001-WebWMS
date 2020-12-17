<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Base_TVShow_Warehouse.aspx.cs" Inherits="Apps_BASE_Base_TVShow_Warehouse" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../../Layout/Js/jquery-1.4.1.min.js"></script>
    <script src="../../scripts/echarts.min.js"></script>

    <title>电视显示</title>
    <style type="text/css">
        .auto-style1
        {
            height: 25px;
        }

        .tv-table
        {
            /*width: 1210px;*/
            height: 100px;
            border: none;
            border-collapse: collapse;
            border-spacing: 0px 10px;
            table-layout: auto;
        }

        .pan_title
        {
            height: 50px;
            width: 8%;
            border: none;
            border-collapse: collapse;
            border-spacing: 0px 10px;
            table-layout: auto;
        }

        .pan_code
        {
            height: 50px;
            width: 5%;
            border: none;
            border-collapse: collapse;
            border-spacing: 0px 2px;
            table-layout: auto;
            text-align: left;
        }
        /*.auto-style3
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            height: 120px;
            width: 132px;
            border-collapse: collapse;
            border-spacing: 0px 2px;
            table-layout: auto;
        }*/
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            window.setInterval(genData, 5000)// $("#lblRefreshtime").attr("innerText"));
            setTimeout("document.getElementById(\"btnRefresh\").click()", 5000);
        });
        function generateChart(data1, data2) {


            var dom = document.getElementById("container");
            var myChart = echarts.init(dom);
            var TotalspaceRate = data1 == null ? '0' : data1;
            var TotalPosUseRate = data2 == null ? '0' : data2;
            option = {
              
                title: {
                    text: '',
                    subtext: '',
                    left: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: '{b}:{c}%'
                },

                //legend: {
                //    //orient: 'vertical',
                //    //left: 'left',
                //    //data: ['储位使用率', '空间使用率']
                //},
                series: [
                    {
                        name: '',
                        type: 'pie',
                        radius: '90%',
                        center: ['65%', '55%'],
                        data: [
                            { value: TotalPosUseRate, name: '储位使用率' },//itemStyle: { color: "red" } itemStyle: { color: 'yellow' } 
                            { value: TotalspaceRate, name: '空间使用率' }//, itemStyle: { color: "yellow" }, itemStyle: { color: '#742894' }
                        ],
                        label: {
                            normal: {
                                show: true,
                                position: 'inner', //标签的位置
                                textStyle: {
                                    fontWeight: 100,
                                    fontSize: 16, //文字的字体大小
                                    color: 'black'
                                },
                                formatter: '{c}%'
                            }
                        },
                        emphasis: {
                            itemStyle: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: 'rgba(0, 0, 0, 0.5)'
                            }
                        }
                    }
                ]
            };

            myChart.setOption(option);
        }
        function genData() {
            //$.ajax({
            //    type: "post",
            //    url: "Base_TVShow_Warehouse.aspx/GetData",
            //    data: "{ 'strCRANE': '1'}",
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (data) {
            var data = $('#lbl_str_divshow').attr("innerText");

            var result = data.split('||');

            $('#lblXianZhi').html(result[0] == null ? '0' : result[0]);
            $('#lblShiYong').html(result[1] == null ? '0' : result[1]);
            $('#lblBaoFei').html(result[2] == null ? '0' : result[2]);
            $('#lblDongJie').html(result[3] == null ? '0' : result[3]);
            generateChart(result[4], result[5]);
            //        $('#Div1').html(result[6] == null ? '0' : result[6]);


            //        //generateChart(data);
            //        //generateChart2(data);

            //    },
            //    error: function (err) {

            //    }
            //});

        };



    </script>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="smg1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upl1" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click" Style="display: none; " />
                <asp:Label ID="lblRefreshtime" runat="server" Style="display: none"></asp:Label>
                <asp:Label ID="lbl_str_divshow" runat="server" Style="display: none" Text=""></asp:Label>

                <div style="padding-top: 0px; height: 100%; width: 1265px;" id="DivScroll">
                    <div align="center" style="background: url(../../Images/TV_Background.png) repeat-y; width: 1265px; height:  100%; ">
                        <div align="left" style="font-size: 60px; width: 100%; color: white">
                            <table style="width: 100%; height: 108px; text-align: center">
                                <tr>
                                    <td style="width: 100%;">
                                        <asp:Label ID="lblTitle" runat="server" Text="仓库使用率可视化"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div id="divWareHouse">
                            <table class="mainTb" style="width: 1210px; height: 100%; color: white;">
                                <tr>
                                    <td style="padding-left: 0px; height: 100%;">
                                        <table border="1" style="width: 100%" class="tv-table">
                                            <tr style="height: 15%;" >                                               
                                                <td class="pan_title" >
                                                    <span style="background-color:#717975;">&nbsp&nbsp&nbsp&nbsp</span>
                                                    <asp:Label ID="Label1" runat="server" Text="闲置中："></asp:Label>
                                                </td>
                                                <td class="pan_code" >
                                                    <asp:Label ID="lblXianZhi" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_title" >
                                                     <span style="background-color:#DE8028;">&nbsp&nbsp&nbsp&nbsp</span>
                                                    <asp:Label ID="Label3" runat="server" Text="使用中："></asp:Label>
                                                </td>
                                                <td class="pan_code" >
                                                    <asp:Label ID="lblShiYong" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_title" >
                                                     <span style="background-color:#D42B40;">&nbsp&nbsp&nbsp&nbsp</span>
                                                    <asp:Label ID="Label5" runat="server" Text="报废中："></asp:Label>
                                                </td>
                                                <td class="pan_code" >
                                                    <asp:Label ID="lblBaoFei" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="pan_title" >
                                                     <span style="background-color:#EBF5F4;">&nbsp&nbsp&nbsp&nbsp</span>
                                                    <asp:Label ID="Label6" runat="server" Text="冻结中："></asp:Label>
                                                </td>
                                                <td class="pan_code" >
                                                    <asp:Label ID="lblDongJie" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td  style="border:0"  rowspan="2">
                                                    <table border="0" class="tv-table" style="width: 100%">
                                                        <tr>
                                                            <td style="width: 100%">
                                                                <div id="container" style="height: 100%;"></div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>

                                            </tr>
                                              <tr style="height: 15%;">

                                                <td class="pan_title">
                                                    <asp:Label ID="Label7" runat="server" Text="仓库："></asp:Label>
                                                </td>
                                                <td class="pan_title">
                                                    <asp:Label ID="lblWareHouse" runat="server" Text="全部"></asp:Label>
                                                </td>
                                                <td class="pan_title">
                                                    <asp:Label ID="Label8" runat="server" Text="线别："></asp:Label>
                                                </td>
                                                <td class="pan_title" >
                                                    <asp:Label ID="lblCrane" runat="server" Text="全部"></asp:Label>
                                                </td>

                                            </tr>
                                            <tr style="height: 15%;">

                                                <td  colspan="10" style="width:100%; text-align:center;" >
                                                    <table style="width:100%; font-size:0.1px" > <%--style="height: 552px; width: 100%; border: 1px; margin-top: 10px; margin-bottom: 15px"--%>
                                                        <tr>
                                                            <td class="auto-style1" style="text-align: left;">
                                                                <asp:Label ID="lblRefeshtext" runat="server" Text="每5分钟刷新一次"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:100%">
                                                                <div id="Div1" runat="server"></div>
                                                                <asp:Label ID="aaa" runat="server"><%=str_divshow%></asp:Label>
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>

