namespace FGCIJOROSystem.Presentation.vChecklist
{
    partial class ChecklistPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChecklistPrint));
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.cbPrinterOption = new Telerik.WinControls.UI.RadDropDownList();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            this.clsChecklistTransactionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.radSpinEditor1 = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPrinterOption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsChecklistTransactionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSpinEditor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(9, 50);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(81, 18);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Select Printer : ";
            // 
            // cbPrinterOption
            // 
            this.cbPrinterOption.Location = new System.Drawing.Point(92, 50);
            this.cbPrinterOption.Margin = new System.Windows.Forms.Padding(2);
            this.cbPrinterOption.Name = "cbPrinterOption";
            this.cbPrinterOption.Size = new System.Drawing.Size(251, 20);
            this.cbPrinterOption.TabIndex = 1;
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.pictureBox1);
            this.radPanel1.Controls.Add(this.radLabel2);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(352, 32);
            this.radPanel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FGCIJOROSystem.Presentation.Properties.Resources.Checklist;
            this.pictureBox1.Location = new System.Drawing.Point(4, 3);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(30, 6);
            this.radLabel2.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(125, 22);
            this.radLabel2.TabIndex = 0;
            this.radLabel2.Text = "PRINT CHECKLIST";
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.Location = new System.Drawing.Point(229, 108);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(114, 32);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.TextWrap = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // clsChecklistTransactionBindingSource
            // 
            this.clsChecklistTransactionBindingSource.DataSource = typeof(FGCIJOROSystem.Domain.Checklist.clsChecklistGenerator);
            // 
            // radSpinEditor1
            // 
            this.radSpinEditor1.Location = new System.Drawing.Point(92, 84);
            this.radSpinEditor1.Margin = new System.Windows.Forms.Padding(2);
            this.radSpinEditor1.Name = "radSpinEditor1";
            this.radSpinEditor1.Size = new System.Drawing.Size(85, 20);
            this.radSpinEditor1.TabIndex = 16;
            this.radSpinEditor1.TabStop = false;
            this.radSpinEditor1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.radSpinEditor1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(9, 84);
            this.radLabel3.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(79, 18);
            this.radLabel3.TabIndex = 15;
            this.radLabel3.Text = "No. of Copies :";
            // 
            // ChecklistPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 147);
            this.Controls.Add(this.radSpinEditor1);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.cbPrinterOption);
            this.Controls.Add(this.radLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChecklistPrint";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.Load += new System.EventHandler(this.ChecklistPrint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPrinterOption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsChecklistTransactionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSpinEditor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList cbPrinterOption;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private System.Windows.Forms.BindingSource clsChecklistTransactionBindingSource;
        private Telerik.WinControls.UI.RadSpinEditor radSpinEditor1;
        private Telerik.WinControls.UI.RadLabel radLabel3;
    }
}
