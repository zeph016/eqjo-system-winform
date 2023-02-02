using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation.vSystem.vMain
{
    public partial class frmOnlineUsers : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        UsersLogRepository UsersLogRepo;
        #endregion
        public frmOnlineUsers()
        {
            InitializeComponent();
        }

        private void frmOnlineUsers_Load(object sender, EventArgs e)
        {
            try
            {
                LoadEvent();
            }
            catch (Exception)
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "The network connection was lost.#6257"
                };
                MsgBox.ShowDialog();
            }
        }
        void LoadEvent()
        {
            UsersLogRepo = new UsersLogRepository();
            clsUsersLogBindingSource.DataSource = UsersLogRepo.GetAllOnline();
            this.radGridView1.MasterTemplate.Refresh(null);

            RadToolTip Tip = new RadToolTip();
            Tip.SetToolTip(btnRefresh, "Refresh");
            Tip.SetToolTip(btnClose, "Close");
            Tip.SetToolTip(btnLogs, "Users Logs");
            recordCount();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadEvent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            //if (Program.CurrentUser.UserLevelId != 1)
            //{
            //    frmMsg MsgBox = new frmMsg()
            //    {
            //        MsgBox = clsEnums.MsgBox.Warning,
            //        Message = "Sorry, you are not allowed to go with this form."
            //    };
            //    MsgBox.ShowDialog();
            //}
            //else
            //{
                frmUsersLogList Logs = new frmUsersLogList();
                Logs.ShowDialog();
            //}
        }
        void recordCount()
        {
            int cn = radGridView1.RowCount;
            lblCount.Text = "Total Record Found: (" + cn.ToString() + ")";
        }

        private void btnBroadcast_Click(object sender, EventArgs e)
        {

        }
    }
}
