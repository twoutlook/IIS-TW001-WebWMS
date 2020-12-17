<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBaseImport.aspx.cs" MasterPageFile="~/Apps/DefaultMasterPage.master"
    Inherits="Apps_Import_FrmBaseImport" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <%--<title>基础数据导入</title>--%>
    <title><%=Resources.Lang.FrmBaseImport_MsgTitle1%></title>

    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <script src="../../Layout/js/iscroll.js" type="text/javascript"></script>
    <script src="../../Layout/js/pad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <style type="text/css">
        li {
            float: left;
            list-style: none;
            margin-left: 10px;
            text-align: center;
        }

        .error {
            color: red;
            font-weight: bold;
        }

        a {
            border: 0px;
            border-style: none;
        }

        img {
            border: 0px;
        }

        .ButtonExcelError {
            border-right: 0px;
            border-top: 0px;
            font-size: 12px;
            font-weight: bold;
            border-left: 0px;
            cursor: hand;
            border-bottom: 0px;
            padding-left: 22px;
            padding-top: 2px;
            font-family: "Arial", "Helvetica", "sans-serif";
            height: 24px;
            color: #3580c9;
            background-image: url(../../Layout/CSS/LG/Images/in_s_bg_excel_error.gif);
            width: 62px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HolderTitle" runat="Server">
    <%=Resources.Lang.FrmBaseImport_MsgTitle2 %>-&gt;<%=Resources.Lang.FrmBaseImport_MsgTitle1 %>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table id="TabMain" style="height: 100%; width: 95%">
        <tr valign="top">
            <td valign="top">
                <%--初始化显示的table--%>
                <table class="InputTable" cellspacing="0" cellpadding="1" width="100%" border="1"
                    id="tabCondition" runat="server">
                    <tr>
                        <th align="left" colspan="5">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                            height="11" alt="" runat="server" id="titleImg" />
                            <%--<%=Resources.Lang.Common_JSCeria%>--%>
                            <%=Resources.Lang.Common_JSCeria%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="imgCollapse" alt="<%=Resources.Lang.Common_Fold%>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr style="display: none">
                        <td colspan="2">
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
                        <td colspan="2">
                            <asp:FileUpload ID="fuFile" runat="server" />
                            &nbsp;
                           
                            
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <br />
                            <asp:Button ID="btnUp" runat="server" CssClass="ButtonExcel" Text="<%$ Resources:Lang, CommonB_ImportFile %>"
                                OnClick="btnUp_Click"></asp:Button><%--导入--%>
                            &nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>"
                                OnClick="btnReturn_Click" /><%--返回--%>
                            <br />
                            <br />
                        </td>
                    </tr>



                    <tr>
                        <td colspan="2">
                            <br />
                            <asp:Label ID="lblIDS" Style="color: Blue; font-size: 14px; font-weight: bold;" runat="server" Text="<%$ Resources:Lang, FrmImportAllocateDetail_lblIDS %>"></asp:Label>：<br /><%--模版下载：--%>

                            <ul>
                                <li>

                                    <a href="/ExcelTemplate/物料信息.xls" id="aprt" runat="server">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Layout/Css/LG/Images/excl.jpg" />
                                        <br />
                                        <%=Resources.Lang.FrmBaseImport_MSG1%><%--物料信息--%>
                                    </a>
                                </li>
                                <li>
                                    <a href="/ExcelTemplate/储位信息.xls" id="a3" runat="server">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Layout/Css/LG/Images/excl.jpg" />
                                        <br />
                                       <%=Resources.Lang.FrmBaseImport_MSG2%> <%--储位信息--%>
                                    </a>
                                </li>
                                <li>
                                    <a href="/ExcelTemplate/仓库资料.xls" id="a5" runat="server">
                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/Layout/Css/LG/Images/excl.jpg" />
                                        <br />
                                       <%=Resources.Lang.FrmBaseImport_MSG3%><%-- 仓库资料--%>
                                    </a>
                                </li>
                                <li>
                                    <a href="/ExcelTemplate/客户信息.xls" id="a4" runat="server">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Layout/Css/LG/Images/excl.jpg" />
                                        <br />
                                        <%=Resources.Lang.FrmBaseImport_MSG4%><%--客户信息--%>
                                    </a>
                                </li>

                                <li>
                                    <a href="/ExcelTemplate/供应商信息.xls" id="agys" runat="server">
                                        <asp:Image ID="imggys" runat="server" ImageUrl="~/Layout/Css/LG/Images/excl.jpg" />
                                        <br />
                                        <%=Resources.Lang.FrmBaseImport_MSG5%> <%--供应商信息--%>
                                    </a>
                                </li>
                                <li>
                                    <a href="/ExcelTemplate/料号和储位、区域关联.xls" id="awcq" runat="server">
                                        <asp:Image ID="imgwaq" runat="server" ImageUrl="~/Layout/Css/LG/Images/excl.jpg" />
                                        <br />
                                        <%=Resources.Lang.FrmBaseImport_MSG6%> <%--料号和储位、区域关联--%>
                                    </a>
                                </li>

                                <li>
                                    <a href="/ExcelTemplate/区域资料.xls" id="aqu" runat="server">
                                        <asp:Image ID="imgqu" runat="server" ImageUrl="~/Layout/Css/LG/Images/excl.jpg" />
                                        <br />
                                         <%=Resources.Lang.FrmBaseImport_MSG7%><%--区域资料--%>
                                    </a>
                                </li>
                               <li>
                                    <a href="/ExcelTemplate/部门信息.xls" id="a2" runat="server">
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Layout/Css/LG/Images/excl.jpg" />
                                        <br />
                                         <%=Resources.Lang.FrmBaseImport_MSG8%><%--部门信息--%>
                                    </a>
                                </li>
                                <li>
                                    <a href="/ExcelTemplate/线体信息.xls" id="a8" runat="server">
                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/Layout/Css/LG/Images/excl.jpg" />
                                        <br />
                                         <%=Resources.Lang.FrmBaseImport_MSG9%><%--线体信息--%>
                                    </a>
                                </li>
                                <li>
                                    <a href="/ExcelTemplate/库存信息.xls" id="a6" runat="server">
                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/Layout/Css/LG/Images/excl.jpg" />
                                        <br />
                                         <%=Resources.Lang.FrmBaseImport_MSG10%><%--库存信息--%>
                                    </a>
                                </li>
                            </ul>

                        </td>

                    </tr>
                    <tr>
                        <td colspan="2">

                            <br />

                            <span style="font-size: 18px; color: #3580c9; font-weight: bold;"><b> <%=Resources.Lang.FrmBaseImport_MSG20%><%--导 入 说 明：--%></b></span><br />
                            <br />


                            <span style="color: Black;">1、<%=Resources.Lang.FrmBaseImport_MSG11%><%--导入文件必须按照模板格式填写，模板文件可从本页面下载--%><br />

                                2、<%=Resources.Lang.FrmBaseImport_MSG12%><%--导入数据量较大时，可能需要时间较长，请耐心等待--%><br />

                                3、<%=Resources.Lang.FrmBaseImport_MSG13%><%--导入过程中若遇到数据格式错误或其他不符时，系统将记录该记录并在导入完成后统一返回--%><br />

                                4、<%=Resources.Lang.FrmBaseImport_MSG14%><%--导入过程中的错误记录并不影响后续正确数据的导入，您可以根据反馈数据修订异常数据后重新上传--%><br />
                            </span>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td valign="top">
                <%--导入后显示的结果table--%>
                <table id="tbResult" runat="server" style="display: none;" class="InputTable" cellspacing="0" cellpadding="1" width="1200px;" border="1">
                    <tr>
                        <th align="left">&nbsp;&nbsp;&nbsp;<img src="../../Layout/CSS/LG/Images/Login/icon1.gif" width="5"
                            height="11" alt="" runat="server" id="Img1" />
                            <%--<%=Resources.Lang.Common_JSCeria%>--%>
                            <%=Resources.Lang.Common_JSCeria%>
                        </th>
                        <th style="border-left-width: 0px" align="right">
                            <script type="text/javascript" language="javascript" src="../../Layout/Collapse.js">
                            </script>
                            <img id="img2" alt="<%=Resources.Lang.Common_Fold %>" src="../../Layout/Css/LG/Images/Up.gif" style="text-align: center"
                                onclick="CollapseCondition('../../');return false;" />
                        </th>
                    </tr>
                    <tr style="display: none">
                        <td colspan="2">
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
                        <td>
                            <asp:Label runat="server" ID="lblTitle" Style="font-size: 18px; color: #3580c9; font-weight: bold; font-family: Arial Normal, Arial"></asp:Label>
                        </td>

                        <td>
                            <asp:Button ID="btnReturn" runat="server" CssClass="ButtonBack" Text="<%$ Resources:Lang, Common_Back %>"
                                OnClick="btnReturn_Click" /><%--返回--%>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 300px; color: Black; font-family: Arial Normal, Arial; font-weight: 400;"><%=Resources.Lang.FrmBaseImport_MSG15%><%--本次导入数据量总计：--%>
                            <asp:Label runat="server" ID="lblTotalCount"></asp:Label>
                            <%=Resources.Lang.FrmBaseImport_MSG16%><%--条--%><br />

                            <%=Resources.Lang.FrmBaseImport_MSG17%><%--正确数据：--%>
                            <asp:Label runat="server" ID="lblOKCount" Style="color: Blue;"></asp:Label>
                            <%=Resources.Lang.FrmBaseImport_MSG16%><%--条--%><br />

                            <%=Resources.Lang.FrmBaseImport_MSG18%><%--异常数据记录：--%>
                            <asp:Label runat="server" ID="lblNGCount" Style="color: Red;"></asp:Label>
                            <%=Resources.Lang.FrmBaseImport_MSG16%><%--条--%> 
                            <br />

                            <%=Resources.Lang.FrmBaseImport_MSG19%><%--异常数据记录如下：--%><br />

                        </td>
                        <td style="width: 900px;">
                            <asp:Button ID="btnExport" runat="server" CssClass="ButtonExcelError" Text="<%$ Resources:Lang, FrmBaseImport_btnExport %>"
                                CausesValidation="false" Width="120px" OnClick="btnExport_Click" /><%--错误信息导出--%>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">

                            <%-- 注意，每一个div都是单独的一个错误信息，所以这里会有10个gridview,请各自添加gridview并实现各自功能--%>
                            <div id="divPart" runat="server">

                                <%--  <asp:GridNavigator3 ID="grdNavigatorPart" runat="server" GridID="gv_Part"
                                    ShowPageNumber="false" IsDbPager="True" ExcelButtonVisible="false" />--%>

                                <div style="height: 600px; overflow-x: scroll; width: 100%" id="DivScroll">
                                    <asp:GridView ID="gv_Part" runat="server" AllowPaging="True" BorderColor="Teal"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                        CssClass="Grid" PageSize="15">
                                        <PagerSettings Visible="False" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"
                                            Wrap="False" />
                                        <PagerStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""
                                            Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmInbill_CinvCode %>"><%--料号--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("料号")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, ShowPO_Div_MSG1 %>"><%--品名--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("品名")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_CommonType %>"><%--类型--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("类型")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_CommonCategory %>"><%--类别--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("类别")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG45 %>"><%--ERP编码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("ERP编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG21 %>"><%--毛重--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("毛重")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG22 %>"><%--净重--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("净重")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG23 %>"><%--重量单位--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("重量单位")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG24 %>"><%--单位--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("单位")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG25 %>"><%--参考单位--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("参考单位")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG26 %>"><%--长--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("长")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG27 %>"><%--宽--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("宽")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG28 %>"><%--高--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("高")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG29 %>"><%--材积--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("材积")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG30 %>"><%--材积单位--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("材积单位")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG31 %>"><%--终止日期--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("终止日期")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_Cstatus %>"><%--状态--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("状态")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINASSITList_MSG3 %>"><%--是否保税--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("是否保税")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG32 %>"><%--是否免检--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("是否免检")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG33 %>"><%--是否预警--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("是否预警")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG34 %>"><%--默认仓库--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("默认仓库")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG35 %>"><%--默认储位--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("默认储位")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG36 %>"><%--默认供应商--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("默认供应商")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG37 %>"><%--上架规则--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("上架规则")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG38 %>"><%--下架规则--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("下架规则")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILL_DEdit_MSG23 %>"><%--条码规则--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("条码规则")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG39 %>"><%--版本--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("版本")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG40 %>"><%--企业编号--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("企业编号")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG41 %>"><%--据点编号--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("据点编号")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG42 %>"><%--应用组织--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("应用组织")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG43 %>"><%--用途--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("用途")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"><%--备注--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("备注")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, CommonB_calias %>"> <%--助记码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("助记码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACE_Report_MSG5 %>"><%--规格--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("规格")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <ul class="OneRowStyle">
                                        <li>
                                            <webdiyer:aspnetpager id="Aspgv_Part" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="Aspgv_Part_PageChanged"
                                                firstpagetext="<%$ Resources:Lang, Common_FirstPage %>" 
                                                lastpagetext="<%$ Resources:Lang, Common_LastPage %>" 
                                                nextpagetext="<%$ Resources:Lang, Common_NextPage %>" 
                                                prevpagetext="<%$ Resources:Lang, Common_PrePage %>" showpageindexbox="Never"
                                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false">
                        </webdiyer:aspnetpager>
                                        </li>
                                        <li>
                                            <div><%=Resources.Lang.Common_TotalPage%><%=Aspgv_Part.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
                                        </li>
                                    </ul>
                                </div>

                            </div>

                            <div id="divCarGoSpace" runat="server">
                             <%--   <asp:DataGridNavigator3 ID="grdCarGoSpaceNavigator" runat="server" GridID="gv_CarGoSpace"
                                    ShowPageNumber="false" IsDbPager="True" ExcelButtonVisible="false" />--%>

                                <div style="height: 600px; overflow-x: scroll; width: 100%" id="Div1">
                                    <asp:GridView ID="gv_CarGoSpace" runat="server" AllowPaging="True" BorderColor="Teal"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                      
                                        CssClass="Grid" PageSize="15">
                                        <PagerSettings Visible="False" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"
                                            Wrap="False" />
                                        <PagerStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""
                                            Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_PartnumNO %>">
                                                <ItemTemplate>
                                                    <div><%# Eval("储位编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_PartnumName %>"><%--储位名称--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("储位名称")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG44 %>"><%--最大量--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("最大量")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, CommonB_calias %>"> <%--助记码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("助记码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG45 %>"><%--ERP编码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("ERP编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACE_Report_MSG4 %>"><%--种类--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("种类")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, WMS_Common_Element_Area %>"><%--区域--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("区域")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG_SuoshuCangkuId %>"><%--所属仓库ID--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("所属仓库ID")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmOUTBILL_DEdit_XianBie %>"><%--线别--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("线别")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IPRIORITY %>"><%--优先级--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("优先级")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG26 %>"><%--长--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("长")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG27 %>"><%--宽--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("宽")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG28 %>"><%--高--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("高")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_IVOLUME %>"><%--体积--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("体积")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG49 %>"><%--用途--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("用途")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEList_lblIPERMITMIX %>"><%--是否允许混放--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("是否允许混放")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="X">
                                                <ItemTemplate>
                                                    <div><%# Eval("X")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Y">
                                                <ItemTemplate>
                                                    <div><%# Eval("Y")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Z">
                                                <ItemTemplate>
                                                    <div><%# Eval("Z")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG_Weight %>"><%--重量--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("重量")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG23 %>"><%--重量单位--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("重量单位")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG29 %>"><%--材积--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("材积")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG30 %>"><%--材积单位--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("材积单位")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG31 %>"><%--终止日期--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("终止日期")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_Cstatus %>"><%--状态--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("状态")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACEEdit_Label1 %>"><%--是否允许调拨--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("是否允许调拨")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"><%--备注--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("备注")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>
                                      <ul class="OneRowStyle">
                                        <li>
                                            <webdiyer:aspnetpager id="AspCarGoSpace" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="AspCarGoSpace_PageChanged"
                                                firstpagetext="<%$ Resources:Lang, Common_FirstPage %>" 
                                                lastpagetext="<%$ Resources:Lang, Common_LastPage %>" 
                                                nextpagetext="<%$ Resources:Lang, Common_NextPage %>" 
                                                prevpagetext="<%$ Resources:Lang, Common_PrePage %>" showpageindexbox="Never"
                                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false">
                        </webdiyer:aspnetpager>
                                        </li>
                                        <li>
                                            <div><%=Resources.Lang.Common_TotalPage%><%=AspCarGoSpace.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
                                        </li>
                                    </ul>
                                </div>

                            </div>

                            <div id="divWareHouse" runat="server">
                                <%--   <cc1:DataGridNavigator3 ID="grdWareHouseNavigator" runat="server" GridID="gv_WareHouse"
                                    ShowPageNumber="false" IsDbPager="True" ExcelButtonVisible="false" />--%>

                                <div style="height: 600px; overflow-x: scroll; width: 100%" id="Div3">
                                    <asp:GridView ID="gv_WareHouse" runat="server" AllowPaging="True" BorderColor="Teal"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                        CssClass="Grid" PageSize="15">
                                        <PagerSettings Visible="False" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"
                                            Wrap="False" />
                                        <PagerStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""
                                            Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_WareHouseCode %>"><%--仓库编码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("仓库编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_WareHouseName %>"><%--仓库名称--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("仓库名称")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, CommonB_WareHouseType %>"><%--仓库类型--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("仓库类型")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, CommonB_CVENDERCODE %>"><%--供应商编码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("供应商编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG24 %>"><%--供应商名称--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("供应商名称")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG50 %>"><%--联系电话--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("电话")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_Cstatus %>"><%--状态--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("状态")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG59 %>"><%--是否保税仓--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("是否保税仓")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG60 %>"><%--是否良品仓--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("是否良品仓")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG40 %>"><%--企业编号--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("企业编号")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG41 %>"><%--据点编号--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("据点编号")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG42 %>"><%--应用组织--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("应用组织")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"><%--备注--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("备注")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    <ul class="OneRowStyle">
                                        <li>
                                            <webdiyer:aspnetpager id="Aspgv_WareHouse" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" 
                                                pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="Aspgv_WareHouse_Part_PageChanged"
                                                firstpagetext="<%$ Resources:Lang, Common_FirstPage %>" 
                                                lastpagetext="<%$ Resources:Lang, Common_LastPage %>" 
                                                nextpagetext="<%$ Resources:Lang, Common_NextPage %>" 
                                                prevpagetext="<%$ Resources:Lang, Common_PrePage %>" showpageindexbox="Never"
                                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false">
                        </webdiyer:aspnetpager>
                                        </li>
                                        <li>
                                            <div><%=Resources.Lang.Common_TotalPage%><%=Aspgv_WareHouse.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
                                        </li>
                                    </ul>
                                </div>

                            </div>

                            <div id="divCLIENT" runat="server">
                                <%--  <cc1:DataGridNavigator3 ID="grdCLIENTNavigator" runat="server" GridID="gv_CLIENT"
                                    ShowPageNumber="false" IsDbPager="True" ExcelButtonVisible="false" />--%>

                                <div style="height: 600px; overflow-x: scroll; width: 100%" id="Div4">
                                    <asp:GridView ID="gv_CLIENT" runat="server" AllowPaging="True" BorderColor="Teal"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                        CssClass="Grid" PageSize="15">
                                        <PagerSettings Visible="False" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"
                                            Wrap="False" />
                                        <PagerStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""
                                            Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG52 %>"><%--客户编码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("客户编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG53 %>"><%--客户名称--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("客户名称")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_Cstatus %>"><%--状态--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("状态")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG51 %>"><%--联系人--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("联系人")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG50 %>"><%--联系电话--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("联系电话")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG48 %>"><%--联系地址--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("联系地址")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG47 %>"><%--客户类型--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("客户类型")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, CommonB_calias %>"> <%--助记码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("助记码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG46 %>"><%--级别--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("级别")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"><%--备注--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("备注")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG45 %>"><%--ERP编码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("ERP编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          

                                        </Columns>
                                    </asp:GridView>
                                    <ul class="OneRowStyle">
                                        <li>
                                            <webdiyer:aspnetpager id="Aspgv_CLIENT" runat="server" cssclass="pagination" layouttype="Ul" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="Aspgv_CLIENT_PageChanged"
                                                firstpagetext="<%$ Resources:Lang, Common_FirstPage %>" 
                                                lastpagetext="<%$ Resources:Lang, Common_LastPage %>" 
                                                nextpagetext="<%$ Resources:Lang, Common_NextPage %>" 
                                                prevpagetext="<%$ Resources:Lang, Common_PrePage %>" showpageindexbox="Never"
                                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false">
                        </webdiyer:aspnetpager>
                                        </li>
                                        <li>
                                            <div><%=Resources.Lang.Common_TotalPage%><%=Aspgv_CLIENT.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
                                        </li>
                                    </ul>
                                </div>

                            </div>

                            <div id="divVENDOR" runat="server">
                               <%-- <cc1:DataGridNavigator3 ID="grdVENDORNavigator" runat="server" GridID="gv_VENDOR"
                                    ShowPageNumber="false" IsDbPager="True" ExcelButtonVisible="false" />--%>

                                <div style="height: 600px; overflow-x: scroll; width: 100%" id="Div5">
                                    <asp:GridView ID="gv_VENDOR" runat="server" AllowPaging="True" BorderColor="Teal"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                       
                                        CssClass="Grid" PageSize="15">
                                        <PagerSettings Visible="False" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"
                                            Wrap="False" />
                                        <PagerStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""
                                            Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, CommonB_CVENDERCODE %>"><%--供应商编码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("供应商编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmCommandQuery_Report_MSG24 %>"><%--供应商名称--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("供应商名称")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_Cstatus %>"><%--状态--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("状态")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG51 %>"><%--联系人--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("联系人")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG50 %>"><%--联系电话--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("联系电话")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG48 %>"><%--联系地址--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("联系地址")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG49 %>"><%--供应商类型--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("供应商类型")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, CommonB_calias %>"> <%--助记码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("助记码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG46 %>"><%--级别--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("级别")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmINBILLEdit_MSG9 %>"><%--备注--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("备注")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG45 %>"><%--ERP编码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("ERP编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG40 %>"><%--企业编号--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("企业编号")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG41 %>"><%--据点编号--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("据点编号")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG42 %>"><%--应用组织--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("应用组织")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    <ul class="OneRowStyle">
                                        <li>
                                            <webdiyer:aspnetpager id="Aspgv_VENDOR" runat="server" cssclass="pagination" layouttype="Ul" 
                                                pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="Aspgv_VENDOR_PageChanged"
                                                firstpagetext="<%$ Resources:Lang, Common_FirstPage %>" 
                                                lastpagetext="<%$ Resources:Lang, Common_LastPage %>" 
                                                nextpagetext="<%$ Resources:Lang, Common_NextPage %>" 
                                                prevpagetext="<%$ Resources:Lang, Common_PrePage %>" showpageindexbox="Never"
                                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false">
                        </webdiyer:aspnetpager>
                                        </li>
                                        <li>
                                            <div><%=Resources.Lang.Common_TotalPage%><%=Aspgv_VENDOR.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
                                        </li>
                                    </ul>
                                </div>

                            </div>

                            <div id="divBASE_PART_CARGOSPACE" runat="server">
                                <%--<cc1:DataGridNavigator3 ID="grdPARTCARGOSPACENavigator" runat="server" GridID="gv_PARTCARGOSPACEN"
                                    ShowPageNumber="false" IsDbPager="True" ExcelButtonVisible="false" />--%>

                                <div style="height: 600px; overflow-x: scroll; width: 100%" id="Div2">
                                    <asp:GridView ID="gv_PARTCARGOSPACEN" runat="server" AllowPaging="True" BorderColor="Teal"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                       
                                        CssClass="Grid" PageSize="15">
                                        <PagerSettings Visible="False" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"
                                            Wrap="False" />
                                        <PagerStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""
                                            Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG54 %>"><%--区域名称--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("区域名称")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, CommonB_CinvCode %>"><%--物料编码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("物料编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_PartnumNO %>">
                                                <ItemTemplate>
                                                    <div><%# Eval("储位编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_WareHouseCode %>"><%--仓库编码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("仓库编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <ul class="OneRowStyle">
                                        <li>
                                            <webdiyer:aspnetpager id="Aspgv_PARTCARGOSPACEN" runat="server" cssclass="pagination" layouttype="Ul" 
                                                pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="Aspgv_PARTCARGOSPACEN_PageChanged"
                                                firstpagetext="<%$ Resources:Lang, Common_FirstPage %>" 
                                                lastpagetext="<%$ Resources:Lang, Common_LastPage %>" 
                                                nextpagetext="<%$ Resources:Lang, Common_NextPage %>" 
                                                prevpagetext="<%$ Resources:Lang, Common_PrePage %>" showpageindexbox="Never"
                                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false">
                        </webdiyer:aspnetpager>
                                        </li>
                                        <li>
                                            <div><%=Resources.Lang.Common_TotalPage%><%=Aspgv_PARTCARGOSPACEN.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                            <div id="divArea" runat="server" visible="False">
                               <%-- <cc1:DataGridNavigator3 ID="grdAreaNavigator" runat="server" GridID="gv_Area"
                                    ShowPageNumber="false" IsDbPager="True" ExcelButtonVisible="false" />--%>

                                <div style="height: 600px; overflow-x: scroll; width: 100%" id="Div6">
                                    <asp:GridView ID="gv_Area" runat="server" AllowPaging="True" BorderColor="Teal"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                        CssClass="Grid" PageSize="15" >
                                        <PagerSettings Visible="False" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"
                                            Wrap="False" />
                                        <PagerStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""
                                            Wrap="False" />
                                         <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG54 %>"><%--区域名称--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("区域名称")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACE_Report_MSG7 %>"><%--备料储位编号--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("备料储位编号")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBASE_CARGOSPACE_Report_MSG6 %>"><%--备料储位名称--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("备料储位名称")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG55 %>"><%--是否超发--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("是否超发")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                     <ul class="OneRowStyle">
                                        <li>
                                            <webdiyer:aspnetpager id="Aspgv_Area" runat="server" cssclass="pagination" layouttype="Ul" 
                                                pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="Aspgv_Area_PageChanged"
                                                firstpagetext="<%$ Resources:Lang, Common_FirstPage %>" 
                                                lastpagetext="<%$ Resources:Lang, Common_LastPage %>" 
                                                nextpagetext="<%$ Resources:Lang, Common_NextPage %>" 
                                                prevpagetext="<%$ Resources:Lang, Common_PrePage %>" showpageindexbox="Never"
                                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false">
                        </webdiyer:aspnetpager>
                                        </li>
                                        <li>
                                            <div><%=Resources.Lang.Common_TotalPage%><%=Aspgv_Area.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
                                        </li>
                                    </ul>

                                </div>

                            </div>


                             <div id="divDepartment" runat="server" visible="False">
                               <%-- <cc1:DataGridNavigator3 ID="grdAreaNavigator" runat="server" GridID="gv_Area"
                                    ShowPageNumber="false" IsDbPager="True" ExcelButtonVisible="false" />--%>

                                <div style="height: 600px; overflow-x: scroll; width: 100%" id="Div8">
                                    <asp:GridView ID="gv_Department" runat="server" AllowPaging="True" BorderColor="Teal"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                        CssClass="Grid" PageSize="15" >
                                        <PagerSettings Visible="False" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"
                                            Wrap="False" />
                                        <PagerStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""
                                            Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG56 %>"><%--部门编码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("departmentno")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG57 %>"><%--部门名称--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("departmentname")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                     <ul class="OneRowStyle">
                                        <li>
                                            <webdiyer:aspnetpager id="Aspgv_Department" runat="server" cssclass="pagination" layouttype="Ul" 
                                                pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="Aspgv_Department_PageChanged"
                                                firstpagetext="<%$ Resources:Lang, Common_FirstPage %>" 
                                                lastpagetext="<%$ Resources:Lang, Common_LastPage %>" 
                                                nextpagetext="<%$ Resources:Lang, Common_NextPage %>" 
                                                prevpagetext="<%$ Resources:Lang, Common_PrePage %>" showpageindexbox="Never"
                                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false">
                        </webdiyer:aspnetpager>
                                        </li>
                                        <li>
                                            <div><%=Resources.Lang.Common_TotalPage%><%=Aspgv_Department.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
                                        </li>
                                    </ul>

                                </div>

                            </div>



                                   <div id="divLine" runat="server" visible="False">
                               <%-- <cc1:DataGridNavigator3 ID="grdAreaNavigator" runat="server" GridID="gv_Area"
                                    ShowPageNumber="false" IsDbPager="True" ExcelButtonVisible="false" />--%>

                                <div style="height: 600px; overflow-x: scroll; width: 100%" id="Div10">
                                    <asp:GridView ID="gv_Line" runat="server" AllowPaging="True" BorderColor="Teal"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                        CssClass="Grid" PageSize="15" >
                                        <PagerSettings Visible="False" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"
                                            Wrap="False" />
                                        <PagerStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""
                                            Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, FrmBaseImport_MSG58 %>"><%--线体--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("LINEID")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                     <ul class="OneRowStyle">
                                        <li>
                                            <webdiyer:aspnetpager id="Aspgv_Line" runat="server" cssclass="pagination" layouttype="Ul" 
                                                pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="Aspgv_Line_PageChanged"
                                                firstpagetext="<%$ Resources:Lang, Common_FirstPage %>" 
                                                lastpagetext="<%$ Resources:Lang, Common_LastPage %>" 
                                                nextpagetext="<%$ Resources:Lang, Common_NextPage %>" 
                                                prevpagetext="<%$ Resources:Lang, Common_PrePage %>" showpageindexbox="Never"
                                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false">
                        </webdiyer:aspnetpager>
                                        </li>
                                        <li>
                                            <div><%=Resources.Lang.Common_TotalPage%><%=Aspgv_Line.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
                                        </li>
                                    </ul>

                                </div>

                            </div>




                            <div id="divStock" runat="server" visible="False">
                             <%--   <cc1:DataGridNavigator3 ID="grdStockNavigator" runat="server" GridID="gv_Stock"
                                    ShowPageNumber="false" IsDbPager="True" ExcelButtonVisible="false" />--%>

                                <div style="height: 600px; overflow-x: scroll; width: 100%" id="Div9">
                                    <asp:GridView ID="gv_Stock" runat="server" AllowPaging="True" BorderColor="Teal"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                        CssClass="Grid" PageSize="15" >
                                        <PagerSettings Visible="False" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                        <RowStyle HorizontalAlign="Left" Wrap="False" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"
                                            Wrap="False" />
                                        <PagerStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass=""
                                            Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_WareHouseCode %>">
                                                <ItemTemplate>
                                                    <div><%# Eval("仓库编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_PartnumNO %>">
                                                <ItemTemplate>
                                                    <div><%# Eval("储位编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, CommonB_CinvCode %>"><%--物料编码--%>
                                                <ItemTemplate>
                                                    <div><%# Eval("物料编码")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Lang, Common_NUM %>">
                                                <ItemTemplate>
                                                    <div><%# Eval("数量")%> </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                      <ul class="OneRowStyle">
                                        <li>
                                            <webdiyer:aspnetpager id="Aspgv_Stock" runat="server" cssclass="pagination" layouttype="Ul" 
                                                pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active" onpagechanged="Aspgv_Stock_PageChanged"
                                                firstpagetext="<%$ Resources:Lang, Common_FirstPage %>" 
                                                lastpagetext="<%$ Resources:Lang, Common_LastPage %>" 
                                                nextpagetext="<%$ Resources:Lang, Common_NextPage %>" 
                                                prevpagetext="<%$ Resources:Lang, Common_PrePage %>" showpageindexbox="Never"
                                                alwaysshow="true" reverseurlpageindex="True" urlpaging="false">
                        </webdiyer:aspnetpager>
                                        </li>
                                        <li>
                                            <div><%=Resources.Lang.Common_TotalPage%><%=Aspgv_Stock.RecordCount  %> <%=Resources.Lang.Common_TotalPage1%></div>
                                        </li>
                                    </ul>

                                </div>

                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>


        <script type="text/javascript">
           


        </script>
    </table>
</asp:Content>
