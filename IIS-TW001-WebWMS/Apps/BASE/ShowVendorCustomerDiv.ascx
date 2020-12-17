<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowVendorCustomerDiv.ascx.cs"
    Inherits="UserControls_ShowVendorCustomerDiv" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
<script src="../../Layout/js/pad.js" type="text/javascript"></script>
<script src="../../Layout/Js/jquery-1.11.3.min.js"></script>
<script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
<link href="../../Layout/Css/LG/Page.css" rel="Stylesheet" type="text/css" />
<script type="text/javascript">

    function setTab5(name, cursel, n) {
        $("#<%= hnTabIndex2.ClientID %>").val(cursel);
       // $("hTabIndex2").val = cursel;
        for (var i = 1; i <= n; i++) {
            //var menu = document.getElementById(name + i);
            //var con = document.getElementById("con_" + name + "_" + i);

            //document.getElementById("con_one_2").style.display = "block";
            if (i == cursel) {
                $("#con_tt_" + i).show();
                $("#tt" + i).addClass("hover");
            } else {
                $("#con_tt_" + i).hide();
                $("#tt" + i).removeClass("hover");
            }

            //
            //con.style.display = i == cursel ? "block" : "none";

        }
    }
    //function setTab1(name, cursel, n) {
    //    document.getElementById("ctl00_ContentPlaceHolderMain_showvendor16_hTabIndex3").value = cursel;
    //    for (i = 1; i <= n; i++) {
    //        var menu = document.getElementById(name + i);
    //        var con = document.getElementById("con_" + name + "_" + i);
    //        menu.className = i == cursel ? "hover" : "";
    //        con.style.display = i == cursel ? "block" : "none";

    //    }
    //}

    function refresh() {
        var indexT = $("#<%= hnTabIndex2.ClientID %>").val();
        //var indexT1 = document.getElementById("ctl00_ContentPlaceHolderMain_showvendor16_hTabIndex3").value;
         document.getElementById("tt" + indexT).click();

    }

    window.onload = function () {
        var indexT = $("#<%= hnTabIndex2.ClientID %>").val();
        //var indexT1 = document.getElementById("ctl00_ContentPlaceHolderMain_showvendor16_hTabIndex3").value;
        document.getElementById("tt" + indexT).click();
        //document.getElementById("one" + indexT1).click();
    }

</script>
<style type="text/css">
    .ajaxWebSearChBox {
        position: absolute;
        background-color: #0d1e4a;
        width: 400px;
        padding: 1px;
        display: none;
    }

    .ajaxWebSearchHeading {
        position: relative;
        background-color: #1162cc;
        font: bold 14px 宋体;
        height: 0;
        color: White;
        padding: 3px 0px 0px 2px;
    }

    .ajaxWebSearchCloseLink {
        position: absolute;
        right: 5px;
        text-decoration: none;
        color: Red;
        cursor: hand;
        font-size: large;
    }

    .ajaxWebSearchCloesLink:hover {
        color: Red;
    }

    .ajaxWebSearchResults {
        background-color: #d3e5fa;
        padding: 5px;
        margin: 5px 0 5px 0;
    }

    .ajaxWebSearchResult:div {
        text-align: center;
        font: bold 14px 宋体;
        color: #0a246a;
        margin: 5px 0 5px 0;
    }

    a.ajaxWebSearchLink {
        font: 12px 宋体;
        padding: 2px;
        display: block;
        color: #0a246a;
    }

    a.ajaxSearchLink:hover {
        color: White;
        background-color: #316ac5;
    }

    a.ajaxSeachLink:visited {
        color: Purple;
    }

    .tableFilter {
        border: 1px solid #ccc;
        padding: 2px;
        margin: 5px 0 5px 0;
    }

        .tableFilter input {
            border: 1px solid #ccc;
        }

    /*Tab1*/ 
    #lib_Tab1 {
        width: 100%;
        margin: 0 auto;
        padding: 0px;
        margin-top: 2px;
        margin-bottom: 2px;
    }
  

    .lib_tabborder {
        border: 1px solid #D5E3F0;
    }

    .lib_Menubox {
        height: 28px;
        line-height: 28px;
        position: relative;
    }



        .lib_Menubox ul {
            margin: 0px;
            padding: 0px;
            list-style: none;
            position: absolute;
            top: 3px;
            left: 0;
            margin-left: 10px;
            height: 25px;
            text-align: center;
            width: 1100px;
        }


        .lib_Menubox li {
            float: left;
            display: block;
            cursor: pointer;
            width: 115px;
            color: #949694;
            font-weight: bold;
            margin-right: 2px;
            height: 25px;
            line-height: 25px;
            background-color: #F0F3FA;
        }

            .lib_Menubox li.hover {
                padding: 0px;
                background: #fff;
                width: 115px;
                border-left: 1px solid #95C9E1;
                border-top: 1px solid #95C9E1;
                border-right: 1px solid #95C9E1;
                color: #739242;
                height: 25px;
                line-height: 25px;
            }

    .lib_Contentbox {
        clear: both;
        margin-top: 0px;
        border-top: none;
        min-height: 50px;
        text-align: center;
        padding-top: 8px;
    }
</style>

