using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Attendance;
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

namespace FGCIJOROSystem.Presentation.vConfiguration.vAttendance
{
    public partial class frmAttendanceGroup : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        Action SaveAction;
        public clsDataEvent DataEvent;
        AttendanceGroupRepository AttendanceGroupRepo;
        clsAttendanceGroup AttendanceGroup;
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        UsersLogRepository UsersLogRepo;
        public clsUsersLog currUser { get; set; }
        #endregion
        #region Methods
        public frmAttendanceGroup()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new clsDataEvent();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
        }
        public frmAttendanceGroup(clsAttendanceGroup obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new clsDataEvent();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
            AttendanceGroup = obj;
        }
        private void frmAttendanceGroup_Load(object sender, EventArgs e)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                AttendanceGroup = new clsAttendanceGroup();
                SaveAction = Add;
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                DisplayProperties();
                SaveAction = Edit;
            }
        }
        void Add()
        {
            AttendanceGroupRepo = new AttendanceGroupRepository();
            AttendanceGroupRepo.Add(MapProperties());
        }
        void Edit()
        {
            AttendanceGroupRepo = new AttendanceGroupRepository();
            AttendanceGroupRepo.Update(MapProperties());
        }
        void DisplayProperties()
        {
            tbGroupName.Text = AttendanceGroup.AttendanceGroupName;
            tbDescription.Text = AttendanceGroup.AttendanceGroupDescription;
            clbColor.Value = Color.FromArgb(AttendanceGroup.intColor);
            tgActive.Value = AttendanceGroup.IsActive;
        }
        clsAttendanceGroup MapProperties()
        {
            AttendanceGroup.AttendanceGroupName = tbGroupName.Text;
            AttendanceGroup.AttendanceGroupDescription = tbDescription.Text;
            AttendanceGroup.intColor = clbColor.Value.ToArgb();
            AttendanceGroup.IsActive = tgActive.Value;
            return AttendanceGroup;
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
                currUser.DayActivity = "Add Attendance Group " + "(" + tbGroupName.Text + ")";
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
                currUser.DayActivity = "Update Attendance Group " + "(" + tbGroupName.Text + ")";
            }
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            if (tbGroupName.Text == "")
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "All fields with * are required."
                };
                MsgBox.ShowDialog();
                tbGroupName.Focus();
            }
            else
            {
                if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
                {
                    SaveAction.Invoke();
                    DataEvent.ConfirmData(AttendanceGroup);
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "The record has been saved successfully."
                    };
                    MsgBox.ShowDialog();
                }
                else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
                {
                    SaveAction.Invoke();
                    DataEvent.ConfirmData(AttendanceGroup);
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "The record has been saved successfully."
                    };
                    MsgBox.ShowDialog();
                }
                this.Dispose();
            }
        }
        #endregion

    }
}
