namespace FGCIJOROSystem.Presentation.vMonitoring
{
    partial class ucMonitoring
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn1 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn2 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn1 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn3 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn2 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn2 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn3 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn3 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn4 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.Data.GroupDescriptor groupDescriptor1 = new Telerik.WinControls.Data.GroupDescriptor();
            Telerik.WinControls.Data.SortDescriptor sortDescriptor1 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.Data.GroupDescriptor groupDescriptor2 = new Telerik.WinControls.Data.GroupDescriptor();
            Telerik.WinControls.Data.SortDescriptor sortDescriptor2 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.Data.SortDescriptor sortDescriptor3 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.enumBinder1 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.enumBinder2 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.clsMonitoringBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pbLoading = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsMonitoringBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // enumBinder1
            // 
            this.enumBinder1.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.TypeOrder);
            gridViewComboBoxColumn1.DataSource = this.enumBinder1;
            gridViewComboBoxColumn1.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.TypeOrder);
            gridViewComboBoxColumn1.DisplayMember = "Description";
            gridViewComboBoxColumn1.FieldName = "ReferenceType";
            gridViewComboBoxColumn1.HeaderText = "ReferenceType";
            gridViewComboBoxColumn1.IsAutoGenerated = true;
            gridViewComboBoxColumn1.Name = "ReferenceType";
            gridViewComboBoxColumn1.ValueMember = "Value";
            this.enumBinder1.Target = gridViewComboBoxColumn1;
            // 
            // enumBinder2
            // 
            this.enumBinder2.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.TypeOrder);
            gridViewComboBoxColumn2.DataSource = this.enumBinder2;
            gridViewComboBoxColumn2.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.TypeOrder);
            gridViewComboBoxColumn2.DisplayMember = "Description";
            gridViewComboBoxColumn2.FieldName = "ReferenceType";
            gridViewComboBoxColumn2.HeaderText = "ReferenceType";
            gridViewComboBoxColumn2.IsAutoGenerated = true;
            gridViewComboBoxColumn2.Name = "ReferenceType";
            gridViewComboBoxColumn2.ValueMember = "Value";
            this.enumBinder2.Target = gridViewComboBoxColumn2;
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.pictureBox1);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(866, 32);
            this.radPanel1.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FGCIJOROSystem.Presentation.Properties.Resources.Monitoring40;
            this.pictureBox1.Location = new System.Drawing.Point(4, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(30, 5);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(220, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "MECHANIC\'S JOB MONITORING";
            // 
            // radGridView1
            // 
            this.radGridView1.BackColor = System.Drawing.SystemColors.Control;
            this.radGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radGridView1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radGridView1.Location = new System.Drawing.Point(0, 32);
            this.radGridView1.Margin = new System.Windows.Forms.Padding(2);
            // 
            // 
            // 
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AllowSearchRow = true;
            this.radGridView1.MasterTemplate.AutoGenerateColumns = false;
            this.radGridView1.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "EmployeeName";
            gridViewTextBoxColumn1.HeaderText = "Employee Name";
            gridViewTextBoxColumn1.IsAutoGenerated = true;
            gridViewTextBoxColumn1.Name = "EmployeeName";
            gridViewTextBoxColumn1.Width = 167;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "Section";
            gridViewTextBoxColumn2.HeaderText = "Section";
            gridViewTextBoxColumn2.IsAutoGenerated = true;
            gridViewTextBoxColumn2.Name = "Section";
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "Position";
            gridViewTextBoxColumn3.HeaderText = "Position";
            gridViewTextBoxColumn3.IsAutoGenerated = true;
            gridViewTextBoxColumn3.Name = "Position";
            gridViewDecimalColumn1.DataType = typeof(long);
            gridViewDecimalColumn1.EnableExpressionEditor = false;
            gridViewDecimalColumn1.FieldName = "ReferenceNo";
            gridViewDecimalColumn1.HeaderText = "ReferenceNo";
            gridViewDecimalColumn1.IsAutoGenerated = true;
            gridViewDecimalColumn1.IsVisible = false;
            gridViewDecimalColumn1.Name = "ReferenceNo";
            gridViewDecimalColumn1.Width = 121;
            gridViewDateTimeColumn1.CustomFormat = "MMMM dd,yyyy";
            gridViewDateTimeColumn1.DataType = typeof(System.Nullable<System.DateTime>);
            gridViewDateTimeColumn1.EnableExpressionEditor = false;
            gridViewDateTimeColumn1.FieldName = "DateOfUpdate";
            gridViewDateTimeColumn1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            gridViewDateTimeColumn1.FormatString = "{0:MMMM dd,yyyy}";
            gridViewDateTimeColumn1.HeaderText = "Date of Update";
            gridViewDateTimeColumn1.Name = "DateOfUpdate";
            gridViewDateTimeColumn1.Width = 77;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "ReferenceNoStr";
            gridViewTextBoxColumn4.HeaderText = "Reference No";
            gridViewTextBoxColumn4.IsAutoGenerated = true;
            gridViewTextBoxColumn4.Name = "ReferenceNoStr";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 114;
            gridViewComboBoxColumn3.DataSource = this.enumBinder2;
            gridViewComboBoxColumn3.DataType = typeof(object);
            gridViewComboBoxColumn3.DisplayMember = "Description";
            gridViewComboBoxColumn3.EnableExpressionEditor = false;
            gridViewComboBoxColumn3.FieldName = "ReferenceType";
            gridViewComboBoxColumn3.HeaderText = "ReferenceType";
            gridViewComboBoxColumn3.IsAutoGenerated = true;
            gridViewComboBoxColumn3.IsVisible = false;
            gridViewComboBoxColumn3.Name = "ReferenceType";
            gridViewComboBoxColumn3.ValueMember = "Value";
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "EquipmentName";
            gridViewTextBoxColumn5.HeaderText = "Equipment Name";
            gridViewTextBoxColumn5.IsAutoGenerated = true;
            gridViewTextBoxColumn5.Name = "EquipmentName";
            gridViewTextBoxColumn5.Width = 114;
            gridViewDecimalColumn2.DataType = typeof(long);
            gridViewDecimalColumn2.EnableExpressionEditor = false;
            gridViewDecimalColumn2.FieldName = "JODetailId";
            gridViewDecimalColumn2.HeaderText = "JODetailId";
            gridViewDecimalColumn2.IsAutoGenerated = true;
            gridViewDecimalColumn2.IsVisible = false;
            gridViewDecimalColumn2.Name = "JODetailId";
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "WorkDescription";
            gridViewTextBoxColumn6.HeaderText = "Work Description";
            gridViewTextBoxColumn6.IsAutoGenerated = true;
            gridViewTextBoxColumn6.Name = "WorkDescription";
            gridViewTextBoxColumn6.Width = 98;
            gridViewDateTimeColumn2.DataType = typeof(System.Nullable<System.DateTime>);
            gridViewDateTimeColumn2.EnableExpressionEditor = false;
            gridViewDateTimeColumn2.FieldName = "EffectiveDate";
            gridViewDateTimeColumn2.HeaderText = "Effective Date";
            gridViewDateTimeColumn2.IsAutoGenerated = true;
            gridViewDateTimeColumn2.Name = "EffectiveDate";
            gridViewDateTimeColumn2.Width = 87;
            gridViewDateTimeColumn3.DataType = typeof(System.Nullable<System.DateTime>);
            gridViewDateTimeColumn3.EnableExpressionEditor = false;
            gridViewDateTimeColumn3.FieldName = "TargetDate";
            gridViewDateTimeColumn3.HeaderText = "Target Date";
            gridViewDateTimeColumn3.IsAutoGenerated = true;
            gridViewDateTimeColumn3.Name = "TargetDate";
            gridViewDateTimeColumn3.Width = 76;
            gridViewDecimalColumn3.DataType = typeof(long);
            gridViewDecimalColumn3.EnableExpressionEditor = false;
            gridViewDecimalColumn3.FieldName = "WorkTimeSpan";
            gridViewDecimalColumn3.HeaderText = "Work Time Span";
            gridViewDecimalColumn3.IsAutoGenerated = true;
            gridViewDecimalColumn3.Name = "WorkTimeSpan";
            gridViewDecimalColumn3.Width = 83;
            gridViewTextBoxColumn7.EnableExpressionEditor = false;
            gridViewTextBoxColumn7.FieldName = "Unit";
            gridViewTextBoxColumn7.HeaderText = "Unit";
            gridViewTextBoxColumn7.IsAutoGenerated = true;
            gridViewTextBoxColumn7.Name = "Unit";
            gridViewTextBoxColumn7.Width = 41;
            gridViewDecimalColumn4.EnableExpressionEditor = false;
            gridViewDecimalColumn4.FieldName = "WorkPercentage";
            gridViewDecimalColumn4.HeaderText = "Work Percentage";
            gridViewDecimalColumn4.IsAutoGenerated = true;
            gridViewDecimalColumn4.Name = "WorkPercentage";
            gridViewDecimalColumn4.Width = 76;
            gridViewTextBoxColumn8.EnableExpressionEditor = false;
            gridViewTextBoxColumn8.FieldName = "Status";
            gridViewTextBoxColumn8.HeaderText = "Status";
            gridViewTextBoxColumn8.IsAutoGenerated = true;
            gridViewTextBoxColumn8.Name = "Status";
            gridViewTextBoxColumn8.Width = 46;
            this.radGridView1.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewDecimalColumn1,
            gridViewDateTimeColumn1,
            gridViewTextBoxColumn4,
            gridViewComboBoxColumn3,
            gridViewTextBoxColumn5,
            gridViewDecimalColumn2,
            gridViewTextBoxColumn6,
            gridViewDateTimeColumn2,
            gridViewDateTimeColumn3,
            gridViewDecimalColumn3,
            gridViewTextBoxColumn7,
            gridViewDecimalColumn4,
            gridViewTextBoxColumn8});
            this.radGridView1.MasterTemplate.DataSource = this.clsMonitoringBindingSource;
            sortDescriptor1.PropertyName = "Section";
            groupDescriptor1.GroupNames.AddRange(new Telerik.WinControls.Data.SortDescriptor[] {
            sortDescriptor1});
            sortDescriptor2.PropertyName = "EmployeeName";
            sortDescriptor3.PropertyName = "Position";
            groupDescriptor2.GroupNames.AddRange(new Telerik.WinControls.Data.SortDescriptor[] {
            sortDescriptor2,
            sortDescriptor3});
            this.radGridView1.MasterTemplate.GroupDescriptors.AddRange(new Telerik.WinControls.Data.GroupDescriptor[] {
            groupDescriptor1,
            groupDescriptor2});
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.ReadOnly = true;
            this.radGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radGridView1.ShowGroupPanel = false;
            this.radGridView1.Size = new System.Drawing.Size(866, 586);
            this.radGridView1.TabIndex = 9;
            this.radGridView1.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.radGridView1_CellFormatting);
            // 
            // clsMonitoringBindingSource
            // 
            this.clsMonitoringBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Monitoring.clsMonitoring);
            // 
            // pbLoading
            // 
            this.pbLoading.AutoSize = false;
            this.pbLoading.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.pbLoading.ForeColor = System.Drawing.Color.Red;
            this.pbLoading.Location = new System.Drawing.Point(226, 287);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(414, 44);
            this.pbLoading.TabIndex = 12;
            this.pbLoading.Text = "Loading, please wait...";
            this.pbLoading.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucMonitoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbLoading);
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.radPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucMonitoring";
            this.Size = new System.Drawing.Size(866, 618);
            this.Load += new System.EventHandler(this.ucMonitoring_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsMonitoringBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder1;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder2;
        private System.Windows.Forms.BindingSource clsMonitoringBindingSource;
        private Telerik.WinControls.UI.RadLabel pbLoading;
    }
}
