using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FGCIJOROSystem.Presentation.vSystem.vMain
{
    public partial class frmAdmin : Telerik.WinControls.UI.ShapedForm
    {
        #region Properties
        UsersLogRepository UsersLogRepo;
        public clsUsersLog currUser { get; set; }
        #endregion
        public frmAdmin()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetOnlineStatus();
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "Your account in another computer has been forcibly closed. Please try to log in again."
            };
            MsgBox.ShowDialog();
            this.Close();
        }
        clsUsersLog AddMapProperties()
        {
            currUser.Username = Program.CurrentUser.UserName;
            currUser.MLEmployeeId = Program.CurrentUser.MLEmployeeId;
            currUser.EmpName = Program.CurrentUser.FullName;
            currUser.BranchId = Program.CurrentUser.BranchId;
            currUser.UserLevelId = Program.CurrentUser.UserLevelId;
            currUser.ComputerName = System.Environment.MachineName;
            currUser.TimeLogin = System.DateTime.Now;
            currUser.TimeLogout = System.DateTime.Now;
            currUser.DayActivity = "Reset Online Status";
            currUser.OnlineUser = false;
            return currUser;
        }
        void ResetOnlineStatus()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.RESET(AddMapProperties());
        }
    }
}
