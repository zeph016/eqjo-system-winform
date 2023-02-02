using FGCIJOROSystem.DAL.Repositories;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.vMonitoringRepo;
using FGCIJOROSystem.Domain.Configurations.SectionJORO;
using FGCIJOROSystem.Domain.Monitoring;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Reports.rReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using FGCIJOROSystem.Domain.Configurations.Personnels;
using FGCIJOROSystem.DAL.Repositories.EquipReleasalRepo;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Reports.rEquipmentReleasal;

namespace FGCIJOROSystem.Presentation.vReport.vForms
{
    public partial class frmMonitoringSelectOption : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        PersonnelRepository PersonnelRepo;
        SectionJORORepository SectionJORORepo;
        AttendanceGroupRepository AGRepo;
        public List<clsMonitoring> ListOfMonitoring { get; set; }
        public List<clsEquipment> ListOfEquipment { get; private set; }
        EquipmentReleasalRepository JOEquipReleasalRepo;
        EquipmentReleasalRepository ROEquipReleasalRepo;
        MonitoringRepository MonitoringRepo;
        #endregion
        #region Method
        public frmMonitoringSelectOption()
        {
            InitializeComponent();
            MonitoringRepo = new MonitoringRepository();
        }

        private void frmMonitoringSelectOption_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            LoadAttendanceGroup();

