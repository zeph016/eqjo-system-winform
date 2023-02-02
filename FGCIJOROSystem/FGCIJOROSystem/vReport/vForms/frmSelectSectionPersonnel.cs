using FGCIJOROSystem.DAL.Repositories;
using FGCIJOROSystem.Domain.Configurations.Personnels;
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
using FGCIJOROSystem.Domain.Configurations.SectionJORO;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;

namespace FGCIJOROSystem.Presentation.vReport.vForms
{
    public partial class frmSelectSectionPersonnel : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        public List<clsPersonnels> ListOfPersonnel { get; set; }
        public PersonnelRepository PersonnelRepo { get; set; }
        public clsPersonnels curPersonnel { get; set; }
        SectionJORORepository SectionJORORepo;
        #endregion
        #region Method
        
        #endregion
        public frmSelectSectionPersonnel()
        {
            InitializeComponent();
            PersonnelRepo = new PersonnelRepository();
        }
        void LoadAllSection()
        {
            SectionJORORepo = new SectionJORORepository();
            clsSectionJOROBindingSource.DataSource = SectionJORORepo.GetAll();

            //PersonnelRepo = new PersonnelRepository();
            //clsPersonnelsBindingSource.DataSource = PersonnelRepo.GetAll();
        }
        void GeneratePersonnelBySection()
        {
            curPersonnel = new clsPersonnels();
            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckSectionName).ToList();
            if (sectionList.Count != 0)
            {
                List<long> sectionIdList = new List<long>();
                sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
                string sectionIds = string.Join(",", sectionIdList);
                ListOfPersonnel = new List<clsPersonnels>();
                ListOfPersonnel = PersonnelRepo.GetAll(sectionIds);

                frmReportViewer ReportViewerPage = new frmReportViewer();
                var _SummaryReport = new rptPersonnelSummary(ListOfPersonnel, Program.CurrentUser.FullName, Program.CurrentUser.Position);
                ReportViewerPage = new frmReportViewer(_SummaryReport);
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.Show();
            }
            else
            {
                frmMsg msgPage = new frmMsg() { MsgBox = Domain.Enums.clsEnums.MsgBox.Warning, Message = "Please select personnel(s) to work with."};
                msgPage.ShowDialog();
            }
        }
        private void frmSelectSectionPersonnel_Load(object sender, EventArgs e)
        {
            LoadAllSection();
            this.ControlBox = false;
        }
        private void rgvSection_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = (clsSectionJORO)clsSectionJOROBindingSource.Current;
            current.CheckSectionName = !current.CheckSectionName;
            clsSectionJOROBindingSource.ResetCurrentItem();
        }
        private void btnGenerateReportPerSection_Click(object sender, EventArgs e)
        {
            GeneratePersonnelBySection();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rgvSection_Click(object sender, EventArgs e)
        {

        }
    }
}
