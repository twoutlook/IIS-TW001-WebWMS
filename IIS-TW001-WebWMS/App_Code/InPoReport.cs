using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for InAsnReport1
/// </summary>
public class InPoReport : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTable xrTable2;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRLabel xrLabel1;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTable xrTable3;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private System.Data.DataSet dataSet1;
    private System.Data.DataTable INPO;
    private System.Data.DataTable INPO_D;
    private System.Data.DataColumn dataColumn1;
    private System.Data.DataColumn dataColumn2;
    private System.Data.DataColumn dataColumn3;
    private System.Data.DataColumn dataColumn5;
    private System.Data.DataColumn dataColumn6;
    private System.Data.DataColumn dataColumn8;
    private System.Data.DataColumn dataColumn9;
    private System.Data.DataColumn dataColumn10;
    private System.Data.DataColumn dataColumn11;
    private System.Data.DataColumn dataColumn12;
    private System.Data.DataColumn dataColumn13;
    private System.Data.DataColumn dataColumn14;
    private System.Data.DataColumn dataColumn15;
    private System.Data.DataColumn dataColumn16;
    private System.Data.DataColumn dataColumn17;
    private System.Data.DataColumn dataColumn18;
    private System.Data.DataColumn dataColumn19;
    private System.Data.DataColumn dataColumn21;
    private System.Data.DataColumn dataColumn22;
    private System.Data.DataColumn dataColumn20;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private System.Data.DataColumn dataColumn23;
    private System.Data.DataColumn dataColumn4;
    private System.Data.DataColumn dataColumn7;
    private System.Data.DataColumn dataColumn24;
    private System.Data.DataColumn dataColumn25;
    private System.Data.DataColumn dataColumn26;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private System.Data.DataColumn dataColumn27;
    private XRLine xrLine1;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell46;
    private System.Data.DataColumn dataColumn28;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public InPoReport()
	{
		InitializeComponent();
		//
		// TODO: Add constructor logic here
		//
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
            string resourceFileName = "InPoReport.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.dataSet1 = new System.Data.DataSet();
            this.INPO = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn20 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn24 = new System.Data.DataColumn();
            this.dataColumn25 = new System.Data.DataColumn();
            this.INPO_D = new System.Data.DataTable();
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataColumn13 = new System.Data.DataColumn();
            this.dataColumn14 = new System.Data.DataColumn();
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.dataColumn19 = new System.Data.DataColumn();
            this.dataColumn21 = new System.Data.DataColumn();
            this.dataColumn22 = new System.Data.DataColumn();
            this.dataColumn23 = new System.Data.DataColumn();
            this.dataColumn26 = new System.Data.DataColumn();
            this.dataColumn27 = new System.Data.DataColumn();
            this.dataColumn28 = new System.Data.DataColumn();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.INPO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.INPO_D)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable3.SizeF = new System.Drawing.SizeF(790F, 25F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell46,
            this.xrTableCell42,
            this.xrTableCell33,
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell38});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO_D.RNUM")});
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.Text = "xrTableCell46";
            this.xrTableCell46.Weight = 0.40757058539832713D;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "dataSet1DataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.INPO,
            this.INPO_D});
            // 
            // INPO
            // 
            this.INPO.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn20,
            this.dataColumn4,
            this.dataColumn7,
            this.dataColumn24,
            this.dataColumn25});
            this.INPO.TableName = "INPO";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "PONO";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "POTYPE";
            this.dataColumn2.ColumnName = "POTYPE";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "PODATE";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "VENDORID";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "VENDORNAME";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "CURRENCY";
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "PAYMENTTERM";
            // 
            // dataColumn10
            // 
            this.dataColumn10.ColumnName = "SHIPFROM";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "SHIPTO";
            // 
            // dataColumn20
            // 
            this.dataColumn20.ColumnName = "ID";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "STATUS";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "SOURCE";
            // 
            // dataColumn24
            // 
            this.dataColumn24.ColumnName = "CREATETIME";
            // 
            // dataColumn25
            // 
            this.dataColumn25.ColumnName = "CREATEOWNER";
            // 
            // INPO_D
            // 
            this.INPO_D.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn12,
            this.dataColumn13,
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn17,
            this.dataColumn18,
            this.dataColumn19,
            this.dataColumn21,
            this.dataColumn22,
            this.dataColumn23,
            this.dataColumn26,
            this.dataColumn27,
            this.dataColumn28});
            this.INPO_D.TableName = "INPO_D";
            // 
            // dataColumn12
            // 
            this.dataColumn12.ColumnName = "CINVCODE";
            // 
            // dataColumn13
            // 
            this.dataColumn13.ColumnName = "CINVNAME";
            // 
            // dataColumn14
            // 
            this.dataColumn14.ColumnName = "IQUANTITY";
            // 
            // dataColumn15
            // 
            this.dataColumn15.ColumnName = "POLINE";
            // 
            // dataColumn16
            // 
            this.dataColumn16.ColumnName = "UNIT";
            // 
            // dataColumn17
            // 
            this.dataColumn17.ColumnName = "PRICE";
            // 
            // dataColumn18
            // 
            this.dataColumn18.ColumnName = "STATUS";
            // 
            // dataColumn19
            // 
            this.dataColumn19.ColumnName = "QC";
            // 
            // dataColumn21
            // 
            this.dataColumn21.ColumnName = "IDS";
            // 
            // dataColumn22
            // 
            this.dataColumn22.ColumnName = "ID";
            // 
            // dataColumn23
            // 
            this.dataColumn23.ColumnName = "CREATETIME";
            // 
            // dataColumn26
            // 
            this.dataColumn26.ColumnName = "CREATEOWNER";
            // 
            // dataColumn27
            // 
            this.dataColumn27.ColumnName = "total";
            // 
            // dataColumn28
            // 
            this.dataColumn28.ColumnName = "RNUM";
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO_D.POLINE")});
            this.xrTableCell42.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseBorders = false;
            this.xrTableCell42.StylePriority.UseFont = false;
            this.xrTableCell42.Text = "xrTableCell42";
            this.xrTableCell42.Weight = 0.5076417217114001D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO_D.CINVCODE")});
            this.xrTableCell33.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseBorders = false;
            this.xrTableCell33.StylePriority.UseFont = false;
            this.xrTableCell33.Text = "xrTableCell33";
            this.xrTableCell33.Weight = 1.2335664668824709D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO_D.CINVNAME")});
            this.xrTableCell34.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseBorders = false;
            this.xrTableCell34.StylePriority.UseFont = false;
            this.xrTableCell34.StylePriority.UseTextAlignment = false;
            this.xrTableCell34.Text = "xrTableCell34";
            this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell34.Weight = 1.4850082205814532D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO_D.IQUANTITY")});
            this.xrTableCell35.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseBorders = false;
            this.xrTableCell35.StylePriority.UseFont = false;
            this.xrTableCell35.Text = "xrTableCell35";
            this.xrTableCell35.Weight = 0.47791457803093168D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO_D.UNIT")});
            this.xrTableCell36.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseBorders = false;
            this.xrTableCell36.StylePriority.UseFont = false;
            this.xrTableCell36.Text = "xrTableCell36";
            this.xrTableCell36.Weight = 0.53344015633122044D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO_D.PRICE")});
            this.xrTableCell37.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseBorders = false;
            this.xrTableCell37.StylePriority.UseFont = false;
            this.xrTableCell37.Text = "xrTableCell37";
            this.xrTableCell37.Weight = 0.66447730383073322D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO_D.total")});
            this.xrTableCell38.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseBorders = false;
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.Text = "xrTableCell38";
            this.xrTableCell38.Weight = 0.56379872834804234D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrTable2,
            this.xrTable1,
            this.xrLine1});
            this.TopMargin.HeightF = 259F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(7.947286E-06F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(790F, 51.04167F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = Resources.Lang.FrmInPoEdit_Content1;//"采购单";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.BorderWidth = 1F;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 219F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable2.SizeF = new System.Drawing.SizeF(790F, 40F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseBorderWidth = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell45,
            this.xrTableCell41,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell31,
            this.xrTableCell29,
            this.xrTableCell28,
            this.xrTableCell32});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1.6D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.StylePriority.UseFont = false;
            this.xrTableCell45.Text = Resources.Lang.FrmINASNEMTList_MSG4; //"序号";
            this.xrTableCell45.Weight = 0.40755351198437106D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell41.BorderWidth = 1F;
            this.xrTableCell41.CanShrink = true;
            this.xrTableCell41.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.StylePriority.UseBorderColor = false;
            this.xrTableCell41.StylePriority.UseBorderWidth = false;
            this.xrTableCell41.StylePriority.UseFont = false;
            this.xrTableCell41.Text = Resources.Lang.CommonB_IPOLINE; //"PO项次";
            this.xrTableCell41.Weight = 0.50761222611253931D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTableCell25.BorderWidth = 1F;
            this.xrTableCell25.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseBorderDashStyle = false;
            this.xrTableCell25.StylePriority.UseBorderWidth = false;
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.Text = Resources.Lang.FrmInbill_CinvCode;//"料号";
            this.xrTableCell25.Weight = 1.2335419061241886D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseFont = false;
            this.xrTableCell26.Text = Resources.Lang.ShowPO_Div_MSG1; //"品名";
            this.xrTableCell26.Weight = 1.4850049306877338D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.Text = Resources.Lang.Common_IQUANTITY; //"数量";
            this.xrTableCell31.Weight = 0.477989338029583D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseFont = false;
            this.xrTableCell29.Text = Resources.Lang.FrmBASE_PARTEdit_lblCUNITS; //"单位";
            this.xrTableCell29.Weight = 0.53344015633122033D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseFont = false;
            this.xrTableCell28.Text = Resources.Lang.FrmInPoEdit_PRICE; //"单价";
            this.xrTableCell28.Weight = 0.6644768500522894D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell32.Multiline = true;
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseFont = false;
            this.xrTableCell32.Text = Resources.Lang.FrmInPoEdit_TOTAL; //"小计";
            this.xrTableCell32.Weight = 0.56379884179265294D;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 51.04167F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow4,
            this.xrTableRow3,
            this.xrTableRow7});
            this.xrTable1.SizeF = new System.Drawing.SizeF(790F, 125F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell5,
            this.xrTableCell3,
            this.xrTableCell6,
            this.xrTableCell4});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.BorderWidth = 2F;
            this.xrTableCell1.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseBorderWidth = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "PO";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.89999992370605475D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.PONO")});
            this.xrTableCell2.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.Weight = 1.2041667938232421D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BorderWidth = 2F;
            this.xrTableCell5.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorderWidth = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = Resources.Lang.FrmInPoEdit_lblITYPE; //"PO类型";
            this.xrTableCell5.Weight = 1.2083332824707032D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.POTYPE")});
            this.xrTableCell3.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.Weight = 1.6041665649414063D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.BorderWidth = 2F;
            this.xrTableCell6.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseBorderWidth = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.Text = Resources.Lang.FrmInPoEdit_lblCERPCODE; //"PO日期";
            this.xrTableCell6.Weight = 1.0208334350585937D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.PODATE")});
            this.xrTableCell4.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.Weight = 1.3125D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.BorderWidth = 2F;
            this.xrTableCell7.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorderWidth = false;
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.Text = Resources.Lang.FrmWAREHOUSEList_LEADERCODE; //"供应商编码";
            this.xrTableCell7.Weight = 0.89999992370605475D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.VENDORID")});
            this.xrTableCell8.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.Weight = 1.2041667938232421D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.BorderWidth = 2F;
            this.xrTableCell9.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorderWidth = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.Text = Resources.Lang.FrmWAREHOUSEList_LEADER; //"供应商名称";
            this.xrTableCell9.Weight = 1.2083332824707032D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.VENDORNAME")});
            this.xrTableCell10.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.Text = "xrTableCell10";
            this.xrTableCell10.Weight = 1.6041665649414063D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.BorderWidth = 2F;
            this.xrTableCell11.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorderWidth = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.Text = Resources.Lang.Common_Cstatus; //"状态";
            this.xrTableCell11.Weight = 1.0208334350585937D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.STATUS")});
            this.xrTableCell12.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.Text = "xrTableCell12";
            this.xrTableCell12.Weight = 1.3125D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.BorderWidth = 2F;
            this.xrTableCell19.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseBorderWidth = false;
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.Text = Resources.Lang.FrmINBILLEdit_MSG2; //"币别";
            this.xrTableCell19.Weight = 0.89999992370605475D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.CURRENCY")});
            this.xrTableCell20.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.Text = "xrTableCell20";
            this.xrTableCell20.Weight = 1.2041667938232421D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.BorderWidth = 2F;
            this.xrTableCell21.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseBorderWidth = false;
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.Text = Resources.Lang.FrmInPoEdit_Label1; //"付款条件";
            this.xrTableCell21.Weight = 1.2083332824707032D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.PAYMENTTERM")});
            this.xrTableCell22.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.Text = "xrTableCell22";
            this.xrTableCell22.Weight = 1.6041665649414063D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.BorderWidth = 2F;
            this.xrTableCell23.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseBorderWidth = false;
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.Text = Resources.Lang.FrmInPoEdit_Label2; //"出货来源";
            this.xrTableCell23.Weight = 1.0208334350585937D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.SHIPFROM")});
            this.xrTableCell24.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.Text = "xrTableCell24";
            this.xrTableCell24.Weight = 1.3125D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.BorderWidth = 2F;
            this.xrTableCell13.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorderWidth = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.Text = Resources.Lang.FrmInPoEdit_Label4; //"出货地";
            this.xrTableCell13.Weight = 0.89999992370605475D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.SHIPTO")});
            this.xrTableCell14.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.Text = "xrTableCell14";
            this.xrTableCell14.Weight = 1.2041667938232421D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.BorderWidth = 2F;
            this.xrTableCell15.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseBorderWidth = false;
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.Text = Resources.Lang.Common_CreateUser; //"制单人";
            this.xrTableCell15.Weight = 1.2083332824707032D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.CREATEOWNER")});
            this.xrTableCell16.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.Text = "xrTableCell16";
            this.xrTableCell16.Weight = 1.6041665649414063D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.BorderWidth = 2F;
            this.xrTableCell17.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseBorderWidth = false;
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.Text = Resources.Lang.Common_CreateDate; //"制单日期";
            this.xrTableCell17.Weight = 1.0208334350585937D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.CREATETIME")});
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.Text = "xrTableCell18";
            this.xrTableCell18.Weight = 1.3125D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCell30,
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell43,
            this.xrTableCell44});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.BorderWidth = 2F;
            this.xrTableCell27.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseBorderWidth = false;
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.Text = Resources.Lang.FrmInPoEdit_lblCAUDITPERSONCODE; //"单据来源";
            this.xrTableCell27.Weight = 0.89999992370605475D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "INPO.SOURCE")});
            this.xrTableCell30.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseFont = false;
            this.xrTableCell30.Text = "xrTableCell30";
            this.xrTableCell30.Weight = 1.2041667938232421D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.BorderWidth = 2F;
            this.xrTableCell39.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseBorderWidth = false;
            this.xrTableCell39.StylePriority.UseFont = false;
            this.xrTableCell39.Weight = 1.2083332824707032D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseFont = false;
            this.xrTableCell40.Weight = 1.6041665649414063D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.BorderWidth = 2F;
            this.xrTableCell43.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.StylePriority.UseBorderWidth = false;
            this.xrTableCell43.StylePriority.UseFont = false;
            this.xrTableCell43.Weight = 1.0208334350585937D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Font = new System.Drawing.Font("宋体", 9.75F);
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseFont = false;
            this.xrTableCell44.Weight = 1.3125D;
            // 
            // xrLine1
            // 
            this.xrLine1.LineWidth = 3;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(7.947286E-06F, 176.0417F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(790F, 42.95831F);
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 40F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // InPoReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataSource = this.dataSet1;
            this.Margins = new System.Drawing.Printing.Margins(28, 30, 259, 40);
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.INPO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.INPO_D)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion

}
