<%@ Page ValidateRequest="false" Language="C#" AutoEventWireup="true" CodeFile="FrmINASNEdit_UnionPallet.aspx.cs"
    Inherits="FrmINASNEdit_UnionPallet" Title="--<%$ Resources:Lang, FrmUnionPalletList_MSG2 %>" MasterPageFile="~/Apps/DefaultMasterPage.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../BASE/ShowVENDORDiv.ascx" TagName="ShowVENDORDiv" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>

    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script type="text/javascript">
        function SetControlValue(ControlName, Values, ControlName2, Values2) {
            //alert(ControlName + ":" + ControlName2);
            //alert(ControlName2);
            document.all(ControlName).value = Values;
            document.all(ControlName2).value = Values2;
        }

        function Show(divID) {
            //alert(divID);
            disponse_div(event, document.all(divID));
        }

      
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.Common_InbillMangement%>-&gt;<asp:Literal ID="ltPageTable" runat="server" Text="<%$ Resources:Lang, FrmINASNEdit_UnionPallet_ltPageTable%>"></asp:Literal><%--入库通知单拼板--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:ShowVENDORDiv ID="ShowVENDORDiv1" runat="server" />
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="7">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                height="11" alt="" runat="server" id="titleImg" />
                            <%--<%=Resources.Lang.Common_JSCeria%>--%>
                            <%=Resources.Lang.Common_JSCeria%>
                        </th>
                        <th style="border-left-width: 0px" align="right" class="auto-style2">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%=Resources.Lang.Common_Fold%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr style="display: none">
                        <td colspan="8">
                            <style type="text/css">
                                span.requiredSign
                                {
                                    color: #FF0000;
                                    font-weight: bold;
                                    position: relative;
                                    left: -15px;
                                    top: 2px;
                                }
                                .style1
                                {
                                    height: 29px;
                                }
                                .auto-style1 {
                                    width: 10%;
                                }
                                .auto-style2 {
                                    width: 13%;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>                        
                        <td class="InputLabel" style="width: 5%">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Lang, FrmInbill_PalletCode %>"></asp:Label>：
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="txtPalletCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>

                        <td class="InputLabel" style="width: 5%">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Lang, Common_PartnumNO %>"></asp:Label>：
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="txtcpositioncode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                        <%--<td class="InputLabel" style="width: 5%">
                            <asp:Label ID="lblCTICKETCODE" runat="server"Text="<%$ Resources:Lang, Common_PartnumName %>"></asp:Label>：
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtcposition" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>--%>
                         <td class="InputLabel" style="width: 5%">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Lang, FrmInbill_CinvCode %>" CssClass=""></asp:Label>

                        </td>
                        <td class="auto-style2">
                             <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                         
                         <td class="InputLabel" style="width: 5%">
                             <%--储位规格：--%>
                            <asp:Label ID="lblITYPE" runat="server" Text="<%$ Resources:Lang, FrmINASNEdit_UnionPallet_lblITYPE %>"></asp:Label>
                        </td>
                        <td style="width: 10%">
                             <asp:DropDownList ID="ddlsiteType" runat="server" Width="95%">
                             <%--     <asp:ListItem Text="全部" Value="" ></asp:ListItem>
                                 <asp:ListItem Text="低储位" Value="1" ></asp:ListItem>
                                 <asp:ListItem Text="中储位" Value="2" ></asp:ListItem>
                                  <asp:ListItem Text="高储位" Value="3"></asp:ListItem>--%>
                               </asp:DropDownList>

                        </td>
                    </tr>

                    <tr style="display: none">
                        <td colspan="8">
                            <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="<%$ Resources:Lang, Common_QueryBtn %>"  OnClick="btnSearch_Click"  Visible="false"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="TabMain0" style="height: 100%; width: 100%" runat="server">
                   
                    <tr>
                        <td colspan="4">
                            <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                                <asp:Panel ID="panel1" runat="server">
                                    <asp:gridview id="grdStock" runat="server" allowpaging="True" bordercolor="Teal"
                                        datakeynames="ID" borderstyle="Solid" borderwidth="1px" cellpadding="1"
                                        width="100%" autogeneratecolumns="False" ShowHeaderWhenEmpty="true"
                                        onrowdatabound="grdStock_RowDataBound" cssclass="Grid" pagesize="15" >
                                        <pagersettings visible="False" />
                                        <alternatingrowstyle cssclass="AlternatingRowStyle" />
                                        <rowstyle horizontalalign="Left" wrap="False" />
                                        <selectedrowstyle backcolor="#738A9C" font-bold="True" forecolor="#F7F7F7" wrap="False" />
                                        <pagerstyle horizontalalign="Right" />
                                        <headerstyle font-bold="True" horizontalalign="Center" cssclass="" wrap="False" />
                                        <columns> 
                                   <asp:TemplateField HeaderText="<input id='chkSelect' type='checkbox' onclick='SelectAll4Grid(this)' />" HeaderStyle-Width="3%">
                                       <ControlStyle BorderWidth="0px" />
                                       <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" BorderStyle="Solid" 
				                     BorderWidth="0px" /> 
				             </ItemTemplate>
				             <HeaderStyle HorizontalAlign="Center" /> 
				             <ItemStyle HorizontalAlign="Center" /> 
				         </asp:TemplateField>
				                 <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG19 %>"  Visible="False"><%--主表编号--%>
				                     <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                                <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				         </asp:BoundField>
                           <asp:BoundField DataField="palletcode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_PalletCode %>" >
				                <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="left"  Wrap="False" Width="10%" /> 
				           </asp:BoundField>

				            <asp:BoundField DataField="cpositioncode" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PartnumNO %>" >
				                <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="left"  Wrap="False" Width="10%" /> 
				         </asp:BoundField>
				             <asp:BoundField DataField="cposition" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PartnumName %>"><%--储位名称--%>
				                 <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                                <ItemStyle HorizontalAlign="left"  Wrap="False" Width="20%" /> 
				         </asp:BoundField>
				          <asp:BoundField DataField="iqty" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang, FrmINASNEdit_UnionPallet_iqty %>"><%--库存量--%>
				              <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                         <ItemStyle HorizontalAlign="left"  Wrap="False"  Width=""/> 
				         </asp:BoundField>
                         <asp:TemplateField Visible="false">
                             <ItemTemplate>
                                 <asp:HiddenField  runat="server" Value='<%# Eval("cinvcode") %>'  ID="hdcinvcode" />
                             </ItemTemplate>
                         </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="<%$ Resources:Lang, CommonB_View %>" DataTextField=""
                                DataTextFormatString="" HeaderText="<%$ Resources:Lang, FrmINASNEdit_UnionPallet_MSG1 %>" Text="<%$ Resources:Lang, CommonB_View %>"><%--库存详情--%>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                          
				                    </columns>
                                    </asp:gridview>

                            <ul class="OneRowStyle">
                                <li>
                                    <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                        FirstPageText="<%$ Resources:Lang, Common_FirstPage%>" LastPageText="<%$ Resources:Lang, Common_LastPage%>" NextPageText="<%$ Resources:Lang, Common_NextPage%>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                                        AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                    </webdiyer:aspnetpager>
                                </li>
                                <li>
                                    <div><%=Resources.Lang.Common_TotalPage %> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_TotalPage1 %></div>
                                </li>
                             </ul>
                      </asp:Panel>
                                <script type="text/javascript" language="javascript">
                                    //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                                    //settingPad("<%= grdStock.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
                                </script>
                            </div>
                        </td>
                    </tr>

                     <tr valign="middle">
                        <td valign="middle" align="center" class="auto-style1">
                            <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="<%$ Resources:Lang, Common_GenerateBtn %>" OnClick="btnNew_Click"> </asp:Button>
                             &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>"
                                CausesValidation="false" OnClick="btnBack_Click" />
                            <asp:Button  ID="btnRefresh" runat="server" Visible="false"  CssClass="ButtonAdd" OnClick="btnRefresh_Click" Text="<%$ Resources:Lang, FrmINBILLEdit_MSG13 %>" /><%--刷新--%>
                        </td>

                    </tr>

                     <tr>
                        <td colspan="4">
                            <div style="height: 300px; overflow-x: scroll; width: 100%" id="Div1">
                                <asp:Panel ID="panel2" runat="server">
                                    <asp:gridview id="GrdStock_d" runat="server" allowpaging="True" bordercolor="Teal"
                                        datakeynames="ID" borderstyle="Solid" borderwidth="1px" cellpadding="1"
                                        width="100%" autogeneratecolumns="False" ShowHeaderWhenEmpty="true"
                                     
                                        onrowdatabound="GrdStock_d_RowDataBound" cssclass="Grid" pagesize="15" Visible="true" >
                                        <pagersettings visible="False" />
                                        <alternatingrowstyle cssclass="AlternatingRowStyle" />
                                        <rowstyle horizontalalign="Left" wrap="False" />
                                        <selectedrowstyle backcolor="#738A9C" font-bold="True" forecolor="#F7F7F7" wrap="False" />
                                        <pagerstyle horizontalalign="Right" />
                                        <headerstyle font-bold="True" horizontalalign="Center" cssclass="" wrap="False" />
                                        <columns> <asp:TemplateField HeaderText="<input id='chkSelect1' type='checkbox' onclick='SelectAll4Grid(this)' />">
                                            <ControlStyle BorderWidth="0px" /><ItemTemplate><asp:CheckBox ID="chkSelect1" runat="server" AutoPostBack="False" BorderStyle="Solid" 
				                     BorderWidth="0px" /> 
				             </ItemTemplate>
				             <HeaderStyle HorizontalAlign="Center" /> 
				             <ItemStyle HorizontalAlign="Center" /> 
				         </asp:TemplateField>
				                 <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG19 %>"  Visible="False"><%--主表编号--%>
				                     <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                                <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				         </asp:BoundField>
                           <asp:BoundField DataField="palletcode" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmInbill_PalletCode %>" >
				                <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="left"  Wrap="False" Width="80px" /> 
				           </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_PackageNO %>" ItemStyle-Width="30%">
                              <ItemTemplate>
                              <asp:TextBox ID="txtSN" runat="server" Width="95%" Text=<%# Eval("SN_CODE") %> Enabled="false"></asp:TextBox>
                              </ItemTemplate>
                            </asp:TemplateField>
				            <%--<asp:BoundField DataField="sn_code" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_PackageNO %>" >
				                <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="left"  Wrap="False" Width="140px" /> 
				         </asp:BoundField>--%>
				             <asp:BoundField DataField="cstatus" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_Cstatus %>">
				                 <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                                <ItemStyle HorizontalAlign="left"  Wrap="False" Width="260px" /> 
				         </asp:BoundField>
				          <asp:BoundField DataField="cerpcode" DataFormatString="{0:N2}" HeaderText="<%$ Resources:Lang, FrmInbill_ErpCode %>">
				              <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                         <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				         </asp:BoundField>
                              <asp:BoundField DataField="cinvcode" DataFormatString="" HeaderText="<%$ Resources:Lang, CommonB_CinvCode %>" ><%--物料编码--%>
				                <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="left"  Wrap="False" Width="140px" /> 
				         </asp:BoundField>
				             <asp:BoundField DataField="cinvname" DataFormatString="" HeaderText="<%$ Resources:Lang, CommonB_CinvName %> "><%--物料名称--%>
				                 <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                                <ItemStyle HorizontalAlign="left"  Wrap="False" Width="260px" /> 
				            </asp:BoundField>
                            <asp:BoundField DataField="CREATETIME" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINASNEdit_UnionPallet_MSG2 %>" ><%--拼板时间--%>
				                <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="left"  Wrap="False" Width="140px" /> 
				         </asp:BoundField>
				             <asp:BoundField DataField="INASNCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmINASNEdit_UnionPallet_MSG3 %>"><%--通知单号--%>
				                 <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                                <ItemStyle HorizontalAlign="left"  Wrap="False" Width="260px" /> 
				                </asp:BoundField>
                           
				                    </columns>
                                    </asp:gridview>
                                      <table id="webPager2">
                      <tr> 
                         <td>
                            <ul class="OneRowStyle">
                                <li>
                                    <webdiyer:aspnetpager ID="AspNetPager2" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager2_PageChanged"
                                        FirstPageText="<%$ Resources:Lang, Common_FirstPage%>" LastPageText="<%$ Resources:Lang, Common_LastPage%>" NextPageText="<%$ Resources:Lang, Common_NextPage%>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                                        AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                    </webdiyer:aspnetpager>
                                </li>
                                <li>
                                    <div><%=Resources.Lang.Common_TotalPage %> <%=AspNetPager2.RecordCount  %> <%=Resources.Lang.Common_TotalPage1 %></div>
                                </li>
                             </ul>
                             </td>
                          </tr>
                       </table>
                                </asp:Panel>
                                <script type="text/javascript" language="javascript">
                                    //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                                    //settingPad("<%= GrdStock_d.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
