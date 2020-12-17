<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyXML.aspx.cs" Inherits="Apps_ModifyXML_ModifyXML"
    MasterPageFile="~/Apps/DefaultMasterPage.master"
     %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../BASE/ShowVENDORDiv.ascx" TagName="ShowVENDORDiv" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>

    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script type="text/javascript">
      
    </script>

    <style type="text/css">
        fieldset {
        padding:10px;
        margin:10px;
        width:98%;
        color:#333; 
        border:#06c dashed 1px;
        } 
        legend {
        color:#06c;
        border:#b6b6b6 solid 0px;
        padding:3px 6px;
        width:60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    基础资料-&gt;<asp:Literal ID="ltPageTable" runat="server" Text="修改语言文件"></asp:Literal>
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
                    runat="server" id="TabMain">
                   
                   <%-- <tr>
                         <td class="InputLabel" style="width: 13%">
                           <asp:Label ID="Label6" runat="server" Text="语言："></asp:Label>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                           <asp:DropDownList ID="ddllang" runat="server">
                               <asp:ListItem Text="全部" Value=""></asp:ListItem>
                               <asp:ListItem Text="中文（简体）" Value=""></asp:ListItem>
                               <asp:ListItem Text="中文（繁体）" Value=""></asp:ListItem>
                               <asp:ListItem Text="英文（美国）" Value=""></asp:ListItem>
                           </asp:DropDownList>
                        </td>

                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label7" runat="server" Text="模块："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="DropDownList1" runat="server">
                               <asp:ListItem Text="全部" Value=""></asp:ListItem>
                               <asp:ListItem Text="数据库" Value=""></asp:ListItem>
                               <asp:ListItem Text="PDA" Value=""></asp:ListItem>
                           </asp:DropDownList>
                        </td>

                       
                        <td class="InputLabel" style="width: 13%">
                           <asp:Label ID="Label8" runat="server" Text="源文件Key："></asp:Label>
                        </td>
                        <td style="width: 21%; white-space: nowrap;">
                            <asp:DropDownList ID="DropDownList2" runat="server">
                               <asp:ListItem Text="全部" Value=""></asp:ListItem>
                               
                           </asp:DropDownList>
                            <asp:TextBox ID="txtkey" runat="server"></asp:TextBox>
                        </td>
                       
                    </tr>
                   --%>
                  
                </table>
                <br />

              

                   <table class="InputTable"  cellspacing="0" cellpadding="1" width="100%" border="1" runat="server" id="Table1">
                     <tr>
                        <td class="InputLabel" style="width: 13%;text-align:right;">
                            
                            <asp:Label ID="Label11" runat="server" Text="语言："></asp:Label>


                              <div style="display:none;">
                                 <asp:TextBox ID="txtId" runat="server" CssClass="NormalInputText" Width="96%" MaxLength="20" Text=""></asp:TextBox>
                            </div>

                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="ddlLang" runat="server" Enabled="false">
                              <%-- <asp:ListItem Text="中文（简体）" Value="zh-cn"></asp:ListItem>
                               <asp:ListItem Text="中文（繁体）" Value="zh-tw"></asp:ListItem>
                               <asp:ListItem Text="英文（美国）" Value="en-us"></asp:ListItem>--%>
                           </asp:DropDownList>
                               
                        </td>
                       
                         <td class="InputLabel" style="width: 13%;text-align:right;">
                            
                            <asp:Label ID="Label14" runat="server" Text="模块："></asp:Label>
                        </td>
                        <td style="width: 21%">
                            <asp:DropDownList ID="ddlModel" runat="server" Enabled="false">
                               <asp:ListItem Text="数据库" Value="DB"></asp:ListItem>
                               <asp:ListItem Text="PDA" Value="PDA"></asp:ListItem>
                           </asp:DropDownList>
                               
                        </td>
                          <td class="InputLabel" style="width: 13%;text-align:right;">
                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label15" runat="server" Text="源文件Key："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtKey" runat="server" CssClass="NormalInputText" Width="96%"
                                MaxLength="20" Enabled="false" Text=""></asp:TextBox>
                        </td>

                    </tr>
                    
                   
                    <tr>
                       
                     <td class="InputLabel" style="width: 13%;text-align:right;">
                            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label17" runat="server" Text="源文件值："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtValue" runat="server" CssClass="NormalInputText" 
                                Width="96%" TextMode="MultiLine" MaxLength="300" Text="返回成功" Height="50px"></asp:TextBox>
                            
                        </td>
                         <td class="InputLabel" style="width: 13%;text-align:right;">
                            <asp:Label ID="Label18" runat="server" Text="创建人："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCreateUser" runat="server" CssClass="NormalInputText" Width="96%"
                                MaxLength="50" Enabled="false" Text="admin"></asp:TextBox>
                        </td>
                        <td class="InputLabel" style="width: 13%;text-align:right;">
                            <asp:Label ID="Label19" runat="server" Text="创建时间："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCreateTime" runat="server" CssClass="NormalInputText" Width="96%"
                                MaxLength="50" Enabled="false" Text="2019-08-16"></asp:TextBox>
                        </td>

                        
                    </tr>
                  
                  
                    <tr>
                        <td class="InputLabel" style="width: 13%;text-align:right;">
                            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label20" runat="server" Text="状态："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlStatus" runat="server" >
                               <asp:ListItem Text="启用" Value="0"></asp:ListItem>
                               <asp:ListItem Text="作废" Value="1"></asp:ListItem>
                           </asp:DropDownList>
                        </td>
                      <td class="InputLabel" style="width: 13%;text-align:right;">
                            <asp:Label ID="Label21" runat="server" Text="修改人："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtModifyUser" runat="server" CssClass="NormalInputText" Width="96%"
                                MaxLength="50" Enabled="false" Text="admin"></asp:TextBox>
                        </td>
                       <td class="InputLabel" style="width: 13%;text-align:right;">
                            <asp:Label ID="Label22" runat="server" Text="修改时间："></asp:Label>
                        </td>
                        <td style="width: 20%">
                             <asp:TextBox ID="txtModifyTime" runat="server" CssClass="NormalInputText" Width="96%"
                                MaxLength="50" Enabled="false" Text="2019-08-16"></asp:TextBox>
                        </td>

                    </tr>

                    <tr>
                       

                         <td class="InputLabel" style="width: 13%;text-align:right;">
                            <asp:Label ID="Label23" runat="server" Text="备注："></asp:Label>
                        </td>
                        <td style="width: 20%" colspan="4">
                            <asp:TextBox ID="txtReamrk" runat="server" TextMode="MultiLine" Width="96%" 
                                Height="50px" MaxLength="200" Text="返回成功"></asp:TextBox>
                        </td>
                        

                    </tr>
                    </table>

                      <br />
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" CssClass="ButtonSave"  Text="保存" OnClick="btnSave_Click" />
                            &nbsp;&nbsp;
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="返回" CausesValidation="false" />
                        </td>
                    </tr>
                </table>

               

         <fieldset>
          <legend style="font-size:14px;width:100px;">其它语言参考</legend>
           <table  cellspacing="0" cellpadding="1" width="100%" border="0">
            <tr>
            <td>
                <table id="Table2" style=" width: 100%" runat="server">
                    <tr>
                        <td colspan="4">
                            <div style="height: 250px; overflow-x: scroll; width: 100%" id="Div1">
                                <asp:Panel ID="panel2" runat="server">
                                    <asp:gridview id="grdDetail" runat="server" allowpaging="True" bordercolor="Teal"
                                        borderstyle="Solid" borderwidth="1px" cellpadding="1"
                                        width="100%" autogeneratecolumns="False" ShowHeaderWhenEmpty="true"
                                        PageSize="15"
                                       cssclass="Grid">
                                        <pagersettings visible="False" />
                                        <alternatingrowstyle cssclass="AlternatingRowStyle" />
                                        <rowstyle horizontalalign="Left" wrap="False" />
                                        <selectedrowstyle backcolor="#738A9C" font-bold="True" forecolor="#F7F7F7" wrap="False" />
                                        <pagerstyle horizontalalign="Right" />
                                        <headerstyle font-bold="True" horizontalalign="Center" cssclass="" wrap="False" />
                                        <columns> 
                                          <asp:BoundField DataField="LanguageName" DataFormatString="" HeaderText="语言">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="ModuleId" DataFormatString="" HeaderText="模块">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="SourceKey" DataFormatString="" HeaderText="源文件Key">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                          </asp:BoundField>
                                          <asp:BoundField DataField="SourceValue" DataFormatString="" HeaderText="源文件值">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                          </asp:BoundField>
                                            <asp:BoundField DataField="StatusName" DataFormatString="" HeaderText="状态">
                                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                                            </asp:BoundField>
                                          <asp:BoundField DataField="Remark" DataFormatString="" HeaderText="备注">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                          </asp:BoundField>
                                          <asp:BoundField DataField="CreateUser" DataFormatString="" HeaderText="创建人">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                          </asp:BoundField>
                                          <asp:BoundField DataField="CreateTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="创建日期">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                                          </asp:BoundField>
                                          <asp:BoundField DataField="ModifyUser" DataFormatString="" HeaderText="修改人">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="left" Wrap="False" />
                                          </asp:BoundField>
                                          <asp:BoundField DataField="Modifytime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="修改时间">
                                            <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Width="80px" />
                                         </asp:BoundField>
				                    </columns>
                                    </asp:gridview>
                                      <ul class="OneRowStyle"  >
                                        <li>
                                            <webdiyer:aspnetpager ID="AspNetPager2" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" OnPageChanged="AspNetPager2_PageChanged"  PagingButtonSpacing="0" CurrentPageButtonClass="active" 
                                                FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                                                AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                                            </webdiyer:aspnetpager>
                                        </li>
                                        <li>
                                            <div>共 <%=AspNetPager2.RecordCount  %> 条数据</div>
                                        </li>
                                    </ul>
                                </asp:Panel>
                                <script type="text/javascript" language="javascript">
                                    //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                                    //settingPad("", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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
                    
            </table>
        </fieldset>
    </td>
</tr>
       
    </table>
</asp:Content>
