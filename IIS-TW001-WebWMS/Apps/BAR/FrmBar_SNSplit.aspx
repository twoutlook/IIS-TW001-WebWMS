<%@ Page Title="SN条码拆分" Language="C#" MasterPageFile="~/Apps/DefaultMasterPage.master"
    AutoEventWireup="true" CodeFile="FrmBar_SNSplit.aspx.cs" Inherits="Apps_BAR_FrmBar_SNSplit" %>


<%@ Register Src="~/Apps/BAR/ShowSN_Split_Div.ascx" TagName="ShowSN_Div" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
         <script src="../../Layout/Js/jquery-1.4.1.min.js" type="text/javascript"></script>  
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../Layout/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script> 
   
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />

    <link href="../../Layout/CSS/LG/Page.css" rel="Stylesheet" type="text/css" id="cssUrl"
        runat="server" />
    <link href="../../Layout/jquery-ui-1.9.2.custom/css/smoothness/jquery-ui-1.9.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Layout/Calendar/calendar.js"></script>
    <link href="../../Layout/Calendar/calendar-blue.css" rel="Stylesheet" />
    <script  type="text/javascript">
        function GetData() {
           
            var vcinvcode = document.getElementById("<%=txtCinvCode.ClientID %>").value;
            var vcposition = document.getElementById("<%=txtCption.ClientID %>").value;      
            var vtxt="";
            var number = 0;
            
            var gdview = document.getElementById("<%=grdSNDetial.ClientID %>");
            for (var i = 1; i < gdview.rows.length; i++)//要从1开始,从0则会读出表头的值 
            {
                var controls = gdview.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                
                if (controls.type == "checkbox") {
                    if (controls.checked == true) {
                        var vsn = gdview.rows[i].cells[1].getElementsByTagName("INPUT")[0].value;
                        var vQty = gdview.rows[i].cells[2].getElementsByTagName("INPUT")[0].value;
                        vtxt = vtxt + vcinvcode + ";" + vsn + ";" + vQty + ";" + vcposition + ";" + vpo + ";" + "1;" + "\r\n";
                        number = number + 1;
                    }
                }

            }
            if (number == 0) {
                alert("请选择需要打印的项！");
                return false;
            } else 
            {
               
                WriteToTxtFile(vtxt);
            }
        }

        function WriteToTxtFile(txt) {
            if (window.ActiveXObject) {
                var fso = new ActiveXObject("Scripting.FileSystemObject");
                var filespec = "d:\\wms_lable\\PRINT.txt";
                var filebat = "d:\\wms_lable\\print.bat";
                if (!(fso.FileExists(filebat))) {
                    alert("文件d:\wms_lable\print.bat不存在!");
                    return false;
                }

                var asd = fso.CreateTextFile(filespec, true);
                asd.WriteLine(txt);
                asd.close();
                SNprintList(); //执行打印
            }
            else {
                alert("请使用IE并正确配置浏览器！");
                return false;
            }
        }

        function SNprintList() 
        {
            var vbat = new ActiveXObject("wscript.shell");
            vbat.run("D:\\wms_lable\\print.bat");
           
        }

    </script>

    <script type="text/javascript">
        function GridAddRow() {
            var fg = $("#<%=grdSNDetial.ClientID %>");
            fg.append("<tr><td></td><td></td></tr>");
        }

        //设置文本框数量
        function SetQtyValue(qty, oldqty) {
            var qtyvalue = document.all(qty).value;
            var oldqtyvalue = document.all(oldqty).value;
           
            //校验拆分数量
            if(!oldqtyvalue.match(/^(:?(:?\d+.\d+)|(:?\d+))$/)) {
                //if (isNaN(qtyvalue)) {
                    alert("拆分数量必须为浮点型数据！");
                    document.all(qty).value = "";
                    return;
                }
                if (qtyvalue < 0) {
                    alert("拆分数量不能小于0 ！");
                    document.all(qty).value = "";
                    return;
                }
                //if (Number(qtyvalue) - parseInt(qtyvalue) > 0) {
                //    alert("拆分数量必须为整数 ！");
                //    document.all(qty).value = "";
                //    return;
                //}
                var numqty = Number(qtyvalue);
                var numoldqty = Number(oldqtyvalue);

                if (numqty > numoldqty) {
                    alert("拆解数量不能大于SN数量 ！");
                    document.all(qty).value = "";
                    return;
                }
            // var gdview = document.getElementById("<%=grdSNDetial.ClientID %>");
             //var sumQty = 0;   
             //for (var i = 1; i < gdview.rows.length; i++)//要从1开始,从0则会读出表头的值 
            // {
             //   var inputqty = gdview.rows[i].cells[2].getElementsByTagName("INPUT")[0].value;
            //
            //    if (inputqty != "") {
            //        var putnum = Number(inputqty);
            //        sumQty = sumQty + putnum;
            //    }
            // }
            //if (sumQty > numoldqty) {
            //    alert("拆解总数量[" + sumQty + "]不能大于SN数量[" + numoldqty + "] ！");
            //    document.all(qty).value = "";
            //    return;          
            //}
            //else {
            //        alert("请选择需要拆解的SN");
            //        document.all(qty).value = "";
            //        return;
               
            //}
        }
    </script>
    <script type="text/javascript">
        function doSplit(e) {
            var keynum;
            if (window.event) // IE
            {
                keynum = e.keyCode;
                if (keynum == 13) {
                    event.keyCode = 9;                 
                    var btn = document.getElementById("<%=btnOK.ClientID %>");
                    var cfbtn = document.getElementById("<%=btnNew.ClientID %>");
                    btn.click();
                    cfbtn.focus();
                   
                }
                else {
                    return;
                }
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                keynum = e.which;
                if (keynum == 13) {                
                    var btn = document.getElementById("<%=btnOK.ClientID %>");
                    var cfbtn = document.getElementById("<%=btnNew.ClientID %>");
                    btn.click();
                    cfbtn.focus();
                }
                else {
                    return;
                }
            }
        }

        function Show(divID) {         
            disponse_div(event, document.all(divID));
        }
