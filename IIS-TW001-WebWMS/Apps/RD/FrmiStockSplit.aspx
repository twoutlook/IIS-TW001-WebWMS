<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmiStockSplit.aspx.cs"
 MasterPageFile="~/Apps/DefaultMasterPage.master" Title="--<%$Resources:Lang FrmiStockSplit_Title1%>"  Inherits="Apps_RD_FrmiStockSplit" %>
<%--通知單拆解--%>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css"
        id="cssUrl" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.FrmiStockSplit_content%><%--入库通知单--%>-&gt;<%=Resources.Lang.FrmiStockSplit_content1%><%--作业方式拆解--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">

    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
   
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="1" cellpadding="1"
        border="0">

        <%--<tr valign="top">
                        <td valign="top" colspan="4">
                            <cc1:datagridnavigator3 ID="grdNavigatorINASN_D" runat="server" GridID="grdINASN_D"
                                ShowPageNumber="false" ExcelName="OUTASN_D" IsDbPager="True"  />
                        </td>
                    </tr>--%>

        <tr valign="top">
            <td valign="top">
                  <div style="height: 450px; overflow-x: scroll; width: 100%" id="DivScroll">
                   
                    
                    <asp:GridView ID="grdINASN_D" runat="server" AllowPaging="True" BorderColor="Teal"
                        DataKeyNames="IDS,CINVCODE,IQUANTITY,LessQty" BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                        Width="100%" AutoGenerateColumns="False" OnPageIndexChanged="grdINASN_D_PageIndexChanged"
                        OnPageIndexChanging="grdINASN_D_PageIndexChanging" OnRowDataBound="grdINASN_D_RowDataBound"
                        CssClass="Grid" PageSize="15" OnDataBinding="grdINASN_D_DataBinding">
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
                            <%--子表編號--%>
                        <asp:BoundField DataField="IDS" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmiStockSplit_IDS %>"  Visible="False">
				             <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				         </asp:BoundField>
                            <%--主表編號--%>
				          <asp:BoundField DataField="ID" DataFormatString="{0:0}" HeaderText="<%$ Resources:Lang, FrmiStockSplit_ID %>"  Visible="False">
				              <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				         </asp:BoundField>
				          <asp:BoundField DataField="CINVCODE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmALLOCATE_DReport_MSG5 %>"><%--料號--%>
				              <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="left"  Wrap="False" Width="120px" /> 
				         </asp:BoundField>
				         <asp:BoundField DataField="CINVNAME" DataFormatString="" HeaderText="<%$ Resources:Lang, Common_CinvName %>">
				             <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                             <ItemStyle HorizontalAlign="left"  Wrap="False" Width="150px" /> 
				         </asp:BoundField>
                            <%--總數量--%>
				         <asp:BoundField DataField="IQUANTITY" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmiStockSplit_IQUANTITY %>">
				             <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				         </asp:BoundField>
                            <%--入庫量--%>
				          <asp:BoundField DataField="InBill_Qty" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmiStockSplit_InBill_Qty %>">
				              <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                                <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				         </asp:BoundField>
                            <%--扣帳量--%>
                          <asp:BoundField DataField="InBilled_Qty" DataFormatString="{0:0.00}" HeaderText="<%$ Resources:Lang, FrmiStockSplit_InBilled_Qty %>">
                              <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                                <ItemStyle HorizontalAlign="right"  Wrap="False" /> 
				         </asp:BoundField>

                          <%--可拆解數量--%>
                         <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmiStockSplit_txtLessQty %>">
                            <ControlStyle BorderWidth="0px" />
                            <ItemTemplate>
                               <asp:TextBox CssClass="NormalInputText" ID="txtLessQty" runat="server" Enable="false" Text='<%# Eval("LessQty") %>'  Width="95%"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--拆解數量--%>
                        <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmiStockSplit_txtCjQty %>">
                            <ItemTemplate>
                               <asp:TextBox CssClass="NormalInputText" ID="txtCjQty" runat="server"   Width="95%"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                            <%--ERP單號--%>
				          <asp:BoundField DataField="CERPCODELINE" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmiStockSplit_CERPCODELINE %>">
				              <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                               <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>
                          <asp:BoundField DataField="PO_NUMBERNAME" DataFormatString="" HeaderText="PO_NO">
                           <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                          <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>
                         <asp:BoundField DataField="PO_LINENUMBERNAME" DataFormatString="" HeaderText="PO_LINE_NO">
                             <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                          <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>
                         <asp:BoundField DataField="MSTATUS" DataFormatString="" HeaderText="<%$ Resources:Lang, FrmiStockSplit_MSTATUS %>"><%--狀態--%>
                              <HeaderStyle HorizontalAlign="center"  Wrap="False"/>    
                              <ItemStyle HorizontalAlign="left"  Wrap="False" /> 
				         </asp:BoundField>

                        </Columns>
                    </asp:GridView>
                       <ul class="OneRowStyle">
                                <li>
                                    <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="pagination" LayoutType="Ul" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" OnPageChanged="AspNetPager1_PageChanged"
                                        FirstPageText="<%$ Resources:Lang, Common_FirstPage%>" LastPageText="<%$ Resources:Lang, Common_LastPage%>" NextPageText="<%$ Resources:Lang, Common_NextPage%>" PrevPageText="<%$ Resources:Lang, Common_PrePage %>" ShowPageIndexBox="Never"
                                        AlwaysShow="true" ReverseUrlPageIndex="True" UrlPaging="false"> </webdiyer:aspnetpager>
                                </li>
                                <li>
                                    <div><%=Resources.Lang.Common_TotalPage %> <%=AspNetPager1.RecordCount  %> <%=Resources.Lang.Common_TotalPage1 %></div>
                                </li>
                            </ul>

                </div>    
            </td>
        </tr>

         <tr style="display: none">
                        <td>
                            <asp:Label ID="lblID" runat="server" Text="ID："></asp:Label>
                            <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" Width="95%" MaxLength="50"
                                Enabled="False"></asp:TextBox>
                            <asp:HiddenField ID="hfInAsn_Id" runat="server" />
                            <asp:HiddenField ID="hfIsConfirm" runat="server" Value="0" />
                            <!--是否提示-->
                        </td>
                    </tr>

    </table>
     <table cellspacing="0" cellpadding="0" width="100%" border="0" style=" text-align:center;">
        <tr>
            <td align="center" style=" text-align:center;">
                <asp:Button ID="btnCj" runat="server" CssClass="ButtonConfig" OnClick="btnSave_Click"
                    Text="<%$ Resources:Lang, FrmiStockSplit_btnCj %>" />&nbsp;&nbsp; <%--拆解--%>
                <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>"  CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>

