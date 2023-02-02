using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation.vLoader
{
    public partial class FrmLoader : Telerik.WinControls.UI.RadForm
    {
        public FrmLoader()
        {
            InitializeComponent();
            pictureBox1.BackColor = Color.Transparent;
        }

        private void FrmLoader_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
