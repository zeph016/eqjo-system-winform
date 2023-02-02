namespace FGCIJOROSystem.Reports.rReports
{
    partial class rptEquipmentSummaryOnly
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptEquipmentSummaryOnly));
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector1 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector2 = new Telerik.Reporting.Drawing.DescendantSelector();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.txtDate = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.pictureBox2 = new Telerik.Reporting.PictureBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.table1 = new Telerik.Reporting.Table();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.JOROReportDataSource = new Telerik.Reporting.ObjectDataSource();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox35 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.779118537902832D), Telerik.Reporting.Drawing.Unit.Inch(0.34062504768371582D));
            this.textBox1.Style.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.textBox1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.StyleName = "Normal.TableHeader";
            this.textBox1.Value = "EQUIPMENT CODE";
            // 
            // textBox5
            // 
            this.textBox5.KeepTogether = false;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.220881462097168D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox5.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(213)))));
            this.textBox5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.StyleName = "";
            this.textBox5.Value = "= Fields.EquipmentName + \" | COUNT : \" + COUNT(Fields.EquipmentCode) ";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.8637998104095459D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox24,
            this.txtDate,
            this.textBox23,
            this.pictureBox2,
            this.textBox17,
            this.textBox6});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.3000791072845459D), Telerik.Reporting.Drawing.Unit.Inch(0.30000028014183044D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1685924530029297D), Telerik.Reporting.Drawing.Unit.Inch(0.47244107723236084D));
            this.textBox24.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox24.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox24.Style.Font.Bold = true;
            this.textBox24.Style.Font.Name = "Calibri";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox24.Style.Font.Underline = false;
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "SUMMARY OF EQUIPMENT";
            // 
            // txtDate
            // 
            this.txtDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.46875D), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D));
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.69999825954437256D), Telerik.Reporting.Drawing.Unit.Inch(0.47244113683700562D));
            this.txtDate.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtDate.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtDate.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtDate.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtDate.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.txtDate.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.txtDate.Style.Font.Bold = true;
            this.txtDate.Style.Font.Italic = false;
            this.txtDate.Style.Font.Name = "Arial";
            this.txtDate.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.txtDate.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtDate.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtDate.Value = "Date Printed";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:D}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.1688270568847656D), Telerik.Reporting.Drawing.Unit.Inch(0.29993501305580139D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8311731815338135D), Telerik.Reporting.Drawing.Unit.Inch(0.47244113683700562D));
            this.textBox23.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox23.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox23.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox23.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox23.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox23.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Italic = false;
            this.textBox23.Style.Font.Name = "Agency FB";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = "=Now()";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D));
            this.pictureBox2.MimeType = "image/jpeg";
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3000001907348633D), Telerik.Reporting.Drawing.Unit.Inch(0.47244131565093994D));
            this.pictureBox2.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pictureBox2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pictureBox2.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pictureBox2.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pictureBox2.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pictureBox2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.pictureBox2.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pictureBox2.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pictureBox2.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pictureBox2.Value = ((object)(resources.GetObject("pictureBox2.Value")));
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.1688270568847656D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8207563161849976D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.textBox17.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "= PageNumber + \'/\' + PageCount";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.90000009536743164D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(8D), Telerik.Reporting.Drawing.Unit.Inch(0.22748015820980072D));
            this.textBox6.Style.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.textBox6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "=\"Filter: \" + Parameters.FilterBy.Value";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(3.2374916076660156D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox28,
            this.textBox27,
            this.textBox2,
            this.table1});
            this.detail.KeepTogether = false;
            this.detail.Name = "detail";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.7000001072883606D), Telerik.Reporting.Drawing.Unit.Inch(0.87452477216720581D));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3894357681274414D), Telerik.Reporting.Drawing.Unit.Inch(0.1999996155500412D));
            this.textBox28.Style.Font.Underline = true;
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Value = "= Parameters.PreparedBy.Value";
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9312573790084571E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.67231369018554688D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0833333730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20213215053081513D));
            this.textBox27.Value = "Prepared By:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.7000001072883606D), Telerik.Reporting.Drawing.Unit.Inch(1.0746034383773804D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3894357681274414D), Telerik.Reporting.Drawing.Unit.Inch(0.1999996155500412D));
            this.textBox2.Style.Font.Underline = false;
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "= Parameters.PreparedByPos.Value";
            // 
            // table1
            // 
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(2.779118537902832D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D)));
            this.table1.Body.SetCellContent(0, 0, this.textBox15);
            tableGroup1.ReportItem = this.textBox1;
            this.table1.ColumnGroups.Add(tableGroup1);
            this.table1.ColumnHeadersPrintOnEveryPage = true;
            this.table1.Corner.SetCellContent(0, 0, this.textBox7);
            this.table1.DataSource = this.JOROReportDataSource;
            this.table1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7,
            this.textBox15,
            this.textBox1,
            this.textBox5});
            this.table1.KeepTogether = false;
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05D));
            this.table1.Name = "table1";
            tableGroup5.Name = "group3";
            tableGroup4.ChildGroups.Add(tableGroup5);
            tableGroup4.Name = "group2";
            tableGroup3.ChildGroups.Add(tableGroup4);
            tableGroup3.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.EquipmentCode"));
            tableGroup3.Name = "rowGroup1";
            tableGroup3.Sortings.Add(new Telerik.Reporting.Sorting("= Fields.EquipmentCode", Telerik.Reporting.SortDirection.Asc));
            tableGroup2.ChildGroups.Add(tableGroup3);
            tableGroup2.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.EquipmentName"));
            tableGroup2.Name = "rowGroup2";
            tableGroup2.ReportItem = this.textBox5;
            tableGroup2.Sortings.Add(new Telerik.Reporting.Sorting("= Fields.EquipmentName", Telerik.Reporting.SortDirection.Asc));
            this.table1.RowGroups.Add(tableGroup2);
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(8D), Telerik.Reporting.Drawing.Unit.Inch(0.54062503576278687D));
            this.table1.StyleName = "Normal.TableNormal";
            // 
            // textBox15
            // 
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.779118537902832D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox15.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(213)))));
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.StyleName = "";
            this.textBox15.Value = "= Fields.EquipmentCode";
            // 
            // textBox7
            // 
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.220881462097168D), Telerik.Reporting.Drawing.Unit.Inch(0.34062504768371582D));
            this.textBox7.Style.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.textBox7.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.StyleName = "Normal.TableHeader";
            this.textBox7.Value = "EQUIPMENT NAME";
            // 
            // JOROReportDataSource
            // 
            this.JOROReportDataSource.DataSource = typeof(FGCIJOROSystem.Domain.Reports.clsJOROReports);
            this.JOROReportDataSource.Name = "JOROReportDataSource";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.47605082392692566D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox35});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0500004291534424D), Telerik.Reporting.Drawing.Unit.Inch(0.18742157518863678D));
            this.textBox35.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox35.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox35.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox35.Style.Font.Bold = true;
            this.textBox35.Style.Font.Italic = false;
            this.textBox35.Style.Font.Name = "Arial";
            this.textBox35.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox35.Value = "Paper Size: 8.5x11";
            // 
            // rptEquipmentSummaryOnly
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "rptEquipmentSummaryOnly";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(8.5D), Telerik.Reporting.Drawing.Unit.Inch(11D));
            reportParameter1.Name = "FilterBy";
            reportParameter2.Name = "PreparedBy";
            reportParameter3.Name = "PreparedByPos";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.Table), "Normal.TableNormal")});
            styleRule2.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            descendantSelector1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableBody")});
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector1});
            styleRule3.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            descendantSelector2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableHeader")});
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector2});
            styleRule4.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Inch;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox txtDate;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.PictureBox pictureBox2;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.ObjectDataSource JOROReportDataSource;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.Table table1;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox35;
    }
}