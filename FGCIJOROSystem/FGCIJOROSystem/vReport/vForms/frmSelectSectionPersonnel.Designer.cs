namespace FGCIJOROSystem.Presentation.vReport.vForms
{
    partial class frmSelectSectionPersonnel
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
            Telerik.WinControls.Data.SortDescriptor sortDescriptor1 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectSectionPersonnel));
            this.rgvSection = new Telerik.WinControls.UI.RadGridView();
            this.clsSectionJOROBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsPersonnelsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnGenerateReportPerSection = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            ((System.ComponentModel.ISupportInitialize)(this.rgvSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvSection.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsSectionJOROBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPersonnelsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerateReportPerSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // rgvSection
            // 
            this.rgvSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.rgvSection.Cursor = System.Windows.Forms.Cursors.Default;
            this.rgvSection.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rgvSection.ForeColor = System.Drawing.Color.Black;
            this.rgvSection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rgvSection.Location = new System.Drawing.Point(0, 33);
            // 
            // 
            // 
            this.rgvSection.MasterTemplate.AllowAddNewRow = false;
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
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "SectionName";
            gridViewTextBoxColumn1.HeaderText = "Section Name";
            gridViewTextBoxColumn1.IsAutoGenerated = true;
            gridViewTextBoxColumn1.Name = "SectionName";
            gridViewTextBoxColumn1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewTextBoxColumn1.Width = 383;
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
            this.rgvSection.MasterTemplate.ShowTotals = true;
            sortDescriptor1.PropertyName = "SectionName";
            this.rgvSection.MasterTemplate.SortDescriptors.AddRange(new Telerik.WinControls.Data.SortDescriptor[] {
            sortDescriptor1});
            this.rgvSection.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.rgvSection.Name = "rgvSection";
            this.rgvSection.ReadOnly = true;
            this.rgvSection.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rgvSection.ShowGroupPanel = false;
            this.rgvSection.Size = new System.Drawing.Size(442, 394);
            this.rgvSection.TabIndex = 34;
            this.rgvSection.Text = "radGridView1";
            this.rgvSection.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.rgvSection_CellClick);
            this.rgvSection.Click += new System.EventHandler(this.rgvSection_Click);
            // 
            // clsSectionJOROBindingSource
            // 
            this.clsSectionJOROBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Configurations.SectionJORO.clsSectionJORO);
            // 
            // clsPersonnelsBindingSource
            // 
            this.clsPersonnelsBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Configurations.Personnels.clsPersonnels);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(304, 429);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 41);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnGenerateReportPerSection
            // 
            this.btnGenerateReportPerSection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnGenerateReportPerSection.ForeColor = System.Drawing.Color.Black;
            this.btnGenerateReportPerSection.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerateReportPerSection.Image")));
            this.btnGenerateReportPerSection.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerateReportPerSection.Location = new System.Drawing.Point(0, 429);
            this.btnGenerateReportPerSection.Name = "btnGenerateReportPerSection";
            this.btnGenerateReportPerSection.Size = new System.Drawing.Size(301, 41);
            this.btnGenerateReportPerSection.TabIndex = 44;
            this.btnGenerateReportPerSection.Text = "GENERATE REPORT";
            this.btnGenerateReportPerSection.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerateReportPerSection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenerateReportPerSection.Click += new System.EventHandler(this.btnGenerateReportPerSection_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(26, 5);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(185, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "SUMMARY OF PERSONNEL";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FGCIJOROSystem.Presentation.Properties.Resources.SummaryOfPersonnel;
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.pictureBox1);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(443, 32);
            this.radPanel1.TabIndex = 46;
            // 
            // frmSelectSectionPersonnel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 470);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerateReportPerSection);
            this.Controls.Add(this.rgvSection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectSectionPersonnel";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "";
            this.Load += new System.EventHandler(this.frmSelectSectionPersonnel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rgvSection.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsSectionJOROBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPersonnelsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerateReportPerSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView rgvSection;
        private System.Windows.Forms.BindingSource clsSectionJOROBindingSource;
        private System.Windows.Forms.BindingSource clsPersonnelsBindingSource;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadButton btnGenerateReportPerSection;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.RadPanel radPanel1;

    }
}
