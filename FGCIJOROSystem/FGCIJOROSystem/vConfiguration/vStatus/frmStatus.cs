using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Status;
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

namespace FGCIJOROSystem.Presentation.vConfiguration.vStatus
{
    public partial class frmStatus : Telerik.WinControls.UI.RadForm
    {        
        #region Properties
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        public clsDataEvent DataEvent;
        StatusRepository StatusRepo;
        clsStatus Status;
        Action SaveAction;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion
        #region Methods
        public frmStatus()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new Common.clsDataEvent();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
        }
        public frmStatus(clsStatus obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new Common.clsDataEvent();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
            Status = obj;
        }
        private void frmStatus_Load(object sender, EventArgs e)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                Status = new clsStatus();
                SaveAction = Add;
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                DisplayProperties();                
                SaveAction = Edit;
            }
        }
        void Add()
        {
            StatusRepo = new StatusRepository();
            StatusRepo.Add(MapProperties());
            //MapProperties();
        }

        void Edit()
        {
            StatusRepo = new StatusRepository();
            StatusRepo.Update(MapProperties());
            //MapProperties();
        }
        void DisplayProperties()
        {
            tbSeqOrder.Value = Status.SequenceOrder;
            tbStatusName.Text = Status.StatusName;
            tbDescription.Text = Status.StatusDescription;
            tbStatusSymbol.Text = Status.EquipmentStatusSymbol;
            tgActive.Value = Status.Active;
            tgAlert.Value = Status.IsAlert;
            tgApproval.Value = Status.IsApproval;
            tgClosure.Value = Status.IsClosure;
            tgIsBilled.Value = Status.IsBilled;
            tgNeedsApproval.Value = Status.NeedsApproval;
            tgActive.Value = Status.Active;
            //tgPrintable.Value = Status.printa
        }
        clsStatus MapProperties()
        {
            Status.SequenceOrder = (Int64)tbSeqOrder.Value;
            Status.StatusName = tbStatusName.Text;
            Status.StatusDescription = tbDescription.Text;
            Status.EquipmentStatusSymbol = tbStatusSymbol.Text;
            Status.IsAlert = tgAlert.Value;
            Status.IsApproval = tgApproval.Value;
            Status.IsBilled = tgIsBilled.Value;
            Status.IsClosure = tgClosure.Value;
            Status.NeedsApproval = tgNeedsApproval.Value;
            Status.Printable = tgPrintable.Value;
            Status.Active = tgActive.Value;
            return Status;
        }
        void SaveStatus()
        {
            if (tbStatusName.Text == "")
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "All fields with * are required."
                };
                MsgBox.ShowDialog();
                tbStatusName.Focus();
            }
            else
            {
                if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
                {
                    SaveAction.Invoke();
                    DataEvent.ConfirmData(Status);
                }
                if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
                {
                    SaveAction.Invoke();
                    DataEvent.ConfirmData(Status);
                }
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Success,
                    Message = "The record has been saved successfully."
                };
                MsgBox.ShowDialog();
                this.Dispose();
            }
            
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            SaveStatus();
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
                currUser.DayActivity = "Add Status (" + tbStatusName.Text + ")";
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
                currUser.DayActivity = "Update Status (" + tbStatusName.Text + ")";
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
