using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.OtherEquipment;
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

namespace FGCIJOROSystem.Presentation.vConfiguration.vOtherEquipments
{
    public partial class frmOtherEquipments : Telerik.WinControls.UI.RadForm
    {
        public frmOtherEquipments()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            CRUDMode = new clsEnums.CRUDEMode();
            CRUDMode = clsEnums.CRUDEMode.Add;
            OtherEquipment = new clsOtherEquipment();
        }
        public frmOtherEquipments(clsOtherEquipment obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            CRUDMode = new clsEnums.CRUDEMode();
            CRUDMode = clsEnums.CRUDEMode.Edit;
            OtherEquipment = new clsOtherEquipment();
            OtherEquipment = obj;
            DisplayProperties();
        }

        #region Properties
        public clsEnums.CRUDEMode CRUDEMode { get; set; }
        clsOtherEquipment OtherEquipment;
        clsEnums.CRUDEMode CRUDMode;
        Action SaveAction;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion

        #region Mapping
        private clsOtherEquipment Mapping()
        {
            OtherEquipment.Name = tbName.Text;
            OtherEquipment.Active = tgActive.Value;
            return OtherEquipment;
        }
        #endregion

        #region Add
        private void Add()
        {
            new OtherEquipmentRepository().Add(Mapping());
        }
        #endregion
        #region Update
        private void Update()
        {
            new OtherEquipmentRepository().Update(Mapping());
        }
        private void DisplayProperties()
        {
            tbName.Text = OtherEquipment.Name;
            tgActive.Value = OtherEquipment.Active;
        }
        #endregion
        private void frmOtherEquipments_Load(object sender, EventArgs e)
        {

            switch (CRUDMode)
            {
                case clsEnums.CRUDEMode.Add:
                    SaveAction = Add; 
                    break;
                case clsEnums.CRUDEMode.Edit:
                    SaveAction = Update;
                    break;
                case clsEnums.CRUDEMode.Delete:
                    break;
                default:
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            if (tbName.Text=="")
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "All fields with * are required."
                };
                MsgBox.ShowDialog();
                tbName.Focus();
            }
            else
            {
                SaveAction.Invoke();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Success,
                    Message = "The record has been saved successfully!"
                };
                MsgBox.ShowDialog();
            }
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
                currUser.DayActivity = "Add Other Equipment (" + tbName.Text + ")";
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
                currUser.DayActivity = "Update Other Equipment (" + tbName.Text + ")";
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
