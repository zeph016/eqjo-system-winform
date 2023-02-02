using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.Reports;
using FGCIJOROSystem.DAL.Repositories.JORepo;
using FGCIJOROSystem.Domain.Configurations.SectionJORO;
using FGCIJOROSystem.Domain.Configurations.Tariff;
using FGCIJOROSystem.Domain.Reports;
using FGCIJOROSystem.Domain.JobOrder;
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
using System.Threading.Tasks;
using DevExpress.DashboardCommon.Viewer;
using DevExpress.XtraCharts;
using DevExpress.XtraSpellChecker;
using FGCIJOROSystem.Domain.Configurations.JobType;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Configurations.JobCategories;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Presentation.vLoader;

namespace FGCIJOROSystem.Presentation.vReport.vForms
{
    public partial class frmSelectTariff : Telerik.WinControls.UI.RadForm
    {
        

        #region Methods
        public frmSelectTariff()
        {
            InitializeComponent();
            TariffRepo = new TariffRepository();

            DisableControls();
        }

        #region Properties
        SectionJORORepository SectionJORORepo = new SectionJORORepository();
        JobCategoryRepository JobCategoryRepo = new JobCategoryRepository();
        JobTypeRepository JobTypeRepo = new JobTypeRepository();
        JOROReportRepository JOROReportRepo = new JOROReportRepository();
        JORepository JORepo = new JORepository();
        public List<clsTariff> ListOfTariff { get; set; }
        public TariffRepository TariffRepo { get; set; }
        public clsTariff curTariff { get; set; }
        public clsSectionJORO currSection { get; set; }

        public clsJobCategory JobCategoryCurrent; 
        public clsJobType JobTypeCurrent;
        //List<clsSectionJORO> ListOfSections;

        List<clsJobCategory> JobCategoryList;
        List<clsJobType> JobTypeList;
        List<clsTariff> TariffList;
        List<clsTariffEqJO> tariffListEqJO = new List<clsTariffEqJO>();
        List<clsTariffEqRO> tariffListEqRO = new List<clsTariffEqRO>();
        #endregion

        async Task LoadAllSection()
        {
            //clsSectionJOROBindingSource.DataSource = await Task.Run(() => SectionJORORepo.GetAll());
            clsSectionJOROBindingSource.DataSource = await Task.Run(() => SectionJORORepo.GetAllSections());
        }

        async Task LoadActiveJobCategory()
        {
            clsJobCategoryBindingSource.DataSource = await Task.Run(() => JobCategoryRepo.GetAllActiveJobCategory());
        }

        async Task LoadActiveJobTypes()
        {
            clsJobTypeBindingSource.DataSource = await Task.Run(() => JobTypeRepo.GetAllActiveJobTypes());
        }

