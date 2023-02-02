using FGCIJOROSystem.DAL.Repositories.EquipmentURRepo;
using FGCIJOROSystem.DAL.Repositories.PPEStatusLogRepo;
using FGCIJOROSystem.Domain.PPEStatusLog;
using FGCIJOROSystem.Domain.Reports;
using FGCIJOROSystem.Reports.rPPEStatusLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using FGCIJOROSystem.DAL.Repositories.Reports;
using System.Threading.Tasks;
using System.Threading;

namespace FGCIJOROSystem.Presentation.vReport.vForms
{
    public partial class frmPPEStatus : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        PPEStatusLogRepository PPEStatusLogRepo;
        List<clsPPEStatusLog> ListOfPPEStatusLog;
        EquipmentURRepository EquipmentURRepo;
        JOROReportRepository JOROReportRepo;
        #endregion
        public frmPPEStatus()
        {
            InitializeComponent();
        }
        void Generate()
        {
            //if (chkDate.Checked == false)
            //{
            //    MessageBox.Show("Please check and select a date range", "No date range", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //else
            //{
                var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
                List<long> equipmentIdList = new List<long>();
                equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
                string equipmentIds = string.Join(",", equipmentIdList);

                PPEStatusLogRepo = new PPEStatusLogRepository();
                ListOfPPEStatusLog = new List<clsPPEStatusLog>();
            if (chkFromPPE.Checked)
            {
                ListOfPPEStatusLog = PPEStatusLogRepo.GetAllEquipment(chkEquipmentList.Checked, equipmentIds, chkEquipmentStatus.Checked, chkEquipmentStatus.Checked ? (long)cbEquipmentStatus.SelectedValue : 0, chkDate.Checked, dtFrom.Value.Date, dtTo.Value.Date);
            }
            else if (chkDate.Checked )
            {
                ListOfPPEStatusLog = PPEStatusLogRepo.GetAllFiltered(chkEquipmentList.Checked, equipmentIds, chkEquipmentStatus.Checked, chkEquipmentStatus.Checked ? (long)cbEquipmentStatus.SelectedValue : 0, chkDate.Checked, dtFrom.Value.Date, dtTo.Value.Date);
            }
               
                frmReportViewer ReportViewerPage = new frmReportViewer();
                string filter = GenerateFilterByInSection();
                var _tariffSummaryReport = new rptPPEStatusLog(ListOfPPEStatusLog, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            //}
            
        }
        private string GenerateFilterByInSection()
        {
            List<string> resultList = new List<string>();
            if (chkEquipmentStatus.Checked)
            {
                string EQ = ("Equipment Status: " + cbEquipmentStatus.SelectedItem);
                resultList.Add(EQ);
            }
            if (chkEquipmentList.Checked)
            {
                string EL = ("Selected Equipment");
                resultList.Add(EL);
            }
            if (chkDate.Checked)
            {
                string D = ("Date Range: " + dtFrom.Value.Date.ToString("MM/dd/yyyy") + " - " + dtTo.Value.Date.ToString("MM/dd/yyyy"));
                resultList.Add(D);
            }
            return string.Join(" / ", resultList);
        }
        private void btnGenerate_Click_1(object sender, EventArgs e)
        {
            Generate();
        }
        private void chkDate_CheckStateChanged(object sender, EventArgs e)
        {
            dtFrom.Enabled = chkDate.Checked;
            dtTo.Enabled = chkDate.Checked;
        }
        private void chkEquipmentStatus_CheckStateChanged(object sender, EventArgs e)
        {
            cbEquipmentStatus.Enabled = chkEquipmentStatus.Checked;
            //chkDate.Checked = chkEquipmentStatus.Checked;
            LoadPPEEquipmentStatus();
        }
        private void chkEquipmentList_CheckStateChanged(object sender, EventArgs e)
        {
            rgvEquipment.Enabled = chkEquipmentList.Checked;
        }

        private async void rgvEquipment_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = await Task.Run(() => (clsJOROReports)clsJOROReportsBindingSource.Current); //async
            current.CheckEquipment = !current.CheckEquipment;
            clsJOROReportsBindingSource.ResetCurrentItem();
        }
        async void LoadPPEEquipmentStatus()
        {
            EquipmentURRepo = new EquipmentURRepository();
            cbEquipmentStatus.ValueMember = "EquipmentStatusId";
            cbEquipmentStatus.DisplayMember = "EquipmentStatus";
            cbEquipmentStatus.DataSource = await Task.Run(() => EquipmentURRepo.GetAll()); //async

        }

        private void frmPPEStatus_Load(object sender, EventArgs e)
        {
            LoadEquipment();
            dtFrom.Value = System.DateTime.Now;
            dtTo.Value = System.DateTime.Now;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region LoadEquipment
        private void LoadEquipment()
        {
            JOROReportRepo = new JOROReportRepository();
            clsJOROReportsBindingSource.DataSource = JOROReportRepo.LoadAllEquipment();
        }
        #endregion

        private void ChkFromPPE_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkFromPPE.Checked == true)
            {
                dtFrom.Enabled = false;
                dtTo.Enabled = false;
                chkDate.Checked = false;
            }
        }

        private void ChkDate_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkDate.Checked == true)
            {
                dtFrom.Enabled = true;
                dtTo.Enabled = true;
                chkFromPPE.Checked = false;
            }
        }
    }
}
