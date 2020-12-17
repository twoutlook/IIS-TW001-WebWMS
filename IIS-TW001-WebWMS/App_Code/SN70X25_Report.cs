using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for SN70X25_Report
/// </summary>
public class SN70X25_Report : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell2;
    private XRTableCell txtCinvcode;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRBarCode BarCinvCode;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell10;
    private XRTableCell txtSN;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRBarCode BarSN;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell6;
    private XRTableCell txtNum;
    private XRTableCell xrTableCell7;
    private XRBarCode BarNum;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell3;
    private XRTableCell txtCposition;
    private XRTableCell xrTableCell5;
    private XRBarCode BarCposition;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public SN70X25_Report()
	{
		InitializeComponent();
		//
		// TODO: Add constructor logic here
		//
	}
	
     protected override void OnDataSourceRowChanged(DataSourceRowEventArgs e)
     {
         //设置
         string strqty = "";
         string strcposi = "";

       
         txtCinvcode.Text = this.GetCurrentColumnValue("CINVCODE").ToString();
         txtSN.Text = this.GetCurrentColumnValue("SN").ToString();
         txtNum.Text = this.GetCurrentColumnValue("QTY").ToString();
         txtCposition.Text = this.GetCurrentColumnValue("CPOSITIONCODE").ToString();
         BarCinvCode.Text = this.GetCurrentColumnValue("CINVCODE").ToString();
         BarSN.Text = this.GetCurrentColumnValue("SN").ToString();
         strqty = this.GetCurrentColumnValue("QTY").ToString();
         if (strqty.Trim() == "")
         {
             BarNum.Visible = false;
         }
         else
         {
             BarNum.Visible = true;
             BarNum.Text = strqty;
         }
         strcposi = this.GetCurrentColumnValue("CPOSITIONCODE").ToString();
         if (strcposi.Trim() == "")
         {
             BarCposition.Visible = false;
         }
         else
         {
             BarCposition.Visible = true;
             BarCposition.Text = strcposi;
         }
         

         base.OnDataSourceRowChanged(e);
     }

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
        string resourceFileName = "SN70X25_Report.resx";
        DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
        DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator2 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
        DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator3 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
        DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator4 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.txtCinvcode = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.BarCinvCode = new DevExpress.XtraReports.UI.XRBarCode();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.txtSN = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.BarSN = new DevExpress.XtraReports.UI.XRBarCode();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.txtNum = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.BarNum = new DevExpress.XtraReports.UI.XRBarCode();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.txtCposition = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.BarCposition = new DevExpress.XtraReports.UI.XRBarCode();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 300F;
        this.Detail.KeepTogether = true;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow7,
            this.xrTableRow6,
            this.xrTableRow5,
            this.xrTableRow4,
            this.xrTableRow3});
        this.xrTable2.SizeF = new System.Drawing.SizeF(700F, 280F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.txtCinvcode});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Font = new System.Drawing.Font("宋体", 9F);
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = " 料号";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell2.Weight = 0.59178574698311948D;
        // 
        // txtCinvcode
        // 
        this.txtCinvcode.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.txtCinvcode.Dpi = 254F;
        this.txtCinvcode.Font = new System.Drawing.Font("宋体", 9F);
        this.txtCinvcode.Name = "txtCinvcode";
        this.txtCinvcode.StylePriority.UseBorders = false;
        this.txtCinvcode.StylePriority.UseFont = false;
        this.txtCinvcode.StylePriority.UseTextAlignment = false;
        this.txtCinvcode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.txtCinvcode.Weight = 2.4082142530168809D;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrTableCell13});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.Weight = 0.37633927481515067D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.BarCinvCode});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UseFont = false;
        this.xrTableCell13.Weight = 2.6236607251848496D;
        // 
        // BarCinvCode
        // 
        this.BarCinvCode.AutoModule = true;
        this.BarCinvCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.BarCinvCode.Dpi = 254F;
        this.BarCinvCode.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.BarCinvCode.LocationFloat = new DevExpress.Utils.PointFloat(3.814697E-05F, 3.814697E-05F);
        this.BarCinvCode.Module = 44F;
        this.BarCinvCode.Name = "BarCinvCode";
        this.BarCinvCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254F);
        this.BarCinvCode.ShowText = false;
        this.BarCinvCode.SizeF = new System.Drawing.SizeF(600F, 45F);
        this.BarCinvCode.StylePriority.UseBorders = false;
        this.BarCinvCode.StylePriority.UseFont = false;
        this.BarCinvCode.StylePriority.UseTextAlignment = false;
        this.BarCinvCode.Symbology = code128Generator1;
        this.BarCinvCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.txtSN});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Font = new System.Drawing.Font("宋体", 9F);
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.StylePriority.UseFont = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = " S/N:";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell10.Weight = 0.59178574698311959D;
        // 
        // txtSN
        // 
        this.txtSN.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.txtSN.Dpi = 254F;
        this.txtSN.Font = new System.Drawing.Font("宋体", 9F);
        this.txtSN.Name = "txtSN";
        this.txtSN.StylePriority.UseBorders = false;
        this.txtSN.StylePriority.UseFont = false;
        this.txtSN.StylePriority.UseTextAlignment = false;
        this.txtSN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.txtSN.Weight = 2.4082142530168809D;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell9});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.Weight = 0.37633924211774561D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.BarSN});
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.Weight = 2.6236607578822544D;
        // 
        // BarSN
        // 
        this.BarSN.AutoModule = true;
        this.BarSN.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
        this.BarSN.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.BarSN.Dpi = 254F;
        this.BarSN.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.BarSN.LocationFloat = new DevExpress.Utils.PointFloat(7.629395E-06F, 0F);
        this.BarSN.Module = 44F;
        this.BarSN.Name = "BarSN";
        this.BarSN.Padding = new DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254F);
        this.BarSN.ShowText = false;
        this.BarSN.SizeF = new System.Drawing.SizeF(600F, 40F);
        this.BarSN.StylePriority.UseBorderDashStyle = false;
        this.BarSN.StylePriority.UseBorders = false;
        this.BarSN.StylePriority.UseFont = false;
        this.BarSN.StylePriority.UseTextAlignment = false;
        this.BarSN.Symbology = code128Generator2;
        this.BarSN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6,
            this.txtNum,
            this.xrTableCell7});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Font = new System.Drawing.Font("宋体", 8F);
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "数量:";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell6.Weight = 0.37633927481515062D;
        // 
        // txtNum
        // 
        this.txtNum.Dpi = 254F;
        this.txtNum.Font = new System.Drawing.Font("宋体", 8F);
        this.txtNum.Name = "txtNum";
        this.txtNum.StylePriority.UseFont = false;
        this.txtNum.StylePriority.UseTextAlignment = false;
        this.txtNum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.txtNum.Weight = 0.99433020455496635D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.BarNum});
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.StylePriority.UseFont = false;
        this.xrTableCell7.Weight = 1.6293305206298827D;
        // 
        // BarNum
        // 
        this.BarNum.AutoModule = true;
        this.BarNum.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.BarNum.Dpi = 254F;
        this.BarNum.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.BarNum.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.BarNum.Module = 44F;
        this.BarNum.Name = "BarNum";
        this.BarNum.Padding = new DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254F);
        this.BarNum.ShowText = false;
        this.BarNum.SizeF = new System.Drawing.SizeF(367.9897F, 40F);
        this.BarNum.StylePriority.UseBorders = false;
        this.BarNum.StylePriority.UseFont = false;
        this.BarNum.StylePriority.UseTextAlignment = false;
        this.BarNum.Symbology = code128Generator3;
        this.BarNum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.txtCposition,
            this.xrTableCell5});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Font = new System.Drawing.Font("宋体", 8F);
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "储位:";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell3.Weight = 0.37633930751255584D;
        // 
        // txtCposition
        // 
        this.txtCposition.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.txtCposition.Dpi = 254F;
        this.txtCposition.Font = new System.Drawing.Font("宋体", 8F);
        this.txtCposition.Name = "txtCposition";
        this.txtCposition.StylePriority.UseBorders = false;
        this.txtCposition.StylePriority.UseFont = false;
        this.txtCposition.StylePriority.UseTextAlignment = false;
        this.txtCposition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.txtCposition.Weight = 0.99433005741664349D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.BarCposition});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.Weight = 1.629330635070801D;
        // 
        // BarCposition
        // 
        this.BarCposition.AutoModule = true;
        this.BarCposition.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.BarCposition.Dpi = 254F;
        this.BarCposition.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.BarCposition.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4F);
        this.BarCposition.Module = 44F;
        this.BarCposition.Name = "BarCposition";
        this.BarCposition.Padding = new DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254F);
        this.BarCposition.ShowText = false;
        this.BarCposition.SizeF = new System.Drawing.SizeF(367.9897F, 40F);
        this.BarCposition.StylePriority.UseBorders = false;
        this.BarCposition.StylePriority.UseFont = false;
        this.BarCposition.StylePriority.UseTextAlignment = false;
        this.BarCposition.Symbology = code128Generator4;
        this.BarCposition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
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
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 3F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // SN70X25_Report
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 3);
        this.PageHeight = 300;
        this.PageWidth = 700;
        this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        //this.Version = "10.2";
        this.Version = "15.1";
        
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion

    public DevExpress.XtraPrinting.BarCode.BarCodeGeneratorBase code39Generator { get; set; }

    public DevExpress.XtraPrinting.BarCode.BarCodeGeneratorBase code128Generator { get; set; }

    public BarCodeGeneratorBase code39ExtendedGenerator { get; set; }
}
