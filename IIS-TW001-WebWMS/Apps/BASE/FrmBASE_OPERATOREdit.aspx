<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBASE_OPERATOREdit.aspx.cs"
    Inherits="BASE_FrmBASE_OPERATOREdit" Title="--<%$ Resources:Lang, FrmBASE_OPERATORList_Title01%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%--操作人--%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="ShowOperatorDiv.ascx" TagName="ShowOperatorDiv" TagPrefix="ucShowOperatorDiv" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script type="text/javascript">
    <!--
    function setTab(name, cursel, n) {
        document.getElementById("ctl00_ContentPlaceHolderMain_hfTabIndex").value = cursel;
        for (i = 1; i <= n; i++) {
            var menu = document.getElementById(name + i);
            var con = document.getElementById("con_" + name + "_" + i);
            menu.className = i == cursel ? "hover" : "";
            con.style.display = i == cursel ? "block" : "none";

        }
    }

    window.onload = function () {
        var indexT = document.getElementById("ctl00_ContentPlaceHolderMain_hfTabIndex").value;
        document.getElementById("one" + indexT).click();
    }
    //-->
    </script>
    <style type="text/css">
        body {
            padding: 0;
            font: 12px "宋体";
        }
        /*Tab1*/ #lib_Tab1 {
            width: 100%;
            margin: 0 auto;
            padding: 0px;
            margin-top: 2px;
            margin-bottom: 2px;
        }
        /*Tab2*/ #lib_Tab2 {
            width: 1000px;
            margin: 0 auto;
            padding: 0px;
            margin-top: 4px;
            margin-bottom: 5px;
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
                width: 240px;
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

        #topimg {
            margin: 0px auto;
            width: 1000px;
            margin-top: 3px;
        }

        #top img {
            width: 1000px;
            height: 55px;
            margin: 0px auto;
        }

        .divcontent {
            width: 1000px;
            margin: 0 auto;
            margin-top: 3px;
            margin-bottom: 4px;
            border: 1px solid #D5E3F0;
        }

            .divcontent .divAbuy {
                height: 50px;
                width: 200px;
                size: 16px;
                margin-top: 10px;
                margin-left: 10px;
            }

        #search {
            width: 1000px;
            margin: 0 auto;
            height: 50px;
            margin-top: 3px;
            border: 1px solid #D5E3F0;
        }

            #search .sControl {
                margin-top: 10px;
                margin-left: 5px;
                height: 25px;
                width: 580px;
            }

                #search .sControl .leftDate {
                    float: left;
                    width: 420px;
                }

                #search .sControl .rightBtn {
                    float: right;
                    width: 160px;
                }

            #search .btncss {
                width: 60px;
                height: 30px;
            }

            #search .btnSearch {
                width: 63px;
                height: 22px;
                background: url( "../Templets/newimages/search.gif" );
                border: 0;
                cursor: pointer;
            }

        .btnExpert {
            width: 63px;
            height: 22px;
            background: url( "../Templets/newimages/export.gif" );
            border: 0;
            cursor: pointer;
        }

        .dzfw {
            padding: 5px 15px;
            border: 1px solid #d5e3f0;
            overflow: hidden;
            width: 968px;
            margin: 10px 0;
        }

        .dzfw_title {
            float: left;
            margin: 15px 8px 0 0;
        }

        .dzfw_list {
            float: left;
            width: 448px;
        }

            .dzfw_list img {
                float: left;
                margin: 5px 5px 5px 12px;
            }

        .dzfw_title1 {
            float: left;
            margin: 2px 8px 0 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBaseCONFIG_DEdit_Title02%>-&gt;<%= Resources.Lang.FrmBASE_OPERATORList_Title01%><%--基礎資料-&gt;操作人--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <ucShowOperatorDiv:ShowOperatorDiv ID="ucBaseShowOperatorDiv" runat="server" />
    <div id="lib_Tab1">
        <div class="lib_Menubox lib_tabborder">
            <ul>
                <li id="one1" onclick="setTab('one',1,2)" class="hover"><%= Resources.Lang.FrmBASE_OPERATOREdit_one1%></li>
                <%--基本信息--%>
                <li id="one2" onclick="setTab('one',2,2)"><%= Resources.Lang.FrmBASE_OPERATOREdit_one2%></li>
                <%--交付部门--%>
            </ul>
        </div>
        <div class="lib_Contentbox lib_tabborder">
            <div id="con_one_1">
                <asp:HiddenField ID="hfTabIndex" runat="server" Value="1" />
                <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
                    border="0">
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                runat="server" id="TabMain">
                                <tr style="display: none">
                                    <td colspan="6">
                                        <style type="text/css">
                                            span.requiredSign {
                                                color: #FF0000;
                                                font-weight: bold;
                                                position: relative;
                                                left: -2px;
                                                top: 2px;
                                            }
                                        </style>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <span class="requiredSign">*</span>
                                        <asp:Label ID="lblCACCOUNTID" runat="server" Text="<%$ Resources:Lang, FrmBASE_OPERATORList_lblCACCOUNTID%>"></asp:Label>： <%--操作人编码--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtUserNo" runat="server" CssClass="NormalInputText" Width="95%"
                                            MaxLength="20"></asp:TextBox>

                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <span class="requiredSign">*</span>
                                        <asp:Label ID="lblCOPERATORNAME" runat="server" Text="<%$ Resources:Lang, FrmBASE_OPERATORList_lblCOPERATORNAME%>"></asp:Label>： <%--操作人姓名--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCOPERATORNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                            MaxLength="20"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="rfvtxtCOPERATORNAME" runat="server" ControlToValidate="txtCOPERATORNAME"
                                            ErrorMessage="<%$ Resources:Lang, FrmBASE_OPERATOREdit_rfvtxtCOPERATORNAME%>" Display="None"> <%--请填写操作人姓名!--%>
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCALIAS" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCALIAS%>"></asp:Label>： <%--助记码--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCALIAS" runat="server" CssClass="NormalInputText" Width="95%"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCDEPARTMENT" runat="server" Text="<%$ Resources:Lang, FrmBASE_OPERATORList_lblCDEPARTMENT%>"></asp:Label>：<%--部门--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCDEPARTMENT" runat="server" CssClass="NormalInputText" Width="95%"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCDUTY" runat="server" Text="<%$ Resources:Lang, FrmBASE_OPERATORList_lblCDUTY%>"></asp:Label>：<%--职务--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCDUTY" runat="server" CssClass="NormalInputText" Width="95%"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCPHONE" runat="server" Text="<%$ Resources:Lang, FrmWAREHOUSEList_lblLEADERPHONE%>"></asp:Label>：<%--电话--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCPHONE" runat="server" CssClass="NormalInputText" Width="95%"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCSHIFT" runat="server" Text="<%$ Resources:Lang, FrmBASE_OPERATORList_lblCSHIFT%>"></asp:Label>：<%--班别--%>
                                    </td>
                                    <td style="width: 21%">
                                        <asp:TextBox ID="txtCSHIFT" runat="server" CssClass="NormalInputText" Width="95%"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCERPCODE" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_lblCERPCODE%>"></asp:Label>： <%--ERP编码--%>
                                    </td>
                                    <td style="width: 21%">
                                        <asp:TextBox ID="txtCERPCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <span class="requiredSign">*</span>
                                        <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                                    </td>
                                    <td style="width: 21%">
                                        <asp:DropDownList ID="dplCSTATUS" Width="95%" runat="server">
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvtxtCSTATUS" runat="server" ControlToValidate="dplCSTATUS"
                                            ErrorMessage="<%$ Resources:Lang, Common_SelectStatus%>" Display="None"> 
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblDCREATETIME" runat="server" Text="<%$ Resources:Lang, Common_CreateDate%>"></asp:Label>： <%--创建日期--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtDCREATETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                            ToolTip="<%$ Resources:Lang, Common_Format%>" Enabled="false"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCCREATEOWNER" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：<%--创建人--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtCCREATEOWNER" runat="server" CssClass="NormalInputText" Width="95%"
                                            MaxLength="20" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label3%>"></asp:Label>
                                        <%--修改人--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtupdateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label4%>"></asp:Label>
                                        <%--修改时间--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtupdatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%"></td>
                                    <td style="width: 20%"></td>
                                    <td class="InputLabel" style="width: 13%"></td>
                                    <td style="width: 20%"></td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：<%--备注--%>
                                    </td>
                                    <td colspan="5" style="width: 20%">
                                        <asp:TextBox ID="txtCMEMO" runat="server" CssClass="NormalInputText" Width="95%"
                                            MaxLength="20" Height="35px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="36"></asp:TextBox>
                                    </td>
                                    <td colspan="2">&nbsp;
                                    </td>
                                    <td colspan="2">&nbsp;
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td colspan="6">
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
                            <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="true" runat="server"
                                DisplayMode="BulletList" ShowMessageBox="true" />
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                                Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                            &nbsp;&nbsp; &nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="ButtonDel"
                                OnClick="btnDelete_Click" Text="<%$ Resources:Lang, Common_Delete%>" CausesValidation="false" Visible="False" />
                            <%--删除--%>	
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="con_one_2" style="display: none">
                <script type="text/javascript">
                    function CheckSelArea() {
                        var number = 0;
                        var controls = document.getElementById("<%=grdBASE_DEPARTMENT.ClientID %>").getElementsByTagName("input");

                        for (var i = 0; i < controls.length; i++) {
                            var e = controls[i];
                            if (e.type != "CheckBox") {
                                if (e.checked == true) {
                                    number = number + 1;
                                }
                            }
                        }
                        var selType = document.getElementById("ctl00_ContentPlaceHolderMain_ddlAllType2").value;


                        return true;
                    }

                    function SelIDCancelAll() {
                        document.GetElementById("ctl00_ContentPlaceHolderMain_cboxAllArea").checked = false;
                    }
                </script>
                <table id="Table1" style="height: 100%; width: 95%">
                    <tr valign="top">
                        <td valign="top">
                            <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                                id="Table2">
                                <tr>
                                    <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                        height="11" alt="" runat="server" id="Img1" />
                                        <%= Resources.Lang.Common_JSCeria%>
                                    </th>
                                    <th style="border-left-width: 0px" align="right">
                                        <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                                        </script>
                                        <img id="img2" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                            onclick="CollapseCondition('../../');return false;" />
                                    </th>
                                </tr>
                                <tr style="display: none;">
                                    <td colspan="6">
                                        <style type="text/css">
                                            span.requiredSign {
                                                color: #FF0000;
                                                font-weight: bold;
                                                position: relative;
                                                left: -15px;
                                                top: 2px;
                                            }
                                        </style>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="lblPALLETID" runat="server" Text="<%$ Resources:Lang, FrmBASE_OPERATOREdit_lblPALLETID%>"></asp:Label>：<%--部门编码--%>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:TextBox ID="txtDepartNo" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmBASE_OPERATOREdit_Label1%>"></asp:Label>：<%--部门名称--%>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:TextBox ID="txtDepartName" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="InputLabel" style="width: 13%">
                                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmBASE_OPERATOREdit_Label2%>"></asp:Label>：<%--选择类型--%>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:DropDownList ID="ddltype" runat="server" Width="95%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td colspan="6">
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
                                        <asp:HiddenField ID="HidFUser" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="text-align: right;">
                                        <asp:Button ID="btnSearch_D" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch_D_Click"><%--查询--%>
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top" align="center">
                            <asp:Button ID="btnSave_D" runat="server" CssClass="ButtonSave" Text="<%$ Resources:Lang, Common_btnSave%>" OnClick="btnSave_D_Click" /><%--保存--%>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack_D" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 300px; overflow-x: scroll; width: 100%" id="Div1">
                                <asp:GridView ID="grdBASE_DEPARTMENT" runat="server" AllowPaging="True" BorderColor="Teal"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="true"
                                    OnRowDataBound="grdBASE_DEPARTMENT_RowDataBound" CssClass="Grid" PageSize="50"
                                    OnDataBinding="grdBASE_DEPARTMENT_DataBinding">
                                    <PagerSettings Visible="False" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <RowStyle HorizontalAlign="Left" Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="<input id='chkSelect' type='checkbox' onclick='SelectAll4Grid(this)' />">
                                            <ControlStyle BorderWidth="0px" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid"
                                                    BorderWidth="0px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DEPARTMENTNO" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_OPERATOREdit_lblPALLETID%>"><%--部门编码--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DEPARTMENTNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBASE_OPERATOREdit_Label1%>"><%--部门名称--%>
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" Width="80%" />
                                        </asp:BoundField>
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
                                        <div><%= Resources.Lang.Base_Gong%><%=AspNetPager1.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                                    </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <script type="text/javascript">
                        function ChangeDivWidth() {
                            //document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
                        }
                        window.onresize = ChangeDivWidth;
                        ChangeDivWidth();

                    </script>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
