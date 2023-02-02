using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Domain.Configurations.Attendance;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Configurations.Users;

namespace FGCIJOROSystem.Presentation.vConfiguration.vAttendance
{
    public partial class ucAttendanceGroup : UserControl
    {       
        #region Properties
        AttendanceGroupRepository AttendanceGroupRepo;
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        clsUsersLog currUser;
        UsersLogRepository UsersLogRepo;
        #endregion
        #region Methods
        public ucAttendanceGroup()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }

        private void ucAttendanceGroup_Load(object sender, EventArgs e)
        {
            load();
        }

        void load()
        {
            AttendanceGroupRepo = new AttendanceGroupRepository();
            clsAttendanceGroupBindingSource.DataSource = AttendanceGroupRepo.GetAll();
            //radGridView1.Columns["clrColor"] = ((clsAttendanceGroup)clsAttendanceGroupBindingSource.Current).Color;
        }

        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            frmAttendanceGroup EntryPage = new frmAttendanceGroup();
            EntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
            EntryPage.ShowDialog();
        }

        private void radBindingNavigator1DeleteItem_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            AttendanceGroupRepo = new AttendanceGroupRepository();
            AttendanceGroupRepo.Delete((clsAttendanceGroup)clsAttendanceGroupBindingSource.Current);
            clsAttendanceGroup curAttendanceGroup = (clsAttendanceGroup)clsAttendanceGroupBindingSource.Current;
            curAttendanceGroup.IsActive = false;
            clsAttendanceGroupBindingSource.ResetCurrentItem();
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been deactivated successfully!"
            };
            MsgBox.ShowDialog();
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
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
            currUser.DayActivity = "Deactivate Attendance Group- " + ((clsAttendanceGroup)clsAttendanceGroupBindingSource.Current).AttendanceGroupName;
            return currUser;
        }
        void DataEvent_OnDataConfirm(object obj)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                clsAttendanceGroupBindingSource.Add((clsAttendanceGroup)obj);
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                clsAttendanceGroupBindingSource.ResetCurrentItem();
            }
        }
        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnUpdate")
            {
                frmAttendanceGroup EntryPage = new frmAttendanceGroup((clsAttendanceGroup)clsAttendanceGroupBindingSource.Current);
                EntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
                CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
                EntryPage.ShowDialog();
            }
        }
        #endregion

        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "btnUpdate")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Update");
            }
        }

        private void MasterTemplate_Click(object sender, EventArgs e)
        {

        }
    }
}
