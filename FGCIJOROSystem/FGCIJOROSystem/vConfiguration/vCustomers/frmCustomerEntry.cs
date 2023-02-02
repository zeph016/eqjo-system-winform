using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Customers;
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

namespace FGCIJOROSystem.Presentation.vConfiguration.vCustomers
{
    public partial class frmCustomerEntry : Telerik.WinControls.UI.RadForm
    {
        public frmCustomerEntry()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            CRUDEMode = clsEnums.CRUDEMode.Add;
        }
        public frmCustomerEntry(clsEnums.CRUDEMode cm, clsCustomer curCustomer)
        {
            InitializeComponent();
            CRUDEMode = cm;
            currUser = new clsUsersLog();
            CRUDEMode = clsEnums.CRUDEMode.Edit;
            MapUpdate(curCustomer);
        }
        #region Properties && Variables
        clsEnums.CRUDEMode CRUDEMode;
        clsCustomer Customer;
        clsUsersLog currUser;
        UsersLogRepository UsersLogRepo;
        #endregion

        #region Saving
        private void Save()
        {
            try
            {
                if (tbCustomerName.Text == "" || tbAddress.Text == "" || tbPhoneNo.Text == "")
                {
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "All fields with * are required."
                    };
                    MsgBox.ShowDialog();
                    tbCustomerName.Focus();
                    return;
                }
                else
                {
                    switch (CRUDEMode)
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
                }
                
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }
        #endregion
        #region Add
        private void Add()
        {
            Customer = new clsCustomer();
            Customer.CustomerName = tbCustomerName.Text;
            Customer.CustomerAddress = tbAddress.Text;
            Customer.ContactNo = tbPhoneNo.Text;
            Customer.Active = tgActive.Value;
            new CustomerRepository().Add(Customer);

            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been saved successfully."
            };
            MsgBox.ShowDialog();
        }
        #endregion

        #region Map Update
        private void MapUpdate(clsCustomer MapCutomer)
        {
            Customer = MapCutomer;
            tbCustomerName.Text = Customer.CustomerName;
            tbAddress.Text = Customer.CustomerAddress;
            tbPhoneNo.Text = Customer.ContactNo;
            tgActive.Value = Customer.Active;
           
        }
        #endregion

        #region Update
        private void Update()
        {
            Customer.CustomerName = tbCustomerName.Text;
            Customer.CustomerAddress = tbAddress.Text;
            Customer.ContactNo = tbPhoneNo.Text;
            Customer.Active = tgActive.Value;
            new CustomerRepository().Update(Customer);
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been saved successfully."
            };
            MsgBox.ShowDialog();
        }
        
        #endregion

        #region Clear || Reset

        private void Clear()
        {
            Customer = new clsCustomer();
            tbCustomerName.Text = "";
            tbAddress.Text = "";
            tbPhoneNo.Text = "";
            tgActive.Value = true;
        }
        
        #endregion

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
                currUser.DayActivity = "Add Customer (" + tbCustomerName.Text + ")";
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
                currUser.DayActivity = "Update Customer (" + tbCustomerName.Text + ")";
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
