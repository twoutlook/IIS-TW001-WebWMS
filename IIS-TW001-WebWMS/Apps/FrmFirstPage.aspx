<%@ Page language="c#" Inherits="FrmFirstPage" CodeFile="FrmFirstPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<HTML>

	<HEAD>
		<title>FrmFirstPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link href="../Layout/Css/LG/FirstPage.css" rel="stylesheet" type="text/css" />
        <script src="../Layout/Js/jquery-1.4.1.js" type="text/javascript"></script>
        <script src="../Layout/Js/jquery-1.4.1.min.js" type="text/javascript"></script>
        <link rel="Stylesheet" type="text/css" href="../Layout/TabControl/dhtmlxtabbar.css"/>
        <link href="../Layout/multi/css/cn/backImage.css" rel="Stylesheet" type="text/css" id="imgUrl" runat="server" />
        <style type="text/css">
            .badge {
                display: inline-block;
                min-width: 10px;
                padding: 3px 7px;
                font-size: 12px;
                font-weight: 400;
                color: #fff;
                line-height: 1;
                vertical-align: baseline;
                white-space: nowrap;
                text-align: center;
                background-color: #2196f3;
                border-radius: 3px;
                float:right;
            }
            .part1_zhong {
                height:105px;
            }
            .part1_zhong_table td {
                width:235px;
                height:23px;
            }
            .td_left {
                padding-left:15px;
            }
            .td_right {
                padding-right:15px;
            }
            .part1_zhong_datarow_wenzi {
                margin-left:5px;
                width:160px;
                overflow:hidden;
                font-size:12px;
            }
            .part1_zhong_datarow_icon {
                width:15px;
                height:15px;
            }
            .part2_zhong_datarow_icon {
                width:15px;
                height:15px;
            }
            .part3_zhong_datarow_icon {
                width:15px;
                height:15px;
            }
            .part4_zhong_datarow_icon {
                width:15px;
                height:15px;
            }

            .part1_tou_left {
                margin-left:10px;
                height:35px;
                width:43px
            }
            .part2_tou_left {
                margin-left:10px;
                height:35px;
                width:43px
            }
            .part3_tou_left {
                margin-left:10px;
                height:35px;
                width:43px
            }
            .part4_tou_left {
                margin-left:10px;
                height:35px;
                width:43px
            }

            .first_title_img {
                height:35px;
                width:100px;
                float:left;
                margin-left:5px;
            }

        </style>
	</HEAD>
	<body>
    <div id="a_tabbar" style="width:1100px;height:100%"/>
		<form method="post" runat="server">
			<FONT face="宋体"></FONT>
		</form>
            <div id="10000" class="partall">
                <div id="part1" class="part1">

                    <div id="part1_tou" class="part1_tou" style="height:35px;">
                         <div id="part1_tou_left" class="part1_tou_left">
                         </div>

                         <div id="part1_tou_right" class="first_title_img">
                             <span class="first_page_inbill"></span>
                         </div>
                    </div>
                   
                    <div id="part1_zhong" class="part1_zhong">
                        <div class="part1_zhongxian">--------------------------------------------------------
                        </div>
                        <table class="part1_zhong_table">                            
                            <tr>
                                <td class = "td_left" pageName="FrmINASNList.aspx">
                                    <div class="part1_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">  
                                    <a href="#"onclick="jump('1220','<%= Resources.Lang.FrmUnionPalletList_MSG2%>','../../apps/RD/FrmINASNList.aspx?Cstatus=0')"><%= Resources.Lang.FrmUnionPalletList_MSG2%></a></div><%--入库通知单--%>
                                    <span class="badge"><%=P_InNoticeCount%></span>
                                </td>                           
                                   <td class = "td_right" pageName="FrmINBILLList.aspx">
                                    <div class="part1_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                    <a href="#" onclick="jump('1256','<%= Resources.Lang.FrmINBILLList_MSG9%>','../../apps/RD/FrmINBILLList.aspx?Cstatus=0')"><%= Resources.Lang.FrmINBILLList_MSG9%></a></div><%--入库单--%>
                                    <span class="badge"><%=P_InCount%></span>
                                </td>
                            </tr>
                            <tr>
                                <td class = "td_left" pageName="FrmChangeAuditByInAsn.aspx">
                                    <div class="part1_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                    <a href="#" onclick="jump('1356','<%= Resources.Lang.FrmFirstPage_Title1356%>','../../apps/RD/FrmChangeAuditByInAsn.aspx')"><%= Resources.Lang.FrmFirstPage_Title1356%></a></div><%--入库通知变更单审核--%>
                                    <span class="badge"><%=P_InNoticeChangeVerifyCount%></span>
                                </td>
                                <td class="td_right" pageName="FrmINASSITList.aspx">
                                    <div class="part1_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                    <a href="#" onclick="jump('1223','<%= Resources.Lang.FrmINASSITList_Title2%>','../../apps/RD/FrmINASSITList.aspx')"> <%= Resources.Lang.FrmINASSITList_Title2%></a></div><%--上架指引--%>
                                    <span class="badge"><%=P_AddedGuideCount%></span>
                                 </td>
                            </tr>
                            <tr>
                             
                                <td></td>
                            </tr>
                        </table>
                    </div>

                    <div id="part1_wei" class="part1_wei">
                    </div>
                </div>

                <div id="part2" class="part2">

                    <div id="Div2" class="part2_tou" style="height:35px;">
                         <div id="Div3" class="part2_tou_left">
                         </div>

                         <div id="Div4" class="first_title_img">
                             <span class="first_page_outbill"></span>
                         </div>
                       
                    </div>
                   
                    <div id="Div5" class="part1_zhong">
                        <div class="part1_zhongxian">--------------------------------------------------------
                        </div>
                        <table class="part1_zhong_table">
                            <tr>
                                <td class = "td_left" pageName="FrmOUTASNList.aspx">
                                    <div class="part2_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                    <a href="#" onclick="jump('1237','<%= Resources.Lang.FrmOUTASN_Report_title1%>','../../apps/OUT/FrmOUTASNList.aspx?Cstatus=0')"><%= Resources.Lang.FrmOUTASN_Report_title1%></a></div><%--出库通知单--%>
                                    <span class="badge"><%=P_OutNoticeCount%></span>
                                </td>
                                <td class="td_right" pageName="FrmChangeByOutAsn.aspx">
                                    <div class="part2_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                    <a href="#" onclick="jump('1351','<%= Resources.Lang.FrmFirstPage_Title1351%>','../../apps/OUT/FrmChangeByOutAsn.aspx')"><%= Resources.Lang.FrmFirstPage_Title1351%></a></div><%--出库通知变更单--%>
                                    <span class="badge"><%=P_OutNoticeChangeCount%></span>
                                </td>
                               
                            </tr>
                            <tr>
                                <td class = "td_left" pageName="FrmOutChangeAsnAudit.aspx">
                                    <div class="part2_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                    <a href="#" onclick="jump('1344','<%= Resources.Lang.FrmFirstPage_Title1344%>','../../apps/OUT/FrmOutChangeAsnAudit.aspx')"><%= Resources.Lang.FrmFirstPage_Title1344%></a></div><%--出库通知变更单审核--%>
                                    <span class="badge"><%=P_OutNoticeChangeVerifyCount%></span>
                                </td>
                                <td class="td_right" pageName="FrmOUTASSITList.aspx">
                                    <div class="part2_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                    <a href="#" onclick="jump('1238','<%= Resources.Lang.FrmOUTASSITList_Menu_PageName%>','../../apps/OUT/FrmOUTASSITList.aspx')"><%= Resources.Lang.FrmOUTASSITList_Menu_PageName%></a></div><%--拣货指引--%>
                                    <span class="badge"><%=P_MinusGuideCount%></span>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class = "td_left" pageName="FrmOUTBILLList.aspx">
                                    <div class="part2_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                    <a href="#" onclick="jump('1258','<%= Resources.Lang.FrmLogSystemList_Msg02%>','../../apps/OUT/FrmOUTBILLList.aspx?Cstatus=0')"><%= Resources.Lang.FrmLogSystemList_Msg02%></a></div><%--出库单--%>
                                    <span class="badge"><%=P_OutCount%></span>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>

                    </div>
                    <div id="Div6" class="part1_wei">
                    </div>
                </div>

                <div id="part3" class="part3">

                    <div id="Div8" class="part3_tou" style="height:35px;">
                         <div id="Div9" class="part3_tou_left">
                         </div>

                         <div id="Div10" class="first_title_img">
                             <span class="first_page_stock"></span>
                         </div>
                       
                    </div>
                   
                    <div id="Div11" class="part1_zhong">
                        <div class="part1_zhongxian">--------------------------------------------------------
                        </div>
                        <table class="part1_zhong_table">
                            <tr>
                                <td class = "td_left" pageName="FrmALLOCATEList.aspx">
                                    <div class="part3_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                    <a href="#" onclick="jump('1226','<%= Resources.Lang.FrmALLOCATEList_Title1%>','../../apps/ALLOCATE/FrmALLOCATEList.aspx?Cstatus=0')"><%= Resources.Lang.FrmALLOCATEList_Title1%></a></div><%--调拨单--%>
                                    <span class="badge"><%=P_AllocateCount%></span>
                                </td>
                                <td class="td_right" pageName="FrmALLOCATE_Audit_Mail.aspx">
                                    <div class="part3_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                    <a href="#" onclick="jump('1301','<%= Resources.Lang.FrmALLOCATE_Audit_Mail_Title01%>','../../apps/ALLOCATE/FrmALLOCATE_Audit_Mail.aspx')"><%= Resources.Lang.FrmALLOCATE_Audit_Mail_Title01%></a></div><%--调拨单审核--%>
                                    <span class="badge">0</span>
                                </td>
                            </tr>
                            <tr>
                                <td class = "td_left" pageName=""> 
                                    <div class="part3_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi"><%= Resources.Lang.FrmFirstPage_Title01%></div><%--调拨变更--%>
                                    <span class="badge">0</span>
                                </td>
                                <td class="td_right" pageName="">
                                    <div class="part3_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi"><%= Resources.Lang.FrmFirstPage_Title02%></div><%--调拨变更审核单--%>
                                   <span class="badge">0</span>
                                </td>
                            </tr>
                            <tr>
                                <td class = "td_left" pageName="FrmSTOCK_CHECKBILLList1.aspx">
                                    <div class="part3_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                    <a href="#" onclick="jump('1230','<%= Resources.Lang.FrmFirstPage_Title1230%>','../../apps/STOCK/FrmSTOCK_CHECKBILLList1.aspx?Cstatus=0')"><%= Resources.Lang.FrmFirstPage_Title1230%></a></div><%--循环盘点单--%>
                                    <span class="badge"><%=P_CycleCheckCount%></span>
                                </td>
                                <td class="td_right" pageName="FrmSTOCK_CHECKBILLList.aspx">
                                    <div class="part3_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                    <a href="#" onclick="jump('1229','<%= Resources.Lang.FrmFirstPage_Title1229%>','../../apps/STOCK/FrmSTOCK_CHECKBILLList.aspx')"><%= Resources.Lang.FrmFirstPage_Title1229%></a></div><%--物理盘点单--%>
                                    <span class="badge"><%=P_PhysicalcheckCount%></span>
                                 </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                   
                    </div>
                    <div id="Div12" class="part1_wei">
                    </div>
                </div>

                <div id="part4" class="part4" style="display:none">

                    <div id="Div14" class="part4_tou" style="height:35px;">
                         <div id="Div15" class="part4_tou_left">
                         </div>

                         <div id="Div16" class="first_title_img">
                             <span class="first_page_renwu"></span>
                         </div>
                       
                    </div>
                   
                    <div id="Div17" class="part1_zhong">
                        <div class="part1_zhongxian">--------------------------------------------------------
                        </div>
                        <table class="part1_zhong_table">
                           <tr>
                                <td class = "td_left" pageName="TaskList.aspx">
                                    <div class="part4_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                        <a href="#" onclick="jump('1372','<%= Resources.Lang.FrmFirstPage_Title1372%>','../../apps/Task/TaskList.aspx')"><%= Resources.Lang.FrmFirstPage_Title1372%></a><%--任务指派--%>
                                    </div>
                                    <span class="badge" onclick="jump('1372','<%= Resources.Lang.FrmFirstPage_Title1372%>','../../apps/Task/TaskList.aspx')"><%=P_TaskCount2%></span><%--任务指派--%>
                                </td>
                                <td class="td_right" pageName="TaskList_Exe.aspx">
                                    <div class="part4_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi">
                                        <a href="#" onclick="jump('1373','<%= Resources.Lang.FrmFirstPage_Title1373%>','../../apps/Task/TaskList_Exe.aspx')"><%= Resources.Lang.FrmFirstPage_Title1373%></a><%--任务执行--%>
                                    </div>
                                    <span class="badge" onclick="jump('1373','<%= Resources.Lang.FrmFirstPage_Title1372%>','../../apps/Task/TaskList_Exe.aspx')"><%=P_TaskCount%></span><%--任务指派--%>
                                </td>
                               
                            </tr>
                            <tr>
                                <td class = "td_left" >
                                    <div class="part4_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi"><%= Resources.Lang.FrmFirstPage_Title03%></div><%--任务变更单--%>
                                    <span class="badge">0</span>
                                </td>
                                <td class="td_right">
                                    <div class="part4_zhong_datarow_icon"></div>
                                    <div class="part1_zhong_datarow_wenzi"><%= Resources.Lang.FrmFirstPage_Title04%></div><%--任务变更单审核--%>
                                    <span class="badge">0</span>
                                </td>
                            </tr> 
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>

                    </div>
                    <div id="Div18" class="part1_wei">
                    </div>

                </div>

            </div>
	</body>
    <script type="text/javascript" language="javascript">
        function jump(id, title, link) {
            parent.top.ifrContent.NewTabPage(id, title, link);
        }

        //JS卡控，首页没有权限的连接，不用显示出来.
        $(document).ready(function () {
            var menuStr = RightMenuList.authorizeMenu;
            $(this).find("td[class='td_left'],[class='td_right']").each(function () {
                var pageName = $(this).attr("pageName");
                if (pageName != null && pageName != undefined && pageName != "") {
                    var hasRight = menuStr.toLowerCase().indexOf(pageName.toLowerCase());//不区分大小写
                    if (hasRight == -1) {
                        $(this).html("");//没权限，不显示
                    }
                    var isAspx = pageName.toLowerCase().indexOf(".aspx");
                    if (isAspx == -1) {
                        $(this).html("");//没权限，不显示
                    }
                }
                else {
                    $(this).html("");//没权限，不显示
                }
            });
        });
    </script>
</HTML>