<div id="ajaxWebSearChComp" runat="server" class="ajaxWebSearChBox">

    <div class="ajaxWebSearchCloseLink" title="<%= Resources.Lang.Commona_Close%>" id="aCloseLink" onclick="document.all('<%=ajaxWebSearChComp.ClientID %>').style.display='none'"><%--关闭--%>
        <img src="../../Images/zs1.png" />
    </div>

    <div id="divHeading" style="margin:10px 0px 5px 0px; ">
        <div runat="server" id="div_Info" class="ajaxWebSearchResults">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hnTabIndex2" runat="server" Value="1" />
                    <%--<asp:HiddenField ID="hTabIndex3" runat="server" Value="1" />--%>
                    <div id="lib_Tab1" style="height: 100%; width: 100%">

                        <div class="lib_Menubox lib_tabborder">
                            <ul>
                                <li id="tt1" onclick="setTab5('tt',1,2)" class="hover">供应商</li>
                                <li id="tt2" onclick="setTab5('tt',2,2)">客户</li>

                            </ul>
                        </div>
                        <div class="lib_Contentbox lib_tabborder">
                            <div id="con_tt_1">
                                <table width="100%" class="tableFilter">
                                    <tr>
                                        <td width="10%" nowrap="nowrap"><%= Resources.Lang.FrmWAREHOUSEList_LEADERCODE%>：<%--供应商编码--%>
                                        </td>
                                        <td width="10%">
                                            <asp:TextBox ID="txtCode" runat="server" Width="104"></asp:TextBox>
                                        </td>
                                        <td width="10%" nowrap="nowrap"><%= Resources.Lang.FrmWAREHOUSEList_LEADER%>：<%--供应商名称--%>
                                        </td>
                                        <td width="20%">
                                            <asp:TextBox ID="txtName" runat="server" Width="104"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Lang, Common_View%>" OnClick="btnSearch_Click" CausesValidation="False" /><%--查看--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:GridView ID="gvReport" runat="server" AllowPaging="false" BackColor="White"
                                                BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                                                Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="gvReport_PageIndexChanging"
                                                OnSelectedIndexChanging="gvReport_SelectedIndexChanging" AutoGenerateSelectButton="True">
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <PagerStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="<%$ Resources:Lang, FrmWAREHOUSEList_LEADER%>" DataField="CVENDOR" /><%--供应商名称--%>
                                                    <asp:BoundField HeaderText="<%$ Resources:Lang, FrmWAREHOUSEList_LEADERCODE%>" DataField="CVENDORID" /><%--供应商编码--%>
                                                </Columns>
                                            </asp:GridView>

                                            <ul class="OneRowStyle">
                                                <li>
                                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                                       FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                                        AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                                    </webdiyer:AspNetPager>
                                                </li>
                                                <li>
                                                    <div id="aspNetNum1"><%= Resources.Lang.Base_Gong%> <%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                                                </li>
                                            </ul>

                                        </td>
                                    </tr>
                                    <%-- <tr>
                                                    <td colspan="3" align="center" style="color: #FF0000">
                                                        提示：请在企业名称中输入企业部分名称，模糊查询较快
                                                    </td>
                                                </tr>--%>
                                </table>
                            </div>
                            <div id="con_tt_2" style="display:none">
                                <table width="100%" class="tableFilter">
                                    <tr>
                                        <td width="10%" nowrap="nowrap"><%= Resources.Lang.FrmBASE_CLIENTList_lblCCLIENTID%>：<%--客户编码--%>
                                        </td>
                                        <td width="10%">
                                            <asp:TextBox ID="TxtCusNO" runat="server" Width="104"></asp:TextBox>
                                        </td>
                                        <td width="10%" nowrap="nowrap"><%= Resources.Lang.FrmBASE_CLIENTList_lblCCLIENTNAME%>：<%--客户名称--%>
                                        </td>
                                        <td width="20%">
                                            <asp:TextBox ID="TxtCusName" runat="server" Width="104"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Button ID="btnSearchCustomer" runat="server" Text="<%$ Resources:Lang, Common_View%>" OnClick="btnSearchCustomer_Click" CausesValidation="False" /><%--查看--%>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td colspan="5">
                                            <asp:GridView ID="GrdCustomer" runat="server" AllowPaging="false" BackColor="White"
                                                BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                                                Width="100%" AutoGenerateColumns="False" 
                                                OnSelectedIndexChanging="GrdCustomer_SelectedIndexChanging" AutoGenerateSelectButton="True">
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <PagerStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCCLIENTNAME%>" DataField="cclientname" /><%--客户名称--%>
                                                    <asp:BoundField HeaderText="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCCLIENTID%>" DataField="cclientid" /> <%--客户编码--%> 
                                                </Columns>
                                            </asp:GridView>

                                            <ul class="OneRowStyle">
                                                <li>
                                                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CssClass="pagination" LayoutType="Ul"
                                                        PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0"
                                                        CurrentPageButtonClass="active" OnPageChanged="AspNetPager2_PageChanged"
                                                         FirstPageText="<%$ Resources:Lang, Base_FirstPage %>" LastPageText="<%$ Resources:Lang, Base_EndPage %>" NextPageText="<%$ Resources:Lang, Base_NextPage %>" PrevPageText="<%$ Resources:Lang, Base_LastPage %>" ShowPageIndexBox="Never"
                                                        AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                                    </webdiyer:AspNetPager>
                                                </li>
                                                <li>
                                                    <div id="aspNetNum2"><%= Resources.Lang.Base_Gong%> <%=AspNetPager2.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                                                </li>
                                            </ul>

                                        </td>
                                    </tr>
                                </table>
                         
                        </div>
                        </div>
                        
                    </div>
                </ContentTemplate>
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger  ControlID="btnSearch" EventName="Click"  />--%>
                    <%--<asp:AsyncPostBackTrigger  ControlID="btnSearchCustomer" EventName="Click" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>


</div>

