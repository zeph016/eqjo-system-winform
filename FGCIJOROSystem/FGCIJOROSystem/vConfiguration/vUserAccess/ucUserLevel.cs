using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Presentation.vSystem.vMain;
//using FGCIJOROSystem.Common;
namespace FGCIJOROSystem.Presentation.vConfiguration.vUserAccess
{
    public partial class ucUserLevel : UserControl
    {
        public ucUserLevel()
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
            currUser = new clsUsersLog();
            
        }
        public ucUserLevel(clsUserLevel obj)
        {
            InitializeComponent();
            curUserLevel = obj;
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
        }
        #region Properties
        public clsDataEvent DataEvent;
        UserAccessLevelRepository UserAccessLevelRepo;
        clsUserLevel curUserLevel;
        Action SaveAction;
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        public frmMainWindow MainWindowPage;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion
        #region Load
        void load()
        {
            UserAccessLevelRepo = new UserAccessLevelRepository();
            clsUserLevelBindingSource.DataSource = UserAccessLevelRepo.GetAll();
        }
        void Add()
        {
            UserAccessLevelRepo = new UserAccessLevelRepository();
            UserAccessLevelRepo.Add(MapProperties());
        }
        void Edit()
        {
            UserAccessLevelRepo = new UserAccessLevelRepository();
            UserAccessLevelRepo.Update(MapProperties());
        }
        void DisplayProperties()
        {
            tbName.Text = curUserLevel.UserLevelName;
            tgActive.Value = curUserLevel.Active;
        }
        clsUserLevel MapProperties()
        {
            curUserLevel.UserLevelName = tbName.Text;
            curUserLevel.Active = tgActive.Value;
            load();
            return curUserLevel;            
        }
        #endregion
        #region Cancel
        #endregion
        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            if (tbName.Text=="")
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "All fields are required!"
                };
                MsgBox.ShowDialog();
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                clsUserLevelBindingSource.Add(curUserLevel);
                SaveAction.Invoke();
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Success,
                    Message = "The record has been saved successfully!"
                };
                MsgBox.ShowDialog();
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                SaveAction.Invoke();
                clsUserLevelBindingSource.ResetCurrentItem();
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Success,
                    Message = "The record has been saved successfully!"
                };
                MsgBox.ShowDialog();
            }
            DataEvent.ConfirmData(MapProperties());
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
                currUser.DayActivity = "Add User Level (" + tbName.Text + ")";
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
                currUser.DayActivity = "Update User Level (" + tbName.Text + ")";
            }
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        private void cmdAdd_Click_1(object sender, EventArgs e)
        {
            _clearProperties();
            tbName.Focus();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            _clearProperties();
        }
        void _clearProperties()
        {
            tbName.Text = "";
            tgActive.Value = false;
        }
        private void ucUserLevel_Load(object sender, EventArgs e)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                curUserLevel = new clsUserLevel();
                SaveAction = Add;
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                DisplayProperties();
                SaveAction = Edit;
            }

            {
                load();
            }
        }

        void DataEvent_OnDataConfirm(object obj)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                clsUserLevelBindingSource.Add((UserAccessLevelRepository)obj);
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                clsUserLevelBindingSource.ResetCurrentItem();
            }
        }

        private void radGridView1_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "btnUpdate")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Update");
            }
        }

        private void radBindingNavigator1DeleteItem_Click(object sender, EventArgs e)
        {
            UpdateCurrentUser();
            UserAccessLevelRepo = new UserAccessLevelRepository();
            UserAccessLevelRepo.Delete((clsUserLevel)clsUserLevelBindingSource.Current);
            clsUserLevel curBranch = (clsUserLevel)clsUserLevelBindingSource.Current;
            curBranch.Active = false;
            clsUserLevelBindingSource.ResetCurrentItem();
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been deactivated successfully!"
            };
            MsgBox.ShowDialog();
        }

        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            _clearProperties();
            tbName.Focus();
        }

        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnUpdate")
            {
                curUserLevel = (clsUserLevel)clsUserLevelBindingSource.Current;
                DisplayProperties();
                SaveAction = Edit;
                CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
                tbName.Focus();
            }
        }

        private void radGridView1_CellClick(object sender, GridViewCellEventArgs e)
        {
            curUserLevel = (clsUserLevel)clsUserLevelBindingSource.Current;
            DisplayProperties();
            SaveAction = Edit;
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
            tbName.Focus();
        }
        clsUsersLog UpdateMapProperties()
        {
            currUser.Username = Program.CurrentUser.UserName;
            currUser.MLEmployeeId = Program.CurrentUser.MLEmployeeId;
            currUser.EmpName = Program.CurrentUser.FullName;
            currUser.BranchId = Program.CurrentUser.BranchId;
            currUser.UserLevelId = Program.CurrentUser.UserLevelId;
            currUser.ComputerName = System.Environment.MachineName;
            currUser.TimeLogin = System.DateTime.Now;
            currUser.TimeLogout = System.DateTime.Now;
            currUser.OnlineUser = false;
            currUser.DayActivity = "Deactivate UserLevel - " + ((clsUserLevel)clsUserLevelBindingSource.Current).UserLevelName;
            return currUser;
        }
        void UpdateCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(UpdateMapProperties());
        }
    }
}