            dtFromSection.Value = DateTime.Now;
            dtToSection.Value = DateTime.Now;
        }
        void LoadSectionAndPersonnel()
        {
            PersonnelRepo = new PersonnelRepository();
            clsPersonnelsBindingSource.DataSource = PersonnelRepo.GetAllFiltered((Int64)cbSectionGroup.SelectedValue);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        void LoadFilteredSections()
        {
            SectionJORORepo = new SectionJORORepository();
            clsSectionJOROBindingSource.DataSource = SectionJORORepo.FilterSections((Int64)cbSectionGroup.SelectedValue);
        }

        private void cbSectionGroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            LoadFilteredSections();
            LoadSectionAndPersonnel();
        }
        void LoadAttendanceGroup()
        {
            AGRepo = new AttendanceGroupRepository();
            cbSectionGroup.DisplayMember = "AttendanceGroupName";
            cbSectionGroup.ValueMember = "Id";
            cbSectionGroup.DataSource = AGRepo.GetAllActive();
        }

        private void chkDate_CheckStateChanged(object sender, EventArgs e)
        {
            dtFromSection.Enabled = chkDate.Checked;
            dtToSection.Enabled = chkDate.Checked;
        }

        private void chkGroup_CheckStateChanged(object sender, EventArgs e)
        {
            cbSectionGroup.Enabled = chkGroup.Checked;
        }

        private void btnGenerateReportPerSection_Click(object sender, EventArgs e)
        {
            Generate();
        }
        void Generate()
        {
            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckSectionName).ToList();
            var mechanicList = ((List<clsPersonnels>)clsPersonnelsBindingSource.DataSource).Where(item => item.CheckPersonnel).ToList();
            if (chkGenerateAll.Checked)
            {
                if (sectionList.Count != 0)
                    {
                        List<long> sectionIdList = new List<long>();
                        sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
                        string sectionIds = string.Join(",", sectionIdList);

                        List<long> mechanicIdList = new List<long>();
                        mechanicList.ForEach(item => { mechanicIdList.Add(item.EmployeeId); });
                        string mechanicIds = string.Join(",", mechanicIdList);

                        ListOfMonitoring = new List<clsMonitoring>();
                        ListOfMonitoring = MonitoringRepo.GetAllRecord(chkSection.Checked, sectionIds, chkMechanic.Checked, mechanicIds, dtFromSection.Value.Date, dtToSection.Value.Date);
                        frmReportViewer ReportViewerPage = new frmReportViewer();
                        string filter = GenerateFilterByInSection();
                        var _SummaryReport = new rptMonitoringSummary(ListOfMonitoring, Program.CurrentUser.FullName, Program.CurrentUser.Position, filter);
                        ReportViewerPage = new frmReportViewer(_SummaryReport);
                        ReportViewerPage.reportViewer1.RefreshReport();
                        ReportViewerPage.Show();
                    }
                else if (mechanicList.Count != 0)
                    {
                        List<long> sectionIdList = new List<long>();
                        sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
                        string sectionIds = string.Join(",", sectionIdList);

                        List<long> mechanicIdList = new List<long>();
                        mechanicList.ForEach(item => { mechanicIdList.Add(item.EmployeeId); });
                        string mechanicIds = string.Join(",", mechanicIdList);

                        ListOfMonitoring = new List<clsMonitoring>();
                        ListOfMonitoring = MonitoringRepo.GetAllRecord(chkSection.Checked, sectionIds, chkMechanic.Checked, mechanicIds, dtFromSection.Value.Date, dtToSection.Value.Date);
                        frmReportViewer ReportViewerPage = new frmReportViewer();
                        string filter = GenerateFilterByInSection();
                        var _SummaryReport = new rptMonitoringSummary(ListOfMonitoring, Program.CurrentUser.FullName, Program.CurrentUser.Position, filter);
                        ReportViewerPage = new frmReportViewer(_SummaryReport);
                        ReportViewerPage.reportViewer1.RefreshReport();
                        ReportViewerPage.Show();
                    }
            }
            else if (chkGeneratePerReference.Checked)
            {

                if (sectionList.Count != 0)
                {
                    List<long> sectionIdList = new List<long>();
                    sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
                    string sectionIds = string.Join(",", sectionIdList);

                    List<long> mechanicIdList = new List<long>();
                    mechanicList.ForEach(item => { mechanicIdList.Add(item.EmployeeId); });
                    string mechanicIds = string.Join(",", mechanicIdList);

                    ListOfMonitoring = new List<clsMonitoring>();
                    ListOfMonitoring = MonitoringRepo.GetAllRecord(chkSection.Checked, sectionIds, chkMechanic.Checked, mechanicIds, dtFromSection.Value.Date, dtToSection.Value.Date);
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _SummaryReport = new rptMonitoringSummaryPerJORO(ListOfMonitoring, Program.CurrentUser.FullName, Program.CurrentUser.Position, filter);
                    ReportViewerPage = new frmReportViewer(_SummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
                else if (mechanicList.Count != 0)
                {
                    List<long> sectionIdList = new List<long>();
                    sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
                    string sectionIds = string.Join(",", sectionIdList);

                    List<long> mechanicIdList = new List<long>();
                    mechanicList.ForEach(item => { mechanicIdList.Add(item.EmployeeId); });
                    string mechanicIds = string.Join(",", mechanicIdList);

                    ListOfMonitoring = new List<clsMonitoring>();
                    ListOfMonitoring = MonitoringRepo.GetAllRecord(chkSection.Checked, sectionIds, chkMechanic.Checked, mechanicIds, dtFromSection.Value.Date, dtToSection.Value.Date);
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _SummaryReport = new rptMonitoringSummaryPerJORO(ListOfMonitoring, Program.CurrentUser.FullName, Program.CurrentUser.Position, filter);
                    ReportViewerPage = new frmReportViewer(_SummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
            else if (chkJOReleasalList.Checked)
            {
                JOEquipReleasalRepo = new EquipmentReleasalRepository();

                ListOfEquipment = new List<clsEquipment>();
                ListOfEquipment = JOEquipReleasalRepo.PrintSummary();
                frmReportViewer ReportViewerPage = new frmReportViewer();
                var _tariffSummaryReport = new rptReleasalSummaryReport(ListOfEquipment, Program.CurrentUser.FullName, Program.CurrentUser.Position, "EQUIPMENT FOR RELEASAL (JOB ORDER)");
                ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
            else if (chkROReleasalList.Checked)
            {
                ROEquipReleasalRepo = new EquipmentReleasalRepository();

                ListOfEquipment = new List<clsEquipment>();
                ListOfEquipment = ROEquipReleasalRepo.PrintSummaryRO();
                frmReportViewer ReportViewerPage = new frmReportViewer();
                var _tariffSummaryReport = new rptReleasalSummaryReport(ListOfEquipment, Program.CurrentUser.FullName, Program.CurrentUser.Position, "EQUIPMENT FOR RELEASAL (RESCUE ORDER)");
                ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
             else
                {
                    frmMsg msgPage = new frmMsg() { MsgBox = Domain.Enums.clsEnums.MsgBox.Warning, Message = "Please select Section/ Personnel to work with." };
                    msgPage.ShowDialog();
                }
        }
        private string GenerateFilterByInSection()
        {
            List<string> resultList = new List<string>();
            if (chkDate.Checked)
            {
                string date = ("Date Range: " + dtFromSection.Value.ToString("MMMM dd, yyyy") + " - " + dtToSection.Value.ToString("MMMM dd, yyyy"));
                resultList.Add(date);
            }
            if (chkGroup.Checked)
            {
                string Group = ("Group: " + cbSectionGroup.SelectedText);
                resultList.Add(Group);
            } 
            if (chkSection.Checked)
            {
                string Section = ("Section List");
                resultList.Add(Section);
            }
            if (chkMechanic.Checked)
            {
                string Mechanic = ("Mechanic List");
                resultList.Add(Mechanic);
            }
            return string.Join(" / ", resultList);
        }
        #endregion

        private void chkSection_CheckStateChanged(object sender, EventArgs e)
        {
            rgvSection.Enabled = chkSection.Checked;
            SectionList.SelectedPage = pgSection;
        }

        private void chkMechanic_CheckStateChanged(object sender, EventArgs e)
        {
            rgvMechanics.Enabled = chkMechanic.Checked;
            SectionList.SelectedPage = pgMechanic;
        }

        private void rgvSection_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsSectionJORO)clsSectionJOROBindingSource.Current;
            current.CheckSectionName = !current.CheckSectionName;
            clsSectionJOROBindingSource.ResetCurrentItem();
        }

        private void rgvMechanics_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsPersonnels)clsPersonnelsBindingSource.Current;
            current.CheckPersonnel = !current.CheckPersonnel;
            clsPersonnelsBindingSource.ResetCurrentItem();
        }

        private void radGroupBox7_Click(object sender, EventArgs e)
        {

        }

        private void chkGeneratePerReference_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

        }
    }
}
