using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.Domain.Configurations.JobCategories;
using FGCIJOROSystem.Domain.Configurations.JobType;
using FGCIJOROSystem.Domain.Configurations.SectionJORO;
using FGCIJOROSystem.Domain.Configurations.Tariff;
using FGCIJOROSystem.Domain.Configurations.UnitsJORO;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Presentation.vConfiguration.vJobCategories;
using FGCIJOROSystem.Presentation.vConfiguration.vJobTypes;
using FGCIJOROSystem.Presentation.vConfiguration.vSection;
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
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Domain.Configurations.Users;

namespace FGCIJOROSystem.Presentation.vConfiguration.vTariff
{
    public partial class frmTariff : Telerik.WinControls.UI.RadForm
    {
        public frmTariff()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            CrudMode = new clsEnums.CRUDEMode();
            CrudMode = clsEnums.CRUDEMode.Add;
            DataEvent = new clsDataEvent();
            GetSections();
            GetJobCategories();
            GetJobTypes();
            GetUnitsJORO();
        }
        public frmTariff(clsTariff obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            CrudMode = new clsEnums.CRUDEMode();
            CrudMode = clsEnums.CRUDEMode.Edit;
            DataEvent = new clsDataEvent();
            Tariff = new clsTariff();
            Tariff = obj;
            GetSections();
            GetJobCategories();
            GetJobTypes();
            GetUnitsJORO();
        }
        #region Properties
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        public clsDataEvent DataEvent;
        clsTariff Tariff;
        clsEnums.CRUDEMode CrudMode;
        public frmMainWindow MainWindowPage;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion

        #region Configuration

        private void GetSections()
        {
            List<clsSectionJORO> SectionList = new List<clsSectionJORO>();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) => { SectionList = new SectionJORORepository().GetAll(); };
            bg.RunWorkerCompleted += (s, e) => { cbSection.DataSource = SectionList; };
            bg.RunWorkerAsync();
            cbSection.ValueMember = "Id";
            cbSection.DisplayMember = "SectionName";

