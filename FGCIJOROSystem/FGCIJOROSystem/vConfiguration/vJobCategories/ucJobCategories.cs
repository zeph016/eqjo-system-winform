using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.Configurations.JobCategories;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Configurations.Users;

namespace FGCIJOROSystem.Presentation.vConfiguration.vJobCategories
{
    public partial class ucJobCategories : UserControl
    {
        public ucJobCategories()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }
        #region Properties
        List<clsJobCategory> ListOfJobCategory;
        JobCategoryRepository JCRepo;
        clsUsersLog currUser;
        UsersLogRepository UsersLogRepo;
        #endregion

        #region Get All Job Category
        private void GetAll()
        {
            ListOfJobCategory = new List<clsJobCategory>();
            ListOfJobCategory = new JobCategoryRepository().GetAll();
            clsJobCategoryBindingSource.DataSource = ListOfJobCategory;
            radGridView1.BestFitColumns();
        }
        #endregion
        private void ucJobCategories_Load(object sender, EventArgs e)
        {
            GetAll();
        }

        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            if (new frmJobCategoryEntry(clsEnums.CRUDEMode.Add).ShowDialog() == DialogResult.OK)
            {
                GetAll();
            }
        }

        private void MasterTemplate_CommandCellClick(object sender, EventArgs e)
        {
            if (new frmJobCategoryEntry(clsEnums.CRUDEMode.Edit, (clsJobCategory)this.clsJobCategoryBindingSource.Current).ShowDialog() == DialogResult.OK)
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
            JCRepo = new JobCategoryRepository();
            JCRepo.Delete((clsJobCategory)clsJobCategoryBindingSource.Current);
            clsJobCategory curItem = (clsJobCategory)clsJobCategoryBindingSource.Current;
            curItem.IsActive = false;
            clsJobCategoryBindingSource.ResetCurrentItem();
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
            currUser.DayActivity = "Deactivate - " + ((clsJobCategory)clsJobCategoryBindingSource.Current).JobCategoryName;
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
    }
}
