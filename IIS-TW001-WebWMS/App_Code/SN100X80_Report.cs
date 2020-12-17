using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for SN100X80_Report
/// </summary>
public class SN100X80_Report : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell txtSupplierCompany;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell8;
    private XRTableCell txtCustomer;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell19;
    private XRBarCode BarCinvCode;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell22;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell2;
    private XRBarCode BarNum;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRBarCode BarPO;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell16;
    private XRBarCode BarSN;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell7;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public SN100X80_Report()
	{
		InitializeComponent();
		//
		// TODO: Add constructor logic here
		//
	}

    protected override void OnDataSourceRowChanged(DataSourceRowEventArgs e)
    {
        string strqty = "";
        string strpo = "";
        txtSupplierCompany.Text = this.GetCurrentColumnValue("SUPPLIER").ToString();
       // txtCustomer.Text = this.GetCurrentColumnValue("CUSTOMER").ToString();

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
             BarNum.Text =strqty;
        }
        strpo = this.GetCurrentColumnValue("PO").ToString();
        if (strpo.Trim() == "")
        {
            BarPO.Visible = false;
        }
        else
        {
            BarPO.Visible = true;
            BarPO.Text = strpo;
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
        string resourceFileName = "SN100X80_Report.resx";
        DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
        DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator2 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
        DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator3 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
        DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator4 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.txtSupplierCompany = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.txtCustomer = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.BarCinvCode = new DevExpress.XtraReports.UI.XRBarCode();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.BarNum = new DevExpress.XtraReports.UI.XRBarCode();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.BarPO = new DevExpress.XtraReports.UI.XRBarCode();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.BarSN = new DevExpress.XtraReports.UI.XRBarCode();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
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
        this.Detail.HeightF = 1010F;
        this.Detail.KeepTogether = true;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Dpi = 254F;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(3F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow4,
            this.xrTableRow7,
            this.xrTableRow8,
            this.xrTableRow11,
            this.xrTableRow6,
            this.xrTableRow3});
        this.xrTable2.SizeF = new System.Drawing.SizeF(800F, 1000F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.txtSupplierCompany});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.95609876299261221D;
        // 
        // txtSupplierCompany
        // 
        this.txtSupplierCompany.Dpi = 254F;
        this.txtSupplierCompany.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.txtSupplierCompany.Name = "txtSupplierCompany";
        this.txtSupplierCompany.StylePriority.UseFont = false;
        this.txtSupplierCompany.StylePriority.UseTextAlignment = false;
        this.txtSupplierCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.txtSupplierCompany.Weight = 3D;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.txtCustomer});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 0.95609876299261232D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseFont = false;
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = "客户名称";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell8.Weight = 1.471368744617372D;
        // 
        // txtCustomer
        // 
        this.txtCustomer.Dpi = 254F;
        this.txtCustomer.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.txtCustomer.Name = "txtCustomer";
        this.txtCustomer.StylePriority.UseFont = false;
        this.txtCustomer.Text = " 陽立電子有限公司";
        this.txtCustomer.Weight = 1.5286312553826278D;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17,
            this.xrTableCell19});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1.1951234339379437D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseBorders = false;
        this.xrTableCell17.StylePriority.UseFont = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.Text = Resources.Lang.FrmInbill_CinvCode;//"料号";
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell17.Weight = 0.3942650033186656D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell19.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.BarCinvCode});
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseBorders = false;
        this.xrTableCell19.Weight = 2.6057349966813343D;
        // 
        // BarCinvCode
        // 
        this.BarCinvCode.AutoModule = true;
        this.BarCinvCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.BarCinvCode.Dpi = 254F;
        this.BarCinvCode.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.BarCinvCode.LocationFloat = new DevExpress.Utils.PointFloat(0F, 14F);
        this.BarCinvCode.Module = 5.08F;
        this.BarCinvCode.Name = "BarCinvCode";
        this.BarCinvCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
        this.BarCinvCode.SizeF = new System.Drawing.SizeF(680F, 105F);
        this.BarCinvCode.StylePriority.UseBorders = false;
        this.BarCinvCode.StylePriority.UseFont = false;
        this.BarCinvCode.Symbology = code128Generator1;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell22});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 0.95609877855376468D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseFont = false;
        this.xrTableCell20.Text = " 品名";
        this.xrTableCell20.Weight = 1.471368744617372D;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseFont = false;
        this.xrTableCell22.Text = "  说明书";
        this.xrTableCell22.Weight = 1.528631255382628D;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29,
            this.xrTableCell2,
            this.xrTableCell30,
            this.xrTableCell31});
        this.xrTableRow11.Dpi = 254F;
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 1.1951234002728088D;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell29.Dpi = 254F;
        this.xrTableCell29.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.StylePriority.UseBorders = false;
        this.xrTableCell29.StylePriority.UseFont = false;
        this.xrTableCell29.StylePriority.UseTextAlignment = false;
        this.xrTableCell29.Text = Resources.Lang.Common_IQUANTITY; //"数量";
        this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell29.Weight = 0.3942649950697022D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.BarNum});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.Weight = 1.0771036293997245D;
        // 
        // BarNum
        // 
        this.BarNum.AutoModule = true;
        this.BarNum.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.BarNum.Dpi = 254F;
        this.BarNum.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.BarNum.LocationFloat = new DevExpress.Utils.PointFloat(0F, 14F);
        this.BarNum.Module = 5.08F;
        this.BarNum.Name = "BarNum";
        this.BarNum.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
        this.BarNum.SizeF = new System.Drawing.SizeF(280F, 105F);
        this.BarNum.StylePriority.UseBorders = false;
        this.BarNum.StylePriority.UseFont = false;
        this.BarNum.Symbology = code128Generator2;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell30.Dpi = 254F;
        this.xrTableCell30.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseBorders = false;
        this.xrTableCell30.StylePriority.UseFont = false;
        this.xrTableCell30.StylePriority.UseTextAlignment = false;
        this.xrTableCell30.Text = "PO";
        this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell30.Weight = 0.3241203094708372D;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell31.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.BarPO});
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.StylePriority.UseBorders = false;
        this.xrTableCell31.Weight = 1.204511066059736D;
        // 
        // BarPO
        // 
        this.BarPO.AutoModule = true;
        this.BarPO.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.BarPO.Dpi = 254F;
        this.BarPO.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.BarPO.LocationFloat = new DevExpress.Utils.PointFloat(0F, 14.00003F);
        this.BarPO.Module = 5.08F;
        this.BarPO.Name = "BarPO";
        this.BarPO.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
        this.BarPO.SizeF = new System.Drawing.SizeF(296.203F, 105F);
        this.BarPO.StylePriority.UseBorders = false;
        this.BarPO.StylePriority.UseFont = false;
        this.BarPO.Symbology = code128Generator3;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.xrTableCell16});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1.1951233799586511D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.StylePriority.UseFont = false;
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.Text = "交货日期";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell14.Weight = 0.39426503586620609D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell16.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.BarSN});
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.Weight = 2.6057349641337941D;
        // 
        // BarSN
        // 
        this.BarSN.AutoModule = true;
        this.BarSN.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.BarSN.Dpi = 254F;
        this.BarSN.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.BarSN.LocationFloat = new DevExpress.Utils.PointFloat(1.525879E-05F, 14F);
        this.BarSN.Module = 5.08F;
        this.BarSN.Name = "BarSN";
        this.BarSN.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
        this.BarSN.SizeF = new System.Drawing.SizeF(680F, 105F);
        this.BarSN.StylePriority.UseBorderColor = false;
        this.BarSN.StylePriority.UseBorders = false;
        this.BarSN.StylePriority.UseFont = false;
        this.BarSN.StylePriority.UseForeColor = false;
        this.BarSN.Symbology = code128Generator4;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell7});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1.9121975090185486D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "ACC";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell5.Weight = 1.471368744617372D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseFont = false;
        this.xrTableCell7.Weight = 1.528631255382628D;
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
        this.BottomMargin.HeightF = 0F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // SN100X80_Report
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
        this.PageHeight = 1000;
        this.PageWidth = 810;
        this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        //this.Version = "10.2";
        this.Version = "15.1";
        
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion

    public DevExpress.XtraPrinting.BarCode.BarCodeGeneratorBase code128Generator { get; set; }

    public DevExpress.XtraPrinting.BarCode.BarCodeGeneratorBase code39Generator { get; set; }

    public DevExpress.XtraPrinting.BarCode.BarCodeGeneratorBase code39ExtendedGenerator { get; set; }
}