            //MessageBox.Show("BG work completed Sections");
        }
        private void GetJobCategories()
        {
            List<clsJobCategory> JobCategoryList = new List<clsJobCategory>();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) => { JobCategoryList = new JobCategoryRepository().GetAll(); };
            bg.RunWorkerCompleted += (s, e) => { cbJobCategory.DataSource = JobCategoryList; };
            bg.RunWorkerAsync();
            cbJobCategory.ValueMember = "Id";
            cbJobCategory.DisplayMember = "JobCategoryName";
        }

        private void GetJobTypes()
        {
            List<clsJobType> JobTypeList = new List<clsJobType>();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) => {JobTypeList = new JobTypeRepository().GetAll();};
            bg.RunWorkerCompleted += (s, e) => { cbJobType.DataSource = JobTypeList; };
            bg.RunWorkerAsync();
            cbJobType.ValueMember = "Id";
            cbJobType.DisplayMember = "JobTypeName";
        }
        private void GetUnitsJORO()
        {
            List<clsUnitJORO> ProductUnitList = new List<clsUnitJORO>();
            ProductUnitList = new UnitsJORORepository().GetAllActive();
            cbUnit.DataSource = ProductUnitList;
            cbUnit.ValueMember = "Id";
            cbUnit.DisplayMember = "UnitName";
        }
        #endregion

        #region Add
        private void Add()
        {
            Tariff = new clsTariff();
            new TariffRepository().Add(Map(Tariff));
        }
        
        #endregion

        #region Update
        private void Update()
        {
            new TariffRepository().Update(Map(Tariff));
        }
        #endregion

        #region Mapping
        private clsTariff Map(clsTariff curTarif)
        {
            Tariff.Id = curTarif.Id;
            Tariff.Name = tbName.Text;
            Tariff.SectionId = (Int64)cbSection.SelectedValue;
            Tariff.SectionName = cbSection.Text;
            Tariff.JobCategoryId = (Int64)cbJobCategory.SelectedValue;
            Tariff.JobCategoryName = cbJobCategory.Text;
            Tariff.JobTypeId = (Int64)cbJobType.SelectedValue;
            Tariff.JobTypeName = cbJobType.Text;
            Tariff.WorkDescription = tbWorkDescription.Text;
            Tariff.NoOfMechanics = numNoOfMechanics.Value;
            Tariff.Price = numPrice.Value;
            Tariff.UnitId = (Int64)cbUnit.SelectedValue;
            Tariff.WorkTimeSpan = numWorkTimeSpan.Value;
            Tariff.Active = tgActive.Value;
            return Tariff;
        }
        #endregion

        #region Find Duplicate
        private bool IsDuplicate()
        {
            clsTariff inTariff = new clsTariff();
            inTariff = new TariffRepository().SearchBy(" WHERE T.[JobCategoryId] = '" + cbJobCategory.SelectedValue.ToString() + "' AND T.[JobTypeId] = '" + cbJobType.SelectedValue.ToString() + "' AND T.SectionId = '" + cbSection.SelectedValue.ToString() + "' AND T.WorkDescription = '" + tbWorkDescription.Text.Trim()).FirstOrDefault();//SingleOrDefault();
            if (inTariff != null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Display Properties
        private void DisplayProperties()
        {
            tbName.Text = Tariff.Name;
            cbSection.SelectedValue = Tariff.SectionId;
            cbJobCategory.SelectedValue = Tariff.JobCategoryId;
            cbJobType.SelectedValue = Tariff.JobTypeId;
            tbWorkDescription.Text = Tariff.WorkDescription;
            numNoOfMechanics.Value = Tariff.NoOfMechanics;
            numPrice.Value = Tariff.Price;
            cbUnit.SelectedValue = Tariff.UnitId;
            numWorkTimeSpan.Value = Tariff.WorkTimeSpan;
            tgActive.Value = Tariff.Active;
        }
        #endregion

        #region Textbox AutoSuggest
       
        private void tbWorkDescription_TextChanged(object sender, EventArgs e)
        {
            //WorkDescription();
        }
        void WorkDescription()
        {
            List<clsTariff> ListOfTariff = new List<clsTariff>();
            if (cbSection.SelectedValue != null && cbJobCategory.SelectedValue != null && cbJobType.SelectedValue != null)
            {
                clsTariffBindingSource.DataSource = new TariffRepository().SearchBy(" WHERE T.[JobCategoryId] = " + cbJobCategory.SelectedValue.ToString() + " AND T.[JobTypeId] = " + cbJobType.SelectedValue.ToString() + " AND T.SectionId = " + cbSection.SelectedValue.ToString());
            }
        }
        #endregion
        private void frmTariff_Load(object sender, EventArgs e)
        {
            loadOnStart();
            //WorkDescription();
        }

        public void loadOnStart()
        {
            if (CrudMode == clsEnums.CRUDEMode.Edit)
            {
                DisplayProperties();
            }

            this.cbSection.TextChanged += new System.EventHandler(this.cbSection_TextChanged);
            this.cbJobCategory.TextChanged += new System.EventHandler(this.cbJobCategory_TextChanged);
            this.cbJobType.TextChanged += new System.EventHandler(this.cbJobType_TextChanged);
            this.cbUnit.TextChanged += new System.EventHandler(this.cbUnit_TextChanged);
        }
        void Save()
        {
            if (tbWorkDescription .Text == "")
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "All fields with * are required."
                };
                MsgBox.ShowDialog();
                tbWorkDescription.Focus();
            }
            else
            {
                try
                {
                    //if (!IsDuplicate())
                    //{
                        switch (CrudMode)
                        {
                            case clsEnums.CRUDEMode.Add:
                                Add();
                                break;
                            case clsEnums.CRUDEMode.Edit:
                                Update();
                                break;
                            case clsEnums.CRUDEMode.Delete:
                                break;
                            default:
                                break;
                        }
                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Success,
                            Message = "The record has been saved successfully."
                        };
                        MsgBox.ShowDialog();
                        //DataEvent.ConfirmData(Tariff);
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    //}
                    //else
                    //{
                    //    frmMsg MsgBox = new frmMsg()
                    //    {
                    //        MsgBox = clsEnums.MsgBox.Warning,
                    //        Message = "The record is already exist."
                    //    };
                    //    MsgBox.ShowDialog();
                    //}
                }
                catch (Exception ex)
                {
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Error,
                        Message = "Error!"
                    };
                    MsgBox.ShowDialog();
                }
            }
        }
        private void cbSection_TextChanged(object sender, EventArgs e)
        {
            //cbSection.ShowDropDown();
        }

        private void cbJobCategory_TextChanged(object sender, EventArgs e)
        {
            //cbJobCategory.ShowDropDown();
        }

        private void cbJobType_TextChanged(object sender, EventArgs e)
        {
            //cbJobType.ShowDropDown();
        }

        private void cbUnit_TextChanged(object sender, EventArgs e)
        {
            //cbUnit.ShowDropDown();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            Save();
        }

        private void tbName_Click(object sender, EventArgs e)
        {
            tbName.Text = "";
        }

        private void btnAddSection_Click(object sender, EventArgs e)
        {
            frmSection SectionEntry = new frmSection();
            SectionEntry.ShowDialog();
        }

        private void btnAddJobCat_Click(object sender, EventArgs e)
        {
            frmJobCategoryEntry JobCategoryEntry = new frmJobCategoryEntry();
            JobCategoryEntry.ShowDialog();
        }

        private void btnAddJobType_Click(object sender, EventArgs e)
        {
            frmJobTypeEntry JobTypeEntry = new frmJobTypeEntry();
            JobTypeEntry.ShowDialog();
        }

        private void cbSection_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbSection.SelectedValue != null && cbJobCategory.SelectedValue != null && cbJobType.SelectedValue != null)
            {
                WorkDescription();
            }
        }
        clsUsersLog AddMapProperties()
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                currUser.Username = Program.CurrentUser.UserName;
                currUser.MLEmployeeId = Program.CurrentUser.MLEmployeeId;
                currUser.EmpName = Program.CurrentUser.FullName;
                currUser.BranchId = Program.CurrentUser.BranchId;
                currUser.UserLevelId = Program.CurrentUser.UserLevelId;
                currUser.ComputerName = System.Environment.MachineName;
                currUser.DateLogin = System.DateTime.Now;
                currUser.TimeLogin = System.DateTime.Now;
                currUser.DateLogout = System.DateTime.Now;
                currUser.TimeLogout = System.DateTime.Now;
                currUser.OnlineUser = false;
                currUser.DayActivity = "Add Tariff (" + cbJobCategory.Text + "," + cbJobType.Text + "," + cbSection.Text + "," + tbWorkDescription.Text + ")";
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                currUser.Username = Program.CurrentUser.UserName;
                currUser.MLEmployeeId = Program.CurrentUser.MLEmployeeId;
                currUser.EmpName = Program.CurrentUser.FullName;
                currUser.BranchId = Program.CurrentUser.BranchId;
                currUser.UserLevelId = Program.CurrentUser.UserLevelId;
                currUser.ComputerName = System.Environment.MachineName;
                currUser.DateLogin = System.DateTime.Now;
                currUser.TimeLogin = System.DateTime.Now;
                currUser.DateLogout = System.DateTime.Now;
                currUser.TimeLogout = System.DateTime.Now;
                currUser.OnlineUser = false;
                currUser.DayActivity = "Update Tariff (" + cbJobCategory.Text + "," + cbJobType.Text + "," + cbSection.Text + "," + tbWorkDescription.Text + ")";
            }
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void FrmTariff_Activated(object sender, EventArgs e)
        {
            GetSections();
            GetJobCategories();
            GetJobTypes();
            GetUnitsJORO();
            WorkDescription();
        }
    }
}
