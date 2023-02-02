using FGCIJOROSystem.DAL.Repositories.Reports;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Reports;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using FGCIJOROSystem.Reports.rEquipmentHistory;
namespace FGCIJOROSystem.Presentation.vReport.vForms
{
    public partial class frmEquipmentList : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        EquipmentHistoryRepository EquipmentHistoryRepo;
        public List<clsEquipmentHistoryReport> ListOfEquipment { get; set; }
        clsEquipmentHistoryReport EquipHistoryReport;
        #endregion
        public frmEquipmentList()
        {
            InitializeComponent();
        }

        private void btnGenerateReportPerSection_Click(object sender, EventArgs e)
        {
            //EquipHistoryReport = new clsEquipmentHistoryReport();
            var equipmentList = ((List<clsEquipmentHistoryReport>)clsEquipmentHistoryBindingSource.DataSource).Where(item => item.CheckEquipmentName).ToList();
            if (equipmentList.Count != 0)
            {
                List<long> equipmentIdList = new List<long>();
                equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
                string equipmentIds = string.Join(",", equipmentIdList);

                ListOfEquipment = new List<clsEquipmentHistoryReport>();
                ListOfEquipment = EquipmentHistoryRepo.GetEquipmentHistory(chkEquipment.Checked, equipmentIds, chkDate.Checked, dtStartDate.Value.Date, dtEndDate.Value.Date);//, chkDate.Checked, dtStartDate.Value.Date, dtEndDate.Value.Date
                frmReportViewer ReportViewerPage = new frmReportViewer();
                string filter = GenerateFilterByInSection();
                var _SummaryReport = new rptEquipmentHistory(ListOfEquipment, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_SummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
            else
            {
                frmMsg msgPage = new frmMsg() { MsgBox = Domain.Enums.clsEnums.MsgBox.Warning, Message = "Please select section(s) to work with." };
                msgPage.ShowDialog();
            }

            //frmMsg MsgBox = new frmMsg()
            //{
            //    MsgBox = clsEnums.MsgBox.Warning,
            //    Message = "Transaction is under maintenance."
            //};
            //MsgBox.ShowDialog();
        }
        private string GenerateFilterByInSection()
        {
            List<string> resultList = new List<string>();
            if (chkDate.Checked)
            {
                string date = ("Date Range: " + dtStartDate.Value.ToString("MMMM dd, yyyy") + " - " + dtEndDate.Value.ToString("MMMM dd, yyyy"));
                resultList.Add(date);
            }
            return string.Join(" / ", resultList);
        }
        void LoadEquipment()
        {
            EquipmentHistoryRepo = new EquipmentHistoryRepository();
            
            BackgroundWorker bg = new BackgroundWorker();
            List<clsEquipmentHistoryReport> ListOfEquipment = new List<clsEquipmentHistoryReport>();
            bg.DoWork += (s, e) => { ListOfEquipment = EquipmentHistoryRepo.GetAll(); };
            bg.RunWorkerCompleted += (s, e) => { clsEquipmentHistoryBindingSource.DataSource = ListOfEquipment; };
            bg.RunWorkerAsync();
        }

        private void frmEquipmentList_Load(object sender, EventArgs e)
        {
            LoadEquipment();
            dtStartDate.Value = DateTime.Now;
            dtEndDate.Value = DateTime.Now;
            this.ControlBox = false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkDate_CheckStateChanged(object sender, EventArgs e)
        {
            dtStartDate.Enabled = chkDate.Checked;
            dtEndDate.Enabled = chkDate.Checked;
        }

        private void rgvSection_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsEquipmentHistoryReport)clsEquipmentHistoryBindingSource.Current;
            current.CheckEquipmentName = !current.CheckEquipmentName;
            clsEquipmentHistoryBindingSource.ResetCurrentItem();
        }

        private void chkEquipment_CheckStateChanged(object sender, EventArgs e)
        {
            rgvSection.Enabled = chkEquipment.Checked;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void rgvSection_Click(object sender, EventArgs e)
        {

        }
    }
}
