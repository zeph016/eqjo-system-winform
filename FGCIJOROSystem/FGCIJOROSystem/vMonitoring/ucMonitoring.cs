using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using FGCIJOROSystem.DAL.Repositories.vMonitoringRepo;
using FGCIJOROSystem.Presentation.vSystem.vMain;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Domain.Monitoring;

namespace FGCIJOROSystem.Presentation.vMonitoring
{
    public partial class ucMonitoring : UserControl
    {
        #region Properties
        public frmMainWindow MainWindowPage;
        MonitoringRepository MonitoringRepo;
        public List<clsMonitoring> ListOfMonitoring;
        #endregion

        #region Methods
        public ucMonitoring()
        {
            InitializeComponent();
        }
        private async void ucMonitoring_Load(object sender, EventArgs e)
        {
            pbLoading.Dock = DockStyle.Fill;
            pbLoading.Visible = true;
            radGridView1.AutoExpandGroups = true;
            MonitoringRepo = await Task.Run(() => new MonitoringRepository());
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, x) =>
            {
                ListOfMonitoring = MonitoringRepo.SearchBy("");
            };
            bg.RunWorkerCompleted += (s, x) =>
            {
                pbLoading.Visible = false;
                clsMonitoringBindingSource.DataSource = ListOfMonitoring;
            };
            bg.RunWorkerAsync();
        }

        private void radGridView1_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "WorkPercentage")
            {
                radGridView1.Grid_CellFormatting(sender, e);
            }
        }

       
        #endregion
        
    }
}
