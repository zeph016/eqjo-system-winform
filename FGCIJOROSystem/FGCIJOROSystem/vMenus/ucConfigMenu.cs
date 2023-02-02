using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMain;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Presentation.vConfiguration.vUsers;
using FGCIJOROSystem.Presentation.vConfiguration.vAttendance;
using FGCIJOROSystem.Presentation.vConfiguration.vStatus;
using FGCIJOROSystem.Presentation.vConfiguration.vBranches;
using FGCIJOROSystem.Presentation.vConfiguration.vSection;
using FGCIJOROSystem.Presentation.vConfiguration.vCustomers;
using FGCIJOROSystem.Presentation.vConfiguration.vJobTypes;
using FGCIJOROSystem.Presentation.vConfiguration.vJobCategories;
using FGCIJOROSystem.Presentation.vConfiguration.vTariff;
using FGCIJOROSystem.Presentation.vConfiguration.vContractors;
using FGCIJOROSystem.Presentation.vConfiguration.vOtherEquipments;
using FGCIJOROSystem.Presentation.vConfiguration.vUnits;
using FGCIJOROSystem.Presentation.vConfiguration.vUserAccess;
using FGCIJOROSystem.Presentation.vConfiguration.AuditTrail;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;

namespace FGCIJOROSystem.Presentation.vMenus
{
    public partial class ucConfigMenu : UserControl
    {
        public ucConfigMenu()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }
        #region Properties
        public frmMainWindow MainWindowPage;
        public clsUsersLog currUser { get; set; }
        UserRepository _Repo = new UserRepository();
        #endregion
        #region Methods
        private void radTileElement1_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvAttendanceGroup",
                    Text = "Attendance Group",
                    //Image = Properties.Resources.home
                }, new ucAttendanceGroup());
            }
        }
        private void mBranches_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvBranches",
                    Text = "Branches",
                    //Image = Properties.Resources.home
                }, new ucBranches());
            }
        }
        private async void mUser_Click(object sender, EventArgs e) //async
        {
            if (Program.CurrentUser.UserLevelId == 5)
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvUsers",
                    Text = "Users",
                    //Image = Properties.Resources.home
                }, new ucUsers());
            }
            else if (Program.CurrentUser.UserLevelId != 5)
            {
                await Task.Run(() => ShowEntry(clsEnums.CRUDEMode.Edit)); //async
            }
            else
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Sorry, this transaction is prohibited."
                };
                MsgBox.ShowDialog();
            }
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
        }
        void ShowEntry(clsEnums.CRUDEMode action)
        {
            var form = new frmUserEntry();
            form.CRUDEMode = action;
            form._Repo = _Repo;
            form.Current = Program.CurrentUser;
            form.ShowDialog();
        }
        private void mSection_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvSection",
                    Text = "Section",
                    //Image = Properties.Resources.home
                }, new ucSection());
            }
        }
        private void mStatus_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvStatus",
                    Text = "Status",
                    //Image = Properties.Resources.home
                }, new ucStatus());
            }
        }
        #endregion 

        private void mExternalCostomers_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvCustomers",
                    Text = "Customers",
                    //Image = Properties.Resources.home
                }, new ucCustomers());
            }
        }

        private void mJobType_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvJobType",
                    Text = "Job Type",
                    //Image = Properties.Resources.home
                }, new ucJobTypes());
            }
        }

        private void radTileElement6_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvJobCategory",
                    Text = "Job Category",
                    //Image = Properties.Resources.home
                }, new ucJobCategories());
            }
        }

        private void mTariff_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvTariff",
                    Text = "Tariff",
                    //Image = Properties.Resources.home
                }, new ucTariff());
            }
        }

        private void mExternalContractor_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvContractor",
                    Text = "Contractor",
                    //Image = Properties.Resources.home
                }, new ucContrator());
            }
        }

        private void mOtherEquipment_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvOtherEquipment",
                    Text = "Other Equipments",
                    //Image = Properties.Resources.home
                }, new ucOtherEquipments());
            }
        }

        private void mUnit_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvUnits",
                    Text = "Units",
                    //Image = Properties.Resources.home
                }, new ucUnits());
            }
        }

        private void radTileElement2_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvAttendanceStatus",
                    Text = "Attendance Status",
                    //Image = Properties.Resources.home
                }, new ucAttendanceStatus());
            }
        }

        private void mUserAccess_Click(object sender, EventArgs e)
        {
            if (Program.CurrentUser.UserLevelId == 5)
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvUserAccess",
                    Text = "User Access",
                    //Image = Properties.Resources.home
                }, new ucUserAccess());
            }
            else
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Sorry, this transaction is prohibited."
                };
                MsgBox.ShowDialog();
            }
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
        }

        private void mUserLevel_Click(object sender, EventArgs e)
        {
            if (Program.CurrentUser.UserLevelId == 5)
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvUserAccesslLevel",
                    Text = "User Access Level",
                    //Image = Properties.Resources.home
                }, new ucUserLevel());
            }
            else
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Sorry, this transaction is prohibited."
                };
                MsgBox.ShowDialog();
            }
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
        }

        private void btnAuditTrail_Click(object sender, EventArgs e)
        {
            //if (!IsOnline())
            //{
            //    DialogResult result = MessageBox.Show("Sorry, this account has been blocked. Please call system administrator for more info.", "Warning",
            //                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //}
            //else
            {
                frmAuditTrail AT = new frmAuditTrail();
                AT.ShowDialog();
            }
        }
        private bool IsOnline()
        {
            clsUsersLog currUser = new clsUsersLog();
            currUser = new UsersLogRepository().IsOnlineUser((string)Program.CurrentUser.UserName);
            if (currUser == null)
            {
                return false;
            }
            if (currUser.OnlineUser == true)
            {
                return true;
            }
            return false;
        }
    }
}
