<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmALLOCATE_DEdit_SN.aspx.cs"
    Inherits="ALLOCATE_FrmALLOCATE_DEdit_SN" Title="--11" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Src="~/Apps/OUT/ShowSNOutAllo.ascx" TagName="ShowSNOutAllo" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/Js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="Link1"
        runat="server" />
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <style type="text/css">
        
        .showDiv{ MARGIN-RIGHT: auto;
                MARGIN-LEFT: auto;
                vertical-align:middle;
                background-color:White;
                height:100%;
                width:100%;
          }
          .modalBackground   
          { 
            background-color : White;
            filter:alpha(opacity=30); 
            opacity:0.3; 
        }
                   
        .modalPopup-text   { 
                display:block; 
                color:#000; 
                background-color:#E6EEF7; 
                text-align:center;
        }
                 
        .ui-autocomplete-loading
        {
            background: white url('../../Layout/jquery-ui-1.9.2.custom/development-bundle/demos/autocomplete/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }
        .select
        {
            cursor: hand;
            position: relative;
            left: -25px;
            top: 4px;
        }
    </style>
    <script type="text/javascript">
        function ShowNewAll(divID, sn, qty, typeid, cinvcode) {
            document.all("ctl00_ContentPlaceHolderMain_showSn_txtCinvCode").value = cinvcode;
            disponse_div(event, document.all(divID));
            document.getElementById("compareIframe").src = "../OUT/sessionset.aspx?SN=" + sn + "&QTY=" + qty + "&iType=3" + "&TypeID=" + typeid;
            document.all("ctl00_ContentPlaceHolderMain_showSn_btnSearch").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%= Resources.Lang.FrmALLOCATEList_Title%>-&gt;<%= Resources.Lang.FrmALLOCATE_DEdit_SN_AllocateDetail%> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div style="display: none;">
        <iframe id="compareIframe" src=""></iframe>
    </div>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <%--<asp:ScriptManager ID="smg" runat="server"></asp:ScriptManager>--%>
    <%--<uc1:ShowSNOutAllo ID="showSn" runat="server" />--%>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <table id="Table3" style="width: 100%" cellspacing="1" cellpadding="1" border="0">
                <tr valign="top">
                    <td valign="top">
                        <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                            runat="server" id="TabMain">
                            <tr style="display: none">
                                <td colspan="6">
                                    <style type="text/css">
                                        span.requiredSign
                                        {
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
                                    <asp:Label ID="lblCINVCODE" runat="server" Text="<%$ Resources:Lang, Common_LiaoHao%>"></asp:Label>：<%--料号--%>
                                </td>
                                <td style="width: 21%; white-space: nowrap;">
                                    <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                        MaxLength="20" Enabled="False"></asp:TextBox>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblCINVNAME" runat="server" Text="<%$ Resources:Lang, Common_CinvName1%>"></asp:Label>：<%--品名--%>
                                </td>
                                <td style="width: 21%">
                                    <asp:TextBox ID="txtCINVNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                        Enabled="false" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblCPOSITIONCODE" runat="server" Text="<%$ Resources:Lang, Common_CPOSITIONCODE%>"></asp:Label>：<%--原始储位--%>
                                </td>
                                <td style="width: 20%; white-space: nowrap;">
                                    <asp:TextBox ID="txtCPOSITIONCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                        MaxLength="60" Enabled="False"></asp:TextBox>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblCPOSITION" runat="server"  Text="<%$ Resources:Lang, Common_CPOSITION%>"></asp:Label>：<%--原始储位名称--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtCPOSITION" runat="server" CssClass="NormalInputText" Width="95%"
                                        Enabled="false" MaxLength="60"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblCTOPOSITIONCODE" runat="server" Text="<%$ Resources:Lang, Common_CTOPOSITIONCODE%>"></asp:Label>：<%--目的储位--%>
                                </td>
                                <td style="width: 20%; white-space: nowrap;">
                                    <asp:TextBox ID="txtCTOPOSITIONCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                        MaxLength="60" Enabled="False"></asp:TextBox>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblCTOPOSITION" runat="server" Text="<%$ Resources:Lang, Common_CTOPOSITION%>"></asp:Label>：<%--目的储位名称--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtCTOPOSITION" runat="server" CssClass="NormalInputText" Width="95%"
                                        Enabled="false" MaxLength="60"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblIQUANTITY" runat="server" Text="<%$ Resources:Lang, Common_IQUANTITY%>"></asp:Label>：<%--数量--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtIQUANTITY" runat="server" CssClass="NormalInputText" Width="95%"
                                        Enabled="False"></asp:TextBox>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblCDEFINE1" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_CDEFINE1%>"></asp:Label>：<%--机种--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtModels" runat="server" CssClass="NormalInputText" Width="95%"
                                        MaxLength="50" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblDINDATE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_DINDATE%>"></asp:Label>：
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtDINDATE" runat="server" CssClass="NormalInputText" Width="95%"
                                        Enabled="false" ToolTip="<%$ Resources:Lang, Common_Format%>"></asp:TextBox><%--格式：yyyy-MM-dd--%>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblCINPERSONCODE" runat="server" Text="<%$ Resources:Lang, FrmALLOCATEEdit_CINPERSONCODE%>"></asp:Label>：<%--调拨人--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtCINPERSONCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                        Enabled="false" MaxLength="40"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblCMEMO" runat="server" Text="<%$ Resources:Lang, Common_CMEMO%>"></asp:Label>：<%--备注--%>
                                </td>
                                <td style="width: 20%" colspan="5">
                                    <asp:TextBox ID="txtCMEMO" runat="server" Width="95%" TextMode="MultiLine" MaxLength="40"
                                        Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblIDS" runat="server" Text="Resources.Lang.FrmALLOCATEEdit_IDS"></asp:Label>：<%--子表编号--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtIDS" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_txtIDS%>"></asp:TextBox><%--格式：最多10位整数，最多4位小数--%>
                                </td>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Lang, Common_ID%>"></asp:Label>：<%--主表编号--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" ToolTip="<%$ Resources:Lang, FrmALLOCATE_DEdit_txtIDS%>"></asp:TextBox><%--格式：最多10位整数，最多4位小数--%>
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

                                        //function window.confirm(str) {
                                        //    execScript("n = (msgbox('" + str + "',vbYesNo, '提示')=vbYes)", "vbscript"); return (n);
                                        //} 
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
                        <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnNewSave_Click"
                            Text="<%$ Resources:Lang, Common_btnSave%>" /><%--保存--%>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_btnBack%>" CausesValidation="false" />


                        <asp:Button ID="btnShowMsg" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click" Text="show" style=" display:none;" />
                        <ajaxToolkit:ModalPopupExtender 
                        ID="ModalPopupExtender" 
                        runat="server" 
                        TargetControlID="btnShowMsg"
                        PopupControlID="pnlPopupWindown" 
                        BackgroundCssClass="modalBackground" 
                         >
                     </ajaxToolkit:ModalPopupExtender>

                     <asp:Panel ID="pnlPopupWindown" runat="server"  Width="100%"  BorderWidth="1px"   > 
                                <div class="showDiv">
                                <p><h2><%= Resources.Lang.FrmALLOCATE_DEdit_SN_Msg01%></h2> </p>
                                <br/>
                                <p>
                                  <asp:Label runat="server" ID="lblSN"></asp:Label>
                                  <%= Resources.Lang.FrmALLOCATE_DEdit_SN_Msg02%>
                                </p>
		                       <asp:Button ID="Button1" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_SN_Msg03%>"  OnClick="btnYesSave_Click"/>
                                <asp:Button ID="Button2" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, FrmALLOCATE_DEdit_SN_Msg04%>" OnClick="btnNotSave_Click" CausesValidation="false" />
                                </div>
                     </asp:Panel>

                    </td>
                </tr>
            </table>
       <%-- </ContentTemplate>--%>
        <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="okbutton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cancelbutton" EventName="Click" />
        </Triggers>--%>
   <%-- </asp:UpdatePanel>--%>
  <asp:UpdatePanel runat="server" ID="UpdatePanel2" >
        <ContentTemplate>
            <table id="TabMain2" style="width: 100%" runat="server">
                <tr valign="top">
                    <td valign="top" align="left">
                        <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_Add%>" OnClick="btnNew_Click"></asp:Button><%--新增--%>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnDeleteSn" runat="server"  CssClass="ButtonDel" Text="<%$ Resources:Lang, Common_Delete%>" OnClick="btnDeleteSn_Click" /> <%--删除--%>	
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_btnSearch%>" Visible="False"
                            OnClick="btnSearch_Click" /> <%--查询--%>
                    </td>
                </tr>
                <tr valign="top">
                    <td valign="top" colspan="5">
                      <%--  <cc1:DataGridNavigator3 ID="grdNavigatorSNDetial" runat="server" GridID="grdSNDetial"
                            ExcelButtonVisible="false" ShowPageNumber="false" ExcelName="INBILL_D_SN" IsDbPager="True" />--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                            <asp:GridView ID="grdSNDetial" runat="server" AllowPaging="True" BorderColor="Teal"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                CssClass="Grid" PageSize="15" DataKeyNames="ID" OnRowDataBound="grdSNDetial_RowDataBound"
                                OnPageIndexChanged="grdSNDetial_PageIndexChanged" OnPageIndexChanging="grdSNDetial_PageIndexChanging">
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
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chkSelect0" runat="server" BorderStyle="Solid" BorderWidth="0px" />
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID" DataFormatString="" HeaderText="" Visible="false">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_Type%>"> <%--类型--%>
                                        <ItemTemplate>
                                            <asp:Label ID="labtype" runat="server" Text='<%# Eval("SNTYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmALLOCATE_DEdit_SN_txtSN%>"><%--SN編碼--%>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSN" runat="server" Width="95%" Text='<%# Eval("SN_CODE") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="50%" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="False" Width="50%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_IQUANTITY%>"><%--数量--%>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" Width="95%" Text='<%# Eval("QUANTITY") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="50%" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="False" Width="50%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                          
                        </div>
                    </td>
                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>