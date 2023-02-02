namespace FGCIJOROSystem.Presentation
{
    partial class frmChangelogs
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
            Telerik.WinControls.UI.ListViewDataItem listViewDataItem1 = new Telerik.WinControls.UI.ListViewDataItem("1. Added Mileage on JO Report above driver.");
            Telerik.WinControls.UI.ListViewDataItem listViewDataItem2 = new Telerik.WinControls.UI.ListViewDataItem("2. Added cause of damage above remarks on JO Report.");
            Telerik.WinControls.UI.ListViewDataItem listViewDataItem3 = new Telerik.WinControls.UI.ListViewDataItem("3. Capitalized form copy text");
            Telerik.WinControls.UI.ListViewDataItem listViewDataItem4 = new Telerik.WinControls.UI.ListViewDataItem("4. Modified font size of JO details on JO report.");
            this.lblVersion = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radListView1 = new Telerik.WinControls.UI.RadListView();
            this.btnClose = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.lblVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = false;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblVersion.ForeColor = System.Drawing.Color.Blue;
            this.lblVersion.Location = new System.Drawing.Point(12, 36);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblVersion.Size = new System.Drawing.Size(209, 19);
            this.lblVersion.TabIndex = 9;
            this.lblVersion.Text = "version :";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.radLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.radLabel1.Location = new System.Drawing.Point(11, 4);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(157, 27);
            this.radLabel1.TabIndex = 10;
            this.radLabel1.Text = "CHANGELOGS IN:";
            // 
            // radListView1
            // 
            this.radListView1.AllowEdit = false;
            this.radListView1.AllowRemove = false;
            listViewDataItem1.Text = "1. Added Mileage on JO Report above driver.";
            listViewDataItem2.Text = "2. Added cause of damage above remarks on JO Report.";
            listViewDataItem3.Text = "3. Capitalized form copy text";
            listViewDataItem4.Text = "4. Modified font size of JO details on JO report.";
            this.radListView1.Items.AddRange(new Telerik.WinControls.UI.ListViewDataItem[] {
            listViewDataItem1,
            listViewDataItem2,
            listViewDataItem3,
            listViewDataItem4});
            this.radListView1.Location = new System.Drawing.Point(12, 61);
            this.radListView1.Name = "radListView1";
            this.radListView1.Size = new System.Drawing.Size(384, 304);
            this.radListView1.TabIndex = 11;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(286, 371);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 24);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "CLOSE";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmChangelogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 400);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.radListView1);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.lblVersion);
            this.Name = "frmChangelogs";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHANGELOGS";
            ((System.ComponentModel.ISupportInitialize)(this.lblVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lblVersion;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadListView radListView1;
        private Telerik.WinControls.UI.RadButton btnClose;
    }
}