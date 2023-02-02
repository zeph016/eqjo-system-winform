using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.Configurations.Contractors;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Configurations.Users;

namespace FGCIJOROSystem.Presentation.vConfiguration.vContractors
{
    public partial class ucContrator : UserControl
    {
        #region Properties
        clsEnums.CRUDEMode CRUDEMode;
        ContractorRepository ContractorRepo;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion
        #region Methods
        public ucContrator()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }

        private void ucContrator_Load(object sender, EventArgs e)
        {
            load();
        }
        void load()
        {
            BackgroundWorker bg = new BackgroundWorker();
            ContractorRepo = new ContractorRepository();
            bg.DoWork += (s, e) => 
            {
                e.Result = ContractorRepo.GetAll();
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsContractorBindingSource.DataSource = (List<clsContractor>)e.Result;
            };
            bg.RunWorkerAsync();
        }

        
        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            frmContractors EntryPage = new frmContractors();
            CRUDEMode = clsEnums.CRUDEMode.Add;
            EntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;            
            EntryPage.ShowDialog();
        }

        void DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                if(CRUDEMode == clsEnums.CRUDEMode.Add)
                {
                    clsContractorBindingSource.Add((clsContractor)obj);
                }
                if (CRUDEMode == clsEnums.CRUDEMode.Edit)
                {
                    clsContractorBindingSource.ResetCurrentItem();
                }                
            }
        }
        private void dgvContractor_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnUpdate")
            {
                frmContractors EntryPage = new frmContractors((clsContractor)clsContractorBindingSource.Current);
                CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
                EntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
                EntryPage.ShowDialog();
            }
        }
        #endregion

        private void radBindingNavigator1DeleteItem_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            ContractorRepo = new ContractorRepository();
            ContractorRepo.Delete((clsContractor)clsContractorBindingSource.Current);
            clsContractor curContractor = (clsContractor)clsContractorBindingSource.Current;
            curContractor.Active = false;
            clsContractorBindingSource.ResetCurrentItem();
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been deactivated successfully!"
            };
            MsgBox.ShowDialog();
        }
        clsUsersLog AddMapProperties()
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
            currUser.DayActivity = "Deactivate Contractors - " + ((clsContractor)clsContractorBindingSource.Current).FullName + "," + 
                                                                ((clsContractor)clsContractorBindingSource.Current).CompanyName;
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        private void dgvContractor_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "btnUpdate")
            {
                dgvContractor.Grid_CellFormatting(sender, e, "Update");
            }
        }

       
    }
}
