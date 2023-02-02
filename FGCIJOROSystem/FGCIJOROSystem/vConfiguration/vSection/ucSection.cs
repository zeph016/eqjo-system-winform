using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.SectionJORO;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Configurations.Users;
namespace FGCIJOROSystem.Presentation.vConfiguration.vSection
{
    public partial class ucSection : UserControl
    {
        #region Properties
        SectionJORORepository SectionRepo;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion
        #region Methods
        public ucSection()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }
        private void ucSection_Load(object sender, EventArgs e)
        {
            load();
        }
        void load()
        {
            SectionRepo = new SectionJORORepository();
            clsSectionJOROBindingSource.DataSource = SectionRepo.GetAllSections();
        }
        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            frmSection EntryPage = new frmSection();
            if ( EntryPage.ShowDialog() == DialogResult.OK)
            {
                load();
            }
           
        }
        #endregion

        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            frmSection EntryPage = new frmSection((clsSectionJORO)this.clsSectionJOROBindingSource.Current);
            if (EntryPage.ShowDialog() == DialogResult.OK)
            {
                load();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

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
            SectionRepo = new SectionJORORepository();
            SectionRepo.Delete((clsSectionJORO)clsSectionJOROBindingSource.Current);
            clsSectionJORO curItem = (clsSectionJORO)clsSectionJOROBindingSource.Current;
            curItem.Active = false;
            //clsSectionJOROBindingSource.ResetCurrentItem();
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
            currUser.DayActivity = "Deactivate - " + ((clsSectionJORO)clsSectionJOROBindingSource.Current).SectionName;
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
    }
}
