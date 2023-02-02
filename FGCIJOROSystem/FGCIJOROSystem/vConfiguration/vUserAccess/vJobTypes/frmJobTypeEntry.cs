using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.JobType;
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

namespace FGCIJOROSystem.Presentation.vConfiguration.vJobTypes
{
    public partial class frmJobTypeEntry : Telerik.WinControls.UI.RadForm
    {
        public frmJobTypeEntry(clsEnums.CRUDEMode cm)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            crud = cm;
        }
        public frmJobTypeEntry(clsJobType EditJobType,  clsEnums.CRUDEMode cm)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            MapUpdate(EditJobType, cm);
        }

        public frmJobTypeEntry()
        {
            // TODO: Complete member initialization
            InitializeComponent();
        }
        #region Properties
        public clsEnums.CRUDEMode CRUDEMode { get; set; }
        clsJobType JobType;
        clsEnums.CRUDEMode crud;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion

        #region Save
        private void Save()
        {

            if (tbDescription.Text != "" || tbName.Text != "")
            {
                switch (crud)
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
                    Message = "All fields are required!"
                };
                MsgBox.ShowDialog();
                return;
            }  
            
        }
        #endregion

        #region Add
        private void Add()
        {
            JobType = new clsJobType();
            JobType.JobTypeName = tbName.Text;
            JobType.JobTypeDescription = tbDescription.Text;
            JobType.IsActive = tsActive.Value;
            new JobTypeRepository().Add(JobType);
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been saved successfully!"
            };
            MsgBox.ShowDialog();
        }
        #endregion
        #region Map Update
        private void MapUpdate(clsJobType MapJobType, clsEnums.CRUDEMode cm)
        {
            JobType = new clsJobType();
            JobType = MapJobType;
            tbName.Text = JobType.JobTypeName;
            tbDescription.Text = JobType.JobTypeDescription;
            tsActive.Value = JobType.IsActive;
            crud = new clsEnums.CRUDEMode();
            crud = cm;
        }
        #endregion
        
        #region Update
        private void Update()
        {
            JobType.JobTypeName = tbName.Text;
            JobType.JobTypeDescription = tbDescription.Text;
            JobType.IsActive = tsActive.Value;
            new JobTypeRepository().Update(JobType);
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been saved successfully!"
            };
            MsgBox.ShowDialog();
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
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
                    Save();
                }
                catch (Exception ex)
                {
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Error,
                        Message = "Error!"
                    };
                    MsgBox.ShowDialog();
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
                currUser.DayActivity = "Add Job Type Entry(" + tbName.Text + ")";
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
                currUser.DayActivity = "Update Job Type Entry(" + tbName.Text + ")";
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
