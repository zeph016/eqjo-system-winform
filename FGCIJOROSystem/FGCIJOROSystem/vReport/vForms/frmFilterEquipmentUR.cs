using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.EquipmentURRepo;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.DAL.Repositories.Reports;
using FGCIJOROSystem.Domain.Configurations.SectionJORO;
using FGCIJOROSystem.Domain.EquipmentUR;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Domain.Reports;
using FGCIJOROSystem.Reports.rEquipmentUR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;
using Telerik.WinControls;
using System.Linq;
using FGCIJOROSystem.Presentation.vLoader;

namespace FGCIJOROSystem.Presentation.vReport.vForms
{
    public partial class frmFilterEquipmentUR : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        EquipmentURRepository EquipmentURRepo;
        JOROReportRepository JOROReportRepo;

        List<clsEquipmentUR> ListOfEquipmentUR;
        List<clsJOROReports> ListOfJOROReports;
        List<clsEquipmentClasses> ListOfEquipmentClass;
        List<clsEquipmentType> ListOfEquipmentType;
        List<clsSectionJORO> ListOfSectionJORO;
        List<clsProject> ListOfProject;

        ProjectRepository ProjectRepo;
        AttendanceGroupRepository AGRepo;
        SectionJORORepository SectionRepo;
        #endregion
        #region Method
        public frmFilterEquipmentUR()
        {
            InitializeComponent();
            ListOfEquipmentUR = new List<clsEquipmentUR>();
        }
        #region Load
        void LoadPPEEquipmentStatus()
        {
            EquipmentURRepo = new EquipmentURRepository();
            cbEquipmentStatus.ValueMember = "EquipmentStatusId";
            cbEquipmentStatus.DisplayMember = "EquipmentStatus";
            cbEquipmentStatus.DataSource = EquipmentURRepo.GetAll();
        }
        void LoadSections()
        {
            SectionRepo = new SectionJORORepository();
            clsSectionJOROBindingSource.DataSource = SectionRepo.GetAllSections();
        }
        void LoadEquipmentStatus()
        {
            var EquipmentStatus = EnumHelper<FGCIJOROSystem.Domain.Enums.clsEnums.FilterEquipmentUR>.GetTypeEnum2();
            cbFilter.DataSource = EquipmentStatus;
            cbFilter.DisplayMember = "Name";
            cbFilter.ValueMember = "Id";

            cbFilter.SelectedValue = 0;
        }
        void LoadAttendanceGroup()
        {
            AGRepo = new AttendanceGroupRepository();
            cbAG.DisplayMember = "AttendanceGroupName";
            cbAG.ValueMember = "Id";
            cbAG.DataSource = AGRepo.GetAllActive();
        }
        private void frmFilterEquipmentUR_Load(object sender, EventArgs e)
        {
            LoadEquipment();
            LoadProject();
            LoadSections();
            LoadEquipmentClass();
            LoadEquipmentType();
        }
        void LoadEquipment()
        {
            JOROReportRepo = new JOROReportRepository();
            clsJOROReportsBindingSource.DataSource = JOROReportRepo.LoadAllEquipment();

        }
        void LoadProject()
        {
            ProjectRepo = new ProjectRepository();
            clsProjectBindingSource.DataSource = ProjectRepo.GetAll();
        }
        void LoadEquipmentClass()
        {
            EquipmentURRepo = new EquipmentURRepository();
            clsEquipmentBindingSource.DataSource = EquipmentURRepo.GetAllClass();
        }
        void LoadEquipmentType()
        {
            EquipmentURRepo = new EquipmentURRepository();
            clsEquipmentTypeBindingSource.DataSource = EquipmentURRepo.GetAllTypes();
        }
        #endregion
        #region CheckBoxes
        private void chkEquipmentStatus_CheckStateChanged(object sender, EventArgs e)
        {
            cbEquipmentStatus.Enabled = chkEquipmentStatus.Checked;
            if (chkEquipmentStatus.Checked)
            {
                LoadPPEEquipmentStatus();
            }
            else if (chkEquipmentStatus.Checked == false)
            {
                cbEquipmentStatus.Text = "";
            }
        }

        private void chkFilter_CheckStateChanged(object sender, EventArgs e)
        {
            cbFilter.Enabled = chkFilter.Checked;
            if (chkFilter.Checked)
            {
                LoadEquipmentStatus();
            }
            else if (chkFilter.Checked == false)
            {
                cbFilter.Text = "";
            }

            chkEquipmentStatus.Checked = false;
            chkSectionGroup.Checked = false;

            chkLocation.Enabled = true; //

        }
        private void chkSummary_CheckStateChanged(object sender, EventArgs e)
        {
            chkEquipmentStatus.Checked = false;
            chkSectionGroup.Checked = false;
            chkFilter.Checked = false;
        }

        private void chkEquipmentList_CheckStateChanged(object sender, EventArgs e)
        {
            pgvList.SelectedPage = pgEquipmentList;

            chkEquipmentStatus.Checked = false;
            chkSectionGroup.Checked = false;
            chkFilter.Checked = false;
        }

        private void chkProjectList_CheckStateChanged(object sender, EventArgs e)
        {
            dgvProject.Enabled = rdbProjectList.IsChecked;
            pgvList.SelectedPage = pgProjectList;

            chkEquipmentStatus.Checked = false;
            chkSectionGroup.Checked = false;
            chkFilter.Checked = false;
        }

