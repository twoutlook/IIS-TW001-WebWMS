<%@ Page Title="SN条码管理" Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master"
    AutoEventWireup="true" CodeFile="FrmBar_SNManagementEdit.aspx.cs" Inherits="Apps_BAR_FrmBar_SNManagementEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../BASE/ShowPARTDiv.ascx" TagName="ShowPARTDiv" TagPrefix="uc1" %>
<%@ Register Src="../BASE/ShowVendorCustomerDiv.ascx" TagName="ShowVENDORDiv" TagPrefix="uc2" %>
<%@ Register Src="../BASE/ShowBASE_CARGOSPACEBSDiv.ascx" TagName="ShowBASE_CARGOSPACEBSDiv"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/Js/Help.js" type="text/javascript"></script>
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript">
        //function setTab(name, cursel, n) {
        //    document.getElementById("ctl00_ContentPlaceHolderMain_hfTabIndex").value = cursel;
        //    for (i = 1; i <= n; i++) {
        //        var menu = document.getElementById(name + i);
        //        var con = document.getElementById("con_" + name + "_" + i);
        //        menu.className = i == cursel ? "hover" : "";
        //        con.style.display = i == cursel ? "block" : "none";

        //    }
        //}

        //window.onload = function () {
        //    var Session = document.getElementById("ctl00_ContentPlaceHolderMain_session").value;
        //    //新增
        //    if (Session == "0") {
        //        var indexT = document.getElementById("ctl00_ContentPlaceHolderMain_hfTabIndex").value;
        //        document.getElementById("one" + indexT).click();
        //    }
        //    //19码修改
        //    else if (Session == "1") {
        //        document.getElementById("ctl00_ContentPlaceHolderMain_hfTabIndex").value = Session;
        //        document.getElementById("one1").click();
        //        document.getElementById("one1").style.display = "none";
        //        document.getElementById("one2").style.display = "none";
        //    }
        //    //16码修改
        //    else if (Session == "2") {
        //        document.getElementById("ctl00_ContentPlaceHolderMain_hfTabIndex").value = Session;
        //        document.getElementById("one2").click();
        //        document.getElementById("one1").style.display = "none";
        //        document.getElementById("one2").style.display = "none";
        //    }
        //}

       // ChangeTDStyle("ctl00_ContentPlaceHolderMain_TabMain");


        //$(document).ready(function () {
        //    $("btnSave").click(function () {
        //       // alert('test');
        //        $("#btnSave").attr("disabled", "disabled")
               
        //    });
        //});
            
    </script>
    <style type="text/css">
        body
        {
            padding: 0;
            font: 12px "宋体";
        }
        /*Tab1*/#lib_Tab1
        {
            width: 100%;
            margin: 0 auto;
            padding: 0px;
            margin-top: 2px;
            margin-bottom: 2px;
        }
        /*Tab2*/#lib_Tab2
        {
            width: 1000px;
            margin: 0 auto;
            padding: 0px;
            margin-top: 4px;
            margin-bottom: 5px;
        }
        .lib_tabborder
        {
            border: 1px solid #D5E3F0;
        }
        .lib_Menubox
        {
            height: 28px;
            line-height: 28px;
            position: relative;
        }
        .lib_Menubox ul
        {
            margin: 0px;
            padding: 0px;
            list-style: none;
            position: absolute;
            top: 3px;
            left: 0;
            margin-left: 10px;
            height: 25px;
            text-align: center;
            width: 240px;
        }
        .lib_Menubox li
        {
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
        .lib_Menubox li.hover
        {
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
        .lib_Contentbox
        {
            clear: both;
            margin-top: 0px;
            border-top: none;
            min-height: 50px;
            text-align: center;
            padding-top: 8px;
        }
        #topimg
        {
            margin: 0px auto;
            width: 1000px;
            margin-top: 3px;
        }
        #top img
        {
            width: 1000px;
            height: 55px;
            margin: 0px auto;
        }
        .divcontent
        {
            width: 1000px;
            margin: 0 auto;
            margin-top: 3px;
            margin-bottom: 4px;
            border: 1px solid #D5E3F0;
        }
        .divcontent .divAbuy
        {
            height: 50px;
            width: 200px;
            size: 16px;
            margin-top: 10px;
            margin-left: 10px;
        }
        #search
        {
            width: 1000px;
            margin: 0 auto;
            height: 50px;
            margin-top: 3px;
            border: 1px solid #D5E3F0;
        }
        #search .sControl
        {
            margin-top: 10px;
            margin-left: 5px;
            height: 25px;
            width: 580px;
        }
        #search .sControl .leftDate
        {
            float: left;
            width: 420px;
        }
        #search .sControl .rightBtn
        {
            float: right;
            width: 160px;
        }
        #search .btncss
        {
            width: 60px;
            height: 30px;
        }
        #search .btnSearch
        {
            width: 63px;
            height: 22px;
            background: url(    "../Templets/newimages/search.gif" );
            border: 0;
            cursor: pointer;
        }
        .btnExpert
        {
            width: 63px;
            height: 22px;
            background: url(    "../Templets/newimages/export.gif" );
            border: 0;
            cursor: pointer;
        }
        .dzfw
        {
            padding: 5px 15px;
            border: 1px solid #d5e3f0;
            overflow: hidden;
            width: 968px;
            margin: 10px 0;
        }
        .dzfw_title
        {
            float: left;
            margin: 15px 8px 0 0;
        }
        .dzfw_list
        {
            float: left;
            width: 448px;
        }
        .dzfw_list img
        {
            float: left;
            margin: 5px 5px 5px 12px;
        }
        .dzfw_title1
        {
            float: left;
            margin: 2px 8px 0 0;
        }
        .InputLabel2 {
            text-align:right !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HolderTitle" runat="Server">
    条码管理-&gt;SN条码管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" EnablePartialRendering="true"
        EnablePageMethods="true" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    
    <%--<uc1:ShowPARTDiv ID="showcinvcode19" runat="server" />--%>
    <%--<uc2:ShowVENDORDiv ID="showvendor19" runat="server" />--%>
    <uc1:ShowPARTDiv ID="showcinvcode16" runat="server" />
    <uc2:ShowVENDORDiv ID="showvendor16" runat="server" />
    <uc3:ShowBASE_CARGOSPACEBSDiv ID="showcargospace19" runat="server" />
    <%--<div id="lib_Tab1">--%>
        <div class="lib_Menubox lib_tabborder">
            <ul>
                <%--<li id="one1" onclick="setTab('one',1,2)" class="hover">19码SN</li>--%>
                <li id="one2" onclick="setTab('one',2,2)">75码SN</li>
            </ul>
        </div>
        <asp:HiddenField ID="session" runat="server" Value="0" />
        <div class="lib_Contentbox lib_tabborder;" >

            <div id="con_one_2">
                <table id="Table1" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
                    border="0">
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                runat="server" id="Table2">
                                <tr style="display: none">
                                    <td colspan="4" class="auto-style1">
                                        <style type="text/css">
                                            span.requiredSign
                                            {
                                                color: #FF0000;
                                                font-weight: bold;
                                                position: relative;
                                                left: -15px;
                                                top: 2px;
                                            }
                                            .auto-style1
                                            {
                                                height: 25px;
                                            }
                                        </style>
                                    </td>
                                </tr>
                                <tr>
                                     <td class="InputLabel2" style="width: 10%;">
                                        <asp:Label ID="Label27" runat="server" Text="S/N编码："></asp:Label>
                                    </td>
                                    <td style="width: 30%;">
                                        <asp:TextBox ID="txtSNCode" runat="server" CssClass="NormalInputText" Width="95%"
                                            onkeydown="return false;" MaxLength="75" Enabled="false">
                                        </asp:TextBox>
                                    </td>
                                    <td><asp:Label runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="InputLabel2" style="width: 10%">
                                        <asp:Label ID="Label21" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="Label22" runat="server" Text="料号："></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtCinvcode16" runat="server" CssClass="NormalInputText" Width="95%"
                                            onkeydown="return false;" MaxLength="20">
                                        </asp:TextBox>
                                    </td>
                                     <td><asp:Label ID="Label1" runat="server" Text="[7-36码]"></asp:Label></td>

                                </tr>
                                <tr>
                                       <td class="InputLabel2" style="width: 10% ;">
                                        <asp:Label ID="Label23" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="Label24" runat="server" Text="品名："></asp:Label>
                                    </td>
                                    <td style="width: 30%;">
                                        <asp:TextBox ID="txtCinvName16" runat="server" CssClass="NormalInputText" Width="95%"
                                            MaxLength="100" Enabled="False"></asp:TextBox>
                                    </td>
                                     <td><asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>

                                </tr>
                                <tr>
                                    <td class="InputLabel2" style="width: 10%">
                                        <asp:Label ID="Label25" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="Label26" runat="server" Text="DateCode：" ></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtDateCode16" runat="server" MaxLength="6" CssClass="NormalInputText"  Width="95%" >
                                        </asp:TextBox>
                                    </td>
                                     <td><asp:Label ID="Label3" runat="server" Text="[1-6码]"></asp:Label></td>
                                    
                                </tr>
                                <tr>
                                    <td class="InputLabel2" style="width: 10%;">
                                      <%--  <asp:Label ID="Label29" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>--%>
                                        <asp:Label ID="Label30" runat="server" Text="供应商编码/客户编号："></asp:Label>
                                    </td>
                                    <td style="width: 30%;">
                                        <asp:TextBox ID="txtVendor16" runat="server" CssClass="NormalInputText" Width="95%"
                                            onkeydown="return false;"></asp:TextBox>
                                    </td>
                                      <td><asp:Label ID="Label4" runat="server" Text="[62-71码]"></asp:Label></td>
                                </tr>
                                <tr>
                                     <td class="InputLabel2" style="width: 10%;">
                                       <%-- <asp:Label ID="Label31" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>--%>
                                        <asp:Label ID="Label32" runat="server" Text="供应商名称："></asp:Label>
                                    </td>
                                    <td style="width: 30%;">
                                        <asp:TextBox ID="txtVendorName16" runat="server" CssClass="NormalInputText" Width="95%">
                                        </asp:TextBox>
                                    </td>
                                     <td><asp:Label ID="Label5" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="InputLabel2" style="width: 10%;">
                                      <%--  <asp:Label ID="Label28" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>--%>
                                        <asp:Label ID="Label42" runat="server" Text="PO/SO："></asp:Label>
                                    </td>
                                    <td style="width: 30%;">
                                        <asp:TextBox ID="txtPo16" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td><asp:Label ID="Label9" runat="server" Text="[37-61码]"></asp:Label></td>
                                    
                                </tr>
                                <tr>
                                    <td class="InputLabel2" style="width: 10%;">
                                        <asp:Label ID="Label7" runat="server" Text="每箱数量："></asp:Label>
                                    </td>
                                    <td style="width: 30%;">
                                       <asp:TextBox ID="txtPkNumn" runat="server" CssClass="NormalInputText" Width="95%" >
                                        </asp:TextBox> 
                                         <asp:RegularExpressionValidator ID="revtxtBoxNum" runat="server" ValidationExpression="^[1-9][0-9]{0,5}.[0-9]{2}$"
                                                 ControlToValidate="txtPkNumn" ErrorMessage="请填写有效的包装数量!正确的格式是：数字" Display="None"></asp:RegularExpressionValidator>
                                    </td>
                                    <td></td>
                                </tr>
                                  <tr runat="server" id="trSumNum"  visible="false" >
                                    <td class="InputLabel2" style="width: 10%;">
                                        <asp:Label ID="Label12" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="Label33" runat="server" Text="总数量："></asp:Label>
                                    </td>
                                    <td style="width: 30%;">
                                        <asp:TextBox ID="txtSumNum" runat="server" CssClass="NormalInputText" Width="95%">
                                        </asp:TextBox>
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[1-9][0-9]{0,5}.[0-9]{2}$"
                                                 ControlToValidate="txtSumNum" ErrorMessage="请填写有效的总数量!正确的格式是：数字" Display="None"></asp:RegularExpressionValidator>
                                        <%--<%--<asp:RegularExpressionValidator ID="reftxtQty16" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,5}(\.[0-9]{1,3}){0,1}"
                                ControlToValidate="txtQty16" ErrorMessage="请填写有效的数量!正确的格式是：最多5位整数，最多3位小数"
                                Display="None"> 
                                            </asp:RegularExpressionValidator>--%>
                                    </td>
                                      <td><asp:Label ID="Label6" runat="server" Text=""></asp:Label></td>
                                    
                                </tr>

                                <tr runat="server" id="trNum" >
                                    <td class="InputLabel2" style="width: 10%;">
                                        <asp:Label ID="Label37" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="Label38" runat="server" Text="单条码数量："></asp:Label>
                                    </td>
                                    <td style="width: 30%;">
                                        <asp:TextBox ID="txtQty16" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^[1-9][0-9]{0,5}.[0-9]{2}$"
                                                 ControlToValidate="txtQty16" ErrorMessage="请填写有效的单条码数量!正确的格式是：数字" Display="None"></asp:RegularExpressionValidator>
                                        <%--<%--<asp:RegularExpressionValidator ID="reftxtQty16" runat="server" ValidationExpression="[+\-]{0,1}[0-9]{1,5}(\.[0-9]{1,3}){0,1}"
                                ControlToValidate="txtQty16" ErrorMessage="请填写有效的数量!正确的格式是：最多5位整数，最多3位小数"
                                Display="None"> 
                                            </asp:RegularExpressionValidator>--%>
                                    </td>
                                    <td><asp:Label ID="Label11" runat="server" Text=""></asp:Label></td>
                                    
                                </tr>

                                <tr>
                                  <td class="InputLabel2" style="width: 10%;">
                                       
                                        <asp:Label ID="Label20" runat="server" Text="起始流水号：" ></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBegNum16" runat="server" Width="95%" CssClass="NormalInputText" MaxLength="4" Enabled="false"  >
                                        </asp:TextBox>
                                    </td>
                                      <td><asp:Label ID="Label10" runat="server" Text="[72-75码]"></asp:Label></td>
                                </tr>

                                <tr style="display:none">
                                   <td class="InputLabel2" style="width: 20%">
                                        <asp:Label ID="Label45" runat="server" Text="批次数量："></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMultinum16" runat="server" Width="95%" CssClass="NormalInputText">
                                        </asp:TextBox>
                                    </td> 
                                    <td><asp:Label ID="Label8" runat="server" Text="[72-75码]"></asp:Label></td> 

                                </tr>
                               
                                <tr style="display: none">
                                    <td colspan="4">
                                        <script type="text/javascript" language="javascript">
                                            //调整“行”为不能换行的。为什么不在设计期就设置其样式为不换行的呢，因为一旦设置，输入控件莫名其妙地看不见了。
                                            function ChangeTDStyle(inputTableID) {
                                                var tabMain = document.getElementById(inputTableID);
                                                if (tabMain == null) return;
                                                for (var i = 0; i < tabMain.rows.length; i++) {
                                                    var tr = tabMain.rows[i];
                                                    if (tr == null) continue;
                                                    for (var j = 0; j < tr.cells.length; j++) {
                                                        var td = tr.cells[j];
                                                        if (td == null) continue;
                                                        if (td.className == "" || td.className == null) {
                                                            td.style.whiteSpace = "nowrap";
                                                            td.style.borderRightWidth = "0px";
                                                        }
                                                    }
                                                }
                                            }
                                            ChangeTDStyle("ctl00_ContentPlaceHolderMain_TabMain"); 
                                        </script>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" align="center">
                            <%--<asp:ValidationSummary ID="ValidationSummary2" ShowSummary="true" runat="server"
                                DisplayMode="BulletList" ShowMessageBox="true" />--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    <%--</div>--%>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td  style="width:20%"></td>
            <td style="text-align:left; padding:15px 0px;" >
                <asp:Button ID="btnPrint" runat="server" CssClass="ButtonPrint" Text="打印" 
                    OnClick="btnPrint_Click" Visible="False" />
                &nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" Text="保存" OnClick="btnSave_Click"   OnClientClick="this.value='保存';this.disabled=true; " />
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="返回" CausesValidation="false" />
            </td>
             <td></td>
        </tr>
    </table>
    
</asp:Content>
