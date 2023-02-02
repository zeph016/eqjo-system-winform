using FGCIJOROSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.Domain.Global;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Users;
namespace FGCIJOROSystem.Presentation.vChecklist
{
    public partial class frmChecklistItem : Telerik.WinControls.UI.RadForm
    {

        #region Properties
        public clsDataEvent DataEvent;
        ChecklistItemRepository ChecklistItemRepo;
        clsChecklistItem ChecklistItem;
        Action SaveAction;
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        clsChecklistItem curChecklistItem;
        #endregion
        public frmChecklistItem()
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
            currUser = new clsUsersLog();
        }

        public frmChecklistItem(clsChecklistItem obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new clsDataEvent();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
            ChecklistItem = obj;
            currUser = new clsUsersLog();
        }
        #region Load
        private void frmAttendanceGroup_Load(object sender, EventArgs e)
        {
            tbName.Focus();
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                ChecklistItem = new clsChecklistItem();
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
        void load()
        {
            ChecklistItemRepo = new ChecklistItemRepository();
            clsChecklistItemBindingSource.DataSource = ChecklistItemRepo.GetAll();
        }
        void Add()
        {
            ChecklistItemRepo = new ChecklistItemRepository();
            ChecklistItemRepo.Add(MapProperties());
        }
        void Edit()
        {
            ChecklistItemRepo = new ChecklistItemRepository();
            ChecklistItemRepo.Update(MapProperties());
        }
        void DisplayProperties()
        {
            tbName.Text = ChecklistItem.Name;
            tbDescription.Text = ChecklistItem.Description;
            tgActive.Value = ChecklistItem.Active;
        }
        clsChecklistItem MapProperties()
        {
            ChecklistItem.Name = tbName.Text;
            ChecklistItem.Description = tbDescription.Text;
            ChecklistItem.Active = tgActive.Value;
            return ChecklistItem;
        }       
        #endregion
        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            Save();
            ClearProperty();
            tbName.Focus();
        }
        void Save()
        {
            if (tbName.Text == "")
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "All fields with *** are required."
                };
                MsgBox.ShowDialog();
                tbName.Focus();
            }
            else
            {
                if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
                {
                    SaveAction.Invoke();
                    clsChecklistItemBindingSource.Add(ChecklistItem);
                    ClearProperty();
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
                    clsChecklistItemBindingSource.ResetCurrentItem();
                    ClearProperty();
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "The record has been saved successfully."
                    };
                    MsgBox.ShowDialog();
                }
                load();
                //DataEvent.ConfirmData(MapProperties());
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
                currUser.TimeLogin = System.DateTime.Now;
                currUser.TimeLogout = System.DateTime.Now;
                currUser.OnlineUser = false;
                currUser.DayActivity = "Add Checklist Item (" + tbName.Text + ")";
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
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
                currUser.DayActivity = "Update Checklist Item (" + tbName.Text + ")";
            }
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        private void MasterTemplate_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnUpdate")
            {
                ChecklistItem = (clsChecklistItem)clsChecklistItemBindingSource.Current;
                DisplayProperties();
                SaveAction = Edit;
                CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
                tbName.Focus();
            }
        }
        void DataEvent_OnDataConfirm(object obj)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                clsChecklistItemBindingSource.Add((ChecklistItemRepository)obj);
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                clsChecklistItemBindingSource.ResetCurrentItem();
            }
        }
        #endregion
        #region ClearProperty
        void ClearProperty()
        {
            tbName.Text = "";
            tbDescription.Text = "";
            tgActive.Value = true;
        }       
        #endregion
        #region Cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearProperty();
        }
        #endregion
        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            if (cell.ColumnInfo.Name == "btnUpdate")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Update");
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            load();
        }
    }
}