        private void rgvEquipment_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsJOROReports)clsJOROReportsBindingSource.Current;
            current.CheckEquipment = !current.CheckEquipment;
            clsJOROReportsBindingSource.ResetCurrentItem();
        }

        private void dgvProject_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsProject)clsProjectBindingSource.Current;
            current.CheckProject = !current.CheckProject;
            clsProjectBindingSource.ResetCurrentItem();
        }

        private void chkSectionGroup_CheckStateChanged(object sender, EventArgs e)
        {
            cbAG.Enabled = chkSectionGroup.Checked;
            if (chkSectionGroup.Checked)
            {
                LoadAttendanceGroup();
            }
            else if (chkSectionGroup.Checked == false)
            {
                cbAG.Text = "";
            }

            chkEquipmentStatus.Checked = false;
            chkFilter.Checked = false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkPPEClass_CheckStateChanged(object sender, EventArgs e)
        {
            pgvList.SelectedPage = pgPPEClass;
        }

        private void chkSectionList_CheckStateChanged(object sender, EventArgs e)
        {
            pgvList.SelectedPage = pgSection;
            rgvSections.Enabled = rdbSectionList.IsChecked;

            chkEquipmentStatus.Checked = false;
            chkSectionGroup.Checked = false;
            chkFilter.Checked = false;
        }
        #endregion
        #region ReportFilter
        private string GenerateFilterByInSection()
        {
            List<string> resultList = new List<string>();
            if (chkLocation.Checked && chkFilter.Checked) //D
            {
                if (cbSearchEQLocation.Enabled == true && cbSearchEQLocation.Text == "ALL")
                {
                    string EL = ("Equipment Location: " + cbEquipmentLocation.SelectedItem.ToString() + " Filter: " + cbFilter.SelectedItem.ToString());
                    resultList.Add(EL);
                }
                else if (cbSearchEQLocation.Enabled == false)
                {
                    string EL = ("Equipment Location: " + cbEquipmentLocation.SelectedItem.ToString() + " Filter: " + cbFilter.SelectedItem.ToString());
                    resultList.Add(EL);
                }
                else if (cbSearchEQLocation.Enabled == true && cbSearchEQLocation.Enabled == true)
                {
                    string EL = ("Equipment Location: " + cbSearchEQLocation.SelectedItem.ToString() + " Filter: " + cbFilter.SelectedItem.ToString());
                    resultList.Add(EL);
                }
            }
            else if (chkEquipmentStatus.Checked)
            {
                string ES = ("Equipment Status: " + cbEquipmentStatus.SelectedItem);
                resultList.Add(ES);
            }
            else if (chkFilter.Checked)
            {
                string filter = ("Option: " + cbFilter.SelectedItem);
                resultList.Add(filter);
            }
            if (rdbSummary.IsChecked)
            {
                string O = ("Summary of Equipment Status");
                resultList.Add(O);
            }
            if (rdbEquipmentList.IsChecked)
            {
                string E = ("Selected Equipment");
                resultList.Add(E);
            }
            if (rdbProjectList.IsChecked)
            {
                string P = ("Selected Project");
                resultList.Add(P);
            }
            if (rdbPPEClass.IsChecked)
            {
                string EC = ("Selected Equipment Class");
                resultList.Add(EC);
            }
            if (rdbSectionList.IsChecked)
            {
                string S = ("Selected Section");
                resultList.Add(S);
            }
            if (rdbPPEType.IsChecked)
            {
                string ET = ("Selected Equipment Type");
                resultList.Add(ET);
            }
            if (chkSectionGroup.Checked)
            {
                string SG = ("Section Group : " + cbAG.SelectedItem);
                resultList.Add(SG);
            }
            if (rdbSummaryEquipList.IsChecked)
            {
                string EQ = ("Summary Per Equipment");
                resultList.Add(EQ);
            }
            return string.Join(" / ", resultList);
        }

        private string GenerateFilterByLocation()
        {
            List<string> resultList = new List<string>();
            if (chkLocation.Checked)
            {
                string EL = ("Equipment Location: " + cbSearchEQLocation.SelectedItem);
                resultList.Add(EL);
            }

            return string.Join(" / ", resultList);
        }
        #endregion
        #region Generator

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadingManager.ShowLoadScreen();
            Task t = Task.WhenAll(GenerateReport());
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
                LoadingManager.CloseLoadScreen();
        }

        private async Task GenerateReport()
        {
            if (chkFilter.Checked)
            {
               await GenerateAllOption();
            }
            else if (rdbSummary.IsChecked)
            {
               await SummaryExCompleted();
            }
            //if (chkSummaryEquipList.Checked)
            //{
            //    SummaryAllEquipmentRegardlessStatus();
            //}
            else if (rdbPPEClass.IsChecked)
            {
                await GeneratePerPPEClass();
            }
            else if (rdbEquipmentList.IsChecked)
            {
                await GeneratePerEquipment();
            }
            else if (rdbPPEType.IsChecked)
            {
                await GeneratePerPPEType();
            }
            else
            {
                await SummaryOfEquipment();
            }
        }
        #endregion
        
        void GenerateByStatus()
        {
            EquipmentURRepo = new EquipmentURRepository();
            ListOfEquipmentUR = new List<clsEquipmentUR>();
            ListOfEquipmentUR = EquipmentURRepo.GetAllURFiltered(chkEquipmentStatus.Checked, (Int64)cbEquipmentStatus.SelectedItem.Value);
            frmReportViewer ReportViewerPage = new frmReportViewer();
            string filter = GenerateFilterByInSection();
            var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
            ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
            ReportViewerPage.reportViewer1.RefreshReport();
            ReportViewerPage.Show();
        }
        private async Task SummaryExCompleted()
        {
            EquipmentURRepo = new EquipmentURRepository();
            ListOfEquipmentUR = new List<clsEquipmentUR>();
            Task t = GenerateAllSummary();
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                frmReportViewer ReportViewerPage = new frmReportViewer();
                string filter = GenerateFilterByInSection();
                var _tariffSummaryReport = new rptEquipmentURSummary(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
        }
        private async Task SummaryOfEquipment()
        {
            var classList = ((List<clsEquipmentClasses>)clsEquipmentBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> classIdList = new List<long>();
            classList.ForEach(item => { classIdList.Add(item.Id); });
            string classIds = string.Join(",", classIdList);

            var typeList = ((List<clsEquipmentType>)clsEquipmentTypeBindingSource.DataSource).Where(item => item.CheckEquipmentType).ToList();
            List<long> typeIdList = new List<long>();
            typeList.ForEach(item => { typeIdList.Add(item.Id); });
            string typeIds = string.Join(",", typeIdList);

            EquipmentURRepo = new EquipmentURRepository();
            ListOfEquipmentUR = new List<clsEquipmentUR>();
            Task t = GenerateSummaryOfEquipment(chkEquipmentStatus.Checked, chkEquipmentStatus.Checked ? (long)cbEquipmentStatus.SelectedValue : 0,
                rdbPPEClass.IsChecked, classIds, rdbPPEType.IsChecked, typeIds);
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                frmReportViewer ReportViewerPage = new frmReportViewer();
                string filter = GenerateFilterByInSection();
                var _tariffSummaryReport = new rptSummaryOfEquipment(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);//
                ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
        }
        void SummaryAllEquipmentRegardlessStatus()
        {
            EquipmentURRepo = new EquipmentURRepository();
            ListOfEquipmentUR = new List<clsEquipmentUR>();
            ListOfEquipmentUR = EquipmentURRepo.GenerateAllEquipment();
            frmReportViewer ReportViewerPage = new frmReportViewer();
            string filter = GenerateFilterByInSection();
            var _tariffSummaryReport = new rptEquipmentURSummary(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
            ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
            ReportViewerPage.reportViewer1.RefreshReport();
            ReportViewerPage.Show();
        }
        #region PerEquipment
        private async Task GeneratePerEquipment()
        {
            var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> equipmentIdList = new List<long>();
            equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
            string equipmentIds = string.Join(",", equipmentIdList);

            var classList = ((List<clsEquipmentClasses>)clsEquipmentBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> classIdList = new List<long>();
            classList.ForEach(item => { classIdList.Add(item.Id); });
            string classIds = string.Join(",", classIdList);

            var typeList = ((List<clsEquipmentType>)clsEquipmentTypeBindingSource.DataSource).Where(item => item.CheckEquipmentType).ToList();
            List<long> typeIdList = new List<long>();
            typeList.ForEach(item => { typeIdList.Add(item.Id); });
            string typeIds = string.Join(",", typeIdList);

            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckSectionName).ToList();
            List<long> sectionIdList = new List<long>();
            sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
            string sectionIds = string.Join(",", sectionIdList);

            var projectList = ((List<clsProject>)clsProjectBindingSource.DataSource).Where(item => item.CheckProject).ToList();
            List<long> projectIdList = new List<long>();
            projectList.ForEach(item => { projectIdList.Add(item.Id); });
            string projectIds = string.Join(",", projectIdList);

            EquipmentURRepo = new EquipmentURRepository();
            ListOfEquipmentUR = new List<clsEquipmentUR>();
            Task t = GeneratePPEWithFilter(rdbEquipmentList.IsChecked, equipmentIds, rdbPPEClass.IsChecked, 
                    classIds, rdbPPEType.IsChecked, typeIds, rdbSectionList.IsChecked, sectionIds, 
                    chkSectionGroup.Checked, chkSectionGroup.Checked ? (long)cbAG.SelectedValue : 0, 
                    rdbProjectList.IsChecked, projectIds);
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                frmReportViewer ReportViewerPage = new frmReportViewer();
                string filter = GenerateFilterByInSection();
                var _tariffSummaryReport = new rptEquipmentURSummary(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
        }
        #endregion
        #region PerClass
        private async Task GeneratePerPPEClass()
        {
            var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> equipmentIdList = new List<long>();
            equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
            string equipmentIds = string.Join(",", equipmentIdList);

            var classList = ((List<FGCIJOROSystem.Domain.EquipmentUR.clsEquipmentClasses>)clsEquipmentBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> classIdList = new List<long>();
            classList.ForEach(item => { classIdList.Add(item.Id); });
            string classIds = string.Join(",", classIdList);

            var typeList = ((List<clsEquipmentType>)clsEquipmentTypeBindingSource.DataSource).Where(item => item.CheckEquipmentType).ToList();
            List<long> typeIdList = new List<long>();
            typeList.ForEach(item => { typeIdList.Add(item.Id); });
            string typeIds = string.Join(",", typeIdList);

            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckSectionName).ToList();
            List<long> sectionIdList = new List<long>();
            sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
            string sectionIds = string.Join(",", sectionIdList);

            var projectList = ((List<clsProject>)clsProjectBindingSource.DataSource).Where(item => item.CheckProject).ToList();
            List<long> projectIdList = new List<long>();
            projectList.ForEach(item => { projectIdList.Add(item.Id); });
            string projectIds = string.Join(",", projectIdList);

            EquipmentURRepo = new EquipmentURRepository();
            ListOfEquipmentUR = new List<clsEquipmentUR>();
            Task t = GeneratePPEWithFilter(rdbEquipmentList.IsChecked, equipmentIds, rdbPPEClass.IsChecked,
                classIds, rdbPPEType.IsChecked, typeIds, rdbSectionList.IsChecked, sectionIds,
                chkSectionGroup.Checked, chkSectionGroup.Checked ? (long)cbAG.SelectedValue : 0,
                rdbProjectList.IsChecked, projectIds);
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                frmReportViewer ReportViewerPage = new frmReportViewer();
                string filter = GenerateFilterByInSection();
                var _tariffSummaryReport = new rptEquipmentURSummary(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
            //ListOfEquipmentUR = EquipmentURRepo.GenerateAll(chkEquipmentList.Checked, equipmentIds, chkPPEClass.Checked, classIds, chkPPEType.Checked, typeIds, chkSectionList.Checked, sectionIds, chkSectionGroup.Checked, chkSectionGroup.Checked ? (long)cbAG.SelectedValue : 0, chkProjectList.Checked, projectIds);
        }
        #endregion
        #region PerPPEType
        private async Task GeneratePerPPEType()
        {
            var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> equipmentIdList = new List<long>();
            equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
            string equipmentIds = string.Join(",", equipmentIdList);

            var classList = ((List<FGCIJOROSystem.Domain.EquipmentUR.clsEquipmentClasses>)clsEquipmentBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> classIdList = new List<long>();
            classList.ForEach(item => { classIdList.Add(item.Id); });
            string classIds = string.Join(",", classIdList);

            var typeList = ((List<clsEquipmentType>)clsEquipmentTypeBindingSource.DataSource).Where(item => item.CheckEquipmentType).ToList();
            List<long> typeIdList = new List<long>();
            typeList.ForEach(item => { typeIdList.Add(item.Id); });
            string typeIds = string.Join(",", typeIdList);

            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckSectionName).ToList();
            List<long> sectionIdList = new List<long>();
            sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
            string sectionIds = string.Join(",", sectionIdList);

            var projectList = ((List<clsProject>)clsProjectBindingSource.DataSource).Where(item => item.CheckProject).ToList();
            List<long> projectIdList = new List<long>();
            projectList.ForEach(item => { projectIdList.Add(item.Id); });
            string projectIds = string.Join(",", projectIdList);

            EquipmentURRepo = new EquipmentURRepository();
            ListOfEquipmentUR = new List<clsEquipmentUR>();
            Task t = GeneratePPEWithFilter(rdbEquipmentList.IsChecked, equipmentIds, rdbPPEClass.IsChecked, classIds, rdbPPEType.IsChecked, 
                    typeIds, rdbSectionList.IsChecked, sectionIds, chkSectionGroup.Checked, 
                    chkSectionGroup.Checked ? (long)cbAG.SelectedValue : 0, rdbProjectList.IsChecked, 
                    projectIds);
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                frmReportViewer ReportViewerPage = new frmReportViewer();
                string filter = GenerateFilterByInSection();
                var _tariffSummaryReport = new rptEquipmentURSummary(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
        }
        #endregion
        #region PerSection
        void GeneratePerSection()
        {
            var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> equipmentIdList = new List<long>();
            equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
            string equipmentIds = string.Join(",", equipmentIdList);

            var classList = ((List<FGCIJOROSystem.Domain.EquipmentUR.clsEquipmentClasses>)clsEquipmentBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> classIdList = new List<long>();
            classList.ForEach(item => { classIdList.Add(item.Id); });
            string classIds = string.Join(",", classIdList);

            var typeList = ((List<clsEquipmentType>)clsEquipmentTypeBindingSource.DataSource).Where(item => item.CheckEquipmentType).ToList();
            List<long> typeIdList = new List<long>();
            typeList.ForEach(item => { typeIdList.Add(item.Id); });
            string typeIds = string.Join(",", typeIdList);

            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckSectionName).ToList();
            List<long> sectionIdList = new List<long>();
            sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
            string sectionIds = string.Join(",", sectionIdList);

            var projectList = ((List<clsProject>)clsProjectBindingSource.DataSource).Where(item => item.CheckProject).ToList();
            List<long> projectIdList = new List<long>();
            projectList.ForEach(item => { projectIdList.Add(item.Id); });
            string projectIds = string.Join(",", projectIdList);

            EquipmentURRepo = new EquipmentURRepository();
            ListOfEquipmentUR = new List<clsEquipmentUR>();
            ListOfEquipmentUR = EquipmentURRepo.GenerateAll(rdbEquipmentList.IsChecked, equipmentIds, rdbPPEClass.IsChecked, classIds, rdbPPEType.IsChecked, typeIds, rdbSectionList.IsChecked, sectionIds, chkSectionGroup.Checked, chkSectionGroup.Checked ? (long)cbAG.SelectedValue : 0, rdbProjectList.IsChecked, projectIds);
            frmReportViewer ReportViewerPage = new frmReportViewer();
            string filter = GenerateFilterByInSection();
            var _tariffSummaryReport = new rptEquipmentURSummary(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
            ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
            ReportViewerPage.reportViewer1.RefreshReport();
            ReportViewerPage.Show();
        }
        #endregion
        #region PerSectionGroup
        void GeneratePerSectionGroup()
        {
            var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> equipmentIdList = new List<long>();
            equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
            string equipmentIds = string.Join(",", equipmentIdList);

            var classList = ((List<FGCIJOROSystem.Domain.EquipmentUR.clsEquipmentClasses>)clsEquipmentBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> classIdList = new List<long>();
            classList.ForEach(item => { classIdList.Add(item.Id); });
            string classIds = string.Join(",", classIdList);

            var typeList = ((List<clsEquipmentType>)clsEquipmentTypeBindingSource.DataSource).Where(item => item.CheckEquipmentType).ToList();
            List<long> typeIdList = new List<long>();
            typeList.ForEach(item => { typeIdList.Add(item.Id); });
            string typeIds = string.Join(",", typeIdList);

            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckSectionName).ToList();
            List<long> sectionIdList = new List<long>();
            sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
            string sectionIds = string.Join(",", sectionIdList);

            var projectList = ((List<clsProject>)clsProjectBindingSource.DataSource).Where(item => item.CheckProject).ToList();
            List<long> projectIdList = new List<long>();
            projectList.ForEach(item => { projectIdList.Add(item.Id); });
            string projectIds = string.Join(",", projectIdList);

            EquipmentURRepo = new EquipmentURRepository();
            ListOfEquipmentUR = new List<clsEquipmentUR>();
            ListOfEquipmentUR = EquipmentURRepo.GenerateAll(rdbEquipmentList.IsChecked, equipmentIds, rdbPPEClass.IsChecked, classIds, rdbPPEType.IsChecked, typeIds, rdbSectionList.IsChecked, sectionIds, chkSectionGroup.Checked, chkSectionGroup.Checked ? (long)cbAG.SelectedValue : 0, rdbProjectList.IsChecked, projectIds);
            frmReportViewer ReportViewerPage = new frmReportViewer();
            string filter = GenerateFilterByInSection();
            var _tariffSummaryReport = new rptEquipmentURSummary(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
            ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
            ReportViewerPage.reportViewer1.RefreshReport();
            ReportViewerPage.Show();
        }
        #endregion
        #region PerProject
        void GeneratePerProject()
        {
            var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> equipmentIdList = new List<long>();
            equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
            string equipmentIds = string.Join(",", equipmentIdList);

            var classList = ((List<FGCIJOROSystem.Domain.EquipmentUR.clsEquipmentClasses>)clsEquipmentBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
            List<long> classIdList = new List<long>();
            classList.ForEach(item => { classIdList.Add(item.Id); });
            string classIds = string.Join(",", classIdList);

            var typeList = ((List<clsEquipmentType>)clsEquipmentTypeBindingSource.DataSource).Where(item => item.CheckEquipmentType).ToList();
            List<long> typeIdList = new List<long>();
            typeList.ForEach(item => { typeIdList.Add(item.Id); });
            string typeIds = string.Join(",", typeIdList);

            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckSectionName).ToList();
            List<long> sectionIdList = new List<long>();
            sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
            string sectionIds = string.Join(",", sectionIdList);

            var projectList = ((List<clsProject>)clsProjectBindingSource.DataSource).Where(item => item.CheckProject).ToList();
            List<long> projectIdList = new List<long>();
            projectList.ForEach(item => { projectIdList.Add(item.Id); });
            string projectIds = string.Join(",", projectIdList);

            EquipmentURRepo = new EquipmentURRepository();
            ListOfEquipmentUR = new List<clsEquipmentUR>();
            ListOfEquipmentUR = EquipmentURRepo.GenerateAll(rdbEquipmentList.IsChecked, equipmentIds, rdbPPEClass.IsChecked, classIds, rdbPPEType.IsChecked, typeIds, rdbSectionList.IsChecked, sectionIds, chkSectionGroup.Checked, (Int64)cbAG.SelectedValue, rdbProjectList.IsChecked, projectIds);
            frmReportViewer ReportViewerPage = new frmReportViewer();
            string filter = GenerateFilterByInSection();
            var _tariffSummaryReport = new rptEquipmentURSummary(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
            ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
            ReportViewerPage.reportViewer1.RefreshReport();
            ReportViewerPage.Show();
        }
        #endregion
        #region GenerateOptions
        private async Task GenerateAllOption()
        {
            if (cbFilter.SelectedValue.ToString() == "0" && cbEquipmentLocation.Text == "ABILAY") //D
            {
                var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
                List<long> equipmentIdList = new List<long>();
                equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
                string equipmentIds = string.Join(",", equipmentIdList);

                EquipmentURRepo = new EquipmentURRepository();
                ListOfEquipmentUR = new List<clsEquipmentUR>();
                Task t = GenerateAllEquipmenSummaryURrepo("WHERE T.Location LIKE 'ab%' AND T.Location NOT LIKE ''");
                await t;
                if(t.Status == TaskStatus.RanToCompletion)
                {
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
            else if (cbFilter.SelectedValue.ToString() == "0" && cbEquipmentLocation.Text == "SITES") //D
            {
                if (chkALLSites.Checked == true)
                {
                    var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
                    List<long> equipmentIdList = new List<long>();
                    equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
                    string equipmentIds = string.Join(",", equipmentIdList);

                    EquipmentURRepo = new EquipmentURRepository();
                    ListOfEquipmentUR = new List<clsEquipmentUR>();
                    Task t = GenerateAllEquipmenSummaryURrepo("WHERE T.Location NOT LIKE 'ab%' AND T.Location NOT LIKE ''");
                    await t;
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        frmReportViewer ReportViewerPage = new frmReportViewer();
                        string filter = GenerateFilterByInSection();
                        var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                        ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                        ReportViewerPage.reportViewer1.RefreshReport();
                        ReportViewerPage.Show();
                    }
                }
                else
                {
                    var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
                    List<long> equipmentIdList = new List<long>();
                    equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
                    string equipmentIds = string.Join(",", equipmentIdList);

                    EquipmentURRepo = new EquipmentURRepository();
                    ListOfEquipmentUR = new List<clsEquipmentUR>();
                    if (string.IsNullOrEmpty(cbSearchEQLocation.Text))
                    {
                        RadMessageBox.Show("Select specific SITES or click ALL", "No SITES Selected", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                    }
                    else
                    {
                        Task t = Task.WhenAll(GenerateAllEquipmenSummaryURrepo("WHERE T.Location LIKE '" + cbSearchEQLocation.SelectedValue.ToString() + "%" + "'"));
                        await t;
                        if (t.Status == TaskStatus.RanToCompletion)
                        {
                            frmReportViewer ReportViewerPage = new frmReportViewer();
                            string filter = GenerateFilterByInSection();
                            var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                            ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                            ReportViewerPage.reportViewer1.RefreshReport();
                            ReportViewerPage.Show();
                        }
                    }
                }

            }
            else if (cbFilter.SelectedValue.ToString() == "0" && cbEquipmentLocation.Text == "OTHERS") //D
            {
                var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
                List<long> equipmentIdList = new List<long>();
                equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
                string equipmentIds = string.Join(",", equipmentIdList);

                EquipmentURRepo = new EquipmentURRepository();
                ListOfEquipmentUR = new List<clsEquipmentUR>();
                Task t = Task.WhenAll(GenerateAllEquipmenSummaryURrepo("WHERE T.Location LIKE ''"));
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
            else if (cbFilter.SelectedValue.ToString() == "1" && cbEquipmentLocation.Text == "ABILAY") //D
            {
                EquipmentURRepo = new EquipmentURRepository();
                ListOfEquipmentUR = new List<clsEquipmentUR>();
                Task t = GenerateAllURByJOROSearchBy("AND T.Location LIKE 'ab%' AND T.Location NOT LIKE ''");
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
            else if (cbFilter.SelectedValue.ToString() == "1" && cbEquipmentLocation.Text == "SITES") //D)
            {
                if (chkALLSites.Checked == true)
                {
                    EquipmentURRepo = new EquipmentURRepository();
                    ListOfEquipmentUR = new List<clsEquipmentUR>();
                    Task t = GenerateAllURByJOROSearchBy("AND T.Location NOT LIKE 'ab%' AND T.Location NOT LIKE ''");
                    await t;
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        frmReportViewer ReportViewerPage = new frmReportViewer();
                        string filter = GenerateFilterByInSection();
                        var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                        ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                        ReportViewerPage.reportViewer1.RefreshReport();
                        ReportViewerPage.Show();
                    }
                }
                else
                {
                    EquipmentURRepo = new EquipmentURRepository();
                    ListOfEquipmentUR = new List<clsEquipmentUR>();
                    if (string.IsNullOrEmpty(cbSearchEQLocation.Text))
                    {
                        RadMessageBox.Show("Select specific SITES or click ALL", "No SITES Selected", MessageBoxButtons.OK, RadMessageIcon.Exclamation);

                    }
                    else
                    {
                        Task t = GenerateAllURByJOROSearchBy("AND T.Location LIKE '" + cbSearchEQLocation.SelectedValue.ToString() + "%" + "'");
                        await t;
                        if (t.Status == TaskStatus.RanToCompletion)
                        {
                            frmReportViewer ReportViewerPage = new frmReportViewer();
                            string filter = GenerateFilterByInSection();
                            var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                            ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                            ReportViewerPage.reportViewer1.RefreshReport();
                            ReportViewerPage.Show();
                        }
                    }
                }

            }
            else if (cbFilter.SelectedValue.ToString() == "1" && cbEquipmentLocation.Text == "OTHERS")
            {
                EquipmentURRepo = new EquipmentURRepository();
                ListOfEquipmentUR = new List<clsEquipmentUR>();
                Task t = GenerateAllURByJOROSearchBy("AND T.Location LIKE ''");
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {

                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
            else if (cbFilter.SelectedValue.ToString() == "2" && cbEquipmentLocation.Text == "ABILAY")
            {
                EquipmentURRepo = new EquipmentURRepository();
                ListOfEquipmentUR = new List<clsEquipmentUR>();
                Task t = GenerateAllURByUnattendedSearchBy("AND T.Location LIKE 'ab%' AND T.Location NOT LIKE ''");
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
            else if (cbFilter.SelectedValue.ToString() == "2" && cbEquipmentLocation.Text == "SITES")
            {
                if (chkALLSites.Checked == true)
                {
                    EquipmentURRepo = new EquipmentURRepository();
                    ListOfEquipmentUR = new List<clsEquipmentUR>();
                    Task t = GenerateAllURByUnattendedSearchBy("AND T.Location NOT LIKE 'ab%' AND T.Location NOT LIKE ''");
                    await t;
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        frmReportViewer ReportViewerPage = new frmReportViewer();
                        string filter = GenerateFilterByInSection();
                        var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                        ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                        ReportViewerPage.reportViewer1.RefreshReport();
                        ReportViewerPage.Show();
                    }
                }
                else
                {
                    EquipmentURRepo = new EquipmentURRepository();
                    ListOfEquipmentUR = new List<clsEquipmentUR>();
                    if (string.IsNullOrEmpty(cbSearchEQLocation.Text))
                    {
                        RadMessageBox.Show("Select specific SITES or click ALL", "No SITES Selected", MessageBoxButtons.OK, RadMessageIcon.Exclamation);

                    }
                    else
                    {
                        Task t = GenerateAllURByUnattendedSearchBy("AND T.Location LIKE '" + cbSearchEQLocation.SelectedValue.ToString() + "%" + "'");
                        await t;
                        if (t.Status == TaskStatus.RanToCompletion)
                        {
                            frmReportViewer ReportViewerPage = new frmReportViewer();
                            string filter = GenerateFilterByInSection();
                            var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                            ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                            ReportViewerPage.reportViewer1.RefreshReport();
                            ReportViewerPage.Show();
                        }
                    }
                }
            }
            else if (cbFilter.SelectedValue.ToString() == "2" && cbEquipmentLocation.Text == "OTHERS")
            {
                EquipmentURRepo = new EquipmentURRepository();
                ListOfEquipmentUR = new List<clsEquipmentUR>();
                Task t = GenerateAllURByUnattendedSearchBy("AND T.Location LIKE ''");
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
            else if (cbFilter.SelectedValue.ToString() == "3" && cbEquipmentLocation.Text == "ABILAY")
            {
                EquipmentURRepo = new EquipmentURRepository();
                ListOfEquipmentUR = new List<clsEquipmentUR>();
                Task t = GenerateAllNonURWithJOROSearchBy("AND T.Location LIKE 'ab%' AND T.Location NOT LIKE ''");
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
            else if (cbFilter.SelectedValue.ToString() == "3" && cbEquipmentLocation.Text == "SITES")
            {
                if (chkALLSites.Checked == true)
                {
                    EquipmentURRepo = new EquipmentURRepository();
                    ListOfEquipmentUR = new List<clsEquipmentUR>();
                    Task t = GenerateAllNonURWithJOROSearchBy("AND T.Location NOT LIKE 'ab%' AND T.Location NOT LIKE ''");
                    await t;
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        frmReportViewer ReportViewerPage = new frmReportViewer();
                        string filter = GenerateFilterByInSection();
                        var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                        ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                        ReportViewerPage.reportViewer1.RefreshReport();
                        ReportViewerPage.Show();
                    }
                }
                else
                {
                    EquipmentURRepo = new EquipmentURRepository();
                    ListOfEquipmentUR = new List<clsEquipmentUR>();
                    if (string.IsNullOrEmpty(cbSearchEQLocation.Text))
                    {
                        RadMessageBox.Show("Select specific SITES or click ALL", "No SITES Selected", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                    }
                    else
                    {
                        Task t = GenerateAllNonURWithJOROSearchBy("AND T.Location LIKE '" + cbSearchEQLocation.SelectedValue.ToString() + "%" + "'");
                        await t;
                        if (t.Status == TaskStatus.RanToCompletion)
                        {
                            frmReportViewer ReportViewerPage = new frmReportViewer();
                            string filter = GenerateFilterByInSection();
                            var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                            ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                            ReportViewerPage.reportViewer1.RefreshReport();
                            ReportViewerPage.Show();
                        }
                    }
                }

            }
            else if (cbFilter.SelectedValue.ToString() == "3" && cbEquipmentLocation.Text == "OTHERS")
            {
                EquipmentURRepo = new EquipmentURRepository();
                ListOfEquipmentUR = new List<clsEquipmentUR>();
                Task t = GenerateAllNonURWithJOROSearchBy("AND T.Location LIKE ''");
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
            else if (cbFilter.SelectedValue.ToString() == "0")
            {
                var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(item => item.CheckEquipment).ToList();
                List<long> equipmentIdList = new List<long>();
                equipmentList.ForEach(item => { equipmentIdList.Add(item.EquipmentId); });
                string equipmentIds = string.Join(",", equipmentIdList);

                EquipmentURRepo = new EquipmentURRepository();
                ListOfEquipmentUR = new List<clsEquipmentUR>();
                Task t = GenerateAllSummary();
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
            else if (cbFilter.SelectedValue.ToString() == "1")
            {
                EquipmentURRepo = new EquipmentURRepository();
                ListOfEquipmentUR = new List<clsEquipmentUR>();
                Task t = GenerateAllURWithJORO();
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
            else if (cbFilter.SelectedValue.ToString() == "2")
            {
                EquipmentURRepo = new EquipmentURRepository();
                ListOfEquipmentUR = new List<clsEquipmentUR>();
                Task t = GenerateAllURUnattended();
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
            else if (cbFilter.SelectedValue.ToString() == "3")
            {
                EquipmentURRepo = new EquipmentURRepository();
                ListOfEquipmentUR = new List<clsEquipmentUR>();
                Task t = GenerateAllNonURButWithJORO();
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    frmReportViewer ReportViewerPage = new frmReportViewer();
                    string filter = GenerateFilterByInSection();
                    var _tariffSummaryReport = new rptEquipmentUR(ListOfEquipmentUR, filter, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                    ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                    ReportViewerPage.reportViewer1.RefreshReport();
                    ReportViewerPage.Show();
                }
            }
        }
        #endregion


        #region Repository Methods
        private async Task GenerateAllEquipmenSummaryURrepo(string sqlQuery)
        {
            var result = await Task.Run(() => EquipmentURRepo.GenerateAllSummarySearchBy(sqlQuery));
            if (result != null)
                ListOfEquipmentUR = result;
            else
                ListOfEquipmentUR = new List<clsEquipmentUR>();
        }

        private async Task GenerateAllURByJOROSearchBy(string sqlQuery)
        {
            var result = await Task.Run(() => EquipmentURRepo.GenerateAllURWithJOROSearchBy(sqlQuery));
            if (result != null)
                ListOfEquipmentUR = result;
            else
                ListOfEquipmentUR = new List<clsEquipmentUR>();
        }
        
        private async Task GenerateAllURByUnattendedSearchBy(string sqlQuery)
        {
            var result = await Task.Run(() => EquipmentURRepo.GenerateAllURUnattendedSearchBy(sqlQuery));
            if (result != null)
                ListOfEquipmentUR = result;
            else
                ListOfEquipmentUR = new List<clsEquipmentUR>();
        }

        private async Task GenerateAllNonURWithJOROSearchBy(string sqlQuery)
        {
            var result = await Task.Run(() => EquipmentURRepo.GenerateAllNonURButWithJOROSearchBy(sqlQuery));
            if (result != null)
                ListOfEquipmentUR = result;
            else
                ListOfEquipmentUR = new List<clsEquipmentUR>();
        }

        private async Task GenerateAllSummary()
        {
            var result = await Task.Run(() => EquipmentURRepo.GenerateAllSummary());
            if (result != null)
                ListOfEquipmentUR = result;
            else
                ListOfEquipmentUR = new List<clsEquipmentUR>();
        }

        private async Task GenerateAllURWithJORO()
        {
            var result = await Task.Run(() => EquipmentURRepo.GenerateAllURWithJORO());
            if (result != null)
                ListOfEquipmentUR = result;
            else
                ListOfEquipmentUR = new List<clsEquipmentUR>();
        }

        private async Task GenerateAllURUnattended()
        {
            var result = await Task.Run(() => EquipmentURRepo.GenerateAllURUnattended());
            if (result != null)
                ListOfEquipmentUR = result;
            else
                ListOfEquipmentUR = new List<clsEquipmentUR>();
        }

        private async Task GenerateAllNonURButWithJORO()
        {
            var result = await Task.Run(() => EquipmentURRepo.GenerateAllNonURButWithJORO());
            if (result != null)
                ListOfEquipmentUR = result;
            else
                ListOfEquipmentUR = new List<clsEquipmentUR>();
        }

        private async Task GeneratePPEWithFilter(bool isEquipment, string equipmentIds, 
            bool isPPEClass, string ppeClassId, bool isPPEType, string ppeTypeId, bool isSection, string sectionIds,
            bool isSectionGroup, long sectionGroupId, bool isProjectId, string projectIds)
        {
            var result = await Task.Run(() => EquipmentURRepo.GenerateAll(isEquipment, equipmentIds,
            isPPEClass, ppeClassId, isPPEType, ppeTypeId, isSection, sectionIds,
            isSectionGroup, sectionGroupId, isProjectId, projectIds));
            if (result != null)
                ListOfEquipmentUR = result;
            else
                ListOfEquipmentUR = new List<clsEquipmentUR>();
        }
        
        private async Task GenerateSummaryOfEquipment(bool isEquipmentStatus, long equipmentStatusId, bool isPPEClass,
            string ppeClassIds, bool isPPEType, string ppeTypeIds)
        {
            var result = await Task.Run(() => EquipmentURRepo.SummaryOfEquipment(isEquipmentStatus, equipmentStatusId, isPPEClass,
            ppeClassIds, isPPEType, ppeTypeIds));
            if (result != null)
                ListOfEquipmentUR = result;
            else
                ListOfEquipmentUR = new List<clsEquipmentUR>();
        }
        
        #endregion

        private void chkPPEType_CheckStateChanged(object sender, EventArgs e)
        {
            pgvList.SelectedPage = pgPPEType;
        }
        #endregion

        private void chkSummaryEquipList_CheckStateChanged(object sender, EventArgs e)
        {
            chkEquipmentStatus.Checked = false;
            chkSectionGroup.Checked = false;
            chkFilter.Checked = false;
        }
        

        private void rgvPPEClass_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (FGCIJOROSystem.Domain.EquipmentUR.clsEquipmentClasses)clsEquipmentBindingSource.Current;
            current.CheckEquipment = !current.CheckEquipment;
            clsEquipmentBindingSource.ResetCurrentItem();
        }

        private void rgvSections_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsSectionJORO)clsSectionJOROBindingSource.Current;
            current.CheckSectionName = !current.CheckSectionName;
            clsSectionJOROBindingSource.ResetCurrentItem();
        }
        private void rgvPPEType_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsEquipmentType)clsEquipmentTypeBindingSource.Current;
            current.CheckEquipmentType = !current.CheckEquipmentType;
            clsEquipmentTypeBindingSource.ResetCurrentItem();
        }

        private void CbEquipmentLocation_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cbEquipmentLocation.Text == "SITES")
            {
                cbSearchEQLocation.Enabled = true;
                cbSearchEQLocation.DisplayMember = "Location"; //
                cbSearchEQLocation.ValueMember = "Location"; //
                cbSearchEQLocation.DataSource = JOROReportRepo.LoadAllEquipmentLocations();//
                chkALLSites.Enabled = true;
            }
            if (cbEquipmentLocation.Text == "OTHERS")
            {
                cbSearchEQLocation.Enabled = false;
                cbSearchEQLocation.DataSource = null;
                chkALLSites.Enabled = false;
            }
            if (cbSearchEQLocation.Text == "ABILAY")
            {
                cbSearchEQLocation.Enabled = false;
                cbSearchEQLocation.DataSource = null;
                chkALLSites.Enabled = false;
            }
            
        }

        private void ChkLocation_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkLocation.Checked == true)
            {
                cbEquipmentLocation.Enabled = true;
                cbSearchEQLocation.Enabled = false;
                
            }
            else if(chkLocation.Checked == false)
            {
                cbEquipmentLocation.Enabled = false;
                cbEquipmentLocation.DataSource = null;
                cbSearchEQLocation.Enabled = false;
            }
               
        }

        private void ChkALLSites_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if(chkALLSites.Checked == true)
            {
                cbSearchEQLocation.Enabled = false;
                cbSearchEQLocation.DataSource = null;
            }
            else if (chkALLSites.Checked == false)
            {
                cbSearchEQLocation.Enabled = true;
                cbSearchEQLocation.DisplayMember = "Location"; //
                cbSearchEQLocation.ValueMember = "Location"; //
                cbSearchEQLocation.DataSource = JOROReportRepo.LoadAllEquipmentLocations();//
            }
        }

        private void rdbEquipmentList_CheckStateChanged(object sender, EventArgs e)
        {
            if (rdbEquipmentList.IsChecked)
                pgvList.SelectedPage = pgEquipmentList;
            rgvEquipment.Enabled = rdbEquipmentList.IsChecked;
        }

        private void rdbProjectList_CheckStateChanged(object sender, EventArgs e)
        {
            if (rdbProjectList.IsChecked)
                pgvList.SelectedPage = pgProjectList;
            dgvProject.Enabled = rdbProjectList.IsChecked;
        }

        private void rdbPPEClass_CheckStateChanged(object sender, EventArgs e)
        {
            if (rdbPPEClass.IsChecked)
                pgvList.SelectedPage = pgPPEClass;
            rgvPPEClass.Enabled = rdbPPEClass.IsChecked;
        }

        private void rdbSectionList_CheckStateChanged(object sender, EventArgs e)
        {
            if (rdbSectionList.IsChecked)
                pgvList.SelectedPage = pgSection;
            rgvSections.Enabled = rdbSectionList.IsChecked;
        }

        private void rdbPPEType_CheckStateChanged(object sender, EventArgs e)
        {
            if (rdbPPEType.IsChecked)
                pgvList.SelectedPage = pgPPEType;
            rgvPPEType.Enabled = rdbPPEType.IsChecked;
        }
    }
}