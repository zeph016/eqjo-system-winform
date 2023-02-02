using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;

namespace FGCIJOROSystem.Presentation.vSystem.vMain
{
    public partial class frmUsersLogList : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        UsersLogRepository ULRepo;
        public List<clsUsersLog> ListOfUsersLog { get; set; }
        #endregion
        #region Method
        public frmUsersLogList()
        {
            InitializeComponent();
        }
        void load()
        {
            ULRepo = new UsersLogRepository();
            if (chkDate.Checked)
            {
                clsUsersLogBindingSource.DataSource = ULRepo.GetAllLogs(dtFromList.Value.Date, dtToList.Value.Date);
            }
            else
            {
                clsUsersLogBindingSource.DataSource = ULRepo.GetAll();
            }
            recordCount();
        }
        #endregion

        private void frmUsersLogList_Load(object sender, EventArgs e)
        {
            dtFromList.Value = DateTime.Now;
            dtToList.Value = DateTime.Now;

            ULRepo = new UsersLogRepository();
            clsUsersLogBindingSource.DataSource = ULRepo.GetAll();
        }
        private List<clsUsersLog> FilterSectionReportList(List<clsUsersLog> list)
        {
            list = list.Where(item => item.TimeLogin.Date >= dtFromList .Value.Date && item.TimeLogin.Date <= dtToList.Value.Date).ToList();
            return list;
        }

        private void chkDate_CheckStateChanged(object sender, EventArgs e)
        {
            dtFromList.Enabled = chkDate.Checked;
            dtToList.Enabled = chkDate.Checked;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            load();
        }
        void recordCount()
        {
            int cn = radGridView1.RowCount;
            lblCount.Text = "Total Record Found: (" + cn.ToString() + ")";
        }

        private void radLabel1_Click(object sender, EventArgs e)
        {

        }

        private void lblCount_Click(object sender, EventArgs e)
        {

        }
    }
}
