namespace FGCIJOROSystem.Presentation.vConfiguration.vTariff
{
    partial class frmSelectSection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn2 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn3 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn2 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn4 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn5 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectSection));
            this.clsSectionJOROBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsTariffBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.rgvSection = new Telerik.WinControls.UI.RadGridView();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnGenerateReportPerSection = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.clsSectionJOROBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsTariffBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgvSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvSection.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerateReportPerSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // clsSectionJOROBindingSource
            // 
            this.clsSectionJOROBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Configurations.SectionJORO.clsSectionJORO);
            // 
            // clsTariffBindingSource
            // 
            this.clsTariffBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Configurations.Tariff.clsTariff);
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.radGroupBox1.Controls.Add(this.rgvSection);
            this.radGroupBox1.Controls.Add(this.btnCancel);
            this.radGroupBox1.Controls.Add(this.btnGenerateReportPerSection);
            this.radGroupBox1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.radGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox1.HeaderText = "SELECT SECTION";
            this.radGroupBox1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radGroupBox1.Location = new System.Drawing.Point(4, 5);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Size = new System.Drawing.Size(406, 409);
            this.radGroupBox1.TabIndex = 41;
            this.radGroupBox1.Text = "SELECT SECTION";
            // 
            // rgvSection
            // 
            this.rgvSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.rgvSection.Cursor = System.Windows.Forms.Cursors.Default;
            this.rgvSection.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rgvSection.ForeColor = System.Drawing.Color.Black;
            this.rgvSection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rgvSection.Location = new System.Drawing.Point(3, 21);
            // 
            // 
            // 
            this.rgvSection.MasterTemplate.AllowAddNewRow = false;
            this.rgvSection.MasterTemplate.AllowRowReorder = true;
            this.rgvSection.MasterTemplate.AllowSearchRow = true;
            this.rgvSection.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewDecimalColumn1.DataType = typeof(long);
            gridViewDecimalColumn1.EnableExpressionEditor = false;
            gridViewDecimalColumn1.FieldName = "Id";
            gridViewDecimalColumn1.HeaderText = "Id";
            gridViewDecimalColumn1.IsAutoGenerated = true;
            gridViewDecimalColumn1.IsVisible = false;
            gridViewDecimalColumn1.Name = "Id";
            gridViewCheckBoxColumn1.DataType = typeof(string);
            gridViewCheckBoxColumn1.EnableExpressionEditor = false;
            gridViewCheckBoxColumn1.EnableHeaderCheckBox = true;
            gridViewCheckBoxColumn1.FieldName = "CheckSectionName1";
            gridViewCheckBoxColumn1.IsVisible = false;
            gridViewCheckBoxColumn1.MinWidth = 20;
            gridViewCheckBoxColumn1.Name = "CheckSectionName1";
            gridViewCheckBoxColumn1.Width = 29;
            gridViewCheckBoxColumn2.EnableExpressionEditor = false;
            gridViewCheckBoxColumn2.EnableHeaderCheckBox = true;
            gridViewCheckBoxColumn2.FieldName = "CheckSectionName";
            gridViewCheckBoxColumn2.HeaderText = "All";
            gridViewCheckBoxColumn2.MaxWidth = 40;
            gridViewCheckBoxColumn2.MinWidth = 40;
            gridViewCheckBoxColumn2.Name = "CheckSectionName";
            gridViewCheckBoxColumn2.Width = 40;
            gridViewTextBoxColumn1.AllowReorder = false;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "SectionName";
            gridViewTextBoxColumn1.HeaderText = "Sections";
            gridViewTextBoxColumn1.IsAutoGenerated = true;
            gridViewTextBoxColumn1.Name = "SectionName";
            gridViewTextBoxColumn1.Width = 341;
            gridViewTextBoxColumn1.WrapText = true;
            gridViewCheckBoxColumn3.EnableExpressionEditor = false;
            gridViewCheckBoxColumn3.FieldName = "CheckEquipment";
            gridViewCheckBoxColumn3.HeaderText = "CheckEquipment";
            gridViewCheckBoxColumn3.IsAutoGenerated = true;
            gridViewCheckBoxColumn3.IsVisible = false;
            gridViewCheckBoxColumn3.MinWidth = 20;
            gridViewCheckBoxColumn3.Name = "CheckEquipment";
            gridViewCheckBoxColumn3.Width = 26;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "AttendanceGroupName";
            gridViewTextBoxColumn2.HeaderText = "AttendanceGroupName";
            gridViewTextBoxColumn2.IsAutoGenerated = true;
            gridViewTextBoxColumn2.IsVisible = false;
            gridViewTextBoxColumn2.Name = "AttendanceGroupName";
            gridViewTextBoxColumn2.Width = 85;
            gridViewDecimalColumn2.DataType = typeof(long);
            gridViewDecimalColumn2.EnableExpressionEditor = false;
            gridViewDecimalColumn2.FieldName = "AttendanceGroupId";
            gridViewDecimalColumn2.HeaderText = "AttendanceGroupId";
            gridViewDecimalColumn2.IsAutoGenerated = true;
            gridViewDecimalColumn2.IsVisible = false;
            gridViewDecimalColumn2.Name = "AttendanceGroupId";
            gridViewDecimalColumn2.Width = 106;
            gridViewCheckBoxColumn4.EnableExpressionEditor = false;
            gridViewCheckBoxColumn4.FieldName = "IsChecklistGroup";
            gridViewCheckBoxColumn4.HeaderText = "IsChecklistGroup";
            gridViewCheckBoxColumn4.IsAutoGenerated = true;
            gridViewCheckBoxColumn4.IsVisible = false;
            gridViewCheckBoxColumn4.MinWidth = 20;
            gridViewCheckBoxColumn4.Name = "IsChecklistGroup";
            gridViewCheckBoxColumn4.Width = 141;
            gridViewCheckBoxColumn5.EnableExpressionEditor = false;
            gridViewCheckBoxColumn5.FieldName = "Active";
            gridViewCheckBoxColumn5.HeaderText = "Active";
            gridViewCheckBoxColumn5.IsAutoGenerated = true;
            gridViewCheckBoxColumn5.IsVisible = false;
            gridViewCheckBoxColumn5.MinWidth = 20;
            gridViewCheckBoxColumn5.Name = "Active";
            gridViewCheckBoxColumn5.Width = 420;
            gridViewTextBoxColumn3.DataType = typeof(System.Collections.Generic.List<FGCIJOROSystem.Domain.Configurations.Personnels.clsPersonnels>);
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "ListOfPersonnel";
            gridViewTextBoxColumn3.HeaderText = "ListOfPersonnel";
            gridViewTextBoxColumn3.IsAutoGenerated = true;
            gridViewTextBoxColumn3.IsVisible = false;
            gridViewTextBoxColumn3.Name = "ListOfPersonnel";
            gridViewTextBoxColumn3.Width = 210;
            this.rgvSection.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewDecimalColumn1,
            gridViewCheckBoxColumn1,
            gridViewCheckBoxColumn2,
            gridViewTextBoxColumn1,
            gridViewCheckBoxColumn3,
            gridViewTextBoxColumn2,
            gridViewDecimalColumn2,
            gridViewCheckBoxColumn4,
            gridViewCheckBoxColumn5,
            gridViewTextBoxColumn3});
            this.rgvSection.MasterTemplate.DataSource = this.clsSectionJOROBindingSource;
            this.rgvSection.MasterTemplate.EnableGrouping = false;
            this.rgvSection.MasterTemplate.EnableSorting = false;
            this.rgvSection.MasterTemplate.ShowTotals = true;
            this.rgvSection.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.rgvSection.Name = "rgvSection";
            this.rgvSection.ReadOnly = true;
            this.rgvSection.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rgvSection.ShowGroupPanel = false;
            this.rgvSection.Size = new System.Drawing.Size(400, 341);
            this.rgvSection.TabIndex = 44;
            this.rgvSection.Text = "radGridView1";
            this.rgvSection.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.rgvSection_CellClick);
            this.rgvSection.Click += new System.EventHandler(this.rgvSection_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(284, 364);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(119, 41);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // btnGenerateReportPerSection
            // 
            this.btnGenerateReportPerSection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnGenerateReportPerSection.ForeColor = System.Drawing.Color.Black;
            this.btnGenerateReportPerSection.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerateReportPerSection.Location = new System.Drawing.Point(3, 364);
            this.btnGenerateReportPerSection.Name = "btnGenerateReportPerSection";
            this.btnGenerateReportPerSection.Size = new System.Drawing.Size(279, 41);
            this.btnGenerateReportPerSection.TabIndex = 42;
            this.btnGenerateReportPerSection.Text = "SHOW SELECTED RECORD";
            this.btnGenerateReportPerSection.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerateReportPerSection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenerateReportPerSection.Click += new System.EventHandler(this.btnGenerateReportPerSection_Click_1);
            // 
            // frmSelectSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(415, 418);
            this.Controls.Add(this.radGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectSection";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmSelectSection";
            this.Load += new System.EventHandler(this.frmSelectSection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clsSectionJOROBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsTariffBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgvSection.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerateReportPerSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource clsSectionJOROBindingSource;
        private System.Windows.Forms.BindingSource clsTariffBindingSource;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadButton btnGenerateReportPerSection;
        private Telerik.WinControls.UI.RadGridView rgvSection;
    }
}
