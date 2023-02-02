using FGCIJOROSystem.DAL.Repositories;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Personnels;
using FGCIJOROSystem.Domain.Configurations.SectionJORO;
using FGCIJOROSystem.Presentation.vSystem.vMain;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Linq;
using System.Threading.Tasks;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Domain.Configurations.Tariff;
namespace FGCIJOROSystem.Presentation.vConfiguration.vTariff
{
    public partial class frmSelectSection : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        public clsDataEvent DataEvent; 
        public PersonnelRepository PersonnelRepo { get; set; }
        SectionJORORepository SectionJORORepo;
        public frmMainWindow MainWindowPage;
        TariffRepository TariffRepo;
        public List<clsTariff> ListOfTariff { get; set; }
        #endregion
        public frmSelectSection()
        {
            InitializeComponent();
            PersonnelRepo = new PersonnelRepository();
            DataEvent = new clsDataEvent();
        }
        void LoadAllSection()
        {
            SectionJORORepo = new SectionJORORepository();
            clsSectionJOROBindingSource.DataSource = SectionJORORepo.GetAll();
        }

        private void frmSelectSection_Load(object sender, EventArgs e)
        {
            LoadAllSection();
        }
        void GenerateBySection()
        {
            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckSectionName).ToList();
            if (sectionList.Count != 0)
            {
                List<long> sectionIdList = new List<long>();
                sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
                string sectionIds = string.Join(",", sectionIdList);

                BackgroundWorker bg = new BackgroundWorker();
                bg.DoWork += (s, e) =>
                {
                    TariffRepo = new TariffRepository();
                    e.Result = TariffRepo.GenerateByTariff(sectionIds);
                };
                bg.RunWorkerCompleted += (s, e) =>
                {
                    clsTariffBindingSource.DataSource = (List<clsTariff>)e.Result;
                    DataEvent.ConfirmData(e.Result);
                };
                bg.RunWorkerAsync();
            }
            else
            {
                frmMsg msgPage = new frmMsg() { MsgBox = Domain.Enums.clsEnums.MsgBox.Warning, Message = "Please select section(s) to work with." };
                msgPage.ShowDialog();
            }
            this.Dispose();
        }

        async Task AsyncGenerateBySection()
        {
            var sectionList = ((List<clsSectionJORO>)clsSectionJOROBindingSource.DataSource).Where(item => item.CheckSectionName).ToList();
            if (sectionList.Count != 0)
            {
                List<long> sectionIdList = new List<long>();
                sectionList.ForEach(item => { sectionIdList.Add(item.Id); });
                string sectionIds = string.Join(",", sectionIdList);

                
                TariffRepo = new TariffRepository();
                ListOfTariff =  await Task.Run(() => TariffRepo.GenerateByTariff(sectionIds));

                clsTariffBindingSource.DataSource = ListOfTariff;
                DataEvent.ConfirmData(ListOfTariff);
               
            }
            else
            {
                frmMsg msgPage = new frmMsg() { MsgBox = Domain.Enums.clsEnums.MsgBox.Warning, Message = "Please select section(s) to work with." };
                msgPage.ShowDialog();
            }
            this.Dispose();
        }

        //async Task AsyncLoadToDataSource()
        //{
        //    TariffRepo = new RODetailRepository();
        //    ListOfRODetails = await Task.Run(() => RODetailRepo.SearchBy(" WHERE RD.ROId = " + ((clsRescueOrder)clsRescueOrderBindingSource.Current).Id + " AND RD.IsActive = 1"));

        //    clsRODetailsBindingSource.DataSource = ListOfRODetails.ToList();

        //    Task t = Task.WhenAll(NewDisplayStatus());
        //    await t;
        //    t.Wait();

        //    if (t.Status == TaskStatus.RanToCompletion)
        //    {
        //        LoadPrint();
        //        clsRODetailsBindingSource.ResetBindings(true);
        //    }

        //    progressBarRO.Value = 100;
        //}
        private async void btnGenerateReportPerSection_Click_1(object sender, EventArgs e)
        {
            //GenerateBySection();
            await AsyncGenerateBySection();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rgvSection_CellClick(object sender, GridViewCellEventArgs e)
        {
            var current = (clsSectionJORO)clsSectionJOROBindingSource.Current;
            current.CheckSectionName = !current.CheckSectionName;
            clsSectionJOROBindingSource.ResetCurrentItem();
        }

        private void rgvSection_Click(object sender, EventArgs e)
        {

        }
    }
}