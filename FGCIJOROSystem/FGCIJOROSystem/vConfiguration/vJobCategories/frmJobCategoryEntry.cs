using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.JobCategories;
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
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation.vConfiguration.vJobCategories
{
    public partial class frmJobCategoryEntry : Telerik.WinControls.UI.RadForm
    {
        public frmJobCategoryEntry(clsEnums.CRUDEMode cm)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            CRUDMode = new clsEnums.CRUDEMode();
            CRUDMode = cm;
        }
        public frmJobCategoryEntry(clsEnums.CRUDEMode cm, clsJobCategory jc)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            CRUDMode = new clsEnums.CRUDEMode();
            CRUDMode = cm;
            MapUpdate(jc);
        }

        public frmJobCategoryEntry()
        {
            // TODO: Complete member initialization
            InitializeComponent();
            CRUDMode = new clsEnums.CRUDEMode();
            currUser = new clsUsersLog();
        }
        #region Properties
        public clsEnums.CRUDEMode CRUDEMode { get; set; }
        clsEnums.CRUDEMode CRUDMode;
        clsJobCategory JobCategory;
        clsUsersLog currUser;
        UsersLogRepository UsersLogRepo;
        #endregion
        #region Add
        private void Add()
        {
            JobCategory = new clsJobCategory();
            JobCategory.JobCategoryName = tbName.Text;
            JobCategory.Description = tbDescription.Text;
            JobCategory.IsActive = tsActive.Value;
            new JobCategoryRepository().Add(JobCategory);
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been saved successfully!"
            };
            MsgBox.ShowDialog();
        }
        #endregion
        #region Update
        private void MapUpdate(clsJobCategory jc)
        {
            JobCategory = new clsJobCategory();
            JobCategory = jc;
            tbName.Text = JobCategory.JobCategoryName;
            tbDescription.Text = JobCategory.Description;
            tsActive.Value = JobCategory.IsActive;
        }
        private void Update()
        {
            JobCategory.JobCategoryName = tbName.Text;
            JobCategory.Description = tbDescription.Text;
            JobCategory.IsActive = tsActive.Value;
            new JobCategoryRepository().Update(JobCategory);
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been saved successfully!"
            };
            MsgBox.ShowDialog();
        }
        #endregion
        #region Save
        void Save()
        {
            if (tbName.Text=="")
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "All fields with * are required."
                };
                MsgBox.ShowDialog();
                tbName.Focus();
            }
            else
            {
                try
                {
                    if (tbName.Text != "" || tbDescription.Text != "")
                    {
                        switch (CRUDMode)
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
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else
                    {

                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Warning,
                            Message = "The record has been saved successfully!"
                        };
                        MsgBox.ShowDialog();
                    }
                }
                catch (Exception ex)
                {

                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Error,
                        Message = "Error!"
                    };
                    MsgBox.ShowDialog();
                    return;
                }
            }
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            Save();
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
                currUser.DayActivity = "Add Job Category (" + tbName.Text + ")";
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
                currUser.DayActivity = "Update Job Category (" + tbName.Text + ")";
            }
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }


    }
}
