<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestLED.aspx.cs" Inherits="Apps_RD_TestLED" %>
<%@ Register TagName="ShowDIV" Src="~/Apps/BASE/ShowDIV.ascx" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    .watermark {
    background: #FFAAFF;
}

.popupControl {
    background-color:#AAD4FF;
    position:absolute;
    visibility:hidden;
    border-style:solid;
    border-color: Black;
    border-width: 2px;
}

.modalBackground {
    background-color:Gray;
    filter:alpha(opacity=70);
    opacity:0.7;
}

.modalPopup {
    background-color:#ffffdd;
    border-width:3px;
    border-style:solid;
    border-color:Gray;
    padding:3px;
    width:250px;
}

.sampleStyleA {
    background-color:#FFF;
}

.sampleStyleB {
    background-color:#FFF;
    font-family:monospace;
    font-size:10pt;
    font-weight:bold;
}

.sampleStyleC {
    background-color:#ddffdd;
    font-family:sans-serif;
    font-size:10pt;
    font-style:italic;
}

.sampleStyleD {
    background-color:Blue;
    color:White;
    font-family:Arial;
    font-size:10pt;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
<div>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

    <div>

    <asp:TextBox ID="txtMSG" runat="server" TextMode="MultiLine" Text="<%$ Resources:Lang, TestLED_txtMSG%>"></asp:TextBox><%--这是WEB版本测试LED程序...--%>
    <br/>

    <asp:DropDownList ID="ddlID" runat="server">
      <asp:ListItem Text="<%$ Resources:Lang, TestLED_ddlID1%>" Value="2" Selected="True"></asp:ListItem><%--左屏幕--%>
      <asp:ListItem Text="<%$ Resources:Lang, TestLED_ddlID2%>" Value="1"></asp:ListItem><%--右屏幕--%>
    </asp:DropDownList>

    <br/>
    <br/>
      <asp:Button runat="server" ID="btnSendTextToLed"  Text="<%$ Resources:Lang, TestLED_btnSendTextToLed%>" 
                    onclick="btnSendTextToLed_Click"/><%--LED发送数据--%>
                    <asp:Label ID="lblShow" runat="server"></asp:Label>
    </div>

    <br/>
    <br/>
     <asp:TextBox ID="txtCINVCODE" runat="server" CssClass="NormalInputText" Width="25%"
                                MaxLength="20"></asp:TextBox>
     <asp:ImageButton ID="btnImgShow" runat="server" ImageUrl="../../Images/Search.gif" class="select"  OnClick="imgSelect_Click" />

     <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" style="display:none">
        <p>
        <asp:Label ID="Label1" runat="server" BackColor="Blue" ForeColor="White" Style="position: relative"
        Text="<%$ Resources:Lang, TestLED_Label1%>"></asp:Label>&nbsp;</p><%--信息提示--%>
        <uc:ShowDIV ID="ShowDIV1" runat="server"  OnGetInfoEvent="SelectOK_Click" />
        <p style="text-align:center;">
        <asp:Button ID="Button1" runat="server" Text="OK" ></asp:Button>
        <asp:Button ID="Button2" runat="server" Text="Cancel"></asp:Button>
        </p>
      </asp:Panel>
      <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="btnImgShow" 
        PopupControlID="Panel2" BackgroundCssClass="modalBackground" DropShadow="true" 
        OkControlID="Button1" OnOkScript="onOk()" CancelControlID="CancelButton" />
    

    

</div>
    </form>
</body>
</html>