        private async void frmSelectTariff_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            HidePages();
            Task t = Task.WhenAll(LoadAllSection(), LoadActiveJobCategory(), LoadActiveJobTypes(), LoadAllEquipments());
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                this.ControlBox = false;
            }
        }

        private void HidePages()
        {
            this.rpSummaryTariffs.SelectedPage = pageEquipment;
            this.rpSummaryTariffs.Pages[0].Item.Visibility = ElementVisibility.Collapsed;
            this.rpSummaryTariffs.Pages[1].Item.Visibility = ElementVisibility.Collapsed;
            this.rpSummaryTariffs.Pages[2].Item.Visibility = ElementVisibility.Collapsed;
        }

        private async Task LoadAllEquipments()
        {
            JOROReportRepo = new JOROReportRepository();
            clsJOROReportsBindingSource.DataSource = await Task.Run(() => JOROReportRepo.LoadAllEquipment());
        }
        #endregion

        private async void GenerateTariffList()
        {
            curTariff = new clsTariff();
            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckEquipmentName).ToList();
            if (sectionList.Count != 0)
            {
                List<long> sectionIdList = new List<long>();
                sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
                string sectionIds = string.Join(",", sectionIdList);

                ListOfTariff = new List<clsTariff>();
                ListOfTariff = await Task.Run(() => TariffRepo.GenerateByTariff(sectionIds));
                frmReportViewer ReportViewerPage = new frmReportViewer();
                var _tariffSummaryReport = new rptTariffSummary(ListOfTariff, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
            //else
            //{
            //    frmMsg msgPage = new frmMsg() { MsgBox = Domain.Enums.clsEnums.MsgBox.Warning, Message = "Please select section(s) to work with."};
            //    msgPage.ShowDialog();
            //}
        }
       


        async Task GenerateTariffJobCategory()
        {
            var jobCategoryList = ((List<clsJobCategory>)clsJobCategoryBindingSource.DataSource).Where(item => item.CheckJobCategoryName).ToList();
            if (jobCategoryList.Count != 0)
            {
                List<long> jobCategoryIdList = new List<long>();
                jobCategoryList.ForEach(item => { jobCategoryIdList.Add(item.JobCategoryId);});
                string jobCategoryId = string.Join(",", jobCategoryIdList);


                TariffList = await Task.Run(() => TariffRepo.GenerateTariffsByJobCategory(jobCategoryId));
                frmReportViewer ReportViewerPage = new frmReportViewer();
                var _tariffSummaryByJobCategory = new rptTariffByJobCategory(TariffList, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_tariffSummaryByJobCategory); 
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();

                //JobCategoryList = 
            }
        }

        async Task GenerateTariffJobType()
        {
            var jobTypeList = ((List<clsJobType>)clsJobTypeBindingSource.DataSource).Where(item => item.CheckJobTypeName).ToList();
            if (jobTypeList.Count != 0)
            {
                List<long> jobTypeIdLists = new List<long>();
                jobTypeList.ForEach(item => { jobTypeIdLists.Add(item.Id); });
                string jobTypeId = string.Join(",", jobTypeIdLists);


                TariffList = await Task.Run(() => TariffRepo.GenerateTariffsByJobType(jobTypeId));
                frmReportViewer ReportViewerPage = new frmReportViewer();
                var _tariffSummaryByJobType = new rptTariffByJobType(TariffList, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_tariffSummaryByJobType);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();

                //JobCategoryList = 
            }
        }
        private async void btnGenerateReportPerSection_Click(object sender, EventArgs e)
        {
            LoadingManager.ShowLoadScreen();
            if (rbSectionSummary.IsChecked)
            {
                GenerateTariffList();
            }

            if (rbCategory.IsChecked)
            {
                await GenerateTariffJobCategory();
            }

            if (rbJobType.IsChecked)
            {
                await GenerateTariffJobType();
            }

            if (rdbJO.Checked)
            {
                Task getJO = Task.WhenAll(GetAllJOEQ());
                await getJO;
                if (getJO.Status == TaskStatus.RanToCompletion)
                    LoadingManager.CloseLoadScreen();
            }
            if (rdbRO.Checked)
            {
                Task GetRO = Task.WhenAll(GetAllROEQ());
                await GetRO;
                if (GetRO.Status == TaskStatus.RanToCompletion)
                    LoadingManager.CloseLoadScreen();
            }

            //else
            //{
            //    frmMsg msgPage = new frmMsg() { MsgBox = Domain.Enums.clsEnums.MsgBox.Warning, Message = "Please select section(s) to work with." };
            //    msgPage.ShowDialog();
            //}
            LoadingManager.CloseLoadScreen();
        }

        private async Task GetAllJOEQ()
        {
            List<long> eqListIds = new List<long>();
            var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(x => x.CheckEquipment).ToList();
            equipmentList.ForEach(x => { eqListIds.Add(x.EquipmentId); });
            string equipmentIds = string.Join(",", eqListIds);
            Task getTariffList = Task.WhenAll(GenerateTariffsByEq(equipmentIds));
            await getTariffList;
            if (getTariffList.Status == TaskStatus.RanToCompletion)
            {
                frmReportViewer ReportViewerPage = new frmReportViewer();
                var _tariffSummaryReport = new rptTariffByEquipmentJO(tariffListEqJO, Program.CurrentUser.FullName, Program.CurrentUser.Position, rcbDateRange.Checked, dtpFrom.Value, dtpTo.Value);
                ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
        }
        private async Task GetAllROEQ()
        {
            List<long> eqListIds = new List<long>();
            var equipmentList = ((List<clsJOROReports>)clsJOROReportsBindingSource.DataSource).Where(x => x.CheckEquipment).ToList();
            equipmentList.ForEach(x => { eqListIds.Add(x.EquipmentId); });
            string equipmentIds = string.Join(",", eqListIds);
            Task getTariffList = Task.WhenAll(GenerateTariffsByEqRo(equipmentIds));
            await getTariffList;
            if (getTariffList.Status == TaskStatus.RanToCompletion)
            {
                frmReportViewer ReportViewerPage = new frmReportViewer();
                var _tariffSummaryReport = new rptTariffByEquipmentRO(tariffListEqRO, Program.CurrentUser.FullName, Program.CurrentUser.Position, rcbDateRange.Checked, dtpFrom.Value, dtpTo.Value);
                ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
        }

        private async Task GenerateTariffsByEq(string equipmentIds)
        {
            tariffListEqJO = await Task.Run(() => TariffRepo.GenerateTariffsByEquipmentJO(equipmentIds, rcbDateRange.Checked, dtpFrom.Value, dtpTo.Value));
        }
        private async Task GenerateTariffsByEqRo(string equipmentIds)
        {
            tariffListEqRO = await Task.Run(() => TariffRepo.GenerateTariffsByEquipmentRO(equipmentIds, rcbDateRange.Checked, dtpFrom.Value, dtpTo.Value));
        }

        private void rgvSection_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsSectionJORO)clsSectionJOROBindingSource.Current;
            current.CheckEquipmentName = !current.CheckEquipmentName;
            clsSectionJOROBindingSource.ResetCurrentItem();
        }

        private void rgvJobCategory_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsJobCategory) clsJobCategoryBindingSource.Current;
            current.CheckJobCategoryName = !current.CheckJobCategoryName;
            clsJobCategoryBindingSource.ResetCurrentItem();
        }

        private void rgvJobType_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsJobType)clsJobTypeBindingSource.Current;
            current.CheckJobTypeName = !current.CheckJobTypeName;
            clsJobTypeBindingSource.ResetCurrentItem();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        void DisableControls()
        {
            this.rpSummaryTariffs.Pages[0].Item.Enabled = false;
            this.rpSummaryTariffs.Pages[1].Item.Enabled = false;
            this.rpSummaryTariffs.Pages[2].Item.Enabled = false;
        }

        private async void rbSectionSummary_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (rbSectionSummary.IsChecked)
            {
                this.rpSummaryTariffs.Pages[0].Enabled = true;
                this.rpSummaryTariffs.SelectedPage = pageSections;
                this.rpSummaryTariffs.Pages[1].Enabled = false;
                this.rpSummaryTariffs.Pages[2].Enabled = false;

                await deselectCheckbox();
            }
            else
            {
                this.rpSummaryTariffs.Pages[1].Enabled = false;
                this.rpSummaryTariffs.Pages[2].Enabled = false;
            }
        }

        private async void rbCategory_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (rbCategory.IsChecked)
            {
                this.rpSummaryTariffs.Pages[1].Enabled = true;
                this.rpSummaryTariffs.SelectedPage = pageJobCategory;
                this.rpSummaryTariffs.Pages[0].Enabled = false;
                this.rpSummaryTariffs.Pages[2].Enabled = false;

                await deselectCheckbox();
            }
            else
            {
                this.rpSummaryTariffs.Pages[0].Enabled = false;
                this.rpSummaryTariffs.Pages[2].Enabled = false;
            }
        }

        private async void rbJobType_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (rbJobType.IsChecked)
            {
                this.rpSummaryTariffs.Pages[2].Enabled = true;
                this.rpSummaryTariffs.SelectedPage = pageJobType;
                this.rpSummaryTariffs.Pages[0].Enabled = false;
                this.rpSummaryTariffs.Pages[1].Enabled = false;

                await deselectCheckbox();
            }
            else
            {
                this.rpSummaryTariffs.Pages[0].Enabled = false;
                this.rpSummaryTariffs.Pages[1].Enabled = false;
            }
        }

        async Task deselectCheckbox()
        {
            if (rbSectionSummary.IsChecked == false)
            {
                foreach (GridViewRowInfo rowInfo in rgvSection.Rows)
                {
                    rowInfo.Cells["CheckEquipmentName"].Value = false;
                }
            }

            if (rbCategory.IsChecked == false)
            {
                foreach (GridViewRowInfo rowInfo in rgvJobCategory.Rows)
                {
                    rowInfo.Cells["CheckJobCategoryName"].Value = false;
                }
            }

            if (rbJobType.IsChecked == false)
            {
                foreach (GridViewRowInfo rowInfo in rgvJobType.Rows)
                {
                    rowInfo.Cells["CheckJobTypeName"].Value = false;
                }
            }
            
        }

        private void rcbDateRange_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if(rcbDateRange.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }
            else
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
        }
    }
}
