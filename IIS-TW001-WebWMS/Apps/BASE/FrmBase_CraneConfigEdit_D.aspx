<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBase_CraneConfigEdit_D.aspx.cs"  Inherits="BASE_FrmBase_CraneConfigEdit_D" Title="--<%$ Resources:Lang, FrmBase_CraneConfigEdit_Title01%>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>
<%--Crane详情--%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl" runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
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
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmBase_CraneConfig_Msg09%>-&gt;<%= Resources.Lang.FrmBase_AGV_D_Title01%><%--立库线别管理-&gt;站点管理--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>

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
                                    left: -15px;
                                    top: 2px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblCraneID" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_lblCraneID%>"></asp:Label>：<%--线别编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCraneID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50" Enabled="False"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtCraneID" runat="server" ControlToValidate="txtCraneID"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_CraneConfigEdit_rfvtxtCraneID%>" Display="None"> </asp:RequiredFieldValidator><%--请填写线别编号!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblSiteID" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label5%>"></asp:Label>：<%--站点编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSiteID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtSiteID" runat="server" ControlToValidate="txtSiteID"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_AGV_D_rfvtxtSiteID%>" Display="None">
                            </asp:RequiredFieldValidator><%--请填写站点编号!--%>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblSiteName" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label10%>"></asp:Label>： <%--站点名称--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtSiteName" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="30"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtSiteName" runat="server" ControlToValidate="txtSiteName"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_AGV_D_rfvtxtSiteName%>" Display="None"> </asp:RequiredFieldValidator><%--请填写站点名称!--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblSiteTypes" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label11%>"></asp:Label>：<%--站点类型--%>
                        </td>
                        <td style="width: 20%">

                            <asp:DropDownList ID="dplSiteTypes" runat="server" Width="95%">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvtxtSiteTypes" runat="server" ControlToValidate="dplSiteTypes"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_AGV_D_rfvddlAGVSITETYPE%>" Display="None"> </asp:RequiredFieldValidator>
                            <%--请填写站点类型!--%>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblDefulSite" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfigEdit_DEFULSITE%>"></asp:Label>：<%--默认站点--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dplDefulSite" runat="server" Width="95%">
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="rfvtxtDefulSite" runat="server" ControlToValidate="dplDefulSite"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_CraneConfigEdit_D_rfvtxtDefulSite%>" Display="None"> <%--请填写默认站点!--%>
                            </asp:RequiredFieldValidator>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <span style="position: relative; left: -25px; top: 2px; color: #FF0000; font-weight: bold;">*</span>
                            <asp:Label ID="lblFormat" runat="server" Text="<%$ Resources:Lang, FrmBASE_PARTList_CSPECIFICATIONS%>"></asp:Label>：<%--规格--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlFormat" runat="server" Width="95%">
                                <%--<asp:ListItem Value="1">110</asp:ListItem>
                                <asp:ListItem Value="2">115</asp:ListItem>--%>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="rfvddlFormat" runat="server" ControlToValidate="ddlFormat"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_AGV_D_rfvddlFormat%>" Display="None"> <%--请填写规格!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <span style="position: relative; left: -25px; top: 2px; color: #FF0000; font-weight: bold;">*</span>
                            <asp:Label ID="lblCSTATUS" runat="server" Text="<%$ Resources:Lang, Common_CSTATUS%>"></asp:Label>：<%--状态--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="dplCSTATUS" runat="server" Width="95%">
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="rfvtxtCSTATUS" runat="server" ControlToValidate="dplCSTATUS"
                                ErrorMessage="<%$ Resources:Lang, Common_SelectStatus%>" Display="None"> 
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createowner%>"></asp:Label>：
                            <%--创建人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtcreateuser" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfig_Label19%>"></asp:Label>：<%--楼层--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtStorey" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" ></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label3%>"></asp:Label>：
                            <%--修改人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtupdateuser" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False"></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTEdit_Label4%>"></asp:Label>：
                            <%--修改时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtupdatetime" runat="server" CssClass="NormalInputText" Width="95%" Enabled="False"></asp:TextBox>

                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_createtime%>"></asp:Label>：<%--创建时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtcreatetime" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>


                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Lang, FrmBase_AGV_Label7%>"></asp:Label>：<%--停用时间--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDISCONTINUETIME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <span class="requiredSign">*</span>
                            <asp:Label ID="lblSorting" runat="server" Text="<%$ Resources:Lang, FrmBase_CraneConfigEdit_D_lblSorting%>"></asp:Label>：<%--入库排序方式--%>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlSorting" runat="server" Width="95%">
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="rfvddlSorting" runat="server" ControlToValidate="ddlSorting"
                                ErrorMessage="<%$ Resources:Lang, FrmBase_CraneConfigEdit_D_rfvddlSorting%>" Display="None">  <%--请填写入库排序方式!--%>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Lang, FrmBase_AGV_Label1%>"></asp:Label>：<%--停用人--%>
                        </td>
                        <td style="width: 21%">
                            <asp:TextBox ID="txtDISCONTINUEUSER" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="50" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblRemark" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：<%--备注--%>
                        </td>
                        <td colspan="5" style="width: 20%">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="100" Height="33px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                        </td>

                    </tr>
                    <tr style="display: none">
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang, FrmBASE_CLIENTList_ID%>" Visible="False"></asp:Label>： <%--编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"
                                Visible="False"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblIDS" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_ID%>" Visible="False"></asp:Label>：<%--主表编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtIDS" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"
                                Visible="False"></asp:TextBox>
                        </td>
                        <td colspan="2">
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
            <td style="padding:15px 0px; text-align:center;">
                <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" /><%--返回--%>
            </td>
        </tr>
    </table>

    <table id="Table1" style="height: 100%; width: 100%">
        <tr valign="top" style="display: none;">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                            height="11" alt="" runat="server" id="titleImg" />
                            <%= Resources.Lang.Common_JSCeria%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%= Resources.Lang.Common_ZheDie%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr style="display: none">
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
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_ID%>"></asp:Label>：<%--主表编号--%>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <td colspan="1">&nbsp;
                        </td>
                        <td colspan="1">&nbsp;
                        </td>
                        <td colspan="1">&nbsp;
                        </td>
                        <td colspan="1">&nbsp;
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
        <tr id="trOutBill" runat="server">
            <td align="left" colspan="6"></td>
        </tr>
        <tr valign="top">
            <td style="width: 13%; text-align: left; padding-bottom:15px;" colspan="6" >
                <asp:Button ID="btnAddTrade" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>"></asp:Button><%--新增--%>
                <asp:Button ID="Button2" runat="server" CssClass="ButtonDo" Text="<%$ Resources:Lang, FrmBaseDocuReason_List_btnQy%>" OnClientClick="return CheckQY();" OnClick="btnSave1_Click"></asp:Button><%--启用--%>
                <asp:Button ID="btn_BlockUp" runat="server" CssClass="ButtonDel" Text="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_btnUnable%>" OnClientClick="return  CheckDel();" OnClick="btn_BlockUp_Click"></asp:Button><%--作废--%>
                <asp:Button ID="btnSearch1" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" OnClick="btnSearch1_Click" CausesValidation="false" Style="display: none;"></asp:Button>
                <%--查询--%>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <div style="height: 300px; overflow-x: scroll; width: 100%;" id="DivScrolls">

                    <asp:GridView ID="grdBASE_trande" runat="server" AllowPaging="True" BorderColor="Teal"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="true"
                        OnRowDataBound="grdBASE_trande_RowDataBound" CssClass="Grid" PageSize="15">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                        <PagerStyle HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="" Wrap="False" />


                        <Columns>
                            <asp:TemplateField HeaderText="<input id='chkSelect' type='checkbox' onclick='SelectAll4Grid(this)' />">
                                <ControlStyle BorderWidth="0px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid"
                                        BorderWidth="0px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>


                            <asp:BoundField DataField="TYPEID" DataFormatString="" HeaderText="TYPEID" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="T" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Type%>"><%--类型--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />

                                <ItemStyle HorizontalAlign="center" Wrap="False" Width="" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CERPCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_lblCERPCODE%>"><%--类型编码--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TYPENAME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmBase_InOutTypeStatusList_lblTYPENAME%>"><%--类型名称--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ENABLE" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CSTATUS%>"><%--状态--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, Common_Edit%>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, Common_Edit%>" Text="<%$ Resources:Lang, Common_Edit%>"><%--编辑--%>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                        </Columns>
                    </asp:GridView>


                    <ul class="OneRowStyle">
                        <li>
                            <webdiyer:aspnetpager id="AspNetPager2" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="AspNetPager2_PageChanged"
                                firstpagetext="<%$ Resources:Lang, Base_FirstPage %>" lastpagetext="<%$ Resources:Lang, Base_EndPage %>" nextpagetext="<%$ Resources:Lang, Base_NextPage %>" prevpagetext="<%$ Resources:Lang, Base_LastPage %>" showpageindexbox="Never"
                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false" xmlns:webdiyer="wuqi.webdiyer">
                                                                        </webdiyer:aspnetpager>
                        </li>
                        <li>
                            <div><%= Resources.Lang.Base_Gong%><%=AspNetPager2.RecordCount  %> <%= Resources.Lang.Base_Data%></div>
                        </li>
                    </ul>

                </div>
            </td>
        </tr>

        <script type="text/javascript">
            function ChangeDivWidth() {
                document.getElementById("DivScrolls").style.width = window.document.body.offsetWidth - 25;
            }
            window.onresize = ChangeDivWidth;
            ChangeDivWidth();
            function CheckDel() {
                var number = 0;
                var controls = document.getElementById("<%=grdBASE_trande.ClientID %>").getElementsByTagName("input");

                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
                if (number == 0) {
                    alert("<%= Resources.Lang.FrmBase_InOutTypeStatusList_Msg01%>");//请选择需要作废的项！
                                return false;
                            }
                            if (confirm("<%= Resources.Lang.FrmBase_InOutTypeStatusList_Msg02%>")) {//你确认作废吗？
                    return true;
                }
                else {
                    return false;
                }
            }

            function CheckQY() {
                var number = 0;
                var controls = document.getElementById("<%=grdBASE_trande.ClientID %>").getElementsByTagName("input");

                            for (var i = 0; i < controls.length; i++) {
                                var e = controls[i];
                                if (e.type != "CheckBox") {
                                    if (e.checked == true) {
                                        number = number + 1;
                                    }
                                }
                            }
                            if (number == 0) {
                                alert("<%= Resources.Lang.FrmBaseDocuReason_List_Msg01%>");//请选择需要启用的项！
                                return false;
                            }
                            if (confirm("<%= Resources.Lang.FrmBaseDocuReason_List_Msg02%>")) {//你确认启用吗？
                                return true;
                            }
                            else {
                                return false;
                            }
                        }
        </script>



        <script type="text/javascript">
            function ChangeDivWidth() {
                document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
            }
            window.onresize = ChangeDivWidth;
            ChangeDivWidth();
        </script>


    </table>
    <input type="hidden" id="hiddOutTypeName" runat="server" />
</asp:Content>
