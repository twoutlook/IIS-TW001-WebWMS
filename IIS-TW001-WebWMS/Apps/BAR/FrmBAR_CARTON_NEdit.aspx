<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBAR_CARTON_NEdit.aspx.cs"
    Inherits="FrmBAR_CARTON_NEdit" Title="--箱" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="ShowBarTypeDiv.ascx" TagName="ShowBarTypeDiv" TagPrefix="ucType" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    条码管理-&gt;箱
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <ucType:ShowBarTypeDiv ID="ucShowBarType" runat="server" />
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
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
                            <asp:Label ID="lblCCODE" runat="server" Text="箱编码："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCCODE" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="20"  Enabled="false"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCNAME" runat="server" Text="箱名称："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCNAME" runat="server" CssClass="NormalInputText" Width="95%"
                                MaxLength="100"></asp:TextBox>
                            <span class="requiredSign">*</span>
                            <asp:RequiredFieldValidator ID="rfvtxtCNAME" runat="server" ControlToValidate="txtCNAME"
                                ErrorMessage="请填写名称!" Display="None"> 
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblICAPACITY" runat="server" Text="箱类型："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlTYPE_ID" runat="server" Width="95%" ></asp:DropDownList>
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
                            <asp:HiddenField id="hf_Id" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave" OnClick="btnSave_Click"
                                Text="保存" />
                            &nbsp;&nbsp; &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="返回" CausesValidation="false" />                       
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="TabMain0" style="height: 100%; width: 100%" runat="server" >
                    <tr valign="top">
                        <td valign="top" align="left">
                            <div style="display: none">
                                <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="查询" OnClick="btnSearch_Click">
                                </asp:Button>
                                &nbsp;&nbsp;</div>
                            <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="新增" ></asp:Button>
                            &nbsp;&nbsp;<asp:Button ID="btnDelete0" OnClick="btnDelete_Click" runat="server"
                                Text="删除" CssClass="ButtonDel" OnClientClick="return CheckDel('ctl00_ContentPlaceHolderMain_grdINASN_D');" />
                            
                        </td>
                         <td class="InputLabel" align="right" style="width: 10%">
                              <asp:Label ID="Label5" CssClass="label" runat="server" Text="查询料号：" Visible="false" ></asp:Label>
                        </td>
                        <td style="width: 10%">
                                <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="95%" Visible="false"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%">
                                    <asp:Button ID="Button1" runat="server"  CssClass="ButtonSearch" Text="查询" Visible="false" OnClick="btnSearch_Click">
                                    </asp:Button>
                         </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                                <asp:gridview id="grdINASN_D" runat="server" allowpaging="True" bordercolor="Teal"
                                    borderstyle="Solid" borderwidth="1px" cellpadding="1" width="100%" autogeneratecolumns="False"
                                  
                                    onrowdatabound="grdINASN_D_RowDataBound" cssclass="Grid" pagesize="15">
                                       <pagersettings visible="False" /> <alternatingrowstyle cssclass="AlternatingRowStyle" /> <rowstyle horizontalalign="Left" wrap="False" /> <selectedrowstyle backcolor="#738A9C" font-bold="True" forecolor="#F7F7F7" wrap="False" /> <pagerstyle horizontalalign="Right" /> <headerstyle font-bold="True" horizontalalign="Center" cssclass="" wrap="False" /> <columns> <asp:TemplateField HeaderText="<input id='chkSelect' type='checkbox' onclick='SelectAll4Grid(this)' />"><ControlStyle BorderWidth="0px" /><ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid" 
				                     BorderWidth="0px" /> 
				             </ItemTemplate> 
				             <EditItemTemplate> 
				                 <asp:CheckBox ID="chkSelect0" runat="server" BorderStyle="Solid" BorderWidth="0px" /> 
				             </EditItemTemplate> 
				             <HeaderStyle HorizontalAlign="Center" /> 
				             <ItemStyle HorizontalAlign="Center" /> 
				         </asp:TemplateField>
				                    <asp:BoundField DataField="IDS" DataFormatString="{0:0}" HeaderText="子表编号" 
                                Visible="False"><HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                                <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				         </asp:BoundField>
				                    <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="主表编号" 
                                Visible="False"><HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                                <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				         </asp:BoundField>
				                    <asp:BoundField DataField="CINVBARCODE" DataFormatString="" HeaderText="料号" ><HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="left"  Wrap="False" Width="200px" /> 
				          </asp:BoundField>
                                   <asp:BoundField DataField="SNCODE" DataFormatString="" HeaderText="SN" ><HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				          </asp:BoundField>
				                    <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="品名" Visible="False" ><HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                             <ItemStyle HorizontalAlign="left"  Wrap="False" Width="300px" /> 
				         </asp:BoundField>
				                    <asp:BoundField DataField="QTY" DataFormatString="{0:0.00}" HeaderText="数量"><HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                             <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				         </asp:BoundField>                                             
                                    <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="编辑"  DataTextField="" DataTextFormatString="" HeaderText="编辑" Text="编辑"><HeaderStyle HorizontalAlign="Center"  Wrap="False"/> 
				             <ItemStyle HorizontalAlign="Center" Wrap="False" /> 
				         </asp:HyperLinkField>
                                    </columns> </asp:gridview>
                                  <ul class="OneRowStyle">
                        <li>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                            </webdiyer:AspNetPager>
                        </li>
                        <li>
                            <div>共 <%=AspNetPager1.RecordCount  %> 条数据</div>
                        </li>
                    </ul>
                                <script type="text/javascript" language="javascript">
                                    //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                                    //settingPad("<%= grdINASN_D.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                                </script>
                            </div>
                        </td>
                    </tr>
                    <script type="text/javascript">
                        function ChangeDivWidth() {
                            document.getElementById("DivScroll").style.width = window.document.body.offsetWidth - 25;
                        }
                        window.onresize = ChangeDivWidth;
                        ChangeDivWidth();
                    </script>
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
</asp:Content>
