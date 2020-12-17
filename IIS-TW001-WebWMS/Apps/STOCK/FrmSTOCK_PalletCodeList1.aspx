<%@ Page Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="FrmSTOCK_PalletCodeList1.aspx.cs" Inherits="FrmSTOCK_PalletCodeList1" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:content id="Content3" contentplaceholderid="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script src="../../Layout/Js/Help.js" type="text/javascript"></script>

    <style  type="text/css">
         .ButtonDo1 {
            border-right: 0px;
            border-top: 0px;
            font-size: 12px;
            font-weight: bold;
            border-left: 0px;
            cursor: pointer;
            border-bottom: 0px;
            padding-left: 22px;
            padding-top: 2px;
            font-family: "Arial", "Helvetica", "sans-serif";
            height: 24px;
            color: #3580c9;
            background-image: url(../../Layout/Css/LG/Images/in_s_bg_do1.jpg);
            width: 120px;
        }         
    </style>



</asp:content>

<asp:content id="Content1" contentplaceholderid="HolderTitle" runat="Server">
    库存管理-&gt;栈板库存
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition">
                    <tr>
                        <th align="left" colspan="5">
                            &nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                                height="11" alt="" runat="server" id="titleImg" />
                            检索条件
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="折叠" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
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

                                .style1
                                {
                                    height: 29px;
                                }
                            </style>
                        </td>
                    </tr>
                    <tr>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label1" runat="server" Text="仓库："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="ddlWareHouse" runat="server"  Width="95%"> </asp:DropDownList>
                        </td>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTYPE" runat="server" Text="种类："></asp:Label>
                        </td>
                        <td style="width: 20%">                            
                            <asp:DropDownList ID="DropCTYPE" runat="server" Width="95%" >
                            </asp:DropDownList>
                        </td>
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblCTICKETCODE" runat="server" Text="储位编码："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtCPocitionCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="Label4" runat="server" Text="栈板号："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPalledCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                         <td class="InputLabel" style="width: 13%;">
                            <asp:Label ID="Label5" runat="server" Text="料号："></asp:Label>
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCinvcode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                          <td class="InputLabel" style="width: 10%">
                            <asp:Label ID="Label2" runat="server" Text="是否有栈板："></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:RadioButtonList ID="rbtList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" BorderStyle="None">
                                <asp:ListItem Text="没有" Value="0"></asp:ListItem>
                                <asp:ListItem Text="有" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="全部" Value=""></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>

                      
                         <td class="InputLabel" style="width: 13%">
                            <asp:Label ID="lblDCREATETIMEFromFrom" runat="server" Text="物料识别码："></asp:Label>
                        </td>
                        <td style="width: 20%; white-space: nowrap;">
                           <asp:TextBox ID="txtProductCode" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>                       
                       
                    </tr>
                    
                    <tr style="display: none">
                        <td colspan="6">
                            <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch" Text="查询" OnClientClick="return validate_DataTime('ctl00_ContentPlaceHolderMain_txtDCREATETIMEFrom','ctl00_ContentPlaceHolderMain_txtDCREATETIMETo','ctl00_ContentPlaceHolderMain_txtDAUDITDATEFrom','ctl00_ContentPlaceHolderMain_txtDAUDITDATETo','制单日期的第一个日期应该小于第二个日期!','审核日期的第一个日期应该小于第二个日期!');"
                                OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr class="tableCell">
            <td colspan="6" align="left" style="padding: 15px 0px;">
                 <asp:Button ID="Btn_BatchOut" runat="server" CssClass="ButtonDo1" Text="批量出库"  OnClientClick="return CheckOperation('批量出库',0);" OnClick="Btn_BatchOut_Click"></asp:Button> <%--//return CheckConfirm();--%>
                <asp:Button ID="Btn_UnionInAgain" runat="server" CssClass="ButtonDo1" Text="合并重入"  OnClientClick="return CheckOperation('合并重入',1);" OnClick="Btn_UnionInAgain_Click"></asp:Button>
            </td>
        </tr> 
        <tr>
            <td>
                <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                    <asp:GridView ID="grdStockPostition" runat="server" AllowPaging="True" BorderColor="Teal" DataKeyNames="id,cpositioncode"
                        BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="1" Width="100%" AutoGenerateColumns="False"  OnRowDataBound="grdStockPostition_RowDataBound"
                        CssClass="Grid" PageSize="15">
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
                            <asp:BoundField DataField="ID" DataFormatString="" HeaderText="id" Visible="False">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField  DataField="cwareid" DataFormatString="" HeaderText="仓库编号">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>   
                             <asp:BoundField  DataField="cwarename" DataFormatString="" HeaderText="仓库名称">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>   

                            <asp:BoundField  DataField="ctyename" DataFormatString="" HeaderText="储位种类">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>   
                                      <asp:BoundField  DataField="cpositioncode" DataFormatString="" HeaderText="储位编码">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>   

                                      <asp:BoundField  DataField="cposition" DataFormatString="" HeaderText="储位编码">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="left" Wrap="False" />
                            </asp:BoundField>   
                                      <asp:BoundField  DataField="usagename" DataFormatString="" HeaderText="使用率(≈)">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>   
                             <asp:BoundField  DataField="palletcode" DataFormatString="" HeaderText="栈板号">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>   
                                   <asp:BoundField  DataField="hasPalletname" DataFormatString="" HeaderText="是否有栈板">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                <ItemStyle HorizontalAlign="center" Wrap="False" />
                            </asp:BoundField>                              
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="库存详情" DataTextField=""
                                DataTextFormatString="" HeaderText="库存详情" Text="查看">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                            <asp:HyperLinkField DataNavigateUrlFields="" DataNavigateUrlFormatString="栈板出库" DataTextField=""
                                DataTextFormatString="" HeaderText="栈板出库" Text="生成">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:HyperLinkField>
                        </Columns>
                    </asp:GridView>
                    <ul class="OneRowStyle" >
                    <li>
                        <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" 
                            PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                            FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                            AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false">
                        </webdiyer:aspnetpager>
                    </li>
                    <li>
                        <div>共 <%=AspNetPager1.RecordCount  %> 条数据</div>
                    </li>
                </ul>
                    <script type="text/javascript" language="javascript">
                        //移动设备浏览器上，grid如果无法显示全部数据，请按如下设定
                        //settingPad("<%= grdStockPostition.ClientID%>", { "HolderHeight": 170, "HolderWidth": 750, "CtrlWidth": 1000 });
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

            function CheckOperation(operation,flag) {
                var number = 0;
                var controls = document.getElementById("<%=grdStockPostition.ClientID %>").getElementsByTagName("input");

                for (var i = 0; i < controls.length; i++) {
                    var e = controls[i];
                    if (e.type != "CheckBox") {
                        if (e.checked == true) {
                            number = number + 1;
                        }
                    }
                }
                if (number == 0) {
                    alert("请选择需要" + operation + "的项！");
                    return false;
                }
                if (flag == 1 && number==1) {
                    alert("请至少选择两项数据进行" + operation + "操作");
                    return false;
                }
                if (confirm("你确认执行" + operation + "操作吗？")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
    </table>
</asp:content>
