<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyXMLMulit.aspx.cs"
     MasterPageFile="~/Apps/DefaultMasterPage.master"
     Inherits="Apps_ModifyXML_ModifyXMLMulit" %>
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
    基础资料-&gt;<asp:Literal ID="ltPageTable" runat="server" Text="批量修改语言文件"></asp:Literal>
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
                   

                    <tr>
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
                   
                  
                </table>
                <br />

         <table  cellspacing="0" cellpadding="1" width="100%" border="0">
            <tr>
            <td>
                <table id="Table2" style=" width: 100%" runat="server">
                    <tr>
                        <td colspan="4">
                            <div style="height: 950px; overflow-x: scroll; width: 100%" id="Div1">
                                <asp:Panel ID="panel2" runat="server">
                                    <asp:gridview id="Gridview1" runat="server" allowpaging="True" bordercolor="Teal"
                                        borderstyle="Solid" borderwidth="1px" cellpadding="1"
                                        width="100%" autogeneratecolumns="False" ShowHeaderWhenEmpty="true"
                                     
                                       cssclass="Grid" pagesize="15" >
                                        <pagersettings visible="False" />
                                        <alternatingrowstyle cssclass="AlternatingRowStyle" />
                                        <rowstyle horizontalalign="Left" wrap="False" />
                                        <selectedrowstyle backcolor="#738A9C" font-bold="True" forecolor="#F7F7F7" wrap="False" />
                                        <pagerstyle horizontalalign="Right" />
                                        <headerstyle font-bold="True" horizontalalign="Center" cssclass="" wrap="False" />
                                        <columns> 
                                             <asp:BoundField DataField="LanguageId" DataFormatString="" HeaderText="语言">
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
                                            <asp:TemplateField HeaderText="源文件值">
                                              <ItemTemplate>
                                                <asp:TextBox ID="idkey" runat="server" Text='<%#Eval("SourceValue") %>' Width="96%"></asp:TextBox>
                                              </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="备注">
                                                 <ItemTemplate>
                                                <asp:TextBox ID="idRemark" runat="server" Text='<%#Eval("Remark") %>' Width="96%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                        <webdiyer:aspnetpager ID="AspNetPager2" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" 
                            FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                        </webdiyer:aspnetpager>
                    </li>
                    <li>
                        <div>共 条数据</div>
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
    </td>
</tr>
       
    </table>
</asp:Content>
