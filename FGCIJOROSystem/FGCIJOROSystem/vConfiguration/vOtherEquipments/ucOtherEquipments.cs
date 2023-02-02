using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.Configurations.OtherEquipment;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Configurations.Users;
namespace FGCIJOROSystem.Presentation.vConfiguration.vOtherEquipments
{
    public partial class ucOtherEquipments : UserControl
    {
        public ucOtherEquipments()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }
        #region Properties
        List<clsOtherEquipment> ListOfOtherEquipment;
        OtherEquipmentRepository OtherEquipmentRepo;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion

        #region Get All
        private void GetAll()
        {
            ListOfOtherEquipment = new List<clsOtherEquipment>();
            ListOfOtherEquipment = new OtherEquipmentRepository().GetAll();
            clsOtherEquipmentBindingSource.DataSource = ListOfOtherEquipment;
            //radGridView1.BestFitColumns();
        }
        #endregion

        private void ucOtherEquipments_Load(object sender, EventArgs e)
        {
            GetAll();
        }

        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            if (new frmOtherEquipments().ShowDialog() == DialogResult.OK)
            {
                GetAll();
            }
        }

        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            if (new frmOtherEquipments((clsOtherEquipment)clsOtherEquipmentBindingSource.Current).ShowDialog() == DialogResult.OK)

            {
                GetAll();
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
            AddCurrentUser();
            OtherEquipmentRepo = new OtherEquipmentRepository();
            OtherEquipmentRepo.Delete((clsOtherEquipment)clsOtherEquipmentBindingSource.Current);
            clsOtherEquipment curItem = (clsOtherEquipment)clsOtherEquipmentBindingSource.Current;
            curItem.Active = false;
            clsOtherEquipmentBindingSource.ResetCurrentItem();
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
            currUser.DayActivity = "Deactivate - " + ((clsOtherEquipment)clsOtherEquipmentBindingSource.Current).Name;
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
    }
}
