using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.Configurations.Branches;
using Telerik.WinControls.UI;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Configurations.Users;


namespace FGCIJOROSystem.Presentation.vConfiguration.vBranches
{
    public partial class ucBranches : UserControl
    {
        #region Properties
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        BranchRepository BranchRepo = new BranchRepository();
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion
        #region Methods
        
        #endregion
        public ucBranches()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }
        private void ucBranches_Load(object sender, EventArgs e)
        {
            load();
        }
        void load()
        {
            BranchRepo = new BranchRepository();
            clsBranchBindingSource.DataSource = BranchRepo.GetAll();
        }
        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            frmBranches BranchEntryPage = new frmBranches();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
            BranchEntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            BranchEntryPage.ShowDialog();
        }
        private void radBindingNavigator1DeleteItem_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            BranchRepo = new BranchRepository();
            BranchRepo.Delete((clsBranch)clsBranchBindingSource.Current);
            clsBranch curBranch = (clsBranch)clsBranchBindingSource.Current;
            curBranch.Active = false;
            clsBranchBindingSource.ResetCurrentItem();
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
            currUser.DayActivity = "Deactivate Branches- " + ((clsBranch)clsBranchBindingSource.Current).BranchName;
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        void DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
                {
                    clsBranchBindingSource.Add((clsBranch)obj);
                }
                else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
                {
                    clsBranchBindingSource.ResetCurrentItem();
                    clsBranchBindingSource.EndEdit();
                }
            }           
        }
        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnUpdate")
            {
                frmBranches EntryPage = new frmBranches((clsBranch)clsBranchBindingSource.Current);
                CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
                EntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
                EntryPage.ShowDialog();
            }
        }

        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "btnUpdate")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Update");
            }
        }

    }
}
