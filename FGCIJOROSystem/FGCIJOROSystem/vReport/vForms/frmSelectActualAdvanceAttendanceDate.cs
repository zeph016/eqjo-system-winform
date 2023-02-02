using FGCIJOROSystem.DAL.Repositories.ActualAdvanceRepo;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.ActualAdvance;
using FGCIJOROSystem.Domain.Configurations.SectionJORO;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Reports;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Reports.rAttendance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using FGCIJOROSystem.Domain.Configurations.Branches;
using FGCIJOROSystem.Domain.Configurations.Personnels;
using FGCIJOROSystem.DAL.Repositories;
using FGCIJOROSystem.DAL.Repositories.RORepo;
using FGCIJOROSystem.Domain.RescueOrder;
namespace FGCIJOROSystem.Presentation.vReport.vForms
{
    public partial class frmSelectActualAdvanceAttendanceDate : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        
        List<clsAttendance> ListOfAttendance;
        List<clsRescueOrder> ListOfDriversAttendance;
        BranchRepository BranchRepo;
        SectionJORORepository SectionJORORepo;
        AttendanceGroupRepository AGRepo;
        PersonnelRepository PersonnelRepo;
        public clsJOROReports curJOROReports { get; set; }
        public clsBranch curBranch { get; set; }
        #endregion
        public frmSelectActualAdvanceAttendanceDate()
        {
            InitializeComponent();
            this.AcceptButton = btnGenerateReportPerSection;
        }

        private void btnGenerateReportPerSection_Click(object sender, EventArgs e)
        {
           GeneratebySection();
        }
        void GeneratebySection()
        {
            curJOROReports = new clsJOROReports();
            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckSectionName).ToList();
            List<long> sectionIdList = new List<long>();
            sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
            string sectionIds = string.Join(",", sectionIdList);

            var personnelList = ((List<clsPersonnels>)clsPersonnelsBindingSource.DataSource).Where(item => item.CheckPersonnel).ToList();
            List<long> personnelIdList = new List<long>();
            personnelList.ForEach(item => { personnelIdList.Add(item.EmployeeId); });
            string personnelIds = string.Join(",", personnelIdList);

            var dateUpdate = "AS OF " + DateTime.Now.ToString("MMMM/dd/yyyy");

            if (chkDate.Checked)
            {
                dateUpdate = "AS OF " + dtActualDateFrom.Value.ToString(" MMMM dd, yyyy ") + "-" + dtActualDateTo.Value.ToString(" MMMM dd, yyyy ");
            }

