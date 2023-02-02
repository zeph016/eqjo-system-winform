using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Presentation.vActualAdvance;
using FGCIJOROSystem.Presentation.vChecklist;
using FGCIJOROSystem.Presentation.vConfiguration.AuditTrail;
using FGCIJOROSystem.Presentation.vDashboard;
using FGCIJOROSystem.Presentation.vMenus;
using FGCIJOROSystem.Presentation.vMonitoring;
using FGCIJOROSystem.Presentation.vReport.vReports;
using FGCIJOROSystem.Presentation.vSystem.vLogin;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using System.Deployment.Application;
using FGCIJOROSystem.Presentation.vConfiguration.vUsers;
using System.Threading.Tasks;
using System.Threading;
using FGCIJOROSystem.Presentation.vSystem.vMsg;

namespace FGCIJOROSystem.Presentation.vSystem.vMain
{
    public partial class frmMainWindow : Telerik.WinControls.UI.RadForm
    {
        
        #region Properties
        frmLogin LoginPage;
        public clsDataEvent DataEvent = new clsDataEvent();
        UsersLogRepository UsersLogRepo;
        public clsUsersLog currUser { get; set; }
        public clsEnums.CRUDEMode CRUDEMode { get; set; }
        UserRepository _Repo = new UserRepository();
        public string VersionLabel
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                }
                else
                {
                    var ver = Assembly.GetExecutingAssembly().GetName().Version;
                    return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                }
            }
        }
        #endregion

        #region Methods
        public frmMainWindow()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            UsersLogRepo = new UsersLogRepository();
            loadLoginPage();
            lblVersion.Text = VersionLabel;
        }
        
        void loadLoginPage()
        {
            LoginPage = new frmLogin();
            LoginPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            currUser = LoginPage.curUserLog;
            //pvMain.AddControl(pgMain, new ucNewDashboard());
            LoginPage.ShowDialog();
        }

        void DataEvent_OnDataConfirm(object obj)
        {
            DisplayUser();
            LoginPage.Dispose();
        }
        void DisplayUser()
        {
            lblCurrUser.Text = Program.CurrentUser.FullName;
            imgCurrUser.Image = ImageManager.byteArrayToImage(Program.CurrentUser.EmpPicture);
            lblPosition.Text = @"Position :   " + Program.CurrentUser.Position;
            lblUserLevel.Text = @"Access :     " + Program.CurrentUser.UserLevelName;
            //lblUsername.Text = "Username: " + Program.CurrentUser.UserName;
            lblBranch.Text = @"Branch :     " + Program.CurrentUser.Branch;
        }

        private void loadDashFirst()
        {
            InstallUpdateSyncWithInfo();
            pvMain.ShowPages(new RadPageViewPage()
            {
                Name = @"pvHome",
                Text = @"Home",
            }, new ucNewDashboard());
        }
        private void frmMainWindow_Load(object sender, EventArgs e)
        {
            HideControlsFromEncoders();
            LoadUser();
            //loadDashFirst();
            DelayExpanded();

           //LoadChangelog();

        }
        private void HideControlsFromEncoders()
        {
            if (Program.CurrentUser.UserLevelId == 3)
            {
                menuConfig.Visibility = ElementVisibility.Collapsed;
                menuHome.Visibility = ElementVisibility.Collapsed;
                rmiDashboard.Visibility = ElementVisibility.Collapsed;
                rmiConfig.Visibility = ElementVisibility.Collapsed;
            }
        }

        private void DelayExpanded()
        {
            Task.Delay(500).Wait();
            PanelExpanded();
        }

        private void PanelExpanded()
        {
            radCollapsiblePanel2.IsExpanded = false;
            radCollapsiblePanel1.IsExpanded = false;
        }

        void LoadUser()
        {
            if (Program.CurrentUser == null)
            {
                _nullCurrentUser();
            }
            else
            {
                /*pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvHome",
                    Text = "Home",
                    //Image = Properties.Resources.home
                }, new ucDashboard() { /*MainWindowPage = this });*/
            }
        }
        void _nullCurrentUser()
        {
            flowLayoutPanel1.Enabled = false;
            radCollapsiblePanel2.Visible = false;
            radStatusStrip1.Enabled = false;
            radCollapsiblePanel1.Enabled = false;
            lnkLogin.Visible = true;
        }
        clsUsersLog UpdateMapProperties()
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
            currUser.DayActivity = "Log In" + " - " + "Log Out";
            currUser.OnlineUser = false;
            return currUser;
        }
        void UpdateCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Update(UpdateMapProperties());
        }
        private void menuConfig_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            pvMain.ShowPages(new RadPageViewPage()
            {
                Name = "pvConfigMenu",
                Text = @"Configuration Menu",
            }, new ucConfigMenu() { MainWindowPage = this });
            
        }
        private async void menuChecklist_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            pvMain.ShowPages(new RadPageViewPage()
            {
                Name = "pvEquipmentTypeList",
                Text = @"Equipment Type List",
            }, await Task.Run(() => new ucEquipmentTypeLists() { MainWindowPage = this }));
        }
        private void menuJobOrder_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            pvMain.ShowPages(new RadPageViewPage()
            {
                Name = "pvJOMenu",
                Text = @"Job Order Menu",
            }, new ucJOMenu() { MainWindowPage = this });
        }
        private void menuRentalOrder_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvROMenu",
                    Text = @"Rescue Order Menu",
                }, new ucROMenu() { MainWindowPage = this });
        }
        private void menuActualAdvance_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvActualAdvance",
                    Text = @"Actual and Advance",
                }, new ucActualAdvance() { MainWindowPage = this });
        }
        private async void menuMonitoring_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            pvMain.ShowPages(new RadPageViewPage()
            {
                Name = "pvMonitoring",
                Text = @"Work Monitoring",
            }, await Task.Run(() => new ucMonitoring() { MainWindowPage = this }));
        }
        private void menuReports_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            pvMain.ShowPages(new RadPageViewPage()
            {
                Name = "pvReports",
                Text = @"Reports List",
            }, new ucReportList() { MainWindowPage = this });
        }

        #endregion       

        private void frmMainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (Program.CurrentUser == null)
            //{
            //    _nullCurrentUser();
            //}
            //else
            //{
            //    UpdateCurrentUser();
               // Environment.Exit(0);
            //}
        }
        private void frmMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //UpdateCurrentUser();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = RadMessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if(Program.CurrentUser == null)
                    {
                        _nullCurrentUser();
                    }
                    else
                    {
                        UpdateCurrentUser();
                        System.Windows.Forms.Application.Exit();
                        
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        private async void menuHome_Click(object sender, EventArgs e)
        {

            InstallUpdateSyncWithInfo();
            pvMain.ShowPages(new RadPageViewPage()
            {
                Name = @"pvHome",
                Text = @"Home",
            }, await Task.Run(() => new ucNewDashboard()));
        }
        private void radCollapsiblePanel1_Collapsed(object sender, EventArgs e)
        {
            radPanel2.Visible = true;
        }
        private void radCollapsiblePanel1_Expanding(object sender, CancelEventArgs e)
        {
            radPanel2.Visible = false;            
        }

        private void lnkUsersLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            {
                frmUsersLogList list = new frmUsersLogList();
                list.ShowDialog();
            }
        }
        private void btnUsers_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            {
                frmOnlineUsers online = new frmOnlineUsers();
                online.ShowDialog();
            }
        }
        private void btnAuditTrail_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            {
                frmAuditTrail auditTrail = new frmAuditTrail();
                auditTrail.ShowDialog();
            }
        }

        private void lnkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Restart();
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

        private void btnAbout_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            frmAboutSystem AS = new frmAboutSystem();
            AS.ShowDialog();
        }
        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available",
            //                                     MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            //if (result == DialogResult.OK)
            //{
                InstallUpdateSyncWithInfo();
            //}
            //else if(result == DialogResult.Cancel)
            //{
            //    this.Dispose();
            //}
        }
        //private void InstallUpdateSyncWithInfo()
        private void InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();
                    //info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show(@"The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show(@"Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show(@"This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {

                    if (!info.IsUpdateRequired)
                    {
                        Boolean doUpdate = true;

                        DialogResult dr = MessageBox.Show(@"An update is available. Would you like to update the application now? Note: Please save your current transaction to prevent loss of data.", "Update Available", MessageBoxButtons.YesNo);
                        if (!(dr == DialogResult.Yes))
                        {
                            doUpdate = false;
                        }
                        else
                        {
                            try
                            {
                                ad.Update();
                                MessageBox.Show(@"The application has been upgraded, and will now restart.");
                                Application.Restart();
                            }
                            catch (DeploymentDownloadException dde)
                            {
                                MessageBox.Show(@"Cannot install the latest version of the application. Please check your network connection, or try again later. Error: " + dde);
                                return;
                            }
                            //// Display a message that the app MUST reboot. Display the minimum required version.
                            //MessageBox.Show("This application has detected a mandatory update from your current " +
                            //    "version to version " + info.MinimumRequiredVersion.ToString() +
                            //    ". The application will now install the update and restart.",
                            //    "Update Available", MessageBoxButtons.OK,
                            //    MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void llChangeUserAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowEntry(clsEnums.CRUDEMode.Edit);
        }
        void ShowEntry(clsEnums.CRUDEMode action)
        {
            var form = new frmUserEntry();
            form.CRUDEMode = action;
            form._Repo = _Repo;
            form.Current = Program.CurrentUser;
            form.ShowDialog();
        }

        private void lblLogOut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = RadMessageBox.Show("Do you want to Log Out?", "Log Out", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            //DialogResult result = MessageBox.Show("Do you want to Log Out?","Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (Program.CurrentUser == null)
                {
                    _nullCurrentUser();               
                }
                else
                {
                    UpdateCurrentUser();
                    this.Dispose();
                    Application.Restart();

                    //frmLogin loggingOut = new frmLogin();
                    //loggingOut.Show();
                }
            }
            else
            {

            }
        }

        private void RadPageHome_Paint(object sender, PaintEventArgs e)
        {
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }
        private void btnChangelogs_Click(object sender, EventArgs e)
        {
            LoadChangelog();
        }

        public void LoadChangelog()
        {
            InstallUpdateSyncWithInfo();
            frmChangelogs AS = new frmChangelogs();
            AS.ShowDialog();
        }
    }
}
