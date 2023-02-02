using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.Domain.Configurations.UnitsJORO;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using FGCIJOROSystem.Domain.Configurations.Users;
namespace FGCIJOROSystem.Presentation.vConfiguration.vUnits
{
    public partial class frmUnit : Telerik.WinControls.UI.RadForm
    {
        public frmUnit()
        {
            InitializeComponent();
            CRUD = new clsEnums.CRUDEMode();
            CRUD = clsEnums.CRUDEMode.Add;
            UnitJORO = new clsUnitJORO();
            currUser = new clsUsersLog();
        }
        public frmUnit(clsUnitJORO obj)
        {
            InitializeComponent();
            CRUD = new clsEnums.CRUDEMode();
            CRUD = clsEnums.CRUDEMode.Edit;
            UnitJORO = new clsUnitJORO();
            UnitJORO = obj;
            currUser = new clsUsersLog();
        }
        #region Properties
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        clsUnitJORO UnitJORO;
        clsEnums.CRUDEMode CRUD;
        Action SaveAction;
        public List<clsProductUnit> ListOfProductUnit { get; set; }
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion

        #region Get Product Units List
        private void GetUnitList()
        {
            List<clsProductUnit> ListOfProductUnit = new List<clsProductUnit>();
            ListOfProductUnit = new ProductUnitRepository().GetAll();
            cbUnit.DataSource = ListOfProductUnit;
            cbUnit.ValueMember = "Id";
            cbUnit.DisplayMember = "UnitName";
        }
        #endregion

        #region Mapping
        private clsUnitJORO Mapping(clsUnitJORO curr)
        {
            UnitJORO.UnitId = (Int64)cbUnit.SelectedValue;
            UnitJORO.MinutesValue = (double)tbMinutesValue.Value;
            UnitJORO.IsActive = tgActive.Value;
            return UnitJORO;
        }
        
        #endregion
        #region Check Duplicate
        private bool IsDuplicate()
        {
            clsUnitJORO unit = new clsUnitJORO();
            unit = new UnitsJORORepository().FindDuplicate((Int64)cbUnit.SelectedValue);
            if (unit != null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Add
        private void Add()
        {
            if (IsDuplicate() == false)
            {
                new UnitsJORORepository().Add(Mapping(UnitJORO));
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Success,
                    Message = "The record has been saved successfully!"
                };
                MsgBox.ShowDialog();
            }
            else
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Success,
                    Message = "Unit name ( " + cbUnit.Text + " ) is already exist!"
                };
                MsgBox.ShowDialog();
            }
            
        }
        #endregion
        #region Update
        private void DispayProperties()
        {
            cbUnit.SelectedValue = UnitJORO.UnitId;
            tbMinutesValue.Value = (Decimal)UnitJORO.MinutesValue;
            tgActive.Value = UnitJORO.IsActive;
        }
        private void Edit()
        {
            new UnitsJORORepository().Update(Mapping(UnitJORO));
            frmMsg MsgBox = new frmMsg()
            {
                MsgBox = clsEnums.MsgBox.Success,
                Message = "The record has been saved successfully!"
            };
            MsgBox.ShowDialog();
        }
        #endregion
        private void frmUnit_Load(object sender, EventArgs e)
        {
            GetUnitList();
            switch (CRUD)
            {
                case clsEnums.CRUDEMode.Add:
                    SaveAction = Add;
                    break;
                case clsEnums.CRUDEMode.Edit:
                    DispayProperties();
                    SaveAction = Edit;
                    break;
                case clsEnums.CRUDEMode.Delete:
                    break;
                default:
                    break;
            }
            this.cbUnit.TextChanged += new System.EventHandler(this.cbUnit_TextChanged);
        }
        private void cbUnit_TextChanged(object sender, EventArgs e)
        {
            cbUnit.ShowDropDown();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            if (cbUnit.Text=="")
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "All fields with * are required."
                };
                MsgBox.ShowDialog();
                cbUnit.Focus();
            }
            else
            {
                try
                {
                    SaveAction.Invoke();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
                currUser.DayActivity = "Add Unit (" + cbUnit.Text + ")";
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
                currUser.DayActivity = "Update Unit (" + cbUnit.Text + ")";
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
