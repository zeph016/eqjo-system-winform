using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.Configurations.JobType;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Configurations.Users;
namespace FGCIJOROSystem.Presentation.vConfiguration.vJobTypes
{
    public partial class ucJobTypes : UserControl
    {
        public ucJobTypes()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }
        #region Properties
        List<clsJobType> ListOfJobType;
        JobTypeRepository JobTypeRepo;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion

        #region Get All
        private void GetAll()
        {
            ListOfJobType = new List<clsJobType>();
            ListOfJobType = new JobTypeRepository().GetAll();
            clsJobTypeBindingSource.DataSource = ListOfJobType;
        }
        #endregion

        private void ucJobTypes_Load(object sender, EventArgs e)
        {
            GetAll();
        }

        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            if (new frmJobTypeEntry(clsEnums.CRUDEMode.Add).ShowDialog() == DialogResult.OK)
            {
                GetAll();
            }
        }

        private void MasterTemplate_CommandCellClick(object sender, EventArgs e)
        {
            if (new frmJobTypeEntry((clsJobType)clsJobTypeBindingSource.Current 
                ,clsEnums.CRUDEMode.Edit).ShowDialog() == DialogResult.OK)
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
            JobTypeRepo = new JobTypeRepository();
            JobTypeRepo.Delete((clsJobType)clsJobTypeBindingSource.Current);
            clsJobType curJobType = (clsJobType)clsJobTypeBindingSource.Current;
            curJobType.IsActive = false;
            clsJobTypeBindingSource.ResetCurrentItem();
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
            currUser.DayActivity = "Deactivate - " + ((clsJobType)clsJobTypeBindingSource.Current).JobTypeName;
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
    }
}
