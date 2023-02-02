namespace FGCIJOROSystem.Reports.rAuditTrail
{
    partial class rptAuditTrailRecordCountSum
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptAuditTrailRecordCountSum));
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup6 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox112 = new Telerik.Reporting.TextBox();
            this.textBox94 = new Telerik.Reporting.TextBox();
            this.textBox104 = new Telerik.Reporting.TextBox();
            this.txtDate = new Telerik.Reporting.TextBox();
            this.pictureBox2 = new Telerik.Reporting.PictureBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.table1 = new Telerik.Reporting.Table();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.objAuditTrailReport = new Telerik.Reporting.ObjectDataSource();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox35 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(3.1971745491027832D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox112,
            this.textBox94,
            this.textBox104,
            this.txtDate,
            this.pictureBox2,
            this.textBox2});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox112
            // 
            this.textBox112.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5520834922790527D), Telerik.Reporting.Drawing.Unit.Inch(0.0416666679084301D));
            this.textBox112.Name = "textBox112";
            this.textBox112.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4399617910385132D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.textBox112.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox112.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox112.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox112.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox112.Value = "= PageNumber + \'/\' + PageCount";
            // 
            // textBox94
            // 
            this.textBox94.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0069444179534912D), Telerik.Reporting.Drawing.Unit.Inch(0.3645833432674408D));
            this.textBox94.Name = "textBox94";
            this.textBox94.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5440261363983154D), Telerik.Reporting.Drawing.Unit.Inch(0.47244107723236084D));
            this.textBox94.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox94.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox94.Style.Font.Bold = true;
            this.textBox94.Style.Font.Name = "Calibri";
            this.textBox94.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox94.Style.Font.Underline = false;
            this.textBox94.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox94.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox94.Value = "AUDIT TRAIL REPORT";
            // 
            // textBox104
            // 
            this.textBox104.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5625D), Telerik.Reporting.Drawing.Unit.Inch(0.3645833432674408D));
            this.textBox104.Name = "textBox104";
            this.textBox104.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4289273023605347D), Telerik.Reporting.Drawing.Unit.Inch(0.47244113683700562D));
            this.textBox104.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox104.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox104.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox104.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox104.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox104.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox104.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox104.Style.Font.Bold = true;
            this.textBox104.Style.Font.Italic = false;
            this.textBox104.Style.Font.Name = "Agency FB";
            this.textBox104.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox104.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox104.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox104.Value = "=Now()";
            // 
            // txtDate
            // 
            this.txtDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5625D), Telerik.Reporting.Drawing.Unit.Inch(0.3645833432674408D));
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
            this.txtDate.Value = "Date Printed";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.35937502980232239D));
            this.pictureBox2.MimeType = "image/jpeg";
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0000004768371582D), Telerik.Reporting.Drawing.Unit.Inch(0.47244131565093994D));
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
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.98437511920928955D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.9973306655883789D), Telerik.Reporting.Drawing.Unit.Inch(0.22748015820980072D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.textBox2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Style.Visible = true;
            this.textBox2.Value = "=\"Filter: \" + Parameters.FilterBy.Value";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(2.2693741321563721D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.table1,
            this.textBox5,
            this.textBox4,
            this.textBox3});
            this.detail.Name = "detail";
            // 
            // table1
            // 
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(2.390225887298584D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.8684498071670532D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.8684498071670532D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.8684498071670532D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.19999934732913971D)));
            this.table1.Body.SetCellContent(0, 0, this.textBox1, 1, 4);
            this.table1.ColumnGroups.Add(tableGroup1);
            this.table1.ColumnGroups.Add(tableGroup2);
            this.table1.ColumnGroups.Add(tableGroup3);
            this.table1.ColumnGroups.Add(tableGroup4);
            this.table1.DataSource = this.objAuditTrailReport;
            this.table1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1});
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2003713578678799E-08D), Telerik.Reporting.Drawing.Unit.Inch(0.010416666977107525D));
            this.table1.Name = "table1";
            tableGroup6.Name = "group";
            tableGroup5.ChildGroups.Add(tableGroup6);
            tableGroup5.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.EmpName"));
            tableGroup5.Name = "rowGroup";
            tableGroup5.Sortings.Add(new Telerik.Reporting.Sorting("= Fields.EmpName", Telerik.Reporting.SortDirection.Asc));
            this.table1.RowGroups.Add(tableGroup5);
            this.table1.RowHeadersPrintOnEveryPage = true;
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(20.30876350402832D), Telerik.Reporting.Drawing.Unit.Cm(0.50799834728240967D));
            this.table1.StyleName = "Normal.TableNormal";
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.9955759048461914D), Telerik.Reporting.Drawing.Unit.Inch(0.19999934732913971D));
            this.textBox1.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(213)))));
            this.textBox1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.StyleName = "";
            this.textBox1.Value = "= Fields.EmpName + \" - \" + Fields.BranchName + \" ( Total Record : \" + Count(Field" +
    "s.MLEmployeeId)  + \" )\"";
            // 
            // objAuditTrailReport
            // 
            this.objAuditTrailReport.DataSource = typeof(FGCIJOROSystem.Domain.Configurations.Users.clsUsersLog);
            this.objAuditTrailReport.Name = "objAuditTrailReport";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.79166668653488159D), Telerik.Reporting.Drawing.Unit.Inch(0.72892028093338013D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3894357681274414D), Telerik.Reporting.Drawing.Unit.Inch(0.16453413665294647D));
            this.textBox5.Style.Font.Underline = false;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "= Parameters.PreparedByPos.Value";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.37668609619140625D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0833333730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.1666666716337204D));
            this.textBox4.Value = "Prepared By:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.79166668653488159D), Telerik.Reporting.Drawing.Unit.Inch(0.54335278272628784D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3894357681274414D), Telerik.Reporting.Drawing.Unit.Inch(0.16453413665294647D));
            this.textBox3.Style.Font.Underline = true;
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "= Parameters.PreparedBy.Value";
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
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox35.Value = "Paper Size: 8.5x11";
            // 
            // rptAuditTrailRecordCountSum
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "rptAuditTrailRecordCountSum";
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
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Inch;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.9973316192626953D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox112;
        private Telerik.Reporting.TextBox textBox94;
        private Telerik.Reporting.TextBox textBox104;
        private Telerik.Reporting.TextBox txtDate;
        private Telerik.Reporting.PictureBox pictureBox2;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.Table table1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.ObjectDataSource objAuditTrailReport;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox35;
    }
}