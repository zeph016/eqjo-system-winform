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
    public partial class frmAttendanceStatus : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        public clsDataEvent DataEvent;
        AttendanceStatusRepository AttendanceStatusRepo;
        clsAttendanceStatus AttendanceStatus;
        Action SaveAction;
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion
        #region Method
        public frmAttendanceStatus()
        {
            InitializeComponent();

            DataEvent = new clsDataEvent();
            currUser = new clsUsersLog();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
            AttendanceStatus = new clsAttendanceStatus();
        }
        public frmAttendanceStatus(clsAttendanceStatus obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new clsDataEvent();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
            AttendanceStatus = obj;
        }
        private void frmAttendanceStatus_Load(object sender, EventArgs e)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {               
                SaveAction = add;
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                DisplayProperties();
                SaveAction = edit;
            }
        }
        void add()
        {
            AttendanceStatusRepo = new AttendanceStatusRepository();
            AttendanceStatusRepo.Add(MapProperties());
        }
        void edit()
        {
            AttendanceStatusRepo = new AttendanceStatusRepository();
            AttendanceStatusRepo.Update(MapProperties());
        }
        #endregion
        #region DisplayProperties
        void DisplayProperties()
        {
            tbGroupName.Text = AttendanceStatus.AttendanceStatusName;
            tbDescription.Text = AttendanceStatus.AttendanceStatusDescription;
            clbColor.Value = Color.FromArgb(AttendanceStatus.intColor);
            tgActive.Value = AttendanceStatus.IsActive;
            tbSymbol.Text = AttendanceStatus.Symbol;
        }
        #endregion
        #region MapProperties
        clsAttendanceStatus MapProperties()
        {
            AttendanceStatus.AttendanceStatusName = tbGroupName.Text;
            AttendanceStatus.AttendanceStatusDescription = tbDescription.Text;
            AttendanceStatus.intColor = clbColor.Value.ToArgb();
            AttendanceStatus.IsActive = tgActive.Value;
            AttendanceStatus.Symbol = tbSymbol.Text;
            return AttendanceStatus;
        }
        #endregion
        #region Save
        void Save()
        {
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
                    DataEvent.ConfirmData(AttendanceStatus);
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
                    DataEvent.ConfirmData(AttendanceStatus);
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "The record has been saved successfully!"
                    };
                    MsgBox.ShowDialog();
                }
                this.Dispose();
            }
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
                currUser.DayActivity = "Update Attendance Status " + "(" + tbGroupName.Text + ")";
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