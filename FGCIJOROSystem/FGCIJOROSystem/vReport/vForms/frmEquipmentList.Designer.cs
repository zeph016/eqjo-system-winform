namespace FGCIJOROSystem.Presentation.vReport.vForms
{
    partial class frmEquipmentList
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
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn1 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEquipmentList));
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn2 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn2 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.Data.SortDescriptor sortDescriptor1 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.enumBinder1 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnGenerateReportPerSection = new Telerik.WinControls.UI.RadButton();
            this.clsEquipmentHistoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.chkEquipment = new Telerik.WinControls.UI.RadCheckBox();
            this.rgvSection = new Telerik.WinControls.UI.RadGridView();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.dtEndDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.dtStartDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.chkDate = new Telerik.WinControls.UI.RadCheckBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerateReportPerSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsEquipmentHistoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkEquipment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvSection.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // enumBinder1
            // 
            this.enumBinder1.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.EquipmentType);
            gridViewComboBoxColumn1.DataSource = this.enumBinder1;
            gridViewComboBoxColumn1.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.EquipmentType);
            gridViewComboBoxColumn1.DisplayMember = "Description";
            gridViewComboBoxColumn1.FieldName = "ItemType";
            gridViewComboBoxColumn1.HeaderText = "ItemType";
            gridViewComboBoxColumn1.IsAutoGenerated = true;
            gridViewComboBoxColumn1.Name = "ItemType";
            gridViewComboBoxColumn1.ValueMember = "Value";
            gridViewComboBoxColumn1.Width = 53;
            this.enumBinder1.Target = gridViewComboBoxColumn1;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(294, 425);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 41);
            this.btnCancel.TabIndex = 48;
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
            this.btnGenerateReportPerSection.Location = new System.Drawing.Point(0, 425);
            this.btnGenerateReportPerSection.Name = "btnGenerateReportPerSection";
            this.btnGenerateReportPerSection.Size = new System.Drawing.Size(292, 41);
            this.btnGenerateReportPerSection.TabIndex = 47;
            this.btnGenerateReportPerSection.Text = "GENERATE REPORT";
            this.btnGenerateReportPerSection.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerateReportPerSection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenerateReportPerSection.Click += new System.EventHandler(this.btnGenerateReportPerSection_Click);
            // 
            // clsEquipmentHistoryBindingSource
            // 
            this.clsEquipmentHistoryBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Reports.clsEquipmentHistoryReport);
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.chkEquipment);
            this.radGroupBox1.Controls.Add(this.rgvSection);
            this.radGroupBox1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold);
            this.radGroupBox1.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.radGroupBox1.HeaderText = "LIST OF EQUIPMENT";
            this.radGroupBox1.Location = new System.Drawing.Point(-1, 113);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Size = new System.Drawing.Size(429, 311);
            this.radGroupBox1.TabIndex = 49;
            this.radGroupBox1.Text = "LIST OF EQUIPMENT";
            // 
            // chkEquipment
            // 
            this.chkEquipment.Location = new System.Drawing.Point(6, 11);
            this.chkEquipment.Margin = new System.Windows.Forms.Padding(2);
            this.chkEquipment.Name = "chkEquipment";
            this.chkEquipment.Size = new System.Drawing.Size(107, 18);
            this.chkEquipment.TabIndex = 51;
            this.chkEquipment.Text = "Select Equipment";
            this.chkEquipment.CheckStateChanged += new System.EventHandler(this.chkEquipment_CheckStateChanged);
            // 
            // rgvSection
            // 
            this.rgvSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.rgvSection.Cursor = System.Windows.Forms.Cursors.Default;
            this.rgvSection.Enabled = false;
            this.rgvSection.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rgvSection.ForeColor = System.Drawing.Color.Black;
            this.rgvSection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rgvSection.Location = new System.Drawing.Point(2, 33);
            // 
            // 
            // 
            this.rgvSection.MasterTemplate.AllowAddNewRow = false;
            this.rgvSection.MasterTemplate.AllowSearchRow = true;
            this.rgvSection.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
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
            gridViewCheckBoxColumn2.FieldName = "CheckEquipmentName";
            gridViewCheckBoxColumn2.HeaderText = "All";
            gridViewCheckBoxColumn2.MaxWidth = 40;
            gridViewCheckBoxColumn2.MinWidth = 40;
            gridViewCheckBoxColumn2.Name = "CheckEquipmentName";
            gridViewCheckBoxColumn2.Width = 40;
            gridViewComboBoxColumn2.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.EquipmentType);
            gridViewComboBoxColumn2.DisplayMember = "Description";
            gridViewComboBoxColumn2.EnableExpressionEditor = false;
            gridViewComboBoxColumn2.FieldName = "ItemType";
            gridViewComboBoxColumn2.HeaderText = "ItemType";
            gridViewComboBoxColumn2.IsAutoGenerated = true;
            gridViewComboBoxColumn2.IsVisible = false;
            gridViewComboBoxColumn2.Name = "ItemType";
            gridViewComboBoxColumn2.ValueMember = "Value";
            gridViewComboBoxColumn2.Width = 61;
            gridViewDecimalColumn1.DataType = typeof(long);
            gridViewDecimalColumn1.EnableExpressionEditor = false;
            gridViewDecimalColumn1.FieldName = "EquipmentId";
            gridViewDecimalColumn1.HeaderText = "EquipmentId";
            gridViewDecimalColumn1.IsAutoGenerated = true;
            gridViewDecimalColumn1.IsVisible = false;
            gridViewDecimalColumn1.Name = "EquipmentId";
            gridViewDecimalColumn1.Width = 87;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "EquipmentCode";
            gridViewTextBoxColumn1.HeaderText = "Equipment Code";
            gridViewTextBoxColumn1.IsAutoGenerated = true;
            gridViewTextBoxColumn1.Name = "EquipmentCode";
            gridViewTextBoxColumn1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewTextBoxColumn1.Width = 108;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "EquipmentName";
            gridViewTextBoxColumn2.HeaderText = "Equipment Name";
            gridViewTextBoxColumn2.IsAutoGenerated = true;
            gridViewTextBoxColumn2.Name = "EquipmentName";
            gridViewTextBoxColumn2.Width = 152;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "EquipmentLocation";
            gridViewTextBoxColumn3.HeaderText = "EquipmentLocation";
            gridViewTextBoxColumn3.IsAutoGenerated = true;
            gridViewTextBoxColumn3.IsVisible = false;
            gridViewTextBoxColumn3.Name = "EquipmentLocation";
            gridViewTextBoxColumn3.Width = 72;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "EquipmentRemarks";
            gridViewTextBoxColumn4.HeaderText = "EquipmentRemarks";
            gridViewTextBoxColumn4.IsAutoGenerated = true;
            gridViewTextBoxColumn4.IsVisible = false;
            gridViewTextBoxColumn4.Name = "EquipmentRemarks";
            gridViewTextBoxColumn4.Width = 85;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "EquipmentClass";
            gridViewTextBoxColumn5.HeaderText = "Equipment Class";
            gridViewTextBoxColumn5.IsAutoGenerated = true;
            gridViewTextBoxColumn5.Name = "EquipmentClass";
            gridViewTextBoxColumn5.Width = 107;
            this.rgvSection.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewCheckBoxColumn1,
            gridViewCheckBoxColumn2,
            gridViewComboBoxColumn2,
            gridViewDecimalColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5});
            this.rgvSection.MasterTemplate.DataSource = this.clsEquipmentHistoryBindingSource;
            this.rgvSection.MasterTemplate.EnableGrouping = false;
            this.rgvSection.MasterTemplate.ShowTotals = true;
            sortDescriptor1.PropertyName = "EquipmentCode";
            this.rgvSection.MasterTemplate.SortDescriptors.AddRange(new Telerik.WinControls.Data.SortDescriptor[] {
            sortDescriptor1});
            this.rgvSection.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.rgvSection.Name = "rgvSection";
            this.rgvSection.ReadOnly = true;
            this.rgvSection.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rgvSection.ShowGroupPanel = false;
            this.rgvSection.Size = new System.Drawing.Size(424, 274);
            this.rgvSection.TabIndex = 50;
            this.rgvSection.Text = "radGridView1";
            this.rgvSection.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.rgvSection_CellClick);
            this.rgvSection.Click += new System.EventHandler(this.rgvSection_Click);
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox2.Controls.Add(this.dtEndDate);
            this.radGroupBox2.Controls.Add(this.radLabel3);
            this.radGroupBox2.Controls.Add(this.dtStartDate);
            this.radGroupBox2.Controls.Add(this.radLabel2);
            this.radGroupBox2.Controls.Add(this.chkDate);
            this.radGroupBox2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold);
            this.radGroupBox2.HeaderText = "Date Option";
            this.radGroupBox2.Location = new System.Drawing.Point(-1, 34);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Size = new System.Drawing.Size(429, 75);
            this.radGroupBox2.TabIndex = 50;
            this.radGroupBox2.Text = "Date Option";
            // 
            // dtEndDate
            // 
            this.dtEndDate.Enabled = false;
            this.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtEndDate.Location = new System.Drawing.Point(279, 42);
            this.dtEndDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(138, 20);
            this.dtEndDate.TabIndex = 20;
            this.dtEndDate.TabStop = false;
            this.dtEndDate.Text = "12/17/2018";
            this.dtEndDate.Value = new System.DateTime(2018, 12, 17, 15, 45, 40, 615);
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(226, 44);
            this.radLabel3.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(52, 18);
            this.radLabel3.TabIndex = 19;
            this.radLabel3.Text = "End Date";
            // 
            // dtStartDate
            // 
            this.dtStartDate.Enabled = false;
            this.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtStartDate.Location = new System.Drawing.Point(70, 43);
            this.dtStartDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(140, 20);
            this.dtStartDate.TabIndex = 18;
            this.dtStartDate.TabStop = false;
            this.dtStartDate.Text = "12/17/2018";
            this.dtStartDate.Value = new System.DateTime(2018, 12, 17, 0, 0, 0, 0);
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(11, 44);
            this.radLabel2.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(56, 18);
            this.radLabel2.TabIndex = 17;
            this.radLabel2.Text = "Start Date";
            // 
            // chkDate
            // 
            this.chkDate.Location = new System.Drawing.Point(11, 17);
            this.chkDate.Margin = new System.Windows.Forms.Padding(2);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(76, 18);
            this.chkDate.TabIndex = 16;
            this.chkDate.Text = "Select Date";
            this.chkDate.CheckStateChanged += new System.EventHandler(this.chkDate_CheckStateChanged);
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(30, 5);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(254, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "EQUIPMENT WITHDRAWAL HISTORY";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FGCIJOROSystem.Presentation.Properties.Resources.History_100;
            this.pictureBox1.Location = new System.Drawing.Point(4, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.pictureBox1);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(427, 32);
            this.radPanel1.TabIndex = 51;
            // 
            // frmEquipmentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 466);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.radGroupBox1);
            this.Controls.Add(this.radGroupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerateReportPerSection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEquipmentList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.Load += new System.EventHandler(this.frmEquipmentList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerateReportPerSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsEquipmentHistoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkEquipment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvSection.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            this.radGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadButton btnGenerateReportPerSection;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder1;
        private System.Windows.Forms.BindingSource clsEquipmentHistoryBindingSource;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGridView rgvSection;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadCheckBox chkDate;
        private Telerik.WinControls.UI.RadDateTimePicker dtEndDate;
        private Telerik.WinControls.UI.RadDateTimePicker dtStartDate;
        private Telerik.WinControls.UI.RadCheckBox chkEquipment;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel2;
    }
}
