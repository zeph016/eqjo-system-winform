using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.Domain.Configurations.Branches;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Presentation.vLookups;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation.vConfiguration.vUsers
{
    public partial class frmUserEntry : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        
        public UserRepository _Repo;
        public clsUser Current;
        UserAccessLevelRepository UserLevelRepo;
        BranchRepository BranchRepo;
        public clsEnums.CRUDEMode CRUDEMode { get; set; }
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion
        public frmUserEntry()
        {
            InitializeComponent();
            Current = new clsUser();
            currUser = new clsUsersLog();
        }

        #region Methods
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Cancel();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        void Initialize()
        {
            cboUserLevel.ValueMember = "Id";
            cboUserLevel.DisplayMember = "Name";
        }

        void DisplayProperties()
        {

            tbxFullName.Text = Current.FullName;
            tbxPosition.Text = Current.Position;
            tbxSection.Text = Current.Section;
            tbxDepartment.Text = Current.Department;
            tbxDateHired.Text = Current.DateHired.ToShortDateString();
            tbxEmployeeStatus.Text = Current.EmployeeStatus;
            cboBranch.SelectedValue = (Int64)Current.BranchId;
            tbxUserName.Text = Current.UserName;
            if (String.IsNullOrEmpty(Current.UserPassword))
                tbxPassword.Text = "";
            else
                tbxPassword.Text = EncryptorManager.DecryptPassword(Current.UserPassword);
            //tbxPassword.Text =Current.UserPassword;

            tbxConfirmPassword.Text = tbxPassword.Text;

            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                cboUserLevel.SelectedValue = Current.UserLevelId;
                cboBranch.SelectedValue = Current.BranchId;
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                cboUserLevel.SelectedValue = (Int64)Current.UserLevelId;
                cboBranch.SelectedValue = Current.BranchId;
            }
            chkActive.Checked = Current.Active;

            SetPicture();
        }

        void MapDetails()
        {
            if (string.IsNullOrEmpty(tbxFullName.Text))
            {
                btnSearch.Focus();
                throw new Exception("Please select from Masterlist.");
            }

            if (string.IsNullOrEmpty(tbxUserName.Text))
            {
                tbxUserName.Focus();
                throw new Exception("Username cannot be blank.");
            }

            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                if (tbxPassword.Text.Trim().Length < 3)
                {
                    tbxPassword.SelectAll();
                    tbxPassword.Focus();
                    throw new Exception("Password must be at least 3 characters.");
                }

                if (tbxPassword.Text.Trim() != tbxConfirmPassword.Text.Trim())
                {
                    tbxConfirmPassword.Clear();
                    tbxConfirmPassword.Focus();
                    throw new Exception("Password confirmation does not match.");
                }

            }

            if (cboUserLevel.SelectedValue == null)
            {
                throw new Exception("Please select the level of user.");
            }

            Current.FullName = tbxFullName.Text;
            Current.Position = tbxPosition.Text;
            Current.Section = tbxSection.Text;
            Current.Department = tbxDepartment.Text;
            Current.DateHired = DateTime.Parse(tbxDateHired.Text);
            Current.EmployeeStatus = tbxEmployeeStatus.Text;
            Current.UserName = tbxUserName.Text;
            Current.UserPassword = EncryptorManager.EncryptPassword(tbxPassword.Text.Trim());
            //Current.UserPassword = tbxPassword.Text.Trim();
            Current.Active = chkActive.Checked;

            Current.BranchId = Int32.Parse(cboBranch.SelectedValue.ToString());
            Current.Branch = cboBranch.Text.ToString();           

            Current.UserLevelId = Int32.Parse(cboUserLevel.SelectedValue.ToString());
            Current.UserLevelName = cboUserLevel.Text.ToString();           
        }

        void Save()
        {
                MapDetails();

                if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
                {
                    Add();
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "New user account has been successfully saved."
                    };
                    MsgBox.ShowDialog();
            }
                else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
                {
                    Edit();
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "The record has been saved successfully and it will be taking effect after restarting the application."
                    };
                    MsgBox.ShowDialog();
            }
        }

        void Add()
        {
            this.Enabled = false;
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (s, e) =>
            {
                _Repo.Add(Current);
            };
            bw.RunWorkerCompleted += (s, e) =>
            {
                this.Enabled = true;
                if (e.Error == null)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                    if (e.Error != null) throw new Exception(e.Error.Message);
            };
            bw.RunWorkerAsync();
        }

        void Edit()
        {
            this.Enabled = false;
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (s, e) =>
            {
                _Repo.Update(Current);
            };
            bw.RunWorkerCompleted += (s, e) =>
            {
                if (e.Error == null)
                {
                    this.Enabled = true;
                    this.DialogResult = DialogResult.OK;
                }
                else
                    if (e.Error != null) throw new Exception(e.Error.Message);
            };
            bw.RunWorkerAsync();
        }

        void Cancel()
        {
            var confirm = RadMessageBox.Show(this, "Close entry?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            if (confirm == System.Windows.Forms.DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        void SetPicture()
        {
            byte[] picByte = new EmployeeRepository().GetPictureById(Current.MLEmployeeId);
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            if (picByte != null)
            {
                pictureBox3.BackgroundImage = ImageManager.byteArrayToImage(picByte);
            }
        }

        #endregion

        #region Listeners
        private void frmUserEntry_Load(object sender, EventArgs e)
        {
            LoadBranch();
            loadUserLevel();

            DisplayProperties();
            btnSearch.Focus();

            ShowToolTip();
            this.cboBranch.TextChanged += new EventHandler(this.cboBranch_TextChanged);
            this.cboUserLevel.TextChanged += new EventHandler(this.cboUserLevel_TextChanged);

            if (Program.CurrentUser.UserLevelId != 5)
            {
                cboUserLevel.ReadOnly = true;
                cboBranch.ReadOnly = true;
                btnSearch.Enabled = false;
                lblHeader.Text = "Update User Account";
            }
            else
            {
                cboUserLevel.ReadOnly = false;
                cboBranch.ReadOnly = false;
                btnSearch.Enabled = true;
                lblHeader.Text = "Create User Account";
            }

            if (tbxFullName.Text.Length > 0)
            {
                btnSearch.Enabled = false;
                btnSearch.Visible = false;
                lblHeader.Text = "Update User Account";
            }
            else
            {

            }

            checkUserLevel();

        }

        public void checkUserLevel()
        {

            if (Program.CurrentUser.UserLevelId < 5)
            {
                cboUserLevel.Enabled = false;
                cboBranch.Enabled = false;
                chkActive.Visible = false;
            }
            //if (cboUserLevel.Text == "Admin")
            //{
            //    cboUserLevel.Enabled = false;
            //    cboBranch.Enabled = false;
            //    chkActive.Visible = false;
            //}
        }
        void ShowToolTip()
        {
            RadToolTip tooltip1 = new RadToolTip();
            tooltip1.SetToolTip(btnSave, "Save changes");
            tooltip1.SetToolTip(btnCancel, "Cancel changes");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            if (Validation())
            {
                if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
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
                            Message = "Sorry, this account is already exist. Please contact the administrator for assistance."
                        };
                        MsgBox.ShowDialog();
                    }
                }
                else
                {
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "Sorry, " + tbxFullName.Text + " your username ( " + tbxUserName.Text + " ) is already exist. Please try a new one."
                    };
                    MsgBox.ShowDialog();
                }
                if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
                {
                    Save();
                }
                
            }
        }
        clsUsersLog AddMapProperties()
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
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
                currUser.DayActivity = "Add User (" + tbxFullName.Text + ")";
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
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
                currUser.DayActivity = "Update User (" + tbxFullName.Text + ")";
            }
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeLookup EmployeeLookupPage = new frmEmployeeLookup(clsEnums.LookupMode.Employee);
            EmployeeLookupPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            EmployeeLookupPage.ShowDialog();
        }

        void DataEvent_OnDataConfirm(object obj)
        {
            var emp = (clsEmployee)obj;
            Current.FullName = emp.FullName;
            Current.Position = emp.Position;
            Current.Section = emp.Section;
            Current.Department = emp.Department;
            Current.DateHired = emp.DateHired;
            Current.EmployeeStatus = emp.EmployeeStatus;
            Current.MLEmployeeId = emp.Id;

            tbxFullName.Text = Current.FullName;
            tbxPosition.Text = Current.Position;
            tbxSection.Text = Current.Section;
            tbxDepartment.Text = Current.Department;
            tbxDateHired.Text = Current.DateHired.ToShortDateString();
            tbxEmployeeStatus.Text = Current.EmployeeStatus;
            SetPicture();
        }
        void loadUserLevel()
        {
            UserLevelRepo = new UserAccessLevelRepository();
            cboUserLevel.DataSource = UserLevelRepo.SearchBy(cboUserLevel.Text.ToString());
            cboUserLevel.DisplayMember = "UserLevelName";
            cboUserLevel.ValueMember = "Id";
            cboUserLevel.Focus();
        }
        void LoadBranch()
        {
            BranchRepo = new BranchRepository();
            cboBranch.DataSource = BranchRepo.GetAllActive(); //from BranchRepo.GetAll();
            cboBranch.DisplayMember = "BranchName";
            cboBranch.ValueMember = "Id";
            cboBranch.Focus();
        }
        private bool IsDuplicate()
        {
            clsUser _user = new clsUser();
            _user = new UserRepository().FindDuplicate((String)tbxUserName.Text);
            if (_user != null)
            {
                return true;
            }
            return false;
        }
        private bool IsDuplicateId()
        {
            clsUser _user = new clsUser();
            _user = new UserRepository().FindDuplicateId(Current.MLEmployeeId);
            if (_user != null)
            {
                return true;
            }
            return false;
        }
        bool Validation()
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                if (string.IsNullOrWhiteSpace(tbxFullName.Text))
                {
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "Please select EMPLOYEE to register first."
                    };
                    MsgBox.ShowDialog();
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(tbxUserName.Text) || string.IsNullOrWhiteSpace(tbxPassword.Text))
                {
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "Username and Password are required."
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
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
            }
            return true;
        }
        #endregion

        private void cboBranch_TextChanged(object sender, EventArgs e)
        {
            cboBranch.ShowDropDown();
        }

        private void cboUserLevel_TextChanged(object sender, EventArgs e)
        {
            cboUserLevel.ShowDropDown();
        }

        private void chkShowHidePass_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkShowHidePass.Checked)
            {
                tbxPassword.PasswordChar = '\0';
                tbxConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                tbxPassword.PasswordChar = '•';
                tbxConfirmPassword.PasswordChar = '•';
            }
        }
    }
}
