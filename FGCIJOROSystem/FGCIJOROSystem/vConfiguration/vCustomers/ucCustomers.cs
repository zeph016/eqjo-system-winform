using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.Configurations.Customers;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Configurations.Users;
namespace FGCIJOROSystem.Presentation.vConfiguration.vCustomers
{
    public partial class ucCustomers : UserControl
    {
        public ucCustomers()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }
        public
        #region Properties && Variables
         List<clsCustomer> ListOfCustomer;
        private CustomerRepository CustomerRepo;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion

        #region Get All Customers
        private void GetAll()
        {
            ListOfCustomer = new List<clsCustomer>();
            ListOfCustomer = new CustomerRepository().GetAll();
            clsCustomerBindingSource.DataSource = ListOfCustomer.OrderBy(x => x.CustomerName);
        }
        #endregion

        private void ucCustomers_Load(object sender, EventArgs e)
        {
            GetAll();
        }

        private void radGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.Middle:
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    //this.contextMenuStrip1.Show(radGridView1, e.Location);
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    break;
            }
        }

        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            if (new frmCustomerEntry().ShowDialog() == DialogResult.OK)
            {
                this.GetAll();
            }
        }
        private void MasterTemplate_CommandCellClick(object sender, EventArgs e)
        {
            if (new frmCustomerEntry(Domain.Enums.clsEnums.CRUDEMode.Edit
                , (clsCustomer)clsCustomerBindingSource.Current).ShowDialog() == DialogResult.OK)
            {
                this.GetAll();
            }
        }

        #region DeleteItem
        private void radBindingNavigator1DeleteItem_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            CustomerRepo = new CustomerRepository();
            CustomerRepo.Delete((clsCustomer)clsCustomerBindingSource.Current);
            clsCustomer curCustomer = (clsCustomer)clsCustomerBindingSource.Current;
            curCustomer.Active = false;
            clsCustomerBindingSource.ResetCurrentItem();
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been deactivated successfully!"
            };
            MsgBox.ShowDialog();
        }
        #endregion

        private void radGridView1_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "btnUpdate")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Update");
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
            currUser.DayActivity = "Deactivate - " + ((clsCustomer)clsCustomerBindingSource.Current).CustomerName;
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
    }
}