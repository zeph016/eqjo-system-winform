using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.Configurations.Tariff;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Domain.Configurations.Users;
namespace FGCIJOROSystem.Presentation.vConfiguration.vTariff
{
    public partial class ucTariff : UserControl
    {
        public ucTariff()
        {
            InitializeComponent();
            TariffRepo = new TariffRepository();
            currUser = new clsUsersLog();

            disableColumnEdits();
        }
        #region Properties
        TariffRepository TariffRepo;
        List<clsTariff> ListOfTariff;
        SectionJORORepository SectionsRepo;
        frmSelectSection _selectSection;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion

        #region GetAll
        private void GetAll()
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                TariffRepo = new TariffRepository();
                e.Result = TariffRepo.GetAll();
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsTariffBindingSource.DataSource = (List<clsTariff>)e.Result;
            };
            bg.RunWorkerAsync();

            //ListOfTariff = new List<clsTariff>();
            //ListOfTariff = new TariffRepository().GetAll();
            //clsTariffBindingSource.DataSource = ListOfTariff;
            //radGridView1.BestFitColumns();
        }
        #endregion
        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            frmTariff tariff = new frmTariff();
            tariff.ShowDialog();
            //if (new frmTariff().ShowDialog() == DialogResult.OK)
            //{
            //    //GetAll();
            //}
        }

        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            if (new frmTariff((clsTariff)this.clsTariffBindingSource.Current).ShowDialog() == DialogResult.OK)
            {
                //GetAll();
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
            TariffRepo = new TariffRepository();
            TariffRepo.Delete((clsTariff)clsTariffBindingSource.Current);
            clsTariff curStatus = (clsTariff)clsTariffBindingSource.Current;
            curStatus.Active = false;
            clsTariffBindingSource.ResetCurrentItem();
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been deactivated successfully!"
            };
            MsgBox.ShowDialog();
        }

        private void radBindingNavigator1Search_Click(object sender, EventArgs e)
        {
            frmSelectSection _selectSection = new frmSelectSection();
            _selectSection.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            _selectSection.ShowDialog();
        }
        async void DataEvent_OnDataConfirm(object obj)
        {
            pbLoading.Visible = true;
            pbLoading.BringToFront();
            if (obj != null)
            {
                ListOfTariff = (List<clsTariff>)obj;
                clsTariffBindingSource.DataSource = await Task.Run(() => ListOfTariff);
                clsTariffBindingSource.ResetCurrentItem();
            }

            if (_selectSection != null)
            {
                _selectSection.Dispose();
            }
            else
            {
                
            }

            int cn = radGridView1.RowCount; //
            txtRowCount.Text = "Number of items:  " + cn.ToString(); //

            pbLoading.Visible = false;
            pbLoading.SendToBack();

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
            currUser.DayActivity = "Deactivate Tariff - " + ((clsTariff)clsTariffBindingSource.Current).WorkDescription;
            return currUser;
        }

        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }

        void disableColumnEdits()
        {
            this.radGridView1.Columns["Name"].ReadOnly = true;
            this.radGridView1.Columns["UnitName"].ReadOnly = true;
            this.radGridView1.Columns["WorkDescription"].ReadOnly = true;
            this.radGridView1.Columns["NoOfMechanics"].ReadOnly = true;
            this.radGridView1.Columns["Price"].ReadOnly = true;
            this.radGridView1.Columns["WorkTimeSpan"].ReadOnly = true;
            this.radGridView1.Columns["JobCategoryName"].ReadOnly = true;
            this.radGridView1.Columns["JobTypeName"].ReadOnly = true;
            this.radGridView1.Columns["SectionName"].ReadOnly = true;
            this.radGridView1.Columns["Active"].ReadOnly = true;
        }
    }
}