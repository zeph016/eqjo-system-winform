using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Attendance;
using FGCIJOROSystem.Domain.Configurations.Personnels;
using FGCIJOROSystem.Domain.Configurations.SectionJORO;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Presentation.vLookups;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Transactions;
using FGCIJOROSystem.DAL.Repositories;
using System.Linq;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Configurations.Users;

namespace FGCIJOROSystem.Presentation.vConfiguration.vSection
{
    public partial class frmSection : Telerik.WinControls.UI.RadForm
    { 
        #region Properties
        clsSectionJORO Section;
        SectionJORORepository SectionRepo;
        PersonnelRepository PersonnelRepo;

        List<clsPersonnels> ListOfPersonnel;
        List<clsPersonnels> DeletedPersonnel;
        clsAttendanceGroup AttendanceGroup;
        Action SaveAction;
        clsDataEvent DataEvent;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode; 
        #endregion
        #region Methods
        public frmSection()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            Section = new clsSectionJORO();
            DeletedPersonnel = new List<clsPersonnels>();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
            DataEvent = new clsDataEvent();
        }
        public frmSection(clsSectionJORO obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DeletedPersonnel = new List<clsPersonnels>();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
            DataEvent = new clsDataEvent();
            Section = obj;            
        }
        void loadAttendanceGroup()
        {
            AttendanceGroupRepository AttendanceGroupRepo = new AttendanceGroupRepository();
            cbAttendanceGroup.DataSource = AttendanceGroupRepo.GetAll();
            cbAttendanceGroup.ValueMember = "Id";
            cbAttendanceGroup.DisplayMember = "AttendanceGroupName";
        }
        void loadBranch()
        {
            BranchRepository BranchRepo = new BranchRepository();
            cbBranch.DataSource = BranchRepo.GetAll();
            cbBranch.ValueMember = "Id";
            cbBranch.DisplayMember = "BranchName";
        }
        void loadPersonnel()
        {
            PersonnelRepo = new PersonnelRepository();
            Section.ListOfPersonnel = PersonnelRepo.SearchBy("WHERE P.SectionId = " + Section.Id.ToString());
            //Section.ListOfPersonnel = PersonnelRepo.GetAllActive("WHERE P.Active = " + Section.Active.ToString());
        }
        private void frmSection_Load(object sender, EventArgs e)
        {
            loadBranch();     //CHLOE
            loadAttendanceGroup(); //CHLOE
            

            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                ListOfPersonnel = new List<clsPersonnels>();
                SaveAction = Add;
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                SaveAction = Edit;
                loadPersonnel();
                DisplayProperties();
            }

           // loadAttendanceGroup(); //CHLOE
          //  loadBranch();     //CHLOE
            clsPersonnelsBindingSource.DataSource = ListOfPersonnel;

            this.cbBranch.TextChanged += new System.EventHandler(this.cbBranch_TextChanged);
            this.cbAttendanceGroup.TextChanged += new System.EventHandler(this.cbAttendanceGroup_TextChanged);
        }
        void Add()
        {
            SectionRepo = new SectionJORORepository();
            SectionRepo.Add(MapProperties());
            DeletedPersonnel.ForEach(x => {
                PersonnelRepo = new PersonnelRepository();
                PersonnelRepo.Delete(x);
            });
        }
        void Edit()
        {
            SectionRepo = new SectionJORORepository();
            SectionRepo.Update(MapProperties());
            DeletedPersonnel.ForEach(x =>
            {
                PersonnelRepo = new PersonnelRepository();
                PersonnelRepo.Delete(x);
            });
        }
        clsSectionJORO MapProperties()
        {
            Section.Active = tgActive.Value;
            Section.AttendanceGroupId = (Int64)cbAttendanceGroup.SelectedValue;
            var b = cbBranch.Text;
            Section.BranchId = (Int64)cbBranch.SelectedValue;
            Section.IsChecklistGroup = tgChecklistGroup.Value;
            Section.SectionName = tbSectionName.Text;
            Section.ListOfPersonnel = ListOfPersonnel;
            return Section;
        }
        void DisplayProperties()
        {
            cbBranch.SelectedValue = Section.BranchId;
            tgActive.Value = Section.Active;
            cbAttendanceGroup.SelectedValue = Section.AttendanceGroupId;
            tgChecklistGroup.Value = Section.IsChecklistGroup;
            tbSectionName.Text = Section.SectionName;
            ListOfPersonnel = Section.ListOfPersonnel;
        }
        void Save()
        {
            if (tbSectionName.Text == "")
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "All fields with * are required."
                };
                MsgBox.ShowDialog();
                tbSectionName.Focus();
            }
            else
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {
                        if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
                        {
                            SaveAction.Invoke();
                            //  DataEvent.ConfirmData(Section); -- check code
                        }
                        else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
                        {
                            SaveAction.Invoke();
                         //   DataEvent.ConfirmData(Section); -- check code
                        }
                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Success,
                            Message = "The record has been saved successfully."
                        };
                        MsgBox.ShowDialog();
                        ts.Complete();
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        //this.Dispose();                       
                    }
                    catch (Exception)
                    {
                        ts.Dispose();
                        throw;
                    }
                }
            }
        }
        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            frmEmployeeLookup EmployeeLookupPage = new frmEmployeeLookup(clsEnums.LookupMode.Employee);
            EmployeeLookupPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            EmployeeLookupPage.ShowDialog();
        }
        void DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                clsEmployee EmployeeInfo = (clsEmployee)obj;
                clsPersonnelsBindingSource.Add(new clsPersonnels
                {
                    EmployeeId = EmployeeInfo.Id,
                    FirstName = EmployeeInfo.FirstName,
                    MiddleName = EmployeeInfo.MiddleName,
                    LastName = EmployeeInfo.LastName,
                    NameExtension = EmployeeInfo.NameExtension,
                });               
            }
        }
        private void radBindingNavigator1DeleteItem_Click(object sender, EventArgs e)
        {
            if (clsPersonnelsBindingSource.Current != null)
            {
                //DeletedPersonnel = new List<clsPersonnels>();
                //DeletedPersonnel.Add((clsPersonnels)clsPersonnelsBindingSource.Current);
                //clsPersonnelsBindingSource.RemoveCurrent();

                PersonnelRepo = new PersonnelRepository();
                PersonnelRepo.DeletePersonnel((clsPersonnels)clsPersonnelsBindingSource.Current);
                clsPersonnels curJobType = (clsPersonnels)clsPersonnelsBindingSource.Current;
                curJobType.Active = false;
                clsPersonnelsBindingSource.ResetCurrentItem();

                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Success,
                    Message = "The record has been saved successfully."
                };
                MsgBox.ShowDialog();
            }
        }
        #endregion

        private void cbAttendanceGroup_TextChanged(object sender, EventArgs e)
        {
            cbAttendanceGroup.ShowDropDown();
        }

        private void cbBranch_TextChanged(object sender, EventArgs e)
        {
            cbBranch.ShowDropDown();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            Save();
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
                currUser.DayActivity = "Add Section (" + tbSectionName.Text + ")";
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
                currUser.DayActivity = "Update Section (" + tbSectionName.Text + ")";
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
    }
}