</script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HolderTitle" runat="Server">
    条码管理-&gt;SN条码拆分
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
   <%-- <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" EnablePartialRendering="true"
        EnablePageMethods="true" runat="server"> </ajaxToolkit:ToolkitScriptManager>--%>
            <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
   
    <uc1:ShowSN_Div ID="showsn" runat="server" />
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
                                    <asp:Label ID="lblCINVCODE" runat="server" Text="SN编码："></asp:Label>
                                </td>
                                <td style="width: 21%;white-space: nowrap">
                                    <asp:TextBox CssClass="NormalInputText" ID="txtSnCode" runat="server" Width="95%" 
                                    onkeypress="doSplit(event)" ></asp:TextBox>
                                    <img id="selectCin" alt="" onclick="Show('<%= showsn.GetDivName %>');" src="../../Images/Search.gif" class="select"/>
                                </td>
                              <%--  <td style="display: none">
                                     <asp:Button ID="btnOK" runat="server" Text="OK" onclick="btnOK_Click" />
                                </td>--%>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label6" runat="server" Text="数量："></asp:Label>
                                </td>
                                <td style="width: 21%">
                                    <asp:TextBox ID="txtOldQty" runat="server" CssClass="NormalInputText" 
                                        Width="95%" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                               <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="Label1" runat="server" Text="储位："></asp:Label>
                                </td>
                                <td style="width: 21%">
                                    <asp:TextBox ID="txtCption" runat="server" CssClass="NormalInputText" Width="95%"></asp:TextBox>
                                </td>
                              <%--  <td style="display: none">
                                     
                                </td>--%>
                                <td class="InputLabel" style="width: 13%">
                                    <asp:Label ID="lblQty" runat="server" Text="料号："></asp:Label>
                                </td>
                                <td style="width: 21%">
                                    <asp:TextBox ID="txtCinvCode" runat="server" CssClass="NormalInputText" 
                                        Width="95%" ></asp:TextBox>
                                </td>
                            </tr>    
                            <tr style="display:none">
                                <td>
                                    <asp:TextBox ID="txtID" runat="server" CssClass="NormalInputText" 
                                        Width="95%" Enabled="False"></asp:TextBox> 
                                </td>
                            </tr>                       
                          
                        </table>
                    </td>
                </tr>
            </table>        
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                      
                        <asp:Button ID="btnCf" runat="server" CssClass="ButtonConfig" Text="拆分" OnClick="btnSave_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="返回" CausesValidation="false" />
                          <asp:Button ID="btnprint" runat="server" CssClass=" ButtonPrint" Text="打印" 
                            onclick="btnprint_Click" Visible="false" />&nbsp;<asp:Button ID="btnSplitprint" 
                            runat="server" CssClass=" ButtonPrint" Text="打印" 
                            Visible="false" OnClientClick="return GetData()" />
                             &nbsp;
                    </td>
                </tr>
                </table>
   
    
            <table id="TabMain0" style="width: 100%" runat="server">
                 <tr valign="top">
                    <td valign="top" align="left">
                      <%--  <asp:Button ID="btnNew" runat="server" Style="border-style: none; border-color: inherit;
                            border-width: 0px; font-size: 12px; font-weight: bold; cursor: hand; padding-left: 22px;
                            height: 24px; padding-top: 2px; color: #3580c9; background-image: url('../../Layout/Css/LG/Images/in_s_bg_add.gif');"
                            Text="" Width="34px" OnClick="btnNew_Click"></asp:Button>&nbsp;&nbsp;--%>
                           <asp:Button ID="btnNew" runat="server" CssClass="ButtonAdd" Text="" OnClick="btnNew_Click"></asp:Button>&nbsp;&nbsp;
                     <%--   <asp:Button ID="btnDeleteSn" runat="server" Style="border-right: 0px; border-top: 0px;
                            font-size: 12px; font-weight: bold; border-left: 0px; cursor: hand; border-bottom: 0px;
                            padding-left: 22px; padding-top: 2px; height: 24px; color: #3580c9; background-image: url('../../Layout/Css/LG/Images/in_s_bg_del.gif');
                            width: 34px;" Text="" CssClass="ButtonDel" OnClick="btnDeleteSn_Click" />--%>
                         <asp:Button ID="btnDeleteSn" runat="server" CssClass="ButtonDel" Text="" OnClick="btnDeleteSn_Click"></asp:Button>&nbsp;&nbsp;
                        <asp:Button ID="btnOK" runat="server" Text="OK" onclick="btnOK_Click"   style="display: none" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div style="height: 300px; overflow-x: scroll; width: 100%" id="DivScroll">
                            <asp:GridView ID="grdSNDetial" runat="server" AllowPaging="True" BorderColor="Teal"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Width="100%" AutoGenerateColumns="False"
                                CssClass="Grid" PageSize="15" DataKeyNames="ID,TYPE" OnRowDataBound="grdSNDetial_RowDataBound">
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
                                     <asp:BoundField DataField="TYPE" DataFormatString="" HeaderText="" Visible="false">
                                        <HeaderStyle HorizontalAlign="center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="left" Wrap="False" />
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="SN编码">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSN" runat="server" Width="95%" Text='<%# Eval("NEW_SN") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" Width="50%" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="False" Width="50%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="数量">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" Width="95%" Text='<%# Eval("NEW_SN_QTY") %>' DataFormatString="{0:N2}"></asp:TextBox>
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
      
</asp:Content>
