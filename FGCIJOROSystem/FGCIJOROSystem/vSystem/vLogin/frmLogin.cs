using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Presentation.vLookups;
using FGCIJOROSystem.Presentation.vSystem.vMain;
using System;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using System.Reflection;
using System.Deployment.Application;

namespace FGCIJOROSystem.Presentation.vSystem.vLogin
{
    public partial class frmLogin : Telerik.WinControls.UI.RadForm
    {
        public frmLogin()
        {
            InitializeComponent();
            curUserLog = new clsUsersLog();
            DataEvent = new Common.clsDataEvent();
            lblVersion.Text = VersionLabel;
        }
        #region Properties
        public clsDataEvent DataEvent = new clsDataEvent();
        public UserRepository _Repo;
        clsUser _curUser;
        long regId;
        public clsUsersLog curUserLog;
        public bool OnlineUser { get; set; }
        public clsUser Current { get; set; }
        public FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode { get; set; }
        public bool authorizedLogIn { get; set; }
        frmMainWindow _mainPage;
        BranchRepository BranchRepo;
        UsersLogRepository UsersLogRepo;
        #endregion

        #region LoginPage
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //pleaseWaitPage();
            canLogin();
            if (authorizedLogIn == true)
            {
                this.Close();
            }
            else
            {
                
            }
        }
        void pleaseWaitPage()
        {
            frmMsg pleaseWait=new frmMsg ();

            // Display form modelessly
            pleaseWait.Show();

            //  ALlow main UI thread to properly display please wait form.
            Application.DoEvents();

            // Show or load the main form.
            canLogin();
        }
        void canLogin()
        {
            //InstallUpdateSyncWithInfo();
            if (tbUsername.Text == "" || tbPassword.Text == "" )
            {
                authorizedLogIn = false;
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "All fields are required!"
                };
                MsgBox.ShowDialog();
            }
            else
            {
                _Repo = new UserRepository();
                Program.CurrentUser = _Repo.SearchBy("WHERE UserName = '" + tbUsername.Text + "'").FirstOrDefault();
                if (Program.CurrentUser == null)
                {

                    if (tbUsername.Text == "admin" && tbPassword.Text == "admin")
                    {
                        //DataEvent.ConfirmData(); // removed admin-admin confirmation - redirected to below
                    }
                    else
                    {
                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Warning,
                            Message = "Your account does not exist, please contact system administrator for assistance."
                        };
                        MsgBox.ShowDialog();
                        authorizedLogIn = false;
                    }
                }
                else
                {
                    string decryptedPassword = EncryptorManager.DecryptPassword(Program.CurrentUser.UserPassword);
                    if (Program.CurrentUser.Active && tbPassword.Text.Trim() == decryptedPassword)
                    {
                        DataEvent.ConfirmData();
                        AddCurrentUser();
                        authorizedLogIn = true;
                        //this.Close();
                    }
                    else
                    {

                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Warning,
                            Message = "Your account does not exist, please contact system administrator for assistance."
                        };
                        authorizedLogIn = false;
                        MsgBox.ShowDialog();
                        //Application.Restart();
                    }
                }
            }
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            curUserLog.Id = UsersLogRepo.Add(AddMapProperties());
        }
        clsUsersLog AddMapProperties()
        {
            curUserLog.Username = Program.CurrentUser.UserName;
            curUserLog.MLEmployeeId = Program.CurrentUser.MLEmployeeId;
            curUserLog.EmpName = Program.CurrentUser.FullName;
            curUserLog.BranchId = Program.CurrentUser.BranchId;
            curUserLog.UserLevelId = Program.CurrentUser.UserLevelId;
            curUserLog.ComputerName = System.Environment.MachineName;
            curUserLog.DateLogin = System.DateTime.Now;
            curUserLog.TimeLogin = System.DateTime.Now;
            curUserLog.DateLogout = System.DateTime.Now;
            curUserLog.TimeLogout = System.DateTime.Now;
            curUserLog.DayActivity = "Log In";
            curUserLog.OnlineUser = true;
            return curUserLog;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            //Application.Exit();
        }
        #region Registration
        private void btnSearch_Click(object sender, EventArgs e)
        {
                clickSearch();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                if (!IsDuplicate()) 
	            {
                    if (!IsDuplicateId())
                    {
                    Save();
                        
                    }
                    else
                    {
                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Warning,
                            Message = "Sorry, this account already exist. \n\nPlease contact the administrator for assistance."
                        };
                        MsgBox.ShowDialog();
                    }
	            }
                else
                {
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "Sorry, " + tbFullName.Text + " your username ( " + tbRegUsername.Text + " ) already exist. \n\nPlease try a new one."
                    };
                    MsgBox.ShowDialog();
                }
            }
            
        }
        void DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {

                clsEmployee Employee = (clsEmployee)obj;
                regId = Employee.Id;
                tbFullName.Text = Employee.FullName;
                tbPosition.Text = Employee.Position;
                tbSection.Text = Employee.Section;
                tbDepartment.Text = Employee.Department;
                tbDateHired.Text = Employee.DateHired.ToString();
                tbEmployeeStatus.Text = Employee.EmployeeStatus;
                SetPicture(Employee);
            }
         //   _mainPage.Dispose(); -- for review code
        }
        bool Validation()
        {
            if (regId == 0)
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Please select an employee to register."
                };
                MsgBox.ShowDialog();
                return false;
            }
            if (String.IsNullOrWhiteSpace(tbRegUsername.Text) || String.IsNullOrWhiteSpace(tbRegConPassword.Text))
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "All fields are required!"
                };
                MsgBox.ShowDialog();
                return false;
            }
            else if (cboBranch.SelectedValue == null)
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Please select branch to work with."
                };
                MsgBox.ShowDialog();
                return false;

            }
            return true;
        }
        void Save()
        {
            try
            {                
                switch (CRUDEMode)
                {
                    case clsEnums.CRUDEMode.Add:
                        Add();
                        break;
                    case clsEnums.CRUDEMode.Edit:
                        Update();
                        break;
                    case clsEnums.CRUDEMode.Delete:
                        break;
                    default:
                        break;
                }                
            }
            catch (Exception ex)
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Error,
                    Message = ex.Message
                };
                MsgBox.ShowDialog();
                return;
            }
        }
        void Add()
        {
                _curUser = new clsUser();
                _curUser.FullName = tbFullName.Text;
                _curUser.Position = tbPosition.Text;
                _curUser.Section = tbSection.Text;
                _curUser.Department = tbDepartment.Text;
                _curUser.DateHired = DateTime.Parse(tbDateHired.Text);
                _curUser.EmployeeStatus = tbEmployeeStatus.Text;

                _curUser.UserName = tbRegUsername.Text;
                _curUser.UserPassword = tbRegConPassword.Text.Trim();
                _curUser.MLEmployeeId = regId;
                _curUser.UserLevelId = 4;
                _curUser.Active = false;

                _curUser.BranchId = Int32.Parse(cboBranch.SelectedValue.ToString());
                _curUser.Branch = cboBranch.Text.ToString();     
                new UserRepository().Add(_curUser);
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Success,
                    Message = "Congratulations! " + _curUser.FullName + ", your system account has been successfully added. \n\nPlease wait for the administrator to activate your account!"
                };
                MsgBox.ShowDialog();
                clearProperties();
                pvMainLogin.SelectedPage = pgLogin;
        }
        void Cancel()
        {
            var confirm = RadMessageBox.Show(this, "Close entry?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            if (confirm == System.Windows.Forms.DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        void SetPicture(clsEmployee emp)
        {
            byte[] picByte = new EmployeeRepository().GetPictureById(emp.Id); // Current.MLEmployeeId
            imgUser.BackgroundImageLayout = ImageLayout.Stretch;
            if (picByte != null)
            {
                imgUser.BackgroundImage = ImageManager.byteArrayToImage(picByte);
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pvMainLogin.SelectedPage = pgRegistry;
        }

        private void lnkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pvMainLogin.SelectedPage = pgLogin;
        }
        void clearProperties()
        {
            tbFullName.Text = "";
            tbPosition.Text = "";
            tbSection.Text = "";
            tbDepartment.Text = "";
            tbDateHired.Text = "";
            tbEmployeeStatus.Text = "";
            tbRegUsername.Text = "";
            tbRegPassword.Text = "";
            tbRegConPassword.Text = "";
        }
        void loadMainPage()
        {
            _mainPage = new frmMainWindow();
            _mainPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            _mainPage.ShowDialog();
        }
        private void imgUser_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.imgUser, "Click here to search Employee");
        }

        private void imgUser_Click(object sender, EventArgs e)
        {
            clickSearch();
        }
        void clickSearch()
        {
            frmEmployeeLookup EmployeeLookupPage = new frmEmployeeLookup();
            EmployeeLookupPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            EmployeeLookupPage.ShowDialog();
        }
        private bool IsDuplicate()
        {
            clsUser _user = new clsUser();
            _user = new UserRepository().FindDuplicate(tbRegUsername.Text);
            if (_user != null)
            {
                return true;
            }
            return false;
        }
         private bool IsDuplicateId()
        {
            clsUser _user = new clsUser();
            _user = new UserRepository().FindDuplicateId(regId);
            if (_user != null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #endregion

         private void tbUsername_KeyUp(object sender, KeyEventArgs e)
         {
             if(e.KeyCode == Keys.Enter)
             {
                 canLogin();
             }
         }

         private void tbPassword_KeyUp(object sender, KeyEventArgs e)
         {
             if (e.KeyCode == Keys.Enter)
             {
                 canLogin();
             }
         }
         private void frmLogin_Load(object sender, EventArgs e)
         {
             tbUsername.Focus();
             ShowToolTip();
             lblVersion.Text= VersionLabel;
         }
         void ShowToolTip()
         {
             RadToolTip tooltip1 = new RadToolTip();
             tooltip1.SetToolTip(btnRegister, "Save changes");
         }
         void LoadBranch()
         {
             BranchRepo = new BranchRepository();
             cboBranch.DataSource = BranchRepo.GetAllActive();
             cboBranch.DisplayMember = "BranchName";
             cboBranch.ValueMember = "Id";
             cboBranch.Focus();
         }

         private void lnkUserLogs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
         {
             frmUsersLogList List = new frmUsersLogList();
             List.ShowDialog();
         }
         private bool IsOnline()
         {
             clsUsersLog currUser = new clsUsersLog();
             currUser = new UsersLogRepository().IsOnlineUser((string)tbUsername.Text);
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

         private void lnkInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
         {
             frmAboutSystem AS = new frmAboutSystem();
             AS.ShowDialog();
         }
         private void lnkCheckUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
         {
             InstallUpdateSyncWithInfo();
         }
         private void InstallUpdateSyncWithInfo()
         {
             UpdateCheckInfo info = null;

             if (ApplicationDeployment.IsNetworkDeployed)
             {
                 ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                 try
                 {
                     info = ad.CheckForDetailedUpdate();

                 }
                 catch (DeploymentDownloadException dde)
                 {
                     MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                     return;
                 }
                 catch (InvalidDeploymentException ide)
                 {
                     MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                     return;
                 }
                 catch (InvalidOperationException ioe)
                 {
                     MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                     return;
                 }

                 if (info.UpdateAvailable)
                 {

                     if (!info.IsUpdateRequired)
                     {
                         Boolean doUpdate = true;

                         DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now? \n Note: Please save your current transaction to prevent loss of data.", "Update Available", MessageBoxButtons.YesNo);
                         if (!(dr == DialogResult.Yes))
                         {
                             doUpdate = false;
                         }
                         else
                         {
                             try
                             {
                                 ad.Update();
                                 MessageBox.Show("The application has been upgraded, and will now restart.");
                                 Application.Restart();
                             }
                             catch (DeploymentDownloadException dde)
                             {
                                 MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
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
            else
            {
                //RadMessageBox.Show("Application is up-to-date.");
                ////MessageBox.Show("Application is up-to-date.");
                //return;
            }
        }
         public string VersionLabel
         {
             get
             {
                 if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                 {
                     Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                     return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                 }
                 else
                 {
                     var ver = Assembly.GetExecutingAssembly().GetName().Version;
                     return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                 }
             }
         }

        private void cboBranch_Click(object sender, EventArgs e)
        {
            LoadBranch();
        }
    }
}