            //if (rdbDailyAttendanceAccomplishment.IsChecked)
            //{
            //    AttendanceRepository ActivityReportRepo = new AttendanceRepository();
            //    ListOfAttendance = ActivityReportRepo.GetAllFiltered(dtActualDateFrom.Value.Date, chkSections.Checked, sectionIds, chkBranch.Checked, (Int64)cbBranch.SelectedValue, chkAG.Checked, (Int64)cbAG.SelectedValue, chkPersonnel.Checked, personnelIds);
            //    rptDailyAttendance report = new rptDailyAttendance(ListOfAttendance, Program.CurrentUser.FullName, dtActualDateFrom.Value.Date, Program.CurrentUser.Position);
            //    var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            //    frmReportViewer ReportViewerPage = new frmReportViewer();
            //    ReportViewerPage.reportViewer1.ReportSource = reportSource;
            //    ReportViewerPage.reportViewer1.RefreshReport();
            //    ReportViewerPage.ShowDialog();
            //}
            ////else if (chkDate.Checked && chkDateCalculation.Checked == true && chkDateStatus.Checked == false) //dhaniele
            ////{
            ////    AttendanceRepository ActivityReportRepo = new AttendanceRepository();
            ////    ListOfAttendance = ActivityReportRepo.GetSummaryWithSundays(chkSections.Checked, sectionIds, chkBranch.Checked, (Int64)cbBranch.SelectedValue, chkAG.Checked, (Int64)cbAG.SelectedValue, dtActualDateFrom.Value.Date, dtActualDateTo.Value.Date, chkPersonnel.Checked, personnelIds);//
            ////    string filter = GenerateFilterByInSection();
            ////    rptCutOffAttendanceCalc report = new rptCutOffAttendanceCalc(ListOfAttendance, Program.CurrentUser.FullName, dtActualDateFrom.Value.Date, Program.CurrentUser.Position, dtActualDateTo.Value.Date, filter);
            ////    var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            ////    frmReportViewer ReportViewerPage = new frmReportViewer();
            ////    ReportViewerPage.reportViewer1.ReportSource = reportSource;
            ////    ReportViewerPage.reportViewer1.RefreshReport();
            ////    ReportViewerPage.ShowDialog();
            ////}
            //else if (chkDate.Checked && chkDateStatus.Checked == true) //&& chkDateCalculation.Checked == false) //dhaniele
            //{
            //    AttendanceRepository ActivityReportRepo = new AttendanceRepository();
            //    ListOfAttendance = ActivityReportRepo.GetSummaryWithSundays(chkSections.Checked, sectionIds, chkBranch.Checked, (Int64)cbBranch.SelectedValue, chkAG.Checked, (Int64)cbAG.SelectedValue, dtActualDateFrom.Value.Date, dtActualDateTo.Value.Date, chkPersonnel.Checked, personnelIds);//
            //    string filter = GenerateFilterByInSection();
            //    rptCutOffAttendanceStatus report = new rptCutOffAttendanceStatus(ListOfAttendance, Program.CurrentUser.FullName, dtActualDateFrom.Value.Date, Program.CurrentUser.Position, dtActualDateTo.Value.Date, filter);
            //    var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            //    frmReportViewer ReportViewerPage = new frmReportViewer();
            //    ReportViewerPage.reportViewer1.ReportSource = reportSource;
            //    ReportViewerPage.reportViewer1.RefreshReport();
            //    ReportViewerPage.ShowDialog();
            //}
            //else if (chkDate.Checked == true && rdbCheckURperSection.IsChecked == true)
            //{
            //    AttendanceRepository ActivityReportRepo = new AttendanceRepository();
            //    ListOfAttendance = ActivityReportRepo.GetAllAccomplishmentURPerSection(chkSections.Checked, sectionIds, chkBranch.Checked, (Int64)cbBranch.SelectedValue, chkAG.Checked, (long)cbAG.SelectedValue, chkPersonnel.Checked, personnelIds, chkDate.Checked, dtActualDateFrom.Value.Date, dtActualDateTo.Value.Date);
            //    rptAccomplishmentReport report = new rptAccomplishmentReport(ListOfAttendance, Program.CurrentUser.FullName, dtActualDateFrom.Value.Date, Program.CurrentUser.Position, dateUpdate);
            //    var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            //    frmReportViewer ReportViewerPage = new frmReportViewer();
            //    ReportViewerPage.reportViewer1.ReportSource = reportSource;
            //    ReportViewerPage.reportViewer1.RefreshReport();
            //    ReportViewerPage.ShowDialog();
            //}
            //else if (chkDate.Checked)//&& chkDateStatus.Checked == false) // dhaniele
            //{
            //    AttendanceRepository ActivityReportRepo = new AttendanceRepository();
            //    ListOfAttendance = ActivityReportRepo.GetSummaryWithSundays(chkSections.Checked, sectionIds, chkBranch.Checked, (Int64)cbBranch.SelectedValue, chkAG.Checked, (Int64)cbAG.SelectedValue, dtActualDateFrom.Value.Date, dtActualDateTo.Value.Date, chkPersonnel.Checked, personnelIds);//
            //    string filter = GenerateFilterByInSection();
            //    rptCutOffAttendance report = new rptCutOffAttendance(ListOfAttendance, Program.CurrentUser.FullName, dtActualDateFrom.Value.Date, Program.CurrentUser.Position, dtActualDateTo.Value.Date, filter);
            //    var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            //    frmReportViewer ReportViewerPage = new frmReportViewer();
            //    ReportViewerPage.reportViewer1.ReportSource = reportSource;
            //    ReportViewerPage.reportViewer1.RefreshReport();
            //    ReportViewerPage.ShowDialog();
            //}
            //else if (chkIncludeSunday.Checked)
            //{
            //    AttendanceRepository ActivityReportRepo = new AttendanceRepository();
            //    ListOfAttendance = ActivityReportRepo.GetSummaryWithoutSunday(chkSections.Checked, sectionIds, chkBranch.Checked, (Int64)cbBranch.SelectedValue, chkAG.Checked, (Int64)cbAG.SelectedValue, dtActualDateFrom.Value.Date, dtActualDateTo.Value.Date, chkPersonnel.Checked, personnelIds);//
            //    string filter = GenerateFilterByInSection();
            //    rptCutOffAttendance report = new rptCutOffAttendance(ListOfAttendance, Program.CurrentUser.FullName, dtActualDateFrom.Value.Date, Program.CurrentUser.Position, dtActualDateTo.Value.Date, filter);
            //    var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            //    frmReportViewer ReportViewerPage = new frmReportViewer();
            //    ReportViewerPage.reportViewer1.ReportSource = reportSource;
            //    ReportViewerPage.reportViewer1.RefreshReport();
            //    ReportViewerPage.ShowDialog();
            //}
           
