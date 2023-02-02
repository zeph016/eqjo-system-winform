using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.Configurations.Status;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Configurations.Users;
namespace FGCIJOROSystem.Presentation.vConfiguration.vStatus
{
    public partial class ucStatus : UserControl
    {
        public ucStatus()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }
        #region Properties
        List<clsStatus> ListOfStatus;
        public clsStatus Status;
        StatusRepository StatusRepo;
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion

        #region methods
        private void ucStatus_Load(object sender, EventArgs e)
        {
            load();
        }
        void load()
        {
            ListOfStatus = new List<clsStatus>();
            ListOfStatus = new StatusRepository().GetAllStatus();
            clsStatusBindingSource.DataSource = ListOfStatus;

            //StatusRepo = new StatusRepository();
            //clsStatusBindingSource.DataSource = StatusRepo.GetAll();
        }
        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            frmStatus StatusEntryPage = new frmStatus();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
            StatusEntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            StatusEntryPage.ShowDialog();
        }
        private void radBindingNavigator1DeleteItem_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            StatusRepo = new StatusRepository();
            StatusRepo.Delete((clsStatus)clsStatusBindingSource.Current);
            clsStatus curStatus = (clsStatus)clsStatusBindingSource.Current;
            curStatus.Active = false;
            clsStatusBindingSource.ResetCurrentItem();
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been deactivated successfully!"
            };
            MsgBox.ShowDialog();
        }

        void DataEvent_OnDataConfirm(object obj)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                clsStatusBindingSource.Add((clsStatus)obj);
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                clsStatusBindingSource.ResetCurrentItem();
                clsStatusBindingSource.EndEdit();
            }
        }
        private void MasterTemplate_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnUpdate")
            {
                frmStatus StatusEntryPage = new frmStatus((clsStatus)clsStatusBindingSource.Current);
                CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
                StatusEntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
                StatusEntryPage.ShowDialog();
            }
        }
        #endregion        

        private void dgvStatus_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "btnUpdate")
            {
                dgvStatus.Grid_CellFormatting(sender, e, "Update");
            }
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
            currUser.DayActivity = "Deactivate Status - " + ((clsStatus)clsStatusBindingSource.Current).StatusName;
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
    }
}
