using FGCIJOROSystem.DAL.Repositories;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.JORepo;
using FGCIJOROSystem.DAL.Repositories.Reports;
using FGCIJOROSystem.Domain.Configurations.Personnels;
using FGCIJOROSystem.Domain.Configurations.SectionJORO;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.JobOrder;
using FGCIJOROSystem.Domain.Reports;
using FGCIJOROSystem.Presentation.vLoader;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Reports.rReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGCIJOROSystem.Presentation.vReport.vForms
{
    public partial class frmJOROSectionSummary : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        PersonnelRepository PersonnelRepo;
        JOROReportRepository JOROReportRepo;
        SectionJORORepository SectionJORORepo;
        public clsJOROReports curJOROReports { get; set; }
        List<clsJOROReports> ListOfJOROReports;
        AttendanceGroupRepository AGRepo;
        public clsEnums.JOROStatus currentStatus { get; set; }
        #endregion
        #region Method
        public frmJOROSectionSummary()
        {
            InitializeComponent();
            JOROReportRepo = new JOROReportRepository();
        }
        #region CheckboxOptionState
        private void chkStatus_CheckStateChanged(object sender, EventArgs e)
        {
            rcpStatus.Enabled = chkStatus.Checked;
            rgvEquipment.Enabled = rdbEquipment.IsChecked;
            rdbEquipment.IsChecked = chkStatus.Checked;
            pvList.SelectedPage = pgEquipment;
            rdbMechanics.IsChecked = false;

            rcpStatus.IsExpanded = chkStatus.Checked;

            if (chkStatus.Checked)
            {
                chkUnderRepair.Enabled = false;
                chkUnderRepair.Checked = false;
            }
        }
        private void frmJOROSectionSummary_Load(object sender, EventArgs e)
        {
            tbYear.Text = System.DateTime.Now.Year.ToString();
            AddItemToCategory();
            LoadAllList();
            cbMonth.DataSource = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToList();
            dtFromSection.Value = DateTime.Now;
            dtToSection.Value = DateTime.Now;
            //this.rcpStatus.TextChanged += new System.EventHandler(this.cbStatusSection_TextChanged);
            LoadFilteredEquipment();
            LoadFilteredSections();
        }
        private void chkDate_CheckStateChanged(object sender, EventArgs e)
        {
            dtFromSection.Enabled = chkDate.Checked;
            dtToSection.Enabled = chkDate.Checked;
        }
        private void rgvSection_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsSectionJORO)clsSectionJOROBindingSource.Current;
            current.CheckSectionName = !current.CheckSectionName;
            clsSectionJOROBindingSource.ResetCurrentItem();
        }
        private void chkCategoryInSection_CheckStateChanged(object sender, EventArgs e)
        {
            cbCategoryInSection.Enabled = chkCategoryInSection.Checked;
        }
        private void rgvEquipment_CellClick_1(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsJOROReports)clsJOROReportsBindingSource.Current;
            current.CheckEquipment = !current.CheckEquipment;
            clsJOROReportsBindingSource.ResetCurrentItem();
        }
        void AddItemToCategory()
        {
            cbCategoryInSection.DisplayMember = "Text";
            cbCategoryInSection.ValueMember = "Value";

            var items = new[] { 
            new { Text = "Load all Job Order (JO) only" ,    Value = "A" }, 
            new { Text = "Load all Rescue Order (RO) only", Value = "B" }, 
            new { Text = "Load all JO/RO",                  Value = "C" },
        };
            cbCategoryInSection.DataSource = items;
        }
        void LoadFilteredSections()
        {
            SectionJORORepo = new SectionJORORepository();
            clsSectionJOROBindingSource.DataSource = SectionJORORepo.FilterSections((Int64)cbSectionGroup.SelectedValue);
        }
        void LoadFilteredEquipment()
        {
            JOROReportRepo = new JOROReportRepository();
            clsJOROReportsBindingSource.DataSource = JOROReportRepo.LoadAllEquipment();
        }
        void LoadFilteredMechanic()
        {
            PersonnelRepo = new PersonnelRepository();
            clsPersonnelsBindingSource.DataSource = PersonnelRepo.GetAll((Int64)cbSectionGroup.SelectedValue);
        }
        void LoadAllList()
        {
            LoadAttendanceGroup();
            rclbStatus.DataSource = Enum.GetValues(typeof(clsEnums.JOROStatus));
        }
        #endregion
        private void radCheckBox1_CheckStateChanged(object sender, EventArgs e)
        {
            cbMonth.Enabled = chkMonth.Checked;
            chkYear.Checked = chkMonth.Checked;
            tbYear.Enabled = chkMonth.Checked;

            if (chkMonth.Checked)
            {
                chkUnderRepair.Checked = false;
                chkUnderRepair.Enabled = false;
            }
        }
        private void radCheckBox4_CheckStateChanged(object sender, EventArgs e)
        {
            tbYear.Enabled = chkYear.Checked;

            if (chkYear.Checked)
            {
                chkUnderRepair.Checked = false;
                chkUnderRepair.Enabled = false;
            }
        }
        #region GeneratePerSection
        private async void btnGenerateReportPerSection_Click(object sender, EventArgs e)
        {
            Task t = Task.WhenAll(GenerateBySectionList());
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
                LoadingManager.CloseLoadScreen();
        }
        private async Task GenerateBySectionList()
        {
            LoadingManager.ShowLoadScreen();
            curJOROReports = new clsJOROReports();
            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckSectionName).ToList();
            List<long> sectionIdList = new List<long>();
            sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
            string sectionIds = string.Join(",", sectionIdList);

            var mechanicList = ((List<clsPersonnels>)clsPersonnelsBindingSource.DataSource).Where(item => item.CheckPersonnel).ToList();
            List<long> mechanicListIdList = new List<long>();
            mechanicList.ForEach(item => { mechanicListIdList.Add(item.EmployeeId); });
            string MechanicIds = string.Join(",", mechanicListIdList);

            var EquipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> EquipmentIdList = new List<long>();
            EquipmentList.ForEach(item => { EquipmentIdList.Add(item.EquipmentId); });
            string equipmentIds = string.Join(",", EquipmentIdList);

            bool IsJO = false;
            bool IsRO = false;
            if (cbCategoryInSection.Text == "Load all Job Order (JO) only" && chkCategoryInSection.Checked)
            {
                IsJO = true;
            }
            if (cbCategoryInSection.Text == "Load all Rescue Order (RO) only" && chkCategoryInSection.Checked)
            {
                IsRO = true;
            }
            if (cbSectionGroup.Text == "SITE" && rdbSectionDetails.IsChecked)
            {
                IsRO = true;   
            }
            if (rdbSectionDetails.IsChecked)//(chkSectionDetails.Checked)
            {
                //IsRO = true;
                // ORIGINAl
                if (sectionList.Count != 0)
                {
                    ListOfJOROReports = new List<clsJOROReports>();
                    ListOfJOROReports = await Task.Run(() => JOROReportRepo.GetAllReportBySection(rdbMechanics.IsChecked, MechanicIds, rdbEquipment.IsChecked, equipmentIds, rdbSectionDetails.IsChecked, sectionIds, IsJO, IsRO, chkGroup.Checked, chkUnderRepair.Checked, (Int64)cbSectionGroup.SelectedValue));

                    ListOfJOROReports = FilterSectionReportList(ListOfJOROReports);
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _JOROSummaryReport = new rptSectionSummary(ListOfJOROReports, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_JOROSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
                //if (sectionList.Count != 0)
                //{
                //    ListOfJOROReports = new List<clsJOROReports>();
                //    ListOfJOROReports = await Task.Run(() => JOROReportRepo.GetAllReportBySection(chkMechanics.Checked, MechanicIds, chkEquipment.Checked, equipmentIds, chkSectionDetails.Checked, sectionIds, IsJO, IsRO, chkUnderRepair.Checked, chkGroup.Checked, (Int64)cbSectionGroup.SelectedValue));

                //    ListOfJOROReports = FilterSectionReportList(ListOfJOROReports);
                //    frmReportViewer ReportViewerPage = new frmReportViewer();
                //    string filter = GenerateFilterByInSection();
                //    var _JOROSummaryReport = new rptSectionSummary(ListOfJOROReports, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                //    ReportViewerPage = new frmReportViewer(_JOROSummaryReport);
                //    ReportViewerPage.reportViewer1.RefreshReport();
                //    ReportViewerPage.Show();
                //}
                else
                {
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "Please select section."
                    };
                    MsgBox.ShowDialog();
                }
            }
            else if (rdbEquipment.IsChecked)
            {
                if (EquipmentList.Count != 0)
                {
                    ListOfJOROReports = new List<clsJOROReports>();
                    ListOfJOROReports = await Task.Run(() => JOROReportRepo.GetAllReportBySection(rdbMechanics.IsChecked, MechanicIds, rdbEquipment.IsChecked, equipmentIds, rdbSectionDetails.IsChecked, sectionIds, IsJO, IsRO, chkGroup.Checked, chkUnderRepair.Checked, (Int64)cbSectionGroup.SelectedValue));

                    ListOfJOROReports = FilterSectionReportList(ListOfJOROReports);
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _JOROSummaryReport = new rptEquipmentSummary(ListOfJOROReports, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_JOROSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();

                }
                else
                {
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "Please select equipment."
                    };
                    MsgBox.ShowDialog();
                }
            }
            else if (rdbMechanics.IsChecked)
            {
                if (mechanicList.Count != 0)
                {
                    ListOfJOROReports = new List<clsJOROReports>();
                    //ListOfJOROReports = JOROReportRepo.GetAllReportByMechanic(chkMechanics.Checked, MechanicIds, chkEquipment.Checked, equipmentIds, chkSectionDetails.Checked, sectionIds, IsJO, IsRO, chkGroup.Checked, chkUnderRepair.Checked, (Int64)cbSectionGroup.SelectedValue);
                    ListOfJOROReports = await Task.Run(() => JOROReportRepo.GetAllReportBySection(rdbMechanics.IsChecked, MechanicIds, rdbEquipment.IsChecked, equipmentIds, rdbSectionDetails.IsChecked, sectionIds, IsJO, IsRO, chkGroup.Checked, chkUnderRepair.Checked, (Int64)cbSectionGroup.SelectedValue));

                    ListOfJOROReports = FilterSectionReportList(ListOfJOROReports);
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _JOROSummaryReport = new rptMechanicSummary(ListOfJOROReports, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_JOROSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
                else
                {
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "Please select mechanic."
                    };
                    MsgBox.ShowDialog();
                }
            }
            else if (chkEquipmentOnly.Checked)
            {
                if (sectionList.Count != 0)
                {
                    ListOfJOROReports = new List<clsJOROReports>();
                    ListOfJOROReports = await Task.Run(() => JOROReportRepo.GetAllEquipmentOnly(rdbMechanics.IsChecked, MechanicIds, rdbEquipment.IsChecked, equipmentIds, rdbSectionDetails.IsChecked, sectionIds, IsJO, IsRO, chkGroup.Checked, (Int64)cbSectionGroup.SelectedValue));
                    ListOfJOROReports = FilterSectionReportList(ListOfJOROReports);
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _JOROSummaryReport = new rptEquipmentSummaryOnly(ListOfJOROReports, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_JOROSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();

                }
                else
                {
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "Please select equipment."
                    };
                    MsgBox.ShowDialog();
                }
            }
            else
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Please select filter to work with."
                };
                MsgBox.ShowDialog();
            }
           
        }
        private List<clsJOROReports> FilterSectionReportList(List<clsJOROReports> list)
        {
            if (chkStatus.Checked)
            {            
                List<int> selectedStatus = rclbStatus.CheckedItems.Select(x => Convert.ToInt32((clsEnums.JOROStatus)x.Value)).ToList();
                list = list.Where(item => {
                    foreach (int status in selectedStatus)
                    {
                        if (item.Status == (clsEnums.JOROStatus)status) { return true; }
                    }
                    return false;
                }).ToList();
            }
            if (chkDate.Checked)
            {
                list = list.Where(item => item.JODate.Date >= dtFromSection.Value.Date && item.JODate.Date <= dtToSection.Value.Date).ToList();
            }
            if (chkMonth.Checked)
            {
                list = list.Where(item => item.JODate.Month == ((Int64)cbMonth.SelectedIndex + 1) && item.JODate.Year == tbYear.Value).ToList();
            }
            if (chkYear.Checked)
            {
                list = list.Where(item => item.JODate.Year == tbYear.Value).ToList();
            }
            return list;
        }
        private string GenerateFilterByInSection()
        {
            List<string> resultList = new List<string>();
            if (chkStatus.Checked)
            {
                List<string> selectedStatus = new List<string>();
                foreach (Telerik.WinControls.UI.ListViewDataItem item in rclbStatus.CheckedItems)
                {
                    selectedStatus.Add(item.Text);
                }
                resultList.Add("Status: " + string.Join(", ", selectedStatus));
            }
            if (chkCategoryInSection.Checked)
            {
                resultList.Add("Category: " + cbCategoryInSection.SelectedItem.ToString());
            }
            if (chkGroup.Checked)
            {
                resultList.Add("Section Group: " + cbSectionGroup.SelectedItem.ToString());
            }
            if (chkMonth.Checked)
            {
                resultList.Add("Month: " + cbMonth.SelectedItem.ToString());
            }
            if (chkYear.Checked)
            {
                resultList.Add("Year: " + tbYear.Value.ToString());
            }
            if (chkDate.Checked)
            {
                string date = ("Date Range: " + dtFromSection.Value.ToString("MMMM dd, yyyy") + " - " + dtToSection.Value.ToString("MMMM dd, yyyy"));
                resultList.Add(date);
            }
            return string.Join(" / ", resultList);
        }
        #endregion
        private void rgvMechanics_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsPersonnels)clsPersonnelsBindingSource.Current;
            current.CheckPersonnel = !current.CheckPersonnel;
            clsPersonnelsBindingSource.ResetCurrentItem();
        }
        void LoadAttendanceGroup()
        {
            AGRepo = new AttendanceGroupRepository();
            cbSectionGroup.DisplayMember = "AttendanceGroupName";
            cbSectionGroup.ValueMember = "Id";
            cbSectionGroup.DataSource = AGRepo.GetAllActive();
        }
        private void chkGroup_CheckStateChanged(object sender, EventArgs e)
        {
            cbSectionGroup.Enabled = chkGroup.Checked;

            if (chkGroup.Checked == true)
            {
                chkUnderRepair.Checked = false;
                chkUnderRepair.Enabled = false;
                chkCategoryInSection.Checked = false;
                chkCategoryInSection.Checked = false;
            }
        }

        private void cbSectionGroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            LoadFilteredSections();
            LoadFilteredMechanic();
        }
        #endregion

        private void chkEquipmentOnly_CheckStateChanged(object sender, EventArgs e)
        {
            chkDate.Checked = false;
            rdbSectionDetails.IsChecked = false;
            rdbEquipment.IsChecked = false;
        }

        private void chkUnderRepair_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkUnderRepair.IsChecked)
            {
                rdbEquipment.IsChecked = false;
                rdbMechanics.IsChecked = false;
            }
        }

        private void rdbSectionDetails_CheckStateChanged(object sender, EventArgs e)
        {
            if (rdbSectionDetails.IsChecked)
                pvList.SelectedPage = pgSection;
            rgvSection.Enabled = rdbSectionDetails.IsChecked;
        }

        private void rdbEquipment_CheckStateChanged(object sender, EventArgs e)
        {
            if (rdbEquipment.IsChecked)
                pvList.SelectedPage = pgEquipment;
            rgvEquipment.Enabled = rdbEquipment.IsChecked;
        }

        private void rdbMechanics_CheckStateChanged(object sender, EventArgs e)
        {
            if (rdbMechanics.IsChecked)
                pvList.SelectedPage = pgMechanic;
            rgvMechanics.Enabled = rdbMechanics.IsChecked;
        }
    }
}