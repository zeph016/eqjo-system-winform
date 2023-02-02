using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using FGCIJOROSystem.Common;

namespace FGCIJOROSystem.Presentation.vLoader
{
    public partial class FrmLoaderV2 : Telerik.WinControls.UI.RadForm
    {
        Timer progressTimer = new Timer();
        public FrmLoaderV2()
        {
            InitializeComponent();
            radProgressBar1.Maximum = 100;
            GlobalVariable.progressValue = 0;
        }
        private void FrmLoaderV2_Load(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void StartTimer()
        {
            progressTimer.Enabled = true;
            progressTimer.Start();
            progressTimer.Interval = 700;
            progressTimer.Tick += new EventHandler(TimerTick);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (radProgressBar1.Value1 != 100)
                radProgressBar1.Value1 = GlobalVariable.progressValue++;
            else
            {
                radProgressBar1.Value1 = 100;
                progressTimer.Stop();
            }
        }
    }
}
