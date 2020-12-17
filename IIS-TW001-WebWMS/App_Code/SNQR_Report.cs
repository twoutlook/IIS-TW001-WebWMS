using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for SN100X80_Report
/// </summary>
public class SNQR_Report : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;

    private XRBarCode BarSN;
    private XRLabel BarCinvCode;
    private XRLabel xrLabel11;
    private XRLabel LabelDateCode;
    private XRLabel xrLabel1;
    private XRLabel xrLabelPOSO;
    private XRLabel xrLabel7;
    private XRLabel xrLabel4;
    private XRLabel xrLabelSuplier;
    private System.Data.DataTable SNList;
    //private System.Data.DataTable INBILL_D;
    private System.Data.DataColumn dataColumn1;
    private System.Data.DataColumn dataColumn2;
    private System.Data.DataColumn dataColumn3;
    private System.Data.DataColumn dataColumn4;
    private System.Data.DataColumn dataColumn5;
    private System.Data.DataColumn dataColumn6;
    private System.Data.DataColumn dataColumn7;
    private System.Data.DataColumn dataColumn11;     
    private System.Data.DataColumn dataColumn20;
    private System.Data.DataColumn dataColumn21;
    private System.Data.DataColumn dataColumn22;
    private System.Data.DataColumn dataColumn23;
    private System.Data.DataColumn dataColumn24;
   
    private XRLabel xrLabel2;
    private XRLabel xrLabel5;
    private XRLabel xrLabel3;
    private XRLabel xrLabel9;
    private XRLabel xrLabel12;
    private XRLabel xrLabel10;
    private XRLabel xrLabel8;
    private XRLabel xrLabel6;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

    public SNQR_Report()
	{
		InitializeComponent();
		//
		// TODO: Add constructor logic here
		//
	}

    //protected override void OnDataSourceRowChanged(DataSourceRowEventArgs e)
    //{
    //    string strqty = "";
    //    string strpo = "";
    //    this.xrLabelSuplier.Text = this.GetCurrentColumnValue("vendor").ToString();
    //    this.xrLabelSERNum.Text = this.GetCurrentColumnValue("sersnum").ToString();
    //    this.xrLabelPOSO.Text = this.GetCurrentColumnValue("PO").ToString();
    //    var cinvcode=  this.GetCurrentColumnValue("CINVCODE").ToString();
    //    var sn = this.GetCurrentColumnValue("SN").ToString();
    //    var qtyVal= this.GetCurrentColumnValue("QTY").ToString();
    //    var dateCode = this.GetCurrentColumnValue("dateCode").ToString();
       
    //    this.BarCinvCode.Text = cinvcode;
    //    BarSN.Text = sn;
    //    this.SNCode.Text = sn;
    //    strqty = qtyVal;
    //    this.LabelDateCode.Text =dateCode;
    //    if (strqty.Trim() == "")
    //    {
    //        BarNum.Visible = false;
    //    }
    //    else
    //    {
    //        BarNum.Visible = true;
    //         BarNum.Text =strqty;
    //    }
    //    strpo = this.GetCurrentColumnValue("PO").ToString();
    //    //if (strpo.Trim() == "")
    //    //{
    //    //    BarPO.Visible = false;
    //    //}
    //    //else
    //    //{
    //    //    BarPO.Visible = true;
    //    //    BarPO.Text = strpo;
    //    //}
        

    //    base.OnDataSourceRowChanged(e);
    //}


	/// <summary> 
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
            string resourceFileName = "SNQR_Report.resx";
            DevExpress.XtraPrinting.BarCode.QRCodeGenerator qrCodeGenerator1 = new DevExpress.XtraPrinting.BarCode.QRCodeGenerator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelSuplier = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelPOSO = new DevExpress.XtraReports.UI.XRLabel();
            this.BarSN = new DevExpress.XtraReports.UI.XRBarCode();
            this.BarCinvCode = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.LabelDateCode = new DevExpress.XtraReports.UI.XRLabel();
            this.SNList = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn20 = new System.Data.DataColumn();
            this.dataColumn21 = new System.Data.DataColumn();
            this.dataColumn22 = new System.Data.DataColumn();
            this.dataColumn23 = new System.Data.DataColumn();
            this.dataColumn24 = new System.Data.DataColumn();
            
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.SNList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Double;
            this.Detail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8,
            this.xrLabel6,
            this.xrLabel12,
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel5,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel7,
            this.xrLabel4,
            this.xrLabelSuplier,
            this.xrLabel1,
            this.xrLabelPOSO,
            this.BarSN,
            this.BarCinvCode,
            this.xrLabel11,
            this.LabelDateCode});
            this.Detail.Dpi = 254F;
            this.Detail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.Detail.HeightF = 500F;
            this.Detail.KeepTogether = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.StylePriority.UseBorderDashStyle = false;
            this.Detail.StylePriority.UseBorders = false;
            this.Detail.StylePriority.UseFont = false;
            this.Detail.StylePriority.UseTextAlignment = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] 
            {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SNList.QTY")
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "(SNList.boxNum/箱)")
            });
            this.xrLabel8.Dpi = 254F;
            this.xrLabel8.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(359.18F, 281.2085F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(252.2873F, 36.00003F);
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UsePadding = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel6.Dpi = 254F;
            this.xrLabel6.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(273.788F, 281.2084F);
            this.xrLabel6.Multiline = true;
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(85.39203F, 36.00006F);
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UsePadding = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "数量:";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel12.DataBindings.AddRange(
                new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SNList.OCIVCODE")});
            this.xrLabel12.Dpi = 254F;
            this.xrLabel12.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(120.7321F, 71.58333F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(490.7352F, 36F);
            this.xrLabel12.StylePriority.UseBorders = false;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UsePadding = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel10.Dpi = 254F;
            this.xrLabel10.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(11.91053F, 71.58333F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(108.8215F, 35F);
            this.xrLabel10.StylePriority.UseBorders = false;
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UsePadding = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "旧料号:";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel9.Dpi = 254F;
            this.xrLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(359.18F, 317.2085F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(252.2873F, 40.00006F);
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UsePadding = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(273.788F, 317.2084F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(85.39203F, 40.00006F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UsePadding = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "质检:";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SNList.cinvcode_name")});
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(120.7321F, 107.5834F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(490.7351F, 36F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UsePadding = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(11.91061F, 106.5833F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(108.8215F, 36F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UsePadding = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "品名:";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel7.Dpi = 254F;
            this.xrLabel7.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(272.8495F, 245.2085F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(86.33047F, 36F);
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UsePadding = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "PO/SO:";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(273.788F, 209.2085F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(162.1212F, 35.99998F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "供应商/客户:";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabelSuplier
            // 
            this.xrLabelSuplier.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabelSuplier.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SNList.vendor")});
            this.xrLabelSuplier.Dpi = 254F;
            this.xrLabelSuplier.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabelSuplier.LocationFloat = new DevExpress.Utils.PointFloat(435.9092F, 209.2085F);
            this.xrLabelSuplier.Name = "xrLabelSuplier";
            this.xrLabelSuplier.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelSuplier.SizeF = new System.Drawing.SizeF(175.5582F, 35.99998F);
            this.xrLabelSuplier.StylePriority.UseBorders = false;
            this.xrLabelSuplier.StylePriority.UseFont = false;
            this.xrLabelSuplier.StylePriority.UsePadding = false;
            this.xrLabelSuplier.StylePriority.UseTextAlignment = false;
            this.xrLabelSuplier.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(11.91053F, 35.58334F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(108.8215F, 36F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "料号:";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabelPOSO
            // 
            this.xrLabelPOSO.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabelPOSO.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SNList.PO")});
            this.xrLabelPOSO.Dpi = 254F;
            this.xrLabelPOSO.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabelPOSO.LocationFloat = new DevExpress.Utils.PointFloat(359.1802F, 245.2085F);
            this.xrLabelPOSO.Name = "xrLabelPOSO";
            this.xrLabelPOSO.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelPOSO.SizeF = new System.Drawing.SizeF(252.287F, 36F);
            this.xrLabelPOSO.StylePriority.UseBorders = false;
            this.xrLabelPOSO.StylePriority.UseFont = false;
            this.xrLabelPOSO.StylePriority.UsePadding = false;
            this.xrLabelPOSO.StylePriority.UseTextAlignment = false;
            this.xrLabelPOSO.Text = "xrTableCell12";
            this.xrLabelPOSO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // BarSN
            // 
            this.BarSN.Alignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.BarSN.AutoModule = true;
            this.BarSN.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.BarSN.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SN编码：SNList.SN")});
            this.BarSN.Dpi = 254F;
            this.BarSN.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.BarSN.LocationFloat = new DevExpress.Utils.PointFloat(11.91061F, 143.5834F);
            this.BarSN.Module = 5.08F;
            this.BarSN.Name = "BarSN";
            this.BarSN.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 1, 0, 0, 254F);
            this.BarSN.ShowText = false;
            this.BarSN.SizeF = new System.Drawing.SizeF(260.9386F, 260F);
            this.BarSN.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(16, 0, 0, 0, 254F);
            this.BarSN.StylePriority.UseBorderColor = false;
            this.BarSN.StylePriority.UseBorderDashStyle = false;
            this.BarSN.StylePriority.UseBorders = false;
            this.BarSN.StylePriority.UseFont = false;
            this.BarSN.StylePriority.UseForeColor = false;
            this.BarSN.StylePriority.UsePadding = false;
            this.BarSN.StylePriority.UseTextAlignment = false;
            qrCodeGenerator1.CompactionMode = DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.Byte;
            qrCodeGenerator1.ErrorCorrectionLevel = DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel.Q;
            qrCodeGenerator1.Version = DevExpress.XtraPrinting.BarCode.QRCodeVersion.Version2;
            this.BarSN.Symbology = qrCodeGenerator1;
            this.BarSN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // BarCinvCode
            // 
            this.BarCinvCode.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.BarCinvCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SNList.CINVCODE")});
            this.BarCinvCode.Dpi = 254F;
            this.BarCinvCode.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.BarCinvCode.LocationFloat = new DevExpress.Utils.PointFloat(120.7321F, 35.58334F);
            this.BarCinvCode.Name = "BarCinvCode";
            this.BarCinvCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.BarCinvCode.SizeF = new System.Drawing.SizeF(490.7351F, 36F);
            this.BarCinvCode.StylePriority.UseBorders = false;
            this.BarCinvCode.StylePriority.UseFont = false;
            this.BarCinvCode.StylePriority.UsePadding = false;
            this.BarCinvCode.StylePriority.UseTextAlignment = false;
            this.BarCinvCode.Text = "xrTableCell2";
            this.BarCinvCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel11.Dpi = 254F;
            this.xrLabel11.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(272.8492F, 173.2083F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(163.0599F, 36F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UsePadding = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "DateCode:";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // LabelDateCode
            // 
            this.LabelDateCode.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.LabelDateCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SNList.dateCode")});
            this.LabelDateCode.Dpi = 254F;
            this.LabelDateCode.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Bold);
            this.LabelDateCode.LocationFloat = new DevExpress.Utils.PointFloat(435.9092F, 173.2083F);
            this.LabelDateCode.Name = "LabelDateCode";
            this.LabelDateCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.LabelDateCode.SizeF = new System.Drawing.SizeF(175.5581F, 36F);
            this.LabelDateCode.StylePriority.UseBorders = false;
            this.LabelDateCode.StylePriority.UseFont = false;
            this.LabelDateCode.StylePriority.UsePadding = false;
            this.LabelDateCode.StylePriority.UseTextAlignment = false;
            this.LabelDateCode.Text = "dateCode";
            this.LabelDateCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // SNList
            // 
            this.SNList.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn11,
            this.dataColumn20,
            this.dataColumn21,
            this.dataColumn22,
            this.dataColumn23
            });
            this.SNList.TableName = "SNList";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "SN";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "CINVCODE";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "QTY";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "CPOSITIONCODE";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "Supplier";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "PO";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "dateCode";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "vendor";
            // 
            // dataColumn20
            // 
            this.dataColumn20.ColumnName = "sersnum";
            // 
            // dataColumn21
            // 
            this.dataColumn21.ColumnName = "cinvcode_name";
            // 
            // dataColumn22
            // 
            this.dataColumn22.ColumnName = "cspecifications";
            // 
            // dataColumn23
            // 
            this.dataColumn23.ColumnName = "OCIVCODE";
            // 
            // dataColumn24
            // 
            this.dataColumn24.ColumnName = "boxNum";

            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.BorderWidth = 0F;
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.StylePriority.UseBorderWidth = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // SNQR_Report
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 500;
            this.PageWidth = 700;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 0.1F;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.SNList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

     

	#endregion

    public DevExpress.XtraPrinting.BarCode.BarCodeGeneratorBase code128Generator { get; set; }

    public DevExpress.XtraPrinting.BarCode.BarCodeGeneratorBase code39Generator { get; set; }

    public DevExpress.XtraPrinting.BarCode.BarCodeGeneratorBase code39ExtendedGenerator { get; set; }
}
