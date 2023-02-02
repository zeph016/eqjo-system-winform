namespace FGCIJOROSystem.Presentation.vLookups
{
    partial class frmExternalCustomerLookup
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
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn2 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn3 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn4 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn5 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn1 = new Telerik.WinControls.UI.GridViewCommandColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExternalCustomerLookup));
            Telerik.WinControls.Data.SortDescriptor sortDescriptor1 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn6 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            this.enumBinder1 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.enumBinder2 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.enumBinder3 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.enumBinder4 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.enumBinder5 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.clsCustomerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.enumBinder6 = new Telerik.WinControls.UI.Data.EnumBinder();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsCustomerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // enumBinder1
            // 
            this.enumBinder1.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.CustomerType);
            gridViewComboBoxColumn1.DataSource = this.enumBinder1;
            gridViewComboBoxColumn1.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.CustomerType);
            gridViewComboBoxColumn1.DisplayMember = "Description";
            gridViewComboBoxColumn1.FieldName = "CustomerType";
            gridViewComboBoxColumn1.HeaderText = "CustomerType";
            gridViewComboBoxColumn1.IsAutoGenerated = true;
            gridViewComboBoxColumn1.Name = "CustomerType";
            gridViewComboBoxColumn1.ValueMember = "Value";
            this.enumBinder1.Target = gridViewComboBoxColumn1;
            // 
            // enumBinder2
            // 
            this.enumBinder2.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.CustomerType);
            gridViewComboBoxColumn2.DataSource = this.enumBinder2;
            gridViewComboBoxColumn2.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.CustomerType);
            gridViewComboBoxColumn2.DisplayMember = "Description";
            gridViewComboBoxColumn2.FieldName = "CustomerType";
            gridViewComboBoxColumn2.HeaderText = "CustomerType";
            gridViewComboBoxColumn2.IsAutoGenerated = true;
            gridViewComboBoxColumn2.Name = "CustomerType";
            gridViewComboBoxColumn2.ValueMember = "Value";
            this.enumBinder2.Target = gridViewComboBoxColumn2;
            // 
            // enumBinder3
            // 
            this.enumBinder3.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.CustomerType);
            gridViewComboBoxColumn3.DataSource = this.enumBinder3;
            gridViewComboBoxColumn3.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.CustomerType);
            gridViewComboBoxColumn3.DisplayMember = "Description";
            gridViewComboBoxColumn3.FieldName = "CustomerType";
            gridViewComboBoxColumn3.HeaderText = "CustomerType";
            gridViewComboBoxColumn3.IsAutoGenerated = true;
            gridViewComboBoxColumn3.Name = "CustomerType";
            gridViewComboBoxColumn3.ValueMember = "Value";
            this.enumBinder3.Target = gridViewComboBoxColumn3;
            // 
            // enumBinder4
            // 
            this.enumBinder4.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.CustomerType);
            gridViewComboBoxColumn4.DataSource = this.enumBinder4;
            gridViewComboBoxColumn4.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.CustomerType);
            gridViewComboBoxColumn4.DisplayMember = "Description";
            gridViewComboBoxColumn4.FieldName = "CustomerType";
            gridViewComboBoxColumn4.HeaderText = "CustomerType";
            gridViewComboBoxColumn4.IsAutoGenerated = true;
            gridViewComboBoxColumn4.Name = "CustomerType";
            gridViewComboBoxColumn4.ValueMember = "Value";
            this.enumBinder4.Target = gridViewComboBoxColumn4;
            // 
            // enumBinder5
            // 
            this.enumBinder5.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.CustomerType);
            gridViewComboBoxColumn5.DataSource = this.enumBinder5;
            gridViewComboBoxColumn5.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.CustomerType);
            gridViewComboBoxColumn5.DisplayMember = "Description";
            gridViewComboBoxColumn5.FieldName = "CustomerType";
            gridViewComboBoxColumn5.HeaderText = "CustomerType";
            gridViewComboBoxColumn5.IsAutoGenerated = true;
            gridViewComboBoxColumn5.Name = "CustomerType";
            gridViewComboBoxColumn5.ValueMember = "Value";
            this.enumBinder5.Target = gridViewComboBoxColumn5;
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.pictureBox1);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(776, 34);
            this.radPanel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FGCIJOROSystem.Presentation.Properties.Resources.Search40;
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
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
            this.radLabel1.Location = new System.Drawing.Point(29, 4);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(147, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "CUSTOMER LOOK UP";
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radGroupBox1.Controls.Add(this.radGridView1);
            this.radGroupBox1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold);
            this.radGroupBox1.HeaderText = "Details";
            this.radGroupBox1.Location = new System.Drawing.Point(0, 37);
            this.radGroupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(2, 15, 2, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(776, 460);
            this.radGroupBox1.TabIndex = 2;
            this.radGroupBox1.Text = "Details";
            // 
            // radGridView1
            // 
            this.radGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radGridView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.radGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radGridView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radGridView1.ForeColor = System.Drawing.Color.Black;
            this.radGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radGridView1.Location = new System.Drawing.Point(4, 17);
            this.radGridView1.Margin = new System.Windows.Forms.Padding(2);
            // 
            // 
            // 
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AllowSearchRow = true;
            this.radGridView1.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewDecimalColumn1.DataType = typeof(long);
            gridViewDecimalColumn1.EnableExpressionEditor = false;
            gridViewDecimalColumn1.FieldName = "Id";
            gridViewDecimalColumn1.HeaderText = "Id";
            gridViewDecimalColumn1.IsAutoGenerated = true;
            gridViewDecimalColumn1.IsVisible = false;
            gridViewDecimalColumn1.Name = "Id";
            gridViewDecimalColumn1.Width = 37;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "CustomerName";
            gridViewTextBoxColumn1.HeaderText = "Name";
            gridViewTextBoxColumn1.IsAutoGenerated = true;
            gridViewTextBoxColumn1.Name = "CustomerName";
            gridViewTextBoxColumn1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewTextBoxColumn1.Width = 318;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "CustomerAddress";
            gridViewTextBoxColumn2.HeaderText = "Address";
            gridViewTextBoxColumn2.IsAutoGenerated = true;
            gridViewTextBoxColumn2.Name = "CustomerAddress";
            gridViewTextBoxColumn2.Width = 236;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "ContactNo";
            gridViewTextBoxColumn3.HeaderText = "Contact No.";
            gridViewTextBoxColumn3.IsAutoGenerated = true;
            gridViewTextBoxColumn3.Name = "ContactNo";
            gridViewTextBoxColumn3.Width = 167;
            gridViewCommandColumn1.EnableExpressionEditor = false;
            gridViewCommandColumn1.FieldName = "btnSelect";
            gridViewCommandColumn1.HeaderImage = ((System.Drawing.Image)(resources.GetObject("gridViewCommandColumn1.HeaderImage")));
            gridViewCommandColumn1.Image = ((System.Drawing.Image)(resources.GetObject("gridViewCommandColumn1.Image")));
            gridViewCommandColumn1.MaxWidth = 30;
            gridViewCommandColumn1.MinWidth = 30;
            gridViewCommandColumn1.Name = "btnSelect";
            gridViewCommandColumn1.Width = 30;
            this.radGridView1.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewDecimalColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewCommandColumn1});
            this.radGridView1.MasterTemplate.DataSource = this.clsCustomerBindingSource;
            this.radGridView1.MasterTemplate.EnableFiltering = true;
            this.radGridView1.MasterTemplate.EnableGrouping = false;
            this.radGridView1.MasterTemplate.ShowFilteringRow = false;
            sortDescriptor1.PropertyName = "CustomerName";
            this.radGridView1.MasterTemplate.SortDescriptors.AddRange(new Telerik.WinControls.Data.SortDescriptor[] {
            sortDescriptor1});
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.ReadOnly = true;
            this.radGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radGridView1.ShowGroupPanel = false;
            this.radGridView1.Size = new System.Drawing.Size(768, 438);
            this.radGridView1.TabIndex = 0;
            this.radGridView1.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.radGridView1_CellFormatting);
            this.radGridView1.CommandCellClick += new Telerik.WinControls.UI.CommandCellClickEventHandler(this.radGridView1_CommandCellClick);
            // 
            // clsCustomerBindingSource
            // 
            this.clsCustomerBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Configurations.Customers.clsCustomer);
            // 
            // enumBinder6
            // 
            this.enumBinder6.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.CustomerType);
            gridViewComboBoxColumn6.DataSource = this.enumBinder6;
            gridViewComboBoxColumn6.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.CustomerType);
            gridViewComboBoxColumn6.DisplayMember = "Description";
            gridViewComboBoxColumn6.FieldName = "CustomerType";
            gridViewComboBoxColumn6.HeaderText = "CustomerType";
            gridViewComboBoxColumn6.IsAutoGenerated = true;
            gridViewComboBoxColumn6.Name = "CustomerType";
            gridViewComboBoxColumn6.ValueMember = "Value";
            this.enumBinder6.Target = gridViewComboBoxColumn6;
            // 
            // frmExternalCustomerLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 498);
            this.Controls.Add(this.radGroupBox1);
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExternalCustomerLookup";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.Load += new System.EventHandler(this.frmExternalCustomerLookup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsCustomerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private System.Windows.Forms.BindingSource clsCustomerBindingSource;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder1;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder2;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder3;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder4;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder5;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder6;
    }
}
