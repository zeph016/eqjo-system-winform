namespace FGCIJOROSystem.Presentation.vLookups
{
    partial class frmPersonnelLookup
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
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn1 = new Telerik.WinControls.UI.GridViewCommandColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPersonnelLookup));
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn2 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn3 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn4 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn5 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn2 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.Data.GroupDescriptor groupDescriptor1 = new Telerik.WinControls.Data.GroupDescriptor();
            Telerik.WinControls.Data.SortDescriptor sortDescriptor1 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.dgvEmployee = new Telerik.WinControls.UI.RadGridView();
            this.clsPersonnelsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPersonnelsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEmployee
            // 
            this.dgvEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.dgvEmployee.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployee.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvEmployee.ForeColor = System.Drawing.Color.Black;
            this.dgvEmployee.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvEmployee.Location = new System.Drawing.Point(0, 34);
            this.dgvEmployee.Margin = new System.Windows.Forms.Padding(2);
            // 
            // 
            // 
            this.dgvEmployee.MasterTemplate.AllowAddNewRow = false;
            this.dgvEmployee.MasterTemplate.AllowSearchRow = true;
            this.dgvEmployee.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewCommandColumn1.EnableExpressionEditor = false;
            gridViewCommandColumn1.HeaderImage = ((System.Drawing.Image)(resources.GetObject("gridViewCommandColumn1.HeaderImage")));
            gridViewCommandColumn1.Image = ((System.Drawing.Image)(resources.GetObject("gridViewCommandColumn1.Image")));
            gridViewCommandColumn1.IsPinned = true;
            gridViewCommandColumn1.MaxWidth = 30;
            gridViewCommandColumn1.MinWidth = 30;
            gridViewCommandColumn1.Name = "btnSelect";
            gridViewCommandColumn1.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Right;
            gridViewCommandColumn1.Width = 30;
            gridViewDecimalColumn1.DataType = typeof(long);
            gridViewDecimalColumn1.EnableExpressionEditor = false;
            gridViewDecimalColumn1.FieldName = "Id";
            gridViewDecimalColumn1.HeaderText = "Id";
            gridViewDecimalColumn1.IsAutoGenerated = true;
            gridViewDecimalColumn1.IsVisible = false;
            gridViewDecimalColumn1.Name = "Id";
            gridViewDecimalColumn1.Width = 34;
            gridViewDecimalColumn2.DataType = typeof(long);
            gridViewDecimalColumn2.EnableExpressionEditor = false;
            gridViewDecimalColumn2.FieldName = "EmployeeId";
            gridViewDecimalColumn2.HeaderText = "EmployeeId";
            gridViewDecimalColumn2.IsAutoGenerated = true;
            gridViewDecimalColumn2.IsVisible = false;
            gridViewDecimalColumn2.Name = "EmployeeId";
            gridViewDecimalColumn2.Width = 37;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "FirstName";
            gridViewTextBoxColumn1.HeaderText = "FirstName";
            gridViewTextBoxColumn1.IsAutoGenerated = true;
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "FirstName";
            gridViewTextBoxColumn1.Width = 40;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "MiddleName";
            gridViewTextBoxColumn2.HeaderText = "MiddleName";
            gridViewTextBoxColumn2.IsAutoGenerated = true;
            gridViewTextBoxColumn2.IsVisible = false;
            gridViewTextBoxColumn2.Name = "MiddleName";
            gridViewTextBoxColumn2.Width = 44;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "LastName";
            gridViewTextBoxColumn3.HeaderText = "LastName";
            gridViewTextBoxColumn3.IsAutoGenerated = true;
            gridViewTextBoxColumn3.IsVisible = false;
            gridViewTextBoxColumn3.Name = "LastName";
            gridViewTextBoxColumn3.Width = 48;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "NameExtension";
            gridViewTextBoxColumn4.HeaderText = "NameExtension";
            gridViewTextBoxColumn4.IsAutoGenerated = true;
            gridViewTextBoxColumn4.IsVisible = false;
            gridViewTextBoxColumn4.Name = "NameExtension";
            gridViewTextBoxColumn4.Width = 53;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "FullName";
            gridViewTextBoxColumn5.HeaderText = "Full Name";
            gridViewTextBoxColumn5.IsAutoGenerated = true;
            gridViewTextBoxColumn5.Name = "FullName";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 336;
            gridViewDecimalColumn3.DataType = typeof(long);
            gridViewDecimalColumn3.EnableExpressionEditor = false;
            gridViewDecimalColumn3.FieldName = "SectionId";
            gridViewDecimalColumn3.HeaderText = "SectionId";
            gridViewDecimalColumn3.IsAutoGenerated = true;
            gridViewDecimalColumn3.IsVisible = false;
            gridViewDecimalColumn3.Name = "SectionId";
            gridViewDecimalColumn3.Width = 60;
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "SectionName";
            gridViewTextBoxColumn6.HeaderText = "Section Name";
            gridViewTextBoxColumn6.IsAutoGenerated = true;
            gridViewTextBoxColumn6.Name = "SectionName";
            gridViewTextBoxColumn6.Width = 119;
            gridViewDecimalColumn4.DataType = typeof(long);
            gridViewDecimalColumn4.EnableExpressionEditor = false;
            gridViewDecimalColumn4.FieldName = "PositionId";
            gridViewDecimalColumn4.HeaderText = "PositionId";
            gridViewDecimalColumn4.IsAutoGenerated = true;
            gridViewDecimalColumn4.IsVisible = false;
            gridViewDecimalColumn4.Name = "PositionId";
            gridViewDecimalColumn4.Width = 69;
            gridViewTextBoxColumn7.EnableExpressionEditor = false;
            gridViewTextBoxColumn7.FieldName = "PositionName";
            gridViewTextBoxColumn7.HeaderText = "Position Name";
            gridViewTextBoxColumn7.IsAutoGenerated = true;
            gridViewTextBoxColumn7.Name = "PositionName";
            gridViewTextBoxColumn7.Width = 201;
            gridViewCheckBoxColumn1.EnableExpressionEditor = false;
            gridViewCheckBoxColumn1.FieldName = "IsSectionHead";
            gridViewCheckBoxColumn1.HeaderText = "Section Head";
            gridViewCheckBoxColumn1.IsAutoGenerated = true;
            gridViewCheckBoxColumn1.MinWidth = 20;
            gridViewCheckBoxColumn1.Name = "IsSectionHead";
            gridViewCheckBoxColumn1.Width = 113;
            gridViewDecimalColumn5.DataType = typeof(long);
            gridViewDecimalColumn5.EnableExpressionEditor = false;
            gridViewDecimalColumn5.FieldName = "BranchId";
            gridViewDecimalColumn5.HeaderText = "BranchId";
            gridViewDecimalColumn5.IsAutoGenerated = true;
            gridViewDecimalColumn5.IsVisible = false;
            gridViewDecimalColumn5.Name = "BranchId";
            gridViewDecimalColumn5.Width = 81;
            gridViewTextBoxColumn8.EnableExpressionEditor = false;
            gridViewTextBoxColumn8.FieldName = "BranchName";
            gridViewTextBoxColumn8.HeaderText = "BranchName";
            gridViewTextBoxColumn8.IsAutoGenerated = true;
            gridViewTextBoxColumn8.IsVisible = false;
            gridViewTextBoxColumn8.Name = "BranchName";
            gridViewTextBoxColumn8.Width = 98;
            gridViewCheckBoxColumn2.EnableExpressionEditor = false;
            gridViewCheckBoxColumn2.FieldName = "Active";
            gridViewCheckBoxColumn2.HeaderText = "Active";
            gridViewCheckBoxColumn2.IsAutoGenerated = true;
            gridViewCheckBoxColumn2.IsVisible = false;
            gridViewCheckBoxColumn2.MinWidth = 20;
            gridViewCheckBoxColumn2.Name = "Active";
            gridViewCheckBoxColumn2.Width = 89;
            this.dgvEmployee.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewCommandColumn1,
            gridViewDecimalColumn1,
            gridViewDecimalColumn2,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewDecimalColumn3,
            gridViewTextBoxColumn6,
            gridViewDecimalColumn4,
            gridViewTextBoxColumn7,
            gridViewCheckBoxColumn1,
            gridViewDecimalColumn5,
            gridViewTextBoxColumn8,
            gridViewCheckBoxColumn2});
            this.dgvEmployee.MasterTemplate.DataSource = this.clsPersonnelsBindingSource;
            sortDescriptor1.PropertyName = "SectionName";
            groupDescriptor1.GroupNames.AddRange(new Telerik.WinControls.Data.SortDescriptor[] {
            sortDescriptor1});
            this.dgvEmployee.MasterTemplate.GroupDescriptors.AddRange(new Telerik.WinControls.Data.GroupDescriptor[] {
            groupDescriptor1});
            this.dgvEmployee.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvEmployee.Name = "dgvEmployee";
            this.dgvEmployee.ReadOnly = true;
            this.dgvEmployee.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvEmployee.ShowGroupPanel = false;
            this.dgvEmployee.Size = new System.Drawing.Size(718, 395);
            this.dgvEmployee.TabIndex = 5;
            this.dgvEmployee.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.dgvEmployee_CellFormatting);
            this.dgvEmployee.CommandCellClick += new Telerik.WinControls.UI.CommandCellClickEventHandler(this.dgvEmployee_CommandCellClick);
            // 
            // clsPersonnelsBindingSource
            // 
            this.clsPersonnelsBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Configurations.Personnels.clsPersonnels);
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.pictureBox1);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(718, 34);
            this.radPanel1.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FGCIJOROSystem.Presentation.Properties.Resources.Search40;
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
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
            this.radLabel1.Location = new System.Drawing.Point(29, 8);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(151, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "PERSONNEL LOOK UP";
            // 
            // frmPersonnelLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 429);
            this.Controls.Add(this.dgvEmployee);
            this.Controls.Add(this.radPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPersonnelLookup";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "";
            this.Load += new System.EventHandler(this.frmPersonnelLookup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPersonnelsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView dgvEmployee;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.BindingSource clsPersonnelsBindingSource;
    }
}
