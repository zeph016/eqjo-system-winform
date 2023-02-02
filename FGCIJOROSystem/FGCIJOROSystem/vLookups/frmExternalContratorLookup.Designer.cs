namespace FGCIJOROSystem.Presentation.vLookups
{
    partial class frmExternalContratorLookup
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
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn5 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn6 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn1 = new Telerik.WinControls.UI.GridViewCommandColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExternalContratorLookup));
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.enumBinder1 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.enumBinder2 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.enumBinder3 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.enumBinder4 = new Telerik.WinControls.UI.Data.EnumBinder();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.tbSearch = new Telerik.WinControls.UI.RadTextBoxControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.clsContractorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsContractorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // enumBinder1
            // 
            this.enumBinder1.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.ContractorType);
            gridViewComboBoxColumn1.DataSource = this.enumBinder1;
            gridViewComboBoxColumn1.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.ContractorType);
            gridViewComboBoxColumn1.DisplayMember = "Description";
            gridViewComboBoxColumn1.FieldName = "ContractorType";
            gridViewComboBoxColumn1.HeaderText = "ContractorType";
            gridViewComboBoxColumn1.IsAutoGenerated = true;
            gridViewComboBoxColumn1.Name = "ContractorType";
            gridViewComboBoxColumn1.ValueMember = "Value";
            this.enumBinder1.Target = gridViewComboBoxColumn1;
            // 
            // enumBinder2
            // 
            this.enumBinder2.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.ContractorCategory);
            gridViewComboBoxColumn2.DataSource = this.enumBinder2;
            gridViewComboBoxColumn2.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.ContractorCategory);
            gridViewComboBoxColumn2.DisplayMember = "Description";
            gridViewComboBoxColumn2.FieldName = "NameType";
            gridViewComboBoxColumn2.HeaderText = "NameType";
            gridViewComboBoxColumn2.IsAutoGenerated = true;
            gridViewComboBoxColumn2.Name = "NameType";
            gridViewComboBoxColumn2.ValueMember = "Value";
            this.enumBinder2.Target = gridViewComboBoxColumn2;
            // 
            // enumBinder3
            // 
            this.enumBinder3.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.ContractorType);
            gridViewComboBoxColumn3.DataSource = this.enumBinder3;
            gridViewComboBoxColumn3.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.ContractorType);
            gridViewComboBoxColumn3.DisplayMember = "Description";
            gridViewComboBoxColumn3.FieldName = "ContractorType";
            gridViewComboBoxColumn3.HeaderText = "ContractorType";
            gridViewComboBoxColumn3.IsAutoGenerated = true;
            gridViewComboBoxColumn3.Name = "ContractorType";
            gridViewComboBoxColumn3.ValueMember = "Value";
            this.enumBinder3.Target = gridViewComboBoxColumn3;
            // 
            // enumBinder4
            // 
            this.enumBinder4.Source = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.ContractorCategory);
            gridViewComboBoxColumn4.DataSource = this.enumBinder4;
            gridViewComboBoxColumn4.DataType = typeof(FGCIJOROSystem.Domain.Enums.clsEnums.ContractorCategory);
            gridViewComboBoxColumn4.DisplayMember = "Description";
            gridViewComboBoxColumn4.FieldName = "ContractorCategory";
            gridViewComboBoxColumn4.HeaderText = "ContractorCategory";
            gridViewComboBoxColumn4.IsAutoGenerated = true;
            gridViewComboBoxColumn4.Name = "ContractorCategory";
            gridViewComboBoxColumn4.ValueMember = "Value";
            this.enumBinder4.Target = gridViewComboBoxColumn4;
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.radGroupBox2);
            this.radPanel1.Controls.Add(this.pictureBox1);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(859, 34);
            this.radPanel1.TabIndex = 3;
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox2.Controls.Add(this.radLabel2);
            this.radGroupBox2.Controls.Add(this.tbSearch);
            this.radGroupBox2.HeaderText = "";
            this.radGroupBox2.Location = new System.Drawing.Point(490, 3);
            this.radGroupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Padding = new System.Windows.Forms.Padding(2, 15, 2, 2);
            this.radGroupBox2.Size = new System.Drawing.Size(367, 28);
            this.radGroupBox2.TabIndex = 5;
            this.radGroupBox2.Visible = false;
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(4, 6);
            this.radLabel2.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(84, 18);
            this.radLabel2.TabIndex = 1;
            this.radLabel2.Text = "Type to search :";
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(92, 5);
            this.tbSearch.Margin = new System.Windows.Forms.Padding(2);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(271, 20);
            this.tbSearch.TabIndex = 0;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
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
            this.radLabel1.Size = new System.Drawing.Size(165, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "CONTRACTOR LOOK UP";
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
            this.radGroupBox1.Location = new System.Drawing.Point(2, 38);
            this.radGroupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(2, 15, 2, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(857, 469);
            this.radGroupBox1.TabIndex = 4;
            this.radGroupBox1.Text = "Details";
            // 
            // radGridView1
            // 
            this.radGridView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.radGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radGridView1.ForeColor = System.Drawing.Color.Black;
            this.radGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radGridView1.Location = new System.Drawing.Point(2, 15);
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
            gridViewComboBoxColumn5.DataSource = this.enumBinder3;
            gridViewComboBoxColumn5.DataType = typeof(object);
            gridViewComboBoxColumn5.DisplayMember = "Description";
            gridViewComboBoxColumn5.EnableExpressionEditor = false;
            gridViewComboBoxColumn5.FieldName = "ContractorType";
            gridViewComboBoxColumn5.HeaderText = "Type";
            gridViewComboBoxColumn5.IsAutoGenerated = true;
            gridViewComboBoxColumn5.Name = "ContractorType";
            gridViewComboBoxColumn5.ValueMember = "Value";
            gridViewComboBoxColumn5.Width = 105;
            gridViewComboBoxColumn6.DataSource = this.enumBinder4;
            gridViewComboBoxColumn6.DataType = typeof(object);
            gridViewComboBoxColumn6.DisplayMember = "Description";
            gridViewComboBoxColumn6.EnableExpressionEditor = false;
            gridViewComboBoxColumn6.FieldName = "ContractorCategory";
            gridViewComboBoxColumn6.HeaderText = "Category";
            gridViewComboBoxColumn6.IsAutoGenerated = true;
            gridViewComboBoxColumn6.Name = "ContractorCategory";
            gridViewComboBoxColumn6.ValueMember = "Value";
            gridViewComboBoxColumn6.Width = 139;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "Section";
            gridViewTextBoxColumn1.HeaderText = "Contractor / Section";
            gridViewTextBoxColumn1.IsAutoGenerated = true;
            gridViewTextBoxColumn1.Name = "Section";
            gridViewTextBoxColumn1.Width = 177;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "SectionHead";
            gridViewTextBoxColumn2.HeaderText = "Section Head";
            gridViewTextBoxColumn2.IsAutoGenerated = true;
            gridViewTextBoxColumn2.Name = "SectionHead";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 233;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "FullName";
            gridViewTextBoxColumn3.HeaderText = "FullName";
            gridViewTextBoxColumn3.IsAutoGenerated = true;
            gridViewTextBoxColumn3.IsVisible = false;
            gridViewTextBoxColumn3.Name = "FullName";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "Firstname";
            gridViewTextBoxColumn4.HeaderText = "Firstname";
            gridViewTextBoxColumn4.IsAutoGenerated = true;
            gridViewTextBoxColumn4.IsVisible = false;
            gridViewTextBoxColumn4.Name = "Firstname";
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "Middlename";
            gridViewTextBoxColumn5.HeaderText = "Middlename";
            gridViewTextBoxColumn5.IsAutoGenerated = true;
            gridViewTextBoxColumn5.IsVisible = false;
            gridViewTextBoxColumn5.Name = "Middlename";
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "Lastname";
            gridViewTextBoxColumn6.HeaderText = "Lastname";
            gridViewTextBoxColumn6.IsAutoGenerated = true;
            gridViewTextBoxColumn6.IsVisible = false;
            gridViewTextBoxColumn6.Name = "Lastname";
            gridViewTextBoxColumn7.EnableExpressionEditor = false;
            gridViewTextBoxColumn7.FieldName = "NameExtension";
            gridViewTextBoxColumn7.HeaderText = "NameExtension";
            gridViewTextBoxColumn7.IsAutoGenerated = true;
            gridViewTextBoxColumn7.IsVisible = false;
            gridViewTextBoxColumn7.Name = "NameExtension";
            gridViewTextBoxColumn8.EnableExpressionEditor = false;
            gridViewTextBoxColumn8.FieldName = "CompanyName";
            gridViewTextBoxColumn8.HeaderText = "Company Name";
            gridViewTextBoxColumn8.IsAutoGenerated = true;
            gridViewTextBoxColumn8.Name = "CompanyName";
            gridViewTextBoxColumn8.Width = 154;
            gridViewTextBoxColumn9.EnableExpressionEditor = false;
            gridViewTextBoxColumn9.FieldName = "Address";
            gridViewTextBoxColumn9.HeaderText = "Address";
            gridViewTextBoxColumn9.IsAutoGenerated = true;
            gridViewTextBoxColumn9.IsVisible = false;
            gridViewTextBoxColumn9.Name = "Address";
            gridViewTextBoxColumn10.EnableExpressionEditor = false;
            gridViewTextBoxColumn10.FieldName = "ContactNos";
            gridViewTextBoxColumn10.HeaderText = "ContactNos";
            gridViewTextBoxColumn10.IsAutoGenerated = true;
            gridViewTextBoxColumn10.IsVisible = false;
            gridViewTextBoxColumn10.Name = "ContactNos";
            gridViewCheckBoxColumn1.EnableExpressionEditor = false;
            gridViewCheckBoxColumn1.FieldName = "Active";
            gridViewCheckBoxColumn1.HeaderText = "Active";
            gridViewCheckBoxColumn1.IsAutoGenerated = true;
            gridViewCheckBoxColumn1.IsVisible = false;
            gridViewCheckBoxColumn1.MinWidth = 20;
            gridViewCheckBoxColumn1.Name = "Active";
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
            gridViewComboBoxColumn5,
            gridViewComboBoxColumn6,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10,
            gridViewCheckBoxColumn1,
            gridViewCommandColumn1});
            this.radGridView1.MasterTemplate.DataSource = this.clsContractorBindingSource;
            this.radGridView1.MasterTemplate.EnableFiltering = true;
            this.radGridView1.MasterTemplate.EnableGrouping = false;
            this.radGridView1.MasterTemplate.ShowFilteringRow = false;
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.ReadOnly = true;
            this.radGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radGridView1.ShowGroupPanel = false;
            this.radGridView1.Size = new System.Drawing.Size(853, 452);
            this.radGridView1.TabIndex = 1;
            this.radGridView1.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.radGridView1_CellFormatting);
            this.radGridView1.CommandCellClick += new Telerik.WinControls.UI.CommandCellClickEventHandler(this.radGridView1_CommandCellClick);
            this.radGridView1.CustomFiltering += new Telerik.WinControls.UI.GridViewCustomFilteringEventHandler(this.radGridView1_CustomFiltering);
            // 
            // clsContractorBindingSource
            // 
            this.clsContractorBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Configurations.Contractors.clsContractor);
            // 
            // frmExternalContratorLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 511);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.radGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExternalContratorLookup";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.Load += new System.EventHandler(this.frmExternalContratorLookup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            this.radGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsContractorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder1;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder2;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder3;
        private Telerik.WinControls.UI.Data.EnumBinder enumBinder4;
        private System.Windows.Forms.BindingSource clsContractorBindingSource;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBoxControl tbSearch;
    }
}