            //else if (rdbDriverAttendance.IsChecked)
            //{
            //    RORepository RORepo = new RORepository();
            //    ListOfDriversAttendance = RORepo.GetAllDriversAttendance(dtActualDateFrom.Value.Date, dtActualDateTo.Value.Date);
            //    string filter = GenerateFilterByInSection();
            //    rptDriversAttendance report = new rptDriversAttendance(ListOfDriversAttendance,filter, Program.CurrentUser.FullName,Program.CurrentUser.Position, dtActualDateFrom.Value, dtActualDateTo.Value);
            //    var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            //    frmReportViewer ReportViewerPage = new frmReportViewer();
            //    ReportViewerPage.reportViewer1.ReportSource = reportSource;
            //    ReportViewerPage.reportViewer1.RefreshReport();
            //    ReportViewerPage.ShowDialog();
            //}
            //else
            //{
            //    frmMsg MsgBox = new frmMsg()
            //    {
            //        MsgBox = clsEnums.MsgBox.Warning,
            //        Message = "Please select filter to work with."
            //    };
            //    MsgBox.ShowDialog();
            //}
            //-----------------------------------------------------

            frmReportViewer ReportViewerPage = new frmReportViewer();
            if (rdbDailyAttendanceAccomplishment.IsChecked)
            {
                AttendanceRepository ActivityReportRepo = new AttendanceRepository();
                ListOfAttendance = ActivityReportRepo.GetAllFiltered(dtActualDateFrom.Value.Date, chkSections.Checked, sectionIds, chkBranch.Checked, (Int64)cbBranch.SelectedValue, chkAG.Checked, (Int64)cbAG.SelectedValue, chkPersonnel.Checked, personnelIds);
                rptDailyAttendance report = new rptDailyAttendance(ListOfAttendance, Program.CurrentUser.FullName, dtActualDateFrom.Value.Date, Program.CurrentUser.Position);
                var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
                ReportViewerPage.reportViewer1.ReportSource = reportSource;
            }
            if (rdbCheckURperSection.IsChecked)
            {
                AttendanceRepository ActivityReportRepo = new AttendanceRepository();
                ListOfAttendance = ActivityReportRepo.GetAllAccomplishmentURPerSection(chkSections.Checked, sectionIds, chkBranch.Checked, (Int64)cbBranch.SelectedValue, chkAG.Checked, (long)cbAG.SelectedValue, chkPersonnel.Checked, personnelIds, chkDate.Checked, dtActualDateFrom.Value.Date, dtActualDateTo.Value.Date);
                rptAccomplishmentReport report = new rptAccomplishmentReport(ListOfAttendance, Program.CurrentUser.FullName, dtActualDateFrom.Value.Date, Program.CurrentUser.Position, dateUpdate);
                var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
                ReportViewerPage.reportViewer1.ReportSource = reportSource;
            }
            if (rdbDriverAttendance.IsChecked)
            {
                RORepository RORepo = new RORepository();
                ListOfDriversAttendance = RORepo.GetAllDriversAttendance(dtActualDateFrom.Value.Date, dtActualDateTo.Value.Date);
                string filter = GenerateFilterByInSection();
                rptDriversAttendance report = new rptDriversAttendance(ListOfDriversAttendance, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position, dtActualDateFrom.Value, dtActualDateTo.Value);
                var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
                ReportViewerPage.reportViewer1.ReportSource = reportSource;
            }
             if (chkDateStatus.Checked)
            {
                AttendanceRepository ActivityReportRepo = new AttendanceRepository();
                ListOfAttendance = ActivityReportRepo.GetSummaryWithSundays(chkSections.Checked, sectionIds, chkBranch.Checked, (Int64)cbBranch.SelectedValue, chkAG.Checked, (Int64)cbAG.SelectedValue, dtActualDateFrom.Value.Date, dtActualDateTo.Value.Date, chkPersonnel.Checked, personnelIds);//
                string filter = GenerateFilterByInSection();
                rptCutOffAttendanceStatus report = new rptCutOffAttendanceStatus(ListOfAttendance, Program.CurrentUser.FullName, dtActualDateFrom.Value.Date, Program.CurrentUser.Position, dtActualDateTo.Value.Date, filter);
                var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
                ReportViewerPage.reportViewer1.ReportSource = reportSource;
            }
            if (chkDate.Checked && chkDateStatus.Checked == false && chkDateCalculation.Checked == true)
            {
                AttendanceRepository ActivityReportRepo = new AttendanceRepository();
                ListOfAttendance = ActivityReportRepo.GetSummaryWithSundays(chkSections.Checked, sectionIds, chkBranch.Checked, (Int64)cbBranch.SelectedValue, chkAG.Checked, (Int64)cbAG.SelectedValue, dtActualDateFrom.Value.Date, dtActualDateTo.Value.Date, chkPersonnel.Checked, personnelIds);//
                string filter = GenerateFilterByInSection();
                rptCutOffAttendanceCalc report = new rptCutOffAttendanceCalc(ListOfAttendance, Program.CurrentUser.FullName, dtActualDateFrom.Value.Date, Program.CurrentUser.Position, dtActualDateTo.Value.Date, filter);
                var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };               
                ReportViewerPage.reportViewer1.ReportSource = reportSource;               
            }

            if (chkDate.Checked && chkDateStatus.Checked == false && chkDateCalculation.Checked == false)
            {
                AttendanceRepository ActivityReportRepo = new AttendanceRepository();
                ListOfAttendance = ActivityReportRepo.GetSummaryWithSundays(chkSections.Checked, sectionIds, chkBranch.Checked, (Int64)cbBranch.SelectedValue, chkAG.Checked, (Int64)cbAG.SelectedValue, dtActualDateFrom.Value.Date, dtActualDateTo.Value.Date, chkPersonnel.Checked, personnelIds);//
                string filter = GenerateFilterByInSection();
                rptCutOffAttendance report = new rptCutOffAttendance(ListOfAttendance, Program.CurrentUser.FullName, dtActualDateFrom.Value.Date, Program.CurrentUser.Position, dtActualDateTo.Value.Date, filter);
                var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
                ReportViewerPage.reportViewer1.ReportSource = reportSource;
            }

            ReportViewerPage.reportViewer1.RefreshReport();
            ReportViewerPage.ShowDialog();

        }
        private void frmSelectActualAdvanceAttendanceDate_Load(object sender, EventArgs e)
        {
            dtActualDateFrom.Value = DateTime.Now;
            dtActualDateTo.Value = DateTime.Now;

            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnGenerateReportPerSection, "Preview");

            LoadBranch();
            LoadAttendanceGroup();
            LoadPersonnels();
        }
        void LoadBranch()
        {
            BranchRepo = new BranchRepository();
            cbBranch.DisplayMember = "BranchName";
            cbBranch.ValueMember = "Id";
            cbBranch.DataSource = BranchRepo.GetAllActive();
        }
        void LoadSections()
        {
            SectionJORORepo = new SectionJORORepository();
            clsSectionJOROBindingSource.DataSource = SectionJORORepo.GetAllByGroup((Int64)cbAG.SelectedValue);
        }
        void LoadPersonnels()
        {
            PersonnelRepo = new PersonnelRepository();
            clsPersonnelsBindingSource.DataSource = PersonnelRepo.GetAllPersonnels();
        }
        void LoadSectionsNoFilter()
        {
            SectionJORORepo = new SectionJORORepository();
            clsSectionJOROBindingSource.DataSource = SectionJORORepo.GetAll();
        }
        void LoadAttendanceGroup()
        {
            AGRepo = new AttendanceGroupRepository();
            cbAG.DisplayMember = "AttendanceGroupName";
            cbAG.ValueMember = "Id";
            cbAG.DataSource = AGRepo.GetAllActive();
        }
        private void chkBranch_CheckStateChanged(object sender, EventArgs e)
        {
            cbBranch.Enabled = chkBranch.Checked;
            dtActualDateFrom.Enabled = chkBranch.Checked;
        }
        void selectSection()
        {
            var current = (clsSectionJORO)clsSectionJOROBindingSource.Current;
            current.CheckSectionName = !current.CheckSectionName;
            clsSectionJOROBindingSource.ResetCurrentItem();
        }
        private void chkSections_CheckStateChanged(object sender, EventArgs e)
        {
            rgvSection.Enabled = chkSections.Checked;
            pvList.SelectedPage = pgSection;
            //dtActualDateFrom.Enabled = chkSections.Checked;
        }

        private void chkAG_CheckStateChanged(object sender, EventArgs e)
        {
            cbAG.Enabled = chkAG.Checked;
            chkSections.Checked = chkAG.Checked;
        }

        private void cbAG_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            LoadSections();
            
        }
        private void chkAllGroup_CheckStateChanged(object sender, EventArgs e)
        {
            LoadSectionsNoFilter();
            chkSections.Checked = chkAllGroup.Checked;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkDate_CheckStateChanged(object sender, EventArgs e)
        {
            dtActualDateFrom.Enabled = chkDate.Checked;
            dtActualDateTo.Enabled = chkDate.Checked;

            chkDateStatus.Enabled = true;
            chkDateCalculation.Enabled = true;

            //rdbDailyAttendanceAccomplishment.IsChecked = true;
            chkIncludeSunday.Checked = false;

            if(chkDate.Checked == false)
            {
                chkDateStatus.Checked = false;
                chkDateStatus.Enabled = false;
                chkDateCalculation.Checked = false;
                chkDateCalculation.Enabled = false;
            }

        }
        private string GenerateFilterByInSection()
        {
            List<string> resultList = new List<string>();
            if (chkBranch.Checked)
            {
                string B = ("Branch: " + cbBranch.SelectedItem);
                resultList.Add(B);
            }
            if(chkAG.Checked)
            {
                string AG = ("Attendance Group: " + cbAG.SelectedItem);
                resultList.Add(AG);
            }
            if (chkAllGroup.Checked)
            {
                string All = ("All Group");
                resultList.Add(All);
            }
            if (chkSections.Checked)
            {
                string S = ("Section List");
                resultList.Add(S);
            }
            if (chkPersonnel.Checked)
            {
                string P = ("Selected Personnel");
                resultList.Add(P);
            }
            if (rdbDriverAttendance.IsChecked)
            {
                string DA = ("Service Driver's Attendance");
                resultList.Add(DA);
            }
            return string.Join(" / ", resultList);
        }

        private void rgvSection_CellClick_1(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            selectSection();
        }

        private void rgvMechanics_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsPersonnels)clsPersonnelsBindingSource.Current;
            current.CheckPersonnel = !current.CheckPersonnel;
            clsPersonnelsBindingSource.ResetCurrentItem();
        }

        private void chkPersonnel_CheckStateChanged(object sender, EventArgs e)
        {
            pvList.SelectedPage = pgPersonnel;
            rgvMechanics.Enabled = chkPersonnel.Checked;
        }

        private void chkDailyAccomplishment_CheckStateChanged(object sender, EventArgs e)
        {
         

            chkDate.Checked = false;

            chkDateStatus.Checked = false;
            chkDateStatus.Enabled = false;

            //chkDateCalculation.Checked = false;
            //chkDateCalculation.Enabled = false;
        }

        private void chkIncludeSunday_CheckStateChanged(object sender, EventArgs e)
        {
            dtActualDateFrom.Enabled = chkIncludeSunday.Checked;
            dtActualDateTo.Enabled = chkIncludeSunday.Checked;
            //rdbDailyAttendanceAccomplishment.IsChecked;
            chkDate.Checked = false;
        }

     
        private void ChkURperSection_CheckStateChanged(object sender, EventArgs e)
        {
            chkDateStatus.Checked = false;
            chkDateStatus.Enabled = false;
           // chkDateCalculation.Checked = false;
            //chkDateCalculation.Enabled = false;
        }

        private void ChkDateCalculation_CheckStateChanged(object sender, EventArgs e)
        {

            if (chkDateCalculation.Checked)
            {
                chkDateStatus.Checked = false;
                chkDateStatus.Enabled = false;
            }
            else
            {
                chkDateStatus.Enabled = true;
            }

            //chkDateStatus.Checked = false;
            //chkDateStatus.Enabled = false;

            //if (chkDateCalculation.Checked == false)
            //{
            //    chkDateStatus.Enabled = true;
            //}
        }

        private void ChkDateStatus_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkDateStatus.Checked == true)
            {
                chkDateCalculation.Checked = false;
                chkDateCalculation.Enabled = false;
            }
            else if (chkDateStatus.Checked == false)
            {
                //chkDateCalculation.Checked = true;
                chkDateCalculation.Enabled = true;
            }

            //if (chkDateStatus.Checked == false)
            //{
            //    chkDateCalculation.Enabled = true;
            //}
        }

        private void RgvSection_Click(object sender, EventArgs e)
        {

        }

        private void RdbDailyAttendanceAccomplishment_CheckStateChanged(object sender, EventArgs e)
        {
            dtActualDateFrom.Enabled = rdbDailyAttendanceAccomplishment.IsChecked;

            chkDate.Checked = false;

            chkDateStatus.Checked = false;
            chkDateStatus.Enabled = false;
        }

        private void RadRadioButton1_CheckStateChanged(object sender, EventArgs e)
        {
            //chkDateStatus.Checked = false;
            //chkDateStatus.Enabled = false;
        }

        private void RdbDriverAttendance_CheckStateChanged(object sender, EventArgs e)
        {
            dtActualDateFrom.Enabled = rdbDriverAttendance.IsChecked;
            dtActualDateTo.Enabled = rdbDriverAttendance.IsChecked;

            chkDateStatus.Checked = false;
            chkDateStatus.Enabled = false;

            if (rdbDriverAttendance.IsChecked)
            {
                chkDateStatus.Checked = false;
                chkDateCalculation.Checked = false;

                chkDateStatus.Enabled = false;
                chkDateCalculation.Enabled = false;
            }
        }
    }
}
