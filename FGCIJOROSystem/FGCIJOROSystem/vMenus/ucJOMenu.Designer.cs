namespace FGCIJOROSystem.Presentation.vMenus
{
    partial class ucJOMenu
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radPanorama1 = new Telerik.WinControls.UI.RadPanorama();
            this.mJOEntry = new Telerik.WinControls.UI.RadTileElement();
            this.mJOList = new Telerik.WinControls.UI.RadTileElement();
            this.mJOAuditTrail = new Telerik.WinControls.UI.RadTileElement();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.btnEntry = new Telerik.WinControls.UI.RadButton();
            this.btnList = new Telerik.WinControls.UI.RadButton();
            this.btnReleasal = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanorama1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEntry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReleasal)).BeginInit();
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
            this.radPanel1.Size = new System.Drawing.Size(930, 32);
            this.radPanel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FGCIJOROSystem.Presentation.Properties.Resources.Configuration;
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
            this.radLabel1.Size = new System.Drawing.Size(129, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "JOB ORDER MENU";
            // 
            // radPanorama1
            // 
            this.radPanorama1.AllowDragDrop = false;
            this.radPanorama1.AutoScroll = true;
            this.radPanorama1.CellSize = new System.Drawing.Size(170, 170);
            this.radPanorama1.EnableZooming = false;
            this.radPanorama1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.mJOEntry,
            this.mJOList,
            this.mJOAuditTrail});
            this.radPanorama1.Location = new System.Drawing.Point(698, 84);
            this.radPanorama1.Margin = new System.Windows.Forms.Padding(2);
            this.radPanorama1.Name = "radPanorama1";
            this.radPanorama1.RowsCount = 5;
            this.radPanorama1.Size = new System.Drawing.Size(930, 450);
            this.radPanorama1.TabIndex = 0;
            // 
            // mJOEntry
            // 
            this.mJOEntry.ColSpan = 2;
            this.mJOEntry.Name = "mJOEntry";
            this.mJOEntry.Row = 2;
            this.mJOEntry.Text = "JOB ORDER ENTRY";
            this.mJOEntry.Click += new System.EventHandler(this.mJOEntry_Click);
            // 
            // mJOList
            // 
            this.mJOList.AccessibleDescription = "JOB ORDER LISTS";
            this.mJOList.AccessibleName = "JOB ORDER LISTS";
            this.mJOList.ColSpan = 4;
            this.mJOList.Name = "mJOList";
            this.mJOList.RowSpan = 2;
            this.mJOList.Text = "JO ORDER LISTS";
            this.mJOList.Click += new System.EventHandler(this.mJOList_Click);
            // 
            // mJOAuditTrail
            // 
            this.mJOAuditTrail.AccessibleDescription = "AUDIT TRAIL";
            this.mJOAuditTrail.AccessibleName = "AUDIT TRAIL";
            this.mJOAuditTrail.ColSpan = 2;
            this.mJOAuditTrail.Column = 2;
            this.mJOAuditTrail.Name = "mJOAuditTrail";
            this.mJOAuditTrail.Row = 2;
            this.mJOAuditTrail.Text = "EQUIPMENT RELEASED LIST";
            this.mJOAuditTrail.TextWrap = true;
            this.mJOAuditTrail.Click += new System.EventHandler(this.mJOAuditTrail_Click);
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.btnEntry);
            this.radPanel2.Controls.Add(this.btnList);
            this.radPanel2.Controls.Add(this.btnReleasal);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel2.Location = new System.Drawing.Point(0, 32);
            this.radPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(930, 450);
            this.radPanel2.TabIndex = 6;
            // 
            // btnEntry
            // 
            this.btnEntry.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEntry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntry.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnEntry.Image = global::FGCIJOROSystem.Presentation.Properties.Resources.icons8_data_recovery_100;
            this.btnEntry.ImageAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEntry.Location = new System.Drawing.Point(340, 80);
            this.btnEntry.Margin = new System.Windows.Forms.Padding(2);
            this.btnEntry.Name = "btnEntry";
            this.btnEntry.Size = new System.Drawing.Size(240, 289);
            this.btnEntry.TabIndex = 1;
            this.btnEntry.Text = "JOB ORDER ENTRY";
            this.btnEntry.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnEntry.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEntry.Click += new System.EventHandler(this.btnEntry_Click);
            // 
            // btnList
            // 
            this.btnList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnList.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnList.Image = global::FGCIJOROSystem.Presentation.Properties.Resources.icons8_list_100;
            this.btnList.ImageAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnList.Location = new System.Drawing.Point(85, 80);
            this.btnList.Margin = new System.Windows.Forms.Padding(2);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(240, 289);
            this.btnList.TabIndex = 0;
            this.btnList.Text = "JOB ORDER LIST";
            this.btnList.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // btnReleasal
            // 
            this.btnReleasal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReleasal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReleasal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnReleasal.Image = global::FGCIJOROSystem.Presentation.Properties.Resources.icons8_shipped_100;
            this.btnReleasal.ImageAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReleasal.Location = new System.Drawing.Point(595, 80);
            this.btnReleasal.Margin = new System.Windows.Forms.Padding(2);
            this.btnReleasal.Name = "btnReleasal";
            this.btnReleasal.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.btnReleasal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnReleasal.Size = new System.Drawing.Size(240, 289);
            this.btnReleasal.TabIndex = 2;
            this.btnReleasal.Text = "JOB ORDER EQUIPMENT RELEASAL LIST";
            this.btnReleasal.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnReleasal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReleasal.TextWrap = true;
            this.btnReleasal.Click += new System.EventHandler(this.btnReleasal_Click);
            // 
            // ucJOMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.radPanorama1);
            this.Controls.Add(this.radPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucJOMenu";
            this.Size = new System.Drawing.Size(930, 482);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanorama1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnEntry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReleasal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanorama radPanorama1;
        private Telerik.WinControls.UI.RadTileElement mJOEntry;
        private Telerik.WinControls.UI.RadTileElement mJOList;
        private Telerik.WinControls.UI.RadTileElement mJOAuditTrail;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadButton btnReleasal;
        private Telerik.WinControls.UI.RadButton btnEntry;
        private Telerik.WinControls.UI.RadButton btnList;
    }
}
