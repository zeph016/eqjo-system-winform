namespace FGCIJOROSystem.Presentation.vChecklist
{
    partial class ucEquipmentList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucEquipmentList));
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn2 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn1 = new Telerik.WinControls.UI.GridViewCommandColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn2 = new Telerik.WinControls.UI.GridViewCommandColumn();
            Telerik.WinControls.Data.SortDescriptor sortDescriptor1 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.enumBinder1 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.clsEquipmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsEquipmentTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsChecklistItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsChecklistDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsChecklistBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsChecklistGeneratorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsEquipmentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsEquipmentTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsChecklistItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsChecklistDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsChecklistBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsChecklistGeneratorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // enumBinder1
            // 
            this.enumBinder1.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.EquipmentType);
            gridViewComboBoxColumn1.DataSource = this.enumBinder1;
            gridViewComboBoxColumn1.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.EquipmentType);
            gridViewComboBoxColumn1.DisplayMember = "Description";
            gridViewComboBoxColumn1.FieldName = "EquipmentType";
            gridViewComboBoxColumn1.HeaderText = "EquipmentType";
            gridViewComboBoxColumn1.IsAutoGenerated = true;
            gridViewComboBoxColumn1.Name = "EquipmentType";
            gridViewComboBoxColumn1.ValueMember = "Value";
            gridViewComboBoxColumn1.Width = 138;
            this.enumBinder1.Target = gridViewComboBoxColumn1;
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.pictureBox1);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(842, 32);
            this.radPanel1.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
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
            this.radLabel1.Size = new System.Drawing.Size(120, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "EQUIPMENT LIST";
            // 
            // radGridView1
            // 
            this.radGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radGridView1.BackColor = System.Drawing.SystemColors.Control;
            this.radGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radGridView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radGridView1.ForeColor = System.Drawing.Color.Black;
            this.radGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radGridView1.Location = new System.Drawing.Point(2, 33);
            this.radGridView1.Margin = new System.Windows.Forms.Padding(2);
            // 
            // 
            // 
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AllowSearchRow = true;
            this.radGridView1.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewDecimalColumn1.DataType = typeof(long);
            gridViewDecimalColumn1.EnableExpressionEditor = false;
            gridViewDecimalColumn1.FieldName = "EquipmentMasterlistId";
            gridViewDecimalColumn1.HeaderText = "EquipmentMasterlistId";
            gridViewDecimalColumn1.IsAutoGenerated = true;
            gridViewDecimalColumn1.IsVisible = false;
            gridViewDecimalColumn1.Name = "EquipmentMasterlistId";
            gridViewDecimalColumn1.Width = 111;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "EquipmentCode";
            gridViewTextBoxColumn1.HeaderText = "Equipment Code";
            gridViewTextBoxColumn1.IsAutoGenerated = true;
            gridViewTextBoxColumn1.MaxWidth = 100;
            gridViewTextBoxColumn1.MinWidth = 100;
            gridViewTextBoxColumn1.Name = "EquipmentCode";
            gridViewTextBoxColumn1.Width = 100;
            gridViewComboBoxColumn2.DataSource = this.enumBinder1;
            gridViewComboBoxColumn2.DataType = typeof(object);
            gridViewComboBoxColumn2.DisplayMember = "Description";
            gridViewComboBoxColumn2.EnableExpressionEditor = false;
            gridViewComboBoxColumn2.FieldName = "EquipmentType";
            gridViewComboBoxColumn2.HeaderText = "Equipment Type";
            gridViewComboBoxColumn2.IsAutoGenerated = true;
            gridViewComboBoxColumn2.MaxWidth = 100;
            gridViewComboBoxColumn2.MinWidth = 100;
            gridViewComboBoxColumn2.Name = "EquipmentType";
            gridViewComboBoxColumn2.ValueMember = "Value";
            gridViewComboBoxColumn2.Width = 100;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "EquipmentClass";
            gridViewTextBoxColumn2.HeaderText = "EquipmentClass";
            gridViewTextBoxColumn2.IsAutoGenerated = true;
            gridViewTextBoxColumn2.IsVisible = false;
            gridViewTextBoxColumn2.Name = "EquipmentClass";
            gridViewTextBoxColumn2.Width = 74;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "EquipmentName";
            gridViewTextBoxColumn3.HeaderText = "Equipment Name";
            gridViewTextBoxColumn3.IsAutoGenerated = true;
            gridViewTextBoxColumn3.Name = "EquipmentName";
            gridViewTextBoxColumn3.Width = 159;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "Location";
            gridViewTextBoxColumn4.HeaderText = "Location";
            gridViewTextBoxColumn4.IsAutoGenerated = true;
            gridViewTextBoxColumn4.Name = "Location";
            gridViewTextBoxColumn4.Width = 117;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "Driver";
            gridViewTextBoxColumn5.HeaderText = "Driver";
            gridViewTextBoxColumn5.IsAutoGenerated = true;
            gridViewTextBoxColumn5.Name = "Driver";
            gridViewTextBoxColumn5.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewTextBoxColumn5.Width = 301;
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "PlateNo";
            gridViewTextBoxColumn6.HeaderText = "PlateNo";
            gridViewTextBoxColumn6.IsAutoGenerated = true;
            gridViewTextBoxColumn6.IsVisible = false;
            gridViewTextBoxColumn6.MaxWidth = 100;
            gridViewTextBoxColumn6.MinWidth = 100;
            gridViewTextBoxColumn6.Name = "PlateNo";
            gridViewTextBoxColumn6.Width = 100;
            gridViewCommandColumn1.DefaultText = "GenerateChecklist";
            gridViewCommandColumn1.EnableExpressionEditor = false;
            gridViewCommandColumn1.Image = ((System.Drawing.Image)(resources.GetObject("gridViewCommandColumn1.Image")));
            gridViewCommandColumn1.MaxWidth = 24;
            gridViewCommandColumn1.MinWidth = 24;
            gridViewCommandColumn1.Name = "GenerateChecklist";
            gridViewCommandColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewCommandColumn1.Width = 24;
            gridViewCommandColumn2.DefaultText = "PrintLogs";
            gridViewCommandColumn2.EnableExpressionEditor = false;
            gridViewCommandColumn2.Image = ((System.Drawing.Image)(resources.GetObject("gridViewCommandColumn2.Image")));
            gridViewCommandColumn2.MaxWidth = 24;
            gridViewCommandColumn2.MinWidth = 24;
            gridViewCommandColumn2.Name = "PrintLogs";
            gridViewCommandColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewCommandColumn2.Width = 24;
            this.radGridView1.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewDecimalColumn1,
            gridViewTextBoxColumn1,
            gridViewComboBoxColumn2,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewCommandColumn1,
            gridViewCommandColumn2});
            this.radGridView1.MasterTemplate.DataSource = this.clsEquipmentBindingSource;
            this.radGridView1.MasterTemplate.EnableGrouping = false;
            sortDescriptor1.PropertyName = "Driver";
            this.radGridView1.MasterTemplate.SortDescriptors.AddRange(new Telerik.WinControls.Data.SortDescriptor[] {
            sortDescriptor1});
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.ReadOnly = true;
            this.radGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radGridView1.Size = new System.Drawing.Size(839, 553);
            this.radGridView1.TabIndex = 7;
            this.radGridView1.Text = "radGridView1";
            this.radGridView1.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.radGridView1_CellFormatting);
            this.radGridView1.CommandCellClick += new Telerik.WinControls.UI.CommandCellClickEventHandler(this.radGridView1_CommandCellClick);
            // 
            // clsEquipmentBindingSource
            // 
            this.clsEquipmentBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Global.clsEquipment);
            // 
            // clsEquipmentTypeBindingSource
            // 
            this.clsEquipmentTypeBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Configurations.EquipmentType.clsEquipmentType);
            // 
            // clsChecklistItemBindingSource
            // 
            this.clsChecklistItemBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Global.clsChecklistItem);
            // 
            // clsChecklistDetailsBindingSource
            // 
            this.clsChecklistDetailsBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Global.clsChecklistDetails);
            // 
            // clsChecklistBindingSource
            // 
            this.clsChecklistBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Global.clsChecklistDetails);
            // 
            // clsChecklistGeneratorBindingSource
            // 
            this.clsChecklistGeneratorBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Checklist.clsChecklistGenerator);
            // 
            // ucEquipmentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.radPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucEquipmentList";
            this.Size = new System.Drawing.Size(842, 587);
            this.Load += new System.EventHandler(this.ucEquipmentTypeLists_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsEquipmentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsEquipmentTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsChecklistItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsChecklistDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsChecklistBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsChecklistGeneratorBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder1;
        private System.Windows.Forms.BindingSource clsEquipmentBindingSource;
        private System.Windows.Forms.BindingSource clsEquipmentTypeBindingSource;
        private System.Windows.Forms.BindingSource clsChecklistItemBindingSource;
        private System.Windows.Forms.BindingSource clsChecklistDetailsBindingSource;
        private System.Windows.Forms.BindingSource clsChecklistBindingSource;
        private System.Windows.Forms.BindingSource clsChecklistGeneratorBindingSource;
    }
}
