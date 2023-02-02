using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.Configurations.Attendance;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Configurations.Users;

namespace FGCIJOROSystem.Presentation.vConfiguration.vAttendance
{
    public partial class ucAttendanceStatus : UserControl
    {
        public ucAttendanceStatus()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }
        #region Properties
        AttendanceStatusRepository AttendanceStatusRepo;
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;

        clsAttendanceStatus AttendanceStatus;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion

        #region Methods
        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            frmAttendanceStatus EntryPage = new frmAttendanceStatus();
            EntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
            EntryPage.ShowDialog();
        }
        void DataEvent_OnDataConfirm(object obj)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                clsAttendanceStatusBindingSource.Add((clsAttendanceStatus)obj);
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                clsAttendanceStatusBindingSource.ResetCurrentItem();
            }
        }

        private void ucAttendanceStatus_Load(object sender, EventArgs e)
        {
            load();
        }
        void load()
        {
            AttendanceStatusRepo = new AttendanceStatusRepository();
            clsAttendanceStatusBindingSource.DataSource = AttendanceStatusRepo.GetAll();
        }
        #endregion

        #region Update
        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnUpdate")
            {
                frmAttendanceStatus EntryPage = new frmAttendanceStatus((clsAttendanceStatus)clsAttendanceStatusBindingSource.Current);
                EntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
                CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
                EntryPage.ShowDialog();
            }
        }
        #endregion

        #region Delete
        private void radBindingNavigator1DeleteItem_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            AttendanceStatusRepo = new AttendanceStatusRepository();
            AttendanceStatusRepo.Delete((clsAttendanceStatus)clsAttendanceStatusBindingSource.Current);
            clsAttendanceStatus curCustomer = (clsAttendanceStatus)clsAttendanceStatusBindingSource.Current;
            curCustomer.IsActive = false;
            clsAttendanceStatusBindingSource.ResetCurrentItem();
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
            currUser.DayActivity = "Deactivate Attendance Status- " + ((clsAttendanceStatus)clsAttendanceStatusBindingSource.Current).AttendanceStatusName;
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        #endregion

        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "btnUpdate")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Update");
            }
        }
    }
}