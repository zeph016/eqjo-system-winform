using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Branches;
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

namespace FGCIJOROSystem.Presentation.vConfiguration.vBranches
{
    public partial class frmBranches : Telerik.WinControls.UI.RadForm
    {        
        #region Properties
        clsBranch Branch;
        BranchRepository BranchRepo = new BranchRepository();
        Action SaveAction;
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        public clsDataEvent DataEvent;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion
        #region Method
        public frmBranches()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new Common.clsDataEvent();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
        }
        public frmBranches(clsBranch obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new Common.clsDataEvent();            
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
            Branch = obj;
        }
        private void frmBranches_Load(object sender, EventArgs e)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                Branch = new clsBranch();
                SaveAction = Add;
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                SaveAction = Edit;
                DisplayProperties();
            }
        }
        void Add()
        {
            BranchRepo = new BranchRepository();
            BranchRepo.Add(MapProperties());
        }
        void Edit()
        {
            BranchRepo = new BranchRepository();
            BranchRepo.Update(MapProperties());
        }
        void DisplayProperties()
        {
            tbBranchName.Text = Branch.BranchName;
            tgActive.Value = Branch.Active;
        }
        clsBranch MapProperties()
        {
            Branch.BranchName = tbBranchName.Text;
            Branch.Active = tgActive.Value;
            return Branch;
        }
        void Save()
        {
            if (CanSave())
            {
                SaveAction.Invoke();
                DataEvent.ConfirmData(Branch);
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Success,
                    Message = "The record has been saved successfully."
                };
                MsgBox.ShowDialog();
                this.Dispose();
            }
        }
        Boolean CanSave()
        {
            if (tbBranchName.Text == "")
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "All fields with * are required."
                };
                MsgBox.ShowDialog();
                tbBranchName.Focus();
            }
            return true;
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
                currUser.DayActivity = "Add Branches " + "(" + tbBranchName.Text + ")";
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
                currUser.DayActivity = "Update Branches " + "(" + tbBranchName.Text + ")";
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
