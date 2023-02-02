using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Contractors;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation.vConfiguration.vContractors
{
    public partial class frmContractors : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        public clsDataEvent DataEvent;

        Action SaveAction;
        clsEnums.CRUDEMode CRUDEMode;
        ContractorRepository ContractorRepo;
        clsContractor Contractor;
        clsUsersLog currUser;
        UsersLogRepository UsersLogRepo;
        #endregion
        #region Methods
        public frmContractors()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            CRUDEMode = clsEnums.CRUDEMode.Add;
            DataEvent = new clsDataEvent();
            Contractor = new clsContractor();
        }
        public frmContractors(clsContractor obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            CRUDEMode = clsEnums.CRUDEMode.Edit;
            DataEvent = new clsDataEvent();
            Contractor = obj;
        }

        private void frmContractors_Load(object sender, EventArgs e)
        {
            loadTypeName();
            //Contractor.ContractorType = clsEnums.ContractorType.External;
            if (CRUDEMode == clsEnums.CRUDEMode.Add)
            {
                SaveAction = Add;
            }
            else if (CRUDEMode == clsEnums.CRUDEMode.Edit)
            {
                SaveAction = Edit;
                DisplayProperties();
            }
            this.cbTypeName.TextChanged += new System.EventHandler(this.cbTypeName_TextChanged);
        }
        void loadTypeName()
        {
            cbTypeName.DataSource = Enum.GetValues(typeof(clsEnums.ContractorCategory));
        }
        void Add()
        {
            ContractorRepo = new ContractorRepository();
            ContractorRepo.Add(MapProperties());

        }
        void Edit()
        {
            ContractorRepo = new ContractorRepository();
            ContractorRepo.Update(MapProperties());
           
        }
        void DisplayProperties()
        {
            tbAddress.Text = Contractor.Address;
            tbCompany.Text = Contractor.CompanyName;
            tbContactNumber.Text = Contractor.ContactNos;
            cbTypeName.SelectedValue = (clsEnums.ContractorCategory)Contractor.ContractorCategory;
            tbFirstName.Text = Contractor.Firstname;
            tbMiddleName.Text = Contractor.Middlename;
            tbLastName.Text = Contractor.Lastname;
            tbNameExtension.Text = Contractor.NameExtension;
            tgActive.Value = Contractor.Active;
        }
        clsContractor MapProperties()
        {
            Contractor.Address = tbAddress.Text;
            Contractor.CompanyName = tbCompany.Text;
            Contractor.ContactNos = tbContactNumber.Text;
            Contractor.ContractorCategory = (clsEnums.ContractorCategory)cbTypeName.SelectedValue;
            Contractor.Firstname = tbFirstName.Text;
            Contractor.Middlename = tbMiddleName.Text;
            Contractor.Lastname = tbLastName.Text;
            Contractor.NameExtension = tbNameExtension.Text;
            Contractor.Active = tgActive.Value;
            return Contractor;
        }
        void Save()
        {
            if (CanSave())
            {
                SaveStatus();
            }
        }
        Boolean CanSave()
        {
            clsEnums.ContractorCategory InternalType = ((clsEnums.ContractorCategory)cbTypeName.SelectedItem.DataBoundItem);
            switch (InternalType)
            {
                case clsEnums.ContractorCategory.Person:
                    if (tbFirstName.Text == "" && tbLastName.Text=="" && tbLastName.Text == "")
                    {
                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Warning,
                            Message = "All fields with * are required."
                        };
                        MsgBox.ShowDialog();
                        tbFirstName.Focus();
                    }
                    break;
                case clsEnums.ContractorCategory.Company:
                    if (tbCompany.Text=="")
                    {
                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Warning,
                            Message = "All fields with * are required."
                        };
                        MsgBox.ShowDialog();
                        tbCompany.Focus();
                    }
                    break;
                case clsEnums.ContractorCategory.Department:
                    if (tbCompany.Text == "")
                    {
                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Warning,
                            Message = "All fields with * are required."
                        };
                        MsgBox.ShowDialog();
                        tbCompany.Focus();
                    }
                    break;
                case clsEnums.ContractorCategory.Section:
                    if (tbCompany.Text=="")
	                {
                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Warning,
                            Message = "All fields with * are required."
                        };
                        MsgBox.ShowDialog();
                        tbCompany.Focus();
	                }
                    break;
                default:
                    return false;
            }           
            return true;
        }
        void SaveStatus()
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                SaveAction.Invoke();
                DataEvent.ConfirmData(Contractor);
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                SaveAction.Invoke();
                DataEvent.ConfirmData(Contractor);
            }
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been saved successfully."
            };
            MsgBox.ShowDialog();
            this.Dispose();
        }
        #endregion

        private void cbTypeName_TextChanged(object sender, EventArgs e)
        {
            cbTypeName.ShowDropDown();
        }

        private void cbTypeName_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            _departmentChange();
        }
        void _departmentChange()
        {
            clsEnums.ContractorCategory InternalType = ((clsEnums.ContractorCategory)cbTypeName.SelectedItem.DataBoundItem);
            switch (InternalType)
            {
                case clsEnums.ContractorCategory.Person:
                    if (tbFirstName.Text.Equals("") && tbLastName.Text.Equals("") && tbLastName.Text.Equals(""))
                    {
                        lblCompany.Text = "Company :";
                        tbFirstName.Enabled = true;
                        tbMiddleName.Enabled = true;
                        tbLastName.Enabled = true;
                        tbNameExtension.Enabled = true;
                    }
                    break;
                case clsEnums.ContractorCategory.Company:
                    if (tbCompany.Text.Equals(""))
                    {
                        lblCompany.Text = "Company : ";
                        tbFirstName.Enabled = false;
                        tbMiddleName.Enabled = false;
                        tbLastName.Enabled = false;
                        tbNameExtension.Enabled = false;
                    }
                    break;
                case clsEnums.ContractorCategory.Department:
                    if (tbCompany.Text.Equals(""))
                    {
                        lblCompany.Text = "Department : ";
                        tbFirstName.Enabled = false;
                        tbMiddleName.Enabled = false;
                        tbLastName.Enabled = false;
                        tbNameExtension.Enabled = false;
                    }
                    break;
                case clsEnums.ContractorCategory.Section:
                    if (tbCompany.Text.Equals(""))
                    {
                        lblCompany.Text = "Section : ";
                        tbFirstName.Enabled = false;
                        tbMiddleName.Enabled = false;
                        tbLastName.Enabled = false;
                        tbNameExtension.Enabled = false;
                    }
                    break;
            }
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
                currUser.DayActivity = "Add Contractors (" + tbLastName.Text + ", " + tbCompany.Text + ")";
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
                currUser.DayActivity = "Update Contractors (" + tbLastName.Text + ", " + tbCompany.Text + ")";
            }
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }




    }
}
