namespace FGCIJOROSystem.Reports.rAttendance
{
    partial class rptDriversAttendance
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptDriversAttendance));
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector1 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector2 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector3 = new Telerik.Reporting.Drawing.DescendantSelector();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.tbDatePrinted = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.txtDate = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.crosstab2 = new Telerik.Reporting.Crosstab();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.objDriversAttendance = new Telerik.Reporting.ObjectDataSource();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox35 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0:d}";
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.73749822378158569D), Telerik.Reporting.Drawing.Unit.Inch(0.20833322405815125D));
            this.textBox7.Style.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.StyleName = "Normal.TableGroup";
            this.textBox7.Value = "= Fields.RODate";
            // 
            // textBox8
            // 
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0638856887817383D), Telerik.Reporting.Drawing.Unit.Inch(0.13194437325000763D));
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.StyleName = "Normal.TableGroup";
            this.textBox8.Value = "= Fields.ServiceDriverName";
            // 
            // textBox3
            // 
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.22222216427326202D), Telerik.Reporting.Drawing.Unit.Inch(0.13194437325000763D));
            this.textBox3.Style.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(6D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.StyleName = "Normal.TableGroup";
            this.textBox3.Value = "= RowNumber()";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(3.5560004711151123D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.tbDatePrinted,
            this.textBox1,
            this.textBox17,
            this.txtDate,
            this.textBox2,
            this.pictureBox1,
            this.textBox14});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // tbDatePrinted
            // 
            this.tbDatePrinted.Format = "{0:G}";
            this.tbDatePrinted.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(22.092708587646484D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.tbDatePrinted.Name = "tbDatePrinted";
            this.tbDatePrinted.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.3177213668823242D), Telerik.Reporting.Drawing.Unit.Cm(0.50800013542175293D));
            this.tbDatePrinted.Style.Font.Name = "Microsoft Sans Serif";
            this.tbDatePrinted.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.tbDatePrinted.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.tbDatePrinted.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.tbDatePrinted.Value = "=\"Date Printed: \" + Now()";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.09375D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.40000057220459D), Telerik.Reporting.Drawing.Unit.Inch(0.47244107723236084D));
            this.textBox1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Calibri";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox1.Style.Font.Underline = false;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "MAINTENANCE DRIVER\'S ATTENDANCE";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(10.791666984558106D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7062505483627319D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.textBox17.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "= PageNumber + \'/\' + PageCount";
            // 
            // txtDate
            // 
            this.txtDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(8.5D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992185831069946D), Telerik.Reporting.Drawing.Unit.Inch(0.47244113683700562D));
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
            this.txtDate.Value = "CUT-OFF DATE";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(9.5D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9998431205749512D), Telerik.Reporting.Drawing.Unit.Inch(0.47244113683700562D));
            this.textBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox2.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Italic = true;
            this.textBox2.Style.Font.Name = "Arial";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "= Format(\"{0:MMMM dd, yyyy}\", CDate(Parameters.DateFrom.Value)) + \" - \" + Format(" +
    "\"{0:MMMM dd, yyyy}\", CDate(Parameters.DateTo.Value))";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.pictureBox1.MimeType = "image/jpeg";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0936710834503174D), Telerik.Reporting.Drawing.Unit.Inch(0.47244131565093994D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pictureBox1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pictureBox1.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pictureBox1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pictureBox1.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pictureBox1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.pictureBox1.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pictureBox1.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pictureBox1.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // textBox14
            // 
            this.textBox14.Format = "{0:dd.MMM.yy}";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(1.1724808216094971D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(12.499881744384766D), Telerik.Reporting.Drawing.Unit.Inch(0.22748015820980072D));
            this.textBox14.Style.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.textBox14.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Style.Visible = true;
            this.textBox14.Value = "=\"Filter: \" + Parameters.Filter.Value";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(3.2584717273712158D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.crosstab2,
            this.textBox29,
            this.textBox28,
            this.textBox27});
            this.detail.KeepTogether = false;
            this.detail.Name = "detail";
            // 
            // crosstab2
            // 
            this.crosstab2.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.73749816417694092D)));
            this.crosstab2.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.13194431364536285D)));
            this.crosstab2.Body.SetCellContent(0, 0, this.textBox10);
            tableGroup1.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.RODate"));
            tableGroup1.Name = "RODate1";
            tableGroup1.ReportItem = this.textBox7;
            tableGroup1.Sortings.Add(new Telerik.Reporting.Sorting("= Fields.RODate", Telerik.Reporting.SortDirection.Asc));
            this.crosstab2.ColumnGroups.Add(tableGroup1);
            this.crosstab2.ColumnHeadersPrintOnEveryPage = true;
            this.crosstab2.Corner.SetCellContent(0, 1, this.textBox9);
            this.crosstab2.Corner.SetCellContent(0, 0, this.textBox4);
            this.crosstab2.DataSource = this.objDriversAttendance;
            this.crosstab2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox9,
            this.textBox10,
            this.textBox7,
            this.textBox8,
            this.textBox3});
            this.crosstab2.KeepTogether = false;
            this.crosstab2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.crosstab2.Name = "crosstab2";
            tableGroup3.Name = "group";
            tableGroup3.ReportItem = this.textBox8;
            tableGroup2.ChildGroups.Add(tableGroup3);
            tableGroup2.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.ServiceDriverName"));
            tableGroup2.Name = "ServiceDriverName1";
            tableGroup2.ReportItem = this.textBox3;
            tableGroup2.Sortings.Add(new Telerik.Reporting.Sorting("= Fields.ServiceDriverName", Telerik.Reporting.SortDirection.Asc));
            this.crosstab2.RowGroups.Add(tableGroup2);
            this.crosstab2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.1399593353271484D), Telerik.Reporting.Drawing.Unit.Cm(0.86430495977401733D));
            this.crosstab2.StyleName = "Normal.TableNormal";
            // 
            // textBox10
            // 
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.73749834299087524D), Telerik.Reporting.Drawing.Unit.Inch(0.13194437325000763D));
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.StyleName = "Normal.TableBody";
            this.textBox10.Value = "=Fields.Activity";
            // 
            // textBox9
            // 
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0638855695724487D), Telerik.Reporting.Drawing.Unit.Inch(0.20833322405815125D));
            this.textBox9.Style.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.StyleName = "Normal.TableHeader";
            this.textBox9.Value = "Service Driver Name";
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.22222216427326202D), Telerik.Reporting.Drawing.Unit.Inch(0.20833322405815125D));
            this.textBox4.Style.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(6D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.StyleName = "Normal.TableHeader";
            this.textBox4.Value = "#";
            // 
            // objDriversAttendance
            // 
            this.objDriversAttendance.DataSource = typeof(FGCIJOROSystem.Domain.RescueOrder.clsRescueOrder);
            this.objDriversAttendance.Name = "objDriversAttendance";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(1.0828233957290649D));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.19999998807907105D));
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox29.Value = "= Parameters.PreparedByPos.Value";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.87449008226394653D));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.19999998807907105D));
            this.textBox28.Style.Font.Underline = true;
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox28.Value = "= Parameters.PreparedBy.Value";
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.47314071655273438D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.19999998807907105D));
            this.textBox27.Value = "Prepared By:";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.47605079412460327D);
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
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox35.Value = "Paper Size: 8.5x13";
            // 
            // rptDriversAttendance
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "rptDriversAttendance";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(13D), Telerik.Reporting.Drawing.Unit.Inch(8.5D));
            reportParameter1.Name = "Filter";
            reportParameter2.Name = "PreparedBy";
            reportParameter3.Name = "PreparedByPos";
            reportParameter4.Name = "DateFrom";
            reportParameter5.Name = "DateTo";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
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
            descendantSelector3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableGroup")});
            styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector3});
            styleRule5.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule5.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule5.Style.Font.Name = "Tahoma";
            styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4,
            styleRule5});
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Inch;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(12.499960899353027D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.ObjectDataSource objDriversAttendance;
        private Telerik.Reporting.Crosstab crosstab2;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox tbDatePrinted;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox txtDate;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox3;
    }
}