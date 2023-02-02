namespace FGCIJOROSystem.Presentation.vDashboard
{
    partial class ucNewDashboard
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
            this.dashboardBarAndDockingController1 = new DevExpress.DashboardWin.Native.DashboardBarAndDockingController(this.components);
            this.dashboardBarController1 = new DevExpress.DashboardWin.Bars.DashboardBarController(this.components);
            this.textBoxEditorBarController1 = new DevExpress.DashboardWin.Bars.TextBoxEditorBarController(this.components);
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.devDashboard = new DevExpress.DashboardWin.DashboardViewer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardBarAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardBarController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxEditorBarController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.devDashboard)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxEditorBarController1
            // 
            this.textBoxEditorBarController1.Designer = null;
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.label1);
            this.radPanel1.Controls.Add(this.pictureBox1);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(879, 32);
            this.radPanel1.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FGCIJOROSystem.Presentation.Properties.Resources.icons8_speed_48;
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
            this.radLabel1.Size = new System.Drawing.Size(95, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "DASHBOARD";
            // 
            // devDashboard
            // 
            this.devDashboard.AllowPrintDashboardItems = true;
            this.devDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devDashboard.Location = new System.Drawing.Point(0, 32);
            this.devDashboard.Name = "devDashboard";
            this.devDashboard.PdfExportOptions.ChartSizeMode = DevExpress.DashboardCommon.ChartExportSizeMode.Stretch;
            this.devDashboard.PrintPreviewOptions.ChartSizeMode = DevExpress.DashboardCommon.ChartExportSizeMode.Stretch;
            this.devDashboard.Size = new System.Drawing.Size(879, 433);
            this.devDashboard.TabIndex = 9;
            this.devDashboard.Load += new System.EventHandler(this.dashboardViewer1_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(368, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // ucNewDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.devDashboard);
            this.Controls.Add(this.radPanel1);
            this.Name = "ucNewDashboard";
            this.Size = new System.Drawing.Size(879, 465);
            ((System.ComponentModel.ISupportInitialize)(this.dashboardBarAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardBarController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxEditorBarController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.devDashboard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.DashboardWin.Native.DashboardBarAndDockingController dashboardBarAndDockingController1;
        private DevExpress.DashboardWin.Bars.DashboardBarController dashboardBarController1;
        private DevExpress.DashboardWin.Bars.TextBoxEditorBarController textBoxEditorBarController1;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private DevExpress.DashboardWin.DashboardViewer devDashboard;
        private System.Windows.Forms.Label label1;
    }
}
