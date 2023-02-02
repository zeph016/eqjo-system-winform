namespace FGCIJOROSystem.Presentation.vChecklist
{
    partial class frmChecklistPrintLogs
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
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn2 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn1 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.Data.SortDescriptor sortDescriptor1 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChecklistPrintLogs));
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.viewChecklistPrintLogs = new Telerik.WinControls.UI.RadGridView();
            this.clsChecklistGeneratorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewChecklistPrintLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewChecklistPrintLogs.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsChecklistGeneratorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.pictureBox1);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(630, 32);
            this.radPanel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FGCIJOROSystem.Presentation.Properties.Resources.Logs;
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
            this.radLabel1.Location = new System.Drawing.Point(32, 6);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(164, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "CHECKLIST PRINT LOGS";
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.viewChecklistPrintLogs);
            this.radGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGroupBox1.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.radGroupBox1.HeaderText = "List Of Transactions";
            this.radGroupBox1.Location = new System.Drawing.Point(0, 32);
            this.radGroupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(2, 15, 2, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(630, 455);
            this.radGroupBox1.TabIndex = 8;
            this.radGroupBox1.Text = "List Of Transactions";
            // 
            // viewChecklistPrintLogs
            // 
            this.viewChecklistPrintLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.viewChecklistPrintLogs.Cursor = System.Windows.Forms.Cursors.Default;
            this.viewChecklistPrintLogs.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clsChecklistGeneratorBindingSource, "ChecklistNo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.viewChecklistPrintLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewChecklistPrintLogs.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.viewChecklistPrintLogs.ForeColor = System.Drawing.Color.Black;
            this.viewChecklistPrintLogs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.viewChecklistPrintLogs.Location = new System.Drawing.Point(2, 15);
            this.viewChecklistPrintLogs.Margin = new System.Windows.Forms.Padding(2);
            // 
            // 
            // 
            this.viewChecklistPrintLogs.MasterTemplate.AllowAddNewRow = false;
            this.viewChecklistPrintLogs.MasterTemplate.AllowSearchRow = true;
            this.viewChecklistPrintLogs.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewDecimalColumn1.DataType = typeof(long);
            gridViewDecimalColumn1.EnableExpressionEditor = false;
            gridViewDecimalColumn1.FieldName = "Id";
            gridViewDecimalColumn1.HeaderText = "Id";
            gridViewDecimalColumn1.IsAutoGenerated = true;
            gridViewDecimalColumn1.IsVisible = false;
            gridViewDecimalColumn1.Name = "Id";
            gridViewDecimalColumn1.Width = 71;
            gridViewDecimalColumn2.DataType = typeof(long);
            gridViewDecimalColumn2.EnableExpressionEditor = false;
            gridViewDecimalColumn2.FieldName = "ChecklistNo";
            gridViewDecimalColumn2.HeaderText = "ChecklistNo";
            gridViewDecimalColumn2.IsAutoGenerated = true;
            gridViewDecimalColumn2.IsVisible = false;
            gridViewDecimalColumn2.MaxWidth = 90;
            gridViewDecimalColumn2.MinWidth = 90;
            gridViewDecimalColumn2.Name = "ChecklistNo";
            gridViewDecimalColumn2.Width = 90;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "ChecklistNoStr";
            gridViewTextBoxColumn1.HeaderText = "Checklist No.";
            gridViewTextBoxColumn1.IsAutoGenerated = true;
            gridViewTextBoxColumn1.MaxWidth = 90;
            gridViewTextBoxColumn1.MinWidth = 90;
            gridViewTextBoxColumn1.Name = "ChecklistNoStr";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewTextBoxColumn1.Width = 90;
            gridViewDateTimeColumn1.EnableExpressionEditor = false;
            gridViewDateTimeColumn1.FieldName = "DateEncoded";
            gridViewDateTimeColumn1.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            gridViewDateTimeColumn1.HeaderText = "Date Encoded";
            gridViewDateTimeColumn1.IsAutoGenerated = true;
            gridViewDateTimeColumn1.Name = "DateEncoded";
            gridViewDateTimeColumn1.Width = 179;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "UserName";
            gridViewTextBoxColumn2.HeaderText = "User Name";
            gridViewTextBoxColumn2.Name = "UserName";
            gridViewTextBoxColumn2.Width = 339;
            this.viewChecklistPrintLogs.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewDecimalColumn1,
            gridViewDecimalColumn2,
            gridViewTextBoxColumn1,
            gridViewDateTimeColumn1,
            gridViewTextBoxColumn2});
            this.viewChecklistPrintLogs.MasterTemplate.DataSource = this.clsChecklistGeneratorBindingSource;
            this.viewChecklistPrintLogs.MasterTemplate.EnableGrouping = false;
            sortDescriptor1.PropertyName = "ChecklistNoStr";
            this.viewChecklistPrintLogs.MasterTemplate.SortDescriptors.AddRange(new Telerik.WinControls.Data.SortDescriptor[] {
            sortDescriptor1});
            this.viewChecklistPrintLogs.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.viewChecklistPrintLogs.Name = "viewChecklistPrintLogs";
            this.viewChecklistPrintLogs.ReadOnly = true;
            this.viewChecklistPrintLogs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.viewChecklistPrintLogs.Size = new System.Drawing.Size(626, 438);
            this.viewChecklistPrintLogs.TabIndex = 0;
            this.viewChecklistPrintLogs.Text = "radGridView1";
            // 
            // clsChecklistGeneratorBindingSource
            // 
            this.clsChecklistGeneratorBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Checklist.clsChecklistGenerator);
            // 
            // frmChecklistPrintLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 487);
            this.Controls.Add(this.radGroupBox1);
            this.Controls.Add(this.radPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChecklistPrintLogs";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.Load += new System.EventHandler(this.frmChecklistPrintLogs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewChecklistPrintLogs.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewChecklistPrintLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsChecklistGeneratorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGridView viewChecklistPrintLogs;
        private System.Windows.Forms.BindingSource clsChecklistGeneratorBindingSource;
    }
}
