using FGCIJOROSystem.DAL.Repositories;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Personnels;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Presentation.vReport.vForms;
using FGCIJOROSystem.Reports.rAuditTrail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;

namespace FGCIJOROSystem.Presentation.vConfiguration.AuditTrail
{
    public partial class frmAuditTrailSelectOption : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        PersonnelRepository PersonnelRepo;
        clsPersonnels Personnels;
        public List<clsUsersLog> ListOfUsersLog { get; set; }
        clsUsersLog currUser;
        UsersLogRepository UsersLogRepo;
        #endregion
        public frmAuditTrailSelectOption()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            ListOfUsersLog = new List<clsUsersLog>();
            UsersLogRepo = new UsersLogRepository();
        }

        private void frmAuditTrailSelectOption_Load(object sender, EventArgs e)
        {
            cbPrintOption.Text = "";
            LoadPersonnels();
            LoadDropdownList();
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
            this.ControlBox = false;
        }
        void LoadPersonnels()
        {
            PersonnelRepo = new PersonnelRepository();

            BackgroundWorker bg = new BackgroundWorker();
            List<clsPersonnels> ListOfPersonnel = new List<clsPersonnels>();
            bg.DoWork += (s, e) =>
            {
                ListOfPersonnel = PersonnelRepo.GetAllPersonnels();
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsPersonnelsBindingSource.DataSource = ListOfPersonnel;
            };
            bg.RunWorkerAsync();
        }

        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsPersonnels)clsPersonnelsBindingSource.Current;
            current.CheckPersonnel = !current.CheckPersonnel;
            clsPersonnelsBindingSource.ResetCurrentItem();
        }
        void LoadDropdownList()
        {
            cbOption.DisplayMember = "Text";
            cbOption.ValueMember = "Value";

            var items = new[] { 
            new { Text = "Added JO Only" ,          Value = "A" }, 
            new { Text = "Update JO Only",          Value = "B" }, 
            new { Text = "All Added and Update JO", Value = "C" },
            new { Text = "Added RO Only" ,          Value = "D" }, 
            new { Text = "Update RO Only",          Value = "E" }, 
            new { Text = "All Added and Update RO", Value = "F" },
            new { Text = "Update Actual Advance",   Value = "G" },
            new { Text = "Summary",                 Value = "H" },

        };
            cbOption.DataSource = items;
        }
        void LoadPrintOption()
        {
            cbPrintOption.DisplayMember = "Text";
            cbPrintOption.ValueMember = "Value";

            var items = new[] { 
            new { Text = "Generate By Details" ,          Value = "A" }, 
            new { Text = "Generate Each Details Summary",          Value = "B" },
            new { Text = "Generate Summary",          Value = "C" },

            };
            cbPrintOption.DataSource = items;
        }

        private void chkPersonnel_CheckStateChanged(object sender, EventArgs e)
        {
            radGridView1.Enabled = chkPersonnel.Checked;
            chkOptionBox.Checked = chkPersonnel.Checked;
        }
        void GenerateByDetails()
        {
            ListOfUsersLog = new List<clsUsersLog>();
            var personnelList = ((List<clsPersonnels>)clsPersonnelsBindingSource.DataSource).Where(item => item.CheckPersonnel).ToList();
            if (personnelList.Count != 0)
            {
                List<long> personnelIdList = new List<long>();
                personnelList.ForEach(item => { personnelIdList.Add(item.EmployeeId); });
                string personnelIds = string.Join(",", personnelIdList);

                if (cbOption.Text == "Added JO Only")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered3(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "Update JO Only")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered4(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "Added RO Only")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered5(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "Update RO Only")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered6(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "Update Actual Advance")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered8(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "All Added and Update JO")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered34(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "All Added and Update RO")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered56(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "Update Actual Advance")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered8(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                frmReportViewer ReportViewerPage = new frmReportViewer();
                string filter = GenerateFilterByInSection();
                var _SummaryReport = new rptAuditTrail(ListOfUsersLog, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_SummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
        }
        void GenerateByDetailSummary()
        {
            ListOfUsersLog = new List<clsUsersLog>();
            var personnelList = ((List<clsPersonnels>)clsPersonnelsBindingSource.DataSource).Where(item => item.CheckPersonnel).ToList();
            if (personnelList.Count != 0)
            {
                List<long> personnelIdList = new List<long>();
                personnelList.ForEach(item => { personnelIdList.Add(item.EmployeeId); });
                string personnelIds = string.Join(",", personnelIdList);

                if (cbOption.Text == "Added JO Only")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered3(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "Update JO Only")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered4(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "Added RO Only")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered5(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "Update RO Only")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered6(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "Update Actual Advance")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered8(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "All Added and Update JO")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered34(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "All Added and Update RO")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered56(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                else if (cbOption.Text == "Update Actual Advance")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered8(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                frmReportViewer ReportViewerPage = new frmReportViewer();
                string filter = GenerateFilterByInSection();
                var _SummaryReport = new rptAuditTrailRecordCountSum(ListOfUsersLog, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_SummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
        }
        void GenerateSummary()
        {
            ListOfUsersLog = new List<clsUsersLog>();
            var personnelList = ((List<clsPersonnels>)clsPersonnelsBindingSource.DataSource).Where(item => item.CheckPersonnel).ToList();
            if (personnelList.Count != 0)
            {
                List<long> personnelIdList = new List<long>();
                personnelList.ForEach(item => { personnelIdList.Add(item.EmployeeId); });
                string personnelIds = string.Join(",", personnelIdList);

                if (cbOption.Text == "Summary")
                {
                    ListOfUsersLog = UsersLogRepo.GetAllAuditTrailFiltered3456(dtStart.Value.Date, dtEnd.Value.Date, chkPersonnel.Checked, personnelIds);
                }
                frmReportViewer ReportViewerPage = new frmReportViewer();
                string filter = GenerateFilterByInSection();
                var _SummaryReport = new rptAuditTrailSummary(ListOfUsersLog, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_SummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
        }
        private string GenerateFilterByInSection()
        {
            List<string> resultList = new List<string>();
            if (chkDate.Checked)
            {
                string date = ("Date Range: " + dtStart.Value.ToString("MMMM dd, yyyy") + " - " + dtEnd.Value.ToString("MMMM dd, yyyy"));
                resultList.Add(date);
            } 
            if (chkOptionBox.Checked)
            {
                string Category = cbOption.SelectedItem.ToString();
                resultList.Add(Category);
            }
                
            return string.Join(" , ", resultList);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void chkOptionBox_CheckStateChanged(object sender, EventArgs e)
        {
            cbOption.Enabled = chkOptionBox.Checked;
            chkPersonnel.Checked = chkOptionBox.Checked;
        }

        private void chkDate_CheckStateChanged(object sender, EventArgs e)
        {
            dtStart.Enabled = chkDate.Checked;
            dtEnd.Enabled = chkDate.Checked;
        }
        private void btnShowPersonnelOnly_Click(object sender, EventArgs e)
        {
            if (cbPrintOption.Text == "Generate By Details")
            {
                GenerateByDetails();
            }
            else if (cbPrintOption.Text == "Generate Each Details Summary")
            {
                GenerateByDetailSummary();
            }
            else if (cbPrintOption.Text =="Generate Summary")
            {
                GenerateSummary();
            }
            else
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Please select print option to work with."
                };
                MsgBox.ShowDialog();
            }
        }

        private void cbOption_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cbOption.Text == "Summary")
            {
                cbPrintOption.Text = "Generate Summary";
            }
            else
            {
                cbPrintOption.Text = "";
            }
        }

        private void cbPrintOption_Click(object sender, EventArgs e)
        {
            LoadPrintOption();
        }

        private void MasterTemplate_Click(object sender, EventArgs e)
        {

        }

        private void cbPrintOption_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cbPrintOption.Text == "Generate Summary")
            {
                cbOption.Text = "Summary";
            }
        }
    }
}
