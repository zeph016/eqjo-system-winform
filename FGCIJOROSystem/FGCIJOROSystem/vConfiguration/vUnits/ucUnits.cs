using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.Configurations.UnitsJORO;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Configurations.Users;
namespace FGCIJOROSystem.Presentation.vConfiguration.vUnits
{
    public partial class ucUnits : UserControl
    {
        public ucUnits()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }
        #region Properties
        List<clsUnitJORO> ListOfUnitJORO;
        clsEnums.CRUDEMode CRUD;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        UnitsJORORepository UnitsRepo;
        #endregion

        #region Get All
        private void GetAll()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (s, e) =>
            {
                ListOfUnitJORO = new List<clsUnitJORO>();
                ListOfUnitJORO = new UnitsJORORepository().GetAll();
            };
            bw.RunWorkerCompleted += (s, e) =>
            {
                clsUnitJOROBindingSource.DataSource = ListOfUnitJORO;
            };
            bw.RunWorkerAsync();
        }
        #endregion

        private void ucUnits_Load(object sender, EventArgs e)
        {
            GetAll();
        }

        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            if (new frmUnit((clsUnitJORO)clsUnitJOROBindingSource.Current).ShowDialog() == DialogResult.OK)
            {
                GetAll();
            }
        }

        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            if (new frmUnit().ShowDialog() == DialogResult.OK)
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
            UnitsRepo = new UnitsJORORepository();
            UnitsRepo.Delete((clsUnitJORO)clsUnitJOROBindingSource.Current);
            clsUnitJORO curItem = (clsUnitJORO)clsUnitJOROBindingSource.Current;
            curItem.IsActive = false;
            clsUnitJOROBindingSource.ResetCurrentItem();
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
            currUser.DayActivity = "Deactivate Unit - " + ((clsUnitJORO)clsUnitJOROBindingSource.Current).UnitName;
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
    }
}
